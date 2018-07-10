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
    public class HYKGL_JFRBB_Srch : HYK_DJLR_CLass
    {
        public string dRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fSQWCLJF = 0;
        public double fWCLJFBD_GHKLX = 0;
        public double fWCLJFBD_BDD = 0;
        public double fWCLJFBD_TZD = 0;
        public double fWCLJFBD_SJ = 0;
        public double fWCLJFBD_JJ = 0;
        public double fWCLJFBD_QLSJLSJ = 0;
        public double fWCLJFBD_YXQYC = 0;
        public double fWCLJFBD_XM = 0;
        public double fWCLJFBD_FL_CZ = 0;
        public double fWCLJFBD_LKHXK = 0;
        public double fWCLJFBD_LKBXK = 0;
        public double fWCLJFBD_QTXF = 0;
        public double fWCLJFBD_YQMJF = 0;
        public double fWCLJFBD_JCZZ = 0;
        public double fWCLJFBD_QJF = 0;
        public double fWCLJFBD_JFHG = 0;
        public double fWCLJFBD_JFZC = 0;
        public double fWCLJFBD_JFQL = 0;
        public double fWCLJFBD_CZKSJF = 0;
        public double fWCLJFBD_WEB = 0;
        public double fWCLJFBD_JFDX = 0;
        public double fQMWCLJF = 0;
        public int iYEARMONTH = 0;


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_JFRBB_Srch obj = new HYKGL_JFRBB_Srch();
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.fSQWCLJF = query.FieldByName("SQWCLJF").AsFloat;
            obj.fWCLJFBD_GHKLX = query.FieldByName("WCLJFBD_GHKLX").AsFloat;
            obj.fWCLJFBD_BDD = query.FieldByName("WCLJFBD_BDD").AsFloat;
            obj.fWCLJFBD_TZD = query.FieldByName("WCLJFBD_TZD").AsFloat;
            obj.fWCLJFBD_SJ = query.FieldByName("WCLJFBD_SJ").AsFloat;
            obj.fWCLJFBD_JJ = query.FieldByName("WCLJFBD_JJ").AsFloat;
            obj.fWCLJFBD_QLSJLSJ = query.FieldByName("WCLJFBD_QLSJLSJ").AsFloat;
            obj.fWCLJFBD_YXQYC = query.FieldByName("WCLJFBD_YXQYC").AsFloat;
            obj.fWCLJFBD_XM = query.FieldByName("WCLJFBD_XM").AsFloat;
            obj.fWCLJFBD_FL_CZ = query.FieldByName("WCLJFBD_FL_CZ").AsFloat;
            obj.fWCLJFBD_LKHXK = query.FieldByName("WCLJFBD_LKHXK").AsFloat;
            obj.fWCLJFBD_LKBXK = query.FieldByName("WCLJFBD_LKBXK").AsFloat;
            obj.fWCLJFBD_QTXF = query.FieldByName("WCLJFBD_QTXF").AsFloat;
            obj.fWCLJFBD_YQMJF = query.FieldByName("WCLJFBD_YQMJF").AsFloat;
            obj.fWCLJFBD_JCZZ = query.FieldByName("WCLJFBD_JCZZ").AsFloat;
            obj.fWCLJFBD_QJF = query.FieldByName("WCLJFBD_QJF").AsFloat;
            obj.fWCLJFBD_JFHG = query.FieldByName("WCLJFBD_JFHG").AsFloat;
            obj.fWCLJFBD_JFZC = query.FieldByName("WCLJFBD_JFZC").AsFloat;
            obj.fWCLJFBD_JFQL = query.FieldByName("WCLJFBD_JFQL").AsFloat;
            obj.fWCLJFBD_CZKSJF = query.FieldByName("WCLJFBD_CZKSJF").AsFloat;
            obj.fWCLJFBD_WEB = query.FieldByName("WCLJFBD_WEB").AsFloat;
            obj.fWCLJFBD_JFDX = query.FieldByName("WCLJFBD_JFDX").AsFloat;
            obj.fQMWCLJF = query.FieldByName("QMWCLJF").AsFloat;
            obj.iYEARMONTH = query.FieldByName("YEARMONTH").AsInteger;
            return obj;
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dRQ", "W.RQ");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("fSQWCLJF", "W.SQWCLJF");
            CondDict.Add("fWCLJFBD_GHKLX", "W.WCLJFBD_GHKLX");
            CondDict.Add("fWCLJFBD_TZD", "W.WCLJFBD_TZD");
            CondDict.Add("fWCLJFBD_BDD", "W.WCLJFBD_BDD");
            CondDict.Add("fWCLJFBD_SJ", "W.WCLJFBD_SJ");
            CondDict.Add("fWCLJFBD_JJ", "W.WCLJFBD_JJ");
            CondDict.Add("fWCLJFBD_YXQYC", "W.WCLJFBD_YXQYC");
            CondDict.Add("fWCLJFBD_FL_CZ", "W.WCLJFBD_FL_CZ");
            CondDict.Add("fWCLJFBD_LKHXK", "W.WCLJFBD_LKHXK");
            CondDict.Add("fWCLJFBD_QTXF", "W.WCLJFBD_QTXF");
            CondDict.Add("fWCLJFBD_JFHG", "W.WCLJFBD_JFHG");
            CondDict.Add("fWCLJFBD_YQMJF", "W.WCLJFBD_YQMJF");
            CondDict.Add("fWCLJFBD_JCZZ", "W.WCLJFBD_JCZZ");
            CondDict.Add("fWCLJFBD_QJF", "W.WCLJFBD_QJF");
            CondDict.Add("fWCLJFBD_JFZC", "W.WCLJFBD_JFZC");
            query.SQL.Text = "select W.*,D.HYKNAME from HYK_WCLJF_RBB W,HYKDEF D";
            query.SQL.Add(" where W.HYKTYPE=D.HYKTYPE");
            SetSearchQuery(query, lst);
            return lst;
        }
    }
}
