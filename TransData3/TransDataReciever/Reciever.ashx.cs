using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RRR;

namespace TransDataReciever
{
    /// <summary>
    /// Reciever 的摘要说明
    /// </summary>
    public class Reciever : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                if (context.Request.HttpMethod == "POST")
                {
                    HttpPostedFile file = context.Request.Files[0];
                    string mapPath = context.Server.MapPath("~");
                    string path = mapPath + "Recieved\\";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string tblname = context.Request["name"].ToString();
                    string keyfld = context.Request["key"].ToString();
                    string tmfld = context.Request["tm"].ToString();
                    if (tblname.IndexOf("DIR") >= 0)
                    {
                        //if (!Directory.Exists(path + keyfld))
                        //    Directory.CreateDirectory(path + keyfld);
                        string fileNewName = file.FileName;
                        int inx = file.FileName.LastIndexOf("\\");
                        int inx2 = file.FileName.IndexOf("\\"+keyfld);
                        string savePath = path + fileNewName.Substring(0, inx + 1).Substring(inx2 + 1);
                        if (!Directory.Exists(savePath))
                            Directory.CreateDirectory(savePath);
                        savePath += fileNewName.Substring(inx + 1);
                        file.SaveAs(savePath);
                        context.Response.Write("2");
                    }
                    else
                    {

                        if (file != null && file.ContentLength > 0)
                        {
                            string fileNewName = file.FileName;
                            int inx = file.FileName.LastIndexOf("\\");
                            string savePath = path + fileNewName.Substring(inx + 1);
                            file.SaveAs(savePath);
                            savePath = RTools.DecompressFile(savePath);
                            string lines = File.ReadAllText(savePath);
                            JArray jc = JArray.Parse(lines);
                            //开始写库尼玛
                            DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
                            RQuery query = new RQuery(conn);
                            DbTransaction dbTrans = conn.BeginTransaction();
                            try
                            {
                                query.SetTrans(dbTrans);
                                StringBuilder sbi = new StringBuilder();
                                StringBuilder sbv = new StringBuilder();
                                StringBuilder sbu = new StringBuilder();
                                StringBuilder sbw = new StringBuilder();
                                foreach (var one in jc)
                                {
                                    sbi.Clear();
                                    sbv.Clear();
                                    sbu.Clear();
                                    sbw.Clear();
                                    foreach (JProperty two in one)
                                    {
                                        if (two.Name.ToUpper() != tmfld.ToUpper())
                                        {
                                            sbi.Append(two.Name + ",");
                                            sbv.Append(GetJPropertyValue(two) + ",");
                                            //if (two.Value.Type == JTokenType.String || two.Value.Type == JTokenType.Date)
                                            //    sbv.Append("'" + two.Value.ToString() + "',");
                                            //else
                                            //    sbv.Append(two.Value.ToString() + ",");

                                            if (two.Name.ToUpper() != keyfld.ToUpper())
                                            {
                                                sbu.Append(two.Name + "=" + GetJPropertyValue(two) + ",");
                                            }
                                            else if (two.Name.ToUpper() == keyfld.ToUpper())
                                            {
                                                sbw.Append(two.Name + "=" + GetJPropertyValue(two));
                                            }
                                        }
                                    }
                                    query.SQL.Text = "update " + tblname + " set " + sbu.Remove(sbu.Length - 1, 1).ToString() + " where " + sbw.ToString();
                                    if (query.ExecSQL() == 0)
                                    {
                                        query.SQL.Text = "insert into " + tblname + "(" + sbi.Remove(sbi.Length - 1, 1).ToString() + ") values(" + sbv.Remove(sbv.Length - 1, 1).ToString() + ")";
                                        query.ExecSQL();
                                    }
                                }
                                dbTrans.Commit();
                            }
                            catch (Exception e)
                            {
                                dbTrans.Rollback();
                                throw new Exception(e.Message);
                            }
                            finally
                            {
                                conn.Close();
                            }
                            //string key=
                            context.Response.Write("1");
                        }
                    }
                }
                else
                {
                    context.Response.Write("你发Get我不收你发它有啥用啊");
                }
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
        }
        string GetJPropertyValue(JProperty j)
        {
            if (j.Value.Type == JTokenType.String || j.Value.Type == JTokenType.Date)
                return "'" + j.Value.ToString() + "'";
            else
                return j.Value.ToString();

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