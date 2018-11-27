using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EMR.Controls
{
    public class UcNumericUpDown : System.Windows.Forms.NumericUpDown
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

        public UcNumericUpDown()
        {
            //this.Paint += UcNumericUpDown_Paint;
        }

        void UcNumericUpDown_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            this.Value = 37;
            this.DecimalPlaces = 1;
            this.Increment = 0.1M;
            this.Maximum = 42;
            this.Minimum = 35;
        }
    }
}