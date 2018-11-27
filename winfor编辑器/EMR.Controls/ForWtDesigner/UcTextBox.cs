using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EMR.Controls
{
    public class UcTextBox : System.Windows.Forms.TextBox
    {
        #region 控件的属性
        private enumEleName elemName;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("字典名称")]
        public enumEleName ElemName
        {
            get { return elemName; }
            set { elemName = value; }
        }

        private int columnNo = 0;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("列位置号")]
        public int ColumnNo
        {
            get { return columnNo; }
            set { columnNo = value; }
        }

        private string sqlStr = string.Empty;
        [CategoryAttribute("数据检索"), ReadOnlyAttribute(false), DescriptionAttribute("SQL语句")]
        public string SqlStr
        {
            get { return sqlStr; }
            set { sqlStr = value; }
        }

        private string bindfield = string.Empty;
        [CategoryAttribute("数据检索"), ReadOnlyAttribute(false), DescriptionAttribute("返回值字段")]
        public string Bindfield
        {
            get { return bindfield; }
            set { bindfield = value; }
        } 
        #endregion

        #region 初始化组建

        private ErrorProvider errorProvider1;
        private IContainer components;

        /// <summary>
        /// 初始化组建
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.RightToLeftChanged += new System.EventHandler(this.TestTextBoxValue);
            // 
            // UcTextBox
            // 
            this.TextChanged += new System.EventHandler(this.UcTextBox_TextChanged);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        } 
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public UcTextBox()
        {
            InitializeComponent();
        } 
        #endregion

        #region 检测用户输入的数据的格式是否正确
        /// <summary>
        /// 增则表达式，用于验证用户输入的是否正确
        /// </summary>
        Regex regex = new Regex(@"(^-?\d+$)|(^\d+(\.\d+)?$)|(^\d+(\/\d+)?$)|(^$)");

        /// <summary>
        /// 检测用户输入的数据的格式是否正确
        /// </summary>
        private void TestTextBoxValue(object sender, EventArgs e)
        {
            if (!regex.IsMatch(this.Text.Trim()))
            {
                errorProvider1.SetError(this, "用户输入的数据格式不正确，请重新输入");
                this.Focus();
            }
            else
            {
                errorProvider1.Clear();
            }
        } 
        #endregion

        private void UcTextBox_TextChanged(object sender, EventArgs e)
        {
            TestTextBoxValue(null, null);
        }
    }
}