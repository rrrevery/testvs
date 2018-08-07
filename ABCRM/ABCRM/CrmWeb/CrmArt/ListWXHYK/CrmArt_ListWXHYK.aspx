<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_ListWXHYK.aspx.cs" Inherits="BF.CrmWeb.CrmArt.ListWXHYK.CrmArt_ListWXHYK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_WebArtList%>
    <script>
        vPageMsgID = <%=CM_GTPT_WXHYKCX%>;
    </script>



    <script src="CrmAtr_ListWXHYX.js"></script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>


</head>

    <body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>

        <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">开始卡号</div>
                <div class="bffld_right">
                    <input id="TB_KSKH" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">结束卡号</div>
                <div class="bffld_right">
                    <input id="TB_JSKH" type="text" />
                </div>
            </div>
        </div>

           <div class="bfrow">
            <div class="bffld_art">
                <div class="bffld_left">会员姓名</div>
                <div class="bffld_right">
                    <input id="TB_HYNAME" type="text" />

                </div>
            </div>
            <div class="bffld_art">
                <div class="bffld_left">卡类型</div>
                <div class="bffld_right">
                 <input id="TB_HYKNAME" type="text" />
                <input id="HF_HYKTYPE" type="hidden" />
                <input id="zHF_HYKTYPE" type="hidden" />
                </div>
            </div>
        </div>
            <div class="bfrow">
           <div class="bffld">
            <div class="bffld_left">性别</div>
            <div class="bffld_right">              
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_B" value="0" />
                <label for="C_B">男</label>
                <input class="magic-checkbox" type="checkbox" name="CB_SEX" id="C_G" value="1" />
                <label for="C_G">女</label>
           
                <input type="hidden" id="HF_SEX" />
            </div>
        </div>
        </div>
             <div class="bfrow">
               <div class="bffld_art">
                <div class="bffld_left">职业类型</div>
                <div class="bffld_right">
                    <select id="DDL_ZY">
                        <option selected="selected"></option>
                    </select>
                </div>
            </div>
                                       </div>

        <%--   <div class="bfrow">
                <div class="bffld_l2">
                    <div class="bffld_left">职业</div>
                    <div class="bffld_right">
                        <div id="CB_ZYLX" ></div>
                    </div>
                </div>
            </div>--%>
    
        <table id="list"></table>
    </div>
</body>


</html>
