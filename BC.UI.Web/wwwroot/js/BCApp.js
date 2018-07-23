var BCApp = BCApp || {};

(function () {
    var self = this;

    //API urls
    self.UrlSaveVm = '';
    self.UrlGetVm = '';
    self.UrlRemoveVm = '';

    self.ProcessErrorResponse = function (data,element) {
        var fullMessage = data.message;
        console.log(data.messages);
        if (data.messages.length > 0) {
            data.messages.forEach(function (element) {
                fullMessage = fullMessage + '<br/> * ' + element;
            });
        }

        toastr.error("Error! " + fullMessage);
    }

}).apply(BCApp);
