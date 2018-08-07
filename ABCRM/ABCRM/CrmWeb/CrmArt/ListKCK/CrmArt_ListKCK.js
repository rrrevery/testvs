vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "库存卡信息";
var data = $.dialog.data("DialogCondition");
if (data)
{
    data = JSON.parse(data);
}
var vDialogName = $.dialog.data("vDialogName");


function InitGrid() {
    vColumns = [
        { field: 'sCZKHM', title: '卡号', width: 100 },
        { field: 'iHYKTYPE', title: 'iHYKTYPE', hidden: true },
        { field: 'sHYKNAME', title: '卡类型', width: 100 },
        { field: 'fQCYE', title: '面值金额', width: 100 },
        { field: 'dYXQ', title: '有效期', width: 100 },
    ];
    vIdField = "sCZKHM";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_KSKH", "sKSKH", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSKH", "sJSKH", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SL", "iSL", "<=", false);
    for (var item in data) {
        if (item != 'vHF' && item != 'vCZK') {
            MakeSrchCondition2(arrayObj, data[item], item, "=", typeof (data[item]) == "string" ? true : false);
        }

    }
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.iHF = data['vHF'];
    if (data['vCZK'] == "1")
        Obj.iBJ_CZK = 1;
    Obj.dialogName = vDialogName;
}