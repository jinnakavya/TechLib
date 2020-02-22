import axios from 'axios';

export default {
    name: 'Book',
    props: ["id"],
    data: () => ({
        book: null,
        checked: false,
        shortDescr: ""
    }),
    mounted() {
        axios.get(`https://localhost:5001/books/${this.id}`)
            .then(response => {
                this.book = response.data;
                this.shortDescr = this.book.shortDescr;
            });
    },
    methods: {
        DecriptionChanged(updatedBook) {
            this.book.ShortDescr = updatedBook;
            this.book.Descr = updatedBook;
            axios.put(`https://localhost:5001/books`, this.book)
                .then(response => {
                    this.posts = response.data;
                    this.checked = false;
                });
        }
    }
}