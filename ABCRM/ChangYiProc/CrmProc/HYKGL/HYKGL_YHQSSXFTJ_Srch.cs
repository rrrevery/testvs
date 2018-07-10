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

using System.Collections;

namespace BF.CrmProc.HYKGL
{
    public class HYKGL_YHQSSXFTJ : YHQDEF
    {
        public string[] asFieldNames = { 
                                          
                                          
                                           "dRQ;L.CLSJ",
                                           "iYHQID;L.YHQID",
                                           "iMDID;L.MDID",
                                    
                                       };

        public string sMDMC = string.Empty;
        public int iMDID = 0;
        public double fXFJE = 0;
        public double fTKJE = 0;
        public double fTZJE = 0;
        public string sSKTNO = string.Empty;
        public string sSKYDM = string.Empty;
        public string dJSRQ = string.Empty;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("dRQ", "L.CLSJ");
            CondDict.Add("iYHQID", "L.YHQID");
            CondDict.Add("iMDID", "L.MDID");

            List<Object> lst = new List<Object>();
            if (iSEARCHMODE == 0)
            {
                query.SQL.Text = " select L.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,";
                query.SQL.Add("   SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,  ");
                query.SQL.Add("   SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,  ");
                query.SQL.Add("  SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ");
                query.SQL.Add("  from HYK_YHQCLJL L,MDDY Y,YHQDEF F ");
                query.SQL.Add("   where L.MDID=Y.MDID and L.YHQID=F.YHQID and L.CLLX in (6,7,11)  ");

                MakeSrchCondition(query, "group by L.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ");
            }
            if (iSEARCHMODE == 1)
            {
                query.SQL.Text = " select L.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO,";
                query.SQL.Add("    SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,  ");
                query.SQL.Add("   SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,  ");
                query.SQL.Add("   SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ");
                query.SQL.Add("  from HYK_YHQCLJL L,MDDY Y,YHQDEF F ");
                query.SQL.Add("   where L.MDID=Y.MDID and L.YHQID=F.YHQID and L.CLLX in (6,7,11)  ");
                query.SQL.Add("   and L.MDID=" + iMDID);
                query.SQL.Add("   and L.YHQID=" + iYHQID);
                query.SQL.Add("   and L.JSRQ='" + dJSRQ + "'");

                MakeSrchCondition(query, "group by L.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO");
            }
            if (iSEARCHMODE == 2)
            {
                query.SQL.Text = "  select L.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO,L.SKYDM ,";
                query.SQL.Add("    SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,  ");
                query.SQL.Add("   SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,  ");
                query.SQL.Add("   SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ");
                query.SQL.Add("  from HYK_YHQCLJL L,MDDY Y,YHQDEF F ");
                query.SQL.Add("   where L.MDID=Y.MDID and L.YHQID=F.YHQID and L.CLLX in (6,7,11)  ");
                query.SQL.Add("   and L.MDID=" + iMDID);
                query.SQL.Add("   and L.YHQID=" + iYHQID);
                query.SQL.Add("   and L.JSRQ='" + dJSRQ + "'");
                query.SQL.Add("   and L.SKTNO='" + sSKTNO + "'");

                MakeSrchCondition(query, "group by L.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO,L.SKYDM ");
            }

            SetSearchQuery(query, lst, false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQSSXFTJ item = new HYKGL_YHQSSXFTJ();
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.fXFJE = query.FieldByName("XFJE").AsFloat;
            item.fTKJE = query.FieldByName("TKJE").AsFloat;
            item.fTZJE = query.FieldByName("TZJE").AsFloat;
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            if (iSEARCHMODE == 1)
            {
                item.sSKTNO = query.FieldByName("SKTNO").AsString;
            }
            if (iSEARCHMODE == 2)
            {
                item.sSKTNO = query.FieldByName("SKTNO").AsString;
                item.sSKYDM = query.FieldByName("SKYDM").AsString;
            }
            return item;
        }
        public string SearchMDKTXF(ConditionCollection condition, int iMDID, int iYHQID, string dJSRQ)
        {

            List<Object> lst = new List<Object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " SELECT Y.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO,";
                    query.SQL.Add("    SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,  ");
                    query.SQL.Add("   SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,  ");
                    query.SQL.Add("   SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ");
                    query.SQL.Add("  FROM HYK_YHQCLJL L,MDDY Y,YHQDEF F ");
                    query.SQL.Add("   WHERE L.MDID=Y.MDID AND L.YHQID=F.YHQID AND L.CLLX in (6,7,11)  ");
                    query.SQL.Add("   AND L.MDID=" + iMDID);
                    query.SQL.Add("   AND L.YHQID=" + iYHQID);
                    query.SQL.Add("   AND L.JSRQ='" + dJSRQ + "'");
                    query.SQL.Add("  and rownum<10");
                    MakeSrchCondition(query, " GROUP BY Y.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYKGL_YHQSSXFTJ item = new HYKGL_YHQSSXFTJ();
                        item.iMDID = query.FieldByName("MDID").AsInteger;
                        item.sMDMC = query.FieldByName("MDMC").AsString;
                        item.iYHQID = query.FieldByName("YHQID").AsInteger;
                        item.sYHQMC = query.FieldByName("YHQMC").AsString;
                        item.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item.fTKJE = query.FieldByName("TKJE").AsFloat;
                        item.fTZJE = query.FieldByName("TZJE").AsFloat;
                        item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                        item.sSKTNO = query.FieldByName("SKTNO").AsString;
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

        public string SearchMDKTSKYXF(ConditionCollection condition, int iMDID, int iYHQID, string dJSRQ, string sSKTNO)
        {

            List<Object> lst = new List<Object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  SELECT Y.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO,L.SKYDM ,";
                    query.SQL.Add("    SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,  ");
                    query.SQL.Add("   SUM((1-abs(sign(CLLX-6))) * (nvl(DFJE,0) - nvl(JFJE,0))) TKJE ,  ");
                    query.SQL.Add("   SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE ");
                    query.SQL.Add("  FROM HYK_YHQCLJL L,MDDY Y,YHQDEF F ");
                    query.SQL.Add("   WHERE L.MDID=Y.MDID AND L.YHQID=F.YHQID AND L.CLLX in (6,7,11)  ");
                    query.SQL.Add("   AND L.MDID=" + iMDID);
                    query.SQL.Add("   AND L.YHQID=" + iYHQID);
                    query.SQL.Add("   AND L.JSRQ='" + dJSRQ + "'");
                    query.SQL.Add("   AND L.SKTNO='" + sSKTNO + "'");
                    query.SQL.Add("  and rownum<10");
                    MakeSrchCondition(query, " GROUP BY Y.MDID,Y.MDMC,L.YHQID,F.YHQMC,L.JSRQ,L.SKTNO,L.SKYDM ");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYKGL_YHQSSXFTJ item = new HYKGL_YHQSSXFTJ();
                        item.fXFJE = query.FieldByName("XFJE").AsFloat;
                        item.fTKJE = query.FieldByName("TKJE").AsFloat;
                        item.fTZJE = query.FieldByName("TZJE").AsFloat;
                        item.sSKTNO = query.FieldByName("SKTNO").AsString;
                        item.sSKYDM = query.FieldByName("SKYDM").AsString;
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
    }
}
