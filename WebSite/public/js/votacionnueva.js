String.prototype.format = function () {
    var txt = this;
    for (var i = 0; i < arguments.length; i++) {
        var exp = new RegExp('\\{' + (i) + '\\}', 'gm');
        txt = txt.replace(exp, arguments[i]);
    }
    return txt;
}

function genQR() {
    var tweet = '{0}¬{1}¬{2}¬{3}¬{4}';
    tweet = tweet.format($('#corporacion').val(), $('#proyecto').val(), $('#numero').val(), $('#anio').val(), $('#url').val());
    tweet = encodeURIComponent(tweet);
    var urlQR = 'http://chart.apis.google.com/chart?cht=qr&chs=300x300&chl={0}&chld=H|0'
    $('#qr').attr("src", urlQR.format(tweet));
}