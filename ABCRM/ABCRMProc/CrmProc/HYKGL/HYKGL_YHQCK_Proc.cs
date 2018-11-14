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
    public class HYKGL_YHQCK : DZHYKYHQCQK_DJLR_CLass
    {
        public double fCKJE = 0;
        //public int iMDID = -1;
        public string sMDFWMC = "", sMDMC = string.Empty;
        public int iFS_YQMDFW = 0;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (fCKJE <= 0)
            {
                msg = CrmLibStrings.msgWrongCKJE;
                return false;
            }
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZK_YHQ_CKJL", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int tp_mdid = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_YHQ_CKJL");

            query.SQL.Text = "insert into HYK_CZK_YHQ_CKJL(CZJPJ_JLBH,MDFWDM,HYID,YHQID,JSRQ,CXID,YJE,CKJE,ZY,CZDD,HYKNO,DJR,DJRMC,DJSJ,MDID)";
            query.SQL.Add(" values(:JLBH,:MDFWDM,:HYID,:YHQID,:JSRQ,:CXID,:YJE,:CKJE,:ZY,:CZDD,:HYKNO,:DJR,:DJRMC,:DJSJ,:MDID)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            //  query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("HYKNO").AsString = sHYKNO;
            query.ParamByName("YHQID").AsInteger = iYHQID;
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("MDFWDM").AsString = iFS_YQMDFW == 1 ? " " : sMDFWDM;
            query.ParamByName("CXID").AsInteger = iCXID;
            query.ParamByName("YJE").AsFloat = fYYE;
            query.ParamByName("CKJE").AsFloat = fCKJE;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("CZDD").AsString = sBGDDDM;//已取消
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = tp_mdid;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int iMDID = CrmLibProc.MDDMToMDID(query, sMDFWDM);
            ExecTable(query, "HYK_CZK_YHQ_CKJL", serverTime, "CZJPJ_JLBH");
            CrmLibProc.UpdateYHQZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iYHQID, iMDID, iCXID, fCKJE, dJSRQ, "优惠券存款", iFS_YQMDFW == 1 ? " " : sMDFWDM);
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "A.CZJPJ_JLBH");
            CondDict.Add("sBGDDDM", "A.CZDD");
            CondDict.Add("sBGDDMC", "B.BGDDMC");
            CondDict.Add("sHYKNO", "A.HYKNO");
            CondDict.Add("iYHQID", "D.YHQID");
            CondDict.Add("iCXID", "A.CXID");
            CondDict.Add("fCKJE", "A.CKJE");
            CondDict.Add("iDJR", "A.DJR");
            CondDict.Add("dDJSJ", "A.DJSJ");
            CondDict.Add("sDJRMC", "A.DJRMC");
            CondDict.Add("iZXR", "A.ZXRMC");
            CondDict.Add("sZXRMC", "A.ZXRMC");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("iMDID", "A.MDID");
            CondDict.Add("sHY_NAME", "C.HY_NAME");
            CondDict.Add("sYHQMC", "D.YHQMC");
            CondDict.Add("sCXZT", "E.CXZT");
            CondDict.Add("sZY", "A.ZY");

            query.SQL.Text="select A.*,(select HY_NAME FROM HYK_HYXX WHERE HYID=A.HYID) HY_NAME ,D.YHQMC,E.CXZT,B.BGDDMC,M.MDMC MDFWMC,Y.MDMC MDMC,D.FS_YQMDFW";//  ,(select MDMC FROM MDDY WHERE MDID=A.MDFWDM) MDFWMC
            query.SQL.Add(" from HYK_CZK_YHQ_CKJL A,YHQDEF D,CXHDDEF E,HYK_BGDD B,MDDY M,MDDY Y");
            query.SQL.Add(" where A.YHQID=D.YHQID and A.CXID=E.CXID and A.CZDD=B.BGDDDM and A.MDFWDM=M.MDDM and A.MDID=Y.MDID(+) and D.FS_YQMDFW=3");//

            MakeSrchCondition(query, "", false);
            query.SQL.Add(" union");
            query.SQL.Add("  select A.*,(select HY_NAME FROM HYK_HYXX WHERE HYID=A.HYID) HY_NAME ,D.YHQMC,E.CXZT,B.BGDDMC,M.SHMC MDFWMC,Y.MDMC MDMC,D.FS_YQMDFW");//  ,(select MDMC FROM MDDY WHERE MDID=A.MDFWDM) MDFWMC
            query.SQL.Add("  from HYK_CZK_YHQ_CKJL A,YHQDEF D,CXHDDEF E,HYK_BGDD B,SHDY M,MDDY Y");
            query.SQL.Add("  where A.YHQID=D.YHQID and A.CXID=E.CXID and A.CZDD=B.BGDDDM and A.MDFWDM=M.SHDM(+) and A.MDID=Y.MDID(+) and D.FS_YQMDFW in(1,2)");//       
            MakeSrchCondition(query, "", false);
            SetSearchQuery(query, lst,false);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_YHQCK item = new HYKGL_YHQCK();
            item.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            //  item.iDJLX = query.FieldByName("DJLX").AsInteger;
            item.iHYID = query.FieldByName("HYID").AsInteger;
            item.sHYKNO = query.FieldByName("HYKNO").AsString;
            item.sZY = query.FieldByName("ZY").AsString;
            item.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            item.sBGDDDM = query.FieldByName("CZDD").AsString;
            item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            item.iYHQID = query.FieldByName("YHQID").AsInteger;
            item.sYHQMC = query.FieldByName("YHQMC").AsString;
            item.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            item.iCXID = query.FieldByName("CXID").AsInteger;
            item.sCXZT = query.FieldByName("CXZT").AsString;
            item.fCKJE = query.FieldByName("CKJE").AsFloat;
            item.fYYE = query.FieldByName("YJE").AsFloat;

            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            //item.iMDID = query.FieldByName("MDID").AsInteger;
            item.sMDMC = query.FieldByName("MDMC").AsString;
            item.iFS_YQMDFW = query.FieldByName("FS_YQMDFW").AsInteger;
            if (item.iFS_YQMDFW != 1)
            {
                item.sMDFWDM = query.FieldByName("MDFWDM").AsString;
                item.sMDFWMC = query.FieldByName("MDFWMC").AsString;
            }
            else
            {
                item.sMDFWDM = " ";
                item.sMDFWMC = "集团";
            }
            return item;
        }

    }
}
