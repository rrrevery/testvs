function returnMsg(vname, vInput, vReg) {
    if (vInput.length == 0) { return ""; }
    var strValue = Trim(vInput);
    if (!vReg.test(strValue)) {
        return "" + vname + "（" + vInput + "）不正确，请重新填写!";
    }
    else { return ""; }
}
//isnull----------------------------------------------------------------------
function zIsNull(vname, vInput) {
    var strValue = Trim(vInput);
    if (strValue.length <= 0) {
        return "" + vname + "不能为空，请填写!<br />";
    }
    else { return ""; }
}
//date------------------------------------------------------------------------
function zIsDateYM(vname, vInput) {
    var reg = /^(?=19|2)\d{4}-?(?:0?[1-9]|1[012])$/;
    return returnMsg(vname, vInput, reg);
}
function zIsDate(vname, vInput) {
    var reg = /^(?:(?!0000)[0-9]{4}([-/.])(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-/.])0?2\2(?:29))$/;
    return returnMsg(vname, vInput, reg);
}
function zIsFullDate(vname, vInput) {
    var reg = /^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$/;
    return returnMsg(vname, vInput, reg);
}
//number----------------------------------------------------------------------
function zIsInt(vname, vInput) {
    var reg = /^[0-9]*$/;
    return returnMsg(vname, vInput, reg);
}
function zIsNumber(vname, vInput) {
    var reg = /^[0-9]+\.?[0-9]*$/;
    return returnMsg(vname, vInput, reg);
}
function zIsInteger(vname, vInput) {
    var reg = /^[0-9]*$/;
    return returnMsg(vname, vInput, reg);
}
function zIsJE(vname, vInput) {
    var reg = /^[-+]?[0-9]+?$/;
    return returnMsg(vname, vInput, reg);
}
function zIsJE2(vname, vInput) {
    var reg = /^(-?\d*)\.?\d{1,2}$/;
    return returnMsg(vname, vInput, reg);
}
function zIsJE3(vname, vInput) {
    var reg = /^(-?\d*)\.?\d{1,3}$/;
    return returnMsg(vname, vInput, reg);
}
function zIsJE4(vname, vInput) {
    var reg = /^(-?\d*)\.?\d{1,4}$/;
    return returnMsg(vname, vInput, reg);
}
//other-----------------------------------------------------------------------
function zIsValidvar(vname, vInput) {
    var reg = /[\']+/;
    if (vInput.length == 0) { return ""; }
    var strValue = Trim(vInput);
    if (reg.test(strValue)) {
        return "" + vname + "（" + vInput + "）不正确，请重新填写!";
    }
    else { return ""; }
}
function IsIDCard(vname, vInput) {
    if (vInput.length == 0) { return "证件号码不能为空！"; }
    var strValue = Trim(vInput);
    if (!checkCard(strValue)) {
        return "" + vname + "（" + vInput + "）不合法，请重新填写!";
    }
    else
        return "";
    //if (strValue.length == 15) {
    //    var reg = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{2}[xX\d]$/;
    //    return returnMsg(vname, vInput, reg);
    //}
    //if (strValue.length == 18) {
    //    var reg = /^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}[xX\d]$/;
    //    return returnMsg(vname, vInput, reg);
    //}
    //return "" + vname + "（" + vInput + "）不合法，请重新填写!";
}
function zIsEMail(vname, vInput) {
    var reg = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    return returnMsg(vname, vInput, reg);
}
function zIsTelePhone(vname, vInput) {
    if (vInput.length == 0) { return "手机号码为空"; }
    var strValue = Trim(vInput);
    if (strValue.length == 11) {
        var reg = /^1[3|4|5|7|8][0-9]\d{4,8}$/;
        return returnMsg(vname, vInput, reg);
    }
    else {
        return "" + vname + "（" + vInput + "）不合法，请重新填写!";
    }
}
function zIsCSRQ(vname, vInput) {
    var reg = /^(?:(?!0000)[0-9]{4}([-/.])(?:(?:0?[1-9]|1[0-2])\1(?:0?[1-9]|1[0-9]|2[0-8])|(?:0?[13-9]|1[0-2])\1(?:29|30)|(?:0?[13578]|1[02])\1(?:31))|(?:[0-9]{2}(?:0[48]|[2468][048]|[13579][26])|(?:0[48]|[2468][048]|[13579][26])00)([-/.])0?2\2(?:29))$/;
    var tp_1 = returnMsg(vname, vInput, reg);
    if (tp_1 == "") {
        var date = new Date(vInput);
        var myDate = new Date();
        if (date.getFullYear > myDate.getFullYear) {
            return "请输入正确的出生日期！";
        }
        else { return ""; }
    }
    else {
        return tp_1;
    }
}
//


//金额判断
function IsMoneyCheck(obj) {
    var re = /^[\-\+]?([0-9]\d*|0|[1-9]\d{0,2}(,\d{3})*)(\.\d+)?$/;
    var str = obj.value;
    if (str != "") {
        if (re.test(obj.value) == false) {
            obj.value = str;
            alert("请输入正确的金额！");
            obj.focus();
        }
        else { }
    }
}
//电话号码判断
function IsPhoneCheck(obj) {
    var phoneRegWithArea = /^[0][1-9]{2,3}-[0-9]{5,10}$/;
    var phoneRegNoArea = /^[1-9]{1}[0-9]{5,8}$/;
    var prompt = "您输入的电话号码不正确!"
    if (obj.value.length > 9) {
        if (phoneRegWithArea.test(obj.value)) {
            return true;
        } else {
            alert(prompt);
            obj.focus();
        }
    } else {
        if (phoneRegNoArea.test(obj.value)) {
            return true;
        } else {
            alert(prompt);
            obj.focus();
        }
    }
}

//正整数判断
function IsNumberCheck(obj) {
    var regu = "^[0-9]+$";
    var re = new RegExp(regu);
    if (obj.value.search(re) != -1) {
        return true;
    } else {
        alert("该输入框只能输入数字！");
        obj.focus();
    }
}
//日期判断
function IsDateCheck(startDate, endDate) {
    if (!isDate(startDate)) {
        alert("起始日期不正确!");
        return false;
    } else if (!isDate(endDate)) {
        alert("终止日期不正确!");
        return false;
    } else if (startDate > endDate) {
        alert("起始日期不能大于终止日期!");
        return false;
    }
    return true;
}
//表单不为空
function IsEmptyCheck() {
    if (document.form.name.value.length == 0) {
        alert("请输入您姓名!");
        document.form.name.focus();
        return false;
    }
    return true;
}

//判断是否是数字
function IsNumber(str) {
    str = Trim(str);
    //var reg = /^[0-9]+\.?[0-9]*$/;
    var reg = (/^(([1-9]\d*)|\d)(\.\d{1,2})?$/).test(str.toString());
    return reg;
}
//取出空格
function Trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}

/*判断控件值是否为空*/
function IsEmpty(str) {
    if (str == null) {
        return true;
    }
    else {
        str = Trim(str);
        if (str != "") {
            return false;
        }
        else {
            return true;
        }
    }
}

//是否正小数和0
function IsFloat(str) {
    str = Trim(str);
    var reg = /^\d+(\.\d+)?$/;
    if (str.match(reg) == null) {
        return false;
    }
    else {
        return true;
    }
}

//是否字母或数字的组合
function IsNumOrLetters(str) {
    str = Trim(str);
    var reg = /^[A-Za-z0-9]+$/;
    if (str.match(reg) == null) {
        return false;
    }
    else {
        return true;
    }
}

//字符串长度在制定的区间
function StringLength(str, minLength, maxLength) {
    var num = GetLength(str);
    if ((num >= minLength) && (num <= maxLength)) {
        return true;
    }
    else {
        return false;
    }
}

//字符串真实长度     
function GetLength(strTemp) {
    var i, sum;
    sum = 0;
    for (i = 0; i < strTemp.length; i++) {
        if ((strTemp.charCodeAt(i) >= 0) && (strTemp.charCodeAt(i) <= 255))
            sum = sum + 1;
        else
            sum = sum + 2;
    }
    return sum;
}

String.prototype.barryCount = function () {
    txt = this.replace(/(<.*?>)/ig, '');
    txt = txt.replace(/[\u4E00-\u9FA5]/g, '11');
    var count = txt.length;
    return count;

}

//验证日期
function checkDate(obj) {
    value = Trim(obj.value);
    if (value != "") {
        if (!/^\d{4}-\d{1,2}-\d{1,2}$/.test(value)) {
            alert("请正确的输入日期格式");
            obj.select();
            obj.value = "";
        }
        else {
            var year = parseInt(obj.value.split('-')[0]);
            var month = parseInt(obj.value.split('-')[1]);
            var day = parseInt(obj.value.split('-')[2]);
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0)) {
                if (month == 2 && day > 29) {
                    alert("请正确的输入日期格式");
                    obj.select();
                    obj.value = "";
                    return;
                }
            }
            else if (month == 2 && day > 28) {
                alert("请正确的输入日期格式");
                obj.select();
                obj.value = "";
                return;
            }
            if ((month == 4 || month == 6 || month == 9 || month == 11) && day > 30) {
                alert("请正确的输入日期格式");
                obj.select();
                obj.value = "";
                return;
            }
            if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && day > 31) {
                alert("请正确的输入日期格式");
                obj.select();
                obj.value = "";
                return;
            }
        }
    }
}

function checkMonth(obj) {
    var value = Trim(obj.value);
    if (value != "") {
        if (!/^\d{4}-\d{1,2}$/.test(value)) {
            alert("请正确的输入月份");
            obj.select();
            obj.value = "";
        }
        else {
            var month = parseInt(value.split('-')[1]);
            if (month > 12 || month < 1) {
                alert("请正确输入月")
            }
        }
    }
}

function checkYear(obj) {
    var value = Trim(obj.value);
    if (value != "") {
        if (!/^\d{4}$/.test(value)) {
            alert("请正确的输入年份");
            obj.select();
            obj.value = "";
        }
    }
}
//LBC增加
function PageControl(bEnable) {
    var Element = document.getElementById("MainPanel");
    DisableCtrls(Element, !bEnable);
};

function DisableCtrls(Element, bDisable) {
    //if (Element.id.substr(0, 3) == 'zMP')
    //    return;
    if (Element.id == "menu")
        return;
    if (Element.className.indexOf("datagrid") >= 0)
        return;
    if (Element.className.indexOf("easyui-combobox") >= 0) {
        var s = $("#" + Element.id).combobox("getValue");
        $("#" + Element.id).combobox({
            disabled: bDisable
        });
        $("#" + Element.id).combobox("setValue", s);
        return;
    }
    if (Element.no_control == "true" || Element.attributes["no_control"] != undefined) {
        Element.disabled = false;
        return;
    }
    if (Element.localName == "select" && Element.hasAttribute("no_control") != true) {
        Element.disabled = bDisable;
    }
    if (Element.localName == "div" && Element.id == "MainPanel") {
        Element.disabled = false;
    }
    if (typeof (editor) != "undefined")
        editor.readonly(bDisable);
    if (typeof (editor2) != "undefined")//应该有更好的方式，然而并没有研究出来，所以先收工写一下，目前一个页面顶多两个
        editor2.readonly(bDisable);
    //切换输入状态，Element参数是网页元素，一般是MainPanel的div
    //如果有子元素则对子元素继续调用
    if (Element.childElementCount > 0) {
        for (var i = 0; i < Element.childElementCount; i++) {
            DisableCtrls(Element.children[i], bDisable)
        }

    }
        //Element.disabled = bDisable;
    else if (Element.hasAttribute("no_control") != true) {
        //else
        Element.disabled = bDisable;
    }
};

function ClearInputdata(Element) {
    //清除输入数据，Element参数是网页元素，一般是MainPanel的div
    //如果有子元素则对子元素继续调用
    if (Element.children.length > 0) {//childElementCount在某些浏览器上无效，改用children.length
        if (Element.tagName == "SELECT") {
            if (Element.hasAttribute("no_control") != true)
                Element.value = "";
        }
        for (var i = 0; i < Element.children.length; i++) {
            ClearInputdata(Element.children[i])
        }
    }
    else {
        //只清除text、label、hidden类型
        //if (Element.type == "text" || Element.type == "hidden" || Element.type == "password")
        //    Element.value = "";
        //if (Element.nodeName == "LABEL")
        //    $("#" + Element.id).text("");
        if (Element.tagName == "INPUT" && (Element.type == "text" || Element.type == "hidden"))// checkbox的value被清空|| Element.type == "checkbox"))//注意可能有未预料的问题
            $("#" + Element.id).val("");
        if (Element.tagName == "INPUT" && (Element.type == "checkbox"))
            Element.checked = false;
        if (Element.tagName == "LABEL")
            $("#" + Element.id).text("");
    }
};

function GetUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return "";
}

function GetUrlCaption(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return "";
}

function AddToolButtons(name, id, cls) {
    //动态生成控件  
    cls = cls || "bftoolbtn";
    var facls = "";
    var val;//= document.getElementById("btn-toolbar").innerHTML;
    //var img;
    //var filedir = "../../../Image/BillPic/";
    switch (name) {
        //case "添加": img = "add.png"; break;
        //case "修改": img = "edit.png"; break;
        //case "删除": img = "delete.png"; break;
        //case "保存": img = "save.png"; break;
        //case "取消": img = "cancel.png"; break;
        //case "审核": img = "exec.png"; break;
        //case "查询": img = "search.png"; break;
        //case "添加下级": img = "add2.png"; break;
        //case "添加同级": img = "add3.png"; break;

        //case "添加": cls += " bfbtn_add"; break;
        //case "修改": cls += " bfbtn_edit"; break;
        //case "删除": cls += " bfbtn_delete"; break;
        //case "保存": cls += " bfbtn_save"; break;
        //case "取消": cls += " bfbtn_cancel"; break;
        //case "审核": cls += " bfbtn_exec"; break;
        //case "启动": cls += " bfbtn_start"; break;
        //case "终止": cls += " bfbtn_cancel"; break;
        //case "查询": cls += " bfbtn_search"; break;
        //case "打印": cls += " bfbtn_print"; break;
        //case "导出": cls += " bfbtn_save"; break;
        //case "添加下级": cls += " bfbtn_add"; break;
        //case "添加同级": cls += " bfbtn_add"; break;

        case "添加": facls = "fa fa-plus-circle"; break;
        case "修改": facls = "fa fa-pencil-square-o"; break;
        case "删除": facls = "fa fa-trash-o"; break;
        case "保存": facls = "fa fa-floppy-o"; break;
        case "确定": facls = "fa fa-check-circle"; break;
        case "取消": facls = "fa fa-repeat"; break;
        case "审核": facls = "fa fa-check"; break;
        case "取消审核": facls = "fa fa-times"; break;
        case "启动": facls = "fa fa-play"; break;
        case "终止": facls = "fa fa-stop"; break;
        case "查询": facls = "fa fa-search"; break;
        case "打印": facls = "fa fa-print"; break;
        case "导出": facls = "fa fa-file-o"; break;
        case "导出组": facls = "fa fa-users"; break;
        case "加下级": facls = "fa fa-caret-square-o-down"; break;
        case "加同级": facls = "fa fa-caret-square-o-right"; break;
        case "IC卡": facls = "fa fa-credit-card-alt"; break;
        case "补磁": facls = "fa fa-credit-card-alt"; break;
        case "查卡": facls = "fa fa-search"; break;
        case "刷新": facls = "fa fa-refresh"; break;
        case "刷卡": facls = "fa fa-credit-card"; break;
        case "发布": facls = "fa fa-exchange"; break;
    }
    //val = "<button id='" + id + "' type='button' class='" + cls + " " + facls + "'><span class='bftoolbtn_space'></span>" + name + "</button>";
    //val = "<button id='" + id + "' type='button' class='" + cls + "' ><i class='" + facls + "'></i><span class='btntxt'>" + name + "</span></button>";
    val = "<span id='btnout_" + id + "' class='btnout'><button id='" + id + "' type='button' class='" + cls + " " + facls + "'>" + name + "</button><span class='btnsep'>|</span></span>";
    //val += "<button id='" + id + "' type='button' class='" + cls + "' ><i class='" + facls + "'></i>" + name + "</button>";
    //document.getElementById("btn-toolbar").innerHTML = val;
    $("#btn-toolbar").append(val);
};

function AddButtonSep() {
    return;
    var e = document.getElementById("btn-toolbar");
    var bFirst = true;
    for (var i = 1; i < e.children.length; i++) {
        //var s = e.children[i].innerHTML;
        if (e.children[i].style.display != "none") {
            if (!bFirst)
                e.children[i].outerHTML = "|" + e.children[i].outerHTML;
            bFirst = false;
        }
    }
}

function RefreshButtonSep() {
    for (var i = 0; i < $(".bftoolbtn").length ; i++) {
        if ($(".bftoolbtn")[i].style.display == "none")
            $("#btnout_" + $(".bftoolbtn")[i].id).hide();
        else
            $("#btnout_" + $(".bftoolbtn")[i].id).show();
    }
    //return;
    //var e = document.getElementById("btn-toolbar");
    //var bFirst = true;
    //e.innerHTML = e.innerHTML.replace(new RegExp("\\|", "gm"), "");
    //for (var i = 1; i < e.children.length; i++) {
    //    //var s = e.children[i].innerHTML;
    //    if (e.children[i].style.display != "none") {
    //        if (!bFirst)
    //            e.children[i].outerHTML = "|" + e.children[i].outerHTML;
    //        bFirst = false;
    //    }
    //}
    //BindKey();
}

function WUC_RYXX_LX2_Return(DJR, DJRMC, zDJR) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + DJRMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ",";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + DJRMC).val(tp_mc.substr(0, tp_mc.length - 1));
            $("#" + DJR).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zDJR).val(jsonString);
        }
    }
}

function GetSelectValue(name) {
    if ($("#" + name).prop("disabled")) {
        DisableCtrls(document.getElementById(name), false);
        //$("#" + name).prop("disabled", false);
        var msg = $("#" + name).val();
        DisableCtrls(document.getElementById(name), true);
        //$("#" + name).prop("disabled", true);
        return msg;
    }
    else
        return $("#" + name).val();
}

//一些日期函数
// 增加天
function AddDays(date, value) {
    date.setDate(date.getDate() + value);
}

// 返回两位数的年份
function GetHarfYear(date) {
    var v = date.getYear();
    if (v > 9) return v.toString();
    return "0" + v;
}

// 返回月份（修正为两位数）
function GetFullMonth(date) {
    var v = date.getMonth() + 1;
    if (v > 9) return v.toString();
    return "0" + v;
}

// 返回日 （修正为两位数）
function GetFullDate(date) {
    var v = date.getDate();
    if (v > 9) return v.toString();
    return "0" + v;
}

function Replace(str, from, to) {
    return str.split(from).join(to);
}

function FormatDate(date, str) {
    //str = Replace(str, "yyyy", date.getFullYear());
    //str = Replace(str, "MM", GetFullMonth(date));
    //str = Replace(str, "dd", GetFullDate(date));
    //str = Replace(str, "yy", GetHarfYear(date));
    //str = Replace(str, "M", date.getMonth() + 1);
    //str = Replace(str, "d", date.getDate());
    //return str;

    var o = {
        "M+": date.getMonth() + 1, //月份 
        "d+": date.getDate(), //日 
        "h+": date.getHours() % 12 == 0 ? 12 : date.getHours() % 12, //12小时 
        "H+": date.getHours(), //24小时   
        "m+": date.getMinutes(), //分 
        "s+": date.getSeconds(), //秒 
        "q+": Math.floor((date.getMonth() + 3) / 3), //季度 
        "S": date.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(str)) str = str.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(str)) str = str.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return str;
}
function GetDateString(date, rqlx, datefmt) {
    datefmt = IsNullValue(datefmt, "yyyyMMdd");
    var result = FormatDate(date, datefmt);
    switch (rqlx) {
        case "1":
            result = FormatDate(date, "yyyyMM");
            break;
        case "2":
            result = DateToSeason(date);
            break;
    }
    return result;
}
function DateToSeason(date) {
    var month = date.getMonth();
    var jd = Math.floor(month / 3) + 1;
    return date.getFullYear().toString() + jd;
}
function FillRQLX(selectName, rqlx) {
    //日期类型选择框
    //年季月周日小时000000，0表示不需要这个选项1表示需要
    var arr = [];
    if (rqlx[5] == "1")
        arr.push({ value: 5, text: "小时" });
    if (rqlx[4] == "1")
        arr.push({ value: 4, text: "日" });
    if (rqlx[3] == "1")
        arr.push({ value: 3, text: "周" });
    if (rqlx[2] == "1")
        arr.push({ value: 2, text: "月" });
    if (rqlx[1] == "1")
        arr.push({ value: 1, text: "季" });
    if (rqlx[0] == "1")
        arr.push({ value: 0, text: "年" });    
    $(selectName).combobox("loadData", arr);
    $(selectName).combobox("setValue", arr[0].value);

}
//function SelectWX_XQGZ(GZMC, GZID, zGZID, Single) {  //SelectWXLPFFGZ
//    var data = $("#" + zGZID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WX_WUC/WX_XQGZ/WUC_XQGZ.aspx?", {
//        lock: true, width: 450, height: 470, cancel: false
//        , close: function () {
//            WUC_WX_XQGZ_Return(GZMC, GZID, zGZID);
//        }
//    }, false);
//}
//function WUC_WX_XQGZ_Return(GZMC, GZID, zGZID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + GZMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].Name; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += contractValues[i].Id + ",";

//            }
//            $("#" + GZMC).val(tp_mc);
//            $("#" + GZID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zGZID).val(jsonString);
//        }
//    }
//}
function SelectWX_CXHD(CXZT, CXID, zCXID, Single) {
    var data = $("#" + zCXID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/WX_CXHD/WUC_WXCXHD.aspx", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_WX_CXHD_Return(CXZT, CXID, zCXID);
        }
    }, false);
}

function WUC_WX_CXHD_Return(CXZT, CXID, zCXID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + CXZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            //contractValues = jsonInput.Articles;
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sCXZT; i++) {
                tp_mc += contractValues[i].sCXZT + ";";
                tp_hf += contractValues[i].iCXID + ",";

            }
            $("#" + CXZT).val(tp_mc);
            $("#" + CXID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zCXID).val(jsonString);
        }
    }
}


function WUC_CXHD_Return(CXZT, CXID, zCXID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + CXZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sCXZT; i++) {
                tp_mc += contractValues[i].sCXZT + ";";
                tp_hf += contractValues[i].iCXID + ",";

            }
            $("#" + CXZT).val(tp_mc);
            $("#" + CXID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zCXID).val(jsonString);
        }
    }
}
function SelectMDLC(MDMC, MDID, zMDID, Single, LCMC, LCID) {
    var data = $("#" + zMDID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/WX_MDLC/WUC_WXMDLC.aspx", {
        lock: true, width: 450, height: 450, cancel: false
        , close: function () {
            WUC_MDLC_Return(MDMC, MDID, zMDID, LCMC, LCID);
        }
    }, false);

}

function WUC_MDLC_Return(MDMC, MDID, zMDID, LCMC, LCID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_lcid = ""
            var tp_lcmc = ""
            $("#" + MDMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Code + ";";
                tp_hf += contractValues[i].Id + ",";
                tp_lcmc += contractValues[i].Name + ",";
                tp_lcid += contractValues[i].Id1;
            }
            $("#" + MDMC).val(tp_mc);//赋值
            $("#" + MDID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zMDID).val(jsonString);
            $("#HF_LCMC").val(tp_lcmc);
            $("#HF_LCID").val(tp_lcid);
        }
    }
}
function SelectLCSB(SBMC, SBID, zSBID, Single, FLMC) {
    var data = $("#" + zSBID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/LCSB/WUC_LCSB.aspx", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_LCSB_Return(SBMC, SBID, zSBID, FLMC);
        }
    }, false);

}

function WUC_LCSB_Return(SBMC, SBID, zSBID, FLMC) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_lcid = ""
            $("#" + SBMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Code + ";";
                tp_hf += contractValues[i].Id + ",";
                tp_lcid += contractValues[i].Name + ",";
            }
            $("#" + SBMC).val(tp_mc);
            $("#" + SBID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zSBID).val(jsonString);
        }
    }
}
function SelectHYKFXDW(FXDWMC, FXDWID, zFXDWID, Single) {
    var data = $("#" + zFXDWID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('FXDWReturnValue', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/HYKFXDW/WUC_HYKFXDW.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_HYKFXDW_Return(FXDWMC, FXDWID, zFXDWID);
        }
    }, false);
}

function WUC_HYKFXDW_Return(FXDWMC, FXDWID, zFXDWID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + FXDWMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].id + ",";

            }
            $("#" + FXDWMC).val(tp_mc);
            $("#" + FXDWID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zFXDWID).val(jsonString);
            $.dialog.data('IpValuesReturn', "");
        }
    }
}


function SelectSRLP(LPMC, LPDM, zLPID, bgdddm, Single, subTableID) {
    var data = $("#" + zLPID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    //  $.dialog.data('IpValuesEnable', tp_enable);
    $.dialog.open("../../WUC/LPGL/WUC_SRLP.aspx?sBGDDDM=" + bgdddm, {
        lock: true, width: 480, height: 410, cancel: false
        , close: function () {
            WUC_LP_Return2(LPMC, LPDM, zLPID, subTableID);
        }
    }, false);
}

//function WUC_LP_Return(LPMC, LPID, zLPID, subTableID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    //XXM start
//    if (subTableID && subtable != "") {
//        var subtable = $("#" + subTableID);
//        var count = subtable.jqGrid("getGridParam", "records");
//        var ids = subtable.jqGrid("getDataIDs");
//        var lpids = new Array();
//        for (var j = 0; j < ids.length; j++) {
//            lpids[j] = parseInt(subtable.jqGrid("getRowData", ids[j]).iLPID);
//        }
//    }
//    //XXM stop
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            var extendname = new Array();
//            $("#" + LPMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            if (contractValues.length > 0) {
//                for (var i = 0; i <= contractValues.length - 1; i++) {
//                    tp_mc += contractValues[i].Name + ";";
//                    tp_hf += contractValues[i].Id + ",";
//                    //XXM start
//                    extendname[i] = contractValues[i].ExtendName.split(",")[i];
//                    if (subTableID && subtable != "") {
//                        if (subTableID && subTableID != "") {
//                            //判断重复选择
//                            if (jQuery.inArray(parseInt(contractValues[i].Id), lpids) == "-1") {
//                                subtable.addRowData("subGridRow_" + count + i + 1, {
//                                    iLPID: contractValues[i].Id,
//                                    sLPMC: contractValues[i].Name
//                                });
//                            }
//                        }
//                    }
//                    //XXM stop
//                }
//            }
//            $("#" + LPMC).val(tp_mc);
//            $("#" + LPID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zLPID).val(jsonString);
//        }
//    }
//}
function WUC_LP_Return2(LPMC, LPDM, zLPID, subTableID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    //XXM start
    if (subTableID && subtable != "") {
        var subtable = $("#" + subTableID);
        var count = subtable.jqGrid("getGridParam", "records");
        var ids = subtable.jqGrid("getDataIDs");
        var lpids = new Array();
        for (var j = 0; j < ids.length; j++) {
            lpids[j] = subtable.jqGrid("getRowData", ids[j]).sLPDM;
        }
    }
    //XXM stop
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname = new Array();
            $("#" + LPMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            if (contractValues.length > 0) {
                for (var i = 0; i <= contractValues.length - 1; i++) {
                    tp_mc += contractValues[i].Name + ";";
                    tp_hf += contractValues[i].Code + ",";
                    //XXM start
                    extendname[i] = contractValues[i].ExtendName.split(",")[i];
                    if (subTableID && subtable != "") {
                        if (subTableID && subTableID != "") {
                            //判断重复选择
                            if (jQuery.inArray(parseInt(contractValues[i].Id), lpids) == "-1") {
                                subtable.addRowData("subGridRow_" + count + i + 1, {
                                    sLPDM: contractValues[i].Code,
                                    sLPMC: contractValues[i].Name
                                });
                            }
                        }
                    }
                    //XXM stop
                }
            }
            $("#" + LPMC).val(tp_mc);
            $("#" + LPDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zLPID).val(jsonString);
        }
    }
}

function WUC_YHQ_Return(YHQMC, YHQID, zYHQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + YHQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";

            }
            $("#" + YHQMC).val(tp_mc);
            $("#" + YHQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zYHQID).val(jsonString);
            WUC_YHQ_ReturnCustom();
        }
    }
}

function WUC_YHQ_ReturnCustom() {
}



//function SelectFLMC(FLMC, FLID, zFLID, Single) {
//    var data = $("#" + zFLID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WUC/FLMC/WUC_FLMC.aspx", {
//        lock: true, width: 400, height: 450, cancel: false
//        , close: function () {
//            WUC_FLMC_Return(FLMC, FLID, zFLID);
//        }
//    }, false);
//}


//function WUC_FLMC_Return(FLMC, FLID, zFLID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + FLMC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += contractValues[i].Id + ",";
//                //if (tp_return_ChoiceOne) {
//                //    tp_hf += contractValues[i].Id;
//                //} else {

//                //}
//            }
//            $("#" + FLMC).val(tp_mc);
//            $("#" + FLID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zFLID).val(jsonString);
//        }
//    }
//}


function SelectKH(KHMC, KHDM, zKHDM, Single) {
    var data = $("#" + zKHDM).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/MZKGL/WUC_SelectKH.aspx", {
        lock: true, width: 600, height: 400, cancel: false
        , close: function () {
            WUC_KH_Return(KHMC, KHDM, zKHDM);
        }
    }, false);
}


function WUC_KH_Return(KHMC, KHDM, zKHDM) {


    var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
    var dataArray = new Array();

    //有数据返回
    if (returnData != null && returnData.length > 0) {

        dataArray = JSON.parse(returnData);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
    }

    //将添加到文本框
    for (var i = 0; i <= dataArray.length - 1; i++) {
        if (dataArray[i].sKHDM != "") {


            $("#" + KHMC).val(dataArray[i].sKHMC);
            $("#" + KHDM).val(dataArray[i].sKHDM);
        }
    }
    $.dialog.data('IpValuesReturn', "");//清空数据


}



function SelectSBMC(SBMC, SBID, zSBID, Single) {
    var data = $("#" + zSBID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/SBMC/WUC_SBMC.aspx", {
        lock: true, width: 600, height: 400, cancel: false
        , close: function () {
            WUC_SBMC_Return(SBMC, SBID, zSBID);
        }
    }, false);
}


function WUC_SBMC_Return(SBMC, SBID, zSBID) {


    var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
    var dataArray = new Array();

    //有数据返回
    if (returnData != null && returnData.length > 0) {

        dataArray = JSON.parse(returnData);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
    }

    //将添加到文本框
    for (var i = 0; i <= dataArray.length - 1; i++) {
        if (dataArray[i].sSHMC != "") {


            $("#" + SBMC).val(dataArray[i].sSBMC);
            $("#" + SBID).val(dataArray[i].iSBID);
        }
    }
    $.dialog.data('IpValuesReturn', "");//清空数据


}
function SelectPDPC(PDPC, zPDPC, Single) {
    var data = $("#" + zPDPC).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/PDPC/WUC_PDPC.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_PDPC_Return(PDPC);
        }
    }, false);
}



function WUC_PDPC_Return(PDPC, zPDPC) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + PDPC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id;
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + PDPC).val(tp_hf);
            //$("#" + HYKTYPE).val(tp_hf.substr(0, tp_hf.length - 1));
            //$("#" + zHYKTYPE).val(jsonString);
        }
    }
}
//function SelectWXCXHD(CXZT, CXID, zCXID, Single) {
//    var data = $("#" + zCXID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WX_WUC/WX_CXHD/WUC_WXCXHD.aspx", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_WXCXHD_Return(CXZT, CXID, zCXID);
//        }
//    }, false);
//}

//function WUC_WXCXHD_Return(CXZT, CXID, zCXID) {


//    var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
//    var dataArray = new Array();

//    //有数据返回
//    if (returnData != null && returnData.length > 0) {

//        dataArray = JSON.parse(returnData);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
//    }

//    //将添加到文本框
//    for (var i = 0; i <= dataArray.length - 1; i++) {
//        if (dataArray[i].sKHDM != "") {


//            $("#" + CXZT).val(dataArray[i].sCXZT);
//            $("#" + CXID).val(dataArray[i].iCXID);
//        }
//    }
//    $.dialog.data('IpValuesReturn', "");//清空数据


//}





function SelectLCMC(LCMC, LCID, zLCID, Single) {
    var data = $("#" + zLCID).val();
    MDID = $("#HF_MDID").val()
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesshdm', MDID);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WX_WUC/WX_LCMD/WUC_WXLCMD.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_LCMC_Return(LCMC, LCID, zLCID);
        }
    }, false);
}



function WUC_LCMC_Return(LCMC, LCID, zLCID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + LCMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name;
                tp_hf += contractValues[i].Id;
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + LCMC).val(tp_mc);
            $("#" + LCID).val(tp_hf);
            $("#" + zLCID).val(jsonString);
        }
    }
}
//礼品供货商（非停用状态）
//function SelectGHS(GHSNAME, GHSID, zGHSID, ChoiceOne) {
//    var data = $("#" + zGHSID).val();//存的是选中列后，返回的json字符串
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('hasSelected', data);
//    $.dialog.data('isChoiceOne', ChoiceOne);
//    $.dialog.open("../../WUC/GHS/WUC_GHS.aspx", {
//        lock: true, width: 420, height: 470, cancel: false
//        , close: function () {
//            //窗口关闭，数据返回
//            WUC_GHS_Return(GHSNAME, GHSID, zGHSID);
//        }
//    }, false);
//}
//function WUC_GHS_Return(GHSNAME, GHSID, zGHSID) {
//    var tp_return = $.dialog.data('returnValues');
//    var tp_return_ChoiceOne = $.dialog.data('isChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + GHSNAME).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].sGHSMC + ";";
//                tp_hf += contractValues[i].iGHSID + ",";

//            }
//            $("#" + GHSNAME).val(tp_mc);
//            $("#" + GHSID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zGHSID).val(jsonString);
//        }
//    }
//}
//礼品进货单位（非停用状态）
function SelectJHDW(JHDWMC, JHDWID, zJHDWID, ChoiceOne) {
    var data = $("#" + zJHDWID).val();//存的是选中列后，返回的json字符串
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('hasSelected', data);
    $.dialog.data('isChoiceOne', ChoiceOne);
    $.dialog.open("../../WUC/JHDW/WUC_JHDW.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            //窗口关闭，数据返回
            WUC_JHDW_Return(JHDWMC, JHDWID, zJHDWID);
        }
    }, false);
}
function WUC_JHDW_Return(JHDWMC, JHDWID, zJHDWID) {
    var tp_return = $.dialog.data('returnValues');
    var tp_return_ChoiceOne = $.dialog.data('isChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + JHDWMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].sJHDWMC + ";";
                tp_hf += contractValues[i].iJHDWID + ",";

            }
            $("#" + JHDWMC).val(tp_mc);
            $("#" + JHDWID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zJHDWID).val(jsonString);
        }
    }
}
//礼品分类（非停用状态）
function SelectLPFL(LPFLMC, LPFLID, zLPFLID, ChoiceOne) {
    var data = $("#" + zLPFLID).val();//存的是选中列后，返回的json字符串
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('hasSelected', data);
    $.dialog.data('isChoiceOne', ChoiceOne);
    $.dialog.open("../../WUC/LPFL/WUC_LPFL.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            //窗口关闭，数据返回
            WUC_LPFL_Return(LPFLMC, LPFLID, zLPFLID);
        }
    }, false);
}
function WUC_LPFL_Return(LPFLMC, LPFLID, zLPFLID) {
    var tp_return = $.dialog.data('returnValues');
    var tp_return_ChoiceOne = $.dialog.data('isChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + LPFLMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].sLPFLMC + ";";
                tp_hf += contractValues[i].iLPFLID + ",";

            }
            $("#" + LPFLMC).val(tp_mc);
            $("#" + LPFLID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zLPFLID).val(jsonString);
        }
    }
}
//function SelectHYQY(QYMC, QYDM, zQYDM)
//{
//    var data = $("#" + zQYDM).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.open("../../WUC/HYQY/WUC_HYQY.aspx?ryid=1001", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function ()
//        {
//            WUC_HYQY_Return(QYMC, QYDM, zQYDM);
//        }
//    }, false);
//}

function WUC_HYQY_Return(QYMC, QYDM, zQYDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + QYMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                //tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";

                sjson = "{}";
                $.ajax({
                    type: 'post',
                    url: "../../CrmLib/CrmLib.ashx?func=GetHYQYMX&QYDM=" + contractValues[i].Code,
                    dataType: "json",
                    async: false,
                    cache: false,
                    data: {
                        json: JSON.stringify(sjson), titles: 'cecece'
                    },
                    success: function (data) {
                        result = "";
                        if (data && data != "null") {
                            tp_mc += data.sQYMC + ";";
                        }
                        return;

                    },
                    error: function (data) {
                        result = "";
                    }
                });
            }
            $("#" + QYMC).val(tp_mc);
            $("#" + QYDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zQYDM).val(jsonString);
        }
    }
}

//function SelectBGDD_Muti(BGDDMC, BGDDDM, zBGDDDM) {
//    var data = $("#" + zBGDDDM).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValues', el);
//    $.dialog.open("../../WUC/BGDD/WUC_BGDD.aspx?ryid=1001", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_BGDD_Return(BGDDMC, BGDDDM, zBGDDDM);
//        }
//    }, false);
//}

//function SelectHYBQ(HYBQMC, HYBQID, zHYBQID, Single, LabelType) {
//    var data = $("#" + zHYBQID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesLabelType', LabelType);
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValuesChoiceOne', Single);
//    $.dialog.open("../../WUC/HYBQ/WUC_HYBQ.aspx?ryid=1001", {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_HYBQ_Return(HYBQMC, HYBQID, zHYBQID);
//        }
//    }, false);
//}

function SelectJHDW_Muti(JHDWMC, JHDWID, zJHDWID) {
    var data = $("#" + zJHDWID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', true);
    $.dialog.open("../../WUC/JHDWTree/WUC_JHDW.aspx?ryid=1001", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_JHDWMuti_Return(JHDWMC, JHDWID, zJHDWID);
        }
    }, false);
}

function WUC_JHDWMuti_Return(JHDWMC, JHDWID, zJHDWID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + JHDWMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "" + contractValues[i].id + ",";
            }
            $("#" + JHDWMC).val(tp_mc);
            $("#" + JHDWID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zJHDWID).val(jsonString);
        }
    }
}

function WUC_BGDD_Return(BGDDMC, BGDDDM, zBGDDDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + BGDDMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
                //tp_hf += "" + contractValues[i].Code + ",";
            }
            $("#" + BGDDMC).val(tp_mc);
            $("#" + BGDDDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zBGDDDM).val(jsonString);
        }
    }
}

function WUC_HYBQ_Return(HYBQMC, HYBQID, zHYBQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + HYBQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code.substr(2, contractValues[i].Code.length - 2) + "',";
                //tp_hf += "" + contractValues[i].Code + ",";
            }
            $("#" + HYBQMC).val(tp_mc);
            $("#" + HYBQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zHYBQID).val(jsonString);
        }
    }
}


function SelectQK(QKMC, QKDM, zQKDM, isChooseOne) {
    var data = $("#" + zQKDM).val();
    if (isChooseOne) {
        $.dialog.data('ChoiceOne_QK', isChooseOne);
    }
    $.dialog.data('SelectedQkValues', data);
    $.dialog.open("../../WUC/QK/WUC_QK.aspx?ryid=1001", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_QK_Return(QKMC, QKDM, zQKDM);
        }
    }, false);
}

function WUC_QK_Return(QKMC, QKDM, zQKDM) {
    var tp_return = $.dialog.data('QKValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + QKMC).val(tp_mc);
            var jsonReturn = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonReturn;
            for (var i = 0; i <= contractValues.length - 1; i++) {

                tp_hf += contractValues[i].qkid + ",";
                tp_mc += contractValues[i].Name + ";";
            }
            $("#" + QKMC).val(tp_mc);
            $("#" + QKDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zQKDM).val(jsonString);
        }
    }
}

function SelectHYXQ(HYXQMC, HYXQDM, zHYXQDM) {
    var data = $("#" + zHYXQDM).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValues', el);
    $.dialog.open("../../WUC/XQ/WUC_XQ.aspx?ryid=1001", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_XQ_Return(HYXQMC, HYXQDM, zHYXQDM);
        }
    }, false);
}

function WUC_XQ_Return(HYXQMC, HYXQDM, zHYXQDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + HYXQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
            }
            $("#" + HYXQMC).val(tp_mc);
            $("#" + HYXQDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zHYXQDM).val(jsonString);
        }
    }
}

function SelectBMQDY1(BMQMC, BMQID, zBMQID, Single) {
    var data = $("#" + zBMQID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/BMQDY/WUC_BMQDY.aspx?", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_BMQDY_Return(BMQMC, BMQID, zBMQID);
        }
    }, false);

}

function WUC_BMQDY_Return(BMQMC, BMQID, zBMQID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + BMQMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sBMQMC; i++) {
                tp_mc += contractValues[i].sBMQMC + ";";
                tp_hf += contractValues[i].iBMQID + ",";

            }
            $("#" + BMQMC).val(tp_mc);
            $("#" + BMQID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zBMQID).val(jsonString);
        }
    }
}

function SelectLMSHDY(SHMC, LMSHID, zLMSHID, Single) {
    var data = $("#" + zLMSHID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/LMSHDY/WUC_LMSHDY.aspx?", {
        lock: true, width: 450, height: 470, cancel: false
        , close: function () {
            WUC_LMSHDY_Return(SHMC, LMSHID, zLMSHID);
        }
    }, false);

}

function WUC_LMSHDY_Return(SHMC, LMSHID, zLMSHID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + SHMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sSHMC; i++) {
                tp_mc += contractValues[i].sSHMC + ";";
                tp_hf += contractValues[i].iJLBH + ",";

            }
            $("#" + SHMC).val(tp_mc);
            $("#" + LMSHID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zLMSHID).val(jsonString);
        }
    }
}
function MakeNewTab(url, title, tabid) {
    //生成新Tab
    if (url.indexOf("?") < 0) {
        url += "?1=1";
    }
    url += "&bftitle=" + encodeURI(title);
    if (tabid == undefined) {
        alert("未传入tabid");
        return;
    }
    //if (tabid.toString().indexOf("old") >= 0)
    //    tabid = tabid.substr(0, 7);

    var curUrlPath = window.document.location.href;
    //获取主机地址之后的目录，如： abcd/index.aspx
    var pathName = window.document.location.pathname;
    var pos = curUrlPath.indexOf(pathName);
    //获取主机地址，如： http://localhost:8083
    var localhostPath = "";
    if (pos == 5) {
        localhostPath = curUrlPath;
    }
    else {
        localhostPath = curUrlPath.substring(0, pos + 1);
    }
    if (localhostPath.substr(localhostPath.length - 1) == "/" && url.substr(0, 1) == "/")
        url = url.substr(1);

    if (tabid > 0 && parent.OpenTab2 != undefined) {// 
        sCK = GetUrlParam("ck");
        sCK = "ck=" + sCK;
        if (url.indexOf("?") > 0)
            sCK = "&" + sCK;
        else
            sCK = "?" + sCK;
        parent.OpenTab2((tabid).toString(), localhostPath + url + sCK, title, true);//localhostPath暂时去掉
    }
    else {
        parent.navTab.openTab(tabid.toString() + "tab", localhostPath + url, { title: title, external: true });
        //jQuery(function ($) {
        //    //jQuery.noConflict();
        //    //(function ($) {
        //    //    $(function () {
        //    //        // 使用 $ 作为 jQuery 别名的代码
        //    //var $ = jQuery.noConflict();
        //    var liid = "li" + parent.id;
        //    parent.curdivifid = "div" + liid;
        //    if ($(window.parent.frames["subTabs"].document).find("li").length < 6) {//主界面可以打开的选项卡总数

        //        var addli = " <li class='' id='" + liid + "'><a href='" + url + "' data-toggle='tab'>" + title + "<span class='closeIcon'>×</span></a><>";
        //        var html = "<div  id='" + parent.curdivifid + "' class='iFrame' ><iframe id='subFrame" + parent.curdivifid + "'  frameborder='0' class='iFrame' name='subFrame" + parent.curdivifid + "' src='" + url + "'></iframe></div>";
        //        $(window.parent.frames["subTabs"].document).find("#mainTabs").append(addli);
        //        var bodyheight = parent.document.body.clientHeight;
        //        $("#maincontent", window.parent.document).append(html);
        //        $("#subFrame" + parent.curdivifid, window.parent.document).height(bodyheight - 44);

        //        $(window.parent.frames["subTabs"].document).find("li").attr("class", "");
        //        $(window.parent.frames["subTabs"].document).find("#" + liid).show().attr("class", "active");

        //        $("#" + parent.predivifid, window.parent.document).hide();
        //        parent.predivifid = parent.curdivifid;
        //        $("#" + parent.curdivifid, window.parent.document).show();
        //        parent.id++;
        //        window.parent.bindLi();

        //    } else { alert("请关闭其他选项！") }


        //    //    });
        //    //})(jQuery);//此方法会导致$被还原为空！
        //})
    }
};

//function SelectSHBM(SHBMMC, SHBMDM, zSHBMDM, SHDM, shbmdm) {
//    var data = $("#" + zSHBMDM).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.open("../../WUC/BM/WUC_BM.aspx?shdm=" + SHDM + "&shbmdm=" + shbmdm, {
//        lock: true, width: 400, height: 400, cancel: false
//        , close: function () {
//            WUC_SHBM_Return(SHBMMC, SHBMDM, zSHBMDM);

//        }
//    }, false);
//}

function WUC_SHBM_Return(SHBMMC, SHBMDM, zSHBMDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + SHBMMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Code + ",";
            }
            $("#" + SHBMMC).val(tp_mc);
            $("#" + SHBMDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zSHBMDM).val(jsonString);
        }
    }
}

function WUC_Return(MC, ID, zID, SHDM, SHMC, jqxhr) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname1 = "";
            var extendname2 = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                if (tp_mc != "undefined;") {
                    extendname1 += contractValues[i].ExtendName.split(",")[0] + ",";
                    extendname2 += contractValues[i].ExtendName.split(",")[1] + ",";
                }

            }
            if (tp_mc != "undefined;") {
                $("#" + MC).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                $("#" + zID).val(jsonString);
                $("#" + SHDM).val(extendname1.substr(0, extendname1.length - 1));
                $("#" + SHMC).text(extendname2.substr(0, extendname2.length - 1));
            }
            else {
                $("#" + MC).val("");
                $("#" + ID).val("");
                $("#" + zID).val("");
                $("#" + SHDM).val("");
                $("#" + SHMC).text("");
            }
            //xxm start
            if (jqxhr) {
                jqxhr.resolve("ok");
            }
            //stop
            WUC_MD_ReturnCustom();
        }
    }
}

function WUC_MD_ReturnCustom() {
}

//function SelectSQ(SQMC, SQID, zSQID, Single) {
//    var data = $("#" + zSQID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    return $.dialog.open("../../WUC/HYSQ/WUC_HYSQ.aspx", {
//        lock: true, width: 420, height: 470, cancel: false
//          , close: function () {
//              WUC_Return_SQ(SQMC, SQID, zSQID);
//          }
//    }, false);
//}

//function WUC_Return_SQ(MC, ID, zID) {
//    var tp_return = $.dialog.data('IpValuesReturn');
//    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
//    if (tp_return) {
//        var jsonString = tp_return;
//        if (jsonString != null && jsonString.length > 0) {
//            var tp_mc = "";
//            var tp_hf = "";
//            $("#" + MC).val(tp_mc);
//            var jsonInput = JSON.parse(jsonString);
//            var contractValues = new Array();
//            contractValues = jsonInput.Articles;
//            for (var i = 0; i <= contractValues.length - 1; i++) {
//                tp_mc += contractValues[i].Name + ";";
//                tp_hf += contractValues[i].Id + ",";
//            }
//            $("#" + MC).val(tp_mc);
//            $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
//            $("#" + zID).val(jsonString);
//            $("#TB_PPXQ").val("");
//            $("#HF_XQID").val("");
//            $("#HF_zXQID").val("");

//        }
//    }
//}

//function SelectLQXQ(MC, ID, zID, Single) {
//    var data = $("#" + zID).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    //$.dialog.data('IpValuesSQID', SQID);
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    return $.dialog.open("../../WUC/LQXQ/WUC_LQXQ.aspx", {
//        lock: true, width: 450, height: 550, cancel: false
//          , close: function () {
//              WUC_ReturnLQXQ(MC, ID, zID);
//          }
//    }, false);
//}

function WUC_ReturnLQXQ(MC, ID, zID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_qymc = "";
            //var tp_SQID = "";
            $("#" + MC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_qymc += contractValues[i].FreeField + ";";
                tp_hf += contractValues[i].Id + ",";
                //tp_SQID += contractValues[i].Id1 + ",";
            }
            if (tp_mc == "undefined;") {
                $("#" + MC).val("");
                $("#" + ID).val("");
                //$("#TB_SQMC").val(tp_sqmc);
                //$("#HF_SQID").val(tp_SQID.substr(0, tp_SQID.length - 1));
                $("#" + zID).val("");

            }
            else {
                $("#" + MC).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                //$("#TB_SQMC").val(tp_sqmc);
                //$("#HF_SQID").val(tp_SQID.substr(0, tp_SQID.length - 1));
                $("#" + zID).val(jsonString);
            }
            WUC_LQXQ_ReturnCustom();
        }
    }
}

function WUC_LQXQ_ReturnCustom() {

}

function SelectBGDD(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeBGDD", hideField, showField, hideData, dialogUrl, "保管地点", mult, "shortName", "id");
}
function SelectHYBQ(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeHYBQ", hideField, showField, hideData, dialogUrl, "会员标签", mult, "shortName", "actid", ConData);
}
function SelectQY(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeQY", hideField, showField, hideData, dialogUrl, "区域", mult, "shortName", "id");
}
function SelectMD(showField, hideField, hideData, mult, shdm) {
    var dialogUrl = "../../CrmArt/ListMD/CrmArt_ListMD.aspx?";
    if (shdm) {
        dialogUrl += "sSHDM=" + shdm;
    }
    MoseDialogModel("ListMD", hideField, showField, hideData, dialogUrl, "门店信息", mult, "sMDMC", "iJLBH");
};
function SelectTSLX(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListTSLX/CrmArt_ListTSLX.aspx?";
    MoseDialogModel("ListTSLX", hideField, showField, hideData, dialogUrl, "投诉类型", mult, "sLXMC", "iJLBH", ConData);
};
function SelectMEDIA(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListMEDIA/CrmArt_ListMEDIA.aspx?";
    MoseDialogModel("ListMEDIA", hideField, showField, hideData, dialogUrl, "媒体素材", mult, "sTITLE", "sMEDIA_ID", condData);
};
function SelectNEWS(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListNEWS/CrmArt_ListNEWS.aspx?";
    MoseDialogModel("ListNEWS", hideField, showField, hideData, dialogUrl, "图文素材", mult, "sNAME", "sMEDIA_ID", condData);
};
function MakePTUrl(dialogUrl) {
    dialogUrl = dialogUrl.replace("../..", pturl + "CRMWEB");
    return dialogUrl;
}
function TakePhoto(showField, hideField) {
    var dialogUrl = "../../CrmArt/WebCamera/WebCamera.aspx";
    dialogUrl = MakePTUrl(dialogUrl);
    $.dialog.open(dialogUrl, {
        lock: true, width: 340, height: 380, cancel: false,
        drag: true, fixed: false,
        close: function () {
            var bSelected = $.dialog.data('dialogSelected');
            if (bSelected) {
                var imagePath = $.dialog.data("WebCamera");
                $("#" + showField).attr('src', imagePath);
                $("#" + hideField).val(imagePath);
            }
            //else {
            //    $("#" + showField).val("");
            //}
            $.dialog.data("WebCamera", "");
            $.dialog.data('dialogSelected', "");
        }
    }, false);
}

function SelectLB(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListWXLB/CrmArt_ListWXLB.aspx?";

    MoseDialogModel("ListLB", hideField, showField, hideData, dialogUrl, "礼包信息", mult, "sLBMC", "iJLBH");
};



function SelectCLDX(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ArtCLDX/CrmArt_ArtCLDX.aspx";

    MoseDialogModel("ArtCLDX", hideField, showField, hideData, dialogUrl, "处理对象信息", mult, "sMDMC", "iJLBH", ConData, true, 800, 600);
};

function SelectYWY(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListYWY/CrmArt_ListYWY.aspx";
    MoseDialogModel("ListYWY", hideField, showField, hideData, dialogUrl, "业务员信息", mult, "sYWYMC", "iYWYID", ConData);
};

function SelectCXHD(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListCXHD/CrmArt_ListCXHD.aspx";
    MoseDialogModel("ListCXHD", hideField, showField, hideData, dialogUrl, "促销活动", mult, "sCXZT", "iCXID", condData);
}

function SelectSK(showField, hideField, hideData, condData) {
    var dialogUrl = "../../CrmArt/SK/Crm_Art_SK.aspx";
    MoseDialogModel("DialogSK", hideField, showField, hideData, dialogUrl, "CRM刷卡", "", "sHYK_NO", "iHYID", condData);
}

function SelectYHQ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListYHQ/CrmArt_ListYHQ.aspx?";
    MoseDialogModel("ListYHQ", hideField, showField, hideData, dialogUrl, "优惠券信息", mult, "sYHQMC", "iYHQID", condData);
};

//微信标签
function SelectWXBQ(showField, hideField, hideData, wxpid, wxpif, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXBQ/CrmArt_ListWXBQ.aspx?wxpid=" + wxpid + "&wxpif=" + wxpif;
    MoseDialogModel("ListWXBQ", hideField, showField, hideData, dialogUrl, "微信标签信息", mult, "sBQMC", "iTAGID", condData);
};




function SelectBMQDY(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListBMQ/CrmArt_ListBMQ.aspx?";
    MoseDialogModel("ListBMQ", hideField, showField, hideData, dialogUrl, "编码券信息", mult, "sBMQMC", "iBMQID", condData);
};

function SelectBMQLBMC(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListBMQLB/CrmArt_ListBMQLB.aspx?";
    MoseDialogModel("ListBMQLB", hideField, showField, hideData, dialogUrl, "编码券礼包信息", mult, "sLBMC", "iLBID", condData);
};


function SelectBMQFFGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListBMQFFGZ/CrmArt_ListBMQFFGZ.aspx?";
    MoseDialogModel("ListWXBMQFFGZ", hideField, showField, hideData, dialogUrl, "编码券发放规则信息", mult, "sGZMC", "iBMQFFGZID", condData);
};

function SelectWXCXHD(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXCXHD/CrmArt_ListWXCXHD.aspx";
    MoseDialogModel("ListWXCXHD", hideField, showField, hideData, dialogUrl, "移动端促销活动", mult, "sCXZT", "iJLBH", condData);
}
////短信个性化符号选择
//function SelectFH(tb, tf) {
//    var dialogUrl = " ../../WebDialog/SelectFH/SelectFH.aspx?autoshow=1";
//    MoseDialogModel("dialogFH", hideField, showField, hideData, dialogUrl, "符号选择", "", "sFH", "iID");
//}


function SelectSH(showField, hideField, hideData, mult, shdm) {
    var dialogUrl = "../../CrmArt/ListSH/CrmArt_ListSH.aspx";
    MoseDialogModel("ListSH", hideField, showField, hideData, dialogUrl, "商户信息", mult, "sSHMC", "sSHDM");
}


function SelectKLX(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListKLX/CrmArt_ListKLX.aspx?";
    MoseDialogModel("ListKLX", hideField, showField, hideData, dialogUrl, "卡类型信息", mult, "sHYKNAME", "iJLBH", condData);
}

function SelectRYXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListRY/CrmArt_ListRY.aspx";
    MoseDialogModel("ListRY", hideField, showField, hideData, dialogUrl, "人员信息", mult, "sRYMC", "iJLBH");
};

function SelectKFJL(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListKFJL/CrmArt_ListKFJL.aspx";
    MoseDialogModel("ListKFJL", hideField, showField, hideData, dialogUrl, "经理信息", mult, "sRYMC", "iKFRYID");
};

function SelectGKXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListGKXX/CrmArt_ListGKXX.aspx";
    MoseDialogModel("ListGKXX", hideField, showField, hideData, dialogUrl, "顾客信息", mult, "sHY_NAME", "iJLBH");
};

function SelectGHS(showField, hideField, hideData, mult, bj_ty) {
    var dialogUrl = "../../CrmArt/ListGHS/CrmArt_ListGHS.aspx?";
    if (bj_ty == 0 || bj_ty == 1) {
        dialogUrl += "iBJ_TY=" + bj_ty;
    }
    MoseDialogModel("ListGHS", hideField, showField, hideData, dialogUrl, "供货商信息", mult, "sGHSMC", "iJLBH");
};

function SelectFXDW(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeFXDW", hideField, showField, hideData, dialogUrl, "发行单位", mult, "shortName", "id");
}

function SelectSPSB(showField, hideField, hideData, mult, shdm) {
    var dialogUrl = "../../CrmArt/ListSPSB/CrmArt_ListSPSB.aspx?";
    if (shdm) {
        dialogUrl += "sSHDM=" + shdm;
    }
    MoseDialogModel("ListSPSB", hideField, showField, hideData, dialogUrl, "商品商标", mult, "sSBMC", "iSHSBID");
};
function SelectFLMC(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXPPFL/CrmArt_ListWXPPFL.aspx?";
    MoseDialogModel("ListPPFL", hideField, showField, hideData, dialogUrl, "品牌分类", mult, "sFLMC", "iJLBH", condData);
};
function SelectSHBM(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeSHBM", hideField, showField, hideData, dialogUrl, "商户部门", mult, "shortName", "id", condData);
}

function SelectSHSPFL(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/TreeArt/CrmArt_TreeArt.aspx";
    MoseDialogModel("TreeSHSPFL", hideField, showField, hideData, dialogUrl, "商品分类", mult, "shortName", "id", condData);
}
function SelectSHSP(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListSHSP/CrmArt_ListSHSP.aspx";
    MoseDialogModel("ListSHSP", hideField, showField, hideData, dialogUrl, "商品信息", mult, "sSPMC", "iSHSPID", condData);
}
function SelectLPXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListLP.aspx?";
    MoseDialogModel("ListLP", hideField, showField, hideData, dialogUrl, "礼品信息", mult, "sLPMC", "iLPID");
}

function SelectHYZXX(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListHYZ/CrmArt_ListHYZ.aspx?";
    MoseDialogModel("ListHYZ", hideField, showField, hideData, dialogUrl, "会员组信息", mult, "sGRPMC", "iGRPID");
}
function SelectWX_JFDHLPGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXJFDHLPGZ/CrmArt_ListWXJFDHLPGZ.aspx?";
    MoseDialogModel("ListWXJFDHLPGZ", hideField, showField, hideData, dialogUrl, "积分兑换礼品规则", mult, "sGZMC", "iGZID", condData);
};
//function SelectWXMD(showField, hideField, hideData, mult) {
//    var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";
//    MoseDialogModel("ListWXMD", hideField, showField, hideData, dialogUrl, "微信门店", mult, "sMDMC", "iJLBH");
//};

function SelectWXMD(showField, hideField, hideData, mult, iWXPID) {
    if (iWXPID == "" || iWXPID == undefined || iWXPID == 0 || iWXPID == null) {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";

    }
    else {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?iWXPID=" + iWXPID;

    }

    MoseDialogModel("ListWXMD", hideField, showField, hideData, dialogUrl, "微信门店", mult, "sMDMC", "iJLBH");
};


function SelectJFGZ(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListJFGZ/CrmArt_ListJFGZ.aspx?";
    MoseDialogModel("ListJFGZ", hideField, showField, hideData, dialogUrl, "补积分规则", mult, "sMC", "iJLBH");
};
function SelectCS(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListCS/CrmArt_ListCS.aspx?";
    MoseDialogModel("ListCS", hideField, showField, hideData, dialogUrl, "城市定义", mult, "sCSMC", "iJLBH");
};
function SelectWXLC(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXLC/CrmArt_ListWXLC.aspx?";
    MoseDialogModel("ListWXLC", hideField, showField, hideData, dialogUrl, "微信楼层", mult, "sNAME", "iJLBH", condData);
};
function SelectWXSBMC(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListWXSB/CrmArt_ListWXSB.aspx?";
    MoseDialogModel("ListWXSB", hideField, showField, hideData, dialogUrl, "微信品牌商标", mult, "sSBMC", "iJLBH");
};

function SelectWXLPFFGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXLPFFGZ/CrmArt_ListWXLPFFGZ.aspx?";
    MoseDialogModel("ListWXLPFFGZ", hideField, showField, hideData, dialogUrl, "微信礼品发放规则", mult, "sGZMC", "iJLBH", condData);
};
function SelectWXYHQGZ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListWXYHQGZ/CrmArt_ListWXYHQGZ.aspx?";
    MoseDialogModel("ListWXYHQGZ", hideField, showField, hideData, dialogUrl, "微信优惠券规则", mult, "sGZMC", "iJLBH", condData);
};
function SelectSQ(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListSQ/CrmArt_ListSQ.aspx?";
    MoseDialogModel("ListSQ", hideField, showField, hideData, dialogUrl, "商圈", mult, "sSQMC", "iJLBH");

}
function SelectLMSHLX(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListLMSHLX/CrmArt_ListLMSHLX.aspx?";
    MoseDialogModel("ListLMSHLX", hideField, showField, hideData, dialogUrl, "联盟商户类型", mult, "sLXMC", "iJLBH", condData);
}
//list列表
function SelectKCK(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListKCK/CrmArt_ListKCK.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListKCK', CheckFieldId);
}

function SelectXQ(showField, hideField, hideData, mult, conData) {
    var dialogUrl = "../../CrmArt/ListXQ/CrmArt_ListXQ.aspx";
    MoseDialogModel("ListXQ", hideField, showField, hideData, dialogUrl, "小区信息", mult, "sXQMC", "iXQID", conData);
};


function SelectHYK(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListHYK/CrmArt_ListHYK.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListHYK', CheckFieldId);
}


function SelectWXHYK(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListWXHYK/CrmArt_ListWXHYK.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListWXHYK', CheckFieldId);
}
function SelectLP(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListLP.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListLP', CheckFieldId);
}
function SelectTM(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListWXTM/CrmArt_ListWXTM.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListTM', CheckFieldId);
}

function SelectYHQZH(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListYHQZH/CrmArt_ListYHQZH.aspx?";
    var vDialogName = "ListYHQZH";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}


function SelectHYZList(showField, hideField, hideData, mult, conData) {
    var dialogUrl = "../../CrmArt/ListHYZ/CrmArt_ListHYZ.aspx?";
    MoseDialogModel("ListHYZ", hideField, showField, hideData, dialogUrl, "会员组", mult, "sGROUPMC", "iGROUPID", conData);
};



function SelectHYZ(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListHYZ/CrmArt_ListHYZ.aspx?";
    var vDialogName = "ListHYZ";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}



function SelectWXMDList(listName, DataObject, CheckFieldId, mult, iWXPID) {
    if (iWXPID == "" || iWXPID == undefined || iWXPID == 0 || iWXPID == null) {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?";

    }
    else {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx?iWXPID" + iWXPID;

    }
    var vDialogName = "ListWXMD";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);

}
function SelectKLXList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListKLX/CrmArt_ListKLX.aspx?";
    var vDialogName = "ListKLX";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);

}
function SelectMZKSKDList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListMZKSKD/CrmArt_ListMZKSKD.aspx?";
    var vDialogName = "ListMZKSKD";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectMZKSKD(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListMZKSKD/CrmArt_ListMZKSKD.aspx?";
    var vDialogName = "ListMZKSKD";
    MoseDialogModel("ListMZKSKD", hideField, showField, hideData, dialogUrl, "售卡单信息", mult, "iJLBH", "iJLBH", condData);
}
function SelectMZKSKMX(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListMZKSKMX/CrmArt_ListMZKSKMX.aspx?";
    var vDialogName = "ListMZKSKMX";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}

function SelectMZK(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListMZK/CrmArt_ListMZK.aspx?";
    var vDialogName = "ListMZK";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectBMQList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListBMQ/CrmArt_ListBMQ.aspx?";
    var vDialogName = "ListWXBMQ";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectYYJLList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListWXYYJL/CrmArt_ListWXYYJL.aspx?";
    var vDialogName = "ListYYJL";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}

function SelectLBList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListWXLB/CrmArt_ListWXLB.aspx?";
    var vDialogName = "ListLB";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}

function SelectYHQList(listName, DataObject, CheckFieldId, mult) {
    var dialogUrl = "../../CrmArt/ListYHQ/CrmArt_ListYHQ.aspx?";
    var vDialogName = "ListYHQ";
    OpenDialog(dialogUrl, listName, DataObject, vDialogName, CheckFieldId, mult);
}
function SelectXZJFDHLP(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListXZJFDHLP.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListXZJFDHLP', CheckFieldId);
}
function SelectXZLP(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListLP/CrmArt_ListXZLP.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListXZLP', CheckFieldId);
}
function SelectWXUSER(listName, DataObject, CheckFieldId) {
    var dialogUrl = "../../CrmArt/ListWXUSER/CrmArt_ListWXUSER.aspx?";
    OpenDialog(dialogUrl, listName, DataObject, 'ListWXUSER', CheckFieldId);
}

function SelectWXGroup(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListWXGroup/CrmArt_ListWXGroup.aspx";
    MoseDialogModel("ListWXGroup", hideField, showField, hideData, dialogUrl, "微信分组", mult, "sGROUP_NAME", "iGROUPID");
};

function SelectSQD(showField, hideField, hideData, mult, vCZK) {
    vCZK = vCZK || 0;
    var dialogUrl = "../../CrmArt/ListSQD/CrmArt_ListSQD.aspx?";
    dialogUrl += "vCZK=" + vCZK;
    MoseDialogModel("ListSQD", hideField, showField, hideData, dialogUrl, "申请单信息", mult, "iJLBH", "iJLBH");
};

function SelectKH(showField, hideField, hideData, mult) {
    var dialogUrl = "../../CrmArt/ListKH/CrmArt_ListKH.aspx?";
    MoseDialogModel("ListKH", hideField, showField, hideData, dialogUrl, "客户信息", mult, "sKHMC", "iJLBH");
}


function SelectWX_ZXHD(NAME, ID, zID, Single, SHDM, SHMC, jqxhr) {
    var data = $("#" + zID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('SHDM', SHDM);
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    return $.dialog.open("../../WX_WUC/WX_ZXHD/WX_ZXHD.aspx", {
        lock: true, width: 420, height: 470, cancel: false
          , close: function () {
              WUC_WX_ZXHDReturn(NAME, ID, zID, SHDM, SHMC, jqxhr);
          }
    }, false);
}
function WUC_WX_ZXHDReturn(NAME, ID, zID, SHDM, SHMC, jqxhr) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var extendname1 = "";
            var extendname2 = "";
            $("#" + NAME).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                if (tp_mc != "undefined;") {
                    extendname1 += contractValues[i].ExtendName.split(",")[0] + ",";
                    extendname2 += contractValues[i].ExtendName.split(",")[1] + ",";
                }

            }
            if (tp_mc != "undefined;") {
                $("#" + NAME).val(tp_mc);
                $("#" + ID).val(tp_hf.substr(0, tp_hf.length - 1));
                $("#" + zID).val(jsonString);
                $("#" + SHDM).val(extendname1.substr(0, extendname1.length - 1));
                $("#" + SHMC).text(extendname2.substr(0, extendname2.length - 1));
            }
            else {
                $("#" + NAME).val("");
                $("#" + ID).val("");
                $("#" + zID).val("");
                $("#" + SHDM).val("");
                $("#" + SHMC).text("");
            }
            //xxm start
            if (jqxhr) {
                jqxhr.resolve("ok");
            }
            //stop
            WUC_MD_ReturnCustom();
        }
    }
}
function SelectKFZY(ZYMC, ZYID, zZYID, Single) {
    var data = $("#" + zZYID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    return $.dialog.open("../../WUC/KFZY/WUC_KFZY.aspx", {
        lock: true, width: 420, height: 470, cancel: false
          , close: function () {
              WUC_Return_KFZY(ZYMC, ZYID, zZYID);
          }
    }, false);
}

function WUC_Return_KFZY(ZYMC, ZYID, zZYID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + ZYMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + ZYMC).val(tp_mc);
            $("#" + ZYID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zZYID).val(jsonString);
        }
    }
}

function ShowMessage(sMsg, time, cls, lock, closefunc) {
    time = time || 3;
    cls = cls || "infolog";
    if (lock == undefined)
        lock = false;

    var top = $(window).height() * 0.85;
    var width = $(window).width() * 0.9;
    art.dialog({
        lock: lock,
        top: top,
        content: "<div class='bfdialog " + cls + "' style='width:" + width + "px'>" + sMsg + "</div>",
        time: time,
    });
}

function ShowErrMessage(sMsg) {
    ShowMessage(sMsg, 999, "errorlog", true);
}

function ShowYesNoMessage(sMsg, okfunc) {
    cls = "errorlog";
    var top = $(window).height() * 0.3;
    var width = $(window).width() * 0.3;
    art.dialog({
        lock: true,
        top: top,
        content: "<div class='bfdialog " + cls + "' style='width:" + width + "px'>" + sMsg + "</div>",
        ok: okfunc,
        okVal: '是',
        cancelVal: '否',
        cancel: true
    });
}

//function SelectYHQZH(hyid, yhqid, cxid, jsrq, MDFWDM, MDFWMC, YHQJE, JSONSTRING, Single) {
//    var data = $("#" + JSONSTRING).val();
//    var el = $("<input>", { type: 'text', val: data });
//    $.dialog.data('IpValuesReturn', "");
//    if (!isNaN(YHQJE)) {
//        $.dialog.data('IpValuesReturnBalance', YHQJE);
//    }
//    $.dialog.data('IpValues', el);
//    $.dialog.data('IpValuesChoiceOne', Single);
//    return $.dialog.open("../../WUC/HYKYHQ/WUC_HYKYHQ.aspx?hyid=" + hyid + "&yhqid=" + yhqid + "&cxid=" + cxid + "&jsrq=" + jsrq,
//        {
//            lock: true, width: 740, heigth: 500, cancel: false
//          , close: function () {
//              WUC_Return_YHQZH(MDFWDM, MDFWMC, YHQJE, JSONSTRING);
//          }
//        }, false);
//}

function WUC_Return_YHQZH(MDFWDM, MDFWMC, YHQJE, JSONSTRING) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mdfwdm = "";
            var tp_mdfwmc = "";
            var tp_je = "";
            var tp_hf = "";
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mdfwdm += contractValues[i].sMDFWDM + ",";
                tp_mdfwmc += contractValues[i].sMDFWMC + ";";
                tp_je = contractValues[i].fJE + ";";

            }
            $("#" + MDFWDM).text(tp_mdfwdm.substr(0, tp_mdfwdm.length - 1));
            $("#" + MDFWMC).text(tp_mdfwmc.substr(0, tp_mdfwmc.length - 1));
            $("#" + YHQJE).text(tp_je.substr(0, tp_je.length - 1));
            $("#" + JSONSTRING).val(jsonString);
        }
    }
}

function MakePrivateNumber(pNumber) {
    ret = pNumber;
    if (pNumber.length >= 8) {
        ret = pNumber.substring(0, pNumber.length - 4) + "****";
    }
    return ret;
}

function IsNullValue(pVal, pDefVal) {
    ret = pVal;
    if (ret == "" || ret == null || ret == undefined) {
        ret = pDefVal;
    }
    return ret;
}

function setDisplay(id, is) {
    if ($("#" + id).css("display") == "none") {
        $("#" + id).css("display", "block");   //  显示
    } else {
        $("#" + id).css("display", "none");   //  隐藏
    }
    //
    //$("html,body").animate({ scrollTop: $("#" + is).offset().top }, 1000);
    //window.location.hash = "id7";
}

function DrawChartOne(ContainerName, ChartType, xAxisArray, yAxisTitle, seriesData, ChartTitle) {
    if (ChartTitle == undefined)
        ChartTitle = "";
    $('#' + ContainerName + '').highcharts({                   //图表展示容器，与div的id保持一致
        chart: {
            type: '' + ChartType + '',                       //指定图表的类型，默认是折线图（line）
        },
        title: {
            text: '' + ChartTitle + '',     //指定图表标题
            style: {
                fontSize: '10px',
            }
        },
        xAxis: {
            categories: xAxisArray   //指定x轴分组
        },
        yAxis: {
            title: {
                text: '' + yAxisTitle + ''                  //指定y轴的标题
            }
        },
        //legend: {
        //    layout: 'vertical',
        //    align: 'right',
        //    verticalAlign: 'middle',
        //    borderWidth: 0
        //},
        series: seriesData,
        credits: {
            enabled: false // 禁用版权信息
        },
        //点击事件
        //plotOptions: {
        //    series: {
        //        events : {
        //            click: function(e) {
        //                alert("点击了：" + this.name + "纵坐标：" + e.point.y + "横坐标：" + e.point.category)
        //            }
        //        }
        //    }
        //}

    });
}                       //多曲线绘图
//身份证检验
var vcity = {
    11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古",
    21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏",
    33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南",
    42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆",
    51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃",
    63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外"
};

checkCard = function (obj) {
    //var card = document.getElementById('card_no').value;
    //是否为空
    // if(card === '')
    // {

    //     return false;
    //}
    //校验长度，类型
    if (isCardNo(obj) === false) {

        return false;
    }
    //检查省份
    if (checkProvince(obj) === false) {

        return false;
    }
    //校验生日
    if (checkBirthday(obj) === false) {

        return false;
    }
    //检验位的检测
    if (checkParity(obj) === false) {

        return false;
    }
    return true;
};


//检查号码是否符合规范，包括长度，类型
isCardNo = function (obj) {
    //身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X
    var reg = /(^\d{15}$)|(^\d{17}(\d|[xX])$)/;
    if (reg.test(obj) === false) {
        return false;
    }

    return true;
};

//取身份证前两位,校验省份
checkProvince = function (obj) {
    var province = obj.substr(0, 2);
    if (vcity[province] == undefined) {
        return false;
    }
    return true;
};

//检查生日是否正确
checkBirthday = function (obj) {
    var len = obj.length;
    //身份证15位时，次序为省（3位）市（3位）年（2位）月（2位）日（2位）校验位（3位），皆为数字
    if (len == '15') {
        var re_fifteen = /^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/;
        var arr_data = obj.match(re_fifteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date('19' + year + '/' + month + '/' + day);
        return verifyBirthday('19' + year, month, day, birthday);
    }
    //身份证18位时，次序为省（3位）市（3位）年（4位）月（2位）日（2位）校验位（4位），校验位末尾可能为X
    if (len == '18') {
        var re_eighteen = /^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|[xX])$/;
        var arr_data = obj.match(re_eighteen);
        var year = arr_data[2];
        var month = arr_data[3];
        var day = arr_data[4];
        var birthday = new Date(year + '/' + month + '/' + day);
        return verifyBirthday(year, month, day, birthday);
    }
    return false;
};

//校验日期
verifyBirthday = function (year, month, day, birthday) {
    var now = new Date();
    var now_year = now.getFullYear();
    //年月日是否合理
    if (birthday.getFullYear() == year && (birthday.getMonth() + 1) == month && birthday.getDate() == day) {
        //判断年份的范围（3岁到100岁之间)
        var time = now_year - year;
        if (time >= 0 && time <= 130) {
            return true;
        }
        return false;
    }
    return false;
};

//校验位的检测
checkParity = function (obj) {
    //15位转18位
    obj = changeFivteenToEighteen(obj);
    var len = obj.length;
    if (len == '18') {
        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
        var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
        var cardTemp = 0, i, valnum;
        for (i = 0; i < 17; i++) {
            cardTemp += obj.substr(i, 1) * arrInt[i];
        }
        valnum = arrCh[cardTemp % 11];

        if (valnum == obj.substr(17, 1).toUpperCase()) {
            return true;
        }
        return false;
    }
    return false;
};

//15位转18位身份证号
changeFivteenToEighteen = function (obj) {
    if (obj.length == '15') {
        var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
        var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
        var cardTemp = 0, i;
        obj = obj.substr(0, 6) + '19' + obj.substr(6, obj.length - 6);
        for (i = 0; i < 17; i++) {
            cardTemp += obj.substr(i, 1) * arrInt[i];
        }
        obj += arrCh[cardTemp % 11];
        return obj;
    }
    return obj;
};

function SelectLMSH(tb, hf, mult) {
    $.dialog.data("dialogLMSH", "");
    $.dialog.data("dialogInput", $("#" + hf).val());
    $.dialog.open("../../CrmLib/SelectLMSH/SelectLMSH.aspx?autoshow=1&mult=" + mult, {
        lock: true,
        width: 500,
        height: 350,
        close: function () {
            LMSH_Return(tb, hf, mult);
        }
    }, false);
}
function LMSH_Return(tb, hf, mult) {
    var lst = $.dialog.data('dialogLMSH');
    var bSelected = $.dialog.data('dialogSelected');
    if (bSelected) {
        if (lst.length == 0) {
            $("#" + tb).val("");
            $("#" + hf).val("");
            WUC_LMSH_ReturnCustom();
        }
        else {
            if (!mult) {
                $("#" + tb).val(lst[0].sSHMC);
                $("#" + hf).val(lst[0].iJLBH);
                WUC_LMSH_ReturnCustom();
            }
            else {
                var tp_mc = "";
                var tp_hf = "";
                for (var i = 0; i <= lst.length - 1; i++) {
                    tp_mc += lst[i].sSHMC + ",";
                    tp_hf += lst[i].iJLBH + ",";
                }
                $("#" + tb).val(tp_mc.substr(0, tp_mc.length - 1));
                $("#" + hf).val(tp_hf.substr(0, tp_hf.length - 1));
                WUC_LMSH_ReturnCustom();
            }
        }
    }
}

function WUC_LMSH_ReturnCustom() {
}
//从查询模板挪过来的
function MakeSrchCondition(arrayObj, ElementName, FieldName, Sign, Quot) {
    if ($.trim($("#" + ElementName).val()) != "") {
        MakeSrchCondition2(arrayObj, $("#" + ElementName).val(), FieldName, Sign, Quot)
        //var ObjJLBH = new Object();
        //ObjJLBH.ElementName = FieldName;
        //ObjJLBH.ComparisonSign = Sign;
        //ObjJLBH.Value1 = $("#" + ElementName).val();
        //ObjJLBH.InQuotationMarks = Quot;
        //if (FieldName[0] == "d" && Sign == "<=") {
        //    //当日期条件为<=某天时，为避免时分秒的问题，强制转换成<某天+1
        //    var date = new Date(ObjJLBH.Value1);
        //    AddDays(date, 1);
        //    ObjJLBH.Value1 = FormatDate(date, "yyyy-MM-dd");
        //    ObjJLBH.ComparisonSign = "<";
        //}
        //arrayObj.push(ObjJLBH);
    }
}

function MakeSrchCondition2(arrayObj, Value, FieldName, Sign, Quot) {
    if (Value != undefined) {
        var ObjJLBH = new Object();
        ObjJLBH.ElementName = FieldName;
        ObjJLBH.ComparisonSign = Sign;
        ObjJLBH.Value1 = Value;
        ObjJLBH.InQuotationMarks = Quot;
        //if (FieldName[0] == "d" && Sign == "<=") {
        //    //当日期条件为<=某天时，为避免时分秒的问题，强制转换成<某天+1
        //    var date = new Date(ObjJLBH.Value1);
        //    AddDays(date, 1);
        //    ObjJLBH.Value1 = FormatDate(date, "yyyy-MM-dd");
        //    ObjJLBH.ComparisonSign = "<";
        //}
        //if (ObjJLBH.ComparisonSign == "like") {
        //    ObjJLBH.InQuotationMarks = true;
        //    ObjJLBH.Value1 = "%'||'" + ObjJLBH.Value1 + "'||'%";
        //}
        arrayObj.push(ObjJLBH);
    }
}

//返回日期格式2009-10-12
//2016-04-11
function getDateFormat(date) {
    var currentDate = date.getFullYear() + "-";
    if (date.getMonth() < 9) {
        currentDate += "0" + (date.getMonth() + 1) + "-";
    }
    else {
        currentDate += date.getMonth() + 1 + "-";
    }
    if (date.getDate() < 10) {
        currentDate += "0" + date.getDate();
    }
    else {
        currentDate += date.getDate();
    }
    return currentDate;
};
//2016-04-22
//报表日月季日期格式
function BindDateEvent(DataType) {
    switch (DataType) {
        case "1":
            WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
            break;
        case "2":
            WdatePicker({ dateFmt: 'yyyyM', isQuarter: true, disabledDates: ['....-0[5-9]-..', '....-1[0-2]-..'], startDate: '%y-01-01' });
            break;
        default:
            WdatePicker({ isShowWeek: true, });
            break;
    }
};


function GetContrastDate(CurrentDate, DataType) {
    var resultData = CurrentDate;
    DataType = parseInt(DataType);
    if (DataType == 1)
        resultData = parseInt(CurrentDate) - 100;
    if (DataType == 2)
        resultData = parseInt(CurrentDate) - 10;
    if (DataType == 0) {
        var contrastDate = new Date(CurrentDate);
        contrastDate = new Date(contrastDate.getFullYear() - 1, contrastDate.getMonth(), contrastDate.getDate());
        resultData = contrastDate
    }
    return resultData;
}

function ClearDate(dateOne, dateTwo) {
    $("#" + dateOne + "").val("");
    $("#" + dateTwo + "").val("");
    CustomerClearDate();
};

function CustomerClearDate() {
};

function SelectJJR(JJRMC, JJRID, zJJRID, Single, SelectAll) {
    var data = $("#" + zJJRID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.data('IpValuesSelectAll', SelectAll);
    $.dialog.open("../../WUC/JJR/WUC_JJR.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_JJR_Return(JJRMC, JJRID, zJJRID);
        }
    }, false);
}

function WUC_JJR_Return(JJRMC, JJRID, zJJRID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + JJRMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + JJRMC).val(tp_mc);
            $("#" + JJRID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zJJRID).val(jsonString);
        }
    }
}

function SelectYXHD(HDZT, YXHDID, zYXHDID, Single, SelectAll) {
    var data = $("#" + zYXHDID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.data('IpValuesSelectAll', SelectAll);
    $.dialog.open("../../WUC/YXHD/WUC_YXHD.aspx", {
        lock: true, width: 400, height: 400, cancel: false
        , close: function () {
            WUC_JJR_Return(HDZT, YXHDID, zYXHDID);
        }
    }, false);
}

function WUC_YXHD_Return(HDZT, YXHDID, zYXHDID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + HDZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + HDZT).val(tp_mc);
            $("#" + YXHDID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zYXHDID).val(jsonString);
        }
    }
}

function SelectYDDZXHD(ACTNAME, ACTID, zACTID, Single) {
    var data = $("#" + zACTID).val();
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesChoiceOne', Single);
    return $.dialog.open("../../WUC/YDDZXHD/WUC_YDDZXHD.aspx", {
        lock: true, width: 420, height: 470, cancel: false
          , close: function () {
              WUC_Return_YDDZXHD(ACTNAME, ACTID, zACTID);
          }
    }, false);
}

function WUC_Return_YDDZXHD(ACTNAME, ACTID, zACTID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + ACTNAME).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
            }
            $("#" + ACTNAME).val(tp_mc);
            $("#" + ACTID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zACTID).val(jsonString);
        }
    }
}

function InitPage(iMenuID) {
    return JSON.parse(GetCrmLibFunc("InitPage", "&menuid=" + iMenuID + "&ryid=" + iDJR));
}

//T--获取光标位置函数
function getCursortPosition(ctrl) {
    var CaretPos = 0; // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
        // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart == '0')
        CaretPos = ctrl.selectionStart;
    return (CaretPos);
};

function BFButtonClick(inputname, func) {
    var dis = "";
    if ($("#" + inputname).attr("disabled") == "disabled") {
        dis = " disabled='disabled'";
    }
    $("#" + inputname).parent().append("<input type='button' class='bfbtn btn_query' id='" + inputname + "_srch'" + dis + "/>");
    $("#" + inputname).click(func);
    $("#" + inputname + "_srch").click(func);
}

function BFUploadClick(input, hf, dir) {
    //图片上传
    $("#" + input).attr("readonly", "readonly");
    $("#" + input).after("<button id='upload" + input + "' class='bfbtn btn_upload'></button>");
    $("#upload" + input).after("<form id='form" + input + "' method='post' name='form1' enctype='multipart/form-data'><input type='file' name='file' dir=" + dir + " id='file" + input + "' accept='image/jpeg,image/x-png' class='uploadfile'/></form>");
    $("#upload" + input).click(function () {
        $("#file" + input).click();
    })
    $("#file" + input).change(function () {
        upLoadFile("form" + input, dir, function (data) {
            ShowMessage("上传成功");
            $("#" + input).val(data);
            $("#" + hf).val(data);
            if (typeof (BFUploadClickCustom) == "function") {
                BFUploadClickCustom();
            }
        });
    });
}

function upLoadFile(form, dir, suc) {
    //图片上传，调用jquery.form.js
    var param = { sDir: dir };
    var options = {
        type: "POST",
        url: '../../CrmLib/CrmLib.ashx?func=upload',
        dataType: "text",
        data: {
            json: JSON.stringify(param)
        },
        success: suc,
    };

    // 将options传给ajaxForm
    $("#" + form).ajaxSubmit(options);
}

//T--适用于大部分列表弹出框
//T--URL参数、取值参数后续可以数组拼接
function MoseDialogModel(dialogName, hideField, showField, hideData, dialogUrl, dialogTitle, multiSelect, nameValue, idValue, condData, autoShow, dialogWidth, dialogHeight) {
    if (autoShow == undefined) {
        autoShow == true;
    }
    $.dialog.data("dialogName", dialogName);
    $.dialog.data("dialogTitle", dialogTitle);
    if (typeof (hideData) == 'object')
        $.dialog.data("dialogInput", JSON.stringify(hideData));
    else {
        $.dialog.data("dialogInput", $("#" + hideData).val());
    }
    $.dialog.data("SingleSelect", !multiSelect);//尼玛叫Single给赋值multi
    $.dialog.data("autoShow", autoShow);
    $.dialog.data("DialogCondition", "");
    if (condData) {
        $.dialog.data("DialogCondition", JSON.stringify(condData));
    }
    var vWidth = dialogWidth || 600;
    var vHeight = dialogHeight || 550;
    if (dialogName.indexOf("Tree") >= 0) {
        vHeight = 400;
        vWidth = 400;
    }
    if (dialogName.indexOf("DialogSK") >= 0) {
        vHeight = 200;
        vWidth = 500;
    }
    dialogUrl = MakePTUrl(dialogUrl);
    $.dialog.open(dialogUrl, {
        lock: true, width: vWidth, height: vHeight, cancel: false,
        drag: true, fixed: false,
        close: function () {
            var tp_mc = "";
            var tp_hf = "";
            var bSelected = $.dialog.data('dialogSelected');
            if (bSelected) {
                var lst = JSON.parse($.dialog.data(dialogName));
                for (var i = 0; i <= lst.length - 1; i++) {
                    tp_mc += lst[i][nameValue] + ",";
                    if (typeof (lst[i][idValue]) == "string" && lst.length > 1) {
                        tp_hf += "'" + lst[i][idValue] + "',";
                    }
                    else {
                        tp_hf += lst[i][idValue] + ",";
                    }
                }
                $("#" + showField).val(tp_mc.substr(0, tp_mc.length - 1));
                $("#" + hideField).val(tp_hf.substr(0, tp_hf.length - 1));
                if (typeof (hideData) != 'object')
                    $("#" + hideData).val($.dialog.data(dialogName));
                MoseDialogCustomerReturn(dialogName, lst, showField);
            }
            else {
                if (dialogName.indexOf("DialogSK") < 0) {
                    $("#" + showField).val("");
                    $("#" + hideField).val("");
                    if (typeof (hideData) != 'object') {
                        $("#" + hideData).val("");
                    }
                }
            }
            $.dialog.data(dialogName, "");
            $.dialog.data("dialogInput", "");
            $.dialog.data("DialogCondition", "");
        }
    }, false);
};

//T--自定义返回方法
function MoseDialogCustomerReturn(dialogName, lstData, showField) {
};

function ClearDialogSelect(showField, hideField, hideData) {
    $("#" + showField).val("");
    //$("#" + showField).text("");
    $("#" + hideField).val("");
    $("#" + hideData).val("");
}
function PostToCrmlib(func, Params, suc, async) {
    if (async == undefined)
        async = true;
    var result;
    $.ajax({
        type: 'post',
        //url: "/CrmWeb/CrmLib/CrmLib.ashx?func=" + func,
        url: "../../CrmLib/CrmLib.ashx?func=" + func,
        dataType: "json",
        async: async,
        cache: false,
        //data: data,
        data: {
            json: JSON.stringify(Params)
        },
        success: function (data) {
            if (data != null) {
                if (typeof (data.indexOf) != "undefined" && data.indexOf("错误：") >= 0) {
                    ShowMessage(data.substr(data.indexOf("错误：")));
                    data = null;
                }
            }
            result = suc(data);
        },
        error: function (data) {
            //ShowErrMessage("请求错误，错误信息：" + data.responseText + "，方法名：" + func);
            ShowErrMessage("" + data.responseText + "(" + func + ")");
        }
    });
    return result;
}
function CheckMenuPermit(iPersonID, iMenuID) {
    return PostToCrmlib("CheckMenuPermit", {
        iRYID: iPersonID, iMENUID: iMenuID
    }, function (data) {
        return data;
    }, false);
}

var sCurrentPath = "";
var pturl = window.location.pathname.toUpperCase();
pturl = pturl.substr(0, pturl.indexOf("CRMWEB"));
if (pturl == "//")
    pturl = "/";
//var pturl = location.href;
//pturl = pturl.substr(0, pturl.toUpperCase().indexOf("CRMWEB"));

$(document).ready(function () {
    //尼玛不知道放哪儿
    //缩进
    //for (i = 0; i < $(".slide_down_title").length; i++) {
    //    elementName = $(".slide_down_title")[i].id;
    //    panelName = elementName + "_Hidden";
    //    $("#" + panelName).addClass("maininput3");
    //}
    $(".slide_down_title").append("<div class='btn_dropdown'><i class='fa fa-angle-down' aria-hidden='true' style='color: white'></i></div>");
    $(".slide_down_title").click(function () {
        elementName = this.id;
        panelName = elementName + "_Hidden";
        if ($("#" + panelName + "").css("display") != "none") {
            $("#" + elementName + " [class='fa fa-angle-down']").removeClass("fa fa-angle-down").addClass("fa fa-angle-left");
        }
        else {
            $("#" + elementName + " [class='fa fa-angle-left']").removeClass("fa fa-angle-left").addClass("fa fa-angle-down");
        }
        $("#" + panelName + "").slideToggle();
        ToggleHiddenPanelCustomer(elementName);
    });
    //更多菜单

    if (parent.OpenTab2 == undefined) {
        $("#morebuttons").hide();
    }
    else {
        $("#morebuttons").click(function () {
            ToggleNavigationMen();
        });
    }
    //对话框背景透明度
    var d = art.dialog.defaults;
    d.opacity = 0.1;//取消弹出框时背景变暗

    //页面地址
    sCurrentPath = window.location.href;
    sCurrentPath = sCurrentPath.substr(sCurrentPath.indexOf("//") + 2, sCurrentPath.length);
    sCurrentPath = sCurrentPath.substring(sCurrentPath.indexOf("/") + 1, sCurrentPath.indexOf(".aspx"));
    sCurrentPath = sCurrentPath.substr(0, sCurrentPath.length - 5) + ".aspx";

    //导航图用
    //$(".nav_count_wall").hide();
    //$(".nav_left").append("<div class='nav_count_tip_white'></div>");
    $(".nav_fld").append("<div class='nav_count_tip_white'></div>");
    $(".nav_fld").append("<div class='nav_count_tip_blue'></div>");
    $(".nav_count_wall").show();
});

function ToggleHiddenPanelCustomer(elementName) {
    ;
}

function LoadMenList() {
    $("#menuList").html("");
    AddBeforeCustomerMenu();
    $("#menuList").append("<li onclick='parent.SaveFav()'>加入常用</li>");
    $("#menuList").append("<li onclick='parent.closeCurTab()'>关闭当前</li>");
    $("#menuList").append("<li onclick='parent.closeOtherTab()'>关闭其它</li>");
    $("#menuList").append("<li onclick='parent.closeAllTab()'>关闭全部</li>");

    //$("#menuList").append("<li>加入常用</li>")
    //$("#menuList").append("<li>我要吐槽</li>")
    //$("#menuList").append("<li>功能评分</li>")
    //$("#menuList").append("<li>使用说明</li>")
    //$("#menuList").append("<li>流程查看</li>")
    AddAfterCustomerMenu();
}
//T-- 加载导航栏
function ToggleNavigationMen() {
    var Obj = $("[class='fa fa-list-ul fa-lg']");
    var Offset = Obj.offset();
    var html = '<div id="navigationMenu"><ul id="menuList"></ul> </div>';
    $("body").append(html);
    $("#navigationMenu").css({ left: Offset.left - 100 + "px", top: $("#btn-toolbar").offset().top + $("#btn-toolbar").outerHeight() + 5 + "px" }).slideDown("fast");
    $("body").bind("mousedown", function () {
        $("#navigationMenu").slideUp();
    });
    LoadMenList();
}

function AddAfterCustomerMenu() {;
    //
}
function AddBeforeCustomerMenu() {
    ;
}

function GetCJDYD(iGZID) {
    return PostToCrmlib("GetCJDYD", {
        iGZID: iGZID
    }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetJFDHLPDYD(iGZID) {
    return PostToCrmlib("GetJFDHLPDYD", {
        iGZID: iGZID
    }, function (data) {
        return JSON.stringify(data);
    }, false);
}

function GetSydhdy1(iID) {
    return PostToCrmlib("GetSydhdy1", {
        iID: iID
    }, function (data) {
        return JSON.stringify(data);
    }, false);
}
function GetWXSIGNData(dKSRQ, dJSRQ) {
    return PostToCrmlib("GetWXSIGNData", {
        dKSRQ: dKSRQ, dJSRQ: dJSRQ
    }, function (data) {
        return JSON.stringify(data);
    }, false);
}
// 生成多选框
function CYCBL_ADD_ITEM(s_controlname, o_data, checked) {
    var tp_str = "";
    if (o_data)
        o_data = JSON.parse(o_data);
    if (navigator.userAgent.indexOf("MSIE") > 0 || navigator.userAgent.indexOf("Trident") > 0) {
        tp_str += "<table id=\"" + s_controlname + "\" border=\"0\"> ";
        tp_str += "<tr><td>";
        for (var i = 0; i <= o_data.length - 1; i++) {
            if (document.getElementById(s_controlname) && document.getElementById(s_controlname).innerText != "" && document.getElementById(s_controlname).innerText.indexOf(o_data[i]["iXMID"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
                //if (document.getElementById(s_controlname) && document.getElementById(s_controlname).textContent != "" && document.getElementById(s_controlname).textContent.indexOf(o_data[i]["id"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
            {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" checked=\"checked\"  class=\"magic-checkbox\"/>";
            }
            else {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" class=\"magic-checkbox\" />";
            }
            tp_str += "<label for=\"" + s_controlname + "_" + i + "\">" + o_data[i]["sNR"].toString() + "</label>   ";//label的for属性目的为 使能点的区域变大
        }
        tp_str += "</td> </tr></table>";
        $("#" + s_controlname).append(tp_str);//text()替换文本 html()替换所有 val()替换value
    }
        //if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
    else {

        tp_str += "<table id=\"" + s_controlname + "\" border=\"0\"> ";
        tp_str += "<tr><td>";
        for (var i = 0; i <= o_data.length - 1; i++) {
            //if (document.getElementById(s_controlname) && document.getElementById(s_controlname).innerText != "" && document.getElementById(s_controlname).innerText.indexOf(o_data[i]["id"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
            if (document.getElementById(s_controlname) && document.getElementById(s_controlname).textContent != "" && document.getElementById(s_controlname).textContent.indexOf(o_data[i]["iXMID"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
            {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" checked=\"checked\" class=\"magic-checkbox\" />";
            }
            else {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" class=\"magic-checkbox\"/>";
            }
            tp_str += "<label for=\"" + s_controlname + "_" + i + "\">" + o_data[i]["sNR"].toString() + "</label>   ";//label的for属性目的为 使能点的区域变大
        }
        tp_str += "</td> </tr></table>";
        $("#" + s_controlname).append(tp_str);//text()替换文本 html()替换所有 val()替换value
    }
}
//选中多选框
function CYCBL_ADD_ITEM_True(s_controlname, o_data) {
    var tp_str = "";
    if (o_data)
        o_data = JSON.parse(o_data);
    if (navigator.userAgent.indexOf("MSIE") > 0 || navigator.userAgent.indexOf("Trident") > 0) {
        tp_str += "<table id=\"" + s_controlname + "\" border=\"0\"> ";
        tp_str += "<tr><td>";
        for (var i = 0; i <= o_data.length - 1; i++) {
            if (document.getElementById(s_controlname) && document.getElementById(s_controlname).innerText != "" && document.getElementById(s_controlname).innerText.indexOf(o_data[i]["iXMID"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
                //if (document.getElementById(s_controlname) && document.getElementById(s_controlname).textContent != "" && document.getElementById(s_controlname).textContent.indexOf(o_data[i]["id"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
            {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" checked=\"checked\" class=\"magic-checkbox\" />";
            }
            else {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" checked=\"true\" class=\"magic-checkbox\" />";
            }
            tp_str += "<label for=\"" + s_controlname + "_" + i + "\">" + o_data[i]["sNR"].toString() + "</label>   ";//label的for属性目的为 使能点的区域变大
        }
        tp_str += "</td> </tr></table>";
        $("#" + s_controlname).append(tp_str);//text()替换文本 html()替换所有 val()替换value
    }
        //if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
    else {

        tp_str += "<table id=\"" + s_controlname + "\" border=\"0\"> ";
        tp_str += "<tr><td>";
        for (var i = 0; i <= o_data.length - 1; i++) {
            //if (document.getElementById(s_controlname) && document.getElementById(s_controlname).innerText != "" && document.getElementById(s_controlname).innerText.indexOf(o_data[i]["id"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
            if (document.getElementById(s_controlname) && document.getElementById(s_controlname).textContent != "" && document.getElementById(s_controlname).textContent.indexOf(o_data[i]["iXMID"]) >= 0)//indexOf:取得母字符串中某字符串下标位置，从0开始，没有返回-1
            {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" checked=\"checked\" class=\"magic-checkbox\" />";
            }
            else {
                tp_str += "<input id=\"" + s_controlname + "_" + i + "\" type=\"checkbox\" name=\"" + s_controlname + "$" + i + "\" value=\"" + o_data[i]["iXMID"].toString() + "\" checked=\"checked\" class=\"magic-checkbox\" />";
            }
            tp_str += "<label for=\"" + s_controlname + "_" + i + "\">" + o_data[i]["sNR"].toString() + "</label>   ";//label的for属性目的为 使能点的区域变大
        }
        tp_str += "</td> </tr></table>";
        $("#" + s_controlname).append(tp_str);//text()替换文本 html()替换所有 val()替换value
    }
}

//初始化DataGrid列绑定
function InitColumns(sort, vColModel, vColName) {
    if (sort == undefined) {
        sort = true;
    }
    if (vColName == undefined) {
        vColName = vColumnNames;
    }
    if (vColModel == undefined) {
        vColModel = vColumnModel;
    }
    vColumns = [];
    for (var i = 0; i < vColModel.length; i++) {
        obj = new Object();
        obj.field = vColModel[i].name;
        obj.title = vColName[i];
        if (vColModel[i].width == undefined)
            obj.width = obj.title.length * 20;
        else
            obj.width = vColModel[i].width * 1.5;
        obj.hidden = vColModel[i].hidden;
        obj.sortable = sort;
        obj.editor = vColModel[i].editor;
        if (vColModel[i].formatter != undefined)
            if (typeof (vColModel[i].formatter) == "function") {
                obj.formatter = vColModel[i].formatter;
                if (vColModel[i].formatter.name == "BoolCellFormat")
                    obj.align = "center"
            }

        //obj.align = "left";
        //if (obj.field[0] == "i")
        //    obj.align = "right";
        vColumns.push(obj);
    }
    return vColumns;
};

function OpenDialog(dialogUrl, listName, data, vDialogName, CheckFieldId, multiSelect) {
    if (multiSelect == undefined)
        multiSelect = false;
    dialogUrl = MakePTUrl(dialogUrl);
    $.dialog.data("SingleSelect", multiSelect);
    $.dialog.data("DialogCondition", JSON.stringify(data));
    $.dialog.data("vDialogName", vDialogName);
    $.dialog.open(dialogUrl, {
        lock: true, width: 600, height: 550, cancel: false,
        close: function () {
            var array = [];
            var rows;
            if (!multiSelect) {
                rows = $('#' + listName).datagrid("getRows");
                for (var j = 0; j < rows.length; j++) {
                    array.push(rows[j]);
                }
            }
            if ($.dialog.data('dialogSelected') == true) {
                var lst = JSON.parse($.dialog.data(vDialogName));
                CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId);
            }
        }
    });
}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (rows.length == 0) {
        $('#' + listName).datagrid('loadData', lst, "json");
    }
    else {
        for (var i = 0; i < lst.length; i++) {
            if (CheckReapet(array, CheckFieldId, lst[i])) {//[CheckFieldId]
                $('#' + listName).datagrid('appendRow', lst[i]);
            }
        }
    }
}

function CheckReapet(array, CheckFieldId, checkFieldValue) {
    var boolInsert = true;
    for (var j = 0; j < array.length; j++) {
        if (typeof (CheckFieldId) == "object") {
            var checkData = new Array();
            var existData = new Array();
            for (var i = 0; i < CheckFieldId.length; i++) {
                checkData.push(array[j][CheckFieldId[i]]);
                existData.push(checkFieldValue[CheckFieldId[i]]);
            }
            if (checkData.toString() == existData.toString()) {
                boolInsert = false;
            }
        }
        else {
            if (array[j][CheckFieldId] == checkFieldValue[CheckFieldId]) {
                boolInsert = false;
            }
        }
    }
    return boolInsert;
}

function DeleteRows(listName) {
    var rows = $('#' + listName).datagrid("getSelections");
    var copyRows = [];
    if (rows.length == 0) {
        ShowMessage("未选中记录");
        return false;
    }
    for (var j = 0; j < rows.length; j++) {
        copyRows.push(rows[j]);
    }
    for (var i = 0; i < copyRows.length; i++) {
        var index = $('#' + listName).datagrid('getRowIndex', copyRows[i]);
        $('#' + listName).datagrid('deleteRow', index);
    }
    $('#' + listName).datagrid('clearSelections');
}

function MakeSearchJSON(listName) {
    if (listName == undefined) {
        listName = "list";
    }
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    var colModels = "", colNames = "", colWidths = "";
    var cols = $("#" + listName + "").data('datagrid').options.columns[0];
    for (var i = 0; i < cols.length; i++) {
        if (!cols[i].hidden) {
            colModels += cols[i].field + "|";
            colNames += cols[i].title + "|";
            colWidths += cols[i].width + "|";
        }
    }
    Obj.sColNames = colNames;
    Obj.sColModels = colModels;
    Obj.sColWidths = colWidths;
    Obj.sSortFiled = $("#" + listName + "").data('datagrid').options.sortName;// cols[0].field;
    Obj.sSortType = $("#" + listName + "").data('datagrid').options.sortOrder;
    Obj.iLoginRYID = iDJR;
    Obj.iLoginPUBLICID = iPID;
    if (optype == "dclrw" && jlbhlist != "") {
        //待处理任务;
    }
    AddCustomerCondition(Obj);

    return Obj;
}

function AddCustomerCondition(Obj) {
    ;
}

function MakeSearchCondition() {
    ;
}

function PostToServer(Obj, str_url, str_mode, str_suc, async, succ) {
    //统一调用这个
    if (str_suc == undefined)
        str_suc = "操作成功";
    if (async == undefined)
        async = true;
    Obj.sIPAddress = sIPAddress;
    Obj.iPageMsgID = vPageMsgID;
    //把记录编号和当前登录人摘出来统一写
    //Obj.iJLBH = $("#TB_JLBH").val();
    //if (Obj.iJLBH == "")
    //    Obj.iJLBH = "0";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    var canBeClose = false;
    var myDialog = art.dialog({
        lock: true,
        content: "<div class='bfdialog otherlog' style='width:200px'>正在提交数据,请等待……</div>",
        close: function () {
            if (canBeClose) {
                return true;
            }
            return false;
        }
    });
    $.ajax({
        type: "post",
        url: str_url + "?mode=" + str_mode + "&func=" + vPageMsgID + "&old=" + vOLDDB,
        async: async,
        data: {
            json: JSON.stringify(Obj), titles: 'cybillpost'
        },
        success: function (data) {
            canBeClose = true;
            myDialog.close();
            if (data.indexOf("错误") >= 0 || data.indexOf("error") >= 0) {
                ShowErrMessage(data);
                canBeClose = false;
            }
            else {
                if (str_suc != "")
                    ShowMessage(str_suc);
                canBeClose = true;
                vProcStatus = cPS_BROWSE;
                succ(data);
                //SearchData();
                //SetControlBaseState();
                //SearchData();
            }
        },
        error: function (data) {
            canBeClose = false;
            myDialog.close();
            ShowErrMessage("保存失败：" + data);
        }
    });
    return canBeClose;
}

function BoolCellFormat(value, row, index) {
    try {
        if (value == "0") {
            return "";
        }
        if (value == "1") {
            return "√";
        }
    }
    catch (err) {
        return "";
    }
}

function ChildCellFormat(value, row, index) {
    try {
        if (value == "0") {
            return "主卡";
        }
        if (value == "1") {
            return "从卡";
        }
    }
    catch (err) {
        return "";
    }
}

function DJZTCellFormat(value, row, index) {
    try {
        if (value == "0") {
            return "未审核";
        }
        if (value == "1") {
            return "已审核";
        }
        if (value == "2") {
            return "已启动";
        }
        if (value == "3") {
            return "已终止";
        }
    }
    catch (err) {
        return "";
    }
}


//判断URL
function IsURL(URL) {
    var str = URL;
    //判断URL地址的正则表达式为:http(s)?://([\w-]+\.)+[\w-]+(/[\w- .\%&=]*)?
    //下面的代码中应用了转义字符"\"输出一个字符"/"
    var Expression = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- .\%&=]*)?/;
    var objExp = new RegExp(Expression);
    if (objExp.test(str) == true) {
        return true;
    } else {
        return false;
    }
}


//判断是不是数字格式
function IsNumber(testChar) {
    var BoolVailed = true;
    var regstring = "^[1-9][0-9]*$";
    var ipReg = new RegExp(regstring);
    if (ipReg.test(testChar) == false) {
        BoolVailed = false;
    }
    return BoolVailed;

}

//判断输入框中输入的日期格式为yyyy-mm-dd和正确的日期  
function IsDate(mystring) {
    var reg = /^(\d{4})-(\d{2})-(\d{2})$/;
    var str = mystring;
    var arr = reg.exec(str);
    if (str == "") return false;
    if (!reg.test(str) && RegExp.$2 <= 12 && RegExp.$3 <= 31) {
        return false;
    }
    return true;
}

function SelectDYDGZ(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListDYDGZ/CrmArt_ListDYDGZ.aspx";
    MoseDialogModel("ListDYDGZ", hideField, showField, hideData, dialogUrl, "选择规则", mult, "sGZMC", "iJLBH", ConData, false);
};
function SelectHTGHS(showField, hideField, hideData, mult, ConData) {
    var dialogUrl = "../../CrmArt/ListHTGHS/CrmArt_ListHTGHS.aspx";
    MoseDialogModel("ListHTGHS", hideField, showField, hideData, dialogUrl, "供货商信息", mult, "sGHSMC", "sGHSDM");
};