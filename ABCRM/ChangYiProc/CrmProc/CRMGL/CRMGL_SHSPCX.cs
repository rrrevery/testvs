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
    public class CRMGL_SHSPCX : SHSPXX
    {
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("sSHDM", "A.SHDM");
            CondDict.Add("sSPDM", "A.SPDM");
            CondDict.Add("sSPMC", "A.SPMC");
            CondDict.Add("sSPFLDM", "F.SPFLDM");
            List<object> lst = new List<object>();
            query.SQL.Text = "select A.SHSPID,A.SHDM,A.SPDM,A.SPMC,F.SPFLDM from SHSPXX A,SHDY S,SHSPFL F where A.SHDM=S.SHDM and A.SHSPFLID=F.SHSPFLID";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SHSPCX obj = new CRMGL_SHSPCX();
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSPDM = query.FieldByName("SPDM").AsString;
            obj.sSPMC = query.FieldByName("SPMC").AsString;
            obj.iSHSPID = query.FieldByName("SHSPID").AsInteger;
            return obj;
        }
    }
}
