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
    public class CRMGL_ZYDY : ZYLX
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYZYDY", "ZYID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HYZYDY");
            query.SQL.Text = "insert into HYK_HYZYDY(ZYID,ZYDM,ZYMC,BJ_MJ,BJ_TY)";
            query.SQL.Add(" values(:ZYID,:ZYDM,:ZYMC,:BJ_MJ,:BJ_TY)");
            query.ParamByName("ZYID").AsInteger = iZYID;
            query.ParamByName("ZYDM").AsString = sZYDM;
            query.ParamByName("ZYMC").AsString = sZYMC;
            query.ParamByName("BJ_MJ").AsInteger = iBJ_MJ;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select B.* from HYK_HYZYDY B";
            query.SQL.Add("    where 1=1");
            if (iJLBH != 0)
                query.SQL.AddLine("  and B.ZYID=" + iJLBH);
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CRMGL_ZYDY obj = new CRMGL_ZYDY();
                lst.Add(obj);
                obj.iZYID = query.FieldByName("ZYID").AsInteger;
                obj.sZYDM = query.FieldByName("ZYDM").AsString;
                obj.sZYMC = query.FieldByName("ZYMC").AsString;
                obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                query.Next();
            }
            query.Close();

            
                
                return lst;
        }
    }
}
