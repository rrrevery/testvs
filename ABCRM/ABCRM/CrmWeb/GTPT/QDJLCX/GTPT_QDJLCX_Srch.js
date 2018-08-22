vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['会员ID', '会员卡号', '会员姓名','卡类型',  '签到积分'],
    vColumnModel = [
        { name: 'iHYID',  width: 80, },
        { name: 'sHYK_NO', width: 100, },
        { name: 'sHY_NAME', width: 100, },
        { name: 'sHYKNAME', width: 100, },
        { name: 'fWCLJF', width: 100, },
]
}
$(document).ready(function () {
    $("#B_Insert").hide();
    $("#B_Update").hide();

    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });


    RefreshButtonSep();

})



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};