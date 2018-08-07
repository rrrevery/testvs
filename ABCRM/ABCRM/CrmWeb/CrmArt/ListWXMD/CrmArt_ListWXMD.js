vUrl = "../../GTPT/GTPT.ashx";
vCaption = "微信门店";
var vDialogName = "ListWXMD";
var iWXPID = GetUrlParam("iWXPID");



function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '门店ID', hidden: true },
        { field: 'sMDMC', title: '门店名称', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_MDID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_MDMC", "sMDMC", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
  
    if (iWXPID == "" || iWXPID == undefined || iWXPID == 0 || iWXPID == null) {
        Obj.iBQFGZH = 1;//不区分公众号
    }
    else {
        Obj.iLoginPUBLICID = iWXPID;
    }





}