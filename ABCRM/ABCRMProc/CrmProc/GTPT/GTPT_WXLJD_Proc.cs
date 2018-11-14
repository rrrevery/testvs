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
    public class GTPT_WXLJD_Proc : DJLR_ZX_CLass
    {
        public double fKCSL = 0;
        public int iLPID = 0;
        public string sHYK_NO = string.Empty;
        public int iHYID = 0;
        public int iPUBLICID = 0;

        public new string[] asFieldNames = {
                                           "iJLBH;A.JLBH",
                                           "iDJR;A.DJR",
                                           "sDJRMC;A.DJRMC",
                                           "dDJSJ;A.DJSJ",
                                           "iZXR;A.ZXR",
                                           "sZXRMC;A.ZXRMC",
                                           "dZXRQ;A.ZXRQ",
                                           "iFFJLBH;I.FFJLBH",
                                           "iJC;I.JC",
                                           "fLPSL;I.LPSL",
                                           "iLPID;I.LPID",
                                           "sLPMC;C.LPMC",
                                            "sBGDDMC;B.BGDDMC",
                                       };

        public List<GTPT_WXLJDItem> itemTable = new List<GTPT_WXLJDItem>();

        public class GTPT_WXLJDItem
        {
            public int iJC = 0, iFFJLBH = 0, iLPID = 0, iTYPE = 0, iLBID = 0;
            public int iJLBH = 0;
            public string sLPMC = string.Empty;
            public string sBGDDDM = string.Empty;
            public string sJCMC = string.Empty;


        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_SW_LJJL;MOBILE_SW_LJJLITEM", "JLBH", iJLBH);

        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_SW_LJJL");
            query.SQL.Text = "SELECT *  from  WX_HYKHYXX  where HYID=" + iHYID;
            query.Open();
            if (!query.IsEmpty)
                sUNIONID = query.FieldByName("UNIONID").AsString;
            query.Close();
            query.SQL.Text = "insert into MOBILE_SW_LJJL(JLBH,DJSJ,DJR,DJRMC,HYID,CZDD,UNIONID,PUBLICID)";
            query.SQL.Add("values(:JLBH,:DJSJ,:DJR,:DJRMC,:HYID,:BGDDDM,:UNIONID,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("UNIONID").AsString = sUNIONID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;


            query.ExecSQL();

            foreach (GTPT_WXLJDItem one in itemTable)
            {
                query.SQL.Text = "insert into MOBILE_SW_LJJLITEM(JLBH,FFJLBH,JC,LPID,LBID,TYPE)";
                query.SQL.Add(" values (:JLBH,:FFJLBH,:JC,:LPID,:LBID,:TYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("FFJLBH").AsInteger = one.iFFJLBH;
                query.ParamByName("JC").AsInteger = one.iJC;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("TYPE").AsInteger = one.iTYPE;
                query.ParamByName("LBID").AsInteger = one.iLBID;
                query.ExecSQL();
            }
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","A.JLBH");
            CondDict.Add("iDJR","A.DJR");
            CondDict.Add("iZXR","A.ZXR");
            CondDict.Add("dDJSJ","A.DJSJ");
            CondDict.Add("dZXRQ","A.ZXRQ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "SELECT A.*,B.BGDDMC,C.HYK_NO ";
            query.SQL.Add(" from MOBILE_SW_LJJL A,HYK_BGDD B,HYK_HYXX C");
            query.SQL.Add(" where A.CZDD=B.BGDDDM  and A.HYID=C.HYID and  A.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "SELECT I.*,LPMC ,D.MC JCMC";
                query.SQL.Add("  FROM MOBILE_SW_LJJLITEM I,HYK_JFFHLPXX C,MOBILE_JCDEF D");
                query.SQL.Add(" where I.LPID=C.LPID AND I.JC=D.JC ");
                if (iJLBH != 0)
                    query.SQL.Add("  AND I.JLBH=" + iJLBH);

                query.SQL.Add(" UNION");
                query.SQL.Add("SELECT I.*,LPMC ,'' JCMC");
                query.SQL.Add(" FROM MOBILE_SW_LJJLITEM I,HYK_JFFHLPXX C");
                query.SQL.Add("where I.LPID=C.LPID AND I.JC=-1");


                if (iJLBH != 0)
                    query.SQL.Add("  AND I.JLBH=" + iJLBH);



                query.Open();
                while (!query.Eof)
                {
                    GTPT_WXLJDItem item = new GTPT_WXLJDItem();
                    ((GTPT_WXLJD_Proc)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.iFFJLBH = query.FieldByName("FFJLBH").AsInteger;
                    item.iJC = query.FieldByName("JC").AsInteger;
                    item.iLBID = query.FieldByName("LBID").AsInteger;
                    item.iLPID = query.FieldByName("LPID").AsInteger;
                    item.iTYPE = query.FieldByName("TYPE").AsInteger;
                    item.sJCMC = query.FieldByName("JCMC").AsString;
                    item.sLPMC = query.FieldByName("LPMC").AsString;

                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_SW_LJJL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ");
            foreach (GTPT_WXLJDItem one in itemTable)
            {
                // CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_ZF, iJLBH, sBGDDDM, -1, iLoginRYID.ToString(), sLoginRYMC);

                query.SQL.Text = " update MOBILE_LPFFZX_HY_MXITEM set STATUS=:STATUS,LJSJ=:LJSJ,LJJLBH=:LJJLBH WHERE JYBH=:JYBH AND LBID=:LBID AND LPID=:LPID AND STATUS=0 ";
                query.ParamByName("LJJLBH").AsInteger = iJLBH;
                query.ParamByName("LJSJ").AsDateTime = serverTime;
                query.ParamByName("JYBH").AsInteger = one.iFFJLBH;
                query.ParamByName("LBID").AsInteger = one.iLBID;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("STATUS").AsInteger = 2;

                query.ExecSQL();
            }
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXLJD_Proc item = new GTPT_WXLJD_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return item;
        }
    }
}
