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
        FirstName : ko.observable(""),
        LastName : ko.observable(""),
        FullName : ko.pureComputed(function () { return self.VM.FirstName() + " " + self.VM.LastName(); }),
        Country : ko.observable(""),
        Quote : ko.observable(""),
        BooksCount : -1,
        Born : ko.observable(-1),
        Died: ko.observable(-1),
        Rating: ko.observable(0),
        TopBooks : ["Book name 1", "Book name 2", "Book name 3", "Book name 4"],
        IsFavourite : ko.observable(true),
        Wiki : ko.observable("https://nourl")
    }

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
            self.VM.TopBooks = ["Book name 1", "Book name 2", "Book name 3", "Book name 4"];
            self.VM.IsFavourite(-1);
            self.VM.Wiki(model.wiki);
            //Or we can use automatic mapping http://knockoutjs.com/documentation/plugins-mapping.html
        });
    }

    self.SaveVMHandler = function () {
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
