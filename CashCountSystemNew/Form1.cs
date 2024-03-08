using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using System.Net;
using Modle;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using MvCamCtrl.NET;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using System.Security.Policy;

namespace CashCountSystem
{
    public partial class Form1 : Form
    {
        #region 私有变量
        private Round _round = new Round();
        private Machine _machine = new Machine();
        private Worker _worker = new Worker();
        string[] imagesPath = new string[0];//储存照片路径，在窗体加载事件中会根据每轮计数次数设置其数组大小
        List<PictureBox> pictureBoxes = new List<PictureBox>();//窗体加载时tabControl1控件中添加的picturebox控件的集合
        FormWait frm = null;//载入框窗体对象
        List<CashCount> _cashCounts = new List<CashCount>();
        #endregion

        #region 连接相机所需变量，来源：海康源码
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", SetLastError = false)]
        private static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        // ch:判断用户自定义像素格式 | en:Determine custom pixel format
        public const Int32 CUSTOMER_PIXEL_FORMAT = unchecked((Int32)0x80000000);

        MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        private MyCamera m_MyCamera = new MyCamera();
        bool m_bGrabbing = false;
        Thread m_hReceiveThread = null;
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        UInt32 m_nBufSizeForDriver = 0;
        IntPtr m_BufForDriver = IntPtr.Zero;
        private static Object BufForDriverLock = new Object();

        // ch:Bitmap及其像素格式 | en:Bitmap and Pixel Format
        Bitmap m_bitmap = null;
        PixelFormat m_bitmapPixelFormat = PixelFormat.DontCare;
        IntPtr m_ConvertDstBuf = IntPtr.Zero;
        UInt32 m_nConvertDstBufLen = 0;
        MyCamera.MV_SAVE_IMG_TO_FILE_PARAM stSaveFileParam = new MyCamera.MV_SAVE_IMG_TO_FILE_PARAM();
        #endregion

        #region 拍照并保存图片
        private void bnSaveJpg_Click(object sender, EventArgs e)
        {
            if (GetFirstEmptyPictureBox() == null)
            {
                MessageBox.Show("照片已拍摄完毕");
                return;
            }
            if (false == m_bGrabbing)
            {
                ShowErrorMsg("Not Start Grabbing", 0);
                return;
            }
            lock (BufForDriverLock)
            {
                if (m_stFrameInfo.nFrameLen == 0)
                {
                    ShowErrorMsg("Save Jpeg Fail!", 0);
                    return;
                }
                stSaveFileParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
                stSaveFileParam.enPixelType = m_stFrameInfo.enPixelType;
                stSaveFileParam.pData = m_BufForDriver;
                stSaveFileParam.nDataLen = m_stFrameInfo.nFrameLen;
                stSaveFileParam.nHeight = m_stFrameInfo.nHeight;
                stSaveFileParam.nWidth = m_stFrameInfo.nWidth;
                stSaveFileParam.nQuality = 80;
                stSaveFileParam.iMethodValue = 2;
                stSaveFileParam.pImagePath = "E:/Ftp/carmar/" + "Image_w" + stSaveFileParam.nWidth.ToString() + "_h" + stSaveFileParam.nHeight.ToString() + "_fn" + m_stFrameInfo.nFrameNum.ToString() + ".jpg";
                m_MyCamera.MV_CC_SaveImageToFile_NET(ref stSaveFileParam);//保存图片
                //将拍摄的图片显示在控件内
                PictureBox emptyPictureBox = GetFirstEmptyPictureBox();
                emptyPictureBox.Image = Image.FromFile(stSaveFileParam.pImagePath);
                for (int i = 0; i < imagesPath.Length; i++)
                {
                    if (imagesPath[i] == null)
                    {
                        imagesPath[i] = stSaveFileParam.pImagePath;
                        return;
                    }
                }
            }

        }
        /// <summary>
        /// 获取第一个值为null的picturebox控件
        /// </summary>
        /// <returns></returns>
        private PictureBox GetFirstEmptyPictureBox()
        {
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is PictureBox pictureBox && pictureBox.Image == null && pictureBox.Name != "pictureBox")
                    {
                        return pictureBox;
                    }
                }
            }
            return null; // 如果没有找到值为null的PictureBox
        }
        #endregion

        #region 构造函数，从窗体登录界面获取对象machine和worker
        public Form1(Machine machine, Worker worker)
        {
            InitializeComponent();
            //显示设备信息，操作人
            this._machine = machine;
            this._worker = worker;
            tbMachineIP.Text = machine.machineIP;
            tbMachineID.Text = machine.machineID.ToString();
            tbWorkerName.Text = worker.workerName;
            tbWorkerNumber.Text = worker.workerNumber;
            labelCount.Text = machine.machineCountNumber.ToString();
            //设定路径数组的大小
            this.imagesPath = new string[machine.machineCountNumber];
            for (int i = 0; i < machine.machineCountNumber; i++)
            {
                _cashCounts.Add(null);
            }
            dgvCount.DataSource = _cashCounts;
        }
        #endregion

        #region 窗体加载事件
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();// 启动定时器
            UpdateClock();// 初始化显示当前系统时间

            DeviceListAcq();//显示摄像头设备到选择框
            Control.CheckForIllegalCrossThreadCalls = false;

            for (int i = 1; i <= int.Parse(labelCount.Text); i++)
            {
                // 创建新的选项卡页
                TabPage newTabPage = new TabPage($"照片{i}");
                // 将新的选项卡页添加到TabControl
                tabControl1.TabPages.Add(newTabPage);
                // 在选项卡页中添加一个PictureBox
                PictureBox pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;// 设置图像显示模式
                pictureBox.Dock = DockStyle.Fill; // 设置PictureBox填充整个选项卡页
                pictureBox.Name = "pictureBox" + i;
                newTabPage.Controls.Add(pictureBox);
                pictureBoxes.Add(pictureBox);

            }

            #region 修改界面中表格属性

            dgvCount.AutoGenerateColumns = false;
            dgvCount.ClearSelection();
            dgvCount.MultiSelect = false;
            dgvRound.Enabled = false;

            #endregion

            #region Aforge调用摄像头相关代码，已废弃（只能调用本机和外接摄像头，无法调用网口相机）
            //this._videoDevices = CashCarmear.ConnectCarmear();
            //if (this._videoDevices != null)
            //{
            //    foreach (FilterInfo device in this._videoDevices)
            //    {
            //        cboCarmear.Items.Add(device.Name);//把摄像设备添加到摄像列表中
            //    }
            //}
            //cboCarmear.SelectedIndex = 0;//默认选择第一个
            ////获取所选摄像头
            //this._videoDevice = new VideoCaptureDevice(this._videoDevices[cboCarmear.SelectedIndex].MonikerString);
            ////获取摄像头的分辨率数组
            //this._videoCapabilities = this._videoDevice.VideoCapabilities;
            //foreach (VideoCapabilities capabilty in this._videoCapabilities)
            //{
            //    //把这个设备的所有分辨率添加到列表
            //    cboResolution.Items.Add($"{capabilty.FrameSize.Width} x {capabilty.FrameSize.Height}");
            //}
            //cboResolution.SelectedIndex = 0;//默认选择第一个
            #endregion
        }
        #endregion

        #region 时间显示
        private void UpdateClock()
        {
            // 更新 Label4 显示当前系统时间
            label4.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 定时器触发时更新系统时间
            UpdateClock();
        }
        #endregion

        #region Post请求
        /// <summary>
        /// 图片转64编码(使用路径会发生线程冲突，舍弃)
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        static string GetFileContentAsBase64(string path)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Open))
            {
                byte[] arr = new byte[filestream.Length];
                filestream.Read(arr, 0, (int)filestream.Length);
                string base64 = Convert.ToBase64String(arr);
                return base64;
            }
        }

        /// <summary>
        /// 图片转64编码
        /// </summary>
        /// <param name="image">图片对象</param>
        /// <returns></returns>
        static string ImageToBase64(Image image)
        {
            if (image != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // 将图像保存到内存流中
                    image.Save(memoryStream, image.RawFormat);

                    // 获取字节数组
                    byte[] imageBytes = memoryStream.ToArray();

                    // 将字节数组转换为Base64编码的字符串
                    string base64String = Convert.ToBase64String(imageBytes);

                    return base64String;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="jsonContent">参数（传递图片的base64编码）</param>
        /// <returns></returns>
        private async Task<string> PostHelperAsync(string jsonContent)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // 创建HttpContent对象
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // 发送POST请求
                HttpResponseMessage response = await httpClient.PostAsync("http://192.168.2.50:3000/mock/11/v1.0/api/get-count-by-bin", content);

                if (response.IsSuccessStatusCode)
                {
                    // 请求成功，处理响应数据
                    string responseData = await response.Content.ReadAsStringAsync();
                    return responseData;
                }
                else
                {
                    // 请求失败，处理错误
                    return ("Error" + response.StatusCode.ToString());
                }
            }
        }

        #endregion

        #region 开始计数逻辑
        private async void btnBegin_Click(object sender, EventArgs e)
        {
            ImageService imageService = new ImageService();
            CashCountService cashCountService = new CashCountService();
            RoundService roundService = new RoundService();

            //判断照片是否拍摄完毕
            if (GetFirstEmptyPictureBox() != null)
            {
                MessageBox.Show("请拍照指定次数！");
                return;
            }

            //该轮计数开始
            if (roundService.Select(this._round.countRoundID) == null)
            {
                roundService.Insert(this._round);
            }

            for (int i = 0; i < _machine.machineCountNumber; i++)
            {
                if (_cashCounts[i] == null)
                {
                    string imageBytes = ImageToBase64(pictureBoxes[i].Image);//图片的64位编码
                                                                             // 构建要发送的数据
                    var requestData = new
                    {
                        data = imageBytes,//图片的64位编码
                        detectNum = 10,//返回结果个数
                        len = imageBytes.Length//图片大小
                    };
                    string jsonContent = JsonConvert.SerializeObject(requestData);// 将参数转为JSON字符串
                    string responseData = await PostHelperAsync(jsonContent);//获取返回结果
                    JsonData jsonData = JsonConvert.DeserializeObject<JsonData>(responseData);//返回结果映射
                    string dataValue = jsonData.Data;//获取计数结果的值
                    string[] values = dataValue.Split(',');
                    //为该次计数实体赋值
                    CashCount cashCount = new CashCount();
                    cashCount.FK_roundID = this._round.countRoundID;
                    cashCount.FK_workerID = this._worker.workerID;
                    cashCount.FK_machineID = this._machine.machineID;
                    cashCount.countResult = int.Parse(values[0]);
                    cashCount.countDoubleResult = int.Parse(values[1]);
                    //cashCount.resultJudge = cashCount.resultJudge = (cashCount.countResult == cashCount.countDoubleResult) ? false : true;
                    cashCount.resultJudge = cashCount.resultJudge = (cashCount.countResult == cashCount.countDoubleResult) ? true : false;
                    cashCount.operatTime = DateTime.Now;
                    cashCountService.Insert(cashCount);

                    //为该次计数实体对应的图片赋值并插入数据库
                    CashImage cashImage = new CashImage();
                    cashImage.imagePath = imagesPath[i];
                    cashImage.FK_CountID = cashCount.countID;
                    imageService.Insert(cashImage);
                }
            }
            //查询该轮计数所有结果,并显示在dgvCount表中
            _cashCounts = cashCountService.Selects(this._round.countRoundID);
            dgvCount.DataSource = _cashCounts;
            dgvCount.ClearSelection();
            DgvShow();

            #region 每轮完成标志判断，完成后数据自动写入dgvRound表中
            bool passFlag = true;
            List<int> deleteCountIndexs = new List<int>();
            for (int i = 0; i < _cashCounts.Count; i++)
            {
                if (_cashCounts[i].resultJudge == false)
                {
                    passFlag = false;
                    break;
                }
            }

            if (dgvCount.RowCount >= this._machine.machineCountNumber && passFlag)
            {
                MessageBox.Show("已达到循环要求次数，且全部通过，即将写入数据", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCount.DataSource = null;
                dgvCount.Refresh();
                this._round.finalResult = _cashCounts[0].countResult;
                //将最终结果写入第二张表
                dgvRound.Rows.Add(this._round.countRoundID, this._round.finalResult, this._worker.workerName,
                this._machine.machineID, this._machine.machineIP, DateTime.Now);
                dgvRound.ClearSelection();
                roundService.Update(this._round);
                this._round.countRoundID = 0;
                //清空图片
                foreach (PictureBox pictureBox in pictureBoxes)
                {
                    pictureBox.Image = null;
                }
                for (int i = 0; i < _cashCounts.Count; i++)
                {
                    _cashCounts[i] = null;
                }

            }
            else if (dgvCount.RowCount >= this._machine.machineCountNumber && !passFlag)
            {
                DialogResult mes = MessageBox.Show("请确保表中数据全为通过,是否删除未通过的数据", "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (mes == DialogResult.OK)
                {
                    for (int i = 0; i < _cashCounts.Count; i++)
                    {
                        if (_cashCounts[i].resultJudge == false)
                        {
                            deleteCountIndexs.Add(i);
                            cashCountService.Delete(_cashCounts[i]);
                            _cashCounts[i] = null;
                        }
                    }
                    dgvCount.DataSource = _cashCounts;
                    dgvCount.ClearSelection();
                    dgvCount.Refresh();
                    foreach (DataGridViewRow row in dgvCount.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }

                    for (int i = 0; i < pictureBoxes.Count; i++)
                    {
                        for (int j = 0; j < deleteCountIndexs.Count; j++)
                        {
                            if (i == deleteCountIndexs[j])
                            {
                                pictureBoxes[i].Image = null;
                            }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        /// <summary>
        /// 遍历dgvCount表中resultJudge列的数据，若有不通过，则显示红色
        /// </summary>
        private void DgvShow()
        {
            // 获取列索引
            int columnIndex = dgvCount.Columns["resultJudge"].Index;
            // 遍历每一行
            foreach (DataGridViewRow row in dgvCount.Rows)
            {
                // 检查单元格是否存在，并且不为 null

                if (bool.Parse(row.Cells[columnIndex].Value.ToString()) == false)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }

            //设置dgvCount表自增长列，用于标记每轮的次数
            for (int i = 0; i < dgvCount.Rows.Count; i++)
            {
                dgvCount.Rows[i].Cells["countNumber"].Value = (i + 1).ToString();
            }
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 删除dgvCount表中不通过的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteCount_Click(object sender, EventArgs e)
        {
            if (dgvCount.SelectedRows.Count > 0 && dgvCount.SelectedRows != null)
            {
                DataGridViewRow selectedRow = dgvCount.SelectedRows[0];
                // 获取选中行列名为 "countID"的数据,并赋值给要删除的cashCount
                if (selectedRow.Cells["countID"].Value != null)
                {
                    int cashCountID = int.Parse(selectedRow.Cells["countID"].Value.ToString());
                    CashCountService cashCountService = new CashCountService();
                    CashCount cashCount = cashCountService.Select(cashCountID);//获取要删除的cashCount实体
                    int FK_countID = cashCount.countID;//获取要删除的图片外键FK_countID
                    ImageService imageService = new ImageService();
                    CashImage cashImage = imageService.Select(FK_countID);//获取要删除的图片实体
                    for (int i = 0; i < imagesPath.Length; i++)
                    {
                        //判断，如果要删除的图片实体的储存路径和图片路径数组中的值相同，则将其赋为null值，以便重新拍照赋值
                        if (imagesPath[i] == cashImage.imagePath)
                        {
                            imagesPath[i] = null;
                            pictureBoxes[i].Image = null;//设置其对应的picturebox控件图片为null
                            _cashCounts[i] = null;
                            break;
                        }
                    }
                    cashCountService.Delete(cashCount);
                    //查询该轮计数所有结果,并显示在dgvCount表中
                    dgvCount.DataSource = _cashCounts;
                    if (dgvCount.SelectedRows.Count > 0)
                    {
                        // 获取选中行的索引
                        int selectedIndex = dgvCount.SelectedRows[0].Index;

                        // 更改选中行的背景色
                        dgvCount.Rows[selectedIndex].DefaultCellStyle.BackColor = Color.White;
                        //dgvCount.Refresh();
                    }
                    dgvCount.ClearSelection();
                }

                else
                {
                    MessageBox.Show("请选择数据", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 关闭窗体时，同时关闭摄像机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ch:取流标志位清零 | en:Reset flow flag bit
            if (m_bGrabbing == true)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
            }

            if (m_BufForDriver != IntPtr.Zero)
            {
                Marshal.Release(m_BufForDriver);
            }

            // ch:关闭设备 | en:Close Device
            m_MyCamera.MV_CC_CloseDevice_NET();
            m_MyCamera.MV_CC_DestroyDevice_NET();
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            int selectImageIndex = tabControl1.SelectedIndex;//获取选定页面索引
            selectImageIndex -= 1;//获取路径数组索引
            if (selectImageIndex >= 0)
            {
                imagesPath[selectImageIndex] = null;
                PictureBox pictureBox = FindPictureBoxInTabPage(tabControl1.SelectedTab);
                pictureBox.Image = null;
            }
            else
            {
                MessageBox.Show("先选择图片！");
            }
        }

        /// <summary>
        /// 找到选定页面的PictureBox控件
        /// </summary>
        /// <param name="tabPage"></param>
        /// <returns></returns>
        private PictureBox FindPictureBoxInTabPage(TabPage tabPage)
        {
            foreach (Control control in tabPage.Controls)
            {
                if (control is PictureBox)
                {
                    return (PictureBox)control;
                }
            }
            return null;
        }

        #region 连接相机，来源：海康源码


        /// <summary>
        /// 
        /// </summary>
        private void ShowProgress()
        {
            //if (frm == null || frm.IsDisposed)
            //{
            frm = new FormWait();
            frm.ShowDialog();
            //}
            //else
            //{
            //    frm.ShowDialog();
            //}
        }
        private void bnOpen_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(this.ShowProgress));
            th.Start();//开启载入框线程


            if (m_stDeviceList.nDeviceNum == 0 || cbDeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("No device, please select", 0);
                frm.IsToColse = true;//通知等待窗体关闭
                return;
            }

            // ch:获取选择的设备信息 | en:Get selected device information
            MyCamera.MV_CC_DEVICE_INFO device =
                (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[cbDeviceList.SelectedIndex],
                                                              typeof(MyCamera.MV_CC_DEVICE_INFO));

            // ch:打开设备 | en:Open device
            if (null == m_MyCamera)
            {
                m_MyCamera = new MyCamera();
                if (null == m_MyCamera)
                {
                    ShowErrorMsg("Applying resource fail!", MyCamera.MV_E_RESOURCE);
                    frm.IsToColse = true;//通知等待窗体关闭
                    return;
                }
            }

            int nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Create device fail!", nRet);
                frm.IsToColse = true;//通知等待窗体关闭
                return;
            }

            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                m_MyCamera.MV_CC_DestroyDevice_NET();
                ShowErrorMsg("Device open fail!", nRet);
                frm.IsToColse = true;//通知等待窗体关闭
                return;
            }

            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.MV_CC_SetIntValueEx_NET("GevSCPSPacketSize", nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        ShowErrorMsg("Set Packet Size failed!", nRet);
                        frm.IsToColse = true;//通知等待窗体关闭
                    }
                }
                else
                {
                    ShowErrorMsg("Get Packet Size failed!", nPacketSize);
                    frm.IsToColse = true;//通知等待窗体关闭
                }
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

            // ch:前置配置 | en:pre-operation
            int nRetL = NecessaryOperBeforeGrab();
            if (MyCamera.MV_OK != nRetL)
            {
                frm.IsToColse = true;//通知等待窗体关闭
                return;
            }
            // ch:标志位置true | en:Set position bit true
            m_bGrabbing = true;

            m_stFrameInfo.nFrameLen = 0;//取流之前先清除帧长度
            m_stFrameInfo.enPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Undefined;

            m_hReceiveThread = new Thread(ReceiveThreadProcess);
            m_hReceiveThread.Start();

            // ch:开始采集 | en:Start Grabbing
            nRetL = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRetL)
            {
                m_bGrabbing = false;
                m_hReceiveThread.Join();
                ShowErrorMsg("Start Grabbing Fail!", nRetL);
                frm.IsToColse = true;//通知等待窗体关闭
                return;
            }
            frm.IsToColse = true;//通知等待窗体关闭
        }
        /// <summary>
        /// 取图前的必要操作步骤，来源：海康源码
        /// </summary>
        /// <returns></returns>
        private Int32 NecessaryOperBeforeGrab()
        {
            // ch:取图像宽 | en:Get Iamge Width
            MyCamera.MVCC_INTVALUE_EX stWidth = new MyCamera.MVCC_INTVALUE_EX();
            int nRet = m_MyCamera.MV_CC_GetIntValueEx_NET("Width", ref stWidth);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Get Width Info Fail!", nRet);
                return nRet;
            }
            // ch:取图像高 | en:Get Iamge Height
            MyCamera.MVCC_INTVALUE_EX stHeight = new MyCamera.MVCC_INTVALUE_EX();
            nRet = m_MyCamera.MV_CC_GetIntValueEx_NET("Height", ref stHeight);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Get Height Info Fail!", nRet);
                return nRet;
            }
            // ch:取像素格式 | en:Get Pixel Format
            MyCamera.MVCC_ENUMVALUE stPixelFormat = new MyCamera.MVCC_ENUMVALUE();
            nRet = m_MyCamera.MV_CC_GetEnumValue_NET("PixelFormat", ref stPixelFormat);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Get Pixel Format Fail!", nRet);
                return nRet;
            }

            // ch:设置bitmap像素格式，申请相应大小内存 | en:Set Bitmap Pixel Format, alloc memory

            else if (IsMono(stPixelFormat.nCurValue))
            {
                m_bitmapPixelFormat = PixelFormat.Format8bppIndexed;

                if (IntPtr.Zero != m_ConvertDstBuf)
                {
                    Marshal.Release(m_ConvertDstBuf);
                    m_ConvertDstBuf = IntPtr.Zero;
                }

                // Mono8为单通道
                m_nConvertDstBufLen = (UInt32)(stWidth.nCurValue * stHeight.nCurValue);
                m_ConvertDstBuf = Marshal.AllocHGlobal((Int32)m_nConvertDstBufLen);
                if (IntPtr.Zero == m_ConvertDstBuf)
                {
                    ShowErrorMsg("Malloc Memory Fail!", MyCamera.MV_E_RESOURCE);
                    return MyCamera.MV_E_RESOURCE;
                }
            }
            else
            {
                m_bitmapPixelFormat = PixelFormat.Format24bppRgb;

                if (IntPtr.Zero != m_ConvertDstBuf)
                {
                    Marshal.FreeHGlobal(m_ConvertDstBuf);
                    m_ConvertDstBuf = IntPtr.Zero;
                }

                // RGB为三通道
                m_nConvertDstBufLen = (UInt32)(3 * stWidth.nCurValue * stHeight.nCurValue);
                m_ConvertDstBuf = Marshal.AllocHGlobal((Int32)m_nConvertDstBufLen);
                if (IntPtr.Zero == m_ConvertDstBuf)
                {
                    ShowErrorMsg("Malloc Memory Fail!", MyCamera.MV_E_RESOURCE);
                    return MyCamera.MV_E_RESOURCE;
                }
            }

            // 确保释放保存了旧图像数据的bitmap实例，用新图像宽高等信息new一个新的bitmap实例
            if (null != m_bitmap)
            {
                m_bitmap.Dispose();
                m_bitmap = null;
            }
            m_bitmap = new Bitmap((Int32)stWidth.nCurValue, (Int32)stHeight.nCurValue, m_bitmapPixelFormat);

            // ch:Mono8格式，设置为标准调色板 | en:Set Standard Palette in Mono8 Format
            if (PixelFormat.Format8bppIndexed == m_bitmapPixelFormat)
            {
                ColorPalette palette = m_bitmap.Palette;
                for (int i = 0; i < palette.Entries.Length; i++)
                {
                    palette.Entries[i] = Color.FromArgb(i, i, i);
                }
                m_bitmap.Palette = palette;
            }

            return MyCamera.MV_OK;
        }
        /// <summary>
        /// 像素类型是否为Mono格式，来源：海康源码
        /// </summary>
        /// <param name="enPixelType"></param>
        /// <returns></returns>
        private Boolean IsMono(UInt32 enPixelType)
        {
            switch (enPixelType)
            {
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono1p:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono2p:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono4p:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8_Signed:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono14:
                case (UInt32)MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono16:
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 相机线程，来源：海康源码
        /// </summary>
        public void ReceiveThreadProcess()
        {
            MyCamera.MV_FRAME_OUT stFrameInfo = new MyCamera.MV_FRAME_OUT();
            MyCamera.MV_DISPLAY_FRAME_INFO stDisplayInfo = new MyCamera.MV_DISPLAY_FRAME_INFO();
            MyCamera.MV_CC_PIXEL_CONVERT_PARAM stConvertInfo = new MyCamera.MV_CC_PIXEL_CONVERT_PARAM();
            int nRet = MyCamera.MV_OK;

            while (m_bGrabbing)
            {
                nRet = m_MyCamera.MV_CC_GetImageBuffer_NET(ref stFrameInfo, 1000);
                if (nRet == MyCamera.MV_OK)
                {
                    lock (BufForDriverLock)
                    {
                        if (m_BufForDriver == IntPtr.Zero || stFrameInfo.stFrameInfo.nFrameLen > m_nBufSizeForDriver)
                        {
                            if (m_BufForDriver != IntPtr.Zero)
                            {
                                Marshal.Release(m_BufForDriver);
                                m_BufForDriver = IntPtr.Zero;
                            }

                            m_BufForDriver = Marshal.AllocHGlobal((Int32)stFrameInfo.stFrameInfo.nFrameLen);
                            if (m_BufForDriver == IntPtr.Zero)
                            {
                                return;
                            }
                            m_nBufSizeForDriver = stFrameInfo.stFrameInfo.nFrameLen;
                        }

                        m_stFrameInfo = stFrameInfo.stFrameInfo;
                        CopyMemory(m_BufForDriver, stFrameInfo.pBufAddr, stFrameInfo.stFrameInfo.nFrameLen);

                        // ch:转换像素格式 | en:Convert Pixel Format
                        stConvertInfo.nWidth = stFrameInfo.stFrameInfo.nWidth;
                        stConvertInfo.nHeight = stFrameInfo.stFrameInfo.nHeight;
                        stConvertInfo.enSrcPixelType = stFrameInfo.stFrameInfo.enPixelType;
                        stConvertInfo.pSrcData = stFrameInfo.pBufAddr;
                        stConvertInfo.nSrcDataLen = stFrameInfo.stFrameInfo.nFrameLen;
                        stConvertInfo.pDstBuffer = m_ConvertDstBuf;
                        stConvertInfo.nDstBufferSize = m_nConvertDstBufLen;
                        if (PixelFormat.Format8bppIndexed == m_bitmap.PixelFormat)
                        {
                            stConvertInfo.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                            m_MyCamera.MV_CC_ConvertPixelType_NET(ref stConvertInfo);
                        }
                        else
                        {
                            stConvertInfo.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_BGR8_Packed;
                            m_MyCamera.MV_CC_ConvertPixelType_NET(ref stConvertInfo);
                        }

                        // ch:保存Bitmap数据 | en:Save Bitmap Data
                        BitmapData bitmapData = m_bitmap.LockBits(new Rectangle(0, 0, stConvertInfo.nWidth, stConvertInfo.nHeight), ImageLockMode.ReadWrite, m_bitmap.PixelFormat);
                        CopyMemory(bitmapData.Scan0, stConvertInfo.pDstBuffer, (UInt32)(bitmapData.Stride * m_bitmap.Height));
                        m_bitmap.UnlockBits(bitmapData);
                    }

                    stDisplayInfo.hWnd = pictureBox.Handle;
                    stDisplayInfo.pData = stFrameInfo.pBufAddr;
                    stDisplayInfo.nDataLen = stFrameInfo.stFrameInfo.nFrameLen;
                    stDisplayInfo.nWidth = stFrameInfo.stFrameInfo.nWidth;
                    stDisplayInfo.nHeight = stFrameInfo.stFrameInfo.nHeight;
                    stDisplayInfo.enPixelType = stFrameInfo.stFrameInfo.enPixelType;
                    m_MyCamera.MV_CC_DisplayOneFrame_NET(ref stDisplayInfo);

                    m_MyCamera.MV_CC_FreeImageBuffer_NET(ref stFrameInfo);
                }
            }
        }
        /// <summary>
        /// 显示错误信息，来源：海康源码
        /// </summary>
        /// <param name="csMessage"></param>
        /// <param name="nErrorNum"></param>
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }

        /// <summary>
        /// 在窗体中显示设备列表
        /// </summary>
        private void DeviceListAcq()
        {
            // ch:创建设备列表 | en:Create Device List
            System.GC.Collect();
            cbDeviceList.Items.Clear();
            m_stDeviceList.nDeviceNum = 0;
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref m_stDeviceList);
            if (0 != nRet)
            {
                ShowErrorMsg("Enumerate devices fail!", 0);
                return;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < m_stDeviceList.nDeviceNum; i++)
            {
                MyCamera.MV_CC_DEVICE_INFO device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(m_stDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                string strUserDefinedName = "";
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)MyCamera.ByteToStruct(device.stSpecialInfo.stGigEInfo, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    if ((gigeInfo.chUserDefinedName.Length > 0) && (gigeInfo.chUserDefinedName[0] != '\0'))
                    {
                        if (MyCamera.IsTextUTF8(gigeInfo.chUserDefinedName))
                        {
                            strUserDefinedName = Encoding.UTF8.GetString(gigeInfo.chUserDefinedName).TrimEnd('\0');
                        }
                        else
                        {
                            strUserDefinedName = Encoding.Default.GetString(gigeInfo.chUserDefinedName).TrimEnd('\0');
                        }
                        cbDeviceList.Items.Add("GEV: " + strUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)MyCamera.ByteToStruct(device.stSpecialInfo.stUsb3VInfo, typeof(MyCamera.MV_USB3_DEVICE_INFO));

                    if ((usbInfo.chUserDefinedName.Length > 0) && (usbInfo.chUserDefinedName[0] != '\0'))
                    {
                        if (MyCamera.IsTextUTF8(usbInfo.chUserDefinedName))
                        {
                            strUserDefinedName = Encoding.UTF8.GetString(usbInfo.chUserDefinedName).TrimEnd('\0');
                        }
                        else
                        {
                            strUserDefinedName = Encoding.Default.GetString(usbInfo.chUserDefinedName).TrimEnd('\0');
                        }
                        cbDeviceList.Items.Add("U3V: " + strUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (m_stDeviceList.nDeviceNum != 0)
            {
                cbDeviceList.SelectedIndex = 0;
            }
        }
        #endregion

        #region 删除dgvRound表中的数据（未做）
        private void btnDeleteRound_Click(object sender, EventArgs e)
        {
            //if (dgvRound.SelectedRows.Count > 0)
            //{
            //    DataGridViewRow selectedRow = dgvRound.SelectedRows[0];
            //    // 获取选中行列名为 "countRoundID"的数据,并赋值给要删除的round
            //    int index = int.Parse(selectedRow.Cells["countRoundID"].Value.ToString());
            //    RoundService roundService = new RoundService();
            //    Round round = roundService.Select(index);
            //    int FK_round_cashCount = round.countRoundID;
            //    roundService.Delete(round);
            //    CashCountService cashCountService = new CashCountService();
            //}
        }
        #endregion
    }

}

