namespace EMR.Controls
{
    partial class UcDataGridView
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Group = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Medicine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Insert = new System.Windows.Forms.DataGridViewLinkColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Group,
            this.Medicine,
            this.Dosage,
            this.Remove,
            this.Insert});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(402, 213);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Group
            // 
            this.Group.DataPropertyName = "Group";
            this.Group.HeaderText = "分组";
            this.Group.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.Group.Name = "Group";
            this.Group.Width = 50;
            // 
            // Medicine
            // 
            this.Medicine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Medicine.DataPropertyName = "Medicine";
            this.Medicine.HeaderText = "名称";
            this.Medicine.Name = "Medicine";
            // 
            // Dosage
            // 
            this.Dosage.DataPropertyName = "Dosage";
            dataGridViewCellStyle1.NullValue = null;
            this.Dosage.DefaultCellStyle = dataGridViewCellStyle1;
            this.Dosage.HeaderText = "量";
            this.Dosage.Name = "Dosage";
            this.Dosage.Width = 109;
            // 
            // Remove
            // 
            this.Remove.DataPropertyName = "Remove";
            this.Remove.HeaderText = "删除";
            this.Remove.Name = "Remove";
            this.Remove.Width = 50;
            // 
            // Insert
            // 
            this.Insert.DataPropertyName = "Insert";
            this.Insert.HeaderText = "插入";
            this.Insert.Name = "Insert";
            this.Insert.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem1.Text = "增加一行";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // UcDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "UcDataGridView";
            this.Size = new System.Drawing.Size(402, 213);
            this.Load += new System.EventHandler(this.UcDataGridView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewComboBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medicine;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dosage;
        private System.Windows.Forms.DataGridViewLinkColumn Remove;
        private System.Windows.Forms.DataGridViewLinkColumn Insert;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
