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
    public class PosCalculator
    {
        public bool IsToCalcOfferCoupon = false;

        public CrmRSaleBillHead BillHead = null;
        public List<CrmPayment> PaymentList = null;
        public List<CrmArticle> ArticleList = null;
        public List<CrmPaymentArticleShare> PaymentArticleShareList = null;
        public DbCommand DbCmd = null;
        
        public string ErrorMessage = string.Empty;

        public List<CrmPromDecMoneyCalcItem> DecMoneyCalcItemList = null;
        public List<CrmPromOfferCouponCalcItem> OfferCouponCalcItemList = null;

        public List<CrmPromPaymentOfferCouponArticle> PaymentOfferCouponArticleList = null;
        public List<CrmPromPaymentOfferCouponCalcItem> PaymentOfferCouponCalcItemList = null;

        public double TotalDecMoney = 0;
        private double TotalSaleMoneyUsed = 0;

        public bool ProcWhenCalcDecMoney()
        {
            if ((ArticleList == null) || (ArticleList.Count == 0) || (PaymentList == null) || (PaymentList.Count == 0))
                return true;

            double totalSaleMoney = 0;
            foreach (CrmArticle article in ArticleList)
            {
                totalSaleMoney = totalSaleMoney + article.SaleMoney;
                article.SaleMoneyNoShare = article.SaleMoney;
                article.SaleMoneySharedJoinDecMoney = 0;
                article.SaleMoneySharedJoinOfferCoupon = 0;
                article.SaleMoneyForDecMoney = article.SaleMoney;
                article.SaleMoneyForOfferCoupon = article.SaleMoney;
            }
            double totalPayMoney = 0;
            foreach (CrmPayment payment in PaymentList)
            {
                payment.IsShared = false;
                totalPayMoney = totalPayMoney + payment.PayMoney;
                payment.PayMoneyNoShare = payment.PayMoney;
            }
            if (!MathUtils.DoubleAEuqalToDoubleB(totalSaleMoney, totalPayMoney))
            {
                ErrorMessage = "销售金额 <> 收款金额，不能分摊收款金额到商品";
                return false;
            }

            bool allPromPayment = true;
            bool allNoPromPayment = true;
            foreach (CrmPayment payment in PaymentList)
            {
                if (payment.JoinPromDecMoney)
                    allNoPromPayment = false;
                else
                    allPromPayment = false;
            }
            if (allNoPromPayment)     //收款方式都不满减，则直接退出
            {
                foreach (CrmArticle article in ArticleList)
                {
                    article.SaleMoneyForDecMoney = 0;
                }
                return true;
            }
            DecMoneyCalcItemList = new List<CrmPromDecMoneyCalcItem>();
            PromCalculator.MakePromDecMoneyCalcItemList(DecMoneyCalcItemList, ArticleList);

            if (DecMoneyCalcItemList.Count == 0)    //商品都不满减，则直接退出
                return true;

            if (allPromPayment)     //收款方式都满减
            {
                PromCalculator.CalcPromDecMoney(DecMoneyCalcItemList, out TotalDecMoney, out TotalSaleMoneyUsed);
                return true;
            }

            //注：计算满减时，需要分摊不参加满减的收款，并保存下来，在结账时再取出
            PaymentArticleShareList = new List<CrmPaymentArticleShare>();

            List<CrmPayment> tempPaymentList = new List<CrmPayment>();
            foreach (CrmPayment payment in PaymentList)
            {
                if ((!payment.JoinPromDecMoney) && (payment.CouponType >= 0))
                {
                    tempPaymentList.Add(payment);
                }
            }

            if (tempPaymentList.Count > 0)
            {
                //检查券的使用限制，去掉没有使用限制的券
                if (!PromCalculator.CheckPayLimitForCouponPaymentList(out ErrorMessage,BillHead.CompanyCode, tempPaymentList, ArticleList, BillHead.ServerBillId, DbCmd))
                    return false;
            }

            if (ArticleList.Count == 1)     //只有一条商品记录时  简化处理
            {
                CrmArticle article = ArticleList[0];
                foreach (CrmPayment payment in PaymentList)
                {
                    if (!payment.JoinPromDecMoney)
                    {
                        CrmPaymentArticleShare share = PromCalculator.AddPaymentArticleShareToList(false, PaymentArticleShareList, article, payment, payment.PayMoney, PromCalculator.CheckCouponPaymentJoinOfferCoupon(article, payment));
                        //payment.PayMoneyNoShare = 0;
                        payment.IsShared = true;
                    }
                }
                PromCalculator.CalcPromDecMoney(DecMoneyCalcItemList, out TotalDecMoney, out TotalSaleMoneyUsed);
                return true;
            }

            //由于有些收款参加满减，有些收款不参加满减，且有些收款参加返券，有些收款不参加返券，为了达到顾客最惠化，采取以下策略分摊收款：
            // 1 先分摊有使用限制的券，以保证这些券能够分摊完
            // 2 分摊其它收款前，先对满减计算项列表和返券计算项列表进行试算和排序，然后把参加满减的收款尽可能分摊到满减更多的商品上，把参加返券的收款尽可能分摊到返券更多的商品上
            // 3 试算和排序是先假设商品都分摊到现金，试算出各计算项的满减额，然后按满减额从小到大排序，根据排序结果，赋值各计算项的商品的参与满减的优先级（数值越大，满减越多）
            // 4 

            double payMoneyDecMoney = 0;    //参加满减的收款额
            double payMoneyOfferCoupon = 0;    //参加返券的收款额
            foreach (CrmPayment payment in PaymentList)
            {
                if (payment.JoinPromDecMoney)
                {
                    payMoneyDecMoney = payMoneyDecMoney + payment.PayMoney;
                }
                else if (payment.JoinPromOfferCoupon)
                {
                    payMoneyOfferCoupon = payMoneyOfferCoupon + payment.PayMoney;
                }
            }

            //对满减计算项列表进行试算
            PromCalculator.CalcPromDecMoneyToSort(DecMoneyCalcItemList, payMoneyDecMoney, out TotalDecMoney, out TotalSaleMoneyUsed);

            if (MathUtils.DoubleAEuqalToDoubleB(TotalDecMoney, 0))  //总满减额=0，则直接退出
                return true;

            //先分摊有使用限制的券（券的收款额正好等于使用限额或可用商品数为1时，简化处理，提前分摊）
            if (tempPaymentList.Count > 0)
            {
                if (!PromCalculator.ShareCouponPaymentList1(out ErrorMessage, tempPaymentList, PaymentArticleShareList))
                    return false;

                allPromPayment = true;
                foreach (CrmPayment payment in PaymentList)
                {
                    if (!payment.IsShared)
                    {
                        if (!payment.JoinPromDecMoney)
                            allPromPayment = false;
                    }
                }
                if (allPromPayment)     //剩下的收款方式都满减，则不用再分摊
                {
                    PromCalculator.CalcPromDecMoney(DecMoneyCalcItemList, out TotalDecMoney, out TotalSaleMoneyUsed);
                    return true;
                }
            }

            //对满减计算项列表进行排序
            PromCalculator.SortPromDecMoneyCalcItemList(DecMoneyCalcItemList);

            allPromPayment = true;
            allNoPromPayment = (!MathUtils.DoubleASmallerThanDoubleB(TotalSaleMoneyUsed, payMoneyDecMoney));
            //TotalSaleMoneyUsed >= payMoneyDecMoney，说明参加满减的收款额只够分摊给满减商品，不会再分摊给返券商品
            foreach (CrmPayment payment in PaymentList)
            {
                if ((!payment.IsShared) && (!payment.JoinPromDecMoney))
                {
                    if (payment.JoinPromOfferCoupon)
                        allNoPromPayment = false;
                    else
                        allPromPayment = false;
                }
            }

            bool existOfferCoupon = false;
            if ((!allPromPayment) && (!allNoPromPayment))   //有些收款参加返券，有些收款不参加返券,在计算满减时也要考虑返券的最大化
            {
                OfferCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                PromCalculator.MakePromOfferCouponCalcItemList(OfferCouponCalcItemList, ArticleList, BillHead.ServerBillId, BillHead.CompanyCode, BillHead.StoreCode, DbCmd);
                if (OfferCouponCalcItemList.Count > 0)
                {
                    PromCalculator.GetPromOfferCouponDataBefore(OfferCouponCalcItemList, BillHead.OfferCouponVipId, DateTimeUtils.GetMyDateIndex(BillHead.OfferCouponDate), 0, false, DbCmd);
                    //注意： 假设参加满减的收款，肯定参加返券，这是跟pos程序配合的（在计算满减时，参加满减的收款缺省都是现金）
                    PromCalculator.CalcPromOfferCouponToSort(OfferCouponCalcItemList, payMoneyOfferCoupon + payMoneyOfferCoupon, out existOfferCoupon);
                    if (existOfferCoupon)
                        PromCalculator.SortPromOfferCouponCalcItemList(OfferCouponCalcItemList);
                }
            }

            if (tempPaymentList.Count > 0)
            {
                //继续分摊有使用限制的券（用券额小于使用限额且可用商品数大于1）
                PromCalculator.ShareCouponPaymentList2(out ErrorMessage, tempPaymentList, PaymentArticleShareList);

                if (ErrorMessage.Length > 0)
                    return false;

                allPromPayment = true;
                foreach (CrmPayment payment in PaymentList)
                {
                    if (!payment.IsShared)
                    {
                        if (!payment.JoinPromDecMoney)
                            allPromPayment = false;
                    }
                }
                if (allPromPayment)     //剩下的收款方式都满减，则不用再分摊
                {
                    PromCalculator.CalcPromDecMoney(DecMoneyCalcItemList, out TotalDecMoney, out TotalSaleMoneyUsed);
                    return true;
                }
            }

            //考虑满减最大化，分摊收款，并计算满减
            double payMoneyNoShare = 0;
            PromCalculator.CalcPromDecMoneyMax(DecMoneyCalcItemList, payMoneyDecMoney, out payMoneyNoShare, out TotalDecMoney, out TotalSaleMoneyUsed);
            if (MathUtils.DoubleAEuqalToDoubleB(TotalDecMoney, 0))  //总满减额=0，则直接退出
                return true;

            payMoneyOfferCoupon = 0;    //参加返券的收款额
            foreach (CrmPayment payment in PaymentList)
            {
                if ((!payment.IsShared) && (!payment.JoinPromDecMoney) && (payment.JoinPromOfferCoupon))
                {
                    payMoneyOfferCoupon = payMoneyOfferCoupon + payment.PayMoney;
                }
            }
            if (existOfferCoupon && (MathUtils.DoubleAGreaterThanDoubleB(payMoneyNoShare + payMoneyOfferCoupon, 0)))
            {
                //考虑返券最大化，分摊收款，并预算返券
                double payMoneyDecMoneyNoShare = 0;
                double payMoneyOfferCouponNoShare = 0;
                PromCalculator.CalcPromOfferCouponMax(OfferCouponCalcItemList, payMoneyNoShare, payMoneyOfferCoupon, out payMoneyDecMoneyNoShare, out payMoneyOfferCouponNoShare);

                if (MathUtils.DoubleASmallerThanDoubleB(payMoneyOfferCouponNoShare, payMoneyOfferCoupon))
                {
                    PromCalculator.ShareArticleMoneyJoinOfferCouponToPayment(ArticleList, PaymentList, PaymentArticleShareList);
                }
            }

            tempPaymentList.Clear();
            foreach (CrmPayment payment in PaymentList)
            {
                if ((!payment.IsShared) && (!payment.JoinPromDecMoney))
                {
                    tempPaymentList.Add(payment);
                }
            }

            if (tempPaymentList.Count == 0)
                return true;

            List<CrmArticle> tempArticleList = new List<CrmArticle>();
            foreach (CrmArticle article in ArticleList)
            {
                article.SaleMoneyNoShare = article.SaleMoneyNoShare - article.SaleMoneySharedJoinDecMoney;
                if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyNoShare, 0))
                {
                    tempArticleList.Add(article);
                }
            }

            return PromCalculator.SharePaymentEqually(out ErrorMessage, tempArticleList, tempPaymentList, PaymentArticleShareList);
        }

        public bool ProcWhenPrepareCheckout()
        {
            if ((ArticleList == null) || (ArticleList.Count == 0) || (PaymentList == null) || (PaymentList.Count == 0))
                return true;

            PaymentArticleShareList = new List<CrmPaymentArticleShare>();

            if (BillHead.BillType != CrmPosData.BillTypeSale)
            {
                foreach (CrmArticle article in ArticleList)
                {
                    article.SaleMoney = -article.SaleMoney;
                }
                foreach (CrmPayment payment in PaymentList)
                {
                    payment.PayMoney = -payment.PayMoney;
                }
            }

            double totalSaleMoney = 0;
            foreach (CrmArticle article in ArticleList)
            {
                totalSaleMoney = totalSaleMoney + article.SaleMoney;
                article.SaleMoneyNoShare = article.SaleMoney;
                article.SaleMoneySharedJoinDecMoney = 0;
                article.SaleMoneySharedJoinOfferCoupon = 0;
                article.SaleMoneyForDecMoney = article.SaleMoney;
                article.SaleMoneyForOfferCoupon = article.SaleMoney;
                article.SaleMoneyForCent = article.SaleMoney;
            }
            double totalPayMoney = 0;
            foreach (CrmPayment payment in PaymentList)
            {
                payment.IsShared = false;
                totalPayMoney = totalPayMoney + payment.PayMoney;
                payment.PayMoneyNoShare = payment.PayMoney;
            }

            if (BillHead.BillType == CrmPosData.BillTypeSale)
            {
                if (!PromCalculator.CheckDecMoneyDataBefore(out ErrorMessage, out TotalDecMoney, BillHead.ServerBillId, ArticleList, PaymentList, PaymentArticleShareList, DbCmd))
                    return false;
            }
            if (!MathUtils.DoubleAEuqalToDoubleB(totalSaleMoney - TotalDecMoney, totalPayMoney))
            {
                ErrorMessage = "销售金额 <> 收款金额，不能分摊收款金额到商品";
                return false;
            }

            List<CrmPayment> tempPaymentList = new List<CrmPayment>();
            foreach (CrmPayment payment in PaymentList)
            {
                if ((!payment.IsShared) && (payment.CouponType >= 0))
                {
                    tempPaymentList.Add(payment);
                }

            }
            
            if (tempPaymentList.Count > 0)
            {
                //检查券的使用限制，去掉没有使用限制的券
                if (!PromCalculator.CheckPayLimitForCouponPaymentList(out ErrorMessage, BillHead.CompanyCode, tempPaymentList, ArticleList, BillHead.ServerBillId, DbCmd))
                    return false;
            }

            if (IsToCalcOfferCoupon)
            {
                OfferCouponCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                PromCalculator.MakePromOfferCouponCalcItemList(OfferCouponCalcItemList, ArticleList, BillHead.ServerBillId, BillHead.CompanyCode,BillHead.StoreCode,DbCmd);
                if (OfferCouponCalcItemList.Count > 0)
                    PromCalculator.GetPromOfferCouponDataBefore(OfferCouponCalcItemList, BillHead.OfferCouponVipId, DateTimeUtils.GetMyDateIndex(BillHead.OfferCouponDate), 0, false, DbCmd);
            }

            if (ArticleList.Count == 1)     //只有一条商品记录时  简化处理
            {
                CrmArticle article = ArticleList[0];
                foreach (CrmPayment payment in PaymentList)
                {
                    if (!payment.IsShared)
                    {
                        CrmPaymentArticleShare share = PromCalculator.AddPaymentArticleShareToList(false, PaymentArticleShareList, article, payment, payment.PayMoney, PromCalculator.CheckCouponPaymentJoinOfferCoupon(article, payment));

                        //payment.PayMoneyNoShare = 0;
                        payment.IsShared = true;
                    }
                }
            }
            else
            {
                tempPaymentList.Clear();
                foreach (CrmPayment payment in PaymentList)
                {
                    if (!payment.IsShared)
                    {
                        tempPaymentList.Add(payment);
                    }

                }
                if (tempPaymentList.Count == 1)
                {
                    CrmPayment payment = PaymentList[0];
                    foreach (CrmArticle article in ArticleList)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyNoShare, 0))
                        {
                            CrmPaymentArticleShare share = PromCalculator.AddPaymentArticleShareToList(false, PaymentArticleShareList, article, payment, article.SaleMoneyNoShare, PromCalculator.CheckCouponPaymentJoinOfferCoupon(article, payment));
                            //payment.PayMoneyNoShare -= share.ShareMoney;
                            article.SaleMoneyNoShare = 0;
                        }
                    }
                }
                else
                {
                    bool isToMaxOfferCoupon = false;
                    double payMoneyOfferCoupon = 0;
                    if (IsToCalcOfferCoupon)
                    {
                        bool allPromPayment = true;
                        bool allNoPromPayment = true;
                        foreach (CrmPayment payment in PaymentList)
                        {
                            if (payment.JoinPromOfferCoupon)
                            {
                                allNoPromPayment = false;
                                payMoneyOfferCoupon = payMoneyOfferCoupon + payment.PayMoney;
                            }
                            else
                                allPromPayment = false;
                        }

                        isToMaxOfferCoupon = (!allNoPromPayment) && (!allPromPayment) && (OfferCouponCalcItemList.Count > 0);  //有些收款返券，有些不返券，且有商品返券，则在分摊时要考虑使返券最大化
                        if (isToMaxOfferCoupon)
                        {
                            PromCalculator.CalcPromOfferCouponToSort(OfferCouponCalcItemList, payMoneyOfferCoupon, out isToMaxOfferCoupon);
                            if (isToMaxOfferCoupon)
                                PromCalculator.SortPromOfferCouponCalcItemList(OfferCouponCalcItemList);
                        }
                    }
                    tempPaymentList.Clear();
                    foreach (CrmPayment payment in PaymentList)
                    {
                        if ((!payment.IsShared) && (payment.CouponType >= 0))
                        {
                            tempPaymentList.Add(payment);
                        }

                    }

                    //先分摊有使用限制的券（券的收款额正好等于使用限额或可用商品数为1时，简化处理，提前分摊）
                    if (tempPaymentList.Count > 0)
                    {
                        if (!PromCalculator.ShareCouponPaymentList1(out ErrorMessage, tempPaymentList, PaymentArticleShareList))
                            return false;
                    }

                    if (tempPaymentList.Count > 0)
                    {
                        //继续分摊有使用限制的券（用券额小于使用限额且可用商品数大于1）
                        if (!PromCalculator.ShareCouponPaymentList2(out ErrorMessage, tempPaymentList, PaymentArticleShareList))
                            return false;
                    }

                    if (isToMaxOfferCoupon)
                    {
                        payMoneyOfferCoupon = 0;    //参加返券的收款额
                        foreach (CrmPayment payment in PaymentList)
                        {
                            if ((!payment.IsShared) && (payment.JoinPromOfferCoupon))
                            {
                                payMoneyOfferCoupon = payMoneyOfferCoupon + payment.PayMoney;
                            }
                        }
                        //考虑返券最大化，分摊收款，并预算返券
                        double payMoneyDecMoneyNoShare = 0;
                        double payMoneyOfferCouponNoShare = 0;
                        PromCalculator.CalcPromOfferCouponMax(OfferCouponCalcItemList, 0, payMoneyOfferCoupon, out payMoneyDecMoneyNoShare, out payMoneyOfferCouponNoShare);

                        if (MathUtils.DoubleASmallerThanDoubleB(payMoneyOfferCouponNoShare, payMoneyOfferCoupon))
                        {
                            PromCalculator.ShareArticleMoneyJoinOfferCouponToPayment(ArticleList, PaymentList, PaymentArticleShareList);
                        }
                    }

                    tempPaymentList.Clear();
                    foreach (CrmPayment payment in PaymentList)
                    {
                        if (!payment.IsShared)
                        {
                            tempPaymentList.Add(payment);
                        }
                    }

                    List<CrmArticle> tempArticleList = new List<CrmArticle>();
                    foreach (CrmArticle article in ArticleList)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyNoShare, 0))
                        {
                            tempArticleList.Add(article);
                        }
                    }

                    if (!PromCalculator.SharePaymentEqually(out ErrorMessage, tempArticleList, tempPaymentList, PaymentArticleShareList))
                        return false;
                }   //if (tempPaymentList.Count > 1)
            }   //if (ArticleList.Count > 1)

            if (BillHead.BillType != CrmPosData.BillTypeSale)
            {
                foreach (CrmArticle article in ArticleList)
                {
                    article.SaleMoney = -article.SaleMoney;
                    article.SaleMoneyForCent = -article.SaleMoneyForCent;
                    article.SaleMoneyForOfferCoupon = -article.SaleMoneyForOfferCoupon;
                }
                foreach (CrmPayment payment in PaymentList)
                {
                    payment.PayMoney = -payment.PayMoney;
                }
                foreach (CrmPaymentArticleShare share in PaymentArticleShareList)
                {
                    share.ShareMoney = -share.ShareMoney;
                }
            }

            if (IsToCalcOfferCoupon)
            {
                if (OfferCouponCalcItemList.Count > 0)
                    PromCalculator.CalcPromOfferCoupon(OfferCouponCalcItemList);

                PaymentOfferCouponArticleList = new List<CrmPromPaymentOfferCouponArticle>();
                PaymentOfferCouponCalcItemList = new List<CrmPromPaymentOfferCouponCalcItem>();
                CrmVipCard vipCard = new CrmVipCard();
                vipCard.CardId = BillHead.VipId;    //还没取会员卡类型，后面可能用到这个条件

                bool existPromOfferCoupon = false;
                PromRuleSearcher.LookupPromOfferCouponRuleOfPayment(out existPromOfferCoupon,DbCmd,0, vipCard, BillHead.CompanyCode, BillHead.SaleTime, PaymentArticleShareList, PaymentOfferCouponArticleList);
                if (existPromOfferCoupon)
                {
                    PromCalculator.CalcPromPaymentOfferCoupon(PaymentOfferCouponArticleList, PaymentOfferCouponCalcItemList, DbCmd);
                }
            }

            return true;
        }

    }
}
