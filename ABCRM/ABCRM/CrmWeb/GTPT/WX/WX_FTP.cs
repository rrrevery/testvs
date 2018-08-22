using System;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.CrmProc;

using System.Net;
using System.Text;
using System.IO;
using BF.Pub;
using System.Threading;
using System.Data;
using System.Data.Common;
using System.Security;
using System.Net.Security;
using System.Configuration;
namespace BF.CrmWeb.GTPT.WX
{
    public class WX_FTP : WX_Base
    {
        ManualResetEvent waitObject;
        public string wxFileName = "";//
        public string link = "";//
        public string ftpurl = "";
        public string ftpuser = "";
        public string ftppwd = "";
        public string wxserverurl = "";
        public string dirname = "";


        //将数据保存到本地(为了本地的预览,前台控件已实现)，也保存到FTP服务器上（为了页面的显示正常）
        public override string InsertData(out string msg, HttpContext context = null)
        {

            msg = "";

            if (UploadFileToFTP(out msg, context))
            {

                wxFileName = filename.Substring(filename.LastIndexOf("\\") + 1);
                return JsonConvert.SerializeObject(this);//在前台主要使用 filename wxFileName这两个属性
            }
            return "请求失败";
        }

        //上传文件(1、读取文件，将文件转换成流写入到请求流当中。2、发送请求，获得FTP服务器的返回信息)
        public bool UploadFileToFTP(out string msg, HttpContext context = null)
        {

            msg = "";
            bool result = false;
            try
            {
                if (GetBuffer(out msg, context))
                {
                    string name = saveFileResource(out msg);

                    ;//写到程序所指定的文件夹当中
                    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                    CyQuery query = new CyQuery(conn);
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "select IP_NET,PSWD,IP_PUB,DIR from FTPCONFIG WHERE ID=1";
                    query.Open();
                    while (!query.Eof)
                    {

                        wxserverurl = query.FieldByName("IP_PUB").AsString;
                        this.ftppwd = query.FieldByName("PSWD").AsString;
                        string FTPDIR = query.FieldByName("DIR").AsString;
                        int t = FTPDIR.LastIndexOf("@");
                        this.ftpuser = FTPDIR.Substring(0, t);
                        this.ftpurl = "ftp://" + FTPDIR.Substring(t + 1, FTPDIR.Length - t - 1);
                        //this.ftpuser =
                        //obj.sDIR = query.FieldByName("DIR").AsString;
                        //obj.sIP_NET = query.FieldByName("IP_NET").AsString;

                        query.Next();
                    }
                    query.Close();
                    //wxserverurl = ConfigurationManager.AppSettings["WXServerIP"].ToString();
                    //this.ftpurl = ConfigurationManager.AppSettings["WXServerFTP"].ToString();
                    //this.ftpuser = ConfigurationManager.AppSettings["ftpuser"].ToString();
                    //this.ftppwd = ConfigurationManager.AppSettings["ftppwd"].ToString();

                    string year = serverTime.Year.ToString();
                    string month = (serverTime.Month.ToString().CompareTo("10") > 0) ? serverTime.Month.ToString() : 0 + serverTime.Month.ToString();
                    string day = (serverTime.Day.ToString().CompareTo("10") > 0) ? serverTime.Day.ToString() : 0 + serverTime.Day.ToString();
                    dirname = "WXFtp//" + year + month + day;
                    FtpMakeDir("WXFtp");
                    FtpMakeDir(dirname);

                    Url = ftpurl + "//" + dirname + "//" + filename.Substring(filename.LastIndexOf("/") + 1);

                    Uri uri = new Uri(Url);
                    FtpWebRequest ftprequest = (FtpWebRequest)FtpWebRequest.Create(uri);
                    ftprequest.Method = WebRequestMethods.Ftp.UploadFile;//设置操作类型  
                    //STOR <filename>储存（复制）文件到服务器上
                    //STOU <filename>储存文件到服务器名称上
                    ftprequest.Credentials = new NetworkCredential(ftpuser, ftppwd);//设置验证

                    //与FTP交互有关的类
                    FtpState state = new FtpState();
                    if (name == "")//属于只传了个文件绝对路径过来
                    {
                        state.FileName = this.dir + this.filename;
                    }
                    else//传的是文件流，并且已经保存到了本地
                    {
                        state.FileName = fullFileName;
                    }
                    //state.FileName = fullFileName;

                    state.Request = ftprequest;

                    // Asynchronously get the stream for the file contents.  //将要上传的内容写到请求流当中
                    ftprequest.BeginGetRequestStream(
                        new AsyncCallback(EndGetStreamCallback),//委托  ，相当于事件处理函数
                        state);


                    waitObject = state.OperationComplete;//用于线程控制

                    // Block the current thread until all operations are complete.

                    waitObject.WaitOne();//直接收到信号，才开始进程

                    // The operations either completed or threw an exception.
                    if (state.OperationException != null)
                    {
                        msg = state.OperationException.Message;
                        result = false;
                    }
                    result = true;
                }
                else if (msg != "")
                {
                    msg = "上传到服务器失败";
                    result = false;
                }
                else if (msg == "")
                {
                    result = true;
                }

            }
            catch (Exception e)
            {
                msg = e.Message;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 完成文件流的读取
        /// </summary>
        /// <param name="ar"></param>
        private void EndGetStreamCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState)ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);
                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;
                FileStream stream = File.OpenRead(state.FileName);
                do
                {
                    readBytes = stream.Read(buffer, 0, bufferLength);
                    requestStream.Write(buffer, 0, readBytes);
                    count += readBytes;
                }
                while (readBytes != 0);
                //Console.WriteLine("Writing {0} bytes to the stream.", count);
                // IMPORTANT: Close the request stream before sending the request.
                requestStream.Close();

                //正式将指定的流写到FTP服务器指定的文件夹当中
                // Asynchronously get the response to the upload request.
                state.Request.BeginGetResponse(
                    new AsyncCallback(EndGetResponseCallback),
                    state
                );
            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        // The EndGetResponseCallback method  
        // completes a call to BeginGetResponse.
        private void EndGetResponseCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState)ar.AsyncState;
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)state.Request.EndGetResponse(ar);
                response.Close();//所有的流操作正式结束
                state.StatusDescription = response.StatusDescription;
                // Signal the main application thread that 
                // the operation is complete.
                state.OperationComplete.Set();//操作完成
                wxFileName = response.ResponseUri.Segments[response.ResponseUri.Segments.Length - 1].ToString();
                Url = wxserverurl + "//" + dirname + "//" + filename.Substring(filename.LastIndexOf("/") + 1);
                filename = Path;//前面有用到

            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
        }
        //创建目录
        public bool FtpMakeDir(string dirname)
        {

            Url = ftpurl + "//" + dirname;
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(Url);
            req.Credentials = new NetworkCredential(ftpuser, ftppwd);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception e)
            {
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }
    }
    //用于与FTP交互  
    public class FtpState
    {
        private ManualResetEvent wait;//线程相关
        private FtpWebRequest request;//请求相关
        private string fileName;//本地文件名称
        private Exception operationException = null;//错误信息
        string status;//状态信息

        public FtpState()
        {
            wait = new ManualResetEvent(false);
        }

        public ManualResetEvent OperationComplete
        {
            get { return wait; }
        }

        public FtpWebRequest Request
        {
            get { return request; }
            set { request = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public Exception OperationException
        {
            get { return operationException; }
            set { operationException = value; }
        }
        public string StatusDescription
        {
            get { return status; }
            set { status = value; }
        }
    }
}