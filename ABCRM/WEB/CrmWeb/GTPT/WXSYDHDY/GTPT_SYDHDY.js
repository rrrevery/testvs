vUrl = "../GTPT.ashx";
var sPSWD;
var sURL;
var sUSER;
var saveIp;
var imagesFirstName = "";
var iDHID = 0;
var selectId = -1;
var selectId2 = -1;
var selectId3 = -1;

function InitGrid() {
    vLBTColumnNames = ['轮播图顺序号', '轮播图片地址', '轮播链接地址', ];
    vLBTColumnModel = [
             { name: 'iTURNINX', width: 100 },
          { name: 'iTURNIMG', editor: 'text', width: 100 },
          { name: 'iTURNURL', editor: 'text', width: 100 },

    ];
    vZDHBColumnNames = ['子导航顺序号', '子导航名称', '显示名称', '子导航图片地址', '子导航链接地址', ];
    vZDHBColumnModel = [
          { name: 'iNAVIINX', width: 100 },
          { name: 'iNAVINAME', editor: 'text', width: 100 },
          { name: 'iNAVIVIEWNAME', editor: 'text', width: 100 },
          { name: 'iNAVIIMG', editor: 'text', width: 100 },
          { name: 'iNAVIURL', editor: 'text', width: 100 },
    ];
    vSHOWColumnNames = ['展示顺序号', '展示名称', '展示简介', '展示图片链接', '展示链接地址'];
    vSHOWColumnModel = [
          { name: 'iSHOWINX', width: 100, },
          { name: 'iSHOWNAME', editor: 'text', width: 100, },
          { name: 'sSHOWTITLE', editor: 'text', width: 100, },
          { name: 'iSHOWIMG', editor: 'text', width: 100, },
          { name: 'iSHOWURL', width: 100, editor: 'text', },


    ];
}
function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
    RefreshButtonSep();

}

function IsValidData() {
    if ($("#TB_DHMC").text() == "") {
        ShowMessage("请输入导航名称");
        return false;
    }
    if ($("#DDL_DHLX").val() == "") {
        ShowMessage("请选择导航类型");
        return false;
    }
    return true;
}

function selectValueChange(ruleId) {
}

 
function OnClickRow() {

    var row1 = $('#list1').datagrid('getSelected')

    if (row1 != null) {
        $("#TB_TURNINX").val(row1.iTURNINX);
        $("#TB_TURNURL").val(row1.iTURNURL);
        $("#TB_TURNIMG").val(row1.iTURNIMG);
}
    var row2 = $('#list2').datagrid('getSelected')

    if (row2 != null) {
        $("#TB_NAVIINX").val(row2.iNAVIINX);
        $("#TB_NAVINAME").val(row2.iNAVINAME);
        $("#TB_VIEWNAME").val(row2.iNAVIVIEWNAME);
        $("#TB_NAVIURL").val(row2.iNAVIURL);
        $("#TB_NAVIIMG").val(row2.iNAVIIMG);
    }

    var row3 = $('#list3').datagrid('getSelected')

    if (row3 != null) {
        $("#TB_SHOWINX").val(row3.iSHOWINX);
        $("#TB_SHOWNAME").val(row3.iSHOWNAME);
        $("#TB_SHOWIMG").val(row3.iSHOWIMG);
        $("#TB_SHOWURL").val(row3.iSHOWURL);
        $("#TB_SHOWTITLE").val(row3.sSHOWTITLE);
    }

}
$(document).ready(function () {
    BFButtonClick("TB_WXMDMC", function () {
        SelectWXMD("TB_WXMDMC", "HF_WXMDID", "zHF_WXMDID", false);
    });
    document.getElementById("B_Insert").style.display = "none";
    document.getElementById("B_Delete").style.display = "none";
    DrawGrid("list2", vZDHBColumnNames, vZDHBColumnModel);
    DrawGrid("list3", vSHOWColumnNames, vSHOWColumnModel);
    DrawGrid("list1", vLBTColumnNames, vLBTColumnModel);
    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;

        var str = GetSydhdy1(iDHID);
        var data = JSON.parse(str);
        var gzid = data.iID;
        if (gzid > 0) {
            ShowYesNoMessage("此导航在已启动的子导航定义单中被调用，如要修改将重新启动定义单，是否继续修改？", function () {
                {

                    SetControlBaseState();
                    document.getElementById("B_Delete").disabled = true;
                    document.getElementById("B_Update").disabled = true;
                }
            });

        }
        else {

            SetControlBaseState();
            document.getElementById("B_Delete").disabled = true;
            document.getElementById("B_Update").disabled = true;
        }

    }

    CheckBox("CB_TY", "DH_DHTY");//是否停用标志
    BFUploadClick("TB_TURNIMG", "HF_IMAGEURL", "FtpImg/LBT");



    $("#turn_Add").click(function () {
        //if ($("#TB_DHMC").text() == "") {
        //    ShowMessage("请输入导航名称");
        //    return false;
        //}

        var lx = document.getElementById("DDL_DHLX").value;
        if (lx == "") {
            ShowMessage("请选择导航类型");
            return false;
        }

        //if (selectId != -1 && !document.getElementById("turn_Update").disabled) {
        //    ShowMessage("正在修改轮播图数据，待修改后可添加新内容");
        //    return false;
        //}
        if ($("#TB_TURNINX").val() == "") {
            ShowMessage("请输入轮播图序号！");
            return false;
        }
        if (!IsNumber($("#TB_TURNINX").val())) {
            ShowMessage("序号不合法！");
            return false;
        }




        //if ($("#TB_TURNURL").val() == "") {
        //    //ShowMessage("请输入轮播图链接地址");
        //    //return false;
        //} else {
        //    if (!IsURL($("#TB_TURNURL").val())) {
        //        ShowMessage("链接URL填写不正确！");
        //        return
        //    }
        //}    
        var a = $("#TB_TURNINX").val();
        var rowData = $("#list1").datagrid("getData").rows;


        if (rowData.length >= 8) {
            ShowMessage("子导航最多为8条！");
            return;
        }
        if (rowData.length != 0) {
            for (i = 0; i < rowData.length  ; i++) {
                if (rowData[i].iTURNINX == a) {
                    ShowMessage("序号重复");
                    return false;
                }

            }
            $('#list1').datagrid('appendRow', {
                iTURNINX: $("#TB_TURNINX").val(),
                iTURNIMG: $("#TB_TURNIMG").val(),
                iTURNURL: $("#TB_TURNURL").val(),
            });

        }
        else {
            $('#list1').datagrid('appendRow', {
                iTURNINX: $("#TB_TURNINX").val(),
                iTURNIMG: $("#TB_TURNIMG").val(),
                iTURNURL: $("#TB_TURNURL").val(),
            });

        }
        var ElementNew = document.getElementById("ruleList1");
        ClearInputdata(ElementNew);
    });


    $("#turn_Del").click(function () {
        DeleteRows("list1");
        var ElementNew = document.getElementById("ruleList1");
        ClearInputdata(ElementNew);
        document.getElementById("turn_Add").disabled = false;
        selectId = -1;

    });
    BFUploadClick("TB_NAVIIMG", "HF_IMAGEURL", "FtpImg/ZDHT");

    $("#sub_Add").click(function () {
        if ($("#TB_DHMC").text() == "") {
            ShowMessage("请输入导航名称");
            return false;
        }

        var lx = document.getElementById("DDL_DHLX").value;
        if (lx == "") {
            ShowMessage("请选择导航类型");
            return false;
        }

        //if (selectId2 != -1 && !document.getElementById("sub_Update").disabled) {
        //    ShowMessage("正在修改子导航数据，待修改后可添加新内容");
        //    return false;
        //}
        //var rowidarr = $("#list2").getDataIDs();
        //if (rowidarr.length >= 8) {
        //    ShowMessage("子导航最多为8条！");
        //    return;
        //}
        if ($("#TB_NAVIINX").val() == "") {
            ShowMessage("请输入子导航序号！");
            return false;
        }
        if (!IsNumber($("#TB_NAVIINX").val())) {
            ShowMessage("序号不合法！");
            return false;
        }


        if ($("#TB_NAVINAME").val() == "") {
            ShowMessage("请输入子导航名称！");
            return false;
        }
        if ($("#TB_VIEWNAME").val() == "") {
            ShowMessage("请输入子显示名称！");
            return false;
        }
        var a = $("#TB_TURNINX").val();
        var rowData = $("#list2").datagrid("getData").rows;
        if (rowData.length != 0) {
            for (i = 0; i < rowData.length  ; i++) {
                if (rowData[i].iTURNINX == a) {
                    ShowMessage("序号重复");
                    return false;
                }

            }
            $('#list2').datagrid('appendRow', {
                iNAVIINX: $("#TB_NAVIINX").val(),
                iNAVINAME: $("#TB_NAVINAME").val(),
                iNAVIVIEWNAME: $("#TB_VIEWNAME").val(),
                iNAVIIMG: $("#TB_NAVIIMG").val(),
                iNAVIURL: $("#TB_NAVIURL").val(),
            });

        }
        else {
            $('#list2').datagrid('appendRow', {
                iNAVIINX: $("#TB_NAVIINX").val(),
                iNAVINAME: $("#TB_NAVINAME").val(),
                iNAVIVIEWNAME: $("#TB_VIEWNAME").val(),
                iNAVIIMG: $("#TB_NAVIIMG").val(),
                iNAVIURL: $("#TB_NAVIURL").val(),
            });

        }
        var ElementNew = document.getElementById("ruleList2");
        ClearInputdata(ElementNew);
    });

    $("#sub_Del").click(function () {


        DeleteRows("list2");




        var ElementNew = document.getElementById("ruleList2");
        ClearInputdata(ElementNew);
        document.getElementById("sub_Add").disabled = false;
        selectId2 = -1;

    });
    BFUploadClick("TB_SHOWIMG", "HF_IMAGEURL", "FtpImg/ZST");

    $("#show_Add").click(function () {
        if ($("#TB_DHMC").text() == "") {
            ShowMessage("请输入导航名称");
            return false;
        }

        var lx = document.getElementById("DDL_DHLX").value;
        if (lx == "") {
            ShowMessage("请选择导航类型");
            return false;
        }
        //if (selectId3 != -1 && !document.getElementById("show_Update").disabled) {
        //    ShowMessage("正在修改额外显示数据，待修改后可添加新内容");
        //    return false;
        //}


        if ($("#TB_SHOWINX").val() == "") {
            ShowMessage("请输入额外显示序号！");
            return false;
        }
        if (!IsNumber($("#TB_SHOWINX").val())) {
            ShowMessage("序号不合法！");
            return false;
        }

        if ($("#TB_SHOWNAME").val() == "") {
            ShowMessage("请输入额外展示名称！");
            return false;
        }

        var a = $("#TB_TURNINX").val();
        var rowData = $("#list3").datagrid("getData").rows;
        if (parseInt(GetSelectValue("DDL_DHLX")) == 0) {
            if (rowData.length > 2) {
                ShowMessage("额外展示最多为2条！");
                return;
            }
        }


        if (rowData.length != 0) {
            for (i = 0; i < rowData.length  ; i++) {
                if (rowData[i].iTURNINX == a) {
                    ShowMessage("序号重复");
                    return false;
                }

            }
            $('#list3').datagrid('appendRow', {
                iSHOWINX: $("#TB_SHOWINX").val(),
                iSHOWNAME: $("#TB_SHOWNAME").val(),
                sSHOWTITLE: $("#TB_SHOWTITLE").val(),
                iSHOWIMG: $("#TB_SHOWIMG").val(),
                iSHOWURL: $("#TB_SHOWURL").val(),
            });

        }
        else {
            $('#list3').datagrid('appendRow', {
                iSHOWINX: $("#TB_SHOWINX").val(),
                iSHOWNAME: $("#TB_SHOWNAME").val(),
                sSHOWTITLE: $("#TB_SHOWTITLE").val(),
                iSHOWIMG: $("#TB_SHOWIMG").val(),
                iSHOWURL: $("#TB_SHOWURL").val(),
            });

        }
        var ElementNew = document.getElementById("ruleList3");
        ClearInputdata(ElementNew);
    });

    $("#show_Del").click(function () {

        DeleteRows("list3");


        var ElementNew = document.getElementById("ruleList3");
        ClearInputdata(ElementNew);
        document.getElementById("show_Add").disabled = false;
        selectId3 = -1;

    });
});


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();//记录编号
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iNAME = $("#TB_DHMC").text();//导航名称
    Obj.iLX = GetSelectValue("DDL_DHLX");//导航类型
    Obj.iTY = $("[name='status']:checked").val();//停用标志
    //Obj.iMDID = $("#HF_WXMDID").val();




    var lst = new Array();
    var lst = $("#list1").datagrid("getRows");
    Obj.itemTable1 = lst;


    var lst1 = new Array();
    lst1 = $("#list2").datagrid("getRows");
    Obj.itemTable2 = lst1;

    var lst2 = new Array();
    lst2 = $("#list3").datagrid("getRows");
    Obj.itemTable3 = lst2;
  

    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    iDHID = Obj.iJLBH;
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_DHMC").text(Obj.iNAME);
    $("#DDL_DHLX").val(Obj.iLX);
    $("#HF_WXMDID").val(Obj.iMDID);
    $("#TB_WXMDMC").val(Obj.sMDMC);

    $("[name='status'][value='" + Obj.iTY + "']").prop("checked", true);

   

    $('#list2').datagrid('loadData', Obj.itemTable2, "json");
    $('#list2').datagrid("loaded");

    $('#list1').datagrid('loadData', Obj.itemTable1, "json");
    $('#list1').datagrid("loaded");

    $('#list3').datagrid('loadData', Obj.itemTable3, "json");
    $('#list3').datagrid("loaded");
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)



  
}

//复选框控制
function CheckBox(cbname, hfname) {
    $("input[type='checkbox'][name='" + cbname + "']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}



