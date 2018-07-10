using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;
using System.Web.UI;
using System.Net;
using System.Security.Cryptography;
using System.Xml;

namespace BF.CrmWeb.GTPT.WX
{
    /// <summary>
    /// access_tokken的相关操作
    /// </summary>
    public class WX_Token
    {
        public string getToken(string PUBLICIF)
        {
            string Token = string.Empty;
            //微信相关token等都直接发送到微信程序接口 统一一个获取token接口
            
                //string serverUrl1 = ConfigurationManager.AppSettings["WeChatSeverAddress1"];
                string msg = string.Empty;
                Token = SendHttpGetRequest(out msg, PUBLICIF);
            
            
            return Token;
        }

        //post json
       
        public string SendHttpGetRequest(out string msg, string url)
        {
            msg = "";
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
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
        
    }
}