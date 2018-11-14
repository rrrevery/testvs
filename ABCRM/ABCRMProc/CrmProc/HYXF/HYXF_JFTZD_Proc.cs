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
    public class HYXF_JFTZD_Proc: DZHYK_DJLR_CLass
    {
        public int iMDID = 0;
        public string sMDMC = string.Empty, sSKTNO = string.Empty;
        public int iXSJYBH = 0;
        public string dXSRQ = string.Empty;
        public double fTZJF = 0;
        public List<HYXF_JFTZDItem> itemTable = new List<HYXF_JFTZDItem>();

        public class HYXF_JFTZDItem
        {
            public int iSHSPID = 0;
            public string sSPDM = string.Empty, sSPMC = string.Empty;
            public string sBMDM = string.Empty, sBMMC = string.Empty;
            public double fJF = 0, fTZJF = 0;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFTZJL;HYK_JFTZJLITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFTZJL");
            query.SQL.Text = "insert into HYK_JFTZJL(JLBH,MDID,SKTNO,XSJYBH,HYID,XSRQ,TZJF,ZY,HYKNO,DJSJ,DJR,DJRMC)";
            query.SQL.Add(" values(:JLBH,:MDID,:SKTNO,:XSJYBH,:HYID,:XSRQ,:TZJF,:ZY,:HYKNO,:DJSJ,:DJR,:DJRMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("SKTNO").AsString = sSKTNO;
            query.ParamByName("XSJYBH").AsInteger = iXSJYBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("XSRQ").AsDateTime = FormatUtils.ParseDateString(dXSRQ);
            query.ParamByName("TZJF").AsFloat = fTZJF;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("HYKNO").AsString = sHYKNO;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();
            foreach (HYXF_JFTZDItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFTZJLITEM(JLBH,SHSPID,SPDM,XSBM,JF_OLD,TZJF)";
                query.SQL.Add(" values(:JLBH,:SHSPID,:SPDM,:XSBM,:JF_OLD,:TZJF)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("SHSPID").AsInteger = one.iSHSPID;
                query.ParamByName("SPDM").AsString = one.sSPDM;
                query.ParamByName("XSBM").AsString = one.sBMDM;
                query.ParamByName("JF_OLD").AsFloat = one.fJF;
                query.ParamByName("TZJF").AsFloat = one.fTZJF;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFTZJL", serverTime);
            CrmLibProc.UpdateJFZH(out msg, query, 0, iHYID, BASECRMDefine.HYK_JFBDCLLX_JFTZD, iJLBH, iMDID, fTZJF, iLoginRYID, sLoginRYMC, "积分调整", fTZJF, fTZJF, fTZJF);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sMDMC", "M.MDMC");
            CondDict.Add("sSKTNO", "W.SKTNO");
            CondDict.Add("sHYKNO", "W.HYKNO");
            CondDict.Add("iXSJYBH", "W.XSJYBH");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.XSRQ");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,H.HY_NAME,M.MDMC";
            query.SQL.Add("     from HYK_JFTZJL W,HYK_HYXX H,MDDY M");
            query.SQL.Add("    where W.HYID=H.HYID and W.MDID=M.MDID");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,X.SPMC,B.BMMC from HYK_JFTZJLITEM I,SHSPXX X,SHBM B where I.JLBH="+iJLBH;
                query.SQL.Add(" and I.SHSPID=X.SHSPID and I.XSBM=B.BMDM and X.SHDM=B.SHDM");
                query.Open();
                while (!query.Eof)
                {
                    HYXF_JFTZDItem item = new HYXF_JFTZDItem();
                    ((HYXF_JFTZD_Proc)lst[0]).itemTable.Add(item);
                    item.iSHSPID = query.FieldByName("SHSPID").AsInteger;
                    item.sSPDM = query.FieldByName("SPDM").AsString;
                    item.sSPMC = query.FieldByName("SPMC").AsString;
                    item.sBMDM = query.FieldByName("XSBM").AsString;
                    item.sBMMC = query.FieldByName("BMMC").AsString;
                    item.fJF = query.FieldByName("JF_OLD").AsFloat;
                    item.fTZJF = query.FieldByName("TZJF").AsFloat;
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_JFTZD_Proc obj = new HYXF_JFTZD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sSKTNO = query.FieldByName("SKTNO").AsString;
            obj.iXSJYBH = query.FieldByName("XSJYBH").AsInteger;
            obj.dXSRQ = FormatUtils.DateToString(query.FieldByName("XSRQ").AsDateTime);
            obj.fTZJF = query.FieldByName("TZJF").AsFloat;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYKNO = query.FieldByName("HYKNO").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZY = query.FieldByName("ZY").AsString;
            return obj;
        }
    }
}
