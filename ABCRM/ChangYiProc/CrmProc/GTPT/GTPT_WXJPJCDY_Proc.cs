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
    public class GTPT_WXJPJCDY_Proc : BASECRMClass
    {
        public int iJC
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sMC = string.Empty;


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_JCDEF", "JC", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_JCDEF");
            query.SQL.Text = "insert into MOBILE_JCDEF(JC,MC)";
            query.SQL.Add(" values(:JC,:MC)");
            query.ParamByName("JC").AsInteger = iJLBH;
            query.ParamByName("MC").AsString = sMC;
            query.ExecSQL();
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "B.JC");

            CondDict.Add("sMC", "B.MC");
            query.SQL.Text = "select B.* from MOBILE_JCDEF B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXJPJCDY_Proc obj = new GTPT_WXJPJCDY_Proc();
            obj.iJLBH = query.FieldByName("JC").AsInteger;
            obj.sMC = query.FieldByName("MC").AsString;
            return obj;
        }

    }
}
