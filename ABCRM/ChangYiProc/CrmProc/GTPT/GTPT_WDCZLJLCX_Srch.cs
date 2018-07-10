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
    public class GTPT_WDCZLJLCX_Srch : BASECRMClass
    {
        public int iDJJLBH = 0, iLPLX, iLPID, iSTATUS, iHYID,iLBID;
        public string dDJSJ = string.Empty;
        public string dJSRQ = string.Empty;
        public string dLJYXQ = string.Empty;
        public string dRQ = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sWX_NO = string.Empty;
        public string dLJSJ = string.Empty;
        public string sJPMC = string.Empty;
        public string sLBMC = string.Empty;

        public double fJE = 0;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","A.JLBH");
            CondDict.Add("dRQ", "A.RQ");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iDJJLBH", "A.DJJLBH");
            CondDict.Add("sOPENID", "A.OPENID");
            CondDict.Add("iHYID", "A.HYID");
            CondDict.Add("iLPID", "A.LPID");
            CondDict.Add("dJSRQ", "A.JSRQ");
            CondDict.Add("fJE", "A.JE");
            CondDict.Add("dLJYXQ", "A.LJYXQ");
            CondDict.Add("iSTATUS", "C.STATUS");
            CondDict.Add("dLJSJ", "A.LJSJ");
            CondDict.Add("sHYK_NO", "A.HYK_NO");


            query.SQL.Text = "select A.*,D.HYK_NO,P.LBMC ,C.STATUS STATUSS,C.LJSJ,C.JSRQ";
            query.SQL.Add("from MOBILE_DCJL A,HYK_HYXX D,MOBILE_LPZDEF_YHQ P,MOBILE_LPFFZX_HY_MX B,MOBILE_LPFFZX_HY_MXITEM C");
            query.SQL.Add("where A.LBID=P.LBID AND B.JYBH=C.JYBH AND A.JLBH=B.DJJLBH AND C.LBID=A.LBID");
            query.SQL.Add("   and A.HYID=D.HYID(+)");


            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {

            GTPT_WDCZLJLCX_Srch obj = new GTPT_WDCZLJLCX_Srch();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iDJJLBH = query.FieldByName("DJJLBH").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.dLJSJ = FormatUtils.DatetimeToString(query.FieldByName("LJSJ").AsDateTime);
            obj.iLBID = query.FieldByName("LBID").AsInteger;
            obj.sLBMC = query.FieldByName("LBMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUSS").AsInteger;
            obj.dLJYXQ = FormatUtils.DatetimeToString(query.FieldByName("LJYXQ").AsDateTime);

            return obj;
        }


    }
}
