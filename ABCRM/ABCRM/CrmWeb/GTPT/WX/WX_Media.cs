using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.Pub;
using BF.CrmProc;

using System.Net;
using System.Text;
using System.IO;
using System.Data;
using System.Data.Common;
namespace BF.CrmWeb.GTPT.WX
{
    /// <summary>
    /// 上传与下载  多媒体文件
    /// </summary>
    public class WX_Media : WX_Base
    {
        public WX_Media()
        {
            mode = "insert";
        }
        //{"type":"TYPE","media_id":"MEDIA_ID","created_at":123456789}
        public string type = "";//分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb） 有点冲突，回头再改
        public string media_id = "";
        public string created_at = "";
        public string yxq = "";
        public string cjsj = "";
        public string title = "";
        public string descrption = "";

        //上传二进制数据(文件 )  图片  语音 等 如果是视频需要两次上传(发送之前还需要上传一次)
        public override string InsertData(out string msg, HttpContext context = null)
        {
            if (type == "text")
            {

            }
            method = "POST";
            Url = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token + "&type=" + type;
            //设置buffer  获取上传文件的字节流
            msg = "";
            string wxstring = "";
            //如果是素材库，就不再使用此函数
            bool isEND = GetBuffer(out msg, context);
            if (isEND)
            {
                wxstring = WXRequestString(out msg, context);
            }
            else if (msg == "")
            {
                return "分块上传完成";
            }
            else if (msg != "")
            {
                return msg;
            }
            //this.SearchData(out msg);

            buffer = new List<Byte[]>();//清空数据

            WX_Media media = JsonConvert.DeserializeObject<WX_Media>(wxstring);
            media.filename = filename;//记录保存到本地的文件名 
            media.cjsj = GetTime(media.created_at);
            DateTime dt = FormatUtils.ParseDatetimeString(media.cjsj);
            dt = dt.AddDays(3);//只保存在服务器上三天
            media.yxq = FormatUtils.DatetimeToString(dt);

            if (media.created_at != "")
            {
                return JsonConvert.SerializeObject(media);
            }
            return "上传失败";
        }

        //下载多媒体文件()
        public override string SearchData(out string msg, HttpContext context = null)
        {
            method = "GET";
            Url = "http://file.api.weixin.qq.com/cgi-bin/media/get?access_token=" + Token + "&media_id=" + media_id;
            filename = WXDownloadFile(out msg, "a.jpg", type);
            return filename;
        }

        public string saveFile(out string msg, string filetype = "jpg")
        {
            msg = "";
            FileStream outfile = null;
            try
            {
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                CyQuery query = new CyQuery(conn);
                DateTime serverTime = CyDbSystem.GetDbServerTime(query);
                switch (type)
                {
                    case "text":
                        //dir += "Texts";
                        dir = "";
                        break;
                    case "image":
                        dir += "Images\\" + serverTime.Date + "\\";
                        break;
                    case "voice":
                        dir += "Voices\\" + serverTime.Date + "\\";
                        break;
                    case "video":
                        dir += "Videos\\" + serverTime.Date + "\\";
                        break;
                    case "news":
                        dir += "News" + serverTime.Date + "\\";
                        break;
                }
                if (dir == "")
                {
                    return "";
                }
                filename = serverTime.Ticks.ToString() + Convert.ToInt32(((new Random()).NextDouble() * 1000)).ToString() + "." + filetype;
                fullFileName = dir + filename;
                outfile = new FileStream(fullFileName, FileMode.Create);
                for (int i = 0; i < buffer.Count; i++)
                {
                    outfile.Write(buffer[i], 0, buffer[i].Length);
                }
                return filename;
            }
            catch (Exception e)
            {
                throw new Exception("文件上传失败!");
            }
            finally
            {
                outfile.Flush();
                outfile.Close();
            }

        }
    }


}