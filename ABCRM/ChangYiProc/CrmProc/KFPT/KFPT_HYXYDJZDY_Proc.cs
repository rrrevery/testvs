using BF.Pub;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace BF.CrmProc.KFPT
{
    public class KFPT_HYXYDJZDY_Proc : BASECRMClass
    {
        public int iXYDJID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sXYDJMC = string.Empty;
        public int iXYDJYS = 0;
        public int iFXZQ = 0;
        public int iTHCS = 0;
        public int iXFCS_TGZ = 0;
        public int iPPS_TLC = 0;
        public string sBZ = string.Empty;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYXYDJDEF", "XYDJID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);

            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HYXYDJDEF");
            query.SQL.Text = "insert into HYK_HYXYDJDEF(XYDJID,XYDJMC,XYDJYS,FXZQ,THCS,XFCS_TGZ,PPS_TLC,BZ)";
            query.SQL.Add(" values(:XYDJID,:XYDJMC,:XYDJYS,:FXZQ,:THCS,:XFCS_TGZ,:PPS_TLC,:BZ)");
            query.ParamByName("XYDJID").AsInteger = iJLBH;
            query.ParamByName("XYDJMC").AsString = sXYDJMC;
            query.ParamByName("XYDJYS").AsInteger = iXYDJYS;
            query.ParamByName("FXZQ").AsInteger = iFXZQ;
            query.ParamByName("THCS").AsInteger = iTHCS;
            query.ParamByName("XFCS_TGZ").AsInteger = iXFCS_TGZ;
            query.ParamByName("PPS_TLC").AsInteger = iPPS_TLC;
            query.ParamByName("BZ").AsString = sBZ;

            query.ExecSQL();
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.XYDJID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.* from HYK_HYXYDJDEF W where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HYXYDJZDY_Proc obj = new KFPT_HYXYDJZDY_Proc();
            obj.iJLBH = query.FieldByName("XYDJID").AsInteger;
            obj.sXYDJMC = query.FieldByName("XYDJMC").AsString;
            obj.iXYDJYS = query.FieldByName("XYDJYS").AsInteger;
            obj.iFXZQ = query.FieldByName("FXZQ").AsInteger;
            obj.iTHCS = query.FieldByName("THCS").AsInteger;
            obj.iXFCS_TGZ = query.FieldByName("XFCS_TGZ").AsInteger;
            obj.iPPS_TLC = query.FieldByName("PPS_TLC").AsInteger;
            obj.sBZ = query.FieldByName("BZ").AsString;
            return obj;
        }
    }
}
