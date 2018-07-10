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
    public class CRMGL_NLDDY : NLDDY
    {

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYNLDDY", "NLDID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("HYNLDDY");
            query.SQL.Text = "insert into HYNLDDY(NLDID,NLDMC,NL1,NL2)";
            query.SQL.Add(" values(:NLDID,:NLDMC,:NL1,:NL2)");
            query.ParamByName("NLDID").AsInteger = iJLBH;
            query.ParamByName("NLDMC").AsString = sNLDMC;
            query.ParamByName("NL1").AsInteger = iNL1;
            query.ParamByName("NL2").AsInteger = iNL2;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iNL1;iNL2;";
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH","B.NLDID");
            CondDict.Add("iNL1", "B.NL1");
            CondDict.Add("iNL2", "B.NL2");
            CondDict.Add("sNLDMC", "B.NLDMC");                          
            query.SQL.Text = "select B.* from HYNLDDY B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_NLDDY obj = new CRMGL_NLDDY();
            obj.iJLBH = query.FieldByName("NLDID").AsInteger;
            obj.sNLDMC = query.FieldByName("NLDMC").AsString;
            obj.iNL1 = query.FieldByName("NL1").AsInteger;
            obj.iNL2 = query.FieldByName("NL2").AsInteger;
            return obj;
        }
    }
}
