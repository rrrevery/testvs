using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;
//using BF.CrmProc.CrmLib;

namespace BF.CrmWeb.CRMREPORT
{
    /// <summary>
    /// CRMREPORT 的摘要说明
    /// </summary>
    public class CRMREPORT : CrmLib.CRMBaseProc
    {

        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_CRMREPORT_RFMJB:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPORT_RFMJBDY_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_RFMFXZB:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPORT_RFMFXZB_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_RFMFXZB_MD:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPORT_RFMFXZBMD_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_RFMQZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.BFMZQDY_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_JJR_RQ_DEF:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPOTR_JJR_RQ_DEF>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_YXHHDEF:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPORT_YXHDDEF>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_JJR_DEF:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPORT_JJRDEF>(updateValue);
                    break;
                case BasePage.CM_CRMREPORT_SQXQFX:
                    _class = JsonConvert.DeserializeObject<CrmProc.CRMREPORT.CRMREPORT_SQXSFX>(updateValue);
                    break;
                default:
                    _class = null;
                    break;
            }

        }

    }
}