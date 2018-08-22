using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using System.Net;
using System.Text;
using System.IO;
using BF.CrmWeb.GTPT.WX;
using System.Web.UI;
using System.Configuration;

namespace BF.CrmWeb.GTPT
{
    /// <summary>
    /// GTPT_WXBQ 的摘要说明
    /// </summary>
    public class GTPT_WXBQ : System.Web.UI.Page, IHttpHandler
    {
        public string dir = "";//用来获得程序目录绝对路径

        public override void ProcessRequest(HttpContext context)
        {
            dir = Server.MapPath("~");//项目根目录
            string requestType = "";//哪个相关操作  比如 用户信息  组信息  后期用在基类中的字段代替
            string postData = "";

            string result = "";
            string msg = "";
            string PUBLICIF = "";
            string PUBLICID = "";
            string mode = "";
            string tagid = "";
            string iDJR = "";
            string sDJRMC= "";
         
            if (context.Request["PUBLICID"] != null)
            {
                PUBLICID = context.Request["PUBLICID"].ToString();

            }
            if (context.Request["PUBLICIF"] != null)
            {
                PUBLICIF = context.Request["PUBLICIF"].ToString();
            }
            if (context.Request["requestType"] != null)
            {
                requestType = context.Request["requestType"].ToString();
            }
            if (context.Request["mode"] != null)
            {
                mode = context.Request["mode"].ToString();

            }
            if (context.Request["iDJR"] != null)
            {
                iDJR = context.Request["iDJR"].ToString();

            }
            if (context.Request["sDJRMC"] != null)
            {
                sDJRMC = context.Request["sDJRMC"].ToString();



            }
      
            if (context.Request["postData"] != null)
            {
                //postData 可以在前台定义好与类对应的格式，并将URL 和 Type设置好
                postData = context.Request["postData"].ToString();
            }

            else
            {
                postData = "{\"postData\":\"\"}";
            }
            string updateValue = "";

            context.Request.ContentType = "text/plain";
            context.Request.ContentType = "text/html; charset=utf-8";

            if (context.Request["name"] != null && context.Request["name"]!="")

            {

                updateValue = context.Request["name"].ToString()
;




            }
            else if (context.Request["id"] != null && context.Request["id"] != "")
            {
                updateValue = context.Request["id"].ToString();
            }

            else if (context.Request["next_openid"] != null && context.Request["next_openid"] != "")
            {
                updateValue = context.Request["next_openid"].ToString();
            }

           else{
            updateValue = context.Request.Params["json"].Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}");
               
          }




            WX_Base wxbase = new WX_Base();

            switch (requestType.ToLower())
            {
                case "post":
                    wxbase = JsonConvert.DeserializeObject<WX_Menu>(postData);
                    break;
                case "groups"://分组

                    //几个主要需要设置的值 (除了URL ，其它都最好在postData当中设置好)
                    //1、 微信URL  暂时在后台函数当中写，更为简单,除了这个参数，以下4个都得在请求程序当中写好
                    //2、 postData字符串的发送数据   
                    //3、buff二进制格式的发送数据   保存上传过来的二进制
                    //4、method 请求方式  目前 只有两种 get post 前台控制 ，默认get 
                    //5、mode  指定  增  删  改 查  导入   

                    wxbase = JsonConvert.DeserializeObject<WX_Group>(postData);
                    break;
                case "users"://用户
                    wxbase = JsonConvert.DeserializeObject<WX_GRXX>(postData);
                    break;
                case "usergroup"://用户分组
                    wxbase = JsonConvert.DeserializeObject<WX_UserGroup>(postData);
                    break;
                case "media":
                    wxbase = JsonConvert.DeserializeObject<WX_Media>(postData);
                    break;
                case "image_text":
                case "textmessage":
                case "video":
                case "vioce":
                case "massmessage"://群发
                    postData = System.Web.HttpUtility.UrlDecode(postData).Replace("#$~@#$%*#e", "'");
                    wxbase = JsonConvert.DeserializeObject<WX_Message>(postData);
                    //将前台的全改成一个
                    break;
                case "cusmessage":
                    postData = System.Web.HttpUtility.UrlDecode(postData).Replace("#$~@#$%*#e", "'");
                    wxbase = JsonConvert.DeserializeObject<WX_CustomerMessage>(postData);
                    break;
                case "wxftp":
                    wxbase = JsonConvert.DeserializeObject<WX_FTP>(postData);
                    break;
                case "menu":
                    wxbase = JsonConvert.DeserializeObject<WX_Menu>(postData);
                    break;
                default:
                    msg = "请在程序当中指定requestType值!";
                    break;

            }
            wxbase.dir = dir;
            //与微信交互
         result  =   wxbase.WXRequest(out msg, PUBLICID, PUBLICIF,mode,updateValue,iDJR,sDJRMC, context);
         //   = result_temp.name;

            if (msg != "")
            {
                context.Response.Write(msg);

                return;
            }

            //WriteToLog(result);
            Pub.Log4Net.WriteLog(LogLevel.INFO, result);

            context.Response.Write(result);
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
     


       
    }
}