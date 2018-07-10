using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using BF.CrmProc.YHQGL;

namespace BF.CrmWeb.YHQGL
{
    /// <summary>
    /// HYXF 的摘要说明
    /// </summary>
    public class YHQGL : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_YHQGL_CXHDZT:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_CXHDDY>(updateValue);
                    break;
                case BasePage.CM_YHQGL_DEFJFDXBL:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_JFDXBL>(updateValue);
                    break;
                case BasePage.CM_YHQGL_YHQDEFFFGZ:
                case BasePage.CM_YHQGL_SJFGZ:
                case BasePage.CM_YHQGL_YHQDEFFFGZ_CJ:
                case BasePage.CM_YHQGL_YHQDEFFFGZ_LP:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_YHQFFGZ>(updateValue);
                    break;
                case BasePage.CM_YHQGL_YHQDEFSYGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_YHQSYGZ>(updateValue);
                    break;
                case BasePage.CM_YHQGL_LDZQHDDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_LDZQHDDY>(updateValue);
                    break;
                //case BasePage.CM_YHQGL_LDZQGZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_LDZQGZ>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_LDZQ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_LDZQ>(updateValue);
                //    break;

                case BasePage.CM_HYKGL_YHQ_CurrYE:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_YHQDQYE>(updateValue);
                    break;
                //case BasePage.CM_YHQGL_SrchCXBySP:
                //case BasePage.CM_YHQGL_SrchJCJFBySP:
                //case BasePage.CM_YHQGL_SrchZKBySP:
                //case BasePage.CM_YHQGL_SrchFQBySP:
                //case BasePage.CM_YHQGL_SrchYQBySP:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_SPCXHD_Srch>(updateValue);
                //    break;
                case BasePage.CM_YHQGL_HZCXJLByHT:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_SFQHZ_HT_Srch>(updateValue);
                    break;
                case BasePage.CM_YHQGL_HZCXJLByXSBM:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_SFQHZ_BM_Srch>(updateValue);
                    break;
                case BasePage.CM_YHQGL_HZCXJLBySPFL:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_SFQHZ_SPFL_Srch>(updateValue);
                    break;
                case BasePage.CM_YHQGL_HZCXJLBySP:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_SFQHZ_SP_Srch>(updateValue);
                    break;
                case BasePage.CM_YHQGL_MBJZGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_MBJZGZ>(updateValue);
                    break;
                case BasePage.CM_YHQGL_JFBSGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_JFBSGZ>(updateValue);
                    break;
                case BasePage.CM_YHQGL_YHQYCZZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_YHQYCZZ>(updateValue);
                    break;
                case BasePage.CM_YHQGL_QTJFDHJL:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_QTJFDHJL_Srch>(updateValue);
                    break;
                case BasePage.CM_YHQGL_MZDEF:
                    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_YHQMZDEF>(updateValue);
                    break;
                //case BasePage.CM_YHQGL_DEFCXJFDXBL:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_CXJFDXBL>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_DFQJLXXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_DFQJLXXCX>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_DFQJLXXHZCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_DFQJLXXHZCX>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_JEZJEZCJLCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_JEZJEZCJLCX>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_SQJLHZXXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_SQJLHZXX>(updateValue);
                //    break;
                //case BasePage.CM_YHQGL_YHQCXFX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.YHQGL.YHQGL_YHQCXFX>(updateValue);
                //    break;

                default:
                    _class = null;
                    break;
            }
            return;
        }
    }
}