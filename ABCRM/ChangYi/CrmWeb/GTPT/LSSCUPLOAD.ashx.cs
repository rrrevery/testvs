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
using System.Web.UI;
using BF.Pub;
using System.Data.Common;
using Newtonsoft.Json.Linq;

namespace BF.CrmWeb.GTPT
{
    /// <summary>
    /// LSSCUPLOAD 的摘要说明
    /// </summary>
    public class LSSCUPLOAD : IHttpHandler
    {


        public string filenameA;


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

             if (context.Request["filenameA"] != null)
            {
          filenameA = context.Request["filenameA"].ToString();
            }
            var file =context.Request.Files[0];

            HttpPostedFile files = context.Request.Files[0];

            string newFile = DateTime.Now.ToString("yyyyMMddHHmmss");
            //Access_token model = new Access_token();
            //model = pub.Check_Token();
            string PUBLICIF = "http://wxwh.oysd.cn/SaveWeChatData.ashx";
            WX.WX_Token wxproc = new WX.WX_Token();
            string token = wxproc.getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(token);


            string type = "voice";
            string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", oToken.result, type.ToString());


 //这里是个坑，临时文件不能上传，需要保存到某个路径下再上传----------

string path = "G://bs//CRM-BS//BSProject//新版2017//ChangYi//image//" + files.FileName;
            //服务器上的UpLoadFile文件夹必须有读写权限　　　　　　
files.SaveAs(path);

            string filename = System.Web.HttpContext.Current.Server.MapPath("/image/" + files.FileName);

            //string filename = filenameA;



            string json = HttpUploadFile(url, filename);
            JObject jb = (JObject)JsonConvert.DeserializeObject(json);//这里就能知道返回正确的消息了下面是个人的逻辑我就没写

        }

        public static string HttpUploadFile(string url, string path)//这个方法是两个URL第一个url是条到微信的，第二个是本地图片路径
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "POST";
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线
            request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

            int pos = path.LastIndexOf("\\");
            string fileName = path.Substring(pos + 1);

            //请求头部信息 
            StringBuilder sbHeader = new StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", fileName));
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] bArr = new byte[fs.Length];
            fs.Read(bArr, 0, bArr.Length);
            fs.Close();

            Stream postStream = request.GetRequestStream();
            postStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            postStream.Write(bArr, 0, bArr.Length);


            postStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            postStream.Close();

            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream instream = response.GetResponseStream();
            StreamReader sr = new StreamReader(instream, Encoding.UTF8);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            return content;
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