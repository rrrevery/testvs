namespace TestPost
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
            this.tbReq = new System.Windows.Forms.TextBox();
            this.tbResp = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.tbTradeID = new System.Windows.Forms.TextBox();
            this.tbAccountID = new System.Windows.Forms.TextBox();
            this.tbPay = new System.Windows.Forms.TextBox();
            this.tbJLBH = new System.Windows.Forms.TextBox();
            this.tbFree = new System.Windows.Forms.TextBox();
            this.tbCPH = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbUsr = new System.Windows.Forms.TextBox();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(724, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "查询停车";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbReq
            // 
            this.tbReq.Location = new System.Drawing.Point(12, 68);
            this.tbReq.Multiline = true;
            this.tbReq.Name = "tbReq";
            this.tbReq.Size = new System.Drawing.Size(522, 167);
            this.tbReq.TabIndex = 1;
            // 
            // tbResp
            // 
            this.tbResp.Location = new System.Drawing.Point(12, 312);
            this.tbResp.Multiline = true;
            this.tbResp.Name = "tbResp";
            this.tbResp.Size = new System.Drawing.Size(522, 321);
            this.tbResp.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(540, 120);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "停车支付";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbTradeID
            // 
            this.tbTradeID.Location = new System.Drawing.Point(540, 39);
            this.tbTradeID.Name = "tbTradeID";
            this.tbTradeID.Size = new System.Drawing.Size(259, 21);
            this.tbTradeID.TabIndex = 4;
            // 
            // tbAccountID
            // 
            this.tbAccountID.Location = new System.Drawing.Point(540, 66);
            this.tbAccountID.Name = "tbAccountID";
            this.tbAccountID.Size = new System.Drawing.Size(259, 21);
            this.tbAccountID.TabIndex = 5;
            // 
            // tbPay
            // 
            this.tbPay.Location = new System.Drawing.Point(540, 93);
            this.tbPay.Name = "tbPay";
            this.tbPay.Size = new System.Drawing.Size(100, 21);
            this.tbPay.TabIndex = 6;
            this.tbPay.Text = "0.01";
            // 
            // tbJLBH
            // 
            this.tbJLBH.Location = new System.Drawing.Point(646, 93);
            this.tbJLBH.Name = "tbJLBH";
            this.tbJLBH.Size = new System.Drawing.Size(100, 21);
            this.tbJLBH.TabIndex = 7;
            this.tbJLBH.Text = "1";
            // 
            // tbFree
            // 
            this.tbFree.Location = new System.Drawing.Point(646, 12);
            this.tbFree.Name = "tbFree";
            this.tbFree.Size = new System.Drawing.Size(61, 21);
            this.tbFree.TabIndex = 8;
            this.tbFree.Text = "0";
            // 
            // tbCPH
            // 
            this.tbCPH.Location = new System.Drawing.Point(540, 12);
            this.tbCPH.Name = "tbCPH";
            this.tbCPH.Size = new System.Drawing.Size(100, 21);
            this.tbCPH.TabIndex = 9;
            this.tbCPH.Text = "京CW0001";
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(12, 12);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(522, 21);
            this.tbURL.TabIndex = 10;
            this.tbURL.Text = "http://open.tingjiandan.com/openapi/gateway ";
            // 
            // tbUsr
            // 
            this.tbUsr.Location = new System.Drawing.Point(12, 39);
            this.tbUsr.Name = "tbUsr";
            this.tbUsr.Size = new System.Drawing.Size(241, 21);
            this.tbUsr.TabIndex = 11;
            this.tbUsr.Text = "068c8349dbcb4d879aa45a4cfc7ee66d";
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(271, 39);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(263, 21);
            this.tbKey.TabIndex = 12;
            this.tbKey.Text = "55c64ac089034aa0a4513fa7e9333379";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 645);
            this.Controls.Add(this.tbKey);
            this.Controls.Add(this.tbUsr);
            this.Controls.Add(this.tbURL);
            this.Controls.Add(this.tbCPH);
            this.Controls.Add(this.tbFree);
            this.Controls.Add(this.tbJLBH);
            this.Controls.Add(this.tbPay);
            this.Controls.Add(this.tbAccountID);
            this.Controls.Add(this.tbTradeID);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tbResp);
            this.Controls.Add(this.tbReq);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbReq;
        private System.Windows.Forms.TextBox tbResp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbTradeID;
        private System.Windows.Forms.TextBox tbAccountID;
        private System.Windows.Forms.TextBox tbPay;
        private System.Windows.Forms.TextBox tbJLBH;
        private System.Windows.Forms.TextBox tbFree;
        private System.Windows.Forms.TextBox tbCPH;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbUsr;
        private System.Windows.Forms.TextBox tbKey;
    }
}

