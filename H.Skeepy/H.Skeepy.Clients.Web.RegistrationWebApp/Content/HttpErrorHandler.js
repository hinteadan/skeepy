class HttpErrorHandler {
    static handle(httpStatusCode) {
        if (typeof httpStatusCode === 'object') {
            httpStatusCode = httpStatusCode.status;
        }
        switch (httpStatusCode) {
            case 404://Not Found
                window.location.href = `${applicationBaseUrl}/inexistent`;
                break;
            case 410://Gone (Expired)
                window.location.href = `${applicationBaseUrl}/expired`;
                break;
        }
    }
}