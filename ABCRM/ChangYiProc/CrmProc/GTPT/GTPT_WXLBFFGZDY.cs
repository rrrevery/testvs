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


namespace BF.CrmProc.GTPT
{
    public class GTPT_WXLBFFGZDY : DJLR_CLass
    {
        public int iGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }

        public string sXZTS = string.Empty;
        public string sXZTS_HY = string.Empty;
        public string sXZTS_DAY_HY = string.Empty;
        public string sGZLXMC = string.Empty;
        public int iGZLD = 0;
        public int iGZLX = 0;
        public int iCHANNELID = 0;
        public string sGZMC = string.Empty;
        public int iXZCS = 0;
        public int iXZCS_HY = 0;
        public int iXZCS_DAY_HY = 0;
        public int status = 0;

        public List<MOBILE_LBFFGZ_MX> itemTable = new List<MOBILE_LBFFGZ_MX>();
        public List<MOBILE_LBFFGZ_ITEM> itemTableJD = new List<MOBILE_LBFFGZ_ITEM>();

        public class MOBILE_LBFFGZ_MX
        {
            public int iGZID = 0;
            public int iXH = 0;
            public int iLBID = 0;
            public int iLPSL = 0;
            public int iFFZSL = 0;
            public string sWXZY = string.Empty;
            public string sLBMC = string.Empty;
            public string sXHMC = string.Empty;
            public int iJC = 0;
            public string sMC = string.Empty;

        }
        public class MOBILE_LBFFGZ_ITEM
        {
            public int iGZID = 0;
            public int iXH = 0;
            public int iVAL = 0;
            public string sXHMC = string.Empty;
            public int iJC = 0;
            public string sMC = string.Empty;
        }

        public override bool IsValidDeleteData(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (status == 0)
            {
                query.SQL.Text = "select count(A.JLBH) as Q from MOBILE_LPFFGZDYD A where A.GZID=" + iJLBH;
                query.Open();
                int count = query.FieldByName("Q").AsInteger;
                if (count > 0)
                {
                    msg = "此规则存在已定义单据，禁止删除！";
                    return false;
                }
            }
            return true;
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataTables(query, out msg, "MOBILE_LPFFGZ;MOBILE_LPFFGZ_LP;MOBILE_LPFFGZ_LP_ITEM", "GZID", iJLBH);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);


            msg = string.Empty;
            if (iJLBH != 0)
            {

                //修改规则时，要写变动记录
                int iJYBH = SeqGenerator.GetSeq("MOBILE_LPFFGZBDJL");
                query.SQL.Text = "insert into MOBILE_LPFFGZBDJL(JYBH,GZID_BD,BDSJ,BDR,BDRMC)";//,MACHINE,IP_ADDRESS
                query.SQL.Add(" values(:JYBH,:GZID,:BDSJ,:BDR,:BDRMC)");//,:MACHINE,:IP_ADDRESS
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("BDR").AsInteger = iLoginRYID;
                query.ParamByName("BDSJ").AsDateTime = serverTime;
                query.ParamByName("BDRMC").AsString = sLoginRYMC;
                query.ExecSQL();

                query.SQL.Text = " insert into MOBILE_LPFFGZNRBDJL select :JYBH,GZID,GZMC,XZCS_HY,XZTS_HY,XZCS,XZTS,XZCS_DAY_HY,XZTS_DAY_HY,DJR,DJRMC,DJSJ,GZLX,CHANNELID from MOBILE_LPFFGZ where GZID=:GZID";
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                query.SQL.Text = " insert into MOBILE_LPFFGZ_LPNRBDJL select :JYBH,GZID,XH,LBID,FFZSL,WXZY from MOBILE_LPFFGZ_LP where GZID=:GZID";
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();


                query.SQL.Text = " insert into MOBILE_LPFFGZ_LP_ITEM_NRBDJL select :JYBH,GZID,XH,VAL from MOBILE_LPFFGZ_LP_ITEM where GZID=:GZID";
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();
                int billJLBH = 0;
                query.SQL.Text = "select JLBH from MOBILE_LPFFGZDYD where STATUS=2 and GZID=" + iJLBH;
                query.Open();
                if (!query.IsEmpty)
                {
                    billJLBH = query.FieldByName("JLBH").AsInteger;
                }
                if (billJLBH > 0)
                {
                    query.SQL.Text = "update MOBILE_LPFFGZDYD set ZZR=:ZZR,ZZSJ=" + CrmLibProc.GetDbSystemField(DbSystemName, 0) + ",STATUS=3,ZZRMC=:ZZRMC where STATUS=2 and JLBH=" + billJLBH;
                    query.ParamByName("ZZR").AsInteger = iLoginRYID;
                    query.ParamByName("ZZRMC").AsString = sLoginRYMC;
                    query.ExecSQL();
                    int NewBillId = SeqGenerator.GetSeq("MOBILE_LPFFGZDYD");
                    query.SQL.Text = "insert into MOBILE_LPFFGZDYD(JLBH,GZID,DJLX,KSRQ,JSRQ,DJSJ,DJR,DJRMC,ZXR,ZXRMC,ZXRQ,QDR,QDRMC,QDSJ,STATUS,LJYXQ,PUBLICID)";
                    query.SQL.Add(" select :JLBH,GZID,DJLX,KSRQ,JSRQ,:DJSJ,:DJR,:DJRMC,:ZXR,:ZXRMC,:ZXRQ,:QDR,:QDRMC,:QDSJ,2,LJYXQ,PUBLICID");
                    query.SQL.Add(" from MOBILE_LPFFGZDYD where JLBH=").Add(billJLBH);
                    query.ParamByName("JLBH").AsInteger = NewBillId;
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ParamByName("ZXR").AsInteger = iLoginRYID;
                    query.ParamByName("ZXRQ").AsDateTime = serverTime;
                    query.ParamByName("QDR").AsInteger = iLoginRYID;
                    query.ParamByName("QDSJ").AsDateTime = serverTime;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("ZXRMC").AsString = sLoginRYMC;
                    query.ParamByName("QDRMC").AsString = sLoginRYMC;
                    query.ExecSQL();

                    query.SQL.Text = "insert into MOBILE_LPFFGZDYDITEM(JLBH,RQ,XZSL,WXTS)";
                    query.SQL.Add(" select :JLBH,RQ,XZSL,WXTS from MOBILE_LPFFGZDYDITEM where JLBH=").Add(billJLBH);
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("JLBH").AsInteger = NewBillId;
                    query.ExecSQL();
                }
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MOBILE_LPFFGZ");

            query.SQL.Text = "insert into MOBILE_LPFFGZ(GZID,GZMC,XZCS_HY,XZTS_HY,XZCS,XZTS,XZCS_DAY_HY,XZTS_DAY_HY,GZLX,DJR,DJRMC,DJSJ,CHANNELID)";
            query.SQL.Add(" values(:GZID,:GZMC,:XZCS_HY,:XZTS_HY,:XZCS,:XZTS,:XZCS_DAY_HY,:XZTS_DAY_HY,:GZLX,:DJR,:DJRMC,:DJSJ,:CHANNELID)");
            query.ParamByName("GZID").AsInteger = iJLBH;
            query.ParamByName("XZCS_HY").AsInteger = iXZCS_HY;
            query.ParamByName("XZCS").AsInteger = iXZCS;
            query.ParamByName("XZCS_DAY_HY").AsInteger = iXZCS_DAY_HY;
            query.ParamByName("GZLX").AsInteger = iGZLX;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("CHANNELID").AsInteger = iCHANNELID;

            query.ParamByName("GZMC").AsString = sGZMC;
            query.ParamByName("XZTS").AsString = sXZTS;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("XZTS_HY").AsString = sXZTS_HY;
            query.ParamByName("XZTS_DAY_HY").AsString = sXZTS_DAY_HY;


            query.ExecSQL();

            foreach (MOBILE_LBFFGZ_MX one in itemTable)
            {
                query.SQL.Text = "insert into MOBILE_LPFFGZ_LP(GZID,XH,LBID,FFZSL,WXZY)";
                query.SQL.Add(" values(:GZID,:XH,:LBID,:FFZSL,:WXZY)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = one.iJC;
                query.ParamByName("LBID").AsInteger = one.iLBID;
                query.ParamByName("FFZSL").AsInteger = one.iFFZSL;
                query.ParamByName("WXZY").AsString = one.sWXZY;


                query.ExecSQL();
            }

            foreach (MOBILE_LBFFGZ_ITEM ones in itemTableJD)
            {
                query.SQL.Text = "insert into MOBILE_LPFFGZ_LP_ITEM(GZID,XH,VAL)";
                query.SQL.Add(" values(:GZID,:XH,:VAL)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("XH").AsInteger = ones.iJC;
                query.ParamByName("VAL").AsInteger = ones.iVAL;
                query.ExecSQL();
            }
        }



        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();


            CondDict.Add("iJLBH", "GZID");

            CondDict.Add("sGZLXMC", "GZLX");

            string DbSystemName = CyDbSystem.GetDbSystemName(query.Connection);

            query.SQL.Text = "select * from  MOBILE_LPFFGZ";
            query.SQL.Add("where GZID is not null");
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                query.SQL.Text = "select M.*,D.LBMC,A.MC JCMC from  MOBILE_LPFFGZ_LP M,MOBILE_LPZDEF_YHQ D,MOBILE_JCDEF A";
                query.SQL.Add("  where M.LBID=D.LBID AND M.XH=A.JC");
                if (iJLBH != 0)
                    query.SQL.Add("  and GZID=" + iJLBH);
                query.Open();
                while (!query.Eof)
                {
                    MOBILE_LBFFGZ_MX item = new MOBILE_LBFFGZ_MX();
                    ((GTPT_WXLBFFGZDY)lst[0]).itemTable.Add(item);
                    item.iGZID = query.FieldByName("GZID").AsInteger;
                    item.iJC = query.FieldByName("XH").AsInteger;
                    item.iLBID = query.FieldByName("LBID").AsInteger;
                    item.iFFZSL = query.FieldByName("FFZSL").AsInteger;
                    item.sLBMC = query.FieldByName("LBMC").AsString;
                    item.sMC = query.FieldByName("JCMC").AsString;
                    item.sWXZY = query.FieldByName("WXZY").AsString;
                    query.Next();
                }
                query.Close();

                if (((GTPT_WXLBFFGZDY)lst[0]).itemTable.Count > 0)
                {
                    query.SQL.Text = "select B.* ,A.MC JCMC from MOBILE_LPFFGZ_LP_ITEM B ,MOBILE_JCDEF A ";
                    query.SQL.Add(" where B.XH=A.JC and GZID is not null");
                    if (iJLBH != 0)
                        query.SQL.Add("  and GZID=" + iJLBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        MOBILE_LBFFGZ_ITEM item2 = new MOBILE_LBFFGZ_ITEM();
                        ((GTPT_WXLBFFGZDY)lst[0]).itemTableJD.Add(item2);
                        item2.iVAL = query.FieldByName("VAL").AsInteger;
                        item2.iJC = query.FieldByName("XH").AsInteger;
                        item2.sMC = query.FieldByName("JCMC").AsString;
                        query.Next();
                    }
                }
                query.Close();

            }

            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            GTPT_WXLBFFGZDY item = new GTPT_WXLBFFGZDY();
            item.iJLBH = query.FieldByName("GZID").AsInteger;
            item.iGZLX = query.FieldByName("GZLX").AsInteger;
            item.iCHANNELID = query.FieldByName("CHANNELID").AsInteger;
            if (item.iGZLX == 2)
            {
                item.sGZLXMC = "抽奖";

            }
            if (item.iGZLX == 1)
            {
                item.sGZLXMC = "抢红包";

            }
            if (item.iGZLX == 3)
            {
                item.sGZLXMC = "刮刮卡";

            }
            item.iDJR = query.FieldByName("DJR").AsInteger;
            item.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            item.iXZCS_HY = query.FieldByName("XZCS_HY").AsInteger;
            item.iXZCS = query.FieldByName("XZCS").AsInteger;
            item.sGZMC = query.FieldByName("GZMC").AsString;
            item.sDJRMC = query.FieldByName("DJRMC").AsString;
            item.sXZTS = query.FieldByName("XZTS").AsString;
            item.sXZTS_HY = query.FieldByName("XZTS_HY").AsString;
            item.sXZTS_DAY_HY = query.FieldByName("XZTS_DAY_HY").AsString;
            item.iXZCS_DAY_HY = query.FieldByName("XZCS_DAY_HY").AsInteger;

            return item;
        }
    }
}
