using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using BF.CrmProc.GTPT;

namespace BF.CrmWeb.GTPT
{
    /// <summary>
    ///主要用来与本地数据库的交互操作 ,一般都是与微信服务器的数据交互
    /// </summary>
    public class GTPT : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_GTPT_GGDY:
                    _class = JsonConvert.DeserializeObject<GTPT_GGDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXJFDHLPLQD:
                    _class = JsonConvert.DeserializeObject<GTPT_WXJFDHLPLQD_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXJFDHYHQ:
                    _class = JsonConvert.DeserializeObject<GTPT_WXJFDHYHQ_Proc>(updateValue);
                    break;
                //case BasePage.CM_GTPT_WXJFDHLPD:
                //    _class = JsonConvert.DeserializeObject<GTPT_WXJFDHLPD_Proc>(updateValue);
                //    break;
                case BasePage.CM_GTPT_WXYYFWJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXYYFWJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXHF_GZ:
                case BasePage.CM_GTPT_WXHF_MR:
                case BasePage.CM_GTPT_WXHF_GJC:
                case BasePage.CM_GTPT_WXHF_TS:

                    _class = JsonConvert.DeserializeObject<GTPT_WXGZHF_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXKKBKZLJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXKKBKZLJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXSQJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSQJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXLJD:
                    _class = JsonConvert.DeserializeObject<GTPT_WXLJD_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXJPJCDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXJPJCDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXZXHDDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXZXHDDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXYYFWDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXYYFWDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WDCYHCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WDCYHCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WDCZLJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WDCZLJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXCDNRDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXCDNRDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXCDDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXCDDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXJPFFJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXJPFFJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXYHCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXYHCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXGJCCFJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXGJCCFJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXYHGZFX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXYHGZFX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_AKLXFXWXBDHY:
                    _class = JsonConvert.DeserializeObject<GTPT_AKLXFXWXBDHY_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXGZGNFX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXGZGNFX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXBDHYTJ:
                    _class = JsonConvert.DeserializeObject<GTPT_WXBDHYTJ_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WDCMXCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WDCXXJLCX_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WDCTJCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WDCJGTJ_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXKKSFGZ:
                case BasePage.CM_GTPT_WXBKSFGZ:
                    _class = JsonConvert.DeserializeObject<GTPT_WXKKZSJFGZDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXCJDYD:
                case BasePage.CM_GTPT_WXQHBDYD:
                case BasePage.CM_GTPT_WXGGKDYD:
                    _class = JsonConvert.DeserializeObject<GTPT_WXCJDYD_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXCXHD:
                    _class = JsonConvert.DeserializeObject<GTPT_CXHDDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXJB:
                    _class = JsonConvert.DeserializeObject<GTPT_WXJB_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXPPFL:
                    _class = JsonConvert.DeserializeObject<GTPT_WXFL_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXGROUP:
                    _class = JsonConvert.DeserializeObject<GTPT_WXGroup_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXUSER_GROUP:
                    _class = JsonConvert.DeserializeObject<GTPT_WXUser_Group_Proc>(updateValue);
                    break;
                //case BasePage.CM_GTPT_WXGRXX:
                //    _class = JsonConvert.DeserializeObject<GTPT_WXUser_Proc>(updateValue);
                //    break;
                case BasePage.CM_GTPT_WXMESSAGE://群发消息
                case BasePage.CM_GTPT_WXAUTOREPLY://自动回复
                //case BasePage.CM_GTPT_WXCUSTOMERMESSAGE://客服消息
                //    _class = JsonConvert.DeserializeObject<GTPT_WXMessage_Proc>(updateValue);
                //    break;
                //case BasePage.CM_GTPT_WXRESOURCE:
                //    _class = JsonConvert.DeserializeObject<GTPT_WX_Resource_Proc>(updateValue);
                //    break;

                case BasePage.CM_GTPT_WXLCDY:   //微信楼层定义
                    _class = JsonConvert.DeserializeObject<GTPT_WXLCDY_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXMDDY:   //微信门店定义
                    _class = JsonConvert.DeserializeObject<GTPT_WXMDDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXPPSB:
                    _class = JsonConvert.DeserializeObject<GTPT_WXPPSBDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXSPTJ:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSPTJ>(updateValue);
                    break;

                case BasePage.CM_GTPT_WXHYQY:
                    _class = JsonConvert.DeserializeObject<GTPT_HYQYDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXSHLM:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSHLMDY>(updateValue);
                    break;
                //case BasePage.CM_GTPT_WXJTYM:
                //    _class = JsonConvert.DeserializeObject<GTPT_WXJTWYDY>(updateValue);
                //    break;
                case BasePage.CM_GTPT_WXLCSB:
                    _class = JsonConvert.DeserializeObject<GTPT_WXLCSBDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXSQGZ:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSQGZDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXSQDYD:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSQGZDYD>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXQDGZ:

                    _class = JsonConvert.DeserializeObject<GTPT_WXQDSJFGZDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXLBDY:

                    _class = JsonConvert.DeserializeObject<GTPT_WXLBDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXLBFFGZ:

                    _class = JsonConvert.DeserializeObject<GTPT_WXLBFFGZDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WDCDY:

                    _class = JsonConvert.DeserializeObject<GTPT_WDCDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_WDCNRDY:

                    _class = JsonConvert.DeserializeObject<GTPT_WDCNRTKDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_TSLXDY:

                    _class = JsonConvert.DeserializeObject<GTPT_TSLXDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_TSCL:

                    _class = JsonConvert.DeserializeObject<GTPT_TSJL>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXWTDY:   //微信问题定义
                    _class = JsonConvert.DeserializeObject<GTPT_WXWTDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXFXDY:   //微信分享定义
                    _class = JsonConvert.DeserializeObject<GTPT_WXFXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXHYKTPSC:
                    _class = JsonConvert.DeserializeObject<GTPT_WXHYKPTSC>(updateValue);
                    break;
                //case BasePage.CM_GTPT_WXKKBK_CX:
                //    _class = JsonConvert.DeserializeObject<GTPT_WXKKBK_Srch>(updateValue);
                //    break;
                case BasePage.CM_GTPT_WXKBKTPSC:
                    _class = JsonConvert.DeserializeObject<GTPT_WXKBKTPSC>(updateValue);
                    break;
                //case BasePage.CM_GTPT_WXKKBKSJF_CX:
                //    _class = JsonConvert.DeserializeObject<GTPT_WXKBKSJF>(updateValue);
                //    break;
                //case BasePage.CM_GTPT_WXKKBKTJR_CX:
                //    _class = JsonConvert.DeserializeObject<GTPT_KBKTJR>(updateValue);
                //    break;
                case BasePage.CM_GTPT_SBZKDYD:
                    _class = JsonConvert.DeserializeObject<GTPT_SBZKDYD_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_GXHFHDY:
                    _class = JsonConvert.DeserializeObject<GTPT_GXHFHDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_CSDEF:
                    _class = JsonConvert.DeserializeObject<GTPT_CSDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_LMSHLXDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSHLMLXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXYYFWJLCL:
                    _class = JsonConvert.DeserializeObject<GTPT_WXYYFWJLCL_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_LPTPSC:
                    _class = JsonConvert.DeserializeObject<GTPT_LPSCTP_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXSYDHDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXSYDHDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_JFDHLPGZDY:
                    _class = JsonConvert.DeserializeObject<GTPT_JFDHLPGZDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_JFDHLPDYD:
                    _class = JsonConvert.DeserializeObject<GTPT_JFDHLPDYD_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_QMQGZDYD:
                    _class = JsonConvert.DeserializeObject<GTPT_QMQGZDYD_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXQMQJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXQMQJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXXHJFMXCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXXHJFMXCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXZJHYMDCX:
                    _class = JsonConvert.DeserializeObject<GTPT_WXZJHYMDCX_Srch>(updateValue);
                    break;
                case BasePage.CM_GTPT_DQJLCX:
                    _class = JsonConvert.DeserializeObject<GTPT_QDJLCX_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_BQDY:
                    _class = JsonConvert.DeserializeObject<GTPT_BQDY>(updateValue);
                    break;
                case BasePage.CM_GTPT_MEDIADY:
                    _class = JsonConvert.DeserializeObject<GTPT_MEDIADY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_NEWSDY:
                    _class = JsonConvert.DeserializeObject<GTPT_NEWSDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXKLXDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXKLXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_WXQFXXDY:
                    _class = JsonConvert.DeserializeObject<GTPT_WXQFXXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_GTPT_HYDBQ:
                    _class = JsonConvert.DeserializeObject<GTPT_HYPLDBQ>(updateValue);
                    break;

                case BasePage.CM_GTPT_JFDHBLDY:
                    _class = JsonConvert.DeserializeObject<GTPT_JFDHBLDY>(updateValue);
                    break;

                //case BasePage.CM_GTPT_WXKBHYKTF:
                //    _class = JsonConvert.DeserializeObject<GTPT_WXKBHYKTF_Proc>(updateValue);
                //    break;
                default:
                    _class = null;
                    break;
            }
            return;
        }

        /*public void ProcessRequest(HttpContext context)
        {
            try
            {

                bool isOk = false;
                string msg = string.Empty, outdata = string.Empty;
                string updateValue = string.Empty;
                if (context.Request["json"] != null)// 此为json字符串。用在将json字符串转换成类的对象
                    updateValue = context.Request["json"].Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}");
                ConditionCollection cc = new ConditionCollection();
                if (updateValue == string.Empty)
                {
                    CrmLibProc.MakeSrchCondition(cc, context.Request["afterFirst"], null);
                    updateValue = "{'iJLBH':'0'";
                    if (context.Request.Params["SearchMode"] != null)
                        updateValue += ",'iSEARCHMODE':'" + context.Request.Params["SearchMode"] + "'";
                    if (context.Request.Params["conditionData"] != null && context.Request.Params["conditionData"] != "null" && context.Request.Params["conditionData"] != "")
                    {
                        updateValue += context.Request.Params["conditionData"].Replace("\\\"", "\"").Replace("\"{", ",").Replace("}\"", "}");
                    }
                    else
                    {
                        updateValue += "}";
                    }
                }
                BASECRMClass _class = new BASECRMClass();//所有后台操作数据的基类，在子类中重写一些与数据库操作的方法
                if (CrmBaseFc.IsInteger(context.Request["func"]))//根据传过来的值，将json字符串转换为指定的类的对象
                {
                    switch (Convert.ToInt32(context.Request["func"]))
                    {
                        case BasePage.CM_GTPT_WXGROUP:
                            _class = JsonConvert.DeserializeObject<GTPT_WXGroup_Proc>(updateValue);
                            break;
                        case BasePage.CM_GTPT_WXUSER_GROUP:
                            _class = JsonConvert.DeserializeObject<GTPT_WXUser_Group_Proc>(updateValue);
                            break;
                        case BasePage.CM_GTPT_WXGRXX:
                            _class = JsonConvert.DeserializeObject<GTPT_WXUser_Proc>(updateValue);
                            break;
                        case BasePage.CM_GTPT_WXMESSAGE://群发消息
                        case BasePage.CM_GTPT_WXAUTOREPLY://自动回复
                        case BasePage.CM_GTPT_WXCUSTOMERMESSAGE://客服消息
                            _class = JsonConvert.DeserializeObject<GTPT_WXMessage_Proc>(updateValue);
                            break;
                        case BasePage.CM_GTPT_WXRESOURCE:
                            _class = JsonConvert.DeserializeObject<GTPT_WX_Resource_Proc>(updateValue);
                            break;
                    }
                }
                isOk = _class.ProcRequest(context.Request, out msg, out outdata, cc);//调用相应的数据库操作  后台函数

                //返回指定的内容
                if (isOk)
                {
                    if (context.Request["mode"] == "View" || context.Request["mode"] == "Search")
                        context.Response.Write(outdata.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}"));
                    else if (_class.iJLBH != 0 && context.Request["mode"] != "Delete")
                        context.Response.Write("jlbh=" + _class.iJLBH);
                    else
                        context.Response.Write("yes");
                    return;
                }
                else
                {
                    context.Response.Write("错误:" + msg);
                    return;
                }
            }
            catch (Exception e)
            {
                string str = e.Message;
                if (e is Newtonsoft.Json.JsonException)
                    str = "JSON解析错误，请检查输入数据，错误信息(" + e.Message + ")";
                context.Response.Write("错误:" + str);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }*/
    }
}