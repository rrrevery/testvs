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
    public class HYXF_XFMXCX_Srch :XFMX
    {
        public string sTXDZ = string.Empty;
        public string sPHONE = string.Empty;
        public string sYZBM = string.Empty;
        public string sEmail = string.Empty;
        public string sBGDH = string.Empty;
        public string sQYMC = string.Empty;
        public string sSP_ID = string.Empty;
        public string sSFZBH = string.Empty;
        public int iSEX = 0;
        public string sSEX = string.Empty;
        public string dCSRQ = string.Empty;
        public int iAGE1 = 0;
        public int iAGE2 = 0;
        public int iAGE = 0;
        public string sHYKNO_Begin = string.Empty;
        public string sHYKNO_End = string.Empty;

        public bool bShowPublic = false;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            StringBuilder mainCardStr = new StringBuilder(" select HYID  from  HYK_HYXX where 1=1");
            StringBuilder childCardStr = new StringBuilder(" select HYID   from  HYK_CHILD_JL where 1=1");

            CondDict.Add("sSHDM", "B.SHDM");
            CondDict.Add("iHYKTYPE", "F.HYKTYPE");
            CondDict.Add("iMDID", "X.MDID");
            CondDict.Add("sSPFLDM", "L.SPFLDM");
            CondDict.Add("sQYDM", "D.QYDM");
            CondDict.Add("sHY_NAME", "Y.HY_NAME");
            CondDict.Add("fXFJE", "X.XSJE");
            CondDict.Add("dTJRQ", "X.JYSJ");
            CondDict.Add("iSEX", "R.SEX");
            CondDict.Add("sBMDM", "X.DEPTID");
            CondDict.Add("sHYKNAME", "F.HYKNAME");
            CondDict.Add("sTXDZ", "R.TXDZ");
            CondDict.Add("sYZBM", "R.YZBM");
            CondDict.Add("sQYMC", "D.QYMC");
            CondDict.Add("sEmail", "R.Email");
            CondDict.Add("sSPMC", "P.SPMC");
            CondDict.Add("iBJ_YT", "B.BJ_YT");



            query.SQL.Text = " select *  from (";
            query.SQL.Add("select X.*,Y.HY_NAME,R.TXDZ,R.YZBM,R.SJHM,R.BGDH,R.E_MAIL,R.SEX,R.CSRQ,Y.HYK_NO,F.HYKNAME,D.QYMC,R.SFZBH,floor(MONTHS_BETWEEN(sysdate,CSRQ)/12) AS AGE,");
            query.SQL.Add(" B.BMMC,P.SPDM,P.SPMC,M.PERSON_NAME as KFJLMC ,G.MDMC,L.SPFLDM,L.SPFLMC");
            query.SQL.Add(" from HYK_XFMX X,HYK_HYXX Y,HYK_GRXX R,HYKDEF F,HYK_HYQYDY D,SHSPXX P,SHBM B,RYXX M,SHSPFL L,MDDY G ");//,HYXFJL J;
            query.SQL.Add(" where X.HYID=R.HYID(+)");
            query.SQL.Add(" and X.HYID=Y.HYID");
            query.SQL.Add(" and X.MDID=G.MDID(+)");
            query.SQL.Add(" and X.HYKTYPE=F.HYKTYPE");
            query.SQL.Add(" and R.QYID=D.QYID(+)");
            query.SQL.Add(" and X.SHSPID=P.SHSPID");
            query.SQL.Add(" and X.DEPTID=B.BMDM(+)");
            query.SQL.Add(" and Y.KFRYID=M.PERSON_ID(+)");
            query.SQL.Add(" and P.SHDM=B.SHDM");
            query.SQL.Add(" and X.SHSPFLID=L.SHSPFLID");
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
            MakeSrchCondition(query,"",false);
            query.SQL.Add("  )  where 1=1");
            if (iAGE1 != 0)
                query.SQL.Add("  and AGE>=" + iAGE1);
            if (iAGE2 != 0)
                query.SQL.Add("  and AGE<=" + iAGE2);
            SetSearchQuery(query, lst,false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_XFMXCX_Srch obj = new HYXF_XFMXCX_Srch();
            obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
            obj.fJF = query.FieldByName("JF").AsFloat;
            obj.sSPDM = query.FieldByName("SPDM").AsString;
            obj.sSPMC = query.FieldByName("SPMC").AsString;
            obj.fXSSL = query.FieldByName("XSSL").AsFloat;
            obj.fXSJE = query.FieldByName("XSJE").AsFloat;
            obj.sTXDZ = query.FieldByName("TXDZ").AsString;
            obj.sYZBM = query.FieldByName("YZBM").AsString;
            obj.sBGDH = query.FieldByName("BGDH").AsString;
            obj.sEmail = query.FieldByName("E_MAIL").AsString;
            obj.iSEX = query.FieldByName("SEX").AsInteger;
            if (obj.iSEX == 0)
                obj.sSEX = "男";
            else if (obj.iSEX == 1)
                obj.sSEX = "女";
            else
                obj.sSEX = "";
            obj.dCSRQ = FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime);
            obj.iAGE = query.FieldByName("AGE").AsInteger;
            obj.sQYMC = query.FieldByName("QYMC").AsString;
            obj.sDEPTID = query.FieldByName("DEPTID").AsString;
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.dJYSJ = FormatUtils.DatetimeToString(query.FieldByName("JYSJ").AsDateTime);
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iSHSPID = query.FieldByName("SHSPID").AsInteger;
            obj.sSPFLMC = query.FieldByName("SPFLMC").AsString;
            obj.sSPFLDM = query.FieldByName("SPFLDM").AsString;
            if (bShowPublic == false)
            {
                obj.sPHONE = CrmLibProc.MakePrivateNumber(query.FieldByName("SJHM").AsString);
                obj.sSFZBH = CrmLibProc.MakePrivateNumber(query.FieldByName("SFZBH").AsString);
            }
            else
            {
                obj.sSFZBH = query.FieldByName("SFZBH").AsString;
                obj.sPHONE = query.FieldByName("SJHM").AsString;

            }
            return obj;
        }
    }
}
