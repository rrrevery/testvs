vUrl = "../../LPGL/LPGL.ashx";
vCaption = "礼品信息";
var vDialogName = "ListXZLP";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
else {
    data = new Array();  //避免查询条件弹出框报错
    data['iHYID'] = 0;
    data['iPUBLICID'] = 0;
}
function InitGrid() {
    vColumns = [
        { field: 'iLPID', title: '礼品ID', hidden: true },
        { field: 'iFFJLBH', title: '发放记录编号', width: 100 },
        { field: 'sLPDM', title: '礼品代码', width: 100 },
        { field: 'sLPMC', title: '礼品名称', width: 100 },
        { field: 'iJC', title: '级次', hidden: true },
        { field: 'sJCMC', title: '级次名称', width: 100 },
        { field: 'iLPLX', title: '礼品类型', hidden: true },
        { field: 'sLXMC', title: '礼品类型', width: 100 },
        { field: 'iLPSL', title: '发放数量', hidden: true },
        { field: 'iLBID', title: 'ID', hidden: true },
    ];
    vIdField = "iLPID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LPDM", "sLPDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPMC", "sLPMC", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.iHYID = data['iHYID'];
    Obj.iPUBLICID = data['iPUBLICID'];
    Obj.dialogName = vDialogName;
}
