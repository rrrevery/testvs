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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_KCKHZ_Srch : HYK_DJLR_CLass
    {
        public int iZSL = 0, iBGR = 0;
        public double fZYE = 0;
        public string sBGRMC = string.Empty;
 

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sBGDDDM", "H.BGDDDM");
            CondDict.Add("iMDID", "B.MDID");
            CondDict.Add("sBGRMC", "R.PERSON_NAME");
            query.SQL.Text = "select D.HYKNAME,B.BGDDMC,H.BGR,R.PERSON_NAME,sum(H.QCYE) ZYE,count(*) ZSL from HYKCARD H,HYKDEF D,HYK_BGDD B,RYXX R";
            query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM and H.BGR=R.PERSON_ID");
            MakeSrchCondition(query, "group by D.HYKNAME,B.BGDDMC,H.BGR,R.PERSON_NAME");
            SetSearchQuery(query, lst,false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_KCKHZ_Srch item = new HYKGL_KCKHZ_Srch();
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.iZSL = query.FieldByName("ZSL").AsInteger;
            item.fZYE = query.FieldByName("ZYE").AsFloat;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
            return item;
        }
    }
}
