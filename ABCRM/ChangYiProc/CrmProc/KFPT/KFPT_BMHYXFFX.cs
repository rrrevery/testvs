using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections.Generic;
using System;

namespace BF.CrmProc.KFPT
{
    public class KFPT_BMHYXFFX : BASECRMClass
    {
        public string dRQ = string.Empty;
        public int aa = 0;
        public int iSEX = 0;
        public int iHYKTYPE = 0;
        public int iHYID = 0;
        public int iMDID = 0;
        public double fXFJE = 0;
        public string sHY_NAME = string.Empty;
        public string sKEJLMC = string.Empty;
        public string sBMMC = string.Empty;
        public string sSHMC = string.Empty;
        public string sHYK_NO = string.Empty;
        public string sTXDZ = string.Empty;
        public string sSJHM = string.Empty;
        public string sKFJLMC = string.Empty;
        public string sBMDM = string.Empty;
        public string sSHDM = string.Empty;
        public string sHYKNAME = string.Empty;
        public string[] asFieldNames = { 
                                            
                                            "iSEX;G.SEX",
                                             "sHY_NAME;H.HY_NAME",
                                              "sBMMC;M.BMMC",
                                             "sKFJLMC;R.sKFJLMC",
                                            "sHYK_NO;H.HYK_NO",
                                            "sSHMC;S.SHMC", 
                                            "iHYKTYPE;H.HYKTYPE", 
                                                 "sHYKNAME;D.HYKNAME", 
                                           "sTXDZ;G.TXDZ" ,
                                                 "dRQ;X.RQ" ,
                                           "sSJHM;G.SJHM" ,
                                            //"iBMJC;X.BMJC",
                                           
                                       };



        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            string msg = "";
            Boolean flag = false;

            //string sRQ = "and RQ>=:RQ1 and RQ<=:RQ2";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            try
            {

                try
                {
                    query.SQL.Text = "select 1 from GKXX_CXQX where PERSON_ID=:PERSON_ID  ";
                    query.ParamByName("PERSON_ID").AsInteger = iLoginRYID;
                    query.Open();

                    if (!query.IsEmpty)
                    {
                        flag = true;
                    }
                    CondDict.Add("iSEX", "G.SEX");
                    CondDict.Add("sHY_NAME", "H.HY_NAME");
                    CondDict.Add("sBMMC", "M.BMMC");
                    CondDict.Add("sKFJLMC", "R.KFJLMC");
                    CondDict.Add("sHYK_NO", "H.HYK_NO");
                    CondDict.Add("sSHMC", "S.SHMC");
                    CondDict.Add("iHYKTYPE", "H.HYKTYPE");
                    CondDict.Add("sHYKNAME", "D.HYKNAME");
                    CondDict.Add("sTXDZ", "G.TXDZ");
                    CondDict.Add("dRQ", "X.RQ");
                    CondDict.Add("sSJHM", "G.SJHM");
                    query.SQL.Text = "select S.SHMC,H.HYID,H.HYK_NO,H.HY_NAME,M.BMMC,R.PERSON_NAME as KFJLMC,G.SEX,G.SJHM,G.TXDZ,sum(X.XSJE) as XFJE ,D.HYKNAME ,X.RQ ";
                    query.SQL.Text += "  from HYK_XFMX X,HYK_HYXX H,SHBM M,MDDY Y,RYXX R,HYK_GRXX G,SHDY S,HYKDEF D ";
                    query.SQL.Text += "  where X.HYID=H.HYID and substr(X.DEPTID,1,:JC)=M.BMDM and X.MDID=Y.MDID and Y.SHDM=M.SHDM and Y.SHDM=S.SHDM ";
                    query.SQL.Text += "  and H.KFRYID=R.PERSON_ID ";
                    query.SQL.Text += " and H.HYID=G.HYID ";
                    query.SQL.Text += " and D.HYKTYPE=H.HYKTYPE ";
                    //query.SQL.Text += " and RQ>=:RQ1 and RQ<=:RQ2 ";
                    if (!flag)
                    { query.SQL.Add(" and H.KFRYID=" + iLoginRYID); }

                    MakeSrchCondition(query, "group by S.SHMC,H.HYID,H.HYK_NO,H.HY_NAME,M.BMMC,R.PERSON_NAME,G.SEX,G.SJHM,G.TXDZ,D.HYKNAME,X.RQ");
                    //query.SQL.Text += " group by S.SHMC,H.HYID,H.HYK_NO,H.HY_NAME,M.BMMC,R.PERSON_NAME,G.SEX,G.SJHM,G.TXDZ ";
                    //query.ParamByName("RQ1").AsDateTime = FormatUtils.ParseDatetimeString(sRQ1);     //    Convert.ToDateTime(sRQ1);
                    //query.ParamByName("RQ2").AsDateTime = FormatUtils.ParseDatetimeString(sRQ2);
                    query.ParamByName("JC").AsInteger = (aa + 1) * 2;
                    query.Open();
                    while (!query.Eof)
                    {
                        KFPT_BMHYXFFX obj = new KFPT_BMHYXFFX();
                        lst.Add(obj);

                        obj.sSHMC = query.FieldByName("SHMC").AsString;
                        obj.sHYK_NO = query.FieldByName("HYK_NO").AsString;
                        obj.sHY_NAME = query.FieldByName("HY_NAME").AsString;
                        obj.sBMMC = query.FieldByName("BMMC").AsString;
                        obj.sKFJLMC = query.FieldByName("KFJLMC").AsString;
                        obj.iSEX = query.FieldByName("SEX").AsInteger;
                        obj.sSJHM = query.FieldByName("SJHM").AsString;
                        obj.sTXDZ = query.FieldByName("TXDZ").AsString;
                        obj.fXFJE = query.FieldByName("XFJE").AsFloat;
                        obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                        obj.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
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
                    throw new MyDbException(e.Message, query.SqlText);
                }
            }
            finally
            {
                conn.Close();
            }


            return lst;
        }
    }
}
