<template>
    <div class="home">
        <b-pagination aria-controls="booksTable" size="md" style="margin-top:20px;"
                      :total-rows="totalRows" 
                      :per-page="10" limit="10"
                      v-model="currentPage" 
                      @input="OnPagination(currentPage)">
        </b-pagination>
        <p class="mt-3">Current Page: {{ currentPage }}</p>
        <b-table id="booksTable" striped hover :items="posts" :fields="fields" responsive="sm">
            <template v-slot:cell(thumbnailUrl)="data">
                <b-img :src="data.value" thumbnail fluid></b-img>
            </template>
            <template v-slot:cell(title_link)="data">
                <b-link :to="{ name: 'book_view', params: { 'id' : data.item.bookId } }">{{ data.item.title }}</b-link>
            </template>
        </b-table>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'Home',
        props: {
            msg: String,
            perPage: Number
        },
        created() {
            this.getPosts();
        },
        data: () => ({
            fields: [
                { key: 'thumbnailUrl', label: 'Book Image' },
                { key: 'title_link', label: 'Book Title', sortable: true, sortDirection: 'desc' },
                { key: 'isbn', label: 'ISBN', sortable: true, sortDirection: 'desc' },
                { key: 'shortDescr', label: 'Description', sortable: true, sortDirection: 'desc' }

            ],
            totalRows: 100,
            currentPage: 1,
            items: [],
            search: "",
            posts: []
        }),
        methods: {
            getPosts() {
                //onload let the page number be 1
                this.OnPagination(1, "");
            },
            OnPagination(pageNumber, filter = "") {
                axios.get("https://localhost:5001/books/?pageNumber=" + pageNumber + "&PageSize=10&SearchText=" + filter)
                    .then(response => {
                        this.posts = response.data.results;
                        this.totalRows = response.data.rowCount;

                    });
            }
        }
    }
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>

