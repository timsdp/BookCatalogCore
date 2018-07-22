//It will define the variable BookVM to an empty object if it is not already defined.
var Book = Book || {};

(function () {
    var self = this;

    //API urls
    self.UrlSaveVm = '';
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
        Authors: ko.observableArray([{ id:0, fullName:'Test Author' }])
    }

    self.InitVM = function (entryId) {
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
    }

    self.SaveVMHandler = function () {
        if ($('#BookEditForm').valid() === false) {
            toastr.error('Client-side validation did not pass.');
            return;
        }
        console.log("Post Book to server...");
        var dto = ko.toJS(self.VM);
        console.log("json: " + dto);
        console.log(self.UrlSaveVm);
        $.ajax({
            url: self.UrlSaveVm,
            data: dto,
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded'
        }).success(function (data) {
            if (data !== undefined && data.error) {
                toastr.error('Error! ' + data.msg);
                return;
            }
            console.log('Save: success!');
            toastr.success('Update Success!');
            $(self.EditModalId).modal('hide');
            $(self.DataTableId).DataTable().ajax.reload(null, false);
        })
            .error(function () { console.log('Save: error!'); toastr.error('Update error'); });
    }

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
}).apply(Book);
