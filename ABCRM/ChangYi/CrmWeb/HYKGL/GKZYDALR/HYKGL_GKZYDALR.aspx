<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_GKZYDALR.aspx.cs" Inherits="BF.CrmWeb.HYKGL.GKZYDALR.HYKGL_GKZYDALR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <script>
        vPageMsgID = '<%=CM_HYKGL_GKZYDALR %>';
    </script>
    <script src="HYKGL_GKZYDALR.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
</head>
<body>
    <%=V_InputBodyBegin %>

    <div class="maininput bfborder_bottom">
        <div style="float: left; width: 33%; text-align: center">
            <img src="../../../image/Img/Image.jpg" id="HeadPhoto" style="width: 90%; height: 200px; padding-left: 20px; float: left" />
            <input style="text-align: center" id="takePhoto" type="button" class="bfbut bfblue" value="拍照" />
            <input type="hidden" id="HF_IMGURL" />
            <input type="hidden" id="HF_IMGURL_OLD" />
        </div>
        <div style="float: left; width: 67%">
            <div class="bfrow">
                <div class="bffld" style="width: 49%" id="jlbh">
                </div>
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">证件号码</div>
                    <div class="bffld_right">
                        <input id="TB_SFZBH" type="text" maxlength="18" />
                        <input id="HF_GKID" type="hidden" />
                        <input id="HF_HYID" type="hidden" />
                        <input id="HF_SFZBH" type="hidden" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">姓名</div>
                    <div class="bffld_right">
                        <input id="TB_GKNAME" type="text" />
                        <input id="HF_GKNAME" type="hidden" />
                    </div>
                </div>
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">性别</div>
                    <div class="bffld_right">
                        <input class="magic-radio" type="radio" name="sex" id="Radio1" value="0" disabled="disabled" />
                        <label for="Radio1">男</label>
                        <input class="magic-radio" type="radio" name="sex" id="Radio2" value="1" disabled="disabled" />
                        <label for="Radio2">女</label>
                    </div>
                </div>
            </div>


            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">手机号码</div>
                    <div class="bffld_right">
                        <input id="TB_SJHM" type="text" maxlength="11" />
                        <input id="HF_SJHM" type="hidden" />
                        <input id="HF_SJHMOLD" type="hidden" />
                    </div>
                </div>

                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">验证码 </div>
                    <div class="bffld_right">
                        <input id="TB_YZM" type="text" maxlength="6" />
                        <input id="HF_YZM" type="hidden" />
                        <input type="button" class="bfbtn btn_query" id="btnYZM" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">出生日期</div>
                    <div class="bffld_right">
                        <input id="TB_CSRQ" class="Wdate" type="text" onfocus="WdatePicker({maxDate:'%y-%M-#{%d}'})" readonly="readonly" />
                    </div>
                </div>
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">生肖</div>
                    <div class="bffld_right">
                        <input id="TB_SX" type="text" readonly="readonly" />
                    </div>
                </div>
            </div>

            <div class="bfrow">
                <div class="bffld" style="width: 49%">
                    <div class="bffld_left">星座</div>
                    <div class="bffld_right">
                        <input id="TB_XZ" type="text" readonly="readonly" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%=V_InputBodyEnd %>
</body>
</html>
