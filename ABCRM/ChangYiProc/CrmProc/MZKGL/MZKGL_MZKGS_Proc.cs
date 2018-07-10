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
    public class MZKGL_MZKGS : DZHYK_DJLR_CLass
    {
        public int iLX = 0;//1挂失恢复0挂失
        public double fJF = 0;
        public double fJE = 0;
        //public string sFXDWDM = string.Empty;
        public string sFXDWMC = string.Empty;
        public string sMDMC = string.Empty;
        public int iMDID = -1;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYID == 0)
            {
                msg = CrmLibStrings.msgNeedHYKNO;
                return false;
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_GS", "CZJPJ_JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //CheckExecutedTable(query, "HYK_CZK_KX", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_GS");
            query.SQL.Text = "insert into MZK_GS(CZJPJ_JLBH,HYKTYPE,VIPID,HYKNO,ZY,JF,JE,JLJE,LX,DJSJ,DJR,DJRMC,CZMD)";
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:HYID,:HYK_NO,:ZY,:JF,:JE,0,:LX,:DJSJ,:DJR,:DJRMC,:CZMD)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYK_NO").AsString = sHYKNO;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("JF").AsFloat = fJF;
            query.ParamByName("JE").AsFloat = fJE;
            query.ParamByName("LX").AsInteger = iLX;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZMD").AsInteger = iMDID;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_GS", serverTime, "CZJPJ_JLBH");
            if (iLX == 0)
            {
                CrmLibProc.UpdateMZKSTATUS(query, iHYID, (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_KX);
                CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_GS, iJLBH, iLoginRYID, sLoginRYMC);
            }
            else if (iLX == 1)
            {
                CrmLibProc.UpdateMZKSTATUS(query, iHYID, (int)BASECRMDefine.HYXXStatus.HYXX_STATUS_XF);
                CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_GSHF, iJLBH, iLoginRYID, sLoginRYMC);
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYID","W.VIPID");
            CondDict.Add("iMDID", "W.CZMD");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("iFXDWID", "H.FXDW");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sHYKNO", "H.HYK_NO");
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");

            query.SQL.Text = "select W.*,H.HY_NAME,D.HYKNAME,B.MDMC,F.FXDWMC";
            query.SQL.Add(" from MZK_GS W,MZKXX H,HYKDEF D,MDDY B,FXDWDEF F ");
            query.SQL.Add(" where W.VIPID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.CZMD=B.MDID and H.FXDW=F.FXDWID(+)");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKGS obj = new MZKGL_MZKGS();
            obj.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iHYID = query.FieldByName("VIPID").AsInteger;
            obj.sHYKNO = query.FieldByName("HYKNO").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.fJF = query.FieldByName("JF").AsFloat;
            obj.fJE = query.FieldByName("JE").AsFloat;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            //obj.sFXDWDM = query.FieldByName("FXDWDM").AsString;
            obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iMDID = query.FieldByName("CZMD").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);

            return obj;
        }
    }
}
