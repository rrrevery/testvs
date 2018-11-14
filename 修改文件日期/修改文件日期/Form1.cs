using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace 修改文件日期
{
    public partial class Form1 : Form
    {
        DataTable table;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            table.Clear();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            foreach (var item in files)
            {
                string s= item.Name;
                s = s.Replace(".jpg", "");
                s = s.Replace(".mp4", "");
                if (s[0] == 'P')
                    s = s.Replace("P", "201");
                if (s[0] == 'S')
                    s = s.Replace("S", "201");
                s = s.Replace("-24", "-00");
                s = s.Replace("-", "");
                DateTime dt = DateTime.ParseExact(s, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                table.Rows.Add(item.Name, item.LastWriteTime, dt);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("文件名", typeof(string));
            table.Columns.Add("原修改时间", typeof(string));
            table.Columns.Add("新修改时间", typeof(DateTime));
            dataGridView1.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataRow item in table.Rows)
            {
                FileInfo f = new FileInfo(textBox1.Text + item["文件名"]);
                f.LastWriteTime = (DateTime)item["新修改时间"];
            }
        }
    }
}
