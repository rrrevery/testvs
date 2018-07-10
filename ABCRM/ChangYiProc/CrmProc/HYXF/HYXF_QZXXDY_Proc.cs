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
    public class HYXF_QZXXDY_Proc : DJLR_ZX_CLass
    {
        public string sQZMC = string.Empty;
        public int iQZLXID = 0;
        public string sQZLXMC = string.Empty;
        public int iQZCYRS = 0;
        public int iMDID = 0;
        public string sMDMC = string.Empty;
        public List<HYK_QZXX_CYXXITEM> CYXXItemTable = new List<HYK_QZXX_CYXXITEM>();
        public List<HYK_QZXX_BMPLITEM> BMPLItemTable = new List<HYK_QZXX_BMPLITEM>();//可选择放在HYK_QZXX_CYXXITEM 类中
        public class HYK_QZXX_CYXXITEM
        {
            public int iJLBH = 0;
            public int iHYID = 0;
            public string sHYK_NO = string.Empty;
            public string sHY_NAME = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public string dYXQ = string.Empty;

        }
        public class HYK_QZXX_BMPLITEM
        {

            public int iJLBH = 0;
            public int iHYID = 0;
            public int iSJLX = 0;
            public int iSJNR = 0;
            public string sSJMC = string.Empty;
            //SJLX=1 SJNR为BMDM（部门代码）  SJLX=2  SJNR为SBDM（商标代码）
        }
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYK_QZXX;HYK_QZXX_CYXXITEM;HYK_QZXX_BMPLITEM;", "JLBH", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {

            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("HYK_QZXX");
            query.SQL.Text = "insert into HYK_QZXX(JLBH,QZMC,QZLXID,QZCYRS,DJR,DJRMC,DJSJ,MDID,STATUS) ";
            query.SQL.Add(" values(:JLBH,:QZMC,:QZLXID,:QZCYRS,:DJR,:DJRMC,:DJSJ,:MDID,0)");
            query.ParamByName("JLBH").AsInteger = iJLBH;
            query.ParamByName("QZMC").AsString = sQZMC;
            query.ParamByName("QZLXID").AsInteger = iQZLXID;
            query.ParamByName("QZCYRS").AsInteger = iQZCYRS;
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();

            foreach (HYK_QZXX_CYXXITEM one in CYXXItemTable)
            {
                query.SQL.Text = "insert into HYK_QZXX_CYXXITEM(JLBH,HYID)";
                query.SQL.Add(" values(:JLBH,:HYID)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ExecSQL();
            }

            foreach (HYK_QZXX_BMPLITEM one in BMPLItemTable)
            {
                query.SQL.Text = "insert into HYK_QZXX_BMPLITEM(JLBH,HYID,SJLX,SJNR)";
                query.SQL.Add(" values(:JLBH,:HYID,:SJLX,:SJNR)");
                query.ParamByName("JLBH").AsInteger = iJLBH;
                query.ParamByName("HYID").AsInteger = one.iHYID;
                query.ParamByName("SJLX").AsInteger = one.iSJLX;
                query.ParamByName("SJNR").AsInteger = one.iSJNR;
                query.ExecSQL();
            }
        }

        public static bool CheckUniqueCustomer(int iCirleNumber, int iMemberId)
        {
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            bool BoolInsert = true;
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "  select * from HYK_QZXX Q ,HYK_QZXX_CYXXITEM I where Q.JLBH=I.JLBH and Q.QZLXID=:QZLXID and I.HYID=:HYID";
                    query.ParamByName("QZLXID").AsInteger = iCirleNumber;
                    query.ParamByName("HYID").AsInteger = iMemberId;
                    query.Open();
                    if (!query.IsEmpty)
                    {
                        BoolInsert = false;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is MyDbException)
                        throw ex;
                    else
                        throw new MyDbException(ex.Message, query.SqlText);
                }

            }
            finally
            {
                conn.Close();
            }
            return BoolInsert;
        }


        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();

            CondDict.Add("iJLBH", "W.JLBH");
            CondDict.Add("iDJR", "W.DJR");
            CondDict.Add("iZXR", "W.ZXR");
            CondDict.Add("iQZLX", "W.QZLXID");
            CondDict.Add("iQZRS", "W.QZCYRS");
            CondDict.Add("iMDID", "W.MDID");

            query.SQL.Text = "select W.*,A.QZLXMC,B.MDMC from HYK_QZXX W,HYK_QZLXDEF A,MDDY B ";
            query.SQL.Text += " where W.QZLXID=A.JLBH and W.MDID=B.MDID ";
            SetSearchQuery(query, lst);

            if (lst.Count == 1)
            {
                //成员信息
                query.SQL.Text = " select I.*,A.HY_NAME,A.HYK_NO,B.HYKNAME from  HYK_QZXX_CYXXITEM I,HYK_HYXX A,HYKDEF B ";
                query.SQL.Text += " where I.HYID=A.HYID and A.HYKTYPE=B.HYKTYPE and I.JLBH= " + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYK_QZXX_CYXXITEM obj = new HYK_QZXX_CYXXITEM();
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                    obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                    obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    //obj.dYXQ = FormatUtils.DateToString(query.FieldByName("YXQ").AsDateTime);

                    ((HYXF_QZXXDY_Proc)lst[0]).CYXXItemTable.Add(obj);
                    query.Next();
                }
                query.Close();

                //各成员喜爱的部门及品牌信息   
                //select I.*,A.BMMC SJMC from HYK_QZXX_BMPLITEM I,SHBM A
                //where I.SJLX=1 and I.SJNR=A.BMDM    
                //union
                //select I.*,A.SBMC SJMC from HYK_QZXX_BMPLITEM I,SHSPSB A
                //where I.SJLX=2 and I.SJNR=A.SBDM
                query.SQL.Text = "  select I.*,A.BMMC SJMC from HYK_QZXX_BMPLITEM I,SHBM A ";
                query.SQL.Text += " where I.SJLX=1 and I.SJNR=A.SHBMID and I.JLBH= " + iJLBH;
                query.SQL.Text += " union ";
                query.SQL.Text += " select I.*,A.SBMC SJMC from HYK_QZXX_BMPLITEM I,SHSPSB A ";
                query.SQL.Text += " where I.SJLX=2 and I.SJNR=A.SHSBID and I.JLBH= " + iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    HYK_QZXX_BMPLITEM obj = new HYK_QZXX_BMPLITEM();
                    obj.iHYID = query.FieldByName("HYID").AsInteger;
                    obj.iSJLX = query.FieldByName("SJLX").AsInteger;
                    obj.iSJNR = query.FieldByName("SJNR").AsInteger;
                    obj.sSJMC = query.FieldByName("SJMC").AsString;

                    ((HYXF_QZXXDY_Proc)lst[0]).BMPLItemTable.Add(obj);
                    query.Next();
                }
                query.Close();
            }
            return lst;
        }

        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            ExecTable(query, "HYK_QZXX", serverTime, "JLBH", "ZXR", "ZXRMC", "ZXRQ",1);
        }

        public override void StopDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            query.SQL.Clear();
            query.SQL.Text = "  Update  HYK_QZXX set STATUS=2 where JLBH=:JLBH ";
            query.ParamByName("JLBH").AsInteger = iJLBH;
            if (query.ExecSQL() != 1)
            {
                msg = "作废失败";
            }

        }
        public override object SetSearchData(CyQuery query)
        {
            HYXF_QZXXDY_Proc obj = new HYXF_QZXXDY_Proc();
            obj.iJLBH = query.FieldByName("JLBH").AsInteger;
            obj.iQZLXID = query.FieldByName("QZLXID").AsInteger;
            obj.sQZLXMC = query.FieldByName("QZLXMC").AsString;
            obj.sQZMC = query.FieldByName("QZMC").AsString;
            obj.iQZCYRS = query.FieldByName("QZCYRS").AsInteger;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.sMDMC = query.FieldByName("MDMC").AsString;

            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DateToString(query.FieldByName("ZXRQ").AsDateTime);
            obj.iSTATUS = query.FieldByName("STATUS").AsInteger;
            return obj;

        }
    }
}
