
var totalRow = new Array();
var bqArray = new Array();//标签数组


function DrawGrid(listName, vColName, vColModel, vSingle, vHeight) {
    //为简化查询模板开发流程，统一Grid格式，新的查询可以使用InitGrid函数初始化vColumnNames和vColumnModel
    InitGrid();
    if (vHeight == undefined) { vHeight = vInitHeight; }
    if (vSingle == undefined) { vSingle = false; }
    if (listName == undefined) { listName = "list"; tabName = "#tb"; }
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
        autoRowHeight: true,
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
        pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 3,
        pageList: [3, 10, 50, 100],
        toolbar: '' + tabName + '', //+ tabName,
        onClickCell: onClickCell,
        onClickRow: OnClickRow,

    })
}
function InitGrid() {
    vColumnNames = ['头像', '昵称', '所在分组', 'openid', '备注'];
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
    HQQBBQ();//进入页面就得到全部标签，便于用标签号匹配标签名称

    //移出黑名单
    $("#DELHMD").click(function () {
        var rowxz = $('#list').datagrid('getSelections');
        if (rowxz.length <= 0) {
            ShowMessage("还未选中要移出黑名单的用户");
            return;
        }
        var openid_listhmd = [];
        for (var j = 0; j < rowxz.length; j++) {
            openid_listhmd.push(rowxz[j].sOPENID);
        }
        var sjson = { "openid_list": openid_listhmd }
        $.ajax({
            type: "post",
            async: false,

            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=DELHMD&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {

                    ShowMessage("成功移出黑名单");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });


        //var list_search = totalRow.filter(function (item) {
        //    return item.sNC == $("#TB_NC").val();
        //})

        //$('#list').datagrid('loadData', list_search);


        HQHMD()
    });


    //获取黑名单列表
    $("#SHOWHMD").click(function () {

        $('#list').datagrid("loadData", { total: 0, rows: [] });

        var sjson = { "begin_openid": "" }
        $.ajax({
            type: "post",
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=SHOWHMD&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            async: false,
            success: function (data) {
                var dataArray = new Array();
                dataArray = JSON.parse(data);
                //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
                if (dataArray != null) {
                    var user_list = [];
                    for (var j = 0; j < dataArray.openid.length; j++) {
                        user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
                    }
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

                    ShowMessage("黑名单里没有用户")


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




});


function HQHMD() {
    $('#list').datagrid("loadData", { total: 0, rows: [] });

    var sjson = { "begin_openid": "" }
    $.ajax({
        type: "post",
        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=SHOWHMD&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { json: JSON.stringify(sjson) },
        async: false,
        success: function (data) {
            var dataArray = new Array();
            dataArray = JSON.parse(data);
            //现在   这里得到了OpenID，根据openid去  批量获取用户基本信息
            if (dataArray != null) {
                var user_list = [];
                for (var j = 0; j < dataArray.openid.length; j++) {
                    user_list.push({ "openid": dataArray.openid[j], "lang": "zh_CN" });
                }
                var sjson = { "user_list": user_list }
                $.ajax({
                    type: "post",
                    url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetYHXXList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
                    data: { json: JSON.stringify(sjson) },
                    async: false,
                    success: function (data) {
                        var dataArray2 = new Array();
                        dataArray2 = JSON.parse(data);

                        for (var i = 0; i < dataArray2.length; i++) {
                            $('#list').datagrid('appendRow', {
                                sTX: "<img src='" + dataArray2[i].headimgurl + "' />",
                                sNC: dataArray2[i].nickname,
                                sTAGMC: "111",
                                sOPENID: dataArray2[i].openid,
                                sBZ: dataArray2[i].remark,

                            });
                        }
                        row = $("#list").datagrid("getData").rows;
                        return row;



                    },



                    error: function (data) {
                        ShowMessage(data)
                    }
                });


            }
            else {

                ShowMessage("黑名单里没有用户")


            }
        },
        error: function (data) {
            ShowMessage(data)
        }
    });



}



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
