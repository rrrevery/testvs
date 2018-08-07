using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.CrmProc;

using System.Net;
using System.Text;
using System.IO;
using BF.Pub;
using System.Data;
using System.Data.Common;
using System.Security;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace BF.CrmWeb.GTPT.WX
{
    /// <summary>
    /// 与微信进行交互的基类
    /// </summary>
    public class WX_Base
    {

        public string dir = "";//存放上传文件的目录
        public string Path = "";//存放本地图片img src的值
        public string Url = string.Empty;
        public string Token = "";
        //public string Token = (new WX_Token()).getToken();
        public string method = "GET";
        public string postData = "";
        public static List<Byte[]> buffer = new List<byte[]>();
        public string mode = "search";
        //public string returnString = "";
        protected string returnString = "";
        public string filename = "";//文件名称(保存在此服务器当中)，最后返回给前台文:=Path
        public string fullFileName = "";//本地文件全称（包括路径）,如果传了这个参数过来，就不再上传文件到此服务器，为素材库而备
        public string wx_filename = "";//文件名称（保存在微信服务器当中,为微信网页内容的正常显示而备,一般只是用来存放网页上的 img src）
        public WX_Base()
        {
            //WX_Token wt = new WX_Token();
            //Token = wt.getToken();

            //Token = "0_Hk1hv_FKqAxkin2CKGe6tTt1rbuST6Wds2nM7q3c_R4ZetD1eaUxZx6QHmnLviHoCPt9EEeZH_qD-du-IcyKSmlb_ASfJg8649GWXYjnc";
        }

        protected string GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return FormatUtils.DatetimeToString(dtStart.Add(toNow));
        }
        /// <summary>
        /// 处理http请求
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string WXRequest(out string msg, string PUBLICID, string PUBLICIF, string mode, string updateValue, string iDJR, string sDJRMC, HttpContext context = null)
        {
            msg = "";
            string result = "";          
            try
            {
                switch (mode.ToLower())
                {
                    case "search":
                    case "download":
                        result = SearchDatatoWX(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        break;
                    case "insert":
                    case "upload":
                    case "send":
                        result = InsertData(out msg, context);
                        break;
                    case "save":
                        result = SaveDataWX(out msg, context);
                        break;
                    case "update":
                        result = UpdateData(out msg, context);
                        break;
                    case "delete":
                        result = DeleteData(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        break;
                    case "deletenews":
                        result = DeleteNews(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        break;
                    case "previewmassmsg":
                        result = PreviewMassMsg(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        break;
                    case "createtag":

                        TagJson TagJson = CreateTag(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        result = TagJson.name;
                        break;
                    case "updatetag":
                        TagJsonBJ TagJsonbj = UpdateTag(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        result = TagJsonbj.errmsg;

                        break;

                    case "deletetag":
                        TagJsonBJ TagJsonsc = DeleteTag(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        result = TagJsonsc.errmsg;

                        break;

                    case "gettaglist":

                        List<TagJsonBQ> TagJsonBQlist = GetTagList(out msg, PUBLICID, PUBLICIF, updateValue, context);

                        var postData = JsonConvert.SerializeObject(TagJsonBQlist);

                        result = postData;

                        break;

                    case "getfslist":

                        TagJsonFS TagJsonFSlist = GetFSList(out msg, PUBLICID, PUBLICIF, updateValue, context);

                        var postDatafs = JsonConvert.SerializeObject(TagJsonFSlist.data);

                        result = postDatafs;

                        break;

                    case "getyhxxlist":

                        List<TagJsonYHXX> TagJsonYHXXlist = GetYHXXList(out msg, PUBLICID, PUBLICIF, updateValue, context);

                        var postDatayhxx = JsonConvert.SerializeObject(TagJsonYHXXlist);

                        result = postDatayhxx;

                        break;
                        //打标签
                    case "pldbqtag":
                        TagJsonBJ TagJsonPLDBQ = PLDBQTag(out msg, PUBLICID, PUBLICIF, updateValue,iDJR,sDJRMC, context);
                        result = TagJsonPLDBQ.errmsg;

                        break;
                    //取消打标签
                    case "plqxbqtag":
                        TagJsonBJ TagJsonPLQXBQ = PLQXBQTag(out msg, PUBLICID, PUBLICIF, updateValue, iDJR, sDJRMC,context);
                        result = TagJsonPLQXBQ.errmsg;

                        break;
                    case "xgbz":
                        TagJsonBJ TagJsonXGBZ = XGBZ(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        result = TagJsonXGBZ.errmsg;

                        break;

                    case "addhmd":
                        TagJsonBJ TagJsonADDHMD = ADDHMD(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        result = TagJsonADDHMD.errmsg;

                        break;
                    case "delhmd":
                        TagJsonBJ TagJsonDELHMD = DELHMD(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        result = TagJsonDELHMD.errmsg;
                        break;
                    case "getqbfslist":

                        TagJsonQBFS TagJsonQBFSlist = GetQBFSList(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        var postDataQBfs = JsonConvert.SerializeObject(TagJsonQBFSlist.data);
                        result = postDataQBfs;
                        break;

                    case "showhmd":
                        TagJsonHMD TagJsonHMDlist = SHOWHMD(out msg, PUBLICID, PUBLICIF, updateValue, context);
                        var postDataHMD = JsonConvert.SerializeObject(TagJsonHMDlist.data);
                        result = postDataHMD;
                        break;
                    default:
                        msg = "请在程序当中指定mode值!";
                        break;
                }
                if (msg != "")
                {
                    msg = "错误：" + msg;
                    return "";
                }
                return result;
            }
            catch (Exception e)
            {
                msg = "错误:" + e.Message;
                return "";
            }
        }

        /// <summary>
        /// 普通的GET 请求，以及POST上传文件
        /// </summary>
        /// <param name="msg">记录错误信息</param>
        /// <param name="context">请求内容</param>
        /// <param name="pdata">请求字符串</param>
        /// <param name="p_postdta">是否加上请求字符串</param>
        /// <returns></returns>
        /// 


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
        private void WriteLog(string p)
        {
            throw new NotImplementedException();
        }
        // 添加https  
        //添加证书信任
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受       
        }
        public string WXRequestString(out string msg, HttpContext context = null, string pdata = "", bool p_postdta = true)
        {
            if (pdata == "" && method.ToUpper() == "POST" && p_postdta == true)
            {
                postData = JsonConvert.SerializeObject(this);
            }
            else
            {
                postData = pdata;
            }

            Pub.Log4Net.WriteLog(LogLevel.INFO, postData);

            //可以使用  WebReqeust和WebResponse 代替WebClient  
            msg = "";
            try
            {
                //分为两种方式  get  post  其中get只从微信 获取信息 post可以向其发送数据，但是post 又可以发送简单的文本信息，还可以上传图片等其它文件
                WebClient wc = new WebClient();
                wc.Encoding = ASCIIEncoding.UTF8;
                Byte[] responseData = null;
                Byte[] data = null;//发送的数据
                StreamReader sr = null;
                if (method.ToUpper() == "GET")
                {
                    //responseData = wc.DownloadData(Url);                    
                    //WebHeaderCollection cc = wc.ResponseHeaders;
                    Stream stream = wc.OpenRead(Url);
                    sr = new StreamReader(stream);
                }
                else if (method.ToUpper() == "POST")
                {
                    //wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");   
                    if (buffer.Count == 0)
                    {
                        data = Encoding.UTF8.GetBytes(postData);//此处要注意转换成UTF-8 
                        //ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
                        ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                        responseData = wc.UploadData(Url, "POST", data);


                        Pub.Log4Net.WriteLog(LogLevel.INFO, Url + "大家好1");
                        Pub.Log4Net.WriteLog(LogLevel.INFO, data + "大家好2");

                        Pub.Log4Net.WriteLog(LogLevel.INFO, responseData+"大家好3");


                        //其他日志都没用了 这里记录下URL和Data 还有这个responseData


                       
                    }
                    else
                    {
                        string name = context.Request.Form["name"].ToString();
                        filename = saveFileResource(out msg, name.Substring(name.LastIndexOf('.') + 1));
                        if (msg != "")
                        {
                            return msg;
                        }
                        responseData = wc.UploadFile(Url, filename);
                        filename = Path;
                    }

                    MemoryStream ms = new MemoryStream(responseData);
                    sr = new StreamReader(ms, ASCIIEncoding.UTF8);
                }
                String wxString = sr.ReadToEnd();
                //然后就是这条日志  就我写中文这两个地址比较重要你加上吧其他没用了
                Pub.Log4Net.WriteLog(LogLevel.INFO,wxString) ;


                Pub.Log4Net.WriteLog(LogLevel.INFO,"我们好");




                HYKGL_WX_ERROR wxerror = JsonConvert.DeserializeObject<HYKGL_WX_ERROR>(wxString);


                if (wxerror.errcode > -2)
                {
                    if (wxerror.errcode == 42001 || wxerror.errcode == 40001)
                    {

                        //重新获取一下Token
                    }
                    int index = -1;

                    Pub.Log4Net.WriteLog(LogLevel.INFO, wxString);


                    //使用二分查找  待做
                    for (int i = 0; i < HYKGL_WX_ERROR.errcodeArr.Length; i++)
                    {
                        if (wxerror.errcode == HYKGL_WX_ERROR.errcodeArr[i])
                        {
                            index = i;

                            break;
                        }
                    }
                    if (index != -1 && index != 1)
                    {

                        msg = HYKGL_WX_ERROR.errmsgArr[index];
                    }
                    return msg;
                }

                Pub.Log4Net.WriteLog(LogLevel.INFO, wxString);

                Pub.Log4Net.WriteLog(LogLevel.INFO, "返回值");

                return wxString;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return msg;
            }
        }

       //接口返回json
        public string WXRequestStringResult(out string msg, HttpContext context = null, string pdata = "", bool p_postdta = true)
        {
            if (pdata == "" && method.ToUpper() == "POST" && p_postdta == true)
            {
                postData = JsonConvert.SerializeObject(this);
            }
            else
            {
                postData = pdata;
            }

            Pub.Log4Net.WriteLog(LogLevel.INFO, postData);

            //可以使用  WebReqeust和WebResponse 代替WebClient  
            msg = "";
            try
            {
                //分为两种方式  get  post  其中get只从微信 获取信息 post可以向其发送数据，但是post 又可以发送简单的文本信息，还可以上传图片等其它文件
                WebClient wc = new WebClient();
                wc.Encoding = ASCIIEncoding.UTF8;
                Byte[] responseData = null;
                Byte[] data = null;//发送的数据
                StreamReader sr = null;
                if (method.ToUpper() == "GET")
                {
                    //responseData = wc.DownloadData(Url);                    
                    //WebHeaderCollection cc = wc.ResponseHeaders;
                    Stream stream = wc.OpenRead(Url);
                    sr = new StreamReader(stream);
                }
                else if (method.ToUpper() == "POST")
                {
                    //wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");   
                    if (buffer.Count == 0)
                    {
                        data = Encoding.UTF8.GetBytes(postData);//此处要注意转换成UTF-8 
                        //ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
                        ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
                        responseData = wc.UploadData(Url, "POST", data);
                    }
                    else
                    {
                        string name = context.Request.Form["name"].ToString();
                        filename = saveFileResource(out msg, name.Substring(name.LastIndexOf('.') + 1));
                        if (msg != "")
                        {
                            return msg;
                        }
                        responseData = wc.UploadFile(Url, filename);
                        filename = Path;
                    }

                    MemoryStream ms = new MemoryStream(responseData);
                    sr = new StreamReader(ms, ASCIIEncoding.UTF8);
                }
                String wxString = sr.ReadToEnd();

                return wxString;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return msg;
            }
        }

        /// <summary>
        /// 下载文件数据并保存到本地，
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string WXDownloadFile(out string msg, string filetype, string type = "thumb")
        {
            msg = "";
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = ASCIIEncoding.UTF8;
                Stream sr = null;
                if (method.ToUpper() == "GET")
                {
                    wc.Encoding = ASCIIEncoding.UTF8;
                    sr = wc.OpenRead(Url);
                    byte[] temp = new byte[100];
                    int length = 100;
                    while ((length = sr.Read(temp, 0, length)) > 0)
                    {
                        temp = new byte[length];
                        buffer.Add(temp);
                    }
                    //filetype = "";
                    //switch (type)
                    //{
                    //    case "image":
                    //        filetype = "jpg";
                    //        break;
                    //    case "voice":
                    //        //filetype=""
                    //        break;
                    //    case "video":
                    //        break;
                    //    case "news":
                    //        break;
                    //}
                    filename = saveFileResource(out msg, "jpg", type == "" ? "thmub" : type);

                }



                return "";
            }
            catch (Exception e)
            {
                msg = e.Message;
                return msg;
            }
        }
        /// <summary>
        /// 获取文件上传字节流
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool GetBuffer(out string msg, HttpContext context)
        {
            msg = "";
            Byte[] tempbuffer = null;

            //读取前台传送的数据
            #region
            try
            {
                int chunk = Convert.ToInt32(context.Request["chunk"] ?? "-1");
                int totalChunks = Convert.ToInt32(context.Request["chunks"] ?? "0");
                //分块数据读取
                if (context.Request.ContentType == "application/octet-stream" && context.Request.ContentLength > 0)
                {
                    tempbuffer = new Byte[context.Request.InputStream.Length];
                    context.Request.InputStream.Read(tempbuffer, 0, tempbuffer.Length);
                }
                else if (context.Request.ContentType.Contains("multipart/form-data") && context.Request.Files.Count > 0 && context.Request.Files[0].ContentLength > 0)
                {
                    tempbuffer = new Byte[context.Request.Files[0].InputStream.Length];
                    context.Request.Files[0].InputStream.Read(tempbuffer, 0, tempbuffer.Length);
                }
                else
                {
                    return true;
                }
                //单独的完整文件上传完成
                buffer.Add(tempbuffer);
                if (chunk + 1 == totalChunks)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                msg = e.Message;
                return false;

            }
            #endregion
        }

        public virtual string SearchDatatoWX(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";

            return "";
        }
        public virtual string SearchData(out string msg, HttpContext context = null)
        {
            msg = "";

            return "";
        }
        public virtual string UpdateData(out string msg, HttpContext context = null)
        {
            msg = "";
            return "";
        }
        public virtual string InsertData(out string msg, HttpContext context = null)
        {
            msg = "";
            return "";
        }
        public virtual string SaveDataWX(out string msg, HttpContext context = null)
        {
            msg = "";
            return "";
        }
        public virtual string DeleteData(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context =null)
        {
            msg = "";

            return "";
        }
        public virtual string DeleteNews(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";

            return "";
        }

        public virtual string PreviewMassMsg(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";

            return "";
        }

        public virtual BF.CrmWeb.GTPT.WX.WX_GroupMessage.PostSuccessResult PostMassMsg(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }


        public virtual TagJson CreateTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }

        public virtual List<TagJsonBQ> GetTagList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        public virtual TagJsonBJ UpdateTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }


        public virtual TagJsonBJ DeleteTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }

        //得到某一个标签下的粉丝
        public virtual TagJsonFS GetFSList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        //得到全部的粉丝
        public virtual TagJsonQBFS GetQBFSList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        //得到黑名单
        public virtual TagJsonHMD SHOWHMD(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        //用户信息
        public virtual List<TagJsonYHXX> GetYHXXList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        //批量打标签
        public virtual TagJsonBJ PLDBQTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, string iDJR, string sDJRMC, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        //批量取消标签
        public virtual TagJsonBJ PLQXBQTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, string iDJR, string sDJRMC, HttpContext context = null)
        {
            msg = "";
            return null;
        }


        //修改备注
        public virtual TagJsonBJ XGBZ(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }

        //加入黑名单
        public virtual TagJsonBJ ADDHMD(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }

        //移出黑名单
        public virtual TagJsonBJ DELHMD(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            return null;
        }
        //文件流 保存到本地服务器当中
        public string saveFileResource(out string msg, string filetype = "jpg", string type = "")
        {
            msg = "";
            if (buffer.Count == 0)
            {
                return "";
            }
            FileStream outfile = null;
            try
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                if (type == "thumb" && filetype == "jpg")
                {
                    filetype = "news";
                }
                string year = serverTime.Year.ToString();
                string month = (serverTime.Month.ToString().CompareTo("10") > 0) ? serverTime.Month.ToString() : 0 + serverTime.Month.ToString();
                string day = (serverTime.Day.ToString().CompareTo("10") > 0) ? serverTime.Day.ToString() : 0 + serverTime.Day.ToString();
                string dirname = year + month + day;
                Path = "\\";//显示图片时使用绝对路径
                string subdir = "WX_Resources\\";
                switch (filetype.ToLower())
                {
                    case "text":
                        //dir += "Texts";

                        dir = "";
                        break;
                    case "jpg":
                        subdir += "Images\\" + dirname + "\\";//本地绝对路径
                        //保存到数据库本地
                        break;
                    case "amr":
                    case "mp3":
                        subdir += "Voices\\" + dirname + "\\";
                        break;
                    case "mp4":
                        subdir += "Videos\\" + dirname + "\\";
                        break;
                    case "news":
                        subdir += "News\\" + dirname + "\\";//图文消息的素材缩略图保存
                        filetype = "jpg";
                        break;
                }
                if (dir == "")
                {
                    return "";
                }
                Directory.CreateDirectory(dir + subdir);
                filename = serverTime.Ticks.ToString() + Convert.ToInt32(((new Random()).NextDouble() * 1000)).ToString() + "." + filetype;
                fullFileName = dir + subdir + filename;
                outfile = new FileStream(fullFileName, FileMode.Create);
                for (int i = 0; i < buffer.Count; i++)
                {
                    outfile.Write(buffer[i], 0, buffer[i].Length);
                }
                //素材详细保存到数据库本地
                Path += subdir;
                Path += filename;
                if (buffer.Count == 0)
                {
                    return "";
                }
                return fullFileName;//
            }
            catch (Exception e)
            {
                throw new Exception("文件上传失败!");
            }
            finally
            {
                outfile.Flush();
                outfile.Close();
            }

        }


        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        //internal class AcceptAllCertificatePolicy : ICertificatePolicy
        //{
        //    public AcceptAllCertificatePolicy()
        //    {
        //    }

        //    private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //    {
        //        return true;
        //    }
        //}
    }
    /// <summary>
    /// 错误消息类
    /// </summary>
    public class HYKGL_WX_ERROR
    {
        public int errcode = -2;
        public string errmsg = "";
        //1-1的关系
        public static int[] errcodeArr ={-1,0,40001,40002,40003,40004,40005,40006,40007,40008,40009,40010,40011,40012,
                                      40013,40014,40015,40016,40017,40018,40019,40020,40021,40022,40023,40024,40025,40026,40027,
                                      40028,40029,40030,40031,40032,40033,40035,40038,40039,40050,40051,41001,41002,
                                      41003,41004,41005,41006,41007,41008,41009,42001,42002,42003,43001,43002,43003,43004,43005,
                                      44001,44002,44003,44004,45001,45002,45003,45004,45005,45006,45007,45008,45009,45010,45015,
                                      45016,45017,45018,46001,46002,46003,46004,47001,48001,50001,45065,45066,45067,40130};
        public static string[] errmsgArr ={"系统繁忙","请求成功","获取access_token时AppSecret错误，或者access_token无效 ","不合法的凭证类型","不合法的OpenID ",
                                  "不合法的媒体文件类型","不合法的文件类型","不合法的文件大小","不合法的媒体文件id","不合法的消息类型 ","不合法的图片文件大小",
                                  "不合法的语音文件大小","不合法的视频文件大小 ","不合法的缩略图文件大小","不合法的APPID","不合法的access_token","不合法的菜单类型","不合法的按钮个数",
                                  "不合法的按钮个数 ","不合法的按钮名字长度","不合法的按钮KEY长度","不合法的按钮URL长度 ","不合法的菜单版本号 ",
                                  "不合法的子菜单级数 ","不合法的子菜单按钮个数 ","不合法的子菜单按钮类型 ","不合法的子菜单按钮名字长度","不合法的子菜单按钮KEY长度 ",
                                  "不合法的子菜单按钮URL长度 ","不合法的自定义菜单使用用户 ","不合法的oauth_code ","不合法的refresh_token ",
                                  "不合法的openid列表 ","不合法的openid列表长度 ","不合法的请求字符，不能包含\\uxxxx格式的字符","不合法的参数",
                                  "不合法的请求格式","不合法的URL长度 ","不合法的分组id ","分组名字不合法 ","缺少access_token参数 ","缺少appid参数 ","缺少refresh_token参数 ",
                                  "缺少secret参数 ","缺少多媒体文件数据 ","缺少media_id参数 ","缺少子菜单数据 ","缺少oauth code ","缺少openid ",
                                  "access_token超时 ","refresh_token超时 ","oauth_code超时","需要GET请求 ","需要POST请求","需要HTTPS请求 ","需要接收者关注",
                                  "需要好友关系 ","多媒体文件为空 ","POST的数据包为空","图文消息内容为空","文本消息内容为空 ","多媒体文件大小超过限制",
                                  "消息内容超过限制 ","标题字段超过限制","描述字段超过限制","链接字段超过限制","图片链接字段超过限制","语音播放时间超过限制",
                                  "图文消息超过限制 ","接口调用超过限制","创建菜单个数超过限制","回复时间超过限制 ","系统分组，不允许修改","分组名字过长","分组数量超过上限","不存在媒体数据",
                                  "不存在的菜单版本","不存在的菜单数据","不存在的用户","解析JSON/XML内容错误","api功能未授权","用户未授权该api","相同 clientmsgid 已存在群发记录","相同 clientmsgid 重试速度过快，请间隔1分钟重试","clientmsgid 长度超过限制","至少要添加两条用户信息"};

    }
}