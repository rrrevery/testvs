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

namespace BF.CrmWeb.GTPT.WX
{
    public class FILTER
    {
        public bool is_to_all = false;
        public int group_id = -1;
    }
    public class CONTENT
    {
        public string content = string.Empty;
    }
    public class MEDIA
    {
        public string media_id;
    }

    /// <summary>
    /// 消息基类（群发）
    /// </summary>
    public class WX_Message : WX_Base
    {
        public string msgtype;//text   image  mpnews
        public FILTER filter;
        public string type;
        public string created_at;
        public string media_id;
        public string title;
        public string description;
        public CONTENT text;
        public IMAGE_TEXT__MESSAGE[] articles;
        public MEDIA mpnews;
        public MEDIA mpvideo;
        public MEDIA voice;

        public bool preview = false;
        public string previewuser = "";


        public override string InsertData(out string msg, HttpContext context = null)
        {
            msg = "";
            if (filter.group_id == -1)
            {
                filter.is_to_all = true;
            }
            else
            {
                filter.is_to_all = false;
            }
            string wxString = "返回字符串";
            switch (msgtype)
            {
                case "text":
                    wxString = SendText(out msg);
                    break;
                case "news":
                case "mpnews":
                    msgtype = "mpnews";
                    wxString = SendImageText(out msg);
                    break;
                case "voice":
                    wxString = SendVoice(out msg);
                    break;
                case "video":
                    wxString = SendVideo(out msg);
                    break;
            }
            return wxString;
        }
        //发送文本消息
        public string SendText(out string msg, HttpContext context = null)
        {
            msg = "";
            msgtype = "text";
            method = "POST";
            string wxstring = "";
            if (preview == false)
            {

                Url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + Token;
                wxstring = WXRequestString(out msg);
                return wxstring;
            }
            //预览,待修改成静态函数，直接调用
            WX_MessagePreview msgpreview = new WX_MessagePreview();
            msgpreview.touser = previewuser;
            msgpreview.text = text;
            msgpreview.msgtype = msgtype;
            Url = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token=" + Token;
            wxstring = WXRequestString(out msg, context, JsonConvert.SerializeObject(msgpreview));
            return wxstring;

        }
        //发送图文消息
        public string SendImageText(out string msg, HttpContext context = null)
        {
            msg = "";
            //消息上传
            method = "POST";
            //for (int i = 0; i < articles.Length; i++)
            //{
            //    articles[i].content = System.Web.HttpUtility.HtmlDecode(articles[i].content);
            //}
            Url = "https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token=" + Token;

            string wxstring = WXRequestString(out msg);
            if (msg != "")
            {
                msg = "图文消息创建失败";
                return msg;
            }


            WX_Message test = JsonConvert.DeserializeObject<WX_Message>(wxstring);

            //消息发送
            test.filter = new FILTER();
            test.filter.is_to_all = this.filter.is_to_all;
            test.filter.group_id = filter.group_id;
            test.mpnews = new MEDIA();
            test.mpnews.media_id = test.media_id;
            test.msgtype = "mpnews";
            test.dir = this.dir;
            //filename = test.DownData(out msg, "thumb");
            if (msg != "")
            {
                return "";
            }

            if (preview == false)
            {
                Url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + Token;
                wxstring = WXRequestString(out msg, null, JsonConvert.SerializeObject(test));
                return wxstring;
            }
            WX_MessagePreview msgpreview = new WX_MessagePreview();
            msgpreview.touser = previewuser;
            msgpreview.mpnews = test.mpnews;
            msgpreview.msgtype = msgtype;
            Url = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token=" + Token;
            wxstring = WXRequestString(out msg, context, JsonConvert.SerializeObject(msgpreview));
            return wxstring;
        }

        //发送语音消息
        public string SendVoice(out string msg, HttpContext context = null)
        {
            msgtype = "voice";
            method = "POST";
            string wxstring = "";
            if (preview == false)
            {
                Url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + Token;
                wxstring = WXRequestString(out msg);
                return wxstring;
            }
            //预览,待修改成静态函数，直接调用
            WX_MessagePreview msgpreview = new WX_MessagePreview();
            msgpreview.touser = previewuser;
            msgpreview.voice = voice;
            msgpreview.msgtype = msgtype;
            Url = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token=" + Token;
            wxstring = WXRequestString(out msg, context, JsonConvert.SerializeObject(msgpreview));
            return wxstring;
        }
        //发送视频消息
        public string SendVideo(out string msg, HttpContext context = null)
        {
            Url = "https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token=" + Token;
            method = "POST";

            string wxstring = UploadVideo(out msg);//二次上传

            if (msg == "")
            {
                WX_Message videomsg = JsonConvert.DeserializeObject<WX_Message>(wxstring);
                videomsg.filter = new FILTER();
                videomsg.filter.is_to_all = this.filter.is_to_all;
                videomsg.filter.group_id = filter.group_id;
                videomsg.Url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + Token;
                Url = videomsg.Url;
                videomsg.mpvideo = new MEDIA();
                videomsg.mpvideo.media_id = videomsg.media_id;
                videomsg.msgtype = "mpvideo";
                videomsg.method = "POST";
                if (preview == false)
                {
                    wxstring = videomsg.WXRequestString(out msg, null, JsonConvert.SerializeObject(videomsg));
                    return wxstring;
                }
                //预览
                WX_MessagePreview msgpreview = new WX_MessagePreview();
                msgpreview.touser = previewuser;
                msgpreview.mpvideo = videomsg.mpvideo;
                msgpreview.msgtype = videomsg.msgtype;
                Url = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token=" + Token;
                wxstring = WXRequestString(out msg, context, JsonConvert.SerializeObject(msgpreview));
                return wxstring;
            }

            return wxstring;
        }
        //视频消息上传
        public string UploadVideo(out string msg)
        {
            method = "POST";
            Url = "https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token=" + Token;
            string wxstring = WXRequestString(out msg);

            return wxstring;
        }

        //下载
        public string DownData(out string msg, string type = "")
        {
            method = "GET";
            Url = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + Token + "&media_id=" + media_id;
            filename = WXDownloadFile(out msg, "xxm.jpg", type);
            return filename;
        }
    }
    /// <summary>
    /// 单条图文
    /// </summary>
    public class IMAGE_TEXT__MESSAGE
    {
        public string thumb_media_id { set; get; }
        public string author { set; get; }
        public string title { set; get; }
        public string content_source_url { set; get; }
        public string content { set; get; }
        public string digest { set; get; }
        public string show_cover_pic { set; get; }
    }
    //    {     
    //    "touser":"OPENID",
    //    "text":{           
    //           "content":"CONTENT"            
    //           },     
    //    "msgtype":"text"
    //}
    //    {
    //   "touser":"OPENID", 
    //   "mpnews":{              
    //            "media_id":"123dsdajkasd231jhksad"               
    //             },
    //   "msgtype":"mpnews" 
    //}

    //{
    //    "touser":"OPENID",
    //    "voice":{              
    //            "media_id":"123dsdajkasd231jhksad"
    //            },
    //    "msgtype":"voice" 
    //}
    //{
    //    "touser":"OPENID",
    //    "mpvideo":{  "media_id":"IhdaAQXuvJtGzwwc0abfXnzeezfO0NgPK6AQYShD8RQYMTtfzbLdBIQkQziv2XJc",   
    //               },
    //    "msgtype":"mpvideo" 
    //}
    /// <summary>
    /// 消息预览
    /// </summary>
    public class WX_MessagePreview : WX_Base
    {
        public string touser = "";
        public MEDIA mpnews;//需要上传获取mediaid
        public MEDIA mpvideo;
        public MEDIA voice;
        public CONTENT text;
        public string msgtype = "";

    }

}