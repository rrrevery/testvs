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
    public class MZKGL_MZKCQK : DZHYK_DJLR_CLass
    {
        public double fYJE = 0;
        public double fCKJE = 0;
        public double fQKJE = 0;
        public double fFKJE = 0;
        public double fZL = 0;

        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public int iZFFSID = 0;
        public string sZFFSMC = string.Empty;
        public string sZFFSDM = string.Empty;
        public int iCK = 0;
        public int iQK= 0;


        public List<SKFS> ZFFS = new List<SKFS>();
        public List<SKFSQK> ZFFSQK = new List<SKFSQK>();

        public class SKFS
        {
            public int iZFFSID = 0;
            //public string DM = string.Empty;
            //public string NAME = string.Empty;
            //public int TYPE = 0;
            public double fJE = 0;

        }
        public class SKFSQK
        {
            public int iZFFSID = 0;
            //public string DM = string.Empty;
            //public string NAME = string.Empty;
            //public int TYPE = 0;
            public double fJE = 0;

        }
        public bool Get_SKFS(out string msg, out List<MZKGL_CZKCK.SKFS> bill)
        {
            bill = new List<MZKGL_CZKCK.SKFS>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDBMZK");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                throw new MyDbException(e.Message, true);
            }
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = " SELECT * FROM ZFFS where BJ_MJ=1 order by ZFFSID";
                    query.Open();
                    while (!query.Eof)
                    {
                        MZKGL_CZKCK.SKFS tp_list = new MZKGL_CZKCK.SKFS();
                        //tp_list.iID = query.FieldByName("ZFFSID").AsInteger;
                        //tp_list.sDM = query.FieldByName("ZFFSDM").AsString;
                        //tp_list.sNAME = query.FieldByName("ZFFSMC").AsString;
                        //tp_list.iTYPE = query.FieldByName("TYPE").AsInteger;
                        tp_list.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                        tp_list.DM = query.FieldByName("ZFFSDM").AsString;
                        tp_list.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                        tp_list.TYPE = query.FieldByName("TYPE").AsInteger;
                        bill.Add(tp_list);
                        query.Next();
                    }
                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }

            if (msg == "") { return true; }
            else { return false; }
        }

        public override bool IsValidData(out string msg, BF.Pub.CyQuery query, System.DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYID == 0)
            {
                msg = CrmLibStrings.msgNeedHYKNO;
                return false;
            }
            if (iQK==1&&fYJE == 0)
            {
                msg = "原余额为零，不能取款！";
                return false;
            }
            if (iQK==1&&fYJE < fQKJE)
            {
                msg = "取款不能大于原余额!";
                return false;
            }


            if (iCK == 1 && fFKJE < fCKJE)
            {
                msg = "付款金额不能小于存款金额!";
                return false;
            }

            if (iQK == 1 && fQKJE != Get_SSJEALL_NEWQK())
            {
                msg = "应收金额与取款金额不等！";
                return false;
            }
            return true;

        }
        private double Get_SSJEALL_NEW()
        {
            try
            {
                double tp_ssje = 0;
                foreach (SKFS one in ZFFS)
                {
                    tp_ssje += one.fJE;
                }
                return tp_ssje;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private double Get_SSJEALL_NEWQK()
        {
            try
            {
                double tp_ssje = 0;
                foreach (SKFSQK one in ZFFSQK)
                {
                    tp_ssje += one.fJE;
                }
                return tp_ssje;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public bool DeleteDataCK(out string msg, CyQuery query = null)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_CKJL", "CZJPJ_JLBH", iJLBH );
            return msg == "";
        }
        public bool DeleteDataQK(out string msg, CyQuery query = null)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_QKJL", "CZJPJ_JLBH", iJLBH);
            return msg == "";
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iCK == 1)
            {
                CheckExecutedTable(query, "MZK_CKJL", "CZJPJ_JLBH");
                if (iJLBH != 0)
                {
                    DeleteDataCK(out msg, query);
                }
                else
                    iJLBH = SeqGenerator.GetSeq("MZK_CKJL");
                query.SQL.Text = "insert into MZK_CKJL(CZJPJ_JLBH,HYID,ZY,YJE,CKJE,DJR,DJRMC,DJSJ,MDID,HYKNO,BJ_CHILD)";
                query.SQL.Add(" values(:JLBH,:HYID,:ZY,:YJE,:CKJE,:DJR,:DJRMC,:DJSJ,:CZMD,:HYKNO,0)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("ZY").AsString = sZY;
                query.ParamByName("YJE").AsFloat = fYJE;
                query.ParamByName("CKJE").AsFloat = fCKJE;
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("DJSJ").AsDateTime = serverTime;
                query.ParamByName("CZMD").AsInteger = iMDID;
                // query.ParamByName("MDID").AsInteger = iMDID;
                query.ParamByName("HYKNO").AsString = sHYKNO;
                query.ExecSQL();

                //支付方式
                double ZJE = 0;
                foreach (SKFS one in ZFFS)
                {
                   
                    ZJE += one.fJE;

                
                }
                foreach (SKFS one in ZFFS)
                {

                    if (one.iZFFSID == 1)
                        one.fJE = one.fJE-(ZJE - fCKJE);
                    //Math.Round(one.JE, 2);
                    query.SQL.Text = "insert into MZK_CZK_CK_SKJL(JLBH,ZFFSID,JE)";
                    query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                    query.ParamByName("JE").AsFloat = one.fJE;
                    query.ExecSQL();
                }

                ExecTable(query, "MZK_CKJL", serverTime, "CZJPJ_JLBH");

                query.SQL.Text = "update MZKXX set STATUS=1 where STATUS=0 and HYID=:HYID";
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
                CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iMDID, fCKJE, "储值卡存款", sHYK_NO);
                CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_CK, iJLBH, iLoginRYID, sLoginRYMC, fCKJE);

            }

            if (iQK == 1) {
                CheckExecutedTable(query, "MZK_QKJL", "CZJPJ_JLBH");
                if (iJLBH != 0)
                {
                    DeleteDataQK(out msg, query);
                }
                else
                    iJLBH = SeqGenerator.GetSeq("MZK_QKJL");
                query.SQL.Text = "insert into MZK_QKJL(CZJPJ_JLBH,HYID,ZY,YJE,QKJE,DJR,DJRMC,DJSJ,MDID,HYKNO,BJ_CHILD)";
                query.SQL.Add(" values(:JLBH,:HYID,:ZY,:YJE,:QKJE,:DJR,:DJRMC,:DJSJ,:MDID,:HYKNO,0)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("ZY").AsString = sZY;
                query.ParamByName("YJE").AsFloat = fYJE;
                query.ParamByName("QKJE").AsFloat = fQKJE;
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("DJSJ").AsDateTime = serverTime;
                query.ParamByName("MDID").AsInteger = iMDID;
                query.ParamByName("HYKNO").AsString = sHYKNO;
                query.ExecSQL();

                //支付方式
             
                foreach (SKFSQK one in ZFFSQK)
                {

                    //Math.Round(one.JE, 2);

                    query.SQL.Text = "insert into MZK_CZK_QK_SKJL(JLBH,ZFFSID,JE)";
                    query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                    query.ParamByName("JE").AsFloat = one.fJE;
                    query.ExecSQL();
                }
                ExecTable(query, "MZK_QKJL", serverTime, "CZJPJ_JLBH");

                query.SQL.Text = "update MZKXX set STATUS=1 where STATUS=0 and HYID=:HYID";
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
                CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, iMDID, -fQKJE,"储值卡取款", sHYK_NO);
                CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_QK, iJLBH, iLoginRYID, sLoginRYMC, -fQKJE);
            
            }

        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iCK == 1)
            {
                ExecTable(query, "MZK_CKJL", serverTime, "CZJPJ_JLBH");

                query.SQL.Text = "update MZKXX set STATUS=1 where STATUS=0 and HYID=:HYID";
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();

                CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iMDID, fCKJE, "储值卡存款", sHYK_NO);
                CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_CK, iJLBH, iLoginRYID, sLoginRYMC, fCKJE);
            }

            if (iQK == 1) {
                ExecTable(query, "MZK_QKJL", serverTime, "CZJPJ_JLBH");
              
                query.SQL.Text = "update MZKXX set STATUS=1 where STATUS=0 and HYID=:HYID";
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ExecSQL();
                CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, iMDID, -fQKJE, "储值卡取款", sHYK_NO);
                CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_QK, iJLBH, iLoginRYID, sLoginRYMC, -fQKJE);
            
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("sHYKNO", "W.HYKNO");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,H.HYKTYPE,D.HYKNAME,M.MDMC,H.YXQ,E.PDJE,E.YE";
            query.SQL.Add(" from MZK_CKJL W,MZKXX H,HYKDEF D,MDDY M,HYK_JEZH E");
            query.SQL.Add(" where W.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.MDID=M.MDID and E.HYID=H.HYID");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKCQK obj = new MZKGL_MZKCQK();
            obj.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYKNO = query.FieldByName("HYKNO").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.fYJE = query.FieldByName("YJE").AsFloat;
            obj.fCKJE = query.FieldByName("CKJE").AsFloat;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
            obj.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
            obj.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
            return obj;
        }



    }
}
