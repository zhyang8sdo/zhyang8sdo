namespace CashCountSystem
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.tbWorkerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboxCount = new System.Windows.Forms.ComboBox();
            this.tbMachineID = new System.Windows.Forms.TextBox();
            this.tbMachineIP = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(43, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "操作人";
            // 
            // tbWorkerName
            // 
            this.tbWorkerName.Location = new System.Drawing.Point(172, 57);
            this.tbWorkerName.Multiline = true;
            this.tbWorkerName.Name = "tbWorkerName";
            this.tbWorkerName.Size = new System.Drawing.Size(107, 20);
            this.tbWorkerName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(43, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "机器码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(43, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "ip地址";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(7, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 24);
            this.label5.TabIndex = 9;
            this.label5.Text = "每轮计数次数";
            // 
            // comboxCount
            // 
            this.comboxCount.FormattingEnabled = true;
            this.comboxCount.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "100"});
            this.comboxCount.Location = new System.Drawing.Point(172, 260);
            this.comboxCount.Name = "comboxCount";
            this.comboxCount.Size = new System.Drawing.Size(107, 20);
            this.comboxCount.TabIndex = 10;
            // 
            // tbMachineID
            // 
            this.tbMachineID.Location = new System.Drawing.Point(172, 119);
            this.tbMachineID.Multiline = true;
            this.tbMachineID.Name = "tbMachineID";
            this.tbMachineID.ReadOnly = true;
            this.tbMachineID.Size = new System.Drawing.Size(107, 20);
            this.tbMachineID.TabIndex = 11;
            this.tbMachineID.Text = "1";
            // 
            // tbMachineIP
            // 
            this.tbMachineIP.Location = new System.Drawing.Point(172, 190);
            this.tbMachineIP.Multiline = true;
            this.tbMachineIP.Name = "tbMachineIP";
            this.tbMachineIP.Size = new System.Drawing.Size(107, 20);
            this.tbMachineIP.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(47, 342);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 35);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(172, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 35);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 427);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbMachineIP);
            this.Controls.Add(this.tbMachineID);
            this.Controls.Add(this.comboxCount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbWorkerName);
            this.Controls.Add(this.label3);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLogin";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbWorkerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboxCount;
        private System.Windows.Forms.TextBox tbMachineID;
        private System.Windows.Forms.TextBox tbMachineIP;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}