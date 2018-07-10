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
    public class LPGL_LPJHD : DJLR_ZX_CLass
    {
        public int iGHSID;
        public string sGHSMC;
        public double fZSL, fZJE, fJJJE;
        public int iDHDBH = 0, iBJ_WDD = 0;
        public List<LPGL_LPJHDItem> itemTable = new List<LPGL_LPJHDItem>();
        public class LPGL_LPJHDItem : LPXX
        {
            public double fJHSL, fDHSL;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFFHLPJHD;HYK_JFFHLPJHDMX;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_JFFHLPJHD");
            query.SQL.Text = "insert into HYK_JFFHLPJHD(JLBH,GHSID,BGDDDM,DJR,DJRMC,DJSJ,ZSL,ZJE,JJJE,BZ,DJLX,DHDBH,BJ_WDD)";
            query.SQL.Add(" values(:JLBH,:GHSID,:BGDDDM,:DJR,:DJRMC,:DJSJ,:ZSL,:ZJE,:JJJE,:ZY,:DJLX,:DHDBH,:BJ_WDD)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("GHSID").AsInteger = iGHSID;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            if (iDJLX == 0)
                query.ParamByName("ZSL").AsFloat = fZSL;
            if (iDJLX == 1)
                query.ParamByName("ZSL").AsFloat = -fZSL;

            query.ParamByName("ZJE").AsFloat = fZJE;
            query.ParamByName("JJJE").AsFloat = fJJJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("DHDBH").AsInteger = iDHDBH;
            query.ParamByName("BJ_WDD").AsInteger = iBJ_WDD;

            query.ExecSQL();
            foreach (LPGL_LPJHDItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_JFFHLPJHDMX(JLBH,LPID,JHSL,LPDJ,LPJJ,DHSL,LPJF,KCSL)";
                query.SQL.Add(" values(:JLBH,:LPID,:JHSL,:LPDJ,:LPJJ,:DHSL,:LPJF,:KCSL)");
                query.ParamByName("KCSL").AsFloat = one.fKCSL;
                query.ParamByName("LPJF").AsFloat = one.fLPJF;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                if (iDJLX == 0)
                    query.ParamByName("JHSL").AsFloat = one.fJHSL;
                if (iDJLX == 1)
                    query.ParamByName("JHSL").AsFloat = -one.fJHSL;
                query.ParamByName("LPDJ").AsFloat = one.fLPDJ;
                query.ParamByName("LPJJ").AsFloat = one.fLPJJ;
                query.ParamByName("DHSL").AsFloat = -one.fDHSL;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFFHLPJHD", serverTime);
            foreach (LPGL_LPJHDItem one in itemTable)
            {
                if (iDJLX == 0)
                {
                    CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_JH, iJLBH, sBGDDDM, one.fJHSL, iLoginRYID.ToString(), sLoginRYMC);
                }
                if (iDJLX == 1)
                {
                    query.SQL.Clear();
                    query.SQL.Text = "select * from HYK_JFFHLPKC where BGDDDM='" + sBGDDDM + "' and  LPID=" + one.iLPID + "";
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        if (query.FieldByName("KCSL").AsInteger >= Math.Abs(one.fJHSL))
                        {
                            CrmLibProc.UpdateLPKC(out msg, query, one.iLPID, (int)BASECRMDefine.CLLX_LPBD.LPBD_TH, iJLBH, sBGDDDM, -Math.Abs(one.fJHSL), iLoginRYID.ToString(), sLoginRYMC);

                        }
                        else
                        {
                            msg = "库存不足无法退货！";
                            throw new Exception("库存不足无法退货！");
                        }
                    }
                    else
                    {
                        msg = "库存不足无法退货！";
                        throw new Exception("库存不足无法退货！");
                    }
                }
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sGHSMC", "G.GHSMC");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("fZSL", "W.ZSL");
            CondDict.Add("fZJE", "W.ZJE");
            CondDict.Add("fJJJE", "W.JJJE");
            CondDict.Add("sZY", "W.BZ");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iDJLX", "W.DJLX");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sMDMC", "M.MDMC");
            CondDict.Add("iGHSID", "W.GHSID");
            query.SQL.Text = "select W.*,B.BGDDMC,G.GHSMC,G.GHSID";
            query.SQL.Add("     from HYK_JFFHLPJHD W, HYK_BGDD B,GHSDEF G");
            query.SQL.Add("    where W.BGDDDM=B.BGDDDM and W.GHSID=G.GHSID(+)");
            SetSearchQuery(query, lst);

            if(lst.Count==1)
            {
                query.SQL.Text = "select I.*,F.LPFLMC,F.LPFLID from HYK_JFFHLPJHDMX I,HYK_JFFHLPXX L,LPFLDEF F";
                query.SQL.Add(" where I.LPID=L.LPID and L.LPFLID=F.LPFLID(+) and I.JLBH=" + iJLBH);
                query.SQL.Add(" order by I.LPID");
                query.Open();
                while (!query.Eof)
                {
                    LPGL_LPJHDItem item = new LPGL_LPJHDItem();
                    ((LPGL_LPJHD)lst[0]).itemTable.Add(item);
                    item.fJHSL = query.FieldByName("JHSL").AsFloat;
                    if (item.fJHSL < 0)
                    {
                        item.fJHSL = -item.fJHSL;
                    }
                    item.fDHSL = query.FieldByName("DHSL").AsFloat;
                    item.fKCSL = query.FieldByName("KCSL").AsFloat;
                    item.fLPJJ = query.FieldByName("LPJJ").AsFloat;
                    item.iLPFLID = query.FieldByName("LPFLID").AsInteger;
                    item.sLPFLMC = query.FieldByName("LPFLMC").AsString;
                    CrmLibProc.SetLPXX(item, query.FieldByName("LPID").AsInteger);
                    query.Next();
                }
            }
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            LPGL_LPJHD item = new LPGL_LPJHD();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iDJLX = query.FieldByName("DJLX").AsInteger;
            item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sZY = query.FieldByName("BZ").AsString;
            item.iGHSID = query.FieldByName("GHSID").AsInteger;
            item.sGHSMC = query.FieldByName("GHSMC").AsString;
            if (item.iDJLX == 1)
            {
                item.fZSL = -query.FieldByName("ZSL").AsFloat;
            }
            else
            {
                item.fZSL = query.FieldByName("ZSL").AsFloat;
            }
            item.fZJE = query.FieldByName("ZJE").AsFloat;
            item.fJJJE = query.FieldByName("JJJE").AsFloat;
            item.iDHDBH = query.FieldByName("DHDBH").AsInteger;
            item.iBJ_WDD = query.FieldByName("BJ_WDD").AsInteger;

            return item;
        }
    }
}
