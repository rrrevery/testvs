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


namespace BF.CrmProc.CRMGL
{
    public class CRMGL_RYXXDY : BASECRMClass
    {
        public string sRYDM = string.Empty;
        public string sRYMC = string.Empty;
        public string sPYM = string.Empty;
        public int iRYID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("sRYDM", "R.RYDM");
            CondDict.Add("sRYMC", "R.PERSON_NAME");
            CondDict.Add("iRYID", "R.PERSON_ID");
            query.SQL.Text = " select * from RYXX R where PERSON_ID>0";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            CRMGL_RYXXDY obj = new CRMGL_RYXXDY();
            obj.iJLBH = query.FieldByName("PERSON_ID").AsInteger;
            obj.sRYDM = query.FieldByName("RYDM").AsString;
            obj.sRYMC = query.FieldByName("PERSON_NAME").AsString;
            obj.sPYM = query.FieldByName("PYM").AsString;
            return obj;
        }
    }
}
