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
    public class MZKGL_MZKHK : DZHYK_DJLR_CLass
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

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_HK", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "MZK_HK", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_HK");
            query.Close();
            iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            query.SQL.Text = "insert into MZK_HK(CZJPJ_JLBH,HYKTYPE,HYID,HYKHM_OLD,HYKHM_NEW,ZY,JF,JE,GBF,DJSJ,DJR,DJRMC,BGDDDM,CZMD)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:HYID,:HYKHM_OLD,:HYKHM_NEW,:ZY,:JF,:JE,:GBF,:DJSJ,:DJR,:DJRMC,:BGDDDM,:CZMD)");
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
            query.ParamByName("CZMD").AsInteger = iMDID;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string track = string.Empty;
            string sYZM = string.Empty;
            DateTime validDate = CrmLibProc.GetHYKYXQ(query, iHYKTYPE);
            query.Close();
            query.SQL.Text = "select CDNR,YZM from MZKCARD where CZKHM=:CZKHM";
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
            ExecTable(query, "MZK_HK", serverTime, "CZJPJ_JLBH");
            query.SQL.Text = "update MZKXX set HYK_NO=:HYK_NO,CDNR=:CDNR,OLD_HYKNO=:OLD_HYKNO,JKRQ=:JKRQ,YXQ=:YXQ";
            query.SQL.Add(" ,YZM=:YZM where HYID=:HYID");
            query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
            query.ParamByName("CDNR").AsString = track;
            query.ParamByName("OLD_HYKNO").AsString = sHYKHM_OLD;
            query.ParamByName("JKRQ").AsDateTime = serverTime.Date;
            query.ParamByName("YXQ").AsDateTime = validDate;
            query.ParamByName("YZM").AsString = sYZM;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
            query.SQL.Text = "delete from MZKCARD where CZKHM=:HYK_NO";
            query.ParamByName("HYK_NO").AsString = sHYKHM_NEW;
            query.ExecSQL();
            CrmLibProc.SaveCardTrack(query, sHYKHM_NEW, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_HK, iJLBH, iLoginRYID, sLoginRYMC, 0, sHYKHM_OLD);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sBGDDDM", "W.BGDDDM");

            query.SQL.Text = "select W.*,D.HYKNAME,B.BGDDMC ";
            query.SQL.Add("     from MZK_HK W,MZKXX H,HYKDEF D,HYK_BGDD B");
            query.SQL.Add("    where W.HYID=H.HYID");
            query.SQL.Add("    and W.BGDDDM=B.BGDDDM(+)");
            query.SQL.Add("      and W.HYKTYPE=D.HYKTYPE");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKHK item = new MZKGL_MZKHK();
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
            item.iMDID = query.FieldByName("CZMD").AsInteger;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }
    }
}
