using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR.Controls
{
    public partial class UcUserDefined : UserControl
    {
        #region 自定义的属性
        private enumUserDeineItems elemName;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("字典名称")]
        public enumUserDeineItems ElemName
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
        #endregion

        public UcUserDefined()
        {
            InitializeComponent();
        }

        private void UcUserDefined_Load(object sender, EventArgs e)
        {
            ContextMenu emptyMenu = new ContextMenu();
            textEdit1.Properties.ContextMenu = emptyMenu;

            setDsCmbDictValue();
        }

        private void setDsCmbDictValue()
        {
            //DataSet dsCmbDict = EMR.PubFunction.PubFunc.GetEmrDictDetails("TEMPERATURE_ZDY", "", "");
            //if (dsCmbDict != null && dsCmbDict.Tables.Count > 0 && dsCmbDict.Tables[0].Rows.Count > 0)
            //{
            //    //comboBox1.Items
            //    comboBoxEdit1.Properties.Items.Add("");//在自定义项列表的顶端增加一个空项目，用作删除自定义项
            //    for (int i_c = 0; i_c < dsCmbDict.Tables[0].Rows.Count; i_c++)
            //    {
            //        comboBoxEdit1.Properties.Items.Add(dsCmbDict.Tables[0].Rows[i_c]["short_name"].ToString());
            //    }
            //}

            //comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }

        private void UcUserDefined_SizeChanged(object sender, EventArgs e)
        {
            this.Height = 20;
        }

        private void textEdit1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FormSymbol f = new FormSymbol();
                f.Location = textEdit1.PointToScreen(new Point(0, textEdit1.Height));
                f.upDataTextBox += f_upDataTextBox;
                f.ShowDialog();
            }
        }

        void f_upDataTextBox(string s)
        {
            //throw new NotImplementedException();
            textEdit1.Text += s;
            textEdit1.Select(textEdit1.Text.Length, 0);
        }
    }
}
