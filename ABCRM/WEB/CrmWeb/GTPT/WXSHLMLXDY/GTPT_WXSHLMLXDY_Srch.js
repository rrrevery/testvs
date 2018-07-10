vUrl = "../GTPT.ashx";
vCaption = "微信联盟商户类型定义";

function InitGrid() {
    vColumnNames = ['类型ID', '类型名称', '顺序'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iLXID', width: 80, },
			{ name: 'sLXMC', width: 100, },
            { name: 'iINX', width: 100, },

    ];
}



$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LXID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_LXMC", "sLXMC", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};