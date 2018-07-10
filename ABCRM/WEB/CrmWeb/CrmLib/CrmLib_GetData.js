//前边是已经改成调用PostToCrmlib方法的，后边是没有改的
function FillSH(selectName) {
    PostToCrmlib("FillSH", {}, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            //selectName.append("<option value='" + data[i].sSHDM + "' bmjc='" + data[i].sBMJC + "'>" + data[i].sSHMC + "</option>");
            arr.push({ value: data[i].sSHDM, text: data[i].sSHMC, bmjc: data[i].sBMJC });
        }
        selectName.combobox({
            onSelect: selComSH,
        });
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "选择商户");

    }, false);
}

function GetHYKNOData(iHYID, sHYK_NO, sDBConnName) {
    sDBConnName = sDBConnName || "CRMDB";
    return PostToCrmlib("GetWXHYK_NOData", { iHYID: iHYID, sHYK_NO: sHYK_NO, sDBConnName: sDBConnName }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetWXCARDData() {
    return PostToCrmlib("GetWXCARDData", {}, function (data) {
        return JSON.stringify(data);
    }, false);
}
function FillZFFS(selectName) {
    PostToCrmlib("FillZFFS", {}, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            arr.push({ value: data[i].iZFFSID, text: data[i].sZFFSMC, bmjc: data[i].iBJ_MJ });
        }
        selectName.combobox({
            onSelect: selComZFFS,
        });
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "选择支付方式");

    }, false);
}
function FillWT_new(selectName, vTYPE, vBJ_NONE, vWXPID) {
    PostToCrmlib("FillWT_new", { iTYPE: vTYPE, iPUBLICID: vWXPID }, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iID + "'>" + data[i].sASK + "</option>");
        }
    }, false);
}
function selComZFFS(record) {
    ;
}


function selComSH(record) {;
}
function FillMD(selectName, shdm, qx) {
    qx = qx || 1;
    shdm = shdm || "";
    PostToCrmlib("FillMD", { iRYID: iDJR, sSHDM: shdm, iQX: qx }, function (data) {
        //selectName.combobox({
        //    valueField: "iMDID",
        //    textField: "sMDMC",
        //});
        //selectName.combobox("loadData", data);
        //selectName.combobox("setValue", "选择门店");
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iMDID + "'>" + data[i].sMDMC + "</option>");
        }
    });
}
function FillCITY(selectName) {
    PostToCrmlib("FillCITY", {}, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iCITYID + "'>" + data[i].sCSMC + "</option>");
        }
    });
}
function FillYHQ(selectName, mode) {
    mode = mode || 1;
    PostToCrmlib("FillYHQ", { iMODE: mode }, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            //selectName.append("<option value='" + data[i].iYHQID + "' title='" + data[i].iFS_YQMDFW + "'>" + data[i].sYHQMC + "</option>");
            arr.push({ value: data[i].iYHQID, text: data[i].sYHQMC, title: data[i].iFS_YQMDFW });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "");
    }, false);
}

function FillKZ(selectName, bj_czk) {
    PostToCrmlib("FillKZ", { iBJ_CZK: bj_czk }, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iHYKTYPE + "'>" + data[i].sHYKNAME + "</option>");
        }
    }, false);
}

function FillHYKJC(selectName, kzid) {
    $("#" + selectName).empty();
    PostToCrmlib("FillHYKJC", { iKZID: kzid }, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iHYKJCID + "'>" + data[i].sHYKJCNAME + "</option>");
        }
    }, false);
}
function FillJPJC(selectName) {
    PostToCrmlib("FillJPJC", {}, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iJC + "'>" + data[i].sMC + "</option>");
        }
    }, false);
}
function FillFLGZ(selectName, hyktype, hyid) {
    PostToCrmlib("FillFLGZ", { iHYKTYPE: hyktype, iHYID: hyid }, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iFLGZBH + "' >" + data[i].sGZMC + "</option>");
        }
    }, false);
}

function FillBQLB(selectName, iMODE) {
    iMODE = iMODE || 0;
    PostToCrmlib("FillBQLB", { iMODE: iMODE }, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iLABELLBID + "'>" + data[i].sLABELMC + "</option>");
        }
    }, false);
}

function FillBQZ(selectName, bqxmid) {
    PostToCrmlib("FillBQZ", { iLABELXMID: bqxmid }, function (data) {
        for (i = 0; i < data.length; i++) {
            selectName.append("<option value='" + data[i].iLABEL_VALUEID + "' labelid='" + data[i].iLABELID + "'>" + data[i].sLABEL_VALUE + "</option>");
        }
    }, false);
}
function FillGZ(selectName) {
    //PostToCrmlib("FillGZ", {}, function (data) {
    //    for (i = 0; i < data.length; i++) {
    //        $("#" + selectName).append("<option value='" + data[i].iJLBH + "' type='" + data[i].sTYPE + "'>" + data[i].sNAME + "</option>");
    //    }
    //}, false);

    PostToCrmlib("FillGZ", {}, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            arr.push({ value: data[i].iJLBH, text: data[i].sNAME, type: data[i].sTYPE });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "");
    }, false);
}

function FillRWZT(selectName) {
    PostToCrmlib("FillRWZT", {}, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iJLBH + "'>" + data[i].sRWZT + "</option>");
        }
    }, false);
}

function FillWT(selectName, vTYPE, vBJ_NONE) {
    PostToCrmlib("FillWT", { iTYPE: vTYPE }, function (data) {
        for (i = 0; i < data.length; i++) {
            $("#" + selectName).append("<option value='" + data[i].iID + "'>" + data[i].sASK + "</option>");
        }
    }, false);
}

function FillNBDM(selectName, nbdm, qx) {
    if (qx == "undefined")
        qx = 1;
    if (nbdm == "undefined")
        nbdm = "";
    PostToCrmlib("FillNBDM", { NBDM: nbdm, QX: qx, iRYID: iDJR }, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            arr.push({ value: data[i].sNBDM, text: data[i].sMC });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "");
    }, false);
}

function FillPublicID(selectName) {
    PostToCrmlib("FillPublicID", {}, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            arr.push({ value: data[i].iJLBH, text: data[i].sPUBLICNAME, pif: data[i].sPUBLICIF });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "选择公众号");
    }, false);
}

function GetFLGZMXData(iFLGZBH) {
    return PostToCrmlib("GetFLGZMXData", { iFLGZBH: iFLGZBH }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetHYXXData(iHYID, sHYK_NO, sDBConnName, sCDNR, iHYKTYPE, sSJHM) {
    sDBConnName = sDBConnName || "CRMDB";
    iHYKTYPE = iHYKTYPE || 0;
    return PostToCrmlib("GetHYXX", { iHYID: iHYID, sHYK_NO: sHYK_NO, sDBConnName: sDBConnName, sCDNR: sCDNR, iHYKTYPE: iHYKTYPE, sSJHM: sSJHM }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetGRXXData(iHYID, sHYK_NO, sDBConnName) {
    sDBConnName = sDBConnName || "CRMDB";
    return PostToCrmlib("GetGRXX", { iHYID: iHYID, sHYK_NO: sHYK_NO, sDBConnName: sDBConnName }, function (data) {
        return JSON.stringify(data);
    }, false);
    //sjson = "{'iHYID':" + iHYID + ",'sHYK_NO':'" + sHYK_NO + "','sDBConnName':'" + sDBConnName + "'}";
    //result = "";
    //$.ajax({
    //    type: 'post',
    //    url: "../../CrmLib/CrmLib.ashx?func=GetGRXX",
    //    dataType: "json",
    //    async: false,
    //    data: { json: JSON.stringify(sjson), titles: 'cecece' },
    //    success: function (data) {
    //        result = JSON.stringify(data);
    //    },
    //    error: function (data) {
    //        //result = data.responseText;
    //        result = "";
    //    }
    //});
    //return result;
}
function GetHYKDEF(iHYKTYPE) {
    return PostToCrmlib("GetHYKDEF", { iHYKTYPE: iHYKTYPE }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetJFBL(iID) {
    return PostToCrmlib("GetJFBL", { iID: iID }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetHYBQData(sHYK_NO) {
    return PostToCrmlib("GetBQXX", { sHYK_NO: sHYK_NO }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetKCKXXData(sCZKHM, sCDNR, iHYKTYPE, sBGDDDM, sDBConnName, iSTATUS) {
    sDBConnName = sDBConnName || "CRMDB";
    iHYKTYPE = iHYKTYPE || 0;
    sBGDDDM = sBGDDDM || "";
    iSTATUS = iSTATUS || 1;//0建卡1领用
    return PostToCrmlib("GetKCKXX", { sCZKHM: sCZKHM, sCDNR: sCDNR, sDBConnName: sDBConnName, iSTATUS: iSTATUS, iHYKTYPE: iHYKTYPE, sBGDDDM: sBGDDDM },
        function (data) {
            return JSON.stringify(data);
        }, false);
}

function GetMZKKCKXXData(sCZKHM, sCDNR, sDBConnName, iSTATUS) {
    sDBConnName = sDBConnName || "CRMDB";
    iSTATUS = iSTATUS || -1;
    return PostToCrmlib("GetMZKKCKXX", { sCZKHM: sCZKHM, sCDNR: sCDNR, sDBConnName: sDBConnName, iSTATUS: iSTATUS },
        function (data) {
            return JSON.stringify(data);
        }, false);
}

function GetMZKXXData(iHYID, sCZKHM, sCDNR, sDBConnName) {
    sDBConnName = sDBConnName || "CRMDBMZK";
    return PostToCrmlib("GetMZKXX", { iHYID: iHYID, sCZKHM: sCZKHM, sCDNR: sCDNR, sDBConnName: sDBConnName },
        function (data) {
            return JSON.stringify(data);
        }, false);
}

function GetSJGZ(iHYKTYPE_OLD, iBJ_SJ, fBQJF, fXFJE, iMDID) {
    return PostToCrmlib("GetSJGZ", { iHYKTYPE: iHYKTYPE_OLD, iSJ: iBJ_SJ, fBQJF: fBQJF, fXFJE: fXFJE, iMDID: iMDID },
    function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetHYXXXM(xmlx, hyktype) {
    xmlx = xmlx || 0;
    hyktype = hyktype || 0;
    return PostToCrmlib("GetHYXXXM", { iXMLX: xmlx, iHYKTYPE: hyktype }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function SearchBackCardData(iSKJLBH, sDBConnName) {
    return PostToCrmlib("SearchBackCardData", { iSKJLBH: iSKJLBH, sDBConnName: sDBConnName }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetSRXX(CSRQ) {
    return PostToCrmlib("GetSRXX", { dCSRQ: CSRQ }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetGKDAData(iGKID, sSFZBH, sSJHM, sHYK_NO) {
    sHYK_NO = sHYK_NO || "";
    return PostToCrmlib("GetGKDA", { iGKID: iGKID, sSFZBH: sSFZBH, sSJHM: sSJHM, sHYK_NO: sHYK_NO }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetMZKKCKKD_FS(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE, sBGDDDM, sDBConnName, iSTATUS) {
    iSTATUS = iSTATUS || 1;//0建卡1领用
    return PostToCrmlib("GetMZKKCKKD_FS", { sCZKHM_BEGIN: sCZKHM_BEGIN, sCZKHM_END: sCZKHM_END, sDBConnName: sDBConnName, iSTATUS: iSTATUS, iHYKTYPE: iHYKTYPE, sBGDDDM: sBGDDDM },
        function (data) {
            return JSON.stringify(data);
        }, false);
}


function GetCXBFKHBC(sHYK_NO1, sHYK_NO2) {
    return PostToCrmlib("GetCXBFKHBC", { sHYK_NO1: sHYK_NO1, sHYK_NO2: sHYK_NO2 },
        function (data) {
            return JSON.stringify(data);
        }, false);
}


function GetMZKFP(iFP_FLAG, iSKJLBH, sHYK_NO1, sHYK_NO2, dDJSJ1, dDJSJ2) {

    return PostToCrmlib("GetMZKFP", { iFP_FLAG: iFP_FLAG, iSKJLBH: iSKJLBH, sHYK_NO1: sHYK_NO1, sHYK_NO1: sHYK_NO1, sHYK_NO2: sHYK_NO2, dDJSJ1: dDJSJ1, dDJSJ2: dDJSJ2 },
        function (data) {
            return JSON.stringify(data);
        }, false);
}
function CheckBGDDQX(sBGDDDM, iLoginRYID) {
    return PostToCrmlib("CheckBGDDQX", { sBGDDDM: sBGDDDM, iRYID: iLoginRYID }, function (data) {
        return JSON.stringify(data);
    }, false);
    ////  sjson = "{'sBGDDDM':'" + sBDDDDM + "'}";
    //result = "";
    //$.ajax({
    //    type: 'post',
    //    url: "../../CrmLib/CrmLib.ashx?func=CheckBGDDQX&sBGDDDM=" + sBDDDDM + "&iLoginRYID=" + iLoginRYID + "",
    //    async: false,
    //    success: function (data) {
    //        result = data;
    //    },
    //    error: function (data) {
    //        result = data.responseText;
    //    }
    //});
    //return result;
}
function FillSelect(selectName, data) {
    var option = "";
    if (data) {
        data = JSON.parse(data);
    }
    for (var i in data) {
        option += "<option value=" + data[i].iXMID + ">" + data[i].sNR + "</option>";
    }
    $("#" + selectName).append(option);
}
function GetHYXXXMMC(xmid) {
    xmid = xmid || 0;
    return PostToCrmlib("getHYXXXMMC", { iXMID: xmid }, function (data) {
        return JSON.stringify(data);
    }, false);
}


function GetHYXF(iJLBH, iMDID, sSKTNO, iHYID) {
    return PostToCrmlib("GetHYXF", { iXPH: iJLBH, iMDID: iMDID, sSKTNO: sSKTNO, iHYID: iHYID }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetRCLResult(dPDRQ) {
    return PostToCrmlib("GetRCLResult", { dPDRQ: dPDRQ }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function Getftpconfig() {
    return PostToCrmlib("Getftpconfig", {}, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetSPTJINX() {
    return PostToCrmlib("GetSPTJINX", {}, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetYYFWDEFINX() {
    return PostToCrmlib("GetYYFWDEFINX", {}, function (data) {
        return JSON.stringify(data);
    }, false);
}

function UploadPicture(formID, tbName, imagesFirstName) {
    var str = Getftpconfig();
    var data = JSON.parse(str);
    var saveIp = data.sIP_PUB;
    var sPSWD = data.sPSWD;
    var sURL = data.sIP_NET;
    var sDIR = data.sDIR;
    var t = sDIR.lastIndexOf("@");
    var sUSER = sDIR.substr(0, t);
    $("#" + formID).ajaxSubmit({
        url: '../../GTPT/UpLoaPicture_NEW2.ashx?sPath=' + saveIp + "&sName=" + imagesFirstName + "&sPSWD=" + sPSWD + "&sURL=" + sURL + "&sUSER=" + sUSER,
        dataType: "json",
        async: true,
        success: function (data) {
            if (data.errCode == 0) {
                ShowMessage("上传成功！");
                $("#" + tbName).val(data.result);

            } else {
                ShowMessage(data.errMessage);
            }
        },
        error: function (data) {
            ShowMessage("FTP上传失败:" + data.responsetext);
        }
    });

}


function GetCXHD() {
    return PostToCrmlib("GetCXHD", {}, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetYHQDEF_CXHD(cxid, yhqid) {
    return PostToCrmlib("GetYHQDEF_CXHD", { iCXID: cxid, iYHQID: yhqid }, function (data) {
        return data;
    }, false);
}

function FiLLQZLX(selectName, mdid) {
    mdid = mdid || 0;
    PostToCrmlib("FillQZLX", { iMDID: mdid }, function (data) {
        for (i = 0; i < data.length; i++) {
            //selectName.append("<option value='" + data[i].iMDID + "'>" + data[i].sMDMC + "</option>");
            selectName.append("<option id='qzcyrs_" + data[i].iQZCYRS + "' value='" + data[i].iJLBH + "'>" + data[i].sQZLXMC + "</option>");
        }
    });


}
function FillHD(selectName, status) {
    status = status || 1;
    PostToCrmlib("FillHD", { iSTATUS: status }, function (data) {
        for (i = 0; i < data.length; i++) {
            selectName.append("<option value='" + data[i].iHDID + "'>" + data[i].sHDMC + "</option>");
        }
    });
}
function FillLPFFGZ(selectName, iGZLX, iHYKTYPE) {
    PostToCrmlib("FillLPFFGZ", { iGZLX: iGZLX, iHYKTYPE: iHYKTYPE }, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            //selectName.append("<option value='" + data[i].sSHDM + "' bmjc='" + data[i].sBMJC + "'>" + data[i].sSHMC + "</option>");
            arr.push({ value: data[i].iJLBH, text: data[i].sGZMC, gzlx: data[i].iGZLX, slxz: data[i].iBJ_SL });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "");
    }, false);
}
function GetLPFFGZLP(iJLBH, sBGDDDM) {
    return PostToCrmlib("GetLPFFGZLP", { iJLBH: iJLBH, sBGDDDM: sBGDDDM }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function CheckLPFFResult(conditionName, hykno, ruleNumber) {
    return PostToCrmlib("CheckLPFF", { sGZLX: conditionName, sHYK_NO: hykno, iJLBH: ruleNumber }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetSelfInx(tablename, field) {
    return PostToCrmlib("GetSelfInx", { sTABLENAME: tablename, sFIELD: field }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetPath1() {
    return PostToCrmlib("GetPath1", {}, function (data) {
        return JSON.stringify(data);
    }, false);
}

function FillLMSHLX(selectName) {
    PostToCrmlib("FillLMSHLX", {}, function (data) {
        var arr = [];
        for (i = 0; i < data.length; i++) {
            //selectName.append("<option value='" + data[i].sSHDM + "' bmjc='" + data[i].sBMJC + "'>" + data[i].sSHMC + "</option>");
            arr.push({ value: data[i].iID, text: data[i].sMC });
        }
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", "");
    }, false);
}



function GetWXHYXXData(iHYID, sHYK_NO, sDBConnName) {
    sDBConnName = sDBConnName || "CRMDB";
    return PostToCrmlib("GetWXHYXX2", { iHYID: iHYID, sHYK_NO: sHYK_NO, sDBConnName: sDBConnName }, function (data) {
        return JSON.stringify(data);
    }, false);
}


function GetHYQYXQXX(iQYID, sDBConnName) {
    sDBConnName = sDBConnName || "CRMDB";
    iQYID = iQYID || 0;
    return PostToCrmlib("GetHYQYXQXX", { iQYID: iQYID, sDBConnName: sDBConnName }, function (data) {
        return JSON.stringify(data);
    }, false);
}


function CheckBK(iGKID) {
    return PostToCrmlib("CheckBK", { iGKID: iGKID }, function (data) {
        return JSON.stringify(data);
    }, false);
}


function GetSQDXX(JLBH, iBJ_CZK) {
    iBJ_CZK = iBJ_CZK || 0;
    return PostToCrmlib("GetSQDXX", { iJLBH: JLBH, iBJ_CZK: iBJ_CZK }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetKCKSL(iHYKTYPE, iBJ_CZK) {
    iBJ_CZK = iBJ_CZK || 0;
    return PostToCrmlib("GetKCKSL", { iHYKTYPE: iHYKTYPE, iBJ_CZK: iBJ_CZK }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function CalcKDKSL(iSTATUS, sBGDDDM, sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE, sDBConnName) {
    sDBConnName = sDBConnName || "CRMDB";
    return PostToCrmlib("GetKCKKD2", { iSTATUS: iSTATUS, sBGDDDM: sBGDDDM, sCZKHM_BEGIN: sCZKHM_BEGIN, sCZKHM_END: sCZKHM_END, iHYKTYPE: iHYKTYPE, sDBConnName: sDBConnName }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetJFFQJFJE(pFLGZ, fCLJF) {
    return PostToCrmlib("GetJFFQJFJE", { FLGZ: pFLGZ, fCLJF: fCLJF }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetZQMX(fCZJE) {
    return PostToCrmlib("GetZQMX", { fCZJE: fCZJE }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetAPPSettings(key) {
    return PostToCrmlib("GetAPPSettings", { KEY: key }, function (data) {
        return data;
    }, false);
}
function GetReportServer() {
    return GetAPPSettings("ReportServer");
}
function FillChannel(selectName) {
    var arr = [];
    //arr.push({ value: 0, text: "全部" });
    arr.push({ value: 1, text: "微信" });
    arr.push({ value: 2, text: "APP" });
    arr.push({ value: 3, text: "线下" });
    if (typeof (selectName) == "string") {
        $(selectName).combobox("loadData", arr);
        $(selectName).combobox("setValue", arr[0].text);
    }
    else {
        selectName.combobox("loadData", arr);
        selectName.combobox("setValue", arr[0].text);
    }
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//没改成PostToCrmlib的，以后慢慢改
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function getHYXXXM(container, name, xmlx) {
    sjson = "{'iXMLX':" + xmlx + "}";
    var result = new Array();
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=getHYXXXM",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = data;
            var xmsj = "";
            if (result.length >= 1) {
                for (var i = 0; i < result.length; i++) {
                    if (i % 4 == 0 && i != 0) {
                        xmsj += "<br/>";
                    }
                    xmsj += "<input type='checkbox' name='" + name + "' value='" + result[i].iXMID + "'/>" + result[i].sNR + " ";
                }

            }
            container.html(xmsj);
        },
        error: function (data) {
            result = data;
        }
    });
}


function GetLMSHXM(xmlx) {

    var sjson = "{'iXMLX':" + xmlx + "}";
    var tmp = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=getLMSHXM",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'ys' },
        success: function (data) {
            tmp = data;

        },
        error: function (data) {
            tmp = "";
        }
    });
    return tmp;
}

function GetHKdata(sHYK_NO) {
    sjson = "{'sHYK_NO':'" + sHYK_NO + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHKdata",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            //   tipDialog.close();
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
            //   tipDialog.close();
        },
        //complete: function (data) {
        //    if (data && data != "null") {
        //        result = JSON.parse(data.responseText);
        //        var pHYKHM_NEW = result.sHYKHM_NEW;
        //        art.dialog({ content: "此卡已换卡，新卡号为" + pHYKHM_NEW, lock: true });
        //        return false;
        //    }
        //}
    });
    return result;
}


function GetHYSJXXData(iHYID, sHYK_NO, sDBConnName, sCDNR, iBJ_SJ) {
    if (sDBConnName == undefined) {
        sDBConnName = "CRMDB";
    }
    if (sCDNR == undefined) {
        sCDNR = "";
    }
    sjson = "{'iHYID':" + iHYID + ",'sHYK_NO':'" + sHYK_NO + "','sDBConnName':'" + sDBConnName + "','sCDNR':'" + sCDNR + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHYSJXX&iBJ_SJ=" + iBJ_SJ + "",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            return;

        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function GetFWNR(iFWNRID, HYKTYPE, HYID, MAINHYID) {
    if (MAINHYID == "" || MAINHYID == undefined)
        MAINHYID = 0;
    sjson = "{'iFWNRID':" + iFWNRID + ",'iHYKTYPE':" + HYKTYPE + ",'iHYID':" + HYID + ",'iMAINHYID':" + MAINHYID + "}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetFWNR",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;
}

function GetMDMC(iDJR) {
    sjson = "{'iDJR':" + iDJR + "}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetMDMC",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;
}

//function GetHYBQData(HYKNO) {
//    sjson = "{'sHYK_NO':'" + HYKNO + "'}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=GetBQXX",
//        dataType: "json",
//        async: false,
//        cache: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            result = "";
//            if (data && data != "null") {
//                result = JSON.stringify(data);
//            }
//            return;

//        },
//        error: function (data) {
//            //result = data.responseText;
//            result = "";
//        }
//    });
//    return result;
//}


//圈子类型
//function FiLLQZLX(selectName, iMDID) {
//    var vUrl = "../../CrmLib/CrmLib.ashx?func=FillQZLX";
//    if (iMDID != undefined) {
//        vUrl += "&iMDID=" + iMDID + "";
//    }
//    sjson = "{}";
//    selectName.html("");
//    $.ajax({
//        type: 'post',
//        url: vUrl,
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            selectName.append("<option></option>");
//            for (i = 0; i < data.length; i++) {
//                selectName.append("<option id='qzcyrs_" + data[i].iQZCYRS + "' value='" + data[i].iJLBH + "'>" + data[i].sQZLXMC + "</option>");
//            }
//        },
//        error: function (data) {
//            ;
//        }
//    });
//}


//function GetWXHYXXData(iHYID, sHYK_NO, sDBConnName) {
//    if (sDBConnName == undefined) { sDBConnName = "CRMDB"; }
//    sjson = "{'iHYID':" + iHYID + ",'sHYK_NO':'" + sHYK_NO + "','sDBConnName':'" + sDBConnName + "'}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=GetWXHYXX",
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            result = JSON.stringify(data);
//        },
//        error: function (data) {
//            //result = data.responseText;
//            result = "";
//        }
//    });
//    return result;
//}

function GetWX_NOData(sOPENID, sWX_NO, sDBConnName) {
    if (sDBConnName == undefined) { sDBConnName = "CRMDB"; }
    sjson = "{'sOPENID':'" + sOPENID + "','sWX_NO':'" + sWX_NO + "','sDBConnName':'" + sDBConnName + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetWX_NO",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;
}
function GetHYK_NOData(iHYID, sHYK_NO, sDBConnName) {
    if (sDBConnName == undefined) { sDBConnName = "CRMDB"; }
    sjson = "{'iHYID':'" + iHYID + "','sHYK_NO':'" + sHYK_NO + "','sDBConnName':'" + sDBConnName + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHYK_NO",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;
}
//function GetGRXXData(iHYID, sHYK_NO, sDBConnName) {
//    if (sDBConnName == undefined) { sDBConnName = "CRMDB"; }
//    sjson = "{'iHYID':" + iHYID + ",'sHYK_NO':'" + sHYK_NO + "','sDBConnName':'" + sDBConnName + "'}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=GetGRXX",
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            result = JSON.stringify(data);
//        },
//        error: function (data) {
//            //result = data.responseText;
//            result = "";
//        }
//    });
//    return result;
//}


function GetGLDBYHYXXData(sHYK_NO, sDBConnName) {
    if (sDBConnName == undefined) { sDBConnName = "CRMDB"; }
    sjson = "{'sHYK_NO':" + sHYK_NO + ",'sDBConnName':'" + sDBConnName + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetGLDBYHYXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;
}

function GetDBYXXData(sSJHM, bSeaCustMes, sDBConnName) {
    if (sDBConnName == undefined) { sDBConnName = "CRMDB"; }
    var zURL = "../../CrmLib/CrmLib.ashx?func=GetDBYXX";
    if (bSeaCustMes == 1) {
        zURL += "&bSeaCusMes=1";
    }
    sjson = "{'sSJHM':" + sSJHM + ",'sDBConnName':'" + sDBConnName + "'}";
    result = "";
    $.ajax({
        type: 'post',
        // url: "../../CrmLib/CrmLib.ashx?func=GetDBYXX",
        url: zURL,
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function GetGXSHJF(iHYID, iLoginID, BJ_WCLJF) {//这个暂时注释掉，需要增加MDID参数
    var iBJ_WCLJF = 1;
    if (BJ_WCLJF != "undefined" && BJ_WCLJF != undefined) {
        iBJ_WCLJF = BJ_WCLJF;
    }
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetGXSHJF&iHYID=" + iHYID + "&iLoginID=" + iLoginID + "&iBJ_WCLJF=" + iBJ_WCLJF,
        // dataType: "json",
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (data) {

        }
    });
    return result;
}

function GetMDJF(iHYID, iMDID) {
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetMDJF&iHYID=" + iHYID + "&iMDID=" + iMDID,
        // dataType: "json",
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (data) {

        }
    });
    return result;
}
function GetZK(iHYID) {
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetZK&iHYID=" + iHYID,
        // dataType: "json",
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (data) {

        }
    });
    return result;
}



function GetHYXXDataMore(iHYID, sHYK_NO, sSJHM, sSFZBH) {
    sjson = "{'iHYID':" + iHYID + ",'sHYK_NO':'" + sHYK_NO + "','sSJHM':'" + sSJHM + "','sSFZBH':'" + sSFZBH + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHYXXMore",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}
function GetMZKKCKXXData(sCZKHM, sCDNR, sDBConnName, iSTATUS) {
    sDBConnName = sDBConnName || "CRMDBMZK";
    iSTATUS = iSTATUS || -1;
    return PostToCrmlib("GetMZKKCKXX", { sCZKHM: sCZKHM, sCDNR: sCDNR, sDBConnName: sDBConnName, iSTATUS: iSTATUS },
        function (data) {
            return JSON.stringify(data);
        }, false);
}


//function GetMZKKCKXXData(sCZKHM, sCDNR, sDBConn, iSTATUS) {
//    if (!sDBConn)
//        sDBConn = "CRMDBMZK";
//    iSTATUS = iSTATUS || -1;
//    sjson = "{'sCZKHM':'" + sCZKHM + "','sCDNR':'" + sCDNR + "','sDBConnName':'" + sDBConn + "','iSTATUS':" + iSTATUS + "}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=GetMZKKCKXX",
//        dataType: "json",
//        async: false,
//        cache: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            result = JSON.stringify(data);
//        },
//        error: function (data) {
//            result = "";
//        }
//    });
//    return result;
//}

function GetHYYHQData(iHYID, iYHQID, iCXID, dJSRQ, sMDFWDM) {
    sjson = "{'iHYID':" + iHYID + ",'iYHQID':" + iYHQID + ",'iCXID':" + iCXID + ",'dJSRQ':'" + dJSRQ + "','sMDFWDM':'" + sMDFWDM + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetYHQZH",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}
function FillHYKTYPE(selectName, lx, qx) {
    if (qx == undefined)
        qx = 1;
    if (lx == undefined)
        lx = 1;
    //lx:1会员卡2储值卡3储值账户
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillHYKTYPEList&lx=" + lx + "&RYID=" + iDJR + "&QX=" + qx,
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iHYKTYPE + "'>" + data[i].sHYKNAME + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}




function FillJJR(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillJJR",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iJJRID + "'>" + data[i].sJJRMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
function FillXYDJ(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillXYDJ",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iJLBH + "'>" + data[i].sXYDJMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillHYLX(selectName, xmlx) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillHYLX&XMLX=" + xmlx,
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iJLBH + "'>" + data[i].sNAME + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillFWNR(selectName, hyktype, mainhyid) {
    if (hyktype == "undefined")
        hyktype = 0;
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillFWNR&HYKTYPE=" + hyktype + "&MAINHYID=" + mainhyid,
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iFWNRID + "'>" + data[i].sFWNRMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });



}


function FillMDDM(selectName, shdm, qx) {
    if (qx == "undefined")
        qx = 1;
    if (shdm == "undefined")
        shdm = "";
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillMD&SHDM=" + shdm + "&RYID=" + iDJR + "&QX=" + qx,
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].sMDDM + "'>" + data[i].sMDMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
//function FillNBDM(selectName, nbdm, qx) {
//    if (qx == "undefined")
//        qx = 1;
//    if (nbdm == "undefined")
//        nbdm = "";
//    sjson = "{}";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=FillNBDM&NBDM=" + nbdm + "&RYID=" + iDJR + "&QX=" + qx,
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            for (i = 0; i < data.length; i++) {
//                selectName.append("<option value='" + data[i].sNBDM + "'>" + data[i].sMC + "</option>");
//            }
//        },
//        error: function (data) {
//            ;
//        }
//    });
//}


function FillSMZQ(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillSMZQ",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iLBID + "'>" + data[i].sLBMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillLMSH(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillLMSH",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value=" + data[i].iJLBH + ">" + data[i].sLMSHMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}








function GetLPXXData(sLPDM) {
    sjson = "{'sLPDM':'" + sLPDM + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetLPXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

//function FillLPFFGZ(selectName, iGZLX, iHYKTYPE, iBJ_WCLJF) {
//    sjson = "{'iGZLX':" + iGZLX + ",'iHYKTYPE':'" + iHYKTYPE + "','iBJ_WCLJF':'" + iBJ_WCLJF + "'}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=GetFFGZ",
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            selectName.find("option").remove();
//            for (i = 0; i < data.length; i++) {
//                selectName.append("<option value='" + data[i].iJLBH + "'>" + data[i].sGZMC + "</option>");
//            }
//        },
//        error: function (data) {
//            result = "";
//        }
//    });
//    return result;
//}
function GetLPKCData(iLPID, sLPDM, sBGDDDM) {
    sjson = "{'iLPID':" + iLPID + ",'sLPDM':'" + sLPDM + "','sBGDDDM':'" + sBGDDDM + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetLPKC",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}
//function GetLPFFGZLP(pGZID, pHYID, pCZDD) {
//    sjson = "{'iFLGZBH':" + pGZID + ",'iHYID':'" + pHYID + "','sCZDD':'" + pCZDD + "'}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=GetLPFFGZLP",
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            result = JSON.stringify(data);
//        },
//        error: function (data) {
//            result = "";
//        }
//    });
//    return result;
//}



function GetGKDADataByHYKNO(sHYKNO) {
    sjson = "{'sHYK_NO':'" + sHYKNO + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetGKDA",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function FillCXHD(select, sSHDM) {
    sjson = "{'sSHDM':'" + sSHDM + "',}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillCXHD",
        //dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            data = JSON.parse(data);
            var htmlstring = "<option></option>";
            for (i = 0; i < data.length; i++) {
                htmlstring += "<option value='" + data[i].iCXID + "'>" + data[i].sCXZT + "</option>";
            }
            select.html(htmlstring);
        },
        error: function (data) {
            ;
        }
    });
}




function GetSHSPXXData(sSPDM) {
    sjson = "{'sSPDM':'" + sSPDM + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetSHSPXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'ys' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function GetBFConfig(pJLBH) {
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetBFConfig&JLBH=" + pJLBH,
        dataType: "json",
        async: false,
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function GetBMRS(iHDID) {
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetBMRS&HDID=" + iHDID,
        dataType: "json",
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function GetHDHFXX(iHFJLBH) {
    sjson = "{'iHFJLBH':" + iHFJLBH + "}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHDHFXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function GetSHCJ(pJLBH, iLoginRYID, CJRS, CJBZ) {
    sjson = "{'iJLBH':" + pJLBH + ",'iLoginRYID':'" + iLoginRYID + "','iCJRS':'" + CJRS + "','sCJBZ':'" + CJBZ + "'}";
    var result = false;
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetSHCJ",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function FillHDMC(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillHDMC",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iHDID + "'>" + data[i].sHDMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FiLLHYZ(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillHYZ",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iGRPID + "'>" + data[i].sGRPMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function GetLDPSXX(iHDID, iKFRYID) {
    sjson = "{'iHDID':" + iHDID + ",'iKFRYID':" + iKFRYID + "}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetLDPSXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;

}

function GetRWJL(iJLBH) {
    sjson = "{'iJLBH':" + iJLBH + "}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetRWJL",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;

}


function CheckSHMDXX(sHYKTYPE, KSY, KSR, JSY, JSR, iDJR) {
    //sjson = "{'sHYKTYPE':" + sHYKTYPE + ",'KSY':" + KSY + ",'KSR':" + KSR + ",'JSY':" + JSY + ",'JSR':" + JSR + "}";
    sjson = "{}";
    var tipDialog = art.dialog({ content: "查询中...", lock: true });
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=CheckSRMD&sHYKTYPE=" + sHYKTYPE + "&KSY=" + KSY + "&KSR=" + KSR + "&JSY=" + JSY + "&JSR=" + JSR + "&DJR=" + iDJR,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            tipDialog.close();
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
            tipDialog.close();
        }
    });
    return result;

}

function GetSRMD(iDJR, sHYKTYPE, KSY, KSR, JSY, JSR) {
    var result = false;
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetSRMD&DJR=" + iDJR + "&sHYKTYPE=" + sHYKTYPE + "&KSY=" + KSY + "&KSR=" + KSR + "&JSY=" + JSY + "&JSR=" + JSR,
        dataType: "json",
        async: false,
        //data: { json: JSON.stringify(sjson), },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function CheckHYMDXX(sHYKTYPE, iDJR) {
    sjson = "{}";
    var tipDialog = art.dialog({ content: "查询中...", lock: true });
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=CheckHYMDXX&sHYKTYPE=" + sHYKTYPE + "&DJR=" + iDJR,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            tipDialog.close();
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
            tipDialog.close();
        }
    });
    return result;

}

function GetHYMD(iDJR, sHYKTYPE) {
    var result = false;
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHYMD&DJR=" + iDJR + "&sHYKTYPE=" + sHYKTYPE,
        dataType: "json",
        async: false,
        //data: { json: JSON.stringify(sjson), },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function CheckWXFHYMDXX(sHYKTYPE, iDJR) {
    sjson = "{}";
    var tipDialog = art.dialog({ content: "查询中...", lock: true });
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=CheckWXFHYMDXX&sHYKTYPE=" + sHYKTYPE + "&DJR=" + iDJR,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            tipDialog.close();
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
            tipDialog.close();
        }
    });
    return result;

}

function GetWXFHYMD(iDJR, sHYKTYPE, iTS) {
    var result = false;
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetWXFHYMD&DJR=" + iDJR + "&sHYKTYPE=" + sHYKTYPE + "&TS=" + iTS,
        dataType: "json",
        async: false,
        //data: { json: JSON.stringify(sjson), },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function updateWXFDHWHBZ(iDJR, iHYID, sBZ) {
    sjson = "{'iDJR':'" + iDJR + "','iHYID':'" + iHYID + "','sBZ':'" + sBZ + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=updateWXFDHWHBZ",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {

            result = true;

        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function GetSRSLXX(iDJR) {//获取生日名单总数量和已完成数量
    sjson = "{}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetSRSL&DJR=" + iDJR,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }

            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";

        }
    });
    return result;

}


function getSRLP(iDJR, hyid, lpdm, bgdddm, bz) {//获取生日名单总数量和已完成数量
    sjson = "{}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=SelectSRLP&DJR=" + iDJR + "&HYID=" + hyid + "&LPDM=" + lpdm + "&BGDDDM=" + bgdddm + "&BZ=" + bz,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {

            result = true;

        },
        error: function (data) {
            result = data.responseText;

        }
    });
    return result;

}

function LoadBZXX(HYID) {
    sjson = "{}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=LoadBZXX&HYID=" + HYID,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }

            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;

}

function updateBZ(iDJR, iHYID, iLX, sBZ) {
    sjson = "{'iDJR':'" + iDJR + "','iHYID':'" + iHYID + "','iLX':'" + iLX + "','sBZ':'" + sBZ + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=updateBZ",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {

            result = true;

        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function updateVIPBZ(iDJR, sDJRMC, iHYID, sBZ) {
    sjson = "{'iDJR':'" + iDJR + "','iHYID':'" + iHYID + "','sDJRMC':'" + sDJRMC + "','sBZ':'" + sBZ + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=updateVIPBZ",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {

            result = true;

        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}


function LoadVIPBZXX(HYID) {
    sjson = "{}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=LoadVIPBZXX&HYID=" + HYID,
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }

            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;

}


function updateDHWHBZ(iDJR, iHYID, sBZ) {
    sjson = "{'iDJR':'" + iDJR + "','iHYID':'" + iHYID + "','sBZ':'" + sBZ + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=updateDHWHBZ",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {

            result = true;

        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}
function updateXYDJ(iHYID, iXYDJID) {
    sjson = "{'iHYID':'" + iHYID + "','iXYDJID':'" + iXYDJID + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=updateXYDJ",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function updateXYMD(iHYID) {
    sjson = "{'iHYID':'" + iHYID + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=updateXYMD",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}


function SendSMS(sjhm, msg) {
    PostToCrmlib("SendSMS", { sSJHM: sjhm, sDM: msg }, function (data) {
        if (data == "ok")
            ShowMessage("短信发送成功");
        else
            ShowErrMessage("短信发送失败，错误代码：" + data);
    });
}

function CheckPSW(HYKTYPE) {
    var result = "";
    sjson = "{'iHYKTYPE':" + HYKTYPE + "}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetPSW",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = "";
            if (data && data != "null") {
                result = JSON.stringify(data);
            }
            return;

        },
        error: function (data) {
            //result = data.responseText;
            result = "";
        }
    });
    return result;
}

//function CheckBK(iGKID) {
//    //  sjson = "{'sBGDDDM':'" + sBDDDDM + "'}";
//    result = "";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=CheckBK&iGKID=" + iGKID,
//        // dataType: "json",
//        async: false,
//        //  data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            result = data;
//        },
//        error: function (data) {
//            result = data.responseText;
//        }
//    });
//    return result;
//}

function GetLQJF(pSFZBH) {
    result = "0";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetLQJF&SFZBH=" + pSFZBH,
        async: false,
        success: function (data) {
            result = data;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function GetHYSRLQXX(iHYID) {
    sjson = "{'iHYID':'" + iHYID + "'}";
    result = false;
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHYSRLQXX",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function GetHYSRYDXX(iHYID) {
    sjson = "{'iHYID':'" + iHYID + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetHYSRYDXX",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = true;
        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function GetLPLX() {
    sjson = "{}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetLPLX",
        dataType: "json",
        async: false,
        cache: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = true;

        },
        error: function (data) {
            result = data.responseText;
        }
    });
    return result;
}

function GetSRLPYDXX(iHYID) {
    sjson = "{'iHYID':'" + iHYID + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetSRLPYDXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = JSON.stringify(data);
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}

function FillXFLID(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillXFLID",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iFLID + "'>" + data[i].sXFLMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillAPPMD(selectName, shdm, qx) {
    if (qx == "undefined")
        qx = 1;
    if (shdm == "undefined")
        shdm = "";
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillAPPMD&SHDM=" + shdm + "&RYID=" + iDJR + "&QX=" + qx,
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iMDID + "'>" + data[i].sMDMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillAPPJPJC(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillAPPJPJC",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iJC + "'>" + data[i].sMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillSHMD(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillSHMD",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].sSHDM + "'>" + data[i].sSHMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillQYMD(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillQYMD",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iQYID + "'>" + data[i].sQYMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillYDDPP(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillYDDPP",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iBRANDID + "'>" + data[i].sNAME + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

//function FillLMSHLX(selectName) {
//    sjson = "{}";
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=FillLMSHLX",
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(sjson), titles: 'cecece' },
//        success: function (data) {
//            for (i = 0; i < data.length; i++) {
//                selectName.append("<option value='" + data[i].iID + "'>" + data[i].sMC + "</option>");
//            }
//        },
//        error: function (data) {
//            ;
//        }
//    });
//}

function FillLCMD(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillLCMD",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iMDID + "'>" + data[i].sMDMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillMDCT(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillMDCT",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iMDID + "'>" + data[i].sMDMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}

function FillCTLX(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillCTLX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iCTLXID + "'>" + data[i].sCTLXNAME + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
function FillCT(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillCT",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iCTID + "'>" + data[i].sCTMC + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
function FillCPLX(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillCPLX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iCPLXID + "'>" + data[i].sCPLXNAME + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
function FillCZLX(selectName) {
    sjson = "{}";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=FillCZLX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                selectName.append("<option value='" + data[i].iCZLXID + "'>" + data[i].sCZLXNAME + "</option>");
            }
        },
        error: function (data) {
            ;
        }
    });
}
