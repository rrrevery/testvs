using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using BF.Pub;
using BF.CrmProc;

namespace BF.CrmWeb.HYKGL.HYDALR_NEW
{
    public partial class ImportHYDA : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.dt1.Clear();
                this.GridView1.DataSource = dt1;
                this.GridView1.DataBind();
            }
            //this.LB_DJR.Text = V_UserName;
            //this.LB_DJSJ.Text = System.DateTime.Now.ToString();
        }
        
        public DataTable dt1
        {
            get
            {
                if (ViewState["dt1"] == null)
                {
                    DataTable tptb = new DataTable();
                    tptb.Columns.Add("HYK_NO");
                    tptb.Columns.Add("HY_NAME");//
                    tptb.Columns.Add("SFZBH");
                    tptb.Columns.Add("SEX");
                    //tptb.Columns.Add("CSRQ");
                    tptb.Columns.Add("SJHM");
                    tptb.Columns.Add("TXDZ");
                    //tptb.Columns.Add("YZBM");
                    //tptb.Columns.Add("E_MAIL");

                    ViewState["dt1"] = tptb;
                }
                return (ViewState["dt1"] as DataTable);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //判读是否有上传文件
            if (FileUp.PostedFile.ContentLength > 0)
            {
                if (IsAllowableFileType())
                {
                    if (Directory.Exists(Server.MapPath("~/File")) == false)//判断文件夹是否存在,若不存在则创建
                    {
                        Directory.CreateDirectory(Server.MapPath("~/File"));
                    }
                    else
                    {
                        #region
                        if (IsAllowableFileSize())
                        {
                            //string UploadFilePath = ConfigurationManager.AppSettings["UploadFile"].ToString();
                            string UploadFilePath = Server.MapPath("~/File\\");
                            string fullName = FileUp.PostedFile.FileName;
                            string newName = DateTime.Now.Ticks.ToString() + fullName.Substring(fullName.LastIndexOf("."));
                            FileUp.SaveAs(UploadFilePath + newName);
                            //lblFileUrl.Text = fullName.Substring(fullName.LastIndexOf("\\")) + "   上传成功";
                            //lblSaveFileName.Text = newName;
                            dt1.Clear();

                            ExcelDataSource(UploadFilePath + newName, "");

                            DeleteFile(UploadFilePath, newName);
                        }
                        else
                        {
                            Js_Alert(this.Page, "文件太大了,上传失败!"); return;
                            //MessegeBox.Show(this, "文件太大了,上传失败");
                        }
                        #endregion
                    }
                }
                else
                {
                    Js_Alert(this.Page, "文件类型不正确,上传失败!"); return;
                    //MessegeBox.Show(this, "文件类型不正确,上传失败");
                }
            }
            else
            {
                Js_Alert(this.Page, "上传文件为空,上传失败!"); return;
                // MessegeBox.Show(this, "上传文件为空,上传失败");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.GridView1.PageIndex = e.NewPageIndex;
            this.GridView1.DataSource = dt1;
            this.GridView1.DataBind();
        }

        #region 判断上传文件类型
        protected bool IsAllowableFileType()
        {
            //从web.config读取判断文件类型限制
            string strFileTypeLimit = ConfigurationManager.AppSettings["FileType"].ToString();
            //当前文件扩展名是否包含在这个字符串中
            if (strFileTypeLimit.IndexOf(Path.GetExtension(FileUp.FileName).ToLower()) != -1)
            {
                return true;
            }
            else
                return false;
        }
        protected bool IsAllowablePictureType()
        {
            //从web.config读取判断图片类型限制
            string strFileTypeLimit = ConfigurationManager.AppSettings["PicTureTye"].ToString();
            //当前文件扩展名是否包含在这个字符串中
            if (strFileTypeLimit.IndexOf(Path.GetExtension(FileUp.FileName).ToLower()) != -1)
            {
                return true;
            }
            else
                return false;
        }
        #endregion
        #region 判断文件大小限制
        private bool IsAllowableFileSize()
        {
            //从web.config读取判断文件大小的限制
            double iFileSizeLimit = Convert.ToInt32(ConfigurationManager.AppSettings["FileSizeLimit"]) * 1024;
            //判断文件是否超出了限制
            if (iFileSizeLimit > FileUp.PostedFile.ContentLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public void ExcelDataSource(string filepath, string sheetname)
        {
            try
            {
                string strConn = "";
                ////excel 2007   
                //if (RB_2.Checked == true)
                //{
                //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
                //}
                ////excel 2003   
                //else
                //{
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + filepath + "';Extended Properties=Excel 8.0;";
                //}


                OleDbConnection conn = new OleDbConnection(strConn);
                OleDbDataAdapter oada = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
                DataSet ds = new DataSet();
                oada.Fill(ds);

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    //jlbh = DBUpdateBHZT("HYK_CZKZPXX").ToString();
                    //&& ds.Tables[0].Rows[i][1].ToString() != "" && ds.Tables[0].Rows[i][2].ToString() != "" && ds.Tables[0].Rows[i][3].ToString() != ""
                    if (ds.Tables[0].Rows[i][1].ToString() != "")
                    {
                        DataRow mrow = dt1.NewRow();
                        mrow["HYK_NO"] = ds.Tables[0].Rows[i][0].ToString();
                        mrow["HY_NAME"] = ds.Tables[0].Rows[i][1].ToString();
                        mrow["SFZBH"] = ds.Tables[0].Rows[i][2].ToString();
                        mrow["SEX"] = ds.Tables[0].Rows[i][3].ToString();
                        //mrow["CSRQ"] = Convert.ToDateTime(ds.Tables[0].Rows[i][4].ToString()).ToShortDateString();
                        mrow["SJHM"] = ds.Tables[0].Rows[i][4].ToString();
                        mrow["TXDZ"] = ds.Tables[0].Rows[i][5].ToString();
                        //mrow["YZBM"] = ds.Tables[0].Rows[i][7].ToString();
                        //mrow["E_MAIL"] = ds.Tables[0].Rows[i][8].ToString();
                        dt1.Rows.Add(mrow);
                    }
                }
                this.LB_GV_ROW.Text = dt1.Rows.Count.ToString();
                //this.LB_GV_JE.Text = dt1.Compute("sum(je)", "").ToString();
                this.GridView1.DataSource = dt1;
                this.GridView1.DataBind();

            }
            catch (Exception ex)
            {
                Js_Alert(this.Page, "导入文件格式错误!" + ex.Message.ToString());
                //Js_Alert(this.Page, ); 
            }
        }

        public void DeleteFile(string strAbsolutePath, string strFileName)
        {
            // 判断路径最后有没有 \ 符号，没有则自动加上 
            if (strAbsolutePath.LastIndexOf("\\") == strAbsolutePath.Length)
            {
                // 判断要删除的文件是否存在 
                if (File.Exists(strAbsolutePath + strFileName))
                {
                    // 删除文件 
                    File.Delete(strAbsolutePath + strFileName);
                }
            }
            else
            {
                if (File.Exists(strAbsolutePath + "\\" + strFileName))
                {
                    File.Delete(strAbsolutePath + "\\" + strFileName);
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string rMsg;
            int num = Import(out rMsg);

            if (rMsg == "")
            {
                Js_Alert(this.Page, "导入成功！其中关联卡号 " + num + "条记录");
            }
            else
            {
                Js_Alert(this.Page, "导入失败！" + rMsg);
            }
        }

        private int Import(out string msg)
        {
            int j = 0;
            msg = "";
            if (CheckData(out msg) == false) { return 0; }

            DbConnection conn = CyDbConnManager.GetDbConnection("CRMDB");
            try { conn.Open(); }
            catch (Exception e)
            {
                msg = e.Message;
                //
                throw new MyDbException(e.Message, true);
            }

            DbTransaction dbTrans = conn.BeginTransaction();
            try
            {
                CyQuery query = new CyQuery(conn);

                try
                {
                    query.SetTrans(dbTrans);    //跟Delphi不一样，这一行必须有！
                    int iJLBH;
                    iJLBH = SeqGenerator.GetSeq("HYK_GKDA",dt1.Rows.Count);
                    for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                    {

                        #region SQL语句 HYK_HYXX关联GKID HYK_GKDA不为空

                        //if (dt1.Rows[i]["HYK_NO"].ToString() != "")
                        //{
                        //    query.SQL.Clear();
                        //    query.Params.Clear();
                        //    query.SQL.AddLine("update HYK_HYXX set HY_NAME=:pHY_NAME  where HYK_NO=:pHYK_NO ");
                        //    query.ParamByName("pHYK_NO").AsString = dt1.Rows[i]["HYK_NO"].ToString();
                        //    query.ParamByName("pHY_NAME").AsString = dt1.Rows[i]["HY_NAME"].ToString();
                        //    query.ExecSQL();

                        //    query.Close();
                        //    query.SQL.Clear();
                        //    query.Params.Clear();

                        //    query.SQL.AddLine("update HYK_GKDA set GK_NAME=:GK_NAME,SFZBH=:pSFZBH,SEX=:pSEX,TXDZ=:pTXDZ,SJHM=:pSJHM,GXR=:pDJR,GXRMC=:pDJRMC,GXSJ=sysdate where GKID=(select GKID from HYK_HYXX where HYK_NO=:pHYK_NO) ");
                        //    query.ParamByName("pHYK_NO").AsString = dt1.Rows[i]["HYK_NO"].ToString();
                        //}
                        //else
                        //{
                        //    query.Close();
                        //    query.SQL.Clear();
                        //    query.Params.Clear();
                        //    //query.SQL.AddLine("begin ");
                        //    //query.SQL.AddLine("if SQL%NOTFOUND then");
                        //    query.SQL.AddLine("insert into  HYK_GKDA(GKID,GK_NAME,SFZBH,SEX,SJHM,TXDZ,DJR,DJRMC,DJSJ) values(:GKID,:GK_NAME,:pSFZBH,:pSEX,:pSJHM,:pTXDZ,:pDJR,:pDJRMC,sysdate) ");
                        //    //query.SQL.AddLine("end if; ");
                        //    //query.SQL.AddLine("end; ");
                        //    query.ParamByName("GKID").AsInteger = SeqGenerator.GetSeq("HYK_GKDA");
                        //}
                            
                        //    query.ParamByName("GK_NAME").AsString = dt1.Rows[i]["HY_NAME"].ToString();
                        //    query.ParamByName("pSEX").AsString = dt1.Rows[i]["SEX"].ToString() == "男" ? "0" : "1";
                        //    //query.ParamByName("pCSRQ").AsString = dt1.Rows[i]["CSRQ"].ToString();
                        //    query.ParamByName("pTXDZ").AsString = dt1.Rows[i]["TXDZ"].ToString();
                        //    query.ParamByName("pSFZBH").AsString = dt1.Rows[i]["SFZBH"].ToString();
                        //    //query.ParamByName("pYZBM").AsString = dt1.Rows[i]["YZBM"].ToString();
                        //    //query.ParamByName("pE_MAIL").AsString = dt1.Rows[i]["E_MAIL"].ToString();
                        //    query.ParamByName("pSJHM").AsString = dt1.Rows[i]["SJHM"].ToString();

                        //    query.ParamByName("pDJR").AsString = V_UserID; //Request.Form["LB_DJRMC"].ToString();//
                        //    query.ParamByName("pDJRMC").AsString = V_UserName;//Request.Form["HF_DJR"].ToString();//
                        //    //query.ParamByName("pDJSJ").AsString = System.DateTime.Now.ToString();

                        //    query.ExecSQL();

                        #endregion

                        #region HYK_HYXX未关联GKID HYK_GKDA为空



                            query.Close();
                            query.SQL.Clear();
                            query.Params.Clear();

                            query.SQL.AddLine("insert into  HYK_GKDA(GKID,GK_NAME,SFZBH,SEX,SJHM,TXDZ,DJR,DJRMC,DJSJ) values(:GKID,:GK_NAME,:pSFZBH,:pSEX,:pSJHM,:pTXDZ,:pDJR,:pDJRMC,sysdate) ");


                            query.ParamByName("GKID").AsInteger = iJLBH-i;
                            query.ParamByName("GK_NAME").AsString = dt1.Rows[i]["HY_NAME"].ToString();
                            query.ParamByName("pSEX").AsString = dt1.Rows[i]["SEX"].ToString() == "男" ? "0" : "1";
                            //query.ParamByName("pCSRQ").AsString = dt1.Rows[i]["CSRQ"].ToString();
                            query.ParamByName("pTXDZ").AsString = dt1.Rows[i]["TXDZ"].ToString();
                            query.ParamByName("pSFZBH").AsString = dt1.Rows[i]["SFZBH"].ToString();
                            //query.ParamByName("pYZBM").AsString = dt1.Rows[i]["YZBM"].ToString();
                            //query.ParamByName("pE_MAIL").AsString = dt1.Rows[i]["E_MAIL"].ToString();
                            query.ParamByName("pSJHM").AsString = dt1.Rows[i]["SJHM"].ToString();

                            query.ParamByName("pDJR").AsString = V_UserID; //Request.Form["LB_DJRMC"].ToString();//
                            query.ParamByName("pDJRMC").AsString = V_UserName;//Request.Form["HF_DJR"].ToString();//
                            //query.ParamByName("pDJSJ").AsString = System.DateTime.Now.ToString();

                            query.ExecSQL();

                            if (dt1.Rows[i]["HYK_NO"].ToString() != "")
                            {
                                query.Close();
                                query.SQL.Text = "update HYK_HYXX set GKID=:GKID,HY_NAME=:HY_NAME where HYK_NO=:HYK_NO ";
                                query.ParamByName("GKID").AsInteger = iJLBH - i;
                                query.ParamByName("HYK_NO").AsString = dt1.Rows[i]["HYK_NO"].ToString();
                                query.ParamByName("HY_NAME").AsString = dt1.Rows[i]["HY_NAME"].ToString();
                                if (query.ExecSQL() > 0)
                                {
                                    j++;
                                }
                            }

                        #endregion

                    }
                    dbTrans.Commit();
                }
                catch (Exception e)
                {
                    dbTrans.Rollback();
                    if (e is MyDbException)
                        throw e;
                    else
                        msg = e.Message;
                    //
                    throw new MyDbException(e.Message, query.SqlText);

                }
            }
            finally
            {
                conn.Close();
            }
            if (msg == "") { return j; }
            else { return 0; }
            //msg = string.Empty;
            //return false;
        }

        private bool CheckData(out string msg)
        {
            msg = "";
            string sql = "";
            DataTable dts = new DataTable();
            
            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
            {
                #region HY_NAME
                if (string.IsNullOrEmpty(dt1.Rows[i]["HY_NAME"].ToString()))
                {
                }
                else
                {
                    msg = "第" + (i + 1) + "条记录姓名为空！";
                    return false;
                }
                #endregion

                #region TB_SJHM
                //if (crmfc.IsTelePhone(dt1.Rows[i]["SJHM"].ToString()) == true)//电话中含有固定电话
                //if (crmfc.IsNumber(dt1.Rows[i]["SJHM"].ToString()))
                //{
                //    //--------手机号不能重复--------------------------------------
                //    sql = "select GKID from HYK_GKDA where SJHM='" + dt1.Rows[i]["SJHM"].ToString() + "' ";


                //    dts = crmfc.GetDataToTable(sql, ref msg);
                //    if (msg == "")
                //    {
                //        if (dts.Rows.Count >= 1)
                //        {
                //            msg = "手机号 " + dt1.Rows[i]["SJHM"].ToString() + " 已存在！";
                //            return false;
                //        }
                //    }
                //}
                //else
                //{
                //    msg = "第" + (i + 1) + "条记录手机号码不合法！";
                //    return false;
                //}
                #endregion


                #region SFZBH
                //--------身份证号不能重复--------------------------------------
                //string sfzbh = dt1.Rows[i]["SFZBH"].ToString();

                //if (crmfc.IsNull(sfzbh) && !crmfc.IsIDCard(sfzbh))
                //{
                //    msg = "第" + (i + 1) + "条记录身份证号码不合法！";
                //    return false;
                //}


                //sql = "select SFZBH from HYK_GKDA where SFZBH='" + dt1.Rows[i]["SFZBH"].ToString() + "' ";

                //dts = crmfc.GetDataToTable(sql, ref msg);
                //if (msg == "")
                //{
                //    if (dts.Rows.Count >= 1)
                //    {


                //        msg = "身份证号： " + dt1.Rows[0]["SFZBH"].ToString() + " 重复！";
                //        return false;

                //    }
                //}

                #endregion

                //#region SFZBH
                //string sfzbh = dt1.Rows[i]["SFZBH"].ToString();
                //if (crmfc.IsNull(sfzbh) == false)
                //{
                //    if (crmfc.IsIDCard(sfzbh) == true)
                //    {
                //        string birthday = sfzbh.Substring(6, 4) + "-" + sfzbh.Substring(10, 2) + "-" + sfzbh.Substring(12, 2);
                //        string sex = sfzbh.Substring(16, 1);
                //        if (int.Parse(sex) % 2 == 0)
                //        {
                //            dt1.Rows[i]["SEX"] = "女";
                //        }
                //        else
                //        {
                //            dt1.Rows[i]["SEX"] = "男";
                //        }
                //        dt1.Rows[i]["CSRQ"] = birthday;
                //    }
                //    else { msg = "第" + (i + 1) + "条记录身份证号不合法！"; return false; }
                //}
                //else
                //{
                //    if (crmfc.IsNull(dt1.Rows[i]["SEX"].ToString()))
                //    { msg = "第" + (i + 1) + "条记录性别为空！"; return false; }
                //    if (crmfc.IsDate(dt1.Rows[i]["CSRQ"].ToString()))
                //    { msg = "第" + (i + 1) + "条记录出生日期不合法！"; return false; }
                //}
                //#endregion


            }
            return true;

        }
    }
}