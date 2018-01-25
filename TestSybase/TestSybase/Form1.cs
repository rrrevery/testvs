using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using BF.Pub;

namespace TestSybase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Log(string str)
        {
            tbMsg.Text += str + "\r\n";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Func("SYB");
        }

        private void Func(string dbname)
        {
            DateTime be = DateTime.Now;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "update HYK_MDJF set WCLJF=WCLJF+round(:WCLJF,2)";
                query.SQL.Add(",BQJF=BQJF+round(:BDJF,2)");
                query.SQL.Add(",BNLJJF=BNLJJF+round(:BDJF,2),LBCTEST=:LBCTEST");
                query.SQL.Add("where 1=1");
                if (checkBox1.Checked)
                {
                    query.SQL.Add("and MDID=:MDID");
                    query.ParamByName("MDID").AsInteger = Convert.ToInt32(tbMDID.Text);
                }
                //query.SQL.Add("and ABC=:ABC");
                query.SQL.Add("and HYID=:HYID");

                query.ParamByName("WCLJF").AsFloat = Convert.ToDouble(tbWCLJF.Text);
                query.ParamByName("BDJF").AsFloat = Convert.ToDouble(tbBDJF.Text);
                //query.ParamByName("ABC").AsString = "abc";
                query.ParamByName("HYID").AsInteger = Convert.ToInt32(tbHYID.Text);
                query.ParamByName("LBCTEST").AsString = "猫咪";
                int a = query.ExecSQL();
                DateTime en = DateTime.Now;
                tbMsg.Text += dbname + ":OK(" + a.ToString() + ") " + (en - be).Milliseconds + "\r\n";
            }
            catch (Exception e)
            {
                tbMsg.Text += dbname + ":" + e.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Func("ORA");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbMsg.Text += (ConvertDateTimeInt(DateTime.Now) - ConvertDateTimeInt2(DateTime.Now)).ToString();
        }
        private static int ConvertDateTimeInt(System.DateTime time)
        {

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(2015, 1, 1));
            int timestamp = (int)(time - startTime).TotalSeconds + 1000000000;
            return timestamp;

        }
        private static int ConvertDateTimeInt2(System.DateTime time)
        {

            System.DateTime startTime = (new System.DateTime(2015, 1, 1));
            int timestamp = (int)(time - startTime).TotalSeconds + 1000000000;
            return timestamp;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string dbname = "ORA";
            List<string> hylst = new List<string>();
            DateTime t1 = DateTime.Now;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "select * from HYK_HYXX";
                query.SQL.Add("where 1=1");
                query.Open();
                int i = 0;
                while (!query.Eof)
                {
                    hylst.Add(query.FieldByName("HYID").AsInteger.ToString());
                    i++;
                    query.Next();
                    if (i > 100000)
                        break;
                }
                //DataTable dt = query.GetDataTable();
                //tbMsg.Text += dbname + ":OK(" + a.ToString() + ")\r\n";
                DateTime t2 = DateTime.Now;
                tbMsg.Text += (t2 - t1).TotalMilliseconds + "\r\n";

            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dbname = "ORA";
            List<string> hylst = new List<string>();
            DateTime t1 = DateTime.Now;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "select * from HYK_HYXX";
                query.SQL.Add("where rownum<=100000");
                DataTable dt = query.GetDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    hylst.Add(dt.Rows[i]["HYID"].ToString());
                }
                //tbMsg.Text += dbname + ":OK(" + a.ToString() + ")\r\n";
                DateTime t2 = DateTime.Now;
                tbMsg.Text += (t2 - t1).TotalMilliseconds + "\r\n";

            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Text = O2S(textBox1.Text);
        }
        public string O2S(string str)
        {
            int i = 0;
            string l = str.Substring(0, str.IndexOf("="));
            string r = str.Substring(str.IndexOf("=") + 1);
            if (l.IndexOf("(+)") >= 0)
            {
                l = l.Replace("(+)", "");
                i = 1;
            }
            else if (r.IndexOf("(+)") >= 0)
            {
                r = r.Replace("(+)", "");
                i = 2;
            }
            return l + (i == 2 ? "*" : "") + "=" + (i == 1 ? "*" : "") + r;
        }
        public string O2S_SQL(string str)
        {
            int i = 0;
            while (str.IndexOf("(+)") >= 0)
            {
                string cond = str.Substring(0, str.IndexOf("(+)") + 3);
                cond = cond.Substring(cond.LastIndexOf(" ") + 1);
                string newcond = O2S(cond);
                str = str.Replace(cond, newcond);
            }
            str = OracleFuncReplace(str);
            return str;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tbMsg.Text = O2S_SQL(textBox4.Text);
        }
        public static string OracleFuncReplace(string str)
        {
            str = OracleFuncStrReplace(str, "nvl", "isnull");
            str = OracleFuncStrReplace(str, "sysdate", "getdate()");
            return str;
        }
        private static string OracleFuncStrReplace(string str, string ora_str, string syb_str)
        {
            str = str.Replace(ora_str.ToLower(), syb_str);
            str = str.Replace(ora_str.Substring(0, 1).ToUpper() + ora_str.Substring(1).ToLower(), syb_str);
            str = str.Replace(ora_str.ToUpper(), syb_str);
            return str;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string dbname = "SYB";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "select * from HYK_HYXX";
                query.SQL.Add("where HYID<=100");
                query.Open();
                while (!query.Eof)
                {
                    Log(query.FieldByName("HY_NAME").AsString);
                    query.Next();
                }
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string dbname = "SYB";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "update HYK_HYXX set CDNR=:CDNR";
                query.SQL.Add("where HYID=3");
                query.ParamByName("CDNR").AsString = "长益吗";
                //query.ParamByName("CDNR").AsChineseString = "长益";
                query.ExecSQL();
                query.SQL.Text = "select * from HYK_HYXX where HYID=3";
                query.Open();
                Log(query.FieldByName("CDNR").AsString);
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string dbname = "SYB";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "select * from TESTTEXT";
                query.Open();
                Log(query.FieldByName("VAL").AsString);
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string dbname = "SYB";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "update TESTTEXT set VAL=:VAL";
                query.ParamByName("VAL").SetParamValue(DbType.String, Encoding.GetEncoding(936).GetBytes("长益呗呗"));
                //query.ParamByName("VAL").AsString = "长益呗呗";
                query.ExecSQL();
                query.SQL.Text = "select * from TESTTEXT";
                query.Open();
                Log(query.FieldByName("VAL").AsString);
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string dbname = "ORA";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = textBox3.Text;
                query.Open();
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string dbname = "ORA";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = textBox3.Text;
                query.ExecSQL();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string dbname = "ORA_NEW";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                //query.SQL.Text = "update HYKCARD A set A.SKJLBH=:SKJLBH";
                //query.SQL.Add(" where A.CZKHM between :CZKHM_BEGIN and :CZKHM_END");
                //query.SQL.Add(" and length(A.CZKHM) = length(:CZKHM_BEGIN)");
                //query.SQL.Add(" and BGDDDM =:BGDDDM");
                //query.SQL.Add(" and exists(select 1 from HYKDEF B where B.HYKTYPE=A.HYKTYPE and B.BJ_CZK=1)");
                //query.ParamByName("SKJLBH").AsInteger = 1;
                //query.ParamByName("CZKHM_BEGIN").AsString = "aa";
                //query.ParamByName("CZKHM_END").AsString = "aa";
                //query.ParamByName("BGDDDM").AsString = "aa";

                query.SQL.Text = "begin";
                query.SQL.Add("delete MZK_JEZCLJL where HYID=:pHYID;");
                query.SQL.Add("delete MZK_YEBD where HYID=:pHYID;");
                //query.SQL.Add("delete MZK_JEZH where HYID=:pHYID;");
                //query.SQL.Add("delete MZKXX where HYID=:pHYID;");
                query.SQL.Add("end;");
                query.ParamByName("pHYID").AsInteger = 1;

                int a = query.ExecSQL();
                tbMsg.Text += dbname + ":YES(" + a.ToString() + ")\r\n";
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            Func("ORA_NEW");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string dbname = "ORA_NEW";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Add("select * from HYKDEF where HYKTYPE=101");
                query.Open();
                Log("整数类型按字符串：" + query.FieldByName("HYKTYPE").AsString);
                Log("浮点类型按字符串：" + query.FieldByName("KFJE").AsString);
                Log("字符串类型按数字" + query.FieldByName("HYKNAME").AsInteger.ToString());
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string dbname = "ORA_NEW";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Add("select * from HYK_JFFHLPJHD where JLBH=14");
                query.Open();
                Log("Float类型：" + query.FieldByName("ZSL").AsFloat.ToString());
                query.Close();
            }
            catch (Exception ex)
            {
                tbMsg.Text += dbname + ":" + ex.Message + "\r\n";
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
