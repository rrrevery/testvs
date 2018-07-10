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
using System.Collections;

namespace BF.CrmWeb.GTPT
{
    /// <summary>
    /// UpLoadMediaToWXServer 的摘要说明  //上传永久素材 图片（image）、语音（voice）、视频（video）和缩略图（thumb）从公众号获取media_id
    /// </summary>
    public class UpLoadMediaToWXServer : IHttpHandler
    {
        public class WX_Mediaid
        {
            public string type = string.Empty;
            public string media_id = string.Empty;
            public string created_at = string.Empty;
            public int errcode = 0;
            public string errmsg = string.Empty;
            public string thumb_media_id = string.Empty;
            public string url = string.Empty;
        }

        public class VideoInfo
        {
            public string title = string.Empty;
            public string introduction = string.Empty;
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

        public class HttpRequestClient
        {
            private ArrayList bytesArray;
            private Encoding encoding = Encoding.UTF8;
            private string boundary = String.Empty;
            public HttpRequestClient()
            {
                bytesArray = new ArrayList();
                string flag = DateTime.Now.Ticks.ToString("x");
                boundary = "---------------------------" + flag;
            }

            /// <summary>  
            /// 设置表单数据字段  
            /// </summary>  
            /// <param name="fieldName">字段名</param>  
            /// <param name="fieldValue">字段值</param>  
            /// <returns></returns>  
            public void SetFieldValue(String fieldName, String fieldValue)
            {
                string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n";
                string httpRowData = String.Format(httpRow, fieldName, fieldValue);

                bytesArray.Add(encoding.GetBytes(httpRowData));
            }  
            /// <summary>  
            /// 设置表单文件数据  
            /// </summary>  
            /// <param name="fieldName">字段名</param>  
            /// <param name="filename">字段值</param>  
            /// <param name="contentType">内容内型</param>  
            /// <param name="fileBytes">文件字节流</param>  
            /// <returns></returns>  
            public void SetFieldValue(String fieldName, String filename, String contentType, Byte[] fileBytes)
            {
                string end = "\r\n";
                string httpRow = "--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
                string httpRowData = String.Format(httpRow, fieldName, filename, contentType);

                byte[] headerBytes = encoding.GetBytes(httpRowData);
                byte[] endBytes = encoding.GetBytes(end);
                byte[] fileDataBytes = new byte[headerBytes.Length + fileBytes.Length + endBytes.Length];

                headerBytes.CopyTo(fileDataBytes, 0);
                fileBytes.CopyTo(fileDataBytes, headerBytes.Length);
                endBytes.CopyTo(fileDataBytes, headerBytes.Length + fileBytes.Length);

                bytesArray.Add(fileDataBytes);
            }
            /// <summary>  
            /// 合并请求数据  
            /// </summary>  
            /// <returns></returns>  
            private byte[] MergeContent()
            {
                int length = 0;
                int readLength = 0;
                string endBoundary = "--" + boundary + "--\r\n";
                byte[] endBoundaryBytes = encoding.GetBytes(endBoundary);

                bytesArray.Add(endBoundaryBytes);

                foreach (byte[] b in bytesArray)
                {
                    length += b.Length;
                }

                byte[] bytes = new byte[length];

                foreach (byte[] b in bytesArray)
                {
                    b.CopyTo(bytes, readLength);
                    readLength += b.Length;
                }

                return bytes;
            }
            /// <summary>  
            /// 上传  
            /// </summary>  
            /// <param name="requestUrl">请求url</param>  
            /// <param name="responseText">响应</param>  
            /// <returns></returns>  
            public bool Upload(String requestUrl, out String responseText)
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);

                byte[] responseBytes;
                byte[] bytes = MergeContent();

                try
                {
                    responseBytes = webClient.UploadData(requestUrl, bytes);
                    responseText = System.Text.Encoding.UTF8.GetString(responseBytes);
                    return true;
                }
                catch (WebException ex)
                {
                    Stream responseStream = ex.Response.GetResponseStream();
                    responseBytes = new byte[ex.Response.ContentLength];
                    responseStream.Read(responseBytes, 0, responseBytes.Length);
                }
                responseText = System.Text.Encoding.UTF8.GetString(responseBytes);
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string PUBLICIF = "";
            string postData = "";
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

                //将文件转化为字节
                FileStream fs = new FileStream(savePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] fileBytes = new byte[fs.Length];
                fs.Read(fileBytes, 0, fileBytes.Length);
                fs.Close();
                fs.Dispose();

                //获取token
                WX.WX_Token wxproc = new WX.WX_Token();
                string token = wxproc.getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(token);

                WebClient wc = new WebClient();
                wc.Encoding = ASCIIEncoding.UTF8;
                var MediaUrl="";
                string tp_media = "";
                if (fileType == "newsimg")  //图文图片接口 只返回图片地址
                {
                    MediaUrl = "https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token=";
                    byte[] result = wc.UploadFile(new Uri(String.Format(MediaUrl + "{0}", oToken.result)), savePath);
                    tp_media = Encoding.Default.GetString(result);
                }
                else //image voice video thumb
                {
                    MediaUrl ="https://api.weixin.qq.com/cgi-bin/material/add_material?access_token=";
                    string newUri = Convert.ToString(new Uri(String.Format(MediaUrl + "{0}&type={1}", oToken.result, fileType)));

                    //设置表单数据格式
                    HttpRequestClient httpRequestClient = new HttpRequestClient();
                    if (fileType == "video")  //上传视频需要传title、description
                    {
                        VideoInfo ObjVideo = JsonConvert.DeserializeObject<VideoInfo>(postData);
                        httpRequestClient.SetFieldValue("title", ObjVideo.title);//发送数据  
                        httpRequestClient.SetFieldValue("introduction", ObjVideo.introduction);//发送数据 
                    }
                    httpRequestClient.SetFieldValue("media", savePath, "application/octet-stream", fileBytes);//发送文件数据
                    string responseText = string.Empty;
                    httpRequestClient.Upload(newUri, out responseText);  //请求  responseText是返回结果
                    tp_media = responseText;

                }


                WX_Mediaid tp_mediaid = new WX_Mediaid();
                tp_mediaid = JsonConvert.DeserializeObject<WX_Mediaid>(tp_media);
                if (tp_mediaid.errcode != 0)
                {
                    NoteObject.errCode = 2;
                    NoteObject.errMessage = "上传微信服务器：" + tp_mediaid.errmsg;
                    context.Response.Write(JsonConvert.SerializeObject(NoteObject));

                }
                else
                {

                    if (fileType == "newsimg")
                    {
                        ImgMsg imgObject = new ImgMsg();
                        imgObject.error = 0;
                        imgObject.url = tp_mediaid.url;
                        context.Response.Write(JsonConvert.SerializeObject(imgObject));
                        // NoteObject.result = tp_mediaid.url;
                        //context.Response.Write(NoteObject.result);
                    }
                    else 
                    {
                        NoteObject.result = tp_mediaid.media_id;
                        context.Response.Write(JsonConvert.SerializeObject(NoteObject));

                    }

                }
                //context.Response.Write(JsonConvert.SerializeObject(NoteObject));
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

        public class ImgMsg
        {
            public int error { get; set; }
            public string url { get; set; }
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