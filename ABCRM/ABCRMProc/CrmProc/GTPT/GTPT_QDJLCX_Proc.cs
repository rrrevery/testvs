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

namespace BF.CrmProc.GTPT
{
    public class GTPT_QDJLCX_Proc:BASECRMClass
    {
        public int iHYID = 0;
        public double fWCLJF = 0;
        public string sSHDM = string.Empty;
        public string sSHMC = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sHY_NAME = string.Empty;
        public string sHYKNAME = string.Empty;


        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("sHY_NAME","Y.HY_NAME");
            CondDict.Add("sHYK_NO","Y.HYK_NO");
            CondDict.Add("iHYKTYPE","Y.HYKTYPE");
            CondDict.Add("dRQ","S.RQ");
            CondDict.Add("sSHDM","S.SHDM");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select S.HYID,X.WCLJF,Y.HYK_NO,Y.HY_NAME,D.HYKNAME";
            query.SQL.Add(" from MOBILE_SIGN S,MOBILE_SIGNXX X,HYK_HYXX Y,HYKDEF D");
            query.SQL.Add(" where S.HYID=X.HYID  and S.HYID=Y.HYID and Y.HYKTYPE=D.HYKTYPE");
            SetSearchQuery(query, lst, true, "group by S.HYID,X.WCLJF,Y.HYK_NO,Y.HY_NAME,D.HYKNAME");
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_QDJLCX_Proc obj = new GTPT_QDJLCX_Proc();
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.fWCLJF = query.FieldByName("WCLJF").AsFloat;
            return obj;
        }
    }
}
