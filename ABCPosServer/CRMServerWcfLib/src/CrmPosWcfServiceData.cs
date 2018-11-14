using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections;

namespace ChangYi.Crm.Server.Wcf
{

    [DataContract]
    public class VipCard
    {
        int cardId = 0;
        
        [DataMember(IsRequired = true)]
        public int CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }

        string cardCode = string.Empty;
        [DataMember(IsRequired = true)]
        public string CardCode
        {
            get { return cardCode; }
            set { cardCode = value; }
        }

        string vipName = string.Empty;

        [DataMember(IsRequired = true)]
        public string VipName
        {
            get { return vipName; }
            set { vipName = value; }
        }
        int cardTypeId = 0;

        [DataMember(IsRequired = true)]
        public int CardTypeId
        {
            get { return cardTypeId; }
            set { cardTypeId = value; }
        }

        string cardTypeName = string.Empty;

        [DataMember(IsRequired = true)]
        public string CardTypeName
        {
            get { return cardTypeName; }
            set { cardTypeName = value; }
        }
        bool canCent = false;

        [DataMember(IsRequired = true)]
        public bool CanCent 
        {
            get { return canCent; }
            set { canCent = value; }
        }
        bool canDisc = false;

        [DataMember(IsRequired = true)]
        public bool CanDisc
        {
            get { return canDisc; }
            set { canDisc = value; }
        }

        bool canReturn = false;
        [DataMember(IsRequired = true)]
        public bool CanReturn
        {
            get { return canReturn; }
            set { canReturn = value; }
        }
        bool canOwnCoupon = false;

        [DataMember(IsRequired = true)]
        public bool CanOwnCoupon
        {
            get { return canOwnCoupon; }
            set { canOwnCoupon = value; }
        }
        double validCent = 0;

        [DataMember(IsRequired = true)]
        public double ValidCent
        {
            get { return validCent; }
            set { validCent = value; }
        }
        double yearCent = 0;

        [DataMember(IsRequired = true)]
        public double YearCent
        {
            get { return yearCent; }
            set { yearCent = value; }
        }
        double stageCent = 0;

        [DataMember(IsRequired = true)]
        public double StageCent
        {
            get { return stageCent; }
            set { stageCent = value; }
        }
        string hello = string.Empty;

        [DataMember(IsRequired = true)]
        public string Hello
        {
            get { return hello; }
            set { hello = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CardId=").Append(CardId);
            sb.Append(",CardCode=").Append(CardCode);
            sb.Append(",VipName=").Append(VipName);
            sb.Append(",CardTypeId=").Append(CardTypeId);
            sb.Append(",CardTypeName=").Append(CardTypeName);
            sb.Append(",CanCent=").Append(CanCent);
            sb.Append(",CanOwnCoupon=").Append(CanOwnCoupon);
            sb.Append(",CanDisc=").Append(CanDisc);
            sb.Append(",CanReturn=").Append(CanReturn);
            sb.Append(",ValidCent=").Append(ValidCent);
            sb.Append(",YearCent=").Append(YearCent);
            sb.Append(",StageCent=").Append(StageCent);
            sb.Append(",Hello=").Append(Hello);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class VipInfoUpdated
    {
        int cardId = 0;

        [DataMember(IsRequired = true)]
        public int CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }

        string mobile = string.Empty;

        [DataMember(IsRequired = true)]
        public string Mobile
        {
            get { return mobile; }
            set { mobile = value; }
        }

        string address = string.Empty;

        [DataMember(IsRequired = true)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string email = string.Empty;

        [DataMember(IsRequired = true)]
        public string EMail
        {
            get { return email; }
            set { email = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CardId=").Append(CardId);
            sb.Append(",Mobile=").Append(Mobile);
            sb.Append(",Address=").Append(Address);
            sb.Append(",EMail=").Append(EMail);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class CashCard
    {
        int cardId = 0;

        [DataMember(IsRequired = true)]
        public int CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }

        string cardCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string CardCode
        {
            get { return cardCode; }
            set { cardCode = value; }
        }
        int cardTypeId = 0;

        [DataMember(IsRequired = true)]
        public int CardTypeId
        {
            get { return cardTypeId; }
            set { cardTypeId = value; }
        }

        double balance = 0;

        [DataMember(IsRequired = true)]
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CardId=").Append(CardId);
            sb.Append(",CardCode=").Append(CardCode);
            sb.Append(",CardTypeId=").Append(CardTypeId);
            sb.Append(",Balance=").Append(Balance);
            sb.Append(" )");
        }
    }


    [DataContract]
    public class CashCardPayment
    {
        int cardId = 0;

        [DataMember(IsRequired = true)]
        public int CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }

        double payMoney = 0;
        [DataMember(IsRequired = true)]
        public double PayMoney
        {
            get { return payMoney; }
            set { payMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CardId=").Append(CardId);
            sb.Append(",PayMoney=").Append(PayMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class Coupon
    {
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }

        string couponTypeName = string.Empty;

        [DataMember(IsRequired = true)]
        public string CouponTypeName
        {
            get { return couponTypeName; }
            set { couponTypeName = value; }
        }
        //string validDate = string.Empty;

        //[DataMember(IsRequired = true)]
        //public string ValidDate
        //{
        //    get { return validDate; }
        //    set { validDate = value; }
        //}

        double balance = 0;

        [DataMember(IsRequired = true)]
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CouponType=").Append(CouponType);
            sb.Append(",CouponTypeName=").Append(CouponTypeName);
            sb.Append(",Balance=").Append(Balance);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class CouponPayLimit
    {
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }

        double limitMoney = 0;
        [DataMember(IsRequired = true)]
        public double LimitMoney
        {
            get { return limitMoney; }
            set { limitMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CouponType=").Append(CouponType);
            sb.Append(",LimitMoney=").Append(LimitMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class CouponPayment
    {
        int vipId = 0;

        [DataMember(IsRequired = true)]
        public int VipId
        {
            get { return vipId; }
            set { vipId = value; }
        }
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }
        //string validDateStr = string.Empty;
        //[DataMember(IsRequired = true)]
        //public string ValidDateStr
        //{
        //    get { return validDateStr; }
        //    set { validDateStr = value; }
        //}

        double payMoney = 0;
        [DataMember(IsRequired = true)]
        public double PayMoney
        {
            get { return payMoney; }
            set { payMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( VipId=").Append(VipId);
            sb.Append(",CouponType=").Append(CouponType);
            sb.Append(",PayMoney=").Append(PayMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class CouponPayback
    {
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }
        string couponTypeName = string.Empty;

        [DataMember(IsRequired = true)]
        public string CouponTypeName
        {
            get { return couponTypeName; }
            set { couponTypeName = value; }
        }
        double payMoney = 0;
        [DataMember(IsRequired = true)]
        public double PayMoney
        {
            get { return payMoney; }
            set { payMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CouponType=").Append(CouponType);
            sb.Append(",PayMoney=").Append(PayMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class DeptArticleCode
    {
        string deptCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        string articleCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string ArticleCode
        {
            get { return articleCode; }
            set { articleCode = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( DeptCode=").Append(DeptCode);
            sb.Append(",ArticleCode=").Append(ArticleCode);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class ArticleVipDisc
    {
        double discRate  = 1;

        [DataMember(IsRequired = true)]
        public double DiscRate
        {
            get { return discRate; }
            set { discRate = value; }
        }
  
        double multiDiscRate  = 0;

        [DataMember(IsRequired = true)]
        public double MultiDiscRate
        {
            get { return multiDiscRate; }
            set { multiDiscRate = value; }
        }

        int precisionType  = 0;
        [DataMember(IsRequired = true)]
        public int PrecisionType
        {
            get { return precisionType; }
            set { precisionType = value; }
        }
  
        int discCombinationType = 0;
        [DataMember(IsRequired = true)]
        public int DiscCombinationType
        {
            get { return discCombinationType; }
            set { discCombinationType = value; }
        }
 
        int discBillId  = 0;
        [DataMember(IsRequired = true)]
        public int DiscBillId
        {
            get { return discBillId; }
            set { discBillId = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( DiscRate=").Append(DiscRate);
            sb.Append(",MultiDiscRate=").Append(MultiDiscRate);
            sb.Append(",PrecisionType=").Append(PrecisionType);
            sb.Append(",DiscCombinationType=").Append(DiscCombinationType);
            sb.Append(",DiscBillId=").Append(DiscBillId);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillHead
    {
        string storeCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string StoreCode
        {
            get { return storeCode; }
            set { storeCode = value; }
        }
        string posId = string.Empty;

        [DataMember(IsRequired = true)]
        public string PosId
        {
            get { return posId; }
            set { posId = value; }
        }
        int billId = 0;

        [DataMember(IsRequired = true)]
        public int BillId
        {
            get { return billId; }
            set { billId = value; }
        }

        int billType = 0;

        [DataMember(IsRequired = true)]
        public int BillType
        {
            get { return billType; }
            set { billType = value; }
        }
        int vipId = 0;

        [DataMember(IsRequired = true)]
        public int VipId
        {
            get { return vipId; }
            set { vipId = value; }
        }
        string saleTime = string.Empty;
        [DataMember(IsRequired = true)]
        public string SaleTime
        {
            get { return saleTime; }
            set { saleTime = value; }
        }
        string accountDate = string.Empty;
        [DataMember(IsRequired = true)]
        public string AccountDate
        {
            get { return accountDate; }
            set { accountDate = value; }
        }
        string cashier = string.Empty;
        [DataMember(IsRequired = true)]
        public string Cashier
        {
            get { return cashier; }
            set { cashier = value; }
        }
        string originalPosId = string.Empty;

        [DataMember(IsRequired = true)]
        public string OriginalPosId
        {
            get { return originalPosId; }
            set { originalPosId = value; }
        }
        int originalBillId = 0;

        [DataMember(IsRequired = true)]
        public int OriginalBillId
        {
            get { return originalBillId; }
            set { originalBillId = value; }
        }
 
        public void Log(StringBuilder sb)
        {
            sb.Append("( StoreCode=").Append(StoreCode);
            sb.Append(",PosId=").Append(PosId);
            sb.Append(",BillId=").Append(BillId);
            sb.Append(",BillType=").Append(BillType);
            sb.Append(",VipId=").Append(VipId);
            sb.Append(",SaleTime=").Append(SaleTime);
            sb.Append(",AccountDate=").Append(AccountDate);
            sb.Append(",Cashier=").Append(Cashier);
            sb.Append(",OriginalPosId=").Append(OriginalPosId);
            sb.Append(",OriginalBillId=").Append(OriginalBillId);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillArticle
    {
        int inx = 0;

        [DataMember(IsRequired = true)]
        public int Inx
        {
            get { return inx; }
            set { inx = value; }
        }
        string deptCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string DeptCode
        {
            get { return deptCode; }
            set { deptCode = value; }
        }
        string articleCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string ArticleCode
        {
            get { return articleCode; }
            set { articleCode = value; }
        }
        public int ArticleId = 0;

        //int articleId = 0;

        //[DataMember(IsRequired = true)]
        //public int ArticleId
        //{
        //    get { return articleId; }
        //    set { articleId = value; }
        //}

        double saleNum = 0;
        [DataMember(IsRequired = true)]
        public double SaleNum
        {
            get { return saleNum; }
            set { saleNum = value; }
        }

        double saleMoney = 0;
        [DataMember(IsRequired = true)]
        public double SaleMoney
        {
            get { return saleMoney; }
            set { saleMoney = value; }
        }
        double discMoney = 0;
        [DataMember(IsRequired = true)]
        public double DiscMoney
        {
            get { return discMoney; }
            set { discMoney = value; }
        }
        double vipDiscMoney = 0;
        [DataMember(IsRequired = true)]
        public double VipDiscMoney
        {
            get { return vipDiscMoney; }
            set { vipDiscMoney = value; }
        }
        double vipDiscRate = 0;
        [DataMember(IsRequired = true)]
        public double VipDiscRate
        {
            get { return vipDiscRate; }
            set { vipDiscRate = value; }
        }
        int vipDiscBillId = 0;
        [DataMember(IsRequired = true)]
        public int VipDiscBillId
        {
            get { return vipDiscBillId; }
            set { vipDiscBillId = value; }
        }
        bool isNoCent = false;
        [DataMember(IsRequired = true)]
        public bool IsNoCent
        {
            get { return isNoCent; }
            set { isNoCent = value; }
        }
        bool isNoProm = false;
        [DataMember(IsRequired = true)]
        public bool IsNoProm
        {
            get { return isNoProm; }
            set { isNoProm = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( Inx=").Append(Inx);
            sb.Append(",DeptCode=").Append(DeptCode);
            sb.Append(",ArticleCode=").Append(ArticleCode);
            sb.Append(",ArticleId=").Append(ArticleId);

            sb.Append(",SaleNum=").Append(SaleNum);
            sb.Append(",SaleMoney=").Append(SaleMoney);
            sb.Append(",DiscMoney=").Append(DiscMoney);
            sb.Append(",VipDiscMoney=").Append(VipDiscMoney);
            sb.Append(",VipDiscRate=").Append(VipDiscRate);
            sb.Append(",VipDiscBillId=").Append(VipDiscBillId);
            sb.Append(",IsNoCent=").Append(IsNoCent);
            sb.Append(",IsNoProm=").Append(IsNoProm);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillPayment
    {
        string payTypeCode = string.Empty;

        [DataMember(IsRequired = true)]
        public string PayTypeCode
        {
            get { return payTypeCode; }
            set { payTypeCode = value; }
        }

        double payMoney = 0;
        [DataMember(IsRequired = true)]
        public double PayMoney
        {
            get { return payMoney; }
            set { payMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( PayTypeCode=").Append(PayTypeCode);
            sb.Append(",PayMoney=").Append(PayMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillArticleCoupon
    {
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }
        int articleInx = 0;

        [DataMember(IsRequired = true)]
        public int ArticleInx
        {
            get { return articleInx; }
            set { articleInx = value; }
        }
        double sharedMoney = 0;
        [DataMember(IsRequired = true)]
        public double SharedMoney
        {
            get { return sharedMoney; }
            set { sharedMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CouponType=").Append(CouponType);
            sb.Append(",ArticleInx=").Append(ArticleInx);
            sb.Append(",SharedMoney=").Append(SharedMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillArticleCent
    {
        int articleInx = 0;

        [DataMember(IsRequired = true)]
        public int ArticleInx
        {
            get { return articleInx; }
            set { articleInx = value; }
        }

        double cent = 0;
        [DataMember(IsRequired = true)]
        public double Cent
        {
            get { return cent; }
            set { cent = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( ArticleInx=").Append(ArticleInx);
            sb.Append(",Cent=").Append(Cent);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillArticlePromFlag
    {
        int articleInx = 0;

        [DataMember(IsRequired = true)]
        public int ArticleInx
        {
            get { return articleInx; }
            set { articleInx = value; }
        }

        bool joinPromCent = false;
        [DataMember(IsRequired = true)]
        public bool JoinPromCent
        {
            get { return joinPromCent; }
            set { joinPromCent = value; }
        }

        bool joinOfferCoupon = false;
        [DataMember(IsRequired = true)]
        public bool JoinOfferCoupon
        {
            get { return joinOfferCoupon; }
            set { joinOfferCoupon = value; }
        }

        bool joinDecMoney = false;
        [DataMember(IsRequired = true)]
        public bool JoinDecMoney
        {
            get { return joinDecMoney; }
            set { joinDecMoney = value; }
        }
        public void Log(StringBuilder sb)
        {
            sb.Append("( ArticleInx=").Append(ArticleInx);
            sb.Append(",JoinPromCent=").Append(JoinPromCent);
            sb.Append(",JoinOfferCoupon=").Append(JoinOfferCoupon);
            sb.Append(",JoinDecMoney=").Append(JoinDecMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class RSaleBillArticleDecMoney
    {
        int articleInx = 0;

        [DataMember(IsRequired = true)]
        public int ArticleInx
        {
            get { return articleInx; }
            set { articleInx = value; }
        }

        double decMoney = 0;
        [DataMember(IsRequired = true)]
        public double DecMoney
        {
            get { return decMoney; }
            set { decMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( ArticleInx=").Append(ArticleInx);
            sb.Append(",DecMoney=").Append(DecMoney);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class OfferCoupon
    {
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }

        string couponTypeName = string.Empty;

        [DataMember(IsRequired = true)]
        public string CouponTypeName
        {
            get { return couponTypeName; }
            set { couponTypeName = value; }
        }
        string validDate = string.Empty;

        [DataMember(IsRequired = true)]
        public string ValidDate
        {
            get { return validDate; }
            set { validDate = value; }
        }

        double offerMoney = 0;

        [DataMember(IsRequired = true)]
        public double OfferMoney
        {
            get { return offerMoney; }
            set { offerMoney = value; }
        }

        double balance = 0;

        [DataMember(IsRequired = true)]
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CouponType=").Append(CouponType);
            sb.Append(",CouponTypeName=").Append(CouponTypeName);
            sb.Append(",ValidDate=").Append(ValidDate);
            sb.Append(",GainedMoney=").Append(OfferMoney);
            sb.Append(",Balance=").Append(Balance);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class OfferBackCoupon
    {
        int couponType = 0;

        [DataMember(IsRequired = true)]
        public int CouponType
        {
            get { return couponType; }
            set { couponType = value; }
        }

        string couponTypeName = string.Empty;

        [DataMember(IsRequired = true)]
        public string CouponTypeName
        {
            get { return couponTypeName; }
            set { couponTypeName = value; }
        }
        string validDate = string.Empty;

        [DataMember(IsRequired = true)]
        public string ValidDate
        {
            get { return validDate; }
            set { validDate = value; }
        }

        double offerMoney = 0;

        [DataMember(IsRequired = true)]
        public double OfferMoney
        {
            get { return offerMoney; }
            set { offerMoney = value; }
        }

        double balance = 0;

        [DataMember(IsRequired = true)]
        public double Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        double difference = 0;

        [DataMember(IsRequired = true)]
        public double Difference
        {
            get { return difference; }
            set { difference = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( CouponType=").Append(CouponType);
            sb.Append(",CouponTypeName=").Append(CouponTypeName);
            sb.Append(",ValidDate=").Append(ValidDate);
            sb.Append(",GainedMoney=").Append(OfferMoney);
            sb.Append(",Balance=").Append(Balance);
            sb.Append(",Difference=").Append(Difference);
            sb.Append(" )");
        }
    }

    [DataContract]
    public class SaleMoneyLeftWhenPromCalc
    {

        string addupTypeDesc = string.Empty;

        [DataMember(IsRequired = true)]
        public string AddupTypeDesc
        {
            get { return addupTypeDesc; }
            set { addupTypeDesc = value; }
        }
        string promotionName = string.Empty;

        [DataMember(IsRequired = true)]
        public string PromotionName
        {
            get { return promotionName; }
            set { promotionName = value; }
        }
        string couponTypeName = string.Empty;

        [DataMember(IsRequired = true)]
        public string CouponTypeName
        {
            get { return couponTypeName; }
            set { couponTypeName = value; }
        }

        string ruleName = string.Empty;

        [DataMember(IsRequired = true)]
        public string RuleName
        {
            get { return ruleName; }
            set { ruleName = value; }
        }

        double saleMoney = 0;

        [DataMember(IsRequired = true)]
        public double SaleMoney
        {
            get { return saleMoney; }
            set { saleMoney = value; }
        }

        public void Log(StringBuilder sb)
        {
            sb.Append("( AddupTypeDesc=").Append(AddupTypeDesc);
            sb.Append(",PromotionName=").Append(PromotionName);
            sb.Append(",CouponTypeName=").Append(CouponTypeName);
            sb.Append(",RuleName=").Append(RuleName);
            sb.Append(",SaleMoney=").Append(SaleMoney);
            sb.Append(" )");
        }
    }
}