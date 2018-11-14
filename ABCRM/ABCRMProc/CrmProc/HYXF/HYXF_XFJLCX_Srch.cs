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
    public class HYXF_XFJLCX_Srch : XFJL
    {
        public string sMDDM = string.Empty;
        public string sTXDZ = string.Empty;
        public string sPHONE = string.Empty;
        public string sSPMC = string.Empty;
        public double fXSJE = 0, fZKJE = 0, fZKJE_HY = 0;
        public string sXPH = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHYK_NO", "B.HYK_NO");
            CondDict.Add("iMDID", "A.MDID");
            CondDict.Add("sSKTNO", "A.SKTNO");
            CondDict.Add("iHYKTYPE", "B.HYKTYPE");
            CondDict.Add("iXFJLID", "JLBH");
            CondDict.Add("fZK", "ZK");
            CondDict.Add("fJE", "JE");
            CondDict.Add("fJF", "JF");
            CondDict.Add("dXFSJ", "A.XFSJ");
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sTXDZ", "X.TXDZ");
            CondDict.Add("sPHONE", "X.PXHONE");
            CondDict.Add("sMDMC", "Y.MDMC");
            CondDict.Add("fZK_HY", "ZK_HY");
            CondDict.Add("sHYKNAME", "F.HYKNAME");

            if (iXFJLID != 0)
            {
                query.SQL.Text = "  select L.XSSL,L.XSJE,L.ZKJE,L.ZKJE_HY,X.SPMC from HYXFJL_SP L ,SHSPXX  X ";
                query.SQL.Add("    where L.SHSPID=X.SHSPID  and  xfjlid=" + iXFJLID + "");
                query.SQL.Add(" union");
                query.SQL.Add("  select L.XSSL,L.XSJE,L.ZKJE,L.ZKJE_HY,X.SPMC from HYK_XFJL_SP L ,SHSPXX  X");
                query.SQL.Add("  where L.SHSPID=X.SHSPID  and  xfjlid=" + iXFJLID + "");
                SetSearchQuery(query, lst);
            }
            else
            {
                query.SQL.Text = " select A.MDID,Y.MDDM,Y.MDMC,A.SKTNO,A.JLBH,A.XFSJ,A.JE,A.ZK,A.ZK_HY,A.JF,A.XFJLID,B.HYK_NO,B.HYKTYPE,F.HYKNAME,X.TXDZ,X.PHONE,A.HYID,";
                query.SQL.Add("  A.SHDM,A.DJLX,A.XFJLID_OLD,A.HYID_FQ,A.HYID_TQ,A.SKYDM,A.JZRQ,A.CRMJZRQ,A.SCSJ,A.XSSL,A.XFRQ_FQ,A.BJ_CHILD,A.JFBS,A.BSFS,A.FXDW,A.PGRYID");
                query.SQL.Add(" from HYXFJL A,HYK_HYXX B ,MDDY Y,HYKDEF F,HYK_GRXX X");
                query.SQL.Add(" where B.HYID=A.HYID and A.MDID=Y.MDID and A.HYKNO=B.HYK_NO and B.HYKTYPE=F.HYKTYPE AND B.HYID=X.HYID(+)");// and A.DJLX=" + iDJLX
                MakeSrchCondition(query, "", false);
                query.SQL.Add(" union " + query.SQL.Text.Replace("HYXFJL", "HYK_XFJL"));
                query.SQL.Add(" and A.STATUS=1");// and A.DJLX=" + iDJLX
                query.SQL.Text = "select * from ( " + query.SQL.Text + "  ) ";
                SetSearchQuery(query,lst,false,"",true);
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            if (iXFJLID != 0)
            {
                HYXF_XFJLCX_Srch item = new HYXF_XFJLCX_Srch();
                item.sSPMC = query.FieldByName("SPMC").AsString;
                item.fXSJE = query.FieldByName("XSJE").AsFloat;
                item.fXSSL = query.FieldByName("XSSL").AsFloat;
                item.fZKJE = query.FieldByName("ZKJE").AsFloat;
                item.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
                return item;
            }
            else 
            {
                HYXF_XFJLCX_Srch item = new HYXF_XFJLCX_Srch();
                item.iMDID = query.FieldByName("MDID").AsInteger;
                item.sMDDM = query.FieldByName("MDDM").AsString;
                item.sMDMC = query.FieldByName("MDMC").AsString;
                item.sSKTNO = query.FieldByName("SKTNO").AsString;
                item.iXFJLID = query.FieldByName("XFJLID").AsInteger;
                item.sSHDM = query.FieldByName("SHDM").AsString;
                item.iXPH = query.FieldByName("JLBH").AsInteger;
                item.sXPH = Convert.ToString(item.iXPH);
                item.iDJLX = query.FieldByName("DJLX").AsInteger;
                item.iXFJLID_OLD = query.FieldByName("XFJLID_OLD").AsInteger;
                item.iHYID = query.FieldByName("HYID").AsInteger;
                item.iHYID_FQ = query.FieldByName("HYID_FQ").AsInteger;
                item.iHYID_TQ = query.FieldByName("HYID_TQ").AsInteger;
                item.sSKYDM = query.FieldByName("SKYDM").AsString;
                item.dXFSJ = FormatUtils.DatetimeToString(query.FieldByName("XFSJ").AsDateTime);
                item.dJZRQ = FormatUtils.DatetimeToString(query.FieldByName("JZRQ").AsDateTime);
                item.dCRMJZRQ = FormatUtils.DatetimeToString(query.FieldByName("CRMJZRQ").AsDateTime);
                item.dSCSJ = FormatUtils.DatetimeToString(query.FieldByName("SCSJ").AsDateTime);
                item.fXSSL = query.FieldByName("XSSL").AsFloat;
                item.fJE = query.FieldByName("JE").AsFloat;
                item.fZK = query.FieldByName("ZK").AsFloat;
                item.fZK_HY = query.FieldByName("ZK_HY").AsFloat;
                item.fJF = query.FieldByName("JF").AsFloat;
                // obj.iBJ_HTBSK = query.FieldByName("BJ_HTBSK").AsInteger;
                item.dXFRQ_FQ = FormatUtils.DatetimeToString(query.FieldByName("XFRQ_FQ").AsDateTime);
                item.fJFBS = query.FieldByName("JFBS").AsFloat;
                item.iBSFS = query.FieldByName("BSFS").AsInteger;
                item.sHYKNO = query.FieldByName("HYK_NO").AsString;
                item.iPGRYID = query.FieldByName("PGRYID").AsInteger;
                item.iFXDW = query.FieldByName("FXDW").AsInteger;
                item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item.sTXDZ = query.FieldByName("TXDZ").AsString;
                item.sPHONE = query.FieldByName("PHONE").AsString;
                return item;
            }
            
        }
    }
}
