vUrl = "../../LPGL/LPGL.ashx";
vCaption = "礼品信息";
var vDialogName = "ListLP";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
else {
    data=new Array();  //避免查询条件弹出框报错
    data['iDJLX'] = 0;   //是否要显示库存数量 0不显示;1显示
}

function InitGrid() {
    vColumns = [
        { field: 'iLPID', title: '礼品ID', hidden: true },
        { field: 'sLPDM', title: '礼品代码', width: 100 },
        { field: 'sLPMC', title: '礼品名称', width: 100 },
        { field: 'sGJBM', title: '国际编码', width: 100 },
        { field: 'sLPGG', title: '礼品规格', width: 100 },
        { field: 'fLPDJ', title: '礼品单价', width: 100 },
        { field: 'fLPJF', title: '礼品积分', width: 100 },
        { field: 'fKCSL', title: '库存数量', hidden: data['iDJLX'] == 0 ? true : false },
        { field: 'iBJ_WKC', title: '不限制库存', formatter: BoolCellFormat },
    ];
    vIdField = "iLPID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LPDM", "sLPDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_LPMC", "sLPMC", "like", true);
    for (var item in data) {
        //if (item == 'sBGDDDM') {
        //    MakeSrchCondition2(arrayObj, data[item] + " or K.BGDDDM is null ", "sBGDDDM", "=", typeof (data[item]) == "string" ? true : false);
        //}
        if (item == 'fWCLJF') {
            MakeSrchCondition2(arrayObj, data[item], item, "<=", false);
        }
    }

    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.iCXLX = data["iDJLX"];
    Obj.sBGDDDM = data["sBGDDDM"];
    Obj.iPDCX = data["iPDCX"]; //是否要盘点
}

