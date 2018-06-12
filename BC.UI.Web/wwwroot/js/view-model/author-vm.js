//It will define the variable AuthorVM to an empty object if it is not already defined.
var Author = Author || {};

(function() {
    var self = this;

    //API urls
    self.UrlSaveVm = '';
    self.UrlGetVm = '';

    //Viewmodel instance
    self.VM = {
        //Observable fields
        Id: 999,
        FirstName : ko.observable("Jack"),
        LastName : ko.observable("London"),
        FullName : ko.pureComputed(function () { return self.VM.FirstName() + " " + self.VM.LastName(); }),
        Country : ko.observable("USA"),
        Quote : ko.observable("Famous authors quote value"),
        BooksCount : ko.observable(28),
        Born : ko.observable(1865),
        Died: ko.observable(1930),
        Rating: ko.observable(3),
        TopBooks : ["Book name 1", "Book name 2", "Book name 3", "Book name 4"],
        IsFavourite : ko.observable(true),
        Wiki : ko.observable("https://en.wikipedia.org/wiki/Jack_London")
    }

    self.GetVM = function () {
        console.log("Post Author to server...");
        var dto = ko.toJS(self.VM);
        console.log("json: " + dto);
        console.log(self.UrlSaveVm);
        $.ajax({
            url: self.UrlSaveVm,
            data: dto,
            type: 'GET',
            contentType: 'application/x-www-form-urlencoded'
        }).success(function () { console.log('Save: success!'); toastr.success('Update Success!'); }).error(function () { console.log('Save: error!'); toastr.error('Update error'); });
    }

    self.SaveVM = function () {
        console.log("Post Author to server...");
        var dto = ko.toJS(self.VM);
        console.log("json: " + dto);
        console.log(self.UrlSaveVm);
        $.ajax({
            url: self.UrlSaveVm,
            data: dto,
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded'
        }).success(function () { console.log('Save: success!'); toastr.success('Update Success!'); }).error(function(){ console.log('Save: error!'); toastr.error('Update error'); });
    }
//https://learn.javascript.ru/call-apply
// transfer passed context (AuthorVM) to "this" in function as a parameter
}).apply(Author);
