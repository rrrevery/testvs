//vPageMsgID = CM_HYKGL_JFKPLFF;
vUrl = "../MZKGL.ashx";
var sksl = 0;
var vSKFSColumnNames;
var vSKFSColumnModel;
var vZKColumnNames;
var vZKColumnModel;
var vZJFColumnNames;
var vZJFColumnModel;
var vDBConnName  = "CRMDBMZK";

function InitGrid() {
    vColumnNames = ['开始卡号', '结束卡号', 'iHYKTYPE', '卡类型', '面值金额', '数量', ];
    vColumnModel = [
            { name: 'sCZKHM_BEGIN', index: 'sCZKHM_BEGIN', width: 100, },
            { name: 'sCZKHM_END', index: 'sCZKHM_END', width: 100, },
            { name: 'iHYKTYPE', index: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', index: 'iHYKNAME', width: 50, hidden: true, },
            { name: 'fMZJE', index: 'fMZJE', width: 20, align: "right", hidden: true, },
            { name: 'iSKSL', index: 'iSKSL', width: 20, align: "right", },
    ];
    vSKFSColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称" ,"收款金额", "交易号"];
    vSKFSColumnModel = [
          { name: "iZFFSID", width: 150 },
          { name: "sZFFSDM",hidden: true },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text',hidden:true},

    ];
    vZKColumnNames = ['卡号', '优惠券', '优惠券ID', '有效期', '赠券金额'];
    vZKColumnModel = [
            { name: 'sHYK_NO', index: 'sHYK_NO', width: 100, },
            { name: 'sYHQMC', index: 'sYHQMC', width: 100, },
            { name: 'iYHQLX', index: 'iYHQLX', hidden: true, },
            { name: 'iYXQTS', index: 'iYXQTS', width: 50, },
            { name: 'fZKJE', index: 'fZKJE', width: 50, align: 'right', },
    ];
    vZJFColumnNames = ['卡号',  '赠送积分'];
    vZJFColumnModel = [
            { name: 'sHYK_NO', index: 'sHYK_NO', width: 100, },
            { name: 'fZSJF', index: 'fZSJF', width: 50, },
    ];

};

$(document).ready(function () {
    $("#btnout_B_Start").show();
    $("#B_Start").show();
    DrawGrid("GV_SKFS", vSKFSColumnNames, vSKFSColumnModel, true);
    DrawGrid("GV_ZK", vZKColumnNames, vZKColumnModel, true);
    DrawGrid("GV_ZJF", vZJFColumnNames, vZJFColumnModel, true);
    FillBGDDTreeSK("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME",2);//会员卡建卡
    
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_KHMC", "HF_ZXR");
    });
    
    $("#TB_KHMC").click(function () {
        SelectKH("TB_KHMC", "HF_KHID", "zHF_KHID",false);
    })

    $("#AddItem").click(function () {

        if ($("#HF_BGDDDM").val() == "") {
            art.dialog({ lock: true, content: "请选择保管地点" });
            return;
        }
        if ($("#TB_CZKHM_BEGIN").val() == "") {
            art.dialog({ lock: true, content: "请输入开始卡号" });
            return;
        }
        if ($("#TB_CZKHM_END").val() == "") {
            art.dialog({ lock: true, content: "请输入结束卡号" });
            return;
        }
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型");
            return;
        }
        if ($("#TB_CZKHM_BEGIN").val().length != $("#TB_CZKHM_END").val().length) {
            ShowMessage("开始卡号与结束卡号长度不一致");
            return;
        }
        var str1 = GetMZKKCKXXData($("#TB_CZKHM_BEGIN").val(), "", vDBConnName); 
        if (str1 == "" || str1 == undefined) {
            ShowMessage("没有找到开始卡号的库存卡");
            return;
        }
        var obj1 = JSON.parse(str1);
        if (obj1.sCZKHM == "") {
            ShowMessage("没有找到开始卡号的库存卡");
            return;
        }
        if (obj1.iSTATUS != 1) {
            ShowMessage("开始卡号的状态错误");
            return;
        }
        //if (obj1.iSKJLBH > 0) {
        //    ShowMessage("开始卡号已经发售");
        //    return;
        //}
        var str2 = GetMZKKCKXXData($("#TB_CZKHM_END").val(), "", vDBConnName);
        if (str2 == "") {
            ShowMessage("没有找到结束卡号的库存卡");
            return;
        }
        var obj2 = JSON.parse(str2);
        if (obj2.iSTATUS != 1) {
            ShowMessage("结束卡号的状态错误");
            return;
        }
        if (obj1.iHYKTYPE != obj2.iHYKTYPE) {
            art.dialog({ lock: true, content: "开始卡类型与结束卡类型不一致" });
            return;
        }
        //if (obj2.iSKJLBH > 0) {
        //    ShowMessage("结束卡号已经发售");
        //    return;
        //}
        CalcKD($("#TB_CZKHM_BEGIN").val(), $("#TB_CZKHM_END").val(), $("#HF_HYKTYPE").val());
        
    });


    $("#DelItem").click(function () {
        var rows = $('#list').datagrid("getSelections");
        for (var j = 0; j < rows.length; j++) {
            $("#LB_SK_YSZS").text(parseInt($("#LB_SK_YSZS").text()) - parseInt(rows[j].iSKSL));
            $("#LB_SK_YSJE").text(parseInt($("#LB_SK_YSJE").text()) - (parseInt(rows[j].fMZJE) * parseInt(rows[j].iSKSL)));
        }      
        DeleteRows("list");
        GetZQ();
    });

    //$("#B_SK_Search").click(function () {
    //    $("#B_SK_Search").prop("disabled", true);
    //    var tp_kskh = $("#TB_CZKHM_BEGIN").val();
    //    var tp_ksl = $("#TB_CZKHM_END").val();
    //    var tp_bgdddm = $("#HF_BGDDDM").val();  
    //    var tp_jlbh = $("#TB_JLBH").val();
    //    var tp_skje = $("#LB_SK_YSJE").text();
    //    var hyktype = $("#HF_HYKTYPE").val();
    //    if (tp_kskh == null) { alert("开始卡号不能为空！"); return false; }
    //    if (tp_ksl == null) { alert("卡数量不能为空！"); return false; }
    //    if (tp_bgdddm == null) { alert("保管地点不能为空！"); return false; }
    //    if (hyktype == null) { alert("卡类型不能为空！"); return false; }

    //    var Obj = new Object();
    //    var tp_SKKD = new Array(); 
    //    $.ajax({
    //        type: 'post',
    //        async: false,
    //        url: "../../CrmLib/CrmLib.ashx?func=GetFSSKMX",
    //        dataType: "Text",       
    //        data: { KSKH: tp_kskh, KSL: tp_ksl, BGDDDM: tp_bgdddm, JLBH: tp_jlbh, SKJE: tp_skje, SKKD: JSON.stringify(Obj), BJCZK: 1, BJZSK: 0, LoginRYID: iDJR, LoginRYMC: sDJRMC, HYKTYPE: hyktype },
    //        success: function (data) {
    //            try {         
    //                var Obj = JSON.parse(data);
    //                $("#LB_SK_YSJE").text(Obj.LB_YSJE);
    //                //$("#LB_SK_YSZS").text(Obj.LB_SKXX_SKZS);
    //                $("#LB_SK_ZK").html(Obj.ZKJE);
    //                $("#LB_ZKLPBL").html(Obj.ZKLPBL);               
    //                $("#LB_SK_JF").html(Obj.ZKLPJE);
    //                $("#HF_ZKYHQMC").val(Obj.ZKYHQMC);
    //                $("#HF_ZKYHQLX").val(Obj.ZKYHQLX);
    //                $("#HF_ZKYHQTS").val(Obj.ZKYHQTS);
    //                $("#list").jqGrid("clearGridData");
    //                for (i = 0; i < Obj.SKKD.length; i++) {
    //                    $("#list").jqGrid('addRowData', i + 1,
    //                        {
    //                            CZKHM_BEGIN: Obj.SKKD[i].iRANDOM_LEN != 0 ? Obj.SKKD[i].CZKHM_BEGIN.substr(0, Obj.SKKD[i].CZKHM_BEGIN.length - 2) : Obj.SKKD[i].CZKHM_BEGIN,
    //                            CZKHM_END: Obj.SKKD[i].iRANDOM_LEN != 0 ? Obj.SKKD[i].CZKHM_END.substr(0, Obj.SKKD[i].CZKHM_END.length - 2) : Obj.SKKD[i].CZKHM_END,
    //                            HYKTYPE: Obj.SKKD[i].HYKTYPE,
    //                            HYKNAME: Obj.SKKD[i].HYKNAME,
    //                            MZJE:Obj.SKKD[i].MZJE,
    //                            SKSL: Obj.SKKD[i].SKSL,
    //                            iRANDOM_LEN:Obj.SKKD[i].iRANDOM_LEN
    //                        }
    //                        )
    //                    checkBGDDDM();
    //                }
    //                $("#TB_SK_KSKH").val("");
    //                $("#TB_SK_KSL").val("");    
    //            } catch (e) {
    //                alert(data);
    //            }
    //        }
    //    });




    //});

    $("#B_ZK_Search").click(function () {
        var listrow = $("#list").datagrid("getData").rows;
        if (listrow.length <= 0)
        {
            ShowMessage("请先添加面值卡", 3);
            return;
        }
        if ($("#TB_HYKNO_ZQ").val() != "") {
            var str = GetHYXXData(0, $("#TB_HYKNO_ZQ").val(), "CRMDB");
            if (str == "null" || str == "")
            {
                ShowMessage("没有找到卡号", 3);
                $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
                return;
            }
            else {
                var Obj = JSON.parse(str);
                if (Obj.iSTATUS < 0)
                {
                    ShowMessage("卡号状态错误", 3);
                    $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
                    return;
                }
                $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
                $('#GV_ZK').datagrid('appendRow', {
                    sHYK_NO: Obj.sHYK_NO,
                    sYHQMC: $("#HF_ZKYHQMC").val(),
                    iYHQLX: $("#HF_ZKYHQLX").val(),
                    iYXQTS: $("#HF_ZKYHQTS").val(),
                    fZKJE: $("#LB_SK_ZK").text(),
                });
                $("#LB_ZK_YSJE").text($("#LB_SK_ZK").text());
            }
            
          
        }


    });


    $("#B_ZF_Search").click(function () {
        var listrow = $("#list").datagrid("getData").rows;
        if (listrow.length <= 0) {
            ShowMessage("请先添加面值卡", 3);
            return;
        }
        if ($("#TB_HYKNO_ZF").val() != "") {
            var str = GetHYXXData(0, $("#TB_HYKNO_ZF").val(), "CRMDB");
            if (str == "null" || str == "") {
                ShowMessage("没有找到卡号", 3);
                $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
                return;
            }
            else {
                var Obj = JSON.parse(str);
                if (Obj.iSTATUS < 0) {
                    ShowMessage("卡号状态错误", 3);
                    $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
                    return;
                }
                $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
                $('#GV_ZJF').datagrid('appendRow', {
                    sHYK_NO: Obj.sHYK_NO,
                    fZSJF: $("#LB_SK_JF").text(),
                });
                $("#LB_ZSJF").text($("#LB_SK_JF").text());
            }


        }


    });

    $("#B_ZK_Delete").click(function () {
        $("#LB_ZK_YSJE").text(0);
        DeleteRows("GV_ZK");
    });

    $("#B_ZF_Delete").click(function () {
        $("#LB_ZSJF").text(0);
        DeleteRows("GV_ZJF");
    });

    $("#R_ZFFS").click(function () {
        window.setTimeout(function () {
            SearchData();
        }, 1);

    });
});




function SearchData(page, rows, sort, order) {
    var vtp_Url = "../../CRMGL/CRMGL.ashx";
    var vPageMsgID_ZFFS = 5160003;
    var obj = MakeSearchJSON();
    page = page || $('#GV_SKFS').datagrid("options").pageNumber;
    rows = rows || $('#GV_SKFS').datagrid("options").pageSize;
    sort = sort || $('#GV_SKFS').datagrid("options").sortName;
    order = order || $('#GV_SKFS').datagrid("options").sortOrder;
    $('#GV_SKFS').datagrid("loading");
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
            $('#GV_SKFS').datagrid('loadData', JSON.parse(data), "json");
            $('#GV_SKFS').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}


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
    //MakeSrchCondition2(arrayObj, "1, 2", "iBJ_KF", "in", false);
    //MakeSrchCondition2(arrayObj, "0, 2", "iBJ_XSMD", "in", false);
    return arrayObj;
}

function CalcKD(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE) {

    var rowList = $("#list").datagrid("getData").rows;
    for (i = 0; i < rowList.length ; i++) {
        var rowData = rowList[i];
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的开始卡号，不能添加");
            return;
        }
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的结束卡号，不能添加");
            return;
        }
    }
    var str = GetMZKKCKKD_FS(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE, $("#HF_BGDDDM").val(), vDBConnName, 1);
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡号段", 3);     
        return;
    }
    var data = JSON.parse(str);
    for (i = 0; i < data.SKKD.length; i++) {
        $('#list').datagrid('appendRow', {
            sCZKHM_BEGIN: data.SKKD[i].sCZKHM_BEGIN,
            sCZKHM_END: data.SKKD[i].sCZKHM_END,
            iHYKTYPE: data.SKKD[i].iHYKTYPE,
            sHYKNAME: data.SKKD[i].sHYKNAME,
            iSKSL: data.SKKD[i].iSKSL,
            fMZJE: data.SKKD[i].fMZJE,
        });
        if ($("#LB_SK_YSZS").text() == "")
            $("#LB_SK_YSZS").text("0");
        $("#LB_SK_YSZS").text(parseInt($("#LB_SK_YSZS").text()) + data.SKKD[i].iSKSL);
        if ($("#LB_SK_YSJE").text() == "")
            $("#LB_SK_YSJE").text("0");
        $("#LB_SK_YSJE").text(parseInt($("#LB_SK_YSJE").text()) + data.SKKD[i].fMZJE * data.SKKD[i].iSKSL);
    }
    GetZQ();
    $("#TB_CZKHM_BEGIN").val("");
    $("#TB_CZKHM_END").val("");
    //jsdata = "{'iSTATUS':" + 1 + ",'sBGDDDM':'" + $("#HF_BGDDDM").val() + "','iHYKTYPE':'" + iHYKTYPE + "','sCZKHM_BEGIN':'" + sCZKHM_BEGIN + "','sCZKHM_END':'" + sCZKHM_END + "','sDBConnName':'" + vDBConnName + "','iBJ_KCK':'" + 1 + "'}";
    //$.ajax({
    //    type: 'post',
    //    asycn: false,
    //    url: "../../CrmLib/CrmLib.ashx?func=GetMZKKCKKD_FS",
    //    dataType: "json",
    //    //postData: { sBGDDDM: $("#HF_BGDDDM_BC").val(), iSTATUS: 0, iHYKTYPE: iHYKTYPE, sCZKHM_Begin: sCZKHM_BEGIN, sCZKHM_End: sCZKHM_END, },
    //    data: { json: jsdata, titles: 'cecece' },
    //    success: function (data) {
    //        try {
    //            //var str = JSON.stringify(data);
    //            //var Obj = JSON.parse(str);
    //            //var rowNum1 = $("#list").getGridParam("reccount");
    //            for (i = 0; i < data.SKKD.length; i++) {
    //                    $('#list').datagrid('appendRow', {
    //                        sCZKHM_BEGIN: data.SKKD[i].sCZKHM_BEGIN,
    //                        sCZKHM_END: data.SKKD[i].sCZKHM_END,
    //                        iHYKTYPE: data.SKKD[i].iHYKTYPE,
    //                        sHYKNAME: data.SKKD[i].sHYKNAME,
    //                        iSKSL: data.SKKD[i].iSKSL,
    //                        fMZJE: data.SKKD[i].fMZJE,
    //                    });
    //                    if ($("#LB_SK_YSZS").text() == "")
    //                        $("#LB_SK_YSZS").text("0");
    //                    $("#LB_SK_YSZS").text(parseInt($("#LB_SK_YSZS").text()) + data.SKKD[i].iSKSL);
    //                    if ($("#LB_SK_YSJE").text() == "")
    //                        $("#LB_SK_YSJE").text("0");
    //                    $("#LB_SK_YSJE").text(parseInt($("#LB_SK_YSJE").text()) + data.SKKD[i].fMZJE * data.SKKD[i].iSKSL);
    //            }
    //            GetZQ();
    //            $("#TB_CZKHM_BEGIN").val("");
    //            $("#TB_CZKHM_END").val("");

    //        } catch (e) {
    //            alert(data);
    //        }
    //    }
    //});
}

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeHYKTYPE": $("#HF_HYKTYPE").val(treeNode.iHYKTYPE); break;
    }
}


function SaveData() {
    var a = 0;
    var b = 0;
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "") { Obj.iJLBH = "0"; }
    Obj.FS = 1;
    //Obj.SKSL = $("#LB_SK_YSZS").html();
    Obj.YSZE = $("#LB_SK_YSJE").text();
    Obj.ZKL = "0";//$("#LB_ZKL").html();
    Obj.ZKJE = $("#LB_SK_ZK").text();
    Obj.SSJE = "0";
    Obj.BGDDDM = $("#HF_BGDDDM").val();
    Obj.ZY = $("#TA_ZY").val();
    Obj.ZSJE = $("#LB_SK_ZK").text();
    Obj.KHID = ($("#HF_KHID").val() == "") ? "0" : $("#HF_KHID").val();
    Obj.LXR = "0";
    Obj.YWY = ($("#HF_YWY").val() == "") ? "0" : $("#HF_YWY").val();


    var lst = new Array();
    var listrow = $("#list").datagrid("getData").rows;
    for (var i = 0; i < listrow.length ; i++) {
        var rowData = listrow[i];
        lst.push(rowData);
        a = Number(a) + Number(rowData.iSKSL);
    }


    Obj.SKSL = a;
    Obj.SKKD = lst;
    var SJZQJE = 0;
    var lst_zk = new Array();
    var rowIDs = $("#GV_ZK").datagrid("getData").rows;
    for (i = 0; i < rowIDs.length ; i++) {
        var rowData = rowIDs[i];
        SJZQJE = parseFloat(SJZQJE) + parseFloat(rowData.fZKJE);
        lst_zk.push(rowData);
    }
    Obj.ZQMX = lst_zk;
    Obj.SJZSJE = SJZQJE || 0;
    //var lst_skfs = new Array();
    //lst_skfs.splice();
    //for (i = 0; i < $("#GV_SKFS").getGridParam("reccount") ; i++) {
    //    var tp_skje = $("#GV_SKFS").getRowData(i + 1).JE;
    //    if (tp_skje != 0 && tp_skje != null && tp_skje != "") {
    //        var rowData = $("#GV_SKFS").getRowData(i + 1);
    //        lst_skfs.push(rowData);
    //    }
    //}

    var lst_skfs = new Array();
    var Rows = $("#GV_SKFS").datagrid("getData").rows;
    for (var i = 0; i < Rows.length; i++) {
        var rowData = Rows[i]
        if (parseFloat(rowData.fJE) > 0) {
            lst_skfs.push(rowData);
            b = Number(b) + Number(rowData.fJE);
        }
    }
    Obj.ZFFS = lst_skfs;
    Obj.SSJE = b;
    var lst_jf = new Array();
    var SJZSJF = 0;
    var rowJFIDs = $("#GV_ZJF").datagrid("getData").rows;
    for (var i = 0; i < rowJFIDs.length; i++) {
        var rowData = rowJFIDs[i];
        SJZSJF = parseFloat(SJZSJF) + parseFloat(rowData.fZSJF);
        lst_jf.push(rowData);
    }
    Obj.SJZSJF = SJZSJF||0;
    Obj.ZFMX = lst_jf;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    Obj.SXJLBH = $("#LB_SX_JLBH").text();
    Obj.SXJE = $("#LB_SX_JE").text();
    Obj.KHZYID = $("#HF_KHZYID").val();

    Obj.ZKSL = $("#LB_ZK_YSZS").text();
    Obj.ZKLPBL = $("#LB_ZKLPBL").text();
    Obj.ZKLPSL = $("#LB_LP_YSZS").text();
    Obj.ZKLPJE = $("#LB_SK_LP").text();
    Obj.ZKLPSJE = $("#LB_LP_YSJE").text();
    Obj.ZSJF = $("#LB_SK_JF").text();
    Obj.sDBConnName = "CRMDBMZK";
    //
    if ($("#BJ_CZLX2").is(':checked') == true) {
        Obj.BJ_CZ0 = "2";
    }
    if ($("#BJ_CZLX0").is(':checked') == true) {
        Obj.BJ_CZ0 = "0";
    }
    return Obj;
}

function IsValidData() {

    return true;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...
    $("#LB_SK_YSJE").text(Obj.YSZE);
    $("#LB_SK_YSZS").text(Obj.SKSL);
    $("#LB_ZKL").html(Obj.ZKL);
    $("#LB_SK_ZK").text(Obj.ZKJE);
    $("#LB_SK_JF").text(Obj.ZSJF);
    $("#LB_ZK_YSJE").html(Obj.SJZSJE);
    $("#LB_ZSJF").text(Obj.SJZSJF);

    $("#HF_BGDDDM").val(Obj.BGDDDM);
    $("#TB_BGDDMC").val(Obj.BGDDMC);
    $("#TA_ZY").val(Obj.ZY);
    $("#TB_DZKFJE").val(Obj.DZKFJE);
    //$("#HF_LXR").val(Obj.iLXR);
    //$("#TB_LXRMC").val(Obj.sLXRMC);
    $("#TB_LXRMC").val(Obj.YWY);
    $("#HF_STATUS").val(Obj.STATUS);

    //$("#list").jqGrid("clearGridData");

    $('#list').datagrid('loadData', Obj.SKKD, "json");
    $('#list').datagrid("loaded");
    var skje = 0;
    for (i = 0; i < Obj.SKKD.length ; i++) {
        skje += parseFloat(Obj.SKKD[i].fMZJE) * parseFloat(Obj.SKKD[i].iSKSL);
    }
    //$("#GV_ZK").jqGrid("clearGridData");
    //for (i = 0; i < Obj.ZQMX.length ; i++) {
    //    $("#GV_ZK").addRowData(Obj.ZQMX[i].sHYK_NO, Obj.ZQMX[i]);
    //}
    $('#GV_ZK').datagrid('loadData', Obj.ZQMX, "json");
    $('#GV_ZK').datagrid("loaded");

    $('#GV_SKFS').datagrid('loadData', Obj.ZFFS, "json");
    $('#GV_SKFS').datagrid("loaded");

    $('#GV_ZJF').datagrid('loadData', Obj.ZFMX, "json");
    $('#GV_ZJF').datagrid("loaded");

    //$("#GV_SKFS").jqGrid("clearGridData");
    //for (i = 0; i < Obj.ZFFS.length ; i++) {
    //    $("#GV_SKFS").addRowData(i + 1, Obj.ZFFS[i]);
    //    if (Obj.ZFFS[i] == "12") {
    //        $("#GV_SKFS").setGridParam({ cellEdit: false });
    //    }
    //}
    //$("#GV_ZJF").jqGrid("clearGridData");
    //for (i = 0; i < Obj.ZFMX.length ; i++) {
    //    $("#GV_ZJF").addRowData(Obj.ZFMX[i].sHYK_NO, Obj.ZFMX[i]);
    //}


    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.ZXRMC);
    $("#HF_ZXR").val(Obj.ZXR);
    $("#LB_ZXRQ").text(Obj.ZXRQ);
    //
    $("#LB_SX_JLBH").html(Obj.SXJLBH);
    $("#LB_SX_JE").html(Obj.SXJE);
    $("#HF_KHID").val(Obj.KHID);
    $("#TB_KHMC").val(Obj.KHMC);
    $("#LB_LXRXM").text(Obj.LXRXM);
    $("#LB_LXRSJ").text(Obj.LXRSJ);
    $("#LB_ZKLPBL").html(Obj.ZKLPBL);
    $("#LB_LP_YSZS").html(Obj.ZKLPSL);
    $("#LB_SK_LP").html(Obj.ZKLPJE);
    $("#LB_LP_YSJE").html(Obj.ZKLPSJE);

    $("#HF_ZKYHQMC").val(Obj.findkdxx.ZKYHQMC);
    $("#HF_ZKYHQLX").val(Obj.findkdxx.ZKYHQLX);
    $("#HF_ZKYHQTS").val(Obj.findkdxx.ZKYHQTS);

    if (Obj.BJ_CZ0 == "2") {
        $("#BJ_CZLX2").prop("checked", true);
        $("#BJ_CZLX0").prop("checked", false);
    } else {
        $("#BJ_CZLX0").prop("checked", true);
        $("#BJ_CZLX2").prop("checked", false);
    }
    $("#HF_YWY").val(Obj.YWY);
    $("#HF_STATUS").val(Obj.STATUS);   
}


function InsertClickCustom() {
    window.setTimeout(function () {
        $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
        $('#GV_SKFS').datagrid('loadData', { total: 0, rows: [] });
        $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
        SearchData();
    }, 100);
    $("#LB_SK_YSJE").text(0);
    $("#LB_SK_ZK").text(0);
    $("#LB_SK_JF").text(0);
    $("#LB_ZKLPBL").text(0);
    $("#LB_ZK_YSJE").text(0);
    $("#LB_ZSJF").text(0);
    $("#LB_SK_YSZS").text(0);
};
function MoseDialogCustomerReturn(dialogName, lstData, showField) {
    if (dialogName == "ListKH")
    {
        $("#LB_LXRXM").text(lstData[0].sLXRXM);
        $("#LB_LXRSJ").text(lstData[0].sLXRSJ);

    }
};
function GetZQ()
{
    var str = GetZQMX(parseInt($("#LB_SK_YSJE").text()));
    var zqmx = JSON.parse(str);
    $("#LB_SK_YSJE").text(zqmx.fYSJE);
    $("#LB_SK_ZK").text(zqmx.ZKJE);
    $("#LB_ZKLPBL").text(zqmx.ZKLPBL);
    $("#LB_SK_JF").text(zqmx.ZKLPJE);
    $("#HF_ZKYHQMC").val(zqmx.ZKYHQMC);
    $("#HF_ZKYHQLX").val(zqmx.ZKYHQLX);
    $("#HF_ZKYHQTS").val(zqmx.ZKYHQTS);
    $("#LB_ZSJF").text(0);
    $("#LB_ZK_YSJE").text(0);
    if ($("#GV_ZK").datagrid("getData").rows.length > 0)
    {
        $('#GV_ZK').datagrid('updateRow', {
            index: 0,
            row: {
                sYHQMC: $("#HF_ZKYHQMC").val(),
                iYHQLX: $("#HF_ZKYHQLX").val(),
                iYXQTS: $("#HF_ZKYHQTS").val(),
                fZKJE: $("#LB_SK_ZK").text(),
            }
        });
        $("#LB_ZK_YSJE").text($("#LB_SK_ZK").text());
    }
    if ($("#GV_ZJF").datagrid("getData").rows.length > 0)
    {
        $('#GV_ZJF').datagrid('updateRow', {
            index: 0,
            row: {
                fZSJF: $("#LB_SK_JF").text(),
            }
        });
        $("#LB_ZSJF").text($("#LB_SK_JF").text());
    }
    if (zqmx.ZKJE == 0)
    {
        $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
        $("#B_ZK_Search").prop("disabled", true);
    }
    else
    {
        $("#B_ZK_Search").prop("disabled", false);
    }
    if (zqmx.ZKLPJE == 0)
    {
        $("#B_ZF_Search").prop("disabled", true);
        $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
    }
    else
    {
        $("#B_ZF_Search").prop("disabled", false);
    }
}

function SetControlState() {
    if ($("#HF_STATUS").val() == 2 || ($("#HF_ZXR").val() == "" || $("#HF_ZXR").val()=="0"))
        $("#B_Start").prop("disabled", true);
    else
        $("#B_Start").prop("disabled", false);
    //if (vProcStatus == cPS_MODIFY)
    //    $("#R_ZFFS").show();
    //else
    //    $("#R_ZFFS").hide();
    $("#R_ZFFS").prop("disabled", vProcStatus != cPS_MODIFY);
}

function SaveClick()
{
    var vMode;
    if (IsValidInputData())
    {
        if (vJLBH != "")
        {
            vMode = "Update";
        }
        else
        {
            vMode = "Insert";
        }
        var pZKJE = $("#LB_SK_ZK").text();
        var rowDatas = $("#GV_ZK").datagrid("getData").rows;
        var pSJZSJF = $("#LB_SK_JF").text();
        var rowJFIDs = $("#GV_ZJF").datagrid("getData").rows;
        if ((rowDatas.length <= 0 && parseFloat(pZKJE) > 0) || (rowJFIDs.length <= 0 && parseFloat(pSJZSJF) > 0))
        {
            ShowYesNoMessage("未赠送券或者积分，是否保存？", function ()
            {
                if (posttosever(SaveDataBase(), vUrl, vMode) == true)
                {
                    vProcStatus = cPS_BROWSE;
                    SetControlBaseState();
                }
            });
        }
            //else if (parseFloat(pZKJE) > parseFloat($("#LB_ZS_CZK").text()))
            //{
            //    ShowYesNoMessage("赠卡金额小于应送金额，是否保存？", function ()
            //    {
            //        if (posttosever(SaveDataBase(), vUrl, vMode) == true)
            //        {
            //            vProcStatus = cPS_BROWSE;
            //            SetControlBaseState();
            //        }
            //    });
            //}
        else
        {
            if (posttosever(SaveDataBase(), vUrl, vMode) == true)
            {
                vProcStatus = cPS_BROWSE;
                SetControlBaseState();
            }
        }
    }
};



