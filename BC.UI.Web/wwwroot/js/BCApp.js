var BCApp = BCApp || {};

(function () {
    var self = this;

    self.ProcessErrorResponse = function (data,element) {
        var fullMessage = data.message;
        console.log(data.messages);
        if (data.messages != undefined && data.messages.length > 0) {
            data.messages.forEach(function (element) {
                fullMessage = fullMessage + '<br/> * ' + element;
            });
        }
        if (data.exception != undefined) {
            var msg = "Server exception! <br/> " + data.exception;
            console.log("[SRV-EXC]" + msg);
            toastr.error(msg);
        }
        var msg = "Error! " + fullMessage;
        console.log("[SRV-ERR]" + msg);
        toastr.error(msg);
    }

}).apply(BCApp);
