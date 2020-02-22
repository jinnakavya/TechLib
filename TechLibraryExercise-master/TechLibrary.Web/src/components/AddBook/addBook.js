import axios from 'axios';

export default {
    name: 'AddBook',
    data() {
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
        }
    },
    methods: {
        onSubmit(evt) {
            evt.preventDefault()
            alert(JSON.stringify(this.form))
            axios.post(`https://localhost:5001/books`, this.form)
                .then(response => {
                    this.success = response.data;
                    evt.target.reset();
                });
        },
        onReset(evt) {
            evt.preventDefault()
            this.form.title = ''
            this.form.isbn = ''
            this.form.publishedDate = ''
            this.form.shortDescr = ''
            this.form.longDescr = ''
            this.form.thumbnailurl = ''
            this.show = false
            this.$nextTick(() => {
                this.show = true
            })
        }
    }
}