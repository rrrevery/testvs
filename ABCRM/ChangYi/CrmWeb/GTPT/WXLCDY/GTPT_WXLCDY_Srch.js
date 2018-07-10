vUrl = "../GTPT.ashx";
vCaption="微信楼层定义";

function InitGrid() {
    vColumnNames = ['楼层ID', '微信门店ID', '门店名称', '楼层名称', '序号', '图片'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'iWX_MDID', hidden: true, },
               { name: 'sMDMC', width: 80, },
               { name: 'sNAME', width: 80, },
               { name: 'iINX', width: 80, },
               { name: 'sIMG', width: 80, },
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_WXMDMC", function () {
        SelectWXMD("TB_WXMDMC", "HF_WXMDID", "zHF_WXMDID", false);
    });
    ////FillWXMD($("#DDL_WX_MDID"));//门店下拉框

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_WXMDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_NAME", "sNAME", "=", true);
    return arrayObj;
};