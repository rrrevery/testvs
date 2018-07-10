using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Configuration;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using System.Web.UI;
using BF.Pub;
using System.Data.Common;

namespace BF.CrmWeb.GTPT
{
    /// <summary>
    /// UploaPicture_NEW2 的摘要说明
    /// </summary>
    public class UploaPicture_NEW2 : IHttpHandler
    {
        //时间戳函数
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;

        }
        public string sURL;
        public string sPSWD;
        public string saveIp;
        public string filename;
        public string sUSER;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/html; charset=utf-8";

            string dir = context.Server.MapPath("~");//项目根目录
            string requestType = "";//哪个相关操作  比如 用户信息  组信息  后期用在基类中的字段代替
            if (context.Request["requestType"] != null)
            {
                requestType = context.Request["requestType"].ToString();
            }
            if (context.Request["filename"] != null)
            {
                //postData 可以在前台定义好与类对应的格式，并将URL 和 Type设置好
                filename = context.Request["filename"].ToString();
            }
            if (context.Request["localUrl"] != null)
            {
                filename = context.Request["localUrl"];
                filename = filename.Substring(filename.LastIndexOf("\\")+1);
            }
            //返回提示
            ErrMsg NoteObject = new ErrMsg();
            if (requestType.Length > 0)
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                query.SQL.Text = "select IP_NET,PSWD,IP_PUB,DIR from FTPCONFIG WHERE ID=1";
                query.Open();
                while (!query.Eof)
                {

                    saveIp = query.FieldByName("IP_PUB").AsString;
                    sPSWD = query.FieldByName("PSWD").AsString;
                    string FTPDIR = query.FieldByName("DIR").AsString;
                    int t = FTPDIR.LastIndexOf("@");
                    sUSER = FTPDIR.Substring(0, t);
                    //sURL = "ftp://" + FTPDIR.Substring(t + 1, FTPDIR.Length - t - 1);
                    sURL = FTPDIR.Substring(t + 1, FTPDIR.Length - t - 1);
                    //this.ftpuser =
                    //obj.sDIR = query.FieldByName("DIR").AsString;
                    //obj.sIP_NET = query.FieldByName("IP_NET").AsString;

                    query.Next();
                }
                query.Close();
                string year = serverTime.Year.ToString();
                string month = (serverTime.Month.ToString().CompareTo("10") > 0) ? serverTime.Month.ToString() : 0 + serverTime.Month.ToString();
                string day = (serverTime.Day.ToString().CompareTo("10") > 0) ? serverTime.Day.ToString() : 0 + serverTime.Day.ToString();
                string FtpsavePath = "WXFtp" + year + month + day;
                #region  此处为FTP上传
                //将文件上传到ftp
                string savePath = dir + filename;
                FileInfo fileNew = new FileInfo(savePath);
                string msg = string.Empty;
                //ftp不存在此目录
                if (!ftpIsExistsFile(out msg, FtpsavePath, sURL, sUSER, sPSWD))
                {
                    if (msg.Length == 0)
                    {
                        //创建ftp目录
                        MakeDir(out msg, FtpsavePath, sURL, sUSER, sPSWD);
                        if (msg.Length > 0)
                        {
                            NoteObject.errCode = 1;
                            NoteObject.errMessage = "ftp目录：" + msg;
                            context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                            return;
                        }
                    }
                }
                //上传文件上传不成功报错
                if (!UploadFile(out msg, fileNew, FtpsavePath, sURL, sUSER, sPSWD))
                {
                    NoteObject.errCode = 2;
                    NoteObject.errMessage = "ftp上传文件：" + msg;
                    context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                    return;
                }
                else
                {
                    NoteObject.result = "http://" + saveIp + "/" + FtpsavePath + "/" + filename.Substring(filename.LastIndexOf("/") + 1);
                    context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                    return;
                }
                #endregion
            }
            else
            {
                //保存公网IP
                saveIp = context.Request.QueryString["sPath"];
                //存在服务器FTP目录
                string FtpsavePath = "FtpImg";
                //文件基础名称
                filename = context.Request.QueryString["sName"];
                //ftp地址  密码 及用户名
                sURL = context.Request.QueryString["sURL"].ToString().Trim();
                sPSWD = context.Request.QueryString["sPSWD"].ToString().Trim();
                sUSER = context.Request.QueryString["sUSER"].ToString().Trim();
                //取出file上传文件
                HttpPostedFile file = context.Request.Files[0];
                //取本地路径，先将文件上传到服务器（程序服务器）
                string mapPath = context.Server.MapPath("~");
                string path = mapPath + "\\FtpImg\\";
                if (file != null && file.ContentLength > 0)
                {
                    //form本地上传缓存解决了file取路径浏览器安全导致问题
                    #region  此处先上传本地缓存本地可进行预览（如加预览功能需将ftp上传和本地上传分开写）
                    int imagesKindInx = file.FileName.LastIndexOf(".");
                    string fileNewName = ConvertDateTimeInt(DateTime.Now) + filename + file.FileName.Substring(imagesKindInx, file.FileName.Length - imagesKindInx);
                    string savePath = path + fileNewName;
                    //先删除本地目录及目录下文件（清除文件缓存）
                    try
                    {
                        Directory.Delete(path, true);
                    }
                    catch
                    {
                        try
                        {
                            Directory.CreateDirectory(path);
                        }
                        catch (Exception e)
                        {
                            NoteObject.errCode = 1;
                            NoteObject.errMessage = "文件目录创建：" + e.Message;
                            context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                        }
                    }
                    try
                    {
                        //本地目录不存在创建
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);
                        //本地命名重复存在删除
                        if (File.Exists(savePath))
                        {
                            File.Delete(savePath);
                        }
                        //保存文件在本地
                        file.SaveAs(savePath);
                    }
                    catch (Exception e)
                    {
                        NoteObject.errCode = 1;
                        NoteObject.errMessage = "文件提交本地目录：" + e.Message;
                        context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                    }
                    #endregion

                    #region  此处为FTP上传
                    //将文件上传到ftp
                    FileInfo fileNew = new FileInfo(savePath);
                    string msg = string.Empty;
                    //ftp不存在此目录
                    if (!ftpIsExistsFile(out msg, FtpsavePath, sURL, sUSER, sPSWD))
                    {
                        if (msg.Length == 0)
                        {
                            //创建ftp目录
                            MakeDir(out msg, FtpsavePath, sURL, sUSER, sPSWD);
                            if (msg.Length > 0)
                            {
                                NoteObject.errCode = 1;
                                NoteObject.errMessage = "ftp目录：" + msg;
                                context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                                return;
                            }
                        }
                    }
                    //上传文件上传不成功报错
                    if (!UploadFile(out msg, fileNew, FtpsavePath, sURL, sUSER, sPSWD))
                    {
                        NoteObject.errCode = 2;
                        NoteObject.errMessage = "ftp上传文件：" + msg;
                        context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                        return;
                    }
                    else
                    {
                        NoteObject.result = "http://" + saveIp + "/" + FtpsavePath + "/" + fileNewName;
                        context.Response.Write(JsonConvert.SerializeObject(NoteObject));
                        return;
                    }
                    #endregion
                }
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileinfo">需要上传的文件</param>
        /// <param name="targetDir">目标路径</param>
        /// <param name="hostname">ftp地址</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        public static bool UploadFile(out string msg, FileInfo fileinfo, string targetDir, string hostname, string username, string password)
        {
            msg = string.Empty;
            //1. check target
            string target;
            if (targetDir.Trim() == "")
            {
                return false;
            }

            target = Guid.NewGuid().ToString();  //使用临时文件名

            string URI = "FTP://" + hostname + "/" + targetDir + "/" + target;
            ///WebClient webcl = new WebClient();
            System.Net.FtpWebRequest ftp = GetRequest(URI, username, password);
            //设置FTP命令 设置所要执行的FTP命令，
            //ftp.Method = System.Net.WebRequestMethods.Ftp.ListDirectoryDetails;//假设此处为显示指定路径下的文件列表
            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            //告诉ftp文件大小
            ftp.ContentLength = fileinfo.Length;
            ftp.KeepAlive = false;
            //此处注释掉因程序是先将重复文件删除
            //try
            //{
            //    string tp_URI = "FTP://" + hostname + "/" + targetDir + "/" + fileinfo.Name;
            //    System.Net.FtpWebRequest ftp1 = GetRequest(tp_URI, username, password);
            //    ftp1 = GetRequest(tp_URI, username, password);
            //    ftp1.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
            //    ftp1.GetResponse();

            //}
            //catch(Exception e)
            //{
            //    msg = e.Message;
            //}
            //finally
            //{







            //缓冲大小设置为2KB
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;
            //打开一个文件流 (System.IO.FileStream) 去读上传的文件
            using (FileStream fs = fileinfo.OpenRead())
            {
                try
                {
                    //把上传的文件写入流
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        {
                            //每次读文件流的2KB
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }
                }
                catch (Exception ex) { msg = "文件上传：" + ex.Message; return false; }
                finally
                {
                    fs.Close();
                }
            }
            ftp = null;
            //设置FTP命令
            ftp = GetRequest(URI, username, password);
            ftp.Method = System.Net.WebRequestMethods.Ftp.Rename; //改名
            ftp.RenameTo = fileinfo.Name;
            ftp.KeepAlive = false;
            try
            {
                ftp.GetResponse();
            }
            catch (Exception ex)
            {
                //ftp = GetRequest(URI, username, password);
                //ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                //ftp.GetResponse();
                //throw ex;
                msg = ex.Message;
            }
            //finally
            //{
            //    //可以对FTP文件进行删除
            //    //fileinfo.Delete();
            //}
            // 可以记录一个日志  "上传" + fileinfo.FullName + "上传到" + "FTP://" + hostname + "/" + targetDir + "/" + fileinfo.Name + "成功." );
            ftp = null;
            //}
            return (msg.Length == 0);

        }

        /// <summary>
        /// 判断ftp服务器上该目录是否存在
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="ftpHostIP"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ftpIsExistsFile(out string msg, string dirName, string ftpHostIP, string username, string password)
        {
            bool flag = false;
            msg = string.Empty;
            string uri = "ftp://" + ftpHostIP + "/" + dirName;
            //string[] value = GetFileList(pFtpServerIP, pFtpUserID, pFtpPW);
            System.Net.FtpWebRequest ftp = null;
            try
            {
                ftp = GetRequest(uri, username, password);
                //ftp.Method = WebRequestMethods.Ftp.ListDirectory;
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftp.KeepAlive = false;
                //ftp.GetResponse();
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                //FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
                if (line != null)
                {
                    flag = true;
                }
            }
            catch (Exception x)
            {
                msg = x.Message;
                flag = false;
            }
            return flag;
        }
        /// 在ftp服务器上创建目录
        /// </summary>
        /// <param name="dirName">创建的目录名称</param>
        /// <param name="ftpHostIP">ftp地址</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public void MakeDir(out string msg, string dirName, string ftpHostIP, string username, string password)
        {
            msg = string.Empty;
            try
            {
                string uri = "ftp://" + ftpHostIP + "/" + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftp.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
        }

        private static FtpWebRequest GetRequest(string URI, string username, string password)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
            //提供身份验证信息
            result.Credentials = new System.Net.NetworkCredential(username, password);
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = false;
            return result;
        }

        public class ErrMsg
        {
            //错误码。0 表示正常返回，否则表示有异常
            public int errCode = 0;
            //错误说明。异常说明信息
            public string errMessage = string.Empty;
            //执行结果
            public object result = null;
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