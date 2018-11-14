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
    public class JKPT_SKTXFCSJKR_Srch : BASECRMClass
    {
        public string sSKTNO = string.Empty;
        public double fXFHYSL = 0, fXFCS = 0, fMAX_XFCS = 0;
        public double fXFCS_5 = 0, fXFCS_10 = 0, fXFCS_20 = 0;

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "  select SKTNO,XFHYSL,XFCS,MAX_XFCS,XFCS_5,XFCS_10,XFCS_20";
            if (fMAX_XFCS != 0)
            {
                query.SQL.Add(" ,case when MAX_XFCS>" + fMAX_XFCS + " then " + fMAX_XFCS + " else MAX_XFCS end MAX_XFCS_NEW ");
            }
            query.SQL.Add(" from HYK_XFFX_SKT_R R where 1=1 ");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            JKPT_SKTXFCSJKR_Srch obj = new JKPT_SKTXFCSJKR_Srch();
            obj.iJLBH = iJLBH + 1;
            obj.sSKTNO = query.FieldByName("SKTNO").AsString;
            obj.fXFHYSL = query.FieldByName("XFHYSL").AsFloat;
            obj.fXFCS = query.FieldByName("XFCS").AsFloat;
            obj.fMAX_XFCS = query.FieldByName("MAX_XFCS_NEW").AsFloat;
            obj.fXFCS_5 = query.FieldByName("XFCS_5").AsFloat;
            obj.fXFCS_10 = query.FieldByName("XFCS_10").AsFloat;
            obj.fXFCS_20 = query.FieldByName("XFCS_20").AsFloat;
            return obj;
        }
    }

}
