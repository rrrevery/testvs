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


namespace BF.CrmProc.KFPT
{
    public class KFPT_KFJLDY_Proc : DJLR_ZX_CLass
    {
        public int iKFRYID = 0;
        public string sRYMC = string.Empty;
        public string sRYDM = string.Empty;
        public string sPYM = string.Empty;

        public List<KFPT_HYK_KFDYJLITEM> KFDYJLITEM = new List<KFPT_HYK_KFDYJLITEM>();

        public class KFPT_HYK_KFDYJLITEM
        {
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public string sLXDH = string.Empty;
        }


        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_KFDYJL; HYK_KFDYJLITEM;", "JLBH", iJLBH);
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_KFDYJL", serverTime, "JLBH");
            foreach (KFPT_HYK_KFDYJLITEM one in KFDYJLITEM)
            {
                query.SQL.Text = "update HYK_HYXX set KFRYID = " + iKFRYID;
                query.SQL.AddLine(" where HYID in (select HYID from HYK_KFDYJLITEM where JLBH=" + iJLBH + " ) ");
                query.ExecSQL();
            }
        }


        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
            {
                iJLBH = SeqGenerator.GetSeq("HYK_KFDYJL");
            }

            query.SQL.Text = "insert into HYK_KFDYJL(JLBH,KFRYID,DJR,DJRMC,DJSJ,BZ)";
            query.SQL.Add(" values(:JLBH,:KFRYID,:DJR,:DJRMC,:DJSJ,:BZ)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("KFRYID").AsInteger = iKFRYID;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("BZ").AsString = sBZ;
            query.ExecSQL();
            foreach (KFPT_HYK_KFDYJLITEM one in KFDYJLITEM)
            {
                query.SQL.Text = "insert into HYK_KFDYJLITEM(JLBH,HYID)";
                query.SQL.Add(" select :JLBH, H.HYID from HYK_HYXX H where HYK_NO=:HYK_NO");
                //query.SQL.Add(" values(:JLBH,:HYID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                //query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("HYK_NO").AsString = one.sHYK_NO;
                query.ExecSQL();
            }
        }

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iJLBH", "L.JLBH");
            CondDict.Add("iKFRYID", "L.KFRYID");
            CondDict.Add("sBZ", "L.BZ");
            query.SQL.Text = "select L.*,R.PERSON_NAME,R.PYM,R.PERSON_NAME AS RYMC,R.RYDM from HYK_KFDYJL L,RYXX R where L.KFRYID=R.PERSON_ID ";
            if (iJLBH != 0)
                query.SQL.AddLine("  and L.JLBH=" + iJLBH);
            MakeSrchCondition(query);
            query.Open();
            while (!query.Eof)
            {
                KFPT_KFJLDY_Proc obj = new KFPT_KFJLDY_Proc();
                obj.iJLBH = query.FieldByName("JLBH").AsInteger;
                obj.iKFRYID = query.FieldByName("KFRYID").AsInteger;
                obj.sRYMC = query.FieldByName("RYMC").AsString;
                obj.sRYDM = query.FieldByName("RYDM").AsString;
                obj.sBZ = query.FieldByName("BZ").AsString;
                obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
                obj.sDJRMC = query.FieldByName("DJRMC").AsString;
                obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
                obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
                obj.sPYM = query.FieldByName("PYM").AsString;
                lst.Add(obj);
                query.Next();
            }
            query.Close();

            {
                query.SQL.Text = "select M.*,X.HYK_NO,X.HY_NAME,G.SJHM as LXDH from HYK_KFDYJLITEM M,HYK_HYXX X,HYK_GRXX G  where M.HYID=X.HYID and M.HYID=G.HYID(+) and M.JLBH=" + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    KFPT_HYK_KFDYJLITEM obj = new KFPT_HYK_KFDYJLITEM();

                    ((KFPT_KFJLDY_Proc)lst[0]).KFDYJLITEM.Add(obj);
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    obj.sLXDH = query.FieldByName("LXDH").AsString;
                    query.Next();
                }
                query.Close();
            }

            return lst;
        }
    }
}
