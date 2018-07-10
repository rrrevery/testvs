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
using System.Reflection.Emit;

namespace BF.CrmProc.MZKGL
{
    public class MZKGL_MZKASKFSCXSKXX_Srch : DJLR_ZX_CLass
    {
        public string sFS = string.Empty;
        public int iZFFSID = 0;
        public string sZFFSMC = string.Empty;
        public double fKFJE = 0, fYHJE = 0, fSSJE = 0;


        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            CondDict.Add("iDJLX","A.DJLX");
            CondDict.Add("iMDID", "A.MDID_CZ");
            CondDict.Add("dZXRQ", "A.ZXRQ");
            CondDict.Add("iZXR", "A.ZXR");

            query.SQL.Text = "select A.ZXR,A.ZXRMC,sum(JE) as SSJE,A.ZFFSID,A.ZFFSMC,A.FS from (";
            query.SQL.Add(" select  L.ZXR, L.ZXRMC,L.ZXRQ,'售卡' as FS,M.ZFFSID,F.ZFFSMC,sum (JE) JE ,D.MDMC,L.MDID_CZ,1 as DJLX");
            query.SQL.Add(" from MZK_SKJLSKMX M,MZK_SKJL L ,ZFFS F,MDDY D ");
            query.SQL.Add(" where  M.JLBH=L.JLBH  and F.ZFFSID=M.ZFFSID and L.ZXR >0  and L.MDID_CZ=D.MDID");
            query.SQL.Add(" group  by  L.ZXR,L.ZXRMC,M.ZFFSID,F.ZFFSMC,D.MDMC,L.MDID_CZ,L.ZXRQ union ");
            query.SQL.Add(" select Q.ZXR,Q.ZXRMC,Q.ZXRQ,'存款' as FS,L.ZFFSID,F.ZFFSMC,sum(L.JE) JE,D.MDMC,Q.MDID_CZ,2 as DJLX");
            query.SQL.Add(" from  MZK_PLCKSKJL L,ZFFS F, MZK_PLCK Q ,MDDY D ");
            query.SQL.Add(" where L.ZFFSID=F.ZFFSID and L.JLBH=Q.JLBH and Q.ZXR >0 and Q.MDID_CZ=D.MDID");
            query.SQL.Add(" group by Q.ZXR,Q.ZXRMC,Q.ZXRQ,L.ZFFSID,F.ZFFSMC,D.MDMC,Q.MDID_CZ union ");
            query.SQL.Add(" select Q.ZXR,Q.ZXRMC,Q.ZXRQ,'存款' as FS,L.ZFFSID,F.ZFFSMC,sum(L.JE) JE,D.MDMC,Q.MDID as MDID_CZ,2 as DJLX");
            query.SQL.Add(" from  MZK_CKJLSKITEM L,ZFFS F, MZK_CKJL Q ,MDDY D");
            query.SQL.Add(" where L.ZFFSID=F.ZFFSID and L.CZJPJ_JLBH=Q.CZJPJ_JLBH and Q.ZXR >0 and Q.MDID=D.MDID");
            query.SQL.Add(" group by Q.ZXR,Q.ZXRMC,L.ZFFSID,F.ZFFSMC,D.MDMC,Q.MDID,Q.ZXRQ");
            query.SQL.Add(" ) A where 1=1 ");
            SetSearchQuery(query, lst,true, "group by A.ZXR,A.ZXRMC,A.ZFFSID,A.ZFFSMC,A.FS");
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_MZKASKFSCXSKXX_Srch item = new MZKGL_MZKASKFSCXSKXX_Srch();
            item.sZXRMC = query.FieldByName("ZXRMC").AsString;
            item.iZXR = query.FieldByName("ZXR").AsInteger;
            item.sFS = query.FieldByName("FS").AsString;
            item.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
            item.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
            item.fSSJE = query.FieldByName("SSJE").AsFloat;
            return item;
        }

    }
}
