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


namespace BF.CrmProc.MZKGL
{
    public class MZKGL_KCKBGZ_Srch : HYK_DJLR_CLass
    {
        public string dRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public double fMZJE = 0;
        public int iQCSL = 0;
        public double fQCJE = 0;
        public int iJKSL = 0;
        public double fJKJE = 0;
        public int iXKSL = 0;
        public double fXKJE = 0;
        public int iBRSL = 0;
        public double fBRJE = 0;
        public double fBCJE = 0;
        public int iBCSL = 0;
        public int iHKSL = 0;
        public double fHKJE = 0;
        public int iTZSL = 0;
        public double fTZJE = 0;
        public int iFSSL = 0;
        public double fFSJE = 0;
        public int iFSTSSL = 0;
        public double fFSTSJE = 0;
        public int iXFTSSL = 0;
        public double fXFTSJE = 0;
        public int iZFSL = 0;
        public double fZFJE = 0;
        public int iJCSL = 0;
        public double fJCJE = 0;
        public int iYEARMONTH = 0;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            query.SQL.Text = "select W.*,D.HYKNAME,B.BGDDMC from MZK_KCBGZ W,HYKDEF D,HYK_BGDD B";
            query.SQL.Add(" where W.HYKTYPE=D.HYKTYPE and W.BGDDDM=B.BGDDDM");
            SetSearchQuery(query, lst);
            return lst;

        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_KCKBGZ_Srch obj = new MZKGL_KCKBGZ_Srch();
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
            obj.fMZJE = query.FieldByName("MZJE").AsFloat;
            obj.iQCSL = query.FieldByName("QCSL").AsInteger;
            obj.fQCJE = query.FieldByName("QCJE").AsFloat;
            obj.iJKSL = query.FieldByName("JKSL").AsInteger;
            obj.fJKJE = query.FieldByName("JKJE").AsFloat;
            obj.iXKSL = query.FieldByName("XKSL").AsInteger;
            obj.fXKJE = query.FieldByName("XKJE").AsFloat;
            obj.iBRSL = query.FieldByName("BRSL").AsInteger;
            obj.fBRJE = query.FieldByName("BRJE").AsFloat;
            obj.fBCJE = query.FieldByName("BCJE").AsFloat;
            obj.iBCSL = query.FieldByName("BCSL").AsInteger;
            obj.iHKSL = query.FieldByName("HKSL").AsInteger;
            obj.fHKJE = query.FieldByName("HKJE").AsFloat;
            obj.iTZSL = query.FieldByName("TZSL").AsInteger;
            obj.fTZJE = query.FieldByName("TZJE").AsFloat;
            obj.iFSSL = query.FieldByName("FSSL").AsInteger;
            obj.fFSJE = query.FieldByName("FSJE").AsFloat;
            obj.iFSTSSL = query.FieldByName("FSTSSL").AsInteger;
            obj.fFSTSJE = query.FieldByName("FSTSJE").AsFloat;
            obj.iXFTSSL = query.FieldByName("XFTSSL").AsInteger;
            obj.fXFTSJE = query.FieldByName("XFTSJE").AsFloat;
            obj.iZFSL = query.FieldByName("ZFSL").AsInteger;
            obj.fZFJE = query.FieldByName("ZFJE").AsFloat;
            obj.iJCSL = query.FieldByName("JCSL").AsInteger;
            obj.fJCJE = query.FieldByName("JCJE").AsFloat;
            obj.iYEARMONTH = query.FieldByName("YEARMONTH").AsInteger;
            return obj;
        }
    }
}
