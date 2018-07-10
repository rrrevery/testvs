vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "会员卡信息";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);

var vDialogName = $.dialog.data("vDialogName");
var zylxarr = new Array();

function InitGrid() {
    vColumns = [
        { field: 'iHYID', title: '会员ID', width: 100 },
        { field: 'sHYK_NO', title: '卡号', width: 100 },
        { field: 'sHY_NAME', title: '姓名', width: 100 },
        { field: 'iHYKTYPE', title: 'iHYKTYPE', hidden: true },
        { field: 'sHYKNAME', title: '卡类型', width: 100 },
        { field: 'iSTATUS', title: 'iSTATUS', hidden: true },
        { field: 'sStatusName', title: '状态', width: 100 },
       // { field: 'iBJ_CHILD', title: 'iBJ_CHILD', hidden: true },
        { field: 'dYXQ', title: '有效期', width: 100 },
        { field: 'iSEX', title: '性别', hidden: true },
        { field: 'sSJHM', title: '手机号码', hidden: true },
        { field: 'sOPENID', title: 'OPENID', hidden: true },
    ];
    vIdField = "iHYID";
}

$(document).ready(function () {
    FillSelect("DDL_ZY", GetHYXXXM(1));

    $("input[type='checkbox'][name='CB_SEX']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#HF_SEX").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_SEX").val("");
        }
        if ($.trim($(this).val()) == "") {
            $("#HF_SEX").val("");
        }
    });

    $("input[type='checkbox'][name='CB_SEX']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#HF_SEX").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_SEX").val("");
        }
        if ($.trim($(this).val()) == "") {
            $("#HF_SEX").val("");
        }
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_KSKH", "sKSKH", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSKH", "sJSKH", "<=", true);
    MakeSrchCondition(arrayObj, "HF_SEX", "iSEX", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "DDL_ZY", "iZYID", "=", false);



    for (var item in data) {
        if (data[item] && item != 'vHF' && item != 'vZT') {
            MakeSrchCondition2(arrayObj, data[item], item, "=", typeof (data[item]) == "string" ? true : false);
        }
    }
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.dialogName = vDialogName;
    if (data['vZT'] != undefined)
        Obj.iZT = data['vZT'];
    else
        Obj.iZT = 0;
}