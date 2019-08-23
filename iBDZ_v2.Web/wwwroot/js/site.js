// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

/**
 * @param {string} url Address to send data to.
 * @param {Object} data Data to be sent.
 * @param {function} onSuccess Callback for when the request succedes.
 * @param {function} onError Callback for when the request fails.
 */
function AjaxPostAsync(url, data, onSuccess, onError) {
    let xhr = new XMLHttpRequest()
    xhr.open("POST", url, true);
    xhr.onload = onSuccess
    xhr.onerror = onError
    xhr.send(JSON.stringify(data));
}

/**
 * @param {string} url Address to get data from.
 * @param {function} onSuccess Callback for when the request succedes.
 * @param {function} onError Callback for when the request fails.
 */
function AjaxGetAsync(url, onSuccess, onError) {
    let xhr = new XMLHttpRequest()
    xhr.open("GET", url, true);
    xhr.onload = onSuccess
    xhr.onerror = onError
    xhr.send();
}