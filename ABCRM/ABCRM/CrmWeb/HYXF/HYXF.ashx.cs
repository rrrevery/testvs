using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using BF.CrmProc.HYXF;

namespace BF.CrmWeb.HYXF
{
    /// <summary>
    /// HYXF 的摘要说明
    /// </summary>
    public class HYXF : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {

            switch (msgid)
            {
                case BasePage.CM_HYXF_JFBD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFBDD_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_JFTZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFTZD_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_JFZC:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFZCD_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYZDY:
                case BasePage.CM_HYXF_HYZDY_DT:
                case BasePage.CM_HYXF_HYZDY_CX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYZDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYZLXDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYZLXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_JFCLGZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFFLGZ>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYKJFHBZX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFFLZX>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYKJFBDJCLCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYKJFBDCLJL>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYXFMXCX:
                case BasePage.CM_HYKGL_THJLCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_XFJLCX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYXFMXQJHZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_XFMXHZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYXFMXQJHZDBQ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYXFQJHZDBQ>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYXFMXYBLB:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_XFMXCX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYKYQMJFGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_QMFDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_CRMYQMJF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_QMF_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_BMXF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_BMXFHZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYXF_BMKLXXF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_BMKLXXFHZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYXF_BMSBXF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_BMPPXFHZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYXF_HYTDJFDYD:
                case BasePage.CM_HYXF_HYTDZKDYD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_TDJFZKD>(updateValue);
                    break;

                //case BasePage.CM_HYXF_FLFHSJTJ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFFLFQHZ>(updateValue);
                //    break;
                //case BasePage.CM_HYXF_THQMFBB:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_QMFBB_Srch>(updateValue);
                //    break;
                case BasePage.CM_HYXF_QZLXDEF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_QZLXDEF_Proc>(updateValue);
                    break;
                //case BasePage.CM_HYXF_QZCXHDDEF:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_QZCXHDDEF>(updateValue);
                //    break;
                //case BasePage.CM_HYXF_SPJFXXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_SPJFXXCX>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_SHOWCURRFFD:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_FQDYD2>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_SHOWCURRSYD:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_YQDYD2>(updateValue);
                //    break;
                case BasePage.CM_HYXF_QZXXDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_QZXXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_JFHZCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFHZCX_Srch>(updateValue);
                    break;
                //case BasePage.CM_HYXF_HYZXSPDY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYZXSPDYD_Proc>(updateValue);
                //    break;
                ////case BasePage.CM_HYXF_JFTZCLCS:
                //  //  _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFTZCL_Proc>(updateValue);
                //  //  break;
                //case BasePage.CM_HYXF_HYJSPXSCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYJSPXSCX>(updateValue);
                //    break;
                //case BasePage.CM_HYXF_HYZXSPXSCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_HYZXSPXSCX>(updateValue);
                //    break;
                //case BasePage.CM_HYXF_GYSRQDNMRJF:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_GYSRQDNMRJF>(updateValue);
                //    break;
                case BasePage.CM_HYXF_SrchCurrJFGZ:
                case BasePage.CM_HYXF_JFGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_JFDYD>(updateValue);
                    break;
                case BasePage.CM_HYXF_XFZKL:
                case BasePage.CM_HYXF_SrchCurrZKLGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_ZKDYD>(updateValue);
                    break;
                case BasePage.CM_YHQGL_YHQSYDYD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_YQDYD>(updateValue);
                    break;
                case BasePage.CM_YHQGL_YHQFFDYD:
                case BasePage.CM_YHQGL_YHQFFDYD_LP:
                case BasePage.CM_YHQGL_YHQFFDYD_CJ:
                case BasePage.CM_YHQGL_YHQFFDYD_JF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_FQDYD>(updateValue);
                    break;
                case BasePage.CM_YHQGL_CXMBJZDYD:
                case BasePage.CM_YHQGL_CXMDDYD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_MBJZDYD>(updateValue);
                    break;
                case BasePage.CM_HYXF_BJF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_BJF_Proc>(updateValue);
                    break;
                case BasePage.CM_HYXF_SHJFGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYXF.HYXF_SHJFGZ_Proc>(updateValue);
                    break;

                default:
                    _class = null;
                    break;
            }
            return;
        }
    }
}
