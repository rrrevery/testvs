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
    public class GTPT_WXYYFWJLCX_Srch : BASECRMClass
    {
        public int iID = 0;
        public string sHYK_NO = string.Empty;
        public string dRQ = string.Empty;
        public string sGKXM = string.Empty;
        public string sLXDH = string.Empty;
        public string sWX_NO = string.Empty;
        public string dDJSJ = string.Empty;
        public string sMC = string.Empty;
        public string sOPENID = string.Empty;
        public int iHYID = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string sBZ = string.Empty;
        public int iSTATUS = 0;
        public string sSTATUS
        {
            get 
            {
                if (iSTATUS == 0)
                    return "已取消";
                else if (iSTATUS == 1)
                    return "已预约";
                else
                    return "";
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","L.JLBH");
            CondDict.Add("iHYID", "L.HYID");
            CondDict.Add("iOPENID", "L.OPENID");
            CondDict.Add("sHYK_NO", "X.HYK_NO");
            CondDict.Add("sGKXM", "L.GKXM");
            CondDict.Add("dDJSJ", "L.DJSJ");
            CondDict.Add("sWX_NO", "W.WX_NO");
            CondDict.Add("iTYPE", "F.TYPE");
            CondDict.Add("sMC", "F.MC");
            CondDict.Add("iBJ_CJ","L.BJ_CJ");
            CondDict.Add("iSTATUS", "L.STATUS");

            query.SQL.Text = "select L.*,F.MC FWLBMC,F.YYNR,X.HYK_NO,F.TYPE,G.SJHM ,X.HY_NAME,M.MDMC ";
            query.SQL.Add("from MOBILE_YDFWJL L,MOBILE_YDFWDEF F,HYK_HYXX X,HYK_GRXX G,WX_MDDY M");
            query.SQL.Add(" where L.ID=F.ID  and L.HYID=G.HYID(+) and L.HYID=X.HYID(+) and L.MDID=M.MDID ");
            query.SQL.Add(" and  L.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXYYFWJLCX_Srch obj = new GTPT_WXYYFWJLCX_Srch();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.dRQ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sMC = query.FieldByName("FWLBMC").AsString;
            obj.sGKXM = query.FieldByName("HY_NAME").AsString;
            obj.sLXDH = query.FieldByName("SJHM").AsString;
            obj.sOPENID = query.FieldByName("OPENID").AsString;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return obj;
        }

    }
}
