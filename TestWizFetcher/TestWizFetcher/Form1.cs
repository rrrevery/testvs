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

namespace TestWizFetcher
{
    public partial class Form1 : Form
    {
        DataTable table;
        int Rec = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //webBrowser1.

            string url = @"file://E:/Mobile/电子书/恐怖小说/R/5/80fed310-5926-4dfc-9a5d-5dcb04baf12a.html";
            webBrowser1.Navigate(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            File.WriteAllText(table.Rows[Rec]["TXT"].ToString(), webBrowser1.Document.Body.InnerText);
            if (Rec < table.Rows.Count)
            {
                Rec++;
                ProcNext();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = tbFolder.Text;
            table.Clear();
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            foreach (var item in files)
            {
                string s = item.FullName;
                s = s.Replace(@"\", @"/");
                s = "file://" + s;
                table.Rows.Add(item.Name, s, item.FullName.Replace(".html", ".txt"));
            }
            ProcNext();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("HTML", typeof(string));
            table.Columns.Add("URL", typeof(string));
            table.Columns.Add("TXT", typeof(string));
            tabControl1.SelectedIndex = 1;
        }
        private void ProcNext()
        {
            string url = table.Rows[Rec]["URL"].ToString();
            webBrowser1.Navigate(url);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var lst = textBox1.Lines;
            string folder = string.Empty;
            string rootdir = @"e:\Mobile\电子书\恐怖小说\R\7\";
            foreach (var one in lst)
            {
                if (one == "")
                    continue;
                if (one.Substring(0, 3) == "<ul")
                {
                    folder = GetHTMLAttr(one, "data-id");
                    DirectoryInfo d = Directory.CreateDirectory(rootdir + folder);
                }
                if (one.Substring(0, 3) == "<li")
                {
                    string ofile = GetHTMLAttr(one, "data-docguid");
                    string nfile = GetHTMLText(one, "li");
                    File.Copy(rootdir + ofile + ".txt", rootdir + folder + "\\" + nfile + ".txt", true);
                    File.Delete(rootdir + ofile + ".txt");
                }
            }
        }
        string GetHTMLAttr(string html, string attr)
        {
            int i = html.IndexOf(attr);
            int q1 = html.IndexOf("\"", i);
            int q2 = html.IndexOf("\"", q1 + 1);
            return html.Substring(q1 + 1, q2 - q1 - 1);
        }
        string GetHTMLText(string html, string element)
        {
            int i = html.IndexOf(element);
            int q1 = html.IndexOf(">", i);
            int q2 = html.IndexOf("<", q1 + 1);
            return html.Substring(q1 + 1, q2 - q1 - 1);
        }
    }
}
