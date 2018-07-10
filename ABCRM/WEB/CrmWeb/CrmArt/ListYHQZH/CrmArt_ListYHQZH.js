vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "优惠券账户";
vDialogName = "ListYHQZH";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);


function InitGrid() {
    vColumns = [
        { field: 'iYHQID', title: '优惠券ID', hidden: true },
        { field: 'iHYID', title: '会员ID', hidden: true },
        { field: 'iCXID', title: '促销ID', hidden: true },
        { field: 'iHYKTYPE', title: '卡类型', hidden: true },
        { field: 'sHYK_NO', title: '会员卡号', width: 100 },
        { field: 'sHY_NAME', title: '会员姓名', width: 100 },
        { field: 'sYHQMC', title: '优惠券名称', width: 100 },
        { field: 'sCXZT', title: '促销主题', width: 100 },
        { field: 'sMDFWMC', title: '门店范围名称', width: 100 },
        { field: 'sMDFWDM', title: '门店范围代码', hidden: true },
        { field: 'fJE', title: '优惠券余额', width: 100 },
        { field: 'dJSRQ', title: '结束日期', width: 100 },
    ];
    vIdField = "iYHQID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_KSKH", "sKSKH", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSKH", "sJSKH", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SL", "iSL", "<=", false);
    if (data) {
        for (var item in data) {
            if (item != 'vHF' && item != 'dJSRQ1' && item != 'dJSRQ2') {
                MakeSrchCondition2(arrayObj, data[item], item, "=", typeof (data[item]) == "string" ? true : false);
            }
            else if (item == 'dJSRQ1') {
                MakeSrchCondition2(arrayObj, data["dJSRQ1"], "dJSRQ", ">=", true);
            }
            else if (item == 'dJSRQ2') {
                MakeSrchCondition2(arrayObj, data["dJSRQ2"], "dJSRQ", "<=", true);
            }

        }
    }
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.iHF = data['vHF'];
    Obj.dialogName = vDialogName;
}