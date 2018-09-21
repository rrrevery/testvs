using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Security.Cryptography;

using Newtonsoft.Json;
using BF.Pub;
using BF.CrmProc.GTPT;
using BF.CrmProc.HYKGL;
using BF.CrmProc.HYXF;
using System.Configuration;

namespace BF.CrmProc
{
    public static class CrmLibProc
    {
        public static List<MenuPermit> MenuPermits = new List<MenuPermit>();
        public static void GenMenuPermit(CyQuery query = null)
        {
            bool bNoQry = query == null;
            if (bNoQry)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                query = new CyQuery(conn);
            }
            query.Close();
            query.SQL.Text = "select distinct * from(select Q.PERSON_ID,Q.MSG_ID,Q.BJ_FJQX1,BJ_FJQX2,BJ_FJQX3,BJ_FJQX4 from BFPUB.XTCZY_QX Q";
            query.SQL.Add("union select X.PERSON_ID,Q.MSG_ID,Q.BJ_FJQX1,BJ_FJQX2,BJ_FJQX3,BJ_FJQX4 from BFPUB.XTCZYGRP X,BFPUB.CZYGROUP_QX Q where X.GROUPID=Q.ID)");
            query.Open();
            MenuPermits.Clear();
            while (!query.Eof)
            {
                MenuPermit one = new MenuPermit();
                one.iPERSON_ID = query.FieldByName("PERSON_ID").AsInteger;
                one.iMSG_ID = query.FieldByName("MSG_ID").AsInteger;
                one.iBJ_FJQX1 = query.FieldByName("BJ_FJQX1").AsInteger;
                one.iBJ_FJQX2 = query.FieldByName("BJ_FJQX2").AsInteger;
                one.iBJ_FJQX3 = query.FieldByName("BJ_FJQX3").AsInteger;
                one.iBJ_FJQX4 = query.FieldByName("BJ_FJQX4").AsInteger;
                MenuPermits.Add(one);
                query.Next();
            }
            query.Close();
            if (bNoQry)
                query.Connection.Close();
        }
        /// <summary>
        /// 门店权限
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pRYID"></param>
        /// <param name="pTB_NAME">表名</param>
        /// <param name="filedName">列名</param>
        public static void CheckMDQX(CyQuery query, int pRYID, string pTB_NAME, string filedName)
        {
            query.SQL.Add(" and (" + pTB_NAME + "." + filedName + "=-1 ");
            query.SQL.Add("   or " + pTB_NAME + "." + filedName + "  is null or exists(select * from XTCZY_MDQX X where X.PERSON_ID=:RYID and X.MDID=" + pTB_NAME + "." + filedName + ")");
            query.SQL.Add("  or exists(select * from CZYGROUP_MDQX X,XTCZYGRP G where G.PERSON_ID=:RYID and X.ID=G.GROUPID and X.MDID=" + pTB_NAME + "." + filedName + "))");
            query.ParamByName("RYID").AsInteger = pRYID;
        }

        public static string GetWXHYXX2(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();



            query.SQL.Text = "select H.HYID,H.HYKTYPE,D.HYKNAME,X.LX,X.DJSJ,X.OPENID,X.UNIONID,X.PUBLICID,W.BINDCODE";
            query.SQL.Add(" from HYK_HYXX H,HYKDEF D,WX_BINDCARDJL X ,WX_HYKHYXX W ");
            query.SQL.Add(" where   H.HYKTYPE=D.HYKTYPE   and W.UNIONID=X.UNIONID  and  X.HYID=H.HYID and H.HYK_NO= :HYKNO");
            query.ParamByName("HYKNO").AsString = param.sHYK_NO;
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.iLX = query.FieldByName("LX").AsInteger;
                obj.sUNIONID = query.FieldByName("UNIONID").AsString;
                obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                obj.sOPENID = query.FieldByName("OPENID").AsString;
                obj.sBINDCODE = query.FieldByName("BINDCODE").AsString;
            }
            else
            {
                msg = CrmLibStrings.msgHYXXNotFound;
                return null;
            }
            query.Close();




            return JsonConvert.SerializeObject(obj);

        }
        public static string FillWT_new(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<WXGZ> lst = new List<WXGZ>();
            query.SQL.Text = " select ASKID,ASK from WX_ASK where STATUS=1 and TYPE=" + obj.iTYPE + "AND PUBLICID=" + obj.iPUBLICID;
            query.SQL.Add("order by ASKID");
            query.Open();
            while (!query.Eof)
            {
                WXGZ _item = new WXGZ();
                _item.iID = query.FieldByName("ASKID").AsInteger;
                _item.sASK = query.FieldByName("ASK").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillKSJHYKTYPE(out string msg, CyQuery query, CRMLIBASHX param)// int pHYKTYPE, int pSJ)
        {
            msg = string.Empty;
            List<HYKDEF> lst = new List<HYKDEF>();
            query.SQL.Text = " select A.*,B.BJ_XFSJ from HYKDEF A,HYKKZDEF B,HYKDJDEF C,";
            query.SQL.Add(" (select A.HYKKZID,A.HYKJCID from HYKDEF A,HYKKZDEF B,HYKDJDEF C  WHERE A.HYKKZID=B.HYKKZID AND A.HYKJCID=C.HYKJCID  AND A.HYKKZID=C.HYKKZID AND A.HYKTYPE=:HYKTYPE) D ");
            query.SQL.Add("  WHERE A.HYKKZID=B.HYKKZID");
            query.SQL.Add("   AND A.HYKJCID=C.HYKJCID");
            query.SQL.Add("    AND A.HYKKZID=C.HYKKZID");
            query.SQL.Add("    AND A.HYKKZID=D.HYKKZID");
            if (param.iSJ == 0)
            {
                query.SQL.Add("     AND A.HYKJCID>D.HYKJCID");
            }
            if (param.iSJ == 1)
            {
                query.SQL.Add("     AND A.HYKJCID<D.HYKJCID");
            }
            query.ParamByName("HYKTYPE").AsInteger = param.iHYKTYPE;
            //if (query.IsEmpty)
            //{                   
            //}
            query.Open();
            while (!query.Eof)
            {
                HYKDEF item = new HYKDEF();
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item.iHYKKZID = query.FieldByName("HYKKZID").AsInteger;
                item.iBJ_XFSJ = query.FieldByName("BJ_XFSJ").AsInteger;
                //item.sHYKDM = item.iHYKTYPE < 10 ? ("0" + item.iHYKTYPE) : item.iHYKTYPE.ToString();
                //item.sHYKPDM = item.iHYKKZID < 10 ? ("0" + item.iHYKKZID) : item.iHYKKZID.ToString();
                lst.Add(item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        /// <summary>
        /// 部门权限
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pRYID"></param>
        /// <param name="pTB_NAME"></param>
        public static void CheckBMQX(CyQuery query, int pRYID, string pTB_NAME)
        {
            query.SQL.Add(" and  ( exists(select 1 from XTCZY_BMCZQX X where " + pTB_NAME + ".BMDM like X.BMDM||'%' and X.PERSON_ID=" + pRYID + ")");
            query.SQL.Add(" or  exists(select 1 from CZYGROUP_BMCZQX Q,XTCZYGRP G where " + pTB_NAME + ".BMDM LIKE Q.BMDM||'%' and Q.ID=G.GROUPID and G.PERSON_ID=" + pRYID + ")");
            query.SQL.Add(" or  exists(select 1 from XTCZY_BMCZQX X where X.PERSON_ID=" + pRYID + " and X.BMDM=' ')");
            query.SQL.Add(" or  exists(select 1 from CZYGROUP_BMCZQX Q,XTCZYGRP G where Q.ID=G.GROUPID and Q.BMDM=' ' and  G.PERSON_ID=" + pRYID + ")  )");
            //query.SQL.Add(" order by jlbh desc");
            //query.ParamByName("RYID").AsInteger = pRYID;
            //query.ParamByName("RYID1").AsInteger = pRYID;
            //query.ParamByName("RYID2").AsInteger = pRYID;
            //query.ParamByName("RYID3").AsInteger = pRYID;

        }
        public static string GetWXHYK_NOData(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();



            query.SQL.Text = "select A.* from HYK_HYXX A  where A.HYK_NO= ";
            query.SQL.Add(" '" + param.sHYK_NO + "'");
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iHYID = query.FieldByName("HYID").AsInteger;
            }
            else
            {
                msg = CrmLibStrings.msgHYXXNotFound;
                return null;
            }
            query.Close();




            return JsonConvert.SerializeObject(obj);

        }

        private static string GetCZK_DKHDAListhyid(out string msg, string hyk_no)
        {
            msg = "";
            string tp_restart = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_p = "";
                    tp_p += " select HYID";
                    tp_p += " from HYK_HYXX A WHERE HYK_NO='" + hyk_no + "'";
                    query.SQL.Text = tp_p;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        tp_restart = query.FieldByName("HYID").AsInteger.ToString();
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

            return tp_restart;
        }

        public static string GetCZK_DKHDAList(out string msg, CZK_KHDA obj)
        {
            msg = string.Empty;
            string tp_hykhyid = "";
            if (obj.sHYKNO != null && obj.sHYKNO != "")
            {
                tp_hykhyid = GetCZK_DKHDAListhyid(out msg, obj.sHYKNO);
            }
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_p = "";
                    //tp_p += " select A.KHID AS KHID,MDMC,KHMC,KHDZ,C.GKRRYID AS GKRRYID,GKRYNAME,GKRYSJHM,DKHLX,CASE WHEN A.DKHLX=0 THEN '个人客户' ELSE '企业客户' END AS DKHLXMC, B.HYKHM AS HYKHM ";
                    //tp_p += " from CZK_KHDA A,KH_GKRY B,KH_GKRYXX C,MDDY D  ";
                    //tp_p += " where A.KHID=B.KHID AND B.GKRRYID=C.GKRRYID AND A.MDID=D.MDID";
                    //tp_p += " AND NVL(C.BJ_TY,0)<>1 AND NVL(B.BJ_TY,0)<>1   ";
                    tp_p += "   select A.*,D.MDMC  from MZK_KHDA A,MDDY D   where A.MDID=D.MDID ";
                    //and A.DKHLX=1
                    //if (tp_hykhyid != "")
                    //{
                    //    tp_p += "   AND C.HYID = " + tp_hykhyid + "";
                    //}
                    if (obj.sKHMC != "")
                    {
                        tp_p += " AND A.KHMC LIKE '%" + obj.sKHMC + "%'";
                    }
                    //if (obj.sHYKHM != "")
                    //{
                    //    tp_p += "   AND B.HYKHM LIKE '%" + obj.sHYKHM + "%'";
                    //}
                    if (obj.sGKRYSJHM != "")
                    {
                        tp_p += " AND A.LXRSJ LIKE '%" + obj.sGKRYSJHM + "%'";
                    }
                    query.SQL.Text = tp_p;
                    query.Open();
                    while (!query.Eof)
                    {
                        CZK_KHDA item = new CZK_KHDA();
                        item.iKHID = query.FieldByName("KHID").AsInteger;
                        item.sHYKNO = obj.sHYKNO;
                        item.sKHMC = query.FieldByName("KHMC").AsString;
                        item.sMD = query.FieldByName("MDMC").AsString;
                        item.sKHDZ = query.FieldByName("KHDZ").AsString;
                        //   item.sGKRRYID = query.FieldByName("GKRRYID").AsInteger.ToString();
                        item.sGKRYNAME = query.FieldByName("LXRXM").AsString;
                        item.sGKRYSJHM = query.FieldByName("LXRSJ").AsString;
                        //    item.sDKHLXMC = query.FieldByName("DKHLXMC").AsString;
                        //    item.sHYKHM = query.FieldByName("HYKHM").AsString;
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

        public static string GetCZK_KFZYList(out string msg, CZK_KFZY obj)
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * ";
                    query.SQL.Add(" from KFZYDEF A,HYK_BGDD B WHERE A.mdid=B.mdid and bgdddm=" + obj.sBGDDDM + " ");

                    if (obj.iKHID != 0)
                    {
                        query.SQL.Add("   and A.ZYID=" + obj.iKHID);
                    }
                    if (obj.sKHDM != "")
                    {
                        query.SQL.Add("   and A.ZYDM='" + obj.sKHDM + "'");
                    }
                    if (obj.sKHMC != "")
                    {
                        query.SQL.Add(" and A.ZYMC like '%" + obj.sKHMC + "%'");
                    }


                    query.Open();
                    while (!query.Eof)
                    {
                        CZK_KFZY item = new CZK_KFZY();
                        //item.iJLBH = query.FieldByName("ZYID").AsInteger;
                        item.iKHID = query.FieldByName("ZYID").AsInteger;
                        item.sKHDM = query.FieldByName("ZYDM").AsString;
                        item.sKHMC = query.FieldByName("ZYMC").AsString;
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

        public static string FillQKTree(out string msg)
        {

            List<object> nodes = new List<object>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
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
                    //此SQL只筛选出与区块相关的信息
                    query.SQL.Text = " SELECT (select MAX(QYDM) FROM HYK_HYQYDY B WHERE A.QYDM!=B.QYDM AND A.QYDM LIKE B.QYDM||'%') PQKDM,A.QYID QKID,A.QYDM QKDM,A.QYMC QKMC,0 BJ_QK FROM HYK_HYQYDY A";
                    query.SQL.Text += "  where exists( ";
                    query.SQL.Text += " select Q.QYDM PQYDM from HYK_QYQKDY K,HYK_HYQYDY Q  where K.QYID=Q.QYID and Q.QYDM like A.QYDM||'%' ";
                    query.SQL.Text += ") ";
                    query.SQL.Text += " union ";
                    query.SQL.Text += "select Q.QYDM PQKDM,K.QKID,K.QKDM,K.QKMC,1 BJ_QK from HYK_QYQKDY K,HYK_HYQYDY Q where K.QYID=Q.QYID";
                    query.Open();

                    while (!query.Eof)
                    {
                        QKDY node = new QKDY();
                        node.sPQKDM = query.FieldByName("PQKDM").AsString;
                        node.iQKID = query.FieldByName("QKID").AsInteger;
                        node.sQKDM = query.FieldByName("QKDM").AsString;
                        node.sQKMC = query.FieldByName("QKMC").AsString;
                        node.iBJ_QK = query.FieldByName("BJ_QK").AsInteger;
                        nodes.Add(node);
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
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return JsonConvert.SerializeObject(nodes);
        }
        public static string Find_YHQ_TS(out string msg, string yhqid)
        {
            string tp_str = "1";
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
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
                    query.SQL.Text = " select * from YHQDEF where yhqid=" + yhqid + " order by YHQID ";
                    query.Open();

                    if (!query.IsEmpty)
                    {
                        string tp_ts = query.FieldByName("BJ_TS").AsInteger.ToString();
                        string tp_dzyhq = query.FieldByName("BJ_DZYHQ").AsInteger.ToString();
                        //BJ_DZYHQ=1电子优惠券
                        if (tp_dzyhq == "1") { tp_str = "0"; }
                        //纸券
                        if (tp_dzyhq == "0" && tp_ts == "0") { tp_str = "1"; }


                    }
                    query.Close();
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
            return tp_str;


        }

        //zhangyu2014 0820 add----------------------------------------------------------------------
        //
        public static string GetKHDAETHDList(out string msg, GKDAQZ obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_sql = "";
                    tp_sql += " select * from HYK_GKDA_ETHD where hyid=" + obj.iHYID;
                    query.SQL.Text = tp_sql;
                    query.Open();
                    while (!query.Eof)
                    {
                        GKDAQZ item = new GKDAQZ();
                        item.iQZID = query.FieldByName("HDID").AsInteger;
                        item.iHYID = query.FieldByName("HDID").AsInteger;
                        item.sQZDM = query.FieldByName("HDDM").AsString;
                        item.sQZMC = query.FieldByName("HDMC").AsString;
                        item.sBZ = query.FieldByName("BZ").AsString;

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
        //--------------------------------------------end add---------------------------------------

        public static string FillBGDDTree(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            List<BGDD> lst = new List<BGDD>();
            query.SQL.Text = "select (select max(BGDDDM) from HYK_BGDD B where A.BGDDDM like B.BGDDDM||'%' and A.BGDDDM<>B.BGDDDM) PBGDDDM,";
            query.SQL.Add(" A.*,M.MDMC from HYK_BGDD A,MDDY M where 1=1 ");
            if (obj.iSK == 1)
                query.SQL.Add(" and XS_BJ=1");
            if (obj.iZK == 1)
                query.SQL.Add(" and ZK_BJ=1");
            if (true)
                query.SQL.Add(" and nvl(TY_BJ,0)=0");
            if (obj.iMDID > 0)
            {
                query.SQL.Add(" and (A.MDID=:MDID or exists(select * from HYK_BGDD C where A.BGDDDM like C.BGDDDM||'%' and C.MDID=:MDID))");
                query.ParamByName("MDID").AsInteger = obj.iMDID;
            }
            if (obj.iQX == 1 && obj.iRYID > 0)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_BGDDQX X where (X.BGDDDM=' ' or (A.BGDDDM like X.BGDDDM||'%' or X.BGDDDM like A.BGDDDM||'%')) and X.PERSON_ID=:RYID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where (Q.BGDDDM=' ' or (A.BGDDDM LIKE Q.BGDDDM||'%' or Q.BGDDDM LIKE A.BGDDDM||'%')) and Q.ID=G.GROUPID and G.PERSON_ID=:RYID))");
                //query.SQL.Add(" or exists (select 1 from XTCZY_BGDDQX X where X.PERSON_ID=:RYID and X.BGDDDM=' ')");
                //query.SQL.Add(" or exists (select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where Q.ID=G.GROUPID and Q.BGDDDM=' ' and  G.PERSON_ID=:RYID))");
                query.ParamByName("RYID").AsInteger = obj.iRYID;
            }
            query.SQL.Add(" and A.MDID = M.MDID(+)");
            query.SQL.Add(" order by BGDDDM");
            // if (query.SQL.Text.IndexOf(":RYID") >= 0)
            // {
            //     query.ParamByName("RYID").AsInteger = pRYID;
            //  }
            query.Open();
            while (!query.Eof)
            {
                BGDD one = new BGDD();
                one.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                one.sPBGDDDM = query.FieldByName("PBGDDDM").AsString;
                one.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                one.sBGDDQC = one.sBGDDDM + " " + one.sBGDDMC;
                one.bMJBJ = query.FieldByName("MJBJ").AsInteger;
                one.bXS_BJ = query.FieldByName("XS_BJ").AsInteger;
                one.bZK_BJ = query.FieldByName("ZK_BJ").AsInteger;
                one.bTY_BJ = query.FieldByName("TY_BJ").AsInteger;
                one.iMDID = query.FieldByName("MDID").AsInteger;
                one.sMDMC = query.FieldByName("MDMC").AsString;
                lst.Add(one);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillSHZFFSTree(out string msg, string sSHDM)
        {
            msg = string.Empty;
            List<SHZFFS> lst = new List<SHZFFS>();
            //string sNodes = "[";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = "  select (select max(zffsdm) from shzffs B where to_char(A.zffsdm) like B.zffsdm||'%'";
                query.SQL.Add("   and A.zffsdm<>B.zffsdm and length(B.ZFFSDM)>1) PZFFSDM,A.ZFFSDM,A.ZFFSMC,A.TYPECODE");
                query.SQL.Add("  ,A.MJBJ,A.BJ_JF,A.BJ_FQ,A.YHQID,A.BJ_MBJZ,Y.YHQMC,A.SHZFFSID,A.TYPE");
                query.SQL.Add(" from shzffs A,YHQDEF Y");
                query.SQL.Add("  where  A.YHQID=Y.YHQID(+) and (A.TYPE=4 or A.TYPECODE is not null) and  A.SHDM='" + sSHDM + "'");
                query.SQL.Add("  order by A.ZFFSDM");
                query.Open();
                while (!query.Eof)
                {
                    SHZFFS obj = new SHZFFS();
                    obj.sPZFFSDM = query.FieldByName("PZFFSDM").AsString;
                    obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                    obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    obj.sTYPECODE = query.FieldByName("TYPECODE").AsString;
                    obj.iMJBJ = query.FieldByName("MJBJ").AsInteger;
                    obj.iBJ_JF = query.FieldByName("BJ_JF").AsInteger;
                    obj.iBJ_FQ = query.FieldByName("BJ_FQ").AsInteger;
                    obj.iBJ_MBJZ = query.FieldByName("BJ_MBJZ").AsInteger;
                    obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                    obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                    obj.iSHZFFSID = query.FieldByName("SHZFFSID").AsInteger;
                    obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);

        }

        public static string FillWXCDDYTree(out string msg, CyQuery query, CRMLIBASHX param)//int pRYID, int pMDID, string tURL, int tPUBLICID)
        {
            msg = string.Empty;
            List<WXCD> lst = new List<WXCD>();
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            if (DbSystemName == "ORACLE")
            {
                query.SQL.Text = " select ( select MAX(B.DM) from WX_MENU B WHERE A.DM LIKE B.DM|| '%' AND  A.DM <> B.DM ) PDM";
                query.SQL.Add("  ,(A.DM||' '||A.NAME) sDMQC,A.* FROM WX_MENU A  WHERE 1=1 ");
                query.SQL.Add("and A.PUBLICID=" + param.iPUBLICID);
                query.SQL.Add(" order by DM");
            }
            else if (DbSystemName == "SYBASE")
            {
                query.SQL.Text = " select ( select MAX(B.DM) from WX_MENU B WHERE A.DM LIKE B.DM+ '%' AND  A.DM <> B.DM ) PDM";
                query.SQL.Add("  ,(A.DM+' '+A.NAME) sDMQC,A.* FROM WX_MENU A WHERE  1=1 ");
                query.SQL.Add(" and A.PUBLICID=" + param.iPUBLICID);
                query.SQL.Add(" order by DM");

            }
            query.Open();
            while (!query.Eof)
            {
                WXCD obj = new WXCD();
                obj.sDM = query.FieldByName("DM").AsString;
                obj.sPDM = query.FieldByName("PDM").AsString;
                obj.sNAME = query.FieldByName("NAME").AsString;
                obj.sDMQC = obj.sDM + " " + obj.sNAME;
                obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                obj.iASKID = query.FieldByName("ASKID").AsInteger;
                obj.sNBDM = query.FieldByName("NBDM").AsString;
                obj.sURL = query.FieldByName("URL").AsString;
                obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillMDBGDDTree(out string msg, int pRYID, int pMDID, bool bXS = false, bool bZK = false, bool bAll = false, bool bQX = true)
        {
            msg = string.Empty;
            List<BGDD> lst = new List<BGDD>();
            //string sNodes = "[";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                //商户
                query.Close();
                query.SQL.Text = "select A.* from SHDY A where 1=1 ";
                query.SQL.Add(" order by A.SHDM");
                query.Open();
                while (!query.Eof)
                {
                    BGDD obj = new BGDD();
                    obj.sBGDDDM = query.FieldByName("SHDM").AsString;
                    obj.sPBGDDDM = "";
                    obj.sBGDDMC = query.FieldByName("SHMC").AsString;
                    obj.sBGDDQC = obj.sBGDDDM + " " + obj.sBGDDMC;
                    obj.bMJBJ = 0;
                    obj.bXS_BJ = 1;
                    obj.bZK_BJ = 1;
                    obj.bTY_BJ = 0;
                    obj.iMDID = 0;
                    lst.Add(obj);
                    query.Next();
                }
                //门店
                query.Close();
                query.SQL.Text = "select A.* from MDDY A where 1=1 ";
                query.SQL.Add(" order by A.SHDM,A.MDDM");
                query.Open();
                while (!query.Eof)
                {
                    BGDD obj = new BGDD();
                    obj.sBGDDDM = query.FieldByName("MDDM").AsString;
                    obj.sPBGDDDM = query.FieldByName("SHDM").AsString;
                    obj.sBGDDMC = query.FieldByName("MDMC").AsString;
                    obj.sBGDDQC = obj.sBGDDDM + " " + obj.sBGDDMC;
                    obj.bMJBJ = 0;
                    obj.bXS_BJ = 1;
                    obj.bZK_BJ = 1;
                    obj.bTY_BJ = 0;
                    obj.iMDID = query.FieldByName("MDID").AsInteger;
                    lst.Add(obj);
                    query.Next();
                }
                //保管地点
                query.Close();
                query.SQL.Text = "select (select max(BGDDDM) from HYK_BGDD B where A.BGDDDM like B.BGDDDM||'%' and A.BGDDDM<>B.BGDDDM) PBGDDDM,";
                query.SQL.Add(" A.*,M.MDDM from HYK_BGDD A,MDDY M where A.MDID=M.MDID ");
                if (bXS)
                    query.SQL.Add(" and XS_BJ=1");
                if (bZK)
                    query.SQL.Add(" and ZK_BJ=1");
                if (!bAll)
                    query.SQL.Add(" and nvl(TY_BJ,0)=0");
                if (pMDID > 0)
                {
                    query.SQL.Add(" and (MDID=:MDID or exists(select * from HYK_BGDD C where A.BGDDDM like C.BGDDDM||'%' and C.MDID=:MDID))");
                    query.ParamByName("MDID").AsInteger = pMDID;
                }
                if (bQX && pRYID > 0)
                {
                    query.SQL.Add(" and (exists(select 1 from XTCZY_BGDDQX X where A.BGDDDM like X.BGDDDM||'%' and X.PERSON_ID=:RYID)");
                    query.SQL.Add(" or exists(select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where A.BGDDDM LIKE Q.BGDDDM||'%' and Q.ID=G.GROUPID and G.PERSON_ID=:RYID)");
                    query.SQL.Add(" or exists (select 1 from XTCZY_BGDDQX X where X.PERSON_ID=:RYID and X.BGDDDM='')");
                    query.SQL.Add(" or exists (select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where Q.ID=G.GROUPID and Q.BGDDDM='' and  G.PERSON_ID=:RYID))");
                    query.ParamByName("RYID").AsInteger = pRYID;
                }
                query.SQL.Add(" order by BGDDDM");
                query.Open();
                while (!query.Eof)
                {
                    BGDD obj = new BGDD();
                    obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                    obj.sPBGDDDM = query.FieldByName("PBGDDDM").AsString;
                    if (obj.sPBGDDDM == "")
                        obj.sPBGDDDM = query.FieldByName("MDDM").AsString;
                    obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                    obj.sBGDDQC = obj.sBGDDDM + " " + obj.sBGDDMC;
                    obj.bMJBJ = query.FieldByName("MJBJ").AsInteger;
                    obj.bXS_BJ = query.FieldByName("XS_BJ").AsInteger;
                    obj.bZK_BJ = query.FieldByName("ZK_BJ").AsInteger;
                    obj.bTY_BJ = query.FieldByName("TY_BJ").AsInteger;
                    obj.iMDID = query.FieldByName("MDID").AsInteger;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillCXHDTree(out string msg, int pRYID, bool bQX = true)
        {
            msg = string.Empty;
            List<CXHD> lst = new List<CXHD>();
            //string sNodes = "[";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                //query.SQL.Text = "select (select max(CXDM) from CXHDDEF B where A.CXDM like B.CXDM||'%' and A.CXDM<>B.CXDM) PCXDM,";
                //query.SQL.Add(" A.* from CXHDDEF A where 1=1 ");
                query.SQL.Text = "select * from CXHDDEF A where A.CXID is not null ";
                if (bQX && pRYID > 0)
                {
                }
                //query.SQL.Add(" order by CXDM");
                query.Open();
                while (!query.Eof)
                {
                    CXHD obj = new CXHD();
                    //obj.sCXDM = query.FieldByName("CXDM").AsString;
                    //obj.sPCXDM = query.FieldByName("PCXDM").AsString;
                    obj.sCXZT = query.FieldByName("CXZT").AsString;
                    obj.iCXID = query.FieldByName("CXID").AsInteger;
                    obj.iCXHDBH = query.FieldByName("CXHDBH").AsInteger;
                    //obj.sCXQC = obj.sCXDM + " " + obj.sCXZT;
                    obj.sSHDM = query.FieldByName("SHDM").AsString;
                    obj.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
                    obj.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
                    obj.iNIAN = query.FieldByName("NIAN").AsInteger;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillJHDWTree(out string msg, bool bXS = false, bool bZK = false)
        {
            msg = string.Empty;
            List<LPJHDW> lst = new List<LPJHDW>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                // query.SQL.Text = "select ( select MAX(B.JHDWDM) from JHDWDEF B WHERE A.JHDWDM LIKE B.JHDWDM|| '%'  AND A.JHDWDM <> B.JHDWDM) PJHDWDM,";
                // query.SQL.Add("(A.JHDWDM||' '||A.JHDWMC) JHDWQC,A.* from JHDWDEF A where 1=1");
                //if (bXS)
                //    query.SQL.Add("and XS_BJ=1");
                //if (bZK)
                //    query.SQL.Add("and ZK_BJ=1");
                query.SQL.Text = "select ( select MAX(B.JHDWDM) from JHDWDEF B WHERE A.JHDWDM LIKE B.JHDWDM|| '%'  AND A.JHDWDM <> B.JHDWDM) PJHDWDM,";
                query.SQL.Add("  (A.JHDWDM||' '||A.JHDWMC) JHDWQC,A.*,M.MDMC from JHDWDEF A ,MDDY M where A.MDID=M.MDID(+)");
                query.SQL.Add("order by JHDWDM");
                query.Open();
                while (!query.Eof)
                {
                    LPJHDW _item = new LPJHDW();
                    _item.iJLBH = query.FieldByName("JHDWID").AsInteger;
                    _item.sJHDWDM = query.FieldByName("JHDWDM").AsString;
                    _item.sJHDWMC = query.FieldByName("JHDWMC").AsString;
                    _item.sPJHDWDM = query.FieldByName("PJHDWDM").AsString;
                    _item.sJHDWQC = query.FieldByName("JHDWQC").AsString;
                    _item.iBJ_KTC = query.FieldByName("BJ_KTC").AsInteger;
                    _item.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                    _item.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                    _item.iMDID = query.FieldByName("MDID").AsInteger;
                    _item.sMDMC = query.FieldByName("MDMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillHYKTYPETree(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<HYKDEF> lst = new List<HYKDEF>();
            //string sNodes = "[";
            switch (obj.iMODE)
            {
                case 1:
                    sql = " and BJ_CZK=0";
                    break;
                case 2:
                    sql = " and BJ_CZK=1";
                    break;
                case 3:
                    sql = " and BJ_CZZH=1";
                    break;
            }
            query.SQL.Text = "select HYKKZID ID,HYKKZNAME NAME,0 PID,0.0 KFJE,BJ_XFSJ,ZFBJ from HYKKZDEF Z where 1=1 " + sql;
            if (obj.iQX == 1 && obj.iRYID > 0)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_HYLXQX X,HYKDEF D where D.HYKTYPE=X.HYKTYPE and D.HYKKZID=Z.HYKKZID and X.PERSON_ID=:RYID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_HYLXQX X,XTCZYGRP G,HYKDEF D where D.HYKTYPE=X.HYKTYPE and X.ID=G.GROUPID and D.HYKKZID=Z.HYKKZID and G.PERSON_ID=:RYID))");
                query.ParamByName("RYID").AsInteger = obj.iRYID;
            }
            query.SQL.Add(" union select HYKTYPE,HYKNAME,HYKKZID,KFJE,");
            query.SQL.Add("(select BJ_XFSJ from HYKKZDEF Z where Z.HYKKZID=D.HYKKZID) BJ_XFSJ,ZFBJ from HYKDEF D where 1=1 " + sql);
            if (obj.iQX == 1 && obj.iRYID > 0)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_HYLXQX X where D.HYKTYPE=X.HYKTYPE and X.PERSON_ID=:RYID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_HYLXQX X,XTCZYGRP G where D.HYKTYPE=X.HYKTYPE and X.ID=G.GROUPID and G.PERSON_ID=:RYID))");
                query.ParamByName("RYID").AsInteger = obj.iRYID;
            }
            query.Open();
            while (!query.Eof)
            {
                HYKDEF item = new HYKDEF();
                item.iHYKTYPE = query.FieldByName("ID").AsInteger;
                item.sHYKNAME = query.FieldByName("NAME").AsString;
                item.iHYKKZID = query.FieldByName("PID").AsInteger;
                item.sHYKDM = item.iHYKTYPE < 10 ? ("0" + item.iHYKTYPE) : item.iHYKTYPE.ToString();
                item.sHYKPDM = item.iHYKKZID < 10 ? ("0" + item.iHYKKZID) : item.iHYKKZID.ToString();
                item.fKFJE = query.FieldByName("KFJE").AsFloat;
                item.iBJ_XFSJ = query.FieldByName("BJ_XFSJ").AsInteger;
                item.iZFBJ = query.FieldByName("ZFBJ").AsInteger;
                lst.Add(item);
                query.Next();
                //sNodes = sNodes + "{id:" + item.sHYKDM + ",pID:" + item.sHYKPDM + ",name:" + item.sHYKNAME + "}";
                //if (!query.Eof)
                //    sNodes = sNodes + ",";
            }
            //sNodes = sNodes + "]";
            query.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillHYKJC(out string msg, CyQuery query, CRMLIBASHX param)// int kzid)
        {
            msg = string.Empty;
            List<HYKDJDEF> lst = new List<HYKDJDEF>();
            query.SQL.Text = "select * from HYKDJDEF where HYKKZID=:HYKKZID ";
            query.SQL.Add(" order by HYKJCID");
            query.ParamByName("HYKKZID").AsInteger = param.iKZID;
            query.Open();
            while (!query.Eof)
            {
                HYKDJDEF item = new HYKDJDEF();
                item.iHYKJCID = query.FieldByName("HYKJCID").AsInteger;
                item.sHYKJCNAME = query.FieldByName("HYKJCNAME").AsString;
                lst.Add(item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillHYGRP(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from HYK_HYGRP where STATUS=2 ";
                query.SQL.Add(" order by GRPID");
                query.Open();
                while (!query.Eof)
                {
                    HYZLX item = new HYZLX();
                    item.iGRPID = query.FieldByName("GRPID").AsInteger;
                    item.sGRPMC = query.FieldByName("GRPMC").AsString;

                    lst.Add(item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillFLGZ(out string msg, CyQuery query, CRMLIBASHX param)//int iHYID, int iHYKTYPE)
        {
            msg = string.Empty;
            List<Object> lst = new List<object>();
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);

            query.SQL.Text = "  select  G.GZMC, S.SHMC,S.SHDM,G.JLBH FLGZBH,CLFS,XSWS,G.YHQID,YHQJSRQ,YHQSL,YHQDW,BJ_KCDYJF,ZXDW,YHQMC,G.YHQSL,G.YHQDW, sysdate YHQYXQ";
            query.SQL.Add("  ,0.0 FLJE ");
            query.SQL.Add("    from HYK_JFFLGZ G,SHDY S,YHQDEF Y,HYKDEF F ");
            query.SQL.Add("  WHERE  G.SHDM=S.SHDM and G.YHQID=Y.YHQID  and G.HYKTYPE=F.HYKTYPE and STATUS=1");
            query.SQL.Add("  and G.HYKTYPE=" + param.iHYKTYPE);
            if (param.iHYID != 0)
            {
                query.SQL.Add(" and exists(SELECT * FROM  HYK_JFZH where HYID=" + param.iHYID + "  )");
            }
            query.SQL.Add(" order by FLGZBH desc");
            query.Open();
            while (!query.Eof)
            {
                JFFLGZ item = new JFFLGZ();
                item.sSHDM = query.FieldByName("SHDM").AsString;
                item.fFLJE = query.FieldByName("FLJE").AsFloat;
                if (item.dYHQJSRQ != "")
                {
                    item.dYHQJSRQ = FormatUtils.DateToString(query.FieldByName("YHQJSRQ").AsDateTime);
                }
                else
                {
                    if (query.FieldByName("YHQSL").AsInteger == 0)
                    {
                        item.dYHQJSRQ = FormatUtils.DateToString(serverTime);
                    }
                    else
                    {
                        if (query.FieldByName("YHQDW").AsInteger == 1)
                        {
                            serverTime = serverTime.AddDays(query.FieldByName("YHQSL").AsInteger);
                        }
                        else
                        {
                            serverTime = serverTime.AddMonths(query.FieldByName("YHQSL").AsInteger);
                        }
                        item.dYHQJSRQ = FormatUtils.DateToString(serverTime);
                    }
                }
                item.sYHQMC = query.FieldByName("YHQMC").AsString;
                item.iYHQID = query.FieldByName("YHQID").AsInteger;
                item.iFLGZBH = query.FieldByName("FLGZBH").AsInteger;
                item.sGZMC = query.FieldByName("GZMC").AsString;
                item.iHYID = param.iHYID;
                //item.sHYKNO = Obj.sHYKNO;

                lst.Add(item);
                query.Next();
            }

            return JsonConvert.SerializeObject(lst);
        }

        public static string GetJFFQJFJE(out string msg, CyQuery query, CRMLIBASHX param)// JFFLGZ obj, double CLJF)
        {
            msg = string.Empty;
            double fCLJF = param.fCLJF;
            JFFQ jffq = new JFFQ();
            int CLFS = 0;
            JFFLGZ obj = param.FLGZ;
            query.SQL.Text = "select * from HYK_JFFLGZ G where ";
            query.SQL.Add("   G.JLBH=" + obj.iJLBH);
            query.Open();
            if (!query.IsEmpty)
                CLFS = query.FieldByName("CLFS").AsInteger;

            query.Close();
            if (CLFS == 2)
            {
                for (int i = obj.itemTable.Count - 1; i >= 0; i--)
                {
                    if (fCLJF >= obj.itemTable[i].fJFXX)
                    {
                        JFFQITEM jffqitem = new JFFQITEM();

                        jffq.fZCLJF = fCLJF;
                        jffq.fZFQJE = fCLJF * 1 * obj.itemTable[i].fFLBL;
                        jffqitem.fCLJF = fCLJF;
                        jffqitem.fFQJE = fCLJF * 1 * obj.itemTable[i].fFLBL;
                        jffqitem.iGridId = i;
                        jffq.itemTable1.Add(jffqitem);
                        fCLJF = fCLJF - jffqitem.fCLJF;
                        //    jffq.itemTable1[0].fCLJF = fCLJF;
                        //    jffq.itemTable1[0].fFQJE = fCLJF * 1 * obj.itemTable[i].fFLBL;
                        //  jffq.itemTable1[0].iGridId = i;

                    }

                }
            }
            if (CLFS == 1)
            {
                for (int i = obj.itemTable.Count - 1; i >= 0; i--)
                {
                    if (fCLJF >= obj.itemTable[i].fJFXX)
                    {
                        JFFQITEM jffqitem = new JFFQITEM();
                        if (i != 0)
                        {
                            // if (fCLJF - obj.itemTable[i].fJFXX != 0)
                            //   {
                            jffqitem.fCLJF = fCLJF - obj.itemTable[i].fJFXX;
                            jffqitem.fFQJE = (fCLJF - obj.itemTable[i].fJFXX) * 1 * obj.itemTable[i].fFLBL;
                            jffqitem.iGridId = i;
                            jffq.itemTable1.Add(jffqitem);
                            // jffq.itemTable1[i].fCLJF = fCLJF - obj.itemTable[i].fJFXX;
                            //jffq.itemTable1[i].fFQJE = (fCLJF - obj.itemTable[i].fJFXX) * 1 * obj.itemTable[i].fFLBL;
                            //jffq.itemTable1[i].iGridId = i;
                            jffq.fZCLJF += fCLJF - obj.itemTable[i].fJFXX;
                            jffq.fZFQJE += (fCLJF - obj.itemTable[i].fJFXX) * 1 * obj.itemTable[i].fFLBL;
                            fCLJF = fCLJF - (fCLJF - obj.itemTable[i].fJFXX);
                            //   }
                            //   else
                            //  {
                            //     jffqitem.fCLJF = fCLJF;
                            //    jffqitem.fFQJE = fCLJF * obj.itemTable[i].fFLBL;
                            //    jffqitem.iGridId = i;
                            //      jffq.fZCLJF += jffqitem.fCLJF;
                            //      jffq.fZFQJE += jffqitem.fFQJE;
                            //     jffq.itemTable1.Add(jffqitem);
                            //    fCLJF = fCLJF - jffqitem.fCLJF;

                            //  }
                        }
                        else
                        {
                            jffqitem.fCLJF = fCLJF;
                            jffqitem.fFQJE = fCLJF * obj.itemTable[i].fFLBL;
                            jffqitem.iGridId = i;
                            jffq.fZCLJF += jffqitem.fCLJF;
                            jffq.fZFQJE += jffqitem.fFQJE;
                            jffq.itemTable1.Add(jffqitem);
                            fCLJF = fCLJF - jffqitem.fCLJF;

                        }
                    }
                }
            }

            if (CLFS == 3)
            {
                for (int i = obj.itemTable.Count - 1; i >= 0; i--)
                {
                    if (fCLJF >= obj.itemTable[i].fJFXX)
                    {
                        JFFQITEM jffqitem = new JFFQITEM();
                        int JFBS = Convert.ToInt32(Math.Floor(fCLJF / obj.itemTable[i].fJFXX));
                        int JFYS = Convert.ToInt32(fCLJF % obj.itemTable[i].fJFXX);
                        //   jffq.itemTable1[i].iGridId = i;
                        //  jffq.itemTable1[i].fCLJF = JFBS * obj.itemTable[i].fJFXX;
                        //    jffq.itemTable1[i].fFQJE = JFBS * obj.itemTable[i].fJFXX * obj.itemTable[i].fFLBL;
                        jffq.fZCLJF += JFBS * obj.itemTable[i].fJFXX;
                        jffq.fZFQJE += JFBS * obj.itemTable[i].fJFXX * obj.itemTable[i].fFLBL;
                        jffqitem.iGridId = i;
                        jffqitem.fCLJF = JFBS * obj.itemTable[i].fJFXX;
                        jffqitem.fFQJE = JFBS * obj.itemTable[i].fJFXX * obj.itemTable[i].fFLBL;
                        jffq.itemTable1.Add(jffqitem);
                        fCLJF = JFYS;
                    }
                }
            }
            return (JsonConvert.SerializeObject(jffq));
        }
        public static string FillYHQ(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<YHQDEF> lst = new List<YHQDEF>();
            query.SQL.Text = "select * from YHQDEF where 1=1";
            switch (param.iMODE)
            {
                case 1:
                    query.SQL.Add(" and BJ_TY=0");
                    break;
            }
            query.SQL.Add(" order by YHQID");
            query.Open();
            while (!query.Eof)
            {
                YHQDEF obj = new YHQDEF();
                obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                //public int iFS_YQMDFW = 0, iFS_FQMDFW = 0, iBJ_TS = 0;
                obj.iFS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
                obj.iFS_FQMDFW = query.FieldByName("FS_FQMDFW").AsInteger;
                obj.iBJ_TS = query.FieldByName("BJ_TS").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillCXHD(out string msg, string sSHDM)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CXHD> lst = new List<CXHD>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select C.*,S.SHMC from CXHDDEF C,SHDY S where C.SHDM=S.SHDM";
                if (sSHDM != "")
                {
                    query.SQL.Add(" and C.SHDM='" + sSHDM + "' ");
                }
                query.SQL.Add(" order by C.CXID");
                query.Open();
                while (!query.Eof)
                {
                    CXHD obj = new CXHD();
                    obj.iCXID = query.FieldByName("CXID").AsInteger;
                    obj.sSHDM = query.FieldByName("SHDM").AsString;
                    obj.sSHMC = query.FieldByName("SHMC").AsString;
                    obj.iCXHDBH = query.FieldByName("CXHDBH").AsInteger;
                    obj.sCXZT = query.FieldByName("CXZT").AsString;
                    if (obj.sCXZT.IndexOf("\"") != -1)
                    {
                        obj.sCXZT = obj.sCXZT.Replace("\"", "\'");
                    }
                    obj.sCXNR = query.FieldByName("CXNR").AsString;
                    if (obj.sCXNR.IndexOf("\"") != -1)
                    {
                        obj.sCXNR = obj.sCXNR.Replace("\"", "\'");
                    }
                    obj.iNIAN = query.FieldByName("NIAN").AsInteger;
                    obj.iCXQS = query.FieldByName("CXQS").AsInteger;
                    obj.dKSSJ = FormatUtils.DatetimeToString(query.FieldByName("KSSJ").AsDateTime);
                    obj.dJSSJ = FormatUtils.DatetimeToString(query.FieldByName("JSSJ").AsDateTime);
                    obj.dSCSJ = FormatUtils.DatetimeToString(query.FieldByName("SCSJ").AsDateTime);
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        //XXM 重载
        public static string FillKLXTreeByQX(out string msg, int iMode, int iPersonID)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                switch (iMode)
                {
                    case 1:
                        sql = " and A.BJ_CZK=0";
                        break;
                    case 2:
                        sql = " and A.BJ_CZK=1";
                        break;
                    case 3:
                        sql = " and A.BJ_CZZH=1";
                        break;
                }
                query.Close();
                query.SQL.Text = "SELECT * FROM  ( ";
                query.SQL.Text += " select 1 QX,1 QX2,HYKKZID ID,HYKKZNAME NAME,0 PID,0 HYKTYPE from HYKKZDEF where 1=1 ";
                query.SQL.Add(" union  ");
                query.SQL.Add(" select  ");
                query.SQL.Add(" (select 1 from XTCZY_HYLXQX Q where PERSON_ID=:PERSON_ID and (A.HYKTYPE=Q.HYKTYPE)) QX,");
                query.SQL.Add(" (select 1 from CZYGROUP_HYLXQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (A.HYKTYPE=Q.HYKTYPE)) QX2,");
                query.SQL.Add(" A.HYKTYPE ID,A.HYKNAME NAME,A.HYKKZID PID,A.HYKTYPE HYKTYPE from HYKDEF A  where 1=1");
                query.SQL.Add(sql);
                query.SQL.Add(") ORDER BY ID,HYKTYPE");
                query.ParamByName("PERSON_ID").AsInteger = iPersonID;
                query.Open();
                while (!query.Eof)
                {
                    //Ztree格式
                    HYKDEF item = new HYKDEF();
                    item.iHYKTYPE = query.FieldByName("ID").AsInteger;
                    item.sHYKNAME = query.FieldByName("NAME").AsString;
                    item.iHYKKZID = query.FieldByName("PID").AsInteger;
                    item.sHYKDM = item.iHYKTYPE < 10 ? ("0" + item.iHYKTYPE) : item.iHYKTYPE.ToString();
                    item.sHYKPDM = item.iHYKKZID < 10 ? ("0" + item.iHYKKZID) : item.iHYKKZID.ToString();
                    lst.Add(item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        //XXM
        public static string FillSMZQ(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<SMZQDEF> lst = new List<SMZQDEF>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBOLD");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select S.* from HYK_SMZQ_DEF S where 1=1";
                query.SQL.Add(" order by S.LBID");
                query.Open();
                while (!query.Eof)
                {
                    SMZQDEF _item = new SMZQDEF();
                    _item.iLBID = query.FieldByName("LBID").AsInteger;
                    _item.sLBMC = query.FieldByName("LBMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        #region 五个权限CheckTree型的填充方法

        public static string FillCheckTreeKLX(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select ";
            query.SQL.Add(" (select distinct 1 from XTCZY_HYLXQX Q where PERSON_ID=:PERSON_ID and (A.HYKTYPE=Q.HYKTYPE)) QX,");
            query.SQL.Add(" (select distinct 1 from CZYGROUP_HYLXQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (A.HYKTYPE=Q.HYKTYPE)) QX2,");
            query.SQL.Add(" A.* from HYKDEF A where 1=1");
            query.SQL.Add(" order by HYKTYPE");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("HYKTYPE").AsInteger.ToString();
                obj.iID = -1;
                obj.pId = "";
                obj.sMC = query.FieldByName("HYKNAME").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeBGDD(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select (select max(BGDDDM) from HYK_BGDD B where A.BGDDDM like B.BGDDDM||'%' and A.BGDDDM<>B.BGDDDM) PBGDDDM,";
            query.SQL.Add(" (select distinct 1 from XTCZY_BGDDQX Q where PERSON_ID=:PERSON_ID and (A.BGDDDM like Q.BGDDDM||'%' or Q.BGDDDM=' ')) QX,");
            query.SQL.Add(" (select distinct 1 from CZYGROUP_BGDDQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (A.BGDDDM like Q.BGDDDM||'%' or Q.BGDDDM=' ')) QX2,");
            query.SQL.Add(" A.* from HYK_BGDD A where 1=1");
            query.SQL.Add(" order by BGDDDM");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("BGDDDM").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PBGDDDM").AsString;
                obj.sMC = query.FieldByName("BGDDMC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeSHBM(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select '' PDM,";
            query.SQL.Add(" (select distinct 1 from XTCZY_BMCZQX Q where Q.PERSON_ID=:PERSON_ID and (Q.SHDM=A.SHDM or Q.SHDM=' ')) QX,");
            query.SQL.Add(" (select distinct 1 from CZYGROUP_BMCZQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (Q.SHDM=A.SHDM or Q.SHDM=' ')) QX2,");
            query.SQL.Add(" SHDM DM,SHMC MC from SHDY A order by SHDM");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("DM").AsString;
                obj.rootId = obj.id;
                obj.iID = -1;
                obj.pId = query.FieldByName("PDM").AsString;
                obj.sMC = query.FieldByName("MC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            query.Close();
            query.SQL.Text = "select ";
            query.SQL.Add("(select distinct 1 from XTCZY_BMCZQX Q where PERSON_ID=:PERSON_ID and Q.SHDM=A.SHDM and (A.BMDM like Q.BMDM||'%' or Q.BMDM=' ')) QX,");
            query.SQL.Add("(select max(BMDM) from SHBM B where A.SHDM=B.SHDM and A.BMDM like B.BMDM||'%' and A.BMDM<>B.BMDM) PDM,");
            query.SQL.Add("A.BMDM DM,A.BMMC MC,A.SHDM from SHBM A where 1=1");
            query.SQL.Add("order by SHDM,BMDM");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("SHDM").AsString + query.FieldByName("DM").AsString;//由 SHDM 加 BMDM 组成Ztree节点id
                obj.rootId = query.FieldByName("SHDM").AsString;
                obj.iID = -1;
                //通过部门级次参数来生成上级部门
                //int tBMJC = CRMConfig.iBMDMZCDs.IndexOf(query.FieldByName("DM").AsString.Length);
                //if (tBMJC > 0)
                //{
                //    obj.sPDM = obj.sDM.Substring(0, obj.sDM.Length - CRMConfig.iBMDMCDs[tBMJC]);
                //}
                //通过查询语句来生成上级部门
                obj.pId = query.FieldByName("SHDM").AsString + query.FieldByName("PDM").AsString;
                //if (obj.sPDM.Trim() == "")
                //    obj.sPDM = query.FieldByName("SHDM").AsString;
                obj.sMC = query.FieldByName("MC").AsString.Trim().Replace("\"", "\\\"");
                obj.name = query.FieldByName("DM").AsString + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;                    
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeMD(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select '00' PDM,";//必须为2级树形结构
            query.SQL.Add(" (select distinct 1 from XTCZY_MDQX Q,MDDY M where Q.PERSON_ID=:PERSON_ID and Q.MDID=M.MDID and M.SHDM=A.SHDM ) QX,");
            //query.SQL.Add(" (select distinct 1 from CZYGROUP_BMCZQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (Q.SHDM=A.SHDM or Q.SHDM=' ')) QX2,");
            query.SQL.Add(" SHDM DM,SHMC MC from SHDY A order by SHDM");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("DM").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PDM").AsString;
                obj.sMC = query.FieldByName("MC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.id = "SH" + obj.id;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }

            query.Close();
            query.SQL.Text = "select ";
            query.SQL.Add(" (select 1 from XTCZY_MDQX Q where PERSON_ID=:PERSON_ID and (A.MDID=Q.MDID)) QX,");
            //query.SQL.Add(" (select 1 from CZYGROUP_MDQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (A.MDID=Q.MDID)) QX2,");
            query.SQL.Add(" A.* from MDDY A,SHDY B where A.SHDM=B.SHDM");
            query.SQL.Add(" order by MDID");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("MDID").AsInteger.ToString();
                obj.iID = -1;
                obj.pId = "SH" + query.FieldByName("SHDM").AsString;
                obj.sMC = query.FieldByName("MDMC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeFXDW(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select ( select max(B.FXDWDM) from FXDWDEF B where A.FXDWDM like B.FXDWDM|| '%' and  A.FXDWDM <> B.FXDWDM ) PFXDWDM,";
            query.SQL.Add(" (select distinct 1 from XTCZY_FXDWQX Q where PERSON_ID=:PERSON_ID and (A.FXDWDM like Q.FXDWDM||'%' or Q.FXDWDM=' ')) QX,");
            query.SQL.Add(" (select distinct 1 from CZYGROUP_FXDWQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (A.FXDWDM like Q.FXDWDM||'%' or Q.FXDWDM=' ')) QX2,");
            query.SQL.Add(" A.* from FXDWDEF A where 1=1 order by FXDWDM");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("FXDWDM").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PFXDWDM").AsString;
                obj.sMC = query.FieldByName("FXDWMC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillCheckTreeKLX_GROUP(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select ";
            query.SQL.Add(" (select distinct 1 from CZYGROUP_HYLXQX Q where ID=:ID and (A.HYKTYPE=Q.HYKTYPE)) QX,");
            query.SQL.Add(" A.* from HYKDEF A where 1=1");
            query.SQL.Add(" order by HYKTYPE");
            query.ParamByName("ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("HYKTYPE").AsInteger.ToString();
                obj.iID = -1;
                obj.pId = "";
                obj.sMC = query.FieldByName("HYKNAME").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeBGDD_GROUP(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.Close();
            query.SQL.Text = "select (select max(BGDDDM) from HYK_BGDD B where A.BGDDDM like B.BGDDDM||'%' and A.BGDDDM<>B.BGDDDM) PBGDDDM,";
            query.SQL.Add(" (select distinct 1 from CZYGROUP_BGDDQX Q where ID=:ID and (A.BGDDDM like Q.BGDDDM||'%' or Q.BGDDDM=' ')) QX,");

            query.SQL.Add(" A.* from HYK_BGDD A where 1=1");
            query.SQL.Add(" order by BGDDDM");
            query.ParamByName("ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("BGDDDM").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PBGDDDM").AsString;
                obj.sMC = query.FieldByName("BGDDMC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;

                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeSHBM_GROUP(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select '' PDM,";
            query.SQL.Add(" (select distinct 1 from CZYGROUP_BMCZQX Q where Q.ID=:ID and (Q.SHDM=A.SHDM or Q.SHDM=' ')) QX,");

            query.SQL.Add(" SHDM DM,SHMC MC from SHDY A order by SHDM");
            query.ParamByName("ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("DM").AsString;
                obj.rootId = obj.id;
                obj.iID = -1;
                obj.pId = query.FieldByName("PDM").AsString;
                obj.sMC = query.FieldByName("MC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            query.Close();
            query.SQL.Text = "select ";
            query.SQL.Add("(select distinct 1 from CZYGROUP_BMCZQX Q where ID=:ID and Q.SHDM=A.SHDM and (A.BMDM like Q.BMDM||'%' or Q.BMDM=' ')) QX,");
            query.SQL.Add("(select max(BMDM) from SHBM B where A.SHDM=B.SHDM and A.BMDM like B.BMDM||'%' and A.BMDM<>B.BMDM) PDM,");
            query.SQL.Add("A.BMDM DM,A.BMMC MC,A.SHDM from SHBM A where 1=1");
            query.SQL.Add("order by SHDM,BMDM");
            query.ParamByName("ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("SHDM").AsString + query.FieldByName("DM").AsString;//由 SHDM 加 BMDM 组成Ztree节点id
                obj.rootId = query.FieldByName("SHDM").AsString;
                obj.iID = -1;
                //通过部门级次参数来生成上级部门
                //int tBMJC = CRMConfig.iBMDMZCDs.IndexOf(query.FieldByName("DM").AsString.Length);
                //if (tBMJC > 0)
                //{
                //    obj.sPDM = obj.sDM.Substring(0, obj.sDM.Length - CRMConfig.iBMDMCDs[tBMJC]);
                //}
                //通过查询语句来生成上级部门
                obj.pId = query.FieldByName("SHDM").AsString + query.FieldByName("PDM").AsString;
                //if (obj.sPDM.Trim() == "")
                //    obj.sPDM = query.FieldByName("SHDM").AsString;
                obj.sMC = query.FieldByName("MC").AsString.Trim().Replace("\"", "\\\"");
                obj.name = query.FieldByName("DM").AsString + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeMD_GROUP(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select '00' PDM,";//数据库中存在DM为0的数据。
            query.SQL.Add(" (select distinct 1 from CZYGROUP_MDQX Q,MDDY M where Q.ID=:PERSON_ID and Q.MDID=M.MDID and M.SHDM=A.SHDM ) QX,");
            //query.SQL.Add(" (select distinct 1 from CZYGROUP_BMCZQX Q,XTCZYGRP X where PERSON_ID=:PERSON_ID and X.GROUPID=Q.ID and (Q.SHDM=A.SHDM or Q.SHDM=' ')) QX2,");
            query.SQL.Add(" SHDM DM,SHMC MC from SHDY A order by SHDM");
            query.ParamByName("PERSON_ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("DM").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PDM").AsString;
                obj.sMC = query.FieldByName("MC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.id = "SH" + obj.id;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }

            query.Close();
            query.SQL.Text = "select ";
            query.SQL.Add(" (select 1 from CZYGROUP_MDQX Q where ID=:ID and (A.MDID=Q.MDID)) QX,");
            query.SQL.Add(" A.* from MDDY A where 1=1");
            query.SQL.Add(" order by MDID");
            query.ParamByName("ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("MDID").AsInteger.ToString();
                obj.iID = -1;
                obj.pId = "SH" + query.FieldByName("SHDM").AsString;
                obj.sMC = query.FieldByName("MDMC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCheckTreeFXDW_GROUP(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select ( select max(B.FXDWDM) from FXDWDEF B where A.FXDWDM like B.FXDWDM|| '%' and  A.FXDWDM <> B.FXDWDM ) PFXDWDM,";
            query.SQL.Add(" (select distinct 1 from CZYGROUP_FXDWQX Q where ID=:ID and (A.FXDWDM like Q.FXDWDM||'%' or Q.FXDWDM=' ')) QX,");
            query.SQL.Add(" A.* from FXDWDEF A where 1=1 order by FXDWDM");
            query.ParamByName("ID").AsInteger = param.iRYID;
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("FXDWDM").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PFXDWDM").AsString;
                obj.sMC = query.FieldByName("FXDWMC").AsString;
                obj.name = obj.id + " " + obj.sMC;
                obj.@checked = query.FieldByName("QX").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        #endregion

        public static string FillHYBQTree(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = " select * from(";
            query.SQL.Add("select 'AA'||LABELLBID ID,BQMC MC,'00' PID,0 LABELID from LABEL_LB");
            query.SQL.Add("union select 'BB'||LABELXMID,LABELXMMC,nvl((select max('BB'||LABELXMID) from LABEL_XM B where A.LABELXMDM like B.LABELXMDM||'%' and B.LABELLBID=A.LABELLBID and A.LABELXMDM<>B.LABELXMDM),'AA'||LABELLBID),0 LABELID from LABEL_XM A");
            query.SQL.Add("union select 'CC'||LABELID,LABEL_VALUE,'BB'||(select LABELXMID from LABEL_XM B where B.LABELXMID=A.LABELXMID),LABELID from LABEL_XMITEM A)");
            //query.SQL.Add("order by PID,ID");
            query.Open();
            while (!query.Eof)
            {
                CheckTreeNode obj = new CheckTreeNode();
                obj.id = query.FieldByName("ID").AsString;
                obj.iID = -1;
                obj.pId = query.FieldByName("PID").AsString;
                obj.sMC = query.FieldByName("MC").AsString;
                // obj.sQC = obj.sDM + " " + obj.sMC;
                obj.name = obj.sMC;
                obj.iUID = query.FieldByName("LABELID").AsInteger;
                obj.@checked = false;
                //if (!obj.bChecked)
                //    obj.bChecked = query.FieldByName("QX2").AsInteger == 1;
                lst.Add(obj);
                query.Next();
            }
            query.Close();


            return JsonConvert.SerializeObject(lst);
        }


        public static string FillMD(out string msg, CyQuery query, CRMLIBASHX obj)//out string msg, int pRYID, string pSHDM, bool bQX)
        {
            msg = string.Empty;
            List<MDDY> lst = new List<MDDY>();
            query.SQL.Text = "select M.*,S.SHMC from MDDY M,SHDY S where M.SHDM=S.SHDM";
            if (obj.sSHDM != null && obj.sSHDM != "" && obj.sSHDM != "undefined")
            {
                query.SQL.Add(" and M.SHDM='" + obj.sSHDM + "'");
                //query.ParamByName("SHDM").AsString = pSHDM;
            }
            if (obj.iQX == 1 && obj.iRYID > 0)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_MDQX X where X.PERSON_ID=:RYID and X.MDID=M.MDID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_MDQX X,XTCZYGRP G where G.PERSON_ID=:RYID and X.MDID=M.MDID and X.ID=G.GROUPID))");
                query.ParamByName("RYID").AsInteger = obj.iRYID;
            }
            query.SQL.Add(" order by MDID");
            query.Open();
            //DataTable dt = query.GetDataTable(); 
            while (!query.Eof)
            {
                MDDY one = new MDDY();
                one.iMDID = query.FieldByName("MDID").AsInteger;
                one.sMDMC = query.FieldByName("MDMC").AsString;
                one.sMDDM = query.FieldByName("MDDM").AsString;
                one.sSHDM = query.FieldByName("SHDM").AsString;
                one.sSHMC = query.FieldByName("SHMC").AsString;
                lst.Add(one);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string HttpGetWriteCard(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(obj.sURL + (obj.sData == "" ? "" : "?") + obj.sData);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            obj.sData = retString;
            return JsonConvert.SerializeObject(obj);
        }


        public static string FillFXQDTree(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CrmProc.CRMGL.CRMGL_FXQDDY> lst = new List<CrmProc.CRMGL.CRMGL_FXQDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "  select (select max(FXQDDM) from FXQDDY B where A.FXQDDM like B.FXQDDM||'%' and A.FXQDDM<>B.FXQDDM) PFXQDDM";
                query.SQL.Add("  ,(A.FXQDDM||' '||A.FXQDMC) FXQDQC,A.* from FXQDDY A where 1=1 order by FXQDDM");
                query.Open();
                while (!query.Eof)
                {
                    CrmProc.CRMGL.CRMGL_FXQDDY obj = new CrmProc.CRMGL.CRMGL_FXQDDY();
                    obj.iJLBH = query.FieldByName("FXQDID").AsInteger;
                    obj.sFXQDMC = query.FieldByName("FXQDMC").AsString;
                    obj.sFXQDDM = query.FieldByName("FXQDDM").AsString;
                    obj.sPFXQDDM = query.FieldByName("PFXQDDM").AsString;
                    obj.sFXQDQC = query.FieldByName("FXQDQC").AsString;
                    obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;

                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);

        }



        public static string FillXYDJ(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<HYXYDJDY> lst = new List<HYXYDJDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select XYDJID,XYDJMC from HYK_HYXYDJDEF ";
                query.SQL.Add(" order by XYDJID");
                query.Open();
                while (!query.Eof)
                {
                    HYXYDJDY _item = new HYXYDJDY();
                    _item.iXYDJID = query.FieldByName("XYDJID").AsInteger;
                    _item.sXYDJMC = query.FieldByName("XYDJMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillHYLX(out string msg, int pXMLX)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<HYXXXMDY> lst = new List<HYXXXMDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select D.* from LM_JCXXDY D where D.XMLX=" + pXMLX;
                query.SQL.Add(" order by D.JLBH");
                query.Open();
                while (!query.Eof)
                {
                    HYXXXMDY _item = new HYXXXMDY();
                    _item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    _item.iXMLX = query.FieldByName("XMLX").AsInteger;
                    _item.sNAME = query.FieldByName("NAME").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillFWNR(out string msg, int pHYKTYPE, int pMAINHYID = 0)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<HYXX_Detail> lst = new List<HYXX_Detail>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                if (pHYKTYPE != 0)
                {
                    query.SQL.Text = "select F.FWNRID,F.FWNRMC, 0 BJ_ZKFW from FWNRDEF F,HYK_XSFWDEF H where F.FWNRID=H.FWNRID";
                    query.SQL.Add(" and H.HYKTYPE = " + pHYKTYPE);
                    if (pMAINHYID != 0)
                    {
                        query.SQL.Add("UNION");
                        query.SQL.Add(" select F.FWNRID,F.FWNRMC, 1 BJ_ZKFW from FWNRDEF F,HYK_XSFWDEF H where F.FWNRID=H.FWNRID ");
                        query.SQL.Add(" and H.HYKTYPE IN(SELECT  HYKTYPE FROM HYK_HYXX　where HYID=" + pMAINHYID + ") ");
                    }
                }
                else
                    query.SQL.Text = "select F.FWNRID,F.FWNRMC,0 BJ_ZKFW from FWNRDEF F";
                query.Open();
                while (!query.Eof)
                {
                    HYXX_Detail _item = new HYXX_Detail();
                    _item.iFWNRID = query.FieldByName("FWNRID").AsInteger;
                    _item.iBJ_PARENT = query.FieldByName("BJ_ZKFW").AsInteger;
                    _item.sFWNRMC = _item.iBJ_PARENT == 1 ? "(主卡服务)" + query.FieldByName("FWNRMC").AsString : query.FieldByName("FWNRMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillNBDM(out string msg, CyQuery query, CRMLIBASHX param)//int pRYID, string pBMDM, bool bQX)
        {
            msg = string.Empty;
            List<WXCD> lst = new List<WXCD>();
            query.SQL.Text = "select NBDM,MC from WX_NBDMDEF";
            query.SQL.Add("  order by NBDM");
            query.Open();
            while (!query.Eof)
            {
                WXCD _item = new WXCD();
                _item.sNBDM = query.FieldByName("NBDM").AsString;
                _item.sMC = query.FieldByName("MC").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillPublicID(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<PUBLICID> lst = new List<PUBLICID>();
            query.SQL.Text = "select * from WX_PUBLIC order by PUBLICID";
            query.Open();
            while (!query.Eof)
            {
                PUBLICID one = new PUBLICID();
                one.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
                one.sPUBLICNAME = query.FieldByName("PUBLICNAME").AsString;
                one.sPUBLICIF = query.FieldByName("PUBLICIF").AsString;
                lst.Add(one);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillGZ(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<WXCD> lst = new List<WXCD>();
            query.SQL.Text = " select * from WX_ANSWERGZDEF";
            query.SQL.Add("  order by JLBH");
            query.Open();
            while (!query.Eof)
            {
                WXCD _item = new WXCD();
                _item.iJLBH = query.FieldByName("JLBH").AsInteger;
                _item.sTYPE = query.FieldByName("TYPE").AsString;
                _item.sNAME = query.FieldByName("NAME").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillWT(out string msg, CyQuery query, CRMLIBASHX param)// int pTYPE, string pBJ_NONE)
        {
            msg = string.Empty;
            List<WXGZ> lst = new List<WXGZ>();
            query.SQL.Text = " select ASKID,ASK from WX_ASK where STATUS=1 and TYPE=" + param.iTYPE;
            query.SQL.Add("  order by ASKID");
            query.Open();
            while (!query.Eof)
            {
                WXGZ _item = new WXGZ();
                _item.iID = query.FieldByName("ASKID").AsInteger;
                _item.sASK = query.FieldByName("ASK").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillHDMC(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<HYHDDY> lst = new List<HYHDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from HYK_HDNRDEF where 1=1";
                query.SQL.Add(" order by HDID");
                query.Open();
                while (!query.Eof)
                {
                    HYHDDY _item = new HYHDDY();
                    _item.iHDID = query.FieldByName("HDID").AsInteger;
                    _item.sHDMC = query.FieldByName("HDMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillXFLID(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<YDDDXFLDY> lst = new List<YDDDXFLDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select XFLID,XFLMC from MOBILE_XFL where 1 = 1 ";
                query.SQL.Add(" order by INX");
                query.Open();
                while (!query.Eof)
                {
                    YDDDXFLDY _item = new YDDDXFLDY();
                    _item.iFLID = query.FieldByName("XFLID").AsInteger;
                    _item.sXFLMC = query.FieldByName("XFLMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillAPPMD(out string msg, int pRYID, string pSHDM, bool bQX)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<MDDY> lst = new List<MDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select M.*,S.SHMC from MOBILE_MDDY M,MOBILE_SHDY S where M.SHDM=S.SHDM";
                if (pSHDM != null && pSHDM != "" && pSHDM != "undefined")
                {
                    query.SQL.Add(" and M.SHDM='" + pSHDM + "'");
                    //query.ParamByName("SHDM").AsString = pSHDM;
                }
                if (bQX && pRYID > 0)
                {
                    query.SQL.Add(" and (exists(select 1 from XTCZY_MDQX X where X.PERSON_ID=:RYID and X.MDID=M.MDID)");
                    query.SQL.Add(" or exists(select 1 from CZYGROUP_MDQX X,XTCZYGRP G where G.PERSON_ID=:RYID and X.MDID=M.MDID and X.ID=G.GROUPID))");

                }
                query.SQL.Add(" order by MDID");
                if (query.SQL.Text.IndexOf(":RYID") >= 0)
                {
                    query.ParamByName("RYID").AsInteger = pRYID;
                }
                //if (pRYID > 0)
                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    MDDY _item = new MDDY();
                    _item.iMDID = query.FieldByName("MDID").AsInteger;
                    _item.sMDMC = query.FieldByName("MDMC").AsString;
                    //_item.sMDDM = query.FieldByName("MDDM").AsString;
                    _item.sSHDM = query.FieldByName("SHDM").AsString;
                    _item.sSHMC = query.FieldByName("SHMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillHD(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<HYHDDY> lst = new List<HYHDDY>();
            query.SQL.Text = "select * from HYK_HDNRDEF where 1=1";
            // if (param.iSTATUS < 3)
            query.SQL.Add(" and STATUS = " + param.iSTATUS);
            query.SQL.Add(" order by HDID");
            query.Open();
            while (!query.Eof)
            {
                HYHDDY _item = new HYHDDY();
                _item.iHDID = query.FieldByName("HDID").AsInteger;
                _item.sHDMC = query.FieldByName("HDMC").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillRWZT(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<RWJL> lst = new List<RWJL>();
            query.SQL.Text = "select * from RWQFJL where 1=1";
            query.SQL.Add(" order by JLBH");
            query.Open();
            while (!query.Eof)
            {
                RWJL _item = new RWJL();
                _item.iJLBH = query.FieldByName("JLBH").AsInteger;
                _item.sRWZT = query.FieldByName("RWZT").AsString;
                lst.Add(_item);
                query.Next();
            }
            query.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillSH(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            List<SHDY> lst = new List<SHDY>();
            query.SQL.Text = "select S.* from SHDY S where 1=1";
            query.SQL.Add(" order by S.SHDM");
            query.Open();
            while (!query.Eof)
            {
                SHDY one = new SHDY();
                one.sSHDM = query.FieldByName("SHDM").AsString;
                one.sSHMC = query.FieldByName("SHMC").AsString;
                //one.sBMJC = query.FieldByName("BMJC").AsString;
                //one.sBMJC_Full = GetBMJC_Full(one.sBMJC);
                lst.Add(one);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetHYQYXQXX(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CRMGL.CRMGL_LQXQDY> lst = new List<CRMGL.CRMGL_LQXQDY>();
            query.SQL.Text = " select * from XQDY where 1=1";
            if (obj.iQYID != 0)
            {
                query.SQL.Add(" and QYID=" + obj.iQYID);
            }

            query.Open();
            while (!query.Eof)
            {
                CRMGL.CRMGL_LQXQDY one = new CRMGL.CRMGL_LQXQDY();
                one.iXQID = query.FieldByName("XQID").AsInteger;
                one.sXQMC = query.FieldByName("XQMC").AsString;
                lst.Add(one);
                query.Next();
            }
            msg = "w";
            return JsonConvert.SerializeObject(lst);

        }

        public static object FillLMSH(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<LMSHDY> lst = new List<LMSHDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select L.* from LM_SHDY L where 1=1";
                query.SQL.Add(" order by L.JLBH");
                query.Open();
                while (!query.Eof)
                {
                    LMSHDY _item = new LMSHDY();
                    _item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    _item.sLMSHMC = query.FieldByName("SHMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return lst;
        }
        public static string FillBQLB(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<LABEL_LB> lst = new List<LABEL_LB>();

            query.Close();
            query.SQL.Text = "select D.* from LABEL_LB D where STATUS = 0";
            query.SQL.Add(" order by D.LABELLBID asc");
            query.Open();
            while (!query.Eof)
            {
                LABEL_LB _item = new LABEL_LB();
                _item.iLABELLBID = query.FieldByName("LABELLBID").AsInteger;
                _item.sLABELMC = query.FieldByName("BQMC").AsString;
                lst.Add(_item);
                query.Next();
            }
            query.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillBQZ(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<LABEL_XMITEM> lst = new List<LABEL_XMITEM>();
            query.SQL.Text = "select L.* from LABEL_XMITEM L where LABELXMID = " + param.iLABELXMID;
            query.SQL.Add(" order by L.LABEL_VALUEID asc");
            query.Open();
            while (!query.Eof)
            {
                LABEL_XMITEM _item = new LABEL_XMITEM();
                _item.iLABEL_VALUEID = query.FieldByName("LABEL_VALUEID").AsInteger;
                _item.sLABEL_VALUE = query.FieldByName("LABEL_VALUE").AsString;
                _item.iLABELID = query.FieldByName("LABELID").AsInteger;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillBMQFFGZ(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<BMQFFGZ> lst = new List<BMQFFGZ>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from BMQFFGZ B";
                query.SQL.Add(" order by B.BMQFFGZID");
                query.Open();
                while (!query.Eof)
                {
                    BMQFFGZ _item = new BMQFFGZ();
                    _item.iBMQFFGZID = query.FieldByName("BMQFFGZID").AsInteger;
                    _item.sGZMC = query.FieldByName("GZMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillMDMC(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<MDDY> lst = new List<MDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select M.* from MDDY M where 1=1";
                //query.SQL.Add(" order by M.MDDY");
                query.Open();
                while (!query.Eof)
                {
                    MDDY _item = new MDDY();
                    _item.iJLBH = query.FieldByName("MDID").AsInteger;
                    _item.sMDMC = query.FieldByName("MDMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillCXHDDEF(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CXHDDEF> lst = new List<CXHDDEF>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select C.* from CXHDDEF C where 1=1";
                //query.SQL.Add(" order by C.CXHDDEF");
                query.Open();
                while (!query.Eof)
                {
                    CXHDDEF _item = new CXHDDEF();
                    _item.iCXID = query.FieldByName("CXID").AsInteger;
                    _item.sCXZT = query.FieldByName("CXZT").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillQZLX(out string msg, CyQuery query, CRMLIBASHX param)// int iMDID)
        {
            msg = string.Empty;
            List<QZLXMC> lst = new List<QZLXMC>();
            query.SQL.Text = "select C.* from HYK_QZLXDEF C where 1=1";
            if (param.iMDID != 0)
                query.SQL.Add(" and C.MDID=" + param.iMDID);
            query.Open();
            while (!query.Eof)
            {
                QZLXMC _item = new QZLXMC();
                _item.iJLBH = query.FieldByName("JLBH").AsInteger;
                _item.sQZLXMC = query.FieldByName("QZLXMC").AsString;
                _item.iQZCYRS = query.FieldByName("QZCYRS").AsInteger;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillKZ(out string msg, CyQuery query, CRMLIBASHX param)// int iBJ_CZK)
        {
            msg = string.Empty;
            List<HYKDEF> lst = new List<HYKDEF>();
            query.SQL.Text = "select * from HYKKZDEF where BJ_CZK=:BJ_CZK ";
            query.SQL.Add(" order by HYKKZID");
            query.ParamByName("BJ_CZK").AsInteger = param.iBJ_CZK;
            query.Open();
            while (!query.Eof)
            {
                HYKDEF _item = new HYKDEF();
                _item.iHYKTYPE = query.FieldByName("HYKKZID").AsInteger;
                _item.sHYKNAME = query.FieldByName("HYKKZNAME").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillFXDWTree(out string msg, CyQuery query, CRMLIBASHX obj)//out string msg, int pRYID, bool bQX)
        {
            msg = string.Empty;
            List<FXDW> lst = new List<FXDW>();
            query.SQL.Text = "select (select MAX(B.FXDWDM) from FXDWDEF B WHERE A.FXDWDM LIKE B.FXDWDM|| '%' AND  A.FXDWDM <> B.FXDWDM ) PFXDWDM";
            query.SQL.Add(" ,(A.FXDWDM||' '||A.FXDWMC) FXDWQC,A.* FROM FXDWDEF A WHERE 1=1 ");
            if (obj.iQX == 1 && obj.iRYID > 0)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_FXDWQX X where (X.FXDWDM=' ' or (A.FXDWDM like X.FXDWDM||'%' or X.FXDWDM like A.FXDWDM||'%')) and X.PERSON_ID=:RYID)");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_FXDWQX Q,XTCZYGRP G where (Q.FXDWDM=' ' or (A.FXDWDM like Q.FXDWDM||'%' or Q.FXDWDM like A.FXDWDM||'%')) and Q.ID=G.GROUPID and G.PERSON_ID=:RYID))");
                query.ParamByName("RYID").AsInteger = obj.iRYID;
            }
            query.SQL.Add("order by FXDWDM");
            query.Open();
            while (!query.Eof)
            {
                FXDW one = new FXDW();
                one.iJLBH = query.FieldByName("FXDWID").AsInteger;
                one.sFXDWDM = query.FieldByName("FXDWDM").AsString;
                one.sPFXDWDM = query.FieldByName("PFXDWDM").AsString;
                one.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                one.sFXDWQC = query.FieldByName("FXDWQC").AsString;
                one.iHMCD = query.FieldByName("HMCD").AsInteger;
                one.sKHQDM = query.FieldByName("KHQDM").AsString;
                one.sKHHZM = query.FieldByName("KHHZM").AsString;
                one.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                one.iMJBJ = query.FieldByName("MJBJ").AsInteger;
                lst.Add(one);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillTreeSHBM(out string msg, CyQuery query, CRMLIBASHX param)// string pSHDM, int pRYID, int pLevel)
        {
            string sBMJC = "22222";
            msg = string.Empty;
            List<object> lst = new List<object>();
            query.SQL.Text = "select BMJC from SHDY where SHDM=:SHDM";
            query.ParamByName("SHDM").AsString = param.sSHDM;
            query.Open();
            if (!query.IsEmpty)
            {
                if (query.Fields[0].AsString != "")
                    sBMJC = query.Fields[0].AsString;
            }
            int A = GetBMJC(sBMJC, param.iLEVEL);
            //query.SQL.Text = "select (select max(BMDM) from SHBM B where A.SHDM=B.SHDM and A.BMDM like B.BMDM||'%' and A.BMDM<>B.BMDM) PDM,";
            query.SQL.Text = "select";//substr(A.BMDM,0,length(A.BMDM)-2) PDM,
            query.SQL.Add("A.BMDM DM,A.BMMC MC,A.SHDM,A.SHBMID,(select max(BMDM) from SHBM B where A.SHDM=B.SHDM and A.BMDM like B.BMDM||'%' and A.BMDM<>B.BMDM) PDM");
            query.SQL.Add("from SHBM A where length(BMDM)<=:LEVLE");
            query.SQL.Add("and A.SHDM=:SHDM and A.BMDM like :BMDM");//and length(A.BMDM)>:BMDMCD
            query.SQL.Add("order by SHDM,BMDM");
            query.ParamByName("SHDM").AsString = param.sSHDM;
            query.ParamByName("BMDM").AsString = param.sBMDM + "%";
            //query.ParamByName("BMDMCD").AsInteger = param.sBMDM.Length;
            query.ParamByName("LEVLE").AsInteger = A;
            query.Open();
            while (!query.Eof)
            {
                SHBM obj = new SHBM();
                obj.iSHBMID = query.FieldByName("SHBMID").AsInteger;
                obj.sBMDM = query.FieldByName("DM").AsString;
                obj.sPBMDM = query.FieldByName("PDM").AsString;
                //if (query.FieldByName("DM").AsString.Length % 2 == 0)
                //{
                //    obj.sPDM = obj.sDM.Substring(0, obj.sDM.Length - 2);
                //}
                //else
                //{
                //    if (obj.sDM.Length >= 5)
                //        obj.sPDM = obj.sDM.Substring(0, obj.sDM.Length - 5);
                //}
                obj.sBMMC = query.FieldByName("MC").AsString.Replace("'", "′");
                obj.sBMQC = query.FieldByName("DM").AsString + " " + obj.sBMMC;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillTreeSHSPFL(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<SHSPFL> lst = new List<SHSPFL>();
            query.SQL.Text = "select(select max(C.SPFLDM) from SHSPFL C where D.SPFLDM like C.SPFLDM ||'%' and D.SPFLDM<>C.SPFLDM) PSHSPFLDM";
            query.SQL.Add(",D.* from SHSPFL D where 1=1");
            query.SQL.Add("   and D.SHDM=:SHDM");
            query.ParamByName("SHDM").AsString = param.sSHDM;
            query.Open();
            while (!query.Eof)
            {
                SHSPFL one = new SHSPFL();
                one.sSPFLDM = query.FieldByName("SPFLDM").AsString;
                one.sSPFLMC = query.FieldByName("SPFLMC").AsString;
                one.sPSPFLDM = query.FieldByName("PSHSPFLDM").AsString;
                one.iSHSPFLID = query.FieldByName("SHSPFLID").AsInteger;
                lst.Add(one);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillSHMD(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<MOBILE_SHDY> lst = new List<MOBILE_SHDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select SHDM,SHMC from MOBILE_SHDY order by SHDM";

                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    MOBILE_SHDY _item = new MOBILE_SHDY();
                    _item.sSHDM = query.FieldByName("SHDM").AsString;
                    _item.sSHMC = query.FieldByName("SHMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillQYMD(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<MOBILE_QYDY> lst = new List<MOBILE_QYDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select QYID,QYMC from MOBILE_QYDY order by QYID";

                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    MOBILE_QYDY _item = new MOBILE_QYDY();
                    _item.iQYID = query.FieldByName("QYID").AsInteger;
                    _item.sQYMC = query.FieldByName("QYMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static int GetBMJC(string pBMJC, int pLevel)
        {
            int iLen = 0;
            for (int i = 0; i < pBMJC.Length && i < pLevel; i++)
                iLen += Convert.ToInt32(pBMJC[i].ToString());
            return iLen;
        }
        public static string GetBMJC_Full(string pBMJC)
        {
            string sBMJC_Full = string.Empty;
            int iLen = 0;
            for (int i = 0; i < pBMJC.Length; i++)
            {
                iLen += Convert.ToInt32(pBMJC[i].ToString());
                sBMJC_Full += iLen.ToString() + ",";
            }
            return sBMJC_Full;
        }

        public static string FillHYSQ(out string msg, int pMDID)
        {
            msg = string.Empty;
            List<SQDY> lst = new List<SQDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.SQL.Text = " select * from  SQDY where MDID=" + pMDID;
                query.SQL.Add("order by SQID");
                query.Open();
                while (!query.Eof)
                {
                    SQDY obj = new SQDY();
                    obj.iSQID = query.FieldByName("SQID").AsInteger;
                    obj.sSQMC = query.FieldByName("SQMC").AsString;
                    obj.sSQMS = query.FieldByName("SQMS").AsString;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }


        public static string FillHYQY(out string msg, CyQuery query, CRMLIBASHX obj)//int BJ_MJ = 3
        {
            msg = string.Empty;
            List<HYQY> lst = new List<HYQY>();
            if (obj.iMODE == 0)
            {
                query.SQL.Text = "select (select max(QYDM) from HYK_HYQYDY B where A.QYDM like B.QYDM||'%' and A.QYDM<>B.QYDM) PQYDM";
                query.SQL.Add("  ,(A.QYDM||' '||A.QYMC) QYQC,A.* from HYK_HYQYDY A where BJ_MJ is null OR BJ_MJ=0 order by QYDM");
            }
            else
            {
                query.SQL.Text = "select (select max(QYDM) from HYK_HYQYDY B where A.QYDM like B.QYDM||'%' and A.QYDM<>B.QYDM) PQYDM";
                query.SQL.Add("  ,(A.QYDM||' '||A.QYMC) QYQC,A.* from HYK_HYQYDY A where 1=1 order by QYDM");
            }
            query.Open();
            while (!query.Eof)
            {
                HYQY item = new HYQY();
                item.iJLBH = query.FieldByName("QYID").AsInteger;
                item.sQYDM = query.FieldByName("QYDM").AsString;
                item.sPQYDM = query.FieldByName("PQYDM").AsString;
                item.sQYMC = query.FieldByName("QYMC").AsString;
                item.sQYQC = query.FieldByName("QYQC").AsString;
                item.sYZBM = query.FieldByName("YZBM").AsString;
                item.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                lst.Add(item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillLMSHLX(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<MOBILE_LMSHDY> lst = new List<MOBILE_LMSHDY>();
            query.SQL.Text = "select LXID,LXMC from MOBILE_LMSHLXDEF order by LXID";
            query.Open();
            while (!query.Eof)
            {
                MOBILE_LMSHDY _item = new MOBILE_LMSHDY();
                _item.iID = query.FieldByName("LXID").AsInteger;
                _item.sMC = query.FieldByName("LXMC").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillLCMD(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<MOBILE_MDDY> lst = new List<MOBILE_MDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MDID,MDMC from MOBILE_MDDY order by MDID";

                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    MOBILE_MDDY _item = new MOBILE_MDDY();
                    _item.iMDID = query.FieldByName("MDID").AsInteger;
                    _item.sMDMC = query.FieldByName("MDMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillMDCT(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<MOBILE_MDDY> lst = new List<MOBILE_MDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MDID,MDMC from MOBILE_MDDY  order by MDID";

                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    MOBILE_MDDY _item = new MOBILE_MDDY();
                    _item.iMDID = query.FieldByName("MDID").AsInteger;
                    _item.sMDMC = query.FieldByName("MDMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCTLX(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CY_CTLXDY> lst = new List<CY_CTLXDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select CTLXID,CTLXNAME from CY_CTLXDY  ";
                query.SQL.Add(" order by CTLXID");
                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    CY_CTLXDY _item = new CY_CTLXDY();
                    _item.iCTLXID = query.FieldByName("CTLXID").AsInteger;
                    _item.sCTLXNAME = query.FieldByName("CTLXNAME").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillCT(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CY_CTXXDY> lst = new List<CY_CTXXDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select CTID,CTNAME from CY_CTXXDY  order by CTID";

                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    CY_CTXXDY _item = new CY_CTXXDY();
                    _item.iCTID = query.FieldByName("CTID").AsInteger;
                    _item.sCTMC = query.FieldByName("CTNAME").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillLPFLDEF(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<APP_LPFLDY> lst = new List<APP_LPFLDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select LPFLID,LPFLMC from LPFLDEF where BJ_TY=0 and BJ_MJ=1 order by LPFLDM";
                query.Open();
                while (!query.Eof)
                {
                    APP_LPFLDY _item = new APP_LPFLDY();
                    _item.iLPFLID = query.FieldByName("LPFLID").AsInteger;
                    _item.sLPFLMC = query.FieldByName("LPFLMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillLPFLTree(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<LPFLDY> lst = new List<LPFLDY>();
            query.SQL.Text = "select ( select MAX(B.LPFLDM) from LPFLDEF B where A.LPFLDM like B.LPFLDM|| '%' and  A.LPFLDM <> B.LPFLDM )  PLPFLDM";
            query.SQL.Add(" ,(A.LPFLDM||' '||A.LPFLMC) LPFLQC,A.* FROM LPFLDEF A WHERE 1=1 ");
            if (param.iBJ_TY == 1)
            {
                query.SQL.Add(" and A.BJ_TY=0  and not exists(select 1 from LPFLDEF C where C.BJ_TY = 1 and A.LPFLDM like C.LPFLDM||'%') ");
            }
            query.SQL.Add(" order by LPFLDM");
            query.Open();
            while (!query.Eof)
            {
                LPFLDY obj = new LPFLDY();
                obj.iJLBH = query.FieldByName("LPFLID").AsInteger;
                obj.sLPFLDM = query.FieldByName("LPFLDM").AsString;
                obj.sPLPFLDM = query.FieldByName("PLPFLDM").AsString;
                obj.sLPFLMC = query.FieldByName("LPFLMC").AsString;
                obj.sLPFLQC = query.FieldByName("LPFLQC").AsString;
                obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        //public static string FillLPFLTree(out string msg)
        //{
        //    msg = string.Empty;
        //    string sql = string.Empty;
        //    List<LPFLDY> lst = new List<LPFLDY>();
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
        //    CyQuery query = new CyQuery(conn);
        //    try
        //    {
        //        query.Close();
        //        query.SQL.Text = "select ( select MAX(B.LPFLDM) from LPFLDEF B WHERE A.LPFLDM LIKE B.LPFLDM|| '%' AND  A.LPFLDM <> B.LPFLDM)  PLPFLDM";
        //        query.SQL.Add(" ,(A.LPFLDM||' '||A.LPFLMC) LPFLQC,A.* FROM LPFLDEF A WHERE A.BJ_TY=0 and not exists(select 1 from LPFLDEF P where P.BJ_TY=1 and A.LPFLDM like P.LPFLDM||'%') order by LPFLDM");

        //        query.Open();
        //        while (!query.Eof)
        //        {
        //            LPFLDY obj = new LPFLDY();
        //            obj.iJLBH = query.FieldByName("LPFLID").AsInteger;
        //            obj.sLPFLDM = query.FieldByName("LPFLDM").AsString;
        //            obj.sPLPFLDM = query.FieldByName("PLPFLDM").AsString;
        //            obj.sLPFLMC = query.FieldByName("LPFLMC").AsString;
        //            obj.sLPFLQC = query.FieldByName("LPFLQC").AsString;
        //            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
        //            obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
        //            lst.Add(obj);
        //            query.Next();
        //        }
        //        query.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is MyDbException)
        //            throw e;
        //        else
        //            msg = e.Message;
        //        throw new MyDbException(e.Message, query.SqlText);

        //    }
        //    conn.Close();
        //    return JsonConvert.SerializeObject(lst);
        //}

        public static string FillCZLX(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CY_CZLXDY> lst = new List<CY_CZLXDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select CZLXID,CZLXNAME from CY_CZLXDY  ";
                query.SQL.Add(" order by CZLXID");
                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    CY_CZLXDY _item = new CY_CZLXDY();
                    _item.iCZLXID = query.FieldByName("CZLXID").AsInteger;
                    _item.sCZLXNAME = query.FieldByName("CZLXNAME").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillCPLX(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<CY_CPLXDY> lst = new List<CY_CPLXDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select CPLXID,CPLXNAME from CY_CPLXDY  ";
                query.SQL.Add(" order by CPLXID");
                query.Open();
                //DataTable dt = query.GetDataTable(); 
                while (!query.Eof)
                {
                    CY_CPLXDY _item = new CY_CPLXDY();
                    _item.iCPLXID = query.FieldByName("CPLXID").AsInteger;
                    _item.sCPLXNAME = query.FieldByName("CPLXNAME").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillAPPJPJC(out string msg, int iMode)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<JPJC> lst = new List<JPJC>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from MOBILE_JCDEF where 1=1";

                query.SQL.Add(" order by JC");
                query.Open();
                while (!query.Eof)
                {
                    JPJC obj = new JPJC();
                    obj.iJC = query.FieldByName("JC").AsInteger;
                    obj.sMC = query.FieldByName("MC").AsString;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillBQXMTree(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<LABEL_XM> lst = new List<LABEL_XM>();
            query.SQL.Text = "select (select max(LABELXMDM) from LABEL_XM B where A.LABELXMDM like B.LABELXMDM||'%' and A.LABELXMDM<>B.LABELXMDM) PLABELXMDM";
            query.SQL.Add("  ,(A.LABELXMDM||' '||A.LABELXMMC) LABELXMQC,A.* from LABEL_XM A where LABELLBID = " + param.iLABELLBID);
            query.SQL.Add("  order by LABELXMDM");
            query.Open();
            while (!query.Eof)
            {
                LABEL_XM obj = new LABEL_XM();
                obj.iLABELXMID = query.FieldByName("LABELXMID").AsInteger;
                obj.sLABELXMDM = query.FieldByName("LABELXMDM").AsString;
                obj.sPLABELXMDM = query.FieldByName("PLABELXMDM").AsString;
                obj.sLABELXMMC = query.FieldByName("LABELXMMC").AsString;
                obj.sLABELXMQC = query.FieldByName("LABELXMQC").AsString;
                obj.sLABELXMMS = query.FieldByName("LABELXMMS").AsString;
                obj.iLABELLBID = query.FieldByName("LABELLBID").AsInteger;
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                obj.iBJ_WY = query.FieldByName("BJ_WY").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillSQTree(out string msg, CyQuery query, CRMLIBASHX param)// int pMDID)
        {
            msg = string.Empty;
            List<SQDY> lst = new List<SQDY>();
            query.SQL.Text = " select * from  SQDY where MDID=" + param.iMDID;
            query.SQL.Add("order by SQID");
            query.Open();
            while (!query.Eof)
            {
                SQDY obj = new SQDY();
                obj.iSQID = query.FieldByName("SQID").AsInteger;
                obj.sSQMC = query.FieldByName("SQMC").AsString;
                obj.sSQMS = query.FieldByName("SQMS").AsString;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillYH(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<YHXX> lst = new List<YHXX>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from YHXX  where 1=1";
                query.SQL.Add(" order by YHID asc");
                query.Open();
                while (!query.Eof)
                {
                    YHXX obj = new YHXX();
                    obj.iJLBH = query.FieldByName("YHID").AsInteger;
                    obj.sYHMC = query.FieldByName("YHMC").AsString;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillZFFSTree(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<ZFFS> lst = new List<ZFFS>();
            query.SQL.Text = "select (select max(ZFFSDM) from ZFFS B where A.ZFFSDM like B.ZFFSDM||'%' and A.ZFFSDM<>B.ZFFSDM) PZFFSDM";
            query.SQL.Add(" ,(A.ZFFSDM||' '||A.ZFFSMC) ZFFSQC,A.* from ZFFS A where 1=1 order by ZFFSDM");
            query.Open();
            while (!query.Eof)
            {
                ZFFS obj = new ZFFS();
                obj.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                obj.sPZFFSDM = query.FieldByName("PZFFSDM").AsString;
                obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                obj.sZFFSQC = query.FieldByName("ZFFSQC").AsString;
                obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                obj.iBJ_DZQDCZK = query.FieldByName("BJ_DZQDCZK").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillZFFS(out string msg, CyQuery query, CRMLIBASHX item)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<ZFFS> lst = new List<ZFFS>();
            query.SQL.Text = "select * from ZFFS where BJ_MJ=1";
            query.SQL.Add(" order by ZFFSDM asc");
            query.Open();
            while (!query.Eof)
            {
                ZFFS obj = new ZFFS();
                obj.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillSHZFFS(out string msg, string shdm)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<SHZFFS> lst = new List<SHZFFS>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from SHZFFS where MJBJ=1 ";
                if (shdm != "")
                {
                    query.SQL.Add("and SHDM='" + shdm + "' ");
                }
                query.SQL.Add(" order by ZFFSDM asc");
                query.Open();
                while (!query.Eof)
                {
                    SHZFFS obj = new SHZFFS();
                    obj.iSHZFFSID = query.FieldByName("SHZFFSID").AsInteger;
                    obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                    obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    obj.sSHDM = query.FieldByName("SHDM").AsString;

                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static HYXX_Detail GetBGDDDM(out string msg, int iDJR, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "(select PERSON_ID from (select PERSON_ID from XTCZY_BGDDQX T where BGDDDM = '00'";
                    query.SQL.Add("intersect select PERSON_ID  from XTCZY_BGDDQX T where BGDDDM = '01'");
                    query.SQL.Add("intersect select PERSON_ID  from XTCZY_BGDDQX T where BGDDDM = '02'");
                    query.SQL.Add("intersect select PERSON_ID  from XTCZY_BGDDQX T where BGDDDM = '03'");
                    query.SQL.Add("intersect select PERSON_ID  from XTCZY_BGDDQX T where BGDDDM = '05'");
                    query.SQL.Add("intersect select PERSON_ID  from XTCZY_BGDDQX T where BGDDDM = '10') where PERSON_ID =" + iDJR + ")");
                    query.SQL.Add("union");
                    query.SQL.Add("(select PERSON_ID from(select X.PERSON_ID from CZYGROUP_BGDDQX Q,XTCZYGRP X where Q.BGDDDM = '00' and X.GROUPID=Q.ID");
                    query.SQL.Add("intersect select X.PERSON_ID from CZYGROUP_BGDDQX Q,XTCZYGRP X where Q.BGDDDM = '01' and X.GROUPID=Q.ID ");
                    query.SQL.Add("intersect select X.PERSON_ID from CZYGROUP_BGDDQX Q,XTCZYGRP X where Q.BGDDDM = '02' and X.GROUPID=Q.ID ");
                    query.SQL.Add("intersect select X.PERSON_ID from CZYGROUP_BGDDQX Q,XTCZYGRP X where Q.BGDDDM = '03' and X.GROUPID=Q.ID ");
                    query.SQL.Add("intersect select X.PERSON_ID from CZYGROUP_BGDDQX Q,XTCZYGRP X where Q.BGDDDM = '05' and X.GROUPID=Q.ID ");

                    query.SQL.Add("intersect select X.PERSON_ID from CZYGROUP_BGDDQX Q,XTCZYGRP X where Q.BGDDDM = '10' and X.GROUPID=Q.ID ) where PERSON_ID =" + iDJR + ")");
                    query.Open();
                    if (!query.IsEmpty)
                        obj.sBGDDDM = "all";
                    else
                    {
                        query.Close();
                        query.SQL.Text = "select X.BGDDDM,B.BGDDMC from XTCZY_BGDDQX X,HYK_BGDD B  where X.BGDDDM = B.BGDDDM and X.PERSON_ID=:RYID and B.XS_BJ = 1";
                        query.SQL.Add(" union");
                        query.SQL.Add(" select q.BGDDDM,B.BGDDMC from CZYGROUP_BGDDQX Q,XTCZYGRP G ,HYK_BGDD B where Q.ID=G.GROUPID and  Q.BGDDDM = B.BGDDDM and  G.PERSON_ID=:RYID and B.XS_BJ = 1");
                        query.ParamByName("RYID").AsInteger = iDJR;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            //obj.sBGDDDM = "'" + query.FieldByName("BGDDDM").AsString + "'";
                            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        }
                    }
                    query.Close();
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
            return obj;
        }
        public static string SearchHYZLX(out string msg, HYZLX Obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from HYZLXDY H,HYK_HYGRP G  where H.HYZLXID=G.HYZLXID";
                query.SQL.Add("  and H.HYZLXID=" + Obj.iHYZLXID);
                query.Open();
                while (!query.Eof)
                {
                    HYZLX item = new HYZLX();
                    item.iJLBH = query.FieldByName("GRPID").AsInteger;
                    item.sHYZLXMC = query.FieldByName("HYZLXMC").AsString;
                    item.iGRPID = query.FieldByName("GRPID").AsInteger;
                    item.sGRPMC = query.FieldByName("GRPMC").AsString;
                    lst.Add(item);
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
                throw new MyDbException(e.Message, query.SqlText);
            }
            finally
            {
                conn.Close();
            }
            return Obj.GetTableJson(lst);
        }

        public static string FillHYZLXTree(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<HYZLX> lst = new List<HYZLX>();
            query.SQL.Text = "select (select max(HYZLXDM) from HYZLXDY B where A.HYZLXDM like B.HYZLXDM||'%' and A.HYZLXDM<>B.HYZLXDM) PHYZLXDM";
            query.SQL.Add(" ,(A.HYZLXDM||' '||A.HYZLXMC) HYZLXQC,A.* from HYZLXDY A where 1=1 order by HYZLXDM");
            query.Open();
            while (!query.Eof)
            {
                HYZLX obj = new HYZLX();
                obj.iHYZLXID = query.FieldByName("HYZLXID").AsInteger;
                obj.sHYZLXDM = query.FieldByName("HYZLXDM").AsString;
                obj.sPHYZLXDM = query.FieldByName("PHYZLXDM").AsString;
                obj.sHYZLXMC = query.FieldByName("HYZLXMC").AsString;
                //obj.sHYZLXQC = query.FieldByName("HYZLXQC").AsString;
                obj.iBJ_MJ = query.FieldByName("BJ_MJ").AsInteger;
                lst.Add(obj);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static HYXX_Detail GetGLDBYHYXX(out string msg, string pHYK_NO, string sDBConnName = "CRMDM")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    // query.SQL.Text = "  select H.HYID,G.GKID,G.SJHM,H.HY_NAME from KH_GKRYXX K, HYK_HYXX H,HYK_GKDA G where K.HYID<>H.HYID and G.GKID=H.GKID and H.HYKTYPE in(103,106,108) and hyk_no='"+pHYK_NO+"'";
                    //   query.ParamByName("HYK_NO").AsString = pHYK_NO;
                    query.SQL.Text = " select distinct H.HYID,G.GKID,G.SJHM,H.HY_NAME from KH_GKRYXX K, HYK_HYXX H,HYK_GKDA G ";
                    query.SQL.Add("  where G.GKID=H.GKID and H.HYKTYPE in(103,106,108) and hyk_no='" + pHYK_NO + "'");
                    query.SQL.Add("  and  H.HYID not in(select K.HYID from KH_GKRYXX K, HYK_HYXX H where H.HYID=K.HYID  )");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        obj.sSJHM = query.FieldByName("SJHM").AsString;
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }

        //验证查询
        public static string GetWXSIGNData(out string msg, DateTime dKSRQ, DateTime dJSRQ)
        {
            msg = string.Empty;
            CrmProc.GTPT.GTPT_WXQDSJFGZDY obj = new CrmProc.GTPT.GTPT_WXQDSJFGZDY();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select JLBH  from MOBILE_SIGN_GZ  where ";
                query.SQL.Add(" KSRQ=:KSRQ and JSRQ=:JSRQ and  status!=3");
                query.ParamByName("KSRQ").AsDateTime = Convert.ToDateTime(dKSRQ);
                query.ParamByName("JSRQ").AsDateTime = Convert.ToDateTime(dJSRQ);
                query.Open();
                while (!query.Eof)
                {

                    obj.iJLBH = query.FieldByName("JLBH").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(obj);
        }
        public static void GetINX(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(INX)INX from WX_SB ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iINX = query.FieldByName("INX").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static void GetGGXH(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(GG_ID) XH from ADV_CONTENT ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iINX = query.FieldByName("XH").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static void GetXH(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(XH) XH from WX_HYQYITEM ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iINX = query.FieldByName("XH").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static void GetJTWYXH(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(XH) XH from WX_HTMLITEM ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iXH = query.FieldByName("XH").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static void GetWDCXH(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(XH) XH from WX_DCDYDITEM ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iXH = query.FieldByName("XH").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static void GetWDCPAGE_ID(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(PAGE_ID) PAGE_ID from WX_DCDYDITEM ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iPAGE_ID = query.FieldByName("PAGE_ID").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static void GetSHLMINX(out string msg, ref INX obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(INX) INX from WX_SH ";


                query.Open();
                while (!query.Eof)
                {

                    obj.iINX = query.FieldByName("INX").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static HYXX_Detail GetHYK_NOData(out string msg, string pHYK_NO, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select A.* from HYK_HYXX A  where  a.wx_card=1 and A.HYK_NO= ";
                    query.SQL.Add(" '" + pHYK_NO + "'" + "");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }

        //查询门店名称
        public static HYXX_Detail GetWXHYXX(out string msg, string pHYK_NO, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select A.HYID,A.HYKTYPE,B.HYKNAME,C.LX,C.DJSJ,C.OPENID,C.UNIONID,C.PUBLICID from HYK_HYXX A,HYKDEF B,WX_BINDCARDJL  C  where  A.HYKTYPE=B.HYKTYPE  and  A.HYK_NO= ";
                    query.SQL.Add(" '" + pHYK_NO + "'" + "and  C.HYID=A.HYID and ROWNUM<=1  order by C.DJSJ");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        obj.iLX = query.FieldByName("LX").AsInteger;
                        obj.sUNIONID = query.FieldByName("UNIONID").AsString;
                        obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
                        obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.sOPENID = query.FieldByName("OPENID").AsString;
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }
        //查询微信号是否存在
        public static HYXX_Detail GetWX_NO(out string msg, string pWX_NO, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select OPENID,WX_NO from WX_USER WHERE WX_NO='" + pWX_NO + "'";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sOPENID = query.FieldByName("OPENID").AsString;
                        obj.sWX_NO = query.FieldByName("WX_NO").AsString;
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }
        //查询会员卡号是否存在
        public static HYXX_Detail GetHYK_NO(out string msg, string pHYK_NO, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select HYID,HYK_NO  from HYK_HYXX  WHERE HYK_NO='" + pHYK_NO + "'";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }

        public static string GetGRXX(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            query.SQL.Text = "select H.*,G.SJHM,G.SFZBH,G.SEX,D.HYKNAME from HYK_HYXX H, HYK_GRXX G,HYKDEF D where H.HYID = G.HYID(+) and H.HYKTYPE=D.HYKTYPE";
            query.SQL.Add("and H.HYK_NO ='" + param.sHYK_NO + "'");
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                obj.sSJHM = query.FieldByName("SJHM").AsString;
                obj.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                obj.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
                obj.sSFZBH = query.FieldByName("SFZBH").AsString;
            }
            else
            {
                msg = CrmLibStrings.msgHYXXNotFound;
                return null;
            }
            query.Close();
            return JsonConvert.SerializeObject(obj);

        }
        //public static HYXX_Detail GetGRXX(out string msg, string pHYK_NO, string sDBConnName = "CRMDB") //通过会员卡号获取个人信息
        //{
        //    msg = string.Empty;
        //    HYXX_Detail obj = new HYXX_Detail();
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        try
        //        {
        //            query.SQL.Text = "select H.*,G.SJHM,G.SFZBH,G.SEX,D.HYKNAME from HYK_HYXX H, HYK_GRXX G,HYKDEF D where H.HYID = G.HYID(+) and H.HYKTYPE=D.HYKTYPE";
        //            query.SQL.Add("and H.HYK_NO ='" + pHYK_NO + "'");
        //            query.Open();
        //            if (!query.IsEmpty)
        //            {
        //                obj.iHYID = query.FieldByName("HYID").AsInteger;
        //                obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
        //                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
        //                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
        //                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
        //                obj.sSJHM = query.FieldByName("SJHM").AsString;
        //                obj.iKFRYID = query.FieldByName("KFRYID").AsInteger;
        //                obj.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
        //                obj.sSFZBH = query.FieldByName("SFZBH").AsString;
        //            }
        //            else
        //            {
        //                msg = CrmLibStrings.msgHYXXNotFound;
        //                return null;
        //            }
        //            query.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is MyDbException)
        //                throw e;
        //            else
        //                msg = e.Message;
        //            throw new MyDbException(e.Message, query.SqlText);
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return obj;
        //}

        public static HYXX_Detail GetHYSJXX(out string msg, int pHYID, string pHYK_NO, string pCDNR, string sDBConnName = "CRMDB", int iBJ_SJ = 1)
        {
            //获取会员信息，必须传入HYID或HYK_NO或CDNR
            //CDNR、HYK_NO不为空参与判断、HYID>0参与判断，判断顺序CDNR、HYID、HYK_NO
            //只能查一张卡
            msg = string.Empty;
            if (pHYID == 0 && pHYK_NO == "" && pCDNR == "")
            {
                msg = CrmLibStrings.msgHYXXNotFound;
                return null;
            }
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select  L.*,H.HYK_NO,H.HY_NAME,K.HYKNAME,N.HYKNAME HYKNAME_NEW ,H.STATUS KSTATUS ,H.JKRQ,H.FXDW from   HYDJSQJL L,HYK_HYXX H ,HYKDEF K,HYKDEF N";
                    query.SQL.Add("  where L.HYID=H.HYID and L.Hyktype=K.HYKTYPE and L.HYKTYPE_NEW=N.HYKTYPE    ");
                    query.SQL.Add("  and L.BJ_SJ=" + iBJ_SJ + " ");
                    // query.SQL.Add("  and L.BJ_SJ=" + iBJ_SJ + "  and ( L.status=0 or L.STATUS is null)");
                    if (pCDNR != "")
                        query.SQL.Add(" and CDNR='" + pCDNR + "'");
                    if (pHYID != 0)
                        query.SQL.Add(" and L.HYID=" + pHYID);
                    if (pHYK_NO != "")
                        query.SQL.Add(" and H.HYK_NO='" + pHYK_NO + "'");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        obj.iHYKTYPE_NEW = query.FieldByName("HYKTYPE_NEW").AsInteger;
                        obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        obj.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
                        obj.fBQJF = query.FieldByName("BQJF").AsFloat;
                        obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        obj.iSTATUS = query.FieldByName("KSTATUS").AsInteger;
                        obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                        obj.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
                        obj.iFXDW = query.FieldByName("FXDW").AsInteger;
                        obj.iGZID = query.FieldByName("GZID").AsInteger;
                        obj.iINX = query.FieldByName("INX").AsInteger;
                        //query.Close();
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }
        public static HYXX_Detail GetHKdata(out string msg, string pHYK_NO)
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select A.HYKHM_NEW from  HYK_CZK_WK A where A.CZJPJ_JLBH =(";
                    query.SQL.Add("  select MAX(CZJPJ_JLBH) from HYK_CZK_WK B where  B.HYID =(");
                    query.SQL.Add("  select C.HYID from HYK_CZK_WK C where C.HYKHM_OLD='" + pHYK_NO + "') )");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sHYKHM_NEW = query.FieldByName("HYKHM_NEW").AsString;
                    }
                    else
                    {
                        msg = CrmLibStrings.msgHYXXNotFound;
                        return null;
                    }
                    query.Close();
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
            return obj;
        }
        public static string GetJFBL(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            HYXF_SHJFGZ_Proc obj = new HYXF_SHJFGZ_Proc();
            query.SQL.Text = "select * from SHJFGZ S WHERE ID=" + param.iID;
            query.Open();
            if (!query.IsEmpty)
            {
                obj.fJF = query.FieldByName("JF").AsFloat;
                obj.fJE = query.FieldByName("JE").AsFloat;
            }
            query.Close();
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetHYXX(out string msg, CyQuery query, CRMLIBASHX param)// obj.iHYID, obj.sHYK_NO, obj.sCDNR, obj.sDBConnName, obj.iHYKTYPE
        {
            //获取会员信息，必须传入HYID或HYK_NO或CDNR
            //CDNR、HYK_NO不为空参与判断、HYID>0参与判断，判断顺序CDNR、HYID、HYK_NO
            //只能查一张卡
            msg = string.Empty;
            if (param.iHYID == 0 && param.sHYK_NO == "" && param.sCDNR == "" && param.sSJHM == "")
            {
                msg = CrmLibStrings.msgHYXXNotFound;
                return "";
            }
            HYXX_Detail obj = new HYXX_Detail();

            if (GlobalVariables.SYSConfig.bCDNR2JM)
                param.sCDNR = EncCDNR(param.sCDNR);


            query.SQL.Text = "select H.*,D.HYKNAME,D.BJ_CZZH,F.FXDWMC,F.FXDWDM,M.MDMC,PERSON_NAME KHJLMC,G.YZBM,B.BGDDDM,B.BGDDMC,G.IMAGEURL,D.BJ_FSK,D.KFJE";
            query.SQL.AddLine("  ,D.KXBJ,D.TKBJ,D.ZFBJ");
            query.SQL.Add(" from HYK_HYXX H,HYKDEF D,FXDWDEF F,MDDY M,RYXX R,HYK_GKDA G,HYK_BGDD B");
            query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and H.FXDW=F.FXDWID(+) and H.MDID=M.MDID(+) ");
            query.SQL.Add(" and H.GKID=G.GKID(+) and G.KHJLRYID=R.PERSON_ID(+) and H.YBGDD=B.BGDDDM(+) ");//and (H.MAINHYID is null or H.MAINHYID=0)
            if (param.sCDNR != "")
                query.SQL.Add(" and CDNR='" + param.sCDNR + "'");
            if (param.iHYID != 0)
                query.SQL.Add(" and HYID=" + param.iHYID);
            if (param.sHYK_NO != "")
                query.SQL.Add(" and H.HYK_NO = '" + param.sHYK_NO + "'");//改成模糊匹配
            if (param.sSJHM != "")
            {
                query.SQL.Add(" and G.SJHM = '" + param.sSJHM + "'");
                query.SQL.Text = query.SQL.Text.Replace("H.GKID=G.GKID(+)", "H.GKID=G.GKID");
            }
            //这是在逗我吗、
            //if (param.sHYK_NO != "")
            //    query.SQL.Add(" and H.HYK_NO like '%" + param.sHYK_NO + "%'");//改成模糊匹配
            query.Open();
            bool bChild = false;
            //if (query.IsEmpty)
            //{
            //    msg = CrmLibStrings.msgHYXXNotFound;
            //    return null;

            //    //查询子卡表 目前没有用这个表
            //    //query.SQL.Text = "select C.*,H.HYKTYPE,H.GKID,H.YXQ,D.HYKNAME,F.FXDWMC,F.FXDWDM,M.MDMC,PERSON_NAME KHJLMC,G.YZBM,B.BGDDDM,B.BGDDMC ";
            //    //query.SQL.Add(" from HYK_HYXX H,HYKDEF D,FXDWDEF F,MDDY M,RYXX R,HYK_GKDA G,HYK_CHILD_JL C,HYK_BGDD B");
            //    //query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE and C.FXDW=F.FXDWID(+) and C.MDID=M.MDID(+) and H.GKID=G.GKID(+) ");
            //    //query.SQL.Add(" and G.KHJLRYID=R.PERSON_ID(+) and H.HYID=C.HYID and H.YBGDD=B.BGDDDM(+)");
            //    //if (pCDNR != "")
            //    //    query.SQL.Add(" and C.CDNR='" + pCDNR + "'");
            //    //if (pHYID != 0)
            //    //    query.SQL.Add(" and C.HYID=" + pHYID);
            //    //if (pHYK_NO != "")
            //    //    query.SQL.Add(" and C.HYK_NO='" + pHYK_NO + "'");
            //    //query.Open();
            //    //bChild = true;
            //}
            if (!query.IsEmpty)
            {
                if (bChild == false)
                {
                    obj.iMAINHYID = query.FieldByName("MAINHYID").AsInteger;
                }
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                obj.sCDNR = query.FieldByName("CDNR").AsString;
                if (GlobalVariables.SYSConfig.bCDNR2JM)

                    obj.sXKCDNR = DecCDNR(obj.sCDNR);
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString + (bChild ? "(子卡)" : "");
                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                obj.iGKID = query.FieldByName("GKID").AsInteger;
                obj.sKHJLMC = query.FieldByName("KHJLMC").AsString;
                obj.sYZBM = query.FieldByName("YZBM").AsString;
                obj.sIMGURL = query.FieldByName("IMAGEURL").AsString;
                obj.dDJSJ = FormatUtils.DateToString(query.FieldByName("DJSJ").AsDateTime);
                //obj.sTXDZ = query.FieldByName("TXDZ").AsString;
                obj.iBJ_CHILD = bChild ? 1 : 0;
                obj.sPASSWORD = query.FieldByName("PASSWORD").AsString;
                obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                obj.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
                obj.iFXDW = query.FieldByName("FXDW").AsInteger;
                obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                obj.sFXDWDM = query.FieldByName("FXDWDM").AsString;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                obj.iBJ_FSK = query.FieldByName("BJ_FSK").AsInteger;
                obj.fKFJE = query.FieldByName("KFJE").AsFloat;
                obj.sOPENID = query.FieldByName("OPENID").AsString;
                obj.iBJ_GS = query.FieldByName("KXBJ").AsInteger;
                obj.iBJ_TK = query.FieldByName("TKBJ").AsInteger;
                obj.iBJ_ZF = query.FieldByName("ZFBJ").AsInteger;
                query.Close();
                query.SQL.Text = "select nvl(K.BJ_XFSJ,0) BJ_XFSJ,D.BJ_CZZH from HYKKZDEF K,HYKDEF D";
                query.SQL.Add(" where D.HYKKZID=K.HYKKZID and  HYKTYPE=:HYKTYPE");
                query.ParamByName("HYKTYPE").AsInteger = obj.iHYKTYPE;
                query.Open();
                if (!query.IsEmpty)
                {
                    obj.iBJ_XFJE = query.FieldByName("BJ_XFSJ").AsInteger;
                    obj.iBJ_CZZH = query.FieldByName("BJ_CZZH").AsInteger;
                }
                query.Close();
                query.SQL.Text = "select * from HYK_JFZH where HYID=" + obj.iHYID;
                query.Open();
                if (!query.IsEmpty)
                {
                    obj.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    obj.fBQJF = query.FieldByName("BQJF").AsFloat;
                    obj.fBNLJJF = query.FieldByName("BNLJJF").AsFloat;
                    obj.fLJJF = query.FieldByName("LJJF").AsFloat;
                    obj.fXFJE = query.FieldByName("XFJE").AsFloat;
                    obj.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    obj.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    obj.fLJZKJE = query.FieldByName("LJZKJE").AsFloat;
                }
                query.Close();
                query.SQL.Text = "select * from HYK_JEZH where HYID=" + obj.iHYID;
                query.Open();
                if (!query.IsEmpty)
                {
                    obj.fCZJE = query.FieldByName("YE").AsFloat;
                    obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                    obj.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                    obj.fPDJE = query.FieldByName("PDJE").AsFloat;
                    obj.fJYDJJE = query.FieldByName("JYDJJE").AsFloat;
                }
                query.SQL.Text = "select  SUM(JE) JE, SUM(JYDJJE) JYDJJE ,MIN(JSRQ) JSRQ from HYK_YHQZH where HYID=" + obj.iHYID + " and JSRQ+1>sysdate group by HYID  ";
                query.Open();
                if (!query.IsEmpty)
                {
                    obj.fYHQJE = query.FieldByName("JE").AsFloat;
                    obj.fYHQJYDJJE = query.FieldByName("JYDJJE").AsFloat;
                    obj.dYHQJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                }
                if (param.sDBConnName == "CRMDB")
                {
                    query.Close();
                    query.SQL.Text = "select G.*,Q.QYMC from HYK_GKDA G,HYK_HYQYDY Q where G.GKID=" + obj.iGKID;
                    query.SQL.Add(" and G.QYID=Q.QYID(+)");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        //obj.sHY_NAME = query.FieldByName("GK_NAME").AsString;
                        obj.iCANSMS = query.FieldByName("CANSMS").AsInteger;
                        obj.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
                        if (!query.FieldByName("CSRQ").IsNull)
                            obj.iNL = (int)Math.Floor((DateTime.Now - query.FieldByName("CSRQ").AsDateTime).TotalDays / 365);
                        obj.dJHJNR = FormatUtils.DateToString(query.FieldByName("JHJNR").AsDateTime);
                        obj.iQYID = query.FieldByName("QYID").AsInteger;
                        obj.iMZID = query.FieldByName("MZID").AsInteger;
                        obj.iQYID = query.FieldByName("QYID").AsInteger;
                        obj.iBJ_CLD = query.FieldByName("BJ_CLD").AsInteger;
                        obj.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
                        obj.iZYID = query.FieldByName("ZYID").AsInteger;
                        obj.iXLID = query.FieldByName("XLID").AsInteger;
                        obj.iJTSRID = query.FieldByName("JTSRID").AsInteger;
                        obj.iZSCSJID = query.FieldByName("ZSCSJID").AsInteger;
                        obj.iJTGJID = query.FieldByName("JTGJID").AsInteger;
                        obj.iJTCYID = query.FieldByName("JTCYID").AsInteger;
                        obj.sSFZBH = query.FieldByName("SFZBH").AsString;
                        obj.sQYMC = query.FieldByName("QYMC").AsString;
                        obj.sTXDZ = query.FieldByName("TXDZ").AsString;
                        obj.sYZBM = query.FieldByName("YZBM").AsString;
                        obj.sGZDW = query.FieldByName("GZDW").AsString;
                        obj.sZW = query.FieldByName("ZW").AsString;
                        obj.sYYAH = query.FieldByName("YYAH").AsString;
                        obj.sCXXX = query.FieldByName("CXXX").AsString;
                        obj.sXXFS = query.FieldByName("XXFS").AsString;
                        obj.sSJHM = query.FieldByName("SJHM").AsString;
                        obj.sPHONE = query.FieldByName("PHONE").AsString;
                        obj.sBGDH = query.FieldByName("BGDH").AsString;
                        obj.sFAX = query.FieldByName("FAX").AsString;
                        obj.sEMAIL = query.FieldByName("E_MAIL").AsString;
                        obj.sCPH = query.FieldByName("CPH").AsString;
                        obj.sBZ = query.FieldByName("BZ").AsString;
                        obj.sQIYEMC = query.FieldByName("QYMC").AsString;
                        obj.sQIYEXZ = query.FieldByName("QYXZ").AsString;
                        obj.sGKNC = query.FieldByName("GKNC").AsString;
                        obj.iDJR = query.FieldByName("DJR").AsInteger;
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        // obj.dDJSJ = FormatUtils.DateToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.iGXR = query.FieldByName("GXR").AsInteger.ToString();
                        obj.sGXRMC = query.FieldByName("GXRMC").AsString;
                        obj.dGXSJ = FormatUtils.DatetimeToString(query.FieldByName("GXSJ").AsDateTime);
                        obj.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
                        obj.sGK_NAME = query.FieldByName("GK_NAME").AsString;
                    }
                    query.Close();
                    //查询所有父级区域
                    query.SQL.Text = " select B.* from HYK_HYQYDY A,HYK_HYQYDY B   ";
                    query.SQL.Add(" where A.QYDM like B.QYDM||'%' and A.QYDM!=B.QYDM and A.QYID=" + obj.iQYID);
                    query.SQL.Add(" order by B.QYDM desc ");
                    query.Open();
                    while (!query.Eof)
                    {
                        obj.sQYMC = query.FieldByName("QYMC").AsString + "-" + obj.sQYMC;
                        query.Next();
                    }
                    query.Close();
                }
                if (obj.iMAINHYID == 0)
                    obj.iBJ_CHILD = 0;
                else
                {
                    obj.iBJ_CHILD = 1;
                }
            }
            else
            {
                msg = CrmLibStrings.msgHYXXNotFound;
                return "";
            }
            return JsonConvert.SerializeObject(obj);
        }
        public static HYXX_Detail GetFWNR(out string msg, int pFWNRID, int pHYKTYPE, int pHYID, int pMAINHYID = 0, string sDBConnName = "CRMDB")
        {

            msg = string.Empty;
            double pSYFWSL = 0;
            double pYSYFWSL = 0;
            double pFWSL = 0;
            string pKSSJ = FormatUtils.DateToString(DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")));
            string pJSSJ = FormatUtils.DateToString(DateTime.Now.AddDays(1));
            HYXX_Detail obj = new HYXX_Detail();
            //附属卡共享主卡服务相关
            List<int> listFWNRID = new List<int>();
            bool BJ_ZKFW = false;
            int HYKTYPE_ZK = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (pMAINHYID != 0)
                    {
                        listFWNRID.Clear();
                        query.SQL.Text = "  select F.FWNRID,F.FWNRMC,H.HYKTYPE from FWNRDEF F,HYK_XSFWDEF H where F.FWNRID=H.FWNRID ";
                        query.SQL.Add("  and ( H.HYKTYPE IN(SELECT  HYKTYPE FROM HYK_HYXX　where HYID=" + pMAINHYID + " ) )");
                        query.Open();
                        while (!query.Eof)
                        {
                            listFWNRID.Add(query.FieldByName("FWNRID").AsInteger);
                            HYKTYPE_ZK = query.FieldByName("HYKTYPE").AsInteger;
                            query.Next();
                        }
                        query.Close();
                    }
                    if (listFWNRID.Contains(pFWNRID))
                    {
                        BJ_ZKFW = true;
                    }

                    query.SQL.Text = "select sum(H.DFSL) as DFSL, D.BJ_XZFWSL from HYK_HYFWCLJL H,FWNRDEF D where H.FWNRID=:FWNRID and H.HYID=:HYID and (sysdate-(JSRQ+1))<0 and H.FWNRID = D.FWNRID";
                    query.SQL.Add("and CLSJ>='" + pKSSJ + "'");
                    query.SQL.Add("and CLSJ<'" + pJSSJ + "'");
                    query.SQL.Add("group by D.BJ_XZFWSL");
                    query.ParamByName("FWNRID").AsInteger = pFWNRID;
                    query.ParamByName("HYID").AsInteger = BJ_ZKFW == true ? pMAINHYID : pHYID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        if (query.FieldByName("BJ_XZFWSL").AsInteger == 1)
                            pYSYFWSL = query.FieldByName("DFSL").AsFloat;
                        else
                            pYSYFWSL = 0;
                    }
                    else
                    {
                        pYSYFWSL = 0;
                    }
                    query.Close();
                    query.SQL.Text = "select FWSL from HYK_XSFWDEF where FWNRID=:FWNRID and HYKTYPE=:HYKTYPE";
                    query.ParamByName("FWNRID").AsInteger = pFWNRID;
                    query.ParamByName("HYKTYPE").AsInteger = BJ_ZKFW == true ? HYKTYPE_ZK : pHYKTYPE;
                    query.Open();
                    if (!query.IsEmpty)
                        pFWSL = query.FieldByName("FWSL").AsFloat;
                    pSYFWSL = pFWSL - pYSYFWSL;
                    query.Close();
                    query.SQL.Text = "select F.FWNRID,BJ_XZFWSL,ZDFWSL,0.0 as SYFWSL,1.0 FWSL,DW,'' as BZ from HYK_XSFWDEF E,FWNRDEF F";
                    query.SQL.Add(" where E.FWNRID=F.FWNRID and E.HYKTYPE=:HYKTYPE and F.FWNRID=:FWNRID");
                    query.ParamByName("FWNRID").AsInteger = pFWNRID;
                    query.ParamByName("HYKTYPE").AsInteger = BJ_ZKFW == true ? HYKTYPE_ZK : pHYKTYPE;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iBJ_XZFWSL = query.FieldByName("BJ_XZFWSL").AsInteger;
                        obj.fZDFWSL = query.FieldByName("ZDFWSL").AsFloat;
                        obj.fSYFWSL = pSYFWSL;
                        obj.sDW = query.FieldByName("DW").AsString;
                    }
                    query.Close();
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
            return obj;
        }
        public static HYXX_Detail GetMDMC(out string msg, int pDJR, string sDBConnName = "CRMDB")
        {

            msg = string.Empty;
            string pCavans = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select *　　 from(";
                    query.SQL.Text += "  select M.*,S.SHMC from MDDY M,SHDY S where M.SHDM=S.SHDM   "; //增加操作员权限
                    query.SQL.Add("and ( ");
                    query.SQL.Add("  exists(  select 1 from XTCZY_MDQX X where X.PERSON_ID=:RYID and X.MDID=M.MDID)  ");
                    query.SQL.Add("   or exists(select * from CZYGROUP_MDQX X,XTCZYGRP G where G.PERSON_ID=:RYID and X.ID=G.GROUPID and X.MDID=M.MDID)");
                    query.SQL.Add(" ) ");
                    query.SQL.Add("  ) order by MDID asc ");
                    query.ParamByName("RYID").AsInteger = pDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iMDID = query.FieldByName("MDID").AsInteger;
                        obj.sMDMC = query.FieldByName("MDMC").AsString;
                    }
                    query.Close();
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
            return obj;
        }
        //public static int GetMinHYID(int iGKID)
        //{
        //}
        public static HYXX_Detail GetPSW(out string msg, int pHYKTYPE, string sDBConnName = "CRMDB")
        {

            msg = string.Empty;
            string pCavans = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select BJ_PSW from HYKDEF where HYKTYPE=" + pHYKTYPE;
                    query.Open();
                    if (!query.IsEmpty)
                        obj.iBJ_PSW = query.FieldByName("BJ_PSW").AsInteger;
                    query.Close();
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
            return obj;
        }

        public static string GetBQXX(out string msg, CyQuery query, CRMLIBASHX param)
        {

            msg = string.Empty;
            string pCavans = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();

            query.SQL.Text = " select * from ( ";
            query.SQL.Add(" select H.*,L.LABELXMMC,X.LABEL_VALUE,K.HYK_NO from HYK_HYBQ H,LABEL_XM L,LABEL_XMITEM X,HYK_HYXX K  ");
            query.SQL.Add(" where H.LABELXMID = L.LABELXMID and L.LABELXMID =X.LABELXMID and H.LABEL_VALUEID = X.LABEL_VALUEID and H.HYID = K.HYID  ");
            query.SQL.Add(" and K.HYK_NO='" + param.sHYK_NO + "'");
            query.SQL.Add(" order by H.QZ desc");
            query.SQL.Add("  ) where rownum<=4");
            query.Open();
            while (!query.Eof)
            {
                obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                if (obj.dYXQ == "")
                    obj.sCANVAS += query.FieldByName("LABELXMMC").AsString + ":" + query.FieldByName("LABEL_VALUE").AsString + ";";
                else
                {
                    if (DateTime.Compare(DateTime.Now, (DateTime.Parse(obj.dYXQ).AddDays(1))) < 0)
                        obj.sCANVAS += query.FieldByName("LABELXMMC").AsString + ":" + query.FieldByName("LABEL_VALUE").AsString + ";";
                }
                query.Next();
            }
            query.Close();



            return JsonConvert.SerializeObject(obj);
        }
        public static string GetSRXX(out string msg, CyQuery query, CRMLIBASHX param)// string pCSRQ, string sDBConnName = "CRMDB"
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DateTime CSRQ = FormatUtils.ParseDateString(param.dCSRQ);
            System.Globalization.ChineseLunisolarCalendar chinseCaleander = new System.Globalization.ChineseLunisolarCalendar();
            string TreeYear = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
            int intYear = chinseCaleander.GetSexagenaryYear(CSRQ);
            string Tree = TreeYear.Substring(chinseCaleander.GetTerrestrialBranch(intYear) - 1, 1);
            obj.sSX = Tree;
            //string[] shuxiang = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
            //int tmp = CSRQ.Year - 2008;
            //if (CSRQ.Year < 2008)
            //{
            //    obj.sSX = shuxiang[tmp % 12 + 12];
            //}
            //else
            //{
            //    obj.sSX = shuxiang[tmp % 12];
            //}

            float birthdayF = 0.00F;

            if (CSRQ.Month == 1 && CSRQ.Day < 20)
            {
                birthdayF = float.Parse(string.Format("13.{0}", CSRQ.Day.ToString("00")));
            }
            else
            {
                birthdayF = float.Parse(string.Format("{0}.{1}", CSRQ.Month, CSRQ.Day.ToString("00")));
            }
            float[] atomBound = { 1.20F, 2.20F, 3.21F, 4.21F, 5.21F, 6.22F, 7.23F, 8.23F, 9.23F, 10.23F, 11.21F, 12.22F, 13.20F };
            string[] atoms = { "水瓶座", "双鱼座", "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "魔羯座" };

            string ret = string.Empty;
            for (int i = 0; i < atomBound.Length - 1; i++)
            {
                if (atomBound[i] <= birthdayF && atomBound[i + 1] > birthdayF)
                {
                    ret = atoms[i];
                    break;
                }
            }
            obj.sXZ = ret;

            return JsonConvert.SerializeObject(obj);
        }
        public static string GetMZKXX(out string msg, CyQuery query, CRMLIBASHX item)
        {
            //获取会员信息，必须传入HYID或HYK_NO或CDNR
            //CDNR、HYK_NO不为空参与判断、HYID>0参与判断，判断顺序CDNR、HYID、HYK_NO
            //只能查一张卡
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();

            query.SQL.Text = "select H.*,D.HYKNAME,F.FXDWMC,F.FXDWDM,M.MDMC from MZKXX H,HYKDEF D,FXDWDEF F,MDDY M";
            query.SQL.Text += " where H.HYKTYPE=D.HYKTYPE and H.FXDW=F.FXDWID(+) and H.MDID=M.MDID(+) ";
            if (item.sCDNR != "")
            {
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                {
                    item.sCDNR = EncCDNR(item.sCDNR);
                }
                query.SQL.Add(" and CDNR='" + item.sCDNR + "'");
            }
            if (item.iHYID != 0)
                query.SQL.Add(" and HYID=" + item.iHYID);
            if (item.sCZKHM != "")
                query.SQL.Add(" and HYK_NO='" + item.sCZKHM + "'");
            query.Open();
            if (!query.IsEmpty)
            {

                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                obj.sCDNR = query.FieldByName("CDNR").AsString;

                if (GlobalVariables.SYSConfig.bCDNR2JM)

                    obj.sXKCDNR = DecCDNR(obj.sCDNR);


                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                //obj.sHY_NAME = Decrypt2015(obj.iHYID.ToString(), query.FieldByName("HY_NAME").AsString);
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                //obj.sTXDZ = query.FieldByName("TXDZ").AsString;                
                obj.sPASSWORD = query.FieldByName("PASSWORD").AsString;
                obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                obj.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
                obj.iFXDW = query.FieldByName("FXDW").AsInteger;
                obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                obj.sFXDWDM = query.FieldByName("FXDWDM").AsString;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                query.Close();
                //query.SQL.Text = "select " + CyDbSystem.GetIsNullFuncName(conn) + "(K.BJ_XFSJ,0) BJ_XFSJ from HYKKZDEF K,HYKDEF D";
                //query.SQL.Add(" where D.HYKKZID=K.HYKKZID and  HYKTYPE=:HYKTYPE");
                //query.ParamByName("HYKTYPE").AsInteger = obj.iHYKTYPE;
                //query.Open();
                //obj.iBJ_XFJE = query.FieldByName("BJ_XFSJ").AsInteger;
                //query.Close();

                query.SQL.Text = "select * from MZK_JEZH where HYID=" + obj.iHYID;
                query.Open();
                if (!query.IsEmpty)
                {
                    obj.fCZJE = query.FieldByName("YE").AsFloat;
                    obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                    obj.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                    obj.fPDJE = query.FieldByName("PDJE").AsFloat;
                    obj.fJYDJJE = query.FieldByName("JYDJJE").AsFloat;
                }

            }
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetGKDA(out string msg, CyQuery query, CRMLIBASHX param) //ref GKDA obj
        {
            msg = string.Empty;
            if (param.sSFZBH==""&& param.sSJHM==""&& param.sHYK_NO == "")
                return "";
            GKDA obj = null;
            query.SQL.Text = "select G.*,B.GK_NAME TJRYMC,PERSON_NAME KHJLMC";
            //if (obj.sHYK_NO != "")
            //{
            //    query.SQL.Add(",H.HYID ");
            //}
            query.SQL.Add("from HYK_GKDA G,HYK_GKDA B,RYXX C ");
            if (param.sHYK_NO != "")
            {
                query.SQL.Add(" ,HYK_HYXX H ");
            }
            query.SQL.Add(" where G.TJRYID=B.GKID(+) and G.KHJLRYID=C.PERSON_ID(+) ");
            if (param.sHYK_NO != "")
            {
                query.SQL.Add("and H.GKID=G.GKID(+)");
            }

            //if (obj.iGKID != 0)
            //    query.SQL.Add(" and G.GKID=" + obj.iGKID);
            if (param.sSFZBH != "")
                query.SQL.Add(" and G.SFZBH='" + param.sSFZBH + "'");
            //if (obj.iZJLXID != 0)
            //    query.SQL.Add(" and G.ZJLXID=" + obj.iZJLXID);
            if (param.sSJHM != "")
                query.SQL.Add(" and G.SJHM='" + param.sSJHM + "'");
            if (param.sHYK_NO != "")
            {
                query.SQL.Add(" and H.HYK_NO='" + param.sHYK_NO + "'");
            }
            query.Open();
            if (!query.IsEmpty)
            {
                //if (obj.sHYK_NO != "")
                //{
                //    obj.iHYID = query.FieldByName("HYID").AsInteger;
                //}
                obj = new GKDA();
                obj.iGKID = query.FieldByName("GKID").AsInteger;
                obj.sGK_NAME = query.FieldByName("GK_NAME").AsString;
                obj.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
                obj.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
                obj.dJHJNR = FormatUtils.DateToString(query.FieldByName("JHJNR").AsDateTime);
                obj.iQYID = query.FieldByName("QYID").AsInteger;
                obj.iMZID = query.FieldByName("MZID").AsInteger;
                obj.iBJ_CLD = query.FieldByName("BJ_CLD").AsInteger;
                obj.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
                obj.iZYID = query.FieldByName("ZYID").AsInteger;
                obj.iXLID = query.FieldByName("XLID").AsInteger;
                obj.iJTSRID = query.FieldByName("JTSRID").AsInteger;
                obj.iZSCSJID = query.FieldByName("ZSCSJID").AsInteger;
                obj.iJTGJID = query.FieldByName("JTGJID").AsInteger;
                obj.iJTCYID = query.FieldByName("JTCYID").AsInteger;
                obj.iQCPPID = query.FieldByName("QCPPID").AsInteger;
                obj.iJHBJ = query.FieldByName("JHBJ").AsInteger;
                obj.sSFZBH = query.FieldByName("SFZBH").AsString;
                //obj.sQYMC = query.FieldByName("QUYUMC").AsString;
                obj.sTXDZ = query.FieldByName("TXDZ").AsString;
                obj.sYZBM = query.FieldByName("YZBM").AsString;
                obj.sGZDW = query.FieldByName("GZDW").AsString;
                obj.sZW = query.FieldByName("ZW").AsString;
                obj.sYYAH = query.FieldByName("YYAH").AsString;
                obj.sCXXX = query.FieldByName("CXXX").AsString;
                obj.sXXFS = query.FieldByName("XXFS").AsString;
                obj.sSJHM = query.FieldByName("SJHM").AsString;
                obj.sPHONE = query.FieldByName("PHONE").AsString;
                obj.sBGDH = query.FieldByName("BGDH").AsString;
                obj.sFAX = query.FieldByName("FAX").AsString;
                obj.sEMAIL = query.FieldByName("E_MAIL").AsString;
                obj.sCPH = query.FieldByName("CPH").AsString;
                obj.sBZ = query.FieldByName("BZ").AsString;
                obj.sQIYEMC = query.FieldByName("QYMC").AsString;
                obj.sQIYEXZ = query.FieldByName("QYXZ").AsString;
                obj.sGKNC = query.FieldByName("GKNC").AsString;
                obj.iDJR = query.FieldByName("DJR").AsInteger;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                obj.iGXR = query.FieldByName("GXR").AsInteger.ToString();
                obj.sGXRMC = query.FieldByName("GXRMC").AsString;
                obj.dGXSJ = FormatUtils.DatetimeToString(query.FieldByName("GXSJ").AsDateTime);
                obj.iTJRYID = query.FieldByName("TJRYID").AsInteger;
                obj.sTJRYMC = query.FieldByName("TJRYMC").AsString;
                obj.sQQ = query.FieldByName("QQ").AsString;
                obj.sWX = query.FieldByName("WX").AsString;
                obj.sWB = query.FieldByName("WB").AsString;
                obj.sTJRYMC = query.FieldByName("TJRYMC").AsString;
                obj.sCanvas = query.FieldByName("Canvas").AsString;
                obj.sROAD = query.FieldByName("ROAD").AsString;
                obj.sHOUSENUM = query.FieldByName("HOUSENUM").AsString;
                //obj.sIMGURL = query.FieldByName("IMAGEURL").AsString;
            }
            else
            {
                msg = CrmLibStrings.msgGKDANotFound;
            }

            //if (obj.iGKID == 0)
            //{
            //    return;
            //}
            //家庭信息
            //List<HYKGL.HYKGL_GKDALR._JTXX> tp_jtxxs = new List<HYKGL.HYKGL_GKDALR._JTXX>();
            //query.Close();
            //query.SQL.Clear();
            //query.SQL.Text = "SELECT * FROM HYK_GKDA_JTXX WHERE GKID=" + obj.iGKID;
            //query.Open();
            //while (!query.Eof)
            //{
            //    HYKGL.HYKGL_GKDALR._JTXX tp_jtxx = new HYKGL.HYKGL_GKDALR._JTXX();
            //    tp_jtxx.JTXM = query.FieldByName("JTXM").AsString;
            //    tp_jtxx.JTGX = query.FieldByName("JTGX").AsString;
            //    tp_jtxx.JTXB = query.FieldByName("JTXB").AsString;
            //    tp_jtxx.JTNL = query.FieldByName("JTNL").AsInteger;
            //    tp_jtxx.JTSR = FormatUtils.DatetimeToString(query.FieldByName("JTSR").AsDateTime);
            //    tp_jtxxs.Add(tp_jtxx);
            //    query.Next();
            //}
            //query.Close();
            //obj.JTXX = tp_jtxxs;

            return JsonConvert.SerializeObject(obj);
        }

        public static string GetKHDAETHDList(out string msg, int iHYID)
        {
            msg = string.Empty;
            List<Object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_sql = "";
                    tp_sql += " select * from HYK_GKDA_ETHD where HYID=" + iHYID;
                    query.SQL.Text = tp_sql;
                    query.Open();
                    while (!query.Eof)
                    {
                        GKDAQZ item = new GKDAQZ();
                        item.iQZID = query.FieldByName("HDID").AsInteger;
                        item.iHYID = query.FieldByName("HDID").AsInteger;
                        item.sQZDM = query.FieldByName("HDDM").AsString;
                        item.sQZMC = query.FieldByName("HDMC").AsString;
                        item.sBZ = query.FieldByName("BZ").AsString;

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

            return JsonConvert.SerializeObject(lst);
        }

        public static string GetKHDAQZList(out string msg, int iHYID)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " select * from HYK_GKDA_QZ where GKID=" + iHYID;
                    query.Open();
                    while (!query.Eof)
                    {
                        GKDAQZ item = new GKDAQZ();
                        item.iQZID = query.FieldByName("QZID").AsInteger;
                        item.iHYID = query.FieldByName("GKID").AsInteger;
                        item.sQZDM = query.FieldByName("QZDM").AsString;
                        item.sQZMC = query.FieldByName("QZMC").AsString;
                        item.sBZ = query.FieldByName("BZ").AsString;
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

            return JsonConvert.SerializeObject(lst);
        }

        public static string GetKHDATJList(out string msg, int iHYID)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select H.HYK_NO, G.*,A.HYKNAME,(select NR from HYXXXMDEF where XMID=G.ZJLXID) ZJLXMC ";
                    query.SQL.Add("     from HYK_GKDA G,HYK_HYXX H,HYKDEF A where H.HYKTYPE=A.HYKTYPE(+) AND G.GKID=H.GKID (+)   ");
                    query.SQL.Add("  and G.TJRYID=" + iHYID);
                    query.Open();
                    while (!query.Eof)
                    {

                        HYXX obj = new HYXX();
                        obj.iHYID = query.FieldByName("GKID").AsInteger;
                        obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        obj.sHY_NAME = query.FieldByName("GK_NAME").AsString;
                        obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        lst.Add(obj);
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
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }

            return JsonConvert.SerializeObject(lst);
        }

        public static string GetSHSBList(out string msg, SHSPSB obj)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select SHSBID,SHDM,SBDM,SBMC from SHSPSB";
                    query.SQL.Add("  where SHDM='" + obj.sSHDM + "'");
                    query.Open();
                    while (!query.Eof)
                    {
                        SHSPSB item = new SHSPSB();
                        item.iSHSBID = query.FieldByName("SHSBID").AsInteger;
                        item.sSHDM = query.FieldByName("SHDM").AsString;
                        item.sSBDM = query.FieldByName("SBDM").AsString;
                        item.sSBMC = query.FieldByName("SBMC").AsString;
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

        public static string GetHYKSJXXList(out string msg, HYXX_Detail obj)
        {
            //获取会员卡，必须传入iHYKTYPE，可以传入sHYKNO_Begin、sHYKNO_End、iSL、iSTATUS(默认>=0)
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(obj.sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "   select L.*,H.HYK_NO,H.HY_NAME,K.HYKNAME ,N.HYKNAME HYKNAME_NEW";
                    query.SQL.Add("    from HYDJSQJL L ,HYK_HYXX H , HYKDEF K,HYKDEF N");
                    query.SQL.Add("    where L.HYID=H.HYID and L.HYKTYPE=K.HYKTYPE and L.HYKTYPE_NEW=N.HYKTYPE");
                    query.SQL.Add("  and L.BJ_SJ=" + obj.iBJ_SJ + "");
                    if (obj.sHYKNO_Begin != "" && obj.sHYKNO_Begin != null)
                        query.SQL.Add(" and H.HYK_NO>='" + obj.sHYKNO_Begin + "'");
                    if (obj.sHYKNO_End != "" && obj.sHYKNO_End != null)
                        query.SQL.Add(" and H.HYK_NO<='" + obj.sHYKNO_End + "'");
                    if (obj.iHYKTYPE != 0)
                        query.SQL.Add(" and L.HYKTYPE=" + obj.iHYKTYPE + "");
                    if (obj.iHYKTYPE_NEW != 0)
                        query.SQL.Add(" and L.HYKTYPE_NEW=" + obj.iHYKTYPE_NEW + " ");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.iHYKTYPE_NEW = query.FieldByName("HYKTYPE_NEW").AsInteger;
                        item.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                        item.fBQJF = query.FieldByName("BQJF").AsFloat;
                        item.dKSRQ = FormatUtils.DateToString(query.FieldByName("SJRQ").AsDateTime);
                        item.iGZID = query.FieldByName("GZID").AsInteger;
                        item.iINX = query.FieldByName("INX").AsInteger;
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
        public static string GetGKDAList(out string msg, HYXX_Detail obj)
        {
            //获取会员卡，必须传入iHYKTYPE，可以传入sHYKNO_Begin、sHYKNO_End、iSL、iSTATUS(默认>=0)
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_sql = "";
                    tp_sql += " select ";
                    tp_sql += " H.* ";
                    tp_sql += " ,D.HYKNAME ";
                    tp_sql += " ,nvl(F.LJXFJE,0) LJXFJE ";
                    tp_sql += " ,nvl(F.WCLJF,0) WCLJF ";
                    tp_sql += " ,E.QCYE,E.YE ";
                    tp_sql += " ,G.SFZBH ";
                    tp_sql += " ,A.MDMC  ,B.MDMC AS FXDWMC,C.MDMC AS SXMDMC ";
                    tp_sql += " from HYK_HYXX H";
                    tp_sql += " ,HYKDEF D,HYK_JFZH F,HYK_JEZH E,HYK_GKDA G";
                    tp_sql += " ,MDDY A,MDDY B,MDDY C ";
                    tp_sql += " where H.HYKTYPE=D.HYKTYPE(+)";
                    tp_sql += " and H.HYID=F.HYID(+)";
                    tp_sql += " and H.HYID=E.HYID(+)";
                    tp_sql += " and H.GKID=G.GKID(+)";
                    tp_sql += " and H.MDID=A.MDID(+)  and H.FXDW=B.MDID(+)  and H.MDID=C.MDID(+)";
                    query.SQL.Text = tp_sql;
                    if (obj.iGKID != 0)
                    {
                        query.SQL.Add("   and H.GKID=" + obj.iGKID);
                    }
                    if (obj.iHYKTYPE != 0)
                    {
                        query.SQL.Add("   and H.HYKTYPE=" + obj.iHYKTYPE);
                    }
                    if (obj.iSTATUS != 0)
                    {
                        query.SQL.Add("   and H.STATUS=" + obj.iSTATUS);
                    }
                    else
                    {
                        query.SQL.Add("   and H.STATUS<>-1 ");
                        query.SQL.Add("   and H.STATUS<>-2 ");
                        query.SQL.Add("   and H.STATUS<>-3 ");
                    }

                    if (obj.sHYKNO_Begin != "" && obj.sHYKNO_Begin != null)
                        query.SQL.Add(" and H.HYK_NO>='" + obj.sHYKNO_Begin + "'");
                    if (obj.sHYKNO_End != "" && obj.sHYKNO_End != null)
                        query.SQL.Add(" and H.HYK_NO<='" + obj.sHYKNO_End + "'");
                    if (obj.iSL != 0)
                        query.SQL.Add("   and  ROWNUM <=  " + obj.iSL);
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                        item.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                        item.fQCYE = query.FieldByName("QCYE").AsFloat;
                        item.fCZJE = query.FieldByName("YE").AsFloat;
                        item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                        item.sSFZBH = query.FieldByName("SFZBH").AsString;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.fYE = query.FieldByName("YE").AsFloat;
                        item.sMZMC = query.FieldByName("MDMC").AsString;
                        //item.iFXDW = query.FieldByName("FXDW").AsInteger;
                        item.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                        //item.iSXMDID = query.FieldByName("SXMDID").AsInteger;
                        item.sSXMDMC = query.FieldByName("SXMDMC").AsString;
                        item.dJKRQ = query.FieldByName("JKRQ").AsDateTime.ToString();
                        item.iBJ_PSW = query.FieldByName("BJ_PSW").AsInteger;
                        item.iCDJZ = query.FieldByName("CDJZ").AsInteger;
                        item.iBJ_PARENT = query.FieldByName("BJ_PARENT").AsInteger;

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
        public static string GetHYXXXM(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<HYXXXMDY> lst = new List<HYXXXMDY>();
            query.SQL.Text = "select * from HYXXXMDEF where 1=1 ";
            if (param.iXMLX != -1)
                query.SQL.Add(" and XMLX=" + param.iXMLX);
            query.Open();
            while (!query.Eof)
            {
                HYXXXMDY item = new HYXXXMDY();
                item.iXMID = query.FieldByName("XMID").AsInteger;
                item.iXMLX = query.FieldByName("XMLX").AsInteger;
                item.sNR = query.FieldByName("NR").AsString;
                lst.Add(item);
                query.Next();
            }

            return JsonConvert.SerializeObject(lst);
        }

        //public static string GetKLXBQ(out string msg, CyQuery query, CRMLIBASHX param)// int iHYKTYPE)
        //{
        //    msg = string.Empty;
        //    List<HYXXXMDY> lst = new List<HYXXXMDY>();
        //    query.SQL.Text = " select * from  LABEL_LB L,LABEL_XMITEM_KLX K where L.LABELLBID=K.LABELBID";
        //    if (param.iHYKTYPE != -1)
        //        query.SQL.Add(" and K.HYKTYPE=" + param.iHYKTYPE);
        //    query.Open();
        //    while (!query.Eof)
        //    {
        //        HYXXXMDY item = new HYXXXMDY();
        //        item.iXMID = query.FieldByName("LABELLBID").AsInteger;
        //        item.iXMLX = 0;
        //        item.sNR = query.FieldByName("BQMC").AsString;
        //        lst.Add(item);
        //        query.Next();
        //    }

        //    return JsonConvert.SerializeObject(lst);
        //}

        public static string getLMSHXM(out string msg, ref HYXXXMDY obj)
        {
            msg = string.Empty;
            List<HYXXXMDY> lst = new List<HYXXXMDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * FROM  LM_JCXXDY where 1=1 ";
                    if (obj.iXMLX != -1)
                        query.SQL.Add(" and XMLX=" + obj.iXMLX);
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXXXMDY item = new HYXXXMDY();
                        item.iXMID = query.FieldByName("JLBH").AsInteger;
                        item.iXMLX = query.FieldByName("XMLX").AsInteger;
                        item.sNR = query.FieldByName("NAME").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string GetKCKXX(out string msg, CyQuery query, CRMLIBASHX item)//string pCZKHM, string pCDNR, string sDBConnName = "CRMDB"
        {
            msg = string.Empty;
            KCKXX obj = new KCKXX();
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            query.Close();
            if (item.sCZKHM == "" && item.sCDNR == "")
            {
                msg = "没有传入查询条件";
                return "";
            }

            //if (item.sCZKHM == "" && item.sCDNR == "" && item.sBGDDDM != "" && item.iHYKTYPE != 0)
            //{
            //    query.SQL.Text = "select min(CZKHM) CZKHM,count(*) from HYKCARD where BGDDDM=:BGDDDM and HYKTYPE=:HYKTYPE and STATUS=:STATUS";
            //    query.ParamByName("BGDDDM").AsString = item.sBGDDDM;
            //    query.ParamByName("HYKTYPE").AsInteger = item.iHYKTYPE;
            //    query.ParamByName("STATUS").AsInteger = item.iSTATUS;
            //    query.Open();
            //    if (query.Fields[1].AsInteger > 0)
            //    {
            //        item.sCZKHM = query.FieldByName("CZKHM").AsString;
            //    }
            //    else
            //    {
            //        return JsonConvert.SerializeObject(obj);
            //    }
            //}

            if (item.sCDNR != "")
            {
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                    item.sCDNR = EncCDNR(item.sCDNR);
            }

            query.SQL.Text = "select H.*,D.HYKNAME,D.KFJE,D.FS_YXQ,D.YXQCD,B.BGDDMC,B.MDID AS MDIDM,F.FXDWMC";
            query.SQL.Add("  from HYKCARD H,HYKDEF D,HYK_BGDD B,FXDWDEF F where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM and H.FXDWID = F.FXDWID");
            if (item.sCZKHM != "")
                query.SQL.Add(" and CZKHM='" + item.sCZKHM + "'");
            if (item.sCDNR != "")
                query.SQL.Add(" and CDNR='" + item.sCDNR + "'");
            query.Open();
            if (!query.IsEmpty)
            {
                obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                obj.sCDNR = query.FieldByName("CDNR").AsString;
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                    obj.sXKCDNR = CrmLibProc.DecCDNR(obj.sCDNR);
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                obj.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                obj.fPDJE = query.FieldByName("PDJE").AsFloat;
                obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                obj.iFXDWID = query.FieldByName("FXDWID").AsInteger;
                obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                obj.iMDID = query.FieldByName("MDIDM").AsInteger;
                obj.iSKJLBH = query.FieldByName("SKJLBH").AsInteger;
                obj.fKFJE = query.FieldByName("KFJE").AsFloat;
                if (query.FieldByName("FS_YXQ").AsInteger == 0)
                    obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                else
                    obj.dYXQ = FormatUtils.DateToString(GetYXQ(serverTime.Date, query.FieldByName("YXQCD").AsString));
                obj.dXKRQ = FormatUtils.DateToString(query.FieldByName("XKRQ").AsDateTime);
            }
            //if (obj.sCZKHM != "")
            //{
            //    query.SQL.Clear();
            //    query.Params.Clear();
            //    query.SQL.Text = "     select * from FXDWDEF where BJ_FKFS=0 and BJ_MRFS=1";
            //    query.Open();
            //    if (!query.IsEmpty)
            //    {
            //        obj.iFXDWID = query.FieldByName("FXDWID").AsInteger;
            //        obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            //    }
            //}
            //else
            //{
            //    msg = CrmLibStrings.msgHYXXNotFound;
            //    return null;
            //}
            // query.Close();

            return JsonConvert.SerializeObject(obj);
        }

        public static string GetMZKKCKXX(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            query.SQL.Text = "select * from MZKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM and not exists(select 1 from MZKXX M where H.CZKHM = M.HYK_NO)";
            if (obj.sCZKHM != "" && obj.sCZKHM != "0")
                query.SQL.Add(" and CZKHM='" + obj.sCZKHM + "'");
            if (obj.sCDNR != "" && obj.sCDNR != "0")
                query.SQL.Add(" and CDNR='" + obj.sCDNR + "'");
            query.Open();
            if (!query.IsEmpty)
            {
                obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                obj.sCDNR = query.FieldByName("CDNR").AsString;
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                    obj.sXKCDNR = CrmLibProc.DecCDNR(obj.sCDNR);

                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                obj.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                obj.fPDJE = query.FieldByName("PDJE").AsFloat;
                obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                obj.iFXDWID = query.FieldByName("FXDWID").AsInteger;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.fKFJE = query.FieldByName("KFJE").AsFloat;
                obj.iSKJLBH = query.FieldByName("SKJLBH").AsInteger;

                if (query.FieldByName("FS_YXQ").AsInteger == 0)
                {
                    obj.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                }
                else
                {

                    // string addDays=query.FieldByName("YXQCD").AsString.Substring(0,query.FieldByName("YXQCD").AsString.Length-1);

                    string addDays = query.FieldByName("YXQCD").AsString.Trim().Substring(0, query.FieldByName("YXQCD").AsString.Trim().Length - 1);
                    int days = Convert.ToInt32(addDays);
                    serverTime = serverTime.AddYears(days);
                    obj.dYXQ = serverTime.ToString("yyyy-MM-dd");

                }
                obj.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
            }
            else
            {
                obj = new CRMLIBASHX();
                //// msg = CrmLibStrings.msgHYXXNotFound;
                //return null;
            }
            query.Close();

            return JsonConvert.SerializeObject(obj);
        }
        public static string FillHYKTYPEList(out string msg, int iMode, int pRYID, bool bQX)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<HYKDEF> lst = new List<HYKDEF>();
            //string sNodes = "[";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                switch (iMode)
                {
                    case 1:
                        sql = " and BJ_CZK=0";
                        break;
                    case 2:
                        sql = " and BJ_CZK=1";
                        break;
                    case 3:
                        sql = " and BJ_CZZH=1";
                        break;
                }
                query.SQL.Text = "select HYKTYPE,HYKNAME,HYKKZID from HYKDEF M where 1=1 " + sql;
                if (bQX && pRYID > 0)
                {
                    query.SQL.Add(" and (exists(select 1 from XTCZY_HYLXQX X where X.PERSON_ID=" + pRYID + " and X.HYKTYPE=M.HYKTYPE)");
                    query.SQL.Add(" or exists(select 1 from CZYGROUP_HYLXQX X,XTCZYGRP G where G.PERSON_ID=" + pRYID + " and X.HYKTYPE=M.HYKTYPE and X.ID=G.GROUPID))");
                }
                query.SQL.Add(" order by HYKTYPE");
                query.Open();
                while (!query.Eof)
                {
                    HYKDEF item = new HYKDEF();
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    lst.Add(item);
                    query.Next();
                    //sNodes = sNodes + "{id:" + item.sHYKDM + ",pID:" + item.sHYKPDM + ",name:" + item.sHYKNAME + "}";
                    //if (!query.Eof)
                    //    sNodes = sNodes + ",";
                }
                //sNodes = sNodes + "]";
                query.Close();
            }
            catch (Exception e)
            {
                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string GetHYXXList(out string msg, HYXX_Detail obj, int pbj, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            //KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    //query.SQL.Text = "select H.*,G.SJHM,G.SFZBH,G.SEX,D.HYKNAME,R.PERSON_NAME from HYK_HYXX H, HYK_GRXX G,HYKDEF D, RYXX R where H.HYID = G.HYID and H.HYKTYPE=D.HYKTYPE and R.PERSON_ID(+)=H.KFRYID";
                    //query.SQL.Add(" and H.STATUS=" + obj.iSTATUS);
                    query.SQL.Text = "select A.* from (select H.HYID,H.HYK_NO,H.HY_NAME,H.HYKTYPE,H.KFRYID,G.SJHM,G.SFZBH,G.SEX,D.HYKNAME,R.PERSON_NAME ";
                    query.SQL.Add(" from HYK_HYXX H, HYK_GRXX G,HYKDEF D, RYXX R ");
                    query.SQL.Add(" where H.HYID = G.HYID and H.HYKTYPE=D.HYKTYPE and R.PERSON_ID(+)=H.KFRYID");
                    if (obj.sHYKNO_BEGIN != "")
                        query.SQL.Add(" and H.HYK_NO >='" + obj.sHYKNO_BEGIN + "' ");
                    if (obj.sHYKNO_END != "")
                        query.SQL.Add(" and H.HYK_NO <='" + obj.sHYKNO_END + "' ");
                    if (obj.sSJHM != "")
                        query.SQL.Add(" and G.SJHM ='" + obj.sSJHM + "' ");
                    if (obj.sSFZBH != "")
                        query.SQL.Add(" and G.SFZBH ='" + obj.sSFZBH + "' ");
                    if (pbj == 1)
                        query.SQL.Add(" and H.KFRYID is not null");
                    if (pbj == 0)
                        query.SQL.Add("and H.KFRYID is null");
                    query.SQL.Add(" order by H.HYK_NO asc) A");
                    if (obj.iSL != 0)
                        query.SQL.Add(" where ROWNUM< =" + obj.iSL);
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.sSJHM = query.FieldByName("SJHM").AsString;
                        item.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                        item.sPERSON_NAME = query.FieldByName("PERSON_NAME").AsString;
                        item.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
                        item.sSFZBH = query.FieldByName("SFZBH").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }

        public static string GetHYXQList(out string msg, HYXX_Detail obj, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select G.*,Q.XQMC from HYK_GKDA G,XQDY Q where G.XQID=Q.XQID(+)";
                    if (obj.sSJHM is string)
                        query.SQL.Add(" and G.SJHM ='" + obj.sSJHM + "' ");
                    if (obj.sSFZBH is string)
                        query.SQL.Add(" and G.SFZBH ='" + obj.sSFZBH + "' ");
                    if (obj.iXQID != 0)
                        query.SQL.Add(" and G.XQID=" + obj.iXQID);
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iGKID = query.FieldByName("GKID").AsInteger;
                        item.sGK_NAME = query.FieldByName("GK_NAME").AsString;
                        item.iXQID = query.FieldByName("XQID").AsInteger;
                        item.sXQMC = query.FieldByName("XQMC").AsString;
                        item.sSJHM = query.FieldByName("SJHM").AsString;
                        item.sSFZBH = query.FieldByName("SFZBH").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }

        public static string GetLQHYXXList(out string msg, HYXX_Detail obj, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            //KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select H.*,G.SJHM,G.SFZBH,G.SEX,D.HYKNAME,R.PERSON_NAME from HYK_HYXX H, HYK_GKDA G,HYKDEF D, RYXX R where H.GKID = G.GKID and H.HYKTYPE=D.HYKTYPE and R.PERSON_ID(+)=H.KFRYID";
                    //query.SQL.Add(" and H.STATUS=" + obj.iSTATUS);
                    if (obj.sHYKNO_BEGIN != "")
                        query.SQL.Add(" and H.HYK_NO >='" + obj.sHYKNO_BEGIN + "' ");
                    if (obj.sHYKNO_END != "")
                        query.SQL.Add(" and H.HYK_NO <='" + obj.sHYKNO_END + "' ");
                    if (obj.sSJHM != "")
                        query.SQL.Add(" and G.SJHM ='" + obj.sSJHM + "' ");
                    if (obj.sSFZBH != "")
                        query.SQL.Add(" and G.SFZBH ='" + obj.sSFZBH + "' ");
                    if (obj.iSL != 0)
                        query.SQL.Add(" and ROWNUM< =" + obj.iSL);
                    query.SQL.Add("order by H.HYK_NO asc ");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        item.sSJHM = query.FieldByName("SJHM").AsString;
                        item.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                        item.sPERSON_NAME = query.FieldByName("PERSON_NAME").AsString;
                        item.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
                        item.sSFZBH = query.FieldByName("SFZBH").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }

        public static string GetSexStr_01(int pSex)
        {
            if (pSex == 0)
                return "男";
            if (pSex == 1)
                return "女";
            return "";
        }

        public static string GetSexStr_FM(string pSex)
        {
            if (pSex.ToUpper() == "M")
                return "男";
            if (pSex.ToUpper() == "F")
                return "女";
            return "";
        }

        public static string GetYJLX(int pYJLX)
        {
            if (pYJLX == 1)
                return "消费预警";
            if (pYJLX == 2)
                return "收款台预警";
            if (pYJLX == 3)
                return "同部门预警";
            if (pYJLX == 4)
                return "返利预警";
            return "";
        }

        public static string GetZBLX(int iZBLX)
        {
            if (iZBLX == 4)
                return "积分";
            if (iZBLX == 1)
                return "消费次数";
            return "";
        }

        public static string GetSQList(out string msg, HYXX_Detail obj, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select S.*,M.MDMC from SQDY S, MDDY M where S.MDID = M.MDID";
                    if (obj != null)
                    {
                        if (obj.sSQMC != "")
                        {
                            query.SQL.Add("  and S.SQMC like '%" + obj.sSQMC + "%'");
                        }
                        if (obj.sMDMC != "")
                        {
                            query.SQL.Add("  and M.MDMC like '%" + obj.sMDMC + "%'");
                        }
                        if (obj.iSQID != 0)
                        {
                            query.SQL.Add(" and S.SQID=" + obj.iSQID + " ");
                        }
                    }
                    query.SQL.Add("   order by S.SQID asc ");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iSQID = query.FieldByName("SQID").AsInteger;
                        item.sSQMC = query.FieldByName("SQMC").AsString;
                        item.iMDID = query.FieldByName("MDID").AsInteger;
                        item.sMDMC = query.FieldByName("MDMC").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static string GetFWHYKList(out string msg, HYXX_Detail obj, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select C.HYK_NO,C.HYID ";
                    query.SQL.Add("from HYK_HYXX C,HYKDEF D");
                    query.SQL.Add("WHERE C.HYKTYPE=D.HYKTYPE and (C.STATUS>=0 ) ");
                    query.SQL.Add(" and C.HYKTYPE=" + obj.iHYKTYPE);
                    if (obj.sHYKNO_BEGIN != "")
                        query.SQL.Add(" and C.HYK_NO >='" + obj.sHYKNO_BEGIN + "' ");
                    if (obj.sHYKNO_END != "")
                        query.SQL.Add(" and C.HYK_NO <='" + obj.sHYKNO_END + "' ");
                    if (obj.iSL != 0)
                        query.SQL.Add(" and ROWNUM< =" + obj.iSL);

                    query.SQL.Add("order by HYK_NO asc ");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iHYID = query.FieldByName("HYID").AsInteger;
                        item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        CyQuery query1 = new CyQuery(conn);
                        query1.SQL.Text = "select H.FWSL from HYK_FWZH H, HYK_HYXX X where X.HYID = H.HYID and X.HYK_NO ='" + item.sHYK_NO + "'";
                        query1.SQL.Add(" and H.FWNRID =" + obj.iFWNRID);
                        query1.SQL.Add(" and (sysdate-(H.JSRQ+1))<0");
                        query1.Open();
                        if (!query1.IsEmpty)
                            item.fSYFWSL = query1.FieldByName("FWSL").AsFloat;
                        else
                        {
                            query1.Close();
                            query1.SQL.Clear();
                            query1.SQL.Text = "select D.FWSL from HYK_XSFWDEF D, HYK_HYXX H where D.HYKTYPE =" + obj.iHYKTYPE;
                            query1.SQL.Add(" and D.FWNRID =" + obj.iFWNRID);
                            query1.Open();
                            if (!query1.IsEmpty)
                                item.fSYFWSL = query1.FieldByName("FWSL").AsFloat;
                        }
                        query1.Close();
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static string GetFW(out string msg, HYXX_Detail obj, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * from FWNRDEF where BJ_TY = 0";
                    query.SQL.Add("order by FWNRID asc ");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXX_Detail item = new HYXX_Detail();
                        item.iFWNRID = query.FieldByName("FWNRID").AsInteger;
                        item.sFWNRMC = query.FieldByName("FWNRMC").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static string GetKCKList(out string msg, KCKXX obj, string sDBConnName = "CRMDB")
        {
            //获取库存卡，必须传入obj.sBGDDDM、iHYKTYPE、iSTATUS，可以传入sCZKHM_Begin、sCZKHM_End、iSL
            msg = string.Empty;
            List<object> lst = new List<object>();
            //KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (obj.iHF == 0)
                    {
                        query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC from HYKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";


                    }
                    else
                    {
                        query.SQL.Text = " Select H.*,D.HYKNAME,B.BGDDMC from HYKCARD_BAK H,HYKDEF D, HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                    }
                    //query.SQL.Add(" and H.STATUS=" + obj.iSTATUS);
                    if (obj.sBGDDDM != "")
                    {
                        query.SQL.Add(" and H.BGDDDM='" + obj.sBGDDDM + "'");
                    }
                    query.SQL.Add(" and H.HYKTYPE=" + obj.iHYKTYPE);
                    if (obj.sCZKHM_BEGIN != "")
                        query.SQL.Add(" and H.CZKHM>='" + obj.sCZKHM_BEGIN + "' ");
                    if (obj.sCZKHM_END != "")
                        query.SQL.Add(" and H.CZKHM<='" + obj.sCZKHM_END + "' ");

                    if (obj.iSL != 0)
                        query.SQL.Add(" and ROWNUM<=" + obj.iSL);
                    query.SQL.Add("order by CZKHM ");
                    query.Open();
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        //item.iSL = query.FieldByName("SL").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        item.fQCYE = query.FieldByName("QCYE").AsFloat;
                        item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                        item.fPDJE = query.FieldByName("PDJE").AsFloat;
                        item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                        item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                        item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }

        public static string GetMZKKCKList(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {
            //获取库存卡，必须传入obj.sBGDDDM、iHYKTYPE、iSTATUS，可以传入sCZKHM_Begin、sCZKHM_End、iSL
            msg = string.Empty;
            List<object> lst = new List<object>();
            //KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (obj.iHF == 0)
                    {
                        query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC from MZKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";


                    }
                    else
                    {
                        query.SQL.Text = " Select H.*,D.HYKNAME,B.BGDDMC from MZKCARD_BAK H,HYKDEF D, HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                    }
                    //query.SQL.Add(" and H.STATUS=" + obj.iSTATUS);
                    if (obj.sBGDDDM != "" && obj.sBGDDDM != "undefined")
                    {
                        query.SQL.Add(" and H.BGDDDM='" + obj.sBGDDDM + "'");
                    }
                    query.SQL.Add(" and H.HYKTYPE=" + obj.iHYKTYPE);
                    if (obj.sCZKHM_BEGIN != "")
                        query.SQL.Add(" and H.CZKHM>='" + obj.sCZKHM_BEGIN + "' ");
                    if (obj.sCZKHM_END != "")
                        query.SQL.Add(" and H.CZKHM<='" + obj.sCZKHM_END + "' ");

                    if (obj.iSL != 0)
                        query.SQL.Add(" and ROWNUM<=" + obj.iSL);
                    query.SQL.Add("order by CZKHM ");
                    query.Open();
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        //item.iSL = query.FieldByName("SL").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        item.fQCYE = query.FieldByName("QCYE").AsFloat;
                        item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                        item.fPDJE = query.FieldByName("PDJE").AsFloat;
                        item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                        item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                        item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }

        public static string GetKCKKD(out string msg, CyQuery query, CRMLIBASHX param)// string sBGDDDM, string sCZKHM_BEGIN, string sCZKHM_END, int iHYKTYPE, int iSTATUS, string sDBConnName = "CRMDB")
        {
            //获取库存卡卡段，必须传入obj.sBGDDDM、iHYKTYPE、iSTATUS、sCZKHM_Begin、sCZKHM_End
            msg = string.Empty;
            if (param.sCZKHM_END == "" && param.iSL == 0)
            {
                msg = "必须输入结束卡号或者数量";
                return msg;
            }
            List<object> lst = new List<object>();
            List<object> kdlst = new List<object>();
            KCKXX obj = new KCKXX();
            query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC,D.KHQDM,D.KHHZM from HYKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
            query.SQL.Add(" and H.BGDDDM='" + param.sBGDDDM + "'");
            query.SQL.Add(" and H.HYKTYPE=" + param.iHYKTYPE);
            query.SQL.Add(" and H.CZKHM>='" + param.sCZKHM_BEGIN + "' ");
            if (param.sCZKHM_END != "")
                query.SQL.Add(" and H.CZKHM<='" + param.sCZKHM_END + "' ");
            query.SQL.Add(" and H.STATUS=" + param.iSTATUS);
            if (param.iSKJLBH > 0)
                query.SQL.Add("and H.SKJLBH=" + param.iSKJLBH);
            if (param.iSL > 0)
                query.SQL.Add("and rownum<=" + param.iSL);
            query.SQL.Add("order by CZKHM ");
            query.Open();
            if (query.IsEmpty)
            {
                msg = CrmLibStrings.msgKCKNotFound;
                return msg;
            }
            while (!query.Eof)
            {
                KCKXX item = new KCKXX();
                item.sCZKHM = query.FieldByName("CZKHM").AsString;
                //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                //item.iSL = query.FieldByName("SL").AsInteger;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                item.fQCYE = query.FieldByName("QCYE").AsFloat;
                item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                item.fPDJE = query.FieldByName("PDJE").AsFloat;
                item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                item.sKHQDM = query.FieldByName("KHQDM").AsString;
                item.sKHHZM = query.FieldByName("KHHZM").AsString;
                item.iSKJLBH = query.FieldByName("SKJLBH").AsInteger;
                if (param.iSKJLBH == -1)//发卡，需要SKJLBH字段为空
                {
                    if (item.iSKJLBH > 0)
                    {
                        msg = "库存卡" + item.sCZKHM + "存在于售卡单" + item.iSKJLBH + "号中";
                        return msg;
                    }
                }
                lst.Add(item);
                query.Next();
            }
            query.Close();
            //if (lst.Count > 0)
            //    kdlst.Add(new KCKHD());
            foreach (KCKXX item in lst)
            {
                //                        public int iHYKTYPE = 0;
                //public string sHYKNAME = string.Empty;
                //public double fQCYE = 0;
                //public int iSL = 0;

                KCKHD item2 = new KCKHD();
                if (kdlst.Count == 0)
                {
                    item2.sCZKHM_BEGIN = item.sCZKHM;
                    item2.sCZKHM_END = item.sCZKHM;
                    item2.iHYKTYPE = item.iHYKTYPE;
                    item2.sHYKNAME = item.sHYKNAME;
                    item2.fMZJE = item.fQCYE;
                    item2.iSKSL++;
                    kdlst.Add(item2);
                }
                else if (((KCKHD)kdlst[kdlst.Count - 1]).iHYKTYPE == item.iHYKTYPE
                    && ((KCKHD)kdlst[kdlst.Count - 1]).fMZJE == item.fQCYE
                    && Convert.ToInt64(((KCKHD)kdlst[kdlst.Count - 1]).sCZKHM_END.Substring(0, ((KCKHD)kdlst[kdlst.Count - 1]).sCZKHM_END.Length - item.sKHHZM.Length)) + 1 == Convert.ToInt64(item.sCZKHM.Substring(0, item.sCZKHM.Length - item.sKHHZM.Length)))
                {
                    ((KCKHD)kdlst[kdlst.Count - 1]).sCZKHM_END = item.sCZKHM;
                    ((KCKHD)kdlst[kdlst.Count - 1]).iSKSL++;
                }
                else
                {
                    item2.sCZKHM_BEGIN = item.sCZKHM;
                    item2.sCZKHM_END = item.sCZKHM;
                    item2.iHYKTYPE = item.iHYKTYPE;
                    item2.sHYKNAME = item.sHYKNAME;
                    item2.fMZJE = item.fQCYE;
                    item2.iSKSL++;
                    kdlst.Add(item2);
                }
            }
            query.Close();

            return JsonConvert.SerializeObject(kdlst);//obj.GetTableJson(kdlst);
        }
        public static string GetSydhdy1(out string msg, int iID)
        {
            msg = string.Empty;
            CrmProc.GTPT.GTPT_WXSYDHDY obj = new CrmProc.GTPT.GTPT_WXSYDHDY();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select ID from WX_NAVIGATIONDEF where ID=:ID";
                    query.ParamByName("ID").AsInteger = iID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iID = query.FieldByName("ID").AsInteger;
                    }
                    query.Close();
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
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetKCKKD2(out string msg, string sBGDDDM, string sCZKHM_BEGIN, string sCZKHM_END, int iHYKTYPE, int iSTATUS, string sDBConnName = "CRMDB")
        {
            //GetKCKKD是根据卡号段分段，GetKCKKD2不分段，只计算数量
            //获取库存卡卡段，必须传入obj.sBGDDDM、iHYKTYPE、iSTATUS、sCZKHM_Begin、sCZKHM_End
            msg = string.Empty;
            List<KCKXX> lst = new List<KCKXX>();
            List<object> kdlst = new List<object>();
            KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select H.*,D.HYKNAME,D.BJ_PSW,B.BGDDMC,D.KHQDM,D.KHHZM from HYKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                    query.SQL.Add(" and H.BGDDDM='" + sBGDDDM + "'");
                    query.SQL.Add(" and H.HYKTYPE=" + iHYKTYPE);
                    query.SQL.Add(" and H.CZKHM>='" + sCZKHM_BEGIN + "' ");
                    query.SQL.Add(" and H.CZKHM<='" + sCZKHM_END + "' ");
                    query.SQL.Add(" and H.STATUS=" + iSTATUS);
                    query.SQL.Add("order by CZKHM ");
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        //item.iSL = query.FieldByName("SL").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        item.fQCYE = query.FieldByName("QCYE").AsFloat;
                        item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                        item.fPDJE = query.FieldByName("PDJE").AsFloat;
                        item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                        item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                        item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                        item.sKHQDM = query.FieldByName("KHQDM").AsString;
                        item.sKHHZM = query.FieldByName("KHHZM").AsString;
                        item.iBJ_PSW = query.FieldByName("BJ_PSW").AsInteger;
                        lst.Add(item);
                        query.Next();
                    }
                    query.Close();
                    KCKHD item2 = new KCKHD();
                    item2.sCZKHM_BEGIN = obj.sCZKHM_BEGIN;
                    item2.sCZKHM_END = obj.sCZKHM_END;
                    item2.iHYKTYPE = obj.iHYKTYPE;
                    item2.sHYKNAME = lst[0].sHYKNAME;
                    item2.fMZJE = lst[0].fQCYE;
                    item2.iSKSL = lst.Count;
                    item2.iBJ_PSW = lst[0].iBJ_PSW;
                    kdlst.Add(item2);
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
            return obj.GetTableJson(kdlst);
        }

        public static void UpdateKCKZKXX(out string msg, KCKXX obj)
        {
            //更新库存卡制卡信息，必须传入sCZKHM或sCDNR，验卡传入iBJ_YK=1，写卡传入dXKRQ，不可以同时传入
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "update HYKCARD set";
                    if (obj.iBJ_YK != 0)
                        query.SQL.Add(" BJ_YK=" + obj.iBJ_YK);
                    if (obj.dXKRQ != DateTime.MinValue.ToString("yyyy-MM-dd") && obj.dXKRQ != "")
                    {
                        query.SQL.Add(" XKRQ=:XKRQ ");
                        query.ParamByName("XKRQ").AsDateTime = FormatUtils.ParseDatetimeString(obj.dXKRQ);
                    }
                    if (obj.sCZKHM != "")
                        query.SQL.Add(" where CZKHM='" + obj.sCZKHM + "'");
                    if (obj.sCDNR != "")
                        query.SQL.Add(" where CDNR='" + obj.sCDNR + "'");
                    if (obj.dXKRQ != "" && obj.dXKRQ != DateTime.MinValue.ToString("yyyy-MM-dd"))
                    {
                        query.ParamByName("XKRQ").AsDateTime = FormatUtils.ParseDatetimeString(obj.dXKRQ);
                    }
                    query.ExecSQL();
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
        }

        public static void GetYHQZH(out string msg, ref YHQZH obj)
        {
            //获取优惠券账户，必须传入HYID、YHQID、JSRQ，MDFWDM不传默认为空格，CXID不传默认为-1
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select Y.*,D.YHQMC from HYK_YHQZH Y,YHQDEF D where Y.YHQID=D.YHQID";
                    query.SQL.Add(" and Y.HYID=" + obj.iHYID);
                    query.SQL.Add(" and Y.YHQID=" + obj.iYHQID);
                    if (obj.iCXID >= 0)
                        query.SQL.Add(" and Y.CXID=" + obj.iCXID);
                    query.SQL.Add(" and Y.JSRQ=:JSRQ");
                    query.SQL.Add(" and Y.MDFWDM=:MDFWDM");
                    query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(obj.dJSRQ);
                    query.ParamByName("MDFWDM").AsString = obj.sMDFWDM;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.fJE = query.FieldByName("JE").AsFloat;
                        obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                    }
                    query.Close();
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
        }

        public static void GetLPFFGZ(out string msg, ref LPFFGZ obj)
        {
            //获取礼品发发规则，必须传入iJLBH
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select G.* from HYK_LPFFGZ G";
                    query.SQL.Add(" where G.JLBH=" + obj.iJLBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                        obj.sGZMC = query.FieldByName("GZMC").AsString;
                        obj.iGZLX = query.FieldByName("GZLX").AsInteger;
                        obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                        obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        obj.iBJ_DC = query.FieldByName("BJ_DC").AsInteger;
                        obj.iBJ_SR = query.FieldByName("BJ_SR").AsInteger;
                        obj.iBJ_LJ = query.FieldByName("BJ_LJ").AsInteger;
                        obj.iBJ_BK = query.FieldByName("BJ_BK").AsInteger;
                        obj.iBJ_SL = query.FieldByName("BJ_SL").AsInteger;
                        obj.iDJR = query.FieldByName("DJR").AsInteger;
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.iDJR = query.FieldByName("DJR").AsInteger;
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.iZXR = query.FieldByName("ZXR").AsInteger;
                        obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        obj.iZZR = query.FieldByName("ZZR").AsInteger;
                        obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
                        obj.dSHRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
                        obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                        obj.iCXID = query.FieldByName("CXID").AsInteger;
                        //obj.sGZLXMC = CrmLibProc.GetLPFFGZLXName(obj.iGZLX);
                        query.Next();
                    }
                    query.Close();
                    query.SQL.Text = "select * from HYK_LPFFGZ_LP where JLBH=" + obj.iJLBH;
                    query.Open();
                    while (!query.Eof)
                    {
                        LPFFGZ_LP item = new LPFFGZ_LP();
                        item.iLPID = query.FieldByName("LPID").AsInteger;
                        item.iLPGRP = query.FieldByName("LPGRP").AsInteger;
                        item.fLPJE = query.FieldByName("LPJE").AsFloat;
                        //SetLPXX(item,item.iLPID);
                        obj.lpitem.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static string FillLPFFGZ(out string msg, CyQuery query, CRMLIBASHX param)// int iGZLX, int iHYKTYPE = 0)
        {
            //获取礼品发发规则列表，必须传入iGZLX，可以传入iBJ_CheckDate，iHYKTYPE，iFXMD，iCZMD
            msg = string.Empty;
            List<LPFFGZ> lst = new List<LPFFGZ>();
            //int itp_MDID = 0;
            //if (sBGDDDM != "") 
            //    itp_MDID = CrmLibProc.BGDDToMDID(query,sBGDDDM);
            query.SQL.Text = "select G.* from HYK_LPFFGZ G";
            query.SQL.Add(" where G.GZLX=" + param.iGZLX);
            query.SQL.Add(" and G.JSRQ+1>sysdate and G.ZXR is not null and G.ZZR is null");
            query.SQL.Add(" and G.KSRQ<= sysdate");
            if (param.iHYKTYPE > 0)
            {
                query.SQL.Add(" and G.HYKTYPE=" + param.iHYKTYPE);
            }
            //query.SQL.Add(" and G.HYKTYPE=" + obj.iHYKTYPE);
            //if (obj.iFXMD >= 0)
            //{
            //    //query.SQL.Add(" and G.HYKTYPE=" + obj.iHYKTYPE);
            //}
            //if (sBGDDDM != "")
            //{
            //    query.SQL.Add("  and G.JLBH in (select JLBH from HYK_LPFFGZ_MD where MDID=" + itp_MDID + ")");
            //    //query.SQL.Add(" and G.HYKTYPE=" + obj.iHYKTYPE);
            //}
            query.SQL.Add(" order by G.JLBH desc");
            query.Open();
            while (!query.Eof)
            {
                LPFFGZ item = new LPFFGZ();
                lst.Add(item);
                item.iJLBH = query.FieldByName("JLBH").AsInteger;
                item.sGZMC = query.FieldByName("GZMC").AsString;
                item.iGZLX = query.FieldByName("GZLX").AsInteger;
                item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                item.iBJ_DC = query.FieldByName("BJ_DC").AsInteger;
                item.iBJ_SR = query.FieldByName("BJ_SR").AsInteger;
                item.iBJ_LJ = query.FieldByName("BJ_LJ").AsInteger;
                item.iBJ_BK = query.FieldByName("BJ_BK").AsInteger;
                item.iBJ_SL = query.FieldByName("BJ_SL").AsInteger;
                item.iDJR = query.FieldByName("DJR").AsInteger;
                item.sDJRMC = query.FieldByName("DJRMC").AsString;
                item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                item.iDJR = query.FieldByName("DJR").AsInteger;
                item.sDJRMC = query.FieldByName("DJRMC").AsString;
                item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                item.iZXR = query.FieldByName("ZXR").AsInteger;
                item.sZXRMC = query.FieldByName("ZXRMC").AsString;
                item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                item.iZZR = query.FieldByName("ZZR").AsInteger;
                item.sZZRMC = query.FieldByName("ZZRMC").AsString;
                item.dSHRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
                item.iYHQID = query.FieldByName("YHQID").AsInteger;
                item.iCXID = query.FieldByName("CXID").AsInteger;
                //item.sGZLXMC = CrmLibProc.GetLPFFGZLXName(item.iGZLX);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetLPFFGZLP(out string msg, LPFFGZ_TJ obj)
        {
            //获取礼品发发可用礼品，必须传入pGZID、pHYID、pCZDD
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    int tp_mdid = BGDDToMDID(query, obj.sCZDD);
                    double tp_ljxfje = 0;
                    LPFFGZ ffgz = new LPFFGZ();
                    ffgz.iJLBH = obj.iFLGZBH;
                    GetLPFFGZ(out msg, ref ffgz);
                    if (ffgz.iGZLX == 7 || ffgz.iGZLX == 8)
                    {
                        query.SQL.Text = "  select L.HYID,L.MDID ,SUM(Z.JE) JE from HYXFJL L,HYXFJL_ZFFS Z where L.XFJLID=Z.XFJLID  and Z.BJ_FQ=1";
                        query.SQL.Add("  and L.HYID=:HYID and L.MDID=:MDID and L.XFSJ>=:KSRQ and L.XFSJ<:JSRQ");
                        query.SQL.Add(" group by  L.HYID,L.MDID");
                        query.SQL.Add("  union");
                        query.SQL.Add("  select L.HYID,L.MDID ,SUM(Z.JE) JE from HYK_XFJL L,HYK_XFJL_ZFFS Z where L.XFJLID=Z.XFJLID and L.STATUS=1 and Z.BJ_FQ=1 ");
                        query.SQL.Add("  and L.HYID=:HYID and L.MDID=:MDID and L.XFSJ>=:KSRQ and L.XFSJ<:JSRQ");
                        query.SQL.Add(" group by  L.HYID,L.MDID");
                        query.ParamByName("HYID").AsInteger = obj.iHYID;
                        query.ParamByName("MDID").AsInteger = tp_mdid;
                        if (ffgz.iBJ_LJ == 1)
                        {
                            DateTime sysdate = DateTime.Today;
                            query.ParamByName("KSRQ").AsDateTime = sysdate;
                            query.ParamByName("JSRQ").AsDateTime = sysdate.AddDays(1);
                        }
                        else
                        {
                            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(ffgz.dKSRQ);
                            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(ffgz.dJSRQ).AddDays(1);
                        }
                        query.Open();
                        while (!query.Eof)
                        {
                            tp_ljxfje += query.FieldByName("JE").AsFloat;
                            query.Next();
                        }
                        query.SQL.Clear();
                        query.Params.Clear();
                        query.Close();
                        query.SQL.Text = "  select P.LPJE,P.SL from HYK_JFFLJL L, HYK_JFFLJLITEM I,HYK_JFFLJL_LP P";
                        query.SQL.Add("  where L.JLBH=I.JLBH and I.JLBH=P.JLBH and L.MDID=" + tp_mdid + "");
                        query.SQL.Add("  and I.HYID=" + obj.iHYID + " and I.FLGZBH=" + obj.iFLGZBH + " and L.SHR IS NOT  NULL");
                        query.Open();
                        while (!query.Eof)
                        {
                            if (ffgz.iGZLX != 8)
                            {
                                tp_ljxfje -= query.FieldByName("LPJE").AsFloat * query.FieldByName("SL").AsFloat;
                            }
                            else
                            {
                                tp_ljxfje -= query.FieldByName("LPJE").AsFloat;
                            }
                            query.Next();
                        }
                        query.SQL.Clear();
                        query.Params.Clear();
                        query.Close();

                    }
                    // tp_ljxfje = 100;
                    if (ffgz.iGZLX != 8)
                    {
                        query.SQL.Text = "select L.*,K.KCSL from HYK_LPFFGZ_LP L,HYK_JFFHLPKC K";
                        query.SQL.Add(" where K.KCSL>0 and L.JLBH=" + obj.iFLGZBH + " and L.LPID=K.LPID and K.BGDDDM='" + obj.sCZDD + "'");
                        if (ffgz.iGZLX == 7)
                        {
                            query.SQL.Add("  and L.LPJE<=" + tp_ljxfje + " ");
                        }
                        query.SQL.Add(" order by L.LPJF,L.LPJF desc");
                    }
                    else
                    {
                        query.SQL.Text = "select * from ( select L.*,10 KCSL from HYK_LPFFGZ_LP L where L.JLBH=" + obj.iFLGZBH + " and L.LPID=0 ";
                        query.SQL.Add(" and L.LPJE<=" + tp_ljxfje + "");
                        query.SQL.Add("  order by LPJE desc) where rownum<=1");
                    }

                    query.Open();
                    while (!query.Eof)
                    {
                        LPFFGZ_LP item = new LPFFGZ_LP();
                        lst.Add(item);
                        item.iLPID = query.FieldByName("LPID").AsInteger;
                        SetLPXX(item, item.iLPID);
                        item.iLPGRP = query.FieldByName("LPGRP").AsInteger;
                        item.fLPJF = query.FieldByName("LPJF").AsFloat;
                        item.fLPJE = query.FieldByName("LPJE").AsFloat;
                        item.fKCSL = query.FieldByName("KCSL").AsFloat;
                        if (ffgz.iGZLX == 8)
                        {
                            item.fSL = query.FieldByName("LPJF").AsFloat;
                        }

                        if (ffgz.iGZLX == 7 || ffgz.iGZLX == 8)
                        {
                            item.fZSJF = Math.Round(tp_ljxfje, 2);
                        }

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
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetHYXF(out string msg, string sSKTNO, int iMDID, int iXPH, int iHYID)
        {
            //获取会员消费，必须传入XFJLID或MDID+SKTNO+JLBH，如传入iHYID，则额外检查是否匹配，否则将查询到的HYID存入iHYID中
            msg = string.Empty;
            XFJL obj = new XFJL();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tmpSQL = string.Empty, tableName;
                    //if (iXFJLID > 0)
                    //    tmpSQL = " XFJLID=" + iXFJLID;
                    //else 
                    if (iMDID > 0 && sSKTNO != "" && iXPH > 0)
                        tmpSQL = " MDID=" + iMDID + " and SKTNO='" + sSKTNO + "' and JLBH=" + iXPH;
                    else
                    {
                        msg = CrmLibStrings.msgXFJLConditionError;
                        return msg;
                    }
                    query.SQL.Text = "select XFJLID,HYID,0 HYXFJL from HYK_XFJL where " + tmpSQL + " and STATUS=1";
                    query.SQL.Add(" union select XFJLID,HYID,1 HYXFJL from HYXFJL where " + tmpSQL);
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgXFJLNotFound;
                        return msg;
                    }
                    if (iHYID > 0 && iHYID != query.FieldByName("HYID").AsInteger)
                    {
                        msg = CrmLibStrings.msgXFJLWithWrongHYXX;
                        return msg;
                    }

                    obj.iXFJLID = query.FieldByName("XFJLID").AsInteger;
                    obj.bHYXFJL = query.FieldByName("HYXFJL").AsInteger;
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    if (obj.bHYXFJL == 1)
                        tableName = "HYXFJL";
                    else
                        tableName = "HYK_XFJL";
                    query.Close();
                    query.SQL.Text = "select X.*,M.MDMC from " + tableName + " X,MDDY M where X.MDID=M.MDID";
                    query.SQL.Add(" and X.XFJLID=" + obj.iXFJLID);
                    query.Open();
                    obj.iMDID = query.FieldByName("MDID").AsInteger;
                    obj.iXPH = query.FieldByName("JLBH").AsInteger;
                    obj.sSHDM = query.FieldByName("SHDM").AsString;
                    obj.sSKTNO = query.FieldByName("SKTNO").AsString;
                    obj.sMDMC = query.FieldByName("MDMC").AsString;
                    obj.sHYKNO = query.FieldByName("HYKNO").AsString;
                    obj.fJE = query.FieldByName("JE").AsFloat;
                    obj.fJF = query.FieldByName("JF").AsFloat;
                    obj.fZK = query.FieldByName("ZK").AsFloat;
                    obj.fZK_HY = query.FieldByName("ZK_HY").AsFloat;
                    obj.fXSSL = query.FieldByName("XSSL").AsFloat;
                    obj.dXFSJ = FormatUtils.DatetimeToString(query.FieldByName("XFSJ").AsDateTime);
                    obj.dJZRQ = FormatUtils.DateToString(query.FieldByName("JZRQ").AsDateTime);
                    obj.dCRMJZRQ = FormatUtils.DateToString(query.FieldByName("CRMJZRQ").AsDateTime);
                    query.Close();
                    //query.SQL.Text = "select S.*,P.SPMC,B.BMMC from " + tableName + "_SP S,SHSPXX P,SHBM B";
                    //query.SQL.Add(" where S.XFJLID=:XFJLID and S.SHSPID=P.SHSPID and B.SHDM=:SHDM and S.BMDM=B.BMDM");
                    query.SQL.Text = "  select S.SHSPID,S.BMDM,S.SPDM, P.SPMC,B.BMMC ,SUM(S.JF) JF from " + tableName + "_SP S,SHSPXX P,SHBM B";
                    query.SQL.Add(" where S.XFJLID=:XFJLID and S.SHSPID=P.SHSPID and B.SHDM=:SHDM and S.BMDM=B.BMDM");
                    query.SQL.Add("  GROUP BY  S.SHSPID,S.BMDM,S.SPDM, P.SPMC,B.BMMC");
                    query.ParamByName("XFJLID").AsInteger = obj.iXFJLID;
                    query.ParamByName("SHDM").AsString = obj.sSHDM;
                    query.Open();
                    while (!query.Eof)
                    {
                        XFJL_SP item = new XFJL_SP();
                        // item.iINX = query.FieldByName("INX").AsInteger;
                        item.iSHSPID = query.FieldByName("SHSPID").AsInteger;
                        item.sBMDM = query.FieldByName("BMDM").AsString;
                        item.sBMMC = query.FieldByName("BMMC").AsString;
                        item.sSPDM = query.FieldByName("SPDM").AsString;
                        item.sSPMC = query.FieldByName("SPMC").AsString;
                        // item.fXSJE = query.FieldByName("XSJE").AsFloat;
                        item.fJF = query.FieldByName("JF").AsFloat;
                        // item.fZKJE = query.FieldByName("ZKJE").AsFloat;
                        // item.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
                        // item.fXSSL = query.FieldByName("XSSL").AsFloat;
                        obj.itemTable.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetHYKDEF(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            HYKDEF obj = new HYKDEF();
            query.SQL.Text = "select D.* from HYKDEF D where D.HYKTYPE=" + param.iHYKTYPE;
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.iHYKKZID = query.FieldByName("HYKKZID").AsInteger;
                obj.sHYKDM = obj.iHYKTYPE < 10 ? ("0" + obj.iHYKTYPE) : obj.iHYKTYPE.ToString();
                obj.sHYKPDM = obj.iHYKKZID < 10 ? ("0" + obj.iHYKKZID) : obj.iHYKKZID.ToString();
                obj.iHMCD = query.FieldByName("HMCD").AsInteger;
                obj.sKHQDM = query.FieldByName("KHQDM").AsString.Trim();
                obj.sKHHZM = query.FieldByName("KHHZM").AsString.Trim();
                obj.sYXQCD = query.FieldByName("YXQCD").AsString;
                obj.iBJ_CDNRJM = query.FieldByName("BJ_CDNRJM").AsInteger;
                obj.iFS_YXQ = query.FieldByName("FS_YXQ").AsInteger;
            }
            else
            {
                msg = CrmLibStrings.msgHYKTYPENotFound;
                //throw new Exception(CrmLibStrings.msgHYKTYPENotFound);
            }


            return JsonConvert.SerializeObject(obj);
        }

        public static string GetHYKDEF_TYPE(out string msg, CyQuery query, int iHYKTYPE)
        {
            CRMLIBASHX param = new CRMLIBASHX();
            param.iHYKTYPE = iHYKTYPE;
            return GetHYKDEF(out msg, query, param);
        }
        public static void GetLPXX(out string msg, ref LPXX obj)
        {
            //获取礼品信息，必须传入LPDM或LPID，优先判断LPID
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select D.*,LPFLMC from HYK_JFFHLPXX D,LPFLDEF F";
                    query.SQL.Add(" where 1=1");
                    if (CyDbSystem.GetDbSystemName(query.Connection) == CyDbSystem.OracleDbSystemName)
                        query.SQL.Add(" and D.LPFLID=F.LPFLID(+)");
                    else
                        query.SQL.Add(" and D.LPFLID*=F.LPFLID");

                    if (obj.iLPID >= 0)
                        query.SQL.Add(" and D.LPID=" + obj.iLPID);
                    else if (obj.sLPDM != "")
                        query.SQL.Add(" and D.LPDM='" + obj.sLPDM + "'");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iLPID = query.FieldByName("LPID").AsInteger;
                        obj.sLPMC = query.FieldByName("LPMC").AsString;
                        obj.sLPDM = query.FieldByName("LPDM").AsString;
                        obj.sLPGG = query.FieldByName("LPGG").AsString;
                        obj.fLPDJ = query.FieldByName("LPDJ").AsFloat;
                        obj.fLPJJ = query.FieldByName("LPJJ").AsFloat;
                        obj.fLPJF = query.FieldByName("LPJF").AsFloat;
                        obj.fZSJF = query.FieldByName("ZSJF").AsFloat;
                        obj.iLPFLID = query.FieldByName("LPFLID").AsInteger;
                        obj.sLPFLMC = query.FieldByName("LPFLMC").AsString;
                        obj.iBJ_WKC = query.FieldByName("BJ_WKC").AsInteger;
                    }
                    else
                        throw new Exception(CrmLibStrings.msgHYKTYPENotFound);
                    query.Close();
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
        }

        public static bool GetHYSRLQXX(int iHYID)
        {
            bool bValid = false;
            var iLPSL = 0;
            var msg = "";
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select sum(M.LPSL) LPSL from HYK_SRLPLQJL L,HYK_SRLPLQJLITEM M ";
                    query.SQL.Add(" where L.JLBH=M.JLBH and L.HYID=:HYID and L.ND=:ND");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("ND").AsInteger = DateTime.Now.Year;
                    query.Open();
                    while (!query.Eof)
                    {
                        iLPSL = query.FieldByName("LPSL").AsInteger;
                        query.Next();
                    }
                    if (iLPSL > 0)
                        bValid = true;
                    query.Close();
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
            return bValid;
        }

        public static bool GetHYSRYDXX(int iHYID)
        {
            bool bValid = false;
            var iLPSL = 0;
            var msg = "";
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                try
                {
                    query.SQL.Text = "select sum(M.LPSL) LPSL from HYK_SRLPYDJL L,HYK_SRLPYDJLITEM M ";
                    query.SQL.Add(" where L.JLBH=M.JLBH and L.HYID=:HYID and L.DJSJ>=:SJ");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("SJ").AsDateTime = new DateTime(serverTime.Year, 1, 1);
                    query.Open();
                    while (!query.Eof)
                    {
                        iLPSL = query.FieldByName("LPSL").AsInteger;
                        query.Next();
                    }
                    if (iLPSL > 0)
                    {
                        bValid = true;
                    }
                    query.Close();

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
            return bValid;
        }

        public static bool GetLPLX()
        {
            bool bValid = false;
            int iLPSL = 0;
            var msg = "";
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " select sum(M.LPSL) LPSL from HYK_SRLPYDJL L,HYK_SRLPYDJLITEM M,HYK_HYXX H where L.JLBH=M.JLBH and L.HYID=H.HYID ";
                    query.Open();
                    while (!query.Eof)
                    {
                        iLPSL = query.FieldByName("LPSL").AsInteger;
                        query.Next();
                    }
                    if (iLPSL > 0)
                        bValid = true;
                    query.Close();
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
            return bValid;
        }

        public static SRLPYDXX GetSRLPYDXX(out string msg, int pHYID, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            SRLPYDXX obj = new SRLPYDXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select S.HYID,S.CZDD,S.BZ,I.LPDM,I.LPMC,D.BGDDMC  ";
                    query.SQL.Add(" from HYK_SRLPYDJL S,HYK_SRLPYDJLITEM I,HYK_BGDD D");
                    query.SQL.Add(" where S.JLBH=I.JLBH and S.CZDD=D.BGDDDM(+) and S.HYID = " + pHYID);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.sCZDD = query.FieldByName("CZDD").AsString;
                        obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        obj.sBZ = query.FieldByName("BZ").AsString;
                        obj.sLPDM = query.FieldByName("LPDM").AsString;
                        obj.sLPMC = query.FieldByName("LPMC").AsString;
                    }
                    query.Close();
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
            return obj;
        }


        public static void GetHYSRXXData(out string msg, ref SRLPLQXX obj)
        {
            //获取礼品信息，必须传入LPDM或LPID，优先判断LPID
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");


            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select S.HYID ,H.HYID,H.HYK_NO from HYK_SRLPLQJL S,HYK_SRLPLQJLITEM I,HYK_HYXX H";
                    query.SQL.Add(" where H.HYID=S.HYID");
                    query.SQL.Add(" AND I.JLBH=S.JLBH");
                    query.SQL.Add("AND HYK_NO=:HYK_NO");
                    query.SQL.Add("AND ND=:ND");
                    query.ParamByName("HYK_NO").AsString = obj.sHYK_NO;
                    query.ParamByName("ND").AsInteger = DateTime.Now.Year;
                    query.Open();
                    while (!query.Eof)
                    {

                        obj.iHYID = query.FieldByName("HYID").AsInteger;
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static void GetHYSRYDXXData(out string msg, ref SRLPYDXX obj)
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");

            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                try
                {
                    query.SQL.Text = "select S.HYID ,H.HYID,H.HYK_NO from HYK_SRLPYDJL S,HYK_SRLPYDJLITEM I ,HYK_HYXX H";
                    query.SQL.Add(" where H.HYID=S.HYID");
                    query.SQL.Add(" AND I.JLBH=S.JLBH");
                    query.SQL.Add("AND H.HYK_NO=:HYK_NO");
                    query.SQL.Add("AND S.DJSJ=:SJ");
                    query.ParamByName("HYK_NO").AsString = obj.sHYK_NO;
                    query.ParamByName("SJ").AsDateTime = serverTime;

                    query.Open();
                    while (!query.Eof)
                    {

                        obj.iHYID = query.FieldByName("HYID").AsInteger;
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static void GetLPKC(out string msg, ref LPXX obj)
        {
            //获取礼品库存信息，必须传入LPID/LPDM和BGDDDM
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select K.*,B.BGDDMC from HYK_JFFHLPKC K,HYK_JFFHLPXX L,HYK_BGDD B";
                    query.SQL.Add(" where L.LPID=K.LPID and K.BGDDDM=B.BGDDDM");
                    if (obj.iLPID > 0)
                        query.SQL.Add(" and K.LPID=" + obj.iLPID);
                    else if (obj.sLPDM != "")
                        query.SQL.Add(" and L.LPDM='" + obj.sLPDM + "'");
                    query.SQL.Add(" and B.BGDDDM='" + obj.sBGDDDM + "'");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.fKCSL = query.FieldByName("KCSL").AsFloat;
                        obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                        obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        SetLPXX(obj, obj.iLPID);
                    }
                    else
                        throw new Exception(CrmLibStrings.msgHYKTYPENotFound);
                    query.Close();
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
        }

        public static void SetLPXX(object obj, int pLPID)
        {
            string msg;
            LPXX lp = new LPXX();
            lp.iLPID = pLPID;
            CrmLibProc.GetLPXX(out msg, ref lp);
            ((LPXX)obj).iLPID = ((LPXX)lp).iLPID;
            ((LPXX)obj).sLPMC = ((LPXX)lp).sLPMC;
            ((LPXX)obj).sLPDM = ((LPXX)lp).sLPDM;
            ((LPXX)obj).sLPGG = ((LPXX)lp).sLPGG;
            ((LPXX)obj).fLPDJ = ((LPXX)lp).fLPDJ;
            ((LPXX)obj).fLPJJ = ((LPXX)lp).fLPJJ;

            ((LPXX)obj).fLPJF = ((LPXX)lp).fLPJF;
            ((LPXX)obj).fZSJF = ((LPXX)lp).fZSJF;
            ((LPXX)obj).iLPFLID = ((LPXX)lp).iLPFLID;
            ((LPXX)obj).sLPFLMC = ((LPXX)lp).sLPFLMC;
            ((LPXX)obj).iBJ_WKC = ((LPXX)lp).iBJ_WKC;
        }

        public static string GetHYKStatusName(int pSTATUS)
        {
            if (pSTATUS >= -8 && pSTATUS <= 7)
                return BASECRMDefine.HYXXStatusName[pSTATUS + 8];
            return "";
        }

        public static string GetBFConfig(int pJLBH)
        {
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select CUR_VAL from BFCONFIG";
                    query.SQL.Add(" where JLBH=:JLBH");
                    query.ParamByName("JLBH").AsInteger = pJLBH;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        string ret = query.Fields[0].AsString;
                        query.Close();
                        return ret;
                    }
                    else
                    {
                        query.Close();
                        return "";
                    }
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static int GetBMRS(int iHDID)
        {
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    int iBMRS = 0;
                    query.SQL.Text = "select sum(BMRS)BMRS from HYK_HDCJJL where HDID=:HDID";

                    query.ParamByName("HDID").AsInteger = iHDID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        iBMRS = query.Fields[0].AsInteger;
                        query.Close();
                        return iBMRS;
                    }
                    else
                    {
                        query.Close();
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool GetSHCJ(int pJLBH, int iLoginRYID, int iCJRS, string sCJBZ)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "update HYK_HDCJJL set CJDJR=:CJDJR,CJDJRMC=(select PERSON_NAME from RYXX where PERSON_ID=:CJDJR),CJSJ=:CJSJ,CJRS=:CJRS,CJBZ=:CJBZ where JLBH=:JLBH";
                    query.ParamByName("JLBH").AsInteger = pJLBH;
                    query.ParamByName("CJDJR").AsInteger = iLoginRYID;
                    query.ParamByName("CJSJ").AsDateTime = serverTime;
                    query.ParamByName("CJRS").AsInteger = iCJRS;
                    query.ParamByName("CJBZ").AsString = sCJBZ;
                    int i = query.ExecSQL();
                    if (i == 1)
                    {
                        query.Close();
                        bValid = true;
                    }
                    else
                    {
                        query.Close();
                    }
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static void GetSHSPXX(out string msg, ref SHSPXX obj)
        {
            //获取商户商品信息，必须传入SPDM或SHSPID，优先判断SHSPID
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select D.*,SBMC,SPFLMC,HTH,GHSDM,GSHMC from SHSPXX D,SHSPSB B,SHSPFL F,SHHT H ";
                    query.SQL.Add(" where 1=1");
                    if (CyDbSystem.GetDbSystemName(query.Connection) == CyDbSystem.OracleDbSystemName)
                        query.SQL.Add(" and D.SHSBID=B.SHSBID(+) and D.SHSPFLID=F.SHSPFLID(+) and D.SHHTID=H.SHHTID(+) ");
                    else
                        query.SQL.Add(" and D.SHSBID*=B.SHSBID and D.SHSPFLID*=F.SHSPFLID and D.SHHTID*=H.SHHTID ");

                    if (obj.iSHSPID > 0)
                        query.SQL.Add(" and D.SHSPID=" + obj.iSHSPID);
                    else if (obj.sSPDM != "")
                        query.SQL.Add(" and D.SPDM='" + obj.sSPDM + "'");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iSHSPID = query.FieldByName("SHSPID").AsInteger;
                        obj.sSPMC = query.FieldByName("SPMC").AsString;
                        obj.sSPDM = query.FieldByName("SPDM").AsString;
                        obj.sSPGG = query.FieldByName("SPGG").AsString;

                        obj.iSHSBID = query.FieldByName("SHSBID").AsInteger;
                        obj.sSBMC = query.FieldByName("SBMC").AsString;
                        obj.iSHSPFLID = query.FieldByName("SHSPFLID").AsInteger;
                        obj.sSPFLMC = query.FieldByName("SPFLMC").AsString;
                        obj.iSHHTID = query.FieldByName("SHHTID").AsInteger;
                        obj.sHTH = query.FieldByName("HTH").AsString;
                        obj.sGHSDM = query.FieldByName("GHSDM").AsString;
                        obj.sGHSMC = query.FieldByName("GSHMC").AsString;
                    }
                    else
                        throw new Exception(CrmLibStrings.msgHYKTYPENotFound);
                    query.Close();
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
        }

        public static string GetLPFFGZLXName(int pGZLX)
        {
            switch (pGZLX)
            {
                case 0: return "日常礼";
                case 5: return "生日礼";
                case 1: return "首刷礼";
                case 2: return "办卡礼";
                case 3: return "积分返礼";
                case 4: return "来店礼";
                case 6: return "印花换购";
                case 7: return "满额送礼";
                case 8: return "抽奖活动";
                default: return "";
            }
        }

        public static void DeleteDataBase(CyQuery query, out string msg, string sql, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            if (query == null)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
                query = new CyQuery(conn);
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    query.SQL.Text = sql;
                    query.ExecSQL();
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);

                }
                conn.Close();
            }
            else
            {
                query.Close();
                query.SQL.Text = sql;
                query.ExecSQL();
            }
        }

        public static void DeleteDataTables(CyQuery query, out string msg, string tables, string jlbhField, int pJLBH, string zxrField = "ZXR", string sDBConnName = "CRMDB")
        {
            if (tables.Substring(tables.Length - 1, 1) != ";")
                tables = tables + ";";
            msg = string.Empty;
            if (query == null)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
                query = new CyQuery(conn);
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    DeleteDataSQL(query, tables, jlbhField, pJLBH, zxrField);
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);

                }
                conn.Close();
            }
            else
            {
                DeleteDataSQL(query, tables, jlbhField, pJLBH, zxrField);
            }
        }

        static void DeleteDataSQL(CyQuery query, string tables, string jlbhField, int pJLBH, string zxrField)
        {
            int iCount = 0;//计数用
            bool bZXR = false;
            while (tables != "")
            {
                string table1 = tables.Substring(0, tables.IndexOf(";"));
                tables = tables.Substring(tables.IndexOf(";") + 1, tables.Length - tables.IndexOf(";") - 1);
                if (iCount == 0)
                {
                    try
                    {
                        query.SQL.Text = "select " + zxrField + " from " + table1 + " where 1=2";
                        query.Open();
                        query.Close();
                        bZXR = true;
                    }
                    catch
                    {
                        bZXR = false;
                    }
                }
                query.SQL.Text = "delete from " + table1 + " where " + jlbhField + "=" + pJLBH;
                if (iCount == 0 && zxrField != "" && bZXR)
                    query.SQL.Add(" and " + zxrField + " is null");
                if (query.ExecSQL() == 0 && iCount == 0 && zxrField != "" && bZXR)
                    throw new Exception(CrmLibStrings.msgExecExecuted);
                iCount++;
                //处理特殊表
                if (table1 == "HYK_GRXX")
                {
                    query.SQL.Text = "update HYK_HYXX set HY_NAME=null where HYID=" + pJLBH;
                    query.ExecSQL();
                }
                if (table1 == "HYK_GKDA")
                {
                    query.SQL.Text = "update HYK_HYXX set HY_NAME=null where GKID=" + pJLBH;
                    query.ExecSQL();
                }
            }
        }

        public static DateTime GetSysDatetime()
        {
            string msg;
            DateTime dNow;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                dNow = CyDbSystem.GetDbServerTime(query);
            }
            catch (Exception e)
            {
                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            conn.Close();
            return dNow;
        }

        public static bool IsNumber(string strInput)
        {
            string strValue = strInput.Trim();
            Regex regex = new Regex(@"^[0-9]+\.?[0-9]*$");
            Match match = regex.Match(strValue);
            return match.Success;
        }

        public static bool IsDate(string Input)
        {
            string strValue = Input.Trim();
            //Regex regex = new Regex(@"^(?:(?!0000)[0-9]{4}([-/.]?)(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-/.]?)0?2\2(?:29))$");
            Regex regex = new Regex(@"^(?:(?!0000)[0-9]{4}([-/.])(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-/.])0?2\2(?:29))$");
            Match match = regex.Match(strValue);
            return match.Success;
        }

        public static DateTime GetHYKYXQ(CyQuery query, int pHYKTYPE)
        {
            string msg = string.Empty;
            DateTime dNow = new DateTime();

            dNow = CyDbSystem.GetDbServerTime(query);
            query.SQL.Text = " select YXQCD from HYKDEF where HYKTYPE=:HYKTYPE";
            query.ParamByName("HYKTYPE").AsInteger = pHYKTYPE;
            query.Open();
            if (!query.IsEmpty)
            {
                dNow = GetYXQ(dNow, query.FieldByName("YXQCD").AsString);
            }
            query.Close();
            return dNow;
        }
        public static DateTime GetYXQ(DateTime dNow, string YXQCD)
        {
            if (string.IsNullOrEmpty(YXQCD))
                return DateTime.MinValue;
            string sDateType = YXQCD.Substring(YXQCD.Length - 1, 1).ToUpper();
            string a = YXQCD.Substring(0, YXQCD.Length - 1);
            int cd = Convert.ToInt32(YXQCD.Substring(0, YXQCD.Length - 1));
            if (sDateType == "Y")
            {
                return dNow.AddYears(cd);
            }
            else if (sDateType == "M")
            {
                return dNow.AddMonths(cd);
            }
            else if (sDateType == "D")
            {
                return dNow.AddDays(cd);
            }
            else
            {
                //增加不输入YMD的判断，按年计算
                if (Int32.TryParse(YXQCD, out cd))
                    return dNow.AddYears(cd);
                else
                    return DateTime.MinValue;
            }

        }
        public static DateTime GetYXQ(string YXQCD)
        {
            return GetYXQ(DateTime.Now.Date, YXQCD);
        }
        public static string GetSJGZ(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = "";
            int pBJ_XFJE = 0;
            string sjj = "升";
            if (param.iSJ == 0)
                sjj = "降";
            SJGZ obj = new SJGZ();
            query.SQL.Text = "select * from HYK_DJSQGZ where HYKTYPE=:HYKTYPE";
            query.ParamByName("HYKTYPE").AsInteger = param.iHYKTYPE;
            query.Open();
            if (!query.IsEmpty)
            {
                pBJ_XFJE = query.FieldByName("BJ_XFJE").AsInteger;
            }
            query.Close();
            if (param.iSJ == 0) //降级
            {
                if (pBJ_XFJE == 0) //按积分
                {
                    query.SQL.Text = "select D.*,T.HYKNAME HYKNAME_NEW from HYK_DJSQGZ D,HYKDEF T ";
                    query.SQL.Add(" where D.HYKTYPE_NEW=T.HYKTYPE and QDJF>:QDJF and BJ_SJ=:BJ_SJ order by QDJF asc");
                    query.ParamByName("QDJF").AsFloat = param.fBQJF;

                }
                if (pBJ_XFJE == 1) //按当日消费金额
                {
                    query.SQL.Text = "select D.*,T.HYKNAME HYKNAME_NEW from HYK_DJSQGZ D,HYKDEF T ";
                    query.SQL.Add(" where D.HYKTYPE_NEW=T.HYKTYPE and DRXFJE>:XFJE and BJ_SJ=:BJ_SJ order by DRXFJE asc");
                    query.ParamByName("XFJE").AsFloat = param.fXFJE;
                }

            }
            if (param.iSJ == 1) //升级 
            {
                if (pBJ_XFJE == 0)
                {
                    query.SQL.Text = "select D.*,T.HYKNAME HYKNAME_NEW from HYK_DJSQGZ D,HYKDEF T ";
                    query.SQL.Add(" where D.HYKTYPE_NEW=T.HYKTYPE and QDJF<=:QDJF and BJ_SJ=:BJ_SJ order by QDJF desc");
                    query.ParamByName("QDJF").AsFloat = param.fBQJF;

                }
                if (pBJ_XFJE == 1)
                {
                    query.SQL.Text = "select D.*,T.HYKNAME HYKNAME_NEW from HYK_DJSQGZ D,HYKDEF T ";
                    query.SQL.Add(" where D.HYKTYPE_NEW=T.HYKTYPE and DRXFJE<=:XFJE and BJ_SJ=:BJ_SJ order by DRXFJE desc");
                    query.ParamByName("XFJE").AsFloat = param.fXFJE;
                }
            }
            query.ParamByName("BJ_SJ").AsInteger = param.iSJ;
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iHYKTYPE_NEW = query.FieldByName("HYKTYPE_NEW").AsInteger;
                obj.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
                obj.fQDJF = query.FieldByName("QDJF").AsFloat;
                obj.fDRXFJE = query.FieldByName("DRXFJE").AsFloat;
                obj.iBJ_XFJE = query.FieldByName("BJ_XFJE").AsInteger;
            }
            else
            {
                msg = "没有找到" + sjj + "级规则";
                return null;
            }
            query.Close();

            return JsonConvert.SerializeObject(obj);
        }

        public static int BGDDToMDID(CyQuery query, string pBGDDDM)
        {
            int iMDID = 0;
            query.Close();
            query.SQL.Text = "select MDID from HYK_BGDD WHERE BGDDDM=:BGDDDM";
            query.ParamByName("BGDDDM").AsString = pBGDDDM;
            query.Open();
            iMDID = query.Fields[0].AsInteger;
            query.Close();
            return iMDID;
        }

        public static int MDDMToMDID(CyQuery query, string pMDDM)
        {
            int iMDID = 0;
            query.Close();
            query.SQL.Text = "select MDID from MDDY WHERE MDDM=:MDDM";
            query.ParamByName("MDDM").AsString = pMDDM;
            query.Open();
            if (!query.IsEmpty)
            {
                iMDID = query.Fields[0].AsInteger;
            }
            query.Close();
            return iMDID;
        }



        public static void SaveGRXX(CyQuery query, HYXX_Detail HYKXX, int pDJR, string pDJRMC)
        {
            query.SQL.Text = "";
            query.Close();
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            query.Close();
            bool bGRXX = false;
            query.SQL.Text = "select 1 from HYK_GKDA where GKID=" + HYKXX.iGKID;
            query.Open();
            bGRXX = !query.IsEmpty;
            if (bGRXX)
            {
                query.SQL.Text = "update HYK_GKDA set GK_NAME=:GK_NAME,SEX=:SEX,";
                query.SQL.Add(" CSRQ=:CSRQ,SFZBH=:SFZBH,ZJLXID=:ZJLXID,");
                query.SQL.Add(" TXDZ=:TXDZ,YZBM=:YZBM,E_MAIL=:E_MAIL,GZDW=:GZDW,ZW=:ZW,BZ=:BZ,");
                query.SQL.Add(" XLID=:XLID,ZYID=:ZYID,JTSRID=:JTSRID,ZSCSJID=:ZSCSJID,JTGJID=:JTGJID,JTCYID=:JTCYID,");
                query.SQL.Add(" PHONE=:PHONE,FAX=:FAX,SJHM=:SJHM,BGDH=:BGDH,");
                query.SQL.Add(" QYID=:QYID,GXR=:DJR,GXRMC=:DJRMC,GXSJ=:DJSJ,");
                query.SQL.Add(" CPH=:CPH,BJ_CLD=:BJ_CLD,QYMC=:QYMC,QYXZ=:QYXZ,");
                query.SQL.Add(" GKNC=:GKNC,MZID=:MZID,JHJNR=:JHJNR");
                //2014.12.1
                query.SQL.Add(" ,TJRYID=:TJRYID,QQ=:QQ,WX=:WX,WB=:WB,XQ=:XQ,BJ_YZZJLX=:BJ_YZZJLX,BJ_YZSJHM=:BJ_YZSJHM");//TXDZ1=:TXDZ1,TXDZ2=:TXDZ2,TXDZ3=:TXDZ3,TXDZ4=:TXDZ4,
                query.SQL.Add(" ,QCPPID=:QCPPID,JHBJ=:JHBJ,PPHY=:PPHY,PPXQ=:PPXQ,KHJLRYID=:KHJLRYID,CANVAS=:CANVAS,");
                query.SQL.Add(" XXFS=:XXFS,CXXX=:CXXX,YYAH=:YYAH");
                //2013.12.4
                query.SQL.Add(" ,CANSMS=:CANSMS ");
                query.SQL.Add(" ,XQID=:XQID,SW=:SW,CM=:CM,XZ=:XZ,SX=:SX,ROAD=:ROAD,HOUSENUM=:HOUSENUM");
                query.SQL.Add(" where GKID=:GKID");
            }
            else
            {
                query.SQL.Text = "insert into HYK_GKDA(GKID,GK_NAME,SEX,CSRQ,SFZBH,ZJLXID,TXDZ,YZBM,E_MAIL,GZDW,BZ,ZW,";
                query.SQL.Add(" XLID,ZYID,JTSRID,ZSCSJID,JTGJID,JTCYID,PHONE,FAX,SJHM,BGDH,");
                query.SQL.Add(" QYID,DJR,DJRMC,DJSJ,CPH,BJ_CLD,QYMC,QYXZ,GKNC,MZID,JHJNR");
                //
                query.SQL.Add(" ,TJRYID,QQ,WX,WB,XQ,BJ_YZZJLX,BJ_YZSJHM");//TXDZ1,TXDZ2,TXDZ3,TXDZ4,
                query.SQL.Add(" ,QCPPID,JHBJ,PPHY,PPXQ,KHJLRYID,CANVAS,XXFS,CXXX,YYAH");
                query.SQL.Add(",CANSMS ");
                query.SQL.Add(",XQID,SW,CM,XZ,SX,ROAD,HOUSENUM");
                query.SQL.Add(" )");
                query.SQL.Add(" values (:GKID,:GK_NAME,:SEX,:CSRQ,:SFZBH,:ZJLXID,:TXDZ,:YZBM,:E_MAIL,:GZDW,:BZ,:ZW,");
                query.SQL.Add(" :XLID,:ZYID,:JTSRID,:ZSCSJID,:JTGJID,:JTCYID,:PHONE,:FAX,:SJHM,:BGDH,");
                query.SQL.Add(" :QYID,:DJR,:DJRMC,:DJSJ,:CPH,:BJ_CLD,:QYMC,:QYXZ,:GKNC,:MZID,:JHJNR");
                //
                query.SQL.Add(" ,:TJRYID,:QQ,:WX,:WB,:XQ,:BJ_YZZJLX,:BJ_YZSJHM");//:TXDZ1,:TXDZ2,:TXDZ3,:TXDZ4
                query.SQL.Add(" ,:QCPPID,:JHBJ,:PPHY,:PPXQ,:KHJLRYID,:CANVAS,:XXFS,:CXXX,:YYAH");
                query.SQL.Add(",:CANSMS");
                query.SQL.Add(",:XQID,:SW,:CM,:XZ,:SX,:ROAD,:HOUSENUM");
                query.SQL.Add(" )");
            }
            query.ParamByName("GKID").AsInteger = HYKXX.iGKID;
            query.ParamByName("GK_NAME").AsString = HYKXX.sHY_NAME;
            query.ParamByName("SEX").AsInteger = HYKXX.iSEX;
            if (HYKXX.dCSRQ == "")
            {
                query.ParamByName("CSRQ").AsDateTime = FormatUtils.ParseDateString("1900-01-01");
            }
            else
            {
                query.ParamByName("CSRQ").AsDateTime = FormatUtils.ParseDateString(HYKXX.dCSRQ);
            }
            query.ParamByName("SFZBH").AsString = HYKXX.sSFZBH;
            query.ParamByName("ZJLXID").AsInteger = HYKXX.iZJLXID;
            query.ParamByName("TXDZ").AsString = HYKXX.sTXDZ;
            query.ParamByName("YZBM").AsString = HYKXX.sYZBM;
            query.ParamByName("E_MAIL").AsString = HYKXX.sEMAIL;
            query.ParamByName("GZDW").AsString = HYKXX.sGZDW;
            query.ParamByName("BZ").AsString = HYKXX.sBZ;
            query.ParamByName("ZW").AsString = HYKXX.sZW;
            query.ParamByName("XLID").AsInteger = HYKXX.iXLID;
            if (HYKXX.iZYID == -1 || HYKXX.iZYID == 0)
            {
                query.ParamByName("ZYID").AsString = "";
            }
            else
            {
                query.ParamByName("ZYID").AsInteger = HYKXX.iZYID;
            }
            query.ParamByName("JTSRID").AsInteger = HYKXX.iJTSRID;
            query.ParamByName("JTGJID").AsInteger = HYKXX.iJTGJID;
            query.ParamByName("JTCYID").AsInteger = HYKXX.iJTCYID;
            query.ParamByName("ZSCSJID").AsInteger = HYKXX.iZSCSJID;
            query.ParamByName("PHONE").AsString = HYKXX.sPHONE;
            query.ParamByName("FAX").AsString = HYKXX.sFAX;
            query.ParamByName("SJHM").AsString = HYKXX.sSJHM;
            query.ParamByName("BGDH").AsString = HYKXX.sBGDH;
            if (HYKXX.iQYID == -1 || HYKXX.iQYID == 0)
            {
                query.ParamByName("QYID").AsString = "";
            }
            else
            {
                query.ParamByName("QYID").AsInteger = HYKXX.iQYID;
            }
            query.ParamByName("DJR").AsInteger = pDJR;
            query.ParamByName("DJRMC").AsString = pDJRMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CPH").AsString = HYKXX.sCPH;
            query.ParamByName("BJ_CLD").AsInteger = HYKXX.iBJ_CLD;
            query.ParamByName("QYMC").AsString = HYKXX.sQIYEMC;
            query.ParamByName("QYXZ").AsString = HYKXX.sQIYEXZ;
            query.ParamByName("GKNC").AsString = HYKXX.sGKNC;
            query.ParamByName("MZID").AsInteger = HYKXX.iMZID;
            if (HYKXX.dJHJNR == "")
            {
                query.ParamByName("JHJNR").AsString = "";
            }
            else
            {
                query.ParamByName("JHJNR").AsDateTime = FormatUtils.ParseDateString(HYKXX.dJHJNR);
            }
            //
            query.ParamByName("TJRYID").AsInteger = HYKXX.iTJRYID;
            query.ParamByName("QQ").AsString = HYKXX.sQQ;
            query.ParamByName("WX").AsString = HYKXX.sWX;
            query.ParamByName("WB").AsString = HYKXX.sWB;
            query.ParamByName("XQ").AsString = HYKXX.sXQ;
            //query.ParamByName("TXDZ1").AsString = HYKXX.sTXDZ1;
            //query.ParamByName("TXDZ2").AsString = HYKXX.sTXDZ2;
            //query.ParamByName("TXDZ3").AsString = HYKXX.sTXDZ3;
            //query.ParamByName("TXDZ4").AsString = HYKXX.sTXDZ4;
            query.ParamByName("BJ_YZZJLX").AsInteger = HYKXX.iBJ_YZZJLX;
            query.ParamByName("BJ_YZSJHM").AsInteger = HYKXX.iBJ_YZSJHM;
            //query.ParamByName("QCPP").AsString = HYKXX.sQCPP;
            query.ParamByName("QCPPID").AsInteger = HYKXX.iQCPPID;//2014.12.4
            query.ParamByName("JHBJ").AsInteger = HYKXX.iJHBJ;
            query.ParamByName("PPHY").AsString = HYKXX.sPPHY;
            query.ParamByName("PPXQ").AsString = HYKXX.sPPXQ;
            query.ParamByName("KHJLRYID").AsInteger = HYKXX.iKHJLRYID;
            query.ParamByName("CANVAS").AsString = HYKXX.sCanvas;
            query.ParamByName("CXXX").AsString = HYKXX.sCXXX;
            query.ParamByName("XXFS").AsString = HYKXX.sXXFS;
            query.ParamByName("YYAH").AsString = HYKXX.sYYAH;
            query.ParamByName("CANSMS").AsInteger = HYKXX.iCANSMS;
            query.ParamByName("XQID").AsInteger = HYKXX.iXQID;
            //query.ParamByName("SQID").AsInteger = HYKXX.iSQID;
            query.ParamByName("SW").AsString = HYKXX.sSW;
            query.ParamByName("CM").AsString = HYKXX.sCM;
            query.ParamByName("XZ").AsString = HYKXX.sXZ;
            query.ParamByName("SX").AsString = HYKXX.sSX;
            query.ParamByName("ROAD").AsString = HYKXX.sROAD;
            query.ParamByName("HOUSENUM").AsString = HYKXX.sHOUSENUM;
            //query.ParamByName("IMAGEURl").AsString = HYKXX.sIMGURL;
            //query.ParamByName("XSGSKH").AsString = HYKXX.sXSGSKH;
            query.ExecSQL();

            //统一修改HYK_HYXX中同一GKID的记录（主要更新GKID，以及HY_NAME）by XXM
            if (!HYKXX.sHYK_NO.Equals(""))
            {
                int pGKID = 0;
                int pHYID = 0;
                query.SQL.Text = "select NVL(GKID,0) GKID,NVL(HYID,0) HYID from HYK_HYXX where HYK_NO='" + HYKXX.sHYK_NO + "'";
                query.Open();
                if (!query.IsEmpty)
                {
                    pGKID = query.FieldByName("GKID").AsInteger;
                    pHYID = query.FieldByName("HYID").AsInteger;
                }
                query.Close();
                if (pGKID != 0)//将所有此GKID的HYXX都修改
                {
                    query.SQL.Text = " update HYK_HYXX set HY_NAME=:HY_NAME,GKID=:GKID where GKID=:pGKID or GKID=:GKID ";
                    query.ParamByName("GKID").AsInteger = HYKXX.iGKID;
                    query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
                    query.ParamByName("pGKID").AsInteger = pGKID;
                }
                else//这是专门为此卡新创建的顾客信息
                {
                    query.SQL.Text = " update HYK_HYXX set HY_NAME=:HY_NAME,GKID=:GKID where HYK_NO=:HYK_NO ";
                    query.ParamByName("GKID").AsInteger = HYKXX.iGKID;
                    query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
                    query.ParamByName("HYK_NO").AsString = HYKXX.sHYK_NO;
                }
                query.ExecSQL();
                if (pHYID != 0)
                {
                    query.SQL.Text = " update HYK_CHILD_JL set HY_NAME=:HY_NAME where HYID=:HYID";
                    query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
                    query.ParamByName("HYID").AsInteger = pHYID;
                    query.ExecSQL();
                }
            }

            //note by XXM
            //query.SQL.Text = "update HYK_HYXX set HY_NAME=:HY_NAME,GKID=:GKID where HYID=:HYID ";
            ////query.SQL.Text = "update HYK_HYXX set HY_NAME=:HY_NAME where GKID=:GKID";
            //query.ParamByName("GKID").AsInteger = HYKXX.iGKID;
            //query.ParamByName("HY_NAME").AsString = HYKXX.sHY_NAME;
            //query.ParamByName("HYID").AsInteger = HYKXX.iHYID;
            //query.ExecSQL();

        }

        public static void UpdateYHQZH(out string msg, CyQuery query, int pHYID, int pCLLX, int pJLBH, int pYHQID, int pMDID, int pCXID, double pJE, string pJSRQ, string pZY = "", string sMDFWDM = "", int pCLFS = 0, int aJLBH = 0)
        {
            //如果确定MDFWDM则调用UpdateYHQZH_MDFW，否则根据传入的MDID计算MDFWDM
            msg = string.Empty;
            int iFS_YQMDFW = 0;
            query.SQL.Text = "select FS_YQMDFW from YHQDEF WHERE YHQID=:YHQID";
            query.ParamByName("YHQID").AsInteger = pYHQID;
            query.Open();
            if (!query.IsEmpty)
                iFS_YQMDFW = query.Fields[0].AsInteger;
            else
            {
                msg = "优惠券不存在";
                return;
            }
            query.Close();
            // string sMDFWDM = string.Empty;
            if (sMDFWDM == "")
            {
                switch (iFS_YQMDFW)
                {
                    case 1: sMDFWDM = " "; break;
                    case 2:
                        query.SQL.Text = "select SHDM from MDDY WHERE MDID=" + pMDID;
                        query.Open();
                        sMDFWDM = query.Fields[0].AsString;
                        break;
                    case 3:
                        query.SQL.Text = "select MDDM from MDDY WHERE MDID=" + pMDID;
                        query.Open();
                        sMDFWDM = query.Fields[0].AsString;
                        break;
                }
            }
            switch (pCLFS)
            {
                case 0:
                    UpdateYHQZH_MDFW(out msg, query, pHYID, pCLLX, pJLBH, pYHQID, pMDID, pCXID, pJE, pJSRQ, sMDFWDM, pZY);
                    break;
                case 1:
                    #region 印花扣减
                    query.Close();
                    query.SQL.Text = "select * from HYK_YHQZH Y where Y.HYID=:HYID and YHQID=:YHQID and JSRQ>sysdate-1 and MDFWDM=:MDFWDM order by JSRQ";
                    query.ParamByName("HYID").AsInteger = pHYID;
                    query.ParamByName("YHQID").AsInteger = pYHQID;
                    query.ParamByName("MDFWDM").AsString = sMDFWDM;
                    query.Open();
                    List<YHQZH> lst = new List<YHQZH>();
                    while (!query.Eof)
                    {
                        YHQZH one = new YHQZH();
                        one.iCXID = query.FieldByName("CXID").AsInteger;
                        one.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        one.fJE = query.FieldByName("JE").AsFloat;
                        lst.Add(one);
                        query.Next();
                    }
                    if (lst.Count > 0)
                    {
                        double tBDJE = 0;
                        foreach (YHQZH one in lst)
                        {
                            if (-pJE < one.fJE)
                            {
                                tBDJE = pJE;
                                pJE = 0;
                            }
                            else
                            {
                                tBDJE = -one.fJE;
                                pJE = pJE + one.fJE;
                            }
                            UpdateYHQZH_MDFW(out msg, query, pHYID, pCLLX, pJLBH, pYHQID, pMDID, one.iCXID, tBDJE, one.dJSRQ, sMDFWDM, pZY);
                            if (pJE == 0)
                                break;
                        }
                    }
                    else
                    {
                        msg = "没有可用的券";
                    }
                    #endregion
                    break;
                case 2:
                    query.Close();
                    query.SQL.Text = " select HYID,YHQID,CXID,MDFWDM,JFJE,JSRQ from HYK_YHQCLJL where HYID=:HYID and YHQID=:YHQID and JLBH=:JLBH  and ZY='印花换购'";
                    query.ParamByName("HYID").AsInteger = pHYID;
                    query.ParamByName("YHQID").AsInteger = pYHQID;
                    query.ParamByName("JLBH").AsInteger = aJLBH;
                    query.Open();
                    List<YHQZH> subList = new List<YHQZH>();
                    while (!query.Eof)
                    {
                        YHQZH subRecord = new YHQZH();
                        subRecord.iHYID = query.FieldByName("HYID").AsInteger;
                        subRecord.iYHQID = query.FieldByName("YHQID").AsInteger;
                        subRecord.iCXID = query.FieldByName("CXID").AsInteger;
                        subRecord.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                        subRecord.fJE = query.FieldByName("JFJE").AsFloat;
                        subRecord.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        subList.Add(subRecord);
                        query.Next();
                    }
                    foreach (YHQZH subRecord in subList)
                    {
                        UpdateYHQZH_MDFW(out msg, query, pHYID, pCLLX, pJLBH, pYHQID, pMDID, subRecord.iCXID, -subRecord.fJE, subRecord.dJSRQ, subRecord.sMDFWDM, pZY);
                    }

                    break;



            }
            //double fDFJE = 0, fJFJE = 0;
            //switch (pCLLX)
            //{
            //    case BASECRMDefine.CZK_CLLX_JK:
            //    case BASECRMDefine.CZK_CLLX_CK:
            //        fJFJE = pJE; break;
            //    case BASECRMDefine.CZK_CLLX_QL:
            //    case BASECRMDefine.CZK_CLLX_QK:
            //        fDFJE = pJE; break;
            //}
            //query.Close();
            //query.SQL.Text = "update HYK_YHQZH set JE=JE+round(:JE,2) where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID";
            //query.ParamByName("JE").AsFloat = pJE;
            //query.ParamByName("HYID").AsInteger = pHYID;
            //query.ParamByName("YHQID").AsInteger = pYHQID;
            //query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            //query.ParamByName("MDFWDM").AsString = sMDFWDM;
            //query.ParamByName("CXID").AsInteger = pCXID;
            //if (query.ExecSQL() == 0)
            //{
            //    query.SQL.Text = "insert into HYK_YHQZH(HYID,YHQID,CXID,JSRQ,MDFWDM,JE,JYDJJE)";
            //    query.SQL.Add(" values(:HYID,:YHQID,:CXID,:JSRQ,:MDFWDM,:JE,0)");
            //    query.ParamByName("JE").AsFloat = pJE;
            //    query.ParamByName("HYID").AsInteger = pHYID;
            //    query.ParamByName("YHQID").AsInteger = pYHQID;
            //    query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            //    query.ParamByName("MDFWDM").AsString = sMDFWDM;
            //    query.ParamByName("CXID").AsInteger = pCXID;
            //    query.ExecSQL();
            //}
            //int tJYBH = SeqGenerator.GetSeq("HYK_YHQCLJL");
            //query.SQL.Text = "insert into HYK_YHQCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,YHQID,CXID,JSRQ,MDFWDM,MDID,ZY,JFJE,DFJE,YE)";
            //query.SQL.Add(" select :JYBH,HYID," + CyDbSystem.GetDbServerTimeFuncSql(query.Connection) + ",:CLLX,:JLBH,YHQID,CXID,JSRQ,MDFWDM,:MDID,:ZY,round(:JFJE,2),round(:DFJE,2),JE");
            //query.SQL.Add(" from HYK_YHQZH where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID");
            //query.ParamByName("JYBH").AsInteger = tJYBH;
            //query.ParamByName("HYID").AsInteger = pHYID;
            //query.ParamByName("YHQID").AsInteger = pYHQID;
            //query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            //query.ParamByName("MDFWDM").AsString = sMDFWDM;
            //query.ParamByName("CXID").AsInteger = pCXID;
            //query.ParamByName("CLLX").AsInteger = pCLLX;
            //query.ParamByName("JLBH").AsInteger = pJLBH;
            //query.ParamByName("MDID").AsInteger = pMDID;
            //query.ParamByName("ZY").AsString = pZY;
            //query.ParamByName("JFJE").AsFloat = fJFJE;
            //query.ParamByName("DFJE").AsFloat = fDFJE;
            //query.ExecSQL();
        }

        public static void UpdateYHQZH_MDFW(out string msg, CyQuery query, int pHYID, int pCLLX, int pJLBH, int pYHQID, int pMDID, int pCXID, double pJE, string pJSRQ, string pMDFWDM, string pZY)
        {
            msg = "";
            double fDFJE = 0, fJFJE = 0;
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            switch (pCLLX)
            {
                case BASECRMDefine.CZK_CLLX_JK:
                case BASECRMDefine.CZK_CLLX_CK:
                    fJFJE = pJE; break;
                case BASECRMDefine.CZK_CLLX_QL:
                case BASECRMDefine.CZK_CLLX_QK:
                    fDFJE = Math.Abs(pJE); break;
            }
            query.Close();
            query.SQL.Text = "update HYK_YHQZH set JE=JE+round(:JE,2) where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID";
            query.ParamByName("JE").AsFloat = pJE;
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("YHQID").AsInteger = pYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            query.ParamByName("MDFWDM").AsString = pMDFWDM;
            query.ParamByName("CXID").AsInteger = pCXID;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into HYK_YHQZH(HYID,YHQID,CXID,JSRQ,MDFWDM,JE,JYDJJE)";
                query.SQL.Add(" values(:HYID,:YHQID,:CXID,:JSRQ,:MDFWDM,:JE,0)");
                query.ParamByName("JE").AsFloat = pJE;
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("YHQID").AsInteger = pYHQID;
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
                query.ParamByName("MDFWDM").AsString = pMDFWDM;
                query.ParamByName("CXID").AsInteger = pCXID;
                query.ExecSQL();
            }
            int tJYBH = SeqGenerator.GetSeq("HYK_YHQCLJL");
            query.SQL.Text = "insert into HYK_YHQCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,YHQID,CXID,JSRQ,MDFWDM,MDID,ZY,JFJE,DFJE,YE)";
            query.SQL.Add(" select :JYBH,HYID," + CyDbSystem.GetDbServerTimeFuncSql(query.Connection) + ",:CLLX,:JLBH,YHQID,CXID,JSRQ,MDFWDM,:MDID,:ZY,round(:JFJE,2),round(:DFJE,2),JE");
            query.SQL.Add(" from HYK_YHQZH where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID");
            query.ParamByName("JYBH").AsInteger = tJYBH;
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("YHQID").AsInteger = pYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            query.ParamByName("MDFWDM").AsString = pMDFWDM;
            query.ParamByName("CXID").AsInteger = pCXID;
            query.ParamByName("CLLX").AsInteger = pCLLX;
            query.ParamByName("JLBH").AsInteger = pJLBH;
            query.ParamByName("MDID").AsInteger = pMDID;
            query.ParamByName("ZY").AsString = pZY;
            query.ParamByName("JFJE").AsFloat = fJFJE;
            query.ParamByName("DFJE").AsFloat = fDFJE;
            query.ExecSQL();
            decimal balance;
            EncryptBalanceOfCouponCard(query, pHYID, pYHQID, pCXID, pJSRQ, pMDFWDM, serverTime, out balance);
        }

        public static void EncryptBalanceOfCouponCard(CyQuery query, int pHYID, int pYHQID, int pCXID, string pJSRQ, string pMDFWDM, DateTime serverTime, out Decimal balance)
        {
            balance = 0;
            Decimal frozen = 0;
            byte[] bytesTM = new byte[8];
            string dbSysName = CyDbSystem.GetDbSystemName(query.Connection);
            switch (dbSysName)
            {
                case CyDbSystem.SybaseDbSystemName:
                    query.SQL.Text = "select JE,JYDJJE,convert(binary(8),TM) as TM from HYK_YHQZH where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID";
                    break;
                case CyDbSystem.OracleDbSystemName:
                    query.SQL.Text = "select JE,JYDJJE,to_char(TM) as TM from HYK_YHQZH where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID";
                    break;
            }
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("YHQID").AsInteger = pYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            query.ParamByName("MDFWDM").AsString = pMDFWDM;
            query.ParamByName("CXID").AsInteger = pCXID;
            query.Open();
            if (!query.IsEmpty)
            {
                balance = query.FieldByName("JE").AsDecimal;
                frozen = query.FieldByName("JYDJJE").AsDecimal;
                switch (dbSysName)
                {
                    case CyDbSystem.SybaseDbSystemName:
                        query.FieldByName("TM").GetBytes(0, bytesTM, 0, 8);
                        break;
                    case CyDbSystem.OracleDbSystemName:
                        string strTM = query.FieldByName("TM").AsString;
                        if ((strTM != null) && (strTM.Length > 0))
                        {
                            Int64 longTM = Int64.Parse(strTM);
                            for (int j = 0; j < 8; j++)
                            {
                                bytesTM[j] = (byte)(longTM >> (j * 8));
                            }
                        }
                        break;
                }
            }
            query.Close();
            byte[] DesKey = { (byte)'q', (byte)'R', (byte)'s', (byte)'Z', (byte)'a', (byte)'P', (byte)'c', (byte)'m' };

            byte[] newKey = EncryptUtils.DesEncrypt(DesKey, bytesTM);
            byte[] bytesBalance = new byte[8];
            int intBalance = Convert.ToInt32(balance * 100);
            for (int j = 0; j < 4; j++)
            {
                bytesBalance[j] = (byte)(intBalance >> (j * 8));
            }
            for (int j = 4; j < 8; j++)
            {
                bytesBalance[j] = 0;
            }
            byte[] bytesDAC = EncryptUtils.DesEncrypt(newKey, bytesBalance);

            query.SQL.Text = "update HYK_YHQBD set BDSJ=:BDSJ,DAC=:DAC where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID";
            query.ParamByName("BDSJ").AsDateTime = serverTime;
            query.ParamByName("DAC").SetParamValue(DbType.Binary, bytesDAC);
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("YHQID").AsInteger = pYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
            query.ParamByName("MDFWDM").AsString = pMDFWDM;
            query.ParamByName("CXID").AsInteger = pCXID;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into HYK_YHQBD (HYID,YHQID,CXID,JSRQ,MDFWDM,BDSJ,DAC) values(:HYID,:YHQID,:CXID,:JSRQ,:MDFWDM,:BDSJ,:DAC)";
                query.ParamByName("BDSJ").AsDateTime = serverTime;
                query.ParamByName("DAC").SetParamValue(DbType.Binary, bytesDAC);
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("YHQID").AsInteger = pYHQID;
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(pJSRQ);
                query.ParamByName("MDFWDM").AsString = pMDFWDM;
                query.ParamByName("CXID").AsInteger = pCXID;
                query.ExecSQL();
            }
            balance += frozen;
        }

        public static void UpdateJEZH(out string msg, CyQuery query, int pHYID, int pCLLX, int pJLBH, int pMDID, double pJE, string pZY = "")
        {
            msg = string.Empty;
            query.Close();
            query.SQL.Text = "";
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            query.Close();
            double fDFJE = 0, fJFJE = 0;
            switch (pCLLX)
            {
                case BASECRMDefine.CZK_CLLX_JK:
                case BASECRMDefine.CZK_CLLX_CK:
                    fJFJE = pJE; break;
                case BASECRMDefine.CZK_CLLX_QL:
                case BASECRMDefine.CZK_CLLX_QK:
                    fDFJE = pJE; break;
            }
            query.SQL.Text = "update HYK_JEZH set YE=YE+round(:JE,2) where HYID=:HYID";
            query.ParamByName("JE").AsFloat = pJE;
            query.ParamByName("HYID").AsInteger = pHYID;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into HYK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE,JYDJJE)";
                query.SQL.Add(" values(:HYID,:QCYE,:JE,0,0,0)");
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("QCYE").AsFloat = pCLLX == BASECRMDefine.CZK_CLLX_JK ? pJE : 0;
                query.ParamByName("JE").AsFloat = pJE;
                query.ExecSQL();
            }
            int tJYBH = SeqGenerator.GetSeq("HYK_JEZCLJL");
            query.SQL.Text = "insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,MDID,ZY,JFJE,DFJE,YE)";
            query.SQL.Add(" select :JYBH,HYID," + CyDbSystem.GetDbServerTimeFuncSql(query.Connection) + ",:CLLX,:JLBH,:MDID,:ZY,round(:JFJE,2),round(:DFJE,2),YE");
            query.SQL.Add(" from HYK_JEZH where HYID=:HYID");
            query.ParamByName("JYBH").AsInteger = tJYBH;
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("CLLX").AsInteger = pCLLX;
            query.ParamByName("JLBH").AsInteger = pJLBH;
            query.ParamByName("MDID").AsInteger = pMDID;
            query.ParamByName("ZY").AsString = pZY;
            query.ParamByName("JFJE").AsFloat = fJFJE;
            query.ParamByName("DFJE").AsFloat = fDFJE;
            query.ExecSQL();
            decimal balance;
            EncryptBalanceOfCashCard(query, pHYID, serverTime, out balance);
        }

        public static void UpdateMZKJEZH(out string msg, CyQuery query, int pHYID, int pCLLX, int pJLBH, int pMDID, double pJE, string pZY = "", string sHYK_NO = "", double pPDJE = 0)
        {
            msg = string.Empty;
            query.Close();
            query.SQL.Text = "";
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            query.Close();
            double fDFJE = 0, fJFJE = 0;
            switch (pCLLX)
            {
                case BASECRMDefine.CZK_CLLX_JK:
                case BASECRMDefine.CZK_CLLX_CK:
                    fJFJE = pJE; break;
                case BASECRMDefine.CZK_CLLX_QL:
                case BASECRMDefine.CZK_CLLX_QK:
                    fDFJE = pJE; break;
            }
            query.SQL.Text = "update MZK_JEZH set YE=YE+round(:JE,2) where HYID=:HYID";
            query.ParamByName("JE").AsFloat = pJE;
            query.ParamByName("HYID").AsInteger = pHYID;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into MZK_JEZH(HYID,QCYE,YE,YXTZJE,PDJE,JYDJJE)";
                query.SQL.Add(" values(:HYID,:QCYE,:JE,0,:PDJE,0)");
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("QCYE").AsFloat = pJE;
                query.ParamByName("JE").AsFloat = pJE;
                query.ParamByName("PDJE").AsFloat = pPDJE;
                query.ExecSQL();
            }
            int tJYBH = SeqGenerator.GetSeq("MZK_JEZCLJL");
            query.SQL.Text = "insert into MZK_JEZCLJL(JYBH,HYID,CLSJ,CLLX,JLBH,MDID,ZY,JFJE,DFJE,YE,HYK_NO)";
            query.SQL.Add(" select :JYBH,HYID," + CyDbSystem.GetDbServerTimeFuncSql(query.Connection) + ",:CLLX,:JLBH,:MDID,:ZY,round(:JFJE,2),round(:DFJE,2),YE,:HYK_NO");
            query.SQL.Add(" from MZK_JEZH where HYID=:HYID");
            query.ParamByName("JYBH").AsInteger = tJYBH;
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("CLLX").AsInteger = pCLLX;
            query.ParamByName("JLBH").AsInteger = pJLBH;
            query.ParamByName("MDID").AsInteger = pMDID;
            query.ParamByName("ZY").AsString = pZY;
            query.ParamByName("JFJE").AsFloat = fJFJE;
            query.ParamByName("DFJE").AsFloat = fDFJE;
            query.ParamByName("HYK_NO").AsString = sHYK_NO;
            query.ExecSQL();
            double balance;
            EncryptBalanceOfCashCard_MZK(query, pHYID, serverTime, out balance);
        }

        public static void UpdateJFZH(out string msg, CyQuery query, int pCLFS, int pHYID, int pCLLX, int pJLBH, int pMDID, double pBDJF, int pRYID, string pRYMC, string pZY = "", double pBQJFBD = 0, double pBNJFBD = 0, double pLJJFBD = 0, double pBDJE = 0)
        {
            //更新积分账户只允许调用本方法，如果不满足需求，则直接修改此方法，不要新加其他方法
            //CLFS处理方式
            //0单门店增减积分
            //2同商户门店分摊积分
            //3全部门店分摊积分
            //4管辖商户分摊，按传入的pMDID
            //5积分返利冲正(pMDID传的是原记录编号）（暂无）
            //升降级使用方式3，扣减BQJF或XFJE            
            msg = string.Empty;
            string sSHDM = string.Empty;
            double fZJF = 0, tBDJF = 0, fZJE = 0, tBDJE = 0, fZBQ = 0, tBDBQ = 0;
            //int tMDID = query.FieldByName("MDID").AsInteger;//tMDID为人员所属MDID，管辖商户分摊使用此MDID
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            int tJYBH = SeqGenerator.GetSeq("HYK_JFBDJLMX");
            UpdateJFZH_ZH(out msg, query, pHYID, pCLLX, pJLBH, pMDID, tJYBH, pBDJF, pBDJE, pRYID.ToString(), pRYMC, serverTime, pZY, pBQJFBD, pBNJFBD, pLJJFBD, true);
            //处理门店积分
            switch (pCLFS)
            {
                case 0:
                    UpdateJFZH_ZH(out msg, query, pHYID, pCLLX, pJLBH, pMDID, tJYBH, pBDJF, pBDJE, pRYID.ToString(), pRYMC, serverTime, pZY, pBQJFBD, pBNJFBD, pLJJFBD, false);
                    break;
                case 2:
                    {
                        query.SQL.Text = "select SHDM from MDDY where MDID=" + pMDID;
                        query.Open();
                        sSHDM = query.Fields[0].AsString;
                        query.SQL.Text = "select sum(WCLJF),sum(XFJE),sum(BQJF) from HYK_MDJF J,MDDY M";
                        query.SQL.Add("  where J.HYID=:HYID and M.SHDM='" + sSHDM + "' and J.MDID=M.MDID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                        query.Open();
                        fZJF = query.Fields[0].AsFloat;
                        fZJE = query.Fields[1].AsFloat;
                        fZBQ = query.Fields[2].AsFloat;
                        query.Close();
                        query.SQL.Text = "select WCLJF,XFJE,BQJF,J.MDID from HYK_MDJF J,MDDY M";
                        query.SQL.Add("   where HYID=:HYID and M.SHDM='" + sSHDM + "' and J.MDID=M.MDID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                    }
                    break;
                case 3:
                    {
                        query.SQL.Text = "select sum(WCLJF),sum(XFJE),sum(BQJF) from HYK_MDJF J";
                        query.SQL.Add("  where J.HYID=:HYID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                        query.Open();
                        fZJF = query.Fields[0].AsFloat;
                        fZJE = query.Fields[1].AsFloat;
                        fZBQ = query.Fields[2].AsFloat;
                        query.Close();
                        query.SQL.Text = "select WCLJF,XFJE,BQJF,J.MDID from HYK_MDJF J";
                        query.SQL.Add("   where HYID=:HYID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                    }
                    break;
                case 4:
                    {
                        query.SQL.Text = "select GXSHDM from MDDY where MDID=" + pMDID;
                        query.Open();
                        sSHDM = query.Fields[0].AsString;
                        query.SQL.Text = "select sum(WCLJF),sum(XFJE),sum(BQJF) from HYK_MDJF J,MDDY M";
                        query.SQL.Add("  where J.HYID=:HYID and M.GXSHDM='" + sSHDM + "' and J.MDID=M.MDID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                        query.Open();
                        fZJF = query.Fields[0].AsFloat;
                        fZJE = query.Fields[1].AsFloat;
                        fZBQ = query.Fields[2].AsFloat;
                        query.Close();
                        query.SQL.Text = "select WCLJF,XFJE,BQJF,J.MDID from HYK_MDJF J,MDDY M";
                        query.SQL.Add("   where HYID=:HYID and M.GXSHDM='" + sSHDM + "' and J.MDID=M.MDID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                    }
                    break;
                case 5:
                    {//
                        query.SQL.Text = "select sum(WCLJF),sum(XFJE),sum(BQJF) from HYK_MDJF J";
                        query.SQL.Add("  where J.HYID=:HYID and WCLJF>0");
                        query.ParamByName("HYID").AsInteger = pHYID;
                        query.Open();
                        fZJF = query.Fields[0].AsFloat;
                        fZJE = query.Fields[1].AsFloat;
                        fZBQ = query.Fields[2].AsFloat;
                        query.Close();///li                                            
                        query.SQL.Text = "select B.MDID,B.WCLJF,B.XFJE,B.BQJF from HYK_JFBDJLMX A,HYK_JFBDJLMX_MD B where  A.JYBH=B.JYBH  and A.HYID=:HYID and A.JLBH=:JLBH and A.CLLX=:CLLX ";
                        query.ParamByName("HYID").AsInteger = pHYID;
                        query.ParamByName("JLBH").AsInteger = pMDID;
                        query.ParamByName("CLLX").AsInteger = pCLLX;
                        query.Open();
                    }
                    break;
            }
            if (pCLFS > 1)
            {
                //分摊只支持WCLJF、BQJF、XFJE
                List<MDJF> lst = new List<MDJF>();
                query.Open();
                while (!query.Eof)
                {
                    MDJF one = new MDJF();
                    one.iMDID = query.FieldByName("MDID").AsInteger;
                    one.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    one.fXFJE = query.FieldByName("XFJE").AsFloat;
                    one.fBQJF = query.FieldByName("BQJF").AsFloat;
                    lst.Add(one);
                    query.Next();
                }
                query.Close();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (i < lst.Count - 1)
                    {
                        UpdateJFZH_ZH(out msg, query, pHYID, pCLLX, pJLBH, lst[i].iMDID, tJYBH,
                            fZJF == 0 ? 0 : Math.Round(pBDJF * lst[i].fWCLJF / fZJF, 4),
                            fZJE == 0 ? 0 : Math.Round(pBDJE * lst[i].fXFJE / fZJE, 4),
                            pRYID.ToString(), pRYMC, serverTime, pZY,
                            fZBQ == 0 ? 0 : Math.Round(pBQJFBD * lst[i].fBQJF / fZBQ, 4),
                            pBNJFBD, pLJJFBD, false);
                        tBDJF += (fZJF == 0 ? 0 : Math.Round(pBDJF * lst[i].fWCLJF / fZJF, 4));
                        tBDJE += (fZJE == 0 ? 0 : Math.Round(pBDJE * lst[i].fXFJE / fZJE, 4));
                        tBDBQ += (fZBQ == 0 ? 0 : Math.Round(pBQJFBD * lst[i].fBQJF / fZBQ, 4));
                    }
                    else
                        UpdateJFZH_ZH(out msg, query, pHYID, pCLLX, pJLBH, lst[i].iMDID, tJYBH,
                            pBDJF - tBDJF,
                            pBDJE - tBDJE,
                            pRYID.ToString(), pRYMC, serverTime, pZY,
                            pBQJFBD - tBDBQ,
                            pBNJFBD, pLJJFBD, false);
                }
            }
        }

        public static void UpdateJFZH_ZH(out string msg, CyQuery query, int pHYID, int pCLLX, int pJLBH, int pMDID, int pJYBH, double pBDJF, double pBDJE, string pRYDM, string pRYMC, DateTime serverTime, string pZY, double pBQJFBD, double pBNJFBD, double pLJJFBD, bool bMain)
        {
            msg = string.Empty;
            string sTblName = string.Empty;
            if (bMain)
                sTblName = "HYK_JFZH";
            else
                sTblName = "HYK_MDJF";
            query.Close();
            query.SQL.Text = "update " + sTblName + " set WCLJF=WCLJF+round(:BDJF,4),";
            query.SQL.Add(" XFJE=XFJE+round(:BDJE,4),");
            query.SQL.Add(" BQJF=BQJF+round(:BQJF,4),");
            query.SQL.Add(" BNLJJF=BNLJJF+round(:BNLJJF,4),");
            query.SQL.Add(" LJJF=LJJF+round(:LJJF,4)");
            query.SQL.Add(" where HYID=:HYID");
            if (!bMain)
                query.SQL.Add(" and MDID=" + pMDID);
            query.ParamByName("BDJF").AsFloat = pBDJF;
            query.ParamByName("BDJE").AsFloat = pBDJE;
            query.ParamByName("BQJF").AsFloat = pBQJFBD;
            query.ParamByName("BNLJJF").AsFloat = pBNJFBD;
            query.ParamByName("LJJF").AsFloat = pLJJFBD;
            query.ParamByName("HYID").AsInteger = pHYID;
            if (query.ExecSQL() == 0)
            {
                if (bMain)
                    query.SQL.Text = "insert into HYK_JFZH(HYID,WCLJF,BQJF,BNLJJF,LJJF,XFJE,ZKJE) values(:HYID,:BDJF,:BQJF,:BNLJJF,:LJJF,:BDJE,0)";
                else
                    query.SQL.Text = "insert into HYK_MDJF(HYID,MDID,WCLJF,BQJF,BNLJJF,LJJF,XFJE,ZKJE) values(:HYID," + pMDID + ",:BDJF,:BQJF,:BNLJJF,:LJJF,:BDJE,0)";
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("BDJF").AsFloat = pBDJF;
                query.ParamByName("BDJE").AsFloat = pBDJE;
                query.ParamByName("BQJF").AsFloat = pBQJFBD;
                query.ParamByName("BNLJJF").AsFloat = pBNJFBD;
                query.ParamByName("LJJF").AsFloat = pLJJFBD;
                query.ExecSQL();
            }
            if (bMain)
            {
                query.SQL.Text = "insert into HYK_JFBDJLMX(JYBH,CZMD,JLBH,HYID,CLSJ,CLLX,WCLJFBD,WCLJF,CZYDM,CZYMC,XFJE,XFJEBD,BQJF,BQJFBD,ZY)";
                query.SQL.Add(" select :JYBH,:CZMD,:JLBH,HYID,:CLSJ,:CLLX,:WCLJFBD,WCLJF,:CZYDM,:CZYMC,XFJE,:XFJEBD,BQJF,:BQJFBD,:ZY from HYK_JFZH where HYID=:HYID");
                query.ParamByName("JYBH").AsInteger = pJYBH;
                query.ParamByName("CZMD").AsInteger = pMDID;
                query.ParamByName("JLBH").AsInteger = pJLBH;
                query.ParamByName("CLSJ").AsDateTime = serverTime;
                query.ParamByName("CLLX").AsInteger = pCLLX;
                query.ParamByName("WCLJFBD").AsFloat = pBDJF;
                query.ParamByName("CZYDM").AsString = pRYDM;
                query.ParamByName("CZYMC").AsString = pRYMC;
                query.ParamByName("XFJEBD").AsFloat = pBDJE;
                query.ParamByName("BQJFBD").AsFloat = pBQJFBD;
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("ZY").AsString = pZY;
                query.ExecSQL();
            }
            else
            {
                query.SQL.Text = "insert into HYK_JFBDJLMX_MD(JYBH,MDID,WCLJFBD,WCLJF,XFJE,XFJEBD,BQJF,BQJFBD)";
                query.SQL.Add(" select :JYBH,MDID,:WCLJFBD,WCLJF,XFJE,:XFJEBD,BQJF,:BQJFBD from HYK_MDJF where HYID=:HYID and MDID=:MDID");
                query.ParamByName("JYBH").AsInteger = pJYBH;
                query.ParamByName("WCLJFBD").AsFloat = pBDJF;
                query.ParamByName("XFJEBD").AsFloat = pBDJE;
                query.ParamByName("BQJFBD").AsFloat = pBQJFBD;
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("MDID").AsInteger = pMDID;
                query.ExecSQL();
            }
        }

        public static void EncryptBalanceOfCashCard_MZK(CyQuery query, int pHYID, DateTime serverTime, out double balance)
        {
            balance = 0;
            double frozen = 0;
            byte[] bytesTM = new byte[8];
            string dbSysName = CyDbSystem.GetDbSystemName(query.Connection);
            switch (dbSysName)
            {
                case CyDbSystem.SybaseDbSystemName:
                    query.SQL.Text = "select YE,JYDJJE,convert(binary(8),TM) as TM from MZK_JEZH where HYID = " + pHYID;
                    break;
                case CyDbSystem.OracleDbSystemName:
                    query.SQL.Text = "select YE,JYDJJE,to_char(TM) as TM from MZK_JEZH where HYID =" + pHYID;
                    break;
            }
            query.Open();
            if (!query.IsEmpty)
            {
                balance = query.FieldByName("YE").AsFloat;
                frozen = query.FieldByName("JYDJJE").AsFloat;
                switch (dbSysName)
                {
                    case CyDbSystem.SybaseDbSystemName:
                        query.FieldByName("TM").GetBytes(0, bytesTM, 0, 8);
                        break;
                    case CyDbSystem.OracleDbSystemName:
                        string strTM = query.FieldByName("TM").AsString;
                        if ((strTM != null) && (strTM.Length > 0))
                        {
                            Int64 longTM = Int64.Parse(strTM);
                            for (int j = 0; j < 8; j++)
                            {
                                bytesTM[j] = (byte)(longTM >> (j * 8));
                            }
                        }
                        break;
                }
            }
            query.Close();

            byte[] newKey = EncryptUtils.DesEncrypt(BASECRMDefine.DesKey, bytesTM);
            byte[] bytesBalance = new byte[8];
            int intBalance = Convert.ToInt32(balance * 100);
            for (int j = 0; j < 4; j++)
            {
                bytesBalance[j] = (byte)(intBalance >> (j * 8));
            }
            for (int j = 4; j < 8; j++)
            {
                bytesBalance[j] = 0;
            }
            byte[] bytesDAC = EncryptUtils.DesEncrypt(newKey, bytesBalance);

            query.SQL.Text = "update MZK_YEBD set BDSJ=:BDSJ,DAC=:DAC where HYID = :HYID";
            query.ParamByName("BDSJ").AsDateTime = serverTime;
            query.ParamByName("DAC").SetParamValue(DbType.Binary, bytesDAC);
            query.ParamByName("HYID").AsInteger = pHYID;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into MZK_YEBD (HYID,BDSJ,DAC) values(:HYID,:BDSJ,:DAC)";
                query.ParamByName("BDSJ").AsDateTime = serverTime;
                query.ParamByName("DAC").SetParamValue(DbType.Binary, bytesDAC);
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ExecSQL();
            }
            balance += frozen;
        }

        public static void EncryptBalanceOfCashCard(CyQuery query, int pHYID, DateTime serverTime, out Decimal balance)
        {
            balance = 0;
            Decimal frozen = 0;
            byte[] bytesTM = new byte[8];
            string dbSysName = CyDbSystem.GetDbSystemName(query.Connection);
            switch (dbSysName)
            {
                case CyDbSystem.SybaseDbSystemName:
                    query.SQL.Text = "select YE,JYDJJE,convert(binary(8),TM) as TM from HYK_JEZH where HYID = " + pHYID;
                    break;
                case CyDbSystem.OracleDbSystemName:
                    query.SQL.Text = "select YE,JYDJJE,to_char(TM) as TM from HYK_JEZH where HYID =" + pHYID;
                    break;
            }
            query.Open();
            if (!query.IsEmpty)
            {
                balance = query.FieldByName("YE").AsDecimal;
                frozen = query.FieldByName("JYDJJE").AsDecimal;
                switch (dbSysName)
                {
                    case CyDbSystem.SybaseDbSystemName:
                        query.FieldByName("TM").GetBytes(0, bytesTM, 0, 8);
                        break;
                    case CyDbSystem.OracleDbSystemName:
                        string strTM = query.FieldByName("TM").AsString;
                        if ((strTM != null) && (strTM.Length > 0))
                        {
                            Int64 longTM = Int64.Parse(strTM);
                            for (int j = 0; j < 8; j++)
                            {
                                bytesTM[j] = (byte)(longTM >> (j * 8));
                            }
                        }
                        break;
                }
            }
            query.Close();

            byte[] newKey = EncryptUtils.DesEncrypt(BASECRMDefine.DesKey, bytesTM);
            byte[] bytesBalance = new byte[8];
            int intBalance = Convert.ToInt32(balance * 100);
            for (int j = 0; j < 4; j++)
            {
                bytesBalance[j] = (byte)(intBalance >> (j * 8));
            }
            for (int j = 4; j < 8; j++)
            {
                bytesBalance[j] = 0;
            }
            byte[] bytesDAC = EncryptUtils.DesEncrypt(newKey, bytesBalance);

            query.SQL.Text = "update HYK_YEBD set BDSJ=:BDSJ,DAC=:DAC where HYID = :HYID";
            query.ParamByName("BDSJ").AsDateTime = serverTime;
            query.ParamByName("DAC").SetParamValue(DbType.Binary, bytesDAC);
            query.ParamByName("HYID").AsInteger = pHYID;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into HYK_YEBD (HYID,BDSJ,DAC) values(:HYID,:BDSJ,:DAC)";
                query.ParamByName("BDSJ").AsDateTime = serverTime;
                query.ParamByName("DAC").SetParamValue(DbType.Binary, bytesDAC);
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ExecSQL();
            }
            balance += frozen;
        }

        public static void UpdateLPKC(out string msg, CyQuery query, int pLPID, int pCLLX, int pJLBH, string pBGDDDM, double pBDSL, string pCZYDM, string pCZYMC, string pZY = "", int pBJ_WKC = 0)
        {
            msg = string.Empty;
            if (pBJ_WKC == 0)  //未限制库存的礼品发放不需要写礼品库存表
            {
                query.SQL.Text = "update HYK_JFFHLPKC set KCSL=" + CyDbSystem.GetIsNullFuncName(query.Connection) + "(KCSL,0)+:BDSL Where LPID=:LPID and BGDDDM=:BGDDDM";
                query.ParamByName("LPID").AsInteger = pLPID;
                query.ParamByName("BDSL").AsFloat = pBDSL;
                query.ParamByName("BGDDDM").AsString = pBGDDDM;
                if (query.ExecSQL() == 0)
                {
                    query.SQL.Text = "insert into HYK_JFFHLPKC(LPID,KCSL,BGDDDM)";
                    query.SQL.Add(" values(:LPID,:BDSL,:BGDDDM)");
                    query.ParamByName("LPID").AsInteger = pLPID;
                    query.ParamByName("BDSL").AsFloat = pBDSL;
                    query.ParamByName("BGDDDM").AsString = pBGDDDM;
                    query.ExecSQL();
                }
            }
            int tJYBH = SeqGenerator.GetSeq("HYK_LPKCBDJL");
            query.SQL.Text = "insert into HYK_LPKCBDJL(JYBH,LPID,BGDDDM,JLBH,CLSJ,CLLX,BDSL,KCSL,CZYDM,CZYMC,ZY)";
            query.SQL.Add(" select :JYBH,LPID,BGDDDM,:JLBH,sysdate,:CLLX,:BDSL,KCSL,:CZYDM,:CZYMC,:ZY from HYK_JFFHLPKC");
            query.SQL.Add(" where LPID=:LPID and BGDDDM=:BGDDDM");
            query.ParamByName("JYBH").AsInteger = tJYBH;
            query.ParamByName("JLBH").AsInteger = pJLBH;
            query.ParamByName("CLLX").AsInteger = pCLLX;
            query.ParamByName("BDSL").AsFloat = pBDSL;
            query.ParamByName("CZYDM").AsString = pCZYDM;
            query.ParamByName("CZYMC").AsString = pCZYMC;
            query.ParamByName("ZY").AsString = pZY;
            query.ParamByName("LPID").AsInteger = pLPID;
            query.ParamByName("BGDDDM").AsString = pBGDDDM;
            query.ExecSQL();
        }

        public static void UpdateHYKSTATUS(CyQuery query, int pHYID, int pSTATUS)
        {
            query.Close();
            query.SQL.Text = "update HYK_HYXX set STATUS=:STATUS where HYID=:HYID";
            query.ParamByName("STATUS").AsInteger = pSTATUS;
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ExecSQL();
        }

        public static void UpdateMZKSTATUS(CyQuery query, int pHYID, int pSTATUS)
        {
            query.Close();
            query.SQL.Text = "update MZKXX set STATUS=:STATUS where HYID=:HYID";
            query.ParamByName("STATUS").AsInteger = pSTATUS;
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ExecSQL();
        }

        public static int GetHYID(CyQuery query, string sHYK_NO)
        {
            query.Close();
            query.SQL.Text = "select HYID from HYK_HYXX where HYK_NO=:HYK_NO";
            query.ParamByName("HYK_NO").AsString = sHYK_NO;
            query.Open();
            if (query.IsEmpty)
                return 0;
            else
            {
                int i = query.Fields[0].AsInteger;
                query.Close();
                return i;
            }
        }

        public static int GetMDIDByRY(int LoginRYID)
        {
            int MDID = 0;
            return MDID;
        }

        public static string GetMDByRYID(int LoginRYID)
        {
            MDDY md = new MDDY();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select M.* from RYXX R,MDDY M ";
                    query.SQL.Add("where  R.MDID=M.MDID ");
                    query.SQL.Add("and  R.PERSON_ID=" + LoginRYID);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        md.iMDID = query.FieldByName("MDID").AsInteger;
                        md.sMDMC = query.FieldByName("MDMC").AsString;
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

            return JsonConvert.SerializeObject(md);


        }

        public static int GetMDByHYID(int iHYID, string sDBConnect = "CRMDB")
        {
            MDDY md = new MDDY();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnect);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (sDBConnect == "CRMDB")
                    {
                        query.SQL.Text = "select MDID from HYK_HYXX ";
                        query.SQL.Add("where HYID=" + iHYID);
                        query.Open();
                    }
                    else if (sDBConnect == "CRMDBMZK")
                    {
                        query.SQL.Text = "select MDID from MZKXX ";
                        query.SQL.Add("where HYID=" + iHYID);
                        query.Open();
                    }
                    if (!query.IsEmpty)
                    {
                        md.iMDID = query.FieldByName("MDID").AsInteger;
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

            return md.iMDID;


        }

        public static string CheckBGDDQX(out string msg, string sBGDDDM, int iLoginRYID)
        {
            msg = "";
            CRMLIBASHX obj = new CRMLIBASHX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select A.* from HYK_BGDD A where BGDDDM=:BGDDDM ";
                    query.SQL.Add(" and (exists(select 1 from XTCZY_BGDDQX X where (A.BGDDDM like X.BGDDDM||'%' or X.BGDDDM like A.BGDDDM||'%') and X.PERSON_ID=:RYID)");
                    query.SQL.Add(" or exists(select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where (A.BGDDDM LIKE Q.BGDDDM||'%' or Q.BGDDDM LIKE A.BGDDDM||'%') and Q.ID=G.GROUPID and G.PERSON_ID=:RYID)");
                    query.SQL.Add(" or exists (select 1 from XTCZY_BGDDQX X where X.PERSON_ID=:RYID and X.BGDDDM=' ')");
                    query.SQL.Add(" or exists (select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where Q.ID=G.GROUPID and Q.BGDDDM=' ' and  G.PERSON_ID=:RYID))");
                    query.ParamByName("BGDDDM").AsString = sBGDDDM;
                    query.ParamByName("RYID").AsInteger = iLoginRYID;
                    query.Open();
                    //bValid = !query.IsEmpty;
                    if (!query.IsEmpty)
                    {
                        obj.iBJ_CHECK = true;
                    }

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
            return JsonConvert.SerializeObject(obj);
        }

        public static string CheckBK(out string msg, int iGKID, int iChild = 0)
        {
            msg = "";
            CRMLIBASHX obj = new CRMLIBASHX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (iChild == 0)
                    {
                        query.SQL.Text = "select HYID,HYK_NO,STATUS from HYK_HYXX where GKID=:GKID order by  status desc";
                    }
                    else
                    {
                        query.SQL.Text = " select count(*) from HYK_HYXX H,HYK_CHILD_JL L where H.HYID=L.HYID and GKID=:GKID and L.FXDW=1 ";
                    }
                    query.ParamByName("GKID").AsInteger = iGKID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iBJ_CHECK = true;
                        obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    }
                    query.Close();
                    if (obj.iHYID != 0)
                    {
                        query.SQL.Text = "SELECT * FROM HYK_JEZH where HYID=:HYID";
                        query.ParamByName("HYID").AsInteger = obj.iHYID;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            obj.fYE = query.FieldByName("YE").AsFloat;
                        }
                        query.Close();
                    }

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
            return JsonConvert.SerializeObject(obj);
        }

        public static double GetGXSHJF(int HYID, int LoginRYID, int iBJ_WCLJF, int iMDID = 0)
        {
            double GXSHJF;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (iMDID == 0)
                    {
                        query.SQL.Text = "select F.* from HYK_JFZH F  where 1=1";
                        query.SQL.Add("   and F.HYID=" + HYID + "");
                    }
                    else
                    {
                        query.SQL.Text = "select J.* from HYK_MDJF J ,MDDY M where J.MDID=M.MDID and M.GXSHDM =(select GXSHDM from MDDY where MDID=" + iMDID + " )";
                        query.SQL.Add("  and J.HYID=" + HYID + "");
                    }

                    query.Open();
                    if (!query.IsEmpty)
                    {
                        if (iBJ_WCLJF == 1)
                        {
                            GXSHJF = query.FieldByName("WCLJF").AsFloat;
                        }
                        else
                        {
                            GXSHJF = query.FieldByName("BNLJJF").AsFloat;
                        }
                    }
                    else
                    {
                        GXSHJF = 0;
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

            return GXSHJF;

        }

        public static double GetMDJF(int HYID, int MDID)
        {
            double MDJF;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select sum(J.WCLJF) WCLJF from HYK_MDJF J  where J.MDID=" + MDID + "";
                    query.SQL.Add("  and J.HYID=" + HYID + "");
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        MDJF = query.FieldByName("WCLJF").AsFloat;
                    }
                    else
                    {
                        MDJF = 0;
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
            return MDJF;
        }

        public static string GetZK(int HYID)
        {
            string HYK_NO;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * from HYK_CHILD_JL J  where j.status>=0 and J.HYID=" + HYID + "";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        HYK_NO = query.FieldByName("HYK_NO").AsString;
                    }
                    else
                    {
                        HYK_NO = "";
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
            return HYK_NO;
        }
        //public static double GetGXSHBQJF(int HYID)
        //{

        //    double GXSHBQJF;
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        try
        //        {
        //            query.SQL.Text = "select sum(BQJF) BQJF from  HYK_MDJF J, MDDY M where M.MDID=J.MDID and M.SHDM='BH'";
        //            query.SQL.Add("  and J.HYID=" + HYID + "");
        //            query.Open();
        //            if (!query.IsEmpty)
        //            {
        //                GXSHBQJF = query.FieldByName("BQJF").AsFloat;
        //            }
        //            else
        //            {
        //                GXSHBQJF = 0;
        //            }
        //            query.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is MyDbException)
        //                throw e;
        //            else
        //                throw new MyDbException(e.Message, query.SqlText);

        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return GXSHBQJF;

        //}

        public static bool CheckListCount(List<object> lst)
        {
            if (lst.Count >= 10000)
                Pub.Log4Net.WriteLog(LogLevel.INFO, "查询结果过多：" + lst[0].GetType().ToString());
            return (lst.Count >= 10000);
        }

        public static string MakePrivateNumber(string pNumber)
        {
            string ret = pNumber;
            //int i, j;
            if (pNumber.Length >= 8)
            {
                //i = (pNumber.Length - 4) / 2;
                //ret = pNumber.Substring(0, i) + "****" + pNumber.Substring(i + 4);
                ret = pNumber.Substring(0, pNumber.Length - 4) + "****";
            }
            return ret;
        }

        public static string GetCXDStatus(int istatus)
        {
            if (istatus == 0)
            {
                return "未审核!";
            }
            if (istatus == 1)
            {
                return "已审核!";
            }
            if (istatus == 2)
            {
                return "已启动!";
            }
            if (istatus == 3)
            {
                return "已终止!";
            }
            return "无状态!eg:" + istatus;
        }

        public static void SendDJToQ(CyQuery query, int pJLBH, int pDJLX, int pQD = 1)
        {
            //队列传输用，暂时无用
            //int tID = SeqGenerator.GetSeq("CXD_SENDQ");
            //string sTableName = string.Empty;
            //switch (pDJLX)
            //{
            //    case 1: sTableName = "HYKJFDYD"; break;
            //    case 2: sTableName = "HYKZKDYD"; break;
            //    case 3: sTableName = "HYKFQDYD"; break;
            //    case 4: sTableName = "HYKYQDYD"; break;
            //    case 5: sTableName = "CXMBJZDYD"; break;
            //}

            //query.Close();
            //query.SQL.Text = "insert into CXD_SENDQ(ID,JLBH,DJLX,BJ_QD,QYID)";
            //query.SQL.Add(" select :ID,JLBH,:DJLX,:BJ_QD,QYID from " + sTableName + " J,SHBM B where J.JLBH=:JLBH and J.SHBMID=B.SHBMID");
            //query.ParamByName("ID").AsInteger = tID;
            //query.ParamByName("JLBH").AsInteger = pJLBH;
            //query.ParamByName("DJLX").AsInteger = pDJLX;
            //query.ParamByName("BJ_QD").AsInteger = pQD;
            //query.ExecSQL();
        }

        //public static void Export(HttpResponse Resp, string dt)
        //{
        //    //Resp = HttpContext.Current.Response;
        //    Resp.Clear();
        //    Resp.Buffer = true;
        //    Resp.Charset = "UTF-8";//GB2312
        //    //attachment   参数表示作为附件下载，可以改成   online在线打开  
        //    //filename=*.xls   指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc .xls .txt .htm　
        //    Resp.AppendHeader("Content-Disposition", "attachment;filename=Export.txt");
        //    //Resp.Charset = "gb2312";
        //    //Resp.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        //    //thispage.Response.ContentEncoding = System.Text.Encoding.Default;//GB2312.GetEncoding("UTF-8");
        //    Resp.ContentEncoding = System.Text.Encoding.UTF8;
        //    Resp.HeaderEncoding = System.Text.Encoding.UTF8;
        //    Resp.ContentType = "text/plain";// application/ms-excel 
        //    //this.Page.EnableViewState = false;
        //    StringWriter tw = new StringWriter();//dataTableExportToText(dt);//
        //    tw.Write(dt);
        //    //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        //    //ctl.RenderControl(hw);
        //    Resp.Write(tw.ToString());
        //    //Resp.End();//不能放在try catch 内！
        //}

        //public static StringWriter dataTableExportToText(DataTable dt)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    StringWriter streamWrite = new StringWriter();
        //    try
        //    {
        //        // 创建文件
        //        if (dt != null)
        //        {
        //            // 写数据
        //            string dataLine = null;
        //            string value = null;
        //            int columnCount = dt.Columns.Count;
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                for (int j = 0; j < columnCount; j++)
        //                {
        //                    value = dr[j].ToString().Trim();
        //                    if (value == null || value == string.Empty)
        //                    {
        //                        value = "?";
        //                    }
        //                    sb.Append(value + ",");
        //                }
        //                dataLine = sb.ToString().Trim();
        //                // 按行写入数据
        //                streamWrite.WriteLine(dataLine.Substring(0, dataLine.Length - 1));
        //                sb.Remove(0, sb.Length);
        //            }
        //        }
        //    }
        //    catch (IOException ioEx)
        //    {
        //        throw ioEx;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        streamWrite.Close();
        //    }
        //    return streamWrite;
        //}

        //public static void WriteToLog(string e)
        //{
        //    DateTime timeEnd = DateTime.Now;
        //    DateTime timeBegin = DateTime.Now;
        //    StringBuilder logStr = new System.Text.StringBuilder();
        //    logStr.Append("\r\n").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(" Response error, ").Append(timeEnd.Subtract(timeBegin).Milliseconds).Append(" ms:");
        //    logStr.Append("\r\n-->").Append(e);

        //    DailyLogFileWriter DataLogFileWriter = null;
        //    string logPath = System.Configuration.ConfigurationManager.AppSettings["LogPath"].ToString();// "F:\\ZY_web\\ljbb\\WEBErrorMsg";
        //    DataLogFileWriter = new DailyLogFileWriter(logPath, "CrmProcLog");
        //    DataLogFileWriter.Write(timeBegin.ToString("yyyy-MM-dd"), logStr.ToString());
        //    DataLogFileWriter.Close();
        //    //throw e;
        //}

        public static string GetMZKList(out string msg, HYXX_Detail obj, int BJ_TK = 0)
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (BJ_TK == 0)
                    {
                        query.SQL.Text = "select * from HYK_HYXX H,HYKDEF D,CZK_KHDA K where H.HYKTYPE=D.HYKTYPE and D.BJ_CZK=1 and H.KHID=K.KHID(+) ";
                    }
                    if (BJ_TK == 1)
                    {
                        query.SQL.Text = "  select H.HYK_NO,H.HYID,nvl(J.QCYE,0) QCYE,nvl(J.YE,0) YE ,YXQ,H.STATUS,D.HYKNAME from HYK_HYXX H,HYKDEF D,HYK_JEZH J,CZK_KHDA K";
                        query.SQL.Add("   WHERE H.HYKTYPE=D.HYKTYPE and H.HYID>0 and H.STATUS<>-1 AND   H.HYID=J.HYID(+)   and H.KHID=K.KHID(+) and D.TKBJ=1 and H.STATUS>=0");
                    }

                    if (obj.iHYID != 0)
                    {
                        query.SQL.Add("   and H.HYID=" + obj.iHYID);
                    }
                    if (obj.sHYK_NO != "")
                    {
                        query.SQL.Add("   and H.HYK_NO='" + obj.sHYK_NO + "'");
                    }
                    if (obj.iHYKTYPE != 0)
                    {
                        query.SQL.Add("   and H.HYKTYPE=" + obj.iHYKTYPE);
                    }
                    if (obj.dYXQ != "")
                    {
                        query.SQL.Add(" and H.YXQ='" + obj.dYXQ + "'");
                    }
                    if (obj.dJKRQ != "")
                    {
                        query.SQL.Add(" and trunc(H.JKRQ)='" + obj.dJKRQ + "'");
                    }
                    if (obj.sHY_NAME != "")
                    {
                        query.SQL.Add(" and K.KHMC='" + obj.sHY_NAME + "'");
                    }
                    if (obj.sSFZBH != "")
                    {
                        query.SQL.Add(" and K.SFZBH='" + obj.sSFZBH + "'");
                    }
                    if (obj.sSJHM != "")
                    {
                        query.SQL.Add(" and K.SJHM='" + obj.sSJHM + "'");
                    }
                    if (obj.iDJR != 0)
                    {
                        query.SQL.Add(" and K.DJR=" + obj.iDJR);
                    }
                    if (obj.sHYKNO_Begin != "")
                    {
                        query.SQL.Add(" and H.HYK_NO>='" + obj.sHYKNO_Begin + "'");
                    }
                    if (obj.sHYKNO_End != "")
                    {
                        query.SQL.Add(" and H.HYK_NO<='" + obj.sHYKNO_End + "'");
                    }
                    if (obj.iSL != 0)
                    {
                        query.SQL.Add(" and rownum<=" + obj.iSL);
                    }
                    //if (obj.fQCYE != 0) {
                    //    query.SQL.Add(" and ");
                    //}
                    if (obj.iLoginRYID != 0)
                    {//人员卡类型权限
                        //query.SQL.Add("and exists(select * from XTCZY_HYLXQX A where A.HYKTYPE=D.HYKTYPE and A.PERSON_ID="+obj.iLoginRYID+")");
                    }

                    query.Open();
                    while (!query.Eof)
                    {
                        if (CrmLibProc.IsValidHHC(null, query.FieldByName("HYID").AsInteger) == true)
                        {
                            HYXX_Detail item = new HYXX_Detail();
                            item.iHYID = query.FieldByName("HYID").AsInteger;
                            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                            item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                            if (BJ_TK == 0)
                            {
                                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                                item.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
                            }
                            if (BJ_TK == 1)
                            {
                                item.fQCYE = query.FieldByName("QCYE").AsFloat;
                                item.fYE = query.FieldByName("YE").AsFloat;
                                item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                            }
                            lst.Add(item);
                        }
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
        //20140108 WHY --------MZKSKD
        public static string GetMZKSKDList(out string msg, MZKSKD_Detail obj)
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    if (obj.iBJ_TS == 1)
                    {
                        query.SQL.Text = " select L.JLBH,L.SKSL,L.SSJE,L.YSZE,L.ZY,L.DJRMC,L.DJSJ,L.ZXRMC,L.ZXRQ,A.KHMC,A.KHID,I.YXQTS, I.YHQLX,Y.YHQMC,L.SJZSJF,L.SJZSJE";
                        query.SQL.Add("  from  MZK_SKJL L,MZK_KHDA A, MZK_SKJLZKITEM I,YHQDEF Y");
                        query.SQL.Add("  where L.KHID=A.KHID(+) and L.JLBH=I.JLBH(+) and I.YHQLX=Y.Yhqid(+) AND L.FS=1");

                    }
                    else
                    {

                        query.SQL.Text = "select L.JLBH,L.SKSL,L.SSJE,L.ZY,L.DJRMC,L.DJSJ,L.ZXRMC,L.ZXRQ,A.KHMC,A.KHID ";
                        if (obj.iBJ_QKGX != 0)
                        {
                            query.SQL.Add(" ,(SELECT sum(C.JE) FROM  MZK_SKJLSKMX  C ,ZFFS Z where C.JLBH=L.JLBH  and  C.ZFFSID=Z.ZFFSID and Z.TYPE=10) DHJE ");
                        }
                        query.SQL.Add(" from MZK_SKJL L,MZK_KHDA A");
                        query.SQL.Add(" where  A.KHID=L.KHID and L.FS=1 ");
                    }
                    if (obj.iJLBH != 0)
                    {
                        query.SQL.Add("   and L.JLBH=" + obj.iJLBH);
                    }
                    if (obj.sMZKHH != "")
                    {
                        query.SQL.Add("  and L.JLBH in(select JLBH from  MZK_SKJLITEM  WHERE CZKHM=" + obj.sMZKHH + " )  ");
                    }

                    if (obj.iSTATUS != 0)
                    {
                        query.SQL.Add("   and L.STATUS=" + obj.iSTATUS);
                    }


                    if (obj.iDJR != 0)
                    {
                        query.SQL.Add(" and L.DJR=" + obj.iDJR);
                    }
                    if (obj.iBJ_QKGX != 0)
                    {
                        query.SQL.Add("  and L.JLBH in(SELECT JLBH FROM  MZK_SKJLSKMX  C ,ZFFS Z where C.ZFFSID=Z.ZFFSID and Z.TYPE=10)  ");
                    }

                    query.Open();
                    while (!query.Eof)
                    {
                        MZKSKD_Detail item = new MZKSKD_Detail();
                        item.iJLBH = query.FieldByName("JLBH").AsInteger;
                        item.iSKSL = query.FieldByName("SKSL").AsInteger;
                        item.cSSJE = query.FieldByName("YSZE").AsCurrency;
                        item.sZY = query.FieldByName("ZY").AsString;
                        item.sDJRMC = query.FieldByName("DJRMC").AsString;
                        item.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        item.sKHMC = query.FieldByName("KHMC").AsString;
                        item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        item.iKHID = query.FieldByName("KHID").AsInteger;
                        if (obj.iBJ_QKGX != 0)
                        {
                            item.fDHJE = query.FieldByName("DHJE").AsFloat;
                        }
                        if (obj.iBJ_TS == 1)
                        {
                            item.iYHQID = query.FieldByName("YHQLX").AsInteger;
                            item.sYHQMC = query.FieldByName("YHQMC").AsString;
                            item.fSJZSJE = query.FieldByName("SJZSJE").AsFloat;
                            item.fSJZSJF = query.FieldByName("SJZSJF").AsFloat;
                            item.iYXQTS = query.FieldByName("YXQTS").AsInteger;

                        }
                        //  item.iDKHLX = query.FieldByName("DKHLX").AsInteger;
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

        public static string GetCZK_KHDAList(out string msg, CZK_KHDA obj)
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select A.KHID,A.KHDM,A.KHMC,A.DKHLX ";
                    query.SQL.Add(" from CZK_KHDA A");

                    if (obj.iKHID != 0)
                    {
                        query.SQL.Add("   and A.KHID=" + obj.iKHID);
                    }
                    if (obj.sKHDM != "")
                    {
                        query.SQL.Add("   and A.KHDM='" + obj.sKHDM + "'");
                    }
                    if (obj.sKHMC != "")
                    {
                        query.SQL.Add(" and A.KHMC='" + obj.sKHMC + "'");
                    }


                    query.Open();
                    while (!query.Eof)
                    {
                        CZK_KHDA item = new CZK_KHDA();
                        item.iKHID = query.FieldByName("KHID").AsInteger;
                        item.sKHDM = query.FieldByName("KHDM").AsString;
                        item.sKHMC = query.FieldByName("KHMC").AsString;
                        item.iKHLX = query.FieldByName("DKHLX").AsInteger;
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

        public static string GetMZKKCKKD(out string msg, CyQuery query, CRMLIBASHX param)// string sBGDDDM, string sCZKHM_BEGIN, string sCZKHM_END, int iHYKTYPE, int iSTATUS, string sDBConnName = "CRMDBMZK", int iBJ_KCK = 1)
        {
            //获取库存卡卡段，必须传入obj.sBGDDDM、iHYKTYPE、iSTATUS、sCZKHM_Begin、sCZKHM_End
            msg = string.Empty;
            List<object> lst = new List<object>();
            List<object> kdlst = new List<object>();
            KCKXX obj = new KCKXX();
            if (param.iBJ_KCK == 0)
            {
                query.SQL.Text = "select H.*,D.HYKNAME,D.KHQDM,D.KHHZM from MZKXX H,HYKDEF D where H.HYKTYPE=D.HYKTYPE ";
                query.SQL.Add(" and H.HYK_NO>='" + param.sCZKHM_BEGIN + "' ");
                query.SQL.Add(" and H.HYK_NO<='" + param.sCZKHM_END + "' ");
                query.SQL.Add(" and H.STATUS>=" + param.iSTATUS);
            }
            else
            {
                query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC,D.KHQDM,D.KHHZM from MZKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                query.SQL.Add(" and H.BGDDDM='" + param.sBGDDDM + "'");
                query.SQL.Add(" and H.CZKHM>='" + param.sCZKHM_BEGIN + "' ");
                query.SQL.Add(" and H.CZKHM<='" + param.sCZKHM_END + "' ");
                query.SQL.Add(" and H.STATUS=" + param.iSTATUS);
            }

            query.SQL.Add(" and H.HYKTYPE=" + param.iHYKTYPE);

            if (param.iBJ_KCK == 0)
                query.SQL.Add("  order by HYK_NO");
            else
            {
                query.SQL.Add("order by CZKHM ");
            }
            query.Open();
            if (query.IsEmpty)
            {
                msg = CrmLibStrings.msgKCKNotFound;
                return msg;
            }
            while (!query.Eof)
            {
                KCKXX item = new KCKXX();

                //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                //item.iSL = query.FieldByName("SL").AsInteger;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                if (param.iBJ_KCK == 1)
                {
                    item.sCZKHM = query.FieldByName("CZKHM").AsString;
                    item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                    item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                    item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                    item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                    item.fQCYE = query.FieldByName("QCYE").AsFloat;
                    item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                    item.fPDJE = query.FieldByName("PDJE").AsFloat;
                }
                else
                {
                    item.sCZKHM = query.FieldByName("HYK_NO").AsString;
                }

                item.sKHQDM = query.FieldByName("KHQDM").AsString;
                item.sKHHZM = query.FieldByName("KHHZM").AsString;
                lst.Add(item);
                query.Next();
            }
            query.Close();
            //if (lst.Count > 0)
            //    kdlst.Add(new KCKHD());
            foreach (KCKXX item in lst)
            {
                //                        public int iHYKTYPE = 0;
                //public string sHYKNAME = string.Empty;
                //public double fQCYE = 0;
                //public int iSL = 0;

                KCKHD item2 = new KCKHD();
                if (kdlst.Count == 0)
                {
                    item2.sCZKHM_BEGIN = item.sCZKHM;
                    item2.sCZKHM_END = item.sCZKHM;
                    item2.iHYKTYPE = item.iHYKTYPE;
                    item2.sHYKNAME = item.sHYKNAME;
                    item2.fMZJE = item.fQCYE;
                    item2.iSKSL++;
                    kdlst.Add(item2);
                }
                else if (((KCKHD)kdlst[kdlst.Count - 1]).iHYKTYPE == item.iHYKTYPE
                    && ((KCKHD)kdlst[kdlst.Count - 1]).fMZJE == item.fQCYE
                    && Convert.ToInt64(((KCKHD)kdlst[kdlst.Count - 1]).sCZKHM_END.Substring(0, ((KCKHD)kdlst[kdlst.Count - 1]).sCZKHM_END.Length - item.sKHHZM.Length)) + 1 == Convert.ToInt64(item.sCZKHM.Substring(0, item.sCZKHM.Length - item.sKHHZM.Length)))
                {
                    ((KCKHD)kdlst[kdlst.Count - 1]).sCZKHM_END = item.sCZKHM;
                    ((KCKHD)kdlst[kdlst.Count - 1]).iSKSL++;
                }
                else
                {
                    item2.sCZKHM_BEGIN = item.sCZKHM;
                    item2.sCZKHM_END = item.sCZKHM;
                    item2.iHYKTYPE = item.iHYKTYPE;
                    item2.sHYKNAME = item.sHYKNAME;
                    item2.fMZJE = item.fQCYE;
                    item2.iSKSL++;
                    kdlst.Add(item2);
                }
            }
            return JsonConvert.SerializeObject(kdlst);
        }

        //public static string GetMZKKCKKD3(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        //{

        //    msg = string.Empty;
        //    List<KCKXX> lst = new List<KCKXX>();
        //    List<Object> kdlst = new List<Object>();
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        try
        //        {
        //            query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC,D.KHQDM,D.KHHZM,R.PERSON_NAME from MZKCARD H,HYKDEF D,HYK_BGDD B, RYXX R where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM and H.BGR=R.PERSON_ID";
        //            query.SQL.Add(" and H.BGDDDM='" + obj.sBGDDDM + "'");
        //            query.SQL.Add(" and H.HYKTYPE=" + obj.iHYKTYPE);
        //            query.SQL.Add(" and H.BGR=" + obj.iBGR);
        //            query.SQL.Add("order by CZKHM ");
        //            query.Open();
        //            if (query.IsEmpty)
        //            {
        //                msg = CrmLibStrings.msgKCKNotFound;
        //                return msg;
        //            }
        //            while (!query.Eof)
        //            {
        //                KCKXX item = new KCKXX();
        //                item.sCZKHM = query.FieldByName("CZKHM").AsString;

        //                //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
        //                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
        //                //item.iSL = query.FieldByName("SL").AsInteger;
        //                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
        //                item.iSTATUS = query.FieldByName("STATUS").AsInteger;
        //                item.fQCYE = query.FieldByName("QCYE").AsFloat;
        //                item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
        //                item.fPDJE = query.FieldByName("PDJE").AsFloat;
        //                item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
        //                item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
        //                item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
        //                item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
        //                item.sKHQDM = query.FieldByName("KHQDM").AsString;
        //                item.sKHHZM = query.FieldByName("KHHZM").AsString;
        //                item.iBGR = query.FieldByName("BGR").AsInteger;
        //                item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
        //                lst.Add(item);
        //                query.Next();
        //            }
        //            query.Close();
        //            KCKHD item2 = new KCKHD();
        //            item2.sBGRMC = lst[0].sBGRMC;
        //            item2.iBGR = obj.iBGR;
        //            item2.sHM_MIN = lst[0].sCZKHM;
        //            item2.sHM_MAX = lst[0].sCZKHM;
        //            item2.iHYKTYPE = obj.iHYKTYPE;
        //            item2.sHYKNAME = lst[0].sHYKNAME;
        //            item2.fJE = obj.fQCYE;
        //            item2.iSL = lst.Count;
        //            kdlst.Add(item2);



        //        }
        //        catch (Exception e)
        //        {
        //            if (e is MyDbException)
        //                throw e;
        //            else
        //                msg = e.Message;
        //            throw new MyDbException(e.Message, query.SqlText);

        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return obj.GetTableJson(kdlst);
        //}

        public static string GetMZKKCKKD_FS(out string msg, string sBGDDDM, string sCZKHM_BEGIN, string sCZKHM_END, int iHYKTYPE, int iSTATUS, string sDBConnName = "CRMDBMZK")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            MZKGL.MZKGL_MZKFS obj = new MZKGL.MZKGL_MZKFS();
            MZKGL.MZKGL_MZKFS.findkd kdlst = new MZKGL.MZKGL_MZKFS.findkd();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {

                    query.SQL.Text = "select H.*,D.HYKNAME,B.BGDDMC,D.KHQDM,D.KHHZM from MZKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                    query.SQL.Add(" and H.BGDDDM='" + sBGDDDM + "'");
                    query.SQL.Add(" and H.CZKHM>='" + sCZKHM_BEGIN + "' ");
                    query.SQL.Add(" and H.CZKHM<='" + sCZKHM_END + "' ");
                    query.SQL.Add(" and H.STATUS=" + iSTATUS);
                    query.SQL.Add(" and H.HYKTYPE=" + iHYKTYPE);
                    query.SQL.Add("order by CZKHM ");
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        //item.iSL = query.FieldByName("SL").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                        item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                        item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                        item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                        item.fQCYE = query.FieldByName("QCYE").AsFloat;
                        item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                        item.fPDJE = query.FieldByName("PDJE").AsFloat;
                        item.sKHQDM = query.FieldByName("KHQDM").AsString;
                        item.sKHHZM = query.FieldByName("KHHZM").AsString;
                        lst.Add(item);
                        query.Next();
                    }
                    query.Close();
                    foreach (KCKXX item in lst)
                    {
                        MZKGL.MZKGL_MZKFS.kdmx item2 = new MZKGL.MZKGL_MZKFS.kdmx();
                        if (obj.SKKD.Count == 0)
                        {
                            item2.sCZKHM_BEGIN = item.sCZKHM;
                            item2.sCZKHM_END = item.sCZKHM;
                            item2.iHYKTYPE = item.iHYKTYPE.ToString();
                            item2.sHYKNAME = item.sHYKNAME;
                            item2.fMZJE = Convert.ToDecimal(item.fQCYE);
                            item2.iSKSL++;
                            obj.SKKD.Add(item2);
                        }
                        else if ((obj.SKKD[obj.SKKD.Count - 1]).iHYKTYPE == item.iHYKTYPE.ToString()
                            && (obj.SKKD[obj.SKKD.Count - 1]).fMZJE == Convert.ToDecimal(item.fQCYE)
                            && Convert.ToInt64((obj.SKKD[obj.SKKD.Count - 1]).sCZKHM_END.Substring(0, (obj.SKKD[obj.SKKD.Count - 1]).sCZKHM_END.Length - item.sKHHZM.Length)) + 1 == Convert.ToInt64(item.sCZKHM.Substring(0, item.sCZKHM.Length - item.sKHHZM.Length)))
                        {
                            (obj.SKKD[obj.SKKD.Count - 1]).sCZKHM_END = item.sCZKHM;
                            (obj.SKKD[obj.SKKD.Count - 1]).iSKSL++;
                        }
                        else
                        {
                            item2.sCZKHM_BEGIN = item.sCZKHM;
                            item2.sCZKHM_END = item.sCZKHM;
                            item2.iHYKTYPE = item.iHYKTYPE.ToString();
                            item2.sHYKNAME = item.sHYKNAME;
                            item2.fMZJE = Convert.ToDecimal(item.fQCYE);
                            item2.iSKSL++;
                            obj.SKKD.Add(item2);
                        }
                    }
                    query.Close();
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
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetMZKKCKKH0(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select DISTINCT(P.CZKHM),C.BGR,R.PERSON_NAME, P.MZJE from  MZK_KCKPDPCITEM P,RYXX R,MZK_KCKPDPCKDITEM C,MZK_KCKPDLR R";
                    query.SQL.Add("  where P.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDLRITEM P ,MZK_KCKPDLR R WHERE P.JLBH=R.JLBH AND R.JLBH_PC=" + obj.iPDPC + ")");
                    query.SQL.Add(" and C.BGR=R.PERSON_ID ");
                    query.SQL.Add(" and C.BGR=R.BGR ");
                    query.SQL.Add(" and P.JLBH=C.JLBH");
                    query.SQL.Add(" and P.JLBH=" + obj.iPDPC);
                    query.SQL.Add("order by P.CZKHM ");
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
                        item.iBGR = query.FieldByName("BGR").AsInteger;
                        item.fMZJE = query.FieldByName("MZJE").AsFloat;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }


        public static string GetMZKKCKKH1(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P";
                    query.SQL.Add("  where L.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);
                    query.SQL.Add("order by L.CZKHM ");
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
                        item.iBGR = query.FieldByName("BGR").AsInteger;
                        item.fMZJE = query.FieldByName("MZJE").AsFloat;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }

        public static string GetMZKKCKKH2(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select DISTINCT(P.CZKHM),C.BGR,R.PERSON_NAME, P.MZJE,0 as BJ_SY,'损' as BJMC from  MZK_KCKPDPCITEM P,RYXX R,MZK_KCKPDPCKDITEM C,MZK_KCKPDLR R";
                    query.SQL.Add("  where P.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDLRITEM P ,MZK_KCKPDLR R WHERE P.JLBH=R.JLBH AND R.JLBH_PC=" + obj.iPDPC + ")");
                    query.SQL.Add(" and C.BGR=R.PERSON_ID ");
                    query.SQL.Add(" and C.BGR=R.BGR ");
                    query.SQL.Add(" and P.JLBH=C.JLBH");
                    query.SQL.Add(" and P.JLBH=" + obj.iPDPC);
                    query.SQL.Add(" union");

                    query.SQL.Add("  select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE,1 as BJ_SY,'溢' as BJMC from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P");
                    query.SQL.Add("  where L.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);


                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
                        item.iBGR = query.FieldByName("BGR").AsInteger;
                        item.fMZJE = query.FieldByName("MZJE").AsFloat;
                        item.iBJ = query.FieldByName("BJ_SY").AsInteger;
                        item.sBJMC = query.FieldByName("BJMC").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static string GetMZKKCKKH4(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select DISTINCT(P.CZKHM),C.BGR,R.PERSON_NAME, P.MZJE,0 as BJ_SY,'损' as BJMC from  MZK_KCKPDPCITEM P,RYXX R,MZK_KCKPDPCKDITEM C,MZK_KCKPDLR R";
                    query.SQL.Add("  where P.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDLRITEM P ,MZK_KCKPDLR R WHERE P.JLBH=R.JLBH AND R.JLBH_PC=" + obj.iPDPC + ")");
                    query.SQL.Add(" and C.BGR=R.PERSON_ID ");
                    query.SQL.Add(" and C.BGR=R.BGR ");
                    query.SQL.Add(" and P.JLBH=C.JLBH");
                    query.SQL.Add(" and P.JLBH=" + obj.iPDPC);
                    query.SQL.Add(" union");

                    query.SQL.Add("  select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE,1 as BJ_SY,'溢' as BJMC from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P");
                    query.SQL.Add("  where L.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);
                    query.SQL.Add(" union");

                    query.SQL.Add("  select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE,2 as BJ_SY,'正常' as BJMC from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P");
                    query.SQL.Add("  where L.CZKHM  in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);


                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
                        item.iBGR = query.FieldByName("BGR").AsInteger;
                        item.fMZJE = query.FieldByName("MZJE").AsFloat;
                        item.iBJ = query.FieldByName("BJ_SY").AsInteger;
                        item.sBJMC = query.FieldByName("BJMC").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static string GetMZKKCKKHNEW(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select DISTINCT(P.CZKHM),C.BGR,R.PERSON_NAME, P.MZJE,0 as BJ_SY,'损' as BJMC from  MZK_KCKPDPCITEM P,RYXX R,MZK_KCKPDPCKDITEM C,MZK_KCKPDLR R";
                    query.SQL.Add("  where P.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDLRITEM P ,MZK_KCKPDLR R WHERE P.JLBH=R.JLBH AND R.JLBH_PC=" + obj.iPDPC + ")");
                    query.SQL.Add(" and C.BGR=R.PERSON_ID ");
                    query.SQL.Add(" and C.BGR=R.BGR ");
                    query.SQL.Add(" and P.JLBH=C.JLBH");
                    query.SQL.Add(" and P.JLBH=" + obj.iPDPC);
                    query.SQL.Add(" union");

                    query.SQL.Add("  select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE,1 as BJ_SY,'溢' as BJMC from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P");
                    query.SQL.Add("  where L.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);
                    query.SQL.Add(" union");

                    query.SQL.Add("  select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE,2 as BJ_SY,'正常' as BJMC from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P");
                    query.SQL.Add("  where L.CZKHM  in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);


                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
                        item.iBGR = query.FieldByName("BGR").AsInteger;
                        item.fMZJE = query.FieldByName("MZJE").AsFloat;
                        item.iBJ = query.FieldByName("BJ_SY").AsInteger;
                        item.sBJMC = query.FieldByName("BJMC").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static string GetMZKKCKKHSY(out string msg, KCKXX obj, string sDBConnName = "CRMDBMZK")
        {

            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select DISTINCT(P.CZKHM),C.BGR,R.PERSON_NAME, P.MZJE,0 as BJ_SY,'损' as BJMC from  MZK_KCKPDPCITEM P,RYXX R,MZK_KCKPDPCKDITEM C,MZK_KCKPDLR R";
                    query.SQL.Add("  where P.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDLRITEM P,MZK_KCKPDLR R WHERE P.JLBH=R.JLBH AND R.JLBH_PC=" + obj.iPDPC + ")");
                    query.SQL.Add(" and C.BGR=R.PERSON_ID ");
                    query.SQL.Add(" and C.BGR=R.BGR ");
                    query.SQL.Add(" and P.JLBH=C.JLBH");
                    query.SQL.Add(" and P.JLBH=" + obj.iPDPC);
                    query.SQL.Add(" union");

                    query.SQL.Add("  select DISTINCT(L.CZKHM),K.BGR,R.PERSON_NAME, M.MZJE,1 as BJ_SY,'溢' as BJMC from RYXX R,MZK_KCKPDLRITEM L,MZK_KCKPDLRKDITEM M,MZK_KCKPDLR K,MZK_KCKPDPCITEM P");
                    query.SQL.Add("  where L.CZKHM not in");
                    query.SQL.Add("(select P.CZKHM from MZK_KCKPDPCITEM P where jlbh=" + obj.iPDPC + ")");
                    query.SQL.Add(" and K.BGR=R.PERSON_ID");
                    query.SQL.Add(" and K.BGR=P.BGR");
                    query.SQL.Add(" and K.JLBH=M.JLBH");
                    query.SQL.Add(" and K.JLBH=L.JLBH");
                    query.SQL.Add(" and K.JLBH_PC=" + obj.iPDPC);


                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = CrmLibStrings.msgKCKNotFound;
                        return msg;
                    }
                    while (!query.Eof)
                    {
                        KCKXX item = new KCKXX();
                        item.sCZKHM = query.FieldByName("CZKHM").AsString;
                        item.sBGRMC = query.FieldByName("PERSON_NAME").AsString;
                        item.iBGR = query.FieldByName("BGR").AsInteger;
                        item.fMZJE = query.FieldByName("MZJE").AsFloat;
                        item.iBJ = query.FieldByName("BJ_SY").AsInteger;
                        item.sBJMC = query.FieldByName("BJMC").AsString;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }


        public static string GetMZKXXList(out string msg, HYXX_Detail obj)
        {
            //获取面值卡，必须传入iHYKTYPE，可以传入sHYKNO_Begin、sHYKNO_End、iSL、iSTATUS(默认>=0)
            msg = string.Empty;
            List<object> lst = new List<object>();
            //DbConnection conn = CyDbConnManager.GetActiveDbConnection(obj.sDBConnName);
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string tp_sql = "";
                    tp_sql += " select ";
                    tp_sql += " H.* ";
                    tp_sql += " ,D.HYKNAME,D.CDJZ ";
                    tp_sql += " ,E.QCYE,E.YE ";
                    tp_sql += " ,A.MDMC,B.FXDWMC ";
                    tp_sql += " from MZKXX H";
                    tp_sql += " ,HYKDEF D,MZK_JEZH E";
                    tp_sql += " ,MDDY A,FXDWDEF B ";
                    tp_sql += " where H.HYKTYPE=D.HYKTYPE";
                    tp_sql += " and H.HYID=E.HYID(+)";
                    tp_sql += " and H.MDID=A.MDID  and H.FXDW=B.FXDWID(+)";
                    query.SQL.Text = tp_sql;
                    if (obj.iHYID != 0)
                    {
                        query.SQL.Add("   and H.HYID=" + obj.iHYID);
                    }
                    if (obj.iHYKTYPE != 0)
                    {
                        query.SQL.Add("   and H.HYKTYPE=" + obj.iHYKTYPE);
                    }
                    if (obj.iSTATUS != 0)
                    {
                        query.SQL.Add("   and H.STATUS=" + obj.iSTATUS);
                    }
                    else
                    {
                        //query.SQL.Add("   and H.STATUS<>-1 ");
                        //query.SQL.Add("   and H.STATUS<>-2 ");
                        //query.SQL.Add("   and H.STATUS<>-3 ");
                    }

                    if (obj.sHYKNO_Begin != "" && obj.sHYKNO_Begin != null)
                        query.SQL.Add(" and H.HYK_NO>='" + obj.sHYKNO_Begin + "'");
                    if (obj.sHYKNO_End != "" && obj.sHYKNO_End != null)
                        query.SQL.Add(" and H.HYK_NO<='" + obj.sHYKNO_End + "'");
                    if (obj.iSL != 0)
                        query.SQL.Add("   and  ROWNUM <=  " + obj.iSL);
                    query.Open();
                    while (!query.Eof)
                    {
                        if (CrmLibProc.IsValidHHC(null, query.FieldByName("HYID").AsInteger) == true)
                        {
                            HYXX_Detail item = new HYXX_Detail();
                            item.iHYID = query.FieldByName("HYID").AsInteger;
                            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                            item.fQCYE = query.FieldByName("QCYE").AsFloat;
                            item.fCZJE = query.FieldByName("YE").AsFloat;
                            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                            item.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
                            //item.sSFZBH = query.FieldByName("SFZBH").AsString;
                            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                            item.fYE = query.FieldByName("YE").AsFloat;
                            item.sMDMC = query.FieldByName("MDMC").AsString;
                            //item.iFXDW = query.FieldByName("FXDW").AsInteger;
                            item.sFXDWMC = query.FieldByName("FXDWMC").AsString;
                            //item.iSXMDID = query.FieldByName("SXMDID").AsInteger;
                            //item.sSXMDMC = query.FieldByName("SXMDMC").AsString;
                            item.dJKRQ = query.FieldByName("JKRQ").AsDateTime.ToString();
                            item.iBJ_PSW = query.FieldByName("BJ_PSW").AsInteger;
                            item.iCDJZ = query.FieldByName("CDJZ").AsInteger;//??
                            item.iFXDW = query.FieldByName("FXDW").AsInteger;
                            item.iBJ_PARENT = (query.FieldByName("BJ_PARENT").AsInteger == 0) ? 1 : 0;
                            //item.sFFXDWMC = query.FieldByName("FFXDWMC").AsString;
                            lst.Add(item);
                        }
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
        public static string GetMZKFP(out string msg, CyQuery query, CRMLIBASHX param)
        {
            //获取面值卡，必须传入iHYKTYPE，可以传入sHYKNO_Begin、sHYKNO_End、iSL、iSTATUS(默认>=0)
            msg = string.Empty;

            msg = string.Empty;

            List<HYXX_Detail> lst = new List<HYXX_Detail>();


            string tp_sql = "";
            if (param.iFP_FLAG == 2)
            {
                //KA开


                tp_sql += " select ";
                tp_sql += " X.HYK_NO,X.HYID ";
                tp_sql += " ,E.YE ,P.JLBH ";
                tp_sql += " from ";
                tp_sql += " MZK_JEZH E,MZKXX X,HYK_CZKFPJL P ,MZK_SKJL L ";
                tp_sql += "WHERE 1=1 ";
                tp_sql += "AND X.HYID=P.HYID ";
                tp_sql += "AND X.HYID=E.HYID  ";
                tp_sql += "AND L.JLBH=P.JLBH  ";



                if (param.sHYK_NO1 != "")
                {
                    tp_sql += "  and X.HYK_NO>='" + param.sHYK_NO1 + "'";
                }
                if (param.sHYK_NO2 != "")
                {
                    tp_sql += " and X.HYK_NO<='" + param.sHYK_NO2 + "'";
                }


                if (param.iSKJLBH != 0)
                {
                    tp_sql += "  and  P.JLBH=" + param.iSKJLBH;
                }
                if (param.dDJSJ1 != "")
                {
                    tp_sql += "   and L.ZXRQ<='" + param.dDJSJ1 + "'";
                }
                if (param.dDJSJ2 != "")
                {
                    tp_sql += "  and L.ZXRQ<='" + param.dDJSJ2 + "'";
                }

            }

            else if (param.iFP_FLAG == 3)
            {//没开
                tp_sql += " select ";
                tp_sql += " X.HYK_NO,X.HYID, L.JLBH,";
                tp_sql += " E.YE ";
                tp_sql += " from ";
                tp_sql += " MZK_JEZH E,MZKXX X,MZK_SKJL L,mzk_skjlitem m ";
                tp_sql += "WHERE 1=1 ";
                tp_sql += "AND X.HYID=E.HYID  and L.JLBH=M.JLBH AND M.CZKHM=X.HYK_NO AND L.STATUS=2 ";
                tp_sql += "AND X.HYID  NOT IN (SELECT P.HYID FROM HYK_CZKFPJL P)";
                if (param.sHYK_NO1 != "")
                {
                    tp_sql += "   and X.HYK_NO>='" + param.sHYK_NO1 + "'";
                }
                if (param.sHYK_NO2 != "")
                {
                    tp_sql += "   and X.HYK_NO<='" + param.sHYK_NO2 + "'";
                }

                if (param.dDJSJ1 != "")
                {
                    tp_sql += "   and L.ZXRQ<='" + param.dDJSJ1 + "'";
                }
                if (param.dDJSJ2 != "")
                {
                    tp_sql += "  and L.ZXRQ<='" + param.dDJSJ2 + "'";
                }
                if (param.iSKJLBH != 0)
                {
                    tp_sql += "  and  L.JLBH=" + param.iSKJLBH;
                }

            }

            else
            {

                tp_sql += " select ";
                tp_sql += " X.HYK_NO,X.HYID ";
                tp_sql += " ,E.YE ,P.FP_FLAG,L.JLBH";
                tp_sql += " from ";
                tp_sql += " MZK_JEZH E,MZKXX X,HYK_CZKFPJL P,";
                tp_sql += "MZK_SKJL L WHERE 1=1 ";
                tp_sql += "AND X.HYID=P.HYID ";
                tp_sql += "AND X.HYID=E.HYID  ";
                tp_sql += "AND L.JLBH=P.JLBH  ";


                if (param.iSKJLBH != 0)
                {
                    tp_sql += "  and  L.SKJLBH=" + param.iSKJLBH;
                }
                if (param.sHYK_NO1 != "")
                {
                    tp_sql += "  and X.HYK_NO>='" + param.sHYK_NO1 + "'";
                }
                if (param.sHYK_NO2 != "")
                {
                    tp_sql += " and X.HYK_NO<='" + param.sHYK_NO2 + "'";
                }
                if (param.dDJSJ1 != "")
                {
                    tp_sql += "   and L.DJSJ<='" + param.dDJSJ1 + "'";
                }
                if (param.dDJSJ2 != "")
                {
                    tp_sql += "  and L.DJSJ<='" + param.dDJSJ2 + "'";
                }

                if (param.iFP_FLAG != 0)
                {
                    tp_sql += "  and P.FP_FLAG<=" + param.iFP_FLAG;
                }
                tp_sql += "UNION ";

                tp_sql += " select ";
                tp_sql += " X.HYK_NO,X.HYID,";
                tp_sql += " E.YE, L. JLBH,0 AS FP_FLAG";
                tp_sql += " from ";
                tp_sql += " MZK_JEZH E,MZKXX X,MZK_SKJLITEM M ,MZK_SKJL L   ";
                tp_sql += "WHERE 1=1 ";
                tp_sql += "AND X.HYID=E.HYID  ";
                tp_sql += "  AND M.CZKHM=X.HYK_NO AND M.JLBH=L.JLBH  ";
                if (param.sHYK_NO1 != "")
                {
                    tp_sql += "   and X.HYK_NO>='" + param.sHYK_NO1 + "'";
                }
                if (param.sHYK_NO2 != "")
                {
                    tp_sql += "   and X.HYK_NO<='" + param.sHYK_NO2 + "'";
                }





            }






            query.SQL.Text = tp_sql;


            query.Open();
            while (!query.Eof)
            {

                HYXX_Detail item = new HYXX_Detail();
                item.iHYID = query.FieldByName("HYID").AsInteger;
                item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                item.fCZJE = query.FieldByName("YE").AsFloat;
                item.iJLBH = query.FieldByName("JLBH").AsInteger;

                //if (param.iFP_FLAG != 3)
                //{
                //    item.iFP_FLAG = query.FieldByName("FP_FLAG").AsInteger;
                //}
                lst.Add(item);

                query.Next();
            }
            query.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetCXBFKHBC(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<HYXX_Detail> lst = new List<HYXX_Detail>();
            string tp_sql = "";
            tp_sql += " select * from HYKJKJLITEM  ";

            tp_sql += "WHERE 1=1   ";
            if (param.sHYK_NO1 != "")
            {
                tp_sql += "  and CZKHM>='" + param.sHYK_NO1 + "'";
            }
            if (param.sHYK_NO2 != "")
            {
                tp_sql += " and CZKHM<='" + param.sHYK_NO2 + "'";
            }
            query.SQL.Text = tp_sql;


            query.Open();
            while (!query.Eof)
            {

                HYXX_Detail item = new HYXX_Detail();
                item.sCZKHM = query.FieldByName("CZKHM").AsString;
                item.sCDNR = query.FieldByName("CDNR").AsString;
                if (GlobalVariables.SYSConfig.bCDNR2JM)
                    item.sCDNR = CrmLibProc.DecCDNR(item.sCDNR);
                item.fJE = query.FieldByName("JE").AsFloat;
                item.iBJ_ZK = 0;


                lst.Add(item);

                query.Next();
            }
            query.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetSQDList(out string msg, SQD_Detail obj)
        {
            //获取申请单，必须传入iHYKTYPE，可以传入sBGDDMC_BC、BGDDMC_BR、iSL
            msg = string.Empty;
            List<object> lst = new List<object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {

                    string tp_sql = "";
                    tp_sql += " select ";
                    tp_sql += " W.* ";
                    tp_sql += " ,BC.BGDDMC BGDDMC_BC";
                    tp_sql += " ,BR.BGDDMC BGDDMC_BR";
                    tp_sql += " ,D.HYKNAME ";
                    tp_sql += " from MZK_LQSQJL W";
                    tp_sql += " ,HYK_BGDD BC";
                    tp_sql += " ,HYK_BGDD BR ";
                    tp_sql += " ,HYKDEF D ";
                    tp_sql += " where W.BGDDDM_BC=BC.BGDDDM";
                    tp_sql += " and W.BGDDDM_BR=BR.BGDDDM";
                    tp_sql += " and W.HYKTYPE=D.HYKTYPE";
                    tp_sql += " and W.STATUS=1";
                    query.SQL.Text = tp_sql;

                    if (obj.iHYKTYPE != 0)
                    {
                        query.SQL.Add("   and D.HYKTYPE=" + obj.iHYKTYPE);
                    }


                    if (obj.sBGDDDM_BC != "" && obj.sBGDDDM_BC != null)
                        query.SQL.Add(" and BC.BGDDDM>='" + obj.sBGDDDM_BC + "'");

                    if (obj.sBGDDDM_BR != "" && obj.sBGDDDM_BR != null)
                        query.SQL.Add(" and BR.BGDDDM<='" + obj.sBGDDDM_BR + "'");
                    if (obj.iSL != 0)
                        query.SQL.Add("   and  ROWNUM <=  " + obj.iSL);
                    query.Open();
                    while (!query.Eof)
                    {
                        SQD_Detail item = new SQD_Detail();
                        item.iJLBH = query.FieldByName("JLBH").AsInteger;
                        item.iHYKSL = query.FieldByName("HYKSL").AsInteger;
                        item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.sBGDDDM_BC = query.FieldByName("BGDDDM_BC").AsString;
                        item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        item.sBZ = query.FieldByName("BZ").AsString;
                        item.sBGDDMC_BC = query.FieldByName("BGDDMC_BC").AsString;
                        item.sBGDDDM_BR = query.FieldByName("BGDDDM_BR").AsString;
                        item.sBGDDMC_BR = query.FieldByName("BGDDMC_BR").AsString;
                        //item.iDJR = query.FieldByName("DJR").AsInteger;
                        //item.sDJRMC = query.FieldByName("DJRMC").AsString;
                        //item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        item.iZXR = query.FieldByName("ZXR").AsInteger;
                        item.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        //item.iDJLX = query.FieldByName("DJLX").AsInteger;


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
        public static void FillzWUCTabs_BMQDY(out string msg, BMQDY obj, out List<object> list)
        {
            list = new List<object>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
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
                    query.SQL.Text = "select * from BMQDEF A WHERE 1=1 ";
                    if (obj.iBMQID != 0)
                    {
                        query.SQL.Text += " and A.BMQID=" + obj.iBMQID;
                    }
                    if (obj.sBMQMC != "")
                    {
                        query.SQL.Text += " and  A.BMQMC like '%" + obj.sBMQMC + "%'";
                    }
                    query.SQL.Text += " order by A.BMQID desc";
                    query.Open();

                    while (!query.Eof)
                    {
                        BMQDY item = new BMQDY();
                        item.iBMQID = query.FieldByName("BMQID").AsInteger;
                        item.sBMQMC = query.FieldByName("BMQMC").AsString;
                        list.Add(item);
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static void FillzWUCTabs_LMSHDY(out string msg, LMSHDY obj, out List<object> list)
        {
            list = new List<object>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
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
                    query.SQL.Text = "select * from LM_SHDY A WHERE 1=1 ";
                    if (obj.iJLBH != 0)
                    {
                        query.SQL.Text += " and A.JLBH=" + obj.iJLBH;
                    }
                    if (obj.sSHMC != "")
                    {
                        query.SQL.Text += " and  A.SHMC like '%" + obj.sSHMC + "%'";
                    }
                    query.SQL.Text += " order by A.JLBH desc";
                    query.Open();

                    while (!query.Eof)
                    {
                        LMSHDY item = new LMSHDY();
                        item.iJLBH = query.FieldByName("JLBH").AsInteger;
                        item.sSHMC = query.FieldByName("SHMC").AsString;
                        list.Add(item);
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool FillzWUCTabs_GHS(out string outMessage, int userId, out List<LPGHS> ghsList, int iGHSBH, string sGHSMC, string sZYNR)
        {
            ghsList = new List<LPGHS>();
            outMessage = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                outMessage = e.Message;
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " select * from GHSDEF W where W.BJ_TY=0";
                    if (iGHSBH != 0 && iGHSBH != null)
                    {
                        query.SQL.Add("  and W.GHSID= " + iGHSBH + "");
                    }
                    if (sGHSMC != "" && sGHSMC != null)
                    {
                        query.SQL.Add("  and W.GHSMC like '%" + sGHSMC + "%'");
                    }
                    if (sZYNR != "" && sZYNR != null)
                    {
                        query.SQL.Add("  and W.ZYXM like '%" + sZYNR + "%' ");
                    }
                    query.Open();

                    while (!query.Eof)
                    {
                        LPGHS ghsItem = new LPGHS();
                        ghsList.Add(ghsItem);
                        ghsItem.iJLBH = query.FieldByName("GHSID").AsInteger;
                        ghsItem.sGHSMC = query.FieldByName("GHSMC").AsString;
                        ghsItem.sDHHM = query.FieldByName("DHHM").AsString;
                        ghsItem.sGHSDZ = query.FieldByName("GHSDZ").AsString;
                        ghsItem.sZYXM = query.FieldByName("ZYXM").AsString;
                        ghsItem.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        outMessage = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            if (outMessage == "") { return true; }
            else { return false; }
        }

        public static string FillLPFFHD(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<LPFFHDDY> lst = new List<LPFFHDDY>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select S.* from LPHDDYD S where S.ZXR is not null";
                query.SQL.Add(" order by S.JLBH");
                query.Open();
                while (!query.Eof)
                {
                    LPFFHDDY _item = new LPFFHDDY();
                    _item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    _item.sZT = query.FieldByName("ZT").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }

        public static string SearchRuleData(int iLPFFHDBH)
        {
            List<object> lst = new List<object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select W.MDID,W.MDMC,W.KSRQ,W.JSRQ  from LPHDDYD W";
                    if (iLPFFHDBH != 0)
                        query.SQL.Add("  where W.JLBH=" + iLPFFHDBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        LPFFHDDY obj = new LPFFHDDY();
                        lst.Add(obj);
                        obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                        obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        obj.sMDMC = query.FieldByName("MDMC").AsString;
                        obj.iMDID = query.FieldByName("MDID").AsInteger;
                        query.Next();
                    }
                    query.Close();
                    if (lst.Count == 1)
                    {
                        query.SQL.Text = "select * from LPHDDYD_LP I, HYK_JFFHLPKC K where I.LPID=K.LPID and I.BGDDDM=K.BGDDDM  and I.JLBH=" + iLPFFHDBH;
                        query.SQL.Add(" order by I.LPID");
                        query.Open();
                        while (!query.Eof)
                        {
                            LPFFHDDY.LPGL_LPFFHDITEM obj = new LPFFHDDY.LPGL_LPFFHDITEM();
                            ((LPFFHDDY)lst[0]).itemTable.Add(obj);
                            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                            obj.sLPMC = query.FieldByName("LPMC").AsString;
                            obj.sLPDM = query.FieldByName("LPDM").AsString;
                            obj.iLPID = query.FieldByName("LPID").AsInteger;
                            obj.fKCSL = query.FieldByName("KCSL").AsFloat;
                            query.Next();
                        }
                        query.Close();
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
            return (JsonConvert.SerializeObject(lst[0]));
        }

        public static string FillLPSX(out string msg, int lpsxlx)
        {
            List<LPSX> lst = new List<LPSX>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * from LPSXDEF W ";
                    // iLPSXLX=0 材质,1颜色,2款式

                    query.SQL.Add(" where W.LPSXLX=" + lpsxlx);

                    query.Open();
                    while (!query.Eof)
                    {
                        LPSX obj = new LPSX();
                        obj.iJLBH = query.FieldByName("LPSXID").AsInteger;
                        obj.iLPSXLX = query.FieldByName("LPSXLX").AsInteger;
                        obj.sLPSXMC = query.FieldByName("LPSXMC").AsString;
                        obj.sLPSXNR = query.FieldByName("LPSXNR").AsString;
                        lst.Add(obj);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string GenMenu(out string msg, CyQuery query, CRMLIBASHX param)
        {
            //这里将使用PUBDB连接
            msg = string.Empty;
            string sMENU = LibProc.GetWebConfig("MENUSET", "L");
            //菜单模式有三种
            //A全部菜单，即SYSLIB和PROCMSGDEF组成的两级菜单
            //D显示菜单，即MODULE_DEF和MODULE_DEF_ITEM组成的多级菜单
            //L文件菜单，即JS文件
            int sysid = 510;

            List<DisplayMenu> menus = new List<DisplayMenu>();
            DisplayMenu root = new DisplayMenu();
            root.id = "root";
            root.pId = "";
            root.name = sMENU;
            menus.Add(root);

            switch (sMENU)
            {
                case "D":
                    query.SQL.Text = "select D.* from MODULE_DEF D,MODULE_GROUP G where D.MGRPID=G.MGRPID and G.SYSID=" + sysid + " order by DISPLAY_INX";
                    query.Open();
                    while (!query.Eof)
                    {
                        DisplayMenu one = new DisplayMenu();
                        one.id = "D" + query.FieldByName("ID").AsInteger.ToString();
                        one.pId = "root";
                        one.name = query.FieldByName("NAME").AsString;
                        menus.Add(one);
                        query.Next();
                    }
                    query.Close();
                    query.SQL.Text = "select M.*,P.URL from MODULE_DEF_MENU M,MODULE_DEF D,MODULE_GROUP G,PROCMSGDEF P";
                    query.SQL.Add("where M.ID=D.ID and D.MGRPID=G.MGRPID and G.SYSID=" + sysid + " and M.MSG_ID=P.MSG_ID(+)");
                    query.SQL.Add("order by D.DISPLAY_INX,M.POS");
                    query.Open();
                    while (!query.Eof)
                    {
                        DisplayMenu one = new DisplayMenu();
                        one.sPOS = query.FieldByName("POS").AsString;
                        one.id = "M" + one.sPOS;
                        one.rId = "D" + query.FieldByName("ID").AsInteger.ToString();
                        if (one.sPOS.Length == 2)
                            one.pId = one.rId;
                        else
                            one.pId = "M" + one.sPOS.Substring(0, 2);
                        one.name = query.FieldByName("CAPTION").AsString;
                        one.sURL = query.FieldByName("URL").AsString;
                        menus.Add(one);
                        query.Next();
                    }
                    break;
                case "A":
                    query.SQL.Text = "select * from SYSLIB S where S.SYSID=" + sysid + " order by ID";
                    query.Open();
                    while (!query.Eof)
                    {
                        DisplayMenu one = new DisplayMenu();
                        one.id = "D" + query.FieldByName("ID").AsInteger.ToString();
                        one.pId = "root";
                        one.name = query.FieldByName("LIBNAME").AsString;
                        menus.Add(one);
                        query.Next();
                    }
                    query.Close();
                    query.SQL.Text = "select P.* from PROCMSGDEF P,SYSLIB S";
                    query.SQL.Add("where P.PARENT_MSGID is null and P.LIBID=S.ID and S.SYSID=" + sysid);
                    query.SQL.Add("order by P.MSG_ID");
                    query.Open();
                    while (!query.Eof)
                    {
                        DisplayMenu one = new DisplayMenu();
                        one.id = "M" + query.FieldByName("MSG_ID").AsInteger.ToString();
                        one.pId = "D" + query.FieldByName("LIBID").AsInteger.ToString();
                        one.name = query.FieldByName("CAPTION").AsString;
                        one.sURL = query.FieldByName("URL").AsString;
                        menus.Add(one);
                        query.Next();
                    }
                    break;
                    ;
            }
            return JsonConvert.SerializeObject(menus);
        }

        /// <summary>
        /// 加密磁道内容
        /// </summary>
        /// <param name="pCDNR"></param>
        /// <returns></returns>
        public static string EncCDNR(string pCDNR)
        {
            return EncryptUtils.EncryptWebData(pCDNR, GetKey(GlobalVariables.SYSInfo.sProjectKey, 1));
        }
        /// <summary>
        /// 解密磁道内容
        /// </summary>
        /// <param name="pCDNR"></param>
        /// <returns></returns>
        public static string DecCDNR(string pCDNR)
        {
            return EncryptUtils.DecryptWebData(pCDNR, GetKey(GlobalVariables.SYSInfo.sProjectKey, 1));
        }
        private static byte[] GetKey(string pKey, int pVal)
        {
            int iProjectKey = Convert.ToInt32(pKey);
            byte[] result = Encoding.Default.GetBytes(Math.Floor(Math.Pow(iProjectKey, 1.5) * pVal).ToString());
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return Encoding.ASCII.GetBytes(BitConverter.ToString(output).Replace("-", "").Substring(2, 8));
        }
        /// <summary>
        /// 把HYID,HYK_NO,CDNR连在一起加密
        /// </summary>
        /// <param name="pHYID"></param>
        /// <param name="sHYK_NO"></param>
        /// <param name="sCDNR"></param>
        /// <returns></returns>
        public static string EncHHC(int pHYID, string sHYK_NO, string sCDNR)
        {
            return EncryptUtils.EncryptWebData(pHYID + "," + sHYK_NO + "," + sCDNR, GetKey(GlobalVariables.SYSInfo.sProjectKey, 3));
        }
        /// <summary>
        /// 判断HYID,HYK_NO,CDNR是否符合加密的数据
        /// </summary>
        /// <param name="pHYID"></param>
        /// <param name="sHYK_NO"></param>
        /// <param name="sCDNR"></param>
        /// <returns></returns>
        private static bool CheckHHC(int pHYID, string sHYK_NO, string sCDNR, string HHC)
        {
            return EncryptUtils.EncryptWebData(pHYID + "," + sHYK_NO + "," + sCDNR, GetKey(GlobalVariables.SYSInfo.sProjectKey, 3)) == HHC;
        }
        /// <summary>
        /// 插入HHC
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pHYID"></param>
        /// <returns></returns>
        public static bool InsHHC(CyQuery query, int pHYID)
        {
            query.Close();
            query.SQL.Text = "select HYK_NO,CDNR from HYK_HYXX H where H.HYID=:HYID";
            query.ParamByName("HYID").AsInteger = pHYID;
            query.Open();
            if (query.IsEmpty)
                return false;
            string tHYK_NO = query.FieldByName("HYK_NO").AsString;
            string tCDNR = query.FieldByName("CDNR").AsString;
            query.Close();
            query.SQL.Text = "update HHC set HHC=:HHC where HYID=:HYID";
            query.ParamByName("HYID").AsInteger = pHYID;
            query.ParamByName("HHC").AsString = EncHHC(pHYID, tHYK_NO, tCDNR);
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into HHC(HYID,HHC)values(:HYID,:HHC)";
                query.ParamByName("HYID").AsInteger = pHYID;
                query.ParamByName("HHC").AsString = EncHHC(pHYID, tHYK_NO, tCDNR);
                query.ExecSQL();
            }
            return true;
        }
        /// <summary>
        /// 检测HHC
        /// </summary>
        /// <param name="query"></param>
        /// <param name="pHYID"></param>
        /// <returns></returns>
        public static bool IsValidHHC(CyQuery query, int pHYID)
        {
            DbConnection conn = null;
            bool BoolInsert = true;
            if (query == null)
                conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                if (query == null)
                    query = new CyQuery(conn);
                try
                {
                    query.Close();
                    query.SQL.Text = "select HYK_NO,CDNR from MZKXX H where H.HYID=:HYID";
                    query.ParamByName("HYID").AsInteger = pHYID;
                    query.Open();
                    if (query.IsEmpty)
                        return false;
                    string tHYK_NO = query.FieldByName("HYK_NO").AsString;
                    string tCDNR = query.FieldByName("CDNR").AsString;
                    query.Close();
                    query.SQL.Text = "select HHC from HHC H where H.HYID=:HYID";
                    query.ParamByName("HYID").AsInteger = pHYID;
                    query.Open();
                    if (query.IsEmpty)
                        return false;
                    string tHHC = query.FieldByName("HHC").AsString;
                    query.Close();
                    BoolInsert = CheckHHC(pHYID, tHYK_NO, tCDNR, tHHC);

                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return BoolInsert;

        }
        /// <summary>
        /// 加密顾客档案中的资料用（如手机号码、身份证号等）
        /// </summary>
        /// <param name="pGKDA">资料内容</param>
        /// <returns>加密后的资料内容</returns>
        public static string EncGKDA(string pGKDA)
        {
            return EncryptUtils.EncryptWebData(pGKDA, GetKey(GlobalVariables.SYSInfo.sProjectKey, 13));
        }
        /// <summary>
        /// 解密顾客档案中的资料用（如手机号码、身份证号等）
        /// </summary>
        /// <param name="pGKDA">加密后的资料内容</param>
        /// <returns>资料内容</returns>
        public static string DecGKDA(string pGKDA)
        {
            return EncryptUtils.DecryptWebData(pGKDA, GetKey(GlobalVariables.SYSInfo.sProjectKey, 13));
        }
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pPSW"></param>
        /// <returns></returns>
        public static string EncPSW(string pPSW)
        {
            return EncryptUtils.EncryptWebData(pPSW, GetKey(GlobalVariables.SYSInfo.sProjectKey, 7));
        }
        /// <summary>
        /// 解密密码
        /// </summary>
        /// <param name="pPSW"></param>
        /// <returns></returns>
        public static string DecPSW(string pPSW)
        {
            return EncryptUtils.DecryptWebData(pPSW, GetKey(GlobalVariables.SYSInfo.sProjectKey, 7));
        }
        /// <summary>
        /// 记录卡变动信息
        /// </summary>
        /// <param name="pHYK_NO">卡号，必填</param>
        /// <param name="pHYID">会员ID，已发售卡必填，库存卡传0</param>
        /// <param name="pCLLX">处理类型，待定义</param>
        /// <param name="pJLBH">单据记录编号</param>
        /// <param name="pDJR">登记人</param>
        /// <param name="pDJRMC">登记人名称</param>
        /// <param name="pBDJE">变动金额</param>
        /// <param name="pOLD_HYK_NO">原卡号</param>
        /// <param name="pOLD_HYKTYPE">原卡类型</param>
        public static void SaveCardTrack(CyQuery query, string pHYK_NO, int pHYID, int pCLLX, int pJLBH, int pDJR, string pDJRMC, double pBDJE = 0, string pOLD_HYK_NO = "no", int pOLD_HYKTYPE = 0, string pIPAdd = "")
        {
            //说明，在审核之后调用，所有pHYK_NO是新卡号，卡类型可以从库里获取，所以需要传入老卡号、老卡类型，但不是必须
            string msg;
            DbConnection conn = null;
            if (query == null)
                conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                if (query == null)
                    query = new CyQuery(conn);
                try
                {
                    if (pHYID > 0)
                    {
                        query.SQL.Text = "select M.*,E.YE QCYE from MZKXX M,MZK_JEZH E where M.HYID=:HYID and M.HYID=E.HYID";
                        query.ParamByName("HYID").AsInteger = pHYID;
                    }
                    else
                    {
                        query.SQL.Text = "select * from MZKCARD where CZKHM=:CZKHM";
                        query.ParamByName("CZKHM").AsString = pHYK_NO;
                    }
                    query.Open();
                    if (query.IsEmpty)
                    {
                        throw new Exception("没有找到卡信息");
                    }
                    int tSTATUS = query.FieldByName("STATUS").AsInteger;
                    int tHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    double tYE = query.FieldByName("QCYE").AsFloat;
                    DateTime tYXQ = query.FieldByName("YXQ").AsDateTime;
                    string tValue = string.Empty;
                    tValue += pCLLX + "," + pJLBH + "," + pDJR + "," + HttpUtility.UrlEncode(pDJRMC) + ",";
                    tValue += pBDJE + "," + tYE + "," + tSTATUS + "," + tYXQ + ",";
                    tValue += pOLD_HYK_NO + "," + pOLD_HYKTYPE + "," + tHYKTYPE + ",";
                    tValue += pIPAdd;
                    byte[] DesKey = { (byte)'R', (byte)'3', (byte)'V', (byte)'h', (byte)'N', (byte)'T', (byte)'E', (byte)'w' };
                    //R3VhNTEw
                    tValue = EncryptUtils.EncryptWebData(tValue, DesKey);
                    //query.Command.Dispose();
                    DateTime ServerTime;
                    ServerTime = CyDbSystem.GetDbServerTime(query);
                    query.Close();
                    int iJLBH = SeqGenerator.GetSeq("CARD_TRACK");
                    query.SQL.Text = "insert into CARD_TRACK(JLBH,HYID,HM,RQ,CLLX,VALUE)";
                    query.SQL.Add("  values(:JLBH,:HYID,:HM,:RQ,:CLLX,:VALUE)");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("HYID").AsInteger = pHYID;
                    query.ParamByName("HM").AsString = pHYK_NO;
                    query.ParamByName("RQ").AsDateTime = ServerTime;
                    query.ParamByName("CLLX").AsInteger = pCLLX;
                    query.ParamByName("VALUE").AsString = tValue;
                    query.ExecSQL();
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
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
        public static string GetHD(out string msg, HYHDXX obj, int tSFPS, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            List<object> lst = new List<object>();

            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "delete from TMP_HDDEF_PS where PERSON_ID=:PERSON_ID";
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ExecSQL();

                    #region 报名人数统计
                    //有客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,X.KFRYID,0,0,0,sum(L.BMRS) as BMRS");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and X.KFRYID>0 and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,X.KFRYID");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //无客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,0,0,0,sum(L.BMRS) as BMRS");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and nvl(X.KFRYID,0)<=0 and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //非会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,0,0,0,sum(L.BMRS) as BMRS");
                    query.SQL.Add(" from HYK_HDCJJL L");
                    query.SQL.Add(" where nvl(L.HYID,0)<=0 and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    #endregion
                    #region 确认人数统计
                    //有客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,X.KFRYID,sum(L.CJRS) as QRRS,0,0,0");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID = X.HYID and X.KFRYID > 0 and L.QRSJ is not null and L.HDID = :HDID");
                    query.SQL.Add(" group by L.HDID,X.KFRYID");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //无客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,sum(L.CJRS) as QRRS,0,0,0");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and nvl(X.KFRYID,0)<=0 and L.QRSJ is not null and L.HDID= :HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //非会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,sum(L.CJRS) as QRRS,0,0,0");
                    query.SQL.Add(" from HYK_HDCJJL L");
                    query.SQL.Add(" where nvl(L.HYID,0)<=0 and L.QRSJ is not null and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    #endregion
                    #region 参加人数统计
                    //有客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,X.KFRYID,0,sum(L.CJRS) as CJRS,0,0");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and X.KFRYID>0 and L.CJSJ is not null and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,X.KFRYID");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //无客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,0,sum(L.CJRS) as CJRS,0,0");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and nvl(X.KFRYID,0)<=0 and L.CJSJ is not null and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //非会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,0,sum(L.CJRS) as CJRS,0,0");
                    query.SQL.Add(" from HYK_HDCJJL L");
                    query.SQL.Add(" where nvl(L.HYID,0)<=0 and L.CJSJ is not null and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    #endregion
                    #region 回访人数统计
                    //有客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,X.KFRYID,0,0,count(*) as HFRS,0");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and X.KFRYID>0 and L.HFBJ=1 and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,X.KFRYID");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //无客服经理的会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,0,0,count(*) as HFRS,0");
                    query.SQL.Add(" from HYK_HDCJJL L,HYK_HYXX X");
                    query.SQL.Add(" where L.HYID=X.HYID and nvl(X.KFRYID,0)<=0 and L.HFBJ=1 and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    //非会员
                    query.SQL.Text = "insert into TMP_HDDEF_PS(PERSON_ID,HDID,KFRYID,QRRS,CJRS,HFRS,BMRS)";
                    query.SQL.Add(" select :PERSON_ID,L.HDID,L.BMDJR,0,0,count(*) as HFRS,0");
                    query.SQL.Add(" from HYK_HDCJJL L");
                    query.SQL.Add(" where nvl(L.HYID,0)<=0 and L.HFBJ=1 and L.HDID=:HDID");
                    query.SQL.Add(" group by L.HDID,L.BMDJR");
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.ParamByName("HDID").AsInteger = obj.iHDID;
                    query.ExecSQL();
                    query.SQL.Clear();
                    #endregion

                    query.SQL.Clear();
                    query.SQL.Text = "select * from( ";
                    query.SQL.Add("select F.HDID,F.HDMC,F.KSSJ,F.JSSJ,F.RS,F.DJSJ,A.KFRYID,R.PERSON_NAME as KFJLMC");
                    query.SQL.Add(" ,sum(A.BMRS) as BMRS,sum(A.QRRS) as QRRS,sum(A.CJRS) as CJRS,sum(A.HFRS) as HFRS");
                    query.SQL.Add(" from TMP_HDDEF_PS A,HYK_HDNRDEF F,RYXX R ,HYK_HDNRDEF_KFJLPS P");
                    query.SQL.Add(" where A.HDID=F.HDID and A.KFRYID=R.PERSON_ID(+) and A.HDID=P.HDID(+) and A.KFRYID=P.KFRYID(+) and A.PERSON_ID=:PERSON_ID");
                    if (tSFPS == 1)
                        query.SQL.Add(" and P.PSR is not null");
                    if (tSFPS == 2)
                        query.SQL.Add(" and P.PSR is null");
                    query.SQL.Add(" group by F.HDID,F.HDMC,F.KSSJ,F.JSSJ,F.RS,F.DJSJ,A.KFRYID,R.PERSON_NAME");
                    query.SQL.Add(" )D where D.HFRS!=0");  //查询评述前一定要满足已回访过
                    query.ParamByName("PERSON_ID").AsInteger = obj.iLoginRYID;
                    query.Open();
                    while (!query.Eof)
                    {
                        HYHDXX item = new HYHDXX();
                        item.iHDID = query.FieldByName("HDID").AsInteger;
                        item.sHDMC = query.FieldByName("HDMC").AsString;
                        item.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
                        item.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
                        item.iRS = query.FieldByName("RS").AsInteger;
                        item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        item.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                        item.sKFRYMC = query.FieldByName("KFJLMC").AsString;
                        item.iBMRS = query.FieldByName("BMRS").AsInteger;
                        item.iQRRS = query.FieldByName("QRRS").AsInteger;
                        item.iCJRS = query.FieldByName("CJRS").AsInteger;
                        item.iHFRS = query.FieldByName("HFRS").AsInteger;
                        lst.Add(item);
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
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return obj.GetTableJson(lst);
        }
        public static HYHDXX GetLDPSXX(out string msg, int pHDID, int pKFRYID, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYHDXX obj = new HYHDXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select P.*,F.HDMC,R.PERSON_NAME ";
                    query.SQL.Add(" from HYK_HDNRDEF_KFJLPS P,HYK_HDNRDEF F,RYXX R");
                    query.SQL.Add(" where P.HDID=F.HDID(+) and P.KFRYID=R.PERSON_ID(+) ");
                    query.SQL.Add(" and P.HDID = " + pHDID);
                    query.SQL.Add(" and P.KFRYID = " + pKFRYID);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.fPF = query.FieldByName("FZ").AsFloat;
                        obj.sLDPY = query.FieldByName("LDPY").AsString;
                        obj.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                        obj.sKFRYMC = query.FieldByName("PERSON_NAME").AsString;
                        obj.iHDID = query.FieldByName("HDID").AsInteger;
                        obj.sHDMC = query.FieldByName("HDMC").AsString;
                        obj.dPSSJ = FormatUtils.DateToString(query.FieldByName("PSSJ").AsDateTime);
                        obj.iPSR = query.FieldByName("PSR").AsInteger;
                        obj.sPSRMC = query.FieldByName("PSRMC").AsString;

                    }
                    else
                    {
                        obj.fPF = 0;
                        obj.sLDPY = "";
                        obj.iHDID = pHDID;
                        obj.iKFRYID = pKFRYID;
                        obj.sHDMC = "";
                        obj.sKFRYMC = "";
                    }
                    query.Close();
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
            return obj;
        }

        public static RWJL GetRWJL(out string msg, int pJLBH, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            RWJL obj = new RWJL();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * from RWJL  ";
                    query.SQL.Add(" where JLBH = " + pJLBH);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sRW = query.FieldByName("RW").AsString;
                        obj.sWCQK = query.FieldByName("WCQK").AsString;
                        obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        obj.iRWWCZT = query.FieldByName("RWWCZT").AsInteger;
                        obj.sHYKNO = query.FieldByName("HYKNO").AsString;
                        obj.sHYNAME = query.FieldByName("HYNAME").AsString;
                        obj.iDJR = query.FieldByName("DJR").AsInteger;
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.iZXR = query.FieldByName("ZXR").AsInteger;
                        obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                        obj.sLDPY = query.FieldByName("LDPY").AsString;
                        obj.fFZ = query.FieldByName("FZ").AsFloat;
                        obj.iPYR = query.FieldByName("PYR").AsInteger.ToString();
                        obj.sPYRMC = query.FieldByName("PYRMC").AsString;
                        obj.dPYRQ = FormatUtils.DatetimeToString(query.FieldByName("PYRQ").AsDateTime);
                    }
                    query.Close();
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
            return obj;
        }

        public static HYHDXX GetHDHFXX(out string msg, int pHFJLBH, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYHDXX obj = new HYHDXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select BZ,HFJG ";
                    query.SQL.Add(" from HYK_HDCJJL_HFXX");
                    query.SQL.Add(" where HFJLBH = " + pHFJLBH);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iHFJG = query.FieldByName("HFJG").AsInteger;
                        obj.sBZ = query.FieldByName("BZ").AsString;
                    }
                    else
                    {
                        obj.iHFJG = 0;
                        obj.sBZ = "";
                    }
                    query.Close();
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
            return obj;
        }
        public static HYXX_Detail CheckSRMD(out string msg, string pHYKTYPE, int pKSY, int pKSR, int pJSY, int pJSR, int pDJR)
        {
            msg = string.Empty;
            string sDBConnName = "CRMDB";
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select count(*) from HYK_HYXX H,HYK_GRXX G,HYKDEF D  ";
                    query.SQL.Add(" where H.HYID=G.HYID and H.HYKTYPE=D.HYKTYPE ");
                    query.SQL.Add(" and H.HYKTYPE in (" + pHYKTYPE + ")");
                    query.SQL.Add(" and ((extract(month from G.CSRQ)= " + pKSY);
                    query.SQL.Add(" and extract(day from G.CSRQ)>=" + pKSR + " )");
                    query.SQL.Add(" or (extract(month from G.CSRQ)> " + pKSY + "))");
                    query.SQL.Add(" and ((extract(month from G.CSRQ)= " + pJSY);
                    query.SQL.Add(" and extract(day from G.CSRQ)<=" + pJSR + " )");
                    query.SQL.Add(" or (extract(month from G.CSRQ)< " + pJSY + "))");
                    query.SQL.Add(" and H.KFRYID = " + pDJR);
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "查询无结果";
                        return null;
                    }
                    else
                    {
                        query.Close();
                        query.SQL.Clear();
                        query.SQL.Text = "select count(*) from HYK_SRMD where RYID=:RYID";
                        query.ParamByName("RYID").AsInteger = pDJR;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            obj.iCOUNT = query.Fields[0].AsInteger;
                        }
                    }
                    query.Close();
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
            return obj;
        }

        public static HYXX_Detail CheckHYMDXX(out string msg, string pHYKTYPE, int pDJR)
        {
            msg = string.Empty;
            string sDBConnName = "CRMDB";
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select count(*) from HYK_HYXX H,HYKDEF D  ";
                    query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE ");
                    query.SQL.Add(" and H.HYKTYPE in (" + pHYKTYPE + ")");
                    query.SQL.Add(" and H.KFRYID = " + pDJR);
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "查询无结果";
                        return null;
                    }
                    else
                    {
                        query.Close();
                        query.SQL.Clear();
                        query.SQL.Text = "select count(*) from HYK_DQWHHYMD where RYID=:RYID";
                        query.ParamByName("RYID").AsInteger = pDJR;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            obj.iCOUNT = query.Fields[0].AsInteger;
                        }
                    }
                    query.Close();
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
            return obj;
        }
        public static HYXX_Detail CheckWXFHYMDXX(out string msg, string pHYKTYPE, int pDJR)
        {
            msg = string.Empty;
            string sDBConnName = "CRMDB";
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select count(*) from HYK_HYXX H,HYKDEF D  ";
                    query.SQL.Add(" where H.HYKTYPE=D.HYKTYPE ");
                    query.SQL.Add(" and H.HYKTYPE in (" + pHYKTYPE + ")");
                    query.SQL.Add(" and H.KFRYID = " + pDJR);
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "查询无结果";
                        return null;
                    }
                    else
                    {
                        query.Close();
                        query.SQL.Clear();
                        query.SQL.Text = "select count(*) from HYK_WXFHYMD where RYID=:RYID";
                        query.ParamByName("RYID").AsInteger = pDJR;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            obj.iCOUNT = query.Fields[0].AsInteger;
                        }
                    }
                    query.Close();
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
            return obj;
        }

        public static HYXX_Detail GetHYQYMX(out string msg, string pQYDM)
        {
            msg = string.Empty;
            string sDBConnName = "CRMDB";
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    string qydm1 = pQYDM.Substring(0, pQYDM.Length - 4);
                    string qydm2 = pQYDM.Substring(0, pQYDM.Length - 2);
                    query.SQL.Text = "select QYMC from HYK_HYQYDY where QYDM=:QYDM";
                    query.ParamByName("QYDM").AsString = qydm1;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sQYMC += query.FieldByName("QYMC").AsString;
                    }
                    query.Close();
                    query.SQL.Text = "select QYMC from HYK_HYQYDY where QYDM=:QYDM";
                    query.ParamByName("QYDM").AsString = qydm2;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sQYMC += query.FieldByName("QYMC").AsString;
                    }
                    query.Close();
                    query.SQL.Text = "select QYMC from HYK_HYQYDY where QYDM=:QYDM";
                    query.ParamByName("QYDM").AsString = pQYDM;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.sQYMC += query.FieldByName("QYMC").AsString;
                    }
                    query.Close();

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
            return obj;
        }

        public static bool GetWXFHYMD(int iDJR, string pHYKTYPE, int pTS)
        {
            bool bValid = false;
            string sDJRMC = string.Empty;
            int iMDSL = 0;
            int _iJLBH = SeqGenerator.GetSeq("HYK_WXFHYMDJL");
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();


                    query.SQL.Text = "delete from HYK_WXFHYMD where RYID=:RYID";
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_WXFHYMDJL(JLBH,MDSL,HFSL,DJR,DJRMC,DJSJ)";
                    query.SQL.Add("values(:JLBH,0,0,:DJR,:DJRMC,:DJSJ)");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ExecSQL();

                    query.SQL.Clear();
                    DateTime today = DateTime.Now;
                    string wxfrq = today.AddDays(-pTS).ToShortDateString();
                    query.SQL.Text = "insert into HYK_WXFHYMDJLITEM(JLBH,HYID,STATUS)";
                    query.SQL.Add("select :JLBH,H.HYID,0");
                    query.SQL.Add(" from HYK_HYXX H,HYKDEF D");
                    query.SQL.Add("where H.HYKTYPE=D.HYKTYPE");
                    query.SQL.Add(" and H.HYKTYPE in (" + pHYKTYPE + ")");
                    query.SQL.Add(" and H.KFRYID = " + iDJR);
                    query.SQL.Add(" and ( ( H.ZHXFRQ is not null and H.ZHXFRQ<='" + wxfrq + " ') or ( H.ZHXFRQ is null and H.JKRQ<='" + wxfrq + "'))");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "select count(*) from HYK_WXFHYMDJLITEM where JLBH=:JLBH";
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        iMDSL = query.Fields[0].AsInteger;
                    }
                    query.Close();

                    query.SQL.Clear();
                    query.SQL.Text = "update HYK_WXFHYMDJL set MDSL=:MDSL where JLBH=:JLBH";
                    query.ParamByName("MDSL").AsInteger = iMDSL;
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_WXFHYMD(JLBH,HYID,RYID)";
                    query.SQL.Add("select JLBH,HYID,:RYID from HYK_WXFHYMDJLITEM where JLBH=:JLBH");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.ExecSQL();

                    bValid = true;


                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool updateWXFDHWHBZ(int iDJR, string sBZ, int iHYID)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    bValid = true;
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    //int iDJBH = 0;
                    int iJLBH = 0;
                    int _iJLBH = 0;
                    string sDJRMC = string.Empty;
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();
                    //query.SQL.Text = "select JLBH from HYK_WXFHYHFJL where HYID=" + iHYID;
                    //query.Open();
                    //if (!query.IsEmpty)
                    //    iDJBH = query.Fields[0].AsInteger;
                    //if (iDJBH <= 0)
                    iJLBH = SeqGenerator.GetSeq("HYK_WXFHYHFJL");
                    //else
                    //    iJLBH = iDJBH;
                    //query.Close();
                    query.SQL.Text = "select JLBH from HYK_WXFHYMD where HYID=" + iHYID;
                    query.Open();
                    if (!query.IsEmpty)
                        _iJLBH = query.Fields[0].AsInteger;
                    query.Close();

                    //query.SQL.Text = "begin update HYK_WXFHYHFJL set BZ=:BZ where JLBH=:JLBH;";
                    //query.SQL.Add("if SQL%NOTFOUND then");
                    query.SQL.Text = "insert into HYK_WXFHYHFJL(JLBH,HYID,DJSJ,DJR,DJRMC,BZ)";
                    query.SQL.Add("values(:JLBH,:HYID,sysdate,:DJR,:DJRMC,:BZ)"); //;end if;end;
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("BZ").AsString = sBZ;
                    query.ExecSQL();
                    query.SQL.Clear();
                    query.SQL.Text = "delete from HYK_WXFHYMD where HYID=:HYID";
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "update HYK_WXFHYMDJLITEM set STATUS=1 where JLBH=:JLBH and HYID=:HYID";
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ExecSQL();
                    bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool GetSRMD(int iDJR, string pHYKTYPE, int pKSY, int pKSR, int pJSY, int pJSR)
        {
            bool bValid = false;
            string sDJRMC = string.Empty;
            int iMDSL = 0;
            int _iJLBH = SeqGenerator.GetSeq("HYK_SRMDJL");
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();


                    query.SQL.Text = "delete from HYK_SRMD where RYID=:RYID";
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_SRMDJL(JLBH,MDSL,HFSL,DJR,DJRMC,DJSJ)";
                    query.SQL.Add("values(:JLBH,0,0,:DJR,:DJRMC,:DJSJ)");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_SRMDJLITEM(JLBH,HYID,STATUS)";
                    query.SQL.Add("select :JLBH,H.HYID,0");
                    query.SQL.Add(" from HYK_HYXX H,HYK_GRXX G,HYKDEF D");
                    query.SQL.Add(" where H.HYID=G.HYID and H.HYKTYPE=D.HYKTYPE");
                    query.SQL.Add(" and H.HYKTYPE in (" + pHYKTYPE + ")");
                    query.SQL.Add(" and ((extract(month from G.CSRQ)= " + pKSY);
                    query.SQL.Add(" and extract(day from G.CSRQ)>=" + pKSR + " )");
                    query.SQL.Add(" or (extract(month from G.CSRQ)> " + pKSY + "))");
                    query.SQL.Add(" and ((extract(month from G.CSRQ)= " + pJSY);
                    query.SQL.Add(" and extract(day from G.CSRQ)<=" + pJSR + " )");
                    query.SQL.Add(" or (extract(month from G.CSRQ)< " + pJSY + "))");
                    query.SQL.Add(" and H.KFRYID = " + iDJR);
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "select count(*) from HYK_SRMDJLITEM where JLBH=:JLBH";
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        iMDSL = query.Fields[0].AsInteger;
                    }
                    query.Close();

                    query.SQL.Clear();
                    query.SQL.Text = "update HYK_SRMDJL set MDSL=:MDSL where JLBH=:JLBH";
                    query.ParamByName("MDSL").AsInteger = iMDSL;
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_SRMD(JLBH,HYID,RYID)";
                    query.SQL.Add("select JLBH,HYID,:RYID from HYK_SRMDJLITEM where JLBH=:JLBH");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.ExecSQL();

                    bValid = true;


                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool GetHYMD(int iDJR, string pHYKTYPE)
        {
            bool bValid = false;
            string sDJRMC = string.Empty;
            int iMDSL = 0;
            int _iJLBH = SeqGenerator.GetSeq("HYK_DQWHHYMDJL");
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();


                    query.SQL.Text = "delete from HYK_DQWHHYMD where RYID=:RYID";
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_DQWHHYMDJL(JLBH,MDSL,HFSL,DJR,DJRMC,DJSJ)";
                    query.SQL.Add("values(:JLBH,0,0,:DJR,:DJRMC,:DJSJ)");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_DQWHHYMDJLITEM(JLBH,HYID,STATUS)";
                    query.SQL.Add("select :JLBH,H.HYID,0");
                    query.SQL.Add(" from HYK_HYXX H,HYKDEF D");
                    query.SQL.Add("where H.HYKTYPE=D.HYKTYPE");
                    query.SQL.Add(" and H.HYKTYPE in (" + pHYKTYPE + ")");
                    query.SQL.Add(" and H.KFRYID = " + iDJR);
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "select count(*) from HYK_DQWHHYMDJLITEM where JLBH=:JLBH";
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        iMDSL = query.Fields[0].AsInteger;
                    }
                    query.Close();

                    query.SQL.Clear();
                    query.SQL.Text = "update HYK_DQWHHYMDJL set MDSL=:MDSL where JLBH=:JLBH";
                    query.ParamByName("MDSL").AsInteger = iMDSL;
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_DQWHHYMD(JLBH,HYID,RYID)";
                    query.SQL.Add("select JLBH,HYID,:RYID from HYK_DQWHHYMDJLITEM where JLBH=:JLBH");
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.ExecSQL();

                    bValid = true;


                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool getSRLP(int iDJR, int iHYID, string sLPDM, string sBGDDDM, string sBZ)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    bValid = true;
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    int iDJBH = 0;
                    int iJLBH = 0;
                    int _iJLBH = 0;
                    int iLPID = 0;
                    string sLPMC = string.Empty;
                    string sDJRMC = string.Empty;
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();
                    query.SQL.Text = "select max(JLBH) from HYK_SRLPYDJL where HYID=" + iHYID;
                    query.SQL.Add("and DJSJ>=:SJ");
                    query.ParamByName("SJ").AsDateTime = new DateTime(serverTime.Year, 1, 1);
                    query.Open();
                    if (!query.IsEmpty)
                        iDJBH = query.Fields[0].AsInteger;
                    if (iDJBH <= 0)
                        iJLBH = SeqGenerator.GetSeq("HYK_SRLPYDJL");
                    else
                        iJLBH = iDJBH;
                    query.Close();
                    query.SQL.Text = "select max(JLBH) from HYK_SRMD where RYID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                        _iJLBH = query.Fields[0].AsInteger;
                    query.Close();

                    query.SQL.Text = "begin update HYK_SRLPYDJL set BZ=:BZ,CZDD=:CZDD,LX=:LX where JLBH=:JLBH;";
                    query.SQL.Add("if SQL%NOTFOUND then");
                    query.SQL.Add("insert into HYK_SRLPYDJL(JLBH,HYID,CZDD,DJSJ,DJR,DJRMC,BZ,LX)");
                    query.SQL.Add("values(:JLBH,:HYID,:CZDD,sysdate,:DJR,:DJRMC,:BZ,:LX);end if;end;");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("CZDD").AsString = sBGDDDM;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("BZ").AsString = sBZ;
                    query.ParamByName("LX").AsInteger = 1;
                    query.ExecSQL();
                    query.SQL.Clear();

                    query.SQL.Text = "delete from HYK_SRLPYDJLITEM where JLBH=:JLBH";
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ExecSQL();
                    query.SQL.Clear();

                    query.SQL.Text = "select LPMC,LPID from HYK_JFFHLPXX where LPDM = '" + sLPDM + "'";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        iLPID = query.FieldByName("LPID").AsInteger;
                        sLPMC = query.FieldByName("LPMC").AsString;
                    }
                    query.Close();
                    query.SQL.Clear();
                    query.SQL.Text = "insert into HYK_SRLPYDJLITEM(JLBH,LPID,LPDM,LPMC,LPSL)";
                    query.SQL.Add("values(:JLBH,:LPID,:LPDM,:LPMC,:LPSL)");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("LPID").AsInteger = iLPID;
                    query.ParamByName("LPDM").AsString = sLPDM;
                    query.ParamByName("LPMC").AsString = sLPMC;
                    query.ParamByName("LPSL").AsInteger = 1;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "delete from HYK_SRMD where HYID=:HYID";
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "update HYK_SRMDJLITEM set STATUS=1 where JLBH=:JLBH and HYID=:HYID";
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ExecSQL();

                    bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }
        public static HYXX_Detail GetSRSL(out string msg, int iDJR, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select max(JLBH) as JLBH from HYK_SRMD where RYID=:RYID";
                    query.ParamByName("RYID").AsInteger = iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                        obj._iJLBH = query.FieldByName("JLBH").AsInteger;

                    if (obj._iJLBH > 0)
                    {
                        query.Close();
                        query.SQL.Clear();
                        query.SQL.Text = "select count(*) as MDSL from HYK_SRMDJLITEM where JLBH=:JLBH and STATUS=0";
                        query.ParamByName("JLBH").AsInteger = obj._iJLBH;
                        query.Open();
                        if (!query.IsEmpty)
                            obj.iMDSL = query.FieldByName("MDSL").AsInteger;

                        query.Close();
                        query.SQL.Clear();
                        query.SQL.Text = "select count(*) as HFSL from HYK_SRMDJLITEM where JLBH=:JLBH and STATUS=1";
                        query.ParamByName("JLBH").AsInteger = obj._iJLBH;
                        query.Open();
                        if (!query.IsEmpty)
                            obj.iHFSL = query.FieldByName("HFSL").AsInteger;
                    }
                    else
                        return null;
                    query.Close();
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
            return obj;
        }
        public static HYXX_Detail LoadBZXX(out string msg, int iHYID, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    int iDJBH = 0;
                    query.SQL.Text = "select max(JLBH) from HYK_SRLPYDJL where HYID=" + iHYID;
                    query.SQL.Add("and DJSJ>=:SJ");
                    query.ParamByName("SJ").AsDateTime = new DateTime(serverTime.Year, 1, 1);
                    query.Open();
                    if (!query.IsEmpty)
                        iDJBH = query.Fields[0].AsInteger;
                    query.Close();
                    query.SQL.Text = "select BZ from HYK_SRLPYDJL where JLBH=" + iDJBH;
                    query.Open();
                    if (!query.IsEmpty)
                        obj.sBZ = query.FieldByName("BZ").AsString;
                    else
                        obj.sBZ = "";
                    query.Close();

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
            return obj;
        }

        public static HYXX_Detail LoadVIPBZXX(out string msg, int iHYID, string sDBConnName = "CRMDB")
        {
            msg = string.Empty;
            HYXX_Detail obj = new HYXX_Detail();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "select BZ from VIP_BZXX  where HYID=" + iHYID;
                    query.Open();
                    if (!query.IsEmpty)
                        obj.sBZ = query.FieldByName("BZ").AsString;
                    else
                        obj.sBZ = "";
                    query.Close();
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
            return obj;
        }

        public static bool updateVIPBZ(int iDJR, string sDJRMC, string sBZ, int iHYID)
        {
            bool bValid = false;
            int iID = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    bValid = true;
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    iID = SeqGenerator.GetSeq("VIP_BZXX");
                    query.SQL.Text = "insert into VIP_BZXX(ID,HYID,DJSJ,DJR,DJRMC,BZ)";
                    query.SQL.Add("values(:ID,:HYID,sysdate,:DJR,:DJRMC,:BZ)");
                    query.ParamByName("ID").AsInteger = iID;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("BZ").AsString = sBZ;
                    int i = query.ExecSQL();
                    if (i == 1)
                        bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool updateBZ(int iDJR, string sBZ, int iLX, int iHYID)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    bValid = true;
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    //int iDJBH = 0;
                    int iJLBH = 0;
                    int _iJLBH = 0;
                    string sLPMC = string.Empty;
                    string sDJRMC = string.Empty;
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();   //每填写一次备注就要生成一条新纪录，不能覆盖原来的
                    //query.SQL.Text = "select max(JLBH) from HYK_SRLPYDJL where HYID=" + iHYID;
                    //query.SQL.Add("and DJSJ>=:SJ");
                    //query.ParamByName("SJ").AsDateTime = new DateTime(serverTime.Year, 1, 1);
                    //query.Open();
                    //if (!query.IsEmpty)
                    //    iDJBH = query.Fields[0].AsInteger;
                    //if (iDJBH <= 0)
                    iJLBH = SeqGenerator.GetSeq("HYK_SRLPYDJL");
                    //else
                    //    iJLBH = iDJBH;
                    //query.Close();
                    query.SQL.Text = "select max(JLBH) from HYK_SRMD where RYID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                        _iJLBH = query.Fields[0].AsInteger;
                    query.Close();

                    //query.SQL.Text = "begin update HYK_SRLPYDJL set BZ=:BZ,LX=:LX where JLBH=:JLBH;";
                    //query.SQL.Add("if SQL%NOTFOUND then");
                    query.SQL.Text = "insert into HYK_SRLPYDJL(JLBH,HYID,DJSJ,DJR,DJRMC,BZ,LX)";
                    query.SQL.Add("values(:JLBH,:HYID,sysdate,:DJR,:DJRMC,:BZ,:LX)");   //;end if;end;
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("BZ").AsString = sBZ;
                    query.ParamByName("LX").AsInteger = iLX;
                    query.ExecSQL();
                    query.SQL.Clear();

                    if (iLX == 1)
                    {
                        query.SQL.Text = "delete from HYK_SRMD where HYID=:HYID";
                        query.ParamByName("HYID").AsInteger = iHYID;
                        query.ExecSQL();

                        query.SQL.Clear();
                        query.SQL.Text = "update HYK_SRMDJLITEM set STATUS=1 where JLBH=:JLBH and HYID=:HYID";
                        query.ParamByName("JLBH").AsInteger = _iJLBH;
                        query.ParamByName("HYID").AsInteger = iHYID;
                        query.ExecSQL();
                    }
                    bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool updateDHWHBZ(int iDJR, string sBZ, int iHYID)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    bValid = true;
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    //int iDJBH = 0;
                    int iJLBH = 0;
                    int _iJLBH = 0;
                    string sDJRMC = string.Empty;
                    query.SQL.Text = "select PERSON_NAME from RYXX where PERSON_ID=" + iDJR;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        sDJRMC = query.FieldByName("PERSON_NAME").AsString;
                    }
                    query.Close();
                    //query.SQL.Text = "select JLBH from HYK_DQWHHYHFJL where HYID=" + iHYID;
                    //query.Open();
                    //if (!query.IsEmpty)
                    //    iDJBH = query.Fields[0].AsInteger;
                    //if (iDJBH <= 0)
                    iJLBH = SeqGenerator.GetSeq("HYK_DQWHHYHFJL");
                    //else
                    //    iJLBH = iDJBH;
                    //query.Close();
                    query.SQL.Text = "select JLBH from HYK_DQWHHYMD where HYID=" + iHYID;
                    query.Open();
                    if (!query.IsEmpty)
                        _iJLBH = query.Fields[0].AsInteger;
                    query.Close();

                    //query.SQL.Text = "begin update HYK_DQWHHYHFJL set BZ=:BZ where JLBH=:JLBH;";
                    //query.SQL.Add("if SQL%NOTFOUND then");
                    query.SQL.Text = "insert into HYK_DQWHHYHFJL(JLBH,HYID,DJSJ,DJR,DJRMC,BZ)";
                    query.SQL.Add("values(:JLBH,:HYID,sysdate,:DJR,:DJRMC,:BZ)");  //;end if;end;
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("DJR").AsInteger = iDJR;
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("BZ").AsString = sBZ;
                    query.ExecSQL();
                    query.SQL.Clear();
                    query.SQL.Text = "delete from HYK_DQWHHYMD where HYID=:HYID";
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ExecSQL();

                    query.SQL.Clear();
                    query.SQL.Text = "update HYK_DQWHHYMDJLITEM set STATUS=1 where JLBH=:JLBH and HYID=:HYID";
                    query.ParamByName("JLBH").AsInteger = _iJLBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ExecSQL();
                    bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool updateXYDJ(int iHYID, int iXYDJID)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "update HYK_HYXX set XYDJID=:XYDJID where HYID=:HYID";
                    query.ParamByName("XYDJID").AsInteger = iXYDJID;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    int i = query.ExecSQL();
                    if (i == 1)
                        bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }

        public static bool updateXYMD(int iHYID)
        {
            bool bValid = false;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    query.SQL.Text = "begin update HYK_HYXYMD set HYID=HYID where HYID=:HYID;";
                    query.SQL.Add(" if SQL%NOTFOUND then");
                    query.SQL.Add(" insert into HYK_HYXYMD(HYID) values(:HYID);end if;end;");
                    query.ParamByName("HYID").AsInteger = iHYID;
                    int i = query.ExecSQL();
                    if (i == 1)
                        bValid = true;
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return bValid;

        }


        public static bool CheckMenuPermit(int iPersonID, int iMenuID)
        {

            MenuPermit res = MenuPermits.Find((MenuPermit one) => { return one.iPERSON_ID == iPersonID && one.iMSG_ID == iMenuID; });

            return res != null;
        }

        public static string FillCITY(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<CITY> lst = new List<CITY>();
            query.SQL.Text = "select ID,NAME from CITY";
            query.SQL.Add(" order by ID");
            query.Open();
            while (!query.Eof)
            {
                CITY _item = new CITY();
                _item.iCITYID = query.FieldByName("ID").AsInteger;
                _item.sCSMC = query.FieldByName("NAME").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }
        public static string FillJPJC(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<JCDEF> lst = new List<JCDEF>();
            query.SQL.Text = "select JC,MC from MOBILE_JCDEF";
            query.SQL.Add(" order by JC");
            query.Open();
            while (!query.Eof)
            {
                JCDEF _item = new JCDEF();
                _item.iJC = query.FieldByName("JC").AsInteger;
                _item.sMC = query.FieldByName("MC").AsString;
                lst.Add(_item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string FillJJR(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<JJR> lst = new List<JJR>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select * from CR_JJRDEF";
                query.SQL.Add(" order by JJRID");
                query.Open();
                while (!query.Eof)
                {
                    JJR _item = new JJR();
                    _item.iJJRID = query.FieldByName("JJRID").AsInteger;
                    _item.sJJRMC = query.FieldByName("JJRMC").AsString;
                    lst.Add(_item);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string GetJFDHLPDYD(out string msg, int iGZID)
        {
            msg = string.Empty;
            CrmProc.GTPT.GTPT_WXCJDYD_Proc obj = new CrmProc.GTPT.GTPT_WXCJDYD_Proc();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select GZID from WX_JFFLLPDYD where GZID=:GZID and status=2";
                    query.ParamByName("GZID").AsInteger = iGZID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iGZID = query.FieldByName("GZID").AsInteger;
                    }
                    query.Close();
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
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetCXHD(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<CXHD> lst = new List<CXHD>();
            query.SQL.Text = "select * from CXHDDEF where 1=1";
            query.Open();
            while (!query.Eof)
            {
                CXHD item = new CXHD();
                item.iCXID = query.FieldByName("CXID").AsInteger;
                item.sCXZT = query.FieldByName("CXZT").AsString;
                item.sCXNR = query.FieldByName("CXNR").AsString;
                item.dKSSJ = FormatUtils.DatetimeToString(query.FieldByName("KSSJ").AsDateTime);
                item.dJSSJ = FormatUtils.DatetimeToString(query.FieldByName("JSSJ").AsDateTime);
                item.iZXR = query.FieldByName("ZXR").AsInteger;
                lst.Add(item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetYHQDEF_CXHD(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<CXHDYHQ> lst = new List<CXHDYHQ>();
            query.SQL.Text = "select * from YHQDEF_CXHD where CXID=:CXID and YHQID=:YHQID";
            query.ParamByName("CXID").AsInteger = param.iCXID;
            query.ParamByName("YHQID").AsInteger = param.iYHQID;
            query.Open();
            while (!query.Eof)
            {
                CXHDYHQ item = new CXHDYHQ();
                item.iCXID = query.FieldByName("CXID").AsInteger;
                item.iYHQID = query.FieldByName("YHQID").AsInteger;
                item.dYHQSYJSRQ = FormatUtils.DateToString(query.FieldByName("YHQSYJSRQ").AsDateTime);
                item.iYXQTS = query.FieldByName("YXQTS").AsInteger;
                lst.Add(item);
                query.Next();
            }
            return JsonConvert.SerializeObject(lst);
        }

        public static string GetSPTJINX(out string msg, CyQuery query, CRMLIBASHX param)
        {
            param.sFIELD = "INX";
            param.sTABLENAME = "WX_SP";
            return GetSelfInx(out msg, query, param);
            //msg = string.Empty;
            //query.SQL.Text = "select max(INX) INX from WX_SP ";
            //query.Open();
            //INX obj = new INX();
            //obj.iINX = query.FieldByName("INX").AsInteger;
            //return JsonConvert.SerializeObject(obj);
        }

        public static string GetYYFWDEFINX(out string msg, CyQuery query, CRMLIBASHX param)
        {
            param.sFIELD = "INX";
            param.sTABLENAME = "MOBILE_YDFWDEF";
            return GetSelfInx(out msg, query, param);
            //msg = string.Empty;
            //query.SQL.Text = "select max(INX) INX from MOBILE_YDFWDEF";
            //query.Open();
            //INX obj = new INX();
            //obj.iINX = query.FieldByName("INX").AsInteger;
            //return JsonConvert.SerializeObject(obj);
        }

        public static void GetNDXH(out string msg, ref INX obj, int iND)
        {
            msg = string.Empty;
            List<object> lst = new List<object>();
            string sql = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                query.SQL.Text = "select MAX(YXHDID) XH from CR_YXHDDEF WHERE ND= " + iND;
                query.Open();
                while (!query.Eof)
                {

                    obj.iINX = query.FieldByName("XH").AsInteger;
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            //return JsonConvert.SerializeObject(lst);
        }
        public static string GetSelfInx(out string msg, CyQuery query, CRMLIBASHX param)// string sTABLENAME, string sFIELD)
        {
            msg = string.Empty;
            INX obj = new INX();
            query.SQL.Text = "select max(" + param.sFIELD + ") INX from " + param.sTABLENAME;
            query.Open();
            obj.iINX = query.FieldByName("INX").AsInteger;
            return JsonConvert.SerializeObject(obj);
        }
        public static string GetDbSystemField(string sDbSystemName, int iLX)
        {
            string sReturn = string.Empty;
            if (sDbSystemName == "ORACLE")
            {
                if (iLX == 0)
                {
                    sReturn = "sysdate";
                }
                else if (iLX == 1)
                {
                    sReturn = "substr";
                }
                else if (iLX == 2)
                {
                    sReturn = "nvl";
                }
                else if (iLX == 3)
                {
                    sReturn = "trunc(sysdate)";
                }
                else if (iLX == 4)
                {
                    sReturn = "from dual";
                }
                else if (iLX == 5)
                {
                    sReturn = "length";
                }
                else if (iLX == 6)
                {
                    sReturn = "to_char(";
                }
            }
            else if (sDbSystemName == "SYBASE")
            {
                if (iLX == 0)
                {
                    sReturn = "getdate()";
                }
                else if (iLX == 1)
                {
                    sReturn = "substring";
                }
                else if (iLX == 2)
                {
                    sReturn = "isnull";
                }
                else if (iLX == 3)
                {
                    sReturn = "convert(datetime,getdate(),102)";
                }
                else if (iLX == 4)
                {
                    sReturn = " ";
                }
                else if (iLX == 5)
                {
                    sReturn = "datalength";
                }
                else if (iLX == 6)
                {
                    sReturn = "convert(char,";
                }
            }
            return sReturn;
        }

        public static string UploadFile2(HttpContext context, CRMLIBASHX obj)
        {
            string msg;
            string dir = context.Server.MapPath("~/upload/" + obj.sDir + "/");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (GlobalVariables.FTPConfig.Count > 0)
            {
                if (!ftpIsExistsFile(obj.sDir, GlobalVariables.FTPConfig[0].sURL, GlobalVariables.FTPConfig[0].sUSER, GlobalVariables.FTPConfig[0].sPSWD))
                {
                    Pub.Log4Net.WriteLog(LogLevel.INFO, "可能没执行1");

                    MakeDir(out msg, obj.sDir, GlobalVariables.FTPConfig[0].sURL, GlobalVariables.FTPConfig[0].sUSER, GlobalVariables.FTPConfig[0].sPSWD);
                }
            }
            Pub.Log4Net.WriteLog(LogLevel.INFO, "可能没执行3");

            List<string> fileNameList = new List<string>();
            List<string> ftpNameList = new List<string>();
            int i = 1;
            foreach (string f in context.Request.Files.AllKeys)
            {
                Pub.Log4Net.WriteLog(LogLevel.INFO, "可能没执行4");

                HttpPostedFile file = context.Request.Files[f];
                string fileName = LibProc.ConvertDateTimeInt(DateTime.Now).ToString() + "_" + i.ToString() + ".jpg";
                file.SaveAs(dir + fileName);
                fileNameList.Add("/upload/" + obj.sDir + "/" + fileName);
                if (GlobalVariables.FTPConfig.Count > 0)
                {
                    Pub.Log4Net.WriteLog(LogLevel.INFO, "可能没执行5");

                    FileInfo fileNew = new FileInfo(dir + fileName);
                    if (UploadFileStart(out msg, fileNew, obj.sDir, GlobalVariables.FTPConfig[0].sURL, GlobalVariables.FTPConfig[0].sUSER, GlobalVariables.FTPConfig[0].sPSWD))
                    {
                        ftpNameList.Add(@"http://" + GlobalVariables.FTPConfig[0].sIP_PUB + "/" + obj.sDir + "/" + fileName);
                    }

                }
                i++;
            }
            if (ftpNameList.Count > 0)
                return ftpNameList[0];
            else
                return ""; ;
            //return JsonConvert.SerializeObject(ftpNameList);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileinfo">需要上传的文件</param>
        /// <param name="targetDir">目标路径</param>
        /// <param name="hostname">ftp地址</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        public static bool UploadFileStart(out string msg, FileInfo fileinfo, string targetDir, string hostname, string username, string password)
        {
            msg = string.Empty;
            string URI = "FTP://" + hostname + "/" + targetDir + "/" + fileinfo.Name;
            System.Net.FtpWebRequest ftp = GetRequest(URI, username, password);
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            ftp.ContentLength = fileinfo.Length;
            try
            {
                //T-- 如果已存在该文件、进行删除操作
                string tp_URI = "FTP://" + hostname + "/" + targetDir + "/" + fileinfo.Name;
                System.Net.FtpWebRequest ftp1 = GetRequest(tp_URI, username, password);
                ftp1 = GetRequest(tp_URI, username, password);
                ftp1.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                ftp1.GetResponse();

            }
            catch (Exception ex)
            {
                //msg = ex.Message;
                //Log4Net.E(msg);
            }
            finally
            {
                const int BufferSize = 2048;
                byte[] content = new byte[BufferSize - 1 + 1];
                int dataRead;
                using (FileStream fs = fileinfo.OpenRead())
                {
                    try
                    {
                        using (Stream rs = ftp.GetRequestStream())
                        {
                            do
                            {
                                dataRead = fs.Read(content, 0, BufferSize);
                                rs.Write(content, 0, dataRead);
                            } while (!(dataRead < BufferSize));
                            rs.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        Log4Net.E(msg);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            return (msg.Length == 0);
        }

        public static string SaveBase64Pic(string b64, string targetDir, string prefix)
        {
            string local = HttpRuntime.AppDomainAppPath + "Upload\\" + targetDir + "\\";
            string filename = prefix + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string web = "../../../Upload/" + targetDir + "/" + filename;

            System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(320, 240);
            string[] rows = b64.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < rows.Length; i++)
            {
                string[] col = rows[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < col.Length; j++)
                {
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(Convert.ToInt32(col[j]));
                    System.Drawing.Color reColor = System.Drawing.Color.FromArgb(255, color);
                    bmap.SetPixel(j, i, reColor);
                }
            }
            System.IO.DirectoryInfo dirPath = new System.IO.DirectoryInfo(local);
            if (!dirPath.Exists)
            {
                dirPath.Create();
            }
            local += filename;
            bmap.Save(local);





            //byte[] arr = Convert.FromBase64String(b64);
            //MemoryStream ms = new MemoryStream(arr);
            //Bitmap bmp = new Bitmap(ms);                 
            //if (!Directory.Exists(local))
            //    Directory.CreateDirectory(local);
            //local += "\\" + filename;
            //bmp.Save(local, System.Drawing.Imaging.ImageFormat.Jpeg);
            //ms.Close();
            return JsonConvert.SerializeObject(web);
        }

        /// <summary>
        /// 判断ftp服务器上该目录是否存在
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="ftpHostIP"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        //判断文件的目录是否存,不存则创建

        private static bool ftpIsExistsFile(string dirName, string ftpHostIP, string username, string password)
        {
            bool flag = false;
            StreamReader reader = null;
            System.Net.FtpWebResponse response = null;
            try
            {
                //由登录ftp判断文件夹到直接到指定文件夹，如果内容为空则为未创建直接可以创建
                //由于两级目录，创建做了判断
                string uri = "ftp://" + ftpHostIP + "/" + dirName;
                //uri = "ftp://" + ftpHostIP;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                //ftp.Method=  System.Net .WebRequestMethods.Ftp.ListDirectory ;
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                response = (System.Net.FtpWebResponse)ftp.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                if (line != null)
                {
                    flag = true;
                }
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (response != null)
                    response.Close();
            }
            return flag;
        }
        //private static bool ftpIsExistsFile(string dirName, string ftpHostIP, string username, string password)
        //{
        //    bool flag = false;
        //    StreamReader reader = null;
        //    System.Net.FtpWebResponse response = null;
        //    try
        //    {
        //        string uri = "ftp://" + ftpHostIP; //+ "/" + dirName;
        //        System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
        //        ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectory;
        //        response = (System.Net.FtpWebResponse)ftp.GetResponse();
        //        reader = new StreamReader(response.GetResponseStream());
        //        string line = reader.ReadLine();
        //        while (line != null)
        //        {
        //            if (line == dirName)
        //            {
        //                flag = true;
        //                break;
        //            }
        //            line = reader.ReadLine();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        flag = false;
        //    }
        //    finally
        //    {
        //        if (reader != null)
        //            reader.Close();
        //        if (response != null)
        //            response.Close();
        //    }
        //    return flag;
        //}

        /// 在ftp服务器上创建目录
        /// </summary>
        /// <param name="dirName">创建的目录名称</param>
        /// <param name="ftpHostIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// 


        public static void MakeDir(out string msg, string dirName, string ftpHostIP, string username, string password)
        {
            msg = string.Empty;
            List<string> dirList = new List<string>(dirName.Split('/'));
            string uri = "ftp://" + ftpHostIP;
            //目录不是末级时候，先创建上一级目录，不进行报错，然后再创建下一级，规避一下问题
            for (int i = 0; i < dirList.Count; i++)
            {
                try
                {
                    uri += "/" + dirList[i];
                    System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                    ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;
                    System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)ftp.GetResponse();
                    response.Close();
                }
                catch (Exception ex)
                {
                    if (i == dirList.Count - 1)
                    {
                        msg = ex.Message;
                        Log4Net.E(msg);
                        Pub.Log4Net.WriteLog(LogLevel.INFO, "可能没执行2" + msg);

                    }
                }

            }
        }

        //public static void MakeDir(out string msg, string dirName, string ftpHostIP, string username, string password)
        //{
        //    msg = string.Empty;
        //    try
        //    {
        //        string uri = "ftp://" + ftpHostIP + "/" + dirName;
        //        System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
        //        ftp.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory;

        //        System.Net.FtpWebResponse response = (System.Net.FtpWebResponse)ftp.GetResponse();
        //        response.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //}

        private static System.Net.FtpWebRequest GetRequest(string URI, string username, string password)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            System.Net.FtpWebRequest result = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(URI);
            //提供身份验证信息
            result.Credentials = new System.Net.NetworkCredential(username, password);
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = false;
            return result;
        }

        public static void Send_Email(string toMailAddress, string sYJZT, string sYJNR)
        {
            //T-- 发送邮件
            string senderServerIp = System.Configuration.ConfigurationManager.AppSettings["EmailServerAddress"];   //"smtp.qq.com";          
            string fromMailAddress = System.Configuration.ConfigurationManager.AppSettings["CompanyEmail"];  //"1078316522@qq.com";
            string mailUsername = fromMailAddress.Substring(0, fromMailAddress.IndexOf("@"));
            string mailPassword = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"];
            string mailPort = System.Configuration.ConfigurationManager.AppSettings["EmailPort"];
            //string toMailAddress = "";
            MyEmail email = new MyEmail(senderServerIp, toMailAddress, fromMailAddress, sYJZT, sYJNR, mailUsername, mailPassword, mailPort, false, false);
            email.Send();
        }

        public static string SendSMS(string msg, CyQuery query, CRMLIBASHX param)
        {
            SMS.wmgwSoapClient soap = new SMS.wmgwSoapClient();
            string ret = soap.MongateSendSubmit(GlobalVariables.SYSConfig.sSMSUser, GlobalVariables.SYSConfig.sSMSPass, param.sSJHM, param.sDM, 1, "", "");
            if (ret.Length >= 15 && ret.Length <= 25)
                return JsonConvert.SerializeObject("ok");
            else
                return JsonConvert.SerializeObject(ret);
        }
        public static string getHYXXXMMC(out string msg, int iXMID)
        {
            msg = string.Empty;
            HYXXXMDY obj = new HYXXXMDY();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * FROM  HYXXXMDEF where 1=1 ";
                    query.SQL.Add(" and XMID=" + iXMID);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        obj.iXMID = query.FieldByName("XMID").AsInteger;
                        obj.iXMLX = query.FieldByName("XMLX").AsInteger;
                        obj.sNR = query.FieldByName("NR").AsString;
                    }
                    query.Close();
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
            return JsonConvert.SerializeObject(obj);
        }

        public static string GetRCLResult(out string msg, CyQuery query, CRMLIBASHX param)//string dPDRQ)
        {
            msg = string.Empty;
            LPXX obj = new LPXX();
            query.SQL.Text = "select LPID from HYK_JFFHLPJXC where RQ=:RQ";
            query.ParamByName("RQ").AsDateTime = FormatUtils.StrToDate(param.dPDRQ);
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iLPID = query.FieldByName("LPID").AsInteger;
            }
            return JsonConvert.SerializeObject(obj);
        }
        public static string GetCJDYD(out string msg, CyQuery query, CRMLIBASHX param)// int iGZID)
        {
            msg = string.Empty;
            GTPT_WXCJDYD_Proc obj = new GTPT_WXCJDYD_Proc();
            query.SQL.Text = "select GZID from MOBILE_LPFFGZDYD where GZID=:GZID and status=2";
            query.ParamByName("GZID").AsInteger = param.iGZID;
            query.Open();
            if (!query.IsEmpty)
            {
                obj.iGZID = query.FieldByName("GZID").AsInteger;
            }
            return JsonConvert.SerializeObject(obj);
        }
        //public static string GetSydhdy1(out string msg, int iID)
        //{
        //    msg = string.Empty;
        //    CrmProc.GTPT.GTPT_WXSYDHDY obj = new CrmProc.GTPT.GTPT_WXSYDHDY();
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        try
        //        {
        //            query.SQL.Text = "select ID from WX_NAVIGATIONDEF where ID=:ID";
        //            query.ParamByName("ID").AsInteger = iID;
        //            query.Open();
        //            if (!query.IsEmpty)
        //            {
        //                obj.iID = query.FieldByName("ID").AsInteger;
        //            }
        //            query.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is MyDbException)
        //                throw e;
        //            else
        //                msg = e.Message;
        //            throw new MyDbException(e.Message, query.SqlText);
        //        }
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return JsonConvert.SerializeObject(obj);
        //}

        public static string GetLPFFGZLPList(out string msg, int iJLBH, string sCZDD)
        {
            msg = string.Empty;
            List<LPFFGZ_LP> lst = new List<LPFFGZ_LP>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select A.*,K.KCSL from HYK_LPFFGZ_LP A,HYK_JFFHLPKC K where JLBH=" + iJLBH;
                    query.SQL.Add(" and A.LPID = K.LPID(+) and (K.BGDDDM=:CZDD or K.BGDDDM is null)");
                    query.ParamByName("CZDD").AsString = sCZDD;
                    query.Open();
                    while (!query.Eof)
                    {
                        LPFFGZ_LP item = new LPFFGZ_LP();
                        item.iLPID = query.FieldByName("LPID").AsInteger;
                        SetLPXX(item, item.iLPID);
                        item.iLPGRP = query.FieldByName("LPGRP").AsInteger;
                        item.fLPJF = query.FieldByName("LPJF").AsFloat;
                        item.fLPJE = query.FieldByName("LPJE").AsFloat;
                        item.fKCSL = query.FieldByName("KCSL").AsFloat;
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

            return JsonConvert.SerializeObject(lst);
        }
        public static string GetUserOpenid(out string msg)
        {
            msg = string.Empty;
            string sql = string.Empty;
            List<Object> lst = new List<Object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            try
            {
                query.Close();
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                DateTime lastTime = serverTime;
                DateTime firstTime = serverTime.AddDays(-2);
                string strsql = " select DJSJ,OPENID from WX_ZLOG W  ";
                strsql += " where W.DJSJ>=:DJSJ1 ";
                strsql += " and W.DJSJ<:DJSJ2 ";
                strsql += " union ";
                strsql += " select DJSJ,OPENID from WX_WORD_ZLOG W  ";
                strsql += " where W.DJSJ>=:DJSJ11 ";
                strsql += " and W.DJSJ<:DJSJ22 ";
                //query.SQL.Text = " select DJSJ,OPENID from WX_WORD_ZLOG W ";
                //query.SQL.Add(" where W.DJSJ>=:DJSJ1 ");
                //query.SQL.Add(" and W.DJSJ<:DJSJ2 ");
                query.SQL.Text = strsql;
                query.ParamByName("DJSJ1").AsDateTime = firstTime;
                query.ParamByName("DJSJ2").AsDateTime = lastTime;
                query.ParamByName("DJSJ11").AsDateTime = firstTime;
                query.ParamByName("DJSJ22").AsDateTime = lastTime;
                query.Open();
                while (!query.Eof)
                {
                    GTPT_WXUser_Proc obj = new GTPT_WXUser_Proc();
                    obj.sOPENID = query.FieldByName("OPENID").AsString;
                    lst.Add(obj);
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
                throw new MyDbException(e.Message, query.SqlText);

            }
            conn.Close();
            return JsonConvert.SerializeObject(lst);
        }
        public static string GetSQDXX(out string msg, int iJLBH, int iBJ_CZK)
        {
            msg = string.Empty;
            List<Object> lst = new List<Object>();
            HYKGL_HYKLYSQ_Proc obj = new HYKGL_HYKLYSQ_Proc();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select W.*,B.BGDDMC from CARDLQSQJL W,HYK_BGDD B ";
                    query.SQL.Add(" where W.BGDDDM_BR=B.BGDDDM(+)");
                    query.SQL.Add(" and W.JLBH=" + iJLBH);
                    if (iBJ_CZK == 1)
                        query.SQL.Text = query.SQL.Text.Replace("CARDLQSQJL", "MZK_CARDLQSQJL");
                    query.Open();
                    if (!query.IsEmpty)
                    {

                        obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                        obj.sBGDDDM_BR = query.FieldByName("BGDDDM_BR").AsString;
                        obj.sBGDDMC_BR = query.FieldByName("BGDDMC").AsString;
                        obj.iHYKSL = query.FieldByName("HYKSL").AsInteger;
                        obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        obj.sBZ = query.FieldByName("BZ").AsString;
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                        obj.sBZ = query.FieldByName("BZ").AsString;
                        obj.iDJLX = query.FieldByName("DJLX").AsInteger;
                        lst.Add(obj);
                    }
                    if (lst.Count == 1)
                    {
                        query.SQL.Text = "select I.*,D.HYKNAME from CARDLQSQJLITEM I,HYKDEF D where I.JLBH=" + iJLBH;
                        query.SQL.Add("and I.HYKTYPE=D.HYKTYPE");
                        if (iBJ_CZK == 1)
                            query.SQL.Text = query.SQL.Text.Replace("CARDLQSQJLITEM", "MZK_CARDLQSQJLITEM");
                        query.Open();
                        while (!query.Eof)
                        {
                            HYKGL_HYKLYSQ_Proc.HYKGL_LYSQItem item = new HYKGL_HYKLYSQ_Proc.HYKGL_LYSQItem();
                            obj.itemTable.Add(item);
                            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                            item.iHYKSL = query.FieldByName("HYKSL").AsInteger;
                            query.Next();
                        }

                    }
                    query.Close();
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
            return JsonConvert.SerializeObject(obj);
        }
        public static string GetKCKSL(out string msg, int iHYKTYPE, int iBJ_CZK)
        {
            msg = string.Empty;
            List<Object> lst = new List<Object>();
            KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select count(*) as SL from HYKCARD where STATUS=0 and HYKTYPE= " + iHYKTYPE;
                    if (iBJ_CZK == 1)
                        query.SQL.Text = query.SQL.Text.Replace("HYKCARD", "MZKCARD");
                    query.Open();
                    if (!query.IsEmpty)
                    {

                        obj.iSL = query.FieldByName("SL").AsInteger;
                    }
                    query.Close();
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
            return JsonConvert.SerializeObject(obj);
        }


        public static string GetZQMX(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            List<Object> lst = new List<Object>();
            MZKGL.MZKGL_MZKFS.findkd ZKMX = new MZKGL.MZKGL_MZKFS.findkd();
            ZKMX.fYSJE = param.fCZJE;
            query.SQL.Text = " select G.XSJE_BEGIN ,G.XSJE_END,G.ZSBL,G.ZSBL*:pXSJE Y_ZSBL,G.YHQTYPE,D.YHQMC,G.YXQTS";
            query.SQL.Add(" from MZK_ZKGZ G,YHQDEF D where G.YHQTYPE=D.YHQID and G.XSJE_BEGIN <=:pXSJE and G.XSJE_END>:pXSJE");
            query.ParamByName("pXSJE").AsFloat = param.fCZJE;
            query.Open();
            if (!query.IsEmpty)
            {
                ZKMX.ZKL = query.FieldByName("ZSBL").AsDecimal;
                ZKMX.ZKJE = query.FieldByName("Y_ZSBL").AsDecimal;
                ZKMX.ZKYHQLX = query.FieldByName("YHQTYPE").AsInteger;
                ZKMX.ZKYHQMC = query.FieldByName("YHQMC").AsString;
                ZKMX.ZKYHQTS = query.FieldByName("YXQTS").AsInteger;
            }
            else
            {
                ZKMX.ZKL = -1;
                ZKMX.ZKJE = 0;
                ZKMX.ZKYHQLX = -1;
                ZKMX.ZKYHQMC = string.Empty;
                ZKMX.ZKYHQTS = 0;
            }
            query.Close();
            query.SQL.Text = " select CKJE_BEGIN ,CKJE_END,ZSBL,ZSBL*:pXSJE Y_ZSBL,ZSJF";
            query.SQL.Add(" from MZK_CKSJFGZ where CKJE_BEGIN <=:pXSJE and CKJE_END>=:pXSJE");
            query.ParamByName("pXSJE").AsFloat = param.fCZJE;
            query.Open();
            if (!query.IsEmpty)
            {
                if (query.FieldByName("ZSBL").AsDecimal > 0)
                {
                    ZKMX.ZKLPBL = query.FieldByName("ZSBL").AsDecimal;
                    ZKMX.ZKLPJE = query.FieldByName("Y_ZSBL").AsDecimal;
                }
                else
                {
                    ZKMX.ZKLPBL = query.FieldByName("ZSBL").AsDecimal;
                    ZKMX.ZKLPJE = query.FieldByName("ZSJF").AsDecimal;
                }

            }
            else
            {
                ZKMX.ZKLPJE = 0;
                ZKMX.ZKLPBL = 0;
            }
            query.Close();
            return JsonConvert.SerializeObject(ZKMX);
        }
        public static string SearchBackCardData(out string msg, CyQuery query, CRMLIBASHX param)
        {
            msg = string.Empty;
            CRMLIBASHX obj = new CRMLIBASHX();
            query.SQL.Text = " select M.JLBH,T.CZKHM  from  MZK_SKJL M,MZK_SKJLITEM T where M.JLBH=T.JLBH and M.FS=2 and T.CZKHM in";
            query.SQL.Add("  ( select S.CZKHM from MZK_SKJL L, MZK_SKJLITEM S where L.JLBH=S.JLBH and L.FS=1 and S.JLBH=:JLBH)");
            query.SQL.Add("  and M.JLBH>:JLBH");
            query.ParamByName("JLBH").AsInteger = param.iSKJLBH;
            query.Open();
            while (!query.Eof)
            {
                obj.sData += query.FieldByName("JLBH").AsInteger.ToString() + ",";
                query.Next();
            }
            query.Close();

            return JsonConvert.SerializeObject(obj);
        }
        public static string GetWXCARDData(out string msg, CyQuery query, CRMLIBASHX obj)
        {
            msg = string.Empty;
            string cardid = System.Configuration.ConfigurationManager.AppSettings["card_id"].ToString();
            GTPT_WXKBHYKTF_Proc item = new GTPT_WXKBHYKTF_Proc();
            query.SQL.Text = "select S.QRCODEURL,S.CONTENT,B.PUBLICID,B.TITLE,B.CARDID from WX_KLXDEF B,WX_KBHYKTF S";
            query.SQL.Add("where B.CARDID=S.CARDID(+) and B.CARDID='" + cardid + "'");
            query.Open();
            if (!query.IsEmpty)
            {
                item.sQRCODEURL = query.FieldByName("QRCODEURL").AsString;
                item.sCONTENT = query.FieldByName("CONTENT").AsString;
                item.sCARDID = query.FieldByName("CARDID").AsString;
                item.sCARDMC = query.FieldByName("TITLE").AsString;
                item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            }
            return JsonConvert.SerializeObject(item);
        }

        //写卡程序
        public static string LoginCRM(out string msg, CyQuery query, CRMLIBASHX param)//string user, string pass, out int ryid, out string rymc
        {
            msg = string.Empty;
            RYXX obj = new RYXX();
            query.SQL.Text = "select X.*,R.PERSON_NAME from XTCZY X,RYXX R where X.PERSON_ID=R.PERSON_ID and R.RYDM=:RYDM";
            query.ParamByName("RYDM").AsString = param.sRYDM;
            query.Open();
            if (query.IsEmpty)
                msg = "人员代码不存在";
            else
            {
                string pass_db = EncryptUtils.DelphiDecrypt((byte[])query.FieldByName("LOGIN_PASSWORD").Value);
                if (param.sPASSWORD != pass_db.Trim())
                    msg = "密码错误";
                else
                {
                    obj.iRYID = query.FieldByName("PERSON_ID").AsInteger;
                    obj.sRYMC = query.FieldByName("PERSON_NAME").AsString;
                }
            }
            return JsonConvert.SerializeObject(obj);
        }
        public static bool GetJKJL(out string msg, CyQuery query, CRMLIBASHX param, int jlbh, bool czk, out JKJL jkjl)
        {
            msg = string.Empty;
            jkjl = new JKJL();
            string tbl = czk ? "MZK_JKJL" : "HYKJKJL";
            query.SQL.Text = "select J.*,D.HYKNAME,B.BGDDMC,D.BJ_CZK from " + tbl + " J,HYKDEF D,HYK_BGDD B";
            query.SQL.Add(" where J.JLBH=:JLBH and J.HYKTYPE=D.HYKTYPE and J.BGDDDM=B.BGDDDM");
            query.ParamByName("JLBH").AsInteger = jlbh;
            query.Open();
            if (query.IsEmpty)
                msg = "建卡单号不存在";
            else
            {
                //int czk = query.FieldByName("BJ_CZK").AsInteger;
                jkjl.iJLBH = query.FieldByName("JLBH").AsInteger;
                jkjl.iJKFS = query.FieldByName("JKFS").AsInteger;
                jkjl.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                jkjl.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                jkjl.sZY = query.FieldByName("ZY").AsString;
                jkjl.sCZKHM_BEGIN = query.FieldByName("CZKHM_BEGIN").AsString;
                jkjl.sCZKHM_END = query.FieldByName("CZKHM_END").AsString;
                jkjl.iJKSL = query.FieldByName("JKSL").AsInteger;
                jkjl.fQCYE = query.FieldByName("QCYE").AsFloat;
                jkjl.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                jkjl.iJKWJCS = query.FieldByName("JKWJCS").AsInteger;
                jkjl.iBGR = query.FieldByName("BGR").AsInteger;
                jkjl.sBGRMC = query.FieldByName("BGRMC").AsString;
                jkjl.fPDJE = query.FieldByName("PDJE").AsFloat;
                jkjl.iBJ_CZK = query.FieldByName("BJ_CZK").AsInteger;
                jkjl.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                jkjl.iDJR = query.FieldByName("DJR").AsInteger;
                jkjl.sDJRMC = query.FieldByName("DJRMC").AsString;
                jkjl.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                jkjl.iZXR = query.FieldByName("ZXR").AsInteger;
                jkjl.sZXRMC = query.FieldByName("ZXRMC").AsString;
                jkjl.iSTATUS = query.FieldByName("STATUS").AsInteger;
                jkjl.iDJLX = query.FieldByName("DJLX").AsInteger;
                jkjl.dYXQ = FormatUtils.DatetimeToString(query.FieldByName("YXQ").AsDateTime);
                jkjl.iXKSL = query.FieldByName("XKSL").AsInteger;
                jkjl.iFXDWID = query.FieldByName("FXDWID").AsInteger;

                jkjl.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                jkjl.sBGDDMC = query.FieldByName("BGDDMC").AsString;

                query.Close();
                query.SQL.Text = "select * from " + tbl + "ITEM where JLBH=:JLBH";
                query.ParamByName("JLBH").AsInteger = jlbh;
                query.Open();
                while (!query.Eof)
                {
                    JKJLITEM obj = new JKJLITEM();
                    obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                    obj.sCDNR = query.FieldByName("CDNR").AsString;
                    obj.sCDNR = DecCDNR(obj.sCDNR);
                    //if (czk)
                    //    obj.sCDNR = DecCDNR(obj.sCDNR);
                    //else
                    //    obj.sCDNR = DesDecryptCardTrackSecondly(obj.sCDNR, CrmServerPlatform.PubData.DesKey);
                    obj.fJE = query.FieldByName("JE").AsFloat;
                    obj.iBJ_ZK = query.FieldByName("BJ_ZK").AsInteger;
                    obj.dXKRQ = FormatUtils.DatetimeToString(query.FieldByName("XKRQ").AsDateTime);
                    jkjl.item.Add(obj);
                    query.Next();
                }
            }
            return msg == "";
        }
        public static bool SaveZKXX(out string msg, CyQuery query, CRMLIBASHX param, int jlbh, int cllx, string kh, int ryid, string rymc)
        {
            msg = string.Empty;
            query.SQL.Text = "insert into HYK_ZKXXJL(HYK_NO,RQ,CLLX,ZXR,ZXRMC) values(:HYK_NO,sysdate,:CLLX,:ZXR,:ZXRMC)";
            query.ParamByName("HYK_NO").AsString = kh;
            query.ParamByName("CLLX").AsInteger = cllx;
            query.ParamByName("ZXR").AsInteger = ryid;
            query.ParamByName("ZXRMC").AsString = rymc;
            query.ExecSQL();
            query.SQL.Text = "update HYKJKJLITEM set XKRQ=trunc(sysdate),BJ_ZK=nvl(BJ_ZK,0)+ 1 where JLBH=:JLBH and CZKHM=:CZKHM";
            query.ParamByName("JLBH").AsInteger = jlbh;
            query.ParamByName("CZKHM").AsString = kh;
            query.ExecSQL();
            query.Close();
            query.SQL.Text = "update HYKCARD set XKRQ=trunc(sysdate) where CZKHM=:CZKHM";
            query.ParamByName("CZKHM").AsString = kh;
            query.ExecSQL();
            return msg == "";
        }
        public static bool GetCDNR(out string msg, CyQuery query, CRMLIBASHX param, string kh, out CKXX obj)
        {
            msg = string.Empty;
            obj = new CKXX();
            string cdnr = string.Empty;
            if (kh.IndexOf("cdnr") == 0)
                cdnr = kh.Substring(5);
            //cdnr = EncryptUtils.DesEncryptCardTrackSecondly(cdnr, CrmServerPlatform.PubData.DesKey);
            //库存卡
            query.SQL.Text = "select C.*,D.HYKNAME,D.BJ_CZK,C.QCYE YE,1 KCK from HYKCARD C,HYKDEF D where C.HYKTYPE=D.HYKTYPE";
            if (cdnr != "")
                query.SQL.Add(" and CDNR='" + cdnr + "'");
            else
                query.SQL.Add(" and CZKHM='" + kh + "'");
            //query.ParamByName("HYK_NO").AsString = kh;
            query.Open();
            if (query.IsEmpty)
            {
                query.Close();
                //会员卡
                query.SQL.Text = "select C.*,D.HYKNAME,D.BJ_CZK,C.HYK_NO CZKHM,E.YE,E.QCYE,0 KCK,G.SFZBH,G.SJHM from HYK_HYXX C,HYKDEF D,HYK_JEZH E,HYK_GKDA G where C.HYKTYPE=D.HYKTYPE and C.HYID=E.HYID(+) and C.GKID=G.GKID(+)";
                if (cdnr != "")
                    query.SQL.Add(" and CDNR='" + cdnr + "'");
                else
                    query.SQL.Add(" and HYK_NO='" + kh + "'");
                //query.ParamByName("HYK_NO").AsString = kh;
                query.Open();
                if (query.IsEmpty)
                {
                    query.Close();
                    //会员子卡
                    query.SQL.Text = "select C.*,D.HYKNAME,D.BJ_CZK,C.HYK_NO CZKHM,E.YE,E.QCYE,0 KCK,G.SFZBH,G.SJHM from HYK_CHILD_JL C,HYKDEF D,HYK_JEZH E,HYK_GKDA G where C.HYKTYPE=D.HYKTYPE and C.HYID=E.HYID(+) and C.GKID=G.GKID(+)";
                    if (cdnr != "")
                        query.SQL.Add(" and CDNR='" + cdnr + "'");
                    else
                        query.SQL.Add(" and HYK_NO='" + kh + "'");
                    //query.ParamByName("HYK_NO").AsString = kh;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "没有查到该卡号";
                    }
                }
            }
            if (msg == "")
            {
                int czk = query.FieldByName("BJ_CZK").AsInteger;
                obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                obj.sCDNR = query.FieldByName("CDNR").AsString;
                obj.sCDNR = DecCDNR(obj.sCDNR);
                //if (czk == 1)
                //    //obj.sCDNR = DecCDNR(obj.sCDNR);
                //    obj.sCDNR = CrmEnCryptUtils.DESDecrypt(obj.sCDNR, CrmEnCryptUtils.GetKey(CrmServerPlatform.Config.ProjectKey, 1));
                //else
                //    obj.sCDNR = DesDecryptCardTrackSecondly(obj.sCDNR, CrmServerPlatform.PubData.DesKey);
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                obj.fJE = query.FieldByName("QCYE").AsFloat;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.bKCK = query.FieldByName("KCK").AsInteger == 1;
                if (!obj.bKCK)
                {
                    obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    obj.sSFZBH = query.FieldByName("SFZBH").AsString;
                    obj.sSJHM = query.FieldByName("SJHM").AsString;
                }
            }
            return msg == "";
        }
        public static bool GetCDNR_CZK(out string msg, CyQuery query, CRMLIBASHX param, string kh, out CKXX obj)
        {
            msg = string.Empty;
            obj = new CKXX();
            //库存面值卡
            query.SQL.Text = "select C.*,D.HYKNAME,D.BJ_CZK,C.QCYE YE,1 KCK from MZKCARD C,HYKDEF D where CZKHM=:HYK_NO and C.HYKTYPE=D.HYKTYPE";
            query.ParamByName("HYK_NO").AsString = kh;
            query.Open();
            if (query.IsEmpty)
            {
                query.Close();
                //面值卡
                query.SQL.Text = "select C.*,D.HYKNAME,D.BJ_CZK,E.QCYE,E.YE,C.HYK_NO CZKHM,0 KCK from MZKXX C,HYKDEF D,MZK_JEZH E  where HYK_NO=:HYK_NO and C.HYKTYPE=D.HYKTYPE and C.HYID=E.HYID(+)";
                query.ParamByName("HYK_NO").AsString = kh;
                query.Open();
                if (query.IsEmpty)
                {
                    msg = "没有查到该卡号";
                }
            }
            if (msg == "")
            {
                int czk = query.FieldByName("BJ_CZK").AsInteger;
                obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                obj.sCDNR = query.FieldByName("CDNR").AsString;
                obj.sCDNR = DecCDNR(obj.sCDNR);
                //if (czk == 1)
                //    //obj.sCDNR = DecCDNR(obj.sCDNR);
                //    obj.sCDNR = CrmEnCryptUtils.DESDecrypt(obj.sCDNR, CrmEnCryptUtils.GetKey(CrmServerPlatform.Config.ProjectKey, 1));
                //else
                //    obj.sCDNR = DesDecryptCardTrackSecondly(obj.sCDNR, CrmServerPlatform.PubData.DesKey);
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                obj.fJE = query.FieldByName("YE").AsFloat;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.bKCK = query.FieldByName("KCK").AsInteger == 1;
            }
            return msg == "";
        }
        public static bool SendHttpPostRequest(out string msg, string url, string date, out string getData)
        {
            msg = "";
            getData = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);

                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(date);
                request.ContentType = "application/x-gzip";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                getData = responseFromServer.ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message;

            }
            return msg.Length == 0;
        }
        public static string SendHttpGetRequest(out string msg, string url)
        {
            msg = "";
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer.ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return "-1";
            }
        }
        public static string getToken(string PUBLICIF)
        {
            string Token = string.Empty;
            //微信相关token等都直接发送到微信程序接口 统一一个获取token接口

            //string serverUrl1 = ConfigurationManager.AppSettings["WeChatSeverAddress1"];
            string msg = string.Empty;
            Token = SendHttpGetRequest(out msg, PUBLICIF);


            return Token;
        }
        public static void WriteToLog(string e)
        {
            DateTime timeEnd = DateTime.Now;
            DateTime timeBegin = DateTime.Now;
            StringBuilder logStr = new System.Text.StringBuilder();
            logStr.Append("\r\n--->St:").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(" Response error, ").Append(timeEnd.Subtract(timeBegin).Milliseconds).Append(" ms:");
            logStr.Append("\r\n").Append(e);
            logStr.Append("\r\n--->Ed:").Append(e);
            DailyLogFileWriter DataLogFileWriter = null;
            string logPath = ConfigurationManager.AppSettings["LogPath"];
            DataLogFileWriter = new DailyLogFileWriter(logPath, "LOG");
            DataLogFileWriter.Write(timeBegin.ToString("yyyy-MM-dd"), logStr.ToString());
            DataLogFileWriter.Close();
        }
    }
}