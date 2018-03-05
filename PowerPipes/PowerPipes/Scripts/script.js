function CallController(url, data, context) {
    var httpMethod = 'post';
    if (!data) { httpMethod = 'get'; }
    return $.ajax(
        {
            url: url,
            context: context,
            cache: false,
            type: httpMethod,
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (result) {
                var isRedirect = false;
                try {
                    isRedirect = result.isRedirect;
                }
                finally {
                    if (isRedirect) {
                        window.location.href = result.redirectUrl;
                    }
                }
            }
        }
    );
}

function Redirect(url, data) {
    if (!data) {
        window.location.href = url;
    }
    else {
        CallController(url, data);
    }
}