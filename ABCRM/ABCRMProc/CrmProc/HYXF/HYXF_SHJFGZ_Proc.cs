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

namespace BF.CrmProc.HYXF
{
    public class HYXF_SHJFGZ_Proc :BASECRMClass
    {
        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMC = string.Empty;
        public double fJE = 0, fJF = 0;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "SHJFGZ", "ID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("SHJFGZ");
            query.SQL.Text = "insert into SHJFGZ(ID,MC,JE,JF)";
            query.SQL.Add(" values(:ID,:MC,:JE,:JF)");
            query.ParamByName("ID").AsInteger = iJLBH;
            query.ParamByName("MC").AsString = sMC;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("JF").AsFloat = fJF;
            query.ExecSQL();
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "B.ID");

            query.SQL.Text = "select B.* from SHJFGZ B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_SHJFGZ_Proc obj = new HYXF_SHJFGZ_Proc();
            obj.iJLBH = query.FieldByName("ID").AsInteger;
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.sMC = query.FieldByName("MC").AsString;
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.fJF = query.FieldByName("JF").AsFloat;
            return obj;
        }
    }
}
