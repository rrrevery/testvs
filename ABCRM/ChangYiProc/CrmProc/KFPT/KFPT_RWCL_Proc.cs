using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

namespace BF.CrmProc.KFPT
{
    public class KFPT_RWCL_Proc : KFPT_RWZX_Proc
    {
        public string sLDPY = string.Empty;
        public int iPYR = 0;
        public string sPYRMC = string.Empty;
        public string dPYRQ = string.Empty;
        public double fFZ = 0;

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update RWJL set STATUS=2,LDPY=:LDPY,FZ=:FZ,PYR=:PYR,PYRMC=:PYRMC,PYRQ=:PYRQ where JLBH=" + iJLBH;
            query.ParamByName("LDPY").AsString = sLDPY;
            query.ParamByName("FZ").AsFloat = fFZ;
            query.ParamByName("PYR").AsInteger = iPYR;
            query.ParamByName("PYRMC").AsString = sPYRMC;
            query.ParamByName("PYRQ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "J.JLBH");
            CondDict.Add("iSTATUS", "J.STATUS");
            CondDict.Add("sSTATUS", "J.STATUS");
            CondDict.Add("iPYR", "J.PYR");
            CondDict.Add("dPYRQ", "J.PYRQ");
            CondDict.Add("iJLBH_RW", "I.JLBH");
            CondDict.Add("dKSRQ", "J.KSRQ");
            CondDict.Add("dJSRQ", "J.JSRQ");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select J.*,R.PERSON_NAME ";
            query.SQL.Add(" from RWJL J, RYXX R,RWQFJLITEM I");
            query.SQL.Add(" where J.RWDX=R.PERSON_ID and J.JLBH=I.JLBH_RW(+)");
            query.SQL.Add(" and J.STATUS!=0");  //只能查询到已执行过的任务进行处理
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_RWCL_Proc obj = new KFPT_RWCL_Proc();
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
            obj.sLDPY = query.FieldByName("LDPY").AsString;
            obj.fFZ = query.FieldByName("FZ").AsFloat;
            obj.iPYR = query.FieldByName("PYR").AsInteger;
            obj.sPYRMC = query.FieldByName("PYRMC").AsString;
            obj.dPYRQ = FormatUtils.DatetimeToString(query.FieldByName("PYRQ").AsDateTime);
            return obj;
        }
    }
}
