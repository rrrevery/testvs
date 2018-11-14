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
  public  class GTPT_QMQGZDYD_Proc : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public string sMDMC = string.Empty;
        public string dLJYXQ = string.Empty;
        public int iMDID = 0;




        public List<WECHAT_BUYCOUPONGZ_CARDMD> itemTable = new List<WECHAT_BUYCOUPONGZ_CARDMD>();
        public List<WX_BUGCOUPONDYDITEM_HYKTYPE> itemTable1 = new List<WX_BUGCOUPONDYDITEM_HYKTYPE>();
        public List<WX_BUGCOUPONDYDITEM> itemTable2 = new List<WX_BUGCOUPONDYDITEM>();


        public class WECHAT_BUYCOUPONGZ_CARDMD
        {//1门店表
            public int iJLBH = 0;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
        }
        public class WX_BUGCOUPONDYDITEM_HYKTYPE
        {//3卡类型表
            public int iJLBH = 0, iLIMIT_HYKTYPE;
            public int iYHQID = 0;
            public int iHYKTYPE = 0;
            public string sLIMITCONTENT_HYKTYPE = string.Empty;
            public string sYHQMC = string.Empty;
            public string sHYKNAME = string.Empty;
        }
        public class WX_BUGCOUPONDYDITEM
        {//2优惠券
            public int iJLBH = 0, iHYKTYPE;
            public int iYHQID = 0, iMDID, iZS = 0, iLIMIT, iLIMIT_DAY, iLIMIT_HY;
            public double iPAYMONEY = 0;
            public string sYHQMC = string.Empty;
            public double fYHQJE = 0, fDHJE, flimit = 0;
            public string dJSRQ = string.Empty;
            public string sJSMC = string.Empty;
            public string sHYKNAME = string.Empty;
            public string sMDMC = string.Empty;
            public string sTS = string.Empty;
            public string sIMG = string.Empty;
            public string sLIMITCONTENT_HY = string.Empty;
            public string sLIMITCONTENT_DAY = string.Empty;
            public string sLIMITCONTENT = string.Empty;
            public string sSHOWNAME = string.Empty;
            public string sHDJS = string.Empty;
            public string sGZSM = string.Empty;
            public string dENDTIME = string.Empty;
            public string sTITLE = string.Empty;
            public string sDESCRIBE = string.Empty;
            public string sURL = string.Empty;
            public string sIMG2 = string.Empty;
        }
      

     
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
           CrmLibProc.DeleteDataTables(query, out msg, "WX_BUGCOUPONDYD;WX_BUGCOUPONDYD_MD;WX_BUGCOUPONDYDITEM;WX_BUGCOUPONDYDITEM_HYKTYPE", "JLBH", iJLBH);

        }


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_BUGCOUPONDYD");

            query.SQL.Text = "insert into WX_BUGCOUPONDYD(JLBH,KSSJ,JSSJ,STATUS,DJR,DJRMC,DJSJ,PUBLICID,MDID)";
            query.SQL.Add(" values(:JLBH,:KSSJ,:JSSJ,:STATUS,:DJR,:DJRMC,:DJSJ,:PUBLICID,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSSJ").AsDateTime = Convert.ToDateTime(dKSRQ);
            query.ParamByName("JSSJ").AsDateTime = Convert.ToDateTime(dJSRQ);
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("MDID").AsInteger = iMDID;

            query.ParamByName("DJSJ").AsDateTime = serverTime;
      
                query.ParamByName("DJRMC").AsString = sLoginRYMC;

            
           
            query.ExecSQL();

            foreach (WECHAT_BUYCOUPONGZ_CARDMD one in itemTable)
            {
                query.SQL.Text = "insert into WX_BUGCOUPONDYD_MD (JLBH,MDID)";
                query.SQL.Add(" values(:JLBH,:MDID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ExecSQL();
            }


            foreach (WX_BUGCOUPONDYDITEM ones1 in itemTable2)
            {
                query.SQL.Text = "insert into WX_BUGCOUPONDYDITEM(JLBH,YHQID,PAYMONEY,YHQJE,JSRQ,ENDTIME,LIMIT,LIMITCONTENT,IMG,LIMIT_DAY,LIMITCONTENT_DAY,JSMC,HDJS,LIMIT_HY,LIMITCONTENT_HY,GZSM,TITLE,DESCRIBE,URL,IMG2)";
                query.SQL.Add(" values(:JLBH,:YHQID,:PAYMONEY,:YHQJE,:JSRQ,:ENDTIME,:LIMIT,:LIMITCONTENT,:IMG,:LIMIT_DAY,:LIMITCONTENT_DAY,:JSMC,:HDJS,:LIMIT_HY,:LIMITCONTENT_HY,:GZSM,:TITLE,:DESCRIBE,:URL,:IMG2)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                //query.ParamByName("HYKTYPE").AsInteger = ones1.iHYKTYPE;
                query.ParamByName("YHQID").AsInteger = ones1.iYHQID;

                query.ParamByName("PAYMONEY").AsFloat = ones1.iPAYMONEY * 100;

                query.ParamByName("YHQJE").AsFloat = ones1.fYHQJE;
                query.ParamByName("JSRQ").AsDateTime = Convert.ToDateTime(ones1.dJSRQ);
                query.ParamByName("ENDTIME").AsDateTime = Convert.ToDateTime(ones1.dENDTIME);
                query.ParamByName("LIMIT").AsInteger = ones1.iLIMIT;
                query.ParamByName("LIMITCONTENT").AsString = ones1.sLIMITCONTENT;


                query.ParamByName("IMG").AsString = ones1.sIMG;
                query.ParamByName("LIMIT_DAY").AsInteger = ones1.iLIMIT_DAY;
                query.ParamByName("LIMITCONTENT_DAY").AsString = ones1.sLIMITCONTENT_DAY;
                query.ParamByName("JSMC").AsString = ones1.sJSMC;


                query.ParamByName("HDJS").AsString = ones1.sHDJS;
                query.ParamByName("LIMIT_HY").AsInteger = ones1.iLIMIT_HY;
                query.ParamByName("LIMITCONTENT_HY").AsString = ones1.sLIMITCONTENT_HY;


                query.ParamByName("GZSM").AsString = ones1.sGZSM;



                //query.ParamByName("JE").AsFloat = (ones1.fJE) * 100;
                //query.ParamByName("DHJE").AsFloat = ones1.fDHJE;
                //query.ParamByName("LIMITMONEY").AsFloat = (ones1.fJE) * (ones1.iZS);


                //query.ParamByName("SHOWNAME").AsString = ones1.sSHOWNAME;
                query.ParamByName("TITLE").AsString = ones1.sTITLE;
                query.ParamByName("DESCRIBE").AsString = ones1.sDESCRIBE;
                query.ParamByName("URL").AsString = ones1.sURL;
                query.ParamByName("IMG2").AsString = ones1.sIMG2;
                query.ExecSQL();

            }
            foreach (WX_BUGCOUPONDYDITEM_HYKTYPE ones in itemTable1)
            {
                query.SQL.Text = "insert into WX_BUGCOUPONDYDITEM_HYKTYPE(JLBH,HYKTYPE,YHQID,LIMIT_HYKTYPE,LIMITCONTENT_HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:HYKTYPE,:YHQID,:LIMIT_HYKTYPE,:LIMITCONTENT_HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = ones.iHYKTYPE;
                query.ParamByName("YHQID").AsInteger = ones.iYHQID;
                query.ParamByName("LIMIT_HYKTYPE").AsInteger = ones.iLIMIT_HYKTYPE;

                query.ParamByName("LIMITCONTENT_HYKTYPE").AsString = ones.sLIMITCONTENT_HYKTYPE;



                query.ExecSQL();
            }

        

        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {


            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("iHYKTYPE", "M.HYKTYPE");
            CondDict.Add("iLPID", "M.LPID");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select A.* ,Y.MDMC from WX_BUGCOUPONDYD A ,WX_MDDY Y where Y.MDID=A.MDID and A.PUBLICID=" + iLoginPUBLICID;

            SetSearchQuery(query, lst);


            if (lst.Count == 1)
            {
                query.SQL.Text = "select D.*,Y.MDMC ";
                query.SQL.Add(" from WX_BUGCOUPONDYD_MD D,WX_MDDY Y where  D.MDID=Y.MDID");
                if (iJLBH != 0)
                    query.SQL.Add("  and D.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WECHAT_BUYCOUPONGZ_CARDMD item = new WECHAT_BUYCOUPONGZ_CARDMD();
                    ((GTPT_QMQGZDYD_Proc)lst[0]).itemTable.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.iJLBH = query.FieldByName("JLBH").AsInteger;
                    query.Next();
                }
            }
            query.Close();

            if (lst.Count == 1)
            {
                query.SQL.Text = "select M.*,Y.YHQMC ";
                query.SQL.Add(" from WX_BUGCOUPONDYDITEM M ,YHQDEF Y where  M.YHQID=Y.YHQID");
                if (iJLBH != 0)
                    query.SQL.Add("  and M.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_BUGCOUPONDYDITEM item3 = new WX_BUGCOUPONDYDITEM();
                    ((GTPT_QMQGZDYD_Proc)lst[0]).itemTable2.Add(item3);
                    //item3.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item3.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item3.iPAYMONEY = query.FieldByName("PAYMONEY").AsFloat / 100;
                    item3.fYHQJE = query.FieldByName("YHQJE").AsFloat;//元
                    item3.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    item3.dENDTIME = FormatUtils.DatetimeToString(query.FieldByName("ENDTIME").AsDateTime);
                    item3.iLIMIT = query.FieldByName("LIMIT").AsInteger;
                    item3.sLIMITCONTENT = query.FieldByName("LIMITCONTENT").AsString;

                    item3.iLIMIT_DAY = query.FieldByName("LIMIT_DAY").AsInteger;
                    item3.sLIMITCONTENT_DAY = query.FieldByName("LIMITCONTENT_DAY").AsString;

                    item3.iLIMIT_HY = query.FieldByName("LIMIT_HY").AsInteger;
                    item3.sLIMITCONTENT_HY = query.FieldByName("LIMITCONTENT_HY").AsString;


                    //item3.fDHJE = query.FieldByName("DHJE").AsFloat;
                    //item3.iZS =Convert.ToInt32( query.FieldByName("LIMITMONEY").AsFloat/ (item3.fJE));
                    //item3.iMDID = query.FieldByName("MDID").AsInteger;


                    item3.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item3.sJSMC = query.FieldByName("JSMC").AsString;
                    item3.sIMG = query.FieldByName("IMG").AsString;


                    item3.sHDJS = query.FieldByName("HDJS").AsString;
                    item3.sGZSM = query.FieldByName("GZSM").AsString;

                    item3.sSHOWNAME = query.FieldByName("SHOWNAME").AsString;

                    item3.sTITLE = query.FieldByName("TITLE").AsString;
                    item3.sDESCRIBE = query.FieldByName("DESCRIBE").AsString;
                    item3.sURL = query.FieldByName("URL").AsString;
                    item3.sIMG2 = query.FieldByName("IMG2").AsString;


                    query.Next();
                }




                query.Close();

                query.SQL.Text = "select H.*,E.HYKNAME,F.YHQMC";
                query.SQL.Add(" from WX_BUGCOUPONDYDITEM_HYKTYPE H,HYKDEF E,YHQDEF F  where  H.HYKTYPE=E.HYKTYPE AND H.YHQID=F.YHQID");
                if (iJLBH != 0)
                    query.SQL.Add("  and H.JLBH=" + iJLBH);
                query.SQL.Add("union");
                query.SQL.Add("select H.*,'非虚拟会员卡类型' as HYKNAME,F.YHQMC");
                query.SQL.Add(" from WX_BUGCOUPONDYDITEM_HYKTYPE H,YHQDEF F where  H.YHQID=F.YHQID and H.HYKTYPE=-1");
                if (iJLBH != 0)
                    query.SQL.Add("  and H.JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    WX_BUGCOUPONDYDITEM_HYKTYPE item2 = new WX_BUGCOUPONDYDITEM_HYKTYPE();
                    ((GTPT_QMQGZDYD_Proc)lst[0]).itemTable1.Add(item2);
                    item2.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item2.iYHQID = query.FieldByName("YHQID").AsInteger;
                    item2.iLIMIT_HYKTYPE = query.FieldByName("LIMIT_HYKTYPE").AsInteger;
                    item2.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item2.sLIMITCONTENT_HYKTYPE = query.FieldByName("LIMITCONTENT_HYKTYPE").AsString;
                    item2.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    query.Next();
                }

            }

            
            query.Close();


            query.Close();
            return lst;

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_BUGCOUPONDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Text = "update WX_BUGCOUPONDYD set ZZR=:ZZR,ZZRQ=:ZZRQ,STATUS=3,ZZRMC=:ZZRMC where STATUS=2  and PUBLICID=" + iLoginPUBLICID;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;

                query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            
          
            query.ExecSQL();

            query.SQL.Text = "update WX_BUGCOUPONDYD set QDR=:QDR,QDRMC=:QDRMC,QDRQ=:QDRQ,STATUS=:STATUS where JLBH=" + iJLBH;
            query.ParamByName("STATUS").AsInteger = 2;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRQ").AsDateTime = serverTime;
           query.ParamByName("QDRMC").AsString = sLoginRYMC;
            
          
            query.ExecSQL();
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_BUGCOUPONDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZRQ", 3);
        }



        public override object SetSearchData(CyQuery query)
        {
            GTPT_QMQGZDYD_Proc item = new GTPT_QMQGZDYD_Proc();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            item.iQDR = query.FieldByName("QDR").AsInteger;
            item.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDRQ").AsDateTime);
            item.iZZR = query.FieldByName("ZZR").AsInteger;
            item.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            item.iSTATUS = query.FieldByName("STATUS").AsInteger;
          
                item.sDJRMC = query.FieldByName("DJRMC").AsString;
                item.sZXRMC = query.FieldByName("ZXRMC").AsString;
                item.sQDRMC = query.FieldByName("QDRMC").AsString;
                item.sZZRMC = query.FieldByName("ZZRMC").AsString;
                item.iMDID = query.FieldByName("MDID").AsInteger;
                item.sMDMC = query.FieldByName("MDMC").AsString;

                return item;
          

            
        }
    }
}
