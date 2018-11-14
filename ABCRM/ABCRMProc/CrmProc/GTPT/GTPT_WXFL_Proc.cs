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
    public class GTPT_WXFL_Proc : BASECRMClass
    {
        public int iFLID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sFLMC = string.Empty;
        public int iINX = 0;
        public string sIMG = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_FL;", "FLID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("WX_FL");
            }
            query.SQL.Text = "insert into WX_FL(FLID,FLMC,INX,IMG)";
            query.SQL.Add(" values(:FLID,:FLMC,:INX,:IMG)");
            query.ParamByName("FLID").AsInteger = iJLBH;
            query.ParamByName("FLMC").AsString = sFLMC;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("IMG").AsString = sIMG;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.FLID");
            CondDict.Add("sFLMC", "W.FLMC");
            CondDict.Add("iINX", "W.INX");
            CondDict.Add("sIMG", "W.IMG");
            query.SQL.Text = "select W.* from WX_FL W WHERE 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXFL_Proc obj = new GTPT_WXFL_Proc();
            obj.iJLBH = query.FieldByName("FLID").AsInteger;
            obj.sFLMC = query.FieldByName("FLMC").AsString;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.sIMG = query.FieldByName("IMG").AsString;
            return obj;
        }
    }
}
