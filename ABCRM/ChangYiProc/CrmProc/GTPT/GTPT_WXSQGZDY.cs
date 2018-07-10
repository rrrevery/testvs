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
    public class GTPT_WXSQGZDY : BASECRMClass
    {
        public int iGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGZMC = string.Empty;
        public string sNOTE = string.Empty;

        public List<WX_MDDY> itemTable = new List<WX_MDDY>();
        public List<WX_YHQGZTEM> itemTable2 = new List<WX_YHQGZTEM>();
        public List<WX_YHQGZITEM_YHQ> itemTable3 = new List<WX_YHQGZITEM_YHQ>();

        public class WX_MDDY
        {
            public int iMDID = 0;
            public string sMDMC = string.Empty;
        }

        public class WX_YHQGZTEM
        {
            public int iGZID = 0;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
            public int iYHQID = 0;
            public string sYHQMC = string.Empty;
            public string dSYJSRQ = string.Empty;
            public int iXZCS = 0;
            public string sXZTS = string.Empty;
            public int iXZCS_DAY = 0;
            public string sXZTS_DAY = string.Empty;
            public int iXZCS_DAYHY = 0;
            public string sXZTS_DAYHY = string.Empty;
            public int iXZCS_HY = 0;
            public string sXZTS_HY = string.Empty;
            public string sWXZY = string.Empty;
        }

        public class WX_YHQGZITEM_YHQ
        {
            public int iMDID = 0;
            public string sMDMC = string.Empty;
            public int iYHQID = 0;
            public string sYHQMC = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public double fJE = 0;
            public string sWXZY = string.Empty;

        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_YHQGZ;MOBILE_YHQGZTEM;MOBILE_YHQGZITEM_YHQ", "GZID", iJLBH);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH","S.GZID");
            CondDict.Add("sGZMC","S.GZMC");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select  * FROM MOBILE_YHQGZ S ";
            query.SQL.Add(" where 1=1 ");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {

                query.SQL.Text = "select distinct(C.MDID),C.MDMC FROM MOBILE_YHQGZTEM Q,MDDY C ";
                query.SQL.Add(" where  Q.MDID=C.MDID ");
                if (iJLBH != 0)
                    query.SQL.Add("  AND Q.GZID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_MDDY item = new WX_MDDY();
                    ((GTPT_WXSQGZDY)lst[0]).itemTable.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    query.Next();
                }
                query.Close();

                query.SQL.Text = "select Q.* ,F.YHQMC,C.MDMC FROM MOBILE_YHQGZTEM Q,YHQDEF F,MDDY C ";
                query.SQL.Add(" where F.YHQID=Q.YHQID and Q.MDID=C.MDID  ");
                if (iJLBH != 0)
                    query.SQL.Add("  AND Q.GZID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_YHQGZTEM item = new WX_YHQGZTEM();
                    ((GTPT_WXSQGZDY)lst[0]).itemTable2.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item.dSYJSRQ = FormatUtils.DateToString(query.FieldByName("SYJSRQ").AsDateTime);
                    item.iXZCS = query.FieldByName("XZCS").AsInteger;
                    item.iXZCS_DAY = query.FieldByName("XZCS_DAY").AsInteger;
                    item.iXZCS_DAYHY = query.FieldByName("XZCS_DAYHY").AsInteger;
                    item.iXZCS_HY = query.FieldByName("XZCS_HY").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item.sXZTS = query.FieldByName("XZTS").AsString;
                    item.sXZTS_DAY = query.FieldByName("XZTS_DAY").AsString;
                    item.sXZTS_DAYHY = query.FieldByName("XZTS_DAYHY").AsString;
                    item.sXZTS_HY = query.FieldByName("XZTS_HY").AsString;
                    item.sWXZY = query.FieldByName("WXZY").AsString;
                    query.Next();
                }
                query.Close();

                query.SQL.Text = "select Q.* ,F.YHQMC,C.MDMC,B.HYKNAME FROM MOBILE_YHQGZITEM_YHQ Q,YHQDEF F,MDDY C,HYKDEF B ";
                query.SQL.Add(" where F.YHQID=Q.YHQID and Q.MDID=C.MDID and Q.HYKTYPE=B.HYKTYPE ");
                if (iJLBH != 0)
                    query.SQL.Add("  AND Q.GZID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_YHQGZITEM_YHQ item = new WX_YHQGZITEM_YHQ();
                    ((GTPT_WXSQGZDY)lst[0]).itemTable3.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item.sYHQMC = query.FieldByName("YHQMC").AsString;
                    query.Next();
                }
                query.Close();

            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXSQGZDY item = new GTPT_WXSQGZDY();
            item.iJLBH = query.FieldByName("GZID").AsInteger;
            item.sGZMC = query.FieldByName("GZMC").AsString;
            item.sNOTE = query.FieldByName("NOTE").AsString;
            return item;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            if (iJLBH != 0)
            {
                //修改规则时，要写变动记录
                int iJYBH = SeqGenerator.GetSeq("MOBILE_YHQGZ_BDJL");

                query.SQL.Text = " insert into MOBILE_YHQGZ_BDJL select :JYBH,GZID,GZMC,NOTE,DJR,DJRMC,DJSJ from MOBILE_YHQGZ where GZID=:GZID";
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                query.SQL.Text = " insert into MOBILE_YHQGZTEM_BDJL select :JYBH,GZID,MDID,YHQID,SYJSRQ,XZCS,XZTS,XZCS_DAY,XZTS_DAY,XZCS_DAYHY,XZTS_DAYHY,XZCS_HY,XZTS_HY,WXZY from MOBILE_YHQGZTEM where GZID=:GZID";
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                query.SQL.Text = " insert into MOBILE_YHQGZITEM_YHQ_BDJL select :JYBH,GZID,MDID,HYKTYPE,YHQID,JE from MOBILE_YHQGZITEM_YHQ where GZID=:GZID";
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                query.SQL.Text = " update MOBILE_YHQGZ set GXR=:GXR, GXRMC=:GXRMC, GXSJ=:GXSJ where GZID=:GZID";
                query.ParamByName("GXR").AsInteger = iLoginRYID;
                query.ParamByName("GXRMC").AsString = sLoginRYMC;

                
                query.ParamByName("GXSJ").AsDateTime = serverTime;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                DeleteDataQuery(out msg, query, serverTime);
            }
            iJLBH = SeqGenerator.GetSeq("MOBILE_YHQGZ");

            query.SQL.Text = "insert into  MOBILE_YHQGZ(GZID,GZMC,NOTE,DJR,DJRMC,DJSJ)";
            query.SQL.Add(" values(:GZID,:GZMC,:NOTE,:DJR,:DJRMC,:DJSJ)");
            query.ParamByName("GZID").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("GZMC").AsString = sGZMC;
            query.ParamByName("NOTE").AsString = sNOTE;
            query.ExecSQL();

            foreach (WX_YHQGZTEM one in itemTable2)
            {
                query.SQL.Text = "insert into MOBILE_YHQGZTEM(GZID,MDID,YHQID,SYJSRQ,XZCS,XZTS,XZCS_DAY,XZTS_DAY,XZCS_DAYHY,XZTS_DAYHY,XZCS_HY,XZTS_HY,WXZY)";
                query.SQL.Add(" values(:GZID,:MDID,:YHQID,:SYJSRQ,:XZCS,:XZTS,:XZCS_DAY,:XZTS_DAY,:XZCS_DAYHY,:XZTS_DAYHY,:XZCS_HY,:XZTS_HY,:WXZY)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ParamByName("YHQID").AsInteger = one.iYHQID;
                query.ParamByName("SYJSRQ").AsDateTime = FormatUtils.ParseDateString(one.dSYJSRQ);
                query.ParamByName("XZCS").AsInteger = one.iXZCS;
                query.ParamByName("XZCS_DAY").AsInteger = one.iXZCS_DAY;
                query.ParamByName("XZCS_DAYHY").AsInteger = one.iXZCS_DAYHY;
                query.ParamByName("XZCS_HY").AsInteger = one.iXZCS_HY;
                query.ParamByName("XZTS").AsString = one.sXZTS;
                query.ParamByName("XZTS_DAY").AsString = one.sXZTS_DAY;
                query.ParamByName("XZTS_DAYHY").AsString = one.sXZTS_DAYHY;
                query.ParamByName("XZTS_HY").AsString = one.sXZTS_HY;
                query.ParamByName("WXZY").AsString = one.sWXZY;
                query.ExecSQL();
            }

            foreach (WX_YHQGZITEM_YHQ one in itemTable3)
            {
                query.SQL.Text = "insert into MOBILE_YHQGZITEM_YHQ(GZID,MDID,HYKTYPE,YHQID,JE)";
                query.SQL.Add(" values(:GZID,:MDID,:HYKTYPE,:YHQID,:JE)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("YHQID").AsInteger = one.iYHQID;
                query.ParamByName("JE").AsFloat = one.fJE;
                query.ExecSQL();
            }

        }
    }

}
