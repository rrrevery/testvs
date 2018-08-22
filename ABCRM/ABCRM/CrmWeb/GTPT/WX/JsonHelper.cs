using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

namespace BF.CrmWeb.GTPT.WX
{
    //public class JsonHelper
    //{
    //}

    /// <summary>
    /// Json字符串操作辅助类
    /// </summary>
    public class JsonHelper<T> where T : class, new()
    {
        /// <summary>
        /// 检查返回的记录，如果返回没有错误，或者结果提示成功，则不抛出异常
        /// </summary>
        /// <param name="content">返回的结果</param>
        /// <returns></returns>
        //private static bool VerifyErrorCode(string content)
        //{
        //    if (content.Contains("errcode"))
        //    {
        //        ErrorJsonResult errorResult = JsonConvert.DeserializeObject<ErrorJsonResult>(content);
        //        //非成功操作才记录异常，因为有些操作是返回正常的结果（{"errcode": 0, "errmsg": "ok"}）
        //        if (errorResult != null && errorResult.errcode != ReturnCode.请求成功)
        //        {
        //            string error = string.Format("微信请求发生错误！错误代码：{0}，说明：{1}", (int)errorResult.errcode, errorResult.errmsg);
        //            LogTextHelper.Error(errorResult);

        //            throw new WeixinException(error);//抛出错误
        //        }
        //    }
        //    return true;
        //}


        /// <summary>
        /// 转换Json字符串到具体的对象
        /// </summary>
        /// <param name="url">返回Json数据的链接地址</param>
        /// <returns></returns>
        public static T ConvertJson(string url, string postData)
        {
            HttpHelper helper = new HttpHelper();
            string content = helper.SendPost(url, postData);
         //   VerifyErrorCode(content);

            T a = JsonConvert.DeserializeObject<T>(content);

            return a;
        }

        public static T ConvertJsonGET(string url)
        {
            HttpHelper helper = new HttpHelper();
            string content = helper.SendPost(url);
            //   VerifyErrorCode(content);
            T a = JsonConvert.DeserializeObject<T>(content);

            return a;
        }
    }
}