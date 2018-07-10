<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSPTJDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSPTJDY.GTPT_WXSPTJDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = <%=CM_GTPT_WXSPTJ%>;
    </script>
    <script src="GTPT_WXSPTJDY.js"></script>
    <script src="../../../Js/jquery.form.js"></script>
    <script src="../../../Js/KindEditor/kindeditor.js"></script>
    <script src="../../../Js/KindEditor/lang/zh-CN.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">导航页展示</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="status" id="R_F" value="0" />
                <label for="R_F">否</label>
                <input class="magic-radio" type="radio" name="status" id="R_Y" value="1" />
                <label for="R_Y">是</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商户名称</div>
            <div class="bffld_right">
                <input id="TB_SHMC" type="text" />
                <input type="hidden" id="HF_SHDM" />
                <input type="hidden" id="zHF_SHDM" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text" />
                <input type="hidden" id="HF_MDID" />
                <input type="hidden" id="zHF_MDID" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商品名称</div>
            <div class="bffld_right">
                <input id="TB_SPMC" type="text" />
                <input type="hidden" id="HF_SPDM" />
                <input type="hidden" id="zHF_SPDM" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商品简称</div>
            <div class="bffld_right">
                <input id="TB_SPJC" type="text" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">网址</div>
            <div class="bffld_right">
                <input id="TB_IP" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">电话</div>
            <div class="bffld_right">
                <input id="TB_PHONE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">地址</div>
            <div class="bffld_right">
                <input id="TB_DZ" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">价格</div>
            <div class="bffld_right">
                <input id="TB_SPJG" type="text" />

            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">商标名称</div>
            <div class="bffld_right">
                <input id="TB_SBMC" type="text" />
                <input type="hidden" id="HF_SBID" />
                <input type="hidden" id="zHF_SBID" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">排序</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" />
            </div>
            <div style="left: 155px; position: relative; line-height: 25px; color: red;">注：正数序号越小，排序越靠前</div>

        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传LOGO图片</div>
            <div class="bffld_right">
                <input id="TB_LOGO" type="text" tabindex="2" />
            </div>
        </div>
        <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file1" id="files1" />
                </span>
                <input type="button" value="点击上传" id="upload1" />
            </form>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传主图片</div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
        <div class="div2">
            <form id="form2" method="post" name="form2" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="upload" />
            </form>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">商品参数</div>
            <div class="bffld_right">
                <textarea id="TA_SPCS"></textarea>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">商品介绍</div>
            <div class="bffld_right">
                <textarea id="TA_CONTENT"></textarea>
            </div>
        </div>
    </div>



    <div class="bfrow">
        <table id="list"></table>
        <div id="pager"></div>
    </div>
    <%=V_InputBodyEnd %>
    <script>
        KindEditor.ready(function (K) {
            window.editor = K.create('#TA_SPCS', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1"});
            window.editor2 = K.create('#TA_CONTENT', { uploadJson: "../../CrmLib/CrmLib.ashx?func=upload&folder=sample1"});
        });
    </script>
</body>
</html>
