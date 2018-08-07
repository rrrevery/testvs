vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "照片采集";
var vDialogName = "WebCamera";
var pos = 0;
var ctx = null;
var cam = null;
var image = null;
var filter_on = false;
var filter_id = 0;
var imageData = new Array();
var imagePath;

$(document).ready(function () {
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
    var canvas = document.getElementById("resultPhoto");
    ctx = canvas.getContext("2d");
    image = ctx.getImageData(0, 0, 320, 240);
    jQuery("#webcam").webcam({
        width: 320,
        height: 240,
        mode: "callback",
        swffile: "jscam_canvas_only.swf",
        onTick: function (remain) {
            if (0 == remain) {
                jQuery("#status").text("Cheese!");
            } else {
                jQuery("#status").text(remain + " seconds remaining...");
            }
        },
        onSave: function (data) {
            imageData.push(data);
            var col = data.split(";");
            var img = image;
            if (false == filter_on) {
                for (var i = 0; i < 320; i++) {
                    //转换为十进制
                    var tmp = parseInt(col[i]);
                    img.data[pos + 0] = (tmp >> 16) & 0xff;
                    img.data[pos + 1] = (tmp >> 8) & 0xff;
                    img.data[pos + 2] = tmp & 0xff;
                    img.data[pos + 3] = 0xff;
                    pos += 4;
                }
            }
            else {
                var id = filter_id;
                var r, g, b;
                var r1 = Math.floor(Math.random() * 255);
                var r2 = Math.floor(Math.random() * 255);
                var r3 = Math.floor(Math.random() * 255);
                for (var i = 0; i < 320; i++) {
                    var tmp = parseInt(col[i]);
                    if (id == 0) {
                        r = (tmp >> 16) & 0xff;
                        g = 0xff;
                        b = 0xff;
                    } else if (id == 1) {
                        r = 0xff;
                        g = (tmp >> 8) & 0xff;
                        b = 0xff;
                    } else if (id == 2) {
                        r = 0xff;
                        g = 0xff;
                        b = tmp & 0xff;
                    } else if (id == 3) {
                        r = 0xff ^ ((tmp >> 16) & 0xff);
                        g = 0xff ^ ((tmp >> 8) & 0xff);
                        b = 0xff ^ (tmp & 0xff);
                    } else if (id == 4) {

                        r = (tmp >> 16) & 0xff;
                        g = (tmp >> 8) & 0xff;
                        b = tmp & 0xff;
                        var v = Math.min(Math.floor(.35 + 13 * (r + g + b) / 60), 255);
                        r = v;
                        g = v;
                        b = v;
                    } else if (id == 5) {
                        r = (tmp >> 16) & 0xff;
                        g = (tmp >> 8) & 0xff;
                        b = tmp & 0xff;
                        if ((r += 32) < 0) r = 0;
                        if ((g += 32) < 0) g = 0;
                        if ((b += 32) < 0) b = 0;
                    } else if (id == 6) {
                        r = (tmp >> 16) & 0xff;
                        g = (tmp >> 8) & 0xff;
                        b = tmp & 0xff;
                        if ((r -= 32) < 0) r = 0;
                        if ((g -= 32) < 0) g = 0;
                        if ((b -= 32) < 0) b = 0;
                    } else if (id == 7) {
                        r = (tmp >> 16) & 0xff;
                        g = (tmp >> 8) & 0xff;
                        b = tmp & 0xff;
                        r = Math.floor(r / 255 * r1);
                        g = Math.floor(g / 255 * r2);
                        b = Math.floor(b / 255 * r3);
                    }
                    img.data[pos + 0] = r;
                    img.data[pos + 1] = g;
                    img.data[pos + 2] = b;
                    img.data[pos + 3] = 0xff;
                    pos += 4;
                }
            }

            if (pos >= 4 * 320 * 240) {
                ctx.putImageData(img, 0, 0);
                pos = 0;
            }

        },
        onCapture: function () {
            webcam.save();
            UploadPhoto();
        },
    });
    $("#takePhoto").click(function () {
        webcam.capture();
        changeFilter();
        $("#showImage").show();
        $("#webcam").hide();
        $("#takeAgain").prop("disabled", false);
        $("#takePhoto").prop("disabled", true);
    });

    $("#takeAgain").click(function () {
        filter_on = false;
        filter_id = 0;
        imageData = new Array();
        imagePath = "";
        $("#showImage").hide();
        $("#webcam").show();
        $("#takeAgain").prop("disabled", true);
        $("#takePhoto").prop("disabled", false);
    });
});

function ArtSaveClick() {
    $.dialog.data('dialogSelected', imagePath != "");
    $.dialog.data('WebCamera', imagePath);
    $.dialog.close();
}

function changeFilter() {
    if (filter_on) {
        filter_id = (filter_id + 1) & 7;
    }
}

function ArtCancelClick() {
    $.dialog.close();
}

function UploadPhoto() {
    imagePath = "";
    PostToCrmlib("SavePhoto", { sData: imageData.join('|'), sDir: "HYTX", sFileName: "test" }, function (data) {
        imagePath = data;
    });
}