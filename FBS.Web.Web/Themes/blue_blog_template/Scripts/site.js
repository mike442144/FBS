/// <reference path="jquery-1.3.2.min-vsdoc.js" />
$(function() {
    tab_kontrol();
});

/*tab başla*/
var aktif_tab_alan = 0;
function tab_kontrol() {
    tab_goster(aktif_tab_alan);
    $("#tabs_baslik > div").click(function() {
        var index = $("#tabs_baslik > div").index(this);
        if (aktif_tab_alan != index) {
            tab_goster(index);
        }
    }).hover(function() {
        var index = $("#tabs_baslik > div").index(this);
        if (aktif_tab_alan != index) {
            $("#tabs_baslik > div:eq(" + index + ")").css("opacity", "0.7");
        }
    }, function() {
        var index = $("#tabs_baslik > div").index(this);
        if (aktif_tab_alan != index) {
            $("#tabs_baslik > div:eq(" + index + ")").css("opacity", "0.4");
        }
    });
}
function tab_goster(index) {
    $(".loading").show().fadeOut(500);
    $("#tabs_baslik > div").css("opacity", "0.4");
    $("#tabs_baslik > div:eq(" + index + ")").css("opacity", "1");
    $("#tabs_alan > div").hide();
    $("#tabs_alan > div:eq(" + index + ")").fadeIn(500);
    aktif_tab_alan = index;
}
/*tab bitiş*/