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
    public class GTPT_JFDHBLDY : DJLR_ZXQDZZ_CLass
    {
       
        public int A = 0;

        public int iLX = 0;
        public int iFWFS = 0;
        public string sHYKNAME = string.Empty;
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public double fDHJF = 0;
        public double cDHJE = 0;
        public double fQDJF = 0;
        public double fJFSX = 0;
        public int iHYKTYPE = 0;
        //public List<WX_XFLPFFDYDITEM_MD> itemTable2 = new List<WX_XFLPFFDYDITEM_MD>();

        public int iMDID_SY = 0;
        public string sMDMC_SY = string.Empty;

        public int iMDID = 0;
        public string sMDMC = string.Empty;

        //public class WX_XFLPFFDYDITEM_MD
        //{
        //    public int iMDID = 0;
        //    public string sMDMC = string.Empty;
        //}

        public new string[] asFieldNames = {
                                       "iJLBH;W.JLBH", 
                                       //"iGZID;W.GZID", 
                                       //"sGZMC;D.GZMC",
                                       "dKSRQ;W.KSRQ", 
                                       "dJSRQ;W.JSRQ",
                                       "iSTATUS;W.STATUS",
                                       "iDJR;W.DJR",
                                       "sDJRMC;W.DJRMC",
                                       "dDJSJ;W.DJRQ",                                       
                                       "iQDR;W.QDR",
                                       "sQDRMC;W.QDRMC",
                                       "dQDRQ;W.QDSJ",
                                       "iZZR;W.ZZR",
                                       "sZZRMC;W.ZZRMC",
                                       "dZZRQ;W.ZZSJ",                                                                 
                                       };





        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_HYK_JFDHBL", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)  //, string DbSystemName = "ORACLE"
        {
            msg = string.Empty;
            string sMDDM = string.Empty;
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_HYK_JFDHBL");

            if (iFWFS == 2)
            {
                query.SQL.Text = "select MDDM FROM MDDY WHERE MDID=:MDID";
                query.ParamByName("MDID").AsInteger = iMDID;
                query.Open();
                sMDDM = query.FieldByName("MDDM").AsString;
            }

            query.SQL.Text = "insert into WX_HYK_JFDHBL(JLBH,KSRQ,JSRQ,HYKTYPE,FWFS,SHDM,STATUS,LX,MDID,DHJF,DHJE,QDJF,JFSX,DJSJ,DJR,DJRMC)";
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:HYKTYPE,:FWFS,:SHDM,0,:LX,:MDID,:DHJF,:DHJE,:QDJF,:JFSX,:DJSJ,:DJR,:DJRMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("FWFS").AsInteger = iFWFS;
            if (iFWFS == 0)
            {
                query.ParamByName("SHDM").AsString = "-1";
            }
            else if (iFWFS == 2)
            {
                query.ParamByName("SHDM").AsString = sMDDM;
            }
            query.ParamByName("LX").AsInteger = iLX;
            if (iLX == 0)
            {
                query.ParamByName("MDID").AsInteger = 0;
            }
            else if (iLX == 1)
            {
                query.ParamByName("MDID").AsInteger = iMDID_SY;
            }
            query.ParamByName("DHJF").AsFloat = fDHJF;
            query.ParamByName("DHJE").AsFloat = cDHJE;
            query.ParamByName("QDJF").AsFloat = fQDJF;
            query.ParamByName("JFSX").AsFloat = fJFSX;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ExecSQL();
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.* ,Y.MDID AS MDID_N,F.HYKNAME,A.MDMC AS MDMC_SY,Y.MDMC";
            query.SQL.Add("from WX_HYK_JFDHBL W,MDDY Y,HYKDEF F,MDDY A");
            query.SQL.Add(" where Y.MDDM(+)=W.SHDM AND W.HYKTYPE=F.HYKTYPE AND A.MDID(+)=W.MDID");
            SetSearchQuery(query, lst);
            return lst;      
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_HYK_JFDHBL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);

        }
        public override void StartDataQuery(out string msg, CyQuery query, DateTime serverTime)  //, string DbSystemName = "ORACLE"
        {
            msg = string.Empty;
            string DbSystemName = "ORACLE";
            query.SQL.Text = "update WX_HYK_JFDHBL set ZZR=:ZZR,ZZSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=3,ZZRMC=:ZZRMC where  STATUS=2 and JLBH=:JLBH ";
            query.ParamByName("JLBH").AsInteger = iJLBH;

            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ExecSQL();

            query.SQL.Text = "update WX_HYK_JFDHBL set QDR=:QDR,QDRMC=:QDRMC,QDSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=2 where JLBH=" + iJLBH;
            query.ParamByName("QDR").AsInteger = iLoginRYID;
            query.ParamByName("QDRMC").AsString = sLoginRYMC;
            query.ExecSQL();
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "WX_HYK_JFDHBL", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZSJ", 3);
        }
        public override object SetSearchData(CyQuery query)
        {
            GTPT_JFDHBLDY obj = new GTPT_JFDHBLDY();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iMDID = query.FieldByName("MDID_N").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.dQDSJ = FormatUtils.DateToString(query.FieldByName("QDSJ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.dZZSJ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            //obj.sGZMC = query.FieldByName("GZMC").AsString;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            //obj.sMDMC = query.FieldByName("MDMC").AsString;

            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iMDID_SY = query.FieldByName("MDID").AsInteger;
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.iFWFS = query.FieldByName("FWFS").AsInteger;
            obj.fDHJF = query.FieldByName("DHJF").AsFloat;
            obj.cDHJE = query.FieldByName("DHJE").AsFloat;
            obj.fQDJF = query.FieldByName("DHJE").AsFloat;
            obj.fJFSX = query.FieldByName("JFSX").AsFloat;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sMDMC_SY = query.FieldByName("MDMC_SY").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }


        
    }
}
