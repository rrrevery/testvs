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
    public class GTPT_WXKKBKZLJLCX_Srch : BASECRMClass
    {
        public int iLX = 0, iHYID = 0, iLPLX = 0, iLPID = 0, iSTATUS = 0;
        public string dDJSJ = string.Empty;
        public string dLJYXQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sOPENID = string.Empty;
        public string sWX_NO = string.Empty;
        public string sJCMC = string.Empty;
        public string dLJSJ = string.Empty;
        public string sJPMC = string.Empty;
        public double fJE = 0;
        public string sHYK_NO = string.Empty;
        public string sLPLXMC
        {
            get
            {
                if (iLPLX == 0)
                    return "礼品";
                else if (iLPLX == 1)
                    return "优惠券";
                else if (iLPLX == 2)
                    return "积分";
                else
                    return "";

            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHYK_NO", "E.HYK_NO");
            CondDict.Add("iLX", "B.LX");
            CondDict.Add("iSTATUS", "H.STATUS");
            CondDict.Add("iJLBH", "B.JLBH");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("dLJSJ", "A.DJSJ");
            CondDict.Add("dJSRQ", "G.JSRQ");
            CondDict.Add("dLJYXQ", "A.LJYXQ");
            CondDict.Add("sJPMC", "F.LBMC");
            CondDict.Add("sLPLXMC", "B.LX");

            query.SQL.Text = "select A.*,B.LX,E.HYK_NO,F.LBMC,H.JSRQ,H.JE,H.LPLX,H.STATUS from";
            query.SQL.Add(" MOBILE_CARD_LPFFJL A,MOBILE_CARD_LPFFGZ B,HYK_HYXX E");
            query.SQL.Add(" ,MOBILE_LPZDEF_YHQ  F,MOBILE_LPFFZX_HY_MXITEM H,MOBILE_LPFFZX_HY_MX M");
            query.SQL.Add(" where A.DJJLBH=B.JLBH  and F.LBID=A.LBID   AND H.JYBH=M.JYBH AND A.JYBH=M.DJJLBH");
            query.SQL.Add("  and A.HYID=E.HYID and F.LBID=M.LBID and H.LBID=F.LBID  and (DJLX=5 or DJLX=6)");
            SetSearchQuery(query, lst);

            return lst;

        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXKKBKZLJLCX_Srch obj = new GTPT_WXKKBKZLJLCX_Srch();
            obj.sOPENID = query.FieldByName("OPENID").AsString;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.dLJYXQ = FormatUtils.DateToString(query.FieldByName("LJYXQ").AsDateTime);
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.iLPLX = query.FieldByName("LPLX").AsInteger;
            obj.iLPID = query.FieldByName("LBID").AsInteger;
            obj.fJE = query.FieldByName("JE").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sJPMC = query.FieldByName("LBMC").AsString;
            return obj;
        }
    }
}
