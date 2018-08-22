using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

//using BF.CrmProc.MZKGL;

namespace BF.CrmWeb.MZKGL
{
    /// <summary>
    /// MZKGL 的摘要说明
    /// </summary>
    public class MZKGL : CrmLib.CRMBaseProc
    {
        public override void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            switch (msgid)
            {
                case BasePage.CM_HYKGL_HYKJK_XK:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_HYKJKZK>(updateValue);
                    break;
                //case BasePage.CM_MZKGL_SKGLYDY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKGLYDY_Proc>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKSKKHMXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKSKKHMXCX_Srch>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKSKZFMXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKSKZFMXCX_Srch>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKSKZQMXCX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKSKZQMXCX_Srch>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_AMDTJMZKFS:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_AMDTJMZKFS_Srch>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_AKHTJSKHZQK:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_AKHTJSKHZQK_Srch>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKJEZBB:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JEZBB_Srch>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKJKDNEW:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJKDNEW_Proc>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_JKDZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JKDZ_Proc>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_ZFFS:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_ZFFS_Proc>(updateValue);
                //    break;

                //case BasePage.CM_MZKGL_MZKJKD:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJKD_Proc>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_MZKPDQCL:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPDQCL>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKPDLR:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPDLR>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKPDHZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPDHZ>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKPDSY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPDSY>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                //case BasePage.CM_MZKGL_MZKJEDY:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEDY>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                case BasePage.CM_MZKGL_MZKSKQKCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKSKQKCX>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                //case BasePage.CM_MZKGL_ASHTJMZKXF:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_ASHTJMZKXF>(updateValue);
                //    _class.sDBConnName = "CRMDB";
                //    break;
                case BasePage.CM_MZKGL_QKCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKQK>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_CKCL:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKCK>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_CZKXYMDDEF:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKXYMDSZ_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                //case BasePage.CM_MZKGL_FS:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKFS.MZKGL_MZKFS_Proc.SKBill>(updateValue);
                //    break;
                case BasePage.CM_MZKGL_MZKJEZC:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEZC>(updateValue);
                    break;
                //case BasePage.CM_MZKGL_MZKCLJL_CX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEZCLJL>(updateValue);
                //    break;
                case BasePage.CM_MZKGL_JEZRBB:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JEZRBB_Srch>(updateValue);
                    break;
                case BasePage.CM_MZKGL_JEZYBB:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JEZYBB_Srch>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKYEQL:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKYEQL_Proc>(updateValue);
                    break;
                case BasePage.CM_MZKGL_SKDPLQD:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKDPLQD_Proc>(updateValue);
                    break;
                //case BasePage.CM_MZKGL_SKDPLQD_CX:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKDPLQD>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_MZKCZCL:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKCZCL>(updateValue);
                //    break;
                case BasePage.CM_MZKGL_MZKJEPLZC:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEPLZC>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_XFCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKXFCX_Srch>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKBKCK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKBKCK>(updateValue);
                    break;
                case BasePage.CM_MZKGL_DQYECX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKYECX>(updateValue);
                    break;
                case BasePage.CM_MZKGL_DQYEHZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKYEHZ>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKZF:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKZF>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKDKHGL_DKHZLLR:
                case BasePage.CM_MZKGL_DKHQYKHDA:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KHDALR_Proc>(updateValue);
                    break;
                //case BasePage.CM_MZKGL_DKHSXHT:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_DKHSXHT>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_DKHSXD:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_DKHSXD>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_MZKMRCZJEXZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKMRCZJEXZ>(updateValue);
                //    break;
                case BasePage.CM_MZKGL_JEZ_XFTJ:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKXFTJ_Srch>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKZPQD:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKZPQD_Proc>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKHS:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKHS_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                //case BasePage.CM_MZKGL_MZKZSGZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKZSGZ>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_SKJKD:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKJKD>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_JKDPLQD:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JKDPLQD_Proc>(updateValue);
                //    break;
                case BasePage.CM_MZKGL_FSNEW:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKFS>(updateValue);
                    break;
                case BasePage.CM_MZKGL_TS:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKTS>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKZPPLQD:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPLQD>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKQKGX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKQKGX>(updateValue);
                    break;
                case BasePage.CM_MZKGL_MZKHK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKHK>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKGS:
                case BasePage.CM_MZKGL_MZKGSHF:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKGS>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_HYKGL_MZKZTBD:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_ZTBD_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_JEZHCLJLCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEZCLJL>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_FSCK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPLCK>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_TSCK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKPLQK_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKDKHGL_CZKZKGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKZKGZDY_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKDKHGL_CZKZJFGZ:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKZJFGZDY_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                //case BasePage.CM_MZKDKHGL_CZKZMZKGZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKDKHGL.MZKDKHGL_CZKZKGZDY>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                case BasePage.CM_MZKGL_MZKJK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJK_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKLQ:
                case BasePage.CM_MZKGL_MZKTL:
                case BasePage.CM_MZKGL_MZKDB:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKLQ_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKKCBB:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KCKHZ_Srch>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKBGKC_CX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KCKCX_Srch>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKBGZBB:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KCKBGZ_Srch>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_KCKYETZD:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KCKYETZD_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                //case BasePage.CM_MZKGL_KCKYXQBG:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KCKYXQ>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                case BasePage.CM_MZKGL_KCKZF:
                case BasePage.CM_MZKGL_KCKZF_CX:
                case BasePage.CM_MZKGL_KCKZFHF:
                case BasePage.CM_MZKGL_KCKZFHF_CX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_KCKZF_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_YXQGG:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_YXQYC_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKLYSQ:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKLYSQ_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKASKFSCXSKXX:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKASKFSCXSKXX_Srch>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                //case BasePage.CM_MZKGL_SKTJKCSR:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKTJKCSR>(updateValue);
                //    break;
                //case BasePage.CM_MZKGL_MZKJKDZ:
                //    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJKDZ>(updateValue);
                //    _class.sDBConnName = "CRMDBMZK";
                //    break;
                case BasePage.CM_HYKGL_KCKCX:
                case BasePage.CM_HYKGL_HYKCX:
                case BasePage.CM_CRMART_YHQZHCX:
                    _class = JsonConvert.DeserializeObject<CrmProc.HYKGL.HYKGL_CXKXX>(updateValue);
                    break;
                case BasePage.CM_MZKGL_FSZXGZDY:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKZXGZDY_Proc>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKKFP:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKKFP>(updateValue);
                    _class.sDBConnName = "CRMDBMZK";
                    break;
                case BasePage.CM_MZKGL_MZKCQK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKCQK>(updateValue);
                    break;

                case BasePage.CM_MZKGL_ASKFSTJMZKCQK:
                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_ASKFSTJMZKCQK>(updateValue);
                    break;
                default:
                    _class = null;
                    break;
            }
            return;
        }
        //public void ProcessRequest(HttpContext context)
        //{
        //    try
        //    {
        //        bool isOk = false;
        //        string msg = string.Empty, outdata = string.Empty;
        //        string updateValue = string.Empty;
        //        if (context.Request.Params["json"] != null)
        //            updateValue = context.Request.Params["json"].Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}");
        //        ConditionCollection cc = new ConditionCollection();
        //        if (updateValue == string.Empty)
        //        {
        //            CrmLibProc.MakeSrchCondition(cc, context.Request.Params["afterFirst"], null);
        //            updateValue = "{'iJLBH':'0'";

        //            if (context.Request.Params["SearchMode"] != null)
        //                updateValue += ",'iSEARCHMODE':'" + context.Request.Params["SearchMode"] + "'";
        //            if (context.Request.Params["conditionData"] != null && context.Request.Params["conditionData"] != "null" && context.Request.Params["conditionData"] != "")
        //            {
        //                updateValue += context.Request.Params["conditionData"].Replace("\\\"", "\"").Replace("\"{", ",").Replace("}\"", "}");
        //            }
        //            else
        //            {
        //                updateValue += "}";
        //            }
        //        }
        //        BASECRMClass _class = new BASECRMClass();
        //        if (CrmBaseFc.IsInteger(context.Request["func"]))
        //        {
        //            switch (Convert.ToInt32(context.Request["func"]))
        //            {
        //                case BasePage.CM_MZKGL_QKCL:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKQK>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_CKCL:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKCK>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_CZKXYMDDEF:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_CZKXMDSZ>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_FS:
        //                    {

        //                        _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKFS.MZKGL_MZKFS_Proc.SKBill>(updateValue);
        //                        break;
        //                    }
        //                case BasePage.CM_MZKGL_MZKJEZC:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEZC>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKCLJL_CX:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKJEZCLJL>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_JEZRBB:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JEZRBB>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_JEZYBB:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JEZYBB>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKYEQL:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKYEQL>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_SKDPLQD:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKDPLQD>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_SKDPLQD_CX:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKDPLQD>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKCZCL:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKCZCL>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_XFCX:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKXFCX>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKBKCK:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKBKCK>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_DQYECX:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKYECX>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_DQYEHZ:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKYEHZ>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKZF:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKZF>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_DKHQYKHDA:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_DKHQYKHDA>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_DKHSXHT:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_DKHSXHT>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_DKHSXD:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_DKHSXD>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKMRCZJEXZ:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKMRCZJEXZ>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_JEZ_XFTJ:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKXFTJ>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKZPQD:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKZPQD>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKHS:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKHS>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_MZKZSGZ:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_MZKZSGZ>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_SKJKD:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_SKJKD>(updateValue);
        //                    break;
        //                case BasePage.CM_MZKGL_JKDPLQD:
        //                    _class = JsonConvert.DeserializeObject<CrmProc.MZKGL.MZKGL_JKDPLQD_Proc>(updateValue);
        //                    break;

        //                default:
        //                    context.Response.Write("错误:未找到对应菜单号" + msg);
        //                    return;
        //            }
        //            isOk = _class.ProcRequest(context.Request, out msg, out outdata, cc);
        //            if (isOk)
        //            {
        //                if (context.Request["mode"] == "View" || context.Request["mode"] == "Search")
        //                    context.Response.Write(outdata.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}"));
        //                else if (_class.iJLBH != 0 && context.Request["mode"] != "Delete")
        //                    context.Response.Write("jlbh=" + _class.iJLBH);
        //                else
        //                    context.Response.Write("yes");
        //                return;
        //            }
        //            else
        //            {
        //                context.Response.Write("错误:" + msg);
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        string str = e.Message;
        //        if (e is Newtonsoft.Json.JsonException)
        //            str = "JSON解析错误，请检查输入数据，错误信息(" + e.Message + ")";
        //        context.Response.Write("错误:" + str);
        //        return;
        //    }
        //}

        //public bool IsReusable
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}
    }
}