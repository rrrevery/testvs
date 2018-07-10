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
    public class GTPT_WXMessage_Proc : DJLR_ZXQDZZ_CLass
    {

        public int iGROUPID;
        public string sGROUP_NAME = string.Empty;
        public int iXXLX = 0;// 1 文本消息  2 图文消息  3 语音消息  4 视频消息
        public string sXXNR = string.Empty;
        public string dFSSJ = string.Empty;
        public new int iSTATUS = 1;//  
        public int iHFLX = -1;// 0 代表群发消息  1 代表自动回复 2 代表客服接口消息
        public int iXYLX = -1;//如自动回复时    1代表默认回复 2关注回复   3关键词回复
        public string sKEYS = string.Empty;//关键词(含有多个)
        public string sKFACCOUNT = string.Empty;//发送客服消息时，指定的客服账号
        //public new string[] asFieldNames = {
        //                                "iJLBH;W.JLBH", 
        //                               "iGROUPID;W.GROUPID", 
        //                               "iXXLX;W.XXLX",
        //                               "iXYLX;W.XYLX",
        //                               "sGROUP_NAME;B.GROUP_NAME", 
        //                               "sZY;W.ZY", 
        //                               "sDJRMC;W.DJRMC", 
        //                               "iDJR;W.DJR", 
        //                               "dDJSJ;W.DJSJ", 
        //                               "sZXRMC;W.ZXRMC", 
        //                               "iZXR;W.ZXR", 
        //                               "dZXRQ;W.ZXRQ", 
        //                               "iHFLX;W.HFLX"
        //                               };
        public List<WX_MESSAGEITEM> itemTable = new List<WX_MESSAGEITEM>();
        public class WX_MESSAGEITEM
        {
            public int iJLBH = 0;
            public int iFSXH = 0;
            public string sTITLE = string.Empty;
            public string sDESCRIPTION = string.Empty;
            public string sCONTENT = string.Empty;
            public string sCONTENT2 = string.Empty;
            public string sCONTENTURL = string.Empty;
            public int iBJ_CONVER = 0;
            public string sFILENAME = string.Empty;
            public string sMEDIA_ID = string.Empty;
            public string sURL = string.Empty;
            public string sTHUMB_MEDIA_ID = string.Empty;
            public string sPICURL = string.Empty;
            public string sREPLACESRC = string.Empty;

        }
        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_MESSAGE", serverTime, "JLBH");
        }
        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_MESSAGEITEM;WX_MESSAGE", "JLBH", iJLBH);
                    }
        //查询
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();


            CondDict.Add("iGROUPID", "W.GROUPID");
         
                    string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
                    CondDict.Add("iJLBH", "W.JLBH");
                    CondDict.Add("iGROUPID", "W.GROUPID");
                    CondDict.Add("iXXLX", "W.XXLX");
                    CondDict.Add("iXYLX", "W.XYLX");
                    CondDict.Add("sGROUP_NAME", "B.GROUP_NAME");
                    CondDict.Add("sZY", "W.ZY");
                    CondDict.Add("sDJRMC", "W.DJRMC");
                    CondDict.Add("iDJR", "W.DJR");
                    CondDict.Add("dDJSJ", "W.DJSJ");
                    CondDict.Add("sZXRMC", "W.ZXRMC");
                    CondDict.Add("iZXR", "W.ZXR");
                    CondDict.Add("dZXRQ", "W.ZXRQ");
                    CondDict.Add("iHFLX", "W.HFLX");

                    query.SQL.Text = "select W.*,B.GROUP_NAME from WX_MESSAGE W,WX_GROUP B ";
                    if (DbSystemName == "ORACLE")
                    {
                        query.SQL.AddLine(" where W.GROUPID=B.GROUPID(+)");
                    }
                    else if (DbSystemName == "SYBASE")
                    {
                        query.SQL.AddLine(" where W.GROUPID *= B.GROUPID");
                    }

                    //if (iHFLX != -1)
                    //{
                    //    query.SQL.AddLine("  and W.HFLX=" + iHFLX);
                    //}
                    //if (iXYLX != -1)
                    //{
                    //    query.SQL.AddLine("  and W.XYLX=" + iXYLX);
                    //}
                    MakeSrchCondition(query);
                    query.Open();
                    while (!query.Eof)
                    {
                        GTPT_WXMessage_Proc obj = new GTPT_WXMessage_Proc();
                        lst.Add(obj);
                        obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                        this.iJLBH = obj.iJLBH;
                        obj.iGROUPID = query.FieldByName("GROUPID").AsInteger;
                        obj.iXXLX = query.FieldByName("XXLX").AsInteger;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.iDJR = query.FieldByName("DJR").AsInteger;
                        obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                        obj.iZXR = query.FieldByName("ZXR").AsInteger;
                        obj.iQDR = query.FieldByName("QDR").AsInteger;
                        obj.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
                        obj.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
                        obj.iZZR = query.FieldByName("ZZR").AsInteger;
                        obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        obj.iXYLX = query.FieldByName("XYLX").AsInteger;
                        obj.iHFLX = query.FieldByName("HFLX").AsInteger;

                        obj.sKFACCOUNT = query.FieldByName("KFACCOUNT").AsString;

                        if (DbSystemName == "ORACLE")
                        {
                            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                            obj.sGROUP_NAME = query.FieldByName("GROUP_NAME").AsString;
                            obj.sXXNR = query.FieldByName("XXNR").AsString;//一般用来保存文本消息的内容
                            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
                            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
                            obj.sKEYS = query.FieldByName("KEYS").AsString;
                            obj.sZY = query.FieldByName("ZY").AsString;
                        }
                        else if (DbSystemName == "SYBASE")
                        {

                            obj.sDJRMC = query.FieldByName("DJRMC").GetChineseString(100);
                            obj.sGROUP_NAME = query.FieldByName("GROUP_NAME").GetChineseString(100);
                            obj.sXXNR = query.FieldByName("XXNR").GetChineseString(2000);//一般用来保存文本消息的内容
                            obj.sZXRMC = query.FieldByName("ZXRMC").GetChineseString(100);
                            obj.sQDRMC = query.FieldByName("QDRMC").GetChineseString(100);
                            obj.sZZRMC = query.FieldByName("ZZRMC").GetChineseString(100);
                            obj.sKEYS = query.FieldByName("KEYS").GetChineseString(100); ;
                            obj.sZY = query.FieldByName("ZY").GetChineseString(200);
                        }
                        query.Next();
                    }
                    query.Close();

                    if (lst.Count == 1)
                    {
                        query.SQL.Text = "select * from  WX_MESSAGEITEM I  ";
                        query.SQL.AddLine(" where I.JLBH=" + iJLBH);
                        query.SQL.AddLine(" order by FSXH asc ");
                        query.Open();
                        while (!query.Eof)
                        {
                            WX_MESSAGEITEM obj = new WX_MESSAGEITEM();
                            obj.iJLBH = iJLBH;
                            obj.iFSXH = query.FieldByName("FSXH").AsInteger;
                            obj.iBJ_CONVER = query.FieldByName("BJ_CONVER").AsInteger;
                            obj.sCONTENT2 = query.FieldByName("CONTENT2").AsString;//.Replace("\"", "'")
                            obj.sCONTENT = query.FieldByName("CONTENT").AsString;//.Replace("\"", "'")
                            obj.iFSXH = query.FieldByName("FSXH").AsInteger;
                            obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
                            obj.iFSXH = query.FieldByName("FSXH").AsInteger;
                            obj.sMEDIA_ID = query.FieldByName("MEDIA_ID").AsString;
                            obj.sURL = query.FieldByName("URL").AsString;
                            obj.sTHUMB_MEDIA_ID = query.FieldByName("THUMB_MEDIA_ID").AsString;
                            obj.sPICURL = query.FieldByName("PICURL").AsString;
                            obj.sREPLACESRC = query.FieldByName("REPLACESRC").AsString;
                            if (DbSystemName == "ORACLE")
                            {
                                obj.sTITLE = query.FieldByName("TITLE").AsString;
                                obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
                                obj.sFILENAME = query.FieldByName("FILENAME").AsString;
                                obj.sCONTENTURL = query.FieldByName("CONTENTURL").AsString;
                            }
                            else if (DbSystemName == "SYBASE")
                            {
                                obj.sTITLE = query.FieldByName("TITLE").GetChineseString(100);
                                obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").GetChineseString(100);
                                obj.sFILENAME = query.FieldByName("FILENAME").GetChineseString(100);
                                obj.sCONTENTURL = query.FieldByName("CONTENTURL").GetChineseString(100);

                            }
                            ((GTPT_WXMessage_Proc)lst[0]).itemTable.Add(obj);
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
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_MESSAGE");
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);



            query.SQL.Text = "insert into WX_MESSAGE(JLBH,HFLX,XYLX,GROUPID,XXLX,XXNR,STATUS,KEYS,KFACCOUNT,DJSJ,DJRMC,DJR)";//,FSSJ
            query.SQL.Add(" values(:JLBH,:HFLX,:XYLX,:GROUPID,:XXLX,:XXNR,:STATUS,:KEYS,:KFACCOUNT,:DJSJ,:DJRMC,:DJR)");//,:FSSJ
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HFLX").AsInteger = iHFLX;
            query.ParamByName("XYLX").AsInteger = iXYLX;
            query.ParamByName("GROUPID").AsInteger = iGROUPID;
            query.ParamByName("XXLX").AsInteger = iXXLX;
            //query.ParamByName("FSSJ").AsDateTime = FormatUtils.ParseDatetimeString(dFSSJ); //启动时间
            query.ParamByName("STATUS").AsInteger = 0;
            query.ParamByName("KFACCOUNT").AsString = sKFACCOUNT;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            if (DbSystemName == "ORACLE")
            {
                query.ParamByName("XXNR").AsString = sXXNR;
                query.ParamByName("KEYS").AsString = sKEYS;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
            }
            else if (DbSystemName == "SYBASE")
            {

                query.ParamByName("XXNR").AsChineseString = sXXNR;
                query.ParamByName("KEYS").AsChineseString = sKEYS;
                query.ParamByName("DJRMC").AsChineseString = sLoginRYMC;
            }
            query.ExecSQL();
            foreach (WX_MESSAGEITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_MESSAGEITEM(JLBH,FSXH,BJ_CONVER,TITLE,DESCRIPTION,CONTENTURL,CONTENT,CONTENT2,FILENAME,MEDIA_ID,URL,THUMB_MEDIA_ID,PICURL,REPLACESRC)";
                query.SQL.Add(" values(:JLBH,:FSXH,:BJ_CONVER,:TITLE,:DESCRIPTION,:CONTENTURL,:CONTENT,:CONTENT2,:FILENAME,:MEDIA_ID,:URL,:THUMB_MEDIA_ID,:PICURL,:REPLACESRC)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("FSXH").AsInteger = one.iFSXH;
                query.ParamByName("BJ_CONVER").AsInteger = one.iBJ_CONVER;
                query.ParamByName("CONTENTURL").AsString = one.sCONTENTURL;
                query.ParamByName("CONTENT").AsString = one.sCONTENT;//.Replace("'", "\"")
                query.ParamByName("CONTENT2").AsString = one.sCONTENT2;//.Replace("'", "\"")
                query.ParamByName("FILENAME").AsString = one.sFILENAME;
                query.ParamByName("MEDIA_ID").AsString = one.sMEDIA_ID;
                query.ParamByName("URL").AsString = one.sURL;
                query.ParamByName("THUMB_MEDIA_ID").AsString = one.sTHUMB_MEDIA_ID;
                query.ParamByName("PICURL").AsString = one.sPICURL;
                query.ParamByName("REPLACESRC").AsString = one.sREPLACESRC;
                if (DbSystemName == "ORACLE")
                {
                    query.ParamByName("TITLE").AsString = one.sTITLE;
                    query.ParamByName("DESCRIPTION").AsString = one.sDESCRIPTION;
                }
                else if (DbSystemName == "SYBASE")
                {
                    query.ParamByName("TITLE").AsChineseString = one.sTITLE;
                    query.ParamByName("DESCRIPTION").AsChineseString = one.sDESCRIPTION;

                }
                query.ExecSQL();
            }
        }

        //启动
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_MESSAGE", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
        }
        //终止
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_MESSAGE", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", -1);
        }

    }
}
