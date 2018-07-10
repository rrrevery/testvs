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
    public class HYXF_HYZLXDY_Proc : HYZLX
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYZLXDY", "HYZLXID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYZLXDY");
            query.SQL.Text = "insert into HYZLXDY(HYZLXID,HYZLXDM,HYZLXMC,BJ_MJ)";
            query.SQL.Add(" values(:HYZLXID,:HYZLXDM,:HYZLXMC,:BJ_MJ)");
            query.ParamByName("HYZLXID").AsInteger = iJLBH;
            query.ParamByName("HYZLXDM").AsString = sHYZLXDM;
            query.ParamByName("HYZLXMC").AsString = sHYZLXMC;
            query.ParamByName("BJ_MJ").AsInteger = iBJ_MJ;
            query.ExecSQL();
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            query.SQL.Text = "select B.* from HYZLXDY B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);


            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_HYZLXDY_Proc obj = new HYXF_HYZLXDY_Proc();
            obj.iJLBH = query.FieldByName("HYZLXID").AsInteger;
            obj.sHYZLXDM = query.FieldByName("HYZLXDM").AsString;
            obj.sHYZLXMC = query.FieldByName("HYZLXMC").AsString;
            obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
            return obj;
        }
    }
}
