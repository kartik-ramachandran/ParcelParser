$(function () {
    $('input.getParcelCost').click(function () {

        var RequestData = {};
        var RequestParams = [];

        var tempRequestParam = {
            "key": "Height", "value": $('input#Height').val()
        };

        RequestParams.push(tempRequestParam);

        tempRequestParam = {
            "key": "Length", "value": $('input#Length').val()
        };

        RequestParams.push(tempRequestParam);

        tempRequestParam = {
            "key": "Breadth", "value": $('input#Breadth').val()
        };

        RequestParams.push(tempRequestParam);

        tempRequestParam = {
            "key": "Weight", "value": $('input#Weight').val()
        };

        RequestParams.push(tempRequestParam);

        var RequestData = {
            "RequestParams": RequestParams
        };

        DataProvider().AjaxCall('POST', 'Home/GetParcelCost', RequestData, function (responsedata) {
            $('div.searchResult').empty();
            var resultSpan = $('<span></span>').html(responsedata.message);
            $('div.searchResult').append(resultSpan);
        }, function (errordata) { });

    });
});

function DataProvider() {    

    var AjaxCall = function (type, url, data, successHandler, errorHandler) {        

        $.ajax({
            type: type,
            url: url,
            data: {                
                RequestData: data,
            },
            success: successHandler,
            error: errorHandler
        });
    }

    return {
        AjaxCall: AjaxCall
    };
}