function showInLightbox(panel, closeHandler) {
    $.fancybox(
        {
            'content': panel,
            'scrolling': 'no',
            'titleShow': false,
            'hideOnOverlayClick': false,
            'transitionIn': 'none',
            'transitionOut': 'none',
            'onClosed': function () { if (closeHandler != undefined) closeHandler(); closeLightbox }
        }
    );
}

function closeLightbox() {
    $.fancybox.close();
}