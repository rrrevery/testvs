vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "面值卡信息";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);



var vDialogName = $.dialog.data("vDialogName");

function InitGrid() {
    vColumns = [
        { field: 'sHYK_NO', title: '卡号', width: 100 },
        { field: 'iHYID', title: '会员ID', width: 100, hidden: true },
        { field: 'iHYKTYPE', title: 'iHYKTYPE', hidden: true },
        { field: 'sHYKNAME', title: '卡类型', width: 100 },
        { field: 'iSTATUS', title: 'iSTATUS', hidden: true },
        { field: 'sStatusName', title: '状态', width: 100 },
        { field: 'fYE', title: '余额', width: 100 },
        { field: 'fQCYE', title: '期初余额', width: 100 },
        { field: 'dYXQ', title: '有效期', width: 100 },
    ];
    vIdField = "iHYID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_KSKH", "sKSKH", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSKH", "sJSKH", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SL", "iSL", "<=", false);
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