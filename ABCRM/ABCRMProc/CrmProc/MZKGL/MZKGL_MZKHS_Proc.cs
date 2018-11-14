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
    public class MZKGL_MZKHS_Proc : HYK_DJLR_CLass
    {
        public int iTKSL = 0;
        public double fJE = 0;
        public double fTKJE = 0;
        public int iTKFS = 0;
        public int iMDID = 0;
        public int iBGR = 0;
        public string sBGRMC = string.Empty;
        public int iFXDWID = 0;
        public string dYXQ_NEW = FormatUtils.DatetimeToString(DateTime.MinValue);

        public string sFXDWMC = string.Empty;
        public List<HYKGL_HYKHSItem> itemTable = new List<HYKGL_HYKHSItem>();

        public class HYKGL_HYKHSItem
        {
            public int iHYID = 0;
            public string sHYKNO = string.Empty;
            public string sHYK_NO = string.Empty;
            public double fCZKYE_OLD = 0;
            public double fYHQYE_OLD = 0;
            public double fQCJE = 0;
            public int iINX = 0;
            public int iBJ_CHILD = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_TK;MZK_TK_ITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_TK");
            query.SQL.Text = "insert into MZK_TK(JLBH,HYKTYPE,ZY,TKSL,JE,TKJE,TKFS,DJR,DJRMC,DJSJ,MDID,BGR,BGRMC,BGDDDM,LYR,LYRMC,FXDWID)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:ZY,:TKSL,:JE,:TKJE,:TKFS,:DJR,:DJRMC,:DJSJ,:MDID,:BGR,:BGRMC,:BGDDDM,:LYR,:LYRMC,:FXDWID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("TKSL").AsInteger = iTKSL;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("TKJE").AsFloat = fTKJE;
            query.ParamByName("TKFS").AsInteger = iTKFS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("BGR").AsInteger = iBGR;
            query.ParamByName("LYR").AsInteger = iBGR;
            query.ParamByName("LYRMC").AsString = sBGRMC;
            query.ParamByName("BGRMC").AsString = sBGRMC;
            query.ParamByName("FXDWID").AsInteger = iFXDWID;
            // query.ParamByName("YXQ_NEW").AsDateTime = FormatUtils.ParseDateString(dYXQ_NEW);
            query.ExecSQL();
            foreach (HYKGL_HYKHSItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_TKITEM(JLBH,HYID,HYK_NO,CZKYE_OLD,YHQYE_OLD,JE,ZKL,TKJE,KFJE,QCJE,INX,BJ_CHILD)";
                query.SQL.Add(" values(:JLBH,:HYID,:HYK_NO,:CZKYE_OLD,:YHQYE_OLD,:JE,:ZKL,:TKJE,:KFJE,:QCJE,:INX,:BJ_CHILD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("CZKYE_OLD").AsFloat = one.fCZKYE_OLD;
                query.ParamByName("YHQYE_OLD").AsFloat = one.fYHQYE_OLD;
                query.ParamByName("JE").AsFloat = 0;
                query.ParamByName("ZKL").AsFloat = 0;
                query.ParamByName("TKJE").AsFloat = 0;
                query.ParamByName("KFJE").AsFloat = 0;
                query.ParamByName("QCJE").AsFloat = one.fQCJE;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("BJ_CHILD").AsInteger = one.iBJ_CHILD;

                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_TK", serverTime);
            query.Close();
            query.SQL.Text = "insert into MZKCARD(CZKHM,CDNR,HYKTYPE,QCYE,YXTZJE,PDJE,BGDDDM,BGR,JKFS,SKJLBH,STATUS,YXQ,XKRQ)";
            query.SQL.Add(" select I.HYK_NO,H.CDNR,H.HYKTYPE,I.QCJE,0,0,H.YBGDD,:DJR,2,null,1,H.YXQ,:XKRQ");
            query.SQL.Add(" from MZK_TKITEM I,MZKXX H where I.JLBH=:JLBH and I.HYID=H.HYID");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("XKRQ").AsDateTime = serverTime;
            query.ExecSQL();
            query.SQL.Text = "update MZKXX set OLD_HYKNO=HYK_NO,HYK_NO=LPAD(to_char(HYID),15,'0')||'X',CDNR=LPAD(to_char(HYID),15,'0')||'X',STATUS=-1";
            query.SQL.Add(" where HYID in(select HYID from MZK_TKITEM I where I.JLBH=:JLBH)");
            query.ParamByName("JLBH").AsInteger = iJLBH;

            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sFXDWDM", "F.FXDWDM");
            CondDict.Add("iBGR", "W.BGR");
            CondDict.Add("sBGRMC", "W.BGRMC");
            CondDict.Add("iLYR", "W.LYR");
            CondDict.Add("sLYR", "W.LYRMC");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("sDJRMC", "W.DJRMC");
            query.SQL.Text = "select W.*,B.BGDDMC,D.HYKNAME,F.FXDWMC";
            query.SQL.Add("     from MZK_TK W,HYK_BGDD B,HYKDEF D,FXDWDEF F");
            query.SQL.Add("    where W.BGDDDM=B.BGDDDM and W.HYKTYPE=D.HYKTYPE and W.FXDWID=F.FXDWID(+)");
            SetSearchQuery(query, lst);


            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.* from MZK_TKITEM I,MZKXX H where I.JLBH=" + iJLBH;
                query.SQL.Add(" and I.HYID=H.HYID");
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKHSItem obj = new HYKGL_HYKHSItem();
                    ((MZKGL_MZKHS_Proc)lst[0]).itemTable.Add(obj);
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    obj.fCZKYE_OLD = query.FieldByName("CZKYE_OLD").AsFloat;
                    obj.fYHQYE_OLD = query.FieldByName("YHQYE_OLD").AsFloat;
                    obj.fQCJE = query.FieldByName("QCJE").AsFloat;
                    obj.iINX = query.FieldByName("INX").AsInteger;
                    obj.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKHS_Proc obj = new MZKGL_MZKHS_Proc();
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
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.iTKSL = query.FieldByName("TKSL").AsInteger;
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.fTKJE = query.FieldByName("TKJE").AsFloat;
            obj.iTKFS = query.FieldByName("TKFS").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iBGR = query.FieldByName("BGR").AsInteger;
            obj.sBGRMC = query.FieldByName("BGRMC").AsString;
            obj.iFXDWID = query.FieldByName("FXDWID").AsInteger;
            obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            obj.dYXQ_NEW = FormatUtils.DatetimeToString(query.FieldByName("YXQ_NEW").AsDateTime);
            return obj;
        }
    }
}
