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
using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_JFDHLPDYD_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iGZID = 0;
        public string sGZMC = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iCLFS = 0;
        public int iMDID = 0;
        public string dLJYXQ = string.Empty;
        public string sMDMC = string.Empty;
        public int iPUBLICID = 0;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "WX_JFFLLPDYD", "JLBH", iJLBH);
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFFLLPDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }



        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_JFFLLPDYD");

            query.SQL.Text = "insert into WX_JFFLLPDYD(JLBH,PUBLICID,DJLX,GZID,KSRQ,JSRQ,CLFS,DJR,DJRMC,DJSJ,STATUS,BZ,MDID,LJYXQ)";
            query.SQL.Add("values(:JLBH,:PUBLICID,:DJLX,:GZID,:KSRQ,:JSRQ,:CLFS,:DJR,:DJRMC,:DJSJ,:STATUS,:BZ,:MDID,:LJYXQ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("GZID").AsInteger = iGZID;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("CLFS").AsInteger = iCLFS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("STATUS").AsInteger = 0;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("LJYXQ").AsDateTime = FormatUtils.ParseDateString(dLJYXQ);
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("BZ").AsString = sBZ;                
            query.ExecSQL();
        }
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iGZID", "W.GZID");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iQDR", "W.QDR");
            CondDict.Add("sQDRMC", "W.QDRMC");
            CondDict.Add("dQDRQ", "W.QDRQ");
            CondDict.Add("iZZR", "W.ZZR");
            CondDict.Add("sZZRMC", "W.ZZRMC");
            CondDict.Add("dZZRQ", "W.ZZRQ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.*,B.GZMC,Y.MDMC from WX_JFFLLPDYD W,WX_JFFLLPGZ B,WX_MDDY Y where W.GZID=B.GZID AND W.MDID=Y.MDID and W.PUBLICID=" + iLoginPUBLICID;
  

            SetSearchQuery(query, lst);
            return lst;
                         
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_JFDHLPDYD_Proc obj = new GTPT_JFDHLPDYD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iDJLX = query.FieldByName("DJLX").AsInteger;
            obj.iGZID = query.FieldByName("GZID").AsInteger;
            obj.iCLFS = query.FieldByName("CLFS").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.dLJYXQ = FormatUtils.DateToString(query.FieldByName("LJYXQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.sBZ = query.FieldByName("BZ").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return obj;
        }

        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update WX_JFFLLPDYD set ZZR=:ZZR,ZZRQ=:ZZRQ,STATUS=3,ZZRMC=:ZZRMC where STATUS=2 and JSRQ>=:RQ1 and KSRQ<=:RQ2 and DJLX=:DJLX  and  PUBLICID=" + iLoginPUBLICID;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("RQ1").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("RQ2").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
           
                query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            
          
            query.ExecSQL();

            query.SQL.Text = "update WX_JFFLLPDYD set QDR=:QDR,QDRMC=:QDRMC,QDSJ=:QDSJ,STATUS=2 where PUBLICID=" + iLoginPUBLICID +"and JLBH=" + iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDSJ").AsDateTime = serverTime;
           
                query.ParamByName("QDRMC").AsString = sLoginRYMC;
            
         
            query.ExecSQL();
        }
     
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_JFFLLPDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }
    }
}
