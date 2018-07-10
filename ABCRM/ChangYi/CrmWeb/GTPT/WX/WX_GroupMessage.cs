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
    public class WX_GroupMessage : WX_Base
    {

        #region  按标签发送文本消息
        public class WX_SendText
        {
            public Filter filter { get; set; }
            public Text text { get; set; }
            public string msgtype { get; set; }
        }
        public class Filter
        {
            public bool is_to_all { get; set; }
            public int tag_id { get; set; }
        }

        public class Text
        {
            public string content { get; set; }
        }

        public string SendText(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendText tp_sendtext = new WX_SendText();
            tp_sendtext.filter = new Filter();
            tp_sendtext.text = new Text();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendtext.filter.is_to_all = query.FieldByName("QFDX").AsInteger == 1 ? false : true;
                    tp_sendtext.filter.tag_id = query.FieldByName("TAGID").AsInteger;
                    tp_sendtext.text.content = query.FieldByName("CONTENT").AsString;
                    tp_sendtext.msgtype = query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendtext);
        }

        #endregion

        #region 按标签发送图文消息
        //public class Filter
        //{
        //    public string is_to_all { get; set; }
        //    public int tag_id { get; set; }
        //}

        public class Mpnews
        {
            public string media_id { get; set; }
        }

        public class WX_SendNews
        {
            public Filter filter { get; set; }
            public Mpnews mpnews { get; set; }
            public string msgtype { get; set; }
            public int send_ignore_reprint { get; set; }
            public string clientmsgid { get; set; } //clientmsgid 参数，避免重复推送
        }

        public string SendNews(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendNews tp_sendnews = new WX_SendNews();
            tp_sendnews.filter = new Filter();
            tp_sendnews.mpnews = new Mpnews();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendnews.filter.is_to_all = query.FieldByName("QFDX").AsInteger == 1 ? false : true;
                    tp_sendnews.filter.tag_id = query.FieldByName("TAGID").AsInteger;
                    tp_sendnews.mpnews.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendnews.msgtype = query.FieldByName("TYPE").AsString;
                    tp_sendnews.send_ignore_reprint = query.FieldByName("BJ_ZZ").AsInteger;
                    tp_sendnews.clientmsgid = query.FieldByName("JLBH").AsInteger.ToString();
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

            return JsonConvert.SerializeObject(tp_sendnews);
        }

        #endregion

        #region 按标签发送语音消息

        public class Voice
        {
            public string media_id { get; set; }
        }

        public class WX_SendVoice
        {
            public Filter filter { get; set; }
            public Voice voice { get; set; }
            public string msgtype { get; set; }
        }

        public string SendVoice(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendVoice tp_sendvoice = new WX_SendVoice();
            tp_sendvoice.filter = new Filter();
            tp_sendvoice.voice = new Voice();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendvoice.filter.is_to_all = query.FieldByName("QFDX").AsInteger == 1 ? false : true;
                    tp_sendvoice.filter.tag_id = query.FieldByName("TAGID").AsInteger;
                    tp_sendvoice.voice.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendvoice.msgtype = query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendvoice);
        }

        #endregion

        #region 按标签发送图片消息

        public class Image
        {
            public string media_id { get; set; }
        }
        public class WX_SendImage
        {
            public Filter filter { get; set; }
            public Image image { get; set; }
            public string msgtype { get; set; }
        }
        public string SendImage(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendImage tp_sendimage = new WX_SendImage();
            tp_sendimage.filter = new Filter();
            tp_sendimage.image = new Image();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendimage.filter.is_to_all = query.FieldByName("QFDX").AsInteger == 1 ? false : true;
                    tp_sendimage.filter.tag_id = query.FieldByName("TAGID").AsInteger;
                    tp_sendimage.image.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendimage.msgtype = query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendimage);
        }

        #endregion

        #region 按标签发送视频消息

        public class Mpvideo
        {
            public string media_id { get; set; }
        }
        public class WX_SendVideo
        {
            public Filter filter { get; set; }
            public Mpvideo mpvideo { get; set; }
            public string msgtype { get; set; }
        }
        public string SendVideo(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendVideo tp_sendvideo = new WX_SendVideo();
            tp_sendvideo.filter = new Filter();
            tp_sendvideo.mpvideo = new Mpvideo();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendvideo.filter.is_to_all = query.FieldByName("QFDX").AsInteger == 1 ? false : true;
                    tp_sendvideo.filter.tag_id = query.FieldByName("TAGID").AsInteger;
                    tp_sendvideo.mpvideo.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendvideo.msgtype = "mpvideo";//query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendvideo);
        }

        #endregion

        #region 按标签发送卡券消息

        public class WX_SendCard
        {
            public Filter filter { get; set; }
            public Wxcard wxcard { get; set; }
            public string msgtype { get; set; }
            public int send_ignore_reprint { get; set; }
            public string clientmsgid { get; set; } //clientmsgid 参数，避免重复推送
        }

        public string SendCard(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendCard tp_sendcard = new WX_SendCard();
            tp_sendcard.filter = new Filter();
            tp_sendcard.wxcard = new Wxcard();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendcard.filter.is_to_all = query.FieldByName("QFDX").AsInteger == 1 ? false : true;
                    tp_sendcard.filter.tag_id = query.FieldByName("TAGID").AsInteger;
                    tp_sendcard.wxcard.card_id = System.Configuration.ConfigurationManager.AppSettings["card_id"];// query.FieldByName("MEDIA_ID").AsString;
                    tp_sendcard.msgtype = query.FieldByName("TYPE").AsString;
                    tp_sendcard.send_ignore_reprint = query.FieldByName("BJ_ZZ").AsInteger;
                    tp_sendcard.clientmsgid = query.FieldByName("JLBH").AsInteger.ToString();
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

            return JsonConvert.SerializeObject(tp_sendcard);
        }

        #endregion

        #region  按OpenID发送文本消息

        public class WX_SendTextByOpenID
        {
            public List<string> touser { get; set; }
            public string msgtype { get; set; }
            public Text text { get; set; }
        }

        public string SendTextByOpenID(out string msg, string PUBLICID, string updateValue, List<string> listOpenID)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendTextByOpenID tp_sendtext = new WX_SendTextByOpenID();
            tp_sendtext.text = new Text();
            List<string> touser = new List<string>();
            tp_sendtext.touser = listOpenID;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendtext.text.content = query.FieldByName("CONTENT").AsString;
                    tp_sendtext.msgtype = query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendtext);
        }

        #endregion


        #region 按OpenID发送图文消息

        public class WX_SendNewsByOpenID
        {
            public List<string> touser { get; set; }
            public Mpnews mpnews { get; set; }
            public string msgtype { get; set; }
            public int send_ignore_reprint { get; set; }
        }

        public string SendNewsByOpenID(out string msg, string PUBLICID, string updateValue, List<string> listOpenID)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendNewsByOpenID tp_sendnews = new WX_SendNewsByOpenID();
            tp_sendnews.mpnews = new Mpnews();
            List<string> touser = new List<string>();
            tp_sendnews.touser = listOpenID;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {                   
                    tp_sendnews.mpnews.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendnews.msgtype = query.FieldByName("TYPE").AsString;
                    tp_sendnews.send_ignore_reprint = query.FieldByName("BJ_ZZ").AsInteger;
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

            return JsonConvert.SerializeObject(tp_sendnews);
        }

        #endregion


        #region 按OpenID发送语音消息

        public class WX_SendVoiceByOpenID
        {
            public List<string> touser { get; set; }
            public Voice voice { get; set; }
            public string msgtype { get; set; }
        }

        public string SendVoiceByOpenID(out string msg, string PUBLICID, string updateValue, List<string> listOpenID)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendVoiceByOpenID tp_sendvoice = new WX_SendVoiceByOpenID();
            tp_sendvoice.voice = new Voice();
            List<string> touser = new List<string>();
            tp_sendvoice.touser = listOpenID;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    Voice voice = new Voice();
                    tp_sendvoice.voice.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendvoice.msgtype = query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendvoice);
        }

        #endregion

        #region 按OpenID发送图片消息

        public class WX_SendImageByOpenID
        {
            public List<string> touser { get; set; }
            public Image image { get; set; }
            public string msgtype { get; set; }
        }

        public string SendImageByOpenID(out string msg, string PUBLICID, string updateValue, List<string> listOpenID)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendImageByOpenID tp_sendimage = new WX_SendImageByOpenID();
            tp_sendimage.image = new Image();
            List<string> touser = new List<string>();
            tp_sendimage.touser = listOpenID;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendimage.image.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendimage.msgtype = query.FieldByName("TYPE").AsString;
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

            return JsonConvert.SerializeObject(tp_sendimage);
        }

        #endregion

        #region 按OpenID发送视频消息

        public class WX_SendVideoByOpenID
        {
            public List<string> touser { get; set; }
            public Mpvideo mpvideo { get; set; }
            public string msgtype { get; set; }
        }

        public string SendVideoByOpenID(out string msg, string PUBLICID, string updateValue, List<string> listOpenID)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendVideoByOpenID tp_sendvideo = new WX_SendVideoByOpenID();
            tp_sendvideo.mpvideo = new Mpvideo();
            List<string> touser = new List<string>();
            tp_sendvideo.touser = listOpenID;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendvideo.mpvideo.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_sendvideo.msgtype = "mpvideo";//query.FieldByName("TYPE").AsString; 数据库记录的是video
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

            return JsonConvert.SerializeObject(tp_sendvideo);
        }

        #endregion

        #region 按OpenID发送卡券消息

        public class WX_SendCardByOpenID
        {
            public List<string> touser { get; set; }
            public Wxcard wxcard { get; set; }
            public string msgtype { get; set; }
            public int send_ignore_reprint { get; set; }
        }

        public string SendCardByOpenID(out string msg, string PUBLICID, string updateValue, List<string> listOpenID)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_SendCardByOpenID tp_sendcard = new WX_SendCardByOpenID();
            tp_sendcard.wxcard = new Wxcard();
            List<string> touser = new List<string>();
            tp_sendcard.touser = listOpenID;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_sendcard.wxcard.card_id =System.Configuration.ConfigurationManager.AppSettings["card_id"]; //query.FieldByName("MEDIA_ID").AsString;
                    tp_sendcard.msgtype = query.FieldByName("TYPE").AsString;
                    tp_sendcard.send_ignore_reprint = query.FieldByName("BJ_ZZ").AsInteger;
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

            return JsonConvert.SerializeObject(tp_sendcard);
        }

        #endregion

        public List<string> GetUserList(out string msg, string PUBLICID, string updateValue)
        {
            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            List<string> listOpenID = new List<string>();
            string openId = string.Empty;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select * from WX_QFXX_YHLIST  where  1=1";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and JLBH=" + obj.iJLBH);
                }
                query.Open();
                while (!query.Eof)
                {
                    openId = query.FieldByName("OPENID").AsString;
                    listOpenID.Add(openId);
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

            return listOpenID;
        }


        public List<string> GetAllUserList(out string msg, string PUBLICID, string updateValue)
        {
            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            List<string> listOpenID = new List<string>();
            string openId = string.Empty;
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select * from WX_USER where STATUS=1 and PUBLICID=" + PUBLICID;    
                query.Open();
                while (!query.Eof)
                {
                    openId = query.FieldByName("OPENID").AsString;
                    listOpenID.Add(openId);
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

            return listOpenID;
        }

        public override string SearchDatatoWX(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            method = "POST";
            msg = "";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            if (obj.iQFDX == 1)//按标签群发
            {
                Url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token=" + oToken.result;
                switch (obj.sMSGTYPE)
                {
                    case "text":
                        postData = SendText(out msg, PUBLICID, updateValue);
                        break;
                    case "image":
                        postData = SendImage(out msg, PUBLICID, updateValue);
                        break;
                    case "voice":
                        postData = SendVoice(out msg, PUBLICID, updateValue);
                        break;
                    case "video":
                        postData = SendVideo(out msg, PUBLICID, updateValue);
                        break;
                    case "music": break;
                    case "mpnews":
                        postData = SendNews(out msg, PUBLICID, updateValue);
                        break;
                    case "wxcard":
                        postData = SendCard(out msg, PUBLICID, updateValue);
                        break;
                    default: break;
                }
            }
            else
            {
                Url = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token=" + oToken.result;
                List<string> listOpenID = new List<string>();
                if (obj.iQFDX == 2) //按OpenID列表群发
                {
                    listOpenID = GetUserList(out msg, PUBLICID, updateValue);//获取已选用户列表
                }
                else //向全部用户群发
                {
                    listOpenID = GetAllUserList(out msg, PUBLICID, updateValue);//获取全部用户列表
                }
                switch (obj.sMSGTYPE)
                {
                    case "text":
                        postData = SendTextByOpenID(out msg, PUBLICID, updateValue, listOpenID);
                        break;
                    case "image":
                        postData = SendImageByOpenID(out msg, PUBLICID, updateValue, listOpenID);
                        break;
                    case "voice":
                        postData = SendVoiceByOpenID(out msg, PUBLICID, updateValue, listOpenID);
                        break;
                    case "video":
                        postData = SendVideoByOpenID(out msg, PUBLICID, updateValue, listOpenID);
                        break;
                    case "music": break;
                    case "mpnews":
                        postData = SendNewsByOpenID(out msg, PUBLICID, updateValue, listOpenID);
                        break;
                    case "wxcard":
                        postData = SendCardByOpenID(out msg, PUBLICID, updateValue, listOpenID);
                        break;
                    default: break;
                }
            }
            postData = Regex.Replace(postData, ",\"[^\"]+\":null", "");

            string wxstring = WXRequestStringResult(out msg, context, postData); //调接口发布数据，返回的是json

            PostSuccessResult objSucResult= JsonConvert.DeserializeObject<PostSuccessResult>(wxstring);
            //var result = JsonHelper<PostSuccessResult>.ConvertJson(Url, postData);
            //return result;

            if (msg.Length > 0||objSucResult.errcode!=0)
            {
                WriteToLog(msg);
                updatePostStatus(updateValue, wxstring, 4);//发布消息失败
            }
            else
            {
                updatePostStatus(updateValue, wxstring, 3);//发布消息成功
            }
            Url = PUBLICIF + "?func=PostNews";
            Pub.Log4Net.WriteLog(LogLevel.INFO, Url);

            HYKGL_WX_ERROR wxerror = JsonConvert.DeserializeObject<HYKGL_WX_ERROR>(wxstring); //处理接口返回json的报错
            if (wxerror.errcode > -2)
            {
                if (wxerror.errcode == 42001 || wxerror.errcode == 40001)
                {

                    //重新获取一下Token
                }
                int index = -1;
                Pub.Log4Net.WriteLog(LogLevel.INFO, wxstring);
                //使用二分查找  待做
                for (int i = 0; i < HYKGL_WX_ERROR.errcodeArr.Length; i++)
                {
                    if (wxerror.errcode == HYKGL_WX_ERROR.errcodeArr[i])
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1 && index != 1)
                {
                    msg = HYKGL_WX_ERROR.errmsgArr[index];
                }
                return msg;
            }
            Pub.Log4Net.WriteLog(LogLevel.INFO, wxstring);


            return wxstring;
        }


        public override string DeleteData(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            msg = "";
            method = "POST";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);

            checkPostCondition(out msg,updateValue);
            if (msg != "") {
                return msg;
            }
            Url = "https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token=" + oToken.result;

            postData = GetMsgID(out msg, PUBLICID, updateValue);
            string wxstring = WXRequestString(out msg, context, postData);
            if (msg.Length > 0)
            {
                WriteToLog(msg);
            }
            else
            {
                updatePostStatus(updateValue, postData, 5);//成功删除发布消息
            }
            return wxstring;
        }  
        
        #region  得到MSG_ID
        public string GetMsgID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            postDelData tp_deldata = new postDelData();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "select * from WX_QFXXDY Q   where  1=1";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_deldata.msg_id = query.FieldByName("MSG_ID").AsString;
                    tp_deldata.article_idx = 0;
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

            return JsonConvert.SerializeObject(tp_deldata);
        }

        #endregion

        public class PostSuccessResult
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string msg_id { get; set; }
            public string msg_data_id { get; set; }
        }

        public class postDelData
        {
            public string msg_id { get; set; }
            public int article_idx { get; set; }
        }
        public void checkPostCondition(out string msg, string updateValue)
        {
            msg = "";
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
                if (obj.iJLBH != 0)
                {
                    query.SQL.Text = "select * from WX_QFXXDY where STATUS=3 and JLBH=" + obj.iJLBH;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "此消息未发布成功，不能删除";
                    }
                }
                else
                {
                    msg = "没有获取到菜单记录编号";
                }
            }
            catch (Exception e)
            {
                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);

            }

        }

        public void updatePostStatus(string updateValue, string wxstring, int status)
        {
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
                if (status == 3)  //3发布成功(更新状态、MSG_ID、MSG_DATA_ID) 
                {
                    PostSuccessResult objSucResult = JsonConvert.DeserializeObject<PostSuccessResult>(wxstring);
                    query.SQL.Text = "update WX_QFXXDY  set STATUS=3,MSG_ID=:MSG_ID,MSG_DATA_ID=:MSG_DATA_ID where JLBH=:JLBH";
                    query.ParamByName("MSG_ID").AsString = objSucResult.msg_id;
                    query.ParamByName("MSG_DATA_ID").AsString = objSucResult.msg_data_id;
                    query.ParamByName("JLBH").AsInteger = obj.iJLBH;

                }
                else if (status == 4)  //4发布失败 
                {
                    query.SQL.Text = "update WX_QFXXDY  set STATUS=4 where JLBH=:JLBH";
                    query.ParamByName("JLBH").AsInteger = obj.iJLBH;
                }
                else //5已删除发布 参数传的是media_id
                {
                    postDelData objDelParam = JsonConvert.DeserializeObject<postDelData>(wxstring);
                    query.SQL.Text = "update WX_QFXXDY  set STATUS=5 where MSG_ID=:MSG_ID";
                    query.ParamByName("MSG_ID").AsString = objDelParam.msg_id;
                }
                query.ExecSQL();
            }
            catch (Exception e)
            {
                if (e is MyDbException)
                    throw e;
                throw new MyDbException(e.Message, query.SqlText);

            }

        }

        public override string PreviewMassMsg(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
        {
            method = "POST";
            msg = "";
            Token = (new WX_Token()).getToken(PUBLICIF);
            Token1 oToken = new Token1();
            oToken = JsonConvert.DeserializeObject<Token1>(Token);
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            Url = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token=" + oToken.result;

            if (obj.sMSGTYPE == "") 
            {
                msg = "没有传消息类型";
                return "";
            }
            //传微信号获取openid
            switch (obj.sMSGTYPE)
            {
                case "text":
                    postData = PreviewTextByOpenID(out msg, PUBLICID, updateValue);
                    break;
                case "image":
                    postData = PreviewImageByOpenID(out msg, PUBLICID, updateValue);
                    break;
                case "voice":
                    postData = PreviewVoiceByOpenID(out msg, PUBLICID, updateValue);
                    break;
                case "video":
                    postData = PreviewVideoByOpenID(out msg, PUBLICID, updateValue);
                    break;
                case "music": break;
                case "mpnews":
                    postData = PreviewNewsByOpenID(out msg, PUBLICID, updateValue);
                    break;
                case "wxcard":
                    postData = PreviewCardByOpenID(out msg, PUBLICID, updateValue);
                    break;
                default: break;
            }
            postData = Regex.Replace(postData, ",\"[^\"]+\":null", "");

            string wxstring = WXRequestString(out msg, context, postData);
            if (msg.Length > 0)
            {
                WriteToLog(msg);
            }

            return wxstring;
        }
        #region 预览文本消息
        public class WX_PreviewText
        {
            public string touser { get; set; }
            public Text text { get; set; }
            public string msgtype { get; set; }
        }

        public string PreviewTextByOpenID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_PreviewText tp_previewtext = new WX_PreviewText();

            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                string openid = getOpenid(out msg, query, PUBLICID,obj.sHYK_NO);
                tp_previewtext.touser = openid;
                tp_previewtext.text = new Text();
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_previewtext.text.content = query.FieldByName("CONTENT").AsString;
                    tp_previewtext.msgtype = query.FieldByName("TYPE").AsString;
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return JsonConvert.SerializeObject(tp_previewtext);
        }

        #endregion

        #region 预览图片消息
        public class WX_PreviewImage
        {
            public string touser { get; set; }
            public Image image { get; set; }
            public string msgtype { get; set; }
        }

        public string PreviewImageByOpenID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_PreviewImage tp_previewimage = new WX_PreviewImage();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                string openid = getOpenid(out msg, query, PUBLICID, obj.sHYK_NO);
                tp_previewimage.touser = openid;
                tp_previewimage.image = new Image();
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_previewimage.image.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_previewimage.msgtype = query.FieldByName("TYPE").AsString;
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return JsonConvert.SerializeObject(tp_previewimage);
        }

        #endregion

        #region 预览语音消息
        public class WX_PreviewVoice
        {
            public string touser { get; set; }
            public Voice voice { get; set; }
            public string msgtype { get; set; }
        }

        public string PreviewVoiceByOpenID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_PreviewVoice tp_previewvoice = new WX_PreviewVoice();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                string openid = getOpenid(out msg, query, PUBLICID, obj.sHYK_NO);
                tp_previewvoice.touser = openid;
                tp_previewvoice.voice = new Voice();

                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_previewvoice.voice.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_previewvoice.msgtype = query.FieldByName("TYPE").AsString;
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return JsonConvert.SerializeObject(tp_previewvoice);
        }

        #endregion

        #region 预览视频消息
        public class WX_PreviewVideo
        {
            public string touser { get; set; }
            public Mpvideo mpvideo { get; set; }
            public string msgtype { get; set; }
        }

        public string PreviewVideoByOpenID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_PreviewVideo tp_previewvideo = new WX_PreviewVideo();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                string openid = getOpenid(out msg, query, PUBLICID, obj.sHYK_NO);
                tp_previewvideo.touser = openid;
                tp_previewvideo.mpvideo = new Mpvideo();

                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_previewvideo.mpvideo.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_previewvideo.msgtype = query.FieldByName("TYPE").AsString;
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return JsonConvert.SerializeObject(tp_previewvideo);
        }

        #endregion

        #region 预览图文消息
        public class WX_PreviewNews
        {
            public string touser { get; set; }
            public Mpnews mpnews { get; set; }
            public string msgtype { get; set; }
        }

        public string PreviewNewsByOpenID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_PreviewNews tp_previewnews = new WX_PreviewNews();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                string openid = getOpenid(out msg, query, PUBLICID, obj.sHYK_NO);
                tp_previewnews.touser = openid;
                tp_previewnews.mpnews = new Mpnews();
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_previewnews.mpnews.media_id = query.FieldByName("MEDIA_ID").AsString;
                    tp_previewnews.msgtype = query.FieldByName("TYPE").AsString;
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return JsonConvert.SerializeObject(tp_previewnews);
        }

        #endregion

        #region 预览卡券消息
        public class Wxcard
        {
            public string card_id { get; set; }
        }

        public class WX_PreviewCard
        {
            public string touser { get; set; }
            public Wxcard wxcard { get; set; }
            public string msgtype { get; set; }
        }

        public string PreviewCardByOpenID(out string msg, string PUBLICID, string updateValue)
        {

            msg = string.Empty;
            CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(updateValue);
            WX_PreviewCard tp_previewcard = new WX_PreviewCard();
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            CyQuery query = new CyQuery(conn);
            try
            {
                string openid = getOpenid(out msg, query, PUBLICID, obj.sHYK_NO);
                tp_previewcard.touser = openid;
                tp_previewcard.wxcard = new Wxcard();
                query.SQL.Text = "select Q.*,T.TYPE from WX_QFXXDY Q,WX_ANSWERGZDEF T   where  Q.MSGTYPE=T.JLBH";
                if (obj.iJLBH != 0)
                {
                    query.SQL.Add(" and Q.JLBH=" + obj.iJLBH);
                }
                query.Open();
                if (!query.IsEmpty)
                {
                    tp_previewcard.wxcard.card_id = System.Configuration.ConfigurationManager.AppSettings["card_id"]; // query.FieldByName("MEDIA_ID").AsString;
                    tp_previewcard.msgtype = query.FieldByName("TYPE").AsString;
                }
                query.Close();
            }
            catch (Exception e)
            {

                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {

                conn.Close();
            }

            return JsonConvert.SerializeObject(tp_previewcard);
        }

        #endregion

        public string getOpenid(out string msg, CyQuery query,string publicid, string hykno)
        {
            msg = "";
            string openid = string.Empty;
            query.SQL.Text = "select U.OPENID from HYK_HYXX H,WX_HYKHYXX Y,WX_UNION U ";
            query.SQL.Add(" where H.HYID=Y.HYID and Y.UNIONID=U.UNIONID and U.PUBLICID=:PUBLICID and  H.HYK_NO=:HYN_NO");
            query.ParamByName("PUBLICID").AsInteger = Convert.ToInt32(publicid);
            query.ParamByName("HYN_NO").AsString = hykno;
            query.Open();
            if (!query.IsEmpty)
            {
                openid = query.FieldByName("OPENID").AsString;
            }
            else
            {
                msg = "openid不存在";
                
            }
            query.Close();
            return openid;
        }
    }
}


