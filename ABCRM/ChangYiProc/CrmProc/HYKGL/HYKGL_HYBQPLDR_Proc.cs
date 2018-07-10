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
    public class HYKGL_HYBQPLDR : BASECRMClass
    {
        public string sSFZBH = string.Empty, sSJHM = string.Empty, sHY_NAME = string.Empty;
        public int iXH1 = 0, iXH2 = 0, iXH3 = 0, iXH4 = 0, iXH5 = 0;
        public int iMONTH1 = 0, iMONTH2 = 0, iMONTH3 = 0, iMONTH4 = 0, iMONTH5 = 0;
        public double fQZ1 = 0, fQZ2 = 0, fQZ3 = 0, fQZ4 = 0, fQZ5 = 0;
        public string sBQ1 = string.Empty, sBQ2 = string.Empty, sBQ3 = string.Empty, sBQ4 = string.Empty, sBQ5 = string.Empty;
        public List<HYKGL_HYBQPLDR> itemTable = new List<HYKGL_HYBQPLDR>();
        public List<HYKGL.HYKGL_BQZDY_Proc> itemLabelTable = new List<HYKGL_BQZDY_Proc>();
        List<int> itemHYID = new List<int>();
        Dictionary<int, int> dictionaryLabel = new Dictionary<int, int>();

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            List<int> error_index = new List<int>();
            if (iJLBH == 0)
            {
                // foreach (HYKGL_HYBQPLDR one in itemTable)
                for (int i = 0; i < itemTable.Count; i++)
                {
                    HYKGL_HYBQPLDR one = itemTable[i];
                    double[] arrayQZ = new double[] { one.fQZ1, one.fQZ2, one.fQZ3, one.fQZ4, one.fQZ5 };
                    int[] arrayMONTH = new int[] { one.iMONTH1, one.iMONTH2, one.iMONTH3, one.iMONTH4, one.iMONTH5 };
                    itemHYID.Clear();
                    query.SQL.Clear();
                    query.SQL.Text = "  select   /*+leading(G,H) index(G INX_GKDA_SFZHM)*/  HYID  from  HYK_HYXX H,HYK_GKDA G where H.GKID=G.GKID and G.SJHM='" + one.sSJHM + "' and G.SFZBH='" + one.sSFZBH + "'";
                    query.Open();
                    while (!query.Eof)
                    {
                        itemHYID.Add(query.FieldByName("HYID").AsInteger);
                        query.Next();
                    }
                    query.Close();
                    if (itemHYID.Count > 0)
                    {
                        query.SQL.Clear();
                        itemLabelTable.Clear();
                        query.SQL.Text = " SELECT * FROM LABEL_XMITEM  WHERE LABELID IN(" + one.iXH1 + "," + one.iXH2 + "," + one.iXH3 + "," + one.iXH4 + "," + one.iXH5 + ") ";
                        query.Open();
                        while (!query.Eof)
                        {
                            if (query.FieldByName("LABELID").AsInteger == one.iXH1 && query.FieldByName("LABEL_VALUE").AsString.Trim() != one.sBQ1.Trim())
                            {
                                if (!error_index.Contains(one.iXH1))
                                {
                                    error_index.Add(one.iXH1);
                                }
                            }
                            if (query.FieldByName("LABELID").AsInteger == one.iXH2 && query.FieldByName("LABEL_VALUE").AsString.Trim() != one.sBQ2.Trim())
                            {
                                if (!error_index.Contains(one.iXH2))
                                {
                                    error_index.Add(one.iXH2);
                                }
                            }
                            if (query.FieldByName("LABELID").AsInteger == one.iXH3 && query.FieldByName("LABEL_VALUE").AsString.Trim() != one.sBQ3.Trim())
                            {
                                if (!error_index.Contains(one.iXH3))
                                {
                                    error_index.Add(one.iXH3);
                                }
                            }
                            if (query.FieldByName("LABELID").AsInteger == one.iXH4 && query.FieldByName("LABEL_VALUE").AsString.Trim() != one.sBQ4.Trim())
                            {
                                if (!error_index.Contains(one.iXH4))
                                {
                                    error_index.Add(one.iXH4);
                                }
                            }
                            if (query.FieldByName("LABELID").AsInteger == one.iXH5 && query.FieldByName("LABEL_VALUE").AsString.Trim() != one.sBQ5.Trim())
                            {
                                if (!error_index.Contains(one.iXH5))
                                {
                                    error_index.Add(one.iXH5);
                                }
                            }
                            else
                            {
                                HYKGL_BQZDY_Proc labelItem = new HYKGL_BQZDY_Proc();
                                labelItem.iLABELXMID = query.FieldByName("LABELXMID").AsInteger;
                                labelItem.iLABEL_VALUEID = query.FieldByName("LABEL_VALUEID").AsInteger;
                                labelItem.iLABELID = query.FieldByName("LABELID").AsInteger;
                                itemLabelTable.Add(labelItem);
                            }
                            query.Next();
                        }
                        query.Close();
                        if (error_index.Count <= 0)
                        {

                            foreach (int hyid in itemHYID)
                            {
                                int tpcount = 0;
                                foreach (var item in itemLabelTable)
                                {
                                    query.SQL.Text = "  select * from  HYK_HYBQ where hyid=" + hyid + " and LABELXMID=" + item.iLABELXMID + " and LABEL_VALUEID=" + item.iLABEL_VALUEID + " and LABELID=" + item.iLABELID + "";
                                    query.Open();
                                    if (query.IsEmpty)
                                    {
                                        query.SQL.Clear();
                                        query.SQL.Text = "insert into  HYK_HYBQ(LABELXMID,LABEL_VALUEID,YXQ,HYID,QZ,LABELID,BJ_TRANS)";
                                        query.SQL.Add(" values(:LABELXMID,:LABEL_VALUEID,:YXQ,:HYID,:QZ,:LABELID,:BJ_TRANS)");
                                        query.ParamByName("LABEL_VALUEID").AsInteger = item.iLABEL_VALUEID;
                                        query.ParamByName("LABELXMID").AsInteger = item.iLABELXMID;
                                        query.ParamByName("HYID").AsInteger = hyid;
                                        query.ParamByName("YXQ").AsDateTime = DateTime.Now.AddMonths(arrayMONTH[tpcount]);
                                        query.ParamByName("LABELID").AsInteger = item.iLABELID;
                                        query.ParamByName("QZ").AsFloat = arrayQZ[tpcount];
                                        query.ParamByName("BJ_TRANS").AsInteger = 2;
                                        query.ExecSQL();
                                        tpcount++;

                                    }
                                    else
                                    {
                                        tpcount++;
                                    }
                                    query.Close();
                                }
                            }
                        }

                    }
                }
                if (error_index.Count > 0)
                {
                    msg = string.Join(",", error_index.ToArray());
                    msg += "标签值不符合";
                }

            }
        }
    }
}
