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
    public class HYKGL_KCKYETZ_Proc : HYK_DJLR_CLass
    {
        public List<HYKGL_KCKYETZItem> itemTable = new List<HYKGL_KCKYETZItem>();


        public class HYKGL_KCKYETZItem
        {
            public string sCZKHM = string.Empty;
            public double fQCYE_OLD = 0;
            public double fTZJE = 0;
            public double fQCYE_NEW = 0;
            public double fQCYE = 0;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public string dYXQ = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CARD_YETZD;CARD_YETZDITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("CARD_YETZD");
            query.SQL.Text = "insert into CARD_YETZD(JLBH,HYKTYPE,BGDDDM,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:BGDDDM,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYKGL_KCKYETZItem one in itemTable)
            {
                query.SQL.Text = "insert into CARD_YETZDITEM(JLBH,HM,QCYE_OLD,TZJE,QCYE_NEW)";
                query.SQL.Add(" values(:JLBH,:HM,:QCYE_OLD,:TZJE,:QCYE_NEW)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HM").AsString = one.sCZKHM;
                query.ParamByName("QCYE_OLD").AsFloat = one.fQCYE_OLD;
                query.ParamByName("TZJE").AsFloat = one.fTZJE;
                //  query.ParamByName("QCYE_NEW").AsFloat = one.fQCYE_NEW;
                query.ParamByName("QCYE_NEW").AsFloat = one.fQCYE_OLD + one.fTZJE;
                query.ExecSQL();
            }
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "CARD_YETZD", serverTime);
            foreach (HYKGL_KCKYETZItem one in itemTable)
            {
                query.SQL.Text = "update HYKCARD set QCYE=:QCYE where CZKHM=:CZKHM";
                //  query.ParamByName("QCYE").AsFloat = one.fTZJE;
                query.ParamByName("QCYE").AsFloat = one.fQCYE_NEW;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");

            query.SQL.Text = "select W.*,B.BGDDMC,D.HYKNAME";
            query.SQL.Add("     from CARD_YETZD W,HYK_BGDD B,HYKDEF D");
            query.SQL.Add("    where W.BGDDDM=B.BGDDDM and W.HYKTYPE=D.HYKTYPE");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "SELECT C.*,K.QCYE,K.YXQ,D.HYKTYPE,D.HYKNAME FROM CARD_YETZDITEM C,HYKCARD K,HYKDEF D where C.HM=K.CZKHM and K.HYKTYPE=D.HYKTYPE and C.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_KCKYETZItem obj = new HYKGL_KCKYETZItem();
                    ((HYKGL_KCKYETZ_Proc)lst[0]).itemTable.Add(obj);
                    obj.sCZKHM = query.FieldByName("HM").AsString;
                    obj.fQCYE_OLD = query.FieldByName("QCYE_OLD").AsFloat;
                    obj.fTZJE = query.FieldByName("TZJE").AsFloat;
                    obj.fQCYE_NEW = query.FieldByName("QCYE_NEW").AsFloat;
                    obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    if (query.FieldByName("YXQ").AsDateTime != null)
                    {
                        obj.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        obj.dYXQ = new DateTime(1, 1, 1).ToString("yyyy-MM-dd");
                    }
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_KCKYETZ_Proc obj = new HYKGL_KCKYETZ_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return obj;
        }
    }
}
