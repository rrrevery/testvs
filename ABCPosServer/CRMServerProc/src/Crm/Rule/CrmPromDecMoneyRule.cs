using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;

namespace ChangYi.Crm.Rule
{
    public class CrmPromDecMoneyRuleBill : ChangYi.Crm.Rule.CrmRuleBill
    {
        public bool DecMoneyIsExpense = false;
        public int AddupSaleMoneyType = 0;
    }
    public class CrmPromDecMoneyRuleBillPool : ChangYi.Crm.Rule.AbstractRuleBillPool
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
                    sql.Append("select INX,KSRQ,JSRQ,SD from CXMBJZDYD_FD where JLBH = ").Append(bill.BillId);
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
                        if (!reader.IsDBNull(1))
                            subBill.BeginTime = DbUtils.GetDateTime(reader, 1);
                        else
                            subBill.BeginTime = bill.BeginTime;
                        if (!reader.IsDBNull(2))
                            subBill.EndTime = DbUtils.GetDateTime(reader, 2);
                        else
                            subBill.EndTime = bill.EndTime;
                        if (reader.IsDBNull(3))
                        {
                            for (int i = 0; i < 48; i++)
                                subBill.Period[i] = 0xFF;
                        }
                        else
                            reader.GetBytes(3, 0, subBill.Period, 0, 48);
                    }
                    reader.Close();

                    if ((bill.SubBillList != null) && (bill.SubBillList.Count() > 0))
                    {
                        List<int> listInt = new List<int>();
                        List<CrmRuleValue> listRuleValue = new List<CrmRuleValue>();
                        /*
                                                sql.Length = 0;
                                                sql.Append("select SHSPID,BJ_CJ,MBJZGZID from CXMBJZDYD_SP where JLBH = ").Append(billId).Append(" and INX = ?");
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
                        sql.Append("select GZBH,MBJZGZID,BJ_CJ,CLFS_BM,CLFS_SPFL,CLFS_SPSB,CLFS_HT,CLFS_HYZ,CLFS_SP from CXMBJZDYD_GZSD where JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select b.BMDM from CXMBJZDYD_GZITEM a, SHBM b where a.SJLX = 1 and a.SJNR = b.SHBMID and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select a.SJNR from CXMBJZDYD_GZITEM a where a.SJLX = 2 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select b.SPFLDM from CXMBJZDYD_GZITEM a,SHSPFL b where a.SJLX = 3 and a.SJNR = b.SHSPFLID and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select a.SJNR from CXMBJZDYD_GZITEM a where a.SJLX = 4 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select a.SJNR from CXMBJZDYD_GZITEM a where a.SJLX = 5 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select a.SJNR from CXMBJZDYD_GZITEM a where a.SJLX = 6 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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

    public class CrmPromDecMoneyRuleItem
    {
        public double SaleMoney = 0;
        public double DecMoney = 0;
    }

    public class CrmPromDecMoneyRule
    {
        public int RuleId = 0;
        public string RuleName = string.Empty;
        public double MinSaleMoney = 0;
        public double MaxDecMoney = 0;
        public bool IsNoDecMoney = false;
        public List<CrmPromDecMoneyRuleItem> Items = new List<CrmPromDecMoneyRuleItem>();
        public CrmPromDecMoneyRuleItem AddItem(double saleMoney, double decMoney)
        {
            CrmPromDecMoneyRuleItem item = new CrmPromDecMoneyRuleItem();
            item.SaleMoney = saleMoney;
            item.DecMoney = decMoney;
            return item;
        }
        public void CalcDecMoney(double saleMoney, out double saleMoneyUsed, out double decMoney)
        {
            saleMoneyUsed = 0;
            decMoney = 0;
            if (!IsNoDecMoney)
            {
                if (MathUtils.DoubleAGreaterThanDoubleB(MinSaleMoney, 0) && MathUtils.DoubleASmallerThanDoubleB(saleMoney, MinSaleMoney))
                    return;
                foreach (CrmPromDecMoneyRuleItem item in Items)
                {
                    int multiple = MathUtils.Truncate((saleMoney - saleMoneyUsed) / item.SaleMoney);
                    if (multiple > 0)
                    {
                        saleMoneyUsed = saleMoneyUsed + item.SaleMoney * multiple;
                        decMoney = decMoney + item.DecMoney * multiple;
                    }
                    if (!MathUtils.DoubleAGreaterThanDoubleB(saleMoney, saleMoneyUsed))
                        break;
                }
                if ((MathUtils.DoubleAGreaterThanDoubleB(MaxDecMoney, 0)) && (MathUtils.DoubleAGreaterThanDoubleB(decMoney, MaxDecMoney)))
                    decMoney = MaxDecMoney;
                if (MathUtils.DoubleAGreaterThanDoubleB(decMoney, saleMoneyUsed))   //不合理的满减规则，可能对后面的分摊计算造成影响
                    decMoney = saleMoneyUsed;
            }
        }
    }

    public class CrmPromDecMoneyRulePool
    {
        private List<CrmPromDecMoneyRule> RuleList = new List<CrmPromDecMoneyRule>();

        private CrmPromDecMoneyRule CreateRule(int ruleId)
        {
            CrmPromDecMoneyRule rule = null;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select QDJE,FFXE,BJ_TY from MBJZGZ where MBJZGZID = ").Append(ruleId);
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
                        rule = new CrmPromDecMoneyRule();
                        rule.RuleId = ruleId;
                        rule.MinSaleMoney = DbUtils.GetDouble(reader, 0);
                        rule.MaxDecMoney = DbUtils.GetDouble(reader, 1);
                        rule.IsNoDecMoney = (DbUtils.GetBool(reader, 2));
                        reader.Close();

                        sql.Length = 0;
                        sql.Append("select XSJE,ZKJE from MBJZGZITEM where MBJZGZID = ").Append(ruleId);
                        sql.Append("  and XSJE > 0 and ZKJE >= 0 order by XSJE desc ");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CrmPromDecMoneyRuleItem ruleItem = new CrmPromDecMoneyRuleItem();
                            rule.Items.Add(ruleItem);
                            ruleItem.SaleMoney = DbUtils.GetDouble(reader, 0);
                            ruleItem.DecMoney = DbUtils.GetDouble(reader, 1);
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
        public CrmPromDecMoneyRule GetRule(int ruleId)
        {
            lock (RuleList)
            {
                foreach (CrmPromDecMoneyRule rule in RuleList)
                {
                    if (rule.RuleId == ruleId)
                    {
                        return rule;
                    }
                }
                CrmPromDecMoneyRule rule2 = CreateRule(ruleId);
                if (rule2 != null)
                    RuleList.Add(rule2);
                return rule2;
            }
        }
    }
}
