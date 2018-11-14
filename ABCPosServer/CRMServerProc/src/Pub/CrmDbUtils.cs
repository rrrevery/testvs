using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace ChangYi.Pub
{
    public class DbUtils
    {
        public const string OracleDbSystemName = "ORACLE";
        public const string SybaseDbSystemName = "SYBASE";
        public const string DB2DbSystemName = "DB2";
        //public const byte OracleDbSystem = 1;
        //public const byte SybaseDbSystem = 2;
        //public const byte DB2DbSystem = 3;

        private static Encoding gbkEncoding = Encoding.GetEncoding(936);

        public static string GetDbSystemName(DbConnection conn)
        {
            string str = conn.GetType().FullName.ToUpper();
            if (str.Contains("ASECONNECTION"))
                return SybaseDbSystemName;
            if (str.Contains(OracleDbSystemName))
                return OracleDbSystemName; 
            str = conn.ConnectionString.ToUpper();
            if (str.Contains(SybaseDbSystemName))
                return SybaseDbSystemName;
            else if (str.Contains(OracleDbSystemName))
                return OracleDbSystemName;
            else if (str.Contains(DB2DbSystemName))
                return DB2DbSystemName;
            else
                throw new Exception("Only support Syabse or Oracle or DB2");
        }
        public static DateTime GetDbServerTime(DbCommand cmd)
        {
            switch (GetDbSystemName(cmd.Connection))
            {
                case SybaseDbSystemName:
                    cmd.CommandText = "select getdate() ";
                    break;
                case OracleDbSystemName:
                    cmd.CommandText = "select sysdate from dual ";
                    break;
                case DB2DbSystemName:
                    cmd.CommandText = "select current timestamp from SYSIBM.SYSDUMMY1 ";
                    break;
            }
            return (DateTime)cmd.ExecuteScalar();
        }
        public static string GetDbServerTimeStr(DbCommand cmd)
        {
            return FormatUtils.DatetimeToString(GetDbServerTime(cmd));
        }

        public static string GetDbServerTimeFuncSql(DbCommand cmd)
        {
            switch (GetDbSystemName(cmd.Connection))
            {
                case SybaseDbSystemName:
                    return " getdate() ";
                case OracleDbSystemName:
                    return " sysdate ";
                case DB2DbSystemName:
                    return " current timestamp ";
            }
            return string.Empty;
        }

        public static string GetIsNullFuncName(string dbSysName)
        {
            switch (dbSysName)
            {
                case SybaseDbSystemName:
                    return "isnull";
                case OracleDbSystemName:
                    return "nvl";
                case DB2DbSystemName:
                    return "value";
            }
            return string.Empty;
        }

        public static string SpellSqlParameter(DbConnection conn, string paramName)
        {
            string str = conn.GetType().FullName.ToUpper();
            if (str.Contains("ASECONNECTION"))
                return "@" + paramName;
            else if (str.Contains(OracleDbSystemName))
                return ":" + paramName;
            else
                return "?";
        }

        public static void SpellSqlParameter(DbConnection conn, StringBuilder sql, string prefix, string fieldName, string operationSymbol)
        {
            sql.Append(prefix);
            string str = conn.GetType().FullName.ToUpper();
            if (str.Contains("ASECONNECTION"))
            {
                if (operationSymbol.Length > 0)
                    sql.Append(fieldName).Append(operationSymbol).Append("@").Append(fieldName);
                else
                    sql.Append("@").Append(fieldName);
            }
            else if (str.Contains(OracleDbSystemName))
            {
                if (operationSymbol.Length > 0)
                    sql.Append(fieldName).Append(operationSymbol).Append(":").Append(fieldName);
                else
                    sql.Append(":").Append(fieldName);
            }
            else
            {
                if (operationSymbol.Length > 0)
                    sql.Append(fieldName).Append(operationSymbol).Append("?");
                else
                    sql.Append("?");
            }
        }
        public static void SpellSqlParameter(DbConnection conn, StringBuilder sql, string prefix, string fieldName, string operationSymbol, string paramName)
        {
            if (paramName.Length == 0)
                paramName = fieldName;
            sql.Append(prefix);
            string str = conn.GetType().FullName.ToUpper();
            if (str.Contains("ASECONNECTION"))
            {
                if (operationSymbol.Length > 0)
                    sql.Append(fieldName).Append(operationSymbol).Append("@").Append(paramName);
                else
                    sql.Append("@").Append(paramName);
            }
            else if (str.Contains(OracleDbSystemName))
            {
                if (operationSymbol.Length > 0)
                    sql.Append(fieldName).Append(operationSymbol).Append(":").Append(paramName);
                else
                    sql.Append(":").Append(paramName);
            }
            else
            {
                if (operationSymbol.Length > 0)
                    sql.Append(fieldName).Append(operationSymbol).Append("?");
                else
                    sql.Append("?");
            }
        }
        public static DbParameter AddParameter(DbCommand cmd, ParameterDirection paramDirection, DbType paramType, int paramSize, string paramName, Object paramValue)
        {
            DbParameter param = cmd.CreateParameter();
            param.Direction = paramDirection;
            param.DbType = paramType;
            param.Size = paramSize;
            if (cmd.Connection.GetType().FullName.ToUpper().Contains("ASECONNECTION"))
                param.ParameterName = "@" + paramName;
            else
                param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return param;
        }
        public static DbParameter AddIntInputParameter(DbCommand cmd, string paramName)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.Int32, 0, paramName, null);
        }
        public static DbParameter AddIntInputParameterAndValue(DbCommand cmd, string paramName, string paramValue)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.Int32, 0, paramName, paramValue);
        }
        public static DbParameter AddStrInputParameter(DbCommand cmd, int paramSize, string paramName)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.String, paramSize, paramName, null);
        }
        public static DbParameter AddStrInputParameter(DbCommand cmd, int paramSize, string paramName,bool isChinese)
        {
            return AddParameter(cmd, ParameterDirection.Input, isChinese?DbType.Binary:DbType.String, paramSize, paramName, null);
        }
        public static DbParameter AddStrInputParameterAndValue(DbCommand cmd, int paramSize, string paramName, string paramValue)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.String, paramSize, paramName, paramValue);
        }

        public static DbParameter AddStrInputParameterAndValue(DbCommand cmd, int paramSize, string paramName, string paramValue, bool isChinese)
        {
            if (isChinese)
            {
                DbParameter param = AddParameter(cmd, ParameterDirection.Input, DbType.Binary, paramSize, paramName, null);
                param.Value = Encoding.Convert(Encoding.Unicode, gbkEncoding, Encoding.Unicode.GetBytes(paramValue));
                return param;
            }
            else
            {
                return AddParameter(cmd, ParameterDirection.Input, DbType.String, paramSize, paramName, paramValue);
            }
        }

        public static DbParameter AddDoubleInputParameter(DbCommand cmd, string paramName)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.Double, 0, paramName, null);
        }
        public static DbParameter AddDoubleInputParameterAndValue(DbCommand cmd, string paramName, string paramValue)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.Double, 0, paramName, paramValue);
        }

        public static DbParameter AddDatetimeInputParameter(DbCommand cmd, string paramName)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.DateTime, 0, paramName, null);
        }
        public static DbParameter AddDatetimeInputParameterAndValue(DbCommand cmd, string paramName, DateTime paramValue)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.DateTime, 0, paramName, paramValue);
        }
        public static DbParameter AddBytesInputParameterAndValue(DbCommand cmd, string paramName, byte[] paramValue)
        {
            return AddParameter(cmd, ParameterDirection.Input, DbType.Binary, 0, paramName, paramValue);
        }

        public static int GetInt(DbDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
                return 0;
            else
                return int.Parse(reader.GetValue(index).ToString());
        }
        public static double GetDouble(DbDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
                return 0;
            else
                return double.Parse(reader.GetValue(index).ToString());
        }
        public static string GetString(DbDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
                return string.Empty;
            else
                return reader.GetString(index);
        }
        public static string GetString(DbDataReader reader, int index,bool isChinese,int dataSize)
        {
            if (reader.IsDBNull(index))
                return string.Empty;
            else if (isChinese)
            {
                byte[] gbkBytes = new byte[dataSize];
                reader.GetBytes(index, 0, gbkBytes, 0, dataSize);
                if (gbkBytes[0] == 0)
                {
                    return string.Empty;
                }
                else
                {
                    int dataSize2 = dataSize;
                    for (int i = dataSize - 1; i >= 0; i--)
                    {
                        if (gbkBytes[i] != 0)
                        {
                            dataSize2 = i + 1;
                            break;
                        }
                    }
                    byte[] gbkBytes2 = new byte[dataSize2];
                    Array.Copy(gbkBytes, gbkBytes2, dataSize2);
                    return Encoding.Unicode.GetString(Encoding.Convert(gbkEncoding, Encoding.Unicode, gbkBytes2)).Trim();
                }
            }
            else
                return reader.GetString(index);
        }

        public static bool GetBool(DbDataReader reader, int index)
        {
            return (GetInt(reader,index) != 0);
        }
        public static DateTime GetDateTime(DbDataReader reader, int index)
        {
            if (reader.IsDBNull(index))
                return DateTime.MinValue;
            else
                return reader.GetDateTime(index);
        }
    }
}
