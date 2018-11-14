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
    public class HYXF_QMF_Proc : DZHYK_DJLR_CLass
    {
        public double fYWCLJF = 0;
        public double fYNLJJF = 0;
        public double fYYHQJE = 0;
        public double fYCZKYE = 0;
        public double fTHJF = 0;
        public double fYMJJF = 0;
        public double fYMNJF = 0;
        public double fYFJE_J = 0;
        public double fYFJE_N = 0;
        public double fYFJE = 0;
        public double fSMJJF = 0;
        public double fSMNJF = 0;
        public double fSFJE_J = 0;
        public double fSFJE_N = 0;
        public double fSFJE = 0;
        public double fXJ = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public string dJZRQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string sSKYDM = string.Empty;
        public string sSKYMC = string.Empty;
        public string sSKTNO = string.Empty;


        public List<HYXF_QMFItem> itemTable = new List<HYXF_QMFItem>();

        public class HYXF_QMFItem
        {
            public int iHYID = 0;
            public double fYYHQJE = 0;
            public double fYCZKYE = 0;
            public double fKYHQJE = 0;
            public double fKCZKYE = 0;
            public int iYHQJLBH = 0;

            public string sHYK_NO = string.Empty, sHY_NAME = string.Empty;
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "CRMTHMJFJL;CRMTHMJFJLITEM", "JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("CRMTHMJFJL");
            query.SQL.Text = "insert into CRMTHMJFJL(JLBH,CZDD,HYID,YWCLJF,YNLJJF,YYHQJE,YCZKYE,THJF,YMJJF,YMNJF,YFJE_J,YFJE_N,SMJJF,SMNJF,SFJE_J,SFJE_N,XJ,DJR,DJRMC,DJSJ,BZ,MDID,SKTNO,JZRQ,SKYDM,DJLX,SKYMC)";
            query.SQL.Add(" values(:JLBH,:CZDD,:HYID,:YWCLJF,:YNLJJF,:YYHQJE,:YCZKYE,:THJF,:YMJJF,:YMNJF,:YFJE_J,:YFJE_N,:SMJJF,:SMNJF,:SFJE_J,:SFJE_N,:XJ,:DJR,:DJRMC,:DJSJ,:BZ,:MDID,:SKTNO,:JZRQ,:SKYDM,:DJLX,:SKYMC)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("YWCLJF").AsFloat = fYWCLJF;
            query.ParamByName("YNLJJF").AsFloat = fYNLJJF;
            query.ParamByName("YYHQJE").AsFloat = fYYHQJE;
            query.ParamByName("YCZKYE").AsFloat = fYCZKYE;
            query.ParamByName("THJF").AsFloat = fTHJF;
            query.ParamByName("YMJJF").AsFloat = fYMJJF;
            query.ParamByName("YMNJF").AsFloat = fYMNJF;
            query.ParamByName("YFJE_J").AsFloat = fYFJE_J;
            query.ParamByName("YFJE_N").AsFloat = fYFJE_N;
            query.ParamByName("SMJJF").AsFloat = fSMJJF;
            query.ParamByName("SMNJF").AsFloat = fSMNJF;

            query.ParamByName("SFJE_J").AsFloat = fSFJE_J;
            query.ParamByName("SFJE_N").AsFloat = fSFJE_N;
            query.ParamByName("XJ").AsFloat = fXJ;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BZ").AsString = sZY;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("SKTNO").AsString = sSKTNO;
            query.ParamByName("JZRQ").AsDateTime = FormatUtils.ParseDateString(dJZRQ);
            query.ParamByName("SKYDM").AsString = sSKYDM;
            query.ParamByName("DJLX").AsInteger = iDJLX;
            query.ParamByName("SKYMC").AsString = sSKYMC;
            query.ExecSQL();

            //暂时只实现了单卡操作
            foreach (HYXF_QMFItem one in itemTable)
            {
                query.SQL.Text = "insert into CRMTHMJFJLITEM(JLBH,HYID,YYHQJE,YCZKYE,KYHQJE,KCZKYE,YHQJLBH)";
                query.SQL.Add(" values(:JLBH,:HYID,:YYHQJE,:YCZKYE,:KYHQJE,:KCZKYE,:YHQJLBH)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("YYHQJE").AsFloat = one.fYYHQJE;
                query.ParamByName("YCZKYE").AsFloat = one.fYCZKYE;
                query.ParamByName("KYHQJE").AsFloat = one.fKYHQJE;
                query.ParamByName("KCZKYE").AsFloat = one.fKCZKYE;
                query.ParamByName("YHQJLBH").AsInteger = one.iYHQJLBH;
                query.ExecSQL();
            }
        }
        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "CRMTHMJFJL", serverTime);//执行人和执行时间录入

            try
            {
                query.SQL.Text = "select * from  CRMTHMJFJL where JLBH=" + iJLBH;
                while (!query.Eof)
                {
                    this.iJLBH = query.FieldByName("JLBH").AsInteger;
                    iHYID = query.FieldByName("HYID").AsInteger;
                    fTHJF = query.FieldByName("THJF").AsFloat;
                    fYMJJF = query.FieldByName("YMJJF").AsFloat;
                    fYFJE_J = query.FieldByName("YFJE_J").AsFloat;
                    fSMJJF = query.FieldByName("SMJJF").AsFloat;
                    fSFJE_J = query.FieldByName("SFJE_J").AsFloat;
                    fXJ = query.FieldByName("XJ").AsFloat;
                    iMDID = query.FieldByName("MDID").AsInteger;
                    query.Next();
                }
                CrmLibProc.UpdateJFZH(out msg, query, 0, iHYID, BASECRMDefine.HYK_JFBDCLLX_QMJF, iJLBH, iMDID, fSMJJF, iLoginRYID, sLoginRYMC, "用钱买分", fSMJJF, fSMJJF, fSMJJF);

            }
            catch (Exception e)
            {
                if (e is MyDbException)
                    throw e;
                else
                    msg = e.Message;
                throw new MyDbException(e.Message, query.SqlText);
            }



            //对此单据执行时，影响的账户的信息修改

            //CrmLibProc.UpdateJFZH();
            //CrmLibProc.UpdateJEZH():

            //暂时只实现了单卡操作
            foreach (HYXF_QMFItem one in itemTable)
            {
                //query.SQL.Text = "update HYK_HYXX set STATUS=:STATUS where HYID=:HYID";
                //query.ParamByName("STATUS").AsInteger = iNEW_STATUS;
                //query.ParamByName("HYID").AsInteger = one.iHYID;
                //query.ExecSQL();
            }
        }

        //查询
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sBGDDDM", "B.BGDDDM");
            CondDict.Add("iHYKTYPE", "H.HYKTYPE");
            CondDict.Add("sHYKNO", "H.HYK_NO");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            if (this.sHYKNO != "")
            {
                query.SQL.Text = "select H.HYID,H.HYKTYPE,K.HYKNAME,J.YE,F.WCLJF,F.BNLJJF,Y.JE from HYK_HYXX H,HYKDEF K,HYK_JEZH J,HYK_JFZH F,HYK_YHQZH Y WHERE H.HYKTYPE=K.HYKTYPE ";
                query.SQL.Add(" AND H.HYID=J.HYID(+) ");
                query.SQL.Add(" AND H.HYID=F.HYID(+) ");
                query.SQL.Add(" AND H.HYID=Y.HYID(+) ");
                query.SQL.Add(" AND STATUS>=0 ");
                query.SQL.Add(" AND H.HYK_NO=" + "'" + this.sHYKNO + "'");
                query.SQL.Add(" AND rownum<=1");
                SetSearchQuery(query, lst,false);
            }
            else
            {
                query.SQL.Text = "select W.*,H.HYK_NO,H.HY_NAME,H.YXQ,B.BGDDMC,D.HYKNAME,H.HYKTYPE,M.MDMC";
                query.SQL.Add(" from CRMTHMJFJL W,HYK_HYXX H,HYK_BGDD B,HYKDEF D, MDDY M ");
                query.SQL.Add(" where W.HYID=H.HYID and W.CZDD=B.BGDDDM and H.HYKTYPE=D.HYKTYPE and W.MDID = M.MDID ");
                SetSearchQuery(query, lst);
                if (lst.Count == 1)
                {
                    query.SQL.Text = "select I.*,H.HYK_NO,H.HY_NAME";
                    query.SQL.Add(" from CRMTHMJFJLITEM I,HYK_HYXX H where I.JLBH=" + iJLBH);
                    query.SQL.Add(" and I.HYID=H.HYID");
                    query.Open();
                    while (!query.Eof)
                    {
                        HYXF_QMFItem obj = new HYXF_QMFItem();
                        ((HYXF_QMF_Proc)lst[0]).itemTable.Add(obj);
                        obj.iHYID = query.FieldByName("HYID").AsInteger;
                        obj.fYYHQJE = query.FieldByName("YYHQJE").AsFloat;
                        obj.fYCZKYE = query.FieldByName("YCZKYE").AsFloat;
                        obj.fKYHQJE = query.FieldByName("KYHQJE").AsFloat;
                        obj.fKCZKYE = query.FieldByName("KCZKYE").AsFloat;
                        obj.iYHQJLBH = query.FieldByName("YHQJLBH").AsInteger;
                        obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        query.Next();
                    }
                }
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_QMF_Proc obj = new HYXF_QMF_Proc();
            if (sHYKNO != "")
            {
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.fYWCLJF = query.FieldByName("WCLJF").AsFloat;
                obj.fYNLJJF = query.FieldByName("BNLJJF").AsFloat;
                obj.fYYHQJE = query.FieldByName("JE").AsFloat;
                obj.fYCZKYE = query.FieldByName("YE").AsFloat;
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            }
            else {
                obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.sBGDDDM = query.FieldByName("CZDD").AsString;
                obj.iHYID = query.FieldByName("HYID").AsInteger;
                obj.fYWCLJF = query.FieldByName("YWCLJF").AsFloat;
                obj.fYNLJJF = query.FieldByName("YNLJJF").AsFloat;
                obj.fYYHQJE = query.FieldByName("YYHQJE").AsFloat;
                obj.fYCZKYE = query.FieldByName("YCZKYE").AsFloat;
                obj.fTHJF = query.FieldByName("THJF").AsFloat;
                obj.fYMJJF = query.FieldByName("YMJJF").AsFloat;
                obj.fYFJE_J = query.FieldByName("YFJE_J").AsFloat;
                obj.fSMJJF = query.FieldByName("SMJJF").AsFloat;
                obj.fSFJE_J = query.FieldByName("SFJE_J").AsFloat;
                obj.fXJ = query.FieldByName("XJ").AsFloat;
                obj.iDJR = query.FieldByName("DJR").AsInteger;
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                obj.iZXR = query.FieldByName("ZXR").AsInteger;
                obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
                obj.sZY = query.FieldByName("BZ").AsString;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sSKTNO = query.FieldByName("SKTNO").AsString;
                obj.dJZRQ = FormatUtils.DateToString(query.FieldByName("JZRQ").AsDateTime);
                obj.sSKYDM = query.FieldByName("SKYDM").AsString;
                obj.iDJLX = query.FieldByName("DJLX").AsInteger;
                obj.sSKYMC = query.FieldByName("SKYMC").AsString;
                obj.iMDID = query.FieldByName("MDID").AsInteger;
                obj.sMDMC = query.FieldByName("MDMC").AsString;
                obj.sSKTNO = query.FieldByName("SKTNO").AsString;
                obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
                obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            }
            return obj;
        }
    }
}
