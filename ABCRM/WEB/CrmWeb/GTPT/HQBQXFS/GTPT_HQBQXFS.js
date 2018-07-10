vUrl = "../GTPT.ashx";
var bj;
var idsc;
var tagid = GetUrlParam("TAGID")
var tagmc = GetUrlParam("TAGMC")
var next_openid;
var next_openidBQ;
var row;
var totalRow = new Array();

var bqArray = new Array();//标签数组

function DrawGrid(listName, vColName, vColModel, vSingle, vHeight) {
    //为简化查询模板开发流程，统一Grid格式，新的查询可以使用InitGrid函数初始化vColumnNames和vColumnModel
    InitGrid();

    if (vHeight == undefined) { vHeight = vInitHeight; }
    if (vSingle == undefined) { vSingle = false; }
    if (listName == undefined) { listName = "list"; tabName = "#tb";}
    if (listName != "list") { tabName = "#" + listName + "_tb" }
    if (vColName == undefined) { vColName = vColumnNames; }
    if (vColModel == undefined) { vColModel = vColumnModel; }
    if (vColumns.length == 0 || vColName != vColumnNames) {
        vColumns = InitColumns(undefined, vColModel, vColName);
        vAllColumns = vColumns;
    }
    $("#" + listName + "").datagrid({
        width: '100%',
        height: vHeight,
        autoRowHeight: true,//加宽列
        singleSelect: vSingle,
        striped: true,
        columns: [vColumns],
        sortName: vColumns[0].field,
        sortOrder: 'desc',
        fitColumns: true,
        showHeader: true,
        showFooter: false,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        rownumWidth: 100,
        pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 20,
        pageList: [20, 50, 100],
        toolbar: '' + tabName + '', //+ tabName,
        onClickCell: onClickCell,
        onClickRow: OnClickRow,
    });
   
}

function InitGrid() {
    vColumnNames = ['头像', '昵称', '所在分组', 'openid', '备注', '关注时间'];
    vColumnModel = [
        {
            name: 'sTX', width: 60, height: 60,
            columns:
            [
    {
        title: '图片', field: 'image', width: 60, height: 60, align: 'center',
    }
            ]
        },
    { name: 'sNC', width: 100, },
    { name: 'sTAGMC', width: 200, },
    { name: 'sOPENID', hidden: true },
    { name: 'sBZ', editor: 'text', width: 100, },
    { name: 'sGZSJ', width: 100, },

    ];
}

$(document).ready(function () {
    $("#hh").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Save").hide();
    $("#B_Cancel").hide();

    BFButtonClick("TB_BQMC", function () {
        SelectWXBQ("TB_BQMC", "TB_TAGID", "zTB_TAGID", iWXPID, sWXPIF, false);
    });

    HQQBBQ();//进入页面就得到全部标签，便于用标签号匹配标签名称
    //给用户批量打标签
    $("#PLDBQ").click(function () {
        if ($("#TB_TAGID").val() == "") {
            ShowMessage("请选择一个标签");
            return false;
        }
        var rowxz = $('#list').datagrid('getSelections');

        if (rowxz.length <= 0) {
            ShowMessage("还未选中要打标签的用户");
            return;
        }
        var openid_list = [];
        for (var j = 0; j < rowxz.length; j++) {
            openid_list.push(rowxz[j].sOPENID);
        }

        var sjson = { "openid_list": openid_list, "tagid": Number($("#TB_TAGID").val()) }
        $.ajax({
            type: "post",
            async: false,

            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=PLDBQTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&iDJR=" + iDJR + "&sDJRMC=" + encodeURIComponent(sDJRMC),
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {

                    //给分组列 赋新值
                    for (var j = 0; j < rowxz.length; j++) {


                        var index = $('#list').datagrid('getRowIndex', rowxz[j]);

                        if (rowxz[j].sTAGMC == "") {
                            // 得到columns对象
                            var columns = $('#list').datagrid("options").columns;

                            rowxz[j][columns[0][2].field] = $("#TB_BQMC").val();
                            // 刷新该行, 只有刷新了才有效果
                            $('#list').datagrid('refreshRow', index);
                        }
                        else {
                            // 得到columns对象
                            var columns = $('#list').datagrid("options").columns;

                            if (rowxz[j].sTAGMC.indexOf($("#TB_BQMC").val()) > 0) {
                                rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC
                                $('#list').datagrid('refreshRow', index);
                            }

                            else {

                                rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC + "," + $("#TB_BQMC").val();

                                $('#list').datagrid('refreshRow', index);

                            }
                            // 刷新该行, 只有刷新了才有效果



                        }

                    }


                    ShowMessage("成功打标签");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    });

    //给表格里当前页打标签

    $("#QXBY").click(function () {
        if ($("#TB_TAGID").val() == "") {
            ShowMessage("请选择一个标签");
            return false;
        }

        var rowxz = $("#list").datagrid("getData").rows;
        if (rowxz.length <= 0) {
            ShowMessage("还未选中要打标签的用户");
            return;
        }
        var openid_list = [];
        for (var j = 0; j < rowxz.length; j++) {
            openid_list.push(rowxz[j].sOPENID);
        }

        var sjson = { "openid_list": openid_list, "tagid": Number($("#TB_TAGID").val()) }
        $.ajax({
            type: "post",
            async: false,

            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=PLDBQTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&iDJR=" + iDJR + "&sDJRMC=" + encodeURIComponent(sDJRMC),
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {

                    //给分组列 赋新值
                    for (var j = 0; j < rowxz.length; j++) {
                        var index = $('#list').datagrid('getRowIndex', rowxz[j]);

                        if (rowxz[j].sTAGMC == "") {
                            // 得到columns对象
                            var columns = $('#list').datagrid("options").columns;

                            rowxz[j][columns[0][2].field] = $("#TB_BQMC").val();

                            // 刷新该行, 只有刷新了才有效果

                            $('#list').datagrid('refreshRow', index);
                        }
                        else {
                            // 得到columns对象
                            var columns = $('#list').datagrid("options").columns;


                            if (rowxz[j].sTAGMC.indexOf($("#TB_BQMC").val()) > 0) {
                                rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC
                                $('#list').datagrid('refreshRow', index);
                            }

                            else {

                                rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC + "," + $("#TB_BQMC").val();

                                $('#list').datagrid('refreshRow', index);

                            }

                            // 刷新该行, 只有刷新了才有效果

                            $('#list').datagrid('refreshRow', index);


                        }

                    }


                    ShowMessage("成功打标签");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    });
    //给表格里所有用户打标签
    $("#QXYJQHDYH").click(function () {
        if ($("#TB_TAGID").val() == "") {
            ShowMessage("请选择一个标签");
            return false;
        }

        var rowxz = totalRow;
        if (rowxz.length <= 0) {
            ShowMessage("还未选中要打标签的用户");
            return;
        }
        var openid_list = [];
        for (var j = 0; j < rowxz.length; j++) {
            openid_list.push(rowxz[j].sOPENID);
        }

        var sjson = { "openid_list": openid_list, "tagid": Number($("#TB_TAGID").val()) }
        $.ajax({
            type: "post",
            async: false,

            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=PLDBQTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&iDJR=" + iDJR + "&sDJRMC=" + encodeURIComponent(sDJRMC),
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {

                    //给分组列 赋新值
                    for (var j = 0; j < rowxz.length; j++) {
                        if (rowxz[j].sTAGMC == "") {
                            // 得到columns对象
                            var columns = $('#list').datagrid("options").columns;

                            rowxz[j][columns[0][2].field] = $("#TB_BQMC").val();

                            // 刷新该行, 只有刷新了才有效果

                            $('#list').datagrid('refreshRow', j);
                        }
                        else {
                            // 得到columns对象
                            var columns = $('#list').datagrid("options").columns;


                            if (rowxz[j].sTAGMC.indexOf($("#TB_BQMC").val()) > 0)
                                rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC
                            else {

                                rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC + "," + $("#TB_BQMC").val();
                            }


                            // 刷新该行, 只有刷新了才有效果

                            $('#list').datagrid('refreshRow', j);


                        }

                    }


                    ShowMessage("成功打标签");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    });

    //给用户批量取消标签
    $("#PLQXBQ").click(function () {
        var rowxz = $('#list').datagrid('getSelections');
        if (rowxz.length <= 0) {
            ShowMessage("还未选中要取消标签的用户");
            return;
        }

        var openid_list = [];
        for (var j = 0; j < rowxz.length; j++) {
            openid_list.push(rowxz[j].sOPENID);
        }
        var sjson = { "openid_list": openid_list, "tagid": Number($("#TB_TAGID").val()) }
        $.ajax({
            type: "post",
            async: false,
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=PLQXBQTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {
                    //标签打成功后，在界面还得把 刚刚取消打的标签 不要显示在页面表格的所在分组列了;



                    //给分组列 赋新值
                    for (var j = 0; j < rowxz.length; j++) {

                        var index = $('#list').datagrid('getRowIndex', rowxz[j]);

                        // 得到columns对象
                        var columns = $('#list').datagrid("options").columns;
                        var QX = $("#TB_BQMC").val();

                        rowxz[j].sTAGMC = rowxz[j].sTAGMC.replace("," + QX, "");

                        rowxz[j].sTAGMC = rowxz[j].sTAGMC.replace(QX, "");


                        rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC;

                        // 刷新该行, 只有刷新了才有效果

                        $('#list').datagrid('refreshRow', index);




                    }




                    ShowMessage("成功取消标签");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    });


    //修改备注
    $("#XGBZ").click(function () {
        var rowxz = $('#list').datagrid('getSelections');
        if (rowxz.length <= 0) {
            ShowMessage("还未选中要修改备注的用户");
            return;
        }

        if (rowxz.length != 1) {
            ShowMessage("只可以选中一个用户进行修改备注");
            return;
        }
        var sjson = { "openid": rowxz[0].sOPENID, "remark": rowxz[0].sBZ }
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=XGBZ&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {

                    ShowMessage("修改备注成功");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    });


    //加入黑名单
    $("#ADDHMD").click(function () {


        var rowxz = $('#list').datagrid('getSelections');
        if (rowxz.length <= 0) {
            ShowMessage("还未选中要加入黑名单的用户");
            return;
        }
        var openid_listhmd = [];
        for (var j = 0; j < rowxz.length; j++) {
            openid_listhmd.push(rowxz[j].sOPENID);
        }
        var sjson = { "openid_list": openid_listhmd }
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=ADDHMD&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {

                    ShowMessage("成功加入黑名单");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });





    });


    //获取某一个标签下粉丝,界面自己选择TAGID
    $("#XZBQHQFS").click(function () {

        totalRow = [];
        $('#list').datagrid("loadData", { total: 0, rows: [] });

        if ($("#TB_TAGID").val() == "") {
            ShowMessage("请选择一个标签");
            return false;
        }
        var sjson = { "tagid": Number($("#TB_TAGID").val()), "next_openid": "" }
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetFSList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            async: false,
            success: function (data) {
                var dataArray = new Array();
                dataArray = JSON.parse(data);
                //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
                if (dataArray != null) {
                    var user_list = [];
                    var dd;
                    if (dataArray.openid.length < 20)
                    { dd = dataArray.openid.length; }
                    else
                    { dd = 20; }

                    for (var j = 0; j < dd; j++) {
                        user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
                    }
                    next_openidBQ = dataArray.openid[dd - 1];

                    var sjson = { "user_list": user_list }
                    $.ajax({
                        type: "post",
                        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetYHXXList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
                        data: { json: JSON.stringify(sjson) },
                        async: false,
                        success: function (data) {

                            var dataArray2 = new Array();
                            dataArray2 = JSON.parse(data);
                            var nextArray = new Array();

                            for (var i = 0; i < dataArray2.length; i++) {
                                var sTAGMC_all_temp = "";
                                for (var m = 0; m < dataArray2[i].tagid_list.length; m++) {
                                    //标签号    dataArray2[i].tagid_list[m];
                                    var sTAGMC_temp1 = "";

                                    for (var n = 0; n < bqArray.length; n++) {
                                        if (dataArray2[i].tagid_list[m] == bqArray[n].id) {
                                            sTAGMC_temp1 = bqArray[n].name;
                                        }

                                    }

                                    if (sTAGMC_all_temp == "")
                                    { sTAGMC_all_temp = sTAGMC_temp1 }
                                    else
                                    { sTAGMC_all_temp = sTAGMC_all_temp + "," + sTAGMC_temp1; }

                                }

                                var tmp_obj = {
                                    sTX: "<img src='" + dataArray2[i].headimgurl + "' />",
                                    sNC: dataArray2[i].nickname,
                                    sTAGMC: sTAGMC_all_temp,//"不知道咋取",
                                    sOPENID: dataArray2[i].openid,
                                    sBZ: dataArray2[i].remark,
                                    sGZSJ: dataArray2[i].subscribe_time,
                                }
                              nextArray.push(tmp_obj);
                            }
                            totalRow = totalRow.concat(nextArray);
                            var row = $("#list").datagrid("getData").rows;
                            $('#list').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', row.concat(nextArray));
                        },

                        error: function (data) {
                            ShowMessage(data)
                        }
                    });
                }
                else {
                    ShowMessage("没有更多用户了")

                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
    })

    //获取某一个标签下粉丝,界面自己选择TAGID,  下一次
    $("#NEXTBQYHXX").click(function () {
        var rowxz = $("#list").datagrid("getData").rows;
        if (rowxz.length <= 0) {
            ShowMessage("请先点击·获取标签粉丝");
            return;
        }
        var sjson = { "tagid": Number($("#TB_TAGID").val()), "next_openid": next_openidBQ }
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetFSList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson)},
            async: false,
            success: function (data) {
                var dataArray = new Array();
                dataArray = JSON.parse(data);
                //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
                if (dataArray != null) {
                    var user_list = [];
                    var dd;
                    if (dataArray.openid.length < 20)
                    { dd = dataArray.openid.length; }
                    else
                    { dd = 20; }

                    for (var j = 0; j < dd; j++) {
                        user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
                    }
                    next_openidBQ = dataArray.openid[dd - 1];

                    var sjson = { "user_list": user_list }
                    $.ajax({
                        type: "post",
                        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetYHXXList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
                        data: { json: JSON.stringify(sjson) },
                        async: false,
                        success: function (data) {

                            var dataArray2 = new Array();
                            dataArray2 = JSON.parse(data);
                            var nextArray = new Array();

                            for (var i = 0; i < dataArray2.length; i++) {

                                var sTAGMC_all_temp = "";

                                for (var m = 0; m < dataArray2[i].tagid_list.length; m++) {
                                    //标签号    dataArray2[i].tagid_list[m];
                                    var sTAGMC_temp1 = "";

                                    for (var n = 0; n < bqArray.length; n++) {
                                        if (dataArray2[i].tagid_list[m] == bqArray[n].id) {
                                            sTAGMC_temp1 = bqArray[n].name;
                                        }

                                    }

                                    if (sTAGMC_all_temp == "")
                                    { sTAGMC_all_temp = sTAGMC_temp1 }
                                    else
                                    { sTAGMC_all_temp = sTAGMC_all_temp + "," + sTAGMC_temp1; }

                                }

                                var tmp_obj = {
                                    sTX: "<img src='" + dataArray2[i].headimgurl + "' />",
                                    sNC: dataArray2[i].nickname,
                                    sTAGMC: tagmc,
                                    sOPENID: dataArray2[i].openid,
                                    sBZ: dataArray2[i].remark,
                                    sGZSJ: dataArray2[i].subscribe_time,
                                }
                                nextArray.push(tmp_obj);

                            }

                            totalRow = totalRow.concat(nextArray);

                            $('#list').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', totalRow);

                        },



                        error: function (data) {
                            ShowMessage(data)
                        }
                    });

                }
                else {

                    ShowMessage("没有更多的用户了")

                }

            },
            error: function (data) {
                ShowMessage(data)
            }


        });
    })

    //获取全部粉丝，获取基本信息的时候只能一次100条，前100条

    $("#HQQBFS").click(function () {
        $('#list').datagrid("loadData", { total: 0, rows: [] });
        var sjson = {}
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetQBFSList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            async: false,
            success: function (data) {
                var dataArray = new Array();
                dataArray = JSON.parse(data);
                if (dataArray != null) {

                    //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
                    var user_list = [];
                    var dd;
                    if (dataArray.openid.length < 100)
                    { dd = dataArray.openid.length; }
                    else
                    { dd = 100; }

                    for (var j = 0; j < dd; j++) {
                        user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
                    }

                    next_openid = dataArray.openid[dd - 1];


                    var sjson = { "user_list": user_list }
                    $.ajax({
                        type: "post",
                        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetYHXXList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
                        data: { json: JSON.stringify(sjson) },
                        async: false,
                        success: function (data) {
                            var dataArray2 = new Array();
                            dataArray2 = JSON.parse(data);
                            var nextArray = new Array();
                            for (var i = 0; i < dataArray2.length; i++) {

                                var sTAGMC_all_temp = "";

                                for (var m = 0; m < dataArray2[i].tagid_list.length; m++) {
                                    //标签号    dataArray2[i].tagid_list[m];
                                    var sTAGMC_temp1 = "";

                                    for (var n = 0; n < bqArray.length; n++) {
                                        if (dataArray2[i].tagid_list[m] == bqArray[n].id) {
                                            sTAGMC_temp1 = bqArray[n].name;
                                        }

                                    }

                                    if (sTAGMC_all_temp == "")
                                    { sTAGMC_all_temp = sTAGMC_temp1 }
                                    else
                                    { sTAGMC_all_temp = sTAGMC_all_temp + "," + sTAGMC_temp1; }

                                }
                                var tmp_obj = {
                                    sTX: "<img src='" + dataArray2[i].headimgurl + "' />",
                                    sNC: dataArray2[i].nickname,
                                    sTAGMC: sTAGMC_all_temp,//"不知道咋取",
                                    sOPENID: dataArray2[i].openid,
                                    sBZ: dataArray2[i].remark,
                                    sGZSJ: dataArray2[i].subscribe_time,
                                }
                                nextArray.push(tmp_obj);

                            }



                            totalRow = totalRow.concat(nextArray);
                            var row = $("#list").datagrid("getData").rows;
                            $('#list').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', row.concat(nextArray));

                        },



                        error: function (data) {
                            ShowMessage(data)
                        }
                    });

                }
                else {
                    ShowMessage("没有更多用户了")

                }

            },
            error: function (data) {
                ShowMessage(data)
            }
        });



    })

    //获取下一次100条的粉丝，获取基本信息的时候只能一次100条
    $("#NEXTYHXX").click(function () {
        var rowxz = $("#list").datagrid("getData").rows;
        if (rowxz.length <= 0) {
            ShowMessage("请先点击·获取所有粉丝·");
            return;
        }
        var sjson = {}
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetQBFSList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&next_openid=" + next_openid,
            data: { json: JSON.stringify(sjson) },
            async: false,
            success: function (data) {
                var dataArray = new Array();
                dataArray = JSON.parse(data);
                if (dataArray != null) {

                    //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
                    var user_list = [];
                    var dd;
                    if (dataArray.openid.length < 100)
                    { dd = dataArray.openid.length; }
                    else
                    { dd = 100; }

                    for (var j = 0; j < dd; j++) {
                        user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
                    }
                    next_openid = dataArray.openid[dd - 1];

                    var sjson = { "user_list": user_list }
                    $.ajax({
                        type: "post",
                        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetYHXXList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
                        data: { json: JSON.stringify(sjson) },
                        async: false,


                        success: function (data) {
                            var dataArray2 = new Array();
                            dataArray2 = JSON.parse(data);
                            //这里要加上一次的取消分页，要不不完全显示


                            var nextArray = new Array();
                            for (var i = 0; i < dataArray2.length ; i++) {


                                var tmp_obj = {
                                    sTX: "<img src='" + dataArray2[i].headimgurl + "' />",
                                    sNC: dataArray2[i].nickname,
                                    sTAGMC: tagmc,
                                    sOPENID: dataArray2[i].openid,
                                    sBZ: dataArray2[i].remark,
                                    sGZSJ: dataArray2[i].subscribe_time,
                                }
                                nextArray.push(tmp_obj);


                            }
                            totalRow = totalRow.concat(nextArray);

                            // var row = $("#list").datagrid("getData").rows;
                            $('#list').datagrid({ loadFilter: pagerFilter }).datagrid('loadData', totalRow);

                        },

                        error: function (data) {
                            ShowMessage(data)
                        }
                    });

                }
                else {
                    ShowMessage("没有更多的用户了")

                }

            },
            error: function (data) {
                ShowMessage(data)
            }
        });


    });

    //按昵称查询
    $("#CXNC").click(function () {

        var rowxz = totalRow;
        if (rowxz.length <= 0) {
            ShowMessage("请先获取用户，这个只能查询已经获取到的用户");
            return;
        }
        var list_search = totalRow.filter(function (item) {
            return item.sNC == $("#TB_NC").val();
        })
        
        $('#list').datagrid('loadData', list_search);


    })

});


//function removeByValue(arr, val) {
//    for (var i = 0; i < arr.length; i++) {
//        if (arr[i] == val) {
//            arr.splice(i, 1);
//            break;
//        }
//    }
//}

function HQQBBQ() {
    var sjson = {}
    $.ajax({
        type: "post",
        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetTagList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { json: JSON.stringify(sjson) },
        success: function (data) {
            var dataArray = new Array();
            dataArray = JSON.parse(data);
            bqArray = dataArray;

        },
        error: function (data) {
            ShowMessage(data)
        }
    });


}
//分页
function pagerFilter(data) {
    if (typeof data.length == 'number' && typeof data.splice == 'function') {	// is array
        data = {
            total: data.length,
            rows: data
        }
    }
    var dg = $(this);
    var opts = dg.datagrid('options');
    var pager = dg.datagrid('getPager');
    pager.pagination({
        onSelectPage: function (pageNum, pageSize) {
            opts.pageNumber = pageNum;
            opts.pageSize = pageSize;
            pager.pagination('refresh', {
                pageNumber: pageNum,
                pageSize: pageSize
            });
            dg.datagrid('loadData', data);
        }
    });
    if (!data.originalRows) {
        data.originalRows = (data.rows);
    }
    var start = (opts.pageNumber - 1) * parseInt(opts.pageSize);
    var end = start + parseInt(opts.pageSize);
    data.rows = (data.originalRows.slice(start, end));
    return data;
}
function IsValidInputData() {
    return true;
}

function timestampToTime(timestamp) {
    var date = new Date(timestamp * 1000);//时间戳为10位需*1000，时间戳为13位的话不需乘1000
    Y = date.getFullYear() + '-';
    M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1) + '-';
    D = date.getDate() + ' ';
    h = date.getHours() + ':';
    m = date.getMinutes() + ':';
    s = date.getSeconds();
    return Y + M + D + h + m + s;
};

function IsValidData() {

    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBQMC = $("#TB_BQMC").val();
    Obj.iTAGID = $("#TB_TAGID").val();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_BQMC").val(Obj.sBQMC);
    $("#TB_TAGID").val(Obj.iTAGID);

}


//var sjson = { "tagid": Number(tagid), "next_openid": "" }
//$.ajax({
//    type: "post",
//    url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetFSList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
//    data: { json: JSON.stringify(sjson) },
//    async: false,
//    success: function (data) {
//        var dataArray = new Array();
//        dataArray = JSON.parse(data);
//        //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
//        var user_list = [];
//        for (var j = 0; j < dataArray.openid.length; j++) {
//            user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
//        }
//        var sjson = { "user_list": user_list }
//        $.ajax({
//            type: "post",
//            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetYHXXList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
//            data: { json: JSON.stringify(sjson) },
//            async: false,
//            success: function (data) {
//                var dataArray2 = new Array();
//                dataArray2 = JSON.parse(data);

//                for (var i = 0; i < dataArray2.length; i++) {
//                    $('#list').datagrid('appendRow', {
//                        sTX: "<img src='" + dataArray2[i].headimgurl + "' />",
//                        sNC: dataArray2[i].nickname,
//                        sTAGMC: "111",
//                        sOPENID: dataArray2[i].openid,
//                        sBZ: dataArray2[i].remark,

//                    });
//                }
//                row = $("#list").datagrid("getData").rows;
//                return row;
//            },
//            error: function (data) {
//                ShowMessage(data)
//            }
//        });



//    },
//    error: function (data) {
//        ShowMessage(data)
//    }
//});

