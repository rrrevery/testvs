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
    public class HYKGL_KCKZF : HYK_DJLR_CLass
    {
        public int iZFKSL = 0;
        public double fZFKJE = 0;
        public string sZFKYY=string.Empty;
        public int iBJ_HF = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;

        public class HYKGL_KCKZFItem
        {
            public string sCZKHM = string.Empty;
            public double fQCYE = 0;
        }
        public List<HYKGL_KCKZFItem> itemTable = new List<HYKGL_KCKZFItem>();


        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iZFKSL <= 0)
            {
                msg = "作废卡数量不能小于等于0";
                return false;
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_KCKZFJL;HYK_KCKZFJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_KCKZFJL");
            query.SQL.Text = "insert into HYK_KCKZFJL(JLBH,BGDDDM,HYKTYPE,ZFKSL,ZFKJE,DJR,DJRMC,DJSJ,ZFKYY,BJ_HF,MDID)";
            query.SQL.Add(" values(:JLBH,:BGDDDM,:HYKTYPE,:ZFKSL,:ZFKJE,:DJR,:DJRMC,:DJSJ,:ZFKYY,:BJ_HF,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZFKSL").AsInteger = iZFKSL;
            query.ParamByName("ZFKJE").AsFloat = fZFKJE;
            query.ParamByName("ZFKYY").AsString = sZFKYY;
            query.ParamByName("BJ_HF").AsInteger = iBJ_HF;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYKGL_KCKZFItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_KCKZFJLITEM(JLBH,CZKHM,QCYE)";
                query.SQL.Add(" values(:JLBH,:CZKHM,:QCYE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("CZKHM").AsString = one.sCZKHM;
                query.ParamByName("QCYE").AsFloat = one.fQCYE;
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
            ExecTable(query, "HYK_KCKZFJL", serverTime);
            if (iBJ_HF == 0)
            {
                query.SQL.Text = "insert into HYKCARD_BAK(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ,BJ_YK,MDID,YZM,FXDWID)";
                query.SQL.Add(" select C.CZKHM,CDNR,HYKTYPE,C.QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ,BJ_YK,MDID,YZM,FXDWID");
                query.SQL.Add(" from HYKCARD C,HYK_KCKZFJLITEM I where I.JLBH=:JLBH and C.CZKHM=I.CZKHM");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ExecSQL();
                query.SQL.Text = "delete from HYKCARD where CZKHM in(select CZKHM from HYK_KCKZFJLITEM where JLBH=:JLBH)";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ExecSQL();
            }
            else
            {
                query.SQL.Text = "insert into HYKCARD(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ,BJ_YK,MDID,YZM,FXDWID)";
                query.SQL.Add(" select C.CZKHM,CDNR,HYKTYPE,C.QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ,BJ_YK,MDID,YZM,FXDWID");
                query.SQL.Add(" from HYKCARD_BAK C,HYK_KCKZFJLITEM I where I.JLBH=:JLBH and C.CZKHM=I.CZKHM");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ExecSQL();
                query.SQL.Text = "delete from HYKCARD_BAK where CZKHM in(select CZKHM from HYK_KCKZFJLITEM where JLBH=:JLBH)";
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","W.JLBH");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,B.BGDDMC,D.HYKNAME,M.MDMC";
            query.SQL.Add("     from HYK_KCKZFJL W,HYK_BGDD B,HYKDEF D,MDDY M");
            query.SQL.Add("    where W.BGDDDM=B.BGDDDM and W.HYKTYPE=D.HYKTYPE");
            query.SQL.Add("   AND B.MDID=M.MDID(+)");
            SetSearchQuery(query, lst); 

            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.* from HYK_KCKZFJLITEM I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_KCKZFItem obj = new HYKGL_KCKZFItem();
                    ((HYKGL_KCKZF)lst[0]).itemTable.Add(obj);
                    obj.sCZKHM = query.FieldByName("CZKHM").AsString;
                    obj.fQCYE = query.FieldByName("QCYE").AsFloat;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_KCKZF obj = new HYKGL_KCKZF();
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
            obj.sZFKYY = query.FieldByName("ZFKYY").AsString;
            obj.iZFKSL = query.FieldByName("ZFKSL").AsInteger;
            obj.fZFKJE = query.FieldByName("ZFKJE").AsFloat;
            obj.iBJ_HF = query.FieldByName("BJ_HF").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
    }

}
