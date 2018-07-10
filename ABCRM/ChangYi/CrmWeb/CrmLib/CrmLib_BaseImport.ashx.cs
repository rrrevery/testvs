using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;
using BF.Pub;
using BF.CrmProc;
using System.Text;
using Newtonsoft.Json;
using System.Web.SessionState;
using System.Web.UI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace BF.CrmWeb.CrmLib
{
    /// <summary>
    /// CrmLib_BaseImport 的摘要说明
    /// </summary>
    public class CrmLib_BaseImport : System.Web.UI.Page, IHttpHandler
    {
        public static List<Byte[]> buffer = new List<Byte[]>();//所有分块数据
        public Byte[] tempbuffer = null;
        string colNames = string.Empty;
        string tpFileName = string.Empty;

        public new void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Request["name"] != "" && context.Request["name"] != null)
                {
                    tpFileName = context.Request["name"];
 
                }
                if (context.Request["cols"] != "" && context.Request["cols"] != null)
                {
                    colNames = context.Request["cols"];
                }
                //读取前台传送的数据
                #region
                int chunk = Convert.ToInt32(context.Request["chunk"].ToString());
                int totalChunks = Convert.ToInt32(context.Request["chunks"].ToString());
                //分块数据读取
                if (context.Request.ContentType == "application/octet-stream" && context.Request.ContentLength > 0)
                {
                    tempbuffer = new Byte[context.Request.InputStream.Length];
                    context.Request.InputStream.Read(tempbuffer, 0, tempbuffer.Length);
                }
                else if (context.Request.ContentType.Contains("multipart/form-data") && context.Request.Files.Count > 0 && context.Request.Files[0].ContentLength > 0)
                {
                    tempbuffer = new Byte[context.Request.Files[0].InputStream.Length];
                    context.Request.Files[0].InputStream.Read(tempbuffer, 0, tempbuffer.Length);
                }
                buffer.Add(tempbuffer);
                #endregion
                string msg = "";
                //单独的完整文件上传完成
                if (chunk + 1 == totalChunks)
                {

                    string filename = saveFile(out msg);//保存文件到服务器
                    //打开文件解析，返回给前台（客户端）
                    DataTable table = ExcelDataSourceByNPOI(out msg, filename);
                    if (msg == "")
                    {
                        string outdata = GetTableJson(table);
                        context.Response.Write(outdata);
                        return;
                    }
                    else
                    {
                        context.Response.Write("错误:" + msg);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                context.Response.Write("错误:" + e.Message);
            }


        }


        public string saveFile(out string msg)
        {
            msg = "";
            string fullFileName = "";
            FileStream outfile = null;
            try
            {
                DirectoryInfo directoruInfor = new DirectoryInfo(Server.MapPath("~/File"));
                if (directoruInfor.Exists == false)
                {
                    directoruInfor.Create();
                }                               
                fullFileName = Server.MapPath("~/File") + "\\" + DateTime.Now.Ticks + "%" + ((new Random()).NextDouble() * 10000).ToString();
                if (tpFileName.IndexOf("xlsx") == -1)
                {
                    fullFileName += ".xls";
                }
                else
                {
                    fullFileName += ".xlsx";
                }
                outfile = new FileStream(fullFileName, FileMode.Create);
                for (int i = 0; i < buffer.Count; i++)
                {
                    outfile.Write(buffer[i], 0, buffer[i].Length);
                }
                return fullFileName;
            }
            catch (Exception e)
            {
                throw new Exception("文件上传失败!");
            }
            finally
            {
                outfile.Flush();
                outfile.Close();
                buffer.Clear();
            }

        }

        public DataTable ExcelDataSource(out string msg, string filename)
        {
            msg = "";
            DataTable table = new DataTable();
            string[] cols = colNames.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string col in cols)
            {
                table.Columns.Add(col);
            }
            try
            {
                string strConn = "";
                //此处解析excel需要有组件  http://www.microsoft.com/zh-cn/download/confirmation.aspx?id=13255   Microsoft Access 2010 数据库引擎可再发行程序包 
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();

                //获取页名
                System.Data.DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string[] sheetName = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sheetName[i] = dt.Rows[i][2].ToString().Trim();
                }
                for (int sheet = 0; sheet < dt.Rows.Count; sheet++)
                {
                    string name = sheetName[sheet];
                    OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + name + "]", strConn);
                    DataSet ds = new DataSet();
                    oada.Fill(ds);
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        if (ds.Tables[0].Rows[i][0].ToString().Trim() != "")
                        {
                            DataRow row = table.NewRow();
                            for (int j = 0; j < ds.Tables[0].Columns.Count && j < cols.Length; j++)
                            {
                                row[cols[j]] = ds.Tables[0].Rows[i][j].ToString();
                            }
                            table.Rows.Add(row);
                        }
                    }
                }

                return table;

            }
            catch (Exception ex)
            {
                throw new Exception("excel文件结构不对 或者 \"Microsoft Access 2010 数据库引擎可再发行程序包\"没有安装 ");

            }
        }

        public string GetTableJson(DataTable table)
        {
            string[] cols = colNames.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            //string jsonString = "[";
            string result = "[]";
            StringBuilder jsonString=new StringBuilder();
            jsonString.Append("[");
            bool hasdata = false;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                hasdata = true;
                //jsonString += "{";
                jsonString.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    //jsonString += "\"" + cols[j] + "\":\"" + table.Rows[i][cols[j]].ToString() + "\",";
                    jsonString.Append("\"").Append(cols[j]).Append( "\":\"").Append(table.Rows[i][cols[j]].ToString()).Append("\",");
                }
                //jsonString = jsonString.Substring(0, jsonString.Length - 1) + "}" + ",";
                jsonString.Remove(jsonString.Length - 1, 1).Append("},");
            }
            //jsonString = jsonString.Substring(0, jsonString.Length - 1) + "]";
            jsonString.Remove(jsonString.Length - 1, 1).Append("]");
            result = jsonString.ToString();
            //if (!hasdata)
            //{
            //    jsonString = "[]";
            //}
            return result;

        }

        public DataTable ExcelDataSourceByNPOI(out string msg, string filename)
        {
            msg = "";
            DataTable table = new DataTable();
            IWorkbook workbook = null;
            string[] cols = colNames.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string col in cols)
            {
                table.Columns.Add(col);
            }

            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                if (filename.IndexOf(".xlsx") > 0)
                {
                    workbook = new XSSFWorkbook(fs);
                }
                else if (filename.IndexOf(".xls") > 0)
                {
                    workbook = new HSSFWorkbook(fs);
                }
                int sheetNumber = workbook.NumberOfSheets;
                IRow firstRow = workbook.GetSheetAt(0).GetRow(0);
                int cellCount = firstRow.LastCellNum;
                for (int i = 0; i < sheetNumber; i++)
                {
                    ISheet sheet = workbook.GetSheetAt(i);
                    int rowCount = sheet.LastRowNum;
                    for (int j = 1; j <= rowCount; j++)
                    {
                        IRow DataRow = sheet.GetRow(j);
                        if (DataRow != null)
                        {
                            DataRow tablRow = table.NewRow();
                            for (int m = DataRow.FirstCellNum; m < cellCount; m++)
                            {
                                if (DataRow.GetCell(m) != null)
                                {

                                    tablRow[m] = DataRow.GetCell(m).ToString();
                                }
                            }
                            if (CheckEveryRowNull(DataRow, cellCount) == true)
                            {
                                table.Rows.Add(tablRow);
                            }
                        }

                    }
                }
                fs.Dispose();
                return table;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw new Exception(ex.Message);
            }

        }

        public bool CheckEveryRowNull(IRow row, int cellCount)
        {
            bool BoolInsert = false;
            for (int i = 0; i < cellCount; i++)
            {
                if (row.GetCell(i) != null && row.GetCell(i).ToString() != "")
                {
                    BoolInsert = true;
                }
            }
            return BoolInsert;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}