vUrl = "../GTPT.ashx";
vCaption = "微信预约服务定义";
function InitGrid() {
    vColumnNames = ['ID', '预约主题', '门店ID', '门店'];
    vColumnModel = [
               { name: 'iID', width: 80, },
               { name: 'sMC', width: 80, },
               { name: 'iMDID', hidden: true, },
               { name: 'sMDMC', width: 80, },
    ];
}

$(document).ready(function () {

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    //FillMD($("#DDL_MDID"));//门店下拉框

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "DDL_MDID", "iMDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_INX", "iINX", "=", false);
    MakeSrchCondition(arrayObj, "TB_YYZT", "sMC", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};