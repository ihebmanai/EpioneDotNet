
//On init
$(".messages").animate({scrollTop: $(document).height()+100}, "fast");
$('.contact-profile').hide();
$('.messages').hide();




$("#profile-img").click(function () {
    $("#status-options").toggleClass("active");
});

$(".expand-button").click(function () {
    $("#profile").toggleClass("expanded");
    $("#contacts").toggleClass("expanded");
});

$("#status-options ul li").click(function () {
    $("#profile-img").removeClass();
    $("#status-online").removeClass("active");
    $("#status-away").removeClass("active");
    $("#status-busy").removeClass("active");
    $("#status-offline").removeClass("active");
    $(this).addClass("active");

    if ($("#status-online").hasClass("active")) {
        $("#profile-img").addClass("online");
    } else if ($("#status-away").hasClass("active")) {
        $("#profile-img").addClass("away");
    } else if ($("#status-busy").hasClass("active")) {
        $("#profile-img").addClass("busy");
    } else if ($("#status-offline").hasClass("active")) {
        $("#profile-img").addClass("offline");
    } else {
        $("#profile-img").removeClass();
    }

    $("#status-options").removeClass("active");
});

$(".contact").click(function (e) {
    $(".contact").removeClass("active");
    $(this).addClass("active");

// console.log($(this).getAttribute("id").val());
var idConversation = e.currentTarget.attributes.id.value;
console.log('#'+idConversation);

console.log($(this.getElementsByClassName("name")).text());



$('.contact-profile').hide();
$('.messages').hide();

$('#contact-profile-'+idConversation).show();
$('#messages-'+idConversation).show();
$('#messages-'+idConversation).animate({scrollTop: $(document).height()}, "fast");
    
})

  

$('.submit').click(function () {
    newMessage();
});

$(window).on('keypress', function (e) {
    if (e.which === 13) {
        newMessage();
        return false;
    }
});

// Filter contact by Name
$(document).ready(function(){
    $("#searchInput").on("keypress", function() {
        var value = $(this).val().toLowerCase().trim();
        $("#contacts .contact").filter(function() {
            $(this).toggle($(this.getElementsByClassName("name")).text().toLowerCase().indexOf(value) > -1);
        });
    });
});

function convertCharStr2SelectiveCPs ( str, parameters, pad, before, after, base ) { 
    // converts a string of characters to code points or code point based escapes
    // str: string, the string to convert
    // parameters: string enum [ascii, latin1], a set of characters to not convert
    // pad: boolean, if true, hex numbers lower than 1000 are padded with zeros
    // before: string, any characters to include before a code point (eg. &#x for NCRs)
    // after: string, any characters to include after (eg. ; for NCRs)
    // base: string enum [hex, dec], hex or decimal output
    // Usage : 
    //          result = convertCharStr2SelectiveCPs( message, 'ascii',  false, '&#', ';', 'dec' );
    //          result = convertCharStr2SelectiveCPs( str, 'ascii', true, '&#x', ';', 'hex' );
    var haut = 0; 
    var n = 0; var cp;
    var CPstring = '';
    for (var i = 0; i < str.length; i++) {
        var b = str.charCodeAt(i); 
        if (b < 0 || b > 0xFFFF) {
            CPstring += 'Error in convertCharStr2SelectiveCPs: byte out of range ' + dec2hex(b) + '!';
            }
        if (haut != 0) {
            if (0xDC00 <= b && b <= 0xDFFF) {
                if (base == 'hex') {
                    CPstring += before + dec2hex(0x10000 + ((haut - 0xD800) << 10) + (b - 0xDC00)) + after;
                    }
                else { cp = 0x10000 + ((haut - 0xD800) << 10) + (b - 0xDC00);
                    CPstring += before + cp + after; 
                    }
                haut = 0;
                continue;
                }
            else {
                // CPstring += 'Error in convertCharStr2SelectiveCPs: surrogate out of range ' + dec2hex(haut) + '!';
                haut = 0;
                }
            }
        if (0xD800 <= b && b <= 0xDBFF) {
            haut = b;
            }
        else {
            if (parameters.match(/ascii/) && b <= 127) { //  && b != 0x3E && b != 0x3C &&  b != 0x26) {
                CPstring += str.charAt(i);
                }
            else if (b <= 255 && parameters.match(/latin1/)) { // && b != 0x3E && b != 0x3C &&  b != 0x26) {
                CPstring += str.charAt(i);
                }
            else { 
                if (base == 'hex') {
                    cp = dec2hex(b); 
                    if (pad) { while (cp.length < 4) { cp = '0'+cp; } }
                    }
                else { cp = b; }
                CPstring += before + cp + after; 
                }
            }
        }
    return CPstring;
    }
function dec2hex ( textString ) {
    return (textString+0).toString(16).toUpperCase();
    }
    