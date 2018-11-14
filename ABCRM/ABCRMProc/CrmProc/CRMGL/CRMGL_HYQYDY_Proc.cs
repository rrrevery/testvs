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
    public class CRMGL_HYQYDY : HYQY
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYQYDY", "QYID", iJLBH);
        }
        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            CheckChildExist(query, out msg, "HYK_GKDA", "QYID", iJLBH);
            return msg == "";
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HYQYDY");
            query.SQL.Text = "insert into  HYK_HYQYDY(QYID,QYDM,QYMC,YZBM,BJ_MJ) values(:QYID,:QYDM,:QYMC,:YZBM,:BJ_MJ)";
            query.ParamByName("QYID").AsInteger = iJLBH;
            query.ParamByName("QYDM").AsString = sQYDM;
            query.ParamByName("QYMC").AsString = sQYMC;
            query.ParamByName("YZBM").AsString = sYZBM;
            query.ParamByName("BJ_MJ").AsInteger = iBJ_MJ;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            query.SQL.Text = "select B.* from HYK_HYQYDY B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_HYQYDY obj = new CRMGL_HYQYDY();
            obj.iJLBH = query.FieldByName("QYID").AsInteger;
            obj.sQYDM = query.FieldByName("QYDM").AsString;
            obj.sQYMC = query.FieldByName("QYMC").AsString;
            obj.sYZBM = query.FieldByName("YZBM").AsString;
            obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
            return obj;
        }
    }
}
