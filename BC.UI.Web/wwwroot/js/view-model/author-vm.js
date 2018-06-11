//It will define the variable AuthorVM to an empty object if it is not already defined.
var Author = Author || {};

(function() {
    var self = this;
    self.UrlSaveVm = '';
    self.UrlGetVm = '';

    function VM(model) {
        var inner = this;

    }

    //Observable fields
    self.FirstName = ko.observable("Jack");
    self.LastName = ko.observable("London");
    self.FullName = ko.pureComputed(function () { return self.FirstName() + " " + self.LastName(); });
    self.Country = ko.observable("USA");
    self.Quote = ko.observable("Famous authors quote value");
    self.BooksCount = ko.observable(28);
    self.Born = ko.observable(1865);
    self.Died = ko.observable(1930);
    self.TopBooks = ["Book name 1", "Book name 2", "Book name 3", "Book name 4"];

    self.IsFavourite - ko.observable(true);
    self.Wiki = ko.observable("https://en.wikipedia.org/wiki/Jack_London");

    //Initialization
    //self.InitVM = function () {
    //    console.log("Getting Author from server...");
    //    console.log("json: ");
    //}

    self.Save = function () {
        console.log("Post Author to server...");
        var dto = ko.toJS(self);
        console.log("json: " + dto);
        $.ajax({
            url: self.UrlSaveVm,
            data: ko.toJS(self),
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded'
        }).success(console.log('Save: success!')).error(console.log('Save: error!'));
    }
//https://learn.javascript.ru/call-apply
// transfer passed context (AuthorVM) to "this" in function as a parameter
}).apply(Author);
