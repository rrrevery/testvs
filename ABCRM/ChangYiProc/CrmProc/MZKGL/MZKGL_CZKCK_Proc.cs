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
    public class MZKGL_CZKCK : DZHYK_DJLR_CLass
    {
        public double fYJE = 0;
        public double fCKJE = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public int iZFFSID = 0;
        public string sZFFSMC = string.Empty;
        public string sZFFSDM = string.Empty;
        public List<SKFS> ZFFS = new List<SKFS>();
        public class SKFS
        {
            public string sZFFSMC;
            public int iZFFSID = 0;
            public decimal fJE = 0;
            public string dFKRQ = string.Empty;
            public string sJYBH = string.Empty;
            public int TYPE = 0;
            public string DM = string.Empty;

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
                    query.SQL.Text = " SELECT * FROM ZFFS where BJ_MJ=1 order by ZFFSID";// 
                    query.Open();
                    while (!query.Eof)
                    {
                        MZKGL_CZKCK.SKFS tp_list = new MZKGL_CZKCK.SKFS();
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
                    //
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

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYID == 0)
            {
                msg = CrmLibStrings.msgNeedHYKNO;
                return false;
            }
            //if (sBGDDDM == "")
            //{
            //    msg = CrmLibStrings.msgNeedBGDD;
            //    return false;
            //}
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_CKJL;MZK_CKJLSKITEM", "CZJPJ_JLBH", iJLBH, "", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "MZK_CKJL", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_CKJL");
            //iMDID = CrmLibProc.BGDDToMDID(query, sBGDD);
            query.SQL.Text = "insert into MZK_CKJL(CZJPJ_JLBH,HYID,ZY,YJE,CKJE,DJR,DJRMC,DJSJ,MDID,HYKNO,BJ_CHILD,ZFFSID)";
            query.SQL.Add(" values(:JLBH,:HYID,:ZY,:YJE,:CKJE,:DJR,:DJRMC,:DJSJ,:CZMD,:HYKNO,0,:ZFFSID)");
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
            query.ParamByName("ZFFSID").AsInteger = iZFFSID;
            query.ExecSQL();
            foreach (SKFS one in ZFFS)
            {
                query.SQL.Text = "insert into MZK_CKJLSKITEM(CZJPJ_JLBH,ZFFSID,JE)";
                query.SQL.Add(" values(:JLBH,:ZFFSID,:JE)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("ZFFSID").AsInteger = one.iZFFSID;
                query.ParamByName("JE").AsDecimal = one.fJE;
                //query.ParamByName("JYBH").AsString = one.sJYBH;
                query.ExecSQL();
            }


        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_CKJL", serverTime, "CZJPJ_JLBH");
            //iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //更新状态
            query.SQL.Text = "update MZKXX set STATUS=1 where STATUS=0 and HYID=:HYID";
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
            CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iMDID, fCKJE, "储值卡存款", sHYK_NO);
            CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_CK, iJLBH, iLoginRYID, sLoginRYMC, fCKJE);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("sHYKNO", "W.HYKNO");
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,H.HY_NAME,H.HYKTYPE,D.HYKNAME,M.MDMC";
            query.SQL.Add(" from MZK_CKJL W,MZKXX H,HYKDEF D,MDDY M");
            query.SQL.Add(" where W.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.MDID=M.MDID ");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.*,S.ZFFSMC";
                query.SQL.Add(" from ZFFS S,MZK_CKJLSKITEM I");
                query.SQL.Add(" where I.ZFFSID=S.ZFFSID AND I.CZJPJ_JLBH=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    SKFS item = new SKFS();
                    ((MZKGL_CZKCK)lst[0]).ZFFS.Add(item);
                    item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                    item.fJE = query.FieldByName("JE").AsDecimal;
                    item.sZFFSMC= query.FieldByName("ZFFSMC").AsString;
                    //item.sJYBH = query.FieldByName("JYBH").AsString;
                    query.Next();
                }

            }
            
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            MZKGL_CZKCK obj = new MZKGL_CZKCK();
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
            return obj;
        }
    }
}
