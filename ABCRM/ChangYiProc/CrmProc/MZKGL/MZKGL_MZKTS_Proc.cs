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
    public class MZKGL_MZKTS : DJLR_ZX_CLass
    {
        public List<MZKGL_MZKTSKMXITEM> SKMXITEM = new List<MZKGL_MZKTSKMXITEM>();
        public class MZKGL_MZKTSKMXITEM : BASECRMClass
        {
            public string sCZKHM = string.Empty;
            public string sCZKHMEND = string.Empty;
            public double fQCYE = 0;
            public double fPDJE = 0;
            public int iHYID = 0;
            public string dYXQ = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public int iSKDJLBH = 0;

        }
        public List<SKFS> ZFFS = new List<SKFS>();
        public class SKFS
        {
            public string sZFFSMC;
            public int iZFFSID = 0;
            public double fJE = 0;
            public string dFKRQ = string.Empty;
            public string sJYBH = string.Empty;
            public int TYPE = 0;
            public string DM = string.Empty;
        }
        public List<ZQITEM> ZQMX = new List<ZQITEM>();
        public class ZQITEM
        {
            public string sHYK_NO = "";
            public int iYHQLX = 0;
            public double fZKJE = 0;
            public int iYXQTS = 0;
            public string sYHQMC = "";
        }
        public List<ZFITEM> ZFMX = new List<ZFITEM>();
        public class ZFITEM
        {
            public string sHYK_NO = "";
            public double fZSJF = 0;
        }
        public int FS = 0;
        public int SKSL = 0;
        public double YSZE = 0;
        public double ZKL = 0;
        public double ZKJE = 0;
        public double SSJE = 0;
        public DateTime YXQ = DateTime.MinValue;
        public string BGDDDM = string.Empty;
        public string ZY = string.Empty;
        public int TK_FLAG = 0;
        public int ZXR = 0;
        public string ZXRMC = string.Empty;
        public string ZXRQ = string.Empty;
        public int STATUS = 0;
        public string QDSJ = string.Empty;
        public double DZKFJE = 0;
        public double KFJE = 0;
        public double ZSJE = 0;
        public int KHID = 0;
        public string LXR = string.Empty;
        public double SJZSJE = 0;
        public int TKJLBH = 0;
        public int YWY = 0;
        public int XGR = 0;
        public string XGRMC = string.Empty;
        public string XGSJ = string.Empty;
        public int BJ_RJSH = 0;
        public double ZSJF = 0;
        public double SJZSJF = 0;
        public int MDID = 0;
        public int MDID_CZ = 0;
        public int KDSL = 0;
        public int KHCD = 0;
        public string KHMC = string.Empty;
        public string LXRXM = string.Empty;
        public string LXRSJ = string.Empty;
        public string BGDDMC = string.Empty;
        public string MDMC = string.Empty;
        public int iTKJLBH = 0;
        public string ZKYHQMC = string.Empty;
        public int ZKYHQLX = 0, ZKYHQTS = 0;

        public static string GetMZKSKDMXList(out string msg, MZKGL_MZKTSKMXITEM obj)
        {

            msg = string.Empty;
            List<Object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "    select I.JLBH,I.PDJE, H.HYID,H.HYKTYPE,H.HYK_NO,F.HYKNAME,I.QCYE,H.YXQ from MZK_SKJLITEM I, MZKXX H,HYKDEF F  ";
                    query.SQL.Add("  where H.HYID=I.HYID and F.HYKTYPE=I.HYKTYPE");
                    if (obj.iSKDJLBH != 0)
                    {
                        query.SQL.Add("   and I.JLBH=" + obj.iSKDJLBH);
                    }
                    if (obj.sCZKHM != "")
                    {
                        query.SQL.Add("  and  H.HYK_NO>=" + obj.sCZKHM + " ");
                    }

                    if (obj.sCZKHMEND != "")
                    {
                        query.SQL.Add("  and  H.HYK_NO<=" + obj.sCZKHMEND + " ");
                    }
                    if (obj.iHYKTYPE != 0)
                    {
                        query.SQL.Add("   and H.HYKTYPE=" + obj.iHYKTYPE);
                    }
                    query.SQL.Add("ORDER BY I.JLBH");

                    //if (obj.dYXQ != "")
                    //{
                    //    query.SQL.Add(" and H.YXQ='" + obj.dYXQ + "'");
                    //}
                    query.Open();
                    while (!query.Eof)
                    {
                        MZKGL_MZKTSKMXITEM item = new MZKGL_MZKTSKMXITEM();
                        item.iSKDJLBH = query.FieldByName("JLBH").AsInteger;
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.fPDJE = query.FieldByName("PDJE").AsFloat;
                        item.sCZKHM = query.FieldByName("HYK_NO").AsString;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.fQCYE = query.FieldByName("QCYE").AsFloat;
                        item.dYXQ = FormatUtils.DatetimeToString(query.FieldByName("YXQ").AsDateTime);
                        lst.Add(item);
                        query.Next();
                    }
                    query.Close();
                }

                catch (Exception e)
                {
                    if (e is MyDbException)
                    {
                        throw e;
                    }
                    else
                    {
                        msg = e.Message;
                        throw new MyDbException(e.Message, query.SqlText);
                    }
                }
            }

            finally
            {
                conn.Close();
            }

            return obj.GetTableJson(lst);
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (Get_YSZE() != Get_SSJEALL())
            {
                msg = "退款方式金额不等实退金额！";
                return false;
            }
            if (Get_SSJEALL() == -1)
            {
                msg = "退款方式金额校验错误请输入合法数据！";
                return false;
            }
            if (Get_TKSL() <= 0)
            {
                msg = "没有退卡数量";
                return false;
            }
            if ((ZKJE > 0) && (SJZSJE > 0))
            {
                if (ZKJE < GetZQJE())
                {
                    msg = "退券金额与应退券金额不等！";
                    return false;
                }
            }
            if ((ZSJF > 0) && (SJZSJF > 0))
            {
                if (ZSJF < GetZSJF())
                {
                    msg = "退积分与应退积分不等！";
                    return false;
                }
            }
            foreach (ZQITEM one in ZQMX)
            {
                query.SQL.Text = "SELECT Y.HYID,YHQID,SUM(JE) JE FROM HYK_YHQZH Y,HYK_HYXX X ";
                query.SQL.Add("WHERE Y.HYID=X.HYID AND Y.JSRQ>sysdate and X.HYK_NO=:HYK_NO ");
                query.SQL.Add("and Y.YHQID=:YHQID");
                query.SQL.Add("GROUP BY Y.HYID,Y.YHQID");
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("YHQID").AsInteger = one.iYHQLX;
                query.Open();
                if (query.FieldByName("JE").AsFloat < one.fZKJE)
                {
                    msg = "会员卡" + one.sHYK_NO + "中没有足够的优惠券!";
                    return false;
                }
            }
            foreach (ZFITEM one in ZFMX)
            {
                query.SQL.Text = "SELECT X.HYID,SUM(WCLJF) WCLJF FROM HYK_JFZH H,HYK_HYXX X ";
                query.SQL.Add("WHERE H.HYID=X.HYID AND X.HYK_NO=:HYK_NO ");
                query.SQL.Add("GROUP BY X.HYID");
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.Open();
                if (query.FieldByName("WCLJF").AsFloat < one.fZSJF)
                {
                    msg = "会员卡" + one.sHYK_NO + "中没有足够的积分!";
                    return false;
                }
            }
            return true;
        }

        private double Get_YSZE()
        {
            try
            {
                double tp_yzje = 0;
                foreach (MZKGL_MZKTSKMXITEM one in SKMXITEM)
                {
                    tp_yzje += one.fQCYE;
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
        private Int32 Get_TKSL()
        {
            try
            {
                return SKMXITEM.Count;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private double GetZQJE()
        {
            try
            {
                double tp_zqje = 0;
                foreach (ZQITEM one in ZQMX)
                {
                    tp_zqje = tp_zqje + one.fZKJE;
                }
                return tp_zqje;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private double GetZSJF()
        {
            try
            {
                double tp_zsjf = 0;
                foreach (ZFITEM one in ZFMX)
                {
                    tp_zsjf = tp_zsjf + one.fZSJF;
                }
                return tp_zsjf;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void DoExecZS(out string msg, DateTime serverTime, DateTime MyDate)
        {
            msg = string.Empty;
            int aHYID = 0;
            string aCDNR = string.Empty;
            string aHYK_NO = string.Empty;
            int aHYKTYPE = 0;
            string aBGDDDM = string.Empty;
            int mMDID = 0;
            double cZQJE = 0;

            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);

                try
                {
                    //处理赠送
                    foreach (ZQITEM one in ZQMX)
                    {
                        query.SQL.Text = "select X.HYK_NO,X.CDNR,X.HYID,X.HYKTYPE from HYK_HYXX X,HYKDEF D";
                        query.SQL.Add("where X.HYKTYPE=D.HYKTYPE AND D.BJ_CZK=0 AND X.HYK_NO=:CZKHM");
                        query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            aHYK_NO = query.FieldByName("HYK_NO").AsString;
                            aCDNR = query.FieldByName("CDNR").AsString;
                            aHYID = query.FieldByName("HYID").AsInteger;
                            aHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                            cZQJE = -one.fZKJE;

                            //券
                            query.SQL.Text = "select MDID from HYK_HYXX where HYK_NO=:HYK_NO";
                            query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                            query.Open();
                            mMDID = query.FieldByName("MDID").AsInteger;

                            query.SQL.Text = "select Y.HYID,Y.YHQID,Y.JSRQ,Y.JE from HYK_YHQZH Y,HYK_HYXX H where Y.HYID=H.HYID  and Y.YHQID=:pYHQID and H.HYK_NO=:HYK_NO and Y.JSRQ=:JSRQ order by JSRQ";
                            query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                            query.ParamByName("pYHQID").AsInteger = one.iYHQLX;
                            query.ParamByName("JSRQ").AsDateTime = MyDate.AddDays(one.iYXQTS).Date;
                            query.Open();
                            if ((!query.IsEmpty) && (cZQJE > 0))
                            {
                                if (cZQJE <= query.FieldByName("JE").AsFloat)
                                {
                                    CrmLibProc.UpdateYHQZH(out msg, query, aHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, one.iYHQLX, mMDID, 0, -cZQJE, FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime), "面值卡退售减券");
                                }
                                else
                                {
                                    // CrmLibProc.UpdateYHQZH(out msg, query, aHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, one.iYHQLX, mMDID, 0, -query.FieldByName("JE").AsFloat, FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime), "面值卡退售减券");
                                    msg = "会员优惠券余额不足,无法退售";
                                }
                            }
                        }
                    }
                    //处理赠送
                    foreach (ZFITEM one in ZFMX)
                    {
                        query.SQL.Text = "select X.HYK_NO,X.CDNR,X.HYID,X.HYKTYPE from HYK_HYXX X,HYKDEF D";
                        query.SQL.Add("where X.HYKTYPE=D.HYKTYPE AND D.BJ_CZK=0 AND X.HYK_NO=:CZKHM");
                        query.ParamByName("CZKHM").AsString = one.sHYK_NO;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            aHYK_NO = query.FieldByName("HYK_NO").AsString;
                            aCDNR = query.FieldByName("CDNR").AsString;
                            aHYID = query.FieldByName("HYID").AsInteger;
                            aHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;

                            //积分
                            mMDID = CrmLibProc.BGDDToMDID(query, BGDDDM);

                            CrmLibProc.UpdateJFZH(out msg, query, 0, aHYID, BASECRMDefine.HYK_JFBDCLLX_CZKFKSF, iJLBH, mMDID, one.fZSJF, iLoginRYID, sLoginRYMC, "面值卡退售减积分");

                        }
                    }
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

        public static string SearchBackCardData(int iSKJLBH)
        {
            string tp_return = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " SELECT M.JLBH,T.CZKHM  FROM  MZK_SKJL M,MZK_SKJLITEM T WHERE M.JLBH=T.JLBH AND M.FS=2 AND T.CZKHM IN";
                    query.SQL.Add("  ( select S.CZKHM from MZK_SKJL L,  MZK_SKJLITEM S where L.JLBH=S.JLBH AND L.FS=1 AND S.JLBH=:JLBH)");
                    query.SQL.Add("  and M.JLBH>:JLBH");
                    query.ParamByName("JLBH").AsInteger = iSKJLBH;
                    query.Open();
                    while (!query.Eof)
                    {
                        tp_return += query.FieldByName("JLBH").AsInteger.ToString() + ",";
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return tp_return;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_SKJL;MZK_SKJLITEM;MZK_SKJLSKMX;MZK_SKJLZPMX;MZK_SKJLZKITEM;MZK_SKJLZJFITEM", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string sKH = string.Empty;
            StringBuilder sql = new StringBuilder();


            //CheckExecutedTable(query, "HYK_CZK_CKJL", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_SKJL");

            MDID = CrmLibProc.BGDDToMDID(query, BGDDDM);
            //MDDY md = JsonConvert.DeserializeObject<MDDY>(BF.CrmProc.CrmLib.CrmLibProc.GetMDByRYID(iLoginRYID));
            MDID_CZ = MDID;

            query.SQL.Text = "insert into MZK_SKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,ZY,TK_FLAG,DJR,DJRMC,DJSJ,STATUS,DZKFJE,KFJE,ZSJE,KHID,LXR,SJZSJE,YWY,MDID,MDID_CZ,BJ_RJSH,TKJLBH,ZSJF,SJZSJF)";
            query.SQL.Add(" values(:JLBH,2,:SKSL,:YSZE,round(:ZKL,4),:ZKJE,:SSJE,:YXQ,:BGDDDM,:ZY,:TK_FLAG,:DJR,:DJRMC,:DJSJ,0,:DZKFJE,:KFJE,:ZSJE,:KHID,:LXR,:SJZSJE,:YWY,:MDID,:MDID_CZ,0,:TKJLBH,:ZSJF,:SJZSJF)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("SKSL").AsInteger = SKSL;
            query.ParamByName("YSZE").AsFloat = YSZE;
            query.ParamByName("ZKL").AsFloat = ZKL;
            query.ParamByName("ZKJE").AsFloat = ZKJE;
            query.ParamByName("SSJE").AsFloat = SSJE;
            query.ParamByName("YXQ").AsDateTime = YXQ;
            query.ParamByName("BGDDDM").AsString = BGDDDM;
            query.ParamByName("ZY").AsString = ZY;
            query.ParamByName("TK_FLAG").AsInteger = TK_FLAG;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DZKFJE").AsFloat = DZKFJE;
            query.ParamByName("KFJE").AsFloat = KFJE;
            query.ParamByName("ZSJE").AsFloat = ZSJE;
            query.ParamByName("KHID").AsInteger = KHID;
            query.ParamByName("LXR").AsString = LXR;
            query.ParamByName("SJZSJE").AsFloat = SJZSJE;
            query.ParamByName("YWY").AsInteger = YWY;
            query.ParamByName("MDID").AsInteger = MDID;
            query.ParamByName("MDID_CZ").AsInteger = MDID_CZ;
            query.ParamByName("TKJLBH").AsInteger = iTKJLBH;
            query.ParamByName("ZSJF").AsFloat = ZSJF;
            query.ParamByName("SJZSJF").AsFloat = SJZSJF;
            query.ExecSQL();

            query.SQL.Text = "update MZKCARD set SKJLBH=null where SKJLBH=:SKJLBH";
            query.ParamByName("SKJLBH").AsInteger = iJLBH;
            query.ExecSQL();

            foreach (MZKGL_MZKTSKMXITEM one in SKMXITEM)
            {
                query.SQL.Text = "insert into MZK_SKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,PDJE,HYID,KFJE,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:QCYE,:YXTZJE,:PDJE,:HYID,:KFJE,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("QCYE").AsFloat = one.fQCYE;
                query.ParamByName("YXTZJE").AsFloat = 0;
                query.ParamByName("PDJE").AsFloat = one.fPDJE;
                query.ParamByName("KFJE").AsFloat = 0;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                //query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDatetimeString(one.dYXQ);
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ExecSQL();

                query.SQL.Text = "update MZKCARD set XKRQ=sysdate, SKJLBH=:SKJLBH where CZKHM=:pCZKHM and NVL(SKJLBH,0)<=0";
                query.ParamByName("SKJLBH").AsInteger = iJLBH;
                query.ParamByName("pCZKHM").AsString = one.sCZKHM;
                query.ExecSQL();
            }
            //支付方式
            foreach (SKFS one in ZFFS)
            {
                query.SQL.Text = "insert into MZK_SKJLSKMX(JLBH,ZFFSID,JE)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();
            }
            //赠券
            foreach (ZQITEM one in ZQMX)
            {
                query.SQL.Text = "insert into MZK_SKJLZKITEM(JLBH,HYK_NO,YHQLX,ZKJE,YXQTS)";
                query.SQL.Add(" values(:JLBH,:HYK_NO,:YHQLX,:ZKJE,:YXQTS)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("YHQLX").AsInteger = one.iYHQLX;
                query.ParamByName("ZKJE").AsFloat = -one.fZKJE;
                query.ParamByName("YXQTS").AsInteger = one.iYXQTS;
                query.ExecSQL();
            }
            //赠积分
            foreach (ZFITEM one in ZFMX)
            {
                query.SQL.Text = "insert into MZK_SKJLZJFITEM(JLBH,HYK_NO,ZSJF)";
                query.SQL.Add(" values(:JLBH,:HYK_NO,:ZSJF)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("ZSJF").AsFloat = -one.fZSJF;
                query.ExecSQL();
            }

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //ExecTable(query, "HYK_CZK_CKJL", serverTime, "CZJPJ_JLBH");
            query.SQL.Text = "delete MZK_SKJLSKMX where JLBH=:JLBH";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ExecSQL();
            //支付方式
            foreach (SKFS one in ZFFS)
            {
                query.SQL.Text = "insert into MZK_SKJLSKMX(JLBH,ZFFSID,JE)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();
            }
            query.SQL.Text = "update MZK_SKJL set ZXR=:pZXR,ZXRQ = :ZXRQ,ZXRMC=:pZXRMC,STATUS = :pStat";
            query.SQL.Add("where JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("pZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRQ").AsDateTime = serverTime;
            query.ParamByName("pZXRMC").AsString = sLoginRYMC;
            query.ParamByName("pStat").AsInteger = 1;
            query.ExecSQL();


        }

        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string sYXQ = string.Empty;
            int sFXDW = 0;
            string sYZM = string.Empty;
            int sMDID = 0;
            string sCDNR = string.Empty;
            DateTime MyDate = serverTime;
            query.SQL.Text = " SELECT ZXRQ FROM MZK_SKJL WHERE JLBH=:JLBH ";
            query.ParamByName("JLBH").AsInteger = iTKJLBH;
            query.Open();
            if (!query.IsEmpty)
            {
                MyDate = query.FieldByName("ZXRQ").AsDateTime;
            }
            query.SQL.Clear();
            query.Params.Clear();
            //  ExecTable(query, "WX_MENU", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
            foreach (MZKGL_MZKTSKMXITEM one in SKMXITEM)
            {
                query.SQL.Text = "select YXQ,FXDW,YZM,MDID,CDNR from MZKXX where HYID=:HYID";
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.Open();
                sYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                sFXDW = query.FieldByName("FXDW").AsInteger;
                sYZM = query.FieldByName("YZM").AsString;
                sMDID = query.FieldByName("MDID").AsInteger;
                sCDNR = query.FieldByName("CDNR").AsString;
                query.Close();

                query.SQL.Text = "insert into MZKCARD(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,XKRQ,YXQ,FXDWID,YZM,MDID)";
                query.SQL.Add("values (:pHYK_NO,:pCDNR,:pHYKTYPE,round(:pYE,2),round(:pYXTZJE,2),round(:pPDJE,2),:pBGDDDM,:pBGR,2,null,1,sysdate,:pYXQ,:pFXDWID,:pYZM,:pMDID)");
                query.ParamByName("pHYK_NO").AsString = one.sCZKHM;
                query.ParamByName("pCDNR").AsString = sCDNR;
                query.ParamByName("pHYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("pYE").AsFloat = one.fQCYE;
                query.ParamByName("pYXTZJE").AsFloat = 0;
                query.ParamByName("pPDJE").AsFloat = one.fPDJE;
                query.ParamByName("pBGDDDM").AsString = BGDDDM;
                query.ParamByName("pBGR").AsInteger = iLoginRYID;
                query.ParamByName("pYXQ").AsDateTime = FormatUtils.StrToDate(sYXQ);
                query.ParamByName("pFXDWID").AsInteger = sFXDW;
                query.ParamByName("pYZM").AsString = sYZM;
                query.ParamByName("pMDID").AsInteger = sMDID;
                query.ExecSQL();
                CrmLibProc.SaveCardTrack(query, one.sCZKHM, one.iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_TS, iJLBH, iLoginRYID, sLoginRYMC);
                query.Close();
                query.SQL.Clear();
                try
                {
                    query.Params.Clear();
                    query.SQL.Text = "begin";
                    query.SQL.Add("delete MZK_JEZCLJL where HYID=:pHYID;");
                    query.SQL.Add("delete MZK_YEBD where HYID=:pHYID;");
                    // query.SQL.Add("delete HYK_MDJF where HYID=:pHYID;");
                    query.SQL.Add("delete MZK_JEZH where HYID=:pHYID;");
                    // query.SQL.Add("delete HYK_JFZH where HYID=:pHYID;");
                    // query.SQL.Add("delete HYK_YHQZH where HYID=:pHYID;");
                    query.SQL.Add("delete MZKXX where HYID=:pHYID;");
                    query.SQL.Add("end;");
                    query.ParamByName("pHYID").AsInteger = one.iHYID;
                    query.ExecSQL();
                }
                catch (Exception ex)
                {

                    msg = "以消费卡、无法进行退卡操作 " + ex.Message;
                    return;
                }
            }
            DoExecZS(out msg, serverTime, MyDate);
            query.SQL.Text = "update MZK_SKJL set STATUS=2,QDSJ=sysdate";
            query.SQL.Add("where JLBH=:pJLBH");
            query.ParamByName("pJLBH").AsInteger = iJLBH;
            query.ExecSQL();
        }

        public override bool IsValidExecData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = string.Empty;
            foreach (SKFS one in ZFFS)
            {
                if (one.fJE <= 0)
                {
                    msg = "退款方式" + one.sZFFSMC + "的金额大于0，不合法";
                    return false;
                }
            }
            if (Get_YSZE() != Get_SSJEALL())
            {
                msg = "退款方式的合计金额与实退金额不等！";
                return false;
            }
            if (Get_TKSL() <= 0)
            {
                msg = "退卡数量不能小于等于0！";
                return false;
            }

            query.SQL.Text = "select CZKHM  from MZK_SKJLITEM A ";
            query.SQL.Add("where JLBH=:JLBH ");
            query.SQL.Add("and (not exists(select 1 from MZKXX X,MZK_JEZH H where X.HYID=H.HYID and A.HYID=X.HYID and X.STATUS=0 and H.QCYE=H.YE)");
            query.SQL.Add("or exists(select 1 from MZK_SKJL B,MZK_SKJLITEM C where B.FS=2 and B.ZXR>0 and B.JLBH=C.JLBH and B.JLBH<>A.JLBH and A.HYID=C.HYID ))");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.Open();
            while (!query.Eof)
            {
                msg += "卡号" + query.FieldByName("CZKHM").AsString + "已退卡或不是有效卡！";
                query.Next();
            }
            if (msg != "")
            {
                return false;
            }
            query.SQL.Clear();
            query.Params.Clear();
            query.Close();
            foreach (ZQITEM one in ZQMX)
            {

                query.SQL.Text = "SELECT Y.HYID,YHQID,SUM(JE) JE FROM HYK_YHQZH Y,HYK_HYXX X ";
                query.SQL.Add("WHERE Y.HYID=X.HYID AND Y.JSRQ>sysdate and X.HYK_NO=:HYK_NO ");
                query.SQL.Add("and Y.YHQID=:YHQID");
                query.SQL.Add("GROUP BY Y.HYID,Y.YHQID");
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("YHQID").AsInteger = one.iYHQLX;
                query.Open();
                if (query.FieldByName("JE").AsFloat < one.fZKJE)
                {
                    msg = "会员卡" + one.sHYK_NO + "中没有足够的优惠券!";
                    return false;
                }
            }
            foreach (ZFITEM one in ZFMX)
            {
                query.SQL.Text = "SELECT X.HYID,SUM(WCLJF) WCLJF FROM HYK_JFZH H,HYK_HYXX X ";
                query.SQL.Add("WHERE H.HYID=X.HYID AND X.HYK_NO=:HYK_NO ");
                query.SQL.Add("GROUP BY X.HYID");
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.Open();
                if (query.FieldByName("WCLJF").AsFloat < one.fZSJF)
                {
                    msg = "会员卡" + one.sHYK_NO + "中没有足够的积分!";
                    return false;
                }
            }
            query.Close();


            return true;
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("iKHID", "W.KHID");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iSTATUS", "W.STATUS");

            query.SQL.Text = "select W.*,B.BGDDMC,M.MDMC,W.KHID,A.KHMC,A.LXRXM,A.LXRSJ";
            query.SQL.Add(" from MZK_SKJL W,HYK_BGDD B,MDDY M,MZK_KHDA A");
            query.SQL.Add(" where W.BGDDDM=B.BGDDDM and W.MDID=M.MDID and W.KHID=A.KHID(+) and W.FS=2");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,F.HYKNAME";
                query.SQL.Add(" from MZK_SKJLITEM I,HYKDEF F");
                query.SQL.Add(" where I.HYKTYPE=F.HYKTYPE and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    MZKGL_MZKTSKMXITEM item = new MZKGL_MZKTSKMXITEM();
                    ((MZKGL_MZKTS)lst[0]).SKMXITEM.Add(item);
                    item.sCZKHM = query.FieldByName("CZKHM").AsString;
                    item.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item.fPDJE = query.FieldByName("PDJE").AsInteger;
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString();
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.iSKDJLBH = query.FieldByName("JLBH").AsInteger;
                    query.Next();
                }

                query.SQL.Text = "select I.*,S.ZFFSMC";
                query.SQL.Add(" from MZK_SKJLSKMX I,ZFFS S");
                query.SQL.Add(" where I.ZFFSID=S.ZFFSID and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    SKFS item = new SKFS();
                    ((MZKGL_MZKTS)lst[0]).ZFFS.Add(item);
                    item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    query.Next();
                }
                query.SQL.Text = "select I.*,S.YHQMC";
                query.SQL.Add(" from MZK_SKJLZKITEM I,YHQDEF S");
                query.SQL.Add(" where I.YHQLX=S.YHQID and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    ZQITEM item = new ZQITEM();
                    ((MZKGL_MZKTS)lst[0]).ZQMX.Add(item);
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.iYHQLX = query.FieldByName("YHQLX").AsInteger;
                    ((MZKGL_MZKTS)lst[0]).ZKYHQLX = query.FieldByName("YHQLX").AsInteger;
                    item.iYXQTS = query.FieldByName("YXQTS").AsInteger;
                    ((MZKGL_MZKTS)lst[0]).ZKYHQTS = query.FieldByName("YXQTS").AsInteger;
                    item.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    ((MZKGL_MZKTS)lst[0]).ZKYHQMC = query.FieldByName("YHQMC").AsString;
                    query.Next();
                }

                query.SQL.Text = "select I.*";
                query.SQL.Add(" from MZK_SKJLZJFITEM I");
                query.SQL.Add(" where I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    ZFITEM item = new ZFITEM();
                    ((MZKGL_MZKTS)lst[0]).ZFMX.Add(item);
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.fZSJF = query.FieldByName("ZSJF").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKTS bill = new MZKGL_MZKTS();
            bill.iJLBH = query.FieldByName("JLBH").AsInteger;
            bill.FS = query.FieldByName("FS").AsInteger;
            bill.SKSL = query.FieldByName("SKSL").AsInteger;
            bill.YSZE = query.FieldByName("YSZE").AsFloat;
            bill.ZKL = query.FieldByName("ZKL").AsFloat;
            bill.ZKJE = query.FieldByName("ZKJE").AsFloat;
            bill.SSJE = query.FieldByName("SSJE").AsFloat;
            bill.BGDDDM = query.FieldByName("BGDDDM").AsString;
            bill.ZY = query.FieldByName("ZY").AsString;
            bill.iDJR = query.FieldByName("DJR").AsInteger;
            bill.sDJRMC = query.FieldByName("DJRMC").AsString;
            bill.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            bill.STATUS = query.FieldByName("STATUS").AsInteger;
            bill.KHID = query.FieldByName("KHID").AsInteger;
            bill.DZKFJE = query.FieldByName("DZKFJE").AsInteger;
            bill.ZSJE = query.FieldByName("ZSJE").AsFloat;
            bill.LXR = query.FieldByName("LXR").AsString;
            bill.SJZSJE = query.FieldByName("SJZSJE").AsFloat;
            bill.YWY = query.FieldByName("YWY").AsInteger;
            bill.ZXR = query.FieldByName("ZXR").AsInteger;
            bill.ZXRMC = query.FieldByName("ZXRMC").AsString;
            bill.ZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            bill.KHMC = query.FieldByName("KHMC").AsString;
            bill.LXRXM = query.FieldByName("LXRXM").AsString;
            bill.LXRSJ = query.FieldByName("LXRSJ").AsString;
            bill.BGDDMC = query.FieldByName("BGDDMC").AsString;
            bill.MDMC = query.FieldByName("MDMC").AsString;
            bill.iTKJLBH = query.FieldByName("TKJLBH").AsInteger;
            bill.ZSJF = query.FieldByName("ZSJF").AsFloat;
            bill.SJZSJF = query.FieldByName("SJZSJF").AsFloat;
            return bill;
        }
    }
}
