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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_BQLBDY_Proc : LABEL_LB
    {


        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            query.SQL.Text = "select LABELXMID from LABEL_XM ";
            query.SQL.Add(" where LABELLBID=:LABELLBID");
            query.ParamByName("LABELLBID").AsInteger = iLABELLBID;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "该标签类别下存在标签项目，不允许删除";
                return false;
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "LABEL_LB", "LABELLBID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                query.SQL.Text = "update LABEL_LB set BQMC=:BQMC,STATUS=:STATUS";
                query.SQL.Add(" where LABELLBID=:LABELLBID ");
            }
            else
            {
                iJLBH = SeqGenerator.GetSeqNoDBID("LABEL_LB");
                query.SQL.Text = "insert into LABEL_LB(LABELLBID,BQMC,STATUS)";
                query.SQL.Add(" values(:LABELLBID,:BQMC,:STATUS)");         
            }
            query.ParamByName("LABELLBID").AsInteger = iLABELLBID;
            query.ParamByName("BQMC").AsString = sLABELMC;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ExecSQL();
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "LABELLBID");
            query.SQL.Text = "select  * from LABEL_LB where 1=1";
            SetSearchQuery(query, lst); 
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_BQLBDY_Proc obj = new HYKGL_BQLBDY_Proc();
            obj.iLABELLBID = query.FieldByName("LABELLBID").AsInteger;
            obj.sLABELMC = query.FieldByName("BQMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return obj;
        }
    }
}
