using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading;
using ChangYi.Pub;

namespace ChangYi.Crm.Server
{
    public class BackgroundProc
    {
        public void AutoRollBackTrans()
        {
            while (true)
            {
                DateTime time1 = DateTime.Now;
                DbConnection conn = null;
                DbCommand cmd = null;
                try
                {
                    conn = DbConnManager.GetDbConnection("CRMDB");
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
                        string dbSysName = DbUtils.GetDbSystemName(conn);

                        cmd = conn.CreateCommand();

                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                        List<int> transIds = null;
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select JYID from HYK_JYCL_ZBZT ");
                        DbUtils.SpellSqlParameter(conn, sql, " where ", "ZBSJ", "<");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime.AddMinutes(-10));
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            transIds = new List<int>();
                            transIds.Add(DbUtils.GetInt(reader, 0));
                            while (reader.Read())
                            {
                                transIds.Add(DbUtils.GetInt(reader, 0));
                            }
                        }
                        reader.Close();
                        cmd.Parameters.Clear();

                        if (transIds != null)
                        {
                            foreach (int transId in transIds)
                            {
                                int transType = 0;
                                bool isCashCard = false;
                                bool isVipCoupon = false;
                                bool isCodedCoupon = false;
                                sql.Length = 0;
                                sql.Append("select JYLX,BJ_CZK,BJ_YHQ from HYK_JYCL where JYZT = 1 and JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    transType = DbUtils.GetInt(reader, 0);
                                    isCashCard = DbUtils.GetBool(reader, 1);
                                    int couponFlag = DbUtils.GetInt(reader, 2);
                                    isVipCoupon = (couponFlag == 1);
                                    isCodedCoupon = (couponFlag == 2);
                                }
                                reader.Close();

                                if (transType > 0)
                                    serverTime = DbUtils.GetDbServerTime(cmd);
                                DbTransaction dbTrans = conn.BeginTransaction();
                                try
                                {
                                    cmd.Transaction = dbTrans;
                                    if (transType > 0)
                                    {
                                        sql.Length = 0;
                                        sql.Append("update HYK_JYCL set JYZT = 3 ");
                                        DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                        sql.Append(" where JYID = ").Append(transId);
                                        sql.Append("   and JYZT = 1");
                                        cmd.CommandText = sql.ToString();
                                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                                        if (cmd.ExecuteNonQuery() != 0)
                                        {
                                            cmd.Parameters.Clear();

                                            if (isCashCard)
                                            {
                                                #region 取支付明细
                                                List<CrmCashCardPayment> paymentList = new List<CrmCashCardPayment>();
                                                sql.Length = 0;
                                                sql.Append("select HYID,JE from HYK_JYCLITEM_CZK ");
                                                sql.Append("where JYID = ").Append(transId);
                                                sql.Append("  and JE > 0 ");
                                                cmd.CommandText = sql.ToString();
                                                reader = cmd.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    CrmCashCardPayment payment = new CrmCashCardPayment();
                                                    paymentList.Add(payment);
                                                    payment.CardId = DbUtils.GetInt(reader, 0);
                                                    payment.PayMoney = DbUtils.GetDouble(reader, 1);
                                                }
                                                reader.Close();
                                                #endregion

                                                #region 取消每张卡的冻结金额
                                                double balance = 0;
                                                foreach (CrmCashCardPayment payment in paymentList)
                                                {
                                                    sql.Length = 0;
                                                    sql.Append("update HYK_JEZH set YE = YE + ").Append(payment.PayMoney.ToString("f2"));
                                                    sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) - ").Append(payment.PayMoney.ToString("f2"));
                                                    sql.Append(" where HYID = ").Append(payment.CardId);
                                                    sql.Append("   and ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) >= ").Append(payment.PayMoney.ToString("f2"));
                                                    cmd.CommandText = sql.ToString();
                                                    if (cmd.ExecuteNonQuery() == 0)
                                                    {
                                                        throw new Exception(string.Format("储值帐户冻结金额不足, CardId={0}", payment.CardId));
                                                    }

                                                    PosProc.EncryptBalanceOfCashCard(cmd, sql, payment.CardId, serverTime, out balance);
                                                }
                                                #endregion
                                            }
                                            else if (isVipCoupon)
                                            {
                                                #region 取支付明细
                                                List<CrmCouponPayment> paymentList = new List<CrmCouponPayment>();
                                                sql.Length = 0;
                                                sql.Append("select HYID,YHQID,CXID,JSRQ,MDFWDM,JE from HYK_JYCLITEM_YHQ ");
                                                sql.Append("where JYID = ").Append(transId);
                                                sql.Append("  and JE > 0 ");
                                                cmd.CommandText = sql.ToString();
                                                reader = cmd.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    CrmCouponPayment payment = new CrmCouponPayment();
                                                    paymentList.Add(payment);
                                                    payment.VipId = DbUtils.GetInt(reader, 0);
                                                    payment.CouponType = DbUtils.GetInt(reader, 1);
                                                    payment.PromId = DbUtils.GetInt(reader, 2);
                                                    payment.ValidDate = DbUtils.GetDateTime(reader, 3);
                                                    payment.StoreScope = DbUtils.GetString(reader, 4).Trim();
                                                    payment.PayMoney = DbUtils.GetDouble(reader, 5);
                                                }
                                                reader.Close();
                                                #endregion

                                                #region 取消每张卡的优惠券冻结金额
                                                foreach (CrmCouponPayment payment in paymentList)
                                                {
                                                    cmd.Parameters.Clear();
                                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", payment.ValidDate);
                                                    sql.Length = 0;
                                                    sql.Append("update HYK_YHQZH set JE = JE + ").Append(payment.PayMoney.ToString("f2"));
                                                    sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) - ").Append(payment.PayMoney.ToString("f2"));
                                                    sql.Append(" where YHQID = ").Append(payment.CouponType);
                                                    sql.Append("   and HYID = ").Append(payment.VipId);
                                                    sql.Append("   and CXID = ").Append(payment.PromId);
                                                    DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                                    if (payment.StoreScope.Length == 0)
                                                        sql.Append("  and MDFWDM = ' '");
                                                    else
                                                        sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                                    sql.Append("   and ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) >= ").Append(payment.PayMoney.ToString("f2"));
                                                    cmd.CommandText = sql.ToString();
                                                    if (cmd.ExecuteNonQuery() == 0)
                                                    {
                                                        cmd.Parameters.Clear();
                                                        throw new Exception(string.Format("优惠券帐户冻结金额不足, VipId={0}, CouponType={1}", payment.VipId, payment.CouponType));
                                                    }
                                                    cmd.Parameters.Clear();
                                                }
                                                #endregion
                                            }
                                            else if (isCodedCoupon)
                                            {
                                                #region 取交易的代码券
                                                List<string> couponCodeList = new List<string>();
                                                sql.Length = 0;
                                                sql.Append("select YHQCODE from HYK_JYCLITEM_YHQDM ");
                                                sql.Append("where JYID = ").Append(transId);
                                                cmd.CommandText = sql.ToString();
                                                reader = cmd.ExecuteReader();
                                                while (reader.Read())
                                                {
                                                    couponCodeList.Add(DbUtils.GetString(reader, 0));
                                                }
                                                reader.Close();
                                                #endregion

                                                #region 取消每张代码券的冻结状态
                                                foreach (string couponCode in couponCodeList)
                                                {
                                                    sql.Length = 0;
                                                    sql.Append("update YHQCODE set STATUS = 1,MDID_YQ = null,YQSJ = null ");
                                                    sql.Append(" where YHQCODE = '").Append(couponCode).Append("'");
                                                    sql.Append("   and STATUS = 2 ");
                                                    cmd.CommandText = sql.ToString();
                                                    if (cmd.ExecuteNonQuery() == 0)
                                                    {
                                                        throw new Exception(string.Format("代码券 {0} 状态不对", couponCode));
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                        cmd.Parameters.Clear();
                                    }
                                    sql.Length = 0;
                                    sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();

                                    dbTrans.Commit();
                                }
                                catch (Exception e)
                                {
                                    dbTrans.Rollback();
                                    CrmServerPlatform.WriteErrorLog("\r\n Auto rollback trans (id=" + transId.ToString() + "), sql：" + cmd.CommandText + "\r\n error: " + e.Message);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (cmd == null)
                            CrmServerPlatform.WriteErrorLog("\r\n Auto rollback trans error: " + e.Message);
                        else
                            CrmServerPlatform.WriteErrorLog("\r\n Auto rollback trans, sql：" + cmd.CommandText + "\r\n error: " + e.Message);
                    }
                }
                finally
                {
                    if (conn != null) conn.Close();
                }

                DateTime time2 = DateTime.Now;
                int interval = 300000 - MathUtils.Truncate(time2.Subtract(time1).TotalMilliseconds);
                if (interval < 1000)
                    interval = 1000;
                Thread.Sleep(interval);   //5分钟后再来
            }
        }
    }
}
