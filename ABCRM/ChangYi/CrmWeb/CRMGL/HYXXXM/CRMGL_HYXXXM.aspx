<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMGL_HYXXXM.aspx.cs" Inherits="BF.CrmWeb.CRMGL.HYXXXM.CRMGL_HYXXXM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_InputList %>
    <script>
        vPageMsgID = '<%=CM_CRMGL_HYXXXMDEF%>';
    </script>
    <script src="CRMGL_HYXXXM.js"></script>
</head>
<body>
    <%=V_InputListBegin %>

    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">项目类型</div>
            <div class="bffld_right">
                <select id="DDL_CSLX" no_control="true">
                    <option no_control="true" value="0" selected="selected">证件类型  </option>
                    <option no_control="true" value="1">职业      </option>
                    <option no_control="true" value="2">家庭收入  </option>
                    <option no_control="true" value="3">学历      </option>
                    <option no_control="true" value="4">至商场时  </option>
                    <option no_control="true" value="5">交通工具  </option>
                    <option no_control="true" value="6">家庭成员  </option>
                    <option no_control="true" value="7">兴趣爱好  </option>
                    <option no_control="true" value="9">促销信息  </option>
                    <option no_control="true" value="10">信息方式  </option>
                    <option no_control="true" value="13">民族      </option>
                    <option no_control="true" value="11">汽车品牌 </option>
                </select>
            </div>
        </div>
    </div>
    <div class="bfrow" style="display: none">
        <div class="bffld" id="jlbh">
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">项目序号</div>
            <div class="bffld_right">
                <input id="TB_INDEX" type="text" />
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">项目内容</div>
            <div class="bffld_right">
                <input id="TB_CONTENT" type="text" />
            </div>
        </div>
    </div>
    <%=V_InputListEnd %>
</body>
</html>
