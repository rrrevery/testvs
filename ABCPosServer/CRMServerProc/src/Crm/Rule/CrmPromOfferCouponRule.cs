using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;

namespace ChangYi.Crm.Rule
{
    public class CrmPromOfferCouponRuleBill : ChangYi.Crm.Rule.CrmRuleBill
    {
        public int CouponType = 0;
        public int AddupSaleMoneyType = 0;
    }
    public class CrmPromOfferCouponRuleBillPool : ChangYi.Crm.Rule.AbstractRuleBillPool
    {

        protected override void GetRuleBillDetail(ChangYi.Crm.Rule.CrmRuleBill bill)
        {
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
                    sql.Append("select INX,SD from HYKFQDYD_FD where JLBH = ").Append(bill.BillId);
                    sql.Append(" order by INX");
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (bill.SubBillList == null)
                            bill.SubBillList = new List<CrmRuleSubBill>();
                        CrmRuleSubBill subBill = new CrmRuleSubBill();
                        bill.SubBillList.Add(subBill);
                        subBill.Inx = DbUtils.GetInt(reader, 0);
                        subBill.BeginTime = bill.BeginTime;
                        subBill.EndTime = bill.EndTime;
                        if (reader.IsDBNull(1))
                        {
                            for (int i = 0; i < 48; i++)
                                subBill.Period[i] = 0xFF;
                        }
                        else
                            reader.GetBytes(1, 0, subBill.Period, 0, 48);
                    }
                    reader.Close();

                    if ((bill.SubBillList != null) && (bill.SubBillList.Count() > 0))
                    {
                        List<int> listInt = new List<int>();
                        List<CrmRuleValue> listRuleValue = new List<CrmRuleValue>();
                        /* 
                                                sql.Length = 0;
                                                sql.Append("select SHSPID,BJ_CJ,FQGZID from HYKFQDYD_SP where JLBH = ").Append(billId).Append(" and INX = ?");
                                                sql.Append(" order by SHSPID");
                                                cmd.CommandText = sql.ToString();

                                                DbParameter param = cmd.CreateParameter();
                                                cmd.Parameters.Clear();
                                                cmd.Parameters.Add(param);
                                                param.DbType = DbType.Int32;
                                                param.Direction = ParameterDirection.Input;

                                                foreach (CrmRuleSubBill subBill in bill.SubBillList)
                                                {
                                                    listInt.Clear();
                                                    listRuleValue.Clear();
                                                    param.Value = subBill.Inx;
                                                    reader = cmd.ExecuteReader();
                                                    while (reader.Read())
                                                    {
                                                        listInt.Add(DbUtils.GetInt(reader,0));
                                                        CrmRuleValue ruleValue = new CrmRuleValue();
                                                        listRuleValue.Add(ruleValue);
                                                        ruleValue.IsJoined = (DbUtils.GetInt(reader,1) != 0);
                                                        ruleValue.IntValue = DbUtils.GetInt(reader,2);
                                                        ruleValue.BillId = bill.BillId;
                                                        ruleValue.SubInx = subBill.Inx;                         
                                                    }
                                                    reader.Close();
                                                    if (listInt.Count() > 0)
                                                    {
                                                        subBill.ArticleIds = listInt.ToArray<int>();
                                                        subBill.ArticleRuleValues = listRuleValue.ToArray<CrmRuleValue>();
                                                    }
                                                }
                        */
                        sql.Length = 0;
                        sql.Append("select GZBH,FQGZID,BJ_CJ,CLFS_BM,CLFS_SPFL,CLFS_SPSB,CLFS_HT,CLFS_HYZ,CLFS_SP,CLFS_ZFFS,CLFS_BANK,CLFS_KH from HYKFQDYD_GZSD where JLBH = ").Append(bill.BillId);
                        DbUtils.SpellSqlParameter(conn, sql, " and ", "INX", "=");
                        sql.Append(" order by GZBH");
                        cmd.CommandText = sql.ToString();

                        cmd.Parameters.Clear();
                        DbParameter param = DbUtils.AddIntInputParameter(cmd, "INX");

                        foreach (CrmRuleSubBill subBill in bill.SubBillList)
                        {
                            param.Value = subBill.Inx;
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                if (subBill.Items == null)
                                    subBill.Items = new List<CrmRuleBillItem>();
                                CrmRuleBillItem ruleItem = new CrmRuleBillItem();
                                subBill.Items.Add(ruleItem);
                                ruleItem.Inx = DbUtils.GetInt(reader, 0);
                                ruleItem.RuleValue.IntValue = DbUtils.GetInt(reader, 1);
                                ruleItem.RuleValue.IsJoined = DbUtils.GetBool(reader, 2);
                                ruleItem.ExistDept = (DbUtils.GetInt(reader, 3) > 0);
                                ruleItem.ExistCategory = (DbUtils.GetInt(reader, 4) > 0);
                                ruleItem.ExistBrand = (DbUtils.GetInt(reader, 5) > 0);
                                ruleItem.ExistContract = (DbUtils.GetInt(reader, 6) > 0);
                                ruleItem.ExistVipGroup = (DbUtils.GetInt(reader, 7) > 0);
                                ruleItem.ExistArticle = (DbUtils.GetInt(reader, 8) > 0);
                                ruleItem.ExistPayType = (DbUtils.GetInt(reader, 9) > 0);
                                ruleItem.ExistBank = (DbUtils.GetInt(reader, 10) > 0);
                                ruleItem.ExistBankCard = (DbUtils.GetInt(reader, 11) > 0);

                                ruleItem.RuleValue.Bill = bill;
                                ruleItem.RuleValue.SubBill = subBill;
                                ruleItem.RuleValue.BillItem = ruleItem;
                            }
                            reader.Close();
                        }
                        cmd.Parameters.Clear();

                        List<string> listStr = new List<string>();
                        foreach (CrmRuleSubBill subBill in bill.SubBillList)
                        {
                            if (subBill.Items != null)
                            {
                                foreach (CrmRuleBillItem ruleItem in subBill.Items)
                                {
                                    if (ruleItem.ExistDept)
                                    {
                                        listStr.Clear();
                                        sql.Length = 0;
                                        sql.Append("select b.BMDM from HYKFQDYD_GZITEM a, SHBM b where a.SJLX = 1 and a.SJNR = b.SHBMID and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by b.BMDM");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listStr.Add(DbUtils.GetString(reader, 0).Trim());
                                        }
                                        reader.Close();
                                        if (listStr.Count == 0)
                                            ruleItem.ExistDept = false;
                                        else
                                            ruleItem.DeptCodes = listStr.ToArray<string>();
                                    }
                                    if (ruleItem.ExistContract)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 2 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistContract = false;
                                        else
                                            ruleItem.ContractIds = listInt.ToArray<int>();
                                    }
                                    if (ruleItem.ExistCategory)
                                    {
                                        listStr.Clear();
                                        sql.Length = 0;
                                        sql.Append("select b.SPFLDM from HYKFQDYD_GZITEM a,SHSPFL b where a.SJLX = 3 and a.SJNR = b.SHSPFLID and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by b.SPFLDM");

                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listStr.Add(DbUtils.GetString(reader, 0).Trim());
                                        }
                                        reader.Close();
                                        if (listStr.Count == 0)
                                            ruleItem.ExistCategory = false;
                                        else
                                            ruleItem.CategoryCodes = listStr.ToArray<string>();
                                    }
                                    if (ruleItem.ExistBrand)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 4 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistBrand = false;
                                        else
                                            ruleItem.BrandIds = listInt.ToArray<int>();
                                    }
                                    if (ruleItem.ExistVipGroup)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 5 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistVipGroup = false;
                                        else
                                        {
                                            ruleItem.VipGroups = new List<CrmVipGroup>();
                                            ChangYi.Crm.Server.CrmPubUtils.GetVipGroups(cmd, sql, listInt, ruleItem.VipGroups);
                                        }
                                    }
                                    if (ruleItem.ExistArticle)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 6 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistArticle = false;
                                        else
                                            ruleItem.ArticleIds = listInt.ToArray<int>();
                                    }
                                    if (ruleItem.ExistPayType)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 7 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistPayType = false;
                                        else
                                            ruleItem.PayTypes = listInt.ToArray<int>();
                                    }
                                    if (ruleItem.ExistBank)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 8 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistBank = false;
                                        else
                                            ruleItem.BankIds = listInt.ToArray<int>();
                                    }
                                    if (ruleItem.ExistBankCard)
                                    {
                                        listInt.Clear();
                                        sql.Length = 0;
                                        sql.Append("select a.SJNR from HYKFQDYD_GZITEM a where a.SJLX = 9 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by a.SJNR");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listInt.Add(DbUtils.GetInt(reader, 0));
                                        }
                                        reader.Close();
                                        if (listInt.Count == 0)
                                            ruleItem.ExistBankCard = false;
                                        else
                                            ruleItem.BankCardCodeScopeIds = listInt.ToArray<int>();
                                    }
                                }
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
        }
    }

    public class CrmPromOfferCouponRuleItem
    {
        public double SaleMoney = 0;
        public double OfferCouponMoney = 0;
        public string GiftCode = string.Empty;
    }

    public class CrmPromOfferCouponRule
    {
        public int RuleId = 0;
        public string RuleName = string.Empty;
        public double MaxOfferCouponMoney = 0;
        public double MinSaleMoney = 0;
        public bool IsNoOfferCoupon = false;
        public List<CrmPromOfferCouponRuleItem> Items = new List<CrmPromOfferCouponRuleItem>();
        public CrmPromOfferCouponRuleItem AddItem(double saleMoney, double offerCouponMoney)
        {
            CrmPromOfferCouponRuleItem item = new CrmPromOfferCouponRuleItem();
            item.SaleMoney = saleMoney;
            item.OfferCouponMoney = offerCouponMoney;
            return item;
        }
        public void CalcOfferCoupon(double saleMoney, out double saleMoneyUsed, out double offerCouponMoney)
        {
            saleMoneyUsed = 0;
            offerCouponMoney = 0;
            if (!IsNoOfferCoupon)
            {
                if (MathUtils.DoubleAGreaterThanDoubleB(MinSaleMoney,0) && MathUtils.DoubleASmallerThanDoubleB(saleMoney,MinSaleMoney))
                    return;
                foreach (CrmPromOfferCouponRuleItem item in Items)
                {
                    int multiple = MathUtils.Truncate((saleMoney - saleMoneyUsed) / item.SaleMoney);
                    if (multiple > 0)
                    {
                        saleMoneyUsed = saleMoneyUsed + item.SaleMoney * multiple;
                        offerCouponMoney = offerCouponMoney + item.OfferCouponMoney * multiple;
                    }
                    if (!MathUtils.DoubleAGreaterThanDoubleB(saleMoney, saleMoneyUsed))
                        break;
                }
                if ((MathUtils.DoubleAGreaterThanDoubleB(MaxOfferCouponMoney,0)) && (MathUtils.DoubleAGreaterThanDoubleB(offerCouponMoney,MaxOfferCouponMoney)))
                    offerCouponMoney = MaxOfferCouponMoney;
            }
        }
        public void CalcOfferGift(double saleMoney, out double saleMoneyUsed, out double offerGiftNum, out string giftCode)
        {
            saleMoneyUsed = 0;
            offerGiftNum = 0;
            giftCode = string.Empty;
            if (!IsNoOfferCoupon)
            {
                if (MathUtils.DoubleAGreaterThanDoubleB(MinSaleMoney, 0) && MathUtils.DoubleASmallerThanDoubleB(saleMoney, MinSaleMoney))
                    return;
                foreach (CrmPromOfferCouponRuleItem item in Items)
                {
                    int multiple = MathUtils.Truncate((saleMoney - saleMoneyUsed) / item.SaleMoney);
                    if (multiple > 0)
                    {
                        saleMoneyUsed = saleMoneyUsed + item.SaleMoney * multiple;
                        offerGiftNum = offerGiftNum + item.OfferCouponMoney * multiple;
                        giftCode = item.GiftCode;
                        break;  //杭州大厦，只找满足的第一档，如果是多档，应返回列表
                    }
                    if (!MathUtils.DoubleAGreaterThanDoubleB(saleMoney, saleMoneyUsed))
                        break;
                }
                if ((MathUtils.DoubleAGreaterThanDoubleB(MaxOfferCouponMoney, 0)) && (MathUtils.DoubleAGreaterThanDoubleB(offerGiftNum, MaxOfferCouponMoney)))
                    offerGiftNum = MaxOfferCouponMoney;
            }
        }
    }

    public class CrmPromOfferCouponRulePool
    {
        private List<CrmPromOfferCouponRule> RuleList = new List<CrmPromOfferCouponRule>();

        private CrmPromOfferCouponRule CreateRule(int ruleId)
        {
            CrmPromOfferCouponRule rule = null;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select FFXE,BJ_BFQGZ,BJ_TY,FFQDJE from YHQFFGZ where YHQFFGZID = ").Append(ruleId);
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
                        rule = new CrmPromOfferCouponRule();
                        rule.RuleId = ruleId;
                        rule.MaxOfferCouponMoney = DbUtils.GetDouble(reader, 0);
                        rule.IsNoOfferCoupon = ((DbUtils.GetBool(reader, 1)) || (DbUtils.GetBool(reader, 2)));
                        rule.MinSaleMoney = DbUtils.GetDouble(reader, 3);
                        reader.Close();

                        sql.Length = 0;
                        sql.Append("select XSJE,FQJE,LPDM from YHQFFGZITEM where YHQFFGZID = ").Append(ruleId);
                        sql.Append("  and XSJE > 0 and FQJE >= 0 order by XSJE desc ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromOfferCouponRuleItem ruleItem = new CrmPromOfferCouponRuleItem();
                            rule.Items.Add(ruleItem);
                            ruleItem.SaleMoney = DbUtils.GetDouble(reader, 0);
                            ruleItem.OfferCouponMoney = DbUtils.GetDouble(reader, 1);
                            ruleItem.GiftCode = DbUtils.GetString(reader, 2);
                        }
                        reader.Close();
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
            return rule;
        }
        public CrmPromOfferCouponRule GetRule(int ruleId)
        {
            lock (RuleList)
            {
                foreach (CrmPromOfferCouponRule rule in RuleList)
                {
                    if (rule.RuleId == ruleId)
                    {
                        return rule;
                    }
                }
                CrmPromOfferCouponRule rule2 = CreateRule(ruleId);
                if (rule2 != null)
                    RuleList.Add(rule2);
                return rule2;
            }
        }
    }
}
