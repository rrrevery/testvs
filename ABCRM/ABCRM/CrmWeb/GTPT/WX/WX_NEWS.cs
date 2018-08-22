using BF.CrmProc;
using BF.Pub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BF.CrmWeb.GTPT.WX
{
    public class WX_NEWS : WX_Base
    {
        public class MeidaInformation
        {
            public string media_id = string.Empty;
        }
        public class WX_NewsContent
        {
            public List<WX_NewsContentList> articles { get; set; }
        }
        public class WX_NewsContentList
        {
            public string title { get; set; }
            public string thumb_media_id { get; set; }
            public string author { get; set; }
            public string digest { get; set; }
            public int show_cover_pic { get; set; }
            public string content { get; set; }
            public string content_source_url { get; set; }
        }

        public override string SearchDatatoWX(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            method = "POST";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);

            Url = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token=" + oToken.result;
            postData = JsonConvert.SerializeObject(Get_WX_News(out msg, PUBLICID, updateValue));
            postData = Regex.Replace(postData, ",\"[^\"]+\":null", "");

            string wxstring = WXRequestString(out msg, context, postData); //调接口发布数据
            if (msg.Length > 0)
            {
                WriteToLog(msg);
            }
            else  //发布成功 传接口返回数据,更新media_id
            {
                UpdateMediaID(updateValue, wxstring);
            }
            Url = PUBLICIF + "?func=PostNews";
            Pub.Log4Net.WriteLog(LogLevel.INFO, Url);


            return wxstring;
        }


        public WX_NewsContent Get_WX_News(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_NewsContent tp_newscontent = new WX_NewsContent();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                List<WX_NewsContentList> newscontentlist = new List<WX_NewsContentList>();
                tp_newscontent.articles = newscontentlist;
                query.SQL.Text = "select * from WX_NEWSDY_ITEM where 1=1";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and JLBH=" + obj.iJLBH);
                }
                query.Open();
                while (!query.Eof)
                {
                    WX_NewsContentList list = new WX_NewsContentList();
                    newscontentlist.Add(list);
                    list.title = query.FieldByName("TITLE").AsString;
                    list.thumb_media_id = query.FieldByName("THUMB_MEDIA_ID").AsString;
                    list.author = query.FieldByName("AUTHOR").AsString;
                    list.digest = query.FieldByName("DIGEST").AsString;
                    list.show_cover_pic = query.FieldByName("BJ_COVER").AsInteger;
                    list.content = query.FieldByName("CONTNET").AsString;
                    list.content_source_url = query.FieldByName("YWURL").AsString;
                    query.Next();
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return tp_newscontent;
        }


        public void UpdateMediaID(string updateValue, string wxstring)
        {
            MeidaInformation objMeida = JsonConvert.DeserializeObject<MeidaInformation>(wxstring);
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "update WX_MEDIADY set MEDIA_ID=:MEDIA_ID where JLBH=:JLBH";
                query.ParamByName("MEDIA_ID").AsString = objMeida.media_id;
                query.ParamByName("JLBH").AsInteger = obj.iJLBH;
                query.ExecSQL();
            }
            catch (Exception e)
            {
                if (e is MyDbException)
                    throw e;
                throw new MyDbException(e.Message, query.SqlText);

            }
        }

        public override string DeleteData(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            method = "POST";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);

            Url = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token=" + oToken.result;

            string wxstring = WXRequestString(out msg, context, updateValue);
            if (msg.Length > 0)
            {
                WriteToLog(msg);
            }
            else
            {
                MeidaInformation objMeida = JsonConvert.DeserializeObject<MeidaInformation>(updateValue);
                DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
                try { conn.Open(); }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, true);
                }
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "delete from WX_MEDIADY where MEDIA_ID=:MEDIA_ID";
                    query.ParamByName("MEDIA_ID").AsString = objMeida.media_id;
                    query.ExecSQL();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            return wxstring;
        }

        public override string DeleteNews(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            method = "POST";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);

            Url = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token=" + oToken.result;

            string wxstring = WXRequestString(out msg, context, updateValue);
            if (msg.Length > 0)
            {
                WriteToLog(msg);
            }
            else
            {
                MeidaInformation objMeida = JsonConvert.DeserializeObject<MeidaInformation>(updateValue);
                DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
                try { conn.Open(); }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, true);
                }
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "delete from WX_MEDIADY where MEDIA_ID=:MEDIA_ID";
                    query.ParamByName("MEDIA_ID").AsString = objMeida.media_id;
                    query.ExecSQL();
                    query.SQL.Text = "delete from WX_NEWSDY_ITEM where JLBH=(select JLBH from WX_MEDIADY where MEDIA_ID=:MEDIA_ID)";
                    query.ParamByName("MEDIA_ID").AsString = objMeida.media_id;
                    query.ExecSQL();

                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            return wxstring;
        }

    }
}