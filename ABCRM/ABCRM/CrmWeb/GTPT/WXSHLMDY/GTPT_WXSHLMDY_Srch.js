vUrl = "../GTPT.ashx";
vCaption = "微信商户联盟定义";

function InitGrid() {
    vColumnNames = ['联盟商户ID', '联盟商户名称', '地址', '电话'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iID', width: 80, },
			{ name: 'sLMSHMC', width: 200, },
            { name: 'sADDRESS', width: 200, },
            { name: 'sPHONE', width: 200, },

    ];
}



$(document).ready(function () {


});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_LMSHID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_LMSHMC", "sLMSHMC", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};