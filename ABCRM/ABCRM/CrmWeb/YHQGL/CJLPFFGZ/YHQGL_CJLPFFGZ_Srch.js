vUrl = "../YHQGL.ashx";
var lx = GetUrlParam("lx");
vCaption = "前台礼品发放规则";

function InitGrid()
{
    vColumnNames = ['发放规则代码', '规则名称', '抽奖次数上限', '起点金额', '停用标记', ];
    vColumnModel = [
            { name: 'iJLBH', width: 100, },//sortable默认为true width默认150
			{ name: 'sYHQFFGZMC', width: 100, },
			{ name: 'fFFXE', width: 80 },
			{ name: 'fFFQDJE', width: 80 },
			{ name: 'iBJ_TY', width: 80, formatter: 'checkbox' },
    ];
};



$(document).ready(function ()
{
    //$("#HF_BJ_SF").val(bj_sf);
    $("#B_Exec").hide();

    //单选控制
    $("input[type='checkbox']").click(function ()
    {
        var ele = $(this);
        var name = ele.attr("name");
        ele.prop("checked", this.checked);
        if (this.checked)
        {
            ele.siblings("[name='" + name + "']").
			prop("checked", !this.checked);
        }
        var hf = "#" + name.replace("CB", "HF");
        $(hf).val(this.checked ? ele.val() : "");

    });

});

function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_FFGZMC", "sYHQFFGZMC", "in", true);
    //MakeSrchCondition(arrayObj, "HF_BJ_SF", "iBJ_SF", "=", false);
    MakeSrchCondition(arrayObj, "TB_CJCSSX", "fFFXE", "=", false);
    MakeSrchCondition(arrayObj, "TB_QDJE", "fFFQDJE", "=", false);
    MakeSrchCondition(arrayObj, "HF_BJ_TY", "iBJ_TY", "=", false);
    MakeSrchCondition2(arrayObj, 1, "iLX", "=", false);
    //MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};



