document.body.addEventListener('htmx:configRequest', function (event) {
    var token = document.querySelector('meta[name="x-request-verification-token"]').content;
    if (token) {
        event.detail.headers['RequestVerificationToken'] = token;
    }
});