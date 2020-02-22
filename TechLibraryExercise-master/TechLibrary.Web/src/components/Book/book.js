"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
exports.default = {
    name: 'Book',
    props: ["id"],
    data: function () { return ({
        book: null,
        checked: false,
        shortDescr: ""
    }); },
    mounted: function () {
        var _this = this;
        axios_1.default.get("https://localhost:5001/books/" + this.id)
            .then(function (response) {
            _this.book = response.data;
            _this.shortDescr = _this.book.shortDescr;
        });
    },
    methods: {
        DecriptionChanged: function (updatedBook) {
            var _this = this;
            this.book.ShortDescr = updatedBook;
            this.book.Descr = updatedBook;
            axios_1.default.put("https://localhost:5001/books/" + this.id, this.book)
                .then(function (response) {
                _this.posts = response.data;
                _this.checked = false;
            });
        }
    }
};
//# sourceMappingURL=book.js.map