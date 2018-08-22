vUrl = "../MZKGL.ashx";

function AddCustomerButton() {
    AddToolButtons("支票启动", "B_SaveTo");
};

function InitGrid() {
    vColumnNames = ['记录编号', '客户名称', '应收金额', '实收金额', '售卡数量', '保管地点', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核日期'];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, sortable: true },
			{ name: 'iKHMC', width: 80, sortable: true },
			{ name: 'iYSJE', width: 80, sortable: true },
            { name: 'iSSJE', width: 80, sortable: true },
            { name: 'iSKSL', width: 80, sortable: true },
            { name: 'sBGDDMC', width: 100 },
            { name: 'iDJR', hidden: true, sortable: true },
			{ name: 'sDJRMC', width: 80, sortable: true },
			{ name: 'dDJSJ', width: 120, sortable: true },
			{ name: 'iZXR', hidden: true, sortable: true },
			{ name: 'sZXRMC', width: 80, sortable: true },
			{ name: 'dZXRQ', width: 120, sortable: true },
    ];
}


$(document).ready(function () {
    DrawGrid("list", vColumnNames, vColumnModel, false);
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM",false);
    })


    $("#B_SaveTo").click(function () {
        if (posttosever(SaveDataToDataBase(), vUrl, "Insert") == true) {
            SetControlBaseState();
        }
    })

});


function SaveDataToDataBase(){
    var Obj = new Object();
    var lst = new Array();
    var rowIDs = $("#list").datagrid("getSelections");
    for (var i = 0; i < rowIDs.length; i++) {
        var rowData = rowIDs[i]; 
        lst.push(rowData);
    }
    Obj.MZKPLQDITEM = lst;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();

    MakeSrchCondition(arrayObj, "TB_SKJLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", true);
    MakeSrchCondition(arrayObj, "TB_MZJE", "fMZJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_SSJE", "fSSJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO_Begin", "sHYK_NO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO_End", "sHYK_NO", "<=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    return arrayObj;
};

function SetControlState()
{
    $("#B_Update").hide();
    $("#B_Insert").hide();
    $("#B_Export").hide();
    $("#B_Print").hide();
    RefreshButtonSep();
    var rowDatas = $("#list").datagrid("getSelections");
    if (rowDatas.length <= 0) {
        $("#B_SaveTo").prop("disabled", true);
    }
    else {
        $("#B_SaveTo").prop("disabled", false);
    }
}






function posttosever(str_data, str_url, str_mode, str_suc) {
    if (str_suc == undefined)
        str_suc = "操作成功";
    var canBeClose = false;
    var myDialog = art.dialog({
        lock: true, content: '正在提交数据,请等待......'
        , close: function () {
            if (canBeClose) {
                return true;
            }
            return false;
        }
    });
    $.ajax({
        type: "post",
        url: str_url + "?mode=" + str_mode + "&func=" + vPageMsgID ,//+ "&old=" + vOLDDB,
        async: false,
        data: { json: JSON.stringify(str_data), titles: 'cecece' },
        success: function (data) {
            canBeClose = true;
            myDialog.close();
            if (data.indexOf("错误") >= 0 || data.indexOf("error") >= 0) {
                art.dialog({ lock: true, content: data });
                canBeClose = false;
            }
            else {
                if (str_suc != "")
                    art.dialog({ lock: true, content: str_suc, time: 1 });
                canBeClose = true;
            }
        },
        error: function (data) {
            canBeClose = false;
            myDialog.close();
            art.dialog({ lock: true, content: "保存失败：" + data });
        }
    });
    return canBeClose;
}

