using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChangYi.Pub;
using ChangYi.Crm.Rule;

namespace ChangYi.Crm.Server
{
    public class CrmStoreInfo
    {
        public string StoreCode = string.Empty;
        public string Company = string.Empty;
        public int StoreId = 0;
    }

    public class CrmAreaInfo
    {
        public int AreaId = 0;
        public string AreaName = string.Empty;
        public string ServerIp = string.Empty;
        public int ServerPort = 0;
        public string ServiceUrl = string.Empty;
    }

    public class CrmLoginData
    {
        public CrmStoreInfo StoreInfo = new CrmStoreInfo();
        public string PosId = string.Empty;
        public string UserCode = string.Empty;
        public string Password = string.Empty;
        public string ClientSystem = string.Empty;
        public bool ExistPromOfferCoupon = false;
        public bool ExistPromDecMoney = false;
        public int LengthVerifyOfVipCard = 0;
        public int LengthVerifyOfCashCard = 0;
        public bool PayCashCardWithArticle = false;
    }
    public class CrmPosData
    {
        public const int BillTypeSale = 0;
        public const int BillTypeReturn = 1;
        public const int BillTypeExchange = 2;
        public const int BillTypeVoucher2Cash = 3;
        public const int BillTypeVoucherTransfer = 4;

        public const int BillStatusArticlesUploaded = 0;
        public const int BillStatusCheckedOut = 1;
        public const int BillStatusPrepareCheckOut = 2;
        public const int BillStatusCancelCheckOut = 3;

        public const int TransStatusPrepared = 1;
        public const int TransStatusConfirmed = 2;
        public const int TransStatusRolledback = 3;
        public const int TransStatusCanceled = 4;

        public const int TransTypePayment = 1;
        public const int TransTypeTransfer = 2;
        public const int TransTypeOfferBack = 3;
        public const int TransTypeCoupon2Cash = 4;
        public const int TransTypeSaveChange = 5;

        public const int AddupSaleMoneyOfOneBill = 0;
        public const int AddupSaleMoneyOfOneDay = 1;
        public const int AddupSaleMoneyOfOnePromotion = 2;
        public const int AddupSaleMoneyOfOneArticle = 3;
        public const int AddupSaleMoneyOfOneBillOneBrand = 4;
        public const int AddupSaleMoneyOfOneBillOneContract = 5;
        public const int AddupSaleMoneyOfOneBillOneDept = 6;
        public const int AddupSaleMoneyOfOneBillOneSupp = 7;

    }

    public class CrmRSaleBillHead
    {
        public int ServerBillId = 0;
        public string CompanyCode = string.Empty;
        public string StoreCode = string.Empty;
        public int StoreId = 0;
        public int AreaId = 0;
        public string PosId = string.Empty;
        public int BillId = 0;
        public int BillType = 0;
        public int VipId = 0;
        public int OfferCouponVipId = 0;
        public int PayBackCouponVipId = 0;
        public DateTime SaleTime = DateTime.MinValue;
        public DateTime AccountDate = DateTime.MinValue;
        public DateTime OfferCouponDate = DateTime.MinValue;
        public string Cashier = string.Empty;
        public string OriginalPosId = string.Empty;
        public int OriginalBillId = 0;
        public double TotalSaleNum = 0;
        public double TotalSaleMoney = 0;
        public double TotalDiscMoney = 0;
        public double TotalVipDiscMoney = 0;
        public double TotalPayMoney = 0;
        public double TotalPayCashCardMoney = 0;
        public double TotalGainedCent = 0;
        public double TotalDecMoney = 0;

        public int OriginalServerBillId = 0;
        public double OriginalTotalSaleMoney = 0;
        public int SaleUserId = 0;
        public int VipType = 0;
        public string VipCode = string.Empty;
        public string OfferCouponVipCode = string.Empty;
        public int IssueCardCompanyId = 0;
        public int CentMultiMode = 0;
        public double CentMultiple = 0;
        public bool ExistSpecialCashCardCentRule = false;
        public double OneCentCashCardPayMoney = 0;  //多少元储值卡支付额积1分
        public double GainedCentCashCardPayMoney = 0;

        public double TotalGainedCentUpgraded = 0;

        public bool IsFromBackupTable = false;

    }

    public class CrmArticle
    {
        public int Inx = 0;
        public int OriginalInx = 0;
        public string DeptCode = string.Empty;
        public string ArticleCode = string.Empty;
        public int ArticleId = 0;
        public double SaleNum = 0;
        public double SaleMoney = 0;
        public double DiscMoney = 0;
        public double VipDiscMoney = 0;
        public double VipDiscRate = 1;
        public int VipDiscBillId = 0;
        public bool IsNoCent = false;
        public bool IsNoProm = false;

        public bool JoinPromCent = false;
        public bool JoinOfferCoupon = false;

        public int VipDiscPrecisionType = 0;
        public double VipMultiDiscRate = 0;
        public int VipDiscCombinationType = 0;

        public string CategoryCode = string.Empty;
        public int BrandId = 0;
        public int ContractId = 0;
        public string SuppCode = string.Empty;

        public double GainedCent = 0;
        public double BaseCent = 0;
        public double CentMultiple = 1;
        public int CentMoneyMultipleRuleId = 0;
        public double CentMoneyMultiple = 1;
        public double SaleMoneyForCent = 0;
        public double CentShareRate = 0;
        public int VipCentBillId = 0;
        public bool CanCentMultiple = false;

        public double GainedCentUpgraded = 0;
        public double BaseCentUpgraded = 0;
        public int CentMultipleUpgraded = 1;
        public int VipCentBillIdUpgraded = 0;

        public double TempMoney = 0;

        public int DecMoneyRuleBillId = 0;
        public int DecMoneyPromId = 0;
        public int DecMoneyAddupSaleMoneyType = 0;
        public int DecMoneyRuleId = 0;
        public double DecMoney = 0;
        public bool DecMoneyIsExpense = false;
        
        public double OfferMultiCouponMoney = 0;
        public double SaleMoneyForOfferCoupon = 0;
        public double SaleMoneyForDecMoney = 0;

        public double SaleMoneyNoShare = 0;
        public double SaleMoneySharedJoinOfferCoupon = 0;
        public double SaleMoneySharedJoinDecMoney = 0;

        public int priDecMoney = 0;
        public int priOfferCoupon = 0;

        public double SaleBackMoney = 0;

        public CrmRuleValue VipCentRuleValue = null;
        public CrmRuleValue VipDiscRuleValue = null;
        public CrmRuleValue DecMoneyRuleValue = null;

        public CrmArticle OriginalArticle = null;

        public List<CrmPayment> SharedPaymentList = new List<CrmPayment>();
        public List<CrmPromOfferCouponCalcItem> PromOfferCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
        public List<CrmCouponPayLimitCalcItem> CouponPayLimitCalcItemList = new List<CrmCouponPayLimitCalcItem>();

        public void CalcVipCent(double billCentMultiple, int billCentMultiMode)
        {
            if (MathUtils.DoubleAEuqalToDoubleB(CentMultiple, 0))
                CentMultiple = 1;
            if (MathUtils.DoubleAEuqalToDoubleB(CentMoneyMultiple, 0))
                CentMoneyMultiple = 1;
            if (CanCentMultiple)
            {
                if (billCentMultiMode == 0) //两个倍数相加
                {
                    GainedCent = SaleMoneyForCent * BaseCent * (billCentMultiple + CentMultiple);
                }
                else if (billCentMultiMode == 2) //两个倍数相乘
                {
                    if (MathUtils.DoubleAEuqalToDoubleB(billCentMultiple, 0))
                        GainedCent = SaleMoneyForCent * BaseCent * CentMultiple;
                    else
                        GainedCent = SaleMoneyForCent * BaseCent * CentMultiple * billCentMultiple;
                }
                else  //两个倍数取最大
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(billCentMultiple, CentMultiple))
                        GainedCent = SaleMoneyForCent * BaseCent * billCentMultiple;
                    else
                        GainedCent = SaleMoneyForCent * BaseCent * CentMultiple;
                }
            }
            else
                GainedCent = SaleMoneyForCent * BaseCent * CentMultiple;
            GainedCent = GainedCent * CentMoneyMultiple;
            GainedCent = Math.Round(GainedCent, 4);
        }
    }

    public class CrmPromCentMoneyMultipleCalcItem
    {
        public int RuleId = 0;
        public double SaleMoney = 0;
        public double Multiple = 0;
        public double GainedCent = 0;
        public bool Ok = false;
        public List<CrmArticle> ArticleList = new List<CrmArticle>();
    }

    public class CrmArticleCoupon
    {
        public int CouponType = -1;
        public int ArticleInx = 0;
        public double SaleMoney = 0;
        public double PayLimitMoney = 0;
        public double PayMoney = 0;
        public bool JoinPromOfferCoupon = false;

        public CrmArticle Article = null;

        public int ArticleId = 0;
        public string DeptCode = string.Empty;
        public int PromId = 0;
        public bool PromIdIsBH = false;
        public int RuleBillId = 0;
        public int RuleId = 0;
    }

    public class CrmBankCardCodeScope
    {
        public int ScopeId = 0;
        public int BankId = 0;
    }

    public class CrmBankCardPayment
    {
        public int Inx = 0;
        public int PayTypeId = 0;
        public string BankCardCode = string.Empty;
        public double PayMoney = 0;
        public List<CrmBankCardCodeScope> BankCardCodeScopeList = new List<CrmBankCardCodeScope>();
    }

    public class CrmPayment
    {
        public string PayTypeCode = string.Empty;
        public string PayTypeName = string.Empty;
        public double PayMoney = 0;
        public bool IsCashCard = false;

        public int PayTypeId = 0;
        public int CouponType = -1;
        public bool JoinCent = false;
        public bool JoinPromOfferCoupon = false;
        public bool JoinPromDecMoney = false;

        public bool IsLimited = false;
        public double TotalLimitMoney = 0;
        public double PayMoneyNoShare = 0;
        public bool IsShared = false;

        public double TempMoney = 0;

        public List<CrmArticleCoupon> LimitArticleList = null;

        public List<CrmBankCardPayment> BankCardList = null;

        public void Assign(CrmPayment other)
        {
            this.PayTypeCode = other.PayTypeCode;
            this.PayMoney = other.PayMoney;
            this.PayTypeId = other.PayTypeId;
            this.CouponType = other.CouponType;
            this.JoinCent = other.JoinCent;
            this.JoinPromOfferCoupon = other.JoinPromOfferCoupon;
            this.JoinPromDecMoney = other.JoinPromDecMoney;
        }
    }

    public class CrmPaymentArticleShare
    {
        public CrmArticle Article = null;
        public CrmPayment Payment = null;
        public int CouponType = -1;
        public int ArticleInx = 0;
        public double ShareMoney = 0;
        public double TempMoney = 0;
        public bool JoinPromOfferCoupon = false;
        public bool SharedWhenCalcDecMoney = false;
    }

    public class CrmVipCard
    {
        public int CardId = 0;
        public string CardCode = string.Empty;
        public string VipName = string.Empty;
        public int CardTypeId = 0;
        public string CardTypeName = string.Empty;
        public bool CanCent = false;
        public bool CanOwnCoupon = false;
        public bool CanDisc = false;
        public int DiscType = 0;
        public bool CanReturn = false;
        public bool bCashCard = false;
        public double ValidCent = 0;
        public double YearCent = 0;
        public double StageCent = 0;
        public string PhoneCode = string.Empty;
        public string Hello = string.Empty;
        public int Status = 0;
        public double CashCardBalance = 0;
        public double CouponBalance = 0;

        public int SexType = -1;
        public DateTime Birthday = DateTime.MinValue;
        public int IdCardType = 0;
        public int JobType = 0;
        public int IssueCardCompanyId = 0;
        public bool BirthdayIsChinese = false;

        public string IdCardCode = string.Empty;
        public string Mobile = string.Empty;
        public string Address = string.Empty;
        public string EMail = string.Empty;
    }

    public class CrmVipInfo
    {
        public int CardId = 0;

        public string Mobile = string.Empty;
        public string Address = string.Empty;
        public string EMail = string.Empty;
    }

    public class CrmCashCard
    {
        public int CardId = 0;
        public string CardCode = string.Empty;
        public int CardTypeId = 0;
        public double Balance = 0;
        public double Bottom = 0;
        public DateTime ValidDate = DateTime.MinValue;
        public bool CanReturn = false;
    }

    public class CrmCashCardPayment
    {
        public int CardId = 0;
        public double PayMoney = 0;
        public bool Recycle = false;
    }

    public class CrmCoupon
    {
        public int CouponType = -1;
        public string CouponTypeName = string.Empty;
        public string PayStoreScope = string.Empty;
        public DateTime ValidDate = DateTime.MinValue;
        public double Balance = 0;
        public bool CanOfferCoupon = false;
        public bool MustCalcLimit = false;
        public bool IsCalcedLimit = false;

        public double OfferBackDifference = 0;

        public int SpecialType = 0;
        public double ExchangeCent = 0;
        public double ExchangeMoney = 0;

        //public int OfferStoreScopeType = 0;
        //public int PayStoreScopeType = 0;
        public bool IsPaperCoupon = false;
        public bool IsCoded = false;
        public int CodeLength = 0;
        public string CodePrefix = string.Empty;
        public string CodeSuffix = string.Empty;
        public string PaperNote1 = string.Empty;
        public string PaperNote2 = string.Empty;
        public string PaperUnit = string.Empty;
    }

    public class CrmCouponPayLimit
    {
        public int CouponType = -1;
        public double LimitMoney = 0;
    }

    public class CrmCouponPayment
    {
        public int VipId = 0;
        public int CouponType = -1;
        public double PayMoney = 0;
        public double Balance = 0;
        public int PromId = 0;
        public DateTime ValidDate = DateTime.MinValue;
        public string StoreScope = string.Empty;
        public int ValidDaysToBack = 0;
        public string CouponTypeName = string.Empty;

        public int SpecialType = 0;
        public double ExchangeCent = 0;
        public double ExchangeMoney = 0;
        public double PayCent = 0;
    }

    public class CrmCodedCouponPayment
    {
        public string CouponCode = string.Empty;
        public int CouponType = -1;
        public double PayMoney = 0;
        public int PromId = 0;
    }

    public class CrmCouponPayLimitCalcItem
    {
        public int CouponType = -1;
        public string CouponTypeName = string.Empty;
        public int PromId = 0;
        public int RuleBillId = 0;
        public int RuleId = 0;
        public bool CanOfferCoupon = false;

        public CrmRuleValue RuleValue = null;

        public double SaleMoney = 0;
        public double LimitMoney = 0;
        public List<CrmCouponPayLimitCalcItem> ArticleCalcItemList = null;
    }

    public class CrmPayAccount
    {
        public int BillId = 0;
        public double PayCashCardMoney = 0;
        public double PayVipCouponMoney = 0;
        public double PayCodedCouponMoney = 0;
    }

    public class CrmArticleOfferCouponMoney
    {
        public CrmArticle Article = null;
        public double OfferMoney = 0;
    }

    public class CrmPromOfferCouponCalcItem
    {
        public int CouponType = -1;
        public int AddupSaleMoneyType = 0;
        public int PromId = 0;
        public bool PromIdIsBH = false;
        public int RuleBillId = 0;
        public int RuleId = 0;
        public string DeptCode = string.Empty;
        public CrmRuleValue RuleValue = null;

        public double SaleMoney = 0;
        public double SaleMoneyUsed = 0;
        public double OfferCouponMoney = 0;
        public double ActualOfferMoney = 0;
        //public double ActualOfferBackCouponMoney = 0;

        public double SaleMoneyBefore = 0;
        public double SaleMoneyUsedBefore = 0;
        public double OfferCouponMoneyBefore = 0;

        public DateTime ValidDate = DateTime.MinValue;

        public bool IsPaperCoupon = false;
        public string OfferStoreScope = string.Empty;
        public string PayStoreScope = string.Empty;

        public int SpecialType = 0;
        public string GiftCode = string.Empty;

        public List<CrmArticle> ArticleList = new List<CrmArticle>();
        public List<CrmArticleOfferCouponMoney> ArticleOfferCouponMoneyList = null;
    }

    public class CrmBankCardPaymentArticleShare
    {
        public CrmPaymentArticleShare PaymentArticleShare = null;
        //public CrmArticle Article = null;
        //public CrmPayment Payment = null;
        public CrmBankCardPayment BankCardPayment = null;
        public double PayMoney = 0;
    }

    public class CrmPromPaymentOfferCouponArticle
    {
        public CrmArticle Article = null;
        public CrmPayment Payment = null;
        public CrmBankCardPayment BankCardPayment = null;
        public int BankId = 0;
        public double PayMoney = 0;
        public int CouponType = -1;
        public int PromId = 0;
        public int RuleBillId = 0;
        public int RuleId = 0;

        public CrmRuleValue RuleValue = null;
    }

    public class CrmPromPaymentOfferCouponCalcItem
    {
        public int CouponType = -1;
        public int PromId = 0;
        public int RuleBillId = 0;
        public int RuleId = 0;
        public int PayTypeId = 0;
        public int BankId = 0;
        public int BankCardInx = 0;
        public string BankCardCode = string.Empty;

        public double PayMoney = 0;
        public double PayMoneyUsed = 0;
        public double OfferCouponMoney = 0;
        public double ActualOfferMoney = 0;
        
        public bool IsPaperCoupon = false;
        public string OfferStoreScope = string.Empty;

        public int SpecialType = 0;
        public string GiftCode = string.Empty;

        public List<CrmPromPaymentOfferCouponArticle> ArticleList = new List<CrmPromPaymentOfferCouponArticle>();
    }

    public class CrmPromPaymentOfferCouponLimit
    {
        public int PromId = 0;
        public int CouponType = 0;
        public int StoreId = 0;
        public int BankId = 0;
        public double MaxMoneyOnce = 0;
        public double MaxMoneyAllCardPeriod = 0;
        public double MaxMoneyAllCardEveryDay = 0;
        public double MaxMoneyEveryCardPeriod = 0;
        public int MaxTimesEveryCardPeriod = 0;
        public double MaxMoneyEveryCardEveryDay = 0;
        public int MaxTimesEveryCardEveryDay = 0;
    }

    public class CrmPromOfferCouponLimit
    {
        public int PromId = 0;
        public int CouponType = 0;
        public int StoreId = 0;
        public double MaxMoneyPeriod = 0;
    }

    public class CrmPromOfferGiftLimit
    {
        public int PromId = 0;
        public string GiftCode = string.Empty;
        public int StoreId = 0;
        public int MaxNumEveryDay = 0;
    }

    public class CrmPromOfferCoupon
    {
        public int CouponType = -1;
        public string CouponTypeName = string.Empty;
        public CrmCoupon Coupon = null;
        public DateTime ValidDate = DateTime.MinValue;
        public double OfferMoney = 0;
        public double ActualOfferMoney = 0; 

        public double Balance = 0;
        public double OfferBackDifference = 0;
        //public double ActualOfferBackMoney = 0;

        public int PromId = 0;
        public bool PromIdIsBH = false;
        public string PayStoreScope = string.Empty;
        public string CouponCode = string.Empty;
        public int BankId = 0;
        public int BankCardInx = 0;
        public string BankCardCode = string.Empty;
        public bool IsPaperCoupon = false;
        public bool IsFromPayment = false;
        public int SpecialType = 0;
    }

    public class CrmPromBankCardOfferCoupon
    {
        public int PromId = 0;
        public int CouponType = 0;
        public int BankId = 0;
        public string BankCardCode = string.Empty;

        public CrmPromPaymentOfferCouponLimit OfferCouponLimit = null;
        public bool IsExistYHKFQJE_CARD = false;
        public bool IsExistYHKFQJE_CARD_DAY = false;
        public double OfferMoney = 0;
        public double ActualOfferMoney = 0; 
        public int OfferTimes = 0;
        public int ActualOfferTimes = 0;
        public List<CrmPromOfferCoupon> Items = new List<CrmPromOfferCoupon>();
    }

    public class CrmSaleMoneyLeftWhenPromCalc
    {

        public string AddupTypeDesc = string.Empty;
        public string PromotionName = string.Empty;
        public string CouponTypeName = string.Empty;
        public string RuleName = string.Empty;
        public double SaleMoney = 0;
    }

    public class CrmPromDecMoneyCalcItem
    {
        public int AddupSaleMoneyType = 0;
        public int PromId = 0;
        public int RuleId = 0;

        public string DeptCode = string.Empty;
        public int BrandId = 0;
        public int ContractId = 0;
        public string SuppCode = string.Empty;

        public double SaleMoney = 0;
        public double SaleMoneyUsed = 0;
        public double DecMoney = 0;
        public List<CrmArticle> ArticleList = new List<CrmArticle>();
    }

    //public class CrmBankCardProm
    //{
    //    public int PromId = 0;
    //    public string PromDesc = string.Empty;
    //    public int BillId = 0;
    //    public int SubBillInx = 0;
    //    public DateTime BeginTime = DateTime.MinValue;
    //    public DateTime EndTime = DateTime.MinValue;
    //    public string PeriodDesc = string.Empty;
    //    public int BillId = 0;
    //}

    public class CrmPromBillItemDesc
    {
        public int BillType = 0;    //1 积分，2 折扣 ，3 返券，4用券，5满减
        public int PromId = 0;
        public string PromDesc = string.Empty;
        public int BillId = 0;
        public int SubBillInx = 0;
        public int ItemInx = 0;
        public DateTime BeginTime = DateTime.MinValue;
        public DateTime EndTime = DateTime.MinValue;
        public string PeriodDesc = string.Empty;
        public int RuleId = 0;
        public string RuleDesc = string.Empty;
        public int CouponType = -1;
        public string CouponName = string.Empty;
        public int SpecialType = 0;
    }

    public class CrmWXMessage
    {
        public string OpenId = string.Empty;
        public int MemberId = 0;
        public string CardCode = string.Empty;
        public DateTime TimeShopping = DateTime.MinValue;
        public int StoreId = 0;
        public string StoreName = string.Empty;
        public int ServerBillId = 0;
        public double Sale = 0;
        public double Balance = 0;
        public double Cent = 0;
        public double ValidCent = 0;
        public bool isSearch = false;//--是否在前置机已查询好数据
    }
}
