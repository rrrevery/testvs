<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HYKGL_HYKJKZK.aspx.cs" Inherits="BF.CrmWeb.HYKGL.HYKJKZK.HYKGL_HYKJKZK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%=V_Head_Input%>
    <script>
        var jkjlbh = GetUrlParam("jkjlbh");
        var vCZK = GetUrlParam("czk");
        if(jkjlbh!=undefined)
        {
            vPageMsgID = '<%=CM_HYKGL_HYKJK_XK%>';
        }
        else
        {
            vPageMsgID = '<%=CM_HYKGL_HYKBC%>';
        }
        if(vCZK=="1")
        {
            vPageMsgID2 = <%=CM_MZKGL_MZKJK%>;
        }
        else
        {
            vPageMsgID2 = '<%=CM_HYKGL_HYKJK%>';
        }
        vPageMsgID3 = '<%=CM_HYKGL_HYKCX%>';
        bCanWriteCard = CheckMenuPermit(iDJR, <%=CM_HYKGL_HYKJK_XK%>);
    </script>
    <script type="text/javascript" src="http://localhost:22345/writeCard.js"></script>
    <script src="HYKGL_HYKJKZK.js"></script>
        <script src="../../CrmLib/CrmLib_GetData.js"></script>

</head>
<body>
    <%--    <object
        id="rwcard"
        classid="clsid:936CB8A6-052B-4ECA-9625-B8CF4CE51B5F"
        codebase="../../CrmLib/RWCard/BFCRM_RWCard.inf"
        width="0"
        height="0">
    </object>--%>
    <%=V_InputBodyBegin%>
    <div class="bfrow">
        <div class="bffld" style="display: none;">
            <div id="jlbh"></div>
        </div>
        <div class="bffld">
            <div class="bffld_left" style="white-space: nowrap;">建卡记录编号</div>
            <div class="bffld_right">
                <input type="text" id="TB_JKJLBH" />
                <input type="hidden" id="HF_HYID" />
                <input type="hidden" id="HF_CNDR" />
            </div>
        </div>
    </div>
    <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">总数量</div>
            <div class="bffld_right">
                <label id="LB_ZSL"></label>
            </div>
        </div>
        <div class="bffld">
            <div class="bffld_left">制卡数量</div>
            <div class="bffld_right">
                <label id="LB_ZKSL"></label>
            </div>
        </div>
    </div>
      <div class="bfrow">
        <div class="bffld">
            <div class="bffld_left">卡号</div>
            <div class="bffld_right">
                <input id="TB_HYKNO1" type="text" />
            </div>
        </div>
          <div class="bffld">
            <div class="bffld_left">---</div>
            <div class="bffld_right">
                 <input id="TB_HYKNO2" type="text"  />
            </div>
        </div>


    </div>


    <div class="bfrow" style="display: none">
        <div class="bffld">
            <input id="B_ALL" type="button" value="显示所有卡" class="bfbut bfblue" />
            <input id="B_ZK1" type="button" value="显示已制卡" class="bfbut bfblue" />
            <input id="B_ZK0" type="button" value="显示未制卡" class="bfbut bfblue" />
        </div>
        <div class="bffld">
        </div>
    </div>

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
    <%=V_InputBodyEnd%>
</body>
</html>
