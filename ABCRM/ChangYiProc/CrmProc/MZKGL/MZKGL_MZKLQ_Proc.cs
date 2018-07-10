﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;

using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKLQ_Proc : DJLR_BRBCDD_CLass
    {
        public int iHYKSL = 0, iBJ_CZK = 0;
        public int iLQR = 0;
        public string sLQRMC = string.Empty;
        public int iSQDJH = 0;
        public int iQRSHR = 0;
        public string sQRSHRMC = string.Empty;
        public string dQRSHRQ = string.Empty;
        public List<HYKGL_HYKLYKDItem> kditemTable = new List<HYKGL_HYKLYKDItem>();
        public List<HYKGL_HYKLYItem> itemTable = new List<HYKGL_HYKLYItem>();
        public List<HYKGL_LYSQItem> itemTable2 = new List<HYKGL_LYSQItem>();

        public class HYKGL_HYKLYKDItem
        {
            public string sCZKHM_BEGIN = string.Empty, sCZKHM_END = string.Empty;
            public int iHYKTYPE = 0, iSKSL = 0, iRANDOM_LEN = 0;
            public string sHYKNAME = string.Empty;
            public double fMZJE = 0;
        }

        public class HYKGL_LYSQItem
        {
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public int iSKSL = 0;
        }
        public string sCZKHM = "";
        public class HYKGL_HYKLYItem
        {
            public string sHM;
            public int iHYKTYPE = 0;
            public double fJE = 0;
            public string sLQRMC = string.Empty, sHYKNAME;
        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sBGDDDM_BC == "")
            {
                msg = CrmLibStrings.msgNeedBGDD_BC;
                return false;
            }
            if (sBGDDDM_BR == "")
            {
                msg = CrmLibStrings.msgNeedBGDD_BR;
                return false;
            }
            return true;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            CrmLibProc.DeleteDataTables(query, out msg, "MZK_LQJL;MZK_LQJLITEM;MZK_LQJLKDITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM_BR);//无锡华地 领取与发放时需要写MDID 2014.11.10
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_LQJL", 1);
            query.SQL.Text = "insert into MZK_LQJL(JLBH,BGDDDM_BC,BGDDDM_BR,HYKSL,";//ZY,
            query.SQL.Add(" LQR,LQRMC,BJ_CZK,DJSJ,DJR,DJRMC,STATUS,DJLX,MDID,SQDJH)");
            query.SQL.Add(" values(:JLBH,:BGDDDM_BC,:BGDDDM_BR,:HYKSL,");//:ZY,
            query.SQL.Add(" :LQR,:LQRMC,:BJ_CZK,:DJSJ,:DJR,:DJRMC,0,:DJLX,:MDID,:SQDJH)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM_BC").AsString = sBGDDDM_BC;
            query.ParamByName("BGDDDM_BR").AsString = sBGDDDM_BR;
            //query.ParamByName("HYKKH1").AsString = sHYKKH1;
            //query.ParamByName("HYKKH2").AsString = sHYKKH2;
            query.ParamByName("HYKSL").AsInteger = iHYKSL;
            //query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("LQR").AsInteger = iLQR;
            query.ParamByName("LQRMC").AsString = sLQRMC;
            query.ParamByName("BJ_CZK").AsInteger = iBJ_CZK;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("MDID").AsInteger = CrmLibProc.GetMDIDByRY(iLoginRYID);
            query.ParamByName("SQDJH").AsInteger = iSQDJH;
            query.ExecSQL();
            foreach (HYKGL_HYKLYKDItem one in kditemTable)
            {
                query.SQL.Text = "insert into MZK_LQJLKDITEM(JLBH,CZKHM_BEGIN,CZKHM_END,HYKTYPE,SKSL,MZJE)";
                query.SQL.Add(" values(:JLBH,:CZKHM_BEGIN,:CZKHM_END,:HYKTYPE,:SKSL,:MZJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("SKSL").AsInteger = one.iSKSL;
                query.ParamByName("MZJE").AsFloat = one.fMZJE;
                query.ExecSQL();
            }
            //重写卡数量
            query.SQL.Text = " select count(distinct CZKHM)";
            query.SQL.Add(" from MZKCARD A,MZK_LQJL C,MZK_LQJLKDITEM B");
            query.SQL.Add(" where C.JLBH=B.JLBH and B.CZKHM_BEGIN<=A.CZKHM and B.CZKHM_END>=A.CZKHM");
            query.SQL.Add(" and length(B.CZKHM_BEGIN)=length(A.CZKHM)");
            //query.SQL.Add(" and A.QCYE=B.MZJE");
            query.SQL.Add(" and A.HYKTYPE=B.HYKTYPE");
            query.SQL.Add(" and B.JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.Open();
            iHYKSL = query.Fields[0].AsInteger;
            query.Close();
            query.SQL.Text = "update MZK_LQJL set HYKSL=:HYKSL where JLBH=:JLBH";
            query.ParamByName("HYKSL").AsInteger = iHYKSL;
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_LQJL", serverTime, "JLBH");
            //if (iSQDJH != 0)
            //{
            //    ExecTableJLBH(query, "MZK_CARDLQSQJL", serverTime, iSQDJH, "JLBH", "LQR", "LQRMC", "LQRQ", 2);
            //    query.SQL.Text = "update MZK_CARDLQSQJL set LQDH=" + iJLBH;
            //    query.SQL.Add(" where JLBH=" + iSQDJH);
            //    query.ExecSQL();
            //}



            query.SQL.Text = "insert into MZK_LQJLITEM(JLBH,HM,JE,LQRMC,HYKTYPE)";
            query.SQL.Add(" select distinct C.JLBH,CZKHM,QCYE,LQRMC,A.HYKTYPE");
            query.SQL.Add(" from MZKCARD A,MZK_LQJL C,MZK_LQJLKDITEM B");
            query.SQL.Add(" where C.JLBH=B.JLBH and B.CZKHM_BEGIN<=A.CZKHM and B.CZKHM_END>=A.CZKHM");
            query.SQL.Add(" and length(B.CZKHM_BEGIN)=length(A.CZKHM)");
            // query.SQL.Add(" and A.QCYE=B.MZJE");
            query.SQL.Add(" and A.HYKTYPE=B.HYKTYPE");
            query.SQL.Add(" and B.JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();

            //query.SQL.Text = "update MZK_LQSQJL set STATUS=2 where JLBH=" + iSQDJH;
            //query.ExecSQL();
            /* 
             * 如果启用OLD，数据流向：
             * 领取：OLD->CRM，OLD要删除
             * 退领：CRM->OLD，CRM要删除
             * 调拨：CRM->CRM，不删除只更新
             * 否则只更新
            */
            if (System.Configuration.ConfigurationManager.AppSettings["EnableCRMDBOLD"] != "true" || iDJLX == BASECRMDefine.HYKLYD_DBD)
            {
                int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM_BR);
                int iCZK_CLLX = -1;
                query.SQL.Text = "update MZKCARD set BGDDDM=:BGDDDM_BR,BGR=:BGR,STATUS=:STATUS";
                query.SQL.Add(" where CZKHM in (select I.HM from MZK_LQJLITEM I where");
                query.SQL.Add(" I.JLBH=:JLBH) and BGDDDM=:BGDDDM_BC");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("BGR").AsInteger = iLQR;
                switch (iDJLX)
                {
                    case BASECRMDefine.HYKLYD_LYD: query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.KCKStatus.LY; iCZK_CLLX = (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_LY; break;
                    case BASECRMDefine.HYKLYD_DBD: query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.KCKStatus.LY; iCZK_CLLX = (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_DB; break;
                    case BASECRMDefine.HYKLYD_TLD: query.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.KCKStatus.JK; iCZK_CLLX = (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_TL; break;
                }
                query.ParamByName("BGDDDM_BC").AsString = sBGDDDM_BC;
                query.ParamByName("BGDDDM_BR").AsString = sBGDDDM_BR;
                //query.ParamByName("MDID").AsInteger = iMDID;
                query.ExecSQL();
                query.SQL.Clear();
                query.Params.Clear();

                System.Collections.ArrayList list_Hykno = new System.Collections.ArrayList();
                query.SQL.Text = "  select I.HM from MZK_LQJLITEM I where I.JLBH=:JLBH";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    list_Hykno.Add(query.FieldByName("HM").AsString);
                    query.Next();
                }
                foreach (string item in list_Hykno)
                {
                    CrmLibProc.SaveCardTrack(query, item, 0, iCZK_CLLX, iJLBH, iLoginRYID, sLoginRYMC, 0, "no", 0, sIPAddress);
                }
            }
            else
            {
                string sDBConnNameTo = iBJ_CZK == 0 ? "CRMDB" : "CRMDBMZK";
                if (iDJLX == BASECRMDefine.HYKLYD_TLD)
                    sDBConnNameTo = "CRMDBOLD";
                DbConnection conn2 = CyDbConnManager.GetActiveDbConnection(sDBConnNameTo);
                try
                {
                    CyQuery query2 = new CyQuery(conn2);
                    query2.Close();
                    DbTransaction dbTrans2 = conn2.BeginTransaction();
                    try
                    {
                        query2.SetTrans(dbTrans2);
                        int iMDID_ZB = CrmLibProc.BGDDToMDID(query, sBGDDDM_BR);
                        query.SQL.Text = "select * from MZKCARD where CZKHM in (select I.HM from CARDLQJLITEM I";
                        query.SQL.Add(" where I.JLBH=:JLBH) and BGDDDM=:BGDDDM_BC");
                        query.ParamByName("JLBH").AsInteger = iJLBH;
                        query.ParamByName("BGDDDM_BC").AsString = sBGDDDM_BC;
                        //select * from CARDLQJL(制卡库当中的表)  此表中记录了领取人 和 拨入地点（sBGDDDM_BR）转换成的的MDID
                        query.Open();
                        while (!query.Eof)
                        {
                            query2.SQL.Text = "insert into MZKCARD(CZKHM,CDNR,HYKTYPE,QCYE,PDJE,YXTZJE,JKFS,BGDDDM,BGR,STATUS,YXQ,XKRQ,FXDWID,YZM)";
                            query2.SQL.Add("values(:CZKHM,:CDNR,:HYKTYPE,:QCYE,:PDJE,:YXTZJE,:JKFS,:BGDDDM,:BGR,:STATUS,:YXQ,:XKRQ,:FXDWID,:YZM)");
                            query2.ParamByName("CZKHM").AsString = query.FieldByName("CZKHM").AsString;
                            query2.ParamByName("CDNR").AsString = query.FieldByName("CDNR").AsString;
                            query2.ParamByName("HYKTYPE").AsInteger = query.FieldByName("HYKTYPE").AsInteger;
                            query2.ParamByName("QCYE").AsFloat = query.FieldByName("QCYE").AsFloat;
                            query2.ParamByName("PDJE").AsFloat = query.FieldByName("PDJE").AsFloat;
                            query2.ParamByName("YXTZJE").AsFloat = query.FieldByName("YXTZJE").AsFloat;
                            query2.ParamByName("JKFS").AsInteger = query.FieldByName("JKFS").AsInteger;
                            query2.ParamByName("BGDDDM").AsString = sBGDDDM_BR;
                            query2.ParamByName("BGR").AsInteger = iLQR;
                            //query2.ParamByName("MDID").AsInteger = iMDID_ZB;
                            //query2.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.KCKStatus.LY;// query.FieldByName("STATUS").AsInteger;                       
                            switch (iDJLX)
                            {
                                case BASECRMDefine.HYKLYD_LYD:
                                case BASECRMDefine.HYKLYD_DBD: query2.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.KCKStatus.LY; break;
                                case BASECRMDefine.HYKLYD_TLD: query2.ParamByName("STATUS").AsInteger = (int)BASECRMDefine.KCKStatus.JK; break;
                            }
                            query2.ParamByName("YXQ").AsDateTime = query.FieldByName("YXQ").AsDateTime;
                            query2.ParamByName("XKRQ").AsDateTime = query.FieldByName("XKRQ").AsDateTime;
                            query2.ParamByName("FXDWID").AsInteger = query.FieldByName("FXDWID").AsInteger;
                            query2.ParamByName("YZM").AsString = query.FieldByName("YZM").AsString;
                            query2.ExecSQL();
                            query.Next();
                        }
                        query.Close();
                        query.SQL.Text = "delete from MZKCARD where CZKHM in (select I.HM from MZK_LQJLITEM I";
                        query.SQL.Add(" where I.JLBH=:JLBH) and BGDDDM=:BGDDDM_BC");
                        query.ParamByName("JLBH").AsInteger = iJLBH;
                        query.ParamByName("BGDDDM_BC").AsString = sBGDDDM_BC;
                        query.ExecSQL();
                        dbTrans2.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans2.Rollback();
                        if (e is MyDbException)
                            throw e;
                        else
                            msg = e.Message;
                        //CrmLibProc.WriteToLog(query.SqlText + "\r\n" + e.Message);
                        throw new MyDbException(e.Message, query2.SqlText);
                    }
                }
                finally
                {
                    conn2.Close();
                }
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM_BC", "W.BGDDDM_BC");
            CondDict.Add("sBGDDDM_BR", "W.BGDDDM_BR");
            CondDict.Add("sBGDDMC_BC", "BC.BGDDMC");
            CondDict.Add("sBGDDMC_BR", "BR.BGDDMC");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");


            query.SQL.Text = "select W.*,BC.BGDDMC BGDDMC_BC,BR.BGDDMC BGDDMC_BR";
            query.SQL.Add("     from MZK_LQJL W,HYK_BGDD BC,HYK_BGDD BR");
            query.SQL.Add("    where W.BGDDDM_BC=BC.BGDDDM");
            query.SQL.Add("      and W.BGDDDM_BR=BR.BGDDDM");
            if (sCZKHM != "")
            {
                query.SQL.Add(" and exists(select 1 from MZK_LQJLITEM I WHERE I.JLBH=W.JLBH AND I.HM='" + sCZKHM + "')");
                // query.ParamByName("HM").AsString = sCZKHM;
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,D.HYKNAME from MZK_LQJLKDITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYKTYPE=D.HYKTYPE");
                query.Open();
                HYKDEF hyk = new HYKDEF();
                while (!query.Eof)
                {
                    HYKGL_HYKLYKDItem item = new HYKGL_HYKLYKDItem();
                    ((MZKGL_MZKLQ_Proc)lst[0]).kditemTable.Add(item);
                    item.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString;
                    item.sCZKHM_END = query.FieldByName("CZKHM_END").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.iSKSL = query.FieldByName("SKSL").AsInteger;
                    item.fMZJE = query.FieldByName("MZJE").AsFloat;
                    query.Next();
                }
                query.Close();
                query.SQL.Text = "select I.*,D.HYKNAME from MZK_LQJLITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYKTYPE=D.HYKTYPE");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKLYItem item = new HYKGL_HYKLYItem();
                    ((MZKGL_MZKLQ_Proc)lst[0]).itemTable.Add(item);
                    item.sHM = query.FieldByName("HM").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.sLQRMC = query.FieldByName("LQRMC").AsString;
                    query.Next();
                }
                query.Close();

                //query.SQL.Text = "select I.*,D.HYKNAME from MZK_CARDLQSQJLITEM I,HYKDEF D,MZK_LQJL C where C.JLBH=" + iJLBH;
                //query.SQL.Add("and I.HYKTYPE=D.HYKTYPE and C.SQDJH = I.JLBH");
                //query.Open();
                //while (!query.Eof)
                //{
                //    HYKGL_LYSQItem item = new HYKGL_LYSQItem();
                //    ((MZKGL_MZKLQ_Proc)lst[0]).itemTable2.Add(item);
                //    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                //    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                //    item.iSKSL = query.FieldByName("HYKSL").AsInteger;
                //    query.Next();
                //}
            }



            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKLQ_Proc item = new MZKGL_MZKLQ_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            //item.sHYKKH1 = query.FieldByName("HYKKH1").AsString;
            //item.sHYKKH2 = query.FieldByName("HYKKH2").AsString;
            item.iHYKSL = query.FieldByName("HYKSL").AsInteger;
            //item.sZY = query.FieldByName("ZY").AsString;
            item.sBGDDDM_BC = query.FieldByName("BGDDDM_BC").AsString;
            item.sBGDDMC_BC = query.FieldByName("BGDDMC_BC").AsString;
            item.sBGDDDM_BR = query.FieldByName("BGDDDM_BR").AsString;
            item.sBGDDMC_BR = query.FieldByName("BGDDMC_BR").AsString;
            item.iLQR = query.FieldByName("LQR").AsInteger;
            item.sLQRMC = query.FieldByName("LQRMC").AsString;

            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iDJLX = query.FieldByName("DJLX").AsInteger;
            //item.iQRSHR = query.FieldByName("QRSHR").AsInteger;
            //item.sQRSHRMC = query.FieldByName("QRSHRMC").AsString;
            //item.dQRSHRQ = FormatUtils.DateToString(query.FieldByName("QRSHRQ").AsDateTime);
            item.iSQDJH = query.FieldByName("SQDJH").AsInteger;
            return item;
        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            ExecTable(query, "MZK_LQJL", serverTime, "JLBH", "QRSHR", "QRSHRMC", "QRSHRQ");
            msg = string.Empty;
        }
    }
}
