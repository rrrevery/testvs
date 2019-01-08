using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;
using System.Configuration;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using RRR;

namespace TransData3
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private const int VM_NCLBUTTONDOWN = 0XA1;//定义鼠标左键按下
        private const int HTCAPTION = 2;

        public List<TransDefine> TransList = new List<TransDefine>();
        public string Server = ConfigurationManager.AppSettings["Reciever"].ToString();
        public string TransID = ConfigurationManager.AppSettings["TransID"].ToString();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransData();
        }
        private void TransData()
        {
            try
            {
                Log("开始同步数据……");
                TransList.Clear();
                DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
                RQuery query = new RQuery(conn);
                query.SQL.Text = "select * from TransDefine where Enabled=1 and TransID=" + TransID + " order by Inx";
                query.Open();
                while (!query.Eof)
                {
                    TransList.Add(new TransDefine
                    {
                        TblName = query.FieldByName("TblName").AsString,
                        KeyFld = query.FieldByName("KeyFld").AsString,
                        TMFld = query.FieldByName("TMFld").AsString,
                        TMType = query.FieldByName("TMType").AsInteger,
                        Stamp = RTools.GetTMStr(query.FieldByName("Stamp").AsBytes),
                        Stamp_Int = query.FieldByName("Stamp_Int").AsInteger,
                        Stamp_Str = query.FieldByName("Stamp_Str").AsString
                    });
                    query.Next();
                }
                query.Close();
                foreach (var one in TransList)
                {
                    TransOneData(query, one);
                }
                conn.Close();
                Log("同步数据完毕");
            }
            catch (Exception e)
            {
                Log("杯具了：" + e.Message);
            }
        }
        private void TransOneData(RQuery query, TransDefine one)
        {
            query.Close();
            if (one.TblName.IndexOf("DIR") >= 0)
            {
                string path = one.TblName.Substring(4);
                if (!long.TryParse(one.Stamp_Str, out long tick))
                    tick = 0;
                var files = (from f in Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                             let fi = new FileInfo(f)
                             where fi.LastWriteTime.Ticks > tick
                             orderby fi.LastWriteTime
                             select new { name = fi.FullName, time = fi.LastWriteTime });

                foreach (var file in files)
                {
                    string result = TransTools.PostFileToServer(Server + "?name=" + one.TblName + "&key=" + one.KeyFld + "&tm=" + one.TMFld, file.name);
                    if (result != "2")
                        Log("传输目录" + one.KeyFld + "失败，失败文件" + file.name);
                    else
                    {
                        Log("传输了目录" + one.KeyFld + "的文件" + file.name);
                        query.SQL.Text = "update TransDefine set Stamp_Str='" + file.time.Ticks + "' where TransID=" + TransID + " and TblName=:TblName";
                        //query.ParamByName("TrasnID").asi = one.TblName;
                        query.ParamByName("TblName").AsString = one.TblName;
                        query.ExecSQL();
                    }

                }
            }
            else if (one.TblName.IndexOf("FILE") >= 0)
            {

            }
            else
            {
                query.SQL.Text = "select * from " + one.TblName + " where 1=1";
                if (one.Stamp != string.Empty)
                    query.SQL.Add("and " + one.TMFld + ">" + one.Stamp);
                query.SQL.Add(" order by " + one.TMFld);
                query.Open();
                if (!query.IsEmpty)
                {
                    //string o = JsonConvert.SerializeObject(one);
                    string path = Application.StartupPath + "\\Files\\";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string filename = path + one.TblName + ".txt";
                    //File.WriteAllText(filename, o);
                    //File.AppendAllText(filename, "\r\n");
                    DataTable dt = query.GetDataTable();
                    Log("正在生成" + one.TblName + "数据，共" + dt.Rows.Count + "条");
                    string s = JsonConvert.SerializeObject(dt);
                    File.WriteAllText(filename, s);
                    string result = TransTools.PostFileToServer(Server + "?name=" + one.TblName + "&key=" + one.KeyFld + "&tm=" + one.TMFld, RTools.CompressFile(filename));
                    Log("正在传输" + one.TblName);
                    if (result == "1")
                    {
                        Log("传输" + one.TblName + "成功");
                        string tm = RTools.GetTMStr((byte[])dt.Rows[dt.Rows.Count - 1]["TM"]);
                        query.Close();
                        query.SQL.Text = "update TransDefine set Stamp=" + tm + " where TransID=" + TransID + " and TblName=:TblName";
                        //query.ParamByName("TrasnID").asi = one.TblName;
                        query.ParamByName("TblName").AsString = one.TblName;
                        query.ExecSQL();
                    }
                    else
                    {
                        Log("传输" + one.TblName + "失败，" + result);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
            RQuery query = new RQuery(conn);
            query.SQL.Text = "delete from TB1";
            query.ExecSQL();
            query.SQL.Text = "delete from TB2";
            query.ExecSQL();
            for (int i = 1; i <= 1000; i++)
            {
                query.SQL.Text = "insert into TB1(ID,NAME,CODE,CREATETIME) values(newid(),:NAME,:CODE,getdate())";
                query.ParamByName("NAME").AsString = "名称" + i;
                query.ParamByName("CODE").AsInteger = i;
                query.ExecSQL();
                query.SQL.Text = "insert into TB2(ID,VALUE,COUNT) values(newid(),:VALUE,:COUNT)";
                query.ParamByName("VALUE").AsFloat = DateTime.Now.Millisecond / 10;
                query.ParamByName("COUNT").AsInteger = DateTime.Now.Millisecond;
                query.ExecSQL();
            }
            conn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
            RQuery query = new RQuery(conn);
            query.SQL.Text = "update TransDefine set Stamp=null,Stamp_Int=null,Stamp_Str=null";
            query.ExecSQL();
            conn.Close();
        }
        private void Log(string str)
        {
            tbMsg.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff ") + str + "\r\n");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            TransData();
            timer1.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chbAuto_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = chbAuto.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            TransData();
            timer1.Enabled = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage((IntPtr)this.Handle, VM_NCLBUTTONDOWN, HTCAPTION, 0);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            tbMsg.Clear();
        }
    }
}
