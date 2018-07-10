vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ["HYID", "会员卡号", "姓名","OPENID"];
    vColumnModel = [
            { name: "iHYID", hidden: true, },
            { name: "sHYK_NO", width: 150, },
            { name: "sHY_NAME", width: 100, },
            { name: "sOPENID", },

    ];
}

$(document).ready(function () {
    FillHYZLXTree("TreeHYZLX", "TB_HYZLXMC");
    FillFXDWTree("TreeFXDW", "TB_FXDWMC");
    BFButtonClick("TB_BQMC", function () {

        SelectWXBQ("TB_BQMC", "TB_TAGID", "zTB_TAGID", iWXPID, sWXPIF, false);
    });

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    BFButtonClick("TB_MDXF_MD", function () {
        SelectMD("TB_MDXF_MD", "HF_MDXF_MD", "zHF_MDXF_MD", false);
    });
    BFButtonClick("TB_HYKNAME", function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

    $("#AddItem").click(function () {
        art.dialog.open("../../HYXF/HYXFQJHZQBQ/HYXF_HYXFQJHZDBQ.aspx?PLDBQ=" + 1, {
            lock: true,
            width: 1000,
            height: 1500,
         
            cancel:false,//去掉右上角的叉号
            close: function () {
                var returnData = $.dialog.data('IpValuesReturn');
                if (returnData != undefined) {
                    for (var i = 0; i <= returnData.length - 1; i++) {

                        if (returnData[i].iHYID != "") {
                            var lst = $("#list").datagrid("getRows");

                            if (lst.length<50)
                            {
                            $('#list').datagrid('appendRow', {
                                iHYID: returnData[i].iHYID,
                                sHYK_NO: returnData[i].sHYK_NO,
                                sHY_NAME: returnData[i].sHY_NAME,
                                sOPENID: returnData[i].sOPENID,
                            });

                            }

                            else{

                                ShowMessage("一次只能给50位会员打标签");
                                $.dialog.data('IpValuesReturn', "");//清空数据
                                $.dialog.close();
                                //return false;

                            }

                        }
                        $.dialog.data('IpValuesReturn', "");//清空数据
                    }

                }
            }

        }, false);
    });
 
 
    $("#DelItem").click(function () {
        DeleteRows("list");
    });



    //给用户批量打标签
    $("#PLDBQ").click(function () {
        if ($("#TB_TAGID").val() == "") {
            ShowMessage("请选择一个标签");
            return false;
        }
        var rowxz = $("#list").datagrid("getData").rows;

        if (rowxz.length <= 0) {
            ShowMessage("请添加用户");
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
                    //for (var j = 0; j < rowxz.length; j++) {


                    //    var index = $('#list').datagrid('getRowIndex', rowxz[j]);

                    //    if (rowxz[j].sTAGMC == "") {
                    //        // 得到columns对象
                    //        var columns = $('#list').datagrid("options").columns;

                    //        rowxz[j][columns[0][2].field] = $("#TB_BQMC").val();
                    //        // 刷新该行, 只有刷新了才有效果
                    //        $('#list').datagrid('refreshRow', index);
                    //    }
                    //    else {
                    //        // 得到columns对象
                    //        var columns = $('#list').datagrid("options").columns;

                    //        if (rowxz[j].sTAGMC.indexOf($("#TB_BQMC").val()) > 0) {
                    //            rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC
                    //            $('#list').datagrid('refreshRow', index);
                    //        }

                    //        else {

                    //            rowxz[j][columns[0][2].field] = rowxz[j].sTAGMC + "," + $("#TB_BQMC").val();

                    //            $('#list').datagrid('refreshRow', index);

                    //        }
                    //        // 刷新该行, 只有刷新了才有效果



                    //    }

                    //}


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
        var rowxz = $("#list").datagrid("getData").rows;
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

   
    $("#CK_BJ_DEL").change(function () {
        if (($("#CK_BJ_DEL")[0].checked ? 1 : 0) == 1) {
            $("#aa").show();
            $("#hh").show();
            document.getElementById("B_Save").disabled = false;

        }
        if (($("#CK_BJ_DEL")[0].checked ? 1 : 0) == 0) {
            $("#aa").hide();
            $("#hh").hide();
            document.getElementById("B_Save").disabled = true;



        }
    });
});
function TreeNodeClickCustom(e, treeId, treeNode) {
    switch (treeId) {
        case "TreeHYZLX": $("#HF_HYZLXID").val(treeNode.iHYZLXID); break;
        case "TreeFXDW": $("#HF_FXDWDM").val(treeNode.sFXDWDM); break;
    }
};

function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    //$("#B_Save").hide();
    $("#B_Cancel").hide();
    $("#aa").hide();
    $("#hh").hide();
    document.getElementById("B_Save").disabled=true

}

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
        Obj.sGRPMC = $("#TB_GRPMC").val();
        Obj.iGRPYT = $("#HF_GRPYT").val();
        Obj.sGRPMS = $("#TB_GRPMS").val();
        Obj.iGZFS = 1;
        Obj.iHYZLXID = $("#HF_HYZLXID").val();
        Obj.dKSSJ = $("#TB_KSSJ").val();
        Obj.dJSSJ = $("#TB_JSSJ").val();
        Obj.sGRPMS = $("#TB_GRPMS").val();

    
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
function ShowData(data) {

}








