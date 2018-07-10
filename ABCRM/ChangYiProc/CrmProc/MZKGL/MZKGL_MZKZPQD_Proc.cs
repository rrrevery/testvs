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
namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKZPQD_Proc : DJLR_ZX_CLass
    {
        public string sDZYH = string.Empty;
        public string sFKRMC = string.Empty;
        public string dDZRQ = string.Empty;
        public double fJE = 0;
        public double fSYJE = 0;
        public string sMDMC = string.Empty;


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_CZKZPXX", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_CZKZPXX");
            query.SQL.Text = "insert into MZK_CZKZPXX(JLBH,DZYH,FKRMC,JE,DZRQ,DJR,DJRMC,DJSJ,STATUS,ZY,SYJE)";
            query.SQL.Add("  values(:JLBH,:DZYH,:FKRMC,:JE,:DZRQ,:DJR,:DJRMC,:DJSJ,:STATUS,:ZY,:SYJE)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DZYH").AsString = sDZYH;
            query.ParamByName("FKRMC").AsString = sFKRMC;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("DZRQ").AsDateTime = FormatUtils.ParseDateString(dDZRQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("SYJE").AsFloat = fSYJE;
            query.ExecSQL();
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_CZKZPXX", serverTime,"JLBH","ZXR","ZXRMC", "ZXRQ",1);

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.JLBH");

            query.SQL.Text = " select L.* from  MZK_CZKZPXX L where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKZPQD_Proc obj = new MZKGL_MZKZPQD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sDZYH = query.FieldByName("DZYH").AsString;
            obj.sFKRMC = query.FieldByName("FKRMC").AsString;
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.fSYJE = query.FieldByName("SYJE").AsFloat;
            obj.dDZRQ = FormatUtils.DateToString(query.FieldByName("DZRQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sZY = query.FieldByName("ZY").AsString;
            return obj;
        }
    }
}
