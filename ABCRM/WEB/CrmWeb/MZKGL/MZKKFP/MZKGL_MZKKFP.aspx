<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MZKGL_MZKKFP.aspx.cs" Inherits="BF.CrmWeb.MZKGL.MZKKFP.MZKGL_MZKKFP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <%=V_Head_Input %>
    <script>
  
            vPageMsgID = '<%=CM_MZKGL_MZKKFP%>';
        
  </script>
    <script src="../../CrmLib/CrmLib_GetData.js"></script>
    <script src="MZKGL_MZKKFP.js"></script>
   <script src="../../CrmLib/CrmLib_FillTree.js"></script>

</head>
<body>
 
     <%=V_InputBodyBegin %>
    <div class="bfrow">
        <div class="bffld" id="jlbh" style="display:none">
        </div>
        <div class="bffld">
            <div class="bffld_left">售卡记录编号</div>
            <div class="bffld_right">
                <input id="TB_SKJLBH" type="text" readonly="readonly"/>
                <input id="HF_SKJLBH" type="hidden" />
                <input id="zHF_SKJLBH" type="hidden" />
            </div>
        </div>
                </div>

        <div class="bfrow">
       
        <div class="bffld">
            <div class="bffld_left">操作地点</div>
            <div class="bffld_right">
                <input id="TB_BGDDMC" type="text" readonly="readonly" style="background-color: bisque;" />
                <input id="HF_BGDDDM" type="hidden" />
            </div>
        </div>
    </div>



          <div class="bfrow">
         <div class="bffld_l">
            <div class="bffld_left">类型</div>
            <div class="bffld_right">
            
                <input type="radio" value="2" name="CLLX" class="magic-radio" id="R_LQ" />
                <label for="R_LQ">已开</label>
                <input type="radio" value="3" name="CLLX" class="magic-radio" id="R_CZ" checked="checked"/>
                <label for="R_CZ">未开</label>
               
            </div>
        </div>



    </div>

     <div class="bfrow">

                 <div class="bffld">
            <div class="bffld_left">售卡日期</div>
            <div class="bffld_right">
                <input id="TB_DJSJ1" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
                <span class="Wdate_span">至</span>
                <input id="TB_DJSJ2" type="text" class="Wdate" onfocus="WdatePicker({isShowWeek:true})" style="width: 45%; float: left;" />
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
   
      

    <div class="bfrow bfrow_table">
        <table id="list" style="border: thin"></table>
    </div>
     <div id="tb" class="item_toolbar">
        <span style="float: left"></span>
        <button id="Dosearch" type='button' class="item_addtoolbar">查询</button>
    </div>

    <%=V_InputBodyEnd %>



</body>
</html>
