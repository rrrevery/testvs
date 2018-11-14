using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

namespace BF.CrmProc.KFPT
{
    public class KFPT_RWZX_Proc : DJLR_ZX_CLass
    {
        public string sRWZT = string.Empty;
        public string sRW = string.Empty;
        public int iRWDX = 0;
        public string sPERSON_NAME = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sWCQK = string.Empty;
        public string sHYKNO = string.Empty;
        public string sHYNAME = string.Empty;
        public int iRWWCZT = 0;
        public string sRWWCZT 
        {
            get {
                if (iRWWCZT == 0)
                    return "未完成";
                else if (iRWWCZT == 1)
                    return "已完成";
                else
                    return "";

            }
        }
        public string sSTATUS
        {
            get
            {
                if (iSTATUS == 0)
                    return "未执行";
                else if (iSTATUS == 1)
                    return "已执行";
                else if (iSTATUS == 2)
                    return "已处理";
                else
                    return "";
            }
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "RWJL", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update RWJL set RWWCZT=:RWWCZT,WCQK=:WCQK where JLBH=" + iJLBH;
            query.ParamByName("RWWCZT").AsInteger = iRWWCZT;
            query.ParamByName("WCQK").AsString = sWCQK;
            query.ExecSQL();
            ExecTable(query, "RWJL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);//STATUS (0未执行，1已执行，2已处理)
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "J.JLBH");
            CondDict.Add("iSTATUS", "J.STATUS");
            CondDict.Add("iZXR", "J.ZXR");
            CondDict.Add("dKSRQ", "J.KSRQ");
            CondDict.Add("dJSRQ", "J.JSRQ");
            CondDict.Add("iRWWCZT", "J.RWWCZT");
            CondDict.Add("sRWWCZT", "J.RWWCZT");
            CondDict.Add("iJLBH_RW", "I.JLBH");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select J.*,R.PERSON_NAME ";
            query.SQL.Add(" from RWJL J, RYXX R,RWQFJLITEM I");
            query.SQL.Add("  where J.RWDX=R.PERSON_ID and J.JLBH=I.JLBH_RW(+)");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_RWZX_Proc obj = new KFPT_RWZX_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sRWZT = query.FieldByName("RWZT").AsString;
            obj.sRW = query.FieldByName("RW").AsString;
            obj.iRWDX = query.FieldByName("RWDX").AsInteger;
            obj.sPERSON_NAME = query.FieldByName("PERSON_NAME").AsString;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.sWCQK = query.FieldByName("WCQK").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iRWWCZT = query.FieldByName("RWWCZT").AsInteger;
            obj.sHYKNO = query.FieldByName("HYKNO").AsString;
            obj.sHYNAME = query.FieldByName("HYNAME").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            return obj;
        }
    }
}
