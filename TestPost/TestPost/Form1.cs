using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace TestPost
{
    public partial class Form1 : Form
    {
        //string TJDUser = "514c943fc2c64d55af428b9824607083";
        //string TJDKey = "6944b8af1afa43ab95031241d59bb604";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg;
            string resp;
            string req;
            //Show time
            string service = "parkhub.order.infoForFreeMins";
            string parkId = string.Empty;
            string carNum = tbCPH.Text;
            string sign = string.Empty;
            Dictionary<string, string> param = new Dictionary<string, string>();
            DateTime serverTime = DateTime.Now;

            param.Add("service", service);
            param.Add("parkId", parkId);
            param.Add("freeMins", tbFree.Text);
            param.Add("carNum", carNum);
            param.Add("timestamp", serverTime.ToString("yyyy-MM-dd hh24:mm:ss"));
            SignParam(ref param, out sign);

            JObject jo = new JObject();
            foreach (var item in param)
            {
                jo[item.Key] = item.Value;
            }
            req = jo.ToString();
            tbReq.Text = req;
            DoRequestToAnotherServer(out msg, out resp, tbURL.Text, req);//http://prep.tingjiandan.com/openapi/gateway
            jo = JObject.Parse(resp);
            tbTradeID.Text = jo["tradeId"].ToString();
            tbAccountID.Text = jo["accountId"].ToString();
            tbResp.Text = jo.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string msg;
            string resp;
            string req;

            string service = "parkhub.order.deductionNotSettle";
            string parkId = string.Empty;
            string sign = string.Empty;
            Dictionary<string, string> param = new Dictionary<string, string>();
            DateTime serverTime = DateTime.Now;

            param.Add("service", service);
            param.Add("tradeId", tbTradeID.Text);
            param.Add("deductionAmount", tbPay.Text);
            param.Add("outTradeNo", tbJLBH.Text);
            param.Add("accountId", tbAccountID.Text);
            param.Add("timestamp", serverTime.ToString("yyyy-MM-dd hh24:mm:ss"));
            SignParam(ref param, out sign);

            JObject jo = new JObject();
            foreach (var item in param)
            {
                jo[item.Key] = item.Value;
            }
            req = jo.ToString();
            tbReq.Text = req;
            DoRequestToAnotherServer(out msg, out resp, "http://prep.tingjiandan.com/openapi/gateway", req);
            jo = JObject.Parse(resp);
            tbResp.Text = jo.ToString();

        }
        public void SignParam(ref Dictionary<string, string> param, out string sign)
        {
            sign = string.Empty;
            param.Add("version", "1.0");
            param.Add("charset", "utf-8");
            param.Add("partner", tbUsr.Text);
            param = param.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (var item in param)
            {
                if (item.Value != "")
                    sign += item.Key + "=" + item.Value + "&";
            }
            sign = sign.Substring(0, sign.Length - 1) + tbKey.Text;
            sign = MD5(sign);
            param.Add("signType", "md5");
            param.Add("sign", sign);
        }
        public string MD5(string s)
        {
            byte[] result = Encoding.UTF8.GetBytes(s);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");

        }
        private static bool DoRequestToAnotherServer(out string msg, out string respXml, string serviceUrl, string reqXml)
        {
            msg = string.Empty;
            DateTime timeBegin = DateTime.Now;
            respXml = string.Empty;
            bool ok = false;
            string errorLog = string.Empty;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serviceUrl);
                req.Method = "POST";
                req.Timeout = 60000;
                req.ContentType = "application/json;charset=UTF-8";

                StringBuilder sb = new StringBuilder();
                sb.Append(reqXml);
                //byte[] reqBytesReqXml = Encoding.Default.GetBytes(reqXml);
                //sb.Append("BFAPPXML").Append(appCode).Append(reqBytesReqXml.Length.ToString("d8")).Append(reqXml);
                byte[] reqBytes = Encoding.UTF8.GetBytes(sb.ToString());
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(reqBytes, 0, reqBytes.Length);

                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string respStr = reader.ReadToEnd();
                respXml = respStr;
            }
            catch (Exception e)
            {
                errorLog = string.Format("访问服务失败 {0}。{1}", serviceUrl, e.Message);
                msg = "访问停车场系统失败，失败原因：" + e.Message;
            }
            DateTime timeEnd = DateTime.Now;
            StringBuilder logStr = new StringBuilder();
            //logStr.Append("\r\n begin ").Append(timeBegin.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //logStr.Append("\r\n url:").Append(serviceUrl);
            //logStr.Append("\r\n req:\r\n ").Append(reqXml);
            //logStr.Append("\r\n resp:\r\n ").Append(respXml);
            //logStr.Append("\r\n end ").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //logStr.Append(", ").Append(timeEnd.Subtract(timeBegin).TotalMilliseconds.ToString("f0")).Append(" ms");
            //logStr.Append("\r\n");
            //CrmAppServerPlatform.WriteDataLog(timeBegin.ToString("yyyy-MM-dd"), logStr.ToString());
            //if (errorLog.Length > 0)
            //{
            //    logStr.Length = 0;
            //    logStr.Append("\r\n request ").Append(appCode).Append(",").Append(timeBegin.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //    logStr.Append("\r\n").Append(reqXml);
            //    logStr.Append("\r\n error ").Append(timeEnd.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //    logStr.Append(", ").Append(timeEnd.Subtract(timeBegin).TotalMilliseconds.ToString("f0")).Append(" ms");
            //    logStr.Append("\r\n").Append(errorLog);
            //    CrmAppServerPlatform.WriteErrorLog(logStr.ToString());
            //}
            return (msg.Length == 0);
        }

    }
}
