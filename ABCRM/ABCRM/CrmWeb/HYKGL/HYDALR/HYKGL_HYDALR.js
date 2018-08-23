vUrl = "../HYKGL.ashx";

var fileuploader;
var vHYK_NO = GetUrlParam("HYK_NO") || "";
var flag = false;
if (vHYK_NO != "") {
    flag = true;
}
var canvas = new Array();//动态标记
var isUpdate = true;

function uploadBind() {

    //文件添加
    fileuploader.bind('FilesAdded', function (up, files) {
        var html = '';
        plupload.each(files, function (file) {
            html += '<li id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></li>';
        });
        document.getElementById('filelist').innerHTML += html;
        //查看是否已经上传，如果已经上传，是否提示更改上传的文件
        fileuploader.start();
        // uploaddialog = art.dialog({ content: '正在上传,请稍候', lock: true });
    });
    //上传进度显示
    fileuploader.bind('UploadProgress', function (up, file) {
        document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span class="del_span">' + file.percent + "%</span>";
    });
    //错误处理
    fileuploader.bind('Error', function (up, err) {
        document.getElementById('console').innerHTML += "\nError #" + err.code + ": " + err.message;
    });

    //分块数据上传完成
    fileuploader.bind('ChunkUploaded', function (upload, file, response) {

    });

    //单个文件整体上传完成
    fileuploader.bind('FileUploaded', function (upload, file, response) {
        if (response.response.indexOf("错误") == 0) {
            fileuploader.stop();
            art.dialog({ content: response.response, lock: true, });
            return;
        }
        else {
            userIconUrl = "../../../HeadPhoto/" + response.response + ".jpg";
            $("#HeadPhoto").attr("src", userIconUrl + "?temp=" + Math.random());//userIconUrl
        }

    });

    //队列中的所有文件上传结束
    fileuploader.bind('UploadComplete', function (upload, file, response) {
        document.getElementById('console').innerHTML = "";
        document.getElementById('filelist').innerHTML = "";

    });
}
function SetControlState() {
    $("#B_Exec").hide();
    document.getElementById("B_Update").disabled = !document.getElementById("B_Save").disabled || !bCanEdit;
    //document.getElementById("TB_CSRQ").disabled = true;
    if (vProcStatus == cPS_MODIFY) {
        sjhm = $("#TB_SJHM").val();
        if (sjhm.substr(0, 1) == "0") {
            $("#CB_LNK").prop("checked", true);
            CB_LNK_Click();
        }
    }

    //$("#DDL_ZJLX").attr("disabled", isUpdate);
    //$("#TB_SFZBH").attr("disabled", isUpdate);
    // $("#TB_CSRQ").attr("disabled", isUpdate);
    // $("#TB_SJHM").attr("disabled", isUpdate);
    //$("#takePhoto").prop("disabled", isUpdate);
    //$("#TB_YZM").prop("disabled", isUpdate);
    // $("#Radio1").prop("disabled", isUpdate);
    // $("#Radio2").prop("disabled", isUpdate);
    // $("#TB_HYNAME").prop("disabled", isUpdate);
    if ($("#TB_SJHM").val() != "")
        $("#TB_SJHM").attr("disabled", "disabled");
    else
        $("#TB_SJHM").removeAttr("disabled");

}

function CB_LNK_Click() {
    var tp_lnk = $("#CB_LNK").prop("checked");
    if (tp_lnk) {
        $("#TB_SJHM").removeAttr("maxlength");
        $("#TB_YZM").prop("disabled", true);
    }
    else {
        $("#TB_SJHM").val("").attr("maxlength", "11");
        $("#TB_YZM").prop("disabled", false);
    }
}

function InitGrid() {
    vColumnNames = ['家人姓名', '关系', '性别', '年龄', '生日'];
    vColumnModel = [
            { name: 'JTXM', index: 'JTXM', width: 60, },
            { name: 'JTGX', index: 'JTGX', width: 60, },
            { name: 'JTXB', index: 'JTXB', width: 60, },
            { name: 'JTNL', index: 'JTNL', width: 60, },
            { name: 'JTSR', index: 'JTSR', width: 60, },
    ];

    vETHDColumnNames = ['iHDID', '活动代码', '活动名称', '发备注'];
    vETHDColumnModel = [
              { name: 'iQZID', index: 'iJLBH', width: 60, hidden: true, },
              { name: 'sQZDM', width: 60, },
              { name: 'sQZMC', width: 60, },
              { name: 'sBZ', width: 60, },
    ];
    vWDTJColumnNames = ['HYID', '卡号', '顾客姓名', '卡类型', '卡状态'];
    vWDTJColumnModel = [
              { name: 'iHYID', hidden: true, },
              { name: 'sHYK_NO', width: 70, },
              { name: 'sHY_NAME', width: 70, },
              { name: 'sHYKNAME', width: 70, },
              { name: 'sStatusName', width: 90, },
    ];

    vWDQZColumnNames = ['圈子ID', '圈子名称', '备注', ];
    vWDQZColumnModel = [
              { name: 'iQZID', index: 'iJLBH', width: 60, },
              { name: 'sQZMC', width: 60, },
              { name: 'sBZ', width: 60, },
    ];

    vHYKColumnNames = ['HYID', 'GKID', '卡号', '姓名', '卡类型', '卡状态', '所属门店', '发行单位', '发行渠道', '建卡日期', '主卡标记', '卡介质', '开通密码'];
    vHYKColumnModel = [
            { name: 'iHYID', hidden: true, },
            { name: 'iGKID', hidden: true, },
            { name: 'sHYK_NO', width: 70, },
            { name: 'sHY_NAME', width: 70, hidden: true, },
            { name: 'sHYKNAME', width: 70, },
            { name: 'sStatusName', width: 90, },
            { name: 'sMDMC', width: 90, },
            { name: 'sFXDWMC', width: 90, },
            { name: 'sRHQDMC', width: 90, },
            { name: 'dJKRQ', width: 90, },
            { name: 'sBJ_PARENTSTR', width: 90 },
            { name: 'iCDJZ', width: 90, formatter: CellFormatForJZ },
            { name: 'iBJ_PSW', width: 90, formatter: CellFormatForYes },
    ];

};

$(document).ready(function () {
    $("#takePhoto").click(function () {
        TakePhoto("HeadPhoto", "HF_IMGURL");
    });
    DrawGrid("listETHD", vETHDColumnNames, vETHDColumnModel);
    DrawGrid("listWDTJ", vWDTJColumnNames, vWDTJColumnModel);
    DrawGrid("listWDQZ", vWDQZColumnNames, vWDQZColumnModel);
    DrawGrid("List_HYK", vHYKColumnNames, vHYKColumnModel);
    FillQYTree("TreeQY", "TB_QY");
    FillSelect("DDL_ZJLX", GetHYXXXM(0));
    FillSelect("DDL_ZY", GetHYXXXM(1));
    FillSelect("DDL_JTSR", GetHYXXXM(2));
    FillSelect("DDL_XL", GetHYXXXM(3));
    FillSelect("DDL_JTGJ", GetHYXXXM(5));
    CYCBL_ADD_ITEM("CBL_YYAH", GetHYXXXM(7));
    CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));
    CYCBL_ADD_ITEM("CBL_CXXX", GetHYXXXM(9));
    FillSelect("S_QCPP", GetHYXXXM(11));
    $("#DDL_ZJLX").val("1");
    //$("#B_Update").click(function () {
    //    UploadInit();
    //});
    $("#zxr1").html("更新人");
    $("#TB_SFZBH").change(function () {
        if ($("#TB_SFZBH").val() == "") {
            ShowMessage("请输入证件号码", 3);
            return;
        }
        //重新查找顾客信息,显示出来
        getGKDA($("#TB_SFZBH").val(), "", isUpdate);
        val = $("#DDL_ZJLX").val();
        if ($("#DDL_ZJLX").val() == 1) {
            showBirthday($("#TB_SFZBH").val());
        }

    });
    $("#TB_SJHM").change(function () {
        if ($("#TB_SJHM").val() == "") {
            ShowMessage("请输入手机号码", 3);
            return;
        }
        getGKDA("", $("#TB_SJHM").val(), isUpdate);
        if ($("#DDL_ZJLX").val() == 1) {
            showBirthday($("#TB_SFZBH").val());
        }

        //根据手机号，查询客户信息，显示出来
        //if ($("#TB_SFZBH").val() == "") {
        //    art.dialog({
        //        content: "证件号为空,是否根据手机信息查找顾客信息?",
        //        lock: true,
        //        ok: function () {
        //            getGKDA("", $("#TB_SJHM").val(), isUpdate);
        //            if ($("#DDL_ZJLX").val() == 1) {
        //                showBirthday($("#TB_SFZBH").val());
        //            }
        //        }
        //    , cancel: true
        //    });

        //}
    });

    $("#B_ReadCard").click(function () {
        var str = SynCardOcx1.FindReader();
        if (str > 0) {
            var nRet = SynCardOcx1.ReadCardMsg();
            if (nRet == 0) {
                $("#TB_HYNAME").val(SynCardOcx1.NameA);
                $("#TB_CSRQ").val(SynCardOcx1.Born.substr(0, 4) + "-" + SynCardOcx1.Born.substr(4, 2) + "-" + SynCardOcx1.Born.substr(6, 2));
                $("#TB_SFZBH").val(SynCardOcx1.CardNo);
                if (SynCardOcx1.Sex == 1) {
                    document.getElementById("Radio1").disabled = false;
                    $("#Radio1").click();
                }
                else {
                    document.getElementById("Radio2").disabled = false;
                    $("#Radio2").click();
                }
                getGKDA(SynCardOcx1.CardNo, "", isUpdate);
            }
        }
        else {
            showMessage("未找到读卡设备，请检查设备连接！", 3);
        }
    });


    $("#TA_PPHY").click(function () {
        var data = $("#zHF_PPHYID").val();
        var el = $("<input>", { type: 'text', val: data });
        $.dialog.data('IpValues', el);

        $.dialog.open("../../WUC/SPSB/WUC_SPSB.aspx?ryid=1001", {
            lock: true, width: 400, height: 400, cancel: false
            , close: function () {
                WUC_SPSB_Return();
            }
        }, false);

        if ($("#TA_PPHY").width() < 700) {
            $("#TA_PPHY").animate(
                { width: '+=50px' }

                );
        }

    });

    $("#TB_PPXQ").click(function () {
        var condData = new Object();
        if ($("#HF_QYID").val() != "") {
            condData["iQYID"] = $("#HF_QYID").val();
        }
        SelectXQ("TB_PPXQ", "HF_XQID", "zHF_XQID", true, condData);
    });


    //chick checkbox cb_djlx
    $("#CB_ZJLX").click(function () {

        if ($("#CB_ZJLX")[0].checked == false) {
            $("#DDL_ZJLX").attr("disabled", true);
            $("#DDL_ZJLX").val(1);
            //$("#TB_CSRQ").attr("disabled", true);
        } else {
            $("#DDL_ZJLX").attr("disabled", false);
            $("#TB_CSRQ").attr("disabled", false);
        }

    });

    //$("#B_CXHYKNO").click(function () {
    //    var tp_cxhykno = $("#TB_CXHYKNO").val();
    //    var tp_cssfz = $("#TB_CXSFZ").val();
    //    var tp_cssjhm = $("#TB_CXSJHM").val();
    //    if (tp_cxhykno == "" && tp_cssfz == "" && tp_cssjhm == "") {
    //        alert("会员卡号码/身份证/手机号码 不能同时为空！"); return;
    //    }
    //    //只要HYXX有，不管GKDA有没有，都可以录入
    //    var tp_hyxx = GetHYXXDataMore("0", tp_cxhykno, tp_cssfz, tp_cssjhm);
    //    var tp_hyxxjson = JSON.parse(tp_hyxx);
    //    var tp_gkid = "-1";
    //    var tp_hyid = 0;

    //    if (tp_hyxxjson != null) {
    //        tp_gkid = tp_hyxxjson.iGKID;
    //        tp_hyid = tp_hyxxjson.iHYID;
    //        vHYK_NO = tp_hyxxjson.sHYK_NO || vHYK_NO;
    //        $("#HF_GKID").val(tp_gkid);
    //        $("#TB_JLBH").val(tp_gkid);
    //        $("#HF_HYID").val(tp_hyid);
    //        $("#List_HYK").jqGrid('setGridParam', {
    //            url: "../../WUC/WUC.ashx?func=GetHYKList",
    //            postData: { iGKID: tp_gkid, iHYID: tp_hyid },
    //            page: 1,
    //            loadError: function (xhr, status, error) {
    //                alert(error);
    //            },
    //        }).trigger("reloadGrid");

    //        if (tp_gkid != null) { zDataToPage(tp_gkid); }
    //    }
    //    else {
    //        alert("没有找到符合条件的顾客档案！");
    //    }

    //});

    //用于会员卡操作台,应该使用会员卡号查
    if (vHYK_NO) {
        var str = GetGKDAData(0, "", "", vHYK_NO);
        $("#TB_CXHYKNO").val(vHYK_NO)

        ShowGKDA(str);
    }




    $("#B_CXHYKNO").click(function () {
        vHYK_NO = $("#TB_CXHYKNO").val();
        var str = GetGKDAData(0, "", "", vHYK_NO);
        ShowGKDA(str);
    });
    $("#TB_RYXX_LX4").click(function () {
        SelectRYXX("TB_RYXX_LX4", "HF_RYXX_LX4", "zHF_RYXX_LX4", true);
    });
    bNeedItemData = false;

});

function IsValidData() {
    var tp_msg = "";
    var tp_lnk = $("#CB_LNK").prop("checked");
    if ($("#SP_BJ_NAME").is(":visible") == true) { tp_msg += zIsNull("姓名", $("#TB_HYNAME").val()); }
    if ($("#SP_BJ_SJHM").is(":visible") == true) { tp_msg += zIsNull("手机号码", $("#TB_SJHM").val()); }
    if ($("#SP_BJ_QQ").is(":visible") == true) { tp_msg += zIsNull("QQ号", $("#TB_QQ").val()); }
    if ($("#SP_BJ_WX").is(":visible") == true) { tp_msg += zIsNull("微信号", $("#TB_WX").val()); }
    if ($("#SP_BJ_GZDW").is(":visible") == true) { tp_msg += zIsNull("工作单位", $("#TB_GZDW").val()); }
    if ($("#SP_BJ_ZY").is(":visible") == true) { tp_msg += zIsNull("备注", $("#TB_BZ").val()); }
    if ($("#SP_BJ_JTGJ").is(":visible") == true) { tp_msg += zIsNull("交通工具", $("#DDL_JTGJ").val()); }

    tp_msg += zIsNull("证件类型", $("#DDL_ZJLX").val());
    //if ($("#DDL_ZJLX").val() == "1") {
    //    tp_msg += IsIDCard("证件号码", $("#TB_SFZBH").val());
    //}
    tp_msg += zIsCSRQ("出生日期", $("#TB_CSRQ").val());
    tp_msg += zIsValidvar("姓名", $("#TB_HYNAME").val());
    if (!tp_lnk) {
        tp_msg += zIsTelePhone("手机号码", $("#TB_SJHM").val());
    }
    tp_msg += zIsInt("住宅电话号码", $("#TB_PHONE").val());
    tp_msg += zIsInt("QQ号", $("#TB_QQ").val());
    tp_msg += zIsEMail("E-MAIL", $.trim($("#TB_EMAIL").val()));
    tp_msg += zIsInt("邮编", $("#TB_YZBM").val());
    tp_msg += zIsValidvar("匹配小区", $("#TB_PPXQ").val());
    tp_msg += zIsValidvar("工作单位", $("#TB_GZDW").val());

    //if ($("#zHF_SJHM").val() != $("#TB_SJHM").val() && !tp_lnk) {
    //    if ($("#TB_YZM").val() == "") {
    //        ShowMessage('请输入验证码', 3);
    //        return false;
    //    }
    //    if ($("#HF_YZM").val() != $("#TB_YZM").val()) {
    //        ShowMessage('验证码输入错误', 3);
    //        return false;
    //    }

    //    if ($("#HF_SJHM").val() != $("#TB_SJHM").val()) {
    //        ShowMessage("手机号和接受验证码的手机号不一致", 3);
    //        return false;
    //    }
    //}

    if (tp_msg != "") {
        ShowMessage(tp_msg, 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "") {
        Obj.iJLBH = "0";
    }
    Obj.sHYK_NO = vHYK_NO;
    var tp_HYKXX = new Object();
    //member base infomation 
    tp_HYKXX.iZJLXID = $("#DDL_ZJLX")[0].value;
    tp_HYKXX.iBJ_YZZJLX = ($("#CB_YZZJLX").is(":checked")) ? 1 : 0;
    tp_HYKXX.sSFZBH = $("#TB_SFZBH").val();
    tp_HYKXX.sHY_NAME = $("#TB_HYNAME").val();
    tp_HYKXX.dCSRQ = $("#TB_CSRQ").val();
    tp_HYKXX.iSEX = $("[name='sex']:checked").val();
    tp_HYKXX.iTJRYID = $("#HF_GKDA").val() ? $("#HF_GKDA").val() : "-1";
    Obj.YZ_SJHM = ($("#CB_YZ_SJHM").is(":checked")) ? 1 : 0;
    //member Communication infomation
    tp_HYKXX.sSJHM = $("#TB_SJHM").val();
    tp_HYKXX.iBJ_YZSJHM = ($("#CB_YZSJHM").is(":checked")) ? 1 : 0;
    tp_HYKXX.sPHONE = $("#TB_PHONE").val();
    tp_HYKXX.sQQ = $("#TB_QQ").val();
    tp_HYKXX.sWX = $("#TB_WX").val();
    tp_HYKXX.sWB = $("#TB_WB").val();
    tp_HYKXX.sEMAIL = $.trim($("#TB_EMAIL").val());
    tp_HYKXX.sYZBM = $("#TB_YZBM").val();
    tp_HYKXX.iQYID = ($("#HF_QYID").val() != "") ? $("#HF_QYID").val() : "-1";//
    tp_HYKXX.sTXDZ1 = $(".selectList").find(".province").val();
    tp_HYKXX.sTXDZ2 = $(".selectList").find(".city").val();
    tp_HYKXX.sTXDZ3 = $(".selectList").find(".district").val();
    tp_HYKXX.sTXDZ4 = $(".selectList").find(".door").val();
    tp_HYKXX.sTXDZ = $("#TB_QY").val();
    if ($("#HF_XQID").val() != "0") {
        tp_HYKXX.sTXDZ += $("#TB_PPXQ").val();
    }
    tp_HYKXX.sTXDZ += $("#TB_MPH").val();
    tp_HYKXX.sPPXQ = $("#TB_PPXQ").val();
    tp_HYKXX.sGZDW = $("#TB_GZDW").val();
    tp_HYKXX.sROAD = $("#TB_ROAD").val();
    tp_HYKXX.sHOUSENUM = $("#TB_MPH").val();
    //other
    //var zyvalue = $("#DDL_ZY").val();  //删除的时候取值为null,下拉框要这样取值$("#DDL_ZY").find("option:selected").val()
    tp_HYKXX.iZYID = ($("#DDL_ZY")[0].value != "") ? GetSelectValue("DDL_ZY") : "-1";
    tp_HYKXX.iXLID = ($("#DDL_XL")[0].value != "") ? GetSelectValue("DDL_XL") : "-1";
    tp_HYKXX.iJTSRID = ($("#DDL_JTSR")[0].value != "") ? GetSelectValue("DDL_JTSR") : "-1";
    tp_HYKXX.iJTGJID = ($("#DDL_JTGJ")[0].value != "") ? GetSelectValue("DDL_JTGJ") : "-1";
    tp_HYKXX.iQCPPID = ($("#S_QCPP")[0].value != "") ? GetSelectValue("S_QCPP") : "-1";
    tp_HYKXX.sCPH = $("#TB_CPH").val();
    tp_HYKXX.iJHBJ = ($("#TB_HYZK")[0].value != "") ? GetSelectValue("TB_HYZK") : "-1";
    tp_HYKXX.dJHJNR = $("#TB_JHJNR").val();

    tp_HYKXX.sBZ = $("#TB_BZ").val();

    tp_HYKXX.iKHJLRYID = $("#HF_RYXX_LX4").val() ? $("#HF_RYXX_LX4").val() : "-1";


    var jsonValues = new Array();
    jsonValues = $("#list").datagrid("getData").rows;
    Obj.JTXX = jsonValues;

    tp_HYKXX.sXXFS = Get_CYCBL_CheckItem("CBL_XXFS");
    tp_HYKXX.sCXXX = Get_CYCBL_CheckItem("CBL_CXXX");
    tp_HYKXX.sYYAH = Get_CYCBL_CheckItem("CBL_YYAH");

    if ($("#R_CANSMS").prop("checked")) { tp_HYKXX.iCANSMS = 1; }
    if ($("#R_NOSMS").prop("checked")) { tp_HYKXX.iCANSMS = 1; }
    tp_HYKXX.iXQID = $("#HF_XQID").val() == "" ? 0 : $("#HF_XQID").val();
    tp_HYKXX.sSW = $("#TB_SW").val();
    tp_HYKXX.sCM = $("#TB_CM").val();
    tp_HYKXX.sXZ = $("#TB_XZ").val();
    tp_HYKXX.sSX = $("#TB_SX").val();
    tp_HYKXX.sIMGURL = $("#HF_IMGURL").val();
    Obj.HYKXX = tp_HYKXX;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(Data) {
    var data = JSON.parse(Data);
    $("#LB_GKXX").text("老顾客");
    //关联卡信息
    $('#List_HYK').datagrid("loadData", { total: 0, rows: [] });
    var hyklist = data.HYKXXLIST;
    if (hyklist) {
        window.setTimeout(function () {
            for (var i = 0; i <= hyklist.length - 1; i++) {
                $('#List_HYK').datagrid('appendRow', hyklist[i]);
            }
        }, 1000);
        if (hyklist.length) {
            if (flag) {//由操作台传入，表示维护信息
                vHYK_NO = vHYK_NO || hyklist[0].sHYK_NO;
            }
            else {//只是简单的录入信息
                vHYK_NO = hyklist[0].sHYK_NO;
            }

        }

    }
    if (!data.HYKXX) {//如果不存在GKDA
        $("#DDL_ZJLX").val(1);
        return;
    }

    $("#TB_JLBH").val(data.HYKXX.iGKID);
    $("#HF_GKID").val(data.HYKXX.iGKID);
    //member base infomation  
    $("#DDL_ZJLX").val(data.HYKXX.iZJLXID);
    if (data.HYKXX.iZJLXID == "") { $("#DDL_ZJLX").val(1); }//默认身份证
    if (data.HYKXX.iZJLXID == 1) {
        $("#CB_ZJLX").attr("checked", true)
    }
    else {
        $("#CB_ZJLX").attr("checked", false)
    }

    if (data.HYKXX.iBJ_YZZJLX == 1) {
        $("#CB_YZZJLX").attr("checked", true)
    }
    else {
        $("#CB_YZZJLX").attr("checked", false)
    }
    $("#TB_SFZBH").val(data.HYKXX.sSFZBH);
    //userIconUrl = "../../../HeadPhoto/" + data.HYKXX.sSFZBH + ".jpg";
    //$("#HeadPhoto").attr("src", userIconUrl + "?temp=" + Math.random());//userIconUrl
    // UploadInit();
    if ($("#DDL_ZJLX").val() != "1") {
        //document.getElementById("TB_CSRQ").disabled = false;
    }

    $("#TB_CSRQ").val(data.HYKXX.dCSRQ);
    //getSRXXMORE();
    $("#TB_HYNAME").val(data.HYKXX.sHY_NAME);

    $("[name='sex'][value='" + data.HYKXX.iSEX + "']").attr("checked", true);
    $("#HF_TJRID").val(data.HYKXX.iTJRYID);
    $("#TB_TJRMC").val(data.HYKXX.sTJRYMC);

    var jsonArticlesString = "\"Articles\":[{\"Id\":\"" + data.HYKXX.iTJRYID + "\",\"TB_RYDM\":\"\",\"TB_RYMC\":\"\",\"TB_PYM\":\"\"}]";
    var strRet = "{\"Depts\":[],\"Contracts\":[],\"Brands\":[],\"Categories\":[],\"Groups\":[]," + jsonArticlesString + ",\"PayTypes\":[],\"Banks\":[],\"BankCardCodeScopes\":[]}";
    $("#zHF_GKDA").val(strRet);

    if (data.HYKXX.iBJ_DKH == 1) {
        $("#CB_DKH").attr("checked", true)
    }
    else {
        $("#CB_DKH").attr("checked", false)
    }

    //member Communication infomation
    $("#TB_SJHM").val(data.HYKXX.sSJHM);
    if (data.HYKXX.sSJHM.substr(0, 1) == "0") {
        $("#CB_LNK").prop("checked", true);
        CB_LNK_Click();
    }
    $("#zHF_SJHM").val(data.HYKXX.sSJHM);
    if (data.HYKXX.iBJ_YZSJHM == 1) {
        $("#CB_YZSJHM").attr("checked", true)
    }
    else {
        $("#CB_YZSJHM").attr("checked", false)
    }
    if (data.HYKXX.sPHONE != "") {
        //var tp_1 = data.HYKXX.sPHONE.split("-");

        //$("#TB_PHONEHEAD").val(tp_1[0]);
        $("#TB_PHONE").val(data.HYKXX.sPHONE);
    }

    //document.getElementById("TB_CSRQ").disabled = true;
    $("#TB_QQ").val(data.HYKXX.sQQ);
    $("#TB_WX").val(data.HYKXX.sWX);
    $("#TB_WB").val(data.HYKXX.sWB);
    $("#TB_EMAIL").val(data.HYKXX.sEMAIL);
    $("#TB_YZBM").val(data.HYKXX.sYZBM);
    $("#HF_QYID").val(data.HYKXX.iQYID);
    $("#TB_TXDZ").val(data.HYKXX.sTXDZ);
    $("#TB_PPXQ").val(data.HYKXX.sPPXQ);
    $("#TB_GZDW").val(data.HYKXX.sGZDW);
    $("#TB_ROAD").val(data.HYKXX.sROAD);
    $("#TB_MPH").val(data.HYKXX.sHOUSENUM);
    $("#HF_XQID").val(data.HYKXX.iXQID);
    $("#TB_PPXQ").val(data.HYKXX.sXQMC);
    $("#TB_SW").val(data.HYKXX.sSW);
    $("#TB_CM").val(data.HYKXX.sCM);
    $("#TB_XZ").attr("readonly", "readonly").addClass("cs");
    $("#TB_XZ").val(data.HYKXX.sXZ);
    $("#TB_SX").attr("readonly", "readonly").addClass("cs");
    $("#TB_SX").val(data.HYKXX.sSX);
    $("#DDL_XL").val(data.HYKXX.iXLID);
    $("#DDL_JTSR").val(data.HYKXX.iJTSRID);
    $("#DDL_JTGJ").val(data.HYKXX.iJTGJID);
    $("#DDL_JTCY").val(data.HYKXX.iJTCYID);
    $("#S_QCPP").val(data.HYKXX.iQCPPID);
    $("#TB_CPH").val(data.HYKXX.sCPH);
    $("#TB_HYZK").val(data.HYKXX.iJHBJ);
    $("#TB_JHJNR").val(data.HYKXX.dJHJNR);
    if (data.HYKXX.sSFZBH == "") {

        //$("#CBL_XXFS").html("");
        //document.getElementById("CBL_XXFS").innerHTML = "";
        //CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));//华地 默认勾选 2014.11.7
    } else {
        getSRXXMORE();
        Set_CYCBL_Item("CBL_XXFS", data.HYKXX.sXXFS);
    }
    Set_CYCBL_Item("CBL_CXXX", data.HYKXX.sCXXX);
    Set_CYCBL_Item("CBL_YYAH", data.HYKXX.sYYAH);
    $("#R_CANSMS").prop("checked", data.HYKXX.iCANSMS);
    $("#R_NOSMS").prop("checked", data.HYKXX.iNOSMS);


    $("#TB_BZ").val(data.HYKXX.sBZ);
    $("#HF_RYXX_LX4").val(data.HYKXX.iKHJLRYID);
    $("#TB_RYXX_LX4").val(data.HYKXX.sKHJLMC);
    var jsonArticlesString = "\"Articles\":[{\"Id\":\"" + data.HYKXX.iKHJLRYID + "\",\"TB_RYDM\":\"\",\"TB_RYMC\":\"\",\"TB_PYM\":\"\"}]";
    var strRet = "{\"Depts\":[],\"Contracts\":[],\"Brands\":[],\"Categories\":[],\"Groups\":[]," + jsonArticlesString + ",\"PayTypes\":[],\"Banks\":[],\"BankCardCodeScopes\":[]}";
    $("#zHF_RYXX_LX4").val(strRet);

    $("#LB_DJRMC").text(data.HYKXX.sDJRMC);
    $("#HF_DJR").val(data.HYKXX.iDJR);
    $("#LB_DJSJ").text(data.HYKXX.dDJSJ);

    $("#LB_ZXRMC").text(data.HYKXX.sGXRMC);
    $("#HF_ZXR").val(data.HYKXX.iGXR);
    $("#LB_ZXRQ").text(data.HYKXX.dGXSJ);
    $("#HF_IMGURL").val(data.HYKXX.sIMGURL);
    //if (data.HYKXX.sIMGURL != "") {
    //    //var pturl = window.location.pathname.toUpperCase();
    //    //pturl = pturl.substr(0, pturl.indexOf("CRMWEB"));
    //    //dialogUrl = data.HYKXX.sIMGURL;
    //    //dialogUrl = dialogUrl.replace("../../..", pturl + "..");
    //    document.getElementById("HeadPhoto").src = data.HYKXX.sIMGURL;
    //}

    $('#list').datagrid('loadData', data.JTXX, "json");
    $('#list').datagrid("loaded");

    var treeStr = "";
    var treeObj = $.fn.zTree.getZTreeObj("TreeQY");
    var treeNode = treeObj.getNodeByParam("jlbh", data.HYKXX.iQYID);
    while (treeNode != null && treeNode.pId != "") {
        treeStr = treeNode.qymc + " " + treeStr;
        treeNode = treeObj.getNodeByParam("id", treeNode.pId);
    }
    $("#TB_QY").val(treeStr);
    $("#TB_ROAD").val(data.HYKXX.sROAD);
    $("#TB_MPH").val(data.HYKXX.sHOUSENUM);

    //other
    $("#DDL_ZY").val(data.HYKXX.iZYID);
    //var treeZY = $.fn.zTree.getZTreeObj("TreeZY");
    //if (treeZY) {
    //    var treeNodeZY = treeZY.getNodeByParam("jlbh", data.HYKXX.iZYID);
    //    if (treeNodeZY != null) {
    //        $("#TB_ZY").val(treeNodeZY.name);
    //    }
    //}


    //$("#CB_DKH").prop("disabled", true);//大客户标记不允许修改 无锡华地 2014.12.1

    //$("#DDL_ZJLX").attr("disabled", true);
    //$("#TB_SFZBH").attr("disabled", true);
    //$("#TB_CSRQ").attr("disabled", true);
    //// $("#TB_SJHM").attr("disabled", true);
    //$("#takePhoto").prop("disabled", true);
    //$("#TB_YZM").prop("disabled", true);
    //$("#Radio1").prop("disabled", true);
    //$("#Radio2").prop("disabled", true);
    //$("#TB_HYNAME").prop("disabled", true);
    isUpdate = true;
}
function zDataArrayToString(varray) {
    var tp_target = "";
    if (varray.length > 0) {
        for (var i = 0; i <= varray.length - 1; i++) {
            tp_target += varray[i] + ";";
        }
    }
    return tp_target.substr(0, tp_target.length - 1);
}
function zDataStringToArray(varray) {
    if (varray == "") { return new Array(); }
    return tp_str1 = varray.split(";");
    if (varray.length > 0) {
        for (var i = 0; i <= tp_str1.length - 1; i++) {
            tp_target.push(varray[i]);
        }
    }
    return tp_target;
}
function DDL_ZJLXChange() {
    if ($("#DDL_ZJLX").val() != 1) {
        //$("#TB_CSRQ").attr("disabled", false);
    }
    else
        $("#TB_CSRQ").attr("disabled", true);

    $("#TB_SFZBH").prop("disabeld", false);
    $("#TB_SFZBH").removeAttr("readonly").removeClass("cs");
}

function InsertClickCustom() {
    $("#TB_SFZBH").removeAttr("readonly").removeClass("cs");
    // $("#TB_SJHM").removeAttr("readonly").removeClass("cs");
    isUpdate = false;
    $("#DDL_ZJLX").attr("disabled", false);
    $("#TB_SFZBH").attr("disabled", false);
    $("#TB_CSRQ").attr("disabled", false);
    //  $("#TB_SJHM").attr("disabled", false);
    $("#takePhoto").prop("disabled", false);
    $("#TB_YZM").prop("disabled", false);
    $("#Radio1").prop("disabled", false);
    $("#Radio2").prop("disabled", false);
    $("#TB_HYNAME").prop("disabled", false);
}

function WUC_SPSB_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var insertWidth;
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#TA_PPHY").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
                insertWidth += contractValues[i].Name.length;

            }
            $("#TA_PPHY").val(tp_mc);
            $("#HF_PPHYID").val(tp_hf.substr(0, tp_hf.length - 1));
            $("#zHF_PPHYID").val(jsonString);
        }
    }
}

function GetHYXX() {
    if ($("#TB_CXHYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_CXHYKNO").val());
        if (str) {
            var Obj = JSON.parse(str);
            $("#TB_JLBH").val(Obj.iHYID);
            $("#HF_HYID").val(Obj.iHYID);
            $("#LB_HYKTYPE").text(Obj.iHYKTYPE);
            $("#LB_HYKNAME").text(Obj.sHYKNAME);
            $("#LB_STATUS").text(Obj.iSTATUS);

            //window.location("HYKGL_HYDALR.aspx?jlbh=" + Obj.iHYID);
            ShowDataBase(Obj.iHYID);
        }
    }
}

function onClick(e, treeId, treeNode) {
    if (treeId == "TreeQY") {
        $("#HF_QYID").val(treeNode.jlbh);
        var str = "";
        var treeObj = $.fn.zTree.getZTreeObj(treeId);
        while (treeNode != null && treeNode.pId != "") {
            str = treeNode.qymc + " " + str;
            treeNode = treeObj.getNodeByParam("id", treeNode.pId);
        }
        $("#TB_QY").val(str);
        $("#TB_PPXQ").val("");
        $("#HF_XQID").val("");
        $("#zHF_XQID").val("");
        hideMenu("menuContent");
    }
    if (treeId == "TreeZY") {
        $("#HF_ZYID").val(treeNode.jlbh);
        $("#TB_ZY").val(treeNode.name);

        hideMenu("menuContent_ZY");
    }
}


function setDisplay(id, is) {
    if ($("#" + id).css("display") == "none") {
        $("#" + id).css("display", "block");   //  显示
    } else {
        $("#" + id).css("display", "none");   //  隐藏
    }
    //
    $("html,body").animate({ scrollTop: $("#" + is).offset().top }, 1000);
    //window.location.hash = "id7";
}

function showBirthday(val) {
    var birthdayValue;
    if (15 == val.length) { //15位身份证号码
        birthdayValue = val.charAt(6) + val.charAt(7);
        if (parseInt(birthdayValue) < 10) {
            birthdayValue = '20' + birthdayValue;
        }
        else {
            birthdayValue = '19' + birthdayValue;
        }
        birthdayValue = birthdayValue + '-' + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11);
        if (parseInt(val.charAt(14) / 2) * 2 != val.charAt(14))//看最后一位，奇数男偶数女
        {
            //man
            $("[name='sex'][value='0']").attr("checked", true);
        }
        else {  //sex = '女';
            $("[name='sex'][value='1']").attr("checked", true);
        }
    }
    if (18 == val.length) { //18位身份证号码
        birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11) + '-' + val.charAt(12) + val.charAt(13);

        if (parseInt(val.charAt(16) / 2) * 2 != val.charAt(16))//看倒数第二位，奇数男偶数女
        {
            //man
            $("[name='sex'][value='0']").attr("checked", true);
        }
        else {  //sex = '女';
            $("[name='sex'][value='1']").attr("checked", true);
        }
    }
    $("#TB_CSRQ").val(birthdayValue);
    document.getElementById("TB_CSRQ").disabled = true;
    getSRXXMORE();
}


function Get_CYCBL_CheckItem(cbl_name) {
    var valuelist = "";
    $("input[name^='" + cbl_name + "']").each(function () {
        if (this.checked) {
            valuelist += $(this).val() + ";";
        }
    });
    if (valuelist.length > 0) {
        valuelist = valuelist.substring(0, valuelist.length - 1);
    }
    return valuelist;
}

function Set_CYCBL_Item(cbl_name, CBLHYBJ) {
    if (CBLHYBJ != null) {

        var splitArray = CBLHYBJ.split(";");
        var s1 = $("input[name^='" + cbl_name + "']").length;
        for (var i = 0; i <= s1 - 1; i++) {
            for (var j = 0; j <= splitArray.length - 1; j++) {
                if ($("#" + cbl_name + "_" + i).val() == splitArray[j]) {
                    $("#" + cbl_name + "_" + i).prop("checked", true);
                }
            }
        }

    }
}

//change identy card change
function Set_SfzEnalbe(isenable) {
    if (isenable == true) {

        $("#DDL_ZJLX").attr("disabled", false);

    }
    else {


    }
}

function CellFormatForParent(value) {
    if (value == "0") {
        return "子卡";
    }
    if (value == "1") {
        return "主卡";
    }
    if (value == "2")
        return "附属卡";
    return "";
}
function CellFormatForJZ(value) {
    if (value == "0") {
        return "磁卡";
    }
    if (value == "1") {
        return "IC";
    }
    if (value == "2") {
        return "磁卡读磁加密";
    }
    return "磁卡";
}
function CellFormatForYes(value) {
    if (value == "1") {
        return "开通";
    }
    else {
        return "未开通";
    }
}


function MakeSrchCondition(arrayObj, ElementName, FieldName, Sign, Quot) {
    if ($("#" + ElementName).val() != "") {
        var ObjJLBH = new Object();
        ObjJLBH.ElementName = FieldName;
        ObjJLBH.ComparisonSign = Sign;
        ObjJLBH.Value1 = $("#" + ElementName).val();
        ObjJLBH.InQuotationMarks = Quot;
        if (FieldName[0] == "d" && Sign == "<=") {
            //当日期条件为<=某天时，为避免时分秒的问题，强制转换成<某天+1
            var date = new Date(ObjJLBH.Value1);
            AddDays(date, 1);
            ObjJLBH.Value1 = FormatDate(date, "yyyy-MM-dd");
            ObjJLBH.ComparisonSign = "<";
        }
        arrayObj.push(ObjJLBH);
    }
}
function GetHYDALRDataMore(iHYID, sHYK_NO, sSJHM, sSFZBH, iGKID) {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_GKID", "iJLBH", "=", false);
    //sjson = "{'iHYID':" + iHYID + ",'sHYK_NO':'" + sHYK_NO + "','sSJHM':'" + sSJHM + "','sSFZBH':'" + sSFZBH + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: vUrl + "?func=" + vPageMsgID + "&mode=View",
        dataType: "json",
        async: false,
        data: { json: "", 'afterFirst': JSON.stringify(arrayObj), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            $("#TB_JLBH").val(0);
            result = "";
        }
    });
    return result;
}

function zDataToPage(gkid) {

    var tp_hyxx = GetHYDALRDataMore(0, "", "", "", gkid);//GetHYXXDataMore
    if (tp_hyxx == null || tp_hyxx == "") { zDataClear(); return; }
    ShowData(tp_hyxx);
}
function zDataClear() {
    $("#TB_SFZBH").val("");
    $("#TB_CSRQ").val("");
    $("#TB_HYNAME").val("");
    $("#TB_TJRMC").val("");
    $("#HF_TJRID").val("");
    $("#TB_SJHM").val("");
    $("#TB_PHONE").val("");
    $("#TB_QQ").val("");
    $("#TB_WX").val("");
    $("#TB_WB").val("");
    $("#TB_EMAIL").val("");
    $("#TB_YZBM").val("");
    $("#TB_TXDZ").val("");
    $("#TB_PPXQ").val("");
    $("#TB_GZDW").val("");
    $("#TB_SW").val("");
    $("#TB_CM").val("");
    $("#TB_XZ").val("");
    $("#TB_SX").val("");
    //other
    $("#DDL_ZY").val("");
    $("#DDL_XL").val("");
    $("#DDL_JTSR").val("");
    $("#DDL_JTGJ").val("");
    $("#DDL_JTCY").val("");
    $("#TB_CPH").val("");
    $("#TA_BZ").val("");
    $("#LB_DJRMC").text("");
    $("#HF_DJR").val("");
    $("#LB_DJSJ").text("");
    $("#LB_GXRMC").text("");
    $("#HF_GXR").val("");
    $("#LB_GXSJ").text("");
}
function zDatamult(value) {
    var tp_id = "";
    var tp_name = "";
    var tp_zhf = "";

    if (value != "") {
        for (var i = 0; i <= value.split(';').length - 1; i++) {
            if (value.split(';').length == 1) {
                tp_id += value.split(';')[i].split(':')[0];
                tp_name += value.split(';')[i].split(':')[1];
                tp_zhf += "{\"Id\":\"" + value.split(';')[i].split(':')[0] + "\",\"Name\":\"" + value.split(';')[i].split(':')[1] + "\"}";
            }
            else {
                tp_id += value.split(';')[i].split(':')[0] + ";";
                tp_name += value.split(';')[i].split(':')[1] + ";";
                tp_zhf += "{\"Id\":\"" + value.split(';')[i].split(':')[0] + "\",\"Name\":\"" + value.split(';')[i].split(':')[1] + "\"},";
            }
        }
        $("#HF_SPSB").val(tp_id.substring(0, tp_id.lastIndexOf(';')));
        $("#TB_SPSB").val(tp_name.substring(0, tp_id.lastIndexOf(';')));
    }

    var jsonArticlesString = "\"Articles\":[" + tp_zhf.substring(0, tp_zhf.lastIndexOf(',')) + "]";
    var strRet = "{\"Depts\":[],\"Contracts\":[],\"Brands\":[],\"Categories\":[],\"Groups\":[]," + jsonArticlesString + ",\"PayTypes\":[],\"Banks\":[],\"BankCardCodeScopes\":[]}";
    $("#zHF_SPSB").val(strRet);
}
function zDatasimple(zvalue) {
    if (zvalue == "") { return ""; }
    var jsonInput = JSON.parse(zvalue);
    ArticlesValues = jsonInput.Articles;

    var tp_1 = "";
    for (var i = 0; i <= ArticlesValues.length - 1; i++) {
        tp_1 += ArticlesValues[i].Id + ":" + ArticlesValues[i].Name + ";";
    }
    return tp_1.substring(0, tp_1.lastIndexOf(';'));
}

function ReBind_ETHD() {
    ReloadList("GetKHDAETHDList", "listETHD", { iHYID: $("#HF_GKID").val() })
}
function ReBind_WDTJ() {
    ReloadList("GetKHDATJList", "listWDTJ", { iHYID: $("#HF_GKID").val() })
}
function ReBind_WDQZ() {
    ReloadList("GetKHDAQZList", "listWDQZ", { iHYID: $("#HF_GKID").val() })
}

function ReloadList(func, listName, Params) {
    $('#' + listName + '').datagrid("loading");
    PostToCrmlib(func, Params, function (data) {
        $('#' + listName + '').datagrid('loadData', data, "json");
        $('#' + listName + '').datagrid("loaded");
    }, false);
}

function EnterPress(e) {
    var e = e || window.event;
    if (e.keyCode == 13) {
        $("#B_CXHYKNO").click();
    }
}

//-------------------------------------SPSB-------------------------
function WUC_SPSB_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#TB_SPSB").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                if (tp_return_ChoiceOne) {
                    tp_hf += contractValues[i].Id;
                } else {
                    tp_hf += contractValues[i].Id + ";";
                }
            }
            $("#TB_SPSB").val(tp_mc);
            $("#HF_SPSB").val(tp_hf);
            $("#zHF_SPSB").val(jsonString);
        }
    }
}
//-------------------------------------YZSFZ-------------------------
$(document).ready(function () {
    $("#B_YZSFZ").click(function () {
        $.dialog.data('IpValuesSFZ', $("#TB_SFZBH").val());
        $.dialog.open("../../WUC/YZSFZ/WUC_YZSFZ.aspx", {
            lock: true, width: 400, height: 400, cancel: false
            , close: function () {
                WUC_YZSFZ_Return();
            }
        }, false);
    });
});
function WUC_YZSFZ_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');

    if (tp_return != "") {
        if ($("#DDL_ZJLX").val() == 1) {
            $("#HF_YZSFZ").val(tp_return);
            $("#TB_SFZBH").val(tp_return);
            $("#CB_YZZJLX").attr("checked", true);
            showBirthday(tp_return);

        }
        else {
            $("#CB_YZZJLX").attr("checked", false);
        }
    }
}
//-------------------------------------GKDA-------------------------
$(document).ready(function () {

    $("#B_TJR").click(function () {
        SelectGKXX("TB_TJRMC", "HF_TJRID", "zHF_TJRID", true);
    });
});

//-------------------------------------YZSJHM-------------------------
$(document).ready(function () {
    $("#B_YZSJHM").click(function () {
        $.dialog.data('IpValuesSFZ', $("#TB_SJHM").val());
        $.dialog.open("../../WUC/YZSJHM/WUC_YZSJHM.aspx", {
            lock: true, width: 400, height: 400, cancel: false
            , close: function () {
                WUC_YZSJHM_Return();
            }
        }, false);
    });


    $("#btnYZM").click(function () {
        var iYZM = 10000 + Math.ceil(Math.random() * 9999);
        var sYZM = iYZM.toString().substr(1);
        var tInterval;
        //if (true) {
        if (SendSMS($("#TB_SJHM").val(), sYZM) == "0") {
            $("#HF_YZM").val(sYZM);
            $("#btnYZM").attr("disabled", "disabled");
            tInterval = setInterval(function () {
                settime()
            }, 1000)
        }
        $("#HF_SJHM").val($("#TB_SJHM").val());


        function settime() {
            if (countdown == 0) {
                $("#btnYZM").removeAttr("disabled");
                $("#btnYZM").val("验证码");
                countdown = 60;
                clearInterval(tInterval);
            } else {
                $("#btnYZM").attr("disabled", "disabled");
                $("#btnYZM").val("重发(" + countdown + ")");
                countdown--;
            }
        }
    });




});
function WUC_YZSJHM_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');

    if (tp_return != "") {
        $("#TB_SJHM").val(tp_return);
        $("#CB_YZSJHM").attr("checked", true);
    }
    else {
        //$("#TB_SJHM").val(tp_return);
        $("#CB_YZSJHM").attr("checked", false);
    }
}
function matchVillage() {
    var url = "../../CrmLib/CrmLib.ashx?func=FillXQ";
    var qyid = $("#HF_QYID").val();
    var txdz = $("#TB_TXDZ").val();
    $.getJSON(url, { iQYID: qyid, }, function (data) {
        $.each(data, function (i, n) {
            if (txdz.indexOf(data[i].sXQMC) >= 0) {
                $("#TB_PPXQ").val(data[i].sXQMC);
                $("#HF_XQID").val(data[i].iXQID);
            } else {
                if (data[i].sXQMC == "默认小区") {
                    $("#TB_PPXQ").val(data[i].sXQMC);
                    $("#HF_XQID").val(data[i].iXQID);
                }
            }
        });
    });
}


//-------------------------------------ryxx-------------------------

function addRowFcn() {
    var tp_JTXM = $("#TB_JTXM").val();
    var tp_JTGX = $("#DDL_JTGX   option:selected").text();
    var tp_JTXB = $("#DDL_JTXB   option:selected").text();
    var tp_JTNL = $("#TB_JTNL").val();
    var tp_JTSR = $("#TB_JTSR").val();
    if (!IsNumber(tp_JTNL)) { ShowMessage("年龄只能输入数字！", 3); return false; }
    var tp_flag = false;
    var rows = $('#list').datagrid("getRows");
    for (var j = 0; j < rows.length; j++) {
        if (tp_JTXM == rows.JTXM && tp_JTGX == rows.JTGX) {
            tp_flag = true;
        }
    }
    if (tp_flag == false) {
        $('#list').datagrid('appendRow', { JTXM: tp_JTXM, JTGX: tp_JTGX, JTXB: tp_JTXB, JTNL: tp_JTNL, JTSR: tp_JTSR });
    }
    else {
        ShowMessage("已添加过改成员", 3);
    }
}


function getGKDA(sfzbh, sjhm, isUpdate) {

    if (!sfzbh && !sjhm) {

        return;
    }
    var str = GetGKDAData(0, sfzbh, sjhm);
    if (str == "null" || str == "") {
        if (sfzbh != "" && sfzbh.charAt(sfzbh.length - 1) == "x") {
            sfzbh = sfzbh.toUpperCase();
            $("#TB_SFZBH").val(sfzbh);
            getGKDA($("#TB_SFZBH").val(), "");
        }
        //检查手机号码是否重复
        if (isUpdate) {
            return;
        }

        //$("#List_HYK").clearGridData();
        //$("#List_JT").clearGridData();
        $('#List_HYK').datagrid("loadData", { total: 0, rows: [] });
        $('#listWDQZ').datagrid("loadData", { total: 0, rows: [] });
        $('#list').datagrid("loadData", { total: 0, rows: [] });
        $('#listETHD').datagrid("loadData", { total: 0, rows: [] });


        var zjlxid = $("#DDL_ZJLX").val();
        // PageDate_Clear();
        $("#DDL_ZJLX").val(zjlxid);
        $("#TB_SFZBH").val(sfzbh || $("#TB_SFZBH").val());
        $("#TB_SJHM").val(sjhm || $("#TB_SJHM").val());
        if ($("#HF_GKID").val() == "") {
            $("#LB_GKXX").text("新顾客");
        }
        else { }
        if ($("#DDL_ZJLX").val() != "1") {
            document.getElementById("TB_CSRQ").disabled = false;
        }

        return;
    }
    else {
        var Obj = JSON.parse(str);
        if (sfzbh == "" && sjhm != "" && isUpdate && $("#HF_GKID").val() != Obj.iGKID && $("#HF_GKID").val() != "") {
            ShowMessage("手机号码重复", 3);
            $("#TB_SJHM").val("");
            return;
        }
        //检查身份证是否重复
        if (sjhm == "" && sfzbh != "" && isUpdate && $("#HF_GKID").val() != Obj.iGKID && $("#HF_GKID").val() != "") {
            $("#TB_SFZBH").val("");
            ShowMessage("证件号已重复", 3);
            return;
        }
        ShowGKDA(str);
    }


}
function getSRXXMORE() {
    if ($("#TB_CSRQ").val() != "" && $("#TB_CSRQ").val() != "1900-01-01") {
        var str = GetSRXX($("#TB_CSRQ").val());
        var Obj = JSON.parse(str);
        $("#TB_SX").val(Obj.sSX);
        $("#TB_XZ").val(Obj.sXZ);
        $("#TB_SX").attr("readonly", "readonly").addClass("cs");
        $("#TB_XZ").attr("readonly", "readonly").addClass("cs");
    }
}
function ShowGKDA(str) {
    if (!str || str == "null") {
        return;
    }

    var zjlxid = $("#DDL_ZJLX")[0].value;

    //$('#List_HYK').datagrid('loadData', "", "json");
    //$('#List_JT').datagrid('loadData', "", "json");

    $("#DDL_ZJLX").val(zjlxid);
    //  PageDate_Clear();
    $("#LB_GKXX").text("老顾客");
    var Obj = JSON.parse(str);
    vJLBH = Obj.iGKID;

    $("#TB_JLBH").val(Obj.iGKID)
    $("#HF_GKID").val(Obj.iGKID);
    $("#TB_HYNAME").val(Obj.sGK_NAME);
    $("#DDL_ZJLX").val(Obj.iZJLXID);
    $("#TB_SFZBH").val(Obj.sSFZBH);
    //userIconUrl = "../../../HeadPhoto/" + Obj.sSFZBH + ".jpg";
    //$("#HeadPhoto").attr("src", userIconUrl + "?temp=" + Math.random());//userIconUrl
    if (vHYK_NO == "" || vHYK_NO == undefined) {
        $("#TB_SFZBH").attr("readonly", "readonly");
    }
    $("[name='sex'][value='" + Obj.iSEX + "']").attr("checked", true);
    $("#TB_CSRQ").val(Obj.dCSRQ);

    //getSRXXMORE();
    $("[name='cld'][value='" + Obj.iBJ_CLD + "']").attr("checked", true);
    $("#HF_QYID").val(Obj.iQYID);
    var treeObj = $.fn.zTree.getZTreeObj("TreeQY");
    var node = treeObj.getNodeByTId(Obj.iQYID.toString());
    if (node != null) { $("#TB_QY").val(node.qymc); }
    $("#TB_SJHM").val(Obj.sSJHM);

    //$("C_CANSMS")

    var index = Obj.sPHONE.indexOf("-");
    if (index > 0) {
        $("#TB_PHONEHEAD").val(Obj.sPHONE.substring(0, index));
        $("#TB_PHONE").val(Obj.sPHONE.substring(index + 1));
    }
    else
        $("#TB_PHONE").val(Obj.sPHONE);
    //$("#TB_BGDH").val(Obj.sBGDH);
    $("#TB_TJRMC").val(Obj.sTJRYMC);
    $("#HF_TJRID").val(Obj.iTJRYID);
    $("#TB_QQ").val(Obj.sQQ);
    $("#TB_WX").val(Obj.sWX);
    $("#TB_WB").val(Obj.sWB);
    $("#TB_PPXQ").val(Obj.sPPXQ);
    $("#HF_XQID").val(Obj.iQYID);
    $("#TB_EMAIL").val(Obj.sEMAIL);
    $("#TB_YZBM").val(Obj.sYZBM);
    $("#TB_TXDZ").val(Obj.sTXDZ);
    $("#TB_JHJNR").val(Obj.dJHJNR);
    $("#TB_GZDW").val(Obj.sGZDW);
    //$("#DDL_MZ").val(Obj.iMZID);
    $("#DDL_ZY").val(Obj.iZYID);
    $("#DDL_XL").val(Obj.iXLID);
    $("#DDL_JTSR").val(Obj.iJTSRID);
    $("#DDL_JTCY").val(Obj.iJTCYID);
    $("#S_QCPP").val(Obj.iQCPPID);
    $("#TB_HYZK").val(Obj.iJHBJ);
    $("#DDL_ZSCSJ").val(Obj.iZSCSJID);
    $("#DDL_JTGJ").val(Obj.iJTGJID);
    $("#TB_CPH").val(Obj.sCPH);
    $("#TA_BZ").val(Obj.sBZ);
    //sxxfs scxxx syyah
    //查出所有的会员卡
    $("#HF_GKID").val(Obj.iGKID);
    $("#TB_JLBH").val(Obj.iGKID);
    //$("#HF_HYID").val(Obj.iHYID);
    ShowDataBase(Obj.iGKID);// ShowData 有部分与上面的显式重复，可以将上面重复的部分去除

}
//function MakeJLBH(t_jlbh) {
//    //生成iJLBH的JSON
//    var Obj = new Object();
//    Obj.iJLBH = t_jlbh;
//    Obj.sHYK_NO = vHYK_NO;
//    if (GetUrlParam("mzk") == "1") {
//        Obj.sDBConnName = "CRMDBMZK";
//    }
//    return Obj;
//}
function UpdateClickCustom() {
    //isUpdate = true;
    //document.getElementById("TB_CSRQ").disabled = true;
    //var tp_lnk = $("#CB_LNK").prop("checked");
    //if (tp_lnk) {
    //    CB_LNK_Click();
    //}
    //$("#DDL_ZJLX").attr("disabled", true);
    //$("#TB_SFZBH").attr("disabled", true);
    //$("#TB_CSRQ").attr("disabled", true);
    //// $("#TB_SJHM").attr("disabled", true);
    //$("#takePhoto").prop("disabled", true);
    //$("#TB_YZM").prop("disabled", true);
    //$("#Radio1").prop("disabled", true);
    //$("#Radio2").prop("disabled", true);
    //$("#TB_HYNAME").prop("disabled", true);
}


