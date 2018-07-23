//It will define the variable AuthorVM to an empty object if it is not already defined.
var AuthorModal = AuthorModal || {};

(function() {
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
        FirstName: ko.observable(""),
        LastName: ko.observable(""),
        FullName: ko.pureComputed(function () { return self.VM.FirstName() + " " + self.VM.LastName(); }),
        Country: ko.observable(""),
        Quote: ko.observable(""),
        BooksCount: -1,
        Born: ko.observable(-1),
        Died: ko.observable(-1),
        Rating: ko.observable(0),
        TopBooks: ko.observableArray([{ name: 'default book' }]),
        IsFavourite: ko.observable(true),
        Wiki: ko.observable("https://nourl")
    };

    self.InitVM = function (entryId) {
        console.log("Fetching Author from server...");
        $.getJSON(self.UrlGetVm + '/?id=' + entryId, function (data) {
            var model = data;
            //Init each property by new value
            self.VM.Id = model.id;
            self.VM.FirstName(model.firstName);
            self.VM.LastName(model.lastName);
            self.VM.Country(model.country);
            self.VM.Quote(model.quote);
            self.VM.BooksCount = -1;
            self.VM.Born(model.born);
            self.VM.Died(model.died);
            self.VM.Rating(model.rating);
            self.VM.TopBooks = ko.mapping.fromJS(model.topBooks);
            self.VM.IsFavourite(-1);
            self.VM.Wiki(model.wiki);
            //Or we can use automatic mapping http://knockoutjs.com/documentation/plugins-mapping.html
        });
    };

    self.SaveVMHandler = function () {
        if ($('#authorEditForm').valid() === false) {
            toastr.error('Client-side validation did not pass.');
            return;
        }
        console.log("Post Author to server...");
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

    self.Remove = function (authorId) {
        var dto = ko.toJS({ id: authorId });
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
            toastr.success('Author was removed!');
            $(self.DataTableId).DataTable().ajax.reload(null, false);
        })
            .error(function () { console.log('Removing: error!'); toastr.error('Removing error'); });
    };
//https://learn.javascript.ru/call-apply
// transfer passed context (AuthorVM) to "this" in function as a parameter
}).apply(AuthorModal);
