vUrl = "../JKPT.ashx";

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    FillYJLX();
    FillZQLX();
    FillZBLX();
    //FillTYPE();
    //FillYJJB();
    $("#TB_HYSL").val(50);
});

function FillYJLX() {
    var obj = document.getElementById('DDL_YJLX');
    obj.options.add(new Option("", 0));
    obj.options.add(new Option("消费预警", 1));
    $("#DDL_YJLX option[value=1]").attr("selected", true);
    obj.options.add(new Option("收款台预警", 2));
    obj.options.add(new Option("同部门预警", 3));
    obj.options.add(new Option("返利预警", 4));
}
function FillZQLX() {
    var obj = document.getElementById('DDL_ZQLX');
    obj.options.add(new Option("", 0));
    obj.options.add(new Option("日", 1));
    $("#DDL_ZQLX option[value=1]").attr("selected", true);
    obj.options.add(new Option("月", 2));
}
function FillZBLX() {
    var obj = document.getElementById('DDL_ZBLX');
    // obj.options.add(new Option("", 0));
    obj.options.add(new Option("积分", 4));
    $("#DDL_ZBLX option[value=1]").attr("selected", true);
    obj.options.add(new Option("消费次数", 1));
}
function FillTYPE() {
    var obj = document.getElementById('DDL_TYPE');
    obj.options.add(new Option("", 0));
    obj.options.add(new Option("平日", 1));
    $("#DDL_TYPE option[value=1]").attr("selected", true);
    obj.options.add(new Option("节假日", 2));
}
function FillYJJB() {
    var obj = document.getElementById('DDL_YJJB');
    obj.options.add(new Option("", 0));
    obj.options.add(new Option("一级", 1));
    $("#DDL_YJJB option[value=1]").attr("selected", true);
    obj.options.add(new Option("二级", 2));
    obj.options.add(new Option("三级", 3));
}

function IsValidData() {

    if ($("#DDL_YJLX").val() == "") {
        art.dialog({ lock: true, content: "请选择预警类型", time: 2 });
        return false;
    }
    if ($("#DDL_ZQLX").val() == "") {
        art.dialog({ lock: true, content: "请选择周期类型", time: 2 });
        return false;
    }
    if ($("#DDL_ZBLX").val() == "") {
        art.dialog({ lock: true, content: "请选择指标类型", time: 2 });
        return false;
    }

    if ($("#DDL_TYPE").val() == "") {
        art.dialog({ lock: true, content: "请选择日类型", time: 2 });
        return false;
    }
    if ($("#DDL_YJJB").val() == "") {
        art.dialog({ lock: true, content: "请选择预警级别", time: 2 });
        return false;
    }
    if ($("#TB_ZBS").val() == "") {
        art.dialog({ lock: true, content: "请输入起始指数", time: 2 });
        return false;
    }
    if ($("#TB_ZBS2").val() == "") {
        art.dialog({ lock: true, content: "请输入结束指数", time: 2 });
        return false;
    }
    if (parseFloat($("#TB_ZBS").val()) > parseFloat($("#TB_ZBS2").val())) {
        art.dialog({ lock: true, content: "起始指数不能大于结束指数", time: 2 });
        return false;
    }
    if ($("#TB_HYSL").val() == "") {
        art.dialog({ lock: true, content: "请输入最大预警会员", time: 2 });
        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iYJLX = GetSelectValue("DDL_YJLX");
    Obj.iZQLX = GetSelectValue("DDL_ZQLX");
    Obj.iZBLX = GetSelectValue("DDL_ZBLX");
    Obj.fZBS = $("#TB_ZBS").val();
    Obj.iBJ_TY = $("#CB_BJ_TY")[0].checked ? "1" : "0";
    Obj.iHYSL = $("#TB_HYSL").val();
    Obj.iYJJB = GetSelectValue("DDL_YJJB");
    Obj.iTYPE = GetSelectValue("DDL_TYPE");
    Obj.fZBS2 = $("#TB_ZBS2").val();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#DDL_YJLX").val(Obj.iYJLX);
    $("#DDL_ZQLX").val(Obj.iZQLX);
    $("#DDL_ZBLX").val(Obj.iZBLX);
    $("#TB_ZBS").val(Obj.fZBS);

    $("#CB_BJ_TY").val(Obj.iBJ_TY);
    $("#CB_BJ_TY").prop("checked", Obj.iBJ_TY == "1" ? true : false);

    $("#TB_HYSL").val(Obj.iHYSL);
    $("#DDL_YJJB").val(Obj.iYJJB);
    $("#DDL_TYPE").val(Obj.iTYPE);
    $("#TB_ZBS2").val(Obj.fZBS2);
}
