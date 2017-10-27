namespace CreateAccess
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
            this.button1 = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbHYK = new System.Windows.Forms.TextBox();
            this.tbHYXX = new System.Windows.Forms.TextBox();
            this.tbGKDA = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbLog
            // 
            this.tbLog.Location = new System.Drawing.Point(12, 122);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(676, 209);
            this.tbLog.TabIndex = 3;
            // 
            // tbHYK
            // 
            this.tbHYK.Location = new System.Drawing.Point(12, 12);
            this.tbHYK.Name = "tbHYK";
            this.tbHYK.Size = new System.Drawing.Size(676, 21);
            this.tbHYK.TabIndex = 4;
            this.tbHYK.Text = "select * from HYKDEF D where D.BJ_JF=1";
            // 
            // tbHYXX
            // 
            this.tbHYXX.Location = new System.Drawing.Point(12, 39);
            this.tbHYXX.Name = "tbHYXX";
            this.tbHYXX.Size = new System.Drawing.Size(676, 21);
            this.tbHYXX.TabIndex = 5;
            this.tbHYXX.Text = "select * from HYK_HYXX H,HYKDEF D where H.HYKTYPE=D.HYKTYPE and D.BJ_JF=1";
            // 
            // tbGKDA
            // 
            this.tbGKDA.Location = new System.Drawing.Point(12, 66);
            this.tbGKDA.Name = "tbGKDA";
            this.tbGKDA.Size = new System.Drawing.Size(676, 21);
            this.tbGKDA.TabIndex = 6;
            this.tbGKDA.Text = "select * from HYK_HYXX H,HYKDEF D,HYK_GKDA G where H.HYKTYPE=D.HYKTYPE and D.BJ_J" +
    "F=1 and H.GKID=G.GKID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 341);
            this.Controls.Add(this.tbGKDA);
            this.Controls.Add(this.tbHYXX);
            this.Controls.Add(this.tbHYK);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TextBox tbHYK;
        private System.Windows.Forms.TextBox tbHYXX;
        private System.Windows.Forms.TextBox tbGKDA;
    }
}

