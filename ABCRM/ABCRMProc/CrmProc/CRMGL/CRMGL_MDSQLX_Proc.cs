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
    public class CRMGL_MDSQLX_Proc : BASECRMClass
    {
        public int iLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sLXMC = string.Empty;
        public string sBZ = string.Empty;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CS_MDSQLX;", "LXID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("CS_MDSQLX");
            query.SQL.Text = "insert into CS_MDSQLX(LXID,LXMC,BZ)";
            query.SQL.Add(" values(:LXID,:LXMC,:BZ)");
            query.ParamByName("LXID").AsInteger = iJLBH;
            query.ParamByName("LXMC").AsString = sLXMC;
            query.ParamByName("BZ").AsString = sBZ;
            query.ExecSQL();

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.LXID");
            CondDict.Add("iLXID", "W.LXID");
            CondDict.Add("sLXMC", "W.LXMC");
            CondDict.Add("sBZ", "W.BZ");
            query.SQL.Text = "select W.* from CS_MDSQLX W";
            query.SQL.Add("    where W.LXID>0");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CRMGL_MDSQLX_Proc obj = new CRMGL_MDSQLX_Proc();
                lst.Add(obj);
                obj.iJLBH = query.FieldByName("LXID").AsInteger;
                obj.sLXMC = query.FieldByName("LXMC").AsString;
                obj.sBZ = query.FieldByName("BZ").AsString;
                query.Next();
            }
            query.Close();
            return lst;
        }
    }
}
