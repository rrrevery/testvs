vUrl = "../../LPGL/LPGL.ashx";
vCaption = "礼品信息";
var vDialogName = "ListXZJFDHLP";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
else {
    data = new Array();  //避免查询条件弹出框报错
    data['iHYID'] = 0;
    data['iCLLX'] = 0;
}
function InitGrid() {
    vColumns = [    
        { field: 'iLPID', title: '礼品ID', hidden: true },
        { field: 'iFFJLBH', title: '发放记录编号', width: 100 },
        { field: 'sLPDM', title: '礼品代码', width: 100 },
        { field: 'sLPMC', title: '礼品名称', width: 100 },
        { field: 'fLPJF', title: '兑换积分', width: 100 },
        { field: 'iLPSL', title: '发放数量', width: 100 },
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
    Obj.iCLLX = data['iCLLX'];
    Obj.dialogName = vDialogName;
}
