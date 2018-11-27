using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR.WinformDesigner
{
    public partial class RHFrmNewFormType : Form
    {
        #region 全局变量
        /// <summary>
        /// 模版名称
        /// </summary>
        public string strFrmType;
        /// <summary>
        /// 打印模版
        /// </summary>
        //public string strPintNo = string.Empty;
        #endregion

        /// <summary>
        /// 打印模版数据集
        /// </summary>
        DataSet dsPrintList = new DataSet();

        public RHFrmNewFormType()
        {
            InitializeComponent();
        }

        private void RHFrmNewFormType_Load(object sender, EventArgs e)
        {
            //using (Session emrSession = SessionManager.CreateByConnName(LoginDictCache.emrConnectStr))
            //{
            //    dsPrintList = emrSession.FindDataTable<EMR.Model.EmrTemplateIndex>();
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            strFrmType = textBox1.Text.Trim();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}