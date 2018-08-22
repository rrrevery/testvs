using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using BF.CrmProc.LPGL;

namespace BF.CrmWeb.LPGL
{
    /// <summary>
    /// LPGL 的摘要说明
    /// </summary>
    public class LPGL : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_LPGL_GHSDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_GHSDY>(updateValue);
                    break;
                //case BasePage.CM_LPGL_LPSXDY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPSXDY>(updateValue);
                //    break;
                case BasePage.CM_LPGL_LPFLDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPFLDY>(updateValue);
                    break;
                case BasePage.CM_LPGL_JFFHLPJHD:
                case BasePage.CM_LPGL_JFFHLPTHD:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPJHD>(updateValue);
                    break;
                case BasePage.CM_LPGL_JFFHLPLQD:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPLQD>(updateValue);
                    break;
                case BasePage.CM_LPGL_JFFHLPDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPDY>(updateValue);
                    break;
                case BasePage.CM_LPGL_LPFFGZDEF:
                case BasePage.CM_LPGL_JSZLGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPFFGZ_Proc>(updateValue);
                    break;
                case BasePage.CM_LPGL_LPFF_SR:
                case BasePage.CM_LPGL_LPFF_SS:
                case BasePage.CM_LPGL_LPFF_BK:
                case BasePage.CM_LPGL_LPFF_JFFL:
                case BasePage.CM_LPGL_LPFF_LD:
                case BasePage.CM_LPGL_RCLPFF:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPFF>(updateValue);
                    break;
                //case BasePage.CM_LPGL_LPJHDWDY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPJHDWDY_Proc>(updateValue);
                //    break;
                case BasePage.CM_LPGL_JFFHLPKC_CX:
                    _class = JsonConvert.DeserializeObject<LPGL_JFFHLPKC>(updateValue);
                    break;
                case BasePage.CM_LPGL_JFFHLPMX_CX:
                    _class = JsonConvert.DeserializeObject<LPGL_JFFHLPJXC>(updateValue);
                    break;
                case BasePage.CM_LPGL_LPFFHZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_JFFHLPHZ>(updateValue);
                    break;
                case BasePage.CM_LPGL_LPKCBDJL:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPKCBDJL>(updateValue);
                    break;
                case BasePage.CM_LPGL_LPBFCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPZFD>(updateValue);
                    break;
                case BasePage.CM_LPGL_LPPDCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPPDCL>(updateValue);
                    break;
                //case BasePage.CM_LPGL_LPFFHDDY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_LPFFHDDY>(updateValue);
                //    break;
                //case BasePage.CM_LPGL_HDLPFF:
                //    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_HDLPFF>(updateValue);
                //    break;
                case BasePage.CM_CRMART_CXLPXX:
                case BasePage.CM_CRMART_XZJFDHLP:
                    _class = JsonConvert.DeserializeObject<CrmProc.LPGL.LPGL_CXLPXX_Srch>(updateValue);
                    break;
                default:
                    _class = null;
                    break;
            }
            return;
        }
    }
}