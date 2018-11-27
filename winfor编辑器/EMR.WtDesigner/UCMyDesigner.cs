using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;

namespace EMR.WinformDesigner
{
    public partial class UCMyDesigner : UserControl
    {
        #region 全局变量
        private HostSurfaceManager _hostSurfaceManager = null;
        private KeystrokeMessageFilter filter;
        private DataTable dtFormTempletList = new DataTable();
        private int nCurrentPageNo = -1;
        /// <summary>
        /// 护理记录Model
        /// </summary>
        //private EMR.Model.EmrWinformTemplet modWinformTemplet = null;
        /// <summary>
        /// 护理记录模版与带你模版对照Model
        /// </summary>
        //private EMR.Model.EmrNurseCatlogDict modNursCatalogDic = null;
        #endregion

        public UCMyDesigner()
        {
            InitializeComponent();
            CustomInitialize();
        }

        private void UCMyDesigner_Load(object sender, EventArgs e)
        {
            SetInitialData();
        }

        public void SetInitialData()
        {
            GetFormTempletList();

        }

        /// <summary>
        /// Adds custom services to the HostManager like TGoolbox, PropertyGrid, 
        /// SolutionExplorer.
        /// OutputWindow is added as a service. It is used by the HostSurfaceManager
        /// to write out to the OutputWindow. You can add any services
        /// you want.
        /// </summary>
        private void CustomInitialize()
        {
            _hostSurfaceManager = new HostSurfaceManager();
            _hostSurfaceManager.AddService(typeof(IToolboxService), this.myToolBox);
            _hostSurfaceManager.AddService(typeof(System.Windows.Forms.PropertyGrid), this.propertyGrid1);
        }

        private void ActionClick(object sender, EventArgs e)
        {
            PerformAction(sender.ToString());
        }

        #region 方法 AddTabForNewHost(string tabText, HostControl hc)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabText"></param>
        /// <param name="hc"></param>
        private void AddTabForNewHost(string tabText, HostControl hc)
        {
            this.myToolBox.DesignerHost = hc.DesignerHost;
            TabPage tabpage = new TabPage(tabText);
            hc.Parent = tabpage;
            hc.Dock = DockStyle.Fill;
            this.tabControl1.TabPages.Add(tabpage);
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
            _hostSurfaceManager.ActiveDesignSurface = hc.HostSurface;
            //添加控件有键盘方向键移动 孙奎松 2014年6月10日18:12:50
            filter = new KeystrokeMessageFilter(hc.DesignerHost);
            Application.AddMessageFilter(filter);

        }
        private HostControl CurrentDocumentsHostControl
        {
            get
            {
                if (tabControl1.TabPages.Count > 0)
                {
                    return (HostControl)this.tabControl1.TabPages[tabControl1.SelectedIndex].Controls[0];
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// Perform all the Edit menu options using the MenuCommandService
        /// </summary>
        private void PerformAction(string text)
        {

            if (this.CurrentDocumentsHostControl == null)
                return;

            IMenuCommandService ims = this.CurrentDocumentsHostControl.HostSurface.GetService(typeof(IMenuCommandService)) as IMenuCommandService;

            try
            {
                switch (text)
                {
                    case "&Cut":
                        ims.GlobalInvoke(StandardCommands.Cut);
                        break;
                    case "C&opy":
                        ims.GlobalInvoke(StandardCommands.Copy);
                        break;
                    case "&Paste":
                        ims.GlobalInvoke(StandardCommands.Paste);
                        break;
                    case "&Undo":
                        ims.GlobalInvoke(StandardCommands.Undo);
                        break;
                    case "&Redo":
                        ims.GlobalInvoke(StandardCommands.Redo);
                        break;
                    case "删除":
                        ims.GlobalInvoke(StandardCommands.Delete);
                        break;
                    case "全选":
                        ims.GlobalInvoke(StandardCommands.SelectAll);
                        break;
                    case "左对齐":
                        ims.GlobalInvoke(StandardCommands.AlignLeft);
                        break;
                    case "右对齐":
                        ims.GlobalInvoke(StandardCommands.AlignRight);
                        break;
                    case "顶对齐":
                        ims.GlobalInvoke(StandardCommands.AlignTop);
                        break;
                    case "中心对齐":
                        ims.GlobalInvoke(StandardCommands.AlignVerticalCenters);
                        break;
                    case "底对齐":
                        ims.GlobalInvoke(StandardCommands.AlignBottom);
                        break;
                    default:
                        break;
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取所有的窗体模板
        /// </summary>
        private void GetFormTempletList()
        {
            //string strSQL = "select * from emr_winform_templet ORDER BY PAGE_SHOW_ORDER DESC";
            //DataSet dsFormList = new DataSet();
            //using (Session emrSession = SessionManager.CreateByConnName(LoginDictCache.emrConnectStr))
            //{
            //    dsFormList = emrSession.FindDataTable<EMR.Model.EmrWinformTemplet>();
            //}

            //if (dsFormList != null)
            //{
            //    if (dsFormList.Tables.Count > 0)
            //    {
            //        dtFormTempletList = dsFormList.Tables[0];
            //        gridControl1.DataSource = dtFormTempletList.DefaultView;
            //    }
            //}
        }
        #endregion

        #region 保存按钮单击事件
        /// <summary>
        /// 保存按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentDocumentsHostControl == null)
            {
                return;
            }

            HostControl currentHostControl = CurrentDocumentsHostControl;
            string strFormContent = ((BasicHostLoader)currentHostControl.HostSurface.Loader).SaveToDataBase(false);
            //System.IO.File.WriteAllText("temp.xml", strFormContent);

            byte[] b = Encoding.Default.GetBytes(strFormContent);
            SaveFileDialog fileDialog = new SaveFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(fileDialog.FileName, b);
                MessageBox.Show("保存成功！");
            }
            //string strSQL = string.Empty;
            //if (tabControl1.SelectedTab.Text == "新建窗体")
            //{
            //    if (!AddNewTemplet(b))
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    if (MessageBox.Show("是否保留原来的模板？", "提醒", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        //string s = tabControl1.SelectedTab.Name;
            //        //DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //        //using (Session emrSession = SessionManager.CreateByConnName(LoginDictCache.emrConnectStr))
            //        //{
            //        //    EMR.Model.EmrWinformTemplet emraaa = emrSession.FindSingleModel<EMR.Model.EmrWinformTemplet>(string.Format("Page_No = '{0}'", dr["Page_No"].ToString().Trim()));
            //        //    emraaa.PAGE_CONTENT = b;
            //        //    emrSession.Delete<EMR.Model.EmrWinformTemplet>(string.Format(@"Page_No = '{0}'", dr["Page_No"].ToString().Trim()));
            //        //    emrSession.Save<EMR.Model.EmrWinformTemplet>(emraaa);
            //        //}
            //    }
            //    else
            //    {
            //        AddNewTemplet(b);
            //    }
            //}
            //GetFormTempletList();
            //SetFocus(tabControl1.SelectedTab.Text, gridControl1, "PAGE_TYPE");
        }
        #endregion

        #region 向数据库中插入新的模版
        /// <summary>
        /// 向数据库中插入指定PageNo的模版
        /// </summary>
        /// <param name="b">模版的二进制数据格式</param>
        /// <returns>如果添加则返回ture，如果添加则返回false</returns>
        private bool AddNewTemplet(byte[] b)
        {
            RHFrmNewFormType frm = new RHFrmNewFormType();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes("D:\\temp.txt", b);
                //EMR.Model.EmrWinformTemplet modWinformTemplet = new Model.EmrWinformTemplet();

                //using (Session emrSession = SessionManager.CreateByConnName(LoginDictCache.emrConnectStr))
                //{
                //    int nMaxNo = 1;
                //    string strMaxNoSql = "select max(PAGE_NO) from EMR_WINFORM_TEMPLET";
                //    string strMaxNo = Convert.ToString(emrSession.FindObjectBySql(strMaxNoSql));

                //    if (!string.IsNullOrEmpty(strMaxNo))
                //    {
                //        nMaxNo = Convert.ToInt32(strMaxNo) + 1;
                //    }


                //    modWinformTemplet.PAGE_NO = nMaxNo;
                //    modWinformTemplet.PAGE_TYPE = frm.strFrmType;
                //    modWinformTemplet.PAGE_CONTENT = b;
                //    modWinformTemplet.PAGE_CREATER = LoginDictCache.LoginUserID;
                //    modWinformTemplet.PAGE_CREATE_DATE = DateTime.Now;
                //    modWinformTemplet.PAGE_IS_USER = "0";
                //    modWinformTemplet.PAGE_SHOW_ORDER = nMaxNo;
                //    emrSession.Save<EMR.Model.EmrWinformTemplet>(modWinformTemplet);

                //    tabControl1.SelectedTab.Name = modWinformTemplet.PAGE_NO.ToString();
                //    tabControl1.SelectedTab.Text = modWinformTemplet.PAGE_TYPE;
                //}

                //AddNewCatalogDic(modWinformTemplet);    //增加“护理记录模版与护理记录打印对照”数据

                return true;
            }
            else
            {
                return false;
            }
        }

        ///// <summary>
        ///// 增加“护理记录模版与护理记录打印对照”数据
        ///// </summary>
        //private void AddNewCatalogDic(EMR.Model.EmrWinformTemplet modWinformTemplet)
        //{
        //    using (Session emrSession = SessionManager.CreateByConnName(LoginDictCache.emrConnectStr))
        //    {
        //        modNursCatalogDic = new Model.EmrNurseCatlogDict();
        //        modNursCatalogDic.CATALOG_CODE = "BE";
        //        modNursCatalogDic.MR_CODE = modWinformTemplet.PAGE_NO.ToString();
        //        modNursCatalogDic.MR_CODE_PRINT = "BF010001"; //BF010001（上海）;BE010013（本地）
        //        modNursCatalogDic.ROW_COUNT = 15;
        //        modNursCatalogDic.PRINT_GROUP = null;
        //        modNursCatalogDic.DEFAULT_LAST = 0;
        //        modNursCatalogDic.FIRST_PAGE = 0;
        //        modNursCatalogDic.ORDER_NO = null;
        //        modNursCatalogDic.SIGN_FLAG = 0;
        //        modNursCatalogDic.FIRST_ROW_COUNT = 2;
        //        modNursCatalogDic.FIRST_MR_CODE_PRINT = "BE010013";
        //        modNursCatalogDic.OBSERV_NURS_LINE_LENGTH = 30;
        //        emrSession.Save<EMR.Model.EmrNurseCatlogDict>(modNursCatalogDic);
        //    }
        //}
        #endregion

        #region 删除数据库中的模版（未使用）
        /// <summary>
        /// 删除数据库中的模版
        /// </summary>
        /// <param name="strPageNo"></param>
        private static void DeleteTemplet(string strPageNo)
        {
            //using (Session emrSession = SessionManager.CreateByConnName(LoginDictCache.emrConnectStr))
            //{
            //    emrSession.Delete<EMR.Model.EmrWinformTemplet>(string.Format(@"Page_No = '{0}'", strPageNo));
            //    emrSession.Delete<EMR.Model.EmrNurseCatlogDict>(string.Format(@"Mr_Code = '{0}'", strPageNo));
            //}
        }
        #endregion

        #region 模版列表gridView1双击事件
        /// <summary>
        /// 模版列表gridView1双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            //if (tabControl1.SelectedTab != null)
            //{
            //    if (tabControl1.SelectedTab.Text == dr["PAGE_TYPE"].ToString())
            //    {
            //        return;     //如果该窗体已经打开，则返回
            //    }
            //}

            foreach (TabPage tp in tabControl1.TabPages)
            {
                if (dr["PAGE_NO"].ToString() == tp.Name)
                {
                    this.tabControl1.SelectedTab = tp;
                    return;
                }
            }

            nCurrentPageNo = Convert.ToInt32(dr["PAGE_NO"].ToString());
            DataRow[] drCurrent = dtFormTempletList.Select("PAGE_NO=" + nCurrentPageNo + "");
            if (drCurrent.Length > 0)
            {
                byte[] b = drCurrent[0]["PAGE_CONTENT"] as byte[];
                HostControl hc = _hostSurfaceManager.GetNewHostByByte(b);
                this.myToolBox.DesignerHost = hc.DesignerHost;
                TabPage tabpage = new TabPage(drCurrent[0]["PAGE_TYPE"].ToString());
                tabpage.Tag = LoaderType.BasicDesignerLoader;
                tabpage.Name = drCurrent[0]["PAGE_NO"].ToString();
                hc.Parent = tabpage;
                hc.Dock = DockStyle.Fill;
                this.tabControl1.TabPages.Add(tabpage);
                this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                _hostSurfaceManager.ActiveDesignSurface = hc.HostSurface;

                //添加控件有键盘方向键移动 孙奎松 2014年6月10日18:12:50
                filter = new KeystrokeMessageFilter(hc.DesignerHost);
                Application.AddMessageFilter(filter);
            }
        }
        #endregion

        #region  新建按钮单击事件
        /// <summary>
        /// 新建按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                HostControl hc = _hostSurfaceManager.GetNewHost(typeof(UserControl), LoaderType.BasicDesignerLoader);
                AddTabForNewHost("新建窗体", hc);
            }
            catch
            {
                MessageBox.Show("Error in creating new host", "Shell Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region tabControl1_SelectedIndexChanged事件（切换Tap页时，把GridView里相当应的行设为当前行）。
        /// <summary>
        /// tabControl1_SelectedIndexChanged事件（切换Tap页时，把GridView里相当应的行设为当前行）。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFocus(tabControl1.SelectedTab.Text, gridControl1, "PAGE_TYPE");
        }

        /// <summary>
        /// 设定GridView的某一行为当前当前行
        /// </summary>
        /// <param name="searchText">设定行的本文内容</param>
        /// <param name="gridControl">设定行的本文内容所在的列</param>
        /// <param name="colname"></param>
        private void SetFocus(string searchText, DevExpress.XtraGrid.GridControl gridControl, string colname)
        {
            DevExpress.XtraGrid.Views.Base.ColumnView view = (DevExpress.XtraGrid.Views.Base.ColumnView)gridControl.FocusedView;
            DevExpress.XtraGrid.Columns.GridColumn column = view.Columns[colname];
            if (column != null)
            {
                //int rhFound = view.LocateByDisplayText(view.FocusedRowHandle + 1, column, searchText);
                int rhFound = view.LocateByDisplayText(0, column, searchText);
                if (rhFound != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    view.FocusedRowHandle = rhFound;
                    view.FocusedColumn = column;
                }
            }
        }
        #endregion

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var path = ofd.FileName;
                byte[] b = File.ReadAllBytes(path);
                HostControl hc = _hostSurfaceManager.GetNewHostByByte(b);
                this.myToolBox.DesignerHost = hc.DesignerHost;
                TabPage tabpage = new TabPage(ofd.SafeFileName);
                tabpage.Tag = LoaderType.BasicDesignerLoader;
                tabpage.Name = ofd.SafeFileName;
                hc.Parent = tabpage;
                hc.Dock = DockStyle.Fill;
                this.tabControl1.TabPages.Add(tabpage);
                this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                _hostSurfaceManager.ActiveDesignSurface = hc.HostSurface;

                //添加控件有键盘方向键移动 孙奎松 2014年6月10日18:12:50
                filter = new KeystrokeMessageFilter(hc.DesignerHost);
                Application.AddMessageFilter(filter);
            }
        }
    }
}