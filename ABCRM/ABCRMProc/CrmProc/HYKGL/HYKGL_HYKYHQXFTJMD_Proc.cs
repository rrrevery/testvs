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
    public class HYKGL_HYKYHQXFTJMD_Proc : YHQDEF
    {
        public string dRQ = string.Empty;
        public int iHYKTYPE;
        public string sHYKNAME = string.Empty;
        public string sMDFWDM = string.Empty;
        public int iMDID;
        public string sMDMC = string.Empty;
        public string sSKTNO = string.Empty;
        public double fXFJE = 0;
        public double fTZJE = 0;
        public string sHYK_NO = string.Empty;
        public string sZY = string.Empty;
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fYE = 0;
        public string dCLSJ = string.Empty;



        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            if (iSEARCHMODE == 0)
            {
                CondDict.Add("dRQ", "B.RQ");
                CondDict.Add("iYHQID", "B.YHQID");
                CondDict.Add("iMDID", "B.MDID");
                CondDict.Add("iHYKTYPE", "B.HYKTYPE");
                query.SQL.Text = " select Y.MDMC,Y.MDID,B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDFWDM,SUM(B.XFJE) XFJE,sum(B.TZJE) TZJE";
                query.SQL.Add("  from HYK_YHQ_XFRBB B,HYKDEF D,MDDY Y,YHQDEF F");
                query.SQL.Add("    where B.HYKTYPE=D.HYKTYPE  and B.MDID=Y.MDID  and B.YHQID=F.YHQID");
                MakeSrchCondition(query, " GROUP BY Y.MDMC,Y.MDID,B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDFWDM");
            }
            if (iSEARCHMODE == 1)
            {
                CondDict.Add("dRQ", "B.RQ");
                query.SQL.Text = "  select B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDID,Y.MDMC,B.SKTNO, SUM(B.XFJE) XFJE ,sum(B.TZJE) TZJE";
                query.SQL.Add("  from HYK_YHQ_XFRBB B,HYKDEF D,MDDY Y,YHQDEF F ");
                query.SQL.Add("   where B.HYKTYPE=D.HYKTYPE  and B.MDID=Y.MDID and B.YHQID=F.YHQID  ");
                query.SQL.Add("   and B.MDID=" + iMDID);
                query.SQL.Add("   and B.HYKTYPE=" + iHYKTYPE);
                query.SQL.Add("   and B.YHQID=" + iYHQID);
                MakeSrchCondition(query, "  group by  B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDID,Y.MDMC,B.SKTNO");
            }
            if (iSEARCHMODE == 2)
            {
                CondDict.Add("dRQ", "L.CRMJZRQ");
                query.SQL.Text = "  select  L.HYID,X.HYK_NO,L.CLSJ,L.MDID,Y.MDMC,L.SKTNO,L.YHQID,F.YHQMC,L.JLBH,L.ZY,L.JFJE,L.DFJE,L.YE,L.JYBH,L.JYID";
                query.SQL.Add("   from  HYK_YHQCLJL L,HYK_HYXX X,MDDY Y ,YHQDEF F ");
                query.SQL.Add("   where CLLX in (7,11) and L.HYID=X.HYID  and L.MDID=Y.MDID and F.YHQID=L.YHQID  ");
                query.SQL.Add("   and L.MDID=" + iMDID);
                query.SQL.Add("   and L.YHQID=" + iYHQID);
                query.SQL.Add("   and L.SKTNO='" + sSKTNO + "'");
                query.SQL.Add("   and X.HYKTYPE=" + iHYKTYPE);
                MakeSrchCondition(query);
            }
            SetSearchQuery(query, lst, false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKYHQXFTJMD_Proc item = new HYKGL_HYKYHQXFTJMD_Proc();
            item.iMDID = query.FieldByName("MDID").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            if (iSEARCHMODE != 0)
            {
                item.sSKTNO = query.FieldByName("SKTNO").AsString;
            }
            if (iSEARCHMODE != 2)
            {
                item.fXFJE = query.FieldByName("XFJE").AsFloat;
                item.fTZJE = query.FieldByName("TZJE").AsFloat;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                
            }
            else
            {
                item.sSKTNO = query.FieldByName("SKTNO").AsString;
                item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                item.sZY = query.FieldByName("ZY").AsString;
                item.fJFJE = query.FieldByName("JFJE").AsFloat;
                item.fDFJE = query.FieldByName("DFJE").AsFloat;
                item.fYE = query.FieldByName("YE").AsFloat;
                item.dCLSJ = FormatUtils.DateToString(query.FieldByName("CLSJ").AsDateTime);
            }

            return item;
        }
    }
}
