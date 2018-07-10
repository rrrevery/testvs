using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using BF.CrmProc.HYKGL;

namespace BF.CrmWeb.HYKGL
{
    /// <summary>
    /// HYKGL 的摘要说明
    /// </summary>
    public class HYKGL : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_HYKGL_HYKBGDDDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_BGDDDY>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKJK:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKJK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKLQ:
                case BasePage.CM_HYKGL_HYKDB:
                case BasePage.CM_HYKGL_HYKTL:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKLQ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKGS:
                case BasePage.CM_HYKGL_HYKGSHF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKGS>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKGHKLX:
                    _class = JsonConvert.DeserializeObject<HYKGL_GHKLX>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKHK:
                    //case BasePage.CM_HYKGL_DZKZSTK:
                    _class = JsonConvert.DeserializeObject<HYKGL_HYKHK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHCKCL:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQCK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHQKCL:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQQK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKKCBB:
                    _class = JsonConvert.DeserializeObject<HYKGL_KCKHZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKBGZBB:
                    _class = JsonConvert.DeserializeObject<HYKGL_KCKBGZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKBGKC_CX:
                    _class = JsonConvert.DeserializeObject<HYKGL_KCKCX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHRBB:
                case BasePage.CM_HYKGL_YHQZHYBB:
                case BasePage.CM_HYKGL_YHQZHNBB:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQRBB_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHRBB_MD:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQMDRBB_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_SrchZKXX:
                case BasePage.CM_HYKGL_KCKXXYZ:
                case BasePage.CM_HYKGL_HYKBC:
                case BasePage.CM_HYKGL_KCKBC:
                    _class = JsonConvert.DeserializeObject<HYKGL_ZKXX>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKJK_XK:
                    _class = JsonConvert.DeserializeObject<HYKGL_HYKJKZK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHCLJLCX:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQCLJL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYDALR:
                    _class = JsonConvert.DeserializeObject<HYKGL_GKDALR>(updateValue);//
                    break;
                case BasePage.CM_HYKGL_JEZHCLJLCX:
                    _class = JsonConvert.DeserializeObject<HYKGL_HYKJEZHCLJL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JFKFF:
                case BasePage.CM_HYKGL_JFKFF_CX:
                    _class = JsonConvert.DeserializeObject<HYKGL_HYKFF_DZ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JFKPLFF:
                    _class = JsonConvert.DeserializeObject<HYKGL_HYKFF_PL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_KCKZF:
                case BasePage.CM_HYKGL_KCKZFHF:
                    _class = JsonConvert.DeserializeObject<HYKGL_KCKZF>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHPLCKCL:
                case BasePage.CM_HYKGL_YHQZHPLCKCL_CX:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQPLCK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQYEQLTZD:
                    _class = JsonConvert.DeserializeObject<HYKGL_YHQYEQL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKDJSQGZDY:
                case BasePage.CM_HYKGL_HYKJJGZDY:
                    _class = JsonConvert.DeserializeObject<HYKGL_SJJGZ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_KCKCX:
                case BasePage.CM_HYKGL_HYKCX:
                case BasePage.CM_CRMART_YHQZHCX:
                case BasePage.CM_MZKGL_FSNEW:
                case BasePage.CM_GTPT_WXHYKCX:

                    _class = JsonConvert.DeserializeObject<HYKGL_CXKXX>(updateValue);
                    break;
                case BasePage.CM_HYKGL_KCKYETZD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_KCKYETZ_Proc>(updateValue);
                    break;
                case BasePage.CM_CRMART_TREE:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_ARTTREE>(updateValue);
                    break;
                case BasePage.CM_HYKGL_KCKYXQBG:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_KCKYXQ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKZTBD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_ZTBD_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YXQGG:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_YXQYC_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKFS_TJ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKFFTJ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_GHFXDW:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_GHFXDW_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKHS:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKHS_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHPLQKCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_YHQPLQK>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKZF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKZF_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_XGPASSWORD:
                case BasePage.CM_HYKGL_MMCZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_MMXG>(updateValue);
                    break;
                case BasePage.CM_HYKGL_SrchHYKJFRBB:
                case BasePage.CM_HYKGL_SrchHYKJFYBB:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_JFRBB_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_SrchHYKKSJJL:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKKSJJL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_CXHYXX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYXX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYSJHKCL:
                case BasePage.CM_HYKGL_HYJJHKCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKSJ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_SrchKXXByZT:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_CXKXXBYZT>(updateValue);
                    break;
                case BasePage.CM_HYKGL_ZTBGGZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKZTBGGZDY>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JFBDMX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_JFBDJLMX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQBGYXQ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_YHQYXQ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHMX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_YHQZHMX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQZHZC:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_YHQZC>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JFXFMX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_JFXFMX_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_GXSHJFMX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYGXSHJFXF_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JEZCKCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_JEZCKCL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JEZQKCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_JEZQKCL>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYTJGZ:
                    _class = JsonConvert.DeserializeObject<HYKGL_HYTJGZDY>(updateValue);
                    break;
                case BasePage.CM_HYKGL_BQLBDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_BQLBDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_BQXMDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_BQXMDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_BQZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_BQZDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_BQHYCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_BQHYCX>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYPLDBQ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYPLDBQ_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYDBQ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYDBQ_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYBQPLDR:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYBQPLDR>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYJCBQGZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_JCBQGZDY>(updateValue);
                    break;
                case BasePage.CM_HYKGL_ZHBQGZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_ZHBQGZDY>(updateValue);
                    break;
                case BasePage.CM_HYKGL_JEZHHZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKJEZHHZ_Srch>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQ_XFTJ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKYHQXFTJ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQ_XFTJByMD:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKYHQXFTJMD_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQ_XFTJBySH:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKYHQXFTJSH_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQ_QTCLTJ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKYHQQTCLTJ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_YHQ_SSCX_XFTJ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_YHQSSXFTJ>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKLYSQ:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKLYSQ_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKTK:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKTK>(updateValue);
                    break;
                //case BasePage.CM_HYKGL_HYKZR:
                //    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKZR_Proc>(updateValue);
                //    break;
                case BasePage.CM_HYKGL_GKZYDALR:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_GKZYDALR>(updateValue);
                    break;
                case BasePage.CM_HYKGL_BQZKLXDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_BQZKLXDY_Proc>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYDADR:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYDADR>(updateValue);
                    break;
                case BasePage.CM_HYKGL_HYKXF:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKXF>(updateValue);
                    break;
                default:
                    _class = null;
                    break;
            }
            return;
        }
    }
}