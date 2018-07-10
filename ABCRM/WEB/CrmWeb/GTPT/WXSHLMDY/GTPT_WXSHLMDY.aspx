<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXSHLMDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXSHLMDY.GTPT_WXSHLMDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_GTPT_WXSHLM%>';
    </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="GTPT_WXSHLMDY.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left">联盟商户名称</div>
            <div class="bffld_right">
                <input id="TB_LMSHMC" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">类型名称</div>
            <div class="bffld_right">
                <input id="TB_LMSHLXMC" type="text"/>
                <input id="HF_LMSHLXID" type="hidden"/>
                <input id="zHF_LMSHLXID" type="hidden"/>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">人均单价</div>
            <div class="bffld_right">
                <input id="TB_RJDJ" type="text" />
            </div>
        </div>
    </div>


    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠活动介绍</div>
            <div class="bffld_right">
                <input id="TB_YHJS" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">地址</div>
            <div class="bffld_right">
                <input id="TB_ADDRESS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">营业时间</div>
            <div class="bffld_right">
                <input id="TB_YJSJ" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">联系电话</div>
            <div class="bffld_right">
                <input id="TB_PHONE" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">百度地图经度</div>
            <div class="bffld_right">
                <input id="TB_LAT" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">百度地图纬度</div>
            <div class="bffld_right">
                <input id="TB_LEN" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">百度地图位置</div>
            <div class="bffld_right">
                <input id="TB_TITLE" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">百度地图描述</div>
            <div class="bffld_right">
                <input id="TB_CONTENT" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">收藏人数</div>
            <div class="bffld_right">
                <input id="TB_SCRS" type="text" />

            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">显示顺序</div>
            <div class="bffld_right">
                <input id="TB_INX" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">

        <div class="bffld">
            <div class="bffld_left">渠道</div>
            <div class="bffld_right">
                <select id="DDL_CHANNELID" class="easyui-combobox">
                    <option value=""></option>
                    <option value="0">全部</option>
                    <option value="1">微信</option>
                    <option value="2">APP</option>
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">商家介绍</div>
            <div class="bffld_right">
                <input id="TB_NR" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传LOGO图片</div>
            <div class="bffld_right">
                <input id="TB_LOGO" type="text" tabindex="2" />
            </div>
        </div>
       <%-- <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file1" id="files1" />
                </span>
                <input type="button" value="点击上传" id="B_LOGO" />
            </form>
        </div>--%>
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">上传主图片</div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
        <%--<div class="bffld">
            <form id="form2" method="post" name="form2" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="B_IMG" />
            </form>
        </div>--%>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停用标记</div>
            <div class="bffld_right">
                <input id="CK_iBJ_TY" type="checkbox" tabindex="1" style="width: 50px" class="magic-checkbox" />
                <label for='CK_iBJ_TY'></label>
            </div>
        </div>
    </div>

    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">备注</div>
            <div class="bffld_right">
                <input id="TB_BZ" type="text" />

            </div>
        </div>

    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
