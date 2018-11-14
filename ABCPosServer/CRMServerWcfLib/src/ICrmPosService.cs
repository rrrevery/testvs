using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ChangYi.Crm.Server.Wcf
{
    [ServiceContract]
    public interface IPOSService
    {
        [OperationContract]
        bool GetVipCard(out string msg, out VipCard vipCard, int condType, string condValue, string cardCodeToCheck, string cardVerifyToCheck);
        
        [OperationContract]
        bool UpdateVipInfo(out string msg, VipInfoUpdated vipInfo);

        [OperationContract]
        bool UpdateVipCent(out string msg, int vipId, double updateCent, int updateType, string storeCode, string posId, int billId);

        [OperationContract]
        bool GetArticleVipDisc(out string msg, out ArticleVipDisc[] discs,int vipID, int vipType, string storeCode, DeptArticleCode[] articles);

        [OperationContract]
        bool GetCashCard(out string msg, out CashCard cashCard, int condType, string condValue, string cardCodeToCheck, string verifyCode, string password, string storeCode);

        [OperationContract]
        bool GetVipCoupon(out string msg, out int vipID, out string vipCode, out Coupon[] coupons, int condType, string condValue, string cardCodeToCheck, string verifyCode, string storeCode, bool requireValidDate);
        
        [OperationContract]
        bool GetVipCouponToPay(out string msg, out int vipID, out string vipCode, out Coupon[] coupons, out CouponPayLimit[] payLimits, int condType, string condValue, string cardCodeToCheck, string verifyCode, string storeCode, int serverBillID);

        [OperationContract]
        bool GetVipCouponPayLimit(out string msg, out CouponPayLimit[] payLimits, int vipType, int serverBillID, int[] couponTypes);

        [OperationContract]
        bool PrepareTransCashCardPayment(out string msg, out int transID, int serverBillID, CashCardPayment[] payments);

        [OperationContract]
        bool PrepareTransCashCardPayment2(out string msg, out int transID, string storeCode, string posID, int billID, string cashier, DateTime accountDate, CashCardPayment[] payments);

        [OperationContract]
        bool ConfirmTransCashCardPayment(out string msg, int transID, int serverBillID, double transMoney);

        [OperationContract]
        bool CancelTransCashCardPayment(out string msg, int transID, int serverBillID, double transMoney);

        [OperationContract]
        bool PrepareTransCouponPayment(out string msg, out int transID, int serverBillID, CouponPayment[] payments);

        [OperationContract]
        bool PrepareTransCouponPayment2(out string msg, out int transID, string storeCode, string posID, int billID, string cashier, DateTime accountDate, CouponPayment[] payments);

        [OperationContract]
        bool ConfirmTransCouponPayment(out string msg, int transID, int serverBillID, double transMoney);

        [OperationContract]
        bool CancelTransCouponPayment(out string msg, int transID, int serverBillID, double transMoney);

        [OperationContract]
        bool SaveRSaleBillArticles(out string msg, out int serverBillID, out double decMoney, out RSaleBillArticleDecMoney[] articleDecMoneys, out RSaleBillArticlePromFlag[] articlePromFlags, RSaleBillHead billHead, RSaleBillArticle[] billArticles);

        [OperationContract]
        bool SaveRSaleBackBillArticles(out string msg, out int serverBillID, out OfferBackCoupon[] offerBackCoupons, out CouponPayback[] paybackCoupons, RSaleBillHead billHead, RSaleBillArticle[] billArticles);

        [OperationContract]
        bool CalcRSaleBillDecMoney(out string msg, out double decMoney, out RSaleBillArticleDecMoney[] articleDecMoneys, int serverBillID, RSaleBillPayment[] payments);

        [OperationContract]
        bool PrepareCheckOutRSaleBill(out string msg, out double billCent, out RSaleBillArticleCent[] articleCents, out RSaleBillArticleCoupon[] articleCoupons, int serverBillID, RSaleBillPayment[] payments, bool couponPaid);

        [OperationContract]
        bool CheckOutRSaleBill(out string msg, out double billCent, out double vipCent, out OfferCoupon[] offerCoupons, out SaleMoneyLeftWhenPromCalc[] leftSaleMoneys, int serverBillID);


    }

}
