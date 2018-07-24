using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Newtonsoft.Json;
using LoginServiceLib;
using BF.Pub;

namespace BF.CrmProc
{
    public static class LibProc
    {
        public static void Initialize()
        {
            GlobalVariables.SYSInfo.bTest = GetWebConfig("TestModel", "false") == "true";
            GlobalVariables.SYSInfo.sPubUser = GetWebConfig("PubUser", "BFPUB");
            GlobalVariables.SYSConfig.sSMSUser = GetWebConfig("SMSUser");
            GlobalVariables.SYSConfig.sSMSPass = GetWebConfig("SMSPass");
            GlobalVariables.SYSConfig.sBMJG = GetWebConfig("BMJG", "22222");
            GlobalVariables.SYSConfig.iBMZJC = GlobalVariables.SYSConfig.sBMJG.Length;
            int a = 0;
            for (int i = 0; i < GlobalVariables.SYSConfig.iBMZJC; i++)
            {
                a += Convert.ToInt32(GlobalVariables.SYSConfig.sBMJG[i].ToString());
                GlobalVariables.SYSConfig.iBMDMZCDs.Add(a);
                GlobalVariables.SYSConfig.iBMDMCDs.Add(Convert.ToInt32(GlobalVariables.SYSConfig.sBMJG[i].ToString()));
            }
            //数据库内容考虑加到单独的方法里，在登录时也调用一遍，防止改完参数没用还得重启服务
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            GlobalVariables.SYSInfo.sProjectKey = GetXMXX(query, 150);
            query.SQL.Text = "select JLBH,CUR_VAL from BFCONFIG";
            query.Open();
            while (!query.Eof)
            {
                switch (query.FieldByName("JLBH").AsInteger)
                {
                    case 0:
                        GlobalVariables.SYSConfig.iDBID = Convert.ToInt32(query.FieldByName("CUR_VAL").AsString);
                        break;
                    case 520000128:
                        GlobalVariables.SYSConfig.bCDNR2JM = query.FieldByName("CUR_VAL").AsString == "1";
                        break;
                    case 520000130:
                        GlobalVariables.SYSConfig.sSJMSG = query.FieldByName("CUR_VAL").AsString;
                        break;
                }
                query.Next();
            }
            query.Close();
            query.SQL.Text = "select * from FTPCONFIG order by ID";
            //GenMenuPermit(query);
            query.Open();
            while (!query.Eof)
            {
                FtpConfig one = new FtpConfig();
                one.iID = query.FieldByName("ID").AsInteger;
                one.sDIR = query.FieldByName("DIR").AsString;
                int t = one.sDIR.LastIndexOf("@");
                one.sUSER = one.sDIR.Substring(0, t);
                one.sURL = one.sDIR.Substring(t + 1, one.sDIR.Length - t - 1);
                one.sPSWD = query.FieldByName("PSWD").AsString;
                one.sFIRST_DIR = query.FieldByName("FIRST_DIR").AsString;
                one.sIP_NET = query.FieldByName("IP_NET").AsString;
                one.sIP_PUB = query.FieldByName("IP_PUB").AsString;
                GlobalVariables.FTPConfig.Add(one);
                query.Next();
            }
            query.Close();
            conn.Close();
        }

        public static void GenMenu()
        {
            //这里将使用PUBDB连接
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("PUBDB");
            CyQuery query = new CyQuery(conn);
            string sMENU = GetWebConfig("MENUSET", "L");
            //菜单模式有三种
            //A全部菜单，即SYSLIB和PROCMSGDEF组成的两级菜单
            //D显示菜单，即MODULE_DEF和MODULE_DEF_ITEM组成的多级菜单
            //F文件菜单，即JS文件
            int sysid = 510;
            List<DisplayMenu> menus = new List<DisplayMenu>();
            try
            {
                //尼玛暂时只做D模式算了
                query.SQL.Text = "select D.* from MODULE_DEF D,MODULE_GROUP G where D.MGRPID=G.MGRPID and G.SYSID=" + sysid + " order by DISPLAY_INX";
                query.Open();
                while (!query.Eof)
                {
                    DisplayMenu one = new DisplayMenu();
                    one.id = "D" + query.FieldByName("ID").AsInteger.ToString();
                    one.pId = "";
                    one.name = query.FieldByName("NAME").AsString;
                    menus.Add(one);
                    query.Next();
                }
                query.Close();
                query.SQL.Text = "select M.*,P.URL from MODULE_DEF_MENU M,MODULE_DEF D,MODULE_GROUP G,PROCMSGDEF P";
                query.SQL.Add("where M.ID=D.ID and D.MGRPID=G.MGRPID and G.SYSID=" + sysid + " and M.MSG_ID=P.MSG_ID(+)");
                query.SQL.Add("order by D.DISPLAY_INX,M.POS");
                query.Open();
                while (!query.Eof)
                {
                    DisplayMenu one = new DisplayMenu();
                    one.sPOS = query.FieldByName("POS").AsString;
                    one.id = "M" + one.sPOS;
                    if (one.sPOS.Length == 2)
                        one.pId = "D" + query.FieldByName("ID").AsInteger.ToString();
                    else
                        one.pId = "M" + one.sPOS.Substring(0, 2);
                    one.name = query.FieldByName("CAPTION").AsString;
                    one.sURL = query.FieldByName("URL").AsString;
                    menus.Add(one);
                    query.Next();
                }

            }
            finally
            {
                query.Close();
                conn.Close();
            }
        }
        public static bool LoginProc(out string msg, out LoginData login, string user, string pass, string str1)
        {
            msg = string.Empty;
            login = new LoginData();
            if (user == "")
            {
                msg = "用户名不能为空!";
                return false;
            }
            if (pass == "")
            {
                msg = "密码不能为空!";
                return false;
            }
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);
            //string sPubUser = GlobalVariables.SYSInfo.sPubUser;
            if (user.ToUpper() == "SUPER")
            {
                if (GlobalVariables.SYSInfo.bTest || pass == GlobalVariables.SYSInfo.sAdminPass)
                {
                    login.iRYID = GlobalVariables.SYSInfo.iAdminID;
                    login.sRYDM = "SUPER";
                    login.sRYMC = "超级用户";
                }
                else
                    msg = "密码错误";
            }
            else
            {
                query.SQL.Text = "select * from XTCZY X,RYXX R";
                query.SQL.Add("where X.PERSON_ID=R.PERSON_ID and R.RYDM=:RYDM ");
                query.ParamByName("RYDM").AsString = user;
                query.Open();
                if (query.IsEmpty)
                    msg = "用户名不存在";
                else
                {
                    string passdb = EncryptUtils.DelphiDecrypt((byte[])query.FieldByName("LOGIN_PASSWORD").Value);
                    if (GlobalVariables.SYSInfo.bTest || passdb == pass)
                    {
                        login.iRYID = query.FieldByName("PERSON_ID").AsInteger;
                        login.sRYDM = query.FieldByName("RYDM").AsString;
                        login.sRYMC = query.FieldByName("PERSON_NAME").AsString;
                        //login.sPUBLICID = query.FieldByName("PUBLICID").AsInteger.ToString();
                        //login.sPUBLICIF = query.FieldByName("PUBLICIF").AsString;
                    }
                    else
                        msg = "密码错误";
                }
            }
            query.Close();
            conn.Close();
            return msg == "";
        }
        public static DataTable GetDataToTable(string sql, ref string msg)
        {
            msg = "";
            DataTable tp_target = new DataTable();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Add(sql);
                    query.Open();
                    tp_target = query.GetDataTable();

                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            return tp_target;
        }
        public static bool CheckSQLInject(string val)
        {
            val = " " + val + " ";
            string[] keyword = new string[] { "insert", "delete", "update", "select", "declare", "drop", "truncate" };
            foreach (string a in keyword)
            {
                if (val.ToLower().IndexOf(" " + a + " ") >= 0)//全字匹配
                    return true;
            }
            return false;
        }
        public static string GetXMXX(CyQuery query, int pID)
        {
            //if (query == null)
            //{
            //    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            //    query = new CyQuery(conn);
            //}
            query.Close();
            query.SQL.Text = "select CONTENT from XTXX where ID=" + pID;
            DataTable dt = query.GetDataTable();
            if (dt.Rows.Count == 0)
                throw new Exception("项目信息不存在");
            string s = EncryptUtils.DelphiDecrypt((byte[])dt.Rows[0]["CONTENT"]);
            query.Close();
            return s;
        }
        public static string GetWebConfig(string pName, string pDefault = "")
        {
            string ret = System.Configuration.ConfigurationManager.AppSettings[pName];
            if (ret == "" || ret == null)
                ret = pDefault;
            return ret;
        }
        //public static PageInfo InitPage2(int iPersonID, int iMenuID)
        //{
        //    PageInfo pi = new PageInfo();
        //    pi.iPageMsgID = iMenuID;
        //    MenuModule menu = GlobalVariables.Menus.Find((MenuModule one) => { return one.iMNUID == iMenuID; });
        //    if (menu != null)
        //    {
        //        pi.bNeedINPUT = menu.iINPUT == 1;
        //        pi.bNeedDEL = menu.iDEL == 1;
        //        pi.bNeedEXECU = menu.iEXECU == 1;
        //        pi.bNeedSTRT = menu.iSTRT == 1;
        //        pi.bNeedSTP = menu.iSTP == 1;
        //        pi.bNeedSRCH = menu.iSRCH == 1;
        //        pi.bNeedPRT = menu.iPRT == 1;
        //        pi.bNeedEXPORT = menu.iEXPORT == 1;
        //        pi.bNeedPERMIT1 = menu.iPERMIT1 == 1;
        //        pi.bNeedPERMIT2 = menu.iPERMIT2 == 1;
        //        pi.bNeedPERMIT3 = menu.iPERMIT3 == 1;
        //        pi.bNeedPERMIT4 = menu.iPERMIT4 == 1;
        //    }
        //    else
        //    {
        //        //按默认值
        //    }
        //    MenuPermit per = GlobalVariables.MenuPermits.Find((MenuPermit one) => { return one.iMNUID == iMenuID && one.iPERSON_ID == iPersonID; });
        //    if (per != null && iPersonID != GlobalVariables.SYSInfo.iAdminID)
        //    {
        //        pi.bCanINPUT = per.iINPUT == 1;
        //        pi.bCanDEL = per.iDEL == 1;
        //        pi.bCanEXECU = per.iEXECU == 1;
        //        pi.bCanSTRT = per.iSTRT == 1;
        //        pi.bCanSTP = per.iSTP == 1;
        //        pi.bCanSRCH = per.iSRCH == 1;
        //        pi.bCanPRT = per.iPRT == 1;
        //        pi.bCanEXPORT = per.iEXPORT == 1;
        //        pi.bCanPERMIT1 = per.iPERMIT1 == 1;
        //        pi.bCanPERMIT2 = per.iPERMIT2 == 1;
        //        pi.bCanPERMIT3 = per.iPERMIT3 == 1;
        //        pi.bCanPERMIT4 = per.iPERMIT4 == 1;
        //    }
        //    else
        //    {
        //        if (iPersonID == GlobalVariables.SYSInfo.iAdminID || bTest)
        //        {
        //            pi.bCanINPUT = true;
        //            pi.bCanDEL = true;
        //            pi.bCanEXECU = true;
        //            pi.bCanSTRT = true;
        //            pi.bCanSTP = true;
        //            pi.bCanSRCH = true;
        //            pi.bCanPRT = true;
        //            pi.bCanEXPORT = true;
        //            pi.bCanPERMIT1 = true;
        //            pi.bCanPERMIT2 = true;
        //            pi.bCanPERMIT3 = true;
        //            pi.bCanPERMIT4 = true;
        //        }
        //        else
        //        {
        //            pi.bCanINPUT = false;
        //            pi.bCanDEL = false;
        //            pi.bCanEXECU = false;
        //            pi.bCanSTRT = false;
        //            pi.bCanSTP = false;
        //            pi.bCanSRCH = false;
        //            pi.bCanPRT = false;
        //            pi.bCanEXPORT = false;
        //            pi.bCanPERMIT1 = false;
        //            pi.bCanPERMIT2 = false;
        //            pi.bCanPERMIT3 = false;
        //            pi.bCanPERMIT4 = false;
        //        }

        //    }
        //    return pi;
        //}

        public static string ProcLibRequest(string func, CRMLIBASHX obj)
        {
            //我们之所以把处理方法挪到这儿，是为了方便统一建立数据库连接，每个方法自己建立连接需要至少10行代码
            //方法通过反射调用，满足要求的按新方法调用，否则还按老方法调用
            //反射调用要求方法名和func一致，参数为3个，且必须是out string msg, CyQuery query, CRMLIBASHX param
            //改好的方法参考FillSH
            //默认为CRMDB连接，如果不一样，应在json里指定sDBConnName
            string outdata = string.Empty;
            string msg = string.Empty; ;
            string sDBConnName = obj.sDBConnName;
            bool found = false;
            if (sDBConnName == string.Empty || sDBConnName == null)
                sDBConnName = "CRMDB";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            CyQuery query = new CyQuery(conn);
            try
            {
                Type type = typeof(CrmLibProc);
                if (type.GetMethod(func) != null)
                {
                    ParameterInfo[] funcparam = type.GetMethod(func).GetParameters();
                    if (funcparam.Length == 3 && funcparam[0].Name == "msg" && funcparam[1].Name == "query" && funcparam[2].ParameterType.Name == "CRMLIBASHX")
                    {
                        //out string msg, CyQuery query, CRMLIBASHX param
                        found = true;
                        object[] param = new object[] { msg, query, obj };
                        outdata = (string)type.GetMethod(func).Invoke(null, param);
                        msg = (string)param[0];
                        if ((outdata == null || outdata == "") && msg != "")
                            outdata = JsonConvert.SerializeObject("错误：" + msg);
                    }
                }
                if (!found)
                    switch (func)
                    {
                        //Get类
                        case "GetMZKKCKKD_FS":
                            outdata = CrmLibProc.GetMZKKCKKD_FS(out msg, obj.sBGDDDM, obj.sCZKHM_BEGIN, obj.sCZKHM_END, obj.iHYKTYPE, obj.iSTATUS, obj.sDBConnName);
                            break;
                        case "GetKHDAETHDList":
                            outdata = CrmLibProc.GetKHDAETHDList(out msg, obj.iHYID);
                            break;
                        case "GetKHDAQZList":
                            outdata = CrmLibProc.GetKHDAQZList(out msg, obj.iHYID);
                            break;
                        case "GetKHDATJList":
                            outdata = CrmLibProc.GetKHDATJList(out msg, obj.iHYID);
                            break;
                        //case "GetSJGZ":
                        //    outdata = JsonConvert.SerializeObject(CrmLibProc.GetSJGZ(out msg, obj.iHYKTYPE, obj.iSJ, obj.fBQJF, obj.fXFJE, obj.iMDID));
                        //    break;

                        case "CheckBGDDQX":
                            outdata = CrmLibProc.CheckBGDDQX(out msg, obj.sBGDDDM, obj.iRYID);
                            break;
                        case "getHYXXXMMC":
                            outdata = CrmLibProc.getHYXXXMMC(out msg, obj.iXMID);
                            break;
                        case "GetHYXF":
                            outdata = CrmLibProc.GetHYXF(out msg, obj.sSKTNO, obj.iMDID, obj.iXPH, obj.iHYID);
                            break;
                        case "GetQMFGZ":
                            outdata = CrmProc.HYXF.HYXF_QMFDY_Proc.GetQMFGZ(out msg, obj.iHYKTYPE);
                            break;
                        case "GetJFDHLPDYD":
                            outdata = CrmLibProc.GetJFDHLPDYD(out msg, obj.iGZID);
                            break;
                        //case "Getftpconfig":
                        //    outdata = CrmLibProc.Getftpconfig(out msg);
                        //    break;
                        case "GetFLGZMXData":
                            outdata = BF.CrmProc.HYXF.HYXF_JFFLZX.SearchRuleData(obj.iFLGZBH);
                            break;
                        case "GetLPFFGZLP":
                            outdata = CrmLibProc.GetLPFFGZLPList(out msg, obj.iJLBH, obj.sBGDDDM);
                            break;
                        case "CheckLPFF":
                            outdata = CrmProc.LPGL.LPGL_LPFF.CheckRuleCondition(obj.iJLBH, obj.sHYK_NO, obj.sGZLX); //CrmLibProc.GetLPFFGZLPList(out msg, obj.iJLBH, obj.sBGDDDM);
                            break;
                        case "GetWXHYXX":
                            outdata = JsonConvert.SerializeObject(CrmLibProc.GetWXHYXX(out msg, obj.sHYK_NO, obj.sDBConnName));
                            break;
                        case "GetWXSIGNData":
                            outdata = CrmLibProc.GetWXSIGNData(out msg, obj.dKSRQ, obj.dJSRQ);
                            break;
                        case "CheckBK":
                            outdata = CrmLibProc.CheckBK(out msg, obj.iGKID);
                            break;
                        case "GetSQDXX":
                            outdata = CrmLibProc.GetSQDXX(out msg, obj.iJLBH, obj.iBJ_CZK);
                            break;
                        case "SavePhoto":
                            outdata = CrmLibProc.SaveBase64Pic(obj.sData, obj.sDir, obj.sFileName);
                            break;
                        case "GetKCKSL":
                            outdata = CrmLibProc.GetKCKSL(out msg, obj.iHYKTYPE, obj.iBJ_CZK);
                            break;
                        case "GetKCKKD2":
                            outdata = CrmLibProc.GetKCKKD2(out msg, obj.sBGDDDM, obj.sCZKHM_BEGIN, obj.sCZKHM_END, obj.iHYKTYPE, obj.iSTATUS, obj.sDBConnName);
                            break;
                        case "GetSydhdy1":
                            outdata = CrmLibProc.GetSydhdy1(out msg, obj.iID);
                            break;
                    }
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    msg = e.InnerException.Message;
                else
                    msg = e.Message;
                throw new MyDbException(msg, query.SqlText);
            }
            finally
            {
                query.Close();
                conn.Close();
            }
            return outdata;
        }

        public static string CyUrlEncode(string str)
        {
            //尼玛空格给转成+，还得转回来
            return System.Web.HttpUtility.UrlEncode(str).Replace("+", "%20");
        }
        public static int ConvertDateTimeInt(DateTime time)
        {

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;

        }

        public static void BFIncDJZT(string token, int jlbh, int msgid)
        {
            if (GetWebConfig("UsePlatform") == "true" && token != "")
            {
                DJZTInfo obj = new DJZTInfo();
                obj.JLBH = jlbh.ToString();
                obj.MSGID = msgid.ToString();
                obj.DISPLAY_JLBH = jlbh.ToString().PadLeft(9, '0');
                LibTeskUtil.IncDJZT(token, obj);
            }
        }
        public static void BFDelDJZT(string token, int jlbh, int msgid)
        {
            if (GetWebConfig("UsePlatform") == "true" && token != "")
            {
                LibTeskUtil.DelDJZT(token, msgid.ToString(), jlbh.ToString());
            }
        }
    }
}
