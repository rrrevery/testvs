using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

//using BF.CrmProc.KFPT;

namespace BF.CrmWeb.JKPT
{
    /// <summary>
    /// JKPT 的摘要说明
    /// </summary>
    public class JKPT : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_JKPT_YJGZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_YJGZDEF_Srch>(updateValue);    //预警规则定义
                    break;
                case BasePage.CM_JKPT_YJHYR:
                case BasePage.CM_JKPT_YJHYRZB:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_YJHYR_Srch>(updateValue);    //预警会员日
                    break;
                case BasePage.CM_JKPT_YJHYY://989898
                case BasePage.CM_JKPT_YJHYYZB:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_YJHYY_Srch>(updateValue);    //预警会员月数据
                    break;
                case BasePage.CM_JKPT_MDXFR:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_MDXFCSJKR_Srch>(updateValue);     //门店消费次数监控日
                    break;
                case BasePage.CM_JKPT_MDXFY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_MDXFCSJKY_Srch>(updateValue);     //门店消费次数监控月
                    break;
                case BasePage.CM_JKPT_SKTXFR:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_SKTXFCSJKR_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_SKTXFR_1:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_DSKTXFCSJKR_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_SKTXFY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_SKTXFCSJKY_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_SKTXFY_1:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_DSKTXFCSJKY_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_BMXFR:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_BMXFCSJKR_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_BMXFR_1:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_DBMXFCSJKR_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_BMXFY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_BMXFCSJKY_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_BMXFY_1:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_DBMXFCSJKY_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_MDPMR:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_MDPMR_Srch>(updateValue);      //会员门店消费日排名
                    break;
                case BasePage.CM_JKPT_SKTPMR:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_SKTPMR_Srch>(updateValue);      //会员收款台消费日排名
                    break;
                case BasePage.CM_JKPT_BMPMR:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_BMXFPMR_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_MDPMY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_MDPMY_Srch>(updateValue);       //会员门店消费月排名
                    break;
                case BasePage.CM_JKPT_SKTPMY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_SKTPMY_Srch>(updateValue);       //会员收款台消费月排名
                    break;
                case BasePage.CM_JKPT_BMPMY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_BMXFPMY_Srch>(updateValue);
                    break;
                case BasePage.CM_JKPT_KYHY:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_KYHY_Proc>(updateValue);
                    break;
                case BasePage.CM_JKPT_XFCSJK:
                    _class = JsonConvert.DeserializeObject<CrmProc.JKPT.JKPT_XFCSJK_Srch>(updateValue);
                    break;

                default:
                    _class = null;
                    break;
            }
            if (msgid != BasePage.CM_JKPT_KYHY)
            {
                _class.sDBConnName = "CRMDBOLD";
            }
            return;
        }
    }
}