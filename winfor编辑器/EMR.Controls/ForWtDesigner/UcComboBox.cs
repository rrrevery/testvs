using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EMR.Controls
{
    public class UcComboBox : System.Windows.Forms.ComboBox
    {
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

        private bool isSaveKey = false;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("存储键值，还是文本。True:存储键值；fasle:False存储文本")]
        public bool IsSaveKey
        {
            get { return isSaveKey; }
            set { isSaveKey = value; }
        }

      
    }
}
