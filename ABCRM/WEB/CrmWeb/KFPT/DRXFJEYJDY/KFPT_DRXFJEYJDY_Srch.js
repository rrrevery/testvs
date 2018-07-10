vUrl = "../KFPT.ashx";
var vHYKTYPE = "";
vCaption = "当日消费金额预警"
function InitGrid() {
    vColumnNames = ['HYKTYPE', '会员卡类型', '预警金额'];
    vColumnModel = [
			{ name: 'iJLBH', hidden: true, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'iYJJE', },
    ];
}



$(document).ready(function () {
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });//卡类型多选

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iJLBH", "in", false);
    MakeSrchCondition(arrayObj, "TB_YJJE", "iYJJE", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};