using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;
using ChangYi.Crm.Rule;

namespace ChangYi.Crm.Server
{
    public class CrmDeptPromRuleBillId
    {
        public string DeptCode = string.Empty;
        public int RuleBillId1 = 0;
        public int PromId1 = 0;
        public int AddupSaleMoneyType1 = 0;
        public bool CanOfferCoupon1 = false;
        public int RuleBillId2 = 0;
        public int PromId2 = 0;
        public int AddupSaleMoneyType2 = 0;
        public bool CanOfferCoupon2 = false;

        public int CouponType = -1;
    }

    public class PromRuleSearcher
    {
        public static CrmDiscRuleBillPool DiscRuleBillPool = new CrmDiscRuleBillPool();
        public static CrmSpecialDiscBillPool SpecialDiscBillPool = new CrmSpecialDiscBillPool();
        public static CrmCentRuleBillPool CentRuleBillPool = new CrmCentRuleBillPool();
        public static CrmCentMultipleBillPool CentMultiRuleBillPool = new CrmCentMultipleBillPool();

        public static CrmPromPayCouponRuleBillPool PromPayCouponRuleBillPool = new CrmPromPayCouponRuleBillPool();
        public static CrmPromPayCouponRulePool PromPayCouponRulePool = new CrmPromPayCouponRulePool();
        
        public static CrmPromOfferCouponRuleBillPool PromOfferCouponRuleBillPool = new CrmPromOfferCouponRuleBillPool();
        public static CrmPromOfferCouponRulePool PromOfferCouponRulePool = new CrmPromOfferCouponRulePool();

        public static CrmPromDecMoneyRuleBillPool PromDecMoneyRuleBillPool = new CrmPromDecMoneyRuleBillPool();
        public static CrmPromDecMoneyRulePool PromDecMoneyRulePool = new CrmPromDecMoneyRulePool();

        public static CrmCentMoneyMultiRulePool CentMoneyMultiRulePool = new CrmCentMoneyMultiRulePool();

        public static void LookupVipSpecialDisc(out double discRate, out int discCombinationMode, DbCommand cmd, CrmVipCard vipCard, int storeId, DateTime saleTime)
        {
            discRate = 1;
            discCombinationMode = 0;
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select MDID,JLBH from HYTDJFDYD ");
            DbUtils.SpellSqlParameter(cmd.Connection, sql, " where ", "SJ", "");
            sql.Append(" between KSRQ and JSRQ and MDID in (0,").Append(storeId).Append(") and STATUS = 2 and DJLX = 1 order by MDID desc,JLBH desc");
            cmd.CommandText = sql.ToString();
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ", saleTime);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int ruleBillId = DbUtils.GetInt(reader, 1);
                CrmSpecialDiscBill specialDiscBill = PromRuleSearcher.SpecialDiscBillPool.GetRuleBill(ruleBillId);
                CrmSpecialDiscBillItem ruleItem = specialDiscBill.FindRuleItem(vipCard, saleTime);
                if (ruleItem != null)
                {
                    discRate = ruleItem.DiscRate;
                    discCombinationMode = ruleItem.DiscCombinationMode;
                    break;
                }
            }
            reader.Close();
            cmd.Parameters.Clear();
        }
        public static void LookupVipDiscRule(DbCommand cmd, int situationalMode, CrmVipCard vipCard, string companyCode, int storeId, DateTime saleTime, List<CrmArticle> articleList)
        {
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            //DateTime saleTime = DbUtils.GetDbServerTime(cmd);

            string oldDeptCode = string.Empty;
            List<CrmDiscRuleBill> billList = new List<CrmDiscRuleBill>();

            foreach (CrmArticle article in articleList)
            {
                article.VipDiscBillId = 0;
                article.VipDiscRate = 1;
                article.VipDiscPrecisionType = 0;
                article.VipMultiDiscRate = 1;
                article.VipDiscCombinationType = 0;
                if (article.ArticleId > 0)
                {
                    if (!article.DeptCode.Equals(oldDeptCode))
                    {
                        oldDeptCode = article.DeptCode;
                        billList.Clear();

                        sql.Length = 0;
                        sql.Append("select a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.ZKFS from HYKZKDYD a,HYKZKDYD_KLX b ");
                        sql.Append(" where a.JLBH = b.JLBH ");
                        sql.Append("   and b.HYKTYPE = ").Append(vipCard.CardTypeId);
                        sql.Append("   and a.SHDM = '").Append(companyCode).Append("'");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
                        switch (DbUtils.GetDbSystemName(cmd.Connection))
                        {
                            case DbUtils.SybaseDbSystemName:
                                sql.Append("  and ((a.SHBMDM = '') or (charindex(rtrim(a.SHBMDM),'").Append(article.DeptCode).Append("') = 1))");
                                break;
                            case DbUtils.OracleDbSystemName:
                                sql.Append("  and ((a.SHBMDM = ' ') or (instr('").Append(article.DeptCode).Append("',rtrim(a.SHBMDM)) = 1))");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        switch (situationalMode)
                        {
                            case 0:     //当前销售的会员折扣计算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 1:     //未来销售的会员折扣预算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）
                                sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        sql.Append(" order by a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
                        if (situationalMode == 2)
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmDiscRuleBill bill = new CrmDiscRuleBill();
                            billList.Add(bill);
                            bill.DeptCode = DbUtils.GetString(reader, 0);
                            bill.BillId = DbUtils.GetInt(reader, 1);
                            bill.IsPrior = DbUtils.GetBool(reader, 2);
                            bill.BeginTime = DbUtils.GetDateTime(reader, 3);
                            bill.EndTime = DbUtils.GetDateTime(reader, 4);
                            bill.DiscCombinationMode = DbUtils.GetInt(reader, 5);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                    foreach (CrmDiscRuleBill bill in billList)
                    {
                        CrmDiscRuleBill ruleBill = (CrmDiscRuleBill)PromRuleSearcher.DiscRuleBillPool.GetRuleBill(bill);
                        CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, article);
                        if (ruleValue != null)
                        {
                            article.VipDiscBillId = ruleBill.BillId;
                            article.VipDiscCombinationType = ruleBill.DiscCombinationMode;
                            if (ruleValue.IsJoined)
                            {
                                article.VipDiscRate = ruleValue.DoubleValue;
                                article.VipMultiDiscRate = ruleValue.DoubleValue2;
                                article.VipDiscPrecisionType = ruleValue.DoubleDigits;

                                article.VipDiscRuleValue = ruleValue;
                            }
                            break;
                        }
                    }
                }
            }

            double specialDiscRate = 1;
            int specialDiscMode = 0;
            PromRuleSearcher.LookupVipSpecialDisc(out specialDiscRate, out specialDiscMode, cmd, vipCard, storeId, saleTime);
            if (MathUtils.DoubleASmallerThanDoubleB(specialDiscRate, 1))
            {
                foreach (CrmArticle article in articleList)
                {
                    switch (specialDiscMode)
                    {
                        case 0:
                            if (MathUtils.DoubleASmallerThanDoubleB(specialDiscRate,article.VipDiscRate))
                                article.VipDiscRate = specialDiscRate;
                            break;
                        case 1:
                            if (MathUtils.DoubleAGreaterThanDoubleB(article.VipDiscRate,0))
                                article.VipDiscRate = article.VipDiscRate * specialDiscRate;
                            else
                                article.VipDiscRate = specialDiscRate;
                            break;
                    }
                }
            }
        }
        public static void LookupVipCentMultiple(out double billCentMultiple, out int multiMode, DbCommand cmd, CrmVipCard vipCard, int storeId, DateTime saleTime)
        {
            billCentMultiple = 0;
            multiMode = 0;
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            sql.Length = 0;
            sql.Append("select MDID,JLBH from HYTDJFDYD ");
            DbUtils.SpellSqlParameter(cmd.Connection, sql, " where ", "SJ", "");
            sql.Append(" between KSRQ and JSRQ and MDID in (0,").Append(storeId).Append(") and STATUS = 2 and DJLX = 0 order by MDID desc,JLBH desc");
            cmd.CommandText = sql.ToString();
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ", saleTime);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int ruleBillId = DbUtils.GetInt(reader, 1);
                CrmCentMultipleBill centMultiBill = PromRuleSearcher.CentMultiRuleBillPool.GetRuleBill(ruleBillId);
                CrmCentMultipleBillItem ruleItem = centMultiBill.FindRuleItem(vipCard, saleTime);
                if (ruleItem != null)
                {
                    billCentMultiple = ruleItem.Multiple;
                    multiMode = ruleItem.MultiMode;
                    break;
                }
            }
            reader.Close();
            cmd.Parameters.Clear();
        }

        public static void LookupVipCentMultipleNew(out double billCentMultiple, out int multiMode, CyQuery query, int situationalMode, CrmVipCard vipCard, int storeId, DateTime saleTime)
        {
            billCentMultiple = 0;
            multiMode = 0;

            query.SQL.Text = "select MDID,JLBH from HYTDJFDYD ";
            query.SQL.Add(" where :SJ between KSRQ and JSRQ");
            query.SQL.Add("  and MDID in (0,").Add(storeId).Add(")");
            query.SQL.Add("  and DJLX = 0 ");
            switch (situationalMode)
            {
                case 0:     //当前销售的积分计算
                    query.SQL.Add("  and a.STATUS = 2 ");
                    break;
                case 1:     //未来销售的积分预算
                    query.SQL.Add("  and a.STATUS = 2 ");
                    break;
                case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）,补积分
                    query.SQL.Add("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and :SJ < a.ZZRQ))) ");
                    break;
                default:
                    query.SQL.Add("  and 1 = 2 ");
                    break;
            }
            query.SQL.Add(" order by MDID desc,JLBH desc");
            query.ParamByName("SJ").AsDateTime = saleTime;
            query.Open();
            while (!query.Eof)
            {
                int ruleBillId = query.Fields[1].AsInteger;
                CrmCentMultipleBill centMultiBill = PromRuleSearcher.CentMultiRuleBillPool.GetRuleBill(ruleBillId);
                CrmCentMultipleBillItem ruleItem = centMultiBill.FindRuleItem(vipCard, saleTime);
                if (ruleItem != null)
                {
                    billCentMultiple = ruleItem.Multiple;
                    multiMode = ruleItem.MultiMode;
                    break;
                }
            }
            query.Close();
        }

        public static void LookupVipCentRuleOfArticle(DbCommand cmd,int situationalMode, CrmVipCard vipCard, string companyCode, DateTime saleTime, List<CrmArticle> articleList)
        {
            foreach (CrmArticle article in articleList)
            {
                article.VipCentBillId = 0;
                article.BaseCent = 0;
                article.CentMultiple = 1;
                article.CentShareRate = 0;
                article.CentMoneyMultipleRuleId = 0;
            }
            if (vipCard == null)
                return;

            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();

            string oldDeptCode = string.Empty;
            List<CrmCentRuleBill> billList = new List<CrmCentRuleBill>();

            foreach (CrmArticle article in articleList)
            {
                if (!article.IsNoCent)
                {
                    if (!article.DeptCode.Equals(oldDeptCode))
                    {
                        oldDeptCode = article.DeptCode;
                        billList.Clear();

                        sql.Length = 0;
                        sql.Append("select a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.QDZKL,a.BJ_JFBS from HYKJFDYD a,HYKJFDYD_KLX b ");
                        sql.Append(" where a.JLBH = b.JLBH ");
                        sql.Append("   and b.HYKTYPE = ").Append(vipCard.CardTypeId);
                        sql.Append("   and a.SHDM = '").Append(companyCode).Append("'");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
                        switch (DbUtils.GetDbSystemName(cmd.Connection))
                        {
                            case DbUtils.SybaseDbSystemName:
                                sql.Append("  and ((a.SHBMDM = '') or (charindex(rtrim(a.SHBMDM),'").Append(article.DeptCode).Append("') = 1))");
                                break;
                            case DbUtils.OracleDbSystemName:
                                sql.Append("  and ((a.SHBMDM = ' ') or (instr('").Append(article.DeptCode).Append("',rtrim(a.SHBMDM)) = 1))");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        switch (situationalMode)
                        {
                            case 0:     //当前销售的积分计算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 1:     //未来销售的积分预算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）,补积分
                                sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        sql.Append(" order by a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
                        if (situationalMode == 2)
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmCentRuleBill bill = new CrmCentRuleBill();
                            billList.Add(bill);
                            bill.DeptCode = DbUtils.GetString(reader, 0);
                            bill.BillId = DbUtils.GetInt(reader, 1);
                            bill.IsPrior = DbUtils.GetBool(reader, 2);
                            bill.BeginTime = DbUtils.GetDateTime(reader, 3);
                            bill.EndTime = DbUtils.GetDateTime(reader, 4);
                            bill.BaseDiscRate = DbUtils.GetDouble(reader, 5);
                            bill.CanCentMultiple = DbUtils.GetBool(reader, 6);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                    foreach (CrmCentRuleBill bill in billList)
                    {
                        CrmCentRuleBill ruleBill = (CrmCentRuleBill)PromRuleSearcher.CentRuleBillPool.GetRuleBill(bill);
                        CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, article);
                        if (ruleValue != null)
                        {
                            article.VipCentBillId = ruleBill.BillId;
                            if (ruleValue.IsJoined)
                            {
                                article.BaseCent = ruleValue.DoubleValue;
                                article.CentMultiple = ruleValue.IntValue2;
                                article.CentMoneyMultipleRuleId = ruleValue.IntValue;
                                article.CentShareRate = ruleValue.DoubleValue2;
                                article.CanCentMultiple = ruleBill.CanCentMultiple;
                                article.JoinPromCent = ruleBill.IsPrior;

                                article.VipCentRuleValue = ruleValue;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public static void LookupPromPayCouponRuleOfArticle(out bool existPromPayCoupon, DbCommand cmd, int situationalMode, CrmVipCard vipCard, string couponTypeStr, string companyCode, DateTime saleTime, List<CrmArticle> articleList)
        {
            existPromPayCoupon = false;
            foreach (CrmArticle article in articleList)
            {
                article.CouponPayLimitCalcItemList.Clear();
            }
           
            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();

            string oldDeptCode = string.Empty;
            List<CrmPromPayCouponRuleBill> billList = new List<CrmPromPayCouponRuleBill>();

            foreach (CrmArticle article in articleList)
            {
                if (!article.IsNoProm)
                {
                    if (!article.DeptCode.Equals(oldDeptCode))
                    {
                        oldDeptCode = article.DeptCode;
                        billList.Clear();

                        sql.Length = 0;
                        sql.Append("select a.YHQID,a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.CXID,a.BJ_FQ from HYKYQDYD a ");
                        sql.Append(" where a.SHDM = '").Append(companyCode).Append("'");
                        //if ((couponTypeStr != null) && (couponTypeStr.Length > 0))
                        //    sql.Append("   and a.YHQID in (").Append(couponTypeStr).Append(')');
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
                        switch (DbUtils.GetDbSystemName(cmd.Connection))
                        {
                            case DbUtils.SybaseDbSystemName:
                                sql.Append("  and ((a.SHBMDM = '') or (charindex(rtrim(a.SHBMDM),'").Append(article.DeptCode).Append("') = 1))");
                                break;
                            case DbUtils.OracleDbSystemName:
                                sql.Append("  and ((a.SHBMDM = ' ') or (instr('").Append(article.DeptCode).Append("',rtrim(a.SHBMDM)) = 1))");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        switch (situationalMode)
                        {
                            case 0:     //当前销售的用券计算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 1:     //未来销售的用券预算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）
                                sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        sql.Append(" order by a.YHQID,a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
                        if (situationalMode == 2)
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromPayCouponRuleBill bill = new CrmPromPayCouponRuleBill();
                            billList.Add(bill);
                            bill.CouponType = DbUtils.GetInt(reader, 0);
                            bill.DeptCode = DbUtils.GetString(reader, 1);
                            bill.BillId = DbUtils.GetInt(reader, 2);
                            bill.IsPrior = DbUtils.GetBool(reader, 3);
                            bill.BeginTime = DbUtils.GetDateTime(reader, 4);
                            bill.EndTime = DbUtils.GetDateTime(reader, 5);
                            bill.PromId = DbUtils.GetInt(reader, 6);
                            bill.CanOfferCoupon = DbUtils.GetBool(reader, 7);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                    int couponType = -1;
                    bool isFound = false;
                    foreach (CrmPromPayCouponRuleBill bill in billList)
                    {
                        if (couponType == bill.CouponType)
                        {
                            if (isFound)
                                continue;
                        }
                        else
                        {
                            couponType = bill.CouponType;
                            isFound = false;
                        }
                        CrmPromPayCouponRuleBill ruleBill = (CrmPromPayCouponRuleBill)PromRuleSearcher.PromPayCouponRuleBillPool.GetRuleBill(bill);
                        CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, article);
                        if (ruleValue != null)
                        {
                            isFound = true;
                            if (ruleValue.IsJoined && (ruleValue.IntValue > 0))
                            {
                                existPromPayCoupon = true;

                                CrmCouponPayLimitCalcItem calcItem = new CrmCouponPayLimitCalcItem();
                                article.CouponPayLimitCalcItemList.Add(calcItem);
                                calcItem.CouponType = ruleBill.CouponType;
                                calcItem.RuleId = ruleValue.IntValue;
                                calcItem.RuleBillId = ruleBill.BillId;
                                calcItem.PromId = ruleBill.PromId;
                                calcItem.CanOfferCoupon = ruleBill.CanOfferCoupon;

                                calcItem.RuleValue = ruleValue;
                            }
                        }
                    }
                }
            }
 
        }

        public static void LookupPromOfferCouponRuleOfArticle(out bool existPromOfferCoupon, DbCommand cmd, int situationalMode, CrmVipCard vipCard, string companyCode, DateTime saleTime, List<CrmArticle> articleList)
        {
            existPromOfferCoupon = false;
            foreach (CrmArticle article in articleList)
            {
                article.PromOfferCouponCalcItemList.Clear();
            }

            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();

            string oldDeptCode = string.Empty;
            List<CrmPromOfferCouponRuleBill> billList = new List<CrmPromOfferCouponRuleBill>();

            foreach (CrmArticle article in articleList)
            {
                if (!article.IsNoProm)
                {
                    if (!article.DeptCode.Equals(oldDeptCode))
                    {
                        oldDeptCode = article.DeptCode;
                        billList.Clear();

                        sql.Length = 0;
                        sql.Append("select a.YHQID,a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.CXID,a.XFLJFQFS from HYKFQDYD a ");
                        sql.Append(" where a.SHDM = '").Append(companyCode).Append("'");
                        sql.Append("  and ((a.BJ_HTFQ is null) or (a.BJ_HTFQ = 0)) ");
                        if ((vipCard != null) && (vipCard.Status == 0))
                            sql.Append("  and a.DJLX in (0,3) ");
                        else
                            sql.Append("  and a.DJLX = 0 ");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
                        switch (DbUtils.GetDbSystemName(cmd.Connection))
                        {
                            case DbUtils.SybaseDbSystemName:
                                sql.Append("  and ((a.SHBMDM = '') or (charindex(rtrim(a.SHBMDM),'").Append(article.DeptCode).Append("') = 1))");
                                break;
                            case DbUtils.OracleDbSystemName:
                                sql.Append("  and ((a.SHBMDM = ' ') or (instr('").Append(article.DeptCode).Append("',rtrim(a.SHBMDM)) = 1))");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        switch (situationalMode)
                        {
                            case 0:     //当前销售的返券计算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 1:     //未来销售的返券预算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）
                                sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        sql.Append(" order by a.YHQID,a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
                        if (situationalMode == 2)
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromOfferCouponRuleBill bill = new CrmPromOfferCouponRuleBill();
                            billList.Add(bill);
                            bill.CouponType = DbUtils.GetInt(reader, 0);
                            bill.DeptCode = DbUtils.GetString(reader, 1);
                            bill.BillId = DbUtils.GetInt(reader, 2);
                            bill.IsPrior = DbUtils.GetBool(reader, 3);
                            bill.BeginTime = DbUtils.GetDateTime(reader, 4);
                            bill.EndTime = DbUtils.GetDateTime(reader, 5);
                            bill.PromId = DbUtils.GetInt(reader, 6);
                            bill.AddupSaleMoneyType = DbUtils.GetInt(reader, 7);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }
                    int couponType = -1;
                    bool isFound = false;
                    foreach (CrmPromOfferCouponRuleBill bill in billList)
                    {
                        if (couponType == bill.CouponType)
                        {
                            if (isFound)
                                continue;
                        }
                        else
                        {
                            couponType = bill.CouponType;
                            isFound = false;
                        }
                        CrmPromOfferCouponRuleBill ruleBill = (CrmPromOfferCouponRuleBill)PromRuleSearcher.PromOfferCouponRuleBillPool.GetRuleBill(bill);
                        CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, article);
                        if (ruleValue != null)
                        {
                            isFound = true;
                            if (ruleValue.IsJoined && (ruleValue.IntValue > 0))
                            {
                                existPromOfferCoupon = true;
                                article.JoinOfferCoupon = true;

                                CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                                article.PromOfferCouponCalcItemList.Add(calcItem);
                                calcItem.CouponType = ruleBill.CouponType;
                                calcItem.RuleId = ruleValue.IntValue;
                                calcItem.RuleBillId = ruleBill.BillId;
                                calcItem.PromId = ruleBill.PromId;
                                calcItem.AddupSaleMoneyType = ruleBill.AddupSaleMoneyType;

                                calcItem.RuleValue = ruleValue;
                            }
                        }
                    }
                }
            }
        }

        public static void LookupPromOfferCouponRuleOfPayment(out bool existPromOfferCoupon, DbCommand cmd, int situationalMode, CrmVipCard vipCard, string companyCode, DateTime saleTime, List<CrmPaymentArticleShare> paymentArticleShareList,List<CrmPromPaymentOfferCouponArticle> paymentOfferCouponArticleList)
        {
            existPromOfferCoupon = false;
            List<CrmPromOfferCouponRuleBill> billList = new List<CrmPromOfferCouponRuleBill>();

            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            #region 取商户的所有规则单
            sql.Length = 0;
            sql.Append("select a.YHQID,a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.CXID,a.XFLJFQFS from HYKFQDYD a ");
            sql.Append(" where a.SHDM = '").Append(companyCode).Append("'");
            sql.Append("  and ((a.BJ_HTFQ is null) or (a.BJ_HTFQ = 0)) ");
            sql.Append("  and a.DJLX = 2 ");
            sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
            switch (situationalMode)
            {
                case 0:     //当前销售的返券计算
                    sql.Append("  and a.STATUS = 2 ");
                    break;
                case 1:     //未来销售的返券预算
                    sql.Append("  and a.STATUS = 2 ");
                    break;
                case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）
                    sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                    break;
                default:
                    sql.Append("  and 1 = 2 ");
                    break;
            }
            sql.Append(" order by a.YHQID,a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
            if (situationalMode == 2)
                DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmPromOfferCouponRuleBill bill = new CrmPromOfferCouponRuleBill();
                billList.Add(bill);
                bill.CouponType = DbUtils.GetInt(reader, 0);
                bill.DeptCode = DbUtils.GetString(reader, 1);
                bill.BillId = DbUtils.GetInt(reader, 2);
                bill.IsPrior = DbUtils.GetBool(reader, 3);
                bill.BeginTime = DbUtils.GetDateTime(reader, 4);
                bill.EndTime = DbUtils.GetDateTime(reader, 5);
                bill.PromId = DbUtils.GetInt(reader, 6);
                bill.AddupSaleMoneyType = DbUtils.GetInt(reader, 7);
            }
            reader.Close();
            cmd.Parameters.Clear();
            #endregion

            if (billList.Count > 0)
            {
                List<CrmBankCardPaymentArticleShare> bankCardPaymentArticleList = new List<CrmBankCardPaymentArticleShare>();
                PromCalculator.SharePaymentArticleToBankCard(paymentArticleShareList, bankCardPaymentArticleList);

                List<CrmArticle> articleList = new List<CrmArticle>();
                foreach (CrmBankCardPaymentArticleShare bankCardPaymentArticle in bankCardPaymentArticleList)
                {
                    if (!articleList.Contains(bankCardPaymentArticle.PaymentArticleShare.Article))
                        articleList.Add(bankCardPaymentArticle.PaymentArticleShare.Article);
                }

                foreach (CrmArticle article in articleList)
                {
                    #region 取销售商品的分类、品牌、合同、供货商
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
                    #endregion
                }
                
                List<string> deptCodeList = new List<string>();
                foreach (CrmArticle article in articleList)
                {
                    if (!deptCodeList.Contains(article.DeptCode))
                        deptCodeList.Add(article.DeptCode);
                }
                List<CrmPromOfferCouponRuleBill> billList2 = new List<CrmPromOfferCouponRuleBill>();
                foreach (string deptCode in deptCodeList)
                {
                    billList2.Clear();
                    #region 取部门的规则单
                    foreach (CrmPromOfferCouponRuleBill bill in billList)
                    {
                        if (deptCode.StartsWith(bill.DeptCode))
                        {
                            billList2.Add(bill);
                        }
                    }
                    #endregion

                    if (billList2.Count > 0)
                    {
                        foreach (CrmBankCardPaymentArticleShare bankCardPaymentArticle in bankCardPaymentArticleList)
                        {
                            if (deptCode.Equals(bankCardPaymentArticle.PaymentArticleShare.Article.DeptCode) && (!bankCardPaymentArticle.PaymentArticleShare.Article.IsNoProm))
                            {
                                int couponType = -1;
                                bool isFound = false;
                                foreach (CrmPromOfferCouponRuleBill bill in billList2)
                                {
                                    if (couponType == bill.CouponType)
                                    {
                                        if (isFound)
                                            continue;
                                    }
                                    else
                                    {
                                        couponType = bill.CouponType;
                                        isFound = false;
                                    }
                                    CrmBankCardCodeScope bankCardCodeScope = null;
                                    CrmPromOfferCouponRuleBill ruleBill = (CrmPromOfferCouponRuleBill)PromRuleSearcher.PromOfferCouponRuleBillPool.GetRuleBill(bill);
                                    CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, bankCardPaymentArticle.PaymentArticleShare.Article, bankCardPaymentArticle.PaymentArticleShare.Payment.PayTypeId, bankCardPaymentArticle.BankCardPayment, out bankCardCodeScope);
                                    if (ruleValue != null)
                                    {
                                        isFound = true;
                                        if (ruleValue.IsJoined && (ruleValue.IntValue > 0))
                                        {
                                            existPromOfferCoupon = true;

                                            CrmPromPaymentOfferCouponArticle paymentOfferCouponArticle = new CrmPromPaymentOfferCouponArticle();
                                            paymentOfferCouponArticleList.Add(paymentOfferCouponArticle);
                                            paymentOfferCouponArticle.Article = bankCardPaymentArticle.PaymentArticleShare.Article;
                                            paymentOfferCouponArticle.Payment = bankCardPaymentArticle.PaymentArticleShare.Payment;
                                            paymentOfferCouponArticle.BankCardPayment = bankCardPaymentArticle.BankCardPayment;
                                            paymentOfferCouponArticle.PayMoney = bankCardPaymentArticle.PayMoney;
                                            paymentOfferCouponArticle.CouponType = ruleBill.CouponType;
                                            paymentOfferCouponArticle.RuleId = ruleValue.IntValue;
                                            paymentOfferCouponArticle.RuleBillId = ruleBill.BillId;
                                            paymentOfferCouponArticle.PromId = ruleBill.PromId;
                                            if (bankCardCodeScope != null)
                                                paymentOfferCouponArticle.BankId = bankCardCodeScope.BankId;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void LookupPromOfferCouponRuleOfPayment2(out bool existPromOfferCoupon, DbCommand cmd, int situationalMode, CrmVipCard vipCard, string companyCode, DateTime saleTime, List<CrmArticle> articleList, int payTypeId, CrmBankCardPayment bankCardPayment, List<CrmPromPaymentOfferCouponArticle> bankCardPaymentArticleList)
        {
            existPromOfferCoupon = false;
            List<CrmPromOfferCouponRuleBill> billList = new List<CrmPromOfferCouponRuleBill>();

            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();
            #region 取商户的所有规则单
            sql.Length = 0;
            sql.Append("select a.YHQID,a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.CXID,a.XFLJFQFS from HYKFQDYD a ");
            sql.Append(" where a.SHDM = '").Append(companyCode).Append("'");
            sql.Append("  and ((a.BJ_HTFQ is null) or (a.BJ_HTFQ = 0)) ");
            sql.Append("  and a.DJLX = 2 ");
            sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
            switch (situationalMode)
            {
                case 0:     //当前销售的返券计算
                    sql.Append("  and a.STATUS = 2 ");
                    break;
                case 1:     //未来销售的返券预算
                    sql.Append("  and a.STATUS = 2 ");
                    break;
                case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）
                    sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                    break;
                default:
                    sql.Append("  and 1 = 2 ");
                    break;
            }
            sql.Append(" order by a.YHQID,a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
            if (situationalMode == 2)
                DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
            cmd.CommandText = sql.ToString();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CrmPromOfferCouponRuleBill bill = new CrmPromOfferCouponRuleBill();
                billList.Add(bill);
                bill.CouponType = DbUtils.GetInt(reader, 0);
                bill.DeptCode = DbUtils.GetString(reader, 1);
                bill.BillId = DbUtils.GetInt(reader, 2);
                bill.IsPrior = DbUtils.GetBool(reader, 3);
                bill.BeginTime = DbUtils.GetDateTime(reader, 4);
                bill.EndTime = DbUtils.GetDateTime(reader, 5);
                bill.PromId = DbUtils.GetInt(reader, 6);
                bill.AddupSaleMoneyType = DbUtils.GetInt(reader, 7);
            }
            reader.Close();
            cmd.Parameters.Clear();
            #endregion

            if (billList.Count > 0)
            {
                List<string> deptCodeList = new List<string>();
                foreach (CrmArticle article in articleList)
                {
                    if (!deptCodeList.Contains(article.DeptCode))
                        deptCodeList.Add(article.DeptCode);
                }
                List<CrmPromOfferCouponRuleBill> billList2 = new List<CrmPromOfferCouponRuleBill>();
                foreach (string deptCode in deptCodeList)
                {
                    billList2.Clear();
                    #region 取部门的规则单
                    foreach (CrmPromOfferCouponRuleBill bill in billList)
                    {
                        if (deptCode.StartsWith(bill.DeptCode))
                        {
                            billList2.Add(bill);
                        }
                    }
                    #endregion

                    if (billList2.Count > 0)
                    {
                        foreach (CrmArticle article in articleList)
                        {
                            if (deptCode.Equals(article.DeptCode) && (!article.IsNoProm))
                            {
                                int couponType = -1;
                                bool isFound = false;
                                foreach (CrmPromOfferCouponRuleBill bill in billList2)
                                {
                                    if (couponType == bill.CouponType)
                                    {
                                        if (isFound)
                                            continue;
                                    }
                                    else
                                    {
                                        couponType = bill.CouponType;
                                        isFound = false;
                                    }
                                    CrmBankCardCodeScope bankCardCodeScope = null;
                                    CrmPromOfferCouponRuleBill ruleBill = (CrmPromOfferCouponRuleBill)PromRuleSearcher.PromOfferCouponRuleBillPool.GetRuleBill(bill);
                                    CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, article, payTypeId, bankCardPayment, out bankCardCodeScope);
                                    if (ruleValue != null)
                                    {
                                        isFound = true;
                                        if (ruleValue.IsJoined && (ruleValue.IntValue > 0))
                                        {
                                            existPromOfferCoupon = true;
                                            CrmPromPaymentOfferCouponArticle bankCardPaymentArticle = new CrmPromPaymentOfferCouponArticle();
                                            bankCardPaymentArticleList.Add(bankCardPaymentArticle);
                                            bankCardPaymentArticle.Article = article;
                                            bankCardPaymentArticle.BankCardPayment = bankCardPayment;
                                            bankCardPaymentArticle.CouponType = ruleBill.CouponType;
                                            bankCardPaymentArticle.RuleId = ruleValue.IntValue;
                                            bankCardPaymentArticle.RuleBillId = ruleBill.BillId;
                                            bankCardPaymentArticle.PromId = ruleBill.PromId;
                                            //bankCardPaymentArticle.AddupSaleMoneyType = ruleBill.AddupSaleMoneyType;
                                            if (bankCardCodeScope != null)
                                                bankCardPaymentArticle.BankId = bankCardCodeScope.BankId;

                                            bankCardPaymentArticle.RuleValue = ruleValue;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void LookupPromDecMoneyRuleOfArticle(out bool existPromDecMoney, DbCommand cmd, int situationalMode, CrmVipCard vipCard, string companyCode, DateTime saleTime, List<CrmArticle> articleList)
        {
            existPromDecMoney = false;
            foreach (CrmArticle article in articleList)
            {
                article.DecMoney = 0;
                article.DecMoneyRuleId = 0;
                article.DecMoneyRuleBillId = 0;
                article.DecMoneyPromId = 0;
                article.DecMoneyAddupSaleMoneyType = 0;
                article.DecMoneyIsExpense = false;
            }

            DbDataReader reader = null;
            StringBuilder sql = new StringBuilder();

            string oldDeptCode = string.Empty;
            List<CrmPromDecMoneyRuleBill> billList = new List<CrmPromDecMoneyRuleBill>();

            foreach (CrmArticle article in articleList)
            {
                if (!article.IsNoProm)
                {
                    if (!article.DeptCode.Equals(oldDeptCode))
                    {
                        oldDeptCode = article.DeptCode;
                        billList.Clear();

                        sql.Length = 0;
                        sql.Append("select a.SHBMDM,a.JLBH,a.YXCLBJ,a.RQ1,a.RQ2,a.CXID,a.XFLJMJFS,a.MJJQ from CXMBJZDYD a ");
                        sql.Append(" where a.SHDM = '").Append(companyCode).Append("'");
                        sql.Append("   and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ1")).Append("  between RQ1 and RQ2 ");
                        switch (DbUtils.GetDbSystemName(cmd.Connection))
                        {
                            case DbUtils.SybaseDbSystemName:
                                sql.Append("  and ((a.SHBMDM = '') or (charindex(rtrim(a.SHBMDM),'").Append(article.DeptCode).Append("') = 1))");
                                break;
                            case DbUtils.OracleDbSystemName:
                                sql.Append("  and ((a.SHBMDM = ' ') or (instr('").Append(article.DeptCode).Append("',rtrim(a.SHBMDM)) = 1))");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        switch (situationalMode)
                        {
                            case 0:     //当前销售的满减计算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 1:     //未来销售的满减预算
                                sql.Append("  and a.STATUS = 2 ");
                                break;
                            case 2:     //选单换货（STATUS = 2 启动 3 提前手工终止 4 到期自动终止）
                                sql.Append("  and ((a.STATUS = 2) or (a.STATUS = 4) or ((a.STATUS = 3) and ").Append(DbUtils.SpellSqlParameter(cmd.Connection, "SJ2")).Append(" < a.ZZRQ))) ");
                                break;
                            default:
                                sql.Append("  and 1 = 2 ");
                                break;
                        }
                        sql.Append(" order by a.SHBMDM desc,a.YXCLBJ desc,a.JLBH desc");
                        DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ1", saleTime);
                        if (situationalMode == 2)
                            DbUtils.AddDatetimeInputParameterAndValue(cmd, "SJ2", saleTime);
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromDecMoneyRuleBill bill = new CrmPromDecMoneyRuleBill();
                            billList.Add(bill);
                            bill.DeptCode = DbUtils.GetString(reader, 0);
                            bill.BillId = DbUtils.GetInt(reader, 1);
                            bill.IsPrior = DbUtils.GetBool(reader, 2);
                            bill.BeginTime = DbUtils.GetDateTime(reader, 3);
                            bill.EndTime = DbUtils.GetDateTime(reader, 4);
                            bill.PromId = DbUtils.GetInt(reader, 5);
                            bill.AddupSaleMoneyType = DbUtils.GetInt(reader, 6);
                            bill.DecMoneyIsExpense = DbUtils.GetBool(reader, 7);
                        }
                        reader.Close();
                        cmd.Parameters.Clear();
                    }

                    foreach (CrmPromDecMoneyRuleBill bill in billList)
                    {
                        CrmPromDecMoneyRuleBill ruleBill = (CrmPromDecMoneyRuleBill)PromRuleSearcher.PromDecMoneyRuleBillPool.GetRuleBill(bill);
                        CrmRuleValue ruleValue = ruleBill.FindRuleValue(saleTime, vipCard, article);
                        if (ruleValue != null)
                        {
                            article.DecMoneyRuleBillId = ruleBill.BillId;
                            if (ruleValue.IsJoined && (ruleValue.IntValue > 0))
                            {
                                existPromDecMoney = true;
                                article.DecMoneyRuleId = ruleValue.IntValue;
                                article.DecMoneyPromId = ruleBill.PromId;
                                article.DecMoneyAddupSaleMoneyType = ruleBill.AddupSaleMoneyType;
                                article.DecMoneyIsExpense = ruleBill.DecMoneyIsExpense;
                            }
                            break;
                        }
                    }
                }
            }
        }

    }

}
