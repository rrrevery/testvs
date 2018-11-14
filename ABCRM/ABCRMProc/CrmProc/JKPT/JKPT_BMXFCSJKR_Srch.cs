using BF;

using BF.Pub;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace BF.CrmProc.JKPT
{
    public class JKPT_BMXFCSJKR_Srch: BASECRMClass
    {
        public string sDEPTID = string.Empty;
        public int iXFHYSL = 0;
        public int iXFCS = 0;
        public int iMAX_XFCS = 0;
        public double fXFCS_5 = 0;
        public double fXFCS_10 = 0;
        public double fXFCS_20 = 0;
        public string dRQ = string.Empty;
        public string sBMMC = string.Empty;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iMDID", "R.MDID");
            CondDict.Add("sSHDM", "B.SHDM");
            CondDict.Add("sDEPTID", "R.DEPTID");
            CondDict.Add("dRQ", "R.RQ");
           

            query.SQL.Text = "select case when R.DEPTID='' then '00' else R.DEPTID end DEPTID,B.BMMC,XFHYSL,XFCS,MAX_XFCS,XFCS_5,XFCS_10,XFCS_20, ";
            query.SQL.Add("case when MAX_XFCS > " + iMAX_XFCS + " then " + iMAX_XFCS);
            query.SQL.Add(" else MAX_XFCS");
            query.SQL.Add(" end MAX_XFCS_NEW");
            query.SQL.Add(" from HYK_XFFX_BM_R R,SHBM B where R.DEPTID=B.BMDM ");

            SetSearchQuery(query, lst);
            query.Close();


            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            JKPT_BMXFCSJKR_Srch obj = new JKPT_BMXFCSJKR_Srch();
            obj.sDEPTID = query.FieldByName("DEPTID").AsString;
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.iXFHYSL = query.FieldByName("XFHYSL").AsInteger;
            obj.iXFCS = query.FieldByName("XFCS").AsInteger;
            obj.fXFCS_5 = query.FieldByName("XFCS_5").AsFloat;
            obj.fXFCS_10 = query.FieldByName("XFCS_10").AsFloat;
            obj.fXFCS_20 = query.FieldByName("XFCS_20").AsFloat;
            obj.iMAX_XFCS = query.FieldByName("MAX_XFCS_NEW").AsInteger;
            return obj;
        }
    }
}
