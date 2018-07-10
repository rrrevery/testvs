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
    public class HYXF_JFFLGZ : JFFLGZ
    {
        public int iHBFS = 0;
        public int iCLRC = 0;
        public int iCLFS = 0;
        public int iXSWS = 0;
        public string dKSRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iYHQSL = 0;
        public int iYHQDW = 0;
        public int iBJ_KCDYJF = 0;
        public double fZXDW = 0;
        public int iBJ_XZ = 0;
        public int iZZR = 0;
        public string sZZRMC = string.Empty;
        public string dZZRQ = string.Empty;


        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFFLGZ;HYK_JFFLGZITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                #region  CheckReapt
                // {
                // int JLBH = CheckReaptRule(iHYKTYPE, iMDID);
                //  if (JLBH != 0)
                //  {
                //    iJLBH = JLBH;
                //    DeleteDataQuery(out msg, query,serverTime);

                //  }
                // } 
                #endregion
                iJLBH = SeqGenerator.GetSeq("HYK_JFFLGZ");
            query.SQL.Text = "insert into HYK_JFFLGZ(JLBH,SHDM,HYKTYPE,HBFS,CLRC,CLFS,XSWS,KSRQ,JSRQ,YHQID,YHQJSRQ,YHQSL,YHQDW,STATUS,DJR,DJRMC,DJSJ,BJ_KCDYJF,ZXDW,MDID,BJ_XZ,GZMC)";
            query.SQL.Add(" values(:JLBH,:SHDM,:HYKTYPE,:HBFS,:CLRC,:CLFS,:XSWS,:KSRQ,:JSRQ,:YHQID,:YHQJSRQ,:YHQSL,:YHQDW,:STATUS,:DJR,:DJRMC,:DJSJ,:BJ_KCDYJF,:ZXDW,:MDID,:BJ_XZ,:GZMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("SHDM").AsString = sSHDM;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("HBFS").AsInteger = iHBFS;
            query.ParamByName("CLRC").AsInteger = iCLRC;
            query.ParamByName("CLFS").AsInteger = iCLFS;
            query.ParamByName("XSWS").AsInteger = iXSWS;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("YHQSL").AsInteger = iYHQSL;
            query.ParamByName("YHQJSRQ").AsDateTime = FormatUtils.ParseDateString(dYHQJSRQ);
            query.ParamByName("YHQDW").AsInteger = iYHQDW;
            query.ParamByName("STATUS").AsInteger = iSTATUS;
            query.ParamByName("BJ_KCDYJF").AsInteger = iBJ_KCDYJF;
            query.ParamByName("ZXDW").AsFloat = fZXDW;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("BJ_XZ").AsInteger = iBJ_XZ;
            query.ParamByName("GZMC").AsString = sGZMC;
            query.ExecSQL();
            foreach (JFFLGZItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFFLGZITEM(JLBH,XH,JFXX,JFSX,FLBL)";
                query.SQL.Add(" values(:JLBH,:XH,:JFXX,:JFSX,:FLBL)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = one.iXH;
                query.ParamByName("JFXX").AsFloat = one.fJFXX;
                query.ParamByName("JFSX").AsFloat = one.fJFSX;
                query.ParamByName("FLBL").AsFloat = one.fFLBL;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTableStatus(query, "HYK_JFFLGZ", serverTime, 1);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "W.CZDD");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iZZR", "W.ZZR");
            CondDict.Add("dZZRQ", "W.ZZRQ");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("sSHDM", "W.SHDM");
            CondDict.Add("iYHQID", "W.YHQID");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
            CondDict.Add("dYHQJSRQ", "W.YHQJSRQ");
            CondDict.Add("iSTATUS", "W.STATUS");
            CondDict.Add("iBJ_CX", "W.BJ_CX");
            CondDict.Add("iMDID", "W.MDID");

            query.SQL.Text = "select W.*,D.HYKNAME,Y.YHQMC,M.MDMC,S.SHMC";
            query.SQL.Add("     from HYK_JFFLGZ W,HYKDEF D,YHQDEF Y,MDDY M,SHDY S");
            query.SQL.Add("    where W.HYKTYPE=D.HYKTYPE and W.YHQID=Y.YHQID");
            query.SQL.Add("   and W.MDID=M.MDID and W.SHDM = S.SHDM");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*from HYK_JFFLGZITEM I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    JFFLGZItem obj = new JFFLGZItem();
                    ((HYXF_JFFLGZ)lst[0]).itemTable.Add(obj);
                    obj.iXH = query.FieldByName("XH").AsInteger;
                    obj.fJFXX = query.FieldByName("JFXX").AsFloat;
                    obj.fJFSX = query.FieldByName("JFSX").AsFloat;
                    obj.fFLBL = query.FieldByName("FLBL").AsFloat;
                    query.Next();
                }
                query.Close();

            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFFLGZ obj = new HYXF_JFFLGZ();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sSHDM = query.FieldByName("SHDM").AsString;
            obj.sSHMC = query.FieldByName("SHMC").AsString;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iHBFS = query.FieldByName("HBFS").AsInteger;
            obj.iCLRC = query.FieldByName("CLRC").AsInteger;
            obj.iCLFS = query.FieldByName("CLFS").AsInteger;
            obj.iXSWS = query.FieldByName("XSWS").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iYHQID = query.FieldByName("YHQID").AsInteger;
            obj.dYHQJSRQ = FormatUtils.DateToString(query.FieldByName("YHQJSRQ").AsDateTime);
            obj.iYHQSL = query.FieldByName("YHQSL").AsInteger;
            obj.iYHQDW = query.FieldByName("YHQDW").AsInteger;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iBJ_KCDYJF = query.FieldByName("BJ_KCDYJF").AsInteger;
            obj.fZXDW = query.FieldByName("ZXDW").AsFloat;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sYHQMC = query.FieldByName("YHQMC").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iBJ_XZ = query.FieldByName("BJ_XZ").AsInteger;
            obj.sGZMC = query.FieldByName("GZMC").AsString;
            return obj;
        }
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            base.StopDataQuery(out msg, query, serverTime);
            query.Close();
            query.SQL.Text = "update HYK_JFFLGZ set ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=:ZZRQ,STATUS=2 ";
            query.SQL.AddLine("where JLBH=:JLBH ");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("ZZR").AsInteger = iLoginRYID;
            query.ParamByName("ZZRMC").AsString = sLoginRYMC;
            query.ParamByName("ZZRQ").AsDateTime = serverTime;
            query.ExecSQL();
        }

        /// <summary>
        /// 老系统判断该同一门店，卡类型不能存在多规则
        /// </summary>
        /// <param name="HYKTYPE"></param>
        /// <param name="MDID"></param>
        /// <returns></returns>
        public int CheckReaptRule(int HYKTYPE, int MDID)
        {
            int JLBH = 0;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = " select JLBH from HYK_JFFLGZ G where G.HYKTYPE=:HYKTYPE and G.MDID=:MDID and STATUS=1";
                    query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
                    query.ParamByName("MDID").AsInteger = iMDID;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        JLBH = query.FieldByName("JLBH").AsInteger;
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }
            return JLBH;
        }
    }
}
