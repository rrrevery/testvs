
vUrl = "../CRMGL.ashx";
var vCaption = "业务员定义";
function InitGrid() {
    vColumnNames = ['业务员编号', '业务员代码', '所属门店', '业务员名称', '停用', '摘要', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sYWYDM', width: 80, },
            { name: 'sMDMC', width: 80, },
            { name: 'sYWYMC', width: 80 },
			{ name: 'iBJ_TY', width: 80, formatter: BoolCellFormat, },
			{ name: 'sZY', width: 120, },
    ];
}


$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_NAME", "sYWYMC", "like", true);
    MakeSrchCondition(arrayObj, "TB_YWYDM", "sYWYDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

