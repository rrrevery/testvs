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
    public class CRMGL_SPJGDDY : SPJGDDY
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "SPJGDDY", "JGDID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("SPJGDDY");
            query.SQL.Text = "insert into SPJGDDY(JGDID,JGDMC,LSDJ1,LSDJ2)";
            query.SQL.Add(" values(:JGDID,:JGDMC,:LSDJ1,:LSDJ2)");
            query.ParamByName("JGDID").AsInteger = iJLBH;
            query.ParamByName("JGDMC").AsString = sJGDMC;
            query.ParamByName("LSDJ1").AsFloat = fLSDJ1;
            query.ParamByName("LSDJ2").AsFloat = fLSDJ2;
            //query.ParamByName("SHDM").AsString = sSHDM;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH","B.JGDID");
            query.SQL.Text = "select B.* from SPJGDDY B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SPJGDDY obj = new CRMGL_SPJGDDY();
            obj.iJLBH = query.FieldByName("JGDID").AsInteger;
            obj.sJGDMC = query.FieldByName("JGDMC").AsString;
            obj.fLSDJ1 = query.FieldByName("LSDJ1").AsFloat;
            obj.fLSDJ2 = query.FieldByName("LSDJ2").AsFloat;
            return obj;
        }
    }
}
