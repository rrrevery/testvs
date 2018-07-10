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


namespace BF.CrmProc.YHQGL
{
    public class YHQGL_JFDXBL : DJLR_ZXQDZZ_CLass
    {
        public string dKSRQ = string.Empty;
        public string dJSRQ = string.Empty;
        public int iHYKTYPE = 0, iMDID = 0;
        public double fDHJF = 0;
        public double fDHJE = 0;
        public double fQDJF = 0;//铺底积分
        public string sHYKNAME = string.Empty, sMDMC = string.Empty;

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        //删除
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_JFDHBL;", "JLBH", iJLBH);
        }

        //数据修改 添加
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYK_JFDHBL");
            }
            query.SQL.Text = "insert into HYK_JFDHBL(JLBH,KSRQ,JSRQ,HYKTYPE,DHJF,DHJE,QDJF,DJR,DJRMC,DJSJ,MDID,CXID,STATUS)";//,ZXR,ZXRMC,ZXRQ
            query.SQL.Add(" values(:JLBH,:KSRQ,:JSRQ,:HYKTYPE,:DHJF,:DHJE,:QDJF,:DJR,:DJRMC,:DJSJ,:MDID,0,0)");//,:ZXR,:ZXRMC,:ZXRQ
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("DHJF").AsFloat = fDHJF;
            query.ParamByName("DHJE").AsFloat = fDHJE;
            query.ParamByName("QDJF").AsFloat = fQDJF;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();

        }

        public override bool IsValidExecData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            query.SQL.Text = "  select * from HYK_JFDHBL where MDID=:MDID and HYKTYPE=:HYKTYPE and STATUS=1 ";
            query.SQL.Add("  and KSRQ<=:JSRQ and JSRQ>:KSRQ");
            query.ParamByName("JSRQ").AsDateTime = FormatUtils.ParseDateString(dJSRQ);
            query.ParamByName("KSRQ").AsDateTime = FormatUtils.ParseDateString(dKSRQ);
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "已存在编号为" + query.FieldByName("JLBH").AsInteger + "的有效兑换比例";
                return false;
            }
            return true;
        }
        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFDHBL", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ", 1);
            query.SQL.Text = "delete from HYK_JF_DHBL where HYKTYPE=:HYKTYPE";
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ExecSQL();
            query.SQL.Text = "insert into HYK_JF_DHBL(HYKTYPE,DHJF,DHJE) values(:HYKTYPE,:DHJF,:DHJE) ";
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("DHJF").AsFloat = fDHJF;
            query.ParamByName("DHJE").AsFloat = fDHJE;
            query.ExecSQL();
        }

        //停用
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_JFDHBL", serverTime, "JLBH", "ZZR", "ZZRMC", "ZZSJ", 3);
            query.Close();
            query.SQL.Text = "delete from HYK_JF_DHBL where HYKTYPE=:HYKTYPE ";
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ExecSQL();
        }
        //查询
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("dKSRQ", "W.KSRQ");
            CondDict.Add("dJSRQ", "W.JSRQ");
            CondDict.Add("iHYKTYPE", "W.HYKTYPE");
            CondDict.Add("fDHJF", "W.DHJF");
            CondDict.Add("fDHJE", "W.DHJE");
            CondDict.Add("fQDJF", "W.QDJF");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("sDJRMC", "W.DJRMC");
            CondDict.Add("dDJSJ", "W.DJSJ");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("sZXRMC", "W.ZXRMC");
            CondDict.Add("dZXRQ", "W.ZXRQ");
            CondDict.Add("iZZR", "W.ZZR");
            CondDict.Add("sZZRMC", "W.ZZRMC");
            CondDict.Add("sHYKNAME", "H.HYKNAME");
            CondDict.Add("iMDID", "W.MDID");
            query.SQL.Text = "select W.*,H.HYKNAME,M.MDMC from HYK_JFDHBL W,HYKDEF H ,MDDY M";
            query.SQL.Add("where W.HYKTYPE=H.HYKTYPE and W.MDID=M.MDID(+)");
            SetSearchQuery(query, lst);


            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            YHQGL_JFDXBL obj = new YHQGL_JFDXBL();

            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.dKSRQ = FormatUtils.DateToString(query.FieldByName("KSRQ").AsDateTime);
            obj.dJSRQ = FormatUtils.DateToString(query.FieldByName("JSRQ").AsDateTime);
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.fDHJF = query.FieldByName("DHJF").AsFloat;
            obj.fDHJE = query.FieldByName("DHJE").AsFloat;
            obj.fQDJF = query.FieldByName("QDJF").AsFloat;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZSJ").AsDateTime);
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = obj.iMDID == 0 ? "总部" : query.FieldByName("MDMC").AsString;
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return obj;
        }
    }

}
