vUrl = "../YHQGL.ashx";
vCaption = "消费送积分规则定义";
var bj_sf = GetUrlParam("bj_sf");

function InitGrid() {
    vColumnNames = ['规则代码', '规则名称', '发放限额', '起点金额', '是否停用', ];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 120, },//sortable默认为true width默认150
		{ name: 'sYHQFFGZMC', width: 80, },
		{ name: 'fFFXE', width: 80, },
        { name: 'fFFQDJE', width: 80, },
        { name: 'iBJ_TY', width: 80, formatter: 'checkbox' },
    ];
};




$(document).ready(function () {
    $("#HF_BJ_SF").val(bj_sf);
    $("input[type='checkbox']").click(function () {
        var ele = $(this);
        var name = ele.attr("name");
        ele.prop("checked", this.checked);
        if (this.checked) {
            ele.siblings("[name='" + name + "']").
            prop("checked", !this.checked);
        }
        var hf = "#" + name.replace("CB", "HF");
        $(hf).val(this.checked ? ele.val() : "");

    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BJ_SF", "iBJ_SF", "=", false);
    MakeSrchCondition(arrayObj, "TB_FFGZMC", "sYHQFFGZMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_FFXE", "fFFXE", "=", false);
    MakeSrchCondition(arrayObj, "TB_QDJE", "fFFQDJE", "=", false);
    MakeSrchCondition(arrayObj, "HF_BJ_TY", "iBJ_TY", "=", false);
    return arrayObj;
};
