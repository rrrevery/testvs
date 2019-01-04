using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace OraSP
{
    public class OracleDB
    {
        public static void OraProc_prc_create_patientinfo(
            string par_card_no,
            string par_cardtype,
            string par_fundtype,
            string par_mcardid,
            string par_idcard,
            string par_idcard_type,
            string par_name,
            string par_sex,
            string par_birthday,
            string par_nationlity,
            string par_telephone,
            string par_id_address,
            string par_opercode,
            out string par_retmsg,
            out string par_patientid,
            out int par_bound
            )
        {
            par_retmsg = string.Empty;
            par_patientid = string.Empty;
            par_bound = 0;

            OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["HISDB_Ora"].ConnectionString);
            conn.Open();
            DbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "prc_create_patientinfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(Param(cmd, "par_card_no", DbType.String, par_card_no));
            cmd.Parameters.Add(Param(cmd, "par_cardtype", DbType.String, par_cardtype));
            cmd.Parameters.Add(Param(cmd, "par_fundtype", DbType.String, par_fundtype));
            cmd.Parameters.Add(Param(cmd, "par_mcardid", DbType.String, par_mcardid));
            cmd.Parameters.Add(Param(cmd, "par_idcard", DbType.String, par_idcard));
            cmd.Parameters.Add(Param(cmd, "par_idcard_type", DbType.String, par_idcard_type));
            cmd.Parameters.Add(Param(cmd, "par_name", DbType.String, par_name));
            cmd.Parameters.Add(Param(cmd, "par_sex", DbType.String, par_sex));
            cmd.Parameters.Add(Param(cmd, "par_birthday", DbType.String, par_birthday));
            cmd.Parameters.Add(Param(cmd, "par_nationlity", DbType.String, par_nationlity));
            cmd.Parameters.Add(Param(cmd, "par_telephone", DbType.String, par_telephone));
            cmd.Parameters.Add(Param(cmd, "par_id_address", DbType.String, par_id_address));
            cmd.Parameters.Add(Param(cmd, "par_opercode", DbType.String, par_opercode));
            cmd.Parameters.Add(Param(cmd, "par_retmsg", DbType.String, par_retmsg, ParameterDirection.Output));
            cmd.Parameters.Add(Param(cmd, "par_patientid", DbType.String, par_patientid, ParameterDirection.Output));
            cmd.Parameters.Add(Param(cmd, "par_bound", DbType.Int32, par_bound, ParameterDirection.Output));
            cmd.ExecuteNonQuery();
            par_retmsg = cmd.Parameters["par_retmsg"].Value.ToString();
            par_patientid = cmd.Parameters["par_patientid"].Value.ToString();
            par_bound = int.Parse(cmd.Parameters["par_bound"].Value.ToString());
            conn.Close();
        }
        static DbParameter Param(DbCommand cmd, string ParamName, DbType ParamType, object ParamValue, ParameterDirection In = ParameterDirection.Input)
        {
            DbParameter pm = cmd.CreateParameter();
            pm.ParameterName = ParamName;
            pm.DbType = ParamType;
            pm.Value = ParamValue;
            pm.Direction = In;
            if (pm.DbType == DbType.String)
                pm.Size = 255;
            return pm;
        }
    }
}
