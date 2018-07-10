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
    public class CRMGL_SPSBCX : SHSPSB
    {
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("Id", "SHSBID");
            CondDict.Add("sSHDM", "SHDM");
            CondDict.Add("sSBMC", "SBMC");
            List<object> lst = new List<object>();
            query.SQL.Text = "select * from SHSPSB where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SPSBCX  obj = new CRMGL_SPSBCX ();
            obj.iSHSBID = query.FieldByName("SHSBID").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSBDM = query.FieldByName("SBDM").AsString;
            obj.sSBMC = query.FieldByName("SBMC").AsString;
            obj.sPYM = query.FieldByName("PYM").AsString;
            obj.sSYZ = query.FieldByName("SYZ").AsString;
            obj.iMJBJ = query.FieldByName("MJBJ").AsInteger;
            obj.iSBID = query.FieldByName("SBID").AsInteger;
            return obj;
        }
    }
}
