using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BF.CrmProc;
using System.Net;
using System.Text;
using System.IO;
using BF.Pub;
using System.Data;
using System.Data.Common;
using System.Security;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NPOI.SS.Formula.Functions;
using Newtonsoft.Json.Linq;
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

namespace BF.CrmWeb.GTPT.WX
{
    /// <summary>
    /// 微信标签管理的API接口
    /// 开发者可以使用用户标签管理的相关接口，实现对公众号的标签进行创建、查询、修改、删除等操作，也可以对用户进行打标签、取消标签等操作。
    /// </summary>
    public interface ITagApi
    {
        /// <summary>
        /// 创建标签
        /// 一个公众号，最多可以创建100个标签。
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="name">标签名（30个字符以内）</param>
        /// <returns></returns>
        TagJson CreateTag(string accessToken, string name);


    }

    public class GROUP
    {
        public string groupid = "";
        public string name = "";
        public string id = "";
        public string count = "0";
    }

    public class GROUPS : GROUP
    {
        public new string count;
    }

    public class TagJson
    {
        /// <summary>
        /// 标签id，由微信分配
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 标签名，UTF8编码
        /// </summary>
        public string name { get; set; }
    }
    public class TagJsonBQ//标签
    {
       
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }

       
    }

    public class TagresultBQ//获取所有标签
    {
      
        public List<TagJsonBQ> tags= new List<TagJsonBQ>();// { get; set; }
    }

    public class TagJsonFS//获取某一个标签下粉丝openid
    {
        public int count { get; set; }
        public TagJsonFS_data data { get; set; }
        public string next_openid { get; set; }

    }

    public class TagJsonFS_data//获取某一个粉丝openid
    {
        public List<string> openid = new List<string>();

    }
    public class TagJsonQBFS//获取全部粉丝openid
    {
        public int totle { get; set; }

        public int count { get; set; }
        public TagJsonQBFS_data data { get; set; }
        public string next_openid { get; set; }

    }

    public class TagJsonQBFS_data//获取全部粉丝openid
    {
        public List<string> openid = new List<string>();

    }
    public class TagJsonHMD//获取黑名单openid
    {
        public int totle { get; set; }

        public int count { get; set; }
        public TagJsonHMD_data data { get; set; }
        public string next_openid { get; set; }

    }

    public class TagJsonHMD_data//获取黑名单粉丝openid
    {
        public List<string> openid = new List<string>();

    }
 
    public class TagJsonYHXX//获取用户信息
    {
        public int subscribe { get; set; }
        public string openid { get; set; }
        public int sex { get; set; }
        public string nickname { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public int subscribe_time { get; set; }
        public string unionid { get; set; }
        public string remark { get; set; }
        public int groupid { get; set; }
        public List<int> tagid_list = new List<int>();
    }

    public class TagJsonYHXX_list//获取用户信息粉丝openid
    {
        public List<TagJsonYHXX> user_info_list = new List<TagJsonYHXX>();

    }
    public class TagJsonBJ
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }     
    }
    public class Tagresult
    {
        /// <summary>
        /// 标签id，由微信分配
        /// </summary>
        public TagJson tag { get; set; }

 
    }
    /// <summary>
    /// 分组操作
    /// </summary>
    public class WX_Group : WX_Base
    {
        public GROUP group;//用于单个分组操作
        public GROUP groups;//用于查询所有分组
      
            public int id{ get; set; }
            public string name { get; set; }



            public override TagJson CreateTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);

                Url = "https://api.weixin.qq.com/cgi-bin/tags/create?access_token=" + oToken.result;
         
                //BQ content_out = new BQ();
                var data = new { tag = new { name = updateValue } };
                var postData = JsonConvert.SerializeObject(data);

                var postData1 = JsonConvert.DeserializeObject(postData);

                var postData2 = (postData1.ToString());

                var result = JsonHelper<Tagresult>.ConvertJson(Url, postData2);


               DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");

                CyQuery query=new CyQuery(conn);
                if (result.tag.id != 0 && result.tag.name != null)
                {
                    query.SQL.Text = "update WX_BQDY set TAGID=:TAGID";
                    query.SQL.Add(" where TAGMC=:TAGMC");
                    query.ParamByName("TAGID").AsInteger = result.tag.id;
                    query.ParamByName("TAGMC").AsString = result.tag.name;
                    //query.ParamByName("COUNT").AsInteger = result.tag.count;

                    query.ExecSQL();
                }

                return result.tag ; 
            }

            public override List<TagJsonBQ> GetTagList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/tags/get?access_token=" + oToken.result;

                var result = JsonHelper<TagresultBQ>.ConvertJsonGET(Url);



                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");

                CyQuery query = new CyQuery(conn);

                for (var i = 0; i < result.tags.Count; i++)
                {
                    query.SQL.Text = "update WX_BQDY set COUNT=:COUNT";
                    query.SQL.Add(" where TAGID=:TAGID");
                    query.ParamByName("TAGID").AsInteger = result.tags[i].id;
                    query.ParamByName("COUNT").AsInteger = result.tags[i].count;
                    query.ExecSQL();
                }
 
                return result.tags;
            }

            public override TagJsonBJ UpdateTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);

                Url = "https://api.weixin.qq.com/cgi-bin/tags/update?access_token=" + oToken.result;

                var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, updateValue);

                string str = updateValue;
			    JObject o = JObject.Parse(str);


                var id= o["tag"]["id"].ToString();

                var name= o["tag"]["name"].ToString();

                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");

                CyQuery query = new CyQuery(conn);
               query.SQL.Text = "update WX_BQDY set TAGMC=:TAGMC";
                query.SQL.Add(" where TAGID=:TAGID");
                query.ParamByName("TAGID").AsInteger =int.Parse(id);
                query.ParamByName("TAGMC").AsString = name;
                query.ExecSQL();

                
             

                return result;
            }

            public override TagJsonBJ DeleteTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                Token = (new WX_Token()).getToken(PUBLICIF1);
                //Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);

                Url = "https://api.weixin.qq.com/cgi-bin/tags/delete?access_token=" + oToken.result;

                int updateValue2 = int.Parse(updateValue);

                var data = new { tag = new { id = updateValue2 } };

                var postData = JsonConvert.SerializeObject(data);

                var postData1 = JsonConvert.DeserializeObject(postData);

                var postData2 = (postData1.ToString());


                var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, postData2);

                return result;
            }
       
            public override TagJsonFS GetFSList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                Token = (new WX_Token()).getToken(PUBLICIF1);
                //Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/user/tag/get?access_token=" + oToken.result;//获取粉丝token接口


                var result = JsonHelper<TagJsonFS>.ConvertJson(Url, updateValue);


                return result;
            }

            public override List<TagJsonYHXX> GetYHXXList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token=" + oToken.result;//获取粉丝token接口

                var result = JsonHelper<TagJsonYHXX_list>.ConvertJson(Url, updateValue);


                return result.user_info_list;
            }
            //获取用户列表
            public override TagJsonQBFS GetQBFSList(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);

                if (updateValue != "{}")
                {
                    Url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + oToken.result + "&next_openid=" + updateValue;//获取粉丝token接口
                }
                else
                {
                    Url = "https://api.weixin.qq.com/cgi-bin/user/get?access_token=" + oToken.result;//获取粉丝token接口
                }
                var result = JsonHelper<TagJsonQBFS>.ConvertJson(Url, updateValue);


                return result;
            }

            //批量打标签
            public override TagJsonBJ PLDBQTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, string iDJR, string sDJRMC, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);

                Url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?access_token=" + oToken.result;


                string str = updateValue;

                JObject o = JObject.Parse(str);
                var tagid = o["tagid"].ToString();

                var openid_LS = o["openid_list"];



                string str_LS = openid_LS.ToString();




                int count = str_LS.Split(',').Length;



                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                CyQuery query = new CyQuery(conn);


                for (var i = 0; i < count; i++)
                {
                    var openid = o["openid_list"][i].ToString();
                 
                    int iJLBH = SeqGenerator.GetSeq("WX_HYBQJL");
                    query.SQL.Text = "insert into WX_HYBQJL(JLBH,TAGID,OPENID,DJR,DJRMC,DJSJ,CZLX,PUBLICID)";
                    query.SQL.Add(" values(:JLBH,:TAGID,:OPENID,:DJR,:DJRMC,:DJSJ,:CZLX,:PUBLICID)");
                    query.ParamByName("JLBH").AsInteger = iJLBH;
                    query.ParamByName("DJR").AsInteger = int.Parse(iDJR);
                    query.ParamByName("DJRMC").AsString = sDJRMC;
                    query.ParamByName("OPENID").AsString = openid;
                    query.ParamByName("TAGID").AsInteger = int.Parse(tagid);
                    query.ParamByName("CZLX").AsInteger = 0;
                    query.ParamByName("DJSJ").AsDateTime = DateTime.Now;
                    query.ParamByName("PUBLICID").AsInteger = int.Parse(PUBLICID);
                    query.ExecSQL();

                }

                 var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, updateValue);
                if (result.errmsg == "ok")
                {
                    for (var i = 0; i < count; i++)
                    {
                        var openid = o["openid_list"][i].ToString();
                        query.SQL.Text = "update WX_HYBQJL set CZLX=1";
                        query.SQL.Add(" where OPENID=:OPENID AND TAGID=:TAGID ");
                        query.ParamByName("TAGID").AsInteger = int.Parse(tagid);
                        query.ParamByName("OPENID").AsString = openid;
                        query.ExecSQL();


                        query.SQL.Text = "select * FROM WX_HYBQ where TAGID=" + int.Parse(tagid) + "and OPENID=" + "'"+openid+"'";
                        query.Open();
                        if (query.IsEmpty)
                        {
                            query.SQL.Text = "insert into WX_HYBQ(TAGID,OPENID,CREATETIME,PUBLICID)";
                            query.SQL.Add(" values(:TAGID,:OPENID,:CREATETIME,:PUBLICID)");
                            query.ParamByName("TAGID").AsInteger = int.Parse(tagid);
                            query.ParamByName("OPENID").AsString = openid;
                            query.ParamByName("CREATETIME").AsDateTime = DateTime.Now;
                            query.ParamByName("PUBLICID").AsInteger = int.Parse(PUBLICID);

                            query.ExecSQL();

                        }


                    }



                }
                else

                {
                    for (var i = 0; i < count; i++)
                    {
                        var openid = o["openid_list"][i].ToString();
                        query.SQL.Text = "update WX_HYBQJL set CZLX=2";
                        query.SQL.Add(" where OPENID=:OPENID AND TAGID=:TAGID ");
                        query.ParamByName("TAGID").AsInteger = int.Parse(tagid);
                        query.ParamByName("OPENID").AsString = openid;
                        query.ExecSQL();
                    }
                
                }
            

             
                return result;
            }
            //批量取消标签
            public override TagJsonBJ PLQXBQTag(out string msg, string PUBLICID, string PUBLICIF, string updateValue, string iDJR, string sDJRMC, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?access_token=" + oToken.result;

                var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, updateValue);


                string str = updateValue;


                JObject o = JObject.Parse(str);


                var tagid = o["tagid"].ToString();

                var openid_LS = o["openid_list"];


 
                string str_LS= openid_LS.ToString();




                 int count=str_LS.Split(',').Length;


                 
                DbConnection conn = CyDbConnManager.GetActiveDbConnection("CRMDB");
                CyQuery query = new CyQuery(conn);


                if (result.errmsg == "ok")
                {
                    for (var i = 0; i < count; i++)
                    {


                        var openid = o["openid_list"][i].ToString();
                       query.SQL.Text = "update WX_HYBQJL set CZLX=3";
                        query.SQL.Add(" where OPENID=:OPENID AND TAGID=:TAGID ");
                        query.ParamByName("TAGID").AsInteger = int.Parse(tagid);
                        query.ParamByName("OPENID").AsString = openid;
                        query.ExecSQL();


                     query.SQL.Text = "delete from WX_HYBQ where TAGID=" + int.Parse(tagid)+"And OPENID="+"'"+openid+"'";
                     query.ExecSQL();




                    }
                }
                else
                {
                    for (var i = 0; i < count; i++)
                    {
                        var openid = o["openid_list"][i].ToString();
                        query.SQL.Text = "update WX_HYBQJL set CZLX=4";
                        query.SQL.Add(" where OPENID=:OPENID AND TAGID=:TAGID ");
                        query.ParamByName("TAGID").AsInteger = int.Parse(tagid);
                        query.ParamByName("OPENID").AsString = openid;
                        query.ExecSQL();
                    }

                }



                return result;
            }
           //修改备注
            public override TagJsonBJ XGBZ(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                Token = (new WX_Token()).getToken(PUBLICIF1);
                //Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token=" + oToken.result;
                var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, updateValue);
                return result;
            }


            //加入黑名单
            public override TagJsonBJ ADDHMD(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchblacklist?access_token=" + oToken.result;
                var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, updateValue);
                return result;
            }

            //移出黑名单
            public override TagJsonBJ DELHMD(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/tags/members/batchunblacklist?access_token=" + oToken.result;
                var result = JsonHelper<TagJsonBJ>.ConvertJson(Url, updateValue);
                return result;
            }

            //获取黑名单列表
            public override TagJsonHMD SHOWHMD(out string msg, string PUBLICID, string PUBLICIF, string updateValue, HttpContext context = null)
            {
                msg = string.Empty;
                method = "POST";
                //var PUBLICIF1 = "http://wx.changyi.com/SaveWeChatData.ashx";
                //Token = (new WX_Token()).getToken(PUBLICIF1);
                Token = (new WX_Token()).getToken(PUBLICIF);
                Token1 oToken = new Token1();
                oToken = JsonConvert.DeserializeObject<Token1>(Token);
                Url = "https://api.weixin.qq.com/cgi-bin/tags/members/getblacklist?access_token=" + oToken.result;//获取粉丝token接口


                var result = JsonHelper<TagJsonHMD>.ConvertJson(Url, updateValue);


                return result;
            }


        public override string SearchData(out string msg, HttpContext context = null)
        {
            Url = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token=" + Token;
            returnString = WXRequestString(out msg);
            return returnString;
        }
        public override string InsertData(out string msg, HttpContext context = null)
        {
            Url = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token=" + Token;
            returnString = WXRequestString(out msg);
            return returnString;
        }
        public override string UpdateData(out string msg, HttpContext context = null)
        {
            Url = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token=" + Token;
            method = "POST";
            WXRequestString(out msg);
            if (msg == "")
            {
                return JsonConvert.SerializeObject(this);
            }
            return "";
        }
    }
}