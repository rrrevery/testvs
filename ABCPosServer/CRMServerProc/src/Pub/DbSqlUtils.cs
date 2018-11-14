using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace ChangYi.Pub
{
    public class DbSqlUtils
    {
        private static void AddParamToCommand(DbCommand cmd, string param)
        {
            if ((param != null) && (param.Length > 0))
            {
                string[] ps = param.Split(';');
                foreach (string str in ps)
                {
                    string[] p = str.Split(',');
                    DbParameter dbParam = cmd.CreateParameter();
                    cmd.Parameters.Add(dbParam);
                    dbParam.Direction = ParameterDirection.Input;
                    dbParam.ParameterName = p[0].Trim();
                    dbParam.Value = p[2];
                    string paramType = p[1].Trim();
                    if (paramType.Equals("INT"))
                        dbParam.DbType = DbType.Int32;
                    else if (paramType.Equals("DOUBLE") || paramType.Equals("FLOAT"))
                        dbParam.DbType = DbType.Double;
                    else if (paramType.Equals("NUMBER"))
                        dbParam.DbType = DbType.Decimal;
                    else if (paramType.Equals("DATE"))
                        dbParam.DbType = DbType.Date;                                
                    else if (paramType.Equals("DATETIME"))
                        dbParam.DbType = DbType.DateTime;                                
                    else 
                        dbParam.DbType = DbType.String;
                    //dbParam.Size = paramSize;
                }
            }

        }

        public static DataTable GetDataToTable(string connName, string sql, ref string msg)
        {
            return GetDataToTable(connName, sql, null, ref msg);
        }

        public static DataTable GetDataToTable(string connName, string sql, string param, ref string msg)
        {
            msg = string.Empty;
            DataTable dataTable = new DataTable();
            try
            {
                DbConnection conn = DbConnManager.GetDbConnection(connName);
                DbDataAdapter adapter = DbConnManager.CreateDbDataAdapter(connName);
                DbCommand cmd = conn.CreateCommand();
                try
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception e)
                    {
                        throw new MyDbException(e.Message, true);
                    }
                    try
                    {
                        adapter.SelectCommand = cmd;
                        cmd.CommandText = sql;
                        AddParamToCommand(cmd, param);
                        adapter.Fill(dataTable);
                    }
                    catch (Exception e)
                    {
                        if (e is MyDbException)
                            throw e;
                        else
                            throw new MyDbException(e.Message, cmd.CommandText);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return dataTable;
        }

        public static int ExecSql(string connName, string sql, ref string msg)
        {
            return ExecSql(connName,sql, null, ref msg);
        }

        public static int ExecSql(string connName, string sql, string param, ref string msg)
        {
            msg = string.Empty;
            int rows = 0;
            try
            {
                DbConnection conn = DbConnManager.GetDbConnection(connName);
                DbCommand cmd = conn.CreateCommand();
                try
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception e)
                    {
                        throw new MyDbException(e.Message, true);
                    }
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;
                        cmd.CommandText = sql;
                        AddParamToCommand(cmd, param);
                        rows = cmd.ExecuteNonQuery();
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        if (e is MyDbException)
                            throw e;
                        else
                            throw new MyDbException(e.Message, cmd.CommandText);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return rows;
        }

        public static int ExecSqlList(string connName, ArrayList sqlList, ref string msg)
        {
            msg = string.Empty;
            int rows = 0;
            try
            {
                DbConnection conn = DbConnManager.GetDbConnection(connName);
                DbCommand cmd = conn.CreateCommand();
                try
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception e)
                    {
                        throw new MyDbException(e.Message, true);
                    }
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;

                        foreach (string sql in sqlList)
                        {
                            string[] s = sql.Split('~');
                            if (s.Length > 0)
                            {
                                cmd.CommandText = s[0];
                                if (s.Length > 1)
                                    AddParamToCommand(cmd, s[1]);
                                rows += cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        if (e is MyDbException)
                            throw e;
                        else
                            throw new MyDbException(e.Message, cmd.CommandText);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }
            return rows;
        }
    }
}
