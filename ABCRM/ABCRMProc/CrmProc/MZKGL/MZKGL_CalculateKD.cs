using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_CalculateKD
    {
        public class _class_input
        {
            public string CZKHM = string.Empty;
            public string QCYE = string.Empty;
            public string HYKTYPE = string.Empty;
        }

        public DataTable tblKDMX = new DataTable();

        public void aa()
        {
            tblKDMX.Columns.Add("JLBH");
            tblKDMX.Columns.Add("CZKHM_BEGIN");
            tblKDMX.Columns.Add("CZKHM_END");
            tblKDMX.Columns.Add("SKSL");
            tblKDMX.Columns.Add("MZJE");
            tblKDMX.Columns.Add("HYKTYPE");
            //tblKDMX.Columns.Add("FXDWID");
        }

        public DataTable a_CalculateKD(DataTable tabels, string iBegin, string iEnd)
        {
            aa();
            int iCount;
            string iTempCode, iFirstCode, iLastCode;
            double iLastMoney, iTempMoney;
            int ilen_b, ilen_e;
            ilen_b = iBegin.Length;
            ilen_e = iEnd.Length;
            if (tabels.Rows.Count == 0) { DeleteAllRecords(tblKDMX); }


            if (tabels.Rows.Count > 0)
            {
                DeleteAllRecords(tblKDMX);
                iFirstCode = tabels.Rows[0]["CZKHM"].ToString().Substring(ilen_b, tabels.Rows[0]["CZKHM"].ToString().Trim().Length - ilen_b - ilen_e);
                iLastCode = iFirstCode;
                iLastMoney = Convert.ToDouble(tabels.Rows[0]["QCYE"]);
                iCount = 0;
                for (int i = 0; i <= tabels.Rows.Count - 1; i++)
                {
                    iTempCode = tabels.Rows[i]["CZKHM"].ToString().Substring(ilen_b, tabels.Rows[i]["CZKHM"].ToString().Trim().Length - ilen_b - ilen_e);
                    iTempMoney = Convert.ToDouble(tabels.Rows[i]["QCYE"]);
                    if ((i > 0) && (((Convert.ToInt64(iLastCode) + 1) != Convert.ToInt64(iTempCode)) || (iLastMoney != iTempMoney)))
                    {
                        InsertKDMX(iFirstCode, iLastCode, iLastMoney, iCount, iBegin, iEnd, tabels.Rows[0]["HYKTYPE"].ToString());
                        iFirstCode = iTempCode;
                        iCount = 0;
                    }
                    iLastMoney = iTempMoney;
                    iLastCode = iTempCode;
                    iCount = iCount + 1;
                }
                InsertKDMX(iFirstCode, iLastCode, iLastMoney, iCount, iBegin, iEnd, tabels.Rows[0]["HYKTYPE"].ToString());
            }
            return tblKDMX;
        }

        private void InsertKDMX(string sKS, string sJS, double iMoney, int iCount, string iBegin, string iEnd, string hyktype)
        {

            DataRow mrows = tblKDMX.NewRow();

            mrows["CZKHM_BEGIN"] = iBegin + sKS + iEnd;
            mrows["CZKHM_END"] = iBegin + sJS + iEnd;
            mrows["MZJE"] = iMoney;
            mrows["SKSL"] = iCount;
            mrows["HYKTYPE"] = hyktype;
            //mrows["FXDWID"] = fxdwid;
            tblKDMX.Rows.Add(mrows);
        }

        public void DeleteAllRecords(DataTable sTable)
        {
            sTable.Clear();
        }
    }
}
