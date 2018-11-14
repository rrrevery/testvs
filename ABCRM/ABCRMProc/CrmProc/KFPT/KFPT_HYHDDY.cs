using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections.Generic;
using System;

namespace BF.CrmProc.KFPT
{
    public class KFPT_HYHDDY : DJLR_ZXQDZZ_CLass
    {
        public int iHDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string dKSSJ = string.Empty;
        public string dJSSJ = string.Empty;
        public string dZZSJ = string.Empty;
        public int iRS = 0;
        public double fXFJE = 0;
        public double fJF = 0;
        public string sHDMC = string.Empty;
        public string sHDNR = string.Empty;
        public string dBM_RQ1 = string.Empty;
        public string dBM_RQ2 = string.Empty;
        public string dQR_RQ1 = string.Empty;
        public string dQR_RQ2 = string.Empty;
        public string dCJ_RQ1 = string.Empty;
        public string dCJ_RQ2 = string.Empty;
        public string dHF_RQ1 = string.Empty;
        public string dHF_RQ2 = string.Empty;
        public string dPS_RQ1 = string.Empty;
        public string dPS_RQ2 = string.Empty;
        public string dZHXFRQ = string.Empty;
        public int iHDStatus = 0;



        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HDNRDEF", "HDID", iJLBH);
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "H.HDID");
            CondDict.Add("sSTATUS", "H.STATUS");
            CondDict.Add("dKSRQ", "H.KSSJ");
            CondDict.Add("dJSRQ", "H.JSSJ");
            CondDict.Add("iDJR", "DJR");
            CondDict.Add("sDJRMC", "DJRMC");
            CondDict.Add("dDJSJ", "DJSJ");
            CondDict.Add("iZXR", "ZXR");
            CondDict.Add("sZXRMC", "ZXRMC");
            CondDict.Add("dZXRQ", "ZXRQ");
            CondDict.Add("iZZR", "ZZR");
            CondDict.Add("sZZRMC", "ZZRMC");
            CondDict.Add("dZZRQ", "ZZRQ");
            CondDict.Add("sQZLXMC", "QZLXMC");
            query.SQL.Text = "select H.* from  HYK_HDNRDEF H";
            query.SQL.Add("where H.HDID is not null");
            SetSearchQuery(query, lst);

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_HYHDDY item = new KFPT_HYHDDY();
            item.sHDMC = query.FieldByName("HDMC").AsString;
            item.iJLBH = query.FieldByName("HDID").AsInteger;
            item.iRS = query.FieldByName("RS").AsInteger;
            item.fXFJE = query.FieldByName("XFJE").AsFloat;
            item.fJF = query.FieldByName("JF").AsFloat;
            item.sHDNR = query.FieldByName("HDNR").AsString;
            item.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            item.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
            item.dZHXFRQ = FormatUtils.DateToString(query.FieldByName("ZHXFRQ").AsDateTime);
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZSJ").AsDateTime);
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.sZZRMC = query.FieldByName("ZZRMC").AsString;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.dBM_RQ1 = FormatUtils.DateToString(query.FieldByName("BM_RQ1").AsDateTime);
            item.dBM_RQ2 = FormatUtils.DateToString(query.FieldByName("BM_RQ2").AsDateTime);
            item.dCJ_RQ1 = FormatUtils.DateToString(query.FieldByName("CJ_RQ1").AsDateTime);
            item.dCJ_RQ2 = FormatUtils.DateToString(query.FieldByName("CJ_RQ2").AsDateTime);
            item.dHF_RQ1 = FormatUtils.DateToString(query.FieldByName("HF_RQ1").AsDateTime);
            item.dHF_RQ2 = FormatUtils.DateToString(query.FieldByName("HF_RQ2").AsDateTime);
            item.dPS_RQ1 = FormatUtils.DateToString(query.FieldByName("PS_RQ1").AsDateTime);
            item.dPS_RQ2 = FormatUtils.DateToString(query.FieldByName("PS_RQ2").AsDateTime);
            item.dQR_RQ1 = FormatUtils.DateToString(query.FieldByName("QR_RQ1").AsDateTime);
            item.dQR_RQ2 = FormatUtils.DateToString(query.FieldByName("QR_RQ2").AsDateTime);
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return item;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_HDNRDEF");

            query.SQL.Text = "insert into HYK_HDNRDEF(HDID,HDMC,KSSJ,JSSJ,DJR,DJRMC,DJSJ,XFJE,JF,ZHXFRQ,RS,HDNR,STATUS,BM_RQ1,BM_RQ2,QR_RQ1,QR_RQ2,CJ_RQ1,CJ_RQ2,HF_RQ1,HF_RQ2,PS_RQ1,PS_RQ2)";
            query.SQL.Add("values(:HDID,:HDMC,:KSSJ,:JSSJ,:DJR,:DJRMC,:DJSJ,:XFJE,:JF,:ZHXFRQ,:RS,:HDNR,:STATUS,:BM_RQ1,:BM_RQ2,:QR_RQ1,:QR_RQ2,:CJ_RQ1,:CJ_RQ2,:HF_RQ1,:HF_RQ2,:PS_RQ1,:PS_RQ2)");
            query.ParamByName("HDID").AsInteger = iJLBH;
            query.ParamByName("JSSJ").AsDateTime = FormatUtils.ParseDateString(dJSSJ);
            query.ParamByName("KSSJ").AsDateTime = FormatUtils.ParseDateString(dKSSJ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("HDNR").AsString = sHDNR;
            query.ParamByName("HDMC").AsString = sHDMC;
            query.ParamByName("XFJE").AsFloat = fXFJE;
            query.ParamByName("JF").AsFloat = fJF;
            query.ParamByName("ZHXFRQ").AsDateTime = FormatUtils.ParseDateString(dZHXFRQ);
            query.ParamByName("RS").AsInteger = iRS;
            query.ParamByName("STATUS").AsInteger = 0;
            query.ParamByName("BM_RQ1").AsDateTime = FormatUtils.ParseDateString(dBM_RQ1);
            query.ParamByName("BM_RQ2").AsDateTime = FormatUtils.ParseDateString(dBM_RQ2);
            query.ParamByName("CJ_RQ1").AsDateTime = FormatUtils.ParseDateString(dCJ_RQ1);
            query.ParamByName("CJ_RQ2").AsDateTime = FormatUtils.ParseDateString(dCJ_RQ2);
            query.ParamByName("HF_RQ1").AsDateTime = FormatUtils.ParseDateString(dHF_RQ1);
            query.ParamByName("HF_RQ2").AsDateTime = FormatUtils.ParseDateString(dHF_RQ2);
            query.ParamByName("PS_RQ1").AsDateTime = FormatUtils.ParseDateString(dPS_RQ1);
            query.ParamByName("PS_RQ2").AsDateTime = FormatUtils.ParseDateString(dPS_RQ2);
            query.ParamByName("QR_RQ1").AsDateTime = FormatUtils.ParseDateString(dQR_RQ1);
            query.ParamByName("QR_RQ2").AsDateTime = FormatUtils.ParseDateString(dQR_RQ2);
            query.ExecSQL();


        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_HDNRDEF", serverTime, "HDID", "ZXR", "ZXRMC", "ZXRQ", 1);
        }


        //终止
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_HDNRDEF", serverTime, "HDID", "ZZR", "ZZRMC", "ZZRQ", -1);
        }
    }
}
