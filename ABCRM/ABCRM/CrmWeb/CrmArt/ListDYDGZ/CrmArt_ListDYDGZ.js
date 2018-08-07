vCaption = "选择规则";
vUrl = "../../YHQGL/YHQGL.ashx";
var vDialogName = "ListDYDGZ";
var vSingleSelect = true;
var vMODE = "";
var vBJ_TS = "";
var vData = $.dialog.data("DialogCondition");
if (vData) {
    vData = JSON.parse(vData);
    vMODE = vData["Mode"];
    vBJ_TS = vData["iBJ_TS"];
    if (vBJ_TS == undefined)
        vBJ_TS = vData.iBJ_TS;
}
var vAlreadyData = $.dialog.data("dialogInput");
if (vAlreadyData)
    vAlreadyData = JSON.parse(vAlreadyData);
else
    vAlreadyData = [];
var vAlreadyTabData = [];
var vSearchEnable = vData.SearchEnable && true;

function InitGrid() {
    switch (vMODE) {
        case "MBJZGZ":
            vPageMsgID = vMBJZPageId;
            vColumns = [
                 { field: 'iJLBH', title: '规则编号', width: 100 },
                 { field: 'sGZMC', title: '规则名称', width: 100 },
                 { field: 'fFFXE', title: '发放限额', width: 100 },
                 { field: 'fQDJE', title: '起点金额', width: 100 },
            ];
            break;
        case "FQGZ":
            vPageMsgID = vFQPageId;
            vColumns = [
                { field: 'iJLBH', title: '规则编号', width: 100 },
                { field: 'sGZMC', title: '规则名称', width: 100 },
                { field: 'fFFXE', title: '发放限额', width: 100 },
                { field: 'fFFQDJE', title: '起点金额', width: 100 },
            ];
            break;
        case "YQGZ":
            vPageMsgID = vYQPageId;
            vColumns = [
                { field: 'iJLBH', title: '规则编号', width: 100 },
                { field: 'sGZMC', title: '规则名称', width: 100 },
                { field: 'fYQBL_XFJE', title: '消费金额', width: 100 },
                { field: 'fYQBL_YHQJE', title: '优惠券金额', width: 100 },
                { field: 'fZDYQJE', title: '用券上限', width: 100 },
            ];
            break;
    }
    vIdField = "iJLBH";
}


$(document).ready(function () {
    SearchData();

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GZID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZGMC", "sGZMC", "like", true);
    if (vMODE == "FQGZ")
        MakeSrchCondition2(arrayObj, vBJ_TS, "iLX", "=", false);
    return arrayObj;
};
