using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.Common;
using BF.Pub;

namespace TransData2
{
    public partial class FormMain : Form
    {
        public List<JXCDB> JXCList = new List<JXCDB>();
        public List<TransData> TransList = new List<TransData>();
        public string TblSuffix = "";//_CRM _TRAN

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化业务库信息
            JXCConfig config = ConfigurationManager.GetSection("JXCConfig") as JXCConfig;
            for (int i = 0; i < config.JXC.Count; i++)
            {
                lstJXC.Items.Add(config.JXC[i].JXCName);
                JXCDB one = new JXCDB();
                one.JXCName = config.JXC[i].JXCName;
                one.Connection = config.JXC[i].Connection;
                one.SHDM = config.JXC[i].SHDM;
                JXCList.Add(one);
            }
            //初始传输内容，以后传输程序将不处理过多的SQL，条件和关联尽量在视图里写
            TransData bm = new TransData();
            bm.Name = "部门";
            bm.Tbl = "BM";
            bm.CRMTbl = "SHBM";
            TransList.Add(bm);

            TransData ht = new TransData();
            ht.Name = "合同";
            ht.Tbl = "HT";
            ht.CRMTbl = "SHHT";
            TransList.Add(ht);

            TransData fl = new TransData();
            fl.Name = "商品分类";
            fl.Tbl = "SPFL";
            fl.CRMTbl = "SHSPFL";
            TransList.Add(fl);

            TransData sb = new TransData();
            sb.Name = "商标";
            sb.Tbl = "SPSB";
            sb.CRMTbl = "SHSPSB";
            TransList.Add(sb);

            TransData sp = new TransData();
            sp.Name = "商品信息";
            sp.Tbl = "SPXX";
            sp.CRMTbl = "SHSPXX";
            TransList.Add(sp);

            TransData gtsp = new TransData();
            gtsp.Name = "柜台商品";
            gtsp.Tbl = "GTSP";
            TransList.Add(gtsp);

            TransData skfs = new TransData();
            skfs.Name = "收款方式";
            skfs.Tbl = "SKFS";
            skfs.CRMTbl = "SHZFFS";
            TransList.Add(skfs);

            //TransData cxhd = new TransData();
            //cxhd.Name = "促销活动";
            //cxhd.Tbl = "CXHDDEF";
            //cxhd.CRMTbl = "CXHDDEF";
            //TransList.Add(cxhd);
            foreach (TransData one in TransList)
            {
                lstTran.Items.Add(one.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoTransData();
        }

        private void DoTransData()
        {
            foreach (JXCDB jxc in JXCList)
            {
                DoTransDataJXC(jxc);
            }
        }
        private void DoTransDataJXC(JXCDB jxcdb)
        {
            AddLog("开始传输[" + jxcdb.JXCName + "]的数据……");
            int tot = 0;
            //连接进销存库，Sybase和Oracle的TM方式不一样，所以要区分
            DbConnection connjxc = CyDbConnManager.GetActiveDbConnection(jxcdb.Connection);
            CyQuery queryjxc = new CyQuery(connjxc);

            //连接CRM库
            DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
            CyQuery query = new CyQuery(conn);

            try
            {
                foreach (TransData one in TransList)
                {
                    AddLog("开始传输[" + jxcdb.JXCName + "]的[" + one.Name + "]数据……");
                    int rec = 0;
                    if (one.CRMTbl != "")
                        rec = SeqGenerator.GetSeq(one.CRMTbl);
                    int ct = 0;
                    queryjxc.SQL.Text = "select * from " + one.Tbl + TblSuffix + " where 1=1";
                    if (ConfigurationManager.AppSettings["USETM"] == "true")
                    {
                        queryjxc.SQL.Add("and TM>(select STAMP_OLD from CRMSTAMP where TBL_NAME=:TBL_NAME)");
                        queryjxc.ParamByName("TBL_NAME").AsString = one.Tbl;
                    }
                    queryjxc.SQL.Add("order by TM");
                    queryjxc.Open();
                    while (!queryjxc.Eof)
                    {
                        switch (one.Tbl)
                        {
                            case "BM": SaveBM(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "HT": SaveHT(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "SPFL": SaveFL(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "SPSB": SaveSB(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "SPXX": SaveSP(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "GTSP": SaveBMSP(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "SKFS": SaveSKFS(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                            case "CXHDDEF": SaveCXHD(query, queryjxc, jxcdb.SHDM, rec + ct); break;
                        }
                        ct++;
                        queryjxc.Next();
                    }
                    //保存TM
                    //
                    //批量改编号状态
                    if (ct > 1)
                        SeqGenerator.GetSeq(one.CRMTbl, ct - 1);
                    tot += ct;
                    AddLog("完成传输[" + jxcdb.JXCName + "]的[" + one.Name + "]数据，共" + ct + "条。", 1);
                    MessageBox.Show("a");
                }
                AddLog("完成传输[" + jxcdb.JXCName + "]的数据，共" + tot + "条。", 1);
            }
            catch (Exception ex)
            {
                AddLog("传输出现错误：" + ex.Message, 2);
            }
            finally
            {
                queryjxc.Close();
                connjxc.Close();
                query.Close();
                conn.Close();
            }
        }

        private void SaveBM(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.SQL.Text = "update SHBM set BMMC=:BMMC,BMQC=:BMQC,DEPT_TYPE=:DEPT_TYPE where SHDM=:SHDM and BMDM=:BMDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("BMDM").AsString = queryjxc.FieldByName("BMDM").AsString;
            query.ParamByName("BMMC").AsString = queryjxc.FieldByName("DEPT_NAME").AsString;
            query.ParamByName("BMQC").AsString = queryjxc.FieldByName("DEPT_NAME").AsString;
            query.ParamByName("DEPT_TYPE").AsInteger = queryjxc.FieldByName("DEPT_TYPE").AsInteger;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into SHBM(SHBMID,SHDM,BMDM,BMMC,BMQC,DEPT_TYPE) values(:SHBMID,:SHDM,:BMDM,:BMMC,:BMQC,:DEPT_TYPE)";
                query.ParamByName("SHDM").AsString = SHDM;
                query.ParamByName("SHBMID").AsInteger = rec;
                query.ParamByName("BMDM").AsString = queryjxc.FieldByName("BMDM").AsString;
                query.ParamByName("BMMC").AsString = queryjxc.FieldByName("DEPT_NAME").AsString;
                query.ParamByName("BMQC").AsString = queryjxc.FieldByName("DEPT_NAME").AsString;
                query.ParamByName("DEPT_TYPE").AsInteger = queryjxc.FieldByName("DEPT_TYPE").AsInteger;
                query.ExecSQL();
            }
        }
        private void SaveHT(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.Close();
            query.SQL.Text = "select SHBMID from SHBM where SHDM=:SHDM and BMDM=:BMDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("BMDM").AsString = queryjxc.FieldByName("DEPTID").AsString;
            query.Open();
            if (query.IsEmpty)
                throw new Exception("部门代码(" + queryjxc.FieldByName("DEPTID").AsString + ")不存在");
            int bm = query.Fields[0].AsInteger;
            query.Close();

            query.SQL.Text = "update SHHT set GHSDM=:GHSDM,GSHMC=:GSHMC,SHBMID=:SHBMID,BJ_YX=:BJ_YX";
            query.SQL.Add("where SHDM=:SHDM and HTH=:HTH");
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("HTH").AsString = queryjxc.FieldByName("HTH").AsString;
            query.ParamByName("GSHMC").AsString = queryjxc.FieldByName("NAME").AsString;
            query.ParamByName("GHSDM").AsString = queryjxc.FieldByName("GHDWDM").AsString;
            query.ParamByName("BJ_YX").AsInteger = queryjxc.FieldByName("BJ_YX").AsInteger;
            query.ParamByName("BMDM").AsString = queryjxc.FieldByName("DEPTID").AsString;
            query.ParamByName("SHBMID").AsInteger = bm;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into SHHT(SHHTID,SHDM,HTH,GHSDM,GSHMC,SHBMID,BJ_YX) values(:SHHTID,:SHDM,:HTH,:GHSDM,:GSHMC,:SHBMID,:BJ_YX)";
                query.SQL.Add("from SHBM where BMDM=:BMDM");
                query.ParamByName("SHDM").AsString = SHDM;
                query.ParamByName("SHHTID").AsInteger = rec;
                query.ParamByName("HTH").AsString = queryjxc.FieldByName("HTH").AsString;
                query.ParamByName("GSHMC").AsString = queryjxc.FieldByName("NAME").AsString;
                query.ParamByName("GHSDM").AsString = queryjxc.FieldByName("GHDWDM").AsString;
                query.ParamByName("BJ_YX").AsInteger = queryjxc.FieldByName("BJ_YX").AsInteger;
                query.ParamByName("BMDM").AsString = queryjxc.FieldByName("DEPTID").AsString;
                query.ParamByName("SHBMID").AsInteger = bm;
                query.ExecSQL();
            }

        }
        private void SaveFL(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.SQL.Text = "update SHSPFL set SPFLDM=:SPFLDM,SPFLMC=:SPFLMC,PYM=:PYM,MJBJ=:MJBJ where SHDM=:SHDM and SPFLDM=:SPFLDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("SPFLDM").AsString = queryjxc.FieldByName("SPFL").AsString;
            query.ParamByName("SPFLMC").AsString = queryjxc.FieldByName("NAME").AsString;
            query.ParamByName("PYM").AsString = queryjxc.FieldByName("SPFL").AsString;
            query.ParamByName("MJBJ").AsInteger = 0;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into SHSPFL(SHSPFLID,SHDM,SPFLDM,SPFLMC,PYM,MJBJ) values(:SHSPFLID,:SHDM,:SPFLDM,:SPFLMC,:PYM,:MJBJ)";
                query.ParamByName("SHDM").AsString = SHDM;
                query.ParamByName("SHSPFLID").AsInteger = rec;
                query.ParamByName("SPFLDM").AsString = queryjxc.FieldByName("SPFL").AsString;
                query.ParamByName("SPFLMC").AsString = queryjxc.FieldByName("NAME").AsString;
                query.ParamByName("PYM").AsString = queryjxc.FieldByName("SPFL").AsString;
                query.ParamByName("MJBJ").AsInteger = 0;
                query.ExecSQL();
            }
        }
        private void SaveSB(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.SQL.Text = "update SHSPSB set SBDM=:SBDM,SBMC=:SBMC,PYM=:PYM,SYZ=:SYZ,MJBJ=:MJBJ where SHDM=:SHDM and SBDM=:SBDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("SBDM").AsString = queryjxc.FieldByName("SPFL").AsString;
            query.ParamByName("SBMC").AsString = queryjxc.FieldByName("SBID").AsInteger.ToString();
            query.ParamByName("PYM").AsString = queryjxc.FieldByName("PYM").AsString;
            query.ParamByName("SYZ").AsString = queryjxc.FieldByName("SYZ").AsString;
            query.ParamByName("MJBJ").AsInteger = 1;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into SHSPSB(SHSBID,SHDM,SBDM,SBMC,PYM,SYZ,MJBJ) values(:SHSBID,:SHDM,:SBDMNEW,:SBMC,:PYM,:SYZ,:MJBJ) ";
                query.ParamByName("SHDM").AsString = SHDM;
                query.ParamByName("SHSBID").AsInteger = rec;
                query.ParamByName("SBDM").AsString = queryjxc.FieldByName("SPFL").AsString;
                query.ParamByName("SBMC").AsString = queryjxc.FieldByName("SBID").AsInteger.ToString();
                query.ParamByName("PYM").AsString = queryjxc.FieldByName("PYM").AsString;
                query.ParamByName("SYZ").AsString = queryjxc.FieldByName("SYZ").AsString;
                query.ParamByName("MJBJ").AsInteger = 1;
                query.ExecSQL();
            }
        }
        private void SaveSP(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.Close();
            query.SQL.Text = "select SHHTID from SHHT where SHDM=:SHDM and HTH=:HTH";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("HTH").AsString = queryjxc.FieldByName("HTH").AsString;
            query.Open();
            if (query.IsEmpty)
                throw new Exception("合同号(" + queryjxc.FieldByName("HTH").AsString + ")不存在");
            int ht = query.Fields[0].AsInteger;

            query.Close();
            query.SQL.Text = "select SHSBID from SHSPSB where SHDM=:SHDM and SBDM=:SBDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("SBDM").AsString = queryjxc.FieldByName("SB").AsString;
            query.Open();
            if (query.IsEmpty)
                throw new Exception("商标(" + queryjxc.FieldByName("SB").AsString + ")不存在");
            int sb = query.Fields[0].AsInteger;

            query.Close();
            query.SQL.Text = "select SHSPFLID from SHSPFL where SHDM=:SHDM and SPFLDM=:SPFLDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("SPFLDM").AsString = queryjxc.FieldByName("SPFL").AsString;
            query.Open();
            if (query.IsEmpty)
                throw new Exception("商品分类(" + queryjxc.FieldByName("SPFL").AsString + ")不存在");
            int fl = query.Fields[0].AsInteger;
            query.Close();

            query.SQL.Text = "update SHSPXX set SPDM=:SPDM,SPMC=:SPMC,SPJC=:SPJC,PYM=:PYM,UNIT=:UNIT,SPHS=:SPHS,SPGG=:SPGG,HH=:HH,";
            query.SQL.Add("SHSPFLID=:SHSPFLID,SHSBID=:SHSBID,SHHTID=:SHHTID where SHDM=:SHDM and SPDM=:SPDM");
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("SPDM").AsString = queryjxc.FieldByName("SPCODE").AsString;
            query.ParamByName("SPMC").AsString = queryjxc.FieldByName("NAME").AsString;
            query.ParamByName("SPJC").AsString = queryjxc.FieldByName("NAME_QC").AsString;
            query.ParamByName("PYM").AsString = queryjxc.FieldByName("PYM").AsString;
            query.ParamByName("UNIT").AsString = queryjxc.FieldByName("UNIT").AsString;
            query.ParamByName("SPHS").AsString = queryjxc.FieldByName("SPHS").AsString;
            query.ParamByName("SPGG").AsString = queryjxc.FieldByName("SPGG").AsString;
            query.ParamByName("HH").AsString = queryjxc.FieldByName("HH").AsString;
            query.ParamByName("SHSPFLID").AsInteger = fl;
            query.ParamByName("SHSBID").AsInteger = sb;
            query.ParamByName("SHHTID").AsInteger = ht;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into SHSPXX(SHSPID,SHDM,SPDM,SPMC,SPJC,PYM,UNIT,SPHS,SPGG,HH,SHSPFLID,SHSBID,SHHTID)";
                query.SQL.Add("values(:SHSPID,:SHDM,:SPDM,:SPMC,:SPJC,:PYM,:UNIT,:SPHS,:SPGG,:HH,:SHSPFLID,:SHSBID,:SHHTID)");
                query.ParamByName("SHDM").AsString = SHDM;
                query.ParamByName("SHSPID").AsInteger = rec;
                query.ParamByName("SPDM").AsString = queryjxc.FieldByName("SPCODE").AsString;
                query.ParamByName("SPMC").AsString = queryjxc.FieldByName("NAME").AsString;
                query.ParamByName("SPJC").AsString = queryjxc.FieldByName("NAME_QC").AsString;
                query.ParamByName("PYM").AsString = queryjxc.FieldByName("PYM").AsString;
                query.ParamByName("UNIT").AsString = queryjxc.FieldByName("UNIT").AsString;
                query.ParamByName("SPHS").AsString = queryjxc.FieldByName("SPHS").AsString;
                query.ParamByName("SPGG").AsString = queryjxc.FieldByName("SPGG").AsString;
                query.ParamByName("HH").AsString = queryjxc.FieldByName("HH").AsString;
                query.ParamByName("SHSPFLID").AsInteger = fl;
                query.ParamByName("SHSBID").AsInteger = sb;
                query.ParamByName("SHHTID").AsInteger = ht;
                query.ExecSQL();
            }
        }
        private void SaveBMSP(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.Close();
            query.SQL.Text = "select SHBMID from SHBM where SHDM=:SHDM and BMDM=:BMDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("BMDM").AsString = queryjxc.FieldByName("DEPTID").AsString;
            query.Open();
            if (query.IsEmpty)
                throw new Exception("部门代码(" + queryjxc.FieldByName("DEPTID").AsString + ")不存在");
            int bm = query.Fields[0].AsInteger;

            query.Close();
            query.SQL.Text = "select SHSPID from SHSPXX where SHDM=:SHDM and SPDM=:SPDM";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("SPDM").AsString = queryjxc.FieldByName("SPCODE").AsString;
            query.Open();
            if (query.IsEmpty)
                throw new Exception("商品代码(" + queryjxc.FieldByName("SPCODE").AsString + ")不存在");
            int sp = query.Fields[0].AsInteger;
            query.Close();

            query.SQL.Text = "update BMSP set SHBMID=:SHBMID where SHBMID=:SHBMID and SHSPID=:SHSPID";
            query.ParamByName("SHBMID").AsInteger = bm;
            query.ParamByName("SHSPID").AsInteger = sp;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into BMSP(SHBMID,SHSPID) values(:SHBMID,:SHSPID)";
                query.ParamByName("SHBMID").AsInteger = bm;
                query.ParamByName("SHSPID").AsInteger = sp;
                query.ExecSQL();
            }
        }
        private void SaveSKFS(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            query.SQL.Text = "update SHZFFS set ZFFSDM = :ZFFSDMNEW,ZFFSMC = :ZFFSMC,MJBJ = :MJBJ,YHQID = :YHQID,BJ_MBJZ = :BJ_MBJZ where SHDM = :SHDM and ZFFSDM = :ZFFSDM ";
            query.ParamByName("SHDM").AsString = SHDM;
            query.ParamByName("ZFFSDM").AsString = queryjxc.FieldByName("CODE").AsString;
            query.ParamByName("ZFFSMC").AsString = queryjxc.FieldByName("NAME").AsString;
            query.ParamByName("MJBJ").AsInteger = 1;
            if (!queryjxc.FieldByName("YHQID").IsNull)
                query.ParamByName("YHQID").AsInteger = queryjxc.FieldByName("YHQID").AsInteger;
            else
            {
                //query.ParamByName("YHQID").DataType = DbType.Int32;
                query.ParamByName("YHQID").Value = DBNull.Value;
            }
            query.ParamByName("BJ_MBJZ").AsInteger = queryjxc.FieldByName("BJ_MBJZ").AsInteger;
            if (query.ExecSQL() == 0)
            {
                query.SQL.Text = "insert into SHZFFS(SHZFFSID,SHDM,ZFFSDM,ZFFSMC,MJBJ,YHQID,BJ_MBJZ) values(:SHZFFSID,:SHDM,:ZFFSDM,:ZFFSMC,:MJBJ,:YHQID,:BJ_MBJZ)";
                query.ParamByName("SHDM").AsString = SHDM;
                query.ParamByName("SHZFFSID").AsInteger = rec;
                query.ParamByName("ZFFSDM").AsString = queryjxc.FieldByName("CODE").AsString;
                query.ParamByName("ZFFSMC").AsString = queryjxc.FieldByName("NAME").AsString;
                query.ParamByName("MJBJ").AsInteger = 1;
                if (!queryjxc.FieldByName("YHQID").IsNull)
                    query.ParamByName("YHQID").AsInteger = queryjxc.FieldByName("YHQID").AsInteger;
                else
                {
                    //query.ParamByName("YHQID").DataType = DbType.Int32;
                    query.ParamByName("YHQID").Value = DBNull.Value;
                }
                query.ParamByName("BJ_MBJZ").AsInteger = queryjxc.FieldByName("BJ_MBJZ").AsInteger;
                query.ExecSQL();
            }
        }
        private void SaveCXHD(CyQuery query, CyQuery queryjxc, string SHDM, int rec)
        {
            //query.SQL.Text = "update SHZFFS set ZFFSDM = :ZFFSDMNEW,ZFFSMC = :ZFFSMC,MJBJ = :MJBJ,YHQID = :YHQID,BJ_MBJZ = :BJ_MBJZ where SHDM = :SHDM and ZFFSDM = :ZFFSDM ";
            //query.ParamByName("SHDM").AsString = SHDM;
            //query.ParamByName("ZFFSDM").AsString = queryjxc.FieldByName("CODE").AsString;
            //query.ParamByName("ZFFSMC").AsString = queryjxc.FieldByName("NAME").AsString;
            //query.ParamByName("MJBJ").AsInteger = 1;
            //if (!queryjxc.FieldByName("YHQID").IsNull)
            //    query.ParamByName("YHQID").AsInteger = queryjxc.FieldByName("YHQID").AsInteger;
            //else
            //{
            //    //query.ParamByName("YHQID").DataType = DbType.Int32;
            //    query.ParamByName("YHQID").Value = DBNull.Value;
            //}
            //query.ParamByName("BJ_MBJZ").AsInteger = queryjxc.FieldByName("BJ_MBJZ").AsInteger;
            //if (query.ExecSQL() == 0)
            //{
            //    query.SQL.Text = "insert into SHZFFS(SHZFFSID,SHDM,ZFFSDM,ZFFSMC,MJBJ,YHQID,BJ_MBJZ) values(:SHZFFSID,:SHDM,:ZFFSDM,:ZFFSMC,:MJBJ,:YHQID,:BJ_MBJZ)";
            //    query.ParamByName("SHDM").AsString = SHDM;
            //    query.ParamByName("SHZFFSID").AsInteger = rec;
            //    query.ParamByName("ZFFSDM").AsString = queryjxc.FieldByName("CODE").AsString;
            //    query.ParamByName("ZFFSMC").AsString = queryjxc.FieldByName("NAME").AsString;
            //    query.ParamByName("MJBJ").AsInteger = 1;
            //    if (!queryjxc.FieldByName("YHQID").IsNull)
            //        query.ParamByName("YHQID").AsInteger = queryjxc.FieldByName("YHQID").AsInteger;
            //    else
            //    {
            //        //query.ParamByName("YHQID").DataType = DbType.Int32;
            //        query.ParamByName("YHQID").Value = DBNull.Value;
            //    }
            //    query.ParamByName("BJ_MBJZ").AsInteger = queryjxc.FieldByName("BJ_MBJZ").AsInteger;
            //    query.ExecSQL();
            //}
        }

        private void AddLog(string s, int cl = 0)
        {
            s = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + s + "\r\n";
            //tbLog.Text += s;

            //cl 0提示1重要2错误
            int b = rtbLog.TextLength;
            rtbLog.Text += s;
            rtbLog.Select(b, s.Length - 2);
            switch (cl)
            {
                case 0: rtbLog.SelectionColor = Color.Black; break;
                case 1: rtbLog.SelectionColor = Color.Blue; break;
                case 2: rtbLog.SelectionColor = Color.Red; break;
            }
            rtbLog.Select(0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
