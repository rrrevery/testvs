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

using System.Threading;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKXFTJ_Srch :HYXX_Detail
    {
        public decimal fXFJE = 0;
        public decimal fTKJE = 0;
        public decimal fTZJE = 0;
        public string sSKTNO = string.Empty;
        public class HYK_JEZCLJL : XFJL
        {
            public decimal fDFJE = 0;
            public decimal fJFJE = 0;
            public decimal fYE = 0;
            public int iJYBH = 0;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            if (iSEARCHMODE == 0)
            {
                CondDict.Add("dRQ", "B.RQ");
                CondDict.Add("iHYKTYPE", "D.HYKTYPE");
                query.SQL.Text = " SELECT B.CZMD as MDID,Y.MDMC,B.HYKTYPE,D.HYKNAME,SUM(B.XFJE) XFJE,sum(B.TKJE) TKJE,sum(nvl(B.TZJE,0)) TZJE ";
                query.SQL.Add(" FROM MZK_XFRBB B,HYKDEF D,MDDY Y ");
                query.SQL.Add(" WHERE B.HYKTYPE=D.HYKTYPE AND (D.BJ_CZK=1) AND B.CZMD=Y.MDID ");
                SetSearchQuery(query,lst,true, " group by B.CZMD,Y.MDMC,B.HYKTYPE,D.HYKNAME");
                //MakeSrchCondition(query, condition, asFieldNames, " group by B.CZMD,Y.MDMC,B.HYKTYPE,D.HYKNAME");
            }
            if (iSEARCHMODE == 1)
            {
                CondDict.Add("dRQ", "B.RQ");
                CondDict.Add("iHYKTYPE", "D.HYKTYPE");
                query.SQL.Text = "SELECT B.HYKTYPE,D.HYKNAME,B.CZMD as MDID,Y.MDMC,B.SKTNO,SUM(B.XFJE) XFJE,sum(TKJE) TKJE,sum(nvl(TZJE,0)) TZJE  ";
                query.SQL.Add("    FROM MZK_XFRBB B,HYKDEF D,MDDY Y  ");
                query.SQL.Add(" WHERE B.HYKTYPE=D.HYKTYPE AND (D.BJ_CZK=1 ) AND B.CZMD=Y.MDID  ");  //or D.BJ_CZZH=1
                query.SQL.AddLine("AND B.CZMD=" + iMDID);
                SetSearchQuery(query, lst, true, " group by B.HYKTYPE,D.HYKNAME,B.CZMD,Y.MDMC,B.SKTNO ");
                //    query.SQL.AddLine("and B.HYKTYPE=" + iHYKTYPE + "");
                //MakeSrchCondition(query, condition, asFieldNames, "GROUP BY B.HYKTYPE,D.HYKNAME,B.CZMD,Y.MDMC,B.SKTNO ");
            }

            if (iSEARCHMODE == 2)
            {
                CondDict.Add("dRQ", "L.CRMJZRQ");
                query.SQL.Text = "SELECT  L.HYID,D.HYK_NO,";
                query.SQL.Add(" L.CLSJ,L.MDID,Y.MDMC,L.SKTNO,L.JLBH,L.ZY,L.JFJE,L.DFJE,L.YE,L.JYBH,L.JYID ");
                query.SQL.Add(" from  MZK_JEZCLJL L,MZKXX D,MDDY Y,MZK_JEZH J ");
                query.SQL.Add(" WHERE CLLX in(6,7,11) AND L.HYID=D.HYID  AND L.MDID=Y.MDID ");
                query.SQL.Add("  and J.HYID=L.HYID  ");
                query.SQL.Add("  and Y.MDID=" + iMDID);
                query.SQL.Add("  AND L.SKTNO='" + sSKTNO + "'");
                SetSearchQuery(query, lst);
                //MakeSrchCondition(query, condition, asFieldNames);// AND  L.CRMJZRQ between :KSRQ and :JSRQ   ,, AND L.SKTNO=:SKTNO 
            }

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            if (iSEARCHMODE == 0 || iSEARCHMODE == 1)
            {
                MZKGL_MZKXFTJ_Srch item = new MZKGL_MZKXFTJ_Srch();
                if (iSEARCHMODE == 1)
                {
                    item.sSKTNO = query.FieldByName("SKTNO").AsString;
                }
                item.sMDMC = query.FieldByName("MDMC").AsString;
                item.iMDID = query.FieldByName("MDID").AsInteger;
                item.fXFJE = query.FieldByName("XFJE").AsDecimal;
                item.fTKJE = query.FieldByName("TKJE").AsDecimal;
                item.fTZJE = query.FieldByName("TZJE").AsDecimal;
                item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                return item;
            }
            else
            {
                HYK_JEZCLJL itemXF = new HYK_JEZCLJL();
                itemXF.iFDBH = query.FieldByName("MDID").AsInteger;
                itemXF.sMDMC = query.FieldByName("MDMC").AsString;
                itemXF.sSKTNO = query.FieldByName("SKTNO").AsString;
                itemXF.iJLBH = query.FieldByName("JLBH").AsInteger;
                itemXF.dXFSJ = FormatUtils.DatetimeToString(query.FieldByName("CLSJ").AsDateTime);
                itemXF.fJFJE = query.FieldByName("JFJE").AsDecimal;
                itemXF.fDFJE = query.FieldByName("DFJE").AsDecimal;
                itemXF.fYE = query.FieldByName("YE").AsDecimal;
                itemXF.sZY = query.FieldByName("ZY").AsString;
                itemXF.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                itemXF.iJYBH = query.FieldByName("JYBH").AsInteger;
                return itemXF;
            }
            
        }

    }
}
