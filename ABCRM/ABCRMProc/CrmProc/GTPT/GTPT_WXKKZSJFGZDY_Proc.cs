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
    public class GTPT_WXKKZSJFGZDY_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iLX, iXZSL, iPUBLICID,iHYKTYPE = 0;
        public string sWXZY = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string dLJYXQ = string.Empty;

        public List<KKBKGZ_KLX> itemTable = new List<KKBKGZ_KLX>();
        public List<WX_CARD_LPFFGZItem> itemTable1 = new List<WX_CARD_LPFFGZItem>();

        public class KKBKGZ_KLX
        {
            public int iJLBH = 0;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
        }
        public class WX_CARD_LPFFGZItem
        {
            public int iJLBH = 0;
            public int iLBID = 0;
            public string sLBMC = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_CARD_LPFFGZ;MOBILE_CARD_LPFFGZITEM;", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_CARD_LPFFGZ");
            query.SQL.Text = "insert into MOBILE_CARD_LPFFGZ(JLBH,KSRQ,JSRQ,DJSJ,DJR,DJRMC,STATUS,LX,XZSL,ZY,LJYXQ,PUBLICID)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:DJSJ,:DJR,:DJRMC,0,:LX,:XZSL,:ZY,:LJYXQ,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("LX").AsInteger = iLX;
            query.ParamByName("XZSL").AsInteger = iXZSL;
            query.ParamByName("LJYXQ").AsDateTime = FormatUtils.ParseDateString(dLJYXQ);
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("ZY").AsString = sWXZY;


            query.ExecSQL();

            foreach (WX_CARD_LPFFGZItem one in itemTable1)
            {
                query.SQL.Text = "insert into MOBILE_CARD_LPFFGZITEM(JLBH,HYKTYPE,LBID)";
                query.SQL.Add(" values(:JLBH,:HYKTYPE,:LBID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("LBID").AsInteger = one.iLBID;
                query.ExecSQL();
            }

        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iLX","W.LX");
            CondDict.Add("dLJYXQ","W.LJYXQ");
            CondDict.Add("iXZSL","W.XZSL");
            CondDict.Add("sWXZY", "W.WXZY");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
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
            query.SQL.Text = "select W.*  from MOBILE_CARD_LPFFGZ W where W.PUBLICID=" + iLoginPUBLICID;
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select DISTINCT M.JLBH, M.HYKTYPE,D.HYKNAME from  MOBILE_CARD_LPFFGZITEM M,HYKDEF D";
                query.SQL.Add(" where M.HYKTYPE=D.HYKTYPE" );
                if (iJLBH != 0)
                    query.SQL.Add(" and M.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    KKBKGZ_KLX item = new KKBKGZ_KLX();
                    ((GTPT_WXKKZSJFGZDY_Proc)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    query.Next();
                }
                query.Close();

                query.SQL.Text = "select C.*,K.HYKNAME,B.LBMC";
                query.SQL.Add(" from MOBILE_CARD_LPFFGZITEM C,HYKDEF K,MOBILE_LPZDEF_YHQ  B");
                query.SQL.Add(" where C.HYKTYPE=K.HYKTYPE and B.LBID= C.LBID ");
                if (iJLBH != 0)
                    query.SQL.AddLine("  and C.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_CARD_LPFFGZItem obj = new WX_CARD_LPFFGZItem();
                    ((GTPT_WXKKZSJFGZDY_Proc)lst[0]).itemTable1.Add(obj);
                    obj.iLBID = query.FieldByName("LBID").AsInteger;
                    obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;

                    obj.sLBMC = query.FieldByName("LBMC").AsString;
                    obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    query.Next();
                }

            }

            return lst;

        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_CARD_LPFFGZ", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string DbSystemName = "ORACLE";

            query.SQL.Text = "update MOBILE_CARD_LPFFGZ set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=3 where STATUS=2 and JLBH<>:JLBH and QDR is not null and ZZR is null and LX=:LX  and PUBLICID=" + iLoginPUBLICID;
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("LX").AsInteger = iLX;

            query.ParamByName("ZZR").AsInteger = iLoginRYID;

            query.ParamByName("ZZRMC").AsString = sLoginRYMC;



            query.ExecSQL();
            query.SQL.Text = "update MOBILE_CARD_LPFFGZ set QDR=:QDR,QDRMC=:QDRMC,QDRQ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=2 where  PUBLICID=" + iLoginPUBLICID +"and  JLBH=" + iJLBH  ;
            query.ParamByName("QDR").AsInteger = iLoginRYID;

            query.ParamByName("QDRMC").AsString = sLoginRYMC;


            query.ExecSQL();
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_CARD_LPFFGZ", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXKKZSJFGZDY_Proc obj = new GTPT_WXKKZSJFGZDY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.iXZSL = query.FieldByName("XZSL").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iSHR = query.FieldByName("ZXR").AsInteger;
            obj.dSHRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.dQDRQ = FormatUtils.DateToString(query.FieldByName("QDRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.dLJYXQ = FormatUtils.DateToString(query.FieldByName("LJYXQ").AsDateTime);
            obj.sWXZY = query.FieldByName("ZY").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sSHRMC = query.FieldByName("ZXRMC").AsString;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return obj;
        }



    }
}
