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
    public class HYKGL_HYKSJ : DZHYK_DJLR_CLass
    {
        public int iHYKTYPE_OLD = 0;
        public int iHYKTYPE_NEW = 0;
        public string sHYKNAME_OLD = string.Empty;
        public string sHYKNAME_NEW = string.Empty;
        public string sHYKHM_OLD = string.Empty;
        public string sHYKHM_NEW = string.Empty;
        public double fWCLJF_OLD, fBQJF_OLD = 0, fBQXFJE_OLD = 0, fSJJF = 0;
        public double fJE = 0;
        public string dYJKRQ;
        public int iBJ_SJ = 1, iBJ_XFJE = 0;
        public int iMDID;
        public string sMDMC = string.Empty;

        public override bool IsValidData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = "";
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




        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_SJJL;", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_SJJL");
            query.SQL.Text = "insert into HYK_SJJL(JLBH,HYID,HYKTYPE_NEW,HYKTYPE_OLD,HYKHM_OLD,HYKHM_NEW,ZY,WCLJF_OLD,";
            query.SQL.Add(" BQJF_OLD,SJJF,JE,DJSJ,DJR,DJRMC,BGDDDM,BJ_SJ,YJKRQ,BQXFJE_OLD,BJ_XFJE)");
            query.SQL.Add(" values(:JLBH,:HYID,:HYKTYPE_NEW,:HYKTYPE_OLD,:HYKHM_OLD,:HYKHM_NEW,:ZY,:WCLJF_OLD,");
            query.SQL.Add(" :BQJF_OLD,:SJJF,:JE,:DJSJ,:DJR,:DJRMC,:BGDDDM,:BJ_SJ,:YJKRQ,:BQXFJE_OLD,:BJ_XFJE)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYKTYPE_NEW").AsInteger = iHYKTYPE_NEW;
            query.ParamByName("HYKTYPE_OLD").AsInteger = iHYKTYPE_OLD;
            query.ParamByName("HYKHM_OLD").AsString = sHYKHM_OLD;
            query.ParamByName("HYKHM_NEW").AsString = sHYKHM_NEW;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("WCLJF_OLD").AsFloat = fWCLJF_OLD;
            query.ParamByName("BQJF_OLD").AsFloat = fBQJF_OLD;
            query.ParamByName("BQXFJE_OLD").AsFloat = fBQXFJE_OLD;
            query.ParamByName("SJJF").AsFloat = fSJJF;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BJ_SJ").AsInteger = iBJ_SJ;
            query.ParamByName("BJ_XFJE").AsInteger = iBJ_XFJE;
            query.ParamByName("YJKRQ").AsDateTime = FormatUtils.ParseDateString(dYJKRQ);
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string track = string.Empty;
            string sYZM = string.Empty;
            DateTime validDate = CrmLibProc.GetHYKYXQ(query, iHYKTYPE_NEW);
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
            ExecTable(query, "HYK_SJJL", serverTime);

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
            int cllx = iBJ_SJ == 1 ? BASECRMDefine.HYK_JFBDCLLX_SJHK : BASECRMDefine.HYK_JFBDCLLX_JJHK;
            double kjjf = iBJ_XFJE == 0 ? 0 : fSJJF;
            double kjje = iBJ_XFJE == 1 ? 0 : fSJJF;
            CrmLibProc.UpdateJFZH(out msg, query, 3, iHYID, cllx, iJLBH, iMDID, 0, iLoginRYID, sLoginRYMC, "", -kjjf, 0, 0, -kjje);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","W.JLBH");
            CondDict.Add("iBJ_SJ", "W.BJ_SJ");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("iHYKTYPE_OLD", "W.HYKTYPE_OLD");
            CondDict.Add("sHYKNAME_OLD", "O.HYKNAME");
            CondDict.Add("iHYKTYPE_NEW", "W.HYKTYPE_NEW");
            CondDict.Add("sHYKNAME_NEW", "N.HYKNAME");
            CondDict.Add("sHYKHM_NEW", "W.HYKHM_NEW");
            CondDict.Add("sHYKHM_OLD", "W.HYKHM_OLD");
            CondDict.Add("iHYID", "W.HYID");
            CondDict.Add("sHY_NAME", "H.HY_NAME");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iMDID", "B.MDID");
            query.SQL.Text = "select W.*,H.HY_NAME,O.HYKNAME HYKNAME_OLD,N.HYKNAME HYKNAME_NEW,B.BGDDMC,M.MDMC";
            query.SQL.Add("from HYK_SJJL W,HYK_HYXX H,HYKDEF O,HYKDEF N,HYK_BGDD B,MDDY M");
            query.SQL.Add("where W.HYID=H.HYID and W.HYKTYPE_NEW=N.HYKTYPE and W.HYKTYPE_OLD=O.HYKTYPE and W.BGDDDM=B.BGDDDM and B.MDID=M.MDID");
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKSJ item = new HYKGL_HYKSJ();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYKTYPE_OLD = query.FieldByName("HYKTYPE_OLD").AsInteger;
            item.iHYKTYPE_NEW = query.FieldByName("HYKTYPE_NEW").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYKHM_OLD = query.FieldByName("HYKHM_OLD").AsString;
            item.iBJ_SJ = query.FieldByName("BJ_SJ").AsInteger;
            item.iBJ_XFJE = query.FieldByName("BJ_XFJE").AsInteger;
            item.sHYKHM_NEW = query.FieldByName("HYKHM_NEW").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.fWCLJF_OLD = query.FieldByName("WCLJF_OLD").AsFloat;
            item.fBQJF_OLD = query.FieldByName("BQJF_OLD").AsFloat;
            item.fBQXFJE_OLD = query.FieldByName("BQXFJE_OLD").AsFloat;
            item.fSJJF = query.FieldByName("SJJF").AsFloat;
            item.fJE = query.FieldByName("JE").AsFloat;
            item.sHYKNAME_OLD = query.FieldByName("HYKNAME_OLD").AsString;
            item.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sMDMC = query.FieldByName("MDMC").AsString;
            return item;
        }

    }
}
