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
using System.Text.RegularExpressions;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKFS : DJLR_ZXQDZZ_CLass
    {
        private DataTable dt_kdmx = new DataTable();
        public int iHYID = 0;

        public List<SKFS> ZFFS = new List<SKFS>();
        public class SKFS
        {
            public string sZFFSMC;
            public int iZFFSID = 0;
            public decimal fJE = 0;
            public string dFKRQ = string.Empty;
            public string sJYBH = string.Empty;
            public int TYPE = 0;
            public string DM = string.Empty;

        }
        public class SXD
        {
            public int JLBH = 0;
            public string NAME = string.Empty;
            public Decimal JE = 0;
        }
        public List<kdmx> SKKD = new List<kdmx>();
        public class kdmx
        {
            public string sCZKHM_BEGIN = "";
            public string sCZKHM_END = "";
            public int iSKSL = 0;
            public Decimal fMZJE = 0;
            public string iHYKTYPE = "";
            public string iBJ_SKLX = "";
            public string sHYKNAME = "";
            public string iJLBH = "";
            public string sFSstr = "";
            public int iRANDOM_LEN = 0;
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
        public class findkd
        {
            public Decimal LB_YSJE = 0;
            public Decimal TB_ZFFS_1 = 0;
            public int LB_SKXX_SKZS = 0;
            public Decimal ZKL = 0;
            public Decimal ZKJE = 0;
            public List<kdmx> SKKD = new List<kdmx>();
            public Decimal ZKLPBL = 0;
            public Decimal ZKLPJE = 0;
            public string TKJLBH = string.Empty;
            public int ZKYHQLX = 0;
            public string ZKYHQMC = string.Empty;
            public int ZKYHQTS = 0;
            public double fYSJE = 0;

        }
        public List<findkd> ZKMX = new List<findkd>();
        public class CashCard
        {
            public int CardId = 0;
            public string CardCode = string.Empty;
            public string CardCodeScope1 = string.Empty;
            public int CardTypeId = 0;
            public Decimal Balance = 0;
            public Decimal Bottom = 0;
            public int ValidDateType = 0;
            public string ValidDateLen = string.Empty;
            public DateTime ValidDate = DateTime.MinValue;
            public int ProcSeq = 0;
            public Decimal ProcMoney = 0;
            public int ProcType = 0;
            public string sCDNR = string.Empty;


        }
        public class HYIDYEJM
        {
            public int pHYID = 0;
            public int pMDID = 0;

            public string sHYK_NO = string.Empty;
            public string sCDNR = string.Empty;
            public double fQCYE = 0;

            public double fPDJE = 0;

        }
        public string[] asFieldNames = {
                                           "iJLBH;W.JLBH",
                                           "sBGDDDM;W.BGDDDM",
                                           "iSKSL;W.SKSL",
                                           "iMDID_CZ;W.MDID_CZ",
                                           "iDJR;W.DJR",
                                           "sDJRMC;W.DJRMC",
                                           "dDJSJ;W.DJSJ",
                                           "iZXR;W.ZXR",
                                           "sZXRMC;W.ZXRMC",
                                           "dZXRQ;W.ZXRQ",
                                           "LXRSJ;A.LXRSJ",
                                           "MDMC;M.MDMC",
                                           "LXRXM;A.LXRXM",
                                       };

        public int FS = 0;
        public int SKSL = 0;
        public Decimal YSZE = 0;
        public double ZKL = 0;
        public Decimal ZKJE = 0;
        public Decimal SSJE = 0;
        public DateTime YXQ = DateTime.MinValue;
        public string BGDDDM = string.Empty;
        public string ZY = string.Empty;
        public int TK_FLAG = 0;
        public int ZXR = 0;
        public string ZXRMC = string.Empty;
        public string ZXRQ = string.Empty;
        public int STATUS = 0;
        public string QDSJ = string.Empty;
        public Decimal DZKFJE = 0;
        public Decimal KFJE = 0;
        public Decimal ZSJE = 0;
        public int KHID = 0;
        public string LXR = string.Empty;
        public Decimal SJZSJE = 0;
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
        public int iRJSH = 0;
        public int iJKSH = 0;
        public int aHYID = 0;
        public int minHYID = 0;
        public findkd findkdxx = new findkd();


        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (YSZE != Get_SSJEALL_NEW())
            {
                msg = "应收金额与实收金额不等！";
                return false;
            }
            if (Get_SSJEALL_NEW() == -1)
            {
                msg = "实收金额校验错误请输入合法数据！";
                return false;
            }
            if (Get_SKSL_NEW() <= 0)
            {
                msg = "售卡数量不能小于等于0！";
                return false;
            }
            if ((ZKJE > 0) && (SJZSJE > 0))
            {
                if (ZKJE < GetZQJE())
                {
                    msg = "赠券金额大于允许赠券金额！";
                    return false;
                }
            }

            return true;
        }

        public bool Get_SKFS(out string msg, out List<MZKGL_MZKFS.SKFS> bill)
        {
            bill = new List<MZKGL_MZKFS.SKFS>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = " SELECT * FROM ZFFS where TYPE<>12";// 
                    query.Open();
                    while (!query.Eof)
                    {
                        MZKGL_MZKFS.SKFS tp_list = new MZKGL_MZKFS.SKFS();
                        tp_list.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                        tp_list.DM = query.FieldByName("ZFFSDM").AsString;
                        tp_list.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                        tp_list.TYPE = query.FieldByName("TYPE").AsInteger;
                        bill.Add(tp_list);
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }

            if (msg == "") { return true; }
            else { return false; }
        }

        public bool Find_CXFHTJDK(out string msg, string hyktype, string bgdddm, string jlbh, string kskh, string ksl, string skje, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            msg = "";
            //
            #region
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, ksl, "结束卡号") == false) { return false; };
            if (CheckValid_Int(out msg, bgdddm, "保管地点") == false) { return false; };
            if (CheckValid_Int(out msg, skje, "售卡金额") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };
            //

            #endregion
            //
            DbConnection conn;
            if (bj_czk == "0")
            {
                conn = CyDbConnManager.GetDbConnection("CRMDB");
            }
            else
            {
                conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            }
            //
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                //
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_hyktype = "";
                    string tp_qcye = "0";
                    string tp_sklx = "";
                    string tp_hzm = "";
                    //string tp_fwdwid = "";
                    #region
                    string rSQL1 = " ";
                    rSQL1 += " select C.CZKHM,C.HYKTYPE,NVL(C.QCYE,0) AS QCYE,Y.KHHZM,C.FXDWID from MZKCARD C,HYKDEF Y";
                    rSQL1 += " where C.HYKTYPE=Y.HYKTYPE ";
                    rSQL1 += " and BGDDDM = '" + bgdddm.Trim() + "'";
                    rSQL1 += " and STATUS = 1";
                    rSQL1 += " and Y.BJ_CZK=" + bj_czk;

                    //if (bj_zsk == "1")
                    //{
                    //    rSQL1 += " and BJ_ZSK = 1";
                    //}
                    //else
                    //{
                    //    rSQL1 += " and ((BJ_ZSK is null) or (BJ_ZSK = 0))";
                    //}
                    //rSQL1 += " and ((SKJLBH is null) or (SKJLBH = 0))";
                    rSQL1 += " and ((SKJLBH is null) or (SKJLBH = 0) or (SKJLBH ='" + jlbh.Trim() + "'))";

                    if (kskh.Trim() != "")
                    {
                        rSQL1 += " and CZKHM = '" + kskh.Trim() + "'";
                    }
                    if (hyktype != null && hyktype != "")
                    {
                        rSQL1 += " and C.HYKTYPE = '" + hyktype + "'";
                    }
                    rSQL1 += " and C.HYKTYPE in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")";
                    //rSQL1 += " and C.BGR in ( " + Get_SQL_CZK_Qx1(userid, userid) + ")";

                    query.SQL.Text = rSQL1;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或无权限！";
                        return false;
                    }
                    else
                    {
                        tp_hyktype = query.FieldByName("hyktype").AsInteger.ToString();
                        tp_qcye = query.FieldByName("QCYE").AsInteger.ToString();
                        tp_hzm = query.FieldByName("KHHZM").AsString;
                        //tp_fwdwid = query.FieldByName("FXDWID").AsInteger.ToString();



                    }
                    //获取卡类型 找同种卡类型的卡
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    if (bj_czk == "1")
                    {
                        if (tp_qcye == "0")
                        {
                            //零面值卡 输入skje !=0 true 其他都false
                            if (skje != "0")
                            {
                                tp_sklx = "1";
                            }
                            else
                            {
                                msg = "零面值卡，单卡售卡金额不为零！";
                                return false;
                            }
                        }
                        else
                        {
                            //非零面值卡 输入skje =0 true 其他都false
                            if (skje == "0")
                            {
                                tp_sklx = "0";
                            }
                            else
                            {
                                msg = "非零面值卡，单卡售卡金额必须为零！";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        tp_sklx = "0";
                    }
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    string tp_czkhm_ks = "";//开始卡号
                    string tp_czkhm_sl = "";//sl
                    string tp_czkhm_js = "";//结束卡号
                    //
                    if (kskh.Trim() != "")
                    {

                        tp_czkhm_ks = kskh.Trim();
                    }
                    else
                    {
                        msg = "开始卡号不能为空！";
                        return false;
                    }
                    //
                    #region
                    bool tp_bjtshzm = false;
                    int tp_bjtshzm_inx = 0;
                    if (tp_hzm != null && tp_hzm != "")
                    {
                        if (tp_hzm.IndexOf("x") < 0 && tp_hzm.IndexOf("X") < 0)
                        {
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_czkhm_ks.LastIndexOf(tp_hzm));

                        }
                        else
                        {
                            tp_bjtshzm_inx = tp_czkhm_ks.Length - (tp_hzm.Length);
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_bjtshzm_inx);
                            ksl = ksl.Substring(0, tp_bjtshzm_inx);
                            tp_bjtshzm = true;
                        }
                    }
                    //
                    #region
                    if (tp_czkhm_ks.Length != ksl.Length)
                    {
                        msg = "开始卡号与结束卡号位数不等！";
                        return false;
                    }
                    else
                    {
                        long tp_con = Int64.Parse(ksl) - Int64.Parse(tp_czkhm_ks) + 1;
                        if (tp_con <= 0 || tp_con > 1000)
                        {
                            msg = "卡数量范围（0-999）！"; return false;
                        }
                        else
                        {
                            ksl = tp_con.ToString();
                        }
                    }
                    #endregion

                    #endregion
                    //
                    if (ksl.Trim() != "")
                    {
                        tp_czkhm_sl = ksl;
                        //计算结束卡号
                        tp_czkhm_js = (Int64.Parse(tp_czkhm_ks) + Int64.Parse(tp_czkhm_sl) - 1).ToString().PadLeft(tp_czkhm_ks.Length, '0');
                    }
                    else
                    {
                        msg = "卡数量不能为空！";
                        return false;
                    }
                    #endregion
                    //------------------------------------------------------------------------------------------
                    DataTable dt_Adress = new DataTable();
                    #region
                    string rSQL = " ";
                    rSQL += " select C.CZKHM,QCYE,C.HYKTYPE,C.FXDWID from MZKCARD C,HYKDEF Y ";
                    rSQL += " where C.HYKTYPE=Y.HYKTYPE ";
                    rSQL += " and BGDDDM = '" + bgdddm + "'";
                    rSQL += " and c.QCYE='" + tp_qcye + "'";
                    rSQL += " and STATUS = 1";
                    rSQL += " and Y.BJ_CZK=" + bj_czk;
                    //if (bj_zsk == "1")
                    //{
                    //    rSQL1 += " and BJ_ZSK = 1";
                    //}
                    //else
                    //{
                    //    rSQL1 += " and ((BJ_ZSK is null) or (BJ_ZSK = 0))";
                    //}
                    rSQL += " and ((SKJLBH is null) or (SKJLBH = 0) or (SKJLBH ='" + jlbh + "'))";
                    rSQL += " and C.HYKTYPE='" + tp_hyktype + "'";
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.CZKHM,0," + tp_bjtshzm_inx + ") >= '" + tp_czkhm_ks + "'";
                    }
                    else
                    {
                        rSQL += " and C.CZKHM >= '" + tp_czkhm_ks + tp_hzm + "'";
                    }
                    rSQL += " and length(C.CZKHM)=" + (tp_czkhm_ks + tp_hzm).Length;
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.CZKHM,0," + tp_bjtshzm_inx + ") <= '" + tp_czkhm_js + "'";
                    }
                    else
                    {
                        rSQL += " and C.CZKHM <= '" + tp_czkhm_js + tp_hzm + "'";
                    }
                    //rSQL += " and C.FXDWID="+tp_fwdwid+"";
                    rSQL += " order by C.CZKHM";

                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = rSQL;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或不符合规则！";
                        return false;
                    }
                    else
                    {
                        dt_Adress = query.GetDataTable();
                        //
                        if (tp_sklx == "1")
                        {
                            #region
                            for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                            {
                                //
                                string tp_shykhype = dt_Adress.Rows[i]["HYKTYPE"].ToString();
                                string tp_shykno = dt_Adress.Rows[i]["czkhm"].ToString();
                                Decimal tp_ryxje = 0;
                                Decimal tp_ryxcs = 0;
                                rSQL = " ";
                                rSQL += " select ";
                                rSQL += " RYXJE,RYXCS FROM CZKMRCZJEXZ where  ";
                                rSQL += " HYKTYPE= " + tp_shykhype;
                                query.SQL.Clear();
                                query.Params.Clear();
                                query.SQL.Text = rSQL;
                                query.Open();
                                if (!query.IsEmpty)
                                {
                                    Decimal tp_inxnum = 0;
                                    while (!query.Eof)
                                    {
                                        tp_inxnum++;
                                        tp_ryxje = query.FieldByName("RYXJE").AsDecimal;
                                        tp_ryxcs = query.FieldByName("RYXCS").AsDecimal;
                                        query.Next();
                                    }
                                    if (tp_inxnum != 1)
                                    {
                                        msg = "该卡类型不存在唯一存款规则！";
                                        return false;
                                    }
                                }
                                else
                                {
                                    msg = "该卡类型不存在存款规则！";
                                    return false;
                                }
                                //
                                if (tp_ryxje - Convert.ToDecimal(skje) < 0)
                                {
                                    msg = "该存款规则额度不足！";
                                    return false;
                                }
                                if (tp_ryxcs - 1 < 0)
                                {
                                    msg = "该存款规则额度次数不足！";
                                    return false;
                                }

                                //
                                rSQL = " ";
                                rSQL += " select ";
                                rSQL += " case when " + tp_ryxje + "-nvl(SUM(MZJE) over(ORDER BY A.HYKTYPE) ,0) -" + skje + ">=0 then '1' else '0' end as yje, ";
                                rSQL += " case when " + tp_ryxcs + "-nvl(count(MZJE) over(ORDER BY A.HYKTYPE),0)>0 then '1' else '0' end as ycs ";
                                rSQL += " from MZK_SKJLKDITEM A , MZK_SKJL B  ";
                                rSQL += " where A.JLBH=B.JLBH AND A.CZKHM_BEGIN<=" + tp_shykno + " AND A.CZKHM_END>=" + tp_shykno + " ";
                                rSQL += " AND A.HYKTYPE= " + tp_shykhype;
                                rSQL += " AND (B.BJ_CK=1 OR B.BJ_CK=3 OR B.BJ_CK=0 OR B.BJ_CK IS NULL) AND B.ZXR>0 AND B.ZXRQ IS NOT NULL ";
                                rSQL += " and b.zxrq< to_date(to_char(sysdate+1,'yyyy-MM-dd'),'yyyy-MM-dd' ) ";
                                rSQL += " and b.zxrq>= to_date(to_char(sysdate,'yyyy-MM-dd'),'yyyy-MM-dd' ) ";
                                query.SQL.Clear();
                                query.Params.Clear();
                                query.SQL.Text = rSQL;
                                query.Open();
                                if (!query.IsEmpty)
                                {
                                    if (query.FieldByName("yje").AsString == "0")
                                    {
                                        msg = "该卡号" + tp_shykno + "不存在存款额度不足！"; return false;
                                    }
                                    if (query.FieldByName("ycs").AsString == "0")
                                    {
                                        msg = "该卡号" + tp_shykno + "不存在存款次数不足！"; return false;
                                    }

                                }
                            }
                            #endregion
                        }
                    }
                    //检查卡库存
                    int tp_rows = dt_Adress.Rows.Count;
                    if (tp_rows < Convert.ToInt64(ksl.Trim()))
                    {
                        string tp_khm = "";
                        //循环检测连续性 数据集合 为从小到大的排序
                        for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                        {
                            tp_khm = (Int64.Parse(tp_czkhm_ks) + i).ToString().PadLeft(tp_czkhm_ks.Length, '0');
                            if (dt_Adress.Rows[i]["CZKHM"].ToString() != tp_khm)
                            {
                                msg = "卡号(" + tp_khm + ")不存在！请录入连续的卡段!";
                                return false;
                            }
                        }
                        msg = "库存卡数量不足！最后一张卡号为" + tp_khm + "";
                        return false;
                    }

                    //检查卡面值金额也就是qcye 是否满足条件 面值卡面值金额超过范围 需提示 -20120330
                    //string tp_mzje = Get_MZKFS_MZJE(dt_Adress.Rows[0]["qcye"].ToString());
                    //if (tp_mzje != "-1")
                    //{
                    //    msg = "面值卡面值超出规定的" + tp_mzje + "元！";
                    //    return false;
                    //}
                    #endregion
                    //------------------------------------------------------------------------------------------
                    #region
                    if (tp_hyktype == "") { return false; }//获取 会员卡type失败

                    string iBegin = "";
                    string iEnd = "";

                    string tp_sql = " select HMCD,KHQDM,KHHZM from HYKDEF WHERE HYKTYPE=" + tp_hyktype;
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = tp_sql;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "卡段前导码查询失败！";
                        return false;
                    }
                    else
                    {
                        iBegin = query.FieldByName("KHQDM").AsString;
                        iEnd = query.FieldByName("KHHZM").AsString;
                    }
                    string maxkhm = "";
                    //
                    if (tp_bjtshzm == true)
                    {
                        if (dt_Adress.Rows.Count != Convert.ToInt32(ksl))
                        {
                            string tp_s = "";
                            for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                            {
                                tp_s += dt_Adress.Rows[i]["CZKHM"] + ";";
                            }
                            msg = "随机后缀码校验错误！查出卡号为:" + tp_s;
                            return false;
                        }
                        else
                        {
                            maxkhm = dt_Adress.Rows[dt_Adress.Rows.Count - 1]["CZKHM"].ToString();
                        }
                    }
                    //
                    MZKGL_CalculateKD crm_calculateKd = new MZKGL_CalculateKD();
                    DataTable tps = new DataTable();
                    tps = crm_calculateKd.a_CalculateKD(dt_Adress, iBegin, iEnd).Copy();
                    //
                    if (tps.Rows.Count != 1)
                    {
                        msg = "计算卡段失败！";
                        return false;
                    }
                    if (tp_bjtshzm == true)
                    {
                        if (maxkhm.Substring(0, tp_bjtshzm_inx) == tps.Rows[0]["CZKHM_END"].ToString().Substring(0, tp_bjtshzm_inx))
                        {
                            tps.Rows[0]["CZKHM_END"] = maxkhm;
                        }
                    }
                    //
                    set_dt_kdmx();
                    //
                    if (findkd != null && findkd.SKKD != null)
                    {
                        for (int i = 0; i <= findkd.SKKD.Count - 1; i++)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            mrow["CZKHM_BEGIN"] = findkd.SKKD[i].sCZKHM_BEGIN;
                            mrow["CZKHM_END"] = findkd.SKKD[i].sCZKHM_END;
                            mrow["SKSL"] = findkd.SKKD[i].iSKSL;
                            mrow["MZJE"] = findkd.SKKD[i].fMZJE;
                            mrow["HYKTYPE"] = findkd.SKKD[i].iHYKTYPE;
                            mrow["BJ_SKLX"] = findkd.SKKD[i].iBJ_SKLX;
                            //mrow["FXDWID"] = findkd.SKKD[i].FXDWID;

                            dt_kdmx.Rows.Add(mrow);
                        }
                    }
                    //
                    for (int i = 0; i <= tps.Rows.Count - 1; i++)
                    {
                        if (FindDate_In_Gv(dt_kdmx, "CZKHM_BEGIN", "CZKHM_END", tps.Rows[i]["CZKHM_BEGIN"].ToString(), tps.Rows[i]["CZKHM_END"].ToString()) == true)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            //mrow["JLBH"] = tps.Rows[i]["JLBH"].ToString();
                            mrow["CZKHM_BEGIN"] = tps.Rows[i]["CZKHM_BEGIN"].ToString();
                            mrow["CZKHM_END"] = tps.Rows[i]["CZKHM_END"].ToString();
                            mrow["SKSL"] = tps.Rows[i]["SKSL"].ToString();
                            if (tp_qcye != "0")
                            {
                                mrow["MZJE"] = tps.Rows[i]["MZJE"].ToString();
                            }
                            else
                            {
                                mrow["MZJE"] = skje.Trim();
                            }
                            mrow["HYKTYPE"] = tps.Rows[i]["HYKTYPE"].ToString();
                            mrow["BJ_SKLX"] = tp_sklx;
                            //mrow["FXDWID"] = tps.Rows[i]["FXDWID"].ToString();
                            dt_kdmx.Rows.Add(mrow);
                        }
                        else
                        {
                            msg = "您输入的卡号段已经存在！";
                            return false;

                        }
                    }
                    //this.GV_KDMX.DataSource = dt_kdmx;
                    //this.GV_KDMX.DataBind();

                    dt_Adress.Dispose();
                    tps.Dispose();
                    #endregion
                    MZKGL_MZKFS.findkd tp_findkd = new MZKGL_MZKFS.findkd();
                    //
                    if (bj_zsk != "1")
                    {
                        Get_FSZQ(query, tp_findkd);
                        Get_FSZJF(query, tp_findkd);
                    }

                    //

                    tp_findkd.LB_YSJE = Get_YSZE();
                    tp_findkd.TB_ZFFS_1 = Get_YSZE();
                    tp_findkd.LB_SKXX_SKZS = Get_SKSL();
                    List<MZKGL_MZKFS.kdmx> tp_list_skkd = new List<MZKGL_MZKFS.kdmx>();
                    for (int i = 0; i <= dt_kdmx.Rows.Count - 1; i++)
                    {
                        MZKGL_MZKFS.kdmx tp_skkd = new MZKGL_MZKFS.kdmx();
                        tp_skkd.sCZKHM_BEGIN = dt_kdmx.Rows[i]["CZKHM_BEGIN"].ToString();
                        tp_skkd.sCZKHM_END = dt_kdmx.Rows[i]["CZKHM_END"].ToString();
                        tp_skkd.iSKSL = Convert.ToInt32(dt_kdmx.Rows[i]["SKSL"].ToString());
                        tp_skkd.fMZJE = Convert.ToDecimal(dt_kdmx.Rows[i]["MZJE"].ToString());
                        tp_skkd.iHYKTYPE = dt_kdmx.Rows[i]["HYKTYPE"].ToString();
                        tp_skkd.iBJ_SKLX = dt_kdmx.Rows[i]["BJ_SKLX"].ToString();
                        //tp_skkd.FXDWID = dt_kdmx.Rows[i]["FXDWID"].ToString(); 
                        tp_list_skkd.Add(tp_skkd);
                    }
                    tp_findkd.SKKD = tp_list_skkd;
                    skxx = tp_findkd;
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        public bool Find_SKKD(out string msg, string hyktype, string bgdddm, string jlbh, string kskh, string ksl, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            #region
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, ksl, "结束卡号") == false) { return false; };
            if (CheckValid_Int(out msg, bgdddm, "保管地点") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };
            #endregion
            DbConnection conn = CyDbConnManager.GetDbConnection(bj_czk == "0" ? "CRMDB" : "CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_hyktype = "";
                    string tp_qcye = "0";
                    string tp_sklx = "";
                    string tp_hzm = "";
                    #region
                    string rSQL1 = " ";
                    rSQL1 += " select C.CZKHM,C.HYKTYPE,NVL(C.QCYE,0) AS QCYE,Y.KHHZM,C.FXDWID from MZKCARD C,HYKDEF Y";
                    rSQL1 += " where C.HYKTYPE=Y.HYKTYPE ";
                    rSQL1 += " and BGDDDM = '" + bgdddm.Trim() + "'";
                    rSQL1 += " and STATUS = 1";
                    rSQL1 += " and Y.BJ_CZK=" + bj_czk;
                    rSQL1 += " and ((SKJLBH is null) or (SKJLBH = 0) or (SKJLBH ='" + jlbh.Trim() + "'))";
                    if (kskh.Trim() != "")
                    {
                        rSQL1 += " and CZKHM = '" + kskh.Trim() + "'";
                    }
                    if (hyktype != null && hyktype != "")
                    {
                        rSQL1 += " and C.HYKTYPE = '" + hyktype + "'";
                    }
                    rSQL1 += " and C.HYKTYPE in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")";
                    query.SQL.Text = rSQL1;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或无权限！";
                        return false;
                    }
                    else
                    {
                        tp_hyktype = query.FieldByName("hyktype").AsInteger.ToString();
                        tp_qcye = query.FieldByName("QCYE").AsInteger.ToString();
                        tp_hzm = query.FieldByName("KHHZM").AsString;
                    }
                    //获取卡类型 找同种卡类型的卡
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    if (bj_czk == "1")
                    {
                        tp_sklx = tp_qcye == "0" ? "1" : "0";
                    }
                    else
                    {
                        tp_sklx = "0";
                    }
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    string tp_czkhm_ks = "";//开始卡号
                    string tp_czkhm_sl = "";//sl
                    string tp_czkhm_js = "";//结束卡号              
                    if (kskh.Trim() != "")
                    {
                        tp_czkhm_ks = kskh.Trim();
                    }
                    else
                    {
                        msg = "开始卡号不能为空！";
                        return false;
                    }
                    #region
                    bool tp_bjtshzm = false;
                    int tp_bjtshzm_inx = 0;
                    if (tp_hzm != null && tp_hzm != "")
                    {
                        if (tp_hzm.IndexOf("x") < 0 && tp_hzm.IndexOf("X") < 0)
                        {
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_czkhm_ks.LastIndexOf(tp_hzm));
                        }
                        else
                        {
                            tp_bjtshzm_inx = tp_czkhm_ks.Length - (tp_hzm.Length);
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_bjtshzm_inx);
                            ksl = ksl.Substring(0, tp_bjtshzm_inx);
                            tp_bjtshzm = true;
                        }
                    }
                    //
                    #region
                    if (tp_czkhm_ks.Length != ksl.Length)
                    {
                        msg = "开始卡号与结束卡号位数不等！";
                        return false;
                    }
                    else
                    {
                        long tp_con = Int64.Parse(ksl) - Int64.Parse(tp_czkhm_ks) + 1;
                        if (tp_con <= 0 || tp_con > 1000)
                        {
                            msg = "卡数量范围（0-999）！"; return false;
                        }
                        else
                        {
                            ksl = tp_con.ToString();
                        }
                    }
                    #endregion

                    #endregion
                    //
                    if (ksl.Trim() != "")
                    {
                        tp_czkhm_sl = ksl;
                        tp_czkhm_js = (Int64.Parse(tp_czkhm_ks) + Int64.Parse(tp_czkhm_sl) - 1).ToString().PadLeft(tp_czkhm_ks.Length, '0');
                    }
                    else
                    {
                        msg = "卡数量不能为空！";
                        return false;
                    }
                    #endregion
                    //------------------------------------------------------------------------------------------
                    DataTable dt_Adress = new DataTable();
                    #region
                    string rSQL = " ";
                    rSQL += " select C.CZKHM,QCYE,C.HYKTYPE,C.FXDWID from MZKCARD C,HYKDEF Y ";
                    rSQL += " where C.HYKTYPE=Y.HYKTYPE ";
                    rSQL += " and BGDDDM = '" + bgdddm + "'";
                    rSQL += " and c.QCYE='" + tp_qcye + "'";
                    rSQL += " and STATUS = 1";
                    rSQL += " and Y.BJ_CZK=" + bj_czk;
                    rSQL += " and ((SKJLBH is null) or (SKJLBH = 0) or (SKJLBH ='" + jlbh + "'))";
                    rSQL += " and C.HYKTYPE='" + tp_hyktype + "'";
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.CZKHM,0," + tp_bjtshzm_inx + ") >= '" + tp_czkhm_ks + "'";
                    }
                    else
                    {
                        rSQL += " and C.CZKHM >= '" + tp_czkhm_ks + tp_hzm + "'";
                    }
                    rSQL += " and length(C.CZKHM)=" + (tp_czkhm_ks + tp_hzm).Length;
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.CZKHM,0," + tp_bjtshzm_inx + ") <= '" + tp_czkhm_js + "'";
                    }
                    else
                    {
                        rSQL += " and C.CZKHM <= '" + tp_czkhm_js + tp_hzm + "'";
                    }
                    rSQL += " order by C.CZKHM";
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = rSQL;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或不符合规则！";
                        return false;
                    }
                    else
                    {
                        dt_Adress = query.GetDataTable();
                    }
                    int tp_rows = dt_Adress.Rows.Count;
                    if (tp_rows < Convert.ToInt64(ksl.Trim()))
                    {
                        string tp_khm = "";
                        for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                        {
                            tp_khm = (Int64.Parse(tp_czkhm_ks) + i).ToString().PadLeft(tp_czkhm_ks.Length, '0');
                            if (dt_Adress.Rows[i]["CZKHM"].ToString() != tp_khm)
                            {
                                msg = "卡号(" + tp_khm + ")不存在！请录入连续的卡段!";
                                return false;
                            }
                        }
                        msg = "库存卡数量不足！最后一张卡号为" + tp_khm + "";
                        return false;
                    }

                    #endregion
                    //------------------------------------------------------------------------------------------
                    #region
                    if (tp_hyktype == "") { return false; }//获取 会员卡type失败
                    string iBegin = "";
                    string iEnd = "";
                    string tp_sql = " select HMCD,KHQDM,KHHZM from HYKDEF WHERE HYKTYPE=" + tp_hyktype;
                    string rmsg = "";
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = tp_sql;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "卡段前导码查询失败！";
                        return false;
                    }
                    else
                    {
                        iBegin = query.FieldByName("KHQDM").AsString;
                        iEnd = query.FieldByName("KHHZM").AsString;
                    }
                    string maxkhm = "";
                    if (tp_bjtshzm == true)
                    {
                        if (dt_Adress.Rows.Count != Convert.ToInt32(ksl))
                        {
                            string tp_s = "";
                            for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                            {
                                tp_s += dt_Adress.Rows[i]["CZKHM"] + ";";
                            }
                            msg = "随机后缀码校验错误！查出卡号为:" + tp_s;
                            return false;
                        }
                        else
                        {
                            maxkhm = dt_Adress.Rows[dt_Adress.Rows.Count - 1]["CZKHM"].ToString();
                        }
                    }
                    //
                    MZKGL_CalculateKD crm_calculateKd = new MZKGL_CalculateKD();
                    DataTable tps = new DataTable();
                    tps = crm_calculateKd.a_CalculateKD(dt_Adress, iBegin, iEnd).Copy();
                    //
                    if (tps.Rows.Count != 1)
                    {
                        msg = "计算卡段失败！";
                        return false;
                    }
                    if (tp_bjtshzm == true)
                    {
                        if (maxkhm.Substring(0, tp_bjtshzm_inx) == tps.Rows[0]["CZKHM_END"].ToString().Substring(0, tp_bjtshzm_inx))
                        {
                            tps.Rows[0]["CZKHM_END"] = maxkhm;
                        }
                    }
                    set_dt_kdmx();
                    if (findkd != null && findkd.SKKD != null)
                    {
                        for (int i = 0; i <= findkd.SKKD.Count - 1; i++)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            mrow["CZKHM_BEGIN"] = findkd.SKKD[i].sCZKHM_BEGIN;
                            mrow["CZKHM_END"] = findkd.SKKD[i].sCZKHM_END;
                            mrow["SKSL"] = findkd.SKKD[i].iSKSL;
                            mrow["MZJE"] = findkd.SKKD[i].fMZJE;
                            mrow["HYKTYPE"] = findkd.SKKD[i].iHYKTYPE;
                            mrow["BJ_SKLX"] = findkd.SKKD[i].iBJ_SKLX;
                            dt_kdmx.Rows.Add(mrow);
                        }
                    }
                    //
                    for (int i = 0; i <= tps.Rows.Count - 1; i++)
                    {
                        if (FindDate_In_Gv(dt_kdmx, "CZKHM_BEGIN", "CZKHM_END", tps.Rows[i]["CZKHM_BEGIN"].ToString(), tps.Rows[i]["CZKHM_END"].ToString()) == true)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            mrow["CZKHM_BEGIN"] = tps.Rows[i]["CZKHM_BEGIN"].ToString();
                            mrow["CZKHM_END"] = tps.Rows[i]["CZKHM_END"].ToString();
                            mrow["SKSL"] = tps.Rows[i]["SKSL"].ToString();
                            mrow["MZJE"] = tps.Rows[i]["MZJE"].ToString();
                            mrow["HYKTYPE"] = tps.Rows[i]["HYKTYPE"].ToString();
                            mrow["BJ_SKLX"] = tp_sklx;
                            dt_kdmx.Rows.Add(mrow);
                        }
                        else
                        {
                            msg = "您输入的卡号段已经存在！";
                            return false;
                        }
                    }
                    decimal tp_zqcje = 0;
                    for (int i = 0; i < dt_Adress.Rows.Count; i++)
                    {
                        tp_zqcje += Convert.ToInt32(dt_Adress.Rows[i]["QCYE"]);
                    }
                    tp_zqcje += findkd.LB_YSJE;
                    dt_Adress.Dispose();
                    tps.Dispose();
                    #endregion
                    MZKGL_MZKFS.findkd tp_findkd = new MZKGL_MZKFS.findkd();
                    if (bj_zsk != "1")
                    {
                        Get_FSZQ(query, tp_findkd, tp_zqcje);
                        Get_FSZJF(query, tp_findkd, tp_zqcje);
                    }
                    tp_findkd.LB_YSJE = tp_zqcje;
                    tp_findkd.TB_ZFFS_1 = tp_zqcje;
                    tp_findkd.LB_SKXX_SKZS = Get_SKSL();
                    List<MZKGL_MZKFS.kdmx> tp_list_skkd = new List<MZKGL_MZKFS.kdmx>();
                    for (int i = 0; i <= dt_kdmx.Rows.Count - 1; i++)
                    {
                        MZKGL_MZKFS.kdmx tp_skkd = new MZKGL_MZKFS.kdmx();
                        tp_skkd.sCZKHM_BEGIN = dt_kdmx.Rows[i]["CZKHM_BEGIN"].ToString();
                        tp_skkd.sCZKHM_END = dt_kdmx.Rows[i]["CZKHM_END"].ToString();
                        tp_skkd.iSKSL = Convert.ToInt32(dt_kdmx.Rows[i]["SKSL"].ToString());
                        tp_skkd.fMZJE = Convert.ToDecimal(dt_kdmx.Rows[i]["MZJE"].ToString());
                        tp_skkd.iHYKTYPE = dt_kdmx.Rows[i]["HYKTYPE"].ToString();
                        tp_skkd.iBJ_SKLX = dt_kdmx.Rows[i]["BJ_SKLX"].ToString();
                        tp_list_skkd.Add(tp_skkd);
                    }
                    tp_findkd.SKKD = tp_list_skkd;
                    skxx = tp_findkd;
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        public bool Find_SKKD1(out string msg, string hyktype, string bgdddm, string jlbh, string kskh, string jskh, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            #region
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, jskh, "结束卡号") == false) { return false; };
            if (CheckValid_Int(out msg, bgdddm, "保管地点") == false) { return false; };
            if (CheckValid_Int(out msg, hyktype, "卡类型") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };
            #endregion
            DbConnection conn = CyDbConnManager.GetDbConnection(bj_czk == "0" ? "CRMDB" : "CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                throw new MyDbException(e.Message, true);
            }
            //if (hyk.iRANDOM_LEN != 0)
            //{
            //    kskh += "00";
            //    jskh += "99";
            //}
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    HYKDEF hyk = JsonConvert.DeserializeObject<HYKDEF>(CrmLibProc.GetHYKDEF_TYPE(out msg, query, Convert.ToInt32(hyktype)));
                    #region
                    query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC,D.KHQDM,D.KHHZM,NVL(H.QCYE,0) AS QCYE from MZKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                    query.SQL.Add(" and H.BGDDDM='" + bgdddm.Trim() + "'");
                    query.SQL.Add(" and H.HYKTYPE=" + Convert.ToInt32(hyktype));
                    query.SQL.Add(" and H.CZKHM>='" + kskh.Trim() + "' ");
                    query.SQL.Add(" and H.CZKHM<='" + jskh.Trim() + "' ");
                    query.SQL.Add(" and  length(H.CZKHM)=" + kskh.Trim().Length + "");
                    query.SQL.Add(" and ((SKJLBH is null) or (SKJLBH = 0))");
                    query.SQL.Add(" and H.HYKTYPE in (" + Get_SQL_CZK_Qx(userid, bj_czk) + ") ");
                    if (findkd != null && findkd.SKKD.Count != 0)
                    {
                        foreach (var item in findkd.SKKD)
                        {
                            query.SQL.Add("  and  H.CZKHM  not in (select CZKHM from MZKCARD where CZKHM>=" + item.sCZKHM_BEGIN + "  and CZKHM<=" + item.sCZKHM_END + " )");
                        }
                    }
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "库存卡不存在或者卡号段重复";
                        return false;
                    }
                    else
                    {
                        int tp_ksl = 0;
                        while (!query.Eof)
                        {
                            findkd.LB_SKXX_SKZS += 1;
                            findkd.LB_YSJE += query.FieldByName("QCYE").AsDecimal;
                            tp_ksl += 1;
                            query.Next();
                        }
                        kdmx kditem = new kdmx();
                        // kditem.CZKHM_BEGIN=hyk.iRANDOM_LEN!=0?  kskh.Trim().Substring(0,kskh.Trim().Length-2):kskh.Trim();
                        // kditem.CZKHM_END = hyk.iRANDOM_LEN != 0 ? jskh.Trim().Substring(0, jskh.Trim().Length - 2) : jskh.Trim();
                        kditem.sCZKHM_BEGIN = kskh.Trim();
                        kditem.sCZKHM_END = jskh.Trim();
                        kditem.iHYKTYPE = hyktype.Trim();
                        kditem.iSKSL = tp_ksl;
                        kditem.fMZJE = 0;
                        //kditem.iRANDOM_LEN = hyk.iRANDOM_LEN;
                        kditem.sHYKNAME = "123";
                        findkd.SKKD.Add(kditem);
                    }
                    if (bj_zsk != "1")
                    {
                        Get_FSZQ(query, findkd, findkd.LB_YSJE);
                        Get_FSZJF(query, findkd, findkd.LB_YSJE);
                    }
                    skxx = findkd;
                    #endregion
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        public bool Find_MZKPLCK(out string msg, string hyktype, string mdid, string jlbh, string kskh, string jskh, string skje, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, jskh, "结束卡号") == false) { return false; };
            if (CheckValid_Int(out msg, mdid, "操作门店") == false) { return false; };
            if (CheckValid_Int(out msg, skje, "售卡金额") == false) { return false; };
            if (CheckValid_Int(out msg, hyktype, "卡类型") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };

            DbConnection conn = CyDbConnManager.GetDbConnection(bj_czk == "0" ? "CRMDB" : "CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                throw new MyDbException(e.Message, true);
            }
            //if (hyk.iRANDOM_LEN != 0)
            //{
            //    kskh += "00";
            //    jskh += "99";
            //}
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    HYKDEF hyk = JsonConvert.DeserializeObject<HYKDEF>(CrmLibProc.GetHYKDEF_TYPE(out msg, query, Convert.ToInt32(hyktype)));
                    DataTable dt_Adress = new DataTable();
                    query.SQL.Text = " select D.KHHZM,C.HYKTYPE,C.HYK_NO as czkhm,C.HYID,nvl(H.QCYE,0) QCYE,nvl(H.YE,0) YE";
                    query.SQL.Add("  from MZKXX C,HYKDEF D,MZK_JEZH H  WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+)");
                    query.SQL.Add("  and (c.status=0 or c.status=1) and (D.BJ_TS=0 or D.BJ_TS IS NULL) and D.BJ_XK=1 ");
                    query.SQL.Add("  and HYK_NO>='" + kskh.Trim() + "'   and HYK_NO<='" + jskh.Trim() + "'");
                    query.SQL.Add(" and  length(C.HYK_NO)=" + kskh.Trim().Length + "");
                    // query.SQL.Add("  AND C.MDID IN (select MDID from hyk_bgdd where bgdddm=" + bgdddm + ")");
                    query.SQL.Add("  and c.hyktype in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")");
                    if (findkd != null && findkd.SKKD.Count != 0)
                    {
                        foreach (var item in findkd.SKKD)
                        {
                            query.SQL.Add("  and HYK_NO  not in (select HYK_NO from MZKXX where HYK_NO>=" + item.sCZKHM_BEGIN + "  and HYK_NO<=" + item.sCZKHM_END + " )");
                        }
                    }
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "面值卡不存在或者卡号段重复";
                        return false;
                    }
                    else
                    {
                        int tp_ksl = 0;
                        bool BoolInsert = true;
                        string tp_hykno = string.Empty;
                        while (!query.Eof)
                        {
                            tp_hykno = query.FieldByName("czkhm").AsString;
                            BoolInsert = CrmLibProc.IsValidHHC(null, query.FieldByName("HYID").AsInteger);
                            findkd.LB_SKXX_SKZS += 1;
                            findkd.LB_YSJE += Convert.ToDecimal(skje);
                            tp_ksl += 1;
                            query.Next();
                        }
                        if (BoolInsert == false)
                        {
                            msg = "卡段中面值卡" + tp_hykno + "校验失败";
                            return false;
                        }
                        kdmx kditem = new kdmx();
                        kditem.sCZKHM_BEGIN = kskh.Trim();
                        kditem.sCZKHM_END = jskh.Trim();
                        kditem.iHYKTYPE = hyktype.Trim();
                        kditem.iSKSL = tp_ksl;
                        kditem.fMZJE = Convert.ToDecimal(skje);
                        //kditem.iRANDOM_LEN = hyk.iRANDOM_LEN;
                        kditem.sHYKNAME = "123";
                        findkd.SKKD.Add(kditem);
                    }
                    if (bj_zsk != "1")
                    {
                        Get_FSZQ(query, findkd, findkd.LB_YSJE);
                        Get_FSZJF(query, findkd, findkd.LB_YSJE);
                    }
                    skxx = findkd;

                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        public bool Find_MZKPLQK(out string msg, string hyktype, string mdid, string jlbh, string kskh, string jskh, string skje, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, jskh, "卡数量") == false) { return false; };
            if (CheckValid_Int(out msg, mdid, "操作门店") == false) { return false; };
            if (CheckValid_Int(out msg, skje, "取款金额") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };
            if (CheckValid_Int(out msg, hyktype, "卡类型") == false) { return false; };
            //
            DbConnection conn = CyDbConnManager.GetDbConnection(bj_czk == "0" ? "CRMDB" : "CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                throw new MyDbException(e.Message, true);
            }
            //if (hyk.iRANDOM_LEN != 0)
            //{
            //    kskh += "00";
            //    jskh += "99";
            //}
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    HYKDEF hyk = JsonConvert.DeserializeObject<HYKDEF>(CrmLibProc.GetHYKDEF_TYPE(out msg, query, Convert.ToInt32(hyktype)));
                    #region
                    if (Convert.ToDecimal(skje) <= 0)
                    {
                        msg = "取款金额不能为零";
                        return false;
                    }
                    query.SQL.Text = "  select D.KHHZM,C.HYKTYPE,C.HYK_NO as czkhm,C.HYID,nvl(H.QCYE,0) QCYE,nvl(H.YE,0) YE";
                    query.SQL.Add("  from MZKXX C,HYKDEF D,MZK_JEZH H  WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+)");
                    query.SQL.Add("  and (c.status=0 or c.status=1) and (D.BJ_TS=0 or D.BJ_TS IS NULL) AND H.YE>=" + skje + "");
                    query.SQL.Add("  and C.HYK_NO>='" + kskh.Trim() + "'  and C.HYK_NO<='" + jskh.Trim() + "'");
                    query.SQL.Add(" and  length(C.HYK_NO)=" + kskh.Trim().Length + "");
                    // query.SQL.Add("  AND C.MDID IN (select MDID from hyk_bgdd where bgdddm=" + bgdddm + ")");
                    query.SQL.Add("  and c.hyktype in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")");
                    if (findkd != null && findkd.SKKD.Count != 0)
                    {
                        foreach (var item in findkd.SKKD)
                        {
                            query.SQL.Add("  and C.HYK_NO  not in (select HYK_NO from MZKXX where HYK_NO>=" + item.sCZKHM_BEGIN + "  and HYK_NO<=" + item.sCZKHM_END + " )");
                        }
                    }
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "面值卡不存在或者已添加或余额不足！";
                        return false;
                    }
                    else
                    {
                        int tp_ksl = 0;
                        bool BoolInsert = true;
                        string tp_hykno = string.Empty;
                        while (!query.Eof)
                        {
                            tp_hykno = query.FieldByName("czkhm").AsString;
                            BoolInsert = CrmLibProc.IsValidHHC(null, query.FieldByName("HYID").AsInteger);
                            findkd.LB_SKXX_SKZS += 1;
                            findkd.LB_YSJE += Convert.ToDecimal(skje);
                            tp_ksl += 1;
                            query.Next();
                        }
                        if (BoolInsert == false)
                        {
                            msg = "卡段中面值卡" + tp_hykno + "校验失败";
                            return false;
                        }
                        kdmx kditem = new kdmx();
                        kditem.sCZKHM_BEGIN = kskh.Trim();
                        kditem.sCZKHM_END = jskh.Trim();
                        kditem.iHYKTYPE = hyktype.Trim();
                        kditem.iSKSL = tp_ksl;
                        kditem.fMZJE = Convert.ToDecimal(skje);
                        //kditem.iRANDOM_LEN = hyk.iRANDOM_LEN;
                        kditem.sHYKNAME = "123";
                        findkd.SKKD.Add(kditem);
                    }
                    skxx = findkd;
                    #endregion
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }


        public bool Find_CXFHTJDKQK(out string msg, string hyktype, string bgdddm, string jlbh, string kskh, string ksl, string skje, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            msg = "";
            //
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, ksl, "卡数量") == false) { return false; };
            if (CheckValid_Int(out msg, bgdddm, "保管地点") == false) { return false; };
            if (CheckValid_Int(out msg, skje, "售卡金额") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };

            //
            DbConnection conn;
            if (bj_czk == "0")
            {
                conn = CyDbConnManager.GetDbConnection("CRMDB");
            }
            else
            {
                conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            }
            //
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                //
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_hyktype = "";
                    string tp_qcye = "0";
                    string tp_sklx = "";
                    string tp_hzm = "";
                    Decimal tp_yye = 0;
                    //string tp_fwdwid = "";
                    #region

                    if (Convert.ToDecimal(skje) <= 0)
                    {
                        msg = "取款金额不足";
                        return false;
                    }

                    string rSQL1 = " ";
                    rSQL1 += " select D.KHHZM,C.HYKTYPE,C.HYK_NO as czkhm,C.HYID,nvl(H.QCYE,0) QCYE,nvl(H.YE,0) YE";
                    rSQL1 += " from MZKXX C,HYKDEF D,MZK_JEZH H ";
                    rSQL1 += " WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+) ";
                    rSQL1 += " and (c.status=0 or c.status=1) and (D.BJ_TS=0 or D.BJ_TS IS NULL) AND H.YE>0  ";
                    if (kskh.Trim() != "")
                    {
                        rSQL1 += " and HYK_NO = '" + kskh.Trim() + "'";
                    }
                    rSQL1 += " AND C.MDID IN (select MDID from hyk_bgdd where bgdddm=" + bgdddm + ")";
                    rSQL1 += " and c.hyktype in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")";
                    //客户强烈建议去掉"不能对赠送的卡存款的限制条件
                    //提出人 朱英俊 汤勇 2015-4-20
                    //rSQL1 += " and ((D.BJ_ZSK is null) or (D.BJ_ZSK = 0))";
                    rSQL1 += " order by HYK_NO";

                    query.SQL.Text = rSQL1;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或无权限！";
                        return false;
                    }
                    else
                    {
                        tp_hyktype = query.FieldByName("hyktype").AsInteger.ToString();
                        tp_qcye = query.FieldByName("QCYE").AsInteger.ToString();
                        tp_hzm = query.FieldByName("KHHZM").AsString;
                        tp_yye = query.FieldByName("YE").AsDecimal;
                        if (tp_yye < Convert.ToDecimal(skje))
                        {
                            msg = "余额不足";
                            return false;
                        }
                        //tp_fwdwid = query.FieldByName("FXDWID").AsInteger.ToString();
                    }
                    //获取卡类型 找同种卡类型的卡
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    if (bj_czk == "1")
                    {
                        tp_sklx = "2";

                        //skje = tp_qcye;
                    }
                    else
                    {
                        tp_sklx = "0";
                    }
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    string tp_czkhm_ks = "";//开始卡号
                    string tp_czkhm_sl = "";//sl
                    string tp_czkhm_js = "";//结束卡号
                    //
                    if (kskh.Trim() != "")
                    {

                        tp_czkhm_ks = kskh.Trim();
                    }
                    else
                    {
                        msg = "开始卡号不能为空！";
                        return false;
                    }
                    //
                    #region
                    bool tp_bjtshzm = false;
                    int tp_bjtshzm_inx = 0;
                    if (tp_hzm != null && tp_hzm != "")
                    {
                        if (tp_hzm.IndexOf("x") < 0 && tp_hzm.IndexOf("X") < 0)
                        {
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_czkhm_ks.LastIndexOf(tp_hzm));

                        }
                        else
                        {
                            tp_bjtshzm_inx = tp_czkhm_ks.Length - (tp_hzm.Length);
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_bjtshzm_inx);
                            ksl = ksl.Substring(0, tp_bjtshzm_inx);
                            tp_bjtshzm = true;
                        }
                    }
                    //
                    #region
                    if (tp_czkhm_ks.Length != ksl.Length)
                    {
                        msg = "开始卡号与结束卡号位数不等！";
                        return false;
                    }
                    else
                    {
                        long tp_con = Int64.Parse(ksl) - Int64.Parse(tp_czkhm_ks) + 1;
                        if (tp_con <= 0 || tp_con > 1000)
                        {
                            msg = "卡数量范围（0-999）！"; return false;
                        }
                        else
                        {
                            ksl = tp_con.ToString();
                        }
                    }
                    #endregion
                    #endregion
                    //
                    if (ksl.Trim() != "")
                    {
                        tp_czkhm_sl = ksl;
                        //计算结束卡号
                        tp_czkhm_js = (Int64.Parse(tp_czkhm_ks) + Int64.Parse(tp_czkhm_sl) - 1).ToString().PadLeft(tp_czkhm_ks.Length, '0');

                    }
                    else
                    {
                        msg = "卡数量不能为空！";
                        return false;
                    }
                    #endregion
                    //------------------------------------------------------------------------------------------
                    DataTable dt_Adress = new DataTable();
                    #region
                    string rSQL = " ";

                    //rSQL += " select HYKTYPE,czkhm,HYID,nvl(QCYE,0) QCYE,nvl(YE,0) YE from(";
                    rSQL += " select D.KHHZM,C.HYKTYPE,C.HYK_NO as czkhm,C.HYID,nvl(H.QCYE,0) QCYE,nvl(H.YE,0) YE";
                    rSQL += " from MZKXX C,HYKDEF D,MZK_JEZH H ";
                    //   rSQL += " WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+) and nvl(d.bj_zsk,0)=0 AND H.YE-" + skje + ">=0";
                    rSQL += " WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+)  AND H.YE-" + skje + ">=0";
                    //rSQL += " and BGDDDM = '" + WUC_Lv5_Vertical1.Value.Trim() + "'";
                    ///////////////////////////////////////////////////////////////
                    //  PubData pubdate = new PubData();
                    //默认可以重复存款
                    rSQL += " and (c.status=0 or c.status=1)";
                    //if (pubdate.GetBFConfigure("510000122", "0") == "0")
                    //{
                    //    rSQL += " and C.STATUS>=0";
                    //}
                    //else
                    //{
                    //    rSQL += " and C.STATUS=0 ";
                    //}
                    ////////////////////////////////////////////////////////////////
                    rSQL += " and C.HYKTYPE='" + tp_hyktype + "'";
                    //rSQL += " and H.YE=" + tp_qcye + "";
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.HYK_NO,0," + tp_bjtshzm_inx + ") >= '" + tp_czkhm_ks + "'";
                    }
                    else
                    {
                        rSQL += " and C.HYK_NO >= '" + tp_czkhm_ks + tp_hzm + "'";
                    }
                    rSQL += " and length(C.HYK_NO)=" + (tp_czkhm_ks + tp_hzm).Length;
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.HYK_NO,0," + tp_bjtshzm_inx + ") <= '" + tp_czkhm_js + "'";
                    }
                    else
                    {
                        rSQL += " and C.HYK_NO <= '" + tp_czkhm_js + tp_hzm + "'";
                    }
                    rSQL += " and c.hyktype in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")";
                    rSQL += " order by HYK_NO";


                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = rSQL;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或不符合规则！";
                        return false;
                    }
                    else
                    {
                        dt_Adress = query.GetDataTable();
                    }
                    //检查卡库存
                    int tp_rows = dt_Adress.Rows.Count;
                    if (tp_rows < Convert.ToInt64(ksl.Trim()))
                    {
                        string tp_khm = "";
                        //循环检测连续性 数据集合 为从小到大的排序
                        for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                        {
                            tp_khm = (Int64.Parse(tp_czkhm_ks) + i).ToString().PadLeft(tp_czkhm_ks.Length, '0');
                            if (dt_Adress.Rows[i]["CZKHM"].ToString() != tp_khm)
                            {
                                msg = "卡号(" + tp_khm + ")不存在！请录入连续的卡段!";
                                return false;
                            }
                        }
                        msg = "库存卡数量不足！最后一张卡号为" + tp_khm + "";
                        return false;
                    }

                    //检查卡面值金额也就是qcye 是否满足条件 面值卡面值金额超过范围 需提示 -20120330
                    //string tp_mzje = Get_MZKFS_MZJE(dt_Adress.Rows[0]["qcye"].ToString());
                    //if (tp_mzje != "-1")
                    //{
                    //    msg = "面值卡面值超出规定的" + tp_mzje + "元！";
                    //    return false;
                    //}
                    #endregion
                    //------------------------------------------------------------------------------------------
                    #region
                    if (tp_hyktype == "") { return false; }//获取 会员卡type失败

                    string iBegin = "";
                    string iEnd = "";

                    string tp_sql = " select HMCD,KHQDM,KHHZM from HYKDEF WHERE HYKTYPE=" + tp_hyktype;
                    string rmsg = "";
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = tp_sql;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "卡段前导码查询失败！";
                        return false;
                    }
                    else
                    {
                        iBegin = query.FieldByName("KHQDM").AsString;
                        iEnd = query.FieldByName("KHHZM").AsString;
                    }
                    string maxkhm = "";
                    //
                    if (tp_bjtshzm == true)
                    {
                        if (dt_Adress.Rows.Count != Convert.ToInt32(ksl))
                        {
                            string tp_s = "";
                            for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                            {
                                tp_s += dt_Adress.Rows[i]["CZKHM"] + ";";
                            }
                            msg = "随机后缀码校验错误！查出卡号为:" + tp_s;
                            return false;
                        }
                        else
                        {
                            maxkhm = dt_Adress.Rows[dt_Adress.Rows.Count - 1]["CZKHM"].ToString();
                        }
                    }
                    //
                    MZKGL_CalculateKD crm_calculateKd = new MZKGL_CalculateKD();
                    DataTable tps = new DataTable();
                    tps = crm_calculateKd.a_CalculateKD(dt_Adress, iBegin, iEnd).Copy();
                    //
                    if (tps.Rows.Count != 1)
                    {
                        msg = "计算卡段失败！";
                        return false;
                    }
                    if (tp_bjtshzm == true)
                    {
                        if (maxkhm.Substring(0, tp_bjtshzm_inx) == tps.Rows[0]["CZKHM_END"].ToString().Substring(0, tp_bjtshzm_inx))
                        {
                            tps.Rows[0]["CZKHM_END"] = maxkhm;
                        }
                    }
                    //
                    set_dt_kdmx();
                    //
                    if (findkd != null && findkd.SKKD != null)
                    {
                        for (int i = 0; i <= findkd.SKKD.Count - 1; i++)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            mrow["CZKHM_BEGIN"] = findkd.SKKD[i].sCZKHM_BEGIN;
                            mrow["CZKHM_END"] = findkd.SKKD[i].sCZKHM_END;
                            mrow["SKSL"] = findkd.SKKD[i].iSKSL;
                            mrow["MZJE"] = findkd.SKKD[i].fMZJE;
                            mrow["HYKTYPE"] = findkd.SKKD[i].iHYKTYPE;
                            mrow["BJ_SKLX"] = findkd.SKKD[i].iBJ_SKLX;
                            //mrow["FXDWID"] = findkd.SKKD[i].FXDWID;

                            dt_kdmx.Rows.Add(mrow);
                        }
                    }
                    //
                    for (int i = 0; i <= tps.Rows.Count - 1; i++)
                    {
                        if (FindDate_In_Gv(dt_kdmx, "CZKHM_BEGIN", "CZKHM_END", tps.Rows[i]["CZKHM_BEGIN"].ToString(), tps.Rows[i]["CZKHM_END"].ToString()) == true)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            //mrow["JLBH"] = tps.Rows[i]["JLBH"].ToString();
                            mrow["CZKHM_BEGIN"] = tps.Rows[i]["CZKHM_BEGIN"].ToString();
                            mrow["CZKHM_END"] = tps.Rows[i]["CZKHM_END"].ToString();
                            mrow["SKSL"] = tps.Rows[i]["SKSL"].ToString();

                            mrow["MZJE"] = skje.Trim();

                            mrow["HYKTYPE"] = tps.Rows[i]["HYKTYPE"].ToString();
                            mrow["BJ_SKLX"] = tp_sklx;
                            //mrow["FXDWID"] = tps.Rows[i]["FXDWID"].ToString();
                            dt_kdmx.Rows.Add(mrow);
                        }
                        else
                        {
                            msg = "您输入的卡号段已经存在！";
                            return false;

                        }
                    }
                    //this.GV_KDMX.DataSource = dt_kdmx;
                    //this.GV_KDMX.DataBind();

                    dt_Adress.Dispose();
                    tps.Dispose();
                    #endregion
                    MZKGL_MZKFS.findkd tp_findkd = new MZKGL_MZKFS.findkd();
                    //
                    if (bj_zsk != "1")
                    {
                        //Get_CKFL(query, tp_findkd, bgdddm);
                        //Get_CKFLP(query, tp_findkd, bgdddm);
                    }

                    //

                    tp_findkd.LB_YSJE = Get_YSZE();
                    tp_findkd.TB_ZFFS_1 = Get_YSZE();
                    tp_findkd.LB_SKXX_SKZS = Get_SKSL();
                    List<MZKGL_MZKFS.kdmx> tp_list_skkd = new List<MZKGL_MZKFS.kdmx>();
                    for (int i = 0; i <= dt_kdmx.Rows.Count - 1; i++)
                    {
                        MZKGL_MZKFS.kdmx tp_skkd = new MZKGL_MZKFS.kdmx();
                        tp_skkd.sCZKHM_BEGIN = dt_kdmx.Rows[i]["CZKHM_BEGIN"].ToString();
                        tp_skkd.sCZKHM_END = dt_kdmx.Rows[i]["CZKHM_END"].ToString();
                        tp_skkd.iSKSL = Convert.ToInt32(dt_kdmx.Rows[i]["SKSL"].ToString());
                        tp_skkd.fMZJE = Convert.ToDecimal(dt_kdmx.Rows[i]["MZJE"].ToString());
                        tp_skkd.iHYKTYPE = dt_kdmx.Rows[i]["HYKTYPE"].ToString();
                        tp_skkd.iBJ_SKLX = dt_kdmx.Rows[i]["BJ_SKLX"].ToString();
                        //tp_skkd.FXDWID = dt_kdmx.Rows[i]["FXDWID"].ToString(); 
                        tp_list_skkd.Add(tp_skkd);
                    }
                    tp_findkd.SKKD = tp_list_skkd;
                    skxx = tp_findkd;
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        public bool Find_CXFHTJDKCK(out string msg, string hyktype, string bgdddm, string jlbh, string kskh, string ksl, string skje, string bj_czk, string userid, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd)
        {
            skxx = null;
            msg = string.Empty;
            msg = "";
            //
            if (CheckValid_Int(out msg, kskh, "开始卡号") == false) { return false; };
            if (CheckValid_Int(out msg, ksl, "卡数量") == false) { return false; };
            if (CheckValid_Int(out msg, bgdddm, "保管地点") == false) { return false; };
            if (CheckValid_Int(out msg, skje, "售卡金额") == false) { return false; };
            if (CheckValid_Int(out msg, bj_czk, "单据调用参数错误") == false) { return false; };

            //
            DbConnection conn;
            if (bj_czk == "0")
            {
                conn = CyDbConnManager.GetDbConnection("CRMDB");
            }
            else
            {
                conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            }
            //
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                //
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_hyktype = "";
                    string tp_qcye = "0";
                    string tp_sklx = "";
                    string tp_hzm = "";
                    //string tp_fwdwid = "";
                    #region

                    string rSQL1 = " ";
                    rSQL1 += " select D.KHHZM,C.HYKTYPE,C.HYK_NO as czkhm,C.HYID,nvl(H.QCYE,0) QCYE,nvl(H.YE,0) YE";
                    rSQL1 += " from MZKXX C,HYKDEF D,MZK_JEZH H ";
                    rSQL1 += " WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+) ";//and nvl(d.bj_zsk,0)=0
                    rSQL1 += " and (c.status=0 or c.status=1) and (D.BJ_TS=0 or D.BJ_TS IS NULL) and D.BJ_XK=1 ";//and C.BJ_CZ=2";
                    if (kskh.Trim() != "")
                    {
                        rSQL1 += " and HYK_NO = '" + kskh.Trim() + "'";
                    }
                    rSQL1 += " AND C.MDID IN (select MDID from hyk_bgdd where bgdddm=" + bgdddm + ")";
                    rSQL1 += " and c.hyktype in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")";
                    //客户强烈建议去掉"不能对赠送的卡存款的限制条件
                    //提出人 朱英俊 汤勇 2015-4-20
                    //rSQL1 += " and ((D.BJ_ZSK is null) or (D.BJ_ZSK = 0))";
                    rSQL1 += " order by HYK_NO";
                    query.SQL.Text = rSQL1;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或无权限！";
                        return false;
                    }
                    else
                    {
                        tp_hyktype = query.FieldByName("hyktype").AsInteger.ToString();
                        tp_qcye = query.FieldByName("QCYE").AsInteger.ToString();
                        tp_hzm = query.FieldByName("KHHZM").AsString;
                        //tp_fwdwid = query.FieldByName("FXDWID").AsInteger.ToString();
                    }
                    //获取卡类型 找同种卡类型的卡
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    if (bj_czk == "1")
                    {
                        //零面值卡 输入skje !=0 true 其他都false
                        if (skje != "0")
                        {
                            tp_sklx = "2";
                        }
                        else
                        {
                            msg = "存款金额不为零！";
                            return false;
                        }
                    }
                    else
                    {
                        tp_sklx = "0";
                    }
                    #endregion
                    //---------------------------------------------------------------------------------------------
                    #region
                    string tp_czkhm_ks = "";//开始卡号
                    string tp_czkhm_sl = "";//sl
                    string tp_czkhm_js = "";//结束卡号
                    //
                    if (kskh.Trim() != "")
                    {

                        tp_czkhm_ks = kskh.Trim();
                    }
                    else
                    {
                        msg = "开始卡号不能为空！";
                        return false;
                    }
                    //
                    #region
                    bool tp_bjtshzm = false;
                    int tp_bjtshzm_inx = 0;
                    if (tp_hzm != null && tp_hzm != "")
                    {
                        if (tp_hzm.IndexOf("x") < 0 && tp_hzm.IndexOf("X") < 0)
                        {
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_czkhm_ks.LastIndexOf(tp_hzm));

                        }
                        else
                        {
                            tp_bjtshzm_inx = tp_czkhm_ks.Length - (tp_hzm.Length);
                            tp_czkhm_ks = tp_czkhm_ks.Substring(0, tp_bjtshzm_inx);
                            ksl = ksl.Substring(0, tp_bjtshzm_inx);
                            tp_bjtshzm = true;
                        }
                    }
                    //
                    #region
                    if (tp_czkhm_ks.Length != ksl.Length)
                    {
                        msg = "开始卡号与结束卡号位数不等！";
                        return false;
                    }
                    else
                    {
                        long tp_con = Int64.Parse(ksl) - Int64.Parse(tp_czkhm_ks) + 1;
                        if (tp_con <= 0 || tp_con > 1000)
                        {
                            msg = "卡数量范围（0-999）！"; return false;
                        }
                        else
                        {
                            ksl = tp_con.ToString();
                        }
                    }
                    #endregion
                    #endregion
                    //
                    if (ksl.Trim() != "")
                    {
                        tp_czkhm_sl = ksl;
                        //计算结束卡号
                        tp_czkhm_js = (Int64.Parse(tp_czkhm_ks) + Int64.Parse(tp_czkhm_sl) - 1).ToString().PadLeft(tp_czkhm_ks.Length, '0');

                    }
                    else
                    {
                        msg = "卡数量不能为空！";
                        return false;
                    }
                    #endregion
                    //------------------------------------------------------------------------------------------
                    DataTable dt_Adress = new DataTable();
                    #region
                    string rSQL = " ";

                    //rSQL += " select HYKTYPE,czkhm,HYID,nvl(QCYE,0) QCYE,nvl(YE,0) YE from(";
                    rSQL += " select D.KHHZM,C.HYKTYPE,C.HYK_NO as czkhm,C.HYID,nvl(H.QCYE,0) QCYE,nvl(H.YE,0) YE";
                    rSQL += " from MZKXX C,HYKDEF D,MZK_JEZH H ";
                    rSQL += " WHERE C.HYKTYPE=D.HYKTYPE  and   C.HYID=H.HYID(+) ";//and nvl(d.bj_zsk,0)=0
                    //rSQL += " and BGDDDM = '" + WUC_Lv5_Vertical1.Value.Trim() + "'";
                    ///////////////////////////////////////////////////////////////
                    //PubData pubdate = new PubData();
                    //默认可以重复存款
                    rSQL += " and (c.status=0 or c.status=1)";
                    //if (pubdate.GetBFConfigure("510000122", "0") == "0")
                    //{
                    //    rSQL += " and C.STATUS>=0";
                    //}
                    //else
                    //{
                    //    rSQL += " and C.STATUS=0 ";
                    //}
                    ////////////////////////////////////////////////////////////////
                    rSQL += " and C.HYKTYPE='" + tp_hyktype + "'";
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.HYK_NO,0," + tp_bjtshzm_inx + ") >= '" + tp_czkhm_ks + "'";
                    }
                    else
                    {
                        rSQL += " and C.HYK_NO >= '" + tp_czkhm_ks + tp_hzm + "'";
                    }
                    rSQL += " and length(C.HYK_NO)=" + (tp_czkhm_ks + tp_hzm).Length;
                    if (tp_bjtshzm == true)
                    {
                        rSQL += " and substr(C.HYK_NO,0," + tp_bjtshzm_inx + ") <= '" + tp_czkhm_js + "'";
                    }
                    else
                    {
                        rSQL += " and C.HYK_NO <= '" + tp_czkhm_js + tp_hzm + "'";
                    }
                    rSQL += " and c.hyktype in ( " + Get_SQL_CZK_Qx(userid, bj_czk) + ")";
                    rSQL += " order by HYK_NO";


                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = rSQL;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该卡号不存在或不符合规则！";
                        return false;
                    }
                    else
                    {
                        dt_Adress = query.GetDataTable();
                        //
                        #region
                        //for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                        //{
                        //    //
                        //    string tp_shykhype = dt_Adress.Rows[i]["HYKTYPE"].ToString();
                        //    string tp_shykno = dt_Adress.Rows[i]["czkhm"].ToString();
                        //    Decimal tp_ryxje = 0;
                        //    Decimal tp_ryxcs = 0;
                        //    //rSQL = " ";
                        //    //rSQL += " select ";
                        //    //rSQL += " RYXJE,RYXCS FROM CZKMRCZJEXZ where  ";
                        //    //rSQL += " HYKTYPE= " + tp_shykhype;
                        //    //query.SQL.Clear();
                        //    //query.Params.Clear();
                        //    //query.SQL.Text = rSQL;
                        //    //query.Open();
                        //    //if (!query.IsEmpty)
                        //    //{
                        //    //    Decimal tp_inxnum = 0;
                        //    //    while (!query.Eof)
                        //    //    {
                        //    //        tp_inxnum++;
                        //    //        tp_ryxje = query.FieldByName("RYXJE").AsDecimal;
                        //    //        tp_ryxcs = query.FieldByName("RYXCS").AsDecimal;
                        //    //        query.Next();
                        //    //    }
                        //    //    if (tp_inxnum != 1)
                        //    //    {
                        //    //        msg = "该卡类型不存在唯一存款规则！";
                        //    //        return false;
                        //    //    }
                        //    //}
                        //    //else
                        //    //{
                        //    //    msg = "该卡类型不存在存款规则！";
                        //    //    return false;
                        //    //}
                        //    //
                        //    //if (tp_ryxje - Convert.ToDecimal(skje) < 0)
                        //    //{
                        //    //    msg = "该存款规则额度不足！";
                        //    //    return false;
                        //    //}
                        //    //if (tp_ryxcs - 1 < 0)
                        //    //{
                        //    //    msg = "该存款规则额度次数不足！";
                        //    //    return false;
                        //    //}

                        //    //
                        //    rSQL = " ";
                        //    rSQL += " select ";
                        //    rSQL += " case when " + tp_ryxje + "-nvl(SUM(MZJE) over(ORDER BY A.HYKTYPE) ,0) -" + skje + ">=0 then '1' else '0' end as yje, ";
                        //    rSQL += " case when " + tp_ryxcs + "-nvl(count(MZJE) over(ORDER BY A.HYKTYPE),0)>0 then '1' else '0' end as ycs ";
                        //    rSQL += " from HYK_CZKSKJLKDITEM A , HYK_CZKSKJL B  ";
                        //    rSQL += " where A.JLBH=B.JLBH AND A.CZKHM_BEGIN<=" + tp_shykno + " AND A.CZKHM_END>=" + tp_shykno + " ";
                        //    rSQL += " AND A.HYKTYPE= " + tp_shykhype;
                        //    rSQL += " AND (B.BJ_CK=1 OR B.BJ_CK=3 OR B.BJ_CK=0 OR B.BJ_CK IS NULL) ";//AND B.ZXR>0 AND B.ZXRQ IS NOT NULL 
                        //    rSQL += " and b.zxrq< to_date(to_char(sysdate+1,'yyyy-MM-dd'),'yyyy-MM-dd' ) ";
                        //    rSQL += " and b.zxrq>= to_date(to_char(sysdate,'yyyy-MM-dd'),'yyyy-MM-dd' ) ";
                        //    query.SQL.Clear();
                        //    query.Params.Clear();
                        //    query.SQL.Text = rSQL;
                        //    query.Open();
                        //    if (!query.IsEmpty)
                        //    {
                        //        if (query.FieldByName("yje").AsString == "0")
                        //        {
                        //            msg = "该卡号" + tp_shykno + "不存在存款额度不足！"; return false;
                        //        }
                        //        if (query.FieldByName("ycs").AsString == "0")
                        //        {
                        //            msg = "该卡号" + tp_shykno + "不存在存款次数不足！"; return false;
                        //        }

                        //    }
                        //}
                        #endregion

                    }
                    //检查卡库存
                    int tp_rows = dt_Adress.Rows.Count;
                    if (tp_rows < Convert.ToInt64(ksl.Trim()))
                    {
                        string tp_khm = "";
                        //循环检测连续性 数据集合 为从小到大的排序
                        for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                        {
                            tp_khm = (Int64.Parse(tp_czkhm_ks) + i).ToString().PadLeft(tp_czkhm_ks.Length, '0');
                            if (dt_Adress.Rows[i]["CZKHM"].ToString() != tp_khm)
                            {
                                msg = "卡号(" + tp_khm + ")不存在！请录入连续的卡段!";
                                return false;
                            }
                        }
                        msg = "库存卡数量不足！最后一张卡号为" + tp_khm + "";
                        return false;
                    }

                    //检查卡面值金额也就是qcye 是否满足条件 面值卡面值金额超过范围 需提示 -20120330
                    //string tp_mzje = Get_MZKFS_MZJE(dt_Adress.Rows[0]["qcye"].ToString());
                    //if (tp_mzje != "-1")
                    //{
                    //    msg = "面值卡面值超出规定的" + tp_mzje + "元！";
                    //    return false;
                    //}
                    #endregion
                    //------------------------------------------------------------------------------------------
                    #region
                    if (tp_hyktype == "") { return false; }//获取 会员卡type失败

                    string iBegin = "";
                    string iEnd = "";

                    string tp_sql = " select HMCD,KHQDM,KHHZM from HYKDEF WHERE HYKTYPE=" + tp_hyktype;
                    string rmsg = "";
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = tp_sql;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "卡段前导码查询失败！";
                        return false;
                    }
                    else
                    {
                        iBegin = query.FieldByName("KHQDM").AsString;
                        iEnd = query.FieldByName("KHHZM").AsString;
                    }
                    string maxkhm = "";
                    //
                    if (tp_bjtshzm == true)
                    {
                        if (dt_Adress.Rows.Count != Convert.ToInt32(ksl))
                        {
                            string tp_s = "";
                            for (int i = 0; i <= dt_Adress.Rows.Count - 1; i++)
                            {
                                tp_s += dt_Adress.Rows[i]["CZKHM"] + ";";
                            }
                            msg = "随机后缀码校验错误！查出卡号为:" + tp_s;
                            return false;
                        }
                        else
                        {
                            maxkhm = dt_Adress.Rows[dt_Adress.Rows.Count - 1]["CZKHM"].ToString();
                        }
                    }
                    //
                    MZKGL_CalculateKD crm_calculateKd = new MZKGL_CalculateKD();
                    DataTable tps = new DataTable();
                    tps = crm_calculateKd.a_CalculateKD(dt_Adress, iBegin, iEnd).Copy();
                    //
                    if (tps.Rows.Count != 1)
                    {
                        msg = "计算卡段失败！";
                        return false;
                    }
                    if (tp_bjtshzm == true)
                    {
                        if (maxkhm.Substring(0, tp_bjtshzm_inx) == tps.Rows[0]["CZKHM_END"].ToString().Substring(0, tp_bjtshzm_inx))
                        {
                            tps.Rows[0]["CZKHM_END"] = maxkhm;
                        }
                    }
                    //
                    set_dt_kdmx();
                    //
                    if (findkd != null && findkd.SKKD != null)
                    {
                        for (int i = 0; i <= findkd.SKKD.Count - 1; i++)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            mrow["CZKHM_BEGIN"] = findkd.SKKD[i].sCZKHM_BEGIN;
                            mrow["CZKHM_END"] = findkd.SKKD[i].sCZKHM_END;
                            mrow["SKSL"] = findkd.SKKD[i].iSKSL;
                            mrow["MZJE"] = findkd.SKKD[i].fMZJE;
                            mrow["HYKTYPE"] = findkd.SKKD[i].iHYKTYPE;
                            mrow["BJ_SKLX"] = findkd.SKKD[i].iBJ_SKLX;
                            //mrow["FXDWID"] = findkd.SKKD[i].FXDWID;

                            dt_kdmx.Rows.Add(mrow);
                        }
                    }
                    //
                    for (int i = 0; i <= tps.Rows.Count - 1; i++)
                    {
                        if (FindDate_In_Gv(dt_kdmx, "CZKHM_BEGIN", "CZKHM_END", tps.Rows[i]["CZKHM_BEGIN"].ToString(), tps.Rows[i]["CZKHM_END"].ToString()) == true)
                        {
                            DataRow mrow = dt_kdmx.NewRow();
                            //mrow["JLBH"] = tps.Rows[i]["JLBH"].ToString();
                            mrow["CZKHM_BEGIN"] = tps.Rows[i]["CZKHM_BEGIN"].ToString();
                            mrow["CZKHM_END"] = tps.Rows[i]["CZKHM_END"].ToString();
                            mrow["SKSL"] = tps.Rows[i]["SKSL"].ToString();

                            mrow["MZJE"] = skje.Trim();

                            mrow["HYKTYPE"] = tps.Rows[i]["HYKTYPE"].ToString();
                            mrow["BJ_SKLX"] = tp_sklx;
                            //mrow["FXDWID"] = tps.Rows[i]["FXDWID"].ToString();
                            dt_kdmx.Rows.Add(mrow);
                        }
                        else
                        {
                            msg = "您输入的卡号段已经存在！";
                            return false;

                        }
                    }
                    //this.GV_KDMX.DataSource = dt_kdmx;
                    //this.GV_KDMX.DataBind();

                    dt_Adress.Dispose();
                    tps.Dispose();
                    #endregion
                    MZKGL_MZKFS.findkd tp_findkd = new MZKGL_MZKFS.findkd();
                    //
                    if (bj_zsk != "1")
                    {
                        //Get_CKFL(query, tp_findkd, bgdddm);
                        //Get_CKFLP(query, tp_findkd, bgdddm);
                    }

                    //

                    tp_findkd.LB_YSJE = Get_YSZE();
                    tp_findkd.TB_ZFFS_1 = Get_YSZE();
                    tp_findkd.LB_SKXX_SKZS = Get_SKSL();
                    List<MZKGL_MZKFS.kdmx> tp_list_skkd = new List<MZKGL_MZKFS.kdmx>();
                    for (int i = 0; i <= dt_kdmx.Rows.Count - 1; i++)
                    {
                        MZKGL_MZKFS.kdmx tp_skkd = new MZKGL_MZKFS.kdmx();
                        tp_skkd.sCZKHM_BEGIN = dt_kdmx.Rows[i]["CZKHM_BEGIN"].ToString();
                        tp_skkd.sCZKHM_END = dt_kdmx.Rows[i]["CZKHM_END"].ToString();
                        tp_skkd.iSKSL = Convert.ToInt32(dt_kdmx.Rows[i]["SKSL"].ToString());
                        tp_skkd.fMZJE = Convert.ToDecimal(dt_kdmx.Rows[i]["MZJE"].ToString());
                        tp_skkd.iHYKTYPE = dt_kdmx.Rows[i]["HYKTYPE"].ToString();
                        tp_skkd.iBJ_SKLX = dt_kdmx.Rows[i]["BJ_SKLX"].ToString();
                        //tp_skkd.FXDWID = dt_kdmx.Rows[i]["FXDWID"].ToString(); 
                        tp_list_skkd.Add(tp_skkd);
                    }
                    tp_findkd.SKKD = tp_list_skkd;
                    skxx = tp_findkd;
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        public bool Find_CXFHTJDKDEL(out string msg, string bgdddm, string bj_czk, string bj_zsk, out MZKGL_MZKFS.findkd skxx, MZKGL_MZKFS.findkd findkd, string delinx)
        {
            skxx = null;
            msg = string.Empty;
            //
            DbConnection conn;
            if (bj_czk == "0")
            {
                conn = CyDbConnManager.GetDbConnection("CRMDB");
            }
            else
            {
                conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            }
            //
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                //
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    //------------------------------------------------------------------------------------------
                    #region
                    //
                    set_dt_kdmx();
                    decimal tp_zqcje = 0;
                    //
                    if (findkd != null && findkd.SKKD != null)
                    {
                        for (int i = 0; i <= findkd.SKKD.Count - 1; i++)
                        {
                            if (i.ToString() != delinx)
                            {
                                DataRow mrow = dt_kdmx.NewRow();
                                mrow["CZKHM_BEGIN"] = findkd.SKKD[i].sCZKHM_BEGIN;
                                mrow["CZKHM_END"] = findkd.SKKD[i].sCZKHM_END;
                                mrow["SKSL"] = findkd.SKKD[i].iSKSL;
                                mrow["MZJE"] = findkd.SKKD[i].fMZJE;
                                mrow["HYKTYPE"] = findkd.SKKD[i].iHYKTYPE;
                                mrow["BJ_SKLX"] = findkd.SKKD[i].iBJ_SKLX;
                                tp_zqcje += findkd.SKKD[i].iSKSL * findkd.SKKD[i].fMZJE;
                                dt_kdmx.Rows.Add(mrow);
                            }
                        }
                    }
                    #endregion
                    MZKGL_MZKFS.findkd tp_findkd = new MZKGL_MZKFS.findkd();
                    //
                    if (bj_zsk != "1")
                    {
                        Get_FSZQ(query, tp_findkd, tp_zqcje);
                        Get_FSZJF(query, tp_findkd, tp_zqcje);
                    }

                    //

                    tp_findkd.LB_YSJE = Get_YSZE();
                    tp_findkd.TB_ZFFS_1 = Get_YSZE();
                    tp_findkd.LB_SKXX_SKZS = Get_SKSL();
                    List<MZKGL_MZKFS.kdmx> tp_list_skkd = new List<MZKGL_MZKFS.kdmx>();
                    for (int i = 0; i <= dt_kdmx.Rows.Count - 1; i++)
                    {
                        MZKGL_MZKFS.kdmx tp_skkd = new MZKGL_MZKFS.kdmx();
                        tp_skkd.sCZKHM_BEGIN = dt_kdmx.Rows[i]["CZKHM_BEGIN"].ToString();
                        tp_skkd.sCZKHM_END = dt_kdmx.Rows[i]["CZKHM_END"].ToString();
                        tp_skkd.iSKSL = Convert.ToInt32(dt_kdmx.Rows[i]["SKSL"].ToString());
                        tp_skkd.fMZJE = Convert.ToDecimal(dt_kdmx.Rows[i]["MZJE"].ToString());
                        tp_skkd.iHYKTYPE = dt_kdmx.Rows[i]["HYKTYPE"].ToString();
                        tp_skkd.iBJ_SKLX = dt_kdmx.Rows[i]["BJ_SKLX"].ToString(); ;
                        //tp_skkd.FXDWID = dt_kdmx.Rows[i]["FXDWID"].ToString(); 
                        tp_list_skkd.Add(tp_skkd);
                    }
                    tp_findkd.SKKD = tp_list_skkd;
                    skxx = tp_findkd;
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }

        }

        private void set_dt_kdmx()
        {
            dt_kdmx.Columns.Add("CZKHM_BEGIN");
            dt_kdmx.Columns.Add("CZKHM_END");
            dt_kdmx.Columns.Add("SKSL");
            dt_kdmx.Columns.Add("MZJE");
            dt_kdmx.Columns.Add("HYKTYPE");
            Decimal tt = 0;
            dt_kdmx.Columns["SKSL"].DataType = tt.GetType();
            dt_kdmx.Columns["MZJE"].DataType = tt.GetType();
            dt_kdmx.Columns.Add("BJ_SKLX");
            //dt_kdmx.Columns.Add("FXDWID");

        }

        public bool GET_SXD(out string msg, string userId, string bgdddm, out List<MZKGL_MZKFS.SXD> lpxx)
        {
            List<MZKGL_MZKFS.SXD> tp_sxd = new List<MZKGL_MZKFS.SXD>();
            lpxx = new List<MZKGL_MZKFS.SXD>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                //
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select A.JLBH,A.KHID,B.KHMC,A.SXJE ";
                    query.SQL.Add(" from MZK_SXSQD A ,MZK_KHDA B WHERE A.KHID=B.KHID and A.STATUS<=1");
                    query.SQL.Add(" AND A.MDID IN (SELECT MDID FROM HYK_BGDD WHERE BGDDDM=" + bgdddm + ")");
                    if (userId != "")
                    {
                        query.SQL.Add("  and B.DKHLX=1 and B.KHID=" + userId);
                    }

                    query.Open();
                    while (!query.Eof)
                    {

                        MZKGL_MZKFS.SXD entity = new MZKGL_MZKFS.SXD();
                        entity.JLBH = query.FieldByName("JLBH").AsInteger;
                        entity.JE = query.FieldByName("SXJE").AsDecimal;
                        tp_sxd.Add(entity);
                        query.Next();
                    }
                    query.Close();

                    if (tp_sxd.Count == 0)
                    {
                        msg = "该客户没有赊销单或不是大客户！";
                    }
                    else
                    {
                        if (tp_sxd.Count == 1)
                        {
                            lpxx = tp_sxd;
                        }
                        else
                        {
                            msg = "赊销单不唯一!";
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }

            }

            finally
            {
                conn.Close();
            }
            if (msg == "") { return true; }
            else { return false; }


        }
        public bool CheckValid_Int(out string msg, string name, string mssg)
        {
            msg = "";
            //BF.CrmProc.CrmBaseFc mfc = new CrmProc.CrmBaseFc();
            if (string.IsNullOrEmpty(name) == true)
            {
                msg = mssg + ":非空！请输入内容！";
                return false;
            }
            else
            {
                if (IsInt(name) == true)
                {
                    return true;
                }
                else
                {
                    msg = mssg + ":非空！请重新输入！";
                    return false;
                }
            }
        }

        private Decimal Get_YSZE_NEW()
        {
            try
            {
                Decimal tp_yzje = 0;
                foreach (kdmx one in SKKD)
                {
                    tp_yzje += one.fMZJE;
                }
                return tp_yzje;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        private Decimal Get_YSZE(MZKGL_MZKFS.findkd findkd)
        {
            Decimal tp_zje = 0;
            Decimal tp_zsl = 0;
            for (int i = 0; i <= findkd.SKKD.Count - 1; i++)
            {
                Decimal tp_sksl = 0;
                Decimal tp_mzje = 0;
                tp_sksl = findkd.SKKD[i].iSKSL; tp_zsl += tp_sksl;
                tp_mzje = findkd.SKKD[i].fMZJE; tp_zje += tp_mzje;
            }
            findkd.LB_YSJE = tp_zje;
            findkd.LB_SKXX_SKZS = Convert.ToInt32(tp_zsl);
            return tp_zje;
        }
        private Decimal Get_SSJEALL_NEW()
        {
            try
            {
                Decimal tp_ssje = 0;
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
        private Int32 Get_SKSL_NEW()
        {
            try
            {
                Int32 tp_SKSL = 0;
                foreach (kdmx one in SKKD)
                {
                    tp_SKSL += one.iSKSL;
                }
                return tp_SKSL;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        private Boolean FindDate_In_Gv(DataTable tables, string name_ks, string name_je, string czkhm_ks, string czkhm_js)
        {
            try
            {
                if (czkhm_ks.Length != czkhm_js.Length) { return false; }

                for (int i = 0; i <= tables.Rows.Count - 1; i++)
                {
                    //长度相等卡断
                    if (czkhm_ks.Length == tables.Rows[i]["CZKHM_BEGIN"].ToString().Length)
                    {
                        if (czkhm_ks.CompareTo(tables.Rows[i]["CZKHM_END"].ToString()) <= 0 && czkhm_js.CompareTo(tables.Rows[i]["CZKHM_BEGIN"].ToString()) >= 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string Get_SQL_CZK_Qx(string persion_id, string bj_czk)
        {
            string tp_str = "";
            tp_str += " select lpad(m.HYKKZID,2,'0') as hyktype  ";
            tp_str += " from HYKKZDEF m,HYKDEF H where H.FXFS=0 and (H.BJ_CZK=" + bj_czk + " ) and h.hykkzid=m.hykkzid  ";
            tp_str += " and (exists(select 1 from XTCZY_HYLXQX where HYKTYPE=H.HYKTYPE and PERSON_ID=" + persion_id + ")  ";
            tp_str += " or exists(select 1 from CZYGROUP_HYLXQX Q,XTCZYGRP G ";
            tp_str += " where Q.HYKTYPE=H.HYKTYPE and Q.ID=G.GROUPID and G.PERSON_ID=" + persion_id + "))  ";
            tp_str += " union ";
            tp_str += " select lpad(H.HYKTYPE,4,'0')  as hyktype  from HYKDEF H  ";
            tp_str += " where H.FXFS=0 and (H.BJ_CZK=" + bj_czk + " )  and (exists(select 1 from XTCZY_HYLXQX ";
            tp_str += " where HYKTYPE=H.HYKTYPE and PERSON_ID=" + persion_id + ")  ";
            tp_str += " or exists(select 1 from CZYGROUP_HYLXQX Q,XTCZYGRP G ";
            tp_str += " where Q.HYKTYPE=H.HYKTYPE and Q.ID=G.GROUPID and G.PERSON_ID=" + persion_id + ")) ";
            return tp_str;
        }
        private Decimal Get_YSZE()
        {
            try
            {
                Decimal tp_yzje = 0;
                for (int i = 0; i <= this.dt_kdmx.Rows.Count - 1; i++)
                {
                    Decimal tp_sksl = 0;
                    Decimal tp_mzje = 0;
                    tp_sksl = Convert.ToInt32(dt_kdmx.Rows[i]["SKSL"].ToString());
                    tp_mzje = Convert.ToDecimal(dt_kdmx.Rows[i]["MZJE"].ToString());

                    tp_yzje = tp_yzje + (tp_sksl * tp_mzje);


                }
                return tp_yzje;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        private Int32 Get_SKSL()
        {
            try
            {
                return Convert.ToInt32(this.dt_kdmx.Compute("sum(sksl)", "").ToString());
            }
            catch (Exception ex)
            {
                //this.LB_YSJE.Text = "0";
                return 0;
            }
        }

        private Decimal GetZQJE()
        {
            try
            {
                Decimal tp_zqje = 0;
                foreach (ZQITEM one in ZQMX)
                {
                    tp_zqje = tp_zqje + Convert.ToDecimal(one.fZKJE);
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

        public void Get_FSZJF(CyQuery query, MZKGL_MZKFS.findkd skxx, bool flag = true)
        {

            Decimal ckje = 0;
            if (flag == false)
            {
                ckje = Get_YSZE(skxx);
            }
            else
            {
                ckje = Get_YSZE();
            }
            #region ""
            string sql = "";
            sql += " select CKJE_BEGIN ,CKJE_END,ZSBL,ZSBL*:pXSJE Y_ZSBL,ZSJF";
            sql += " from MZK_CKSJFGZ where CKJE_BEGIN <=:pXSJE and CKJE_END>:pXSJE";
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql;
            query.ParamByName("pXSJE").AsDecimal = ckje;
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

        public void Get_FSZQ(CyQuery query, MZKGL_MZKFS.findkd skxx, bool flag = true)
        {

            Decimal ckje = 0;
            if (flag == false)
            {
                ckje = Get_YSZE(skxx);
            }
            else
            {
                ckje = Get_YSZE();
            }
            #region ""
            string sql = "";
            sql += " select G.XSJE_BEGIN ,G.XSJE_END,G.ZSBL,G.ZSBL*:pXSJE Y_ZSBL,G.YHQTYPE,D.YHQMC,G.YXQTS";
            sql += " from MZK_ZKGZ G,YHQDEF D where G.YHQTYPE=D.YHQID and G.XSJE_BEGIN <=:pXSJE and G.XSJE_END>:pXSJE";
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql;
            query.ParamByName("pXSJE").AsDecimal = ckje;
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
            double psjzsjf = 0;
            double psjzsje = 0;
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
                        psjzsje += one.fZKJE;
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
                                query.ParamByName("MDID").AsInteger = Convert.ToInt32(CrmLibProc.GetMDByRYID(iLoginRYID));
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

                            CrmLibProc.UpdateYHQZH(out msg, query, aHYID, BASECRMDefine.CZK_CLLX_CK, aFSJLBH, one.iYHQLX, mMDID, 0, one.fZKJE, FormatUtils.DateToString(serverTime.AddDays(one.iYXQTS)), "面值卡售卡赠券");



                        }
                    }
                    //处理赠送
                    foreach (ZFITEM one in ZFMX)
                    {
                        psjzsjf += one.fZSJF;
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
                                query.ParamByName("pYXQ").AsDateTime = YXQ;
                                query.ParamByName("pDJR").AsInteger = iLoginRYID;
                                query.ParamByName("pDJRMC").AsString = sLoginRYMC;
                                query.ParamByName("pDJSJ").AsDateTime = serverTime;
                                query.ParamByName("MDID").AsInteger = Convert.ToInt32(CrmLibProc.GetMDByRYID(iLoginRYID));
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
                                query.ParamByName("YXQ").AsDateTime = YXQ;
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
                            //积分
                            mMDID = CrmLibProc.BGDDToMDID(query, BGDDDM);

                            CrmLibProc.UpdateJFZH(out msg, query, 0, aHYID, BASECRMDefine.HYK_JFBDCLLX_CZKFKSF, aFSJLBH, mMDID, one.fZSJF, iLoginRYID, sLoginRYMC, "面值卡发售赠积分");


                        }

                    }
                    query.Close();

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

        public void Get_FSZQ(CyQuery query, MZKGL_MZKFS.findkd skxx, decimal zqcye)
        {
            #region "GetFSZQGZ"
            string sql = "";
            sql += " select G.XSJE_BEGIN ,G.XSJE_END,G.ZSBL,G.ZSBL*:pXSJE Y_ZSBL,G.YHQTYPE,D.YHQMC,G.YXQTS";
            sql += " from MZK_ZKGZ G,YHQDEF D where G.YHQTYPE=D.YHQID and G.XSJE_BEGIN <=:pXSJE and G.XSJE_END>:pXSJE";
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql;
            query.ParamByName("pXSJE").AsDecimal = zqcye;
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

        public void Get_FSZJF(CyQuery query, MZKGL_MZKFS.findkd skxx, decimal zqcye)
        {
            #region "GetFSZJFGZ"
            string sql = "";
            sql += " select CKJE_BEGIN ,CKJE_END,ZSBL,ZSBL*:pXSJE Y_ZSBL,ZSJF";
            sql += " from MZK_CKSJFGZ where CKJE_BEGIN <=:pXSJE and CKJE_END>:pXSJE";
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql;
            query.ParamByName("pXSJE").AsDecimal = zqcye;
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

        public static bool ValidData_FSJESQ(string sBGDDDM, string sSQMM)
        {
            bool BoolContinue = true;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
                    query.SQL.Text = "  select * from DEF_SKGLY where MDID=:MDID and PASSWORD=:PASSWORD";
                    query.ParamByName("MDID").AsInteger = iMDID;
                    // string sProjectKey = System.Configuration.ConfigurationManager.AppSettings["ProjectKey"].ToString();
                    // sSQMM = sProjectKey + EncryptUtils.CardEncrypt_NEW(sSQMM);
                    // query.ParamByName("PASSWORD").AsString = sSQMM;
                    query.ParamByName("PASSWORD").AsString = CrmLibProc.EncPSW(sSQMM);
                    query.Open();
                    if (query.IsEmpty)
                    {
                        BoolContinue = false;
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                    {
                        throw new MyDbException(e.Message, query.SqlText);
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return BoolContinue;

        }

        public bool IsInt(string strInput)
        {
            string strValue = strInput.Trim();
            Regex regex = new Regex(@"^[0-9]*$");
            Match match = regex.Match(strValue);

            return match.Success;
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
                iJLBH = SeqGenerator.GetSeq("MZK_SKJL"); ;
            MDID = CrmLibProc.BGDDToMDID(query, BGDDDM);
            //MDDY md = JsonConvert.DeserializeObject<MDDY>(BF.CrmProc.CrmLib.CrmLibProc.GetMDByRYID(iLoginRYID));
            MDID_CZ = MDID;
            query.SQL.Text = "insert into MZK_SKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,ZY,TK_FLAG,DJR,DJRMC,DJSJ,STATUS,DZKFJE,KFJE,ZSJE,KHID,LXR,SJZSJE,YWY,MDID,MDID_CZ,BJ_RJSH,ZSJF,SJZSJF)";
            query.SQL.Add(" values(:JLBH,1,:SKSL,:YSZE,round(:ZKL,4),:ZKJE,:SSJE,:YXQ,:BGDDDM,:ZY,:TK_FLAG,:DJR,:DJRMC,:DJSJ,0,:DZKFJE,:KFJE,:ZSJE,:KHID,:LXR,:SJZSJE,:YWY,:MDID,:MDID_CZ,0,:ZSJF,:SJZSJF)");
            query.ParamByName("JLBH").AsInteger = iJLBH; ;
            query.ParamByName("SKSL").AsInteger = SKSL;
            query.ParamByName("YSZE").AsDecimal = YSZE;
            query.ParamByName("ZKL").AsFloat = ZKL;
            query.ParamByName("ZKJE").AsDecimal = ZKJE;
            query.ParamByName("SSJE").AsDecimal = SSJE;
            query.ParamByName("YXQ").AsDateTime = YXQ.Date;
            query.ParamByName("BGDDDM").AsString = BGDDDM;
            query.ParamByName("ZY").AsString = ZY;
            query.ParamByName("TK_FLAG").AsInteger = TK_FLAG;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DZKFJE").AsDecimal = DZKFJE;
            query.ParamByName("KFJE").AsDecimal = KFJE;
            query.ParamByName("ZSJE").AsDecimal = ZSJE;
            query.ParamByName("KHID").AsInteger = KHID;
            query.ParamByName("LXR").AsString = LXR;
            query.ParamByName("SJZSJE").AsDecimal = SJZSJE;
            query.ParamByName("YWY").AsInteger = YWY;
            query.ParamByName("MDID").AsInteger = MDID;
            query.ParamByName("MDID_CZ").AsInteger = MDID_CZ;
            query.ParamByName("ZSJF").AsFloat = ZSJF;
            query.ParamByName("SJZSJF").AsFloat = SJZSJF;
            query.ExecSQL();

            query.SQL.Text = "update MZKCARD set SKJLBH=null where SKJLBH=:SKJLBH";
            query.ParamByName("SKJLBH").AsInteger = iJLBH;
            query.ExecSQL();

            foreach (kdmx one in SKKD)
            {
                query.SQL.Clear();
                query.Params.Clear();
                query.SQL.Text = "update MZKCARD A set A.SKJLBH=:SKJLBH";
                query.SQL.Add(" where A.CZKHM between :CZKHM_BEGIN and :CZKHM_END");
                query.SQL.Add(" and length(A.CZKHM) = length(:CZKHM_BEGIN)");
                query.SQL.Add(" and BGDDDM =:BGDDDM");
                query.SQL.Add(" and exists(select 1 from HYKDEF B where B.HYKTYPE=A.HYKTYPE and B.BJ_CZK=1)");
                query.ParamByName("SKJLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                //query.ParamByName("CZKHM").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("BGDDDM").AsString = BGDDDM;
                query.ExecSQL();

                query.SQL.Text = "insert into MZK_SKJLKDITEM(JLBH,CZKHM_BEGIN,CZKHM_END,SKSL,MZJE,HYKTYPE)";//hyktype
                query.SQL.Add(" values(:JLBH,:CZKHM_BEGIN,:CZKHM_END,:SKSL,:MZJE,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.ParamByName("SKSL").AsInteger = one.iSKSL;
                query.ParamByName("MZJE").AsDecimal = one.fMZJE;
                query.ParamByName("HYKTYPE").AsInteger = Convert.ToInt32(one.iHYKTYPE);
                query.ExecSQL();



            }
            List<CashCard> saleCardList = new List<CashCard>();

            #region
            sql.Length = 0;
            sql.Append("select a.CZKHM_BEGIN,b.QCYE,b.HYKTYPE,b.CZKHM,b.PDJE,b.YXQ,c.FS_YXQ,c.YXQCD ");
            sql.Append(" from MZK_SKJLKDITEM a, MZKCARD b, HYKDEF c ");
            sql.Append(" where a.HYKTYPE = b.HYKTYPE ");
            sql.Append("   and b.HYKTYPE = c.HYKTYPE ");
            sql.Append("   and a.JLBH = ").Append(iJLBH);
            sql.Append("   and b.CZKHM between a.CZKHM_BEGIN and a.CZKHM_END");
            sql.Append("   and length(b.CZKHM) = length(a.CZKHM_BEGIN) ");
            sql.Append("   and b.BGDDDM = '").Append(BGDDDM).Append("'");
            sql.Append("   and c.BJ_CZK = 1");
            sql.Append("   and b.SKJLBH = ").Append(iJLBH);
            sql.Append(" order by a.CZKHM_BEGIN");
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql.ToString();
            query.Open();
            while (!query.Eof)
            {
                CashCard card = new CashCard();
                saleCardList.Add(card);
                card.CardCodeScope1 = query.FieldByName("CZKHM_BEGIN").AsString;
                card.ProcMoney = query.FieldByName("QCYE").AsDecimal;
                card.CardTypeId = query.FieldByName("HYKTYPE").AsInteger;
                card.CardCode = query.FieldByName("CZKHM").AsString;
                card.Bottom = query.FieldByName("PDJE").AsDecimal;
                card.ValidDateType = query.FieldByName("FS_YXQ").AsInteger;

                card.ValidDateLen = query.FieldByName("YXQCD").AsString.Trim();
                if (card.ValidDateType == 1)
                {
                    card.ValidDate = serverTime.AddYears(Convert.ToInt32(card.ValidDateLen.Substring(0, card.ValidDateLen.Length - 1))).Date;
                }
                else
                {
                    card.ValidDate = query.FieldByName("YXQ").AsDateTime.Date;
                }
                query.Next();
            }
            query.Close();

            foreach (CashCard one in saleCardList)
            {
                query.SQL.Text = "insert into MZK_SKJLITEM(JLBH,CZKHM,QCYE,YXTZJE,PDJE,KFJE,YXQ,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:QCYE,:YXTZJE,:PDJE,:KFJE,:YXQ,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.CardCode;
                query.ParamByName("QCYE").AsDecimal = one.ProcMoney;
                query.ParamByName("YXTZJE").AsDecimal = 0;
                query.ParamByName("PDJE").AsDecimal = one.Bottom;
                query.ParamByName("KFJE").AsDecimal = 0;
                query.ParamByName("YXQ").AsDateTime = one.ValidDate;
                query.ParamByName("HYKTYPE").AsInteger = one.CardTypeId;

                query.ExecSQL();


                query.Close();
                query.SQL.Text = "select CDNR from MZKCARD where CZKHM='" + one.CardCode + "'";
                query.Open();
                one.sCDNR = query.Fields[0].AsString;
                query.Close();
                query.SQL.Text = "update MZKCARD set SKJLBH=:JLBH where CZKHM=:CZKHM";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.CardCode;
                query.ExecSQL();




            }

            #endregion
            //支付方式
            foreach (SKFS one in ZFFS)
            {
                query.SQL.Text = "insert into MZK_SKJLSKMX(JLBH,ZFFSID,JE)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                query.ParamByName("JE").AsDecimal = one.fJE;
                //query.ParamByName("JYBH").AsString = one.sJYBH;
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
                query.ParamByName("ZKJE").AsFloat = one.fZKJE;
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
                query.ParamByName("ZSJF").AsFloat = one.fZSJF;
                query.ExecSQL();
            }

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string pOutStr = string.Empty;
            bool bQD = true;
            CyParam myParams = new CyParam(query);
            double balance;

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
                query.ParamByName("JE").AsDecimal = one.fJE;
                //query.ParamByName("JYBH").AsString = one.sJYBH;
                query.ExecSQL();
            }
            List<CashCard> saleCardList = new List<CashCard>();
            StringBuilder sql = new StringBuilder();

            sql.Length = 0;
            sql.Append("select a.CZKHM_BEGIN,b.QCYE,b.HYKTYPE,b.CZKHM,b.PDJE,b.YXQ,c.FS_YXQ,c.YXQCD ");
            sql.Append(" from MZK_SKJLKDITEM a, MZKCARD b, HYKDEF c ");
            sql.Append(" where a.HYKTYPE = b.HYKTYPE ");
            sql.Append("   and b.HYKTYPE = c.HYKTYPE ");
            sql.Append("   and a.JLBH = ").Append(iJLBH);
            sql.Append("   and b.CZKHM between a.CZKHM_BEGIN and a.CZKHM_END");
            sql.Append("   and length(b.CZKHM) = length(a.CZKHM_BEGIN) ");
            sql.Append("   and b.BGDDDM = '").Append(BGDDDM).Append("'");
            sql.Append("   and c.BJ_CZK = 1");
            sql.Append("   and b.SKJLBH = ").Append(iJLBH);
            sql.Append(" order by a.CZKHM_BEGIN");
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql.ToString();
            query.Open();
            while (!query.Eof)
            {
                CashCard card = new CashCard();
                saleCardList.Add(card);
                card.CardCodeScope1 = query.FieldByName("CZKHM_BEGIN").AsString;
                card.ProcMoney = query.FieldByName("QCYE").AsDecimal;
                card.CardTypeId = query.FieldByName("HYKTYPE").AsInteger;
                card.CardCode = query.FieldByName("CZKHM").AsString;
                card.Bottom = query.FieldByName("PDJE").AsDecimal;
                card.ValidDateType = query.FieldByName("FS_YXQ").AsInteger;
                card.ValidDateLen = query.FieldByName("YXQCD").AsString.Trim();
                if (card.ValidDateType == 1)
                {
                    card.ValidDate = serverTime.AddYears(Convert.ToInt32(card.ValidDateLen.Substring(0, card.ValidDateLen.Length - 1))).Date;
                }
                else
                {
                    card.ValidDate = query.FieldByName("YXQ").AsDateTime.Date;
                }
                query.Next();
            }
            query.Close();

            foreach (CashCard one in saleCardList)
            {
                iHYID = SeqGenerator.GetSeq("MZKXX");
                MDID = CrmLibProc.BGDDToMDID(query, BGDDDM);
                query.SQL.Text = "insert into MZKXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,BJ_PSW,PASSWORD,DJR,DJRMC,DJSJ,STATUS,YBGDD,KHID,MDID,YZM,FXDW)";
                query.SQL.Add(" select :HYID,:HYKTYPE,CZKHM,CDNR,:JKRQ,:YXQ,:BJ_PSW,:PASSWORD,:DJR,:DJRMC,:DJSJ,-4,:YBGDD,:KHID,:MDID,YZM,FXDWID");//发卡门店保存与MDID相同
                query.SQL.Add(" from MZKCARD where CZKHM='" + one.CardCode + "'");
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("HYKTYPE").AsInteger = one.CardTypeId;
                //query.ParamByName("HYK_NO").AsString = one.sCZKHM;
                //query.ParamByName("CDNR").AsString = one.sCDNR;
                query.ParamByName("JKRQ").AsDateTime = serverTime;
                query.ParamByName("YXQ").AsDateTime = one.ValidDate;
                query.ParamByName("BJ_PSW").AsInteger = 1;
                query.ParamByName("PASSWORD").AsString = "";
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("DJSJ").AsDateTime = serverTime;
                query.ParamByName("YBGDD").AsString = BGDDDM;
                query.ParamByName("KHID").AsInteger = KHID;
                query.ParamByName("MDID").AsInteger = MDID;
                //query.ParamByName("GKID").AsInteger = iGKID;
                //query.ParamByName("FKRID").AsInteger = iJBRID;
                //query.ParamByName("FXDW").AsInteger = one.iFXDWID;
                query.ExecSQL();
                query.SQL.Text = "update MZK_SKJLITEM set HYID=:HYID where JLBH=:JLBH and czkhm='" + one.CardCode + "'";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = iHYID;

                query.ExecSQL();



            }
            DoExecZS(out msg, serverTime);

            query.SQL.Text = "update MZK_SKJL set ZXR=:pZXR,ZXRQ = :ZXRQ,ZXRMC=:pZXRMC,STATUS = :pStat,BJ_RJSH=:BJ_RJSH,BJ_JKSH=:BJ_JKSH";
            query.SQL.Add("where JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("pZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRQ").AsDateTime = serverTime;
            // query.ParamByName("QDSJ").AsDateTime = serverTime;
            query.ParamByName("pZXRMC").AsString = sLoginRYMC;
            query.ParamByName("pStat").AsInteger = 1;
            query.ParamByName("BJ_RJSH").AsInteger = iRJSH;
            query.ParamByName("BJ_JKSH").AsInteger = iJKSH;
            query.ExecSQL();

            minHYID = aHYID - SKSL + 1;
            if (iRJSH != 1)
            {
                //query.StoreProcName = "HYK_PROC_CZKFS_SH";
                //query.AddInputParam("pHYID", DbType.Int32, 10, minHYID);
                //query.AddInputParam("pJLBH", DbType.Int32, 10, iJLBH);
                //query.AddOutputParam("pStr", DbType.String, 3000);
                //query.ExecSQL();
                //ParamCount = query.Params.Count;
                //for (int inx = 0; inx < ParamCount; inx++)
                //{
                //    CyParam param = query.Params[inx];
                //    if ((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput))
                //    {
                //        myParams.Value = query.Command.Parameters[param.Index].Value;
                //    }
                //}
                //if (myParams.Value != null)
                //{
                //    msg = Convert.ToString(myParams.Value);
                //}
                if (msg == "")
                {
                    //余额加密
                    List<HYIDYEJM> HYIDYEJMList = new List<HYIDYEJM>();
                    #region
                    query.SQL.Text = "select X.HYID,X.HYK_NO,X.CDNR ,M.QCYE ,X.MDID,M.PDJE from MZK_SKJLITEM M,MZKXX X ";
                    query.SQL.Add(" where X.HYK_NO=M.CZKHM and M.JLBH=:JLBH order by X.HYID");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.Open();
                    while (!query.Eof)
                    {
                        HYIDYEJM sYEJM = new HYIDYEJM();
                        sYEJM.pHYID = query.FieldByName("HYID").AsInteger;
                        sYEJM.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        sYEJM.sCDNR = query.FieldByName("CDNR").AsString;
                        sYEJM.fQCYE = query.FieldByName("QCYE").AsFloat;
                        sYEJM.pMDID = query.FieldByName("MDID").AsInteger;
                        sYEJM.fQCYE = query.FieldByName("QCYE").AsFloat;
                        sYEJM.fPDJE = query.FieldByName("PDJE").AsFloat;



                        HYIDYEJMList.Add(sYEJM);
                        query.Next();
                    }

                    foreach (HYIDYEJM one in HYIDYEJMList)
                    {
                        CrmLibProc.UpdateMZKJEZH(out msg, query, one.pHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, one.pMDID, one.fQCYE, "储值卡发售", one.sHYK_NO, one.fPDJE);

                        CrmLibProc.EncryptBalanceOfCashCard_MZK(query, one.pHYID, serverTime, out balance);
                        query.SQL.Clear();
                        query.SQL.Text = "  insert into HHC(HYID,HHC)values(:HYID,:HHC)";
                        query.ParamByName("HYID").AsInteger = one.pHYID;
                        // query.ParamByName("HHC").AsString = one.pHYID.ToString() + one.sHYK_NO + one.sCDNR;
                        query.ParamByName("HHC").AsString = CrmLibProc.EncHHC(one.pHYID, one.sHYK_NO, one.sCDNR);
                        query.ExecSQL();
                        CrmLibProc.SaveCardTrack(query, one.sHYK_NO, one.pHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_FS, iJLBH, iLoginRYID, sLoginRYMC);
                    }
                    #endregion
                }
            }
            if (msg == "")
                iRJSH = 0;
            foreach (SKFS one in ZFFS)
            {
                query.SQL.Text = "select BJ_DZQDCZK,TYPE from ZFFS where ZFFSID =  " + one.iZFFSID;
                query.Open();
                if (query.FieldByName("BJ_DZQDCZK").AsInteger == 1 || query.FieldByName("TYPE").AsInteger == 10)
                {
                    bQD = false;
                    break;
                }
            }
            query.Close();
            if (bQD)
                StartDataQuery(out msg, query, serverTime);


            //iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //CrmLibProc.UpdateJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iMDID, fCKJE, "储值卡存款");
        }
        public override bool IsValidStartData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "SELECT x.* FROM MZK_SKJLSKMX x,zffs f where x.zffsid=f.zffsid and f.type=10 and x.jlbh=" + iJLBH;
            query.Open();
            if (!query.Eof)
            {
                msg = "该售卡单存在欠款，不可启动";
                return false;
            }
            else
                return true;
        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            query.SQL.Text = "update MZK_SKJL set QDSJ = :QDSJ,STATUS = :pStat";
            query.SQL.Add("where JLBH=:JLBH");
            query.ParamByName("JLBH").AsInteger = iJLBH;

            query.ParamByName("QDSJ").AsDateTime = serverTime;
            query.ParamByName("pStat").AsInteger = 2;
            query.ExecSQL();


            StringBuilder sql = new StringBuilder();
            #region
            List<CashCard> saleCardList = new List<CashCard>();

            sql.Length = 0;
            sql.Append("select a.CZKHM_BEGIN,a.MZJE,b.HYKTYPE,b.CZKHM,b.PDJE,b.YXQ,c.FS_YXQ,c.YXQCD ");
            sql.Append(" from MZK_SKJLKDITEM a, MZKCARD b, HYKDEF c ");
            sql.Append(" where a.HYKTYPE = b.HYKTYPE ");
            sql.Append("   and b.HYKTYPE = c.HYKTYPE ");
            sql.Append("   and a.JLBH = ").Append(iJLBH);
            sql.Append("   and b.CZKHM between a.CZKHM_BEGIN and a.CZKHM_END");
            sql.Append("   and length(b.CZKHM) = length(a.CZKHM_BEGIN) ");
            sql.Append("   and b.BGDDDM = '").Append(BGDDDM).Append("'");
            sql.Append("   and c.BJ_CZK = 1");
            sql.Append("   and b.SKJLBH = ").Append(iJLBH);
            sql.Append(" order by a.CZKHM_BEGIN");
            query.SQL.Clear();
            query.Params.Clear();
            query.SQL.Text = sql.ToString();
            query.Open();
            while (!query.Eof)
            {
                CashCard card = new CashCard();
                saleCardList.Add(card);
                card.CardCodeScope1 = query.FieldByName("CZKHM_BEGIN").AsString;
                card.ProcMoney = query.FieldByName("MZJE").AsDecimal;
                card.CardTypeId = query.FieldByName("HYKTYPE").AsInteger;
                card.CardCode = query.FieldByName("CZKHM").AsString;
                card.Bottom = query.FieldByName("PDJE").AsDecimal;
                card.ValidDateType = query.FieldByName("FS_YXQ").AsInteger;
                card.ValidDateLen = query.FieldByName("YXQCD").AsString;
                if (card.ValidDateType == 1)
                {
                    card.ValidDate = serverTime.AddYears(Convert.ToInt32(card.ValidDateLen.Substring(0, card.ValidDateLen.Length - 1)));
                }
                else
                {
                    card.ValidDate = query.FieldByName("YXQ").AsDateTime;
                }
                query.Next();
            }
            query.Close();

            foreach (CashCard one in saleCardList)
            {

                query.SQL.Text = "delete from MZKCARD where CZKHM=:CZKHM";
                query.ParamByName("CZKHM").AsString = one.CardCode;
                query.ExecSQL();


                query.SQL.Text = "update  MZKXX set STATUS=:STATUS WHERE HYK_NO=:HYK_NO";
                query.ParamByName("HYK_NO").AsString = one.CardCode;
                query.ParamByName("STATUS").AsInteger = 0;
                query.ExecSQL();

            }
            #endregion

        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (SKFS one in ZFFS)
            {
                if (one.fJE <= 0)
                {
                    msg = "收款方式" + one.sZFFSMC + "的金额小于0，不合法";
                    return false;
                }
            }
            if (YSZE != Get_SSJEALL_NEW())
            {
                msg = "应收金额与实收金额不等！";
                return false;
            }
            if (Get_SKSL_NEW() <= 0)
            {
                msg = "售卡数量不能小于等于0！";
                return false;
            }

            query.SQL.Text = "select CZKHM  from MZK_SKJLITEM A ";
            query.SQL.Add("where JLBH=:JLBH and not exists(select 1 from MZKCARD where SKJLBH=A.JLBH and CZKHM=A.CZKHM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.Open();
            while (!query.Eof)
            {
                msg += "卡号" + query.ParamByName("CZKHM").AsString + "已销售或卡号不存在！";
                query.Next();
            }
            if (msg != "")
            {
                return false;
            }
            query.SQL.Text = "select to_number(CUR_VAL) CURVAL from BFCONFIG where JLBH=520000126";
            query.Open();
            if (query.FieldByName("CURVAL").AsInteger <= SKSL && query.FieldByName("CURVAL").AsInteger != 0)
            {
                iRJSH = 1;
                msg = "售卡数量" + Convert.ToString(SKSL) + "超过了(或等于了)" + query.FieldByName("CURVAL").AsInteger.ToString() + "张，审核第二天才能生效！";
            }
            else
            {
                iRJSH = 0;
            }

            query.SQL.Clear();
            query.SQL.Text = " select to_number(CUR_VAL) CURVAL from BFCONFIG where JLBH=520000130";
            query.Open();
            if (!query.IsEmpty)
            {
                if (query.FieldByName("CURVAL").AsInteger == 1)
                {
                    msg = "该售卡单缴款后才能开卡";
                    iJKSH = 1;
                }
            }





            aHYID = SeqGenerator.GetSeq("MZKXX", SKSL, "CRMDBMZK");
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_SKJL;MZK_SKJLITEM;MZK_SKJLSKMX;MZK_SKJLKDITEM;MZK_SKJLZKITEM;MZK_SKJLZJFITEM", "JLBH", iJLBH, "ZXR", "CRMDBMZK");
            foreach (kdmx one in SKKD)
            {

                query.SQL.Text = "update MZKCARD A set A.SKJLBH=NULL";
                query.SQL.Add("where A.CZKHM between :CZKHM_BEGIN and :CZKHM_END");
                query.SQL.Add("and length(A.CZKHM) = length( :CZKHM_BEGIN )");
                query.SQL.Add("and BGDDDM = :BGDDDM");
                query.SQL.Add("and exists(select 1 from HYKDEF where HYKTYPE=A.HYKTYPE and BJ_CZK=1)");
                query.SQL.Add(" and A.SKJLBH=:SKJLBH  ");
                query.ParamByName("SKJLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM_BEGIN").AsString = one.sCZKHM_BEGIN;
                query.ParamByName("CZKHM_END").AsString = one.sCZKHM_END;
                query.ParamByName("BGDDDM").AsString = BGDDDM;
                if (query.ExecSQL() == 0)
                {
                    msg = "更新SKJLBH失败！";
                }

            }
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
            query.SQL.Add(" where W.BGDDDM=B.BGDDDM and W.MDID=M.MDID and W.KHID=A.KHID(+) and W.FS<2");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*";
                query.SQL.Add(" from MZK_SKJLKDITEM I");
                query.SQL.Add(" where I.JLBH=" + iJLBH);
                query.Open();
                HYKDEF hyk = new HYKDEF();
                while (!query.Eof)
                {
                    kdmx item = new kdmx();
                    ((MZKGL_MZKFS)lst[0]).SKKD.Add(item);
                    item.iSKSL = query.FieldByName("SKSL").AsInteger;
                    item.fMZJE = query.FieldByName("MZJE").AsDecimal;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger.ToString();
                    hyk.iHYKTYPE = Convert.ToInt32(item.iHYKTYPE);
                    //CrmLibProc.GetHYKDEF(out msg, ref hyk);
                    //item.iRANDOM_LEN = hyk.iRANDOM_LEN;
                    //if (item.iRANDOM_LEN != 0)
                    // {
                    item.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString;
                    item.sCZKHM_END = query.FieldByName("CZKHM_END").AsString;
                    //}
                    query.Next();
                }

                query.SQL.Text = "select I.*,S.ZFFSMC";
                query.SQL.Add(" from MZK_SKJLSKMX I,ZFFS S");
                query.SQL.Add(" where I.ZFFSID=S.ZFFSID and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    SKFS item = new SKFS();
                    ((MZKGL_MZKFS)lst[0]).ZFFS.Add(item);
                    item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                    item.fJE = query.FieldByName("JE").AsDecimal;
                    item.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    //item.sJYBH = query.FieldByName("JYBH").AsString;
                    query.Next();
                }

                query.SQL.Text = "select I.*,S.YHQMC";
                query.SQL.Add(" from MZK_SKJLZKITEM I,YHQDEF S");
                query.SQL.Add(" where I.YHQLX=S.YHQID and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    ZQITEM item = new ZQITEM();
                    ((MZKGL_MZKFS)lst[0]).ZQMX.Add(item);
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.iYHQLX = query.FieldByName("YHQLX").AsInteger;
                    item.iYXQTS = query.FieldByName("YXQTS").AsInteger;
                    item.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    query.Next();
                }

                query.SQL.Text = "select I.*";
                query.SQL.Add(" from MZK_SKJLZJFITEM I");
                query.SQL.Add(" where I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    ZFITEM item = new ZFITEM();
                    ((MZKGL_MZKFS)lst[0]).ZFMX.Add(item);
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.fZSJF = query.FieldByName("ZSJF").AsFloat;
                    query.Next();
                }

                Get_FSZQ(query, ((MZKGL_MZKFS)lst[0]).findkdxx, ((MZKGL_MZKFS)lst[0]).YSZE);
                Get_FSZJF(query, ((MZKGL_MZKFS)lst[0]).findkdxx, ((MZKGL_MZKFS)lst[0]).YSZE);

            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKFS bill = new MZKGL_MZKFS();

            bill.iJLBH = query.FieldByName("JLBH").AsInteger;
            bill.FS = query.FieldByName("FS").AsInteger;
            bill.SKSL = query.FieldByName("SKSL").AsInteger;
            bill.YSZE = query.FieldByName("YSZE").AsDecimal;
            bill.ZKL = query.FieldByName("ZKL").AsFloat;
            bill.ZKJE = query.FieldByName("ZKJE").AsDecimal;
            bill.SSJE = query.FieldByName("SSJE").AsDecimal;
            bill.BGDDDM = query.FieldByName("BGDDDM").AsString;
            bill.ZY = query.FieldByName("ZY").AsString;
            bill.iDJR = query.FieldByName("DJR").AsInteger;
            bill.sDJRMC = query.FieldByName("DJRMC").AsString;
            bill.dDJSJ = query.FieldByName("DJSJ").AsDateTime.ToString();
            bill.STATUS = query.FieldByName("STATUS").AsInteger;
            bill.KHID = query.FieldByName("KHID").AsInteger;
            bill.DZKFJE = query.FieldByName("DZKFJE").AsInteger;
            bill.ZSJE = query.FieldByName("ZSJE").AsDecimal;
            bill.LXR = query.FieldByName("LXR").AsString;
            bill.SJZSJE = query.FieldByName("SJZSJE").AsDecimal;
            bill.YWY = query.FieldByName("YWY").AsInteger;
            bill.ZXR = query.FieldByName("ZXR").AsInteger;
            bill.ZXRMC = query.FieldByName("ZXRMC").AsString;
            bill.ZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            bill.KHMC = query.FieldByName("KHMC").AsString;
            bill.LXRXM = query.FieldByName("LXRXM").AsString;
            bill.LXRSJ = query.FieldByName("LXRSJ").AsString;
            bill.BGDDMC = query.FieldByName("BGDDMC").AsString;
            bill.MDMC = query.FieldByName("MDMC").AsString;
            bill.ZSJF = query.FieldByName("ZSJF").AsFloat;
            bill.SJZSJF = query.FieldByName("SJZSJF").AsFloat;
            bill.dQDSJ = FormatUtils.DateToString(query.FieldByName("QDSJ").AsDateTime);
            return bill;
        }
    }
}



