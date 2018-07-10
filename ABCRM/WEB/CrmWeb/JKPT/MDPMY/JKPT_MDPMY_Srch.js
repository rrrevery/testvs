vUrl = "../JKPT.ashx";
vCaption = "会员门店消费月排名";
function InitGrid() {
    vColumnNames = ['会员卡号', '卡类型', '姓名', '性别', '手机号码', '交易金额', '交易次数', '退货次数', '积分', '最小金额', '最大金额', ];
    vColumnModel = [
             { name: 'sHYK_NO', width: 80, },
             { name: 'sHYKNAME', width: 80, },
             { name: 'sHY_NAME', width: 80, },
             { name: 'sSEX', width: 60, },
             { name: 'sSJHM', width: 100, hidden: true },
             { name: 'fXFJE', width: 80, },
             { name: 'iXFCS', width: 80, },
             { name: 'iTHCS', width: 80 },
             { name: 'fJF', width: 80, },
             { name: 'fZXJE', width: 80, },
             { name: 'fZDJE', width: 80, },
    ];
}


$(document).ready(function () {
    //$("#list").jqGrid("setGridParam", {
    //    ondblClickRow: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        MakeNewTab("/CrmWeb/KFPT/DYHYFX/KFPT_DYHYFX.aspx?hykno=" + rowData.sHYK_NO, "单会员分析", vPageMsgID);
    //    },
    //});
    //FillMD($("#DDL_MDID"), "", 0);//门店下拉框
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
})

function AddCustomerCondition(Obj) {
    Obj.iPM = $("[name='djzt']:checked").val();
    Obj.irownum = $("#TB_ROWNUM").val();
    Obj.iMAX_XFCS = $("#TB_TSJX").val();
}
function IsValidSearch() {
    if ($("#TB_YEARMONTH").val() == "") {
        ShowMessage("请选择年月");
        return false;
    }
    if ($("#HF_MDID").val() == "" || $("#HF_MDID").val() == null) {
        ShowMessage("请选择门店");
        return false;
    }
    if ($("#TB_ROWNUM").val() == "") {
        ShowMessage("请输入筛选记录");
        return false;
    }
    if ($("[name='djzt']:checked").val() == null) {
        ShowMessage("请选择排序标准");
        return false;
    }
    return true;
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false);
    MakeSrchCondition(arrayObj, "TB_YEARMONTH", "iYEARMONTH", "=", false);

    return arrayObj;
};
