vUrl = "../CRMGL.ashx";
var rowNumer = 0;
var pXQDM = "";
var pXQHS = "";
var pQYID = "";
var pQYMC = "";
var pCOPY = false;
var ID = "";
var MC = "";
$(document).ready(function () {
    $("#jlbh .dv_sub_left").html("小区编号");
    FillQYTree("TreeQY", "TB_QYMC");
    BFButtonClick("TB_SQMC", function () {
        SelectSQ("TB_SQMC", "HF_SQID", "zHF_SQID", false);
    });



})



function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
    if (vProcStatus == cPS_ADD || vProcStatus == cPS_MODIFY) {
        document.getElementById("CB_BJ_COPY").disabled = true;
    }
    else
        document.getElementById("CB_BJ_COPY").disabled = false;
}

function IsValidData() {
    if ($("#TB_XQMC").val() == "") {
        ShowMessage("请输入小区名称！");
        return;
    }
    if ($("#TB_QYMC").val() == "") {
        ShowMessage("请选择所属区域！");
        return;
    }
    if ($("#TB_SQMC").val() == "") {
        ShowMessage("请选择所属商圈！");
        return;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iXQHS = $("#TB_XQHS").val();
    Obj.sXQMC = $("#TB_XQMC").val();
    Obj.iQYID = $("#HF_QYID").val();
    Obj.sXQDM = $("#TB_XQDM").val();
    Obj.iBJ_TY = $("#CB_BJ_TY")[0].checked ? "1" : "0";

    Obj.sSQID = $("#HF_SQID").val();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_XQMC").val(Obj.sXQMC);
    $("#TB_XQHS").val(Obj.iXQHS);
    $("#CB_BJ_TY").val(Obj.iBJ_TY);
    $("#CB_BJ_TY").prop("checked", Obj.iBJ_TY == "1" ? true : false);
    $("#TB_QYMC").val(Obj.sQYMC);
    $("#HF_QYID").val(Obj.iQYID);
    $("#TB_XQDM").val(Obj.sXQDM);
    MC = "";
    ID = "";
    for (i = 0; i < Obj.itemTable.length ; i++) {
        MC += Obj.itemTable[i].sSQMC + ",";
        ID += Obj.itemTable[i].iSQID + ",";
    }
    $("#HF_SQID").val(ID);
    $("#TB_SQMC").val(MC);




};

function onClick(e, treeId, treeNode) {
    var check = (!treeNode.isParent);
    if (check) {
        $("#HF_QYID").val(treeNode.jlbh);
        var str = "";
        var treeObj = $.fn.zTree.getZTreeObj("TreeQY");
        while (treeNode != null && treeNode.pId != "") {
            str = treeNode.name + " " + str;
            treeNode = treeObj.getNodeByParam("id", treeNode.pId);
        }
        $("#TB_QYMC").val(str);

        hideMenu("menuContent");
    }
    else {
        art.dialog({ lock: true, content: "请选择最末级区域" });
        $("#TB_QYMC").val("");
        $("#HF_QYID").val("");
    }
}


function InsertClick() {
    if ($("#CB_BJ_COPY").prop("checked") == true) {
        pXQDM = $("#TB_XQDM").val();
        pXQHS = $("#TB_XQHS").val();
        pQYID = $("#HF_QYID").val();
        pQYMC = $("#TB_QYMC").val();
        pCOPY = true;
    }
    else {
        pXQDM = "";
        pXQHS = "";
        pQYID = "";
        pQYMC = "";
        pCOPY = false;
    }
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    $("#LB_DJRMC").text(sDJRMC);
    $("#HF_DJR").val(iDJR);
    SetControlBaseState();
    InsertClickCustom();
};
function InsertClickCustom() {
    if (pCOPY == true) {
        //$("#TB_XQMC").val(pXQMC);
        $("#TB_XQHS").val(pXQHS);
        $("#TB_QYMC").val(pQYMC);
        $("#HF_QYID").val(pQYID);
        $("#TB_XQDM").val(pXQDM);
        $("#CB_BJ_COPY").prop("checked", true);
    }
};

