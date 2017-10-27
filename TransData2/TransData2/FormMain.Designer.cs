namespace TransData2
{
    partial class FormMain
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
            this.lstJXC = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstTran = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.edTime = new System.Windows.Forms.NumericUpDown();
            this.cbAuto = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.edTime)).BeginInit();
            this.SuspendLayout();
            // 
            // lstJXC
            // 
            this.lstJXC.CheckOnClick = true;
            this.lstJXC.FormattingEnabled = true;
            this.lstJXC.Location = new System.Drawing.Point(12, 42);
            this.lstJXC.Name = "lstJXC";
            this.lstJXC.Size = new System.Drawing.Size(120, 84);
            this.lstJXC.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "业务库列表";
            // 
            // lstTran
            // 
            this.lstTran.CheckOnClick = true;
            this.lstTran.FormattingEnabled = true;
            this.lstTran.Location = new System.Drawing.Point(153, 42);
            this.lstTran.MultiColumn = true;
            this.lstTran.Name = "lstTran";
            this.lstTran.Size = new System.Drawing.Size(318, 84);
            this.lstTran.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "传输内容";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "传输";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(589, 103);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // edTime
            // 
            this.edTime.Location = new System.Drawing.Point(589, 39);
            this.edTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.edTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edTime.Name = "edTime";
            this.edTime.Size = new System.Drawing.Size(40, 21);
            this.edTime.TabIndex = 7;
            this.edTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edTime.ValueChanged += new System.EventHandler(this.edTime_ValueChanged);
            // 
            // cbAuto
            // 
            this.cbAuto.AutoSize = true;
            this.cbAuto.Location = new System.Drawing.Point(496, 42);
            this.cbAuto.Name = "cbAuto";
            this.cbAuto.Size = new System.Drawing.Size(72, 16);
            this.cbAuto.TabIndex = 8;
            this.cbAuto.Text = "自动传输";
            this.cbAuto.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(635, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "分钟";
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbLog.Font = new System.Drawing.Font("宋体", 10F);
            this.tbLog.Location = new System.Drawing.Point(0, 143);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLog.Size = new System.Drawing.Size(720, 390);
            this.tbLog.TabIndex = 11;
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(493, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "清空日志";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 533);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbAuto);
            this.Controls.Add(this.edTime);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstTran);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstJXC);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.edTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lstJXC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox lstTran;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown edTime;
        private System.Windows.Forms.CheckBox cbAuto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
    }
}

