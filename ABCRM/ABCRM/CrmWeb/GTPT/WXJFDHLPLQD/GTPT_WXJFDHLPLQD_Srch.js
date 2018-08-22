vUrl = "../GTPT.ashx";
vCaption = "微信积分兑换礼品领取单";
function InitGrid() {
    vColumnNames = ['记录编号', '处理类型', 'iCZDD', '操作地点', 'iDJR', '登记人', '登记时间', 'iZXR', '审核人', '审核日期', '会员卡号'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        {
            name: 'iCLLX', width: 80, formatter: function (cellvalue, icol) {
                switch (cellvalue) {
                    case 0:
                        return "未领取";
                        break;
                    case 1:
                        return "领取";
                        break;
                    case 2:
                        return "冲正";
                        break;
                    case 3:
                        return "取消兑换";
                        break;

                }
            }
        },
        { name: 'iCZDD', hidden: true },
       { name: 'sBGDDMC', width: 80, },
        { name: 'iDJR', hidden: true },
        { name: 'sDJRMC', width: 80, },
        { name: 'dDJSJ', width: 120, },
        { name: 'iZXR', hidden: true },
        { name: 'sZXRMC', width: 80, },
        { name: 'dZXRQ', width: 120, },
        { name: 'sHYK_NO', width: 80, },
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
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
    //MakeSrchCondition2(arrayObj, $("input[name='DJZT']:checked").val(), "iSTATUS", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
