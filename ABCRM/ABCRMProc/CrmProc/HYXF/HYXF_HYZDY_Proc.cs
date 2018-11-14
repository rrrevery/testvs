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
    public class HYXF_HYZDY_Proc : DJLR_ZXQDZZ_CLass
    {
        public int iGRPID
        {

            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGRPMC = string.Empty;
        public string sGRPMS
        {
            set { sZY = value; }
            get { return sZY; }
        }

        public int iGRPYT = -1;
        public string dKSSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public string dJSSJ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iSRFS = -1;
        public int iGZFS = -1;
        public int iGXZQ = -1;
        public int iJB = -1;
        public string dYXQ = FormatUtils.DatetimeToString(DateTime.MinValue);
        public int iBJ_DJT = -1;
        public int iBJ_YXXG = -1;
        public string iXGR = string.Empty;
        public string sXGRMC = string.Empty;
        public string dXGRQ = string.Empty;
        public int iHYZLXID = 0;
        public string sHYZLXMC = string.Empty;
        public int iBJ_WXHY = 0;
        public string sMDMC = string.Empty;
        public int iMDID = 0;

        public List<HYXF_HYZDYItem> itemTable = new List<HYXF_HYZDYItem>();

        public class HYXF_HYZDYItem
        {

            public int iGZBH = 0;
            public int iSJLX = 0;
            public string sSJNR = string.Empty;
            public string sSJNR2 = string.Empty;
            public string sSJDM = string.Empty;
            public string sSJMC = string.Empty;
            public string sSJMC2 = string.Empty;
            public string sSJMC3 = string.Empty;
            // SJLX值   SJNR意义
            // 1        HYID 
            // 2        证件类型
            // 3        卡类型
            // 4        性别 
            // 5        职业类型
            // 20       开卡日期   
            // 22       发行单位
            // 23       最后消费日期
            // 24       积分余额
            // 25       升级积分
            // 26       消费积分
            // 27       门店
            // 28       汇总时间段(未启用)
            // 29       最后门店消费日期(未启用)
            // 30       消费金额(未启用)
            // 31       来店次数(未启用)
            // 32       客单价(未启用)
            // 33       消费次数(未启用)
            // 34       消费排名(未启用)
            // ？       GKID
        }

        public override bool IsValidData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            return true;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_HYGRP;HYK_HYGRP_GZMX;", "GRPID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {


            msg = string.Empty;
            if (iJLBH != 0)
            {
                //删除子表数据
                query.SQL.Text = "delete from HYK_HYGRP_GZMX where GRPID= " + iJLBH;
                int row = query.ExecSQL();

                query.SQL.Text = "update HYK_HYGRP set GRPID = :GRPID,GRPMC = :GRPMC,GRPMS = :GRPMS,GRPYT = :GRPYT,KSSJ = :KSSJ,JSSJ = :JSSJ,SRFS = :SRFS,GZFS = :GZFS,GXZQ = :GXZQ,HYZLXID = :HYZLXID,JB = :JB,YXQ = :YXQ,BJ_DJT = :BJ_DJT,BJ_YXXG = :BJ_YXXG,XGR = :XGR,XGRMC = :XGRMC,XGRQ=:XGRQ,BJ_WXHY=:BJ_WXHY,MDID=:MDID";
                query.SQL.Text += " where GRPID=" + iJLBH;

                query.ParamByName("XGR").AsInteger = iLoginRYID;
                query.ParamByName("XGRMC").AsString = sLoginRYMC;
                query.ParamByName("XGRQ").AsDateTime = serverTime;
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYK_HYGRP");
                query.SQL.Text = "insert into HYK_HYGRP(GRPID,GRPMC,GRPMS,GRPYT,KSSJ,JSSJ,DJR,DJRMC,DJSJ,STATUS,SRFS,GZFS,GXZQ,HYZLXID,JB,YXQ,BJ_DJT,BJ_YXXG,BJ_WXHY,MDID)";
                query.SQL.Add(" values(:GRPID,:GRPMC,:GRPMS,:GRPYT,:KSSJ,:JSSJ,:DJR,:DJRMC,:DJSJ,:STATUS,:SRFS,:GZFS,:GXZQ,:HYZLXID,:JB,:YXQ,:BJ_DJT,:BJ_YXXG,:BJ_WXHY,:MDID)");
                query.ParamByName("STATUS").AsInteger = iSTATUS;
                query.ParamByName("DJR").AsInteger = iLoginRYID;
                query.ParamByName("DJRMC").AsString = sLoginRYMC;
                query.ParamByName("DJSJ").AsDateTime = serverTime;
            }

            query.ParamByName("GRPID").AsInteger = iGRPID;
            query.ParamByName("GRPMC").AsString = sGRPMC;
            query.ParamByName("GRPMS").AsString = sGRPMS;
            query.ParamByName("GRPYT").AsInteger = iGRPYT;
            query.ParamByName("KSSJ").AsDateTime = FormatUtils.ParseDateString(dKSSJ);
            query.ParamByName("JSSJ").AsDateTime = FormatUtils.ParseDateString(dJSSJ);
            query.ParamByName("SRFS").AsInteger = iSRFS;
            query.ParamByName("GZFS").AsInteger = iGZFS;
            query.ParamByName("GXZQ").AsInteger = iGXZQ;
            query.ParamByName("HYZLXID").AsInteger = iHYZLXID;
            query.ParamByName("JB").AsInteger = iJB;
            query.ParamByName("YXQ").AsDateTime = FormatUtils.ParseDateString(dYXQ);
            query.ParamByName("BJ_DJT").AsInteger = iBJ_DJT;
            query.ParamByName("BJ_YXXG").AsInteger = iBJ_YXXG;
            query.ParamByName("BJ_WXHY").AsInteger = iBJ_WXHY;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();
            foreach (HYXF_HYZDYItem one in itemTable)
            {

                if (one.iSJLX == 7)
                {
                    string hykno = one.sSJNR;
                    one.sSJNR = Convert.ToString(CrmLibProc.GetHYID(query, one.sSJNR));
                    if (one.sSJNR == "0")
                    {
                        msg = "卡号：" + hykno + " 不存在";
                        throw new Exception(msg);
                    }
                    one.iSJLX = 1;
                    //query.SQL.Text = "insert into HYK_HYGRP_GZMX(GRPID,GZBH,SJLX,SJNR,SJNR2)";
                    //query.SQL.Add(" values(:GRPID,:GZBH,:SJLX,:SJNR,:SJNR2)");
                    //query.ParamByName("GRPID").AsInteger = iJLBH;
                    //query.ParamByName("GZBH").AsInteger = one.iGZBH;
                    //query.ParamByName("SJLX").AsInteger = one.iSJLX;
                    //query.ParamByName("SJNR").AsString = one.sSJNR;
                    //query.ParamByName("SJNR2").AsString = one.sSJNR2;
                    //query.ExecSQL();
                }
                if (one.iSJLX == 3)
                {
                    string[] tp_hyktype = one.sSJNR.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tp_hyktype.Length > 0)
                    {
                        for (int i = 0; i < tp_hyktype.Length; i++)
                        {
                            query.SQL.Text = "insert into HYK_HYGRP_GZMX(GRPID,GZBH,SJLX,SJNR,SJNR2)";
                            query.SQL.Add(" values(:GRPID,:GZBH,:SJLX,:SJNR,:SJNR2)");
                            query.ParamByName("GRPID").AsInteger = iJLBH;
                            query.ParamByName("GZBH").AsInteger = one.iGZBH;
                            query.ParamByName("SJLX").AsInteger = one.iSJLX;
                            query.ParamByName("SJNR").AsString = tp_hyktype[i].Trim();
                            query.ParamByName("SJNR2").AsString = one.sSJNR2;
                            query.ExecSQL();
                        }

                    }
                }
                else
                {
                    query.SQL.Text = "insert into HYK_HYGRP_GZMX(GRPID,GZBH,SJLX,SJNR,SJNR2)";
                    query.SQL.Add(" values(:GRPID,:GZBH,:SJLX,:SJNR,:SJNR2)");
                    query.ParamByName("GRPID").AsInteger = iJLBH;
                    query.ParamByName("GZBH").AsInteger = one.iGZBH;
                    query.ParamByName("SJLX").AsInteger = one.iSJLX;
                    query.ParamByName("SJNR").AsString = one.sSJNR;
                    query.ParamByName("SJNR2").AsString = one.sSJNR2;
                    query.ExecSQL();
                }
            }
        }

        //审核
        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_HYGRP", serverTime, "GRPID");
            //query.SQL.Text = "update HYK_HYGRP set STATUS=" + 1 + " where GRPID=" + iJLBH;
            query.SQL.Text = "update HYK_HYGRP set STATUS=" + 2 + " where GRPID=" + iJLBH;
            query.ExecSQL();
        }
        //停用
        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_HYGRP", serverTime, "GRPID", "ZZR", "ZZRMC", "ZZRQ", -2);

        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "W.GRPID");
            CondDict.Add("iHYZLXID", "W.HYZLXID");
            CondDict.Add("iMDID", "W.MDID");
            CondDict.Add("sGRPMC","W.GRPMC");
            CondDict.Add("iBJ_DJT", "W.BJ_DJT");

            query.SQL.Text = "select W.*,D.HYZLXMC,M.MDMC from HYK_HYGRP W,HYZLXDY D,MDDY M ";
            query.SQL.Add("where W.HYZLXID=D.HYZLXID and W.MDID=M.MDID(+)");
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select I.GRPID,I.SJLX SJLX,I.SJNR SJNR,H.HYK_NO SJDM,D.HYKNAME SJMC,H.HY_NAME SJMC2,H.HYKTYPE SJMC3 from HYK_HYGRP_GZMX I,HYK_HYXX H,HYKDEF D where to_number(I.SJNR)=H.HYID and H.HYKTYPE=D.HYKTYPE  and I.SJLX=1";
                query.SQL.Text += "and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX SJLX,I.SJNR SJNR,I.SJNR SJDM,I.SJNR SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 from HYK_HYGRP_GZMX I where  (I.SJLX between 23 and 26  or I.SJLX between 28 and 34 or I.SJLX in (20,37,39,100))";
                query.SQL.Text += " and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX,I.SJNR,I.SJNR SJDM,H.HYKNAME SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 FROM HYKDEF H,HYK_HYGRP_GZMX I where H.HYKTYPE=I.SJNR and I.SJLX=3";
                query.SQL.Text += " and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX,I.SJNR,I.SJNR SJDM,F.FXDWMC SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 FROM FXDWDEF F,HYK_HYGRP_GZMX I where F.FXDWID=I.SJNR and I.SJLX=6 and ROWNUM<=1";
                query.SQL.Text += "and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += " select I.GRPID,I.SJLX,I.SJNR,I. SJNR SJDM,M.MDMC SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 FROM MDDY M,HYK_HYGRP_GZMX I where M.MDID=I.SJNR and I.SJLX in(27,35,38) ";
                query.SQL.Text += " and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX,I.SJNR,I.SJNR SJDM,B.BMMC SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 FROM SHBM B,HYK_HYGRP_GZMX I where B.BMDM=I.SJNR and I.SJLX=36";
                query.SQL.Text += " and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX,I.SJNR,I.SJNR SJDM,B.NR SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 FROM HYXXXMDEF B,HYK_HYGRP_GZMX I where B.XMID=I.SJNR and I.SJLX in(2,5) ";
                query.SQL.Text += " and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX,I.SJNR,I.SJNR SJDM,I.SJNR SJMC,I.SJNR2 SJMC2,I.SJLX SJMC3 FROM HYK_HYGRP_GZMX I where I.SJLX=4 ";
                query.SQL.Text += " and I.GRPID=" + iJLBH;

                query.SQL.Text += " UNION ";
                query.SQL.Text += "select I.GRPID,I.SJLX,I.SJNR,I.SJNR2 SJDM, B.LABELXMMC SJMC,A.LABEL_VALUE SJMC2 ,I.SJLX SJMC3 from HYK_HYGRP_GZMX  I,LABEL_XM B,LABEL_XMITEM A where I.SJNR=A.LABELID  ";
                query.SQL.Text += " and B.LABELXMID=A.LABELXMID  and  I.SJLX=40";
                query.SQL.Text += " and I.GRPID=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYXF_HYZDYItem obj = new HYXF_HYZDYItem();
                    obj.sSJNR = query.FieldByName("SJNR").AsString;
                    obj.sSJNR2 = query.FieldByName("SJDM").AsString;
                    obj.iSJLX = query.FieldByName("SJLX").AsInteger;
                    obj.sSJDM = query.FieldByName("SJDM").AsString;
                    obj.sSJMC = query.FieldByName("SJMC").AsString;
                    obj.sSJMC2 = query.FieldByName("SJMC2").AsString;
                    obj.sSJMC3 = query.FieldByName("SJMC3").AsInteger.ToString();
                    ((HYXF_HYZDY_Proc)lst[0]).itemTable.Add(obj);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_HYZDY_Proc obj = new HYXF_HYZDY_Proc();
            obj.iGRPID = query.FieldByName("GRPID").AsInteger;
            obj.iHYZLXID = query.FieldByName("HYZLXID").AsInteger;
            obj.sHYZLXMC = query.FieldByName("HYZLXMC").AsString;
            obj.sGRPMC = query.FieldByName("GRPMC").AsString;
            obj.sGRPMS = query.FieldByName("GRPMS").AsString;
            obj.iGXZQ = query.FieldByName("GXZQ").AsInteger;
            obj.iGRPYT = query.FieldByName("GRPYT").AsInteger;
            obj.dKSSJ = FormatUtils.DateToString(query.FieldByName("KSSJ").AsDateTime);
            obj.dJSSJ = FormatUtils.DateToString(query.FieldByName("JSSJ").AsDateTime);
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iZZR = query.FieldByName("ZZR").AsInteger;
            obj.sZZRMC = query.FieldByName("ZZRMC").AsString;
            obj.dZZRQ = FormatUtils.DatetimeToString(query.FieldByName("ZZRQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            obj.iSRFS = query.FieldByName("SRFS").AsInteger;
            obj.iGZFS = query.FieldByName("GZFS").AsInteger;
            this.iGZFS = obj.iGZFS;
            obj.iJB = query.FieldByName("JB").AsInteger;
            obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);
            obj.iBJ_DJT = query.FieldByName("BJ_DJT").AsInteger;
            obj.iBJ_YXXG = query.FieldByName("BJ_YXXG").AsInteger;
            obj.iXGR = query.FieldByName("XGR").AsInteger.ToString();
            obj.sXGRMC = query.FieldByName("XGRMC").AsString;
            obj.dXGRQ = FormatUtils.DatetimeToString(query.FieldByName("XGRQ").AsDateTime);
            obj.iBJ_WXHY = query.FieldByName("BJ_WXHY").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }

    }
}
