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
    public class HYKGL_ARTTREE : BASECRMClass
    {
        public string sDialogName = string.Empty;
        public string sID = string.Empty;
        public string sName = string.Empty;
        public string sTalName = string.Empty;
        public string sSHDM = string.Empty;
        public int iActId = 0;           //保存实际ID
        public string sPID = string.Empty;
        public int iLABELLX = -1;
        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<object> lst = new List<object>();
            switch (sDialogName)
            {
                case "TreeBGDD":
                    query.SQL.Text = "select 0 ACTID, BGDDDM  ID,BGDDMC NAME,BGDDDM||' '||BGDDMC TALNAME  from  HYK_BGDD A where 1=1";
                    if (iLoginRYID != GlobalVariables.SYSInfo.iAdminID)
                    {
                        query.SQL.Add(" and (exists(select 1 from XTCZY_BGDDQX X where (X.BGDDDM=' ' or (A.BGDDDM like X.BGDDDM||'%' or X.BGDDDM like A.BGDDDM||'%')) and X.PERSON_ID=:RYID)");
                        query.SQL.Add(" or exists(select 1 from CZYGROUP_BGDDQX Q,XTCZYGRP G where (Q.BGDDDM=' ' or (A.BGDDDM LIKE Q.BGDDDM||'%' or Q.BGDDDM LIKE A.BGDDDM||'%')) and Q.ID=G.GROUPID and G.PERSON_ID=:RYID))");
                        query.ParamByName("RYID").AsInteger = iLoginRYID;
                    }
                    query.SQL.Add("order by ID asc");
                    //MakeSrchCondition(query);  
                    break;
                case "TreeFXDW":
                    //query.SQL.Text = "select (select MAX(B.FXDWDM) from FXDWDEF B WHERE A.FXDWDM LIKE B.FXDWDM|| '%' AND  A.FXDWDM <> B.FXDWDM ) PFXDWDM ,(A.FXDWDM||' '||A.FXDWMC) FXDWQC,A.* FROM FXDWDEF A WHERE 1=1 order by FXDWDM";
                    query.SQL.Text = "select 0 ACTID, FXDWDM ID,FXDWMC NAME,FXDWDM||' '||FXDWMC TALNAME from FXDWDEF A where 1=1";
                    if (iLoginRYID != GlobalVariables.SYSInfo.iAdminID)
                    {
                        query.SQL.Add(" and (exists(select 1 from XTCZY_FXDWQX X where (X.FXDWDM=' ' or (A.FXDWDM like X.FXDWDM||'%' or X.FXDWDM like A.FXDWDM||'%')) and X.PERSON_ID=:RYID)");
                        query.SQL.Add(" or exists(select 1 from CZYGROUP_FXDWQX Q,XTCZYGRP G where (Q.FXDWDM=' ' or (A.FXDWDM like Q.FXDWDM||'%' or Q.FXDWDM like A.FXDWDM||'%')) and Q.ID=G.GROUPID and G.PERSON_ID=:RYID))");
                        query.ParamByName("RYID").AsInteger = iLoginRYID;
                    }
                    query.SQL.Add("order by ID asc");
                    break;
                case "TreeSHBM":
                    CondDict.Add("iSHBMID", "A.SHBMID");
                    query.SQL.Text = "select SHBMID ACTID,  BMDM ID,BMMC NAME,BMDM||' '||BMMC TALNAME from SHBM A where 1=1";
                    if (sSHDM != "") { query.SQL.Add(" and SHDM='" + sSHDM + "'"); }
                    if (iLoginRYID != GlobalVariables.SYSInfo.iAdminID)
                    {
                        query.SQL.Add(" and (exists(select 1 from XTCZY_BMCZQX X where (X.BMDM=' ' or (A.BMDM like X.BMDM||'%' or X.BMDM like A.BMDM||'%')) and X.PERSON_ID=:RYID");
                        if (sSHDM != "") { query.SQL.Add(" and X.SHDM='" + sSHDM + "'"); }
                        query.SQL.Add(" ) or exists(select 1 from CZYGROUP_BMCZQX Q,XTCZYGRP G where (Q.BMDM=' ' or (A.BMDM like Q.BMDM||'%' or Q.BMDM like A.BMDM||'%')) and Q.ID=G.GROUPID and G.PERSON_ID=:RYID");
                        if (sSHDM != "") { query.SQL.Add(" and Q.SHDM='" + sSHDM + "'"); }
                        query.SQL.Add(" ))");
                        query.ParamByName("RYID").AsInteger = iLoginRYID;
                    }
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("order by ID asc");
                    break;
                case "TreeSHSPFL":
                    CondDict.Add("iSHSPFLID", "A.SHSPFLID");
                    query.SQL.Text = "select SHSPFLID ACTID, SPFLDM ID,SPFLMC NAME,SPFLDM||' '||SPFLMC TALNAME from SHSPFL A where 1=1";
                    query.SQL.Add(" and SHDM='" + sSHDM + "'");
                    MakeSrchCondition(query, "", false);
                    query.SQL.Add("order by ID asc");
                    break;
                case "TreeHYBQ":
                    query.SQL.Text = " select * from(";
                    query.SQL.Add("select LABELLBID ACTID，'AA'||LABELLBID ID,BQMC NAME,'00' PID,BQMC TALNAME from LABEL_LB");
                    query.SQL.Add("union select LABELXMID ACTID，'BB'||LABELXMID ID,LABELXMMC NAME,nvl((select max('BB'||LABELXMID) from LABEL_XM B where A.LABELXMDM like B.LABELXMDM||'%' and B.LABELLBID=A.LABELLBID and A.LABELXMDM<>B.LABELXMDM),'AA'||LABELLBID)PID,LABELXMMC TALNAME from LABEL_XM A");
                    query.SQL.Add("union select LABELID ACTID，to_char(LABELID) ID,LABEL_VALUE NAME,'BB'||(select LABELXMID from LABEL_XM B where B.LABELXMID=A.LABELXMID)PID,LABEL_VALUE TALNAME from LABEL_XMITEM A where 1=1");
                    if (iLABELLX != -1)
                        query.SQL.Add("  and A.LABELLX=" + iLABELLX);
                    query.SQL.Add("  )");
                    query.SQL.Add("  order by ID");
                    break;
                case "TreeQY":
                    query.SQL.Text = " select QYID ACTID, QYDM  ID,QYMC NAME,QYDM||' '||QYMC TALNAME  from  HYK_HYQYDY where 1=1 order by ID";
                    break;
            }
            query.Open();
            while (!query.Eof)
            {
                HYKGL_ARTTREE obj = new HYKGL_ARTTREE();
                lst.Add(obj);
                obj.sID = query.FieldByName("ID").AsString;
                obj.sName = query.FieldByName("NAME").AsString;
                obj.sTalName = query.FieldByName("TALNAME").AsString;
                obj.iActId = query.FieldByName("ACTID").AsInteger;
                if (sDialogName == "TreeHYBQ")
                    obj.sPID = query.FieldByName("PID").AsString;
                query.Next();
            }
            query.Close();
            return lst;
        }
    }
}
