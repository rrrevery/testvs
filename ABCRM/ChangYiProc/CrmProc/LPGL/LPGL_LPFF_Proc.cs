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

namespace BF.CrmProc.LPGL
{
    public class LPGL_LPFF : DZHYK_DJLR_CLass
    {
        public int iHBFS = 0;
        public int iCZJLBH = 0;
        public string dCLRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fCLJF = 0;
        public double fFLJE = 0;
        public double fWCLJF_OLD = 0;
        public double fSJFLJE = 0;
        public double fKYXFJE = 0;
        public string sCLIENT_IP_ADDRESS = string.Empty;
        public int iDYCS = 0;
        public int iBJ_XSMX = 0;
        public int iFLGZBH = 0;
        public string sGZMC = string.Empty;
        public string sSHDM = string.Empty;
        public int iYHQID = 0;
        public double fWCLXFJE = 0;
        public string dYHQJSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fCLJE = 0;
        public double fLPJSJE = 0;
        public double fLJJSJE = 0;
        public double fYXXFJE = 0;
        public double fLJXFJE = 0;
        public string sConditionName = string.Empty;
        public int iBJ_CHILD = 0;
        public int iJFLX = 0;
        public string sLPDM = string.Empty;
        public string sLPMC = string.Empty;
        public string sGJBM = string.Empty;
        public int iFFSL = 0;
        public int iCZR = 0;
        public string sCZRMC = string.Empty;
        public string dCZRQ = string.Empty;
        public int BJ_DC, BJ_SR, BJ_LJ, BJ_BK = 0;
        public DateTime dKSRQ = DateTime.MinValue;
        public DateTime dJSRQ = DateTime.MinValue;
        public bool bCHECKRESULT = true;
        //iDJLX;//礼品发放的方式：日常发放0  首刷礼品发放1 办卡礼品发放2 积分礼品发放3 来店礼品发放4 生日礼品发放5    
        public List<LPGL_LPFF_LP> itemTable = new List<LPGL_LPFF_LP>();

        public class LPGL_LPFF_LP : LPXX
        {
            public double fSL = 0;
            //public int iGZID = 0;
            //public int iCXHDBH = 0;
            //public string sRQ = string.Empty;
            public double fLPJE = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }
        public int aJLBH = 0;
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int iCZ = 1;
            if (iJLBH != 0)
            {
                //DeleteDataQuery(out msg, query,serverTime);
                //此单据无删除、修改，只有保存、冲正
                iCZ = -1;
                aJLBH = iJLBH;
            }
            iJLBH = SeqGenerator.GetSeq("HYK_JFFLJL");
            query.SQL.Text = "insert into HYK_JFFLJL(JLBH,HBFS,CZJLBH,DJR,DJRMC,DJSJ,CZDD,ZY,CLRQ,CLJF,FLJE,DJLX,";
            query.SQL.Add(" WCLJF_OLD,SJFLJE,KYXFJE,CLIENT_IP_ADDRESS,DYCS)");
            query.SQL.Add(" values(:JLBH,:HBFS,:CZJLBH,:DJR,:DJRMC,:DJSJ,:CZDD,:ZY,:CLRQ,:CLJF,:FLJE,:DJLX,");
            query.SQL.Add(" :WCLJF_OLD,:SJFLJE,:KYXFJE,:CLIENT_IP_ADDRESS,:DYCS)");
            query.ParamByName("JLBH").AsInteger = iJLBH;

            query.ParamByName("HBFS").AsInteger = 0;
            query.ParamByName("CZJLBH").AsInteger = iCZJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CLRQ").AsDateTime = FormatUtils.ParseDateString(dCLRQ);
            query.ParamByName("CLJF").AsFloat = iCZ * fCLJF;
            query.ParamByName("FLJE").AsFloat = iCZ * fFLJE;
            query.ParamByName("DJLX").AsInteger = iDJLX;//礼品发放的方式：日常发放0  首刷礼品发放1 办卡礼品发放2 积分礼品发放3 来店礼品发放4 生日礼品发放5    
            query.ParamByName("WCLJF_OLD").AsFloat = fWCLJF_OLD;
            query.ParamByName("SJFLJE").AsFloat = iCZ * fSJFLJE;
            query.ParamByName("KYXFJE").AsFloat = fKYXFJE;
            query.ParamByName("CLIENT_IP_ADDRESS").AsString = sCLIENT_IP_ADDRESS;
            query.ParamByName("DYCS").AsInteger = iDYCS;
            query.ExecSQL();
            query.SQL.Text = "insert into HYK_JFFLJLITEM(JLBH,HYID,FLGZBH,SHDM,YHQID,WCLXFJE,CLJF,FLJE,YHQJSRQ,WCLJF_OLD,CLJE,LPJSJE,LJJSJE,YXXFJE,LJXFJE,HYK_NO)";
            query.SQL.Add(" values(:JLBH,:HYID,:FLGZBH,:SHDM,:YHQID,:WCLXFJE,:CLJF,:FLJE,:YHQJSRQ,:WCLJF_OLD,:CLJE,:LPJSJE,:LJJSJE,:YXXFJE,:LJXFJE,:HYK_NO)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("FLGZBH").AsInteger = iFLGZBH;
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("WCLXFJE").AsFloat = fWCLXFJE;
            query.ParamByName("CLJF").AsFloat = iCZ * fCLJF;
            query.ParamByName("FLJE").AsFloat = iCZ * fFLJE;
            query.ParamByName("YHQJSRQ").AsDateTime = FormatUtils.ParseDateString(dYHQJSRQ);
            query.ParamByName("WCLJF_OLD").AsFloat = fWCLJF_OLD;
            query.ParamByName("CLJE").AsFloat = fCLJE;
            query.ParamByName("LPJSJE").AsFloat = fLPJSJE;
            query.ParamByName("LJJSJE").AsFloat = fLJJSJE;
            query.ParamByName("YXXFJE").AsFloat = fYXXFJE;
            query.ParamByName("LJXFJE").AsFloat = fLJXFJE;
            query.ParamByName("HYK_NO").AsString = sHYK_NO;
            query.ExecSQL();

            foreach (LPGL_LPFF_LP one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFFLJL_LP(JLBH,BGDDDM,LPID,SL,LPDJ,LPJF,LPJJ,KCSL)";
                query.SQL.Add(" values(:JLBH,:BGDDDM,:LPID,:SL,:LPDJ,:LPJF,:LPJJ,:KCSL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("BGDDDM").AsString = sBGDDDM;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("SL").AsFloat = iCZ * one.fSL;
                query.ParamByName("LPDJ").AsFloat = one.fLPDJ;
                query.ParamByName("LPJF").AsFloat = one.fLPJF;
                query.ParamByName("LPJJ").AsFloat = one.fLPJJ;
                query.ParamByName("KCSL").AsFloat = one.fKCSL;
                query.ExecSQL();
            }
            //此单据保存立刻生效
            ExecTable(query, "HYK_JFFLJL", serverTime, "JLBH", "SHR", "SHRMC", "SHRQ");
            query.SQL.Clear();
            query.Params.Clear();
            if (iCZ == -1)
            {
                query.SQL.Text = "  UPdate HYK_JFFLJL  set CZR=:CZR, CZRMC=:CZRMC,CZRQ=:CZRQ where JLBH in(:JLBH,:CZJLBH) ";
                query.ParamByName("CZR").AsInteger = iLoginRYID;
                query.ParamByName("CZRMC").AsString = sLoginRYMC;
                query.ParamByName("CZRQ").AsDateTime = serverTime;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZJLBH").AsInteger = aJLBH;
                query.ExecSQL();
            }


            if (fCLJF != 0)
            {
                int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
                CrmLibProc.UpdateJFZH(out msg, query, 4, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFFLZX_CZ, iJLBH, iMDID, -iCZ * fCLJF, iLoginRYID, sLoginRYMC, "积分返礼");
            }
            foreach (LPGL_LPFF_LP one in itemTable)
            {
                if (one.fSL != 0)
                    CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, 4, iJLBH, sBGDDDM, -iCZ * one.fSL, iLoginRYID.ToString(), sLoginRYMC, "礼品发放",one.iBJ_WKC);//4:礼品发放？
            }
            if (iCZ == -1)
            {
                iJLBH = aJLBH;
            }

        }
        public static string CheckRuleCondition(int iFLGZBH, string sHYK_NO, string sConditionName)
        {
            LPGL_LPFF obj = new LPGL_LPFF();
            int BJ_DC, BJ_SR, BJ_LJ, BJ_BK = 0;
            DateTime dKSRQ = DateTime.MinValue;
            DateTime dJSRQ = DateTime.MinValue;
            bool returnValue = true;
            int A = 0;
            int CS = 0;
            int ZCS = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            string str, str2;
            str = sHYK_NO;
            str2 = str.Replace(" ", "");

            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime currentTime = CyDbSystem.GetDbServerTime(query);
                try
                {
                    query.SQL.Text = "  select G.* from HYK_LPFFGZ G where  G.JLBH=" + iFLGZBH + "";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        BJ_BK = query.FieldByName("BJ_BK").AsInteger;
                        BJ_SR = query.FieldByName("BJ_SR").AsInteger;
                        BJ_LJ = query.FieldByName("BJ_LJ").AsInteger;
                        BJ_DC = query.FieldByName("BJ_DC").AsInteger;
                        dKSRQ = query.FieldByName("KSRQ").AsDateTime;
                        dJSRQ = query.FieldByName("JSRQ").AsDateTime;
                        if (sConditionName == "BJ_DC")
                        {
                            if (BJ_DC == 1)
                            {
                                query.SQL.Clear();
                                query.Close();
                                query.SQL.Text = "SELECT COUNT(M.HYK_NO)  CS FROM HYK_JFFLJLITEM M，HYK_JFFLJL L WHERE L.JLBH=M.JLBH  AND L.CZRMC IS NULL AND FLGZBH=" + iFLGZBH + " AND HYK_NO='" + str2 + "'";
                                query.Open();
                                if (!query.IsEmpty)
                                {
                                    CS = query.FieldByName("CS").AsInteger;


                                }
                                ZCS = CS + 1;
                                if (ZCS > 1)
                                {
                                    A = 1;
                                    returnValue = false;


                                }

                            }
                            else if (BJ_DC == 2)
                            {
                                query.SQL.Clear();
                                query.Close();
                                DateTime checkTime = DateTime.Parse(currentTime.ToString("yyyy-MM-dd 00:00:00"));
                                query.SQL.Text = "  select  COUNT(I.HYK_NO)  CS from  HYK_JFFLJL F,HYK_JFFLJLITEM I where F.JLBH=I.JLBH and I.FLGZBH=" + iFLGZBH + " AND  HYK_NO='" + str2 + "'";
                                query.SQL.Add("  AND F.DJSJ> to_date( '" + checkTime + "','yyyy-MM-dd HH24:mi:ss')  AND F.DJSJ < to_date(  '" + currentTime + "','yyyy-MM-dd HH24:mi:ss')");
                                query.Open();
                                if (!query.IsEmpty)
                                {
                                    CS = query.FieldByName("CS").AsInteger;
                                }
                                ZCS = CS + 1;

                                if (ZCS > 1)
                                {
                                    A = 2;
                                    returnValue = false;



                                }

                            }
                        }
                        if (sConditionName == "BJ_SR")
                        {
                            string str3, str4;
                            str3 = sHYK_NO;
                            int a = str3.Length;
                            str4 = str3.Replace(" ", "");
                            string csrq = string.Empty;
                            int csy = 0;
                            int csr = 0;
                            int checky = 0;
                            int checkr = 0;
                            query.SQL.Clear();
                            query.Close();
                            query.SQL.Text = "select G.CSRQ from HYK_HYXX H,HYK_GRXX G where H.HYK_NO = '" + str4 + "'";
                            query.SQL.Add(" and H.HYID = G.HYID(+)");
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                if (BJ_SR == 0)
                                {
                                    returnValue = true;
                                    obj.bCHECKRESULT = returnValue;
                                    return JsonConvert.SerializeObject(obj);
                                }

                                csrq = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
                                if (csrq == "")
                                {
                                    returnValue = false;
                                    obj.bCHECKRESULT = returnValue;
                                    return JsonConvert.SerializeObject(obj);
                                }
                                csy = DateTime.Parse(csrq).Month;
                                csr = DateTime.Parse(csrq).Day;
                                checkr = DateTime.Parse(currentTime.ToString("yyyy-MM-dd 00:00:00")).Day;
                                checky = DateTime.Parse(currentTime.ToString("yyyy-MM-dd 00:00:00")).Month;
                                if (BJ_SR == 1)
                                {

                                    if (csy == checky && csr != checkr)
                                        returnValue = true;
                                    else
                                        returnValue = false;
                                }
                                else if (BJ_SR == 2)
                                {
                                    if (csy == checky && csr == checkr)
                                        returnValue = true;
                                    else
                                        returnValue = false;
                                }
                            }
                        }
                        if (sConditionName == "BJ_BK")
                        {
                            string str3, str4;
                            str3 = sHYK_NO;
                            int a = str3.Length;
                            str4 = str3.Replace(" ", "");
                            DateTime bkrq = DateTime.MinValue;
                            query.SQL.Clear();
                            query.Close();
                            query.SQL.Text = "select H.JKRQ from HYK_HYXX H where H.HYK_NO = '" + str4 + "'";
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                bkrq = query.FieldByName("JKRQ").AsDateTime;
                            }
                            if (BJ_BK == 1)
                            {
                                if (bkrq >= DateTime.Parse(dKSRQ.ToString("yyyy-MM-dd 00:00:00")) && bkrq < DateTime.Parse(dJSRQ.AddDays(1).ToString("yyyy-MM-dd 00:00:00")))
                                    returnValue = true;
                                else
                                    returnValue = false;
                            }

                        }
                        if (sConditionName == "BJ_LJ")
                        {
                            //List<Object> lst = new List<Object>();

                            double fZXFJE = 0; double fLPJE = 0;
                            //int iSJNR = 0;
                            DateTime dXFSJ = DateTime.MinValue;
                            DateTime a = DateTime.MinValue;
                            DateTime b = DateTime.MinValue;
                            //query.SQL.Text = "SELECT G.SJNR FROM HYK_LPFFGZ_XFGZ G,HYK_LPFFGZ Z WHERE G.JLBH=Z.JLBH  AND G.JLBH=" + iFLGZBH;
                            //query.Open();
                            //if (!query.IsEmpty)
                            //{
                            //    iSJNR = query.FieldByName("SJNR").AsInteger;
                            //}

                            query.SQL.Text = "SELECT F.LPJE FROM HYK_LPFFGZ_LP F,HYK_LPFFGZ Z WHERE F.JLBH=Z.JLBH  AND F.JLBH=" + iFLGZBH;
                            query.Open();
                            while (!query.Eof)
                            {
                                fLPJE = query.FieldByName("LPJE").AsFloat;

                                query.Next();

                            }
                            query.Close();



                            if (BJ_LJ == 2)
                            {
                                returnValue = true;
                                obj.bCHECKRESULT = returnValue;
                                return JsonConvert.SerializeObject(obj);





                            }

                            if (BJ_LJ == 4)
                            {
                                returnValue = true;
                                obj.bCHECKRESULT = returnValue;
                                return JsonConvert.SerializeObject(obj);


                            }


                            if (BJ_LJ == 5)
                            {
                                query.SQL.Clear();
                                query.Close();
                                List<Object> lst2 = new List<Object>();

                                a = DateTime.Parse(dKSRQ.ToString("yyyy-MM-dd 00:00:00"));
                                b = DateTime.Parse(dJSRQ.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));
                                query.SQL.Text = " SELECT SUM(XSJE1) ZXSJE ,XFSJ FROM (SELECT L.HYID ,L.XFSJ ,S.BMDM,SUM(S.XSJE) XSJE1 FROM HYXFJL L,HYXFJL_SP S,HYK_HYXX X,shbm h WHERE L.XFJLID=S.XFJLID AND X.HYID=L.HYID AND  h.bmdm=s.bmdm AND L.XFSJ>=" + "'" + a + "'";
                                query.SQL.Add(" and L.XFSJ<" + "'" + b + "'");
                                //if (iSJNR > 0)
                                //    query.SQL.Add(" and h.shbmid=" + iSJNR);

                                query.SQL.Add("AND X.HYK_NO=" + "'" + sHYK_NO + "'");
                                query.SQL.Add("GROUP BY L.HYID,L.XFSJ,S.BMDM");
                                query.SQL.Add(" UNION");
                                query.SQL.Add("  SELECT K.HYID ,K.XFSJ,P.BMDM,SUM(P.XSJE) XSJE2 FROM HYK_XFJL K,HYK_XFJL_SP P,HYK_HYXX X,shbm h WHERE K.XFJLID=P.XFJLID AND X.HYID=K.HYID and k.status=1 and h.bmdm=p.bmdm AND K.XFSJ>=" + "'" + a + "'");
                                query.SQL.Add(" and K.XFSJ<" + "'" + b + "'");
                                //if (iSJNR > 0)
                                //    query.SQL.Add(" and h.shbmid=" + iSJNR);
                                query.SQL.Add("AND X.HYK_NO=" + "'" + sHYK_NO + "'");
                                query.SQL.Add("  GROUP BY K.HYID,K.XFSJ,P.BMDM) GROUP BY XFSJ");
                                query.Open();
                                while (!query.Eof)
                                {
                                    LPGL_LPFFGZ_Proc.LPGL_LPFFGZ_XFJE obj2 = new LPGL_LPFFGZ_Proc.LPGL_LPFFGZ_XFJE();
                                    obj2.fZXFJE = query.FieldByName("ZXSJE").AsFloat;
                                    obj2.dXFSJ = query.FieldByName("XFSJ").AsDateTime;
                                    lst2.Add(obj2);
                                    query.Next();

                                }
                                query.Close();
                                if (lst2.Count != 1)
                                {
                                    returnValue = false;
                                    obj.bCHECKRESULT = returnValue;
                                    return JsonConvert.SerializeObject(obj);

                                }
                                else
                                {

                                    if (fZXFJE < fLPJE)
                                    {
                                        returnValue = false;
                                        obj.bCHECKRESULT = returnValue;
                                        return JsonConvert.SerializeObject(obj);

                                    }

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
                        throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }

            obj.bCHECKRESULT = returnValue;
            return JsonConvert.SerializeObject(obj);
        }
        //public bool CheckRuleCondition()
        //{
        //    bool returnValue = true;
        //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
        //    try
        //    {
        //        CyQuery query = new CyQuery(conn);
        //        DateTime currentTime = CyDbSystem.GetDbServerTime(query);
        //        try
        //        {
        //            query.SQL.Text = "  select G.* from HYK_LPFFGZ G where  G.JLBH=" + iFLGZBH + "";
        //            query.Open();
        //            if (!query.IsEmpty)
        //            {
        //                if (sConditionName == "BJ_DC")
        //                {
        //                    if (query.FieldByName("BJ_DC").AsInteger == 1)
        //                    {
        //                        query.SQL.Clear();
        //                        query.Close();
        //                        query.SQL.Text = "SELECT * FROM HYK_JFFLJLITEM WHERE FLGZBH=" + iFLGZBH + " AND HYK_NO='" + sHYK_NO + "'";
        //                        query.Open();
        //                        if (!query.IsEmpty)
        //                        {
        //                            returnValue = false;

        //                        }
        //                    }
        //                    else if (query.FieldByName("BJ_DC").AsInteger == 2)
        //                    {
        //                        query.SQL.Clear();
        //                        query.Close();
        //                        DateTime checkTime = DateTime.Parse(currentTime.ToString("yyyy-MM-dd 00:00:00"));
        //                        query.SQL.Text = "  select * from  HYK_JFFLJL F,HYK_JFFLJLITEM I where F.JLBH=I.JLBH and I.FLGZBH=" + iFLGZBH + " AND  HYK_NO='" + sHYK_NO + "'";
        //                        query.SQL.Add("  AND F.DJSJ> to_date( '" + checkTime + "','yyyy-MM-dd HH24:mi:ss')  AND F.DJSJ < to_date(  '" + currentTime + "','yyyy-MM-dd HH24:mi:ss')");
        //                        query.Open();
        //                        if (!query.IsEmpty)
        //                        {
        //                            returnValue = false;
        //                        }

        //                    }
        //                }
        //            }
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


        //    return returnValue;
        //}

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "J.JLBH");
            CondDict.Add("sHYK_NO", "I.HYK_NO");
            CondDict.Add("sGZMC", "G.GZMC");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("fWCLJF_OLD", "J.WCLJF_OLD");
            CondDict.Add("fCLJF", "J.CLJF");
            CondDict.Add("sZY", "J.ZY");
            CondDict.Add("iDJR", "J.DJR");
            CondDict.Add("sDJRMC", "J.DJRMC");
            CondDict.Add("dDJSJ", "J.DJSJ");
            CondDict.Add("iGZLX", "J.DJLX"); // 1 首刷礼品发放 2 办卡礼品发放 3 积分礼品发放 4 来店礼品发放
            CondDict.Add("iCZR", "J.CZR");
            CondDict.Add("sCZRMC", "J.CZRMC");
            CondDict.Add("dCZRQ", "J.CZRQ");
            CondDict.Add("iMDID", "B.MDID");
            CondDict.Add("iZXR", "J.SHR");
            CondDict.Add("sZXRMC", "J.SHRMC");
            CondDict.Add("dZXRQ", "J.SHRQ");
            query.SQL.Text = "  select J.*,I.*,I.HYK_NO,H.MDID,B.MDID, H.HY_NAME,B.BGDDMC,G.GZMC";
            query.SQL.Add("    from HYK_JFFLJL J,HYK_JFFLJLITEM I,HYK_HYXX H,HYK_BGDD B,HYKDEF D,HYK_LPFFGZ G");
            query.SQL.Add("    where J.CZDD=B.BGDDDM and J.JLBH=I.JLBH and I.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and I.FLGZBH=G.JLBH(+)");
            //query.SQL.Add("  and I.FLGZBH=0 and J.HBFS=0");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,L.LPMC from HYK_JFFLJL_LP I,HYK_JFFHLPXX L where I.JLBH=" + iJLBH;
                query.SQL.Add(" and L.LPID=I.LPID");
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPFF_LP item = new LPGL_LPFF_LP();
                    ((LPGL_LPFF)lst[0]).itemTable.Add(item);
                    CrmLibProc.SetLPXX(item, query.FieldByName("LPID").AsInteger);
                    item.fSL = query.FieldByName("SL").AsFloat;
                    item.fLPJE = query.FieldByName("LPJE").AsFloat;
                    item.fLPJF = query.FieldByName("LPJF").AsFloat;
                    item.fKCSL = query.FieldByName("KCSL").AsFloat;
                    query.Next();
                }
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPFF obj = new LPGL_LPFF();
            if (iBJ_XSMX == 1)
            {
                obj.sLPMC = query.FieldByName("LPMC").AsString;
                obj.iFFSL = query.FieldByName("SL").AsInteger;
            }
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHBFS = query.FieldByName("HBFS").AsInteger;
            obj.iCZJLBH = query.FieldByName("CZJLBH").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iCZR = query.FieldByName("CZR").AsInteger;
            obj.sCZRMC = query.FieldByName("CZRMC").AsString;
            obj.dCZRQ = FormatUtils.DateToString(query.FieldByName("CZRQ").AsDateTime);
            obj.iZXR = query.FieldByName("SHR").AsInteger;
            obj.sZXRMC = query.FieldByName("SHRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("SHRQ").AsDateTime);
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.dCLRQ = FormatUtils.DatetimeToString(query.FieldByName("CLRQ").AsDateTime);
            obj.fCLJF = query.FieldByName("CLJF").AsFloat;
            obj.fFLJE = query.FieldByName("FLJE").AsFloat;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.fWCLJF_OLD = query.FieldByName("WCLJF_OLD").AsFloat;
            obj.fSJFLJE = query.FieldByName("SJFLJE").AsFloat;
            obj.fKYXFJE = query.FieldByName("KYXFJE").AsFloat;
            obj.sCLIENT_IP_ADDRESS = query.FieldByName("CLIENT_IP_ADDRESS").AsString;
            obj.iDYCS = query.FieldByName("DYCS").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.fWCLXFJE = query.FieldByName("WCLXFJE").AsFloat;
            obj.dYHQJSRQ = FormatUtils.DatetimeToString(query.FieldByName("YHQJSRQ").AsDateTime);
            obj.fCLJE = query.FieldByName("CLJE").AsFloat;
            obj.fLPJSJE = query.FieldByName("LPJSJE").AsFloat;
            obj.fLJJSJE = query.FieldByName("LJJSJE").AsFloat;
            obj.fYXXFJE = query.FieldByName("YXXFJE").AsFloat;
            obj.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
            obj.iFLGZBH = query.FieldByName("FLGZBH").AsInteger;
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            return obj;
        }

        //礼品冲正
        public override void UnExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "select * from HYK_JFFLJL where CZJLBH=:JLBH and BJ_CZ=:BJ_CZ and CZJLBH!=0 ";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BJ_CZ").AsInteger = 1;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "此单据已冲正过,不可重复冲正";
                return;
            }
            query.Close();
            // serverTime = CyDbSystem.GetDbServerTime(query);
            iJLBH = SeqGenerator.GetSeq("HYK_JFFLJL");
            if (iCZJLBH == 0)
            {
                msg = "冲正失败CZJLBH不能为零";
                return;
            }
            query.SQL.Text = "insert into HYK_JFFLJL(JLBH,HBFS,CZJLBH,DJR,DJRMC,DJSJ,CZDD,ZY,CLRQ,CLJF,FLJE,DJLX,";
            query.SQL.Add(" WCLJF_OLD,SJFLJE,KYXFJE,CLIENT_IP_ADDRESS,DYCS)");
            query.SQL.Add(" values(:JLBH,:HBFS,:CZJLBH,:DJR,:DJRMC,:DJSJ,:CZDD,:ZY,:CLRQ,:CLJF,:FLJE,:DJLX,");
            query.SQL.Add(" :WCLJF_OLD,:SJFLJE,:KYXFJE,:CLIENT_IP_ADDRESS,:DYCS)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HBFS").AsInteger = 0;
            query.ParamByName("CZJLBH").AsInteger = iCZJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CLRQ").AsDateTime = FormatUtils.ParseDateString(dCLRQ);
            query.ParamByName("CLJF").AsFloat = fCLJF;
            query.ParamByName("FLJE").AsFloat = fFLJE;
            query.ParamByName("DJLX").AsInteger = iDJLX;//礼品发放的方式：日常发放0  首刷礼品发放1 办卡礼品发放2 积分礼品发放3 来店礼品发放4 生日礼品发放5    

            query.ParamByName("WCLJF_OLD").AsFloat = fWCLJF_OLD;
            query.ParamByName("SJFLJE").AsFloat = fSJFLJE;
            query.ParamByName("KYXFJE").AsFloat = fKYXFJE;
            query.ParamByName("CLIENT_IP_ADDRESS").AsString = sCLIENT_IP_ADDRESS;
            query.ParamByName("DYCS").AsInteger = iDYCS;

            int rows = query.ExecSQL();
            if (rows <= 0)
            {
                msg = "冲正失败";
                return;
            }
            if (fCLJF != 0)
            {
                int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
                CrmLibProc.UpdateJFZH(out msg, query, 4, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFFLZX_CZ, iJLBH, iMDID, fCLJF, iLoginRYID, sLoginRYMC, "礼品发放");
            }
            foreach (LPGL_LPFF_LP one in itemTable)
            {
                if (one.fSL != 0)
                {
                    CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, 4, iJLBH, sBGDDDM, one.fSL, iLoginRYID.ToString(), sLoginRYMC, "礼品发放");//4:礼品发放？
                }
            }
            ExecTableJLBH(query, "HYK_JFFLJL", serverTime, iCZJLBH ,"JLBH", "CZR", "CZRMC", "CZRQ");

        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFFLJL", serverTime, "JLBH", "SHR", "SHRMC", "SHRQ");
        }

    }
}