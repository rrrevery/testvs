using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using BF.CrmProc.CRMGL;

namespace BF.CrmWeb.CRMGL
{
    /// <summary>
    /// CRMGL 的摘要说明
    /// </summary>
    public class CRMGL : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_CRMGL_SHDEF:
                    _class = JsonConvert.DeserializeObject<CRMGL_SHDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_MDDEF:
                    _class = JsonConvert.DeserializeObject<CRMGL_MDDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_YHQDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_YHQDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_HYQYDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_HYQYDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_FXDW:
                    _class = JsonConvert.DeserializeObject<CRMGL_FXDWDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_HYNLDDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_NLDDY>(updateValue);
                    break;
                //case BasePage.CM_CRMGL_GXSHDEF:
                //    _class = JsonConvert.DeserializeObject<CRMGL_GXSHDY_Proc>(updateValue);
                //    break;
                case BasePage.CM_CRMGL_SHZFFSDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_SHZFFS>(updateValue);
                    break;
                case BasePage.CM_CRMGL_ZFFSDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_ZFFSDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_DEFYHQSYSH:
                    _class = JsonConvert.DeserializeObject<CRMGL_YHQSYSH>(updateValue);
                    break;
                case BasePage.CM_CRMGL_DEFYHQCXHD:
                    _class = JsonConvert.DeserializeObject<CRMGL_CXHDYHQDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_HYXXXMDEF:
                    _class = JsonConvert.DeserializeObject<CRMGL_HYXXXM>(updateValue);
                    break;
                case BasePage.CM_CRMGL_HYKKZDY:
                case BasePage.CM_CRMGL_MZKKZDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_KZDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_HYKLXDY:
                case BasePage.CM_CRMGL_MZKLXDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_KLXDY>(updateValue);
                    break;
                //case BasePage.CM_CRMGL_CRMToJXC:
                //    _class = JsonConvert.DeserializeObject<CRMGL_JXCDB>(updateValue);
                //    break;
                case BasePage.CM_CRMGL_CheckXTSJ:
                    _class = JsonConvert.DeserializeObject<CRMGL_XTSJPHJC>(updateValue);
                    break;
                case BasePage.CM_CRMGL_YHXXDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_YHXXDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_CRMQXDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_CRMCZQX>(updateValue);
                    break;
                case BasePage.CM_CRMGL_XTCSDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_XTCSDY>(updateValue);
                    break;
                //case BasePage.CM_CRMGL_GXSJDY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_GXSJDY>(updateValue);
                //    break;
                //case BasePage.CM_CRMGL_FXQDDY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_FXQDDY>(updateValue);
                //    break;
                //case BasePage.CM_CRMGL_SQLXDY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_MDSQLX_Proc>(updateValue);
                //    break;
                case BasePage.CM_CRMGL_SQDY:
                    // _class = JsonConvert.DeserializeObject<CRMGL_MDSQ_Proc>(updateValue);
                    _class = JsonConvert.DeserializeObject<CRMGL_SQDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_XQDY:
                    // _class = JsonConvert.DeserializeObject<CRMGL_XQDY>(updateValue);
                    _class = JsonConvert.DeserializeObject<CRMGL_LQXQDY>(updateValue);
                    break;
                //case BasePage.CM_CRMGL_QTKGLY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_QTKGLY>(updateValue);
                //    break;
                case BasePage.CM_CRMGL_YHPOSXX:
                    _class = JsonConvert.DeserializeObject<CRMGL_YHPOSXX>(updateValue);
                    break;
                //case BasePage.CM_CRMGL_ZYDY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_ZYDY>(updateValue);
                //    break;
                //case BasePage.CM_CRMGL_SHZFFSMDJFBL:
                //    _class = JsonConvert.DeserializeObject<CRMGL_SHZFFSMDJFBL_Proc>(updateValue);
                //    break;
                case BasePage.CM_CRMGL_XQSQDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_XQSQDY_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMGL_SPJGDDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_SPJGDDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_DefSD:
                    _class = JsonConvert.DeserializeObject<CRMGL_SDDY>(updateValue);
                    break;
                //case BasePage.CM_CRMGL_DXFS:
                //    _class = JsonConvert.DeserializeObject<CRMGL_DXFS_Proc>(updateValue);
                //    break;
                //case BasePage.CM_CRMGL_SHYHQZFFS:
                //    _class = JsonConvert.DeserializeObject<CRMGL_SHYHQZFFSDY>(updateValue);
                //    break;
                //case BasePage.CM_CRMGL_XTCZY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_CTCZY>(updateValue);
                //    break;
                //case BasePage.CM_CRMGL_RYXXDY:
                //    _class = JsonConvert.DeserializeObject<CRMGL_RYXXDY_Proc>(updateValue);
                //    break
                case BasePage.CM_CRMGL_GXSJDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_GXSJDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_CRMToJXC:
                    _class = JsonConvert.DeserializeObject<CRMGL_JXCDB>(updateValue);
                    break;
                case BasePage.CM_CRMGL_RYXXDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_RYXXDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_FXQDDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_FXQDDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_QTKGLY:
                    _class = JsonConvert.DeserializeObject<CRMGL_QTKGLY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_ZYDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_ZYDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_SQLXDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_MDSQLX_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMGL_SHHTCX:
                    _class = JsonConvert.DeserializeObject<CRMGL_SHHT>(updateValue);
                    break;
                case BasePage.CM_CRMGL_SHSPCX:
                    _class = JsonConvert.DeserializeObject<CRMGL_SHSPCX>(updateValue);
                    break;
                case BasePage.CM_CRMGL_YWYDY:
                    _class = JsonConvert.DeserializeObject<CRMGL_YWYDY>(updateValue);
                    break;
                case BasePage.CM_CRMGL_SPSBCX:
                    _class = JsonConvert.DeserializeObject<CRMGL_SPSBCX>(updateValue);
                    break;
                default:
                    _class = null;
                    break;
            }
            return;
        }
    }
}