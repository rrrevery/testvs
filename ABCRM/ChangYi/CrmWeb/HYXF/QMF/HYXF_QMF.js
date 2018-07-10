vUrl = "../HYXF.ashx";
var fJF_J = 0;

$(document).ready(function () {

    //FillMD($("#S_MDMC"));
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    $("#TB_HYK_NO").change(function () {
        GetHYKXX();
    });
    //$("#S_MDMC").change(function () {
    //    $("#HF_MDID").val($(this).val());
    //})
    $("#TB_THJF").change(function () {
        //根据退货积分计算 应买积分，以及根据其他数据计算实买积分  金额设置 
        if ($("#LB_YWCLJF").text() == "") {
            art.dialog({ content: '请输入有效卡号', lock: true, time: 2 });
            return;
        }
        //根据卡类型查询买积分规则
        GetQMFGZ($("#HF_HYKTYPE").val());
    });

    $("#TB_SFJE_J").change(function () {
        if ($("#TB_THJF").val() == "" || fJF_J == 0) {
            return;
        }
        //if ($(this).val() > $("#LB_YFJE_J").text()) {
        //    art.dialog({ content: '金额过大', time: 2, lock: true });
        //    $(this).val("0");
        //}
        $("#LB_SMJJF").text($(this).val() * fJF_J);
        $("#TB_XJ").val($(this).val());
    });

    $("#TB_SFJE_J").blur(function () {
        if ($("#TB_THJF").val() == "" || fJF_J == 0) {
            return;
        }
        //if ($(this).val() > $("#LB_YFJE_J").text()) {
        //    art.dialog({ content: '金额过大', time: 2, lock: true });
        //    $(this).val("0");
        //}
        $("#LB_SMJJF").text($(this).val() * fJF_J);
        $("#TB_XJ").val($(this).val());
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false)
    })


});
function GetHYKXX() {
    //根据卡号查询卡信息
    if ($("#TB_HYK_NO").val() == "") {
        return;
    }
    var str = GetHYXXData(0, $("#TB_HYK_NO").val());
    $("#HF_HYID").val(0);
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡号", 3);
        return;
    }
    if (str.indexOf("错误") >= 0) {
        ShowMessage(str, 3);
        return;
    }

    var Obj = JSON.parse(str);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_YWCLJF").text(Obj.fWCLJF);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_YNLJF").val(Obj.fBNLJJF);
    $("#LB_YYHQJE").text(Obj.fYHQJE);
    $("#LB_YCZKYE").text(Obj.fCZJE);

    //$.ajax({
    //    type: 'post',
    //    url: vUrl + "?mode=View&func=" + vPageMsgID,
    //    dataType: "json",
    //    data: { json: JSON.stringify(data) },
    //    success: function (data) {
    //        if (data != null && data.iHYID != null && data.iHYID != "0") {
    //            $("#HF_HYID").val(data.iHYID);
    //           
    //            $("#LB_YWCLJF").text(data.fYWCLJF == null ? 0 : data.fYWCLJF);
    //            $("#LB_YNLJF").text(data.fYNLJJF == null ? 0 : data.fYNLJJF);
    //            $("#LB_YYHQJE").text(data.fYYHQJE == null ? 0 : data.fYYHQJE);
    //            $("#LB_YCZKYE").text(data.fYCZKYE == null ? 0 : data.fYCZKYE);
    //            $("#HF_HYKTYPE").val(data.iHYKTYPE);
    //        }
    //        else {
    //            art.dialog({ content: '此卡号无效,请重新输入', time: 2, lock: true });
    //        }
    //    },
    //    error: function (data) {

    //    }
    //});


}
//function SetControlState() {

//}
function GetQMFGZ(hyktype) {
    var obj = new Object();
    obj.iHYKTYPE = hyktype;
    $.ajax(
        {
            url: "../../CrmLib/CrmLib.ashx?func=GetQMFGZ",
            type: 'post',
            dataType: "json",
            data: "json=" + JSON.stringify(obj),
            success: function (data) {
                if (data != null && data.iHYKTYPE == null) {
                    art.dialog({ content: '无此卡类型买积分规则', time: 2, lock: true });
                    return;
                }
                $("#LB_YMJJF").text(parseFloat($("#TB_THJF").val() - $("#LB_YWCLJF").text()));
                $("#LB_YFJE_J").text(parseFloat(($("#LB_YMJJF").text() / data.fJF_J)).toFixed(2));
                fJF_J = data.fJF_J;

            },
            error: function (data) {

            }
        });


}
function IsValidData() {
    if ($("#HF_BGDDDM").val() == "") {
        art.dialog({ content: '请选择操作地点!', time: 2, lock: true });
        return;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();//只有当是修改时，才应该获取
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.iHYID = $("#HF_HYID").val();
    //Obj.iMDID = GetSelectValue("S_MDMC");
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    //Obj.iLQR = $("#HB_HYK_TYPE").text();

    Obj.fYWCLJF = $("#LB_YWCLJF").text();
    Obj.fYNLJJF = $("#LB_YNLJF").text();
    Obj.fYYHQJE = $("#LB_YYHQJE").text();
    Obj.fYCZKYE = $("#LB_YCZKYE").text();

    Obj.fTHJF = $("#TB_THJF").val();
    Obj.fYMJJF = $("#LB_YMJJF").text();
    Obj.fYFJE_J = $("#LB_YFJE_J").text();
    Obj.fSMJJF = $("#LB_SMJJF").text();
    Obj.fSFJE_J = $("#TB_SFJE_J").val();

    Obj.fXJ = $("#TB_XJ").val();
    Obj.sZY = "";

    //Obj.kditemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    //var itemtable=new Array();


    //Obj.itemTable

    return Obj;
}


function ShowData(data) {
    FillMD($("#S_MDMC"));
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_HYK_NO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    //$("#HF_MDID").val(Obj.iMDID);
    $("#TB_CCDDMC").val(Obj.sBGDDMC);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);

    //$("#S_MDMC").val(Obj.iMDID);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);


    $("#LB_YWCLJF").text(Obj.fYWCLJF);
    $("#LB_YNLJF").text(Obj.fYNLJJF);
    $("#LB_YYHQJE").text(Obj.fYYHQJE);
    $("#LB_YCZKYE").text(Obj.fYCZKYE);

    $("#TB_THJF").val(Obj.fTHJF);

    $("#LB_YMJJF").text(Obj.fYMJJF);
    $("#LB_YFJE_J").text(Obj.fYFJE_J);
    $("#LB_SMJJF").text(Obj.fSMJJF);
    $("#TB_SFJE_J").val(Obj.fSFJE_J);
    $("#TB_XJ").val(Obj.fXJ);

    //登记人
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    //审核人
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

}

function onClick(e, treeId, treeNode) {

    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

var clearNoNum = function (obj) {
    obj.value = obj.value.replace(/[^\d.]/g, "");
    obj.value = obj.value.replace(/^\./g, "");
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
    obj.value = obj.value.replace(/^(\-)*(\d+)\.(\d).*$/, '$1$2.$3');
};

function GetHYXX() {
    if ($("#TB_HYK_NO").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKNO").val(), vDBName);
        $("#HF_HYID").val(0);
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            return;
        }
        if (str.indexOf("错误") >= 0) {
            ShowMessage(str, 3);
            return;
        }

        var Obj = JSON.parse(str);
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYNAME").text(Obj.sHY_NAME);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#HF_FXDWDM").val(Obj.sFXDWDM);
        $("#LB_FXDWMC").text(Obj.sFXDWMC);
        $("#HF_BJ_CHILD").val(Obj.iBJ_CHILD);



        $("#LB_JF").text(Obj.fWCLJF);
        $("#LB_JE").text(Obj.fCZJE);
        if (vHF == 0) {
            $("#LB_YZT").text("正常有效");
            $("#LB_XZT").text("挂失");

        }
        else {
            $("#LB_YZT").text("挂失");
            $("#LB_XZT").text("正常有效");
        }
    }
}