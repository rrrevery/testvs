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
    public class GTPT_WXMenu_Proc : BASECRMClass
    {
        public string sDM = string.Empty;
        public int iTYPE = 0;//与类型相对应
        public string sURL = string.Empty;
        public string sKEY = string.Empty;
        //public new string[] asFieldNames = {
        //                                "iJLBH;W.JLBH", 
        //                               "iGROUPID;W.GROUPID", 
        //                               "sGROUP_NAME;B.GROUP_NAME", 
        //                               "sZY;W.ZY", 
        //                               "sDJRMC;W.DJRMC", 
        //                               "iDJR;W.DJR", 
        //                               "dDJSJ;W.DJSJ", 
        //                               "sZXRMC;W.ZXRMC", 
        //                               "iZXR;W.ZXR", 
        //                               "dZXRQ;W.ZXRQ", 
        //                               };
        public List<WX_MENUITEM> itemTable = new List<WX_MENUITEM>();
        public class WX_MENUITEM
        {
            public string sDM = string.Empty;
            public int iFSXH = 0;
            public string sTITLE = string.Empty;
            public string sDESCRIPTION = string.Empty;
            public string sCONTENT = string.Empty;
            public string sCONTENT2 = string.Empty;
            public string sCONTENTURL = string.Empty;
            public int iBJ_CONVER = 0;
            public string sFILENAME = string.Empty;
            public string sMEDIA_ID = string.Empty;

        }
        ////审核
        //public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        //{
        //    //msg = string.Empty;
        //    //ExecTable(query, "WX_MENU", serverTime, "JLBH");
        //}
        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete  from  WX_MENU where DM='" + sDM + "'; delete  from WX_MENUITEM where DM='" + sDM + "'");
                    }
        //查询
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            string msg = "";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                
                try
                {
                    query.SQL.Text = "select W.*  from WX_MENU W  where W.DM is not null";
                    if (sDM != string.Empty)
                        query.SQL.AddLine("  and W.DM='" + sDM + "'");
                    //MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        GTPT_WXMenu_Proc obj = new GTPT_WXMenu_Proc();
                        lst.Add(obj);

                        obj.sDM = query.FieldByName("DM").AsString;
                        obj.iTYPE = query.FieldByName("TYPE").AsInteger;
                        obj.sNAME = query.FieldByName("NAME").AsString;
                        obj.sURL = query.FieldByName("URL").AsString;
                        obj.sKEY = query.FieldByName("KEY").AsString;
                        query.Next();
                    }
                    query.Close();

                    
                    {
                        query.SQL.Text = "SELECT * FROM  WX_MENUITEM I  ";
                        query.SQL.AddLine(" where I.DM='" + sDM + "'");
                        query.SQL.AddLine(" order by FSXH asc ");
                        query.Open();
                        while (!query.Eof)
                        {
                            WX_MENUITEM obj = new WX_MENUITEM();
                            obj.sDM = sDM;
                            obj.iFSXH = query.FieldByName("FSXH").AsInteger;
                            obj.sTITLE = query.FieldByName("TITLE").AsString;
                            obj.iBJ_CONVER = query.FieldByName("BJ_CONVER").AsInteger;
                            obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
                            obj.sCONTENT = query.FieldByName("CONTENT").AsString;
                            obj.sCONTENT2 = query.FieldByName("CONTENT2").AsString;
                            obj.sFILENAME = query.FieldByName("FILENAME").AsString;
                            obj.sCONTENTURL = query.FieldByName("CONTENTURL").AsString;
                            obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
                            ((GTPT_WXMenu_Proc)lst[0]).itemTable.Add(obj);
                            query.Next();

                        }
                        query.Close();

                    }

                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            
                
                return lst;
        }
        //修改  添加
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (sDM != string.Empty)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }

            query.SQL.Text = "insert into WX_MENU(DM,TYPE,NAME,URL,KEY)";
            query.SQL.Add(" values(:DM,:TYPE,:NAME,:URL,:KEY))");
            query.ParamByName("DM").AsString = sDM;
            query.ParamByName("TYPE").AsInteger = iTYPE;
            query.ParamByName("NAME").AsString = sNAME;
            query.ParamByName("URL").AsString = sURL;
            query.ParamByName("KEY").AsString = sKEY;
            query.ExecSQL();
            foreach (WX_MENUITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_MENUITEM(DM,FSXH,BJ_CONVER,TITLE,DESCRIPTION,CONTENTURL,CONTENT,CONTENT2,FILENAME,MEDIA_ID)";
                query.SQL.Add(" values(:DM,:FSXH,:BJ_CONVER,:TITLE,:DESCRIPTION,:CONTENTURL,:CONTENT,:CONTENT2,:FILENAME,:MEDIA_ID)");
                query.ParamByName("DM").AsString = sDM;
                query.ParamByName("FSXH").AsInteger = one.iFSXH;
                query.ParamByName("BJ_CONVER").AsInteger = one.iBJ_CONVER;
                query.ParamByName("TITLE").AsString = one.sTITLE;
                query.ParamByName("DESCRIPTION").AsString = one.sDESCRIPTION;
                query.ParamByName("CONTENTURL").AsString = one.sCONTENTURL;
                query.ParamByName("CONTENT").AsString = one.sCONTENT;
                query.ParamByName("CONTENT2").AsString = one.sCONTENT2;
                query.ParamByName("FILENAME").AsString = one.sFILENAME;
                query.ParamByName("MEDIA_ID").AsString = one.sMEDIA_ID;
                query.ExecSQL();
            }
        }

        ////启动
        //public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        //{
        //    msg = string.Empty;
        //    ExecTable(query, "WX_MENU", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
        //}
        ////终止
        //public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        //{
        //    msg = string.Empty;
        //    ExecTable(query, "WX_MENU", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", -1);
        //}
    }
}
