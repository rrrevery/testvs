﻿using System;
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
    public class HYKGL_JEZCKCL : DZHYK_DJLR_CLass
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
            public int iID = 0;
            public string sDM = string.Empty;
            public string sNAME = string.Empty;
            public int iTYPE = 0;
        }

        public bool Get_SKFS(out string msg, out List<HYKGL_JEZCKCL.SKFS> bill)
        {
            bill = new List<HYKGL_JEZCKCL.SKFS>();
            msg = "";
            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
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
                        HYKGL_JEZCKCL.SKFS tp_list = new HYKGL_JEZCKCL.SKFS();
                        tp_list.iID = query.FieldByName("ZFFSID").AsInteger;
                        tp_list.sDM = query.FieldByName("ZFFSDM").AsString;
                        tp_list.sNAME = query.FieldByName("ZFFSMC").AsString;
                        tp_list.iTYPE = query.FieldByName("TYPE").AsInteger;
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
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_CZK_CKJL", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "HYK_CZK_CKJL", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_CZK_CKJL");
            //iMDID = CrmLibProc.BGDDToMDID(query, sBGDD);
            query.SQL.Text = "insert into HYK_CZK_CKJL(CZJPJ_JLBH,HYID,ZY,YJE,CKJE,DJR,DJRMC,DJSJ,MDID,HYKNO,BJ_CHILD,ZFFSID)";
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
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_CZK_CKJL", serverTime, "CZJPJ_JLBH");
            //iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //更新状态
            query.SQL.Text = "update HYK_HYXX set STATUS=1 where STATUS=0 and HYID=:HYID";
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
            CrmLibProc.UpdateJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_CK, iJLBH, iMDID, fCKJE, "会员卡存款");
            //CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_CK, iJLBH, iLoginRYID, sLoginRYMC, fCKJE);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("fCKJE", "W.CKJE");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sHYKNO", "W.HYKNO");

            query.SQL.Text = "select W.*,H.HY_NAME,H.HYKTYPE,D.HYKNAME,M.MDMC,S.ZFFSMC,S.ZFFSDM";
            query.SQL.Add(" from HYK_CZK_CKJL W,HYK_HYXX H,HYKDEF D,MDDY M,ZFFS S");
            query.SQL.Add(" where W.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.MDID=M.MDID and W.ZFFSID=S.ZFFSID");
            SetSearchQuery(query, lst);
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYKGL_JEZCKCL obj = new HYKGL_JEZCKCL();
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
