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
    public class HYKGL_HYKFFTJ_Srch : HYK_DJLR_CLass
    {
        public int iSKSL = 0;
        public double fQCYE = 0, fYSZE = 0, fKFJE = 0, fZJJE = 0;
        public string sFS = string.Empty;
        public int iMDID;
        public string sMDMC;
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "C.JLBH");
            CondDict.Add("iHYKTYPE", "I.HYKTYPE");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("iZXR", "C.ZXR");
            CondDict.Add("sZXRMC", "C.ZXRMC");
            CondDict.Add("iSKSL", "SKSL");
            CondDict.Add("fQCYE", "QCYE");
            CondDict.Add("fKFJE", "KFJE");
            CondDict.Add("sBGDDDM", "C.BGDDDM");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("dZXRQ", "C.ZXRQ");
            query.SQL.Text = "select C.ZXR,C.ZXRMC,D.HYKNAME,B.BGDDMC,count(I.CZKHM) SKSL,sum(nvl(I.KFJE,0)) KFJE,sum(nvl(I.QCYE,0)) QCYE ";
            query.SQL.Add(" from HYK_CZKSKJL C,HYK_CZKSKJLITEM I,HYKDEF D,HYK_BGDD B");
            query.SQL.Add(" where C.JLBH=I.JLBH and I.HYKTYPE=D.HYKTYPE and C.BGDDDM=B.BGDDDM");
            SetSearchQuery(query, lst, true, "group by C.ZXR,C.ZXRMC,D.HYKNAME,B.BGDDMC");
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKFFTJ_Srch item = new HYKGL_HYKFFTJ_Srch();
            //item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.iSKSL = query.FieldByName("SKSL").AsInteger;
            //item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.fKFJE = query.FieldByName("KFJE").AsFloat;
            item.fQCYE = query.FieldByName("QCYE").AsFloat;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            //item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }
    }
}
