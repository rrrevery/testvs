using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EMR.Controls
{
    public class UcCheckBox : System.Windows.Forms.CheckBox
    {
        private enumEleName elemName;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("字典名称")]
        public enumEleName ElemName
        {
            get { return elemName; }
            set { elemName = value; }
        }

        private int columnNo = 0;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("列序位置号")]
        public int ColumnNo
        {
            get { return columnNo; }
            set { columnNo = value; }
        }
    }
}
