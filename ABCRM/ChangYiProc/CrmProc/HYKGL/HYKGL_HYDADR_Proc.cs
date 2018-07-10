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
    public class HYKGL_HYDADR : GKDA
    {
        public List<GKDA> itemTable = new List<GKDA>();

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            foreach (GKDA item in itemTable)
            {
                query.SQL.Text = "  select * from HYK_GKDA  where SJHM='" + item.sSJHM + "' or SFZBH='" + item.sSFZBH + "' ";
                query.Open();
                if (!query.IsEmpty)
                {
                    if (query.FieldByName("SJHM").AsString == item.sSJHM)
                        msg += @"第" + (itemTable.IndexOf(item) + 1) + "行手机号码重复;\n";
                    else
                        msg += @"第" + (itemTable.IndexOf(item) + 1) + "行证件号码重复;\n";
                }
                else
                {
                    query.SQL.Clear();
                    query.Params.Clear();
                    query.Close();
                    iJLBH = SeqGenerator.GetSeqNoDBID("HYK_GKDA");
                    query.SQL.Text = " insert into HYK_GKDA(GKID,SEX,SFZBH,CSRQ,SJHM,DJR,DJRMC,DJSJ,GK_NAME)";
                    query.SQL.Add("  values(:GKID,:SEX,:SFZBH,:CSRQ,:SJHM,:DJR,:DJRMC,:DJSJ,:GK_NAME)");
                    query.ParamByName("GKID").AsInteger = iJLBH;
                    query.ParamByName("SEX").AsInteger = 1;
                    query.ParamByName("CSRQ").AsDateTime = new DateTime(Convert.ToInt32(item.sSFZBH.Substring(6, 4)), Convert.ToInt32(item.sSFZBH.Substring(10, 2)), Convert.ToInt32(item.sSFZBH.Substring(12, 2)));
                    query.ParamByName("SJHM").AsString = item.sSJHM;
                    query.ParamByName("DJR").AsInteger = iLoginRYID;
                    query.ParamByName("DJRMC").AsString = sLoginRYMC;
                    query.ParamByName("DJSJ").AsDateTime = serverTime;
                    query.ParamByName("GK_NAME").AsString = item.sGK_NAME;
                    query.ParamByName("SFZBH").AsString = item.sSFZBH;
                    query.ExecSQL();
                }
            }
            query.Close();
        }

    }
}
