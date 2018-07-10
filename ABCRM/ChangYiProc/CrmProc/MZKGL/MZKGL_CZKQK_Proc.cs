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
    public class MZKGL_CZKQK : DZHYK_DJLR_CLass
    {
        public double fYJE = 0;
        public double fQKJE = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;

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
            if (fYJE == 0)
            {
                msg = "原余额为零，不能取款！";
                return false;
            }
            if (fYJE < fQKJE)
            {
                msg = "取款不能大于原余额!";
                return false;
            }
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                try
                {
                    query.SQL.Text = "select YE from MZK_JEZH where HYID=:HYID ";
                    query.ParamByName("HYID").AsInteger = iHYID;
                    query.Open();
                    if (query.FieldByName("YE").AsFloat < fQKJE)
                    {
                        msg = "该账户的余额不够！";
                        return false;
                    }
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
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_QKJL", "CZJPJ_JLBH", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            CheckExecutedTable(query, "MZK_QKJL", "CZJPJ_JLBH");
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_QKJL");
            query.SQL.Text = "insert into MZK_QKJL(CZJPJ_JLBH,HYID,ZY,YJE,QKJE,DJR,DJRMC,DJSJ,CZDD,MDID,HYKNO,BJ_CHILD)";
            query.SQL.Add(" values(:JLBH,:HYID,:ZY,:YJE,:QKJE,:DJR,:DJRMC,:DJSJ,:CZDD,:MDID,:HYKNO,0)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ParamByName("ZY").AsString = sZY;
            query.ParamByName("YJE").AsFloat = fYJE;
            query.ParamByName("QKJE").AsFloat = fQKJE;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CZDD").AsString = sBGDDDM;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("HYKNO").AsString = sHYKNO;
            query.ExecSQL();
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "MZK_QKJL", serverTime, "CZJPJ_JLBH");
            // iMDID = CrmLibProc.BGDDToMDID(query, sBGDDDM);
            //更新状态
            query.SQL.Text = "update MZKXX set STATUS=1 where STATUS=0 and HYID=:HYID";
            query.ParamByName("HYID").AsInteger = iHYID;
            query.ExecSQL();
            CrmLibProc.UpdateMZKJEZH(out msg, query, iHYID, BASECRMDefine.CZK_CLLX_QK, iJLBH, iMDID, -fQKJE, "储值卡取款", sHYK_NO);
            CrmLibProc.SaveCardTrack(query, sHYKNO, iHYID, (int)BASECRMDefine.CZK_DKGZCLLX.CZK_CLLX_QK, iJLBH, iLoginRYID, sLoginRYMC, -fQKJE);
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.CZJPJ_JLBH");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            query.SQL.Text = "select W.*,H.HY_NAME,H.HYKTYPE,D.HYKNAME,M.MDMC";
            query.SQL.Add(" from MZK_QKJL W,MZKXX H,HYKDEF D,MDDY M");
            query.SQL.Add(" where W.HYID=H.HYID and H.HYKTYPE=D.HYKTYPE and W.MDID=M.MDID");
            SetSearchQuery(query, lst); 
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_CZKQK obj = new MZKGL_CZKQK();
            obj.iJLBH = query.FieldByName("CZJPJ_JLBH").AsInteger;
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.sHYKNO = query.FieldByName("HYKNO").AsString;
            obj.sZY = query.FieldByName("ZY").AsString;
            obj.fYJE = query.FieldByName("YJE").AsFloat;
            obj.fQKJE = query.FieldByName("QKJE").AsFloat;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDBMZK");
            try
            {
                try
                {
                    query.SQL.Text = "select YE+YXTZJE from MZK_JEZH where HYID=:pHYID";
                    query.ParamByName("pHYID").AsInteger = iHYID;
                    query.Open();
                    if (query.IsEmpty)
                    {
                        msg = "该账户的没有余额";
                        return false;
                    }
                    if (query.Fields[0].AsFloat < fQKJE)
                    {
                        msg = "该账户的余额不够！";
                        return false;
                    }

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
            return true;
        }
    }
}
