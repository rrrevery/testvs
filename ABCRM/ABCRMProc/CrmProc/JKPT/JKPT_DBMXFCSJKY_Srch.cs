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
    public class JKPT_DBMXFCSJKY_Srch : BASECRMClass
    {
        public string sDEPTID = string.Empty;
        public int iYEARMONTH = 0;
        public string sYEARMONTH = string.Empty;
        public int iXFHYSL = 0;
        public int iXFCS = 0;
        public int iMAX_XFCS = 0;
        public double fXFCS_5 = 0;
        public double fXFCS_10 = 0;
        public double fXFCS_20 = 0;
        public string sMDMC = string.Empty;
        public int iMDID = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "R.MDID");
            query.SQL.Text = "select YEARMONTH,R.MDID,Y.MDMC,XFHYSL,XFCS,MAX_XFCS,XFCS_5,XFCS_10,XFCS_20, ";
            query.SQL.Add("case when MAX_XFCS > " + iMAX_XFCS + " then " + iMAX_XFCS);
            query.SQL.Add(" else MAX_XFCS");
            query.SQL.Add(" end MAX_XFCS_NEW");
            query.SQL.Add("  from HYK_XFFX_BM_Y R,MDDY Y where R.MDID=Y.MDID ");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            JKPT_DBMXFCSJKY_Srch obj = new JKPT_DBMXFCSJKY_Srch();
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iYEARMONTH = query.FieldByName("YEARMONTH").AsInteger;
            obj.sYEARMONTH = Convert.ToString(obj.iYEARMONTH);
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
