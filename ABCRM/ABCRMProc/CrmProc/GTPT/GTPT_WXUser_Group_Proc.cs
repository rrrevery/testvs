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

using System.Collections;

namespace BF.CrmProc.GTPT
{
    /// <summary>
    /// 分组记录
    /// </summary>
    public class GTPT_WXUser_Group_Proc : DJLR_ZX_CLass
    {
        public int iGROUPID;
        public string sGROUP_NAME = string.Empty;
        public string sGROUPJL_NAME = string.Empty;
        //public string sZY = string.Empty;
        //public int iSTATUS = 0;// 0 有效 -1 无效
        public new string[] asFieldNames = {
                                       "iJLBH;W.JLBH",
                                       "sGROUPJL_NAME;W.GROUPJL_NAME",
                                       "iGROUPID;W.GROUPID",
                                       "sGROUP_NAME;B.GROUP_NAME",
                                       "sZY;W.ZY",
                                       "sDJRMC;W.DJRMC",
                                       "iDJR;W.DJR",
                                       "dDJSJ;W.DJSJ",
                                       "sZXRMC;W.ZXRMC",
                                       "iZXR;W.ZXR",
                                       "dZXRQ;W.ZXRQ",
                                       };
        public List<GTPT_WXUSER_GROUPITEM> itemTable = new List<GTPT_WXUSER_GROUPITEM>();
        public class GTPT_WXUSER_GROUPITEM
        {
            public int iJLBH = 0;
            public string sOPENID = string.Empty;
            public string sINFO = string.Empty;
            public int iGROUPID = 0;
            public string sGROUP_NAME = string.Empty;
            public string sHYKNO = string.Empty;
            public string sHYKNAME = string.Empty;
            public int iHYKTYPE = 0;
            public int iSEX = 0;

        }
        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //或者重新写一个类专门用来处理 WX_USER的信息
            //query.SQL.Text = "update WX_USERGROUP set ZXR=" + iLoginRYID + ",ZXRMC='" + sLoginRYMC + "',ZXRQ='" + FormatUtils.DatetimeToString(serverTime + "'";
            //query.SQL.Text += " where JLBH=" + iJLBH;
            //query.ExecSQL();

            ExecTable(query, "WX_USERGROUP", serverTime, "JLBH");
            query.SQL.Text = " update WX_USER set GROUPID=:GROUPID where OPENID in(select OPENID from WX_USERGROUPITEM where JLBH=:JLBH)";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GROUPID").AsInteger = iGROUPID;
            query.ExecSQL();
            //与微信服务器进行交互 修改用户的GROUPID
        }
        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            //与微信服务器进行交互  修改用户的GROUPID
            CrmLibProc.DeleteDataTables(query, out msg, "WX_USERGROUPITEM;WX_USERGROUP", "JLBH", iJLBH);
        }
        //查询
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);


            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sGROUPJL_NAME", "W.GROUPJL_NAME");
            CondDict.Add("iGROUPID", "W.GROUP_ID");
            CondDict.Add("sGROUP_NAME", "W.GROUP_NAME");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,B.GROUP_NAME from WX_USERGROUP W,WX_GROUP B ";
            if (DbSystemName == "ORACLE")
            {
                query.SQL.AddLine(" where W.GROUP_ID=B.GROUP_ID(+)");
            }
            else if (DbSystemName == "SYBASE")
            {
                query.SQL.AddLine(" where W.GROUP_ID *= B.GROUP_ID");
            }
            SetSearchQuery(query, lst);                    
            if (lst.Count==1)
            {
                query.SQL.Text = "select I.*,U.SEX,H.HYK_NO,K.HYKTYPE,K.HYKNAME,G.GROUPID,G.GROUP_NAME from WX_USERGROUPITEM I,WX_USER U, HYK_HYXX  H,HYKDEF K,WX_GROUP G";
                if (DbSystemName == "ORACLE")
                {
                    query.SQL.AddLine(" where I.OPENID=H.OPENID(+)  ");
                    query.SQL.AddLine("and H.HYKTYPE=K.HYKTYPE(+)  ");
                    query.SQL.AddLine(" and I.GROUPID_OLD=G.GROUPID(+) ");
                }
                else if (DbSystemName == "SYBASE")
                {
                    query.SQL.AddLine(" where I.OPENID *=H.OPENID  ");
                    query.SQL.AddLine(" and H.HYKTYPE *=K.HYKTYPE ");
                    query.SQL.AddLine(" and I.GROUPID_OLD *=G.GROUPID ");
                }
                query.SQL.AddLine(" and I.OPENID=U.OPENID ");
                query.SQL.AddLine(" and I.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    GTPT_WXUSER_GROUPITEM obj = new GTPT_WXUSER_GROUPITEM();
                    obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                    obj.sOPENID = query.FieldByName("OPENID").AsString;
                    obj.iGROUPID = query.FieldByName("GROUPID").AsInteger;
                    obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
                    obj.iSEX = query.FieldByName("SEX").AsInteger;
                    if (DbSystemName == "ORACLE")
                    {
                        obj.sGROUP_NAME = query.FieldByName("GROUP_NAME").AsString;
                        obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    }
                    else if (DbSystemName == "SYBASE")
                    {
                        obj.sGROUP_NAME = query.FieldByName("GROUP_NAME").GetChineseString(100);
                        obj.sHYKNAME = query.FieldByName("HYKNAME").GetChineseString(100);
                    }
                    ((GTPT_WXUser_Group_Proc)lst[0]).itemTable.Add(obj);
                    query.Next();

                }
                query.Close();

            }




            return lst;
        }
        //修改  添加
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_USERGROUP");
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            query.SQL.Text = "insert into WX_USERGROUP(JLBH,GROUPJL_NAME,GROUPID,DJSJ,DJRMC,DJR,ZY)";
            query.SQL.Add(" values(:JLBH,:GROUPJL_NAME,:GROUPID,:DJSJ,:DJRMC,:DJR,:ZY)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GROUPID").AsInteger = iGROUPID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            if (DbSystemName == "ORACLE")
            {
                query.ParamByName("ZY").AsString = sZY;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("GROUPJL_NAME").AsString = sGROUPJL_NAME;
            }
            else if (DbSystemName == "SYBASE")
            {

                query.ParamByName("ZY").AsChineseString = sZY;
                query.ParamByName("DJRMC").AsChineseString = sLoginRYMC;
                query.ParamByName("GROUPJL_NAME").AsChineseString = sGROUPJL_NAME;
            }
            query.ExecSQL();
            foreach (GTPT_WXUSER_GROUPITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_USERGROUPITEM(JLBH,OPENID,GROUPID_OLD)";
                query.SQL.Add(" values(:JLBH,:OPENID,:GROUPID_OLD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("OPENID").AsString = one.sOPENID;
                query.ParamByName("GROUPID_OLD").AsInteger = one.iGROUPID;
                query.ExecSQL();
            }
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXUser_Group_Proc obj = new GTPT_WXUser_Group_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iGROUPID = query.FieldByName("GROUPID").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
                obj.sGROUPJL_NAME = query.FieldByName("GROUPJL_NAME").AsString;
                obj.sGROUP_NAME = query.FieldByName("GROUP_NAME").AsString;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                obj.sZY = query.FieldByName("ZY").AsString;
            

            return obj;
        }
    }
}
