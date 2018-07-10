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
    public class GTPT_BMQFFD_Proc : DJLR_ZXQDZZ_CLass
    {
        int BJ = 0;

        public int iBMQID = 0;
        public string sBMQMC = string.Empty;
        public int iCXID = 0;
        public int iWX_MDID = 0;
        public string sCXZT = string.Empty;
        public int iLMSHID = 0;
        public string sMDMC = string.Empty;
        public string sSHMC = string.Empty;
        public string dQKSRQ = string.Empty;
        public string dQJSRQ = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iQYXQTS = 0;
        public int iFFPT = 0;
        public int iLBBJ= 0;
        public int iBUY = 0;
        public string sTITLE_MZ = string.Empty;
        public string sDESCRIBE_MZ = string.Empty;
        public string sURL_MZ = string.Empty;
        public string sIMG_MZ = string.Empty;

        public List<BMQFFDYD_MZ> MZitemTable = new List<BMQFFDYD_MZ>();
        public List<BMQFFDYD_SH> SHitemTable = new List<BMQFFDYD_SH>();
        public List<BMQFFDYD_SD> SDitemTable = new List<BMQFFDYD_SD>();
        public List<BMQFFDYD_LB> LBitemTable = new List<BMQFFDYD_LB>();

        public class BMQFFDYD_MZ
        {
            public double fMZJE = 0;
            public string sNAME = string.Empty;
            public int iBMQFFGZID = 0;
            public string sGZMC = string.Empty;

            public string sIMG = string.Empty;
            public string sLOGO = string.Empty;
            public string sCONTENT = string.Empty;
            public string sSYFW = string.Empty;
            public string sSYXQ = string.Empty;
            public string sLQXZ = string.Empty;
            public string sSBMC = string.Empty;
            public int iSBID = 0;
            public int iGMJE = 0;
            public string dENDTIME = string.Empty;


        }

        public class BMQFFDYD_LB
        {
            public int iGMJE = 0;
            public string sNAME = string.Empty;
            public int iBMQFFGZID = 0;
            public string sGZMC = string.Empty;

            public string sIMG = string.Empty;
            public string sLOGO = string.Empty;
            public string sCONTENT = string.Empty;
            public string sSYFW = string.Empty;
            public string sSYXQ = string.Empty;
            public string sLQXZ = string.Empty;
            public string sLBMC = string.Empty;
            public int iLBID = 0;
            public string dENDTIME_LB = string.Empty;
            public int iBUY = 0;
            public string sTITLE_LB = string.Empty;
            public string sDESCRIBE_LB = string.Empty;
            public string sURL_LB = string.Empty;
            public string sIMG_LB = string.Empty;
          

        }
        public class BMQFFDYD_SH
        {
            public int iSJLX = 0;
            public string sSJLX = string.Empty;
            public int iSJNR = 0;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
            public string sSHMC = string.Empty;
        }
        public class BMQFFDYD_SD
        {
            public string dKSRQ = string.Empty;
            public string dJSRQ = string.Empty;
            public int iKSSJ = 0;
            public int iJSSJ = 0;
        }

        //public new string[] asFieldNames = { 
        //                                   "iJLBH;F.JLBH",
        //                                   "sNAME;F.NAME",
        //                                   "iBMQID;F.BMQID",
        //                                   "dKSRQ;F.KSRQ",
        //                                   "dJSRQ;F.JSRQ",
        //                                   "dQKSRQ;F.QKSRQ",
        //                                   "dQJSRQ;F.QJSRQ",
        //                                   "iFFPT;F.FFPT",
        //                                   "iCXID;F.CXID",
        //                                   "iDJR;F.DJR",
        //                                   "sDJRMC;F.DJRMC",
        //                                   "dDJSJ;F.DJSJ",
        //                                   "iZXR;F.ZXR",
        //                                   "sZXRMC;F.ZXRMC",
        //                                   "dZXRQ;F.ZXRQ",
        //                                   "iQDR;F.QDR",
        //                                   "sQDRMC;F.QDRMC",
        //                                   "dQDSJ;F.QDSJ",
        //                                   "iZZR;F.ZZR",
        //                                   "sZZRMC;F.ZZRMC",
        //                                   "dZZRQ;F.ZZRQ",
        //                                     "iSTATUS;F.STATUS"

        //                                   };


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
           CrmLibProc.DeleteDataTables(query, out msg, "BMQFFDYD;BMQFFDYD_MZ;BMQFFDYD_LB", "JLBH", iJLBH);
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {

            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","F.JLBH");
            CondDict.Add("iBMQID","F.BMQID");
            CondDict.Add("iCXID","F.CXID");
            CondDict.Add("sNAME","F.NAME");
            CondDict.Add("dKSRQ","F.KSRQ");
            CondDict.Add("dJSRQ","F.JSRQ");
            CondDict.Add("iZXR", "F.ZXR");
            CondDict.Add("sZXRMC", "F.ZXRMC");
            CondDict.Add("dZXRQ", "F.ZXRQ");
            CondDict.Add("iDJR", "F.DJR");
            CondDict.Add("sDJRMC", "F.DJRMC");
            CondDict.Add("dDJSJ", "F.DJSJ");

            query.SQL.Text = "select F.*,B.BMQMC,C.CXZT,Y.MDMC  from BMQFFDYD F,BMQDEF B,WX_CXHDDEF C,WX_MDDY Y where F.BMQID=B.BMQID(+)  and F.CXID=C.CXID AND Y.MDID=F.MDID";
            SetSearchQuery(query, lst);
            if(lst.Count==1)
            {
                query.SQL.Text = "select M.*,B.GZMC,S.SBMC from BMQFFDYD_MZ M,BMQFFGZ B ,WX_SB S where M.BMQFFGZID=B.BMQFFGZID AND S.SBID(+)=M.SBID WHERE M.JLBH="+iJLBH;

                query.Open();
                    while (!query.Eof)
                        {
                            BMQFFDYD_MZ item = new BMQFFDYD_MZ();
                            ((GTPT_BMQFFD_Proc)lst[0]).MZitemTable.Add(item);
                            item.fMZJE = query.FieldByName("MZJE").AsFloat;
                            item.sNAME = query.FieldByName("NAME").AsString;
                            item.iBMQFFGZID = query.FieldByName("BMQFFGZID").AsInteger;
                            item.sGZMC = query.FieldByName("GZMC").AsString;
                            item.sIMG = query.FieldByName("IMG").AsString;
                            item.sLOGO = query.FieldByName("LOGO").AsString;
                            item.iSBID = query.FieldByName("SBID").AsInteger;
                            item.sSBMC= query.FieldByName("SBMC").AsString;
                            item.iGMJE = query.FieldByName("GMJE").AsInteger;
                            item.dENDTIME = FormatUtils.DatetimeToString(query.FieldByName("ENDTIME").AsDateTime);
                            item.sSYFW = query.FieldByName("SYFW").AsString;
                            item.sSYXQ = query.FieldByName("SYXQ").AsString;


                            query.Next();
                        }




                query.Close();

            }


               if (lst.Count == 1 && BJ == 2)
                        {
                            query.SQL.Text = "select M.*,B.GZMC,S.LBMC from BMQFFDYD_LB M,BMQFFGZ B ,BMQLBDEF S where M.BMQFFGZID=B.BMQFFGZID AND S.LBID=M.LBID";
                            query.SQL.Add(" and M.JLBH=" + iJLBH);
                            query.Open();
                            while (!query.Eof)
                            {
                                BMQFFDYD_LB item = new BMQFFDYD_LB();
                                ((GTPT_BMQFFD_Proc)lst[0]).LBitemTable.Add(item);
                                item.iGMJE = query.FieldByName("GMJE").AsInteger;
                                item.sNAME = query.FieldByName("NAME").AsString;
                                item.iBMQFFGZID = query.FieldByName("BMQFFGZID").AsInteger;
                                item.sGZMC = query.FieldByName("GZMC").AsString;
                                item.sIMG = query.FieldByName("IMG").AsString;
                                item.sLOGO = query.FieldByName("LOGO").AsString;
                                item.iLBID = query.FieldByName("LBID").AsInteger;
                                item.sLBMC = query.FieldByName("LBMC").AsString;
                                item.dENDTIME_LB = FormatUtils.DatetimeToString(query.FieldByName("ENDTIME").AsDateTime);
                                item.sSYFW = query.FieldByName("SYFW").AsString;
                                item.sSYXQ = query.FieldByName("SYXQ").AsString;
                                item.sTITLE_LB = query.FieldByName("TITLE").AsString;
                                item.sDESCRIBE_LB = query.FieldByName("DESCRIBE").AsString;
                                item.sURL_LB = query.FieldByName("URL").AsString;
                                item.sIMG_LB = query.FieldByName("IMG2").AsString;
                                query.Next();
                            }
                            query.Close();
                        }   
            return lst;
        }




        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (iJLBH != 0)
                DeleteDataQuery(out msg, query, serverTime);
            else
                iJLBH = SeqGenerator.GetSeq("BMQFFDYD");
            query.SQL.Text = "insert into BMQFFDYD(JLBH,NAME,BMQID,CXID,KSRQ,JSRQ,QKSRQ,QJSRQ,QYXQTS,FFPT,STATUS,DJR,DJRMC,DJSJ,PUBLICID,LBBJ,BJ_BUY,MDID,TITLE,DESCRIBE,URL,IMG)";
            query.SQL.Add(" values(:JLBH,:NAME,:BMQID,:CXID,:KSRQ,:JSRQ,:QKSRQ,:QJSRQ,:QYXQTS,:FFPT,:STATUS,:DJR,:DJRMC,:DJSJ,:PUBLICID,:LBBJ,:BJ_BUY,:MDID,:TITLE,:DESCRIBE,:URL,:IMG)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("NAME").AsString = sNAME;
            query.ParamByName("BMQID").AsInteger = iBMQID;
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("PUBLICID").AsInteger = iLoginPUBLICID;
            query.ParamByName("KSRQ").AsDateTime = Convert.ToDateTime(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = Convert.ToDateTime(dJSRQ);
            query.ParamByName("QKSRQ").AsDateTime = Convert.ToDateTime(dQKSRQ);
            query.ParamByName("QJSRQ").AsDateTime = Convert.ToDateTime(dQJSRQ);
            query.ParamByName("QYXQTS").AsInteger = iQYXQTS;
            query.ParamByName("FFPT").AsInteger = iFFPT;
            query.ParamByName("STATUS").AsInteger = 0;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("LBBJ").AsInteger = iLBBJ;
            query.ParamByName("BJ_BUY").AsInteger = iBUY;
            query.ParamByName("MDID").AsInteger = iWX_MDID;
            query.ParamByName("TITLE").AsString = sTITLE_MZ;
            query.ParamByName("DESCRIBE").AsString = sDESCRIBE_MZ;
            query.ParamByName("URL").AsString = sURL_MZ;
            query.ParamByName("IMG").AsString = sIMG_MZ;
            query.ExecSQL();

            foreach (BMQFFDYD_MZ one in MZitemTable)
            {
                query.SQL.Text = "insert into BMQFFDYD_MZ(JLBH,MZJE,NAME,BMQFFGZID,IMG,LOGO,SBID,GMJE,ENDTIME,SYFW,SYXQ)";
                query.SQL.Add(" values(:JLBH,:MZJE,:NAME,:BMQFFGZID,:IMG,:LOGO,:SBID,:GMJE,:ENDTIME,:SYFW,:SYXQ)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("MZJE").AsFloat = one.fMZJE;
                query.ParamByName("NAME").AsString = one.sNAME;
                query.ParamByName("BMQFFGZID").AsInteger = one.iBMQFFGZID;
                query.ParamByName("IMG").AsString = one.sIMG;
                query.ParamByName("LOGO").AsString = one.sLOGO;
                query.ParamByName("SBID").AsInteger = one.iSBID;
                query.ParamByName("GMJE").AsInteger = one.iGMJE;
                query.ParamByName("ENDTIME").AsDateTime = Convert.ToDateTime(one.dENDTIME);
                query.ParamByName("SYFW").AsString = one.sSYFW;
                query.ParamByName("SYXQ").AsString = one.sSYXQ;

                query.ExecSQL();
            }


            foreach (BMQFFDYD_LB one1 in LBitemTable)
            {
                query.SQL.Text = "insert into BMQFFDYD_LB(JLBH,GMJE,NAME,BMQFFGZID,IMG,LOGO,LBID,ENDTIME,SYFW,SYXQ,TITLE,DESCRIBE,URL,IMG2)";
                query.SQL.Add(" values(:JLBH,:GMJE,:NAME,:BMQFFGZID,:IMG,:LOGO,:LBID,:ENDTIME,:SYFW,:SYXQ,:TITLE,:DESCRIBE,:URL,:IMG2)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("GMJE").AsInteger = one1.iGMJE;
                query.ParamByName("NAME").AsString = one1.sNAME;
                query.ParamByName("BMQFFGZID").AsInteger = one1.iBMQFFGZID;
                query.ParamByName("IMG").AsString = one1.sIMG;
                query.ParamByName("LOGO").AsString = one1.sLOGO;
                query.ParamByName("LBID").AsInteger = one1.iLBID;
                query.ParamByName("ENDTIME").AsDateTime = Convert.ToDateTime(one1.dENDTIME_LB);
                query.ParamByName("SYFW").AsString = one1.sSYFW;
                query.ParamByName("SYXQ").AsString = one1.sSYXQ;
                query.ParamByName("TITLE").AsString = one1.sTITLE_LB;
                query.ParamByName("DESCRIBE").AsString = one1.sDESCRIBE_LB;
                query.ParamByName("URL").AsString = one1.sURL_LB;
                query.ParamByName("IMG2").AsString =one1.sIMG_LB;

                query.ExecSQL();
            }
           
         
        }
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            ExecTable(query, "BMQFFDYD", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";

            msg = string.Empty;
            query.SQL.Text = "update BMQFFDYD set ZZR=:ZZR,ZZSJ=:ZZRQ,STATUS=3,ZZRMC=:ZZRMC where STATUS=2 and JSRQ>=:RQ1 and KSRQ<=:RQ2  and  PUBLICID=" + iLoginPUBLICID;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;
           
            query.ParamByName("RQ1").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("RQ2").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
          
                query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            
           
            query.ExecSQL();

            query.SQL.Text = "update BMQFFDYD set QDR=:QDR,QDRMC=:QDRMC,QDSJ=:QDSJ,STATUS=2 where JLBH=" + iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDSJ").AsDateTime = serverTime;

                query.ParamByName("QDRMC").AsString = sLoginRYMC;
            
           
            query.ExecSQL();


         //   ExecTable(query, "BMQFFDYD", serverTime, "JLBH", "QDR", "QDRMC", "QDSJ", 2);
            
        }

        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            ExecTable(query, "BMQFFDYD", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZSJ", 3);
          
        }
           public override object SetSearchData(CyQuery query)
        {
              GTPT_BMQFFD_Proc obj = new GTPT_BMQFFD_Proc();
     
                        obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                        obj.sNAME = query.FieldByName("NAME").AsString;
                        obj.iBMQID = query.FieldByName("BMQID").AsInteger;
                        obj.sBMQMC = query.FieldByName("BMQMC").AsString;
                        obj.iCXID = query.FieldByName("CXID").AsInteger;
                        obj.sCXZT = query.FieldByName("CXZT").AsString;
                        obj.dKSRQ = FormatUtils.DatetimeToString(query.FieldByName("KSRQ").AsDateTime);
                        obj.dJSRQ = FormatUtils.DatetimeToString(query.FieldByName("JSRQ").AsDateTime);
                        obj.dQKSRQ = FormatUtils.DateToString(query.FieldByName("QKSRQ").AsDateTime);
                        obj.dQJSRQ = FormatUtils.DateToString(query.FieldByName("QJSRQ").AsDateTime);
                        obj.iQYXQTS = query.FieldByName("QYXQTS").AsInteger;
                        obj.iFFPT = query.FieldByName("FFPT").AsInteger;
                        obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
                        obj.iDJR = query.FieldByName("DJR").AsInteger;
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.iZXR = query.FieldByName("ZXR").AsInteger;
                        obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                        obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                        obj.iZZR = query.FieldByName("ZZR").AsInteger;
                        obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
                        obj.dZZSJ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
                        obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                        obj.iQDR = query.FieldByName("QDR").AsInteger;
                        obj.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
                        obj.sQDRMC = query.FieldByName("QDRMC").AsString;
                        obj.iLBBJ = query.FieldByName("LBBJ").AsInteger;
                        obj.iBUY= query.FieldByName("BJ_BUY").AsInteger;
                        obj.iWX_MDID = query.FieldByName("MDID").AsInteger;
                        obj.sMDMC = query.FieldByName("MDMC").AsString;
                        obj.sTITLE_MZ = query.FieldByName("TITLE").AsString;
                        obj.sDESCRIBE_MZ = query.FieldByName("DESCRIBE").AsString;
                        obj.sURL_MZ = query.FieldByName("URL").AsString;
                        obj.sIMG_MZ = query.FieldByName("IMG").AsString;


                       BJ = obj.iLBBJ;

                           return obj;



        }
    }
}
