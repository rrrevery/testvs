using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Collections;

namespace BF.CrmProc.GTPT
{
   public class GTPT_WXLSSCSC
    {

        /// <summary>
        /// 上传的临时多媒体文件。格式和大小限制，如下：
        /// 图片（image）: 1M，支持JPG格式
        /// 语音（voice）：2M，播放长度不超过60s，支持AMR\MP3格式
        /// 视频（video）：10MB，支持MP4格式
        /// 缩略图（thumb）：64KB，支持JPG格式。
        /// 媒体文件在后台保存时间为3天，即3天后media_id失效。
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb）</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <returns></returns>
        //UploadJsonResult UploadTempMedia(string accessToken, UploadMediaFileType type, string file);

        //public UploadJsonResult UploadTempMedia(string accessToken, UploadMediaFileType type, string file)
        //{
        //    string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());

        //    UploadJsonResult result = JsonHelper<UploadJsonResult>.PostFile(url, file);
        //    return result;
        //}


        /// <summary>
        /// 提交文件并解析返回的结果
        /// </summary>
        /// <param name="url">提交文件数据的链接地址</param>
        /// <param name="file">文件地址</param>
        /// <returns></returns>
        //public static T PostFile(string url, string file, NameValueCollection nvc = null)
        //{
        //    HttpHelper helper = new HttpHelper();
        //    string content = helper.PostStream(url, new string[] { file }, nvc);
        //    VerifyErrorCode(content);

        //    T result = JsonConvert.DeserializeObject<T>(content);
        //    return result;
        //}
        /// <summary>
        /// 上传多媒体文件的返回结果
        /// 其中返回结果的实体类信息UploadJsonResult的类定义如下所示。
        /// </summary>
        //public class UploadJsonResult : BaseJsonResult
        //{
        //    /// <summary>
        //    /// 媒体文件类型，分别有图片（image）、语音（voice）、视频（video）和缩略图（thumb，主要用于视频与音乐格式的缩略图）
        //    /// </summary>
        //    public UploadMediaFileType type { get; set; }

        //    /// <summary>
        //    /// 媒体文件上传后，获取时的唯一标识
        //    /// </summary>
        //    public string media_id { get; set; }

        //    /// <summary>
        //    /// 媒体文件上传时间戳
        //    /// </summary>
        //    public long created_at { get; set; }
        //}

        //private void btnUpload_Click(object sender, EventArgs e)
        //{
        //    string file = FileDialogHelper.OpenImage(false);
        //    if (!string.IsNullOrEmpty(file))
        //    {
        //        IMediaApi mediaBLL = new MediaApi();
        //        UploadJsonResult result = mediaBLL.UploadTempMedia(token, UploadMediaFileType.image, file);
        //        if (result != null)
        //        {
        //            this.image_mediaId = result.media_id;
        //            Console.WriteLine("{0} {1}", result.media_id, result.created_at);
        //        }
        //        else
        //        {
        //            Console.WriteLine("上传文件失败");
        //        }
        //    }
        //}
    }
}
