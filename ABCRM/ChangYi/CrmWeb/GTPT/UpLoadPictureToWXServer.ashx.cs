using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using BF.Pub;
namespace BF.CrmWeb.GTPT
{
    /// <summary>
    /// UpLoadPictureToWXServer 的摘要说明
    /// </summary>
    public class UpLoadPictureToWXServer : IHttpHandler
    {
        public class WX_Mediaid
        {
            public string type = string.Empty;
            public string media_id = string.Empty;
            public string created_at = string.Empty;
            public int errcode = 0;
            public string errmsg = string.Empty;
            public string thumb_media_id = string.Empty;
        }
        //时间戳函数
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;

        }

        public static void WriteToLog(string e)
        {
            DateTime timeEnd = DateTime.Now;
            DateTime timeBegin = DateTime.Now;
            StringBuilder logStr = new System.Text.StringBuilder();
            logStr.Append("\r\n--->St:").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(" Response error, ").Append(timeEnd.Subtract(timeBegin).Milliseconds).Append(" ms:");
            logStr.Append("\r\n").Append(e);
            logStr.Append("\r\n--->Ed:").Append(e);
            DailyLogFileWriter DataLogFileWriter = null;
            string logPath = ConfigurationManager.AppSettings["WeChatLogPath"];
            DataLogFileWriter = new DailyLogFileWriter(logPath, "LOG");
            DataLogFileWriter.Write(timeBegin.ToString("yyyy-MM-dd"), logStr.ToString());
            DataLogFileWriter.Close();
        }
        public void ProcessRequest(HttpContext context)
        {
            string PUBLICIF = "";//取人员商户代码
            if (context.Request["PUBLICIF"] != null)
            {
                PUBLICIF = context.Request["PUBLICIF"].ToString();
            }
            PUBLICIF = context.Request["PUBLICIF"].ToString();
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/html; charset=utf-8";
            //返回提示
            ErrMsg NoteObject = new ErrMsg();
            //取出file上传文件
            HttpPostedFile file = context.Request.Files[0];
            string fileType = context.Request.QueryString["type"];
            //取本地路径，先将文件上传到服务器（程序服务器）
            string mapPath = context.Server.MapPath("~");
            string path = mapPath + "\\WeiXin\\";
            if (file != null && file.ContentLength > 0)
            {
                int imagesKindInx = file.FileName.LastIndexOf(".");
                string fileNewName = ConvertDateTimeInt(DateTime.Now) + file.FileName.Substring(imagesKindInx, file.FileName.Length - imagesKindInx);
                string savePath = path + fileNewName;
                //本地目录不存在创建
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //本地命名重复存在删除
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                //保存文件在本地
                file.SaveAs(savePath);

                WX.WX_Token wxproc = new WX.WX_Token();
                string token = wxproc.getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(token);

                WebClient c = new WebClient();
            

                byte[] result = c.UploadFile(new Uri(String.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", oToken.result, fileType)), savePath);
                string tp_media = Encoding.Default.GetString(result);



                WriteToLog("url:http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={" + oToken.result + "}&type={" + fileType + "}\r\nresult:" + tp_media + "savePath:" + savePath);

                WX_Mediaid tp_mediaid = new WX_Mediaid();
                tp_mediaid = JsonConvert.DeserializeObject<WX_Mediaid>(tp_media);
                if (tp_mediaid.errcode != 0)
                {
                    NoteObject.errCode = 2;
                    NoteObject.errMessage = "上传微信服务器：" + tp_mediaid.errmsg;
                }
                else
                {
                    NoteObject.result = tp_mediaid.media_id;
                }
                context.Response.Write(JsonConvert.SerializeObject(NoteObject));
            }
        }

        public class ErrMsg
        {
            //错误码。0 表示正常返回，否则表示有异常
            public int errCode = 0;
            //错误说明。异常说明信息
            public string errMessage = string.Empty;
            //执行结果
            public object result = null;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}