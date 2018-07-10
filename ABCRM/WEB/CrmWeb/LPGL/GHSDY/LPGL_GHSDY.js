vUrl = "../LPGL.ashx";
vCaption = "供货商定义";

function InitGrid() {
    vColumnNames = ['供货商ID', '供货商名称', '主营项目', '地址', '电话', '停用标记', ];
    vColumnModel = [
            { name: 'iJLBH', width: 80, },
			{ name: 'sGHSMC', width: 80, },
			{ name: 'sZYXM', width: 120, },
            { name: 'sGHSDZ', width: 180, },
            { name: 'sDHHM', width: 120, },
            { name:'iBJ_TY',checkbox:true},     
            //{
            //    name: 'iBJ_TY', width: 120, formatter: function (cellvalue, options, rowObject) {
            //        return cellvalue == 1 ? "已停用" : "未停用";
            //    }
            //},
    ];
}

$(document).ready(function () {
    $("#HF_BJ_TY").val("0");
    $("#CB_BJ_TY").click(function () {
        $(this).prop("checked", this.checked);

        if ($(this).prop("checked")) {
            $("#HF_BJ_TY").val("1");
        }
        else {
            $("#HF_BJ_TY").val("0");
        }
    });

});

function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
}
function IsValidData() {
    if ($.trim($("#TB_GHSMC").val()) == "") {
        ShowMessage("请输入供货商名称", 3);
        return false;
    }
    return true;

}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sGHSMC = $("#TB_GHSMC").val();
    Obj.sDHHM = $("#TB_DHHM").val();
    Obj.sZYXM = $("#TB_ZYXM").val();
    Obj.sGHSDZ = $("#TB_GHSDZ").val();
    Obj.iBJ_TY = $("#HF_BJ_TY").val() == "" || $("#HF_BJ_TY").val() == "0" ? 0 : 1;
    return Obj;
}

function ShowData(data) {
    $("#TB_JLBH").val(data.iJLBH);
    $("#TB_GHSMC").val(data.sGHSMC);
    $("#TB_DHHM").val(data.sDHHM);
    $("#TB_ZYXM").val(data.sZYXM);
    $("#TB_GHSDZ").val(data.sGHSDZ);
    $("#HF_BJ_TY").val(data.iBJ_TY);
    $("#CB_BJ_TY").prop("checked", data.iBJ_TY == "0" ? false : true);
}
