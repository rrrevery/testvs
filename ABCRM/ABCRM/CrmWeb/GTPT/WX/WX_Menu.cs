using BF.Pub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;
using System.Configuration;
using System.Text;
namespace BF.CrmWeb.GTPT.WX
{
    public class WX_Menu : WX_Base
    {
        //BUTTON[] button;
        //public override string SearchData(out string msg,HttpContext context=null)
        //{
        //    Url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + Token;
        //    returnString = WXRequestString(out msg);
        //    return returnString;
        //}


        public override string SearchDatatoWX(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            method = "POST";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);


            Url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + oToken.result;


            postData = JsonConvert.SerializeObject(Get_WX_PostMenu(out msg, PUBLICID));
            postData = Regex.Replace(postData, ",\"[^\"]+\":null", "");
           //我觉得这个可能没执行

            string wxstring = WXRequestString(out msg, context, postData);

            if (msg.Length > 0)
            {
                WriteToLog(msg);

            }
            //到这里

            Url = PUBLICIF + "?func=SetMenu";


            Pub.Log4Net.WriteLog(LogLevel.INFO, Url);

            postData = "";
            context = null;
            wxstring = WXRequestString(out msg, context, postData);

            Pub.Log4Net.WriteLog(LogLevel.INFO, wxstring);

            return wxstring;
        }

      
        public class WX_MenuContent
        {
            public List<WX_MenuContentList> button { get; set; }
        }
        public class Token1
        {
            public string errCode = string.Empty;
            public string errMessage = string.Empty;
            public string result = string.Empty;
        }
        public class WX_MenuContentList
        {
            public string name { get; set; }
            public string type { get; set; }
            public string key { get; set; }
            public string url { get; set; }
            public List<WX_MenuContentListItem> sub_button { get; set; }
        }
        public class WX_MenuContentListItem
        {
            public string type { get; set; }
            public string name { get; set; }
            public string key { get; set; }
            public string url { get; set; }
            public int PUBLICID { get; set; }
        }
        public class WX_MenuMiddle
        {
            public string name { get; set; }
            public int type { get; set; }
            public string key { get; set; }
            public string url { get; set; }
            public string dm = string.Empty;
            public int PUBLICID = 0;
        }
        //public class WX_MenuContentList
        //{
        //    public string name { get; set; }
        //    public string type { get; set; }
        //    public string nbdm { get; set; }
        //    public string url { get; set; }
        //    public string dm;
        //    public List<WX_MenuContentListItem> sub_button { get; set; }
        //}
        //public class WX_MenuContentListItem
        //{
        //    public string type { get; set; }
        //    public string name { get; set; }
        //    public string nbdm { get; set; }
        //    public string url { get; set; }
        //}
        public WX_MenuContent Get_WX_PostMenu(out string msg, string PUBLICID)
        {
            msg = string.Empty;
            WX_MenuContent tp_menucontent = new WX_MenuContent();

            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                List<WX_MenuContentList> menucontentlist = new List<WX_MenuContentList>();
                tp_menucontent.button = menucontentlist;
                List<WX_MenuMiddle> menu = new List<WX_MenuMiddle>();
                try
                {
                    string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Add("select NAME,ASKID,DM,TYPE,URL,PUBLICID from WX_MENU where ").Add(CyDbSystem.GetDataLengthFuncName(conn)).Add("(DM)=2 and PUBLICID='" + PUBLICID + "' order by DM");
                    //query.ParamByName("PUBLICID").AsInteger = Convert.ToInt32(PUBLICID);
                    query.Open();
                    while (!query.Eof)
                    {
                        WX_MenuMiddle tp_menu = new WX_MenuMiddle();
                        menu.Add(tp_menu);

                        tp_menu.key = Convert.ToString(query.FieldByName("ASKID").AsInteger);
                        tp_menu.dm = query.FieldByName("DM").AsString;
                        tp_menu.type = query.FieldByName("TYPE").AsInteger;
                        tp_menu.url = query.FieldByName("URL").AsString;
                        tp_menu.PUBLICID = query.FieldByName("PUBLICID").AsInteger;
                        if (DbSystemName == "ORACLE")
                        {
                            tp_menu.name = query.FieldByName("NAME").AsString;
                        }
                        else if (DbSystemName == "SYBASE")
                        {
                            tp_menu.name = query.FieldByName("NAME").GetChineseString(200);
                        }
                        query.Next();
                    }
                    query.Close();
                    query.Params.Clear();
                    for (int i = 0; i < menu.Count; i++)
                    {
                        WX_MenuContentList tp_menucontentlist = new WX_MenuContentList();
                        menucontentlist.Add(tp_menucontentlist);
                        tp_menucontentlist.name = menu[i].name;
                        if (menu[i].type == 0)
                        {
                            List<WX_MenuContentListItem> list_menucontentlistitem = new List<WX_MenuContentListItem>();
                            tp_menucontentlist.sub_button = list_menucontentlistitem;
                            query.Params.Clear();
                            query.SQL.Clear();
                            query.SQL.Add("select NAME,TYPE,ASKID,DM,URL,PUBLICID from WX_MENU where " + CyDbSystem.GetDataLengthFuncName(conn) + "(DM)=4 and DM LIKE '" + menu[i].dm + "%" + "' and PUBLICID='" + PUBLICID + "'  order by DM");
                            //query.ParamByName("PUBLICID").AsInteger = Convert.ToInt32(PUBLICID);
                            query.Open();
                            while (!query.Eof)
                            {
                                WX_MenuContentListItem tp_menucontentlistitem = new WX_MenuContentListItem();
                                list_menucontentlistitem.Add(tp_menucontentlistitem);
                                int itemtype = query.FieldByName("TYPE").AsInteger;
                                tp_menucontentlistitem.PUBLICID = query.FieldByName("PUBLICID").AsInteger;
                                if (DbSystemName == "ORACLE")
                                {
                                    tp_menucontentlistitem.name = query.FieldByName("NAME").AsString;

                                }
                                else if (DbSystemName == "SYBASE")
                                {
                                    tp_menucontentlistitem.name = query.FieldByName("NAME").GetChineseString(200);

                                }
                                if (itemtype == 2)
                                {
                                    tp_menucontentlistitem.type = "view";
                                    tp_menucontentlistitem.url = query.FieldByName("URL").AsString;
                                }
                                if (itemtype == 1)
                                {
                                    tp_menucontentlistitem.type = "click";
                                    tp_menucontentlistitem.key = Convert.ToString(query.FieldByName("ASKID").AsInteger);
                                }
                                query.Next();
                            }
                            query.Close();
                        }
                        else if (menu[i].type == 1)
                        {
                            tp_menucontentlist.type = "click";
                            tp_menucontentlist.key = menu[i].key;
                        }
                        else if (menu[i].type == 2)
                        {
                            tp_menucontentlist.type = "view";
                            tp_menucontentlist.url = menu[i].url;
                        }
                    }
                }
                catch (Exception e)
                {

                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {

                conn.Close();
            }
            return tp_menucontent;
        }

    }
}