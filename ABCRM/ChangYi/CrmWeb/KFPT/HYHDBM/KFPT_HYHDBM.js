vUrl = "../KFPT.ashx";

function GetHYXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetGRXXData(0, $("#TB_HYKNO").val());
        $("#HF_HYID").val(0);
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            ClearData();
            return;
        }
        if (str.indexOf("错误") >= 0) {
            art.dialog({ lock: true, content: str });
            return;
        }

        var Obj = JSON.parse(str);
        $("#HF_HYID").val(Obj.iHYID);
        $("#TB_GKNAME").val(Obj.sHY_NAME);
        $("#TB_LXDH").val(Obj.sSJHM);
        $("#TB_ZJHM").val(Obj.sSFZBH);
    }
}

function Change() {
    var value = GetSelectValue("DDL_HDID");
    if (value != 0) {
        var bmzrs = GetBMRS(value); //获取报名总人数
        $("#LB_BMZRS").text(bmzrs);
    }
}


$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Update").hide();
    $("#status-bar").hide();
    FillHD($("#DDL_HDID"), 1);// 活动下拉菜单，可选状态，0已保存，1已审核，-1已终止，不选的话是所有活动
    $("#LB_BMDJRMC").text(sDJRMC);


    $("#TB_HYKNO").bind('keypress', function (event) {        //通过会员卡号获得HYID         
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });
    Change();
    RefreshButtonSep();
});

function IsValidData() {

    if (GetSelectValue("DDL_HDID") == "") {
        ShowMessage("请选择活动名称");
        return false;
    }
    //会员的情况下通过输入会员卡号获取姓名、联系电话和证件号码；非会员的情况下输入姓名、联系电话和证件号码
    //所以会员的情况下 会员卡号不能为空。 1为会员，2为非会员
    var vBMSF = $("[name='bmsf']:checked").val();
    if (vBMSF == 1) {
        if ($("#TB_HYKNO").val() == "") {
            ShowMessage("请输入会员卡号");
            return false;
        }
    }
    else {
        if ($("#TB_GKNAME").val() == "") {
            ShowMessage("请输入顾客姓名");
            return false;
        }
        if ($("#TB_LXDH").val() == "") {
            ShowMessage("请输入联系电话");
            return false;
        }
        if ($("#TB_ZJHM").val() == "") {
            ShowMessage("请输入证件号码");
            return false;
        }
    }
    if ($("#TB_BMRS").val() == "") {
        ShowMessage("请输入报名人数");
        return false;
    }
    return true;
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHDID = GetSelectValue("DDL_HDID");
    //非会员情况下，HYID为空，姓名 联系电话 证件号码分别对应这些字段gkname，lxdh,zjhm，
    var vBMSF = $("[name='bmsf']:checked").val();
    if (vBMSF == 1) {
        Obj.iHYID = $("#HF_HYID").val();
    }
    Obj.sZJHM = $("#TB_ZJHM").val();
    Obj.sLXDH = $("#TB_LXDH").val();
    Obj.sGKNAME = $("#TB_GKNAME").val();
    Obj.sBZ = $("#TB_BZ").val();
    Obj.iBMRS = $("#TB_BMRS").val();
    Obj.iBJ_BMFS = $("[name='bj_bmfs']:checked").val();

    Obj.iBMDJR = iDJR;
    Obj.sBMDJRMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    if (Obj.iZZR != 0) {
        $("#TB_HDMC").val(Obj.sHDMC);
        $("#HD2").css("display", "block");
        $("#HD").css("display", "none");
    }
    else {
        window.setTimeout(function () {
            $("#DDL_HDID").val(Obj.iHDID);
        }, 100);
        $("#HD2").css("display", "none");
        $("#HD").css("display", "block");

    }
    //1会员卡号不为空则为会员，2会员卡号为空则为非会员
    if (Obj.sHYK_NO != "") {
        $("[name='bmsf'][value='" + 1 + "']").prop("checked", true);
    }
    else {
        $("[name='bmsf'][value='" + 2 + "']").prop("checked", true);
    }
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_HYKNO").val(Obj.sHYK_NO);
    $("#TB_GKNAME").val(Obj.sGKNAME);
    $("#TB_LXDH").val(Obj.sLXDH);
    $("#TB_ZJHM").val(Obj.sZJHM);
    $("#TB_BZ").val(Obj.sBZ);
    $("#TB_BMRS").val(Obj.iBMRS);
    $("#HF_BMDJR").val(Obj.iBMDJR);
    $("#LB_BMDJRMC").text(Obj.sBMDJRMC);
    $("#HF_CJDJR").val(Obj.iCJDJR);
    $("#LB_CJDJRMC").text(Obj.sCJDJRMC);
    $("#LB_BMSJ").text(Obj.dBMSJ);
    $("#LB_CJSJ").text(Obj.dCJSJ);
    $("[name='bj_bmfs'][value='" + Obj.iBJ_BMFS + "']").prop("checked", true);

    var bmzrs = GetBMRS(Obj.iHDID); //获取报名人数
    $("#LB_BMZRS").text(bmzrs);
}


function ClearData() {
    $("#TB_HYKNO").val("");
    $("#TB_GKNAME").val("");
    $("#TB_LXDH").val("");
    $("#TB_ZJHM").val("");
}

