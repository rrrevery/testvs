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


namespace BF.CrmProc.LPGL
{
    public class LPGL_LPLQD : DJLR_BRBCDD_CLass
    {
        public double fZSL, fZJE;
        public string sLQRMC = string.Empty;
        public int iLQR;
        public List<LPGL_LPLQDItem> itemTable = new List<LPGL_LPLQDItem>();

        public class LPGL_LPLQDItem : LPXX
        {
            public double fLQSL;
            public double fKCSL;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_LPLQJL;HYK_LPLQJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_LPLQJL");
            query.Close();
            query.SQL.Text = "insert into HYK_LPLQJL(JLBH,BGDDDM_BC,BGDDDM_BR,DJR,DJRMC,DJSJ,ZSL,ZJE,BZ,STATUS,LQR,LQRMC)";
            query.SQL.Add(" values(:JLBH,:BGDDDM_BC,:BGDDDM_BR,:DJR,:DJRMC,:DJSJ,:ZSL,:ZJE,:BZ,0,:LQR,:LQRMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("BGDDDM_BC").AsString = sBGDDDM_BC;
            query.ParamByName("BGDDDM_BR").AsString = sBGDDDM_BR;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("ZSL").AsFloat = fZSL;
            query.ParamByName("ZJE").AsFloat = fZJE;
            query.ParamByName("BZ").AsString = sZY;
            query.ParamByName("LQR").AsInteger = iLQR;
            query.ParamByName("LQRMC").AsString = sLQRMC;
            query.ExecSQL();
            foreach (LPGL_LPLQDItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_LPLQJLITEM(JLBH,LPID,LQSL,LPDJ)";
                query.SQL.Add(" values(:JLBH,:LPID,:LQSL,:LPDJ)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("LQSL").AsFloat = one.fLQSL;
                query.ParamByName("LPDJ").AsFloat = one.fLPDJ;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_LPLQJL", serverTime);
            foreach (LPGL_LPLQDItem one in itemTable)
            {
                query.SQL.Clear();
                query.SQL.Text = "select * from HYK_JFFHLPKC where BGDDDM='" + sBGDDDM_BC + "' and  LPID=" + one.iLPID + "";
                query.Open();
                if (!query.IsEmpty)
                {
                    if (query.FieldByName("KCSL").AsFloat >= one.fLQSL)
                    {

                        CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_BR, iJLBH, sBGDDDM_BR, one.fLQSL, iLoginRYID.ToString(), sLoginRYMC);
                        CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_BC, iJLBH, sBGDDDM_BC, -one.fLQSL, iLoginRYID.ToString(), sLoginRYMC);
                    }
                    else
                    {
                        msg = "库存不足无法操作！";
                        throw new Exception("库存不足无法操作！");
                    }
                }
                else
                {
                    msg = "库存不足无法操作！";
                    throw new Exception("库存不足无法操作！");
                }
            }

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDMC_BC", "BC.BGDDMC");
            CondDict.Add("sBGDDMC_BR", "BR.BGDDMC");
            CondDict.Add("sBGDDDM_BC", "W.BGDDDM_BC");
            CondDict.Add("sBGDDDM_BR", "W.BGDDDM_BR");
            CondDict.Add("fZSL", "W.ZSL");
            CondDict.Add("fZJE", "W.ZJE");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sMDMC", "M.MDMC");
            query.SQL.Text = "select W.*,BC.BGDDMC BGDDMC_BC,BR.BGDDMC BGDDMC_BR";
            query.SQL.Add("     from HYK_LPLQJL W,HYK_BGDD BC,HYK_BGDD BR");
            query.SQL.Add("    where W.BGDDDM_BR=BR.BGDDDM and W.BGDDDM_BC=BC.BGDDDM");
            SetSearchQuery(query, lst);

            if(lst.Count==1)
            {
                LPGL_LPLQD lplq = (LPGL_LPLQD)lst[0];

                query.SQL.Text = "select I.*, K.KCSL from HYK_LPLQJLITEM I,HYK_JFFHLPKC K   where I.LPID=K.LPID";
                query.SQL.Add("   and I.JLBH=" + iJLBH);
                query.SQL.Add("   and K.BGDDDM='" + lplq.sBGDDDM_BC + "'"); 
                query.SQL.Add("   ORDER BY I.LPID");
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPLQDItem item = new LPGL_LPLQDItem();
                    ((LPGL_LPLQD)lst[0]).itemTable.Add(item);
                    item.fLQSL = query.FieldByName("LQSL").AsFloat;
                    item.fKCSL = query.FieldByName("KCSL").AsFloat;
                    CrmLibProc.SetLPXX(item, query.FieldByName("LPID").AsInteger);
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPLQD obj = new LPGL_LPLQD();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sBGDDDM_BR = query.FieldByName("BGDDDM_BR").AsString;
            obj.sBGDDMC_BR = query.FieldByName("BGDDMC_BR").AsString;
            obj.sBGDDDM_BC = query.FieldByName("BGDDDM_BC").AsString;
            obj.sBGDDMC_BC = query.FieldByName("BGDDMC_BC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZY = query.FieldByName("BZ").AsString;
            obj.fZSL = query.FieldByName("ZSL").AsFloat;
            obj.fZJE = query.FieldByName("ZJE").AsFloat;
            obj.iLQR = query.FieldByName("LQR").AsInteger;
            obj.sLQRMC = query.FieldByName("LQRMC").AsString;
            return obj;
        }
    }
}
