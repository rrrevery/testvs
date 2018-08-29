
vUrl = "../HYKGL.ashx";
var vCaption = "会员卡续费";
var HYKNO = GetUrlParam("HYKNO");
function InitGrid() {
    vColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称", "收款金额", "交易号"];
    vColumnModel = [
          { name: "iZFFSID", hidden: true },
          { name: "sZFFSDM", width: 150 },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text' },
    ];

    vPayColumnNames = ["HYID", "卡号", "姓名", "办卡日期", "iHYKTYPE", "卡类型", "主卡标记", "主卡标记", "卡费金额"];
    vPayColumnModel = [
      { name: "iHYID", hidden: true },
      { name: "sHYK_NO", width: 150 },
      { name: "sHY_NAME", width: 60 },
      { name: "dDJSJ", width: 150 },
      { name: "iHYKTYPE", hidden: true },
      { name: "sHYKNAME", width: 60 },
      { name: "iBJ_CHILD", formatter: ChildCellFormat, },
      { name: "fKFJE", width: 60 },
    ];
};


$(document).ready(function () {

    DrawGrid("list_ck", vPayColumnNames, vPayColumnModel, true);

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

    $("#TB_HYKNO").bind('keypress', function (event) {
        if (event.keyCode == 13) {
            getHYXX($("#TB_HYKNO").val());
        }
    });
    if (HYKNO != "") {
        getHYXX(HYKNO);
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
    if (vJLBH == "") {
        SearchData();
    }
});




function getHYXX(sHYK_NO) {

    if (sHYK_NO != "") {
        var str = GetHYXXData(0, sHYK_NO);
        if (str) {
            var Obj = JSON.parse(str);
            if (Obj.sHYK_NO == "") {
                ShowMessage("没有找到卡号！", 3);
                return;

            }
            switch (Obj.iSTATUS) {
                case 1:
                    ShowMessage("不在提前续费日期内,不可续费！", 3);
                    return;
                case 2:
                case -4:
                    break;
                default:
                    ShowMessage("无效卡,不可续费！", 3);
                    return;
            }
            ShowHYData(Obj);
        }
    }
    else {
        showMessage("会员卡号不能为空", 3);
    }
}

function ShowHYData(Obj) {
    var listData = new Array();
    $("#TB_HYKNO").val(Obj.sHYK_NO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.iHYKTYPE);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);//fKLXKFJE
    $("#LB_GBF").text(Obj.fKLXKFJE);
    if (Obj.SubCardTable.length > 0) {
        for (var i = 0; i < Obj.SubCardTable.length; i++) {
            var subcardItem = new Object();
            subcardItem = Obj.SubCardTable[i];
            if (subcardItem.iBJ_CHILD == 1 && (subcardItem.iSTATUS == 2 || (subcardItem.iSTATUS == -4)) && parseFloat(subcardItem.fKFJE) == 0) {
                listData.push(subcardItem);
            }
        }
    }
    $('#list_ck').datagrid('loadData', listData, "json");
    $('#list_ck').datagrid("loaded");
}


function IsValidData() {

    if ($("#HF_MDID").val() == "") {
        ShowMessage("请选择操作门店!", 3);
        return false;
    }
    if ($("#HF_HYID").val() == "") {
        ShowMessage("请输入会员卡号!", 3);
        return false;
    }
    if (parseFloat($("#LB_GBF").text()) > 0) {
        var rows = $("#list").datagrid("getData").rows;
        rows = rows.filter(function (item) {
            return item.fJE > 0;
        })
        if (rows.length <= 0) {
            ShowMessage("年费收款方式不能为空!", 3);
            return false;
        }
        else {
            var tp_je = parseFloat(0);
            for (var i = 0; i < rows.length; i++) {
                tp_je += parseFloat(rows[i].fJE);
            }
            if (parseFloat($("#LB_GBF").text()) != parseFloat(tp_je)) {
                ShowMessage("收款金额与应收金额不相符,请重新录入!", 3);
                return false;
            }
        }
    }

    return true;

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.fKFJE = $("#LB_GBF").text();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sHYK_NO = $("#TB_HYK_NO").val();
    Obj.sZY = $("#TB_ZY").val();
    if (parseFloat($("#LB_GBF").text()) > 0) {
        var lst = new Array();
        var Rows = $("#list").datagrid("getData").rows;
        for (var i = 0; i < Rows.length; i++) {
            var rowData = Rows[i]
            if (parseFloat(rowData.fJE) > 0) {
                lst.push(rowData);
            }
        }
        Obj.skitemTable = lst;
    }
    Obj.itemTable = lst;
    var lst_ck = new Array();
    lst_ck = $("#list_ck").datagrid("getData").rows;
    Obj.ckitemTable = lst_ck;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#LB_GBF").text(Obj.fKFJE),
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#TB_HYKNO").val(Obj.sHYK_NO);
    $("#HF_HYID").val(Obj.iHYID);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $('#list_ck').datagrid('loadData', Obj.ckitemTable, "json");
    $('#list_ck').datagrid("loaded");
}

function clearDataGrid() {
    $('#list').datagrid('loadData', { total: 0, rows: [] });
    $('#list_ck').datagrid('loadData', { total: 0, rows: [] });
}

function InsertClickCustom() {
    window.setTimeout(function () {
        clearDataGrid();
    }, 1);

};

function SearchData(page, rows, sort, order) {
    var vtp_Url = "../../CRMGL/CRMGL.ashx";
    var vPageMsgID_ZFFS = 5160003;
    var obj = MakeSearchJSON();
    page = page || $('#list').datagrid("options").pageNumber;
    rows = rows || $('#list').datagrid("options").pageSize;
    sort = sort || $('#list').datagrid("options").sortName;
    order = order || $('#list').datagrid("options").sortOrder;
    $('#list').datagrid("loading");
    $.ajax({
        type: "post",
        url: vtp_Url + "?mode=Search&func=" + vPageMsgID_ZFFS,
        async: true,
        data: {
            json: JSON.stringify(obj),
            titles: 'cybillsearch',
            page: page,
            rows: rows,
            sort: sort,
            order: order,
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#list').datagrid('loadData', JSON.parse(data), "json");
            $('#list').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}

function InsertClickCustom() {
    if ($("#list")[0].className != "") {
        SearchData();
    }
};

function MakeSearchJSON() {
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    Obj.iLoginRYID = iDJR;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, 0, "iBJ_KF", "=", false);
    MakeSrchCondition2(arrayObj, "0, 2", "iBJ_XSMD", "in", false);
    return arrayObj;
}