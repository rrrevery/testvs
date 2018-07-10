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
    public class CRMGL_SHDY : SHDY
    {
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataBase(query, out msg, "delete from SHDY where SHDM='" + sOldSHDM + "'");
        }
        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            CheckChildExist(query, out msg, "MDDY", "SHDM", sJLBH, "该商户下存在门店，不允许删除");
            return msg == "";
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sOldSHDM != sSHDM)
                CheckChildExist(query, out msg, "MDDY", "SHDM", sOldSHDM, "该商户下存在门店，不允许修改");
            if (sOldSHDM != "")
            {
                query.SQL.Text = "update SHDY set SHDM=:SHDM,SHMC=:SHMC";
                query.SQL.Add(" where SHDM=:OSHDM");
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("SHMC").AsString = sSHMC;
                query.ParamByName("OSHDM").AsString = sOldSHDM;
                query.ExecSQL();
                //DeleteDataQuery(out msg, query,serverTime);
            }
            else
            {
                query.SQL.Text = "insert into SHDY(SHDM,SHMC)";
                query.SQL.Add(" values(:SHDM,:SHMC)");
                query.ParamByName("SHDM").AsString = sSHDM;
                query.ParamByName("SHMC").AsString = sSHMC;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            query.SQL.Text = "select B.* from SHDY B";
            query.SQL.Add("    where 1=1");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            CRMGL_SHDY obj = new CRMGL_SHDY();
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.sOldSHDM = query.FieldByName("SHDM").AsString;
            return obj;
        }
    }
}
