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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace BF.CrmWeb.KFPT.KFJLDY
{
    /// <summary>
    /// KFPT_KFJLDY1 的摘要说明
    /// </summary>
    public class KFPT_KFJLDY1 : System.Web.UI.Page, IHttpHandler
    {
        public static List<Byte[]> buffer = new List<Byte[]>();//所有分块数据
        public Byte[] tempbuffer = null;
        string[] cols = { "sHYK_NO", "sHY_NAME", "sLXDH" };

        //将数据读取到DataTable当中
        #region 读取excel ,默认第一行为标头Import()
        /// <summary>
        /// 读取excel ,默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            string[] cols = { "sHYK_NO", "sHY_NAME", "sLXDH" };
            DataTable dt = new DataTable();
            FileStream fs = null;
            IWorkbook hssfworkbook = null;
            fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read);
            if (strFileName.IndexOf(".xlsx") > 0) // 2007版本
                hssfworkbook = new XSSFWorkbook(fs);
            else if (strFileName.IndexOf(".xls") > 0) // 2003版本
                hssfworkbook = new HSSFWorkbook(fs);
            int a = hssfworkbook.ActiveSheetIndex;
            ISheet sheet = hssfworkbook.GetSheetAt(a);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            foreach (string col in cols)
            {
                dt.Columns.Add(col);
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();
                for (int j = 0; j < cellCount; j++)
                {
                    if (row.GetCell(j + 1) != null)
                    {
                        ICell cell = row.GetCell(j + 1);
                        if (cell.CellType == CellType.Numeric)
                        {
                            //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                            if (HSSFDateUtil.IsCellDateFormatted(cell))//日期类型
                            {
                                dataRow[j] = cell.DateCellValue.ToShortDateString();
                            }
                            else//其他数字类型
                            {
                                dataRow[j] = cell.NumericCellValue;
                            }
                        }
                        else
                            dataRow[j] = row.GetCell(j + 1).ToString();
                    }
                }

                dt.Rows.Add(dataRow);
            }
            if (File.Exists(strFileName))
            {
                File.Delete(strFileName);
            }
            return dt;
        }
        #endregion

        public string GetTableJson(DataTable table)
        {
            string jsonString = "[";
            bool hasdata = false;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                hasdata = true;
                jsonString += "{";
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    jsonString += "\"" + cols[j] + "\":\"" + table.Rows[i][cols[j]].ToString() + "\",";
                }
                jsonString = jsonString.Substring(0, jsonString.Length - 1) + "}" + ",";

            }
            jsonString = jsonString.Substring(0, jsonString.Length - 1) + "]";
            if (!hasdata)
            {
                jsonString = "[]";
            }
            return jsonString;

        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {

                //读取前台传送的数据
                #region
                int chunk = Convert.ToInt32(context.Request["chunk"].ToString());
                int totalChunks = Convert.ToInt32(context.Request["chunks"].ToString());
                int mod = Convert.ToInt32(context.Request["mod"].ToString());
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

                    string filename = saveFile(out msg, mod);//保存文件到服务器
                    //打开文件解析，返回给前台（客户端）
                    DataTable table = Import(filename);

                    if (msg == "")
                    {
                        buffer = new List<Byte[]>();
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

        public string saveFile(out string msg, int mod)
        {
            msg = "";
            string fullFileName = "";
            FileStream outfile = null;
            try
            {
                if (mod == 1)
                    fullFileName = Server.MapPath("~/File") + "\\" + DateTime.Now.Ticks + ".xls";
                if (mod == 2)
                    fullFileName = Server.MapPath("~/File") + "\\" + DateTime.Now.Ticks + ".xlsx";
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}