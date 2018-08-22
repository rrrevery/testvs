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
using BF.CrmProc.GTPT;

namespace BF.CrmWeb.GTPT.WX
{
    public class WX_GRXX : WX_Base
    {
        public string subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }
        public string unionid { get; set; }
        public string errmsg { get; set; }
        public string groupid { get; set; }

        public string next_openid = "";
        public string total = "0";
        public int count = 0;
        public static int IMPORTCOUNT = 0;
        public static int DOWNCOUNT = 0;
        public int downcount = 0;//已下载
        public int importcount = 0;//导入的量
        public List<WX_GRXX> list = new List<WX_GRXX>();
        /// <summary>
        /// 信息查询
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override string SearchData(out string msg, HttpContext context = null)
        {

            WX_Users users = new WX_Users();
            users.next_openid = this.next_openid;
            try
            {
                //信息下载
                returnString = users.SearchData(out msg);
                if (msg != "")
                {
                    return "";
                }
                WX_GET_USERSLIST wxget = JsonConvert.DeserializeObject<WX_GET_USERSLIST>(returnString);
                for (int i = 0; i < wxget.count; i++)
                {
                    //查询微信用户信息
                    string grxxString = GetUserInfoByOpenid(out msg, wxget.data.openid[i]);
                    if (msg != "")
                    {
                        return msg;
                    }

                    WX_GRXX grxx = JsonConvert.DeserializeObject<WX_GRXX>(grxxString);
                    //查询用户的的groupid
                    grxx.subscribe_time = GetTime(grxx.subscribe_time);
                    //grxx.groupid = grxx.GetGroupidByOpenid(out msg);
                    //查询用户所在分组有10000次的调用限制
                    //if (msg != "")
                    //{
                    //    return msg;
                    //}
                    list.Add(grxx);
                    //if (grxx.nickname == "TJoy" || grxx.nickname == "空-城")
                    //{
                    //    continue;
                    //}
                    //DOWNCOUNT++;
                    //downcount = DOWNCOUNT;
                    //context.Response.Write(JsonConvert.SerializeObject(this));
                    //if (i >= 99)
                    //{
                    //    break;
                    //}
                }
                total = wxget.total;
                count = wxget.count;
                //信息导入
                GTPT_WXUser_Proc wxuser = new GTPT_WXUser_Proc();
                for (int i = 0; i < list.Count; i++)
                {
                    IMPORTCOUNT++;
                    wxuser.sOPENID = list[i].openid;
                    wxuser.dDJSJ = list[i].subscribe_time;
                    wxuser.iSEX = list[i].sex;
                    wxuser.iGROUPID = 0;
                    //list[i].groupid ==""?
                    wxuser.sHEADIMGURL = list[i].headimgurl;
                    wxuser.sNICKNAME = list[i].nickname;
                    wxuser.SaveData(out msg);
                    //context.Response.Write(JsonConvert.SerializeObject(this));
                }
                this.next_openid = wxget.next_openid;
                this.importcount = IMPORTCOUNT;
                if (count < 10000)
                {
                    IMPORTCOUNT = 0;
                    DOWNCOUNT = 0;
                }
                return JsonConvert.SerializeObject(this);
            }
            catch (Exception e)
            {
                throw e;
                msg = e.Message;
                return msg;
            }
        }

        /// <summary>
        /// 获取分组ID
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetGroupidByOpenid(out string msg)
        {
            //{"openid":"od8XIjsmk6QdVTETa9jLtGWA6KBc"}
            Url = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token=" + Token;
            method = "post";
            postData = "{\"openid\":\"" + openid + "\"}";
            string wxstring = WXRequestString(out msg, null, postData);
            if (msg == "")
            {
                GROUP group = JsonConvert.DeserializeObject<GROUP>(wxstring);
                return group.groupid;
            }
            return msg;
        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public string GetUserInfoByOpenid(out string msg, string p_OPENDID)
        {
            method = "GET";
            this.Url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Token + "&openid=" + p_OPENDID + "&lang=zh_CN";
            string grxxString = this.WXRequestString(out msg);
            return grxxString;

        }
    }
}