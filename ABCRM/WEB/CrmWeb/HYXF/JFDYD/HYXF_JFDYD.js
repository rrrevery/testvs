vUrl = "../HYXF.ashx";

function InitGrid() {
    vColumnNames = ["优先顺序", "规则号", "分值", "积分倍数", "处理标记", "处理对象", "分单编号", "iCLFS_BM", "iCLFS_SPFL", "iCLFS_SPSB", "iCLFS_HT", "iCLFS_SP", "iCLFS_HYZ", ];
    vColumnModel = [
          { name: "iXH", },
          { name: "iGZBH", },
          { name: "fFZ", width: 60, editable: true, editor: 'text' },
          { name: "iBS", width: 60, editable: true, editor: 'text' },
          { name: "iBJ_CJ", width: 50, editor: { type: 'checkbox', options: { on: '√', off: 'x' } } },
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
    //$(".tabs-header").width($(".tabs-header").width() + 1);
});

function AddNewFDPanel(tab_index, rowIndex, tp_max) {
    $("#list").datagrid("appendRow", {
        iINX: tab_index,
        iXH: rowIndex + 1,
        iBS: 1,
        iBJ_CJ: '√',
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
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("请选择卡类型");
        return false;
    }
    var fdRows = itemFD;
    for (var i = 0; i < fdRows.length; i++) {
        if (fdRows[i].dKSRQ == "" || fdRows[i].dJSRQ == "") {
            ShowMessage("分单日期不能为空");
            return false;
        }
    }
    if ($("#TB_FDRQ1").val() == "" || $("#TB_FDRQ2").val() == "") {
        ShowMessage("分单日期不能为空");
        return false;
    }

    var rows = itemGZSD; //$("#list").datagrid("getData").rows;
    for (var i = 0; i < rows.length; i++) {
        if (rows[i].sCLDX == undefined || rows[i].sCLDX == "") {
            ShowMessage("请选择正确的处理对象", 3);
            return false;
        }
    }

    var CurrentRows = $("#list").datagrid("getData").rows;
    for (var i = 0; i < CurrentRows.length; i++) {
        if (CurrentRows[i].sCLDX == undefined || CurrentRows[i].sCLDX == "") {
            ShowMessage("请选择正确的处理对象", 3);
            return false;
        }
    }


    return true;
}

function SaveData(Obj) {
    var arrayKLX = new Array();
    if ($("#HF_HYKTYPE").val() != "") {
        arrayKLX = $("#HF_HYKTYPE").val().split(",");
        for (var i = 0; i < arrayKLX.length; i++) {
            arrayKLX[i] = { iHYKTYPE: arrayKLX[i] };
        }
    }
    Obj.itemKLX = arrayKLX;
    var tab = $('#tt').tabs('getSelected');
    var tab_index = $('#tt').tabs('getTabIndex', tab);
    removeRecord(tab_index);
    var curTabData = $("#list").datagrid("getData").rows;
    addRecord(curTabData);
    Obj.itemFD = itemFD;
    for (var i = 0; i < itemGZSD.length; i++) {
        if (itemGZSD[i].iBJ_CJ == "√")
            itemGZSD[i].iBJ_CJ = 1;
        else
            itemGZSD[i].iBJ_CJ = 0;
    }
    Obj.itemGZSD = itemGZSD;
    Obj.itemGZITEM = itemGZITEM;
    Obj.iHYKTYPE = 0;//$("#HF_HYKTYPE").val();
    Obj.iBJ_JFBS = $("#CK_BJ_JFBS").prop("checked") ? "1" : "0";
    Obj.iYXCLBJ = $("#CK_YX")[0].checked ? 1 : 0;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    if (Obj.itemKLX.length) {
        var tp_kname = [];
        var tp_kvalue = [];
        for (var i = 0; i < Obj.itemKLX.length; i++) {
            tp_kname.push(Obj.itemKLX[i].sHYKNAME);
            tp_kvalue.push(Obj.itemKLX[i].iHYKTYPE);
        }
        $("#HF_HYKTYPE").val(tp_kvalue.join(","));
        $("#TB_HYKNAME").val(tp_kname.join(";"));
    }

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
    for (var i = 0; i < Obj.itemGZSD.length; i++) {
        if (Obj.itemGZSD[i].iBJ_CJ == 1)
            Obj.itemGZSD[i].iBJ_CJ = "√";
        else
            Obj.itemGZSD[i].iBJ_CJ = "x";
    }
    $("#list").datagrid("loadData", Obj.itemGZSD.filter(function (item) { return item.iINX == 0; }));
    var curTabInfo = Obj.itemFD.filter(function (item) {
        return item.iINX == 0;
    })
    if (curTabInfo.length) {
        $("#TB_FDRQ1").val(curTabInfo[0].dKSRQ);
        $("#TB_FDRQ2").val(curTabInfo[0].dJSRQ);
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
    $("#CK_BJ_JFBS").prop("checked", Obj.iBJ_JFBS == "1");
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
    $("#CK_YX")[0].checked = Obj.iYXCLBJ == 1;
}

