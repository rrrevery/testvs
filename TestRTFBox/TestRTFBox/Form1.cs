using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestRTFBox
{
    public partial class Form1 : Form
    {
        int cl = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = "测试一下";

            int b = rtbLog.TextLength;
            rtbLog.Text += s;
            rtbLog.Select(b, s.Length - 2);
            //MessageBox.Show(rtbLog.SelectionColor.ToString());
            switch (cl % 3)
            {
                case 0: rtbLog.SelectionColor = Color.Black; break;
                case 1: rtbLog.SelectionColor = Color.Blue; break;
                case 2: rtbLog.SelectionColor = Color.Red; break;
            }
            //MessageBox.Show(rtbLog.SelectionColor.ToString());
            cl++;
            //rtbLog.Text += "\r\n";
            rtbLog.Select(0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = "测试一下";

            lstLog.Items.Add(s);
            //int b = rtbLog.TextLength;
            //lstLog.Text += s;
            //rtbLog.Select(b, s.Length - 2);
            ////MessageBox.Show(rtbLog.SelectionColor.ToString());
            //switch (cl % 3)
            //{
            //    case 0: rtbLog.SelectionColor = Color.Black; break;
            //    case 1: rtbLog.SelectionColor = Color.Blue; break;
            //    case 2: rtbLog.SelectionColor = Color.Red; break;
            //}
            ////MessageBox.Show(rtbLog.SelectionColor.ToString());
            //cl++;
            ////rtbLog.Text += "\r\n";
            //rtbLog.Select(0, 0);
        }

        private void lstLog_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();          //先调用基类实现

            if (e.Index < 0)            //form load 的时候return
                return;

            //因为此函数每一个 listItem drawing 都要调用， 所以不能简单的只写e.Graphics.DrawString(lstLog.Items[e.Index].ToString(),e.Font, Brushes.Red, e.Bounds);
            //那样会造成所有item一个颜色
            //这里是用item字符串是否包含某些词决定的 ， 不好
            if (lstLog.Items[e.Index].ToString().Contains("error"))
            {
                e.Graphics.DrawString(lstLog.Items[e.Index].ToString(),
                e.Font, Brushes.Red, e.Bounds);
            }
            else if (lstLog.Items[e.Index].ToString().Contains("warn"))
            {
                e.Graphics.DrawString(lstLog.Items[e.Index].ToString(),
                    e.Font, Brushes.Red, e.Bounds);

            }
            else
            {
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(),
                        e.Font, Brushes.Black, e.Bounds);
            }
        }
    }
}
