using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Runtime;
using System.Text;
using BF.Pub;
using BF.CrmProc;

namespace BF.CrmWeb.DE
{
    public partial class ExportKCKXX : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void B_Export_Click(object sender, EventArgs e)
        {
            string msg = "";
            CrmProc.KCKXX obj = new CrmProc.KCKXX();
            if(this.TB_HYKNAME.Text!="")
                obj.iHYKTYPE = Convert.ToInt32(this.HF_HYKTYPE.Value);
            obj.sCZKHM_BEGIN = this.TB_KSKH.Text;
            obj.sCZKHM_END = this.TB_JSKH.Text;
            if(this.TB_MZ.Text!="")
                obj.fQCYE = Convert.ToDouble( this.TB_MZ.Text);
            string DbName = this.HF_DbName.Value;
            //this.GridView1.DataSource = GetKCKList(out msg, obj);
            //this.GridView1.DataBind();
            //for (int count = 0; count < GridView1.Rows.Count; count++)//数字太长后几位会变为0
            //{
            //    GridView1.Rows[count].Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            //    GridView1.Rows[count].Cells[1].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
            //    //GridView1.Rows[count].Cells[8].Text = "'" + GridView1.Rows[count].Cells[8].Text;
            //}
            //Export(this.GridView1);
            DataTable tmpdt = GetKCKList(out msg, obj,DbName);
            string tmpstr = dataTableExportToText(tmpdt);
            string filename = "卡类型" + this.TB_HYKNAME.Text + "$面值" + this.TB_MZ.Text + "$卡号" + this.TB_KSKH.Text + "-" + this.TB_JSKH.Text + "$日期" + DateTime.Now.ToString();
            Export(tmpstr,filename);
        }

        public static DataTable GetKCKList(out string msg, KCKXX obj, string sDBConnName = "CRMDB")
        {
            //获取库存卡，必须传入obj.sBGDDDM、iHYKTYPE、iSTATUS，可以传入sCZKHM_Begin、sCZKHM_End、iSL
            msg = string.Empty;
            //List<Object> lst = new List<Object>();
            DataTable tmpDT = new DataTable();
            //KCKXX obj = new KCKXX();
            DbConnection conn = CyDbConnManager.GetActiveDbConnection(sDBConnName);
            try
            {
                CyQuery query = new CyQuery(conn);
                try
                {
                    query.SQL.Text = "select H.CZKHM,H.YZM from HYKCARD H,HYKDEF D,HYK_BGDD B where H.HYKTYPE=D.HYKTYPE and H.BGDDDM=B.BGDDDM";
                    //query.SQL.Add(" and H.STATUS=" + obj.iSTATUS);
                    if (obj.sBGDDDM != "")
                    {
                        query.SQL.Add(" and H.BGDDDM='" + obj.sBGDDDM + "'");
                    }
                    if(obj.iHYKTYPE!=0)
                        query.SQL.Add(" and H.HYKTYPE=" + obj.iHYKTYPE);
                    if (obj.sCZKHM_BEGIN != "")
                        query.SQL.Add(" and H.CZKHM>='" + obj.sCZKHM_BEGIN + "' ");
                    if (obj.sCZKHM_END != "")
                        query.SQL.Add(" and H.CZKHM<='" + obj.sCZKHM_END + "' ");
                    if (obj.fQCYE != 0)
                        query.SQL.Add(" and H.QCYE=" + obj.fQCYE);
                    if (obj.iSL != 0)
                        query.SQL.Add(" and ROWNUM<=" + obj.iSL);

                    query.SQL.AddLine("order by CZKHM ");
                    query.Open();
                    //while (!query.Eof)
                    //{
                    //    KCKXX item = new KCKXX();
                    //    item.sCZKHM = query.FieldByName("CZKHM").AsString;
                    //    //tKCKXX.sCDNR = query.FieldByName("CDNR").AsString;
                    //    item.iHYKTYPE = query.FieldByName("HYKTYPE").AsInteger;
                    //    //item.iSL = query.FieldByName("SL").AsInteger;
                    //    item.sHYKNAME = query.FieldByName("HYKNAME").AsString;
                    //    item.iSTATUS = query.FieldByName("STATUS").AsInteger;
                    //    item.fQCYE = query.FieldByName("QCYE").AsFloat;
                    //    item.fYXTZJE = query.FieldByName("YXTZJE").AsFloat;
                    //    item.fPDJE = query.FieldByName("PDJE").AsFloat;
                    //    item.sBGDDDM = query.FieldByName("BGDDDM").AsString;
                    //    item.sBGDDMC = query.FieldByName("BGDDMC").AsString;
                    //    item.dYXQ = query.FieldByName("YXQ").AsDateTime.ToString("yyyy-MM-dd");
                    //    item.dXKRQ = query.FieldByName("XKRQ").AsDateTime.ToString("yyyy-MM-dd");
                    //    lst.Add(item);
                    //    query.Next();
                    //}
                    
                    tmpDT = query.GetDataTable();

                    query.Close();
                }
                catch (Exception e)
                {
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            //return obj.GetTableJson(lst);
            return tmpDT;
        }

        public void Export(string str,string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "UTF-8";//GB2312
            //attachment   参数表示作为附件下载，可以改成   online在线打开  
            //filename=*.xls   指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc .xls .txt .htm　

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".txt");
            //HttpContext.Current.Response.Charset = "gb2312";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            //thispage.Response.ContentEncoding = System.Text.Encoding.Default;//GB2312.GetEncoding("UTF-8");
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "text/plain";// application/ms-excel 
            this.Page.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();

            tw.Write(str);
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();//不能放在try catch 内！
        }
        public override void VerifyRenderingInServerForm(Control control)//跟着导出，必须重载！
        {
            // Confirms that an HtmlForm control is rendered for
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //CrmLibProc.Export(HttpContext.Current.Response,"abcd");
        }

        private string dataTableExportToText(DataTable dt)
        {
            string dataAll = "";
            if (dt != null)
            {
                // 写数据                
                string value = null;
                int columnCount = dt.Columns.Count;
                foreach (DataRow dr in dt.Rows)
                {
                    string dataLine = null;
                    for (int j = 0; j < columnCount; j++)
                    {
                        value = dr[j].ToString().Trim();
                        if (value == null || value == string.Empty)
                        {
                            value = "?";
                        }
                        dataLine += value + " ";
                    }
                    dataAll += dataLine + "\r\n";
                }
            }
            return dataAll;

        }

    }
}