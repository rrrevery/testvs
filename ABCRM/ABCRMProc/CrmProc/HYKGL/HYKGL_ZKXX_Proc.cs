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
    public class HYKGL_ZKXX : ZKXX
    {

        public string sMDMC = string.Empty;
        public string sCZKHM = string.Empty;
        public string sCDNR = string.Empty;

        public List<HYKGL_ZKXX> itemTable = new List<HYKGL_ZKXX>();
        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;

            return true;
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iCLLX == 0)
            {
                query.SQL.Text = "insert into HYK_ZKXXJL(HYK_NO,RQ,HYID,CLLX,ZXR,ZXRMC,CZDD)";
                query.SQL.Add("select A.CZKHM,:RQ,:HYID,:CLLX,:ZXR,:ZXRMC,:CZDD from HYKJKJLITEM A where A.JLBH=:JKJLBH and A.BJ_ZK=0");
                query.ParamByName("RQ").AsDateTime = serverTime;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("CLLX").AsInteger = iCLLX;
                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                query.ParamByName("CZDD").AsString = sBGDDDM;
                query.ParamByName("JKJLBH").AsInteger = iJKJLBH;
                query.ExecSQL();

                query.SQL.Text = "update HYKJKJLITEM set XKRQ=:XKRQ,BJ_ZK=BJ_ZK+ 1 where JLBH=:JLBH and BJ_ZK=0";
                query.ParamByName("XKRQ").AsDateTime = serverTime;
                query.ParamByName("JLBH").AsInteger = iJKJLBH;
                query.ExecSQL();

                query.SQL.Text = "update HYKCARD A set A.XKRQ=:XKRQ where exists (select 1 from HYKJKJLITEM B where A.CZKHM=B.CZKHM and B.BJ_ZK = 1 and B.JLBH=:JKJLBH)";
                query.ParamByName("JKJLBH").AsInteger = iJKJLBH;
                query.ParamByName("XKRQ").AsDateTime = serverTime;
                query.ExecSQL();




            }
            else
            {
                query.SQL.Text = "insert into HYK_ZKXXJL(HYK_NO,RQ,HYID,CLLX,ZXR,ZXRMC,CZDD)";
                query.SQL.Add(" values(:HYK_NO,:RQ,:HYID,:CLLX,:ZXR,:ZXRMC,:CZDD)");
                query.ParamByName("HYK_NO").AsString = sHYKNO;
                query.ParamByName("RQ").AsDateTime = serverTime;
                query.ParamByName("HYID").AsInteger = iHYID;
                query.ParamByName("CLLX").AsInteger = iCLLX;
                query.ParamByName("ZXR").AsInteger = iLoginRYID;
                query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                query.ParamByName("CZDD").AsString = sBGDDDM;
                query.ExecSQL();
                //0制卡,1验卡,2补磁
                KCKXX obj = new KCKXX();
                obj.sCZKHM = sHYK_NO;
                switch (iCLLX)
                {
                    case 1:
                        obj.iBJ_YK = 1;
                        break;
                    case 0:
                        obj.dXKRQ = FormatUtils.DatetimeToString(serverTime);
                        query.SQL.Text = "update HYKJKJLITEM set XKRQ=:XKRQ,BJ_ZK=BJ_ZK+ 1 where JLBH=:JLBH and CZKHM=:CZKHM";
                        query.ParamByName("XKRQ").AsDateTime = serverTime;
                        query.ParamByName("JLBH").AsInteger = iJKJLBH;
                        query.ParamByName("CZKHM").AsString = sHYKNO;
                        query.ExecSQL();
                        //System.Threading.Thread.Sleep(2000);
                        break;
                    case 2:
                        obj.dXKRQ = FormatUtils.DatetimeToString(serverTime);
                        break;
                }
                CrmLibProc.UpdateKCKZKXX(out msg, obj);
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH","H.XH");
            CondDict.Add("sHYK_NO", "H.HYK_NO");
            CondDict.Add("iCLLX", "H.CLLX");
            CondDict.Add("iZXR", "H.ZXR");
            CondDict.Add("dZXRQ", "H.RQ");
            query.SQL.Text = " select H.* from HYK_ZKXXJL H where 1=1";
            SetSearchQuery(query, lst);
            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            HYKGL_ZKXX obj = new HYKGL_ZKXX();
            obj.iXH = query.FieldByName("XH").AsInteger;
            obj.sHYKNO = query.FieldByName("HYK_NO").AsString;
            obj.dRQ = FormatUtils.DatetimeToString(query.FieldByName("RQ").AsDateTime);
            obj.iHYID = query.FieldByName("HYID").AsInteger;
            obj.iCLLX = query.FieldByName("CLLX").AsInteger;
            obj.sCLLX = BASECRMDefine.ZKCLLXName[obj.iCLLX];
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            //obj.sBGDDDM = query.FieldByName("CZDD").AsString;
            //obj.sBGDDMC = query.FieldByName("BGDDMC").AsString;
            //obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }

    }
}
