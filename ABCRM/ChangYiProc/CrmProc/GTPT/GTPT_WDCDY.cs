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
    public class GTPT_WDCDY : DJLR_ZXQDZZ_CLass
    {
        public string sDCZT = string.Empty;
        public string sZY = string.Empty;
        public int iXZSL = 0;
        public int iLBID = 0;
        public string sLBMC = string.Empty;
        public string dKSRQ = string.Empty;
        public string dLJYXQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iCHANNELID = 0;
        public string sJLMS = string.Empty;
        public string sFINISHNOTE = string.Empty;
        public string sPUBLICNAME = string.Empty;
        public int iPUBLICID = 0;

        public List<MOBILE_DCDYDITEM_TM> itemTable1 = new List<MOBILE_DCDYDITEM_TM>();

        public class MOBILE_DCDYDITEM_TM
        {
            public int iJLBH = 0;
            public string sPAGE_NAME = string.Empty;
            public string sMC = string.Empty;
            public int iPAGE_ID = 0;
            public int iBJ_TYPE = 0;
            public int iID = 0;
            public string sBJ_TYPEMC = string.Empty;

        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_DCDYD;MOBILE_DCDYDITEM_TM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_DCDYD");

            query.SQL.Text = "insert into MOBILE_DCDYD(JLBH,DCZT,ZY,XZSL,LBID,LJYXQ,KSRQ,JSRQ,DJR,DJRMC,DJSJ,STATUS,JLMS,FINISHNOTE,PUBLICID,CHANNELID)";
            query.SQL.Add(" values(:JLBH,:DCZT,:ZY,:XZSL,:LBID,:LJYXQ,:KSRQ,:JSRQ,:DJR,:DJRMC,:DJSJ,:STATUS,:JLMS,:FINISHNOTE,:PUBLICID,:CHANNELID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("LJYXQ").AsDateTime = FormatUtils.ParseDateString(dLJYXQ);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("XZSL").AsInteger = iXZSL;
            query.ParamByName("LBID").AsInteger = iLBID;
            query.ParamByName("CHANNELID").AsInteger = iCHANNELID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID; ;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DCZT").AsString = sDCZT;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("JLMS").AsString = sJLMS;
            query.ParamByName("FINISHNOTE").AsString = sFINISHNOTE;

            query.ExecSQL();


            int bh = 1;
            foreach (MOBILE_DCDYDITEM_TM ones in itemTable1)
            {
                query.SQL.Text = "insert into MOBILE_DCDYDITEM_TM(JLBH,PAGE_ID,ID,BJ_TYPE,BH)";
                query.SQL.Add(" values(:JLBH,:PAGE_ID,:ID,:BJ_TYPE,:BH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("PAGE_ID").AsInteger = ones.iPAGE_ID;
                query.ParamByName("ID").AsInteger = ones.iID;
                query.ParamByName("BJ_TYPE").AsInteger = ones.iBJ_TYPE;
                query.ParamByName("BH").AsInteger = bh;
                query.ExecSQL();
                bh++;
            }

        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_DCDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
        }
        //List<Object> lst = new List<Object>();

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("dKSRQ", "A.KSRQ");
            CondDict.Add("dJSRQ", "A.JSRQ");
            CondDict.Add("iSTATUS", "A.STATUS");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iZXR", "A.ZXR");
            CondDict.Add("sZXRMC", "A.ZXRMC");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("iQDR", "A.QDR");
            CondDict.Add("sQDRMC", "A.QDRMC");
            CondDict.Add("dQDSJ", "A.QDSJ");
            CondDict.Add("iZZR", "A.ZZR");
            CondDict.Add("sZZRMC", "A.ZZRMC");
            CondDict.Add("dZZRQ", "A.ZZRQ");
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            query.SQL.Text = "select A.* ,B.LBMC,P.PUBLICNAME from  MOBILE_DCDYD A,MOBILE_LPZDEF_YHQ B,WX_PUBLIC P ";
            query.SQL.Add(" where  A.LBID=B.LBID  and A.PUBLICID=P.PUBLICID  and A.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select T.*,F.MC,F.ID from  MOBILE_DCDYDITEM_TM T,MOBILE_DCNRDEF F ";
                query.SQL.Add("  where T.ID=F.ID and t.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    MOBILE_DCDYDITEM_TM item2 = new MOBILE_DCDYDITEM_TM();
                    ((GTPT_WDCDY)lst[0]).itemTable1.Add(item2);
                    item2.iID = query.FieldByName("ID").AsInteger;
                    item2.iPAGE_ID = query.FieldByName("PAGE_ID").AsInteger;
                    item2.iBJ_TYPE = query.FieldByName("BJ_TYPE").AsInteger;
                    if (DbSystemName == "ORACLE")
                    {

                        item2.sMC = query.FieldByName("MC").AsString;
                    }
                    else if (DbSystemName == "SYBASE")
                    {

                        item2.sMC = query.FieldByName("MC").GetChineseString(200);
                    }
                    if (item2.iBJ_TYPE == 0)
                    {
                        item2.sBJ_TYPEMC = "单选";
                    }
                    if (item2.iBJ_TYPE == 1)
                    {
                        item2.sBJ_TYPEMC = "多选";
                    }
                    query.Next();
                }

                query.Close();

            }
            return lst;


        }


        public override object SetSearchData(CyQuery query)
        {

            GTPT_WDCDY item = new GTPT_WDCDY();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iLBID = query.FieldByName("LBID").AsInteger;
            item.iXZSL = query.FieldByName("XZSL").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.dLJYXQ = FormatUtils.DateToString(query.FieldByName("LJYXQ").AsDateTime);
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iQDR = query.FieldByName("QDR").AsInteger;
            item.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
            item.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
            item.iCHANNELID = query.FieldByName("CHANNELID").AsInteger;

            item.sLBMC = query.FieldByName("LBMC").AsString;
            item.sDCZT = query.FieldByName("DCZT").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.sJLMS = query.FieldByName("JLMS").AsString;
            //item.sFINISHNOTE = query.FieldByName("FINISHNOTE").AsString;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.sQDRMC = query.FieldByName("QDRMC").AsString;
            item.sZZRMC = query.FieldByName("ZZRMC").AsString;
            item.sPUBLICNAME = query.FieldByName("PUBLICNAME").AsString;
            item.sFINISHNOTE = query.FieldByName("FINISHNOTE").AsString;
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;



            return item;
        }

        //启动
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            //msg = string.Empty;
            //ExecTable(query, "MOBILE_DCDYD", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);

            msg = string.Empty;
            query.SQL.Text = "update MOBILE_DCDYD set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=:ZZRQ,STATUS=3  ";
            query.SQL.Add(" where JLBH<>:JLBH and QDR is not null and ZZR is null  and PUBLICID=:PUBLICID");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;



            query.ExecSQL();

            query.SQL.Text = "update MOBILE_DCDYD set QDR=:QDR,QDRMC=:QDRMC,QDSJ=:QDSJ,STATUS=2 where  PUBLICID=:PUBLICID and JLBH=" + iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;

           query.ParamByName("QDRMC").AsString = sLoginRYMC;
           query.ParamByName("QDSJ").AsDateTime = serverTime;

           
            query.ExecSQL();

        }
        //终止
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MOBILE_DCDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", -1);
        }



    }
}
