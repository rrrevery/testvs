vUrl = "../YHQGL.ashx";
vCaption = "优惠券面值定义";
$(document).ready(function () {
    $("#B_Exec").hide();
    $("#TB_JE").inputmask({ mask: "9{*}[.]9{*}" });
    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
    });
    $("input[name='CB_BJ_TY']").on("click", function () {
        var ele = $(this).prop("checked", this.checked);
        if (this.checked) {//选中
            ele.siblings().prop("checked", false);
            $("#HF_BJ_TY").val(ele.val());
            return;
        }
        $("#HF_BJ_TY").val("");
    });

});



function SetControlState() {

}

function IsValidData() {
    if ($("#HF_YHQID").val() == "") {
        art.dialog({ content: '请选择优惠券', lock: true, time: 2 });
        return false;
    }
    if ($("#TB_NAME").val() == "") {
        art.dialog({ content: '请输入名称', lock: true, time: 2 });
        return false;
    }
    if ($("#TB_JE").val() == "") {
        art.dialog({ content: '请输入金额', lock: true, time: 2 });
        return false;
    }
    //if ($("#HF_BJ_TY").val() == "") {
    //    art.dialog({ content: '请选择优惠券', lock: true, time: 2 });
    //    return false;
    //}
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //主表数据
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.sNAME = $("#TB_NAME").val();
    Obj.dJE = $("#TB_JE").val();
    Obj.iBJ_TY = $("#HF_BJ_TY").val() == "" ? 0 : $("#HF_BJ_TY").val();

    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_NAME").val(Obj.sNAME);
    $("#TB_JE").val(Obj.dJE);
    $("#HF_BJ_TY").val(Obj.iBJ_TY);

    $("input[name='CB_BJ_TY']").each(function () {
        if ($(this).val() == Obj.iBJ_TY) {
            $(this).prop("checked", "true");
            return;
        }
        $(this).prop("checked", false);
    });
}