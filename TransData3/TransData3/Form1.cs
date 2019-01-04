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
using System.Net;
using System.IO.Compression;
using Newtonsoft.Json;
using RRR;

namespace TransData3
{
    public partial class Form1 : Form
    {
        public class TransDefine
        {
            public string TblName = string.Empty;
            public string KeyFld = string.Empty;
            public string TMFld = string.Empty;
            public int TMType = 0;
            public string Stamp = string.Empty;
            public int Stamp_Int = 0;
            public string Stamp_Str = string.Empty;
        }
        public List<TransDefine> TransList = new List<TransDefine>();
        public string Server = ConfigurationManager.AppSettings["Reciever"].ToString();
        public string TrasnID = ConfigurationManager.AppSettings["TransID"].ToString();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TransList.Clear();
            DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
            RQuery query = new RQuery(conn);
            query.SQL.Text = "select * from TransDefine where Enabled=1 and TransID=" + TrasnID + " order by Inx";
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
        }
        private void TransOneData(RQuery query, TransDefine one)
        {
            query.Close();
            query.SQL.Text = "select * from " + one.TblName + " where 1=1";
            if (one.Stamp != string.Empty)
                query.SQL.Add("and " + one.TMFld + ">" + one.Stamp);
            query.SQL.Add(" order by " + one.TMFld);
            query.Open();
            if (!query.IsEmpty)
            {
                string o = JsonConvert.SerializeObject(one);
                string path = Application.StartupPath;
                string filename = path + "\\" + one.TblName + ".txt";
                File.WriteAllText(filename, o);
                File.AppendAllText(filename, "\r\n");
                DataTable dt = query.GetDataTable();
                string s = JsonConvert.SerializeObject(dt);
                File.AppendAllText(filename, s);
                //CompressSingle(filename);
                //PostFileToServer(filename + ".gz");
                PostFileToServer(filename);
                MessageBox.Show("1");
                string tm = RTools.GetTMStr((byte[])dt.Rows[dt.Rows.Count - 1]["TM"]);
                query.Close();
                query.SQL.Text = "update TransDefine set Stamp=" + tm + " where TransID=" + TrasnID + " and TblName=:TblName";
                //query.ParamByName("TrasnID").asi = one.TblName;
                query.ParamByName("TblName").AsString = one.TblName;
                query.ExecSQL();
            }
        }
        public string CompressSingle(string sourceFilePath)
        {
            string zipFileName = sourceFilePath + ".gz";
            using (FileStream sourceFileStream = new FileInfo(sourceFilePath).OpenRead())
            {
                using (FileStream zipFileStream = File.Create(zipFileName))
                {
                    using (GZipStream zipStream = new GZipStream(zipFileStream, CompressionMode.Compress))
                    {
                        sourceFileStream.CopyTo(zipStream);
                    }
                }
            }
            return zipFileName;
        }
        public void PostFileToServer(string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffur = new byte[fs.Length];
            fs.Read(buffur, 0, (int)fs.Length);

            Upload(Server, "file", file, buffur, 0, buffur.Length);
            fs.Close();
        }
        static public string Upload(string uriStr, string name, string fileName, byte[] data, int offset, int count)
        {
            try
            {
                var request = WebRequest.Create(uriStr);
                request.Method = "POST";
                var boundary = $"******{DateTime.Now.Ticks}***";
                request.ContentType = $"multipart/form-data; boundary={boundary}";
                boundary = $"--{boundary}";
                using (var requestStream = request.GetRequestStream())
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes($"Content-Disposition: form-data; name=\"{name}\"; filename=\"{fileName}\"{Environment.NewLine}");
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes($"Content-Type: application/octet-stream{Environment.NewLine}{Environment.NewLine}");
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Write(data, offset, count);
                    buffer = Encoding.ASCII.GetBytes($"{Environment.NewLine}{boundary}--");
                    requestStream.Write(buffer, 0, buffer.Length);
                }
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
            RQuery query = new RQuery(conn);
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
            query.SQL.Text = "update TransDefine set Stamp=null";
            query.ExecSQL();
            conn.Close();
        }
    }
}
