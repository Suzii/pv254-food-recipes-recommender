function NetworkError(response) {
    this.name = "Network Error";
    this.status = response.status;
    this.message = response.statusText;

    this.toString = () => `${this.name}: ${this.status}. ${this.message}`;
}

function getQueryParameters (params) {
    let esc = encodeURIComponent;
    let query = Object.keys(params)
        .map(k => esc(k) + '=' + esc(params[k]))
        .join('&');

    return query;
}

var parseJson = function (response) {
    return response.json();
};

var processStatus = function (response) {
    if(response.status >= 200 && response.status < 300) {
        return response;
    } else {
        throw new NetworkError(response);
    }
};

var logNetworkError = function (error) {
    if(error.name === "Network Error") {
        console.log(error.toString());
    } else {
        throw error;
    }
};

export {
    getQueryParameters,
    parseJson,
    processStatus,
    logNetworkError
};
