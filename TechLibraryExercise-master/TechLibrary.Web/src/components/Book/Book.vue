<template>
    <div v-if="book">
        <b-form-checkbox v-model="checked" name="check-button" switch size="lg" style="margin-top:30px;">
            Edit
        </b-form-checkbox>
        <b-card :title="book.title"
                :img-src="book.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2"
                v-if="!checked">
            <b-card-text>
                {{ book.descr }}
            </b-card-text>
        </b-card>

        <b-card :title="book.title"
                :img-src="book.thumbnailUrl"
                img-alt="Image"
                img-top
                tag="article"
                style="max-width: 30rem;"
                class="mb-2"
                v-if="checked">
            <b-textarea v-model="book.descr" style="{overflow:auto;resize:none; height:170px;}" v-on:change="DecriptionChanged(book.descr)">
            </b-textarea>
        </b-card>
        <b-button to="/" variant="primary" style="">Back</b-button>
        <b-button :to="{ name: 'addbook'}" variant="success" style="margin-left:20px">ADD</b-button>
    </div>
</template>
<script lang="ts" src="./book.ts"></script>

