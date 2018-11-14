using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestShowDialog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MessageBox.Show方法");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageForm mf = new MessageForm("MessageForm.ShowDialog");
            mf.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageForm mf = new MessageForm("MessageForm.Show");
            mf.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            toolTip1.Show("ToolTip(" + MousePosition.X + "," + MousePosition.Y + ")", this, MousePosition.X - Left, MousePosition.Y - Top, 1000);
        }
    }
}
