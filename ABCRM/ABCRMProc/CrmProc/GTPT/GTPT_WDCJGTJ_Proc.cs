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
    public class GTPT_WDCJGTJ_Proc : BASECRMClass
    {
        public int iID = 0;
        public int iNRID = 0;
        public string sDCZT = string.Empty;
        public string sMC = string.Empty;
        public string sNRMC = string.Empty;
        public int iTPS = 0;
        public double fBFB = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","C.JLBH");
            CondDict.Add("iID","D.ID");
            CondDict.Add("iNRID", "F.NRID");
            CondDict.Add("sDCZT", "C.DCZT");
            CondDict.Add("sMC", "E.MC");
            CondDict.Add("sNRMC", "F.NRMC");
            CondDict.Add("sSKTNO", "Q.SKTNO");
            CondDict.Add("fWCLJFBD", "Q.WCLJFBD");
            CondDict.Add("fDHJE", "Q.DHJE");
            CondDict.Add("iXPH", "Q.XPH");
            CondDict.Add("dDCRQ", "A.RQ");
            query.SQL.Text = "select C.JLBH,D.ID,F.NRID,C.DCZT,E.MC,F.NRMC,count(*) TPS,0.00 BFB";
            query.SQL.Add("from MOBILE_DCJL A,MOBILE_DCDYD C,MOBILE_DCDYDITEM_TM D, MOBILE_DCNRDEF E,MOBILE_DCNRDEF_ITEM F,MOBILE_DCJLITEM I");
            query.SQL.Add(" where A.DJJLBH=C.JLBH and C.JLBH=D.JLBH and D.ID=E.ID and E.ID=F.ID ");
            query.SQL.Add("and A.JLBH=I.JLBH and I.ID=E.ID and I.NRID=F.NRID");
            //MakeSrchCondition(query, "group by C.JLBH,D.ID,F.NRID,C.DCZT,E.MC,F.NRMC");
            SetSearchQuery(query, lst, true, "group by C.JLBH,D.ID,F.NRID,C.DCZT,E.MC,F.NRMC");
     
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_WDCJGTJ_Proc obj = new GTPT_WDCJGTJ_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.iNRID = query.FieldByName("NRID").AsInteger;
            obj.sDCZT = query.FieldByName("DCZT").AsString;
            obj.sMC = query.FieldByName("MC").AsString;
            obj.sNRMC = query.FieldByName("NRMC").AsString;
            obj.iTPS = query.FieldByName("TPS").AsInteger;
            obj.fBFB = query.FieldByName("BFB").AsFloat;
            return obj;
        }

    }
}
