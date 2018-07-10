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
    public class HYKGL_HYKXF : DJLR_ZX_CLass
    {
        public string sMDMC = string.Empty, sHYKNAME = string.Empty, sHYK_NO = string.Empty;
        public int iHYID = 0, iHYKTYPE = 0, iMDID = 0, iBJ_CHILD = 0;
        public double fKFJE = 0, fSSJE = 0;
        public string sHY_NAME = string.Empty;
        public List<HYKGL_HYKXFSKItem> itemTable = new List<HYKGL_HYKXFSKItem>();
        public List<HYXX_Detail> ckitemTable = new List<HYXX_Detail>();

        public class HYKGL_HYKXFSKItem
        {
            public string sZFFSMC;
            public int iZFFSID = 0;
            public double fJE = 0;
            public string dFKRQ = string.Empty;
            public string sJYBH = string.Empty;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_XKFJL;HYK_XKFJL_SKMX;HYK_XKFJL_CKMX", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_XKFJL");
            query.Close();
            query.SQL.Text = "insert into HYK_XKFJL(JLBH,HYKTYPE,HYID,MDID,KFJE,SSJE,ZY,SSJE,DJSJ,DJR,DJRMC,STATUS)";//BJ_CHILD
            query.SQL.Add(" values(:JLBH,:HYKTYPE,:HYID,:MDID,:KFJE,:KFJE,:ZY,:SSJE,:DJSJ,:DJR,:DJRMC,0)");//BJ_CHILD
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("KFJE").AsFloat = fKFJE;
            query.ParamByName("SSJE").AsFloat = fSSJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            // query.ParamByName("BJ_CHILD").AsInteger = iBJ_CHILD;
            query.ExecSQL();
            foreach (HYKGL_HYKXFSKItem item in itemTable)
            {
                query.SQL.Text = "  Insert into HYK_XKFJL_SKMX(JLBH,ZFFSID,JE,JYBH)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE,:JYBH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = item.iZFFSID;
                query.ParamByName("JE").AsFloat = item.fJE;
                query.ParamByName("JYBH").AsString = item.sJYBH;
                query.ExecSQL();
            }
            foreach (HYXX_Detail item_hy in ckitemTable)
            {
                query.SQL.Text = "  Insert into HYK_XKFJL_CKMX(JLBH,HYID)";
                query.SQL.Add(" values(:JLBH,:HYID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = item_hy.iHYID;
                query.ExecSQL();
            }

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_XKFJL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
            query.SQL.Text = " update HYK_HYXX set STATUS=1,YXQ=add_months(YXQ,12),XKFRQ=add_months(XKFRQ,12) where HYID=:HYID";
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
            foreach (HYXX_Detail item_hy in ckitemTable)
            {
                query.SQL.Text = " update HYK_HYXX set STATUS=1,YXQ=add_months(YXQ,12),XKFRQ=add_months(XKFRQ,12) where HYID=:HYID";
                query.ParamByName("HYID").AsInteger = item_hy.iHYID;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("iHYID", "W.HYID");
            CondDict.Add("sZY", "W.ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iMDID", "W.MDID");
            query.SQL.Text = "   select W.*,H.HY_NAME,M.MDMC,D.HYKNAME,H.HYK_NO from HYK_XKFJL W,HYK_HYXX H,HYKDEF D,MDDY M where W.HYID=H.HYID and W.MDID=M.MDID and H.HYKTYPE=D.HYKTYPE";
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "  select I.*,Z.ZFFSMC,Z.ZFFSDM from HYK_XKFJL_SKMX I,ZFFS Z where I.ZFFSID=Z.ZFFSID and JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_HYKXFSKItem item = new HYKGL_HYKXFSKItem();
                    item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                    item.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                    item.fJE = query.FieldByName("JE").AsFloat;
                    item.sJYBH = query.FieldByName("JYBH").AsString;
                    ((HYKGL_HYKXF)lst[0]).itemTable.Add(item);
                    query.Next();
                }
                query.Close();
                query.SQL.Text = " select I.* ,D.HYKNAME,H.HYID,H.HYK_NO,H.HY_NAME,H.HYKTYPE,H.DJSJ,H.KFJE from HYK_XKFJL_CKMX I,HYK_HYXX H,HYKDEF D where I.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYXX_Detail item_hy = new HYXX_Detail();
                    item_hy.iHYID = query.FieldByName("HYID").AsInteger;
                    item_hy.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    item_hy.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_hy.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    item_hy.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    item_hy.dDJSJ = FormatUtils.DateToString(query.FieldByName("DJSJ").AsDateTime);
                    item_hy.iBJ_CHILD = 1;
                    item_hy.fKFJE = query.FieldByName("KFJE").AsFloat;
                    ((HYKGL_HYKXF)lst[0]).ckitemTable.Add(item_hy);
                    query.Next();

                }
                query.Close();
            }


            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_HYKXF item = new HYKGL_HYKXF();
            item.iJLBH = query.FieldByName("JLBH").AsInteger;
            item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.fSSJE = query.FieldByName("SSJE").AsFloat;
            item.fKFJE = query.FieldByName("KFJE").AsFloat;
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            //   item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            return item;
        }



    }
}
