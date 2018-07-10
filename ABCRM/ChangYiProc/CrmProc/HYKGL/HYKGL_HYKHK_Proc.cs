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
    public class HYKGL_HYKHK : DZHYK_DJLR_CLass
    {
        public string sHYKHM_OLD = string.Empty;
        public string sHYKHM_NEW = string.Empty;
        public double fJF = 0;
        public double fJE = 0;
        public double fGBF = 0;
        public string sSFZBH = string.Empty;
        public string sZJLX = string.Empty;
        public int iMDID;
        public int iFXDWID;
        public string sFXDWMC = string.Empty;
        public int iBJ_CHILD = 0;
        public int iHKYY = 0;

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

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZK_WK", "CZJPJ_JLBH", iJLBH);
                    }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "HYK_CZK_WK", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HY_CZK_WK");
            query.Close();
            query.SQL.Text = "insert into HYK_CZK_WK(CZJPJ_JLBH,HYKTYPE,HYID,HYKHM_OLD,HYKHM_NEW,ZY,JF,JE,GBF,DJSJ,DJR,DJRMC,BGDDDM,BJ_CHILD,HKYY)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:HYID,:HYKHM_OLD,:HYKHM_NEW,:ZY,:JF,:JE,:GBF,:DJSJ,:DJR,:DJRMC,:BGDDDM,:BJ_CHILD,:HKYY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYKHM_OLD").AsString = sHYKHM_OLD;
            query.ParamByName("HYKHM_NEW").AsString = sHYKHM_NEW;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("JF").AsFloat = fJF;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("GBF").AsFloat = fGBF;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BJ_CHILD").AsInteger = iBJ_CHILD;
            query.ParamByName("HKYY").AsInteger = iHKYY;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string track = string.Empty;
            string sYZM = string.Empty;
            //DateTime validDate = CrmLibProc.GetHYKYXQ(query, iHYKTYPE);
            query.Close();
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
            ExecTable(query, "HYK_CZK_WK", serverTime, "CZJPJ_JLBH");
            if (iBJ_CHILD == 0)
            {
                query.SQL.Text = "update HYK_HYXX set HYK_NO=:HYK_NO,CDNR=:CDNR,OLD_HYKNO=:OLD_HYKNO,JKRQ=:JKRQ";
                query.SQL.Add(" ,YZM=:YZM where HYID=:HYID");
                query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
                query.ParamByName("CDNR").AsString = track;
                query.ParamByName("OLD_HYKNO").AsString = sHYKHM_OLD;
                query.ParamByName("JKRQ").AsDateTime = serverTime.Date;
                //query.ParamByName("YXQ").AsDateTime = validDate;
                query.ParamByName("YZM").AsString = sYZM;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
            }
            else
            {
                query.SQL.Text = "update HYK_CHILD_JL set HYK_NO=:HYK_NO,CDNR=:CDNR,OLD_HYKNO=:OLD_HYKNO,JKRQ=:JKRQ";
                query.SQL.Add(" ,YZM=:YZM where HYID=:HYID and HYK_NO=:OLD_HYKNO");
                query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
                query.ParamByName("CDNR").AsString = track;
                query.ParamByName("OLD_HYKNO").AsString = sHYKHM_OLD;
                query.ParamByName("JKRQ").AsDateTime = serverTime.Date;
                //query.ParamByName("YXQ").AsDateTime = validDate;
                query.ParamByName("YZM").AsString = sYZM;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
            }
                
            query.SQL.Text = "delete from HYKCARD where CZKHM=:HYK_NO";
            query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH","W.CZJPJ_JLBH");
            CondDict.Add("sHYKNAME","D.HYKNAME");
            CondDict.Add("sZJLX","X.NR");
            CondDict.Add("iHYKTYPE","W.HYKTYPE");
            CondDict.Add("iHYID","W.HYID");
            CondDict.Add("sHYKHM_OLD","W.HYKHM_OLD");
            CondDict.Add("sHYKHM_NEW","W.HYKHM_NEW");
            CondDict.Add("fGBF","W.GBF");
            CondDict.Add("sZY","W.ZY");
            CondDict.Add("sSFZBH","G.SFZBH");
            CondDict.Add("fJF","W.JF");
            CondDict.Add("fJE","W.JE");
            CondDict.Add("sBGDDMC","B.BGDDMC");
            CondDict.Add("sBGDDDM","B.BGDDDM");
            CondDict.Add("sHY_NAME","H.HY_NAME");
            CondDict.Add("iDJR","W.DJR");
            CondDict.Add("sDJRMC","W.DJRMC");
            CondDict.Add("dDJSJ","W.DJSJ");
            CondDict.Add("iZXR","W.ZXR");
            CondDict.Add("sZXRMC","W.ZXRMC");
            CondDict.Add("dZXRQ","W.ZXRQ");
            CondDict.Add("iMDID","B.MDID");
            CondDict.Add("iHKYY","W.HKYY");
            query.SQL.Text = "select W.*,H.FXDW,F.FXDWMC,H.HY_NAME,D.HYKNAME,B.BGDDMC,G.SFZBH,X.NR ZJLX";
            query.SQL.Add("     from HYK_CZK_WK W,HYK_HYXX H,HYKDEF D,HYK_BGDD B,HYK_GRXX G,HYXXXMDEF X,FXDWDEF F");
            query.SQL.Add("    where W.HYID=H.HYID");
            query.SQL.Add("    and H.FXDW=F.FXDWID(+)");
            query.SQL.Add("      and W.HYKTYPE=D.HYKTYPE");
            query.SQL.Add("      and W.BGDDDM=B.BGDDDM");
            query.SQL.Add("      and H.HYID=G.HYID(+)");
            query.SQL.Add("      and G.ZJLXID=X.XMID(+)");
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKHK item = new HYKGL_HYKHK();
            item.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYKHM_OLD = query.FieldByName("HYKHM_OLD").AsString;
            item.sHYKHM_NEW = query.FieldByName("HYKHM_NEW").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.fJF = query.FieldByName("JF").AsFloat;
            item.fJE = query.FieldByName("JE").AsFloat;
            item.fGBF = query.FieldByName("GBF").AsFloat;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sZJLX = query.FieldByName("ZJLX").AsString;
            item.sSFZBH = query.FieldByName("SFZBH").AsString;//CrmLibProc.MakePrivateNumber(
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            item.iFXDWID = query.FieldByName("FXDW").AsInteger;
            item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iHKYY = query.FieldByName("HKYY").AsInteger;
            return item;
        }
    }
}
