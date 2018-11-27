using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EMR.Controls
{
    //public class UcCheckBoxList:DevExpress.XtraEditors.CheckedListBoxControl
    public class UcCheckBoxList : System.Windows.Forms.CheckedListBox
    {
        public UcCheckBoxList()
            : base()
        {
            this.Click += UcCheckBoxList_Click;
        }

        /// <summary>
        /// 单击选中Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void UcCheckBoxList_Click(object sender, EventArgs e)
        {
            if (this.GetItemChecked(this.SelectedIndex))
            {
                this.SetItemChecked(this.SelectedIndex, false);
                this.SelectedIndex = -1;
            }
            else
            {
                this.SetItemChecked(this.SelectedIndex, true);
                this.SelectedIndex = -1;
            }
        }

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
        private string dictValue;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("字典类别")]
        public string DictValue
        {
            get { return dictValue; }
            set { dictValue = value; }
        }
    }
}
