$(document).ajaxStart(function () {
    $('body').waitMe({
        effect: "bounce",
        text: '',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000',
        maxSize: '',
        waitTime: -1,
        source: '',
        textPos: 'vertical',
        fontSize: '',
        onClose: function () { }
    });
});
$(document).ajaxComplete(function () {
    //alert('Ajax call completed');
    setTimeout(function () {
        $('body').waitMe("hide");
    }, 800);
});