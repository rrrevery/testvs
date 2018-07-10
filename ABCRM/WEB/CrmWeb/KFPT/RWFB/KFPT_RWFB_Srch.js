vUrl = "../KFPT.ashx";
vCaption = "任务发布";

function InitGrid() {
    vColumnNames = ['记录编号', '任务主题', '开始日期', '结束日期', 'iDJR', '发布人', '发布时间', '任务内容', ];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sRWZT', width: 80, },
			   { name: 'dKSRQ', width: 100, },
			   { name: 'dJSRQ', width: 100, },
               { name: 'iDJR', hidden: true, },
			   { name: 'sDJRMC', width: 80, },
			   { name: 'dDJSJ', width: 140, },
               { name: 'sRW', width: 160, },
    ];
}

$(document).ready(function () {

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", true);
    });
    FillRWZT("S_RWZT");
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "S_RWZT", "iJLBH", "=", false);
    MakeSrchCondition2(arrayObj, "%" + $("#TB_RW").val() + "%", "sRW", "like", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    return arrayObj;
};