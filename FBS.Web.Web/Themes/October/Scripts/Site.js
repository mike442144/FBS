/*-----------------*/
/*      links      */
/*-----------------*/
function styleLinks() {
    // external links
    jQuery('a[@href^="http://"]').addClass('external');
    // image links
    jQuery('a[@href$=".jpg"]').addClass('image');
    jQuery('a[@href$=".png"]').addClass('image');
    jQuery('a[@href$=".gif"]').addClass('image');
    // dpci,emt öomls
    jQuery('a[@href$=".txt"]').addClass('document');
    jQuery('a[@href$=".htm"]').addClass('document');
    jQuery('a[@href$=".html"]').addClass('document');
    jQuery('a[@href$=".doc"]').addClass('document');
    jQuery('a[@href$=".docx"]').addClass('document');
    jQuery('a[@href$=".pdf"]').addClass('document');
}

/*-----------------*/
/*      login      */
/*-----------------*/
function initLogin(partialUrl, standardUrl)
{
    if (document.location.href.indexOf(standardUrl) < 0) {
        jQuery('a[@href*="' + standardUrl + '"]').click(function(e) {
            e.preventDefault();
            var lc = jQuery('#ajaxlogin');
            if (lc.css('display') == 'none') {
                jQuery.ajax({
                    type: 'GET',
                    url: partialUrl,
                    success: function(data) {
                        lc.html(data).slideToggle();
                    },
                    error: function(XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown + "\n\n" + textStatus);
                        document.location.href = standardUrl;
                    }
                });
            }
            else {
                lc.slideToggle();
            }
        });
    }
}