<template>
  <div class="register-form">
    <at-card class="register-card" style="width: 300px;">
      <h1 slot="title">Add new book</h1>
      <div>
        <at-alert v-if="error" message="Somenthing went wrong, please check data you provided" type="error"></at-alert>
        <at-input size="large" v-model="title" placeholder="Title"></at-input>
        <at-input size="large" v-model="isbn" :status="isbnStatus" placeholder="ISBN"></at-input>
        <at-input size="large" v-model="description" type="textarea" placeholder="Description"></at-input>

        <at-select class="select" filterable size="large" v-model="selectedCategory" placeholder="Select category">
          <at-option v-for="category in categories" :value="category.name">{{ category.name }}</at-option>
        </at-select>

        <at-select class="select" filterable size="large" v-model="authorName" placeholder="Select author">
          <at-option v-for="category in filteredDataArray" :value="category.name">{{ category.name }}</at-option>
        </at-select>

        <at-button @click="addBook" class="register-button" size="large" type="primary" hollow>Add</at-button>
      </div>
    </at-card>
  </div>

</template>

<script>
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
        if (this.authorName === '' || !this.selectedCategory || this.title === '' || this.description === '') {
          this.error = true;
        }

        let name = this.authorName.split(' ')[0];
        let surname = this.authorName.split(' ')[1];

        this.$store.getters.authors.map((author) => {
          if (author.name === name && author.surname === surname) {
            authorID = author.id;
          };
        });

        let selectedCategory = this.selectedCategory;

        // TODO: reformat to anonymous function
        const cat = this.$store.getters.categories.find(function (category) { return category.name === selectedCategory; });

        this.$store.dispatch('addBook', {
          title: this.title,
          isbn: this.isbn,
          authorID: authorID,
          description: this.description,
          categoryID: cat.id
        }).then(() => {
          // TODO: Loading
          this.$router.push({ path: '/book' });
        }).catch((error) => {
          this.error = error.message;
        });
      }
    },
    computed: {
      filteredDataArray () {
        return this.$store.getters.authors.map((author) => {
          return { name: author.name + ' ' + author.surname };
        });
      },
      categories () {
        return this.$store.getters.categories.map((category) => {
          return { name: category.name };
        });
      },
      isbnStatus () {
        if (this.isbn !== '') {
          if (this.isbn.length !== 13) {
            return 'error';
          } else {
            return 'success';
          }
        }
      }
    }
  };
</script>

<style>
  .error {
    color: red;
  }
  .register-card {
    text-align: center;
  }

  .register-form {
    display: flex;
    justify-content: center;
    padding-top: 150px;
  }

  .register-button {
    margin-top: 3px;
    margin-bottom: -11px;
    width: 100%;
  }

  .at-input {
    padding: 3px;
  }

  .select {
    padding: 2px;
  }
</style>
