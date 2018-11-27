using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR.Controls
{
    public partial class FormSymbol : Form
    {
        // <summary>
        /// 更新父界面的GridControl里，当前行的打印类别
        /// </summary>
        /// <param name="copyCode"></param>
        public delegate void UpDataTextBox(string s);

        /// <summary>
        /// 更新父界面的GridControl里，当前行的打印类别
        /// </summary>
        public event UpDataTextBox upDataTextBox;

        public FormSymbol()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            upDataTextBox(button1.Text.Trim());
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
