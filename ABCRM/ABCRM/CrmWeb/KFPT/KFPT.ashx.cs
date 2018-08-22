using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

//using BF.CrmProc.KFPT;

namespace BF.CrmWeb.KFPT
{
    /// <summary>
    /// KFPT 的摘要说明
    /// </summary>
    public class KFPT : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                //case BasePage.CM_KFPT_YRGZAP:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_YRGZAP>(updateValue);
                //    break;
                case BasePage.CM_KFPT_DYHYFX:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_DYHYFX_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBOLD";
                    break;
                case BasePage.CM_KFPT_BMHYXFFX:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_BMHYXFFX>(updateValue);
                    break;
                //case BasePage.CM_KFPT_GZLTJ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_GZLTJ>(updateValue);
                //    break;
                case BasePage.CM_KFPT_CXXX:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_CXXX_Srch>(updateValue);
                    break;
                //case BasePage.CM_KFPT_XFYJ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_XFYJ_Proc>(updateValue);
                //    break;
                case BasePage.CM_KFPT_KHXFDJZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_KHXFDJZDY_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_HYXYDJZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYXYDJZDY_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_DRXFJEYJ:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_DRXFJEYJ_Proc>(updateValue);
                    break;
                //case BasePage.CM_KFPT_HYSJYJ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYSJYJ_Proc>(updateValue);
                //    break;
                case BasePage.CM_KFPT_HYHDDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYHDDY>(updateValue);
                    break;
                //case BasePage.CM_KFPT_KFZDY:   //客服组定义
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_KFZDY_Proc>(updateValue);
                //    break;
                case BasePage.CM_KFPT_KFJLDY:   //会员客服经理定义
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_KFJLDY_Proc>(updateValue);
                    break;
                //case BasePage.CM_KFPT_KFJLXG:   //会员客服经理修改
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_KFJLXG_Proc>(updateValue);
                //    break;
                case BasePage.CM_KFPT_HYHDBM:   //会员活动报名
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYHDBM_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_HDFX:   //活动分析
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HDFX_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_HYHDCJ:    //会员活动参加
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYHDCJ>(updateValue);
                    break;
                case BasePage.CM_KFPT_RWFB:     //任务发布
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_RWFB_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_RWZX:     //任务执行
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_RWZX_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_RWCL:     //任务处理
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_RWCL_Proc>(updateValue);
                    break;
                //case BasePage.CM_KFPT_CXYZXRW:     //查询已执行任务
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_CXYZXRW_Proc>(updateValue);
                //    break;
                case BasePage.CM_KFPT_HYHDLDPS:     //会员活动主管评述
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYHDLDPS_Proc>(updateValue);
                    break;
                case BasePage.CM_KFPT_HYHDHF:     //会员活动回访
                    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYHDHF_Proc>(updateValue);
                    break;
                //case BasePage.CM_KFPT_QTHYXHSB:     //群体会员喜好品牌
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_QTHYXHSB_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_SRHY:     
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_SRHY_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_KHXFDJZFX:    //客户消费等级组分析
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_KHXFDJZFX_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_HYSRDHJC:    //会员生日电话检查
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_HYSRDHJC_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_DQHYDHWH:    //定期会员电话维护
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_DQHYDHWH_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_WXFHYDHWH:  //未消费会员电话维护
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_WXFHYDHWH_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_DHWHJLCX:  
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_DHWHJLCX>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_KFJLHYCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_KFJLHYCX_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_VIPBZXXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_VIPBZXXCX_Proc>(updateValue);
                //    break;
                //case BasePage.CM_KFPT_TSJL:
                //    _class = JsonConvert.DeserializeObject<CrmProc.KFPT.KFPT_TSJL>(updateValue);
                //    break;
                default:
                    _class = null;
                    break;
            }
            return;
        }
    }
}

