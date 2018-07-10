<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_MEDIADY.aspx.cs" Inherits="BF.CrmWeb.GTPT.MEDIADY.GTPT_MEDIADY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_MEDIADY%>';
    </script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_MEDIADY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <%-- 图片（image）、语音（voice）、视频（video）和缩略图（thumb）--%>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">素材类型</div>
            <div class="bffld_right">
                <select id="DDL_TYPE" class="easyui-combobox">  
                    <option></option>
                    <option value="2" type="image">图片</option>
                    <option value="3" type="voice">语音</option>
                    <option value="4" type="video">视频</option>
<%--                    <option value="5" type="voice">音乐</option>--%>
                    <option value="8" type="thumb">缩略图</option>
                </select>
            </div>
        </div>
    </div>

    <div id="ruleList">

        <fieldset id="image" class="rule_ask">
            <legend>图片</legend>
            <div class="bfrow">
                <div class="bffld">
                    <font color='red'>注：2M，支持bmp/png/jpeg/jpg/gif格式</font>
                </div>
                <div class="bffld">
                    <input id="HF_MID" type="hidden" />
                    <form id="form1" method="post" name="form1" enctype="multipart/form-data">

                        <span class="img_span">
                            <input type="file" name="file1" id="file_image" />
                        </span>
                        <input type="button" value="点击上传图片" id="upload_img" />
                    </form>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">标题</div>
                    <div class="bffld_right">
                        <input id="TB_ImgTITLE" type="text" />

                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">描述</div>
                    <div class="bffld_right">
                        <input id="TB_DESCRIPTION" type="text" />
                    </div>
                </div>

            </div>

        </fieldset>
        <fieldset id="voice" class="rule_ask">
            <legend>语音</legend>
            <div class="bfrow">
                <div class="bffld">
                    <font color='red'>注：2M，播放长度不超过60s，支持AMR\MP3格式</font>
                </div>
                <div class="bffld">
                    <form id="form2" method="post" name="form1" enctype="multipart/form-data">

                        <span class="img_span">
                            <input type="file" name="file1" id="file_voice" class="button" />
                        </span>
                        <input type="button" value="点击上传语音" id="upload_voice" />
                    </form>
                </div>

            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">标题</div>
                    <div class="bffld_right">
                        <input id="TB_YYTITLE" type="text" />

                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">描述</div>
                    <div class="bffld_right">
                        <input id="TB_YYDESCRIPTION" type="text" />
                    </div>
                </div>
            </div>
        </fieldset>
        <fieldset id="video" class="rule_ask">
            <legend>视频回复</legend>
            <div class="bfrow">
                <div class="bffld">
                    <font color='red'>注：10MB，支持MP4格式</font>
                </div>
                <div class="bffld">
                    <form id="form3" method="post" name="form1" enctype="multipart/form-data">

                        <span class="img_span">
                            <input type="file" name="file1" id="file_video" />
                        </span>
                        <input type="button" value="点击上传视频" id="upload_video" class="button" />
                    </form>
                </div>

            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">标题</div>
                    <div class="bffld_right">
                        <input id="TB_SPTLTLE" type="text" />

                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">描述</div>
                    <div class="bffld_right">
                        <input id="TB_SPDESCRIPTION" type="text" />
                    </div>
                </div>
            </div>
        </fieldset>

<%--        <fieldset id="music" class="rule_ask">
            <legend>音乐</legend>
            <div class="bfrow">
                <div class="bffld">
                    <font color='red'>注：目前腾讯已不支持音乐回复</font>
                </div>
            </div>
        </fieldset>--%>

        <fieldset id="thumb" class="rule_ask">
            <legend>缩略图</legend>
            <div class="bfrow">
                <div class="bffld">
                    <font color='red'>注：64KB，支持JPG格式</font>
                </div>
                <div class="bffld">
                    <form id="form4" method="post" name="form1" enctype="multipart/form-data">
                        <span class="img_span">
                            <input type="file" name="file1" id="file_thumb" />
                        </span>
                        <input type="button" value="点击上传缩略图" id="upload_thumb" class="button" />
                    </form>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">标题</div>
                    <div class="bffld_right">
                        <input id="TB_SLTTITLE" type="text" />

                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">描述</div>
                    <div class="bffld_right">
                        <input id="TB_SLTDESCRIPTION" type="text" />
                    </div>
                </div>
            </div>
        </fieldset>

    </div>


    <%=V_InputBodyEnd %>
</body>
</html>
