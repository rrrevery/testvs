vUrl = "../KFPT.ashx";
vCaption = "会员信用等级组定义";

function InitGrid() {
    vColumnNames = ['会员等级ID', '会员等级名称', '会员等级颜色', '分析周期', '退货次数', '同专柜消费次数', '同楼层消费品牌数量', '备注'];
    vColumnModel = [
            { name: 'iJLBH', },
			{ name: 'sXYDJMC', },
            { name: 'iXYDJYS', },
            { name: 'iFXZQ', },
            { name: 'iTHCS', },
            { name: 'iXFCS_TGZ', },
            { name: 'iPPS_TLC', },
            { name: 'sBZ', },
    ];
}

$(document).ready(function () {
    $("#JLBHCaption").html("会员等级ID");
});

function IsValidData() {
    if ($.trim($("#TB_XYDJMC").val()) == "") {
        ShowMessage("请输入信用等级组名称！");
        return false;
    }
    if (!IsNumber($("#TB_XYDJYS").val())) {
        ShowMessage("信用等级颜色只能输入数字！");
        return false;
    }
    if (!IsNumber($("#TB_FXZQ").val()) == "") {
        ShowMessage("分析周期只能输入数字！");
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sXYDJMC = $("#TB_XYDJMC").val();
    Obj.iXYDJYS = $("#TB_XYDJYS").val();
    Obj.iFXZQ = $("#TB_FXZQ").val();
    Obj.iTHCS = $("#TB_THCS").val();
    Obj.iXFCS_TGZ = $("#TB_XFCS_TGZ").val();
    Obj.iPPS_TLC = $("#TB_PPS_TLC").val();
    Obj.sBZ = $("#TB_BZ").val();
    return Obj;
}
function ShowData(Obj) {
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_XYDJMC").val(Obj.sXYDJMC);
    $("#TB_XYDJYS").val(Obj.iXYDJYS);
    $("#TB_FXZQ").val(Obj.iFXZQ);
    $("#TB_THCS").val(Obj.iTHCS);
    $("#TB_XFCS_TGZ").val(Obj.iXFCS_TGZ);
    $("#TB_PPS_TLC").val(Obj.iPPS_TLC);
    $("#TB_BZ").val(Obj.sBZ);

}

function IsNumber(testChar) {
    var BoolVailed = true;
    var regstring = "^[0-9]*$";
    var ipReg = new RegExp(regstring);
    if (ipReg.test(testChar) == false) {
        BoolVailed = false;
    }
    return BoolVailed;

}