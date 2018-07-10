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

namespace BF.CrmProc.HYKGL
{
    public class HYKGL_YHQPLQK : HYKYHQ_DJLR_CLass
    {

        public string sHYKNO = string.Empty;
        public int iKHID = 0;
        public int iMDID = -1;
        public int iFS_YQMDFW = 0;
        public double fTYQKJE = 0;
        public class HYKGL_YHQPLQKItem
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public double fQKJE = 0;
            public int iINX = 0;
            public double fJE = 0;
            public string sMDFWDM = string.Empty, sMDFWMC = string.Empty;
        }
        public List<HYKGL_YHQPLQKItem> itemTable = new List<HYKGL_YHQPLQKItem>();
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sBGDDDM", "A.CZDD");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("iYHQID", "A.YHQID");
            CondDict.Add("dJSRQ", "A.JSRQ");
            CondDict.Add("iCXID", "A.CXID");
            CondDict.Add("sZY", "A.ZY");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("sZXRMC", "A.ZXRMC");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("iMDID", "A.MDID");

            query.SQL.Text = "select A.*,D.YHQMC,E.CXZT ,M.MDMC,D.FS_YQMDFW";
            query.SQL.Add(" from HYK_YHQ_PLQKJL A,YHQDEF D,CXHDDEF E,MDDY M");
            query.SQL.Add(" where  A.YHQID=D.YHQID and A.CXID=E.CXID(+) ");
            query.SQL.Add("  and A.MDID=M.MDID(+)");
            if (sHYKNO != "" && sHYKNO != null)
            {
                query.SQL.Add("  and A.Jlbh in (select JLBH from HYK_YHQ_PLQKJLITEM where HYKNO='" + sHYKNO + "' )");
            }

            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                HYKGL_YHQPLQK item = new HYKGL_YHQPLQK();
                lst.Add(item);
                //item.sBGDDDM = query.FieldByName("CZDD").AsString;
                //item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                //item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                //item.sMDMC = query.FieldByName("sMDMC").AsString;
                //item.fTYQKJE = query.FieldByName("TYQKJE").AsFloat;
                item.iJLBH = query.FieldByName("JLBH").AsInteger;
                item.sZY = query.FieldByName("ZY").AsString;
                item.iYHQID = query.FieldByName("YHQID").AsInteger;
                item.sYHQMC = query.FieldByName("YHQMC").AsString;
                item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                item.sMDMC = query.FieldByName("MDMC").AsString;
                item.iMDID = query.FieldByName("MDID").AsInteger;
                item.iCXID = query.FieldByName("CXID").AsInteger;
                item.sCXZT = query.FieldByName("CXZT").AsString;
                item.iDJR = query.FieldByName("DJR").AsInteger;
                item.sDJRMC = query.FieldByName("DJRMC").AsString;
                item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                item.iZXR = query.FieldByName("ZXR").AsInteger;
                item.sZXRMC = query.FieldByName("ZXRMC").AsString;
                item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                item.iFS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
                query.Next();
            }
            query.Close();
            if (lst.Count == 1)
            {
                if (((HYKGL_YHQPLQK)lst[0]).iFS_YQMDFW == 1)
                {
                    query.SQL.Text = "select I.*,H.HY_NAME,H.HYK_NO ,'集团' MDFWMC from HYK_YHQ_PLQKJLITEM I,HYK_HYXX H where I.JLBH=" + iJLBH;
                    query.SQL.Add(" and I.HYID=H.HYID ");
                }
                if (((HYKGL_YHQPLQK)lst[0]).iFS_YQMDFW == 2)
                {
                    query.SQL.Text = "select I.*,H.HY_NAME,H.HYK_NO, S.SHMC MDFWMC from HYK_YHQ_PLQKJLITEM I,HYK_HYXX H,SHDY S where I.JLBH=" + iJLBH;
                    query.SQL.Add(" and I.HYID=H.HYID ");
                    query.SQL.Add(" and I.MDFWDM=S.SHDM ");
                }
                if (((HYKGL_YHQPLQK)lst[0]).iFS_YQMDFW == 3)
                {
                    query.SQL.Text = "select I.*,H.HY_NAME,H.HYK_NO, S.MDMC MDFWMC from HYK_YHQ_PLQKJLITEM I,HYK_HYXX H,MDDY S where I.JLBH=" + iJLBH;
                    query.SQL.Add(" and I.HYID=H.HYID ");
                    query.SQL.Add(" and I.MDFWDM=S.MDDM ");
                }
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_YHQPLQKItem item = new HYKGL_YHQPLQKItem();
                    ((HYKGL_YHQPLQK)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.fJE = query.FieldByName("YHQYE").AsFloat;
                    item.fQKJE = query.FieldByName("QKJE").AsFloat;        
                    item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                    item.sMDFWMC = query.FieldByName("MDFWMC").AsString;
                    query.Next();
                }
            }
            query.Close();



            return lst;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_YHQ_PLQKJL;HYK_YHQ_PLQKJLITEM", "JLBH", iJLBH);
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            double SJJE = 0;
            ExecTable(query, "HYK_YHQ_PLQKJL", serverTime, "JLBH");
            foreach (HYKGL_YHQPLQKItem one in itemTable)
            {
                query.SQL.Text = "select JE from HYK_YHQZH  where HYID=:HYID and YHQID=:YHQID and JSRQ=:JSRQ and MDFWDM=:MDFWDM and CXID=:CXID";
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YHQID").AsInteger = iYHQID;
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM;
                query.ParamByName("CXID").AsInteger = iCXID;
                query.Open();
                if (!query.IsEmpty)
                {
                    SJJE = query.FieldByName("JE").AsFloat;
                }
                query.SQL.Clear();
                query.Close();
                if (one.fQKJE <= SJJE)
                {
                    CrmLibProc.UpdateYHQZH(out msg, query, one.iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, iYHQID, iMDID, iCXID, -one.fQKJE, dJSRQ, "优惠券批量取款", one.sMDFWDM);
                }
                else
                {
                    throw new Exception(one.sHYK_NO + "余额不足");
                }


            }
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_YHQ_PLCKJL");
            query.SQL.Text = "insert into HYK_YHQ_PLQKJL(JLBH,KHID,YHQID,JSRQ,CXID,ZY,DJR,DJRMC,DJSJ,CZDD,MDID)";
            query.SQL.Add(" values(:JLBH,:KHID,:YHQID,:JSRQ,:CXID,:ZY,:DJR,:DJRMC,:DJSJ,:CZDD,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KHID").AsInteger = iKHID;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("CXID").AsInteger = iCXID;
            //query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            //query.ParamByName("TYQKJE").AsFloat = fTYQKJE;
            query.ExecSQL();
            foreach (HYKGL_YHQPLQKItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_YHQ_PLQKJLITEM(JLBH,HYID,QKJE,BZ,YHQYE,MDFWDM)";
                query.SQL.Add(" values(:JLBH,:HYID,:QKJE,:BZ,:YHQYE,:MDFWDM)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("QKJE").AsFloat = one.fQKJE;
                query.ParamByName("BZ").AsString = sZY;
                //query.ParamByName("BJ_CHILD").AsInteger = 0;
                //query.ParamByName("INX").AsInteger = one.iINX;
                //query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                //query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ParamByName("YHQYE").AsFloat = one.fJE;
                query.ParamByName("MDFWDM").AsString = one.sMDFWDM;
                query.ExecSQL();
            }
        }


        public static string GetMDFWDM(int MDID, int YHQID)
        {
            string MDFWDM = string.Empty;
            int FS_YQMDFW = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select FS_YQMDFW from YHQDEF Y where Y.YHQID=:YHQID";
                    query.ParamByName("YHQID").AsInteger = YHQID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        FS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
                    }
                    query.Close();
                    if (FS_YQMDFW == 1)
                    {
                        MDFWDM = " ";
                        return MDFWDM;
                    }

                    if (FS_YQMDFW == 2)
                    {
                        query.SQL.Text = " select SHDM from MDDY M  where M.MDID=" + MDID + "";
                    }

                    else if (FS_YQMDFW == 3)
                    {
                        query.SQL.Text = "select MDDM from MDDY M where M.MDID=" + MDID + "";
                    }
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        if (FS_YQMDFW == 1 || FS_YQMDFW == 2)
                        {
                            MDFWDM = query.FieldByName("SHDM").AsString;
                        }
                        if (FS_YQMDFW == 3)
                        {
                            MDFWDM = query.FieldByName("MDDM").AsString;

                        }
                    }
                    else
                    {
                        MDFWDM = "";
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
            return MDFWDM;
        }

        public static HYKGL_YHQPLQKItem GetBalance(int iMDID, int iYHQID, int iHYID, string endDate, int iCXID)
        {
            int tp_yqmdfw = 0;
            HYKGL_YHQPLQKItem obj = new HYKGL_YHQPLQKItem();

            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select FS_YQMDFW from  YHQDEF where YHQID=:YHQID";
                    query.ParamByName("YHQID").AsInteger = iYHQID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        tp_yqmdfw = query.FieldByName("FS_YQMDFW").AsInteger;
                    }
                    else
                    {
                        return obj;
                    }
                    query.Close();
                    if (tp_yqmdfw == 1)
                    {
                        query.SQL.Text = "  select Y.*,'集团' MDFWMC from  HYK_YHQZH Y where HYID=:HYID and YHQID=:YHQID and CXID=:CXID and MDFWDM=:MDFWDM and JSRQ=:JSRQ";
                    }
                    if (tp_yqmdfw == 2)
                    {
                        query.SQL.Text = "  select Y.*,M.MDMC MDFWMC  from  HYK_YHQZH Y,MDDY M where Y.MDFWDM=M.MDDM and  HYID=:HYID and YHQID=:YHQID and CXID=:CXID and MDFWDM=:MDFWDM and JSRQ=:JSRQ";
                    }
                    if (tp_yqmdfw == 3)
                    {
                        query.SQL.Text = "  select Y.*,M.SHMC MDFWMC  from  HYK_YHQZH Y,SHDY M where Y.MDFWDM=M.SHDM and  HYID=:HYID and YHQID=:YHQID and CXID=:CXID and MDFWDM=:MDFWDM and JSRQ=:JSRQ";

                    }
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("YHQID").AsInteger = iYHQID;
                    query.ParamByName("CXID").AsInteger = iCXID;
                    query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(endDate);
                    query.ParamByName("MDFWDM").AsString = GetMDFWDM(iMDID, iYHQID);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        // tp_balance = query.FieldByName("JE").AsFloat;
                        obj.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                        obj.sMDFWMC = query.FieldByName("MDFWMC").AsString;
                        obj.fJE = query.FieldByName("JE").AsFloat;
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
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
            return obj;
        }
    }
}
