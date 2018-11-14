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


namespace BF.CrmProc.HYXF
{
    public class HYXF_YQDYD : YHQCXD
    {
        public int iLX = 0;
        public int iBJ_FQ = 0;

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYKYQDYD;HYKYQDYD_FD;HYKYQDYD_GZSD;HYKYQDYD_GZITEM;HYKYQDYD_SP;", "JLBH", iJLBH);
        }
        HYXF_YQDYD()
        {
            sMainTable = "HYKYQDYD";
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYKYQDYD");
            }

            query.SQL.Text = "insert into HYKYQDYD(JLBH,SHDM,SHBMID,SHBMDM,YHQID,CXID,YXCLBJ,RQ1,RQ2,DJR,DJRMC,DJSJ,STATUS,BJ_FQ)";
            query.SQL.Add(" values(:JLBH,:SHDM,:SHBMID,:SHBMDM,:YHQID,:CXID,:YXCLBJ,:RQ1,:RQ2,:DJR,:DJRMC,:DJSJ,0,:BJ_FQ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("SHBMID").AsInteger = iSHBMID;
            query.ParamByName("SHBMDM").AsString = sSHBMDM;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("YXCLBJ").AsInteger = iYXCLBJ;
            query.ParamByName("RQ1").AsDateTime = FormatUtils.ParseDatetimeString(dRQ1);
            query.ParamByName("RQ2").AsDateTime = FormatUtils.ParseDatetimeString(dRQ2);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BJ_FQ").AsInteger = iBJ_FQ;
            // query.ParamByName("BJ_JFBS").AsInteger = iBJ_JFBS;
            query.ExecSQL();
            foreach (CXD_FD one in itemFD)
            {
                query.SQL.Text = "insert into HYKYQDYD_FD(JLBH,INX,SD)";
                query.SQL.Add(" values(:JLBH,:INX,:SD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                //query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(one.dKSRQ);
                //query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(one.dJSRQ);
                byte[] bytes = FormatUtils.StrToPeriodBytes(one.sSD.TrimEnd(';'));
                query.ParamByName("SD").SetParamValue(DbType.Binary, bytes);
                //FormatUtils.BytesToHexStr(bytes)
                //query.ParamByName("SD").AsString = one.sSD;
                query.ExecSQL();
            }
            foreach (YHQCXD_GZSD one in itemGZSD)
            {
                query.SQL.Text = "insert into HYKYQDYD_GZSD(JLBH,INX,GZBH,CLFS_BM,CLFS_SPFL,CLFS_SPSB,CLFS_HT,BJ_CJ,CLFS_SP,CLFS_HYZ,YQGZID)";
                query.SQL.Add(" values(:JLBH,:INX,:GZBH,:CLFS_BM,:CLFS_SPFL,:CLFS_SPSB,:CLFS_HT,:BJ_CJ,:CLFS_SP,:CLFS_HYZ,:YQGZID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("GZBH").AsInteger = one.iGZBH;
                query.ParamByName("CLFS_BM").AsInteger = one.iCLFS_BM;
                query.ParamByName("CLFS_SPFL").AsInteger = one.iCLFS_SPFL;
                query.ParamByName("CLFS_SPSB").AsInteger = one.iCLFS_SPSB;
                query.ParamByName("CLFS_HT").AsInteger = one.iCLFS_HT;
                query.ParamByName("BJ_CJ").AsInteger = one.iBJ_CJ;
                query.ParamByName("CLFS_SP").AsInteger = one.iCLFS_SP;
                query.ParamByName("CLFS_HYZ").AsInteger = one.iCLFS_HYZ;
                query.ParamByName("YQGZID").AsInteger = one.iGZID;
                query.ExecSQL();
            }
            foreach (CXD_GZITEM one in itemGZITEM)
            {
                query.SQL.Text = "insert into HYKYQDYD_GZITEM(JLBH,INX,GZBH,SJLX,SJNR,XH)";
                query.SQL.Add(" values(:JLBH,:INX,:GZBH,:SJLX,:SJNR,:XH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("GZBH").AsInteger = one.iGZBH;
                query.ParamByName("SJLX").AsInteger = one.iSJLX;
                query.ParamByName("SJNR").AsInteger = one.iSJNR;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ExecSQL();
            }
            //foreach (HYKDEF one in itemKLX)
            //{
            //    query.SQL.Text = "insert into HYKYQDYD_KLX(JLBH,HYKTYPE)";
            //    query.SQL.Add(" values(:JLBH,:HYKTYPE)");
            //    query.ParamByName("JLBH").AsInteger = iJLBH;
            //    query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
            //    query.ExecSQL();
            //}
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sSHDM", "A.SHDM");
            CondDict.Add("sBMDM", "B.BMDM");
            CondDict.Add("iCXID", "A.CXID");
            CondDict.Add("iYHQID", "A.YHQID");
            CondDict.Add("sYHQMC", "Y.YHQMC");
            CondDict.Add("dRQ1", "A.RQ1");
            CondDict.Add("dRQ2", "A.RQ2");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("iZXR", "A.SHR");
            CondDict.Add("dZXRQ", "A.SHSJ");
            CondDict.Add("sZXRMC", "A.SHRMC");
            CondDict.Add("iQDR", "A.QDR");
            CondDict.Add("dQDSJ", "A.QDSJ");
            CondDict.Add("iZZR", "A.ZZR");
            CondDict.Add("dZZRQ", "A.ZZSJ");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select A.*,B.BMMC,S.SHMC,Y.YHQMC,C.CXZT from HYKYQDYD A,SHBM B,SHDY S,YHQDEF Y,CXHDDEF C";
            query.SQL.Add(" where A.SHBMID=B.SHBMID and A.SHDM=S.SHDM and A.YHQID=Y.YHQID and A.CXID=C.CXID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * FROM HYKYQDYD_FD  where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    CXD_FD item_fd = new CXD_FD();
                    //item_fd.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    //item_fd.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                    item_fd.iINX = query.FieldByName("INX").AsInteger;
                    byte[] bytes = (byte[])query.FieldByName("SD").Value;
                    item_fd.sSD = FormatUtils.PeriodBytesToStr(bytes).TrimEnd(';');
                    ((HYXF_YQDYD)lst[0]).itemFD.Add(item_fd);
                    query.Next();
                }
                query.SQL.Text = "select G.*,Z.YHQSYGZMC from HYKYQDYD_GZSD G,YHQSYGZ Z where G.YQGZID=Z.YHQSYGZID and JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    YHQCXD_GZSD item_gzsd = new YHQCXD_GZSD();
                    item_gzsd.iINX = query.FieldByName("INX").AsInteger;
                    item_gzsd.iGZBH = query.FieldByName("GZBH").AsInteger;
                    item_gzsd.iCLFS_BM = query.FieldByName("CLFS_BM").AsInteger;
                    item_gzsd.iCLFS_SPFL = query.FieldByName("CLFS_SPFL").AsInteger;
                    item_gzsd.iCLFS_SPSB = query.FieldByName("CLFS_SPSB").AsInteger;
                    item_gzsd.iCLFS_HT = query.FieldByName("CLFS_HT").AsInteger;
                    item_gzsd.iBJ_CJ = query.FieldByName("BJ_CJ").AsInteger;
                    item_gzsd.iCLFS_SP = query.FieldByName("CLFS_SP").AsInteger;
                    item_gzsd.iCLFS_HYZ = query.FieldByName("CLFS_HYZ").AsInteger;
                    item_gzsd.iCLFS_HYKLX = query.FieldByName("CLFS_HYKLX").AsInteger;
                    item_gzsd.iCLFS_FXDW = query.FieldByName("CLFS_FXDW").AsInteger;
                    item_gzsd.iGZID = query.FieldByName("YQGZID").AsInteger;
                    item_gzsd.sGZMC = query.FieldByName("YHQSYGZMC").AsString;
                    item_gzsd.sCLDX = GetCLDX(query);
                    item_gzsd.iXH = ((HYXF_YQDYD)lst[0]).itemGZSD.Count(item => item.iINX == item_gzsd.iINX) + 1;
                    ((HYXF_YQDYD)lst[0]).itemGZSD.Add(item_gzsd);
                    query.Next();
                }
                query.SQL.Text = "select * from HYKYQDYD_GZITEM where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    CXD_GZITEM item_cxd = new CXD_GZITEM();
                    item_cxd.iINX = query.FieldByName("INX").AsInteger;
                    item_cxd.iGZBH = query.FieldByName("GZBH").AsInteger;
                    item_cxd.iSJLX = query.FieldByName("SJLX").AsInteger;
                    item_cxd.iSJNR = query.FieldByName("SJNR").AsInteger;
                    item_cxd.iXH = query.FieldByName("XH").AsInteger;
                    //item_cxd.sLXMC = query.FieldByName("LXMC").AsString;
                    ((HYXF_YQDYD)lst[0]).itemGZITEM.Add(item_cxd);
                    query.Next();

                }
                query.Close();
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_YQDYD obj = new HYXF_YQDYD();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.iSHBMID = query.FieldByName("SHBMID").AsInteger;
            obj.sSHBMDM = query.FieldByName("SHBMDM").AsString;
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.iYXCLBJ = query.FieldByName("YXCLBJ").AsInteger;
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.dRQ1 = FormatUtils.DatetimeToString(query.FieldByName("RQ1").AsDateTime);
            obj.dRQ2 = FormatUtils.DatetimeToString(query.FieldByName("RQ2").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("SHR").AsInteger;
            obj.sZXRMC = query.FieldByName("SHRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("SHSJ").AsDateTime);
            obj.iQDR = query.FieldByName("QDR").AsInteger;
            obj.sQDRMC = query.FieldByName("QDRMC").AsString;
            obj.dQDSJ = FormatUtils.DatetimeToString(query.FieldByName("QDSJ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.iLX = query.FieldByName("LX").AsInteger;
            obj.iBJ_FQ = query.FieldByName("BJ_FQ").AsInteger;
            obj.iCXID = query.FieldByName("CXID").AsInteger;
            obj.sCXZT = query.FieldByName("CXZT").AsString;
            obj.iBJ_FQ = query.FieldByName("BJ_FQ").AsInteger;
            return obj;
        }

    }
}
