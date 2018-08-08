namespace MyXMPPClient
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPsw = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbSysUser = new System.Windows.Forms.TextBox();
            this.btSysLogin = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tbToSysUser = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.tbGroup = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 48);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbMsg
            // 
            this.tbMsg.Location = new System.Drawing.Point(17, 42);
            this.tbMsg.Margin = new System.Windows.Forms.Padding(2);
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(291, 21);
            this.tbMsg.TabIndex = 1;
            this.tbMsg.Text = "1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(131, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbLog
            // 
            this.tbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLog.Location = new System.Drawing.Point(0, 267);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(682, 258);
            this.tbLog.TabIndex = 0;
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(17, 16);
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(100, 21);
            this.tbTo.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(562, 28);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(8, 22);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 21);
            this.tbUser.TabIndex = 5;
            // 
            // tbPsw
            // 
            this.tbPsw.Location = new System.Drawing.Point(114, 22);
            this.tbPsw.Name = "tbPsw";
            this.tbPsw.Size = new System.Drawing.Size(100, 21);
            this.tbPsw.TabIndex = 6;
            this.tbPsw.Text = "1";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(139, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "reg";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 267);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.btSysLogin);
            this.tabPage1.Controls.Add(this.tbSysUser);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.tbPsw);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.tbUser);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(674, 241);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "登录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 76);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "logout";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.tbGroup);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.tbToSysUser);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.tbTo);
            this.tabPage2.Controls.Add(this.tbMsg);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 241);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "聊天";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbSysUser
            // 
            this.tbSysUser.Location = new System.Drawing.Point(453, 22);
            this.tbSysUser.Name = "tbSysUser";
            this.tbSysUser.Size = new System.Drawing.Size(100, 21);
            this.tbSysUser.TabIndex = 9;
            // 
            // btSysLogin
            // 
            this.btSysLogin.Location = new System.Drawing.Point(453, 76);
            this.btSysLogin.Name = "btSysLogin";
            this.btSysLogin.Size = new System.Drawing.Size(75, 23);
            this.btSysLogin.TabIndex = 11;
            this.btSysLogin.Text = "SysLogin";
            this.btSysLogin.UseVisualStyleBackColor = true;
            this.btSysLogin.Click += new System.EventHandler(this.btSysLogin_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(453, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 12;
            // 
            // tbToSysUser
            // 
            this.tbToSysUser.Location = new System.Drawing.Point(17, 68);
            this.tbToSysUser.Name = "tbToSysUser";
            this.tbToSysUser.Size = new System.Drawing.Size(100, 21);
            this.tbToSysUser.TabIndex = 5;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(131, 68);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "SysSend";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // tbGroup
            // 
            this.tbGroup.Location = new System.Drawing.Point(313, 16);
            this.tbGroup.Multiline = true;
            this.tbGroup.Name = "tbGroup";
            this.tbGroup.Size = new System.Drawing.Size(100, 209);
            this.tbGroup.TabIndex = 7;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(419, 16);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 8;
            this.button7.Text = "SysGroupSend";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 525);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbPsw;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btSysLogin;
        private System.Windows.Forms.TextBox tbSysUser;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox tbToSysUser;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox tbGroup;
    }
}

