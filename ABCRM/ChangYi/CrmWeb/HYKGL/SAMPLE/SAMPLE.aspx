<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SAMPLE.aspx.cs" Inherits="BF.CrmWeb.HYKGL.SAMPLE.SAMPLE" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input %>
    <link href="imgbox.css" rel="stylesheet" />
    <script src="SAMPLE.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="jquery.imgbox.pack.js"></script>
    <script src="../../../Js/KindEditor/kindeditor.js"></script>
    <script src="../../../Js/jquery.form.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#example1-1").imgbox();

            $("#example1-2").imgbox({
                'zoomOpacity': true,
                'alignment': 'center'
            });

            $("#example1-3").imgbox({
                'speedIn': 0,
                'speedOut': 0
            });

            $("#example2-1, #example2-2").imgbox({
                'speedIn': 0,
                'speedOut': 0,
                'alignment': 'center',
                'overlayShow': true,
                'allowMultiple': false
            });
        });
    </script>
</head>
<body>
    <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh">
        </div>
        <div class="bffld">
            <div class="bffld_left">Easy下拉</div>
            <div class="bffld_right">
                <select id="DDL_MD" class="easyui-combobox" name="MD" data-options="value:'',editable:false,panelHeight:'auto'">
                </select>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">普通下拉</div>
            <div class="bffld_right">
                <select>
                    <option>测试</option>
                    <option>测试2</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld" style="height: 30px; width: 300px;">
            <select style="height: 30px">
                <option>测试</option>
                <option>测试2</option>
            </select>
        </div>
        <div class="bffld" style="height: 30px; width: 300px;">
            <select id="DDL_MD2" class="easyui-combobox" style="height: 30px">
            </select>
        </div>

    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">三列</div>
            <div class="bffld_right">
                <input type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">三列</div>
            <div class="bffld_right">
                <input type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">三列</div>
            <div class="bffld_right">
                <input type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">单列66%长</div>
            <div class="bffld_right">
                <select>
                    <option>测试</option>
                    <option>测试2</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l2">
            <div class="bffld_left">单列99%长</div>
            <div class="bffld_right">
                <select>
                    <option>测试</option>
                    <option>测试2</option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">第一列33%长</div>
            <div class="bffld_right">
                <input type="text" />
            </div>
        </div>
        <div class="bffld_l">
            <div class="bffld_left">第二列66%长</div>
            <div class="bffld_right">
                <input type="text" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l3">
            <div class="bffld_left">左边33%</div>
            <div class="bffld_right">
                <input type="text" value="右边66%" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">性别</div>
            <div class="bffld_right">
                <input class="magic-radio" type="radio" name="radio" id="R_M" value="0" />
                <label for="R_M">男</label>
                <input class="magic-radio" type="radio" name="radio" id="R_F" value="1" />
                <label for="R_F">女</label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">爱好</div>
            <div class="bffld_right">
                <input class="magic-checkbox" type="checkbox" name="layout" id="C_M" value="0" />
                <label for="C_M">男</label>
                <input class="magic-checkbox" type="checkbox" name="layout" id="C_F" value="1" />
                <label for="C_F">女</label>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <%--<input type="button" class="button button-primary button-rounded button-small" value="测试" style="width: 120px; height: 30px" />--%>
            <div class="bffld_left">按钮</div>
            <div class="bffld_right">
                <input type="button" class="bfbut bfblue" value="蓝" onclick="ShowFWB()" />
                <input type="button" class="bfbut bfgreen" value="绿" onclick="SendSMSProc()" />
                <input type="button" class="bfbut bfred" value="红" onclick="SetFWB()" />
                <input type="button" class="bfbut bfred" value="打印" onclick="PrintData()" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">图片上传</div>
            <div class="bffld_right">
                <input id="TB_IMAGE" type="text" />
                <input type="hidden" id="HF_IMAGEURL" />
                <%--<form id="form1" method="post" name="form1" enctype="multipart/form-data">
                    <input type="file" name="file" id="files" accept="image/jpeg,image/x-png" />
                </form>--%>
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld_l">
            <div class="bffld_left">富文本</div>
            <div class="bffld_right">
                <textarea id="editor_id"></textarea>
            </div>
        </div>
    </div>
    <div id="content">
        <h1>imgBox</h1>
        <hr />
        <div id="images">

            <%--<a id="example1-1" title="" href="images/4006876523_289a8296ee.jpg">
                <img alt="" src="images/4006876523_289a8296ee_m.jpg" /></a>
            <a id="example1-2" title="Lorem ipsum dolor sit amet" href="images/photo_unavailable.gif">
                <img alt="" src="images/photo_unavailable_m.gif" /></a>
            <a id="example1-3" title="Maecenas eros massa, pulvinar et sagittis adipiscing, &lt;br /&gt; molestie et arcu" href="images/4003912116_389c3891cf.jpg">
                <img alt="" src="images/4003912116_389c3891cf_m.jpg" /></a>

            <a id="example2-1" title="" href="images/3793633187_44790d1f0a_o.jpg">
                <img alt="" src="images/3793633187_f56bb1bf99_m.jpg" /></a>
            <a id="example2-2" title="Maecenas eros massa, pulvinar et sagittis adipiscing, molestie et arcu" href="images/3793633099_3e1e53e4ac_o.jpg">
                <img alt="" src="images/3793633099_4f9c3e08b3_m.jpg" /></a>--%>
        </div>

        <div id="credit"></div>
    </div>
    <!-- 代码 结束 -->
    <%=V_InputBodyEnd %>
</body>
</html>
