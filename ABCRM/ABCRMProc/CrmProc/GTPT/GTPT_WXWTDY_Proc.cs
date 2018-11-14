using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;


namespace BF.CrmProc.GTPT
{
    public class GTPT_WXWTDY_Proc : BASECRMClass
    {
        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sASK = string.Empty;
        public int iBJ_DY, iBJ_NONE, iSTATUS, iPUBLICID, iTYPE = 0;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_ASK;", "ASKID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_ASK");
            query.SQL.Text = "insert into WX_ASK(ASKID,PUBLICID,ASK,TYPE,STATUS)";
            query.SQL.Add(" values(:ID,:PUBLICID,:ASK,:TYPE,:STATUS)");
            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("ASK").AsString = sASK;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "ASKID");
            CondDict.Add("iTYPE", "TYPE");
            CondDict.Add("iSTATUS", "STATUS");
            query.SQL.Text = "select * from WX_ASK where  PUBLICID=" + iLoginPUBLICID;
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXWTDY_Proc obj = new GTPT_WXWTDY_Proc();
            obj.iID = query.FieldByName("ASKID").AsInteger;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            obj.sASK = query.FieldByName("ASK").AsString;
            obj.iTYPE = query.FieldByName("TYPE").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return obj;
        }
    }
}
