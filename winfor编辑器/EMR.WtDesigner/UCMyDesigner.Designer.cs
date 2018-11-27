namespace EMR.WinformDesigner
{
    partial class UCMyDesigner
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.布局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.左对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.右对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.顶对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中心对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.底对齐ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.myToolBox = new EMR.WinformDesigner.Toolbox();
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.panelContainer1.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dockPanel3.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(200, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(490, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.打开ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.fileToolStripMenuItem.Text = "文件";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.全选ToolStripMenuItem,
            this.布局ToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.editToolStripMenuItem.Text = "编辑";
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // 布局ToolStripMenuItem
            // 
            this.布局ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.左对齐ToolStripMenuItem,
            this.右对齐ToolStripMenuItem,
            this.顶对齐ToolStripMenuItem,
            this.中心对齐ToolStripMenuItem,
            this.底对齐ToolStripMenuItem});
            this.布局ToolStripMenuItem.Name = "布局ToolStripMenuItem";
            this.布局ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.布局ToolStripMenuItem.Text = "布局";
            // 
            // 左对齐ToolStripMenuItem
            // 
            this.左对齐ToolStripMenuItem.Name = "左对齐ToolStripMenuItem";
            this.左对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.左对齐ToolStripMenuItem.Text = "左对齐";
            this.左对齐ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // 右对齐ToolStripMenuItem
            // 
            this.右对齐ToolStripMenuItem.Name = "右对齐ToolStripMenuItem";
            this.右对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.右对齐ToolStripMenuItem.Text = "右对齐";
            this.右对齐ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // 顶对齐ToolStripMenuItem
            // 
            this.顶对齐ToolStripMenuItem.Name = "顶对齐ToolStripMenuItem";
            this.顶对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.顶对齐ToolStripMenuItem.Text = "顶对齐";
            this.顶对齐ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // 中心对齐ToolStripMenuItem
            // 
            this.中心对齐ToolStripMenuItem.Name = "中心对齐ToolStripMenuItem";
            this.中心对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.中心对齐ToolStripMenuItem.Text = "中心对齐";
            this.中心对齐ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // 底对齐ToolStripMenuItem
            // 
            this.底对齐ToolStripMenuItem.Name = "底对齐ToolStripMenuItem";
            this.底对齐ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.底对齐ToolStripMenuItem.Text = "底对齐";
            this.底对齐ToolStripMenuItem.Click += new System.EventHandler(this.ActionClick);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.panelContainer1,
            this.dockPanel2});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Controls.Add(this.dockPanel3);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.panelContainer1.ID = new System.Guid("69ad5bde-18ce-4560-a878-35beb2953b02");
            this.panelContainer1.Location = new System.Drawing.Point(0, 0);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(200, 200);
            this.panelContainer1.Size = new System.Drawing.Size(200, 484);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.ID = new System.Guid("63ec6eb4-083b-487f-abda-502313184a64");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 242);
            this.dockPanel1.Text = "工具箱";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.myToolBox);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(192, 215);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // myToolBox
            // 
            this.myToolBox.DesignerHost = null;
            this.myToolBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myToolBox.FilePath = null;
            this.myToolBox.Location = new System.Drawing.Point(0, 0);
            this.myToolBox.Name = "myToolBox";
            this.myToolBox.SelectedCategory = null;
            this.myToolBox.Size = new System.Drawing.Size(192, 215);
            this.myToolBox.TabIndex = 0;
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel3.ID = new System.Guid("ad88bf74-ba7b-4f46-bb7f-eb5c7f540f6c");
            this.dockPanel3.Location = new System.Drawing.Point(0, 242);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel3.Size = new System.Drawing.Size(200, 242);
            this.dockPanel3.Text = "模板列表";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.gridControl1);
            this.dockPanel3_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(192, 215);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(192, 215);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "模板类别";
            this.gridColumn1.FieldName = "PAGE_TYPE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "创建人";
            this.gridColumn2.FieldName = "PAGE_CREATER";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "模板编号";
            this.gridColumn3.FieldName = "PAGE_NO";
            this.gridColumn3.Name = "gridColumn3";
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel2.ID = new System.Guid("f822d617-088d-4330-819f-8c0847e1b49e");
            this.dockPanel2.Location = new System.Drawing.Point(690, 0);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel2.Size = new System.Drawing.Size(200, 484);
            this.dockPanel2.Text = "属性";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.propertyGrid1);
            this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(192, 457);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(192, 457);
            this.propertyGrid1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(200, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 459);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // UCMyDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dockPanel2);
            this.Controls.Add(this.panelContainer1);
            this.Name = "UCMyDesigner";
            this.Size = new System.Drawing.Size(890, 484);
            this.Load += new System.EventHandler(this.UCMyDesigner_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel3.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 布局ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 左对齐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 右对齐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 顶对齐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中心对齐ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 底对齐ToolStripMenuItem;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private Toolbox myToolBox;
        private System.Windows.Forms.TabControl tabControl1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
    }
}
