vUrl = "../HYXF.ashx";
gzdialog = "FQGZ";

function InitGrid() {
    vColumnNames = ["优先顺序", "规则号", "发放规则方案", "发放规则ID", "处理标记", "处理对象", "分单编号", "iCLFS_BM", "iCLFS_SPFL", "iCLFS_SPSB", "iCLFS_HT", "iCLFS_SP", "iCLFS_HYZ", ];//"分值",
    vColumnModel = [
          { name: "iXH", },
          { name: "iGZBH", },
       // { name: "fFZ", width: 60, editable: true, editor: 'text' },
          { name: "sGZMC", width: 60 },
          { name: "iGZID", width: 60, hidden: true },
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
    $("#YHQPanel").show();
    if (lx == 1&& djlx == 0) {
        BFButtonClick("TB_YHQMC", function () {
            var conData = new Object();
            conData.iBJ_TS = 1;
            SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false, conData);
        });
    }
    else {

        BFButtonClick("TB_YHQMC", function () {
            var conData = new Object();
            conData.iBJ_TS = 0;
            SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false, conData);
        });
    }

    //$(".tabs-header").width($(".tabs-header").width() + 1);
});

function AddNewFDPanel(tab_index, rowIndex, tp_max) {
    $("#list").datagrid("appendRow", {
        iINX: tab_index,
        iXH: rowIndex + 1,
        sSYGZMC: "",
        iSYGZID: 0,
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
    if ($("#HF_CXID").val() == "") {
        ShowMessage("请选择促销活动");
        return false;
    }
    if ($("[name='LJFS']:checked").val() == "") {
        ShowMessage("请选择累计方式");
        return false;
    }

    var rows = itemGZSD; //$("#list").datagrid("getData").rows;
    for (var i = 0; i < rows.length; i++) {
        if (parseFloat(rows[i].iGZID) <= 0 || rows[i].iGZID == undefined || rows[i].iGZID == "") {
            ShowMessage("请选择正确的优惠券发放规则", 3);
            return false;
        }
        if (rows[i].sCLDX == undefined || rows[i].sCLDX == "") {
            ShowMessage("请选择正确的处理对象", 3);
            return false;
        }
    }

    var CurrentRows = $("#list").datagrid("getData").rows;
    for (var i = 0; i < CurrentRows.length; i++) {
        if (parseFloat(CurrentRows[i].iGZID) <= 0 || CurrentRows[i].iGZID == undefined || CurrentRows[i].iGZID == "") {
            ShowMessage("请选择正确的优惠券发放规则", 3);
            return false;
        }
        if (CurrentRows[i].sCLDX == undefined || CurrentRows[i].sCLDX == "") {
            ShowMessage("请选择正确的处理对象", 3);
            return false;
        }
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
    Obj.iCXID = $("#HF_CXID").val();
    Obj.iYHQID = $("#HF_YHQID").val();
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
    Obj.iBJ_TD = $("#CK_BJ_TD").prop("checked") ? "1" : "0";
    Obj.iYXCLBJ = $("#CK_YX")[0].checked ? 1 : 0;
    Obj.iLX = lx;
    Obj.iDJLX = djlx;
    Obj.iXFLJFQFS = $("[name='LJFS']:checked").val();
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
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("[name='LJFS'][value='" + Obj.iXFLJFQFS + "']").prop("checked", true);
}

function CheckCXHDYHQ(cxid, yhqid) {
    var obj = GetYHQDEF_CXHD(cxid, yhqid);
    if (obj.length == 0) {
        ShowErrMessage("请先定义促销活动优惠券相关内容");
        ClearDialogSelect("TB_CXZT", "HF_CXID", "zHF_CXID");
    }
    else
        $("#TB_YHQSYJSRQ").text(obj[0].dYHQSYJSRQ);
}
