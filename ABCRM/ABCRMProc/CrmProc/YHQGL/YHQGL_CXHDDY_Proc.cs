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


namespace BF.CrmProc.YHQGL
{
    public class YHQGL_CXHDDY : CXHD
    {
        public int iYHQID = -1;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CXHDDEF", "CXID", iJLBH);
            // msg = "禁止删除促销活动";
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("CXHDDEF");
            query.SQL.Text = "insert into CXHDDEF(CXID,SHDM,CXHDBH,CXZT,CXNR,NIAN,CXQS,KSSJ,JSSJ,SCSJ,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:CXID,:SHDM,:CXHDBH,:CXZT,:CXNR,:NIAN,:CXQS,:KSSJ,:JSSJ,:SCSJ,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("CXHDBH").AsInteger = iCXID;
            query.ParamByName("CXZT").AsString = sCXZT;
            query.ParamByName("CXNR").AsString = sCXNR;
            query.ParamByName("NIAN").AsInteger = iNIAN;
            query.ParamByName("CXQS").AsInteger = iCXQS;
            query.ParamByName("KSSJ").AsDateTime = FormatUtils.ParseDateString(dKSSJ);
            query.ParamByName("JSSJ").AsDateTime = FormatUtils.ParseDateString(dJSSJ);
            query.ParamByName("SCSJ").AsDateTime = serverTime;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "CXHDDEF", serverTime, "CXID");
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iNIAN;iCXQS;";
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "C.CXID");
            CondDict.Add("sSHDM", "C.SHDM");
            CondDict.Add("iNIAN", "C.NIAN");
            CondDict.Add("iCXQS", "C.CXQS");
            CondDict.Add("dKSSJ", "C.KSSJ");
            CondDict.Add("dJSSJ", "C.JSSJ");
            CondDict.Add("sCXZT", "C.CXZT");
            CondDict.Add("sDJRMC", "C.DJRMC");
            CondDict.Add("dDJSJ", "C.DJSJ");
            CondDict.Add("sZXRMC", "C.ZXRMC");
            CondDict.Add("dZXRQ", "C.ZXRQ");
            query.SQL.Text = "select C.*,S.SHMC from CXHDDEF C,SHDY S where C.SHDM=S.SHDM";
            if (iYHQID >= 0)
                query.SQL.AddLine("  and exists(select * from YHQDEF_CXHD B where B.CXID=C.CXID and B.YHQID=" + iYHQID + ")");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            YHQGL_CXHDDY obj = new YHQGL_CXHDDY();
            obj.iCXID = query.FieldByName("CXID").AsInteger;
            obj.iCXHDBH = query.FieldByName("CXHDBH").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.sCXNR = query.FieldByName("CXNR").AsString;
            obj.iNIAN = query.FieldByName("NIAN").AsInteger;
            obj.iCXQS = query.FieldByName("CXQS").AsInteger;
            obj.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            obj.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
            obj.dSCSJ = FormatUtils.DateToString(query.FieldByName("SCSJ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return obj;
        }
    }
}
