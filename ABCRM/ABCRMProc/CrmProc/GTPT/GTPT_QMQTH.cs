using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections;
using System.Reflection;
using System.Web;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace BF.CrmProc.GTPT
{
    public class GTPT_QMQTH : DJLR_ZXQDZZ_CLass
    {

        public string code { get; set; }
        public int totalMoney { get; set; }
        public string cardNo { get; set; }
        public int iapiStatus { get; set; }
        public string sERRORM { get; set; }

        public string MESSAGE { get; set; }


        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds).ToString();
        }


        public class CrmHtml5Interface
        {
            public string sign { get; set; }
            public object data { get; set; }
            public int timestamp { get; set; }
            public CrmHtml5Interface()
            {
                this.sign = string.Empty;
                this.data = null;
                this.timestamp = 0;
            }
        }


        public class CrmHtml5Interface_out
        {
            public int apiStatus { get; set; }
            public object data { get; set; }
            public int timestamp { get; set; }
            public string info { get; set; }

            public CrmHtml5Interface_out()
            {
                this.apiStatus = 0;
                this.data = null;
                this.timestamp = 0;
                this.info = string.Empty;
            }
        }

        public class CrmPayRefundcondition
        {
            public string code { get; set; }
            public int totalMoney { get; set; }
            public string cardNo { get; set; }
            public CrmPayRefundcondition()
            {
                this.code = string.Empty;
                this.totalMoney = 0;
                this.cardNo = string.Empty;
            }
        }

        public class DataParam
        {
            public string key = string.Empty;
            public string value = string.Empty;
        }



        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            msg = string.Empty;


            CrmPayRefundcondition obj = new CrmPayRefundcondition();
            obj.code = code;
            obj.cardNo = cardNo;
            obj.totalMoney = totalMoney * 100;

            int timeStamp = Convert.ToInt32(GetTimeStamp());

            string sign = GetSign(obj, timeStamp);

            CrmHtml5Interface obj_content = new CrmHtml5Interface();
            obj_content.sign = sign;
            obj_content.data = obj;
            obj_content.timestamp = timeStamp;

            string content = JsonConvert.SerializeObject(obj_content);
            string a;

            string serverip = "";

            query.SQL.Text = "select serverip from wx_public where publicid=" + iLoginPUBLICID;
            query.Open();
            if (!query.IsEmpty)
            {
                serverip = query.FieldByName("serverip").AsString;
            }
            query.Close();

            SendMessageToService(content, serverip, "CrmHandler.ashx?service=Crm.submitCouponOrderRefund", out a);
            CrmHtml5Interface_out content_out = new CrmHtml5Interface_out();

            content_out = JsonConvert.DeserializeObject<CrmHtml5Interface_out>(a);


            if (content_out.apiStatus == 0)
            { MESSAGE = "退款成功!正确信息已记录"; }
            if (content_out.apiStatus == 1)
            { MESSAGE = "退款失败:" + content_out.info + "错误信息已记录"; }

            iJLBH = SeqGenerator.GetSeq("WX_QMQHTTKJL");
            query.SQL.Text = "insert into WX_QMQHTTKJL(JLBH,apiStatus,DJR,DJRMC,DJSJ,PUBLICID,JE,CODE,HYK_NO,ERRORM)";
            query.SQL.Add("values(:JLBH,:apiStatus,:DJR,:DJRMC,:DJSJ,:PUBLICID,:JE,:CODE,:HYK_NO,:ERRORM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("apiStatus").AsInteger = content_out.apiStatus;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("JE").AsInteger = totalMoney * 100;
            query.ParamByName("CODE").AsString = code;
            query.ParamByName("HYK_NO").AsString = cardNo;
            query.ParamByName("ERRORM").AsString = MESSAGE;
            query.ExecSQL();
        }



        // code是订单号   totalMoney是输入订单金额分  cardNo是卡号
        //获取签名
        public static string GetSign(Object obj, int timeStamp)
        {
            #region 参数排序
            string[] keys = null;
            string[] values = null;
            List<DataParam> Params = new List<DataParam>();
            List<string> listKey = new List<string>();
            List<string> listValue = new List<string>();
            getProperties(obj, Params);
            foreach (DataParam Param in Params)
            {
                if ((Param.value != null) && (Param.value.Length > 0))
                {
                    listKey.Add(Param.key);
                    listValue.Add(Param.value);
                }
            }
            if (listKey.Count > 0)
                keys = listKey.ToArray<string>();
            if (listValue.Count > 0)
                values = listValue.ToArray<string>();
            Array.Sort(keys, values);
            #endregion
            //--组装计算签名的串
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keys.Length; i++)
            {
                sb.Append(keys[i]).Append("=").Append(values[i]).Append("&");
            }
            string bizParas = sb.ToString().Substring(0, sb.ToString().Length - 1);
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(timeStamp.ToString());
            sb2.Append("12345678");
            bizParas = bizParas + sb2.ToString();
            //CrmAppServerPlatform.WriteDataLog(DateTime.Now.ToString("yyyy-MM-dd"), "\r\n MD5 data1:" + bizParas.ToString());
            bizParas = EncodeUrl(bizParas);
            //CrmAppServerPlatform.WriteDataLog(DateTime.Now.ToString("yyyy-MM-dd"), "\r\n MD5 data2:" + bizParas.ToString());
            string signStr = MD5Encrypt(bizParas);
            return signStr;
        }

        private static void getProperties(Object obj, List<DataParam> Params, string parentCode = "")
        {
            PropertyInfo[] properties = obj.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(obj, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    DataParam Param = new DataParam();
                    Params.Add(Param);
                    if (parentCode.Length > 0)
                        Param.key = parentCode + "." + name;
                    else
                        Param.key = name;
                    Param.value = value.ToString();
                }
                else if (item.PropertyType.Name.StartsWith("List"))
                {
                    #region List
                    List<DataParam> Params2 = new List<DataParam>();
                    string tmpStr = item.Name;
                    int i = 0;
                    IEnumerable<object> list = value as IEnumerable<object>;
                    if (list != null)
                    {
                        foreach (var dtItem in list)
                        {
                            PropertyInfo[] properties2 = dtItem.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                            foreach (System.Reflection.PropertyInfo item2 in properties2)
                            {
                                string name2 = item2.Name;

                                object value2 = item2.GetValue(dtItem, null);
                                if (item2.PropertyType.IsValueType || item2.PropertyType.Name.StartsWith("String"))
                                {
                                    DataParam Param = new DataParam();
                                    Params.Add(Param);
                                    if (tmpStr.Length > 0)
                                        Param.key = tmpStr + "[" + i.ToString().PadLeft(3, '0') + "]" + "." + name2;
                                    else
                                        Param.key = name2;
                                    Param.value = value2.ToString();
                                }
                            }
                            i++;
                        }
                    }
                    #endregion
                }
                else if (item.PropertyType.BaseType.Name.StartsWith("Object"))
                {
                    #region object
                    if (value != null)
                    {
                        List<DataParam> Params2 = new List<DataParam>();
                        string tmpStr = item.Name

;
                        getProperties(value, Params2, tmpStr);
                        for (int i = 0; i < Params2.Count; i++)
                        {
                            Params.Add(Params2[i]);
                        }
                    }
                    #endregion
                }
            }
        }

        private static Regex REG_URL_ENCODING = new Regex(@"%[a-f0-9]{2}");

        public static string EncodeUrl(string str)
        {
            if (str == null)
            {
                return null;
            }
            String stringToEncode = HttpUtility.UrlEncode(str, Encoding.UTF8).Replace("+", "%20").Replace("*", "%2A").Replace("(", "%28").Replace(")", "%29");
            return REG_URL_ENCODING.Replace(stringToEncode, m => m.Value.ToUpperInvariant());
        }

        #region MD5 加密（散列码 Hash 加密）
        /// <summary>
        /// MD5 加密（散列码 Hash 加密）
        /// </summary>
        /// <param name="code">明文</param>
        /// <returns>密文</returns>
        public static string MD5Encrypt(string code)
        {
            /* 获取原文内容的byte数组 */
            byte[] sourceCode = Encoding.Default.GetBytes(code);
            byte[] targetCode;    //声明用于获取目标内容的byte数组

            /* 创建一个MD5加密服务提供者 */
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            targetCode = md5.ComputeHash(sourceCode);    //执行加密
            md5.Clear();
            /* 对字符数组进行转码 */
            StringBuilder sb = new StringBuilder();
            foreach (byte b in targetCode)
            {
                sb.AppendFormat("{0:X2}", b);
            }

            return sb.ToString();
        }
        #endregion


        public static void SendMessageToService(string content, string serverip, string urlName, out string a)
        {
            string serviceUrl = "http://" + serverip + "/CrmHandler.ashx?service=Crm.submitCouponOrderRefund";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serviceUrl);
            req.Method = "POST";
            req.Timeout = 6000;
            byte[] reqBytes = Encoding.Default.GetBytes(content.ToString());
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(reqBytes, 0, reqBytes.Length);
            WebResponse resp = req.GetResponse();
            using (StreamReader reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
            {
                a = reader.ReadToEnd();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)

        {



            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.* from WX_QMQHTTKJL W where W.publicId=" + iLoginPUBLICID;
            
            SetSearchQuery(query, lst);
            return lst;    
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_QMQTH obj = new GTPT_QMQTH();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iapiStatus = query.FieldByName("apiStatus").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.totalMoney = query.FieldByName("JE").AsInteger;
            obj.cardNo = query.FieldByName("HYK_NO").AsString;
            obj.sERRORM = query.FieldByName("ERRORM").AsString;

            obj.code = query.FieldByName("code").AsString;



            return obj;
        }


    }
}
