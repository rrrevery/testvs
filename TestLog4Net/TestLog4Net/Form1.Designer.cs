namespace TestLog4Net
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
            this.btnDEBUG = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnWARN = new System.Windows.Forms.Button();
            this.btnERROR = new System.Windows.Forms.Button();
            this.btnFATAL = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDEBUG
            // 
            this.btnDEBUG.Location = new System.Drawing.Point(12, 12);
            this.btnDEBUG.Name = "btnDEBUG";
            this.btnDEBUG.Size = new System.Drawing.Size(75, 23);
            this.btnDEBUG.TabIndex = 0;
            this.btnDEBUG.Text = "DEBUG";
            this.btnDEBUG.UseVisualStyleBackColor = true;
            this.btnDEBUG.Click += new System.EventHandler(this.btnDEBUG_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(12, 41);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(75, 23);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Text = "INFO";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // btnWARN
            // 
            this.btnWARN.Location = new System.Drawing.Point(12, 70);
            this.btnWARN.Name = "btnWARN";
            this.btnWARN.Size = new System.Drawing.Size(75, 23);
            this.btnWARN.TabIndex = 2;
            this.btnWARN.Text = "WARN";
            this.btnWARN.UseVisualStyleBackColor = true;
            this.btnWARN.Click += new System.EventHandler(this.btnWARN_Click);
            // 
            // btnERROR
            // 
            this.btnERROR.Location = new System.Drawing.Point(12, 99);
            this.btnERROR.Name = "btnERROR";
            this.btnERROR.Size = new System.Drawing.Size(75, 23);
            this.btnERROR.TabIndex = 3;
            this.btnERROR.Text = "ERROR";
            this.btnERROR.UseVisualStyleBackColor = true;
            this.btnERROR.Click += new System.EventHandler(this.btnERROR_Click);
            // 
            // btnFATAL
            // 
            this.btnFATAL.Location = new System.Drawing.Point(12, 128);
            this.btnFATAL.Name = "btnFATAL";
            this.btnFATAL.Size = new System.Drawing.Size(75, 23);
            this.btnFATAL.TabIndex = 4;
            this.btnFATAL.Text = "FATAL";
            this.btnFATAL.UseVisualStyleBackColor = true;
            this.btnFATAL.Click += new System.EventHandler(this.btnFATAL_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "SQL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnFATAL);
            this.Controls.Add(this.btnERROR);
            this.Controls.Add(this.btnWARN);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnDEBUG);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDEBUG;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnWARN;
        private System.Windows.Forms.Button btnERROR;
        private System.Windows.Forms.Button btnFATAL;
        private System.Windows.Forms.Button button1;
    }
}

