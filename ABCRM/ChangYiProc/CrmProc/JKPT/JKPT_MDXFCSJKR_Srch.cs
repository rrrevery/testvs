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
    public class JKPT_MDXFCSJKR_Srch : BASECRMClass
    {
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string dRQ = DateTime.Now.ToString();
        public double fXFHYSL = 0;
        public double fXFCS = 0;
        public double fMAX_XFCS = 0;
        public double fXFCS_5 = 0;
        public double fXFCS_10 = 0;
        public double fXFCS_20 = 0;
        public double fXFCS_30 = 0;
        public double fXFCS_50 = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "R.MDID");
            query.SQL.Text = "select RQ,Y.MDID,Y.MDMC,XFHYSL,XFCS,MAX_XFCS,XFCS_5,XFCS_10,XFCS_20,XFCS_30,XFCS_50,case when MAX_XFCS > " + fMAX_XFCS + " then " + fMAX_XFCS + " else MAX_XFCS end MAX_XFCS_NEW ";
            query.SQL.Add(" from HYK_XFFX_R R,MDDY Y ");
            query.SQL.Add("where R.MDID=Y.MDID ");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            JKPT_MDXFCSJKR_Srch obj = new JKPT_MDXFCSJKR_Srch();
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.fXFHYSL = query.FieldByName("XFHYSL").AsInteger;
            obj.fXFCS = query.FieldByName("XFCS").AsInteger;
            obj.fMAX_XFCS = query.FieldByName("MAX_XFCS_NEW").AsInteger;
            obj.fXFCS_5 = query.FieldByName("XFCS_5").AsInteger;
            obj.fXFCS_10 = query.FieldByName("XFCS_10").AsInteger;
            obj.fXFCS_20 = query.FieldByName("XFCS_20").AsInteger;
            obj.fXFCS_30 = query.FieldByName("XFCS_30").AsInteger;
            obj.fXFCS_50 = query.FieldByName("XFCS_50").AsInteger;
            return obj;
        }
    }
}
