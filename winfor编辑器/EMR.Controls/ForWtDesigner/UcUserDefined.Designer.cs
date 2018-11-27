namespace EMR.Controls
{
    partial class UcUserDefined
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit1.Location = new System.Drawing.Point(145, 4);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(133, 24);
            this.textEdit1.TabIndex = 1;
            this.textEdit1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textEdit1_MouseUp);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = "自定义项：";
            this.comboBoxEdit1.Location = new System.Drawing.Point(4, 4);
            this.comboBoxEdit1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.comboBoxEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.comboBoxEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(133, 22);
            this.comboBoxEdit1.TabIndex = 0;
            // 
            // UcUserDefined
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.comboBoxEdit1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "UcUserDefined";
            this.Size = new System.Drawing.Size(287, 39);
            this.Load += new System.EventHandler(this.UcUserDefined_Load);
            this.SizeChanged += new System.EventHandler(this.UcUserDefined_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;

    }
}
