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


namespace BF.CrmProc.GTPT
{
    public class GTPT_WXSHLMLXDY_Proc : BASECRMClass
    {
        public int iLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLXMC = string.Empty;
        public int iINX = 0;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_LMSHLXDEF;", "LXID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH!=0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("MOBILE_LMSHLXDEF");
            }
            query.SQL.Text = "insert into MOBILE_LMSHLXDEF(LXID,LXMC,INX)";
            query.SQL.Add("values(:LXID,:LXMC,:INX)");
            query.ParamByName("LXID").AsInteger = iJLBH;
            query.ParamByName("LXMC").AsString = sLXMC;
            query.ParamByName("INX").AsInteger = iINX;
            query.ExecSQL();

        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","W.LXID");
            CondDict.Add("sLXMC", "W.LXMC");
            query.SQL.Text = "select W.* from MOBILE_LMSHLXDEF W where 1=1 ";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSHLMLXDY_Proc obj = new GTPT_WXSHLMLXDY_Proc();
            obj.iJLBH = query.FieldByName("LXID").AsInteger;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.sLXMC = query.FieldByName("LXMC").AsString;
            return obj;
        }
    }
}
