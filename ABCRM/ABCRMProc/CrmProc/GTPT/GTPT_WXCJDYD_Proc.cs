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
    public class GTPT_WXCJDYD_Proc : DJLR_ZXQDZZ_CLass
    {
        public int A = 0;
        public int iGZID = 0;

        public int iDJLX = 0;
        public int iPUBLICID = 0;

        public string sGZMC = string.Empty;

        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string dLJYXQ = string.Empty;
        public List<WX_LPFFDYDItem> itemTable = new List<WX_LPFFDYDItem>();
        public List<MOBILE_LPFFGZDYD_MD> itemTable2 = new List<MOBILE_LPFFGZDYD_MD>();
        public class WX_LPFFDYDItem
        {
            public int iJLBH = 0;
            public string dRQ = string.Empty;
            public int iXZSL = 0;
            public string sWXTS = string.Empty;
        }

        public class MOBILE_LPFFGZDYD_MD
        {
            public int iMDID = 0;
            public string sMDMC = string.Empty;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_LPFFGZDYD;MOBILE_LPFFGZDYDITEM;MOBILE_LPFFGZDYD_MD", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_LPFFGZDYD");
            query.SQL.Text = "insert into MOBILE_LPFFGZDYD(JLBH,GZID,DJLX,KSRQ,JSRQ,DJSJ,DJR,DJRMC,STATUS,LJYXQ,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:GZID,:DJLX,:KSRQ,:JSRQ,:DJSJ,:DJR,:DJRMC,0,:LJYXQ,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GZID").AsInteger = iGZID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("LJYXQ").AsDateTime = FormatUtils.ParseDateString(dLJYXQ);
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ExecSQL();
            foreach (WX_LPFFDYDItem one in itemTable)
            {
                query.SQL.Text = "insert into MOBILE_LPFFGZDYDITEM(JLBH,RQ,XZSL,WXTS)";
                query.SQL.Add(" values(:JLBH,:RQ,:XZSL,:WXTS)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("RQ").AsInteger = Convert.ToInt32(Convert.ToDateTime(one.dRQ).ToString("yyyyMMdd"));
                query.ParamByName("XZSL").AsInteger = one.iXZSL;
                query.ParamByName("WXTS").AsString = one.sWXTS;
                query.ExecSQL();
            }

            foreach (MOBILE_LPFFGZDYD_MD ones in itemTable2)
            {
                query.SQL.Text = "insert into MOBILE_LPFFGZDYD_MD(JLBH,MDID)";
                query.SQL.Add(" values(:JLBH,:MDID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = ones.iMDID;
                query.ExecSQL();
            }

        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iGZID", "W.GZID");
            CondDict.Add("sGZMC", "D.GZMC");
            CondDict.Add("iDJLX", "W.DJLX");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("dLJYXQ", "W.LJYXQ");
            CondDict.Add("dRQ", "I.RQ");
            CondDict.Add("iXZSL", "I.XZSL");
            CondDict.Add("sWXTS", "I.WXTS");
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

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.*,D.GZMC ";
            query.SQL.Add("from MOBILE_LPFFGZDYD W,MOBILE_LPFFGZ D");
            query.SQL.Add(" where  W.GZID=D.GZID  and  W.PUBLICID=" + iLoginPUBLICID);
            if (iJLBH != 0)
                query.SQL.AddLine("  and W.JLBH=" + iJLBH);
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select B.* ,A.MDMC from MOBILE_LPFFGZDYD_MD B ,WX_MDDY A ";
                query.SQL.Add(" where B.MDID=A.MDID ");
                if (iJLBH != 0)
                    query.SQL.Add("  and JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    MOBILE_LPFFGZDYD_MD item_MD = new MOBILE_LPFFGZDYD_MD();
                    ((GTPT_WXCJDYD_Proc)lst[0]).itemTable2.Add(item_MD);
                    item_MD.iMDID = query.FieldByName("MDID").AsInteger;
                    item_MD.sMDMC = query.FieldByName("MDMC").AsString;
                    query.Next();
                }
                query.Close();

                query.SQL.Text = "select * from MOBILE_LPFFGZDYDITEM I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    WX_LPFFDYDItem obj = new WX_LPFFDYDItem();
                    ((GTPT_WXCJDYD_Proc)lst[0]).itemTable.Add(obj);
                    obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                    obj.dRQ = FormatUtils.ParseDateTimeStringFmt(query.FieldByName("RQ").AsInteger.ToString(), "yyyyMMdd").ToString("yyyy-MM-dd");
                    obj.iXZSL = query.FieldByName("XZSL").AsInteger;
                    obj.sWXTS = query.FieldByName("WXTS").AsString;
                    query.Next();
                }

            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXCJDYD_Proc obj = new GTPT_WXCJDYD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iGZID = query.FieldByName("GZID").AsInteger;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.dQDSJ = FormatUtils.DateToString(query.FieldByName("QDSJ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.dZZSJ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.dLJYXQ = FormatUtils.DateToString(query.FieldByName("LJYXQ").AsDateTime);
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return obj;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_LPFFGZDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update MOBILE_LPFFGZDYD set ZZR=:ZZR,ZZSJ=sysdate,STATUS=3,ZZRMC=:ZZRMC where DJLX=:DJLX and STATUS=2 and GZID=:GZID and PUBLICID=:PUBLICID";
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("GZID").AsInteger = iGZID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ExecSQL();

            query.SQL.Text = "update MOBILE_LPFFGZDYD set QDR=:QDR,QDRMC=:QDRMC,QDSJ=sysdate,STATUS=2 where PUBLICID=:PUBLICID and  JLBH=" + iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRMC").AsString = sLoginRYMC;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

            query.ExecSQL();
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_LPFFGZDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZSJ", 3);
        }
    }
}
