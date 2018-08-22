vUrl = "../GTPT.ashx";
vCaption = "微信门店定义";

function InitGrid() {
    vColumnNames = ['门店ID', '门店名称', '门店地址', '门店电话', '营业时间'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iMDID',width: 80, },
			{ name: 'sMDMC', width: 100, },
            { name: 'sADDRESS', width: 130, },
			{ name: 'sPHONE', width: 130, },
            { name: 'sTIME', width: 130, },           
    ];
}



$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_MDID", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_MDMC", "sMDMC", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};