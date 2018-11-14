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

namespace BF.CrmProc.HYKGL
{
   public   class HYKGL_HYKYHQQTCLTJ:HYKGL_HYKYHQXFTJ
    {

       public double fQTFQJE = 0;
       public double fBKJE = 0;
       public double fQKJE = 0;

       public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
       {
           List<Object> lst = new List<Object>();
           if (iSEARCHMODE == 0)
           {
               CondDict.Add("dRQ", "B.RQ");
               CondDict.Add("iYHQID", "B.YHQID");
               CondDict.Add("iMDID", "B.MDID");
               query.SQL.Text = " select B.HYKTYPE,D.HYKNAME,B.MDID,Y.MDMC,B.YHQID,F.YHQMC,SUM(B.QTFQJE) QTFQJE,SUM(B.BKJE) BKJE,SUM(B.QKJE) QKJE";
               query.SQL.Add("  from HYK_YHQ_QTCLRBB B,HYKDEF D,MDDY Y,YHQDEF F ");
               query.SQL.Add("    where B.HYKTYPE=D.HYKTYPE and B.MDID=Y.MDID and B.YHQID=F.YHQID  ");

               MakeSrchCondition(query, " group by B.HYKTYPE,D.HYKNAME,B.MDID,Y.MDMC,B.YHQID,F.YHQMC");
           }
           if (iSEARCHMODE == 1)
           {
               CondDict.Add("dRQ", "B.RQ");
               query.SQL.Text = "  select B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDID,Y.MDMC,B.SKTNO,SUM(B.QTFQJE) QTFQJE,SUM(B.BKJE) BKJE,SUM(B.QKJE) QKJE";
               query.SQL.Add("  from HYK_YHQ_QTCLRBB B,HYKDEF D,MDDY Y,YHQDEF F");
               query.SQL.Add("   where B.HYKTYPE=D.HYKTYPE  and B.MDID=Y.MDID and B.YHQID=F.YHQID  ");
               query.SQL.Add("   and B.MDID=" + iMDID);
               query.SQL.Add("   and B.HYKTYPE=" + iHYKTYPE);
               query.SQL.Add("   and B.YHQID=" + iYHQID);
               MakeSrchCondition(query, "  group by B.HYKTYPE,D.HYKNAME,B.YHQID,F.YHQMC,B.MDID,Y.MDMC,B.SKTNO");
           }
           if (iSEARCHMODE == 2)
           {
               CondDict.Add("dRQ", "L.CLSJ");
               query.SQL.Text = "  select  L.HYID,X.HYK_NO,L.CLSJ,L.MDID,Y.MDMC,L.SKTNO,L.YHQID,F.YHQMC,L.JLBH,L.ZY,L.JFJE,L.DFJE,L.YE,L.JYBH,L.JYID";
               query.SQL.Add("   from  HYK_YHQCLJL L,HYK_HYXX X,MDDY Y ,YHQDEF F");
               query.SQL.Add("   where (L.CLLX=2 OR L.CLLX=5 OR L.CLLX=10) and  L.HYID=X.HYID and  L.MDID=Y.MDID and F.YHQID=L.YHQID   ");
               query.SQL.Add("   and L.MDID=" + iMDID);
               query.SQL.Add("   and L.YHQID=" + iYHQID);
               query.SQL.Add("   and L.SKTNO='" + sSKTNO + "'");
               query.SQL.Add("   and X.HYKTYPE=" + iHYKTYPE);
               MakeSrchCondition(query);
           }

           SetSearchQuery(query, lst,false);
           return lst;
       }

       public override object SetSearchData(CyQuery query)
       {
           HYKGL_HYKYHQQTCLTJ item = new HYKGL_HYKYHQQTCLTJ();
           item.iMDID = query.FieldByName("MDID").AsInteger;
           item.sMDMC = query.FieldByName("MDMC").AsString;
           item.iYHQID = query.FieldByName("YHQID").AsInteger;
           item.sYHQMC = query.FieldByName("YHQMC").AsString;
           if (iSEARCHMODE != 2)
           {
               item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
               item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
               item.fQTFQJE = query.FieldByName("QTFQJE").AsFloat;
               //  item.dRQ = FormatUtils.DateToString(query.FieldByName("RQ").AsDateTime);
               if (iSEARCHMODE == 1)
               {
                   item.sSKTNO = query.FieldByName("SKTNO").AsString;

               }
               if (iSEARCHMODE == 0)
               {

                   item.fBKJE = query.FieldByName("BKJE").AsFloat;
                   item.fQKJE = query.FieldByName("QKJE").AsFloat;
               }
           }
           else
           {
               item.sSKTNO = query.FieldByName("SKTNO").AsString;
               item.sHYK_NO = query.FieldByName("HYK_NO").AsString;
               item.sZY = query.FieldByName("ZY").AsString;
               item.fJFJE = query.FieldByName("JFJE").AsFloat;
               item.fDFJE = query.FieldByName("DFJE").AsFloat;
               item.fYE = query.FieldByName("YE").AsFloat;
           }
           return item;
       }
    }
}
