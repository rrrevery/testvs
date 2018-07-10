using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BF.CrmWeb.GTPT.WX
{
    /// <summary>
    /// 微信用户所在分组
    /// </summary>
    public class WX_UserGroup : WX_Base
    {
        //{"openid":"oDF3iYx0ro3_7jD4HFRDfrjdCM58","to_groupid":108}
        public string openid;//微信用户针对公众号的惟一标识
        public int to_groupid;//移动至某分组号
        public int groupid;//用户所在分组编号


        //        http请求方式: POST（请使用https协议）
        //https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token=ACCESS_TOKEN
        //POST数据格式：json
        //POST数据例子：{"openid":"oDF3iYx0ro3_7jD4HFRDfrjdCM58","to_groupid":108}
        public override string UpdateData(out string msg, HttpContext context = null)
        {
            Url = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token=" + Token;
            method = "POST";
            returnString = WXRequestString(out msg);
            return returnString;
        }
    }
}