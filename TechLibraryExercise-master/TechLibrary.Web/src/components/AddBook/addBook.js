"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
exports.default = {
    name: 'AddBook',
    data: function () {
        return {
            form: {
                title: '',
                isbn: '',
                publishedDate: '',
                shortDescr: '',
                longDescr: '',
                thumbnailurl: ''
            },
            show: true,
            success: false
        };
    },
    methods: {
        onSubmit: function (evt) {
            var _this = this;
            evt.preventDefault();
            alert(JSON.stringify(this.form));
            axios_1.default.post("https://localhost:5001/books", this.form)
                .then(function (response) {
                _this.success = response.data;
                evt.target.reset();
            });
        },
        onReset: function (evt) {
            var _this = this;
            evt.preventDefault();
            this.form.title = '';
            this.form.isbn = '';
            this.form.publishedDate = '';
            this.form.shortDescr = '';
            this.form.longDescr = '';
            this.form.thumbnailurl = '';
            this.show = false;
            this.$nextTick(function () {
                _this.show = true;
            });
        }
    }
};
//# sourceMappingURL=addBook.js.map