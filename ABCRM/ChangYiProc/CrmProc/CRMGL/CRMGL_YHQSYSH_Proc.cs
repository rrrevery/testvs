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
    public class CRMGL_YHQSYSH : YHQSYSH
    {

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from YHQSYSH where SHDM='" + sSHDM + "' and YHQID=" + iYHQID);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.SQL.Text = "insert into YHQSYSH(SHDM,YHQID,BJ_SYD)";
            query.SQL.Add(" values(:SHDM,:YHQID,:BJ_SYD)");
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("BJ_SYD").AsInteger = iBJ_SYD;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iBJ_SYD;";
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "A.YHQID");
            CondDict.Add("sSHDM", "A.SHDM");
            CondDict.Add("sSHMC", "S.SHMC");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            query.SQL.Text = "select A.*,Y.YHQMC,S.SHMC from YHQSYSH A,YHQDEF Y,SHDY S";
            query.SQL.Add("    where A.YHQID>0 AND A.SHDM=S.SHDM and A.YHQID=Y.YHQID");
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                CRMGL_YHQSYSH obj = new CRMGL_YHQSYSH();
                lst.Add(obj);
                obj.iYHQID = query.FieldByName("YHQID").AsInteger;
                obj.sYHQMC = query.FieldByName("YHQMC").AsString;
                obj.sSHDM = query.FieldByName("SHDM").AsString;
                obj.sSHMC = query.FieldByName("SHMC").AsString;
                obj.iBJ_SYD = query.FieldByName("BJ_SYD").AsInteger;
                query.Next();
            }
            query.Close();
            return lst;
        }
    }
}
