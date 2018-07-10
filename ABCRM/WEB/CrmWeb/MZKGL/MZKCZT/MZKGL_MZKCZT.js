
//var hyid = GetUrlParam("hyid");

$(document).ready(function () {
    AddToolButtons("刷卡", "B_SK");
    //AddToolButtons("查卡", "B_CK");
    AddToolButtons("刷新", "B_SX");

    $("#B_SK").click(function () {
        var conData = new Object();
        conData.iBJ_MZK = 1;
        SelectSK("TB_HYK_NO", "HF_HYID", "", conData)
    });

    $("#TB_HYK_NO").keydown(function (event) {
        event = (event) ? event : ((window.event) ? window.event : ""); //兼容IE和Firefox获得keyBoardEvent对象
        var key = event.keyCode ? event.keyCode : event.which; //兼容IE和Firefox获得keyBoardEvent对象的键值 
        if (key == 13) {
            if ($("#TB_HYK_NO").val() != "") {
                ShowData($(this).val());
            }
            else {
                ShowMessage("请输入卡号！", 3);
            }
        }
    });
    //查询卡
    $("#B_CK").click(function () {
        $.dialog.open("../../WUC/MZKGL/WUC_SelectMZK.aspx", {
            lock: true, width: 600, height: 600,
            close: function () {
                var hykno = $.dialog.data("passValue");
                if (hykno != "" && hykno != undefined) {
                    var hydata = GetHYXXData(0, hykno, "CRMDBMZK");
                    ShowData(hydata);
                    $.dialog.data("passValue", "");
                }
            }
        });
    });

    //刷新
    $("#B_SX").click(function () {
        var hykno = $("#TB_HYK_NO").val();
        if (hykno != "" && hykno != undefined) {
            ShowData(hykno);
        }
    });
})

function ShowData(hykno) {
    var data = GetMZKXXData(0, hykno, "", "CRMDBMZK");
    var Obj = JSON.parse(data);
    if (Obj.sHYK_NO == "") {
        ShowMessage("面值卡卡号不存在,请检查是否输入有误！", 3);
        return;
    }
    pHYKNO = Obj.sHYK_NO;
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_HYK_NO").val(Obj.sHYK_NO);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_YXQ").text(Obj.dYXQ);
    $("#LB_JKRQ").text(Obj.dJKRQ);
    $("#HF_STATUS").val(Obj.iSTATUS);
    $("#LB_STATUS").text(Obj.sStatusName);
    $("#HF_MDID").val(Obj.iMDID);
    $("#LB_MDMC").text(Obj.sMDMC);

    $("#LB_QCYE").text(Obj.fQCYE);
    $("#LB_YE").text(Obj.fCZJE);
    $("#LB_PDJE").text(Obj.fPDJE);
    $("#LB_DJJE").text(Obj.fJYDJJE);
}

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    if (dialogName == "DialogSK") {
        pHYKNO = $("#TB_HYK_NO").val();
        ShowData(pHYKNO);
    }
}