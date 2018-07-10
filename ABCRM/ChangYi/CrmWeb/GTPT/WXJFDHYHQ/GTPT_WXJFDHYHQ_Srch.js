vUrl = "../GTPT.ashx";
vCaption = "微信积分兑换优惠券";
function InitGrid() {
    vColumnNames = ['记录编号', '状态', '开始日期', '结束日期', 'hyktype', '卡类型', 'YHQID', '优惠券', 'iDJR', '登记人', '登记时间', 'iZXR', '审核人', '审核日期', 'iQDR', '启动人', '启动时间', 'iZZR', '终止人', '终止日期',],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        {
            name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
                switch (cellvalue) {
                    case 0:
                        return "保存";
                        break;
                    case 1:
                        return "审核";
                        break;
                    case 2:
                        return "启动";
                        break;
                    case 3:
                        return "终止";
                        break;
                }
            }
        },
        { name: 'dKSRQ', width: 120, },
        { name: 'dJSRQ', width: 120, },
        { name: 'iHYKTYPE', hidden: true, },
        { name: 'sHYKNAME', width: 120, },
        { name: 'iYHQID', hidden: true, },
        { name: 'sYHQMC', width: 120, },

        { name: 'iDJR', hidden: true },
        { name: 'sDJRMC', width: 80, },
        { name: 'dDJSJ', width: 120, },
        { name: 'iZXR', hidden: true },
        { name: 'sZXRMC', width: 80, },
        { name: 'dZXRQ', width: 120, },
        { name: 'iQDR', hidden: true },
        { name: 'sQDRMC', width: 80, },
        { name: 'dQDSJ', width: 120, },
        { name: 'iZZR', hidden: true },
        { name: 'sZZRMC', width: 80, },
        { name: 'dZZRQ', width: 120, },
    ]

}
$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    CheckBox("CB_STATUS", "HF_STATUS");
})

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
function CheckBox(cbname, hfname) {
    $("input[type='checkbox'][name='" + cbname + "']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}