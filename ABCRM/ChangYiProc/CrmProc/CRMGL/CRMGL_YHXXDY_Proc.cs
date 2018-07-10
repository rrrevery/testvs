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
    public class CRMGL_YHXXDY : YHXX
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YHXX", "YHID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);

            }
            else
                iJLBH = SeqGenerator.GetSeq("YHXX");
            query.SQL.Text = "insert into YHXX(YHID,YHMC)";
            query.SQL.Add(" values(:YHID,:YHMC)");
            query.ParamByName("YHID").AsInteger = iJLBH;
            query.ParamByName("YHMC").AsString = sYHMC;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "W.YHID");
            query.SQL.Text = "select W.* from YHXX W where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_YHXXDY obj = new CRMGL_YHXXDY();
            obj.iJLBH = query.FieldByName("YHID").AsInteger;
            obj.sYHMC = query.FieldByName("YHMC").AsString;
            return obj;
        }
    }
}
