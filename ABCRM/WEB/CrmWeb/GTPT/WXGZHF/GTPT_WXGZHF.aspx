<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXGZHF.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXGZHF.GTPT_WXGZHF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vTYPE = GetUrlParam("type");
        if (vTYPE == "2") {
            vPageMsgID = '<%=CM_GTPT_WXHF_GZ%>';
            vCaption = "微信关注回复";
        }
        if (vTYPE == "3") {
            vPageMsgID = '<%=CM_GTPT_WXHF_MR%>';
            vCaption = "微信默认回复";
        }
        if (vTYPE == "0") {
            vPageMsgID = '<%=CM_GTPT_WXHF_GJC%>';
            vCaption = "微信关键字回复";
        }
        if (vTYPE == "1") {
            vPageMsgID = '<%=CM_GTPT_WXHF_TS%>';
            vCaption = "微信菜单推送回复";
        }
    </script>
     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXGZHF.js"></script>
    <script src="../GTPTLib/Plupload/jquery.form.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow" style="display:none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">问题/关键字</div>
            <div class="bffld_right">
                <select id="DDL_WT" onclick="changewt()">
                    <option></option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">回复规则</div>
            <div class="bffld_right">
                <select id="DDL_GZ" class="easyui-combobox">
<%--                    <option></option>--%>
                </select>
                <input type="hidden" id="TF_HFGZ" value="" />
            </div>
        </div>
    </div>
    <div id="ruleList">
        <fieldset  id="text" class="rule_ask">
            <legend>文字回复</legend>
            <textarea id="TA_WZHF" cols="100" rows="5"></textarea>
        </fieldset>

        <fieldset  id="image" class="rule_ask">
            <legend>图片回复</legend>
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

        <fieldset  id="voice" class="rule_ask">
            <legend>语音回复</legend>
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
        <fieldset  id="video" class="rule_ask">
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
        <fieldset  id="music" class="rule_ask">
            <legend>音乐回复</legend>
            <div class="bfrow">
                <div class="bffld">
                    <font color='red'>注：目前腾讯已不支持音乐回复</font>
                </div>
            </div>
        </fieldset>
        <fieldset  id="news" class="rule_ask">
            <legend>图文回复</legend>
            <div class="bfrow">
                <div class="bffld" style="height: inherit">
                    <font color='red'>注：序号由小到大排序最多为10条</font>
                </div>
            </div>
<%--            <div class="bfrow">
                <div class="bffld" style="height: inherit">
                    <div class="bffld_left">
                        <button id="AddItem" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>添加</button>
                    </div>
                    <div class="bffld_right">
                        <button id="updateItem" tabindex="100" style="height: 28px; width: 70px; padding: 0px; margin: 5px 0 0 20px;" type='button' class='button'>修改</button>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">
                    </div>
                    <div class="bffld_right">
                        <button id="DelItem" tabindex="100" style="height: 28px; width: 70px;" type='button' class='button'>删除</button>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div style="clear: both;"></div>
            </div>--%>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">序号</div>
                    <div class="bffld_right">
                        <input id="TB_XH" type="text" tabindex="" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">标题</div>
                    <div class="bffld_right">
                        <input id="TB_TITLE" type="text" tabindex="" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">描述</div>
                    <div class="bffld_right">
                        <input id="TB_DESCRIBE" type="text" tabindex="" />
                    </div>
                </div>
                <div class="bffld">
                    <div class="bffld_left">消息链接</div>
                    <div class="bffld_right">
                        <input id="TB_URL" type="text" tabindex="" />
                    </div>
                </div>
            </div>
            <div class="bfrow">
                <div class="bffld">
                    <div class="bffld_left">图片链接</div>
                    <div class="bffld_right">
                        <input id="TB_IMG" type="text" readonly="true" />
                    </div>
                </div>
                <div class="bffld">
                    <form id="form" method="post" name="form" enctype="multipart/form-data">

                        <span class="img_span">
                            <input type="file" name="file" id="files" />
                        </span>
                        <input type="button" value="点击上传" id="upload" class="button" />
                    </form>
                </div>
            </div>
    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>

    <div id="tb" class="item_toolbar">
        <span style="float: left"></span>
        <button id="AddItem" type='button' class="item_addtoolbar">添加</button>
        <button id="DelItem" type='button' class="item_deltoolbar">删除</button>
    </div>
        </fieldset>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
