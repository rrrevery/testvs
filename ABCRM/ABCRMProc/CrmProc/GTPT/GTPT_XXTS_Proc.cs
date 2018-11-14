using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_XXTS_Proc : DJLR_ZXQDZZ_CLass
    {
        public string sTSZT = string.Empty;

        public List<MXITEM> itemTable = new List<MXITEM>();

        public class MXITEM
        {
            public int iJLBH = 0;
            public int iHYID = 0;
            public string sSJHM = string.Empty;
            public string sFSNR = string.Empty;
        }
        public string[] asFieldNames = { 
                                            "iJLBH;L.JLBH",
                                            "sTSZT;L.TSZT",                                        
                                            "iSTATUS;L.STATUS",
                                            "iDJR;L.DJR",
                                            "sDJRMC;L.DJRMC" ,
                                            "dDJSJ;L.DJSJ",
                                            "iZXR;L.ZXR",
                                            "sZXRMC;L.ZXRMC" ,
                                            "dZXRQ;L.ZXRQ",                                                                               
                                       };
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_WX_XXTSJL;HYK_WX_XXTSJL_MX", "JLBH", iJLBH);
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_WX_XXTSJL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "HYK_WX_XXTSJL");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_WX_XXTSJL");

            query.SQL.Text = "insert into HYK_WX_XXTSJL(JLBH,TSZT,STATUS,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:TSZT,:STATUS,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("TSZT").AsString = sTSZT;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();

            foreach (MXITEM one in itemTable)
            {
                query.SQL.Text = "insert into HYK_WX_XXTSJL_MX(JLBH,HYID,SJHM,FSNR)";
                query.SQL.Add(" values(:JLBH,:HYID,:SJHM,:FSNR)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("SJHM").AsString = one.sSJHM;
                query.ParamByName("FSNR").AsString = one.sFSNR;
                query.ExecSQL();
            }
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select L.*";
            query.SQL.Add(" from HYK_WX_XXTSJL L ");
            query.SQL.Add(" where 1=1");
            if (iJLBH != 0)
            {
                query.SQL.Add(" AND L.JLBH=" + iJLBH);
            }
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                GTPT_XXTS_Proc obj = new GTPT_XXTS_Proc();
                lst.Add(obj);
                obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.sTSZT = query.FieldByName("TSZT").AsString;
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                //obj.iDJR = query.FieldByName("DJR").AsInteger;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                //obj.iZXR = query.FieldByName("ZXR").AsInteger;
                obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                query.Next();
            }
            query.Close();

            query.SQL.Text = "select A.*,B.HYK_NO,B.HY_NAME from HYK_WX_XXTSJL_MX A,HYK_HYXX B";
            query.SQL.Add("  where A.HYID=B.HYID and A.JLBH is not null");
            if (iJLBH != 0)
                query.SQL.Add("  and A.JLBH=" + iJLBH);
            query.Open();
            while (!query.Eof)
            {
                MXITEM item = new MXITEM();
                ((GTPT_XXTS_Proc)lst[0]).itemTable.Add(item);
                item.iJLBH = query.FieldByName("JLBH").AsInteger;
                item.iHYID = query.FieldByName("HYID").AsInteger;
                item.sSJHM = query.FieldByName("SJHM").AsString;
                item.sFSNR = query.FieldByName("FSNR").AsString;
                query.Next();
            }
            query.Close();



            return lst;
        }
    }
}
