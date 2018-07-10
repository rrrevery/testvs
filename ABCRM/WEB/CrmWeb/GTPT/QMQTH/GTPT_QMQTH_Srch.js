vUrl = "../GTPT.ashx";
vCaption = "钱买券后台退款";


function InitGrid() {
    vColumnNames = ["卡号", "状态", "金额", "订单号", "备注", "登记人", "登记时间"];
    vColumnModel = [
           { name: 'cardNo', width: 130, },
            {
                name: 'iapiStatus', width: 80, formatter: function (cellvalue, icol) {
                    switch (cellvalue) {
                        case 0:
                            return "成功";
                            break;
                        case 1:
                            return "失败";
                            break;
                      
                    }
                }
            },
            


            { name: 'totalMoney', width: 80, },
            { name: 'code', width: 150, },
           { name: 'sERRORM', width: 150, },
          { name: 'sDJRMC', },
           { name: 'dDJSJ', },






    ];

};

$(document).ready(function () {
    $("#B_Exec").hide();
    //$("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

    CheckBox("CB_STATUS", "HF_STATUS");
  
});


function MakeSearchCondition() {
    var arrayObj = new Array();

    var vSTATUS = $("[name='STATUS']:checked").val();

    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "cardNo", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    if (vSTATUS != 'all') {
        MakeSrchCondition2(arrayObj, vSTATUS, "iSTATUS", "=", true);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

//复选框控制
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