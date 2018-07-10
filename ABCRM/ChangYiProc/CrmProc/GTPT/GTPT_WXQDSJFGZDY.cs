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
    public class GTPT_WXQDSJFGZDY : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iCOUNTS = 0;
        public string sCONTENT = string.Empty;


        public int iHYKTYPE = 0;
        public int iZXR = 0;
        public string sZXRMC = string.Empty;
        public string dZXRQ = string.Empty;
        public int iPUBLICID= 0;

        public List<MOBILE_SIGN_GZMX> itemTable = new List<MOBILE_SIGN_GZMX>();
        public List<MOBILE_SIGN_GZITEM> itemTable1 = new List<MOBILE_SIGN_GZITEM>();

        public class MOBILE_SIGN_GZMX
        {
            public int iZSJF = 0;

            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;



        }
        public class MOBILE_SIGN_GZITEM
        {
            public int iHYKTYPES = 0;
            public string sHYKNAMES = string.Empty;
            public int iCOUNTS = 0;
            public int iZSJFS = 0;
        }


        public override bool IsValidData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = string.Empty;
            foreach (MOBILE_SIGN_GZMX one in itemTable)
            {
                if (one.iHYKTYPE <= 0)
                {
                    msg = CrmLibStrings.msgHYKTYPENotFound;
                    return false;
                }
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_SIGN_GZ;MOBILE_SIGN_GZMX;MOBILE_SIGN_GZITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_SIGN_GZ");

            query.SQL.Text = "insert into MOBILE_SIGN_GZ(JLBH,KSRQ,JSRQ,DJR,DJRMC,DJSJ,STATUS,COUNTS,CONTENT,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:DJR,:DJRMC,:DJSJ,:STATUS,:COUNTS,:CONTENT,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("COUNTS").AsInteger = iCOUNTS;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("CONTENT").AsString = sCONTENT;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            query.ExecSQL();

            foreach (MOBILE_SIGN_GZMX one in itemTable)
            {
                query.SQL.Text = "insert into MOBILE_SIGN_GZMX(JLBH,ZSJF,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:ZSJF,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZSJF").AsInteger = one.iZSJF;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ExecSQL();
            }

            foreach (MOBILE_SIGN_GZITEM ones in itemTable1)
            {

                query.SQL.Text = "insert into MOBILE_SIGN_GZITEM(JLBH,ZSJF,HYKTYPE,COUNTS)";
                query.SQL.Add(" values(:JLBH,:ZSJF,:HYKTYPE,:COUNTS)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZSJF").AsInteger = ones.iZSJFS;
                query.ParamByName("HYKTYPE").AsInteger = ones.iHYKTYPES;
                query.ParamByName("COUNTS").AsInteger = ones.iCOUNTS;
                query.ExecSQL();

            }

        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_SIGN_GZ", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iQDR", "W.QDR");
            CondDict.Add("sQDRMC", "W.QDRMC");
            CondDict.Add("dQDRQ", "W.QDRQ");
            CondDict.Add("iZZR", "W.ZZR");
            CondDict.Add("sZZRMC", "W.ZZRMC");
            CondDict.Add("dZZRQ", "W.ZZRQ");
            CondDict.Add("iCOUNTS","COUNTS");
            CondDict.Add("sCONTENT","CONTENT");
            List<Object> lst = new List<Object>();
            query.SQL.Text = " select  *  from  MOBILE_SIGN_GZ  W  where 1=1";
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select M.JLBH, M.HYKTYPE,M.ZSJF,D.HYKNAME from  MOBILE_SIGN_GZMX M,HYKDEF D";
                query.SQL.Add("  where M.HYKTYPE=D.HYKTYPE");
                if (iJLBH != 0)
                    query.SQL.Add("  and M.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    MOBILE_SIGN_GZMX item = new MOBILE_SIGN_GZMX();
                    ((GTPT_WXQDSJFGZDY)lst[0]).itemTable.Add(item);
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.iZSJF = query.FieldByName("ZSJF").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    query.Next();
                }
                query.Close();
                if (((GTPT_WXQDSJFGZDY)lst[0]).itemTable.Count > 0 )
                {
                    query.SQL.Text = "select I.JLBH,I.HYKTYPE,I.ZSJF,I.COUNTS,D.HYKNAME from  MOBILE_SIGN_GZITEM I,HYKDEF D ";
                    query.SQL.Add("  where I.HYKTYPE=D.HYKTYPE");
                    if (iJLBH != 0)
                        query.SQL.Add("  and JLBH=" + iJLBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        MOBILE_SIGN_GZITEM item2 = new MOBILE_SIGN_GZITEM();
                        ((GTPT_WXQDSJFGZDY)lst[0]).itemTable1.Add(item2);
                        item2.iHYKTYPES = query.FieldByName("HYKTYPE").AsInteger;
                        item2.iZSJFS = query.FieldByName("ZSJF").AsInteger;
                        item2.iCOUNTS = query.FieldByName("COUNTS").AsInteger;
                        item2.sHYKNAMES = query.FieldByName("HYKNAME").AsString;
                        query.Next();
                    }
                }
                query.Close();
            }
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXQDSJFGZDY item = new GTPT_WXQDSJFGZDY();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.iCOUNTS = query.FieldByName("COUNTS").AsInteger;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iQDR = query.FieldByName("QDR").AsInteger;
            item.dQDRQ = FormatUtils.DatetimeToString(query.FieldByName("QDRQ").AsDateTime);
            item.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.sCONTENT = query.FieldByName("CONTENT").AsString;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.sQDRMC = query.FieldByName("QDRMC").AsString;
            item.sZZRMC = query.FieldByName("ZZRMC").AsString;
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            return item;
        }

        //启动
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update MOBILE_SIGN_GZ set ZZR=:ZZR,ZZRQ=sysdate,STATUS=3,ZZRMC=:ZZRMC where STATUS=2";
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ExecSQL();
            ExecTable(query, "MOBILE_SIGN_GZ", serverTime, "JLBH", "QDR", "QDRMC", "QDRQ", 2);
        }
        //终止
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_SIGN_GZ", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }


    }
}
