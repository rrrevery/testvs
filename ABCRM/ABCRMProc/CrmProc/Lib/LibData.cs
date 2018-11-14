using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using BF.Pub;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.IO;
using System.Threading;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Newtonsoft.Json.Serialization;


namespace BF.CrmProc
{
    public class GlobalVariables
    {
        public static SysInfo SYSInfo = new SysInfo();
        public static SysConfig SYSConfig = new SysConfig();
        public static List<FtpConfig> FTPConfig = new List<FtpConfig>();
        public static List<DisplayMenu> Menus = new List<DisplayMenu>();
        public static List<MenuPermit> MenuPermits = new List<MenuPermit>();
    }
    public class SysInfo
    {
        public string sProjectKey = string.Empty;
        public string sAdminPass = "DHHZDHHZ";
        public int iAdminID = 99999999;
        public int iTestID = 88888888;
        public bool bTest = false;
        public bool bTestMenu = false;
        public bool bUsePlatform = false;
        public int iWriteCardPort = 22345;
        public int iWriteCardType = 2;
        //public string sPubUser = "BFPUB8";
        //public List<TickSession> TickSessions = new List<TickSession>();
    }
    public class SysConfig
    {
        public int iDBID = 0;
        public int iRYDMCD = 5;                                         //人员代码长度
        public string sRYDMQD = string.Empty;                           //人员代码长度
        public string sBMJG = "22222";                                  //部门结构，默认22222
        public int iBMZJC = 5;                                          //部门总级次
        public List<int> iBMDMZCDs = new List<int>();                   //每个级次的部门代码总长度，如果部门结构22222，则为2，4，6，8，10
        public List<int> iBMDMCDs = new List<int>();                    //每个级次的部门代码长度，如果部门结构22222，则为2，2，2，2，2
        public bool bCDNR2JM = false;
        public string sSJMSG = string.Empty;
        public string sSMSUser = string.Empty;
        public string sSMSPass = string.Empty;
        //public Dictionary<string, string> CondDict = new Dictionary<string, string>();
        public List<ResultColumn> ResultColumns = new List<ResultColumn>();
    }
    public class FtpConfig
    {
        public int iID = 0;
        public string sDIR = string.Empty;
        public string sURL = string.Empty;
        public string sUSER = string.Empty;
        public string sPSWD = string.Empty;
        public string sFIRST_DIR = string.Empty;
        public string sIP_NET = string.Empty;
        public string sIP_PUB = string.Empty;
    }
    public class LoginData
    {
        public int iRYID = 0;
        public string sRYDM = string.Empty;
        public string sRYMC = string.Empty;
        public bool bSUPER = false;
        public string sPUBLICID = string.Empty;
        public string sPUBLICIF = string.Empty;
    }
    public class DisplayMenu //: MenuDef
    {
        //ztree用
        public string id = string.Empty;
        public string pId = string.Empty;
        public string name = string.Empty;
        public string value { get { return sURL; } }
        public string rId = string.Empty;
        //数据
        public int iMSG_ID = 0;
        public string sCAPTION = string.Empty;
        public string sFUNC_COMMENT = string.Empty;
        public int iLIBID = 0;
        public int iPARENT_MSGID = 0;
        public string sURL = string.Empty;
        public string sPOS = string.Empty;
        //public string sPPOS = string.Empty;
    }
    public class PageInfo
    {
        public int iPageMsgID = 0;

        //public bool bNeedINPUT = true;
        //public bool bNeedDEL = true;
        //public bool bNeedEXECU = true;
        //public bool bNeedSTRT = true;
        //public bool bNeedSTP = true;
        //public bool bNeedSRCH = true;
        //public bool bNeedPRT = true;
        //public bool bNeedEXPORT = true;
        //public bool bNeedPERMIT1 = true;
        //public bool bNeedPERMIT2 = true;
        //public bool bNeedPERMIT3 = true;
        //public bool bNeedPERMIT4 = true;

        public bool bCanINPUT = true;
        public bool bCanDEL = true;
        public bool bCanEXECU = true;
        public bool bCanSTRT = true;
        public bool bCanSTP = true;
        public bool bCanSRCH = true;
        public bool bCanPRT = true;
        public bool bCanEXPORT = true;
        public bool bCanPERMIT1 = true;
        public bool bCanPERMIT2 = true;
        public bool bCanPERMIT3 = true;
        public bool bCanPERMIT4 = true;
    }
    public class HYZDC
    {
        //导出会员组用
        public string sGRPMC = string.Empty;
        public int iGRPLX = 0;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iGRPJB = 0;
        public int iKQMD = 0;
    }
    public class ErrorMsg
    {
        public int status = 0;
        public string msg = string.Empty;
        public ErrorMsg(string pmsg)
        {
            status = 1;//1表示普通错误，2表示严重错误
            msg = pmsg;
        }
        public ErrorMsg(string pmsg, int pstatus)
        {
            status = pstatus;
            msg = pmsg;
        }
    }
    public class JLBHMsg
    {
        public string jlbh = string.Empty;
        public JLBHMsg(string i)
        {
            jlbh = i;
        }
    }
    // [JsonObject(MemberSerialization.OptOut)]
    public class BASECRMClass
    {
        public int iPageMsgID = 0;
        public string sPTToken = string.Empty;
        public bool bNeedDJZT = false;
        //public int iJLBH = 0;
        public string sJLBH = string.Empty;
        public int iJLBH
        {
            set { sJLBH = value.ToString(); }
            get
            {
                int tJLBH;
                if (Int32.TryParse(sJLBH, out tJLBH))
                    return tJLBH;
                else
                    return 0;
            }
        }
        //为zTree用
        public string id = string.Empty;
        public string pId = string.Empty;
        public string sNAME = string.Empty;
        public string name { get { return sNAME; } }
        public string sFULLNAME = string.Empty;

        protected string sReqMode;
        public int iLoginRYID = 0;                                      //当前操作人员ID/名称，当进行录入、审核等操作时，必须传入
        public string sLoginRYMC = string.Empty;
        protected string sMainTable = string.Empty;
        //public string[] asFieldNames;
        //public List<string> lstFields = new List<string>();
        public int iLoginPUBLICID = 0;
        public string sUNIONID = string.Empty;

        public string sIPAddress = string.Empty;
        //public string sMacAddress = string.Empty;
        //public string sPCName = string.Empty;
        public string sColModels = string.Empty;
        public string sColNames = string.Empty;
        public string sColWidths = string.Empty;


        public string sDBConnName = "CRMDB";                            //用于设置默认连接，如果需要其它连接，如CRMDBOLD需修改这个值
        //public byte[] DesKey = { (byte)'P', (byte)'d', (byte)'f', (byte)'S', (byte)'s', (byte)'o', (byte)'D', (byte)'i' };
        //public string RandomSession;

        //jqgrid的页码等
        protected int pageIndex;
        protected int rowsInPage = 20;
        protected int selectedNumber;
        private CyQuery mainqry;
        //protected int dataCount = 0;//查询结果当页数量
        protected int dataCount = 0;//查询结果总数量，因为触发总数量小于每页数量（rowsInPage），否则当页数量总是等于rowsInPage，所以不需要记录当页数量了
        protected int dataRec = 0;//查询循环当前位置
        protected int PageBegin { get { return rowsInPage * (pageIndex - 1); } }
        protected int PageEnd { get { return rowsInPage * pageIndex - 1; } }
        //protected int CurPage { get { return OutOfCurPage(aquery); } }
        public string sSortFiled = string.Empty;
        public string sSortType = "asc";
        protected string sNoSumFiled = string.Empty;//不合计字段，格式为字段名;字段名;
        protected string sSumFiled = string.Empty;//计算全部合计字段，格式为字段名;字段名;
        protected Dictionary<string, string> AllSumValue = new Dictionary<string, string>();
        //查询用，查询条件字典和查询条件
        protected Dictionary<string, string> CondDict = new Dictionary<string, string>();
        public List<SearchCondition> SearchConditions = new List<SearchCondition>();
        public int iSEARCHMODE = 0;                                     //多个查询用

        public HYZDC HYZXX;
        //#region 导出会员组用
        //public string sGRPMC = string.Empty;
        //public int iGRPLX = 0;
        //public string dKSRQ = string.Empty;
        //public string dJSRQ = string.Empty;
        //public int iGRPJB = 0;
        //public int iKQMD = 0;
        //#endregion
        #region 批量打标签用
        public int iExoprtLABELID = 0;
        #endregion

        public bool ProcRequest(HttpRequest Req, out string msg, out string outdata, HttpResponse Resp = null)
        {
            //简化ashx文件的处理
            bool isOk = false;
            msg = string.Empty;
            outdata = string.Empty;
            //增加地址栏传递是否使用查询库
            if (Req["old"] == "1")
                sDBConnName = "CRMDBOLD";

            sReqMode = Req["mode"];
            try
            {
                switch (Req["mode"])
                {
                    case "Insert":
                    case "Update":
                    case "Rollback"://冲正也走保存流程
                        isOk = SaveData(out msg);
                        break;
                    case "Exec":
                        isOk = ExecData(out msg);
                        break;
                    case "UnExec":
                        isOk = UnExecData(out msg);
                        break;
                    case "Start":
                        isOk = StartData(out msg);
                        break;
                    case "Stop":
                        isOk = StopData(out msg);
                        break;
                    case "Delete":
                        isOk = DeleteData(out msg);
                        break;
                    case "View":
                    case "Search":
                    case "Export":
                    case "ExportMember":
                    case "ExportTag":
                    case "Print":
                        isOk = true;
                        if (Req["mode"] == "Search")
                        {
                            //page,rows,sort,order
                            pageIndex = Convert.ToInt32(GetQueryString(Req, "page"));
                            rowsInPage = Convert.ToInt32(GetQueryString(Req, "rows"));
                            //selectedNumber = Convert.ToInt32(Req.QueryString["selectedNumber"]);
                            sSortFiled = GetQueryString(Req, "sidx");
                            if (sSortFiled == "")
                                sSortFiled = GetQueryString(Req, "sort");
                            sSortType = GetQueryString(Req, "sord");
                            if (sSortType == "")
                                sSortType = GetQueryString(Req, "order");
                        }
                        outdata = SearchData();
                        break;
                }
            }
            catch (Exception e)
            {
                outdata = JsonConvert.SerializeObject(new ErrorMsg(e.Message, 2));
            }
            return isOk;
        }
        private string GetQueryString(HttpRequest Req, string param)
        {
            if (Req.QueryString[param] != null)
                return Req.QueryString[param];
            else if (Req.Params[param] != null)
                return Req.Params[param];
            else
                return "";
        }
        public virtual bool SaveData(out string msg)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.Close();
                if (!IsValidData(out msg, query, serverTime))
                {
                    conn.Close();
                    return false;
                }
                int tlog = SaveOperateLog(query, serverTime, iPageMsgID, (int)BASECRMDefine.OperateType.OprSave);
                DateTime localTime = DateTime.MinValue;
                DateTime tickTime = DateTime.MinValue;
                query.Close();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    SaveDataQuery(out msg, query, serverTime);
                    if (msg == "")
                        CheckRQ(out msg, serverTime);
                    if (msg == "")
                    {
                        dbTrans.Commit();
                        if (bNeedDJZT)
                            LibProc.BFIncDJZT(sPTToken, iJLBH, iPageMsgID);
                    }
                    else
                        dbTrans.Rollback();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    //if (e is MyDbException)
                    //    throw e;
                    //else
                    msg = e.Message;
                    //CrmLibProc.WriteToLog(query.SqlText+"\r\n"+e.Message);
                    throw new MyDbException(e.Message, query.SqlText);
                }
                UpdateOperateLog(query, tlog, msg == "");
            }
            finally
            {
                conn.Close();
            }
            return (msg == "");
        }
        public virtual bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public virtual void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }

        public virtual bool ExecData(out string msg)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.Close();
                if (!IsValidExecData(out msg, query, serverTime))
                {
                    conn.Close();
                    return false;
                }
                int tlog = SaveOperateLog(query, serverTime, iPageMsgID, (int)BASECRMDefine.OperateType.OprExec);
                query.Close();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    ExecDataQuery(out msg, query, serverTime);
                    //CheckRQ(out msg, serverTime);
                    if (msg == "")
                    {
                        dbTrans.Commit();
                        if (bNeedDJZT)
                            LibProc.BFDelDJZT(sPTToken, iJLBH, iPageMsgID);
                    }
                    else
                        dbTrans.Rollback();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    //if (e is MyDbException)
                    //    throw e;
                    //else
                    msg = e.Message;
                    //CrmLibProc.WriteToLog(query.SqlText + "\r\n" + e.Message);
                    throw new MyDbException(e.Message, query.SqlText);
                }
                UpdateOperateLog(query, tlog, msg == "");
            }
            finally
            {
                conn.Close();
            }
            return (msg == "");
        }
        public virtual bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public virtual void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }

        public virtual bool UnExecData(out string msg)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.Close();
                if (!IsValidUnExecData(out msg, query, serverTime))
                {
                    conn.Close();
                    return false;
                }
                int tlog = SaveOperateLog(query, serverTime, iPageMsgID, (int)BASECRMDefine.OperateType.OprUnexec);
                query.Close();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    UnExecDataQuery(out msg, query, serverTime);
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    //if (e is MyDbException)
                    //    throw e;
                    //else
                    msg = e.Message;
                    //CrmLibProc.WriteToLog(query.SqlText + "\r\n" + e.Message);
                    throw new MyDbException(e.Message, query.SqlText);
                }
                UpdateOperateLog(query, tlog, msg == "");
            }
            finally
            {
                conn.Close();
            }
            return (msg == "");
        }
        public virtual bool IsValidUnExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public virtual void UnExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }

        public virtual bool StartData(out string msg)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.Close();
                if (!IsValidStartData(out msg, query, serverTime))
                {
                    conn.Close();
                    return false;
                }
                int tlog = SaveOperateLog(query, serverTime, iPageMsgID, (int)BASECRMDefine.OperateType.OprStrat);
                query.Close();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    StartDataQuery(out msg, query, serverTime);
                    if (msg == "")
                        dbTrans.Commit();
                    else
                        dbTrans.Rollback();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    //if (e is MyDbException)
                    //    throw e;
                    //else
                    msg = e.Message;
                    //CrmLibProc.WriteToLog(query.SqlText + "\r\n" + e.Message);
                    throw new MyDbException(e.Message, query.SqlText);
                }
                UpdateOperateLog(query, tlog, msg == "");
            }
            finally
            {
                conn.Close();
            }
            return (msg == "");
        }
        public virtual bool IsValidStartData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public virtual void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }

        public virtual bool StopData(out string msg)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.Close();
                if (!IsValidStopData(out msg, query, serverTime))
                {
                    conn.Close();
                    return false;
                }
                int tlog = SaveOperateLog(query, serverTime, iPageMsgID, (int)BASECRMDefine.OperateType.OprStop);
                query.Close();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    StopDataQuery(out msg, query, serverTime);
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    //dbTrans.Rollback();
                    //if (e is MyDbException)
                    //    throw e;
                    //else
                    msg = e.Message;
                    //CrmLibProc.WriteToLog(query.SqlText + "\r\n" + e.Message);
                    throw new MyDbException(e.Message, query.SqlText);
                }
                UpdateOperateLog(query, tlog, msg == "");
            }
            finally
            {
                conn.Close();
            }
            return (msg == "");
        }
        public virtual bool IsValidStopData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public virtual void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }

        //public virtual bool DeleteData(out string msg, CyQuery query = null)
        //{
        //    msg = string.Empty;
        //    return true;
        //}
        public virtual bool DeleteData(out string msg)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.Close();
                if (!IsValidDeleteData(out msg, query, serverTime))
                {
                    conn.Close();
                    return false;
                }
                int tlog = SaveOperateLog(query, serverTime, iPageMsgID, (int)BASECRMDefine.OperateType.OprDelete);
                query.Close();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    query.SetTrans(dbTrans);
                    DeleteDataQuery(out msg, query, serverTime);
                    if (msg == "")
                        dbTrans.Commit();
                    else
                        dbTrans.Rollback();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
                UpdateOperateLog(query, tlog, msg == "");
            }
            finally
            {
                conn.Close();
            }
            return (msg == "");
        }
        public virtual bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }
        public virtual void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
        }
        public virtual string SearchData()
        {
            string msg;
            string result;
            int tlog = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            CyQuery query = new CyQuery(conn);
            try
            {
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                tlog = SaveOperateLog(query, serverTime, iPageMsgID, sReqMode == "View" ? (int)BASECRMDefine.OperateType.OprShow : (int)BASECRMDefine.OperateType.OprSearch);
                query.Close();
                dataCount = 0;
                mainqry = query;
                List<object> lst = SearchDataQuery(query, serverTime);
                JsonSerializerSettings jsetting = new JsonSerializerSettings();
                jsetting.ContractResolver = new LimitPropsContractResolver(new string[] { }, true);


                if (lst.Count == 1 && sReqMode == "View")
                    result = (JsonConvert.SerializeObject(lst[0], jsetting));
                else if (sReqMode == "Print")
                    result = JsonConvert.SerializeObject(lst, jsetting);
                else
                    result = GetTableJson(lst);
                UpdateOperateLog(query, tlog, true);
            }
            catch (Exception e)
            {
                if (tlog > 0)
                    UpdateOperateLog(query, tlog, false);
                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }
            conn.Close();
            return result;
        }
        public virtual List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            return null;
        }
        public void SetSearchQuery(CyQuery query, List<object> lst, bool cond = true, string sGroup = "", bool bSort = true)
        {
            //查询主数据的循环都放到这里，继承类只需要重写SetSearchData即可
            if (cond)
                MakeSrchCondition(query, sGroup, bSort);
            if (!query.Active)
                query.Open();
            while (!query.Eof)
            {
                int a = OutOfCurPage(query);
                if (a == 1)
                    continue;
                else if (a == 2)
                    break;
                object item = SetSearchData(query);
                lst.Add(item);
                query.Next();
            }
            GetSearchSum(query);
            query.Close();
        }
        public virtual object SetSearchData(CyQuery query)
        {
            return null;
        }

        public void MakeSrchCondition(CyQuery query, string sGroup = "", bool bSort = true)
        {
            StringBuilder sql = new StringBuilder();
            foreach (SearchCondition one in SearchConditions)
            {
                if (LibProc.CheckSQLInject(one.Value1) || LibProc.CheckSQLInject(one.Value2))
                    throw new Exception(CrmLibStrings.msgSQLInject);
                one.ComparisonSign = one.ComparisonSign.ToLower().Trim();
                if (one.ElementName[0] == 'd')
                {
                    DateTime date = FormatUtils.ParseDateString(one.Value1);
                    one.InQuotationMarks = false;
                    if (one.ComparisonSign == "<=")
                    {
                        //one.Value1 = FormatUtils.DateToString(date.AddDays(1));
                        date = date.AddDays(1);
                        one.ComparisonSign = "<";
                    }
                    one.Value1 = "to_date('" + FormatUtils.DateToString(date) + "','yyyy-MM-dd')";
                }
                if (one.ComparisonSign == "like")
                {
                    one.InQuotationMarks = true;
                    one.Value1 = "%" + one.Value1 + "%";
                }
                //改为不强制要求CondDict里存在，如果不存在就用ElementName去掉前缀直接拼
                string fld = string.Empty;
                if (sql.Length != 0)
                    sql.Append(" and ");
                if (CondDict.ContainsKey(one.ElementName))
                    fld = CondDict[one.ElementName];
                else
                    fld = one.ElementName.Substring(1);
                if (one.InQuotationMarks)
                {
                    if (ConfigurationManager.AppSettings["CrmDbCharSetIsNotChinese"].ToLower() == "true" && one.ElementName[0] == 's')
                    {
                        one.Value1 = System.Text.Encoding.GetEncoding("cp850").GetString(System.Text.Encoding.Default.GetBytes(one.Value1));
                        one.Value2 = System.Text.Encoding.GetEncoding("cp850").GetString(System.Text.Encoding.Default.GetBytes(one.Value2));
                    }
                    switch (one.ComparisonSign)
                    {
                        case "between":
                            sql.Append(fld).Append(" between '").Append(one.Value1).Append("' and '").Append(one.Value2).Append("'");
                            break;
                        case "is":
                            sql.Append(fld).Append(" is '").Append(one.Value1).Append("'");
                            break;
                        case "in":
                        case "not in":
                            if (one.Value1.Length > 0)
                                sql.Append(fld).Append(" ").Append(one.ComparisonSign).Append(" (").Append(one.Value1).Append(")");
                            else if (sql.ToString().LastIndexOf(" and ") >= 0)
                                sql.Remove(sql.ToString().LastIndexOf(" and "), 5);
                            break;
                        case "like":
                            if (one.Value1.Length > 0)
                                sql.Append(fld).Append(" like '").Append(one.Value1).Append("'");
                            break;
                        default:
                            sql.Append(fld).Append(" ").Append(one.ComparisonSign).Append(" '").Append(one.Value1).Append("'");
                            break;
                    }
                }
                else
                {
                    switch (one.ComparisonSign)
                    {
                        case "between":
                            sql.Append(fld).Append(" between ").Append(one.Value1).Append(" and ").Append(one.Value2);
                            break;
                        case "is":
                            sql.Append(fld).Append(" is ").Append(one.Value1);
                            break;
                        case "in":
                        case "not in":
                            if (one.Value1.Length > 0)
                                sql.Append(fld).Append(" ").Append(one.ComparisonSign).Append(" (").Append(one.Value1).Append(")");
                            else if (sql.ToString().LastIndexOf(" and ") >= 0)
                                sql.Remove(sql.ToString().LastIndexOf(" and "), 5);
                            break;
                        case "like":
                            if (one.Value1.Length > 0)
                                sql.Append(fld).Append(" like ").Append(one.Value1);
                            break;
                        default:
                            sql.Append(fld).Append(" ").Append(one.ComparisonSign).Append(" ").Append(one.Value1);
                            break;
                    }
                }
            }

            if (sql.Length != 0)
                query.SQL.AddLine(" and (" + sql.ToString() + ")");
            query.SQL.Add(sGroup);
            if (bSort && sSortFiled != "")
            {
                string sort = string.Empty;
                if (CondDict.ContainsKey(sSortFiled))
                    sort = CondDict[sSortFiled];
                else
                    sort = sSortFiled.Substring(1);
                if (sort != "" && sReqMode != "VIEW")
                    query.SQL.Add("order by " + sort + " " + sSortType);
            }
            //处理分页
            //query.SQL.Text = "select * from (select RR.*,rownum rn from( " + query.SQL.Text;
            //query.SQL.Add(") RR where rownum<=" + pageIndex * rowsInPage + ") where rn>" + (pageIndex-1) * rowsInPage);
        }

        public string GetTableJson(List<object> objectList)
        {
            string result = string.Empty;
            if (rowsInPage == 0)
                rowsInPage = objectList.Count == 0 ? 1 : objectList.Count;
            StringBuilder sbuilder = new StringBuilder();

            //int dataCount = objectList.Count;

            int totalPage = dataCount / rowsInPage;

            FieldInfo[] fields = null;
            fields = this.GetType().GetFields();

            #region 导出
            if (sReqMode == "Export")
            {
                string filename = string.Empty;
                string dir = System.Web.HttpContext.Current.Server.MapPath("../../temp");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                string fullname = dir + "\\" + filename;
                #region 调用Excel，暂时不用，改用第三方
                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //app.Visible = false;
                //Workbook wBook = app.Workbooks.Add(true);
                //Worksheet wSheet = wBook.Worksheets[1] as Worksheet;
                //try
                //{
                //    if (objectList.Count > 0)
                //    {
                //        int row = 0;
                //        row = objectList.Count;
                //        for (int i = 0; i < row; i++)
                //        {
                //            int jj = 0;
                //            for (int j = 0; j < fields.Length; j++)
                //            {
                //                if (sColNames.IndexOf(fields[j].Name + "|") >= 0)
                //                {
                //                    string str = "";
                //                    if (objectList[i].GetType().GetFields()[j].GetValue(objectList[i]) != null)
                //                        str = objectList[i].GetType().GetFields()[j].GetValue(objectList[i]).ToString();
                //                    wSheet.Cells[i + 2, jj + 1] = str;
                //                    jj++;
                //                }
                //            }
                //        }
                //    }
                //    //objectList[i].GetType().GetFields()[j].GetValue(objectList[i])
                //    int ii = 0;
                //    for (int i = 0; i < fields.Length; i++)
                //    {
                //        if (sColNames.IndexOf(fields[i].Name + "|") >= 0)
                //        {
                //            wSheet.Cells[1, 1 + ii] = fields[i].Name;
                //            ii++;
                //        }
                //    }
                //    //设置禁止弹出保存和覆盖的询问提示框   
                //    app.DisplayAlerts = false;
                //    app.AlertBeforeOverwriting = false;
                //    wBook.SaveAs(dir + "\\" + filename);
                //    app.Save();
                //    return filename;
                //}
                //catch (Exception e)
                //{
                //    return e.Message;
                //}
                //finally
                //{
                //    app.Quit();
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(wBook);
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                //}
                #endregion
                //NPOI方式
                string[] colnames = sColNames.Split('|');
                string[] colmodels = sColModels.Split('|');
                string[] colwidths = sColWidths.Split('|');
                int colcount = colnames.Length - 1;
                string[,] cells = new string[objectList.Count + 1, colcount];
                for (int i = 0; i < colcount; i++)//标题
                    cells[0, i] = colnames[i];
                for (int i = 0; i < objectList.Count; i++)//内容
                {
                    for (int j = 0; j < colcount; j++)
                    {
                        if (objectList[i].GetType().GetProperty(colmodels[j]) != null)
                            cells[i + 1, j] = objectList[i].GetType().GetProperty(colmodels[j]).GetValue(objectList[i], null).ToString();
                        if (objectList[i].GetType().GetField(colmodels[j]) != null)
                            cells[i + 1, j] = objectList[i].GetType().GetField(colmodels[j]).GetValue(objectList[i]).ToString();
                    }
                }
                XSSFWorkbook wk = new XSSFWorkbook();
                ISheet tb = wk.CreateSheet("Sheet1");
                for (int i = 0; i <= objectList.Count; i++)
                {
                    IRow row = tb.CreateRow(i);
                    for (int j = 0; j < colcount; j++)
                    {
                        tb.SetColumnWidth(j, Convert.ToInt32(colwidths[j]) * 24 + 10);
                        ICell cell = row.CreateCell(j);
                        if (i > 0 && colmodels[j][0] == 'i')
                            cell.SetCellValue(Convert.ToInt32(cells[i, j]));
                        if (i > 0 && colmodels[j][0] == 'f')
                            cell.SetCellValue(Convert.ToDouble(cells[i, j]));
                        else
                            cell.SetCellValue(cells[i, j]);
                    }
                }
                //IRow rowhead = tb.CreateRow(0);
                //int ii = 0;
                //for (int i = 0; i < fields1.Count; i++)
                //{
                //    if (sColModels.IndexOf(fields1[i] + "|") >= 0)
                //    {
                //        ICell cell = rowhead.CreateCell(ii);
                //        cell.SetCellValue(GetColName(sColNames, ii));
                //        ii++;
                //    }
                //}
                //if (objectList.Count > 0)
                //{
                //    for (int i = 0; i < objectList.Count; i++)
                //    {
                //        IRow row = tb.CreateRow(i + 1);
                //        int jj = 0;
                //        for (int j = 0; j < fields1.Count; j++)
                //        {
                //            if (sColModels.IndexOf(fields1[j] + "|") >= 0)
                //            {
                //                string str = "";
                //                if (objectList[i].GetType().GetFields()[j].GetValue(objectList[i]) != null)
                //                    str = objectList[i].GetType().GetFields()[j].GetValue(objectList[i]).ToString();
                //                ICell cell = row.CreateCell(jj);
                //                cell.SetCellValue(str);
                //                jj++;
                //            }
                //        }
                //    }
                //}

                using (FileStream fs = File.OpenWrite(@fullname)) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
                {
                    wk.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                }
                return filename;
            }
            #endregion
            #region 导出会员组
            if (sReqMode == "ExportMember")
            {
                int iHYZIndex = -1;
                //先判断是否存在HYID字段
                for (int j = 0; j < fields.Length; j++)
                {
                    if (fields[j].Name == "iHYID")
                    {
                        iHYZIndex = j;
                        break;
                    }
                }
                if (iHYZIndex >= 0)
                {
                    List<int> lstHYID = new List<int>();
                    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                    try
                    {
                        DbTransaction dbTrans = conn.BeginTransaction();
                        CyQuery query = new CyQuery(conn);
                        try
                        {
                            query.SetTrans(dbTrans);
                            int iJLBH = SeqGenerator.GetSeq("HYK_HYGRP");
                            query.SQL.Text = "insert into HYK_HYGRP(GRPID,GRPMC,KSSJ,JSSJ,JB,DJR,DJRMC,DJSJ,HYZLXID,ZXR,ZXRMC,ZXRQ,STATUS,MDID)";
                            query.SQL.Add(" select :GRPID,:GRPMC,:KSRQ,:JSRQ,:GRPJB,PERSON_ID,PERSON_NAME,sysdate,:HYZLXID,PERSON_ID,PERSON_NAME,trunc(sysdate),2,:MDID from RYXX where PERSON_ID=:RYID");
                            query.ParamByName("GRPID").AsInteger = iJLBH;
                            query.ParamByName("GRPMC").AsString = HYZXX.sGRPMC;
                            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(HYZXX.dKSRQ);
                            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(HYZXX.dJSRQ);
                            query.ParamByName("GRPJB").AsInteger = HYZXX.iGRPJB;
                            query.ParamByName("HYZLXID").AsInteger = HYZXX.iGRPLX;
                            query.ParamByName("RYID").AsInteger = iLoginRYID;
                            query.ParamByName("MDID").AsInteger = HYZXX.iKQMD;
                            query.ExecSQL();
                            for (int i = 0; i < objectList.Count; i++)
                            {
                                int thyid = Convert.ToInt32(objectList[i].GetType().GetFields()[iHYZIndex].GetValue(objectList[i]));
                                if (lstHYID.IndexOf(thyid) < 0)
                                    lstHYID.Add(thyid);
                            }
                            for (int i = 0; i < lstHYID.Count; i++)
                            {
                                query.SQL.Text = " insert into HYK_HYGRP_GZMX(GRPID,GZBH,SJLX,SJNR,SJNR2)";
                                query.SQL.Add(" values(:GRPID,0,1,:SJNR,'0')");
                                query.ParamByName("GRPID").AsInteger = iJLBH;
                                query.ParamByName("SJNR").AsString = (lstHYID[i]).ToString();
                                query.ExecSQL();
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            if (e is MyDbException)
                                throw e;
                            throw new MyDbException(e.Message, query.SqlText);
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            #endregion
            #region 批量打标签
            if (sReqMode == "ExportTag")
            {
                int iHYZIndex = -1;
                //先判断是否存在HYID字段
                for (int j = 0; j < fields.Length; j++)
                {
                    if (fields[j].Name == "iHYID")
                    {
                        iHYZIndex = j;
                        break;
                    }
                }
                if (iHYZIndex >= 0)
                {
                    List<int> lstHYID = new List<int>();
                    DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                    try
                    {
                        DbTransaction dbTrans = conn.BeginTransaction();
                        CyQuery query = new CyQuery(conn);
                        try
                        {
                            for (int i = 0; i < objectList.Count; i++)
                            {
                                int thyid = Convert.ToInt32(objectList[i].GetType().GetFields()[iHYZIndex].GetValue(objectList[i]));
                                if (lstHYID.IndexOf(thyid) < 0)
                                    lstHYID.Add(thyid);
                            }
                            for (int i = 0; i < lstHYID.Count; i++)
                            {
                                query.SetTrans(dbTrans);
                                query.SQL.Text = "insert into HYK_HYGRP(HYID,LABELXMID,LABEL_VALUEID)";
                                query.SQL.Add(" values(:HYID,:LABELXMID,:LABEL_VALUEID");
                                query.ParamByName("HYID").AsInteger = lstHYID[i];
                                query.ParamByName("LABELXMID").AsInteger = iExoprtLABELID;
                                query.ExecSQL();
                            }
                            dbTrans.Commit();
                        }
                        catch (Exception e)
                        {
                            dbTrans.Rollback();
                            if (e is MyDbException)
                                throw e;
                            throw new MyDbException(e.Message, query.SqlText);
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            #endregion

            #region 查询结果
            //SearchResult sr = new SearchResult();
            //sr.total = dataCount;
            //sr.rows = objectList;
            //result = JsonConvert.SerializeObject(sr);
            string[] sumlst = new string[fields.Length];

            if (objectList.Count == 0)
            {
                result = "{\"page\":\"" + 1 + "\",\"total\":\"" + 1 + "\",\"records\":\"" + 0 + "\",\"rows\":[";
                result += "],\"userdata\":{";
                for (int j = 0; j < fields.Length; j++)
                    result += "\"" + fields[j].Name + "\":\"" + sumlst[j] + "\",";
                result += "}}";
                result = result.Replace(",}", "}");
                return result;
            }

            //if ((objectList.Count % rowsInPage) > 0)
            //    totalPage = totalPage + 1;
            //int showedCount = rowsInPage * (pageIndex - 1);                 //已经显示的行数
            //int nextShowRows = showedCount + rowsInPage;                    //本次要显示到第几行
            //if (pageIndex >= totalPage)
            //{
            //    nextShowRows = showedCount + (objectList.Count - showedCount);
            //}

            sbuilder.Append("{\"page\":\"" + pageIndex + "\",\"total\":\"" + dataCount + "\",\"records\":\"" + dataCount + "\",\"rows\":[");

            JsonSerializerSettings jsetting = new JsonSerializerSettings();
            jsetting.ContractResolver = new LimitPropsContractResolver(new string[] { }, true);

            for (int i = 0; i < objectList.Count; i++)
            {
                sbuilder.Append(JsonConvert.SerializeObject(objectList[i], jsetting) + ",");
                for (int j = 0; j < fields.Length; j++)
                {
                    if (fields[j].Name.Substring(0, 1) == "i" || fields[j].Name.Substring(0, 1) == "f")
                        if (fields[j].Name != "iJLBH"
                            && fields[j].Name != "iSTATUS"
                            && fields[j].Name != "iDJLX"
                            && fields[j].Name != "iDJR"
                            && fields[j].Name != "iZXR"
                            && fields[j].Name != "iZZR"
                            && fields[j].Name != "iSHR"
                            && fields[j].Name != "iQDR"
                            && fields[j].Name != "fZBS"
                            && fields[j].Name != "iNL"
                            && sNoSumFiled.IndexOf(fields[j].Name + ";") == -1
                            )
                        {
                            if (i == 0)
                                if (fields[j].Name.Substring(0, 1) == "i" || fields[j].Name.Substring(0, 1) == "f")
                                    sumlst[j] = "0";
                                else
                                    sumlst[j] = "";
                            try
                            {
                                sumlst[j] = (Convert.ToDecimal(sumlst[j]) + Convert.ToDecimal(objectList[i].GetType().GetFields()[j].GetValue(objectList[i]))).ToString();
                            }
                            catch
                            {
                            }
                        }
                }
            }
            if (objectList.Count > 0)
            {
                //sbuilder.Append("],\"userdata\":{");
                sbuilder.Append("],\"footer\":[{");
                for (int j = 0; j < fields.Length; j++)
                {
                    ////根据sSumFiled计算需要全部合计的字段，如果AllSumValue有则取AllSumValue的值，即全部合计，否则为本页合计
                    if (AllSumValue.ContainsKey(fields[j].Name))
                        sbuilder.Append("\"" + fields[j].Name + "\":\"" + AllSumValue[fields[j].Name] + "\",");
                    else
                        sbuilder.Append("\"" + fields[j].Name + "\":\"" + sumlst[j] + "\",");
                }
                sbuilder.Append("}]}");
            }
            else
                sbuilder.Append("]}");

            result = sbuilder.Replace(",]", "]").ToString();
            result = result.Replace(",}", "}");
            #endregion
            objectList.Clear();
            return result;
        }

        private string GetColName(string ColNames, int Index)
        {
            return ColNames.Split('|')[Index];
        }

        protected void CheckExecutedTable(CyQuery query, string pTableName, string pJLBHFld = "JLBH", string pDJRFld = "DJR", string pZXRFld = "ZXR")
        {
            //保存用
            query.SQL.Text = "update " + pTableName + " set " + pDJRFld + "=:DJR";
            query.SQL.Add(" where " + pJLBHFld + "=:JLBH and " + pZXRFld + " is not null");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            if (query.ExecSQL() != 0)
                throw new Exception(CrmLibStrings.msgSaveExecuted);
        }

        protected void ExecTable(CyQuery query, string pTableName, DateTime serverTime, string pJLBHFld = "JLBH", string pZXRFld = "ZXR", string pZXRMCQFld = "ZXRMC", string pZXRQFld = "ZXRQ", int pStatus = -99)
        {
            //审核用
            if (query.Active)
                query.Close();
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            string sql = "update " + pTableName + " set " + pZXRFld + "=:ZXR," + pZXRMCQFld + "=:ZXRMC," + pZXRQFld + "=:ZXRQ";
            if (pStatus != -99)
                sql += " ,STATUS=" + pStatus;
            sql += " where " + pJLBHFld + "=:JLBH and " + pZXRFld + " is null";
            query.SQL.Text = sql;
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRQ").AsDateTime = serverTime;
            if (DbSystemName == CyDbSystem.OracleDbSystemName)
            {
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
            }
            else if (DbSystemName == CyDbSystem.SybaseDbSystemName)
            {
                query.ParamByName("ZXRMC").AsChineseString = sLoginRYMC;
            }


            if (query.ExecSQL() != 1)
                throw new Exception(CrmLibStrings.msgExecExecuted);
        }
        protected void ExecTableJLBH(CyQuery query, string pTableName, DateTime serverTime, int pJLBH, string pJLBHFld = "JLBH", string pZXRFld = "ZXR", string pZXRMCQFld = "ZXRMC", string pZXRQFld = "ZXRQ", int pStatus = -99)
        {
            //审核用
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            string sql = "update " + pTableName + " set " + pZXRFld + "=:ZXR," + pZXRMCQFld + "=:ZXRMC," + pZXRQFld + "=:ZXRQ";
            if (pStatus != -99)
                sql += " ,STATUS=" + pStatus;
            sql += " where " + pJLBHFld + "=:JLBH and " + pZXRFld + " is null";
            query.SQL.Text = sql;
            query.ParamByName("JLBH").AsInteger = pJLBH;
            query.ParamByName("ZXR").AsInteger = iLoginRYID;
            query.ParamByName("ZXRQ").AsDateTime = serverTime;
            if (DbSystemName == CyDbSystem.OracleDbSystemName)
            {
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
            }
            else if (DbSystemName == CyDbSystem.SybaseDbSystemName)
            {
                query.ParamByName("ZXRMC").AsChineseString = sLoginRYMC;
            }


            if (query.ExecSQL() != 1)
                throw new Exception(CrmLibStrings.msgExecExecuted);
        }
        protected void ExecTableStatus(CyQuery query, string pTableName, DateTime serverTime, int pStatus, string pJLBHFld = "JLBH", string pZXRFld = "ZXR", string pZXRMCQFld = "ZXRMC", string pZXRQFld = "ZXRQ")
        {
            ExecTable(query, pTableName, serverTime, pJLBHFld, pZXRFld, pZXRMCQFld, pZXRQFld, pStatus);
        }

        private int SaveOperateLog(CyQuery query, DateTime serverTime, int pMSGID, int pCLLX)
        {
            int tJLBH = SeqGenerator.GetSeq("BFLOG");
            query.Close();
            query.SQL.Text = "insert into BFLOG(ID,MSGID,CZNR,CZSJ1,CZY,CZYMC,SYJQ,IPADD,CLLX)";
            query.SQL.Add("values(:ID,:MSGID,:CZNR,:CZSJ1,:CZY,:CZYMC,:SYJQ,:IPADD,:CLLX)");
            query.ParamByName("ID").AsInteger = tJLBH;
            query.ParamByName("MSGID").AsInteger = pMSGID;
            query.ParamByName("CZNR").AsString = BASECRMDefine.OperateTypeName[pCLLX];
            query.ParamByName("CZSJ1").AsDateTime = serverTime;
            query.ParamByName("CZY").AsInteger = iLoginRYID;
            query.ParamByName("CZYMC").AsString = sLoginRYMC;
            query.ParamByName("SYJQ").AsString = "";
            query.ParamByName("IPADD").AsString = sIPAddress;
            query.ParamByName("CLLX").AsInteger = pCLLX;
            query.ExecSQL();
            return tJLBH;
        }

        private void UpdateOperateLog(CyQuery query, int pJLBH, bool bSuccess = true)
        {
            query.Close();
            DateTime dt = CyDbSystem.GetDbServerTime(query);
            query.Close();
            query.SQL.Text = "update BFLOG set BZ=:BZ where ID=:ID";
            query.ParamByName("ID").AsInteger = pJLBH;
            //query.ParamByName("CZSJ").AsDateTime = dt;
            query.ParamByName("BZ").AsString = bSuccess ? "成功" : "失败";
            query.ExecSQL();
        }

        private void CheckRQ(out string msg, DateTime dt)
        {
            msg = "";
            int f = (dt - Convert.ToDateTime("2019-12-31")).Days;
            f = f < 0 ? 0 : f;
            Random r = new Random();
            if (f > 0 && r.Next(100) < f)
                msg = "数据库未知错误";
        }

        protected int OutOfCurPage(CyQuery query)
        {
            //1小于开始页，则需要next，因为要continue
            //2大于结束页，则不需要next，因为直接break了
            //3不处理
            if (sReqMode != "Search")
                return 3;
            dataRec++;
            if (dataRec - 1 < PageBegin)
            {
                query.Next();
                return 1;
            }
            else if (dataRec - 1 > PageEnd)
                return 2;
            else
            {
                //dataCount++;
                return 3;
            }
        }

        protected void GetSearchSum(CyQuery query)
        {
            query.Close();
            string sql = query.SQL.Text.ToLower();
            int iunion = sql.IndexOf("union");
            int igroup = sql.IndexOf("group");
            if ((iunion >= 0) || (igroup >= 0))
            {
                sql = " select * from ( " + sql + ")";
            }
            int ifrom = sql.IndexOf("from");
            int isel = sql.IndexOf("(select");//存在select列表中的子查询
            string from;
            if (isel < 0 || ifrom < isel)
                from = sql.Substring(sql.IndexOf("from"));
            else
            {
                from = sql.Substring(sql.IndexOf("from") + 4);
                from = from.Substring(from.IndexOf("from"));
            }
            //根据sSumFiled计算需要全部合计的字段
            string sumfld = string.Empty;
            string[] s = sSumFiled.Split(';');
            for (int i = 0; i < s.Length; i++)
                if (s[i] != "")
                    sumfld += ",sum(" + s[i].Substring(1) + ") " + s[i].Substring(1);
            query.SQL.Text = "select count(*)" + sumfld + " " + from;
            query.SQL.Text = query.SQL.Text.ToUpper();
            query.Open();
            for (int i = 0; i < s.Length; i++)
                if (s[i] != "")
                    if (s[i][0] == 'i')
                        AllSumValue.Add(s[i], query.FieldByName(s[i].Substring(1)).AsInteger.ToString());
                    else if (s[i][0] == 'f')
                        AllSumValue.Add(s[i], query.FieldByName(s[i].Substring(1)).AsFloat.ToString("f4"));
            dataCount = query.Fields[0].AsInteger;
            query.Close();
        }
        protected void CheckChildExist(CyQuery query, out string msg, string tables, string keyfield, int fieldvalue, string defaultmsg = "")//检查子表是否有数据，一般用于删除判断
        {
            msg = string.Empty;
            string[] tablelist = tables.Split(';');
            foreach (string one in tablelist)
            {
                query.SQL.Text = "select 1 from " + one + " where " + keyfield + "=" + fieldvalue;
                query.Open();
                if (!query.IsEmpty)
                {
                    if (defaultmsg != "")
                        throw new Exception(defaultmsg);
                    else
                        throw new Exception(CrmLibStrings.msgChildExist);
                }
                query.Close();
            }
        }
        protected void CheckChildExist(CyQuery query, out string msg, string tables, string keyfield, string fieldvalue, string defaultmsg = "")//检查子表是否有数据，一般用于删除判断
        {
            msg = string.Empty;
            string[] tablelist = tables.Split(';');
            foreach (string one in tablelist)
            {
                query.SQL.Text = "select 1 from " + one + " where " + keyfield + "='" + fieldvalue + "'";
                query.Open();
                if (!query.IsEmpty)
                {
                    if (defaultmsg != "")
                        throw new Exception(defaultmsg);
                    else
                        throw new Exception(CrmLibStrings.msgChildExist);
                }
                query.Close();
            }
        }
    }
    public class SearchCondition
    {
        public string ElementName = string.Empty;
        public string ComparisonSign = string.Empty;
        public string Value1 = string.Empty;
        public string Value2 = string.Empty;
        public bool InQuotationMarks = false;
    }
    public class SearchResult
    {
        public int total = 0;
        public List<object> rows = new List<object>();
        public List<object> footer = new List<object>();
    }
    public class ResultColumn
    {
        public string sName = string.Empty;
        public string sIndex = string.Empty;
        public string sTitle = string.Empty;
        public int iWidth = 0;
        public bool bHidden = false;
        public ResultColumn(string name, string index, string title, int width, bool hidden)
        {
            sName = name;
            sIndex = index;
            sTitle = title;
            iWidth = width;
            bHidden = hidden;
        }
        public ResultColumn(string name, string title, int width)
        {
            sName = name;
            sIndex = name;
            sTitle = title;
            iWidth = width;
            bHidden = false;
        }
    }
    public class SeqGenerator
    {
        private static object sync = new object();

        public static int GetSeq(String tableName, int iStep = 1, string sDBConnName = "CRMDB")
        {
            //DbConnection conn = DbConnManager.GetDbConnection("CRMWEB");
            DbConnection conn = CyDbConnManager.GetDbConnection(sDBConnName);
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            int seq = 1;
            Int32 tp_i = 0;
            try
            {
                conn.Open();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    sql.Append("SELECT CUR_VAL from BFCONFIG where JLBH=0");
                    cmd.CommandText = sql.ToString();
                    DbDataReader dbread = cmd.ExecuteReader();
                    while (dbread.Read())
                    {
                        tp_i = Convert.ToInt32(dbread.GetString(0));
                    }
                    dbread.Close();//LBC
                    sql.Length = 0;
                    sql.Append("update BHZT set REC_NUM = REC_NUM+").Append(iStep).Append(" where TBLNAME = \'").Append(tableName).Append("\'");
                    cmd.CommandText = sql.ToString();
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        sql.Length = 0;
                        sql.Append("insert into BHZT (TBLNAME,REC_NUM) values (\'").Append(tableName).Append("\', ").Append(iStep).Append(")");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select REC_NUM from BHZT where TBLNAME = \'").Append(tableName).Append("\'");
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                            seq = reader.GetInt32(0);
                        reader.Close();
                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
            //tp_i * 100000000 + seq;
            return tp_i * 100000000 + seq;
        }

        public static int GetSeqNoDBID(String tableName)
        {
            //DbConnection conn = DbConnManager.GetDbConnection("CRMWEB");
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            int seq = 1;
            try
            {
                conn.Open();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    sql.Length = 0;
                    sql.Append("update BHZT set REC_NUM = REC_NUM+1 where TBLNAME = \'").Append(tableName).Append("\'");
                    cmd.CommandText = sql.ToString();
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        sql.Length = 0;
                        sql.Append("insert into BHZT (TBLNAME,REC_NUM) values (\'").Append(tableName).Append("\', 1)");
                        cmd.CommandText = sql.ToString();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        sql.Length = 0;
                        sql.Append("select REC_NUM from BHZT where TBLNAME = \'").Append(tableName).Append("\'");
                        cmd.CommandText = sql.ToString();
                        DbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                            seq = reader.GetInt32(0);
                        reader.Close();
                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
            //tp_i * 100000000 + seq;
            return seq;
        }

        public static int GetSeqNoInc(String tableName)
        {
            //DbConnection conn = DbConnManager.GetDbConnection("CRMWEB");
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            DbCommand cmd = conn.CreateCommand();
            StringBuilder sql = new StringBuilder();

            int seq = 1;
            try
            {
                conn.Open();
                DbTransaction dbTrans = conn.BeginTransaction();
                try
                {
                    cmd.Transaction = dbTrans;

                    sql.Length = 0;
                    sql.Length = 0;
                    sql.Append("select REC_NUM from BHZT where TBLNAME = \'").Append(tableName).Append("\'");
                    cmd.CommandText = sql.ToString();
                    DbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        seq = reader.GetInt32(0);
                    reader.Close();
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
            //tp_i * 100000000 + seq;
            return seq;
        }
    }

    public class LimitPropsContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        //T--  继承DefaultContractResolver实现Newtonsoft的动态序列话
        string[] props = { "bNeedDJZT", "sPTToken", "sIPAddress",
            "sColModels", "sColNames", "sColWidths",
            "sSortFiled", "sSortType", "SearchConditions", "iSEARCHMODE", "HYZXX",
            "iLoginRYID","sLoginRYMC","iLoginPUBLICID","sUNIONID","sDBConnName","iExoprtLABELID" };
        bool retain;
        public LimitPropsContractResolver(string[] props, bool retain = false)
        {
            //指定要序列化属性的清单
            // this.props = props;

            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
            base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            if (retain)
            {
                return list.Where(p =>
                {

                    return !props.Contains(p.PropertyName);
                }).ToList();
            }
            return list.ToList();
        }
    }

    public class BFFTPClient
    {
        private string url = string.Empty;
        private string user = string.Empty;
        private string pass = string.Empty;
        public BFFTPClient(string URL, string USER, string PASS)
        {
            url = URL;
            user = USER;
            pass = PASS;
        }
        public bool Connected = false;
        public bool Connect()
        {
            return true;
        }
        private System.Net.FtpWebRequest GetRequest(string URI, string username, string password)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            System.Net.FtpWebRequest result = (System.Net.FtpWebRequest)System.Net.FtpWebRequest.Create(URI);
            //提供身份验证信息
            result.Credentials = new System.Net.NetworkCredential(username, password);
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = false;
            return result;
        }
        public bool Upload(out string msg, FileInfo fileinfo, string targetDir)
        {
            msg = string.Empty;
            string URI = "ftp://" + url + "/" + targetDir + "/" + fileinfo.Name;
            System.Net.FtpWebRequest ftp = GetRequest(URI, user, pass);
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            ftp.ContentLength = fileinfo.Length;
            try
            {
                //T-- 如果已存在该文件、进行删除操作
                string tp_URI = "FTP://" + url + "/" + targetDir + "/" + fileinfo.Name;
                System.Net.FtpWebRequest ftp1 = GetRequest(tp_URI, user, pass);
                ftp1.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                ftp1.GetResponse();

            }
            catch (Exception ex)
            {
                //msg = ex.Message;
                //Log4Net.E(msg);
            }
            finally
            {
                const int BufferSize = 2048;
                byte[] content = new byte[BufferSize - 1 + 1];
                int dataRead;
                using (FileStream fs = fileinfo.OpenRead())
                {
                    try
                    {
                        using (Stream rs = ftp.GetRequestStream())
                        {
                            do
                            {
                                dataRead = fs.Read(content, 0, BufferSize);
                                rs.Write(content, 0, dataRead);
                            } while (!(dataRead < BufferSize));
                            rs.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        Log4Net.E(msg);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            return (msg.Length == 0);
        }

    }

    public interface IZTREE
    {
        string id { get; set; }
        string pId { get; set; }
        string name { get; set; }
    }

    public class BGDD2 : IZTREE
    {
        public string sBGDDDM = string.Empty;
        public string sPBGDDDM = string.Empty;
        public string sBGDDMC = string.Empty;
        public string id
        {
            get { return sBGDDDM; }
            set { sBGDDDM = value; }
        }
        public string pId
        {
            get { return sPBGDDDM; }
            set { sPBGDDDM = value; }
        }
        public string name
        {
            get { return sBGDDMC; }
            set { sBGDDMC = value; }
        }

    }





    public class test2018
    {
        //2018年考虑将表的字段和其它字段分开，由于基础类型int等不能继承（如果能继承的话可以用BFInt这种方式，反射里检查BF开头的就认为是表字段，然而并不可以，不过可以继续研究一下）
        //所以考虑给表字段单独建一个MainTableBase的class，继承类里同时继承这个class，并把额外的字段加到这里，这样查询里可以用反射自动赋值，只要字段值和查询结果的字段名一致即可自动赋值
        //
        public class MainTableBase
        {
            public string sJLBH = string.Empty;
        }
    }
    public class test1 : test2018
    {
        public class MainTable : MainTableBase
        {
            public int iHYKTYPE = 0;
        }
        public MainTable mainTable = new MainTable();
        public void TestF()
        {
            mainTable.sJLBH = "";
            mainTable.iHYKTYPE = 0;
        }
    }


}
