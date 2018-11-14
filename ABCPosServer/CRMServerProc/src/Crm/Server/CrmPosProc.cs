using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;
using ChangYi.Crm.Rule;
using System.IO;
using System.Net;
using System.Xml;

namespace ChangYi.Crm.Server
{
    public class PosProc
    {
        private static void DeleteRSaleBillFromDb(DbCommand cmd, StringBuilder sql, int serverBillId)
        {
            sql.Length = 0;
            sql.Append("delete from HYK_XFJFBS where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_ZFFS where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_MBJZ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_ZFFS_MBJZ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_FQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_YQFT where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_YQJC where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_ZFFS where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_YHKZF where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_SP_ZFFS_FQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_ZFFS_FQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_FQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL_FQDM where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYTHJL_KQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYTHJL_TQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
            sql.Length = 0;
            sql.Append("delete from HYK_XFJL where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            cmd.ExecuteNonQuery();
        }

        public static void EncryptBalanceOfCashCard(DbCommand cmd, StringBuilder sql, int cardId, DateTime serverTime, out double balance)
        {
            balance = 0;
            double frozen = 0;
            byte[] bytesTM = new byte[8];
            sql.Length = 0;
            string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);
            switch (dbSysName)
            {
                case DbUtils.SybaseDbSystemName:
                    sql.Append("select YE,JYDJJE,convert(binary(8),TM) as TM from HYK_JEZH where HYID = ").Append(cardId);
                    break;
                case DbUtils.OracleDbSystemName:
                    sql.Append("select YE,JYDJJE,to_char(TM) as TM from HYK_JEZH where HYID = ").Append(cardId);
                    break;
            }
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                balance = DbUtils.GetDouble(reader, 0);
                frozen = DbUtils.GetDouble(reader, 1);
                switch (dbSysName)
                {
                    case DbUtils.SybaseDbSystemName:
                        reader.GetBytes(2, 0, bytesTM, 0, 8);
                        break;
                    case DbUtils.OracleDbSystemName:
                         string strTM = reader.GetString(2);
                        if ((strTM != null) && (strTM.Length > 0))
                        {
                            Int64 longTM = Int64.Parse(strTM);
                            for (int j = 0; j < 8; j++)
                            {
                                bytesTM[j] = (byte)(longTM >> (j * 8));
                            }
                        }
                        break;
                }
            }
            reader.Close();

            byte[] newKey = EncryptUtils.DesEncrypt(CrmServerPlatform.PubData.DesKey, bytesTM);
            byte[] bytesBalance = new byte[8];
            int intBalance = Convert.ToInt32(balance * 100);
            for (int j = 0; j < 4; j++)
            {
                bytesBalance[j] = (byte)(intBalance >> (j * 8));
            }
            for (int j = 4; j < 8; j++)
            {
                bytesBalance[j] = 0;
            }
            byte[] bytesDAC = EncryptUtils.DesEncrypt(newKey, bytesBalance);

            DbUtils.AddDatetimeInputParameterAndValue(cmd, "BDSJ", serverTime);
            DbUtils.AddBytesInputParameterAndValue(cmd, "DAC", bytesDAC);
            sql.Length = 0;
            sql.Append("update HYK_YEBD set ");
            DbUtils.SpellSqlParameter(cmd.Connection,sql,"","BDSJ","=");
            DbUtils.SpellSqlParameter(cmd.Connection,sql,",","DAC","=");
            sql.Append(" where HYID = ").Append(cardId);  
            cmd.CommandText = sql.ToString();
            if (cmd.ExecuteNonQuery() == 0)
            {
                sql.Length = 0;
                sql.Append("insert into HYK_YEBD (HYID,BDSJ,DAC) values(").Append(cardId);
                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "BDSJ", "");
                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "DAC", "");
                sql.Append(")");
                cmd.CommandText = sql.ToString();
                cmd.ExecuteNonQuery();
            }
            cmd.Parameters.Clear();

            balance += frozen;
        }

        public static void UpdateCashCardAccount(out string msg, DbCommand cmd, StringBuilder sql, int procType, bool existFrozen, int cardId, double procMoney, out double balance, int procSeq, int transId, int storeId, string posId, int billId, DateTime procTime, DateTime accountDate, string cashier, string brief)
        {
            msg = string.Empty;
            balance = 0;

            bool isAdd = false;
            double debitMoney = 0;  //借方
            double creditMoney = 0; //贷方
            if (procType == 7)  //支付
            {
                creditMoney = procMoney;
                if (MathUtils.DoubleAGreaterThanDoubleB(procMoney, 0))
                    isAdd = false;
                else
                {
                    isAdd = true;
                    procMoney = -procMoney;
                }
            }
            else if (procType == 1)    //存款
            {
                debitMoney = procMoney;
                if (MathUtils.DoubleAGreaterThanDoubleB(procMoney, 0))
                    isAdd = true;
                else
                {
                    isAdd = false;
                    procMoney = -procMoney;
                }
            }
            else
                return;

            string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);
            cmd.Parameters.Clear();
            sql.Length = 0;
            if (isAdd)
            {
                sql.Append("update HYK_JEZH set YE = YE + ").Append(procMoney.ToString("f2"));
            }
            else
            {
                if (existFrozen)
                    sql.Append("update HYK_JEZH set JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) - ").Append(procMoney.ToString("f2")); 
                else
                    sql.Append("update HYK_JEZH set YE = YE - ").Append(procMoney.ToString("f2"));
            }
            sql.Append(" where HYID = ").Append(cardId);
            if (!isAdd)
            {
                if (existFrozen)
                    sql.Append("  and ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) >= ").Append(procMoney.ToString("f2"));
                else
                    sql.Append("  and YE >= ").Append(procMoney.ToString("f2"));
            }
            cmd.CommandText = sql.ToString();
            if (cmd.ExecuteNonQuery() == 0)
            {
                if (isAdd)
                {
                    sql.Length = 0;
                    sql.Append("insert into HYK_JEZH (HYID,JYDJJE,YE) ");
                    sql.Append("  values(").Append(cardId);
                    sql.Append(",0,").Append(procMoney.ToString("f2"));
                    sql.Append(")");
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    if (existFrozen)
                        msg = "更新金额账户时，冻结金额不够";
                    else
                        msg = "更新金额账户时，余额不够";
                    return;
                }
            }

            EncryptBalanceOfCashCard(cmd, sql, cardId, procTime, out balance);

            sql.Length = 0;
            switch (dbSysName)
            {
                case DbUtils.SybaseDbSystemName:
                    sql.Append("insert into HYK_JEZCLJL(HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                    sql.Append("  values(").Append(cardId);
                    break;
                case DbUtils.OracleDbSystemName:
                    sql.Append("insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                    sql.Append("  values(").Append(procSeq).Append(",").Append(cardId);
                    break;
            }
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "CLSJ", "");
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JZRQ", "");
            sql.Append(",").Append(procType);
            sql.Append(",").Append(billId);
            sql.Append(",").Append(storeId);
            sql.Append(",'").Append(posId).Append("'");
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
            sql.Append(",").Append(transId);
            sql.Append(",'").Append(cashier);
            sql.Append("',").Append(debitMoney.ToString("f2"));
            sql.Append(",").Append(creditMoney.ToString("f2"));
            sql.Append(",").Append(balance.ToString("f2"));
            sql.Append(")");
            cmd.CommandText = sql.ToString();
            cmd.Parameters.Clear();
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", procTime);
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
            DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", brief, CrmServerPlatform.Config.DbCharSetIsNotChinese);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public static void UpdateVipCouponAccount(out string msg, DbCommand cmd, StringBuilder sql, int procType, bool isTransferOut, bool existFrozen, int vipId, int couponType, int promId, DateTime validDate, string payStoreScope, double procMoney, out double balance, int procSeq, int transId, int storeId, string posId, int billId, DateTime procTime, DateTime accountDate, string cashier, string brief)
        {
            //"前台消费"
            //"前台消费冲正"
            //"前台无单退货退券"
            //"前台无单退货退券冲正"
            //"前台选单退货退券"
            //"前台选单退货退券冲正"

            //"前台转出"
            //"前台转出冲正"
            //"前台转入"
            //"前台转入冲正"

            //"前台返券"
            //"前台直接扣券"
            //"前台直接扣券冲正"
            //"前台选单退货扣券"
            //"前台选单退货扣券冲正"
            //"前台扣返券款退还"
            //"前台扣返券款退还冲正"

            msg = string.Empty;
            balance = 0;

            bool isAdd = false;
            double debitMoney = 0;  //借方
            double creditMoney = 0; //贷方
            if (procType == 7)  //用券
            {
                creditMoney = procMoney;
                if (MathUtils.DoubleAGreaterThanDoubleB(procMoney, 0))
                    isAdd = false;
                else
                {
                    isAdd = true;
                    procMoney = -procMoney;
                }
            }
            else if (procType == 10)    //返券
            {
                debitMoney = procMoney;
                if (MathUtils.DoubleAGreaterThanDoubleB(procMoney, 0))
                    isAdd = true;
                else
                {
                    isAdd = false;
                    procMoney = -procMoney;
                }
            }
            else if (procType == 5)  //转储，并卡
            {
                if (isTransferOut)
                {
                    creditMoney = procMoney;
                    if (MathUtils.DoubleAGreaterThanDoubleB(procMoney, 0))
                        isAdd = false;
                    else
                    {
                        isAdd = true;
                        procMoney = -procMoney;
                    }
                }
                else
                {
                    debitMoney = procMoney;
                    if (MathUtils.DoubleAGreaterThanDoubleB(procMoney, 0))
                        isAdd = true;
                    else
                    {
                        isAdd = false;
                        procMoney = -procMoney;
                    }
                }
            }
            else
                return;

            string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);

            cmd.Parameters.Clear();
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", validDate);
            sql.Length = 0;
            if (isAdd)
            {
                sql.Append("update HYK_YHQZH set JE = JE + ").Append(procMoney.ToString("f2"));
            }
            else
            {
                if (existFrozen)
                     sql.Append("update HYK_YHQZH set JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) - ").Append(procMoney.ToString("f2"));
                else
                    sql.Append("update HYK_YHQZH set JE = JE - ").Append(procMoney.ToString("f2"));
            }
            sql.Append(" where YHQID = ").Append(couponType);
            sql.Append("   and HYID = ").Append(vipId);
            sql.Append("   and CXID = ").Append(promId);
            DbUtils.SpellSqlParameter(cmd.Connection, sql," and ", "JSRQ","=");
            if (payStoreScope.Length == 0)
                sql.Append("   and MDFWDM = ' '");
            else
                sql.Append("   and MDFWDM = '").Append(payStoreScope).Append("'");
            if (!isAdd)
            {
                if (existFrozen)
                    sql.Append("  and ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) >= ").Append(procMoney.ToString("f2"));
                else
                    sql.Append("  and JE >= ").Append(procMoney.ToString("f2"));
            }
            cmd.CommandText = sql.ToString();
            if (cmd.ExecuteNonQuery() == 0)
            {
                if (isAdd)
                {
                    sql.Length = 0;
                    sql.Append("insert into HYK_YHQZH(HYID,YHQID,CXID,JSRQ,MDFWDM,JYDJJE,JE) ");
                    sql.Append("  values(").Append(vipId);
                    sql.Append(",").Append(couponType);
                    sql.Append(",").Append(promId);
                    DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JSRQ", "");
                    if (payStoreScope.Length == 0)
                        sql.Append(",' '");
                    else
                        sql.Append(",'").Append(payStoreScope).Append("'");
                    sql.Append(",0,").Append(procMoney.ToString("f2"));
                    sql.Append(")");
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    if (existFrozen)
                        msg = "更新优惠券账户时，冻结金额不够";
                    else
                        msg = "更新优惠券账户时，余额不够";
                    return;
                }
            }

            sql.Length = 0;
            sql.Append("select JE, JYDJJE from HYK_YHQZH ");
            sql.Append("where YHQID = ").Append(couponType);
            sql.Append("  and HYID = ").Append(vipId);
            sql.Append("  and CXID = ").Append(promId);
            DbUtils.SpellSqlParameter(cmd.Connection, sql," and ", "JSRQ", "=");
            if (payStoreScope.Length == 0)
                sql.Append("   and MDFWDM = ' '");
            else
                sql.Append("   and MDFWDM = '").Append(payStoreScope).Append("'");
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
                balance = DbUtils.GetDouble(reader, 0) + DbUtils.GetDouble(reader, 1);
            reader.Close();

            sql.Length = 0;
            switch (dbSysName)
            {
                case DbUtils.SybaseDbSystemName:
                    sql.Append("insert into HYK_YHQCLJL(HYID,CLSJ,JZRQ,CLLX,YHQID,CXID,JSRQ,MDFWDM,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                    sql.Append("  values(").Append(vipId);
                    break;
                case DbUtils.OracleDbSystemName:
                    sql.Append("insert into HYK_YHQCLJL(JYBH,HYID,CLSJ,JZRQ,CLLX,YHQID,CXID,JSRQ,MDFWDM,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                    sql.Append("  values(").Append(procSeq).Append(",").Append(vipId);
                    break;
            }
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "CLSJ", "");
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JZRQ", "");
            sql.Append(",").Append(procType);
            sql.Append(",").Append(couponType);
            sql.Append(",").Append(promId);
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JSRQ", "");
            if (payStoreScope.Length == 0)
                sql.Append(",' '");
            else
                sql.Append(",'").Append(payStoreScope).Append("'");
            sql.Append(",").Append(billId);
            sql.Append(",").Append(storeId);
            sql.Append(",'").Append(posId).Append("'");
            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
            sql.Append(",").Append(transId);
            sql.Append(",'").Append(cashier);
            sql.Append("',").Append(debitMoney.ToString("f2"));
            sql.Append(",").Append(creditMoney.ToString("f2"));
            sql.Append(",").Append(balance.ToString("f2"));
            sql.Append(")");
            cmd.CommandText = sql.ToString();
            cmd.Parameters.Clear();
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", procTime);
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", validDate);
            DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", brief, CrmServerPlatform.Config.DbCharSetIsNotChinese);
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public static bool Login(out string msg, CrmLoginData loginData)
        {
            msg = string.Empty;

            if (loginData.StoreInfo.StoreCode.Length > 0)
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg,loginData.StoreInfo))
                    return false;
            }
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            sql.Append("select USER_PSW from CRMUSER where USER_DM = '").Append(loginData.UserCode).Append("'");
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
                        string pass = DbUtils.GetString(reader, 0);
                        reader.Close();
                        if (!loginData.Password.Equals(pass))
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

                    if (loginData.ClientSystem.Equals("bfpos"))
                    {
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ", serverTime);
                        sql.Length = 0;
                        sql.Append("select count(*) from HYKFQDYD a ");
                        sql.Append(" where a.SHDM = '").Append(loginData.StoreInfo.Company).Append("'");
                        sql.Append("  and ((a.BJ_HTFQ is null) or (a.BJ_HTFQ = 0)) ");
                        sql.Append("  and a.DJLX in (0,2) ");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ")).Append("  between RQ1 and RQ2 ");
                        sql.Append("  and a.STATUS = 2 ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            loginData.ExistPromOfferCoupon = (DbUtils.GetInt(reader, 0) > 0);
                        }
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select count(*) from CXMBJZDYD a ");
                        sql.Append(" where a.SHDM = '").Append(loginData.StoreInfo.Company).Append("'");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ")).Append("  between RQ1 and RQ2 ");
                        sql.Append("  and a.STATUS = 2 ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            loginData.ExistPromDecMoney = (DbUtils.GetInt(reader, 0) > 0);
                        }
                        reader.Close();
                    }
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
            loginData.LengthVerifyOfCashCard = CrmServerPlatform.Config.LengthVerifyOfCashCard;
            loginData.LengthVerifyOfVipCard = CrmServerPlatform.Config.LengthVerifyOfVipCard;
            loginData.PayCashCardWithArticle = CrmServerPlatform.Config.PayCashCardWithArticle;

            return (msg.Length == 0);
        }

        public static void Logoff(string storeCode, string PosId)
        {
            //return false;
        }

        public static void TestCode(out string resp)
        {
            resp = string.Empty;

            
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<br>以下为测试代码的结果：<br>");
            //DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            //CyQuery query = new CyQuery(conn);
            //try
            //{
            //    try
            //    {
            //        conn.Open();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new MyDbException(e.Message, true);
            //    }
            //    try
            //    {
            //        query.SQL.Text = "select HYID,HY_NAME from HYK_HYXX where HYK_NO = :HYK_NO";
            //        query.ParamByName("HYK_NO").AsString = "801000000774";
            //        query.Open();
            //        //query.Next();
            //        sb.Append("HYID=").Append(query.FieldByName("HYID").AsInteger).Append(",HY_NAME=").Append(query.FieldByName("HY_NAME").AsString);
            //        query.Close();
            //    }
            //    catch (Exception e)
            //    {
            //        if (e is MyDbException)
            //            throw e;
            //        else
            //            throw new MyDbException(e.Message, query.SQL.Text);
            //    }
            //    resp = sb.ToString();
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }        
  

        private static bool CheckCard(out string msg, string cardCode, bool isToCheckCardCode, string cardCodeToCheck, bool isToCheckVerifyCode, string verifyCode, int lengthVerifyCode)
        {
            //CrmServerPlatform.WriteErrorLog("\r\n" + cardCode + "," + isToCheckCardCode.ToString() + "," + cardCodeToCheck + "," + isToCheckVerifyCode + "," + verifyCode + "," + lengthVerifyCode.ToString());
            msg = string.Empty;
            if ((isToCheckCardCode) && (cardCodeToCheck != null) && (cardCodeToCheck.Length > 0))
            {
                if (!(cardCodeToCheck.Equals(cardCode)))
                    msg = "输入的卡号验证错误";
                if (msg.Length > 0)
                    return false;
            }
            if ((isToCheckVerifyCode) && (lengthVerifyCode > 0))
            {
                if ((verifyCode == null) || (verifyCode.Length == 0))
                    msg = "请输入验证码来验证";
                else if (cardCode.Length <= lengthVerifyCode)
                {
                    if (!(verifyCode.Equals(cardCode)))
                        msg = "输入的验证码错误";
                }
                else
                {
                    if (!(verifyCode.Equals(cardCode.Substring(cardCode.Length - lengthVerifyCode, lengthVerifyCode))))
                        msg = "输入的验证码错误";
                }
                if (msg.Length > 0)
                    return false;
            }
            return true;
        }

        /* 取会员卡
         * condType = 0 磁道内容
         * condType = 1 会员ID
         * condType = 2 卡号
         * condType = 3 手机号
         * condType = 4 证件号码
         */
        public static bool GetVipCard(out string msg, out CrmVipCard vipCard, int condType, string condValue, string cardCodeToCheck, string verifyCode)
        {
            msg = string.Empty;
            vipCard = null;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            
            DateTime today = DateTime.MinValue;
            bool isChinese = false;
            bool isChildCard = false;
            try
            {
                try
                {
                    string s = condValue;
                    if (s.Length > 30) 
                    {
                        s = CrmEnCryptUtils.DesDecryptCardTrack15(s);
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        int times = CrmEnCryptUtils.ConvertDateTimeInt(serverTime);
                        if (!s.Substring(0, 5).Equals("10125"))
                        {
                            msg = "不是本商场的二维码";
                            return false;
                        }
                        if ((times - long.Parse(s.Substring(5, 10))) > 120)
                        {
                            msg = "二维码时间已超过两分钟，已失效";
                            return false;
                        }
                        condType = 0;
                        condValue = s.Substring(15, s.Length - 15);
                    }
                    if ((condType >= 0) && (condType <= 2))
                    {
                        sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM,b.BJ_CZZH ");
                        sql.Append("from HYK_HYXX a,HYKDEF b ");
                        sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                        switch (condType)
                        {
                            case 0:
                                if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                                    condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                                sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                                break;
                            case 1:
                                sql.Append("  and a.HYID = ").Append(condValue);
                                break;
                            case 2:
                                sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                                break;
                        }
                    }
                    else if ((condType >= 3) && (condType <= 4))
                    {
                        sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM,b.BJ_CZZH,c.SJHM,c.PHONE,c.CSRQ,c.BJ_CLD,").Append(DbUtils.GetDbServerTimeFuncSql(cmd)).Append(" as CURRDATE,c.E_MAIL,c.TXDZ ");
                        sql.Append("from HYK_HYXX a,HYKDEF b,HYK_GRXX c ");
                        sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                        sql.Append("  and a.HYID = c.HYID ");
                        switch (condType)
                        {
                            case 3:
                                sql.Append("  and c.SJHM = '").Append(condValue).Append("'");
                                break;
                            case 4:
                                sql.Append("  and c.SFZBH = '").Append(condValue).Append("'");
                                break;
                        }
                    }
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        if ((condType == 0) || (condType == 2))
                        {
                            isChildCard = true;
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select a.HYID,a.HYKTYPE,c.HYK_NO,a.HY_NAME,c.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM,b.BJ_CZZH ");
                            sql.Append("from HYK_HYXX a,HYKDEF b,HYK_CHILD_JL c ");
                            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                            sql.Append("  and a.HYID = c.HYID ");
                            switch (condType)
                            {
                                case 0:
                                    sql.Append("  and c.CDNR = '").Append(condValue).Append("'");
                                    break;
                                case 2:
                                    sql.Append("  and c.HYK_NO = '").Append(condValue).Append("'");
                                    break;
                            }
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "会员卡不存在";
                                return false;
                            }
                        }
                        else
                        {
                            reader.Close();
                            msg = "会员卡不存在";
                            return false;
                        }
                    }

                    int status = DbUtils.GetInt(reader, 4);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "会员卡状态无效";
                        return false;
                    }

                    bool isCashCard = DbUtils.GetBool(reader, 12);
                    if (isCashCard)
                    {
                        reader.Close();
                        msg = "这是张储值卡";
                        return false;
                    }

                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 13);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    //if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                    //{
                    //    reader.Close();
                    //    return false;
                    //}

                    vipCard = new CrmVipCard();
                    vipCard.CardId = DbUtils.GetInt(reader, 0);
                    vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                    vipCard.CardCode = DbUtils.GetString(reader, 2);
                    vipCard.VipName = DbUtils.GetString(reader, 3, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                    if (isChildCard)
                        vipCard.VipName = vipCard.VipName + "(副卡)";

                    vipCard.CardTypeName = DbUtils.GetString(reader, 7, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                    vipCard.CanCent = DbUtils.GetBool(reader, 8);
                    vipCard.CanOwnCoupon = DbUtils.GetBool(reader, 9);
                    vipCard.DiscType = DbUtils.GetInt(reader, 10);
                    vipCard.CanDisc = (vipCard.DiscType > 0);
                    vipCard.CanReturn = DbUtils.GetBool(reader, 11);
                    vipCard.bCashCard = DbUtils.GetBool(reader, 14);
                    if ((condType >= 3) && (condType <= 4))
                    {
                        vipCard.Mobile = DbUtils.GetString(reader, 15);
                        vipCard.PhoneCode = vipCard.Mobile;
                        if (vipCard.PhoneCode.Trim().Length == 0)
                            vipCard.PhoneCode = DbUtils.GetString(reader, 16);
                        vipCard.Birthday = DbUtils.GetDateTime(reader, 17);
                        isChinese = DbUtils.GetBool(reader, 18);
                        today = DbUtils.GetDateTime(reader, 19);
                        //vipCard.EMail = DbUtils.GetString(reader, 20);
                        //vipCard.Address = DbUtils.GetString(reader, 21);
                    }
                    if (reader.Read())
                    {
                        reader.Close();
                        vipCard = null;
                        msg = "查询出多张会员卡";
                        return false;
                    }
                    reader.Close();

                    if ((vipCard.CanDisc) && (status == 6))
                        vipCard.CanDisc = false;    //呆滞卡不能打折

                    sql.Length = 0;
                    sql.Append("select WCLJF,BQJF,BNLJJF from HYK_JFZH where HYID = ").Append(vipCard.CardId.ToString());
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        vipCard.ValidCent = DbUtils.GetDouble(reader, 0);
                        vipCard.StageCent = DbUtils.GetDouble(reader, 1);
                        vipCard.YearCent = DbUtils.GetDouble(reader, 2);
                    }
                    else
                    {
                        vipCard.ValidCent = 0;
                        vipCard.YearCent = 0;
                        vipCard.StageCent = 0;
                    }

                    reader.Close();
                    if ((condType >= 0) && (condType <= 2))
                    {
                        sql.Length = 0;
                        sql.Append("select SJHM,PHONE,CSRQ,BJ_CLD,").Append(DbUtils.GetDbServerTimeFuncSql(cmd)).Append(" as CURRDATE,E_MAIL,TXDZ from HYK_GRXX where HYID = ").Append(vipCard.CardId.ToString());
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            vipCard.Mobile = DbUtils.GetString(reader, 0);
                            vipCard.PhoneCode = vipCard.Mobile;
                            if (vipCard.PhoneCode.Trim().Length == 0)
                                vipCard.PhoneCode = DbUtils.GetString(reader, 1);
                            vipCard.Birthday = DbUtils.GetDateTime(reader, 2);
                            isChinese = DbUtils.GetBool(reader, 3);
                            today = DbUtils.GetDateTime(reader, 4);
                            vipCard.EMail = DbUtils.GetString(reader, 5);
                            vipCard.Address = DbUtils.GetString(reader, 6);
                        }
                        reader.Close();
                    }

                    if ((vipCard.Birthday > DateTime.MinValue) && (DateTimeUtils.CheckBirthday(vipCard.Birthday, today, isChinese)))
                    {
                        vipCard.Hello = "祝您生日快乐";
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
                /*
                * 以下代码是测试某些功能用的，因为这个方法调用起来方便，所以放在此处
  
                                DbTransaction dbTrans = conn.BeginTransaction();
                                try
                                {
                                    cmd.Transaction = dbTrans;
                        
                                        sql.Length = 0;
                                        sql.Append("update HYK_HYXX set HY_NAME = '张三' where HYID = 1");
                                        cmd.CommandText = sql.ToString();
                                        int i = cmd.ExecuteNonQuery();
                                        if (i != 1)
                                            throw new Exception(i.ToString());
                        
                                    dbTrans.Commit();
                                }
                                catch (Exception e)
                                {
                                    dbTrans.Rollback();
                                    throw e;
                                }
                */

            }
            finally
            {
                conn.Close();
            }

            return (msg.Length == 0);
        }

        public static bool GetVipCardList(out string msg, List<CrmVipCard> vipCardList, int condType, string condValue)
        {
            msg = string.Empty;
            vipCardList.Clear();
            if ((condType != 3) && (condType != 4))
                return false;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM,c.SJHM,c.PHONE,c.CSRQ,c.BJ_CLD,").Append(DbUtils.GetDbServerTimeFuncSql(cmd)).Append(" as CURRDATE ,c.E_MAIL,c.TXDZ,c.SFZBH");
            sql.Append(" from HYK_HYXX a,HYKDEF b,HYK_GRXX c ");
            sql.Append(" where a.HYKTYPE = b.HYKTYPE ");
            sql.Append("  and a.HYID = c.HYID ");
            sql.Append("  and a.STATUS >= 0 ");
            sql.Append("  and ((b.BJ_JF <> 0) or (b.YHFS > 0)) ");  //用于积分和折扣
            switch (condType)
            {
                case 3:
                    sql.Append("  and c.SJHM = '").Append(condValue).Append("'");
                    break;
                case 4:
                    sql.Append("  and c.SFZBH = '").Append(condValue).Append("'");
                    break;
            }

            DateTime today = DateTime.MinValue;
            bool isChinese = false;
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
                    CrmVipCard vipCard = new CrmVipCard();
                    vipCardList.Add(vipCard);
                    vipCard.CardId = DbUtils.GetInt(reader, 0);
                    vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                    vipCard.CardCode = DbUtils.GetString(reader, 2);
                    vipCard.VipName = DbUtils.GetString(reader, 3);
                    int status = DbUtils.GetInt(reader, 4);
                    vipCard.CardTypeName = DbUtils.GetString(reader, 7);
                    vipCard.CanCent = DbUtils.GetBool(reader, 8);
                    vipCard.CanOwnCoupon = DbUtils.GetBool(reader, 9);
                    vipCard.DiscType = DbUtils.GetInt(reader, 10);
                    vipCard.CanDisc = (vipCard.DiscType > 0);
                    vipCard.CanReturn = DbUtils.GetBool(reader, 11);

                    vipCard.PhoneCode = DbUtils.GetString(reader, 14);
                    if (vipCard.PhoneCode.Trim().Length == 0)
                        vipCard.PhoneCode = DbUtils.GetString(reader, 15);
                    vipCard.Birthday = DbUtils.GetDateTime(reader, 16);
                    isChinese = DbUtils.GetBool(reader, 17);
                    today = DbUtils.GetDateTime(reader, 18);
                    vipCard.IdCardCode = DbUtils.GetString(reader, 21);
                    if ((vipCard.Birthday > DateTime.MinValue) && (DateTimeUtils.CheckBirthday(vipCard.Birthday, today, isChinese)))
                    {
                        vipCard.Hello = "祝您生日快乐";
                    }
                    if ((vipCard.CanDisc) && (status == 6))
                        vipCard.CanDisc = false;    //呆滞卡不能打折         
                }
                reader.Close();


                if (vipCardList.Count > 0)
                {
                    sql.Length = 0;
                    sql.Append("select HYID,WCLJF,BQJF,BNLJJF from HYK_JFZH where HYID in (").Append(vipCardList[0].CardId);
                    for (int i = 1; i < vipCardList.Count; i++)
                    {
                        sql.Append(",").Append(vipCardList[i].CardId);
                    }
                    sql.Append(")");
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int cardId = DbUtils.GetInt(reader, 0);
                        foreach (CrmVipCard vipCard in vipCardList)
                        {
                            if (vipCard.CardId == cardId)
                            {
                                vipCard.ValidCent = DbUtils.GetDouble(reader, 1);
                                vipCard.YearCent = DbUtils.GetDouble(reader, 2);
                                vipCard.StageCent = DbUtils.GetDouble(reader, 3);
                                break;
                            }
                        }
                    }
                    reader.Close();
                }
            }
            finally
            {
                conn.Close();
            }

            return (msg.Length == 0);
        }
        
        public static bool GetVipCardToOfferCoupon(out string msg, out CrmVipCard vipCard, int serverBillId, int condType, string condValue, string cardCodeToCheck, string verifyCode)
        {
            msg = string.Empty;
            vipCard = null;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            
            DateTime vipBirthday = DateTime.MinValue;
            DateTime today = DateTime.MinValue;
            bool isChinese = false;
            bool isChildCard = false;
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
                    string s = condValue;
                    if (s.Length > 30)
                    {
                        s = CrmEnCryptUtils.DesDecryptCardTrack15(s);
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        int times = CrmEnCryptUtils.ConvertDateTimeInt(serverTime);
                        if (!s.Substring(0, 5).Equals("10125"))
                        {
                            msg = "不是本商场的二维码";
                            return false;
                        }
                        if ((times - long.Parse(s.Substring(5, 10))) > 120)
                        {
                            msg = "二维码时间已超过两分钟，已失效";
                            return false;
                        }
                        condType = 0;
                        condValue = s.Substring(15, s.Length - 15);
                    }
                    if ((condType >= 0) && (condType <= 2))
                    {
                        sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM ");
                        sql.Append("from HYK_HYXX a,HYKDEF b ");
                        sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                        switch (condType)
                        {
                            case 0:
                                if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                                    condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                                sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                                break;
                            case 1:
                                sql.Append("  and a.HYID = ").Append(condValue);
                                break;
                            case 2:
                                sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                                break;
                        }
                    }
                    else if ((condType >= 3) && (condType <= 4))
                    {
                        sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM,c.CSRQ,c.BJ_CLD,").Append(DbUtils.GetDbServerTimeFuncSql(cmd)).Append(" as CURRDATE ");
                        sql.Append("from HYK_HYXX a,HYKDEF b,HYK_GRXX c ");
                        sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                        sql.Append("  and a.HYID = c.HYID ");
                        switch (condType)
                        {
                            case 3:
                                sql.Append("  and c.SJHM = '").Append(condValue).Append("'");
                                break;
                            case 4:
                                sql.Append("  and c.SFZBH = '").Append(condValue).Append("'");
                                break;
                        }
                    }
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        if ((condType == 0) || (condType == 2))
                        {
                            isChildCard = true;
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select a.HYID,a.HYKTYPE,c.HYK_NO,a.HY_NAME,c.STATUS,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM ");
                            sql.Append("from HYK_HYXX a,HYKDEF b,HYK_CHILD_JL c ");
                            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                            sql.Append("  and a.HYID = c.HYID ");
                            switch (condType)
                            {
                                case 0:
                                    sql.Append("  and c.CDNR = '").Append(condValue).Append("'");
                                    break;
                                case 2:
                                    sql.Append("  and c.HYK_NO = '").Append(condValue).Append("'");
                                    break;
                            }
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "会员卡不存在";
                                return false;
                            }
                        }
                        else
                        {
                            reader.Close();
                            msg = "会员卡不存在";
                            return false;
                        }
                    }

                    int status = DbUtils.GetInt(reader, 4);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "会员卡状态无效";
                        return false;
                    }

                    bool isCashCard = DbUtils.GetBool(reader, 11);
                    if (isCashCard)
                    {
                        reader.Close();
                        msg = "这是张储值卡";
                        return false;
                    }

                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 12);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                    {
                        reader.Close();
                        return false;
                    }
                    vipCard = new CrmVipCard();
                    vipCard.CardId = DbUtils.GetInt(reader, 0);
                    vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                    vipCard.CardCode = DbUtils.GetString(reader, 2);
                    vipCard.VipName = DbUtils.GetString(reader, 3, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                    if (isChildCard)
                        vipCard.VipName = vipCard.VipName + "(副卡)";
                    string passwd = DbUtils.GetString(reader, 5);
                    vipCard.CardTypeName = DbUtils.GetString(reader, 6, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                    vipCard.CanCent = DbUtils.GetBool(reader, 7);
                    vipCard.CanOwnCoupon = DbUtils.GetBool(reader, 8);
                    vipCard.DiscType = DbUtils.GetInt(reader, 9);
                    vipCard.CanDisc = (vipCard.DiscType > 0);
                    vipCard.CanReturn = DbUtils.GetBool(reader, 10);
                    if ((condType >= 3) && (condType <= 4))
                    {
                        vipBirthday = DbUtils.GetDateTime(reader, 13);
                        isChinese = DbUtils.GetBool(reader, 14);
                        today = DbUtils.GetDateTime(reader, 15);
                    }
                    reader.Close();

                    if ((vipCard.CanDisc) && (status == 6))
                        vipCard.CanDisc = false;    //呆滞卡不能打折

                    if (!vipCard.CanOwnCoupon)
                    {
                        msg = "这张卡没有开通优惠券账户";
                        return false;
                    }

                    int vipIdCent = 0;
                    int vipIdOfferCoupon = 0;
                    sql.Length = 0;
                    sql.Append("select STATUS,DJLX,HYID,HYID_FQ from HYK_XFJL  ");
                    sql.Append("where XFJLID = ").Append(serverBillId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        status = DbUtils.GetInt(reader, 0);
                        int billType = DbUtils.GetInt(reader, 1);
                        if (status == CrmPosData.BillStatusCheckedOut)
                            msg = string.Format("该销售单 {0} 在 CRM 中已结账，不能再补刷卡", serverBillId);
                        else if (billType != CrmPosData.BillTypeSale)
                            msg = "不是销售单，不能再补刷卡";
                        else
                        {
                            vipIdCent = DbUtils.GetInt(reader, 2);
                            vipIdOfferCoupon = DbUtils.GetInt(reader, 3);
                        }
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select DJLX from HYXFJL ");
                        sql.Append("where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能再补刷卡", serverBillId);
                        else
                            msg = string.Format("该销售单 {0} 在 CRM 中不存在,不能补刷卡", serverBillId);
                    }
                    reader.Close();

                    if (msg.Length > 0)
                        return false;

                    if (vipIdOfferCoupon != vipCard.CardId)
                    {
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;

                            sql.Length = 0;
                            sql.Append("update HYK_XFJL set HYID_FQ = ").Append(vipCard.CardId).Append(",HYKNO_FQ = '").Append(vipCard.CardCode).Append("'");
                            sql.Append(" where XFJLID = ").Append(serverBillId);
                            sql.Append("  and STATUS <> ").Append(CrmPosData.BillStatusCheckedOut);
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //取商品会员折扣
        public static bool GetArticleVipDisc(out string msg, int vipId, int vipType, CrmStoreInfo storeInfo, List<CrmArticle> articleList)
        {
            msg = string.Empty;

            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg,storeInfo))
                    return false;
            }
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            CrmVipCard vipCard = new CrmVipCard();
            vipCard.CardId = vipId;
            vipCard.CardTypeId = vipType;
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
                    sql.Append("select HYKTYPE,FXDW from HYK_HYXX where HYID = ").Append(vipId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        vipCard.CardTypeId = DbUtils.GetInt(reader, 0);
                        vipCard.IssueCardCompanyId = DbUtils.GetInt(reader, 1);
                    }
                    reader.Close();

                    sql.Length = 0;
                    sql.Append("select CSRQ,ZYID,ZJLXID,SEX,BJ_CLD from HYK_GRXX where HYID = ").Append(vipId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        vipCard.Birthday = DbUtils.GetDateTime(reader, 0);
                        vipCard.JobType = DbUtils.GetInt(reader, 1);
                        vipCard.IdCardType = DbUtils.GetInt(reader, 2);
                        vipCard.SexType = DbUtils.GetInt(reader, 3);
                        vipCard.BirthdayIsChinese = DbUtils.GetBool(reader, 4);
                    }
                    reader.Close();

                    foreach (CrmArticle article in articleList)
                    {
                        sql.Length = 0;
                        sql.Append("select SHSPID from SHSPXX_DM where SHDM = '").Append(storeInfo.Company).Append("' and SPDM = '").Append(article.ArticleCode).Append("'");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            article.ArticleId = DbUtils.GetInt(reader, 0);
                        }
                        reader.Close();
                    }
                    foreach (CrmArticle article in articleList)
                    {
                        if (article.ArticleId > 0)
                        {
                            sql.Length = 0;
                            sql.Append("select b.SPFLDM,a.SHSBID,a.SHHTID from SHSPXX a,SHSPFL b where a.SHSPFLID = b.SHSPFLID and a.SHSPID = ").Append(article.ArticleId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                article.CategoryCode = DbUtils.GetString(reader, 0);
                                article.BrandId = DbUtils.GetInt(reader, 1);
                                article.ContractId = DbUtils.GetInt(reader, 2);
                            }
                            reader.Close();
                        }
                    }
                    DateTime saleTime = DbUtils.GetDbServerTime(cmd);
                    PromRuleSearcher.LookupVipDiscRule(cmd,0, vipCard, storeInfo.Company,storeInfo.StoreId,saleTime, articleList);
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
            return (msg.Length == 0);
        }

        public static bool ChangeCardPassowrd(out string msg, int condType, string condValue, string cardCodeToCheck, string verifyCode, string oldPassword, string newPassword)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.HYID,a.HYK_NO,a.STATUS,b.BJ_YZM,a.BJ_PSW,a.PASSWORD ");
            sql.Append("from HYK_HYXX a,HYKDEF b ");
            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
            switch (condType)
            {
                case 0:
                    if (CrmServerPlatform.Config.DesEncryptCashCardTrackSecondly)
                        condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                    sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                    break;
                case 1:
                    sql.Append("  and a.HYID = ").Append(condValue);
                    break;
                case 2:
                    sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                    break;
                default:
                    sql.Append("  and 1=2");
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
                try
                {
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        msg = "卡不存在";
                        return false;
                    }
                    int status = DbUtils.GetInt(reader, 2);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "卡状态无效";
                        return false;
                    }

                    string cardCodeFromDB = DbUtils.GetString(reader, 1);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 3);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfCashCard)))
                    {
                        reader.Close();
                        return false;
                    }
                    bool existPasswd = DbUtils.GetBool(reader, 4);
                    if (existPasswd)
                    {
                        string passwdFromDB = DbUtils.GetString(reader, 5);
                        if ((oldPassword == null) || (oldPassword.Length == 0))
                        {
                            if (passwdFromDB.Length > 0)
                            {
                                reader.Close();
                                msg = "对不起, 请输入原密码";
                                return false;
                            }
                        }
                        else
                        {
                            string passwd2 = EncryptUtils.DesEncryptCardTrackSecondly(oldPassword, CrmServerPlatform.PubData.DesKey);
                            if (!passwdFromDB.Equals(passwd2))
                            {
                                reader.Close();
                                msg = "对不起, 原密码错误";
                                return false;
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        msg = "这张卡不需要密码";
                        return false;
                    }
                    int cardId = DbUtils.GetInt(reader, 1);
                    reader.Close();

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;

                        sql.Length = 0;
                        if ((newPassword == null) || (newPassword.Length == 0))
                        {
                            sql.Append("update HYK_HYXX set PASSWORD = null where HYID = ").Append(cardId);
                        }
                        else
                        {
                            string passwd2 = EncryptUtils.DesEncryptCardTrackSecondly(newPassword, CrmServerPlatform.PubData.DesKey);
                            sql.Append("update HYK_HYXX set PASSWORD = '").Append(passwd2).Append("'");
                            sql.Append(" where HYID = ").Append(cardId);
                        }
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();

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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        //取储值卡余额
        public static bool GetCashCard(out string msg, out int cardAreaId, out CrmCashCard cashCard, int condType, string condValue, string cardCodeToCheck, string verifyCode, string password,bool isCoupon, CrmStoreInfo storeInfo)
        {
            msg = string.Empty;
            cardAreaId = 0;
            cashCard = null;
           
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw new MyDbException(e.Message, true);
            }
            DbCommand cmd = conn.CreateCommand();
            string s = condValue;
            if (s.Length > 30)
            {
                s = CrmEnCryptUtils.DesDecryptCardTrack15(s);
                DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                int times = CrmEnCryptUtils.ConvertDateTimeInt(serverTime);
                if (!s.Substring(0, 5).Equals("10125"))
                {
                    msg = "不是本商场的二维码";
                    return false;
                }
                if ((times - long.Parse(s.Substring(5, 10))) > 120)
                {
                    msg = "二维码时间已超过两分钟，已失效";
                    return false;
                }
                condType = 0;
                condValue = s.Substring(15, s.Length - 15);
            }
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.YXQ,a.MDID,a.BJ_PSW,a.PASSWORD,b.BJ_CZZH,b.FS_SYMD,b.THBJ,b.BJ_YZM,b.BJ_TS ");
            sql.Append("from HYK_HYXX a,HYKDEF b ");
            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
            switch (condType)
            {
                case 0:
                    if (CrmServerPlatform.Config.DesEncryptCashCardTrackSecondly)
                        condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                    sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                    break;
                case 1:
                    sql.Append("  and a.HYID = ").Append(condValue);
                    break;
                case 2:
                    sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                    break;
                default:
                    sql.Append("  and 1=2");
                    break;
            }
            cmd.CommandText = sql.ToString();
            try
            {
                try
                {
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        if ((condType == 0) || (condType == 2))
                        {
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select a.HYID,a.HYKTYPE,c.HYK_NO,c.STATUS,a.YXQ,a.MDID,a.BJ_PSW,a.PASSWORD,b.BJ_CZZH,b.FS_SYMD,b.THBJ,b.BJ_YZM,b.BJ_TS ");
                            sql.Append("from HYK_HYXX a,HYKDEF b,HYK_CHILD_JL c ");
                            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                            sql.Append("  and a.HYID = c.HYID ");
                            switch (condType)
                            {
                                case 0:
                                    sql.Append("  and c.CDNR = '").Append(condValue).Append("'");
                                    break;
                                case 2:
                                    sql.Append("  and c.HYK_NO = '").Append(condValue).Append("'");
                                    break;
                            }
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "会员卡不存在";
                                return false;
                            }
                        }
                        else
                        {
                            reader.Close();
                            msg = "会员卡不存在";
                            return false;
                        }
                        /*reader.Close();
                        msg = "卡不存在";*/
                        if (CrmServerPlatform.Config.ForwardRemoteCashCard)
                        {
                            string cardCode = string.Empty;
                            sql.Length = 0;
                            sql.Append("select QYID from CZK_MD_HDMX where '").Append(cardCode).Append("' between KSKH and JSKH ");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                cardAreaId = DbUtils.GetInt(reader, 0);
                            }
                            reader.Close();
                        }
                        //return false;
                    }
                    int status = DbUtils.GetInt(reader, 3);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "卡状态无效";
                        return false;
                    }
                    bool existCashAccount = DbUtils.GetBool(reader, 8);
                    if (!existCashAccount)
                    {
                        reader.Close();
                        msg = "对不起, 该卡没有开通储值卡帐户";
                        return false;
                    }
                    bool isCouponDb = DbUtils.GetBool(reader, 12);
                    if (isCouponDb != isCoupon)
                    {
                        reader.Close();
                        if (isCouponDb)
                            msg = "对不起, 该卡是储值券";
                        else
                            msg = "对不起, 该卡是储值卡";
                        return false;
                    }
                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 11);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfCashCard)))
                    {
                        reader.Close();
                        return false;
                    }
                    bool existPasswd = DbUtils.GetBool(reader, 6);
                    if (existPasswd)
                    {
                        string passwdFromDB = DbUtils.GetString(reader, 7);
                        if ((password == null) || (password.Length == 0))
                        {
                            if (passwdFromDB.Length > 0)
                            {
                                reader.Close();
                                msg = "对不起, 请输入密码";
                                return false;
                            }
                        }
                        else
                        {
                            string passwd2 = EncryptUtils.DesEncryptCardTrackSecondly(password, CrmServerPlatform.PubData.DesKey);
                            if (!passwdFromDB.Equals(passwd2))
                            {
                                reader.Close();
                                msg = "对不起, 密码错误";
                                return false;
                            }
                        }
                    }

                    cashCard = new CrmCashCard();
                    cashCard.CardId = DbUtils.GetInt(reader, 0);
                    cashCard.CardTypeId = DbUtils.GetInt(reader, 1);
                    cashCard.ValidDate = DbUtils.GetDateTime(reader, 4);
                    int offerCardStoreId = DbUtils.GetInt(reader, 5);
                    int limitMode = DbUtils.GetInt(reader, 9);
                    cashCard.CanReturn = DbUtils.GetBool(reader, 10);
                    cashCard.CardCode = cardCodeFromDB;

                    reader.Close();
                    if ((limitMode == 1) || (limitMode == 2))
                    {
                        if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
                        {
                            if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                                return false;
                        }
                    }
                    if ((limitMode == 2) && (offerCardStoreId != storeInfo.StoreId))  //只能在发卡门店消费
                    {
                        msg = "对不起, 此卡不能在本门店使用";
                        return false;
                    }
                    if (limitMode == 1) //限制在指定门店消费
                    {
                        sql.Length = 0;
                        sql.Append("select MDID from CZK_MD where HYKTYPE=").Append(cashCard.CardTypeId).Append(" and MDID=").Append(storeInfo.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            cashCard = null;
                            reader.Close();
                            msg = "对不起, 此卡不能在本门店使用";
                            return false;
                        }
                        reader.Close();
                    }
                    if (msg.Length == 0)
                    {
                        string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);

                        byte[] bytesTM = new byte[8];
                        sql.Length = 0;
                        switch (dbSysName)
                        {
                            case DbUtils.SybaseDbSystemName:
                                sql.Append("select YE,PDJE,convert(binary(8),TM) as TM from HYK_JEZH where HYID = ").Append(cashCard.CardId);
                                break;
                            case DbUtils.OracleDbSystemName:
                                sql.Append("select YE,PDJE,to_char(TM) as TM from HYK_JEZH where HYID = ").Append(cashCard.CardId);
                                break;
                        }
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        bool existJEZH = false;
                        if (reader.Read())
                        {
                            existJEZH = true;
                            if (reader.IsDBNull(2))
                                msg = "余额被非法修改";
                            else
                            {
                                cashCard.Balance = DbUtils.GetDouble(reader, 0);
                                cashCard.Bottom = DbUtils.GetDouble(reader, 1);
                                switch (dbSysName)
                                {
                                    case DbUtils.SybaseDbSystemName:
                                        reader.GetBytes(2, 0, bytesTM, 0, 8);
                                        break;
                                    case DbUtils.OracleDbSystemName:
                                        string strTM = reader.GetString(2);
                                        if ((strTM == null) || (strTM.Length == 0))
                                            msg = " 余额被非法修改";
                                        else
                                        {
                                            Int64 longTM = Int64.Parse(strTM);
                                            for (int j = 0; j < 8; j++)
                                            {
                                                bytesTM[j] = (byte)(longTM >> (j * 8));
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        reader.Close();
                        if ((msg.Length == 0) && existJEZH)
                        {
                            byte[] newKey = EncryptUtils.DesEncrypt(CrmServerPlatform.PubData.DesKey, bytesTM);
                            sql.Length = 0;
                            sql.Append("select DAC from HYK_YEBD where HYID = ").Append(cashCard.CardId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                byte[] bytesDAC = new byte[8];
                                reader.GetBytes(0, 0, bytesDAC, 0, 8);
                                reader.Close();
                                byte[] bytesBalance = EncryptUtils.DesDecrypt(newKey, bytesDAC);

                                int intBalance = 0;	//int是4字节
                                for (int i = 3; i >= 0; i--)
                                {
                                    intBalance <<= 8;
                                    intBalance |= bytesBalance[i] & 0xff;
                                }
                                if (intBalance != Convert.ToInt32(cashCard.Balance * 100))
                                    msg = " 余额被非法修改";
                            }
                            else
                                msg = " 余额被非法修改";
                            cashCard.Balance = cashCard.Balance - cashCard.Bottom;
                        }
                        reader.Close();
                    }
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
            return (msg.Length == 0);
        }

        //取优惠券余额
        public static bool GetVipCoupon(out string msg, out int vipId, out string vipCode, List<CrmCoupon> couponList, int condType, string condValue, string cardCodeToCheck, string verifyCode, CrmStoreInfo storeInfo, bool requireValidDate)
        {
            msg = string.Empty;
            vipId = 0;
            vipCode = string.Empty;
            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            
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
                    string s = condValue;
                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                    if (s.Length > 30)
                    {
                        s = CrmEnCryptUtils.DesDecryptCardTrack15(s);
                        int times = CrmEnCryptUtils.ConvertDateTimeInt(serverTime);
                        if (!s.Substring(0, 5).Equals("10125"))
                        {
                            msg = "不是本商场的二维码";
                            return false;
                        }
                        if ((times - long.Parse(s.Substring(5, 10))) > 120)
                        {
                            msg = "二维码时间已超过两分钟，已失效";
                            return false;
                        }
                        condType = 0;
                        condValue = s.Substring(15, s.Length - 15);
                    }
                    sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    switch (condType)
                    {
                        case 0:
                            if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                                condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                            sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                            break;
                        case 1:
                            sql.Append("  and a.HYID = ").Append(condValue);
                            break;
                        case 2:
                            sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                            break;
                        default:
                            sql.Append("  and 1=2");
                            break;
                    }
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        if ((condType == 0) || (condType == 2))
                        {
                            reader.Close();
                            sql.Length = 0;//a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM
                            sql.Append("select a.HYID,a.HYKTYPE,c.HYK_NO,c.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM ");
                            sql.Append("from HYK_HYXX a,HYKDEF b,HYK_CHILD_JL c ");
                            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                            sql.Append("  and a.HYID = c.HYID ");
                            switch (condType)
                            {
                                case 0:
                                    sql.Append("  and c.CDNR = '").Append(condValue).Append("'");
                                    break;
                                case 2:
                                    sql.Append("  and c.HYK_NO = '").Append(condValue).Append("'");
                                    break;
                            }
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "会员卡不存在";
                                return false;
                            }
                        }
                        else
                        {
                            reader.Close();
                            msg = "会员卡不存在";
                            return false;
                        }
                        /*reader.Close();
                        msg = "会员卡不存在";
                        return false;*/
                    }
                    int status = DbUtils.GetInt(reader, 3);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "会员卡状态无效";
                        return false;
                    }
                    bool existCashAccount = DbUtils.GetBool(reader, 6);
                    if (!existCashAccount)
                    {
                        reader.Close();
                        msg = "对不起, 该卡没有开通优惠券帐户";
                        return false;
                    }
                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 7);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                    {
                        reader.Close();
                        return false;
                    }
                    vipId = DbUtils.GetInt(reader, 0);
                    //int cardTypeId = DbUtils.GetInt(reader,1);
                    vipCode = DbUtils.GetString(reader, 2);
                    reader.Close();

                    serverTime = DbUtils.GetDbServerTime(cmd);

                    sql.Length = 0;
                    if (requireValidDate)
                        sql.Append("select b.YHQID,b.YHQMC,a.JSRQ,sum(a.JE) from HYK_YHQZH a,YHQDEF b,YHQSYSH c ");
                    else
                        sql.Append("select b.YHQID,b.YHQMC,sum(a.JE) as JE from HYK_YHQZH a,YHQDEF b,YHQSYSH c ");
                    sql.Append("where a.YHQID = b.YHQID ");
                    sql.Append("  and a.YHQID = c.YHQID ");
                    sql.Append("  and a.HYID = ").Append(vipId.ToString());
                    sql.Append("  and a.JE > 0 ");
                    DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", ">=");
                    sql.Append("  and ((b.FS_YQMDFW = 1 and a.MDFWDM = ' ')");
                    sql.Append("   or (b.FS_YQMDFW = 2 and a.MDFWDM = '").Append(storeInfo.Company).Append("')");
                    sql.Append("   or (b.FS_YQMDFW = 3 and a.MDFWDM = '").Append(storeInfo.StoreCode).Append("'))");
                    sql.Append("  and b.BJ_TY = 0");
                    sql.Append("  and c.SHDM = '").Append(storeInfo.Company).Append("'");
                    if (requireValidDate)
                    {
                        sql.Append(" group by b.YHQID,b.YHQMC,a.JSRQ ");
                        sql.Append(" order by b.YHQID,a.JSRQ");
                    }
                    else
                    {
                        sql.Append(" group by b.YHQID,b.YHQMC");
                        sql.Append(" order by b.YHQID");
                    }
                    cmd.CommandText = sql.ToString();
                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", serverTime.Date);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CrmCoupon coupon = new CrmCoupon();
                        couponList.Add(coupon);
                        coupon.CouponType = DbUtils.GetInt(reader, 0);
                        coupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                        if (requireValidDate)
                        {
                            coupon.ValidDate = DbUtils.GetDateTime(reader, 2);
                            coupon.Balance = DbUtils.GetDouble(reader, 3);
                        }
                        else
                        {
                            coupon.Balance = DbUtils.GetDouble(reader, 2);
                        }
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
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
            return (msg.Length == 0);
        }

        //取优惠券余额和支付限额
        public static bool GetVipCouponToPay(out string msg, out int vipId, out string vipCode, List<CrmCoupon> couponList, List<CrmCouponPayLimit> payLimitList, int condType, string condValue, string cardCodeToCheck, string verifyCode, CrmStoreInfo storeInfo, int serverBillId)
        {
            msg = string.Empty;
            vipId = 0;
            vipCode = string.Empty;
            CrmVipCard vipCard = new CrmVipCard();
            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            
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
                    string s = condValue;
                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                    if (s.Length > 30)
                    {
                        s = CrmEnCryptUtils.DesDecryptCardTrack15(s);
                        int times = CrmEnCryptUtils.ConvertDateTimeInt(serverTime);
                        if (!s.Substring(0, 5).Equals("10125"))
                        {
                            msg = "不是本商场的二维码";
                            return false;
                        }
                        if ((times - long.Parse(s.Substring(5, 10))) > 120)
                        {
                            msg = "二维码时间已超过两分钟，已失效";
                            return false;
                        }
                        condType = 0;
                        condValue = s.Substring(15, s.Length - 15);
                    }
                    sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    switch (condType)
                    {
                        case 0:
                            if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                                condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                            sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                            break;
                        case 1:
                            sql.Append("  and a.HYID = ").Append(condValue);
                            break;
                        case 2:
                            sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                            break;
                        default:
                            sql.Append("  and 1=2");
                            break;
                    }
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        if ((condType == 0) || (condType == 2))
                        {
                            //isChildCard = true;
                            reader.Close();
                            sql.Length = 0;//a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM
                            sql.Append("select a.HYID,a.HYKTYPE,c.HYK_NO,c.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM ");
                            sql.Append("from HYK_HYXX a,HYKDEF b,HYK_CHILD_JL c ");
                            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                            sql.Append("  and a.HYID = c.HYID ");
                            switch (condType)
                            {
                                case 0:
                                    sql.Append("  and c.CDNR = '").Append(condValue).Append("'");
                                    break;
                                case 2:
                                    sql.Append("  and c.HYK_NO = '").Append(condValue).Append("'");
                                    break;
                            }
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "会员卡不存在";
                                return false;
                            }
                        }
                    }
                    int status = DbUtils.GetInt(reader, 3);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "会员卡状态无效";
                        return false;
                    }
                    vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                    bool existCashAccount = DbUtils.GetBool(reader, 6);
                    if (!existCashAccount)
                    {
                        reader.Close();
                        msg = "对不起, 该卡没有开通优惠券帐户";
                        return false;
                    }
                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 7);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                    {
                        reader.Close();
                        return false;
                    }
                    vipCard.CardId = DbUtils.GetInt(reader, 0);

                    vipCard.CardCode = DbUtils.GetString(reader, 2);
                    reader.Close();

                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select CSRQ,ZYID,ZJLXID,SEX,BJ_CLD from HYK_GRXX where HYID = ").Append(vipCard.CardId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            vipCard.Birthday = DbUtils.GetDateTime(reader, 0);
                            vipCard.JobType = DbUtils.GetInt(reader, 1);
                            vipCard.IdCardType = DbUtils.GetInt(reader, 2);
                            vipCard.SexType = DbUtils.GetInt(reader, 3);
                            vipCard.BirthdayIsChinese = DbUtils.GetBool(reader, 4);
                        }
                        reader.Close();
                        DoGetVipCouponPayLimit(out msg, conn, cmd, vipCard, serverBillId, couponList, payLimitList, false);
                    }
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
            vipId = vipCard.CardId;
            vipCode = vipCard.CardCode;
            return (msg.Length == 0);
        }

        //取优惠券支付限额
        public static bool GetVipCouponPayLimit(out string msg, List<CrmCouponPayLimit> payLimitList, int vipType, int serverBillId, int[] couponTypes)
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
                    throw new MyDbException(e.Message, true);
                }
                try
                {
                    List<CrmCoupon> couponList = new List<CrmCoupon>();
                    foreach (int couponType in couponTypes)
                    {
                        CrmCoupon coupon = new CrmCoupon();
                        couponList.Add(coupon);
                        coupon.CouponType = couponType;
                    }
                    CrmVipCard vipCard = null;
                    if (vipType > 0)
                    {
                        vipCard = new CrmVipCard();
                        vipCard.CardTypeId = vipType;
                    }
                    return DoGetVipCouponPayLimit(out msg, conn, cmd, vipCard, serverBillId, couponList, payLimitList, true);
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

        //取优惠券支付限额--执行
        private static bool DoGetVipCouponPayLimit(out string msg, DbConnection conn, DbCommand cmd, CrmVipCard vipCard, int serverBillId, List<CrmCoupon> couponList, List<CrmCouponPayLimit> payLimitList, bool couponTypeIsKnown)
        {
            msg = string.Empty;

            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();

            string companyCode = string.Empty;
            string storeCode = string.Empty;
            int billType = 0;
            DateTime saleTime = DateTime.MinValue;
            double totalSaleMoney = 0;
            int shoppingVipId = 0;

            sql.Length = 0;
            sql.Append("select a.STATUS,a.SHDM,a.DJLX,a.SCSJ,a.JE,a.HYID,b.MDDM from HYK_XFJL a,MDDY b ");
            sql.Append("where a.XFJLID = ").Append(serverBillId);
            sql.Append("  and a.MDID = b.MDID");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int status = DbUtils.GetInt(reader, 0);
                if (status != CrmPosData.BillStatusArticlesUploaded)
                    msg = string.Format("该销售单 {0} 在 CRM 中的状态 = {1}，不能计算优惠券限额", serverBillId, status);
                else
                {
                    companyCode = DbUtils.GetString(reader, 1);
                    billType = DbUtils.GetInt(reader, 2);
                    saleTime = DbUtils.GetDateTime(reader, 3);
                    totalSaleMoney = DbUtils.GetDouble(reader, 4);
                    shoppingVipId = DbUtils.GetInt(reader, 5);
                    storeCode = DbUtils.GetString(reader, 6);
                }
            }
            else
            {
                reader.Close();
                sql.Length = 0;
                sql.Append("select DJLX from HYXFJL ");
                sql.Append("where XFJLID = ").Append(serverBillId);
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能再计算优惠券限额", serverBillId);
                else
                    msg = string.Format("该销售单 {0} 在 CRM 中不存在,不能计算优惠券限额", serverBillId);
            }
            reader.Close();

            if (msg.Length > 0)
                return false;

            bool existPayCentRule = false;
            double exchangeCent = 0;
            double exchangeMoney = 0;
            if ((shoppingVipId > 0) && (shoppingVipId == vipCard.CardId))
            {
                sql.Length = 0;
                sql.Append("select DHJE,DHJF from HYK_JF_DHBL where HYKTYPE = ").Append(vipCard.CardTypeId);
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    exchangeMoney = DbUtils.GetDouble(reader, 0);
                    exchangeCent = DbUtils.GetDouble(reader, 1);
                    existPayCentRule = MathUtils.DoubleAGreaterThanDoubleB(exchangeCent, 0) && MathUtils.DoubleAGreaterThanDoubleB(exchangeMoney, 0);
                }
                reader.Close();
            }

            string couponTypeStr = null;

            if (couponTypeIsKnown)
            {
                foreach (CrmCoupon coupon in couponList)
                {
                    if (couponTypeStr == null)
                        couponTypeStr = coupon.CouponType.ToString();
                    else
                        couponTypeStr = couponTypeStr + ',' + coupon.CouponType.ToString();
                }
                if (couponTypeStr == null)
                    return true;
                sql.Length = 0;
                sql.Append("select b.YHQID,b.YHQMC,b.BJ_FQ,b.BJ_TS,c.BJ_SYD from YHQDEF b,YHQSYSH c ");
                sql.Append("where b.YHQID = c.YHQID ");
                sql.Append("  and c.SHDM = '").Append(companyCode).Append("'");
                sql.Append("  and b.YHQID in (").Append(couponTypeStr).Append(")");
                sql.Append("  and exists(select * from SHZFFS d where d.SHDM = '").Append(companyCode).Append("' and b.YHQID = d.YHQID) ");
                sql.Append(" order by b.YHQID ");
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int couponType = DbUtils.GetInt(reader, 0);
                    foreach (CrmCoupon coupon in couponList)
                    {
                        if (coupon.CouponType == couponType)
                        {
                            coupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                            coupon.CanOfferCoupon = DbUtils.GetBool(reader, 2);
                            coupon.SpecialType = DbUtils.GetInt(reader, 3);
                            coupon.MustCalcLimit = DbUtils.GetBool(reader, 4);
                            break;
                        }
                    }
                }
                reader.Close();
            }
            else
            {
                sql.Length = 0;
                sql.Append("select b.YHQID,b.YHQMC,b.BJ_FQ,c.BJ_SYD,sum(a.JE) as JE from HYK_YHQZH a,YHQDEF b,YHQSYSH c ");
                sql.Append("where a.YHQID = b.YHQID ");
                sql.Append("  and a.YHQID = c.YHQID ");
                sql.Append("  and a.HYID = ").Append(vipCard.CardId.ToString());
                if (billType == CrmPosData.BillTypeSale)
                    sql.Append("  and a.JE > 0 ");
                DbUtils.SpellSqlParameter(conn, sql," and ", "JSRQ", ">=");
                sql.Append("  and ((b.FS_YQMDFW = 1 and a.MDFWDM = ' ')");
                sql.Append("   or (b.FS_YQMDFW = 2 and a.MDFWDM = '").Append(companyCode).Append("')");
                sql.Append("   or (b.FS_YQMDFW = 3 and a.MDFWDM = '").Append(storeCode).Append("'))");
                sql.Append("  and b.BJ_TY = 0");
                sql.Append("  and b.BJ_TS = 0 ");
                sql.Append("  and b.BJ_DZYHQ = 1 ");
                sql.Append("  and c.SHDM = '").Append(companyCode).Append("'");
                sql.Append("  and exists(select * from SHZFFS d where d.SHDM = '").Append(companyCode).Append("' and a.YHQID = d.YHQID) ");
                sql.Append(" group by b.YHQID,b.YHQMC,b.BJ_FQ,c.BJ_SYD ");
                sql.Append(" order by b.YHQID ");
                cmd.CommandText = sql.ToString();
                DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", saleTime.Date);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CrmCoupon coupon = new CrmCoupon();
                    couponList.Add(coupon);
                    coupon.CouponType = DbUtils.GetInt(reader, 0);
                    coupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                    coupon.CanOfferCoupon = DbUtils.GetBool(reader, 2);
                    coupon.MustCalcLimit = DbUtils.GetBool(reader, 3);
                    coupon.Balance = DbUtils.GetDouble(reader, 4);
                }
                reader.Close();
                cmd.Parameters.Clear();
                if (existPayCentRule)
                {
                    double validCent = 0;
                    int multiple = 0;
                    sql.Length = 0;
                    sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(shoppingVipId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        validCent = DbUtils.GetDouble(reader, 0);
                    }
                    reader.Close();
                    if (MathUtils.DoubleAGreaterThanDoubleB(validCent, 0))
                        multiple = MathUtils.Truncate(validCent / exchangeCent);

                    if (multiple > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select b.YHQID,b.YHQMC,b.BJ_FQ,b.BJ_TS,c.BJ_SYD from YHQDEF b,YHQSYSH c ");
                        sql.Append("where b.YHQID = c.YHQID ");
                        sql.Append("  and c.SHDM = '").Append(companyCode).Append("'");
                        sql.Append("  and b.BJ_TY = 0");
                        sql.Append("  and b.BJ_TS = 5");    //BJ_TS = 5,积分抵现的优惠券
                        sql.Append("  and exists(select * from SHZFFS d where d.SHDM = '").Append(companyCode).Append("' and b.YHQID = d.YHQID) ");
                        sql.Append(" order by b.YHQID ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            CrmCoupon coupon = new CrmCoupon();
                            couponList.Add(coupon);
                            coupon.CouponType = DbUtils.GetInt(reader, 0);
                            coupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                            coupon.CanOfferCoupon = DbUtils.GetBool(reader, 2);
                            coupon.SpecialType = DbUtils.GetInt(reader, 3);
                            coupon.MustCalcLimit = DbUtils.GetBool(reader, 4);
                            coupon.Balance = exchangeMoney * multiple;
                            coupon.ExchangeCent = exchangeCent;
                            coupon.ExchangeMoney = exchangeMoney;
                        }
                        reader.Close();
                    }
                }
            }
            couponTypeStr = null;
            foreach (CrmCoupon coupon in couponList)
            {
                if (coupon.MustCalcLimit)
                {
                    coupon.IsCalcedLimit = false;
                    if (couponTypeStr == null)
                        couponTypeStr = coupon.CouponType.ToString();
                    else
                        couponTypeStr = couponTypeStr + ',' + coupon.CouponType.ToString();
                }
                else
                {
                    coupon.IsCalcedLimit = true;
                    CrmCouponPayLimit payLimit = new CrmCouponPayLimit();
                    payLimitList.Add(payLimit);
                    payLimit.CouponType = coupon.CouponType;
                    if (billType == CrmPosData.BillTypeSale)
                        payLimit.LimitMoney = totalSaleMoney;
                    else
                        payLimit.LimitMoney = -totalSaleMoney;
                }
            }
            if (couponTypeStr == null)
                return true;

            sql.Length = 0;
            sql.Append("select YHQID,sum(XSJE_YQ) as XSJE_YQ from HYK_XFJL_SP_YQJC where XFJLID = ").Append(serverBillId);
            sql.Append("  and YHQID in (").Append(couponTypeStr).Append(")");
            sql.Append(" group by YHQID");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmCouponPayLimit payLimit = new CrmCouponPayLimit();
                payLimitList.Add(payLimit);
                payLimit.CouponType = DbUtils.GetInt(reader, 0);
                payLimit.LimitMoney = DbUtils.GetDouble(reader, 1);
                foreach (CrmCoupon coupon in couponList)
                {
                    if (coupon.CouponType == payLimit.CouponType)
                    {
                        coupon.IsCalcedLimit = true;
                        break;
                    }
                }
            }
            reader.Close();

            couponTypeStr = null;
            foreach (CrmCoupon coupon in couponList)
            {
                if ((coupon.MustCalcLimit) && (!coupon.IsCalcedLimit))
                {

                    if (couponTypeStr == null)
                        couponTypeStr = coupon.CouponType.ToString();
                    else
                        couponTypeStr = couponTypeStr + ',' + coupon.CouponType.ToString();
                }
            }
            if (couponTypeStr == null)
                return true;

            List<CrmArticle> articleList = new List<CrmArticle>();
            sql.Length = 0;
            sql.Append("select a.INX,a.BMDM,a.SHSPID,a.SPDM,a.XSJE,b.SHSBID,b.SHHTID,c.SPFLDM from HYK_XFJL_SP a,SHSPXX b,SHSPFL c ");
            sql.Append(" where a.SHSPID = b.SHSPID");
            sql.Append("   and b.SHSPFLID = c.SHSPFLID");
            sql.Append("   and a.XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmArticle article = new CrmArticle();
                articleList.Add(article);
                article.Inx = DbUtils.GetInt(reader, 0);
                article.DeptCode = DbUtils.GetString(reader, 1);
                article.ArticleId = DbUtils.GetInt(reader, 2);
                article.ArticleCode = DbUtils.GetString(reader, 3);
                article.SaleMoney = DbUtils.GetDouble(reader, 4);
                article.BrandId = DbUtils.GetInt(reader, 5);
                article.ContractId = DbUtils.GetInt(reader, 6);
                article.CategoryCode = DbUtils.GetString(reader, 7);
                if (billType != CrmPosData.BillTypeSale)
                    article.SaleMoney = -article.SaleMoney;
            }
            reader.Close();
            bool existPayLimit = false;
            PromRuleSearcher.LookupPromPayCouponRuleOfArticle(out existPayLimit, cmd, 0, vipCard, couponTypeStr, companyCode, saleTime, articleList);
            if (existPayLimit)
            {
                PromCalculator.CalculateCouponPayLimit(articleList);
            }
            bool isFound = false;
            foreach (CrmCoupon coupon in couponList)
            {
                if ((coupon.MustCalcLimit) && (!coupon.IsCalcedLimit))
                {
                    CrmCouponPayLimit payLimit = new CrmCouponPayLimit();
                    payLimitList.Add(payLimit);
                    payLimit.CouponType = coupon.CouponType;
                    payLimit.LimitMoney = 0;
                    foreach (CrmArticle article in articleList)
                    {
                        foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in article.CouponPayLimitCalcItemList)
                        {
                            if (calcItemOfArticle.CouponType == coupon.CouponType)
                            {
                                payLimit.LimitMoney = payLimit.LimitMoney + calcItemOfArticle.LimitMoney;
                                break;
                            }
                        }
                    }
                }
            }

            DbTransaction dbTrans = conn.BeginTransaction();
            try
            {
                cmd.Transaction = dbTrans;

                sql.Length = 0;
                sql.Append("insert into HYK_XFJL_SP_YQJC(XFJLID,INX,YHQID,SPDM,BMDM,SHSPID,BJ_FQ,CXID,YHQSYDBH,YHQSYGZID,XSJE,XSJE_YQ) ");
                sql.Append("  values(").Append(serverBillId);
                DbUtils.SpellSqlParameter(conn, sql, ",", "INX", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "YHQID", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "SPDM", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "BMDM", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "SHSPID", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "BJ_FQ", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "CXID", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "YHQSYDBH", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "YHQSYGZID", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "XSJE", "");
                DbUtils.SpellSqlParameter(conn, sql, ",", "XSJE_YQ", "");
                sql.Append(")");
                cmd.CommandText = sql.ToString();
                DbParameter paramINX = DbUtils.AddIntInputParameter(cmd, "INX");
                DbParameter paramYHQID = DbUtils.AddIntInputParameter(cmd, "YHQID");
                DbParameter paramSPDM = DbUtils.AddStrInputParameter(cmd, 13, "SPDM");
                DbParameter paramBMDM = DbUtils.AddStrInputParameter(cmd, 20, "BMDM");
                DbParameter paramSHSPID = DbUtils.AddIntInputParameter(cmd, "SHSPID");
                DbParameter paramBJ_FQ = DbUtils.AddIntInputParameter(cmd, "BJ_FQ");
                DbParameter paramCXID = DbUtils.AddIntInputParameter(cmd, "CXID");
                DbParameter paramYHQSYDBH = DbUtils.AddIntInputParameter(cmd, "YHQSYDBH");
                DbParameter paramYHQSYGZID = DbUtils.AddIntInputParameter(cmd, "YHQSYGZID");
                DbParameter paramXSJE = DbUtils.AddDoubleInputParameter(cmd, "XSJE");
                DbParameter paramXSJE_YQ = DbUtils.AddDoubleInputParameter(cmd, "XSJE_YQ");
                foreach (CrmCoupon coupon in couponList)
                {
                    if ((coupon.MustCalcLimit) && (!coupon.IsCalcedLimit))
                    {
                        paramYHQID.Value = coupon.CouponType;
                        foreach (CrmArticle article in articleList)
                        {
                            paramINX.Value = article.Inx;
                            paramSPDM.Value = article.ArticleCode;
                            paramBMDM.Value = article.DeptCode;
                            paramSHSPID.Value = article.ArticleId;
                            paramXSJE.Value = article.SaleMoney;
                            isFound = false;
                            foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in article.CouponPayLimitCalcItemList)
                            {
                                if (calcItemOfArticle.CouponType == coupon.CouponType)
                                {
                                    isFound = true;
                                    paramCXID.Value = calcItemOfArticle.PromId;
                                    paramYHQSYDBH.Value = calcItemOfArticle.RuleBillId;
                                    paramYHQSYGZID.Value = calcItemOfArticle.RuleId;
                                    paramXSJE_YQ.Value = calcItemOfArticle.LimitMoney;
                                    if (coupon.CanOfferCoupon && calcItemOfArticle.CanOfferCoupon)
                                        paramBJ_FQ.Value = 1;
                                    else
                                        paramBJ_FQ.Value = 0;
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                paramCXID.Value = -1;
                                paramYHQSYDBH.Value = -1;
                                paramYHQSYGZID.Value = -1;
                                paramXSJE_YQ.Value = 0;
                                if (coupon.CanOfferCoupon)
                                    paramBJ_FQ.Value = 1;
                                else
                                    paramBJ_FQ.Value = 0;
                            }
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                cmd.Parameters.Clear();

                dbTrans.Commit();
            }
            catch (Exception e)
            {
                dbTrans.Rollback();
                throw e;
            }
            return (msg.Length == 0);
        }

        public static bool GetVipOfferBackDifference(out string msg, out int vipId, out string vipCode, out int promId, out string promName, List<CrmCoupon> couponList, int condType, string condValue, string cardCodeToCheck, string verifyCode, CrmStoreInfo storeInfo,string posId, int billId)
        {
            msg = string.Empty;
            vipId = 0;
            vipCode = string.Empty;
            promId = 0;
            promName = string.Empty;
            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    DbDataReader reader = null;
                    int serverBillId = 0;
                    //CrmServerPlatform.WriteErrorLog("\r\n posId=" + posId + ",billId=" + billId);
                    if (billId == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM ");
                        sql.Append("from HYK_HYXX a,HYKDEF b ");
                        sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                        switch (condType)
                        {
                            case 0:
                                if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                                    condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                                sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                                break;
                            case 1:
                                sql.Append("  and a.HYID = ").Append(condValue);
                                break;
                            case 2:
                                sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                                break;
                            default:
                                sql.Append("  and 1=2");
                                break;
                        }
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            reader.Close();
                            msg = "会员卡不存在";
                            return false;
                        }
                        int status = DbUtils.GetInt(reader, 3);
                        if (status < 0)
                        {
                            reader.Close();
                            msg = "会员卡状态无效";
                            return false;
                        }
                        bool existCashAccount = DbUtils.GetBool(reader, 6);
                        if (!existCashAccount)
                        {
                            reader.Close();
                            msg = "对不起, 该卡没有开通优惠券帐户";
                            return false;
                        }
                        string cardCodeFromDB = DbUtils.GetString(reader, 2);
                        bool isToCheckVerifyCode = DbUtils.GetBool(reader, 7);
                        bool isToCheckCardCode = isToCheckVerifyCode;

                        if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                        {
                            reader.Close();
                            return false;
                        }
                        vipId = DbUtils.GetInt(reader, 0);
                        //int cardTypeId = DbUtils.GetInt(reader,1);
                        vipCode = DbUtils.GetString(reader, 2);
                        reader.Close();
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select XFJLID,HYID_FQ,HYKNO_FQ from HYK_XFJL ");
                        sql.Append("where MDID = ").Append(storeInfo.StoreId);
                        sql.Append("  and SKTNO = '").Append(posId).Append("'");
                        sql.Append("  and JLBH = ").Append(billId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select XFJLID,HYID_FQ,HYKNO_FQ from HYXFJL ");
                            sql.Append("where MDID = ").Append(storeInfo.StoreId);
                            sql.Append("  and SKTNO = '").Append(posId).Append("'");
                            sql.Append("  and JLBH = ").Append(billId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "所选销售小票不存在";
                                return false;
                            }
                        }
                        serverBillId = DbUtils.GetInt(reader, 0);
                        vipId = DbUtils.GetInt(reader, 1);
                        vipCode = DbUtils.GetString(reader, 2);
                        reader.Close();
                        if (vipId < 0)
                        {
                            msg = "所选销售小票没刷返券卡";
                            return false;
                        }
                        sql.Length = 0;
                        sql.Append("select min(a.CXID) from HYTHJL_KQ a ");
                        sql.Append("where a.XFJLID = ").Append(serverBillId);
                        sql.Append("  and KQJE > KQJE_SJ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            promId = DbUtils.GetInt(reader, 0);
                        }
                        reader.Close();
                    }

                    if (promId == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select min(a.CXID) from HYK_THKQCE a,YHQDEF b,CXHDDEF c ");
                        sql.Append("where a.YHQID = b.YHQID ");
                        sql.Append("  and a.CXID = c.CXID ");
                        sql.Append("  and c.SHDM = '").Append(storeInfo.Company).Append("'");
                        sql.Append("  and a.HYID = ").Append(vipId);
                        sql.Append("  and a.KQCE > 0 ");
                        sql.Append("  and ((b.FS_YQMDFW = 1 and a.MDFWDM = ' ')");
                        sql.Append("   or (b.FS_YQMDFW = 2 and a.MDFWDM = '").Append(storeInfo.Company).Append("')");
                        sql.Append("   or (b.FS_YQMDFW = 3 and a.MDFWDM = '").Append(storeInfo.StoreCode).Append("'))");
                        sql.Append("  and b.BJ_TY = 0");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            promId = DbUtils.GetInt(reader, 0);
                        }
                        reader.Close();
                    }
                    if (promId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.YHQID,a.MDFWDM,b.YHQMC,c.YHQSYJSRQ,sum(a.KQCE) as KQCE from HYK_THKQCE a,YHQDEF b,YHQDEF_CXHD c ");
                        sql.Append("where a.YHQID = b.YHQID ");
                        sql.Append("  and a.YHQID = c.YHQID ");
                        sql.Append("  and a.CXID = c.CXID ");
                        sql.Append("  and a.CXID = ").Append(promId);
                        sql.Append("  and a.HYID = ").Append(vipId);
                        sql.Append("  and ((b.FS_YQMDFW = 1 and a.MDFWDM = ' ')");
                        sql.Append("   or (b.FS_YQMDFW = 2 and a.MDFWDM = '").Append(storeInfo.Company).Append("')");
                        sql.Append("   or (b.FS_YQMDFW = 3 and a.MDFWDM = '").Append(storeInfo.StoreCode).Append("'))");
                        sql.Append("  and b.BJ_TY = 0 ");
                        sql.Append("  and a.KQCE > 0 ");
                        sql.Append(" group by a.YHQID,a.MDFWDM,b.YHQMC,c.YHQSYJSRQ ");
                        sql.Append(" order by a.YHQID,c.YHQSYJSRQ,a.MDFWDM ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmCoupon coupon = new CrmCoupon();
                            couponList.Add(coupon);
                            coupon.CouponType = DbUtils.GetInt(reader, 0);
                            coupon.PayStoreScope = DbUtils.GetString(reader, 1);
                            coupon.CouponTypeName = DbUtils.GetString(reader, 2, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                            coupon.ValidDate = DbUtils.GetDateTime(reader, 3);  //如果某促销活动某券的结束日期不固定，这个该是啥值？
                            coupon.OfferBackDifference = DbUtils.GetDouble(reader, 4);
                        }
                        reader.Close();

                        //DbParameter param = DbUtils.AddDatetimeInputParameter(cmd, "JSRQ");
                        foreach (CrmCoupon coupon in couponList)
                        {
                            //param.Value = coupon.ValidDate;
                            sql.Length = 0;
                            sql.Append("select sum(JE) from HYK_YHQZH ");
                            sql.Append(" where HYID = ").Append(vipId);
                            sql.Append("   and YHQID = ").Append(coupon.CouponType);
                            sql.Append("   and CXID = ").Append(promId);
                            //DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", ">=");
                            sql.Append("   and MDFWDM = '").Append(coupon.PayStoreScope).Append("'");
                            sql.Append("   and JE > 0 ");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                coupon.Balance = DbUtils.GetDouble(reader, 0);
                            }
                            reader.Close();
                        }
                        //cmd.Parameters.Clear();

                        sql.Length = 0;
                        sql.Append("select CXZT from CXHDDEF ");
                        sql.Append(" where CXID = ").Append(promId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            promName = DbUtils.GetString(reader, 0,CrmServerPlatform.Config.DbCharSetIsNotChinese, 40);
                        }
                        reader.Close();
                    }
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
            return (msg.Length == 0);
        }

        //准备储值卡支付交易
        public static bool PrepareTransCashCardPayment(out string msg, out int transId, CrmRSaleBillHead billHead, List<CrmCashCardPayment> paymentList)
        {
            msg = string.Empty;
            transId = 0;
            if ((billHead.ServerBillId <= 0) && (billHead.BillId <= 0))
                return false;

            if ((billHead.BillId > 0) && (billHead.StoreId == 0))
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            double totalMoney = 0;
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
                    string dbSysName = DbUtils.GetDbSystemName(conn);
                    if (billHead.ServerBillId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.JZRQ,a.SKYDM from HYK_XFJL a ");
                        sql.Append("where XFJLID = ").Append(billHead.ServerBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            if (status == 1)
                                msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备储值卡交易", billHead.ServerBillId);
                            else
                            {
                                billHead.CompanyCode = DbUtils.GetString(reader, 1);
                                billHead.StoreId = DbUtils.GetInt(reader, 2);
                                billHead.PosId = DbUtils.GetString(reader, 3);
                                billHead.BillId = DbUtils.GetInt(reader, 4);
                                billHead.BillType = DbUtils.GetInt(reader, 5);
                                billHead.AccountDate = DbUtils.GetDateTime(reader, 6);
                                billHead.Cashier = DbUtils.GetString(reader, 7);
                            }
                        }
                        else
                        {
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select SHDM,MDID,SKTNO,JLBH,JZRQ,SKYDM from HYXFJL ");
                            sql.Append("where XFJLID = ").Append(billHead.ServerBillId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                                msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备储值卡交易", billHead.ServerBillId);
                            else
                                msg = string.Format("该销售单 {0} 在 CRM 中不存在,不能准备储值卡交易", billHead.ServerBillId);
                        }
                        reader.Close();

                        if (msg.Length == 0)
                        {
                            sql.Length = 0;
                            sql.Append("select JYZT from HYK_JYCL ");
                            sql.Append("where JYLX = ").Append(CrmPosData.TransTypePayment);
                            sql.Append("  and XFJLID = ").Append(billHead.ServerBillId);
                            sql.Append("  and BJ_CZK = 1");
                            sql.Append("  and JYZT in (1,2)");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                int status = DbUtils.GetInt(reader, 0);
                                if ((status == 1) || (status == 2))
                                    msg = string.Format("该销售单 {0} 在 CRM 中已有未完成的储值卡交易, 请先冲正", billHead.ServerBillId);
                            }
                            reader.Close();
                        }
                        if (msg.Length == 0)
                        {
                            totalMoney = 0;

                            for (int i = 0; i < paymentList.Count(); i++)
                            {
                                CrmCashCardPayment payment = paymentList[i];
                                if (billHead.BillType == CrmPosData.BillTypeSale)
                                {
                                    if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                                    {
                                        msg = "消费时储值卡的支付金额 <= 0";
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!MathUtils.DoubleASmallerThanDoubleB(payment.PayMoney, 0))
                                    {
                                        msg = "退货时储值卡的支付金额 >= 0";
                                        break;
                                    }
                                }
                                totalMoney = totalMoney + payment.PayMoney;
                            }
                        }
                    }
                    else
                    {
                        totalMoney = 0;
                        bool isPositive = false;
                        bool isNegative = false;
                        for (int i = 0; i < paymentList.Count(); i++)
                        {
                            CrmCashCardPayment payment = paymentList[i];
                            if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                                isPositive = true;
                            else
                                isNegative = true;
                            totalMoney = totalMoney + payment.PayMoney;
                        }
                        if (isPositive && isNegative)
                            msg = "支付金额有正有负";
                        else if (isPositive)
                            billHead.BillType = CrmPosData.BillTypeSale;
                        else
                            billHead.BillType = CrmPosData.BillTypeReturn;
                    }
                    if (msg.Length == 0)
                    {
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                        transId = SeqGenerator.GetSeqHYK_JYCL("CRMDB", CrmServerPlatform.CurrentDbId);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL(JYID,JYLX,XFJLID,MDID,SKTNO,JLBH,SKYDM,BJ_CZK,JYZT,JZRQ,ZBSJ,JYJE,XFQYID)");
                            sql.Append(" values(").Append(transId);
                            sql.Append(",").Append(1);
                            sql.Append(",").Append(billHead.ServerBillId);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",'").Append(billHead.Cashier);
                            sql.Append("',1,1");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(",").Append(totalMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.AreaId);
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL_ZBZT(JYID,ZBSJ)");
                            sql.Append(" values(").Append(transId);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            double balance = 0;
                            foreach (CrmCashCardPayment payment in paymentList)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_JYCLITEM_CZK(JYID,HYID,BJ_HSK,JE)");
                                sql.Append(" values(").Append(transId);
                                sql.Append(",").Append(payment.CardId);
                                sql.Append(",0,").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if (billHead.BillType == 0)  //消费时冻结金额，退货时啥也不做
                                {
                                    sql.Length = 0;
                                    sql.Append("update HYK_JEZH set YE = YE - ").Append(payment.PayMoney.ToString("f2"));
                                    sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) + ").Append(payment.PayMoney.ToString("f2"));
                                    sql.Append(" where HYID = ").Append(payment.CardId);
                                    sql.Append("   and YE - PDJE >= ").Append(payment.PayMoney.ToString("f2"));
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                    {
                                        msg = "储值帐户余额不足";
                                        break;
                                    }

                                    EncryptBalanceOfCashCard(cmd, sql, payment.CardId, serverTime, out balance);
                                }
                            }
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //确认储值卡支付交易
        public static bool ConfirmTransCashCardPayment(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int shopId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_CZK,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("确认储值卡交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypePayment)
                            msg = string.Format("确认储值卡交易({0})不是支付交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("确认交易({0})没有储值卡", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("确认储值卡交易({0})的金额不匹配", transId);
                        else
                        {
                            transStatus = DbUtils.GetInt(reader, 4);
                            if (transStatus == 1)
                            {
                                shopId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                            }
                            else if (transStatus != 2)
                            {
                                msg = string.Format("确认储值卡交易({0}) 已取消", transId);
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("确认储值卡交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && (transStatus == 1))
                    {
                        List<CrmCashCardPayment> paymentList = new List<CrmCashCardPayment>();
                        sql.Length = 0;
                        sql.Append("select HYID,JE from HYK_JYCLITEM_CZK ");
                        sql.Append("where JYID = ").Append(transId);
                        sql.Append("  and JE <> 0 ");
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

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("update HYK_JYCL set JYZT = 2");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "TJSJ", "=");
                            sql.Append(" where JYID = ").Append(transId);
                            sql.Append("   and JYZT = 1");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "TJSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                double balance = 0;
                                int seqInx = 0;
                                foreach (CrmCashCardPayment payment in paymentList)
                                {
                                    bool isPositive = MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0);
                                    UpdateCashCardAccount(out msg, cmd, sql, 7,
                                                            isPositive,
                                                            payment.CardId,
                                                            payment.PayMoney,
                                                            out balance,
                                                            seqBegin + seqInx++,
                                                            transId,
                                                            shopId,
                                                            posId,
                                                            billId,
                                                            serverTime,
                                                            accountDate,
                                                            cashier,
                                                            isPositive ? "前台消费" : "前台退货");
                                    if (msg.Length > 0)
                                        break;

                                    int newStatus = int.MinValue;
                                    sql.Length = 0;
                                    sql.Append("select STATUS from HYK_HYXX where HYID = ").Append(payment.CardId);
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        int status = DbUtils.GetInt(reader, 0);
                                        if (MathUtils.DoubleAEuqalToDoubleB(balance, 0) && (status != 2))
                                            newStatus = 2;
                                        else if (MathUtils.DoubleAGreaterThanDoubleB(balance, 0) && (status != 1))
                                            newStatus = 1;
                                    }
                                    reader.Close();
                                    if (newStatus != int.MinValue)
                                    {
                                        sql.Length = 0;
                                        sql.Append("update HYK_HYXX set STATUS = ").Append(newStatus);
                                        DbUtils.SpellSqlParameter(conn, sql, ",", "ZHXFRQ", "=");
                                        sql.Append(" where HYID = ").Append(payment.CardId);
                                        cmd.CommandText = sql.ToString();
                                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZHXFRQ", serverTime);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                    }
                                }
                            }
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //取消储值卡支付交易
        public static bool CancelTransCashCardPayment(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int shopId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_CZK,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("取消储值卡交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypePayment)
                            msg = string.Format("取消储值卡交易({0})不是支付交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("取消交易({0})没有储值卡", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("取消储值卡交易({0})的金额不匹配", transId);
                        else
                        {
                            transStatus = DbUtils.GetInt(reader, 4);
                            if ((transStatus == 1) || (transStatus == 2))   //冻结状态或已确认状态
                            {
                                if (serverBillId <= 0)
                                    serverBillId = DbUtils.GetInt(reader, 0);
                                shopId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                            }
                        }
                    }
                    else
                    {
                        //msg = string.Format("确认储值卡交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && ((transStatus == 1) || (transStatus == 2)))
                    {
                        if (serverBillId >= 0)
                        {
                            //判断已结账
                        }
                        List<CrmCashCardPayment> paymentList = new List<CrmCashCardPayment>();
                        sql.Length = 0;
                        sql.Append("select HYID,JE from HYK_JYCLITEM_CZK ");
                        sql.Append("where JYID = ").Append(transId);
                        if (transStatus == 1)
                            sql.Append("  and JE > 0 ");
                        else
                            sql.Append("  and JE <> 0 ");
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

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;

                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            if (transStatus == 1)
                            {
                                //冻结状态-->回滚状态
                                sql.Append("update HYK_JYCL set JYZT = 3 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 1");
                            }
                            else
                            {
                                //确认状态-->冲正状态
                                sql.Append("update HYK_JYCL set JYZT = 4 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 2");
                            }
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                double balance = 0;
                                if (transStatus == 1)
                                {
                                    //冻结状态
                                    foreach (CrmCashCardPayment payment in paymentList)
                                    {
                                        if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_JEZH set YE = YE + ").Append(payment.PayMoney.ToString("f2"));
                                            sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) - ").Append(payment.PayMoney.ToString("f2"));
                                            sql.Append(" where HYID = ").Append(payment.CardId);
                                            sql.Append("   and ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) >= ").Append(payment.PayMoney.ToString("f2"));
                                            cmd.CommandText = sql.ToString();
                                            if (cmd.ExecuteNonQuery() == 0)
                                            {
                                                msg = "储值帐户冻结金额不足";
                                                break;
                                            }
                                            EncryptBalanceOfCashCard(cmd, sql, payment.CardId, serverTime, out balance);
                                        }
                                    }
                                }
                                else
                                {
                                    //确认状态
                                    int seqInx = 0;
                                    foreach (CrmCashCardPayment payment in paymentList)
                                    {
                                        UpdateCashCardAccount(out msg, cmd, sql, 7,
                                                                false,
                                                                payment.CardId,
                                                                -payment.PayMoney,
                                                                out balance,
                                                                seqBegin + seqInx++,
                                                                transId,
                                                                shopId,
                                                                posId,
                                                                billId,
                                                                serverTime,
                                                                accountDate,
                                                                cashier,
                                                                MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0) ? "前台消费冲正" : "前台退货冲正");
                                        if (msg.Length > 0)
                                            break;
                                    }
                                }
                            }
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //准备券支付交易
        public static bool PrepareTransCouponPayment(out string msg, out int transId,out double payCent, CrmRSaleBillHead billHead, List<CrmCouponPayment> paymentList)
        {
            msg = string.Empty;
            transId = 0;
            payCent = 0;
            if ((billHead.ServerBillId <= 0) && (billHead.BillId <= 0))
                return false;

            if ((billHead.BillId > 0) && (billHead.StoreId == 0))
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            double totalMoney = 0;
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
                    string dbSysName = DbUtils.GetDbSystemName(conn);
                    if (billHead.ServerBillId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.JZRQ,a.SKYDM,a.HYID,a.VIPTYPE,b.MDDM from HYK_XFJL a,MDDY b ");
                        sql.Append("where XFJLID = ").Append(billHead.ServerBillId);
                        sql.Append("  and a.MDID = b.MDID");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            if (status == 1)
                                msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备优惠券交易", billHead.ServerBillId);
                            else
                            {
                                billHead.CompanyCode = DbUtils.GetString(reader, 1);
                                billHead.StoreId = DbUtils.GetInt(reader, 2);
                                billHead.PosId = DbUtils.GetString(reader, 3);
                                billHead.BillId = DbUtils.GetInt(reader, 4);
                                billHead.BillType = DbUtils.GetInt(reader, 5);
                                billHead.AccountDate = DbUtils.GetDateTime(reader, 6);
                                billHead.Cashier = DbUtils.GetString(reader, 7);
                                billHead.VipId = DbUtils.GetInt(reader, 8);
                                billHead.VipType = DbUtils.GetInt(reader, 9);
                                billHead.StoreCode = DbUtils.GetString(reader, 10);
                            }
                        }
                        else
                        {
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select SHDM,MDID,SKTNO,JLBH,JZRQ,SKYDM from HYXFJL ");
                            sql.Append("where XFJLID = ").Append(billHead.ServerBillId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                                msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备优惠券交易", billHead.ServerBillId);
                            else
                                msg = string.Format("该销售单 {0} 在 CRM 中不存在,不能准备优惠券交易", billHead.ServerBillId);
                        }
                        reader.Close();
                    }
                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select JYZT from HYK_JYCL ");
                        sql.Append("where JYLX = ").Append(CrmPosData.TransTypePayment);
                        sql.Append("  and XFJLID = ").Append(billHead.ServerBillId);
                        sql.Append("  and BJ_YHQ = 1");
                        sql.Append("  and JYZT in (1,2)");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            msg = string.Format("该销售单 {0} 在 CRM 中已有未完成的优惠券交易, 请先冲正", billHead.ServerBillId);
                        }
                        reader.Close();
                    }
                    if (msg.Length == 0)
                    {
                        totalMoney = 0;
                        for (int i = 0; i < paymentList.Count(); i++)
                        {
                            CrmCouponPayment payment = paymentList[i];
                            if (billHead.BillType == 0)
                            {
                                if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                                {
                                    msg = string.Format("消费时优惠券 {0} 的金额 <= 0", payment.CouponType);
                                    break;
                                }
                            }
                            else
                            {
                                if (!MathUtils.DoubleASmallerThanDoubleB(payment.PayMoney, 0))
                                {
                                    msg = string.Format("退货时优惠券 {0} 的金额 >= 0", payment.CouponType);
                                    break;
                                }
                            }
                            totalMoney = totalMoney + payment.PayMoney;

                            bool isFound = false;
                            for (int j = 0; j < i; j++)
                            {
                                if (paymentList[j].CouponType == payment.CouponType)
                                {
                                    isFound = true;
                                    payment.StoreScope = paymentList[j].StoreScope;
                                    payment.ValidDaysToBack = paymentList[j].ValidDaysToBack;
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                sql.Length = 0;
                                sql.Append("select b.YXQTS,b.FS_YQMDFW,b.BJ_TS from YHQDEF b,YHQSYSH c ");
                                sql.Append("where b.YHQID = c.YHQID");
                                sql.Append("  and b.YHQID = ").Append(payment.CouponType);
                                sql.Append("  and b.BJ_TY = 0 ");
                                sql.Append("  and c.SHDM = '").Append(billHead.CompanyCode).Append("'");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    payment.ValidDaysToBack = DbUtils.GetInt(reader, 0);
                                    switch (DbUtils.GetInt(reader, 1))
                                    {
                                        case 1:
                                            payment.StoreScope = string.Empty;
                                            break;
                                        case 2:
                                            payment.StoreScope = billHead.CompanyCode;
                                            break;

                                        case 3:
                                            payment.StoreScope = billHead.StoreCode;
                                            break;

                                    }
                                    payment.SpecialType = DbUtils.GetInt(reader, 2);
                                    reader.Close();
                                }
                                else
                                {
                                    reader.Close();
                                    msg = string.Format("优惠券 {0} 不能在本商户使用或已停用", payment.CouponType);
                                    break;
                                }
                            }
                        }
                    }
                    List<CrmCouponPayment> paymentList2 = new List<CrmCouponPayment>();
                    DateTime serverTime = DateTime.MinValue;
                    if (msg.Length == 0)
                    {
                        serverTime = DbUtils.GetDbServerTime(cmd);
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", serverTime.Date);
                        if (billHead.BillType == CrmPosData.BillTypeSale)
                        {
                            foreach (CrmCouponPayment payment in paymentList)
                            {
                                if (payment.SpecialType == 5) continue; //积分抵现

                                double tmp = payment.PayMoney;
                                double payMoney = 0;
                                sql.Length = 0;
                                sql.Append("select CXID,JSRQ,JE from HYK_YHQZH ");
                                sql.Append("where YHQID = ").Append(payment.CouponType);
                                sql.Append("  and HYID = ").Append(payment.VipId);
                                DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", ">=");
                                if (payment.StoreScope.Length == 0)
                                    sql.Append("  and MDFWDM = ' '");
                                else
                                    sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                sql.Append("  and JE > 0 ");
                                sql.Append("order by JSRQ,CXID ");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    int promId = DbUtils.GetInt(reader, 0);
                                    DateTime validDate = DbUtils.GetDateTime(reader, 1);
                                    double balance = DbUtils.GetDouble(reader, 2);
                                    if (MathUtils.DoubleAGreaterThanDoubleB(balance, tmp))
                                    {
                                        payMoney = tmp;
                                        tmp = 0;
                                    }
                                    else
                                    {
                                        payMoney = balance;
                                        tmp = tmp - balance;
                                    }
                                    CrmCouponPayment payment2 = new CrmCouponPayment();
                                    paymentList2.Add(payment2);
                                    payment2.VipId = payment.VipId;
                                    payment2.CouponType = payment.CouponType;
                                    payment2.StoreScope = payment.StoreScope;
                                    payment2.PromId = promId;
                                    payment2.ValidDate = validDate;
                                    payment2.PayMoney = payMoney;
                                    if (MathUtils.DoubleAEuqalToDoubleB(tmp, 0))
                                        break;
                                }
                                reader.Close();
                                if (MathUtils.DoubleAGreaterThanDoubleB(tmp, 0))
                                {
                                    msg = "优惠券帐户余额不足";
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //退货时取相应的使用结束日期
                            foreach (CrmCouponPayment payment in paymentList)
                            {
                                if (payment.SpecialType == 5) continue; //积分抵现

                                CrmCouponPayment payment2 = new CrmCouponPayment();
                                paymentList2.Add(payment2);
                                payment2.VipId = payment.VipId;
                                payment2.CouponType = payment.CouponType;
                                payment2.StoreScope = payment.StoreScope;
                                payment2.PayMoney = payment.PayMoney;
                                payment2.PromId = 0;
                                payment2.ValidDate = serverTime.Date.AddDays(payment.ValidDaysToBack);

                                sql.Length = 0;
                                sql.Append("select CXID, JSRQ from HYK_YHQZH ");
                                sql.Append("where YHQID = ").Append(payment2.CouponType);
                                sql.Append("  and HYID = ").Append(payment2.VipId);
                                DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", ">=");
                                if (payment2.StoreScope.Length == 0)
                                    sql.Append("  and MDFWDM = ' '");
                                else
                                    sql.Append("  and MDFWDM = '").Append(payment2.StoreScope).Append("'");
                                sql.Append("  and CXID = (select max(CXID) from HYK_YHQZH ");
                                sql.Append("  where YHQID = ").Append(payment2.CouponType);
                                sql.Append("    and HYID = ").Append(payment2.VipId);
                                DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", ">=");
                                if (payment2.StoreScope.Length == 0)
                                    sql.Append("  and MDFWDM = ' '");
                                else
                                    sql.Append("  and MDFWDM = '").Append(payment2.StoreScope).Append("')");
                                sql.Append("order by JSRQ desc");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    payment2.PromId = DbUtils.GetInt(reader, 0);
                                    payment2.ValidDate = DbUtils.GetDateTime(reader, 1);
                                    reader.Close();
                                }
                                else
                                {
                                    reader.Close();

                                    sql.Length = 0;
                                    sql.Append("select CXID,YHQSYJSRQ from YHQDEF_CXHD ");
                                    sql.Append(" where YHQID = ").Append(payment2.CouponType);
                                    sql.Append("   and CXID = (select max(CXID) from YHQDEF_CXHD ");
                                    sql.Append("   where YHQID = ").Append(payment2.CouponType);
                                    DbUtils.SpellSqlParameter(conn, sql, "YHQSYJSRQ", "JSRQ", ">=", " and ");
                                    sql.Append("     and SHDM = '").Append(billHead.CompanyCode).Append("')");
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        payment2.PromId = DbUtils.GetInt(reader, 0);
                                        payment2.ValidDate = DbUtils.GetDateTime(reader, 1);
                                    }
                                    reader.Close();
                                }
                            }
                        }
                        cmd.Parameters.Clear();

                        CrmCouponPayment centPayment = null;
                        foreach (CrmCouponPayment payment in paymentList)
                        {
                            if (payment.SpecialType == 5)    //积分抵现
                            {
                                if (centPayment == null)
                                    centPayment = payment;
                                else
                                {
                                    msg = "券支付明细中只能有一条是积分抵现";
                                    break;
                                }
                            }
                        }
                        if ((msg.Length == 0) && (centPayment != null))
                        {
                            if ((billHead.ServerBillId == 0) || (billHead.VipId == 0) || (billHead.VipId != centPayment.VipId))
                                msg = "积分抵现的会员卡不是本笔消费的积分卡";
                            else
                            {
                                centPayment.PromId = 0;
                                centPayment.ValidDate = serverTime.Date;    //积分抵现，不管有效期，但sybase不能写0001/01/01

                                sql.Length = 0;
                                sql.Append("select DHJE,DHJF from HYK_JF_DHBL where HYKTYPE = ").Append(billHead.VipType);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    centPayment.ExchangeMoney = DbUtils.GetDouble(reader, 0);
                                    centPayment.ExchangeCent = DbUtils.GetDouble(reader, 1);
                                }
                                reader.Close();
                                if (MathUtils.DoubleAGreaterThanDoubleB(centPayment.ExchangeCent, 0) && MathUtils.DoubleAGreaterThanDoubleB(centPayment.ExchangeMoney, 0))
                                {
                                    centPayment.PayCent = Math.Round(centPayment.ExchangeCent * centPayment.PayMoney / centPayment.ExchangeMoney, 4);
                                    payCent = centPayment.PayCent;
                                    if (billHead.BillType == CrmPosData.BillTypeSale)
                                    {
                                        double validCent = 0;
                                        sql.Length = 0;
                                        sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(centPayment.VipId);
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            validCent = DbUtils.GetDouble(reader, 0);
                                        }
                                        reader.Close();
                                        if (MathUtils.DoubleASmallerThanDoubleB(validCent, centPayment.PayCent))
                                        {
                                            msg = string.Format("会员的积分 {0} 不够抵现 {1} 元", validCent.ToString("f4"), centPayment.PayMoney.ToString("f2"));
                                            payCent = 0;
                                        }
                                    }
                                }
                                else
                                    msg = "积分抵现的兑换比例规则没有找到";
                            }
                            if (msg.Length == 0)
                                paymentList2.Add(centPayment);
                        }
                    }
                    if (msg.Length == 0)
                    {
                        transId = SeqGenerator.GetSeqHYK_JYCL("CRMDB", CrmServerPlatform.CurrentDbId);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL(JYID,JYLX,XFJLID,MDID,SKTNO,JLBH,SKYDM,BJ_YHQ,JYZT,JZRQ,ZBSJ,JYJE)");
                            sql.Append(" values(").Append(transId);
                            sql.Append(",").Append(CrmPosData.TransTypePayment);
                            sql.Append(",").Append(billHead.ServerBillId);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",'").Append(billHead.Cashier);
                            sql.Append("',1,1");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(",").Append(totalMoney.ToString("f2"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL_ZBZT(JYID,ZBSJ)");
                            sql.Append(" values(").Append(transId);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            DbUtils.AddDatetimeInputParameter(cmd, "JSRQ");
                            foreach (CrmCouponPayment payment in paymentList2)
                            {
                                cmd.Parameters[0].Value = payment.ValidDate;
                                sql.Length = 0;
                                sql.Append("insert into HYK_JYCLITEM_YHQ(JYID,HYID,YHQID,CXID,JSRQ,MDFWDM,JE,JF,DHJF,DHJE)");
                                sql.Append(" values(").Append(transId);
                                sql.Append(",").Append(payment.VipId);
                                sql.Append(",").Append(payment.CouponType);
                                sql.Append(",").Append(payment.PromId);
                                DbUtils.SpellSqlParameter(conn, sql, ",", "JSRQ", "");
                                if (payment.StoreScope.Length == 0)
                                    sql.Append(",' '");
                                else
                                    sql.Append(",'").Append(payment.StoreScope).Append("'");
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(",").Append(payment.PayCent.ToString("f4"));
                                sql.Append(",").Append(payment.ExchangeCent.ToString("f4"));
                                sql.Append(",").Append(payment.ExchangeMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if ((billHead.BillType == CrmPosData.BillTypeSale) && (payment.SpecialType == 0))
                                {
                                    sql.Length = 0;
                                    sql.Append("update HYK_YHQZH set JE = JE - ").Append(payment.PayMoney.ToString("f2"));
                                    sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) + ").Append(payment.PayMoney.ToString("f2"));
                                    sql.Append(" where YHQID = ").Append(payment.CouponType);
                                    sql.Append("   and HYID = ").Append(payment.VipId);
                                    sql.Append("   and CXID = ").Append(payment.PromId);
                                    DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                    if (payment.StoreScope.Length == 0)
                                        sql.Append("  and MDFWDM = ' '");
                                    else
                                        sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                    sql.Append("   and JE >= ").Append(payment.PayMoney.ToString("f2"));
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                    {
                                        msg = "优惠券帐户余额不足";
                                        break;
                                    }
                                }
                            }
                            cmd.Parameters.Clear();

                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //确认券支付交易
        public static bool ConfirmTransCouponPayment(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_YHQ,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("确认优惠券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypePayment)
                            msg = string.Format("确认优惠券交易({0})不是支付交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("确认交易({0})没有优惠券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("确认优惠券交易({0})的金额不匹配", transId);
                        else
                        {
                            transStatus = DbUtils.GetInt(reader, 4);
                            if (transStatus == 1)
                            {
                                storeId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                            }
                            else if (transStatus != 2)
                            {
                                msg = string.Format("确认优惠券交易({0}) 已取消", transId);
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("确认优惠券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && (transStatus == 1))
                    {
                        List<CrmCouponPayment> paymentList = new List<CrmCouponPayment>();
                        List<int> storeIds = null;
                        List<double> storeCents = null;
                        CrmCouponPayment centPayment = null;
                        sql.Length = 0;
                        sql.Append("select HYID,YHQID,CXID,JSRQ,MDFWDM,JE,JF from HYK_JYCLITEM_YHQ ");
                        sql.Append("where JYID = ").Append(transId);
                        sql.Append("  and JE <> 0 ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmCouponPayment payment = new CrmCouponPayment();
                            payment.VipId = DbUtils.GetInt(reader, 0);
                            payment.CouponType = DbUtils.GetInt(reader, 1);
                            payment.PromId = DbUtils.GetInt(reader, 2);
                            payment.ValidDate = DbUtils.GetDateTime(reader, 3);
                            payment.StoreScope = DbUtils.GetString(reader, 4).Trim();
                            payment.PayMoney = DbUtils.GetDouble(reader, 5);
                            payment.PayCent = DbUtils.GetDouble(reader, 6);
                            if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayCent, 0))
                                centPayment = payment;
                            else
                                paymentList.Add(payment);
                        }
                        reader.Close();

                        if (centPayment != null)
                        {
                            double cent = 0;
                            sql.Length = 0;
                            sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(centPayment.VipId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                cent = DbUtils.GetDouble(reader, 0);
                            }
                            reader.Close();
                            if (MathUtils.DoubleASmallerThanDoubleB(cent, centPayment.PayCent))
                            {
                                msg = "会员的积分不够";
                                return false;
                            }
                            double temp = centPayment.PayCent;
                            cent = 0;
                            storeIds = new List<int>();
                            storeCents = new List<double>();
                            sql.Length = 0;
                            sql.Append("select WCLJF from HYK_MDJF where HYID = ").Append(centPayment.VipId).Append(" and MDID = ").Append(storeId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                cent = DbUtils.GetDouble(reader, 0);
                            }
                            reader.Close();
                            if (MathUtils.DoubleAGreaterThanDoubleB(cent, 0))
                            {
                                storeIds.Add(storeId);
                                if (MathUtils.DoubleAGreaterThanDoubleB(cent, temp))
                                {
                                    storeCents.Add(temp);
                                    temp = 0;
                                }
                                else
                                {
                                    storeCents.Add(cent);
                                    temp = temp - cent;
                                }
                            }
                            if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                            {
                                sql.Length = 0;
                                sql.Append("select MDID,WCLJF from HYK_MDJF where HYID = ").Append(centPayment.VipId).Append(" and MDID <> ").Append(storeId).Append(" and WCLJF > 0 order by WCLJF desc ");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    storeIds.Add(DbUtils.GetInt(reader, 0));
                                    cent = DbUtils.GetDouble(reader, 1);
                                    if (MathUtils.DoubleAGreaterThanDoubleB(cent, temp))
                                    {
                                        storeCents.Add(temp);
                                        temp = 0;
                                    }
                                    else
                                    {
                                        storeCents.Add(cent);
                                        temp = temp - cent;
                                    }
                                    if (MathUtils.DoubleAEuqalToDoubleB(temp, 0))
                                        break;
                                }
                                reader.Close();
                            }
                            if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                            {
                                msg = "会员的门店积分不够";
                                return false;
                            }
                        }

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        int seqQTJFDHJL = 0;
                        int seqJFBDJLMX = 0;
                        if (centPayment != null)
                        {
                            seqJFBDJLMX = SeqGenerator.GetSeqHYK_JFBDJLMX("CRMDB", CrmServerPlatform.CurrentDbId);
                            seqQTJFDHJL = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_QTJFDHJL");
                        }

                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("update HYK_JYCL set JYZT = 2 ");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "TJSJ", "=");
                            sql.Append(" where JYID = ").Append(transId);
                            sql.Append("   and JYZT = 1");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "TJSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                int seqInx = 0;
                                foreach (CrmCouponPayment payment in paymentList)
                                {
                                    bool isPositive = MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0);
                                    UpdateVipCouponAccount(out msg, cmd, sql, 7,
                                                            false,
                                                            isPositive,
                                                            payment.VipId,
                                                            payment.CouponType,
                                                            payment.PromId,
                                                            payment.ValidDate,
                                                            payment.StoreScope,
                                                            payment.PayMoney,
                                                            out payment.Balance,
                                                            seqBegin + seqInx++,
                                                            transId,
                                                            storeId,
                                                            posId,
                                                            billId,
                                                            serverTime,
                                                            accountDate,
                                                            cashier,
                                                            isPositive ? "前台消费" : "前台退货");
                                    if (msg.Length > 0)
                                        break;
                                }
                                if ((centPayment != null) && (msg.Length == 0))
                                {
                                    //积分抵现
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_QTJFDHJL(JLBH,HYID,MDID,SKTNO,XPH,DHJE,YHQID,WCLJFBD,JYID,CLSJ,ZY)");
                                    sql.Append("  values(").Append(seqQTJFDHJL);
                                    sql.Append(",").Append(centPayment.VipId);
                                    sql.Append(",").Append(storeId);
                                    sql.Append(",'").Append(posId);
                                    sql.Append("',").Append(billId);
                                    sql.Append(",").Append(centPayment.PayMoney.ToString("f2"));
                                    sql.Append(",").Append(centPayment.CouponType);
                                    sql.Append(",").Append(centPayment.PayCent.ToString("f4"));
                                    sql.Append(",").Append(transId);
                                    DbUtils.SpellSqlParameter(conn, sql, ",", "CLSJ", "");
                                    DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                    DbUtils.AddStrInputParameterAndValue(cmd, 50, "ZY", "积分抵现", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                    sql.Length = 0;
                                    sql.Append("update HYK_HYXX set ");
                                    DbUtils.SpellSqlParameter(conn, sql, "", "ZHXFRQ", "=");
                                    sql.Append(",STATUS = (case STATUS when 0 then 1 else STATUS end)");
                                    sql.Append(" where HYID = ").Append(centPayment.VipId);
                                    cmd.CommandText = sql.ToString();
                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZHXFRQ", serverTime);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();

                                    if (MathUtils.DoubleAGreaterThanDoubleB(centPayment.PayCent, 0))
                                    {
                                        sql.Length = 0;
                                        sql.Append("update HYK_JFZH set WCLJF = WCLJF - ").Append(centPayment.PayCent.ToString("f4"));
                                        sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                        sql.Append("   and WCLJF >= ").Append(centPayment.PayCent.ToString("f4"));
                                        cmd.CommandText = sql.ToString();
                                        if (cmd.ExecuteNonQuery() == 0)
                                            msg = "会员的积分不够";
                                        else
                                        {
                                            for (int i = 0; i < storeIds.Count; i++)
                                            {
                                                sql.Length = 0;
                                                sql.Append("update HYK_MDJF set WCLJF = WCLJF - ").Append(storeCents[i].ToString("f4"));
                                                sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                                sql.Append("   and MDID = ").Append(storeIds[i]);
                                                sql.Append("   and WCLJF >= ").Append(storeCents[i].ToString("f4"));
                                                cmd.CommandText = sql.ToString();
                                                if (cmd.ExecuteNonQuery() == 0)
                                                {
                                                    msg = "会员的门店积分不够";
                                                    break;
                                                }
                                                sql.Length = 0;
                                                sql.Append("insert into HYK_QTJFDHJL_ITEM(JLBH,MDID,DHJF) ");
                                                sql.Append("  values(").Append(seqQTJFDHJL);
                                                sql.Append(",").Append(storeIds[i]);
                                                sql.Append(",").Append(storeCents[i].ToString("f4"));
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        sql.Length = 0;
                                        sql.Append("update HYK_JFZH set WCLJF = WCLJF + ").Append((-centPayment.PayCent).ToString("f4"));
                                        sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                        cmd.CommandText = sql.ToString();
                                        if (cmd.ExecuteNonQuery() == 0)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_JFZH(HYID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                            sql.Append("  values(").Append(centPayment.VipId);
                                            sql.Append(",").Append((-centPayment.PayCent).ToString("f4"));
                                            sql.Append(",0,0,0,0,0,0,0,0,0");
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                        sql.Length = 0;
                                        sql.Append("update HYK_MDJF set WCLJF = WCLJF + ").Append((-centPayment.PayCent).ToString("f4"));
                                        sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                        sql.Append("   and MDID = ").Append(storeId);
                                        cmd.CommandText = sql.ToString();
                                        if (cmd.ExecuteNonQuery() == 0)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_MDJF(HYID,MDID,XFJE,LJXFJE,LJZKJE,ZKJE,WCLJF,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                            sql.Append("  values(").Append(centPayment.VipId);
                                            sql.Append(",").Append(storeId);
                                            sql.Append(",").Append((-centPayment.PayCent).ToString("f4"));
                                            sql.Append(",0,0,0,0,0,0,0,0,0");
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_QTJFDHJL_ITEM(JLBH,MDID,DHJF) ");
                                        sql.Append("  values(").Append(seqQTJFDHJL);
                                        sql.Append(",").Append(storeId);
                                        sql.Append(",").Append(centPayment.PayCent.ToString("f4"));
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (msg.Length == 0)
                                    {
                                        double validCent = 0;
                                        double storeValidCent = 0;
                                        sql.Length = 0;
                                        sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(centPayment.VipId);
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                            validCent = DbUtils.GetDouble(reader, 0);
                                        reader.Close();
                                        sql.Length = 0;
                                        sql.Append("select WCLJF from HYK_MDJF where HYID = ").Append(centPayment.VipId).Append(" and MDID = ").Append(storeId);
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                            storeValidCent = DbUtils.GetDouble(reader, 0);
                                        reader.Close();
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_JFBDJLMX(JYBH,CZMD,SKTNO,JLBH,HYID,CLLX,CLSJ,ZY,WCLJFBD,WCLJF) ");
                                        sql.Append("  values(").Append(seqJFBDJLMX);
                                        sql.Append(",").Append(storeId);
                                        sql.Append(",'").Append(posId);
                                        sql.Append("',").Append(billId);
                                        sql.Append(",").Append(centPayment.VipId);
                                        sql.Append(",").Append(43);
                                        DbUtils.SpellSqlParameter(conn, sql, ",", "CLSJ", "");
                                        DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                                        sql.Append(",").Append((-centPayment.PayCent).ToString("f4"));
                                        sql.Append(",").Append(validCent.ToString("f4"));
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                        DbUtils.AddStrInputParameterAndValue(cmd, 50, "ZY", "积分抵现", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_JFBDJLMX_MD(JYBH,MDID,WCLJFBD,WCLJF) ");
                                        sql.Append("  values(").Append(seqJFBDJLMX);
                                        sql.Append(",").Append(storeId);
                                        sql.Append(",").Append((-centPayment.PayCent).ToString("f4"));
                                        sql.Append(",").Append(storeValidCent.ToString("f4"));
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //取消券支付交易
        public static bool CancelTransCouponPayment(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_YHQ,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("取消优惠券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypePayment)
                            msg = string.Format("取消优惠券交易({0})不是支付交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("取消交易({0})没有优惠券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("取消优惠券交易({0})的金额不匹配", transId);
                        else
                        {
                            transStatus = DbUtils.GetInt(reader, 4);
                            if ((transStatus == 1) || (transStatus == 2))
                            {
                                if (serverBillId <= 0)
                                    serverBillId = DbUtils.GetInt(reader, 0);
                                storeId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                            }
                        }
                    }
                    else
                    {
                        //msg = string.Format("确认优惠券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && ((transStatus == 1) || (transStatus == 2)))
                    {
                        if (serverBillId >= 0)
                        {
                            //判断已结账
                        }
                        List<CrmCouponPayment> paymentList = new List<CrmCouponPayment>();
                        List<int> storeIds = null;
                        List<double> storeCents = null;
                        CrmCouponPayment centPayment = null;
                        sql.Length = 0;
                        sql.Append("select HYID,YHQID,CXID,JSRQ,MDFWDM,JE,JF from HYK_JYCLITEM_YHQ ");
                        sql.Append("where JYID = ").Append(transId);
                        if (transStatus == 1)
                            sql.Append("  and JE > 0 ");
                        else
                            sql.Append("  and JE <> 0 ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmCouponPayment payment = new CrmCouponPayment();
                            payment.VipId = DbUtils.GetInt(reader, 0);
                            payment.CouponType = DbUtils.GetInt(reader, 1);
                            payment.PromId = DbUtils.GetInt(reader, 2);
                            payment.ValidDate = DbUtils.GetDateTime(reader, 3);
                            payment.StoreScope = DbUtils.GetString(reader, 4).Trim();
                            payment.PayMoney = DbUtils.GetDouble(reader, 5);
                            payment.PayCent = DbUtils.GetDouble(reader, 6);
                            if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayCent, 0))
                                centPayment = payment;
                            else
                                paymentList.Add(payment);
                        }
                        reader.Close();

                        if (centPayment != null)
                        {
                            storeIds = new List<int>();
                            storeCents = new List<double>();
                            sql.Length = 0;
                            sql.Append("select b.MDID,b.DHJF from HYK_QTJFDHJL a,HYK_QTJFDHJL_ITEM b ");
                            sql.Append(" where a.JLBH = b.JLBH ");
                            sql.Append("   and a.JYID = ").Append(transId); //此时一个JYID对应一个JYBH
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                storeIds.Add(DbUtils.GetInt(reader, 0));
                                storeCents.Add(DbUtils.GetDouble(reader, 1));
                            }
                            reader.Close();
                        }

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        int seqQTJFDHJL = 0;
                        int seqJFBDJLMX = 0;
                        if (centPayment != null)
                        {
                            seqJFBDJLMX = SeqGenerator.GetSeqHYK_JFBDJLMX("CRMDB", CrmServerPlatform.CurrentDbId);
                            seqQTJFDHJL = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_QTJFDHJL");
                        }
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            if (transStatus == 1)
                            {
                                sql.Append("update HYK_JYCL set JYZT = 3 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 1");
                            }
                            else
                            {
                                sql.Append("update HYK_JYCL set JYZT = 4 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 2");
                            }
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if (transStatus == 1)
                                {
                                    //冻结状态
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
                                            msg = "优惠券帐户冻结金额不足";
                                            break;
                                        }
                                        cmd.Parameters.Clear();
                                    }
                                }
                                else
                                {
                                    //确认状态
                                    int seqInx = 0;
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        UpdateVipCouponAccount(out msg, cmd, sql, 7,
                                                                false,
                                                                false,
                                                                payment.VipId,
                                                                payment.CouponType,
                                                                payment.PromId,
                                                                payment.ValidDate,
                                                                payment.StoreScope,
                                                                -payment.PayMoney,
                                                                out payment.Balance,
                                                                seqBegin + seqInx++,
                                                                transId,
                                                                storeId,
                                                                posId,
                                                                billId,
                                                                serverTime,
                                                                accountDate,
                                                                cashier,
                                                                MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0) ? "前台消费冲正" : "前台退货冲正");
                                        if (msg.Length > 0)
                                            break;
                                    }
                                    if ((centPayment != null) && (msg.Length == 0))
                                    {
                                        //积分抵现
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_QTJFDHJL(JLBH,HYID,MDID,SKTNO,XPH,DHJE,YHQID,WCLJFBD,JYID,CLSJ,ZY)");
                                        sql.Append("  values(").Append(seqQTJFDHJL);
                                        sql.Append(",").Append(centPayment.VipId);
                                        sql.Append(",").Append(storeId);
                                        sql.Append(",'").Append(posId);
                                        sql.Append("',").Append(billId);
                                        sql.Append(",").Append((-centPayment.PayMoney).ToString("f2"));
                                        sql.Append(",").Append(centPayment.CouponType);
                                        sql.Append(",").Append((-centPayment.PayCent).ToString("f4"));
                                        sql.Append(",").Append(transId);
                                        DbUtils.SpellSqlParameter(conn, sql, ",", "CLSJ", "");
                                        DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                        DbUtils.AddStrInputParameterAndValue(cmd, 50, "ZY", "积分抵现冲正", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                        cmd.ExecuteNonQuery();
                                        cmd.Parameters.Clear();
                                        for (int i = 0; i < storeIds.Count; i++)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_QTJFDHJL_ITEM(JLBH,MDID,DHJF) ");
                                            sql.Append("  values(").Append(seqQTJFDHJL);
                                            sql.Append(",").Append(storeIds[i]);
                                            sql.Append(",").Append((-storeCents[i]).ToString("f4"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                        if (MathUtils.DoubleAGreaterThanDoubleB(centPayment.PayCent, 0))
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_JFZH set WCLJF = WCLJF + ").Append(centPayment.PayCent.ToString("f4"));
                                            sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                            cmd.CommandText = sql.ToString();
                                            if (cmd.ExecuteNonQuery() == 0)
                                            {
                                                sql.Length = 0;
                                                sql.Append("insert into HYK_JFZH(HYID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                                sql.Append("  values(").Append(centPayment.VipId);
                                                sql.Append(",").Append(centPayment.PayCent.ToString("f4"));
                                                sql.Append(",0,0,0,0,0,0,0,0,0");
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        else
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_JFZH set WCLJF = WCLJF - ").Append((-centPayment.PayCent).ToString("f4"));
                                            sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                            sql.Append("   and WCLJF >= ").Append((-centPayment.PayCent).ToString("f4"));
                                            cmd.CommandText = sql.ToString();
                                            if (cmd.ExecuteNonQuery() == 0)
                                                msg = "会员的当前积分不够冲正";
                                        }
                                        if (msg.Length == 0)
                                        {
                                            for (int i = 0; i < storeIds.Count; i++)
                                            {
                                                if (MathUtils.DoubleAGreaterThanDoubleB(storeCents[i], 0))
                                                {
                                                    sql.Length = 0;
                                                    sql.Append("update HYK_MDJF set WCLJF = WCLJF + ").Append(storeCents[i].ToString("f4"));
                                                    sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                                    sql.Append("   and MDID = ").Append(storeIds[i]);
                                                    cmd.CommandText = sql.ToString();
                                                    if (cmd.ExecuteNonQuery() == 0)
                                                    {
                                                        sql.Length = 0;
                                                        sql.Append("insert into HYK_MDJF(HYID,MDID,XFJE,LJXFJE,LJZKJE,ZKJE,WCLJF,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                                        sql.Append("  values(").Append(centPayment.VipId);
                                                        sql.Append(",").Append(storeId);
                                                        sql.Append(",").Append(storeCents[i].ToString("f4"));
                                                        sql.Append(",0,0,0,0,0,0,0,0,0");
                                                        sql.Append(")");
                                                        cmd.CommandText = sql.ToString();
                                                        cmd.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    sql.Length = 0;
                                                    sql.Append("update HYK_MDJF set WCLJF = WCLJF - ").Append((-storeCents[i]).ToString("f4"));
                                                    sql.Append(" where HYID =  ").Append(centPayment.VipId);
                                                    sql.Append("   and MDID = ").Append(storeIds[i]);
                                                    sql.Append("   and WCLJF >= ").Append((-storeCents[i]).ToString("f4"));
                                                    cmd.CommandText = sql.ToString();
                                                    if (cmd.ExecuteNonQuery() == 0)
                                                    {
                                                        msg = "会员的门店积分不够冲正";
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        if (msg.Length == 0)
                                        {
                                            double validCent = 0;
                                            sql.Length = 0;
                                            sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(centPayment.VipId);
                                            cmd.CommandText = sql.ToString();
                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                                validCent = DbUtils.GetDouble(reader, 0);
                                            reader.Close();
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_JFBDJLMX(JYBH,CZMD,SKTNO,JLBH,HYID,CLLX,CLSJ,ZY,WCLJFBD,WCLJF) ");
                                            sql.Append("  values(").Append(seqJFBDJLMX);
                                            sql.Append(",").Append(storeId);
                                            sql.Append(",'").Append(posId);
                                            sql.Append("',").Append(billId);
                                            sql.Append(",").Append(centPayment.VipId);
                                            sql.Append(",").Append(43);
                                            DbUtils.SpellSqlParameter(conn, sql, ",", "CLSJ", "");
                                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                                            sql.Append(",").Append(centPayment.PayCent.ToString("f4"));
                                            sql.Append(",").Append(validCent.ToString("f4"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                            DbUtils.AddStrInputParameterAndValue(cmd, 50, "ZY", "积分抵现冲正", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                            cmd.ExecuteNonQuery();
                                            cmd.Parameters.Clear();
                                            for (int i = 0; i < storeIds.Count; i++)
                                            {
                                                double storeValidCent = 0;
                                                sql.Length = 0;
                                                sql.Append("select WCLJF from HYK_MDJF where HYID = ").Append(centPayment.VipId).Append(" and MDID = ").Append(storeIds[i]);
                                                cmd.CommandText = sql.ToString();
                                                reader = cmd.ExecuteReader();
                                                if (reader.Read())
                                                    storeValidCent = DbUtils.GetDouble(reader, 0);
                                                reader.Close();
                                                sql.Length = 0;
                                                sql.Append("insert into HYK_JFBDJLMX_MD(JYBH,MDID,WCLJFBD,WCLJF) ");
                                                sql.Append("  values(").Append(seqJFBDJLMX);
                                                sql.Append(",").Append(storeId);
                                                sql.Append(",").Append(storeCents[i].ToString("f4"));
                                                sql.Append(",").Append(storeValidCent.ToString("f4"));
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //准备券转储交易
        public static bool PrepareTransCouponTransfer(out string msg, out int transId, int vipIdTo, CrmRSaleBillHead billHead, List<CrmCouponPayment> paymentList)
        {
            msg = string.Empty;
            transId = 0;

            if (billHead.StoreId == 0)
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }
            billHead.BillType = CrmPosData.BillTypeVoucherTransfer;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            double totalMoney = 0;
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
                    string dbSysName = DbUtils.GetDbSystemName(conn);

                    sql.Length = 0;
                    sql.Append("select STATUS,XFJLID from HYK_XFJL ");
                    sql.Append("where MDID = ").Append(billHead.StoreId);
                    sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                    sql.Append("  and JLBH = ").Append(billHead.BillId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int status = DbUtils.GetInt(reader, 0);
                        if (status == CrmPosData.BillStatusCheckedOut)
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备优惠券转储交易", billHead.BillId);
                        else
                            billHead.ServerBillId = DbUtils.GetInt(reader, 1);
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select XFJLID from HYXFJL ");
                        sql.Append("where MDID = ").Append(billHead.StoreId);
                        sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                        sql.Append("  and JLBH = ").Append(billHead.BillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备优惠券转储交易", billHead.BillId);
                    }
                    reader.Close();

                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select JYZT from HYK_JYCL ");
                        sql.Append("where JYLX = ").Append(CrmPosData.TransTypeTransfer);
                        sql.Append("  and XFJLID = ").Append(billHead.ServerBillId);
                        sql.Append("  and BJ_YHQ = 1");
                        sql.Append("  and JYZT in (1,2)");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            msg = string.Format("该销售单 {0} 在 CRM 中已有未完成的优惠券交易, 请先冲正", billHead.ServerBillId);
                        }
                        reader.Close();
                    }
                    if (msg.Length == 0)
                    {
                        totalMoney = 0;
                        for (int i = 0; i < paymentList.Count(); i++)
                        {
                            CrmCouponPayment payment = paymentList[i];

                            if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                            {
                                msg = string.Format("转储时优惠券 {0} 的金额 <= 0", payment.CouponType);
                                break;
                            }

                            totalMoney = totalMoney + payment.PayMoney;

                            bool isFound = false;
                            for (int j = 0; j < i; j++)
                            {
                                if (paymentList[j].CouponType == payment.CouponType)
                                {
                                    isFound = true;
                                    payment.StoreScope = paymentList[j].StoreScope;
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                sql.Length = 0;
                                sql.Append("select b.FS_YQMDFW from YHQDEF b,YHQSYSH c ");
                                sql.Append("where b.YHQID = c.YHQID");
                                sql.Append("  and b.YHQID = ").Append(payment.CouponType);
                                sql.Append("  and b.BJ_TY = 0 ");
                                sql.Append("  and c.SHDM = '").Append(billHead.CompanyCode).Append("'");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    switch (DbUtils.GetInt(reader, 0))
                                    {
                                        case 1:
                                            payment.StoreScope = string.Empty;
                                            break;
                                        case 2:
                                            payment.StoreScope = billHead.CompanyCode;
                                            break;

                                        case 3:
                                            payment.StoreScope = billHead.StoreCode;
                                            break;

                                    }
                                    reader.Close();
                                }
                                else
                                {
                                    reader.Close();
                                    msg = string.Format("优惠券 {0} 不能在本商户使用或已停用", payment.CouponType);
                                    break;
                                }
                            }
                        }
                    }
                    List<CrmCouponPayment> paymentList2 = new List<CrmCouponPayment>();
                    if (msg.Length == 0)
                    {
                        DbParameter paramJSRQ = DbUtils.AddDatetimeInputParameter(cmd, "JSRQ");

                        foreach (CrmCouponPayment payment in paymentList)
                        {
                            paramJSRQ.Value = payment.ValidDate;

                            double tmp = payment.PayMoney;
                            double payMoney = 0;
                            sql.Length = 0;
                            sql.Append("select CXID,JE from HYK_YHQZH ");
                            sql.Append("where YHQID = ").Append(payment.CouponType);
                            sql.Append("  and HYID = ").Append(payment.VipId);
                            DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                            if (payment.StoreScope.Length == 0)
                                sql.Append("  and MDFWDM = ' '");
                            else
                                sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                            sql.Append("  and JE > 0 ");
                            sql.Append("order by CXID ");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                int promId = DbUtils.GetInt(reader, 0);
                                double balance = DbUtils.GetDouble(reader, 1);
                                if (MathUtils.DoubleAGreaterThanDoubleB(balance, tmp))
                                {
                                    payMoney = tmp;
                                    tmp = 0;
                                }
                                else
                                {
                                    payMoney = balance;
                                    tmp = tmp - balance;
                                }
                                CrmCouponPayment payment2 = new CrmCouponPayment();
                                paymentList2.Add(payment2);
                                payment2.VipId = payment.VipId;
                                payment2.CouponType = payment.CouponType;
                                payment2.StoreScope = payment.StoreScope;
                                payment2.PromId = promId;
                                payment2.ValidDate = payment.ValidDate;
                                payment2.PayMoney = payMoney;
                                if (MathUtils.DoubleAEuqalToDoubleB(tmp, 0))
                                    break;
                            }
                            reader.Close();
                            if (MathUtils.DoubleAGreaterThanDoubleB(tmp, 0))
                            {
                                msg = "优惠券帐户余额不足";
                                break;
                            }
                        }
                        cmd.Parameters.Clear();
                    }
                    if (msg.Length == 0)
                    {
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        bool existBill = (billHead.ServerBillId > 0);
                        if (!existBill)
                            billHead.ServerBillId = SeqGenerator.GetSeqHYK_XFJL("CRMDB", CrmServerPlatform.CurrentDbId);
                        transId = SeqGenerator.GetSeqHYK_JYCL("CRMDB", CrmServerPlatform.CurrentDbId);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            if (existBill)
                            {
                                DeleteRSaleBillFromDb(cmd, sql, billHead.ServerBillId);
                            }

                            sql.Length = 0;
                            sql.Append("insert into HYK_XFJL(XFJLID, SHDM, MDID, SKTNO, JLBH, DJLX,SKYDM, JZRQ,XFSJ,SCSJ,STATUS)");
                            sql.Append(" values(").Append(billHead.ServerBillId);
                            sql.Append(",'").Append(billHead.CompanyCode);
                            sql.Append("',").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",").Append(billHead.BillType);
                            sql.Append(",'").Append(billHead.Cashier).Append("'");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "XFSJ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "SCSJ", "");
                            sql.Append(",0");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFSJ", billHead.SaleTime);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SCSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL(JYID,JYLX,XFJLID,MDID,SKTNO,JLBH,SKYDM,BJ_YHQ,JYZT,JZRQ,ZBSJ,JYJE,HYID_TO)");
                            sql.Append(" values(").Append(transId);
                            sql.Append(",").Append(CrmPosData.TransTypeTransfer);
                            sql.Append(",").Append(billHead.ServerBillId);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",'").Append(billHead.Cashier);
                            sql.Append("',1,1");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(",").Append(totalMoney.ToString("f2"));
                            sql.Append(",").Append(vipIdTo.ToString());
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL_ZBZT(JYID,ZBSJ)");
                            sql.Append(" values(").Append(transId);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            DbUtils.AddDatetimeInputParameter(cmd, "JSRQ");
                            foreach (CrmCouponPayment payment in paymentList2)
                            {
                                cmd.Parameters[0].Value = payment.ValidDate;
                                sql.Length = 0;
                                sql.Append("insert into HYK_JYCLITEM_YHQ(JYID,HYID,YHQID,CXID,JSRQ,MDFWDM,JE)");
                                sql.Append(" values(").Append(transId);
                                sql.Append(",").Append(payment.VipId);
                                sql.Append(",").Append(payment.CouponType);
                                sql.Append(",").Append(payment.PromId);
                                DbUtils.SpellSqlParameter(conn, sql, ",", "JSRQ", "");
                                if (payment.StoreScope.Length == 0)
                                    sql.Append(",' '");
                                else
                                    sql.Append(",'").Append(payment.StoreScope).Append("'");
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("update HYK_YHQZH set JE = JE - ").Append(payment.PayMoney.ToString("f2"));
                                sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) + ").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(" where YHQID = ").Append(payment.CouponType);
                                sql.Append("   and HYID = ").Append(payment.VipId);
                                sql.Append("   and CXID = ").Append(payment.PromId);
                                DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                if (payment.StoreScope.Length == 0)
                                    sql.Append("  and MDFWDM = ' '");
                                else
                                    sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                sql.Append("   and JE >= ").Append(payment.PayMoney.ToString("f2"));
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    msg = "优惠券帐户余额不足";
                                    break;
                                }
                            }
                            cmd.Parameters.Clear();

                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //确认券转储交易
        public static bool ConfirmTransCouponTransfer(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
            int vipIdTo = 0;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_YHQ,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ,HYID_TO from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("确认优惠券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypeTransfer)
                            msg = string.Format("确认优惠券交易({0})不是转储交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("确认交易({0})没有优惠券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("确认优惠券交易({0})的金额不匹配", transId);
                        else
                        {
                            if (serverBillId <= 0)
                                serverBillId = DbUtils.GetInt(reader, 0);
                            transStatus = DbUtils.GetInt(reader, 4);
                            if (transStatus == CrmPosData.TransStatusPrepared)
                            {
                                storeId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                                vipIdTo = DbUtils.GetInt(reader, 10);
                            }
                            else if (transStatus != CrmPosData.TransStatusCanceled)
                            {
                                msg = string.Format("确认优惠券交易({0}) 已取消", transId);
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("确认优惠券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && (transStatus == CrmPosData.TransStatusPrepared))
                    {
                        List<CrmCouponPayment> paymentList = new List<CrmCouponPayment>();
                        sql.Length = 0;
                        sql.Append("select HYID,YHQID,CXID,JSRQ,MDFWDM,JE from HYK_JYCLITEM_YHQ ");
                        sql.Append("where JYID = ").Append(transId);
                        sql.Append("  and JE <> 0 ");
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

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count * 2;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("update HYK_JYCL set JYZT = 2 ");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "TJSJ", "=");
                            sql.Append(" where JYID = ").Append(transId);
                            sql.Append("   and JYZT = 1");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "TJSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("update HYK_XFJL set STATUS = ").Append(CrmPosData.BillStatusCheckedOut);
                                sql.Append(" where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                int seqInx = 0;
                                foreach (CrmCouponPayment payment in paymentList)
                                {
                                    UpdateVipCouponAccount(out msg, cmd, sql, 5,
                                                            true,
                                                            true,
                                                            payment.VipId,
                                                            payment.CouponType,
                                                            payment.PromId,
                                                            payment.ValidDate,
                                                            payment.StoreScope,
                                                            payment.PayMoney,
                                                            out payment.Balance,
                                                            seqBegin + seqInx++,
                                                            transId,
                                                            storeId,
                                                            posId,
                                                            billId,
                                                            serverTime,
                                                            accountDate,
                                                            cashier,
                                                            "前台转出");
                                    if (msg.Length > 0)
                                    {
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                }
                                foreach (CrmCouponPayment payment in paymentList)
                                {
                                    UpdateVipCouponAccount(out msg, cmd, sql, 5,
                                                            false,
                                                            false,
                                                            vipIdTo,
                                                            payment.CouponType,
                                                            payment.PromId,
                                                            payment.ValidDate,
                                                            payment.StoreScope,
                                                            payment.PayMoney,
                                                            out payment.Balance,
                                                            seqBegin + seqInx++,
                                                            transId,
                                                            storeId,
                                                            posId,
                                                            billId,
                                                            serverTime,
                                                            accountDate,
                                                            cashier,
                                                            "前台转入");
                                    if (msg.Length > 0)
                                    {
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                }
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //取消券转储交易
        public static bool CancelTransCouponTransfer(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
            int vipIdTo = 0;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_YHQ,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ,HYID_TO from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("取消优惠券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypeTransfer)
                            msg = string.Format("取消优惠券交易({0})不是转储交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("取消交易({0})没有优惠券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("取消优惠券交易({0})的金额不匹配", transId);
                        else
                        {
                            if (serverBillId <= 0)
                                serverBillId = DbUtils.GetInt(reader, 0);
                            transStatus = DbUtils.GetInt(reader, 4);
                            if ((transStatus == 1) || (transStatus == 2))
                            {
                                storeId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                                vipIdTo = DbUtils.GetInt(reader, 10);
                            }
                        }
                    }
                    else
                    {
                        //msg = string.Format("确认优惠券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && ((transStatus == CrmPosData.TransStatusPrepared) || (transStatus == CrmPosData.TransStatusConfirmed)))
                    {
                        if (serverBillId >= 0)
                        {
                            //判断已结账
                        }
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

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count * 2;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            if (transStatus == CrmPosData.TransStatusPrepared)
                            {
                                sql.Append("update HYK_JYCL set JYZT = 3 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 1");
                            }
                            else
                            {
                                sql.Append("update HYK_JYCL set JYZT = 4 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 2");
                            }
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("update HYK_XFJL set STATUS = ").Append(CrmPosData.BillStatusPrepareCheckOut);
                                sql.Append(" where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if (transStatus == CrmPosData.TransStatusPrepared)
                                {
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        //冻结状态
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
                                            msg = "优惠券帐户冻结金额不足";
                                            dbTrans.Rollback();
                                            return false;
                                        }
                                        cmd.Parameters.Clear();
                                    }
                                }
                                else
                                {
                                    //确认状态
                                    int seqInx = 0;
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        UpdateVipCouponAccount(out msg, cmd, sql, 5,
                                                                true,
                                                                false,
                                                                payment.VipId,
                                                                payment.CouponType,
                                                                payment.PromId,
                                                                payment.ValidDate,
                                                                payment.StoreScope,
                                                                -payment.PayMoney,
                                                                out payment.Balance,
                                                                seqBegin + seqInx++,
                                                                transId,
                                                                storeId,
                                                                posId,
                                                                billId,
                                                                serverTime,
                                                                accountDate,
                                                                cashier,
                                                                "前台转出冲正");
                                        if (msg.Length > 0)
                                        {
                                            dbTrans.Rollback();
                                            return false;
                                        }
                                    }
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        UpdateVipCouponAccount(out msg, cmd, sql, 5,
                                                                false,
                                                                false,
                                                                vipIdTo,
                                                                payment.CouponType,
                                                                payment.PromId,
                                                                payment.ValidDate,
                                                                payment.StoreScope,
                                                                -payment.PayMoney,
                                                                out payment.Balance,
                                                                seqBegin + seqInx++,
                                                                transId,
                                                                storeId,
                                                                posId,
                                                                billId,
                                                                serverTime,
                                                                accountDate,
                                                                cashier,
                                                                "前台转入冲正");
                                        if (msg.Length > 0)
                                        {
                                            dbTrans.Rollback();
                                            return false;
                                        }
                                    }
                                }
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //准备扣返券款退还交易
        public static bool PrepareTransCoupon2Cash(out string msg, out int transId, int promId, CrmRSaleBillHead billHead, List<CrmCouponPayment> paymentList)
        {
            msg = string.Empty;
            transId = 0;

            if (billHead.StoreId == 0)
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }
            billHead.BillType = CrmPosData.BillTypeVoucher2Cash;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            double totalMoney = 0;
            List<CrmCouponPayment> paymentList2 = null;
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
                    string dbSysName = DbUtils.GetDbSystemName(conn);

                    sql.Length = 0;
                    sql.Append("select STATUS,XFJLID from HYK_XFJL ");
                    sql.Append("where MDID = ").Append(billHead.StoreId);
                    sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                    sql.Append("  and JLBH = ").Append(billHead.BillId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int status = DbUtils.GetInt(reader, 0);
                        if (status == CrmPosData.BillStatusCheckedOut)
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备扣返券款退还交易", billHead.BillId);
                        else
                            billHead.ServerBillId = DbUtils.GetInt(reader, 1);
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select XFJLID from HYXFJL ");
                        sql.Append("where MDID = ").Append(billHead.StoreId);
                        sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                        sql.Append("  and JLBH = ").Append(billHead.BillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备扣返券款退还交易", billHead.BillId);
                    }
                    reader.Close();

                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select JYZT from HYK_JYCL ");
                        sql.Append("where JYLX = ").Append(CrmPosData.TransTypeCoupon2Cash);
                        sql.Append("  and XFJLID = ").Append(billHead.ServerBillId);
                        sql.Append("  and BJ_YHQ = 1");
                        sql.Append("  and JYZT in (1,2)");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            msg = string.Format("该销售单 {0} 在 CRM 中已有未完成的扣返券款退还交易交易, 请先冲正", billHead.ServerBillId);
                        }
                        reader.Close();
                    }
                    if (msg.Length == 0)
                    {
                        if (paymentList.Count() > 1)
                        {
                            for (int i = paymentList.Count() - 1; i >= 1; i--)
                            {
                                for (int j = 0; j < i; j++)
                                {
                                    if (paymentList[j].CouponType == paymentList[i].CouponType)
                                    {
                                        paymentList[j].PayMoney += paymentList[i].PayMoney;
                                        paymentList.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                        }
                        totalMoney = 0;
                        for (int i = 0; i < paymentList.Count(); i++)
                        {
                            CrmCouponPayment payment = paymentList[i];

                            if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                            {
                                msg = string.Format("扣返券款退还的金额 <= 0", payment.CouponType);
                                break;
                            }

                            totalMoney = totalMoney + payment.PayMoney;

                            bool isFound = false;
                            for (int j = 0; j < i; j++)
                            {
                                if (paymentList[j].CouponType == payment.CouponType)
                                {
                                    isFound = true;
                                    payment.StoreScope = paymentList[j].StoreScope;
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                sql.Length = 0;
                                sql.Append("select b.FS_YQMDFW from YHQDEF b,YHQSYSH c ");
                                sql.Append("where b.YHQID = c.YHQID");
                                sql.Append("  and b.YHQID = ").Append(payment.CouponType);
                                sql.Append("  and b.BJ_TY = 0 ");
                                sql.Append("  and c.SHDM = '").Append(billHead.CompanyCode).Append("'");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    switch (DbUtils.GetInt(reader, 0))
                                    {
                                        case 1:
                                            payment.StoreScope = string.Empty;
                                            break;
                                        case 2:
                                            payment.StoreScope = billHead.CompanyCode;
                                            break;

                                        case 3:
                                            payment.StoreScope = billHead.StoreCode;
                                            break;

                                    }
                                    reader.Close();
                                }
                                else
                                {
                                    reader.Close();
                                    msg = string.Format("优惠券 {0} 不能在本商户使用或已停用", payment.CouponType);
                                    break;
                                }
                            }
                        }
                        paymentList2 = new List<CrmCouponPayment>();
                        DbUtils.AddDatetimeInputParameter(cmd, "JSRQ");
                        foreach (CrmCouponPayment payment in paymentList)
                        {
                            cmd.Parameters[0].Value = payment.ValidDate;
                            double tmp = payment.PayMoney;
                            double payMoney = 0;
                            sql.Length = 0;
                            sql.Append("select JSRQ,JE from HYK_YHQZH ");
                            sql.Append("where YHQID = ").Append(payment.CouponType);
                            sql.Append("  and HYID = ").Append(payment.VipId);
                            sql.Append("  and CXID = ").Append(promId);
                            DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", ">=");
                            if (payment.StoreScope.Length == 0)
                                sql.Append("  and MDFWDM = ' '");
                            else
                                sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                            sql.Append("  and JE > 0 ");
                            sql.Append("order by JSRQ ");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                DateTime validDate = DbUtils.GetDateTime(reader, 0);
                                double balance = DbUtils.GetDouble(reader, 1);
                                if (MathUtils.DoubleAGreaterThanDoubleB(balance, tmp))
                                {
                                    payMoney = tmp;
                                    tmp = 0;
                                }
                                else
                                {
                                    payMoney = balance;
                                    tmp = tmp - balance;
                                }
                                CrmCouponPayment payment2 = new CrmCouponPayment();
                                paymentList2.Add(payment2);
                                payment2.VipId = payment.VipId;
                                payment2.CouponType = payment.CouponType;
                                payment2.PromId = promId;
                                payment2.StoreScope = payment.StoreScope;
                                payment2.ValidDate = validDate;
                                payment2.PayMoney = payMoney;
                                if (MathUtils.DoubleAEuqalToDoubleB(tmp, 0))
                                    break;
                            }
                            reader.Close();
                            if (MathUtils.DoubleAGreaterThanDoubleB(tmp, 0))
                            {
                                cmd.Parameters.Clear();
                                msg = "优惠券帐户余额不足";
                                break;
                            }
                        }
                        cmd.Parameters.Clear();
                    }
                    if (msg.Length == 0)
                    {
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        bool existBill = (billHead.ServerBillId > 0);
                        if (!existBill)
                            billHead.ServerBillId = SeqGenerator.GetSeqHYK_XFJL("CRMDB", CrmServerPlatform.CurrentDbId);
                        transId = SeqGenerator.GetSeqHYK_JYCL("CRMDB", CrmServerPlatform.CurrentDbId);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            if (existBill)
                            {
                                DeleteRSaleBillFromDb(cmd, sql, billHead.ServerBillId);
                            }

                            sql.Length = 0;
                            sql.Append("insert into HYK_XFJL(XFJLID, SHDM, MDID, SKTNO, JLBH, DJLX,HYID_FQ,SKYDM, JZRQ,XFSJ,SCSJ,STATUS)");
                            sql.Append(" values(").Append(billHead.ServerBillId);
                            sql.Append(",'").Append(billHead.CompanyCode);
                            sql.Append("',").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",").Append(billHead.BillType);
                            if (paymentList.Count > 0)
                                sql.Append(",").Append(paymentList[0].VipId);
                            else
                                sql.Append(",null");
                            sql.Append(",'").Append(billHead.Cashier).Append("'");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "XFSJ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "SCSJ", "");
                            sql.Append(",0");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFSJ", serverTime); //billHead.SaleTime);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SCSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL(JYID,JYLX,XFJLID,MDID,SKTNO,JLBH,SKYDM,BJ_YHQ,JYZT,JZRQ,ZBSJ,JYJE,CXID)");
                            sql.Append(" values(").Append(transId);
                            sql.Append(",").Append(CrmPosData.TransTypeCoupon2Cash);
                            sql.Append(",").Append(billHead.ServerBillId);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",'").Append(billHead.Cashier);
                            sql.Append("',1,1");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(",").Append(totalMoney.ToString("f2"));
                            sql.Append(",").Append(promId.ToString());
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL_ZBZT(JYID,ZBSJ)");
                            sql.Append(" values(").Append(transId);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            DbUtils.AddDatetimeInputParameter(cmd, "JSRQ");
                            foreach (CrmCouponPayment payment in paymentList2)
                            {
                                cmd.Parameters[0].Value = payment.ValidDate;
                                sql.Length = 0;
                                sql.Append("insert into HYK_JYCLITEM_YHQ(JYID,HYID,YHQID,CXID,JSRQ,MDFWDM,JE)");
                                sql.Append(" values(").Append(transId);
                                sql.Append(",").Append(payment.VipId);
                                sql.Append(",").Append(payment.CouponType);
                                sql.Append(",").Append(payment.PromId);
                                DbUtils.SpellSqlParameter(conn, sql, ",", "JSRQ", "");
                                if (payment.StoreScope.Length == 0)
                                    sql.Append(",' '");
                                else
                                    sql.Append(",'").Append(payment.StoreScope).Append("'");
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("update HYK_YHQZH set JE = JE - ").Append(payment.PayMoney.ToString("f2"));
                                sql.Append("  , JYDJJE = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JYDJJE,0) + ").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(" where YHQID = ").Append(payment.CouponType);
                                sql.Append("   and HYID = ").Append(payment.VipId);
                                sql.Append("   and CXID = ").Append(payment.PromId);
                                DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                if (payment.StoreScope.Length == 0)
                                    sql.Append("  and MDFWDM = ' '");
                                else
                                    sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                sql.Append("   and JE >= ").Append(payment.PayMoney.ToString("f2"));
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    msg = "优惠券帐户余额不足";
                                    break;
                                }
                            }
                            cmd.Parameters.Clear();

                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //确认扣返券款退还交易
        public static bool ConfirmTransCoupon2Cash(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            string companyCode = string.Empty;
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
            int promId = 0;
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
                    sql.Length = 0;
                    sql.Append("select a.XFJLID,a.JYLX,a.BJ_YHQ,a.JYJE,a.JYZT,a.MDID,a.SKTNO,a.JLBH,a.SKYDM,a.JZRQ,a.CXID,b.SHDM from HYK_JYCL a,MDDY b ");
                    sql.Append(" where JYID = ").Append(transId);
                    sql.Append("   and a.MDID = b.MDID ");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("确认优惠券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypeCoupon2Cash)
                            msg = string.Format("确认优惠券交易({0})不是扣返券款退还交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("确认交易({0})没有优惠券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("确认优惠券交易({0})的金额不匹配", transId);
                        else
                        {
                            if (serverBillId <= 0)
                                serverBillId = DbUtils.GetInt(reader, 0);
                            transStatus = DbUtils.GetInt(reader, 4);
                            if (transStatus == CrmPosData.TransStatusPrepared)
                            {
                                storeId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                                promId = DbUtils.GetInt(reader, 10);
                                companyCode = DbUtils.GetString(reader, 11);
                            }
                            else if (transStatus != CrmPosData.TransStatusCanceled)
                            {
                                msg = string.Format("确认优惠券交易({0}) 已取消", transId);
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("确认优惠券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && (transStatus == CrmPosData.TransStatusPrepared))
                    {
                        List<CrmCouponPayment> paymentList = new List<CrmCouponPayment>();
                        sql.Length = 0;
                        sql.Append("select HYID,YHQID,CXID,JSRQ,MDFWDM,JE from HYK_JYCLITEM_YHQ ");
                        sql.Append("where JYID = ").Append(transId);
                        sql.Append("  and JE <> 0 ");
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

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("update HYK_JYCL set JYZT = 2 ");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "TJSJ", "=");
                            sql.Append(" where JYID = ").Append(transId);
                            sql.Append("   and JYZT = 1");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "TJSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("update HYK_XFJL set STATUS = ").Append(CrmPosData.BillStatusCheckedOut);
                                sql.Append(" where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                int seqInx = 0;
                                foreach (CrmCouponPayment payment in paymentList)
                                {
                                    UpdateVipCouponAccount(out msg, cmd, sql, 10,
                                                            false,
                                                            true,
                                                            payment.VipId,
                                                            payment.CouponType,
                                                            payment.PromId,
                                                            payment.ValidDate,
                                                            payment.StoreScope,
                                                            -payment.PayMoney,
                                                            out payment.Balance,
                                                            seqBegin + seqInx++,
                                                            transId,
                                                            storeId,
                                                            posId,
                                                            billId,
                                                            serverTime,
                                                            accountDate,
                                                            cashier,
                                                            "前台扣返券款退还");
                                    if (msg.Length > 0)
                                    {
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                }

                                List<CrmPromOfferCouponCalcItem> offerBackCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                                foreach (CrmCouponPayment payment in paymentList)
                                {
                                    sql.Length = 0;
                                    sql.Append("update HYK_THKQCE set KQCE = KQCE - ").Append(payment.PayMoney);
                                    sql.Append(" where HYID = ").Append(payment.VipId);
                                    sql.Append("  and CXID = ").Append(payment.PromId);
                                    sql.Append("  and YHQID = ").Append(payment.CouponType);
                                    if (payment.StoreScope.Length == 0)
                                        sql.Append("  and MDFWDM = ' '");
                                    else
                                        sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                    sql.Append("  and KQCE >= ").Append(payment.PayMoney);
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                    {
                                        msg = "扣返券款退还太多,请重新来一次";
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                    offerBackCouponCalcItemList.Clear();
                                    sql.Length = 0;
                                    sql.Append("select a.XFLJFQFS,a.YHQFFGZID,sum(a.KQJE-a.KQJE_SJ) as KQCE from HYTHJL_KQ a ");
                                    sql.Append("where a.CXID = ").Append(payment.PromId);
                                    sql.Append("  and a.YHQID = ").Append(payment.CouponType);
                                    sql.Append("  and (exists(select * from HYK_XFJL b where a.XFJLID = b.XFJLID and b.HYID_FQ = ").Append(payment.VipId).Append(" and b.DJLX > 0 and STATUS = 1) ");
                                    sql.Append("   or exists(select * from HYXFJL c where a.XFJLID = c.XFJLID and c.HYID_FQ = ").Append(payment.VipId).Append(" and c.DJLX > 0)) ");
                                    sql.Append("group by a.XFLJFQFS,a.YHQFFGZID ");
                                    sql.Append("having sum(a.KQJE-a.KQJE_SJ) <> 0 ");
                                    sql.Append("order by a.XFLJFQFS,a.YHQFFGZID ");
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                                        offerBackCouponCalcItemList.Add(calcItem);
                                        calcItem.AddupSaleMoneyType = DbUtils.GetInt(reader, 0);
                                        calcItem.RuleId = DbUtils.GetInt(reader, 1);
                                        calcItem.OfferCouponMoney = DbUtils.GetDouble(reader, 2);
                                    }
                                    reader.Close();
                                    double temp = payment.PayMoney;
                                    double procMoney = 0;
                                    foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                    {
                                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, temp))
                                        {
                                            procMoney = temp;
                                            temp = 0;
                                        }
                                        else
                                        {
                                            procMoney = calcItem.OfferCouponMoney;
                                            temp -= calcItem.OfferCouponMoney;
                                        }
                                        sql.Length = 0;
                                        sql.Append("insert into HYTHJL_KQ(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,MDFWDM_YQ,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,SYXFJE_NEW,THJE,KQJE,KQJE_SJ,BMDM) ");
                                        sql.Append("  values(").Append(serverBillId);
                                        sql.Append(",").Append(payment.PromId);
                                        sql.Append(",").Append(payment.CouponType);
                                        sql.Append(",").Append(calcItem.RuleId);
                                        sql.Append(",").Append(calcItem.AddupSaleMoneyType);
                                        if (payment.StoreScope.Length == 0)
                                            sql.Append(", ' '");
                                        else
                                            sql.Append(", '").Append(payment.StoreScope).Append("'");
                                        sql.Append(",0,0,0,0,0,0,").Append(procMoney.ToString("f2"));
                                        sql.Append(",' '");
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();

                                        sql.Length = 0;
                                        sql.Append("insert into HYK_XFJL_FQ(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,ZXFJE,SYXFJE,FQJE,BMDM) ");
                                        sql.Append("  values(").Append(serverBillId);
                                        sql.Append(",").Append(payment.PromId);
                                        sql.Append(",").Append(payment.CouponType);
                                        sql.Append(",").Append(calcItem.RuleId);
                                        sql.Append(",").Append(calcItem.AddupSaleMoneyType);
                                        sql.Append(",0,0,0,0,0,").Append((-procMoney).ToString("f2"));
                                        sql.Append(",' '");
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                    if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                                    {
                                        msg = "扣返券款退还太多,请重新来一次";
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                }
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //取消扣返券款退还交易
        public static bool CancelTransCoupon2Cash(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            string companyCode = string.Empty;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
            int promId = 0;
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
                    sql.Length = 0;
                    sql.Append("select a.XFJLID,a.JYLX,a.BJ_YHQ,a.JYJE,a.JYZT,a.MDID,a.SKTNO,a.JLBH,a.SKYDM,a.JZRQ,a.CXID,b.SHDM from HYK_JYCL a,MDDY b ");
                    sql.Append(" where JYID = ").Append(transId);
                    sql.Append("   and a.MDID = b.MDID ");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("取消优惠券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypeCoupon2Cash)
                            msg = string.Format("取消优惠券交易({0})不是扣返券款退还交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 1)
                            msg = string.Format("取消交易({0})没有优惠券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("取消优惠券交易({0})的金额不匹配", transId);
                        else
                        {
                            if (serverBillId <= 0)
                                serverBillId = DbUtils.GetInt(reader, 0);
                            transStatus = DbUtils.GetInt(reader, 4);
                            if ((transStatus == 1) || (transStatus == 2))
                            {
                                storeId = DbUtils.GetInt(reader, 5);
                                posId = DbUtils.GetString(reader, 6);
                                billId = DbUtils.GetInt(reader, 7);
                                cashier = DbUtils.GetString(reader, 8);
                                accountDate = DbUtils.GetDateTime(reader, 9);
                                promId = DbUtils.GetInt(reader, 10);
                                companyCode = DbUtils.GetString(reader, 11);
                            }
                        }
                    }
                    else
                    {
                        //msg = string.Format("确认优惠券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && ((transStatus == CrmPosData.TransStatusPrepared) || (transStatus == CrmPosData.TransStatusConfirmed)))
                    {
                        if (serverBillId >= 0)
                        {
                            //判断已结账
                        }
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

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqCount = paymentList.Count;
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            if (transStatus == CrmPosData.TransStatusPrepared)
                            {
                                sql.Append("update HYK_JYCL set JYZT = 3 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 1");
                            }
                            else
                            {
                                sql.Append("update HYK_JYCL set JYZT = 4 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 2");
                            }
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("update HYK_XFJL set STATUS = ").Append(CrmPosData.BillStatusPrepareCheckOut);
                                sql.Append(" where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if (transStatus == CrmPosData.TransStatusPrepared)
                                {
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        //冻结状态
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
                                            msg = "优惠券帐户冻结金额不足";
                                            dbTrans.Rollback();
                                            return false;
                                        }
                                        cmd.Parameters.Clear();
                                    }
                                }
                                else
                                {
                                    //确认状态
                                    int seqInx = 0;
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        UpdateVipCouponAccount(out msg, cmd, sql, 10,
                                                                false,
                                                                false,
                                                                payment.VipId,
                                                                payment.CouponType,
                                                                payment.PromId,
                                                                payment.ValidDate,
                                                                payment.StoreScope,
                                                                payment.PayMoney,
                                                                out payment.Balance,
                                                                seqBegin + seqInx++,
                                                                transId,
                                                                storeId,
                                                                posId,
                                                                billId,
                                                                serverTime,
                                                                accountDate,
                                                                cashier,
                                                                "前台扣返券款退还冲正");
                                        if (msg.Length > 0)
                                        {
                                            dbTrans.Rollback();
                                            return false;
                                        }
                                    }
                                    foreach (CrmCouponPayment payment in paymentList)
                                    {
                                        sql.Length = 0;
                                        sql.Append("update HYK_THKQCE set KQCE = KQCE + ").Append(payment.PayMoney);
                                        sql.Append(" where HYID = ").Append(payment.VipId);
                                        sql.Append("  and CXID = ").Append(promId);
                                        sql.Append("  and YHQID = ").Append(payment.CouponType);
                                        if (payment.StoreScope.Length == 0)
                                            sql.Append("  and MDFWDM = ' '");
                                        else
                                            sql.Append("  and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                        cmd.CommandText = sql.ToString();
                                        if (cmd.ExecuteNonQuery() == 0)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_THKQCE(HYID,CXID,YHQID,MDFWDM,KQCE) ");
                                            sql.Append("  values(").Append(payment.VipId);
                                            sql.Append(",").Append(promId);
                                            sql.Append(",").Append(payment.CouponType);
                                            if (payment.StoreScope.Length == 0)
                                                sql.Append(",' '");
                                            else
                                                sql.Append(",'").Append(payment.StoreScope).Append("'");
                                            sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //准备零钱包交易
        public static bool PrepareTransSaveChangeIntoVipCard(out string msg, out int transId, out double balance, int vipId, int serverBillId, double changeMoney)
        {
            msg = string.Empty;
            transId = 0;
            balance = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            string cashier = string.Empty;
            DateTime accountDate = DateTime.MinValue;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
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
                    sql.Length = 0;
                    sql.Append("select STATUS,MDID,SKTNO,JLBH,JZRQ,SKYDM from HYK_XFJL ");
                    sql.Append("where XFJLID = ").Append(serverBillId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int status = DbUtils.GetInt(reader, 0);
                        storeId = DbUtils.GetInt(reader, 1);
                        posId = DbUtils.GetString(reader, 2);
                        billId = DbUtils.GetInt(reader, 3);
                        accountDate = DbUtils.GetDateTime(reader, 4);
                        cashier = DbUtils.GetString(reader, 5);
                        if (status == CrmPosData.BillStatusCheckedOut)
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备零钱包交易", billId);
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select MDID,SKTNO,JLBH,JZRQ,SKYDM from HYXFJL ");
                        sql.Append("where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            billId = DbUtils.GetInt(reader, 2);
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备零钱包交易", billId);
                        }
                    }
                    reader.Close();

                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select JYZT from HYK_JYCL ");
                        sql.Append("where JYLX = ").Append(CrmPosData.TransTypeSaveChange);
                        sql.Append("  and XFJLID = ").Append(serverBillId);
                        sql.Append("  and JYZT in (1,2)");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            msg = string.Format("该销售单 {0} 在 CRM 中已有未完成的零钱包交易, 请先冲正", serverBillId);
                        }
                        reader.Close();
                    }

                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select YE from HYK_JEZH where HYID = ").Append(vipId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            balance = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();

                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        transId = SeqGenerator.GetSeqHYK_JYCL("CRMDB", CrmServerPlatform.CurrentDbId);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL(JYID,JYLX,XFJLID,MDID,SKTNO,JLBH,SKYDM,JYZT,JZRQ,ZBSJ,JYJE,HYID_TO)");
                            sql.Append(" values(").Append(transId);
                            sql.Append(",").Append(CrmPosData.TransTypeSaveChange);
                            sql.Append(",").Append(serverBillId);
                            sql.Append(",").Append(storeId);
                            sql.Append(",'").Append(posId);
                            sql.Append("',").Append(billId);
                            sql.Append(",'").Append(cashier);
                            sql.Append("',1");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(",").Append(changeMoney.ToString("f2"));
                            sql.Append(",").Append(vipId.ToString());
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL_ZBZT(JYID,ZBSJ)");
                            sql.Append(" values(").Append(transId);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //确认零钱包交易
        public static bool ConfirmTransSaveChangeIntoVipCard(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
            int vipId = 0;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ,HYID_TO from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("确认零钱包交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypeSaveChange)
                            msg = string.Format("确认交易({0})不是零钱包交易", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 2), transMoney))
                            msg = string.Format("确认零钱包交易({0})的金额不匹配", transId);
                        else
                        {
                            if (serverBillId <= 0)
                                serverBillId = DbUtils.GetInt(reader, 0);
                            transStatus = DbUtils.GetInt(reader, 3);
                            if (transStatus == CrmPosData.TransStatusPrepared)
                            {
                                storeId = DbUtils.GetInt(reader, 4);
                                posId = DbUtils.GetString(reader, 5);
                                billId = DbUtils.GetInt(reader, 6);
                                cashier = DbUtils.GetString(reader, 7);
                                accountDate = DbUtils.GetDateTime(reader, 8);
                                vipId = DbUtils.GetInt(reader, 9);
                            }
                            else if (transStatus != CrmPosData.TransStatusCanceled)
                            {
                                msg = string.Format("确认零钱包交易({0}) 已取消", transId);
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("确认零钱包交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && (transStatus == CrmPosData.TransStatusPrepared))
                    {
                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId, 1);
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("update HYK_JYCL set JYZT = 2 ");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "TJSJ", "=");
                            sql.Append(" where JYID = ").Append(transId);
                            sql.Append("   and JYZT = 1");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "TJSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                double balance = 0;
                                UpdateCashCardAccount(out msg, cmd, sql, 1,
                                                            false,
                                                            vipId,
                                                            transMoney,
                                                            out balance,
                                                            seqBegin,
                                                            transId,
                                                            storeId,
                                                            posId,
                                                            billId,
                                                            serverTime,
                                                            accountDate,
                                                            cashier,
                                                            "前台存零钱");
                                if (msg.Length > 0)
                                {
                                    dbTrans.Rollback();
                                    return false;
                                }
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //取消零钱包交易
        public static bool CancelTransSaveChangeIntoVipCard(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            int storeId = 0;
            string posId = string.Empty;
            int billId = 0;
            DateTime accountDate = DateTime.MinValue;
            string cashier = string.Empty;
            int vipId = 0;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ,HYID_TO from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("取消零钱包交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != CrmPosData.TransTypeSaveChange)
                            msg = string.Format("取消交易({0})不是零钱包交易", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 2), transMoney))
                            msg = string.Format("取消零钱包交易({0})的金额不匹配", transId);
                        else
                        {
                            if (serverBillId <= 0)
                                serverBillId = DbUtils.GetInt(reader, 0);
                            transStatus = DbUtils.GetInt(reader, 3);
                            if ((transStatus == CrmPosData.TransStatusPrepared) || (transStatus == CrmPosData.TransStatusConfirmed))
                            {
                                storeId = DbUtils.GetInt(reader, 4);
                                posId = DbUtils.GetString(reader, 5);
                                billId = DbUtils.GetInt(reader, 6);
                                cashier = DbUtils.GetString(reader, 7);
                                accountDate = DbUtils.GetDateTime(reader, 8);
                                vipId = DbUtils.GetInt(reader, 9);
                            }
                        }
                    }
                    else
                    {
                        //msg = string.Format("取消零钱包交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && ((transStatus == CrmPosData.TransStatusPrepared) || (transStatus == CrmPosData.TransStatusConfirmed)))
                    {
                        if (serverBillId >= 0)
                        {
                            //判断已结账
                        }

                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        int seqBegin = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId, 1);
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            if (transStatus == CrmPosData.TransStatusPrepared)
                            {
                                sql.Append("update HYK_JYCL set JYZT = 3 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 1");
                            }
                            else
                            {
                                sql.Append("update HYK_JYCL set JYZT = 4 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 2");
                            }
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if (transStatus == CrmPosData.TransStatusConfirmed)
                                {
                                    //确认状态
                                    double balance = 0;
                                    UpdateCashCardAccount(out msg, cmd, sql, 1,
                                                                false,
                                                                vipId,
                                                                -transMoney,
                                                                out balance,
                                                                seqBegin,
                                                                transId,
                                                                storeId,
                                                                posId,
                                                                billId,
                                                                serverTime,
                                                                accountDate,
                                                                cashier,
                                                                "前台存零钱冲正");
                                    if (msg.Length > 0)
                                    {
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                }
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        //取原单信息(有原单商品序号)
        public static bool GetOriginalBillInfoExistArticleInx(out string msg, DbCommand cmd, CrmRSaleBillHead billHead, List<CrmArticle> articleList, List<CrmArticleCoupon> articlePayCouponList, List<CrmPromOfferCouponCalcItem> offerCouponCalcItemList)
        {
            msg = string.Empty;
            StringBuilder sql = new StringBuilder();

            #region 取原单主表信息
            billHead.OriginalServerBillId = 0;
            billHead.IsFromBackupTable = false;
            sql.Length = 0;
            sql.Append("select XFJLID,DJLX,XFRQ_FQ,HYID,HYID_FQ,JFBS,PGRYID,VIPTYPE,HYKNO,FXDW,HYKNO_FQ,BSFS,JE,STATUS from HYK_XFJL ");
            sql.Append(" where MDID = ").Append(billHead.StoreId);
            sql.Append("   and SKTNO = '").Append(billHead.OriginalPosId).Append("'");
            sql.Append("   and JLBH = ").Append(billHead.OriginalBillId);
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                billHead.IsFromBackupTable = true;
                sql.Length = 0;
                sql.Append("select XFJLID,DJLX,XFRQ_FQ,HYID,HYID_FQ,JFBS,PGRYID,VIPTYPE,HYKNO,FXDW,HYKNO_FQ,BSFS,JE from HYXFJL ");
                sql.Append(" where MDID = ").Append(billHead.StoreId);
                sql.Append("   and SKTNO = '").Append(billHead.OriginalPosId).Append("'");
                sql.Append("   and JLBH = ").Append(billHead.OriginalBillId);
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    return true;
                }
            }
            billHead.OriginalServerBillId = DbUtils.GetInt(reader, 0);
            int originalBillType = DbUtils.GetInt(reader, 1);
            billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 2);
            billHead.VipId = DbUtils.GetInt(reader, 3);
            billHead.OfferCouponVipId = DbUtils.GetInt(reader, 4);
            billHead.CentMultiple = DbUtils.GetDouble(reader, 5);
            billHead.SaleUserId = DbUtils.GetInt(reader, 6);
            billHead.VipType = DbUtils.GetInt(reader, 7);
            billHead.VipCode = DbUtils.GetString(reader, 8);
            billHead.IssueCardCompanyId = DbUtils.GetInt(reader, 9);
            billHead.OfferCouponVipCode = DbUtils.GetString(reader, 10);
            billHead.CentMultiMode = DbUtils.GetInt(reader, 11);
            billHead.OriginalTotalSaleMoney = DbUtils.GetDouble(reader, 12);
            int billStatus = CrmPosData.BillStatusCheckedOut;
            if (!billHead.IsFromBackupTable)
                billStatus = DbUtils.GetInt(reader, 13);
            reader.Close();
            if (billStatus == CrmPosData.BillStatusArticlesUploaded)
            {
                billHead.OriginalServerBillId = 0;
                return true;
            }
            if (billStatus != CrmPosData.BillStatusCheckedOut)
            {
                msg = string.Format("所选销售单 {0}-{1}-{2} 在 CRM 中还没有结帐", billHead.StoreId, billHead.OriginalPosId, billHead.OriginalBillId);
                return false;
            }
            else if (originalBillType != CrmPosData.BillTypeSale)
            {
                msg = string.Format("所选销售单 {0}-{1}-{2} 在 CRM 中是退货单", billHead.StoreId, billHead.OriginalPosId, billHead.OriginalBillId);
                return false;
            }
            #endregion

            if (billHead.OriginalServerBillId == 0)
                return true;

            #region 取原单商品

            List<CrmArticle> originalArticleList = new List<CrmArticle>();
            sql.Length = 0;
            sql.Append("select INX,SHSPID,BMDM,BJ_JF,BJ_BCJHD,JFDYDBH,ZKDYDBH,ZKL,JFJS,BJ_JFBS,BS,FTBL,XSJE,THJE,JF,XSJE_JF,XSJE_FQ ");
            if (billHead.IsFromBackupTable)
                sql.Append("  from HYXFJL_SP ");
            else
                sql.Append("  from HYK_XFJL_SP ");
            sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
            //sql.Append("   and XSJE<>0 ");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmArticle article = new CrmArticle();
                originalArticleList.Add(article);
                article.Inx = DbUtils.GetInt(reader, 0);
                article.ArticleId = DbUtils.GetInt(reader, 1);
                article.DeptCode = DbUtils.GetString(reader, 2);
                article.IsNoCent = (!DbUtils.GetBool(reader, 3));
                article.IsNoProm = DbUtils.GetBool(reader, 4);
                article.VipCentBillId = DbUtils.GetInt(reader, 5);
                article.VipDiscBillId = DbUtils.GetInt(reader, 6);
                article.VipDiscRate = DbUtils.GetDouble(reader, 7);
                article.BaseCent = DbUtils.GetDouble(reader, 8);
                article.CanCentMultiple = DbUtils.GetBool(reader, 9);
                article.CentMultiple = DbUtils.GetInt(reader, 10);
                article.CentShareRate = DbUtils.GetDouble(reader, 11);
                article.SaleMoney = DbUtils.GetDouble(reader, 12);
                article.SaleBackMoney = DbUtils.GetDouble(reader, 13);
                article.GainedCent = DbUtils.GetDouble(reader, 14);
                article.SaleMoneyForCent = DbUtils.GetDouble(reader, 15);
                article.SaleMoneyForOfferCoupon = DbUtils.GetDouble(reader, 16);
            }
            reader.Close();

            #endregion

            #region 判断是否多退
            foreach (CrmArticle article in articleList)
            {
                article.OriginalArticle = null;
                foreach (CrmArticle originalArticle in originalArticleList)
                {
                    if (originalArticle.Inx == article.OriginalInx)
                    {
                        article.OriginalArticle = originalArticle;
                        if (MathUtils.DoubleASmallerThanDoubleB(originalArticle.SaleMoney - originalArticle.SaleBackMoney + article.SaleMoney, 0))
                        {
                            if (MathUtils.DoubleAEuqalToDoubleB(originalArticle.SaleBackMoney, 0))
                                msg = string.Format("商品退货额 大于 销售额（本单第 {0} 条，原单第 {1} 条）", article.Inx,article.OriginalInx);
                            else
                                msg = string.Format("商品合计退货额 大于 销售额（本单第 {0} 条，原单第 {1} 条）", article.Inx, article.OriginalInx);
                            return false;
                        }
                    }
                }
                if (article.OriginalArticle == null)
                {
                    msg = string.Format("原单没有商品（本单第 {0} 条，原单第 {1} 条）", article.Inx, article.OriginalInx);
                    return false;
                }
            }
            #endregion

            #region 计算积分
            billHead.TotalGainedCent = 0;
            if (billHead.VipId > 0)
            {
                foreach (CrmArticle article in articleList)
                {
                    article.IsNoCent = article.OriginalArticle.IsNoCent;
                    article.IsNoProm = article.OriginalArticle.IsNoProm;
                    article.VipCentBillId = article.OriginalArticle.VipCentBillId;
                    article.VipDiscBillId = article.OriginalArticle.VipDiscBillId;
                    article.VipDiscRate = article.OriginalArticle.VipDiscRate;
                    article.BaseCent = article.OriginalArticle.BaseCent;
                    article.CanCentMultiple = article.OriginalArticle.CanCentMultiple;
                    article.CentMultiple = article.OriginalArticle.CentMultiple;
                    article.CentShareRate = article.OriginalArticle.CentShareRate;
                    if (!article.IsNoCent)
                    {
                        if (MathUtils.DoubleAEuqalToDoubleB(article.OriginalArticle.SaleMoney, -article.SaleMoney))
                        {
                            article.SaleMoneyForCent = -article.OriginalArticle.SaleMoneyForCent;
                            article.GainedCent = -article.OriginalArticle.GainedCent;
                        }
                        else if (MathUtils.DoubleAGreaterThanDoubleB(article.OriginalArticle.SaleMoney, 0))
                        {
                            article.SaleMoneyForCent = Math.Round(article.OriginalArticle.SaleMoneyForCent * (article.SaleMoney / article.OriginalArticle.SaleMoney), 4);
                            article.GainedCent = Math.Round(article.OriginalArticle.GainedCent * (article.SaleMoney / article.OriginalArticle.SaleMoney), 4);
                            //article.CalcVipCent(billHead.CentMultiple, billHead.CentMultiMode);
                        }
                    }
                }
                foreach (CrmArticle article in articleList)
                {
                    billHead.TotalGainedCent += article.GainedCent;
                }
            }
            #endregion

            #region 计算退券
            List<CrmArticleCoupon> originalArticlePayCouponList = new List<CrmArticleCoupon>();

            sql.Length = 0;
            sql.Append("select a.INX,a.YHQID,a.CXID,a.YHQSYDBH,a.YHQSYGZID,a.BJ_FQ,a.XSJE_YQ,a.YQJE ");
            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                sql.Append(",a.CXHDBH ");
            if (billHead.IsFromBackupTable)
                sql.Append(" from HYXFJL_SP_YQFT a, YHQDEF c ");
            else
                sql.Append(" from HYK_XFJL_SP_YQFT a, YHQDEF c ");
            sql.Append(" where a.XFJLID = ").Append(billHead.OriginalServerBillId);
            //sql.Append("   and a.XSJE > 0 ");
            sql.Append("   and a.YHQID = c.YHQID ");
            sql.Append("   and c.BJ_TS = 0 ");
            sql.Append("   and c.BJ_DZYHQ <> 0 ");
            if (articleList.Count == 1)
                sql.Append("  and a.INX = ").Append(articleList[0].OriginalInx);
            else
            {
                sql.Append("  and a.INX in (").Append(articleList[0].OriginalInx);
                for (int i = 1; i < articleList.Count; i++)
                {
                    sql.Append(",").Append(articleList[i].OriginalInx);
                }
                sql.Append(")");
            }
            sql.Append(" order by a.INX ");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int originalInx = DbUtils.GetInt(reader, 0);
                foreach (CrmArticle article in articleList)
                {
                    if (article.OriginalInx == originalInx)
                    {
                        CrmArticleCoupon articleCoupon = new CrmArticleCoupon();
                        articlePayCouponList.Add(articleCoupon);
                        articleCoupon.Article = article;
                        articleCoupon.ArticleInx = article.Inx;
                        articleCoupon.CouponType = DbUtils.GetInt(reader, 1);
                        articleCoupon.ArticleId = article.ArticleId;
                        articleCoupon.DeptCode = article.DeptCode;
                        articleCoupon.PromId = DbUtils.GetInt(reader, 2);
                        articleCoupon.RuleBillId = DbUtils.GetInt(reader, 3);
                        articleCoupon.RuleId = DbUtils.GetInt(reader, 4);
                        articleCoupon.JoinPromOfferCoupon = DbUtils.GetBool(reader, 5);
                        articleCoupon.SaleMoney = article.SaleMoney;
                        if (MathUtils.DoubleAEuqalToDoubleB(article.OriginalArticle.SaleMoney, -article.SaleMoney))
                        {
                            articleCoupon.PayLimitMoney = - DbUtils.GetDouble(reader, 6);
                            articleCoupon.PayMoney = - DbUtils.GetDouble(reader, 7);
                        }
                        else if (MathUtils.DoubleAGreaterThanDoubleB(article.OriginalArticle.SaleMoney, 0))
                        {
                            articleCoupon.PayLimitMoney = Math.Round(DbUtils.GetDouble(reader, 6) * (article.SaleMoney / article.OriginalArticle.SaleMoney), 4);
                            articleCoupon.PayMoney = Math.Round(DbUtils.GetDouble(reader, 7) * (article.SaleMoney / article.OriginalArticle.SaleMoney), 4);
                        }
                        if (CrmServerPlatform.Config.IsUpgradedProject2013)
                        {
                            if (articleCoupon.PromId == 0)
                            {
                                articleCoupon.PromIdIsBH = true;
                                articleCoupon.PromId = DbUtils.GetInt(reader, 8);
                            }
                        }
                        break;
                    }
                }
            }
            reader.Close();
            #endregion

            #region 计算扣券
            if (billHead.OfferCouponVipId > 0)
            {
                bool existPromOfferCoupon = false;
                sql.Length = 0;
                sql.Append("select a.INX,a.YHQID,a.CXID,a.YHQFFDBH,a.XFLJFQFS,a.YHQFFGZID,c.FS_FQMDFW ");
                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                    sql.Append(",a.CXHDBH ");
                if (billHead.IsFromBackupTable)
                    sql.Append(" from HYXFJL_SP_FQ a, YHQDEF c ");
                else
                    sql.Append(" from HYK_XFJL_SP_FQ a, YHQDEF c ");
                sql.Append(" where a.XFJLID = ").Append(billHead.OriginalServerBillId);
                //sql.Append("   and a.XSJE > 0 ");
                sql.Append("   and a.YHQID = c.YHQID ");
                sql.Append("   and c.BJ_TS = 0 ");
                sql.Append("   and c.BJ_DZYHQ <> 0 ");
                if (articleList.Count == 1)
                    sql.Append("  and a.INX = ").Append(articleList[0].OriginalInx);
                else
                {
                    sql.Append("  and a.INX in (").Append(articleList[0].OriginalInx);
                    for (int i = 1; i < articleList.Count; i++)
                    {
                        sql.Append(",").Append(articleList[i].OriginalInx);
                    }
                    sql.Append(")");
                }
                sql.Append(" order by a.INX ");
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int originalInx = DbUtils.GetInt(reader, 0);
                    foreach (CrmArticle article in articleList)
                    {
                        if (article.OriginalInx == originalInx)
                        {
                            existPromOfferCoupon = true;
                            CrmPromOfferCouponCalcItem calcItemOfArticle = new CrmPromOfferCouponCalcItem();
                            article.PromOfferCouponCalcItemList.Add(calcItemOfArticle);
                            calcItemOfArticle.CouponType = DbUtils.GetInt(reader, 1);
                            calcItemOfArticle.PromId = DbUtils.GetInt(reader, 2);
                            calcItemOfArticle.RuleBillId = DbUtils.GetInt(reader, 3);
                            calcItemOfArticle.AddupSaleMoneyType = DbUtils.GetInt(reader, 4);
                            calcItemOfArticle.RuleId = DbUtils.GetInt(reader, 5);
                            if (DbUtils.GetInt(reader, 6) == 3)
                                calcItemOfArticle.OfferStoreScope = billHead.StoreCode;
                            else
                                calcItemOfArticle.OfferStoreScope = billHead.CompanyCode;
                            calcItemOfArticle.IsPaperCoupon = false;
                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                            {
                                if (calcItemOfArticle.PromId == 0)
                                {
                                    calcItemOfArticle.PromIdIsBH = true;
                                    calcItemOfArticle.PromId = DbUtils.GetInt(reader, 7);
                                }
                            }
                            break;
                        }
                    }
                }
                reader.Close();

                if (existPromOfferCoupon)
                {
                    foreach (CrmArticle article in articleList)
                    {
                        article.SaleMoneyForOfferCoupon = 0;
                        if (MathUtils.DoubleAEuqalToDoubleB(article.OriginalArticle.SaleMoney, -article.SaleMoney))
                            article.SaleMoneyForOfferCoupon = -article.OriginalArticle.SaleMoneyForOfferCoupon;
                        else if (MathUtils.DoubleAGreaterThanDoubleB(article.OriginalArticle.SaleMoney, 0))
                            article.SaleMoneyForOfferCoupon = Math.Round(article.OriginalArticle.SaleMoneyForOfferCoupon * (article.SaleMoney / article.OriginalArticle.SaleMoney), 4);
                        foreach (CrmPromOfferCouponCalcItem calcItemOfArticle in article.PromOfferCouponCalcItemList)
                        {
                            bool isFound = false;
                            foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                            {
                                if ((calcItem.CouponType == calcItemOfArticle.CouponType) && (calcItem.RuleId == calcItemOfArticle.RuleId) && (calcItem.AddupSaleMoneyType == calcItemOfArticle.AddupSaleMoneyType) && (calcItem.PromId == calcItemOfArticle.PromId))
                                {
                                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)    //单笔单柜
                                    {
                                        if (calcItem.DeptCode.Equals(article.DeptCode))
                                        {
                                            isFound = true;
                                            calcItem.ArticleList.Add(article);
                                            calcItem.SaleMoney = calcItem.SaleMoney - article.SaleMoneyForOfferCoupon;  //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isFound = true;
                                        calcItem.ArticleList.Add(article);
                                        calcItem.SaleMoney = calcItem.SaleMoney - article.SaleMoneyForOfferCoupon;  //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                                        break;
                                    }
                                }
                            }
                            if (!isFound)
                            {
                                CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                                offerCouponCalcItemList.Add(calcItem);
                                calcItem.ArticleList.Add(article);
                                calcItem.CouponType = calcItemOfArticle.CouponType;
                                calcItem.IsPaperCoupon = calcItemOfArticle.IsPaperCoupon;
                                calcItem.OfferStoreScope = calcItemOfArticle.OfferStoreScope;
                                calcItem.AddupSaleMoneyType = calcItemOfArticle.AddupSaleMoneyType;
                                calcItem.PromId = calcItemOfArticle.PromId;
                                calcItem.RuleBillId = calcItemOfArticle.RuleBillId;
                                calcItem.RuleId = calcItemOfArticle.RuleId;
                                calcItem.SaleMoney = -article.SaleMoneyForOfferCoupon; //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                                calcItem.SaleMoneyUsed = 0;
                                calcItem.OfferCouponMoney = 0;
                                if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)    //单笔单柜
                                    calcItem.DeptCode = article.DeptCode;
                            }
                        }
                    }
                    PromCalculator.GetPromOfferCouponDataBefore(offerCouponCalcItemList, billHead.OfferCouponVipId, DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate), billHead.OriginalServerBillId, billHead.IsFromBackupTable, cmd);
                    PromCalculator.CalcPromOfferBackCoupon(offerCouponCalcItemList, false);
                }
            }
            #endregion

            return true;
        }

        //取原单信息(没有原单商品序号)
        public static bool GetOriginalBillInfoNoArticleInx(out string msg, DbCommand cmd, CrmRSaleBillHead billHead, List<CrmArticle> articleList, List<CrmArticleCoupon> articlePayCouponList, List<CrmPromOfferCouponCalcItem> offerCouponCalcItemList)
        {
            msg = string.Empty;
            StringBuilder sql = new StringBuilder();

            #region 取原单主表信息
            billHead.OriginalServerBillId = 0;
            billHead.IsFromBackupTable = false;
            sql.Length = 0;
            sql.Append("select XFJLID,DJLX,XFRQ_FQ,HYID,HYID_FQ,JFBS,PGRYID,VIPTYPE,HYKNO,FXDW,HYKNO_FQ,BSFS,JE,STATUS from HYK_XFJL ");
            sql.Append(" where MDID = ").Append(billHead.StoreId);
            sql.Append("   and SKTNO = '").Append(billHead.OriginalPosId).Append("'");
            sql.Append("   and JLBH = ").Append(billHead.OriginalBillId);
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                billHead.IsFromBackupTable = true;
                sql.Length = 0;
                sql.Append("select XFJLID,DJLX,XFRQ_FQ,HYID,HYID_FQ,JFBS,PGRYID,VIPTYPE,HYKNO,FXDW,HYKNO_FQ,BSFS,JE from HYXFJL ");
                sql.Append(" where MDID = ").Append(billHead.StoreId);
                sql.Append("   and SKTNO = '").Append(billHead.OriginalPosId).Append("'");
                sql.Append("   and JLBH = ").Append(billHead.OriginalBillId);
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    return true;
                }
            }
            billHead.OriginalServerBillId = DbUtils.GetInt(reader, 0);
            int originalBillType = DbUtils.GetInt(reader, 1);
            billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 2);
            billHead.VipId = DbUtils.GetInt(reader, 3);
            billHead.OfferCouponVipId = DbUtils.GetInt(reader, 4);
            billHead.CentMultiple = DbUtils.GetDouble(reader, 5);
            billHead.SaleUserId = DbUtils.GetInt(reader, 6);
            billHead.VipType = DbUtils.GetInt(reader, 7);
            billHead.VipCode = DbUtils.GetString(reader, 8);
            billHead.IssueCardCompanyId = DbUtils.GetInt(reader, 9);
            billHead.OfferCouponVipCode = DbUtils.GetString(reader, 10);
            billHead.CentMultiMode = DbUtils.GetInt(reader, 11);
            billHead.OriginalTotalSaleMoney = DbUtils.GetDouble(reader, 12);
            int billStatus = CrmPosData.BillStatusCheckedOut;
            if (!billHead.IsFromBackupTable)
                billStatus = DbUtils.GetInt(reader, 13);
            reader.Close();
            if (billStatus == CrmPosData.BillStatusArticlesUploaded)
            {
                billHead.OriginalServerBillId = 0;
                return true;
            }
            if (billStatus != CrmPosData.BillStatusCheckedOut)
            {
                msg = string.Format("所选销售单 {0}-{1}-{2} 在 CRM 中还没有结帐", billHead.StoreId, billHead.OriginalPosId, billHead.OriginalBillId);
                return false;
            }
            else if (originalBillType != CrmPosData.BillTypeSale)
            {
                msg = string.Format("所选销售单 {0}-{1}-{2} 在 CRM 中是退货单", billHead.StoreId, billHead.OriginalPosId, billHead.OriginalBillId);
                return false;
            }
            #endregion

            if (billHead.OriginalServerBillId == 0)
                return true;

            #region 取原单商品

            List<CrmArticle> originalArticleList = new List<CrmArticle>();
            sql.Length = 0;
            sql.Append("select SHSPID,BMDM,BJ_JF,BJ_BCJHD,JFDYDBH,ZKDYDBH,ZKL,JFJS,BJ_JFBS,BS,FTBL,sum(XSJE) as XSJE,sum(THJE) as THJE,sum(JF) as JF,sum(XSJE_JF) XSJE_JF,sum(XSJE_FQ) XSJE_FQ ");
            if (billHead.IsFromBackupTable)
                sql.Append("  from HYXFJL_SP ");
            else
                sql.Append("  from HYK_XFJL_SP ");
            sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
            sql.Append("   and XSJE<>0 ");
            sql.Append(" group by SHSPID,BMDM,BJ_JF,BJ_BCJHD,JFDYDBH,ZKDYDBH,ZKL,JFJS,BJ_JFBS,BS,FTBL ");
            sql.Append(" having sum(XSJE) >= 0 ");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmArticle article = new CrmArticle();
                originalArticleList.Add(article);
                article.ArticleId = DbUtils.GetInt(reader, 0);
                article.DeptCode = DbUtils.GetString(reader, 1);
                article.IsNoCent = (!DbUtils.GetBool(reader, 2));
                article.IsNoProm = DbUtils.GetBool(reader, 3);
                article.VipCentBillId = DbUtils.GetInt(reader, 4);
                article.VipDiscBillId = DbUtils.GetInt(reader, 5);
                article.VipDiscRate = DbUtils.GetDouble(reader, 6);
                article.BaseCent = DbUtils.GetDouble(reader, 7);
                article.CanCentMultiple = DbUtils.GetBool(reader, 8);
                article.CentMultiple = DbUtils.GetInt(reader, 9);
                article.CentShareRate = DbUtils.GetDouble(reader, 10);
                article.SaleMoney = DbUtils.GetDouble(reader, 11);
                article.SaleBackMoney = DbUtils.GetDouble(reader, 12);
                article.GainedCent = DbUtils.GetDouble(reader, 13);
                article.SaleMoneyForCent = DbUtils.GetDouble(reader, 14);
                article.SaleMoneyForOfferCoupon = DbUtils.GetDouble(reader, 15);
            }
            reader.Close();

            #endregion

            #region 合并本单商品
            List<CrmArticle> mergedArticleList = new List<CrmArticle>();
            List<List<CrmArticle>> mergedSameArticleList = new List<List<CrmArticle>>();
            if (articleList.Count == 1)
            {
                mergedArticleList.Add(articleList[0]);
                List<CrmArticle> sameArticleList = new List<CrmArticle>();
                mergedSameArticleList.Add(sameArticleList);
                sameArticleList.Add(articleList[0]);
            }
            else
            {
                foreach (CrmArticle article in articleList)
                {
                    bool isFound = false;
                    for (int i = 0; i < mergedArticleList.Count; i++)
                    {
                        CrmArticle mergedArticle = mergedArticleList[i];
                        if ((mergedArticle.ArticleId == article.ArticleId) && mergedArticle.DeptCode.Equals(article.DeptCode))
                        {
                            isFound = true;
                            mergedSameArticleList[i].Add(article);
                            mergedArticle.SaleMoney += article.SaleMoney;
                        }
                    }
                    if (!isFound)
                    {
                        CrmArticle mergedArticle = new CrmArticle();
                        mergedArticleList.Add(mergedArticle);
                        mergedArticle.ArticleId = article.ArticleId;
                        mergedArticle.ArticleCode = article.ArticleCode;
                        mergedArticle.DeptCode = article.DeptCode;
                        mergedArticle.SaleMoney = article.SaleMoney;

                        List<CrmArticle> sameArticleList = new List<CrmArticle>();
                        mergedSameArticleList.Add(sameArticleList);
                        sameArticleList.Add(article);
                    }
                }
            }
            #endregion

            #region 判断是否多退
            for (int i = 0; i < mergedArticleList.Count; i++)
            {
                CrmArticle mergedArticle = mergedArticleList[i];
                bool isFound = false;
                foreach (CrmArticle originalArticle in originalArticleList)
                {
                    if ((originalArticle.ArticleId == mergedArticle.ArticleId) && originalArticle.DeptCode.Equals(mergedArticle.DeptCode))
                    {
                        isFound = true;
                        if (MathUtils.DoubleASmallerThanDoubleB(originalArticle.SaleMoney - originalArticle.SaleBackMoney + mergedArticle.SaleMoney, 0))
                        {
                            if (MathUtils.DoubleAEuqalToDoubleB(originalArticle.SaleBackMoney, 0))
                                msg = string.Format("商品 {0} 的退货额 > 销售额", mergedArticle.ArticleCode);
                            else
                                msg = string.Format("商品 {0} 已经退过货，合计退货额 > 销售额", mergedArticle.ArticleCode);
                            return false;
                        }
                    }
                }
                if (!isFound)
                {
                    msg = string.Format("原销售单没有商品 {0}", mergedArticle.ArticleCode);
                    return false;
                }

            }
            #endregion

            #region 计算积分
            billHead.TotalGainedCent = 0;
            if (billHead.VipId > 0)
            {
                for (int i = 0; i < mergedArticleList.Count; i++)
                {
                    CrmArticle mergedArticle = mergedArticleList[i];
                    CrmArticle originalArticle = null;
                    foreach (CrmArticle article in originalArticleList)
                    {
                        if ((article.ArticleId == mergedArticle.ArticleId) && article.DeptCode.Equals(mergedArticle.DeptCode))
                        {
                            originalArticle = article;
                            break;
                        }
                    }
                    if ((originalArticle != null) && (!originalArticle.IsNoCent) && (MathUtils.DoubleAGreaterThanDoubleB(originalArticle.SaleMoney, 0)))
                    {
                        List<CrmArticle> sameArticleList = mergedSameArticleList[i];
                        foreach (CrmArticle article in sameArticleList)
                        {
                            article.IsNoCent = originalArticle.IsNoCent;
                            article.IsNoProm = originalArticle.IsNoProm;
                            article.VipCentBillId = originalArticle.VipCentBillId;
                            article.VipDiscBillId = originalArticle.VipDiscBillId;
                            article.VipDiscRate = originalArticle.VipDiscRate;
                            article.BaseCent = originalArticle.BaseCent;
                            article.CanCentMultiple = originalArticle.CanCentMultiple;
                            article.CentMultiple = originalArticle.CentMultiple;
                            article.CentShareRate = originalArticle.CentShareRate;
                            article.SaleMoneyForCent = Math.Round(article.SaleMoney * originalArticle.SaleMoneyForCent / originalArticle.SaleMoney, 4);
                        }
                        if (MathUtils.DoubleAEuqalToDoubleB(originalArticle.SaleMoney, -mergedArticle.SaleMoney))
                        {
                            if (sameArticleList.Count == 1)
                            {
                                sameArticleList[0].SaleMoneyForCent = -originalArticle.SaleMoneyForCent;
                                sameArticleList[0].GainedCent = -originalArticle.GainedCent;
                            }
                            else
                            {
                                double tempGainedCent = -originalArticle.GainedCent;
                                foreach (CrmArticle article in sameArticleList)
                                {
                                    article.GainedCent = Math.Round(article.SaleMoney * originalArticle.GainedCent / originalArticle.SaleMoney, 4);
                                    tempGainedCent -= article.GainedCent;
                                }
                                if (!MathUtils.DoubleAEuqalToDoubleB(tempGainedCent, 0)) //存在尾差
                                {
                                    sameArticleList[0].GainedCent += tempGainedCent;
                                }
                            }
                        }
                        else
                        {
                            foreach (CrmArticle article in sameArticleList)
                            {
                                article.CalcVipCent(billHead.CentMultiple, billHead.CentMultiMode);
                            }
                        }
                    }
                }
                foreach (CrmArticle article in articleList)
                {
                    billHead.TotalGainedCent += article.GainedCent;
                }
            }
            #endregion

            #region 计算退券
            List<CrmArticleCoupon> originalArticlePayCouponList = new List<CrmArticleCoupon>();

            sql.Length = 0;
            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                sql.Append("select a.YHQID,a.SHSPID,a.BMDM,a.CXID,a.CXHDBH,a.YHQSYDBH,a.YHQSYGZID,a.BJ_FQ,sum(a.XSJE) as XSJE,sum(a.XSJE_YQ) as XSJE_YQ,sum(a.YQJE) as YQJE ");
            else
                sql.Append("select a.YHQID,a.SHSPID,a.BMDM,a.CXID,a.YHQSYDBH,a.YHQSYGZID,a.BJ_FQ,sum(a.XSJE) as XSJE,sum(a.XSJE_YQ) as XSJE_YQ,sum(a.YQJE) as YQJE ");
            if (billHead.IsFromBackupTable)
                sql.Append(" from HYXFJL_SP_YQFT a, YHQDEF c ");
            else
                sql.Append(" from HYK_XFJL_SP_YQFT a, YHQDEF c ");
            sql.Append(" where a.XFJLID = ").Append(billHead.OriginalServerBillId);
            sql.Append("   and a.XSJE > 0 ");
            sql.Append("   and a.YHQID = c.YHQID ");
            sql.Append("   and c.BJ_DZYHQ <> 0 ");
            if (mergedArticleList.Count == 1)
                sql.Append("  and a.SHSPID = ").Append(mergedArticleList[0].ArticleId);
            else
            {
                sql.Append("  and a.SHSPID in (").Append(mergedArticleList[0].ArticleId);
                for (int i = 1; i < mergedArticleList.Count; i++)
                {
                    sql.Append(",").Append(mergedArticleList[i].ArticleId);
                }
                sql.Append(")");
            }
            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                sql.Append(" group by a.YHQID,a.SHSPID,a.BMDM,a.CXID,a.CXHDBH,a.YHQSYDBH,a.YHQSYGZID,a.BJ_FQ ");
            else
                sql.Append(" group by a.YHQID,a.SHSPID,a.BMDM,a.CXID,a.YHQSYDBH,a.YHQSYGZID,a.BJ_FQ ");
            sql.Append(" order by a.SHSPID,a.BMDM,a.YHQID ");
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmArticleCoupon articleCoupon = new CrmArticleCoupon();
                originalArticlePayCouponList.Add(articleCoupon);
                articleCoupon.CouponType = DbUtils.GetInt(reader, 0);
                articleCoupon.ArticleId = DbUtils.GetInt(reader, 1);
                articleCoupon.DeptCode = DbUtils.GetString(reader, 2);
                articleCoupon.PromId = DbUtils.GetInt(reader, 3);
                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                {
                    if (articleCoupon.PromId == 0)
                    {
                        articleCoupon.PromIdIsBH = true;
                        articleCoupon.PromId = DbUtils.GetInt(reader, 4);
                    }
                    articleCoupon.RuleBillId = DbUtils.GetInt(reader, 5);
                    articleCoupon.RuleId = DbUtils.GetInt(reader, 6);
                    articleCoupon.JoinPromOfferCoupon = DbUtils.GetBool(reader, 7);
                    articleCoupon.SaleMoney = DbUtils.GetDouble(reader, 8);
                    articleCoupon.PayLimitMoney = DbUtils.GetDouble(reader, 9);
                    articleCoupon.PayMoney = DbUtils.GetDouble(reader, 10);
                }
                else
                {
                    articleCoupon.RuleBillId = DbUtils.GetInt(reader, 4);
                    articleCoupon.RuleId = DbUtils.GetInt(reader, 5);
                    articleCoupon.JoinPromOfferCoupon = DbUtils.GetBool(reader, 6);
                    articleCoupon.SaleMoney = DbUtils.GetDouble(reader, 7);
                    articleCoupon.PayLimitMoney = DbUtils.GetDouble(reader, 8);
                    articleCoupon.PayMoney = DbUtils.GetDouble(reader, 9);
                }
            }
            reader.Close();
            if (originalArticlePayCouponList.Count > 0)
            {
                for (int i = 0; i < mergedArticleList.Count; i++)
                {
                    CrmArticle mergedArticle = mergedArticleList[i];
                    CrmArticle originalArticle = null;
                    foreach (CrmArticle article in originalArticleList)
                    {
                        if ((article.ArticleId == mergedArticle.ArticleId) && article.DeptCode.Equals(mergedArticle.DeptCode))
                        {
                            originalArticle = article;
                            break;
                        }
                    }
                    if (originalArticle != null)
                    {
                        bool isSameSaleMoney = (MathUtils.DoubleAEuqalToDoubleB(-mergedArticle.SaleMoney, originalArticle.SaleMoney));
                        foreach (CrmArticleCoupon originalArticleCoupon in originalArticlePayCouponList)
                        {
                            if ((mergedArticle.ArticleId == originalArticleCoupon.ArticleId) && mergedArticle.DeptCode.Equals(originalArticleCoupon.DeptCode))
                            {
                                List<CrmArticle> sameArticleList = mergedSameArticleList[i];
                                double tempPayMoney = 0;
                                if (isSameSaleMoney && (sameArticleList.Count > 0))
                                    tempPayMoney = -originalArticleCoupon.PayMoney;
                                CrmArticleCoupon firstArticleCoupon = null;
                                foreach (CrmArticle article in sameArticleList)
                                {
                                    CrmArticleCoupon articleCoupon = new CrmArticleCoupon();
                                    articlePayCouponList.Add(articleCoupon);
                                    articleCoupon.Article = article;
                                    articleCoupon.ArticleInx = article.Inx;
                                    articleCoupon.ArticleId = article.ArticleId;
                                    articleCoupon.DeptCode = article.DeptCode;
                                    articleCoupon.CouponType = originalArticleCoupon.CouponType;
                                    articleCoupon.PromId = originalArticleCoupon.PromId;
                                    articleCoupon.RuleBillId = originalArticleCoupon.RuleBillId;
                                    articleCoupon.RuleId = originalArticleCoupon.RuleId;
                                    articleCoupon.JoinPromOfferCoupon = originalArticleCoupon.JoinPromOfferCoupon;
                                    //退券相关金额为负
                                    articleCoupon.SaleMoney = article.SaleMoney;
                                    if (isSameSaleMoney)
                                    {
                                        if (sameArticleList.Count == 1)
                                        {
                                            articleCoupon.PayLimitMoney = -originalArticleCoupon.PayLimitMoney;
                                            articleCoupon.PayMoney = -originalArticleCoupon.PayMoney;
                                        }
                                        else
                                        {
                                            articleCoupon.PayLimitMoney = Math.Round(originalArticleCoupon.PayLimitMoney * article.SaleMoney / originalArticle.SaleMoney, 2);
                                            articleCoupon.PayMoney = Math.Round(originalArticleCoupon.PayMoney * article.SaleMoney / originalArticle.SaleMoney, 2);
                                            tempPayMoney -= articleCoupon.PayMoney;
                                            if (firstArticleCoupon == null)
                                                firstArticleCoupon = articleCoupon;
                                        }
                                    }
                                    else
                                    {
                                        articleCoupon.PayLimitMoney = Math.Round(originalArticleCoupon.PayLimitMoney * article.SaleMoney / originalArticle.SaleMoney, 2);
                                        articleCoupon.PayMoney = Math.Round(originalArticleCoupon.PayMoney * article.SaleMoney / originalArticle.SaleMoney, 2);
                                    }
                                }
                                if (isSameSaleMoney && (firstArticleCoupon != null) && (!MathUtils.DoubleAEuqalToDoubleB(tempPayMoney, 0)))  //存在尾差
                                {
                                    firstArticleCoupon.PayMoney += tempPayMoney;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 计算扣券
            if (billHead.OfferCouponVipId > 0)
            {
                bool existPromOfferCoupon = false;
                sql.Length = 0;
                sql.Append("select distinct a.SHSPID,a.BMDM,a.YHQID,a.CXID,a.YHQFFDBH,a.XFLJFQFS,a.YHQFFGZID,c.FS_FQMDFW ");
                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                    sql.Append(",a.CXHDBH ");
                if (billHead.IsFromBackupTable)
                    sql.Append(" from HYXFJL_SP_FQ a, YHQDEF c ");
                else
                    sql.Append(" from HYK_XFJL_SP_FQ a, YHQDEF c ");
                sql.Append(" where a.XFJLID = ").Append(billHead.OriginalServerBillId);
                sql.Append("   and a.XSJE > 0 ");
                sql.Append("   and a.YHQID = c.YHQID ");
                sql.Append("   and c.BJ_DZYHQ <> 0 ");
                sql.Append("   and c.BJ_TS = 0 ");
                if (mergedArticleList.Count == 1)
                    sql.Append("  and a.SHSPID = ").Append(mergedArticleList[0].ArticleId);
                else
                {
                    sql.Append("  and a.SHSPID in (").Append(mergedArticleList[0].ArticleId);
                    for (int i = 1; i < mergedArticleList.Count; i++)
                    {
                        sql.Append(",").Append(mergedArticleList[i].ArticleId);
                    }
                    sql.Append(")");
                }
                sql.Append(" order by a.SHSPID,a.BMDM,a.YHQID ");
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int articleId = DbUtils.GetInt(reader, 0);
                    string deptCode = DbUtils.GetString(reader, 1);
                    foreach (CrmArticle article in articleList)
                    {
                        if ((article.ArticleId == articleId) && deptCode.Equals(article.DeptCode))
                        {
                            existPromOfferCoupon = true;
                            CrmPromOfferCouponCalcItem calcItemOfArticle = new CrmPromOfferCouponCalcItem();
                            article.PromOfferCouponCalcItemList.Add(calcItemOfArticle);
                            calcItemOfArticle.CouponType = DbUtils.GetInt(reader, 2);
                            calcItemOfArticle.PromId = DbUtils.GetInt(reader, 3);
                            calcItemOfArticle.RuleBillId = DbUtils.GetInt(reader, 4);
                            calcItemOfArticle.AddupSaleMoneyType = DbUtils.GetInt(reader, 5);
                            calcItemOfArticle.RuleId = DbUtils.GetInt(reader, 6);
                            if (DbUtils.GetInt(reader, 7) == 3)
                                calcItemOfArticle.OfferStoreScope = billHead.StoreCode;
                            else
                                calcItemOfArticle.OfferStoreScope = billHead.CompanyCode;
                            calcItemOfArticle.IsPaperCoupon = false;
                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                            {
                                if (calcItemOfArticle.PromId == 0)
                                {
                                    calcItemOfArticle.PromIdIsBH = true;
                                    calcItemOfArticle.PromId = DbUtils.GetInt(reader, 7);
                                }
                            }
                            //break;    因articleList中，相同商品可能有多条，所以不能break
                        }
                    }
                }
                reader.Close();

                if (existPromOfferCoupon)
                {
                    foreach (CrmArticle article in articleList)
                    {
                        article.SaleMoneyForOfferCoupon = 0;
                        foreach (CrmArticle originalArticle in originalArticleList)
                        {
                            if ((originalArticle.ArticleId == article.ArticleId) && originalArticle.DeptCode.Equals(article.DeptCode))
                            {
                                if (MathUtils.DoubleAGreaterThanDoubleB(originalArticle.SaleMoney, 0))
                                {
                                    article.SaleMoneyForOfferCoupon = Math.Round(article.SaleMoney * originalArticle.SaleMoneyForOfferCoupon / originalArticle.SaleMoney, 4);
                                    break;
                                }
                            }
                        }
                        foreach (CrmPromOfferCouponCalcItem calcItemOfArticle in article.PromOfferCouponCalcItemList)
                        {
                            bool isFound = false;
                            foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                            {
                                if ((calcItem.CouponType == calcItemOfArticle.CouponType) && (calcItem.RuleId == calcItemOfArticle.RuleId) && (calcItem.AddupSaleMoneyType == calcItemOfArticle.AddupSaleMoneyType) && (calcItem.PromId == calcItemOfArticle.PromId))
                                {
                                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)    //单笔单柜
                                    {
                                        if (calcItem.DeptCode.Equals(article.DeptCode))
                                        {
                                            isFound = true;
                                            calcItem.ArticleList.Add(article);
                                            calcItem.SaleMoney = calcItem.SaleMoney - article.SaleMoneyForOfferCoupon;  //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isFound = true;
                                        calcItem.ArticleList.Add(article);
                                        calcItem.SaleMoney = calcItem.SaleMoney - article.SaleMoneyForOfferCoupon;  //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                                        break;
                                    }
                                }
                            }
                            if (!isFound)
                            {
                                CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                                offerCouponCalcItemList.Add(calcItem);
                                calcItem.ArticleList.Add(article);
                                calcItem.CouponType = calcItemOfArticle.CouponType;
                                calcItem.IsPaperCoupon = calcItemOfArticle.IsPaperCoupon;
                                calcItem.OfferStoreScope = calcItemOfArticle.OfferStoreScope;
                                calcItem.AddupSaleMoneyType = calcItemOfArticle.AddupSaleMoneyType;
                                calcItem.PromId = calcItemOfArticle.PromId;
                                calcItem.RuleBillId = calcItemOfArticle.RuleBillId;
                                calcItem.RuleId = calcItemOfArticle.RuleId;
                                calcItem.SaleMoney = -article.SaleMoneyForOfferCoupon; //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                                calcItem.SaleMoneyUsed = 0;
                                calcItem.OfferCouponMoney = 0;
                                if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)    //单笔单柜
                                    calcItem.DeptCode = article.DeptCode;
                            }
                        }
                    }
                    PromCalculator.GetPromOfferCouponDataBefore(offerCouponCalcItemList, billHead.OfferCouponVipId, billHead.OfferCouponDate.DayOfYear, billHead.OriginalServerBillId, billHead.IsFromBackupTable, cmd);
                    PromCalculator.CalcPromOfferBackCoupon(offerCouponCalcItemList, false);
                }
            }
            #endregion

            return true;
        }

        //保存商品
        public static bool SaveRSaleBillArticles(out string msg, CrmRSaleBillHead billHead, List<CrmArticle> articleList, List<CrmCouponPayment> payBackCouponList, List<CrmPromOfferCoupon> offerBackCouponList)
        {
            msg = string.Empty;

            billHead.ServerBillId = 0;
            billHead.TotalDecMoney = 0;
            CrmVipCard vipCard = null;

            bool existPromOfferCoupon = false;
            bool existPromDecMoney = false;

            if (billHead.StoreId == 0)
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,STATUS,XFJLID_OLD from HYK_XFJL ");
                    sql.Append("where MDID = ").Append(billHead.StoreId);
                    sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                    sql.Append("  and JLBH = ").Append(billHead.BillId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        billHead.ServerBillId = DbUtils.GetInt(reader, 0);
                        int status = DbUtils.GetInt(reader, 1);
                        if (status == 1)
                            msg = string.Format("该销售单 {0}-{1}-{2} 在 CRM 中已结帐,不能重新开始", billHead.StoreId, billHead.PosId, billHead.BillId);
                        else if ((status == 2) && (DbUtils.GetInt(reader, 2) > 0))
                            msg = string.Format("该销售单 {0}-{1}-{2} 为选单退货,在 CRM 中已准备结帐,不能重新开始", billHead.StoreId, billHead.PosId, billHead.BillId);
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select XFJLID from HYXFJL ");
                        sql.Append("where MDID = ").Append(billHead.StoreId);
                        sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                        sql.Append("  and JLBH = ").Append(billHead.BillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            msg = string.Format("该销售单 {0}-{1}-{2} 在 CRM 中已结帐,不能重新开始", billHead.StoreId, billHead.PosId, billHead.BillId);
                    }
                    reader.Close();

                    if (msg.Length == 0)
                    {
                        billHead.TotalSaleNum = 0;
                        billHead.TotalSaleMoney = 0;
                        billHead.TotalDiscMoney = 0;
                        billHead.TotalVipDiscMoney = 0;
                        foreach (CrmArticle article in articleList)
                        {
                            billHead.TotalSaleNum = billHead.TotalSaleNum + article.SaleNum;
                            billHead.TotalSaleMoney = billHead.TotalSaleMoney + article.SaleMoney;
                            billHead.TotalDiscMoney = billHead.TotalDiscMoney + article.DiscMoney;
                            billHead.TotalVipDiscMoney = billHead.TotalVipDiscMoney + article.VipDiscMoney;
                            if (billHead.OriginalBillId == 0)
                            {
                                article.SaleMoneyForCent = article.SaleMoney;
                                if (billHead.BillType == 0)
                                {
                                    article.SaleMoneyForOfferCoupon = article.SaleMoney;
                                    article.SaleMoneyForDecMoney = article.SaleMoney;
                                }
                            }

                            sql.Length = 0;
                            sql.Append("select SHSPID from SHSPXX_DM where SHDM = '").Append(billHead.CompanyCode).Append("' and SPDM = '").Append(article.ArticleCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                article.ArticleId = DbUtils.GetInt(reader, 0);
                            }
                            else
                            {
                                msg = string.Format("商品 {0} 还没有上传到 CRM 库", article.ArticleCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;

                            sql.Length = 0;
                            sql.Append("select c.SPFLDM,b.SHSBID,b.SHHTID ");
                            sql.Append(" ,(select GHSDM from SHHT d where b.SHHTID = d.SHHTID) as GHSDM ");
                            sql.Append(" from SHSPXX b,SHSPFL c where b.SHSPFLID = c.SHSPFLID and b.SHSPID = ").Append(article.ArticleId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                article.CategoryCode = DbUtils.GetString(reader, 0);
                                article.BrandId = DbUtils.GetInt(reader, 1);
                                article.ContractId = DbUtils.GetInt(reader, 2);
                                article.SuppCode = DbUtils.GetString(reader, 3);
                            }
                            reader.Close();
                        }
                    }
                    List<CrmArticleCoupon> articlePayBackCouponList = null;
                    List<CrmPromOfferCouponCalcItem> offerBackCouponCalcItemList = null;
                    if ((msg.Length == 0) && (billHead.OriginalBillId > 0))
                    {
                        bool existNegativeInx = false;
                        int zeroInxCount = 0;
                        foreach (CrmArticle article in articleList)
                        {
                            if (article.OriginalInx < 0)
                            {
                                existNegativeInx = true;    //新pos查原单没有Inx，传-1
                                break;
                            }
                            else if (article.OriginalInx == 0)  //如果inx=0有多条，就是pos程序没更新
                                zeroInxCount++;
                        }
                        if (zeroInxCount > 1)
                        {
                            msg = "传来的商品中，有多条的inx_old = 0";
                            return false;
                        }
                        articlePayBackCouponList = new List<CrmArticleCoupon>();
                        offerBackCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                        bool ok = false;
                        if (existNegativeInx)
                            ok = GetOriginalBillInfoNoArticleInx(out msg, cmd, billHead, articleList, articlePayBackCouponList, offerBackCouponCalcItemList);
                        else
                            ok = GetOriginalBillInfoExistArticleInx(out msg, cmd, billHead, articleList, articlePayBackCouponList, offerBackCouponCalcItemList);
                        if (ok)
                        {
                            existPromOfferCoupon = (offerBackCouponCalcItemList.Count > 0);
                            if (articlePayBackCouponList.Count > 0)
                            {
                                #region 选单退货---合计退券
                                foreach (CrmArticleCoupon articleCoupon in articlePayBackCouponList)
                                {
                                    bool isFound = false;
                                    foreach (CrmCouponPayment payBackCoupon in payBackCouponList)
                                    {
                                        if (payBackCoupon.CouponType == articleCoupon.CouponType)
                                        {
                                            isFound = true;
                                            payBackCoupon.PayMoney += articleCoupon.PayMoney;
                                        }
                                    }
                                    if (!isFound)
                                    {
                                        CrmCouponPayment payBackCoupon = new CrmCouponPayment();
                                        payBackCouponList.Add(payBackCoupon);
                                        payBackCoupon.CouponType = articleCoupon.CouponType;
                                        payBackCoupon.PayMoney = articleCoupon.PayMoney;
                                    }
                                }
                                #endregion
                            }
                            if (offerBackCouponCalcItemList.Count > 0)
                            {
                                #region 选单退货---合计扣券
                                foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                {
                                    if (!MathUtils.DoubleAEuqalToDoubleB(calcItem.OfferCouponMoney, 0))
                                    {
                                        bool isFound = false;
                                        foreach (CrmPromOfferCoupon offerBackCoupon in offerBackCouponList)
                                        {
                                            if ((calcItem.CouponType == offerBackCoupon.CouponType) && (calcItem.PromId == offerBackCoupon.PromId))
                                            {
                                                isFound = true;
                                                offerBackCoupon.OfferMoney += calcItem.OfferCouponMoney;
                                                break;
                                            }
                                        }
                                        if (!isFound)
                                        {
                                            CrmPromOfferCoupon offerBackCoupon = new CrmPromOfferCoupon();
                                            offerBackCouponList.Add(offerBackCoupon);
                                            offerBackCoupon.CouponType = calcItem.CouponType;
                                            offerBackCoupon.PromId = calcItem.PromId;
                                            offerBackCoupon.PromIdIsBH = calcItem.PromIdIsBH;
                                            offerBackCoupon.OfferMoney = calcItem.OfferCouponMoney;
                                        }
                                    }
                                }
                                #endregion

                                if (offerBackCouponList.Count > 0)
                                {
                                    foreach (CrmPromOfferCoupon offerBackCoupon in offerBackCouponList)
                                    {
                                        #region 选单退货---取所扣券的名称，有效期，使用门店范围
                                        sql.Length = 0;
                                        sql.Append("select a.FS_YQMDFW,a.YHQMC,b.YHQSYJSRQ from YHQDEF a, YHQDEF_CXHD b ");
                                        sql.Append(" where a.YHQID = b.YHQID ");
                                        sql.Append("   and a.YHQID = ").Append(offerBackCoupon.CouponType);
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013 && offerBackCoupon.PromIdIsBH)
                                        {
                                            sql.Append("   and b.CXHDBH = ").Append(offerBackCoupon.PromId);
                                            sql.Append("   and b.SHDM = '").Append(billHead.CompanyCode).Append("'");
                                        }
                                        else
                                            sql.Append("   and b.CXID = ").Append(offerBackCoupon.PromId);
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            switch (DbUtils.GetInt(reader, 0))
                                            {
                                                case 2:
                                                    offerBackCoupon.PayStoreScope = billHead.CompanyCode;
                                                    break;
                                                case 3:
                                                    offerBackCoupon.PayStoreScope = billHead.StoreCode;
                                                    break;
                                                default:
                                                    //offerBackCoupon.PayStoreScope = string.Empty;
                                                    offerBackCoupon.PayStoreScope = " ";
                                                    break;
                                            }
                                            offerBackCoupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                                            offerBackCoupon.ValidDate = DbUtils.GetDateTime(reader, 2).Date;

                                            foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                            {
                                                if ((calcItem.CouponType == offerBackCoupon.CouponType) && (calcItem.PromId == offerBackCoupon.PromId))
                                                {
                                                    calcItem.PayStoreScope = offerBackCoupon.PayStoreScope;
                                                }
                                            }
                                        }

                                        #endregion

                                        #region 选单退货---计算扣券差额
                                        offerBackCoupon.OfferBackDifference = offerBackCoupon.OfferMoney;
                                        cmd.Parameters.Clear();
                                        sql.Length = 0;
                                        sql.Append("select sum(JE) from HYK_YHQZH ");
                                        sql.Append("where YHQID = ").Append(offerBackCoupon.CouponType);
                                        sql.Append("  and HYID = ").Append(billHead.OfferCouponVipId);
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013 && offerBackCoupon.PromIdIsBH)
                                        {
                                            DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", offerBackCoupon.ValidDate);
                                        }
                                        else
                                            sql.Append("  and CXID = ").Append(offerBackCoupon.PromId);
                                        if (offerBackCoupon.PayStoreScope.Length == 0)
                                            sql.Append("  and MDFWDM = ' '");
                                        else
                                            sql.Append("  and MDFWDM = '").Append(offerBackCoupon.PayStoreScope).Append("'");
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                            offerBackCoupon.Balance = DbUtils.GetDouble(reader, 0);
                                        reader.Close();
                                        cmd.Parameters.Clear();
                                        if (MathUtils.DoubleASmallerThanDoubleB(offerBackCoupon.Balance, offerBackCoupon.OfferBackDifference))
                                            offerBackCoupon.OfferBackDifference -= offerBackCoupon.Balance;
                                        else
                                            offerBackCoupon.OfferBackDifference = 0;

                                        if (MathUtils.DoubleAGreaterThanDoubleB(offerBackCoupon.OfferBackDifference, 0) && (billHead.BillType == CrmPosData.BillTypeReturn))
                                        {
                                            for (int i = payBackCouponList.Count - 1; i >= 0; i--)
                                            {
                                                CrmCouponPayment payBackCoupon = payBackCouponList[i];
                                                if (payBackCoupon.CouponType == offerBackCoupon.CouponType)
                                                {
                                                    if (MathUtils.DoubleAGreaterThanDoubleB((-payBackCoupon.PayMoney), offerBackCoupon.OfferBackDifference))
                                                    {
                                                        //payBackCoupon.PayMoney += offerBackCoupon.OfferBackDifference;
                                                        offerBackCoupon.OfferBackDifference = 0;
                                                    }
                                                    else
                                                    {
                                                        offerBackCoupon.OfferBackDifference += payBackCoupon.PayMoney;
                                                        //payBackCoupon.PayMoney = 0;
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        offerBackCoupon.ActualOfferMoney = (offerBackCoupon.OfferMoney - offerBackCoupon.OfferBackDifference);
                                        if (MathUtils.DoubleAGreaterThanDoubleB(offerBackCoupon.ActualOfferMoney, 0))
                                        {
                                            double temp = offerBackCoupon.ActualOfferMoney;
                                            foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                            {
                                                if ((calcItem.CouponType == offerBackCoupon.CouponType) && (calcItem.PromId == offerBackCoupon.PromId))
                                                {
                                                    if (MathUtils.DoubleAEuqalToDoubleB(temp - calcItem.OfferCouponMoney, 0))
                                                    {
                                                        calcItem.ActualOfferMoney += calcItem.OfferCouponMoney;
                                                        temp -= calcItem.OfferCouponMoney;
                                                    }
                                                    else
                                                    {
                                                        calcItem.ActualOfferMoney += temp;
                                                        temp = 0;
                                                    }
                                                }
                                                if (MathUtils.DoubleAEuqalToDoubleB(temp, 0))
                                                    break;
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                    }

                    if (msg.Length > 0)
                        return false;

                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                    if (billHead.SaleTime == DateTime.MinValue)
                        billHead.SaleTime = serverTime;
                    if (billHead.AccountDate == DateTime.MinValue)
                        billHead.AccountDate = billHead.SaleTime.Date;

                    if (billHead.OriginalServerBillId == 0)
                    {
                        if (billHead.VipId > 0)     //刷会员卡，销售、无单退货或选单退货时所选单在CRM中不存在
                        {
                            sql.Length = 0;
                            sql.Append("select a.HYK_NO,a.HYKTYPE,a.FXDW,b.BJ_YHQZH,b.BJ_JF,a.STATUS from HYK_HYXX a,HYKDEF b where a.HYKTYPE = b.HYKTYPE and a.HYID = ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                vipCard = new CrmVipCard();
                                vipCard.CardId = billHead.VipId;
                                vipCard.CardCode = DbUtils.GetString(reader, 0);
                                vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                                vipCard.IssueCardCompanyId = DbUtils.GetInt(reader, 2);
                                vipCard.CanOwnCoupon = (DbUtils.GetBool(reader, 3));
                                //vipCard.CanCent = ((DbUtils.GetInt(reader, 5) != 5) && DbUtils.GetBool(reader, 4));
                                vipCard.CanCent = DbUtils.GetBool(reader, 4);
                                vipCard.Status = DbUtils.GetInt(reader, 5);
                            }
                            reader.Close();

                            if (((vipCard != null) && (billHead.VipCode != null) && (billHead.VipCode.Length > 0) && (!vipCard.CardCode.Equals(billHead.VipCode))))  //子卡
                            {
                                sql.Length = 0;
                                sql.Append("select FXDW from HYK_CHILD_JL where HYK_NO = '").Append(billHead.VipCode).Append("'");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    vipCard.IssueCardCompanyId = DbUtils.GetInt(reader, 0);
                                }
                                else
                                    billHead.VipCode = null;
                                reader.Close();
                            }

                            if (vipCard != null)
                            {
                                if ((billHead.VipCode == null) || (billHead.VipCode.Length == 0))
                                    billHead.VipCode = vipCard.CardCode;
                                billHead.VipType = vipCard.CardTypeId;
                                billHead.IssueCardCompanyId = vipCard.IssueCardCompanyId;
                                if (vipCard.CanOwnCoupon)
                                {
                                    billHead.OfferCouponVipId = billHead.VipId;
                                    billHead.OfferCouponVipCode = billHead.VipCode;
                                }
                                else
                                {
                                    billHead.OfferCouponVipId = 0;
                                    billHead.OfferCouponVipCode = string.Empty;
                                }
                                sql.Length = 0;
                                sql.Append("select CSRQ,ZYID,ZJLXID,SEX,BJ_CLD from HYK_GRXX where HYID = ").Append(billHead.VipId);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    vipCard.Birthday = DbUtils.GetDateTime(reader, 0);
                                    vipCard.JobType = DbUtils.GetInt(reader, 1);
                                    vipCard.IdCardType = DbUtils.GetInt(reader, 2);
                                    vipCard.SexType = DbUtils.GetInt(reader, 3);
                                    vipCard.BirthdayIsChinese = DbUtils.GetBool(reader, 4);
                                }
                                reader.Close();
                            }
                        }
                        if ((vipCard != null) && (vipCard.CanCent))
                        {
                            PromRuleSearcher.LookupVipCentMultiple(out billHead.CentMultiple, out billHead.CentMultiMode, cmd, vipCard, billHead.StoreId, serverTime);
                            PromRuleSearcher.LookupVipCentRuleOfArticle(cmd,0, vipCard, billHead.CompanyCode, serverTime, articleList);
                        }


                        if (billHead.BillType == CrmPosData.BillTypeSale) //销售时才返券和满减，退货时不返券和不满减
                        {
                            PromRuleSearcher.LookupPromOfferCouponRuleOfArticle(out existPromOfferCoupon, cmd, 0, vipCard, billHead.CompanyCode, serverTime, articleList);
                            if (existPromOfferCoupon)   //前一笔小票是选单换货，则本笔小票的当日累积返券的日期按选单换货的那笔算
                            {
                                sql.Length = 0;
                                sql.Append("select XFRQ_FQ from HYK_XFJL ");
                                sql.Append(" where MDID = ").Append(billHead.StoreId);
                                sql.Append("   and SKTNO = '").Append(billHead.PosId).Append("'");
                                sql.Append("   and JLBH = ").Append(billHead.BillId - 1);
                                sql.Append("   and STATUS = ").Append(CrmPosData.BillStatusCheckedOut);
                                sql.Append("   and HYID_FQ = ").Append(billHead.OfferCouponVipId);
                                sql.Append("   and DJLX = ").Append(CrmPosData.BillTypeExchange);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 0);
                                }
                                else
                                {
                                    billHead.OfferCouponDate = billHead.SaleTime.AddHours(-6).Date;
                                }
                                reader.Close();
                            }
                            PromRuleSearcher.LookupPromDecMoneyRuleOfArticle(out existPromDecMoney, cmd,0, vipCard, billHead.CompanyCode, serverTime, articleList);
                            if (existPromDecMoney)
                            {
                                List<CrmPromDecMoneyCalcItem> decMoneyCalcItemList = new List<CrmPromDecMoneyCalcItem>();
                                PromCalculator.MakePromDecMoneyCalcItemList(decMoneyCalcItemList, articleList);
                                foreach (CrmArticle article in articleList)
                                {
                                    article.SaleMoneyNoShare = article.SaleMoney;
                                    article.SaleMoneyForDecMoney = article.SaleMoney;
                                    article.DecMoney = 0;
                                }
                                double saleMoneyUsed = 0;
                                PromCalculator.CalcPromDecMoney(decMoneyCalcItemList, out billHead.TotalDecMoney, out saleMoneyUsed);
                            }
                        }
                    }

                    bool existBill = (billHead.ServerBillId > 0);
                    if (!existBill)
                        billHead.ServerBillId = SeqGenerator.GetSeqHYK_XFJL("CRMDB", CrmServerPlatform.CurrentDbId);
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;
                        if (existBill)
                        {
                            DeleteRSaleBillFromDb(cmd, sql, billHead.ServerBillId);
                        }

                        sql.Length = 0;
                        sql.Append("insert into HYK_XFJL(XFJLID, SHDM, MDID, SKTNO, JLBH, DJLX,XFJLID_OLD, HYID,HYID_FQ, SKYDM, JZRQ,XFSJ,XFRQ_FQ,SCSJ,STATUS,BJ_WZSP,JFBS,JF,JE,ZK,ZK_HY,PGRYID,VIPTYPE,HYKNO,HYKNO_FQ,FXDW,BSFS,XSSL)");
                        sql.Append(" values(").Append(billHead.ServerBillId);
                        sql.Append(",'").Append(billHead.CompanyCode);
                        sql.Append("',").Append(billHead.StoreId);
                        sql.Append(",'").Append(billHead.PosId);
                        sql.Append("',").Append(billHead.BillId);
                        sql.Append(",").Append(billHead.BillType);
                        sql.Append(",").Append(billHead.OriginalServerBillId);
                        sql.Append(",").Append(billHead.VipId);
                        sql.Append(",").Append(billHead.OfferCouponVipId);
                        sql.Append(",'").Append(billHead.Cashier).Append("'");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "XFSJ", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "XFRQ_FQ", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "SCSJ", "");
                        sql.Append(",0,0,");
                        sql.Append(billHead.CentMultiple.ToString("f2"));
                        sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                        sql.Append(",").Append(billHead.TotalDiscMoney.ToString("f2"));
                        sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                        sql.Append(",").Append(billHead.SaleUserId);
                        sql.Append(",").Append(billHead.VipType);
                        sql.Append(",'").Append(billHead.VipCode);
                        sql.Append("','").Append(billHead.OfferCouponVipCode);
                        sql.Append("',").Append(billHead.IssueCardCompanyId);
                        sql.Append(",").Append(billHead.CentMultiMode);
                        sql.Append(",").Append(billHead.TotalSaleNum.ToString("f4"));
                        sql.Append(")");
                        cmd.CommandText = sql.ToString();
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFSJ", billHead.SaleTime);
                        DbParameter param = DbUtils.AddDatetimeInputParameter(cmd, "XFRQ_FQ");
                        if (billHead.OfferCouponDate.Year > 2000)
                            param.Value = billHead.OfferCouponDate;
                        else
                            param.Value = null;
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SCSJ", serverTime);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        foreach (CrmArticle article in articleList)
                        {
                            sql.Length = 0;
                            sql.Append("insert into HYK_XFJL_SP(XFJLID,INX,INX_OLD,SHSPID,SPDM,BMDM,BJ_JF,ZKDYDBH,ZKL,XSSL,XSJE,ZKJE,ZKJE_HY,BJ_BCJHD,JFDYDBH,JFJS,BJ_JFBS,BS,XSJE_JF,XSJE_FQ,JF,FTBL,JFFBGZ)");
                            sql.Append(" values(").Append(billHead.ServerBillId);
                            sql.Append(",").Append(article.Inx);
                            sql.Append(",").Append(article.OriginalInx);
                            sql.Append(",").Append(article.ArticleId);
                            sql.Append(",'").Append(article.ArticleCode);
                            sql.Append("','").Append(article.DeptCode);
                            if (article.IsNoCent)
                                sql.Append("',").Append(0);
                            else
                                sql.Append("',").Append(1);
                            sql.Append(",").Append(article.VipDiscBillId);
                            sql.Append(",").Append(article.VipDiscRate.ToString("f4"));
                            sql.Append(",").Append(article.SaleNum.ToString("f4"));
                            sql.Append(",").Append(article.SaleMoney.ToString("f2"));
                            sql.Append(",").Append(article.DiscMoney.ToString("f2"));
                            sql.Append(",").Append(article.VipDiscMoney.ToString("f2"));
                            if (article.IsNoProm)
                                sql.Append(",").Append(1);
                            else
                                sql.Append(",").Append(0);
                            sql.Append(",").Append(article.VipCentBillId);
                            sql.Append(",").Append(article.BaseCent.ToString());
                            if (article.CanCentMultiple)
                                sql.Append(",").Append(1);
                            else
                                sql.Append(",").Append(0);
                            sql.Append(",").Append(article.CentMultiple);
                            sql.Append(",").Append(article.SaleMoneyForCent.ToString("f2"));
                            sql.Append(",").Append(article.SaleMoneyForOfferCoupon.ToString("f2"));
                            sql.Append(",").Append(article.GainedCent.ToString("f4"));
                            sql.Append(",").Append(article.CentShareRate.ToString("f4"));
                            sql.Append(",").Append(article.CentMoneyMultipleRuleId);
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                        }

                        if (existPromOfferCoupon)
                        {
                            foreach (CrmArticle article in articleList)
                            {
                                foreach (CrmPromOfferCouponCalcItem calcItemOfArticle in article.PromOfferCouponCalcItemList)
                                {
                                    sql.Length = 0;
                                    if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                        sql.Append("insert into HYK_XFJL_SP_FQ(XFJLID,INX,YHQID,SPDM,BMDM,SHSPID,CXID,CXHDBH,YHQFFDBH,XFLJFQFS,YHQFFGZID,DJLX,FQJE,XSJE,XSJE_FQ,XSSL)");
                                    else
                                        sql.Append("insert into HYK_XFJL_SP_FQ(XFJLID,INX,YHQID,SPDM,BMDM,SHSPID,CXID,YHQFFDBH,XFLJFQFS,YHQFFGZID,DJLX,FQJE,XSJE,XSJE_FQ,XSSL)");
                                    sql.Append(" values(").Append(billHead.ServerBillId);
                                    sql.Append(",").Append(article.Inx);
                                    sql.Append(",").Append(calcItemOfArticle.CouponType);
                                    sql.Append(",'").Append(article.ArticleCode);
                                    sql.Append("','").Append(article.DeptCode);
                                    sql.Append("',").Append(article.ArticleId);
                                    if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                    {
                                        if (calcItemOfArticle.PromIdIsBH)
                                            sql.Append(",0,").Append(calcItemOfArticle.PromId);
                                        else
                                            sql.Append(",").Append(calcItemOfArticle.PromId).Append(",0");
                                    }
                                    else
                                        sql.Append(",").Append(calcItemOfArticle.PromId);
                                    sql.Append(",").Append(calcItemOfArticle.RuleBillId);
                                    sql.Append(",").Append(calcItemOfArticle.AddupSaleMoneyType);
                                    sql.Append(",").Append(calcItemOfArticle.RuleId);
                                    sql.Append(",0,").Append(calcItemOfArticle.OfferCouponMoney.ToString("f2"));
                                    sql.Append(",").Append(article.SaleMoney.ToString("f2"));
                                    sql.Append(",").Append(article.SaleMoneyForOfferCoupon.ToString("f2"));
                                    sql.Append(",").Append(article.SaleNum.ToString("f4"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            if (billHead.OriginalServerBillId > 0)
                            {
                                foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                {
                                    sql.Length = 0;
                                    if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                        sql.Append("insert into HYTHJL_KQ(XFJLID,CXID,CXHDBH,YHQID,YHQFFGZID,XFLJFQFS,BMDM,MDFWDM_YQ,MDFWDM_FQ,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,THJE,SYXFJE_NEW,KQJE,KQJE_SJ) ");
                                    else
                                        sql.Append("insert into HYTHJL_KQ(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,BMDM,MDFWDM_YQ,MDFWDM_FQ,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,THJE,SYXFJE_NEW,KQJE,KQJE_SJ) ");
                                    sql.Append("  values(").Append(billHead.ServerBillId);
                                    if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                    {
                                        if (calcItem.PromIdIsBH)
                                            sql.Append(",0,").Append(calcItem.PromId);
                                        else
                                            sql.Append(",").Append(calcItem.PromId).Append(",0");
                                    }
                                    else
                                        sql.Append(",").Append(calcItem.PromId);
                                    sql.Append(",").Append(calcItem.CouponType);
                                    sql.Append(",").Append(calcItem.RuleId);
                                    sql.Append(",").Append(calcItem.AddupSaleMoneyType);
                                    if (calcItem.DeptCode.Length > 0)
                                        sql.Append(",'").Append(calcItem.DeptCode).Append("'");
                                    else
                                        sql.Append(",' '");
                                    if (calcItem.PayStoreScope.Length > 0)
                                        sql.Append(",'").Append(calcItem.PayStoreScope).Append("'");
                                    else
                                        sql.Append(",' '");
                                    if (calcItem.OfferStoreScope.Length > 0)
                                        sql.Append(",'").Append(calcItem.OfferStoreScope).Append("'");
                                    else
                                        sql.Append(",' '");
                                    sql.Append(",").Append(calcItem.SaleMoneyBefore.ToString("f2"));
                                    sql.Append(",").Append(calcItem.SaleMoneyUsedBefore.ToString("f2"));
                                    sql.Append(",").Append(calcItem.OfferCouponMoneyBefore.ToString("f2"));
                                    sql.Append(",").Append(calcItem.SaleMoney.ToString("f2"));
                                    sql.Append(",").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                    sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                    sql.Append(",").Append(calcItem.ActualOfferMoney.ToString("f2"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        if (billHead.OriginalServerBillId > 0)
                        {
                            foreach (CrmArticleCoupon articleCoupon in articlePayBackCouponList)
                            {
                                sql.Length = 0;
                                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                    sql.Append("insert into HYK_XFJL_SP_YQFT(XFJLID, INX, YHQID, BMDM, SPDM, SHSPID, YHQSYDBH, CXID,CXHDBH, YHQSYGZID, BJ_FQ, XSJE, XSJE_YQ, YQJE) ");
                                else
                                    sql.Append("insert into HYK_XFJL_SP_YQFT(XFJLID, INX, YHQID, BMDM, SPDM, SHSPID, YHQSYDBH, CXID, YHQSYGZID, BJ_FQ, XSJE, XSJE_YQ, YQJE) ");
                                sql.Append("  values(").Append(billHead.ServerBillId);
                                sql.Append(",").Append(articleCoupon.ArticleInx);
                                sql.Append(",").Append(articleCoupon.CouponType);
                                sql.Append(",'").Append(articleCoupon.DeptCode);
                                sql.Append("','").Append(" "); //Append(articleCoupon.ArticleCode);
                                sql.Append("',").Append(articleCoupon.ArticleId);
                                sql.Append(",").Append(articleCoupon.RuleBillId);
                                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                {
                                    if (articleCoupon.PromIdIsBH)
                                        sql.Append(",0,").Append(articleCoupon.PromId);
                                    else
                                        sql.Append(",").Append(articleCoupon.PromId).Append(",0");
                                }
                                else
                                    sql.Append(",").Append(articleCoupon.PromId);
                                sql.Append(",").Append(articleCoupon.RuleId);
                                if (articleCoupon.JoinPromOfferCoupon)
                                    sql.Append(",1");
                                else
                                    sql.Append(",0");
                                sql.Append(",").Append(articleCoupon.SaleMoney.ToString("f2")); //articleCoupon.SaleMoney为负
                                sql.Append(",").Append(articleCoupon.PayLimitMoney.ToString("f2")); //articleCoupon.PayLimitMoney为负
                                sql.Append(",").Append(articleCoupon.PayMoney.ToString("f2"));  //articleCoupon.PayMoney为负
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (existPromDecMoney)
                        {
                            foreach (CrmArticle article in articleList)
                            {
                                if (article.DecMoneyRuleId > 0)
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_XFJL_SP_MBJZ(XFJLID,INX,SPDM,BMDM,SHSPID,CXID,MBJZDBH,XFLJMJFS,MBJZGZID,XSJE,XSJE_MBJZ,MBJZJE,XSSL,SHSBID,SHHTID,GHSDM,MJJQ)");
                                    sql.Append(" values(").Append(billHead.ServerBillId);
                                    sql.Append(",").Append(article.Inx);
                                    sql.Append(",'").Append(article.ArticleCode);
                                    sql.Append("','").Append(article.DeptCode);
                                    sql.Append("',").Append(article.ArticleId);
                                    sql.Append(",").Append(article.DecMoneyPromId);
                                    sql.Append(",").Append(article.DecMoneyRuleBillId);
                                    sql.Append(",").Append(article.DecMoneyAddupSaleMoneyType);
                                    sql.Append(",").Append(article.DecMoneyRuleId);
                                    sql.Append(",").Append(article.SaleMoney.ToString("f2"));
                                    sql.Append(",").Append(article.SaleMoneyForDecMoney.ToString("f2"));
                                    sql.Append(",").Append(article.DecMoney.ToString("f2"));
                                    sql.Append(",").Append(article.SaleNum.ToString("f4"));
                                    sql.Append(",").Append(article.BrandId);
                                    sql.Append(",").Append(article.ContractId);
                                    sql.Append(",'").Append(article.SuppCode).Append("'");
                                    sql.Append(",").Append(article.DecMoneyIsExpense ? 1 : 0);
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }

            return (msg.Length == 0);
        }

        //计算满减
        public static bool CalcRSaleBillDecMoney(out string msg, out double decMoney, List<CrmArticle> articleDecMoneyList, int serverBillId, List<CrmPayment> paymentList)
        {
            msg = string.Empty;
            decMoney = 0;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
            billHead.ServerBillId = serverBillId;
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
                    sql.Length = 0;
                    sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.HYID_FQ,a.JZRQ,a.SCSJ,a.SKYDM,a.JE,a.ZK_HY,b.MDDM from HYK_XFJL a,MDDY b ");
                    sql.Append("where XFJLID = ").Append(serverBillId);
                    sql.Append("  and a.MDID = b.MDID");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int billStatus = DbUtils.GetInt(reader, 0);
                        if (billStatus != 0)
                            msg = string.Format("该销售单 {0} 在 CRM 中的状态 = {1}，不能再计算满减", serverBillId, billStatus);
                        else
                        {
                            billHead.CompanyCode = DbUtils.GetString(reader, 1);
                            billHead.StoreId = DbUtils.GetInt(reader, 2);
                            billHead.PosId = DbUtils.GetString(reader, 3);
                            billHead.BillId = DbUtils.GetInt(reader, 4);
                            billHead.BillType = DbUtils.GetInt(reader, 5);
                            billHead.OfferCouponVipId = DbUtils.GetInt(reader, 6);
                            billHead.AccountDate = DbUtils.GetDateTime(reader, 7);
                            billHead.SaleTime = DbUtils.GetDateTime(reader, 8);
                            billHead.Cashier = DbUtils.GetString(reader, 9);
                            billHead.TotalSaleMoney = DbUtils.GetDouble(reader, 10);
                            billHead.TotalVipDiscMoney = DbUtils.GetDouble(reader, 11);
                            billHead.StoreCode = DbUtils.GetString(reader, 12);
                        }
                        reader.Close();
                        if (billHead.BillType == 0)
                        {
                            if (billHead.OfferCouponVipId > 0)
                            {
                                sql.Length = 0;
                                sql.Append("select XFRQ_FQ from HYK_XFJL ");
                                sql.Append(" where MDID = ").Append(billHead.StoreId);
                                sql.Append("   and SKTNO = '").Append(billHead.PosId).Append("'");
                                sql.Append("   and JLBH = ").Append(billHead.BillId - 1);
                                sql.Append("   and STATUS = ").Append(CrmPosData.BillStatusCheckedOut);
                                sql.Append("   and HYID_FQ = ").Append(billHead.OfferCouponVipId);
                                sql.Append("   and DJLX = ").Append(CrmPosData.BillTypeExchange);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 0);
                                }
                                else
                                {
                                    billHead.OfferCouponDate = billHead.SaleTime.AddHours(-6).Date;
                                }
                                reader.Close();
                            }
                        }
                        else
                            msg = "退货时不计算满减";
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select HYID,JF from HYXFJL ");
                        sql.Append("where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐，不能再计算满减", serverBillId);
                        else
                            msg = string.Format("该销售单 {0} 在 CRM 中不存在，不能再计算满减", serverBillId);
                        reader.Close();
                    }

                    if (msg.Length == 0)
                    {
                        billHead.TotalPayMoney = 0;
                        foreach (CrmPayment payment in paymentList)
                        {
                            if (billHead.BillType == CrmPosData.BillTypeSale)
                            {
                                if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                                {
                                    msg = "销售时支付金额 <= 0";
                                    break;
                                }
                            }
                            else
                            {
                                if (!MathUtils.DoubleASmallerThanDoubleB(payment.PayMoney, 0))
                                {
                                    msg = "退货时支付金额 >= 0";
                                    break;
                                }
                            }
                            billHead.TotalPayMoney = billHead.TotalPayMoney + payment.PayMoney;

                            sql.Length = 0;
                            sql.Append("select SHZFFSID,BJ_JF,BJ_FQ,BJ_MBJZ,YHQID from SHZFFS where SHDM = '").Append(billHead.CompanyCode).Append("' and ZFFSDM = '").Append(payment.PayTypeCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                payment.PayTypeId = DbUtils.GetInt(reader, 0);
                                payment.JoinCent = (DbUtils.GetInt(reader, 1) == 1);
                                payment.JoinPromOfferCoupon = (DbUtils.GetInt(reader, 2) == 1);
                                payment.JoinPromDecMoney = (DbUtils.GetInt(reader, 3) == 1);
                                if (reader.IsDBNull(4))
                                    payment.CouponType = -1;
                                else
                                    payment.CouponType = DbUtils.GetInt(reader, 4);
                            }
                            else
                            {
                                msg = string.Format("收款方式 {0} 还没有上传到 CRM 库", payment.PayTypeCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;
                        }
                    }
                    if (msg.Length == 0)
                    {
                        if (!MathUtils.DoubleAEuqalToDoubleB(billHead.TotalPayMoney, billHead.TotalSaleMoney))
                        {
                            msg = "销售单的总销售额(" + billHead.TotalSaleMoney.ToString("f2") + ") <> 总收款额(" + billHead.TotalPayMoney.ToString("f2") + ")";
                        }
                    }
                    if (msg.Length == 0)
                    {
                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                        sql.Length = 0;
                        sql.Append("select INX,MBJZGZID,XSSL,XSJE,CXID,XFLJMJFS,SHSBID,SHHTID,BMDM,GHSDM,MJJQ from HYK_XFJL_SP_MBJZ where XFJLID = ").Append(serverBillId);
                        sql.Append(" order by INX ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmArticle article = new CrmArticle();
                            articleDecMoneyList.Add(article);
                            article.Inx = DbUtils.GetInt(reader, 0);
                            article.DecMoneyRuleId = DbUtils.GetInt(reader, 1);
                            article.SaleNum = DbUtils.GetDouble(reader, 2);
                            article.SaleMoney = DbUtils.GetDouble(reader, 3);
                            article.SaleMoneyForDecMoney = 0;
                            article.DecMoneyPromId = DbUtils.GetInt(reader, 4);
                            article.DecMoneyAddupSaleMoneyType = DbUtils.GetInt(reader, 5);
                            article.BrandId = DbUtils.GetInt(reader, 6);
                            article.ContractId = DbUtils.GetInt(reader, 7);
                            article.DeptCode = DbUtils.GetString(reader, 8);
                            article.SuppCode = DbUtils.GetString(reader, 9);
                            article.DecMoneyIsExpense = DbUtils.GetBool(reader, 10);
                        }
                        reader.Close();

                        if (articleDecMoneyList.Count > 0)
                        {
                            sql.Length = 0;
                            sql.Append("select INX,XSSL,XSJE,BMDM from HYK_XFJL_SP where XFJLID = ").Append(serverBillId);
                            sql.Append("  and INX not in (").Append(articleDecMoneyList[0].Inx);
                            for (int i = 1; i < articleDecMoneyList.Count; i++)
                            {
                                sql.Append(",").Append(articleDecMoneyList[i].Inx);
                            }
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                CrmArticle article = new CrmArticle();
                                article.Inx = DbUtils.GetInt(reader, 0);
                                article.SaleNum = DbUtils.GetDouble(reader, 1);
                                article.SaleMoney = DbUtils.GetDouble(reader, 2);
                                article.DeptCode = DbUtils.GetString(reader, 3);
                                bool isFound = false;
                                for (int i = 0; i < articleDecMoneyList.Count; i++)
                                {
                                    if (article.Inx < articleDecMoneyList[i].Inx)
                                    {
                                        isFound = true;
                                        articleDecMoneyList.Insert(i, article);
                                        break;
                                    }
                                }
                                if (!isFound)
                                    articleDecMoneyList.Add(article);
                            }
                            reader.Close();

                            PosCalculator calculator = new PosCalculator();
                            calculator.BillHead = billHead;
                            calculator.IsToCalcOfferCoupon = true;
                            calculator.PaymentList = paymentList;
                            calculator.ArticleList = articleDecMoneyList;
                            calculator.DbCmd = cmd;

                            if (!calculator.ProcWhenCalcDecMoney())
                            {
                                msg = calculator.ErrorMessage;
                                return false;
                            }
                            decMoney = calculator.TotalDecMoney;

                            DbTransaction dbTrans = conn.BeginTransaction();
                            try
                            {
                                cmd.Transaction = dbTrans;
                                sql.Length = 0;
                                sql.Append("update HYK_XFJL_SP_MBJZ set XSJE_MBJZ = 0,MBJZJE = 0 ");
                                sql.Append("where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                if (MathUtils.DoubleAGreaterThanDoubleB(decMoney, 0))
                                {
                                    foreach (CrmArticle article in articleDecMoneyList)
                                    {
                                        if (article.DecMoneyRuleId > 0)
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_XFJL_SP_MBJZ set ");
                                            sql.Append(" XSJE_MBJZ = ").Append(article.SaleMoneyForDecMoney.ToString("f2"));
                                            sql.Append(" ,MBJZJE = ").Append(article.DecMoney.ToString("f2"));
                                            sql.Append(" where XFJLID = ").Append(serverBillId);
                                            sql.Append("   and INX = ").Append(article.Inx);
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_SP_ZFFS_MBJZ where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if ((calculator.PaymentArticleShareList != null) && MathUtils.DoubleAGreaterThanDoubleB(decMoney, 0))
                                {
                                    foreach (CrmPaymentArticleShare share in calculator.PaymentArticleShareList)
                                    {
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_XFJL_SP_ZFFS_MBJZ(XFJLID,INX,SHZFFSID,BJ_JF,BJ_FQ,YHQID,JE) ");
                                        sql.Append("  values(").Append(serverBillId);
                                        sql.Append(",").Append(share.Article.Inx);
                                        sql.Append(",").Append(share.Payment.PayTypeId);
                                        if (share.Payment.JoinCent)
                                            sql.Append(",1");
                                        else
                                            sql.Append(",0");
                                        if (share.JoinPromOfferCoupon)
                                            sql.Append(",1");
                                        else
                                            sql.Append(",0");
                                        if (share.Payment.CouponType >= 0)
                                            sql.Append(",").Append(share.Payment.CouponType);
                                        else
                                            sql.Append(",null");
                                        sql.Append(",").Append(share.ShareMoney.ToString("f2"));
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                dbTrans.Commit();
                            }
                            catch (Exception e)
                            {
                                dbTrans.Rollback();
                                throw e;
                            }
                        }
                    }
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
            return (msg.Length == 0);
        }

        //准备结账
        public static bool PrepareCheckOutRSaleBill(out string msg, out double billCent, out bool needVipToOfferCoupon, out bool needBuyCent, out string offerCouponVipCode, List<CrmArticle> articleList, List<CrmPaymentArticleShare> couponArticleShareList, List<CrmCouponPayment> payBackCouponList, List<CrmPromOfferCoupon> offerBackCouponList, int serverBillId, List<CrmPayment> paymentList, int payBackCouponVipId, bool couponPaid)
        {
            msg = string.Empty;
            billCent = 0;
            //double offerBackCent = 0;
            needVipToOfferCoupon = false;
            needBuyCent = false;
            offerCouponVipCode = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
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
                    sql.Length = 0;
                    sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.HYID,a.HYID_FQ,a.HYID_TQ,a.XFJLID_OLD,a.JZRQ,a.SCSJ,a.XFRQ_FQ,a.SKYDM,a.JE,a.ZK_HY,a.JF,a.JFBS,a.BSFS,a.HYKNO,a.HYKNO_FQ,b.MDDM from HYK_XFJL a,MDDY b ");
                    sql.Append("where a.XFJLID = ").Append(serverBillId);
                    sql.Append("  and a.MDID = b.MDID");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int billStatus = DbUtils.GetInt(reader, 0);
                        if (billStatus == 1)
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐，不能再准备结帐", serverBillId);
                        else
                        {
                            billHead.CompanyCode = DbUtils.GetString(reader, 1);
                            billHead.StoreId = DbUtils.GetInt(reader, 2);
                            billHead.PosId = DbUtils.GetString(reader, 3);
                            billHead.BillId = DbUtils.GetInt(reader, 4);
                            billHead.BillType = DbUtils.GetInt(reader, 5);
                            billHead.VipId = DbUtils.GetInt(reader, 6);
                            billHead.OfferCouponVipId = DbUtils.GetInt(reader, 7);
                            billHead.PayBackCouponVipId = DbUtils.GetInt(reader, 8);
                            billHead.OriginalServerBillId = DbUtils.GetInt(reader, 9);
                            billHead.AccountDate = DbUtils.GetDateTime(reader, 10);
                            billHead.SaleTime = DbUtils.GetDateTime(reader, 11);
                            billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 12);
                            billHead.Cashier = DbUtils.GetString(reader, 13);
                            billHead.TotalSaleMoney = DbUtils.GetDouble(reader, 14);
                            billHead.TotalVipDiscMoney = DbUtils.GetDouble(reader, 15);
                            billHead.TotalGainedCent = DbUtils.GetDouble(reader, 16);
                            billHead.CentMultiple = DbUtils.GetDouble(reader, 17);
                            billHead.CentMultiMode = DbUtils.GetInt(reader, 18);
                            billHead.VipCode = DbUtils.GetString(reader, 19);
                            billHead.OfferCouponVipCode = DbUtils.GetString(reader, 20);
                            billHead.StoreCode = DbUtils.GetString(reader, 21);
                            billHead.ServerBillId = serverBillId;
                            billHead.PayBackCouponVipId = payBackCouponVipId;
                            billCent = billHead.TotalGainedCent;
                            offerCouponVipCode = billHead.OfferCouponVipCode;
                        }
                        reader.Close();
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select HYID from HYXFJL ");
                        sql.Append("where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            msg = string.Format("该销售单 {0} 在 CRM 中已结帐，不能再准备结帐", serverBillId);
                        else
                            msg = string.Format("该销售单 {0} 在 CRM 中不存在，不能准备结帐", serverBillId);
                        reader.Close();
                    }

                    if (msg.Length == 0)
                    {
                        billHead.TotalPayMoney = 0;
                        billHead.TotalPayCashCardMoney = 0;
                        foreach (CrmPayment payment in paymentList)
                        {
                            if (billHead.BillType == CrmPosData.BillTypeSale)
                            {
                                if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                                {
                                    msg = "销售时支付金额 <= 0";
                                    break;
                                }
                            }
                            else
                            {
                                if (!MathUtils.DoubleASmallerThanDoubleB(payment.PayMoney, 0))
                                {
                                    msg = "退货时支付金额 >= 0";
                                    break;
                                }
                            }
                            billHead.TotalPayMoney = billHead.TotalPayMoney + payment.PayMoney;
                            if (payment.IsCashCard)
                                billHead.TotalPayCashCardMoney = billHead.TotalPayCashCardMoney + payment.PayMoney;

                            sql.Length = 0;
                            sql.Append("select SHZFFSID,ZFFSMC,BJ_JF,BJ_FQ,BJ_MBJZ,YHQID from SHZFFS where SHDM = '").Append(billHead.CompanyCode).Append("' and ZFFSDM = '").Append(payment.PayTypeCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                payment.PayTypeId = DbUtils.GetInt(reader, 0);
                                payment.PayTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                                payment.JoinCent = DbUtils.GetBool(reader, 2);
                                payment.JoinPromOfferCoupon = DbUtils.GetBool(reader, 3);
                                payment.JoinPromDecMoney = DbUtils.GetBool(reader, 4);
                                if (reader.IsDBNull(5))
                                    payment.CouponType = -1;
                                else
                                    payment.CouponType = DbUtils.GetInt(reader, 5);
                            }
                            else
                            {
                                msg = string.Format("收款方式 {0} 还没有上传到 CRM 库", payment.PayTypeCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;
                        }
                        if (billHead.BillType == CrmPosData.BillTypeSale)
                        {
                            foreach (CrmPayment payment in paymentList)
                            {
                                if ((payment.BankCardList != null) && (payment.BankCardList.Count > 0))
                                {
                                    foreach (CrmBankCardPayment bankCard in payment.BankCardList)
                                    {
                                        sql.Length = 0;
                                        sql.Append("select ID,YHID from YHXXITEM ");
                                        sql.Append(" where '").Append(bankCard.BankCardCode).Append("' between CODE1 and CODE2 ");
                                        sql.Append("   and length(CODE1) = ").Append(bankCard.BankCardCode.Length);
                                        sql.Append("   and BJ_TY = 0 ");
                                        sql.Append("order by ID desc ");
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            CrmBankCardCodeScope scope = new CrmBankCardCodeScope();
                                            bankCard.BankCardCodeScopeList.Add(scope);
                                            scope.ScopeId = DbUtils.GetInt(reader, 0);
                                            scope.BankId = DbUtils.GetInt(reader, 1);
                                        }
                                        reader.Close();
                                    }
                                }
                            }
                        }
                    }

                    if (msg.Length > 0)
                        return false;

                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                    bool calcCent = false;
                    bool calcOfferCoupon = false;
                    PosCalculator calculator = null;
                    List<CrmCouponPayment> payBackCouponList2 = null;
                    List<CrmPromOfferCouponCalcItem> offerBackCouponCalcItemList = null;
                    List<CrmPromCentMoneyMultipleCalcItem> centMoneyMultipleCalcItemList = null;
                    if (billHead.OriginalServerBillId == 0)
                    {
                        calcCent = (billHead.VipId > 0) && (!(CrmServerPlatform.Config.NoCentWhenPayCoupon && couponPaid));
                        //calcOfferCoupon = (billHead.BillType == CrmPosData.BillTypeSale) && (billHead.OfferCouponVipId > 0) && (!(CrmServerPlatform.Config.NoOfferCouponWhenPayCoupon && couponPaid));
                        calcOfferCoupon = (billHead.BillType == CrmPosData.BillTypeSale) && (!(CrmServerPlatform.Config.NoOfferCouponWhenPayCoupon && couponPaid));

                        #region 销售或无单退货---取商品列表
                        sql.Length = 0;
                        sql.Append("select INX,SHSPID,SPDM,BMDM,XSSL,XSJE,BJ_JF,BJ_BCJHD,JFJS,BJ_JFBS,BS,JFFBGZ from HYK_XFJL_SP where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmArticle article = new CrmArticle();
                            articleList.Add(article);
                            article.Inx = DbUtils.GetInt(reader, 0);
                            article.ArticleId = DbUtils.GetInt(reader, 1);
                            article.ArticleCode = DbUtils.GetString(reader, 2);
                            article.DeptCode = DbUtils.GetString(reader, 3);
                            article.SaleNum = DbUtils.GetDouble(reader, 4);
                            article.SaleMoney = DbUtils.GetDouble(reader, 5);
                            article.IsNoCent = (!DbUtils.GetBool(reader, 6));
                            article.IsNoProm = DbUtils.GetBool(reader, 7);
                            article.BaseCent = DbUtils.GetDouble(reader, 8);
                            article.CanCentMultiple = DbUtils.GetBool(reader, 9);
                            article.CentMultiple = DbUtils.GetInt(reader, 10);
                            article.CentMoneyMultipleRuleId = DbUtils.GetInt(reader, 11);
                        }
                        reader.Close();
                        #endregion

                        calculator = new PosCalculator();
                        calculator.BillHead = billHead;
                        calculator.IsToCalcOfferCoupon = calcOfferCoupon;
                        calculator.PaymentList = paymentList;
                        calculator.ArticleList = articleList;
                        calculator.DbCmd = cmd;
                        if (!calculator.ProcWhenPrepareCheckout())
                        {
                            msg = calculator.ErrorMessage;
                            return false;
                        }
                        billHead.TotalSaleMoney = billHead.TotalSaleMoney - calculator.TotalDecMoney;
                        if (calcOfferCoupon && (billHead.OfferCouponVipId == 0))
                        {
                            #region 如果有返电子券，但没刷卡，则提示补刷
                            List<int> couponTypes = new List<int>();
                            if ((calculator.OfferCouponCalcItemList != null) && (calculator.OfferCouponCalcItemList.Count > 0))
                            {
                                foreach (CrmPromOfferCouponCalcItem calcItem in calculator.OfferCouponCalcItemList)
                                {
                                    if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))
                                    {
                                        couponTypes.Add(calcItem.CouponType);
                                    }
                                }
                            }
                            if ((calculator.PaymentOfferCouponCalcItemList != null) && (calculator.PaymentOfferCouponCalcItemList.Count > 0))
                            {
                                foreach (CrmPromPaymentOfferCouponCalcItem calcItem in calculator.PaymentOfferCouponCalcItemList)
                                {
                                    if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))
                                    {
                                        couponTypes.Add(calcItem.CouponType);
                                    }
                                }
                            }
                            if (couponTypes.Count > 0)
                            {
                                sql.Length = 0;
                                sql.Append("select count(*) from YHQDEF ");
                                if (couponTypes.Count == 1)
                                    sql.Append(" where YHQID = ").Append(couponTypes[0]);
                                else
                                {
                                    sql.Append(" where YHQID in (").Append(couponTypes[0]);
                                    for (int i = 1; i < couponTypes.Count; i++)
                                    {
                                        sql.Append(",").Append(couponTypes[i]);
                                    }
                                    sql.Append(")");
                                }
                                sql.Append("  and BJ_TS = 0 ");
                                sql.Append("  and BJ_DZYHQ = 1 ");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                    needVipToOfferCoupon = (DbUtils.GetInt(reader, 0) > 0);
                                reader.Close();
                            }
                            #endregion
                        }

                        if (calcCent)
                        {
                            centMoneyMultipleCalcItemList = new List<CrmPromCentMoneyMultipleCalcItem>();
                            PromCalculator.CalcVipCent(out billCent, billHead.CentMultiple, billHead.CentMultiMode, articleList, centMoneyMultipleCalcItemList);
                        }

                        couponArticleShareList.Clear();
                        foreach (CrmPaymentArticleShare share in calculator.PaymentArticleShareList)
                        {
                            if (share.Payment.CouponType >= 0)
                            {
                                couponArticleShareList.Add(share);
                            }
                        }
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select XFJLID from HYK_XFJL where XFJLID = ").Append(billHead.OriginalServerBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        billHead.IsFromBackupTable = (!reader.Read());
                        reader.Close();

                        #region 选单退货---取商品积分列表
                        sql.Length = 0;
                        sql.Append("select INX,JF from HYK_XFJL_SP where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmArticle article = new CrmArticle();
                            articleList.Add(article);
                            article.Inx = DbUtils.GetInt(reader, 0);
                            article.GainedCent = DbUtils.GetDouble(reader, 1);
                        }
                        reader.Close();
                        #endregion

                        #region 选单退货---取退券信息
                        sql.Length = 0;
                        sql.Append("select YHQID,INX,YQJE from HYK_XFJL_SP_YQFT where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPaymentArticleShare share = new CrmPaymentArticleShare();
                            couponArticleShareList.Add(share);
                            share.CouponType = DbUtils.GetInt(reader, 0);
                            share.ArticleInx = DbUtils.GetInt(reader, 1);
                            share.ShareMoney = DbUtils.GetDouble(reader, 2);    //退券分摊金额为负

                        }
                        reader.Close();
                        foreach (CrmPaymentArticleShare share in couponArticleShareList)
                        {
                            bool isFound = false;
                            foreach (CrmCouponPayment payBackCoupon in payBackCouponList)
                            {
                                if (share.CouponType == payBackCoupon.CouponType)
                                {
                                    isFound = true;
                                    payBackCoupon.PayMoney -= share.ShareMoney;
                                    break;
                                }
                            }
                            if (!isFound)
                            {
                                CrmCouponPayment payBackCoupon = new CrmCouponPayment();
                                payBackCouponList.Add(payBackCoupon);
                                payBackCoupon.CouponType = share.CouponType;
                                payBackCoupon.PayMoney = -share.ShareMoney; //退券分摊金额为负,退券金额为正
                                payBackCoupon.VipId = billHead.PayBackCouponVipId;
                            }
                        }

                        #endregion

                        if (billHead.OfferCouponVipId > 0)  //退货时不扣纸券
                        {
                            #region 选单退货---取扣券信息
                            offerBackCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                            sql.Length = 0;
                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                sql.Append("select CXID,YHQID,YHQFFGZID,XFLJFQFS,BMDM,MDFWDM_FQ,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,THJE,SYXFJE_NEW,KQJE,CXHDBH from HYTHJL_KQ where XFJLID = ").Append(serverBillId);
                            else
                                sql.Append("select CXID,YHQID,YHQFFGZID,XFLJFQFS,BMDM,MDFWDM_FQ,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,THJE,SYXFJE_NEW,KQJE from HYTHJL_KQ where XFJLID = ").Append(serverBillId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                                offerBackCouponCalcItemList.Add(calcItem);
                                calcItem.PromId = DbUtils.GetInt(reader, 0);
                                calcItem.CouponType = DbUtils.GetInt(reader, 1);
                                calcItem.RuleId = DbUtils.GetInt(reader, 2);
                                calcItem.AddupSaleMoneyType = DbUtils.GetInt(reader, 3);
                                calcItem.DeptCode = DbUtils.GetString(reader, 4);
                                calcItem.OfferStoreScope = DbUtils.GetString(reader, 5);
                                calcItem.SaleMoneyBefore = DbUtils.GetDouble(reader, 6);
                                calcItem.SaleMoneyUsedBefore = DbUtils.GetDouble(reader, 7);
                                calcItem.OfferCouponMoneyBefore = DbUtils.GetDouble(reader, 8);
                                calcItem.SaleMoney = DbUtils.GetDouble(reader, 9);
                                calcItem.SaleMoneyUsed = DbUtils.GetDouble(reader, 10);
                                calcItem.OfferCouponMoney = DbUtils.GetDouble(reader, 11);  //扣券金额为正
                                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                {
                                    if (calcItem.PromId == 0)
                                    {
                                        calcItem.PromIdIsBH = true;
                                        calcItem.PromId = DbUtils.GetInt(reader, 12);
                                    }
                                }
                            }
                            reader.Close();
                            #endregion
                            if (offerBackCouponCalcItemList.Count > 0)
                            {
                                PromCalculator.GetPromOfferCouponDataBefore(offerBackCouponCalcItemList, billHead.OfferCouponVipId, DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate), billHead.OriginalServerBillId, billHead.IsFromBackupTable, cmd);
                                PromCalculator.CalcPromOfferBackCoupon(offerBackCouponCalcItemList, true);

                                #region 选单退货---合计扣券
                                foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                {
                                    if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))
                                    {
                                        bool isFound = false;
                                        foreach (CrmPromOfferCoupon offerBackCoupon in offerBackCouponList)
                                        {
                                            if ((calcItem.CouponType == offerBackCoupon.CouponType) && (calcItem.PromId == offerBackCoupon.PromId))
                                            {
                                                isFound = true;
                                                offerBackCoupon.OfferMoney += calcItem.OfferCouponMoney;
                                                break;
                                            }
                                        }
                                        if (!isFound)
                                        {
                                            CrmPromOfferCoupon offerBackCoupon = new CrmPromOfferCoupon();
                                            offerBackCouponList.Add(offerBackCoupon);
                                            offerBackCoupon.CouponType = calcItem.CouponType;
                                            offerBackCoupon.PromId = calcItem.PromId;
                                            offerBackCoupon.PromIdIsBH = calcItem.PromIdIsBH;
                                            offerBackCoupon.OfferMoney = calcItem.OfferCouponMoney;
                                        }
                                    }
                                }
                                #endregion

                                if (offerBackCouponList.Count > 0)
                                {
                                    payBackCouponList2 = new List<CrmCouponPayment>();
                                    foreach (CrmPromOfferCoupon offerBackCoupon in offerBackCouponList)
                                    {
                                        #region 选单退货---取所扣券的名称，有效期，使用门店范围
                                        sql.Length = 0;
                                        sql.Append("select a.FS_YQMDFW,a.YHQMC,b.YHQSYJSRQ from YHQDEF a, YHQDEF_CXHD b ");
                                        sql.Append(" where a.YHQID = b.YHQID ");
                                        sql.Append("   and a.YHQID = ").Append(offerBackCoupon.CouponType);
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013 && offerBackCoupon.PromIdIsBH)
                                        {
                                            sql.Append("   and b.CXHDBH = ").Append(offerBackCoupon.PromId);
                                            sql.Append("   and b.SHDM = '").Append(billHead.CompanyCode).Append("'");
                                        }
                                        else
                                            sql.Append("   and b.CXID = ").Append(offerBackCoupon.PromId);
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            switch (DbUtils.GetInt(reader, 0))
                                            {
                                                case 2:
                                                    offerBackCoupon.PayStoreScope = billHead.CompanyCode;
                                                    break;
                                                case 3:
                                                    offerBackCoupon.PayStoreScope = billHead.StoreCode;
                                                    break;
                                                default:
                                                    //offerBackCoupon.PayStoreScope = string.Empty;
                                                    offerBackCoupon.PayStoreScope = " ";
                                                    break;
                                            }
                                            offerBackCoupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                                            offerBackCoupon.ValidDate = DbUtils.GetDateTime(reader, 2).Date;

                                            foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                            {
                                                if ((calcItem.CouponType == offerBackCoupon.CouponType) && (calcItem.PromId == offerBackCoupon.PromId))
                                                {
                                                    calcItem.PayStoreScope = offerBackCoupon.PayStoreScope;
                                                }
                                            }
                                        }

                                        #endregion

                                        #region 选单退货---计算扣券差额
                                        offerBackCoupon.OfferBackDifference = offerBackCoupon.OfferMoney;
                                        cmd.Parameters.Clear();
                                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", offerBackCoupon.ValidDate);
                                        sql.Length = 0;
                                        sql.Append("select JE from HYK_YHQZH ");
                                        sql.Append("where YHQID = ").Append(offerBackCoupon.CouponType);
                                        sql.Append("  and HYID = ").Append(billHead.OfferCouponVipId);
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                            sql.Append("  and CXID = ").Append(offerBackCoupon.PromId);
                                        DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                        if (offerBackCoupon.PayStoreScope.Length == 0)
                                            sql.Append("  and MDFWDM = ' '");
                                        else
                                            sql.Append("  and MDFWDM = '").Append(offerBackCoupon.PayStoreScope).Append("'");
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                            offerBackCoupon.Balance = DbUtils.GetDouble(reader, 0);
                                        reader.Close();
                                        cmd.Parameters.Clear();
                                        if (MathUtils.DoubleASmallerThanDoubleB(offerBackCoupon.Balance, offerBackCoupon.OfferBackDifference))
                                            offerBackCoupon.OfferBackDifference -= offerBackCoupon.Balance;
                                        else
                                            offerBackCoupon.OfferBackDifference = 0;

                                        if (MathUtils.DoubleAGreaterThanDoubleB(offerBackCoupon.OfferBackDifference, 0) && (billHead.BillType == CrmPosData.BillTypeReturn))
                                        {
                                            for (int i = payBackCouponList.Count - 1; i >= 0; i--)
                                            {
                                                CrmCouponPayment payBackCoupon = payBackCouponList[i];
                                                if (payBackCoupon.CouponType == offerBackCoupon.CouponType)
                                                {
                                                    CrmCouponPayment payBackCoupon2 = new CrmCouponPayment();
                                                    payBackCouponList2.Add(payBackCoupon2);
                                                    payBackCoupon2.CouponType = payBackCoupon.CouponType;
                                                    payBackCoupon2.VipId = billHead.OfferCouponVipId;
                                                    payBackCoupon2.PromId = offerBackCoupon.PromId;
                                                    payBackCoupon2.StoreScope = offerBackCoupon.PayStoreScope;
                                                    payBackCoupon2.ValidDate = offerBackCoupon.ValidDate;
                                                    if (MathUtils.DoubleAGreaterThanDoubleB(payBackCoupon.PayMoney, offerBackCoupon.OfferBackDifference))
                                                    {
                                                        payBackCoupon2.PayMoney = offerBackCoupon.OfferBackDifference;
                                                        payBackCoupon.PayMoney -= payBackCoupon2.PayMoney;
                                                        offerBackCoupon.OfferBackDifference = 0;
                                                    }
                                                    else
                                                    {
                                                        payBackCoupon2.PayMoney = payBackCoupon.PayMoney;
                                                        offerBackCoupon.OfferBackDifference -= payBackCoupon2.PayMoney;
                                                        payBackCoupon.PayMoney = 0;
                                                    }
                                                    if (MathUtils.DoubleAEuqalToDoubleB(payBackCoupon.PayMoney, 0))
                                                        payBackCouponList.RemoveAt(i);

                                                    break;
                                                }
                                            }
                                        }
                                        offerBackCoupon.ActualOfferMoney = (offerBackCoupon.OfferMoney - offerBackCoupon.OfferBackDifference);
                                        if (MathUtils.DoubleAGreaterThanDoubleB(offerBackCoupon.ActualOfferMoney, 0))
                                        {
                                            double temp = offerBackCoupon.ActualOfferMoney;
                                            foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                            {
                                                if ((calcItem.CouponType == offerBackCoupon.CouponType) && (calcItem.PromId == offerBackCoupon.PromId))
                                                {
                                                    if (MathUtils.DoubleAEuqalToDoubleB(temp - calcItem.OfferCouponMoney, 0))
                                                    {
                                                        calcItem.ActualOfferMoney += calcItem.OfferCouponMoney;
                                                        temp -= calcItem.OfferCouponMoney;
                                                    }
                                                    else
                                                    {
                                                        calcItem.ActualOfferMoney += temp;
                                                        temp = 0;
                                                    }
                                                }
                                                if (MathUtils.DoubleAEuqalToDoubleB(temp, 0))
                                                    break;
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }
                        #region 选单退货---取所退券的名称，使用门店范围，有效期
                        foreach (CrmCouponPayment payBackCoupon in payBackCouponList)
                        {
                            bool isFound = false;
                            foreach (CrmPromOfferCoupon offerBackCoupon in offerBackCouponList)
                            {
                                if ((payBackCoupon.CouponType == offerBackCoupon.CouponType) && (offerBackCoupon.ValidDate.Date.CompareTo(serverTime.Date) >= 0))
                                {
                                    isFound = true;
                                    if (offerBackCoupon.Coupon != null)
                                        payBackCoupon.CouponTypeName = offerBackCoupon.CouponTypeName;
                                    payBackCoupon.StoreScope = offerBackCoupon.PayStoreScope;
                                    payBackCoupon.PromId = offerBackCoupon.PromId;
                                    payBackCoupon.ValidDate = offerBackCoupon.ValidDate.Date;
                                    break;
                                }
                            }

                            if (!isFound)
                            {
                                sql.Length = 0;
                                sql.Append("select YXQTS,YHQMC,FS_YQMDFW from YHQDEF where YHQID = ").Append(payBackCoupon.CouponType);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    payBackCoupon.ValidDaysToBack = DbUtils.GetInt(reader, 0);
                                    payBackCoupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                                    switch (DbUtils.GetInt(reader, 2))
                                    {
                                        case 2:
                                            payBackCoupon.StoreScope = billHead.CompanyCode;
                                            break;
                                        case 3:
                                            payBackCoupon.StoreScope = billHead.StoreCode;
                                            break;
                                        default:
                                            payBackCoupon.StoreScope = string.Empty;
                                            break;
                                    }
                                }
                                reader.Close();
                                sql.Length = 0;
                                sql.Append("select max(b.CXID),max(b.JSRQ) from HYK_JYCL a,HYK_JYCLITEM_YHQ b ");//可能CXID和JSRQ不匹配，先这么着吧 zxc
                                sql.Append(" where a.JYID = b.JYID ");
                                sql.Append("   and a.XFJLID = ").Append(billHead.OriginalServerBillId);
                                sql.Append("   and a.JYZT = ").Append(CrmPosData.TransStatusConfirmed);
                                sql.Append("   and b.YHQID = ").Append(payBackCoupon.CouponType);
                                sql.Append("   and a.BJ_YHQ = 1 ");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    payBackCoupon.PromId = DbUtils.GetInt(reader, 0);
                                    payBackCoupon.ValidDate = DbUtils.GetDateTime(reader, 1);
                                }
                                reader.Close();
                                if (payBackCoupon.ValidDate.Date.CompareTo(serverTime.Date) < 0)
                                    payBackCoupon.ValidDate = serverTime.Date.AddDays(payBackCoupon.ValidDaysToBack);
                            }
                        }
                        #endregion

                        if ((payBackCouponList2 != null) && (payBackCouponList2.Count > 0))
                        {
                            #region 选单退货---合并退券
                            foreach (CrmCouponPayment payBackCoupon2 in payBackCouponList2)
                            {
                                bool isFound = false;
                                foreach (CrmCouponPayment payBackCoupon in payBackCouponList)
                                {
                                    if ((payBackCoupon.CouponType == payBackCoupon2.CouponType) && (payBackCoupon.VipId == payBackCoupon2.VipId) && (payBackCoupon.PromId == payBackCoupon2.PromId) && (payBackCoupon.ValidDate.Date.CompareTo(payBackCoupon2.ValidDate.Date) == 0))
                                    {
                                        isFound = true;
                                        payBackCoupon.PayMoney += payBackCoupon2.PayMoney;
                                    }
                                }
                                if (!isFound)
                                    payBackCouponList.Add(payBackCoupon2);
                            }
                            #endregion
                        }
                    }

                    if (MathUtils.DoubleASmallerThanDoubleB(billCent, 0) && (CrmServerPlatform.Config.NoNegativeValidCent || CrmServerPlatform.Config.NoNegativeYearCent))
                    {
                        double vipValidCent = 0;
                        double vipYearCent = 0;
                        sql.Length = 0;
                        sql.Append("select WCLJF,BNLJJF from HYK_JFZH where HYID = ").Append(billHead.VipId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            vipValidCent = DbUtils.GetDouble(reader, 0);
                            vipYearCent = DbUtils.GetDouble(reader, 1);
                        }
                        reader.Close();
                        if (CrmServerPlatform.Config.NoNegativeValidCent && MathUtils.DoubleASmallerThanDoubleB(billCent + vipValidCent, 0))
                            needBuyCent = true;
                        if (CrmServerPlatform.Config.NoNegativeYearCent && MathUtils.DoubleASmallerThanDoubleB(billCent + vipYearCent, 0))
                            needBuyCent = true;
                        if (needBuyCent)
                            return true;
                    }
                    int seqCount = 0;
                    int seqBegin = 0;
                    int seqInx = 0;
                    if (billHead.OriginalServerBillId > 0)
                    {
                        if (payBackCouponList != null)
                            seqCount += payBackCouponList.Count;
                        if (offerBackCouponList != null)
                            seqCount += offerBackCouponList.Count;
                        if (seqCount > 0)
                        {
                            string dbSysName = DbUtils.GetDbSystemName(conn);
                            if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                                seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                        }
                    }
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;
                        sql.Length = 0;
                        sql.Append("update HYK_XFJL set STATUS = 2");
                        sql.Append(",CZJE = ").Append(billHead.TotalPayCashCardMoney.ToString("f2"));
                        if (billHead.OriginalServerBillId == 0)
                        {
                            sql.Append(",JF = ").Append(billCent.ToString("f4"));
                        }
                        sql.Append(" where XFJLID = ").Append(serverBillId);
                        sql.Append("   and STATUS in (0,2) ");
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            cmd.Parameters.Clear();

                            #region 保存 HYK_XFJL_ZFFS
                            sql.Length = 0;
                            sql.Append("delete from HYK_XFJL_ZFFS where XFJLID = ").Append(serverBillId);
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();

                            for (int inx = 0; inx < paymentList.Count(); inx++)
                            {
                                CrmPayment payment = paymentList[inx];
                                sql.Length = 0;
                                sql.Append("insert into HYK_XFJL_ZFFS(XFJLID,INX,ZFFSID,ZFFSDM,BJ_JF,BJ_FQ,YHQID,JE )");
                                sql.Append(" values(").Append(serverBillId);
                                sql.Append(",").Append(inx);
                                sql.Append(",").Append(payment.PayTypeId);
                                sql.Append(",'").Append(payment.PayTypeCode);
                                if (payment.JoinCent)
                                    sql.Append("',1");
                                else
                                    sql.Append("',0");
                                if (payment.JoinPromOfferCoupon)
                                    sql.Append(",1");
                                else
                                    sql.Append(",0");
                                if (payment.CouponType >= 0)
                                    sql.Append(",").Append(payment.CouponType);
                                else
                                    sql.Append(",null");
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            #endregion

                            #region 保存 HYK_XFJL_YHKZF
                            sql.Length = 0;
                            sql.Append("delete from HYK_XFJL_YHKZF where XFJLID = ").Append(serverBillId);
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                            for (int inx = 0; inx < paymentList.Count(); inx++)
                            {
                                CrmPayment payment = paymentList[inx];
                                if ((payment.BankCardList != null) && (payment.BankCardList.Count > 0))
                                {
                                    foreach (CrmBankCardPayment bankCard in payment.BankCardList)
                                    {
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_XFJL_YHKZF(XFJLID,INX,BANK_KH,BANK_ID,SHZFFSID,JE )");
                                        sql.Append(" values(").Append(serverBillId);
                                        sql.Append(",").Append(bankCard.Inx);
                                        sql.Append(",'").Append(bankCard.BankCardCode);
                                        sql.Append("',").Append(0);   //bankCard.BankId
                                        sql.Append(",").Append(payment.PayTypeId);
                                        sql.Append(",").Append(bankCard.PayMoney.ToString("f2"));
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            #endregion

                            if (billHead.OriginalServerBillId == 0)
                            {
                                #region 销售或无单退货---删除 HYK_XFJL_SP_ZFFS，HYK_XFJL_SP_YQFT, HYK_XFJL_SP_ZFFS_FQ, HYK_XFJL_ZFFS_FQ, HYK_XFJL_ZSLP, HYK_XFJL_FQ
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_SP_ZFFS where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_SP_YQFT where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_SP_ZFFS_FQ where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_ZFFS_FQ where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                //sql.Length = 0;
                                //sql.Append("delete from HYK_XFJL_SP_FQ where XFJLID = ").Append(serverBillId).Append("  and DJLX = 2");
                                //cmd.CommandText = sql.ToString();
                                //cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_ZSLP where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_ZFFS_ZSLP where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJL_FQ where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                #endregion

                                if ((calculator.PaymentArticleShareList != null) && (calculator.PaymentArticleShareList.Count > 0))
                                {
                                    #region 销售或无单退货---取满减时分摊 更新 HYK_XFJL_SP_ZFFS
                                    bool existShareWhenCalcDecMoney = false;
                                    foreach (CrmPaymentArticleShare share in calculator.PaymentArticleShareList)
                                    {
                                        if (share.SharedWhenCalcDecMoney)
                                        {
                                            existShareWhenCalcDecMoney = true;
                                            break;
                                        }
                                    }
                                    if (existShareWhenCalcDecMoney)
                                    {
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_XFJL_SP_ZFFS(XFJLID,INX,SHZFFSID,BJ_JF,BJ_FQ,YHQID,JE) ");
                                        sql.Append(" select XFJLID,INX,SHZFFSID,BJ_JF,BJ_FQ,YHQID,JE ");
                                        sql.Append(" from HYK_XFJL_SP_ZFFS_MBJZ ");
                                        sql.Append(" where XFJLID = ").Append(serverBillId);
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                    #endregion

                                    foreach (CrmPaymentArticleShare share in calculator.PaymentArticleShareList)
                                    {
                                        if (!share.SharedWhenCalcDecMoney)
                                        {
                                            #region 销售或无单退货---本次分摊 更新 HYK_XFJL_SP_ZFFS
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_XFJL_SP_ZFFS(XFJLID,INX,SHZFFSID,BJ_JF,BJ_FQ,YHQID,JE)");
                                            sql.Append(" values(").Append(serverBillId);
                                            sql.Append(",").Append(share.Article.Inx);
                                            sql.Append(",").Append(share.Payment.PayTypeId);
                                            if (share.Payment.JoinCent)
                                                sql.Append(",1");
                                            else
                                                sql.Append(",0");
                                            if (share.JoinPromOfferCoupon)
                                                sql.Append(",1");
                                            else
                                                sql.Append(",0");
                                            if (share.Payment.CouponType >= 0)
                                                sql.Append(",").Append(share.Payment.CouponType);
                                            else
                                                sql.Append(",null");
                                            sql.Append(",").Append(share.ShareMoney.ToString("f2"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                            #endregion
                                        }
                                    }

                                    #region 销售或无单退货---更新 HYK_XFJL_SP_YQFT
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_XFJL_SP_YQFT(XFJLID, INX, YHQID, BMDM, SPDM, SHSPID, YHQSYDBH, CXID, YHQSYGZID, BJ_FQ, XSJE, XSJE_YQ, YQJE) ");
                                    sql.Append(" select a.XFJLID,a.INX,a.YHQID,b.BMDM,b.SPDM,b.SHSPID,null,null,null,0,b.XSJE,0,a.JE ");
                                    sql.Append(" from HYK_XFJL_SP_ZFFS a,HYK_XFJL_SP b ");
                                    sql.Append(" where a.XFJLID = ").Append(serverBillId);
                                    sql.Append("  and a.XFJLID = b.XFJLID ");
                                    sql.Append("  and a.INX = b.INX ");
                                    sql.Append("  and a.YHQID >= 0 ");
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        sql.Length = 0;

                                        switch (DbUtils.GetDbSystemName(conn))
                                        {
                                            case DbUtils.SybaseDbSystemName:
                                                sql.Append("update HYK_XFJL_SP_YQFT set a.YHQSYDBH = b.YHQSYDBH,a.CXID = b.CXID,a.YHQSYGZID = b.YHQSYGZID,a.XSJE_YQ = round(b.XSJE_YQ * (a.XSJE / b.XSJE),2)");
                                                sql.Append(" from HYK_XFJL_SP_YQFT a,HYK_XFJL_SP_YQJC b");
                                                sql.Append(" where a.XFJLID = ").Append(serverBillId);
                                                sql.Append("  and a.XFJLID = b.XFJLID");
                                                sql.Append("  and a.YHQID = b.YHQID");
                                                sql.Append("  and a.BMDM = b.BMDM");
                                                sql.Append("  and a.SPDM = b.SPDM");
                                                sql.Append("  and b.XSJE <> 0");
                                                break;
                                            case DbUtils.OracleDbSystemName:
                                                sql.Append("update HYK_XFJL_SP_YQFT set (YHQSYDBH,CXID,YHQSYGZID,XSJE_YQ) = ");
                                                sql.Append(" (select YHQSYDBH,CXID,YHQSYGZID,round(b.XSJE_YQ * (HYK_XFJL_SP_YQFT.XSJE / b.XSJE),2) from HYK_XFJL_SP_YQJC b where HYK_XFJL_SP_YQFT.XFJLID = b.XFJLID and HYK_XFJL_SP_YQFT.YHQID = b.YHQID and HYK_XFJL_SP_YQFT.INX = b.INX and HYK_XFJL_SP_YQFT.XFJLID = ").Append(serverBillId).Append(") ");
                                                sql.Append(" where exists(select * from HYK_XFJL_SP_YQJC b where HYK_XFJL_SP_YQFT.XFJLID = b.XFJLID and HYK_XFJL_SP_YQFT.YHQID = b.YHQID and HYK_XFJL_SP_YQFT.INX = b.INX and HYK_XFJL_SP_YQFT.XFJLID = ").Append(serverBillId).Append(") ");
                                                break;
                                        }
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();

                                    }
                                    #endregion
                                }

                                foreach (CrmArticle article in articleList)
                                {
                                    #region 销售或无单退货---更新 HYK_XFJL_SP，HYK_XFJL_SP_FQ
                                    sql.Length = 0;
                                    sql.Append("update HYK_XFJL_SP set ");
                                    sql.Append(" XSJE_FQ = ").Append(article.SaleMoneyForOfferCoupon.ToString("f2"));
                                    sql.Append(" ,XSJE_JF = ").Append(article.SaleMoneyForCent.ToString("f2"));
                                    sql.Append(" ,JF = ").Append(article.GainedCent.ToString("f4"));
                                    sql.Append(" where XFJLID = ").Append(serverBillId);
                                    sql.Append("   and INX = ").Append(article.Inx);
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                    sql.Length = 0;
                                    sql.Append("update HYK_XFJL_SP_FQ set FQJE = 0 ");
                                    sql.Append(" ,XSJE_FQ = ").Append(article.SaleMoneyForOfferCoupon.ToString("f2"));
                                    sql.Append(" where XFJLID = ").Append(serverBillId);
                                    sql.Append("   and INX = ").Append(article.Inx);
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                    #endregion
                                }

                                sql.Length = 0;
                                sql.Append("delete from HYK_XFJFBS where XFJLID = ").Append(serverBillId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                if ((centMoneyMultipleCalcItemList != null) && (centMoneyMultipleCalcItemList.Count > 0))
                                {
                                    foreach (CrmPromCentMoneyMultipleCalcItem calcItem in centMoneyMultipleCalcItemList)
                                    {
                                        if (calcItem.Ok)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_XFJFBS(XFJLID,GZID,XFJE_JF,JF,XFJE_JF_OLD,JF_OLD) ");
                                            sql.Append("  values(").Append(serverBillId);
                                            sql.Append(",").Append(calcItem.RuleId);
                                            sql.Append(",").Append(calcItem.SaleMoney.ToString("f2"));
                                            sql.Append(",").Append(calcItem.GainedCent.ToString("f4"));
                                            sql.Append(",0,0");
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                                if (calcOfferCoupon)
                                {
                                    if (calculator.OfferCouponCalcItemList.Count > 0)
                                    {
                                        #region 销售返券---更新 HYK_XFJL_SP_FQ的FQJE
                                        foreach (CrmPromOfferCouponCalcItem calcItem in calculator.OfferCouponCalcItemList)
                                        {
                                            if ((calcItem.AddupSaleMoneyType == 0) && (calcItem.ArticleOfferCouponMoneyList != null))
                                            {
                                                foreach (CrmArticleOfferCouponMoney articleOfferMoney in calcItem.ArticleOfferCouponMoneyList)
                                                {
                                                    sql.Length = 0;
                                                    sql.Append("update HYK_XFJL_SP_FQ set FQJE = ").Append(articleOfferMoney.OfferMoney.ToString("f2"));
                                                    sql.Append(" where XFJLID = ").Append(serverBillId);
                                                    sql.Append("   and INX = ").Append(articleOfferMoney.Article.Inx);
                                                    sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                                    cmd.CommandText = sql.ToString();
                                                    cmd.ExecuteNonQuery();
                                                }
                                            }

                                        }
                                        #endregion

                                        #region 销售返券---更新 HYK_XFJL_FQ, HYK_XFJL_ZSLP
                                        foreach (CrmPromOfferCouponCalcItem calcItem in calculator.OfferCouponCalcItemList)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_XFJL_FQ(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,BMDM,DJLX,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,ZXFJE,SYXFJE,FQJE,FQJE_WX) ");
                                            sql.Append("  values(").Append(serverBillId);
                                            sql.Append(",").Append(calcItem.PromId);
                                            sql.Append(",").Append(calcItem.CouponType);
                                            sql.Append(",").Append(calcItem.RuleId);
                                            sql.Append(",").Append(calcItem.AddupSaleMoneyType);
                                            if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                                                sql.Append(",'").Append(calcItem.OfferStoreScope);
                                            else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                                                sql.Append(",'").Append(calcItem.DeptCode);
                                            else
                                                sql.Append(",'").Append(" ");
                                            sql.Append("',0");
                                            sql.Append(",").Append(calcItem.SaleMoneyBefore.ToString("f2"));
                                            sql.Append(",").Append(calcItem.SaleMoneyUsedBefore.ToString("f2"));
                                            sql.Append(",").Append(calcItem.OfferCouponMoneyBefore.ToString("f2"));
                                            sql.Append(",").Append(calcItem.SaleMoney.ToString("f2"));
                                            sql.Append(",").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                            sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                            if (((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2)) && (calcItem.GiftCode.Length > 0))
                                            {
                                                sql.Length = 0;
                                                sql.Append("insert into HYK_XFJL_ZSLP(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,BMDM,LPDM,LPSL) ");
                                                sql.Append("  values(").Append(serverBillId);
                                                sql.Append(",").Append(calcItem.PromId);
                                                sql.Append(",").Append(calcItem.CouponType);
                                                sql.Append(",").Append(calcItem.RuleId);
                                                sql.Append(",").Append(calcItem.AddupSaleMoneyType);
                                                if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                                                    sql.Append(",'").Append(calcItem.OfferStoreScope);
                                                else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                                                    sql.Append(",'").Append(calcItem.DeptCode);
                                                else
                                                    sql.Append(",'").Append(" ");
                                                sql.Append("','").Append(calcItem.GiftCode).Append("'");
                                                sql.Append(",").Append(((int)calcItem.OfferCouponMoney).ToString());
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        #endregion
                                    }
                                    if (calculator.PaymentOfferCouponArticleList.Count > 0)
                                    {
                                        #region 销售返券---更新 HYK_XFJL_SP_ZFFS_FQ
                                        for (int i = 0; i < calculator.PaymentOfferCouponArticleList.Count; i++)
                                        {
                                            CrmPromPaymentOfferCouponArticle paymentOfferCouponArticle = calculator.PaymentOfferCouponArticleList[i];
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_XFJL_SP_ZFFS_FQ(XFJLID,SPINX,INX,SHZFFSID,BANK_ZFINX,BANK_ID,BANK_KH,YHQID,SHSPID,SPDM,BMDM,XSJE,CXID,YHQFFDBH,YHQFFGZID) ");
                                            sql.Append("  values(").Append(serverBillId);
                                            sql.Append(",").Append(paymentOfferCouponArticle.Article.Inx);
                                            sql.Append(",").Append(i);
                                            sql.Append(",").Append(paymentOfferCouponArticle.Payment.PayTypeId);
                                            if (paymentOfferCouponArticle.BankCardPayment == null)
                                            {
                                                sql.Append(",0,0,' '");
                                            }
                                            else
                                            {
                                                sql.Append(",").Append(paymentOfferCouponArticle.BankCardPayment.Inx);
                                                sql.Append(",").Append(paymentOfferCouponArticle.BankId);
                                                sql.Append(",'").Append(paymentOfferCouponArticle.BankCardPayment.BankCardCode).Append("'");
                                            }
                                            sql.Append(",").Append(paymentOfferCouponArticle.CouponType);
                                            sql.Append(",").Append(paymentOfferCouponArticle.Article.ArticleId);
                                            sql.Append(",'").Append(paymentOfferCouponArticle.Article.ArticleCode);
                                            sql.Append("','").Append(paymentOfferCouponArticle.Article.DeptCode);
                                            sql.Append("',").Append(paymentOfferCouponArticle.PayMoney.ToString("f2"));
                                            sql.Append(",").Append(paymentOfferCouponArticle.PromId);
                                            sql.Append(",").Append(paymentOfferCouponArticle.RuleBillId);
                                            sql.Append(",").Append(paymentOfferCouponArticle.PromId);
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                        #endregion
                                    }
                                    if (calculator.PaymentOfferCouponCalcItemList.Count > 0)
                                    {
                                        #region 销售返券---更新 HYK_XFJL_ZFFS_FQ, HYK_XFJL_ZFFS_ZSLP
                                        foreach (CrmPromPaymentOfferCouponCalcItem calcItem in calculator.PaymentOfferCouponCalcItemList)
                                        {
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_XFJL_ZFFS_FQ(XFJLID,CXID,YHQID,YHQFFGZID,SHZFFSID,BANK_ZFINX,BANK_ID,BANK_KH,ZXFJE,SYXFJE,FQJE,FQJE_SJ) ");
                                            sql.Append("  values(").Append(serverBillId);
                                            sql.Append(",").Append(calcItem.PromId);
                                            sql.Append(",").Append(calcItem.CouponType);
                                            sql.Append(",").Append(calcItem.RuleId);
                                            sql.Append(",").Append(calcItem.PayTypeId);
                                            sql.Append(",").Append(calcItem.BankCardInx);
                                            sql.Append(",").Append(calcItem.BankId);
                                            sql.Append(",'").Append(calcItem.BankCardCode);
                                            sql.Append("',").Append(calcItem.PayMoney.ToString("f2"));
                                            sql.Append(",").Append(calcItem.PayMoneyUsed.ToString("f2"));
                                            sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                            if ((calcItem.SpecialType != 0) && (calcItem.GiftCode.Length > 0))
                                            {
                                                sql.Length = 0;
                                                sql.Append("insert into HYK_XFJL_ZFFS_ZSLP(XFJLID,CXID,YHQID,YHQFFGZID,SHZFFSID,BANK_ZFINX,LPDM,BANK_ID,BANK_KH,LPSL) ");
                                                sql.Append("  values(").Append(serverBillId);
                                                sql.Append(",").Append(calcItem.PromId);
                                                sql.Append(",").Append(calcItem.CouponType);
                                                sql.Append(",").Append(calcItem.RuleId);
                                                sql.Append(",").Append(calcItem.PayTypeId);
                                                sql.Append(",").Append(calcItem.BankCardInx);
                                                sql.Append(",'").Append(calcItem.GiftCode).Append("'");
                                                sql.Append(",").Append(calcItem.BankId);
                                                sql.Append(",'").Append(calcItem.BankCardCode);
                                                sql.Append("',").Append(((int)calcItem.OfferCouponMoney).ToString());
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                foreach (CrmCouponPayment payment in payBackCouponList)
                                {
                                    UpdateVipCouponAccount(out msg, cmd, sql, 7,
                                                            false,
                                                            false,
                                                            payment.VipId,
                                                            payment.CouponType,
                                                            payment.PromId,
                                                            payment.ValidDate,
                                                            payment.StoreScope,
                                                            -payment.PayMoney,
                                                            out payment.Balance,
                                                            seqBegin + seqInx++,
                                                            0,
                                                            billHead.StoreId,
                                                            billHead.PosId,
                                                            billHead.BillId,
                                                            serverTime,
                                                            billHead.AccountDate,
                                                            billHead.Cashier,
                                                            "前台选单退货退券");
                                    if (msg.Length > 0)
                                    {
                                        dbTrans.Rollback();
                                        return false;
                                    }

                                    #region 选单退货---退券 更新 HYTHJL_TQ
                                    cmd.Parameters.Clear();
                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", payment.ValidDate);
                                    sql.Length = 0;
                                    sql.Append("insert into HYTHJL_TQ(XFJLID,HYID,YHQID,CXID,JSRQ,MDFWDM,TQJE) ");
                                    sql.Append(" values(").Append(billHead.ServerBillId);
                                    sql.Append(",").Append(payment.VipId);
                                    sql.Append(",").Append(payment.CouponType);
                                    sql.Append(",").Append(payment.PromId);
                                    DbUtils.SpellSqlParameter(conn, sql, ",", "JSRQ", "");
                                    if (payment.StoreScope.Length == 0)
                                        sql.Append(",' '");
                                    else
                                        sql.Append(",'").Append(payment.StoreScope).Append("'");
                                    sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                    #endregion
                                }

                                if (billHead.OfferCouponVipId > 0)
                                {
                                    foreach (CrmPromOfferCoupon offerCoupon in offerBackCouponList)
                                    {
                                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                                        {
                                            UpdateVipCouponAccount(out msg, cmd, sql, 10,
                                                                false,
                                                                false,
                                                                billHead.OfferCouponVipId,
                                                                offerCoupon.CouponType,
                                                                (CrmServerPlatform.Config.IsUpgradedProject2013 && offerCoupon.PromIdIsBH ? 0 : offerCoupon.PromId),
                                                                offerCoupon.ValidDate,
                                                                offerCoupon.PayStoreScope,
                                                                -offerCoupon.ActualOfferMoney,
                                                                out offerCoupon.Balance,
                                                                seqBegin + seqInx++,
                                                                0,
                                                                billHead.StoreId,
                                                                billHead.PosId,
                                                                billHead.BillId,
                                                                serverTime,
                                                                billHead.AccountDate,
                                                                billHead.Cashier,
                                                                "前台退货扣券");
                                            if (msg.Length > 0)
                                            {
                                                dbTrans.Rollback();
                                                return false;
                                            }
                                        }
                                    }
                                    if (offerBackCouponCalcItemList.Count > 0)
                                    {
                                        foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                        {
                                            if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBill) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept))
                                            {
                                                #region 选单退货---扣券 更新 原单 HYK_XFJL_FQ
                                                sql.Length = 0;
                                                if (billHead.IsFromBackupTable)
                                                    sql.Append("update HYXFJL_FQ set THJE = THJE + ").Append(calcItem.SaleMoney.ToString("f2"));
                                                else
                                                    sql.Append("update HYK_XFJL_FQ set THJE = THJE + ").Append(calcItem.SaleMoney.ToString("f2"));
                                                sql.Append(",SYXFJE_OLD = SYXFJE_OLD + ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                                sql.Append(",KQJE = KQJE + ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                                sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
                                                if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                                                    sql.Append("   and CXHDBH = ").Append(calcItem.PromId);
                                                else
                                                    sql.Append("   and CXID = ").Append(calcItem.PromId);
                                                sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                                sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                                sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                                if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                                                    sql.Append("   and BMDM = '").Append(calcItem.DeptCode).Append("'");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                                #endregion
                                            }
                                            if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))
                                            {
                                                #region 选单退货---扣券 更新 HYK_XFLJDFQ
                                                sql.Length = 0;
                                                sql.Append("update HYK_XFLJDFQ set ZXFJE = ZXFJE - ").Append(calcItem.SaleMoney.ToString("f2"));
                                                sql.Append(" ,SYXFJE = SYXFJE - ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                                sql.Append(" ,FQJE = FQJE - ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                                sql.Append(" where HYID = ").Append(billHead.OfferCouponVipId);
                                                if (calcItem.OfferStoreScope.Length == 0)
                                                    sql.Append("   and MDFWDM = ' '");
                                                else
                                                    sql.Append("   and MDFWDM = '").Append(calcItem.OfferStoreScope).Append("'");
                                                if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                                                    sql.Append("   and CXHDBH = ").Append(calcItem.PromId);
                                                else
                                                    sql.Append("   and CXID = ").Append(calcItem.PromId);
                                                if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)
                                                    sql.Append("  and INX_XFRQ = ").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate));
                                                else
                                                    sql.Append("  and INX_XFRQ = 0");
                                                sql.Append("  and YHQID = ").Append(calcItem.CouponType);
                                                sql.Append("  and YHQFFGZID = ").Append(calcItem.RuleId);
                                                cmd.CommandText = sql.ToString();
                                                if (cmd.ExecuteNonQuery() == 0)
                                                {
                                                    sql.Length = 0;
                                                    if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                                        sql.Append("insert into HYK_XFLJDFQ(HYID,MDFWDM,CXID,CXHDBH,INX_XFRQ,YHQID,YHQFFGZID,XFRQ,ZXFJE,SYXFJE,FQJE) ");
                                                    else
                                                        sql.Append("insert into HYK_XFLJDFQ(HYID,MDFWDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,XFRQ,ZXFJE,SYXFJE,FQJE) ");
                                                    sql.Append("  values(").Append(billHead.OfferCouponVipId);
                                                    if (calcItem.OfferStoreScope.Length == 0)
                                                        sql.Append(",' '");
                                                    else
                                                        sql.Append(",'").Append(calcItem.OfferStoreScope).Append("'");
                                                    if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                                    {
                                                        if (calcItem.PromIdIsBH)
                                                            sql.Append(",0,").Append(calcItem.PromId);
                                                        else
                                                            sql.Append(",").Append(calcItem.PromId).Append(",0");
                                                    }
                                                    else
                                                        sql.Append(",").Append(calcItem.PromId);
                                                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)
                                                        sql.Append(",").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate));
                                                    else
                                                        sql.Append(",0");
                                                    sql.Append(",").Append(calcItem.CouponType);
                                                    sql.Append(",").Append(calcItem.RuleId);
                                                    DbUtils.SpellSqlParameter(conn, sql, ",", "XFRQ", "");
                                                    sql.Append((-calcItem.SaleMoney).ToString("f2"));
                                                    sql.Append(",").Append((-calcItem.SaleMoneyUsed).ToString("f2"));
                                                    sql.Append(",").Append((-calcItem.OfferCouponMoney).ToString("f2"));
                                                    sql.Append(")");
                                                    cmd.CommandText = sql.ToString();
                                                    cmd.Parameters.Clear();
                                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFRQ", billHead.OfferCouponDate);
                                                    cmd.ExecuteNonQuery();
                                                    cmd.Parameters.Clear();
                                                }
                                                #endregion
                                            }
                                            #region 选单退货---扣券 更新 本单 HYTHJL_KQ
                                            sql.Length = 0;
                                            sql.Append("update HYTHJL_KQ set ZXFJE_OLD = ").Append(calcItem.SaleMoneyBefore.ToString("f2"));
                                            sql.Append("      ,SYXFJE_OLD = ").Append(calcItem.SaleMoneyUsedBefore.ToString("f2"));
                                            sql.Append("      ,FQJE_OLD = ").Append(calcItem.OfferCouponMoneyBefore.ToString("f2"));
                                            sql.Append("      ,SYXFJE_NEW = ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                            sql.Append("      ,KQJE = ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append("      ,KQJE_SJ = ").Append(calcItem.ActualOfferMoney.ToString("f2"));
                                            if (calcItem.PayStoreScope.Length == 0)
                                                sql.Append("      ,MDFWDM_YQ = ' '");
                                            else
                                                sql.Append("      ,MDFWDM_YQ = '").Append(calcItem.PayStoreScope).Append("'");
                                            sql.Append(" where XFJLID = ").Append(billHead.ServerBillId);
                                            if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                                                sql.Append("   and CXHDBH = ").Append(calcItem.PromId);
                                            else
                                                sql.Append("   and CXID = ").Append(calcItem.PromId);
                                            sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                            sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                            sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                            if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                                                sql.Append("   and BMDM = '").Append(calcItem.DeptCode).Append("'");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                            #endregion
                                        }
                                        #region 选单退货---扣券 更新 本单 HYK_XFJL_FQ
                                        sql.Length = 0;
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                        {
                                            sql.Append("insert into HYK_XFJL_FQ(XFJLID,CXID,CXHDBH,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,ZXFJE,SYXFJE,FQJE,BMDM) ");
                                            sql.Append(" select XFJLID,CXID,CXHDBH,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,-THJE,-SYXFJE_NEW,-KQJE_SJ,BMDM ");
                                        }
                                        else
                                        {
                                            sql.Append("insert into HYK_XFJL_FQ(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,ZXFJE,SYXFJE,FQJE,BMDM) ");
                                            sql.Append(" select XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,ZXFJE_OLD,SYXFJE_OLD,FQJE_OLD,-THJE,-SYXFJE_NEW,-KQJE_SJ,BMDM ");
                                        }
                                        sql.Append(" from HYTHJL_KQ");
                                        sql.Append(" where XFJLID = ").Append(billHead.ServerBillId);
                                        sql.Append("   and KQJE_SJ <> 0 ");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                        #endregion
                                    }
                                    foreach (CrmPromOfferCoupon offerCoupon in offerBackCouponList)
                                    {
                                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferBackDifference, 0))
                                        {
                                            #region 选单退货---扣券 更新 HYK_THKQCE
                                            sql.Length = 0;
                                            sql.Append("update HYK_THKQCE set KQCE = KQCE + ").Append(offerCoupon.OfferBackDifference.ToString("f2"));
                                            sql.Append(" where HYID = ").Append(billHead.OfferCouponVipId);
                                            if (CrmServerPlatform.Config.IsUpgradedProject2013 && offerCoupon.PromIdIsBH)
                                                sql.Append("   and CXHDBH = ").Append(offerCoupon.PromId);
                                            else
                                                sql.Append("   and CXID = ").Append(offerCoupon.PromId);
                                            sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                            if (offerCoupon.PayStoreScope.Length == 0)
                                                sql.Append("   and MDFWDM = ' '");
                                            else
                                                sql.Append("   and MDFWDM = '").Append(offerCoupon.PayStoreScope).Append("'");
                                            cmd.CommandText = sql.ToString();
                                            if (cmd.ExecuteNonQuery() == 0)
                                            {
                                                sql.Length = 0;
                                                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                                    sql.Append("insert into HYK_THKQCE(HYID,CXID,CXHDBH,YHQID,MDFWDM,KQCE) ");
                                                else
                                                    sql.Append("insert into HYK_THKQCE(HYID,CXID,YHQID,MDFWDM,KQCE) ");
                                                sql.Append("   values(").Append(billHead.OfferCouponVipId);
                                                if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                                {
                                                    if (offerCoupon.PromIdIsBH)
                                                        sql.Append(",0,").Append(offerCoupon.PromId);
                                                    else
                                                        sql.Append(",").Append(offerCoupon.PromId).Append(",0");
                                                }
                                                else
                                                    sql.Append(",").Append(offerCoupon.PromId);
                                                sql.Append(",").Append(offerCoupon.CouponType);
                                                if (offerCoupon.PayStoreScope.Length == 0)
                                                    sql.Append(",' '");
                                                else
                                                    sql.Append(",'").Append(offerCoupon.PayStoreScope).Append("'");
                                                sql.Append(",").Append(offerCoupon.OfferBackDifference.ToString("f2"));
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                        if (msg.Length == 0)
                            dbTrans.Commit();
                        else
                            dbTrans.Rollback();
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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }
            if (billHead.OriginalServerBillId > 0)
            {
                foreach (CrmCouponPayment payment in payBackCouponList)
                {
                    payment.PayMoney = -payment.PayMoney;
                }
                foreach (CrmPromOfferCoupon offerCoupon in offerBackCouponList)
                {
                    offerCoupon.OfferMoney = -offerCoupon.OfferMoney;
                }
            }
            return (msg.Length == 0);
        }

        //结账
        public static bool CheckOutRSaleBill(out string msg, out double billCent, out double vipCent, out double vipShopCent, out double vipCompanyCent, out string offerCouponVipCode, List<CrmPromOfferCoupon> offerCouponList, List<CrmSaleMoneyLeftWhenPromCalc> leftSaleMoneyList, int serverBillId)
        {
            msg = string.Empty;
            billCent = 0;
            double offerCent = 0;
            vipCent = 0;
            vipShopCent = 0;
            vipCompanyCent = 0;
            offerCouponVipCode = string.Empty;
            CrmWXMessage SendWXMessage = null;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            CrmRSaleBillHead billHead = null;
            try
            {
                int billStatus = 0;
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

                    sql.Length = 0;
                    sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.HYID,a.HYID_FQ,a.HYID_TQ,a.XFJLID_OLD,a.JZRQ,a.SCSJ,a.XFRQ_FQ,a.SKYDM,a.JE,a.ZK_HY,a.JF,a.JFBS,a.BSFS,a.HYKNO,a.HYKNO_FQ,b.MDDM from HYK_XFJL a,MDDY b ");
                    sql.Append("where a.XFJLID = ").Append(serverBillId);
                    sql.Append("  and a.MDID = b.MDID");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        billStatus = DbUtils.GetInt(reader, 0);
                        billHead = new CrmRSaleBillHead();
                        billHead.CompanyCode = DbUtils.GetString(reader, 1);
                        billHead.StoreId = DbUtils.GetInt(reader, 2);
                        billHead.PosId = DbUtils.GetString(reader, 3);
                        billHead.BillId = DbUtils.GetInt(reader, 4);
                        billHead.BillType = DbUtils.GetInt(reader, 5);
                        billHead.VipId = DbUtils.GetInt(reader, 6);
                        billHead.OfferCouponVipId = DbUtils.GetInt(reader, 7);
                        billHead.PayBackCouponVipId = DbUtils.GetInt(reader, 8);
                        billHead.OriginalServerBillId = DbUtils.GetInt(reader, 9);
                        billHead.AccountDate = DbUtils.GetDateTime(reader, 10);
                        billHead.SaleTime = DbUtils.GetDateTime(reader, 11);
                        billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 12);
                        billHead.Cashier = DbUtils.GetString(reader, 13);
                        billHead.TotalSaleMoney = DbUtils.GetDouble(reader, 14);
                        billHead.TotalVipDiscMoney = DbUtils.GetDouble(reader, 15);
                        billHead.TotalGainedCent = DbUtils.GetDouble(reader, 16);
                        billHead.CentMultiple = DbUtils.GetDouble(reader, 17);
                        billHead.CentMultiMode = DbUtils.GetInt(reader, 18);
                        billHead.VipCode = DbUtils.GetString(reader, 19);
                        billHead.OfferCouponVipCode = DbUtils.GetString(reader, 20);
                        billHead.StoreCode = DbUtils.GetString(reader, 21);
                        billHead.ServerBillId = serverBillId;
                        billCent = billHead.TotalGainedCent;
                        reader.Close();
                        offerCouponVipCode = billHead.OfferCouponVipCode;
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select HYID,JF from HYXFJL ");
                        sql.Append("where XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            billStatus = 1;
                            //billHead.VipId = DbUtils.GetInt(reader,0);
                            billCent = DbUtils.GetDouble(reader, 1);
                        }
                        else
                            billStatus = -1;
                        reader.Close();
                    }
                    if (billStatus != 2)
                    {
                        return (billStatus == 1);
                    }

                    double totalBaseCent = 0;
                    if (billHead.VipId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select sum(JFJS * XSJE_JF) from HYK_XFJL_SP where BJ_JF = 1 and XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            totalBaseCent = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();
                        totalBaseCent = Math.Round(totalBaseCent, 4);
                    }

                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                    int paperCouponCount = 0;
                    int vipCouponCount = 0;
                    int seqBegin = 0;
                    int seqInx = 0;
                    List<CrmPromOfferCouponCalcItem> offerCouponCalcItemList = null;
                    List<CrmPromPaymentOfferCouponCalcItem> paymentOfferCouponCalcItemList = null;
                    List<CrmPromPaymentOfferCouponLimit> paymentOfferCouponLimitList = null;
                    List<CrmPromOfferCouponLimit> offerCouponLimitList = null;
                    List<CrmPromOfferGiftLimit> offerGiftLimitList = null;
                    if (billHead.BillType == CrmPosData.BillTypeSale)
                    {
                        offerCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                        #region 从HYK_XFJL_FQ得到返券计算项列表
                        sql.Length = 0;
                        sql.Append("select a.CXID,a.YHQID,a.YHQFFGZID,a.XFLJFQFS,a.ZXFJE,a.SYXFJE,a.FQJE,a.BMDM,b.BJ_DZYHQ,b.BJ_TS from HYK_XFJL_FQ a, YHQDEF b ");
                        sql.Append("where a.YHQID = b.YHQID ");
                        sql.Append("  and a.XFJLID = ").Append(serverBillId);
                        sql.Append("  and a.DJLX = 0 ");
                        if (billHead.OfferCouponVipId == 0)
                            sql.Append("  and b.BJ_DZYHQ = 0 ");
                        //sql.Append("  and a.XFLJFQFS in (1,2) ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                            offerCouponCalcItemList.Add(calcItem);
                            calcItem.PromId = DbUtils.GetInt(reader, 0);
                            calcItem.CouponType = DbUtils.GetInt(reader, 1);
                            calcItem.RuleId = DbUtils.GetInt(reader, 2);
                            calcItem.AddupSaleMoneyType = DbUtils.GetInt(reader, 3);
                            calcItem.SaleMoney = DbUtils.GetDouble(reader, 4);
                            calcItem.SaleMoneyUsed = DbUtils.GetDouble(reader, 5);
                            calcItem.OfferCouponMoney = DbUtils.GetDouble(reader, 6);
                            if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))
                                calcItem.OfferStoreScope = DbUtils.GetString(reader, 7);
                            else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                                calcItem.DeptCode = DbUtils.GetString(reader, 7);
                            calcItem.IsPaperCoupon = (!DbUtils.GetBool(reader, 8));
                            calcItem.SpecialType = DbUtils.GetInt(reader, 9);
                        }
                        reader.Close();
                        #endregion

                        #region 从HYK_XFJL_ZSLP得到赠送礼品
                        sql.Length = 0;
                        sql.Append("select a.CXID,a.YHQID,a.YHQFFGZID,a.XFLJFQFS,a.BMDM,a.LPDM from HYK_XFJL_ZSLP a ");
                        sql.Append("where a.XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int promId = DbUtils.GetInt(reader, 0);
                            int couponType = DbUtils.GetInt(reader, 1);
                            int ruleId = DbUtils.GetInt(reader, 2);
                            int addupSaleMoneyType = DbUtils.GetInt(reader, 3);
                            string deptCode = DbUtils.GetString(reader, 4);
                            string giftCode = DbUtils.GetString(reader, 5);
                            foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                            {
                                if ((calcItem.PromId == promId) && (calcItem.CouponType == couponType) && (calcItem.RuleId == ruleId) && (calcItem.AddupSaleMoneyType == addupSaleMoneyType) && ((addupSaleMoneyType != CrmPosData.AddupSaleMoneyOfOneBillOneDept) || calcItem.DeptCode.Equals(deptCode)))
                                {
                                    calcItem.GiftCode = giftCode;
                                    //break;
                                }
                            }
                        }
                        reader.Close();
                        #endregion

                        paymentOfferCouponCalcItemList = new List<CrmPromPaymentOfferCouponCalcItem>();
                        #region 从HYK_XFJL_ZFFS_FQ得到返券计算项列表
                        sql.Length = 0;
                        sql.Append("select a.CXID,a.YHQID,a.YHQFFGZID,a.SHZFFSID,a.BANK_ZFINX,a.BANK_ID,a.BANK_KH,a.FQJE,b.BJ_DZYHQ,b.BJ_TS from HYK_XFJL_ZFFS_FQ a, YHQDEF b ");
                        sql.Append("where a.YHQID = b.YHQID ");
                        sql.Append("  and a.XFJLID = ").Append(serverBillId);
                        sql.Append("  and a.FQJE > 0 ");
                        if (billHead.OfferCouponVipId == 0)
                            sql.Append("  and b.BJ_DZYHQ = 0 ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromPaymentOfferCouponCalcItem calcItem = new CrmPromPaymentOfferCouponCalcItem();
                            paymentOfferCouponCalcItemList.Add(calcItem);
                            calcItem.PromId = DbUtils.GetInt(reader, 0);
                            calcItem.CouponType = DbUtils.GetInt(reader, 1);
                            calcItem.RuleId = DbUtils.GetInt(reader, 2);
                            calcItem.PayTypeId = DbUtils.GetInt(reader, 3);
                            calcItem.BankCardInx = DbUtils.GetInt(reader, 4);
                            calcItem.BankId = DbUtils.GetInt(reader, 5);
                            calcItem.BankCardCode = DbUtils.GetString(reader, 6);
                            calcItem.OfferCouponMoney = DbUtils.GetDouble(reader, 7);
                            calcItem.ActualOfferMoney = calcItem.OfferCouponMoney;
                            calcItem.IsPaperCoupon = (!DbUtils.GetBool(reader, 8));
                            calcItem.SpecialType = DbUtils.GetInt(reader, 9);
                        }
                        reader.Close();
                        #endregion

                        #region 从HYK_XFJL_ZFFS_ZSLP得到赠送礼品
                        sql.Length = 0;
                        sql.Append("select a.CXID,a.YHQID,a.YHQFFGZID,a.SHZFFSID,a.BANK_ZFINX,a.LPDM from HYK_XFJL_ZFFS_ZSLP a ");
                        sql.Append("where a.XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int promId = DbUtils.GetInt(reader, 0);
                            int couponType = DbUtils.GetInt(reader, 1);
                            int ruleId = DbUtils.GetInt(reader, 2);
                            int payTypeId = DbUtils.GetInt(reader, 3);
                            int bankCardInx = DbUtils.GetInt(reader, 4);
                            string giftCode = DbUtils.GetString(reader, 5);
                            foreach (CrmPromPaymentOfferCouponCalcItem calcItem in paymentOfferCouponCalcItemList)
                            {
                                if ((calcItem.PromId == promId) && (calcItem.CouponType == couponType) && (calcItem.RuleId == ruleId) && (calcItem.PayTypeId == payTypeId) && (calcItem.BankCardInx == bankCardInx))
                                {
                                    calcItem.GiftCode = giftCode;
                                    //break;
                                }
                            }
                        }
                        reader.Close();
                        #endregion

                        if (offerCouponCalcItemList.Count > 0)
                        {
                            PromCalculator.GetPromOfferCouponDataBefore(offerCouponCalcItemList, billHead.OfferCouponVipId, DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate), 0, false, cmd);
                            PromCalculator.CalcPromOfferCouponOneDayOrPromotion(offerCouponCalcItemList);

                            #region 合计商品返券
                            foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                            {
                                calcItem.ActualOfferMoney = calcItem.OfferCouponMoney;
                                if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))
                                {
                                    bool isFound = false;
                                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                    {
                                        if ((calcItem.CouponType == offerCoupon.CouponType) && (calcItem.PromId == offerCoupon.PromId))
                                        {
                                            if ((calcItem.SpecialType == 0) || (calcItem.SpecialType == 3) || (calcItem.SpecialType == 4) || (calcItem.GiftCode.Equals(offerCoupon.CouponCode)))
                                            {
                                                isFound = true;
                                                offerCoupon.OfferMoney += calcItem.OfferCouponMoney;
                                                break;
                                            }
                                        }
                                    }
                                    if (!isFound)
                                    {
                                        CrmPromOfferCoupon offerCoupon = new CrmPromOfferCoupon();
                                        offerCouponList.Add(offerCoupon);
                                        offerCoupon.CouponType = calcItem.CouponType;
                                        offerCoupon.PromId = calcItem.PromId;
                                        offerCoupon.OfferMoney = calcItem.OfferCouponMoney;
                                        offerCoupon.SpecialType = calcItem.SpecialType;
                                        if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                                            offerCoupon.CouponCode = calcItem.GiftCode;
                                        offerCoupon.IsPaperCoupon = calcItem.IsPaperCoupon;
                                        offerCoupon.IsFromPayment = false;
                                    }
                                }
                            }
                            #endregion

                            #region 取返券限额列表
                            if (offerCouponList.Count > 0)
                            {
                                offerCouponLimitList = new List<CrmPromOfferCouponLimit>();
                                sql.Length = 0;
                                sql.Append("select CXID,YHQID,MDID,FQZXE from CXHD_FQXEDEF ");
                                sql.Append(" where MDID in (0,").Append(billHead.StoreId).Append(")");
                                sql.Append("   and (");
                                bool isFirst = true;
                                foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                {
                                    if (!offerCoupon.IsFromPayment)
                                    {
                                        if (isFirst)
                                        {
                                            sql.Append(" (CXID = ").Append(offerCoupon.PromId).Append(" and YHQID = ").Append(offerCoupon.CouponType).Append(")");
                                            isFirst = false;
                                        }
                                        else
                                            sql.Append(" or (CXID = ").Append(offerCoupon.PromId).Append(" and YHQID = ").Append(offerCoupon.CouponType).Append(")");
                                    }
                                }
                                sql.Append(")");
                                sql.Append(" order by CXID,YHQID,MDID desc");
                                cmd.CommandText = sql.ToString();
                                if (!isFirst)
                                {
                                    reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        CrmPromOfferCouponLimit offerLimit = new CrmPromOfferCouponLimit();
                                        offerCouponLimitList.Add(offerLimit);
                                        offerLimit.PromId = DbUtils.GetInt(reader, 0);
                                        offerLimit.CouponType = DbUtils.GetInt(reader, 1);
                                        offerLimit.StoreId = DbUtils.GetInt(reader, 2);
                                        offerLimit.MaxMoneyPeriod = DbUtils.GetDouble(reader, 3);
                                    }
                                    reader.Close();
                                }
                            }
                            #endregion

                            #region 取赠送礼品限制数量列表
                            if (offerCouponList.Count > 0)
                            {
                                offerGiftLimitList = new List<CrmPromOfferGiftLimit>();
                                sql.Length = 0;
                                sql.Append("select CXID,LPDM,MDID,LPSL_DAY from CXZLP_XZLPSLDEF ");
                                sql.Append(" where MDID in (0,").Append(billHead.StoreId).Append(")");
                                sql.Append("  and LPSL_DAY > 0 ");
                                sql.Append("   and (");
                                bool isFirst = true;
                                foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                {
                                    if ((!offerCoupon.IsFromPayment) && ((offerCoupon.SpecialType == 1) || (offerCoupon.SpecialType == 2)))
                                    {
                                        if (isFirst)
                                        {
                                            sql.Append(" (CXID = ").Append(offerCoupon.PromId).Append(" and LPDM = ").Append(offerCoupon.CouponCode).Append(")");
                                            isFirst = false;
                                        }
                                        else
                                            sql.Append(" or (CXID = ").Append(offerCoupon.PromId).Append(" and LPDM = ").Append(offerCoupon.CouponCode).Append(")");
                                    }
                                }
                                sql.Append(")");
                                sql.Append(" order by CXID,LPDM,MDID desc");
                                if (!isFirst)
                                {
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        CrmPromOfferGiftLimit offerLimit = new CrmPromOfferGiftLimit();
                                        offerGiftLimitList.Add(offerLimit);
                                        offerLimit.PromId = DbUtils.GetInt(reader, 0);
                                        offerLimit.GiftCode = DbUtils.GetString(reader, 1);
                                        offerLimit.StoreId = DbUtils.GetInt(reader, 2);
                                        offerLimit.MaxNumEveryDay = DbUtils.GetInt(reader, 3);
                                    }
                                    reader.Close();
                                }
                            }
                            #endregion
                        }

                        if (paymentOfferCouponCalcItemList.Count > 0)
                        {
                            #region 合计支付方式返券
                            bool existPaymentOfferCoupon = false;
                            foreach (CrmPromPaymentOfferCouponCalcItem calcItem in paymentOfferCouponCalcItemList)
                            {
                                calcItem.ActualOfferMoney = calcItem.OfferCouponMoney;
                                if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))
                                {
                                    bool isFound = false;
                                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                    {
                                        if ((calcItem.CouponType == offerCoupon.CouponType) && (calcItem.PromId == offerCoupon.PromId) && (calcItem.BankCardInx == offerCoupon.BankCardInx))
                                        {
                                            if ((calcItem.SpecialType == 0) || (calcItem.GiftCode.Equals(offerCoupon.CouponCode)))
                                            {
                                                isFound = true;
                                                offerCoupon.OfferMoney += calcItem.OfferCouponMoney;
                                                break;
                                            }
                                        }
                                    }
                                    if (!isFound)
                                    {
                                        CrmPromOfferCoupon offerCoupon = new CrmPromOfferCoupon();
                                        offerCouponList.Add(offerCoupon);
                                        existPaymentOfferCoupon = true;
                                        offerCoupon.CouponType = calcItem.CouponType;
                                        offerCoupon.PromId = calcItem.PromId;
                                        offerCoupon.BankId = calcItem.BankId;
                                        offerCoupon.BankCardInx = calcItem.BankCardInx;
                                        offerCoupon.BankCardCode = calcItem.BankCardCode;
                                        offerCoupon.OfferMoney = calcItem.OfferCouponMoney;
                                        offerCoupon.SpecialType = calcItem.SpecialType;
                                        if (calcItem.SpecialType > 0)
                                            offerCoupon.CouponCode = calcItem.GiftCode;
                                        offerCoupon.IsPaperCoupon = calcItem.IsPaperCoupon;
                                        offerCoupon.IsFromPayment = true;
                                    }
                                }
                            }
                            #endregion

                            #region 取支付方式返券限额列表
                            if (existPaymentOfferCoupon)
                            {
                                paymentOfferCouponLimitList = new List<CrmPromPaymentOfferCouponLimit>();
                                sql.Length = 0;
                                sql.Append("select CXID,YHQID,BANK_ID,MDID,FQXE,FQZXE,FQZXE_CARD,MAX_FQCS,FQXE_DAY,FQXE_CARD_DAY,MAX_FQCS_DAY from BANK_FQXEDEF ");
                                sql.Append(" where MDID in (0,").Append(billHead.StoreId).Append(")");
                                sql.Append("   and (");
                                bool isFirst = true;
                                foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                {
                                    if (offerCoupon.IsFromPayment)
                                    {
                                        if (isFirst)
                                        {
                                            sql.Append(" (CXID = ").Append(offerCoupon.PromId).Append(" and YHQID = ").Append(offerCoupon.CouponType).Append(" and BANK_ID = ").Append(offerCoupon.BankId).Append(")");
                                            isFirst = false;
                                        }
                                        else
                                            sql.Append(" or (CXID = ").Append(offerCoupon.PromId).Append(" and YHQID = ").Append(offerCoupon.CouponType).Append(" and BANK_ID = ").Append(offerCoupon.BankId).Append(")");
                                    }
                                }
                                sql.Append(")");
                                sql.Append(" order by CXID,YHQID,BANK_ID,MDID desc");
                                cmd.CommandText = sql.ToString();
                                if (!isFirst)
                                {
                                    reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        CrmPromPaymentOfferCouponLimit offerLimit = new CrmPromPaymentOfferCouponLimit();
                                        paymentOfferCouponLimitList.Add(offerLimit);
                                        offerLimit.PromId = DbUtils.GetInt(reader, 0);
                                        offerLimit.CouponType = DbUtils.GetInt(reader, 1);
                                        offerLimit.BankId = DbUtils.GetInt(reader, 2);
                                        offerLimit.StoreId = DbUtils.GetInt(reader, 3);
                                        //FQXE,FQZXE,FQZXE_CARD,MAX_FQCS,FQXE_DAY,FQXE_CARD_DAY,MAX_FQCS_DAY
                                        offerLimit.MaxMoneyOnce = DbUtils.GetDouble(reader, 4);
                                        offerLimit.MaxMoneyAllCardPeriod = DbUtils.GetDouble(reader, 5);
                                        offerLimit.MaxMoneyEveryCardPeriod = DbUtils.GetDouble(reader, 6);
                                        offerLimit.MaxTimesEveryCardPeriod = DbUtils.GetInt(reader, 7);
                                        offerLimit.MaxMoneyAllCardEveryDay = DbUtils.GetDouble(reader, 8);
                                        offerLimit.MaxMoneyEveryCardEveryDay = DbUtils.GetDouble(reader, 9);
                                        offerLimit.MaxTimesEveryCardEveryDay = DbUtils.GetInt(reader, 10);
                                    }
                                    reader.Close();
                                }
                            }
                            #endregion
                        }

                        if (offerCouponList.Count > 0)
                        {
                            int paperCodeCount = 0;
                            foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                            {
                                if ((offerCoupon.SpecialType == 0) || (offerCoupon.SpecialType == 3) || (offerCoupon.SpecialType == 4))
                                {
                                    #region 取所返券的名称，有效期，使用门店范围，纸券打印信息
                                    sql.Length = 0;
                                    sql.Append("select a.FS_YQMDFW,a.YHQMC,a.BJ_DZYHQ,a.ISCODED,a.CODELEN,a.CODEPRE,a.CODESUF,b.YHQSYJSRQ,b.ZQDW,b.ZQBZ1,b.ZQBZ2 from YHQDEF a, YHQDEF_CXHD b ");
                                    sql.Append(" where a.YHQID = b.YHQID ");
                                    sql.Append("   and a.YHQID = ").Append(offerCoupon.CouponType);
                                    sql.Append("   and b.CXID = ").Append(offerCoupon.PromId);
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        if (offerCoupon.Coupon == null)
                                            offerCoupon.Coupon = new CrmCoupon();
                                        switch (DbUtils.GetInt(reader, 0))
                                        {
                                            case 2:
                                                offerCoupon.PayStoreScope = billHead.CompanyCode;
                                                break;
                                            case 3:
                                                offerCoupon.PayStoreScope = billHead.StoreCode;
                                                break;
                                            default:
                                                //offerCoupon.PayStoreScope = string.Empty;
                                                offerCoupon.PayStoreScope = " ";
                                                break;
                                        }
                                        offerCoupon.CouponTypeName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                                        offerCoupon.Coupon.IsPaperCoupon = (!DbUtils.GetBool(reader, 2));
                                        offerCoupon.Coupon.IsCoded = DbUtils.GetBool(reader, 3);
                                        offerCoupon.Coupon.CodeLength = DbUtils.GetInt(reader, 4);
                                        offerCoupon.Coupon.CodePrefix = DbUtils.GetString(reader, 5);
                                        offerCoupon.Coupon.CodeSuffix = DbUtils.GetString(reader, 6);
                                        offerCoupon.ValidDate = DbUtils.GetDateTime(reader, 7).Date;
                                        offerCoupon.Coupon.PaperUnit = DbUtils.GetString(reader, 8, CrmServerPlatform.Config.DbCharSetIsNotChinese, 10);
                                        offerCoupon.Coupon.PaperNote1 = DbUtils.GetString(reader, 9, CrmServerPlatform.Config.DbCharSetIsNotChinese, 255);
                                        offerCoupon.Coupon.PaperNote2 = DbUtils.GetString(reader, 10, CrmServerPlatform.Config.DbCharSetIsNotChinese, 255);
                                        if (offerCoupon.Coupon.IsPaperCoupon && offerCoupon.Coupon.IsCoded)
                                            paperCodeCount++;
                                    }
                                    reader.Close();
                                    #endregion
                                }
                                else if ((offerCoupon.SpecialType == 1) || (offerCoupon.SpecialType == 2))
                                {
                                    #region 取所送礼品的名称，备注信息
                                    sql.Length = 0;
                                    sql.Append("select b.LPMC from HYK_JFFHLPXX b ");
                                    sql.Append(" where b.LPDM = '").Append(offerCoupon.CouponCode).Append("'");
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        offerCoupon.CouponTypeName = DbUtils.GetString(reader, 0);
                                    }
                                    reader.Close();
                                    sql.Length = 0;
                                    sql.Append("select b.YHQSYJSRQ,b.ZQDW,b.ZQBZ1,b.ZQBZ2 from YHQDEF_CXHD b ");
                                    sql.Append(" where b.YHQID = ").Append(offerCoupon.CouponType);
                                    sql.Append("   and b.CXID = ").Append(offerCoupon.PromId);
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        if (offerCoupon.Coupon == null)
                                            offerCoupon.Coupon = new CrmCoupon();

                                        offerCoupon.ValidDate = DbUtils.GetDateTime(reader, 0).Date;
                                        offerCoupon.Coupon.IsPaperCoupon = false;
                                        offerCoupon.Coupon.PaperUnit = DbUtils.GetString(reader, 1);
                                        offerCoupon.Coupon.PaperNote1 = DbUtils.GetString(reader, 2);
                                        offerCoupon.Coupon.PaperNote2 = DbUtils.GetString(reader, 3);
                                    }
                                    reader.Close();
                                    #endregion
                                }
                            }
                            if (paperCodeCount > 0)
                            {
                                #region 产生纸券代码
                                int seqEnd2 = SeqGenerator.GetSeqYHQCODE("CRMDB", paperCodeCount);
                                int seqInx2 = 0;
                                Random rd = new Random();
                                foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                {
                                    if ((offerCoupon.Coupon != null) && (offerCoupon.Coupon.IsPaperCoupon) && (offerCoupon.Coupon.IsCoded))
                                    {
                                        int len = offerCoupon.Coupon.CodeLength - offerCoupon.Coupon.CodePrefix.Length - offerCoupon.Coupon.CodeSuffix.Length - 2;
                                        if (len < 6)
                                            len = 6;
                                        offerCoupon.CouponCode = string.Format("{0}{1:D" + len.ToString() + "}{2:D2}{3}", offerCoupon.Coupon.CodePrefix, seqEnd2 - seqInx2, rd.Next(99), offerCoupon.Coupon.CodeSuffix);
                                    }
                                }
                                #endregion
                            }

                            foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                            {
                                offerCoupon.ActualOfferMoney = offerCoupon.OfferMoney;
                                if (offerCoupon.Coupon != null)
                                {
                                    if (offerCoupon.Coupon.IsPaperCoupon)
                                        paperCouponCount++;
                                    else
                                        vipCouponCount++;
                                }
                            }
                          
                            if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                                seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, vipCouponCount) - vipCouponCount + 1;
                        }
                    }
                    int seqJFBDJLMX = 0;
                    if (billHead.VipId > 0)
                        seqJFBDJLMX = SeqGenerator.GetSeqHYK_JFBDJLMX("CRMDB", CrmServerPlatform.CurrentDbId);
                    if (billHead.VipId > 0)
                    {
                        if (CrmServerPlatform.Config.WXUrl.Length > 0)
                        {
                            sql.Length = 0;
                            sql.Append("select HYK_NO,OPENID from HYK_HYXX where HYID = ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read() && (!reader.IsDBNull(1)))
                            {
                                SendWXMessage = new CrmWXMessage();
                                SendWXMessage.CardCode = DbUtils.GetString(reader, 0);
                                SendWXMessage.OpenId = DbUtils.GetString(reader, 1);
                                SendWXMessage.ServerBillId = serverBillId;
                                SendWXMessage.isSearch = false;
                            }
                            reader.Close();
                        }
                    }
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;
                        sql.Length = 0;
                        sql.Append("update HYK_XFJL set STATUS = 1");
                        sql.Append(" where XFJLID = ").Append(serverBillId);
                        sql.Append("   and STATUS = 2");
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            if (billHead.BillType == CrmPosData.BillTypeSale)
                            {
                                #region 减去满减金额 HYK_XFJL，HYK_XFJL_SP，HYK_XFJL_SP_FQ，HYK_XFJL_SP_YQFT
                                sql.Length = 0;
                                //sql.Append("update HYK_XFJL_SP set a.XSJE = b.XSJE - b.MBJZJE");
                                //sql.Append(" from HYK_XFJL_SP a,HYK_XFJL_SP_MBJZ b");
                                //sql.Append(" where a.XFJLID = ").Append(serverBillId);
                                //sql.Append("   and a.XFJLID = b.XFJLID");
                                //sql.Append("   and a.INX = b.INX");
                                sql.Append("update HYK_XFJL_SP set XSJE = XSJE - (select MBJZJE from HYK_XFJL_SP_MBJZ where HYK_XFJL_SP.XFJLID = HYK_XFJL_SP_MBJZ.XFJLID and HYK_XFJL_SP.INX = HYK_XFJL_SP_MBJZ.INX) ");
                                sql.Append(" where exists(select * from HYK_XFJL_SP_MBJZ where HYK_XFJL_SP.XFJLID = HYK_XFJL_SP_MBJZ.XFJLID and HYK_XFJL_SP.INX = HYK_XFJL_SP_MBJZ.INX and HYK_XFJL_SP.XFJLID = ").Append(serverBillId).Append(")");
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() != 0)
                                {
                                    sql.Length = 0;
                                    sql.Append("select sum(XSJE) from HYK_XFJL_SP where XFJLID = ").Append(serverBillId);
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    double totalSaleMoney = 0;
                                    if (reader.Read())
                                        totalSaleMoney = DbUtils.GetDouble(reader, 0);
                                    reader.Close();
                                    sql.Length = 0;
                                    sql.Append("update HYK_XFJL set JE = ").Append(totalSaleMoney.ToString("f2")).Append(" where XFJLID = ").Append(serverBillId);
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();

                                    sql.Length = 0;
                                    //sql.Append("update HYK_XFJL_SP_FQ set a.XSJE = b.XSJE ");
                                    //sql.Append(" from HYK_XFJL_SP_FQ a,HYK_XFJL_SP b");
                                    //sql.Append(" where a.XFJLID = ").Append(serverBillId);
                                    //sql.Append("   and a.XFJLID = b.XFJLID");
                                    //sql.Append("   and a.INX = b.INX");
                                    sql.Append("update HYK_XFJL_SP_FQ set XSJE = (select XSJE from HYK_XFJL_SP where HYK_XFJL_SP_FQ.XFJLID = HYK_XFJL_SP.XFJLID and HYK_XFJL_SP_FQ.INX = HYK_XFJL_SP.INX) ");
                                    sql.Append(" where exists(select * from HYK_XFJL_SP where HYK_XFJL_SP_FQ.XFJLID = HYK_XFJL_SP.XFJLID and HYK_XFJL_SP_FQ.INX = HYK_XFJL_SP.INX and HYK_XFJL_SP_FQ.XFJLID = ").Append(serverBillId).Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();

                                    sql.Length = 0;
                                    //sql.Append("update HYK_XFJL_SP_YQFT set a.XSJE = b.XSJE ");
                                    //sql.Append(" from HYK_XFJL_SP_YQFT a,HYK_XFJL_SP b");
                                    //sql.Append(" where a.XFJLID = ").Append(serverBillId);
                                    //sql.Append("   and a.XFJLID = b.XFJLID");
                                    //sql.Append("   and a.INX = b.INX");
                                    sql.Append("update HYK_XFJL_SP_YQFT set XSJE = (select XSJE from HYK_XFJL_SP where HYK_XFJL_SP_YQFT.XFJLID = HYK_XFJL_SP.XFJLID and HYK_XFJL_SP_YQFT.INX = HYK_XFJL_SP.INX) ");
                                    sql.Append(" where exists(select * from HYK_XFJL_SP where HYK_XFJL_SP_YQFT.XFJLID = HYK_XFJL_SP.XFJLID and HYK_XFJL_SP_YQFT.INX = HYK_XFJL_SP.INX and HYK_XFJL_SP_YQFT.XFJLID = ").Append(serverBillId).Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                                #endregion
                            }

                            if ((billHead.BillType == CrmPosData.BillTypeSale) && ((offerCouponCalcItemList.Count > 0) || (paymentOfferCouponCalcItemList.Count > 0)))
                            {
                                if ((offerCouponCalcItemList.Count > 0) && (offerCouponLimitList != null) && (offerCouponLimitList.Count > 0))
                                {
                                    CheckOfferCouponLimit(cmd, sql, offerCouponCalcItemList, offerCouponList, offerCouponLimitList);
                                }
                                if ((offerCouponCalcItemList.Count > 0) && (offerGiftLimitList != null) && (offerGiftLimitList.Count > 0))
                                {
                                    CheckOfferGiftLimit(cmd, sql, offerCouponCalcItemList, offerCouponList, offerGiftLimitList, serverTime.ToString("yyyyMMdd"));
                                }
                                if ((paymentOfferCouponCalcItemList.Count > 0) && (paymentOfferCouponLimitList != null) && (paymentOfferCouponLimitList.Count > 0))
                                {
                                    CheckPaymentOfferCouponLimit(cmd, sql, paymentOfferCouponCalcItemList, offerCouponList, paymentOfferCouponLimitList, serverTime.ToString("yyyyMMdd"));
                                }
                                foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                                {
                                    if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))
                                    {
                                        #region 返券 更新 HYK_XFJL_FQ
                                        sql.Length = 0;
                                        sql.Append("update HYK_XFJL_FQ set ZXFJE = ").Append(calcItem.SaleMoney.ToString("f2"));
                                        sql.Append(",SYXFJE = ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                        sql.Append(",FQJE = ").Append(calcItem.ActualOfferMoney.ToString("f2"));
                                        sql.Append(",FQJE_WX = ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                        sql.Append(",ZXFJE_OLD = ").Append(calcItem.SaleMoneyBefore.ToString("f2"));
                                        sql.Append(",SYXFJE_OLD = ").Append(calcItem.SaleMoneyUsedBefore.ToString("f2"));
                                        sql.Append(",FQJE_OLD = ").Append(calcItem.OfferCouponMoneyBefore.ToString("f2"));
                                        sql.Append(" where XFJLID = ").Append(serverBillId);
                                        sql.Append("   and CXID = ").Append(calcItem.PromId);
                                        sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                        sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                        sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                        #endregion

                                        #region 返券 更新 HYK_XFJL_ZSLP
                                        if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_XFJL_ZSLP set LPDM = '").Append(calcItem.GiftCode).Append("'");
                                            sql.Append(",LPSL = ").Append(((int)calcItem.ActualOfferMoney).ToString());
                                            sql.Append(" where XFJLID = ").Append(serverBillId);
                                            sql.Append("   and CXID = ").Append(calcItem.PromId);
                                            sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                            sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                            sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                        #endregion
                                    }
                                    else if (MathUtils.DoubleASmallerThanDoubleB(calcItem.ActualOfferMoney, calcItem.OfferCouponMoney))
                                    {
                                        #region 返券 更新 HYK_XFJL_FQ
                                        sql.Length = 0;
                                        sql.Append("update HYK_XFJL_FQ set FQJE = ").Append(calcItem.ActualOfferMoney.ToString("f2"));
                                        sql.Append(" where XFJLID = ").Append(serverBillId);
                                        sql.Append("   and CXID = ").Append(calcItem.PromId);
                                        sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                        sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                        sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                        #endregion

                                        #region 返券 更新 HYK_XFJL_ZSLP
                                        if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_XFJL_ZSLP set LPSL = ").Append(((int)calcItem.ActualOfferMoney).ToString());
                                            sql.Append(" where XFJLID = ").Append(serverBillId);
                                            sql.Append("   and CXID = ").Append(calcItem.PromId);
                                            sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                            sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                            sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                        }
                                        #endregion
                                    }
                                }

                                if (billHead.OfferCouponVipId > 0)
                                {
                                    foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                                    {
                                        if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))
                                        {
                                            #region 返券 更新 HYK_XFLJDFQ
                                            sql.Length = 0;
                                            sql.Append("update HYK_XFLJDFQ set ZXFJE = ZXFJE + ").Append(calcItem.SaleMoney.ToString("f2"));
                                            sql.Append(" ,SYXFJE = SYXFJE + ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                            sql.Append(" ,FQJE = FQJE + ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append(" where HYID = ").Append(billHead.OfferCouponVipId);
                                            if (calcItem.OfferStoreScope.Length == 0)
                                                sql.Append("   and MDFWDM = ' '");
                                            else
                                                sql.Append("   and MDFWDM = '").Append(calcItem.OfferStoreScope).Append("'");
                                            sql.Append("   and CXID = ").Append(calcItem.PromId);
                                            if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)   //当日累计返券
                                                sql.Append("  and INX_XFRQ = ").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate));
                                            else
                                                sql.Append("  and INX_XFRQ = 0");
                                            sql.Append("  and YHQID = ").Append(calcItem.CouponType);
                                            sql.Append("  and YHQFFGZID = ").Append(calcItem.RuleId);
                                            cmd.CommandText = sql.ToString();
                                            if (cmd.ExecuteNonQuery() == 0)
                                            {
                                                sql.Length = 0;
                                                sql.Append("insert into HYK_XFLJDFQ(HYID,MDFWDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,XFRQ,ZXFJE,SYXFJE,FQJE) ");
                                                sql.Append("  values(").Append(billHead.OfferCouponVipId);
                                                if (calcItem.OfferStoreScope.Length == 0)
                                                    sql.Append(",' '");
                                                else
                                                    sql.Append(",'").Append(calcItem.OfferStoreScope).Append("'");
                                                sql.Append(",").Append(calcItem.PromId);
                                                if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)   //当日累计返券
                                                    sql.Append(",").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate));
                                                else
                                                    sql.Append(",0");
                                                sql.Append(",").Append(calcItem.CouponType);
                                                sql.Append(",").Append(calcItem.RuleId);
                                                DbUtils.SpellSqlParameter(conn, sql, ",", "XFRQ", "");
                                                sql.Append(",").Append(calcItem.SaleMoney.ToString("f2"));
                                                sql.Append(",").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                                sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                                sql.Append(")");
                                                cmd.CommandText = sql.ToString();
                                                cmd.Parameters.Clear();
                                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFRQ", billHead.OfferCouponDate);
                                                cmd.ExecuteNonQuery();
                                                cmd.Parameters.Clear();
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                if ((vipCouponCount > 0) && (billHead.OfferCouponVipId > 0))
                                {
                                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                    {
                                        if ((offerCoupon.Coupon != null) && (((!offerCoupon.Coupon.IsPaperCoupon) && (offerCoupon.SpecialType == 0)) || (offerCoupon.SpecialType == 4)))
                                        {
                                            UpdateVipCouponAccount(out msg, cmd, sql, 10,
                                                                    false,
                                                                    false,
                                                                    billHead.OfferCouponVipId,
                                                                    offerCoupon.CouponType,
                                                                    offerCoupon.PromId,
                                                                    offerCoupon.ValidDate,
                                                                    offerCoupon.PayStoreScope,
                                                                    offerCoupon.OfferMoney,
                                                                    out offerCoupon.Balance,
                                                                    seqBegin + seqInx++,
                                                                    0,
                                                                    billHead.StoreId,
                                                                    billHead.PosId,
                                                                    billHead.BillId,
                                                                    serverTime,
                                                                    billHead.AccountDate,
                                                                    billHead.Cashier,
                                                                    "前台返券");
                                            if (msg.Length > 0)
                                            {
                                                dbTrans.Rollback();
                                                return false;
                                            }
                                        }
                                    }
                                }
                                if ((billHead.VipId > 0))
                                {
                                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                    {
                                        if (offerCoupon.SpecialType == 3)
                                        {
                                            offerCent += offerCoupon.OfferMoney;
                                        }
                                    }
                                }
                                if (paperCouponCount > 0)
                                {
                                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                                    {
                                        if ((offerCoupon.Coupon != null) && (offerCoupon.Coupon.IsPaperCoupon) && (offerCoupon.Coupon.IsCoded) && (offerCoupon.SpecialType == 0))
                                        {
                                            #region 返纸券，写HYK_XFJL_FQDM，YHQCODE
                                            sql.Length = 0;
                                            sql.Append("insert into HYK_XFJL_FQDM(XFJLID,YHQCODE,YHQID,YHQMZ,CXID) ");
                                            sql.Append("  values(").Append(billHead.ServerBillId);
                                            sql.Append(",'").Append(offerCoupon.CouponCode);
                                            sql.Append("',").Append(offerCoupon.CouponType);
                                            sql.Append(",").Append(offerCoupon.OfferMoney.ToString("f2"));
                                            sql.Append(",").Append(offerCoupon.PromId);
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                            sql.Length = 0;
                                            sql.Append("insert into YHQCODE(YHQCODE,YHQID,YHQMZ,BEGINDATE,ENDDATE,CXID,MDID_FQ,FQSJ,STATUS) ");
                                            sql.Append("  values('").Append(offerCoupon.CouponCode);
                                            sql.Append("',").Append(offerCoupon.CouponType);
                                            sql.Append(",").Append(offerCoupon.OfferMoney.ToString("f2"));
                                            sql.Append(",null");
                                            DbUtils.SpellSqlParameter(conn, sql, ",", "ENDDATE", "");
                                            sql.Append(",").Append(offerCoupon.PromId);
                                            sql.Append(",").Append(billHead.StoreId);
                                            DbUtils.SpellSqlParameter(conn, sql, ",", "FQSJ", "");
                                            sql.Append(",1");
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ENDDATE", offerCoupon.ValidDate);
                                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "FQSJ", serverTime);
                                            cmd.ExecuteNonQuery();
                                            cmd.Parameters.Clear();
                                            #endregion
                                        }
                                    }
                                }
                                if (paymentOfferCouponCalcItemList.Count > 0)
                                {
                                    #region 支付方式返券 更新 HYK_XFJL_ZFFS_FQ, HYK_XFJL_FQ
                                    foreach (CrmPromPaymentOfferCouponCalcItem calcItem in paymentOfferCouponCalcItemList)
                                    {
                                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.ActualOfferMoney, calcItem.OfferCouponMoney))
                                        {
                                            sql.Length = 0;
                                            sql.Append("update HYK_XFJL_ZFFS_FQ set FQJE_SJ = ").Append(calcItem.ActualOfferMoney.ToString("f2"));
                                            sql.Append(" where XFJLID = ").Append(serverBillId);
                                            sql.Append("   and CXID = ").Append(calcItem.PromId);
                                            sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                            sql.Append("   and XFLJFQFS = 0 ");
                                            sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                            sql.Append("   and SHZFFSID = ").Append(calcItem.PayTypeId);
                                            sql.Append("   and BANK_ZFINX = ").Append(calcItem.BankCardInx);
                                            cmd.CommandText = sql.ToString();
                                            cmd.ExecuteNonQuery();
                                            if (calcItem.SpecialType > 0)
                                            {
                                                sql.Length = 0;
                                                sql.Append("update HYK_XFJL_ZFFS_ZSLP set LPSL = ").Append(((int)calcItem.ActualOfferMoney).ToString());
                                                sql.Append(" where XFJLID = ").Append(serverBillId);
                                                sql.Append("   and CXID = ").Append(calcItem.PromId);
                                                sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                                sql.Append("   and XFLJFQFS = 0 ");
                                                sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                                sql.Append("   and SHZFFSID = ").Append(calcItem.PayTypeId);
                                                sql.Append("   and BANK_ZFINX = ").Append(calcItem.BankCardInx);
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    sql.Length = 0;
                                    sql.Append("insert into  HYK_XFJL_FQ(XFJLID,CXID,YHQID,YHQFFGZID,XFLJFQFS,BMDM,DJLX,ZXFJE,SYXFJE,FQJE,FQJE_WX) ");
                                    sql.Append(" select XFJLID,CXID,YHQID,YHQFFGZID,0,' ',2,sum(ZXFJE),sum(SYXFJE),sum(FQJE_SJ),sum(FQJE) ");
                                    sql.Append(" from HYK_XFJL_ZFFS_FQ ");
                                    sql.Append(" where XFJLID = ").Append(billHead.ServerBillId);
                                    sql.Append(" group by XFJLID,CXID,YHQID,YHQFFGZID ");
                                    sql.Append(" having sum(FQJE_SJ) > 0 ");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                    #endregion
                                }
                            }
                            if (billHead.OriginalServerBillId > 0)
                            {
                                #region  选单退货 更新原单退货金额
                                bool isFromBackupTable = true;
                                sql.Length = 0;
                                sql.Append("select count(*) from HYK_XFJL where XFJLID = ").Append(billHead.OriginalServerBillId);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                    isFromBackupTable = (DbUtils.GetInt(reader, 0) == 0);
                                else
                                    isFromBackupTable = false;
                                reader.Close();

                                sql.Length = 0;
                                if (isFromBackupTable)
                                {
                                    sql.Append("update HYXFJL_SP set THJE = THJE - (select XSJE from HYK_XFJL_SP b where HYXFJL_SP.INX = b.INX_OLD and b.XFJLID = ").Append(billHead.ServerBillId).Append(" and b.INX_OLD >= 0)");
                                    sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
                                    sql.Append("   and exists(select * from HYK_XFJL_SP b where HYXFJL_SP.INX = b.INX_OLD and b.XFJLID = ").Append(billHead.ServerBillId).Append(" and b.INX_OLD >= 0)");
                                }
                                else
                                {
                                    sql.Append("update HYK_XFJL_SP set THJE = THJE - (select XSJE from HYK_XFJL_SP b where HYK_XFJL_SP.INX = b.INX_OLD and b.XFJLID = ").Append(billHead.ServerBillId).Append(" and b.INX_OLD >= 0)");
                                    sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
                                    sql.Append("   and exists(select * from HYK_XFJL_SP b where HYK_XFJL_SP.INX = b.INX_OLD and b.XFJLID = ").Append(billHead.ServerBillId).Append(" and b.INX_OLD >= 0)");
                                }
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() == 0) //系统切换前的消费，INX_OLD=-1
                                {
                                    List<CrmArticle> articleList = new List<CrmArticle>();
                                    List<CrmArticle> originalArticleList = new List<CrmArticle>();
                                    sql.Length = 0;
                                    sql.Append("select BMDM,SHSPID,sum(XSJE) from HYK_XFJL_SP where XFJLID = ").Append(billHead.ServerBillId).Append(" group by BMDM,SHSPID ");
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        CrmArticle article = new CrmArticle();
                                        articleList.Add(article);
                                        article.DeptCode = DbUtils.GetString(reader, 0);
                                        article.ArticleId = DbUtils.GetInt(reader, 1);
                                        article.SaleMoney = DbUtils.GetDouble(reader, 2);
                                    }
                                    reader.Close();
                                    billHead.IsFromBackupTable = true;
                                    sql.Length = 0;
                                    sql.Append("select INX,BMDM,SHSPID,XSJE-THJE from HYXFJL_SP where XFJLID = ").Append(billHead.OriginalServerBillId).Append("  and XSJE > THJE ");
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        CrmArticle article = new CrmArticle();
                                        originalArticleList.Add(article);
                                        article.Inx = DbUtils.GetInt(reader, 0);
                                        article.DeptCode = DbUtils.GetString(reader, 1);
                                        article.ArticleId = DbUtils.GetInt(reader, 2);
                                        article.SaleMoney = DbUtils.GetDouble(reader, 3);
                                    }
                                    reader.Close();
                                    if (originalArticleList.Count == 0)
                                    {
                                        billHead.IsFromBackupTable = false;
                                        sql.Length = 0;
                                        sql.Append("select INX,BMDM,SHSPID,XSJE-THJE from HYK_XFJL_SP where XFJLID = ").Append(billHead.OriginalServerBillId).Append("  and XSJE > THJE ");
                                        cmd.CommandText = sql.ToString();
                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            CrmArticle article = new CrmArticle();
                                            originalArticleList.Add(article);
                                            article.Inx = DbUtils.GetInt(reader, 0);
                                            article.DeptCode = DbUtils.GetString(reader, 1);
                                            article.ArticleId = DbUtils.GetInt(reader, 2);
                                            article.SaleMoney = DbUtils.GetDouble(reader, 3);
                                        }
                                        reader.Close();
                                    }
                                    foreach (CrmArticle originalArticle in originalArticleList)
                                    {
                                        foreach (CrmArticle article in articleList)
                                        {
                                            if (originalArticle.DeptCode.Equals(article.DeptCode) && (originalArticle.ArticleId == article.ArticleId))
                                            {
                                                double backMoney = 0;
                                                if (MathUtils.DoubleASmallerThanDoubleB(originalArticle.SaleMoney, -article.SaleMoney))
                                                {
                                                    backMoney = originalArticle.SaleMoney;
                                                    article.SaleMoney += backMoney;
                                                }
                                                else
                                                {
                                                    backMoney = -article.SaleMoney;
                                                    article.SaleMoney = 0;
                                                }
                                                sql.Length = 0;
                                                if (billHead.IsFromBackupTable)
                                                    sql.Append("update HYXFJL_SP set THJE = THJE + ").Append(backMoney.ToString("f2"));
                                                else
                                                    sql.Append("update HYK_XFJL_SP set THJE = THJE + ").Append(backMoney.ToString("f2"));
                                                sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
                                                sql.Append("   and INX = ").Append(originalArticle.Inx);
                                                cmd.CommandText = sql.ToString();
                                                cmd.ExecuteNonQuery();
                                                break;
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        if (billHead.VipId > 0)
                        {
                            #region 更新积分账户
                            sql.Length = 0;
                            sql.Append("update HYK_JFZH set XFJE = XFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",LJXFJE = LJXFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",ZKJE = ZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",LJZKJE = LJZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",WCLJF = WCLJF + ").Append((billCent + offerCent).ToString("f4"));
                            sql.Append(",BQJF = BQJF + ").Append(billCent.ToString("f4"));
                            sql.Append(",LJJF = LJJF + ").Append(billCent.ToString("f4"));
                            sql.Append(",BNLJJF = BNLJJF + ").Append(billCent.ToString("f4"));
                            sql.Append(",JCJF = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JCJF,0) + ").Append(totalBaseCent.ToString("f4"));
                            if (billHead.BillType == 0)
                                sql.Append(",XFCS = XFCS + 1 ");
                            else
                                sql.Append(",THCS = THCS + 1 ");
                            sql.Append(" where HYID =  ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_JFZH(HYID,XFJE,LJXFJE,LJZKJE,ZKJE,WCLJF,BQJF,LJJF,BNLJJF,JCJF,XFCS,THCS) ");
                                sql.Append("  values(").Append(billHead.VipId);
                                sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                                sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                                sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                                sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                                sql.Append(",").Append((billCent + offerCent).ToString("f4"));
                                sql.Append(",").Append(billCent.ToString("f4"));
                                sql.Append(",").Append(billCent.ToString("f4"));
                                sql.Append(",").Append(billCent.ToString("f4"));
                                sql.Append(",").Append(totalBaseCent.ToString("f4"));
                                if (billHead.BillType == 0)
                                    sql.Append(",1,0");
                                else
                                    sql.Append(",0,1");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            sql.Length = 0;
                            sql.Append("update HYK_MDJF set XFJE = XFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",LJXFJE = LJXFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",ZKJE = ZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",LJZKJE = LJZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",WCLJF = WCLJF + ").Append((billCent + offerCent).ToString("f4"));
                            sql.Append(",BQJF = BQJF + ").Append(billCent.ToString("f4"));
                            sql.Append(",LJJF = LJJF + ").Append(billCent.ToString("f4"));
                            sql.Append(",BNLJJF = BNLJJF + ").Append(billCent.ToString("f4"));
                            sql.Append(",JCJF = ").Append(DbUtils.GetIsNullFuncName(dbSysName)).Append("(JCJF,0) + ").Append(totalBaseCent.ToString("f4"));
                            if (billHead.BillType == 0)
                                sql.Append(",XFCS = XFCS + 1 ");
                            else
                                sql.Append(",THCS = THCS + 1 ");
                            sql.Append(" where HYID =  ").Append(billHead.VipId);
                            sql.Append("   and MDID = ").Append(billHead.StoreId);
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_MDJF(HYID,MDID,XFJE,LJXFJE,LJZKJE,ZKJE,WCLJF,BQJF,LJJF,BNLJJF,JCJF,XFCS,THCS) ");
                                sql.Append("  values(").Append(billHead.VipId);
                                sql.Append(",").Append(billHead.StoreId);
                                sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                                sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                                sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                                sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                                sql.Append(",").Append((billCent + offerCent).ToString("f4"));
                                sql.Append(",").Append(billCent.ToString("f4"));
                                sql.Append(",").Append(billCent.ToString("f4"));
                                sql.Append(",").Append(billCent.ToString("f4"));
                                sql.Append(",").Append(totalBaseCent.ToString("f4"));
                                if (billHead.BillType == 0)
                                    sql.Append(",1,0");
                                else
                                    sql.Append(",0,1");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            sql.Length = 0;
                            sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                                vipCent = DbUtils.GetDouble(reader, 0);
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select WCLJF from HYK_MDJF where HYID = ").Append(billHead.VipId).Append(" and MDID = ").Append(billHead.StoreId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                                vipShopCent = DbUtils.GetDouble(reader, 0);
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("insert into HYK_JFBDJLMX(JYBH,CZMD,SKTNO,JLBH,HYID,CLLX,CLSJ,ZY,WCLJFBD,WCLJF) ");
                            sql.Append("  values(").Append(seqJFBDJLMX);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",").Append(billHead.VipId);
                            sql.Append(",").Append(31);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "CLSJ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                            sql.Append(",").Append((billCent + offerCent).ToString("f4"));
                            sql.Append(",").Append(vipCent.ToString("f4"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                            DbUtils.AddStrInputParameterAndValue(cmd, 50, "ZY", "消费后积分", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            sql.Length = 0;
                            sql.Append("insert into HYK_JFBDJLMX_MD(JYBH,MDID,WCLJFBD,WCLJF) ");
                            sql.Append("  values(").Append(seqJFBDJLMX);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",").Append((billCent + offerCent).ToString("f4"));
                            sql.Append(",").Append(vipShopCent.ToString("f4"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                            sql.Length = 0;
                            sql.Append("update HYK_HYXX set ");
                            DbUtils.SpellSqlParameter(conn, sql, "", "ZHXFRQ", "=");
                            sql.Append(",STATUS = (case STATUS when 0 then 1 else STATUS end)");
                            sql.Append(" where HYID = ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZHXFRQ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            #endregion
                        }

                        dbTrans.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTrans.Rollback();
                        throw e;
                    }
                    if ((billHead.BillType == CrmPosData.BillTypeSale) && (offerCouponList.Count > 0))
                    {
                        //返券有限额时
                        if ((offerCouponLimitList != null) || (paymentOfferCouponLimitList != null))
                        {
                            foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                            {
                                offerCoupon.OfferMoney = offerCoupon.ActualOfferMoney;
                            }
                        }
                    }

                    if (billHead.VipId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select sum(a.WCLJF) from HYK_MDJF a,MDDY b where a.MDID = b.MDID and a.HYID = ").Append(billHead.VipId).Append("  and b.SHDM = '").Append(billHead.CompanyCode).Append("'");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            vipCompanyCent = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();
                    }

                    if (billHead.OfferCouponVipId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.INX_XFRQ,b.CXZT,c.YHQMC,d.YHQFFGZMC,a.ZXFJE - a.SYXFJE as XFJE ");
                        sql.Append("from HYK_XFLJDFQ a,CXHDDEF b,YHQDEF c,YHQFFGZ d ");
                        sql.Append("where a.CXID = b.CXID ");
                        sql.Append("  and ((a.INX_XFRQ = 0) or (a.INX_XFRQ = ").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate)).Append("))");
                        sql.Append("  and a.YHQID = c.YHQID ");
                        sql.Append("  and a.YHQFFGZID = d.YHQFFGZID");
                        sql.Append("  and a.HYID = ").Append(billHead.OfferCouponVipId);
                        sql.Append("  and b.SHDM = '").Append(billHead.CompanyCode).Append("'");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ")).Append("  between b.KSSJ and b.JSSJ ");
                        sql.Append("  and a.ZXFJE - a.SYXFJE > 0");
                        sql.Append("  and d.BJ_BFQGZ = 0 ");
                        sql.Append("  and d.BJ_TY = 0 ");
                        sql.Append("  and ((c.FS_FQMDFW <> 3 and a.MDFWDM = '").Append(billHead.CompanyCode).Append("')");
                        sql.Append("   or (c.FS_FQMDFW = 3 and a.MDFWDM = '").Append(billHead.StoreCode).Append("'))");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ", serverTime.Date);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmSaleMoneyLeftWhenPromCalc leftSaleMoney = new CrmSaleMoneyLeftWhenPromCalc();
                            leftSaleMoneyList.Add(leftSaleMoney);
                            if (DbUtils.GetInt(reader, 0) == 0)
                                leftSaleMoney.AddupTypeDesc = "活动期内待返券消费累计";
                            else
                                leftSaleMoney.AddupTypeDesc = "本日待返券消费累计";
                            leftSaleMoney.PromotionName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 40);
                            leftSaleMoney.CouponTypeName = DbUtils.GetString(reader, 2, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                            leftSaleMoney.RuleName = DbUtils.GetString(reader, 3, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                            leftSaleMoney.SaleMoney = DbUtils.GetDouble(reader, 4);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
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
            if ((msg.Length == 0) && (CrmServerPlatform.Config.WXUrl.Length > 0) && (SendWXMessage != null))
            {
                try
                {
                    string outMsg = string.Empty;
                    bool ok = PosProc.DoSendMessageToServer(CrmServerPlatform.Config.WXUrl, SendWXMessage, true, out outMsg);
                    if (!ok)
                        CrmServerPlatform.WriteErrorLog(outMsg);
                }
                catch (Exception e)
                {
                    CrmServerPlatform.WriteErrorLog(e.Message);
                }
            }
            return (msg.Length == 0);
        }

        //取待返券金额
        public static bool GetSaleMoneyLeftWhenOfferCoupon(out string msg, out int vipId, out string vipCode, List<CrmSaleMoneyLeftWhenPromCalc> leftSaleMoneyList, int condType, string condValue, string cardCodeToCheck, string verifyCode, CrmStoreInfo storeInfo)
        {
            msg = string.Empty;
            vipId = 0;
            vipCode = string.Empty;
            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.STATUS,a.PASSWORD,a.BJ_PSW,b.BJ_YHQZH,b.BJ_YZM ");
            sql.Append("from HYK_HYXX a,HYKDEF b ");
            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
            switch (condType)
            {
                case 0:
                    if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                        condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                    sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                    break;
                case 1:
                    sql.Append("  and a.HYID = ").Append(condValue);
                    break;
                case 2:
                    sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                    break;
                default:
                    sql.Append("  and 1=2");
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
                try
                {
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        reader.Close();
                        msg = "会员卡不存在";
                        return false;
                    }
                    int status = DbUtils.GetInt(reader, 3);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "会员卡状态无效";
                        return false;
                    }
                    bool existCashAccount = DbUtils.GetBool(reader, 6);
                    if (!existCashAccount)
                    {
                        reader.Close();
                        msg = "对不起, 该卡没有开通优惠券帐户";
                        return false;
                    }
                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 7);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                    {
                        reader.Close();
                        return false;
                    }
                    vipId = DbUtils.GetInt(reader, 0);
                    //int cardTypeId = DbUtils.GetInt(reader,1);
                    vipCode = DbUtils.GetString(reader, 2);
                    reader.Close();

                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                    sql.Length = 0;
                    sql.Append("select a.INX_XFRQ,b.CXZT,c.YHQMC,d.YHQFFGZMC,a.ZXFJE - a.SYXFJE as XFJE ");
                    sql.Append("from HYK_XFLJDFQ a,CXHDDEF b,YHQDEF c,YHQFFGZ d ");
                    sql.Append("where a.CXID = b.CXID ");
                    sql.Append("  and ((a.INX_XFRQ = 0) or (a.INX_XFRQ = ").Append(DateTimeUtils.GetMyDateIndex(serverTime)).Append("))");
                    sql.Append("  and a.YHQID = c.YHQID ");
                    sql.Append("  and a.YHQFFGZID = d.YHQFFGZID");
                    sql.Append("  and a.HYID = ").Append(vipId);
                    sql.Append("  and b.SHDM = '").Append(storeInfo.Company).Append("'");
                    sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ")).Append("  between b.KSSJ and b.JSSJ ");
                    sql.Append("  and a.ZXFJE - a.SYXFJE > 0");
                    sql.Append("  and d.BJ_BFQGZ = 0 ");
                    sql.Append("  and d.BJ_TY = 0 ");
                    sql.Append("  and ((c.FS_FQMDFW <> 3 and a.MDFWDM = '").Append(storeInfo.Company).Append("')");
                    sql.Append("   or (c.FS_FQMDFW = 3 and a.MDFWDM = '").Append(storeInfo.StoreCode).Append("'))");
                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ", serverTime.Date);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CrmSaleMoneyLeftWhenPromCalc leftSaleMoney = new CrmSaleMoneyLeftWhenPromCalc();
                        leftSaleMoneyList.Add(leftSaleMoney);
                        if (DbUtils.GetInt(reader, 0) == 0)
                            leftSaleMoney.AddupTypeDesc = "活动期内待返券消费累计";
                        else
                            leftSaleMoney.AddupTypeDesc = "本日待返券消费累计";
                        leftSaleMoney.PromotionName = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 40);
                        leftSaleMoney.CouponTypeName = DbUtils.GetString(reader, 2, CrmServerPlatform.Config.DbCharSetIsNotChinese, 30);
                        leftSaleMoney.RuleName = DbUtils.GetString(reader, 3, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                        leftSaleMoney.SaleMoney = DbUtils.GetDouble(reader, 4);
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
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
            return (msg.Length == 0);
        }

        //检查返券的限额
        private static void CheckOfferCouponLimit(DbCommand cmd, StringBuilder sql, List<CrmPromOfferCouponCalcItem> offerCouponCalcItemList, List<CrmPromOfferCoupon> offerCouponList, List<CrmPromOfferCouponLimit> offerCouponLimitList)
        {
            foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
            {
                if (!offerCoupon.IsFromPayment)
                {
                    CrmPromOfferCouponLimit offerLimit = null;
                    foreach (CrmPromOfferCouponLimit offerCouponLimit in offerCouponLimitList)
                    {
                        if ((offerCouponLimit.PromId == offerCoupon.PromId) && (offerCouponLimit.CouponType == offerCoupon.CouponType))
                        {
                            offerLimit = offerCouponLimit;
                            break;
                        }
                    }
                    if ((offerLimit != null) && (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyPeriod, 0)))
                    {
                        #region 活动期内 限额，取已发生额并限制本次发生额
                        DbDataReader reader = null;
                        double doneMoney = 0;
                        bool isExistCXHD_FQZJE = false;
                        sql.Length = 0;
                        sql.Append("select JE from CXHD_FQZJE ");
                        sql.Append("where CXID = ").Append(offerLimit.PromId);
                        sql.Append("  and YHQID = ").Append(offerLimit.CouponType);
                        sql.Append("  and MDID = ").Append(offerLimit.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            isExistCXHD_FQZJE = true;
                            doneMoney = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney + doneMoney, offerLimit.MaxMoneyPeriod))
                        {
                            offerCoupon.ActualOfferMoney = offerLimit.MaxMoneyPeriod - doneMoney;
                            if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                                offerCoupon.ActualOfferMoney = 0;
                        }
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                        {
                            #region 更新 CXHD_FQZJE
                            sql.Length = 0;
                            sql.Append("update CXHD_FQZJE set JE = JE + ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                            sql.Append(" where CXID = ").Append(offerLimit.PromId);
                            sql.Append("   and YHQID = ").Append(offerLimit.CouponType);
                            sql.Append("   and MDID = ").Append(offerLimit.StoreId);
                            sql.Append("   and JE + ").Append(offerCoupon.ActualOfferMoney.ToString("f2")).Append(" <= ").Append(offerLimit.MaxMoneyPeriod.ToString("f2"));
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                if (isExistCXHD_FQZJE)
                                {
                                    offerCoupon.ActualOfferMoney = 0;  //简化处理，不再继续算
                                }
                                else
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into CXHD_FQZJE(CXID,YHQID,MDID,JE) values(");
                                    sql.Append(offerLimit.PromId);
                                    sql.Append(",").Append(offerLimit.CouponType);
                                    sql.Append(",").Append(offerLimit.StoreId);
                                    sql.Append(",").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #region 把返券减少额，分摊到返券计算项
                    if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney, offerCoupon.OfferMoney))
                    {
                        double tmpMoney = offerCoupon.ActualOfferMoney;
                        foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                        {
                            if ((calcItem.PromId == offerCoupon.PromId) && (calcItem.CouponType == offerCoupon.CouponType))
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, calcItem.OfferCouponMoney))
                                {
                                    calcItem.ActualOfferMoney = tmpMoney;
                                    tmpMoney = 0;
                                }
                                else
                                    tmpMoney -= calcItem.OfferCouponMoney;
                            }
                        }
                    }
                    #endregion
                }
            }

            for (int i = offerCouponList.Count - 1; i >= 0; i--)
            {
                if ((!offerCouponList[i].IsFromPayment) && (MathUtils.DoubleAEuqalToDoubleB(offerCouponList[i].ActualOfferMoney, 0)))
                    offerCouponList.RemoveAt(i);
            }
        }

        //检查赠送礼品的限制
        private static void CheckOfferGiftLimit(DbCommand cmd, StringBuilder sql, List<CrmPromOfferCouponCalcItem> offerCouponCalcItemList, List<CrmPromOfferCoupon> offerCouponList, List<CrmPromOfferGiftLimit> offerGiftLimitList, string saleDateStr)
        {
            foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
            {
                if ((!offerCoupon.IsFromPayment) && ((offerCoupon.SpecialType == 1) || (offerCoupon.SpecialType == 2)))
                {
                    CrmPromOfferGiftLimit offerLimit = null;
                    foreach (CrmPromOfferGiftLimit offerGiftLimit in offerGiftLimitList)
                    {
                        if ((offerGiftLimit.PromId == offerCoupon.PromId) && (offerGiftLimit.GiftCode.Equals(offerCoupon.CouponCode)))
                        {
                            offerLimit = offerGiftLimit;
                            break;
                        }
                    }
                    if (offerLimit != null)
                    {
                        #region 活动期内 限额，取已发生额并限制本次发生额
                        DbDataReader reader = null;
                        int doneNum = 0;
                        bool isExistCXHD_ZSLPSL_DAY = false;
                        sql.Length = 0;
                        sql.Append("select SL from CXHD_ZSLPSL_DAY ");
                        sql.Append("where CXID = ").Append(offerLimit.PromId);
                        sql.Append("  and XFRQ = '").Append(saleDateStr).Append("'");
                        sql.Append("  and MDID = ").Append(offerLimit.StoreId);
                        sql.Append("  and LPDM = '").Append(offerLimit.GiftCode).Append("'");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            isExistCXHD_ZSLPSL_DAY = true;
                            doneNum = DbUtils.GetInt(reader, 0);
                        }
                        reader.Close();
                        if (MathUtils.Truncate(offerCoupon.ActualOfferMoney) + doneNum > offerLimit.MaxNumEveryDay)
                        {
                            offerCoupon.ActualOfferMoney = offerLimit.MaxNumEveryDay - doneNum;
                            if (MathUtils.Truncate(offerCoupon.ActualOfferMoney) < 0)
                                offerCoupon.ActualOfferMoney = 0;
                        }
                        if (MathUtils.Truncate(offerCoupon.ActualOfferMoney) > 0)
                        {
                            #region 更新 CXHD_ZSLPSL_DAY
                            sql.Length = 0;
                            sql.Append("update CXHD_ZSLPSL_DAY set SL = SL + ").Append(MathUtils.Truncate(offerCoupon.ActualOfferMoney));
                            sql.Append(" where CXID = ").Append(offerLimit.PromId);
                            sql.Append("   and XFRQ = '").Append(saleDateStr).Append("'");
                            sql.Append("   and MDID = ").Append(offerLimit.StoreId);
                            sql.Append("   and LPDM = '").Append(offerLimit.GiftCode).Append("'");
                            sql.Append("   and SL + ").Append(MathUtils.Truncate(offerCoupon.ActualOfferMoney)).Append(" <= ").Append(offerLimit.MaxNumEveryDay);
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                if (isExistCXHD_ZSLPSL_DAY)
                                {
                                    offerCoupon.ActualOfferMoney = 0;  //简化处理，不再继续算
                                }
                                else
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into CXHD_ZSLPSL_DAY(CXID,XFRQ,MDID,LPDM,SL) values(");
                                    sql.Append(offerLimit.PromId);
                                    sql.Append(",'").Append(saleDateStr);
                                    sql.Append("',").Append(offerLimit.StoreId);
                                    sql.Append(",'").Append(offerLimit.GiftCode);
                                    sql.Append("',").Append(MathUtils.Truncate(offerCoupon.ActualOfferMoney));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #region 把返券减少额，分摊到返券计算项
                    if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney, offerCoupon.OfferMoney))
                    {
                        double tmpMoney = offerCoupon.ActualOfferMoney;
                        foreach (CrmPromOfferCouponCalcItem calcItem in offerCouponCalcItemList)
                        {
                            if ((calcItem.PromId == offerCoupon.PromId) && (calcItem.CouponType == offerCoupon.CouponType))
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, calcItem.OfferCouponMoney))
                                {
                                    calcItem.ActualOfferMoney = tmpMoney;
                                    tmpMoney = 0;
                                }
                                else
                                    tmpMoney -= calcItem.OfferCouponMoney;
                            }
                        }
                    }
                    #endregion
                }
            }

            for (int i = offerCouponList.Count - 1; i >= 0; i--)
            {
                if ((!offerCouponList[i].IsFromPayment) && (MathUtils.DoubleAEuqalToDoubleB(offerCouponList[i].ActualOfferMoney, 0)))
                    offerCouponList.RemoveAt(i);
            }
        }

        //检查银行卡返券的限额
        private static void CheckPaymentOfferCouponLimit(DbCommand cmd, StringBuilder sql, List<CrmPromPaymentOfferCouponCalcItem> paymentOfferCouponCalcItemList, List<CrmPromOfferCoupon> offerCouponList, List<CrmPromPaymentOfferCouponLimit> paymentOfferCouponLimitList, string saleDateStr)
        {
            DbDataReader reader = null;
            int doneTimes = 0;
            double doneMoney = 0;
            List<CrmPromBankCardOfferCoupon> bankCardOfferCouponList = new List<CrmPromBankCardOfferCoupon>();
            foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
            {
                if (offerCoupon.IsFromPayment && (offerCoupon.BankId > 0) && (offerCoupon.BankCardCode.Trim().Length > 0))
                {
                    //同一张银行卡，在一张小票刷多次，合在一起检查限额
                    bool isFound = false;
                    foreach (CrmPromBankCardOfferCoupon offerCoupon2 in bankCardOfferCouponList)
                    {
                        if ((offerCoupon2.PromId == offerCoupon.PromId) && (offerCoupon2.CouponType == offerCoupon.CouponType) && (offerCoupon2.BankId == offerCoupon.BankId) && offerCoupon2.BankCardCode.Equals(offerCoupon.BankCardCode))
                        {
                            isFound = true;
                            offerCoupon2.OfferMoney += offerCoupon.OfferMoney;
                            offerCoupon2.OfferTimes++;
                            bool isFound2 = false;
                            for (int i = 0; i < offerCoupon2.Items.Count; i++)
                            {
                                if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferMoney, offerCoupon2.Items[i].OfferMoney))
                                {
                                    isFound2 = true;
                                    offerCoupon2.Items.Insert(i, offerCoupon);
                                    break;
                                }
                            }
                            if (!isFound2)
                                offerCoupon2.Items.Add(offerCoupon);
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        CrmPromBankCardOfferCoupon offerCoupon2 = new CrmPromBankCardOfferCoupon();
                        bankCardOfferCouponList.Add(offerCoupon2);
                        offerCoupon2.PromId = offerCoupon.PromId;
                        offerCoupon2.CouponType = offerCoupon.CouponType;
                        offerCoupon2.BankId = offerCoupon.BankId;
                        offerCoupon2.BankCardCode = offerCoupon.BankCardCode;
                        offerCoupon2.OfferMoney = offerCoupon.OfferMoney;
                        offerCoupon2.OfferTimes = 1;
                        offerCoupon2.OfferCouponLimit = null;
                        foreach (CrmPromPaymentOfferCouponLimit offerLimit in paymentOfferCouponLimitList)
                        {
                            if ((offerLimit.PromId == offerCoupon2.PromId) && (offerLimit.CouponType == offerCoupon2.CouponType) && (offerLimit.BankId == offerCoupon2.BankId))
                            {
                                offerCoupon2.OfferCouponLimit = offerLimit;
                                break;
                            }
                        }

                        bool isFound2 = false;
                        for (int i = 0; i < offerCoupon2.Items.Count; i++)
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferMoney, offerCoupon2.Items[i].OfferMoney))
                            {
                                isFound2 = true;
                                offerCoupon2.Items.Insert(i, offerCoupon);
                                break;
                            }
                        }
                        if (!isFound2)
                            offerCoupon2.Items.Add(offerCoupon);
                    }
                }
            }

            foreach (CrmPromBankCardOfferCoupon offerCoupon in bankCardOfferCouponList)
            {
                offerCoupon.ActualOfferTimes = offerCoupon.OfferTimes;
                offerCoupon.ActualOfferMoney = offerCoupon.OfferMoney;

                if (offerCoupon.OfferCouponLimit != null)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyOnce, 0) && MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, offerCoupon.OfferCouponLimit.MaxMoneyOnce))
                        offerCoupon.ActualOfferMoney = offerCoupon.OfferCouponLimit.MaxMoneyOnce;

                    if ((offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay > 0) || MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay, 0))
                    {
                        #region 每张银行卡 每天 限额，最大次数，取已发生额并限制本次发生额
                        doneTimes = 0;
                        doneMoney = 0;
                        sql.Length = 0;
                        sql.Append("select JE,CS from YHKFQJE_CARD_DAY ");
                        sql.Append("where CXID = ").Append(offerCoupon.OfferCouponLimit.PromId);
                        sql.Append("  and YHQID = ").Append(offerCoupon.CouponType);
                        sql.Append("  and BANK_KH = '").Append(offerCoupon.BankCardCode).Append("'");
                        sql.Append("  and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                        sql.Append("  and XFRQ = '").Append(saleDateStr).Append("'");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            offerCoupon.IsExistYHKFQJE_CARD_DAY = true;
                            doneMoney = DbUtils.GetDouble(reader, 0);
                            doneTimes = DbUtils.GetInt(reader, 1);
                        }
                        reader.Close();

                        if ((offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay > 0) && (doneTimes + offerCoupon.ActualOfferTimes > offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay))
                        {
                            if ((offerCoupon.ActualOfferTimes <= 1) || (offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay <= doneTimes))
                            {
                                offerCoupon.ActualOfferTimes = 0;
                                offerCoupon.ActualOfferMoney = 0;
                                foreach (CrmPromOfferCoupon offerCoupon2 in offerCoupon.Items)
                                {
                                    offerCoupon2.ActualOfferMoney = 0;
                                }
                            }
                            else
                            {
                                offerCoupon.ActualOfferTimes = offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay - doneTimes;
                                offerCoupon.ActualOfferMoney = 0;
                                for (int i = 0; i < offerCoupon.Items.Count; i++) 
                                {
                                    CrmPromOfferCoupon offerCoupon2 = offerCoupon.Items[i];
                                    if (i + 1 > offerCoupon.ActualOfferTimes)
                                        offerCoupon2.ActualOfferMoney = 0;
                                    else
                                        offerCoupon.ActualOfferMoney += offerCoupon2.ActualOfferMoney;
                                }
                            }
                        }
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay, 0) && MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney + doneMoney, offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay))
                        {
                            offerCoupon.ActualOfferMoney = offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay - doneMoney;
                            if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney,0))
                                offerCoupon.ActualOfferMoney = 0;
                            double tmpMoney = offerCoupon.ActualOfferMoney;
                            foreach (CrmPromOfferCoupon offerCoupon2 in offerCoupon.Items)
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, offerCoupon2.ActualOfferMoney))
                                {
                                    offerCoupon2.ActualOfferMoney = tmpMoney;
                                    tmpMoney = 0;
                                }
                                else
                                    tmpMoney -= offerCoupon2.ActualOfferMoney;
                            }
                        }
                        #endregion
                    }
                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, 0) && ((offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod > 0) || MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod, 0)))
                    {
                        #region 每张银行卡 活动期内 限额，最大次数，取已发生额并限制本次发生额
                        doneTimes = 0;
                        doneMoney = 0;
                        sql.Length = 0;
                        sql.Append("select JE,CS from YHKFQJE_CARD ");
                        sql.Append("where CXID = ").Append(offerCoupon.OfferCouponLimit.PromId);
                        sql.Append("  and YHQID = ").Append(offerCoupon.CouponType);
                        sql.Append("  and BANK_KH = '").Append(offerCoupon.BankCardCode).Append("'");
                        sql.Append("  and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            offerCoupon.IsExistYHKFQJE_CARD = true;
                            doneMoney = DbUtils.GetDouble(reader, 0);
                            doneTimes = DbUtils.GetInt(reader, 1);
                        }
                        reader.Close();

                        if ((offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod > 0) && (doneTimes + offerCoupon.ActualOfferTimes > offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod))
                        {
                            if ((offerCoupon.ActualOfferTimes <= 1) || (offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod <= doneTimes))
                            {
                                offerCoupon.ActualOfferTimes = 0;
                                offerCoupon.ActualOfferMoney = 0;
                                foreach (CrmPromOfferCoupon offerCoupon2 in offerCoupon.Items)
                                {
                                    offerCoupon2.ActualOfferMoney = 0;
                                }
                            }
                            else 
                            {
                                offerCoupon.ActualOfferTimes = (offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod - doneTimes);
                                offerCoupon.ActualOfferMoney = 0;
                                for (int i = 0; i < offerCoupon.Items.Count; i++)
                                {
                                    CrmPromOfferCoupon offerCoupon2 = offerCoupon.Items[i];
                                    if (i + 1 > offerCoupon.ActualOfferTimes)
                                        offerCoupon2.ActualOfferMoney = 0;
                                    else
                                        offerCoupon.ActualOfferMoney += offerCoupon2.ActualOfferMoney;
                                }
                            }
                        }

                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod, 0) && MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney + doneMoney, offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod))
                        {
                            offerCoupon.ActualOfferMoney = offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod - doneMoney;
                            if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                                offerCoupon.ActualOfferMoney = 0;
                            double tmpMoney = offerCoupon.ActualOfferMoney;
                            foreach (CrmPromOfferCoupon offerCoupon2 in offerCoupon.Items)
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, offerCoupon2.ActualOfferMoney))
                                {
                                    offerCoupon2.ActualOfferMoney = tmpMoney;
                                    tmpMoney = 0;
                                }
                                else
                                    tmpMoney -= offerCoupon2.ActualOfferMoney;
                            }
                        }
                        #endregion
                    }
                }
            }

            #region 所有银行卡 限额，取已发生额、限制本次发生额并更新到已发生额
            foreach (CrmPromPaymentOfferCouponLimit offerLimit in paymentOfferCouponLimitList)
            {
                double totalOfferMoney = 0;
                foreach (CrmPromBankCardOfferCoupon offerCoupon in bankCardOfferCouponList)
                {
                    if (offerCoupon.OfferCouponLimit == offerLimit)
                    {
                        totalOfferMoney += offerCoupon.ActualOfferMoney;
                    }
                }
                
                if (MathUtils.DoubleAGreaterThanDoubleB(totalOfferMoney, 0))
                {
                    double actualTotalOfferMoney = totalOfferMoney;
                    bool isExistYHKFQZJE_DAY = false;
                    bool isExistYHKFQZJE = false;
                    if (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyAllCardEveryDay, 0))
                    {
                        #region 所有银行卡 每天 限额，取已发生额并限制本次发生额
                        doneMoney = 0;
                        sql.Length = 0;
                        sql.Append("select JE from YHKFQZJE_DAY ");
                        sql.Append("where CXID = ").Append(offerLimit.PromId);
                        sql.Append("  and YHQID = ").Append(offerLimit.CouponType);
                        sql.Append("  and BANK_ID = ").Append(offerLimit.BankId);
                        sql.Append("  and MDID = ").Append(offerLimit.StoreId);
                        sql.Append("  and XFRQ = '").Append(saleDateStr).Append("'");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            isExistYHKFQZJE_DAY = true;
                            doneMoney = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();

                        if (MathUtils.DoubleAGreaterThanDoubleB(actualTotalOfferMoney + doneMoney, offerLimit.MaxMoneyAllCardEveryDay))
                        {
                            actualTotalOfferMoney = offerLimit.MaxMoneyAllCardEveryDay - doneMoney;
                            if (MathUtils.DoubleASmallerThanDoubleB(actualTotalOfferMoney, 0))
                                actualTotalOfferMoney = 0;
                        }
                        #endregion
                    }

                    if (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyAllCardPeriod, 0))
                    {
                        #region 所有银行卡 活动期内 限额，取已发生额并限制本次发生额
                        doneMoney = 0;
                        sql.Length = 0;
                        sql.Append("select JE from YHKFQZJE ");
                        sql.Append("where CXID = ").Append(offerLimit.PromId);
                        sql.Append("  and YHQID = ").Append(offerLimit.CouponType);
                        sql.Append("  and BANK_ID = ").Append(offerLimit.BankId);
                        sql.Append("  and MDID = ").Append(offerLimit.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            isExistYHKFQZJE = true;
                            doneMoney = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();

                        if (MathUtils.DoubleAGreaterThanDoubleB(actualTotalOfferMoney + doneMoney, offerLimit.MaxMoneyAllCardPeriod))
                        {
                            actualTotalOfferMoney = offerLimit.MaxMoneyAllCardPeriod - doneMoney;
                            if (MathUtils.DoubleASmallerThanDoubleB(actualTotalOfferMoney, 0))
                                actualTotalOfferMoney = 0;
                        }
                        #endregion
                    }
                    if (MathUtils.DoubleAGreaterThanDoubleB(actualTotalOfferMoney, 0))
                    {
                        #region 更新 YHKFQZJE
                        sql.Length = 0;
                        sql.Append("update YHKFQZJE set JE = JE + ").Append(actualTotalOfferMoney.ToString("f2"));
                        sql.Append(" where CXID = ").Append(offerLimit.PromId);
                        sql.Append("   and YHQID = ").Append(offerLimit.CouponType);
                        sql.Append("   and BANK_ID = ").Append(offerLimit.BankId);
                        sql.Append("   and MDID = ").Append(offerLimit.StoreId);
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyAllCardPeriod, 0))
                            sql.Append("  and JE + ").Append(actualTotalOfferMoney.ToString("f2")).Append(" <= ").Append(offerLimit.MaxMoneyAllCardPeriod.ToString("f2"));
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyAllCardPeriod, 0) && isExistYHKFQZJE)
                            {
                                actualTotalOfferMoney = 0;  //简化处理，不再继续算
                            }
                            else
                            {
                                sql.Length = 0;
                                sql.Append("insert into YHKFQZJE(CXID,YHQID,BANK_ID,MDID,JE) values(");
                                sql.Append(offerLimit.PromId);
                                sql.Append(",").Append(offerLimit.CouponType);
                                sql.Append(",").Append(offerLimit.BankId);
                                sql.Append(",").Append(offerLimit.StoreId);
                                sql.Append(",").Append(actualTotalOfferMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                    }

                    if (MathUtils.DoubleAGreaterThanDoubleB(actualTotalOfferMoney, 0))
                    {
                        #region 更新 YHKFQZJE_DAY
                        sql.Length = 0;
                        sql.Append("update YHKFQZJE_DAY set JE = JE + ").Append(actualTotalOfferMoney.ToString("f2"));
                        sql.Append(" where CXID = ").Append(offerLimit.PromId);
                        sql.Append("   and YHQID = ").Append(offerLimit.CouponType);
                        sql.Append("   and BANK_ID = ").Append(offerLimit.BankId);
                        sql.Append("   and MDID = ").Append(offerLimit.StoreId);
                        sql.Append("   and XFRQ = '").Append(saleDateStr).Append("'");
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyAllCardEveryDay, 0))
                            sql.Append("  and JE + ").Append(actualTotalOfferMoney.ToString("f2")).Append(" <= ").Append(offerLimit.MaxMoneyAllCardEveryDay.ToString("f2"));
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(offerLimit.MaxMoneyAllCardEveryDay, 0) && isExistYHKFQZJE_DAY)
                            {
                                sql.Length = 0;
                                sql.Append("update YHKFQZJE set JE = JE - ").Append(actualTotalOfferMoney.ToString("f2"));
                                sql.Append(" where CXID = ").Append(offerLimit.PromId);
                                sql.Append("   and YHQID = ").Append(offerLimit.CouponType);
                                sql.Append("   and BANK_ID = ").Append(offerLimit.BankId);
                                sql.Append("   and MDID = ").Append(offerLimit.StoreId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                actualTotalOfferMoney = 0;  //简化处理，不再继续算
                            }
                            else
                            {
                                sql.Length = 0;
                                sql.Append("insert into YHKFQZJE_DAY(CXID,YHQID,BANK_ID,MDID,XFRQ,JE) values(");
                                sql.Append(offerLimit.PromId);
                                sql.Append(",").Append(offerLimit.CouponType);
                                sql.Append(",").Append(offerLimit.BankId);
                                sql.Append(",").Append(offerLimit.StoreId);
                                sql.Append(",'").Append(saleDateStr).Append("'");
                                sql.Append(",").Append(actualTotalOfferMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                    }

                    if (MathUtils.DoubleASmallerThanDoubleB(actualTotalOfferMoney, totalOfferMoney))
                    {
                        #region 把因所有银行卡的限额造成的返券减少额，分摊到每张银行卡
                        double tmpMoney = actualTotalOfferMoney;
                        foreach (CrmPromBankCardOfferCoupon offerCoupon in bankCardOfferCouponList)
                        {
                            if (offerCoupon.OfferCouponLimit == offerLimit)
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, offerCoupon.ActualOfferMoney))
                                {
                                    offerCoupon.ActualOfferMoney = tmpMoney;
                                    tmpMoney = 0;
                                }
                                else
                                    tmpMoney -= offerCoupon.ActualOfferMoney;
                            }
                        }
                        #endregion 
                    }
                    totalOfferMoney = actualTotalOfferMoney;
                }
            }
            #endregion 

            foreach (CrmPromBankCardOfferCoupon offerCoupon in bankCardOfferCouponList)
            {
                if (offerCoupon.OfferCouponLimit != null)
                {
                    if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney, offerCoupon.OfferMoney))
                    {
                        double tmpMoney = offerCoupon.ActualOfferMoney;
                        offerCoupon.ActualOfferTimes = 0;
                        foreach (CrmPromOfferCoupon offerCoupon2 in offerCoupon.Items)
                        {
                            if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, offerCoupon2.ActualOfferMoney))
                            {
                                offerCoupon2.ActualOfferMoney = tmpMoney;
                                tmpMoney = 0;
                            }
                            else
                                tmpMoney -= offerCoupon2.ActualOfferMoney;
                            if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon2.ActualOfferMoney, 0))
                                offerCoupon.ActualOfferTimes++;
                        }
                    }
                }
            }

            #region 每张银行卡 限额, 更新到已发生额
            foreach (CrmPromBankCardOfferCoupon offerCoupon in bankCardOfferCouponList)
            {
                if (offerCoupon.OfferCouponLimit != null)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                    {
                        #region 更新 YHKFQJE_CARD
                        sql.Length = 0;
                        sql.Append("update YHKFQJE_CARD set CS = CS + ").Append(offerCoupon.ActualOfferTimes).Append(",JE = JE + ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                        sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                        sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                        sql.Append("   and BANK_KH = '").Append(offerCoupon.BankCardCode).Append("'");
                        sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod, 0))
                            sql.Append("  and JE + ").Append(offerCoupon.ActualOfferMoney.ToString("f2")).Append(" <= ").Append(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod.ToString("f2"));
                        if (offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod > 0)
                            sql.Append("  and CS + ").Append(offerCoupon.ActualOfferTimes).Append(" <= ").Append(offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            if ((MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardPeriod, 0) || (offerCoupon.OfferCouponLimit.MaxTimesEveryCardPeriod > 0)) && offerCoupon.IsExistYHKFQJE_CARD)
                            {
                                sql.Length = 0;
                                sql.Append("update YHKFQZJE set JE = JE - ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                                sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                sql.Append("   and BANK_ID = ").Append(offerCoupon.BankId);
                                sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("update YHKFQZJE_DAY set JE = JE - ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                                sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                sql.Append("   and BANK_ID = ").Append(offerCoupon.BankId);
                                sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                                sql.Append("   and XFRQ = '").Append(saleDateStr).Append("'");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                offerCoupon.ActualOfferMoney = 0;  //简化处理，不再继续算
                            }
                            else
                            {
                                sql.Length = 0;
                                sql.Append("insert into YHKFQJE_CARD(CXID,YHQID,BANK_KH,BANK_ID,MDID,JE,CS) values(");
                                sql.Append(offerCoupon.PromId);
                                sql.Append(",").Append(offerCoupon.CouponType);
                                sql.Append(",'").Append(offerCoupon.BankCardCode);
                                sql.Append("',").Append(offerCoupon.BankId);
                                sql.Append(",").Append(offerCoupon.OfferCouponLimit.StoreId);
                                sql.Append(",").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(",").Append(offerCoupon.ActualOfferTimes);
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                    }

                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                    {
                        #region 更新 YHKFQJE_CARD_DAY
                        sql.Length = 0;
                        sql.Append("update YHKFQJE_CARD_DAY set CS = CS + ").Append(offerCoupon.ActualOfferTimes).Append(",JE = JE + ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                        sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                        sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                        sql.Append("   and BANK_KH = '").Append(offerCoupon.BankCardCode).Append("'");
                        sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                        sql.Append("   and XFRQ = '").Append(saleDateStr).Append("'");
                        if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay, 0))
                            sql.Append("  and JE + ").Append(offerCoupon.ActualOfferMoney.ToString("f2")).Append(" <= ").Append(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay.ToString("f2"));
                        if (offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay > 0)
                            sql.Append("  and CS + ").Append(offerCoupon.ActualOfferTimes).Append(" <= ").Append(offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            if ((MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferCouponLimit.MaxMoneyEveryCardEveryDay, 0) || (offerCoupon.OfferCouponLimit.MaxTimesEveryCardEveryDay > 0)) && offerCoupon.IsExistYHKFQJE_CARD_DAY)
                            {
                                sql.Length = 0;
                                sql.Append("update YHKFQZJE set JE = JE - ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                                sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                sql.Append("   and BANK_ID = ").Append(offerCoupon.BankId);
                                sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("update YHKFQZJE_DAY set JE = JE - ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                                sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                sql.Append("   and BANK_ID = ").Append(offerCoupon.BankId);
                                sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                                sql.Append("   and XFRQ = '").Append(saleDateStr).Append("'");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                sql.Length = 0;
                                sql.Append("update YHKFQJE_CARD set CS = CS - ").Append(offerCoupon.ActualOfferTimes).Append(",JE = JE - ").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(" where CXID = ").Append(offerCoupon.PromId);
                                sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                sql.Append("   and BANK_KH = '").Append(offerCoupon.BankCardCode).Append("'");
                                sql.Append("   and MDID = ").Append(offerCoupon.OfferCouponLimit.StoreId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                                offerCoupon.ActualOfferMoney = 0;  //简化处理，不再继续算
                            }
                            else
                            {
                                sql.Length = 0;
                                sql.Append("insert into YHKFQJE_CARD_DAY(CXID,YHQID,BANK_KH,BANK_ID,MDID,XFRQ,JE,CS) values(");
                                sql.Append(offerCoupon.PromId);
                                sql.Append(",").Append(offerCoupon.CouponType);
                                sql.Append(",'").Append(offerCoupon.BankCardCode);
                                sql.Append("',").Append(offerCoupon.BankId);
                                sql.Append(",").Append(offerCoupon.OfferCouponLimit.StoreId);
                                sql.Append(",'").Append(saleDateStr).Append("'");
                                sql.Append(",").Append(offerCoupon.ActualOfferMoney.ToString("f2"));
                                sql.Append(",").Append(offerCoupon.ActualOfferTimes);
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion

            #region 把每张银行卡的返券减少额，分摊到每张单次刷银行卡及其返券计算项
            foreach (CrmPromBankCardOfferCoupon offerCoupon in bankCardOfferCouponList)
            {
                if (offerCoupon.OfferCouponLimit != null)
                {
                    if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon.ActualOfferMoney, offerCoupon.OfferMoney))
                    {
                        double tmpMoney = offerCoupon.ActualOfferMoney;
                        foreach (CrmPromOfferCoupon offerCoupon2 in offerCoupon.Items)
                        {
                            if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney, offerCoupon2.ActualOfferMoney))
                            {
                                offerCoupon2.ActualOfferMoney = tmpMoney;
                                tmpMoney = 0;
                            }
                            else
                                tmpMoney -= offerCoupon2.ActualOfferMoney;

                            if (MathUtils.DoubleASmallerThanDoubleB(offerCoupon2.ActualOfferMoney, offerCoupon2.OfferMoney))
                            {
                                double tmpMoney2 = offerCoupon2.ActualOfferMoney;
                                foreach (CrmPromPaymentOfferCouponCalcItem calcItem in paymentOfferCouponCalcItemList)
                                {
                                    if ((calcItem.PromId == offerCoupon2.PromId) && (calcItem.CouponType == offerCoupon2.CouponType) && (calcItem.BankCardInx == offerCoupon2.BankCardInx))
                                    {
                                        if (MathUtils.DoubleASmallerThanDoubleB(tmpMoney2, calcItem.OfferCouponMoney))
                                        {
                                            calcItem.ActualOfferMoney = tmpMoney2;
                                            tmpMoney2 = 0;
                                        }
                                        else
                                            tmpMoney2 -= calcItem.OfferCouponMoney;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion 

            for (int i = offerCouponList.Count - 1; i >= 0; i--)
            {
                if (MathUtils.DoubleAEuqalToDoubleB(offerCouponList[i].ActualOfferMoney, 0))
                    offerCouponList.RemoveAt(i);
            }
        }

        //选单退货冲正
        public static bool CancelRSaleBackBill(out string msg, int serverBillId)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            CrmRSaleBillHead billHead = null;
            try
            {
                int billStatus = 0;
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
                    sql.Length = 0;
                    sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.HYID,a.HYID_FQ,a.HYID_TQ,a.XFJLID_OLD,a.JZRQ,a.SCSJ,a.XFRQ_FQ,a.SKYDM from HYK_XFJL a ");
                    sql.Append("where a.XFJLID = ").Append(serverBillId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        billStatus = DbUtils.GetInt(reader, 0);
                        billHead = new CrmRSaleBillHead();
                        billHead.CompanyCode = DbUtils.GetString(reader, 1);
                        billHead.StoreId = DbUtils.GetInt(reader, 2);
                        billHead.PosId = DbUtils.GetString(reader, 3);
                        billHead.BillId = DbUtils.GetInt(reader, 4);
                        billHead.BillType = DbUtils.GetInt(reader, 5);
                        billHead.VipId = DbUtils.GetInt(reader, 6);
                        billHead.OfferCouponVipId = DbUtils.GetInt(reader, 7);
                        billHead.PayBackCouponVipId = DbUtils.GetInt(reader, 8);
                        billHead.OriginalServerBillId = DbUtils.GetInt(reader, 9);
                        billHead.AccountDate = DbUtils.GetDateTime(reader, 10);
                        billHead.SaleTime = DbUtils.GetDateTime(reader, 11);
                        billHead.OfferCouponDate = DbUtils.GetDateTime(reader, 12);
                        billHead.Cashier = DbUtils.GetString(reader, 13);
                    }
                    reader.Close();

                    if ((billStatus != CrmPosData.BillStatusPrepareCheckOut) || (billHead == null) || (billHead.OriginalServerBillId == 0))
                        return true;

                    List<CrmCouponPayment> payBackCouponList = new List<CrmCouponPayment>();
                    sql.Length = 0;
                    sql.Append("select HYID,YHQID,CXID,JSRQ,MDFWDM,TQJE from HYTHJL_TQ ");
                    sql.Append("where XFJLID = ").Append(serverBillId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CrmCouponPayment payment = new CrmCouponPayment();
                        payBackCouponList.Add(payment);
                        payment.VipId = DbUtils.GetInt(reader, 0);
                        payment.CouponType = DbUtils.GetInt(reader, 1);
                        payment.PromId = DbUtils.GetInt(reader, 2);
                        payment.ValidDate = DbUtils.GetDateTime(reader, 3);
                        payment.StoreScope = DbUtils.GetString(reader, 4);
                        payment.PayMoney = DbUtils.GetDouble(reader, 5);
                    }
                    reader.Close();

                    List<CrmPromOfferCouponCalcItem> offerBackCouponCalcItemList = null;
                    List<CrmPromOfferCoupon> offerBackCouponList = null;
                    if (billHead.OfferCouponVipId > 0)  //退货时不扣纸券
                    {
                        #region 选单退货---取扣券信息
                        offerBackCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                        sql.Length = 0;
                        sql.Append("select a.CXID,a.YHQID,a.YHQFFGZID,a.XFLJFQFS,a.BMDM,a.MDFWDM_YQ,a.MDFWDM_FQ,a.THJE,a.SYXFJE_NEW,a.KQJE,a.KQJE_SJ,b.YHQSYJSRQ ");
                        if (CrmServerPlatform.Config.IsUpgradedProject2013)
                            sql.Append(",a.CXHDBH ");
                        sql.Append(" from HYTHJL_KQ a,YHQDEF_CXHD b where a.YHQID = b.YHQID and a.CXID = b.CXID and a.XFJLID = ").Append(serverBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                            offerBackCouponCalcItemList.Add(calcItem);
                            calcItem.PromId = DbUtils.GetInt(reader, 0);
                            calcItem.CouponType = DbUtils.GetInt(reader, 1);
                            calcItem.RuleId = DbUtils.GetInt(reader, 2);
                            calcItem.AddupSaleMoneyType = DbUtils.GetInt(reader, 3);
                            calcItem.DeptCode = DbUtils.GetString(reader, 4);
                            calcItem.PayStoreScope = DbUtils.GetString(reader, 5);
                            calcItem.OfferStoreScope = DbUtils.GetString(reader, 6);
                            calcItem.SaleMoney = DbUtils.GetDouble(reader, 7);
                            calcItem.SaleMoneyUsed = DbUtils.GetDouble(reader, 8);
                            calcItem.OfferCouponMoney = DbUtils.GetDouble(reader, 9);  //扣券金额为正
                            calcItem.ActualOfferMoney = DbUtils.GetDouble(reader, 10);
                            calcItem.ValidDate = DbUtils.GetDateTime(reader, 11);
                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                            {
                                if (calcItem.PromId == 0)
                                {
                                    calcItem.PromIdIsBH = true;
                                    calcItem.PromId = DbUtils.GetInt(reader, 12);
                                }
                            }
                        }
                        reader.Close();
                        #endregion

                        if (offerBackCouponCalcItemList.Count > 0)
                        {
                            #region 合计扣券
                            offerBackCouponList = new List<CrmPromOfferCoupon>();
                            foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                            {
                                if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))
                                {
                                    bool isFound = false;
                                    foreach (CrmPromOfferCoupon offerCoupon in offerBackCouponList)
                                    {
                                        if ((calcItem.CouponType == offerCoupon.CouponType) && (calcItem.PromId == offerCoupon.PromId))
                                        {
                                            isFound = true;
                                            offerCoupon.OfferMoney += calcItem.OfferCouponMoney;
                                            offerCoupon.ActualOfferMoney += calcItem.ActualOfferMoney;
                                            break;
                                        }
                                    }
                                    if (!isFound)
                                    {
                                        CrmPromOfferCoupon offerCoupon = new CrmPromOfferCoupon();
                                        offerBackCouponList.Add(offerCoupon);
                                        offerCoupon.CouponType = calcItem.CouponType;
                                        offerCoupon.PromId = calcItem.PromId;
                                        offerCoupon.PromIdIsBH = calcItem.PromIdIsBH;
                                        offerCoupon.PayStoreScope = calcItem.PayStoreScope;
                                        offerCoupon.ValidDate = calcItem.ValidDate;
                                        offerCoupon.OfferMoney = calcItem.OfferCouponMoney;
                                        offerCoupon.ActualOfferMoney = calcItem.ActualOfferMoney;
                                    }
                                }
                            }
                            #endregion
                        }
                    }

                    int seqCount = 0;
                    int seqBegin = 0;
                    int seqInx = 0;
                    if (payBackCouponList != null)
                        seqCount += payBackCouponList.Count;
                    if (offerBackCouponList != null)
                        seqCount += offerBackCouponList.Count;
                    if (seqCount > 0)
                    {
                        string dbSysName = DbUtils.GetDbSystemName(conn);
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqBegin = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId, seqCount) - seqCount + 1;
                    }
                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;
                        sql.Length = 0;
                        sql.Append("update HYK_XFJL set STATUS = ").Append(CrmPosData.BillStatusCancelCheckOut);
                        sql.Append(" where XFJLID = ").Append(serverBillId);
                        sql.Append("   and STATUS = ").Append(CrmPosData.BillStatusPrepareCheckOut);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            if ((billHead.OfferCouponVipId > 0) && (offerBackCouponCalcItemList != null) && (offerBackCouponCalcItemList.Count > 0))
                            {
                                sql.Length = 0;
                                sql.Append("select XFJLID from HYK_XFJL where XFJLID = ").Append(billHead.OriginalServerBillId);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                billHead.IsFromBackupTable = (!reader.Read());
                                reader.Close();

                                foreach (CrmPromOfferCouponCalcItem calcItem in offerBackCouponCalcItemList)
                                {
                                    if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBill) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept))
                                    {
                                        #region 更新 原单 HYK_XFJL_FQ
                                        sql.Length = 0;
                                        if (billHead.IsFromBackupTable)
                                            sql.Append("update HYXFJL_FQ set THJE = THJE - ").Append(calcItem.SaleMoney.ToString("f2"));
                                        else
                                            sql.Append("update HYK_XFJL_FQ set THJE = THJE - ").Append(calcItem.SaleMoney.ToString("f2"));
                                        sql.Append(",SYXFJE_OLD = SYXFJE_OLD - ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                        sql.Append(",KQJE = KQJE - ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                        sql.Append(" where XFJLID = ").Append(billHead.OriginalServerBillId);
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                                            sql.Append("   and CXHDBH = ").Append(calcItem.PromId);
                                        else
                                            sql.Append("   and CXID = ").Append(calcItem.PromId);
                                        sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                                        sql.Append("   and XFLJFQFS = ").Append(calcItem.AddupSaleMoneyType);
                                        sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                                        if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                                            sql.Append("   and BMDM = '").Append(calcItem.DeptCode).Append("'");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                        #endregion
                                    }
                                    if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))
                                    {
                                        #region 更新 HYK_XFLJDFQ
                                        sql.Length = 0;
                                        sql.Append("update HYK_XFLJDFQ set ZXFJE = ZXFJE + ").Append(calcItem.SaleMoney.ToString("f2"));
                                        sql.Append(" ,SYXFJE = SYXFJE + ").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                        sql.Append(" ,FQJE = FQJE + ").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                        sql.Append(" where HYID = ").Append(billHead.OfferCouponVipId);
                                        if (calcItem.OfferStoreScope.Length == 0)
                                            sql.Append("   and MDFWDM = ' '");
                                        else
                                            sql.Append("   and MDFWDM = '").Append(calcItem.OfferStoreScope).Append("'");
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                                            sql.Append("   and CXHDBH = ").Append(calcItem.PromId);
                                        else
                                            sql.Append("   and CXID = ").Append(calcItem.PromId);
                                        if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)
                                            sql.Append("  and INX_XFRQ = ").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate));
                                        else
                                            sql.Append("  and INX_XFRQ = 0");
                                        sql.Append("  and YHQID = ").Append(calcItem.CouponType);
                                        sql.Append("  and YHQFFGZID = ").Append(calcItem.RuleId);
                                        cmd.CommandText = sql.ToString();
                                        if (cmd.ExecuteNonQuery() == 0)
                                        {
                                            sql.Length = 0;
                                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                                sql.Append("insert into HYK_XFLJDFQ(HYID,MDFWDM,CXID,CXHDBH,INX_XFRQ,YHQID,YHQFFGZID,XFRQ,ZXFJE,SYXFJE,FQJE) ");
                                            else
                                                sql.Append("insert into HYK_XFLJDFQ(HYID,MDFWDM,CXID,INX_XFRQ,YHQID,YHQFFGZID,XFRQ,ZXFJE,SYXFJE,FQJE) ");
                                            sql.Append("  values(").Append(billHead.OfferCouponVipId);
                                            if (calcItem.OfferStoreScope.Length == 0)
                                                sql.Append(",' '");
                                            else
                                                sql.Append(",'").Append(calcItem.OfferStoreScope).Append("'");
                                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                            {
                                                if (calcItem.PromIdIsBH)
                                                    sql.Append(",0,").Append(calcItem.PromId);
                                                else
                                                    sql.Append(",").Append(calcItem.PromId).Append(",0");
                                            }
                                            else
                                                sql.Append(",").Append(calcItem.PromId);
                                            if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)
                                                sql.Append(",").Append(DateTimeUtils.GetMyDateIndex(billHead.OfferCouponDate));
                                            else
                                                sql.Append(",0");
                                            sql.Append(",").Append(calcItem.CouponType);
                                            sql.Append(",").Append(calcItem.RuleId);
                                            DbUtils.SpellSqlParameter(conn, sql, ",", "XFRQ", "");
                                            sql.Append(calcItem.SaleMoney.ToString("f2"));
                                            sql.Append(",").Append(calcItem.SaleMoneyUsed.ToString("f2"));
                                            sql.Append(",").Append(calcItem.OfferCouponMoney.ToString("f2"));
                                            sql.Append(")");
                                            cmd.CommandText = sql.ToString();
                                            cmd.Parameters.Clear();
                                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFRQ", billHead.OfferCouponDate);
                                            cmd.ExecuteNonQuery();
                                            cmd.Parameters.Clear();
                                        }
                                        #endregion
                                    }
                                }

                                foreach (CrmPromOfferCoupon offerCoupon in offerBackCouponList)
                                {
                                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.ActualOfferMoney, 0))
                                    {
                                        UpdateVipCouponAccount(out msg, cmd, sql, 10,
                                                                false,
                                                                false,
                                                                billHead.OfferCouponVipId,
                                                                offerCoupon.CouponType,
                                                                (CrmServerPlatform.Config.IsUpgradedProject2013 && offerCoupon.PromIdIsBH ? 0 : offerCoupon.PromId),
                                                                offerCoupon.ValidDate,
                                                                offerCoupon.PayStoreScope,
                                                                offerCoupon.ActualOfferMoney,
                                                                out offerCoupon.Balance,
                                                                seqBegin + seqInx++,
                                                                0,
                                                                billHead.StoreId,
                                                                billHead.PosId,
                                                                billHead.BillId,
                                                                serverTime,
                                                                billHead.AccountDate,
                                                                billHead.Cashier,
                                                                "前台选单退货扣券冲正");
                                        if (msg.Length > 0)
                                        {
                                            dbTrans.Rollback();
                                            return false;
                                        }
                                    }

                                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferMoney, offerCoupon.ActualOfferMoney))
                                        offerCoupon.OfferBackDifference = offerCoupon.OfferMoney - offerCoupon.ActualOfferMoney;
                                    if (MathUtils.DoubleAGreaterThanDoubleB(offerCoupon.OfferBackDifference, 0))
                                    {
                                        #region 扣券 更新 HYK_THKQCE
                                        sql.Length = 0;
                                        sql.Append("update HYK_THKQCE set KQCE = KQCE - ").Append(offerCoupon.OfferBackDifference.ToString("f2"));
                                        sql.Append(" where HYID = ").Append(billHead.OfferCouponVipId);
                                        if (CrmServerPlatform.Config.IsUpgradedProject2013 && offerCoupon.PromIdIsBH)
                                            sql.Append("   and CXHDBH = ").Append(offerCoupon.PromId);
                                        else
                                            sql.Append("   and CXID = ").Append(offerCoupon.PromId);
                                        sql.Append("   and YHQID = ").Append(offerCoupon.CouponType);
                                        if (offerCoupon.PayStoreScope.Length == 0)
                                            sql.Append("   and MDFWDM = ' '");
                                        else
                                            sql.Append("   and MDFWDM = '").Append(offerCoupon.PayStoreScope).Append("'");
                                        cmd.CommandText = sql.ToString();
                                        if (cmd.ExecuteNonQuery() == 0)
                                        {
                                            sql.Length = 0;
                                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                                sql.Append("insert into HYK_THKQCE(HYID,CXID,CXHDBH,YHQID,MDFWDM,KQCE) ");
                                            else
                                                sql.Append("insert into HYK_THKQCE(HYID,CXID,YHQID,MDFWDM,KQCE) ");
                                            sql.Append("   values(").Append(billHead.OfferCouponVipId);
                                            if (CrmServerPlatform.Config.IsUpgradedProject2013)
                                            {
                                                if (offerCoupon.PromIdIsBH)
                                                    sql.Append(",0,").Append(offerCoupon.PromId);
                                                else
                                                    sql.Append(",").Append(offerCoupon.PromId).Append(",0");
                                            }
                                            else
                                                sql.Append(",").Append(offerCoupon.PromId);
                                            sql.Append(",").Append(offerCoupon.CouponType);
                                            if (offerCoupon.PayStoreScope.Length == 0)
                                                sql.Append(",' '");
                                            sql.Append(",'").Append(offerCoupon.PayStoreScope).Append("'");
                                            sql.Append(",").Append((-offerCoupon.OfferBackDifference).ToString("f2"));
                                            sql.Append(")");
                                        }
                                        #endregion
                                    }
                                }

                            }
                            if ((payBackCouponList != null) && (payBackCouponList.Count > 0))
                            {
                                foreach (CrmCouponPayment payment in payBackCouponList)
                                {
                                    cmd.Parameters.Clear();
                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", payment.ValidDate);
                                    sql.Length = 0;
                                    sql.Append("select JE from HYK_YHQZH ");
                                    sql.Append("where HYID = ").Append(payment.VipId);
                                    sql.Append("  and YHQID = ").Append(payment.CouponType);
                                    sql.Append("  and CXID = ").Append(payment.PromId);
                                    DbUtils.SpellSqlParameter(conn, sql, " and ", "JSRQ", "=");
                                    if (payment.StoreScope.Length == 0)
                                        sql.Append("   and MDFWDM = ' '");
                                    else
                                        sql.Append("   and MDFWDM = '").Append(payment.StoreScope).Append("'");
                                    cmd.CommandText = sql.ToString();
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                        payment.Balance = DbUtils.GetDouble(reader, 0);
                                    reader.Close();
                                    if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, payment.Balance))
                                    {
                                        msg = "券余额不足，不能冲正选单退货 " + serverBillId.ToString();
                                        dbTrans.Rollback();
                                        return false;
                                    }

                                    UpdateVipCouponAccount(out msg, cmd, sql, 7,
                                                            false,
                                                            false,
                                                            payment.VipId,
                                                            payment.CouponType,
                                                            payment.PromId,
                                                            payment.ValidDate,
                                                            payment.StoreScope,
                                                            payment.PayMoney,
                                                            out payment.Balance,
                                                            seqBegin + seqInx++,
                                                            0,
                                                            billHead.StoreId,
                                                            billHead.PosId,
                                                            billHead.BillId,
                                                            serverTime,
                                                            billHead.AccountDate,
                                                            billHead.Cashier,
                                                            "前台退货退券冲正");
                                    if (msg.Length > 0)
                                    {
                                        dbTrans.Rollback();
                                        return false;
                                    }
                                }
                            }
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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool CalcRSaleBillCent(out string msg, int situationalMode, CrmRSaleBillHead billHead, List<CrmArticle> articleList, List<CrmPayment> paymentList)
        {
            msg = string.Empty;

            billHead.ServerBillId = 0;
            CrmVipCard vipCard = null;

            if (billHead.StoreId == 0)
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }

            billHead.TotalSaleNum = 0;
            billHead.TotalSaleMoney = 0;
            billHead.TotalDiscMoney = 0;
            billHead.TotalVipDiscMoney = 0;
            foreach (CrmArticle article in articleList)
            {
                billHead.TotalSaleNum = billHead.TotalSaleNum + article.SaleNum;
                billHead.TotalSaleMoney = billHead.TotalSaleMoney + article.SaleMoney;
                billHead.TotalDiscMoney = billHead.TotalDiscMoney + article.DiscMoney;
                billHead.TotalVipDiscMoney = billHead.TotalVipDiscMoney + article.VipDiscMoney;
            }
            billHead.TotalPayMoney = 0;
            billHead.TotalPayCashCardMoney = 0;
            foreach (CrmPayment payment in paymentList)
            {
                payment.PayMoneyNoShare = payment.PayMoney;
                if (billHead.BillType == CrmPosData.BillTypeSale)
                {
                    if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                    {
                        msg = "销售时支付金额 <= 0";
                        break;
                    }
                }
                else
                {
                    if (!MathUtils.DoubleASmallerThanDoubleB(payment.PayMoney, 0))
                    {
                        msg = "退货时支付金额 >= 0";
                        break;
                    }
                }
                billHead.TotalPayMoney = billHead.TotalPayMoney + payment.PayMoney;
                if (payment.IsCashCard)
                    billHead.TotalPayCashCardMoney = billHead.TotalPayCashCardMoney + payment.PayMoney;
            }
            if (msg.Length > 0)
                return false;
            if (!MathUtils.DoubleAEuqalToDoubleB(billHead.TotalSaleMoney, billHead.TotalPayMoney))
            {
                msg = "商品销售总额和收款总额不等";
                return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
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
                    if (situationalMode == 1)
                    {
                        if (billHead.VipId == 0)
                        {
                            msg = "补积分时, 没有传会员";
                            return false;
                        }
                    }

                    int billStatus = 0;
                    int billType2 = 0;
                    int vipId2 = 0;
                    double totalSaleMoney2 = 0;
                    bool articlesAndPaymentsReserved = false;
                    DateTime serverTime = DateTime.MinValue;
                    DateTime crmAccountDate = DateTime.MinValue;
                    List<CrmPaymentArticleShare> paymentArticleShareList = null;
                    List<CrmPromCentMoneyMultipleCalcItem> centMoneyMultipleCalcItemList = null;

                    billHead.IsFromBackupTable = false;
                    sql.Length = 0;
                    sql.Append("select XFJLID,XFJLID_OLD,DJLX,HYID,JE,JF,STATUS from HYK_XFJL ");
                    sql.Append("where MDID = ").Append(billHead.StoreId);
                    sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                    sql.Append("  and JLBH = ").Append(billHead.BillId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        billHead.ServerBillId = DbUtils.GetInt(reader, 0);
                        billHead.OriginalServerBillId = DbUtils.GetInt(reader, 1);
                        billType2 = DbUtils.GetInt(reader, 2);
                        vipId2 = DbUtils.GetInt(reader, 3);
                        totalSaleMoney2 = DbUtils.GetDouble(reader, 4);
                        billHead.TotalGainedCent = DbUtils.GetDouble(reader, 5);
                        //billHead.TotalGainedCentUpgraded = DbUtils.GetDouble(reader, 6);
                        billStatus = DbUtils.GetInt(reader, 6);
                    }
                    else
                    {
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select XFJLID,XFJLID_OLD,DJLX,HYID,JE,CRMJZRQ from HYXFJL ");
                        sql.Append("where MDID = ").Append(billHead.StoreId);
                        sql.Append("  and SKTNO = '").Append(billHead.PosId).Append("'");
                        sql.Append("  and JLBH = ").Append(billHead.BillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            billHead.IsFromBackupTable = true;
                            billHead.ServerBillId = DbUtils.GetInt(reader, 0);
                            billHead.OriginalServerBillId = DbUtils.GetInt(reader, 1);
                            billType2 = DbUtils.GetInt(reader, 2);
                            vipId2 = DbUtils.GetInt(reader, 3);
                            totalSaleMoney2 = DbUtils.GetDouble(reader, 4);
                            crmAccountDate = DbUtils.GetDateTime(reader, 5);
                            billStatus = 1;
                        }
                    }
                    reader.Close();
                    if ((billStatus == 1) || ((billStatus == 2) && (billHead.OriginalServerBillId > 0)))
                    {
                        if ((situationalMode == 0) || (situationalMode == 2))   //正常销售或脱机销售
                        {
                            msg = "该小票在CRM中已结帐";
                            return false;
                        }
                        else if (situationalMode == 1)  //补积分
                        {
                            if (vipId2 > 0)
                                msg = "该小票在CRM中已积分";
                            else if (billHead.BillType != billType2)
                                msg = "该小票在CRM中已结帐，但单据类型不同";
                            else if (!MathUtils.DoubleAEuqalToDoubleB(totalSaleMoney2, billHead.TotalSaleMoney))
                                msg = "该小票在CRM中已结帐，但销售金额不等";

                            if (msg.Length > 0)
                                return false;

                            articlesAndPaymentsReserved = true;
                            articleList.Clear();
                            if (billHead.OriginalServerBillId == 0)
                            {
                                #region 取商品列表
                                sql.Length = 0;
                                if (billHead.IsFromBackupTable)
                                    sql.Append("select a.INX,a.BMDM,a.SHSPID,a.SPDM,a.XSJE,a.XSJE_JF,a.BJ_JF,b.SHSBID,b.SHHTID,c.SPFLDM from HYXFJL_SP a,SHSPXX b,SHSPFL c ");
                                else
                                    sql.Append("select a.INX,a.BMDM,a.SHSPID,a.SPDM,a.XSJE,a.XSJE_JF,a.BJ_JF,b.SHSBID,b.SHHTID,c.SPFLDM from HYK_XFJL_SP a,SHSPXX b,SHSPFL c ");
                                sql.Append(" where a.SHSPID = b.SHSPID");
                                sql.Append("   and b.SHSPFLID = c.SHSPFLID");
                                sql.Append("   and a.XFJLID = ").Append(billHead.ServerBillId);
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    CrmArticle article = new CrmArticle();
                                    articleList.Add(article);
                                    article.Inx = DbUtils.GetInt(reader, 0);
                                    article.DeptCode = DbUtils.GetString(reader, 1);
                                    article.ArticleId = DbUtils.GetInt(reader, 2);
                                    article.ArticleCode = DbUtils.GetString(reader, 3);
                                    article.SaleMoney = DbUtils.GetDouble(reader, 4);
                                    article.SaleMoneyForCent = DbUtils.GetDouble(reader, 5);
                                    article.IsNoCent = (!DbUtils.GetBool(reader, 6));
                                    article.BrandId = DbUtils.GetInt(reader, 7);
                                    article.ContractId = DbUtils.GetInt(reader, 8);
                                    article.CategoryCode = DbUtils.GetString(reader, 9);
                                }
                                reader.Close();
                                #endregion
                            }
                        }
                    }
                    else
                    {
                        #region 验证商品
                        foreach (CrmArticle article in articleList)
                        {
                            sql.Length = 0;
                            sql.Append("select SHSPID from SHSPXX_DM where SHDM = '").Append(billHead.CompanyCode).Append("' and SPDM = '").Append(article.ArticleCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                article.ArticleId = DbUtils.GetInt(reader, 0);
                            }
                            else
                            {
                                msg = string.Format("商品 {0} 还没有上传到 CRM 库", article.ArticleCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;

                            sql.Length = 0;
                            sql.Append("select c.SPFLDM,b.SHSBID,b.SHHTID ");
                            sql.Append(" ,(select GHSDM from SHHT d where b.SHHTID = d.SHHTID) as GHSDM ");
                            sql.Append(" from SHSPXX b,SHSPFL c where b.SHSPFLID = c.SHSPFLID and b.SHSPID = ").Append(article.ArticleId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                article.CategoryCode = DbUtils.GetString(reader, 0);
                                article.BrandId = DbUtils.GetInt(reader, 1);
                                article.ContractId = DbUtils.GetInt(reader, 2);
                                article.SuppCode = DbUtils.GetString(reader, 3);
                            }
                            reader.Close();
                        }

                        if (msg.Length > 0)
                            return false;
                        #endregion

                        #region 验证收款
                        foreach (CrmPayment payment in paymentList)
                        {
                            sql.Length = 0;
                            sql.Append("select SHZFFSID,ZFFSMC,BJ_JF,BJ_FQ,BJ_MBJZ,YHQID from SHZFFS where SHDM = '").Append(billHead.CompanyCode).Append("' and ZFFSDM = '").Append(payment.PayTypeCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                payment.PayTypeId = DbUtils.GetInt(reader, 0);
                                payment.PayTypeName = DbUtils.GetString(reader, 1);
                                payment.JoinCent = DbUtils.GetBool(reader, 2);
                                payment.JoinPromOfferCoupon = DbUtils.GetBool(reader, 3);
                                payment.JoinPromDecMoney = DbUtils.GetBool(reader, 4);
                                if (reader.IsDBNull(5))
                                    payment.CouponType = -1;
                                else
                                    payment.CouponType = DbUtils.GetInt(reader, 5);
                            }
                            else
                            {
                                msg = string.Format("收款方式 {0} 还没有上传到 CRM 库", payment.PayTypeCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;
                        }
                        if (msg.Length > 0)
                            return false;
                        #endregion
                    }

                    //选单退货不用再算积分
                    if ((!articlesAndPaymentsReserved) || (billHead.OriginalServerBillId == 0))
                    {
                        #region 取会员属性
                        if (billHead.VipId > 0)
                        {
                            sql.Length = 0;
                            sql.Append("select a.HYK_NO,a.HYKTYPE,a.FXDW,b.BJ_YHQZH,b.BJ_JF,a.STATUS from HYK_HYXX a,HYKDEF b where a.HYKTYPE = b.HYKTYPE and a.HYID = ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                vipCard = new CrmVipCard();
                                vipCard.CardId = billHead.VipId;
                                vipCard.CardCode = DbUtils.GetString(reader, 0);
                                vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                                vipCard.IssueCardCompanyId = DbUtils.GetInt(reader, 2);
                                vipCard.CanOwnCoupon = (DbUtils.GetBool(reader, 3));
                                //vipCard.CanCent = ((DbUtils.GetInt(reader, 5) != 5) && DbUtils.GetBool(reader, 4));
                                vipCard.CanCent = DbUtils.GetBool(reader, 4);
                            }
                            reader.Close();
                            if (vipCard == null)
                            {
                                msg = string.Format("会员内码 {0} 不存在", billHead.VipId);
                                return false;
                            }
                            else if (!vipCard.CanCent)
                            {
                                msg = string.Format("会员内码 {0} 没有积分账户", billHead.VipId);
                                return false;
                            }
                            if (((billHead.VipCode != null) && (billHead.VipCode.Length > 0) && (!vipCard.CardCode.Equals(billHead.VipCode))))  //子卡
                            {
                                sql.Length = 0;
                                sql.Append("select FXDW from HYK_CHILD_JL where HYK_NO = '").Append(billHead.VipCode).Append("'");
                                cmd.CommandText = sql.ToString();
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    vipCard.IssueCardCompanyId = DbUtils.GetInt(reader, 0);
                                }
                                else
                                    billHead.VipCode = null;
                                reader.Close();
                            }

                            if ((billHead.VipCode == null) || (billHead.VipCode.Length == 0))
                                billHead.VipCode = vipCard.CardCode;
                            billHead.VipType = vipCard.CardTypeId;
                            billHead.IssueCardCompanyId = vipCard.IssueCardCompanyId;

                            sql.Length = 0;
                            sql.Append("select CSRQ,ZYID,ZJLXID,SEX,BJ_CLD from HYK_GRXX where HYID = ").Append(billHead.VipId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                vipCard.Birthday = DbUtils.GetDateTime(reader, 0);
                                vipCard.JobType = DbUtils.GetInt(reader, 1);
                                vipCard.IdCardType = DbUtils.GetInt(reader, 2);
                                vipCard.SexType = DbUtils.GetInt(reader, 3);
                                vipCard.BirthdayIsChinese = DbUtils.GetBool(reader, 4);
                            }
                            reader.Close();
                        }
                        #endregion

                        serverTime = DbUtils.GetDbServerTime(cmd);
                        if ((situationalMode == 0) || ((!articlesAndPaymentsReserved) && (situationalMode == 1)))
                        {
                            if (billHead.SaleTime == DateTime.MinValue)
                                billHead.SaleTime = serverTime;
                            if (billHead.AccountDate == DateTime.MinValue)
                                billHead.AccountDate = billHead.SaleTime.Date;
                        }
                        PromRuleSearcher.LookupVipCentMultiple(out billHead.CentMultiple, out billHead.CentMultiMode, cmd, vipCard, billHead.StoreId, serverTime);
                        PromRuleSearcher.LookupVipCentRuleOfArticle(cmd,0, vipCard, billHead.CompanyCode, serverTime, articleList);

                        //小票在crm中已结账，商品与收款的分摊就不做了
                        if (!articlesAndPaymentsReserved)
                        {
                            #region 商品与收款的分摊
                            paymentArticleShareList = new List<CrmPaymentArticleShare>();
                            if (billHead.BillType == CrmPosData.BillTypeSale)
                            {
                                foreach (CrmArticle article in articleList)
                                {
                                    article.SaleMoneyNoShare = article.SaleMoney;
                                    article.SaleMoneyForCent = article.SaleMoney;
                                }
                                foreach (CrmPayment payment in paymentList)
                                {
                                    payment.PayMoneyNoShare = payment.PayMoney;
                                }
                            }
                            else
                            {
                                foreach (CrmArticle article in articleList)
                                {
                                    article.SaleMoney = -article.SaleMoney;
                                    article.SaleMoneyNoShare = article.SaleMoney;
                                    article.SaleMoneyForCent = article.SaleMoney;
                                }
                                foreach (CrmPayment payment in paymentList)
                                {
                                    payment.PayMoney = -payment.PayMoney;
                                    payment.PayMoneyNoShare = payment.PayMoney;
                                }
                            }
                            if (!PromCalculator.SharePaymentEqually(out msg, articleList, paymentList, paymentArticleShareList))
                                return false;

                            if (billHead.BillType != CrmPosData.BillTypeSale)
                            {
                                foreach (CrmArticle article in articleList)
                                {
                                    article.SaleMoney = -article.SaleMoney;
                                    article.SaleMoneyForCent = -article.SaleMoneyForCent;
                                    article.SaleMoneyForOfferCoupon = -article.SaleMoneyForOfferCoupon;
                                }
                                foreach (CrmPayment payment in paymentList)
                                {
                                    payment.PayMoney = -payment.PayMoney;
                                }
                                foreach (CrmPaymentArticleShare share in paymentArticleShareList)
                                {
                                    share.ShareMoney = -share.ShareMoney;
                                }
                            }
                            #endregion
                        }

                        centMoneyMultipleCalcItemList = new List<CrmPromCentMoneyMultipleCalcItem>();
                        PromCalculator.CalcVipCent(out billHead.TotalGainedCent, billHead.CentMultiple, billHead.CentMultiMode, articleList, centMoneyMultipleCalcItemList);
                    }

                    bool existBill = (billHead.ServerBillId > 0);
                    if (!existBill)
                        billHead.ServerBillId = SeqGenerator.GetSeqHYK_XFJL("CRMDB", CrmServerPlatform.CurrentDbId);
                    int seqJFBDJLMX = SeqGenerator.GetSeqHYK_JFBDJLMX("CRMDB", CrmServerPlatform.CurrentDbId);
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;
                        if (articlesAndPaymentsReserved)
                        {
                            #region 更新HYK_XFJL或HYXFJL
                            sql.Length = 0;
                            if (billHead.IsFromBackupTable)
                                sql.Append("update HYXFJL set BJ_HTBSK = 1,HYID = ").Append(billHead.VipId);
                            else
                                sql.Append("update HYK_XFJL set BJ_HTBSK = 1,HYID = ").Append(billHead.VipId);
                            sql.Append(",JF = ").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",JFBS = ").Append(billHead.CentMultiple.ToString("f2"));
                            sql.Append(",BSFS = ").Append(billHead.CentMultiMode);
                            sql.Append(",BJ_HTBSK = ").Append(situationalMode);
                            if (billStatus == 2)
                                sql.Append(",STATUS = 1");
                            sql.Append(" where XFJLID = ").Append(billHead.ServerBillId);
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                            #endregion
                            if (billHead.IsFromBackupTable)
                            {
                                #region 保存HYXFJL_BSK
                                sql.Length = 0;
                                sql.Append("update HYXFJL_BSK set BJ_JF = 1 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "CRMJZRQ_OLD", "=");
                                sql.Append(" where XFJLID = ").Append(billHead.ServerBillId);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "CRMJZRQ_OLD", crmAccountDate);
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into HYXFJL_BSK(XFJLID,CRMJZRQ_OLD,BJ_JF) ");
                                    sql.Append("  values(").Append(billHead.ServerBillId);
                                    DbUtils.SpellSqlParameter(conn, sql, ",", "CRMJZRQ_OLD", "");
                                    sql.Append(",1");
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                                cmd.Parameters.Clear();
                                #endregion
                            }
                            if (billHead.OriginalServerBillId == 0)
                            {
                                #region 更新HYK_XFJL_SP或HYXFJL_SP
                                foreach (CrmArticle article in articleList)
                                {
                                    sql.Length = 0;
                                    if (billHead.IsFromBackupTable)
                                        sql.Append("update HYXFJL_SP set JF = ").Append(article.GainedCent.ToString("f4"));
                                    else
                                        sql.Append("update HYK_XFJL_SP set JF = ").Append(article.GainedCent.ToString("f4"));
                                    sql.Append(",JFJS = ").Append(article.BaseCent.ToString());
                                    sql.Append(",BS = ").Append(article.CentMultiple);
                                    if (article.CanCentMultiple)
                                        sql.Append(",BJ_JFBS = ").Append(1);
                                    else
                                        sql.Append(",BJ_JFBS = ").Append(0);
                                    sql.Append(",JFDYDBH = ").Append(article.VipCentBillId);
                                    sql.Append(",FTBL = ").Append(article.CentShareRate.ToString("f4"));
                                    sql.Append(" where XFJLID = ").Append(billHead.ServerBillId);
                                    sql.Append("   and INX = ").Append(article.Inx);
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            if (existBill)
                            {
                                DeleteRSaleBillFromDb(cmd, sql, billHead.ServerBillId);
                            }

                            #region 保存 HYK_XFJL
                            sql.Length = 0;
                            sql.Append("insert into HYK_XFJL(XFJLID, SHDM, MDID, SKTNO, JLBH, DJLX,XFJLID_OLD, HYID,HYID_FQ, SKYDM, JZRQ,XFSJ,XFRQ_FQ,SCSJ,STATUS,BJ_WZSP,JFBS,JF,JE,ZK,ZK_HY,PGRYID,VIPTYPE,HYKNO,HYKNO_FQ,FXDW,BSFS,XSSL,BJ_HTBSK)");
                            sql.Append(" values(").Append(billHead.ServerBillId);
                            sql.Append(",'").Append(billHead.CompanyCode);
                            sql.Append("',").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",").Append(billHead.BillType);
                            sql.Append(",").Append(billHead.OriginalServerBillId);
                            sql.Append(",").Append(billHead.VipId);
                            sql.Append(",").Append(billHead.OfferCouponVipId);
                            sql.Append(",'").Append(billHead.Cashier).Append("'");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "XFSJ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "XFRQ_FQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "SCSJ", "");
                            sql.Append(",1,0,");
                            sql.Append(billHead.CentMultiple.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalDiscMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.SaleUserId);
                            sql.Append(",").Append(billHead.VipType);
                            sql.Append(",'").Append(billHead.VipCode);
                            sql.Append("','").Append(billHead.OfferCouponVipCode);
                            sql.Append("',").Append(billHead.IssueCardCompanyId);
                            sql.Append(",").Append(billHead.CentMultiMode);
                            sql.Append(",").Append(billHead.TotalSaleNum.ToString("f4"));
                            sql.Append(",").Append(situationalMode);
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "XFSJ", billHead.SaleTime);
                            DbParameter param = DbUtils.AddDatetimeInputParameter(cmd, "XFRQ_FQ");
                            if (billHead.OfferCouponDate.Year > 2000)
                                param.Value = billHead.OfferCouponDate;
                            else
                                param.Value = null;
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SCSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                            #endregion

                            #region 保存 HYK_XFJL_SP
                            foreach (CrmArticle article in articleList)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_XFJL_SP(XFJLID,INX,SHSPID,SPDM,BMDM,BJ_JF,ZKDYDBH,ZKL,XSSL,XSJE,ZKJE,ZKJE_HY,BJ_BCJHD,JFDYDBH,JFJS,BJ_JFBS,BS,XSJE_JF,JF,FTBL,JFFBGZ)");
                                sql.Append(" values(").Append(billHead.ServerBillId);
                                sql.Append(",").Append(article.Inx);
                                sql.Append(",").Append(article.ArticleId);
                                sql.Append(",'").Append(article.ArticleCode);
                                sql.Append("','").Append(article.DeptCode);
                                if (article.IsNoCent)
                                    sql.Append("',").Append(0);
                                else
                                    sql.Append("',").Append(1);
                                sql.Append(",").Append(article.VipDiscBillId);
                                sql.Append(",").Append(article.VipDiscRate.ToString("f4"));
                                sql.Append(",").Append(article.SaleNum.ToString("f4"));
                                sql.Append(",").Append(article.SaleMoney.ToString("f2"));
                                sql.Append(",").Append(article.DiscMoney.ToString("f2"));
                                sql.Append(",").Append(article.VipDiscMoney.ToString("f2"));
                                if (article.IsNoProm)
                                    sql.Append(",").Append(1);
                                else
                                    sql.Append(",").Append(0);
                                sql.Append(",").Append(article.VipCentBillId);
                                sql.Append(",").Append(article.BaseCent.ToString());
                                if (article.CanCentMultiple)
                                    sql.Append(",").Append(1);
                                else
                                    sql.Append(",").Append(0);
                                sql.Append(",").Append(article.CentMultiple);
                                sql.Append(",").Append(article.SaleMoneyForCent.ToString("f2"));
                                sql.Append(",").Append(article.GainedCent.ToString("f4"));
                                sql.Append(",").Append(article.CentShareRate.ToString("f4"));
                                sql.Append(",").Append(article.CentMoneyMultipleRuleId);
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            #endregion

                            #region 保存 HYK_XFJL_ZFFS
                            for (int inx = 0; inx < paymentList.Count(); inx++)
                            {
                                CrmPayment payment = paymentList[inx];
                                sql.Length = 0;
                                sql.Append("insert into HYK_XFJL_ZFFS(XFJLID,INX,ZFFSID,ZFFSDM,BJ_JF,BJ_FQ,YHQID,JE )");
                                sql.Append(" values(").Append(billHead.ServerBillId);
                                sql.Append(",").Append(inx);
                                sql.Append(",").Append(payment.PayTypeId);
                                sql.Append(",'").Append(payment.PayTypeCode);
                                if (payment.JoinCent)
                                    sql.Append("',1");
                                else
                                    sql.Append("',0");
                                if (payment.JoinPromOfferCoupon)
                                    sql.Append(",1");
                                else
                                    sql.Append(",0");
                                if (payment.CouponType >= 0)
                                    sql.Append(",").Append(payment.CouponType);
                                else
                                    sql.Append(",null");
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            #endregion

                            #region 保存 HYK_XFJL_YHKZF
                            for (int inx = 0; inx < paymentList.Count(); inx++)
                            {
                                CrmPayment payment = paymentList[inx];
                                if ((payment.BankCardList != null) && (payment.BankCardList.Count > 0))
                                {
                                    foreach (CrmBankCardPayment bankCard in payment.BankCardList)
                                    {
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_XFJL_YHKZF(XFJLID,INX,BANK_KH,BANK_ID,SHZFFSID,JE )");
                                        sql.Append(" values(").Append(billHead.ServerBillId);
                                        sql.Append(",").Append(bankCard.Inx);
                                        sql.Append(",'").Append(bankCard.BankCardCode);
                                        sql.Append("',").Append(0);   //bankCard.BankId
                                        sql.Append(",").Append(payment.PayTypeId);
                                        sql.Append(",").Append(bankCard.PayMoney.ToString("f2"));
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            #endregion

                            #region 保存 HYK_XFJL_SP_ZFFS
                            if (paymentArticleShareList != null)
                            {
                                foreach (CrmPaymentArticleShare share in paymentArticleShareList)
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_XFJL_SP_ZFFS(XFJLID,INX,SHZFFSID,BJ_JF,BJ_FQ,YHQID,JE)");
                                    sql.Append(" values(").Append(billHead.ServerBillId);
                                    sql.Append(",").Append(share.Article.Inx);
                                    sql.Append(",").Append(share.Payment.PayTypeId);
                                    if (share.Payment.JoinCent)
                                        sql.Append(",1");
                                    else
                                        sql.Append(",0");
                                    if (share.JoinPromOfferCoupon)
                                        sql.Append(",1");
                                    else
                                        sql.Append(",0");
                                    if (share.Payment.CouponType >= 0)
                                        sql.Append(",").Append(share.Payment.CouponType);
                                    else
                                        sql.Append(",null");
                                    sql.Append(",").Append(share.ShareMoney.ToString("f2"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            #endregion
                        }
                        

                        if ((centMoneyMultipleCalcItemList != null) && (centMoneyMultipleCalcItemList.Count > 0))
                        {
                            foreach (CrmPromCentMoneyMultipleCalcItem calcItem in centMoneyMultipleCalcItemList)
                            {
                                if (calcItem.Ok)
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_XFJFBS(XFJLID,GZID,XFJE_JF,JF,XFJE_JF_OLD,JF_OLD) ");
                                    sql.Append("  values(").Append(billHead.ServerBillId);
                                    sql.Append(",").Append(calcItem.RuleId);
                                    sql.Append(",").Append(calcItem.SaleMoney.ToString("f2"));
                                    sql.Append(",").Append(calcItem.GainedCent.ToString("f4"));
                                    sql.Append(",0,0");
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        #region 更新积分账户
                        sql.Length = 0;
                        sql.Append("update HYK_JFZH set XFJE = XFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                        sql.Append(",LJXFJE = LJXFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                        sql.Append(",ZKJE = ZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                        sql.Append(",LJZKJE = LJZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                        sql.Append(",WCLJF = WCLJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",BQJF = BQJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",LJJF = LJJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",BNLJJF = BNLJJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        if (billHead.BillType == 0)
                            sql.Append(",XFCS = XFCS + 1 ");
                        else
                            sql.Append(",THCS = THCS + 1 ");
                        sql.Append(" where HYID =  ").Append(billHead.VipId);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            sql.Length = 0;
                            sql.Append("insert into HYK_JFZH(HYID,XFJE,LJXFJE,LJZKJE,ZKJE,WCLJF,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                            sql.Append("  values(").Append(billHead.VipId);
                            sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            if (billHead.BillType == 0)
                                sql.Append(",1,0");
                            else
                                sql.Append(",0,1");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                        }
                        sql.Length = 0;
                        sql.Append("update HYK_MDJF set XFJE = XFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                        sql.Append(",LJXFJE = LJXFJE + ").Append(billHead.TotalSaleMoney.ToString("f2"));
                        sql.Append(",ZKJE = ZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                        sql.Append(",LJZKJE = LJZKJE + ").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                        sql.Append(",WCLJF = WCLJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",BQJF = BQJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",LJJF = LJJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",BNLJJF = BNLJJF + ").Append(billHead.TotalGainedCent.ToString("f4"));
                        if (billHead.BillType == 0)
                            sql.Append(",XFCS = XFCS + 1 ");
                        else
                            sql.Append(",THCS = THCS + 1 ");
                        sql.Append(" where HYID =  ").Append(billHead.VipId);
                        sql.Append("   and MDID = ").Append(billHead.StoreId);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            sql.Length = 0;
                            sql.Append("insert into HYK_MDJF(HYID,MDID,XFJE,LJXFJE,LJZKJE,ZKJE,WCLJF,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                            sql.Append("  values(").Append(billHead.VipId);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalSaleMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalVipDiscMoney.ToString("f2"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                            if (billHead.BillType == 0)
                                sql.Append(",1,0");
                            else
                                sql.Append(",0,1");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                        }
                        double validCent = 0;
                        double storeValidCent = 0;
                        sql.Length = 0;
                        sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(billHead.VipId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            validCent = DbUtils.GetDouble(reader, 0);
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select WCLJF from HYK_MDJF where HYID = ").Append(billHead.VipId).Append(" and MDID = ").Append(billHead.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                            storeValidCent = DbUtils.GetDouble(reader, 0);
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("insert into HYK_JFBDJLMX(JYBH,CZMD,SKTNO,JLBH,HYID,CLLX,CLSJ,ZY,WCLJFBD,WCLJF) ");
                        sql.Append("  values(").Append(seqJFBDJLMX);
                        sql.Append(",").Append(billHead.StoreId);
                        sql.Append(",'").Append(billHead.PosId);
                        sql.Append("',").Append(billHead.BillId);
                        sql.Append(",").Append(billHead.VipId);
                        sql.Append(",").Append(31);
                        DbUtils.SpellSqlParameter(conn, sql, ",", "CLSJ", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                        sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",").Append(validCent.ToString("f4"));
                        sql.Append(")");
                        cmd.CommandText = sql.ToString();
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                        DbUtils.AddStrInputParameterAndValue(cmd, 50, "ZY", "消费后积分", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        sql.Length = 0;
                        sql.Append("insert into HYK_JFBDJLMX_MD(JYBH,MDID,WCLJFBD,WCLJF) ");
                        sql.Append("  values(").Append(seqJFBDJLMX);
                        sql.Append(",").Append(billHead.StoreId);
                        sql.Append(",").Append(billHead.TotalGainedCent.ToString("f4"));
                        sql.Append(",").Append(storeValidCent.ToString("f4"));
                        sql.Append(")");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                        sql.Length = 0;
                        sql.Append("update HYK_HYXX set ");
                        DbUtils.SpellSqlParameter(conn, sql, "", "ZHXFRQ", "=");
                        sql.Append(",STATUS = (case STATUS when 0 then 1 else STATUS end)");
                        sql.Append(" where HYID = ").Append(billHead.VipId);
                        cmd.CommandText = sql.ToString();
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZHXFRQ", serverTime);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        #endregion

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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }

            return (msg.Length == 0);
        }

        //取代码券
        public static bool GetCodedCoupon(out string msg, out int couponType, out double balance, out double limitMoney, string couponCode, int serverBillId)
        {
            msg = string.Empty;
            couponType = -1;
            balance = 0;
            limitMoney = 0;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select YHQID,YHQMZ,BEGINDATE,ENDDATE,STATUS ");
            sql.Append("from YHQCODE ");
            sql.Append("where YHQCODE = '").Append(couponCode).Append("'");
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
                    if (!reader.Read())
                    {
                        reader.Close();
                        msg = "该优惠券代码 不存在";
                        return false;
                    }
                    couponType = DbUtils.GetInt(reader, 0);
                    balance = DbUtils.GetDouble(reader, 1);
                    DateTime beginDate = DbUtils.GetDateTime(reader, 2);
                    DateTime endDate = DbUtils.GetDateTime(reader, 3);
                    int status = DbUtils.GetInt(reader, 4);
                    reader.Close();

                    if (status < 0)
                    {
                        msg = "该优惠券代码 状态无效";
                    }
                    else if (status == 2)
                    {
                        msg = "该优惠券代码 已冻结";
                    }
                    else
                    {
                        DateTime serverDate = DbUtils.GetDbServerTime(cmd).Date;
                        if ((serverDate.CompareTo(beginDate) < 0) || (serverDate.CompareTo(endDate) > 0))
                        {
                            msg = "该优惠券代码 有效期 " + beginDate.ToString("yyyy-MM-dd") + " --- " + endDate.ToString("yyyy-MM-dd");
                        }
                    }
                    if ((msg.Length == 0) && (serverBillId > 0))
                    {
                        List<CrmCoupon> couponList = new List<CrmCoupon>();
                        CrmCoupon coupon = new CrmCoupon();
                        couponList.Add(coupon);
                        coupon.CouponType = couponType;
                        List<CrmCouponPayLimit> payLimitList = new List<CrmCouponPayLimit>();
                        if (DoGetVipCouponPayLimit(out msg, conn, cmd, null, serverBillId, couponList, payLimitList, true))
                        {
                            if (payLimitList.Count > 0)
                            {
                                CrmCouponPayLimit payLimit = payLimitList[0];
                                limitMoney = payLimit.LimitMoney;
                            }
                        }
                    }
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
            return (msg.Length == 0);
        }

        //准备代码券支付交易
        public static bool PrepareTransCodedCouponPayment(out string msg, out int transId, CrmRSaleBillHead billHead, List<string> couponCodeList)
        {
            msg = string.Empty;
            transId = 0;
            if ((billHead.ServerBillId <= 0) && (billHead.BillId <= 0))
                return false;

            if ((billHead.BillId > 0) && (billHead.StoreId == 0))
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                storeInfo.StoreCode = billHead.StoreCode;
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
                billHead.CompanyCode = storeInfo.Company;
                billHead.StoreCode = storeInfo.StoreCode;
                billHead.StoreId = storeInfo.StoreId;
            }
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            double totalMoney = 0;
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
                    if (billHead.ServerBillId > 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.STATUS,a.SHDM,a.MDID,a.SKTNO,a.JLBH,a.DJLX,a.JZRQ,a.SKYDM from HYK_XFJL a ");
                        sql.Append("where XFJLID = ").Append(billHead.ServerBillId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int status = DbUtils.GetInt(reader, 0);
                            if (status == 1)
                                msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备代码券交易", billHead.ServerBillId);
                            else
                            {
                                billHead.CompanyCode = DbUtils.GetString(reader, 1);
                                billHead.StoreId = DbUtils.GetInt(reader, 2);
                                billHead.PosId = DbUtils.GetString(reader, 3);
                                billHead.BillId = DbUtils.GetInt(reader, 4);
                                billHead.BillType = DbUtils.GetInt(reader, 5);
                                billHead.AccountDate = DbUtils.GetDateTime(reader, 6);
                                billHead.Cashier = DbUtils.GetString(reader, 7);
                            }
                        }
                        else
                        {
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select SHDM,MDID,SKTNO,JLBH,JZRQ,SKYDM from HYXFJL ");
                            sql.Append("where XFJLID = ").Append(billHead.ServerBillId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                                msg = string.Format("该销售单 {0} 在 CRM 中已结帐,不能准备代码券交易", billHead.ServerBillId);
                            else
                                msg = string.Format("该销售单 {0} 在 CRM 中不存在,不能准备代码券交易", billHead.ServerBillId);
                        }
                        reader.Close();

                        if (msg.Length == 0)
                        {
                            sql.Length = 0;
                            sql.Append("select JYZT from HYK_JYCL ");
                            sql.Append("where JYLX = 1 ");
                            sql.Append("  and XFJLID = ").Append(billHead.ServerBillId);
                            sql.Append("  and BJ_YHQ = 2");
                            sql.Append("  and JYZT in (1,2)");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                int status = DbUtils.GetInt(reader, 0);
                                if ((status == 1) || (status == 2))
                                    msg = string.Format("该销售单 {0} 在 CRM 中已有未完成的代码券交易, 请先冲正", billHead.ServerBillId);
                            }
                            reader.Close();
                        }
                    }
                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                    List<CrmCodedCouponPayment> paymentList = new List<CrmCodedCouponPayment>();
                    if (msg.Length == 0)
                    {
                        DateTime serverDate = serverTime.Date;
                        foreach (string couponCode in couponCodeList)
                        {
                            sql.Length = 0;
                            sql.Append("select YHQID,YHQMZ,BEGINDATE,ENDDATE,CXID,STATUS ");
                            sql.Append("from YHQCODE ");
                            sql.Append("where YHQCODE = '").Append(couponCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = string.Format("代码券 {0} 不存在", couponCode);
                                return false;
                            }
                            int couponType = DbUtils.GetInt(reader, 0);
                            double balance = DbUtils.GetDouble(reader, 1);
                            DateTime beginDate = DbUtils.GetDateTime(reader, 2);
                            DateTime endDate = DbUtils.GetDateTime(reader, 3);
                            int promId = DbUtils.GetInt(reader, 4);
                            int status = DbUtils.GetInt(reader, 5);
                            reader.Close();

                            if (status < 0)
                            {
                                msg = string.Format("代码券 {0} 状态无效", couponCode);
                            }
                            else if (status == 2)
                            {
                                msg = string.Format("代码券 {0} 已冻结", couponCode);
                            }
                            else
                            {
                                if ((serverDate.CompareTo(beginDate) < 0) || (serverDate.CompareTo(endDate) > 0))
                                {
                                    msg = string.Format("代码券 {0} 有效期 " + beginDate.ToString("yyyy-MM-dd") + " --- " + endDate.ToString("yyyy-MM-dd"), couponCode);
                                }
                            }
                            if (msg.Length > 0)
                                return false;

                            CrmCodedCouponPayment payment = new CrmCodedCouponPayment();
                            paymentList.Add(payment);
                            payment.CouponType = couponType;
                            payment.CouponCode = couponCode;
                            payment.PayMoney = balance;
                            payment.PromId = promId;
                            totalMoney += payment.PayMoney;
                        }
                    }
                    if (msg.Length == 0)
                    {
                        transId = SeqGenerator.GetSeqHYK_JYCL("CRMDB", CrmServerPlatform.CurrentDbId);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL(JYID,JYLX,XFJLID,MDID,SKTNO,JLBH,SKYDM,BJ_YHQ,JYZT,JZRQ,ZBSJ,JYJE)");
                            sql.Append(" values(").Append(transId);
                            sql.Append(",").Append(1);
                            sql.Append(",").Append(billHead.ServerBillId);
                            sql.Append(",").Append(billHead.StoreId);
                            sql.Append(",'").Append(billHead.PosId);
                            sql.Append("',").Append(billHead.BillId);
                            sql.Append(",'").Append(billHead.Cashier);
                            sql.Append("',2,1");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(",").Append(totalMoney.ToString("f2"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", billHead.AccountDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_JYCL_ZBZT(JYID,ZBSJ)");
                            sql.Append(" values(").Append(transId);
                            DbUtils.SpellSqlParameter(conn, sql, ",", "ZBSJ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZBSJ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            foreach (CrmCodedCouponPayment payment in paymentList)
                            {
                                sql.Length = 0;
                                sql.Append("update YHQCODE set STATUS = 2,MDID_YQ = ").Append(billHead.StoreId);
                                DbUtils.SpellSqlParameter(conn, sql, ",", "YQSJ", "=");
                                sql.Append(" where YHQCODE = '").Append(payment.CouponCode).Append("'");
                                sql.Append("   and STATUS = 1 ");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "YQSJ", serverTime);
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    cmd.Parameters.Clear();
                                    msg = string.Format("代码券 {0} 状态不对", payment.CouponCode);
                                    break;
                                }
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("insert into HYK_JYCLITEM_YHQDM(JYID,YHQCODE,YHQID,YHQMZ,CXID)");
                                sql.Append(" values(").Append(transId);
                                sql.Append(",'").Append(payment.CouponCode).Append("'");
                                sql.Append(",").Append(payment.CouponType);
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                sql.Append(",").Append(payment.PromId);
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //确认代码券支付交易
        public static bool ConfirmTransCodedCouponPayment(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            //int shopId = 0;
            //string posId = string.Empty;
            //int billId = 0;
            //DateTime accountDate = DateTime.MinValue;
            //string cashier = string.Empty;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_YHQ,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("确认代码券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != 1)
                            msg = string.Format("确认代码券交易({0})不是支付交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 2)
                            msg = string.Format("确认交易({0})没有代码券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("确认代码券交易({0})的金额不匹配", transId);
                        else
                        {
                            transStatus = DbUtils.GetInt(reader, 4);
                            if (transStatus == 1)
                            {
                                //shopId = DbUtils.GetInt(reader, 5);
                                //posId = DbUtils.GetString(reader, 6);
                                //billId = DbUtils.GetInt(reader, 7);
                                //cashier = DbUtils.GetString(reader, 8);
                                //accountDate = DbUtils.GetDateTime(reader, 9);
                            }
                            else if (transStatus != 2)
                            {
                                msg = string.Format("确认代码券交易({0}) 已取消", transId);
                            }
                        }
                    }
                    else
                    {
                        msg = string.Format("确认代码券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && (transStatus == 1))
                    {
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

                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            sql.Append("update HYK_JYCL set JYZT = 2 ");
                            DbUtils.SpellSqlParameter(conn, sql, ",", "TJSJ", "=");
                            sql.Append(" where JYID = ").Append(transId);
                            sql.Append("   and JYZT = 1");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "TJSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();
                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "YQSJ", serverTime);
                                foreach (string couponCode in couponCodeList)
                                {
                                    sql.Length = 0;
                                    sql.Append("update YHQCODE set STATUS = -1 ");
                                    DbUtils.SpellSqlParameter(conn, sql, ",", "YQSJ", "=");
                                    sql.Append(" where YHQCODE = '").Append(couponCode).Append("'");
                                    sql.Append("   and STATUS = 2 ");
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                    {
                                        cmd.Parameters.Clear();
                                        msg = string.Format("代码券 {0} 状态不对", couponCode);
                                        break;
                                    }
                                }
                                cmd.Parameters.Clear();
                            }
                            cmd.Parameters.Clear();
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        //取消代码券支付交易
        public static bool CancelTransCodedCouponPayment(out string msg, int transId, int serverBillId, double transMoney)
        {
            msg = string.Empty;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int transStatus = 0;
            //int shopId = 0;
            //string posId = string.Empty;
            //int billId = 0;
            //DateTime accountDate = DateTime.MinValue;
            //string cashier = string.Empty;
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
                    sql.Length = 0;
                    sql.Append("select XFJLID,JYLX,BJ_YHQ,JYJE,JYZT,MDID,SKTNO,JLBH,SKYDM,JZRQ from HYK_JYCL ");
                    sql.Append("where JYID = ").Append(transId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if ((serverBillId > 0) && (DbUtils.GetInt(reader, 0) != serverBillId))
                            msg = string.Format("取消代码券交易({0})的Crm消费记录号不匹配", transId);
                        else if (DbUtils.GetInt(reader, 1) != 1)
                            msg = string.Format("取消代码券交易({0})不是支付交易", transId);
                        else if (DbUtils.GetInt(reader, 2) != 2)
                            msg = string.Format("取消交易({0})没有代码券", transId);
                        else if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 3), transMoney))
                            msg = string.Format("取消代码券交易({0})的金额不匹配", transId);
                        else
                        {
                            transStatus = DbUtils.GetInt(reader, 4);
                            if ((transStatus == 1) || (transStatus == 2))
                            {
                                if (serverBillId <= 0)
                                    serverBillId = DbUtils.GetInt(reader, 0);
                                //shopId = DbUtils.GetInt(reader, 5);
                                //posId = DbUtils.GetString(reader, 6);
                                //billId = DbUtils.GetInt(reader, 7);
                                //cashier = DbUtils.GetString(reader, 8);
                                //accountDate = DbUtils.GetDateTime(reader, 9);
                            }
                        }
                    }
                    else
                    {
                        //msg = string.Format("确认代码券交易({0})不存在", transId);
                    }
                    reader.Close();

                    if ((msg.Length == 0) && ((transStatus == 1) || (transStatus == 2)))
                    {
                        if (serverBillId >= 0)
                        {
                            //判断已结账
                        }
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

                        DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            sql.Length = 0;
                            if (transStatus == 1)
                            {
                                sql.Append("update HYK_JYCL set JYZT = 3 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 1");
                            }
                            else
                            {
                                sql.Append("update HYK_JYCL set JYZT = 4 ");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QXSJ", "=");
                                sql.Append(" where JYID = ").Append(transId);
                                sql.Append("   and JYZT = 2");
                            }
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "QXSJ", serverTime);
                            if (cmd.ExecuteNonQuery() != 0)
                            {
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYK_JYCL_ZBZT where JYID = ").Append(transId);
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                foreach (string couponCode in couponCodeList)
                                {
                                    sql.Length = 0;
                                    sql.Append("update YHQCODE set STATUS = 1,MDID_YQ = null,YQSJ = null ");
                                    sql.Append(" where YHQCODE = '").Append(couponCode).Append("'");
                                    sql.Append("   and STATUS in (-1,2) ");
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                    {
                                        msg = string.Format("代码券 {0} 状态不对", couponCode);
                                        break;
                                    }
                                }
                            }
                            cmd.Parameters.Clear();
                            if (msg.Length == 0)
                                dbTrans.Commit();
                            else
                                dbTrans.Rollback();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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

            return (msg.Length == 0);
        }

        public static bool GetPayAccount(out string msg, List<CrmPayAccount> accountList, bool isDetail, CrmStoreInfo storeInfo, string posId, string cashier, DateTime accountDate, int beginBillId, int endBillId)
        {
            msg = string.Empty;

            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    if (isDetail)
                        sql.Append("select JLBH,sum(DFJE) as XFJE from HYK_JEZCLJL ");
                    else
                        sql.Append("select sum(DFJE) as XFJE from HYK_JEZCLJL ");
                    sql.Append("where MDID = ").Append(storeInfo.StoreId);
                    sql.Append("  and SKTNO = '").Append(posId).Append("'");
                    sql.Append("  and CLLX = 7 ");
                    sql.Append("  and SKYDM = '").Append(cashier).Append("'");
                    if (accountDate > DateTime.MinValue)
                    {
                        DbUtils.SpellSqlParameter(conn, sql, " and ", "JZRQ", "=");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
                    }
                    if (beginBillId > 0)
                    {
                        sql.Append("  and JLBH >= ").Append(beginBillId);
                        if (endBillId > 0)
                            sql.Append("  and JLBH <= ").Append(endBillId);
                    }
                    if (isDetail)
                    {
                        sql.Append(" group by JLBH ");
                        sql.Append(" order by JLBH ");
                    }
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CrmPayAccount account = new CrmPayAccount();
                        accountList.Add(account);
                        if (isDetail)
                        {
                            account.BillId = DbUtils.GetInt(reader, 0);
                            account.PayCashCardMoney = DbUtils.GetDouble(reader, 1);
                        }
                        else
                        {
                            account.PayCashCardMoney = DbUtils.GetDouble(reader, 0);
                        }
                    }
                    reader.Close();
                    cmd.Parameters.Clear();

                    sql.Length = 0;
                    if (isDetail)
                        sql.Append("select JLBH,sum(DFJE) as XFJE from HYK_YHQCLJL ");
                    else
                        sql.Append("select sum(DFJE) as XFJE from HYK_YHQCLJL ");
                    sql.Append("where MDID = ").Append(storeInfo.StoreId);
                    sql.Append("  and SKTNO = '").Append(posId).Append("'");
                    sql.Append("  and CLLX = 7 ");
                    sql.Append("  and SKYDM = '").Append(cashier).Append("'");
                    if (accountDate > DateTime.MinValue)
                    {
                        DbUtils.SpellSqlParameter(conn, sql, " and ", "JZRQ", "=");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
                    }
                    if (beginBillId > 0)
                    {
                        sql.Append("  and JLBH >= ").Append(beginBillId);
                        if (endBillId > 0)
                            sql.Append("  and JLBH <= ").Append(endBillId);
                    }
                    if (isDetail)
                    {
                        sql.Append(" group by JLBH ");
                        sql.Append(" order by JLBH ");
                    }
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (isDetail)
                    {
                        if (accountList.Count == 0)
                        {
                            while (reader.Read())
                            {
                                CrmPayAccount account = new CrmPayAccount();
                                accountList.Add(account);
                                account.BillId = DbUtils.GetInt(reader, 0);
                                account.PayVipCouponMoney = DbUtils.GetDouble(reader, 1);
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                int billId = DbUtils.GetInt(reader, 0);
                                bool isFound = false;
                                foreach (CrmPayAccount account in accountList)
                                {
                                    if (account.BillId == billId)
                                    {
                                        isFound = true;
                                        account.PayVipCouponMoney = DbUtils.GetDouble(reader, 1);
                                        break;
                                    }
                                }
                                if (!isFound)
                                {
                                    CrmPayAccount account = new CrmPayAccount();
                                    accountList.Add(account);
                                    account.BillId = billId;
                                    account.PayVipCouponMoney = DbUtils.GetDouble(reader, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (reader.Read())
                        {
                            CrmPayAccount account = null;
                            if (accountList.Count == 0)
                            {
                                account = new CrmPayAccount();
                                accountList.Add(account);
                            }
                            else
                                account = accountList[0];
                            account.PayVipCouponMoney = DbUtils.GetDouble(reader, 0);
                        }
                    }
                    reader.Close();
                    cmd.Parameters.Clear();

                    sql.Length = 0;
                    if (isDetail)
                        sql.Append("select a.JLBH,sum(b.YHQMZ) as XFJE from HYK_JYCL a,HYK_JYCLITEM_YHQDM b ");
                    else
                        sql.Append("select sum(b.YHQMZ) as XFJE from HYK_JYCL a,HYK_JYCLITEM_YHQDM b  ");
                    sql.Append("where a.JYID = b.JYID ");
                    sql.Append("  and a.MDID = ").Append(storeInfo.StoreId);
                    sql.Append("  and a.SKTNO = '").Append(posId).Append("'");
                    sql.Append("  and a.BJ_YHQ = 2 ");
                    sql.Append("  and a.JYZT = 2 ");
                    sql.Append("  and a.SKYDM = '").Append(cashier).Append("'");
                    if (accountDate > DateTime.MinValue)
                    {
                        DbUtils.SpellSqlParameter(conn, sql, " and ", "JZRQ", "=");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
                    }
                    if (beginBillId > 0)
                    {
                        sql.Append("  and a.JLBH >= ").Append(beginBillId);
                        if (endBillId > 0)
                            sql.Append("  and a.JLBH <= ").Append(endBillId);
                    }
                    if (isDetail)
                    {
                        sql.Append(" group by a.JLBH ");
                        sql.Append(" order by a.JLBH ");
                    }
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (isDetail)
                    {
                        if (accountList.Count == 0)
                        {
                            while (reader.Read())
                            {
                                CrmPayAccount account = new CrmPayAccount();
                                accountList.Add(account);
                                account.BillId = DbUtils.GetInt(reader, 0);
                                account.PayCodedCouponMoney = DbUtils.GetDouble(reader, 1);
                            }
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                int billId = DbUtils.GetInt(reader, 0);
                                bool isFound = false;
                                foreach (CrmPayAccount account in accountList)
                                {
                                    if (account.BillId == billId)
                                    {
                                        isFound = true;
                                        account.PayCodedCouponMoney = DbUtils.GetDouble(reader, 1);
                                        break;
                                    }
                                }
                                if (!isFound)
                                {
                                    CrmPayAccount account = new CrmPayAccount();
                                    accountList.Add(account);
                                    account.BillId = billId;
                                    account.PayCodedCouponMoney = DbUtils.GetDouble(reader, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (reader.Read())
                        {
                            CrmPayAccount account = null;
                            if (accountList.Count == 0)
                            {
                                account = new CrmPayAccount();
                                accountList.Add(account);
                            }
                            else
                                account = accountList[0];
                            account.PayCodedCouponMoney = DbUtils.GetDouble(reader, 0);
                        }
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
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
            return (msg.Length == 0);
        }

        public static bool GetCodedCouponPayAccount_Dennis(out string msg, List<CrmCodedCouponPayment> paymentList, CrmStoreInfo storeInfo, string cashier, DateTime accountDate)
        {
            msg = string.Empty;

            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    sql.Append("select b.HYK_NO,a.DFJE,a.SKTNO,a.JLBH,a.JYBH from HYK_JEZCLJL a,HYK_HYXX b,HYKDEF c ");
                    sql.Append("where a.HYID = b.HYID ");
                    sql.Append("  and b.HYKTYPE = c.HYKTYPE ");
                    sql.Append("  and c.BJ_TS = 1 ");
                    sql.Append("  and a.MDID = ").Append(storeInfo.StoreId);
                    sql.Append("  and a.CLLX = 7 ");
                    sql.Append("  and a.SKYDM = '").Append(cashier).Append("'");
                    DbUtils.SpellSqlParameter(conn, sql, " and ", "a.JZRQ", "=", "JZRQ");
                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
                    sql.Append(" order by a.JYBH,b.HYK_NO ");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CrmCodedCouponPayment payment = new CrmCodedCouponPayment();
                        paymentList.Add(payment);
                        payment.CouponCode = DbUtils.GetString(reader, 0);
                        payment.PayMoney = DbUtils.GetDouble(reader, 1);
                    }
                    reader.Close();
                    cmd.Parameters.Clear();
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
            return (msg.Length == 0);
        }

        public static bool GetBankCardPromDesc(out string msg, out string promDesc, string cardCode, CrmStoreInfo storeInfo)
        {
            msg = string.Empty;
            promDesc = string.Empty;
            if ((storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select a.ID,b.CXZT from YHXXITEM a,CXHDDEF b ");
            sql.Append("where a.CXID = b.CXID ");
            //sql.Append("  and a.SHDM = b.SHDM ");
            sql.Append("  and '").Append(cardCode).Append("' between a.CODE1 and a.CODE2 ");
            sql.Append("  and b.SHDM = '").Append(storeInfo.Company).Append("'");
            sql.Append("  and a.BJ_TY = 0 ");
            sql.Append("order by a.ID desc ");
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
                    if (!reader.Read())
                    {
                        reader.Close();
                        msg = "对不起，该银行卡不参加活动";
                        return false;
                    }
                    promDesc = DbUtils.GetString(reader, 1, CrmServerPlatform.Config.DbCharSetIsNotChinese, 40);
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
            return (msg.Length == 0);
        }

        private static DateTime CalcValidDateOfCard(DateTime today, string validDateLen)
        {
            DateTime result = today;
            if (validDateLen.Length > 1)
            {
                char ch = validDateLen[validDateLen.Length - 1];
                if ((ch == 'Y') || (ch == 'y') || (ch == 'M') || (ch == 'm') || (ch == 'D') || (ch == 'd'))
                {
                    try
                    {
                        int len = int.Parse(validDateLen.Substring(0, validDateLen.Length - 1));
                        if ((ch == 'Y') || (ch == 'y'))
                            result = result.AddYears(len);
                        else if ((ch == 'M') || (ch == 'm'))
                            result = result.AddMonths(len);
                        else if ((ch == 'D') || (ch == 'd'))
                            result = result.AddDays(len);
                    }
                    catch
                    {

                    }
                }
            }
            return result;
        }

        public static bool SaveMoneyToCashCard(out string msg, out int cardId,out string cardCode,out int seq, int condType, string condValue, string cardCodeToCheck, string verifyCode, string password, CrmStoreInfo storeInfo,string posId,string cashierCode,string cashierName,double saveMoney, List<CrmPayment> paymentList)
        {
            msg = string.Empty;
            cardId = 0;
            cardCode = string.Empty;
            seq = 0;

            if ((condType == 0) && CrmServerPlatform.Config.DesEncryptCashCardTrackSecondly)
                condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                    sql.Length = 0;
                    sql.Append("select a.HYKTYPE,a.HYK_NO,a.STATUS,a.YXQ,a.MDID,b.BJ_CZZH,b.FS_SYMD,b.THBJ,b.BJ_YZM,b.BJ_TS,a.HYID,a.BJ_PSW,a.PASSWORD ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    switch (condType)
                    {
                        case 0:
                            sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                            break;
                        case 1:
                            sql.Append("  and a.HYID = ").Append(condValue);
                            break;
                        case 2:
                            sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                            break;
                        default:
                            sql.Append("  and 1=2");
                            break;
                    }
                    cmd.CommandText = sql.ToString();
                    bool isFromHYXX = true;
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (DbUtils.GetInt(reader, 2) < 0)
                        {
                            reader.Close();
                            msg = "卡状态无效";
                            return false;
                        }

                        bool existPasswd = DbUtils.GetBool(reader, 11);
                        if (existPasswd)
                        {
                            string passwdFromDB = DbUtils.GetString(reader, 12);
                            if ((password == null) || (password.Length == 0))
                            {
                                if (passwdFromDB.Length > 0)
                                {
                                    reader.Close();
                                    msg = "对不起, 请输入密码";
                                    return false;
                                }
                            }
                            else
                            {
                                string passwd2 = EncryptUtils.DesEncryptCardTrackSecondly(password, CrmServerPlatform.PubData.DesKey);
                                if (!passwdFromDB.Equals(passwd2))
                                {
                                    reader.Close();
                                    msg = "对不起, 密码错误";
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        isFromHYXX = false;
                        reader.Close();
                        sql.Length = 0;
                        sql.Append("select a.HYKTYPE,a.CZKHM,a.STATUS,a.YXQ,a.MDID,b.BJ_CZZH,b.FS_SYMD,b.THBJ,b.BJ_YZM,b.BJ_TS,b.FS_YXQ,b.YXQCD,a.QCYE,a.PDJE,a.CDNR,a.BGDDDM,a.SKJLBH ");
                        sql.Append("from HYKCARD a,HYKDEF b ");
                        sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                        switch (condType)
                        {
                            case 0:
                                sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                                break;
                            case 2:
                                sql.Append("  and a.CZKHM = '").Append(condValue).Append("'");
                                break;
                            default:
                                sql.Append("  and 1=2");
                                break;
                        }
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            reader.Close();
                            msg = "卡不存在";
                            return false;
                        }
                        if (DbUtils.GetInt(reader, 2) != 1)
                        {
                            reader.Close();
                            msg = "卡不是领取状态";
                            return false;
                        }
                        if (!MathUtils.DoubleAEuqalToDoubleB(DbUtils.GetDouble(reader, 12), 0))
                        {
                            reader.Close();
                            msg = "对不起, 此卡不是零面值卡";
                            return false;
                        }
                        if (DbUtils.GetInt(reader, 16) > 0)
                        {
                            reader.Close();
                            msg = "对不起, 此卡正在售卡中...";
                            return false;
                        }
                    }

                    bool existCashAccount = DbUtils.GetBool(reader, 5);
                    if (!existCashAccount)
                    {
                        reader.Close();
                        msg = "对不起, 该卡没有开通储值卡帐户";
                        return false;
                    }
                    bool isCoupon = DbUtils.GetBool(reader, 9);
                    if (isCoupon)
                    {
                        reader.Close();
                        msg = "对不起, 该卡是储值券";
                        return false;
                    }

                    cardCode = DbUtils.GetString(reader, 1);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 8);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCode, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfCashCard)))
                    {
                        reader.Close();
                        return false;
                    }

                    int cardTypeId = DbUtils.GetInt(reader, 0);
                    int offerCardStoreId = DbUtils.GetInt(reader, 4);
                    int limitMode = DbUtils.GetInt(reader, 6);

                    DateTime cardValidDate = DbUtils.GetDateTime(reader, 3);
                    string cardTrack = string.Empty;
                    string cardStorageCode = string.Empty;
                    double cardBottom = 0;
                    if (isFromHYXX)
                    {
                        cardId = DbUtils.GetInt(reader, 10);
                    }
                    else
                    {
                        cardBottom = DbUtils.GetDouble(reader, 13);
                        if (DbUtils.GetInt(reader, 10) == 1)
                        {
                            cardValidDate = CalcValidDateOfCard(serverTime.Date, DbUtils.GetString(reader, 11));
                        }
                        cardTrack = DbUtils.GetString(reader, 14);
                        cardStorageCode = DbUtils.GetString(reader, 15);
                    }

                    reader.Close();
                    if (storeInfo.StoreId == 0)
                    {
                        if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                            return false;
                    }

                    if ((isFromHYXX) && (offerCardStoreId != storeInfo.StoreId))  //只能在发卡门店消费
                    {
                        msg = "对不起, 此卡不能在本门店使用";
                        return false;
                    }

                    if (msg.Length == 0)
                    {
                        foreach (CrmPayment payment in paymentList)
                        {
                            if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                            {
                                msg = "支付金额应该 大于 0";
                                break;
                            }

                            sql.Length = 0;
                            sql.Append("select SHZFFSID from SHZFFS where SHDM = '").Append(storeInfo.Company).Append("' and ZFFSDM = '").Append(payment.PayTypeCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                payment.PayTypeId = DbUtils.GetInt(reader, 0);
                            }
                            else
                            {
                                msg = string.Format("收款方式 {0} 还没有上传到 CRM 库", payment.PayTypeCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;
                        }
                    }
                    if (msg.Length == 0)
                    {
                        string storageCode = string.Empty;
                        int operatorId = 0;
                        string operatorName = string.Empty;

                        sql.Length = 0;
                        sql.Append("select a.PERSON_ID,b.PERSON_NAME,a.CZDD from DEF_QTKGLY a,RYXX b where a.PERSON_ID = b.PERSON_ID and a.MDID = ").Append(storeInfo.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            operatorId = DbUtils.GetInt(reader, 0);
                            operatorName = DbUtils.GetString(reader, 1);
                            storageCode = DbUtils.GetString(reader, 2);
                        }
                        reader.Close();
                        if (operatorId == 0)
                        {
                            msg = "对不起, CRM没有定义本门店的前台操作员";
                            return false;
                        }
                        if (operatorName.Length == 0)
                        {
                            msg = "对不起, CRM没有定义本门店的前台操作员";
                            return false;
                        }
                        if (storageCode.Length == 0)
                        {
                            msg = "对不起, CRM没有定义本门店的前台保管地点";
                            return false;
                        }

                        if (!isFromHYXX)
                        {
                            if (!storageCode.Equals(cardStorageCode))
                            {
                                msg = "对不起, 此卡不在本门店的前台保管地点";
                                return false;
                            }
                        }

                        string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);
                        int seqCZKSKJL = 0;
                        int seqJEZCLJL_JK = 0;
                        if (!isFromHYXX)
                        {
                            seqCZKSKJL = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_CZKSKJL");
                            cardId = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_HYXX");
                            if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                                seqJEZCLJL_JK = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId);
                        }
                        int seqJEZCLJL_CK = 0;
                        if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqJEZCLJL_CK = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId);
                        seq = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_CZK_CKJL");

                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;
                            if (!isFromHYXX)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_CZKSKJL(JLBH,FS,SKSL,YSZE,ZKL,ZKJE,SSJE,YXQ,BGDDDM,HYKTYPE,ZY,TK_FLAG,DJR,DJRMC,DJSJ,DZKFJE,KFJE,ZSJE,KHID,STATUS,ZXR,ZXRMC,ZXRQ,QDSJ)");
                                sql.Append("  values(").Append(seqCZKSKJL);
                                sql.Append(",1,1,0,1,0,0");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "YXQ", "");
                                sql.Append(",'").Append(storageCode);
                                sql.Append("',").Append(cardTypeId);
                                DbUtils.SpellSqlParameter(conn, sql, ",", "ZY", "");
                                sql.Append(",0,").Append(operatorId);
                                sql.Append(",'").Append(operatorName).Append("'");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "DJSJ", "");
                                sql.Append(",0,0,0,null,2,").Append(operatorId);
                                sql.Append(",'").Append(operatorName).Append("'");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "ZXRQ", "");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "QDSJ", "");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "YXQ", cardValidDate);
                                DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "零面值卡充值建卡", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "DJSJ", serverTime);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZXRQ", serverTime);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "QDSJ", serverTime);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("insert into HYK_CZKSKJLITEM(JLBH,CZKHM,HYID,QCYE,YXTZJE,PDJE)");
                                sql.Append("  values(").Append(seqCZKSKJL);
                                sql.Append(",'").Append(cardCode);
                                sql.Append("',").Append(cardId);
                                sql.Append(",0,0,").Append(cardBottom.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("insert into HYK_CZKSKJLKDITEM(JLBH,CZKHM_BEGIN,CZKHM_END,SKSL,MZJE)");
                                sql.Append("  values(").Append(seqCZKSKJL);
                                sql.Append(",'").Append(cardCode);
                                sql.Append("','").Append(cardCode);
                                sql.Append("',1,0");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("insert into HYK_HYXX(HYID,HYKTYPE,HYK_NO,CDNR,JKRQ,YXQ,DJR,DJRMC,DJSJ,STATUS,YBGDD,MDID) ");
                                sql.Append("  values(").Append(cardId);
                                sql.Append(",").Append(cardTypeId);
                                sql.Append(",'").Append(cardCode);
                                sql.Append("','").Append(cardTrack).Append("'");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "JKRQ", "");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "YXQ", "");
                                sql.Append(",").Append(operatorId);
                                sql.Append(",'").Append(operatorName).Append("'");
                                DbUtils.SpellSqlParameter(conn, sql, ",", "DJSJ", "");
                                sql.Append(",0,'").Append(cardStorageCode);
                                sql.Append("',").Append(storeInfo.StoreId);
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "JKRQ", serverTime);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "YXQ", cardValidDate);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "DJSJ", serverTime);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                sql.Length = 0;
                                sql.Append("delete from HYKCARD where CZKHM = '").Append(cardCode).Append("'");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                sql.Append("insert into HYK_JEZH (HYID,QCYE,YE,PDJE) ");
                                sql.Append("  values(").Append(cardId);
                                sql.Append(",0,").Append(saveMoney.ToString("f2"));
                                sql.Append(",").Append(cardBottom.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();

                                sql.Length = 0;
                                switch (dbSysName)
                                {
                                    case DbUtils.SybaseDbSystemName:
                                        sql.Append("insert into HYK_JEZCLJL(HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                        sql.Append("  values(").Append(cardId);
                                        break;
                                    case DbUtils.OracleDbSystemName:
                                        sql.Append("insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                        sql.Append("  values(").Append(seqJEZCLJL_JK).Append(",").Append(cardId);
                                        break;
                                }
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "CLSJ", "");
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JZRQ", "");
                                sql.Append(",").Append(0);
                                sql.Append(",").Append(seqCZKSKJL);
                                sql.Append(",").Append(storeInfo.StoreId);
                                sql.Append(",'").Append(posId).Append("'");
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                                sql.Append(",").Append(0);
                                sql.Append(",'").Append(cashierCode);
                                sql.Append("',0,0,0");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", serverTime.Date);
                                DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "零面值卡充值建卡", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                                //if (dbSysName.Equals(DbUtils.SybaseDbSystemName))
                                //{
                                //    cmd.CommandText = "select @@IDENTITY";
                                //    reader = cmd.ExecuteReader();
                                //    if (reader.Read())
                                //    {
                                //        seqJEZCLJL_JK = DbUtils.GetInt(reader, 0);
                                //    }
                                //    reader.Close();
                                //}
                            }
                            else
                            {
                                sql.Length = 0;
                                sql.Append("update HYK_JEZH set YE = YE + ").Append(saveMoney.ToString("f2"));
                                sql.Append(" where HYID = ").Append(cardId);
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_JEZH (HYID,QCYE,YE,PDJE) ");
                                    sql.Append("  values(").Append(cardId);
                                    sql.Append(",0,").Append(saveMoney.ToString("f2"));
                                    sql.Append(",").Append(cardBottom.ToString("f2"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            double balance = 0;
                            EncryptBalanceOfCashCard(cmd, sql, cardId, serverTime, out balance);

                            sql.Length = 0;
                            switch (dbSysName)
                            {
                                case DbUtils.SybaseDbSystemName:
                                    sql.Append("insert into HYK_JEZCLJL(HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                    sql.Append("  values(").Append(cardId);
                                    break;
                                case DbUtils.OracleDbSystemName:
                                    sql.Append("insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                    sql.Append("  values(").Append(seqJEZCLJL_CK).Append(",").Append(cardId);
                                    break;
                            }
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "CLSJ", "");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JZRQ", "");
                            sql.Append(",").Append(1);
                            sql.Append(",").Append(seqCZKSKJL);
                            sql.Append(",").Append(storeInfo.StoreId);
                            sql.Append(",'").Append(posId).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                            sql.Append(",").Append(0);
                            sql.Append(",'").Append(cashierCode);
                            sql.Append("',").Append(saveMoney.ToString("f2"));
                            sql.Append(",").Append(0);
                            sql.Append(",").Append(balance.ToString("f2"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.Parameters.Clear();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", serverTime.Date);
                            DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "零面值卡充值", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_CZK_CKJL(CZJPJ_JLBH,CZDD,HYID,YJE,CKJE,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,MDID,SKTNO,SKYDM,SKYMC,BJ_SQ)");
                            sql.Append("  values(").Append(seq);
                            sql.Append(",'").Append(storageCode);
                            sql.Append("',").Append(cardId);
                            sql.Append(",").Append((balance - saveMoney).ToString("f2"));
                            sql.Append(",").Append(saveMoney.ToString("f2"));
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                            sql.Append(",").Append(operatorId);
                            sql.Append(",'").Append(operatorName).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "DJSJ", "");
                            sql.Append(",").Append(operatorId);
                            sql.Append(",'").Append(operatorName).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZXRQ", "");
                            sql.Append(",").Append(storeInfo.StoreId);
                            sql.Append(",'").Append(posId);
                            sql.Append("','").Append(cashierCode);
                            sql.Append("','").Append(cashierName);
                            sql.Append("',0");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "零面值卡充值", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "DJSJ", serverTime);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZXRQ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            foreach (CrmPayment payment in paymentList)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_CZK_CK_SKJL(JLBH,ZFFSID,JE,FKRQ)");
                                sql.Append("  values(").Append(seq);
                                sql.Append(",").Append(payment.PayTypeId);
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "FKRQ", "");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "FKRQ", serverTime);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        public static bool ReturnCashCard(out string msg, out int seq, int cardId, CrmStoreInfo storeInfo, string posId, string cashierCode, string cashierName, double returnMoney, List<CrmPayment> paymentList)
        {
            msg = string.Empty;
            seq = 0;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);

                    sql.Length = 0;
                    sql.Append("select a.HYKTYPE,a.HYK_NO,a.CDNR,a.STATUS,a.YXQ,a.MDID,a.YBGDD,b.BJ_CZK ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    sql.Append("  and a.HYID = ").Append(cardId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (!DbUtils.GetBool(reader, 7))
                        {
                            reader.Close();
                            msg = "对不起, 这不是一张储值卡";
                            return false;
                        }
                        if (DbUtils.GetInt(reader, 3) < 0)
                        {
                            reader.Close();
                            msg = "卡状态无效";
                            return false;
                        }
                    }
                    else
                    {
                        reader.Close();
                        msg = "卡不存在";
                        return false;
                    }
                    int cardTypeId = DbUtils.GetInt(reader, 0);
                    string cardCode = DbUtils.GetString(reader, 1);
                    string cardTrack = DbUtils.GetString(reader, 2);
                    DateTime cardValidDate = DbUtils.GetDateTime(reader, 4);
                    int offerCardStoreId = DbUtils.GetInt(reader, 5);
                    string cardStorageCode = DbUtils.GetString(reader, 2);
                    reader.Close();
                    if (storeInfo.StoreId == 0)
                    {
                        if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                            return false;
                    }

                    if (offerCardStoreId != storeInfo.StoreId)  //只能在发卡门店消费
                    {
                        msg = "对不起, 此卡不能在本门店使用";
                        return false;
                    }

                    double cardBalance = 0;
                    double cardBottom = 0;
                    sql.Length = 0;
                    sql.Append("select QCYE,YE,PDJE,JYDJJE from HYK_JEZH where HYID = ").Append(cardId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(DbUtils.GetDouble(reader, 3), 0))
                        {
                            reader.Close();
                            msg = "对不起, 该卡有交易冻结金额，不能退";
                            return false;
                        }
                        cardBalance = DbUtils.GetDouble(reader, 1);
                        cardBottom = DbUtils.GetDouble(reader, 2);
                    }
                    reader.Close();
                    if (!MathUtils.DoubleAEuqalToDoubleB(returnMoney, cardBalance))
                    {
                        msg = "退卡金额 不等于 卡余额";
                        return false;
                    }
                    if (msg.Length == 0)
                    {
                        foreach (CrmPayment payment in paymentList)
                        {
                            if (!MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, 0))
                            {
                                msg = "支付金额应该 大于 0";
                                break;
                            }

                            sql.Length = 0;
                            sql.Append("select SHZFFSID from SHZFFS where SHDM = '").Append(storeInfo.Company).Append("' and ZFFSDM = '").Append(payment.PayTypeCode).Append("'");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                payment.PayTypeId = DbUtils.GetInt(reader, 0);
                            }
                            else
                            {
                                msg = string.Format("收款方式 {0} 还没有上传到 CRM 库", payment.PayTypeCode);

                            }
                            reader.Close();
                            if (msg.Length > 0)
                                break;
                        }
                    }
                    if (msg.Length == 0)
                    {
                        string storageCode = string.Empty;
                        int operatorId = 0;
                        string operatorName = string.Empty;

                        sql.Length = 0;
                        sql.Append("select a.PERSON_ID,b.PERSON_NAME,a.CZDD from DEF_QTKGLY a,RYXX b where a.PERSON_ID = b.PERSON_ID and a.MDID = ").Append(storeInfo.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            operatorId = DbUtils.GetInt(reader, 0);
                            operatorName = DbUtils.GetString(reader, 1);
                            storageCode = DbUtils.GetString(reader, 2);
                        }
                        reader.Close();
                        if (operatorId == 0)
                        {
                            msg = "对不起, CRM没有定义本门店的前台操作员";
                            return false;
                        }
                        if (operatorName.Length == 0)
                        {
                            msg = "对不起, CRM没有定义本门店的前台操作员";
                            return false;
                        }
                        if (storageCode.Length == 0)
                        {
                            msg = "对不起, CRM没有定义本门店的前台保管地点";
                            return false;
                        }

                        //if (!storageCode.Equals(cardStorageCode))
                        //{
                        //    msg = "对不起, 此卡不在本门店的前台保管地点";
                        //    return false;
                        //}

                        string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);
                        seq = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_TK");
                        int seqCZK_QKJL = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "HYK_CZK_QKJL");
                        int seqJEZCLJL_QK = 0;
                        int seqJEZCLJL_TK = 0;
                        if ((MathUtils.DoubleAGreaterThanDoubleB((returnMoney - cardBottom), 0)) && dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqJEZCLJL_QK = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId);
                        if ((MathUtils.DoubleAGreaterThanDoubleB(cardBottom, 0)) && dbSysName.Equals(DbUtils.OracleDbSystemName))
                            seqJEZCLJL_TK = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId);

                        DbTransaction dbTrans = conn.BeginTransaction();
                        try
                        {
                            cmd.Transaction = dbTrans;

                            sql.Length = 0;
                            sql.Append("insert into HYK_TK(JLBH,HYKTYPE,MDID,SKTNO,XSXPH,SKYDM,ZY,TKSL,TKFS,BGR,BGRMC,BGDDDM,LYR,LYRMC,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,JE,TKJE)");
                            sql.Append("  values(").Append(seq);
                            sql.Append(",").Append(cardTypeId);
                            sql.Append(",").Append(storeInfo.StoreId);
                            sql.Append(",'").Append(posId);
                            sql.Append("',0,'").Append(cashierCode).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                            sql.Append(",1,0,").Append(operatorId);
                            sql.Append(",'").Append(operatorName);
                            sql.Append("','").Append(storageCode);
                            sql.Append("',").Append(operatorId);
                            sql.Append(",'").Append(operatorName);
                            sql.Append("',").Append(operatorId);
                            sql.Append(",'").Append(operatorName).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "DJSJ", "");
                            sql.Append(",").Append(operatorId);
                            sql.Append(",'").Append(operatorName).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZXRQ", "");
                            sql.Append(",").Append(returnMoney.ToString("f2"));
                            sql.Append(",").Append(cardBottom.ToString("f2"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "前台退卡", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "DJSJ", serverTime);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZXRQ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            sql.Length = 0;
                            sql.Append("insert into HYK_TK_ITEM(JLBH,INX,HYID,HYK_NO,ZKL,JE,TKJE,KFJE) ");
                            sql.Append("  values(").Append(seq);
                            sql.Append(",0,").Append(cardId);
                            sql.Append(",'").Append(cardCode);
                            sql.Append("',1,").Append(returnMoney.ToString("f2"));
                            sql.Append(",").Append(cardBottom.ToString("f2"));
                            sql.Append(",0");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();

                            sql.Length = 0;
                            sql.Append("insert into HYKCARD(CZKHM,CDNR,HYKTYPE,QCYE,PDJE,YXTZJE,JKFS,BGDDDM,BGR,STATUS,BJ_YK,YXQ,XKRQ) ");
                            sql.Append("  values('").Append(cardCode);
                            sql.Append("','").Append(cardTrack);
                            sql.Append("',").Append(cardTypeId);
                            sql.Append(",0,").Append(cardBottom.ToString("f2"));
                            sql.Append(",0,0,'").Append(storageCode);
                            sql.Append("',").Append(operatorId);
                            sql.Append(",1,1");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "YXQ", "");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "XKRQ", "");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "YXQ", cardValidDate);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "XKRQ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            string code = cardId.ToString().PadLeft(cardCode.Length, '0') + 'X';
                            sql.Length = 0;
                            sql.Append("update HYK_HYXX set STATUS = -1,OLD_HYKNO = '").Append(cardCode);
                            sql.Append("',HYK_NO = '").Append(code).Append("',CDNR = '").Append(code).Append("'");
                            sql.Append(" where HYID = ").Append(cardId);
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();

                            sql.Length = 0;
                            sql.Append("update HYK_JEZH set YE = 0 ");
                            sql.Append(" where HYID = ").Append(cardId);
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();

                            if (MathUtils.DoubleAGreaterThanDoubleB((returnMoney - cardBottom), 0))
                            {
                                sql.Length = 0;
                                switch (dbSysName)
                                {
                                    case DbUtils.SybaseDbSystemName:
                                        sql.Append("insert into HYK_JEZCLJL(HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                        sql.Append("  values(").Append(cardId);
                                        break;
                                    case DbUtils.OracleDbSystemName:
                                        sql.Append("insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                        sql.Append("  values(").Append(seqJEZCLJL_QK).Append(",").Append(cardId);
                                        break;
                                }
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "CLSJ", "");
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JZRQ", "");
                                sql.Append(",").Append(2);
                                sql.Append(",").Append(seq);
                                sql.Append(",").Append(storeInfo.StoreId);
                                sql.Append(",'").Append(posId).Append("'");
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                                sql.Append(",").Append(0);
                                sql.Append(",'").Append(cashierCode);
                                sql.Append("',0,").Append((returnMoney - cardBottom).ToString("f2"));
                                sql.Append(",").Append(cardBottom.ToString("f2"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", serverTime.Date);
                                DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "前台取款", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                            if (MathUtils.DoubleAGreaterThanDoubleB(cardBottom, 0))
                            {
                                sql.Length = 0;
                                switch (dbSysName)
                                {
                                    case DbUtils.SybaseDbSystemName:
                                        sql.Append("insert into HYK_JEZCLJL(HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                        sql.Append("  values(").Append(cardId);
                                        break;
                                    case DbUtils.OracleDbSystemName:
                                        sql.Append("insert into HYK_JEZCLJL(JYBH,HYID,CLSJ,JZRQ,CLLX,JLBH,MDID,SKTNO,ZY,JYID,SKYDM,JFJE,DFJE,YE)");
                                        sql.Append("  values(").Append(seqJEZCLJL_TK).Append(",").Append(cardId);
                                        break;
                                }
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "CLSJ", "");
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "JZRQ", "");
                                sql.Append(",").Append(6);
                                sql.Append(",").Append(seqCZK_QKJL);
                                sql.Append(",").Append(storeInfo.StoreId);
                                sql.Append(",'").Append(posId).Append("'");
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                                sql.Append(",").Append(0);
                                sql.Append(",'").Append(cashierCode);
                                sql.Append("',0,").Append(cardBottom.ToString("f2"));
                                sql.Append(",0"); ;
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "CLSJ", serverTime);
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", serverTime.Date);
                                DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "前台退卡", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }

                            sql.Length = 0;
                            sql.Append("insert into HYK_CZK_QKJL(CZJPJ_JLBH,CZDD,HYID,HYKNO,YJE,QKJE,ZY,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,MDID)");
                            sql.Append("  values(").Append(seqCZK_QKJL);
                            sql.Append(",'").Append(storageCode);
                            sql.Append("',").Append(cardId);
                            sql.Append(",'").Append(cardCode);
                            sql.Append("',").Append(returnMoney.ToString("f2"));
                            sql.Append(",").Append(returnMoney.ToString("f2"));
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZY", "");
                            sql.Append(",'',").Append(operatorId);
                            sql.Append(",'").Append(operatorName).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "DJSJ", "");
                            sql.Append(",").Append(operatorId);
                            sql.Append(",'").Append(operatorName).Append("'");
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "ZXRQ", "");
                            sql.Append(",").Append(storeInfo.StoreId);
                            //sql.Append(",'").Append(posId);
                            //sql.Append("','").Append(cashierCode);
                            //sql.Append("','").Append(cashierName).Append("'");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddStrInputParameterAndValue(cmd, 40, "ZY", "前台退卡取款", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "DJSJ", serverTime);
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZXRQ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            foreach (CrmPayment payment in paymentList)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_CZK_QK_SKJL(JLBH,ZFFSID,JE,FKRQ)");
                                sql.Append("  values(").Append(seqCZK_QKJL);
                                sql.Append(",").Append(payment.PayTypeId);
                                sql.Append(",").Append(payment.PayMoney.ToString("f2"));
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "FKRQ", "");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "FKRQ", serverTime);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            throw e;
                        }
                    }
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
            return (msg.Length == 0);
        }

        public static bool UpdateVipInfo(out string msg, CrmVipInfo vipInfo)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    sql.Length = 0;
                    sql.Append("select a.HYKTYPE,a.HYK_NO,a.STATUS,b.BJ_CZK ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    sql.Append("  and a.HYID = ").Append(vipInfo.CardId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (DbUtils.GetBool(reader, 3))
                        {
                            reader.Close();
                            msg = "对不起, 这是一张储值卡";
                            return false;
                        }
                        if (DbUtils.GetInt(reader, 2) < 0)
                        {
                            reader.Close();
                            msg = "卡状态无效";
                            return false;
                        }
                    }
                    else
                    {
                        reader.Close();
                        msg = "卡不存在";
                        return false;
                    }
                    reader.Close();

                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;

                        sql.Length = 0;
                        sql.Append("update HYK_GRXX set ");
                        DbUtils.SpellSqlParameter(cmd.Connection, sql, "", "E_MAIL", "=");
                        DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "TXDZ", "=");
                        DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "SJHM", "=");
                        DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "GXSJ", "=");
                        sql.Append(" where HYID = ").Append(vipInfo.CardId);
                        cmd.CommandText = sql.ToString();
                        DbUtils.AddStrInputParameterAndValue(cmd, 100, "E_MAIL", vipInfo.EMail);
                        DbUtils.AddStrInputParameterAndValue(cmd, 100, "TXDZ", vipInfo.Address);
                        DbUtils.AddStrInputParameterAndValue(cmd, 20, "SJHM", vipInfo.Mobile);
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "GXSJ", serverTime);
                        cmd.ExecuteNonQuery();

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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool UpdateVipCent(out string msg, int vipId, double updateCent, int updateType, CrmStoreInfo storeInfo, string posId, int billId)
        {
            msg = string.Empty;

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    sql.Length = 0;
                    sql.Append("select a.HYKTYPE,a.HYK_NO,a.STATUS,b.BJ_CZK ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    sql.Append("  and a.HYID = ").Append(vipId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (DbUtils.GetBool(reader, 3))
                        {
                            reader.Close();
                            msg = "对不起, 这是一张储值卡";
                            return false;
                        }
                        if (DbUtils.GetInt(reader, 2) < 0)
                        {
                            reader.Close();
                            msg = "卡状态无效";
                            return false;
                        }
                    }
                    else
                    {
                        reader.Close();
                        msg = "卡不存在";
                        return false;
                    }
                    reader.Close();

                    List<int> storeIds = null;
                    List<double> cents = null;
                    if (MathUtils.DoubleASmallerThanDoubleB(updateCent, 0) && CrmServerPlatform.Config.NoNegativeValidCent)
                    {
                        double validCent = 0;

                        sql.Length = 0;
                        sql.Append("select WCLJF from HYK_JFZH where HYID = ").Append(vipId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            validCent = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();
                        if (MathUtils.DoubleASmallerThanDoubleB(updateCent + validCent, 0))
                        {
                            msg = "会员卡的积分不能为负";
                            return false;
                        }

                        storeIds = new List<int>();
                        cents = new List<double>();
                        sql.Length = 0;
                        sql.Append("select WCLJF from HYK_MDJF where HYID = ").Append(vipId).Append("  and MDID = ").Append(storeInfo.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            validCent = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();
                        if (MathUtils.DoubleASmallerThanDoubleB(updateCent + validCent, 0))
                        {
                            double temp = updateCent;
                            if (MathUtils.DoubleAGreaterThanDoubleB(validCent, 0))
                            {
                                storeIds.Add(storeInfo.StoreId);
                                cents.Add(-validCent);
                                temp = temp + validCent;
                            }
                            sql.Length = 0;
                            sql.Append("select MDID,WCLJF from HYK_MDJF where HYID = ").Append(vipId).Append(" and WCLJF > 0 order by MDID ");
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                validCent = DbUtils.GetDouble(reader, 1);
                                if (MathUtils.DoubleASmallerThanDoubleB(temp + validCent, 0))
                                {
                                    storeIds.Add(DbUtils.GetInt(reader, 0));
                                    cents.Add(-validCent);
                                    temp = temp + validCent;
                                }
                                else
                                {
                                    storeIds.Add(DbUtils.GetInt(reader, 0));
                                    cents.Add(temp);
                                    break;
                                }
                            }
                            reader.Close();
                        }
                        else
                        {
                            storeIds.Add(storeInfo.StoreId);
                            cents.Add(updateCent);
                        }
                    }

                    DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;

                        sql.Length = 0;
                        sql.Append("insert into HYK_JFBDJLMX(HYID,RQ,CLLX,JLBH,WCLJFBD) ");
                        sql.Append("  values(").Append(vipId);
                        DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "RQ", "");
                        sql.Append(",").Append(updateType);
                        sql.Append(",").Append(billId);
                        sql.Append(",").Append(updateCent.ToString("f4"));
                        sql.Append(")");
                        cmd.CommandText = sql.ToString();
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "RQ", serverTime);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        if (MathUtils.DoubleAGreaterThanDoubleB(updateCent, 0))
                        {
                            sql.Length = 0;
                            sql.Append("update HYK_JFZH set WCLJF = WCLJF + ").Append(updateCent.ToString("f4"));
                            sql.Append(" where HYID =  ").Append(vipId);
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_JFZH(HYID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                sql.Append("  values(").Append(vipId);
                                sql.Append(",").Append(updateCent.ToString("f4"));
                                sql.Append(",0,0,0,0,0,0,0,0,0");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            sql.Length = 0;
                            sql.Append("update HYK_MDJF set WCLJF = WCLJF + ").Append(updateCent.ToString("f4"));
                            sql.Append(" where HYID =  ").Append(vipId);
                            sql.Append("   and MDID =  ").Append(storeInfo.StoreId);
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_MDJF(HYID,MDID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                sql.Append("  values(").Append(vipId);
                                sql.Append(",").Append(storeInfo.StoreId);
                                sql.Append(",").Append(updateCent.ToString("f4"));
                                sql.Append(",0,0,0,0,0,0,0,0,0");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }

                            sql.Length = 0;
                            sql.Append("insert into HYK_JFBDJLMX_MD(HYID,MDID,RQ,CLLX,JLBH,WCLJFBD) ");
                            sql.Append("  values(").Append(vipId);
                            sql.Append(",").Append(storeInfo.StoreId);
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "RQ", "");
                            sql.Append(",").Append(updateType);
                            sql.Append(",").Append(billId);
                            sql.Append(",").Append(updateCent.ToString("f4"));
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "RQ", serverTime);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        else
                        {
                            sql.Length = 0;
                            sql.Append("update HYK_JFZH set WCLJF = WCLJF - ").Append((-updateCent).ToString("f4"));
                            sql.Append(" where HYID =  ").Append(vipId);
                            cmd.CommandText = sql.ToString();
                            if (cmd.ExecuteNonQuery() == 0)
                            {
                                sql.Length = 0;
                                sql.Append("insert into HYK_JFZH(HYID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                sql.Append("  values(").Append(vipId);
                                sql.Append(",").Append(updateCent.ToString("f4"));
                                sql.Append(",0,0,0,0,0,0,0,0,0");
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                cmd.ExecuteNonQuery();
                            }
                            if (CrmServerPlatform.Config.NoNegativeValidCent)
                            {
                                for (int i = 0; i < storeIds.Count; i++)
                                {
                                    sql.Length = 0;
                                    sql.Append("update HYK_MDJF set WCLJF = WCLJF - ").Append((-cents[i]).ToString("f4"));
                                    sql.Append(" where HYID =  ").Append(vipId);
                                    sql.Append("   and MDID =  ").Append(storeIds[i]);
                                    cmd.CommandText = sql.ToString();
                                    if (cmd.ExecuteNonQuery() == 0)
                                    {
                                        sql.Length = 0;
                                        sql.Append("insert into HYK_MDJF(HYID,MDID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                        sql.Append("  values(").Append(vipId);
                                        sql.Append(",").Append(storeIds[i]);
                                        sql.Append(",").Append(cents[i].ToString("f4"));
                                        sql.Append(",0,0,0,0,0,0,0,0,0");
                                        sql.Append(")");
                                        cmd.CommandText = sql.ToString();
                                        cmd.ExecuteNonQuery();
                                    }
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_JFBDJLMX_MD(HYID,MDID,RQ,CLLX,JLBH,WCLJFBD) ");
                                    sql.Append("  values(").Append(vipId);
                                    sql.Append(",").Append(storeIds[i]);
                                    DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "RQ", "");
                                    sql.Append(",").Append(updateType);
                                    sql.Append(",").Append(billId);
                                    sql.Append(",").Append(cents[i].ToString("f4"));
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    DbUtils.AddDatetimeInputParameterAndValue(cmd, "RQ", serverTime);
                                    cmd.ExecuteNonQuery();
                                    cmd.Parameters.Clear();
                                }
                            }
                            else
                            {
                                sql.Length = 0;
                                sql.Append("update HYK_MDJF set WCLJF = WCLJF - ").Append((-updateCent).ToString("f4"));
                                sql.Append(" where HYID =  ").Append(vipId);
                                sql.Append("   and MDID =  ").Append(storeInfo.StoreId);
                                cmd.CommandText = sql.ToString();
                                if (cmd.ExecuteNonQuery() == 0)
                                {
                                    sql.Length = 0;
                                    sql.Append("insert into HYK_MDJF(HYID,MDID,WCLJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,BNLJJF,XFCS,THCS) ");
                                    sql.Append("  values(").Append(vipId);
                                    sql.Append(",").Append(storeInfo.StoreId);
                                    sql.Append(",").Append(updateCent.ToString("f4"));
                                    sql.Append(",0,0,0,0,0,0,0,0,0");
                                    sql.Append(")");
                                    cmd.CommandText = sql.ToString();
                                    cmd.ExecuteNonQuery();
                                }
                                sql.Length = 0;
                                sql.Append("insert into HYK_JFBDJLMX_MD(HYID,MDID,RQ,CLLX,JLBH,WCLJFBD) ");
                                sql.Append("  values(").Append(vipId);
                                sql.Append(",").Append(storeInfo.StoreId);
                                DbUtils.SpellSqlParameter(cmd.Connection, sql, ",", "RQ", "");
                                sql.Append(",").Append(updateType);
                                sql.Append(",").Append(billId);
                                sql.Append(",").Append(updateCent.ToString("f4"));
                                sql.Append(")");
                                cmd.CommandText = sql.ToString();
                                DbUtils.AddDatetimeInputParameterAndValue(cmd, "RQ", serverTime);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool GetVipCardToBuyCent(out string msg, out CrmVipCard vipCard,out double moneyToBuy,double centToBuy, int condType, string condValue, string cardCodeToCheck, string verifyCode)
        {
            msg = string.Empty;
            vipCard = null;
            moneyToBuy = 0;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            if ((condType >= 0) && (condType <= 2))
            {
                sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM ");
                sql.Append("from HYK_HYXX a,HYKDEF b ");
                sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                switch (condType)
                {
                    case 0:
                        if (CrmServerPlatform.Config.DesEncryptVipCardTrackSecondly)
                            condValue = EncryptUtils.DesEncryptCardTrackSecondly(condValue, CrmServerPlatform.PubData.DesKey);
                        sql.Append("  and a.CDNR = '").Append(condValue).Append("'");
                        break;
                    case 1:
                        sql.Append("  and a.HYID = ").Append(condValue);
                        break;
                    case 2:
                        sql.Append("  and a.HYK_NO = '").Append(condValue).Append("'");
                        break;
                }
            }
            else if ((condType >= 3) && (condType <= 4))
            {
                sql.Append("select a.HYID,a.HYKTYPE,a.HYK_NO,a.HY_NAME,a.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM,c.SJHM,c.PHONE,c.CSRQ,c.BJ_CLD,").Append(DbUtils.GetDbServerTimeFuncSql(cmd)).Append(" as CURRDATE,c.E_MAIL,c.TXDZ ");
                sql.Append("from HYK_HYXX a,HYKDEF b,HYK_GRXX c ");
                sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                sql.Append("  and a.HYID = c.HYID ");
                switch (condType)
                {
                    case 3:
                        sql.Append("  and c.SJHM = '").Append(condValue).Append("'");
                        break;
                    case 4:
                        sql.Append("  and c.SFZBH = '").Append(condValue).Append("'");
                        break;
                }
            }

            DateTime today = DateTime.MinValue;
            bool isChinese = false;
            bool isChildCard = false;
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
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        if ((condType == 0) || (condType == 2))
                        {
                            isChildCard = true;
                            reader.Close();
                            sql.Length = 0;
                            sql.Append("select a.HYID,a.HYKTYPE,c.HYK_NO,a.HY_NAME,c.STATUS,a.BJ_PSW,a.PASSWORD,b.HYKNAME,b.BJ_JF,b.BJ_YHQZH,b.YHFS,b.THBJ,b.BJ_CZK,b.BJ_YZM ");
                            sql.Append("from HYK_HYXX a,HYKDEF b,HYK_CHILD_JL c ");
                            sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                            sql.Append("  and a.HYID = c.HYID ");
                            switch (condType)
                            {
                                case 0:
                                    sql.Append("  and c.CDNR = '").Append(condValue).Append("'");
                                    break;
                                case 2:
                                    sql.Append("  and c.HYK_NO = '").Append(condValue).Append("'");
                                    break;
                            }
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (!reader.Read())
                            {
                                reader.Close();
                                msg = "会员卡不存在";
                                return false;
                            }
                        }
                        else
                        {
                            reader.Close();
                            msg = "会员卡不存在";
                            return false;
                        }
                    }

                    int status = DbUtils.GetInt(reader, 4);
                    if (status < 0)
                    {
                        reader.Close();
                        msg = "会员卡状态无效";
                        return false;
                    }

                    bool isCashCard = DbUtils.GetBool(reader, 12);
                    if (isCashCard)
                    {
                        reader.Close();
                        msg = "这是张储值卡";
                        return false;
                    }

                    string cardCodeFromDB = DbUtils.GetString(reader, 2);
                    bool isToCheckVerifyCode = DbUtils.GetBool(reader, 13);
                    bool isToCheckCardCode = isToCheckVerifyCode;

                    if ((condType == 0) && (!PosProc.CheckCard(out msg, cardCodeFromDB, isToCheckCardCode, cardCodeToCheck, isToCheckVerifyCode, verifyCode, CrmServerPlatform.Config.LengthVerifyOfVipCard)))
                    {
                        reader.Close();
                        return false;
                    }

                    vipCard = new CrmVipCard();
                    vipCard.CardId = DbUtils.GetInt(reader, 0);
                    vipCard.CardTypeId = DbUtils.GetInt(reader, 1);
                    vipCard.CardCode = DbUtils.GetString(reader, 2);
                    vipCard.VipName = DbUtils.GetString(reader, 3, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                    if (isChildCard)
                        vipCard.VipName = vipCard.VipName + "(副卡)";

                    vipCard.CardTypeName = DbUtils.GetString(reader, 7, CrmServerPlatform.Config.DbCharSetIsNotChinese, 20);
                    vipCard.CanCent = DbUtils.GetBool(reader, 8);
                    vipCard.CanOwnCoupon = DbUtils.GetBool(reader, 9);
                    vipCard.DiscType = DbUtils.GetInt(reader, 10);
                    vipCard.CanDisc = (vipCard.DiscType > 0);
                    vipCard.CanReturn = DbUtils.GetBool(reader, 11);
                    if ((condType >= 3) && (condType <= 4))
                    {
                        vipCard.Mobile = DbUtils.GetString(reader, 14);
                        vipCard.PhoneCode = vipCard.Mobile;
                        if (vipCard.PhoneCode.Trim().Length == 0)
                            vipCard.PhoneCode = DbUtils.GetString(reader, 15);
                        vipCard.Birthday = DbUtils.GetDateTime(reader, 16);
                        isChinese = DbUtils.GetBool(reader, 17);
                        today = DbUtils.GetDateTime(reader, 18);
                        //vipCard.EMail = DbUtils.GetString(reader, 19);
                        //vipCard.Address = DbUtils.GetString(reader, 20);
                    }
                    if (reader.Read())
                    {
                        reader.Close();
                        vipCard = null;
                        msg = "查询出多张会员卡";
                        return false;
                    }
                    reader.Close();

                    if ((vipCard.CanDisc) && (status == 6))
                        vipCard.CanDisc = false;    //呆滞卡不能打折

                    sql.Length = 0;
                    sql.Append("select WCLJF,BQJF,BNLJJF from HYK_JFZH where HYID = ").Append(vipCard.CardId.ToString());
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        vipCard.ValidCent = DbUtils.GetDouble(reader, 0);
                        vipCard.StageCent = DbUtils.GetDouble(reader, 1);
                        vipCard.YearCent = DbUtils.GetDouble(reader, 2);
                    }
                    else
                    {
                        vipCard.ValidCent = 0;
                        vipCard.YearCent = 0;
                        vipCard.StageCent = 0;
                    }
                    reader.Close();

                    double validCentToBuy = 0;
                    double yearCentToBuy = 0;

                    if ((CrmServerPlatform.Config.NoNegativeValidCent) && (MathUtils.DoubleAGreaterThanDoubleB(centToBuy, vipCard.ValidCent)))
                        validCentToBuy = centToBuy - vipCard.ValidCent;
                    if ((CrmServerPlatform.Config.NoNegativeYearCent) && (MathUtils.DoubleAGreaterThanDoubleB(centToBuy, vipCard.YearCent)))
                        yearCentToBuy = centToBuy - vipCard.YearCent;

                    if ((MathUtils.DoubleAGreaterThanDoubleB(validCentToBuy, 0)) || (MathUtils.DoubleAGreaterThanDoubleB(yearCentToBuy, 0)))
                    {
                        double exchangeValidCent = 0;
                        double exchangeYearCent = 0;
                        sql.Length = 0;
                        //sql.Append("select ID,JF_J,JF_N from HYK_THMJFGZDY where BJ_TY = 0 and JF_J<>0 and JF_N<>0 and HYKTYPE = ").Append(vipCard.CardTypeId).Append(" order by ID desc ");
                        sql.Append("select ID,JF_J,JF_N from HYK_THMJFGZDY where BJ_TY = 0 and JF_J<>0 and JF_N<>0 order by ID desc ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            exchangeValidCent = DbUtils.GetDouble(reader, 1);
                            exchangeYearCent = DbUtils.GetDouble(reader, 2);
                        }
                        reader.Close();
                        if (MathUtils.DoubleAGreaterThanDoubleB(validCentToBuy, 0))
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(exchangeValidCent, 0))
                                moneyToBuy = Math.Round(validCentToBuy / exchangeValidCent, 2);
                            else
                                msg = "没有定义正确的钱换分规则)";
                        }
                        if (MathUtils.DoubleAGreaterThanDoubleB(yearCentToBuy, 0))
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(exchangeYearCent, 0))
                                moneyToBuy += Math.Round(yearCentToBuy / exchangeYearCent, 2);
                            else
                                msg = "没有定义正确的钱换分规则(本年积分)";
                        }
                    }
                    if (msg.Length == 0)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(moneyToBuy, 0))
                        {
                            sql.Length = 0;
                            sql.Append("select YE from HYK_JEZH where HYID = ").Append(vipCard.CardId);
                            cmd.CommandText = sql.ToString();
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                vipCard.CashCardBalance = DbUtils.GetDouble(reader, 0);
                            }
                            reader.Close();
                            DateTime serverTime = DbUtils.GetDbServerTime(cmd);
                            sql.Length = 0;
                            sql.Append("select sum(JE) as YE from HYK_YHQZH a,YHQDEF b ");
                            sql.Append(" where a.YHQID = b.YHQID ");
                            sql.Append("   and a.HYID = ").Append(vipCard.CardId);
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, " and ", "JSRQ", ">=");
                            sql.Append("   and ((b.BJ_CXYHQ is null) or (b.BJ_CXYHQ = 0)) ");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", serverTime.Date);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                vipCard.CouponBalance = DbUtils.GetDouble(reader, 0);
                            }
                            reader.Close();
                            cmd.Parameters.Clear();
                        }
                        else
                            msg = "不需要钱换分";
                    }
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

            return (msg.Length == 0);
        }

        public static bool BuyVipCent(out string msg, int vipId, double centToBuy, double cashMoney,double cardMoney,double couponMoney, CrmStoreInfo storeInfo, string posId, string cashier,string cashierName,DateTime accountDate)
        {
            msg = string.Empty;

            if (MathUtils.DoubleAGreaterThanDoubleB(cardMoney, 0))
                msg = "现在不支持储值卡买会员积分";
            else if (MathUtils.DoubleAGreaterThanDoubleB(couponMoney, 0))
                msg = "现在不支持优惠券买会员积分";

            if ((storeInfo != null) && (storeInfo.StoreCode.Length > 0) && (storeInfo.StoreId == 0))
            {
                if (!CrmServerPlatform.GetStoreInfo(out msg, storeInfo))
                    return false;
            }

            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
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
                    int cardType = 0;
                    sql.Length = 0;
                    sql.Append("select a.HYKTYPE,a.HYK_NO,a.STATUS,b.BJ_CZK ");
                    sql.Append("from HYK_HYXX a,HYKDEF b ");
                    sql.Append("where a.HYKTYPE = b.HYKTYPE ");
                    sql.Append("  and a.HYID = ").Append(vipId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        cardType = DbUtils.GetInt(reader, 0);
                        if (DbUtils.GetBool(reader, 3))
                        {
                            reader.Close();
                            msg = "对不起, 这是一张储值卡";
                            return false;
                        }
                        if (DbUtils.GetInt(reader, 2) < 0)
                        {
                            reader.Close();
                            msg = "卡状态无效";
                            return false;
                        }
                    }
                    else
                    {
                        reader.Close();
                        msg = "卡不存在";
                        return false;
                    }
                    reader.Close();

                    double validCent = 0;
                    double yearCent = 0;
                    sql.Length = 0;
                    sql.Append("select WCLJF,BNLJJF from HYK_JFZH where HYID = ").Append(vipId);
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        validCent = DbUtils.GetDouble(reader, 0);
                        yearCent = DbUtils.GetDouble(reader, 1);
                    }
                    reader.Close();

                    double validCentToBuy = 0;
                    double yearCentToBuy = 0;
                    double moneyToBuyValidCent = 0;
                    double moneyToBuyYearCent = 0;
                    double overMoney = 0;
                    double centForOverMoney = 0;

                    if ((CrmServerPlatform.Config.NoNegativeValidCent) && (MathUtils.DoubleAGreaterThanDoubleB(centToBuy, validCent)))
                        validCentToBuy = centToBuy - validCent;
                    if ((CrmServerPlatform.Config.NoNegativeYearCent) && (MathUtils.DoubleAGreaterThanDoubleB(centToBuy, yearCent)))
                        yearCentToBuy = centToBuy - yearCent;

                    if ((MathUtils.DoubleAGreaterThanDoubleB(validCentToBuy, 0)) || (MathUtils.DoubleAGreaterThanDoubleB(yearCentToBuy, 0)))
                    {
                        double exchangeValidCent = 0;
                        double exchangeYearCent = 0;

                        sql.Length = 0;
                        //sql.Append("select ID,JF_J,JF_N from HYK_THMJFGZDY where BJ_TY = 0 and JF_J<>0 and JF_N<>0 and HYKTYPE = ").Append(cardType).Append(" order by ID desc ");
                        sql.Append("select ID,JF_J,JF_N from HYK_THMJFGZDY where BJ_TY = 0 and JF_J<>0 and JF_N<>0 order by ID desc ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            exchangeValidCent = DbUtils.GetDouble(reader, 1);
                            exchangeYearCent = DbUtils.GetDouble(reader, 2);
                        }
                        reader.Close();
                        if (MathUtils.DoubleAGreaterThanDoubleB(validCentToBuy, 0))
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(exchangeValidCent, 0))
                                moneyToBuyValidCent = Math.Round(validCentToBuy / exchangeValidCent, 2);
                            else
                                msg = "没有定义正确的钱换分规则)";
                        }
                        if (MathUtils.DoubleAGreaterThanDoubleB(yearCentToBuy, 0))
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(exchangeYearCent, 0))
                                moneyToBuyYearCent = Math.Round(yearCentToBuy / exchangeYearCent, 2);
                            else
                                msg = "没有定义正确的钱换分规则(本年积分)";
                        }
                        if (msg.Length == 0)
                        {
                            overMoney = cashMoney + cardMoney + couponMoney - moneyToBuyValidCent - moneyToBuyYearCent;
                            if (MathUtils.DoubleASmallerThanDoubleB(overMoney, 0))
                                msg = "钱换分金额不足";
                            else if (MathUtils.DoubleAGreaterThanDoubleB(overMoney, 0))
                            {
                                centForOverMoney = Math.Round(overMoney * exchangeValidCent, 4);
                            }
                        }
                    }
                    else
                        msg = "不需要钱换分";

                    double cashCardBalance = 0;
                    double couponBalance = 0;
                    DateTime serverTime = DateTime.MinValue;
                    if (msg.Length == 0)
                    {
                        serverTime = DbUtils.GetDbServerTime(cmd);
                        sql.Length = 0;
                        sql.Append("select YE from HYK_JEZH where HYID = ").Append(vipId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            cashCardBalance = DbUtils.GetDouble(reader, 0);
                        }
                        reader.Close();
                        if (MathUtils.DoubleAGreaterThanDoubleB(cardMoney, cashCardBalance))
                            msg = "储值卡余额不足";
                        else
                        {
                            sql.Length = 0;
                            sql.Append("select sum(JE) as YE from HYK_YHQZH a,YHQDEF b ");
                            sql.Append(" where a.YHQID = b.YHQID ");
                            sql.Append("   and a.HYID = ").Append(vipId);
                            DbUtils.SpellSqlParameter(cmd.Connection, sql, " and ", "JSRQ", ">=");
                            sql.Append("   and ((b.BJ_CXYHQ is null) or (b.BJ_CXYHQ = 0)) ");
                            cmd.CommandText = sql.ToString();
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "JSRQ", serverTime.Date);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                couponBalance = DbUtils.GetDouble(reader, 0);
                            }
                            reader.Close();
                            cmd.Parameters.Clear();
                            if (MathUtils.DoubleAGreaterThanDoubleB(couponMoney, couponBalance))
                                msg = "优惠券余额不足";
                        }
                    }

                    string storageCode = string.Empty;
                    int operatorId = 0;
                    string operatorName = string.Empty;
                    if (msg.Length == 0)
                    {
                        sql.Length = 0;
                        sql.Append("select a.PERSON_ID,b.PERSON_NAME,a.CZDD from DEF_QTKGLY a,RYXX b where a.PERSON_ID = b.PERSON_ID and a.MDID = ").Append(storeInfo.StoreId);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            operatorId = DbUtils.GetInt(reader, 0);
                            operatorName = DbUtils.GetString(reader, 1);
                            storageCode = DbUtils.GetString(reader, 2);
                        }
                        reader.Close();
                        if (operatorId == 0)
                            msg = "对不起, crm没有定义本门店的前台操作员";
                        else if (operatorName.Length == 0)
                            msg = "对不起, crm没有定义本门店的前台操作员";
                        else if (storageCode.Length == 0)
                            msg = "对不起, crm没有定义本门店的前台保管地点";
                    }

                    if (msg.Length > 0)
                        return false;

                    string dbSysName = DbUtils.GetDbSystemName(cmd.Connection);
                    int seqCRMTHMJFJL = SeqGenerator.GetSeq("CRMDB", CrmServerPlatform.CurrentDbId, "CRMTHMJFJL"); ;
                    int seqJEZCLJL = 0;
                    int seqYHQCLJL = 0;
                    if (dbSysName.Equals(DbUtils.OracleDbSystemName))
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(cardMoney, 0))
                            seqJEZCLJL = SeqGenerator.GetSeqHYK_JEZCLJL("CRMDB", CrmServerPlatform.CurrentDbId);
                        if (MathUtils.DoubleAGreaterThanDoubleB(couponMoney, 0))
                            seqYHQCLJL = SeqGenerator.GetSeqHYK_YHQCLJL("CRMDB", CrmServerPlatform.CurrentDbId);
                    }
                    DbTransaction dbTrans = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = dbTrans;

                        sql.Length = 0;
                        sql.Append("insert into CRMTHMJFJL(JLBH,CZDD,HYID,YWCLJF,YNLJJF,YYHQJE,YCZKYE,THJF,YMJJF,YMNJF,YFJE_J,YFJE_N,SMJJF,SMNJF,SFJE_J,SFJE_N,XJ,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,BZ,MDID,SKTNO,SKYDM,SKYMC,JZRQ)");
                        sql.Append("  values(").Append(seqCRMTHMJFJL);
                        sql.Append(",'").Append(storageCode);
                        sql.Append("',").Append(vipId);
                        sql.Append(",").Append(validCent.ToString("f4"));
                        sql.Append(",").Append(yearCent.ToString("f4"));
                        sql.Append(",").Append(couponBalance.ToString("f2"));
                        sql.Append(",").Append(cashCardBalance.ToString("f2"));
                        sql.Append(",").Append(centToBuy.ToString("f4"));
                        sql.Append(",").Append(validCentToBuy.ToString("f4"));
                        sql.Append(",").Append(yearCentToBuy.ToString("f4"));
                        sql.Append(",").Append(moneyToBuyValidCent.ToString("f2"));
                        sql.Append(",").Append(moneyToBuyYearCent.ToString("f2"));
                        sql.Append(",").Append((validCentToBuy + centForOverMoney).ToString("f4"));
                        sql.Append(",").Append(yearCentToBuy.ToString("f4"));
                        sql.Append(",").Append((moneyToBuyValidCent + overMoney).ToString("f2"));
                        sql.Append(",").Append(moneyToBuyYearCent.ToString("f2"));
                        sql.Append(",").Append(cashMoney.ToString("f2"));
                        sql.Append(",").Append(operatorId);
                        DbUtils.SpellSqlParameter(conn, sql, ",", "DJRMC", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "DJSJ", "");
                        sql.Append(",").Append(operatorId);
                        DbUtils.SpellSqlParameter(conn, sql, ",", "ZXRMC", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "ZXRQ", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "BZ", "");
                        sql.Append(",").Append(storeInfo.StoreId);
                        sql.Append(",'").Append(posId);
                        sql.Append("','").Append(cashier).Append("'");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "SKYMC", "");
                        DbUtils.SpellSqlParameter(conn, sql, ",", "JZRQ", "");
                        sql.Append(")");
                        cmd.CommandText = sql.ToString();
                        DbUtils.AddStrInputParameterAndValue(cmd, 16, "DJRMC", operatorName, CrmServerPlatform.Config.DbCharSetIsNotChinese);
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "DJSJ", serverTime);
                        DbUtils.AddStrInputParameterAndValue(cmd, 16, "ZXRMC", operatorName, CrmServerPlatform.Config.DbCharSetIsNotChinese);
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "ZXRQ", serverTime.Date);
                        DbUtils.AddStrInputParameterAndValue(cmd, 50, "BZ", "前台钱买分", CrmServerPlatform.Config.DbCharSetIsNotChinese);
                        DbUtils.AddStrInputParameterAndValue(cmd, 16, "SKYMC", cashierName, CrmServerPlatform.Config.DbCharSetIsNotChinese);
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "JZRQ", accountDate);
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        sql.Length = 0;
                        sql.Append("insert into CRMTHMJFJLITEM(JLBH,HYID,YYHQJE,YCZKYE,KYHQJE,KCZKYE) ");
                        sql.Append("  values(").Append(seqCRMTHMJFJL);
                        sql.Append(",").Append(vipId);
                        sql.Append(",").Append(couponBalance.ToString("f2"));
                        sql.Append(",").Append(cashCardBalance.ToString("f2"));
                        sql.Append(",").Append(couponMoney.ToString("f2"));
                        sql.Append(",").Append(cardMoney.ToString("f2"));
                        sql.Append(")");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();

                        sql.Length = 0;
                        sql.Append("update HYK_JFZH set WCLJF = WCLJF + ").Append((validCentToBuy + centForOverMoney).ToString("f4")).Append(",BNLJJF = BNLJJF + ").Append(yearCentToBuy.ToString("f4"));
                        sql.Append(" where HYID =  ").Append(vipId);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            sql.Length = 0;
                            sql.Append("insert into HYK_JFZH(HYID,WCLJF,BNLJJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,XFCS,THCS) ");
                            sql.Append("  values(").Append(vipId);
                            sql.Append(",").Append((validCentToBuy + centForOverMoney).ToString("f4"));
                            sql.Append(",").Append(yearCentToBuy.ToString("f4"));
                            sql.Append(",0,0,0,0,0,0,0,0");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
                        }
                        sql.Length = 0;
                        sql.Append("update HYK_MDJF set WCLJF = WCLJF + ").Append((validCentToBuy + centForOverMoney).ToString("f4")).Append(",BNLJJF = BNLJJF + ").Append(yearCentToBuy.ToString("f4"));
                        sql.Append(" where HYID =  ").Append(vipId);
                        sql.Append("   and MDID =  ").Append(storeInfo.StoreId);
                        cmd.CommandText = sql.ToString();
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            sql.Length = 0;
                            sql.Append("insert into HYK_MDJF(HYID,MDID,WCLJF,BNLJJF,XFJE,LJXFJE,LJZKJE,ZKJE,BQJF,LJJF,XFCS,THCS) ");
                            sql.Append("  values(").Append(vipId);
                            sql.Append(",").Append(storeInfo.StoreId);
                            sql.Append(",").Append((validCentToBuy + centForOverMoney).ToString("f4"));
                            sql.Append(",").Append(yearCentToBuy.ToString("f4"));
                            sql.Append(",0,0,0,0,0,0,0,0");
                            sql.Append(")");
                            cmd.CommandText = sql.ToString();
                            cmd.ExecuteNonQuery();
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
                        throw new MyDbException(e.Message, cmd.CommandText);
                }
            }
            finally
            {
                conn.Close();
            }
            return (msg.Length == 0);
        }

        public static bool SearchArticlePromRule(out string msg,List<CrmPromBillItemDesc> promBillItemList,string companyCode,int storeId, string deptCode, string articleCode, int vipType, int vipId, string vipCode,int payTypeId,int bankId,string bankCardCode, DateTime saleTime)
        {
            msg = string.Empty;

            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
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
                CyQuery query = new CyQuery(conn);
                try
                {
                    #region 取商品信息
                    CrmArticle article = new CrmArticle();
                    query.SQL.Clear();
                    query.SQL.Add("select SHSPID from SHSPXX_DM where SHDM = '").Add(companyCode).Add("' and SPDM = '").Add(articleCode).Add("'");
                    query.Open();
                    if (query.IsEmpty)
                        msg = string.Format("商品 {0} 还没有上传到 CRM 库", articleCode);
                    else
                        article.ArticleId = query.Fields[0].AsInteger;
                    query.Close();
                    if (msg.Length > 0)
                        return false;
                    article.DeptCode = deptCode;

                    query.SQL.Clear();
                    query.SQL.Add("select c.SPFLDM,b.SHSBID,b.SHHTID ");
                    query.SQL.Add(" ,(select GHSDM from SHHT d where b.SHHTID = d.SHHTID) as GHSDM ");
                    query.SQL.Add(" from SHSPXX b,SHSPFL c where b.SHSPFLID = c.SHSPFLID and b.SHSPID = ").Add(article.ArticleId);
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        article.CategoryCode = query.FieldByName("SPFLDM").AsString;
                        article.BrandId = query.FieldByName("SHSBID").AsInteger;
                        article.ContractId = query.FieldByName("SHHTID").AsInteger;
                        article.SuppCode = query.FieldByName("GHSDM").AsString;
                    }
                    query.Close();
                    #endregion

                    #region 取会员信息
                    CrmVipCard vipCard = null;
                    if ((vipId > 0) || (vipCode.Length > 0))
                    {
                        query.SQL.Clear();
                        query.SQL.Add("select a.HYID,a.HYK_NO,a.HYKTYPE,a.FXDW,b.BJ_YHQZH,b.BJ_JF,a.STATUS from HYK_HYXX a,HYKDEF b ");
                        query.SQL.Add(" where a.HYKTYPE = b.HYKTYPE ");
                        if (vipId > 0)
                            query.SQL.Add(" and a.HYID = ").Add(vipId);
                        else
                            query.SQL.Add(" and a.HYK_NO = '").Add(vipCode).Add("'");
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            vipCard = new CrmVipCard();
                            vipCard.CardId = query.FieldByName("HYID").AsInteger;
                            vipCard.CardCode = query.FieldByName("HYK_NO").AsString;
                            vipCard.CardTypeId = query.FieldByName("HYKTYPE").AsInteger;
                            vipCard.IssueCardCompanyId = query.FieldByName("FXDW").AsInteger;
                            vipCard.CanOwnCoupon = query.FieldByName("BJ_YHQZH").AsBoolean;
                            vipCard.CanCent = query.FieldByName("BJ_JF").AsBoolean;
                            vipCard.Status = query.FieldByName("STATUS").AsInteger;
                        }
                        query.Close();
                        if (vipCard != null)
                        {
                            query.SQL.Clear();
                            query.SQL.Add("select CSRQ,ZYID,ZJLXID,SEX,BJ_CLD from HYK_GRXX where HYID = ").Add(vipCard.CardId);
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                vipCard.Birthday = query.FieldByName("CSRQ").AsDateTime;
                                vipCard.JobType = query.FieldByName("ZYID").AsInteger; ;
                                vipCard.IdCardType = query.FieldByName("ZJLXID").AsInteger;
                                vipCard.SexType = query.FieldByName("SEX").AsInteger;
                                vipCard.BirthdayIsChinese = query.FieldByName("BJ_CLD").AsBoolean;
                            }
                            query.Close();
                        }
                    }

                    if (vipCard == null)
                    {
                        vipCard = new CrmVipCard();
                        vipCard.CardTypeId = vipType;
                    }
                    #endregion

                    int situationalMode = 0;
                    DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                    if (saleTime == DateTime.MinValue)
                        saleTime = serverTime;
                    else if (saleTime.Date.CompareTo(serverTime.Date) > 0)
                        situationalMode = 1;
                    else if (saleTime.Date.CompareTo(serverTime.Date) < 0)
                        situationalMode = 2;
                    List<CrmArticle> articleList = new List<CrmArticle>();
                    articleList.Add(article);
                    
                    #region 积分
                    PromRuleSearcher.LookupVipCentRuleOfArticle(query.Command, situationalMode, vipCard, companyCode, saleTime, articleList);
                    if (article.VipCentRuleValue != null)
                    {
                        CrmPromBillItemDesc item = new CrmPromBillItemDesc();
                        promBillItemList.Add(item);
                        item.BillType = 1;
                        item.PromId = article.VipCentRuleValue.Bill.PromId;
                        item.BillId = article.VipCentRuleValue.Bill.BillId;
                        item.SubBillInx = article.VipCentRuleValue.SubBill.Inx;
                        if (article.VipCentRuleValue.BillItem == null)
                            item.ItemInx = -1;
                        else
                            item.ItemInx = article.VipCentRuleValue.BillItem.Inx;
                        item.BeginTime = article.VipCentRuleValue.SubBill.BeginTime;
                        item.EndTime = article.VipCentRuleValue.SubBill.EndTime;
                        item.PeriodDesc = string.Empty;
                        item.RuleId = 0;
                        item.RuleDesc = "基数" + article.BaseCent.ToString("f2") + "倍数" + article.CentMultiple.ToString("f2");
                    }
                    #endregion

                    #region 折扣
                    PromRuleSearcher.LookupVipDiscRule(query.Command, situationalMode, vipCard, companyCode, storeId, saleTime, articleList);
                    if (article.VipDiscRuleValue != null)
                    {
                        CrmPromBillItemDesc item = new CrmPromBillItemDesc();
                        promBillItemList.Add(item);
                        item.BillType = 2;
                        item.PromId = article.VipDiscRuleValue.Bill.PromId;
                        item.BillId = article.VipDiscRuleValue.Bill.BillId;
                        item.SubBillInx = article.VipDiscRuleValue.SubBill.Inx;
                        if (article.VipDiscRuleValue.BillItem == null)
                            item.ItemInx = -1;
                        else
                            item.ItemInx = article.VipDiscRuleValue.BillItem.Inx;
                        item.BeginTime = article.VipDiscRuleValue.SubBill.BeginTime;
                        item.EndTime = article.VipDiscRuleValue.SubBill.EndTime;
                        item.PeriodDesc = string.Empty;
                        item.RuleId = 0;
                        item.RuleDesc = "折扣率" + article.VipDiscRate.ToString("f2");
                    }
                    #endregion

                    #region 购物返券
                    bool existPromOfferCoupon = false;
                    PromRuleSearcher.LookupPromOfferCouponRuleOfArticle(out existPromOfferCoupon, query.Command, situationalMode, vipCard, companyCode, saleTime, articleList);
                    if (existPromOfferCoupon && (article.PromOfferCouponCalcItemList.Count > 0))
                    {
                        foreach (CrmPromOfferCouponCalcItem calcItem in article.PromOfferCouponCalcItemList)
                        {
                            CrmPromBillItemDesc item = new CrmPromBillItemDesc();
                            promBillItemList.Add(item);
                            item.BillType = 3;
                            item.PromId = calcItem.RuleValue.Bill.PromId;
                            item.BillId = calcItem.RuleValue.Bill.BillId;
                            item.SubBillInx = calcItem.RuleValue.SubBill.Inx;
                            if (calcItem.RuleValue.BillItem == null)
                                item.ItemInx = -1;
                            else
                                item.ItemInx = calcItem.RuleValue.BillItem.Inx;
                            item.BeginTime = calcItem.RuleValue.SubBill.BeginTime;
                            item.EndTime = calcItem.RuleValue.SubBill.EndTime;
                            item.PeriodDesc = string.Empty;
                            item.RuleId = calcItem.RuleId;
                            query.SQL.Text = "select YHQFFGZMC from YHQFFGZ where YHQFFGZID = " + item.RuleId;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                    item.RuleDesc = query.Fields[0].GetChineseString(20);
                                else
                                    item.RuleDesc = query.Fields[0].AsString;
                            }
                            query.Close();
                            item.RuleDesc = string.Empty;
                            item.CouponType = calcItem.CouponType;
                            query.SQL.Text = "select YHQMC,BJ_TS from YHQDEF where YHQID = " + item.CouponType;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                    item.CouponName = query.Fields[0].GetChineseString(30);
                                else
                                    item.CouponName = query.Fields[0].AsString;
                                item.SpecialType = query.Fields[1].AsInteger;
                            }
                            query.Close();
                        }
                    }
                    #endregion

                    #region 购物返券
                    if ((payTypeId > 0) || (bankId > 0) || (bankCardCode.Length > 0))
                    {
                        CrmBankCardPayment bankCardPayment = new CrmBankCardPayment();
                        if ((bankId > 0) || (bankCardCode.Length > 0))
                        {
                            bankCardPayment.PayTypeId = payTypeId;
                            bankCardPayment.BankCardCode = bankCardCode;
                            query.SQL.Text = "select ID,YHID from YHXXITEM ";
                            query.SQL.Add(" where '").Add(bankCardCode).Add("' between CODE1 and CODE2 ");
                            query.SQL.Add("   and length(CODE1) = ").Add(bankCardCode.Length);
                            query.SQL.Add("   and BJ_TY = 0 ");
                            query.SQL.Add("order by ID desc ");
                            query.Open();
                            while (query.Eof)
                            {
                                CrmBankCardCodeScope scope = new CrmBankCardCodeScope();
                                bankCardPayment.BankCardCodeScopeList.Add(scope);
                                scope.ScopeId = query.Fields[0].AsInteger;
                                scope.BankId = query.Fields[1].AsInteger;
                                query.Next();
                            }
                            query.Close();
                        }

                        List<CrmPromPaymentOfferCouponArticle> bankCardPaymentArticleList = new List<CrmPromPaymentOfferCouponArticle>();
                        PromRuleSearcher.LookupPromOfferCouponRuleOfPayment2(out existPromOfferCoupon, query.Command, situationalMode, vipCard, companyCode, saleTime, articleList,payTypeId,bankCardPayment,bankCardPaymentArticleList);
                        if (existPromOfferCoupon && (bankCardPaymentArticleList.Count > 0))
                        {
                            foreach (CrmPromPaymentOfferCouponArticle bankCardPaymentArticle in bankCardPaymentArticleList)
                            {
                                CrmPromBillItemDesc item = new CrmPromBillItemDesc();
                                promBillItemList.Add(item);
                                item.BillType = 3;
                                item.PromId = bankCardPaymentArticle.RuleValue.Bill.PromId;
                                item.BillId = bankCardPaymentArticle.RuleValue.Bill.BillId;
                                item.SubBillInx = bankCardPaymentArticle.RuleValue.SubBill.Inx;
                                if (bankCardPaymentArticle.RuleValue.BillItem == null)
                                    item.ItemInx = -1;
                                else
                                    item.ItemInx = bankCardPaymentArticle.RuleValue.BillItem.Inx;
                                item.BeginTime = bankCardPaymentArticle.RuleValue.SubBill.BeginTime;
                                item.EndTime = bankCardPaymentArticle.RuleValue.SubBill.EndTime;
                                item.PeriodDesc = string.Empty;
                                item.RuleId = bankCardPaymentArticle.RuleId;
                                query.SQL.Text = "select YHQFFGZMC from YHQFFGZ where YHQFFGZID = " + item.RuleId;
                                query.Open();
                                if (!query.IsEmpty)
                                {
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        item.RuleDesc = query.Fields[0].GetChineseString(20);
                                    else
                                        item.RuleDesc = query.Fields[0].AsString;
                                }
                                query.Close();
                                item.RuleDesc = string.Empty;
                                item.CouponType = bankCardPaymentArticle.CouponType;
                                query.SQL.Text = "select YHQMC,BJ_TS from YHQDEF where YHQID = " + item.CouponType;
                                query.Open();
                                if (!query.IsEmpty)
                                {
                                    if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                        item.CouponName = query.Fields[0].GetChineseString(30);
                                    else
                                        item.CouponName = query.Fields[0].AsString;
                                    item.SpecialType = query.Fields[1].AsInteger;
                                }
                                query.Close();
                            }
                        }
                    }
                    #endregion

                    #region 用券
                    string couponTypeStr = string.Empty;
                    bool existPayLimit = false;
                    PromRuleSearcher.LookupPromPayCouponRuleOfArticle(out existPayLimit, query.Command, situationalMode, vipCard, couponTypeStr, companyCode, saleTime, articleList);
                    if (existPayLimit && (article.CouponPayLimitCalcItemList.Count > 0))
                    {
                        foreach (CrmCouponPayLimitCalcItem calcItem in article.CouponPayLimitCalcItemList)
                        {
                            CrmPromBillItemDesc item = new CrmPromBillItemDesc();
                            promBillItemList.Add(item);
                            item.BillType = 4;
                            item.PromId = calcItem.RuleValue.Bill.PromId;
                            //item.PromDesc = "";
                            item.BillId = calcItem.RuleValue.Bill.BillId;
                            item.SubBillInx = calcItem.RuleValue.SubBill.Inx;
                            if (calcItem.RuleValue.BillItem == null)
                                item.ItemInx = -1;
                            else
                                item.ItemInx = calcItem.RuleValue.BillItem.Inx;
                            item.BeginTime = calcItem.RuleValue.SubBill.BeginTime;
                            item.EndTime = calcItem.RuleValue.SubBill.EndTime;
                            item.PeriodDesc = string.Empty;
                            item.RuleId = calcItem.RuleId;
                            query.SQL.Text = "select YHQSYGZMC from YHQSYGZ where YHQSYGZID = " + item.RuleId;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                    item.RuleDesc = query.Fields[0].GetChineseString(20);
                                else
                                    item.RuleDesc = query.Fields[0].AsString;
                            }
                            query.Close();
                            item.RuleDesc = string.Empty;
                            item.CouponType = calcItem.CouponType;
                            query.SQL.Text = "select YHQMC,BJ_TS from YHQDEF where YHQID = " + item.CouponType;
                            query.Open();
                            if (!query.IsEmpty)
                            {
                                if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                    item.CouponName = query.Fields[0].GetChineseString(30);
                                else
                                    item.CouponName = query.Fields[0].AsString;
                                item.SpecialType = query.Fields[1].AsInteger;
                            }
                            query.Close();
                        }
                    }
                    #endregion

                    #region 满减
                    bool existPromDecMoney = false;
                    PromRuleSearcher.LookupPromDecMoneyRuleOfArticle(out existPromDecMoney, query.Command, situationalMode, vipCard, companyCode, saleTime, articleList);
                    if (article.DecMoneyRuleValue != null)
                    {
                        CrmPromBillItemDesc item = new CrmPromBillItemDesc();
                        promBillItemList.Add(item);
                        item.BillType = 5;
                        item.PromId = article.DecMoneyRuleValue.Bill.PromId;
                        //item.PromDesc = "";
                        item.BillId = article.DecMoneyRuleValue.Bill.BillId;
                        item.SubBillInx = article.DecMoneyRuleValue.SubBill.Inx;
                        if (article.DecMoneyRuleValue.BillItem == null)
                            item.ItemInx = -1;
                        else
                            item.ItemInx = article.DecMoneyRuleValue.BillItem.Inx;
                        item.BeginTime = article.DecMoneyRuleValue.SubBill.BeginTime;
                        item.EndTime = article.DecMoneyRuleValue.SubBill.EndTime;
                        item.PeriodDesc = string.Empty;
                        item.RuleId = article.DecMoneyRuleId;
                        query.SQL.Text = "select MBJZGZMC from MBJZGZ where MBJZGZID = " + item.RuleId;
                        query.Open();
                        if (!query.IsEmpty)
                        {
                            if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                item.RuleDesc = query.Fields[0].GetChineseString(20);
                            else
                                item.RuleDesc = query.Fields[0].AsString;
                        }
                        query.Close();
                    }
                    #endregion

                    if (promBillItemList.Count > 0)
                    {
                        StringBuilder promIds = new StringBuilder();
                        foreach (CrmPromBillItemDesc item in promBillItemList)
                        {
                            if (item.PromId > 0)
                                promIds.Append(item.PromId).Append(',');
                        }
                        if (promIds.Length > 0)
                        {
                            promIds.Append(0);
                            query.SQL.Text = "select CXID,CXZT from CXHDDEF where CXID in (" + promIds.ToString() + ')';
                            query.Open();
                            while (!query.Eof)
                            {
                                int promId = query.Fields[0].AsInteger;
                                string promDesc = string.Empty;
                                if (CrmServerPlatform.Config.DbCharSetIsNotChinese)
                                    promDesc = query.Fields[1].GetChineseString(40);
                                else
                                    promDesc = query.Fields[1].AsString;
                                foreach (CrmPromBillItemDesc item in promBillItemList)
                                {
                                    if (item.PromId == promId)
                                        item.PromDesc = promDesc;
                                }
                                query.Next();
                            }
                            query.Close();                            
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();   
            }
            return true;
        }

        private static string SpellSendMessageXml(CrmWXMessage SendWXMessage, bool isSveMsg)
        {
            XmlDocument repXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = repXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            repXmlDoc.AppendChild(xmlDec);
            XmlNode repXmlRoot = repXmlDoc.CreateElement("bfcrm_rep");
            repXmlDoc.AppendChild(repXmlRoot);
            XmlUtils.SetAttributeValue(repXmlDoc, repXmlRoot, "app", "0502");
            XmlUtils.SetAttributeValue(repXmlDoc, repXmlRoot, "session_id", "01");
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "openid", SendWXMessage.OpenId);
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "server_bill_id", SendWXMessage.ServerBillId.ToString());
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "member_id", SendWXMessage.MemberId.ToString());
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "member_code", SendWXMessage.CardCode);
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "time_shopping", FormatUtils.DatetimeToString(SendWXMessage.TimeShopping));
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "store_id", SendWXMessage.StoreId.ToString());
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "store_name", SendWXMessage.StoreName);
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "sale", SendWXMessage.Sale.ToString("f2"));
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "balance", SendWXMessage.Balance.ToString("f2"));
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "cent", SendWXMessage.Cent.ToString("f4"));
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "valid_cent", SendWXMessage.ValidCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "is_search", SendWXMessage.isSearch ? "Y" : "N");
            XmlUtils.AppendChildTextNode(repXmlDoc, repXmlRoot, "is_save_msg", isSveMsg ? "Y" : "N");
            return repXmlDoc.OuterXml;
        }

        public static bool DoSendMessageToServer(string serviceUrl, CrmWXMessage SendWXMessage, bool isSveMsg, out string msg)
        {
            msg = string.Empty;
            string respXml = string.Empty;
            string reqXml = SpellSendMessageXml(SendWXMessage, isSveMsg);
            DateTime timeBegin = DateTime.Now;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serviceUrl);
                req.Method = "POST";
                req.Timeout = 6000;

                StringBuilder sb = new StringBuilder();
                byte[] reqBytes_reqXml = Encoding.Default.GetBytes(reqXml);
                sb.Append("BFCRMXML").Append("0502").Append(reqBytes_reqXml.Length.ToString("d8")).Append(reqXml);
                byte[] reqBytes = Encoding.Default.GetBytes(sb.ToString());
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(reqBytes, 0, reqBytes.Length);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.Default);
                string respStr = reader.ReadToEnd();
                int len = respStr.Length;
                if (len > 16)
                {
                    respXml = respStr.Substring(16);
                }
                else
                    msg = "微信服务返回数据错误";
            }
            catch (Exception e)
            {
                msg = string.Format("访问微信服务失败 {0}。{1}", serviceUrl, e.Message);
            }
            DateTime timeEnd = DateTime.Now;
            StringBuilder logStr = new StringBuilder();
            logStr.Append("\r\n begin ").Append("0502").Append(",").Append(timeBegin.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(", ").Append(serviceUrl);
            logStr.Append("\r\n").Append(reqXml);
            logStr.Append("\r\n").Append(respXml);
            logStr.Append("\r\n end ").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(", ").Append(timeEnd.Subtract(timeBegin).TotalMilliseconds.ToString("f0")).Append(" ms");
            logStr.Append("\r\n");
            CrmServerPlatform.WriteDataLog(timeBegin.ToString("yyyy-MM-dd"), logStr.ToString());
            if (msg.Length > 0)
            {
                logStr.Length = 0;
                logStr.Append("\r\n request ").Append("0502").Append(",").Append(timeBegin.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(", ").Append(serviceUrl);
                logStr.Append("\r\n").Append(reqXml);
                logStr.Append("\r\n error ").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(", ").Append(timeEnd.Subtract(timeBegin).TotalMilliseconds.ToString("f0")).Append(" ms");
                logStr.Append("\r\n").Append(msg);
                CrmServerPlatform.WriteErrorLog(logStr.ToString());
            }
            return msg.Length == 0;
        }
    }
}
