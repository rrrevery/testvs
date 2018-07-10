using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ChineseToUnicode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(textBox1.Text);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("[{0}{1}]", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            textBox2.Text = stringBuilder.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text= new Regex(@"\[([0-9A-F]{4})\]", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
             textBox2.Text, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
    }
}
