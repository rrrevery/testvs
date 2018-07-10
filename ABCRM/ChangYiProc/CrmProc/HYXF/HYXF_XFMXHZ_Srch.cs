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
    public class HYXF_XFMXHZ_Srch : HYXX_Detail
    {
        public bool bShowPublic = false;
        public double fXSJE = 0;
        public double fZKJE_HY = 0;
        public double fJF = 0;
        public double fXFCS = 0;
        public int iAGE1 = 0;
        public int iAGE2 = 0;
        public int iXFPM = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sSHDM", "Y.SHDM");
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("iMDID", "X.MDID");
            CondDict.Add("sSPFLDM", "L.SPFLDM");
            CondDict.Add("sQYDM", "Q.QYDM");
            CondDict.Add("sHYKNO", "H.HYK_NO");
            CondDict.Add("sHY_NAME", "H.HY_NAME");
            CondDict.Add("iSEX", "G.SEX");
            CondDict.Add("fJF", "X.JF");
            CondDict.Add("dRQ", "X.RQ");
            CondDict.Add("sSJHM", "G.SJHM");
            CondDict.Add("sBGDH", "G.BGDH");
            CondDict.Add("iSBID", "X.SHSBID");
            CondDict.Add("sBMDM", "X.DEPTID");
            CondDict.Add("dYXQ", "H.YXQ");
            CondDict.Add("iBJID", "J.BJID");
            CondDict.Add("sSPMC", "P.SPMC");
            CondDict.Add("iBJ_YT", "B.BJ_YT");



            StringBuilder mainCardStr = new StringBuilder(" select HYID  from  HYK_HYXX where 1=1");
            StringBuilder childCardStr = new StringBuilder(" select HYID   from  HYK_CHILD_JL where 1=1");
            query.SQL.Text = "  select A.*,rownum XFPM  from (";
            query.SQL.Text += " select X.HYID,H.HYK_NO,H.HYKTYPE,H.HY_NAME,H.GKID,D.HYKNAME,G.SEX,G.CSRQ,G.CANSMS,G.QYID,G.SFZBH,Q.QYMC,G.TXDZ,G.YZBM,G.SJHM,G.PHONE,G.BGDH,G.E_MAIL,floor(MONTHS_BETWEEN(sysdate,CSRQ)/12) AS AGE,";
            query.SQL.Add(" count(distinct X.MDID||X.SKTNO||X.JLBH) XFCS,sum(X.XSJE) XSJE,sum(X.ZKJE) ZKJE,sum(X.ZKJE_HY) ZKJE_HY,sum(X.JF) JF");
            query.SQL.Add("   from HYK_XFMX X,MDDY Y,HYK_HYXX H,HYK_GRXX G,SHSPFL L,HYK_HYQYDY Q,HYK_HYBJ J,HYKDEF D,SHSPXX P,SHBM B");
            query.SQL.Add("   where X.MDID=Y.MDID and X.HYID=H.HYID   and H.HYKTYPE=D.HYKTYPE and X.HYID=J.HYID(+)  and H.HYID=G.HYID(+)");
            query.SQL.Add("  and X.SHSPFLID=L.SHSPFLID AND P.SHSPID=X.SHSPID");
            //   query.SQL.Add("  and X.SHBMID=B.SHBMID");
            query.SQL.Add("   and Y.SHDM=B.SHDM");
            query.SQL.Add("   and X.DEPTID=B.BMDM");
            query.SQL.Add("  and G.QYID=Q.QYID(+)");
            query.SQL.Add("and exists(select 1 from HYK_HYBQ BQ,LABEL_XMITEM XM,HYK_HYXX HY ");
            query.SQL.Add(" where X.HYID=HY.HYID and  BQ.HYID(+)=HY.HYID and BQ.LABELXMID=XM.LABELXMID(+) and BQ.LABEL_VALUEID=XM.LABEL_VALUEID(+)");
            //MakeSrchCondition(query,"",false);
            query.SQL.Add("  )");
            if (sHYKNO_Begin != "" || sHYKNO_End != "")
            {
                query.SQL.Add(" AND  X.HYID IN  ");
                if (sHYKNO_Begin != "")
                {
                    mainCardStr.Append("  and  HYK_NO>='" + sHYKNO_Begin + "'");
                    childCardStr.Append("  and  HYK_NO>='" + sHYKNO_Begin + "'");
                }
                if (sHYKNO_End != "")
                {
                    mainCardStr.Append("  and  HYK_NO<='" + sHYKNO_End + "'");
                    childCardStr.Append("  and  HYK_NO<='" + sHYKNO_End + "'");
                }
                query.SQL.Add("  (" + mainCardStr.ToString() + "  UNION " + childCardStr.ToString() + "   )  ");
            }

            // query.SQL.Add("  and rownum<10");
            MakeSrchCondition(query, "group by X.HYID,H.HYK_NO,H.HYKTYPE,H.HY_NAME,H.GKID,D.HYKNAME,G.SEX,G.CSRQ,G.CANSMS,G.QYID,G.SFZBH,Q.QYMC,G.TXDZ,G.YZBM,G.SJHM,G.PHONE,G.BGDH,G.E_MAIL", false);
            query.SQL.Add(" order by XSJE desc )  A   where 1=1");
            if (iAGE1 != 0)
                query.SQL.Add("  and AGE>=" + iAGE1);
            if (iAGE2 != 0)
                query.SQL.Add("  and AGE<=" + iAGE2);
            if (iXFPM != 0)
                query.SQL.Add("  and rownum<=" + iXFPM);
            SetSearchQuery(query, lst, false);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_XFMXHZ_Srch item = new HYXF_XFMXHZ_Srch();
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.iGKID = query.FieldByName("GKID").AsInteger;
            item.iSEX = query.FieldByName("SEX").IsNull ? -1 : query.FieldByName("SEX").AsInteger;
            item.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            item.iCANSMS = query.FieldByName("CANSMS").AsInteger;
            item.iQYID = query.FieldByName("QYID").AsInteger;
            if (bShowPublic == false)
            {
                item.sSJHM = CrmLibProc.MakePrivateNumber(query.FieldByName("SJHM").AsString);
                item.sSFZBH = CrmLibProc.MakePrivateNumber(query.FieldByName("SFZBH").AsString);
            }
            else
            {
                item.sSFZBH = query.FieldByName("SFZBH").AsString;
                item.sSJHM = query.FieldByName("SJHM").AsString;
            }
            item.sQYMC = query.FieldByName("QYMC").AsString;
            item.sTXDZ = query.FieldByName("TXDZ").AsString;
            item.sYZBM = query.FieldByName("YZBM").AsString;
            item.sPHONE = query.FieldByName("PHONE").AsString;
            item.sBGDH = query.FieldByName("BGDH").AsString;
            item.sEMAIL = query.FieldByName("E_MAIL").AsString;
            item.fXSJE = query.FieldByName("XSJE").AsFloat;
            item.fJF = query.FieldByName("JF").AsFloat;
            item.fZKJE = query.FieldByName("ZKJE").AsFloat;
            item.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
            item.fXFCS = query.FieldByName("XFCS").AsFloat;
            return item;
        }

    }
}
