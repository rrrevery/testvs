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

using BF.CrmProc;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKPLCK : DJLR_ZX_CLass
    {
        public List<kdmx> SKKD = new List<kdmx>();
        public class kdmx
        {
            public string sCZKHM_BEGIN = "";
            public string sCZKHM_END = "";
            public double fMZJE = 0;
            public int iSKSL = 0;
            public int iRANDOM_LEN = 0;
        }
        public List<ZQITEM> ZQMX = new List<ZQITEM>();
        public class ZQITEM
        {
            public string sHYK_NO = "";
            public int iYHQLX = 0;
            public double fZKJE = 0;
            public int iYXQTS = 0;
            public string sYHQMC = string.Empty;
        }
        public List<HYIDITEM> HYIDList = new List<HYIDITEM>();
        public class HYIDITEM
        {
            public int iHYID = 0;
            public double fZJJE = 0;
            public double fYJE = 0;
            public string sHYK_NO = string.Empty;
        }
        public List<SKFS> ZFFS = new List<SKFS>();
        public List<ZFITEM> ZFMX = new List<ZFITEM>();
        public class ZFITEM
        {
            public string sHYK_NO = "";
            public double fZSJF = 0;
        }
        public class SKFS
        {
            public int iZFFSID = 0;
            public string sZFFSDM = string.Empty;
            public string sZFFSMC = string.Empty;
            public double fJE = 0;
            public int TYPE = 0;
        }
        public class CashCard
        {
            public int CardId = 0;
            public string CardCode = string.Empty;
            public double Balance = 0;
            public double PayMoney = 0;

        }
        public int iFS = 0;
        public int iSKSL = 0;
        public double fYSZE = 0;
        public double fZKL = 0;
        public double fZKJE = 0;
        public double fSSJE = 0;
        public double fZSJE = 0;
        public int iKHID = 0;
        public string sLXR = string.Empty;
        public double fSJZSJE = 0, fYZSJF = 0, fSJZSJF = 0;
        public int iYWY = 0;
        public int iMDID = 0;
        public int iMDID_CZ = 0;
        public int iKDSL = 0;
        public int iKHCD = 0;
        public string sKHMC = string.Empty;
        public string sLXRXM = string.Empty;
        public string sLXRSJ = string.Empty;
        public string sMDMC = string.Empty;
        public string sMDDM = string.Empty;

        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public DateTime dYXQ = DateTime.MinValue;
        public Decimal ZKL = 0;
        public Decimal ZKJE = 0;
        public Decimal ZKLPBL = 0;
        public Decimal ZKLPJE = 0;
        public int ZKYHQLX = 0;
        public string ZKYHQMC = string.Empty;
        public int ZKYHQTS = 0;
        public string sCZDD = string.Empty;

        //public override bool IsValidData(out string msg)
        //{
        //    msg = string.Empty;
        //    //if (Get_YSZE() != Get_SSJEALL())
        //    //{
        //    //    msg = "应收金额与实收金额不等！";
        //    //    return false;
        //    //}
        //    //if (Get_SSJEALL() == -1)
        //    //{
        //    //    msg = "实收金额校验错误请输入合法数据！";
        //    //    return false;
        //    //}
        //    //if (Get_CKSL() <= 0)
        //    //{
        //    //    msg = "存款数量不能小于等于0！";
        //    //    return false;
        //    //}
        //    return true;
        //}


        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKPLCK bill = new MZKGL_MZKPLCK();
            bill.iJLBH = query.FieldByName("JLBH").AsInteger;
            bill.iSKSL = query.FieldByName("CKSL").AsInteger;
            bill.fYSZE = query.FieldByName("CKJE").AsFloat;
            bill.fZSJE = query.FieldByName("YXZQJE").AsFloat;
            bill.sZY = query.FieldByName("ZY").AsString;
            bill.iDJR = query.FieldByName("DJR").AsInteger;
            bill.sDJRMC = query.FieldByName("DJRMC").AsString;
            bill.dDJSJ = query.FieldByName("DJSJ").AsDateTime.ToString();
            bill.iSTATUS = query.FieldByName("STATUS").AsInteger;
            bill.iKHID = query.FieldByName("KHID").AsInteger;
            bill.fSJZSJE = query.FieldByName("SJZQJE").AsFloat;
            bill.iZXR = query.FieldByName("ZXR").AsInteger;
            bill.sZXRMC = query.FieldByName("ZXRMC").AsString;
            bill.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            bill.sKHMC = query.FieldByName("KHMC").AsString;
            bill.sLXRXM = query.FieldByName("LXRXM").AsString;
            bill.sLXRSJ = query.FieldByName("LXRSJ").AsString;
            //bill.sMDMC = query.FieldByName("MDMC").AsString;
            //bill.iMDID_CZ = query.FieldByName("MDID_CZ").AsInteger; ;
            bill.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            bill.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            bill.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            bill.sCZDD= query.FieldByName("CZDD").AsString ;
            return bill;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iMDID_CZ", "W.MDID_CZ");
            CondDict.Add("iSKSL", "W.CKSL");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iKHID", "W.KHID");
            CondDict.Add("sKHMC", "A.KHMC");
            CondDict.Add("sLXRXM", "A.LXRXM");
            CondDict.Add("sLXRSJ", "A.LXRSJ");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.*,M.BGDDMC,W.KHID,A.KHMC,A.LXRXM,A.LXRSJ,F.HYKNAME";
            query.SQL.Add(" from MZK_PLCK W,HYK_BGDD M,MZK_KHDA A,HYKDEF F");
            query.SQL.Add(" where W.CZDD=M.BGDDDM(+) and W.HYKTYPE=F.HYKTYPE and W.KHID=A.KHID(+)");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*";
                query.SQL.Add(" from MZK_PLCKKDITEM I");
                query.SQL.Add(" where I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    kdmx item = new kdmx();
                    ((MZKGL_MZKPLCK)lst[0]).SKKD.Add(item);

                    item.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString.Substring(0, query.FieldByName("CZKHM_BEGIN").AsString.Length);
                    item.sCZKHM_END = query.FieldByName("CZKHM_END").AsString.Substring(0, query.FieldByName("CZKHM_END").AsString.Length);
                    item.iSKSL = query.FieldByName("CKSL").AsInteger;
                    item.fMZJE = query.FieldByName("CKJE").AsFloat;
                    query.Next();
                }

                query.SQL.Text = "select I.*,S.ZFFSMC,S.ZFFSDM ";
                query.SQL.Add(" from MZK_PLCKSKJL I,ZFFS S");
                query.SQL.Add(" where I.ZFFSID=S.ZFFSID and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    SKFS item = new SKFS();
                    ((MZKGL_MZKPLCK)lst[0]).ZFFS.Add(item);
                    item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                    item.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    query.Next();
                }
                //query.SQL.Text = "select I.*,S.YHQMC";
                //query.SQL.Add(" from HYK_CZK_PLCKZKITEM I,YHQDEF S");
                //query.SQL.Add(" where I.YHQLX=S.YHQID and I.JLBH=" + iJLBH);
                //query.Open();
                //while (!query.Eof)
                //{
                //    ZQITEM item = new ZQITEM();
                //    ((MZKGL_MZKPLCK)lst[0]).ZQMX.Add(item);
                //    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                //    item.iYHQLX = query.FieldByName("YHQLX").AsInteger;
                //    item.iYXQTS = query.FieldByName("YXQTS").AsInteger;
                //    item.fZKJE = query.FieldByName("ZKJE").AsFloat;
                //    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                //    query.Next();
                //}

                //query.SQL.Text = "select I.*";
                //query.SQL.Add(" from HYK_CZK_ZKITEM I");
                //query.SQL.Add(" where I.JLBH=" + iJLBH);
                //query.Open();
                //while (!query.Eof)
                //{
                //    ZFITEM item = new ZFITEM();
                //    ((MZKGL_MZKPLCK)lst[0]).ZFMX.Add(item);
                //    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                //    item.fZSJF = query.FieldByName("ZSJF").AsFloat;
                //    query.Next();
                //}
                ////Get_FSZJF(query, ((MZKGL_MZKPLCK)lst[0]));
                //Get_FSZQ(query, ((MZKGL_MZKPLCK)lst[0]));
            }
            return lst;
        }



        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_PLCK;MZK_PLCKSKJL;MZK_PLCKKDITEM;MZK_PLCKZPMX;MZK_PLCKZKITEM", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
            query.SQL.Text = "delete from MZK_PLCKITEM where CZJPJ_jlbh=:JLBH";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string sKH = string.Empty;
            StringBuilder sql = new StringBuilder();
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_PLCK");
            query.SQL.Text = "insert into MZK_PLCK(JLBH,HYKTYPE,KHID,CKJE,YXZQJE,SJZQJE,CKSL,DJR,DJRMC,DJSJ,STATUS,CZDD,MDID_CZ,ZY)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:KHID,:CKJE,:YXZQJE,:SJZQJE,:CKSL,:DJR,:DJRMC,:DJSJ,0,:CZDD,:MDID_CZ,:ZY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("KHID").AsInteger = iKHID;
            query.ParamByName("CKJE").AsFloat = fYSZE;
            query.ParamByName("YXZQJE").AsFloat = fZSJE;
            query.ParamByName("SJZQJE").AsFloat = fSJZSJE;
            query.ParamByName("CKSL").AsInteger = iSKSL;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sCZDD;
            query.ParamByName("MDID_CZ").AsInteger = iMDID_CZ;
            query.ParamByName("ZY").AsString = sZY;
           

            query.ExecSQL();

            foreach (kdmx one in SKKD)
            {
                query.SQL.Text = "insert into MZK_PLCKKDITEM(JLBH,CZKHM_BEGIN,CZKHM_END,CKSL,CKJE)";
                query.SQL.Add(" values(:JLBH,:CZKHM_BEGIN,:CZKHM_END,:CKSL,:CKJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.ParamByName("CKSL").AsInteger = one.iSKSL;
                query.ParamByName("CKJE").AsFloat = one.fMZJE;
                query.ExecSQL();
            }
            List<CashCard> saleCardList = new List<CashCard>();
            #region
            foreach (kdmx one in SKKD)
            {
                query.SQL.Text = "select A.HYID,A.HYK_NO,nvl(H.YE,0) YE from MZKXX A,HYKDEF F,MZK_JEZH H ";
                query.SQL.Add("where A.HYKTYPE=F.HYKTYPE and A.HYID=H.HYID(+) and A.HYK_NO>=:CZKHM_BEGIN and A.HYK_NO<=:CZKHM_END");
                query.SQL.Add("and length(A.HYK_NO) = length( :CZKHM_BEGIN )");
                query.SQL.Add("and F.BJ_CZK=1");
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.Open();
                while (!query.Eof)
                {
                    CashCard card = new CashCard();
                    card.CardId = query.FieldByName("HYID").AsInteger;
                    card.CardCode = query.FieldByName("HYK_NO").AsString;
                    card.Balance = query.FieldByName("YE").AsFloat;
                    card.PayMoney = one.fMZJE;
                    saleCardList.Add(card);
                    query.Next();
                }
                query.Close();
            }

            foreach (CashCard one in saleCardList)
            {
                query.SQL.Text = "insert into MZK_PLCKITEM(CZJPJ_JLBH,HYID,YJE,CKJE,CZKHM)";
                query.SQL.Add(" values(:CZJPJ_JLBH,:HYID,:YJE,:CKJE,:CZKHM)");
                query.ParamByName("CZJPJ_JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.CardId;
                query.ParamByName("YJE").AsFloat = one.Balance;
                query.ParamByName("CKJE").AsFloat = one.PayMoney;
                query.ParamByName("CZKHM").AsString = one.CardCode;
                query.ExecSQL();
            }

            #endregion
            //支付方式
            foreach (SKFS one in ZFFS)
            {
                query.SQL.Text = "insert into MZK_PLCKSKJL(JLBH,ZFFSID,JE)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();
            }
            //赠券
            //foreach (ZQITEM one in ZQMX)
            //{
            //    query.SQL.Text = "insert into HYK_CZK_PLCKZKITEM(JLBH,HYK_NO,YHQLX,ZKJE,YXQTS)";
            //    query.SQL.Add(" values(:JLBH,:HYK_NO,:YHQLX,:ZKJE,:YXQTS)");
            //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
            //    query.ParamByName("YHQLX").AsInteger = one.iYHQLX;
            //    query.ParamByName("ZKJE").AsFloat = one.fZKJE;
            //    query.ParamByName("YXQTS").AsInteger = one.iYXQTS;
            //    query.ExecSQL();
            //}
            //foreach (ZFITEM one in ZFMX)
            //{
            //    query.SQL.Text = "insert into MZK_PLCKZJFITEM(JLBH,HYK_NO,ZSJF)";
            //    query.SQL.Add(" values(:JLBH,:HYK_NO,:ZSJF)");
            //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
            //    query.ParamByName("ZSJF").AsFloat = one.fZSJF;
            //    query.ExecSQL();
            //}

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            double balance;
            int sMDID = 0;
            List<HYIDITEM> HYIDList = new List<HYIDITEM>();
            #region
            query.SQL.Text = "select M.HYID,M.CKJE,M.YJE,M.CZKHM from MZK_PLCKITEM M ";
            query.SQL.Add(" where M.CZJPJ_JLBH=:JLBH order by M.HYID");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.Open();
            while (!query.Eof)
            {
                HYIDITEM item = new HYIDITEM();
                item.iHYID = query.FieldByName("HYID").AsInteger;
                item.fZJJE = query.FieldByName("CKJE").AsFloat;
                item.fYJE = query.FieldByName("YJE").AsFloat;
                item.sHYK_NO = query.FieldByName("CZKHM").AsString;
                HYIDList.Add(item);
                query.Next();
            }

            foreach (HYIDITEM one in HYIDList)
            {
                sMDID = BF.CrmProc.CrmLibProc.GetMDByHYID(one.iHYID, "CRMDBMZK");
                int pJYBH = SeqGenerator.GetSeq("MZK_JEZCLJL");
                query.SQL.Text = "begin";
                query.SQL.Add("update MZK_JEZH set YE=round(:pYE,2)");
                query.SQL.Add("where HYID=:pHYID1;");
                query.SQL.Add("if SQL%NOTFOUND then");
                query.SQL.Add("insert into MZK_JEZH(HYID,QCYE,YE,PDJE,JYDJJE)");
                query.SQL.Add("values(:pHYID1,0,:pYE,0,0);");
                query.SQL.Add("end if;");
                query.SQL.Add("insert into MZK_JEZCLJL(JYBH,HYID,MDID,CLSJ,CLLX,JLBH,ZY,JFJE,DFJE,YE)");
                query.SQL.Add("values(:JYBH,:pHYID1,:pMDID,:CLSJ,:pCLLX,:pJLBH,'批量存款',round(:pJFJE,2),0,round(:pYE,2));");
                query.SQL.Add("end;");
                query.ParamByName("pHYID1").AsInteger = one.iHYID;
                query.ParamByName("pYE").AsFloat = one.fYJE + one.fZJJE;
                query.ParamByName("pMDID").AsInteger = sMDID;
                query.ParamByName("CLSJ").AsDateTime = serverTime;
                query.ParamByName("pCLLX").AsInteger = 1;
                query.ParamByName("pJLBH").AsInteger = iJLBH;
                query.ParamByName("pJFJE").AsFloat = one.fZJJE;
                query.ParamByName("JYBH").AsInteger = pJYBH;
                query.ExecSQL();
                CrmLibProc.EncryptBalanceOfCashCard_MZK(query, one.iHYID, serverTime, out balance);
            }
            #endregion

            query.SQL.Text = "update MZK_PLCK set ZXR=:pZXR,ZXRQ = :ZXRQ,ZXRMC=:pZXRMC,STATUS = 2";
            query.SQL.Add("where JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("pZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRQ").AsDateTime = serverTime;
            query.ParamByName("pZXRMC").AsString = sLoginRYMC;
            query.ExecSQL();
            // DoExecZS(out msg, serverTime);
        }

        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //  ExecTable(query, "WX_MENU", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
        }

        //public override bool IsValidExecData(out string msg)
        //{
        //    msg = string.Empty;
        //    foreach (SKFS one in ZFFS)
        //    {
        //        if (one.JE <= 0)
        //        {
        //            msg = "收款方式" + one.NAME + "的金额小于0，不合法";
        //            return false;
        //        }
        //    }
        //    if (Get_YSZE() != Get_SSJEALL())
        //    {
        //        msg = "应收金额与实收金额不等！";
        //        return false;
        //    }
        //    if (Get_CKSL() <= 0)
        //    {
        //        msg = "存款卡数量不能小于等于0！";
        //        return false;
        //    }
        //    return true;
        //}
        private double Get_YSZE()
        {
            try
            {
                double tp_yzje = 0;
                foreach (kdmx one in SKKD)
                {
                    tp_yzje += one.fMZJE * one.iSKSL;
                }
                return tp_yzje;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        private double Get_SSJEALL()
        {
            try
            {
                double tp_ssje = 0;
                foreach (SKFS one in ZFFS)
                {
                    tp_ssje += one.fJE;
                }
                return tp_ssje;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        private Int32 Get_CKSL()
        {
            try
            {
                Int32 tp_SKSL = 0;
                foreach (kdmx one in SKKD)
                {
                    //  tp_SKSL += one.iCKSL;
                    tp_SKSL += one.iSKSL;
                }
                return tp_SKSL;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void DoExecZS(out string msg, DateTime serverTime)
        {
            int aBJ = 0;
            int aHYID = 0;
            string aCDNR = string.Empty;
            string aHYK_NO = string.Empty;
            int aHYKTYPE = 0;
            string aBGDDDM = string.Empty;
            int aFSJLBH = 0;
            int mMDID = 0;
            msg = string.Empty;

            aFSJLBH = iJLBH;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);

                try
                {
                    //处理赠送
                    foreach (ZQITEM one in ZQMX)
                    {
                        query.SQL.Text = "select 0 as BJ,A.CZKHM AS HYKNO,A.CDNR CDNR,-1 as HYID,A.HYKTYPE,A.BGDDDM BGDDDM from HYKCARD A,HYKDEF D";
                        query.SQL.Add("where A.HYKTYPE=D.HYKTYPE AND D.BJ_CZK=0 AND A.STATUS=1 AND A.CZKHM=:CZKHM");
                        query.SQL.Add("union");
                        query.SQL.Add("select 1 as BJ,X.HYK_NO AS HYKNO,X.CDNR CDNR,X.HYID,X.HYKTYPE,X.YBGDD AS BGDDDM from HYK_HYXX X,HYKDEF D");
                        query.SQL.Add("where X.HYKTYPE=D.HYKTYPE AND D.BJ_CZK=0 AND X.HYK_NO=:CZKHM");
                        query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            aBJ = query.FieldByName("BJ").AsInteger;
                            aHYK_NO = query.FieldByName("HYKNO").AsString;
                            aCDNR = query.FieldByName("CDNR").AsString;
                            aHYID = query.FieldByName("HYID").AsInteger;
                            aHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                            aBGDDDM = query.FieldByName("BGDDDM").AsString;
                            if (aBJ == 0) //未售卡
                            {
                                aHYID = SeqGenerator.GetSeq("HYK_HYXX");
                                query.SQL.Text = "insert into HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,DJR,DJRMC,DJSJ,STATUS,MDID)";
                                query.SQL.Add("values(:pHYID,:pHYKTYPE,:pHYK_NO,:pCDNR,:pJKRQ,:pYXQ,:pDJR,:pDJRMC,:pDJSJ,0,:MDID)");
                                query.ParamByName("pHYID").AsInteger = aHYID;
                                query.ParamByName("pHYKTYPE").AsInteger = aHYKTYPE;
                                query.ParamByName("pHYK_NO").AsString = aHYK_NO;
                                query.ParamByName("pCDNR").AsString = aCDNR;
                                query.ParamByName("pJKRQ").AsDateTime = serverTime;
                                query.ParamByName("pYXQ").AsDateTime = serverTime.AddDays(one.iYXQTS);
                                query.ParamByName("pDJR").AsInteger = iLoginRYID;
                                query.ParamByName("pDJRMC").AsString = sLoginRYMC;
                                query.ParamByName("pDJSJ").AsDateTime = serverTime;
                                query.ParamByName("MDID").AsInteger = Convert.ToInt32(BF.CrmProc.CrmLibProc.GetMDByRYID(iLoginRYID));
                                query.ExecSQL();

                                //发售记录
                                aFSJLBH = SeqGenerator.GetSeq("HYK_CZKSKJL");
                                query.SQL.Text = "begin";
                                query.SQL.Add("insert into HYK_CZKSKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,ZY,TK_FLAG,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,STATUS,QDSJ)");
                                query.SQL.Add(" values(:JLBH,0,1,0,1,0,0,:YXQ,:BGDDDM,:ZY,0,:DJR,:DJRMC,:DJSJ,:ZXR,:ZXRMC,:ZXRQ,2,:QDSJ);");
                                query.SQL.Add("insert into HYK_CZKSKJLITEM(JLBH,HYKTYPE,CZKHM,QCYE,YXTZJE,PDJE)");
                                query.SQL.Add("values(:JLBH,:HYKTYPE,:CZKHM,0,0,0);");
                                query.SQL.Add("end;");
                                query.ParamByName("JLBH").AsInteger = aFSJLBH;
                                query.ParamByName("YXQ").AsDateTime = serverTime.AddDays(one.iYXQTS);
                                query.ParamByName("BGDDDM").AsString = aBGDDDM;
                                query.ParamByName("ZY").AsString = "单张卡发放";
                                query.ParamByName("DJR").AsInteger = iLoginRYID;
                                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                                query.ParamByName("DJSJ").AsDateTime = serverTime;
                                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                                query.ParamByName("ZXRQ").AsDateTime = serverTime;
                                query.ParamByName("QDSJ").AsDateTime = serverTime;
                                query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                                query.ParamByName("HYKTYPE").AsInteger = aHYKTYPE;
                                query.ExecSQL();

                                query.SQL.Text = "update HYKCARD set SKJLBH=:JLBH where CZKHM=:CZKHM";
                                query.ParamByName("JLBH").AsInteger = aFSJLBH;
                                query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                                query.ExecSQL();

                                query.SQL.Text = "delete from HYKCARD where CZKHM=:CZKHM";
                                query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                                query.ExecSQL();
                            }
                            //券
                            query.SQL.Text = "select MDID from HYK_HYXX where HYK_NO=:HYK_NO";
                            query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                            query.Open();
                            mMDID = query.FieldByName("MDID").AsInteger;

                            BF.CrmProc.CrmLibProc.UpdateYHQZH(out msg, query, aHYID, BASECRMDefine.CZK_CLLX_CK, aFSJLBH, one.iYHQLX, mMDID, 0, one.fZKJE, FormatUtils.DateToString(serverTime.AddDays(one.iYXQTS)), "储值卡存款赠券");

                        }
                    }
                    //处理赠送
                    //foreach (ZFITEM one in ZFMX)
                    //{
                    //    query.SQL.Text = "select 0 as BJ,A.CZKHM AS HYKNO,A.CDNR CDNR,-1 as HYID,A.HYKTYPE,A.BGDDDM BGDDDM from HYKCARD A,HYKDEF D";
                    //    query.SQL.Add("where A.HYKTYPE=D.HYKTYPE AND D.BJ_CZK=0 AND A.STATUS=1 AND A.CZKHM=:CZKHM");
                    //    query.SQL.Add("union");
                    //    query.SQL.Add("select 1 as BJ,X.HYK_NO AS HYKNO,X.CDNR CDNR,X.HYID,X.HYKTYPE,X.YBGDD AS BGDDDM from HYK_HYXX X,HYKDEF D");
                    //    query.SQL.Add("where X.HYKTYPE=D.HYKTYPE AND D.BJ_CZK=0 AND X.HYK_NO=:CZKHM");
                    //    query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    //    query.Open();
                    //    if (!query.IsEmpty)
                    //    {
                    //        aBJ = query.FieldByName("BJ").AsInteger;
                    //        aHYK_NO = query.FieldByName("HYKNO").AsString;
                    //        aCDNR = query.FieldByName("CDNR").AsString;
                    //        aHYID = query.FieldByName("HYID").AsInteger;
                    //        aHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    //        aBGDDDM = query.FieldByName("BGDDDM").AsString;
                    //        if (aBJ == 0) //未售卡
                    //        {
                    //            aHYID = SeqGenerator.GetSeq("HYK_HYXX");
                    //            query.SQL.Text = "insert into HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,DJR,DJRMC,DJSJ,STATUS,MDID)";
                    //            query.SQL.Add("values(:pHYID,:pHYKTYPE,:pHYK_NO,:pCDNR,:pJKRQ,:pYXQ,:pDJR,:pDJRMC,:pDJSJ,0,:MDID)");
                    //            query.ParamByName("pHYID").AsInteger = aHYID;
                    //            query.ParamByName("pHYKTYPE").AsInteger = aHYKTYPE;
                    //            query.ParamByName("pHYK_NO").AsString = aHYK_NO;
                    //            query.ParamByName("pCDNR").AsString = aCDNR;
                    //            query.ParamByName("pJKRQ").AsDateTime = serverTime;
                    //            query.ParamByName("pYXQ").AsDateTime = dYXQ;
                    //            query.ParamByName("pDJR").AsInteger = iLoginRYID;
                    //            query.ParamByName("pDJRMC").AsString = sLoginRYMC;
                    //            query.ParamByName("pDJSJ").AsDateTime = serverTime;
                    //            query.ParamByName("MDID").AsInteger = Convert.ToInt32(BF.CrmProc.CrmLibProc.GetMDByRYID(iLoginRYID));
                    //            query.ExecSQL();

                    //            //发售记录
                    //            aFSJLBH = SeqGenerator.GetSeq("HYK_CZKSKJL");
                    //            query.SQL.Text = "begin";
                    //            query.SQL.Add("insert into HYK_CZKSKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,ZY,TK_FLAG,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,STATUS,QDSJ)");
                    //            query.SQL.Add(" values(:JLBH,0,1,0,1,0,0,:YXQ,:BGDDDM,:ZY,0,:DJR,:DJRMC,:DJSJ,:ZXR,:ZXRMC,:ZXRQ,2,:QDSJ);");
                    //            query.SQL.Add("insert into HYK_CZKSKJLITEM(JLBH,HYKTYPE,CZKHM,QCYE,YXTZJE,PDJE)");
                    //            query.SQL.Add("values(:JLBH,:HYKTYPE,:CZKHM,0,0,0);");
                    //            query.SQL.Add("end;");
                    //            query.ParamByName("JLBH").AsInteger = aFSJLBH;
                    //            query.ParamByName("YXQ").AsDateTime = dYXQ;
                    //            query.ParamByName("BGDDDM").AsString = aBGDDDM;
                    //            query.ParamByName("ZY").AsString = "单张卡发放";
                    //            query.ParamByName("DJR").AsInteger = iLoginRYID;
                    //            query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    //            query.ParamByName("DJSJ").AsDateTime = serverTime;
                    //            query.ParamByName("ZXR").AsInteger = iLoginRYID;
                    //            query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                    //            query.ParamByName("ZXRQ").AsDateTime = serverTime;
                    //            query.ParamByName("QDSJ").AsDateTime = serverTime;
                    //            query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    //            query.ParamByName("HYKTYPE").AsInteger = aHYKTYPE;
                    //            query.ExecSQL();

                    //            query.SQL.Text = "update HYKCARD set SKJLBH=:JLBH where CZKHM=:CZKHM";
                    //            query.ParamByName("JLBH").AsInteger = aFSJLBH;
                    //            query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    //            query.ExecSQL();

                    //            query.SQL.Text = "delete from HYKCARD where CZKHM=:CZKHM";
                    //            query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                    //            query.ExecSQL();
                    //        }
                    //        //积分
                    //       // mMDID = BF.CrmProc.CrmLibProc.BGDDToMDID(query,sBGDDDM);
                    //        mMDID = iMDID_CZ;
                    //        BF.CrmProc.CrmLibProc.UpdateJFZH(out msg, query, 0, aHYID, BASECRMDefine.HYK_JFBDCLLX_CZKFKSF, aFSJLBH, mMDID, one.fZSJF, iLoginRYID, sLoginRYMC, "储值卡发售赠积分");

                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    throw;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public void Get_FSZQ(CyQuery query, MZKGL_MZKPLCK skxx)
        {
            #region "GetFSZQGZ"
            string sql = "";
            sql += " select G.XSJE_BEGIN ,G.XSJE_END,G.ZSBL,G.ZSBL*:pXSJE Y_ZSBL,G.YHQTYPE,D.YHQMC,G.YXQTS";
            sql += " from HYK_CZK_ZKGZ G,YHQDEF D where G.YHQTYPE=D.YHQID and G.XSJE_BEGIN <=:pXSJE and G.XSJE_END>:pXSJE";
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql;
            query.ParamByName("pXSJE").AsFloat = skxx.fYSZE;
            query.Open();
            if (!query.IsEmpty)
            {
                skxx.ZKL = query.FieldByName("ZSBL").AsDecimal;
                skxx.ZKJE = query.FieldByName("Y_ZSBL").AsDecimal;
                skxx.ZKYHQLX = query.FieldByName("YHQTYPE").AsInteger;
                skxx.ZKYHQMC = query.FieldByName("YHQMC").AsString;
                skxx.ZKYHQTS = query.FieldByName("YXQTS").AsInteger;
            }
            else
            {
                skxx.ZKL = -1;
                skxx.ZKJE = 0;
                skxx.ZKYHQLX = -1;
                skxx.ZKYHQMC = string.Empty;
                skxx.ZKYHQTS = 0;
            }
            #endregion

        }

        public void Get_FSZJF(CyQuery query, MZKGL_MZKPLCK skxx)
        {
            #region "GetFSZJFGZ"
            string sql = "";
            sql += " select CKJE_BEGIN ,CKJE_END,ZSBL,ZSBL*:pXSJE Y_ZSBL,ZSJF";
            sql += " from MZK_CKSJFGZ where CKJE_BEGIN <=:pXSJE and CKJE_END>:pXSJE";
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql;
            query.ParamByName("pXSJE").AsFloat = skxx.fYSZE;
            query.Open();
            if (!query.IsEmpty)
            {
                if (query.FieldByName("ZSBL").AsDecimal > 0)
                {
                    skxx.ZKLPBL = query.FieldByName("ZSBL").AsDecimal;
                    skxx.ZKLPJE = query.FieldByName("Y_ZSBL").AsDecimal;
                }
                else
                {
                    skxx.ZKLPBL = query.FieldByName("ZSBL").AsDecimal;
                    skxx.ZKLPJE = query.FieldByName("ZSJF").AsDecimal;
                }
            }
            else
            {
                skxx.ZKLPBL = -1;
                skxx.ZKLPJE = 0;
            }
            #endregion

        }

    }
}
