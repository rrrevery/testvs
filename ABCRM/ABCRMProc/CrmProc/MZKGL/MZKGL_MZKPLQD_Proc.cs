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
using BF.CrmProc;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKPLQD : DJLR_ZX_CLass
    {
        public List<MZKGL_MZKPLQDITEM> MZKPLQDITEM = new List<MZKGL_MZKPLQDITEM>();
        public class MZKGL_MZKPLQDITEM : BASECRMClass
        {
            public int iJLBH = 0;
        }

        public List<khmx> QDKHMX = new List<khmx>();
        public class khmx
        {
            public int sHYID = 0;
        }

        public double iYSZE = 0;
        public double iSSJE = 0;
        public int iSKSL = 0;
        public int iKHID = 0;
        public string iKHMC = string.Empty;

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","L.JLBH");
            CondDict.Add("sBGDDDM","L.BGDDDM");
            CondDict.Add("dDJSJ","L.DJSJ");
            CondDict.Add("dZXRQ", "L.ZXRQ");
            CondDict.Add("sHYK_NO", "M.CZKHM_BEGIN");
            CondDict.Add("fMZJE", "M.MZJE");
            CondDict.Add("fSSJE", "L.SSJE");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select L.JLBH,L.YSZE,L.SSJE,L.SKSL,L.BGDDDM,D.BGDDMC,L.DJR,L.DJRMC,L.DJSJ,L.ZXR,L.ZXRMC,L.ZXRQ,L.KHID,A.KHMC";
            query.SQL.Add(" from MZK_SKJL L,MZK_SKJLSKMX X,MZK_SKJLKDITEM M,ZFFS S,MZK_KHDA A,HYK_BGDD D");
            query.SQL.Add(" where L.JLBH=X.JLBH and L.JLBH=M.JLBH and X.ZFFSID=S.ZFFSID and L.KHID=A.KHID(+) and L.BGDDDM=D.BGDDDM ");
            query.SQL.Add(" and QDSJ is null and L.ZXRQ is not null AND S.TYPE=6");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKPLQD bill = new MZKGL_MZKPLQD();
            bill.iJLBH = query.FieldByName("JLBH").AsInteger;
            bill.iYSZE = query.FieldByName("YSZE").AsFloat;
            bill.iSSJE = query.FieldByName("SSJE").AsFloat;
            bill.iSKSL = query.FieldByName("SKSL").AsInteger;
            bill.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            bill.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            bill.iDJR = query.FieldByName("DJR").AsInteger;
            bill.sDJRMC = query.FieldByName("DJRMC").AsString;
            bill.dDJSJ = query.FieldByName("DJSJ").AsDateTime.ToString();
            bill.iZXR = query.FieldByName("ZXR").AsInteger;
            bill.sZXRMC = query.FieldByName("ZXRMC").AsString;
            bill.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            bill.iKHID = query.FieldByName("KHID").AsInteger;
            bill.iKHMC = query.FieldByName("KHMC").AsString;
            return bill;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (MZKGL_MZKPLQDITEM one in MZKPLQDITEM)
            {
                query.SQL.Text = "select HYID from MZK_SKJLITEM where JLBH=:JLBH";
                query.ParamByName("JLBH").AsInteger = one.iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    khmx item = new khmx();
                    QDKHMX.Add(item);
                    item.sHYID = query.FieldByName("HYID").AsInteger;
                    query.Next();
                }

                foreach (khmx onemx in QDKHMX)
                {
                    query.SQL.Text = "update  MZKXX set STATUS=:STATUS WHERE HYID=:pHYID";
                    query.ParamByName("pHYID").AsInteger = onemx.sHYID;
                    query.ParamByName("STATUS").AsInteger = 0;
                    query.ExecSQL();
                }

                query.SQL.Text = "update MZK_SKJL set STATUS=2,QDSJ=:QDSJ where JLBH=:pJLBH";
                query.ParamByName("pJLBH").AsInteger = one.iJLBH;
                query.ParamByName("QDSJ").AsDateTime = serverTime;
                query.ExecSQL();

                QDKHMX.Clear();
            }
        }
    }
}
