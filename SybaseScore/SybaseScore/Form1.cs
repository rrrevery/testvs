using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BF.Pub;

namespace SybaseScore
{
    public partial class Form1 : Form
    {
        public static int[] ct = new int[20];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task[] tasks = new Task[20];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task<int>.Factory.StartNew(action);
            }
            //for (int i = 0; i < 50; i++)
            //{
            //    Thread thread1 = new Thread(new ThreadStart(t.InsertThread));
            //    thread1.Start();
            //}
            Task.WaitAll(tasks);
            int tct = 0;
            for (int i = 0; i < tasks.Length; i++)
            {
                tct += ct[i];
            }
            Log(tct.ToString());
        }
        public void Log(string str)
        {
            tbMsg.Text += str + "\r\n";
        }
        Func<int> action = () =>
        {
            int c = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("SYB");
            CyQuery query = new CyQuery(conn);
            for (int i = 0; i < 500; i++)
            {
                Random rd = new Random();
                query.SQL.Text = "insert into SS1(I1,I2,I3,I4,I5,F1,F2,F3,F4,F5,S1,S2,S3,S4,S5) values(:I1,:I2,:I3,:I4,:I5,:F1,:F2,:F3,:F4,:F5,:S1,:S2,:S3,:S4,:S5)";
                query.ParamByName("I1").AsInteger = i;
                query.ParamByName("I2").AsInteger = i + 1;
                query.ParamByName("I3").AsInteger = i + 2;
                query.ParamByName("I4").AsInteger = i + 3;
                query.ParamByName("I5").AsInteger = i + 4;
                query.ParamByName("F1").AsFloat = rd.Next(100);
                query.ParamByName("F2").AsFloat = rd.Next(100);
                query.ParamByName("F3").AsFloat = rd.Next(100);
                query.ParamByName("F4").AsFloat = rd.Next(100);
                query.ParamByName("F5").AsFloat = rd.Next(100);
                query.ParamByName("S1").AsString = i.ToString().PadLeft(100, '0');
                query.ParamByName("S2").AsString = (i + 1).ToString().PadLeft(100, '0');
                query.ParamByName("S3").AsString = (i + 2).ToString().PadLeft(100, '0');
                query.ParamByName("S4").AsString = (i + 3).ToString().PadLeft(100, '0');
                query.ParamByName("S5").AsString = (i + 4).ToString().PadLeft(100, '0');
                DateTime bg = DateTime.Now;
                query.ExecSQL();
                DateTime js = DateTime.Now;
                c += (int)Math.Round((js - bg).TotalMilliseconds);
            }
            ct[(int)Task.CurrentId - 1] = c;
            conn.Close();
            return c;
        };
    }
}
