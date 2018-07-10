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
    public class CRMGL_KZDY : HYKKZDEF
    {
        public List<CRMGL_KZDYItem> itemTable = new List<CRMGL_KZDYItem>();

        public class CRMGL_KZDYItem : HYKDJDEF
        {
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = "";
            if (query == null)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
                query = new CyQuery(conn);
            }
            query.SQL.Text = "select * from HYKDEF ";
            query.SQL.Add(" where HYKKZID=:HYKKZID");
            query.ParamByName("HYKKZID").AsInteger = iHYKKZID;
            query.Open();
            if (!query.IsEmpty)
            {
                msg = "该卡种下存在卡类型，不允许删除";
                return;
            }

            CrmLibProc.DeleteDataTables(query, out msg, "HYKKZDEF;HYKDJDEF", "HYKKZID", iHYKKZID);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                //DeleteDataQuery(out msg, query,serverTime);
                CrmLibProc.DeleteDataTables(query, out msg, "HYKDJDEF", "HYKKZID", iHYKKZID);
                query.SQL.Text = "update HYKKZDEF set HYKKZNAME=:HYKKZNAME,FXFS=:FXFS,HMCD=:HMCD,KHQDM=:KHQDM,KHHZM=:KHHZM,FFDX=:FFDX,YXQCD=:YXQCD,BJ_PSW=:BJ_PSW,BJ_XSJL=:BJ_XSJL,BJ_JF=:BJ_JF,BJ_YHQZH=:BJ_YHQZH,BJ_CZZH=:BJ_CZZH,BJ_CZK=:BJ_CZK, ";
                query.SQL.AddLine("YHFS=:YHFS,FKQDXFJE=:FKQDXFJE,KXBJ=:KXBJ,TKBJ=:TKBJ,ZFBJ=:ZFBJ,BJ_CDNRJM=:BJ_CDNRJM,CDJZ=:CDJZ,BJ_CZYHQ=:BJ_CZYHQ,BJ_XTZK=:BJ_XTZK,FS_YXQ=:FS_YXQ,BJ_QZYK=:BJ_QZYK,BJ_ZQHY=:BJ_ZQHY,BJ_XFSJ=:BJ_XFSJ ");
                query.SQL.AddLine("where HYKKZID=:HYKKZID ");
            }
            else
            {
                iJLBH = SeqGenerator.GetSeqNoDBID("HYKKZDEF");
                query.SQL.Text = "insert into HYKKZDEF(HYKKZID,HYKKZNAME,FXFS,HMCD,KHQDM,KHHZM,FFDX,YXQCD,BJ_PSW,BJ_XSJL,BJ_JF,BJ_YHQZH,BJ_CZZH,BJ_CZK,YHFS,FKQDXFJE,KXBJ,TKBJ,ZFBJ,BJ_CDNRJM,CDJZ,BJ_CZYHQ,BJ_XTZK,FS_YXQ,BJ_QZYK,BJ_ZQHY,BJ_XFSJ)";
                query.SQL.Add(" values(:HYKKZID,:HYKKZNAME,:FXFS,:HMCD,:KHQDM,:KHHZM,:FFDX,:YXQCD,:BJ_PSW,:BJ_XSJL,:BJ_JF,:BJ_YHQZH,:BJ_CZZH,:BJ_CZK,:YHFS,:FKQDXFJE,:KXBJ,:TKBJ,:ZFBJ,:BJ_CDNRJM,:CDJZ,:BJ_CZYHQ,:BJ_XTZK,:FS_YXQ,:BJ_QZYK,:BJ_ZQHY,:BJ_XFSJ)");
            }
            query.ParamByName("HYKKZID").AsInteger = iHYKKZID;
            query.ParamByName("HYKKZNAME").AsString = sHYKKZNAME;
            query.ParamByName("FXFS").AsInteger = iFXFS;
            query.ParamByName("HMCD").AsInteger = iHMCD;
            query.ParamByName("KHQDM").AsString = sKHQDM;
            query.ParamByName("KHHZM").AsString = sKHHZM;
            query.ParamByName("FFDX").AsInteger = iFFDX;
            query.ParamByName("YXQCD").AsString = sYXQCD;
            query.ParamByName("BJ_PSW").AsInteger = iBJ_PSW;
            query.ParamByName("BJ_XSJL").AsInteger = iBJ_XSJL;
            query.ParamByName("BJ_JF").AsInteger = iBJ_JF;
            query.ParamByName("BJ_YHQZH").AsInteger = iBJ_YHQZH;
            query.ParamByName("BJ_CZZH").AsInteger = iBJ_CZZH;
            query.ParamByName("BJ_CZK").AsInteger = iBJ_CZK;
            query.ParamByName("YHFS").AsInteger = iYHFS;
            query.ParamByName("FKQDXFJE").AsFloat = fFKQDXFJE;
            query.ParamByName("KXBJ").AsInteger = iKXBJ;
            query.ParamByName("TKBJ").AsInteger = iTKBJ;
            query.ParamByName("ZFBJ").AsInteger = iZFBJ;
            query.ParamByName("BJ_CDNRJM").AsInteger = iBJ_CDNRJM;
            query.ParamByName("CDJZ").AsInteger = iCDJZ;
            query.ParamByName("BJ_CZYHQ").AsInteger = iBJ_CZYHQ;
            query.ParamByName("BJ_XTZK").AsInteger = iBJ_XTZK;
            query.ParamByName("FS_YXQ").AsInteger = iFS_YXQ;
            query.ParamByName("BJ_QZYK").AsInteger = iBJ_QZYK;
            query.ParamByName("BJ_ZQHY").AsInteger = iBJ_ZQHY;
            query.ParamByName("BJ_XFSJ").AsInteger = iBJ_XFSJ;

            query.ExecSQL();
            foreach (CRMGL_KZDYItem one in itemTable)
            {
                query.SQL.Text = "insert into HYKDJDEF(HYKKZID,HYKJCID,HYKJCNAME) ";
                query.SQL.AddLine("values(:HYKKZID,:HYKJCID,:HYKJCNAME) ");
                query.ParamByName("HYKKZID").AsInteger = iJLBH;
                query.ParamByName("HYKJCID").AsInteger = one.iHYKJCID;
                query.ParamByName("HYKJCNAME").AsString = one.sHYKJCNAME;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iFXFS;iHMCD;iCDJZ;iBJ_XTZK;iBJ_QZYK;iBJ_CDNRJM;";
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "B.HYKKZID");
            CondDict.Add("sHYKKZNAME", "B.HYKKZNAME");
            CondDict.Add("iBJ_CZK", "B.BJ_CZK");
            query.SQL.Text = "select B.* from HYKKZDEF B";
            query.SQL.Add("    where 1=1");
            if (iJLBH != 0)
            {
                query.SQL.Add(" and B.HYKKZID=" + iJLBH);
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select * from HYKDJDEF B where B.HYKKZID="+iJLBH;
                query.Open();
                while (!query.Eof)
                {
                    CRMGL_KZDYItem item = new CRMGL_KZDYItem();
                    ((CRMGL_KZDY)lst[0]).itemTable.Add(item);
                    item.iHYKJCID = query.FieldByName("HYKJCID").AsInteger;
                    item.sHYKJCNAME = query.FieldByName("HYKJCNAME").AsString;
                    query.Next();
                }
                query.Close();
            }

            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            CRMGL_KZDY obj = new CRMGL_KZDY();
            obj.iHYKKZID = query.FieldByName("HYKKZID").AsInteger;
            obj.sHYKKZNAME = query.FieldByName("HYKKZNAME").AsString;
            obj.iFXFS = query.FieldByName("FXFS").AsInteger;
            obj.iHMCD = query.FieldByName("HMCD").AsInteger;
            obj.sKHQDM = query.FieldByName("KHQDM").AsString;
            obj.sKHHZM = query.FieldByName("KHHZM").AsString;
            obj.iFFDX = query.FieldByName("FFDX").AsInteger;
            obj.sYXQCD = query.FieldByName("YXQCD").AsString;
            obj.iBJ_PSW = query.FieldByName("BJ_PSW").AsInteger;
            obj.iBJ_XSJL = query.FieldByName("BJ_XSJL").AsInteger;
            obj.iBJ_JF = query.FieldByName("BJ_JF").AsInteger;
            obj.iBJ_YHQZH = query.FieldByName("BJ_YHQZH").AsInteger;
            obj.iBJ_CZZH = query.FieldByName("BJ_CZZH").AsInteger;
            obj.iBJ_CZK = query.FieldByName("BJ_CZK").AsInteger;
            obj.iYHFS = query.FieldByName("YHFS").AsInteger;
            obj.fFKQDXFJE = query.FieldByName("FKQDXFJE").AsFloat;
            obj.iKXBJ = query.FieldByName("KXBJ").AsInteger;
            obj.iTKBJ = query.FieldByName("TKBJ").AsInteger;
            obj.iZFBJ = query.FieldByName("ZFBJ").AsInteger;
            obj.iBJ_CDNRJM = query.FieldByName("BJ_CDNRJM").AsInteger;
            obj.iCDJZ = query.FieldByName("CDJZ").AsInteger;
            obj.iBJ_CZYHQ = query.FieldByName("BJ_CZYHQ").AsInteger;
            obj.iBJ_XTZK = query.FieldByName("BJ_XTZK").AsInteger;
            obj.iFS_YXQ = query.FieldByName("FS_YXQ").AsInteger;
            obj.iBJ_QZYK = query.FieldByName("BJ_QZYK").AsInteger;
            obj.iBJ_ZQHY = query.FieldByName("BJ_ZQHY").AsInteger;
            obj.iBJ_XFSJ = query.FieldByName("BJ_XFSJ").AsInteger;
            return obj;
        }
    }
}
