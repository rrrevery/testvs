using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;


namespace BF.CrmWeb.CrmLib
{
    /// <summary>
    /// CRMBase 的摘要说明
    /// </summary>
    public class CRMBaseProc : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                bool isOk = false;
                //重新修改，查询改成和其他单据一样的方式
                string msg = string.Empty, outdata = string.Empty;
                string jsonValue = string.Empty;
                //string A = context.Request.Params["json"];
                if (context.Request.Params["json"] != null && context.Request.Params["json"] != "{}")
                    jsonValue = context.Request.Params["json"];//.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}");
                if (jsonValue == "")
                    jsonValue = "{\"SearchConditions\":[]}";

                context.Response.Charset = "UTF-8";//GB2312
                if (context.Session["User_Information"] == null)
                {
                    context.Response.Write("错误:请重新登录后再操作");
                    return;
                }
                //ConditionCollection cc = new ConditionCollection();
                //if (context.Request.Params["mode"] == "Search" || context.Request.Params["mode"] == "ExportMember" || context.Request.Params["mode"] == "Export")//updateValue == string.Empty
                //{
                //    if (context.Request.Params["mode"] == "Search" || context.Request.Params["mode"] == "ExportMember")
                //    {
                //        CrmLibProc.MakeSrchCondition(cc, context.Request.Params["afterFirst"], null);
                //    }
                //    else
                //    {
                //        CrmLibProc.MakeSrchCondition(cc, context.Request.Form["afterFirst"], null);
                //    }
                //    //,'iLoginRYID':" + context.Request.Params["RYID"];
                //    jsonValue = "{'iJLBH':'0'";
                //    //if (context.Request.Params["RYID"] != null)
                //    //    updateValue += ",'iLoginRYID':" + context.Request.Params["RYID"];
                //    //if (context.Request.Params["sColModels"] != null)
                //    //    updateValue += ",'sColModels':'" + context.Request.Params["sColModels"] + "'";
                //    //if (context.Request.Params["sColNames"] != null)
                //    //    updateValue += ",'sColNames':'" + context.Request.Params["sColNames"] + "'";

                //    if (context.Request.Form["RYID"] != null)
                //        jsonValue += ",'iLoginRYID':" + context.Request.Form["RYID"];
                //    if (context.Request.Form["sColModels"] != null)
                //        jsonValue += ",'sColModels':'" + context.Request.Form["sColModels"] + "'";
                //    if (context.Request.Form["sColNames"] != null)
                //        jsonValue += ",'sColNames':'" + context.Request.Form["sColNames"] + "'";
                //    if (context.Request.Form["bShowPublic"] != "" && context.Request.Form["bShowPublic"] != null && context.Request["bShowPublic"]!="undefined")
                //        jsonValue += ",'bShowPublic':'" + context.Request.Form["bShowPublic"] + "'";

                //    if (context.Request.Params["SearchMode"] != null)
                //        jsonValue += ",'iSEARCHMODE':'" + context.Request.Params["SearchMode"] + "'";
                //    if (context.Request.Params["conditionData"] != null && context.Request.Params["conditionData"] != "null" && context.Request.Params["conditionData"] != "" && context.Request.Params["conditionData"] != "\"\"")
                //    {
                //        jsonValue += context.Request.Params["conditionData"].Replace("\\\"", "\"").Replace("\"{", ",").Replace("}\"", "}");
                //    }
                //    else
                //    {
                //        jsonValue += "}";
                //    }
                //}
                BASECRMClass obj = new BASECRMClass();
                int tMsgID = 0;
                if (int.TryParse(context.Request["func"], out tMsgID))
                {
                    ExecFunc(tMsgID, jsonValue, out obj);
                    if (obj == null)
                    {
                        context.Response.Write("错误:未找到对应菜单号：" + tMsgID);
                        return;
                    }
                    obj.iPageMsgID = tMsgID;
                    //obj.sIPAddress = GetRequesterIP();
                    isOk = obj.ProcRequest(context.Request, out msg, out outdata);
                    if (isOk)
                    {
                        if (context.Request["mode"] == "View" || context.Request["mode"] == "Search" || context.Request["mode"] == "Print")
                            //context.Response.Write(outdata.Replace("\\\"", "\"").Replace("\"{", "{").Replace("}\"", "}"));
                            context.Response.Write(outdata);
                        else if ((obj.iJLBH != 0 || obj.sJLBH != "") && context.Request["mode"] != "Delete" && context.Request["mode"] != "Export")
                        {
                            if (obj.iJLBH == 0)
                                context.Response.Write("jlbh=" + obj.sJLBH);
                            else
                                context.Response.Write("jlbh=" + obj.iJLBH);

                        }
                        else if (context.Request["mode"] == "ExportMember")
                            context.Response.Write(outdata);
                        else if (context.Request["mode"] == "Export")
                        {

                            context.Response.Buffer = true;
                            context.Response.Charset = "UTF-8";//GB2312
                            context.Response.Headers.Add("Content-Disposition", "attachment;filename=" + outdata + "");
                            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
                            context.Response.HeaderEncoding = System.Text.Encoding.UTF8;
                            context.Response.ContentType = "application/ms-excel";// application/ms-excel text/plain
                            context.Response.WriteFile("../../temp/" + outdata + "");
                            // context.Response.Write(outdata);
                        }
                        else
                            context.Response.Write("yes");
                        return;
                    }
                    else
                    {
                        context.Response.Write("错误:" + msg);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                string str = e.Message;
                if (e is Newtonsoft.Json.JsonException)
                    str = "JSON解析错误，请检查输入数据，错误信息(" + e.Message + ")";
                context.Response.Write("错误:" + str);
                return;
            }
        }

        public virtual void ExecFunc(int msgid, string updateValue, out BASECRMClass _class)
        {
            _class = null;
            return;
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