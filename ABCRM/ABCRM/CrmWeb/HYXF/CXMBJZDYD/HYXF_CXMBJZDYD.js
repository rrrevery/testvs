vUrl = "../HYXF.ashx";
gzdialog = "MBJZGZ";

function InitGrid() {
    vColumnNames = ["优先顺序", "规则号", "规则方案", "发放规则ID", "处理标记", "处理对象", "分单编号", "iCLFS_BM", "iCLFS_SPFL", "iCLFS_SPSB", "iCLFS_HT", "iCLFS_SP", "iCLFS_HYZ", ];//"分值",
    vColumnModel = [
          { name: "iXH", },
          { name: "iGZBH", },
       // { name: "fFZ", width: 60, editable: true, editor: 'text' },
          { name: "sGZMC", width: 60 },
          { name: "iGZID", width: 60, hidden: true },
          { name: "iBJ_CJ", width: 50, editor: { type: 'checkbox', options: { on: 1, off: 0 } } },
          { name: "sCLDX", width: 90 },
          { name: "iINX", hidden: true },
          { name: "iCLFS_BM", hidden: true },
          { name: "iCLFS_SPFL", hidden: true },
          { name: "iCLFS_SPSB", hidden: true },
          { name: "iCLFS_HT", hidden: true },
          { name: "iCLFS_SP", hidden: true },
          { name: "iCLFS_HYZ", hidden: true },

    ];
}

$(document).ready(function () {
});

function AddNewFDPanel(tab_index, rowIndex, tp_max) {
    $("#list").datagrid("appendRow", {
        iINX: tab_index,
        iXH: rowIndex + 1,
        sSYGZMC: "",
        iSYGZID: 0,
        iBJ_CJ: 1,
        iGZBH: parseInt(tp_max) + 1,
        iCLFS_BM: 0,
        iCLFS_SPFL: 0,
        iCLFS_SPSB: 0,
        iCLFS_HT: 0,
        iCLFS_SP: 0,
        iCLFS_HYZ: 0,
    });
}

function IsValidData() {
    if ($("#TB_RQ1").val() == "" || $("#TB_RQ2").val() == "") {
        ShowMessage("请输入日期范围");
        return false;
    }
    if ($("#HF_CXID").val() == "") {
        ShowMessage("请选择促销活动");
        return false;
    }
    if ($("[name='LJFS']:checked").val() == "") {
        ShowMessage("请选择累计方式");
        return false;
    }
    return true;
}

function SaveData(Obj) {
    var tab = $('#tt').tabs('getSelected');
    var tab_index = $('#tt').tabs('getTabIndex', tab);
    removeRecord(tab_index);
    var curTabData = $("#list").datagrid("getData").rows;
    addRecord(curTabData);
    Obj.dRQ1 = $("#TB_RQ1").val();
    Obj.dRQ2 = $("#TB_RQ2").val();
    Obj.itemFD = itemFD;
    Obj.itemGZSD = itemGZSD;
    Obj.itemGZITEM = itemGZITEM;
    Obj.iCXID = $("#HF_CXID").val();
    Obj.iHYKTYPE = 0;//$("#HF_HYKTYPE").val();
    Obj.iBJ_TD = $("#CK_BJ_TD").prop("checked") ? "1" : "0";
    Obj.iYXCLBJ = $("#CK_YX")[0].checked ? 1 : 0;
    Obj.iXFLJMJFS = $("[name='LJFS']:checked").val();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    var tabs = $('#tt').tabs('tabs');
    for (var i = 0; i < tabs.length;) {
        var delTabInx = $('#tt').tabs('getTabIndex', tabs[0]);
        $('#tt').tabs('close', delTabInx);
    }
    fdindex = 0;
    for (var i = 0; i < Obj.itemFD.length; i++) {
        addPanel();
    }
    $('#tt').tabs('select', 0);
    $("#list").datagrid("loadData", Obj.itemGZSD.filter(function (item) { return item.iINX == 0; }));
    var curTabInfo = Obj.itemFD.filter(function (item) {
        return item.iINX == 0;
    })
    if (curTabInfo.length) {
        $("#HF_CXSD").val(curTabInfo[0].sSD);
        if (curTabInfo[0].sSD.length > 0) {
            $("#LB_CXSDSTR").text(DatasToChinese(curTabInfo[0].sSD))
        }
    }
    itemFD = Obj.itemFD;
    itemGZSD = Obj.itemGZSD;
    itemGZITEM = Obj.itemGZITEM;
    $("#HF_SHDM").val(Obj.sSHDM);
    //$("#S_SH").val(Obj.sSHDM);
    $("#S_SH").combobox("setValue", Obj.sSHDM);
    $("#TB_BM").val(Obj.sBMMC);
    $("#HF_BMDM").val(Obj.sSHBMDM);
    $("#HF_SHBMID").val(Obj.iSHBMID);
    $("#LB_SHBM").text(Obj.sBMMC);
    $("#TB_RQ1").val(Obj.dRQ1);
    $("#TB_RQ2").val(Obj.dRQ2);
    $("#CK_BJ_TD").prop("checked", Obj.iBJ_TD == "1");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#HF_CXID").val(Obj.iCXID);
    $("#CK_YX")[0].checked = Obj.iYXCLBJ == 1;
    $("[name='XFLJMJFS']").each(function () {
        if ($(this).val() == Obj.iXFLJMJFS) {
            $(this).prop("checked", true).siblings().prop("checked", false);
        }
    });
    //$("[name='XFLJMJFS'][value=" + Obj.iXFLJMJFS + "]").attr("checked", true);
}

//function onClickCellCustom(index, field) {
//    if (field == "sGZMC") {
//        if (endEditing() && vProcStatus != cPS_BROWSE) {
//            $('#list').datagrid('selectRow', index)
//            .datagrid('editCell', { index: index, field: field });
//            editIndex = index;
//            var ed = $("#list").datagrid('getEditor', { index: index, field: field });
//            if (ed) {
//                $(ed.target).bind("keypress", function (event) {
//                    if (event.keyCode == 13) {
//                        if ($('#list').datagrid('validateRow', editIndex)) {
//                            $('#list').datagrid('endEdit', editIndex);
//                            editIndex = undefined;
//                            return true;
//                        }
//                    }
//                })
//            }
//            if (field == "sGZMC") {
//                var rowData = $("#list").datagrid("getSelected");
//                if (rowData.iGZBH) {
//                    //curEditcell = ed.target;
//                    //SelectSPSB("", "", "", false);
//                    var conData = new Object();
//                    //conData["SHDM"] = $("#HF_SHDM").val();
//                    //conData["SearchEnable"] = true;
//                    conData["Mode"] = "MBJZGZ";
//                    SelectDYDGZ("", "", "", true, conData);
//                }
//            }
//        }
//    }
//}
