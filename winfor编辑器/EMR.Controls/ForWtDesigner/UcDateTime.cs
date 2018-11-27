using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR.Controls
{
    public class UcDateTime : System.Windows.Forms.DateTimePicker
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

        public UcDateTime()
        {
            this.MouseWheel += UcDateTime_MouseWheel;
            this.Paint += UcDateTime_Paint;

            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "yyyy-MM-dd HH:mm";
        }

        void UcDateTime_Paint(object sender, PaintEventArgs e)
        {
            //throw new NotImplementedException();
            //this.Format = 
        }

        void UcDateTime_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.Delta < 0)//鼠标滚动轴往下滚  
            {
                SendKeys.Send("{Down}");
            }
            else // 鼠标滚动轴 往上滚  
            {
                SendKeys.Send("{Up}");
            }
        }
    }
}
