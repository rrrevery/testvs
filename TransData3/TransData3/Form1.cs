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
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Net;
using System.Data.SqlClient;
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
            DbConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MSSQLLocal"].ToString());
            conn.Open();
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
            return;
            DateTime start = DateTime.Now;
            // 要上传的文件 
            FileStream oFileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader oBinaryReader = new BinaryReader(oFileStream);
            // 根据uri创建HttpWebRequest对象 
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(Server));
            httpReq.Method = "POST";
            //对发送的数据不使用缓存 
            httpReq.AllowWriteStreamBuffering = false;

            //设置获得响应的超时时间（半小时） 
            httpReq.Timeout = 300000;

            httpReq.KeepAlive = true;
            httpReq.ProtocolVersion = HttpVersion.Version11;

            httpReq.ContentType = "multipart/form-data";
            long filelength = oFileStream.Length;
            httpReq.SendChunked = true;
            //在将 AllowWriteStreamBuffering 设置为 false 的情况下执行写操作时，必须将 ContentLength 设置为非负数，或者将 SendChunked 设置为 true。

            //每次上传4k 
            int bufferLength = 4 * 1024;
            byte[] buffer = new byte[bufferLength];

            //已上传的字节数 
            long offset = 0;

            //开始上传时间 
            DateTime startTime = DateTime.Now;
            int size = oBinaryReader.Read(buffer, 0, bufferLength);
            Stream postStream = httpReq.GetRequestStream();

            while (size > 0)
            {
                postStream.Write(buffer, 0, size);
                offset += size;
                size = oBinaryReader.Read(buffer, 0, bufferLength);
                //Console.Write(".");
            }
            //Console.WriteLine(".");
            postStream.Flush();
            postStream.Close();

            //获取服务器端的响应 
            WebResponse webRespon = httpReq.GetResponse();
            Stream s = webRespon.GetResponseStream();
            StreamReader sr = new StreamReader(s);
            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
            //读取服务器端返回的消息 
            String sReturnString = sr.ReadLine();
            Console.WriteLine("retcode=" + sReturnString + " 花费时间=" + ts.TotalSeconds.ToString());
            s.Close();
            sr.Close();
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
        //public ResultObject PostFile(string url, string filePath, Dictionary<string, object> items)
        //{
        //    string boundary = DateTime.Now.Ticks.ToString("x");
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.ContentType = "multipart/form-data; boundary=" + boundary;
        //    request.Method = "POST";

        //    // 最后的结束符  
        //    var endBoundary = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

        //    //文件数据
        //    string fileFormdataTemplate =
        //        "--" + boundary +
        //        "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"" +
        //        "\r\nContent-Type: application/octet-stream" +
        //        "\r\n\r\n";

        //    //文本数据 
        //    string dataFormdataTemplate =
        //        "\r\n--" + boundary +
        //        "\r\nContent-Disposition: form-data; name=\"{0}\"" +
        //        "\r\n\r\n{1}";

        //    string fileHeader = string.Format(fileFormdataTemplate, "upfile", filePath);
        //    var fileBytes = Encoding.UTF8.GetBytes(fileHeader);

        //    using (var postStream = new MemoryStream())
        //    {
        //        //写入文件格式串
        //        postStream.Write(fileBytes, 0, fileBytes.Length);

        //        #region 写入文件流
        //        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //        {
        //            byte[] buffer = new byte[1024];
        //            int bytesRead = 0;
        //            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
        //            {
        //                postStream.Write(buffer, 0, bytesRead);
        //            }
        //        }
        //        #endregion

        //        #region 写入其他表单参数
        //        foreach (KeyValuePair<string, object> key in items)
        //        {
        //            var p = string.Format(dataFormdataTemplate, key.Key, key.Value);
        //            var bp = Encoding.UTF8.GetBytes(p);
        //            postStream.Write(bp, 0, bp.Length);

        //            //System.Diagnostics.Debug.WriteLine(p);
        //        }
        //        #endregion

        //        //写入结束边界
        //        postStream.Write(endBoundary, 0, endBoundary.Length);

        //        #region 写入流

        //        request.ContentLength = postStream.Length;
        //        postStream.Position = 0;
        //        using (Stream ds = request.GetRequestStream())
        //        {
        //            byte[] buffer = new byte[1024];
        //            int bytesRead = 0;
        //            while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
        //            {
        //                ds.Write(buffer, 0, bytesRead);
        //            }
        //        }
        //        #endregion

        //        #region 获取返回值

        //        string result = string.Empty;
        //        using (HttpWebResponse rep = (HttpWebResponse)request.GetResponse())
        //        {
        //            using (Stream ds = rep.GetResponseStream())
        //            {
        //                using (StreamReader sr = new StreamReader(ds))
        //                {
        //                    result = sr.ReadToEnd();
        //                }
        //            }
        //        }

        //        JObject jresult = JObject.Parse(result);
        //        if (bool.Parse(jresult["success"].ToString()))
        //        {
        //            return new ResultObject(EnumState.ok, "ok", jresult["data"]);
        //        }
        //        else
        //        {
        //            return new ResultObject(EnumState.err, jresult["data"].ToString());
        //        }

        //        #endregion
        //    }
        //}
    }
}
