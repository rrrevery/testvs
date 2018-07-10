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


namespace BF.CrmProc.CRMGL
{
    public class CRMGL_XTSJPHJC : BASECRMClass
    {
        public int iDZLX = 0;
        public string dRQ = string.Empty;

        private class CRMGL_RCLZT
        {
            public string sLIBNAME = string.Empty;
            public string dPROC_KSSJ = string.Empty;
            public string dPROC_JSSJ = string.Empty;
            public int iSTATUS = 0;
        }
        private class CRMGL_CZKDZB
        {
            public string sHYKNAME = string.Empty;
            public double dCE = 0;
            public double dSQYE = 0;
            public double dJKJE = 0;
            public double dCKJE = 0;
            public double dBKJE = 0;
            public double dQKJE = 0;
            public double dXFJE = 0;
            public double dTKJE = 0;
            public double dTZJE = 0;
            public double dQMYE = 0;
        }
        private class CRMGL_YHQDZB
        {
            public string sYHQMC = string.Empty;
            public double dCE = 0;
            public double dSQYE = 0;
            public double dCKJE = 0;
            public double dBKJE = 0;
            public double dQKJE = 0;
            public double dXFJE = 0;
            public double dTZJE = 0;
            public double dQMYE = 0;

        }
        private class CRMGL_KZKDZB
        {
            public string sBGDDMC = string.Empty;
            public string sHYKNAME = string.Empty;
            public double dMZJE = 0;
            public double dSL = 0;
            public double dJE = 0;
        }

        public override object SetSearchData(CyQuery query)
        {
            dynamic item = null;
            switch (iDZLX)
            {
                case 0:
                    CRMGL_RCLZT item_tp = new CRMGL_RCLZT();
                    item_tp.sLIBNAME = query.FieldByName("LIBNAME").AsString;
                    item_tp.dPROC_KSSJ = FormatUtils.DatetimeToString(query.FieldByName("PROC_KSSJ").AsDateTime);
                    item_tp.dPROC_JSSJ = FormatUtils.DatetimeToString(query.FieldByName("PROC_JSSJ").AsDateTime);
                    item_tp.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    item = item_tp;
                    break;
                case 1:
                    CRMGL_CZKDZB item_cz = new CRMGL_CZKDZB();
                    item_cz.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_cz.dCE = query.FieldByName("CE").AsFloat;
                    item_cz.dSQYE = query.FieldByName("SQYE").AsFloat;
                    item_cz.dJKJE = query.FieldByName("JKJE").AsFloat;
                    item_cz.dCKJE = query.FieldByName("CKJE").AsFloat;
                    item_cz.dBKJE = query.FieldByName("BKJE").AsFloat;
                    item_cz.dQKJE = query.FieldByName("QKJE").AsFloat;
                    item_cz.dXFJE = query.FieldByName("XFJE").AsFloat;
                    item_cz.dTKJE = query.FieldByName("TKJE").AsFloat;
                    item_cz.dTZJE = query.FieldByName("TZJE").AsFloat;
                    item_cz.dQMYE = query.FieldByName("QMYE").AsFloat;
                    item = item_cz;
                    break;
                case 2:
                    CRMGL_YHQDZB item_yq = new CRMGL_YHQDZB();
                    item_yq.sYHQMC = query.FieldByName("YHQMC").AsString;
                    item_yq.dCE = query.FieldByName("CE").AsFloat;
                    item_yq.dSQYE = query.FieldByName("SQYE").AsFloat;
                    //item.dJKJE = query.FieldByName("JKJE").AsFloat;
                    item_yq.dCKJE = query.FieldByName("CKJE").AsFloat;
                    item_yq.dBKJE = query.FieldByName("BKJE").AsFloat;
                    item_yq.dQKJE = query.FieldByName("QKJE").AsFloat;
                    item_yq.dXFJE = query.FieldByName("XFJE").AsFloat;
                    //item.dTKJE = query.FieldByName("TKJE").AsFloat;
                    item_yq.dTZJE = query.FieldByName("TZJE").AsFloat;
                    item_yq.dQMYE = query.FieldByName("QMYE").AsFloat;
                    item = item_yq;
                    break;
                case 3:
                    CRMGL_KZKDZB item_kz = new CRMGL_KZKDZB();
                    item_kz.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                    item_kz.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    item_kz.dMZJE = query.FieldByName("MZJE").AsFloat;
                    item_kz.dSL = query.FieldByName("SL").AsFloat;
                    item_kz.dJE = query.FieldByName("JE").AsFloat;
                    item = item_kz;
                    break;
            }
            return item;
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            CondDict.Add("dRQ", "L.RQ");

            switch (this.iDZLX)
            {
                case 0://日处理状态
                    query.SQL.Text = "select LIBNAME,PROC_KSSJ,PROC_JSSJ,STATUS  from RCL L,SYSLIB S";
                    query.SQL.Text += " where L.LIBID=S.ID(+) and S.FLAG_RCL=1 ";
                    SetSearchQuery(query, lst);
                    break;
                case 1:
                    query.SQL.Text = "select H.HYKNAME,sum(SQYE+JKJE+CKJE+L.BKJE-QKJE-XFJE-TKJE-QMYE-NVL(TZJE,0)) CE ,";
                    query.SQL.Text += "  sum(SQYE) SQYE,sum(JKJE) JKJE ,sum(CKJE) CKJE ,sum(L.BKJE) BKJE,sum(QKJE) QKJE ,sum(XFJE) XFJE ,sum(TKJE) TKJE ,sum(NVL(TZJE,0)) TZJE,sum(QMYE) QMYE";
                    query.SQL.Text += " from HYK_CZK_RBB L,HYKDEF H  ";
                    query.SQL.Text += " where L.HYKTYPE=H.HYKTYPE  ";
                    SetSearchQuery(query, lst, true, "group by H.HYKNAME");
                    break;
                case 2:
                    query.SQL.Text = "select H.YHQMC,sum(SQYE+CKJE+BKJE-QKJE-XFJE-QMYE-NVL(TZJE,0)) CE , ";
                    query.SQL.Text += "   sum(SQYE) SQYE,sum(CKJE) CKJE ,sum(BKJE) BKJE,sum(QKJE) QKJE ,sum(XFJE) XFJE ,sum(NVL(TZJE,0)) TZJE,sum(QMYE) QMYE";
                    query.SQL.Text += " from HYK_YHQ_RBB L,YHQDEF H  ";
                    query.SQL.Text += " where L.YHQID=H.YHQID  ";
                    SetSearchQuery(query, lst, true, "group by H.YHQMC",false);
                    break;
                case 3:
                    query.SQL.Text = "select BGDDMC,HYKNAME,MZJE,SUM(QCSL+XKSL+BRSL-BCSL-FSSL-HKSL+FSTSSL+XFTSSL-ZFSL-JCSL+TZSL) SL,SUM(QCJE+XKJE+BRJE-BCJE-FSJE-HKJE+FSTSJE+XFTSJE-ZFJE-JCJE+TZJE) JE ";
                    query.SQL.Text += " from HYK_KCCZKBGZ L,HYKDEF B,HYK_BGDD C  ";
                    query.SQL.Text += " where L.HYKTYPE=B.HYKTYPE and L.BGDDDM=C.BGDDDM  ";
                    SetSearchQuery(query, lst, true, "group by  BGDDMC,HYKNAME,MZJE",false);


                    break;
            }
            return lst;
        }

        public void SetSearchQuery(CyQuery query, List<object> lst, bool cond = true, string sGroup = "", bool bSort = true)
        {
            //查询主数据的循环都放到这里，继承类只需要重写SetSearchData即可
            if (cond)
                MakeSrchCondition(query, sGroup, bSort);
            if (!query.Active)
                query.Open();
            while (!query.Eof)
            {
                int a = OutOfCurPage(query);
                if (a == 1)
                    continue;
                else if (a == 2)
                    break;
                object item = SetSearchData(query);
                lst.Add(item);
                query.Next();
            }
            GetSearchSum(query);
            query.Close();
        }

    }
}
