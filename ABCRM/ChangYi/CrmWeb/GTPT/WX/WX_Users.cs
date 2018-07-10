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
using Newtonsoft.Json.Utilities;
using BF.CrmProc.GTPT;
namespace BF.CrmWeb.GTPT.WX
{
    //关注者惟一标识
    public class OPENID
    {
        public string[] openid { get; set; }
    }
    //关注者列表 
    public class WX_GET_USERSLIST
    {
        public string total { get; set; }
        public int count { get; set; }
        public OPENID data { get; set; }
        public string next_openid { get; set; }

    }
    //关注者用户详细信息 在GTPT_WXGRXX 放在了后台BFCRMProc 当中，方便与数据库的操作

    /// <summary>
    /// 所有关注者详细信息获取，有必要还可以插入到本地数据库
    /// </summary>
    public class WX_Users : WX_Base
    {
        public string next_openid = "";
        List<Object> list = new List<Object>();
        //第一步，先获取关注者列表 
        //第二步，获取所有关注者的信息
        public override string SearchData(out string msg,HttpContext context=null)
        {
            if (next_openid != "")
            {
                Url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + Token + "&next_openid=" + next_openid;
            }
            else
            {
                Url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + Token;
            }
            returnString = WXRequestString(out msg);
            return returnString;
        }
    }
}