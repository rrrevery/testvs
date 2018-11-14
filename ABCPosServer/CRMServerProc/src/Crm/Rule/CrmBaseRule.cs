using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using ChangYi.Pub;
using ChangYi.Crm.Server;

namespace ChangYi.Crm.Rule
{
    public class CrmVipGroup
    {
        public int GroupId = 0;
        public string GroupName = string.Empty;

        public int BirthdayMode = 0;
        public bool OnlyExistVipId = false;
        public HashSet<int> VipIds = null;
        public List<int> VipTypes = null;
        public List<int> IdCardTypes = null;
        public List<int> JobTypes = null;
        public List<int> SexTypes = null;
        public List<int> IssueCardCompanyIds = null;

        public List<int> VipIdList = null;

        public bool Match(CrmVipCard vipCard, DateTime saleTime)
        {
            if (vipCard == null) return false;
            bool ok = false;
            if (OnlyExistVipId)
            {
                ok = ((VipIds != null) && VipIds.Contains(vipCard.CardId));
            }
            else
            {
                ok = ((VipTypes == null) || (VipTypes.Contains(vipCard.CardTypeId)))
                    && ((IdCardTypes == null) || (IdCardTypes.Contains(vipCard.IdCardType)))
                    && ((JobTypes == null) || (JobTypes.Contains(vipCard.JobType)))
                    && ((SexTypes == null) || (SexTypes.Contains(vipCard.SexType)))
                    && ((IssueCardCompanyIds == null) || (IssueCardCompanyIds.Contains(vipCard.IssueCardCompanyId)));
            }
            if (ok && (BirthdayMode > 0))
            {
                switch (BirthdayMode)
                {
                    case 1:     //生日
                        ok = ((vipCard.Birthday > DateTime.MinValue) && (DateTimeUtils.CheckBirthday(vipCard.Birthday, saleTime, vipCard.BirthdayIsChinese)));
                        break;
                    case 2:     //生日当月
                        ok = ((vipCard.Birthday > DateTime.MinValue) && (DateTimeUtils.CheckBirthmonth(vipCard.Birthday, saleTime, vipCard.BirthdayIsChinese)));
                        break;
                }
            }
            return ok;
        }
    }

    public class CrmRuleValue
    {
        public bool IsJoined = false;
        public int IntValue = 0;
        public int IntValue2 = 0;
        public double DoubleValue = 0;
        public double DoubleValue2 = 0;
        public double DoubleValue3 = 0;
        public int DoubleDigits = 0;

        public CrmRuleBill Bill = null;
        public CrmRuleSubBill SubBill = null;
        public CrmRuleBillItem BillItem = null;
      
    }

    public class CrmRuleBillItem
    {
        public int Inx = 0;
        public CrmRuleValue RuleValue = new CrmRuleValue();
        public bool ExistContract = false;
        public bool ExistBrand = false;
        public bool ExistDept = false;
        public bool ExistCategory = false;
        public bool ExistVipGroup = false;
        public bool ExistArticle = false;
        public bool ExistPayType = false;
        public bool ExistBank = false;
        public bool ExistBankCard = false;

        public int[] ContractIds = null;
        public int[] BrandIds = null;
        public String[] DeptCodes = null;
        public String[] CategoryCodes = null;
        public List<CrmVipGroup> VipGroups = null;
        public int[] ArticleIds = null;
        public int[] PayTypes = null;
        public int[] BankIds = null;
        public int[] BankCardCodeScopeIds = null;

        //public bool Match(CrmArticle article)
        //{
        //    return ((!ExistDept) || (SeekUtils.IndexOfLikeStringArray(DeptCodes, article.DeptCode) >= 0))
        //           && ((!ExistCategory) || (SeekUtils.IndexOfLikeStringArray(CategoryCodes, article.CategoryCode) >= 0))
        //           && ((!ExistContract) || (SeekUtils.IndexOfAscendingIntArray(ContractIds, article.ContractId) >= 0))
        //           && ((!ExistBrand) || (SeekUtils.IndexOfAscendingIntArray(BrandIds, article.BrandId) >= 0))
        //           && ((!ExistArticle) || (SeekUtils.IndexOfAscendingIntArray(ArticleIds, article.ArticleId) >= 0));    //翠微要改
        //}

        public bool Match(CrmArticle article, CrmVipCard vipCard, DateTime saleDate)
        {
            bool ok = ((!ExistDept) || (SeekUtils.IndexOfLikeStringArray(DeptCodes, article.DeptCode) >= 0))
                   && ((!ExistCategory) || (SeekUtils.IndexOfLikeStringArray(CategoryCodes, article.CategoryCode) >= 0))
                   && ((!ExistContract) || (SeekUtils.IndexOfAscendingIntArray(ContractIds, article.ContractId) >= 0))
                   && ((!ExistBrand) || (SeekUtils.IndexOfAscendingIntArray(BrandIds, article.BrandId) >= 0))
                   && ((!ExistArticle) || (SeekUtils.IndexOfAscendingIntArray(ArticleIds, article.ArticleId) >= 0));
            if (ok && ExistVipGroup && (VipGroups != null))
            {
                foreach(CrmVipGroup vipGroup in VipGroups)
                {
                    if (vipGroup.Match(vipCard,saleDate))
                        return true;
                }
                ok = false;
            }
            return ok;
        }

        public bool Match(CrmArticle article, CrmVipCard vipCard, DateTime saleDate, int payTypeId, CrmBankCardPayment bankCard, out CrmBankCardCodeScope bankCardCodeScope)
        {
            bankCardCodeScope = null;
            bool ok = ((!ExistDept) || (SeekUtils.IndexOfLikeStringArray(DeptCodes, article.DeptCode) >= 0))
                   && ((!ExistCategory) || (SeekUtils.IndexOfLikeStringArray(CategoryCodes, article.CategoryCode) >= 0))
                   && ((!ExistContract) || (SeekUtils.IndexOfAscendingIntArray(ContractIds, article.ContractId) >= 0))
                   && ((!ExistBrand) || (SeekUtils.IndexOfAscendingIntArray(BrandIds, article.BrandId) >= 0))
                   && ((!ExistArticle) || (SeekUtils.IndexOfAscendingIntArray(ArticleIds, article.ArticleId) >= 0))
                   && ((!ExistPayType) || ((payTypeId > 0) && (SeekUtils.IndexOfAscendingIntArray(PayTypes, payTypeId) >= 0)));
            if (ok && ExistBank)
            {
                ok = false;
                if (bankCard != null)
                {
                    foreach (CrmBankCardCodeScope scope in bankCard.BankCardCodeScopeList)
                    {
                        if ((scope.BankId > 0) && (SeekUtils.IndexOfAscendingIntArray(BankIds, scope.BankId) >= 0))
                        {
                            bankCardCodeScope = scope;
                            ok = true;
                            break;
                        }
                    }
                }
            }
            if (ok && ExistBankCard && (bankCard != null))
            {
                ok = false;
                if (bankCard != null)
                {
                    foreach (CrmBankCardCodeScope scope in bankCard.BankCardCodeScopeList)
                    {
                        if ((scope.ScopeId > 0) && (SeekUtils.IndexOfAscendingIntArray(BankCardCodeScopeIds, scope.ScopeId) >= 0))
                        {
                            bankCardCodeScope = scope;
                            ok = true;
                            break;
                        }
                    }
                }
            }
               
            if (ok && ExistVipGroup)
            {
                ok = false;
                if (VipGroups != null)
                {
                    foreach (CrmVipGroup vipGroup in VipGroups)
                    {
                        if (vipGroup.Match(vipCard, saleDate))
                        {
                            ok = true;
                            break;
                        }
                    }
                }
            }
            return ok;
        }

    }

    public class CrmRuleSubBill
    {
        public int Inx = 0;
        public byte[] Period = new byte[48];
        public DateTime BeginTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;
        public int[] ArticleIds = null;
        public CrmRuleValue[] ArticleRuleValues = null;

        public List<CrmRuleBillItem> Items = null;
/*
        public CrmRuleValue FindRuleValue(DateTime saleTime, CrmArticle article)
        {
            //翠微版要改，只有商品条件，取不到规则 2012.2.24
            if (WithinPeriodOfTime(saleTime) && (saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (BeginTime.CompareTo(DateTime.MinValue) > 0) && (EndTime.CompareTo(DateTime.MinValue) > 0))
            {
                if (ArticleIds != null)
                {
                    int inx = SeekUtils.IndexOfAscendingIntArray(ArticleIds, article.ArticleId);
                    if (inx >= 0)
                    {
                        return ArticleRuleValues[inx];
                    }
                }
                if (Items != null)
                {
                    foreach (CrmRuleBillItem ruleItem in Items)
                    {
                        if (ruleItem.Match(article))
                        {
                            return ruleItem.RuleValue;
                        }
                    }
                }
            }
            return null;
        }
 * */
        public CrmRuleValue FindRuleValue(DateTime saleTime, CrmVipCard vipCard, CrmArticle article)
        {
            //翠微版要改，只有商品条件，取不到规则 2012.2.24
            if (WithinPeriodOfTime(saleTime) && (saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (BeginTime.CompareTo(DateTime.MinValue) > 0) && (EndTime.CompareTo(DateTime.MinValue) > 0))
            {
                if (ArticleIds != null)
                {
                    int inx = SeekUtils.IndexOfAscendingIntArray(ArticleIds, article.ArticleId);
                    if (inx >= 0)
                    {
                        return ArticleRuleValues[inx];
                    }
                }
                if (Items != null)
                {
                    foreach (CrmRuleBillItem ruleItem in Items)
                    {
                        if (ruleItem.Match(article, vipCard, saleTime))
                        {
                            return ruleItem.RuleValue;
                        }
                    }
                }
            }
            return null;
        }

        public CrmRuleValue FindRuleValue(DateTime saleTime, CrmVipCard vipCard, CrmArticle article, int payTypeId, CrmBankCardPayment bankCard, out CrmBankCardCodeScope bankCardCodeScope)
        {
            bankCardCodeScope = null;
            if (WithinPeriodOfTime(saleTime) && (saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (BeginTime.CompareTo(DateTime.MinValue) > 0) && (EndTime.CompareTo(DateTime.MinValue) > 0))
            {
                if (ArticleIds != null)
                {
                    int inx = SeekUtils.IndexOfAscendingIntArray(ArticleIds, article.ArticleId);
                    if (inx >= 0)
                    {
                        return ArticleRuleValues[inx];
                    }
                }
                if (Items != null)
                {
                    foreach (CrmRuleBillItem ruleItem in Items)
                    {
                        if (ruleItem.Match(article,vipCard,saleTime, payTypeId, bankCard,out bankCardCodeScope))
                        {
                            return ruleItem.RuleValue;
                        }
                    }
                }
            }
            return null;
        }
        
        private bool WithinPeriodOfTime(DateTime saleTime)
        {
            //int dayOfWork = Convert.ToInt16(saleTime.DayOfWeek) + 1;
            int dayOfWork = 0;
            if (saleTime.DayOfWeek == DayOfWeek.Sunday)
                dayOfWork = 7;
            else
                dayOfWork = Convert.ToInt16(saleTime.DayOfWeek);
            return ((Period[saleTime.Hour * 2 + saleTime.Minute / 30] & (0x1 << dayOfWork)) != 0);
        }
    }

    public class CrmRuleBill
    {
        public string DeptCode = string.Empty;
        public int BillId = 0;
        public int PromId = 0;
        public bool IsPrior = false;
        public DateTime BeginTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;
        public List<CrmRuleSubBill> SubBillList = null;
/*
        public CrmRuleValue FindRuleValue(DateTime saleTime, CrmArticle article)
        {
            if ((saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (SubBillList != null))
            {
                foreach (CrmRuleSubBill subBill in SubBillList)
                {
                    CrmRuleValue ruleValue = subBill.FindRuleValue(saleTime,article);
                    if (ruleValue != null)
                    {
                        ruleValue.RuleBill = this;
                        return ruleValue;
                    }
                }
            }
            return null;
        }
*/
        public CrmRuleValue FindRuleValue(DateTime saleTime, CrmVipCard vipCard, CrmArticle article)
        {
            if ((saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (SubBillList != null))
            {
                foreach (CrmRuleSubBill subBill in SubBillList)
                {
                    CrmRuleValue ruleValue = subBill.FindRuleValue(saleTime,vipCard, article);
                    if (ruleValue != null)
                    {
                        //ruleValue.RuleBill = this;
                        return ruleValue;
                    }
                }
            }
            return null;
        }
        public CrmRuleValue FindRuleValue(DateTime saleTime, CrmVipCard vipCard, CrmArticle article, int payType, CrmBankCardPayment bankCard, out CrmBankCardCodeScope bankCardCodeScope)
        {
            bankCardCodeScope = null;
            if ((saleTime.CompareTo(BeginTime) > 0) && (saleTime.CompareTo(EndTime) < 0) && (SubBillList != null))
            {
                foreach (CrmRuleSubBill subBill in SubBillList)
                {
                    CrmRuleValue ruleValue = subBill.FindRuleValue(saleTime, vipCard, article, payType, bankCard,out bankCardCodeScope);
                    if (ruleValue != null)
                    {
                        //ruleValue.RuleBill = this;
                        return ruleValue;
                    }
                }
            }
            return null;
        }
    }

    public abstract class AbstractRuleBillPool 
    {
	    private List<CrmRuleBill> billList = new List<CrmRuleBill>();
    	
        protected abstract void GetRuleBillDetail(CrmRuleBill bill);
    	
        public CrmRuleBill GetRuleBill(CrmRuleBill bill)
        {
            lock (billList)
            {
                foreach (CrmRuleBill bill2 in billList)
                {
                    if (bill2.BillId == bill.BillId)
                    {
                        return bill2;
                    }
                }
                GetRuleBillDetail(bill);
                billList.Add(bill);
                return bill;
            }
        }    	
    }
}