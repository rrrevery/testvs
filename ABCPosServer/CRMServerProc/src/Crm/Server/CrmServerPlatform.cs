using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Threading;
using ChangYi.Pub;

namespace ChangYi.Crm.Server
{
    public class CrmConfig
    {
        public bool NoNegativeValidCent = false;
        public bool NoNegativeYearCent = false;
        public bool IsSingleCompany = false;
        public int LengthVerifyOfCashCard = 0;
        public int LengthVerifyOfVipCard = 0;
        public bool NoCentWhenPayCoupon = false;
        public bool NoOfferCouponWhenPayCoupon = false;
        public bool PayCashCardWithArticle = false;
        public bool ShowErrorDetail = false;
        public bool LoadBalance = false;
        public bool ForwardRemoteCashCard = false;
        public bool DbCharSetIsNotChinese = false;
        public bool DesEncryptVipCardTrackSecondly = true;
        public bool DesEncryptCashCardTrackSecondly = true;
        public int NoRecordsAffectedAfterExecSql = 0;
        public bool IsUpgradedProject2013 = false;
        public string WXUrl = string.Empty;
    }

    public class CrmAccessUser
    {
        public string UserId = string.Empty;
        public string Password = string.Empty;
        public bool HttpHandlerAccess = false;
        public bool WebServiceAccess = false;
        public bool WcfAccess = false;
    }

    public class CrmServerPubData
    {
        public byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
    }

    public class CrmServerPlatform
    {
        private static Object sync = new Object();
        private static List<CrmAreaInfo> AreaList = new List<CrmAreaInfo>();
        private static List<CrmStoreInfo> StoreList = new List<CrmStoreInfo>();
        private static List<CrmAccessUser> AccessUserList = new List<CrmAccessUser>();
        private static List<string> WebServiceClientIpList = new List<string>();

        public static int CurrentDbId = 0;

        public static CrmServerPubData PubData = new CrmServerPubData();

        public static CrmConfig Config = new CrmConfig();

        private static DailyLogFileWriter DataLogFileWriter = null;
        private static LogFileWriter ErrorLogFileWriter = null;

        private static Thread autoRollBackTransThread = null;
        public static bool isInitiated = false;
        public static void InitiateData()
        {
            if (!isInitiated)
            {
                DbCommand cmd = null;
                try
                {
                    string str = ConfigurationManager.AppSettings["ShowErrorDetail"];
                    Config.ShowErrorDetail = ((str != null) && (str.ToLower().Equals("true")));
                    str = ConfigurationManager.AppSettings["LoadBalance"];
                    Config.LoadBalance = ((str != null) && (str.ToLower().Equals("true")));
                    str = ConfigurationManager.AppSettings["CrmDbCharSetIsNotChinese"];
                    Config.DbCharSetIsNotChinese = ((str != null) && (str.ToLower().Equals("true")));
                    str = ConfigurationManager.AppSettings["WXUrl"];
                    if (str != null)
                        Config.WXUrl = str;
                    string logPath = ConfigurationManager.AppSettings["bfcrm.log.path"];
                    DataLogFileWriter = new DailyLogFileWriter(logPath, "bfcrm");
                    ErrorLogFileWriter = new LogFileWriter(logPath);//, );
                    if (!ErrorLogFileWriter.SetFileName("bfcrmerror.log"))
                    {
                        for (int i = 1; i < 100; i++)
                        {
                            if (ErrorLogFileWriter.SetFileName(string.Format("bfcrmerror_{0}.log",i.ToString().PadLeft(2,'0'))))
                                break;
                        }
                    }

                    DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
                    cmd = conn.CreateCommand();
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select JLBH,CUR_VAL from BFCONFIG where JLBH in (0,520000102,520000103,520000112,520000113,520000115,520000120,520000121,520000123,520000128,520000129)");
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
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int jlbh = DbUtils.GetInt(reader, 0);
                            int curVal = DbUtils.GetInt(reader, 1);
                            switch (jlbh)
                            {
                                case 0:
                                    CurrentDbId = curVal;
                                    break;
                                case 520000102:
                                    Config.NoNegativeValidCent = (curVal != 0);
                                    break;
                                case 520000103:
                                    Config.IsSingleCompany = (curVal != 0);
                                    break;
                                case 520000112:
                                    Config.LengthVerifyOfCashCard = curVal;
                                    //Config.LengthVerifyOfVipCard = curVal;
                                    break;
                                case 520000113:
                                    Config.LengthVerifyOfVipCard = curVal;
                                    //Config.LengthVerifyOfCashCard = curVal;
                                    break;
                                case 520000115:
                                    Config.NoNegativeYearCent = (curVal != 0);
                                    break;
                                case 520000120:
                                    Config.NoCentWhenPayCoupon = (curVal != 0);
                                    break;
                                case 520000121:
                                    Config.NoOfferCouponWhenPayCoupon = (curVal != 0);
                                    break;
                                case 520000123:
                                    Config.PayCashCardWithArticle = (curVal != 0);
                                    break;
                                case 520000128:     //卡磁道内容是否二次加密
                                    Config.DesEncryptVipCardTrackSecondly = (curVal != 0);
                                    Config.DesEncryptCashCardTrackSecondly = (curVal != 0);
                                    break;
                                case 520000129:
                                    Config.IsUpgradedProject2013 = (curVal != 0);
                                    break;
                            }
                        }
                        reader.Close();
                        cmd.CommandText = "select USER_DM,USER_PSW,HTTPOK,WSOK,WCFOK from CRMUSER";
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmAccessUser user = new CrmAccessUser();
                            AccessUserList.Add(user);
                            user.UserId = DbUtils.GetString(reader, 0);
                            user.Password = DbUtils.GetString(reader, 1);
                            user.HttpHandlerAccess = DbUtils.GetBool(reader, 2);
                            user.WebServiceAccess = DbUtils.GetBool(reader, 3);
                            user.WcfAccess = DbUtils.GetBool(reader, 4);
                        }
                        reader.Close();
                        cmd.CommandText = "select IPADDR from CRMCLIENT where WSOK = 1 ";
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            WebServiceClientIpList.Add(DbUtils.GetString(reader, 0));
                        }
                        reader.Close();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                catch (Exception e)
                {
                    if (cmd == null)
                        CrmServerPlatform.WriteErrorLog("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  System Init Error: " + e.Message.ToString());
                    else
                        CrmServerPlatform.WriteErrorLog("\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "  System Init, sql: " + cmd.CommandText + "\r\n Error: " + e.Message.ToString());
                }

                ChangYi.Crm.Server.BackgroundProc backgroundProc = new ChangYi.Crm.Server.BackgroundProc();
                autoRollBackTransThread = new Thread(new ThreadStart(backgroundProc.AutoRollBackTrans));
                autoRollBackTransThread.IsBackground = true;
                autoRollBackTransThread.Start();

                isInitiated = true;
            }
        }

        public static void FinalizeData()
        {
            if (autoRollBackTransThread != null)
                autoRollBackTransThread.Abort();
            if (DataLogFileWriter != null)
            {
                DataLogFileWriter.Close();
            }
            if (ErrorLogFileWriter != null)
            {
                ErrorLogFileWriter.Close();
            }
        }

        private static void ExecSqlToCheckRecordsAffected(DbCommand cmd, string tableName, string sql, StringBuilder msg)
        {
            try
            {
                cmd.CommandText = sql;
                if (cmd.ExecuteNonQuery() == 0)
                    msg.Append("<br><br> 表 ").Append(tableName).Append(" ......OK");
                else
                    msg.Append("<br><br> 表 ").Append(tableName).Append(" ......NO");
            }
            catch (Exception e)
            {
                msg.Append("<br><br> 表 ").Append(tableName).Append(" ......不存在");
            }
        }
        public static void CheckTablesForRecordsAffectedAfterExecSql(out string msg)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            try
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    msg = "CRMDB 连接失败：" + e.Message;
                    return;
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("<br><br> 检查一些表在执行update语句后，返回的影响行数是否有问题：");
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    ExecSqlToCheckRecordsAffected(cmd, "HYK_JFZH", "update HYK_JFZH set HYID = HYID where 1 = 2",sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_MDJF", "update HYK_MDJF set HYID = HYID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_JEZH", "update HYK_JEZH set HYID = HYID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_YHQZH", "update HYK_YHQZH set HYID = HYID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_YEBD", "update HYK_YEBD set HYID = HYID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_JYCL", "update HYK_JYCL set JYID = JYID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_XFJL", "update HYK_XFJL set XFJLID = XFJLID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_XFJL_SP", "update HYK_XFJL_SP set XFJLID = XFJLID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_XFJL_SP_YQFT", "update HYK_XFJL_SP_YQFT set XFJLID = XFJLID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYXFJL_BSK", "update HYXFJL_BSK set XFJLID = XFJLID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_XFLJDFQ", "update HYK_XFLJDFQ set HYID = HYID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "HYK_THKQCE", "update HYK_THKQCE set HYID = HYID where 1 = 2", sb);

                    ExecSqlToCheckRecordsAffected(cmd, "YHQCODE", "update YHQCODE set YHQCODE = YHQCODE where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "CXHD_FQZJE", "update CXHD_FQZJE set CXID = CXID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "CXHD_ZSLPSL_DAY", "update CXHD_ZSLPSL_DAY set CXID = CXID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "YHKFQZJE", "update YHKFQZJE set CXID = CXID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "YHKFQZJE_DAY", "update YHKFQZJE_DAY set CXID = CXID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "YHKFQJE_CARD", "update YHKFQJE_CARD set CXID = CXID where 1 = 2", sb);
                    ExecSqlToCheckRecordsAffected(cmd, "YHKFQJE_CARD_DAY", "update YHKFQJE_CARD_DAY set CXID = CXID where 1 = 2", sb);
                }
                finally
                {
                    dbTrans.Commit();
                }
                msg = sb.ToString();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void WriteDataLog(String dateStr, String logText)
        {
            if (DataLogFileWriter != null)
                DataLogFileWriter.Write(dateStr,logText);
        }

        public static void WriteErrorLog(String logText)
        {
            if (ErrorLogFileWriter != null)
                ErrorLogFileWriter.Write(logText);
        }

        public static void ParseException(out string errorMsg, out string errorLog, out bool dbConnError, Exception e)
        {
            errorMsg = string.Empty;
            errorLog = string.Empty;
            dbConnError = false;
            if (e is MyDbException)
            {
                dbConnError = (e as MyDbException).IsConnError;
                if (dbConnError)
                    errorLog = "CRM 数据库连接出错 " + e.Message;
                else
                {
                    errorLog = "CRM 数据库操作出错 " + e.Message;
                    if (CrmServerPlatform.Config.ShowErrorDetail && ((e as MyDbException).Sql.Length > 0))
                        errorLog = errorLog + "\r\n " + (e as MyDbException).Sql;
                }
            }
            else
                errorLog = "CRM 服务处理出错 " + e.Message;
            if (CrmServerPlatform.Config.ShowErrorDetail)
                errorMsg = errorLog;
            else
            {
                if (e is MyDbException)
                {
                    if ((e as MyDbException).IsConnError)
                        errorMsg = "CRM 数据库连接出错";
                    else
                        errorMsg = "CRM 数据库操作出错";
                }
                else
                    errorMsg = "CRM 服务处理出错 ";
            }
        }

        public static CrmAreaInfo GetAreaInfo(out string msg, int areaId)
        {
            msg = string.Empty;
            lock (sync)
            {
                foreach (CrmAreaInfo area in AreaList)
                {
                    if (area.AreaId == areaId)
                    {
                        return area;
                    }
                }

                DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
                DbCommand cmd = conn.CreateCommand();
                StringBuilder sql = new StringBuilder();
                sql.Append("select CRMIP,CRMPORT,CRMURL from CRMQZJCFG where QYID = ").Append(areaId);
                cmd.CommandText = sql.ToString();
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
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            CrmAreaInfo area = new CrmAreaInfo();
                            AreaList.Add(area);
                            area.AreaId = areaId;
                            area.ServerIp = DbUtils.GetString(reader, 0);
                            area.ServerPort = DbUtils.GetInt(reader, 1);
                            area.ServiceUrl = DbUtils.GetString(reader, 2);
                            reader.Close();
                            return area;
                        }
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        throw new MyDbException(e.Message, cmd.CommandText);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            msg = string.Format("区域ID {0} 还没有在 CRM 定义服务配置", areaId);
            return null;
        }

        public static bool GetStoreInfo(out string msg, CrmStoreInfo storeInfo)
        {
            msg = string.Empty;
            storeInfo.Company = string.Empty;
            storeInfo.StoreId = -1;
            lock (sync)
            {
                foreach (CrmStoreInfo store in StoreList)
                {
                    if (store.StoreCode.Equals(storeInfo.StoreCode))
                    {
                        storeInfo.Company = store.Company;
                        storeInfo.StoreId = store.StoreId;
                        return true;
                    }
                }

                DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
                DbCommand cmd = conn.CreateCommand();
                StringBuilder sql = new StringBuilder();
                sql.Append("select SHDM,MDID from MDDY where MDDM = '").Append(storeInfo.StoreCode).Append("'");
                cmd.CommandText = sql.ToString();
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
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            CrmStoreInfo store2 = new CrmStoreInfo();
                            StoreList.Add(store2);
                            store2.StoreCode = storeInfo.StoreCode;
                            store2.Company = DbUtils.GetString(reader, 0).Trim();
                            store2.StoreId = DbUtils.GetInt(reader, 1);

                            storeInfo.Company = store2.Company;
                            storeInfo.StoreId = store2.StoreId;
                        }
                        reader.Close();
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
            if (storeInfo.StoreId <= 0)
            {
                msg = string.Format("门店代码 {0} 还没有上传到 CRM 库", storeInfo.StoreCode);
                return false;
            }
            return true;
        }

        public static bool CheckCrmUser(out string msg, int accessMode, string userId, string password)
        {
            msg = string.Empty;
            return true;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select USER_PSW from CRMUSER where USER_DM = '").Append(userId).Append("'");
            switch (accessMode)
            {
                case 1:
                    sql.Append(" and HTTPOK = 1 ");
                    break;
                case 2:
                    sql.Append(" and WSOK = 1 ");
                    break;
                case 3:
                    sql.Append(" and WCFOK = 1 ");
                    break;
                default:
                    sql.Append(" and 1 = 2 ");
                    break;
            }
            cmd.CommandText = sql.ToString();
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
                DbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string pass = DbUtils.GetString(reader, 0);
                    reader.Close();
                    if (!password.Equals(pass))
                    {
                        msg = "登录BFCRM的用户密码不正确";
                        return false;
                    }
                }
                else
                {
                    reader.Close();
                    msg = "登录BFCRM的用户代码不存在";
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
            return true;
            if ((accessMode < 1) || (accessMode > 3))
            {
                msg = "accessMode 值只能是 1,2,3";
                return false;
            }
            foreach (CrmAccessUser user in AccessUserList)
            {
                if (user.UserId.Equals(userId))
                {
                    if (!user.Password.Equals(password))
                    {
                        msg = "登录BFCRM的用户密码不正确";
                        return false;
                    }
                    switch (accessMode)
                    {
                        case 1:
                            if (!user.HttpHandlerAccess)
                            {
                                msg = "该用户不能通过HttpHandler方式接入";
                                return false;
                            }
                            break;
                        case 2:
                            if (!user.WebServiceAccess)
                            {
                                msg = "该用户不能通过Web Service方式接入";
                                return false;
                            }
                            break;
                        case 3:
                            if (!user.WcfAccess)
                            {
                                msg = "该用户不能通过WCF方式接入";
                                return false;
                            }
                            break;
                    }
                    return true;
                }
            }
            msg = "登录BFCRM的用户代码(" + userId + ")不存在";
            return false;
        }

        public static bool CheckWebServiceClient(out string msg, string clientIp)
        {
            msg = string.Empty;
            if ((WebServiceClientIpList.Count > 0) && (!IpUtils.ContainIp(WebServiceClientIpList, clientIp)))
            {
                msg = "客户端(" + clientIp + ")未被授权访问";
                return false;
            }
            return true;
        }
    }
}
