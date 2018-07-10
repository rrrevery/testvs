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
    public class GTPT_WXJPFFJLCX_Srch : BASECRMClass
    {
        public int iJC = 0, iDJLX, iLPLX, iLPID, iFFSL, iSTATUS;
        public string dDJSJ = string.Empty;
        public string dLJYXQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sWX_NAME = string.Empty;
        public string sWX_NO = string.Empty;
        public string sJCMC = string.Empty;
        public string dLJSJ = string.Empty;
        public string sJPMC = string.Empty;
        public double fJE = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JYBH");
            CondDict.Add("iJC","A.JC");
            CondDict.Add("sWX_NO", "A.WX_NO");
            CondDict.Add("dLJSJ", "A.LJSJ");
            CondDict.Add("iSTATUS", "A.STATUS");
            CondDict.Add("dLJYXQ","A.LJYXQ");
            CondDict.Add("iDJLX","A.DJLX");
            CondDict.Add("dDJSJ","A.DJSJ");
            query.SQL.Text = "select * from (";
            query.SQL.Add("select A.JYBH,A.JC,A.LJYXQ,A.DJLX,A.DJSJ,B.LPID JPID,B.LPLX,B.JSRQ,B.STATUS,B.LJSJ,D.WX_NO,F.MC JCMC,B.JE, ");
            query.SQL.Add("(case B.LPLX when 0 then (select C1.LPMC from HYK_JFFHLPXX C1 where C1.LPID=B.LPID)");
            query.SQL.Add("when 1 then (select C2.YHQMC from YHQDEF C2 where C2.YHQID=B.LPID) else '积分' end) as JPMC");
            query.SQL.Add("from MOBILE_LPFFZX_HY_MX A,MOBILE_LPFFZX_HY_MXITEM B,WX_USER D,MOBILE_JCDEF F");
            query.SQL.Add("where A.JYBH=B.JYBH and A.OPENID=D.OPENID and A.JC=F.JC");
            query.SQL.Add(" ) A where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXJPFFJLCX_Srch obj = new GTPT_WXJPFFJLCX_Srch();
            obj.iJLBH = query.FieldByName("JYBH").AsInteger;
            obj.iJC = query.FieldByName("JC").AsInteger;
            obj.dLJYXQ = FormatUtils.DateToString(query.FieldByName("LJYXQ").AsDateTime);
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.dJSRQ = FormatUtils.DatetimeToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.sWX_NO = query.FieldByName("WX_NO").AsString;
            obj.iLPLX = query.FieldByName("LPLX").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sJCMC = query.FieldByName("JCMC").AsString;
            obj.sJPMC = query.FieldByName("JPMC").AsString;
            obj.fJE = query.FieldByName("JE").AsInteger;
            obj.dLJSJ = FormatUtils.DateToString(query.FieldByName("LJSJ").AsDateTime);
            return obj;
        }

    }
}
