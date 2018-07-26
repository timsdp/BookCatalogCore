//It will define the variable BookVM to an empty object if it is not already defined.
var BookModal = BookModal || {};

(function () {
    var self = this;

    //API urls
    self.UrlSaveVm = '';
    self.UrlCreateVm = '';
    self.UrlGetVm = '';
    self.UrlRemoveVm = '';

    //Elements' ids
    self.EditModalId = '';
    self.DataTableId = '';

    //Viewmodel instance
    self.VM = {
        //Observable fields
        Id: -1,
        Name: ko.observable(""),
        Published: ko.observable(""),
        Pages: ko.observable(""),
        Rating: ko.observable(""),
        Authors: ko.observableArray([{ id: 0, fullName: 'Test Author' }])
    };

    self.ResetControls = function () {
        self.VM.Id = 0;
        self.VM.Name("");
        self.VM.Published("");
        self.VM.Pages("");
        self.VM.Rating("");
        self.VM.Authors = ko.observableArray([]);
    }

    self.InitVM = function (entryId) {
        if (entryId==null) {
            self.ResetControls();
            return;
        }
        console.log("Fetching Book from server...");
        $.getJSON(self.UrlGetVm + '/?id=' + entryId, function (data) {
            var model = data;
            //Init each property by new value
            self.VM.Id = model.id;
            self.VM.Name(model.name);
            self.VM.Published(model.published);
            self.VM.Pages(model.pages);
            self.VM.Rating(model.rating);
            self.VM.Authors = ko.mapping.fromJS(model.authors);
            //Or we can use automatic mapping http://knockoutjs.com/documentation/plugins-mapping.html
        });
    };

    self.SaveVMHandler = function () {
        if ($('#bookEditForm').valid() === false) {
            toastr.error('Client-side validation did not pass.');
            return;
        }
        var requestUrl = (self.VM.Id > 0) ? self.UrlSaveVm : self.UrlCreateVm;
        console.log("Post Book to server...");
        var dto = ko.toJS(self.VM);
        $.ajax({
            url: requestUrl,
            data: dto,
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded'
        }).success(function (data) {
            if (data !== undefined && data.error) {
                BCApp.ProcessErrorResponse(data);
                return;
            }
            console.log('Save: success!');
            toastr.success('Update Success!');
            $(self.EditModalId).modal('hide');
            $(self.DataTableId).DataTable().ajax.reload(null, false);
        })
            .error(function () { console.log('Save: error!'); toastr.error('Update error'); });
    };

    self.Remove = function (bookId) {
        var dto = ko.toJS({ id: bookId });
        $.ajax({
            url: self.UrlRemoveVm,
            data: dto,
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded'
        }).success(function (data) {
            if (data !== undefined && data.error) {
                toastr.error('Error! ' + data.msg);
                return;
            }
            toastr.success('Book was removed!');
            $(self.DataTableId).DataTable().ajax.reload(null, false);
        })
            .error(function () { console.log('Removing: error!'); toastr.error('Removing error'); });
    }
    //https://learn.javascript.ru/call-apply
    // transfer passed context (BookVM) to "this" in function as a parameter
}).apply(BookModal);
