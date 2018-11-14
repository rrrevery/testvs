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
    public class CRMGL_FXQDDY : BASECRMClass
    {
        public string sFXQDMC = string.Empty;
        public string sFXQDDM = string.Empty;
        public string sPFXQDDM = string.Empty;
        public string sFXQDQC = string.Empty;
        public int iBJ_MJ = 0;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            string sql = "delete from FXQDDY where FXQDDM like '" + sFXQDDM + "%' ";
            CrmLibProc.DeleteDataBase(query, out msg, sql);
        }


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                CrmLibProc.DeleteDataTables(query, out msg, "FXQDDY", "FXQDID", iJLBH);
            }
            else
                iJLBH = SeqGenerator.GetSeq("FXQDDY");
            query.SQL.Text = "insert into FXQDDY(FXQDID,FXQDDM,FXQDMC,BJ_MJ)";
            query.SQL.Add(" values(:FXQDID,:FXQDDM,:FXQDMC,:BJ_MJ)");
            query.ParamByName("FXQDID").AsInteger = iJLBH;
            query.ParamByName("FXQDDM").AsString = sFXQDDM;
            query.ParamByName("FXQDMC").AsString = sFXQDMC;
            query.ParamByName("BJ_MJ").AsInteger = iBJ_MJ;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "B.FXQDID");
            CondDict.Add("sFXQDMC", "B.FXQDMC");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select B.* from FXQDDY B";
            query.SQL.Add("    where 1=1");
            if (iJLBH != 0)
                query.SQL.AddLine("  and B.FXQDID='" + iJLBH + "'");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CRMGL_FXQDDY obj = new CRMGL_FXQDDY();
                lst.Add(obj);
                obj.iJLBH = query.FieldByName("FXQDID").AsInteger;
                obj.sFXQDMC = query.FieldByName("FXQDMC").AsString;
                query.Next();
            }
            query.Close();
            return lst;
        }
    }
}
