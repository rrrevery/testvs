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
    public class HYKGL_JFXFMX_Srch : BASECRMClass
    {
        public string sMDMC = string.Empty;
        public int iMDID = 0;
        public int iHYID = 0;
        public int iCXID = 0;
        public int iTM = 0;
        public double fWCLJF = 0;
        public double fWCLXFJE = 0;
        public double fBQJF = 0;
        public double fXFJE = 0;
        public double fBNLJJF = 0;
        public double fBNXFJE = 0;
        public double fZKJE = 0;
        public double fLJJF = 0;
        public double fLJXFJE = 0;
        public double fLJZKJE = 0;
        public int iXFCS = 0;
        public int iTHCS = 0;
        public double fLJXFJE_JF = 0;
        public double fJCJF = 0;
        public double fWCLJF_TS = 0;
        public string sHYK_NO = string.Empty;
        public double fJF = 0;
        public double fJE = 0;
        public double fJFBS = 0;
        public string dXFSJ = string.Empty;
        public string sSKTNO = string.Empty;
        public string sSKYDM = string.Empty;
        public double fPGRYID = 0;
        public string sBMDM = string.Empty;
        public string sBMMC = string.Empty;
        public string sSPDM = string.Empty;
        public string sSPMC = string.Empty;
        public double fXSSL = 0;
        public double fXSJE = 0;
        //public double fZKJE = 0;
        public double fZKJE_HY = 0;
        //public double fJFBS = 0;
        public int iXFJLID = 0;
        public double fXSJL = 0;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            
            
            if (iSEARCHMODE == 1)
            {
                CondDict.Add("dJSRQ", "L.XFSJ");
                query.SQL.Text = " SELECT L.XFSJ,L.SKTNO,L.MDID,L.HYID,L.JLBH,L.JE,L.JF,L.JFBS,L.SKYDM,L.PGRYID,D.MDMC,D.MDID,X.HYID,L.XFJLID";
                query.SQL.Add("  FROM HYXFJL L,HYK_HYXX X,MDDY D");
                query.SQL.Add("   WHERE  L.HYID=X.HYID");
                query.SQL.Add("   AND L.MDID=D.MDID");
                query.SQL.Add("   AND L.HYID=" + iHYID);
                query.SQL.Add("   AND L.MDID=" + iMDID);
                MakeSrchCondition(query, "", false);
                query.SQL.Add("  union all " + query.SQL.Text.Replace("HYXFJL", "HYK_XFJL"));
                SetSearchQuery(query, lst, false, "", false);
            }
            if (iSEARCHMODE == 2)
            {
                query.SQL.Text = " SELECT P.BMDM,B.BMDM,B.BMMC,P.SPDM,S.SPDM,S.SPMC,P.XSSL,P.XSJE,P.ZKJE,P.ZKJE_HY ,P.JF,P.XFJLID,D.MDMC,D.MDID,X.HYID,L.XFJLID,L.MDID,L.HYID";
                query.SQL.Add("  FROM HYXFJL_SP P,HYK_HYXX X,MDDY D,SHSPXX S,SHBM B,HYXFJL L");
                query.SQL.Add("   WHERE  P.XFJLID=L.XFJLID");
                query.SQL.Add("   AND  L.MDID=D.MDID");
                query.SQL.Add("   AND  L.HYID=X.HYID");
                query.SQL.Add("   AND  P.BMDM=B.BMDM");
                query.SQL.Add("   AND  P.SPDM=S.SPDM");
                query.SQL.Add("   AND  S.SHDM=B.SHDM");
                query.SQL.Add("   AND P.XFJLID=" + iXFJLID);
                query.SQL.Add("   AND L.MDID=" + iMDID);
                query.SQL.Add("   AND L.HYID=" + iHYID);
                query.SQL.Add("  union all " + query.SQL.Text.Replace("HYXFJL_SP", "HYK_XFJL_SP").Replace("HYXFJL", "HYK_XFJL"));
                SetSearchQuery(query, lst, false, "", false);
            }
            if (iSEARCHMODE == 0)
            {
                CondDict.Add("sHYK_NO", "X.HYK_NO");
                query.SQL.Text = "select * from (";
                query.SQL.Add( " SELECT J.*,X.HYK_NO,D.MDMC");
                query.SQL.Add("  FROM HYK_MDJF J,MDDY D,HYK_HYXX X ");
                query.SQL.Add("   WHERE  J.MDID=D.MDID");
                query.SQL.Add("   AND J.HYID=X.HYID");
                query.SQL.AddLine("UNION");
                query.SQL.Add("  SELECT J.*,X.HYK_NO,D.MDMC");
                query.SQL.Add("  FROM HYK_MDJF J,MDDY D,HYK_HYXX X ,HYK_CHILD_JL C");
                query.SQL.Add("   WHERE  J.MDID=D.MDID  and X.HYID=C.HYID ");
                query.SQL.Add("   AND J.HYID=X.HYID) X where 1=1");
                MakeSrchCondition(query);
                SetSearchQuery(query, lst, false, "", false);
            }

            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            dynamic item = null;
            switch (iSEARCHMODE)
            {
                case 0:
                    HYKGL_JFXFMX_Srch item_md = new HYKGL_JFXFMX_Srch();
                    item_md.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_md.sMDMC = query.FieldByName("MDMC").AsString;
                    item_md.iHYID = query.FieldByName("HYID").AsInteger;
                    item_md.iMDID = query.FieldByName("MDID").AsInteger;
                    item_md.iXFCS = query.FieldByName("XFCS").AsInteger;
                    item_md.iTHCS = query.FieldByName("THCS").AsInteger;
                    item_md.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    item_md.fWCLXFJE = query.FieldByName("WCLXFJE").AsFloat;
                    item_md.fBQJF = query.FieldByName("BQJF").AsFloat;
                    item_md.fXFJE = query.FieldByName("XFJE").AsFloat;
                    item_md.fBNLJJF = query.FieldByName("BNLJJF").AsFloat;
                    item_md.fBNXFJE = query.FieldByName("BNXFJE").AsFloat;
                    item_md.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    item_md.fLJJF = query.FieldByName("LJJF").AsFloat;
                    item_md.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    item_md.fLJZKJE = query.FieldByName("LJZKJE").AsFloat;
                    item_md.fLJXFJE_JF = query.FieldByName("LJXFJE_JF").AsFloat;
                    item_md.fJCJF = query.FieldByName("JCJF").AsFloat;
                    item_md.fWCLJF_TS = query.FieldByName("WCLJF_TS").AsInteger;
                    item = item_md;
                    break;
                case 1:
                    HYKGL_JFXFMX_Srch item_jf = new HYKGL_JFXFMX_Srch();
                    item_jf.iMDID = query.FieldByName("MDID").AsInteger;
                    item_jf.iXFJLID = query.FieldByName("XFJLID").AsInteger;
                    item_jf.iHYID = query.FieldByName("HYID").AsInteger;
                    item_jf.sMDMC = query.FieldByName("MDMC").AsString;
                    item_jf.fJE = query.FieldByName("JE").AsFloat;
                    item_jf.fJF = query.FieldByName("JF").AsFloat;
                    item_jf.fJFBS = query.FieldByName("JFBS").AsFloat;
                    item_jf.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item_jf.dXFSJ = FormatUtils.DatetimeToString(query.FieldByName("XFSJ").AsDateTime);
                    item_jf.sSKTNO = query.FieldByName("SKTNO").AsString;
                    item_jf.sMDMC = query.FieldByName("MDMC").AsString;
                    item_jf.sSKYDM = query.FieldByName("SKYDM").AsString;
                    item_jf.fPGRYID = query.FieldByName("PGRYID").AsInteger;
                    item = item_jf;
                    break;
                case 2:
                    HYKGL_JFXFMX_Srch item_spxfmx = new HYKGL_JFXFMX_Srch();
                    item_spxfmx.iMDID = query.FieldByName("MDID").AsInteger;
                    item_spxfmx.iHYID = query.FieldByName("HYID").AsInteger;
                    item_spxfmx.sBMDM = query.FieldByName("BMDM").AsString;
                    item_spxfmx.sBMMC = query.FieldByName("BMMC").AsString;
                    item_spxfmx.sSPDM = query.FieldByName("SPDM").AsString;
                    item_spxfmx.sSPMC = query.FieldByName("SPMC").AsString;
                    item_spxfmx.fXSSL = query.FieldByName("XSSL").AsFloat;
                    item_spxfmx.fXSJE = query.FieldByName("XSJE").AsFloat;
                    item_spxfmx.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    item_spxfmx.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
                    item_spxfmx.fJF = query.FieldByName("JF").AsFloat;
                    item_spxfmx.iXFJLID = query.FieldByName("XFJLID").AsInteger;
                    item = item_spxfmx;
                    break;
            }
            return item;
        }
    }
}
