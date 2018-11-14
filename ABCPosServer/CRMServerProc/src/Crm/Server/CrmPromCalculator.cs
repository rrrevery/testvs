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
    public class PromCalculator
    {

        public static void CalcVipCent(out double billCent, double billCentMultiple, int billCentMultiMode, List<CrmArticle> articleList, List<CrmPromCentMoneyMultipleCalcItem> centMoneyMultipleCalcItemList)
        {
            billCent = 0;
            bool isFound = false;
            foreach (CrmArticle article in articleList)
            {
                article.GainedCent = 0;
                if ((!article.IsNoCent) && (article.CentMoneyMultipleRuleId > 0))
                {
                    isFound = false;
                    foreach (CrmPromCentMoneyMultipleCalcItem calcItem in centMoneyMultipleCalcItemList)
                    {
                        if (calcItem.RuleId == article.CentMoneyMultipleRuleId)
                        {
                            isFound = true;
                            calcItem.ArticleList.Add(article);
                            calcItem.SaleMoney += article.SaleMoneyForCent;
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        CrmPromCentMoneyMultipleCalcItem calcItem = new CrmPromCentMoneyMultipleCalcItem();
                        centMoneyMultipleCalcItemList.Add(calcItem);
                        calcItem.RuleId = article.CentMoneyMultipleRuleId;
                        calcItem.ArticleList.Add(article);
                        calcItem.SaleMoney = article.SaleMoneyForCent;
                    }
                }
            }

            if (centMoneyMultipleCalcItemList.Count > 0)
            {
                foreach (CrmPromCentMoneyMultipleCalcItem calcItem in centMoneyMultipleCalcItemList)
                {
                    CrmCentMoneyMultiRule centMultiRule = PromRuleSearcher.CentMoneyMultiRulePool.GetRule(calcItem.RuleId);
                    calcItem.Ok = centMultiRule.LookupMultiple(calcItem.SaleMoney, out calcItem.Multiple);
                    if (calcItem.Ok)
                    {
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            article.CentMoneyMultiple = calcItem.Multiple;
                        }
                    }
                }
            }
            foreach (CrmArticle article in articleList)
            {
                if (!article.IsNoCent)
                {
                    article.CalcVipCent(billCentMultiple, billCentMultiMode);
                    billCent = billCent + article.GainedCent;
                }
            }

            if (centMoneyMultipleCalcItemList.Count > 0)
            {
                foreach (CrmPromCentMoneyMultipleCalcItem calcItem in centMoneyMultipleCalcItemList)
                {
                    CrmCentMoneyMultiRule centMultiRule = PromRuleSearcher.CentMoneyMultiRulePool.GetRule(calcItem.RuleId);
                    if (calcItem.Ok)
                    {
                        calcItem.GainedCent = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.GainedCent += article.GainedCent;
                        }
                    }
                }
            } 
        }

        public static void CalcVipCentUpgraded(out double billCent, List<CrmArticle> articleList)
        {
            billCent = 0;
            foreach (CrmArticle article in articleList)
            {
                if (!article.IsNoCent)
                {
                    article.GainedCentUpgraded = Math.Round(article.SaleMoney * article.BaseCentUpgraded * article.CentMultipleUpgraded, 4);
                    billCent = billCent + article.GainedCentUpgraded;
                }
            }
        }

        public static bool CheckDecMoneyDataBefore(out string errorMessage, out double totalDecMoney, int serverBillId, List<CrmArticle> articleList, List<CrmPayment> paymentList, List<CrmPaymentArticleShare> shareList, DbCommand cmd)
        {
            errorMessage = string.Empty;
            totalDecMoney = 0;
            bool isFound = false;
            StringBuilder sql = new StringBuilder();
            sql.Append("select INX,MBJZJE from HYK_XFJL_SP_MBJZ where MBJZJE > 0 and XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int articleInx = DbUtils.GetInt(reader, 0);
                double decMoney = DbUtils.GetDouble(reader, 1);
                isFound = false;
                foreach (CrmArticle article in articleList)
                {
                    if (article.Inx == articleInx)
                    {
                        isFound = true;
                        article.DecMoney = decMoney;
                        article.SaleMoney = article.SaleMoney - decMoney;
                        article.SaleMoneyNoShare = article.SaleMoney;
                        article.SaleMoneyForOfferCoupon = article.SaleMoney;
                        article.SaleMoneyForCent = article.SaleMoney;
                        totalDecMoney = totalDecMoney + decMoney;
                        break;
                    }
                }
                if (!isFound)
                {
                    errorMessage = "前期计算的满减数据与现在的数据不一致";
                    reader.Close();
                    return false;
                }
            }
            reader.Close();

            if (MathUtils.DoubleAGreaterThanDoubleB(totalDecMoney, 0))
            {
                sql.Length = 0;
                sql.Append("select INX,SHZFFSID,JE,BJ_FQ from HYK_XFJL_SP_ZFFS_MBJZ where XFJLID = ").Append(serverBillId);
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int articleInx = DbUtils.GetInt(reader, 0);
                    int crmPayType = DbUtils.GetInt(reader, 1);
                    CrmArticle article = null;
                    foreach (CrmArticle article2 in articleList)
                    {
                        if (article2.Inx == articleInx)
                        {
                            article = article2;
                            break;
                        }
                    }
                    CrmPayment payment = null;
                    foreach (CrmPayment payment2 in paymentList)
                    {
                        if (payment2.PayTypeId == crmPayType)
                        {
                            payment = payment2;
                            break;
                        }
                    }
                    if ((article == null) || (payment == null))
                    {
                        errorMessage = "前期计算的满减数据与现在的数据不一致";
                        reader.Close();
                        return false;
                    }

                    CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, shareList, article, payment,DbUtils.GetDouble(reader, 2),DbUtils.GetBool(reader, 3));
                    //payment.PayMoneyNoShare = payment.PayMoneyNoShare - share.ShareMoney;
                    share.SharedWhenCalcDecMoney = true;
                    payment.IsShared = true;
                }
                reader.Close();

                foreach (CrmPayment payment in paymentList)
                {
                    if ((payment.IsShared) && (!MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare,0)))
                    {
                        errorMessage = "前期计算的满减数据与现在的数据不一致";
                        return false;
                    }
                }
            }
            return true;
        }

        public static void MakePromDecMoneyCalcItemList(List<CrmPromDecMoneyCalcItem> calcItemList, List<CrmArticle> articleList)
        {
            foreach (CrmArticle article in articleList)
            {
                article.DecMoney = 0;
                if (article.DecMoneyRuleId > 0)
                {
                    bool isFound = false;
                    foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
                    {
                        if ((calcItem.RuleId == article.DecMoneyRuleId) && (calcItem.AddupSaleMoneyType == article.DecMoneyAddupSaleMoneyType) && (calcItem.PromId == article.DecMoneyPromId))
                        {
                            if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneBrand)    //单笔单品牌
                            {
                                if ((calcItem.BrandId == article.BrandId) && (calcItem.DeptCode.Equals(article.DeptCode)))
                                {
                                    isFound = true;
                                    calcItem.ArticleList.Add(article);
                                    calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                                    break;
                                }
                            }
                            else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneContract)   //单笔单合同
                            {
                                if ((calcItem.ContractId == article.ContractId) && (calcItem.DeptCode.Equals(article.DeptCode)))
                                {
                                    isFound = true;
                                    calcItem.ArticleList.Add(article);
                                    calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                                    break;
                                }
                            }
                            else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneSupp)   //单笔单供货商
                            {
                                if ((article.SuppCode.Length > 0) && (article.SuppCode.Equals(calcItem.SuppCode)) && (calcItem.DeptCode.Equals(article.DeptCode)))
                                {
                                    isFound = true;
                                    calcItem.ArticleList.Add(article);
                                    calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                                    break;
                                }
                            }
                            else
                            {
                                isFound = true;
                                calcItem.ArticleList.Add(article);
                                calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                                break;
                            }
                        }
                    }

                    if ((!isFound) && ((article.DecMoneyAddupSaleMoneyType != CrmPosData.AddupSaleMoneyOfOneBillOneSupp) || (article.SuppCode.Length > 0)))
                    {
                        CrmPromDecMoneyCalcItem calcItem = new CrmPromDecMoneyCalcItem();
                        calcItemList.Add(calcItem);
                        calcItem.ArticleList.Add(article);
                        calcItem.AddupSaleMoneyType = article.DecMoneyAddupSaleMoneyType;
                        calcItem.PromId = article.DecMoneyPromId;
                        calcItem.RuleId = article.DecMoneyRuleId;
                        calcItem.SaleMoney = article.SaleMoneyForDecMoney;
                        calcItem.SaleMoneyUsed = 0;
                        calcItem.DecMoney = 0;
                        calcItem.ContractId = 0;
                        calcItem.BrandId = 0;
                        calcItem.SuppCode = string.Empty;
                        if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneBrand)    //单笔单品牌
                        {
                            calcItem.BrandId = article.BrandId;
                            calcItem.DeptCode = article.DeptCode;
                        }
                        else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneContract)   //单笔单合同
                        {
                            calcItem.ContractId = article.ContractId;
                            calcItem.DeptCode = article.DeptCode;
                        }
                        else if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneSupp)   //单笔单供货商
                        {
                            calcItem.SuppCode = article.SuppCode;
                            calcItem.DeptCode = article.DeptCode;
                        }
                    }
                }
            }
        }

        public static void SortPromDecMoneyCalcItemList(List<CrmPromDecMoneyCalcItem> calcItemList)
        {
            //按满减额从小到大排序
            if (calcItemList.Count > 1)
            {
                List<CrmPromDecMoneyCalcItem> sortedCalcItemList = new List<CrmPromDecMoneyCalcItem>();
                foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
                {
                    bool isFound = false;
                    for (int i = 0; i < sortedCalcItemList.Count; i++)
                    {
                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.DecMoney, sortedCalcItemList[i].DecMoney))
                        {
                            isFound = true;
                            sortedCalcItemList.Insert(i, calcItem);
                            break;
                        }
                    }
                    if (!isFound)
                        sortedCalcItemList.Add(calcItem);
                }
                calcItemList.Clear();
                for (int i = 0; i < sortedCalcItemList.Count; i++)
                {
                    calcItemList.Add(sortedCalcItemList[i]);
                }
            }
            //赋值商品的满减优先级,分摊有用券限制的优惠券时使用
            for (int i = 0; i < calcItemList.Count; i++)
            {
                CrmPromDecMoneyCalcItem calcItem = calcItemList[i];
                if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.DecMoney, 0))
                {
                    foreach (CrmArticle article in calcItem.ArticleList)
                    {
                        article.priDecMoney = i + 1;
                    }
                }
            }
            foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
            {
                if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle) && MathUtils.DoubleAGreaterThanDoubleB(calcItem.DecMoney, 0)) //每件商品满减
                {
                    //按满减额从小到大排序
                    if (calcItem.ArticleList.Count > 1)
                    {
                        List<CrmArticle> sortedArticleList = new List<CrmArticle>();
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            bool isFound = false;
                            for (int i = 0; i < sortedArticleList.Count; i++)
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(article.DecMoney, sortedArticleList[i].DecMoney))
                                {
                                    isFound = true;
                                    sortedArticleList.Insert(i, article);
                                    break;
                                }
                            }
                            if (!isFound)
                                sortedArticleList.Add(article);
                        }
                        calcItem.ArticleList.Clear();
                        for (int i = 0; i < sortedArticleList.Count; i++)
                        {
                            calcItem.ArticleList.Add(sortedArticleList[i]);
                        }

                    }
                }
            }

        }

        public static void CalcPromDecMoney(List<CrmPromDecMoneyCalcItem> calcItemList, out double totalDecMoney, out double totalSaleMoneyUsed)
        {
            totalDecMoney = 0;
            totalSaleMoneyUsed = 0;
            foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
            {
                calcItem.DecMoney = 0;
                calcItem.SaleMoneyUsed = 0;
                foreach (CrmArticle article in calcItem.ArticleList)
                {
                    article.DecMoney = 0;
                }
            }
            foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
            {
                CrmPromDecMoneyRule decRule = PromRuleSearcher.PromDecMoneyRulePool.GetRule(calcItem.RuleId);
                if (decRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品满减
                    {
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            double saleMoney = 0;
                            int decNum = MathUtils.Truncate(article.SaleNum);
                            if (decNum == 1)
                                saleMoney = article.SaleMoneyForDecMoney;
                            else if (decNum > 1)
                            {
                                saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, 0))
                                    decNum = MathUtils.Truncate(article.SaleMoneyForDecMoney / saleMoney);
                                else
                                    decNum = 0;
                            }
                            if (decNum > 0)
                            {
                                double saleMoneyUsed = 0;
                                decRule.CalcDecMoney(saleMoney, out saleMoneyUsed, out article.DecMoney);
                                if (MathUtils.DoubleAGreaterThanDoubleB(article.DecMoney, 0))
                                {
                                    article.DecMoney = article.DecMoney * decNum;
                                    calcItem.DecMoney = calcItem.DecMoney + article.DecMoney;
                                    calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed + saleMoneyUsed * decNum;
                                }
                            }
                        }
                    }
                    else
                    {
                        //非“每件商品满减”的计算
                        calcItem.SaleMoney = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                        }
                        decRule.CalcDecMoney(calcItem.SaleMoney, out calcItem.SaleMoneyUsed, out calcItem.DecMoney);
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.DecMoney, 0))
                            ShareDecMoneyToArticleList(calcItem.DecMoney, calcItem.ArticleList);
                    }
                    totalDecMoney = totalDecMoney + calcItem.DecMoney;
                    totalSaleMoneyUsed = totalSaleMoneyUsed + calcItem.SaleMoneyUsed;
                }
            }
        }

        public static void CalcPromDecMoneyToSort(List<CrmPromDecMoneyCalcItem> calcItemList, double payMoney, out double totalDecMoney, out double totalSaleMoneyUsed)
        {
            totalDecMoney = 0;
            totalSaleMoneyUsed = 0;

            foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
            {
                calcItem.DecMoney = 0;
                calcItem.SaleMoneyUsed = 0;
                foreach (CrmArticle article in calcItem.ArticleList)
                {
                    article.DecMoney = 0;
                }
            }
            foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
            {
                CrmPromDecMoneyRule decRule = PromRuleSearcher.PromDecMoneyRulePool.GetRule(calcItem.RuleId);
                if (decRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品满减
                    {
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            double saleMoney = 0;
                            int decNum = MathUtils.Truncate(article.SaleNum);
                            if (decNum == 1)
                            {
                                saleMoney = article.SaleMoneyForDecMoney;
                                if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, payMoney))
                                    saleMoney = payMoney;
                            }
                            else if (decNum > 1)
                            {
                                saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, 0))
                                {
                                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForDecMoney, payMoney))
                                        decNum = MathUtils.Truncate(payMoney / saleMoney);
                                    else
                                        decNum = MathUtils.Truncate(article.SaleMoneyForDecMoney / saleMoney);
                                }
                                else
                                    decNum = 0;
                            }
                            if (decNum > 0)
                            {
                                double saleMoneyUsed = 0;
                                decRule.CalcDecMoney(saleMoney, out saleMoneyUsed, out article.DecMoney);
                                if (MathUtils.DoubleAGreaterThanDoubleB(article.DecMoney, 0))
                                {
                                    article.DecMoney = article.DecMoney * decNum;
                                    calcItem.DecMoney = calcItem.DecMoney + article.DecMoney;
                                    calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed + saleMoneyUsed * decNum;
                                }
                            }
                        }
                    }
                    else
                    {
                        //非“每件商品满减”的计算
                        calcItem.SaleMoney = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                        }
                        double saleMoney = calcItem.SaleMoney;
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.SaleMoney, payMoney))
                            saleMoney = payMoney;
                        decRule.CalcDecMoney(saleMoney, out calcItem.SaleMoneyUsed, out calcItem.DecMoney);
                    }
                    totalDecMoney = totalDecMoney + calcItem.DecMoney;
                    totalSaleMoneyUsed = totalSaleMoneyUsed + calcItem.SaleMoneyUsed;
                }
            }
        }

        public static void CalcPromDecMoneyMax(List<CrmPromDecMoneyCalcItem> calcItemList, double payMoney, out double payMoneyNoShare, out double totalDecMoney, out double totalSaleMoneyUsed)
        {
            totalDecMoney = 0;
            totalSaleMoneyUsed = 0;
            
            foreach (CrmPromDecMoneyCalcItem calcItem in calcItemList)
            {
                foreach (CrmArticle article in calcItem.ArticleList)
                {
                    article.DecMoney = 0;
                    article.SaleMoneySharedJoinDecMoney = 0;
                }
            }
            payMoneyNoShare = payMoney;
            for (int i = calcItemList.Count - 1; i >= 0; i--)
            {
                CrmPromDecMoneyCalcItem calcItem = calcItemList[i];
                CrmPromDecMoneyRule decRule = PromRuleSearcher.PromDecMoneyRulePool.GetRule(calcItem.RuleId);
                if (decRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品满减
                    {
                        for (int j = calcItem.ArticleList.Count - 1; j >= 0; j--)
                        {
                            CrmArticle article = calcItem.ArticleList[j];
                            double saleMoney = 0;
                            int decNum = MathUtils.Truncate(article.SaleNum);
                            if (decNum == 1)
                            {
                                saleMoney = article.SaleMoneyForDecMoney;
                                if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, payMoneyNoShare))
                                    saleMoney = payMoneyNoShare;
                            }
                            else if (decNum > 1)
                            {
                                saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, 0))
                                {
                                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForDecMoney, payMoneyNoShare))
                                        decNum = MathUtils.Truncate(payMoneyNoShare / saleMoney);
                                    else
                                        decNum = MathUtils.Truncate(article.SaleMoneyForDecMoney / saleMoney);
                                }
                                else
                                    decNum = 0;
                            }
                            if (decNum > 0)
                            {
                                double saleMoneyUsed = 0;
                                decRule.CalcDecMoney(saleMoney, out saleMoneyUsed, out article.DecMoney);
                                if (MathUtils.DoubleAGreaterThanDoubleB(article.DecMoney, 0))
                                {
                                    article.DecMoney = article.DecMoney * decNum;
                                    article.SaleMoneySharedJoinDecMoney = saleMoneyUsed * decNum;
                                    calcItem.DecMoney = calcItem.DecMoney + article.DecMoney;
                                    calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed + article.SaleMoneySharedJoinDecMoney;
                                    totalDecMoney = totalDecMoney + article.DecMoney;
                                    totalSaleMoneyUsed = totalSaleMoneyUsed + article.SaleMoneySharedJoinDecMoney;

                                    payMoneyNoShare = payMoneyNoShare - article.SaleMoneySharedJoinDecMoney;

                                    if (!MathUtils.DoubleAGreaterThanDoubleB(payMoneyNoShare, 0))
                                        return;
                                }
                            }
                        }
                    }
                    else
                    {
                        //非“每件商品满减”的计算
                        calcItem.SaleMoney = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForDecMoney;
                        }
                        double saleMoney = calcItem.SaleMoney;
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.SaleMoney, payMoneyNoShare))
                            saleMoney = payMoneyNoShare;
                        decRule.CalcDecMoney(saleMoney, out calcItem.SaleMoneyUsed, out calcItem.DecMoney);
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.DecMoney, 0))
                        {
                            SharePayMoneyJoinDecMoneyToArticleList(calcItem.SaleMoneyUsed, calcItem.ArticleList);
                            ShareDecMoneyToArticleList(calcItem.DecMoney, calcItem.ArticleList);

                            totalDecMoney = totalDecMoney + calcItem.DecMoney;
                            totalSaleMoneyUsed = totalSaleMoneyUsed + calcItem.SaleMoneyUsed;

                            payMoneyNoShare = payMoneyNoShare - calcItem.SaleMoneyUsed;
                            if (!MathUtils.DoubleAGreaterThanDoubleB(payMoneyNoShare, 0))
                                return;
                        }
                    }
                }
            }
        }

        public static void MakePromOfferCouponCalcItemList(List<CrmPromOfferCouponCalcItem> calcItemList, List<CrmArticle> articleList, int serverBillId,string companyCode,string storeCode, DbCommand cmd)
        {
            calcItemList.Clear();
            foreach (CrmArticle article in articleList)
            {
                article.PromOfferCouponCalcItemList.Clear();
            }

            bool existPromOfferCoupon = false;
            StringBuilder sql = new StringBuilder();

            sql.Append("select INX,YHQID,CXID,XFLJFQFS,YHQFFGZID from HYK_XFJL_SP_FQ where XFJLID = ").Append(serverBillId);
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int articleInx = DbUtils.GetInt(reader, 0);
                foreach (CrmArticle article in articleList)
                {
                    if (article.Inx == articleInx)
                    {
                        existPromOfferCoupon = true;
                        CrmPromOfferCouponCalcItem calcItemOfArticle = new CrmPromOfferCouponCalcItem();
                        article.PromOfferCouponCalcItemList.Add(calcItemOfArticle);
                        calcItemOfArticle.CouponType = DbUtils.GetInt(reader, 1);
                        calcItemOfArticle.PromId = DbUtils.GetInt(reader, 2);
                        calcItemOfArticle.AddupSaleMoneyType = DbUtils.GetInt(reader, 3);
                        calcItemOfArticle.RuleId = DbUtils.GetInt(reader, 4);
                        break;
                    }
                }
            }
            reader.Close();

            if (!existPromOfferCoupon)
                return;

            foreach (CrmArticle article in articleList)
            {
                foreach (CrmPromOfferCouponCalcItem calcItemOfArticle in article.PromOfferCouponCalcItemList)
                {
                    bool isFound = false;
                    foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
                    {
                        if ((calcItem.CouponType == calcItemOfArticle.CouponType) && (calcItem.RuleId == calcItemOfArticle.RuleId) && (calcItem.AddupSaleMoneyType == calcItemOfArticle.AddupSaleMoneyType) && (calcItem.PromId == calcItemOfArticle.PromId))
                        {
                            if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)    //单笔单柜
                            {
                                if (calcItem.DeptCode.Equals(article.DeptCode))
                                {
                                    isFound = true;
                                    calcItem.ArticleList.Add(article);
                                    calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForOfferCoupon;
                                    break;
                                }
                            }
                            else
                            {
                                isFound = true;
                                calcItem.ArticleList.Add(article);
                                calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForOfferCoupon;
                                break;
                            }
                        }
                    }
                    if (!isFound)
                    {
                        CrmPromOfferCouponCalcItem calcItem = new CrmPromOfferCouponCalcItem();
                        calcItemList.Add(calcItem);
                        calcItem.ArticleList.Add(article);
                        calcItem.CouponType = calcItemOfArticle.CouponType;
                        calcItem.AddupSaleMoneyType = calcItemOfArticle.AddupSaleMoneyType;
                        calcItem.PromId = calcItemOfArticle.PromId;
                        calcItem.RuleBillId = calcItemOfArticle.RuleBillId;
                        calcItem.RuleId = calcItemOfArticle.RuleId;
                        calcItem.SaleMoney = article.SaleMoneyForOfferCoupon;
                        calcItem.SaleMoneyUsed = 0;
                        calcItem.OfferCouponMoney = 0;
                        if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)    //单笔单柜
                            calcItem.DeptCode = article.DeptCode;
                    }
                }
            }
            if (calcItemList.Count > 0)
            {
                sql.Length = 0;
                sql.Append("select YHQID,BJ_DZYHQ,FS_FQMDFW,BJ_TS from YHQDEF ");
                if (calcItemList.Count == 1)
                    sql.Append(" where YHQID = ").Append(calcItemList[0].CouponType);
                else
                {
                    sql.Append(" where YHQID in (").Append(calcItemList[0].CouponType);
                    for (int i = 1; i < calcItemList.Count; i++)
                    {
                        sql.Append(",").Append(calcItemList[i].CouponType);
                    }
                    sql.Append(")");
                }
                cmd.CommandText = sql.ToString();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int couponType = DbUtils.GetInt(reader, 0);
                    bool isPaperCoupon = (!DbUtils.GetBool(reader, 1));
                    int offerStoreScopeType = DbUtils.GetInt(reader, 2);
                    int specialType = DbUtils.GetInt(reader, 3);
                    foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
                    {
                        if (couponType == calcItem.CouponType)
                        {
                            calcItem.IsPaperCoupon = isPaperCoupon;
                            if (offerStoreScopeType == 3)
                                calcItem.OfferStoreScope = storeCode;
                            else
                                calcItem.OfferStoreScope = companyCode;
                            calcItem.SpecialType = specialType;
                            //break;
                        }
                    }
                }
                reader.Close();
            }
        }

        public static void SortPromOfferCouponCalcItemList(List<CrmPromOfferCouponCalcItem> calcItemList)
        {
            //按返券额从小到大排序
            if (calcItemList.Count > 1)
            {
                List<CrmPromOfferCouponCalcItem> sortedCalcItemList = new List<CrmPromOfferCouponCalcItem>();
                foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
                {
                    bool isFound = false;
                    for (int i = 0; i < sortedCalcItemList.Count; i++)
                    {
                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.OfferCouponMoney, sortedCalcItemList[i].OfferCouponMoney))
                        {
                            isFound = true;
                            sortedCalcItemList.Insert(i, calcItem);
                            break;
                        }
                    }
                    if (!isFound)
                        sortedCalcItemList.Add(calcItem);
                }
                calcItemList.Clear();
                for (int i = 0; i < sortedCalcItemList.Count; i++)
                {
                    calcItemList.Add(sortedCalcItemList[i]);
                }
            }
            //赋值商品的返券优先级
            for (int i = 0; i < calcItemList.Count; i++)
            {
                CrmPromOfferCouponCalcItem calcItem = calcItemList[i];
                if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))
                {
                    foreach (CrmArticle article in calcItem.ArticleList)
                    {
                        //注意：一种商品可以返多种券,所以累加
                        article.priOfferCoupon = article.priOfferCoupon + i + 1;
                    }
                }
            }

            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle) && MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0))  //每件商品返券
                {
                    //按返券额从小到大排序
                    if (calcItem.ArticleList.Count > 1)
                    {
                        List<CrmArticle> sortedArticleList = new List<CrmArticle>();
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            bool isFound = false;
                            for (int i = 0; i < sortedArticleList.Count; i++)
                            {
                                if (MathUtils.DoubleASmallerThanDoubleB(article.OfferMultiCouponMoney, sortedArticleList[i].OfferMultiCouponMoney))
                                {
                                    isFound = true;
                                    sortedArticleList.Insert(i, article);
                                    break;
                                }
                            }
                            if (!isFound)
                                sortedArticleList.Add(article);
                        }
                        calcItem.ArticleList.Clear();
                        for (int i = 0; i < sortedArticleList.Count; i++)
                        {
                            calcItem.ArticleList.Add(sortedArticleList[i]);
                        }
                    }
                }
            }
        }

        public static void GetPromOfferCouponDataBefore(List<CrmPromOfferCouponCalcItem> calcItemList, int vipId, int offerCouponDay, int originalServerBillId, bool isFromBackupTable, DbCommand cmd)
        {
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                {
                    if (!calcItem.IsPaperCoupon)
                    {
                        if (vipId > 0)
                        {
                            StringBuilder sql = new StringBuilder();
                            sql.Append("select ZXFJE,SYXFJE,FQJE from HYK_XFLJDFQ where HYID = ").Append(vipId);
                            sql.Append("  and MDFWDM = '").Append(calcItem.OfferStoreScope).Append("'");
                            if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                                sql.Append("  and CXHDBH = ").Append(calcItem.PromId);
                            else
                                sql.Append("  and CXID = ").Append(calcItem.PromId);
                            if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay)   //当日累计返券
                                sql.Append("  and INX_XFRQ = ").Append(offerCouponDay);
                            else
                                sql.Append("  and INX_XFRQ = 0");
                            sql.Append("  and YHQID = ").Append(calcItem.CouponType);
                            sql.Append("  and YHQFFGZID = ").Append(calcItem.RuleId);
                            cmd.CommandText = sql.ToString();
                            DbDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                calcItem.SaleMoneyBefore = DbUtils.GetDouble(reader, 0);
                                calcItem.SaleMoneyUsedBefore = DbUtils.GetDouble(reader, 1);
                                calcItem.OfferCouponMoneyBefore = DbUtils.GetDouble(reader, 2);
                            }
                            reader.Close();
                        }
                    }
                    else
                        calcItem.AddupSaleMoneyType = CrmPosData.AddupSaleMoneyOfOneBill;    //纸券不能累计扣券
                }

                if ((originalServerBillId > 0) && ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBill) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select ZXFJE - THJE as ZXFJE,SYXFJE - SYXFJE_OLD as SYXFJE,FQJE - KQJE as FQJE ");
                    if (isFromBackupTable)
                        sql.Append(" from HYXFJL_FQ ");
                    else
                        sql.Append(" from HYK_XFJL_FQ ");
                    sql.Append(" where XFJLID = ").Append(originalServerBillId);
                    if (CrmServerPlatform.Config.IsUpgradedProject2013 && calcItem.PromIdIsBH)
                        sql.Append("  and CXHDBH = ").Append(calcItem.PromId);
                    else
                        sql.Append("  and CXID = ").Append(calcItem.PromId);
                    sql.Append("   and YHQID = ").Append(calcItem.CouponType);
                    sql.Append("   and YHQFFGZID = ").Append(calcItem.RuleId);
                    sql.Append("   and XFLJFQFS in (").Append(CrmPosData.AddupSaleMoneyOfOneBill).Append(",").Append(CrmPosData.AddupSaleMoneyOfOneBillOneDept).Append(") ");
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept)
                        sql.Append("   and BMDM = '").Append(calcItem.DeptCode).Append("' ");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        calcItem.SaleMoneyBefore = DbUtils.GetDouble(reader, 0);
                        calcItem.SaleMoneyUsedBefore = DbUtils.GetDouble(reader, 1);
                        calcItem.OfferCouponMoneyBefore = DbUtils.GetDouble(reader, 2);
                    }
                    reader.Close();
                }
            }
        }

        public static void CalcPromOfferCoupon(List<CrmPromOfferCouponCalcItem> calcItemList)
        {
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                calcItem.OfferCouponMoney = 0;
                calcItem.SaleMoneyUsed = 0;
                foreach (CrmArticle article in calcItem.ArticleList)
                {
                    article.OfferMultiCouponMoney = 0;
                    if (calcItem.ArticleOfferCouponMoneyList != null)
                        calcItem.ArticleOfferCouponMoneyList.Clear();
                }
            }
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                CrmPromOfferCouponRule offerRule = PromRuleSearcher.PromOfferCouponRulePool.GetRule(calcItem.RuleId);
                if (offerRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品返券
                    {
                        if (calcItem.ArticleOfferCouponMoneyList == null)
                            calcItem.ArticleOfferCouponMoneyList = new List<CrmArticleOfferCouponMoney>();
                        else
                            calcItem.ArticleOfferCouponMoneyList.Clear();
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            double saleMoney = 0;
                            double saleMoneyUsed = 0;
                            double offerMoney = 0;
                            int offerNum = MathUtils.Truncate(article.SaleNum);
                            if ((MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForOfferCoupon, 0)) && (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoney, 0)) && (offerNum > 0))
                            {
                                if (offerNum == 1)
                                    saleMoney = article.SaleMoneyForOfferCoupon;
                                else
                                {
                                    saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                    offerNum = MathUtils.Truncate((article.SaleMoneyForOfferCoupon) / saleMoney);
                                }
                            }
                            if (offerNum > 0)
                            {
                                if ((calcItem.SpecialType == 0) || (calcItem.SpecialType == 3) || (calcItem.SpecialType == 4))
                                    offerRule.CalcOfferCoupon(saleMoney, out saleMoneyUsed, out offerMoney);
                                else if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                                    offerRule.CalcOfferGift(saleMoney, out saleMoneyUsed, out offerMoney, out calcItem.GiftCode);//单品，实现不完全
                                if (MathUtils.DoubleAGreaterThanDoubleB(offerMoney, 0))
                                {
                                    CrmArticleOfferCouponMoney articleOfferMoney = new CrmArticleOfferCouponMoney();
                                    articleOfferMoney.Article = article;
                                    articleOfferMoney.OfferMoney = offerMoney * offerNum;
                                    calcItem.ArticleOfferCouponMoneyList.Add(articleOfferMoney);

                                    calcItem.OfferCouponMoney = calcItem.OfferCouponMoney + articleOfferMoney.OfferMoney;
                                    calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed + saleMoneyUsed * offerNum;
                                }
                            }
                        }
                    }
                    else
                    {
                        calcItem.SaleMoney = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForOfferCoupon;
                        }

                        if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                        {
                            if ((calcItem.SpecialType == 0) || (calcItem.SpecialType == 3) || (calcItem.SpecialType == 3))
                                offerRule.CalcOfferCoupon(calcItem.SaleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                            else if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                                offerRule.CalcOfferGift(calcItem.SaleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney, out calcItem.GiftCode);
                            calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed - calcItem.SaleMoneyUsedBefore;
                            calcItem.OfferCouponMoney = calcItem.OfferCouponMoney - calcItem.OfferCouponMoneyBefore;
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.SaleMoneyUsed, 0))
                                calcItem.SaleMoneyUsed = 0;
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.OfferCouponMoney, 0))
                                calcItem.OfferCouponMoney = 0;
                        }
                        else if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBill) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept))  //单笔或单笔单柜返券
                        {
                            if ((calcItem.SpecialType == 0) || (calcItem.SpecialType == 3) || (calcItem.SpecialType == 4))
                                offerRule.CalcOfferCoupon(calcItem.SaleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                            else if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                                offerRule.CalcOfferGift(calcItem.SaleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney, out calcItem.GiftCode);
                        }
                    }
                }
            }
        }

        public static void CalcPromOfferCouponOneDayOrPromotion(List<CrmPromOfferCouponCalcItem> calcItemList)
        {
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                {
                    CrmPromOfferCouponRule offerRule = PromRuleSearcher.PromOfferCouponRulePool.GetRule(calcItem.RuleId);
                    if (offerRule != null)
                    {
                        if ((calcItem.SpecialType == 0) || (calcItem.SpecialType == 3) || (calcItem.SpecialType == 4))
                            offerRule.CalcOfferCoupon(calcItem.SaleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                        else if ((calcItem.SpecialType == 1) || (calcItem.SpecialType == 2))
                            offerRule.CalcOfferGift(calcItem.SaleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney, out calcItem.GiftCode);
                        calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed - calcItem.SaleMoneyUsedBefore;
                        calcItem.OfferCouponMoney = calcItem.OfferCouponMoney - calcItem.OfferCouponMoneyBefore;
                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.SaleMoneyUsed, 0))
                            calcItem.SaleMoneyUsed = 0;
                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.OfferCouponMoney, 0))
                            calcItem.OfferCouponMoney = 0;
                    }
                }
            }
        }

        public static void CalcPromOfferBackCoupon(List<CrmPromOfferCouponCalcItem> calcItemList, bool noCalcAddupSaleMoneyOfOneArticle)
        {
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                //article.SaleMoneyForOfferCoupon为负，calcItem.SaleMoney为正
                CrmPromOfferCouponRule offerRule = PromRuleSearcher.PromOfferCouponRulePool.GetRule(calcItem.RuleId);
                if (offerRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品返券
                    {
                        if (!noCalcAddupSaleMoneyOfOneArticle)
                        {
                            if (calcItem.ArticleOfferCouponMoneyList == null)
                                calcItem.ArticleOfferCouponMoneyList = new List<CrmArticleOfferCouponMoney>();
                            else
                                calcItem.ArticleOfferCouponMoneyList.Clear();
                            foreach (CrmArticle article in calcItem.ArticleList)
                            {
                                double saleMoney = 0;
                                double saleMoneyUsed = 0;
                                double offerMoney = 0;
                                int offerNum = MathUtils.Truncate(article.SaleNum);
                                if ((MathUtils.DoubleASmallerThanDoubleB(article.SaleMoneyForOfferCoupon, 0)) && (MathUtils.DoubleASmallerThanDoubleB(article.SaleMoney, 0)) && (offerNum < 0))
                                {
                                    if (offerNum == -1)
                                        saleMoney = -article.SaleMoneyForOfferCoupon;
                                    else
                                    {
                                        saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                        offerNum = MathUtils.Truncate((article.SaleMoneyForOfferCoupon) / saleMoney);
                                    }
                                }
                                if (offerNum < 0)
                                {
                                    offerRule.CalcOfferCoupon(saleMoney, out saleMoneyUsed, out offerMoney);
                                    if (MathUtils.DoubleAGreaterThanDoubleB(offerMoney, 0))
                                    {
                                        CrmArticleOfferCouponMoney articleOfferMoney = new CrmArticleOfferCouponMoney();
                                        articleOfferMoney.Article = article;
                                        articleOfferMoney.OfferMoney = offerMoney * offerNum;   //articleOfferMoney.OfferMoney为负
                                        calcItem.ArticleOfferCouponMoneyList.Add(articleOfferMoney);

                                        calcItem.OfferCouponMoney = calcItem.OfferCouponMoney - articleOfferMoney.OfferMoney;   //calcItem.OfferCouponMoney为正
                                        calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed - saleMoneyUsed * offerNum;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        offerRule.CalcOfferCoupon(calcItem.SaleMoneyBefore - calcItem.SaleMoney, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                        calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsedBefore - calcItem.SaleMoneyUsed;
                        calcItem.OfferCouponMoney = calcItem.OfferCouponMoneyBefore - calcItem.OfferCouponMoney;
                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.SaleMoneyUsed, 0))
                            calcItem.SaleMoneyUsed = 0;
                        if (MathUtils.DoubleASmallerThanDoubleB(calcItem.OfferCouponMoney, 0))
                            calcItem.OfferCouponMoney = 0;
                    }
                }
            }
        }

        public static void CalcPromOfferCouponToSort(List<CrmPromOfferCouponCalcItem> calcItemList, double payMoney, out bool existOfferCoupon)
        {
            existOfferCoupon = false;
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                calcItem.OfferCouponMoney = 0;
                calcItem.SaleMoneyUsed = 0;
                foreach (CrmArticle article in calcItem.ArticleList)
                {
                    article.OfferMultiCouponMoney = 0;
                }
            }
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                CrmPromOfferCouponRule offerRule = PromRuleSearcher.PromOfferCouponRulePool.GetRule(calcItem.RuleId);
                if (offerRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品返券
                    {
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            double saleMoney = 0;
                            double saleMoneyUsed = 0;
                            double offerMoney = 0;
                            int offerNum = MathUtils.Truncate(article.SaleNum);
                            if ((MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForOfferCoupon, 0)) && (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoney, 0)) && (offerNum > 0))
                            {
                                if (offerNum == 1)
                                {
                                    saleMoney = article.SaleMoneyForOfferCoupon;
                                    if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, payMoney))
                                        saleMoney = payMoney; 
                                }
                                else
                                {
                                    saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForOfferCoupon, payMoney))
                                        offerNum = MathUtils.Truncate(payMoney / saleMoney);
                                    else
                                        offerNum = MathUtils.Truncate(article.SaleMoneyForOfferCoupon / saleMoney);
                                }
                            }
                            if (offerNum > 0)
                            {
                                offerRule.CalcOfferCoupon(saleMoney, out saleMoneyUsed, out offerMoney);
                                if (MathUtils.DoubleAGreaterThanDoubleB(offerMoney, 0))
                                {
                                    //注意：一种商品可以返多种券
                                    article.OfferMultiCouponMoney = article.OfferMultiCouponMoney + offerMoney * offerNum;
                                    calcItem.OfferCouponMoney = calcItem.OfferCouponMoney + offerMoney * offerNum;
                                    calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed + saleMoneyUsed * offerNum;
                                }
                            }
                        }
                    }
                    else
                    {
                        calcItem.SaleMoney = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForOfferCoupon;
                        }
                        double saleMoney = calcItem.SaleMoney;
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.SaleMoney, payMoney))
                            saleMoney = payMoney;

                        if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                        {
                            offerRule.CalcOfferCoupon(saleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                            calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed - calcItem.SaleMoneyUsedBefore;
                            calcItem.OfferCouponMoney = calcItem.OfferCouponMoney - calcItem.OfferCouponMoneyBefore;
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.SaleMoneyUsed, 0))
                                calcItem.SaleMoneyUsed = 0;
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.OfferCouponMoney, 0))
                                calcItem.OfferCouponMoney = 0;
                        }
                        else if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBill) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept))  //单笔或单笔单柜返券
                        {
                            offerRule.CalcOfferCoupon(saleMoney, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                        }
                    }
                }
            }
        }

        public static void CalcPromOfferCouponMax(List<CrmPromOfferCouponCalcItem> calcItemList, double payMoneyDecMoney, double payMoneyOfferCoupon, out double payMoneyDecMoneyNoShare, out double payMoneyOfferCouponNoShare)
        {
            foreach (CrmPromOfferCouponCalcItem calcItem in calcItemList)
            {
                calcItem.OfferCouponMoney = 0;
                calcItem.SaleMoneyUsed = 0;
                foreach (CrmArticle article in calcItem.ArticleList)
                {
                    article.OfferMultiCouponMoney = 0;
                    article.SaleMoneySharedJoinOfferCoupon = 0;
                }
            }
            payMoneyDecMoneyNoShare = payMoneyDecMoney;
            payMoneyOfferCouponNoShare = payMoneyOfferCoupon;
            for (int i = calcItemList.Count - 1; i >= 0; i--)
            {
                CrmPromOfferCouponCalcItem calcItem = calcItemList[i];
                CrmPromOfferCouponRule offerRule = PromRuleSearcher.PromOfferCouponRulePool.GetRule(calcItem.RuleId);
                if (offerRule != null)
                {
                    if (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneArticle)  //每件商品返券
                    {
                        for (int j = calcItem.ArticleList.Count - 1; j >= 0; j--)
                        {
                            CrmArticle article = calcItem.ArticleList[j];
                            double saleMoney = 0;
                            double saleMoneyUsed = 0;
                            double offerMoney = 0;
                            int offerNum = MathUtils.Truncate(article.SaleNum);
                            
                            if (offerNum == 1)
                            {
                                saleMoney = article.SaleMoneyForOfferCoupon;
                                if (MathUtils.DoubleAGreaterThanDoubleB(saleMoney, payMoneyDecMoneyNoShare + payMoneyOfferCouponNoShare + article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon))
                                    saleMoney = payMoneyDecMoneyNoShare + payMoneyOfferCouponNoShare + article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon;
                            }
                            else
                            {
                                saleMoney = Math.Round(article.SaleMoney / article.SaleNum, 2);
                                if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForOfferCoupon, payMoneyDecMoneyNoShare + payMoneyOfferCouponNoShare + article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon))
                                    offerNum = MathUtils.Truncate((payMoneyDecMoneyNoShare + payMoneyOfferCouponNoShare + article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon) / saleMoney);
                                else
                                    offerNum = MathUtils.Truncate(article.SaleMoneyForOfferCoupon / saleMoney);
                            }
                            
                            if (offerNum > 0)
                            {
                                offerRule.CalcOfferCoupon(saleMoney, out saleMoneyUsed, out offerMoney);
                                if (MathUtils.DoubleAGreaterThanDoubleB(offerMoney, 0))
                                {
                                    //注意：一种商品可以返多种券
                                    article.OfferMultiCouponMoney = article.OfferMultiCouponMoney + offerMoney * offerNum;
                                    calcItem.OfferCouponMoney = calcItem.OfferCouponMoney + offerMoney * offerNum;
                                    calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed + saleMoneyUsed * offerNum;

                                    if (MathUtils.DoubleAGreaterThanDoubleB(saleMoneyUsed * offerNum, article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon))
                                    {
                                        double shareMoney = saleMoneyUsed * offerNum - (article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon);
                                        if (MathUtils.DoubleAGreaterThanDoubleB(payMoneyDecMoneyNoShare, 0))
                                        {
                                            if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, payMoneyDecMoneyNoShare))
                                            {
                                                article.SaleMoneySharedJoinDecMoney = article.SaleMoneySharedJoinDecMoney + payMoneyDecMoneyNoShare;
                                                shareMoney = shareMoney - payMoneyDecMoneyNoShare;
                                                payMoneyDecMoneyNoShare = 0;
                                            }
                                            else
                                            {
                                                article.SaleMoneySharedJoinDecMoney = article.SaleMoneySharedJoinDecMoney + shareMoney;
                                                payMoneyDecMoneyNoShare = payMoneyDecMoneyNoShare - shareMoney;
                                                shareMoney = 0;
                                            }
                                        }
                                        if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0) && MathUtils.DoubleAGreaterThanDoubleB(payMoneyOfferCouponNoShare, 0))
                                        {
                                            if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, payMoneyOfferCouponNoShare))
                                            {
                                                article.SaleMoneySharedJoinOfferCoupon = article.SaleMoneySharedJoinOfferCoupon + payMoneyDecMoneyNoShare;
                                                shareMoney = shareMoney - payMoneyOfferCouponNoShare;
                                                payMoneyOfferCouponNoShare = 0;
                                            }
                                            else
                                            {
                                                article.SaleMoneySharedJoinOfferCoupon = article.SaleMoneySharedJoinOfferCoupon + shareMoney;
                                                payMoneyOfferCouponNoShare = payMoneyOfferCouponNoShare - shareMoney;
                                                shareMoney = 0;
                                            }
                                        }
                                        if ((!MathUtils.DoubleAGreaterThanDoubleB(payMoneyDecMoneyNoShare, 0)) && (!MathUtils.DoubleAGreaterThanDoubleB(payMoneyOfferCouponNoShare, 0)))
                                            return;
                                    }
                                    
                                }
                            }
                        }
                    }
                    else
                    {
                        double saleMoneyShared = 0;
                        calcItem.SaleMoney = 0;
                        foreach (CrmArticle article in calcItem.ArticleList)
                        {
                            calcItem.SaleMoney = calcItem.SaleMoney + article.SaleMoneyForOfferCoupon;
                            saleMoneyShared = saleMoneyShared + article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon;
                        }
                        double saleMoney = calcItem.SaleMoney;
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.SaleMoney, payMoneyDecMoneyNoShare + payMoneyOfferCouponNoShare + saleMoneyShared))
                            saleMoney = payMoneyDecMoneyNoShare + payMoneyOfferCouponNoShare + saleMoneyShared;

                        if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneDay) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOnePromotion))  //当日累计返券或活动期内累计返券
                        {
                            offerRule.CalcOfferCoupon(saleMoney + calcItem.SaleMoneyBefore, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                            calcItem.SaleMoneyUsed = calcItem.SaleMoneyUsed - calcItem.SaleMoneyUsedBefore;
                            calcItem.OfferCouponMoney = calcItem.OfferCouponMoney - calcItem.OfferCouponMoneyBefore;
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.SaleMoneyUsed, 0))
                                calcItem.SaleMoneyUsed = 0;
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.OfferCouponMoney, 0))
                                calcItem.OfferCouponMoney = 0;
                        }
                        else if ((calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBill) || (calcItem.AddupSaleMoneyType == CrmPosData.AddupSaleMoneyOfOneBillOneDept))  //单笔或单笔单柜返券
                        {
                            offerRule.CalcOfferCoupon(saleMoney, out calcItem.SaleMoneyUsed, out calcItem.OfferCouponMoney);
                        }

                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.OfferCouponMoney, 0) && MathUtils.DoubleAGreaterThanDoubleB(calcItem.SaleMoneyUsed, saleMoneyShared))
                        {
                            double shareMoney = calcItem.SaleMoneyUsed - saleMoneyShared;

                            if (MathUtils.DoubleAGreaterThanDoubleB(payMoneyDecMoneyNoShare, 0))
                            {
                                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, payMoneyDecMoneyNoShare))
                                {
                                    SharePayMoneyJoinDecMoneyToArticleList(payMoneyDecMoneyNoShare, calcItem.ArticleList);
                                    shareMoney = shareMoney - payMoneyDecMoneyNoShare;
                                    payMoneyDecMoneyNoShare = 0;
                                }
                                else
                                {
                                    SharePayMoneyJoinDecMoneyToArticleList(shareMoney, calcItem.ArticleList);
                                    payMoneyDecMoneyNoShare = payMoneyDecMoneyNoShare - shareMoney;
                                    shareMoney = 0;
                                }
                            }
                            if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0) && MathUtils.DoubleAGreaterThanDoubleB(payMoneyOfferCouponNoShare, 0))
                            {
                                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, payMoneyOfferCouponNoShare))
                                {
                                    SharePayMoneyJoinOfferCouponToArticleList(payMoneyOfferCouponNoShare,calcItem.ArticleList);
                                    shareMoney = shareMoney - payMoneyOfferCouponNoShare;
                                    payMoneyOfferCouponNoShare = 0;
                                }
                                else
                                {
                                    SharePayMoneyJoinOfferCouponToArticleList(shareMoney,calcItem.ArticleList);
                                    payMoneyOfferCouponNoShare = payMoneyOfferCouponNoShare - shareMoney;
                                    shareMoney = 0;
                                }
                            }
                            if ((!MathUtils.DoubleAGreaterThanDoubleB(payMoneyDecMoneyNoShare, 0)) && (!MathUtils.DoubleAGreaterThanDoubleB(payMoneyOfferCouponNoShare, 0)))
                                return;
                        }
                    }
                }
            }
        }

        //计算支付（主要是银行卡）返券
        public static void CalcPromPaymentOfferCoupon(List<CrmPromPaymentOfferCouponArticle> paymentOfferCouponArticleList,  List<CrmPromPaymentOfferCouponCalcItem> calcItemList, DbCommand cmd)
        {
            calcItemList.Clear();
            foreach (CrmPromPaymentOfferCouponArticle paymentOfferCouponArticle in paymentOfferCouponArticleList)
            {
                if (paymentOfferCouponArticle.RuleId > 0)
                {
                    bool isFound = false;
                    foreach (CrmPromPaymentOfferCouponCalcItem calcItem in calcItemList)
                    {
                        if (paymentOfferCouponArticle.BankCardPayment == null)
                        {
                            if ((calcItem.CouponType == paymentOfferCouponArticle.CouponType) && (calcItem.PromId == paymentOfferCouponArticle.PromId) && (calcItem.RuleId == paymentOfferCouponArticle.RuleId) && (calcItem.PayTypeId == paymentOfferCouponArticle.Payment.PayTypeId))
                            {
                                isFound = true;
                                calcItem.PayMoney += paymentOfferCouponArticle.PayMoney;
                                break;
                            }
                        }
                        else
                        {
                            if ((calcItem.CouponType == paymentOfferCouponArticle.CouponType) && (calcItem.PromId == paymentOfferCouponArticle.PromId) && (calcItem.RuleId == paymentOfferCouponArticle.RuleId) && (calcItem.BankCardInx == paymentOfferCouponArticle.BankCardPayment.Inx))
                            {
                                isFound = true;
                                calcItem.PayMoney += paymentOfferCouponArticle.PayMoney;
                                break;
                            }
                        }
                    }
                    if (!isFound)
                    {
                        CrmPromPaymentOfferCouponCalcItem calcItem = new CrmPromPaymentOfferCouponCalcItem();
                        calcItemList.Add(calcItem);
                        calcItem.CouponType = paymentOfferCouponArticle.CouponType;
                        calcItem.PromId = paymentOfferCouponArticle.PromId;
                        calcItem.RuleId = paymentOfferCouponArticle.RuleId;
                        calcItem.PayTypeId = paymentOfferCouponArticle.Payment.PayTypeId;
                        if (paymentOfferCouponArticle.BankCardPayment != null)
                        {
                            calcItem.BankCardInx = paymentOfferCouponArticle.BankCardPayment.Inx;
                            calcItem.BankId = paymentOfferCouponArticle.BankId;
                            calcItem.BankCardCode = paymentOfferCouponArticle.BankCardPayment.BankCardCode;
                        }
                        else
                        {
                            calcItem.BankCardInx = 0;
                            calcItem.BankId = 0;
                            calcItem.BankCardCode = " ";
                        }
                        calcItem.PayMoney = paymentOfferCouponArticle.PayMoney;
                    }
                }
            }
            foreach (CrmPromPaymentOfferCouponCalcItem calcItem in calcItemList)
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select BJ_TS from YHQDEF where YHQID = ").Append(calcItem.CouponType);
                cmd.CommandText = sql.ToString();
                DbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    calcItem.SpecialType = DbUtils.GetInt(reader, 0);
                reader.Close();
                CrmPromOfferCouponRule offerRule = PromRuleSearcher.PromOfferCouponRulePool.GetRule(calcItem.RuleId);
                if (offerRule != null)
                {
                    if (calcItem.SpecialType == 0)
                        offerRule.CalcOfferCoupon(calcItem.PayMoney, out calcItem.PayMoneyUsed, out calcItem.OfferCouponMoney);
                    else
                        offerRule.CalcOfferGift(calcItem.PayMoney, out calcItem.PayMoneyUsed, out calcItem.OfferCouponMoney,out calcItem.GiftCode);
                }
            }
        }

        public static void SharePayMoneyJoinDecMoneyToArticleList(double payMoney, List<CrmArticle> articleList)
        {
            if (articleList.Count == 1)
            {
                articleList[0].SaleMoneySharedJoinDecMoney = payMoney;
            }
            else
            {
                double totalSaleMoney = 0;
                foreach (CrmArticle article in articleList)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyForDecMoney, article.SaleMoneyNoShare))
                        article.TempMoney = article.SaleMoneyNoShare;
                    else
                        article.TempMoney = article.SaleMoneyForDecMoney;
                    totalSaleMoney = totalSaleMoney + article.TempMoney;
                }

                if (MathUtils.DoubleAGreaterThanDoubleB(totalSaleMoney, payMoney))
                {
                    double temp = payMoney;
                    foreach (CrmArticle article in articleList)
                    {
                        article.SaleMoneySharedJoinDecMoney = Math.Round(payMoney * (article.TempMoney / totalSaleMoney), 2);
                        temp = temp - article.SaleMoneySharedJoinDecMoney;
                    }
                    //存在小数尾差
                    if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                    {
                        foreach (CrmArticle article in articleList)
                        {
                            double temp2 = temp;
                            if (MathUtils.DoubleAGreaterThanDoubleB(temp2, (article.TempMoney - article.SaleMoneySharedJoinDecMoney)))
                                temp2 = (article.TempMoney - article.SaleMoneySharedJoinDecMoney);
                            if (MathUtils.DoubleAGreaterThanDoubleB(temp2, 0))
                            {
                                article.SaleMoneySharedJoinDecMoney = article.SaleMoneySharedJoinDecMoney + temp2;
                                temp = temp - temp2;
                            }
                            if (!MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                                break;
                        }
                    }
                    else if (MathUtils.DoubleASmallerThanDoubleB(temp, 0))
                    {
                        foreach (CrmArticle article in articleList)
                        {
                            if (MathUtils.DoubleASmallerThanDoubleB(article.SaleMoneySharedJoinDecMoney + temp, 0))
                            {
                                temp += article.SaleMoneySharedJoinDecMoney;
                                article.SaleMoneySharedJoinDecMoney = 0;
                            }
                            else
                            {
                                article.SaleMoneySharedJoinDecMoney += temp;
                                temp = 0;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (CrmArticle article in articleList)
                    {
                        article.SaleMoneySharedJoinDecMoney = article.TempMoney;
                    }
                }
            }
        }

        public static void ShareDecMoneyToArticleList(double totalDecMoney, List<CrmArticle> articleList)
        {
            if (articleList.Count == 1)
            {
                articleList[0].DecMoney = totalDecMoney;
            }
            else
            {
                double totalSaleMoney = 0;
                foreach (CrmArticle article in articleList)
                {
                    article.TempMoney = article.SaleMoneyForDecMoney;
                    totalSaleMoney = totalSaleMoney + article.TempMoney;
                }
                if (MathUtils.DoubleAGreaterThanDoubleB(totalSaleMoney, totalDecMoney))
                {
                    double temp = totalDecMoney;
                    foreach (CrmArticle article in articleList)
                    {
                        article.DecMoney = Math.Round(totalDecMoney * (article.TempMoney / totalSaleMoney), 2);
                        temp = temp - article.DecMoney;
                    }
                    if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                    {
                        //存在小数尾差
                        foreach (CrmArticle article in articleList)
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(article.TempMoney, 0))
                            {
                                double temp2 = temp;
                                if (MathUtils.DoubleAGreaterThanDoubleB(temp2, (article.TempMoney - article.DecMoney)))
                                    temp2 = (article.TempMoney - article.DecMoney);
                                if (MathUtils.DoubleAGreaterThanDoubleB(temp2, 0))
                                {
                                    article.DecMoney = article.DecMoney + temp2;
                                    temp = temp - temp2;
                                }
                                if (!MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                                    break;
                            }
                        }
                    }
                    else if (MathUtils.DoubleASmallerThanDoubleB(temp, 0))
                    {
                        foreach (CrmArticle article in articleList)
                        {
                            if (MathUtils.DoubleASmallerThanDoubleB(article.DecMoney + temp, 0))
                            {
                                temp += article.DecMoney;
                                article.DecMoney = 0;
                            }
                            else
                            {
                                article.DecMoney += temp;
                                temp = 0;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (CrmArticle article in articleList)
                    {
                        article.DecMoney = article.TempMoney;
                    }
                }
            }
        }

        public static void SharePayMoneyJoinOfferCouponToArticleList(double payMoney, List<CrmArticle> articleList)
        {
            if (articleList.Count == 1)
            {
                articleList[0].SaleMoneySharedJoinOfferCoupon = articleList[0].SaleMoneySharedJoinOfferCoupon + payMoney;
            }
            else
            {
                double totalSaleMoney = 0;
                foreach (CrmArticle article in articleList)
                {
                    article.TempMoney = article.SaleMoneyNoShare - (article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon);
                    totalSaleMoney = totalSaleMoney + article.TempMoney;
                }
                double temp = payMoney;
                foreach (CrmArticle article in articleList)
                {
                    article.SaleMoneySharedJoinOfferCoupon = article.SaleMoneySharedJoinOfferCoupon + Math.Round(payMoney * (article.TempMoney / totalSaleMoney), 2);
                    temp = temp - article.SaleMoneySharedJoinOfferCoupon;
                }
                if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                {
                    //存在小数计算尾差
                    foreach (CrmArticle article in articleList)
                    {
                        double temp2 = temp;
                        if (MathUtils.DoubleAGreaterThanDoubleB(temp2, (article.SaleMoneyNoShare - (article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon))))
                            temp2 = (article.SaleMoneyNoShare - (article.SaleMoneySharedJoinDecMoney + article.SaleMoneySharedJoinOfferCoupon));
                        if (MathUtils.DoubleAGreaterThanDoubleB(temp2, 0))
                        {
                            article.SaleMoneySharedJoinOfferCoupon = article.SaleMoneySharedJoinOfferCoupon + temp2;
                            temp = temp - temp2;
                        }
                        if (!MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                            break;
                    }
                }
                else if (MathUtils.DoubleASmallerThanDoubleB(temp, 0))
                {
                    foreach (CrmArticle article in articleList)
                    {
                        if (MathUtils.DoubleASmallerThanDoubleB(article.SaleMoneySharedJoinOfferCoupon + temp, 0))
                        {
                            temp += article.SaleMoneySharedJoinOfferCoupon;
                            article.SaleMoneySharedJoinOfferCoupon = 0;
                        }
                        else
                        {
                            article.SaleMoneySharedJoinOfferCoupon += temp;
                            temp = 0;
                            break;
                        }
                    }
                }
            }
        }

        public static void CalculateCouponPayLimit(List<CrmArticle> articleList)
        {
            foreach (CrmArticle article in articleList)
            {
                foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in article.CouponPayLimitCalcItemList)
                {
                    calcItemOfArticle.SaleMoney = article.SaleMoney;
                    calcItemOfArticle.LimitMoney = article.SaleMoney;
                }
            }
            List<CrmCouponPayLimitCalcItem> calcItemList = new List<CrmCouponPayLimitCalcItem>();
            foreach (CrmArticle article in articleList)
            {
                foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in article.CouponPayLimitCalcItemList)
                {
                    bool isFound = false;
                    foreach (CrmCouponPayLimitCalcItem calcItem in calcItemList)
                    {
                        if ((calcItem.CouponType == calcItemOfArticle.CouponType) && (calcItem.RuleId == calcItemOfArticle.RuleId) && (calcItem.PromId == calcItemOfArticle.PromId))
                        {
                            isFound = true;
                            calcItem.ArticleCalcItemList.Add(calcItemOfArticle);
                            calcItem.SaleMoney = calcItem.SaleMoney + calcItemOfArticle.SaleMoney;
                            break;
                        }
                    }
                    if (!isFound)
                    {
                        CrmCouponPayLimitCalcItem calcItem = new CrmCouponPayLimitCalcItem();
                        calcItemList.Add(calcItem);
                        calcItem.ArticleCalcItemList = new List<CrmCouponPayLimitCalcItem>();
                        calcItem.ArticleCalcItemList.Add(calcItemOfArticle);
                        calcItem.CouponType = calcItemOfArticle.CouponType;
                        calcItem.PromId = calcItemOfArticle.PromId;
                        calcItem.RuleBillId = calcItemOfArticle.RuleBillId;
                        calcItem.RuleId = calcItemOfArticle.RuleId;
                        calcItem.SaleMoney = calcItemOfArticle.SaleMoney;
                     }
                }
            }
            if (calcItemList.Count > 0)
            {
                foreach (CrmCouponPayLimitCalcItem calcItem in calcItemList)
                {
                    calcItem.LimitMoney = calcItem.SaleMoney;
                    CrmPromPayCouponRule payRule = PromRuleSearcher.PromPayCouponRulePool.GetRule(calcItem.RuleId);
                    if (payRule != null)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(calcItem.SaleMoney, 0))
                        {
                            payRule.CalcPayLimitMoney(calcItem.SaleMoney, out calcItem.LimitMoney);
                            if (MathUtils.DoubleASmallerThanDoubleB(calcItem.LimitMoney, calcItem.SaleMoney))
                            {
                                double temp = calcItem.LimitMoney;
                                foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in calcItem.ArticleCalcItemList)
                                {
                                    calcItemOfArticle.LimitMoney = Math.Round(calcItem.LimitMoney * (calcItemOfArticle.SaleMoney / calcItem.SaleMoney), 2);
                                    temp = temp - calcItemOfArticle.LimitMoney;
                                }
                                //存在小数尾差
                                if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                                {
                                    foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in calcItem.ArticleCalcItemList)
                                    {
                                        double temp2 = temp;
                                        if (MathUtils.DoubleAGreaterThanDoubleB(temp2, (calcItemOfArticle.SaleMoney - calcItemOfArticle.LimitMoney)))
                                            temp2 = (calcItemOfArticle.SaleMoney - calcItemOfArticle.LimitMoney);
                                        if (MathUtils.DoubleAGreaterThanDoubleB(temp2, 0))
                                        {
                                            calcItemOfArticle.LimitMoney = calcItemOfArticle.LimitMoney + temp2;
                                            temp = temp - temp2;
                                        }
                                        if (!MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                                            break;
                                    }
                                }
                                else if (MathUtils.DoubleASmallerThanDoubleB(temp, 0))
                                {
                                    foreach (CrmCouponPayLimitCalcItem calcItemOfArticle in calcItem.ArticleCalcItemList)
                                    {
                                        if (MathUtils.DoubleASmallerThanDoubleB(calcItemOfArticle.LimitMoney + temp, 0))
                                        {
                                            temp += calcItemOfArticle.LimitMoney;
                                            calcItemOfArticle.LimitMoney = 0;
                                        }
                                        else
                                        {
                                            calcItemOfArticle.LimitMoney += temp;
                                            temp = 0;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static bool CheckPayLimitForCouponPaymentList(out string errorMessage, string companyCode, List<CrmPayment> couponPaymentList, List<CrmArticle> articleList, int serverBillId, DbCommand cmd)
        {
            errorMessage = string.Empty;

            StringBuilder sql = new StringBuilder();

            foreach (CrmPayment payment in couponPaymentList)
            {
                payment.IsLimited = false;
            }
            sql.Append("select YHQID,BJ_SYD from YHQSYSH ");
            sql.Append(" where SHDM = '").Append(companyCode).Append("'");
            if (couponPaymentList.Count == 1)
                sql.Append("  and YHQID = ").Append(couponPaymentList[0].CouponType);
            else
            {
                sql.Append("  and YHQID in (").Append(couponPaymentList[0].CouponType);
                for (int i = 1; i < couponPaymentList.Count; i++)
                {
                    sql.Append(",").Append(couponPaymentList[i].CouponType);
                }
                sql.Append(")");
            }
            sql.Append(" order by YHQID ");
            cmd.CommandText = sql.ToString();
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int couponType = DbUtils.GetInt(reader, 0);
                foreach (CrmPayment payment in couponPaymentList)
                {
                    if (payment.CouponType == couponType)
                    {
                        payment.IsLimited = DbUtils.GetBool(reader, 1);
                        break;
                    }
                }
            }
            reader.Close();

            //去掉没有使用限制的券
            for (int i = couponPaymentList.Count - 1; i >= 0; i--)
            {
                if (!couponPaymentList[i].IsLimited)
                    couponPaymentList.RemoveAt(i);
            }

            if (couponPaymentList.Count == 0)
                return true;

            sql.Length = 0;
            sql.Append("select YHQID,INX,BJ_FQ,XSJE,XSJE_YQ ");
            sql.Append(" from HYK_XFJL_SP_YQJC ");
            sql.Append(" where XFJLID = ").Append(serverBillId);
            if (couponPaymentList.Count == 1)
                sql.Append("  and YHQID = ").Append(couponPaymentList[0].CouponType);
            else
            {
                sql.Append("  and YHQID in (").Append(couponPaymentList[0].CouponType);
                for (int i = 1; i < couponPaymentList.Count; i++)
                {
                    sql.Append(",").Append(couponPaymentList[i].CouponType);
                }
                sql.Append(")");
            }
            sql.Append(" order by YHQID ");
            cmd.CommandText = sql.ToString();
            CrmPayment payment2 = null;
            CrmArticle article2 = null;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int couponType = DbUtils.GetInt(reader, 0);
                int articleInx = DbUtils.GetInt(reader, 1);
                payment2 = null;
                article2 = null;
                foreach (CrmArticle article in articleList)
                {
                    if (articleInx == article.Inx)
                    {
                        article2 = article;
                        break;
                    }
                }
                if (article2 != null)
                {
                    if ((payment2 == null) || (payment2.CouponType != couponType))
                    {
                        foreach (CrmPayment payment in couponPaymentList)
                        {
                            if (payment.CouponType == couponType)
                            {
                                payment2 = payment;
                                break;
                            }
                        }
                    }
                    if ((payment2 != null) && (payment2.CouponType == couponType))
                    {
                        CrmArticleCoupon couponArticle = new CrmArticleCoupon();
                        couponArticle.CouponType = couponType;
                        couponArticle.ArticleInx = articleInx;
                        couponArticle.JoinPromOfferCoupon = DbUtils.GetBool(reader, 2);
                        couponArticle.SaleMoney = DbUtils.GetDouble(reader, 3);
                        couponArticle.PayLimitMoney = DbUtils.GetDouble(reader, 4);
                        couponArticle.Article = article2;
                        if (payment2.LimitArticleList == null)
                            payment2.LimitArticleList = new List<CrmArticleCoupon>();
                        payment2.LimitArticleList.Add(couponArticle);
                    }
                }
            }
            reader.Close();

            foreach (CrmPayment payment in couponPaymentList)
            {
                payment.TotalLimitMoney = 0;
                if (payment.LimitArticleList != null)   //翠微版要改，不是优惠券，但SHZFFS里YHQID不是null，则会报空指针错 2012.2.24
                {
                    foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                    {
                        payment.TotalLimitMoney = payment.TotalLimitMoney + couponArticle.PayLimitMoney;
                    }
                }
                if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoney, payment.TotalLimitMoney))
                {
                    errorMessage = string.Format("{0} 的收款额超过规定额", payment.PayTypeName);
                    return false;
                }
            }

            //判断券有没有使用限制
            foreach (CrmPayment payment in couponPaymentList)
            {
                payment.IsLimited = ((payment.LimitArticleList == null) || (payment.LimitArticleList.Count < articleList.Count));
                if (!payment.IsLimited)
                {
                    foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                    {
                        if (MathUtils.DoubleASmallerThanDoubleB(couponArticle.PayLimitMoney, couponArticle.SaleMoney))
                        {
                            payment.IsLimited = true;
                            break;
                        }
                    }
                }
            }
            //去掉没有使用限制的券
            for (int i = couponPaymentList.Count - 1; i >= 0; i--)
            {
                if (!couponPaymentList[i].IsLimited)
                    couponPaymentList.RemoveAt(i);
            }

            return true;
        }

        public static void ShareArticleMoneyJoinOfferCouponToPayment(List<CrmArticle> articleList, List<CrmPayment> paymentList, List<CrmPaymentArticleShare> shareList)
        {
            List<CrmPayment> tempPaymentList = new List<CrmPayment>();
            foreach (CrmPayment payment in paymentList)
            {
                if ((!payment.IsShared) && (!payment.JoinPromDecMoney) && (payment.JoinPromOfferCoupon))
                {
                    tempPaymentList.Add(payment);
                }
            }
            if (tempPaymentList.Count == 1)
            {
                CrmPayment payment = tempPaymentList[0];
                foreach (CrmArticle article in articleList)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneySharedJoinOfferCoupon, 0))
                    {
                        CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, shareList, article, payment, article.SaleMoneySharedJoinOfferCoupon, CheckCouponPaymentJoinOfferCoupon(article, payment));
                        //payment.PayMoneyNoShare = payment.PayMoneyNoShare - article.SaleMoneySharedJoinOfferCoupon;
                    }
                }
            }
            else if (tempPaymentList.Count > 1)
            {
                double totalPayMoney = 0;
                foreach (CrmPayment payment in tempPaymentList)
                {
                    payment.TempMoney = payment.PayMoneyNoShare;
                    totalPayMoney = totalPayMoney + payment.PayMoneyNoShare;
                }
                List<CrmPaymentArticleShare> tempShareList = new List<CrmPaymentArticleShare>();
                double shareMoney = 0;
                double temp = 0;
                foreach (CrmArticle article in articleList)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneySharedJoinOfferCoupon, 0))
                    {
                        tempShareList.Clear();
                        temp = article.SaleMoneySharedJoinOfferCoupon;
                        foreach (CrmPayment payment in tempPaymentList)
                        {
                            //分摊过程中 article.SaleMoneySharedJoinOfferCoupon不会改变
                            shareMoney = Math.Round(article.SaleMoneySharedJoinOfferCoupon * (payment.TempMoney / totalPayMoney), 2);
                            CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, tempShareList, article, payment, shareMoney, CheckCouponPaymentJoinOfferCoupon(article, payment));
                            //payment.PayMoneyNoShare = payment.PayMoneyNoShare - shareMoney;

                            temp = temp - shareMoney;
                        }
                        if (!MathUtils.DoubleAEuqalToDoubleB(temp, 0))
                        {
                            //存在小数计算尾差
                            if (MathUtils.DoubleAGreaterThanDoubleB(temp, 0))
                            {
                                foreach (CrmPaymentArticleShare share in tempShareList)
                                {
                                    shareMoney = temp;
                                    if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, share.Payment.PayMoneyNoShare))
                                        shareMoney = share.Payment.PayMoneyNoShare;
                                    if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                                    {
                                        UpdatePaymentArticleShareMoney(share, shareMoney);
                                        //share.Payment.PayMoneyNoShare = share.Payment.PayMoneyNoShare - shareMoney;
                                        temp = temp - shareMoney;
                                    }
                                    if (MathUtils.DoubleAEuqalToDoubleB(temp, 0))
                                        break;
                                }
                            }
                            else if (tempShareList.Count > 0)
                            {
                                foreach (CrmPaymentArticleShare share in tempShareList)
                                {
                                    shareMoney = temp;
                                    if (MathUtils.DoubleASmallerThanDoubleB(shareMoney + share.Payment.PayMoneyNoShare, 0))
                                        shareMoney = - share.Payment.PayMoneyNoShare;
                                    if (MathUtils.DoubleASmallerThanDoubleB(shareMoney, 0))
                                    {
                                        UpdatePaymentArticleShareMoney(share, shareMoney);
                                        temp = temp - shareMoney;
                                    }
                                    if (MathUtils.DoubleAEuqalToDoubleB(temp, 0))
                                        break;
                                }
                            }
                        }
                        shareList.AddRange(tempShareList);
                    }
                }
            }
            foreach (CrmPayment payment in tempPaymentList)
            {
                payment.IsShared = MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0);
            }
        }

        public static bool ShareCouponPaymentList1(out string errorMessage, List<CrmPayment> couponPaymentList, List<CrmPaymentArticleShare> shareList)
        {
            errorMessage = string.Empty;

            foreach (CrmPayment payment in couponPaymentList)
            {
                //收款额=规定额或可用商品数为1时，简化处理，提前分摊
                if ((payment.LimitArticleList != null) && (MathUtils.DoubleAEuqalToDoubleB(payment.PayMoney, payment.TotalLimitMoney) || (payment.LimitArticleList.Count == 1)))
                {
                    double shareMoney = 0;
                    payment.PayMoneyNoShare = payment.PayMoney;
                    foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                    {
                        shareMoney = couponArticle.PayLimitMoney;
                        if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, couponArticle.Article.SaleMoneyNoShare))
                            shareMoney = couponArticle.Article.SaleMoneyNoShare;
                        if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, payment.PayMoneyNoShare))
                            shareMoney = payment.PayMoneyNoShare;
                        if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                        {
                            CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, shareList, couponArticle.Article, payment, shareMoney, (payment.JoinPromOfferCoupon && couponArticle.JoinPromOfferCoupon));
                            //payment.PayMoneyNoShare = payment.PayMoneyNoShare - shareMoney;
                        }
                    }
                    if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoneyNoShare, 0))
                    {
                        errorMessage = string.Format("{0} 的收款额超过规定额,分摊没成功", payment.PayTypeName);
                        return false;
                    }
                    payment.IsShared = true;
                }
            }
            //去掉已分摊的券
            for (int i = couponPaymentList.Count - 1; i >= 0; i--)
            {
                if (couponPaymentList[i].IsShared)
                    couponPaymentList.RemoveAt(i);
            }
            return true;
        }

        public static bool ShareCouponPaymentList2(out string errorMessage, List<CrmPayment> couponPaymentList, List<CrmPaymentArticleShare> shareList)
        {
            errorMessage = string.Empty;
            if (couponPaymentList.Count > 1)
            {
                List<CrmPayment> sortedPaymentList = new List<CrmPayment>();
                bool isFound = false;
                foreach (CrmPayment payment in couponPaymentList)
                {
                    isFound = false;
                    for (int i = 0; i < sortedPaymentList.Count; i++)
                    {
                        if (MathUtils.DoubleASmallerThanDoubleB((payment.TotalLimitMoney / payment.PayMoney), (sortedPaymentList[i].TotalLimitMoney / sortedPaymentList[i].PayMoney)))
                        {
                            isFound = true;
                            sortedPaymentList.Insert(i, payment);
                            break;
                        }
                    }
                    if (!isFound)
                        sortedPaymentList.Add(payment);
                }
                couponPaymentList.Clear();
                for (int i = 0; i < sortedPaymentList.Count; i++)
                {
                    couponPaymentList.Add(sortedPaymentList[i]);
                }
            }

            foreach (CrmPayment payment in couponPaymentList)
            {
                if ((payment.LimitArticleList != null) && (payment.LimitArticleList.Count > 0))
                {
                    double totalMoney = 0;
                    foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(couponArticle.Article.SaleMoneyNoShare, couponArticle.PayLimitMoney))
                            totalMoney = totalMoney + couponArticle.PayLimitMoney;
                        else
                            totalMoney = totalMoney + couponArticle.Article.SaleMoneyNoShare;
                    }
                    if (MathUtils.DoubleASmallerThanDoubleB(totalMoney, payment.PayMoney))
                    {
                        errorMessage = string.Format("{0} 的收款额超过规定额,分摊没成功", payment.PayTypeName);
                        return false;
                    }

                    ShareLimitCouponPaymentToArticle(payment, shareList);
                }
            }
            return true;
        }

        public static void ShareLimitCouponPaymentToArticle(CrmPayment payment, List<CrmPaymentArticleShare> shareList)
        {
            if ((payment.LimitArticleList == null) || (payment.LimitArticleList.Count == 0))
                return;

            double shareMoney = 0;
            if (payment.LimitArticleList.Count == 1)
            {
                CrmArticleCoupon couponArticle = payment.LimitArticleList[0];
                if (MathUtils.DoubleAGreaterThanDoubleB(couponArticle.Article.SaleMoneyNoShare, couponArticle.PayLimitMoney))
                    shareMoney = couponArticle.PayLimitMoney;
                else
                    shareMoney = couponArticle.Article.SaleMoneyNoShare;
                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, payment.PayMoneyNoShare))
                    shareMoney = payment.PayMoneyNoShare;
                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                {
                    CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, shareList, couponArticle.Article, payment, shareMoney, (payment.JoinPromOfferCoupon && couponArticle.JoinPromOfferCoupon));
                    //payment.PayMoneyNoShare = payment.PayMoneyNoShare - shareMoney;
                }
            }
            else
            {
                double totalMoney = 0;
                foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(couponArticle.Article.SaleMoneyNoShare, couponArticle.PayLimitMoney))
                        couponArticle.Article.TempMoney = couponArticle.PayLimitMoney;
                    else
                        couponArticle.Article.TempMoney = couponArticle.Article.SaleMoneyNoShare;
                    totalMoney = totalMoney + couponArticle.Article.TempMoney;
                }
                List<CrmPaymentArticleShare> tempShareList = new List<CrmPaymentArticleShare>();
                if (MathUtils.DoubleASmallerThanDoubleB(payment.PayMoneyNoShare, totalMoney))   //用券额 < 商品额
                {
                    foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                    {
                        //分摊过程中 payment.PayMoney 不会改变
                        shareMoney = Math.Round(payment.PayMoney * (couponArticle.Article.TempMoney / totalMoney), 2);
                        if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                        {
                            CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, tempShareList, couponArticle.Article, payment, shareMoney, (payment.JoinPromOfferCoupon && couponArticle.JoinPromOfferCoupon));
                            //payment.PayMoneyNoShare = payment.PayMoneyNoShare - shareMoney;
                        }
                    }
                    if (!MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0))
                    {
                        //存在小数计算尾差
                        if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoneyNoShare, 0))
                        {
                            foreach (CrmPaymentArticleShare share in tempShareList)
                            {
                                shareMoney = payment.PayMoneyNoShare;
                                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, share.Article.SaleMoneyNoShare))
                                    shareMoney = share.Article.SaleMoneyNoShare;
                                if (payment.CouponType >= 0)
                                {
                                    double limitMoney = GetCouponPayLimitMoney(share.Article, payment);
                                    if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, limitMoney - share.ShareMoney))
                                        shareMoney = limitMoney - share.ShareMoney;
                                }
                                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                                {
                                    UpdatePaymentArticleShareMoney(share, shareMoney);
                                    //payment.PayMoneyNoShare = payment.PayMoneyNoShare - shareMoney;
                                }
                                if (MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0))
                                    break;
                            }
                        }
                        else 
                        {
                            foreach (CrmPaymentArticleShare share in tempShareList)
                            {
                                shareMoney = payment.PayMoneyNoShare;
                                if (MathUtils.DoubleAGreaterThanDoubleB(share.Article.SaleMoneyNoShare, 0))
                                {
                                    if (MathUtils.DoubleASmallerThanDoubleB(shareMoney + share.Article.SaleMoneyNoShare, 0))
                                        shareMoney = -share.Article.SaleMoneyNoShare;
                                }
                                else
                                {
                                    if (MathUtils.DoubleASmallerThanDoubleB(shareMoney, share.Article.SaleMoneyNoShare))
                                        shareMoney = share.Article.SaleMoneyNoShare;
                                }//zxc 2012.11.09 没完全想透，以后再想

                                if (MathUtils.DoubleASmallerThanDoubleB(shareMoney, 0))
                                {
                                    UpdatePaymentArticleShareMoney(share, shareMoney);
                                }
                                if (MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0))
                                    break;
                            }
                        }
                    }
                }
                else  //用券额 >= 商品额
                {
                    foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(couponArticle.Article.SaleMoneyNoShare, couponArticle.PayLimitMoney))
                            shareMoney = couponArticle.PayLimitMoney;
                        else
                            shareMoney = couponArticle.Article.SaleMoneyNoShare;

                        if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                        {
                            CrmPaymentArticleShare share = AddPaymentArticleShareToList(false, tempShareList, couponArticle.Article, payment, shareMoney, (payment.JoinPromOfferCoupon && couponArticle.JoinPromOfferCoupon));
                            //payment.PayMoneyNoShare = payment.PayMoneyNoShare - shareMoney;
                        }
                    }
                }
                shareList.AddRange(tempShareList);
            }
            payment.IsShared = MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0);
        }

        public static bool SharePaymentEqually(out string errorMessage, List<CrmArticle> articleList, List<CrmPayment> paymentList, List<CrmPaymentArticleShare> shareList)
        {
            errorMessage = string.Empty;
            double totalPayMoney = 0;
            foreach (CrmPayment payment in paymentList)
            {
                totalPayMoney = totalPayMoney + payment.PayMoneyNoShare;
            }
            double totalSaleMoney = 0;
            foreach (CrmArticle article in articleList)
            {
                article.TempMoney = article.SaleMoneyNoShare;
                totalSaleMoney = totalSaleMoney + article.TempMoney;
            }

            if (MathUtils.DoubleASmallerThanDoubleB(totalSaleMoney, totalPayMoney))
            {
                errorMessage = "收款分摊到销售商品算法出错。";
                return false;
            }
            if (MathUtils.DoubleAEuqalToDoubleB(totalSaleMoney, 0) || MathUtils.DoubleAEuqalToDoubleB(totalPayMoney, 0))
                return true;

            if (articleList.Count == 1)
            {
                CrmArticle article = articleList[0];
                foreach (CrmPayment payment in paymentList)
                {
                    bool maybeExistShare = MathUtils.DoubleASmallerThanDoubleB(payment.PayMoneyNoShare, payment.PayMoney);
                    if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoneyNoShare, 0))
                    {
                        CrmPaymentArticleShare share = AddPaymentArticleShareToList(maybeExistShare, shareList, article, payment, payment.PayMoneyNoShare, CheckCouponPaymentJoinOfferCoupon(article, payment));
                    }
                }
            }
            else if ((paymentList.Count == 1) && MathUtils.DoubleAEuqalToDoubleB(totalSaleMoney, totalPayMoney))
            {
                CrmPayment payment = paymentList[0];
                bool maybeExistShare = MathUtils.DoubleASmallerThanDoubleB(payment.PayMoneyNoShare, payment.PayMoney);
                foreach (CrmArticle article in articleList)
                {
                    if (MathUtils.DoubleAGreaterThanDoubleB(article.SaleMoneyNoShare, 0))
                    {
                        CrmPaymentArticleShare share = AddPaymentArticleShareToList(maybeExistShare, shareList, article, payment, article.SaleMoneyNoShare, CheckCouponPaymentJoinOfferCoupon(article, payment));
                    }
                }
            }
            else if (articleList.Count > 1)
            {
                List<CrmPaymentArticleShare> tempShareList = new List<CrmPaymentArticleShare>();
                double toShareMoney = 0;
                double shareMoney = 0;
                foreach (CrmPayment payment in paymentList)
                {
                    tempShareList.Clear();
                    bool maybeExistShare = MathUtils.DoubleASmallerThanDoubleB(payment.PayMoneyNoShare, payment.PayMoney);
                    toShareMoney = payment.PayMoneyNoShare;
                    foreach (CrmArticle article in articleList)
                    {
                        shareMoney = Math.Round(toShareMoney * (article.TempMoney / totalSaleMoney), 2);
                        CrmPaymentArticleShare share = AddPaymentArticleShareToList(maybeExistShare, shareList, article, payment, shareMoney, CheckCouponPaymentJoinOfferCoupon(article, payment));
                        tempShareList.Add(share);
                    }
                    if (!MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0))
                    {
                        //存在小数计算尾差
                        if (MathUtils.DoubleAGreaterThanDoubleB(payment.PayMoneyNoShare, 0))
                        {
                            foreach (CrmPaymentArticleShare share in tempShareList)
                            {
                                shareMoney = payment.PayMoneyNoShare;
                                //if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, share.Article.SaleMoneyNoShare - share.Article.SaleMoneySharedJoinDecMoney))
                                //    shareMoney = share.Article.SaleMoneyNoShare - share.Article.SaleMoneySharedJoinDecMoney;
                                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, share.Article.SaleMoneyNoShare))
                                    shareMoney = share.Article.SaleMoneyNoShare;
                                if (MathUtils.DoubleAGreaterThanDoubleB(shareMoney, 0))
                                {
                                    UpdatePaymentArticleShareMoney(share, shareMoney);
                                }
                                if (MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0))
                                    break;
                            }
                        }
                        else 
                        {
                            foreach (CrmPaymentArticleShare share in tempShareList)
                            {
                                shareMoney = payment.PayMoneyNoShare;
                                if (MathUtils.DoubleAGreaterThanDoubleB(share.Article.SaleMoneyNoShare, 0))
                                {
                                    if (MathUtils.DoubleASmallerThanDoubleB(shareMoney + share.Article.SaleMoneyNoShare, 0))
                                        shareMoney = -share.Article.SaleMoneyNoShare;
                                }
                                else
                                {
                                    if (MathUtils.DoubleASmallerThanDoubleB(shareMoney, share.Article.SaleMoneyNoShare))
                                        shareMoney = share.Article.SaleMoneyNoShare;
                                }//zxc 2012.11.09 没完全想透，以后再想

                                if (MathUtils.DoubleASmallerThanDoubleB(shareMoney, 0))
                                {
                                    UpdatePaymentArticleShareMoney(share, shareMoney);
                                }
                                if (MathUtils.DoubleAEuqalToDoubleB(payment.PayMoneyNoShare, 0))
                                    break;
                            }
                        }
                    }
                }
            }
            return true;
        }

        //根据商品收款分摊，计算商品银行卡分摊
        public static void SharePaymentArticleToBankCard2(List<CrmPaymentArticleShare> paymentArticleShareList, List<CrmPromPaymentOfferCouponArticle> bankCardPaymentArticleList)
        {
            foreach (CrmPaymentArticleShare share in paymentArticleShareList)
            {
                if ((share.Payment.BankCardList == null) || (share.Payment.BankCardList.Count == 0))
                {
                    CrmPromPaymentOfferCouponArticle bankCardPaymentArticle = new CrmPromPaymentOfferCouponArticle();
                    bankCardPaymentArticleList.Add(bankCardPaymentArticle);
                    bankCardPaymentArticle.Article = share.Article;
                    bankCardPaymentArticle.Payment = share.Payment;
                    bankCardPaymentArticle.BankCardPayment = null;
                    bankCardPaymentArticle.PayMoney = share.ShareMoney;
                }
                else
                {
                    double tmpMoney = share.ShareMoney;
                    for (int i = 0; i < share.Payment.BankCardList.Count - 1; i++)
                    {
                        CrmBankCardPayment bankCard = share.Payment.BankCardList[i];
                        CrmPromPaymentOfferCouponArticle bankCardPaymentArticle = new CrmPromPaymentOfferCouponArticle();
                        bankCardPaymentArticleList.Add(bankCardPaymentArticle);
                        bankCardPaymentArticle.Article = share.Article;
                        bankCardPaymentArticle.Payment = share.Payment;
                        bankCardPaymentArticle.BankCardPayment = bankCard;
                        bankCardPaymentArticle.PayMoney = Math.Round(share.ShareMoney * (bankCard.PayMoney/share.Payment.PayMoney),2);
                        tmpMoney -= bankCardPaymentArticle.PayMoney;
                    }
                    CrmPromPaymentOfferCouponArticle bankCardPaymentArticle2 = new CrmPromPaymentOfferCouponArticle();
                    bankCardPaymentArticleList.Add(bankCardPaymentArticle2);
                    bankCardPaymentArticle2.Article = share.Article;
                    bankCardPaymentArticle2.Payment = share.Payment;
                    bankCardPaymentArticle2.BankCardPayment = share.Payment.BankCardList[share.Payment.BankCardList.Count - 1];
                    bankCardPaymentArticle2.PayMoney = tmpMoney;
                }
            }
        }
        public static void SharePaymentArticleToBankCard(List<CrmPaymentArticleShare> paymentArticleShareList, List<CrmBankCardPaymentArticleShare> bankCardPaymentArticleList)
        {
            List<CrmPayment> bankCardPaymentList = new List<CrmPayment>();
            foreach (CrmPaymentArticleShare share in paymentArticleShareList)
            {
                if ((share.Payment.BankCardList == null) || (share.Payment.BankCardList.Count == 0))
                {
                    CrmBankCardPaymentArticleShare bankCardPaymentArticle = new CrmBankCardPaymentArticleShare();
                    bankCardPaymentArticleList.Add(bankCardPaymentArticle);
                    bankCardPaymentArticle.PaymentArticleShare = share;
                    //bankCardPaymentArticle.Article = share.Article;
                    //bankCardPaymentArticle.Payment = share.Payment;
                    bankCardPaymentArticle.BankCardPayment = null;
                    bankCardPaymentArticle.PayMoney = share.ShareMoney;
                }
                else
                {
                    bankCardPaymentList.Add(share.Payment);
                    //double tmpMoney = share.ShareMoney;
                    //for (int i = 0; i < share.Payment.BankCardList.Count - 1; i++)
                    //{
                    //    CrmBankCardPayment bankCard = share.Payment.BankCardList[i];
                    //    CrmBankCardPaymentArticleShare bankCardPaymentArticle = new CrmBankCardPaymentArticleShare();
                    //    bankCardPaymentArticleList.Add(bankCardPaymentArticle);
                    //    bankCardPaymentArticle.Article = share.Article;
                    //    bankCardPaymentArticle.Payment = share.Payment;
                    //    bankCardPaymentArticle.BankCardPayment = bankCard;
                    //    bankCardPaymentArticle.PayMoney = Math.Round(share.ShareMoney * (bankCard.PayMoney/share.Payment.PayMoney),2);
                    //    tmpMoney -= bankCardPaymentArticle.PayMoney;
                    //}
                    //CrmBankCardPaymentArticleShare bankCardPaymentArticle2 = new CrmBankCardPaymentArticleShare();
                    //bankCardPaymentArticleList.Add(bankCardPaymentArticle2);
                    //bankCardPaymentArticle2.Article = share.Article;
                    //bankCardPaymentArticle2.Payment = share.Payment;
                    //bankCardPaymentArticle2.BankCardPayment = share.Payment.BankCardList[share.Payment.BankCardList.Count - 1];
                    //bankCardPaymentArticle2.PayMoney = tmpMoney;
                }
            }
            foreach (CrmPayment payment in bankCardPaymentList)
            {
                double totalMoney = 0;
                double tmpMoney = 0;
                foreach (CrmPaymentArticleShare share in paymentArticleShareList)
                {
                    if (share.Payment == payment)
                        totalMoney += share.ShareMoney;
                }
                foreach (CrmBankCardPayment bankCard in payment.BankCardList)
                {
                    tmpMoney = bankCard.PayMoney;
                    foreach (CrmPaymentArticleShare share in paymentArticleShareList)
                    {
                        if (share.Payment == payment)
                        {
                            CrmBankCardPaymentArticleShare bankCardPaymentArticle = new CrmBankCardPaymentArticleShare();
                            bankCardPaymentArticleList.Add(bankCardPaymentArticle);
                            bankCardPaymentArticle.PaymentArticleShare = share;
                            //bankCardPaymentArticle.Article = share.Article;
                            //bankCardPaymentArticle.Payment = share.Payment;
                            bankCardPaymentArticle.BankCardPayment = bankCard;
                            bankCardPaymentArticle.PayMoney = Math.Round(bankCard.PayMoney * (share.ShareMoney / totalMoney), 2);
                            if (MathUtils.DoubleAGreaterThanDoubleB(bankCardPaymentArticle.PayMoney + share.TempMoney, share.ShareMoney))
                                bankCardPaymentArticle.PayMoney = share.ShareMoney - share.TempMoney;
                            tmpMoney -= bankCardPaymentArticle.PayMoney;
                            share.TempMoney += bankCardPaymentArticle.PayMoney;
                        }
                    }
                    if (!MathUtils.DoubleAEuqalToDoubleB(tmpMoney, 0))  //存在尾差
                    {
                        foreach (CrmBankCardPaymentArticleShare bankCardPaymentArticle in bankCardPaymentArticleList)
                        {
                            if ((bankCardPaymentArticle.PaymentArticleShare.Payment == payment) && (bankCardPaymentArticle.BankCardPayment == bankCard))
                            {
                                if (MathUtils.DoubleAGreaterThanDoubleB(bankCardPaymentArticle.PaymentArticleShare.TempMoney + tmpMoney, bankCardPaymentArticle.PaymentArticleShare.ShareMoney))
                                {
                                    double tmpMoney2 = bankCardPaymentArticle.PaymentArticleShare.ShareMoney - bankCardPaymentArticle.PaymentArticleShare.TempMoney;
                                    bankCardPaymentArticle.PaymentArticleShare.TempMoney += tmpMoney2;
                                    bankCardPaymentArticle.PayMoney += tmpMoney2;
                                    tmpMoney -= tmpMoney2;
                                }
                                else
                                {
                                    bankCardPaymentArticle.PaymentArticleShare.TempMoney += tmpMoney;
                                    bankCardPaymentArticle.PayMoney += tmpMoney;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        public static CrmPaymentArticleShare AddPaymentArticleShareToList(bool maybeExist, List<CrmPaymentArticleShare> shareList, CrmArticle article, CrmPayment payment, double shareMoney, bool joinPromOfferCoupon)
        {
            CrmPaymentArticleShare share = null;
            if (maybeExist)
            {
                foreach (CrmPaymentArticleShare share2 in shareList)
                {
                    if ((share2.Article == article) && (share2.Payment == payment))
                    {
                        share = share2;
                        break;
                    }
                }
            }
            if (share == null)
            {
                share = new CrmPaymentArticleShare();
                shareList.Add(share);
                share.Article = article;
                share.Payment = payment;
                share.JoinPromOfferCoupon = joinPromOfferCoupon;
            }
            UpdatePaymentArticleShareMoney(share, shareMoney);

            return share;
        }

        public static void UpdatePaymentArticleShareMoney(CrmPaymentArticleShare share, double updateMoney)
        {
            share.ShareMoney = share.ShareMoney + updateMoney;

            share.Article.SaleMoneyNoShare -= updateMoney;
            if (!share.Payment.JoinPromDecMoney)
                share.Article.SaleMoneyForDecMoney -= updateMoney;
            if (!share.JoinPromOfferCoupon)
                share.Article.SaleMoneyForOfferCoupon -= updateMoney;
            if (!share.Payment.JoinCent)
                share.Article.SaleMoneyForCent -= updateMoney;
            share.Payment.PayMoneyNoShare -= updateMoney;
        }

        public static double GetCouponPayLimitMoney(CrmArticle article, CrmPayment payment)
        {
            if (payment.JoinPromOfferCoupon && (payment.LimitArticleList != null) && (payment.LimitArticleList.Count > 0))
            {
                foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                {
                    if ((couponArticle.Article == article))
                    {
                        return couponArticle.PayLimitMoney;
                    }
                }
            }
            return article.SaleMoney;
        }

        public static bool CheckCouponPaymentJoinOfferCoupon(CrmArticle article, CrmPayment payment)
        {
            if (payment.JoinPromOfferCoupon && (payment.LimitArticleList != null) && (payment.LimitArticleList.Count > 0))
            {
                foreach (CrmArticleCoupon couponArticle in payment.LimitArticleList)
                {
                    if ((couponArticle.Article == article))
                    {
                        return (payment.JoinPromOfferCoupon && couponArticle.JoinPromOfferCoupon);
                    }
                }
            }
            return payment.JoinPromOfferCoupon;
        }

    }
}
