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
    public class MZKGL_KCKHZ_Srch : HYK_DJLR_CLass
    {
        public int iZSL = 0, iBGR = 0;
        public double fZYE = 0;
        public string sBGRMC = string.Empty;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sBGDDDM", "H.BGDDDM");
            CondDict.Add("sHYKNO", "H.CZKHM");
            CondDict.Add("sBGRMC", "R.PERSON_NAME");

            query.SQL.Text = "select D.HYKNAME,B.BGDDMC,H.BGR,R.PERSON_NAME,sum(H.QCYE) ZYE,count(*) ZSL from MZKCARD H,HYKDEF D,HYK_BGDD B,RYXX R";
            query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM and H.BGR=R.PERSON_ID");
            //query.SQL.Add(" and H.STATUS=" + obj.iSTATUS);
            //MakeSrchCondition(query, condition, asFieldNames, "group by D.HYKNAME,B.BGDDMC,H.BGR,R.PERSON_NAME");
            SetSearchQuery(query, lst, true, "group by D.HYKNAME,B.BGDDMC,H.BGR,R.PERSON_NAME");

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_KCKHZ_Srch item = new MZKGL_KCKHZ_Srch();
            //  item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.iZSL = query.FieldByName("ZSL").AsInteger;
            item.fZYE = query.FieldByName("ZYE").AsFloat;
            // item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            //item.iBGR = query.FieldByName("BGR").AsInteger;                        
            item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
            return item;
        }
    }
}
