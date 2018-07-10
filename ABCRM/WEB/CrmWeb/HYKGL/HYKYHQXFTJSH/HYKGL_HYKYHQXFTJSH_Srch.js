vUrl = "../HYKGL.ashx";
var iMDID, iYHQID, iHYKTYPE, sSHDM, iSEARCHMODE = 0;

function InitGrid() {
    vColumnNames = ["SHDM", "商户名称", "卡类型名称", "卡类型", '优惠券名称', "YHQID", "消费金额", "调整金额"];
    vColumnModel = [
            { name: 'sSHDM', hidden: true },
            { name: 'sSHMC', width: 70 },
            { name: 'sHYKNAME', width: 70 },
            { name: 'iHYKTYPE', width: 70 },
            { name: 'sYHQMC', width: 70, },
            { name: 'iYHQID', hidden: true },
            { name: 'fXFJE', width: 70 },
            { name: 'fTZJE', width: 70 },
    ];
    vMDKTXFColumnNames = ["SHDM", "商户名称",'卡名称', '卡类型', "门店名称", "MDID", '优惠券名称', 'YHQID', '消费金额', '调整金额', ];
    vMDKTXFColumnModel = [
            { name: 'sSHDM', hidden: true },
            { name: 'sSHMC', width: 70 },
            { name: 'sHYKNAME', width: 70 },
            { name: 'iHYKTYPE', width: 70 },
            { name: 'sMDMC', width: 70 },
            { name: 'iMDID', hidden: true },
            { name: 'sYHQMC', width: 70, },
            { name: 'iYHQID', hidden: true },
            { name: 'fXFJE', width: 70, },
            { name: 'fTZJE', width: 70, },
    ];
    vMDKTSKYXFColumnNames = ['YHQID', '优惠券名称', 'SKYDM', '收款员', '消费金额', '调整金额', '余额', '摘要'];
    vMDKTSKYXFColumnModel = [
           { name: 'iYHQID', hidden: true },
           { name: 'sYHQMC', width: 70, },
           { name: 'sSKYDM', hidden: true },
           { name: 'sSKYMC', width: 70, },
           { name: 'fXFJE', width: 70, },
           { name: 'fTZJE', width: 70, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    DrawGrid("list1", vMDKTXFColumnNames, vMDKTXFColumnModel);
    DrawGrid("list2", vMDKTSKYXFColumnNames, vMDKTSKYXFColumnModel);

    BFButtonClick("TB_YHQMC", function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", false);
    });
    BFButtonClick("TB_SHMC", function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", false);
    });
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });
    $("#list1").datagrid({
        onClickRow: function (rowIndex, rowData) {
            iMDID = rowData.iMDID;
            iYHQID = rowData.iYHQID;
            iHYKTYPE = rowData.iHYKTYPE;
            sSHDM = rowData.sSHDM;
            iSEARCHMODE = 2;
            SearchData(undefined, undefined, undefined, undefined, 'list2');
        },
    });

    RefreshButtonSep();
});

function OnClickRow(rowIndex, rowData) {
    $('#list2').datagrid("loadData", { total: 0, rows: [] });
    sSHDM = rowData.sSHDM;
    iYHQID = rowData.iYHQID;
    iHYKTYPE = rowData.iHYKTYPE;
    iSEARCHMODE = 1;
    SearchData(undefined, undefined, undefined, undefined, 'list1');

}
function SearchClick() {
    if (!IsValidSearch())
        return;
    iSEARCHMODE = 0;
    SearchData();
    SetControlBaseState();
};
function MakeSearchCondition() {
    var arrayObj = new Array();
    if (iSEARCHMODE == 0) {
        MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "in", false);
        MakeSrchCondition(arrayObj, "HF_SHDM", "sSHDM", "in", false);
        MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    }
    MakeSrchCondition(arrayObj, "TB_XFRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XFRQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.iMDID = iMDID;
    Obj.iYHQID = iYHQID;
    Obj.iHYKTYPE = iHYKTYPE;
    Obj.sSHDM = sSHDM;
    Obj.iSEARCHMODE = iSEARCHMODE;
}
