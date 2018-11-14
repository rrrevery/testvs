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
    public class HYKGL_HYGXSHJFXF_Srch : HYXX_Detail
    {
        public string sSHMC = string.Empty;
        public string sSHDM = string.Empty;
        public string sGXSHDM;
        public int iXFJLID;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            switch (iSEARCHMODE)
            {
                case 0:
                    query.SQL.Text = " select Y.SHDM,Y.SHMC,sum(M.XFJE) XFJE, sum(M.ZKJE) ZKJE,sum(M.WCLJF) WCLJF,";
                    query.SQL.Add("  sum(M.BQJF) BQJF,sum(M.LJXFJE) LJXFJE, sum(M.LJZKJE) LJZKJE, sum(M.LJJF) LJJF , sum(M.BNLJJF) BNLJJF ");
                    query.SQL.Add("  from HYK_MDJF M,MDDY H,HYK_HYXX X,SHDY Y");
                    query.SQL.Add("  where M.HYID=X.HYID and M.MDID=H.MDID and H.GXSHDM=Y.SHDM  ");
                    query.SQL.Add("  and M.HYID=" + iHYID);
                    SetSearchQuery(query, lst, true , "group by Y.SHDM,Y.SHMC");
                    break;
                case 1:
                    query.SQL.Text = "select D.MDMC, M.XFJE, M.ZKJE, M.WCLJF, M.BQJF, M.LJXFJE LJXFJE, nvl(M.LJZKJE,0) LJZKJE, nvl(M.LJJF,0) LJJF , M.MDID, M.BNLJJF ";
                    query.SQL.AddLine("from HYK_MDJF M,MDDY D");
                    query.SQL.AddLine("where M.MDID=D.MDID ");
                    query.SQL.AddLine("and M.HYID=" + iHYID);
                    query.SQL.AddLine("and D.GXSHDM='" + sSHDM + "'");
                    SetSearchQuery(query, lst, false);
                    break;
                case 2:
                    CondDict.Add("dRQ", "A.XFSJ");
                    query.SQL.Text = "select A.MDID FDBH,M.MDMC,A.SKTNO,A.JLBH,A.XFSJ,A.JE,A.ZK,A.JF,A.CZJE,B.HYK_NO ,A.XFJLID,A.JFBS";
                    query.SQL.Add("   from  HYXFJL A,HYK_HYXX B ,MDDY M");
                    query.SQL.Add("  where (A.HYID=B.HYID OR A.HYID_FQ=B.HYID ) and A.MDID=M.MDID  ");
                    query.SQL.Add("  and B.HYID=" + iHYID);
                    query.SQL.Add("  and A.MDID=" + iMDID);
                    query.SQL.Add("  AND A.HYID=" + iHYID);
                    SetSearchQuery(query, lst);
                    break;
                case 3:
                    query.SQL.Text = "select X.SPMC,sum(S.XSSL) XSSL,sum(S.XSJE) XSJE,sum(S.ZKJE) ZKJE,sum(S.ZKJE_HY) ZKJE_HY,sum(S.JF) JF,";
                    query.SQL.Add("  S.SPDM,S.JFDYDBH,S.JFJS,S.BJ_JFBS");
                    query.SQL.Add("  from HYXFJL_SP S ,HYXFJL L ,SHSPXX X");
                    query.SQL.Add("  where S.XFJLID=L.XFJLID and S.SHSPID=X.SHSPID");
                    query.SQL.Add("  and  L.XFJLID=" + iXFJLID);
                    SetSearchQuery(query, lst, true,"group by X.SPMC,S.SPDM,S.JFDYDBH,S.JFJS,S.BJ_JFBS");
                    break;
            }
            return lst;


        }
        public override object SetSearchData(CyQuery query)
        {
            dynamic item = null;
            switch (iSEARCHMODE)
            {
                case 0:
                case 1:
                    HYKGL_HYGXSHJFXF_Srch item_gxshsj = new HYKGL_HYGXSHJFXF_Srch();
                    if (iSEARCHMODE == 0)
                    {
                        item_gxshsj.sSHDM = query.FieldByName("SHDM").AsString;
                        item_gxshsj.sSHMC = query.FieldByName("SHMC").AsString;
                    }
                    else if (iSEARCHMODE == 1)
                    {
                        item_gxshsj.sMDMC = query.FieldByName("MDMC").AsString;
                        item_gxshsj.iMDID = query.FieldByName("MDID").AsInteger;
                    }
                    item_gxshsj.fWCLJF = query.FieldByName("WCLJF").AsFloat;
                    item_gxshsj.fXFJE = query.FieldByName("XFJE").AsFloat;
                    item_gxshsj.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    item_gxshsj.fBQJF = query.FieldByName("BQJF").AsFloat;
                    item_gxshsj.fLJXFJE = query.FieldByName("LJXFJE").AsFloat;
                    item_gxshsj.fLJZKJE = query.FieldByName("LJZKJE").AsFloat;
                    item_gxshsj.fLJJF = query.FieldByName("LJJF").AsFloat;
                    item_gxshsj.fBNLJJF = query.FieldByName("BNLJJF").AsFloat;
                    item = item_gxshsj;
                    break;
                case 2:
                    XFJL itemXF = new XFJL();
                    itemXF.iFDBH = query.FieldByName("FDBH").AsInteger;
                    itemXF.sMDMC = query.FieldByName("MDMC").AsString;
                    itemXF.sSKTNO = query.FieldByName("SKTNO").AsString;
                    itemXF.iXPH = query.FieldByName("JLBH").AsInteger;
                    itemXF.dXFSJ = FormatUtils.DatetimeToString(query.FieldByName("XFSJ").AsDateTime);
                    itemXF.fJE = query.FieldByName("JE").AsFloat;
                    itemXF.fZK = query.FieldByName("ZK").AsFloat;
                    itemXF.fJF = query.FieldByName("JF").AsFloat;
                    itemXF.fCZJE = query.FieldByName("CZJE").AsFloat;

                    itemXF.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    itemXF.iXFJLID = query.FieldByName("XFJLID").AsInteger;
                    itemXF.fJFBS = query.FieldByName("JFBS").AsFloat;
                    item = itemXF;
                    break;
                case 3:
                    XFJL_SP itemSP = new XFJL_SP();
                    itemSP.sSPMC = query.FieldByName("SPMC").AsString;
                    itemSP.fXSSL = query.FieldByName("XSSL").AsFloat;
                    itemSP.fXSJE = query.FieldByName("XSJE").AsFloat;
                    itemSP.fZKJE = query.FieldByName("ZKJE").AsFloat;
                    itemSP.fZKJE_HY = query.FieldByName("ZKJE_HY").AsFloat;
                    itemSP.sSPDM = query.FieldByName("SPDM").AsString;
                    itemSP.fJF = query.FieldByName("JF").AsFloat;
                    itemSP.iJFDYDBH = query.FieldByName("JFDYDBH").AsInteger;
                    itemSP.fJFJS = query.FieldByName("JFJS").AsFloat;
                    itemSP.iBJ_JFBS = query.FieldByName("BJ_JFBS").AsInteger;
                    item = itemSP;
                    break;
            }
            return item;
        }
    }
}
