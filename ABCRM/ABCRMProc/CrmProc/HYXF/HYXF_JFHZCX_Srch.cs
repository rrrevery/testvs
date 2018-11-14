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
    public class HYXF_JFHZCX_Srch : BASECRMClass
    {
        public int iMDID, iSHBMID, iSHHTID = 0;
        public double fJF = 0;
        public string dRQ = string.Empty;
        public string sMDDM, sBMDM, sSHDM = string.Empty;
        public string sMDMC, sBMMC, sSHMC, sGHSDM, sGHSMC = string.Empty;


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sSHDM", "S.SHDM");
            CondDict.Add("iMDID", "X.MDID");
            CondDict.Add("dRQ", "X.RQ");
            CondDict.Add("sBMDM", "X.DEPTID");
            CondDict.Add("sGHSDM", "G.GHSDM");
            CondDict.Add("sGHSMC", "G.GSHMC");

            query.SQL.Text = "select X.MDID,X.SHHTID,X.SHBMID, round(sum(JF),2) JF, M.MDDM,M.MDMC,S.SHDM,S.SHMC,X.DEPTID,B.BMMC,G.GSHMC,G.GHSDM";
            query.SQL.Add(" from HYK_XFMX X ,MDDY M,SHDY S,SHBM B,SHHT G");
            query.SQL.Add(" where X.MDID = M.MDID and M.SHDM = S.SHDM and X.DEPTID = B.BMDM and X.SHHTID = G.SHHTID");
            //MakeSrchCondition(query, "group by X.MDID,X.SHHTID,X.SHBMID, M.MDDM, M.MDMC,S.SHDM,S.SHMC,X.DEPTID,B.BMMC,G.GSHMC,G.GHSDM");
            //query.SQL.Add("order by S.SHDM,M.MDDM,X.DEPTID");
            SetSearchQuery(query, lst, true, "group by X.MDID, X.SHHTID, X.SHBMID, M.MDDM, M.MDMC, S.SHDM, S.SHMC, X.DEPTID, B.BMMC, G.GSHMC, G.GHSDM");
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFHZCX_Srch obj = new HYXF_JFHZCX_Srch();
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iSHBMID = query.FieldByName("SHBMID").AsInteger;
            obj.iSHHTID = query.FieldByName("SHHTID").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sBMDM = query.FieldByName("DEPTID").AsString;
            obj.sMDDM = query.FieldByName("MDDM").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.sGHSMC = query.FieldByName("GSHMC").AsString;
            obj.sGHSDM = query.FieldByName("GHSDM").AsString;
            obj.fJF = query.FieldByName("JF").AsFloat;
            return obj;
        }

    }
}


