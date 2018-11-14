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
    public class CRMGL_SQDY_Proc : SQDY
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            //CrmLibProc.DeleteDataBase(query, out msg, "delete from SQDY where SQID=" + iJLBH + "");
            CrmLibProc.DeleteDataTables(query, out msg, "SQDY", "SQID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("SQDY");
            query.SQL.Text = "insert into SQDY(SQID,SQMC,SQMS,MDID)";
            query.SQL.Add(" values(:SQID,:SQMC,:SQMS,:MDID)");
            query.ParamByName("SQID").AsInteger = iJLBH;
            query.ParamByName("SQMC").AsString = sSQMC;
            query.ParamByName("SQMS").AsString = sSQMS;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "S.SQID");
            CondDict.Add("sSQMC", "S.SQMC");
            CondDict.Add("sSQMS", "S.SQMS");
            CondDict.Add("iSQID", "S.SQID");
            CondDict.Add("iMDID", "S.MDID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "  select S.*,M.MDMC from  SQDY S,MDDY M where S.MDID = M.MDID(+)";
            SetSearchQuery(query,lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SQDY_Proc obj = new CRMGL_SQDY_Proc();
            obj.iSQID = query.FieldByName("SQID").AsInteger;
            obj.iJLBH = query.FieldByName("SQID").AsInteger;
            obj.sSQMC = query.FieldByName("SQMC").AsString;
            obj.sSQMS = query.FieldByName("SQMS").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
    }
}
