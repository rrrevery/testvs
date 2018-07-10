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
    public class HYKGL_GHKLX : DZHYK_DJLR_CLass
    {
        public int iHYKTYPE_OLD = 0;
        public int iHYKTYPE_NEW = 0;
        public string sHYKNAME_OLD = string.Empty;
        public string sHYKNAME_NEW = string.Empty;
        public string sHYKHM_OLD = string.Empty;
        public string sHYKHM_NEW = string.Empty;
        public double fJF = 0;
        public double fJE = 0;
        public double fBDJF = 0;
        public string sSFZBH = string.Empty;
        public string sZJLX = string.Empty;
        public string sMDMC = string.Empty;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYID == 0)
            {
                msg = CrmLibStrings.msgNeedHYKNO;
                return false;
            }
            if (sHYKHM_NEW == "")
            {
                msg = CrmLibStrings.msgNeedNewHYKNO;
                return false;
            }
            return true;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_GHKLX", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_GHKLX");
            query.SQL.Text = "insert into HYK_GHKLX(JLBH,HYID,HYKTYPE_NEW,HYKTYPE_OLD,HYKHM_OLD,HYKHM_NEW,ZY,JF,JE,BDJF,DJSJ,DJR,DJRMC,BGDDDM)";
            query.SQL.Add(" values(:JLBH,:HYID,:HYKTYPE_NEW,:HYKTYPE_OLD,:HYKHM_OLD,:HYKHM_NEW,:ZY,:JF,:JE,:BDJF,:DJSJ,:DJR,:DJRMC,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYKTYPE_NEW").AsInteger = iHYKTYPE_NEW;
            query.ParamByName("HYKTYPE_OLD").AsInteger = iHYKTYPE_OLD;
            query.ParamByName("HYKHM_OLD").AsString = sHYKHM_OLD;
            query.ParamByName("HYKHM_NEW").AsString = sHYKHM_NEW;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("JF").AsFloat = fJF;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("BDJF").AsFloat = fBDJF;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string track = string.Empty;
            string sYZM = string.Empty;
            DateTime validDate = CrmLibProc.GetHYKYXQ(query, iHYKTYPE_NEW);
            query.Close();
            int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.Close();
            if (sHYKHM_NEW != sHYKHM_OLD)//一致则不换卡号，只更新有效期等，否则按库存卡的信息查
            {
                //不一致则换卡号，
                query.SQL.Text = "select CDNR,YZM from HYKCARD where CZKHM=:CZKHM";
                query.ParamByName("CZKHM").AsString = sHYKHM_NEW;
                query.Open();
                if (query.IsEmpty)
                    throw new Exception(CrmLibStrings.msgKCKNotFound);
                else
                {
                    track = query.FieldByName("CDNR").AsString;
                    sYZM = query.FieldByName("YZM").AsString;
                }
                query.Close();
                query.SQL.Text = "update HYK_HYXX set HYK_NO=:HYK_NO,CDNR=:CDNR,OLD_HYKNO=:OLD_HYKNO,HYKTYPE=:HYKTYPE,JKRQ=:JKRQ,YXQ=:YXQ";
                query.SQL.Add(" ,YZM=:YZM where HYID=:HYID");
                query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
                query.ParamByName("CDNR").AsString = track;
                query.ParamByName("OLD_HYKNO").AsString = sHYKHM_OLD;
                query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE_NEW;
                query.ParamByName("JKRQ").AsDateTime = serverTime.Date;
                query.ParamByName("YXQ").AsDateTime = validDate;
                query.ParamByName("YZM").AsString = sYZM;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
                query.SQL.Text = "delete from HYKCARD where CZKHM=:HYK_NO";
                query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
                query.ExecSQL();
            }
            else
            {
                query.SQL.Text = "update HYK_HYXX set HYKTYPE=:HYKTYPE,JKRQ=:JKRQ,YXQ=:YXQ where HYID=:HYID";
                query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE_NEW;
                query.ParamByName("JKRQ").AsDateTime = serverTime.Date;
                query.ParamByName("YXQ").AsDateTime = validDate;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
            }
            ExecTable(query, "HYK_GHKLX", serverTime);
            CrmLibProc.UpdateJFZH(out msg, query, 0, iHYID, BASECRMDefine.HYK_JFBDCLLX_GHKLX, iJLBH, iMDID, fBDJF, iLoginRYID, sLoginRYMC, "更换卡类型");
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("sHYKNAME", "D.HYKNAME");
            CondDict.Add("sHYKNAME_OLD", "O.HYKNAME");
            CondDict.Add("sHYKNAME_NEW", "N.HYKNAME");
            CondDict.Add("iHYKTYPE_OLD", "W.HYKTYPE_OLD");
            CondDict.Add("iHYKTYPE_NEW", "W.HYKTYPE_NEW");
            CondDict.Add("iHYID", "W.HYID");
            CondDict.Add("sZJLX", "X.NR");
            CondDict.Add("sSFZBH", "G.SFZBH");
            CondDict.Add("sHYKHM_OLD", "W.HYKHM_OLD");
            CondDict.Add("sHYKHM_NEW", "W.HYKHM_NEW");
            CondDict.Add("fBDJF", "W.BDJF");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("fJF", "W.JF");
            CondDict.Add("fJE", "W.JE");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sHY_NAME", "H.HY_NAME");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");

            query.SQL.Text = "select W.JLBH,W.HYKTYPE_OLD,W.HYKTYPE_NEW,W.HYID,W.HYKHM_OLD,W.HYKHM_NEW,W.ZY,W.JF,W.JE,W.BDJF,W.DJSJ,X.NR,";
            query.SQL.Add("  W.DJR,W.DJRMC,W.BGDDDM,W.QCYE,W.BJ_CZK,W.ZXR,W.ZXRMC,W.ZXRQ,H.HY_NAME,O.HYKNAME HYKNAME_OLD,N.HYKNAME HYKNAME_NEW,B.BGDDMC,G.SFZBH");
            query.SQL.Add("     from HYK_GHKLX W,HYK_HYXX H,HYKDEF O,HYKDEF N,HYK_BGDD B,HYK_GRXX G,HYXXXMDEF X");
            query.SQL.Add("    where W.HYID=H.HYID");
            query.SQL.Add("      and W.HYKTYPE_NEW=N.HYKTYPE and W.HYKTYPE_OLD=O.HYKTYPE and W.BGDDDM=B.BGDDDM");
            query.SQL.Add("    and H.HYID=G.HYID(+) and G.ZJLXID=X.XMID(+)");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_GHKLX item = new HYKGL_GHKLX();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYKTYPE_OLD = query.FieldByName("HYKTYPE_OLD").AsInteger;
            item.iHYKTYPE_NEW = query.FieldByName("HYKTYPE_NEW").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYKHM_OLD = query.FieldByName("HYKHM_OLD").AsString;
            item.sHYKHM_NEW = query.FieldByName("HYKHM_NEW").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.fJF = query.FieldByName("JF").AsFloat;
            item.fJE = query.FieldByName("JE").AsFloat;
            item.fBDJF = query.FieldByName("BDJF").AsFloat;
            item.sHYKNAME_OLD = query.FieldByName("HYKNAME_OLD").AsString;
            item.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sSFZBH = query.FieldByName("SFZBH").AsString;
            //item.sZJLX = query.FieldByName("ZJLX").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            //item.sMDMC = query.FieldByName("MDMC").AsString;
            item.sZJLX = query.FieldByName("NR").AsString;
            return item;
        }
    }
}
