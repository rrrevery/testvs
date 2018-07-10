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
    public class CRMGL_SHHT : BASECRMClass
    {
        public string sSHDM = string.Empty, sHTH = string.Empty, sGHSMC = string.Empty, sGHSDM = string.Empty;
        public int iSHHTID = 0;
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Clear();
            CondDict.Add("sGHSMC", "A.GSHMC");
            CondDict.Add("sGHSDM", "A.GHSDM");
            List<object> lst = new List<object>();
            if (iSEARCHMODE == 0)
            {
                CondDict.Add("sSHDM", "A.SHDM");
                CondDict.Add("sHTH", "A.HTH");
                query.SQL.Text = "select  SHHTID ,SHDM,HTH,GHSDM,GSHMC,SHBMID from SHHT A";
                query.SQL.Add("    where 1=1");
                SetSearchQuery(query, lst);
            }
            if (iSEARCHMODE == 1)
            {
                query.SQL.Text = "select A.GHSDM,A.GSHMC from SHHT A where A.GHSDM is not null";
                SetSearchQuery(query, lst, true, "group by  A.GHSDM,A.GSHMC order by A.GHSDM asc", false);

            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SHHT obj = new CRMGL_SHHT();
            if (iSEARCHMODE == 0)
            {
                obj.sSHDM = query.FieldByName("SHDM").AsString;
                obj.sHTH = query.FieldByName("HTH").AsString;
                obj.iSHHTID = query.FieldByName("SHHTID").AsInteger;
            }
            obj.sGHSMC = query.FieldByName("GSHMC").AsString;
            obj.sGHSDM = query.FieldByName("GHSDM").AsString;
            return obj;
        }
    }
}
