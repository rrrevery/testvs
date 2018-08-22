vUrl = "../GTPT.ashx";
vCaption = "移动端品牌商标定义";

function InitGrid() {
    vColumnNames = ['记录编号', '商标名称', '分类名称', '电话', '网址', '优先级', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'sSBMC', width: 120, },
            { name: 'sFLMC', width: 120, },
            { name: 'sPHONE', width: 120, },
            { name: 'sIP', width: 120, },
            { name: 'iINX', hidden: false, },
    ];
};

$(document).ready(function () {

    BFButtonClick("TB_FLMC", function () {
        SelectFLMC("TB_FLMC", "HF_FLID", "zHF_FLID",false);
    });
  
    BFButtonClick("TB_SBMC", function () {
        SelectWXSBMC("TB_SBMC", "HF_SBID", "zHF_SBID", false);
    });

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_SBID", "iSBID", "in", true);
    MakeSrchCondition(arrayObj, "TB_PHONE", "sPNONE", "=", true);
    MakeSrchCondition(arrayObj, "TB_IP", "sIP", "=", true);
    MakeSrchCondition(arrayObj, "HF_FLID", "iFLID", "=", false);
    MakeSrchCondition(arrayObj, "TB_INT", "iINX", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}





