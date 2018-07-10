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
    /// GTPT_WXKBHYKTFSZ 的摘要说明
    /// </summary>
    public class GTPT_WXKBHYKTFSZ : IHttpHandler
    {

        public class QR_CARD
        {
            public string action_name = "QR_CARD";
            public action_info action_info = new action_info();
        }
        public class action_info
        {
            public card card = new card();
        }
        public class card
        {
            public string card_id = string.Empty;
        }

        public class WX_Mediaid
        {
            public string errCode = string.Empty;
            public string errMessage = string.Empty;
            public string result = string.Empty;
            public string content = string.Empty;
            public string show_qrcode_url = string.Empty;
        }

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
            string msg = "";
            string PUBLICIF = "";
            string postData = "";
            string cardjson = "";
            if (context.Request["PUBLICIF"] != null)
            {
                PUBLICIF = context.Request["PUBLICIF"].ToString();
            }
            if (context.Request["postData"] != null)
            {
                postData = context.Request["postData"].ToString();
            }
            PUBLICIF = context.Request["PUBLICIF"].ToString();
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/html; charset=utf-8";
            //返回提示
            ErrMsg NoteObject = new ErrMsg();
            string outpost = string.Empty;
            //取出file上传文件
            var MediaUrl = "";
            card ObjVideo = JsonConvert.DeserializeObject<card>(postData);
            WX.WX_Token wxproc = new WX.WX_Token();
            string token = wxproc.getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(token);
            switch (context.Request["func"])
            {
                case "QRCODE":
                    //MediaUrl = "https://api.weixin.qq.com/card/qrcode/create?access_token=";
                    MediaUrl = String.Format("https://api.weixin.qq.com/card/qrcode/create?access_token={0}", oToken.result);
                   
                    QR_CARD cardinfo = new QR_CARD();
                    cardinfo.action_info.card.card_id = ObjVideo.card_id;
                    cardjson = JsonConvert.SerializeObject(cardinfo);

                    break;
                case "CONTENT":
                    MediaUrl = String.Format("https://api.weixin.qq.com/card/mpnews/gethtml?access_token={0}", oToken.result);
                    cardjson = JsonConvert.SerializeObject(ObjVideo);

                    break;
                default:
                    break;

            }
            bool isok = SendHttpPostRequest(out msg, MediaUrl, cardjson, out outpost);
            WriteToLog("{"+MediaUrl + oToken.result + "}\r\nresult:" + outpost);
            WX_Mediaid oCard = new WX_Mediaid();
            oCard = JsonConvert.DeserializeObject<WX_Mediaid>(outpost);
            //tp_mediaid = JsonConvert.DeserializeObject<WX_Mediaid>(tp_media);

            context.Response.Write(JsonConvert.SerializeObject(oCard));

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
        public static bool SendHttpPostRequest(out string msg, string url, string date, out string getData)
        {
            msg = "";
            getData = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);

                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(date);
                request.ContentType = "application/x-gzip";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                getData = responseFromServer.ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message;

            }
            return msg.Length == 0;
        }

    }
}