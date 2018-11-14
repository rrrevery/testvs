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
    public class CRMGL_YHQDY : YHQDEF
    {

        public string sIMAGEURL = string.Empty;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "YHQDEF", "YHQID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeqNoDBID("YHQDEF");
            query.SQL.Text = "insert into YHQDEF(YHQID,YHQMC,BJ_DZYHQ,BJ_TY,BJ_FQ,FQLX,FS_YQMDFW,FS_FQMDFW,";
            query.SQL.Add(" YXQTS,BJ_CXYHQ,ISCODED,CODELEN,CODEPRE,CODESUF,BJ_TS,IMAGEURL)");
            query.SQL.Add(" values(:YHQID,:YHQMC,:BJ_DZYHQ,:BJ_TY,:BJ_FQ,:FQLX,:FS_YQMDFW,:FS_FQMDFW,");
            query.SQL.Add(" :YXQTS,:BJ_CXYHQ,:ISCODED,:CODELEN,:CODEPRE,:CODESUF,:BJ_TS,:IMAGEURL)");
            query.ParamByName("YHQID").AsInteger = iJLBH;
            query.ParamByName("YHQMC").AsString = sYHQMC;
            query.ParamByName("BJ_DZYHQ").AsInteger = iBJ_DZYHQ;
            query.ParamByName("BJ_TY").AsInteger = iBJ_TY;
            query.ParamByName("BJ_FQ").AsInteger = iBJ_FQ;
            query.ParamByName("FQLX").AsInteger = iFQLX;
            query.ParamByName("FS_YQMDFW").AsInteger = iFS_YQMDFW;
            query.ParamByName("FS_FQMDFW").AsInteger = iFS_FQMDFW;
            query.ParamByName("YXQTS").AsInteger = iYXQTS;
            query.ParamByName("BJ_CXYHQ").AsInteger = iBJ_CXYHQ;
            query.ParamByName("ISCODED").AsInteger = iISCODED;
            query.ParamByName("CODELEN").AsInteger = iCODELEN;
            query.ParamByName("BJ_TS").AsInteger = iBJ_TS;
            query.ParamByName("CODEPRE").AsString = sCODEPRE;
            query.ParamByName("CODESUF").AsString = sCODESUF;
            query.ParamByName("IMAGEURL").AsString = sIMAGEURL;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iYXQTS;";
            if (sSortFiled == "")
            {
                sSortFiled = "iJLBH";
                sSortType = "asc";
            }
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "W.YHQID");
            CondDict.Add("sYHQMC", "W.YHQMC");
            query.SQL.Text = "select W.* from YHQDEF W ";
            query.SQL.Add(" where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_YHQDY obj = new CRMGL_YHQDY();
            obj.iJLBH = query.FieldByName("YHQID").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.iBJ_DZYHQ = query.FieldByName("BJ_DZYHQ").AsInteger;
            obj.iBJ_TY = query.FieldByName("BJ_TY").AsInteger;
            obj.iBJ_FQ = query.FieldByName("BJ_FQ").AsInteger;
            obj.iFQLX = query.FieldByName("FQLX").AsInteger;
            obj.iFS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
            obj.iFS_FQMDFW = query.FieldByName("FS_FQMDFW").AsInteger;
            obj.iYXQTS = query.FieldByName("YXQTS").AsInteger;
            obj.iBJ_CXYHQ = query.FieldByName("BJ_CXYHQ").AsInteger;
            obj.iISCODED = query.FieldByName("ISCODED").AsInteger;
            obj.iCODELEN = query.FieldByName("CODELEN").AsInteger;
            obj.iBJ_TS = query.FieldByName("BJ_TS").AsInteger;
            obj.sCODEPRE = query.FieldByName("CODEPRE").AsString;
            obj.sCODESUF = query.FieldByName("CODESUF").AsString;
            obj.sIMAGEURL = query.FieldByName("IMAGEURL").AsString;
            return obj;
        }
    }
}
