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
    public class GTPT_WXZXHDDY_Proc : BASECRMClass
    {
        public int iHDID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sACTNAME = string.Empty;
        public int iINX = 0;
        public string sACTIME = string.Empty;
        public string sIMG = string.Empty;
        public string sDESCRIBE = string.Empty;
        public int iTY = 0;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sHDNR = string.Empty;
        public int iPUBLICID = 0;

        public List<WX_NEWACTIVITY_STORE> itemTable = new List<WX_NEWACTIVITY_STORE>();//门店定义

        public class WX_NEWACTIVITY_STORE
        {
            public int iACTID = 0;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_NEWACTIVITY;MOBILE_NEWACTIVITY_STORE", "ACTID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_NEWACTIVITY");
            query.SQL.Text = "insert into MOBILE_NEWACTIVITY(ACTID,ACTNAME,INX,ACTIME,IMG,DESCRIBE,BJ_TY,KSRQ,JSRQ,HDNR,PUBLICID)";
            query.SQL.Add(" values(:ACTID,:ACTNAME,:INX,:ACTIME,:IMG,:DESCRIBE,:BJ_TY,:KSRQ,:JSRQ,:HDNR,:PUBLICID)");
            query.ParamByName("ACTID").AsInteger = iJLBH;
            query.ParamByName("INX").AsInteger = iINX;
            query.ParamByName("IMG").AsString = sIMG;
            query.ParamByName("BJ_TY").AsInteger = iTY;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID; ;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("ACTNAME").AsString = sACTNAME;
            query.ParamByName("ACTIME").AsString = sACTIME;
            query.ParamByName("DESCRIBE").AsString = sDESCRIBE;
            query.ParamByName("HDNR").AsString = sHDNR;

            query.ExecSQL();

            foreach (WX_NEWACTIVITY_STORE one in itemTable)
            {
                query.SQL.Text = "insert into MOBILE_NEWACTIVITY_STORE(ACTID,MDID)";
                query.SQL.Add(" values(:ACTID,:MDID)");
                query.ParamByName("ACTID").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = one.iMDID;

                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.ACTID");
            CondDict.Add("iTYPE", "L.TYPE");
            CondDict.Add("sNAME", "L.NAME");
            CondDict.Add("sIMG", "L.IMG");
            CondDict.Add("sDESCRIBE", "L.DESCRIBE");
            CondDict.Add("sCONTENT", "L.CONTENT");
            CondDict.Add("dKSRQ", "L.KSRQ");
            CondDict.Add("dJSRQ", "L.JSRQ");
            CondDict.Add("sADRESS", "L.ADRESS");
            CondDict.Add("iWX_MDID", "L.WX_MDID");
            CondDict.Add("iINX", "L.INX");
            CondDict.Add("sMDMC", "D.MDMC");
            CondDict.Add("sTIMES", "L.TIMES");
            CondDict.Add("iTY", "L.BJ_TY");
            CondDict.Add("sACTIME", "L.ACTIME");

            query.SQL.Text = "select * from  MOBILE_NEWACTIVITY L where  L.PUBLICID=" + iLoginPUBLICID;
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                //门店定义
                query.SQL.Text = "select A.*,C.MDMC  from  MOBILE_NEWACTIVITY_STORE A,WX_MDDY C  ";
                query.SQL.Add(" where  A.MDID=C.MDID");
                if (iJLBH != 0)
                    query.SQL.Add("  and A.ACTID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_NEWACTIVITY_STORE item = new WX_NEWACTIVITY_STORE();
                    ((GTPT_WXZXHDDY_Proc)lst[0]).itemTable.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    query.Next();
                }
                query.Close();

            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXZXHDDY_Proc item = new GTPT_WXZXHDDY_Proc();
            item.iJLBH = query.FieldByName("ACTID").AsInteger;
            item.iINX = query.FieldByName("INX").AsInteger;
            item.sIMG = query.FieldByName("IMG").AsString;
            item.iTY = query.FieldByName("BJ_TY").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.sACTNAME = query.FieldByName("ACTNAME").AsString;
            item.sACTIME = query.FieldByName("ACTIME").AsString;
            item.sDESCRIBE = query.FieldByName("DESCRIBE").AsString;
            item.sHDNR =LibProc.CyUrlEncode( query.FieldByName("HDNR").AsString);
            item.iPUBLICID = query.FieldByName("PUBLICID").AsInteger;

            return item;
        }



    }
}
