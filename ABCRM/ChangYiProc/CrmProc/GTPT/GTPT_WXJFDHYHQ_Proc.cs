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


namespace BF.CrmProc
{
    public class GTPT_WXJFDHYHQ_Proc : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sBZ = string.Empty;
        public string dSYJSRQ = string.Empty;
        public int iCLFS = 0;
        public int iMDID = 0;
        public int iHYKTYPE = 0, iYHQID, iYHQFS;
        public string sHYKNAME = string.Empty;
        public string sYHQMC = string.Empty;
        public int iSYDAY = 0;
        public int iJFCLFS = 0, iBJ_KCDYJF,iPUBLICID;
        public string sPUBLICNAME = string.Empty;
        public string sMDMC = string.Empty;


        public List<WX_JFFLGZITEM> itemTable = new List<WX_JFFLGZITEM>();
        public class WX_JFFLGZITEM
        {
            public int iJLBH = 0;
            public int iXH = 0;
            public double fJFXX = 0, fJFSX, fFLBL;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_JFFLGZ;WX_JFFLGZITEM", "JLBH", iJLBH);
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "Z.JLBH");
            CondDict.Add("iYHQID", "Z.YHQID");
            CondDict.Add("iHYKTYPE", "Z.HYKTYPE");

            query.SQL.Text = "select Z.*,E.YHQMC,F.HYKNAME,M.MDMC ";
            query.SQL.Add("from WX_JFFLGZ Z,YHQDEF E,HYKDEF F,WX_MDDY M");
            query.SQL.Add("where Z.YHQID=E.YHQID and Z.HYKTYPE=F.HYKTYPE and Z.MDID=M.MDID and Z.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);
            if(lst.Count==1)
            {
                query.SQL.Text = "select A.* from WX_JFFLGZITEM A where 1=1";
                query.SQL.Add("  and A.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_JFFLGZITEM item = new WX_JFFLGZITEM();
                    ((GTPT_WXJFDHYHQ_Proc)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.iXH = query.FieldByName("XH").AsInteger;
                    item.fJFXX = query.FieldByName("JFXX").AsFloat;
                    item.fJFSX = query.FieldByName("JFSX").AsFloat;
                    item.fFLBL = query.FieldByName("FLBL").AsFloat;
                    query.Next();
                }
                query.Close();
            }

            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_JFFLGZ");

            query.SQL.Text = "insert into  WX_JFFLGZ(JLBH,KSRQ,JSRQ,STATUS,DJSJ,DJR,DJRMC,CLFS,JFCLFS,BJ_KCDYJF,MDID,HYKTYPE,YHQID,YHQFS,SYJSRQ,SYDAY,BZ,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:STATUS,:DJSJ,:DJR,:DJRMC,:CLFS,:JFCLFS,:BJ_KCDYJF,:MDID,:HYKTYPE,:YHQID,:YHQFS,:SYJSRQ,:SYDAY,:BZ,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CLFS").AsInteger = iCLFS;
            query.ParamByName("JFCLFS").AsInteger = iJFCLFS;
            query.ParamByName("BJ_KCDYJF").AsInteger = iBJ_KCDYJF;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("YHQFS").AsInteger = iYHQFS;
            query.ParamByName("SYJSRQ").AsDateTime = FormatUtils.ParseDateString(dSYJSRQ);
            query.ParamByName("SYDAY").AsInteger = iSYDAY;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("BZ").AsString = sBZ;
            query.ExecSQL();
            foreach (WX_JFFLGZITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_JFFLGZITEM(JLBH,XH,JFXX,JFSX,FLBL)";
                query.SQL.Add(" values(:JLBH,:XH,:JFXX,:JFSX,:FLBL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ParamByName("JFXX").AsFloat = one.fJFXX;
                query.ParamByName("JFSX").AsFloat = one.fJFSX;
                query.ParamByName("FLBL").AsFloat = one.fFLBL;
                query.ExecSQL();

            }

        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFFLGZ", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            query.SQL.Text = "update WX_JFFLGZ set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=:STATUS where STATUS=2 and PUBLICID=:PUBLICID and HYKTYPE=" + iHYKTYPE;
            query.ParamByName("STATUS").AsInteger = 3;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            if (DbSystemName == "ORACLE")
            {
                query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            }
            else if (DbSystemName == "SYBASE")
            {
                query.ParamByName("ZZRMC").AsChineseString = sLoginRYMC;
            }
            query.ExecSQL();

            query.SQL.Text = "update WX_JFFLGZ set QDR=:QDR,QDRMC=:QDRMC,QDSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=:STATUS where PUBLICID=:PUBLICID and JLBH=" + iJLBH;
            query.ParamByName("STATUS").AsInteger = 2;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            if (DbSystemName == "ORACLE")
            {
                query.ParamByName("QDRMC").AsString = sLoginRYMC;
            }
            else if (DbSystemName == "SYBASE")
            {
                query.ParamByName("QDRMC").AsChineseString = sLoginRYMC;
            }
            query.ExecSQL();
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFFLGZ", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXJFDHYHQ_Proc item = new GTPT_WXJFDHYHQ_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iCLFS = query.FieldByName("CLFS").AsInteger;
            item.iJFCLFS = query.FieldByName("JFCLFS").AsInteger;
            item.iYHQFS = query.FieldByName("YHQFS").AsInteger;
            item.iSYDAY = query.FieldByName("SYDAY").AsInteger;
            item.iBJ_KCDYJF = query.FieldByName("BJ_KCDYJF").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iQDR = query.FieldByName("QDR").AsInteger;
            item.sQDRMC = query.FieldByName("QDRMC").AsString;
            item.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.sZZRMC = query.FieldByName("ZZRMC").AsString;
            item.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.sBZ = query.FieldByName("BZ").AsString;
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.dSYJSRQ = FormatUtils.DateToString(query.FieldByName("SYJSRQ").AsDateTime);
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return item;
        }
    }
}
