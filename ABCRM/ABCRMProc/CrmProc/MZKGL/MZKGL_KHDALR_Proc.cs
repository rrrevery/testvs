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


namespace BF.CrmProc.MZKGL
{
    public class MZKGL_KHDALR_Proc  : DJLR_ZX_CLass
    {
        public string sPYM = string.Empty;
        public string sKHWZ = string.Empty, sLXRXM = string.Empty, sLXRSJ = string.Empty, sLXRDH = string.Empty, sLXRTXDZ = string.Empty;
        public string sLXRYB = string.Empty, sLXREMAIL = string.Empty;
        public int iLXRZJ = 0, iMDID = 0;
        public string sLXRZJHM = string.Empty, sMDMC = string.Empty;
        public int iKHZT = 0;//1:保存 2：取消审核 3：审核
        public string sKHDM = string.Empty, sKHMC = string.Empty, sKHDZ = string.Empty;
        public int iKHDJ = 0, iKHXZ = 0, iYXSX = 0;
        public string sQYDH = string.Empty;
        public int iYXSXTS = 0, iKFZYID = 0;
        public string sKFZYMC = string.Empty;
        public double fYXSXJE = 0;
        public string sSFZBH = string.Empty;
        public string sSJHM = string.Empty;
        public string sPHONE = string.Empty;
        //public int iSEX;
        public string dCSRQ = string.Empty;
        public int iGKRRYID;


        public override void ExecDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            ExecTable(query, "MZK_KHDA", serverTime, "KHID");
            msg = string.Empty;
        }

        public override void DeleteDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            CrmLibProc.DeleteDataTables(query, out msg, "MZK_KHDA", "KHID", iJLBH,"ZXR", "CRMDBMZK");
        }

        public override void SaveDataQuery(out string msg, CyQuery query, DateTime serverTime)
        {
            msg = string.Empty;
            if (iJLBH != 0)
            {
                DeleteDataQuery(out msg, query,serverTime);
            }
            else
                iJLBH = SeqGenerator.GetSeq("MZK_KHDA");

            query.SQL.Text = "insert into MZK_KHDA(KHID,KHDM,KHMC,KHDZ,KHXZ,KHWZ,LXRXM,LXRSJ,LXRDH,LXRTXDZ,LXRYB,LXREMAIL,LXRZJ,LXRZJHM,BZ,PYM,DJR,DJRMC,DJSJ,MDID)";
            query.SQL.Add(" values(:KHID,:KHDM,:KHMC,:KHDZ,:KHXZ,:KHWZ,:LXRXM,:LXRSJ,:LXRDH,:LXRTXDZ,:LXRYB,:LXREMAIL,:LXRZJ,:LXRZJHM,:BZ,:PYM,:DJR,:DJRMC,:DJSJ,:MDID)");
            query.ParamByName("KHID").AsInteger = iJLBH;
            query.ParamByName("KHDM").AsString = iJLBH.ToString();
            query.ParamByName("KHMC").AsString = sKHMC;
            query.ParamByName("KHDZ").AsString = sKHDZ;
            query.ParamByName("KHXZ").AsInteger = iKHXZ;
            query.ParamByName("KHWZ").AsString = sKHWZ;
            query.ParamByName("LXRXM").AsString = sLXRXM;
            query.ParamByName("LXRSJ").AsString = sLXRSJ;
            query.ParamByName("LXRDH").AsString = sLXRDH;
            query.ParamByName("LXRTXDZ").AsString = sLXRTXDZ;
            query.ParamByName("LXRYB").AsString = sLXRYB;
            query.ParamByName("LXREMAIL").AsString = sLXREMAIL;
            query.ParamByName("LXRZJ").AsInteger = iLXRZJ;
            query.ParamByName("LXRZJHM").AsString = sLXRZJHM;
            query.ParamByName("BZ").AsString = sZY;
            query.ParamByName("PYM").AsString = IndexCode(sKHMC);
            query.ParamByName("DJR").AsInteger = iLoginRYID;
            query.ParamByName("DJRMC").AsString = sLoginRYMC;
            query.ParamByName("DJSJ").AsDateTime = serverTime;
            query.ParamByName("MDID").AsInteger = iMDID;
            query.ExecSQL();


        }

        public override List<object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<Object> lst = new List<Object>();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);

            CondDict.Add("iJLBH", "A.KHID");
            CondDict.Add("iMDID", "A.MDID");
            query.SQL.Text = "  select A.*,M.MDMC from MZK_KHDA A,MDDY M where A.MDID=M.MDID ";
            SetSearchQuery(query, lst);
            
            query.Close();
            return lst;
        }

        public override object SetSearchData(CyQuery query)
        {
            MZKGL_KHDALR_Proc obj = new MZKGL_KHDALR_Proc();
            obj.iJLBH = query.FieldByName("KHID").AsInteger;
            obj.sKHMC = query.FieldByName("KHMC").AsString;
            obj.sKHDZ = query.FieldByName("KHDZ").AsString;
            obj.iKHXZ = query.FieldByName("KHXZ").AsInteger;
            obj.iLXRZJ = query.FieldByName("LXRZJ").AsInteger;
            obj.sKHDM = query.FieldByName("KHDM").AsString;
            obj.sKHDZ = query.FieldByName("KHDZ").AsString;
            obj.sMDMC = query.FieldByName("MDMC").AsString;
            obj.sLXRDH = query.FieldByName("LXRDH").AsString;
            obj.sLXRXM = query.FieldByName("LXRXM").AsString;
            obj.sLXRYB = query.FieldByName("LXRYB").AsString;
            obj.sLXRZJHM = query.FieldByName("LXRZJHM").AsString;
            obj.sLXRSJ = query.FieldByName("LXRSJ").AsString;
            obj.sLXRTXDZ = query.FieldByName("LXRTXDZ").AsString;
            obj.sPYM = query.FieldByName("PYM").AsString;
            obj.iDJR = query.FieldByName("DJR").AsInteger;
            obj.sDJRMC = query.FieldByName("DJRMC").AsString;
            obj.dDJSJ = FormatUtils.DatetimeToString(query.FieldByName("DJSJ").AsDateTime);
            obj.sLXREMAIL = query.FieldByName("LXREMAIL").AsString;
            obj.sZY = query.FieldByName("BZ").AsString;
            obj.sKHWZ = query.FieldByName("KHWZ").AsString;
            obj.iMDID = query.FieldByName("MDID").AsInteger;
            obj.iZXR = query.FieldByName("ZXR").AsInteger;
            obj.sZXRMC = query.FieldByName("ZXRMC").AsString;
            obj.dZXRQ = FormatUtils.DatetimeToString(query.FieldByName("ZXRQ").AsDateTime);
            return obj;
        }


        public String IndexCode(String IndexTxt)
        {
            String _Temp = null;
            for (int i = 0; i < IndexTxt.Length; i++)
                _Temp = _Temp + GetOneIndex(IndexTxt.Substring(i, 1));
            return _Temp;
        }

        private String GetOneIndex(String OneIndexTxt)
        {
            string msg = string.Empty;
            string exception_tp = string.Empty;
            if (Convert.ToChar(OneIndexTxt) >= 0 && Convert.ToChar(OneIndexTxt) < 256)
                return OneIndexTxt;
            else
            {
                Encoding gb2312 = Encoding.GetEncoding("gb2312");
                byte[] unicodeBytes = Encoding.Unicode.GetBytes(OneIndexTxt);
                byte[] gb2312Bytes = Encoding.Convert(Encoding.Unicode, gb2312, unicodeBytes);
                try
                {
                    GetX(Convert.ToInt32(String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160) + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)));
                }
                catch (Exception e)
                {
                    msg = e.Message;
                }
                if (msg != "")
                {
                    exception_tp = "X";
                    msg = string.Empty;
                }
                else
                {
                    exception_tp = GetX(Convert.ToInt32(String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[0]) - 160) + String.Format("{0:D2}", Convert.ToInt16(gb2312Bytes[1]) - 160)));
                }
                return exception_tp;
            }

        }


        private String GetX(int GBCode)
        {
            if (GBCode >= 1601 && GBCode < 1637) return "A";
            if (GBCode >= 1637 && GBCode < 1833) return "B";
            if (GBCode >= 1833 && GBCode < 2078) return "C";
            if (GBCode >= 2078 && GBCode < 2274) return "D";
            if (GBCode >= 2274 && GBCode < 2302) return "E";
            if (GBCode >= 2302 && GBCode < 2433) return "F";
            if (GBCode >= 2433 && GBCode < 2594) return "G";
            if (GBCode >= 2594 && GBCode < 2787) return "H";
            if (GBCode >= 2787 && GBCode < 3106) return "J";
            if (GBCode >= 3106 && GBCode < 3212) return "K";
            if (GBCode >= 3212 && GBCode < 3472) return "L";
            if (GBCode >= 3472 && GBCode < 3635) return "M";
            if (GBCode >= 3635 && GBCode < 3722) return "N";
            if (GBCode >= 3722 && GBCode < 3730) return "O";
            if (GBCode >= 3730 && GBCode < 3858) return "P";
            if (GBCode >= 3858 && GBCode < 4027) return "Q";
            if (GBCode >= 4027 && GBCode < 4086) return "R";
            if (GBCode >= 4086 && GBCode < 4390) return "S";
            if (GBCode >= 4390 && GBCode < 4558) return "T";
            if (GBCode >= 4558 && GBCode < 4684) return "W";
            if (GBCode >= 4684 && GBCode < 4925) return "X";
            if (GBCode >= 4925 && GBCode < 5249) return "Y";
            if (GBCode >= 5249 && GBCode <= 5589) return "Z";
            if (GBCode >= 5601 && GBCode <= 8794)
            {
                String CodeData = "cjwgnspgcenegypbtwxzdxykygtpjnmjqmbsgzscyjsyyfpggbzgydywjkgaljswkbjqhyjwpdzlsgmr"
                 + "ybywwccgznkydgttngjeyekzydcjnmcylqlypyqbqrpzslwbdgkjfyxjwcltbncxjjjjcxdtqsqzycdxxhgckbphffss"
                 + "pybgmxjbbyglbhlssmzmpjhsojnghdzcdklgjhsgqzhxqgkezzwymcscjnyetxadzpmdssmzjjqjyzcjjfwqjbdzbjgd"
                 + "nzcbwhgxhqkmwfbpbqdtjjzkqhylcgxfptyjyyzpsjlfchmqshgmmxsxjpkdcmbbqbefsjwhwwgckpylqbgldlcctnma"
                 + "eddksjngkcsgxlhzaybdbtsdkdylhgymylcxpycjndqjwxqxfyyfjlejbzrwccqhqcsbzkymgplbmcrqcflnymyqmsqt"
                 + "rbcjthztqfrxchxmcjcjlxqgjmshzkbswxemdlckfsydsglycjjssjnqbjctyhbftdcyjdgwyghqfrxwckqkxebpdjpx"
                 + "jqsrmebwgjlbjslyysmdxlclqkxlhtjrjjmbjhxhwywcbhtrxxglhjhfbmgykldyxzpplggpmtcbbajjzyljtyanjgbj"
                 + "flqgdzyqcaxbkclecjsznslyzhlxlzcghbxzhznytdsbcjkdlzayffydlabbgqszkggldndnyskjshdlxxbcghxyggdj"
                 + "mmzngmmccgwzszxsjbznmlzdthcqydbdllscddnlkjyhjsycjlkohqasdhnhcsgaehdaashtcplcpqybsdmpjlpcjaql"
                 + "cdhjjasprchngjnlhlyyqyhwzpnccgwwmzffjqqqqxxaclbhkdjxdgmmydjxzllsygxgkjrywzwyclzmcsjzldbndcfc"
                 + "xyhlschycjqppqagmnyxpfrkssbjlyxyjjglnscmhcwwmnzjjlhmhchsyppttxrycsxbyhcsmxjsxnbwgpxxtaybgajc"
                 + "xlypdccwqocwkccsbnhcpdyznbcyytyckskybsqkkytqqxfcwchcwkelcqbsqyjqcclmthsywhmktlkjlychwheqjhtj"
                 + "hppqpqscfymmcmgbmhglgsllysdllljpchmjhwljcyhzjxhdxjlhxrswlwzjcbxmhzqxsdzpmgfcsglsdymjshxpjxom"
                 + "yqknmyblrthbcftpmgyxlchlhlzylxgsssscclsldclepbhshxyyfhbmgdfycnjqwlqhjjcywjztejjdhfblqxtqkwhd"
                 + "chqxagtlxljxmsljhdzkzjecxjcjnmbbjcsfywkbjzghysdcpqyrsljpclpwxsdwejbjcbcnaytmgmbapclyqbclzxcb"
                 + "nmsggfnzjjbzsfqyndxhpcqkzczwalsbccjxpozgwkybsgxfcfcdkhjbstlqfsgdslqwzkxtmhsbgzhjcrglyjbpmljs"
                 + "xlcjqqhzmjczydjwbmjklddpmjegxyhylxhlqyqhkycwcjmyhxnatjhyccxzpcqlbzwwwtwbqcmlbmynjcccxbbsnzzl"
                 + "jpljxyztzlgcldcklyrzzgqtgjhhgjljaxfgfjzslcfdqzlclgjdjcsnclljpjqdcclcjxmyzftsxgcgsbrzxjqqcczh"
                 + "gyjdjqqlzxjyldlbcyamcstylbdjbyregklzdzhldszchznwczcllwjqjjjkdgjcolbbzppglghtgzcygezmycnqcycy"
                 + "hbhgxkamtxyxnbskyzzgjzlqjdfcjxdygjqjjpmgwgjjjpkjsbgbmmcjssclpqpdxcdyykypcjddyygywchjrtgcnyql"
                 + "dkljczzgzccjgdyksgpzmdlcphnjafyzdjcnmwescsglbtzcgmsdllyxqsxsbljsbbsgghfjlwpmzjnlyywdqshzxtyy"
                 + "whmcyhywdbxbtlmswyyfsbjcbdxxlhjhfpsxzqhfzmqcztqcxzxrdkdjhnnyzqqfnqdmmgnydxmjgdhcdycbffallztd"
                 + "ltfkmxqzdngeqdbdczjdxbzgsqqddjcmbkxffxmkdmcsychzcmljdjynhprsjmkmpcklgdbqtfzswtfgglyplljzhgjj"
                 + "gypzltcsmcnbtjbhfkdhbyzgkpbbymtdlsxsbnpdkleycjnycdykzddhqgsdzsctarlltkzlgecllkjljjaqnbdggghf"
                 + "jtzqjsecshalqfmmgjnlyjbbtmlycxdcjpldlpcqdhsycbzsckbzmsljflhrbjsnbrgjhxpdgdjybzgdlgcsezgxlblg"
                 + "yxtwmabchecmwyjyzlljjshlgndjlslygkdzpzxjyyzlpcxszfgwyydlyhcljscmbjhblyjlycblydpdqysxktbytdkd"
                 + "xjypcnrjmfdjgklccjbctbjddbblblcdqrppxjcglzcshltoljnmdddlngkaqakgjgyhheznmshrphqqjchgmfprxcjg"
                 + "dychghlyrzqlcngjnzsqdkqjymszswlcfqjqxgbggxmdjwlmcrnfkkfsyyljbmqammmycctbshcptxxzzsmphfshmclm"
                 + "ldjfyqxsdyjdjjzzhqpdszglssjbckbxyqzjsgpsxjzqznqtbdkwxjkhhgflbcsmdldgdzdblzkycqnncsybzbfglzzx"
                 + "swmsccmqnjqsbdqsjtxxmbldxcclzshzcxrqjgjylxzfjphymzqqydfqjjlcznzjcdgzygcdxmzysctlkphtxhtlbjxj"
                 + "lxscdqccbbqjfqzfsltjbtkqbsxjjljchczdbzjdczjccprnlqcgpfczlclcxzdmxmphgsgzgszzqjxlwtjpfsyaslcj"
                 + "btckwcwmytcsjjljcqlwzmalbxyfbpnlschtgjwejjxxglljstgshjqlzfkcgnndszfdeqfhbsaqdgylbxmmygszldyd"
                 + "jmjjrgbjgkgdhgkblgkbdmbylxwcxyttybkmrjjzxqjbhlmhmjjzmqasldcyxyqdlqcafywyxqhz";
                String _gbcode = GBCode.ToString();
                int pos = (Convert.ToInt16(_gbcode.Substring(0, 2)) - 56) * 94 + Convert.ToInt16(_gbcode.Substring(_gbcode.Length - 2, 2));
                return CodeData.Substring(pos - 1, 1);
            }
            return " ";
        }
    }
}
