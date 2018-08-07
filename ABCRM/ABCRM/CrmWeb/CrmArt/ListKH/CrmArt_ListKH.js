vUrl = "../../MZKGL/MZKGL.ashx";
vCaption = "客户信息";
var vDialogName = "ListKH";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '客户ID', width: 100 },
        { field: "sKHMC", title: '客户名称', width: 80 },
        { field: "sLXRXM", title: '联系人姓名', width: 80 },
        { field: 'sLXRSJ', title: '联系人手机', width: 80, },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
    //pass
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_KHID", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_KHMC", "sKHMC", "like", true);
    return arrayObj;
};