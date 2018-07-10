vUrl = "../HYKGL.ashx";
vCaption = "金额账户汇总查询";


function InitGrid() {


    vColumnNames = ['处理日期', '收款台', 'iMDID', '门店名称', '存款金额', '消费金额'];
    vColumnModel = [
            { name: 'dCLSJ', width: 80,},
            { name: 'sSKTNO', width: 80,  },
            { name: 'iMDID', width: 80, hidden: true },//sortable默认为true width默认150
			{ name: 'sMDMC', width: 80,  },
			{ name: 'fJFJE', },
            { name: 'fDFJE', },
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    RefreshButtonSep();
    $("input[name='DJLX']").bind("click", function () {

        var vDJLX = $("[name='DJLX']:checked").val();
        switch (vDJLX) {
            case "0":
                $("#RQ").hide();
                $("#SKT").hide();
                $("#MD").show();
                $('#list').datagrid('refreshRow', 0);
                $('#list').datagrid('refreshRow', 1);
                $('#list').datagrid('refreshRow', 2);
                $('#list').datagrid('refreshRow', 3);
                break;
            case "1":
                $("#RQ").hide();
                $("#SKT").show();
                $("#MD").hide();
                break;

            case "2":
                $("#RQ").show();
                $("#SKT").hide();
                $("#MD").show();
                break;

            case "3":
                $("#RQ").show();
                $("#SKT").show();
                $("#MD").hide();
                break;
        }
    });
    $('#list').datagrid(
        {
            onLoadSuccess: function (data) {
                if ($("[name='DJLX']:checked").val() == 2 || $("[name='DJLX']:checked").val() == 3) {
                    $("#list").datagrid("showColumn", "dCLSJ"); // 
                } else {
                    $("#list").datagrid("hideColumn", "dCLSJ"); // 设置隐藏列
                }
                if ($("[name='DJLX']:checked").val() == 1 || $("[name='DJLX']:checked").val() == 3) {
                    $("#list").datagrid("showColumn", "sSKTNO"); // 
                } else {
                    $("#list").datagrid("hideColumn", "sSKTNO"); // 设置隐藏列
                }
                if ($("[name='DJLX']:checked").val() == 0 || $("[name='DJLX']:checked").val() == 2) {
                    $("#list").datagrid("showColumn", "sMDMC"); // 
                } else {
                    $("#list").datagrid("hideColumn", "sMDMC"); // 设置隐藏列
                }
            }
        });
});

function SetControlState() {
    var vDJLX = $("[name='DJLX']:checked").val();
    switch (vDJLX) {
        case "0":
            $("#RQ").hide();
            $("#SKT").hide();
            $("#MD").show();
            break;
        case "1":
            $("#RQ").hide();
            $("#SKT").show();
            $("#MD").hide();
            break;
        case "2":
            $("#RQ").show();
            $("#SKT").hide();
            $("#MD").show();
            break;

        case "3":
            $("#RQ").show();
            $("#SKT").show();
            $("#MD").hide();
            break;
    }
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    if ($("#TB_MDMC").val() != "")
        MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    if ($("#TB_SKTNO").val() != "")
        MakeSrchCondition(arrayObj, "TB_SKTNO", "sSKTNO", "=", true);
    if ($("#TB_RQ1").val() != "" && $("#TB_RQ2").val() != "") {
        MakeSrchCondition(arrayObj, "TB_RQ1", "dCLSJ", ">=", true);
        MakeSrchCondition(arrayObj, "TB_RQ2", "dCLSJ", "<=", true);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}

function AddCustomerCondition(Obj) {
    Obj.iDZLX = $("[name='DJLX']:checked").val();
}

