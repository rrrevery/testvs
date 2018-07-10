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
using System.Collections;

namespace BF.CrmProc.GTPT
{
    public class GTPT_JFDHLPGZDY_Proc : BASECRMClass
    {
        public int iGZID
        {
            set { iJLBH = value; }
            get { return iJLBH; }
        }
        public string sGZMC = string.Empty;
        public List<JFDHLPGZDY_KLX> itemKLX = new List<JFDHLPGZDY_KLX>();
        public class JFDHLPGZDY_KLX
        {
            public int iGZID = 0;
            public int iLPID = 0;
            public string sLPMC = string.Empty;
            public int iHYKTYPE = 0;
            public string sHYKNAME = string.Empty;
            public double fLPJF = 0;
        }
        public List<JFDHLPGZDY_LP> itemLP = new List<JFDHLPGZDY_LP>();
        public class JFDHLPGZDY_LP
        {
            public int iGZID = 0;
            public int iLPID = 0;
            public string sLPMC = string.Empty;
            public string sWXZY = string.Empty;
            public int iXZCS_HY = 0;
            public int iXZCS_DAY_HY = 0;
            public int iXZCS = 0;
            public int iXZCS_DAY = 0;
            public string sXZCS_HY_TS = string.Empty;
            public string sXZCS_DAY_HY_TS = string.Empty;
            public string sXZCS_TS = string.Empty;
            public string sXZCS_DAY_TS = string.Empty;
        }
        public List<JFDHLPGZDY_OLD> itemJLBH = new List<JFDHLPGZDY_OLD>();
        public class JFDHLPGZDY_OLD
        {
            public int iJLBH_OLD = 0;
        }

    
     
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "WX_JFFLLPGZ;WX_JFFLLPGZ_LP_HYK;WX_JFFLLPGZ_LP", "GZID", iJLBH);
        }
        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            int iModify = 0;
            int iJYBH = 0;
            if (iJLBH != 0)
            {
                iModify = 1;
                //写变动记录表并且将正在启用的启动规则复制单据重做,记录老的
                iJYBH = SeqGenerator.GetSeq("WX_JFFLLPGZ_BDJL");
                query.SQL.Text = "insert into WX_JFFLLPGZ_BDJL(JYBH,GZID,GZMC,BDR,BDRMC,BDSJ)";
                query.SQL.Add("select :JYBH,GZID,GZMC,:BDR,:BDRMC,:BDSJ");
                query.SQL.Add("from WX_JFFLLPGZ");
                query.SQL.Add("where GZID=:GZID");
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("BDR").AsInteger = iLoginRYID;
                query.ParamByName("BDSJ").AsDateTime = serverTime;
               
                    query.ParamByName("BDRMC").AsString = sLoginRYMC;
                
               
                query.ExecSQL();

                query.SQL.Text = "insert into WX_JFFLLPGZ_LP_BDJL(JYBH,GZID,LPID,WXZY,XZCS_HY,XZCS_DAY_HY,XZCS,XZCS_DAY,XZCS_HY_TS,XZCS_DAY_HY_TS,XZCS_TS,XZCS_DAY_TS)";
                query.SQL.Add("select :JYBH,GZID,LPID,WXZY,XZCS_HY,XZCS_DAY_HY,XZCS,XZCS_DAY,XZCS_HY_TS,XZCS_DAY_HY_TS,XZCS_TS,XZCS_DAY_TS");
                query.SQL.Add("from WX_JFFLLPGZ_LP");
                query.SQL.Add("where GZID=:GZID");
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                query.SQL.Text = "insert into WX_JFFLLPGZ_LP_HYK_BDJL(JYBH,GZID,LPID,HYKTYPE,LPJF)";
                query.SQL.Add("select :JYBH,GZID,LPID,HYKTYPE,LPJF");
                query.SQL.Add("from WX_JFFLLPGZ_LP_HYK");
                query.SQL.Add("where GZID=:GZID");
                query.ParamByName("JYBH").AsInteger = iJYBH;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ExecSQL();

                DeleteDataQuery(out msg, query, serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("WX_JFFLLPGZ");

            query.SQL.Text = "insert into WX_JFFLLPGZ(GZID,GZMC)";
            query.SQL.Add("values(:GZID,:GZMC)");
            query.ParamByName("GZID").AsInteger = iJLBH;
        
                query.ParamByName("GZMC").AsString = sGZMC;
            
          
            query.ExecSQL();

            foreach (JFDHLPGZDY_LP one in itemLP)
            {
                query.SQL.Text = "insert into WX_JFFLLPGZ_LP(GZID,LPID,WXZY,XZCS_HY,XZCS_DAY_HY,XZCS,XZCS_DAY,XZCS_HY_TS,XZCS_DAY_HY_TS,XZCS_TS,XZCS_DAY_TS)";
                query.SQL.Add("values(:GZID,:LPID,:WXZY,:XZCS_HY,:XZCS_DAY_HY,:XZCS,:XZCS_DAY,:XZCS_HY_TS,:XZCS_DAY_HY_TS,:XZCS_TS,:XZCS_DAY_TS)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = one.iLPID;
                query.ParamByName("XZCS_HY").AsInteger = one.iXZCS_HY;
                query.ParamByName("XZCS_DAY_HY").AsInteger = one.iXZCS_DAY_HY;
                query.ParamByName("XZCS").AsInteger = one.iXZCS;
                query.ParamByName("XZCS_DAY").AsInteger = one.iXZCS_DAY;
             
                    query.ParamByName("WXZY").AsString = one.sWXZY;
                    query.ParamByName("XZCS_HY_TS").AsString = one.sXZCS_HY_TS;
                    query.ParamByName("XZCS_DAY_HY_TS").AsString = one.sXZCS_DAY_HY_TS;
                    query.ParamByName("XZCS_TS").AsString = one.sXZCS_TS;
                    query.ParamByName("XZCS_DAY_TS").AsString = one.sXZCS_DAY_TS;
                
              
                query.ExecSQL();
            }

            foreach (JFDHLPGZDY_KLX ones in itemKLX)
            {
                query.SQL.Text = "insert into WX_JFFLLPGZ_LP_HYK(GZID,LPID,HYKTYPE,LPJF)";
                query.SQL.Add("values(:GZID,:LPID,:HYKTYPE,:LPJF)");
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.ParamByName("LPID").AsInteger = ones.iLPID;
                query.ParamByName("HYKTYPE").AsInteger = ones.iHYKTYPE;
                query.ParamByName("LPJF").AsFloat = ones.fLPJF;
                query.ExecSQL();
            }
            if (iModify == 1)
            {
                //判断是否有正在启动的规则，有的话终止复制重启              
                query.SQL.Text = "select JLBH from WX_JFFLLPDYD where STATUS=2 and DJLX=2 and KSRQ<=:RQ and JSRQ>=:RQ and GZID=:GZID";
                query.ParamByName("RQ").AsDateTime = serverTime.Date;
                query.ParamByName("GZID").AsInteger = iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    JFDHLPGZDY_OLD item = new JFDHLPGZDY_OLD();
                    itemJLBH.Add(item);
                    item.iJLBH_OLD = query.FieldByName("JLBH").AsInteger;
                    query.Next();
                }
                query.Close();

                foreach (JFDHLPGZDY_OLD old in itemJLBH)
                {
                    query.SQL.Text = "update WX_JFFLLPDYD set STATUS=3,ZZR=:ZZR,ZZRMC=:ZZRMC,ZZRQ=:ZZRQ where JLBH=:JLBH";
                    query.ParamByName("ZZR").AsInteger = iLoginRYID;
                    query.ParamByName("ZZRMC").AsString = sLoginRYMC;
                    query.ParamByName("ZZRQ").AsDateTime = serverTime;
                    query.ParamByName("JLBH").AsInteger = old.iJLBH_OLD;
                    query.ExecSQL();

                    int iJLBH_NEW = SeqGenerator.GetSeq("WX_JFFLLPDYD");
                    query.SQL.Text = "insert into WX_JFFLLPDYD(JLBH,PUBLICID,DJLX,GZID,KSRQ,JSRQ,CLFS,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,QDR,QDRMC,QDSJ,BZ,MDID,LJYXQ)";
                    query.SQL.Add("select :JLBH,PUBLICID,DJLX,GZID,KSRQ,JSRQ,CLFS,DJR,DJRMC,DJSJ,ZXR,ZXRMC,ZXRQ,QDR,QDRMC,QDSJ,BZ,MDID,LJYXQ");
                    query.SQL.Add("from WX_JFFLLPDYD");
                    query.SQL.Add("where JLBH=:JLBH_OLD");
                    query.ParamByName("JLBH_OLD").AsInteger = old.iJLBH_OLD;
                    query.ParamByName("JLBH").AsInteger = iJLBH_NEW;
                    query.ExecSQL();
                }
            }
        }
        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {


            CondDict.Add("iJLBH", "W.GZID");
            List<Object> lst = new List<Object>();
            query.SQL.Text = "select W.* from WX_JFFLLPGZ W where 1=1";
            SetSearchQuery(query, lst);        
            if (lst.Count == 1)
            {
                {
                    query.SQL.Text = "select I.*,L.LPMC FROM WX_JFFLLPGZ_LP I,HYK_JFFHLPXX L";
                    query.SQL.Add("   where I.LPID=L.LPID");
                    if (iJLBH != 0)
                        query.SQL.Add("  and I.GZID=" + iJLBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        JFDHLPGZDY_LP item = new JFDHLPGZDY_LP();
                        ((GTPT_JFDHLPGZDY_Proc)lst[0]).itemLP.Add(item);
                        item.iGZID = query.FieldByName("GZID").AsInteger;
                        item.iLPID = query.FieldByName("LPID").AsInteger;
                        item.iXZCS_HY = query.FieldByName("XZCS_HY").AsInteger;
                        item.iXZCS_DAY_HY = query.FieldByName("XZCS_DAY_HY").AsInteger;
                        item.iXZCS = query.FieldByName("XZCS").AsInteger;
                        item.iXZCS_DAY = query.FieldByName("XZCS_DAY").AsInteger; 
                      
                            item.sLPMC = query.FieldByName("LPMC").AsString;
                            item.sWXZY = query.FieldByName("WXZY").AsString;
                            item.sXZCS_HY_TS = query.FieldByName("XZCS_HY_TS").AsString;
                            item.sXZCS_DAY_HY_TS = query.FieldByName("XZCS_DAY_HY_TS").AsString;
                            item.sXZCS_TS = query.FieldByName("XZCS_TS").AsString;
                            item.sXZCS_DAY_TS = query.FieldByName("XZCS_DAY_TS").AsString;
                        
                      
                        query.Next();
                    }

                    query.SQL.Text = "select I.*,L.LPMC,F.HYKNAME from WX_JFFLLPGZ_LP_HYK I,HYK_JFFHLPXX L,HYKDEF F";
                    query.SQL.Add("   where I.LPID=L.LPID and I.HYKTYPE=F.HYKTYPE");
                    if (iJLBH != 0)
                        query.SQL.Add("  and I.GZID=" + iJLBH);
                    query.Open();
                    while (!query.Eof)
                    {
                        
                        JFDHLPGZDY_KLX item1 = new JFDHLPGZDY_KLX();
                        ((GTPT_JFDHLPGZDY_Proc)lst[0]).itemKLX.Add(item1);
                        item1.iGZID = query.FieldByName("GZID").AsInteger;
                        item1.iLPID = query.FieldByName("LPID").AsInteger;
                        item1.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                        item1.fLPJF = query.FieldByName("LPJF").AsFloat;
                    
                            item1.sLPMC = query.FieldByName("LPMC").AsString;
                            item1.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        
                     
                        query.Next();
                    }
                }
            }
            query.Close();
            return lst;

        }


        public override object SetSearchData(CyQuery query)
        {


            GTPT_JFDHLPGZDY_Proc obj = new GTPT_JFDHLPGZDY_Proc();
            obj.iJLBH = query.FieldByName("GZID").AsInteger;

            obj.sGZMC = query.FieldByName("GZMC").AsString;



            return obj;
        }
    }
}
