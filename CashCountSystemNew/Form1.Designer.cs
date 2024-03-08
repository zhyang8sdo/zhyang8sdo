namespace CashCountSystem
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvRound = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFinish = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBegin = new System.Windows.Forms.Button();
            this.dgvCount = new System.Windows.Forms.DataGridView();
            this.countNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countDoubleResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultJudge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDeleteCount = new System.Windows.Forms.Button();
            this.tbWorkerName = new System.Windows.Forms.Label();
            this.tbWorkerNumber = new System.Windows.Forms.Label();
            this.tbMachineID = new System.Windows.Forms.Label();
            this.tbMachineIP = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cashCountBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bnSaveJpg = new System.Windows.Forms.Button();
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.bnOpen = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.btnDeleteImage = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashCountBindingSource)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // dgvRound
            // 
            this.dgvRound.AllowUserToResizeColumns = false;
            this.dgvRound.AllowUserToResizeRows = false;
            this.dgvRound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            resources.ApplyResources(this.dgvRound, "dgvRound");
            this.dgvRound.MultiSelect = false;
            this.dgvRound.Name = "dgvRound";
            this.dgvRound.ReadOnly = true;
            this.dgvRound.RowTemplate.Height = 23;
            this.dgvRound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "countRoundID";
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "finalResult";
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "workerName";
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "machineID";
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "machineIP";
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "operatTime";
            dataGridViewCellStyle1.Format = "G";
            dataGridViewCellStyle1.NullValue = null;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // btnFinish
            // 
            resources.ApplyResources(this.btnFinish, "btnFinish");
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // btnBegin
            // 
            resources.ApplyResources(this.btnBegin, "btnBegin");
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // dgvCount
            // 
            this.dgvCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.countNumber,
            this.countID,
            this.countResult,
            this.countDoubleResult,
            this.resultJudge,
            this.operatTime});
            resources.ApplyResources(this.dgvCount, "dgvCount");
            this.dgvCount.MultiSelect = false;
            this.dgvCount.Name = "dgvCount";
            this.dgvCount.ReadOnly = true;
            this.dgvCount.RowTemplate.Height = 23;
            this.dgvCount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // countNumber
            // 
            resources.ApplyResources(this.countNumber, "countNumber");
            this.countNumber.Name = "countNumber";
            this.countNumber.ReadOnly = true;
            // 
            // countID
            // 
            this.countID.DataPropertyName = "countID";
            resources.ApplyResources(this.countID, "countID");
            this.countID.Name = "countID";
            this.countID.ReadOnly = true;
            // 
            // countResult
            // 
            this.countResult.DataPropertyName = "countResult";
            resources.ApplyResources(this.countResult, "countResult");
            this.countResult.Name = "countResult";
            this.countResult.ReadOnly = true;
            // 
            // countDoubleResult
            // 
            this.countDoubleResult.DataPropertyName = "countDoubleResult";
            resources.ApplyResources(this.countDoubleResult, "countDoubleResult");
            this.countDoubleResult.Name = "countDoubleResult";
            this.countDoubleResult.ReadOnly = true;
            // 
            // resultJudge
            // 
            this.resultJudge.DataPropertyName = "resultJudge";
            resources.ApplyResources(this.resultJudge, "resultJudge");
            this.resultJudge.Name = "resultJudge";
            this.resultJudge.ReadOnly = true;
            // 
            // operatTime
            // 
            this.operatTime.DataPropertyName = "operatTime";
            dataGridViewCellStyle2.Format = "G";
            dataGridViewCellStyle2.NullValue = null;
            this.operatTime.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.operatTime, "operatTime");
            this.operatTime.Name = "operatTime";
            this.operatTime.ReadOnly = true;
            // 
            // btnDeleteCount
            // 
            resources.ApplyResources(this.btnDeleteCount, "btnDeleteCount");
            this.btnDeleteCount.Name = "btnDeleteCount";
            this.btnDeleteCount.UseVisualStyleBackColor = true;
            this.btnDeleteCount.Click += new System.EventHandler(this.btnDeleteCount_Click);
            // 
            // tbWorkerName
            // 
            resources.ApplyResources(this.tbWorkerName, "tbWorkerName");
            this.tbWorkerName.Name = "tbWorkerName";
            // 
            // tbWorkerNumber
            // 
            resources.ApplyResources(this.tbWorkerNumber, "tbWorkerNumber");
            this.tbWorkerNumber.Name = "tbWorkerNumber";
            // 
            // tbMachineID
            // 
            resources.ApplyResources(this.tbMachineID, "tbMachineID");
            this.tbMachineID.Name = "tbMachineID";
            // 
            // tbMachineIP
            // 
            resources.ApplyResources(this.tbMachineIP, "tbMachineIP");
            this.tbMachineIP.Name = "tbMachineIP";
            // 
            // labelCount
            // 
            resources.ApplyResources(this.labelCount, "labelCount");
            this.labelCount.Name = "labelCount";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cashCountBindingSource
            // 
            this.cashCountBindingSource.DataSource = typeof(Modle.CashCount);
            // 
            // bnSaveJpg
            // 
            resources.ApplyResources(this.bnSaveJpg, "bnSaveJpg");
            this.bnSaveJpg.Name = "bnSaveJpg";
            this.bnSaveJpg.UseVisualStyleBackColor = true;
            this.bnSaveJpg.Click += new System.EventHandler(this.bnSaveJpg_Click);
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            resources.ApplyResources(this.cbDeviceList, "cbDeviceList");
            this.cbDeviceList.Name = "cbDeviceList";
            // 
            // bnOpen
            // 
            resources.ApplyResources(this.bnOpen, "bnOpen");
            this.bnOpen.Name = "bnOpen";
            this.bnOpen.UseVisualStyleBackColor = true;
            this.bnOpen.Click += new System.EventHandler(this.bnOpen_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // btnDeleteImage
            // 
            resources.ApplyResources(this.btnDeleteImage, "btnDeleteImage");
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.UseVisualStyleBackColor = true;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteImage);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.cbDeviceList);
            this.Controls.Add(this.bnOpen);
            this.Controls.Add(this.bnSaveJpg);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.tbMachineIP);
            this.Controls.Add(this.tbMachineID);
            this.Controls.Add(this.tbWorkerNumber);
            this.Controls.Add(this.tbWorkerName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnDeleteCount);
            this.Controls.Add(this.dgvCount);
            this.Controls.Add(this.dgvRound);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBegin);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cashCountBindingSource)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvRound;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.BindingSource cashCountBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.DataGridView dgvCount;
        private System.Windows.Forms.Button btnDeleteCount;
        private System.Windows.Forms.Label tbWorkerName;
        private System.Windows.Forms.Label tbWorkerNumber;
        private System.Windows.Forms.Label tbMachineID;
        private System.Windows.Forms.Label tbMachineIP;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button bnSaveJpg;
        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn countNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn countID;
        private System.Windows.Forms.DataGridViewTextBoxColumn countResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn countDoubleResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultJudge;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatTime;
        private System.Windows.Forms.Button btnDeleteImage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
    }
}

