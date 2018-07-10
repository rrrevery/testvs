using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BF.Pub;
using Newtonsoft.Json;
using System.Data.Common;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKLYSQ_Proc : DJLR_ZX_CLass
    {
        public string sBGDDDM_BR = string.Empty;
        public string sBGDDMC_BR = string.Empty;
        public int iHYKSL = 0;
        public int iBJ_CZK = 0;
        public List<HYKGL_LYSQItem> itemTable = new List<HYKGL_LYSQItem>();
        public class HYKGL_LYSQItem
        {
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public int iHYKSL = 0;
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            ExecTable(query, "MZK_CARDLQSQJL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
            msg = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_CARDLQSQJL;MZK_CARDLQSQJLITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_CARDLQSQJL");
            query.SQL.Text = "insert into MZK_CARDLQSQJL(JLBH,BGDDDM_BR,HYKSL,BJ_CZK,STATUS,DJLX,BZ,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:BGDDDM_BR,:HYKSL,:BJ_CZK,:STATUS,:DJLX,:BZ,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM_BR").AsString = sBGDDDM_BR;
            query.ParamByName("HYKSL").AsInteger = iHYKSL;
            query.ParamByName("BJ_CZK").AsInteger = iBJ_CZK;  //储值卡
            query.ParamByName("STATUS").AsInteger = 0;  //未审核
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("BZ").AsString = sBZ;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();

            foreach (HYKGL_LYSQItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_CARDLQSQJLITEM(JLBH,HYKTYPE,HYKSL)";
                query.SQL.Add(" values(:JLBH,:HYKTYPE,:HYKSL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("HYKSL").AsInteger = one.iHYKSL;
                query.ExecSQL();
            }


        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);

            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "W.BGDDDM_BR");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("sZY", "ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iLQR", "W.LQR");
            query.SQL.Text = "select W.*,B.BGDDMC from MZK_CARDLQSQJL W,HYK_BGDD B ";
            query.SQL.Add(" where W.BGDDDM_BR=B.BGDDDM(+)");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,D.HYKNAME from MZK_CARDLQSQJLITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add("and I.HYKTYPE=D.HYKTYPE");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_LYSQItem item = new HYKGL_LYSQItem();
                    ((MZKGL_MZKLYSQ_Proc)lst[0]).itemTable.Add(item);
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.iHYKSL = query.FieldByName("HYKSL").AsInteger;
                    query.Next();
                }

            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKLYSQ_Proc obj = new MZKGL_MZKLYSQ_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sBGDDDM_BR = query.FieldByName("BGDDDM_BR").AsString;
            obj.sBGDDMC_BR = query.FieldByName("BGDDMC").AsString;
            obj.iHYKSL = query.FieldByName("HYKSL").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            return obj;
        }

        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            ExecTable(query, "MZK_CARDLQSQJL", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
            msg = string.Empty;
        }
    }
}
