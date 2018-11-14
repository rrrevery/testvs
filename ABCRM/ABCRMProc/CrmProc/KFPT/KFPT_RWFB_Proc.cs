using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;

namespace BF.CrmProc.KFPT
{
    public class KFPT_RWFB_Proc : DJLR_ZX_CLass
    {
        public string sRWZT = string.Empty;
        public string sRW = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iRWLX = 0;

        public List<RWQFJLItem> rwqfjlItem = new List<RWQFJLItem>();
        public class RWQFJLItem
        {
            public int iJLBH = 0;
            public int iRWDX = 0;
            public int iJLBH_RW = 0;
            public string sPERSON_NAME = string.Empty;
        }

        public List<RWJL_HYItem> rwjl_hyItem = new List<RWJL_HYItem>();
        public class RWJL_HYItem
        {
            public int iXH = 0;
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iKFRYID = 0;
            public int iSEX = 0;
            public string sSEX = string.Empty;
            public string sSJHM = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataBase(query, out msg, "delete from RWJL L where exists(select 1 from RWQFJLITEM where JLBH_RW=L.JLBH and JLBH=" + iJLBH + ")");   //先根据JLBH删除RWJL中的相关数据，再删除其他表中相关记录
            CrmLibProc.DeleteDataTables(query, out msg, "RWQFJL;RWQFJLITEM;RWJL_HY;", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("RWQFJL");
            query.SQL.Text = "insert into RWQFJL( JLBH,RWZT,RW,STATUS,RWLX,KSRQ,JSRQ,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:JLBH,:RWZT,:RW,0,0,:KSRQ,:JSRQ,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("RWZT").AsString = sRWZT;
            query.ParamByName("RW").AsString = sRW;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.StrToDate(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.StrToDate(dJSRQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (RWQFJLItem one in rwqfjlItem)
            {
                if (one.iJLBH_RW == 0)
                    one.iJLBH_RW = SeqGenerator.GetSeq("RWQFJLITEM"); //往RWQFJLITEM中添加记录的同时添加记录到RWJL中
                query.SQL.Text = "begin insert into RWQFJLITEM(JLBH,RWDX,JLBH_RW)";
                query.SQL.Add(" values(:JLBH,:RWDX,:JLBH_RW);");
                query.SQL.Add("insert into RWJL(JLBH,RWDX,RWZT,RW,STATUS,RWLX,DJR,DJRMC,DJSJ,KSRQ,JSRQ)");
                query.SQL.Add("values(:JLBH_RW,:RWDX,:RWZT,:RW,0,0,:DJR,:DJRMC,:DJSJ,:KSRQ,:JSRQ);end;");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("RWDX").AsInteger = one.iRWDX;
                query.ParamByName("JLBH_RW").AsInteger = one.iJLBH_RW;
                query.ParamByName("RWZT").AsString = sRWZT;
                query.ParamByName("RW").AsString = sRW;
                query.ParamByName("KSRQ").AsDateTime = FormatUtils.StrToDate(dKSRQ);
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.StrToDate(dJSRQ);
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("DJSJ").AsDateTime = serverTime;
                query.ExecSQL();
            }
            foreach (RWJL_HYItem item in rwjl_hyItem)
            {
                query.SQL.Text = "insert into RWJL_HY(JLBH,XH,HYID,HYK_NO,HY_NAME,KFRYID)";
                query.SQL.Add(" values(:JLBH,:XH,:HYID,:HYK_NO,:HY_NAME,:KFRYID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = item.iXH;
                query.ParamByName("HYID").AsInteger = item.iHYID;
                query.ParamByName("HYK_NO").AsString = item.sHYK_NO;
                query.ParamByName("HY_NAME").AsString = item.sHY_NAME;
                query.ParamByName("KFRYID").AsInteger = item.iKFRYID;
                query.ExecSQL();
            }

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "JLBH");
            CondDict.Add("sRWZT", "RWZT");
            CondDict.Add("sRW", "RW");
            CondDict.Add("dKSRQ", "KSRQ");
            CondDict.Add("dJSRQ", "JSRQ");
            CondDict.Add("iDJR", "DJR");
            CondDict.Add("dDJSJ", "DJSJ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select * from RWQFJL where 1=1";
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = " select I.*,R.PERSON_NAME from RWQFJLITEM I, RYXX R where I.RWDX=R.PERSON_ID and I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    RWQFJLItem item = new RWQFJLItem();
                    ((KFPT_RWFB_Proc)lst[0]).rwqfjlItem.Add(item);
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item.iRWDX = query.FieldByName("RWDX").AsInteger;
                    item.sPERSON_NAME = query.FieldByName("PERSON_NAME").AsString;
                    item.iJLBH_RW = query.FieldByName("JLBH_RW").AsInteger;

                    query.Next();
                }
                query.Close();

                query.SQL.Text = " select H.*,G.SEX,G.SJHM from RWJL_HY H, HYK_HYXX Y,HYK_GKDA G where H.HYID=Y.HYID and Y.GKID=G.GKID and H.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    RWJL_HYItem item = new RWJL_HYItem();
                    ((KFPT_RWFB_Proc)lst[0]).rwjl_hyItem.Add(item);
                    item.iXH = query.FieldByName("XH").AsInteger;
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item.iSEX = query.FieldByName("SEX").AsInteger;
                    if (item.iSEX == 0)
                    {
                        item.sSEX = "男";
                    }
                    else
                    {
                        item.sSEX = "女";
                    }
                    item.sSJHM = query.FieldByName("SJHM").AsString;
                    item.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                    query.Next();
                }
                query.Close();

            }

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            KFPT_RWFB_Proc obj = new KFPT_RWFB_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sRWZT = query.FieldByName("RWZT").AsString;
            obj.sRW = query.FieldByName("RW").AsString;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            return obj;
        }
    }
}
