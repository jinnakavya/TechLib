<template>
    <div>
        <h1>Add Book</h1>
        <b-form @submit="onSubmit" @reset="onReset" class="col-md-6">
            <b-form-group id="input-group-1"
                          label="Title:"
                          label-for="input-1"
                          :invalid-feedback="invalidFeedback"
                          :valid-feedback="validFeedback"
                          :state="state">
                <b-form-input id="input-1"
                              v-model="form.title"
                              type="text"
                              required
                              placeholder="Enter Title"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-2" label="ISBN:" label-for="input-2"
                          :invalid-feedback="invalidFeedback"
                          :valid-feedback="validFeedback"
                          :state="state">
                <b-form-input id="input-2"
                              v-model="form.isbn"
                              required
                              placeholder="Enter isbn"></b-form-input>
            </b-form-group>

            <b-form-group id="input-group-3" label="Published Date:" label-for="input-3"
                          :invalid-feedback="invalidFeedback"
                          :valid-feedback="validFeedback"
                          :state="state">
                <b-form-input id="input-3"
                              typeof="date"
                              v-model="form.publishedDate"
                              required
                              placeholder="Enter Date"></b-form-input>

            </b-form-group>


            <b-form-group id="input-group-4" label="URL:" label-for="input-4">
                <b-form-input id="input-4"
                              typeof="url"
                              v-model="form.thumbnailurl"
                              required
                              placeholder="Enter URL"></b-form-input>


            </b-form-group>

            <b-form-group id="input-group-5" label="Short Desc:" label-for="input-5">
                <b-form-input id="input-5"
                              typeof="text"
                              v-model="form.shortDescr"
                              required
                              placeholder="Enter Short Description"></b-form-input>


            </b-form-group>

            <b-form-group id="input-group-6" label="Long Desc:" label-for="input-6">
                <b-form-input id="input-6"
                              typeof="text"
                              v-model="form.longDescr"
                              required
                              placeholder="Enter Long Description"></b-form-input>
            </b-form-group>
            <b-button type="submit" variant="success" style="margin-right:10px">Submit</b-button>
            <b-button type="reset" variant="danger" style="margin-right:10px">Reset</b-button>
            <b-button to="/" variant="primary" style="margin-right:10px">Back</b-button>
        </b-form>

    </div>
</template>
<script lang="ts" src="./addBook.js"></script>
