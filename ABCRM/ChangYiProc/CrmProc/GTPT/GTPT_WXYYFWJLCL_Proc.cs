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
    public class GTPT_WXYYFWJLCL_Proc : DJLR_ZXQDZZ_CLass
    {
      
        public string sBZ1 = string.Empty;

        public List<WX_YDFWJLITEM> itemTable = new List<WX_YDFWJLITEM>();
        public List<WX_YDFWJLITEM> itemTable2 = new List<WX_YDFWJLITEM>();
        public class WX_YDFWJLITEM
        {
            public int iJLBH = 0;
            public int iID = 0;
            public string sOPENID = string.Empty;
            public int iHYID = 0;
            public int iBJ = 0;
            public string sRQ = string.Empty;
            public string sGKXM = string.Empty;
            public string sLXDH = string.Empty;
            public string sFWNR = string.Empty;
            public string sBZ = string.Empty;
            public string dDJSJ= string.Empty;
            public int iCJRS = 0;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
            public string sMC = string.Empty;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_YDFWJL_NEW;MOBILE_YDFWJLITEM_NEW", "JLBH", iJLBH);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add( "iDJR","A.DJR");
            CondDict.Add("sDJRMC","A.DJRMC");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iZXR","A.ZXR");
            CondDict.Add("sZXRMC","A.ZXRMC");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select A.* from MOBILE_YDFWJL_NEW A where 1=1";
            query.SQL.Add(" and  A.PUBLICID=" + iLoginPUBLICID);
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select  A.*,C.MDMC,B.MC ";
                query.SQL.Add("  from MOBILE_YDFWJLITEM_NEW  A ,MOBILE_YDFWDEF B ,WX_MDDY C ");
                query.SQL.Add(" where A.ID=B.ID  and C.MDID=A.MDID  and  BJ_CJ=1");
                if (iJLBH != 0)
                    query.SQL.Add("  and A.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_YDFWJLITEM item = new WX_YDFWJLITEM();
                    ((GTPT_WXYYFWJLCL_Proc)lst[0]).itemTable.Add(item);

                    item.iJLBH = query.FieldByName("JLBH_NEW").AsInteger;
                    item.iID = query.FieldByName("ID").AsInteger;
                    item.sOPENID = query.FieldByName("OPENID").AsString;
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sRQ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
                    item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                    item.sLXDH = query.FieldByName("LXDH").AsString;
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sGKXM = query.FieldByName("GKXM").AsString;
                    item.sMC = query.FieldByName("FWNR").AsString;
                    item.sBZ = query.FieldByName("BZ").AsString;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.sMC = query.FieldByName("MC").AsString;
                    query.Next();
                }

                query.Close();
                query.SQL.Text = "select  A.*,C.MDMC,B.MC ";
                query.SQL.Add("  from MOBILE_YDFWJLITEM_NEW  A ,MOBILE_YDFWDEF B ,WX_MDDY C ");
                query.SQL.Add(" where A.ID=B.ID  and C.MDID=A.MDID  and  BJ_CJ=2");
                if (iJLBH != 0)
                    query.SQL.Add("  and A.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_YDFWJLITEM item = new WX_YDFWJLITEM();
                    ((GTPT_WXYYFWJLCL_Proc)lst[0]).itemTable2.Add(item);
                    item.iJLBH = query.FieldByName("JLBH_NEW").AsInteger;
                    item.iID = query.FieldByName("ID").AsInteger;
                    item.sOPENID = query.FieldByName("OPENID").AsString;
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sRQ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
                    item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                    item.sLXDH = query.FieldByName("LXDH").AsString;
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sGKXM = query.FieldByName("GKXM").AsString;
                    item.sMC = query.FieldByName("FWNR").AsString;
                    item.sBZ = query.FieldByName("BZ").AsString;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.sMC = query.FieldByName("MC").AsString;
                    query.Next();
                }

            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXYYFWJLCL_Proc item = new GTPT_WXYYFWJLCL_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sBZ1 = query.FieldByName("BZ").AsString;
            //item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            return item;
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_YDFWJL_NEW");

            query.SQL.Text = "insert into MOBILE_YDFWJL_NEW(JLBH,BZ,DJSJ,DJR,DJRMC,PUBLICID)";
            query.SQL.Add("values(:JLBH,:BZ,:DJSJ,:DJR,:DJRMC,:PUBLICID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("BZ").AsString = sBZ1;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ExecSQL();

            foreach (WX_YDFWJLITEM one in itemTable)//参加预约的
            {

                query.SQL.Text = "insert into MOBILE_YDFWJLITEM_NEW(JLBH_NEW,JLBH,ID,OPENID,HYID,RQ,GKXM,LXDH,FWNR,BZ,DJSJ,BJ_CJ,CJRS,MDID)";//
                query.SQL.Add(" values (:JLBH_NEW,:JLBH,:ID,:OPENID,:HYID,:RQ,:GKXM,:LXDH,:FWNR,:BZ,:DJSJ,:BJ_CJ,:CJRS,:MDID)");//,:DHJF
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("JLBH_NEW").AsInteger = one.iJLBH;
                query.ParamByName("ID").AsInteger = one.iID;
                query.ParamByName("OPENID").AsString = one.sOPENID;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("RQ").AsDateTime = FormatUtils.ParseDatetimeString(one.sRQ);
                query.ParamByName("LXDH").AsString = one.sLXDH;
                query.ParamByName("DJSJ").AsDateTime = FormatUtils.ParseDatetimeString(one.dDJSJ);
                query.ParamByName("BJ_CJ").AsInteger = 1;
                query.ParamByName("CJRS").AsInteger = one.iCJRS;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ParamByName("GKXM").AsString = one.sGKXM;
                query.ParamByName("BZ").AsString = one.sBZ;
                query.ParamByName("FWNR").AsString = one.sMC;
                query.ExecSQL();

            }
            foreach (WX_YDFWJLITEM one in itemTable2)//不参加预约的
            {

                query.SQL.Text = "insert into MOBILE_YDFWJLITEM_NEW(JLBH_NEW,JLBH,ID,OPENID,HYID,RQ,GKXM,LXDH,FWNR,BZ,DJSJ,BJ_CJ ,CJRS,MDID)";//
                query.SQL.Add(" values (:JLBH_NEW,:JLBH,:ID,:OPENID,:HYID,:RQ,:GKXM,:LXDH,:FWNR,:BZ,:DJSJ,:BJ_CJ,:CJRS,:MDID)");//,:DHJF

                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("JLBH_NEW").AsInteger = one.iJLBH;
                query.ParamByName("ID").AsInteger = one.iID;
                query.ParamByName("OPENID").AsString = one.sOPENID;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("RQ").AsDateTime = FormatUtils.ParseDatetimeString(one.sRQ);
                query.ParamByName("LXDH").AsString = one.sLXDH;
                query.ParamByName("DJSJ").AsDateTime = FormatUtils.ParseDatetimeString(one.dDJSJ);
                query.ParamByName("BJ_CJ").AsInteger = 2;
                query.ParamByName("CJRS").AsInteger = one.iCJRS;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ParamByName("GKXM").AsString = one.sGKXM;
                query.ParamByName("BZ").AsString = one.sBZ;
                query.ParamByName("FWNR").AsString = one.sMC;
                query.ExecSQL();
            }


        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            ExecTable(query, "MOBILE_YDFWJL_NEW", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ");
            foreach (WX_YDFWJLITEM one in itemTable)
            {

                query.SQL.Text = "update MOBILE_YDFWJL set BJ_CJ=:BJ_CJ,BZ=:BZ,ZXR=:ZXR,ZXRMC=:ZXRMC,ZXRQ=:ZXRQ where JLBH=:JLBH ";

                query.ParamByName("JLBH").AsInteger = one.iJLBH;
                query.ParamByName("BJ_CJ").AsInteger = 1;
                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                query.ParamByName("ZXRQ").AsDateTime = serverTime;
                query.ParamByName("BZ").AsString = one.sBZ;
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;

                query.ExecSQL();

            }
            foreach (WX_YDFWJLITEM one in itemTable2)
            {

                query.SQL.Text = "update MOBILE_YDFWJL set BJ_CJ=:BJ_CJ,BZ=:BZ,ZXR=:ZXR,ZXRMC=:ZXRMC,ZXRQ=:ZXRQ where JLBH=:JLBH ";

                query.ParamByName("JLBH").AsInteger = one.iJLBH;
                query.ParamByName("BJ_CJ").AsInteger = 2;
                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                query.ParamByName("ZXRQ").AsDateTime = serverTime;
                query.ParamByName("BZ").AsString = one.sBZ;
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;

                query.ExecSQL();

            }

        }

    }
}
