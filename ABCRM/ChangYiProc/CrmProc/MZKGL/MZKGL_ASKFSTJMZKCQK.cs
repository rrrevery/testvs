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
using System.Reflection;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_ASKFSTJMZKCQK : DJLR_ZX_CLass
    {
        public string sFS = string.Empty;
        public double fKFJE = 0, fYHJE = 0, fSSJE = 0;

        public double f1 = 0, f2 = 0, f3 = 0, f4 = 0, f5 = 0, f6 = 0, f7 = 0, f8 = 0, f9 = 0, f10 = 0;
        public List<ZFFS> zffsItem = new System.Collections.Generic.List<ZFFS>();

        Dictionary<int, string> DictionaryPay = new System.Collections.Generic.Dictionary<int, string>();

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            switch (iSEARCHMODE)
            {
                case 0:
                    CondDict.Add("iZXR","L.ZXR");
                    CondDict.Add("sZXRMC","L.ZXRMC");
                    CondDict.Add("dZXRQ","L.ZXRQ");
                    query.SQL.Text = " select distinct A.ZFFSID,A.ZFFSMC from ( ";
                    query.SQL.Add("  SELECT DISTINCT M.ZFFSID,F.ZFFSMC FROM  MZK_CZK_CK_SKJL M,MZK_CKJL L, ZFFS F");
                    query.SQL.Add("  WHERE M.JLBH=L.CZJPJ_JLBH AND M.ZFFSID=F.ZFFSID AND L.ZXR>0");
                    MakeSrchCondition(query, "", false);
                    query.SQL.AddLine("union");
                    query.SQL.Add(" SELECT DISTINCT L.ZFFSID,F.ZFFSMC FROM  MZK_CZK_QK_SKJL L,ZFFS F, MZK_QKJL Q ");
                    query.SQL.Add(" WHERE L.ZFFSID=F.ZFFSID AND L.JLBH=Q.CZJPJ_JLBH and Q.ZXR >0 ");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  ）A  order by A.ZFFSID");
                    query.Open();
                    while (!query.Eof)
                    {
                        int tp_zffsid = query.FieldByName("ZFFSID").AsInteger;
                        string tp_zffsmc = query.FieldByName("ZFFSMC").AsString;
                        DictionaryPay.Add(tp_zffsid, tp_zffsmc);
                        query.Next();
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = "  select A.ZXR,A.ZXRMC,A.FS";
                    foreach (KeyValuePair<int, string> item in DictionaryPay)
                    {
                        if (DictionaryPay.Count == 1)
                        {
                            query.SQL.Add("  ,sum (CASE A.ZFFSID WHEN " + item.Key + " THEN A.JE ELSE 0 end) " + item.Value + "");
                        }
                        else
                        {
                            query.SQL.Add("   ,sum (CASE A.ZFFSID WHEN " + item.Key + " THEN A.JE ELSE 0 end) " + item.Value + ",");

                        }
                    }
                    if (DictionaryPay.Count > 1)
                    {
                        query.SQL.Text = query.SQL.Text.Substring(0, query.SQL.Text.Length - 1);
                    }
                    query.SQL.Add("  from (SELECT L.ZXR, L.ZXRMC,'存款' as FS,M.ZFFSID,F.ZFFSMC,SUM(JE) JE ");
                    query.SQL.Add("  from MZK_CZK_CK_SKJL M,MZK_CKJL L ,ZFFS F ");
                    query.SQL.Add("   WHERE M.JLBH=L.CZJPJ_JLBH and F.ZFFSID=M.ZFFSID and L.ZXR >0 ");
                    MakeSrchCondition(query, "GROUP BY L.ZXR,L.ZXRMC,M.ZFFSID,F.ZFFSMC", false);

                    query.SQL.Add("  UNION ALL");
                    query.SQL.Add("  SELECT Q.ZXR,Q.ZXRMC,'取款' AS FS,L.ZFFSID,F.ZFFSMC,SUM(L.JE) JE ");
                    query.SQL.Add("   FROM  MZK_CZK_QK_SKJL L,ZFFS F, MZK_QKJL Q ");
                    query.SQL.Add("   WHERE L.ZFFSID=F.ZFFSID AND L.JLBH=Q.CZJPJ_JLBH and Q.ZXR >0 ");
                    MakeSrchCondition(query, "GROUP BY Q.ZXR,Q.ZXRMC,L.ZFFSID,F.ZFFSMC", false);
                    query.SQL.Add("  ) A");
                    query.SQL.Add("GROUP BY  A.ZXR,A.ZXRMC,A.FS ");

                    break;
                case 1:
                    query.SQL.Text = "   SELECT DISTINCT M.ZFFSID,F.ZFFSMC FROM  MZK_CZK_CK_SKJL M,MZK_CKJL L, ZFFS F";
                    query.SQL.Add("   WHERE M.JLBH=L.CZJPJ_JLBH  AND M.ZFFSID=F.ZFFSID AND L.ZXR>0 ");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  ORDER BY M.ZFFSID");
                    query.Open();
                    while (!query.Eof)
                    {
                        int tp_zffsid = query.FieldByName("ZFFSID").AsInteger;
                        string tp_zffsmc = query.FieldByName("ZFFSMC").AsString;
                        DictionaryPay.Add(tp_zffsid, tp_zffsmc);
                        query.Next();
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = "  select A.ZXR,A.ZXRMC,A.FS";
                    foreach (KeyValuePair<int, string> item in DictionaryPay)
                    {
                        if (DictionaryPay.Count == 1)
                        {
                            query.SQL.Add("  ,sum (CASE A.ZFFSID WHEN " + item.Key + " THEN A.JE ELSE 0 end) " + item.Value + "");
                        }
                        else
                        {
                            query.SQL.Add("  ,sum (CASE A.ZFFSID WHEN " + item.Key + " THEN A.JE ELSE 0 end) " + item.Value + ",");

                        }
                    }
                    if (DictionaryPay.Count > 1)
                    {
                        query.SQL.Text = query.SQL.Text.Substring(0, query.SQL.Text.Length - 1);
                    }

                    query.SQL.Add("  from (SELECT L.ZXR, L.ZXRMC,'存款' as FS,M.ZFFSID,F.ZFFSMC,SUM(JE) JE ");
                    query.SQL.Add("  from MZK_CZK_CK_SKJL M,MZK_CKJL L ,ZFFS F ");
                    query.SQL.Add("   WHERE M.JLBH=L.CZJPJ_JLBH and F.ZFFSID=M.ZFFSID and L.ZXR >0 ");
                    MakeSrchCondition(query, "GROUP BY L.ZXR,L.ZXRMC,M.ZFFSID,F.ZFFSMC", false);
                    query.SQL.Add("  ) A");
                    query.SQL.Add("GROUP BY  A.ZXR,A.ZXRMC,A.FS ");

                    break;

                case 2:
                    query.SQL.Text = "   SELECT DISTINCT L.ZFFSID,F.ZFFSMC FROM  MZK_CZK_QK_SKJL L,ZFFS F, MZK_QKJL Q";
                    query.SQL.Add("   WHERE L.ZFFSID=F.ZFFSID AND L.JLBH=Q.CZJPJ_JLBH and Q.ZXR >0 ");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("  ORDER BY L.ZFFSID");
                    query.Open();
                    while (!query.Eof)
                    {
                        int tp_zffsid = query.FieldByName("ZFFSID").AsInteger;
                        string tp_zffsmc = query.FieldByName("ZFFSMC").AsString;
                        DictionaryPay.Add(tp_zffsid, tp_zffsmc);
                        query.Next();
                    }
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.SQL.Text = "  select A.ZXR,A.ZXRMC,A.FS";
                    foreach (KeyValuePair<int, string> item in DictionaryPay)
                    {
                        if (DictionaryPay.Count == 1)
                        {
                            query.SQL.Add("  ,sum (CASE A.ZFFSID WHEN " + item.Key + " THEN A.JE ELSE 0 end) " + item.Value + "");
                        }
                        else
                        {
                            query.SQL.Add("   ,sum(CASE A.ZFFSID WHEN " + item.Key + " THEN A.JE ELSE 0 end) " + item.Value + ",");

                        }
                    }
                    if (DictionaryPay.Count > 1)
                    {
                        query.SQL.Text = query.SQL.Text.Substring(0, query.SQL.Text.Length - 1);
                    }
                    query.SQL.Add("  from (SELECT Q.ZXR,Q.ZXRMC,'存款' AS FS,L.ZFFSID,F.ZFFSMC,SUM(L.JE) JE ");
                    query.SQL.Add("  FROM  MZK_CZK_QK_SKJL L,ZFFS F, MZK_QKJL Q ");
                    query.SQL.Add("   WHERE L.ZFFSID=F.ZFFSID AND L.JLBH=Q.CZJPJ_JLBH and Q.ZXR >0 ");
                    MakeSrchCondition(query, "GROUP BY Q.ZXR,Q.ZXRMC,L.ZFFSID,F.ZFFSMC", false);
                    query.SQL.Add("  ) A");
                    query.SQL.Add("GROUP BY   A.ZXR,A.ZXRMC,A.FS ");


                    break;
            }
            query.Open();
            while (!query.Eof)
            {
                MZKGL_ASKFSTJMZKCQK itemOther = new MZKGL_ASKFSTJMZKCQK();
                itemOther.sZXRMC = query.FieldByName("ZXRMC").AsString;
                itemOther.iZXR = query.FieldByName("ZXR").AsInteger;
                itemOther.sFS = query.FieldByName("FS").AsString;
                double tp_ssje = 0;
                foreach (KeyValuePair<int, string> item in DictionaryPay)
                {
                    Type type = itemOther.GetType();
                    FieldInfo[] fields = type.GetFields();
                    foreach (FieldInfo itemelse in fields)
                    {
                        if (itemelse.Name.Substring(1, itemelse.Name.Length - 1) == item.Key.ToString())
                        {
                            itemelse.SetValue(itemOther, query.FieldByName(item.Value).AsFloat);
                            tp_ssje += query.FieldByName(item.Value).AsFloat;

                        }
                    }

                }
                itemOther.fSSJE = tp_ssje;
                lst.Add(itemOther);
                query.Next();
            }
            query.Close();


            return lst;
        }



    }
}
