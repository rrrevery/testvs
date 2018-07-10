vUrl = "../MZKGL.ashx";
var vRowID = 0;
var selRowID;
var iMDID;
var iHYKTYPE;
var fQDJE;

var vCaption = "面值卡赠积分规则";
function InitGrid() {
    vColumnNames = ['起点金额', '终点金额', '赠送积分', '赠送比例'];
    vColumnModel = [
            { name: 'fQDJE', width: 130 },
			{ name: 'fZDJE', width: 130 },
            { name: 'fYXQTS', width: 130, },
            { name: 'fZSBL', width: 130 },
    ];
}


$(document).ready(function () {


});

function IsValidData() {

    if (($("#TB_ZSBL").val() == "" && $("#TB_YXQTS").val() == "") ) {
        ShowMessage("赠送比例/积分不能同时为空");
        return false;
    }
    if (($("#TB_ZSBL").val() != "" && $("#TB_YXQTS").val() != "") || ($("#TB_ZSBL").val() != 0 && $("#TB_YXQTS").val() != 0)) {
        ShowMessage( "赠送比例/积分只能输入一项");
        return false;
    }

    if ($("#TB_QDJE").val() == "" || $("#TB_ZDJE").val() == "") {
        ShowMessage("销售金额不能为空");
        return false;
    }
    //var rowList = $("#list").datagrid("getData").rows;
    //for (i = 0; i < rowList.length ; i++) {
    //    var rowData = rowList[i];
    //    if (rowData.fQDJE == $("#TB_QDJE").val() ) {
    //        ShowMessage("新增销售金额已经存在，不能添加");
    //        return false;
    //    }
    //    if (rowData.fZDJE == $("#TB_ZDJE").val()) {
    //        ShowMessage("新增销售金额已经存在，不能添加");
    //        return false;
    //    }
    //    if ($("#TB_QDJE").val() >= rowData.fQDJE && $("#TB_QDJE").val() <= rowData.fZDJE) {
    //        ShowMessage("新增起点金额已在其他规则范围内，不能添加");
    //        return false;
    //    }
    //    if ($("#TB_ZDJE").val() >= rowData.fQDJE && $("#TB_ZDJE").val() <= rowData.fZDJE)
    //    {
    //        ShowMessage("新增终点金额已在其他规则范围内，不能添加");
    //        return false;
    //    }
    //    if ($("#TB_ZDJE").val() >= rowData.fZDJE && $("#TB_QDJE").val() <= rowData.fQDJE) {
    //        ShowMessage("新增金额范围内有其他规则，不能添加");
    //        return false;
    //    }

    //}
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.fQDJE = $("#TB_QDJE").val();
    Obj.fZDJE = $("#TB_ZDJE").val();
    Obj.fZSBL = $("#TB_ZSBL").val() == "" ? 0 : $("#TB_ZSBL").val();
    Obj.fYXQTS = $("#TB_YXQTS").val() == "" ? 0 : $("#TB_YXQTS").val();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.sYHQMC = $("#TB_YHQMC").val();

    Obj.sDBConnName = "CRMDBMZK";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    if (vProcStatus == cPS_ADD) {
        fQDJE = $("#TB_QDJE").val();
    }
    if (vProcStatus != cPS_ADD) {
        Obj.fOLDQDJE = $("#HF_QDJE").val();
    }
    return Obj;
}

function ShowData(data) {

    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_QDJE").val(data.fQDJE);
    $("#HF_QDJE").val(data.fQDJE);
    $("#TB_ZDJE").val(data.fZDJE);
    $("#HF_ZDJE").val(data.fZDJE);
    $("#TB_ZSBL").val(data.fZSBL);
    $("#TB_YXQTS").val(data.fYXQTS);
    $("#HF_YHQID").val(data.iYHQID);
    $("#TB_YHQMC").val(data.sYHQMC);

}

function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").show();
    $("#B_Exec").hide();
    $("#status-bar").hide();
}