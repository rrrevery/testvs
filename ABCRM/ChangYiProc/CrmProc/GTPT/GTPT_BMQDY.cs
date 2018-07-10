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
    public class GTPT_BMQDY : DJLR_ZXQDZZ_CLass
    {


        public int iBMQID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iYHQID = 0;
        public int iBJ_TY = 0;
        public string sYHQMC = string.Empty;
        public string sBMQMC = string.Empty;
        public string sSYSM = string.Empty;
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
           CrmLibProc.DeleteDataTables(query, out msg, "BMQDEF", "BMQID", iJLBH);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "B.BMQID");
            CondDict.Add("iYHQID", "B.YHQID");
            CondDict.Add("sBMQMC", "B.BMQMC");
            query.SQL.Text = "select B.*,Y.YHQMC from BMQDEF B,YHQDEF Y where B.YHQID=Y.YHQID";
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_BMQDY obj = new GTPT_BMQDY();
            obj.iBMQID = query.FieldByName("BMQID").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sBMQMC = query.FieldByName("BMQMC").AsString;
            obj.sSYSM = query.FieldByName("SYSM").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;

            return obj;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iJLBH != 0)
                DeleteDataQuery(out msg, query, serverTime);
            else
                iJLBH = SeqGenerator.GetSeq("BMQDEF");
            query.SQL.Text = "insert into BMQDEF(BMQID,YHQID,BMQMC,BJ_TY,SYSM,DJR,DJRMC,DJSJ) values(:BMQID,:YHQID,:BMQMC,:BJ_TY,:SYSM,:DJR,:DJRMC,:DJSJ)";
            query.ParamByName("BMQID").AsInteger = iBMQID;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
           query.ParamByName("DJRMC").AsString = sLoginRYMC;
           query.ParamByName("SYSM").AsString = sSYSM;
           query.ParamByName("BMQMC").AsString = sBMQMC;
            
          
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "BMQDEF", serverTime, "BMQID", "ZXR", "ZXRMC", "ZXRQ");
        }

       
    }
}
