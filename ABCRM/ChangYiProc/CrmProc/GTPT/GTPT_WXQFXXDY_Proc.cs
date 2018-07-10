using BF.Pub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXQFXXDY_Proc : DJLR_ZX_CLass
    {
        public int iQFDX = 0;
        public int iMSGTYPE = 0;
        public string sTYPENAME = string.Empty;
        public string sMEDIA_ID = string.Empty;
        public string sMEDIA_TITLE = string.Empty;
        public string sCONTENT = string.Empty;
        public int iTAGID = 0;
        public string sTAGMC = string.Empty;
        public int iBJ_ZZ = 0;
        public string iMSG_ID = string.Empty;
        public int iMSG_DATA_ID = 0;
        public int iCOUNT = 0;
        public string sQFDX
        {
            get 
            {
                if (iQFDX == 1)
                    return "按标签 ";
                else if (iQFDX == 2)
                    return "按用户 ";
                else
                    return "全部";
            }
        }
        public string sSTATUS
        {
            get
            {
                switch (iSTATUS)
                {
                    case 1:return "保存 ";
                    case 2: return "审核 ";
                    case 3: return "群发成功 ";
                    case 4: return "群发失败 ";
                    case 5: return "删除群发 ";
                default :return "";
                }
            }
        }

        public List<WX_QFXX_YHLIST> itemTable = new List<WX_QFXX_YHLIST>();
        public class WX_QFXX_YHLIST
        {
            public string sOPENID = string.Empty;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iHYID = 0;
        }

        
        public override void DeleteDataQuery(out string msg, Pub.CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_QFXXDY;WX_QFXX_YHLIST", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, Pub.CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iJLBH != 0)
                DeleteDataQuery(out msg, query, serverTime);
            else
                iJLBH = SeqGenerator.GetSeq("WX_QFXXDY");
            query.SQL.Text = "insert into WX_QFXXDY(JLBH,QFDX,MSGTYPE,MEDIA_ID,CONTENT,TAGID,BJ_ZZ,DJR,DJRMC,DJSJ,STATUS)";
            query.SQL.Add(" values(:JLBH,:QFDX,:MSGTYPE,:MEDIA_ID,:CONTENT,:TAGID,:BJ_ZZ,:DJR,:DJRMC,:DJSJ,1)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("QFDX").AsInteger = iQFDX;
            query.ParamByName("MSGTYPE").AsInteger = iMSGTYPE;
            query.ParamByName("MEDIA_ID").AsString = sMEDIA_ID;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("TAGID").AsInteger = iTAGID;
            query.ParamByName("BJ_ZZ").AsInteger = iBJ_ZZ;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();

            foreach (WX_QFXX_YHLIST one in itemTable)
            {
                query.SQL.Text="insert into WX_QFXX_YHLIST(JLBH,OPENID) values(:JLBH,:OPENID)";
                query.ParamByName("JLBH").AsInteger=iJLBH;
                query.ParamByName("OPENID").AsString=one.sOPENID;
                query.ExecSQL();
            }

        }
        public override List<object> SearchDataQuery(Pub.CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "Q.JLBH");
            CondDict.Add("iDJR", "Q.DJR");
            CondDict.Add("sDJRMC", "Q.DJRMC");
            CondDict.Add("dDJSJ", "Q.DJSJ");
            CondDict.Add("iMSGTYPE", "Q.MSGTYPE");
            CondDict.Add("iZXR", "Q.ZXR");
            CondDict.Add("sZXRMC", "Q.ZXRMC");
            CondDict.Add("dZXRQ", "Q.ZXRQ");
            CondDict.Add("iSTATUS", "Q.STATUS");
            List<object> lst = new List<object>();
            query.SQL.Text=" select Q.*,T.NAME TYPENAME,B.TAGMC,B.COUNT,M.TITLE from WX_QFXXDY Q,WX_ANSWERGZDEF T,WX_BQDY B,WX_MEDIADY M";
            query.SQL.Add("  where Q.TAGID=B.TAGID(+) and Q.MSGTYPE=T.JLBH and Q.MEDIA_ID=M.MEDIA_ID(+) "); 

            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select L.*,Y.HYID,H.HYK_NO,H.HY_NAME from WX_QFXX_YHLIST L,WX_UNION U,WX_HYKHYXX Y,HYK_HYXX H";
                query.SQL.Add(" where L.OPENID=U.OPENID and U.UNIONID=Y.UNIONID and Y.HYID=H.HYID and U.PUBLICID=:PUBLICID and JLBH=" + iJLBH);
                query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
                query.Open();
                while (!query.Eof)
                {
                    WX_QFXX_YHLIST item=new WX_QFXX_YHLIST();
                    ((GTPT_WXQFXXDY_Proc)lst[0]).itemTable.Add(item);
                    item.sOPENID = query.FieldByName("OPENID").AsString;
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }

        public override object SetSearchData(Pub.CyQuery query)
        {
            GTPT_WXQFXXDY_Proc obj = new GTPT_WXQFXXDY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iMSGTYPE = query.FieldByName("MSGTYPE").AsInteger;
            obj.sTYPENAME = query.FieldByName("TYPENAME").AsString;
            obj.iQFDX = query.FieldByName("QFDX").AsInteger;
            obj.iTAGID = query.FieldByName("TAGID").AsInteger;
            obj.sTAGMC = query.FieldByName("TAGMC").AsString;
            obj.iCOUNT = query.FieldByName("COUNT").AsInteger;
            obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
            obj.sMEDIA_TITLE = query.FieldByName("TITLE").AsString;
            obj.sCONTENT = query.FieldByName("CONTENT").AsString;
            obj.iBJ_ZZ = query.FieldByName("BJ_ZZ").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iMSG_ID = query.FieldByName("MSG_ID").AsString;
            //obj.iMSG_DATA_ID = Convert.ToInt32(query.FieldByName("MSG_DATA_ID").AsString);
            return obj;
        }


        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            ExecTable(query, "WX_QFXXDY", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 2);

        }
    }
}
