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
    public class MZKGL_MZKSKQKCX : DJLR_ZXQDZZ_CLass
    {
        public string sHYKNAME = string.Empty;
        public string sBDDDDM = string.Empty;
        public string sKHMC = string.Empty;
        public int iKHID = 0;
        public string sLXRXM = string.Empty;
        public string sLXRDH = string.Empty;
        public string sLXRSJ = string.Empty;
        public int iSKSL = 0;
        public double fYSZE = 0;
        public double fWFKJE = 0;
        public double fYFKJE = 0;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("dZXRQ","L.ZXRQ");
            CondDict.Add("dQDSJ", "L.QDSJ");
            CondDict.Add("sHYKNAME", "F.HYKNAME");
            CondDict.Add("iHYKTYPE", "M.HYKTYPE");
            CondDict.Add("iBJ_QD", "M.BJ_QD");
            CondDict.Add("sBGDDDM", "L.BGDDDM");
            CondDict.Add("sKHMC", "A.KHMC");
            CondDict.Add("iSTATUS", "L.STATUS");
            CondDict.Add("iJLBH", "L.JLBH");
            CondDict.Add("iKHID", "L.KHID");
            CondDict.Add("iSKSL", "L.SKSL");
            CondDict.Add("fYSZE", "L.YSZE");

            query.SQL.Text = "select distinct(L.JLBH),D.BGDDMC,L.YSZE,L.SKSL,L.ZXRQ,L.STATUS,L.QDSJ,A.KHMC,A.KHID,A.LXRXM,A.LXRDH,A.LXRSJ,";
            query.SQL.Add("   (select sum(QCYE) from MZK_SKJLITEM where JLBH=L.JLBH and nvl(BJ_QD,0)=0) WFKJE,");
            query.SQL.Add("  (select sum(QCYE) from MZK_SKJLITEM where JLBH=L.JLBH and BJ_QD=1) YFKJE ");
            query.SQL.Add(" from MZK_SKJL L,MZK_SKJLSKMX X,ZFFS S,HYK_BGDD D,MZK_KHDA A,HYKDEF F,MZK_SKJLITEM M ");
            query.SQL.Add(" where L.JLBH=X.JLBH and X.ZFFSID=S.ZFFSID and L.BGDDDM=D.BGDDDM and M.HYKTYPE=F.HYKTYPE and L.KHID=A.KHID(+)");
            query.SQL.Add("  and S.TYPE=10 and L.JLBH=M.JLBH");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                MZKGL_MZKSKQKCX item = new MZKGL_MZKSKQKCX();
                lst.Add(item);
                item.iJLBH = query.FieldByName("JLBH").AsInteger;
                item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                item.iSKSL = query.FieldByName("SKSL").AsInteger;
                item.fYSZE = query.FieldByName("YSZE").AsFloat;
                item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                item.dQDSJ = FormatUtils.DateToString(query.FieldByName("QDSJ").AsDateTime);
                item.sKHMC = query.FieldByName("KHMC").AsString;
                item.iKHID = query.FieldByName("KHID").AsInteger;
                item.sLXRXM = query.FieldByName("LXRXM").AsString;
                item.sLXRDH = query.FieldByName("LXRDH").AsString;
                item.sLXRSJ = query.FieldByName("LXRSJ").AsString;
                item.fWFKJE = query.FieldByName("WFKJE").AsFloat;
                item.fYFKJE = query.FieldByName("YFKJE").AsFloat;
                query.Next();
            }
            return lst;

        }
    }
}
