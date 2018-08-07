vCaption = "促销时段信息";
function DatasToChinese(sstr) {
    tp_val = new Array();
    if (sstr == "") { return "请选择促销时段!"; }

    var tp_array = new Array();
    var values0 = "000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000";
    if (sstr == values0) { return "请选择促销时段!"; }
    var values = "111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111";
    if (sstr == values) {
        return "本周";
    }
    else {
        var tp_target = sstr.split(";");
        for (var i = 0; i <= tp_target.length - 1; i++) {
            //tp_array.push("day:"+(i+1));
            //星期几 时段
            for (var j = 0; j <= 48 - 1; j++) {

                if (tp_target[i].substring(j, j + 1) == "1") {
                    if (j % 2 == 0) {
                        //tp_array.push( j / 2 + ":00~" + j / 2 + ":30");
                        tp_array.push("day:" + (i + 1) + "~day:" + (i + 2) + "," + j / 2 + ":00~" + j / 2 + ":30");
                    }
                    else {
                        //tp_array.push( (j - 1) / 2 + ":30~" + ((j - 1) / 2 + 1) + ":00");
                        if (i + 2 == 8) {
                            tp_array.push("day:" + (i + 1) + "~day:" + 8 + "," + (j - 1) / 2 + ":30~" + ((j - 1) / 2 + 1) + ":00");
                        }
                        else {
                            tp_array.push("day:" + (i + 1) + "~day:" + (i + 2) + "," + (j - 1) / 2 + ":30~" + ((j - 1) / 2 + 1) + ":00");
                        }
                    }
                }
            }
        }
    }

    var tp_st = tp_array[0].split(',')[1].split('~')[0];
    var tp_ed = tp_array[0].split(',')[1].split('~')[1];
    var tp_array2 = new Array();
    for (var i = 1; i <= tp_array.length - 1; i++) {
        if (tp_ed == tp_array[i].split(',')[1].split('~')[0]) {
            tp_ed = tp_array[i].split(',')[1].split('~')[1];
        }
        else {
            tp_array2.push(tp_array[i - 1].split(',')[0] + "," + tp_st + "~" + tp_ed);

            tp_st = tp_array[i].split(',')[1].split('~')[0];
            tp_ed = tp_array[i].split(',')[1].split('~')[1];
        }
    }
    var tp_zz2 = tp_array[tp_array.length - 1].split(',')[0];

    tp_array2.push(tp_zz2 + "," + tp_st + "~" + tp_ed);

    //
    var tpz1 = tp_array2.length;
    //day:1,2:00~9:00
    var tp_array3 = new Array();
    var tpzy = new Array();
    for (var i = 0; i <= tp_array2.length - 1; i++) {
        var tp_31 = tp_array2[i].split(',')[1];
        tpzy.splice(0, tpzy.length);
        tpzy = isCon(tp_array2, tp_31);
        if (tpzy.length > 0 && tpzy.length == tpz1) {
            var tp_zst = tpzy[0].split('~')[0];
            var tp_zed = tpzy[0].split('~')[1];
            for (var j = 1; j <= tpzy.length - 1; j++) {
                if (tp_zed == tpzy[j].split('~')[0]) {
                    tp_zed = tpzy[j].split('~')[1];
                }
                else {
                    if (tp_zst != "" && tp_zed != "") {
                        tp_array3.push(tp_zst + "~" + tp_zed + "," + tp_31);
                    }
                    tp_zst = tpzy[j].split('~')[0];
                    tp_zed = tpzy[j].split('~')[1];
                }
            }
            tp_array3.push(tp_zst + "~" + tp_zed + "," + tp_31);
            i = 0;
        }
        else {
            return "请查看明细!";
        }
    }



    //day:1~day:2,12:00~19:00
    var tp_array4 = "";
    for (var i = 0; i <= tp_array3.length - 1; i++) {
        tp_array4 += zreplace(tp_array3[i]);
    }
    if (tp_array4.length > 24) { return "请查看明细!"; }

    return tp_array4;
}
var tp_val = new Array();
function isCon(arr, val) {

    for (var i = 0; i < arr.length; i++) {
        if (arr[i].split(',')[1] == val) {
            tp_val.push(arr[i].split(',')[0]);
            arr.splice(i, 1);
            isCon(arr, val);
        }
    }
    return tp_val;
}
function zreplace(val) {
    val = val.replace("day:1~", "星期一~");
    val = val.replace("day:2~", "星期二~");
    val = val.replace("day:3~", "星期三~");
    val = val.replace("day:4~", "星期四~");
    val = val.replace("day:5~", "星期五~");
    val = val.replace("day:6~", "星期六~");
    val = val.replace("day:7~", "星期日~");
    //
    val = val.replace("~day:2", "~星期一");
    val = val.replace("~day:3", "~星期二");
    val = val.replace("~day:4", "~星期三");
    val = val.replace("~day:5", "~星期四");
    val = val.replace("~day:6", "~星期五");
    val = val.replace("~day:7", "~星期六");
    val = val.replace("~day:8", "~星期日");
    //
    return val;
}

$(document).ready(function () {
    $("#bftitle").html(vCaption);
    AddToolButtons("确定", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
});

function ArtSaveClick() {
    var tp_dataschinese = getdatas();
    $.dialog.data('DialogTimeData', tp_dataschinese);
    $.dialog.data('DialogTimeDataStr', DatasToChinese(tp_dataschinese));
    art.dialog.close();
}

function ArtCancelClick() {
    art.dialog.close();
}