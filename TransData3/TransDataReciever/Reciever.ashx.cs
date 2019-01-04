using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

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
            context.Response.Write("Hello World");
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
                    string savePath = path + fileNewName.Substring(inx+1);
                    file.SaveAs(savePath);
                }
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