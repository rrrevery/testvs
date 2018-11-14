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


namespace BF.CrmProc.GTPT
{
    public class GTPT_WXJFDHLPD_Proc : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sPUBLICNAME = string.Empty;

        public string dLJYXQ = string.Empty;
        public int iCLFS = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;

        public List<WX_JFFLGZ_LPITEM> itemTable = new List<WX_JFFLGZ_LPITEM>();
        public List<WX_JFFLGZ_LPITEM_JF> itemTableLP = new List<WX_JFFLGZ_LPITEM_JF>();

        public class WX_JFFLGZ_LPITEM
        {
            public int iJLBH = 0;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
        }
        public class WX_JFFLGZ_LPITEM_JF
        {
            public int iLPID = 0;
            public double fLPJF = 0;
            public int iHYKTYPE = 0;
            public int iXZCS_HY = 0;
            public int iXZCS_DAY_HY = 0;
            public int iXZCS = 0, iXZCS_DAY;
            public string sWXZY = string.Empty;
            public string sXZCS_HY_TS = string.Empty;
            public string sXZCS_TS = string.Empty;
            public string sXZCS_DAY_TS = string.Empty;
            public string sXZCS_DAY_HY_TS = string.Empty;
            public string sLPMC = string.Empty;
            public string sHYKNAME = string.Empty;

        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_JFFLGZ_LP;WX_JFFLGZ_LPITEM;WX_JFFLGZ_LPITEM_JF", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_JFFLGZ_LP");

            query.SQL.Text = "insert into WX_JFFLGZ_LP(JLBH,KSRQ,JSRQ,CLFS,STATUS,BZ,MDID,LJYXQ,DJR,DJRMC,DJSJ,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:CLFS,:STATUS,:BZ,:MDID,:LJYXQ,:DJR,:DJRMC,:DJSJ,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("CLFS").AsInteger = iCLFS;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("LJYXQ").AsDateTime = FormatUtils.ParseDateString(dLJYXQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("BZ").AsString = sBZ;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ExecSQL();
            foreach (WX_JFFLGZ_LPITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_JFFLGZ_LPITEM(JLBH,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ExecSQL();
            }
            foreach (WX_JFFLGZ_LPITEM_JF ones in itemTableLP)
            {
                query.SQL.Text = "insert into WX_JFFLGZ_LPITEM_JF(JLBH,HYKTYPE,LPID,LPJF,XZCS_HY,XZCS_DAY_HY,XZCS,XZCS_DAY,WXZY,XZCS_HY_TS,XZCS_DAY_HY_TS,XZCS_TS,XZCS_DAY_TS)";
                query.SQL.Add(" values(:JLBH,:HYKTYPE,:LPID,:LPJF,:XZCS_HY,:XZCS_DAY_HY,:XZCS,:XZCS_DAY,:WXZY,:XZCS_HY_TS,:XZCS_DAY_HY_TS,:XZCS_TS,:XZCS_DAY_TS)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = ones.iHYKTYPE;
                query.ParamByName("LPID").AsInteger = ones.iLPID;
                query.ParamByName("LPJF").AsFloat = ones.fLPJF;
                query.ParamByName("XZCS_HY").AsInteger = ones.iXZCS_HY;
                query.ParamByName("XZCS_DAY_HY").AsInteger = ones.iXZCS_DAY_HY;
                query.ParamByName("XZCS").AsInteger = ones.iXZCS;
                query.ParamByName("XZCS_DAY").AsInteger = ones.iXZCS_DAY;
                query.ParamByName("WXZY").AsString = ones.sWXZY;
                query.ParamByName("XZCS_HY_TS").AsString = ones.sXZCS_HY_TS;
                query.ParamByName("XZCS_DAY_HY_TS").AsString = ones.sXZCS_DAY_HY_TS;
                query.ParamByName("XZCS_TS").AsString = ones.sXZCS_TS;
                query.ParamByName("XZCS_DAY_TS").AsString = ones.sXZCS_DAY_TS;
                query.ExecSQL();

            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            query.SQL.Text = "select A.*,P.PUBLICNAME,M.MDMC from  WX_JFFLGZ_LP A,WX_PUBLIC P,WX_MDDY M ";
            query.SQL.Add(" where  A.PUBLICID=P.PUBLICID and A.MDID=M.MDID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select M.*,D.HYKNAME from  WX_JFFLGZ_LPITEM M,HYKDEF D";
                query.SQL.Add(" WHERE M.HYKTYPE=D.HYKTYPE");
                if (iJLBH != 0)
                    query.SQL.Add("  and JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_JFFLGZ_LPITEM item = new WX_JFFLGZ_LPITEM();
                    ((GTPT_WXJFDHLPD_Proc)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    query.Next();
                }
                query.Close();
                if (((GTPT_WXJFDHLPD_Proc)lst[0]).itemTable.Count > 0)
                {
                    query.SQL.Text = " SELECT M.*,N.LPMC,D.HYKNAME from WX_JFFLGZ_LPITEM_JF M,HYK_JFFHLPXX N ,HYKDEF D";
                    query.SQL.Add("   where M.LPID=N.LPID and M.HYKTYPE=D.HYKTYPE");
                    if (iJLBH != 0)
                        query.SQL.Add("  and M.JLBH=" + iJLBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        WX_JFFLGZ_LPITEM_JF item2 = new WX_JFFLGZ_LPITEM_JF();
                        ((GTPT_WXJFDHLPD_Proc)lst[0]).itemTableLP.Add(item2);
                        item2.iLPID = query.FieldByName("LPID").AsInteger;
                        item2.fLPJF = query.FieldByName("LPJF").AsFloat;
                        item2.iXZCS_HY = query.FieldByName("XZCS_HY").AsInteger;
                        item2.iXZCS_DAY_HY = query.FieldByName("XZCS_DAY_HY").AsInteger;
                        item2.iXZCS = query.FieldByName("XZCS").AsInteger;
                        item2.iXZCS_DAY = query.FieldByName("XZCS_DAY").AsInteger;
                        item2.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item2.sLPMC = query.FieldByName("LPMC").AsString;
                        item2.sWXZY = query.FieldByName("WXZY").AsString;
                        item2.sXZCS_HY_TS = query.FieldByName("XZCS_HY_TS").AsString;
                        item2.sXZCS_DAY_HY_TS = query.FieldByName("XZCS_DAY_HY_TS").AsString;
                        item2.sXZCS_TS = query.FieldByName("XZCS_TS").AsString;
                        item2.sXZCS_DAY_TS = query.FieldByName("XZCS_DAY_TS").AsString;
                        item2.sHYKNAME = query.FieldByName("HYKNAME").AsString;

                        query.Next();
                    }
                }
                query.Close();
            }
            return lst;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFFLGZ_LP", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            msg = string.Empty;
            query.SQL.Text = "update WX_JFFLGZ_LP set ZZR=:ZZR,ZZRQ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=3,ZZRMC=:ZZRMC where JLBH in(select JLBH from WX_JFFLGZ_LP where KSRQ<=:JSRQ and JSRQ>=:KSRQ and STATUS=2)  and  PUBLICID=" + iLoginPUBLICID;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ExecSQL();

            query.SQL.Text = "update WX_JFFLGZ_LP set QDR=:QDR,QDRMC=:QDRMC,QDSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=:STATUS where JLBH=" + iJLBH;
            query.ParamByName("STATUS").AsInteger = 2;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRMC").AsString = sLoginRYMC;
            query.ExecSQL();
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFFLGZ_LP", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXJFDHLPD_Proc item = new GTPT_WXJFDHLPD_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.iCLFS = query.FieldByName("CLFS").AsInteger;
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
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.dLJYXQ = FormatUtils.DatetimeToString(query.FieldByName("LJYXQ").AsDateTime);

            return item;
        }
    }
}
