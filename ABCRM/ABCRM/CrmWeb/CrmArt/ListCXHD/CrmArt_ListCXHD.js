
vUrl = "../../YHQGL/YHQGL.ashx";
vCaption = "促销活动信息";
var vDialogName = "ListCXHD";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
function InitGrid() {
    vColumns = [
        { field: 'iCXID', title: '促销ID', hidden: true },
        { field: 'sCXZT', title: '促销主题', width: 100 },
        { field: 'dKSSJ', title: '开始日期', width: 100 },
        { field: 'dJSSJ', title: '结束日期', width: 100 },
    ];
    vIdField = "iCXID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_CXID", "iCXID", "=", true);
    MakeSrchCondition(arrayObj, "TB_CXZT", "sCXZT", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    if (data.iYHQID) {
        Obj.iYHQID = data.iYHQID;
    }
}