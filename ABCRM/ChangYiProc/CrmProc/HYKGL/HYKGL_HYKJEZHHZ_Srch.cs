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
    public class HYKGL_HYKJEZHHZ_Srch : BASECRMClass
    {
        public string sHYKNO = string.Empty;
        public string sMDMC = string.Empty;
        public string dCLSJ = string.Empty;
        public string sCLLX = string.Empty;
        public double fJFJE = 0;
        public double fDFJE = 0;
        public double fYE = 0;
        public string sSKTNO = string.Empty;
        public int iMDID, iDZLX = 0;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "J.MDID");
            CondDict.Add("iCLSJ", "J.CLSJ");
            CondDict.Add("sSKTNO", "J.SKTNO");
            switch (iDZLX)
            {
                case 0://按门店汇总
                   
                    query.SQL.Text = "select J.MDID,M.MDMC,sum(J.DFJE) DFJE ,sum(J.JFJE) JFJE from HYK_JEZCLJL J,MDDY M where J.MDID = M.MDID and J.CLLX in (1,7)";
                    MakeSrchCondition(query, "group by J.MDID,M.MDMC",false);
                    query.SQL.Add("order by J.MDID");
                    SetSearchQuery(query, lst, false);
                    break;
                case 1://按收款台汇总
                    query.SQL.Text = "select J.SKTNO,M.MDMC,M.MDID,sum(J.DFJE) DFJE ,sum(J.JFJE) JFJE from HYK_JEZCLJL J,MDDY M where J.MDID=M.MDID and  J.CLLX in (1,7)";
                    MakeSrchCondition(query, "group by J.SKTNO,M.MDMC,M.MDID", false);
                    query.SQL.Add("order by J.SKTNO");
                    SetSearchQuery(query, lst, false);
                    break;
                case 2://门店明细
                    query.SQL.Text = "select to_char(J.CLSJ,'yyyy-mm-dd') CLRQ ,J.MDID,M.MDMC,sum(J.DFJE) DFJE ,sum(J.JFJE) JFJE from HYK_JEZCLJL J,MDDY M where J.MDID = M.MDID and J.CLLX in (1,7)";
                    MakeSrchCondition(query, "group by  to_char(J.CLSJ,'yyyy-mm-dd') ,J.MDID,M.MDMC", false);
                    query.SQL.Add("order by J.MDID,CLRQ");
                    SetSearchQuery(query,lst,false);
                    break;
                case 3://收款台明细
                    query.SQL.Text = "select to_char(J.CLSJ,'yyyy-mm-dd') CLRQ ,J.SKTNO,M.MDID,M.MDMC,sum(J.DFJE) DFJE ,sum(J.JFJE) JFJE from HYK_JEZCLJL J ,MDDY M where J.MDID = M.MDID and  J.CLLX in (1,7)";
                    MakeSrchCondition(query, "group by to_char(J.CLSJ,'yyyy-mm-dd') ,J.SKTNO,M.MDID,M.MDMC", false);
                    query.SQL.Add("order by J.SKTNO,CLRQ");
                    SetSearchQuery(query, lst, false);
                    break;
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKJEZHHZ_Srch item = new HYKGL_HYKJEZHHZ_Srch();
            switch (this.iDZLX)
            {
                case 0:
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.fDFJE = query.FieldByName("DFJE").AsFloat;
                    item.fJFJE = query.FieldByName("JFJE").AsFloat;
                    break;
                case 1:
                    item.sSKTNO = query.FieldByName("SKTNO").AsString;
                    item.fDFJE = query.FieldByName("DFJE").AsFloat;
                    item.fJFJE = query.FieldByName("JFJE").AsFloat;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    break;
                case 2:
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.fDFJE = query.FieldByName("DFJE").AsFloat;
                    item.fJFJE = query.FieldByName("JFJE").AsFloat;
                    item.dCLSJ = query.FieldByName("CLRQ").AsString;
                    break;
                case 3:
                    item.sSKTNO = query.FieldByName("SKTNO").AsString;
                    item.fDFJE = query.FieldByName("DFJE").AsFloat;
                    item.fJFJE = query.FieldByName("JFJE").AsFloat;
                    item.dCLSJ = query.FieldByName("CLRQ").AsString;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    break;
            }

            return item;
        }
    }
}
