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

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_KCKCX_Srch : KCKXX
    {
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iSKJLBH;";
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sBGDDDM", "H.BGDDDM");
            CondDict.Add("iBJ_CZK", "D.BJ_CZK");
            CondDict.Add("iSTATUS", "H.STATUS");
            CondDict.Add("sBGRMC", "R.PERSON_NAME");

            query.SQL.Text = "select H.*,B.BGDDMC,R.PERSON_NAME,D.HYKNAME from MZKCARD H,HYKDEF D,HYK_BGDD B,RYXX R";
            query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM and H.BGR=R.PERSON_ID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_KCKCX_Srch item = new MZKGL_KCKCX_Srch();
            item.sCZKHM = query.FieldByName("CZKHM").AsString;
            //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.fQCYE = query.FieldByName("QCYE").AsFloat;
            item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
            item.fPDJE = query.FieldByName("PDJE").AsFloat;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            item.dXKRQ = FormatUtils.DateToString(query.FieldByName("XKRQ").AsDateTime);
            item.iBGR = query.FieldByName("BGR").AsInteger;
            item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
            item.iBJ_YK = query.FieldByName("BJ_YK").AsInteger;
            item.iSKJLBH = query.FieldByName("SKJLBH").AsInteger;
            return item;
        }
    }
}
