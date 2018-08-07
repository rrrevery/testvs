using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using z.SSO;
using z.SSO.Model;
using BF.Pub;
using BF.CrmProc;
using System.Collections;

namespace BF.CrmWeb.LIB
{
    /// <summary>
    /// LIB 的摘要说明
    /// </summary>
    public class LIB : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string msg = string.Empty;
                string outdata = string.Empty;
                string func = context.Request["func"];
                bool bEditorUpload = false;//editor上传
                string json = context.Request.Form["json"];
                if (json == null)
                    json = "";
                CRMLIBASHX obj = JsonConvert.DeserializeObject<CRMLIBASHX>(json);
                Log4Net.D("CrmLib Request, func:" + func + ", json:" + json);
                if (func == "Logout")
                {
                    context.Session["User_Information"] = null;
                    outdata = "ok";
                }
                else if (func == "CheckMenuPermit")
                {
                    //已测试通过，不能用的更新一下bin的LoginServiceLib.dll
                    //web.config的appSettings配<add key="TestModel" value="true"/>为测试模式，跳过平台权限验证
                    //web.config的appSettings配<add key="UsePlatform" value="true"/>为使用平台权限，否则为CRM权限
                    bool bPermit = false;
                    if (obj.iRYID == GlobalVariables.SYSInfo.iAdminID)
                        bPermit = false;
                    else if (GlobalVariables.SYSInfo.bTest)
                        bPermit = true;
                    else if (obj.iRYID <= 0)
                        bPermit = false;
                    else if (System.Configuration.ConfigurationManager.AppSettings["UsePlatform"] == "true")
                    {
                        var emp = UserApplication.GetUser<Employee>();
                        bPermit = emp.HasPermission(obj.iMENUID.ToString()); //获取权限
                        Log4Net.I(emp.Name + "(" + emp.Id + ")," + obj.iMENUID + "," + bPermit);
                        //bPermit = LoginAuthorise.CheckMenuAuthoriseByPersonId(obj.iRYID.ToString(), obj.iMENUID.ToString());
                    }
                    else
                        bPermit = CrmLibProc.CheckMenuPermit(obj.iRYID, obj.iMENUID);
                    outdata = JsonConvert.SerializeObject(bPermit);
                }
                else if (func == "upload")
                {
                    //上传文件
                    if (obj == null)
                    {
                        obj = new CRMLIBASHX();
                        obj.sDir = context.Request["folder"];
                        bEditorUpload = true;
                    }
                    outdata = CrmLibProc.UploadFile2(context, obj);
                }
                else if (func == "GetAPPSettings")
                {
                    //获取Web.config的AppSettings
                    JObject jo = (JObject)JsonConvert.DeserializeObject(json);
                    string key = jo["KEY"].ToString();
                    string val = System.Configuration.ConfigurationManager.AppSettings[key];
                    if (val == null)
                        val = "";
                    outdata = JsonConvert.SerializeObject(val);
                }
                else if (func == "XKXT")
                {
                    //独立写卡程序
                    //todo
                }
                else
                    outdata = LibProc.ProcLibRequest(func, obj);
                Log4Net.D("CrmLib Response, " + outdata);
                //context.Response.Write(outdata);
                if (bEditorUpload)
                {
                    Hashtable hash = new Hashtable();
                    hash["error"] = 0;
                    hash["url"] = outdata;
                    context.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    context.Response.Write(JsonConvert.SerializeObject(hash));
                    context.Response.End();
                }
                else
                    context.Response.Write(outdata);
                //别找了，都挪到LibProc下了，因为我们有个大胆的想法。
                #region
                //switch (func)
                //{
                //    case "CheckMenuPermit":
                //        //已测试通过，不能用的更新一下bin的LoginServiceLib.dll
                //        //web.config的appSettings配<add key="Test" value="true"/>为测试模式，跳过平台权限验证
                //        //web.config的appSettings配<add key="UsePlatform" value="true"/>为使用平台权限，否则为CRM权限
                //        bool bPermit = false;
                //        if (System.Configuration.ConfigurationManager.AppSettings["Test"] == "true")
                //            bPermit = true;
                //        else if (obj.iRYID <= 0)
                //            bPermit = false;
                //        else if (System.Configuration.ConfigurationManager.AppSettings["UsePlatform"] == "true")
                //            bPermit = LoginAuthorise.CheckMenuAuthoriseByPersonId(obj.iRYID.ToString(), obj.iMENUID.ToString());
                //        else
                //            bPermit = CrmLibProc.CheckMenuPermit(obj.iRYID, obj.iMENUID);
                //        outdata = JsonConvert.SerializeObject(bPermit);
                //        break;
                //    //Fill类
                //    case "FillHYKTYPETree":
                //        outdata = CrmLibProc.FillHYKTYPETree(out msg, obj.iRYID, obj.iMODE, obj.iQX == 1);
                //        break;
                //    case "FillBGDDTree":
                //        outdata = CrmLibProc.FillBGDDTree(out msg, obj.iRYID, obj.iMDID, obj.iSK == 1, obj.iZK == 1, true, obj.iQX == 1);
                //        break;
                //    case "FillFXDWTree":
                //        outdata = CrmLibProc.FillFXDW(out msg, obj.iRYID, obj.iQX == 1);
                //        break;
                //    case "FillLPFLTree":
                //        outdata = CrmLibProc.FillLPFLTree(out msg, obj.iBJ_TY);
                //        break;
                //    case "FillZFFSTree":
                //        outdata = CrmLibProc.FillZFFSTree(out msg);
                //        break;
                //    case "FillHYQY":
                //        outdata = CrmLibProc.FillHYQY(out msg);
                //        break;
                //    case "FillSH":
                //        outdata = CrmLibProc.FillSH(out msg);
                //        break;
                //    case "FillMD":
                //        outdata = CrmLibProc.FillMD(out msg, obj.iRYID, obj.sSHDM, obj.iQX == 1);
                //        break;
                //    case "FillYHQ":
                //        outdata = CrmLibProc.FillYHQ(out msg, obj.iMODE);
                //        break;
                //    case "FillFXQDTree":
                //        outdata = CrmLibProc.FillFXQD(out msg);
                //        break;
                //    case "FillZYLXTree":
                //        outdata = CrmLibProc.FillZYLXTree(out msg, obj.iRYID);
                //        break;
                //    case "FillKZ":
                //        outdata = CrmLibProc.FillKZ(out msg, obj.iBJ_CZK);
                //        break;
                //    case "FillHYKJC":
                //        outdata = CrmLibProc.FillHYKJC(out msg, obj.iKZID);
                //        break;
                //    case "FillFLGZ":
                //        outdata = CrmLibProc.FillFLGZ(out msg, obj.iHYID, obj.iHYKTYPE);
                //        break;
                //    case "FillKSJHYKTYPE":
                //        outdata = CrmProc.CrmLibProc.GETSJHYKTYPE(out msg, obj.iHYKTYPE, obj.iSJ);
                //        break;
                //    case "FillHYZLXTree":
                //        outdata = CrmLibProc.FillHYZLX(out msg);
                //        break;
                //    case "FillTreeSHBM":
                //        outdata = CrmLibProc.FillSHBM(out msg, obj.sSHDM, obj.iRYID, obj.iLEVEL);
                //        break;
                //    case "FillHD":
                //        outdata = CrmLibProc.FillHD(out msg, obj.iSTATUS);
                //        break;
                //    case "FillBQZ":
                //        outdata = CrmLibProc.FillBQZ(out msg, obj.iLABELXMID);
                //        break;
                //    case "FillBQXMTree":
                //        outdata = CrmLibProc.FillBQXMTree(out msg, obj.iLABELLBID);
                //        break;
                //    case "FillSQTree":
                //        outdata = CrmLibProc.FillSQTree(out msg, obj.iMDID);
                //        break;
                //    case "FillCITY":
                //        outdata = CrmLibProc.FillCITY(out msg);
                //        break;
                //    case "FillJPJC":
                //        outdata = CrmLibProc.FillJPJC(out msg);
                //        break;
                //    //权限
                //    case "FillCheckTreeKLX":
                //        outdata = CrmLibProc.FillKLXQX(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeBGDD":
                //        outdata = CrmLibProc.FillBGDDTreeQX(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeSHBM":
                //        outdata = CrmLibProc.FillSHBMTreeQX(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeMD":
                //        outdata = CrmLibProc.FillMDQX(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeFXDW":
                //        outdata = CrmLibProc.FillFXDWTreeQX(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeKLX_GROUP":
                //        outdata = CrmLibProc.FillKLXQX_GROUP(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeBGDD_GROUP":
                //        outdata = CrmLibProc.FillBGDDTreeQX_GROUP(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeSHBM_GROUP":
                //        outdata = CrmLibProc.FillSHBMTreeQX_GROUP(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeMD_GROUP":
                //        outdata = CrmLibProc.FillMDQX_GROUP(out msg, obj.iRYID);
                //        break;
                //    case "FillCheckTreeFXDW_GROUP":
                //        outdata = CrmLibProc.FillFXDWTreeQX_GROUP(out msg, obj.iRYID);
                //        break;
                //    case "FillQZLX":
                //        outdata = CrmLibProc.FillQZLXMC(out msg, obj.iMDID);
                //        break;
                //    case "FillBQLB":
                //        outdata = CrmLibProc.FillBQLB(out msg);
                //        break;
                //    case "FillLPFFGZ":
                //        outdata = CrmLibProc.FillLPFFGZList(out msg, obj.iGZLX, obj.iHYKTYPE);
                //        break;
                //    case "FillLMSHLX":
                //        outdata = CrmLibProc.FillLMSHLX(out msg);
                //        break;
                //    case "FillWT":
                //        outdata = CrmLibProc.FillWT(out msg, obj.iTYPE, "");
                //        break;
                //    case "FillNBDM":
                //        outdata = CrmLibProc.FillNBDM(out msg, obj.iRYID, obj.sNBDM, obj.iQX == 1);
                //        break;
                //    case "FillWXCDDYTree":
                //        outdata = CrmProc.CrmLibProc.FillWXCDDYTree(out msg, obj.iRYID, obj.iMDID, obj.sURL, obj.iPUBLICID);
                //        break;
                //    case "FillGZ":
                //        outdata = CrmLibProc.FillGZ(out msg);
                //        break;

                //    //Get类
                //    case "GetHYKDEF":
                //        outdata = CrmLibProc.GetHYKDEF(out msg, obj.iHYKTYPE);
                //        break;
                //    case "GetKCKXX":
                //        outdata = CrmLibProc.GetKCKXX(out msg, obj.sCZKHM, obj.sCDNR, obj.sDBConnName);
                //        break;
                //    case "GetKCKKD":
                //        outdata = CrmLibProc.GetKCKKD(out msg, obj.sBGDDDM, obj.sCZKHM_BEGIN, obj.sCZKHM_END, obj.iHYKTYPE, obj.iSTATUS, obj.sDBConnName);
                //        break;
                //    case "getHYXXXM":
                //        outdata = CrmLibProc.getHYXXXM(out msg, obj.iXMLX);
                //        break;
                //    case "getKLXBQ":
                //        outdata = CrmLibProc.getKLXBQ(out msg, obj.iHYKTYPE);
                //        break;
                //    case "GetSRXX":
                //        outdata = CrmLibProc.GetSRXX(out msg, obj.dCSRQ);
                //        break;
                //    case "GetGKDA":
                //        outdata = CrmLibProc.GetGKDA(out msg, obj.sSFZBH, obj.sSJHM);
                //        break;
                //    case "GetKHDAETHDList":
                //        outdata = CrmLibProc.GetKHDAETHDList(out msg, obj.iHYID);
                //        break;
                //    case "GetKHDAQZList":
                //        outdata = CrmLibProc.GetKHDAQZList(out msg, obj.iHYID);
                //        break;
                //    case "GetKHDATJList":
                //        outdata = CrmLibProc.GetKHDATJList(out msg, obj.iHYID);
                //        break;
                //    case "GetHYXX":
                //        outdata = JsonConvert.SerializeObject(CrmLibProc.GetHYXX(out msg, obj.iHYID, obj.sHYK_NO, obj.sCDNR));
                //        break;
                //    case "GetSJGZ":
                //        outdata = JsonConvert.SerializeObject(CrmLibProc.GetSJGZ(out msg, obj.iHYKTYPE, obj.iSJ, obj.fBQJF, obj.fXFJE, obj.iMDID));
                //        break;

                //    case "CheckBGDDQX":
                //        outdata = CrmLibProc.CheckBGDDQX(out msg, obj.sBGDDDM, obj.iRYID);
                //        break;
                //    case "getHYXXXMMC":
                //        outdata = CrmLibProc.getHYXXXMMC(out msg, obj.iXMID);
                //        break;
                //    case "GetHYXF":
                //        outdata = CrmLibProc.GetHYXF(out msg, obj.sSKTNO, obj.iMDID, obj.iXPH, obj.iHYID);
                //        break;
                //    case "GetRCLResult":
                //        outdata = CrmLibProc.GetRCLResult(out msg, obj.dPDRQ);
                //        break;
                //    case "GetCJDYD":
                //        outdata = CrmLibProc.GetCJDYD(out msg, obj.iGZID);
                //        break;
                //    case "GetQMFGZ":
                //        outdata = CrmProc.HYXF.HYXF_QMFDY_Proc.GetQMFGZ(out msg, obj.iHYKTYPE);
                //        break;
                //    case "GetCXHD":
                //        outdata = CrmLibProc.GetCXHD(out msg);
                //        break;
                //    case "Getftpconfig":
                //        outdata = CrmLibProc.Getftpconfig(out msg);
                //        break;
                //    case "GetSPTJINX":
                //        outdata = CrmLibProc.GetSPTJINX(out msg);
                //        break;
                //    case "GetFLGZMXData":
                //        outdata = BF.CrmProc.HYXF.HYXF_JFFLZX.SearchRuleData(obj.iFLGZBH);
                //        break;
                //    case "GetLPFFGZLP":
                //        outdata = CrmLibProc.GetLPFFGZLPList(out msg, obj.iJLBH, obj.sBGDDDM);
                //        break;
                //    case "CheckLPFF":
                //        outdata = CrmProc.LPGL.LPGL_LPFF.CheckRuleCondition(obj.iJLBH, obj.sHYK_NO, obj.sGZLX); //CrmLibProc.GetLPFFGZLPList(out msg, obj.iJLBH, obj.sBGDDDM);
                //        break;
                //    case "GetPath1":
                //        outdata = CrmLibProc.GetPath(out msg);
                //        break;
                //    case "GetSelfInx":
                //        outdata = CrmLibProc.GetSelfInx(out msg, obj.sTABLENAME, obj.sFIELD);
                //        break;
                //    case "GetWXHYXX":
                //        outdata = JsonConvert.SerializeObject(CrmLibProc.GetWXHYXX(out msg, obj.sHYK_NO, obj.sDBConnName));
                //        break;
                //    case "GetWXSIGNData":
                //        outdata = CrmLibProc.GetWXSIGNData(out msg, obj.dKSRQ, obj.dJSRQ);
                //        break;
                //    case "CheckBK":
                //        outdata = CrmLibProc.CheckBK(out msg, obj.iGKID);
                //        break;
                //    case "GetSQDXX":
                //        outdata = CrmLibProc.GetSQDXX(out msg, obj.iJLBH);
                //        break;
                //    case "SavePhoto":
                //        outdata = CrmLibProc.SaveBase64Pic(obj.sData, obj.sDir, obj.sFileName);
                //        break;
                //    case "FillPublicID":
                //        outdata = CrmLibProc.FillPublicID(out msg);
                //        break;
                //    case "GetKCKSL":
                //        outdata = CrmLibProc.GetKCKSL(out msg, obj.iHYKTYPE);
                //        break;
                //    case "GetKCKKD2":
                //        outdata = CrmLibProc.GetKCKKD2(out msg, obj.sBGDDDM, obj.sCZKHM_BEGIN, obj.sCZKHM_END, obj.iHYKTYPE, obj.iSTATUS, obj.sDBConnName);
                //        break;
                //}
                //Log4Net.D("CrmLib Response, " + outdata);
                //context.Response.Write(outdata);
                #endregion
            }

            catch (Exception e)
            {
                string str = e.Message;
                context.Response.Write("错误：" + str);
                return;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}