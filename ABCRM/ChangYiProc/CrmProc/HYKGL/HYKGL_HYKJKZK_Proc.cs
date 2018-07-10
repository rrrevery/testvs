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
    public class HYKGL_HYKJKZK : DJLR_ZX_CLass
    {

        public int iXKSL = 0;
        public string dXKRQ = string.Empty;
        public int iBJ_CZK = 0;

        public List<HYKGL_HYKJKItem> itemTable = new List<HYKGL_HYKJKItem>();

        public class HYKGL_HYKJKItem
        {
            public string sCZKHM, sCDNR;
            public double fJE = 0;
            public string dXKRQ = string.Empty;
            public int iBJ_ZK = 0;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYKJKJLITEM;", "JLBH", iJLBH, "");
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            try
            {
                //更新建卡记录表
                if (iBJ_CZK == 1)
                { query.SQL.Text = " update MZK_JKJL set XKSL=XKSL+:XKSL WHERE JLBH=:JLBH"; }

                else
                {
                    query.SQL.Text = " update HYKJKJL set XKSL=XKSL+:XKSL WHERE JLBH=:JLBH"; //,ZXR=:ZXR,ZXRMC=:ZXRMC,ZXRQ=:ZXRQ 
                }
                query.ParamByName("XKSL").AsInteger = 1;
                query.ParamByName("JLBH").AsInteger = iJLBH;
                //query.ParamByName("ZXR").AsInteger = iLoginRYID;
                //query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                //query.ParamByName("ZXRQ").AsDateTime = serverTime;
                if (query.ExecSQL() != 0)
                {
                    foreach (HYKGL_HYKJKItem one in itemTable)
                    {
                        if (iBJ_CZK == 1)
                        {
                            query.SQL.Text = " Update MZK_JKJLITEM set BJ_ZK=:BJ_ZK,XKRQ=:XKRQ WHERE JLBH=:JLBH  and CZKHM =:CZKHM";
                        }
                        else
                        {
                            query.SQL.Text = " Update HYKJKJLITEM set BJ_ZK=:BJ_ZK,XKRQ=:XKRQ WHERE JLBH=:JLBH  and CZKHM =:CZKHM";
                        }

                        query.ParamByName("JLBH").AsInteger = iJLBH;
                        query.ParamByName("CZKHM").AsString = one.sCZKHM;
                        query.ParamByName("BJ_ZK").AsInteger = 1;
                        query.ParamByName("XKRQ").AsDateTime = serverTime;
                        query.ExecSQL();


                        query.SQL.Text = "insert into HYK_ZKXXJL(HYK_NO,RQ,HYID,CLLX,ZXR,ZXRMC,CZDD)";
                        query.SQL.Add("select A.CZKHM,:RQ,:HYID,:CLLX,:ZXR,:ZXRMC,:CZDD from HYKJKJLITEM A where A.JLBH=:JKJLBH and A.CZKHM=:CZKHM");
                        query.ParamByName("RQ").AsDateTime = serverTime;
                        query.ParamByName("HYID").AsInteger = 0;
                        query.ParamByName("CLLX").AsInteger = 0;
                        query.ParamByName("ZXR").AsInteger = iLoginRYID;
                        query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                        query.ParamByName("CZDD").AsString = sBGDDDM;
                        query.ParamByName("JKJLBH").AsInteger = iJLBH;
                        query.ParamByName("CZKHM").AsString = one.sCZKHM;
                        query.ExecSQL();



                        //query.SQL.Text = " delete from  HYKJKJLITEM  WHERE JLBH=:JLBH  and CZKHM =:CZKHM ";
                        //query.ParamByName("JLBH").AsInteger = iJLBH;
                        //query.ParamByName("CZKHM").AsString = one.sCZKHM;
                        //query.ExecSQL();

                        //重新插入子表
                        //query.SQL.Text = "insert into HYKJKJLITEM(JLBH,CZKHM,CDNR,JE,BJ_ZK,XKRQ)";
                        //query.SQL.Add(" values(:JLBH,:CZKHM,:CDNR,:JE,:BJ_ZK,:XKRQ)");
                        //query.ParamByName("JLBH").AsInteger = iJLBH;
                        //query.ParamByName("CZKHM").AsString = one.sCZKHM;
                        //query.ParamByName("CDNR").AsString = one.sCDNR;
                        //query.ParamByName("JE").AsFloat = one.fJE;
                        //query.ParamByName("BJ_ZK").AsInteger = 1;
                        //query.ParamByName("XKRQ").AsDateTime = serverTime;
                        //query.ExecSQL();

                        if (iBJ_CZK == 1)
                        {
                            //解决与建卡的冲突
                            query.SQL.Text = " delete from  MZKCARD  where CZKHM =:CZKHM ";
                            query.ParamByName("CZKHM").AsString = one.sCZKHM;
                            query.ExecSQL();
                            //存储到库存表
                            query.SQL.Text = "insert into MZKCARD(CZKHM,CDNR,HYKTYPE,QCYE,PDJE,YXTZJE,JKFS,BGDDDM,BGR,STATUS,YXQ,FXDWID,XKRQ)";
                            query.SQL.Add(" select I.CZKHM,I.CDNR,L.HYKTYPE,L.QCYE,L.PDJE,L.YXTZJE,0,L.BGDDDM,L.BGR,0,L.YXQ,L.FXDWID,I.XKRQ");
                            query.SQL.Add(" from MZK_JKJL L,MZK_JKJLITEM I where L.JLBH=I.JLBH and L.JLBH=:JLBH and I.BJ_ZK=1 and I.CZKHM =:CZKHM");
                            query.ParamByName("JLBH").AsInteger = iJLBH;
                            query.ParamByName("CZKHM").AsString = one.sCZKHM;
                            query.ExecSQL();
                        }
                        else
                        {
                            //解决与建卡的冲突
                            query.SQL.Text = " delete from  HYKCARD  where CZKHM =:CZKHM ";
                            query.ParamByName("CZKHM").AsString = one.sCZKHM;
                            query.ExecSQL();
                            //存储到库存表
                            query.SQL.Text = "insert into HYKCARD(CZKHM,CDNR,HYKTYPE,QCYE,PDJE,YXTZJE,JKFS,BGDDDM,BGR,STATUS,YXQ,FXDWID,XKRQ)";
                            query.SQL.Add(" select I.CZKHM,I.CDNR,L.HYKTYPE,L.QCYE,L.PDJE,L.YXTZJE,0,L.BGDDDM,L.BGR,0,L.YXQ,L.FXDWID,I.XKRQ");
                            query.SQL.Add(" from HYKJKJL L,HYKJKJLITEM I where L.JLBH=I.JLBH and L.JLBH=:JLBH and I.BJ_ZK=1 and I.CZKHM =:CZKHM");
                            query.ParamByName("JLBH").AsInteger = iJLBH;
                            query.ParamByName("CZKHM").AsString = one.sCZKHM;
                            query.ExecSQL();
                        }
                    }

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
    }
}
