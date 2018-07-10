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
    public class GTPT_WXJFDHLPLQD_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iHYID = 0;
        public int iCLLX = 0;
        public string sHYK_NO = string.Empty;
        //public new string[] asFieldNames = {
        //                                   "iJLBH;A.JLBH",
        //                                   "iDJR;A.DJR",
        //                                   "sDJRMC;A.DJRMC",
        //                                   "dDJSJ;A.DJSJ",
        //                                   "iZXR;A.ZXR",
        //                                   "sZXRMC;A.ZXRMC",
        //                                   "dZXRQ;A.ZXRQ",
        //                                   "sBGDDMC;B.BGDDMC",
        //                                   "sHYK_NO;C.HYK_NO",
        //                                   "iFFJLBH;I.FFJLBH",
        //                                   "iLPSL;I.LPSL",
        //                                   "fDHJF;I.DHJF",
        //                                   "iLPID;I.LPID",
        //                                   "sLPMC;C.LPMC",
        //                               };
        public List<WX_JFHL_CLJLITEM> itemTable = new List<WX_JFHL_CLJLITEM>();
        public class WX_JFHL_CLJLITEM
        {
            public int iFFJLBH = 0, iLPID = 0, iLPLX = 0, iJLBH;
            public int iLPSL = 0;
            public string sLPMC = string.Empty;
            public double fDHJF = 0;
        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_JFHL_CLJL;WX_JFHL_CLJLITEM", "JLBH", iJLBH, "ZXR");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            msg = string.Empty;

            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_JFHL_CLJL");
            query.SQL.Text = "insert into WX_JFHL_CLJL(JLBH,DJSJ,DJR,DJRMC,HYID,CZDD,CLLX)";
            query.SQL.Add("values(:JLBH,:DJSJ,:DJR,:DJRMC,:HYID,:BGDDDM,:CLLX)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("CLLX").AsInteger = iCLLX;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;

            query.ExecSQL();
            foreach (WX_JFHL_CLJLITEM one in itemTable)
            {


                int BJ_KC = 0;
                query.SQL.Text = "select BJ_WKC from HYK_JFFHLPXX ";
                query.SQL.Add("where LPID=:LPID");
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.Open();

                BJ_KC = query.FieldByName("BJ_WKC").AsInteger;


                query.SQL.Text = "insert into WX_JFHL_CLJLITEM(JLBH,FFJLBH,LPID,LPSL)";//,DHJF
                query.SQL.Add(" values (:JLBH,:FFJLBH,:LPID,:LPSL)");//,:DHJF
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("FFJLBH").AsInteger = one.iFFJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("LPSL").AsInteger = one.iLPSL;
                query.ExecSQL();
                if (iCLLX == 1)
                {
                    query.SQL.Text = "update WX_JFFLJL set STATUS=1,LJR=:LJR,LJRMC=:LJRMC,LJSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0);
                    query.SQL.Add(" where STATUS=0 and JYBH=:JYBH and LPID=:LPID");
                    query.ParamByName("JYBH").AsInteger = one.iFFJLBH;
                    query.ParamByName("LPID").AsInteger = one.iLPID;
                    query.ParamByName("LJR").AsInteger = iLoginRYID;
                    query.ParamByName("LJRMC").AsString = sLoginRYMC;
                    query.ExecSQL();

                    if (BJ_KC == 0)
                    {
                        query.SQL.Text = "update HYK_JFFHLPKC set KCSL=" + CrmLibProc.GetDbSystemField(DbSystemName, 2) + "(KCSL,0) - :LPSL ";
                        query.SQL.Add("where LPID=:LPID and BGDDDM=:BGDDDM");
                        query.ParamByName("LPSL").AsInteger = one.iLPSL;
                        query.ParamByName("LPID").AsInteger = one.iLPID;
                        query.ParamByName("BGDDDM").AsString = sBGDDDM;
                        query.ExecSQL();
                    }
                }
                else if (iCLLX == 2 || iCLLX == 3)
                {
                    double fDHJF_Z = 0;
                    query.SQL.Text = "select  DHJF from WX_JFFLJL ";
                    query.SQL.Add("where JYBH=:JYBH");
                    query.ParamByName("JYBH").AsInteger = one.iFFJLBH;
                    query.Open();

                    fDHJF_Z = query.FieldByName("DHJF").AsFloat;
                   

                    if (iCLLX == 2)
                    {
                        query.SQL.Text = "update WX_JFFLJL set STATUS=2,QXR=:CZR,QXRMC=:CZRMC,QXSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0);
                        query.SQL.Add("where STATUS=1 and JYBH=:JYBH and LPID=:LPID");
                        query.ParamByName("JYBH").AsInteger = one.iFFJLBH;
                        query.ParamByName("LPID").AsInteger = one.iLPID;
                        query.ParamByName("CZR").AsInteger = iLoginRYID;
                        query.ParamByName("CZRMC").AsString = sLoginRYMC;

                        query.ExecSQL();

                        if (BJ_KC == 0)
                        {
                            query.SQL.Text = "update HYK_JFFHLPKC set KCSL=" + CrmLibProc.GetDbSystemField(DbSystemName, 2) + "(KCSL,0) + :LPSL ";
                            query.SQL.Add("where LPID=:LPID and BGDDDM=:BGDDDM");
                            query.ParamByName("LPSL").AsInteger = one.iLPSL;
                            query.ParamByName("LPID").AsInteger = one.iLPID;
                            query.ParamByName("BGDDDM").AsString = sBGDDDM;
                            query.ExecSQL();
                        }
                        CrmLibProc.UpdateJFZH(out msg, query, 5, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFFLZX_CZ, iJLBH, one.iFFJLBH, fDHJF_Z, iLoginRYID, sLoginRYMC, "微信积分换礼冲正");

                    }
                    else if (iCLLX == 3)
                    {
                        query.SQL.Text = "update WX_JFFLJL set STATUS=3,QXR=:CZR,QXRMC=:CZRMC,QXSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0);
                        query.SQL.Add("where STATUS=0 and JYBH=:JYBH and LPID=:LPID");
                        query.ParamByName("JYBH").AsInteger = one.iFFJLBH;
                        query.ParamByName("LPID").AsInteger = one.iLPID;
                        query.ParamByName("CZR").AsInteger = iLoginRYID;
                        query.ParamByName("CZRMC").AsString = sLoginRYMC;
                        query.ExecSQL();
                        CrmLibProc.UpdateJFZH(out msg, query, 5, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFFLZX_CZ, iJLBH, one.iFFJLBH, fDHJF_Z, iLoginRYID, sLoginRYMC, "微信积分换礼取消兑换");

                    }
                }
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iSTATUS", "A.STATUS");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("sDJRMC", "A.DJRMC");

            query.SQL.Text = "select A.*,B.BGDDMC ,C.HYK_NO ";
            query.SQL.Add("   from WX_JFHL_CLJL A,HYK_BGDD B, HYK_HYXX C");
            query.SQL.Add("  where A.CZDD=B.BGDDDM and A.HYID=C.HYID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,L.LPMC FROM WX_JFHL_CLJLITEM I,HYK_JFFHLPXX L";
                query.SQL.Add("   where I.LPID=L.LPID");
                if (iJLBH != 0)
                    query.SQL.Add("  and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_JFHL_CLJLITEM item = new WX_JFHL_CLJLITEM();
                    ((GTPT_WXJFDHLPLQD_Proc)lst[0]).itemTable.Add(item);
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.iFFJLBH = query.FieldByName("FFJLBH").AsInteger;
                    item.iLPID = query.FieldByName("LPID").AsInteger;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    item.iLPSL = query.FieldByName("LPSL").AsInteger;
                    item.sLPMC = query.FieldByName("LPMC").AsString;
                    query.Next();
                }
            }


            return lst;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFHL_CLJL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ");
            //foreach (WX_JFHL_CLJLITEM one in itemTable)
            //{
            //    //CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_ZF, iJLBH, sBGDDDM, -one.iLPSL, iLoginRYID.ToString(), sLoginRYMC);
            //    query.SQL.Text = "  update WX_JFFLJL_LP set STATUS=:STATUS,LJR=:LJR,LJRMC=:LJRMC,LJSJ=:LJSJ where STATUS=0 and JYBH=:JYBH and LPID=:LPID ";
            //    query.ParamByName("LPID").AsInteger = one.iLPID;
            //    query.ParamByName("LJR").AsInteger = iLoginRYID;
            //    query.ParamByName("LJRMC").AsString = sLoginRYMC;
            //    query.ParamByName("LJSJ").AsDateTime = serverTime;
            //    query.ParamByName("JYBH").AsInteger = one.iFFJLBH;
            //    query.ParamByName("STATUS").AsInteger = 1;
            //    query.ExecSQL();

            //}
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXJFDHLPLQD_Proc item = new GTPT_WXJFDHLPLQD_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.iCLLX = query.FieldByName("CLLX").AsInteger;
            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }
    }
}
