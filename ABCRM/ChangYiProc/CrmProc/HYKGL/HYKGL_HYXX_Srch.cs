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
    public class HYKGL_HYXX_Srch : HYXX_Detail
    {
        //公开显示信息
        public bool bShowPublic = false;

        //public double fYHQJE = 0;
        public string year_begin = string.Empty;
        public string month_begin = string.Empty;
        public string day_begin = string.Empty;
        public string year_end = string.Empty;
        public string month_end = string.Empty;
        public string day_end = string.Empty;
        //顾客详细信息
        public string sTJRMC = string.Empty;

        public string sKHJLRYMC = string.Empty;
        public int iXH = 0;
        public string sXH = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID","G.MDID");
            CondDict.Add("sMDMC", "G.MDMC");
            CondDict.Add("iHYID", "G.HYID");
            CondDict.Add("sHYKNO", "G.HYK_NO");
            CondDict.Add("sHY_NAME", "G.HY_NAME");
            CondDict.Add("iSEX", "G.SEX");
            CondDict.Add("dCSRQ", "G.CSRQ");
            CondDict.Add("iHYKTYPE", "G.HYKTYPE");
            CondDict.Add("sHYKNAME", "G.HYKNAME");
            CondDict.Add("sSJHM", "G.SJHM");
            CondDict.Add("iZJLXID", "G.ZJLXID");
            CondDict.Add("sSFZBH", "G.SFZBH");
            CondDict.Add("dJKRQ", "G.JKRQ");
            CondDict.Add("dYXQ", "G.YXQ");
            CondDict.Add("iXLID", "G.XLID");
            CondDict.Add("iJTSRID", "G.JTSRID");
            CondDict.Add("iJTCYID", "G.JTCYID");
            CondDict.Add("iJTGJID", "G.JTGJID");
            CondDict.Add("SR", "TO_CHAR(G.CSRQ,'MM-dd')");
            CondDict.Add("iAGE", "to_number(to_char(sysdate,'yyyy'))-to_number(to_char(G.CSRQ,'yyyy'))");

            CondDict.Add("sFXDWDM", "G.FXDWDM");
            CondDict.Add("iSTATUS", "G.STATUS");
            CondDict.Add("iDJR", "G.ADJR");
            CondDict.Add("sDJRMC", "G.ADJRMC");
            CondDict.Add("dDJSJ", "G.ADJSJ");
            CondDict.Add("fWCLJF", "G.WCLJF");
            CondDict.Add("fLJJF", "G.LJJF");
            CondDict.Add("fBQJF", "G.BQJF");
            CondDict.Add("sCANVAS", "G.CANVAS");
            CondDict.Add("iSQID", "G.SQID");
            CondDict.Add("iXQID", "G.XQID");
            CondDict.Add("sSHDM", "G.SHDM");
            sSumFiled = "fWCLJF";
            query.SQL.Text = "select HYID,HYK_NO,HY_NAME,nvl(SEX,-1) SEX,CSRQ,HYKTYPE,HYKNAME,SJHM,SFZBH,ZJLXID,JKRQ,YXQ,STATUS,WCLJF,BQJF,LJJF,QQ,WB,E_MAIL,YZBM,TXDZ";
            query.SQL.Add(" ,ZYID,XLID,JTSRID,JTGJID,JTCYID,QCPPID,CPH,JHBJ,JHJNR,PPHY,KHJLRYID,TJRMC,ADJR,ADJRMC ,ADJSJ,XQID,XQMC,QQYMC,KHJLRYMC,MDID,MDMC,SHDM,FXDWDM from (");
            query.SQL.Add("select distinct A.HYID,A.HYKTYPE,A.HYK_NO,A.HY_NAME,A.CDNR,A.JKRQ,A.YXQ,A.STATUS,A.MDID,B.*,J.WCLJF,J.BQJF,J.LJJF,BB.GK_NAME TJRMC,A.DJR ADJR,A.DJRMC ADJRMC ,A.DJSJ ADJSJ,E.XQMC,Q.QYMC QQYMC,M.SHDM,F.FXDWDM,");
            query.SQL.Add(" (select Z.PERSON_NAME from  RYXX Z where Z.PERSON_ID=B.KHJLRYID  ) KHJLRYMC,");
            query.SQL.Add(" (select X.MDMC from MDDY X where X.MDID=A.MDID) MDMC,");
            query.SQL.Add("  (select Y.HYKNAME from HYKDEF Y where Y.HYKTYPE=A.HYKTYPE) HYKNAME ");
            query.SQL.Add(" from HYK_HYXX A,HYK_GKDA B,HYK_GKDA BB,HYK_JFZH J,XQDY E,HYK_HYQYDY Q,XQDYITEM S,MDDY M,FXDWDEF F ");
            query.SQL.Add(" where A.GKID=B.GKID(+) AND A.HYID=J.HYID(+) AND B.TJRYID=BB.GKID(+)  and A.MDID=M.MDID");
            query.SQL.Add("   and B.Xqid=E.XQID(+)  and B.QYID=Q.QYID(+) and B.XQID = S.XQID(+) and A.FXDW = F.FXDWID(+)");
            query.SQL.Add("and exists(select 1 from HYK_HYBQ BQ,LABEL_XMITEM XM,HYK_HYXX X ");
            query.SQL.Add(" where X.HYID=A.HYID and  BQ.HYID(+)=X.HYID and BQ.LABELXMID=XM.LABELXMID(+) and BQ.LABEL_VALUEID=XM.LABEL_VALUEID(+)");
            if (sXH != "")
            {
                query.SQL.Add(" and XM.LABELID  in (" + sXH + "))");
            }
            else
            {
                query.SQL.Add(" )");
            }
            query.SQL.Add(" UNION");
            query.SQL.Add("  select distinct A.HYID,A.HYKTYPE,A.HYK_NO,A.HY_NAME,A.CDNR,A.JKRQ,A.YXQ,A.STATUS,A.MDID,B.*,J.WCLJF,J.BQJF,J.LJJF,BB.GK_NAME TJRMC,A.DJR ADJR,A.DJRMC ADJRMC ,A.DJSJ ADJSJ,E.XQMC,Q.QYMC QQYMC,M.SHDM,F.FXDWDM,");
            query.SQL.Add(" (select Z.PERSON_NAME from  RYXX Z where Z.PERSON_ID=B.KHJLRYID  ) KHJLRYMC,");
            query.SQL.Add(" (select X.MDMC from MDDY X where X.MDID=A.MDID) MDMC,");
            query.SQL.Add("  (select Y.HYKNAME from HYKDEF Y where Y.HYKTYPE=A.HYKTYPE) HYKNAME ");
            query.SQL.Add(" from HYK_HYXX A,HYK_GKDA B,HYK_GKDA BB,HYK_JFZH J,XQDY E,HYK_HYQYDY Q,XQDYITEM S,HYK_CHILD_JL L,MDDY M,FXDWDEF F");
            query.SQL.Add(" where A.HYID=L.HYID and A.GKID=B.GKID(+) AND A.HYID=J.HYID(+) AND B.TJRYID=BB.GKID(+) and A.MDID=M.MDID and A.FXDW = F.FXDWID(+) ");
            query.SQL.Add("   and B.Xqid=E.XQID(+)  and B.QYID=Q.QYID(+) and B.XQID = S.XQID(+)");
            query.SQL.Add("and exists(select 1 from HYK_HYBQ BQ,LABEL_XMITEM XM,HYK_CHILD_JL X ");
            query.SQL.Add(" where X.HYID=L.HYID and  BQ.HYID(+)=X.HYID and BQ.LABELXMID=XM.LABELXMID(+) and BQ.LABEL_VALUEID=XM.LABEL_VALUEID(+) ");
            if (sXH != "")
            {
                query.SQL.Add(" and XM.LABELID  in (" + sXH + "))");
            }
            else
            {
                query.SQL.Add(" )");
            }
            query.SQL.Add("  )G where 1=1  ");
            SetSearchQuery(query, lst);
            return lst;
        }

        public static string GetQYNAME(out string msg, int param)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            string tp_qymc = string.Empty;
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    #region SQL
                    string tp_qydm = string.Empty;
                    query.SQL.Text = "  select  QYDM from HYK_HYQYDY  where qyid=" + param + "";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        tp_qydm = query.FieldByName("QYDM").AsString;
                    }
                    query.Close();
                    if (tp_qydm != "")
                    {
                        query.SQL.Text = "  SELECT * FROM HYK_HYQYDY WHERE '" + tp_qydm + "' LIKE QYDM||'%' order by QYDM";
                        query.Open();
                        while (!query.Eof)
                        {
                            tp_qymc += query.FieldByName("QYMC").AsString + " ";
                            query.Next();
                        }
                        query.Close();
                    }
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
            return tp_qymc;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYXX_Srch obj = new HYKGL_HYXX_Srch();
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
            obj.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            if (bShowPublic == false)
            {
                obj.sSJHM = CrmLibProc.MakePrivateNumber(query.FieldByName("SJHM").AsString);
                obj.sSFZBH = CrmLibProc.MakePrivateNumber(query.FieldByName("SFZBH").AsString);
            }
            else
            {
                obj.sSJHM = query.FieldByName("SJHM").AsString;
                obj.sSFZBH = query.FieldByName("SFZBH").AsString;
            }
            obj.iZJLXID = query.FieldByName("ZJLXID").AsInteger;
            obj.dJKRQ = FormatUtils.DateToString(query.FieldByName("JKRQ").AsDateTime);
            obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iDJR = query.FieldByName("ADJR").AsInteger;
            obj.sDJRMC = query.FieldByName("ADJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("ADJSJ").AsDateTime);
            obj.fWCLJF = query.FieldByName("WCLJF").AsFloat;
            obj.fBQJF = query.FieldByName("BQJF").AsFloat;
            obj.fLJJF = query.FieldByName("LJJF").AsFloat;
            obj.sTJRMC = query.FieldByName("TJRMC").AsString;
            obj.sQQ = query.FieldByName("QQ").AsString;
            obj.sEMAIL = query.FieldByName("E_MAIL").AsString;
            obj.sYZBM = query.FieldByName("YZBM").AsString;
            obj.sTXDZ = query.FieldByName("QQYMC").AsString + "-" + query.FieldByName("TXDZ").AsString;
            obj.iZYID = query.FieldByName("ZYID").AsInteger;
            obj.iXLID = query.FieldByName("XLID").AsInteger;
            obj.iJTSRID = query.FieldByName("JTSRID").AsInteger;
            obj.iJTGJID = query.FieldByName("JTGJID").AsInteger;
            obj.iQCPPID = query.FieldByName("QCPPID").AsInteger;
            obj.sCPH = query.FieldByName("CPH").AsString;
            obj.iJHBJ = query.FieldByName("JHBJ").AsInteger;
            obj.dJHJNR = FormatUtils.DatetimeToString(query.FieldByName("JHJNR").AsDateTime);
            obj.sPPHY = query.FieldByName("PPHY").AsString;
            obj.iKHJLRYID = query.FieldByName("KHJLRYID").AsInteger;
            obj.sKHJLRYMC = query.FieldByName("KHJLRYMC").AsString;
            obj.sXQMC = query.FieldByName("XQMC").AsString;
            return obj;
        }

    }
}