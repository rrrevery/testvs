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


namespace BF.CrmProc.CRMGL
{
    public class CRMGL_SHZFFS : SHZFFS
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            //CrmLibProc.DeleteDataTables(query, out msg, "HYNLDDY", "NLDID", iJLBH);
            msg = "商户支付方式不允许删除";
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update SHZFFS set ";
            query.SQL.Add(" BJ_JF = :BJ_JF,");
            query.SQL.Add(" BJ_FQ = :BJ_FQ,JFBL=:JFBL,BJ_MBJZ=:BJ_MBJZ");
            query.SQL.Add(" where SHZFFSID = :SHZFFSID");
            query.ParamByName("BJ_JF").AsInteger = iBJ_JF;
            query.ParamByName("BJ_FQ").AsInteger = iBJ_FQ;
            query.ParamByName("BJ_MBJZ").AsInteger = iBJ_MBJZ;

            query.ParamByName("SHZFFSID").AsInteger = iSHZFFSID;
            query.ParamByName("JFBL").AsFloat = fJFBL;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "Z.SHZFFSID");
            CondDict.Add("sSHDM", "S.SHDM");
            CondDict.Add("sSHMC", "S.SHMC");
            CondDict.Add("sZFFSDM", "Z.ZFFSDM");
            CondDict.Add("sZFFSMC", "Z.ZFFSMC");
            CondDict.Add("iBJ_JF", "Z.BJ_JF");
            CondDict.Add("iBJ_FQ", "Z.BJ_FQ");
            CondDict.Add("iBJ_MBJZ", "Z.BJ_MBJZ");
            CondDict.Add("iYHQID", "Z.YHQID");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            CondDict.Add("fJFBL", "Z.JFBL");

            query.SQL.Text = "select Z.*, S.SHMC,Y.YHQMC from SHZFFS Z,SHDY S,YHQDEF Y where Z.SHDM=S.SHDM and Y.YHQID(+)=Z.YHQID  ";
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SHZFFS obj = new CRMGL_SHZFFS();
            obj.iJLBH = query.FieldByName("SHZFFSID").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
            obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
            obj.iMJBJ = query.FieldByName("MJBJ").AsInteger;
            obj.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
            obj.iBJ_JF = query.FieldByName("BJ_JF").AsInteger;
            obj.iBJ_FQ = query.FieldByName("BJ_FQ").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.iBJ_MBJZ = query.FieldByName("BJ_MBJZ").AsInteger;
            obj.fJFBL = query.FieldByName("JFBL").AsFloat;
            return obj;
        }
    }
}
