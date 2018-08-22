vUrl = "../HYXF.ashx";
$(document).ready(function () {

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

    $("input[type='checkbox'][name='CB_STATUS']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked)
                .siblings()
                .prop("checked", !this.checked);
            $("#HF_STATUS").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_STATUS").val("");
        }
    });




});


function SetControlState() {

    $("#B_Exec").hide();
    //  $("#status-bar").hide();
    $("#ZXR").hide();
    $("#ZXRMC").hide();
    $("#ZXRQ").hide();


}

function IsValidData() {

    if ($("#TB_QZLXMC").val() == "") {
        art.dialog({ content: "圈子类型名称能为空", lock: true, time: 2 });
        return false;
    }
    if ($("#TB_QZCYRS").val() == "") {
        art.dialog({ content: "圈子成员人数不能为空", lock: true, time: 2 });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sQZLXMC = $("#TB_QZLXMC").val();
    Obj.iQZCYRS = $("#TB_QZCYRS").val();
    Obj.iSTATUS = $("[name='CB_STATUS']:checked").val()
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_QZLXMC").val(Obj.sQZLXMC);
    $("#TB_QZCYRS").val(Obj.iQZCYRS);
    $("[name='CB_STATUS']").each(function () {
        if ($(this).val() == Obj.iSTATUS) {
            $(this).prop("checked", true).siblings().prop("checked", false);
        }
    });
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
}
