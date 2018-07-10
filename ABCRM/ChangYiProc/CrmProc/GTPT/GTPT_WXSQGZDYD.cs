
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

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_WXSQGZDYD : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iGZID = 0;
        public int A = 0, iMDID, iHYKTYPE,iPUBLICID;

        public string sGZMC = string.Empty;
        public string sPUBLICNAME = string.Empty;


        public override bool IsValidStartData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "select count(*) Q from MOBILE_YHQGZ where GZID=:GZID";
            query.ParamByName("GZID").AsInteger = iGZID;
            query.Open();
            int A = query.FieldByName("Q").AsInteger;
            if (A <= 0)
            {
                msg = "请先定义送券发放规则！";
                return false;
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_YHQDYD", "JLBH", iJLBH);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iGZID", "W.GZID");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iQDR", "W.QDR");
            CondDict.Add("sQDRMC", "W.QDRMC");
            CondDict.Add("dQDRQ", "W.QDRQ");
            CondDict.Add("iZZR", "W.ZZR");
            CondDict.Add("sZZRMC", "W.ZZRMC");
            CondDict.Add("dZZRQ", "W.ZZRQ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.*,E.GZMC,P.PUBLICNAME from  MOBILE_YHQDYD W,MOBILE_YHQGZ E,WX_PUBLIC P ";
            query.SQL.Add(" WHERE W.GZID=E.GZID  and W.PUBLICID=P.PUBLICID");
            query.SQL.Add("AND W.JLBH is not null and  W.PUBLICID=" + iLoginPUBLICID);

            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSQGZDYD item = new GTPT_WXSQGZDYD();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iGZID = query.FieldByName("GZID").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.iQDR = query.FieldByName("QDR").AsInteger;
            item.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
            item.sGZMC = query.FieldByName("GZMC").AsString;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.sZZRMC = query.FieldByName("ZZRMC").AsString;
            item.sQDRMC = query.FieldByName("QDRMC").AsString;
            item.sPUBLICNAME = query.FieldByName("PUBLICNAME").AsString;
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return item;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_YHQDYD");

            query.SQL.Text = "insert into  MOBILE_YHQDYD(JLBH,GZID,KSRQ,JSRQ,DJR,DJRMC,DJSJ,STATUS,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:GZID,:KSRQ,:JSRQ,:DJR,:DJRMC,:DJSJ,:STATUS,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("GZID").AsInteger = iGZID;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ExecSQL();


        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_YHQDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }

        //启动
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            query.SQL.Text = "update MOBILE_YHQDYD set ZZR=:ZZR,ZZRQ=sysdate,STATUS=3,ZZRMC=:ZZRMC where STATUS=2 and  PUBLICID=:PUBLICID";
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            query.ExecSQL();

            query.SQL.Text = "update MOBILE_YHQDYD set QDR=:QDR,QDRMC=:QDRMC,QDSJ= sysdate,STATUS=2 where PUBLICID=:PUBLICID AND  JLBH=" + iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRMC").AsString = sLoginRYMC;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            query.ExecSQL();
        }

        //终止
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_YHQDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }



    }
}
