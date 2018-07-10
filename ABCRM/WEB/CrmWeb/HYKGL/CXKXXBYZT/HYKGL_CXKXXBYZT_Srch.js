vUrl = "../HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['卡类型', '数量', '余额', '优惠券金额', ];
    vColumnModel = [
            { name: 'sHYKNAME', width: 80, },//sortable默认为true width默认150
			{ name: 'iSL', width: 80, },
			{ name: 'dYE', width: 80, },
            { name: 'dJE', width: 80, },
    ];
};
$(document).ready(function () {
    //CheckBox("CB_TABNAME", "HF_TABNAME");
    //CheckBox("CB_STATUS", "HF_STATUS");
    //卡类型弹出框 
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });
    RefreshButtonSep();
});

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
function SetControlState() {
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    //$("input[name='CB_STATUS']").eq(0).prop("checked", true);
    //$("input[name='CB_TABNAME']").eq(0).prop("checked", true);
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    var status = $("[name='CB_STATUS']:checked").val();
    switch (status) {
        case "":
            break;
        case "0":
            MakeSrchCondition2(arrayObj, status, "iSTATUS", ">=", false);
            break;
        case "3":
        case "4":
        case "-4":
            MakeSrchCondition2(arrayObj, status, "iSTATUS", "=", true);
            break;
        case "-1":
            MakeSrchCondition2(arrayObj, status, "iSTATUS", "<=", true);
            break;
    }

    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_YXQ1", "dYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ2", "dYXQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};


function AddCustomerCondition(Obj) {
    // Obj.sTABLENAME = $("#HF_TABNAME").val();
    Obj.sTABLENAME = $("[name='CB_TABNAME']:checked").val();
}
