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

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_ZTBD_Proc : DJLR_ZX_CLass
    {
        public int iNEW_STATUS = 0;
        public double fGBF = 0;
        public List<HYKGL_ZTBDItem> itemTable = new List<HYKGL_ZTBDItem>();
        public string sStatusName;
        public int iMDID = -1;
        public string sMDMC = string.Empty;
        public string sHYK_NO = string.Empty;
        public class HYKGL_ZTBDItem
        {
            public int iHYID = 0;
            public int iOLD_STATUS = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string sStatusName;
            public int iBJ_CHILD = 0;
        }
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_ZTBDJL;MZK_ZTBDJLITEM;", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_ZTBDJL");
            query.SQL.Text = "insert into MZK_ZTBDJL(JLBH,NEW_STATUS,ZY,GBF,DJSJ,DJR,DJRMC,BGDDDM)";
            query.SQL.Add(" values(:JLBH,:NEW_STATUS,:ZY,:GBF,:DJSJ,:DJR,:DJRMC,:BGDDDM)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("NEW_STATUS").AsInteger = iNEW_STATUS;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("GBF").AsFloat = fGBF;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BGDDDM").AsString = sBGDDDM;
            query.ExecSQL();
            foreach (HYKGL_ZTBDItem one in itemTable)
            {
                query.SQL.Text = "insert into MZK_ZTBDJLITEM(JLBH,HYID,OLD_STATUS,HYKNO,BJ_CHILD)";
                query.SQL.Add(" values(:JLBH,:HYID,:OLD_STATUS,:HYKNO,:BJ_CHILD)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("OLD_STATUS").AsInteger = one.iOLD_STATUS;
                query.ParamByName("HYKNO").AsString = one.sHYK_NO;
                query.ParamByName("BJ_CHILD").AsInteger = one.iBJ_CHILD;
                query.ExecSQL();
            }
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_ZTBDJL", serverTime);
            foreach (HYKGL_ZTBDItem one in itemTable)
            {
                CrmLibProc.UpdateMZKSTATUS(query, one.iHYID, iNEW_STATUS);             
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);

            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("sBGDDDM", "W.BGDDDM");
            CondDict.Add("sZY", "ZY");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,M.MDMC,B.BGDDMC from MZK_ZTBDJL W,HYK_BGDD B,MDDY M ";
            query.SQL.Add(" where W.BGDDDM=B.BGDDDM(+) and B.MDID=M.MDID(+) ");
            if (sHYK_NO != "")
            {
                query.SQL.Add(" and exists(select 1 from MZK_ZTBDJLITEM I where I.JLBH=W.JLBH and  I.HYKNO='" + sHYK_NO + "')");
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.* from MZK_ZTBDJLITEM I where I.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYKGL_ZTBDItem item = new HYKGL_ZTBDItem();
                    ((MZKGL_ZTBD_Proc)lst[0]).itemTable.Add(item);
                    item.iHYID = query.FieldByName("HYID").AsInteger;
                    item.sHYK_NO = query.FieldByName("HYKNO").AsString;
                    item.iOLD_STATUS = query.FieldByName("OLD_STATUS").AsInteger;
                    item.sStatusName = BASECRMDefine.HYXXStatusName[item.iOLD_STATUS + 8];
                    item.iBJ_CHILD = query.FieldByName("BJ_CHILD").AsInteger;
                    query.Next();
                }
            }
            query.Close();
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_ZTBD_Proc obj = new MZKGL_ZTBD_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iNEW_STATUS = query.FieldByName("NEW_STATUS").AsInteger;
            obj.sStatusName = BASECRMDefine.HYXXStatusName[obj.iNEW_STATUS+4];
            obj.sBGDDDM = query.FieldByName("BGDDDM").AsString;
            obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            obj.fGBF = query.FieldByName("GBF").AsFloat;
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
