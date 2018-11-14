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
    public partial class MessageForm : Form
    {
        public MessageForm(string msg = "", string caption = "")
        {
            InitializeComponent();
            label1.Text = msg;
            Text = caption;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
