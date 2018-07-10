vUrl = "../YHQGL.ashx";
var bj_sf = GetUrlParam("bj_sf");

function InitGrid() {
    vColumnNames = ['销售金额', "赠送积分"];
    vColumnModel = [
          { name: "fXSJE", width: 150, editable: true, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
          { name: "fFQJE", width: 150, editable: true, editor: 'text', editrules: { required: true, number: true, minValue: 1 } }
    ];
};

$(document).ready(function () {



    $("#CB_BJ_TY").click(function (event) {
        if (event.target == this) {
            if (this.checked) {
                $("#HF_BJ_TY").val(1);
            }
            else {
                $("#HF_BJ_TY").val(0);
            }
        }
        $(this).prop("checked", this.checked);
    });
});

function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
}

function IsValidData() {

    if ($("#TB_FFGZMC").val() == "") {
        art.dialog({ lock: true, content: "请输入规则名称" });
        return false;
    }
    if ($("#TB_FFXE").val() == "") {
        art.dialog({ lock: true, content: "请输入发放限额" });
        return false;
    }
    if ($("#TB_FFQDJE").val() == "") {
        art.dialog({ lock: true, content: "请输入起点金额" });
        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //主表数据
    Obj.fFFXE = $("#TB_FFXE").val();
    Obj.fFFQDJE = $("#TB_FFQDJE").val();
    Obj.sYHQFFGZMC = $("#TB_FFGZMC").val();
    Obj.iBJ_TY = $("#HF_BJ_TY").val();
    Obj.iBJ_SF = bj_sf;
    //子表数据
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_FFGZID").val(Obj.iJLBH);
    //主表数据
    $("#TB_FFXE").val(Obj.fFFXE);
    $("#TB_FFQDJE").val(Obj.fFFQDJE);
    $("#TB_FFGZMC").val(Obj.sYHQFFGZMC);
    $("#HF_BJ_TY").val(Obj.iBJ_TY);
    $("#TB_BJ_TY").prop("checked", Obj.iBJ_TY == "1" ? true : false);
    //bj_sf = Obj.iBJ_SF;
    //子表数据绑定
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

}

//jQgird初始化
