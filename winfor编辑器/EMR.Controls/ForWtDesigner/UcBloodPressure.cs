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
    public partial class UcBloodPressure : UserControl
    {
        private enumEleName elemName = enumEleName.血压;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("字典名称")]
        public enumEleName ElemName
        {
            get { return elemName; }
            //set { elemName = value; }
        }

        private int columnNo = 0;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("列序位置号")]
        public int ColumnNo
        {
            get { return columnNo; }
            set { columnNo = value; }
        }

        /// <summary>
        /// 血压值
        /// </summary>
        private string bloodPreValue = string.Empty;

        /// <summary>
        /// 血压值
        /// </summary>
        public string BloodPreValue
        {
            get { return bloodPreValue; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] s = value.Split('/');
                    if (s.Length >= 2)
                    {
                        textBox1.Text = s[0];
                        textBox2.Text = s[1];
                    }
                }
                bloodPreValue = value;
            }
        }

        public UcBloodPressure()
        {
            InitializeComponent();
        }

        private void UcBloodPressure_Resize(object sender, EventArgs e)
        {
            this.Height = 24;
            textBox1.Width = panel1.Width / 2 - label1.Width / 2;
            label1.Left = textBox1.Left + textBox1.Width;
            textBox2.Width = textBox1.Width;
            textBox2.Left = label1.Left + label1.Width;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            BloodPreValue = textBox1.Text.Trim() + "/" + textBox2.Text.Trim();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}+{HOME}");
            }
        }
    }
}
