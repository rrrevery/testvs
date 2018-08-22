using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Web;
using System.IO.Compression;
using System.Collections.Specialized;

namespace BF.CrmWeb.GTPT.WX
{
    public class HttpHelper
    {

        /// <summary>
        /// 发起http请求（POST）
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Data"></param>
        /// <returns></returns>
        public  string SendPost(string Url, string Data)
        {
            return Send(Url, "POST", Data);
        }
        public string SendPost(string Url)
        {
            return Send(Url, "POST");
        }
        /// <summary>
        /// 发起http请求（GET）
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string SendGet(string Url)
        {
            return Send(Url, "GET");
        }
        /// <summary>
        /// 发起请求
        /// </summary>
        /// <param name="url">目标地址</param>
        /// <param name="method">发起方式：GET/POST</param>
        /// <param name="data">发起时：附带数据</param>
        /// <param name="config">配置信息为空则默认配置</param>
        /// <returns>返回string</returns>
        public static string Send(string Url, string Method, string Data = null, HttpConfig Config = null)
        {
            if (Config == null)
            {
                Config = new HttpConfig();
            }
            string result;
            using (HttpWebResponse response = GetResponse(Url, Method, Data, Config))
            {
                Stream stream = response.GetResponseStream();

                if (!String.IsNullOrEmpty(response.ContentEncoding))
                {
                    if (response.ContentEncoding.Contains("gzip"))
                    {
                        stream = new GZipStream(stream, CompressionMode.Decompress);
                    }
                    else if (response.ContentEncoding.Contains("deflate"))
                    {
                        stream = new DeflateStream(stream, CompressionMode.Decompress);
                    }
                }

                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    int count;
                    byte[] buffer = new byte[4096];
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, count);
                    }
                    bytes = ms.ToArray();
                }

                #region 检测流编码
                Encoding encoding;

                //检测响应头是否返回了编码类型,若返回了编码类型则使用返回的编码
                //注：有时响应头没有编码类型，CharacterSet经常设置为ISO-8859-1
                if (!string.IsNullOrEmpty(response.CharacterSet) && response.CharacterSet.ToUpper() != "ISO-8859-1")
                {
                    encoding = Encoding.GetEncoding(response.CharacterSet == "utf8" ? "utf-8" : response.CharacterSet);
                }
                else
                {
                    //若没有在响应头找到编码，则去html找meta头的charset
                    result = Encoding.Default.GetString(bytes);
                    //在返回的html里使用正则匹配页面编码
                    Match match = Regex.Match(result, @"<meta.*charset=""?([\w-]+)""?.*>", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        encoding = Encoding.GetEncoding(match.Groups[1].Value);
                    }
                    else
                    {
                        //若html里面也找不到编码，默认使用utf-8
                        encoding = Encoding.GetEncoding(Config.CharacterSet);
                    }
                }
                #endregion

                result = encoding.GetString(bytes);
            }
            return result;
        }


        /// <summary>
        /// 获取目标网址的返回数据
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="Method"></param>
        /// <param name="Data"></param>
        /// <param name="Config"></param>
        /// <returns></returns>
        private static HttpWebResponse GetResponse(string Url, string Method, string Data, HttpConfig Config)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = Method;
            request.Referer = Config.Referer;
            //有些页面不设置用户代理信息则会抓取不到内容
            request.UserAgent = Config.UserAgent;
            request.Timeout = Config.Timeout;
            request.Accept = Config.Accept;
            request.Headers.Set("Accept-Encoding", Config.AcceptEncoding);
            request.ContentType = Config.ContentType;
            request.KeepAlive = Config.KeepAlive;

            if (Url.ToLower().StartsWith("https"))
            {
                //这里加入解决生产环境访问https的问题--Could not establish trust relationship for the SSL/TLS secure channel
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);
            }

            if (Method.ToUpper() == "POST")
            {
                if (!string.IsNullOrEmpty(Data))
                {
                    byte[] DateBytes = Encoding.UTF8.GetBytes(Data);

                    if (Config.GZipCompress)
                    {
                        using (MemoryStream MRStream = new MemoryStream())
                        {
                            using (GZipStream gZipStream = new GZipStream(MRStream, CompressionMode.Compress))
                            {
                                gZipStream.Write(DateBytes, 0, DateBytes.Length);
                            }
                            DateBytes = MRStream.ToArray();
                        }
                    }

                    request.ContentLength = DateBytes.Length;
                    request.GetRequestStream().Write(DateBytes, 0, DateBytes.Length);
                }
                else
                {
                    request.ContentLength = 0;
                }
            }

            return (HttpWebResponse)request.GetResponse();
        }

        /// <summary>
        /// 解决https 生产环境无法为SSL/TLS安全信道建立信任关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //用户https请求
            return true; //总是接受
        }
    }
    /// <summary>
    /// http配置信息
    /// </summary>
    public class HttpConfig
    {
        /// <summary>
        /// Referer http 表头值设置或获取
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// 默认(text/html)
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 配置值：Accept HTTP 标头的值。 
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// 指定构成 HTTP 标头的名称/值对的集合。
        /// </summary>
        public string AcceptEncoding { get; set; }

        /// <summary>
        /// 超时时间(毫秒)默认60000
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// User-Agent http表头值设置或获取
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// POST请求时，数据是否进行gzip压缩
        /// </summary>
        public bool GZipCompress { get; set; }

        /// <summary>
        /// 持久连接
        /// </summary>
        public bool KeepAlive { get; set; }

        public string CharacterSet { get; set; }

        public HttpConfig()
        {
            this.Timeout = 60000;
            this.ContentType = "text/html; charset=" + Encoding.UTF8.WebName;
            this.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.117 Safari/537.36";
            this.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            this.AcceptEncoding = "gzip,deflate";
            this.GZipCompress = false;
            this.KeepAlive = true;
            this.CharacterSet = "UTF-8";
        }
    }
}