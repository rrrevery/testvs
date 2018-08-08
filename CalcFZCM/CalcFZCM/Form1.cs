using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcFZCM
{
    public partial class Form1 : Form
    {
        //public List<string> AllSizeList = new List<string>({"xxxxl", "xxxl"});
        public string[] AllSizeList = { "xxxxl", "xxxl", "xxl", "4xl", "3xl", "2xl", "xl", "l", "m", "s" };
        //public string[] SizeList = { "4xl", "3xl", "2xl", "xl", "l", "m", "s" };
        //public int[] SizeTotalList = { 0, 0, 0, 0, 0, 0, 0 };
        public List<SizeInfo> SizeList2 = new List<SizeInfo>();
        public class SizeInfo
        {
            public string Size = string.Empty;
            public int Count = 0;
            public string RY = string.Empty;
            public SizeInfo(string size)
            {
                Size = size;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SizeList2.Add(new SizeInfo("4xl"));
            SizeList2.Add(new SizeInfo("3xl"));
            SizeList2.Add(new SizeInfo("2xl"));
            SizeList2.Add(new SizeInfo("xl"));
            SizeList2.Add(new SizeInfo("l"));
            SizeList2.Add(new SizeInfo("m"));
            SizeList2.Add(new SizeInfo("s"));
            string[] ry = textBox1.Text.Split('\n');
            string cm = string.Empty;
            for (int i = 0; i < ry.Length; i++)
            {
                ry[i] = ry[i].Trim();
                for (int j = 0; j < AllSizeList.Length; j++)
                {
                    int inx = ry[i].IndexOf(AllSizeList[j]);
                    if (inx >= 0)
                    {
                        cm = ry[i].Substring(inx);
                        ry[i] = ry[i].Substring(0, inx);
                        //textBox2.Text += cm + " ";
                        int k = j;
                        if (j > 2)
                            k = j - 3;
                        SizeList2[k].Count++;
                        SizeList2[k].RY += ry[i] + ",";
                    }
                }
            }
            for (int i = 0; i < SizeList2.Count; i++)
            {
                textBox2.Text += SizeList2[i].Size + " " + SizeList2[i].Count +" "+ SizeList2[i].RY + "\r\n";
            }
        }
    }
}
