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
using BF.CrmProc.GTPT;

namespace BF.CrmWeb.GTPT.WX
{
    //客服接口-发消息   http请求方式: POST https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=ACCESS_TOKEN
    public class WX_CustomerMessage : WX_Base
    {
        public string[] tousers;
        public string touser;
        public string msgtype;
        public Customer_Text text;
        public Customer_Media image;
        public Customer_Media voice;
        public Customer_Video video;
        public Customer_Image_Text news;

        public override string InsertData(out string msg, HttpContext context = null)
        {
            msg = "";
            method = "POST";
            //tousers = new string[2] { "obZWquMvyYLxTwHkxf_WklSIi3A8", "obZWquGq3TjkuiNYOciytdtttW3g" };

            string openids = CrmLibProc.GetUserOpenid(out msg);
            //去后台查询数据信息  查询得到 的openid得
            List<GTPT_WXUser_Proc> wxusers = JsonConvert.DeserializeObject<List<GTPT_WXUser_Proc>>(openids);

            Url = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Token;
            for (int i = 0; i < wxusers.Count; i++)
            {
                touser = wxusers[i].sOPENID;
                string wxString = base.WXRequestString(out msg);
                if (msg != "")
                {
                    msg += ",已成功发送消息" + (i + 1) + "条";
                    return msg;
                    //break;
                }
            }
            return "发送成功";
        }
    }
    public class CustomerSevice
    {
        public string kfaccount;
    }
    public class Customer_Text
    {
        public string content;
    }
    public class Customer_Media
    {
        public string media_id;
    }
    public class Customer_Video
    {
        public string media_id;
        public string thumb_media_id;
        public string title;
        public string description;
    }
    public class Customer_Image_Text
    {
        public Customer_Image_Text_Item[] articles;
    }
    public class Customer_Image_Text_Item
    {
        public string title;
        public string description;
        public string url;
        public string picurl;

    }

}