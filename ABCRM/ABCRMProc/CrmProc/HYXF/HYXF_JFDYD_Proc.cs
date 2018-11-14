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
    public class HYXF_JFDYD : CXD
    {
        public int iBJ_JFBS = 0;
        public List<JFD_GZSD> itemGZSD = new List<JFD_GZSD>();
        public List<HYKDEF> itemKLX = new List<HYKDEF>();

        public class JFD_GZSD : CXD_GZSD
        {
            public double fFZ = 0;
            public int iBS = 0;
            public double fFTBL = 0;
            public int iJFFBGZ = 0;
        }
        HYXF_JFDYD()
        {
            sMainTable = "HYKJFDYD";
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYKJFDYD;HYKJFDYD_FD;HYKJFDYD_GZSD;HYKJFDYD_GZITEM;HYKJFDYD_KLX;", "JLBH", iJLBH);
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
                iJLBH = SeqGenerator.GetSeq("HYKJFDYD");
            }

            query.SQL.Text = "insert into HYKJFDYD(JLBH,SHDM,SHBMID,SHBMDM,YXCLBJ,RQ1,RQ2,DJR,DJRMC,DJSJ,STATUS,BJ_JFBS)";
            query.SQL.Add(" values(:JLBH,:SHDM,:SHBMID,:SHBMDM,:YXCLBJ,:RQ1,:RQ2,:DJR,:DJRMC,:DJSJ,0,:BJ_JFBS)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("SHBMID").AsInteger = iSHBMID;
            query.ParamByName("SHBMDM").AsString = sSHBMDM;
            query.ParamByName("YXCLBJ").AsInteger = iYXCLBJ;
            query.ParamByName("RQ1").AsDateTime = FormatUtils.ParseDatetimeString(dRQ1);
            query.ParamByName("RQ2").AsDateTime = FormatUtils.ParseDatetimeString(dRQ2);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BJ_JFBS").AsInteger = iBJ_JFBS;
            query.ExecSQL();
            foreach (CXD_FD one in itemFD)
            {
                query.SQL.Text = "insert into HYKJFDYD_FD(JLBH,INX,KSRQ,JSRQ,SD)";
                query.SQL.Add(" values(:JLBH,:INX,:KSRQ,:JSRQ,:SD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(one.dKSRQ);
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDatetimeString(one.dJSRQ);
                byte[] bytes = FormatUtils.StrToPeriodBytes(one.sSD.TrimEnd(';'));
                query.ParamByName("SD").SetParamValue(DbType.Binary, bytes);
                //FormatUtils.BytesToHexStr(bytes)
                //query.ParamByName("SD").AsString = one.sSD;
                query.ExecSQL();
            }
            foreach (JFD_GZSD one in itemGZSD)
            {
                query.SQL.Text = "insert into HYKJFDYD_GZSD(JLBH,INX,GZBH,CLFS_BM,CLFS_SPFL,CLFS_SPSB,CLFS_HT,BJ_CJ,CLFS_SP,ZKL_BEGIN,ZKL_END,FZ,BS,FTBL,JFFBGZ,CLFS_HYZ,CLFS_HYKLX,CLFS_FXDW)";
                query.SQL.Add(" values(:JLBH,:INX,:GZBH,:CLFS_BM,:CLFS_SPFL,:CLFS_SPSB,:CLFS_HT,:BJ_CJ,:CLFS_SP,0,0,:FZ,:BS,:FTBL,:JFFBGZ,:CLFS_HYZ,:CLFS_HYKLX,:CLFS_FXDW)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("GZBH").AsInteger = one.iGZBH;
                query.ParamByName("CLFS_BM").AsInteger = one.iCLFS_BM;
                query.ParamByName("CLFS_SPFL").AsInteger = one.iCLFS_SPFL;
                query.ParamByName("CLFS_SPSB").AsInteger = one.iCLFS_SPSB;
                query.ParamByName("CLFS_HT").AsInteger = one.iCLFS_HT;
                query.ParamByName("BJ_CJ").AsInteger = one.iBJ_CJ;
                query.ParamByName("CLFS_SP").AsInteger = one.iCLFS_SP;
                query.ParamByName("FZ").AsFloat = one.fFZ;
                query.ParamByName("BS").AsInteger = one.iBS;
                query.ParamByName("FTBL").AsFloat = one.fFTBL;
                query.ParamByName("JFFBGZ").AsInteger = one.iJFFBGZ;
                query.ParamByName("CLFS_HYZ").AsInteger = one.iCLFS_HYZ;
                query.ParamByName("CLFS_HYKLX").AsInteger = one.iCLFS_HYKLX;
                query.ParamByName("CLFS_FXDW").AsInteger = one.iCLFS_FXDW;
                query.ExecSQL();
            }
            foreach (CXD_GZITEM one in itemGZITEM)
            {
                query.SQL.Text = "insert into HYKJFDYD_GZITEM(JLBH,INX,GZBH,SJLX,SJNR,XH)";
                query.SQL.Add(" values(:JLBH,:INX,:GZBH,:SJLX,:SJNR,:XH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("GZBH").AsInteger = one.iGZBH;
                query.ParamByName("SJLX").AsInteger = one.iSJLX;
                query.ParamByName("SJNR").AsInteger = one.iSJNR;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ExecSQL();
            }
            foreach (HYKDEF one in itemKLX)
            {
                query.SQL.Text = "insert into HYKJFDYD_KLX(JLBH,HYKTYPE)";
                query.SQL.Add(" values(:JLBH,:HYKTYPE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            CondDict.Add("iJLBH", "A.JLBH");
            CondDict.Add("sSHDM", "A.SHDM");
            CondDict.Add("sBMDM", "B.BMDM");
            CondDict.Add("dRQ1", "A.RQ1");
            CondDict.Add("dRQ2", "A.RQ2");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("iZXR", "A.SHR");
            CondDict.Add("dZXRQ", "A.SHSJ");
            CondDict.Add("sZXRMC", "A.SHRMC");
            CondDict.Add("iQDR", "A.QDR");
            CondDict.Add("dQDSJ", "A.QDSJ");
            CondDict.Add("iZZR", "A.ZZR");
            CondDict.Add("dZZRQ", "A.ZZSJ");
            CondDict.Add("iSTATUS", "A.STATUS");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select A.*,B.BMMC,S.SHMC from HYKJFDYD A,SHBM B,SHDY S where A.SHBMID=B.SHBMID and B.SHDM=S.SHDM";
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.HYKTYPE,F.HYKNAME from HYKJFDYD L,HYKJFDYD_KLX I,HYKDEF F where L.JLBH=I.JLBH and I.HYKTYPE=F.HYKTYPE and L.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKDEF item = new HYKDEF();
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    ((HYXF_JFDYD)lst[0]).itemKLX.Add(item);
                    query.Next();
                }
                query.SQL.Text = "select * FROM HYKJFDYD_FD where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    CXD_FD item_fd = new CXD_FD();
                    item_fd.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    item_fd.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                    item_fd.iINX = query.FieldByName("INX").AsInteger;
                    //byte[] bydata = new byte[1024 * 1024];
                    //long strIndex = query.FieldByName("SD").GetBytes(0, bydata, 0, 1024 * 1024);
                    //while (strIndex > 0)
                    //{
                    //    byte[] byteArray = System.Text.Encoding.Default.GetBytes(item_fd.sSD);
                    //    item_fd.sSD += System.Text.Encoding.Default.GetString(bydata);
                    //    strIndex = query.FieldByName("SD").GetBytes(strIndex, bydata, 0, 1024 * 1024);
                    //}
                    byte[] bytes = (byte[])query.FieldByName("SD").Value;
                    item_fd.sSD = FormatUtils.PeriodBytesToStr(bytes).TrimEnd(';');
                    ((HYXF_JFDYD)lst[0]).itemFD.Add(item_fd);
                    query.Next();
                }
                query.SQL.Text = "select * from HYKJFDYD_GZSD where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    JFD_GZSD item_gzsd = new JFD_GZSD();
                    item_gzsd.iINX = query.FieldByName("INX").AsInteger;
                    item_gzsd.iGZBH = query.FieldByName("GZBH").AsInteger;
                    item_gzsd.iCLFS_BM = query.FieldByName("CLFS_BM").AsInteger;
                    item_gzsd.iCLFS_SPFL = query.FieldByName("CLFS_SPFL").AsInteger;
                    item_gzsd.iCLFS_SPSB = query.FieldByName("CLFS_SPSB").AsInteger;
                    item_gzsd.iCLFS_HT = query.FieldByName("CLFS_HT").AsInteger;
                    item_gzsd.iBJ_CJ = query.FieldByName("BJ_CJ").AsInteger;
                    item_gzsd.iCLFS_SP = query.FieldByName("CLFS_SP").AsInteger;
                    item_gzsd.fFZ = query.FieldByName("FZ").AsFloat;
                    item_gzsd.iBS = query.FieldByName("BS").AsInteger;
                    item_gzsd.fFTBL = query.FieldByName("FTBL").AsFloat;
                    item_gzsd.iJFFBGZ = query.FieldByName("JFFBGZ").AsInteger;
                    item_gzsd.iCLFS_HYZ = query.FieldByName("CLFS_HYZ").AsInteger;
                    item_gzsd.iCLFS_HYKLX = query.FieldByName("CLFS_HYKLX").AsInteger;
                    item_gzsd.iCLFS_FXDW = query.FieldByName("CLFS_FXDW").AsInteger;
                    item_gzsd.sCLDX = GetCLDX(query);
                    item_gzsd.iXH = ((HYXF_JFDYD)lst[0]).itemGZSD.Count(item => item.iINX == item_gzsd.iINX) + 1;
                    ((HYXF_JFDYD)lst[0]).itemGZSD.Add(item_gzsd);
                    query.Next();
                }
                query.SQL.Text = "select * from HYKJFDYD_GZITEM where JLBH=" + iJLBH;
                //query.SQL.Text = " select I.*,B.BMMC LXMC from  HYKJFDYD_GZITEM I,SHBM B  where I.SJNR=B.SHBMID and I.JLBH=" + iJLBH + " and I.SJLX=1";
                //query.SQL.Add(" union ");
                //query.SQL.Add(" select I.*,B.GSHMC LXMC from  HYKJFDYD_GZITEM I,SHHT B  where I.SJNR=B.SHHTID and I.JLBH=" + iJLBH + " and I.SJLX=2       ");
                //query.SQL.Add(" union                                                                                                                 ");
                //query.SQL.Add(" select I.*,B.SPFLMC LXMC from  HYKJFDYD_GZITEM I,SHSPFL B  where I.SJNR=B.SHSPFLID and I.JLBH=" + iJLBH + " and I.SJLX=3  ");
                //query.SQL.Add(" union                                                                                                                 ");
                //query.SQL.Add(" select I.*,B.SBMC LXMC from  HYKJFDYD_GZITEM I,SHSPSB B  where I.SJNR=B.SHSBID and I.JLBH=" + iJLBH + " and I.SJLX=4      ");
                //query.SQL.Add(" union                                                                                                                 ");
                //query.SQL.Add(" select I.*,B.SPMC LXMC from  HYKJFDYD_GZITEM I,SHSPXX B  where I.SJNR=B.SHSPID and I.JLBH=" + iJLBH + " and I.SJLX=6      ");
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
                    ((HYXF_JFDYD)lst[0]).itemGZITEM.Add(item_cxd);
                    query.Next();

                }
                query.Close();
            }

            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFDYD obj = new HYXF_JFDYD();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.iSHBMID = query.FieldByName("SHBMID").AsInteger;
            obj.sSHBMDM = query.FieldByName("SHBMDM").AsString;
            obj.sBMMC = query.FieldByName("BMMC").AsString;
            obj.iYXCLBJ = query.FieldByName("YXCLBJ").AsInteger;
            obj.dRQ1 = FormatUtils.DateToString(query.FieldByName("RQ1").AsDateTime);
            obj.dRQ2 = FormatUtils.DateToString(query.FieldByName("RQ2").AsDateTime);
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
            obj.iBJ_JFBS = query.FieldByName("BJ_JFBS").AsInteger;
            return obj;
        }
    }
}
