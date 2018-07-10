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
    public class GTPT_WDCYHCX_Srch : BASECRMClass
    {
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public string sSJHM = string.Empty;
        public string dRQ = string.Empty;
        public string sWX_NO = string.Empty;
        public string sDCZT = string.Empty;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHY_NAME","E.HY_NAME");
            CondDict.Add("sSJHM", "C.SJHM");
            CondDict.Add("dRQ", "A.RQ");
            CondDict.Add("sDCZT", "D.DCZT");
            CondDict.Add("sWX_NO", "B.WX_NO");
            query.SQL.Text = "select E.HYK_NO,E.HY_NAME,C.SJHM,A.RQ,D.DCZT,B.WX_NO  ";
            query.SQL.Add("from MOBILE_DCJL A,WX_USER B,HYK_GRXX C,MOBILE_DCDYD D,HYK_HYXX E");
            query.SQL.Add(" where A.OPENID=B.OPENID  and A.DJJLBH=D.JLBH");
            query.SQL.Add("and A.HYID=E.HYID(+)  and E.HYID=C.HYID(+)");
            SetSearchQuery(query, lst);


           return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WDCYHCX_Srch obj = new GTPT_WDCYHCX_Srch();
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sSJHM = query.FieldByName("SJHM").AsString;
            obj.dRQ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
            obj.sDCZT = query.FieldByName("DCZT").AsString;
            obj.sWX_NO = query.FieldByName("WX_NO").AsString;
            return obj;
        }
    }
}
