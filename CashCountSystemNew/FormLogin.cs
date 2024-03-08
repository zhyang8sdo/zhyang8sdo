using BLL;
using Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashCountSystem
{

    public partial class FormLogin : Form
    {
        Machine machine = new Machine();
        Worker worker = new Worker();
        MachineService machineService = new MachineService();
        WorkerService workerService = new WorkerService();
        public FormLogin()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            machine.machineID = int.Parse(tbMachineID.Text);
            machine = machineService.Select(machine.machineID);
            tbMachineIP.Text = machine.machineIP;
            comboxCount.Text = machine.machineCountNumber.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //查询操作人
            worker.workerName = tbWorkerName.Text;
            if (workerService.SelectName(worker.workerName) != null)
            {
                worker = workerService.SelectName(worker.workerName);
                //更新机器表数据
                machine.machineIP = tbMachineIP.Text;
                machine.machineCountNumber = int.Parse(comboxCount.Text);
                machineService.Update(machine);
                Form1 form1 = new Form1(machine, worker);
                this.Hide();
                form1.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("该用户尚未注册", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //系统主界面
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
