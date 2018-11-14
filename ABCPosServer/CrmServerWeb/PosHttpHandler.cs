using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Net;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Xml;
using ChangYi.Pub;

namespace ChangYi.Crm.Server.Web
{
    public class PosHttpHandler : IHttpHandler
    {
        // 以下是 预定义的功能代码
        //private const string AppSys = "00"; //系统管理
        private const string AppLogin = "0000"; //登陆
        private const string AppLogoff = "0001"; //退出

        //private const string AppPos = "01"; //会员消费（POS）
        private const string AppGetVipCard = "0101"; //取会员信息
        private const string AppGetVipDisc = "0102"; //取会员折扣
        private const string AppSaveRSaleBillArticles = "0103"; //保存消费商品
        private const string AppCheckoutRSaleBill = "0104"; //结帐
        private const string AppGetCashCard = "0105"; //查询储值卡余额
        private const string AppPrepareTransCashCard = "0106"; //储值卡交易准备
        private const string AppGetVipCoupon = "0107"; //查询购物券余额
        private const string AppPrepareTransCoupon = "0108"; //购物券交易准备
        private const string AppProcTrans = "0109"; //交易处理
        private const string App_CalcRSBillCent = "0110"; //补积分

        private const string AppSendCard = "0111"; //发卡
        private const string AppRecycleCard = "0112"; //收卡
        private const string AppGetPayAccount = "0113"; //对帐
        private const string AppGetCash2Coupon = "0114"; //查询现金买券
        private const string AppGetSaleMoneyLeftWhenOfferCoupon = "0115"; //查询待返券消费金额
        private const string AppGetPayTrans = "0116"; //查询会员消费交易
        //zhangli 2007.5.28
        private const string AppGetVipToBuyCent = "0117";  //传输退货积分
        private const string AppBuyVipCent = "0118";  //处理钱买分  
        private const string AppGetBuyVipCentAccount = "0119";  //取现金买分

        private const string AppPos_GetDecM = "0120"; //取满百减金额
        private const string AppPos_GetMemberGrantVoucher = "0121"; //取返券会员信息

        private const string AppGetYHQDM = "0122"; //取优惠券代码
        private const string AppPreTransYHQDM = "0123"; //优惠券代码交易准备
        private const string AppGetYHKZFHD = "0124"; //取银行卡支付的活动
        private const string AppChgCardPwd = "0125"; //修改卡密码
        private const string AppGetDMQZFList = "0126"; //取收款员的编码券支付列表

        private const string AppPreTransSaveChange = "0127"; //零钱包交易准备

        private const string AppSaveMoneyToCashCard = "0128"; //储值卡存款
        private const string AppReturnCashCard = "0129"; //储值卡退卡
        private const string AppPreTransCashCardRecharging = "0130"; //储值卡充值交易准备

        private const string SrchArticlePromRule = "0131"; //查询商品促销规则

        private const string AppUpdateVipCent = "0141"; //加减会员积分

        //zhangli 2008.5.22
        //private const string AppPos_GetPGXX = "0130";  //取陪赔人员信息
        //private const string AppPos_GetHYZK_HYKTYPE = "0131"; //取会员卡类型折扣
        //private const string AppPos_CheckOutOffline = "0132"; //脱机小票上传  

        //private const string App_CalcRSBillCent = "0199"; //保存小票并计算积分

        //private const string AppInfo = "02"; //信息
        //private const string AppBaseInfo_Upload = "0201"; //基本信息上传

        //private const string AppMaxCode = "03";
        // 以上是 预定义的功能代码

        /// <summary>
        /// 您将需要在您网站的 web.config 文件中配置此处理程序，
        /// 并向 IIS 注册此处理程序，然后才能进行使用。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //在此写入您的处理程序实现。
            if (context.Request.HttpMethod.Equals("GET"))
            {
                string msg;
                context.Response.Write("<br> PosHttpHandler 已可响应请求");
                context.Response.Write("<br>");
                ChangYi.Pub.DbConnManager.TestDbConnect("CRMDB", out msg);
                if (msg.Length > 0)
                    context.Response.Write(msg);
                PosProc.TestCode(out msg);
                if (msg.Length > 0)
                    context.Response.Write(msg);
                CrmServerPlatform.CheckTablesForRecordsAffectedAfterExecSql(out msg);
                if (msg.Length > 0)
                    context.Response.Write(msg);

                return;
            }
            if (!context.Request.HttpMethod.Equals("POST"))
            {
                return;
            }

            if (context.Request.ContentLength < 20)
                return;

            DateTime timeBegin = DateTime.Now;

            Stream reqStream = context.Request.InputStream;

            /*
            int reqSize = context.Request.ContentLength;
            byte[] reqBytes = new byte[reqSize];
            int totalReadNum = 0;
            while (totalReadNum < reqSize)
            {
                int readNum = reqStream.Read(reqBytes, totalReadNum, reqSize - totalReadNum);
                totalReadNum = totalReadNum + readNum;
            }
            string reqStr = Encoding.Default.GetString(reqBytes);
            */

            int sizePrefix = 20;

            byte[] reqBytes = new byte[sizePrefix];
            int offset1 = 0;
            while (offset1 < sizePrefix)
            {
                int readNum = reqStream.Read(reqBytes, offset1, sizePrefix - offset1);
                offset1 = offset1 + readNum;
            }
            string crmSign = Encoding.Default.GetString(reqBytes, 0, 8);
            if (!crmSign.Equals("BFCRMXML"))
                return;

            string appCode = Encoding.Default.GetString(reqBytes, 8, 4);
            int sizeXml = int.Parse(Encoding.Default.GetString(reqBytes, 12, 8), 0);

            if (sizeXml != (context.Request.ContentLength - sizePrefix))
                return;

            reqBytes = new byte[context.Request.ContentLength];
            int offset2 = 0;
            while (offset2 < sizeXml)
            {
                int readNum = reqStream.Read(reqBytes, offset1 + offset2, sizeXml - offset2);
                offset2 = offset2 + readNum;
            }
            string reqXml = Encoding.Default.GetString(reqBytes, offset1, sizeXml);

            DateTime timeRead = DateTime.Now;

            String respXml = string.Empty;
            string errorMsg = string.Empty;
            string errorLog = string.Empty;
            bool dbConnError = false;
            try
            {
                DoResponse(appCode, reqXml, out respXml);
            }
            catch (Exception e)
            {
                CrmServerPlatform.ParseException(out errorMsg, out errorLog, out dbConnError, e);
            }

            if (dbConnError && CrmServerPlatform.Config.LoadBalance)
            {
                context.Response.StatusCode = 503;
                context.Response.StatusDescription = "数据库连接失败";
            }
            else if (!appCode.Equals(AppLogoff))
            {
                if (errorMsg.Length > 0)
                {
                    respXml = SpellFailXml("serverproc", errorMsg);
                }
                StringBuilder sb = new StringBuilder();
                sb.Append("BFCRMXML").Append(respXml.Length.ToString("d8")).Append(respXml);

                byte[] respBytes = System.Text.Encoding.Default.GetBytes(sb.ToString());
                Stream respStream = context.Response.OutputStream;
                respStream.Write(respBytes, 0, respBytes.Length);
                respStream.Flush();
            }

            DateTime timeEnd = DateTime.Now;
            StringBuilder logStr = new StringBuilder();

            logStr.Append("\r\n begin ").Append(appCode).Append(",").Append(timeBegin.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(", ").Append(context.Request.UserHostAddress);
            logStr.Append("\r\n").Append(reqXml);
            logStr.Append("\r\n").Append(respXml);
            logStr.Append("\r\n end ").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            logStr.Append(", ").Append(timeRead.Subtract(timeBegin).TotalMilliseconds.ToString("f0"));
            logStr.Append(", ").Append(timeEnd.Subtract(timeBegin).TotalMilliseconds.ToString("f0")).Append(" ms");
            logStr.Append("\r\n");
            CrmServerPlatform.WriteDataLog(timeBegin.ToString("yyyy-MM-dd"), logStr.ToString());

            if (errorLog.Length > 0)
            {
                logStr.Length = 0;
                logStr.Append("\r\n request ").Append(appCode).Append(",").Append(timeBegin.ToString("yyyy-MM-dd HH:mm:ss.fff")).Append(", ").Append(context.Request.UserHostAddress);
                logStr.Append("\r\n").Append(reqXml);
                logStr.Append("\r\n error ").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                logStr.Append(", ").Append(timeRead.Subtract(timeBegin).TotalMilliseconds.ToString("f0"));
                logStr.Append(", ").Append(timeEnd.Subtract(timeBegin).TotalMilliseconds.ToString("f0")).Append(" ms");
                logStr.Append("\r\n").Append(errorLog);
                CrmServerPlatform.WriteErrorLog(logStr.ToString());
            }
        }

        #endregion

        private void DoRequestToAnotherServer(string serviceUrl, string appCode, string reqXml, out string respXml)
        {
            respXml = string.Empty;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serviceUrl);
                req.Method = "POST";
                req.Timeout = 6000;

                StringBuilder sb = new StringBuilder();
                sb.Append("BFCRMXML").Append(appCode).Append(reqXml.Length.ToString("d8")).Append(reqXml);
                byte[] reqBytes = Encoding.Default.GetBytes(sb.ToString());
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(reqBytes, 0, reqBytes.Length);

                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.Default);
                string respStr = reader.ReadToEnd();
                int len = respStr.Length;
                if (len > 16)
                    respXml = respStr.Substring(16);
                else
                    respXml = SpellFailXml("logic", "异地服务返回数据错误");
            }
            catch (Exception e)
            {
                respXml = SpellFailXml("logic", string.Format("访问异地服务失败 {0}", serviceUrl));
            }
        }

        private void DoResponse(string appCode, string reqXml, out string respXml)
        {
            respXml = string.Empty;
            switch (appCode)
            {
                case AppLogin:
                    Login(reqXml, out respXml);
                    break;
                case AppLogoff:
                    Logoff(reqXml, out respXml);
                    break;
                case AppGetVipCard:
                    GetVipCard(reqXml, out respXml);
                    break;
                case AppPos_GetMemberGrantVoucher:
                    GetVipCardToOfferCoupon(reqXml, out respXml);
                    break;
                case AppGetVipDisc:
                    GetArticleVipDisc(reqXml, out respXml);
                    break;
                case AppSaveRSaleBillArticles:
                    SaveRSaleBillArticles(reqXml, out respXml);
                    break;
                case AppCheckoutRSaleBill:
                    CheckOutRSaleBill(reqXml, out respXml);
                    break;
                case AppGetCashCard:
                    GetCashCard(reqXml, out respXml);
                    break;
                case AppPrepareTransCashCard:
                    PrepareTransCashCard(reqXml, out respXml);
                    break;
                case AppGetVipCoupon:
                    GetVipCoupon(reqXml, out respXml);
                    break;
                case AppPrepareTransCoupon:
                    PrepareTransCoupon(reqXml, out respXml);
                    break;
                case AppProcTrans:
                    ProcTrans(reqXml, out respXml);
                    break;
                case AppPos_GetDecM:
                    CalcRSaleBillDecMoney(reqXml, out respXml);
                    break;
                case AppGetSaleMoneyLeftWhenOfferCoupon:
                    GetSaleMoneyLeftWhenOfferCoupon(reqXml, out respXml);
                    break;
                case AppGetYHQDM:
                    GetCodedCoupon(reqXml, out respXml);
                    break;
                case AppPreTransYHQDM:
                    PrepareTransCodedCouponPayment(reqXml, out respXml);
                    break;
                case AppGetPayAccount:
                    GetPayAccount(reqXml, out respXml);
                    break;
                case AppGetDMQZFList:
                    GetCodedCouponPayAccount_Dennis(reqXml, out respXml);
                    break;
                case AppGetCash2Coupon:
                    GetVipOfferBackDifference(reqXml, out respXml);
                    break;
                case AppGetVipToBuyCent:
                    GetVipCardToBuyCent(reqXml, out respXml);
                    break;
                case AppBuyVipCent:
                    BuyVipCent(reqXml, out respXml);
                    break;
                //case AppGetBuyVipCentAccount:
                //    GetBuyVipCentAccount(reqXml, out respXml);
                //    break;
                case AppGetYHKZFHD:
                    GetBankCardPromDesc(reqXml, out respXml);
                    break;
                case App_CalcRSBillCent:
                    CalcRSaleBillCent(reqXml, out respXml);
                    break;
                case AppChgCardPwd:
                    ChangeCardPassword(reqXml, out respXml);
                    break;
                case AppPreTransSaveChange:
                    PrepareTransSaveChangeIntoVipCard(reqXml, out respXml);
                    break;
                case AppSaveMoneyToCashCard:
                    SaveMoneyToCashCard(reqXml, out respXml);
                    break;
                case AppReturnCashCard:
                    ReturnCashCard(reqXml, out respXml);
                    break;
                case SrchArticlePromRule:
                    SearchArticlePromRule(reqXml, out respXml);
                    break;
            }
        }

        private string SpellFailXml(string failType, string msg)
        {
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "N");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "fail_type", failType);
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "message", msg);
            return respXmlDoc.OuterXml;
        }

        private string MakeClientSessionId(CrmStoreInfo storeInfo, string posId)
        {
            return string.Format("{0},{1},{2},{3}", storeInfo.Company, storeInfo.StoreCode, storeInfo.StoreId, posId);
        }

        private bool GetStoreInfoAndPosIdFromClientSession(XmlNode reqXmlRoot, CrmStoreInfo storeInfo, out string posId)
        {
            posId = string.Empty;
            string sessionId = string.Empty;
            bool ok = false;
            if (XmlUtils.GetAttributeValue(reqXmlRoot, "session_id", out sessionId))
            {
                string[] vals = sessionId.Split(',');
                if (vals.Length == 4)
                {
                    storeInfo.Company = vals[0];
                    storeInfo.StoreCode = vals[1];
                    storeInfo.StoreId = int.Parse(vals[2]);
                    posId = vals[3];
                    ok = true;
                }
            }
            return ok;
        }
        private bool GetStoreInfoFromClientSession(XmlNode reqXmlRoot, CrmStoreInfo storeInfo)
        {
            string sessionId = string.Empty;
            bool ok = false;
            if (XmlUtils.GetAttributeValue(reqXmlRoot, "session_id", out sessionId))
            {
                string[] vals = sessionId.Split(',');
                if (vals.Length == 4)
                {
                    storeInfo.Company = vals[0];
                    storeInfo.StoreCode = vals[1];
                    storeInfo.StoreId = int.Parse(vals[2]);
                    //posId = vals[3];
                    ok = true;
                }
            }
            return ok;
        }

        private void Login(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            CrmLoginData loginData = new CrmLoginData();

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store", out loginData.StoreInfo.StoreCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "machine", out loginData.PosId);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bfcrm_user", out loginData.UserCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "password", out loginData.Password);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "system", out loginData.ClientSystem);

            string msg = string.Empty;
            bool ok = (PosProc.Login(out msg, loginData));

            if (ok)
            {
                XmlDocument respXmlDoc = new XmlDocument();
                XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                respXmlDoc.AppendChild(xmlDec);

                XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
                respXmlDoc.AppendChild(respXmlRoot);
                XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
                if (loginData.StoreInfo.StoreCode.Length > 0)
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "session_id", MakeClientSessionId(loginData.StoreInfo, loginData.PosId));
                else
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "session_id", RandomUtils.MakeRandomString(20));
                if (loginData.ClientSystem.Equals("bfpos"))
                {
                    if (loginData.ExistPromOfferCoupon)
                        XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "exist_grant_voucher_promotion", "Y");
                    if (loginData.ExistPromDecMoney)
                        XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "exist_dec_money_promotion", "Y");
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "company", loginData.StoreInfo.Company);
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "length_verify_cashcard", loginData.LengthVerifyOfCashCard.ToString());
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "length_verify_membercard", loginData.LengthVerifyOfVipCard.ToString());
                    if (loginData.PayCashCardWithArticle)
                        XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "pay_cashcard_with_article", "Y");
                }
                respXml = respXmlDoc.OuterXml;
            }
            else
            {
                respXml = SpellFailXml("login", msg);
            }
        }
        private void Logoff(string reqXml, out string respXml)
        {
            respXml = string.Empty;
        }

        private void GetVipCard(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素member_id或track_data, 且有值";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            CrmVipCard crmCard = null;
           // List<CrmVipCard> cardList = null;

            bool ok = false;
            //if (condType != 3)
                ok = PosProc.GetVipCard(out msg, out crmCard, condType, condValue, cardCodeToCheck, verifyCode);
            //else
            //    ok = PosProc.GetVipCardList(out msg, cardList, condType, condValue);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            //if (condType != 3)
            //{
                XmlNode vipNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "member");
                XmlUtils.SetAttributeValue(respXmlDoc, vipNode, "id", crmCard.CardId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "code", crmCard.CardCode);
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "name", crmCard.VipName);
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_code", crmCard.CardTypeId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_name", crmCard.CardTypeName);
                //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "date_valid", crmCard.);
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_discount", (crmCard.CanDisc ? "Y" : "N"));
                if (crmCard.DiscType == 1)
                    XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "rate");
                else if (crmCard.DiscType == 2)
                    XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "price");
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_voucher", (crmCard.CanOwnCoupon ? "Y" : "N"));
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cumulate_cent", (crmCard.CanCent ? "Y" : "N"));
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_th", (crmCard.CanReturn ? "Y" : "N"));
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_available", crmCard.ValidCent.ToString("f4"));
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_period", crmCard.StageCent.ToString("f4"));
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_bn", crmCard.YearCent.ToString("f4"));
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "phone", crmCard.PhoneCode);
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "hello", crmCard.Hello);
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_valuedcard", (crmCard.bCashCard ? "Y" : "N"));
            //}
            //else
            //{
            //    XmlNode vipNodeList = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "member_list");
            //    foreach (CrmVipCard card in cardList)
            //    {
            //        XmlNode vipNode = XmlUtils.AppendChildNode(respXmlDoc, vipNodeList, "member");
            //        XmlUtils.SetAttributeValue(respXmlDoc, vipNode, "id", card.CardId.ToString());
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "code", card.CardCode);
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "name", card.VipName);
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_code", card.CardTypeId.ToString());
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_name", card.CardTypeName);
            //        //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "date_valid", card.);
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_discount", (card.CanDisc ? "Y" : "N"));
            //        if (card.DiscType == 1)
            //            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "rate");
            //        else if (card.DiscType == 2)
            //            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "price");
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_voucher", (card.CanOwnCoupon ? "Y" : "N"));
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cumulate_cent", (card.CanCent ? "Y" : "N"));
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_th", (card.CanReturn ? "Y" : "N"));
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_available", card.ValidCent.ToString("f4"));
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_period", card.StageCent.ToString("f4"));
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_bn", card.YearCent.ToString("f4"));
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "phone", card.PhoneCode);
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "hello", card.Hello);
            //        XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_valuedcard", (card.bCashCard ? "Y" : "N"));
            //    }
            //}

            respXml = respXmlDoc.OuterXml;
        }

        private void GetVipCardToOfferCoupon(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int serverBillId = 0;
            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                serverBillId = int.Parse(str);

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            string msg = string.Empty;
            if (serverBillId == 0)
            {
                msg = "元素bfcrm_req/server_bill_id必须有 > 0 的整数值";
            }
            else if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素member_id或track_data, 且有值";
            }

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            CrmVipCard crmCard = null;

            bool ok = PosProc.GetVipCardToOfferCoupon(out msg, out crmCard, serverBillId, condType, condValue, cardCodeToCheck, verifyCode);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlNode vipNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "member");
            XmlUtils.SetAttributeValue(respXmlDoc, vipNode, "id", crmCard.CardId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "code", crmCard.CardCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "name", crmCard.VipName);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_code", crmCard.CardTypeId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_name", crmCard.CardTypeName);
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "date_valid", crmCard.);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_discount", (crmCard.CanDisc ? "Y" : "N"));
            if (crmCard.DiscType == 1)
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "rate");
            else if (crmCard.DiscType == 2)
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "price");
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_voucher", (crmCard.CanOwnCoupon ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cumulate_cent", (crmCard.CanCent ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_th", (crmCard.CanReturn ? "Y" : "N"));
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_available", crmCard.ValidCent.ToString("f4"));
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_period", crmCard.StageCent.ToString("f4"));
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_bn", crmCard.YearCent.ToString("f4"));
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "hello", crmCard.Hello);

            respXml = respXmlDoc.OuterXml;
        }

        private void GetArticleVipDisc(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int vipId = 0;
            int vipType = 0;
            CrmStoreInfo storeInfo = new CrmStoreInfo();
            List<CrmArticle> articleList = new List<CrmArticle>();
            string str = string.Empty;
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out str))
            {
                vipId = int.Parse(str);
            }
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_type", out str))
            {
                vipType = int.Parse(str);
            }

            string msg = string.Empty;
            if ((vipId <= 0) && (vipType <= 0))
                msg = "元素bfcrm_req/member_id 或 bfcrm_req/member_type 必须有一个是 > 0 的整数值";
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";

            if (msg.Length == 0)
            {
                XmlNode articleListNode = reqXmlRoot.SelectSingleNode("article_list");
                foreach (XmlNode articleNode in articleListNode.ChildNodes)
                {
                    CrmArticle article = new CrmArticle();
                    articleList.Add(article);
                    if (XmlUtils.GetAttributeValue(articleNode, "inx", out str))
                        article.Inx = int.Parse(str);
                    XmlUtils.GetChildTextNodeValue(articleNode, "code", out article.ArticleCode);
                    XmlUtils.GetChildTextNodeValue(articleNode, "dept_sale", out article.DeptCode);
                }
            }

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.GetArticleVipDisc(out msg, vipId, vipType, storeInfo, articleList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");

            XmlNode articleListNode2 = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "article_list");
            foreach (CrmArticle article in articleList)
            {
                XmlNode articleNode = XmlUtils.AppendChildNode(respXmlDoc, articleListNode2, "article");
                XmlUtils.SetAttributeValue(respXmlDoc, articleNode, "inx", article.Inx.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "rate_discount", article.VipDiscRate.ToString("f4"));
                XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "zkjd", article.VipDiscPrecisionType.ToString());
                if (article.VipDiscBillId > 0)
                {
                    XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "discount_bill_id", article.VipDiscBillId.ToString());
                    XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "rate_discount_multiply", article.VipMultiDiscRate.ToString("f4"));
                    switch (article.VipDiscCombinationType)
                    {
                        case 0:
                            XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "disc_comb_mode", "lower");
                            break;
                        case 1:
                            XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "disc_comb_mode", "multiply");
                            break;
                    }
                }
            }

            respXml = respXmlDoc.OuterXml;
        }

        private void ChangeCardPassword(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            string oldPassword = string.Empty;
            string newPassword = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str))
                condType = int.Parse(str);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "old_password", out oldPassword);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "new_password", out newPassword);

            string msg = string.Empty;
            if (condValue.Length == 0)
                msg = "元素bfcrm_req必须有子元素cond_value, 且有值";
            else if (oldPassword.Length == 0)
                msg = "元素bfcrm_req必须有子元素old_password, 且有值";
            else if (newPassword.Length == 0)
                msg = "元素bfcrm_req必须有子元素new_password, 且有值";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.ChangeCardPassowrd(out msg, condType, condValue, cardCodeToCheck, verifyCode, oldPassword, newPassword);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            respXml = respXmlDoc.OuterXml;
        }

        private void GetCashCard(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            string msg = string.Empty;
            int shoppingAreaId = 0;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "shopping_area_id", out str))    //本请求由其它区域的服务器发来
                shoppingAreaId = int.Parse(str);

            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            string password = string.Empty;
            bool isCoupon = false;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_password", out password);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "is_coupon", out str))
            {
                isCoupon = (str.Equals("Y") || str.Equals("y"));
            }

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);

            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素track_data, 且有值";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            CrmCashCard crmCard = null;

            int cardAreaId = 0;
            bool ok = PosProc.GetCashCard(out msg, out cardAreaId, out crmCard, condType, condValue, cardCodeToCheck, verifyCode, password, isCoupon, storeInfo);
            if (!ok)
            {
                if ((shoppingAreaId == 0) && (cardAreaId > 0) && (cardAreaId != CrmServerPlatform.CurrentDbId))
                {
                    CrmAreaInfo areaInfo = CrmServerPlatform.GetAreaInfo(out msg, cardAreaId);
                    if (areaInfo != null)
                    {
                        XmlUtils.SetAttributeValue(reqXmlDoc, reqXmlRoot, "app", AppPrepareTransCashCard);
                        XmlUtils.AppendChildTextNode(reqXmlDoc, reqXmlRoot, "shopping_area_id", CrmServerPlatform.CurrentDbId.ToString());
                        DoRequestToAnotherServer(areaInfo.ServiceUrl, AppGetCashCard, reqXmlDoc.OuterXml, out respXml);
                        return;
                    }
                }
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_id", crmCard.CardId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_code", crmCard.CardCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "balance", crmCard.Balance.ToString("f2"));
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "bottom", crmCard.Bottom.ToString("f2"));
            if (crmCard.ValidDate.CompareTo(DateTime.MinValue) > 0)
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "date_valid", crmCard.ValidDate.ToString("yyyy-MM-dd"));
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "hyktype", crmCard.CardTypeId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "permit_th", (crmCard.CanReturn ? "Y" : "N"));
            if ((shoppingAreaId > 0) && (shoppingAreaId != CrmServerPlatform.CurrentDbId))
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "card_area_id", CrmServerPlatform.CurrentDbId.ToString());
            respXml = respXmlDoc.OuterXml;

        }

        private void GetVipCoupon(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            bool requireValidDate = false;
            int serverBillId = 0;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "require_valid_date", out str))
                requireValidDate = (str.Equals("Y"));

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                serverBillId = int.Parse(str);

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);

            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素track_data, 且有值";
            }
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int vipId = 0;
            string vipCode = string.Empty;
            List<CrmCoupon> couponList = new List<CrmCoupon>();
            List<CrmCouponPayLimit> payLimitList = null;// new List<CrmCouponPayLimit>();

            bool ok = false;
            if (serverBillId > 0)
            {
                payLimitList = new List<CrmCouponPayLimit>();
                ok = PosProc.GetVipCouponToPay(out msg, out vipId, out vipCode, couponList, payLimitList, condType, condValue, cardCodeToCheck, verifyCode, storeInfo, serverBillId);
            }
            else
                ok = PosProc.GetVipCoupon(out msg, out vipId, out vipCode, couponList, condType, condValue, cardCodeToCheck, verifyCode, storeInfo, requireValidDate);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_id", vipId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_code", vipCode);

            if (serverBillId > 0)
            {
                double totalLimitMoney = 0;
                double limitMoney = 0;
                XmlNode couponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "voucher_list");
                foreach (CrmCoupon coupon in couponList)
                {
                    XmlNode couponNode = XmlUtils.AppendChildNode(respXmlDoc, couponListNode, "voucher");
                    XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "id", coupon.CouponType.ToString());
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "name", coupon.CouponTypeName);
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "balance", coupon.Balance.ToString("f2"));
                    limitMoney = 0;
                    foreach (CrmCouponPayLimit payLimit in payLimitList)
                    {
                        if (payLimit.CouponType == coupon.CouponType)
                        {
                            if (MathUtils.DoubleAGreaterThanDoubleB(coupon.Balance, payLimit.LimitMoney))
                                limitMoney = payLimit.LimitMoney;
                            else
                                limitMoney = coupon.Balance;
                            break;
                        }
                    }
                    totalLimitMoney = totalLimitMoney + limitMoney;
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "payable", limitMoney.ToString("f2"));
                    if (coupon.SpecialType != 0)
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "special_type", coupon.SpecialType.ToString());
                    if (coupon.SpecialType == 3)
                    {
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "exchange_cent", coupon.ExchangeCent.ToString("f4"));
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "exchange_money", coupon.ExchangeMoney.ToString("f2"));
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "payable_cent", (coupon.ExchangeCent * MathUtils.Truncate(limitMoney / coupon.ExchangeMoney)).ToString("f4"));
                    }
                }
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "payable_self_member", totalLimitMoney.ToString("f2"));

                XmlNode payLimitListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "payable_voucher_list");
                totalLimitMoney = 0;
                foreach (CrmCouponPayLimit payLimit in payLimitList)
                {
                    XmlNode payLimitNode = XmlUtils.AppendChildNode(respXmlDoc, payLimitListNode, "voucher");
                    XmlUtils.SetAttributeValue(respXmlDoc, payLimitNode, "id", payLimit.CouponType.ToString());
                    foreach (CrmCoupon coupon in couponList)
                    {
                        if (coupon.CouponType == payLimit.CouponType)
                        {
                            XmlUtils.AppendChildTextNode(respXmlDoc, payLimitNode, "name", coupon.CouponTypeName);
                            break;
                        }
                    }
                    XmlUtils.AppendChildTextNode(respXmlDoc, payLimitNode, "payable", payLimit.LimitMoney.ToString("f2"));
                    totalLimitMoney = totalLimitMoney + payLimit.LimitMoney;
                }
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "payable_all_member", totalLimitMoney.ToString("f2"));

            }
            else
            {
                XmlNode couponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "voucher_list");
                foreach (CrmCoupon coupon in couponList)
                {
                    XmlNode couponNode = XmlUtils.AppendChildNode(respXmlDoc, couponListNode, "voucher");
                    XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "id", coupon.CouponType.ToString());
                    if (requireValidDate)
                        XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "date_valid", coupon.ValidDate.ToString("yyyy-MM-dd"));
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "name", coupon.CouponTypeName);
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "balance", coupon.Balance.ToString("f2"));
                }
            }
            respXml = respXmlDoc.OuterXml;
        }

        private void GetVipCouponToPay(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            int serverBillId = 0;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                serverBillId = int.Parse(str);

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);

            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素track_data, 且有值";
            }
            else if (serverBillId <= 0)
            {
                msg = "元素bfcrm_req/server_bill_id的值必须是 > 0 的整数";
            }
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int vipId = 0;
            string vipCode = string.Empty;
            List<CrmCoupon> couponList = new List<CrmCoupon>();
            List<CrmCouponPayLimit> payLimitList = new List<CrmCouponPayLimit>();

            bool ok = PosProc.GetVipCouponToPay(out msg, out vipId, out vipCode, couponList, payLimitList, condType, condValue, cardCodeToCheck, verifyCode, storeInfo, serverBillId);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_id", vipId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_code", vipCode);
            XmlNode couponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "voucher_list");
            double totalLimitMoney = 0;
            double limitMoney = 0;
            foreach (CrmCoupon coupon in couponList)
            {
                XmlNode couponNode = XmlUtils.AppendChildNode(respXmlDoc, couponListNode, "voucher");
                XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "id", coupon.CouponType.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "name", coupon.CouponTypeName);
                XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "balance", coupon.Balance.ToString("f2"));
                limitMoney = 0;
                foreach (CrmCouponPayLimit payLimit in payLimitList)
                {
                    if (payLimit.CouponType == coupon.CouponType)
                    {
                        if (MathUtils.DoubleAGreaterThanDoubleB(coupon.Balance, payLimit.LimitMoney))
                            limitMoney = payLimit.LimitMoney;
                        else
                            limitMoney = coupon.Balance;
                        break;
                    }
                }
                totalLimitMoney = totalLimitMoney + limitMoney;
                XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "payable", limitMoney.ToString("f2"));
                if (coupon.SpecialType != 0)
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "special_type", coupon.SpecialType.ToString());
                if (coupon.SpecialType == 3)
                {
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "exchange_cent", coupon.ExchangeCent.ToString("f4"));
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "exchange_money", coupon.ExchangeMoney.ToString("f2"));
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "payable_cent", (coupon.ExchangeCent * MathUtils.Truncate(limitMoney / coupon.ExchangeMoney)).ToString("f4"));
                }
            }
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "payable_self_member", totalLimitMoney.ToString("f2"));

            XmlNode payLimitListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "payable_voucher_list");
            totalLimitMoney = 0;
            foreach (CrmCouponPayLimit payLimit in payLimitList)
            {
                XmlNode payLimitNode = XmlUtils.AppendChildNode(respXmlDoc, couponListNode, "voucher");
                XmlUtils.SetAttributeValue(respXmlDoc, payLimitNode, "id", payLimit.CouponType.ToString());
                foreach (CrmCoupon coupon in couponList)
                {
                    if (coupon.CouponType == payLimit.CouponType)
                    {
                        XmlUtils.AppendChildTextNode(respXmlDoc, payLimitNode, "name", coupon.CouponTypeName);
                        break;
                    }
                }
                XmlUtils.AppendChildTextNode(respXmlDoc, payLimitNode, "payable", payLimit.LimitMoney.ToString("f2"));
                totalLimitMoney = totalLimitMoney + payLimit.LimitMoney;
            }
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "payable_all_member", totalLimitMoney.ToString("f2"));
            respXml = respXmlDoc.OuterXml;
        }

        private void GetVipOfferBackDifference(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            //bool requireValidDate = false;
            int billId = 0;
            string posId = string.Empty;
            string str = string.Empty;
            CrmStoreInfo storeInfo = new CrmStoreInfo();

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id", out str))
            {
                billId = int.Parse(str);
                if (!XmlUtils.GetChildTextNodeValue(reqXmlRoot, "machine", out posId))
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out posId);
            }
            else
            {
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
            {
                if (posId.Length == 0)
                    GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out posId);
                else
                    GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);
            }

            string msg = string.Empty;
            if ((billId <= 0) && (condValue.Length == 0))
            {
                msg = "元素bfcrm_req必须有子元素track_data或bill_id, 且有值";
            }

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int vipId = 0;
            string vipCode = string.Empty;
            int promId = 0;
            string promName = string.Empty;
            List<CrmCoupon> couponList = new List<CrmCoupon>();

            bool ok = PosProc.GetVipOfferBackDifference(out msg, out vipId, out vipCode, out promId, out promName, couponList, condType, condValue, cardCodeToCheck, verifyCode, storeInfo, posId, billId);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_id", vipId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_code", vipCode);

            if (promId > 0)
            {
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "promotion_id", promId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "promotion_name", promName);
                XmlNode couponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "voucher_list");
                foreach (CrmCoupon coupon in couponList)
                {
                    XmlNode couponNode = XmlUtils.AppendChildNode(respXmlDoc, couponListNode, "voucher");
                    XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "id", coupon.CouponType.ToString());
                    XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "date_valid", coupon.ValidDate.ToString("yyyy-MM-dd"));
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "name", coupon.CouponTypeName);
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "cash_to_voucher", coupon.OfferBackDifference.ToString("f2"));
                    XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "balance", coupon.Balance.ToString("f2"));
                }
            }
            respXml = respXmlDoc.OuterXml;
        }

        private void PrepareTransCashCard(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;
            string str = string.Empty;
            string msg = string.Empty;
            int shoppingAreaId = 0;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "shopping_area_id", out str))    //本请求由其它区域的服务器发来
                shoppingAreaId = int.Parse(str);
            if ((shoppingAreaId == 0) && (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_area_id", out str)))
            {
                int cardAreaId = int.Parse(str);
                if (cardAreaId != CrmServerPlatform.CurrentDbId)    //发卡区域不是本区域
                {
                    CrmAreaInfo areaInfo = CrmServerPlatform.GetAreaInfo(out msg, cardAreaId);
                    if (areaInfo != null)
                    {
                        XmlUtils.SetAttributeValue(reqXmlDoc, reqXmlRoot, "app", AppPrepareTransCashCard);
                        XmlUtils.AppendChildTextNode(reqXmlDoc, reqXmlRoot, "shopping_area_id", CrmServerPlatform.CurrentDbId.ToString());
                        DoRequestToAnotherServer(areaInfo.ServiceUrl, AppPrepareTransCashCard, reqXmlDoc.OuterXml, out respXml);
                        return;
                    }
                    else
                    {
                        respXml = SpellFailXml("logic", msg);
                        return;
                    }
                }
            }

            int transId = 0;
            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
            List<CrmCashCardPayment> paymentList = new List<CrmCashCardPayment>();

            billHead.AreaId = shoppingAreaId;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                billHead.ServerBillId = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id", out str))
            {
                billHead.BillId = int.Parse(str);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out billHead.StoreCode);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out billHead.PosId);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier", out billHead.Cashier);
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "date_account", out str))
                {
                    billHead.AccountDate = FormatUtils.ParseDateString(str);
                }
                if (billHead.StoreCode.Length == 0)
                {
                    CrmStoreInfo storeInfo = new CrmStoreInfo();
                    if (GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out billHead.PosId))
                    {
                        billHead.CompanyCode = storeInfo.Company;
                        billHead.StoreCode = storeInfo.StoreCode;
                        billHead.StoreId = storeInfo.StoreId;
                    }
                }
            }
            if (billHead.AreaId > 0)
            {
                if (billHead.BillId <= 0)
                    msg = "异地卡交易时，元素bfcrm_req/bill_id 必须有 大于 0 的整数值";
            }
            else if (billHead.ServerBillId <= 0)
            {
                if (billHead.BillId <= 0)
                    msg = "元素bfcrm_req/server_bill_id 或 bfcrm_req/bill_id 必须有一个是 大于 0 的整数值";
            }
            if (billHead.BillId > 0)
            {
                if (billHead.StoreCode.Length == 0)
                    msg = "门店代码 没传 ";
                else if (billHead.PosId.Length == 0)
                    msg = "收款台号 没传 ";
                else if (billHead.AccountDate == DateTime.MinValue)
                    msg = "元素bfcrm_req/date_account 必须有值，且日期值格式为：yyyy-mm-dd";
            }

            if (msg.Length == 0)
            {
                XmlNode cardListNode = reqXmlRoot.SelectSingleNode("card_list");
                if (cardListNode != null)
                {
                    foreach (XmlNode node in cardListNode.ChildNodes)
                    {
                        int vipId = 0;
                        double payMoney = 0;
                        if (XmlUtils.GetAttributeValue(node, "member_id", out str))
                            vipId = int.Parse(str);
                        if (XmlUtils.GetChildTextNodeValue(node, "amount", out str))
                            payMoney = double.Parse(str);
                        if ((vipId != 0) && (!MathUtils.DoubleAEuqalToDoubleB(payMoney, 0)))
                        {
                            CrmCashCardPayment payment = new CrmCashCardPayment();
                            paymentList.Add(payment);
                            payment.CardId = vipId;
                            payment.PayMoney = payMoney;
                            if (XmlUtils.GetChildTextNodeValue(node, "recycle", out str))
                                payment.Recycle = (str.Equals("Y"));
                        }
                    }
                }
                if (paymentList.Count == 0)
                    msg = "对元素bfcrm_req/card_list：无有数据的子元素";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.PrepareTransCashCardPayment(out msg, out transId, billHead, paymentList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_transaction_id", transId.ToString());
            respXml = respXmlDoc.OuterXml;
        }

        private void PrepareTransCoupon(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int transType = 0;
            int transId = 0;
            int vipIdTo = 0;
            int promId = 0;
            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
            List<CrmCouponPayment> paymentList = new List<CrmCouponPayment>();

            string msg = string.Empty;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "type", out str))
            {
                if (str.Equals("pay"))
                    transType = CrmPosData.TransTypePayment;
                else if (str.Equals("transfer"))
                    transType = CrmPosData.TransTypeTransfer;
                else if (str.Equals("deduct_grant"))
                    transType = CrmPosData.TransTypeOfferBack;
                else if (str.Equals("voucher2cash"))
                    transType = CrmPosData.TransTypeCoupon2Cash;
                else
                    msg = "元素bfcrm_req/type的值必须是 pay 或 transfer 或 deduct_grant 或 voucher2cash";
            }
            else
                msg = "元素bfcrm_req/type必须有";

            if (transType == CrmPosData.TransTypeOfferBack)
                msg = "目前优惠券不支持交易 deduct_grant";

            if (msg.Length == 0)
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                    billHead.ServerBillId = int.Parse(str);
                else
                {
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id", out str))
                    {
                        billHead.BillId = int.Parse(str);
                        XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out billHead.StoreCode);
                        XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out billHead.PosId);
                        XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier", out billHead.Cashier);
                        if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "date_account", out str))
                        {
                            billHead.AccountDate = FormatUtils.ParseDateString(str);
                        }
                    }
                    else
                    {
                        XmlNode billNode = reqXmlRoot.SelectSingleNode("bill");
                        if (billNode != null)
                        {
                            if (XmlUtils.GetChildTextNodeValue(billNode, "bill_id", out str))
                                billHead.BillId = int.Parse(str);
                            XmlUtils.GetChildTextNodeValue(billNode, "cashier", out billHead.Cashier);
                            if (XmlUtils.GetChildTextNodeValue(billNode, "time_shopping", out str))
                                billHead.SaleTime = FormatUtils.ParseDatetimeString(str);
                            if (XmlUtils.GetChildTextNodeValue(billNode, "date_account", out str))
                                billHead.AccountDate = FormatUtils.ParseDateString(str);
                            XmlUtils.GetChildTextNodeValue(billNode, "store_code", out billHead.StoreCode);
                            XmlUtils.GetChildTextNodeValue(billNode, "pos_id", out billHead.PosId);
                        }
                    }
                    if (billHead.BillId > 0)
                    {
                        if (billHead.StoreCode.Length == 0)
                        {
                            CrmStoreInfo storeInfo = new CrmStoreInfo();
                            if (GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out billHead.PosId))
                            {
                                billHead.CompanyCode = storeInfo.Company;
                                billHead.StoreCode = storeInfo.StoreCode;
                                billHead.StoreId = storeInfo.StoreId;
                            }
                        }
                    }
                }
                if (transType == CrmPosData.TransTypeTransfer)
                {
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id_to", out str))
                        vipIdTo = int.Parse(str);
                    if (vipIdTo <= 0)
                        msg = "元素bfcrm_req/member_id_to必须有 > 0 的整数值";
                }
                else if (transType == CrmPosData.TransTypeCoupon2Cash)
                {
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "promotion_id", out str))
                        promId = int.Parse(str);
                    if (promId <= 0)
                        msg = "元素bfcrm_req/promotion_id必须有 > 0 的整数值";
                }
                if ((msg.Length == 0) && (billHead.ServerBillId <= 0))
                {
                    if (billHead.BillId <= 0)
                        msg = "元素bfcrm_req/server_bill_id 或 bfcrm_req/bill_id 必须有一个是 > 0 的整数值";
                    else if (billHead.StoreCode.Length == 0)
                        msg = "store_code = ''";
                    else if (billHead.PosId.Length == 0)
                        msg = "pos_id = ''";
                    else if ((billHead.BillId > 0) && (billHead.AccountDate == DateTime.MinValue))
                        msg = "元素bfcrm_req/date_account 必须有值，且日期值格式为：yyyy-mm-dd";
                }
            }

            if (msg.Length == 0)
            {
                XmlNode cardListNode = reqXmlRoot.SelectSingleNode("card_list");
                if (cardListNode != null)
                {
                    foreach (XmlNode cardNode in cardListNode.ChildNodes)
                    {
                        int vipId = 0;
                        int couponType = 0; ;
                        double payMoney = 0;
                        DateTime validDate = DateTime.MinValue;
                        if (XmlUtils.GetAttributeValue(cardNode, "member_id", out str))
                        {
                            vipId = int.Parse(str);
                            XmlNode couponListNode = cardNode.SelectSingleNode("voucher_list");
                            foreach (XmlNode couponNode in couponListNode.ChildNodes)
                            {
                                if (XmlUtils.GetAttributeValue(couponNode, "id", out str))
                                    couponType = int.Parse(str);
                                else
                                    couponType = -1;
                                if (XmlUtils.GetAttributeValue(couponNode, "date_valid", out str))
                                    validDate = FormatUtils.ParseDateString(str);
                                else
                                    validDate = DateTime.MinValue;
                                if (XmlUtils.GetChildTextNodeValue(couponNode, "amount", out str))
                                    payMoney = double.Parse(str);
                                else
                                    payMoney = 0;

                                if ((vipId > 0) && (couponType >= 0) && (!MathUtils.DoubleAEuqalToDoubleB(payMoney, 0)))
                                {
                                    CrmCouponPayment payment = new CrmCouponPayment();
                                    paymentList.Add(payment);
                                    payment.VipId = vipId;
                                    payment.ValidDate = validDate;
                                    payment.CouponType = couponType;
                                    payment.PayMoney = payMoney;
                                }
                            }
                        }
                    }
                }
                if (paymentList.Count == 0)
                    msg = "对元素bfcrm_req/card_list：无有数据的子元素";
                else if (transType != CrmPosData.TransTypePayment)
                {
                    foreach (CrmCouponPayment payment in paymentList)
                    {
                        if (transType == CrmPosData.TransTypeTransfer)
                        {
                            if (payment.VipId == vipIdTo)
                            {
                                msg = "转储时转出卡不能是转入卡";
                                break;
                            }
                        }
                        if (MathUtils.DoubleASmallerThanDoubleB(payment.PayMoney, 0))
                        {
                            msg = "有金额 < 0";
                            break;
                        }
                        if (payment.ValidDate == DateTime.MinValue)
                        {
                            msg = "没有券的有效期";
                            break;
                        }
                    }
                }
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = false;
            double payCent = 0;
            switch (transType)
            {
                case CrmPosData.TransTypePayment:
                    ok = PosProc.PrepareTransCouponPayment(out msg, out transId, out payCent, billHead, paymentList);
                    break;
                case CrmPosData.TransTypeTransfer:
                    ok = PosProc.PrepareTransCouponTransfer(out msg, out transId, vipIdTo, billHead, paymentList);
                    break;
                case CrmPosData.TransTypeOfferBack:
                    //ok = PosProc.PrepareTransCouponPayment(out msg, out transId, billHead, paymentList);
                    break;
                case CrmPosData.TransTypeCoupon2Cash:
                    ok = PosProc.PrepareTransCoupon2Cash(out msg, out transId, promId, billHead, paymentList);
                    break;
                default:
                    msg = "No support";
                    break;
            }
            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_transaction_id", transId.ToString());
            if ((transType == CrmPosData.TransTypePayment) && MathUtils.DoubleAGreaterThanDoubleB(payCent, 0))
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "pay_cent", payCent.ToString("f4"));
            respXml = respXmlDoc.OuterXml;
        }

        private void ProcTrans(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            string msg = string.Empty;
            int shoppingAreaId = 0;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "shopping_area_id", out str))    //本请求由其它区域的服务器发来
                shoppingAreaId = int.Parse(str);
            if ((shoppingAreaId == 0) && (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_area_id", out str)))
            {
                int cardAreaId = int.Parse(str);
                if (cardAreaId != CrmServerPlatform.CurrentDbId)    //发卡区域不是本区域
                {
                    CrmAreaInfo areaInfo = CrmServerPlatform.GetAreaInfo(out msg, cardAreaId);
                    if (areaInfo != null)
                    {
                        XmlUtils.SetAttributeValue(reqXmlDoc, reqXmlRoot, "app", AppProcTrans);
                        XmlUtils.AppendChildTextNode(reqXmlDoc, reqXmlRoot, "shopping_area_id", CrmServerPlatform.CurrentDbId.ToString());
                        DoRequestToAnotherServer(areaInfo.ServiceUrl, AppProcTrans, reqXmlDoc.OuterXml, out respXml);
                        return;
                    }
                    else
                    {
                        respXml = SpellFailXml("logic", msg);
                        return;
                    }
                }
            }

            string step = string.Empty;
            int transId = 0;
            double totalMoney = 0;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "step", out step);
            if (step.Equals("confirm") || (step.Equals("cancel")))
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_transaction_id", out str))
                    transId = int.Parse(str);
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "total_amount", out str))
                    totalMoney = double.Parse(str);
                if (transId <= 0)
                    msg = "元素bfcrm_req/server_transaction_id的值必须是 大于 0 的整数";
                else if (MathUtils.DoubleAEuqalToDoubleB(totalMoney, 0))
                    msg = "元素bfcrm_req/total_amount的值必须是 不等于 0 的金额";
            }
            else
                msg = "元素bfcrm_req/step的值必须是 confirm 或 cancel";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int transStatus = 0;
            int serverBillId = 0;
            int transType = 0;
            double transMoney = 0;
            bool isCashCard = false;
            bool isVipCoupon = false;
            bool isCodedCoupon = false;
            DbConnection conn = DbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();
            try
            {
                sql.Append("select XFJLID,JYLX,JYJE,JYZT,BJ_CZK,BJ_YHQ,HYID_TO,CXID from HYK_JYCL where JYID = ").Append(transId);
                cmd.CommandText = sql.ToString();
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
                        serverBillId = DbUtils.GetInt(reader, 0);
                        transType = DbUtils.GetInt(reader, 1);
                        transMoney = DbUtils.GetDouble(reader, 2);
                        transStatus = DbUtils.GetInt(reader, 3);
                        isCashCard = DbUtils.GetBool(reader, 4);
                        int couponFlag = DbUtils.GetInt(reader, 5);
                        isVipCoupon = (couponFlag == 1);
                        isCodedCoupon = (couponFlag == 2);
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
            bool ok = true;
            if (step.Equals("confirm") && (transStatus == 1))
            {
                if (transType == CrmPosData.TransTypePayment) //支付交易
                {
                    if (isCashCard)
                        ok = PosProc.ConfirmTransCashCardPayment(out msg, transId, serverBillId, totalMoney);
                    if (isVipCoupon)
                        ok = PosProc.ConfirmTransCouponPayment(out msg, transId, serverBillId, totalMoney);
                    else if (isCodedCoupon)
                        ok = PosProc.ConfirmTransCodedCouponPayment(out msg, transId, serverBillId, totalMoney);
                }
                else if ((isVipCoupon) && (transType == CrmPosData.TransTypeTransfer)) //转储交易
                {
                    ok = PosProc.ConfirmTransCouponTransfer(out msg, transId, serverBillId, totalMoney);
                }
                else if ((isVipCoupon) && (transType == CrmPosData.TransTypeCoupon2Cash)) //取消钱买券交易
                {
                    ok = PosProc.ConfirmTransCoupon2Cash(out msg, transId, serverBillId, totalMoney);
                }
                else if (transType == CrmPosData.TransTypeSaveChange) //零钱包交易
                {
                    ok = PosProc.ConfirmTransSaveChangeIntoVipCard(out msg, transId, serverBillId, totalMoney);
                }
                else
                {
                    ok = false;
                    msg = "No support";
                }
            }
            else if (step.Equals("cancel") && ((transStatus == 1) || (transStatus == 2)))
            {
                if (transType == CrmPosData.TransTypePayment) //支付交易
                {
                    if (isCashCard)
                        ok = PosProc.CancelTransCashCardPayment(out msg, transId, serverBillId, totalMoney);
                    if (isVipCoupon)
                        ok = PosProc.CancelTransCouponPayment(out msg, transId, serverBillId, totalMoney);
                    else if (isCodedCoupon)
                        ok = PosProc.CancelTransCodedCouponPayment(out msg, transId, serverBillId, totalMoney);
                }
                else if ((isVipCoupon) && (transType == CrmPosData.TransTypeTransfer)) //转储交易
                {
                    ok = PosProc.CancelTransCouponTransfer(out msg, transId, serverBillId, totalMoney);
                }
                else if ((isVipCoupon) && (transType == CrmPosData.TransTypeCoupon2Cash)) //取消钱买券交易
                {
                    ok = PosProc.CancelTransCoupon2Cash(out msg, transId, serverBillId, totalMoney);
                }
                else if (transType == CrmPosData.TransTypeSaveChange) //零钱包交易
                {
                    ok = PosProc.CancelTransSaveChangeIntoVipCard(out msg, transId, serverBillId, totalMoney);
                }
                else
                {
                    ok = false;
                    msg = "No support";
                }
            }

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            string transStatusStr = string.Empty;
            switch (transStatus)
            {
                case 1:
                    transStatusStr = "prepared";
                    break;
                case 2:
                    transStatusStr = "confirmed";
                    break;
                case 3:
                    transStatusStr = "rolledback";
                    break;
                case 4:
                    transStatusStr = "canceled";
                    break;
                default:
                    transStatusStr = "not_exist";
                    break;
            }
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "status_before_perform", transStatusStr);
            respXml = respXmlDoc.OuterXml;
        }

        private void SaveRSaleBillArticles(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
            billHead.BillType = -1;
            billHead.TotalSaleMoney = 0;
            List<CrmArticle> articleList = new List<CrmArticle>();

            string msg = string.Empty;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out str))
                billHead.VipId = int.Parse(str);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_code", out billHead.VipCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out billHead.StoreCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out billHead.PosId);

            XmlNode billNode = reqXmlRoot.SelectSingleNode("bill");
            if (billNode != null)
            {
                if (XmlUtils.GetAttributeValue(billNode, "id", out str))
                    billHead.BillId = int.Parse(str);
                if (billHead.BillId <= 0)
                    msg = "元素bfcrm_req/bill的属性id必须有 > 0 的整数值";
                else
                {
                    XmlNode headNode = billNode.SelectSingleNode("head");
                    if (headNode != null)
                    {
                        if (XmlUtils.GetChildTextNodeValue(headNode, "type", out str))
                        {
                            if (str.Equals("sale"))
                                billHead.BillType = CrmPosData.BillTypeSale;
                            else if (str.Equals("return"))
                                billHead.BillType = CrmPosData.BillTypeReturn;
                            else if (str.Equals("exchange"))
                                billHead.BillType = CrmPosData.BillTypeExchange;
                            else
                                msg = "元素bfcrm_req/bill/head/type的值必须是 sale 或 return 或 exchange";
                        }
                        else
                            msg = "元素bfcrm_req/bill/head/type必须有";
                        if (msg.Length == 0)
                        {
                            if (billHead.BillType != 0)     //退货或换货
                            {
                                XmlUtils.GetChildTextNodeValue(headNode, "machine_old", out billHead.OriginalPosId);
                                if (XmlUtils.GetChildTextNodeValue(headNode, "bill_id_old", out str))
                                    billHead.OriginalBillId = int.Parse(str);
                                if (billHead.BillType == 2) //换货肯定是选单的
                                {
                                    if ((billHead.OriginalPosId.Length == 0) || (billHead.OriginalBillId <= 0))
                                        msg = "元素bfcrm_req/bill/head/machine_old必须有值, 元素bfcrm_req/bill/head/bill_id_old必须有 > 0 的整数值";
                                }
                            }
                            XmlUtils.GetChildTextNodeValue(headNode, "cashier", out billHead.Cashier);
                            if (XmlUtils.GetChildTextNodeValue(headNode, "time_shopping", out str))
                                billHead.SaleTime = FormatUtils.ParseDatetimeString(str);
                            if (XmlUtils.GetChildTextNodeValue(headNode, "date_account", out str))
                                billHead.AccountDate = FormatUtils.ParseDateString(str);

                            //if (billHead.SaleTime == DateTime.MinValue)
                            //    msg = "元素bfcrm_req/bill/head/time_shopping必须有值，格式为：yyyy-mm-dd hh:nn:ss";
                            //else if (billHead.AccountDate == DateTime.MinValue)
                            //    msg = "元素bfcrm_req/bill/head/date_account必须有值，格式为：yyyy-mm-dd";
                        }
                    }
                    if (msg.Length == 0)
                    {
                        XmlNode articleListNode = billNode.SelectSingleNode("article_list");
                        if (articleListNode != null)
                        {
                            foreach (XmlNode articleNode in articleListNode.ChildNodes)
                            {
                                CrmArticle article = new CrmArticle();
                                articleList.Add(article);
                                if (XmlUtils.GetAttributeValue(articleNode, "inx", out str))
                                    article.Inx = int.Parse(str);
                                if (XmlUtils.GetAttributeValue(articleNode, "inx_old", out str))
                                    article.OriginalInx = int.Parse(str);
                                XmlUtils.GetChildTextNodeValue(articleNode, "code", out article.ArticleCode);
                                XmlUtils.GetChildTextNodeValue(articleNode, "dept_sale", out article.DeptCode);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "quantity", out str))
                                    article.SaleNum = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "amount", out str))
                                    article.SaleMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "amount_discount", out str))
                                    article.DiscMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "amount_discount_member", out str))
                                    article.VipDiscMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "no_cent", out str))
                                    article.IsNoCent = (str.Equals("Y"));
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "bj_bcjhd", out str))
                                    article.IsNoProm = (str.Equals("Y"));
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "discount_bill_id", out str))
                                    article.VipDiscBillId = int.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "rate_discount", out str))
                                    article.VipDiscRate = double.Parse(str);

                                billHead.TotalSaleMoney = billHead.TotalSaleMoney + article.SaleMoney;
                            }
                            if (articleList.Count == 0)
                                msg = "对元素bfcrm_req/bill/article_list：' + #13#10 + '    无有数据的子元素";
                        }
                    }
                }
            }

            if (billHead.StoreCode.Length == 0)
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                if (GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out billHead.PosId))
                {
                    billHead.CompanyCode = storeInfo.Company;
                    billHead.StoreCode = storeInfo.StoreCode;
                    billHead.StoreId = storeInfo.StoreId;
                }
                else
                    msg = "session_id 错误";
            }

            if (msg.Length == 0)
            {
                if ((billHead.BillType == 0) && (MathUtils.DoubleASmallerThanDoubleB(billHead.TotalSaleMoney, 0)))
                    msg = "是销售单, 但总金额 < 0";
                else if ((billHead.BillType != 0) && (MathUtils.DoubleAGreaterThanDoubleB(billHead.TotalSaleMoney, 0)))
                    msg = "是退换货单, 但总金额 > 0";
                else if (billHead.StoreCode.Length == 0)
                    msg = "store_code = ''";
                else if (billHead.PosId.Length == 0)
                    msg = "pos_id = ''";
            }

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            List<CrmCouponPayment> payBackCouponList = null;
            List<CrmPromOfferCoupon> offerBackCouponList = null;
            if (billHead.OriginalBillId > 0)
            {
                payBackCouponList = new List<CrmCouponPayment>();
                offerBackCouponList = new List<CrmPromOfferCoupon>();
            }
            bool ok = PosProc.SaveRSaleBillArticles(out msg, billHead, articleList, payBackCouponList, offerBackCouponList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_bill_id", billHead.ServerBillId.ToString());
            if (billHead.BillType == CrmPosData.BillTypeSale)
            {
                if (MathUtils.DoubleAGreaterThanDoubleB(billHead.TotalDecMoney, 0))
                {
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "dec_sum", billHead.TotalDecMoney.ToString("f2"));
                    XmlNode decListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "article_dec_list");
                    foreach (CrmArticle article in articleList)
                    {
                        if (!MathUtils.DoubleAEuqalToDoubleB(article.DecMoney, 0))
                        {
                            XmlNode decNode = XmlUtils.AppendChildNode(respXmlDoc, decListNode, "article_dec");
                            XmlUtils.AppendChildTextNode(respXmlDoc, decNode, "article_inx", article.Inx.ToString());
                            XmlUtils.AppendChildTextNode(respXmlDoc, decNode, "dec_share", article.DecMoney.ToString("f2"));
                            if (article.DecMoneyIsExpense)
                                XmlUtils.AppendChildTextNode(respXmlDoc, decNode, "is_expense", "Y");
                        }
                    }
                }
                XmlNode promFlagListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "article_prom_flag_list");
                foreach (CrmArticle article in articleList)
                {
                    XmlNode promFlagNode = XmlUtils.AppendChildNode(respXmlDoc, promFlagListNode, "prom_flag");
                    XmlUtils.SetAttributeValue(respXmlDoc, promFlagNode, "article_inx", article.Inx.ToString());
                    if (article.JoinOfferCoupon)
                        XmlUtils.SetAttributeValue(respXmlDoc, promFlagNode, "grant_voucher", "Y");
                    if (article.JoinPromCent)
                        XmlUtils.SetAttributeValue(respXmlDoc, promFlagNode, "vip_cent", "Y");
                    if (article.DecMoneyRuleId > 0)
                        XmlUtils.SetAttributeValue(respXmlDoc, promFlagNode, "dec_m", "Y");
                }
            }
            if (billHead.OriginalBillId > 0)
            {
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_bill_id_old", billHead.OriginalServerBillId.ToString());
                if (billHead.OriginalServerBillId > 0)
                {
                    if (billHead.VipId > 0)
                    {
                        XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_code_cent", billHead.VipCode);
                        XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "thjf", billHead.TotalGainedCent.ToString());
                    }
                    if ((payBackCouponList != null) && (payBackCouponList.Count > 0))
                    {
                        XmlNode listNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "pay_voucher_list");
                        XmlNode cardNode = XmlUtils.AppendChildNode(respXmlDoc, listNode, "card");
                        foreach (CrmCouponPayment payBackCoupon in payBackCouponList)
                        {
                            XmlNode couponNode = XmlUtils.AppendChildNode(respXmlDoc, cardNode, "voucher");
                            XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "id", payBackCoupon.CouponType.ToString());
                            XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "name", payBackCoupon.CouponTypeName);
                            XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "amount", payBackCoupon.PayMoney.ToString("f2"));
                            if (!MathUtils.DoubleAEuqalToDoubleB(payBackCoupon.PayCent, 0))
                                XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "pay_cent", payBackCoupon.PayCent.ToString("f4"));
                        }
                    }
                    if ((offerBackCouponList != null) && (offerBackCouponList.Count > 0))
                    {
                        XmlNode offerCouponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "grant_voucher_list");
                        XmlUtils.SetAttributeValue(respXmlDoc, offerCouponListNode, "member_code", "");
                        foreach (CrmPromOfferCoupon offerCoupon in offerBackCouponList)
                        {
                            XmlNode offerCouponNode = XmlUtils.AppendChildNode(respXmlDoc, offerCouponListNode, "voucher");
                            XmlUtils.SetAttributeValue(respXmlDoc, offerCouponNode, "id", offerCoupon.CouponType.ToString());
                            XmlUtils.SetAttributeValue(respXmlDoc, offerCouponNode, "date_valid", FormatUtils.DateToString(offerCoupon.ValidDate));
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "name", offerCoupon.CouponTypeName);
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "amount", (-offerCoupon.OfferMoney).ToString("f2"));
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "balance", offerCoupon.Balance.ToString("f2"));
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "diff_deduct", offerCoupon.OfferBackDifference.ToString("f2"));
                            if (offerCoupon.SpecialType != 0)
                                XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "special_type", offerCoupon.SpecialType.ToString());
                        }
                    }
                }
            }
            respXml = respXmlDoc.OuterXml;
        }

        private void CalcRSaleBillDecMoney(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            //CrmStoreInfo storeInfo = new CrmStoreInfo();
            //string posId = string.Empty;
            //if (!GetInfoFromClientSession(reqXmlRoot, storeInfo, out posId))
            //{
            //    respXml = SpellFailXml("reqdata", "session_id 错误");
            //    return;
            //}

            int serverBillId = 0;
            List<CrmPayment> paymentList = new List<CrmPayment>();

            string msg = string.Empty;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                serverBillId = int.Parse(str);
            if (serverBillId <= 0)
                msg = "元素bfcrm_req/server_bill_id必须有 > 0 的整数值";
            else
            {
                XmlNode payListNode = reqXmlRoot.SelectSingleNode("pay_list");
                if (payListNode != null)
                {
                    foreach (XmlNode node in payListNode.ChildNodes)
                    {
                        CrmPayment payment = new CrmPayment();
                        paymentList.Add(payment);
                        XmlUtils.GetChildTextNodeValue(node, "type", out payment.PayTypeCode);
                        if (XmlUtils.GetChildTextNodeValue(node, "amount", out str))
                            payment.PayMoney = double.Parse(str);
                    }
                }
                if (paymentList.Count == 0)
                    msg = "对元素bfcrm_req/pay_list：无有数据的子元素";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            double decMoney = 0;
            List<CrmArticle> articleList = new List<CrmArticle>();
            bool ok = PosProc.CalcRSaleBillDecMoney(out msg, out decMoney, articleList, serverBillId, paymentList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }

            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "dec_sum", decMoney.ToString("f2"));
            if (MathUtils.DoubleAGreaterThanDoubleB(decMoney, 0))
            {
                XmlNode decListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "article_dec_list");
                foreach (CrmArticle article in articleList)
                {
                    if (!MathUtils.DoubleAEuqalToDoubleB(article.DecMoney, 0))
                    {
                        XmlNode decNode = XmlUtils.AppendChildNode(respXmlDoc, decListNode, "article_dec");
                        XmlUtils.AppendChildTextNode(respXmlDoc, decNode, "article_inx", article.Inx.ToString());
                        XmlUtils.AppendChildTextNode(respXmlDoc, decNode, "dec_share", article.DecMoney.ToString("f2"));
                        if (article.DecMoneyIsExpense)
                            XmlUtils.AppendChildTextNode(respXmlDoc, decNode, "is_expense", "Y");
                    }
                }
            }
            respXml = respXmlDoc.OuterXml;

        }

        private void CheckOutRSaleBill(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string step = string.Empty;
            int serverBillId = 0;
            int billId = 0;
            int payBackCouponVipId = 0;
            bool couponPaid = false;

            List<CrmPayment> paymentList = new List<CrmPayment>();

            string msg = string.Empty;
            string str = string.Empty;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "step", out step);
            if (step.Equals("prepare") || step.Equals("confirm") || step.Equals("redo") || step.Equals("cancel"))
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                    serverBillId = int.Parse(str);
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id", out str))
                    billId = int.Parse(str);
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id_return_voucher", out str))
                    payBackCouponVipId = int.Parse(str);
                if (serverBillId <= 0)
                    msg = "元素bfcrm_req/server_bill_id必须有 > 0 的整数值";
                else if ((billId <= 0) && (step.Equals("redo") || step.Equals("cancel")))
                    msg = "元素bfcrm_req/bill_id必须有 > 0 的整数值";
            }
            else
                msg = "元素bfcrm_req/step的值必须是 prepare 或 confirm 或 redo 或 cancel";

            if (msg.Length == 0)
            {
                XmlNode payListNode = reqXmlRoot.SelectSingleNode("pay_list");
                int bankCardInx = 0;
                if (payListNode != null)
                {
                    foreach (XmlNode payNode in payListNode.ChildNodes)
                    {
                        CrmPayment payment = new CrmPayment();
                        paymentList.Add(payment);
                        XmlUtils.GetChildTextNodeValue(payNode, "type", out payment.PayTypeCode);
                        if (XmlUtils.GetChildTextNodeValue(payNode, "amount", out str))
                            payment.PayMoney = double.Parse(str);
                        if (XmlUtils.GetChildTextNodeValue(payNode, "is_cashcard", out str))
                            payment.IsCashCard = (str.Equals("Y"));
                        XmlNode bankCardListNode = payNode.SelectSingleNode("bank_list");
                        if (bankCardListNode != null)
                        {
                            payment.BankCardList = new List<CrmBankCardPayment>();
                            foreach (XmlNode bankCardNode in bankCardListNode.ChildNodes)
                            {
                                CrmBankCardPayment bankCard = new CrmBankCardPayment();
                                payment.BankCardList.Add(bankCard);
                                bankCard.Inx = bankCardInx++;
                                XmlUtils.GetAttributeValue(bankCardNode, "bank_no", out bankCard.BankCardCode);
                                //if (XmlUtils.GetChildTextNodeValue(bankCardNode, "bank_id", out str))
                                //    bankCard.BankId = int.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(bankCardNode, "amount", out str))
                                    bankCard.PayMoney = double.Parse(str);
                            }
                        }
                    }
                }
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "xp_yq", out str))
                    couponPaid = (str.Equals("Y"));

                if (step.Equals("prepare") && (paymentList.Count == 0))
                    msg = "对元素bfcrm_req/pay_list：无有数据的子元素";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            double billCent = 0;
            double vipCent = 0;
            double vipShopCent = 0;
            double vipCompanyCent = 0;
            string offerCouponVipCode = string.Empty;
            List<CrmArticle> articleList = null;
            List<CrmPaymentArticleShare> couponArticleShareList = null;
            List<CrmCouponPayment> payBackCouponList = null;
            List<CrmPromOfferCoupon> offerCouponList = null;
            List<CrmSaleMoneyLeftWhenPromCalc> leftSaleMoneyList = null;

            bool ok = false;
            bool needBuyCent = false;
            bool needVipToOfferCoupon = false;
            if (step.Equals("prepare"))
            {
                articleList = new List<CrmArticle>();
                couponArticleShareList = new List<CrmPaymentArticleShare>();
                payBackCouponList = new List<CrmCouponPayment>();
                offerCouponList = new List<CrmPromOfferCoupon>();
                ok = PosProc.PrepareCheckOutRSaleBill(out msg, out billCent, out needVipToOfferCoupon, out needBuyCent, out offerCouponVipCode, articleList, couponArticleShareList, payBackCouponList, offerCouponList, serverBillId, paymentList, payBackCouponVipId, couponPaid);
            }
            else if (step.Equals("confirm") || step.Equals("redo"))
            {
                offerCouponList = new List<CrmPromOfferCoupon>();
                leftSaleMoneyList = new List<CrmSaleMoneyLeftWhenPromCalc>();
                ok = PosProc.CheckOutRSaleBill(out msg, out billCent, out vipCent, out vipShopCent, out vipCompanyCent, out offerCouponVipCode, offerCouponList, leftSaleMoneyList, serverBillId);
            }
            else if (step.Equals("cancel"))
            {
                ok = PosProc.CancelRSaleBackBill(out msg, serverBillId);
            }
            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            if (step.Equals("prepare"))
            {
                if (needVipToOfferCoupon)
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "need_vip_grant_voucher", "Y");
                if (needBuyCent)
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "bRequire_buy_jf", "Y");
                if (!MathUtils.DoubleAEuqalToDoubleB(billCent, 0))
                {
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "cent_gain", billCent.ToString("f4"));

                    XmlNode articleListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "article_list");
                    foreach (CrmArticle article in articleList)
                    {
                        XmlNode articleNode = XmlUtils.AppendChildNode(respXmlDoc, articleListNode, "article");
                        XmlUtils.SetAttributeValue(respXmlDoc, articleNode, "inx", article.Inx.ToString());
                        XmlUtils.AppendChildTextNode(respXmlDoc, articleNode, "cent_sp", article.GainedCent.ToString("f4"));
                    }
                }
                if (offerCouponList.Count > 0)
                {
                    XmlNode offerCouponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "grant_voucher_list");
                    XmlUtils.SetAttributeValue(respXmlDoc, offerCouponListNode, "member_code", offerCouponVipCode);
                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                    {
                        XmlNode offerCouponNode = XmlUtils.AppendChildNode(respXmlDoc, offerCouponListNode, "voucher");
                        XmlUtils.SetAttributeValue(respXmlDoc, offerCouponNode, "id", offerCoupon.CouponType.ToString());
                        XmlUtils.SetAttributeValue(respXmlDoc, offerCouponNode, "date_valid", FormatUtils.DateToString(offerCoupon.ValidDate));
                        XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "name", offerCoupon.CouponTypeName);
                        XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "amount", offerCoupon.OfferMoney.ToString("f2"));
                        XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "balance", offerCoupon.Balance.ToString("f2"));
                        if (!MathUtils.DoubleAEuqalToDoubleB(offerCoupon.OfferBackDifference, 0))
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "diff_deduct", offerCoupon.OfferBackDifference.ToString("f2"));
                        if (offerCoupon.SpecialType != 0)
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "special_type", offerCoupon.SpecialType.ToString());
                    }
                }
                if ((payBackCouponList != null) && (payBackCouponList.Count > 0))
                {
                    XmlNode listNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "pay_voucher_list");
                    XmlNode cardNode = XmlUtils.AppendChildNode(respXmlDoc, listNode, "card");
                    foreach (CrmCouponPayment payBackCoupon in payBackCouponList)
                    {
                        XmlNode couponNode = XmlUtils.AppendChildNode(respXmlDoc, cardNode, "voucher");
                        XmlUtils.SetAttributeValue(respXmlDoc, couponNode, "id", payBackCoupon.CouponType.ToString());
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "name", payBackCoupon.CouponTypeName);
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "amount", payBackCoupon.PayMoney.ToString("f2"));
                        XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "balance", payBackCoupon.Balance.ToString("f2"));
                        if (!MathUtils.DoubleAEuqalToDoubleB(payBackCoupon.PayCent, 0))
                            XmlUtils.AppendChildTextNode(respXmlDoc, couponNode, "pay_cent", payBackCoupon.PayCent.ToString("f4"));
                    }
                }
                if ((couponArticleShareList != null) && (couponArticleShareList.Count > 0))
                {
                    XmlNode shareListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "pay_voucher_article_list");
                    foreach (CrmPaymentArticleShare share in couponArticleShareList)
                    {
                        if (share.Payment != null)
                            share.CouponType = share.Payment.CouponType;
                        if (share.Article != null)
                            share.ArticleInx = share.Article.Inx;
                        if (share.CouponType >= 0)
                        {
                            XmlNode shareNode = XmlUtils.AppendChildNode(respXmlDoc, shareListNode, "voucher_article");
                            XmlUtils.AppendChildTextNode(respXmlDoc, shareNode, "voucher_id", share.CouponType.ToString());
                            XmlUtils.AppendChildTextNode(respXmlDoc, shareNode, "article_inx", share.ArticleInx.ToString());
                            XmlUtils.AppendChildTextNode(respXmlDoc, shareNode, "amount", share.ShareMoney.ToString("f2"));
                        }
                    }
                }
            }
            else if (step.Equals("confirm"))
            {
                if (!MathUtils.DoubleAEuqalToDoubleB(billCent, 0))
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "cent_gain", billCent.ToString("f4"));
                if (!MathUtils.DoubleAEuqalToDoubleB(vipCent, 0))
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "total_cent_valid", vipCent.ToString("f4"));
                if (!MathUtils.DoubleAEuqalToDoubleB(vipShopCent, 0))
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "total_cent_shop", vipShopCent.ToString("f4"));
                if (!MathUtils.DoubleAEuqalToDoubleB(vipCompanyCent, 0))
                    XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "total_cent_company", vipCompanyCent.ToString("f4"));

                if (offerCouponList.Count > 0)
                {
                    XmlNode offerCouponListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "grant_voucher_list");
                    XmlUtils.SetAttributeValue(respXmlDoc, offerCouponListNode, "member_code", offerCouponVipCode);
                    foreach (CrmPromOfferCoupon offerCoupon in offerCouponList)
                    {
                        if (offerCoupon.Coupon != null)
                        {
                            XmlNode offerCouponNode = XmlUtils.AppendChildNode(respXmlDoc, offerCouponListNode, "voucher");
                            XmlUtils.SetAttributeValue(respXmlDoc, offerCouponNode, "id", offerCoupon.CouponType.ToString());
                            XmlUtils.SetAttributeValue(respXmlDoc, offerCouponNode, "date_valid", FormatUtils.DateToString(offerCoupon.ValidDate));
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "name", offerCoupon.CouponTypeName);
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "amount", offerCoupon.OfferMoney.ToString("f2"));
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "balance", offerCoupon.Balance.ToString("f2"));
                            //if (!MathUtils.DoubleAEuqalToDoubleB(offerCoupon.OfferBackDifference, 0))
                            //    XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "diff_deduct", offerCoupon.OfferBackDifference.ToString("f2"));
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "paper_voucher_note1", offerCoupon.Coupon.PaperNote1);
                            XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "paper_voucher_note2", offerCoupon.Coupon.PaperNote2);

                            if ((offerCoupon.Coupon.IsPaperCoupon) || ((offerCoupon.SpecialType == 1) || (offerCoupon.SpecialType == 2)))
                            {
                                XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "paper_voucher_code", offerCoupon.CouponCode);
                                XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "paper_voucher_unit", offerCoupon.Coupon.PaperUnit);
                            };

                            if (offerCoupon.SpecialType == 0)
                            {
                                if ((offerCoupon.SpecialType == 0) && (!offerCoupon.Coupon.IsPaperCoupon))
                                    XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "is_vip_voucher", "Y");
                                else
                                    XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "is_vip_voucher", "N");
                            }
                            else
                                XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "special_type", offerCoupon.SpecialType.ToString());
                            if (offerCoupon.BankCardCode.Trim().Length > 0)
                                XmlUtils.AppendChildTextNode(respXmlDoc, offerCouponNode, "bank_card_code", offerCoupon.BankCardCode.Trim());
                        }
                    }
                }
                if (leftSaleMoneyList.Count > 0)
                {
                    XmlNode leftSaleMoneyListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "amount_list_waiting_grant_voucher");
                    foreach (CrmSaleMoneyLeftWhenPromCalc leftSaleMoney in leftSaleMoneyList)
                    {
                        XmlNode leftSaleMoneyNode = XmlUtils.AppendChildNode(respXmlDoc, leftSaleMoneyListNode, "item");
                        XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "add_up_type", leftSaleMoney.AddupTypeDesc);
                        XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "promotion", leftSaleMoney.PromotionName);
                        XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "voucher", leftSaleMoney.CouponTypeName);
                        XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "grant_rule", leftSaleMoney.RuleName);
                        XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "amount", leftSaleMoney.SaleMoney.ToString("f2"));
                    }
                }
            }
            respXml = respXmlDoc.OuterXml;
        }

        private void GetSaleMoneyLeftWhenOfferCoupon(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);

            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素track_data, 且有值";
            }
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int vipId = 0;
            string vipCode = string.Empty;
            List<CrmSaleMoneyLeftWhenPromCalc> leftSaleMoneyList = new List<CrmSaleMoneyLeftWhenPromCalc>();

            bool ok = PosProc.GetSaleMoneyLeftWhenOfferCoupon(out msg, out vipId, out vipCode, leftSaleMoneyList, condType, condValue, cardCodeToCheck, verifyCode, storeInfo);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_id", vipId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "member_code", vipCode);

            XmlNode leftSaleMoneyListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "amount_list_waiting_grant_voucher");
            foreach (CrmSaleMoneyLeftWhenPromCalc leftSaleMoney in leftSaleMoneyList)
            {
                XmlNode leftSaleMoneyNode = XmlUtils.AppendChildNode(respXmlDoc, leftSaleMoneyListNode, "item");
                XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "add_up_type", leftSaleMoney.AddupTypeDesc);
                XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "promotion", leftSaleMoney.PromotionName);
                XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "voucher", leftSaleMoney.CouponTypeName);
                XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "grant_rule", leftSaleMoney.RuleName);
                XmlUtils.AppendChildTextNode(respXmlDoc, leftSaleMoneyNode, "amount", leftSaleMoney.SaleMoney.ToString("f2"));
            }

            respXml = respXmlDoc.OuterXml;

        }

        private void CalcRSaleBillCent(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
            int situationalMode = 0;
            billHead.BillType = -1;
            billHead.TotalSaleMoney = 0;
            List<CrmArticle> articleList = new List<CrmArticle>();
            List<CrmPayment> paymentList = new List<CrmPayment>();

            string msg = string.Empty;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "situational_mode", out str))
                situationalMode = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out str))
                billHead.VipId = int.Parse(str);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_code", out billHead.VipCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out billHead.StoreCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out billHead.PosId);

            XmlNode billNode = reqXmlRoot.SelectSingleNode("bill");
            if (billNode != null)
            {
                if (XmlUtils.GetAttributeValue(billNode, "id", out str))
                    billHead.BillId = int.Parse(str);
                if (billHead.BillId <= 0)
                    msg = "元素bfcrm_req/bill的属性id必须有 > 0 的整数值";
                else
                {
                    XmlNode headNode = billNode.SelectSingleNode("head");
                    if (headNode != null)
                    {
                        if (XmlUtils.GetChildTextNodeValue(headNode, "type", out str))
                        {
                            if (str.Equals("sale"))
                                billHead.BillType = CrmPosData.BillTypeSale;
                            else if (str.Equals("return"))
                                billHead.BillType = CrmPosData.BillTypeReturn;
                            else if (str.Equals("exchange"))
                                billHead.BillType = CrmPosData.BillTypeExchange;
                            else
                                msg = "元素bfcrm_req/bill/head/type的值必须是 sale 或 return 或 exchange";
                        }
                        else
                            msg = "元素bfcrm_req/bill/head/type必须有";
                        if (msg.Length == 0)
                        {
                            //if (billHead.BillType != 0)     //退货或换货
                            //{
                            //    XmlUtils.GetChildTextNodeValue(headNode, "machine_old", out billHead.OriginalPosId);
                            //    if (XmlUtils.GetChildTextNodeValue(headNode, "bill_id_old", out str))
                            //        billHead.OriginalBillId = int.Parse(str);
                            //    if (billHead.BillType == 2) //换货肯定是选单的
                            //    {
                            //        if ((billHead.OriginalPosId.Length == 0) || (billHead.OriginalBillId <= 0))
                            //            msg = "元素bfcrm_req/bill/head/machine_old必须有值, 元素bfcrm_req/bill/head/bill_id_old必须有 > 0 的整数值";
                            //    }
                            //}
                            XmlUtils.GetChildTextNodeValue(headNode, "cashier", out billHead.Cashier);
                            if (XmlUtils.GetChildTextNodeValue(headNode, "time_shopping", out str))
                                billHead.SaleTime = FormatUtils.ParseDatetimeString(str);
                            if (XmlUtils.GetChildTextNodeValue(headNode, "date_account", out str))
                                billHead.AccountDate = FormatUtils.ParseDateString(str);

                            if (billHead.SaleTime == DateTime.MinValue)
                                msg = "元素bfcrm_req/bill/head/time_shopping必须有值，格式为：yyyy-mm-dd hh:nn:ss";
                            else if (billHead.AccountDate == DateTime.MinValue)
                                msg = "元素bfcrm_req/bill/head/date_account必须有值，格式为：yyyy-mm-dd";
                        }
                    }
                    if (msg.Length == 0)
                    {
                        XmlNode articleListNode = billNode.SelectSingleNode("article_list");
                        if (articleListNode != null)
                        {
                            foreach (XmlNode articleNode in articleListNode.ChildNodes)
                            {
                                CrmArticle article = new CrmArticle();
                                articleList.Add(article);
                                if (XmlUtils.GetAttributeValue(articleNode, "inx", out str))
                                    article.Inx = int.Parse(str);
                                XmlUtils.GetChildTextNodeValue(articleNode, "code", out article.ArticleCode);
                                XmlUtils.GetChildTextNodeValue(articleNode, "dept_sale", out article.DeptCode);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "quantity", out str))
                                    article.SaleNum = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "amount", out str))
                                    article.SaleMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "amount_discount", out str))
                                    article.DiscMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "amount_discount_member", out str))
                                    article.VipDiscMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "no_cent", out str))
                                    article.IsNoCent = (str.Equals("Y"));
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "bj_bcjhd", out str))
                                    article.IsNoProm = (str.Equals("Y"));
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "discount_bill_id", out str))
                                    article.VipDiscBillId = int.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(articleNode, "rate_discount", out str))
                                    article.VipDiscRate = double.Parse(str);

                                billHead.TotalSaleMoney = billHead.TotalSaleMoney + article.SaleMoney;
                            }
                            if (articleList.Count == 0)
                                msg = "对元素bfcrm_req/bill/article_list：' + #13#10 + '    无有数据的子元素";
                        }
                    }
                    if (msg.Length == 0)
                    {
                        XmlNode payListNode = billNode.SelectSingleNode("pay_list");
                        int bankCardInx = 0;
                        if (payListNode != null)
                        {
                            foreach (XmlNode payNode in payListNode.ChildNodes)
                            {
                                CrmPayment payment = new CrmPayment();
                                paymentList.Add(payment);
                                XmlUtils.GetChildTextNodeValue(payNode, "type", out payment.PayTypeCode);
                                if (XmlUtils.GetChildTextNodeValue(payNode, "amount", out str))
                                    payment.PayMoney = double.Parse(str);
                                if (XmlUtils.GetChildTextNodeValue(payNode, "is_cashcard", out str))
                                    payment.IsCashCard = (str.Equals("Y"));
                                XmlNode bankCardListNode = payNode.SelectSingleNode("bank_list");
                                if (bankCardListNode != null)
                                {
                                    payment.BankCardList = new List<CrmBankCardPayment>();
                                    foreach (XmlNode bankCardNode in bankCardListNode.ChildNodes)
                                    {
                                        CrmBankCardPayment bankCard = new CrmBankCardPayment();
                                        payment.BankCardList.Add(bankCard);
                                        bankCard.Inx = bankCardInx++;
                                        XmlUtils.GetAttributeValue(bankCardNode, "bank_no", out bankCard.BankCardCode);
                                        //if (XmlUtils.GetChildTextNodeValue(bankCardNode, "bank_id", out str))
                                        //    bankCard.BankId = int.Parse(str);
                                        if (XmlUtils.GetChildTextNodeValue(bankCardNode, "amount", out str))
                                            bankCard.PayMoney = double.Parse(str);
                                    }
                                }
                            }
                        }
                        if (paymentList.Count == 0)
                            msg = "对元素bfcrm_req/pay_list：无有数据的子元素";
                    }
                }
            }

            if (billHead.StoreCode.Length == 0)
            {
                CrmStoreInfo storeInfo = new CrmStoreInfo();
                if (GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out billHead.PosId))
                {
                    billHead.CompanyCode = storeInfo.Company;
                    billHead.StoreCode = storeInfo.StoreCode;
                    billHead.StoreId = storeInfo.StoreId;
                }
                else
                    msg = "session_id 错误";
            }

            if (msg.Length == 0)
            {
                if ((billHead.BillType == 0) && (MathUtils.DoubleASmallerThanDoubleB(billHead.TotalSaleMoney, 0)))
                    msg = "是销售单, 但总金额 < 0";
                else if ((billHead.BillType != 0) && (MathUtils.DoubleAGreaterThanDoubleB(billHead.TotalSaleMoney, 0)))
                    msg = "是退换货单, 但总金额 > 0";
                else if (billHead.StoreCode.Length == 0)
                    msg = "store_code = ''";
                else if (billHead.PosId.Length == 0)
                    msg = "pos_id = ''";
            }

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.CalcRSaleBillCent(out msg, situationalMode, billHead, articleList, paymentList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_bill_id", billHead.ServerBillId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "cent_gain", billHead.TotalGainedCent.ToString("f4"));
            respXml = respXmlDoc.OuterXml;
        }

        private void GetCodedCoupon(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            string msg = string.Empty;
            string code = string.Empty;
            int serverBillId = 0;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "code", out code);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                serverBillId = int.Parse(str);

            if (code.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素code, 且有值";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            int couponType = -1;
            double balance = 0;
            double limitMoney = 0;

            bool ok = PosProc.GetCodedCoupon(out msg, out couponType, out balance, out limitMoney, code, serverBillId);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "voucher_id", couponType.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "balance", balance.ToString("f2"));
            if (serverBillId > 0)
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "payable", limitMoney.ToString("f2"));
            respXml = respXmlDoc.OuterXml;

        }

        private void PrepareTransCodedCouponPayment(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int transId = 0;
            CrmRSaleBillHead billHead = new CrmRSaleBillHead();
            List<string> couponCodeList = new List<string>();

            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                billHead.ServerBillId = int.Parse(str);
            else if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id", out str))
            {
                billHead.BillId = int.Parse(str);
                billHead.BillType = CrmPosData.BillTypeSale;
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out billHead.StoreCode);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out billHead.PosId);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier", out billHead.Cashier);
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "date_account", out str))
                {
                    billHead.AccountDate = FormatUtils.ParseDateString(str);
                }
                if (billHead.StoreCode.Length == 0)
                {
                    CrmStoreInfo storeInfo = new CrmStoreInfo();
                    if (GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out billHead.PosId))
                    {
                        billHead.CompanyCode = storeInfo.Company;
                        billHead.StoreCode = storeInfo.StoreCode;
                        billHead.StoreId = storeInfo.StoreId;
                    }
                }
            }

            string msg = string.Empty;
            if (billHead.ServerBillId <= 0)
            {
                if (billHead.BillId <= 0)
                {
                    msg = "元素bfcrm_req/server_bill_id 或 bfcrm_req/bill_id 必须有一个是 > 0 的整数值";
                }
                else if (billHead.StoreCode.Length == 0)
                {
                    msg = "store_code = ''";
                }
                else if (billHead.PosId.Length == 0)
                {
                    msg = "pos_id = ''";
                }
                else if (billHead.AccountDate == DateTime.MinValue)
                {
                    msg = "元素bfcrm_req/date_account 必须有值，且日期值格式为：yyyy-mm-dd";
                }
            }
            if (msg.Length == 0)
            {
                XmlNode codeListNode = reqXmlRoot.SelectSingleNode("code_list");
                if (codeListNode != null)
                {
                    foreach (XmlNode node in codeListNode.ChildNodes)
                    {
                        //if (XmlUtils.GetChildTextNodeValue(node, "code", out str))
                        //    couponCodeList.Add(str);
                        if (node.Name.Equals("code"))
                            couponCodeList.Add(node.LastChild.Value);
                    }
                }
                if (couponCodeList.Count == 0)
                    msg = "对元素bfcrm_req/code_list：无有数据的子元素";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.PrepareTransCodedCouponPayment(out msg, out transId, billHead, couponCodeList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_transaction_id", transId.ToString());
            respXml = respXmlDoc.OuterXml;
        }

        private void GetBankCardPromDesc(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string msg = string.Empty;
            string cardCode = string.Empty;
            CrmStoreInfo storeInfo = new CrmStoreInfo();

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);

            if (storeInfo.StoreCode.Length == 0)
            {
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);
            }
            if (cardCode.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素card_code, 且有值";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            string promDesc = string.Empty;

            bool ok = PosProc.GetBankCardPromDesc(out msg, out promDesc, cardCode, storeInfo);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "prom_desc", promDesc);
            respXml = respXmlDoc.OuterXml;
        }

        private void GetPayAccount(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            string posId = string.Empty;
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out posId);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out posId);

            string msg = string.Empty;
            string str = string.Empty;
            string cashier = string.Empty;
            DateTime accountDate = DateTime.MinValue;
            int beginBillId = 0;
            int endBillId = 0;
            bool isDetail = false;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier", out cashier);
            if (cashier.Length == 0)
                msg = "元素bfcrm_req/cashier 必须有值";
            else if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "date_account", out str))
                accountDate = FormatUtils.ParseDateString(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id_begin", out str))
            {
                beginBillId = int.Parse(str);
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bill_id_end", out str))
                    endBillId = int.Parse(str);
            }
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "detail", out str))
                isDetail = str.Equals("Y");
            if ((accountDate == DateTime.MinValue) && (beginBillId == 0))
                msg = "元素bfcrm_req/date_account或bill_id_begin 必须有一个有值";
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";
            else if (posId.Length == 0)
                msg = "pos_id = ''";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            List<CrmPayAccount> accountList = new List<CrmPayAccount>();

            bool ok = PosProc.GetPayAccount(out msg, accountList, isDetail, storeInfo, posId, cashier, accountDate, beginBillId, endBillId);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            foreach (CrmPayAccount account in accountList)
            {
                XmlNode itemXmlNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "item");
                if (isDetail)
                    XmlUtils.AppendChildTextNode(respXmlDoc, itemXmlNode, "bill_id", account.BillId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemXmlNode, "pay_cashcard", account.PayCashCardMoney.ToString("f2"));
                XmlUtils.AppendChildTextNode(respXmlDoc, itemXmlNode, "recycle_cashcard", "0.00");
                XmlUtils.AppendChildTextNode(respXmlDoc, itemXmlNode, "pay_voucher", (account.PayVipCouponMoney + account.PayCodedCouponMoney).ToString("f2"));
            }
            respXml = respXmlDoc.OuterXml;
        }

        private void GetCodedCouponPayAccount_Dennis(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);

            string msg = string.Empty;
            string str = string.Empty;
            string cashier = string.Empty;
            DateTime accountDate = DateTime.MinValue;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier", out cashier);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "date_account", out str))
                accountDate = FormatUtils.ParseDateString(str);
            if (cashier.Length == 0)
                msg = "元素bfcrm_req/cashier 必须有值";
            else if (accountDate == DateTime.MinValue)
                msg = "元素bfcrm_req/date_account或bill_id_begin 必须有一个有值";
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            List<CrmCodedCouponPayment> paymentList = new List<CrmCodedCouponPayment>();

            bool ok = PosProc.GetCodedCouponPayAccount_Dennis(out msg, paymentList, storeInfo, cashier, accountDate);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            foreach (CrmCodedCouponPayment payment in paymentList)
            {
                XmlNode itemXmlNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "item");
                XmlUtils.AppendChildTextNode(respXmlDoc, itemXmlNode, "coupon_code", payment.CouponCode);
                XmlUtils.AppendChildTextNode(respXmlDoc, itemXmlNode, "pay_money", payment.PayMoney.ToString("f2"));
            }
            respXml = respXmlDoc.OuterXml;
        }

        private void PrepareTransSaveChangeIntoVipCard(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            int transId = 0;
            int serverBillId = 0;
            int vipId = 0;
            double changeMoney = 0;
            double balance = 0;
            string str = string.Empty;

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "server_bill_id", out str))
                serverBillId = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "vip_id", out str))
                vipId = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "change", out str))
                changeMoney = double.Parse(str);

            string msg = string.Empty;
            if (serverBillId <= 0)
                msg = "元素bfcrm_req/server_bill_id 必须是 > 0 的整数值";
            else if (vipId <= 0)
                msg = "元素bfcrm_req/vip_id 必须是 > 0 的整数值";
            else if (!MathUtils.DoubleAGreaterThanDoubleB(changeMoney, 0))
                msg = "元素bfcrm_req/change 必须是 > 0 的数";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.PrepareTransSaveChangeIntoVipCard(out msg, out transId, out balance, vipId, serverBillId, changeMoney);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "server_transaction_id", transId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "balance", balance.ToString("f2"));
            respXml = respXmlDoc.OuterXml;
        }

        private void SaveMoneyToCashCard(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            string password = string.Empty;
            string posId = string.Empty;
            string cashierCode = string.Empty;
            string cashierName = string.Empty;
            double saveMoney = 0;
            List<CrmPayment> paymentList = new List<CrmPayment>();

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_password", out password);

            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out posId);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out str))
                posId = str;
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier_code", out cashierCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier_name", out cashierName);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "save_money", out str))
                saveMoney = double.Parse(str);
            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素track_data, 且有值";
            }
            else if (!MathUtils.DoubleAGreaterThanDoubleB(saveMoney, 0))
            {
                msg = "元素bfcrm_req必须有子元素save_money, 且大于 0";
            }
            else
            {
                XmlNode payListNode = reqXmlRoot.SelectSingleNode("pay_list");
                if (payListNode != null)
                {
                    foreach (XmlNode payNode in payListNode.ChildNodes)
                    {
                        CrmPayment payment = new CrmPayment();
                        paymentList.Add(payment);
                        XmlUtils.GetChildTextNodeValue(payNode, "type", out payment.PayTypeCode);
                        if (XmlUtils.GetChildTextNodeValue(payNode, "amount", out str))
                            payment.PayMoney = double.Parse(str);
                    }
                }

                if (paymentList.Count == 0)
                    msg = "对元素bfcrm_req/pay_list：无有数据的子元素";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int cardId = 0;
            string cardCode = string.Empty;
            int seq = 0;
            bool ok = PosProc.SaveMoneyToCashCard(out msg, out cardId, out cardCode, out seq, condType, condValue, cardCodeToCheck, verifyCode, password, storeInfo, posId, cashierCode, cashierName, saveMoney, paymentList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "card_id", cardId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "card_code", cardCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "seq", seq.ToString());

            respXml = respXmlDoc.OuterXml;

        }

        private void ReturnCashCard(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int cardId = 0;

            string posId = string.Empty;
            string cashierCode = string.Empty;
            string cashierName = string.Empty;
            double returnMoney = 0;
            List<CrmPayment> paymentList = new List<CrmPayment>();


            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_id", out str))
                cardId = int.Parse(str);
            CrmStoreInfo storeInfo = new CrmStoreInfo();
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoAndPosIdFromClientSession(reqXmlRoot, storeInfo, out posId);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out str))
                posId = str;
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier_code", out cashierCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cashier_name", out cashierName);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "return_money", out str))
                returnMoney = double.Parse(str);
            string msg = string.Empty;
            if (cardId <= 0)
            {
                msg = "元素bfcrm_req必须有子元素card_id, 且大于 0";
            }
            else if (!MathUtils.DoubleAGreaterThanDoubleB(returnMoney, 0))
            {
                msg = "元素bfcrm_req必须有子元素return_money, 且大于 0";
            }
            else
            {
                XmlNode payListNode = reqXmlRoot.SelectSingleNode("pay_list");
                if (payListNode != null)
                {
                    foreach (XmlNode payNode in payListNode.ChildNodes)
                    {
                        CrmPayment payment = new CrmPayment();
                        paymentList.Add(payment);
                        XmlUtils.GetChildTextNodeValue(payNode, "type", out payment.PayTypeCode);
                        if (XmlUtils.GetChildTextNodeValue(payNode, "amount", out str))
                            payment.PayMoney = double.Parse(str);
                    }
                }

                if (paymentList.Count == 0)
                    msg = "对元素bfcrm_req/pay_list：无有数据的子元素";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            int seq = 0;
            bool ok = PosProc.ReturnCashCard(out msg, out seq, cardId, storeInfo, posId, cashierCode, cashierName, returnMoney, paymentList);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "seq", seq.ToString());

            respXml = respXmlDoc.OuterXml;

        }

        private void GetVipCardToBuyCent(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            double centToBuy = 0;
            double moneyToBuy = 0;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "memberid", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "thjf", out str))
                centToBuy = double.Parse(str);

            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素member_id或track_data, 且有值";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            CrmVipCard crmCard = null;

            bool ok = PosProc.GetVipCardToBuyCent(out msg, out crmCard, out moneyToBuy, centToBuy, condType, condValue, cardCodeToCheck, verifyCode);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "buy_money", moneyToBuy.ToString("f2"));

            XmlNode vipNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "member");
            XmlUtils.SetAttributeValue(respXmlDoc, vipNode, "id", crmCard.CardId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "code", crmCard.CardCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "name", crmCard.VipName);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_code", crmCard.CardTypeId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_name", crmCard.CardTypeName);
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "date_valid", crmCard.);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_discount", (crmCard.CanDisc ? "Y" : "N"));
            if (crmCard.DiscType == 1)
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "rate");
            else if (crmCard.DiscType == 2)
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "price");
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_voucher", (crmCard.CanOwnCoupon ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cumulate_cent", (crmCard.CanCent ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_th", (crmCard.CanReturn ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_available", crmCard.ValidCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_period", crmCard.StageCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_bn", crmCard.YearCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "phone", crmCard.PhoneCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "hello", crmCard.Hello);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "czkye", crmCard.CashCardBalance.ToString("f2"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "yhqye", crmCard.CouponBalance.ToString("f2"));
            respXml = respXmlDoc.OuterXml;
        }

        private void BuyVipCent(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int vipId = 0;
            double centToBuy = 0;
            double cashMoney = 0;
            double cardMoney = 0;
            double couponMoney = 0;
            CrmStoreInfo storeInfo = new CrmStoreInfo();
            string posId = string.Empty;
            string cashier = string.Empty;
            string cashierName = string.Empty;
            DateTime accountDate = DateTime.MinValue;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "memberid", out str))
                vipId = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "thjf", out str))
                centToBuy = double.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "czkje", out str))
                cardMoney = double.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "yhqje", out str))
                couponMoney = double.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "xj", out str))
                cashMoney = double.Parse(str);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_code", out storeInfo.StoreCode);
            if (storeInfo.StoreCode.Length == 0)
                GetStoreInfoFromClientSession(reqXmlRoot, storeInfo);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pos_id", out posId);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "operator", out cashier);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "skymc", out cashierName);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "date_account", out str))
                accountDate = FormatUtils.ParseDateString(str);
            else if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "jzrq", out str))
                accountDate = FormatUtils.ParseDateString(str);
            string msg = string.Empty;
            if (vipId <= 0)
                msg = "元素bfcrm_req必须有子元素memberid, 且大于0";
            else if ((!MathUtils.DoubleAGreaterThanDoubleB(cardMoney, 0)) && (!MathUtils.DoubleAGreaterThanDoubleB(couponMoney, 0)) && (!MathUtils.DoubleAGreaterThanDoubleB(cashMoney, 0)))
                msg = "现金、储值卡和优惠券金额 至少有一个要大于 0";
            else if (storeInfo.StoreCode.Length == 0)
                msg = "store_code = ''";

            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            bool ok = PosProc.BuyVipCent(out msg, vipId, centToBuy, cashMoney, cardMoney, couponMoney, storeInfo, posId, cashier, cashierName, accountDate);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");

            respXml = respXmlDoc.OuterXml;
        }

        private void GetBuyCentAccount(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            int condType = 0;
            string condValue = string.Empty;
            string cardCodeToCheck = string.Empty;
            string verifyCode = string.Empty;
            double centToBuy = 0;
            double moneyToBuy = 0;
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "member_id", out condValue))
            {
                condType = 1;
            }
            else
            {
                if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_value", out condValue))
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "cond_type", out str);
                    condType = int.Parse(str);
                }
                else
                {
                    XmlUtils.GetChildTextNodeValue(reqXmlRoot, "track_data", out condValue);
                    if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "flag", out str))
                    {
                        if (str.Equals("1"))
                            condType = 3;   //手机号
                        else if (str.Equals("2"))
                            condType = 2;   //卡号
                    }
                }
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "card_code", out cardCodeToCheck);
                XmlUtils.GetChildTextNodeValue(reqXmlRoot, "verify_code", out verifyCode);
            }

            string msg = string.Empty;
            if (condValue.Length == 0)
            {
                msg = "元素bfcrm_req必须有子元素member_id或track_data, 且有值";
            }
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }
            CrmVipCard crmCard = null;

            bool ok = PosProc.GetVipCardToBuyCent(out msg, out crmCard, out moneyToBuy, centToBuy, condType, condValue, cardCodeToCheck, verifyCode);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "buy_money", moneyToBuy.ToString("f2"));

            XmlNode vipNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "member");
            XmlUtils.SetAttributeValue(respXmlDoc, vipNode, "id", crmCard.CardId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "code", crmCard.CardCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "name", crmCard.VipName);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_code", crmCard.CardTypeId.ToString());
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "type_name", crmCard.CardTypeName);
            //XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "date_valid", crmCard.);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_discount", (crmCard.CanDisc ? "Y" : "N"));
            if (crmCard.DiscType == 1)
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "rate");
            else if (crmCard.DiscType == 2)
                XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "discount_method", "price");
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_voucher", (crmCard.CanOwnCoupon ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cumulate_cent", (crmCard.CanCent ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "permit_th", (crmCard.CanReturn ? "Y" : "N"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_available", crmCard.ValidCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_period", crmCard.StageCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "cent_bn", crmCard.YearCent.ToString("f4"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "phone", crmCard.PhoneCode);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "hello", crmCard.Hello);
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "czkye", crmCard.CashCardBalance.ToString("f2"));
            XmlUtils.AppendChildTextNode(respXmlDoc, vipNode, "yhqye", crmCard.CouponBalance.ToString("f2"));
            respXml = respXmlDoc.OuterXml;
        }

        private void SearchArticlePromRule(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            string str = string.Empty;
            string companyCode = string.Empty;
            int storeId = 0;
            string deptCode = string.Empty;
            string articleCode = string.Empty;
            int vipType = 0;
            int vipId = 0;
            string vipCode = string.Empty;
            int payType = 0;
            int bankId = 0;
            string bankCardCode = string.Empty;
            DateTime saleTime = DateTime.MinValue;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "company", out companyCode);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "store_id", out str))
                storeId = int.Parse(str);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "dept_code", out deptCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "article_code", out articleCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "vip_code", out vipCode);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "vip_type", out str))
                vipType = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "vip_id", out str))
                vipId = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "pay_type", out str))
                payType = int.Parse(str);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bank_id", out str))
                bankId = int.Parse(str);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bank_card_code", out bankCardCode);
            if (XmlUtils.GetChildTextNodeValue(reqXmlRoot, "sale_time", out str))
                saleTime = FormatUtils.ParseDatetimeString(str);

            //if (companyCode.Length == 0)
            //{
            //    CrmStoreInfo storeInfo = new CrmStoreInfo();
            //    if (GetStoreInfoFromClientSession(reqXmlRoot, storeInfo))
            //        companyCode = storeInfo.Company;
            //}

            string msg = string.Empty;
            if (companyCode.Length == 0)
                msg = "必须指明商户条件";
            else if (deptCode.Length == 0)
                msg = "必须指明销售部门条件";
            else if (articleCode.Length == 0)
                msg = "必须指明商品条件";
            if (msg.Length > 0)
            {
                respXml = SpellFailXml("reqdata", msg);
                return;
            }

            List<CrmPromBillItemDesc> promBillItemList = new List<CrmPromBillItemDesc>();
            bool ok = PosProc.SearchArticlePromRule(out msg, promBillItemList, companyCode, storeId, deptCode, articleCode, vipType, vipId, vipCode, payType, bankId, bankCardCode, saleTime);

            if (!ok)
            {
                respXml = SpellFailXml("logic", msg);
                return;
            }
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
            XmlNode itemListNode = XmlUtils.AppendChildNode(respXmlDoc, respXmlRoot, "item_list");
            foreach (CrmPromBillItemDesc item in promBillItemList)
            {
                XmlNode itemNode = XmlUtils.AppendChildNode(respXmlDoc, itemListNode, "item");
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "bill_type", item.BillType.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "prom_id", item.PromId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "prom_desc", item.RuleDesc);
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "bill_id", item.BillId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "sub_inx", item.SubBillInx.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "item_inx", item.ItemInx.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "begin_time", FormatUtils.DateToString(item.BeginTime));
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "end_time", FormatUtils.DateToString(item.EndTime));
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "period_desc", item.PeriodDesc);
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "rule_id", item.RuleId.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "rule_desc", item.RuleDesc);
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "coupon_type", item.CouponType.ToString());
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "coupon_name", item.CouponName);
                XmlUtils.AppendChildTextNode(respXmlDoc, itemNode, "special_type", item.SpecialType.ToString());
            }
            respXml = respXmlDoc.OuterXml;
        }
    }
}
