using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BF.CrmProc.GTPT
{
    public class GTPT_NEWSDY_Proc : DJLR_ZX_CLass
    {
        public string sNAME = string.Empty;
        public string sMEDIA_ID = string.Empty;
        public string dCREATETIME = string.Empty;
        public int iPUBLICID = 0;
        public int iTYPE = 0;
        public List<NEWSDY_ITEM> itemTable = new List<NEWSDY_ITEM>();
        public class NEWSDY_ITEM 
        {
            public int iINX = 0;
            public string sTITLE = string.Empty;
            public string sTHUMB_MEDIA_ID = string.Empty;
            public string sTHUMB_TITLE = string.Empty;
            public string sAUTHOR = string.Empty;
            public string sDIGEST = string.Empty;
            public int iBJ_COVER = 0;
            public string sCONTNET = string.Empty;
            public string sYWURL = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_MEDIADY;WX_NEWSDY_ITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iJLBH != 0)
                DeleteDataQuery(out msg, query, serverTime);
            else
                iJLBH = SeqGenerator.GetSeq("WX_MEDIADY");
            query.SQL.Text = "insert into WX_MEDIADY(JLBH,TITLE,TYPE,DJR,DJRMC,DJSJ,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:TITLE,:TYPE,:DJR,:DJRMC,:DJSJ,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("TITLE").AsString = sNAME;
            query.ParamByName("TYPE").AsInteger = iTYPE;// 6图文 
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ExecSQL();

            foreach (NEWSDY_ITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_NEWSDY_ITEM(JLBH,INX,TITLE,THUMB_MEDIA_ID,AUTHOR,DIGEST,BJ_COVER,CONTNET,YWURL)";
                query.SQL.Add(" values(:JLBH,:INX,:TITLE,:THUMB_MEDIA_ID,:AUTHOR,:DIGEST,:BJ_COVER,:CONTNET,:YWURL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("TITLE").AsString = one.sTITLE;
                query.ParamByName("THUMB_MEDIA_ID").AsString = one.sTHUMB_MEDIA_ID;
                query.ParamByName("AUTHOR").AsString = one.sAUTHOR;
                query.ParamByName("DIGEST").AsString = one.sDIGEST;
                query.ParamByName("BJ_COVER").AsInteger = one.iBJ_COVER;
                query.ParamByName("CONTNET").AsString = one.sCONTNET;
                query.ParamByName("YWURL").AsString = one.sYWURL;
                query.ExecSQL();
            }


        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {

            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "F.JLBH");
            CondDict.Add("iDJR", "F.DJR");
            CondDict.Add("sDJRMC", "F.DJRMC");
            CondDict.Add("dDJSJ", "F.DJSJ");
            CondDict.Add("iPUBLICID", "F.PUBLICID");

            query.SQL.Text = "select * from WX_MEDIADY F where TYPE in(6,7)";
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,M.TITLE THUMB_TITLE from WX_NEWSDY_ITEM I, WX_MEDIADY M where I.THUMB_MEDIA_ID =M.MEDIA_ID and I.JLBH=" + iJLBH;

                query.Open();
                while (!query.Eof)
                {
                    NEWSDY_ITEM item = new NEWSDY_ITEM();
                    ((GTPT_NEWSDY_Proc)lst[0]).itemTable.Add(item);
                    item.iINX = query.FieldByName("INX").AsInteger;
                    item.sTITLE = query.FieldByName("TITLE").AsString;
                    item.sTHUMB_MEDIA_ID = query.FieldByName("THUMB_MEDIA_ID").AsString;
                    item.sTHUMB_TITLE = query.FieldByName("THUMB_TITLE").AsString;
                    item.sAUTHOR = query.FieldByName("AUTHOR").AsString;
                    item.sDIGEST = query.FieldByName("DIGEST").AsString;
                    item.iBJ_COVER = query.FieldByName("BJ_COVER").AsInteger;
                    item.sCONTNET = query.FieldByName("CONTNET").AsString;
                    item.sYWURL = query.FieldByName("YWURL").AsString;
                    query.Next();
                }
                query.Close();

            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_NEWSDY_Proc obj = new GTPT_NEWSDY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sNAME = query.FieldByName("TITLE").AsString;
            obj.iTYPE = query.FieldByName("TYPE").AsInteger;
            obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            return obj;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            ExecTable(query, "WX_MEDIADY", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ");

        }
    }
}
