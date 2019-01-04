using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;
using System.IO;
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
            if (context.Request.HttpMethod == "POST")
            {
                HttpPostedFile file = context.Request.Files[0];
                string mapPath = context.Server.MapPath("~");
                string path = mapPath + "\\Recieved\\";
                if (file != null && file.ContentLength > 0)
                {
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    string fileNewName = file.FileName;
                    int inx = file.FileName.LastIndexOf("\\");
                    string savePath = path + fileNewName.Substring(inx + 1);
                    file.SaveAs(savePath);
                    string[] lines = File.ReadAllLines(savePath);
                    JToken jh = JToken.Parse(lines[0]);
                    string tblname = jh["TblName"].ToString();
                    string keyfld = jh["KeyFld"].ToString();
                    string tmfld = jh["TMFld"].ToString();
                    JArray jc = JArray.Parse(lines[1]);
                    //开始写库尼玛
                    DbConnection conn = RTools.GetDbConnection("MSSQLLocal");
                    conn.Open();
                    RQuery query = new RQuery(conn);
                    StringBuilder sbi = new StringBuilder();
                    StringBuilder sbv = new StringBuilder();
                    foreach (var one in jc)
                    {
                        sbi.Clear();
                        sbv.Clear();
                        foreach (JProperty two in one)
                        {
                            if (two.Name != tmfld)
                            {
                                sbi.Append(two.Name + ",");
                                if (two.Value.Type == JTokenType.String || two.Value.Type == JTokenType.Date)
                                    sbv.Append("'" + two.Value.ToString() + "',");
                                else
                                    sbv.Append(two.Value.ToString() + ",");
                            }
                        }
                        query.SQL.Text = "insert into " + tblname + "(" + sbi.Remove(sbi.Length-1,1).ToString() + ") values(" + sbv.Remove(sbv.Length - 1, 1).ToString() + ")";
                        query.ExecSQL();
                    }
                    //string key=
                }
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("你发Get我不收你发它有啥用啊");

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