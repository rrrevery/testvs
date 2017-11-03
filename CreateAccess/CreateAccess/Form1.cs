using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ADOX;
using BF.Pub;

namespace CreateAccess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);

            OleDbConnection conna = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=a.mdb;");
            conna.Open();
            DbCommand cmd = conna.CreateCommand();
            StringBuilder sql = new StringBuilder();
            int tt = 0;
            try
            {
                //卡类型
                int ct = 0;
                AddLog("开始生成卡类型定义");
                cmd.CommandText = "delete from HYKDEF";
                cmd.ExecuteNonQuery();

                query.SQL.Text = tbHYK.Text;// "select * from HYKDEF D where D.BJ_JF=1 and D.HYKTYPE>=1001";
                query.Open();
                while (!query.Eof)
                {
                    sql.Length = 0;
                    sql.Append("insert into HYKDEF(HYKTYPE,HYKNAME,BJ_YHQZH,BJ_CZZH,BJ_JF,YHFS) values(");
                    sql.Append(query.FieldByName("HYKTYPE").AsInteger).Append(",");
                    sql.Append("'" + query.FieldByName("HYKNAME").AsString + "',");
                    sql.Append(query.FieldByName("BJ_YHQZH").AsInteger).Append(",");
                    sql.Append(query.FieldByName("BJ_CZZH").AsInteger).Append(",");
                    sql.Append(query.FieldByName("BJ_JF").AsInteger).Append(",");
                    sql.Append(query.FieldByName("YHFS").AsInteger).Append(")");
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                    ct++;
                    query.Next();
                }
                AddLog("生成卡类型定义共" + ct + "条");

                //会员信息
                ct = 0;
                AddLog("开始生成会员信息");
                cmd.CommandText = "delete from HYK_HYXX";
                cmd.ExecuteNonQuery();

                query.SQL.Text = tbHYXX.Text;// "select * from HYK_HYXX H,HYKDEF D where H.HYKTYPE=D.HYKTYPE and D.BJ_JF=1 and D.HYKTYPE>=1001";
                query.Open();
                while (!query.Eof)
                {
                    sql.Length = 0;
                    sql.Append("insert into HYK_HYXX(HYID,HYKTYPE,HY_NAME,HYK_NO,CDNR,STATUS) values(");
                    sql.Append(query.FieldByName("HYID").AsInteger).Append(",");
                    sql.Append(query.FieldByName("HYKTYPE").AsInteger).Append(",");
                    sql.Append("'" + query.FieldByName("HY_NAME").AsString + "',");
                    sql.Append("'" + query.FieldByName("HYK_NO").AsString + "',");
                    sql.Append("'" + query.FieldByName("CDNR").AsString + "',");
                    sql.Append(query.FieldByName("STATUS").AsInteger).Append(")");
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                    ct++;
                    query.Next();
                }
                AddLog("生成会员信息共" + ct + "条");

                //顾客档案
                ct = 0;
                AddLog("开始生成顾客档案");
                cmd.CommandText = "delete from HYK_GRXX";
                cmd.ExecuteNonQuery();

                query.SQL.Text = tbGKDA.Text;// "select * from HYK_HYXX H,HYKDEF D,HYK_GKDA G where H.HYKTYPE=D.HYKTYPE and D.BJ_JF=1 and D.HYKTYPE>=1001 and H.GKID=G.GKID";
                query.Open();
                while (!query.Eof)
                {
                    sql.Length = 0;
                    sql.Append("insert into HYK_GRXX(HYID,SJHM,SFZBH) values(");
                    sql.Append(query.FieldByName("HYID").AsInteger).Append(",");
                    sql.Append("'" + EncryptUtils.EncryptWebData(query.FieldByName("SJHM").AsString, Encoding.ASCII.GetBytes("8498DHHZ")) + "',");
                    sql.Append("'" + EncryptUtils.EncryptWebData(query.FieldByName("SFZBH").AsString, Encoding.ASCII.GetBytes("8498DHHZ")) + "')");
                    //sql.Append("'" + query.FieldByName("SFZBH").AsString + "')");
                    //sql.Append("'" + FormatUtils.DateToString(query.FieldByName("CSRQ").AsDateTime) + "')");
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                    ct++;
                    query.Next();
                }
                AddLog("生成顾客档案共" + ct + "条");
                AddLog("生成完毕");
            }
            catch (Exception ex)
            {
                AddLog(ex.Message);
            }
            finally
            {
                query.Close();
                conn.Close();
                conna.Close();
            }
        }

        private void AddLog(string s)
        {
            s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + s + "\r\n";
            tbLog.Text += s;
        }
    }
}
