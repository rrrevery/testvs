using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using BF.Pub;

namespace TransData2
{
    public class JXCDB
    {
        public string JXCName = string.Empty;
        public string Connection = string.Empty;
        public string SHDM = string.Empty;
    }
    public class TransData
    {
        public string Name = string.Empty;
        public string Tbl = string.Empty;
        public string CRMTbl = string.Empty;
        public int Count = 0;
        public string SelSQL = string.Empty;
        public string FromSQL = string.Empty;
        public string WhereSQL = string.Empty;
    }
    public class SeqGenerator
    {
        private static object sync = new object();

        public static int GetSeq(String tableName, int iStep = 1, string sDBConnName = "CRMDB")
        {
            //DbConnection conn = DbConnManager.GetDbConnection("CRMWEB");
            DbConnection conn = CyDbConnManager.GetDbConnection(sDBConnName);
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            int seq = 1;
            Int32 tp_i = 0;
            try
            {
                conn.Open();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    sql.Append("SELECT CUR_VAL from BFCONFIG where JLBH=0");
                    cmd.CommandText = sql.ToString();
                    DbDataReader dbread = cmd.ExecuteReader();
                    while (dbread.Read())
                    {
                        tp_i = Convert.ToInt32(dbread.GetString(0));
                    }
                    dbread.Close();//LBC
                    sql.Length = 0;
                    sql.Append("update BHZT set REC_NUM = REC_NUM+").Append(iStep).Append(" where TBLNAME = \'").Append(tableName).Append("\'");
                    cmd.CommandText = sql.ToString();
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        sql.Length = 0;
                        sql.Append("insert into BHZT (TBLNAME,REC_NUM) values (\'").Append(tableName).Append("\', ").Append(iStep).Append(")");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select REC_NUM from BHZT where TBLNAME = \'").Append(tableName).Append("\'");
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                            seq = reader.GetInt32(0);
                        reader.Close();
                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
            //tp_i * 100000000 + seq;
            return tp_i * 100000000 + seq;
        }

        public static int GetSeqNoDBID(String tableName)
        {
            //DbConnection conn = DbConnManager.GetDbConnection("CRMWEB");
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            int seq = 1;
            try
            {
                conn.Open();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    sql.Length = 0;
                    sql.Append("update BHZT set REC_NUM = REC_NUM+1 where TBLNAME = \'").Append(tableName).Append("\'");
                    cmd.CommandText = sql.ToString();
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        sql.Length = 0;
                        sql.Append("insert into BHZT (TBLNAME,REC_NUM) values (\'").Append(tableName).Append("\', 1)");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select REC_NUM from BHZT where TBLNAME = \'").Append(tableName).Append("\'");
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                            seq = reader.GetInt32(0);
                        reader.Close();
                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
            //tp_i * 100000000 + seq;
            return seq;
        }

        public static int GetSeqNoInc(String tableName)
        {
            //DbConnection conn = DbConnManager.GetDbConnection("CRMWEB");
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            int seq = 1;
            try
            {
                conn.Open();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    sql.Length = 0;
                    sql.Length = 0;
                    sql.Append("select REC_NUM from BHZT where TBLNAME = \'").Append(tableName).Append("\'");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        seq = reader.GetInt32(0);
                    reader.Close();
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
            //tp_i * 100000000 + seq;
            return seq;
        }
    }

}
