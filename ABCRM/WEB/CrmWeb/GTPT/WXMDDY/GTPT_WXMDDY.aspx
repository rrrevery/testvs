<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GTPT_WXMDDY.aspx.cs" Inherits="BF.CrmWeb.GTPT.WXMDDY.GTPT_WXMDDY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script type="text/javascript">
        vPageMsgID = '<%=CM_GTPT_WXMDDY%>';
        bCanEdit = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXMDDY%>');
        bCanExec = CheckMenuPermit(iDJR, '<%=CM_GTPT_WXMDDY%>');
    </script>
        <script src="../../CrmLib/CrmLib_GetData.js"></script>

     <script src="../../CrmLib/CrmLib_BillWX.js"></script>
    <script src="GTPT_WXMDDY.js"></script>
    <script src="../../../Js/jquery.form.js"></script>

</head>
<body>
<%=V_InputBodyBegin %>
    <div class="bfrow" style="display:none">
        <div class="bffld" id="jlbh">
        </div>
    
    </div>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店名称</div>
            <div class="bffld_right">
                <input id="TB_MDMC" type="text"/>
                <input id="HF_MDID" type="hidden"/>
                <input id="zHF_MDID" type="hidden"/>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">城市</div>
            <div class="bffld_right">
                <input id="TB_CSMC" type="text"/>
                <input id="HF_CSID" type="hidden"/>
                <input id="zHF_CSID" type="hidden"/>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店电话</div>
            <div class="bffld_right">
                <input id="TB_PHONE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">营业时间</div>
            <div class="bffld_right">
                <input id="TB_TIME" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店地址</div>
            <div class="bffld_right">
                <input id="TB_ADDRESS" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店地铁</div>
            <div class="bffld_right">
                <input id="TB_SUBWAY" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">门店驾车</div>
            <div class="bffld_right">
                <input id="TB_DRIVE" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店公交</div>
            <div class="bffld_right">
                <input id="TB_BUS" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">停车信息</div>
            <div class="bffld_right">
                <input id="TB_PARK" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">停车费用</div>
            <div class="bffld_right">
                <input id="TB_PAY" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">优惠信息</div>
            <div class="bffld_right">
                <input id="TB_YHXX" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">门店内容</div>
            <div class="bffld_right">
                <input id="TB_MDNR" type="text" />
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
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">百度地图经度</div>
            <div class="bffld_right">
                <input id="TB_LAT" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">百度地图维度</div>
            <div class="bffld_right">
                <input id="TB_LEN" type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l" >
            <div class="bffld_left">百度地图描述</div>
            <div class="bffld_right">
                <input id="TB_CONTENT" type="text" />
            </div>
        </div>
    </div>

    <div class="bfrow">
<%--        <div class="bffld">
            <div class="bffld_left">图片上传</div>
            <div class="bffld_right">
                <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                    <input id="TB_IMG" type="text" />
                    <input type="file" name="file" id="files" style="display: none" />
                    <button id="upload" class="bfbtn btn_upload " onclick="$('input[id=files]').click();"></button>
                </form>
            </div>
        </div>--%>

        <div class="bffld">
            <div class="bffld_left">上传门店图片 </div>
            <div class="bffld_right">
                <input id="TB_IMG" type="text" tabindex="2" />
            </div>
        </div>
       <%-- <div class="bffld">
            <form id="form1" method="post" name="form1" enctype="multipart/form-data">
                <span class="img_span">
                    <input type="file" name="file" id="files" />
                </span>
                <input type="button" value="点击上传" id="upload" />
            </form>
        </div>--%>
    </div>
    
    <%=V_InputBodyEnd %>
</body>
</html>
