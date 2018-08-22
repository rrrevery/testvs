vUrl = "../HYKGL.ashx";
var iMDID, iYHQID, dJSRQ, sSKTNO, iSEARCHMODE = 0;

function InitGrid() {
    vColumnNames = ["MDID", "门店名称", "消费金额", "退卡金额", "调整金额", '优惠券名称', "YHQID", '结束日期', ];
    vColumnModel = [
            { name: 'iMDID', hidden: true },
            { name: 'sMDMC', width: 70 },
            { name: 'fXFJE', width: 70 },
            { name: 'fTKJE', width: 70 },
            { name: 'fTZJE', width: 70 },
            { name: 'sYHQMC', width: 70, },
            { name: 'iYHQID',hidden:true},
			{ name: 'dJSRQ', width: 80, },
            
    ];

    vMDKTXFColumnNames = ["MDID", '门店名称', '款台号',  'HYID', '小票号', '消费金额', '积分', '特定倍数积分', '收款员代码', '陪购员'];
    vMDKTXFColumnModel = [
            { name: 'iMDID', hidden: true },
            { name: 'sMDMC', width: 100, },
            { name: 'sSKTNO', width: 70, },
            { name: 'iHYID', hidden: true },
            { name: 'iJLBH', width: 70, },
            { name: 'fJE', width: 70, },
            { name: 'fJF', width: 70, },
            { name: 'fJFBS', width: 70, },
            { name: 'sSKYDM', hidden: true },
            { name: 'iPGRYID', width: 70, },
    ];
    vMDKTSKYXFColumnNames = ['款台', '收款员代码', '消费金额', '退卡金额', '调整金额',];
    vMDKTSKYXFColumnModel = [
           { name: 'sSKTNO', width: 70, },
           { name: 'sSKYDM', width: 70, },
           { name: 'fXFJE', width: 70, },
           { name: 'fTKJE', width: 70, },
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
    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });

    $("#list1").datagrid({
        onClickRow: function (rowIndex, rowData) {
            iMDID = rowData.iMDID;
            iYHQID = rowData.iYHQID;
            dJSRQ = rowData.dJSRQ;
            sSKTNO = rowData.sSKTNO;
            iSEARCHMODE = 2;
            SearchData(undefined, undefined, undefined, undefined, 'list2');
        },
    });
    RefreshButtonSep();
});

function OnClickRow(rowIndex, rowData) {
    $('#list2').datagrid("loadData", { total: 0, rows: [] });
    iMDID = rowData.iMDID;
    iYHQID = rowData.iYHQID;
    dJSRQ = rowData.dJSRQ;
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
        MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    }
    MakeSrchCondition(arrayObj, "TB_XFRQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XFRQ2", "dRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function AddCustomerCondition(Obj) {
    Obj.iMDID = iMDID;
    Obj.iYHQID = iYHQID;
    Obj.dJSRQ = dJSRQ;
    Obj.sSKTNO = sSKTNO;
    Obj.iSEARCHMODE = iSEARCHMODE;
}


