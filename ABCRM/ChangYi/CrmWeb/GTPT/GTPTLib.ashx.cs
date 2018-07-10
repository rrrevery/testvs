using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using LoginServiceLib;
using BF.Pub;
using BF.CrmProc.GTPT;
using System.Configuration;
using System.IO;
using System.Text;
using System.Net;

namespace BF.CrmWeb.GTPT
{
    public class GTPTLib : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string PUBLICID = "";
                string PUBLICIF = "";

                if (context.Request["PUBLICID"] != null)
                {
                    PUBLICID = context.Request["PUBLICID"].ToString();
                }
                if (context.Request["PUBLICIF"] != null)
                {
                    PUBLICIF = context.Request["PUBLICIF"].ToString();
                }
                string msg = string.Empty, outdata = string.Empty;
                string updateValue = "";
                if (context.Request["requestType"] == "postToWX")
                {
                    //微信相关token等都直接发送到微信程序接口 统一一个获取token接口
                    string serverUrl = PUBLICIF + "?func=" + context.Request["func"];
                    outdata = SendHttpPostRequest(out msg, serverUrl, "");

                    if (msg == "")
                    {
                        
                        Token1 oToken = new Token1();
                        oToken = JsonConvert.DeserializeObject<Token1>(outdata);
                        context.Response.Write(oToken.result);
                    }
                    else
                    {
                        context.Response.Write(msg);
                    }
                    return;
                }


            }
            catch (Exception e)
            {
                string str = e.Message;
                context.Response.Write("错误:" + str);
                return;
            }
        }
        public string SendHttpPostRequest(out string msg, string url, string date)
        {
            msg = "";
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
                return responseFromServer.ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return "-1";
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    public class Token1
    {
        public string errCode = string.Empty;
        public string errMessage = string.Empty;
        public string result = string.Empty;
    }
}