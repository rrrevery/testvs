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
    public class HYKGL_HYKYHQXFTJSH_Proc : YHQDEF
    {
        public string dRQ = string.Empty;
        public int iHYKTYPE;
        public string sHYKNAME = string.Empty;
        public string sMDFWDM = string.Empty;
        public int iMDID;
        public string sMDMC = string.Empty;
        public double fXFJE = 0;
        public double fTZJE = 0;
        public string sHYK_NO = string.Empty;
        public string sZY = string.Empty;
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fYE = 0;
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sSKYDM = string.Empty;
        public string sSKYMC = string.Empty;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            if (iSEARCHMODE == 0)
            {
                CondDict.Add("dRQ", "B.RQ");
                CondDict.Add("iYHQID", "B.YHQID");
                CondDict.Add("iHYKTYPE", "B.HYKTYPE");
                CondDict.Add("sSHDM", "Y.SHDM");
                query.SQL.Text = " select Y.SHDM,S.SHMC,B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,SUM(B.XFJE) XFJE,sum(B.TZJE) TZJE";
                query.SQL.Add("  from HYK_YHQ_XFRBB B,HYKDEF D,MDDY Y,YHQDEF F,SHDY S ");
                query.SQL.Add("    where B.HYKTYPE=D.HYKTYPE  and B.MDID=Y.MDID  and B.YHQID=F.YHQID and Y.SHDM=S.SHDM   ");
                MakeSrchCondition(query, " group by Y.SHDM,S.SHMC,B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC");
            }
            if (iSEARCHMODE == 1)
            {
                CondDict.Add("dRQ", "B.RQ");
                query.SQL.Text = "  select B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDID,Y.MDMC,Y.SHDM,S.SHMC,SUM(B.XFJE) XFJE ,sum(B.TZJE) TZJE";
                query.SQL.Add("  from HYK_YHQ_XFRBB B,HYKDEF D,MDDY Y,YHQDEF F,SHDY S ");
                query.SQL.Add("   where B.HYKTYPE=D.HYKTYPE  AND B.MDID=Y.MDID and B.YHQID=F.YHQID and Y.SHDM=S.SHDM  ");
                query.SQL.Add("   and Y.SHDM='" + sSHDM + "'");
                query.SQL.Add("   and B.HYKTYPE=" + iHYKTYPE);
                query.SQL.Add("   and B.YHQID=" + iYHQID);
                MakeSrchCondition(query, "  group by B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDID,Y.MDMC,Y.SHDM,S.SHMC");
            }
            if (iSEARCHMODE == 2)
            {
                CondDict.Add("dRQ", "L.CLSJ");
                query.SQL.Text = "   select L.YHQID,F.YHQMC,L.SKYDM ,L.SKYMC, SUM((1-abs(sign(CLLX-7))) * (nvl(DFJE,0) - nvl(JFJE,0))) XFJE ,SUM((1-abs(sign(CLLX-11))) * (nvl(DFJE,0) - nvl(JFJE,0))) TZJE";
                query.SQL.Add("   from HYK_YHQCLJL L,MDDY Y,YHQDEF F,HYK_HYXX X ");
                query.SQL.Add("   where L.MDID=Y.MDID and L.YHQID=F.YHQID and L.HYID=X.HYID and L.CLLX in (6,7,11) ");
                query.SQL.Add("   and L.MDID=" + iMDID);
                query.SQL.Add("   and L.YHQID=" + iYHQID);
                query.SQL.Add("   and Y.SHDM='" + sSHDM + "'");
                query.SQL.Add("   and X.HYKTYPE=" + iHYKTYPE);
                MakeSrchCondition(query, " group by L.YHQID,F.YHQMC,L.SKYDM,L.SKYMC", false);
            }
            SetSearchQuery(query, lst, false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKYHQXFTJSH_Proc item = new HYKGL_HYKYHQXFTJSH_Proc();
            item.fXFJE = query.FieldByName("XFJE").AsFloat;
            item.fTZJE = query.FieldByName("TZJE").AsFloat;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            if (iSEARCHMODE == 1)
            {
                item.iMDID = query.FieldByName("MDID").AsInteger;
                item.sMDMC = query.FieldByName("MDMC").AsString;
            }
            if (iSEARCHMODE != 2)
            {
                item.sSHDM = query.FieldByName("SHDM").AsString;
                item.sSHMC = query.FieldByName("SHMC").AsString;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            }
            else
            {
                item.sSKYDM = query.FieldByName("SKYDM").AsString;
                item.sSKYMC = query.FieldByName("SKYMC").AsString;
            }
            return item;
        }
    }
}
