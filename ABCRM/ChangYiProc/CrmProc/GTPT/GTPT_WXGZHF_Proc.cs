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
    public class GTPT_WXGZHF_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iASKID 
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public int iGZJLBH = 0, iANSWERID;
        public string sANSWER = string.Empty;
        public int iBJ_DY = -1;
        public int iBJ_NONE = -1;
        public string sASK = string.Empty;
        public string sNAME = string.Empty;
        //iBJ_DY=1,iBJ_NONE=0关注回复  
        //iBJ_DY=0,iBJ_NONE=1默认回复
        //iBJ_DY=0,iBJ_NONE=0关键字回复     
        public string sWX_MID = string.Empty;
        public string sTITLE = string.Empty;
        public string sDESCRIPTION = string.Empty;
        public string sMUSICURL = string.Empty;
        public int iPUBLICID=0;

        public List<WX_ANSWERITEM> itemTable = new List<WX_ANSWERITEM>();
        public class WX_ANSWERITEM
        {
            public int iJLBH = 0, iASKID;
            public int iINX = 0;
            public string sURL = string.Empty;
            public string sPIC_URL = string.Empty;
            public string sTITLE = string.Empty;
            public string sDESCRIPTION = string.Empty;

        }
        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_ANSWER", serverTime, "ASKID");
        }
        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_ANSWERITEM;WX_ANSWER", "ASKID", iJLBH);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "R.ASKID");
            CondDict.Add("iGZJLBH", "R.GZJLBH");
            CondDict.Add("iTYPE", "K.TYPE");

            query.SQL.Text = "select R.ASKID,K.ASK,R.ANSWER,R.GZJLBH,F.NAME,R.WX_MID,R.TITLE,R.DESCRIPTION,R.MUSICURL,K.PUBLICID ";
            query.SQL.Add("  from WX_ANSWER R,WX_ANSWERGZDEF F,WX_ASK K");
            query.SQL.Add("  where R.ASKID=K.ASKID and R.GZJLBH=F.JLBH and K.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from  WX_ANSWERITEM I  ";
                query.SQL.AddLine(" where I.ASKID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_ANSWERITEM obj = new WX_ANSWERITEM();
                    obj.iJLBH = query.FieldByName("ASKID").AsInteger;
                    obj.iINX = query.FieldByName("INX").AsInteger;
                    obj.sURL = query.FieldByName("URL").AsString;
                    obj.sPIC_URL = query.FieldByName("PIC_URL").AsString;
                    obj.sTITLE = query.FieldByName("TITLE").AsString;
                    obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
                    ((GTPT_WXGZHF_Proc)lst[0]).itemTable.Add(obj);
                    query.Next();

                }
                query.Close();

            }
            return lst;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            //else
                //iJLBH = SeqGenerator.GetSeq("WX_ANSWER");
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);
            query.SQL.Text = "insert into WX_ANSWER(ASKID,GZJLBH,ANSWER,WX_MID,TITLE,DESCRIPTION,MUSICURL)";
            query.SQL.Add(" values(:JLBH,:GZJLBH,:ANSWER,:WX_MID,:TITLE,:DESCRIPTION,:MUSICURL)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GZJLBH").AsInteger = iGZJLBH;
            query.ParamByName("ANSWER").AsString = sANSWER;
            query.ParamByName("WX_MID").AsString = sWX_MID;
            query.ParamByName("TITLE").AsString = sTITLE;
            query.ParamByName("DESCRIPTION").AsString = sDESCRIPTION;
            query.ParamByName("MUSICURL").AsString = sMUSICURL;


            query.ExecSQL();
            foreach (WX_ANSWERITEM one in itemTable)
            {
                query.SQL.Text = "insert into WX_ANSWERITEM(ASKID,INX,URL,PIC_URL,TITLE,DESCRIPTION)";
                query.SQL.Add(" values(:JLBH,:INX,:URL,:PIC_URL,:TITLE,:DESCRIPTION)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("URL").AsString = one.sURL;
                query.ParamByName("PIC_URL").AsString = one.sPIC_URL;
                query.ParamByName("TITLE").AsString = one.sTITLE;
                query.ParamByName("DESCRIPTION").AsString = one.sDESCRIPTION;
                query.ExecSQL();
            }
        }

        //启动
        //public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        //{
        //    msg = string.Empty;
        //    ExecTable(query, "WX_ANSWER", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
        //}
        ////终止
        //public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        //{
        //    msg = string.Empty;
        //    ExecTable(query, "WX_ANSWER", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", -1);
        //}
        //public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        //{

        //    msg = string.Empty;

        //    query.SQL.Text = "select ASKID from WX_ANSWER where ASKID=:ASKID ";
        //    query.ParamByName("ASKID").AsInteger = iJLBH;
        //    query.Open();
        //    if (!query.Eof)
        //    {
        //        msg = "当前问题/关键字已定义！";
        //        return false;
        //    }

        //    return true;
        //}
        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXGZHF_Proc obj = new GTPT_WXGZHF_Proc();
            obj.iJLBH = query.FieldByName("ASKID").AsInteger;
            obj.iGZJLBH = query.FieldByName("GZJLBH").AsInteger;
            obj.sWX_MID = query.FieldByName("WX_MID").AsString;
            obj.sMUSICURL = query.FieldByName("MUSICURL").AsString;
            obj.sASK = query.FieldByName("ASK").AsString;
            obj.sANSWER = query.FieldByName("ANSWER").AsString;
            obj.sNAME = query.FieldByName("NAME").AsString;
            obj.sTITLE = query.FieldByName("TITLE").AsString;
            obj.sDESCRIPTION = query.FieldByName("DESCRIPTION").AsString;
            obj.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return obj;
        }
    }
}
