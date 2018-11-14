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


namespace BF.CrmProc.HYXF
{
    public class HYXF_JFFLZX : DJLR_ZX_CLass
    {
        public int iHBFS = 0, iCZJLBH = 0;
        public string dCLRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fCLJF = 0, fFLJE = 0;
        public string sHYK_NO = string.Empty;
        public int iYHQID = -1, iFLGZBH = 0, iMDID = 0, iHYID = 0, iDYCS = 0;
        public double fWCLJF_OLD = 0, fSJFLJE = 0, fKYXFJE = 0;
        public string sCLIENT_IP_ADDRESS = string.Empty;
        public string sMDMC = string.Empty;
        public string sYHQMC = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string dYHQJSRQ = string.Empty;
        public int iYHQSL = 0;
        public double fFQJE = 0;
        public int iBJ_WCLJF = 0, iFQGZID = 0;
        public string sHYKNAME = string.Empty;
        public int iCZR = 0;
        public string sCZRMC = string.Empty;
        public string dCZRQ = string.Empty;

        // public List<LPGL_LPFF_LP> itemTable = new List<LPGL_LPFF_LP>();
        public List<JFFLGZItem> itemTable = new List<JFFLGZItem>();
        

        #region  (HYXF_JFFLZXItem ,LPGL_LPFF_LP) No Way Class
        public class HYXF_JFFLZXItem : DZHYKYHQCQK_DJLR_CLass
        {
            public int iFLGZBH = 0;
            public string sSHDM = string.Empty;
            public double fWCLXFJE = 0;
            public double fCLJF = 0;
            public double fFLJE = 0;
            public string dYHQJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
            public double fWCLJF_OLD = 0;
            public double fCLJE = 0;
            public double fLPJSJE = 0;
            public double fLJJSJE = 0;
            public double fYXXFJE = 0;
            public double fLJXFJE = 0;
            public double fFLBL = 0;
            public int iBJ_CHILD = 0;
            public int iMDID = 0;
            public string sGZMC = string.Empty;

        }
        public class LPGL_LPFF_LP : LPXX
        {
            public double fSL = 0;
            //public int iGZID = 0;
            //public int iCXHDBH = 0;
            //public string sRQ = string.Empty;
            public double fLPJE = 0;
        }
        #endregion

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFFLJL;HYK_JFFLJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            //  int MDID = 0;
            msg = string.Empty;
            int tp_cz = 1;
            if (iJLBH != 0)
            {
                iCZJLBH = iJLBH;
                tp_cz = -1;
            }
            if (CheckNumberOfExchange(query) == false)
            {
                msg = "返券机会已用完";
                return;
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFFLJL");
            query.SQL.Text = "insert into HYK_JFFLJL(JLBH,HBFS,CZJLBH,DJR,DJRMC,DJSJ,CZDD,ZY,CLRQ,CLJF,FLJE,DYCS,DJLX,WCLJF_OLD,SJFLJE,KYXFJE,CLIENT_IP_ADDRESS,MDID,YHQID,FQGZID,YHQJSRQ,HYID)";
            query.SQL.Add(" values(:JLBH,:HBFS,:CZJLBH,:DJR,:DJRMC,:DJSJ,:CZDD,:ZY,:CLRQ,:CLJF,:FLJE,:DYCS,:DJLX,:WCLJF_OLD,:SJFLJE,:KYXFJE,:CLIENT_IP_ADDRESS,:MDID,:YHQID,:FQGZID,:YHQJSRQ,:HYID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HBFS").AsInteger = 1;
            query.ParamByName("CZJLBH").AsInteger = iCZJLBH;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("CLRQ").AsDateTime = FormatUtils.ParseDateString(dCLRQ);
            query.ParamByName("CLJF").AsFloat = tp_cz * fCLJF;
            query.ParamByName("FLJE").AsFloat = tp_cz * fFQJE;
            query.ParamByName("DYCS").AsInteger = iDYCS;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("WCLJF_OLD").AsFloat = fWCLJF_OLD;
            query.ParamByName("SJFLJE").AsFloat = fSJFLJE;
            query.ParamByName("KYXFJE").AsFloat = fKYXFJE;
            query.ParamByName("CLIENT_IP_ADDRESS").AsString = sCLIENT_IP_ADDRESS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("FQGZID").AsInteger = iFQGZID;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("YHQJSRQ").AsDateTime = FormatUtils.ParseDateString(dYHQJSRQ);
            query.ParamByName("HYID").AsInteger = iHYID;
            //query.ParamByName("BJ_WCLJF").AsInteger = iBJ_WCLJF;
            //query.ParamByName("BJ_CZ").AsInteger = 0;
            query.ExecSQL();
            foreach (JFFLGZItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFFQJLITEM(JLBH,XH,JFXX,JFSX,FLBL,CLJF,FQJE)";
                query.SQL.Add(" values(:JLBH,:XH,:JFXX,:JFSX,:FLBL,:CLJF,:FQJE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ParamByName("JFXX").AsFloat = one.fJFXX;
                query.ParamByName("JFSX").AsFloat = one.fJFSX;
                query.ParamByName("FLBL").AsFloat = one.fFLBL;
                query.ParamByName("CLJF").AsFloat = one.fCLJF;
                query.ParamByName("FQJE").AsFloat = one.fFQJE;
                query.ExecSQL();

            }
            ExecTable(query, "HYK_JFFLJL", serverTime, "JLBH", "SHR", "SHRMC", "SHRQ");
            query.SQL.Clear();
            query.Params.Clear();
            if (tp_cz == -1)
            {
                query.SQL.Text = "  UPdate HYK_JFFLJL  set CZR=:CZR, CZRMC=:CZRMC,CZRQ=:CZRQ where JLBH in(:JLBH,:CZJLBH) ";
                query.ParamByName("CZR").AsInteger = iLoginRYID;
                query.ParamByName("CZRMC").AsString = sLoginRYMC;
                query.ParamByName("CZRQ").AsDateTime = serverTime;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZJLBH").AsInteger = iCZJLBH;
                query.ExecSQL();
            }
            CrmLibProc.UpdateJFZH(out msg, query, 4, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFFLZX_CZ, iJLBH, iMDID, -tp_cz * fCLJF, iLoginRYID, sLoginRYMC, "会员积分处理回报");
            CrmLibProc.UpdateYHQZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iYHQID, iMDID, 0, tp_cz * fFQJE, dYHQJSRQ, "会员积分处理回报");
            if (tp_cz == -1)
            {
                iJLBH = iCZJLBH;
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFFLJL", serverTime);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","W.JLBH");
            CondDict.Add("sBGDDDM", "W.CZDD");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iSHR", "W.SHR");
            CondDict.Add("sSHRMC", "W.SHRMC");
            CondDict.Add("dSHRQ", "W.SHRQ");
            CondDict.Add("iCZJLBH", "W.CZJLBH");
            CondDict.Add("iCZR", "W.CZR");
            CondDict.Add("sCZMC", "W.CZRMC");
            CondDict.Add("dCZRQ", "W.CZRQ");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("fFQJE", "W.FLJE");

            query.SQL.Text = "select W.*,H.HYK_NO,G.KSRQ,G.JSRQ,G.YHQSL,G.YHQJSRQ,M.MDMC,Y.YHQMC,M.MDDM,F.HYKNAME,B.BGDDMC ";
            query.SQL.Add(" from HYK_JFFLJL W,HYK_JFFLGZ G,MDDY M,YHQDEF Y, HYK_HYXX H,HYKDEF F,HYK_BGDD B");
            query.SQL.Add("    where W.FQGZID=G.JLBH and W.MDID=M.MDID and W.YHQID=Y.YHQID ");
            query.SQL.Add("   and W.HYID=H.HYID and H.HYKTYPE=F.HYKTYPE  AND W.CZDD=B.BGDDDM");
            query.SQL.Add("   and W.HBFS=1 and W.CZJLBH=0");
            SetSearchQuery(query, lst);

            if(lst.Count==1)
            {
                query.SQL.Text = "select * from HYK_JFFQJLITEM";
                query.SQL.Add("  where JLBH=" + iJLBH + "");
                query.Open();
                while (!query.Eof)
                {
                    JFFLGZItem obj = new JFFLGZItem();
                    ((HYXF_JFFLZX)lst[0]).itemTable.Add(obj);
                    obj.iXH = query.FieldByName("XH").AsInteger;
                    obj.fJFSX = query.FieldByName("JFSX").AsFloat;
                    obj.fJFXX = query.FieldByName("JFXX").AsFloat;
                    obj.fFLBL = query.FieldByName("FLBL").AsFloat;
                    obj.fCLJF = query.FieldByName("CLJF").AsFloat;
                    obj.fFQJE = query.FieldByName("FQJE").AsFloat;
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFFLZX obj = new HYXF_JFFLZX();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHBFS = query.FieldByName("HBFS").AsInteger;
            obj.iCZJLBH = query.FieldByName("CZJLBH").AsInteger;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.dCLRQ = FormatUtils.DatetimeToString(query.FieldByName("CLRQ").AsDateTime);
            obj.fCLJF = query.FieldByName("CLJF").AsFloat;
            obj.fFQJE = query.FieldByName("FLJE").AsFloat;
            obj.iDYCS = query.FieldByName("DYCS").AsInteger;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.fWCLJF_OLD = query.FieldByName("WCLJF_OLD").AsFloat;
            obj.fSJFLJE = query.FieldByName("SJFLJE").AsFloat;
            obj.fKYXFJE = query.FieldByName("KYXFJE").AsFloat;
            obj.sCLIENT_IP_ADDRESS = query.FieldByName("CLIENT_IP_ADDRESS").AsString;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("SHR").AsInteger;
            obj.sZXRMC = query.FieldByName("SHRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("SHRQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.iFQGZID = query.FieldByName("FQGZID").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.dYHQJSRQ = FormatUtils.DateToString(query.FieldByName("YHQJSRQ").AsDateTime);
            obj.iYHQSL = query.FieldByName("YHQSL").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iCZR = query.FieldByName("CZR").AsInteger;
            obj.sCZRMC = query.FieldByName("CZRMC").AsString;
            obj.dCZRQ = FormatUtils.DatetimeToString(query.FieldByName("CZRQ").AsDateTime);
            obj.iZXR = query.FieldByName("SHR").AsInteger;
            obj.sZXRMC = query.FieldByName("SHRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("SHRQ").AsDateTime);
            return obj;
        }

        /// <summary>获取积分返利规则明细
        /// 获取规则明细
        /// </summary>
        /// <param name="iFQGZBH"></param>
        /// <returns></returns>
        public static string SearchRuleData(int iFLGZBH)
        {
            List<Object> lst = new List<Object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                try
                {
                    query.SQL.Text = "select W.*,D.HYKNAME,Y.YHQMC,M.MDMC,M.MDDM";
                    query.SQL.Add("     from HYK_JFFLGZ W,HYKDEF D,YHQDEF Y,MDDY M");
                    query.SQL.Add("    where W.HYKTYPE=D.HYKTYPE and W.YHQID=Y.YHQID");
                    query.SQL.Add("   and W.MDID=M.MDID");
                    query.SQL.Add("  and W.STATUS=1");
                    if (iFLGZBH != 0)
                        query.SQL.AddLine("  and JLBH=" + iFLGZBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXF_JFFLGZ obj = new HYXF_JFFLGZ();
                        lst.Add(obj);
                        obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                        obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        if (FormatUtils.DateToString(query.FieldByName("YHQJSRQ").AsDateTime) != "")
                        {
                            obj.dYHQJSRQ = FormatUtils.DateToString(query.FieldByName("YHQJSRQ").AsDateTime);
                        }
                        else
                        {                       
                            if (query.FieldByName("YHQSL").AsInteger == 0)
                            {
                                obj.dYHQJSRQ = FormatUtils.DateToString(serverTime);
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
                                obj.dYHQJSRQ = FormatUtils.DateToString(serverTime);
                            }
                        }
                        obj.iYHQSL = query.FieldByName("YHQSL").AsInteger;
                        obj.sMDDM = query.FieldByName("MDDM").AsString;
                        obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                        obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                        query.Next();
                    }
                    query.Close();
                    if (lst.Count == 1)
                    {
                        query.SQL.Text = "select I.*from HYK_JFFLGZITEM I where I.JLBH=" + iFLGZBH;
                        query.SQL.Add(" order by I.XH");
                        query.Open();
                        while (!query.Eof)
                        {
                            JFFLGZItem obj = new JFFLGZItem();
                            ((HYXF_JFFLGZ)lst[0]).itemTable.Add(obj);
                            obj.iXH = query.FieldByName("XH").AsInteger;
                            obj.fJFXX = query.FieldByName("JFXX").AsFloat;
                            obj.fJFSX = query.FieldByName("JFSX").AsFloat;
                            obj.fFLBL = query.FieldByName("FLBL").AsFloat;
                            obj.fCLJF = 0;
                            obj.fFQJE = 0;

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


        /// <summary>     获取积分返利总金额
        /// 老程序获取积分返利金额
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="iFLGZBH"></param>
        /// <param name="fCLJF"></param>
        /// <returns></returns>   
        public static HYXF_JFFLZXItem GetJFFLJE(out string msg, int iFLGZBH, double fCLJF)
        {
            msg = string.Empty;
            HYXF_JFFLZXItem JFFLITEM = new HYXF_JFFLZXItem();
            List<JFFLGZItem> lst = new List<JFFLGZItem>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select * from HYK_JFFLGZITEM I,HYK_JFFLGZ G where I.JLBH=G.JLBH";
                    query.SQL.Add("  and G.JLBH=" + iFLGZBH);
                    query.SQL.Add("  Order by JFXX");
                    query.Open();
                    while (!query.Eof)
                    {
                        JFFLGZItem item = new JFFLGZItem();
                        item.fFLBL = query.FieldByName("FLBL").AsFloat;
                        item.fJFSX = query.FieldByName("JFSX").AsFloat;
                        item.fJFXX = query.FieldByName("JFXX").AsFloat;
                        item.iCLFS = query.FieldByName("CLFS").AsInteger;
                        item.iXH = query.FieldByName("XH").AsInteger;
                        lst.Add(item);
                        query.Next();
                    }
                    query.Close();
                    if (lst.Count > 0)
                    {
                        if (lst[0].iCLFS == 1)
                        {
                            if (fCLJF >= lst[0].fJFXX)
                            {
                                if (fCLJF <= lst[0].fJFSX)
                                {
                                    JFFLITEM.fFLJE = fCLJF * lst[0].fFLBL;
                                }
                                else
                                {
                                    JFFLITEM.fFLJE = (lst[0].fJFSX * lst[0].fFLBL);

                                    for (int i = 1; i < lst.Count; i++)
                                    {
                                        if (fCLJF >= lst[i].fJFSX)
                                        {
                                            // JFFLITEM.fFLJE += ((fCLJF - lst[i - 1].fJFSX)) * lst[i].fFLBL;
                                            JFFLITEM.fFLJE += (lst[i].fJFSX - lst[i - 1].fJFSX) * lst[i].fFLBL;
                                            if (i == lst.Count - 1)
                                            {
                                                JFFLITEM.fFLJE += (fCLJF - lst[i].fJFSX) * lst[i].fFLBL;
                                            }
                                        }
                                        else if (fCLJF >= lst[i].fJFXX && fCLJF < lst[i].fJFSX)
                                        {
                                            JFFLITEM.fFLJE += (fCLJF - lst[i].fJFXX) * lst[i].fFLBL;
                                            break;
                                        }

                                        else
                                        {
                                            JFFLITEM.fFLJE += (fCLJF - lst[i - 1].fJFSX) * lst[i].fFLBL;
                                        }
                                    }
                                }
                            }
                        }
                        if (lst[0].iCLFS == 2)
                        {
                            for (int i = lst.Count - 1; i >= 0; i--)
                            {
                                if (fCLJF > lst[i].fJFXX)
                                {
                                    JFFLITEM.fFLJE = fCLJF * 1 * lst[i].fFLBL;
                                    break;
                                }
                            }
                        }

                        if (lst[0].iCLFS == 3)
                        {
                            for (int i = lst.Count - 1; i >= 0; i--)
                            {
                                if (fCLJF >= lst[i].fJFXX)
                                {
                                    var JFBS = Math.Floor(fCLJF / lst[i].fJFXX);
                                    var JFYS = fCLJF % lst[i].fJFXX;
                                    if (JFYS != 0)
                                    {
                                        for (int j = lst.Count - 1; j >= 0; j--)
                                        {
                                            if (JFYS >= lst[j].fJFXX)
                                            {
                                                JFFLITEM.fFLJE = JFBS * lst[i].fJFXX * lst[i].fFLBL + JFYS * lst[j].fFLBL;
                                                break;
                                            }
                                            else
                                            {
                                                JFFLITEM.fFLJE = JFBS * lst[i].fJFXX * lst[i].fFLBL;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        JFFLITEM.fFLJE = JFBS * lst[i].fJFXX * lst[i].fFLBL;
                                    }
                                    break;
                                }
                            }
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

            return JFFLITEM;
        }

        /// <summary>       控制会员参加活动次数
        ///  控制会员参加活动次数
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public bool CheckNumberOfExchange(CyQuery query)
        {
            bool BoolInsert = true;
            DateTime serverTime = CyDbSystem.GetDbServerTime(query);
            query.SQL.Text = "select BJ_XZ ,KSRQ,JSRQ from HYK_JFFLGZ  where JLBH=:JLBH ";
            query.ParamByName("JLBH").AsInteger = iFLGZBH;
            query.Open();
            if (!query.IsEmpty)
            {
                int BJ_XZ = query.FieldByName("BJ_XZ").AsInteger;
                DateTime KSRQ = query.FieldByName("KSRQ").AsDateTime;
                DateTime JSRQ = query.FieldByName("JSRQ").AsDateTime;
                query.SQL.Clear();
                query.Close();
                if (BJ_XZ != 0)
                {
                    query.SQL.Text = "select * from HYK_JFFLJL where  FQGZID=:FQGZID and HYID=:HYID and DJSJ>=:KSRQ and DJSJ<=:JSRQ and CZJLBH=:CZJLBH";
                    query.ParamByName("FQGZID").AsInteger = iFLGZBH;
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.ParamByName("CZJLBH").AsInteger = 0;
                }

                if (BJ_XZ == 1)
                {
                    query.ParamByName("KSRQ").AsDateTime = KSRQ;
                    query.ParamByName("JSRQ").AsDateTime = JSRQ;
                }
                if (BJ_XZ == 2)
                {
                    serverTime = Convert.ToDateTime(serverTime.ToShortDateString());
                    query.ParamByName("KSRQ").AsDateTime = serverTime;
                    serverTime = serverTime.AddDays(1);
                    query.ParamByName("JSRQ").AsDateTime = serverTime;
                }
                if (BJ_XZ != 0)
                {
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        BoolInsert = false;
                    }
                    query.Close();
                }

            }

            else
            {
                BoolInsert = false;

            }
            return BoolInsert;
        }


    }
}
