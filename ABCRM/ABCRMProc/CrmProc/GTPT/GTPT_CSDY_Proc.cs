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

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_CSDY : BASECRMClass
    {
        public int ID 
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sCSMC = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CITY", "ID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("CITY");
            query.SQL.Text = "insert into CITY(ID,NAME)";
            query.SQL.Add(" values(:ID,:NAME)");
            query.ParamByName("ID").AsInteger = iJLBH;
            query.ParamByName("NAME").AsString = sCSMC;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","C.ID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "  select * from  CITY C where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_CSDY obj = new GTPT_CSDY();
            obj.iJLBH = query.FieldByName("ID").AsInteger;
            obj.sCSMC = query.FieldByName("NAME").AsString;
            return obj;
        }
    }
}
