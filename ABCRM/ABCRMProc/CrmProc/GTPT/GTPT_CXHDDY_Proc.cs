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
    public class GTPT_CXHDDY_Proc : BASECRMClass
    {
        public int iCXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sCXZT = string.Empty;
        public string sCXNR = string.Empty;
        public string dSTART_RQ = string.Empty;
        public string dEND_RQ = string.Empty;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_CXHDDEF;", "CXID", iJLBH);
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.CXID");
            query.SQL.Text = "select W.* from WX_CXHDDEF W where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_CXHDDEF");
            query.SQL.Text = "insert into WX_CXHDDEF(CXID,CXZT,START_RQ,END_RQ,CXNR)";
            query.SQL.Add(" values(:CXID,:CXZT,:START_RQ,:END_RQ,:CXNR)");
            query.ParamByName("CXID").AsInteger = iJLBH;
            query.ParamByName("CXZT").AsString = sCXZT;
            query.ParamByName("START_RQ").AsDateTime = FormatUtils.ParseDateString(dSTART_RQ);
            query.ParamByName("END_RQ").AsDateTime = FormatUtils.ParseDateString(dEND_RQ);
            query.ParamByName("CXNR").AsString = sCXNR;
            query.ExecSQL();
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_CXHDDY_Proc obj = new GTPT_CXHDDY_Proc();
            obj.iJLBH = query.FieldByName("CXID").AsInteger;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.sCXNR = query.FieldByName("CXNR").AsString;
            obj.dSTART_RQ = FormatUtils.DateToString(query.FieldByName("START_RQ").AsDateTime);
            // obj.dSTART_RQ = FormatUtils.DatetimeToString(query.FieldByName("START_RQ").AsDateTime);
            obj.dEND_RQ = FormatUtils.DateToString(query.FieldByName("END_RQ").AsDateTime);
            return obj;
        }
    }
}
