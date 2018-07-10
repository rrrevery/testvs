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


namespace BF.CrmProc.HYKGL
{
    public class HYKGL_GHFXDW_Proc : DJLR_ZX_CLass
    {
        public int iHYKTYPE = 0;
        public string sHYKNAME = string.Empty;
        public int iXFXDWID = 0;
        public string sFXDWMC = string.Empty;
        public string sCZDD = string.Empty;
        public int iMDID_CZ = 0;
        public string sMDMC = string.Empty;
        public int iBHKS = 0;
        public string sBHKH = string.Empty;

        public List<HYK_GHFXDWITEM> itemTable = new List<HYK_GHFXDWITEM>();
        public class HYK_GHFXDWITEM
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty, sYFXDWMC = string.Empty;
            public int iYFXDWID = 0;
            public int iBJ_CHILD = 0;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public int iMDID = 0;
            public string sMDMC = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_GHFXDW;HYK_GHFXDWITEM;", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_GHFXDW");
            iMDID_CZ = CrmLibProc.GetMDIDByRY(iLoginRYID);
            query.SQL.Text = "insert into HYK_GHFXDW(JLBH,XFXDWID,CZDD,ZY,DJR,DJRMC,DJSJ,MDID_CZ)"; //,HYKTYPE
            query.SQL.Add(" values(:JLBH,:XFXDWID,:CZDD,:ZY,:DJR,:DJRMC,:DJSJ,:MDID_CZ)"); //,:HYKTYPE
            query.ParamByName("JLBH").AsInteger = iJLBH;
            //query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("XFXDWID").AsInteger = iXFXDWID;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("MDID_CZ").AsInteger = iMDID_CZ;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ExecSQL();

            foreach (HYK_GHFXDWITEM one in itemTable)
            {
                query.SQL.Text = "insert into HYK_GHFXDWITEM(JLBH,HYID,HYK_NO,YFXDWID,BJ_CHILD,HYKTYPE,MDID)";
                query.SQL.Add(" values(:JLBH,:HYID,:HYK_NO,:YFXDWID,:BJ_CHILD,:HYKTYPE,:MDID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ParamByName("YFXDWID").AsInteger = one.iYFXDWID;
                query.ParamByName("BJ_CHILD").AsInteger = one.iBJ_CHILD;
                query.ParamByName("HYKTYPE").AsInteger = one.iHYKTYPE;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ExecSQL();
            }

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_GHFXDW", serverTime, "JLBH");
            foreach (HYK_GHFXDWITEM one in itemTable)
            {
                if (query.Active)
                {
                    query.Close();
                }
                query.SQL.Text = "update HYK_HYXX set FXDW=:FXDW where HYID=:HYID";
                query.ParamByName("FXDW").AsInteger = iXFXDWID;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ExecSQL();
            }

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iMDID_CZ", "W.MDID_CZ");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sFXDWDM", "F.FXDWDM");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("sDJRMC", "W.DJRMC");
            query.SQL.Text = "select W.*,F.FXDWMC, M.MDMC,(select count(JLBH) BHKS from HYK_GHFXDWITEM I where i.jlbh=W.JLBH  group by JLBH) BHKS,B.BGDDMC";
            query.SQL.Add(" from HYK_GHFXDW W,FXDWDEF F,MDDY M,HYK_BGDD B  where W.XFXDWID=F.FXDWID  and M.MDID=W.MDID_CZ and W.CZDD = B.BGDDDM");
            if (sBHKH != "" && sBHKH != null)
            {
                query.SQL.Add("  and W.Jlbh in (select JLBH from HYK_GHFXDWITEM where HYKNO='" + sBHKH + "' )");
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,F.FXDWMC,D.HYKNAME,M.MDMC from HYK_GHFXDWITEM I,FXDWDEF F,HYKDEF D,MDDY M";
                query.SQL.Add(" where I.YFXDWID=F.FXDWID(+) and I.HYKTYPE=D.HYKTYPE(+) and I.MDID=M.MDID(+)");
                query.SQL.Add(" and I.JLBH=:JLBH");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYK_GHFXDWITEM item = new HYK_GHFXDWITEM();
                    ((HYKGL_GHFXDW_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item.iYFXDWID = query.FieldByName("YFXDWID").AsInteger;
                    item.sYFXDWMC = query.FieldByName("FXDWMC").AsString;
                    item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    query.Next();
                }              
            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_GHFXDW_Proc obj = new HYKGL_GHFXDW_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.iXFXDWID = query.FieldByName("XFXDWID").AsInteger;
            obj.sFXDWMC = query.FieldByName("FXDWMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iBHKS = query.FieldByName("BHKS").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            return obj;
        }
    }
}
