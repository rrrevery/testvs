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
    public class HYKGL_SJJGZ : SJGZ
    {

        public string sHYKKZNAME = string.Empty;
        public string sHYKKZNAME_NEW = string.Empty;
        public int iHYKKZID = 0;
        public int iHYKKZID_NEW = 0;
        public int iHYKTYPE = 0;
        public int iHYKJCID = 0;
        public string sMDMC = string.Empty;

        public string sHYKJCNAME = string.Empty;
        public string sHYKNAME = string.Empty;

        public string sHYKHM_OLD = string.Empty;
        public string sHYKHM_NEW = string.Empty;

        public string sHYKJCNAME_NEW = string.Empty;
        //用来修改使用
        public int iNewHYKTYPE_OLD = 0;
        public int iNewHYKTYPE_NEW = 0;
        public int iNewXFSJ = -1;
        public int iMDID_NEW = 0;

        
        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            CrmLibProc.DeleteDataBase(query, out msg, "delete from HYK_DJSQGZ where HYKTYPE=" + iHYKTYPE_OLD + " and HYKTYPE_NEW=" + iHYKTYPE_NEW );

        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iHYKTYPE_OLD != 0 && iHYKTYPE_NEW != 0)//修改 && iXFSJ != -1
            {
                DeleteDataQuery(out msg, query, serverTime);
            }
            query.Close();
            query.SQL.Text = "insert into HYK_DJSQGZ(HYKTYPE,HYKTYPE_NEW,XFSJ,QDJF,BJ_SJ,DRXFJE,SJYJJE,SJQDJE,BLRQ,MDID,BJ_XFJE)";
            query.SQL.Add(" values(:HYKTYPE,:HYKTYPE_NEW,:XFSJ,:QDJF,:BJ_SJ,:DRXFJE,:SJYJJE,:SJQDJE,:BLRQ,:MDID,:BJ_XFJE)");

            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE_OLD;
            query.ParamByName("HYKTYPE_NEW").AsInteger = iHYKTYPE_NEW;
            query.ParamByName("XFSJ").AsInteger = iNewXFSJ;
            query.ParamByName("MDID").AsInteger = iMDID_NEW;
            query.ParamByName("QDJF").AsFloat = fQDJF;
            query.ParamByName("BJ_SJ").AsInteger = iBJ_SJ;
            query.ParamByName("DRXFJE").AsFloat = fDRXFJE;
            query.ParamByName("SJYJJE").AsFloat = fSJYJJE;
            query.ParamByName("SJQDJE").AsFloat = fSJQDJE;
            query.ParamByName("BLRQ").AsDateTime = FormatUtils.ParseDateString(dBLRQ);
            query.ParamByName("BJ_XFJE").AsInteger = iBJ_XFJE;
            query.ExecSQL();
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iHYKKZID", "O.HYKKZID");
            CondDict.Add("sHYKKZNAME", "O.HYKKZNAME");
            CondDict.Add("iHYKTYPE_OLD", "O.HYKTYPE");
            CondDict.Add("iHYKTYPE_NEW", "N.HYKTYPE_NEW");
            CondDict.Add("iXFSJ", "N.XFSJ");
            CondDict.Add("fQDJF", "O.QDJF");
            CondDict.Add("iBJ_SJ", "O.BJ_SJ");
            CondDict.Add("iBJ_XFJE", "O.BJ_XFJE");

            query.SQL.Text = "select O.*,N.HYKKZID_NEW,N.HYKKZNAME_NEW,N.HYKJCID_NEW,N.HYKJCNAME_NEW,N.HYKTYPE_NEW,N.HYKNAME_NEW from ";
            query.SQL.Add(" (select K.HYKKZID,K.HYKKZNAME,D.HYKJCID,D.HYKJCNAME,B.HYKTYPE,B.HYKNAME,W.XFSJ,W.BJ_SJ,W.BJ_XFJE,W.HYKTYPE_NEW,W.QDJF,W.DRXFJE,W.SJQDJE,W.BLRQ from HYK_DJSQGZ W,HYKDEF B,HYKDJDEF D,HYKKZDEF K where W.HYKTYPE=B.HYKTYPE AND B.HYKJCID=D.HYKJCID AND D.HYKKZID=K.HYKKZID AND B.HYKKZID=K.HYKKZID  ) O");
            query.SQL.Add(",");
            query.SQL.Add(" (select K.HYKKZID HYKKZID_NEW,K.HYKKZNAME HYKKZNAME_NEW,D.HYKJCID HYKJCID_NEW,D.HYKJCNAME HYKJCNAME_NEW,B.HYKTYPE HYKTYPE_NEW,B.HYKNAME HYKNAME_NEW,W.XFSJ,W.BJ_SJ,W.BJ_XFJE,W.SJQDJE,W.HYKTYPE from HYK_DJSQGZ W,HYKDEF B,HYKDJDEF D,HYKKZDEF K where W.HYKTYPE_NEW=B.HYKTYPE AND B.HYKJCID=D.HYKJCID AND D.HYKKZID=K.HYKKZID AND B.HYKKZID=K.HYKKZID  ) N ");
            query.SQL.Add("WHERE O.HYKTYPE_NEW=N.HYKTYPE_NEW AND O.HYKTYPE=N.HYKTYPE AND O.XFSJ=N.XFSJ AND O.BJ_SJ=N.BJ_SJ ");
            SetSearchQuery(query, lst);
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            HYKGL_SJJGZ obj = new HYKGL_SJJGZ();
            obj.sHYKKZNAME = query.FieldByName("HYKKZNAME").AsString;
            obj.iHYKKZID = query.FieldByName("HYKKZID").AsInteger;
            obj.iHYKKZID_NEW = query.FieldByName("HYKKZID_NEW").AsInteger;
            obj.sHYKKZNAME_NEW = query.FieldByName("HYKKZNAME_NEW").AsString;
            obj.iHYKTYPE_OLD = query.FieldByName("HYKTYPE").AsInteger;
            obj.sHYKJCNAME = query.FieldByName("HYKJCNAME").AsString;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
            obj.iHYKTYPE_NEW = query.FieldByName("HYKTYPE_NEW").AsInteger;
            obj.sHYKJCNAME_NEW = query.FieldByName("HYKJCNAME_NEW").AsString;
            obj.sHYKNAME_NEW = query.FieldByName("HYKNAME_NEW").AsString;
            obj.fDRXFJE = query.FieldByName("DRXFJE").AsFloat;
            obj.fSJQDJE = query.FieldByName("SJQDJE").AsFloat;
            obj.iXFSJ = query.FieldByName("XFSJ").AsInteger;
            obj.fQDJF = query.FieldByName("QDJF").AsFloat;
            obj.iBJ_SJ = query.FieldByName("BJ_SJ").AsInteger;
            obj.iBJ_XFJE = query.FieldByName("BJ_XFJE").AsInteger;
            //obj.iMDID = query.FieldByName("MDID").AsInteger;
            //obj.sMDMC = query.FieldByName("MDMC").AsString;
            return obj;
        }
    }
}
