vUrl = "../HYXF.ashx";
vPLDBQ = GetUrlParam("PLDBQ");
if (vPLDBQ == 1)
{
    var GridWidth = 1000;
}
var vDialogName = "";

function InitGrid() {
    vColumnNames = ["会员姓名", "卡号", "卡类型", "销售金额", "会员折扣金额", "积分", "通讯地址", "邮政编码", "证件号码", "手机号码", "办公电话", "电子邮件", "区域名称", "消费次数", "出生日期", "性别"];
    vColumnModel = [
            { name: 'sHY_NAME', width: 100, },
			{ name: 'sHYK_NO', width: 100, },
			{ name: 'sHYKNAME', width: 100 },
            { name: 'fXSJE', width: 100, },
            { name: 'fZKJE_HY', width: 100 },
            { name: 'fJF', width: 100, },
            { name: 'sTXDZ', width: 200, },
            { name: 'sYZBM', width: 100, },
            { name: 'sSFZBH', width: 200, },
            { name: 'sSJHM', width: 100, },
            { name: 'sBGDH', width: 100, hidden: true, },
	        { name: 'sEmail', width: 200 },
            { name: 'sQYMC', width: 200, },
            { name: 'fXFCS', width: 80 },
            { name: 'dCSRQ', width: 100, },
            { name: 'sSEX', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_QD").hide();//确定按钮

    document.getElementById("B_QD").onclick = QDClick;


    if (vPLDBQ == "1") {
        GridWidth = 1000;

        $("#B_QD").show();//确定按钮
        $("#B_ExportMember").hide();
        $("#B_Export").hide();
        $("#B_Print").hide();
        $("#B_Exec").hide();
        $("#B_Insert").hide();
        $("#B_Update").hide();
        $("#B_Delete").hide();
    }
    else {
        $("#B_Exec").hide();
        $("#B_Insert").hide();
        $("#B_Update").hide();
        $("#B_Delete").hide();
        $("#B_ExportMember").show();
    }
    // DrawView();

    //CYCBL_ADD_ITEM("CBL_HYBJ", GetHYXXXM(12));

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });

    $("#TB_SBMC").click(function () {
        SelectSPSB("TB_SBMC", "HF_SBID", "zHF_SBID", false, $("#HF_SHDM").val());
    });

    $("#TB_SPFL").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHSPFL("TB_SPFL", "HF_SPFLDM", "zHF_SPFLDM", true, condData);
        }
    });

    $("#TB_SHBMMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            ShowMessage("请选择商户！", 3);
            return;
        }
        else {
            var condData = new Object();
            condData["sSHDM"] = $("#HF_SHDM").val();
            SelectSHBM("TB_SHBMMC", "HF_SHBMDM", "zHF_SHBMDM", true, condData);
        }
    });

    $("#TB_QYMC").click(function () {
        SelectQY("TB_QYMC", "HF_QYDM", "zHF_QYDM",true);
    });

    $("#TB_HYBQ").click(function () {
        SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ");
    });



    $("input[type='checkbox'][name='CB_SEX']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#HF_SEX").val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#HF_SEX").val("");
        }
        if ($.trim($(this).val()) == "") {
            $("#HF_SEX").val("");
        }
    });
    RefreshButtonSep();
});


function AddCustomerCondition(Obj) {
    if ($("#TB_AGE1").val()!="")
        Obj.iAGE1 = $("#TB_AGE1").val();
    if ($("#TB_AGE2").val() != "")
        Obj.iAGE2 = $("#TB_AGE2").val();
    if ($("#TB_XFPM").val() != "")
        Obj.iXFPM = $("#TB_XFPM").val();
    Obj.sHYKNO_Begin = $("#TB_HYKNO1").val();
    Obj.sHYKNO_End = $("#TB_HYKNO2").val();
  


}
function MakeSearchCondition() {
    var arrayObj = new Array();
    var tp_yt = "";
    var tp_else = "";
    $("[type='checkbox'][name='CB_YT']").each(function () {
        if (this.checked == true) {
            if (parseFloat(this.value) != 5) {
                if (tp_yt == "") {
                    tp_yt += this.value;
                }
                else {
                    tp_yt += "," + this.value;
                }
            }
            else {
                tp_else = this.value;
            }
        }
    })
    if (tp_yt != "" && tp_else == "") {
        MakeSrchCondition2(arrayObj, tp_yt, "iBJ_YT", "in", false);
    }
    if (tp_yt == "" && tp_else != "") {
        MakeSrchCondition2(arrayObj, "null", "iBJ_YT", "is", false);
    }
    if (tp_yt != "" && tp_else != "") {
        MakeSrchCondition2(arrayObj, "in(" + tp_yt + ") or B.BJ_YT is null", "iBJ_YT", "", false);
    }

    var hybj = Get_CYCBL_CheckItem("CBL_HYBJ");
    if (hybj != "") {
        MakeSrchCondition2(arrayObj, hybj, "iBJID", "in", false);
    }
    MakeSrchCondition(arrayObj, "HF_HYBQ", "iXH", "in", false);
    MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "=", true);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    if ($("#HF_SPFLDM").val() != "") {
        MakeSrchCondition(arrayObj, "HF_SPFLDM", "sSPFLDM", "like", false);
    }
    MakeSrchCondition(arrayObj, "HF_QYDM", "sQYDM", "in", true);
    //MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "HF_SEX", "iSEX", "=", false);
    MakeSrchCondition(arrayObj, "TB_JF1", "fJF", ">=", false);
    MakeSrchCondition(arrayObj, "TB_JF2", "fJF", "<=", false);
    MakeSrchCondition(arrayObj, "TB_XFJE1", "fXFJE", ">=", false);
    MakeSrchCondition(arrayObj, "TB_XFJE2", "fXFJE", "<=", false);
    MakeSrchCondition(arrayObj, "TB_TJRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_TJRQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);
    MakeSrchCondition(arrayObj, "TB_BGDH", "sBGDH", "=", true);
    MakeSrchCondition(arrayObj, "HF_SBID", "iSBID", "in", false);
    MakeSrchCondition(arrayObj, "TB_YXQ1", "dYXQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ2", "dYXQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_SKCS1", "fXFCS", ">=", false);
    MakeSrchCondition(arrayObj, "TB_SKCS2", "fXFCS", "<=", false);

    if ($("#HF_SHBMDM").val() != "") {
        MakeSrchCondition(arrayObj, "HF_SHBMDM", "sBMDM", "like", false);
    }
    MakeSrchCondition(arrayObj, "TB_SPMC" , "sSPMC", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};







function ReturnQX() {
    return bCanShowPublic;
}

function Get_CYCBL_CheckItem(cbl_name) {
    var valuelist = "";
    $("input[name^='" + cbl_name + "']").each(function () {
        if (this.checked) {
            //  valuelist += $(this).parent("span").attr("alt") + ";";
            //valuelist += $(this).text() + ";";
            valuelist += $(this).val() + ",";
        }
    });
    if (valuelist.length > 0) {
        valuelist = valuelist.substring(0, valuelist.length - 1);
    }
    return valuelist;
};

//function DrawGrid(listName, vColName, vColModel, vSingle) {
//    //为简化查询模板开发流程，统一Grid格式，新的查询可以使用InitGrid函数初始化vColumnNames和vColumnModel
//    InitGrid();
//    if (listName == undefined) { listName = "list"; }
//    if (vSingle == undefined) { vSingle = true; }
//    if (vColName == undefined) { vColName = vColumnNames; }
//    if (vColModel == undefined) { vColModel = vColumnModel; }
//    if (vColumns.length == 0 || vColName != vColumnNames) {
//        vColumns = InitColumns(undefined, vColModel, vColName);
//        vAllColumns = vColumns;
//    }
//    $("#" + listName + "").datagrid({
//        //url: vUrl + "?mode=Search&func=" + vPageMsgID,
//        //method: 'post',
//        width: GridWidth,
//        height: GridHeight,//674,
//        autoRowHeight: false,
//        striped: true,
//        columns: [vColumns],
//        frozenColumns: [vFrozenColumns],
//        sortName: vColumns[0].field,
//        singleSelect: vSingle,
//        sortOrder: 'desc',
//        //remoteSort: false,
//        //fitColumns: true,
//        //scrollbarSize: 0,
//        showHeader: true,
//        showFooter: true,
//        pagePosition: 'bottom',
//        rownumbers: true, //添加一列显示行号
//        pagination: true,  //启用分页
//        pageNumber: 1,
//        pageSize: 10,
//        pageList: [10, 50, 100],
//        onSortColumn: function (sort, order) {
//            SearchData(undefined, undefined, sort, order, listName);
//        },
//        onClickRow: OnClickRow,
//        onDblClickRow: DBClickRow,
//    });
//    var pager = $('#' + listName + '').datagrid("getPager");
//    pager.pagination({
//        onSelectPage: function (pageNum, pageSize) {
//            SearchData(pageNum, pageSize, undefined, undefined, listName);
//        },
//        buttons: [{
//            iconCls: 'fa fa-cog datagrid_setting',
//            handler: ListSet
//        }]
//    });
//}

//function OnClickRow(rowIndex, rowData) {
//    if (vSQDXZ == "1") {
//        //window.parent.ReloadData(rowData);
//        //window.parent.art.dialog.list['sqd'].close();
//    }
//    else {
//        SetControlBaseState();
//    }
//}

function AddCustomerButton() {
    AddToolButtons("确定", "B_QD");

    //单据特殊的按钮在这里加，因为如果直接AddToolButtons的话，会覆盖原来按钮的事件，所以加了一个这个方法
    
};

function QDClick() {
    //T--确认事件
    var Rows = $("#list").datagrid("getSelections");

    //$.dialog.data(vDialogName, JSON.stringify(Rows));

    window.parent.ReloadData(Rows);
    window.parent.art.dialog.list['sqd'].close();


    //for (var j = 0; j < Rows.length; j++) {
    //    array.push(Rows[j]);
    //}
    //$('#' + listName).datagrid('appendRow', lst[i]);

    //$.dialog.data(vDialogName, JSON.stringify(Rows));
    //$.dialog.data('dialogSelected', Rows.length > 0);
    //$.dialog.close();

};