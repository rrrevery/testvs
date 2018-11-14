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
    public class CRMGL_KLXDY : HYKDEF
    {
        public bool qx = true;
        public List<CRMGL_KLXDYItem> itemTable = new List<CRMGL_KLXDYItem>();

        public class CRMGL_KLXDYItem
        {
            public int iHYKTYPE = 0, iMDID = 0, iZKFS = 0;
            public string sMDMC = string.Empty;
            public string sZKFS = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "HYKDEF;HYK_MDZKFS", "HYKTYPE", iHYKTYPE);
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                CrmLibProc.DeleteDataTables(query, out msg, "HYK_MDZKFS", "HYKTYPE", iHYKTYPE);
                query.SQL.Text = "update HYKDEF set HYKKZID=:HYKKZID,HYKNAME=:HYKNAME,FXFS=:FXFS,HMCD=:HMCD,KHQDM=:KHQDM,KHHZM=:KHHZM,FFDX=:FFDX,YXQCD=:YXQCD,BJ_PSW=:BJ_PSW,BJ_XSJL=:BJ_XSJL,BJ_JF=:BJ_JF,BJ_YHQZH=:BJ_YHQZH,BJ_CZZH=:BJ_CZZH,BJ_CZK=:BJ_CZK, ";
                query.SQL.AddLine("YHFS=:YHFS,FKQDXFJE=:FKQDXFJE,KXBJ=:KXBJ,TKBJ=:TKBJ,ZFBJ=:ZFBJ,BJ_CDNRJM=:BJ_CDNRJM,CDJZ=:CDJZ,BJ_CZYHQ=:BJ_CZYHQ,BJ_XTZK=:BJ_XTZK,FS_YXQ=:FS_YXQ,BJ_QZYK=:BJ_QZYK,BJ_ZQHY=:BJ_ZQHY, ");
                query.SQL.AddLine("JFXX=:JFXX,JFCLFWFS=:JFCLFWFS,FS_SYMD=:FS_SYMD,SJJZQ=:SJJZQ,");
                query.SQL.AddLine("HYKJCID=:HYKJCID,KFJE=:KFJE, ");
                query.SQL.AddLine("BJ_TH=:BJ_TH,BJ_XK=:BJ_XK,BJ_YZM=:BJ_YZM ");
                query.SQL.AddLine("where HYKTYPE=:HYKTYPE ");
            }
            else
            {
                iJLBH = SeqGenerator.GetSeqNoDBID("HYKDEF" + iHYKKZID) + iHYKKZID * 100;
                query.SQL.Text = "insert into HYKDEF(HYKTYPE,HYKKZID,HYKNAME,FXFS,HMCD,KHQDM,KHHZM,FFDX,YXQCD,BJ_PSW,BJ_XSJL,BJ_JF,BJ_YHQZH,BJ_CZZH,BJ_CZK,YHFS,FKQDXFJE,KXBJ,TKBJ,ZFBJ,HYKJCID,KFJE,BJ_CDNRJM,CDJZ,BJ_CZYHQ,BJ_XTZK,FS_YXQ,BJ_QZYK,BJ_ZQHY,JFXX,JFCLFWFS,FS_SYMD,SJJZQ,BJ_TH,BJ_XK,BJ_YZM)";//,BJ_CX,BJ_FPGL,BJ_TS,BJ_FSK,JFXX,BJ_YZM,THBJ,BJ_ZERO,BJ_CK,BJ_QK,BJ_JEZC,BJ_LMZK,SJJZQ,BJ_WX
                query.SQL.Add(" values(:HYKTYPE,:HYKKZID,:HYKNAME,:FXFS,:HMCD,:KHQDM,:KHHZM,:FFDX,:YXQCD,:BJ_PSW,:BJ_XSJL,:BJ_JF,:BJ_YHQZH,:BJ_CZZH,:BJ_CZK,:YHFS,:FKQDXFJE,:KXBJ,:TKBJ,:ZFBJ,:HYKJCID,:KFJE,:BJ_CDNRJM,:CDJZ,:BJ_CZYHQ,:BJ_XTZK,:FS_YXQ,:BJ_QZYK,:BJ_ZQHY,:JFXX,:JFCLFWFS,:FS_SYMD,:SJJZQ,:BJ_TH,:BJ_XK,:BJ_YZM)");//,:BJ_CX,:BJ_FPGL,:BJ_TS,:BJ_FSK,:JFXX,:BJ_YZM,:THBJ,:BJ_ZERO,:BJ_CK,:BJ_QK,:BJ_JEZC,:BJ_LMZK,:SJJZQ,:BJ_WX
            }
            query.ParamByName("HYKTYPE").AsInteger = iHYKTYPE;
            query.ParamByName("HYKKZID").AsInteger = iHYKKZID;
            query.ParamByName("HYKNAME").AsString = sHYKNAME;
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
            query.ParamByName("HYKJCID").AsInteger = iHYKJCID;
            query.ParamByName("KFJE").AsFloat = fKFJE;
            query.ParamByName("BJ_CDNRJM").AsInteger = iBJ_CDNRJM;
            query.ParamByName("CDJZ").AsInteger = iCDJZ;
            query.ParamByName("BJ_CZYHQ").AsInteger = iBJ_CZYHQ;
            query.ParamByName("BJ_XTZK").AsInteger = iBJ_XTZK;
            query.ParamByName("FS_YXQ").AsInteger = iFS_YXQ;
            query.ParamByName("BJ_QZYK").AsInteger = iBJ_QZYK;
            query.ParamByName("BJ_ZQHY").AsInteger = iBJ_ZQHY;
            query.ParamByName("BJ_TH").AsInteger = iBJ_TH;
            query.ParamByName("BJ_XK").AsInteger = iBJ_XK;
            query.ParamByName("FS_SYMD").AsInteger = iFS_SYMD;
            query.ParamByName("JFCLFWFS").AsInteger = iJFCLFWFS;
            query.ParamByName("JFXX").AsFloat = fJFXX;
            query.ParamByName("SJJZQ").AsString = sSJJZQ;
            query.ParamByName("BJ_YZM").AsInteger = iBJ_YZM;

            query.ExecSQL();

            foreach (CRMGL_KLXDYItem one in itemTable)
            {
                query.SQL.Text = "insert into HYK_MDZKFS(HYKTYPE,MDID,ZKFS) ";
                query.SQL.AddLine("values(:HYKTYPE,:MDID,:ZKFS) ");
                query.ParamByName("HYKTYPE").AsInteger = iJLBH;
                query.ParamByName("MDID").AsInteger = one.iMDID;
                query.ParamByName("ZKFS").AsInteger = one.iZKFS;
                query.ExecSQL();
            }
        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            sNoSumFiled = "iFXFS;fJFXX;fKFJE;iHMCD;iCDJZ;iFS_YXQ;iBJ_CZZH;iBJ_XTZK;iBJ_QZYK;iBJ_PSW";
            if (sSortFiled == "")
            {
                sSortFiled = "iJLBH";
                sSortType = "asc";
            }
            List<object> lst = new List<object>();
            CondDict.Add("iJLBH", "A.HYKTYPE");
            CondDict.Add("iHYKKZID", "A.HYKKZID");
            CondDict.Add("sHYKNAME", "A.HYKNAME");
            CondDict.Add("iFXFS", "A.FXFS");
            CondDict.Add("iHMCD", "A.HMCD");
            CondDict.Add("sKHQDM", "A.KHQDM");
            CondDict.Add("sKHHZM", "A.KHHZM");
            CondDict.Add("iFFDX", "A.FFDX");
            CondDict.Add("sYXQCD", "A.YXQCD");
            CondDict.Add("iBJ_PSW", "A.BJ_PSW");
            CondDict.Add("iBJ_XSJL", "A.BJ_XSJL");
            CondDict.Add("iBJ_JF", "A.BJ_JF");
            CondDict.Add("iBJ_YHQZH", "A.BJ_YHQZH");
            CondDict.Add("iBJ_CZZH", "A.BJ_CZZH");
            CondDict.Add("iBJ_CZK", "A.BJ_CZK");
            CondDict.Add("iYHFS", "A.YHFS");
            CondDict.Add("fFKQDXFJE", "A.FKQDXFJE");
            CondDict.Add("iKXBJ", "A.KXBJ");
            CondDict.Add("iTKBJ", "A.TKBJ");
            CondDict.Add("iZFBJ", "A.ZFBJ");
            CondDict.Add("iHYKJCID", "A.HYKJCID");
            CondDict.Add("iTM", "A.TM");
            CondDict.Add("fKFJE", "A.KFJE");
            CondDict.Add("iBJ_CDNRJM", "A.BJ_CDNRJM");
            CondDict.Add("iCDJZ", "A.CDJZ");
            CondDict.Add("iBJ_CZYHQ", "A.BJ_CZYHQ");
            CondDict.Add("iBJ_XTZK", "A.BJ_XTZK");
            CondDict.Add("iFS_YXQ", "A.FS_YXQ");
            CondDict.Add("iBJ_QZYK", "A.BJ_QZYK");
            CondDict.Add("iBJ_CX", "A.BJ_CX");
            CondDict.Add("iBJ_ZQHY", "A.BJ_ZQHY");
            CondDict.Add("iBJ_TH", "A.BJ_TH");
            CondDict.Add("iBJ_FPGL", "A.BJ_FPGL");
            CondDict.Add("iBJ_XK", "A.BJ_XK");
            CondDict.Add("iBJ_TS", "A.BJ_TS");
            CondDict.Add("iBJ_FSK", "A.BJ_FSK");
            CondDict.Add("iFS_SYMD", "A.FS_SYMD");
            CondDict.Add("iJFCLFWFS", "A.JFCLFWFS");
            CondDict.Add("fJFXX", "A.JFXX");
            CondDict.Add("iBJ_YZM", "A.BJ_YZM");
            CondDict.Add("iTHBJ", "A.THBJ");
            CondDict.Add("iBJ_ZERO", "A.BJ_ZERO");
            CondDict.Add("iYZMCD", "A.YZMCD");
            CondDict.Add("iBJ_POSXK", "A.BJ_POSXK");
            CondDict.Add("fCZJEMIN", "A.CZJEMIN");
            CondDict.Add("fCZJEMAX", "A.CZJEMAX");
            CondDict.Add("iBJ_CK", "A.BJ_CK");
            CondDict.Add("iBJ_QK", "A.BJ_QK");
            CondDict.Add("iBJ_JEZC", "A.BJ_JEZC");
            CondDict.Add("iBJ_LMZK", "A.BJ_LMZK");
            CondDict.Add("sSJJZQ", "A.SJJZQ");
            CondDict.Add("fFJBL", "A.FJBL");
            CondDict.Add("iBJ_JFQL", "A.BJ_JFQL");
            CondDict.Add("iBJ_WX", "A.BJ_WX");
            CondDict.Add("iBJ_YGK", "A.BJ_YGK");
            CondDict.Add("iBJ_WDASK", "A.BJ_WDASK");
            CondDict.Add("sKBJC", "A.KBJC");
            CondDict.Add("iYGK_BJ", "A.YGK_BJ");
            CondDict.Add("iBJ_QTFK", "A.BJ_QTFK");
            CondDict.Add("fBKJE", "A.BKJE");
            CondDict.Add("sIMG", "A.IMG");
            CondDict.Add("iRANDOM_LEN", "A.RANDOM_LEN");
            CondDict.Add("iMFTCSJ", "A.MFTCSJ");

            query.SQL.Text = "select A.*,D.HYKKZNAME from HYKDEF A,HYKKZDEF D";
            query.SQL.Add("    where A.HYKKZID = D.HYKKZID");
            if (sReqMode != "View" && qx && iLoginRYID != GlobalVariables.SYSInfo.iAdminID)
            {
                query.SQL.Add(" and (exists(select 1 from XTCZY_HYLXQX X where A.HYKTYPE=X.HYKTYPE and X.PERSON_ID=" + iLoginRYID + ")");
                query.SQL.Add(" or exists(select 1 from CZYGROUP_HYLXQX X,XTCZYGRP G where A.HYKTYPE=X.HYKTYPE and X.ID=G.GROUPID and G.PERSON_ID=" + iLoginRYID + "))");
                //query.ParamByName("RYID").AsInteger = iLoginRYID;
            }
            SetSearchQuery(query, lst);
            if (lst.Count == 1)
            {
                query.SQL.Text = "select A.*,M.MDMC from HYK_MDZKFS A,MDDY M  where A.MDID=M.MDID and A.HYKTYPE=" + iHYKTYPE;
                query.Open();
                while (!query.Eof)
                {
                    CRMGL_KLXDYItem item = new CRMGL_KLXDYItem();
                    ((CRMGL_KLXDY)lst[0]).itemTable.Add(item);
                    item.iMDID = query.FieldByName("MDID").AsInteger;
                    item.sMDMC = query.FieldByName("MDMC").AsString;
                    item.iZKFS = query.FieldByName("ZKFS").AsInteger;
                    switch (item.iZKFS)
                    {
                        case 0: item.sZKFS = "不折扣"; break;
                        case 1: item.sZKFS = "折扣率"; break;
                        case 2: item.sZKFS = "会员价"; break;
                        default: break;
                    }
                    query.Next();
                }
                query.Close();
            }

            return lst;
        }


        public override object SetSearchData(CyQuery query)
        {
            CRMGL_KLXDY obj = new CRMGL_KLXDY();
            obj.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
            obj.iHYKKZID = query.FieldByName("HYKKZID").AsInteger;
            obj.sHYKNAME = query.FieldByName("HYKNAME").AsString;
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
            obj.iHYKJCID = query.FieldByName("HYKJCID").AsInteger;
            obj.fKFJE = query.FieldByName("KFJE").AsFloat;
            obj.iBJ_CDNRJM = query.FieldByName("BJ_CDNRJM").AsInteger;
            obj.iCDJZ = query.FieldByName("CDJZ").AsInteger;
            obj.iBJ_CZYHQ = query.FieldByName("BJ_CZYHQ").AsInteger;
            obj.iBJ_XTZK = query.FieldByName("BJ_XTZK").AsInteger;
            obj.iFS_YXQ = query.FieldByName("FS_YXQ").AsInteger;
            obj.iBJ_QZYK = query.FieldByName("BJ_QZYK").AsInteger;
            obj.iBJ_CX = query.FieldByName("BJ_CX").AsInteger;
            obj.iBJ_ZQHY = query.FieldByName("BJ_ZQHY").AsInteger;
            obj.iBJ_TH = query.FieldByName("BJ_TH").AsInteger;
            obj.iBJ_FPGL = query.FieldByName("BJ_FPGL").AsInteger;
            obj.iBJ_XK = query.FieldByName("BJ_XK").AsInteger;
            obj.iBJ_TS = query.FieldByName("BJ_TS").AsInteger;
            obj.iBJ_FSK = query.FieldByName("BJ_FSK").AsInteger;
            obj.iFS_SYMD = query.FieldByName("FS_SYMD").AsInteger;
            obj.iJFCLFWFS = query.FieldByName("JFCLFWFS").AsInteger;
            obj.fJFXX = query.FieldByName("JFXX").AsFloat;
            obj.iBJ_CK = query.FieldByName("BJ_CK").AsInteger;
            obj.iBJ_QK = query.FieldByName("BJ_QK").AsInteger;
            obj.iBJ_JEZC = query.FieldByName("BJ_JEZC").AsInteger;
            obj.iBJ_LMZK = query.FieldByName("BJ_LMZK").AsInteger;
            obj.sSJJZQ = query.FieldByName("SJJZQ").AsString;
            obj.iBJ_WX = query.FieldByName("BJ_WX").AsInteger;
            obj.sHYKKZNAME = query.FieldByName("HYKKZNAME").AsString;
            obj.iBJ_YZM = query.FieldByName("BJ_YZM").AsInteger;

            return obj;
        }

    }
}
