using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;
using ChangYi.Crm.Server;

namespace ChangYi.Crm.Rule
{
    public class CrmCentRuleBill : ChangYi.Crm.Rule.CrmRuleBill
    {
        public bool CanCentMultiple = false;
        public double BaseDiscRate = 0;
    }

    public class CrmCentRuleBillPool : ChangYi.Crm.Rule.AbstractRuleBillPool
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
                    sql.Append("select INX,KSRQ,JSRQ,SD from HYKJFDYD_FD where JLBH = ").Append(bill.BillId);
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
                        DbParameter param = null;

                        sql.Length = 0;
                        sql.Append("select SHSPID,BJ_CJ,FZ,BS,FTBL from HYKJFDYD_SP where JLBH = ").Append(bill.BillId);
                        DbUtils.SpellSqlParameter(conn, sql, " and ", "INX", "=");
                        sql.Append(" order by SHSPID");
                        cmd.CommandText = sql.ToString();

                        cmd.Parameters.Clear();
                        param = DbUtils.AddIntInputParameter(cmd, "INX");

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
                                ruleValue.IsJoined = DbUtils.GetBool(reader,1);
                                ruleValue.DoubleValue = DbUtils.GetDouble(reader,2);
                                ruleValue.IntValue2 = DbUtils.GetInt(reader, 3);
                                ruleValue.DoubleValue2 = DbUtils.GetDouble(reader, 4);

                                ruleValue.Bill = bill;
                                ruleValue.SubBill = subBill;
                            }
                            reader.Close();
                            if (listInt.Count() > 0)
                            {
                                subBill.ArticleIds = listInt.ToArray<int>();
                                subBill.ArticleRuleValues = listRuleValue.ToArray<CrmRuleValue>();
                            }
                        }

                        sql.Length = 0;
                        sql.Append("select GZBH,FZ,BJ_CJ,CLFS_BM,CLFS_SPFL,CLFS_SPSB,CLFS_HT,CLFS_HYZ,CLFS_SP,BS,FTBL,JFFBGZ from HYKJFDYD_GZSD where JLBH = ").Append(bill.BillId);
                        DbUtils.SpellSqlParameter(conn, sql, " and ", "INX", "=");
                        sql.Append(" order by GZBH");
                        cmd.CommandText = sql.ToString();

                        cmd.Parameters.Clear();
                        param = DbUtils.AddIntInputParameter(cmd, "INX");

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
                                ruleItem.RuleValue.DoubleValue = DbUtils.GetDouble(reader, 1);
                                ruleItem.RuleValue.IsJoined = DbUtils.GetBool(reader, 2);
                                ruleItem.ExistDept = (DbUtils.GetInt(reader, 3) > 0);
                                ruleItem.ExistCategory = (DbUtils.GetInt(reader, 4) > 0);
                                ruleItem.ExistBrand = (DbUtils.GetInt(reader, 5) > 0);
                                ruleItem.ExistContract = (DbUtils.GetInt(reader, 6) > 0);
                                ruleItem.ExistVipGroup = (DbUtils.GetInt(reader, 7) > 0);
                                ruleItem.ExistArticle = (DbUtils.GetInt(reader, 8) > 0);
                                ruleItem.RuleValue.IntValue2 = DbUtils.GetInt(reader, 9);
                                ruleItem.RuleValue.DoubleValue2 = DbUtils.GetDouble(reader, 10);
                                ruleItem.RuleValue.IntValue = DbUtils.GetInt(reader, 11);

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
                                        sql.Append("select b.BMDM from HYKJFDYD_GZITEM a, SHBM b where a.SJLX = 1 and a.SJNR = b.SHBMID and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by b.BMDM");
                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listStr.Add(reader.GetString(0).Trim());
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
                                        sql.Append("select a.SJNR from HYKJFDYD_GZITEM a where a.SJLX = 2 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select b.SPFLDM from HYKJFDYD_GZITEM a,SHSPFL b where a.SJLX = 3 and a.SJNR = b.SHSPFLID and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
                                        sql.Append("  and a.INX = ").Append(subBill.Inx);
                                        sql.Append("  and a.GZBH = ").Append(ruleItem.Inx);
                                        sql.Append(" order by b.SPFLDM");

                                        cmd.CommandText = sql.ToString();

                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            listStr.Add(reader.GetString(0).Trim());
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
                                        sql.Append("select a.SJNR from HYKJFDYD_GZITEM a where a.SJLX = 4 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select a.SJNR from HYKJFDYD_GZITEM a where a.SJLX = 5 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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
                                        sql.Append("select a.SJNR from HYKJFDYD_GZITEM a where a.SJLX = 6 and a.SJNR > 0 and a.JLBH = ").Append(bill.BillId);
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

    public class CrmCentMultipleBillItem
    {
        public int Inx = 0;
        public double Multiple = 1;
        public int MultiMode = 0;

        public CrmVipGroup VipGroup = new CrmVipGroup();

        public bool Match(CrmVipCard vipCard, DateTime saleTime)
        {
            return VipGroup.Match(vipCard, saleTime);
        }
    }

    public class CrmCentMultipleBill
    {
        public int BillId = 0;
        public DateTime BeginTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;
        public List<CrmCentMultipleBillItem> Items = null;

        public CrmCentMultipleBillItem FindRuleItem(CrmVipCard vipCard, DateTime saleTime)
        {
            if ((saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (Items != null))
            {
                foreach (CrmCentMultipleBillItem item in Items)
                {
                    if (item.Match(vipCard, saleTime))
                    {
                        return item;
                    }
                }
            }
            return null;
        }
    }

    public class CrmCentMultipleBillPool {
	    private List<CrmCentMultipleBill> billList = new List<CrmCentMultipleBill>();

	    public CrmCentMultipleBill GetRuleBill(int billId)
        {
            lock (billList)
            {
                foreach (CrmCentMultipleBill bill in billList)
                {
                    if (bill.BillId == billId)
                    {
                        return bill;
                    }
                }
                CrmCentMultipleBill bill2 = CreateRuleBill(billId);
                if (bill2 != null)
                    billList.Add(bill2);
                return bill2;
            }
	    }
    	
	    private CrmCentMultipleBill CreateRuleBill(int billId)
        {
            CrmCentMultipleBill bill = null;
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
                    sql.Append("select KSRQ,JSRQ from HYTDJFDYD where JLBH = ").Append(billId);
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        bill = new CrmCentMultipleBill();
                        bill.BillId = billId;
                        bill.BeginTime = DbUtils.GetDateTime(reader, 0);
                        bill.EndTime = DbUtils.GetDateTime(reader, 1);
                        reader.Close();

                        sql.Length = 0;
                        sql.Append("select GZBH,JFBS,BSFS,GRPID from HYTDJFDYD_GZSD where JLBH = ").Append(billId);
                        sql.Append(" order by GZBH");
                        cmd.CommandText = sql.ToString();
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            if (bill.Items == null)
                                bill.Items = new List<CrmCentMultipleBillItem>();
                            CrmCentMultipleBillItem item = new CrmCentMultipleBillItem();
                            bill.Items.Add(item);
                            item.Inx = DbUtils.GetInt(reader, 0);
                            item.Multiple = DbUtils.GetDouble(reader, 1);
                            item.MultiMode = DbUtils.GetInt(reader, 2);
                            item.VipGroup.GroupId = DbUtils.GetInt(reader, 3);
                        }
                        reader.Close();

                        if (bill.Items != null)
                        {
                            foreach (CrmCentMultipleBillItem item in bill.Items)
                            {
                                if (item.VipGroup.GroupId > 0)
                                {
                                    CrmPubUtils.GetVipGroup(cmd, sql, item.VipGroup);
                                }
                            }
                        }
                        return bill;
                    }
                    else
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
		    return null;
	    }
    }

    public class CrmCentMoneyMultiRuleItem
    {
        public double SaleMoney = 1;
        public double Multiple = 1;
    }

    public class CrmCentMoneyMultiRule
    {
        public int RuleID = 0;
        public string RuleName = string.Empty;
        public List<CrmCentMoneyMultiRuleItem> Items = new List<CrmCentMoneyMultiRuleItem>();
        public CrmCentMoneyMultiRuleItem AddItem(double saleMoney, double multiple)
        {
            CrmCentMoneyMultiRuleItem item = new CrmCentMoneyMultiRuleItem();
            Items.Add(item);
            item.SaleMoney = saleMoney;
            item.Multiple = multiple;
            return item;
        }
        public bool LookupMultiple(double saleMoney, out double multiple)
        {
            multiple = 0;
            foreach (CrmCentMoneyMultiRuleItem item in Items)
            {
                if (!MathUtils.DoubleASmallerThanDoubleB(saleMoney, item.SaleMoney))
                {
                    multiple = item.Multiple;
                    return true;
                }
            }
            return false;
        }
    }

    public class CrmCentMoneyMultiRulePool
    {
        private List<CrmCentMoneyMultiRule> RuleList = new List<CrmCentMoneyMultiRule>();

        private CrmCentMoneyMultiRule CreateRule(int ruleID)
        {
            CrmCentMoneyMultiRule rule = null;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            sql.Append("select GZMC from JFBSGZ where GZID = ").Append(ruleID);
            cmd.CommandText = sql.ToString();
            try
            {
                conn.Open();
                DbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    rule = new CrmCentMoneyMultiRule();
                    rule.RuleID = ruleID;
                    rule.RuleName = DbUtils.GetString(reader, 0);
                    reader.Close();

                    sql.Length = 0;
                    sql.Append("select XSJE,BS from JFBSGZITEM where GZID = ").Append(ruleID);
                    sql.Append("  and XSJE > 0 order by XSJE desc ");
                    cmd.CommandText = sql.ToString();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        rule.AddItem(DbUtils.GetDouble(reader, 0), DbUtils.GetDouble(reader, 1));
                    }
                    reader.Close();
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }
            return rule;
        }
        public CrmCentMoneyMultiRule GetRule(int ruleID)
        {
            lock (RuleList)
            {
                foreach (CrmCentMoneyMultiRule rule in RuleList)
                {
                    if (rule.RuleID == ruleID)
                    {
                        return rule;
                    }
                }
                CrmCentMoneyMultiRule rule2 = CreateRule(ruleID);
                if (rule2 != null)
                    RuleList.Add(rule2);
                return rule2;
            }
        }
    }
}
