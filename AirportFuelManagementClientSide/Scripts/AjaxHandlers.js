
var baseURL = 'https://localhost:7053/api';

function makeGetRequest(url, successCallback, errorCallback) {
    $.ajax({
        url: baseURL + url,
        type: 'GET',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: successCallback,
        error: errorCallback
    });
}

function makeDeleteRequest(url, successCallback, errorCallback) {
    $.ajax({
        url: baseURL + url,
        type: 'DELETE',
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: successCallback,
        error: errorCallback
    });
}

function makePostRequest(url, data, successCallback, errorCallback) {
    $.ajax({
        url: baseURL + url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: successCallback,
        error: errorCallback
    });
}

function makePutRequest(url, data, successCallback, errorCallback) {
    $.ajax({
        url: baseURL + url,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(data),
        headers: {
            'Authorization': 'Bearer ' + (localStorage.getItem('jwtToken') || null)
        },
        success: successCallback,
        error: errorCallback
    });
}