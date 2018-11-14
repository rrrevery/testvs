using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;

namespace ChangYi.Crm.Server
{
    public class UploadInfoProc
    {
        public static bool Login(out string msg, CrmLoginData loginData)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();

            //sql.Append("select USER_PSW from CRMUSER where USER_DM = '").Append(loginData.UserCode).Append("'");
            //cmd.CommandText = sql.ToString();
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
                CyQuery query = new CyQuery(conn);
                try
                {
                    //DbDataReader reader = cmd.ExecuteReader();
                    query.SQL.Text = "select USER_PSW from CRMUSER ";
                    query.SQL.Add(" where USER_DM = '").Add(loginData.UserCode).Add("'");
                   
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        string pass = query.FieldByName("USER_PSW").AsString;
                        query.Close();
                        if (!loginData.Password.Equals(pass))
                        {
                            msg = "登录BFCRM的用户密码不正确";
                            return false;
                        }
                    }
                    else
                    {
                        query.Close();
                        msg = "登录BFCRM的用户代码不存在";
                        return false;
                    }
                    query.SQL.Text = "select SHMC from SHDY ";
                    query.SQL.Add(" where SHDM = '").Add(loginData.StoreInfo.Company).Add("'");
                    query.Open();
                    if (query.IsEmpty)
                    {
                        //reader.Close();
                        msg = "登录BFCRM的商户代码不存在";
                        return false;
                    }
                    //reader.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }

            return (msg.Length == 0);
        }

        public static bool SaveArticleBrand(out string msg, string companyCode, List<string> codesToDelete, List<ArticleBrand> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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

                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select SHSBID from SHSPSB where SHDM = :SHDM and SBDM = :SBDM ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (ArticleBrand item in itemsToUpdate)
                        {
                            query.ParamByName("SBDM").AsString = item.Code;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                                item.Id = query.FieldByName("SHSBID").AsInteger;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();
                        if (newItemCount > 0)
                            seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "SHSPSB", newItemCount) - newItemCount + 1;
                    }

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHSPSB where SHDM = :SHDM and SBDM = :SBDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                query.ParamByName("SBDM").AsString = code;
                                query.ExecSQL();
                            }
                            query.Close();
                        }
                        if (newItemCount > 0)
                        {
                            query.SQL.Text = "insert into SHSPSB(SHSBID,SHDM,SBDM,SBMC,PYM,SYZ,MJBJ) values(:SHSBID,:SHDM,:SBDMNEW,:SBMC,:PYM,:SYZ,:MJBJ) ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (ArticleBrand item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    query.ParamByName("SHSBID").AsInteger = seqId++;
                                    query.ParamByName("SBDMNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SBMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("SBMC").AsString = item.Name;
                                    query.ParamByName("PYM").AsString = item.Spell;
                                    query.ParamByName("SYZ").AsString = item.Owner;
                                    query.ParamByName("MJBJ").AsInteger = (item.IsLeaf ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update SHSPSB set SBDM = :SBDMNEW,SBMC = :SBMC,PYM = :PYM,SYZ = :SYZ,MJBJ = :MJBJ where SHDM = :SHDM and SBDM = :SBDM ";
                            query.ParamByName("SBDM").AsString = companyCode;
                            foreach (ArticleBrand item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    query.ParamByName("SBDM").AsString = item.Code;
                                    query.ParamByName("SBDMNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SBMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("SBMC").AsString = item.Name;
                                    query.ParamByName("PYM").AsString = item.Spell;
                                    query.ParamByName("SYZ").AsString = item.Owner;
                                    query.ParamByName("MJBJ").AsInteger = (item.IsLeaf ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message,query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool SaveArticleCategory(out string msg, string companyCode, List<string> codesToDelete, List<ArticleCategory> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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

                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select SHSPFLID from SHSPFL where SHDM = :SHDM and SPFLDM = :SPFLDM ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (ArticleCategory item in itemsToUpdate)
                        {
                            if (item.Code.Length == 0)
                            {
                                query.ParamByName("SPFLDM").AsString = " ";
                            }
                            else
                            {
                                query.ParamByName("SPFLDM").AsString = item.Code;
                            }
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                                item.Id = query.FieldByName("SHSPFLID").AsInteger;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();
                        if (newItemCount > 0)
                            seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "SHSPFL", newItemCount) - newItemCount + 1;
                    }

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHSPFL where SHDM = :SHDM and SPFLDM = :SPFLDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                if (code.Length == 0)
                                {
                                    query.ParamByName("SPFLDM").AsString = " ";
                                }
                                else
                                {
                                    query.ParamByName("SPFLDM").AsString = code;
                                }
                                query.ExecSQL();
                            }
                            query.Close();
                        }
                        if (newItemCount > 0)
                        {
                            query.SQL.Text = "insert into SHSPFL(SHSPFLID,SHDM,SPFLDM,SPFLMC,PYM,MJBJ) values(:SHSPFLID,:SHDM,:SPFLDMNEW,:SPFLMC,:PYM,:MJBJ)";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (ArticleCategory item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    query.ParamByName("SHSPFLID").AsInteger = seqId++;
                                    if (item.NewCode.Length == 0)
                                    {
                                        query.ParamByName("SPFLDMNEW").AsString = " ";
                                    }
                                    else
                                    {
                                        query.ParamByName("SPFLDMNEW").AsString = item.NewCode;
                                    }
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPFLMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("SPFLMC").AsString = item.Name;
                                    query.ParamByName("PYM").AsString = item.Spell;
                                    query.ParamByName("MJBJ").AsInteger = (item.IsLeaf ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update SHSPFL set SPFLDM = :SPFLDMNEW,SPFLMC = :SPFLMC,PYM = :PYM,MJBJ = :MJBJ where SHDM = :SHDM and SPFLDM = :SPFLDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (ArticleCategory item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    if (item.NewCode.Length == 0)
                                    {
                                        query.ParamByName("SPFLDMNEW").AsString = " ";
                                    }
                                    else
                                    {
                                        query.ParamByName("SPFLDMNEW").AsString = item.NewCode;
                                    }
                                    if (item.Code.Length == 0)
                                    {
                                        query.ParamByName("SPFLDM").AsString = " ";
                                    }
                                    else
                                    {
                                        query.ParamByName("SPFLDM").AsString = item.Code;
                                    }
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPFLMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("SPFLMC").AsString = item.Name;
                                    query.ParamByName("PYM").AsString = item.Spell;
                                    query.ParamByName("MJBJ").AsInteger = (item.IsLeaf ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool SaveDept(out string msg, string companyCode, List<string> codesToDelete, List<Dept> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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
                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select SHBMID from SHBM where SHDM = :SHDM and BMDM = :BMDM ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Dept item in itemsToUpdate)
                        {
                            if (item.Code.Length == 0)
                            {
                                query.ParamByName("BMDM").AsString = " ";
                            }
                            else
                            {
                                query.ParamByName("BMDM").AsString = item.Code;
                            }

                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                                item.Id = query.FieldByName("SHBMID").AsInteger;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();
                        if (newItemCount > 0)
                            seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "SHBM", newItemCount) - newItemCount + 1;
                    }

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHBM where SHDM = :SHDM and BMDM = :BMDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                if (code.Length == 0)
                                {
                                    query.ParamByName("BMDM").AsString = " ";
                                }
                                else
                                {
                                    query.ParamByName("BMDM").AsString = code;
                                }
                                query.ExecSQL();
                            }
                            query.Close();
                        }
                        if (newItemCount > 0)
                        {
                            query.SQL.Text = "insert into SHBM(SHBMID,SHDM,BMDM,BMMC,BMQC,DEPT_TYPE) values(:SHBMID,:SHDM,:BMDMNEW,:BMMC,:BMQC,:DEPT_TYPE)";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Dept item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    query.ParamByName("SHBMID").AsInteger = seqId++;
                                    if (item.NewCode.Length == 0)
                                    {
                                        query.ParamByName("BMDMNEW").AsString = " ";
                                    }
                                    else
                                    {
                                        query.ParamByName("BMDMNEW").AsString = item.NewCode;
                                    }
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("BMMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("BMMC").AsString = item.Name;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("BMQC").AsChineseString = item.FullName;
                                    else
                                        query.ParamByName("BMQC").AsString = item.FullName;
                                    query.ParamByName("DEPT_TYPE").AsInteger = int.Parse(item.DeptType);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update SHBM set BMDM = :BMDMNEW,BMMC = :BMMC,BMQC = :BMQC,DEPT_TYPE = :DEPT_TYPE where SHDM = :SHDM and BMDM = :BMDM";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Dept item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    if (item.Code.Length == 0)
                                    {
                                        query.ParamByName("BMDM").AsString = " ";
                                    }
                                    else
                                    {
                                        query.ParamByName("BMDM").AsString = item.Code;
                                    }
                                    if (item.NewCode.Length == 0)
                                    {
                                        query.ParamByName("BMDMNEW").AsString = " ";
                                    }
                                    else
                                    {
                                        query.ParamByName("BMDMNEW").AsString = item.NewCode;
                                    }
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("BMMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("BMMC").AsString = item.Name;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("BMQC").AsChineseString = item.FullName;
                                    else
                                        query.ParamByName("BMQC").AsString = item.FullName;
                                    query.ParamByName("DEPT_TYPE").AsInteger = int.Parse(item.DeptType);
                                    query.ExecSQL();
                                }
                            }
                            query.ExecSQL();
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message,query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool SaveContract(out string msg, string companyCode, List<string> codesToDelete, List<Contract> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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

                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select SHBMID from SHBM where SHDM = :SHDM and BMDM = :BMDM";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Contract item in itemsToUpdate)
                        {
                            query.ParamByName("BMDM").AsString = item.DeptCode;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.DeptId = query.FieldByName("SHBMID").AsInteger;
                            }
                            else
                            {
                                query.Close();
                                msg = "合同号" + item.Code + "的部门(代码:" + item.DeptCode + ")未上传到crm";
                                return false;
                            }
                            query.Close();
                        }
                        query.Close();

                        query.SQL.Text = "select SHHTID from SHHT where SHDM = :SHDM and HTH = :HTH ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Contract item in itemsToUpdate)
                        {
                            query.ParamByName("HTH").AsString = item.Code;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                                item.Id = query.FieldByName("SHHTID").AsInteger;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();

                        if (newItemCount > 0)
                            seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "SHHT", newItemCount) - newItemCount + 1;
                    }

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHHT where SHDM = :SHDM and HTH = :HTH ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                query.ParamByName("HTH").AsString = code;
                                query.ExecSQL();
                            }
                            query.Close();
                        }
                        if (newItemCount > 0)
                        {
                            query.SQL.Text = "insert into SHHT(SHHTID,SHDM,HTH,GHSDM,GSHMC,SHBMID,BJ_YX) values(:SHHTID,:SHDM,:HTHNEW,:GHSDM,:GSHMC,:SHBMID,:BJ_YX)";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Contract item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    query.ParamByName("SHHTID").AsInteger = seqId++;
                                    query.ParamByName("HTHNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("GSHMC").AsChineseString = item.SuppName;
                                    else
                                        query.ParamByName("GSHMC").AsString = item.SuppName;
                                    query.ParamByName("GHSDM").AsString = item.SuppCode;
                                    query.ParamByName("SHBMID").AsInteger = item.DeptId;
                                    query.ParamByName("BJ_YX").AsInteger = (item.IsValid ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update SHHT set HTH = :HTHNEW,GHSDM = :GHSDM,GSHMC = :GSHMC,SHBMID = :SHBMID,BJ_YX = :BJ_YX where SHDM = :SHDM and HTH = :HTH";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Contract item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    query.ParamByName("HTH").AsString = item.Code;
                                    query.ParamByName("HTHNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("GSHMC").AsChineseString = item.SuppName;
                                    else
                                        query.ParamByName("GSHMC").AsString = item.SuppName;
                                    query.ParamByName("GHSDM").AsString = item.SuppCode;
                                    query.ParamByName("SHBMID").AsInteger = item.DeptId;
                                    query.ParamByName("BJ_YX").AsInteger = (item.IsValid ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.ExecSQL();
                        }
                        query.Close();
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException )
                        throw e;
                    else
                        throw new MyDbException(e.Message,query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool SavePayMethod(out string msg, string companyCode, List<string> codesToDelete, List<Payment> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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
                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select SHZFFSID from SHZFFS where SHDM = :SHDM and ZFFSDM = :ZFFSDM ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Payment item in itemsToUpdate)
                        {
                            query.ParamByName("ZFFSDM").AsString = item.Code;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                                item.Id = query.FieldByName("SHZFFSID").AsInteger;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();
                        if (newItemCount > 0)
                            seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "SHZFFS", newItemCount) - newItemCount + 1;
                    }

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHZFFS where SHDM = :SHDM and ZFFSDM = :ZFFSDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                query.ParamByName("ZFFSDM").AsString = code;
                                query.ExecSQL();
                            }
                            query.Close();
                        }
                        if (newItemCount > 0)
                        {
                            query.SQL.Text = "insert into SHZFFS(SHZFFSID,SHDM,ZFFSDM,ZFFSMC,MJBJ,YHQID,BJ_MBJZ) values(:SHZFFSID,:SHDM,:ZFFSDMNEW,:ZFFSMC,:MJBJ,:YHQID,:BJ_MBJZ) ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Payment item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    query.ParamByName("SHZFFSID").AsInteger = seqId++;
                                    query.ParamByName("ZFFSDMNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("ZFFSMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("ZFFSMC").AsString = item.Name;
                                    query.ParamByName("MJBJ").AsInteger = (item.IsLeaf ? 1 : 0);
                                    if (item.CouponType >= 0)
                                        query.ParamByName("YHQID").AsInteger = item.CouponType;
                                    else
                                    {
                                        query.ParamByName("YHQID").DataType = DbType.Int32;
                                        query.ParamByName("YHQID").SetNull();
                                    }
                                    query.ParamByName("BJ_MBJZ").AsInteger = (item.JoinPromDecMoney ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update SHZFFS set ZFFSDM = :ZFFSDMNEW,ZFFSMC = :ZFFSMC,MJBJ = :MJBJ,YHQID = :YHQID,BJ_MBJZ = :BJ_MBJZ where SHDM = :SHDM and ZFFSDM = :ZFFSDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Payment item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    query.ParamByName("ZFFSDM").AsString = item.Code;
                                    query.ParamByName("ZFFSDMNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("ZFFSMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("ZFFSMC").AsString = item.Name;
                                    query.ParamByName("MJBJ").AsInteger = (item.IsLeaf ? 1 : 0);
                                    if (item.CouponType >= 0)
                                    {
                                        query.ParamByName("YHQID").AsInteger = item.CouponType;
                                    }
                                    else
                                    {
                                        query.ParamByName("YHQID").DataType = DbType.Int32;
                                        query.ParamByName("YHQID").Value = null;
                                    }
                                    query.ParamByName("BJ_MBJZ").AsInteger = (item.JoinPromDecMoney ? 1 : 0);
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool SaveArticle(out string msg, string companyCode, List<string> codesToDelete, List<Article> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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

                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        //--判断商品商标
                        query.SQL.Text = "select SHSBID from SHSPSB where SHDM = :SHDM and SBDM = :SBDM";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Article item in itemsToUpdate)
                        {
                            query.ParamByName("SBDM").AsString = item.BrandCode;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.BrandId = query.FieldByName("SHSBID").AsInteger;
                            }
                            else
                            {
                                query.Close();
                                msg = "商品代码" + item.Code + "的商标(代码:" + item.BrandCode + ")未上传到crm";
                                return false;
                            }
                            query.Close();
                        }
                        query.Close();

                        //--判断商品分类
                        query.SQL.Text = "select SHSPFLID from SHSPFL where SHDM = :SHDM and SPFLDM = :SPFLDM";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Article item in itemsToUpdate)
                        {
                            query.ParamByName("SPFLDM").AsString = item.CategoryCode;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.CategoryId = query.FieldByName("SHSPFLID").AsInteger;
                            }
                            else
                            {
                                query.Close();
                                msg = "商品代码" + item.Code + "的商品分类(代码:" + item.CategoryCode + ")未上传到crm";
                                return false;
                            }
                            query.Close();
                        }
                        query.Close();

                        //--判断合同
                        query.SQL.Text = "select SHHTID from SHHT where SHDM = :SHDM and HTH = :HTH";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Article item in itemsToUpdate)
                        {
                            query.ParamByName("HTH").AsString = item.ContractCode;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.ContractId = query.FieldByName("SHHTID").AsInteger;
                            }
                            else
                            {
                                query.Close();
                                msg = "商品代码" + item.Code + "的合同(合同号:" + item.ContractCode + ")未上传到crm";
                                return false;
                            }
                            query.Close();
                        }
                        query.Close();

                        query.SQL.Text = "select SHSPID from SHSPXX where SHDM = :SHDM and SPDM = :SPDM ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Article item in itemsToUpdate)
                        {
                            query.ParamByName("SPDM").AsString = item.Code;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                                item.Id = query.FieldByName("SHSPID").AsInteger;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();

                        if (newItemCount > 0)
                            seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "SHSPXX", newItemCount) - newItemCount + 1;
                    }
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHSPXX where SHDM = :SHDM and SPDM = :SPDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                query.ParamByName("SPDM").AsString = code;
                                query.ExecSQL();
                            }
                            query.Close();
                        }

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from SHSPXX_DM where SHDM = :SHDM and SPDM = :SPDM ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                query.ParamByName("SPDM").AsString = code;
                                query.ExecSQL();
                            }
                            query.Close();
                        }

                        if (newItemCount > 0)
                        {
                            query.SQL.Text = "insert into SHSPXX(SHSPID,SHDM,SPDM,SPMC,SPJC,PYM,UNIT,SPHS,SPGG,HH,SHSPFLID,SHSBID,SHHTID) values(:SHSPID,:SHDM,:SPDMNEW,:SPMC,:SPJC,:PYM,:UNIT,:SPHS,:SPGG,:HH,:SHSPFLID,:SHSBID,:SHHTID)";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Article item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    item.Id = seqId++;
                                    query.ParamByName("SHSPID").AsInteger = item.Id;
                                    query.ParamByName("SPDMNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("SPMC").AsString = item.Name;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPJC").AsChineseString = item.ShortName;
                                    else
                                        query.ParamByName("SPJC").AsString = item.ShortName;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("PYM").AsChineseString = item.Spell;
                                    else
                                        query.ParamByName("PYM").AsString = item.Spell;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("UNIT").AsChineseString = item.Unit;
                                    else
                                        query.ParamByName("UNIT").AsString = item.Unit;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPHS").AsChineseString = item.Color;
                                    else
                                        query.ParamByName("SPHS").AsString = item.Color;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPGG").AsChineseString = item.Spec;
                                    else
                                        query.ParamByName("SPGG").AsString = item.Spec;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("HH").AsChineseString = item.ModelCode;
                                    else
                                        query.ParamByName("HH").AsString = item.ModelCode;
                                    query.ParamByName("SHSPFLID").AsInteger = item.CategoryId;
                                    query.ParamByName("SHSBID").AsInteger = item.BrandId;
                                    query.ParamByName("SHHTID").AsInteger = item.ContractId;
                                    query.ExecSQL();
                                }
                            }
                            query.Close();

                            query.SQL.Text = "insert into SHSPXX_DM(SHSPID,SHDM,SPDM) values(:SHSPID,:SHDM,:SPDMNEW)";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Article item in itemsToUpdate)
                            {
                                if (!item.IsExist)
                                {
                                    query.ParamByName("SHSPID").AsInteger = item.Id;
                                    query.ParamByName("SPDMNEW").AsString = item.NewCode;
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update SHSPXX set SPDM = :SPDMNEW,SPMC = :SPMC,SPJC = :SPJC,PYM = :PYM,UNIT = :UNIT,SPHS = :SPHS,SPGG = :SPGG,HH = :HH,SHSPFLID = :SHSPFLID,SHSBID = :SHSBID,SHHTID = :SHHTID";
                            query.SQL.Add(" where SHDM = :SHDM and SPDM = :SPDM");
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Article item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    query.ParamByName("SPDM").AsString = item.Code;
                                    query.ParamByName("SPDMNEW").AsString = item.NewCode;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPMC").AsChineseString = item.Name;
                                    else
                                        query.ParamByName("SPMC").AsString = item.Name;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPJC").AsChineseString = item.ShortName;
                                    else
                                        query.ParamByName("SPJC").AsString = item.ShortName;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("PYM").AsChineseString = item.Spell;
                                    else
                                        query.ParamByName("PYM").AsString = item.Spell;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("UNIT").AsChineseString = item.Unit;
                                    else
                                        query.ParamByName("UNIT").AsString = item.Unit;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPHS").AsChineseString = item.Color;
                                    else
                                        query.ParamByName("SPHS").AsString = item.Color;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("SPGG").AsChineseString = item.Spec;
                                    else
                                        query.ParamByName("SPGG").AsString = item.Spec;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("HH").AsChineseString = item.ModelCode;
                                    else
                                        query.ParamByName("HH").AsString = item.ModelCode;
                                    query.ParamByName("SHSPFLID").AsInteger = item.CategoryId;
                                    query.ParamByName("SHSBID").AsInteger = item.BrandId;
                                    query.ParamByName("SHHTID").AsInteger = item.ContractId;
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                            
                            query.SQL.Text = "update SHSPXX_DM set SHSPID = :SHSPID,SPDM = :SPDMNEW where SHDM = :SHDM and SPDM = :SPDM";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Article item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    query.ParamByName("SHSPID").AsInteger = item.Id;
                                    query.ParamByName("SPDM").AsString = item.Code;
                                    query.ParamByName("SPDMNEW").AsString = item.NewCode;
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }

            return (msg.Length == 0);
        }
        public static bool SaveDeptArticle(out string msg, string companyCode, List<string> codesToDelete, List<DeptArticle> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //DbCommand cmd = conn.CreateCommand();
            //StringBuilder sql = new StringBuilder();
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
                CyQuery query = new CyQuery(conn);
                try
                {
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select SHBMID from SHBM where SHDM = :SHDM and BMDM = :BMDM";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (DeptArticle item in itemsToUpdate)
                        {
                            query.ParamByName("BMDM").AsString = item.DeptCode;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.DeptId = query.FieldByName("SHBMID").AsInteger;
                            }
                            else
                            {
                                query.Close();
                                msg = "部门代码" + item.DeptCode + "未上传到crm";
                                return false;
                            }
                            query.Close();
                        }
                        //query.Close();

                        query.SQL.Text = "select SHSPID from SHSPXX where SHDM = :SHDM and SPDM = :SPDM";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (DeptArticle item in itemsToUpdate)
                        {
                            query.ParamByName("SPDM").AsString = item.ArticleCode;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.ArticleId = query.FieldByName("SHSPID").AsInteger;
                            }
                            else
                            {
                                query.Close();
                                msg = "商品代码" + item.ArticleCode + "未上传到crm";
                                return false;
                            }
                            query.Close();
                        }
                        //query.Close();

                        query.SQL.Text = "select SHSPID from BMSP where SHBMID = :SHBMID and SHSPID = :SHSPID ";
                        foreach (DeptArticle item in itemsToUpdate)
                        {
                            query.ParamByName("SHBMID").AsInteger = item.DeptId;
                            query.ParamByName("SHSPID").AsInteger = item.ArticleId;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                            }
                            query.Close();
                        }
                        //query.Close();
                    }

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);
                        query.SQL.Text = "insert into BMSP(SHBMID,SHSPID) values(:SHBMID,:SHSPID)";
                        foreach (DeptArticle item in itemsToUpdate)
                        {
                            if (!item.IsExist)
                            {
                                query.ParamByName("SHBMID").AsInteger = item.DeptId;
                                query.ParamByName("SHSPID").AsInteger = item.ArticleId;
                                query.ExecSQL();
                            }
                        }
                        //query.Close();
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }
        public static bool SavePromotion(out string msg, string companyCode, List<string> codesToDelete, List<Promotion> itemsToUpdate)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
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
                CyQuery query = new CyQuery(conn);
                try
                {
                    int newItemCount = 0;
                    int seqId = 0;
                    DateTime serverTime = DbUtils.GetDbServerTime(query.Command);
                    if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0))
                    {
                        query.SQL.Text = "select CXHDBH from CXHDDEF where SHDM = :SHDM and CXHDBH = :CXHDBH ";
                        query.ParamByName("SHDM").AsString = companyCode;
                        foreach (Promotion item in itemsToUpdate)
                        {
                            query.ParamByName("CXHDBH").AsInteger = item.Id;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                item.IsExist = true;
                            }
                            else
                                newItemCount++;
                            query.Close();
                        }
                        query.Close();
                    }
                    if (newItemCount > 0)
                        seqId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "CXHDDEF", newItemCount) - newItemCount + 1;

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        query.SetTrans(dbTrans);

                        if ((codesToDelete != null) && (codesToDelete.Count > 0))
                        {
                            query.SQL.Text = "delete from CXHDDEF where SHDM = :SHDM and CXHDBH = :CXHDBH ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (string code in codesToDelete)
                            {
                                query.ParamByName("CXHDBH").AsInteger = int.Parse(code);
                                query.ExecSQL();
                            }
                            query.Close();
                        }
                        if (newItemCount > 0)
                        {

                            query.SQL.Text = "insert into CXHDDEF(CXID,SHDM,CXHDBH,CXZT,CXNR,NIAN,CXQS,KSSJ,JSSJ,SCSJ) values(:CXID,:SHDM,:CXHDBH,:CXZT,:CXNR,:NIAN,:CXQS,:KSSJ,:JSSJ,:SCSJ)";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Promotion item in itemsToUpdate)
                            {
                                if (!item.IsExist)  //一般一个商户只有一个客户端上传基本数据，所以这里没有考虑刚才没有现在已有的情况
                                {
                                    query.ParamByName("CXID").AsInteger = seqId++;
                                    query.ParamByName("CXHDBH").AsInteger = item.Id;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("CXZT").AsChineseString = item.Subject;
                                    else
                                        query.ParamByName("CXZT").AsString = item.Subject;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("CXNR").AsChineseString = item.Content;
                                    else
                                        query.ParamByName("CXNR").AsString = item.Content;
                                    query.ParamByName("NIAN").AsInteger = item.Year;
                                    query.ParamByName("CXQS").AsInteger = item.PeriodNum;
                                    query.ParamByName("KSSJ").AsDateTime = item.BeginTime;
                                    query.ParamByName("JSSJ").AsDateTime = item.EndTime;
                                    query.ParamByName("SCSJ").AsDateTime = serverTime;
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        if ((itemsToUpdate != null) && (itemsToUpdate.Count > 0) && (itemsToUpdate.Count != newItemCount))
                        {
                            query.SQL.Text = "update CXHDDEF set CXZT = :CXZT,CXNR = :CXNR,NIAN = :NIAN,CXQS = :CXQS,KSSJ = :KSSJ,JSSJ = :JSSJ,SCSJ = :SCSJ where SHDM = :SHDM and CXHDBH = :CXHDBH ";
                            query.ParamByName("SHDM").AsString = companyCode;
                            foreach (Promotion item in itemsToUpdate)
                            {
                                if (item.IsExist)
                                {
                                    query.ParamByName("CXHDBH").AsInteger = item.Id;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("CXZT").AsChineseString = item.Subject;
                                    else
                                        query.ParamByName("CXZT").AsString = item.Subject;
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        query.ParamByName("CXNR").AsChineseString = item.Content;
                                    else
                                        query.ParamByName("CXNR").AsString = item.Content;
                                    query.ParamByName("NIAN").AsInteger = item.Year;
                                    query.ParamByName("CXQS").AsInteger = item.PeriodNum;
                                    query.ParamByName("KSSJ").AsDateTime = item.BeginTime;
                                    query.ParamByName("JSSJ").AsDateTime = item.EndTime;
                                    query.ParamByName("SCSJ").AsDateTime = serverTime;
                                    query.ExecSQL();
                                }
                            }
                            query.Close();
                        }
                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SQL.Text);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);

        }

    }
}
