vUrl = "../GTPT.ashx";
vCaption = "微信分享定义";

function InitGrid() {
    vColumnNames = ['ID', '名称', '分享标题'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iID', width: 80, },
			{ name: 'sNAME', width: 100, },
            { name: 'sTITLE', width: 130, },
    ];
}



$(document).ready(function () {


});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_ID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_NAME", "sNAME", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};