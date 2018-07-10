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
  public  class GTPT_WDCXXJLCX_Proc : BASECRMClass
    {
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public string sDCZT = string.Empty;
        public int iDJJLBH = 0;
        public int iID = 0;
        public string dDJSJ = string.Empty;
        public string sWX_NO = string.Empty;
        public int iNRID = 0;
        public int iCXID = 0;
        public string sNRMC = string.Empty;
        public string sMC = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();           
            if (iSEARCHMODE == 0)
            {
                CondDict.Add("iJLBH", "A.JLBH");
                CondDict.Add("dDJSJ", "A.DJSJ");
                CondDict.Add("iDJJLBH", "A.DJJLBH");
                CondDict.Add("sHYK_NO", "E.HYK_NO");
                CondDict.Add("sHY_NAME", "E.HY_NAME");
                CondDict.Add("sDCZT", "D.DCZT");
                CondDict.Add("sWX_NO", "B.WX_NO");
                CondDict.Add("iID", "A.ID");
                CondDict.Add("iNRID", "A.NRID");
                CondDict.Add("sMC", "B.MC");
                CondDict.Add("sNRMC", "C.NRMC");
                CondDict.Add("dDCRQ", "A.RQ");
                query.SQL.Text = " select E.HYK_NO,E.HY_NAME,A.DJSJ,D.DCZT,A.JLBH,A.DJJLBH,B.WX_NO";
                query.SQL.Add("  from MOBILE_DCJL A,WX_USER B,MOBILE_DCDYD D,HYK_HYXX E");
                query.SQL.Add("where A.DJJLBH=D.JLBH");
                query.SQL.Add("   AND A.OPENID=B.OPENID ");
                query.SQL.Add("   and A.HYID=E.HYID(+)");
                SetSearchQuery(query, lst);
            }
            if (iSEARCHMODE == 1)
            {
                query.SQL.Text = " select A.ID,B.MC,A.NRID,C.NRMC ";
                query.SQL.Add("  from MOBILE_DCJLITEM A,MOBILE_DCNRDEF B,MOBILE_DCNRDEF_ITEM C ");
                query.SQL.Add("   where  A.ID=B.ID and A.ID=C.ID ");
                query.SQL.Add("   and A.NRID=C.NRID");
                query.SQL.Add("   AND A.JLBH=" + iJLBH);
                SetSearchQuery(query, lst,false);
            }
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            if (iSEARCHMODE == 0)
            {
                GTPT_WDCXXJLCX_Proc item = new GTPT_WDCXXJLCX_Proc();
                item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                item.sDCZT = query.FieldByName("DCZT").AsString;
                item.iDJJLBH = query.FieldByName("DJJLBH").AsInteger;
                item.iJLBH = query.FieldByName("JLBH").AsInteger;
                item.dDJSJ = FormatUtils.DateToString(query.FieldByName("DJSJ").AsDateTime);
                item.sWX_NO = query.FieldByName("WX_NO").AsString;
                return item;
            }
            else
            {
                GTPT_WDCXXJLCX_Proc item = new GTPT_WDCXXJLCX_Proc();
                item.iID = query.FieldByName("ID").AsInteger;
                item.iNRID = query.FieldByName("NRID").AsInteger;
                item.sMC = query.FieldByName("MC").AsString;
                item.sNRMC = query.FieldByName("NRMC").AsString;
                return item;
            }

        }


    }
}
