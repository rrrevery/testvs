vUrl = "../YHQGL.ashx";
vCaption = "优惠券面值定义";
var bjarr = new Array();
function InitGrid() {
    vColumnNames = ['面值编号', '优惠券', '面值名称', '金额', '停用'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', },//sortable默认为true width默认150
			{ name: 'sYHQMC', },
			{ name: 'sNAME', },
            { name: 'dJE', },
            { name: 'iBJ_TY', formatter: "checkbox" },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();

    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });

    $("input[name='CB_BJ_TY']").on("click", function () {
        var ele = $(this).prop("checked", this.checked);
        var index = $.inArray(ele.val(), bjarr);
        if (this.checked) {//选中
            if (index == -1) {
                bjarr.push(ele.val());
            }
        }
        else {//取消选中
            if (index != -1) {
                var temp = bjarr[index];
                bjarr[index] = bjarr[bjarr.length - 1];
                bjarr[bjarr.length - 1] = temp;
                bjarr.pop();
            }
        }
        var str = "";
        for (var i in bjarr) {
            str += bjarr[i];

        }
        $("#HF_BJ_TY").val(str.substring(0, str.length - 1));
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iYHQMZID", "=", false);
    MakeSrchCondition2(arrayObj, "%" + $("#TB_NAME").val() + "%", "sNAME", "like", true);
    MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
    MakeSrchCondition(arrayObj, "TB_JE", "dJE", "=", false);
    MakeSrchCondition(arrayObj, "HF_BJ_TY", "iBJ_TY", "in", false);
    //MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
