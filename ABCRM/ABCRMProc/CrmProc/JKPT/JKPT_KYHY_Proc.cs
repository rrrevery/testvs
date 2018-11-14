using BF;

using BF.Pub;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace BF.CrmProc.JKPT
{
    public class JKPT_KYHY_Proc : DZHYK_DJLR_CLass
    {
        public string dXFRQ = string.Empty;
        public int iXFNY = 0;
        public int iSEX = 0;
        public string sSEX = string.Empty;
        public string sSJHM = string.Empty;
        public int iBJ_CL = 0;
        public string sSTATUS = string.Empty;
        public int iBJ_KY = 0;
        public string sBJ_KY = string.Empty;
        public int iMDID = 0;
        public string sMDMC = string.Empty;


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_KYJL;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, Pub.CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYK_KYJL");
            }

            query.SQL.Text = "insert into HYK_KYJL(JLBH,HYID,DJR,DJRMC,RQ,BZ,XFRQ,XFNY,BJ_KY,MDID)";
            query.SQL.Add("values(:JLBH,:HYID,:DJR,:DJRMC,:RQ,:BZ,:XFRQ,:XFNY,:BJ_KY,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("RQ").AsDateTime = serverTime;
            query.ParamByName("XFRQ").AsString = dXFRQ;
            query.ParamByName("XFNY").AsInteger = iXFNY;
            query.ParamByName("BJ_KY").AsInteger = iBJ_KY;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("BZ").AsString = sBZ;
            query.ExecSQL();
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_KYJL", serverTime);
            query.SQL.Text = "update HYK_HYXX set BJ_KY=" + iBJ_KY + " where HYID=" + iHYID;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "R.MDID");
            CondDict.Add("iJLBH", "R.JLBH");
            CondDict.Add("iHYID", "R.HYID");
            CondDict.Add("dRQ", "R.RQ");
            CondDict.Add("sBZ", "R.BZ");
            CondDict.Add("iDJR", "R.DJR");
            CondDict.Add("iZXR", "R.ZXR");

            query.SQL.Text = "select R.*,X.HYK_NO,F.HYKNAME,X.HY_NAME,G.SEX,G.SJHM,X.STATUS,M.MDMC";
            query.SQL.Add(" from HYK_KYJL R,HYK_HYXX X,HYK_GRXX G,HYKDEF F,MDDY M");
            query.SQL.Add(" where R.HYID=X.HYID and R.HYID=G.HYID(+) and X.HYKTYPE=F.HYKTYPE and R.MDID = M.MDID(+)");
            if (sHYK_NO != "")
            {
                query.SQL.Add(" R.HYID = (select HYID from HYK_HYXX where HYK_NO='" + sHYK_NO+"'");
                query.SQL.Add(" union  select HYID from HYK_CHILD_JL where HYK_NO='" + sHYK_NO +"')");
            }

            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            JKPT_KYHY_Proc obj = new JKPT_KYHY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iSEX = query.FieldByName("SEX").AsInteger;
            obj.sSEX = CrmLibProc.GetSexStr_01(obj.iSEX);
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.iXFNY = query.FieldByName("XFNY").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sSJHM = query.FieldByName("SJHM").AsString;
            obj.dXFRQ = FormatUtils.DateToString(query.FieldByName("XFRQ").AsDateTime);
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.iBJ_CL = query.FieldByName("BJ_CL").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sSTATUS = CrmLibProc.GetHYKStatusName(obj.iSTATUS);
            obj.iBJ_KY = query.FieldByName("BJ_KY").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iBJ_KY = query.FieldByName("BJ_KY").AsInteger;
            if (obj.iBJ_KY == 1)
                obj.sBJ_KY = "可疑会员";
            else
                obj.sBJ_KY = "非可疑会员";
            return obj;
        }
    }
}
