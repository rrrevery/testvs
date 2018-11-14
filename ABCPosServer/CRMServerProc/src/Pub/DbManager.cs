using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.ProviderBase;
using System.Data.Common;

namespace ChangYi.Pub
{
    public class DbConnSettings
    {
        public string ConnName = string.Empty;
        public string ProviderName = string.Empty;
        public string ConnStr = string.Empty;
        public DbProviderFactory ProviderFactory = null;
    }

    public class DbConnManager
    {
        private static List<DbConnSettings> dbConnSettingsList = new List<DbConnSettings>();

        private static DbConnSettings GetDbConnSettings(string connName)
        {
            lock (dbConnSettingsList)
            {
                foreach (DbConnSettings mySettings in dbConnSettingsList)
                {
                    if (mySettings.ConnName.Equals(connName))
                    {
                        return mySettings;
                    }
                }
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[connName];
                if (settings == null)
                    throw new Exception("在配置文件中没找到名称为 " + connName + " 的数据库连接串配置");

                DbConnSettings mySettings2 = new DbConnSettings();
                dbConnSettingsList.Add(mySettings2);
                mySettings2.ConnName = connName;
                mySettings2.ProviderName = settings.ProviderName;
                mySettings2.ProviderFactory = DbProviderFactories.GetFactory(settings.ProviderName);
                mySettings2.ConnStr = DecryptPasswordInDbConnStr(settings.ConnectionString);
                return mySettings2;
            }
        }

        public static bool TestDbConnect(string connName, out string msg)
        {
            bool ok = false;
            StringBuilder sb = new StringBuilder();
            try
            {
                DbConnSettings settings = GetDbConnSettings(connName);
                sb.Append("<br> DB ProviderName=\"").Append(settings.ProviderName).Append("\"");
                DbConnection conn = settings.ProviderFactory.CreateConnection();
                conn.ConnectionString = settings.ConnStr;
                string str = settings.ConnStr.ToUpper();
                int inx1 = str.IndexOf("PASSWORD=");
                if (inx1 < 0)
                    inx1 = str.IndexOf("PWD=");
                if (inx1 < 0)
                    inx1 = str.IndexOf("PASSWORD");
                if (inx1 < 0)
                    inx1 = str.IndexOf("PWD");
                str = settings.ConnStr;
                if (inx1 >= 0)
                {
                    int inx2 = str.IndexOf(";", inx1);
                    if (inx2 >= 0)
                        str = str.Remove(inx1, inx2 - inx1 + 1);
                    else
                        str = str.Remove(inx1);
                }
                sb.Append("<br> DB ConnectionString=\"").Append(str).Append("\"");
                conn.Open();
                conn.Close();
                sb.Append("<br>");
                sb.Append("<br> 连接到数据库成功");
                ok = true;
            }
            catch (Exception e)
            {
                sb.Append("<br>");
                sb.Append("<br> 连接到数据库失败: ");
                sb.Append("<br> ").Append(e.ToString());
            }

            msg = sb.ToString();
            return ok;
        }

        public static DbConnection GetDbConnection(string connName)
        {
            DbConnSettings settings = GetDbConnSettings(connName);
            DbConnection conn = settings.ProviderFactory.CreateConnection();
            conn.ConnectionString = settings.ConnStr;
            return conn;
        }
        private static string DecryptPasswordInDbConnStr(string connStr)
        {
            StringBuilder sb = new StringBuilder();
            string str = connStr.ToUpper();
            int inx1 = str.IndexOf("PASSWORD=");
            if (inx1 >= 0)
                inx1 += 9;
            else
            {
                inx1 = str.IndexOf("PWD=");
                if (inx1 >= 0)
                    inx1 += 4;
            }
            if (inx1 >= 0)
            {
                if ((inx1 < connStr.Length - 1) && (connStr[inx1] == '\''))
                    inx1++;
                int inx2 = str.IndexOf(";", inx1);
                string password = null;
                if (inx2 >= 0)
                {
                    if ((inx2 > 1) && (connStr[inx2 - 1] == '\''))
                    {
                        inx2--;
                    }
                    password = connStr.Substring(inx1, inx2 - inx1);
                }
                else
                {
                    if (connStr[connStr.Length - 1] == '\'')
                    {
                        inx2 = connStr.Length - 1;
                        password = connStr.Substring(inx1, inx2 - inx1);
                    }
                    else
                    {
                        password = connStr.Substring(inx1, connStr.Length - inx1);
                    }
                }
                password = PasswordEncryptUtils.PasswordDecrypt(password);
                
                sb.Append(connStr.Substring(0,inx1)).Append(password);
                if (inx2 >= 0)
                    sb.Append(connStr.Substring(inx2, connStr.Length - inx2));
            }
            return sb.ToString();
        }
        public static DbDataAdapter CreateDbDataAdapter(string connName)
        {
            DbConnSettings settings = GetDbConnSettings(connName);
            DbDataAdapter adapter = settings.ProviderFactory.CreateDataAdapter();
            return adapter;
        }

        //public static DbConnection GetCrmDbConnection()
        //{
        //    ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["CrmDB"];
        //    //if (settings.ProviderName.Equals("Sybase.Data.AseClient"))
        //    //{
        //    //    Sybase.Data.AseClient.AseConnection aseConn = new Sybase.Data.AseClient.AseConnection(settings.ConnectionString);
        //    //    return aseConn;
        //    //}
            
        //    /*
        //    if (settings.ProviderName.Equals("Oracle.DataAccess.Client"))
        //    {
        //        Oracle.DataAccess.Client.OracleConnection oracleConn = new Oracle.DataAccess.Client.OracleConnection(settings.ConnectionString);
        //        return oracleConn;
        //    }
        //     */

        //    if (crmDbProviderFactory == null)
        //    {
        //        crmDbProviderFactory = DbProviderFactories.GetFactory(settings.ProviderName);
        //        crmDbConnStr = settings.ConnectionString;
        //    }
        //    DbConnection conn = crmDbProviderFactory.CreateConnection();
        //    conn.ConnectionString = crmDbConnStr;
        //    return conn;
        //}

    }

    public class SeqGenerator 
    {
        private static Object sync = new Object();

	    public static int GetSeq(string connName,int dbId, String tableName,int step = 1) 
        {
            DbConnection conn = DbConnManager.GetDbConnection(connName);
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int seq = step;
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
                    sql.Append("update BHZT set REC_NUM = REC_NUM+").Append(step).Append(" where TBLNAME = '").Append(tableName).Append("'");
                    cmd.CommandText = sql.ToString();
                    //if (DbConnManager.CheckNoRecordsAffectedAfterExecSql(connName, cmd.ExecuteNonQuery()))
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        sql.Length = 0;
                        sql.Append("insert into BHZT (TBLNAME,REC_NUM) values ('").Append(tableName).Append("', ").Append(step).Append(")");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select REC_NUM from BHZT where TBLNAME = '").Append(tableName).Append("'");
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            seq = DbUtils.GetInt(reader,0);
                        }
                        reader.Close();
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
            return dbId*100000000 + seq;
	    }

        public static int GetSeqHYK_XFJL(string connName, int dbId, int step = 1)
        {
            lock (sync)
            {
                return GetSeq(connName,dbId, "HYK_XFJL", step);
            }
        }
        public static int GetSeqHYK_JYCL(string connName, int dbId, int step = 1)
        {
            lock (sync)
            {
                return GetSeq(connName, dbId,"HYK_JYCL", step);
            }
        }
        public static int GetSeqHYK_JEZCLJL(string connName, int dbId, int step = 1)
        {
            lock (sync)
            {
                return GetSeq(connName,dbId, "HYK_JEZCLJL", step);
            }
        }
        public static int GetSeqHYK_YHQCLJL(string connName, int dbId, int step = 1)
        {
            lock (sync)
            {
                return GetSeq(connName,dbId, "HYK_YHQCLJL", step);
            }
        }
        public static int GetSeqHYK_JFBDJLMX(string connName, int dbId, int step = 1)
        {
            lock (sync)
            {
                return GetSeq(connName, dbId, "HYK_JFBDJLMX", step);
            }
        }
        public static int GetSeqYHQCODE(string connName, int step = 1)
        {
            lock (sync)
            {
                return GetSeq(connName,0, "YHQCODE", step);
            }
        }
    }
}
