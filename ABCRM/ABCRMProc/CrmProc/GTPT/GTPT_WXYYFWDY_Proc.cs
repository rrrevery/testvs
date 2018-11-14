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
    public class GTPT_WXYYFWDY_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sADDRESS = string.Empty;
        public string sMC = string.Empty;
        public string sIMG = string.Empty;
        public int iMDID = 0, iCHANNELID;
        public int A = 0,R;
        public string sMDMC = string.Empty;
        public int iINX = 0, iTYPE, iXZRS = 0;
        public string sYYTM = string.Empty;
        public string sYYNR = string.Empty;
        public string sXXNR = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sCHANNELIDMC = string.Empty;
        public int iPUBLICID = 0;
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iSTATUS == 1)
            {
            query.SQL.Text = "select count(*) Q from MOBILE_YDFWDEF where MDID=:MDID";
            query.SQL.Add("and MC=:MC");
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("MC").AsString = sMC;
            query.Open();
            A = query.FieldByName("Q").AsInteger;
            //query.Close();
            if (A > 0)
            {
                msg = "此预约主题在所选门店已经定义过不能再定义！";
                return false;
            }
            }
            return true;
        } 
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from MOBILE_YDFWDEF where ID=" + iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_YDFWDEF");
            query.SQL.Text = "insert into MOBILE_YDFWDEF(ID,TYPE,MC,IMG,INX,YYTM,YYNR,ADDRESS,MDID,XXNR,KSRQ,JSRQ,CHANNELID,PUBLICID,XZRS,DJR,DJRMC,DJSJ,STATUS)";
            query.SQL.Add(" values(:ID,:TYPE,:MC,:IMG,:INX,:YYTM,:YYNR,:ADDRESS,:MDID,:XXNR,:KSRQ,:JSRQ,:CHANNELID,:PUBLICID,:XZRS,:DJR,:DJRMC,:DJSJ,:STATUS)");

            query.ParamByName("ID").AsInteger = iID;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("XZRS").AsInteger = iXZRS;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("CHANNELID").AsInteger = iCHANNELID;
            query.ParamByName("MC").AsString = sMC;
            query.ParamByName("YYTM").AsString = sYYTM;
            query.ParamByName("YYNR").AsString = sYYNR;
            query.ParamByName("ADDRESS").AsString = sADDRESS;
            query.ParamByName("XXNR").AsString = sXXNR;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("STATUS").AsInteger = 0;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.ID");
            CondDict.Add("iTYPE", "L.TYPE");
            CondDict.Add("sMC", "L.MC");
            CondDict.Add("sIMG", "L.IMG");
            CondDict.Add("sYYTM", "L.YYTM");
            CondDict.Add("sYYNR", "L.YYNR");
            CondDict.Add("sXXNR", "L.XXNR");
            CondDict.Add("sADDRESS", "L.ADDRESS");
            CondDict.Add("iMDID", "L.MDID");
            CondDict.Add("iINX", "L.INX");
            CondDict.Add("sMDMC", "D.MDMC");
            query.SQL.Text = " select L.*,D.MDMC from MOBILE_YDFWDEF L,MDDY D where L.MDID=D.MDID and  L.PUBLICID=" + iLoginPUBLICID;
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXYYFWDY_Proc obj = new GTPT_WXYYFWDY_Proc();
            obj.iID = query.FieldByName("ID").AsInteger;
            obj.iTYPE = query.FieldByName("TYPE").AsInteger;
            obj.sIMG = query.FieldByName("IMG").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;
            obj.iINX = query.FieldByName("INX").AsInteger;
            obj.iXZRS = query.FieldByName("XZRS").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iCHANNELID = query.FieldByName("CHANNELID").AsInteger;

            obj.sMC = query.FieldByName("MC").AsString;
            obj.sYYTM = query.FieldByName("YYTM").AsString;
            obj.sYYNR = query.FieldByName("YYNR").AsString;
            obj.sADDRESS = query.FieldByName("ADDRESS").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sXXNR = LibProc.CyUrlEncode(query.FieldByName("XXNR").AsString);

            switch (obj.iCHANNELID)
            {
                case 0: obj.sCHANNELIDMC = "微信"; break;
                case 1: obj.sCHANNELIDMC = "APP"; break;
                case 2: obj.sCHANNELIDMC = "后台"; break;
                default: obj.sCHANNELIDMC = ""; break;
            }

            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;

            return obj;
            }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
            {
            msg = string.Empty;
            ExecTable(query, "MOBILE_YDFWDEF", serverTime, "ID", "ZXR", "ZXRMC", "ZXRQ", 1);

            }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
            {
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            msg = string.Empty;
            query.SQL.Text = "update MOBILE_YDFWDEF set ZZR=:ZZR,ZZRQ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=3,ZZRMC=:ZZRMC where ID in(select ID from MOBILE_YDFWDEF where KSRQ<=:JSRQ and JSRQ>=:KSRQ and STATUS=2)  and  PUBLICID=" + iLoginPUBLICID;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ExecSQL();

            query.SQL.Text = "update MOBILE_YDFWDEF set QDR=:QDR,QDRMC=:QDRMC,QDSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=:STATUS where ID=" + iJLBH;
            query.ParamByName("STATUS").AsInteger = 2;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRMC").AsString = sLoginRYMC;
            query.ExecSQL();
            }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_YDFWDEF", serverTime, "ID", "ZZR", "ZZRMC", "ZZRQ", 3);
        }

    }
}
