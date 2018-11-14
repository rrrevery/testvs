using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace BF.CrmProc.GTPT
{
    public class GTPT_WXUser_Proc : BASECRMClass
    {
        public string dDJSJ = string.Empty;
        //public int iWX_NO = 0;
        public string sWX_NO = "";
        public int iHYID = 0;
        public string sHY_NAME = string.Empty;
        public string sOPENID;
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public int iSEX = 0;
        public int iGROUPID = 0;
        public string sGROUP_NAME = string.Empty;
        public string sHEADIMGURL = string.Empty;
        public string sNICKNAME = string.Empty;
        public string sHYKNO = string.Empty;
        public new string[] asFieldNames ={
                                                        "dDJSJ;U.DJSJ",
                                                        "iHYKTYPE;H.HYKTYPE",
                                                        "iSEX;U.SEX",
                                                        "sNICKNAME;U.NICKNAME",
                                                        "sWX_NO;U.WX_NO",
                                                                    
                                      };

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_USER where WX_NO='" + sWX_NO + "'");
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            //if (sWX_NO != "")
            //{
            //    query.SQL.Text = "update HYK_HYGRP set STATUS=" + 2 + " where GRPID=" + iJLBH;
            //    query.ExecSQL();
            //    DeleteDataQuery(out msg, query,serverTime);
            //}
            int rows = 0;
            sWX_NO = SeqGenerator.GetSeq("WX_USER").ToString().PadLeft(6, '0');
            if (sOPENID != "")
            {
                string sql = "update WX_USER set WX_NO=:WX_NO,GROUPID=:GROUPID,STATUS=:STATUS,DJSJ=:DJSJ,SEX=:SEX,HEADIMGURL=:HEADIMGURL,NICKNAME=:NICKNAME ";
                sql += " where OPENID=:OPENID";
                query.SQL.Text = sql;
                query.ParamByName("WX_NO").AsString = sWX_NO;
                query.ParamByName("OPENID").AsString = sOPENID;
                query.ParamByName("STATUS").AsInteger = 1;
                query.ParamByName("GROUPID").AsInteger = iGROUPID;
                query.ParamByName("DJSJ").AsDateTime = FormatUtils.ParseDatetimeString(dDJSJ);
                //query.ParamByName("KKSJ").AsDateTime = FormatUtils.ParseDatetimeString(dDJSJ);
                //query.ParamByName("QXSJ").AsDateTime = FormatUtils.ParseDatetimeString(dDJSJ);
                query.ParamByName("HEADIMGURL").AsString = sHEADIMGURL;
                query.ParamByName("SEX").AsInteger = iSEX;
                if (DbSystemName == CyDbSystem.OracleDbSystemName)
                {
                    query.ParamByName("NICKNAME").AsString = sNICKNAME;
                }
                else if (DbSystemName == CyDbSystem.SybaseDbSystemName)
                {
                    query.ParamByName("NICKNAME").AsChineseString = sNICKNAME;
                }
                rows = query.ExecSQL();
            }
            if (rows > 0)
            {
                return;
            }
            query.SQL.Text = "insert into WX_USER(WX_NO,OPENID,GROUPID,STATUS,DJSJ,SEX,HEADIMGURL,NICKNAME)";//,KKSJ,QXSJ
            query.SQL.Add(" values(:WX_NO,:OPENID,:GROUPID,:STATUS,:DJSJ,:SEX,:HEADIMGURL,:NICKNAME)");//,:KKSJ,:QXSJ
            query.ParamByName("WX_NO").AsString = sWX_NO;
            query.ParamByName("OPENID").AsString = sOPENID;
            query.ParamByName("STATUS").AsInteger = 1;
            query.ParamByName("GROUPID").AsInteger = iGROUPID;
            query.ParamByName("DJSJ").AsDateTime = FormatUtils.ParseDatetimeString(dDJSJ);
            //query.ParamByName("KKSJ").AsDateTime = FormatUtils.ParseDatetimeString(dDJSJ);
            //query.ParamByName("QXSJ").AsDateTime = FormatUtils.ParseDatetimeString(dDJSJ);
            query.ParamByName("HEADIMGURL").AsString = sHEADIMGURL;
            query.ParamByName("SEX").AsInteger = iSEX;
            if (DbSystemName == "ORACLE")
            {
                query.ParamByName("NICKNAME").AsString = sNICKNAME;
            }
            else if (DbSystemName == "SYBASE")
            {
                query.ParamByName("NICKNAME").AsChineseString = sNICKNAME;
            }
            query.ExecSQL();
        }


    }
}
