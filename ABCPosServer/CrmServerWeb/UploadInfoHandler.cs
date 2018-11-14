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
    public class UploadInfoHandler : IHttpHandler
    {
        private const string AppLogin = "0000"; //登陆
        private const string AppLogoff = "0001"; //退出
        private const string AppUploadInfo = "0201"; //基本信息上传

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
                context.Response.Write(msg);
                CrmServerPlatform.CheckTablesForRecordsAffectedAfterExecSql(out msg);
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
            else
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

        private void DoResponse(string appCode, string reqXml, out string respXml)
        {
            respXml = string.Empty;
            switch (appCode)
            {
                case "0000":
                    Login(reqXml, out respXml);
                    break;
                case AppLogoff:
                    Logoff(reqXml, out respXml);
                    break;
                case AppUploadInfo:
                    ProcUploadInfo(reqXml, out respXml);
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

        private void Login(string reqXml, out string respXml)
        {
            respXml = string.Empty;

            CrmLoginData loginData = new CrmLoginData();

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;

            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "company", out loginData.StoreInfo.Company);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "bfcrm_user", out loginData.UserCode);
            XmlUtils.GetChildTextNodeValue(reqXmlRoot, "password", out loginData.Password);

            string msg = string.Empty;
            bool ok = (UploadInfoProc.Login(out msg, loginData));

            if (ok)
            {
                XmlDocument respXmlDoc = new XmlDocument();
                XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
                respXmlDoc.AppendChild(xmlDec);

                XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
                respXmlDoc.AppendChild(respXmlRoot);
                XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");
                XmlUtils.AppendChildTextNode(respXmlDoc, respXmlRoot, "session_id", loginData.StoreInfo.Company);
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

        private void ProcUploadInfo(string reqXml, out string respXml)
        {
            respXml = string.Empty;
            string companyCode = string.Empty;

            XmlDocument reqXmlDoc = new XmlDocument();
            reqXmlDoc.LoadXml(reqXml);
            XmlNode reqXmlRoot = reqXmlDoc.DocumentElement;
            XmlUtils.GetAttributeValue(reqXmlRoot, "session_id", out companyCode);
            XmlDocument respXmlDoc = new XmlDocument();
            XmlDeclaration xmlDec = respXmlDoc.CreateXmlDeclaration("1.0", "GBK", null);
            respXmlDoc.AppendChild(xmlDec);

            XmlNode respXmlRoot = respXmlDoc.CreateElement("bfcrm_resp");
            respXmlDoc.AppendChild(respXmlRoot);
            XmlUtils.SetAttributeValue(respXmlDoc, respXmlRoot, "success", "Y");

            XmlNode itemsNode = null;

            itemsNode = reqXmlRoot.SelectSingleNode("品牌列表");
            if (itemsNode != null)
                ProcUploadArticleBrand(companyCode, itemsNode,respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("品类列表");
            if (itemsNode != null)
                ProcUploadArticleCategory(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("部门列表");
            if (itemsNode != null)
                ProcUploadDept(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("合同列表");
            if (itemsNode != null)
                ProcUploadContract(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("支付方式列表");
            if (itemsNode != null)
                ProcUploadPayMethod(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("商品列表");
            if (itemsNode != null)
                ProcUploadArticle(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("部门商品列表");
            if (itemsNode != null)
                ProcUploadDeptArticle(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            itemsNode = reqXmlRoot.SelectSingleNode("促销活动列表");
            if (itemsNode != null)
                ProcUploadPromotion(companyCode, itemsNode, respXmlDoc, respXmlRoot);

            
            respXml = respXmlDoc.OuterXml;
        }

        private void ProcUploadArticleBrand(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<ArticleBrand> items = null;
            
            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<ArticleBrand>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "代码", out str))
                        {
                            ArticleBrand item = new ArticleBrand();
                            items.Add(item);
                            item.Code = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "代码", out str))
                                item.NewCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "名称", out str))
                                item.Name = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "拼音", out str))
                                item.Spell = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "所有者", out str))
                                item.Owner = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "末级", out str))
                                item.IsLeaf = (str.Equals("Y") || str.Equals("y"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SaveArticleBrand(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "品牌列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadArticleCategory(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<ArticleCategory> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<ArticleCategory>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "代码", out str))
                        {
                            ArticleCategory item = new ArticleCategory();
                            items.Add(item);
                            item.Code = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "代码", out str))
                                item.NewCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "名称", out str))
                                item.Name = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "拼音", out str))
                                item.Spell = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "末级", out str))
                                item.IsLeaf = (str.Equals("Y") || str.Equals("y"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SaveArticleCategory(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "分类列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadDept(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<Dept> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<Dept>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "代码", out str))
                        {
                            Dept item = new Dept();
                            items.Add(item);
                            item.Code = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "代码", out str))
                                item.NewCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "名称", out str))
                                item.Name = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "全称", out str))
                                item.FullName = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "类型", out str))
                                item.DeptType = str;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SaveDept(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "部门列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadContract(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<Contract> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<Contract>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "代码", out str))
                        {
                            Contract item = new Contract();
                            items.Add(item);
                            item.Code = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "代码", out str))
                                item.NewCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "厂商名称", out str))
                                item.SuppName = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "厂商代码", out str))
                                item.SuppCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "部门代码", out str))
                                item.DeptCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "有效", out str))
                                item.IsValid = (str.Equals("Y") || str.Equals("y"));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SaveContract(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "合同列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadPayMethod(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<Payment> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<Payment>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "代码", out str))
                        {
                            Payment item = new Payment();
                            items.Add(item);
                            item.Code = str;
                            XmlUtils.GetChildTextNodeValue(itemNode, "代码", out item.NewCode);
                            XmlUtils.GetChildTextNodeValue(itemNode, "名称", out item.Name);
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "末级", out str))
                                item.IsLeaf = (str.Equals("Y") || str.Equals("y"));
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "满减标记", out str))
                                item.JoinPromDecMoney = (str.Equals("1") || str.Equals("y") || str.Equals("Y"));
                            item.CouponType = -1;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "优惠券", out str))
                            {
                                if (str.Length > 0)
                                    item.CouponType = int.Parse(str);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SavePayMethod(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "支付方式列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadArticle(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<Article> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<Article>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "代码", out str))
                        {
                            Article item = new Article();
                            items.Add(item);
                            item.Code = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "代码", out str))
                                item.NewCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "名称", out str))
                                item.Name = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "简称", out str))
                                item.ShortName = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "拼音", out str))
                                item.Spell = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "单位", out str))
                                item.Unit = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "花色", out str))
                                item.Color = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "规格", out str))
                                item.Spec = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "货号", out str))
                                item.ModelCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "品类", out str))
                                item.CategoryCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "品牌", out str))
                                item.BrandCode = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "合同", out str))
                                item.ContractCode = str;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SaveArticle(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "商品列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadDeptArticle(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<DeptArticle> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                XmlNode itemsNode = reqNode.SelectSingleNode("变更");
                if (itemsNode != null)
                {
                    items = new List<DeptArticle>();
                    foreach (XmlNode itemNode in itemsNode.ChildNodes)
                    {
                        DeptArticle item = new DeptArticle();
                        items.Add(item);
                        item.DeptCode = str;
                        XmlUtils.GetAttributeValue(itemNode, "部门代码", out item.DeptCode);
                        XmlUtils.GetAttributeValue(itemNode, "商品代码", out item.ArticleCode);
                    }
                }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SaveDeptArticle(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "部门商品列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
        private void ProcUploadPromotion(string companyCode, XmlNode reqNode, XmlDocument respXmlDoc, XmlNode respNode)
        {
            List<string> codes = null;
            List<Promotion> items = null;

            string msg = null;
            bool ok = true;
            try
            {
                string str = null;
                XmlNode codesNode = reqNode.SelectSingleNode("删除");
                if (codesNode != null)
                {
                    codes = new List<string>();
                    foreach (XmlNode codeNode in codesNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(codeNode, "代码", out str))
                            codes.Add(str);
                    }
                }
                //--XmlNode itemsNode = reqNode.SelectSingleNode;
               //-- if (itemsNode != null)
               //-- {
                    items = new List<Promotion>();
                    foreach (XmlNode itemNode in reqNode.ChildNodes)
                    {
                        if (XmlUtils.GetAttributeValue(itemNode, "编号", out str))
                        {
                            Promotion item = new Promotion();
                            items.Add(item);
                            item.Id = int.Parse(str);
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "主题", out str))
                                item.Subject = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "内容", out str))
                                item.Content = str;
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "年度", out str))
                                item.Year = int.Parse(str);
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "期数", out str))
                                item.PeriodNum = int.Parse(str);
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "开始时间", out str))
                                item.BeginTime = FormatUtils.ParseDatetimeString(str);
                            if (XmlUtils.GetChildTextNodeValue(itemNode, "结束时间", out str))
                                item.EndTime = FormatUtils.ParseDatetimeString(str);
                        }
                    }
               //-- }
            }
            catch (Exception e)
            {
                ok = false;
                msg = e.Message;
            }
            if (ok)
                ok = UploadInfoProc.SavePromotion(out msg, companyCode, codes, items);
            if (!ok)
            {
                XmlNode errorNode = respNode.SelectSingleNode("错误列表");
                if (errorNode == null)
                    errorNode = XmlUtils.AppendChildNode(respXmlDoc, respNode, "错误列表");
                XmlNode node = XmlUtils.AppendChildNode(respXmlDoc, XmlUtils.AppendChildNode(respXmlDoc, errorNode, "促销活动列表"), "内容");
                XmlUtils.SetAttributeValue(respXmlDoc, node, "信息", msg);
            }
        }
    }
}
