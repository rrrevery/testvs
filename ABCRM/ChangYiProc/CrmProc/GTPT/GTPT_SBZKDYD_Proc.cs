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
    public class GTPT_SBZKDYD_Proc : DJLR_ZXQDZZ_CLass
    {
        public string dKSSJ = string.Empty;
        public string dJSSJ = string.Empty;
        public int iMDID = 0;
        public int iINX = 0;
        public string sCONTENT = string.Empty;
        public string sMDMC = string.Empty;

        public List<SBITEM> itemTable = new List<SBITEM>();
        public List<SBKLXITEM> itemTable1 = new List<SBKLXITEM>();

        public class SBITEM
        {
            public int iJLBH = 0;
            public int iSBID = 0;
            public string sSBMC = string.Empty;
        }
        public class SBKLXITEM
        {
            public int iJLBH = 0;
            public int iSBID = 0;
            public string sSBMC = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public double fZKL = 0;
        }
        public string[] asFieldNames = { 
                                            "iJLBH;D.JLBH",
                                            "iMDID;D.MDID",                                        
                                            "iINX;D.INX",
                                            "dKSSJ;D.KSSJ", 
                                            "dJSSJ;D.JSSJ",
                                            "iSTATUS;D.STATUS",
                                            "sCONTENT;D.CONTENT",
                                            "iDJR;D.DJR",
                                            "sDJRMC;D.DJRMC" ,
                                            "dDJSJ;D.DJSJ",
                                            "iZXR;D.ZXR",
                                            "sZXRMC;D.ZXRMC" ,
                                            "dZXRQ;D.ZXRQ",
                                            "iQDR;D.QDR",
                                            "sQDRMC;D.QDRMC" ,
                                            "dQDSJ;D.QDSJ",
                                            "iZZR;D.ZZR",
                                            "sZZRMC;D.ZZRMC" ,
                                            "dZZRQ;D.ZZRQ",                                                                                       
                                       };
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CrmLibProc.DeleteDataTables(query, out msg, "SBZKDYD;SBZKDYD_SBITEM;SBZKDYD_ITEM", "JLBH", iJLBH);
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "SBZKDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }        
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "SBZKDYD");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("SBZKDYD");

            query.SQL.Text = "insert into SBZKDYD(JLBH,MDID,INX,KSSJ,JSSJ,STATUS,CONTENT,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:MDID,:INX,:KSSJ,:JSSJ,:STATUS,:CONTENT,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSSJ").AsDateTime = FormatUtils.ParseDateString(dKSSJ);
            query.ParamByName("JSSJ").AsDateTime = FormatUtils.ParseDateString(dJSSJ);
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();

            foreach (SBITEM one in itemTable)
            {
                query.SQL.Text = "insert into SBZKDYD_SBITEM(JLBH,SBID)";
                query.SQL.Add(" values(:JLBH,:SBID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("SBID").AsInteger = one.iSBID;
                query.ExecSQL();
            }

            foreach (SBKLXITEM ones in itemTable1)
            {
                query.SQL.Text = "insert into SBZKDYD_ITEM(JLBH,SBID,HYKTYPE,ZKL)";
                query.SQL.Add(" values(:JLBH,:SBID,:HYKTYPE,:ZKL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("SBID").AsInteger = ones.iSBID;
                query.ParamByName("HYKTYPE").AsInteger = ones.iHYKTYPE;
                query.ParamByName("ZKL").AsFloat = ones.fZKL;
                query.ExecSQL();
            }
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select D.*,Y.MDMC";
            query.SQL.Add(" from SBZKDYD D,MDDY Y ");
            query.SQL.Add(" where D.MDID=Y.MDID");
            if (iJLBH != 0)
            {
                query.SQL.Add(" AND D.JLBH=" + iJLBH);
            }
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                GTPT_SBZKDYD_Proc obj = new GTPT_SBZKDYD_Proc();
                lst.Add(obj);
                obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.iINX = query.FieldByName("INX").AsInteger;
                obj.dKSSJ = FormatUtils.DatetimeToString(query.FieldByName("KSSJ").AsDateTime);
                obj.dJSSJ = FormatUtils.DatetimeToString(query.FieldByName("JSSJ").AsDateTime);
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                obj.sCONTENT = query.FieldByName("CONTENT").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                //obj.iDJR = query.FieldByName("DJR").AsInteger;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                //obj.iZXR = query.FieldByName("ZXR").AsInteger;
                obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                obj.sQDRMC = query.FieldByName("QDRMC").AsString;
                obj.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
                obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
                obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
                query.Next();
            }
            query.Close();

            query.SQL.Text = "select A.*,B.SBMC from SBZKDYD_SBITEM A,WX_SB B";
            query.SQL.Add("  where A.SBID=B.SBID and A.JLBH is not null");
            if (iJLBH != 0)
                query.SQL.Add("  and A.JLBH=" + iJLBH);
            query.Open();
            while (!query.Eof)
            {
                SBITEM item = new SBITEM();
                ((GTPT_SBZKDYD_Proc)lst[0]).itemTable.Add(item);
                item.iJLBH = query.FieldByName("JLBH").AsInteger;
                item.iSBID = query.FieldByName("SBID").AsInteger;
                item.sSBMC = query.FieldByName("SBMC").AsString;
                query.Next();
            }
            query.Close();

            query.SQL.Text = "select A.*,B.SBMC,F.HYKNAME from SBZKDYD_ITEM A,WX_SB B,HYKDEF F";
            query.SQL.Add("  where A.SBID=B.SBID and A.HYKTYPE=F.HYKTYPE and A.JLBH is not null");
            if (iJLBH != 0)
                query.SQL.Add("  and A.JLBH=" + iJLBH);
            query.Open();
            while (!query.Eof)
            {
                SBKLXITEM item2 = new SBKLXITEM();
                ((GTPT_SBZKDYD_Proc)lst[0]).itemTable1.Add(item2);
                item2.iJLBH = query.FieldByName("JLBH").AsInteger;
                item2.iSBID = query.FieldByName("SBID").AsInteger;
                item2.sSBMC = query.FieldByName("SBMC").AsString;
                item2.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                item2.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item2.fZKL = query.FieldByName("ZKL").AsFloat;
                query.Next();
            }
            query.Close();

            
                
                return lst;
        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "SBZKDYD", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "SBZKDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }
    }
}
