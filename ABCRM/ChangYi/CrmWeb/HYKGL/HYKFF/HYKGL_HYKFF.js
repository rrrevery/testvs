vUrl = "../HYKGL.ashx";
var countdown = 60;
var pPSW = 0;
var timeCDNR = setInterval(function () { $("#TB_CDNR").focus(); }, 1000);
vCaption = "单张会员卡发放";
var BK = true;
var pBJ_TJM;
var pBJ_XSK;
var pBJ_FSK;
var pBJ_SYK;
var vCommunityArray = new Array();

function InitGrid() {
    vColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称", "收款金额", "交易号"],
    vColumnModel = [
          { name: "iZFFSID", hidden: true },
          { name: "sZFFSDM", width: 150 },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text' },
    ];
};

$(document).ready(function () {
    window.clearInterval(timeCDNR);
    //$("#B_Insert").hide();
    $("#B_Save").text("发卡");
    $("#B_Cancel").text("清空");
    $("#B_Print").show();
    FillQYTree("TreeQY", "TB_QY");
    FillBGDDTreeSK("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", vCZK == "1" ? 2 : 1);//会员卡建卡
    FillFXDWTree("TreeFXDW", "TB_FXDWMC");
    FillSelect("DDL_ZY", GetHYXXXM(1));
    //FillSelect("DDL_ZJLX", GetHYXXXM(0));
    $("#DDL_ZJLX").val(1);
    FillSelect("DDL_JTSR", GetHYXXXM(2));
    FillSelect("DDL_XL", GetHYXXXM(3));
    FillSelect("DDL_JTGJ", GetHYXXXM(5));
    FillSelect("S_QCPP", GetHYXXXM(11));
    CYCBL_ADD_ITEM("CBL_YYAH", GetHYXXXM(7));
    CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));
    CYCBL_ADD_ITEM("CBL_CXXX", GetHYXXXM(9));


    $("#btnICK").click(function () {
        sCDNR = rwcard.ReadCard("2;2;1,9600,n,8,1");
        if (sCDNR != "") {
            $("#TB_CDNR").val(";" + sCDNR + "?");
            ProcIt();
        }
    });

    //$("#TB_HYKNO").bind('keypress', function (event) {//#TB_CDNR,
    //    if (event.keyCode == "13") {
    //        GetKCKXX($("#TB_HYKNO").val(), "");
    //        document.getElementById("BJ_MMBS").disabled = true;
    //        $("#CBL_YYAH").html("");
    //        CYCBL_ADD_ITEM("CBL_YYAH", GetHYXXXM(7));
    //        $("#CBL_XXFS").html("");
    //        CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));
    //        $("#CBL_CXXX").html("");
    //        CYCBL_ADD_ITEM("CBL_CXXX", GetHYXXXM(9));
    //        $("#TA_BZ").val("");
    //    }
    //});

    //$("#TB_BGDDMC").click(function () {
    //    SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", true);
    //});

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
                $("#btnYZM").attr("disabled", "disabled");;
                $("#btnYZM").val("重发(" + countdown + ")");
                countdown--;
            }
        }
    });

    //document.getElementById("TB_HYKNO").onfocus = function () {
    //    window.clearInterval(timeCDNR);
    //};

    //$("#TB_HYKNO").change(function () {
    //    GetKCKXX($("#TB_HYKNO").val(), "");
    //})

    $("#TB_CDNR").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            $("#CBL_YYAH").html("");
            CYCBL_ADD_ITEM("CBL_YYAH", GetHYXXXM(7));
            $("#CBL_XXFS").html("");
            CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));
            $("#CBL_CXXX").html("");
            CYCBL_ADD_ITEM("CBL_CXXX", GetHYXXXM(9));
            $("#TA_BZ").val("");
            ProcIt();
        }
    });

    $("#TB_SFZBH").bind('keypress', function (event) {
        if (event.keyCode == 13) {
            JudgeCardNumber();
        }
    });

    $("#TB_SFZBH").change(function () {
        JudgeCardNumber();
    });

    $("#TB_CSRQ").change(function () {
        getSRXXMORE();
    });

    $("#DDL_ZJLX").change(function () {
        var vSFZ = $("#DDL_ZJLX").val() == 1;
        $("#TB_CSRQ").prop("disabled", vSFZ);
        $("#TB_XZ").prop("disabled", vSFZ);
        $("#TB_SX").prop("disabled", vSFZ);
        $("#Radio1").prop("disabled", vSFZ);
        $("#Radio2").prop("disabled", vSFZ);
    });

    $("#TB_SJHM").change(function () {
        if ($("#TB_SJHM").val() == "") {
            ShowMessage("请输入手机号码", 3);
            return;
        }
        getGKDA("", $("#TB_SJHM").val());
        if ($("#DDL_ZJLX").val() == 1) {
            showBirthday($("#TB_SFZBH").val());
        }
    });


    $("#TB_PPXQ").click(function () {
        var condData = new Object();
        if ($("#HF_QYID").val() != "") {
            condData["iQYID"] = $("#HF_QYID").val();
        }
        SelectXQ("TB_PPXQ", "HF_XQID", "zHF_XQID", true, condData);
    });

    RefreshButtonSep();

    $("#takePhoto").click(function () {
        TakePhoto("HeadPhoto", "HF_IMGURL");
    });

    //$("#Idcard()").click(function () {
    //    var str = SynCardOcx1.FindReader();
    //    if (str > 0) {
    //        var nRet = SynCardOcx1.ReadCardMsg();
    //        if (nRet == 0) {
    //            $("#TB_HYNAME").val(SynCardOcx1.NameA);
    //            $("#TB_CSRQ").val(SynCardOcx1.Born.substr(0, 4) + "-" + SynCardOcx1.Born.substr(4, 2) + "-" + SynCardOcx1.Born.substr(6, 2));
    //            $("#TB_SFZBH").val(SynCardOcx1.CardNo);
    //            if (SynCardOcx1.Sex == 1) {
    //                document.getElementById("Radio1").disabled = false;
    //                $("#Radio1").click();
    //            }
    //            else {
    //                document.getElementById("Radio2").disabled = false;
    //                $("#Radio2").click();
    //            }
    //            getGKDA(SynCardOcx1.CardNo);
    //        }
    //    }
    //    else {
    //        showMessage("未找到读卡设备，请检查设备连接！", 3);
    //    }
    //});

    $("#TB_KHJLRYMC").click(function () {
        SelectYWY("TB_KHJLRYMC", "HF_KHJLRYID", "zHF_KHJLRYID", true);
    });

    //SearchData();
})

function JudgeCardNumber(cardNumber) {
    var tmsg = ""
    if ($("#TB_SFZBH").val() == "") {
        ShowMessage("请输入证件号码", 3);
        return;
    }
    if ($("#DDL_ZJLX").val() == 1) {
        tmsg = IsIDCard("证件号码", $("#TB_SFZBH").val());
        if (tmsg != "") {
            ShowMessage(tmsg, 3);
        }
        else {
            var val = $("#TB_SFZBH").val();
            birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9);
            var currentYear = new Date().getFullYear();
            if ((parseInt(currentYear) - parseInt(birthdayValue) > 22) && parseInt(pBJ_XSK) == 1) {
                ShowMessage("年龄超过限制，无法办理学生卡", 3);
                BK = false;
                return;
            }
            $("#HF_HYID").val("");
            $("#HF_GKID").val("");
            //$("#TB_HYKNO").attr("disabled", "disabled");
            $("#B_Cancel").attr("disabled", false);
            getGKDA($("#TB_SFZBH").val(), "");
            if (BK) {
                showBirthday($("#TB_SFZBH").val());
                //$("#TB_HYKNO").removeAttr("disabled");
            }
        }
    }

    $("#CBL_YYAH").html("");
    CYCBL_ADD_ITEM("CBL_YYAH", GetHYXXXM(7));
    $("#CBL_XXFS").html("");
    CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));
    $("#CBL_CXXX").html("");
    CYCBL_ADD_ITEM("CBL_CXXX", GetHYXXXM(9));
    $("#TA_BZ").val("");
}

function ProcIt() {
    var inx1 = $("#TB_CDNR").val().indexOf(";");
    var inx2 = $("#TB_CDNR").val().indexOf("+");
    var inx3 = $("#TB_CDNR").val().indexOf("?");
    var inx4 = $("#TB_CDNR").val().indexOf("?", inx2);
    var cdnr = $("#TB_CDNR").val();
    cdnr = cdnr.substring(inx1 + 1, inx3 >= 0 ? inx3 : cdnr.length);
    //var hykno = $("#TB_CDNR").val().substring(inx2 + 1, inx4);
    if (cdnr == "") {
        ShowMessage("磁道内容不合法");
        $("#TB_CDNR").val("");
        $("#TB_CDNR").focus();
        return false;
    }
    GetKCKXX("", cdnr);
    window.clearInterval(timeCDNR);
    $("#TB_CDNR").val("");
    $("#btnYZM").removeAttr("disabled");
    document.getElementById("BJ_MMBS").disabled = true;
}

function AddCustomerButton() {
    //AddToolButtons("IC卡", "btnICK");
   // AddToolButtons("写卡", "btnWriteCard", "bftoolbtn fa fa-credit-card");
}

function SetControlState() {
    //$("#btn-toolbar div").hide();
    //$("#TB_HYKNO").val("");
    $("#TB_CDNR").val("");
    $("#TB_CDNR").focus();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();
    if ($("#B_Save").prop("disabled") == true) {
        //$("#TB_CDNR").focus();
        //$("#TB_HYKNO").attr("disabled", "disabled");
        $("#TB_SFZBH").val("");
        //$("#DDL_ZJLX").removeAttr("disabled");
        $("#zMP1_Hidden").find("input,select").attr("disabled", true);
        $("#zMP2_Hidden").find("input,select").attr("disabled", true);
        $("#zMP3_Hidden").find("input,select").attr("disabled", true);
        $("#zMP4_Hidden").find("input,select").attr("disabled", true);
    }
    else {
        $("#zMP1_Hidden").find("input,select").attr("disabled", false);
        $("#zMP2_Hidden").find("input,select").attr("disabled", false);
        $("#zMP3_Hidden").find("input,select").attr("disabled", false);
        $("#zMP4_Hidden").find("input,select").attr("disabled", false);
    }
    document.getElementById("BJ_MMBS").disabled = true;

    //$("#B_Save").attr("disabled", true);
    //$("#B_Cancel").attr("disabled", true);
    //PageControl(false, false);
}

function IsValidData() {
    var tp_msg = "";
    var tp_zk = 0;
    if (parseInt($("#HF_HYID").val()) != 0 && $("#HF_HYID").val() != "" && parseInt($("#HF_FXDWID").val()) == 2) {
        tp_zk = 1;
    }
    //if (tp_zk != 1) {
    //    if ($("#TB_YZM").val() == "") {
    //        ShowMessage("请输入验证码");
    //        return false;
    //    }
    //    if ($("#HF_YZM").val() != $("#TB_YZM").val()) {
    //        ShowMessage("验证码输入错误");
    //        return false;
    //    }

    //    if ($("#HF_SJHM").val() != $("#TB_SJHM").val()) {
    //        ShowMessage("手机号和接受验证码的手机号不一致");
    //        return false;
    //    }
    //}
    //if ($("#HF_GKID").val() != "" && $("#HF_FXDWID").val() == 1) {
    //    ShowMessage("已经存在此手机号/证件号的信息，不能发放会员卡");
    //    return false;
    //}
    if ($("#SP_BJ_NAME").is(":visible") == true) { tp_msg += zIsNull("姓名", $("#TB_HYNAME").val()); }
    if ($("#SP_BJ_SJHM").is(":visible") == true) { tp_msg += zIsNull("手机号码", $("#TB_SJHM").val()); }
    if ($("#SP_BJ_QQ").is(":visible") == true) { tp_msg += zIsNull("QQ号", $("#TB_QQ").val()); }
    if ($("#SP_BJ_WX").is(":visible") == true) { tp_msg += zIsNull("微信号", $("#TB_WX").val()); }
    // if ($("#SP_BJ_TXDZ").is(":visible") == true) { tp_msg += zIsNull("通讯地址", $("#TB_TXDZ").val()); }
    if ($("#SP_BJ_GZDW").is(":visible") == true) { tp_msg += zIsNull("工作单位", $("#TB_GZDW").val()); }
    if ($("#SP_BJ_ZY").is(":visible") == true) { tp_msg += zIsNull("备注", $("#TB_BZ").val()); }
    if ($("#SP_BJ_JTGJ").is(":visible") == true) { tp_msg += zIsNull("交通工具", $("#DDL_JTGJ").val()); }



    //member base infomation 
    tp_msg += zIsNull("证件类型", $("#DDL_ZJLX")[0].value);
    //tp_msg += zIsNull("证件号码", $("#TB_SFZBH").val());
    if ($("#DDL_ZJLX").val() == 1)
        tp_msg += IsIDCard("证件号码", $("#TB_SFZBH").val());
    //tp_msg += zIsNull("手机号码", $("#TB_SJHM").val());


    //tp_msg += zIsNull("出生日期", $("#TB_CSRQ").val());
    tp_msg += zIsCSRQ("出生日期", $("#TB_CSRQ").val());
    tp_msg += zIsValidvar("姓名", $("#TB_HYNAME").val());
    //member Communication infomation
    tp_msg += zIsTelePhone("手机号码", $("#TB_SJHM").val());
    tp_msg += zIsInt("住宅电话区号", $("#TB_PHONEHEAD").val());
    tp_msg += zIsInt("住宅电话号码", $("#TB_PHONE").val());
    tp_msg += zIsInt("QQ号", $("#TB_QQ").val());
    //tp_msg += zIsInt("微信", $("#TB_WX").val());
    //tp_msg += zIsInt("微博", $("#TB_WB").val());
    tp_msg += zIsEMail("E-MAIL", $.trim($("#TB_EMAIL").val()));
    tp_msg += zIsInt("邮编", $("#TB_YZBM").val());
    //   tp_msg += zIsValidvar("通讯地址", $("#TB_TXDZ").val());
    //tp_msg += zIsNull("匹配小区", $("#TB_PPXQ").val());
    //tp_msg += zIsValidvar("匹配小区", $("#TB_PPXQ").val());
    tp_msg += zIsValidvar("工作单位", $("#TB_GZDW").val());

    //if ($("#LB_QDBH").html() == "") {
    //    tp_msg = "入会渠道不能为空！";
    //    art.dialog({ lock: true, content: tp_msg });
    //    return false;
    //}

    if (tp_msg != "") {
        ShowMessage(tp_msg, 3);
        return false;
    }
    //身份证号与手机号确定顾客档案唯一性(分别)
    //var str = GetGKDAData(0, $("#TB_SFZBH").val(), "");
    //if (str != "null") {
    //    //var Obj = JSON.parse(str);
    //    //if (Obj.sSJHM != $("#TB_SJHM").val() && Obj.sSJHM!="") {
    //    //    tp_msg = "对应身份证号的手机号码与数据库中不符！";
    //    //    art.dialog({ lock: true, content: tp_msg });
    //    //    return false;
    //    //}
    //    ShowMessage("身份证号已存在");
    //    return false;
    //}

    //str = GetGKDAData(0, "", $("#TB_SJHM").val());
    //if (str != "null") {
    //    //Obj = JSON.parse(str);
    //    //if (Obj.sSFZBH != $("#TB_SFZBH").val() && Obj.sSFZBH != "") {
    //    //    art.dialog({ lock: true, content: "对应手机号的身份证号码与数据库中不符！", });
    //    //    return false;
    //    //}
    //    ShowMessage("手机号码已存在");
    //    return false;
    //}

    //if (CheckBGDDQX($("#LB_BGDDDM").text(), iDJR) == "False") {
    //    art.dialog({ lock: true, content: "该操作员没有该保管地点的操作权限！", });
    //    return false;
    //}


    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iFS = 0;
    Obj.iSKSL = 1;
    Obj.fDZKFJE = $("#LB_GBF").text();
    Obj.fYSZE = $("#LB_GBF").text();
    Obj.fSSJE = $("#LB_GBF").text();
    Obj.dYXQ = $("#LB_YXQ").text();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iBJ_FAST = 0;
    Obj.sZY = "单张卡发放";
    Obj.iBJ_PSW = $("#BJ_MMBS").prop("checked") ? 1 : 0;
    Obj.iBJ_TJM = pBJ_TJM;

    //var obj2 = new Object();
    //obj2.sCZKHM = $("#LB_HYKNO").text();
    //obj2.sCDNR = $("#TB_CDNR").val();
    //obj2.iHYKTYPE = $("#LB_HYKTYPE").text();
    //obj2.dYXQ = $("#LB_YXQ").text();
    //Obj.itemTable = obj2;

    Obj.iMDID = $("#HF_MDID").val();
    //if (Obj.TB_CDNR.substr(0, 1) == "?" || Obj.TB_CDNR.substr(0, 1) == "!")
    //{ Obj.TB_CDNR = HYKCDNR.TB_CDNR.slice(1, -1); }
    //Obj.HF_FXDW = $("#HF_FXDWID").val();
    var obj3 = new Object();
    obj3.iGKID = $("#HF_GKID").val() || "0";
    obj3.sHYK_NO = $("#LB_HYKNO").text();
    //obj3.iHYKTYPE = $("#LB_HYKTYPE").text();
    obj3.iHYKTYPE = $("#HF_HYKTYPE").text();
    obj3.dYXQ = $("#LB_YXQ").text();
    //Obj.CB_ISCHILD = ($("#CB_CONGKA").is(":checked")) ? 1 : 0;
    //Obj.CB_FFSX = ($("#CB_NBFF").is(":checked")) ? 1 : 0;
    //Obj.B_KFRYID = $("#LB_KHJLID").text();
    //Obj.B_RHQDID = $("#LB_QDBH").text();
    //-------------------------------------------------------------
    obj3.sHY_NAME = $("#TB_HYNAME").val();
    //obj3.iZJLXID = ($("#DDL_ZJLX").val() != "") ? $("#DDL_ZJLX").val() : "-1";//select控件为空，返回"" or null?
    //GetSelectValue("DDL_CLFS") == null ? 0 : GetSelectValue("DDL_CLFS")
    obj3.iZJLXID = ($("#DDL_ZJLX").val() == "" || $("#DDL_ZJLX").val() == null) ? "-1" : $("#DDL_ZJLX").val();
    obj3.sSFZBH = $("#TB_SFZBH").val();
    //Obj.RB_SEX = $("input[name='RB_SEX']:checked").val();
    obj3.iSEX = $("[name='sex']:checked").val();//($("#DDL_SEX").val() == "" || $("#DDL_SEX").val() == null) ? "-1" : $("#DDL_SEX").val();
    obj3.iTJRYID = $("#HF_GKDA").val() ? $("#HF_GKDA").val() : "-1";//推荐人
    obj3.iKHJLRYID = $("#HF_KHJLRYID").val() ? $("#HF_KHJLRYID").val() : "-1";//推卡经理人
    //-------------------------------------------------------------
    obj3.sSJHM = $("#TB_SJHM").val();
    obj3.dCSRQ = $("#TB_CSRQ").val();
    if ($("#TB_PHONEHEAD").val() != "")
        obj3.sPHONE = $("#TB_PHONEHEAD").val() + "-" + $("#TB_PHONE").val();
    else
        obj3.sPHONE = $("#TB_PHONE").val();
    obj3.sQQ = $("#TB_QQ").val();
    obj3.sWX = $("#TB_WX").val();
    obj3.sWB = $("#TB_WB").val();
    obj3.sYZBM = $("#TB_YZBM").val();
    //obj3.sBGDH = $("#TB_BGDH").val();
    obj3.sEMAIL = $("#TB_EMAIL").val();
    obj3.iQYID = ($("#HF_QYID").val() != "") ? $("#HF_QYID").val() : "-1";
    //obj3.sTXDZ1 = $(".selectList").find(".province").val();
    //obj3.sTXDZ2 = $(".selectList").find(".city").val();
    //obj3.sTXDZ3 = $(".selectList").find(".district").val();
    //obj3.sTXDZ4 = $(".selectList").find(".door").val();
    // obj3.sTXDZ = $("#TB_TXDZ").val();
    obj3.sTXDZ = $("#TB_QY").val() + $("#TB_ROAD").val();
    if ($("#HF_XQID").val() != "0") {
        obj3.sTXDZ += $("#TB_PPXQ").val().substr(0, $("#TB_PPXQ").val().length - 1);
    }
    obj3.sTXDZ += $("#TB_MPH").val();
    obj3.sGZDW = $("#TB_GZDW").val();
    obj3.sROAD = $("#TB_ROAD").val();
    obj3.sHOUSENUM = $("#TB_MPH").val();
    //-------------------------------------------------------------
    obj3.iXLID = ($("#DDL_XL").val() == "" || $("#DDL_XL").val() == null) ? "-1" : $("#DDL_XL").val();
    // obj3.iZYID = ($("#HF_ZYID").val() != "") ? $("#HF_ZYID").val() : "-1";  
    //obj3.iZYID = ($("#DDL_ZY")[0].value != "") ? $("#DDL_ZY").val() : "-1";
    obj3.iZYID = (GetSelectValue("DDL_ZY") == null || GetSelectValue("DDL_ZY") == undefined || GetSelectValue("DDL_ZY") == "") ? -1 : GetSelectValue("DDL_ZY");
    obj3.iJTSRID = ($("#DDL_JTSR").val() == "" || $("#DDL_JTSR").val() == null) ? "-1" : $("#DDL_JTSR").val();
    obj3.iJTCYID = ($("#DDL_JTCY").val() == "" || $("#DDL_JTCY").val() == null) ? "-1" : $("#DDL_JTCY").val();
    obj3.iMZID = "-1";//($("#DDL_MZ").val() == "" || $("#DDL_MZ").val() == null) ? "-1" : $("#DDL_MZ").val();
    obj3.iZSCSJID = "-1";//($("#DDL_ZSCSJ").val() == "" || $("#DDL_ZSCSJ").val() == null) ? "-1" : $("#DDL_ZSCSJ").val();
    obj3.iJTGJID = ($("#DDL_JTGJ").val() == "" || $("#DDL_JTGJ").val() == null) ? "-1" : $("#DDL_JTGJ").val();
    obj3.iQCPPID = ($("#S_QCPP")[0].value != "") ? $("#S_QCPP")[0].value : "-1";
    obj3.sCPH = $("#TB_CPH").val();
    //obj3.iGKID = ($("#HF_GKID").val() != "") ? $("#HF_GKID").val() : "-1";//发放必定生成新的顾客档案
    obj3.iJHBJ = ($("#TB_HYZK")[0].value != "") ? $("#TB_HYZK")[0].value : "-1";//改为select
    obj3.dJHJNR = $("#TB_JHJNR").val();
    obj3.sBZ = $("#TA_BZ").val();
    //-------------------------------------------------------------
    obj3.sXXFS = Get_CYCBL_CheckItem("CBL_XXFS");
    obj3.sCXXX = Get_CYCBL_CheckItem("CBL_CXXX");
    //obj3.sHYBQ = Get_CYCBL_CheckItem("CBL_HYBQ");
    //if ($("#R_CANCXXX").prop("checked")) { obj3.sCXXX = 39; }
    //if ($("#R_NOCXXX").prop("checked")) { obj3.sCXXX = 40; }
    obj3.sYYAH = Get_CYCBL_CheckItem("CBL_YYAH");
    obj3.iXQID = $("#HF_XQID").val() == "" ? 0 : $("#HF_XQID").val();
    //obj3.iSQID = $("#HF_SQID").val() == "" ? 0 : $("#HF_SQID").val();
    //-------------------------------------------------------------
    obj3.sSW = $("#TB_SW").val();
    obj3.sCM = $("#TB_CM").val();
    obj3.sXZ = $("#TB_XZ").val();
    obj3.sSX = $("#TB_SX").val();
    obj3.iFXDW = $("#HF_FXDWID").val();
    obj3.sIMGURL = $("#HF_IMGURL").val();
    obj3.sXSGSKH = $("#TB_XSGSKH").val();
    if (obj3.iMAINHYID == "")
        obj3.iMAINHYID = 0;
    Obj.HYKXX = obj3;


    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function GetKCKXX(hykno, cdnr, klx, bgdd) {
    var str = GetKCKXXData(hykno, cdnr, klx, bgdd);
    //if (str == "null" || str == "" || str == undefined) {
    //    ShowMessage("没有找到卡号");
    //    $("#TB_CDNR").val("");
    //    $("#TB_CDNR").focus();
    //    $("#TB_HYKNO").val("");
    //    return;
    //}

    var Obj = JSON.parse(str);
    if (Obj.sCZKHM == "") {
        ShowMessage("没有找到卡号");
        $("#TB_CDNR").val("");
        $("#TB_CDNR").focus();
        //$("#TB_HYKNO").val("");
        return;
    }
    if (Obj.iSKJLBH != 0) {
        ShowMessage("该卡在" + Obj.iSKJLBH + "号售卡单中，请重新选择", 3);
        $("#TB_CDNR").focus();
        return;
    }
    if (Obj.iSTATUS != 1) {
        ShowMessage("该卡不是领取状态，无法发放", 3);
        $("#TB_CDNR").focus();
        return;
    }
    //if (Obj.iBJ_FSK == 1) {
    //    $("#MainCardPanel").show();
    //}
    //else {
    //    $("#MainCardPanel").hide();
    //}

    if (Obj.iBJ_XSK == 1 || Obj.iBJ_SYK == 1) {
        $("#studentRow").show();
    }
    else {
        $("#studentRow").hide();
    }
    //PageDate_Clear();
    //if (CheckBGDDQX(Obj.sBGDDDM, iDJR) != "True") {
    //    art.dialog({ lock: true, content: "该操作员没有该保管地点的操作权限！", });
    //    return;
    //}
    pBJ_FSK = Obj.iBJ_FSK;
    pBJ_TJM = Obj.iBJ_TJM;
    pBJ_XSK = Obj.iBJ_XSK;
    pBJ_SYK = Obj.iBJ_SYK;
    $("#TB_JLBH").val("0");
    $("#DDL_ZJLX").val(1);
    //$("#HF_HYID").val(Obj.iHYID);
    $("#LB_HYKNO").text(Obj.sCZKHM);
    $("#HF_HYKTYPE").text(Obj.iHYKTYPE);
    isPSW(Obj.iHYKTYPE);
    $("#BJ_MMBS").prop("checked", pPSW == "1" ? true : false);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#LB_BGDDMC").text(Obj.sBGDDMC);
    $("#LB_YXQ").text(Obj.dYXQ);
    $("#HF_MDID").val(Obj.iMDID);
    $("#LB_GBF").text(Obj.fKFJE);
    $("#LB_FXDWMC").text(Obj.sFXDWMC);
    $("#HF_FXDWID").val(Obj.iFXDWID);
    $("#B_Save").attr("disabled", false);
    $("#B_Cancel").attr("disabled", false);
    $("#zMP1_Hidden").find("input,select").attr("disabled", false);
    $("#zMP2_Hidden").find("input,select").attr("disabled", false);
    $("#zMP3_Hidden").find("input,select").attr("disabled", false);
    $("#zMP4_Hidden").find("input,select").attr("disabled", false);
    if ($("#HF_HYID").val() != 0 && $("HF_HYID").val() != "") {
        if (parseInt($("#HF_FXDW").val()) == 2) {
            PageControl(true, false);
            //$("#btnYZM").hide();
            //$("#TB_YZM").hide();
        }
    }
    else {
        PageControl(true, false);
        $("#btnYZM").show();
        $("#TB_YZM").show();
        var vSFZ = $("#DDL_ZJLX").val() == 1;
        $("#TB_CSRQ").prop("disabled", vSFZ);
        $("#TB_XZ").prop("disabled", vSFZ);
        $("#TB_SX").prop("disabled", vSFZ);
        $("#Radio1").prop("disabled", vSFZ);
        $("#Radio2").prop("disabled", vSFZ);
    }
    if (Obj.iHYKTYPE != 0) {
        //$("#CBL_HYBQ").html("");
        //CYCBL_ADD_ITEM("CBL_HYBQ", GetKLXBQ(Obj.iHYKTYPE));
        $("#DDL_ZJLX").empty();
        FillSelect("DDL_ZJLX", GetHYXXXM(0, Obj.iHYKTYPE));
    }
    //$("#Radio1").click();//GOOD!默认值已实现，可以使用radio控件
    //$("#Radio3").click();
    //$("#DDL_ZJLX").val("1");
    //$("#TB_SFZBH").focus();
    //$("#DDL_ZJLX").prop("disabled", true);
}



function Get_CYCBL_CheckItem(cbl_name) {
    var valuelist = "";
    $("input[name^='" + cbl_name + "']").each(function () {
        if (this.checked) {
            //valuelist += $(this).parent("span").attr("alt") + ";";
            valuelist += $(this).val() + ";";
        }
    });
    if (valuelist.length > 0) {
        valuelist = valuelist.substring(0, valuelist.length - 1);
    }
    return valuelist;
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD":
            $("#HF_BGDDDM").val(treeNode.sBGDDDM);
            $("#HF_MDID").val(treeNode.iMDID);
            GetKCKXX_KLXBGDD();
            break;
        case "TreeFXDW": $("#HF_FXDWID").val(treeNode.iFXDWID); break;
        case "TreeHYKTYPE":
            $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
            GetKCKXX_KLXBGDD();
            break;
        case "TreeQY":
            var tp_qyid = treeNode.iQYID;
            $("#HF_QYID").val(treeNode.iQYID);
            var str = "";
            var treeObj = $.fn.zTree.getZTreeObj(treeId);
            while (treeNode != null && treeNode.pId != "") {
                str = treeNode.sQYMC + " " + str;
                treeNode = treeObj.getNodeByParam("id", treeNode.pId);
            }
            $("#TB_QY").val(str);
            hideMenu("menuContent");
            vCommunityArray = new Array();
            vCommunityArray = JSON.parse(GetHYQYXQXX(tp_qyid));
            if (vCommunityArray.length > 0) {
                for (var i = 0; i < vCommunityArray.length; i++) {
                    var newData = new Object();
                    newData.label = vCommunityArray[i].sXQMC;
                    newData.value = vCommunityArray[i].iXQID;
                    vCommunityArray[i] = newData;
                }
            }
            $("#TB_PPXQ").autocomplete({
                source: vCommunityArray,
                select: function (event, ui) {
                    $(this).val(ui.item.label);
                    $("#HF_XQID").val(ui.item.value);
                    event.preventDefault();
                }
            });
            break;
    }
}

function GetKCKXX_KLXBGDD() {
    if ($("#HF_BGDDDM").val() == "" || $("#HF_HYKTYPE").val() == "")
        return;
    GetKCKXX("", "", $("#HF_HYKTYPE").val(), $("#HF_BGDDDM").val());
}
function b_khjl() {

    art.dialog.open('../../WUC/HYKRYXX/WUC_HYKRYXX.aspx', { lock: true, title: '人员选择', width: 440, height: 420 }, false);
    //art.dialog.open('../../SrchHYXX/SrchHYXX.aspx', { lock: true, title: '入会35渠道选择', width: 1440, height: 1420 }, false);

}
function b_khjl_loaddata(pId, pName) {
    $("#LB_KHJLID").text(pId);
    $("#LB_KHJLMC").text(pName);
}
function b_rhqd() {

    art.dialog.open('../../WUC/HYKRHQD/WUC_HYKRHQD.aspx', { lock: true, title: '入会渠道选择', width: 440, height: 420 }, false);

    //art.dialog.open('../../WUC/WebForm1.aspx', { lock: true, title: '入会渠道选择', width: 440, height: 420 }, false);

}
function b_rhqd_loaddata(pId, pName) {
    $("#LB_QDBH").text(pId);
    $("#LB_QDMC").text(pName);
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
            //$("#DDL_SEX").val("0");
            document.getElementById("Radio1").disabled = false;
            $("#Radio1").click();
            //document.getElementById("Radio1").checked = true;
            //$("#Radio1")[0].checked = true;
        }
        else {  //sex = '女';
            //$("#DDL_SEX").val("1");
            document.getElementById("Radio2").disabled = false;
            $("#Radio2").click();
            //document.getElementById("Radio2").checked = true;
            //$("#Radio2")[0].checked = true;
        }
    }
    if (18 == val.length) { //18位身份证号码
        birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11) + '-' + val.charAt(12) + val.charAt(13);

        if (parseInt(val.charAt(16) / 2) * 2 != val.charAt(16))//看倒数第二位，奇数男偶数女
        {
            //man
            document.getElementById("Radio1").disabled = false;
            $("#Radio1").click();
            $("#Radio1").prop("checked", true);
            //document.getElementById("Radio1").checked = true;
            //$("#Radio1")[0].checked = true;
        }
        else {  //sex = '女';
            document.getElementById("Radio2").disabled = false;

            $("#Radio2").click();
            $("#Radio2").prop("checked", true);
            //document.getElementById("Radio2").checked = true;
            //$("#Radio2")[0].checked = true;
        }
    }
    document.getElementById("Radio1").disabled = true;
    document.getElementById("Radio2").disabled = true;
    $("#TB_CSRQ").val(birthdayValue);
    document.getElementById("TB_CSRQ").disabled = true;

    getSRXXMORE();
    //$("#Radio3").click();
}

//身份证最后一位校验的计算方式：1. 对前17位数字本体码加权求和公式为：S = Sum(Ai * Wi), i = 0, ... , 16其中Ai表示第i位置上的身份证号码数字值，Wi表示第i位置上的加权因子，其各位对应的值依次为： 7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2
//                              2. 以11对计算结果取模Y = mod(S, 11)
//                              3. 根据模的值得到对应的校验码对应关系为：Y值： 0 1 2 3 4 5 6 7 8 9 10校验码： 1 0 X 9 8 7 6 5 4 3 2

function getGKDA(sfzbh, sjhm) {
    var val = $("#TB_SFZBH").val();
    birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9);
    var currentYear = new Date().getFullYear();
    if ((parseInt(currentYear) - parseInt(birthdayValue) > 22) && parseInt(pBJ_XSK) == 1) {
        ShowMessage("年龄超过限制，无法办理学生卡", 3);
        BK = false;
        return;
    }
    var str = GetGKDAData(0, sfzbh, sjhm);
    if (str == "null" || str == "") {
        $("#LB_GKXX").text("新顾客");
        if (sfzbh != "" && sfzbh.charAt(sfzbh.length - 1) == "x") {
            sfzbh = sfzbh.toUpperCase();
            $("#TB_SFZBH").val(sfzbh);
            getGKDA($("#TB_SFZBH").val(), "");
        }
        BK = true;
        return;
    }
    $("#LB_GKXX").text("老顾客");
    var Obj = JSON.parse(str);
    var t = CheckBK(Obj.iGKID);
    var b = JSON.parse(t);
    if (b.iBJ_CHECK == true) {
        //已经存在此证件号/手机号的信息
        if (b.iSTATUS == -3) {
            ShowMessage("此证件号会员卡已挂失，无法继续办卡！", 4);
            $("#TB_SFZBH").val("");
            BK = false;
            return;
        }
        if (b.iSTATUS == -5) {
            ShowMessage("此证件号会员卡标记为黑名单，无法继续办卡！", 4);
            BK = false;
            return;
        }
        if (b.iSTATUS >= 0) {
            if ($("#TB_SFZBH").val() != "" && $("#TB_SJHM").val() == "") {
                BK = false;
                $("#TB_SFZBH").val("");
                ShowMessage("此证件号已办过会员卡！", 4);
                return;
            }

            else if ($("#TB_SFZBH").val() == "" && $("#TB_SJHM").val() != "") {
                BK = false;
                $("#TB_SJHM").val("");
                ShowMessage("此手机号已办过会员卡！", 4);
                return;
            }
            else {
                ShowMessage("此手机号/证件号已办过会员卡！", 4);
                $("#TB_SJHM").val("");
                $("#TB_SFZBH").val("");
            }
            //$("#TB_HYKNO").attr("disabled", "disabled");
            BK = false;
            return;
        }
        if (b.fYE > 0 && b.iSTATUS < 0) {
            if ($("#TB_SFZBH").val() != "" && $("#TB_SJHM").val() == "") {
                ShowMessage("此证件号会员卡金额不为0！", 4);
            }
            else {
                ShowMessage("此证件号会员卡金额不为0！", 4);
                $("#TB_SJHM").val("");
            }
            //$("#TB_HYKNO").attr("disabled", "disabled");
            BK = false;
            return;
        }
        else {
            BK = true;
        }
    }
    else
        BK = true;
    //if (Obj.iHYID != 0 && Obj.iFXDW != 0) {
    //    ShowMessage("已经存在一张主卡,只能发子卡！", 4);
    //    $("#HF_HYID").val(Obj.iHYID);
    //    $("#HF_FXDW").val(Obj.iFXDW);
    //    PageControl(true, false);
    //}
    //else {
    //    if (Obj.iHYKTYPE == 201) {
    //        ShowMessage("存在一张工会卡,可以发主卡！", 4);
    //        PageControl(true, false);
    //    }
    //}


    $("#HF_GKID").val(Obj.iGKID);
    $("#TB_HYNAME").val(Obj.sGK_NAME);
    $("#DDL_ZJLX").val(Obj.iZJLXID);
    $("#TB_SFZBH").val(Obj.sSFZBH);
    $("[name='sex'][value='" + Obj.iSEX + "']").attr("checked", true);
    $("#TB_CSRQ").val(Obj.dCSRQ);
    $("[name='cld'][value='" + Obj.iBJ_CLD + "']").attr("checked", true);
    $("#HF_QYID").val(Obj.iQYID);
    //// FillQYTree("TreeQY", "TB_QY");
    var treeStr = "";
    var treeObj = $.fn.zTree.getZTreeObj("TreeQY");
    //// var node = treeObj.getNodeByTId(Obj.iQYID.toString());
    //// var node = treeObj.getNodeByTId("0101");
    var treeNode = treeObj.getNodeByParam("jlbh", Obj.iQYID);
    while (treeNode != null && treeNode.pId != "") {
        treeStr = treeNode.name + " " + treeStr;
        treeNode = treeObj.getNodeByParam("id", treeNode.pId);
    }
    $("#TB_QY").val(treeStr);
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
    $("#TB_GKDA").val(Obj.sTJRYMC);
    $("#HF_GKDA").val(Obj.iTJRYID);
    $("#TB_QQ").val(Obj.sQQ);
    $("#TB_WX").val(Obj.sWX);
    $("#TB_WB").val(Obj.sWB);
    $("#TB_PPXQ").val(Obj.sPPXQ);
    $("#HF_XQID").val(Obj.iQYID);
    //2015-12-22
    $("#HF_XQID").val(Obj.iXQID);
    $("#TB_PPXQ").val(Obj.sXQMC);
    //$("#HF_SQID").val(Obj.iSQID);
    //$("#TB_SQMC").val(Obj.sSQMC);

    $("#TB_EMAIL").val(Obj.sEMAIL);
    $("#TB_YZBM").val(Obj.sYZBM);
    // $("#TB_TXDZ").val(Obj.sTXDZ);
    $("#TB_JHJNR").val(Obj.dJHJNR);
    $("#TB_GZDW").val(Obj.sGZDW);
    $("#TB_ROAD").val(Obj.sROAD);
    $("#TB_MPH").val(Obj.sHOUSENUM);

    $("#TB_SW").val(Obj.sSW);
    $("#TB_CM").val(Obj.sCM);
    $("#TB_XZ").val(Obj.sXZ);
    $("#TB_SX").val(Obj.sSX);

    //$("#DDL_MZ").val(Obj.iMZID);
    $("#DDL_ZY").val(Obj.iZYID);
    $("#DDL_XL").val(Obj.iXLID);
    $("#DDL_JTSR").val(Obj.iJTSRID);
    $("#DDL_JTCY").val(Obj.iJTCYID);
    $("#S_QCPP").val(Obj.iQCPPID);
    $("#TB_HYZK").val(Obj.iJHBJ);
    //$("#DDL_ZSCSJ").val(Obj.iZSCSJID);
    $("#DDL_JTGJ").val(Obj.iJTGJID);
    $("#TB_CPH").val(Obj.sCPH);
    $("#TA_BZ").val(Obj.sBZ);
    if (Obj.sSFZBH == "") {
        $("#CBL_XXFS").html("");
        CYCBL_ADD_ITEM_True("CBL_XXFS", GetHYXXXM(10));//华地 默认勾选 2014.11.7
    } else {
        //CYCBL_ADD_ITEM_False("CBL_XXFS", HYKBaseYW_XXFS);
        $("#CBL_XXFS").html("");
        CYCBL_ADD_ITEM("CBL_XXFS", GetHYXXXM(10));
        Set_CYCBL_Item("CBL_XXFS", Obj.sXXFS);
    }
    $("#CBL_YYAH").html("");
    CYCBL_ADD_ITEM("CBL_YYAH", GetHYXXXM(7));
    Set_CYCBL_Item("CBL_YYAH", Obj.sYYAH);
    //$("#CBL_CXXX").html("");
    CYCBL_ADD_ITEM("CBL_CXXX", GetHYXXXM(9));
    Set_CYCBL_Item("CBL_CXXX", Obj.sCXXX);
    //$("#R_CANCXXX").prop("checked", Obj.sCXXX == "39" ? true : false);
    //$("#R_NOCXXX").prop("checked", Obj.sCXXX == "40" ? true : false);
}

//-------------------------------------推荐人-------------------------
$(document).ready(function () {
    $("#TB_GKDA").click(function () {
        SelectGKXX("TB_GKDA", "HF_GKDA", "zHF_GKDA", true)
    });
});
function WUC_GKDA_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#TB_GKDA").val(tp_mc);
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
            $("#TB_GKDA").val(tp_mc);
            $("#HF_GKDA").val(tp_hf);
            $("#zHF_GKDA").val(jsonString);
        }
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
function getSRXXMORE() {
    if ($("#TB_CSRQ").val() != "") {
        var str = GetSRXX($("#TB_CSRQ").val());
        var Obj = JSON.parse(str);
        $("#TB_SX").val(Obj.sSX);
        $("#TB_XZ").val(Obj.sXZ);

    }
}
function Set_CYCBL_Item(cbl_name, CBLHYBJ) {
    if (CBLHYBJ != null) {

        var splitArray = CBLHYBJ.split(";");
        var s1 = $("input[name^='" + cbl_name + "']").length;
        for (var i = 0; i <= s1 - 1; i++) {
            //$("#" + cbl_name + "_" + i).attr("checked", false);
            for (var j = 0; j <= splitArray.length - 1; j++) {
                if ($("#" + cbl_name + "_" + i).val() == splitArray[j]) {
                    $("#" + cbl_name + "_" + i).attr("checked", "true");
                }
            }
        }
        //$("input[name^='" + cbl_name + "']").each(function () {

        //    for (var m = 0; m < splitArray.length - 1; m++) {
        //        if ($(this).val() == splitArray[m]) {
        //            //  this.checked;
        //            $(this).attr("checked", "true");
        //        }
        //        else {
        //          //  $(this).attr("checked", false);
        //        }
        //    }
        //})
    }
}

function isPSW(hyktype) {
    var data = CheckPSW(hyktype);
    if (data) {
        data = JSON.parse(data);
        pPSW = data.iBJ_PSW;
    }
}

function onClickCell(index, field) {
    if (endEditing() && $("#LB_HYKNO").text() != "") {
        $('#list').datagrid('selectRow', index)
                .datagrid('editCell', { index: index, field: field });
        editIndex = index;

        var ed = $(this).datagrid('getEditor', { index: index, field: field });
        if (ed) {
            $(ed.target).bind("keypress", function (event) {
                if (event.keyCode == 13) {
                    if ($('#list').datagrid('validateRow', editIndex)) {
                        $('#list').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return true;
                    }
                }
            })
        }
    }
}


var PortOpened = 0;
var CpuCardFound = 0;
function Idcard() {
    var result;
    var photoname;
    var cardname;
    var cardsn;
    var cardtype;
    try {
        var ax = new ActiveXObject("IDRCONTROL.IdrControlCtrl.1");
        //    alert("已安装");  
    } catch (e) {
        alert("控件未安装");
    }


    //注意：第一个参数为对应的设备端口，USB型为1001，串口型为1至16
    result = IdrControl1.ReadCard("1001", "c:\\ocx\\test.jpg");
    //result=IdrControl1.ReadCard("1001","");

    cardtype = IdrControl1.DecideReadCardType();//判断卡类型 1-身份证 2-外国居留证

    if (result == 1) {
        if (cardtype == 1) {

            $("#TB_HYNAME").val(IdrControl1.GetName());
            $("#TB_CSRQ").val(IdrControl1.GetBirthYear() + "-" + IdrControl1.GetBirthMonth() + "-" + IdrControl1.GetBirthDay());
            $("#TB_SFZBH").val(IdrControl1.GetCode());
            var a = IdrControl1.GetSex();
            if (IdrControl1.GetSex() == "女") {
                document.getElementById("Radio1").disabled = false;
                $("#Radio2")[0].checked = true;
                $("#Radio2").click();
            }
            else {
                document.getElementById("Radio2").disabled = false;
                $("#Radio1").click();
                $("#Radio1")[0].checked = true;

            }
            getGKDA(IdrControl1.GetCode());

            //document.all.oTable.rows(2).cells(1).innerText = IdrControl1.GetName();
            //document.all.oTable.rows(3).cells(1).innerText = IdrControl1.GetFolk();
            //document.all.oTable.rows(4).cells(1).innerText = IdrControl1.GetSex();
            //document.all.oTable.rows(5).cells(1).innerText = IdrControl1.GetBirthYear() + "年" + IdrControl1.GetBirthMonth() + "月" + IdrControl1.GetBirthDay() + "日";
            //cardsn = IdrControl1.GetCode();
            //document.all.oTable.rows(6).cells(1).innerText = cardsn;

            //document.all.oTable.rows(7).cells(1).innerText = IdrControl1.GetAddress();
            //document.all.oTable.rows(8).cells(1).innerText = IdrControl1.GetAgency();
            //document.all.oTable.rows(9).cells(1).innerText = IdrControl1.GetValid();
            //document.all.oTable.rows(10).cells(1).innerText = IdrControl1.GetSAMID();
            //document.all.oTable.rows(11).cells(1).innerText = IdrControl1.GetIDCardSN();
            //document.all['PhotoDisplay1'].src = 'data:image/jpeg;base64,' + IdrControl1.GetJPGPhotobuf();

        }
        else {
            alert("当前卡片不是身份证，请将身份证放到读卡器上！");
        }
    }
    else {
        if (result == -1)
            alert("端口初始化失败！");
        if (result == -2)
            alert("请重新将卡片放到读卡器上！");
        if (result == -3)
            alert("读取数据失败！");
        if (result == -4)
            alert("生成照片文件失败，请检查设定路径和磁盘空间！");
    }

}

function Idfcard() {
    var result;
    var photoname;
    var cardname;
    var cardsn;
    var cardtype;
    try {
        var ax = new ActiveXObject("IDRCONTROL.IdrControlCtrl.1");
        //    alert("已安装");  
    } catch (e) {
        alert("控件未安装");
    }
    //	result=IdrControl1.RepeatRead(1);   //设置是否重复读卡  0-不重复  1-重复
    //  result=IdrControl1.setMute(1);   //设置是否静音读卡  0-不静音  1-静音

    //注意：第一个参数为对应的设备端口，USB型为1001，串口型为1至16
    result = IdrControl1.ReadCard("1001", "c:\\ocx\\test.jpg");
    //result=IdrControl1.ReadCard("1001","");
    cardtype = IdrControl1.DecideReadCardType();

    if (result == 1) {
        if (cardtype == 2) {
            document.all.oTable.rows(15).cells(1).innerText = IdrControl1.GetenName();
            document.all.oTable.rows(16).cells(1).innerText = IdrControl1.GetcnName();
            document.all.oTable.rows(17).cells(1).innerText = IdrControl1.GetNation();
            document.all.oTable.rows(18).cells(1).innerText = IdrControl1.GetSex();
            document.all.oTable.rows(19).cells(1).innerText = IdrControl1.GetBirthYear() + "年" + IdrControl1.GetBirthMonth() + "月" + IdrControl1.GetBirthDay() + "日";
            cardsn = IdrControl1.GetCode();
            document.all.oTable.rows(20).cells(1).innerText = cardsn;

            document.all.oTable.rows(21).cells(1).innerText = IdrControl1.GetValid();
            //		document.all.oTable.rows(10).cells(1).innerText=IdrControl1.GetCardPhotobuf();
            document.all.oTable.rows(22).cells(1).innerText = IdrControl1.GetSAMID();
            document.all.oTable.rows(23).cells(1).innerText = IdrControl1.GetIDCardSN();
            document.all['PhotoDisplay2'].src = 'data:image/jpeg;base64,' + IdrControl1.GetJPGPhotobuf();
            //document.all.oTable.rows(12).cells(1).innerText=IdrControl1.GetIDCardSN();
            //photoname="d:\\test\\"+"Z"+cardsn+".jpg";
            //cardname="d:\\test\\"+"Z"+cardsn+"card.jpg";

            //alert(photoname);
            //result=IdrControl1.ExportPhoto(photoname,cardname);
            //if (result!=1){
            //			alert(result);
            //	}

            //document.all.notbook.src="pic.htm";

            //alert(IdrControl1.GetSexN());
            //alert(IdrControl1.GetFolkN());
        }
        else {
            alert("当前卡片不是外国人永久居留证，请将居留证放到读卡器上！");
        }

    } else {
        if (result == -1)
            alert("端口初始化失败！");
        if (result == -2)
            alert("请重新将卡片放到读卡器上！");
        if (result == -3)
            alert("读取数据失败！");
        if (result == -4)
            alert("生成照片文件失败，请检查设定路径和磁盘空间！");

    }
}
function ICcard() {
    var result;

    //注意：参数为对应的设备端口，iDR210为8159，iDR200 USB型为1001，iDR200串口型为1至16
    result = IdrControl1.ReadICCard("8159");
    if (result == 1) {
        document.all.oTable.rows(2).cells(1).innerText = IdrControl1.GetICCardSN();
        result = IdrControl1.FindICCard();  //读写扇区前， 需调用该接口找卡
        if (result == 1 || result == 3 || result == 4)  //1：M1-S50卡 2：CPU卡 3：M1-S70卡 4：Mifare UltraLight卡片
        {
            //读IC卡
            result = IdrControl1.ReadTypeABlock(0, 6, 1, "ffffffffffff"); //如果为Mifare UltraLight卡片,则只需传第二个参数BID(块区),其它参数可设为0或空值
            alert(result); //失败返回“0”， 成功则返回数据
            //写IC卡
            //如果为Mifare UltraLight卡片,则只需传第二个参数BID(块区),和第五个参数data， 其它参数可设为0或空值
            //result=IdrControl1.WriteTypeABlock(0,6,0," ", "55667788");
            /*if(result==1)
			    alert("写IC卡成功");*/
        }
    }
    else {
        if (result == -1)
            alert("端口初始化失败！");
        if (result == -2)
            alert("读IC卡失败");
    }
}

function Picflesh() {
    document.all.notbook.src = "pic.htm";
}

function FindCPUCard() {
    document.all.oTable.rows(14).cells(1).innerText = "";
    PortOpened = IdrControl1.InitComm(1001);
    CpuCardFound = 0;
    if (PortOpened != 1) {

        IdrControl1.CloseComm();
        alert("端口初始化失败！");
        PortOpened = 0;
        return;
    }


    CpuCardFound = IdrControl1.FindICCard();
    document.all.oTable.rows(14).cells(1).innerText = "找到卡类型：" + CpuCardFound;

    if (CpuCardFound0 == 0) {
        IdrControl1.CloseComm();
        alert("未找到卡！");
        CpuCardFound = 0;
        return;
    }
    if (CpuCardFound0 > 3) {
        IdrControl1.CloseComm();
        alert("找卡失败！");
        CpuCardFound = 0;
        return;
    }
}

function ReadSDCard() {
    document.all.oTable.rows(25).cells(1).innerText = "";
    document.all.oTable.rows(26).cells(1).innerText = "";
    document.all.oTable.rows(27).cells(1).innerText = "";
    document.all.oTable.rows(28).cells(1).innerText = "";
    document.all.oTable.rows(29).cells(1).innerText = "";

    if (CpuCardFound != 2) {
        alert("请先找CPU卡！");
        CpuCardFound = 0;
        return;
    }

    result = IdrControl1.ReadCitizenCard();

    if (result == 1) {
        document.all.oTable.rows(25).cells(1).innerText = IdrControl1.GetCityCardNO();
        document.all.oTable.rows(26).cells(1).innerText = IdrControl1.GetCityCardName();
        document.all.oTable.rows(27).cells(1).innerText = IdrControl1.GetCityCardIDType();
        document.all.oTable.rows(28).cells(1).innerText = IdrControl1.GetCityCardIDCode();
        document.all.oTable.rows(29).cells(1).innerText = IdrControl1.GetCityCardAppInfo();
    }
    else
        if (result == -1)
            alert("端口初始化失败！");
    if (result == -2)
        alert("找CPU卡失败！");

}

function APDU_CMD() {
    if (CpuCardFound != 2) {
        alert("请先找CPU卡！");
        CpuCardFound = 0;
        return;
    }

    //alert(document.form1.apdustr.value);
    document.all.oTable.rows(22).cells(1).innerText = IdrControl1.Routon_APDU(document.form1.apdustr.value);

}