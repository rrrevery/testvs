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
    public class HYKGL_YHQQK : DZHYKYHQCQK_DJLR_CLass
    {

        public double fQKJE = 0;
        public int iMDID = -1;//操作员门店ID
        public string sMDFWMC = "";
        public string sMDMC = string.Empty;
        public int iFS_YQMDFW = 0;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (fQKJE < 0)
            {
                msg = CrmLibStrings.msgWrongCKJE;
                return false;
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZK_YHQ_QKJL", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_YHQ_QKJL");
            query.SQL.Text = "insert into HYK_CZK_YHQ_QKJL(CZJPJ_JLBH,HYID,YHQID,JSRQ,MDFWDM,CXID,YJE,QKJE,ZY,CZDD,HYKNO,DJR,DJRMC,DJSJ)";//,MDID
            query.SQL.Add(" values(:JLBH,:HYID,:YHQID,:JSRQ,:MDFWDM,:CXID,:YJE,:QKJE,:ZY,:CZDD,:HYKNO,:DJR,:DJRMC,:DJSJ)");//,:MDID
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("MDFWDM").AsString = sMDFWDM;
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("YJE").AsFloat = fYYE;
            query.ParamByName("QKJE").AsFloat = fQKJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("HYKNO").AsString = sHYKNO;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            //query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            //int iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            int iMDID = CrmLibProc.MDDMToMDID(query, sMDFWDM);
            if (iMDID == 0)
            {
                iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            }
            ExecTable(query, "HYK_CZK_YHQ_QKJL", serverTime, "CZJPJ_JLBH");
            CrmLibProc.UpdateYHQZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, iYHQID, iMDID, iCXID, -fQKJE, dJSRQ, "优惠券取款", sMDFWDM);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>(); 
            CondDict.Add("iJLBH", "A.CZJPJ_JLBH");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sHYKNO", "A.HYKNO");
            CondDict.Add("iYHQID", "A.YHQID");
            CondDict.Add("iCXID", "A.CXID");
            CondDict.Add("fQKJE", "A.QKJE");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("iZXR", "A.ZXR");
            CondDict.Add("sZXRMC", "A.ZXRMC");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("iMDID", "A.MDID");
            CondDict.Add("sHY_NAME", "H.HY_NAME");
            CondDict.Add("sYHQMC", "D.YHQMC");
            CondDict.Add("sCXZT", "E.CXZT");
            CondDict.Add("sZY", "A.ZY");
            CondDict.Add("iHYID", "A.HYID");
            CondDict.Add("dJSRQ", "A.JSRQ");
            CondDict.Add("fYYE", "A.YJE");

            if (iJLBH != 0)
            {
                query.SQL.Text = "select Y.FS_YQMDFW from HYK_CZK_YHQ_QKJL W,YHQDEF Y  where W.YHQID=Y.YHQID AND W.CZJPJ_JLBH="+iJLBH;
                query.Open();
                if (!query.IsEmpty)
                {
                    this.iFS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
                    query.Close();

                }
            }
            query.SQL.Clear();
            query.Params.Clear();

            query.SQL.Text = "select A.*,HY_NAME,B.BGDDMC,HYKNAME,D.YHQMC,E.CXZT";
            if (iJLBH != 0)
            {
                switch (iFS_YQMDFW)
                {
                    case 1:
                        query.SQL.Add(",'集团' MDFWMC ");
                        break;
                    case 2:
                        query.SQL.Add(",S.SHMC MDFWMC ");
                        break;
                    case 3:
                        query.SQL.Add(",M.MDMC MDFWMC ");
                        break;
                }
                query.SQL.AddLine("");
            }
            query.SQL.Add(" from HYK_CZK_YHQ_QKJL A,YHQDEF D,CXHDDEF E,HYK_BGDD B,HYK_HYXX H,HYKDEF D");
            if (iJLBH != 0)
            {

                switch (iFS_YQMDFW)
                {
                    case 1:
                        //query.SQL.Add(",'集团' MDFWMC ");
                        break;
                    case 2:
                        query.SQL.Add(",SHDY S ");
                        break;
                    case 3:
                        query.SQL.Add(",MDDY M ");
                        break;
                }
                query.SQL.AddLine("");
            }
            query.SQL.Add("  where A.YHQID=D.YHQID and A.CXID=E.CXID and A.CZDD=B.BGDDDM and A.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE ");
            if (iJLBH != 0)
            {

                if (iJLBH != 0)
                {

                    switch (iFS_YQMDFW)
                    {
                        case 1:
                            //query.SQL.Add(",'集团' MDFWMC ");
                            break;
                        case 2:
                            query.SQL.Add("  and S.SHDM=A.MDFWDM ");
                            break;
                        case 3:
                            query.SQL.Add("  and M.MDDM=A.MDFWDM ");
                            break;
                    }
                    query.SQL.AddLine("");
                }

            }
            SetSearchQuery(query, lst);                   
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQQK item = new HYKGL_YHQQK();
            item.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYKNO = query.FieldByName("HYKNO").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            if (iJLBH != 0)
            {
                item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                item.sMDFWMC = query.FieldByName("MDFWMC").AsString;
            }
            item.iCXID = query.FieldByName("CXID").AsInteger;
            item.sCXZT = query.FieldByName("CXZT").AsString;
            item.fQKJE = query.FieldByName("QKJE").AsFloat;
            item.fYYE = query.FieldByName("YJE").AsFloat;

            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            return item;
        }
    }
}
