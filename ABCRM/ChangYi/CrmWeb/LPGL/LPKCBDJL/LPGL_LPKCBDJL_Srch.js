vUrl = "../LPGL.ashx";
vCaption = "礼品库存变动明细";

function InitGrid() {
    vColumnNames = ['变动单号', '礼品名称', '礼品代码', '礼品单价', '变动数量', '库存数量', '保管地点', '处理类型', "iCLLX", "记录编号", "操作员", "处理时间"];
    vColumnModel = [
            { name: 'iJYBH' },
            { name: 'sLPMC', },
            { name: 'sLPDM', },
            { name: 'fLPDJ',},
            { name: 'fBDSL', },
            { name: 'fKCSL', },
            { name: 'sBGDDMC', },
            { name: 'sCLLXSTR', },
            { name: 'iCLLX', hidden: true },
            { name: 'iJLBH' },
            { name: 'sDJRMC', },
            { name: 'dCLSJ',width:150 },
    ]
};

$(document).ready(function () {

    $("#B_Insert").hide();
    $("#B_Update").hide();

    BFButtonClick("TB_BGDDMC", function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });

    BFButtonClick("TB_LPMC", function () {
        SelectLP("TB_LPMC", "HF_LPID", "zHF_LPID", false);
    });
    RefreshButtonSep();
});

function DBClickRow(rowIndex, rowData) {
    switch (parseInt(rowData.iCLLX)) {
        case 0:
            MakeNewTab("CrmWeb/LPGL/LPJHD/LPGL_LPJHD.aspx?iDJLX=0&jlbh=" + rowData.iJLBH, "礼品进货单", '<%=CM_LPGL_JFFHLPJHD%>');
            break;
        case 1:
        case 2:
            MakeNewTab("CrmWeb/LPGL/LPLQD/LPGL_LPLQD.aspx?jlbh=" + rowData.iJLBH, "礼品领取单", '<%=CM_LPGL_JFFHLPLQD%>');
            break;
        case 3:
            MakeNewTab("CrmWeb/LPGL/LPJHD/LPGL_LPJHD.aspx?iDJLX=1&jlbh=" + rowData.iJLBH, "礼品退货单", '<%=CM_LPGL_JFFHLPTHD%>');
            break;
        case 4:
            MakeNewTab("CrmWeb/LPGL/LPFF/LPGL_RCLPFF.aspx?&jlbh=" + rowData.iJLBH, "礼品发放单", '<%=CM_LPGL_RCLPFF%>');
            break;
        case 5:
            MakeNewTab("CrmWeb/LPGL/LPZFCL/LPGL_LPZFCL.aspx?jlbh=" + rowData.iJLBH, "礼品报废单", '<%=CM_LPGL_LPBFCL%>');
            break;
        case 6:
            MakeNewTab("CrmWeb/LPGL/LPPDCL/LPGL_LPPDCL.aspx?jlbh=" + rowData.iJLBH, "礼品盘点单", '<%=CM_LPGL_LPPDCL%>');
            break
        default:
            ShowMessage("请选择数据", 3);
            break;

    }
};

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_LPID", "iLPID", "in", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", true);
    MakeSrchCondition(arrayObj, "DDL_CLLX", "iCLLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dCLSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dCLSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};