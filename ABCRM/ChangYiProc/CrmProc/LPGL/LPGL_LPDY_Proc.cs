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

namespace BF.CrmProc.LPGL
{
    public class LPGL_LPDY : LPXX
    {
        public int iCXLX = 0;
        public string sSHDM = string.Empty;
        public int iPDCX = 0;
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFFHLPXX", "LPID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.Close();
            query.SQL.Text = "select * from HYK_JFFHLPXX where (LPMC=:LPMC or LPDM=:LPDM) and LPID<>:LPID";
            query.ParamByName("LPID").AsInteger = iJLBH;
            query.ParamByName("LPDM").AsString = sLPDM;
            query.ParamByName("LPMC").AsString = sLPMC;
            query.Open();
            if (!query.IsEmpty)
            {
                query.Close();
                msg = "礼品名称或代码已存在";
                return;
            }
            query.Close();
            if (iJLBH != 0)
            {
                //DeleteDataQuery(out msg, query,serverTime);
                query.SQL.Text = "update HYK_JFFHLPXX set ";
                query.SQL.Add("LPDM = :LPDM,");
                query.SQL.Add("LPMC = :LPMC,");
                query.SQL.Add("LPGG = :LPGG,");
                query.SQL.Add("LPDJ = :LPDJ,");
                query.SQL.Add("LPJJ = :LPJJ,");
                query.SQL.Add("LPJF = :LPJF,");
                query.SQL.Add("LPFLID = :LPFLID,");
                query.SQL.Add("BJ_DEL = :BJ_DEL,");
                query.SQL.Add("XGR = :DJR,");
                query.SQL.Add("XGRMC = :DJRMC,");
                query.SQL.Add("XGSJ = :DJSJ,");
                query.SQL.Add("BJ_WKC = :BJ_WKC,");
                query.SQL.Add("GJBM = :GJBM,");
                query.SQL.Add("ZSJF = :ZSJF,");
                query.SQL.Add("LPLX = :LPLX ");
                query.SQL.Add(" where LPID=:LPID");
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYK_JFFHLPXX");
                sLPDM = iJLBH.ToString().PadLeft(6, '0');
                sLPDM = sLPDM.Substring(sLPDM.Length - 6);
                query.SQL.Text = "insert into HYK_JFFHLPXX(LPID,LPFLID,LPDM,LPMC,LPGG,LPDJ,LPJJ,LPJF,BJ_DEL,DJR,DJRMC,DJSJ,";
                query.SQL.Add(" BJ_WKC,GJBM,ZSJF,LPLX)");
                query.SQL.Add(" values(:LPID,:LPFLID,:LPDM,:LPMC,:LPGG,:LPDJ,:LPJJ,:LPJF,:BJ_DEL,:DJR,:DJRMC,:DJSJ,");
                query.SQL.Add(" :BJ_WKC,:GJBM,:ZSJF,:LPLX)");
            }
            query.ParamByName("LPID").AsInteger = iJLBH;
            query.ParamByName("LPFLID").AsInteger = iLPFLID;
            query.ParamByName("LPDM").AsString = sLPDM;
            query.ParamByName("LPMC").AsString = sLPMC;
            query.ParamByName("LPGG").AsString = sLPGG;
            query.ParamByName("LPDJ").AsFloat = fLPDJ;
            query.ParamByName("LPJJ").AsFloat = fLPJJ;
            query.ParamByName("LPJF").AsFloat = fLPJF;
            query.ParamByName("BJ_DEL").AsInteger = iBJ_DEL;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BJ_WKC").AsInteger = iBJ_WKC;
            query.ParamByName("GJBM").AsString = sGJBM;
            query.ParamByName("ZSJF").AsFloat = fZSJF;
            query.ParamByName("LPLX").AsInteger = iLPLX;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.LPID");

            CondDict.Add("sLPDM", "W.LPDM");
            CondDict.Add("sLPMC", "W.LPMC");
            CondDict.Add("sLPGG", "W.LPGG");
            CondDict.Add("fLPDJ", "W.LPDJ");
            CondDict.Add("fLPJJ", "W.LPJJ");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("sXGRMC", "W.XGRMC");
            CondDict.Add("dXGSJ", "W.XGSJ");
            CondDict.Add("iLPFLID", "W.LPFLID");
            CondDict.Add("sGJBM", "W.GJBM");
            CondDict.Add("sBGDDDM", "K.BGDDDM");
            CondDict.Add("fWCLJF", "W.LPJF");
            if (iCXLX == 1)
            {
                query.SQL.Text = "select W.*,F.LPFLMC,K.BGDDDM,K.KCSL from HYK_JFFHLPXX W,LPFLDEF F,HYK_JFFHLPKC K";
                query.SQL.Add(" where W.LPFLID=F.LPFLID(+) and W.LPID=K.LPID(+) ");  //不限制库存的礼品也要显示
                if (iPDCX == 1)  //盘点不需要查不限制库存的礼品
                {
                    query.SQL.Add(" and K.BGDDDM='" + sBGDDDM + "'");
                }
                else
                {
                    query.SQL.Add(" and (K.BGDDDM='" + sBGDDDM + "' or K.BGDDDM is null)");
                }
            }
            else  //这样做的原因是 关联HYK_JFFHLPKC，但不显示库存的时候，会显示重复记录
            {
                query.SQL.Text = "select DISTINCT W.*,F.LPFLMC from HYK_JFFHLPXX W,LPFLDEF F,HYK_JFFHLPKC K";
                query.SQL.Add(" where W.LPFLID=F.LPFLID(+) and W.LPID=K.LPID(+)");
            }
            SetSearchQuery(query, lst, true, "", false);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPDY obj = new LPGL_LPDY();
            obj.iJLBH = query.FieldByName("LPID").AsInteger;
            obj.iLPFLID = query.FieldByName("LPFLID").AsInteger;
            obj.sLPFLMC = query.FieldByName("LPFLMC").AsString;
            obj.sLPDM = query.FieldByName("LPDM").AsString;
            obj.sLPMC = query.FieldByName("LPMC").AsString;
            obj.sLPGG = query.FieldByName("LPGG").AsString;
            obj.fLPDJ = query.FieldByName("LPDJ").AsFloat;
            obj.fLPJJ = query.FieldByName("LPJJ").AsFloat;
            obj.fLPJF = query.FieldByName("LPJF").AsFloat;
            obj.iBJ_DEL = query.FieldByName("BJ_DEL").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iXGR = query.FieldByName("XGR").AsInteger;
            obj.sXGRMC = query.FieldByName("XGRMC").AsString;
            obj.dXGSJ = FormatUtils.DatetimeToString(query.FieldByName("XGSJ").AsDateTime);
            obj.iBJ_WKC = query.FieldByName("BJ_WKC").AsInteger;
            obj.sGJBM = query.FieldByName("GJBM").AsString;
            obj.fZSJF = query.FieldByName("ZSJF").AsFloat;
            obj.iLPLX = query.FieldByName("LPLX").AsInteger;
            if (iCXLX == 1)
            {
                obj.fKCSL = query.FieldByName("KCSL").AsFloat;
            }
            return obj;
        }
    }
}