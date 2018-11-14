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
    public class GTPT_TSLXDY : BASECRMClass
    {
        public int iLXID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public string sBZ = string.Empty;
        public string sLXMC = string.Empty;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_TSLXDEF", "LXID", iJLBH);
        }



        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_TSLXDEF");
            query.SQL.Text = "insert into WX_TSLXDEF(LXID,LXMC,BZ)";
            query.SQL.Add(" values(:LXID,:LXMC,:BZ)");
            query.ParamByName("LXID").AsInteger = iJLBH;
            query.ParamByName("LXMC").AsString = sLXMC;
            query.ParamByName("BZ").AsString = sBZ;


            query.ExecSQL();



        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "B.LXID");
          
            CondDict.Add("sLXMC", "B.LXMC");
            query.SQL.Text = "select B.* from WX_TSLXDEF B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_TSLXDY obj = new GTPT_TSLXDY();
            obj.iLXID = query.FieldByName("LXID").AsInteger;
            obj.sLXMC = query.FieldByName("LXMC").AsString;
            obj.sBZ = query.FieldByName("BZ").AsString;
            return obj;
        }



    }
}
