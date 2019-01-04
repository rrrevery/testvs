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
using System.Configuration;
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                Log(dbname + ":" + ex.Message);
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
                query.SQL.Text = "select * from HYKDEF where HYKTYPE=101";
                query.Open();
                Log("整数类型按字符串：" + query.FieldByName("HYKTYPE").AsString);
                Log("浮点类型按字符串：" + query.FieldByName("KFJE").AsString);
                Log("字符串类型按数字" + query.FieldByName("HYKNAME").AsInteger.ToString());
                query.Close();
            }
            catch (Exception ex)
            {
                Log(dbname + ":" + ex.Message);
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
                query.SQL.Text = "select * from HYK_JFFHLPJHD where JLBH=14";
                query.Open();
                Log("Float类型：" + query.FieldByName("ZSL").AsFloat.ToString());
                query.Close();
            }
            catch (Exception ex)
            {
                Log(dbname + ":" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            byte[] bytes = FormatUtils.StrToPeriodBytes(tbSD_NUM.Text.TrimEnd(';'));
            tbSD_HEX.Text = BitConverter.ToString(bytes, 0).Replace("-", "");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string dbname = "ODBC_HANA";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "select * from CRM_HYKDEF where HYKTYPE=:HYKTYPE";
                query.ParamByName("HYKTYPE").AsInteger = 306;
                query.Open();
                Log("Int类型：" + query.FieldByName("HYKTYPE").AsInteger.ToString());
                Log("String类型：" + query.FieldByName("HYKNAME").AsString);
                Log("Float类型：" + query.FieldByName("KFJE").AsFloat.ToString());
                query.Close();
                //query.SQL.Text = "select current_timestamp from dummy";
                //query.Open();
                //Log(query.Fields[0].AsDateTime.ToString());
                DateTime dt = CyDbSystem.GetDbServerTime(query);
                Log(dt.ToString());
                query.Close();
            }
            catch (Exception ex)
            {
                Log(dbname + ":" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string dbname = "MSSQL";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "insert into SHDY(SHDM,SHMC) values(:SHDM,:SHMC)";
                query.ParamByName("SHDM").AsString = "BH";
                query.ParamByName("SHMC").AsString = "百货";
                query.ExecSQL();
                query.SQL.Text = "select * from SHDY";
                query.Open();
                Log("SHDM：" + query.FieldByName("SHDM").AsString);
                Log("SHMC：" + query.FieldByName("SHMC").AsString);
                query.Close();
            }
            catch (Exception ex)
            {
                Log(dbname + ":" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string dbname = "MSSQLLocal";
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                query.SQL.Text = "insert into TB1(ID,NAME,CODE,CREATETIME) values(newid(),:NAME,:CODE,getdate())";
                query.ParamByName("NAME").AsString = "名称";
                query.ParamByName("CODE").AsInteger = 0;
                query.ExecSQL();
                query.SQL.Text = "select * from TB1";
                query.Open();
                Log("NAME：" + query.FieldByName("NAME").AsString);
                Log("CODE：" + query.FieldByName("CODE").AsInteger);
                query.Close();
                DateTime be = DateTime.Now;
                for (int i = 1; i <= 1000; i++)
                {
                    query.SQL.Text = "insert into TB1(ID,NAME,CODE,CREATETIME) values(newid(),:NAME,:CODE,getdate())";
                    query.ParamByName("NAME").AsString = "名称" + i;
                    query.ParamByName("CODE").AsInteger = i;
                    query.ExecSQL();
                    query.SQL.Text = "insert into TB2(ID,VALUE,COUNT) values(newid(),:VALUE,:COUNT)";
                    query.ParamByName("VALUE").AsFloat = DateTime.Now.Millisecond;
                    query.ParamByName("COUNT").AsInteger = DateTime.Now.Millisecond;
                    query.ExecSQL();
                }
                DateTime en = DateTime.Now;
                Log("10万插入耗时：" + (en - be).TotalMilliseconds.ToString());
                be = DateTime.Now;
                query.SQL.Text = "select count(*) from TB1 where CODE>=1 and CODE<=100000";
                query.Open();
                en = DateTime.Now;
                Log("10万Count耗时：" + (en - be).TotalMilliseconds.ToString());
                //be = DateTime.Now;
                //query.SQL.Text = "delete from TB1 where CODE>=1 and CODE<=100000";
                //query.ExecSQL();
                //en = DateTime.Now;
                //Log("10万删除耗时：" + (en - be).TotalMilliseconds.ToString());
            }
            catch (Exception ex)
            {
                Log(dbname + ":" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
                return;
            if (textBox5.Text == "")
                return;
            string dbname = comboBox1.Text;
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(dbname);
            try
            {
                CyQuery query = new CyQuery(conn);
                if (textBox5.SelectedText == "")
                    query.SQL.Text = textBox5.Text;
                else
                    query.SQL.Text = textBox5.SelectedText;
                query.Open();
                //int i = query.ExecSQL();
                //if (i == -1)
                //{
                //    query.Open();
                //    Log("尼玛还不支持Open");
                //}
                //else
                //{
                //    Log(i + " row(s) effected");
                //}
            }
            catch (Exception ex)
            {
                Log(dbname + ":" + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (ConnectionStringSettings one in ConfigurationManager.ConnectionStrings)
                comboBox1.Items.Add(one.Name);
        }
    }
}
