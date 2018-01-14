<template>
  <div class="columns">
    <div class="column is-4 is-offset-4">
      <card title="Add new book" icon="book">
        <form @submit="addBook">
          <b-field label="Title">
            <b-input
              type="text"
              name="title"
              required
              v-model="title"
              placeholder='Title'
            ></b-input>
          </b-field>

          <b-field label="ISBN">
            <b-input
              type="text"
              name="isbn"
              required
              v-model="isbn"
              placeholder='ISBN'
            ></b-input>
          </b-field>

          <b-field label="Choose author">
              <b-autocomplete
                  v-model="authorName"
                  :data="filteredDataArray"
                  icon="magnify"
                  required
                  @select="option => selected = option">
                  placeholder="Author"
                  <template slot="empty">Brak takiego autora, trzeba najpierw go stworzyć.</template>
              </b-autocomplete>
          </b-field>

          <b-field label="Choose category">
              <b-autocomplete
                  v-model="name"
                  :data="categories"
                  icon="magnify"
                  required
                  @select="option => selectedCategory = option">
                  placeholder="Category"
                  <template slot="empty">Brak takiej kategorii, trzeba najpierw ją stworzyć.</template>
              </b-autocomplete>
          </b-field>


          <b-field label="Description">
            <b-input
              type="textarea"
              v-model="description"
              class="is-half  "
              required
              placeholder='Description'
            ></b-input>
          </b-field>

          <div class="has-text-danger has-text-centered">{{error}}</div>
          <button type="submit" :class="['button', 'is-primary', 'is-fullwidth', {'is-loading': pending}]">Add</button>
        </form>
      </card>
    </div>
  </div>
</template>

<script>
  import axios from 'axios';
  // import _ from 'lodash';
  export default {
    data () {
      return {
        title: '',
        description: '',
        pending: false,
        error: '',
        selected: '',
        categoryName: '',
        selectedCategory: '',
        name: '',
        authorName: '',
        isbn: '',
        categoriesList: [],
        authorList: []
      };
    },
    created: function () {
      this.$store.dispatch('getAuthors');
      this.$store.dispatch('getCategories');
    },
    methods: {
      addBook (e) {
        e.preventDefault();
        var authorID;
        let name = this.authorName.split(' ')[0];
        let surname = this.authorName.split(' ')[1];

        this.$store.getters.authors.map((author) => {
          if (author.name === name && author.surname === surname) {
            authorID = author.id;
          };
        });

        let selectedCategory = this.selectedCategory;
        let cat = this.$store.getters.categories.find(function (category) { return category.name === selectedCategory; });

        axios.post('http://localhost:5000/api/book', {
          title: this.title,
          isbn: this.isbn,
          authorId: authorID,
          description: this.description,
          categoryId: cat.id
        }, {
          headers: {
            'Authorization': 'Bearer ' + this.$store.getters.token
          }
        }).then((response) => {
          this.$router.push({ path: '/book' });
        });
      }
    },
    computed: {
      filteredDataArray () {
        return this.$store.getters.authors.map((author) => {
          return author.name + ' ' + author.surname;
        });
      },
      categories () {
        return this.$store.getters.categories.map((category) => {
          return category.name;
        });
      }
    }
  };
</script>
