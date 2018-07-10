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
    public class HYXF_ZKDYD : CXD
    {
        public int iBJ_TD = 0;
        public List<ZKD_GZSD> itemGZSD = new List<ZKD_GZSD>();
        public List<HYKDEF> itemKLX = new List<HYKDEF>();

        public class ZKD_GZSD : CXD_GZSD
        {
            public double fZKL = 1;
        }
        HYXF_ZKDYD()
        {
            sMainTable = "HYKZKDYD";
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYKZKDYD;HYKZKDYD_FD;HYKZKDYD_GZSD;HYKZKDYD_GZITEM;HYKZKDYD_KLX;", "JLBH", iJLBH);
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
                iJLBH = SeqGenerator.GetSeq("HYKZKDYD");
            }

            query.SQL.Text = "insert into HYKZKDYD(JLBH,SHDM,SHBMID,SHBMDM,YXCLBJ,RQ1,RQ2,DJR,DJRMC,DJSJ,STATUS,BJ_TD)";
            query.SQL.Add(" values(:JLBH,:SHDM,:SHBMID,:SHBMDM,:YXCLBJ,:RQ1,:RQ2,:DJR,:DJRMC,:DJSJ,0,:BJ_TD)");
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
            query.ParamByName("BJ_TD").AsInteger = iBJ_TD;
            query.ExecSQL();
            foreach (CXD_FD one in itemFD)
            {
                query.SQL.Text = "insert into HYKZKDYD_FD(JLBH,INX,KSRQ,JSRQ,SD)";
                query.SQL.Add(" values(:JLBH,:INX,:KSRQ,:JSRQ,:SD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(one.dKSRQ);
                query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDatetimeString(one.dJSRQ);
                byte[] bytes = FormatUtils.StrToPeriodBytes(one.sSD.TrimEnd(';'));
                query.ParamByName("SD").SetParamValue(DbType.Binary, bytes);
                query.ExecSQL();
            }
            foreach (ZKD_GZSD one in itemGZSD)
            {
                query.SQL.Text = "insert into HYKZKDYD_GZSD(JLBH,INX,GZBH,CLFS_BM,CLFS_SPFL,CLFS_SPSB,CLFS_HT,BJ_CJ,CLFS_SP,ZKL_BEGIN,ZKL_END,ZKL,CLFS_HYZ,CLFS_HYKLX,CLFS_FXDW)";
                query.SQL.Add(" values(:JLBH,:INX,:GZBH,:CLFS_BM,:CLFS_SPFL,:CLFS_SPSB,:CLFS_HT,:BJ_CJ,:CLFS_SP,0,0,:ZKL,:CLFS_HYZ,:CLFS_HYKLX,:CLFS_FXDW)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("INX").AsInteger = one.iINX;
                query.ParamByName("GZBH").AsInteger = one.iGZBH;
                query.ParamByName("CLFS_BM").AsInteger = one.iCLFS_BM;
                query.ParamByName("CLFS_SPFL").AsInteger = one.iCLFS_SPFL;
                query.ParamByName("CLFS_SPSB").AsInteger = one.iCLFS_SPSB;
                query.ParamByName("CLFS_HT").AsInteger = one.iCLFS_HT;
                query.ParamByName("BJ_CJ").AsInteger = one.iBJ_CJ;
                query.ParamByName("CLFS_SP").AsInteger = one.iCLFS_SP;
                query.ParamByName("ZKL").AsFloat = one.fZKL;
                query.ParamByName("CLFS_HYZ").AsInteger = one.iCLFS_HYZ;
                query.ParamByName("CLFS_HYKLX").AsInteger = one.iCLFS_HYKLX;
                query.ParamByName("CLFS_FXDW").AsInteger = one.iCLFS_FXDW;
                query.ExecSQL();
            }
            foreach (CXD_GZITEM one in itemGZITEM)
            {
                query.SQL.Text = "insert into HYKZKDYD_GZITEM(JLBH,INX,GZBH,SJLX,SJNR,XH)";
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
                query.SQL.Text = "insert into HYKZKDYD_KLX(JLBH,HYKTYPE)";
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
            CondDict.Add("iSTATUS", "A.STATUS");

            List<Object> lst = new List<Object>();
            query.SQL.Text = "select A.*,B.BMMC,S.SHMC from HYKZKDYD A,SHBM B,SHDY S where A.SHBMID=B.SHBMID and A.SHDM=S.SHDM";
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.HYKTYPE,F.HYKNAME from HYKZKDYD L,HYKZKDYD_KLX I,HYKDEF F where L.JLBH=I.JLBH and I.HYKTYPE=F.HYKTYPE and L.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKDEF item = new HYKDEF();
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    ((HYXF_ZKDYD)lst[0]).itemKLX.Add(item);
                    query.Next();
                }
                query.SQL.Text = "select * from HYKZKDYD_FD  where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    CXD_FD item_fd = new CXD_FD();
                    item_fd.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
                    item_fd.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                    item_fd.iINX = query.FieldByName("INX").AsInteger;
                    byte[] bytes = (byte[])query.FieldByName("SD").Value;
                    item_fd.sSD = FormatUtils.PeriodBytesToStr(bytes).TrimEnd(';');
                    ((HYXF_ZKDYD)lst[0]).itemFD.Add(item_fd);
                    query.Next();
                }
                query.SQL.Text = "select * from HYKZKDYD_GZSD where JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    ZKD_GZSD item_gzsd = new ZKD_GZSD();
                    item_gzsd.iINX = query.FieldByName("INX").AsInteger;
                    item_gzsd.iGZBH = query.FieldByName("GZBH").AsInteger;
                    item_gzsd.iCLFS_BM = query.FieldByName("CLFS_BM").AsInteger;
                    item_gzsd.iCLFS_SPFL = query.FieldByName("CLFS_SPFL").AsInteger;
                    item_gzsd.iCLFS_SPSB = query.FieldByName("CLFS_SPSB").AsInteger;
                    item_gzsd.iCLFS_HT = query.FieldByName("CLFS_HT").AsInteger;
                    item_gzsd.iBJ_CJ = query.FieldByName("BJ_CJ").AsInteger;
                    item_gzsd.iCLFS_SP = query.FieldByName("CLFS_SP").AsInteger;
                    item_gzsd.fZKL = query.FieldByName("ZKL").AsFloat;
                    item_gzsd.iCLFS_HYZ = query.FieldByName("CLFS_HYZ").AsInteger;
                    item_gzsd.iCLFS_HYKLX = query.FieldByName("CLFS_HYKLX").AsInteger;
                    item_gzsd.iCLFS_FXDW = query.FieldByName("CLFS_FXDW").AsInteger;
                    item_gzsd.sCLDX = GetCLDX(query);
                    item_gzsd.iXH = ((HYXF_ZKDYD)lst[0]).itemGZSD.Count(item => item.iINX == item_gzsd.iINX) + 1;
                    ((HYXF_ZKDYD)lst[0]).itemGZSD.Add(item_gzsd);
                    query.Next();
                }
                query.SQL.Text = "select * from HYKZKDYD_GZITEM where JLBH=" + iJLBH;
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
                    ((HYXF_ZKDYD)lst[0]).itemGZITEM.Add(item_cxd);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_ZKDYD obj = new HYXF_ZKDYD();
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
            obj.iBJ_TD = query.FieldByName("BJ_TD").AsInteger;
            return obj;
        }
    }
}
