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

namespace BF.CrmProc.KFPT
{
    public class KFPT_CXXX_Srch : BASECRMClass
    {
        public int iDZLX = 0;
        public int iDJLX = 0;
        public string dKSRQ, dJSRQ = string.Empty;
        private class KFPT_DQZXDJ
        {
            public string sSHMC = string.Empty;
            public string sHYKNAME = string.Empty;
            public string sSHBMDM = string.Empty;
            public string sBMMC = string.Empty;
            public int iYXCLBJ = 0;
            public int iJLBH = 0;
            public int iXFLJMJFS = 0;
            public int iCXID = 0;
            public string sCXZT = string.Empty;
            public int iXFLJFQFS = 0;
            public string sYHQMC = string.Empty;
            public int iBJ_FQ = 0;
        }
        private class KFPT_JJQDDJ
        {
            public string sHYKNAME = string.Empty;
            public string dRQ1, dRQ2, dZZRQ = string.Empty;
            public string sSHMC = string.Empty;
            public string sSHBMDM = string.Empty;
            public string sBMMC = string.Empty;
            public int iBJ_JFBS = 0;
            public int iJLBH = 0;
            public int iYXCLBJ = 0;
            public int iXFLJMJFS = 0;
            public int iCXID = 0;
            public string sCXZT = string.Empty;
            public int iXFLJFQFS = 0;
            public string sYHQMC = string.Empty;
            public int iBJ_FQ = 0;
        }
        private class KFPT_JFFLGZ
        {
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public int iCLRC = 0;
            public int iYHQYXTS = 0;
            public string dKSRQ, dSYJSRQ = string.Empty;
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            switch (iDJLX)
            {
                case 0:
                    query.SQL.Text = "select S.SHMC,D.HYKNAME,H.SHBMDM,M.BMMC,H.YXCLBJ,H.JLBH";
                    query.SQL.Add(" from  HYKJFDYD H,HYKDEF D,SHDY S,SHBM M,HYKJFDYD_KLX X");
                    query.SQL.Add("  where  H.JLBH=X.JLBH and X.HYKTYPE=D.HYKTYPE and H.SHDM=S.SHDM and H.SHBMDM=M.BMDM and H.SHDM=M.SHDM  and H.STATUS = " + iDZLX);
                    break;
                case 1:
                    query.SQL.Text = "select JLBH,KSRQ,JSRQ from HYTDJFDYD ";
                    query.SQL.Add("where  STATUS = " + iDZLX);
                    break;
                case 2:
                    query.SQL.Text = "select S.SHMC,D.HYKNAME,H.SHBMDM,M.BMMC, H.YXCLBJ,H.JLBH ";
                    query.SQL.Add(" from  HYKZKDYD H,HYKDEF D,SHDY S,SHBM M,HYKZKDYD_KLX X ");
                    query.SQL.Add(" where  H.JLBH=X.JLBH and X.HYKTYPE=D.HYKTYPE and H.SHDM=S.SHDM and  H.SHBMDM=M.BMDM and  H.SHDM=M.SHDM  and H.STATUS = " + iDZLX);
                    break;
                case 3:
                    query.SQL.Text = "select S.SHMC,H.SHBMDM,M.BMMC, H.YXCLBJ ,H.XFLJMJFS,H.JLBH,H.CXID,C.CXZT";
                    query.SQL.Add(" from  CXMBJZDYD H,SHDY S,SHBM M,CXHDDEF C ");
                    query.SQL.Add(" where  H.SHDM=S.SHDM and  H.SHBMDM=M.BMDM and  H.SHDM=M.SHDM  and H.CXID = C.CXID(+)  and H.SHDM=C.SHDM  and H.STATUS =" + iDZLX);
                    break;
                case 4:
                    query.SQL.Text = "select S.SHMC,Y.YHQMC,H.SHBMDM,M.BMMC, H.YXCLBJ,H.XFLJFQFS,H.JLBH,H.CXID,C.CXZT ";
                    query.SQL.Add(" from  HYKFQDYD H,SHDY S,SHBM M,YHQDEF Y,CXHDDEF C ");
                    query.SQL.Add(" where  H.SHDM=S.SHDM and  H.SHBMDM=M.BMDM and H.SHDM=M.SHDM  and  H.YHQID=Y.YHQID and  H.CXID = C.CXID(+)  and H.SHDM=C.SHDM and H.STATUS =" + iDZLX);
                    break;
                case 5:
                    query.SQL.Text = "select S.SHMC,Y.YHQMC,H.SHBMDM,M.BMMC,H.YXCLBJ ,H.BJ_FQ ,H.JLBH, H.CXID,C.CXZT ";
                    query.SQL.Add("  from  HYKYQDYD H,SHDY S,SHBM M,YHQDEF Y,CXHDDEF C");
                    query.SQL.Add(" where  H.SHDM=S.SHDM and H.SHBMDM=M.BMDM and  H.SHDM=M.SHDM  and  H.YHQID=Y.YHQID and  H.CXID = C.CXID(+)  and H.SHDM=C.SHDM and H.STATUS =" + iDZLX);
                    break;
                case 6:
                    query.SQL.Text = "select H.HYKTYPE,HYKNAME,CLRC,KSRQ,YHQSL as YHQYXTS,YHQJSRQ as SYJSRQ ";
                    query.SQL.Add("  from HYK_JFFLGZ H,HYKDEF D  ");
                    query.SQL.Add("  where H.HYKTYPE=D.HYKTYPE  ");
                    break;

            }
            SetSearchQuery(query, lst, false);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            switch (iDJLX)
            {
                case 0:
                    KFPT_JJQDDJ item_jf = new KFPT_JJQDDJ();
                    item_jf.dRQ1 = FormatUtils.DateToString(query.FieldByName("RQ1").AsDateTime);
                    item_jf.dRQ2 = FormatUtils.DateToString(query.FieldByName("RQ2").AsDateTime);
                    item_jf.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
                    item_jf.sSHMC = query.FieldByName("SHMC").AsString;
                    item_jf.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_jf.sSHBMDM = query.FieldByName("SHBMDM").AsString;
                    item_jf.sBMMC = query.FieldByName("BMMC").AsString;
                    item_jf.iBJ_JFBS = query.FieldByName("BJ_JFBS").AsInteger;
                    item_jf.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item_jf.iYXCLBJ = query.FieldByName("YXCLBJ").AsInteger;
                    return item_jf;
                case 1:
                    KFPT_JJQDDJ item_tdjf = new KFPT_JJQDDJ();
                    item_tdjf.dRQ1 = FormatUtils.DateToString(query.FieldByName("RQ1").AsDateTime);
                    item_tdjf.dRQ2 = FormatUtils.DateToString(query.FieldByName("RQ2").AsDateTime);
                    item_tdjf.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
                    item_tdjf.iJLBH = query.FieldByName("JLBH").AsInteger;
                    return item_tdjf;
                case 2:
                    KFPT_JJQDDJ item_zk = new KFPT_JJQDDJ();
                    item_zk.dRQ1 = FormatUtils.DateToString(query.FieldByName("RQ1").AsDateTime);
                    item_zk.dRQ2 = FormatUtils.DateToString(query.FieldByName("RQ2").AsDateTime);
                    item_zk.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
                    item_zk.sSHMC = query.FieldByName("SHMC").AsString;
                    item_zk.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_zk.sSHBMDM = query.FieldByName("SHBMDM").AsString;
                    item_zk.sBMMC = query.FieldByName("BMMC").AsString;
                    item_zk.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item_zk.iYXCLBJ = query.FieldByName("YXCLBJ").AsInteger;
                    return item_zk;
                case 3:
                case 4:
                case 5:
                    KFPT_JJQDDJ item_mj = new KFPT_JJQDDJ();
                    item_mj.dRQ1 = FormatUtils.DateToString(query.FieldByName("RQ1").AsDateTime);
                    item_mj.dRQ2 = FormatUtils.DateToString(query.FieldByName("RQ2").AsDateTime);
                    item_mj.dZZRQ = FormatUtils.DateToString(query.FieldByName("ZZRQ").AsDateTime);
                    item_mj.sSHMC = query.FieldByName("SHMC").AsString;
                    item_mj.sSHBMDM = query.FieldByName("SHBMDM").AsString;
                    item_mj.sBMMC = query.FieldByName("BMMC").AsString;
                    item_mj.iJLBH = query.FieldByName("JLBH").AsInteger;
                    item_mj.iYXCLBJ = query.FieldByName("YXCLBJ").AsInteger;
                    item_mj.iXFLJMJFS = query.FieldByName("XFLJMJFS").AsInteger;
                    item_mj.iCXID = query.FieldByName("CXID").AsInteger;
                    item_mj.sCXZT = query.FieldByName("CXZT").AsString;
                    return item_mj;
                case 6:
                    KFPT_JFFLGZ item_flgz = new KFPT_JFFLGZ();
                    item_flgz.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_flgz.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_flgz.iCLRC = query.FieldByName("CLRC").AsInteger;
                    item_flgz.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
                    item_flgz.iYHQYXTS = query.FieldByName("YHQYXTS").AsInteger;
                    item_flgz.dSYJSRQ = FormatUtils.DateToString(query.FieldByName("SYJSRQ").AsDateTime);
                    return item_flgz;
                default:
                    return  null;
            }

        }
    }
}
