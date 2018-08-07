vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "微信用户";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
var vDialogName = "ListWXUSER";

function InitGrid() {
    vColumns = [
        //{
        //    field: 'sHEADIMGURL', title: '头像', width: 100,hidden: true,
        //    formatter: function (cellvalue, options, rowObject) {
        //        return '<img src="' + cellvalue + '" style="width:100px;height:80px;"/>';
        //    }
        //},
        //{ field: 'sNICKNAME', title: '昵称', width: 100 },
        { field: 'sWX_NO', title: '微信号', width: 100 },
        { field: 'sOPENID', title: 'sOPENID', hidden: true },
        { field: 'dDJSJ', title: '关注时间', width: 100 },
        { field: 'iHYKTYPE', title: 'iHYKTYPE', hidden: true },
        { field: 'sHYKNAME', title: 'sHYKNAME', hidden: true },
        //{ field: 'iGROUPID', title: 'iGROUPID', hidden: true },
        //{ field: 'sGROUP_NAME', title: 'sGROUP_NAME', hidden: true },
    ];
    vIdField = "sOPENID";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_WX_NO", "sWX_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_NICKNAME", "sNICKNAME", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.dialogName = vDialogName;
}