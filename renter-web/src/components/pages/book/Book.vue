<template>
  <section>
    <section class="book-search">
      <div class="book-search-inputs">
        <at-input class="book-search-title" v-model="name" @input="searchBook" placeholder="Search for book or title"></at-input>
        <at-select style="width: 150px;" class="book-search-select" size="large" v-model="selectedCategory" placeholder="Select category">
          <at-option v-for="category in categories" :value="category.name">{{ category.name }}</at-option>
        </at-select>
        <at-switch class="book-search-switch" @change="switchSelected" v-model="isSwitched">
          <span slot="checkedText">Show only available books</span>
          <span slot="unCheckedText">Show all books</span>
        </at-switch>
        <at-button size="small" class="clearButton" @click="clearFilter" icon="icon-x"></at-button>
      </div>
      <at-button @click="addBook" type="info" hollow>Add</at-button>
    </section>

    <b-tabs position="is-centered" expanded>
      <b-tab-item label="Covers" class="covers">
        <div class="wrapper">
          <div class="cards">
            <card v-for="collection in selectBooks" :key="collection.resaizedCoverURL" :collection="collection"></card>
          </div>
        </div>
      </b-tab-item>
      <b-tab-item label="List">
        <div class="columns .is-centered">
          <div class="column">
            <b-table
              :data="selectBooks"
              @click="handleClick"
              :striped="true"
            >
              <template slot-scope="props">
                  <b-table-column label="Title">
                    {{ props.row.title }}
                  </b-table-column>

                  <b-table-column label="Author">
                    {{ props.row.author.name }} {{ props.row.author.surname }}
                  </b-table-column>

                  <b-table-column label="Available">
                    <b-tag :type="checkAvailable(props.row) ? 'is-success' : 'is-danger'" rounded>{{ checkAvailable(props.row) ? 'Available' : 'Not available' }}</b-tag>
                  </b-table-column>
                 </template>
            </b-table>
          </div>
        </div>
      </b-tab-item>
    </b-tabs>
  </section>
</template>

<script>
  import Card from './misc/CoverCard';
  export default {
    data () {
      return {
        selectedCategory: '',
        name: '',
        isSwitched: false
      };
    },
    components: {
      Card
    },
    created () {
      this.$store.dispatch('fetchBooksWithRatings');
      this.$store.dispatch('fetchBooks');
      this.$store.dispatch('getCategories');
    },
    methods: {
      handleClick (item) {
        this.$router.push({ path: '/book/' + item.id });
      },
      addBook () {
        this.$router.push({
          path: '/book/create'
        });
      },
      selectBook () {
        this.selectedCategory = this.selectedCategory;
        this.name = this.name;
      },
      searchBook () {
        this.name = this.name;
      },
      switchSelected () {
        this.isSwitched = !this.isSwitched;
      },
      checkAvailable (row) {
        return row.copies.some((book) => {
          return !book.rented === true;
        });
      },
      checkBookAvailable (book) {
        return book.copies.some((book) => {
          return !book.rented === true;
        });
      },
      clearFilter () {
        this.selectedCategory = '';
        this.name = '';
        this.isSwitched = !this.isSwitched;
      },
    },
    computed: {
      user () {
        return this.$store.getters.user;
      },
      admin () {
        return this.$store.getters.admin;
      },
      employee () {
        return this.$store.getters.employee;
      },
      categories () {
        return this.$store.getters.categories;
      },
      books () {
        return this.$store.getters.books;
      },
      selectBooks () {
        if (!this.isSwitched) {
          if (this.selectedCategory && this.name !== '') {
            return this.$store.getters.books.filter((book) => {
              return book.category.name === this.selectedCategory && this.checkBookAvailable(book) && (book.title.toUpperCase().includes(this.name.toUpperCase()) || book.author.name.toUpperCase().includes(this.name.toUpperCase()) || book.author.surname.toUpperCase().includes(this.name
                .toUpperCase()));
            });
          } else if (this.selectedCategory) {
            return this.$store.getters.books.filter((book) => {
              return book.category.name === this.selectedCategory;
            });
          } else if (this.name !== '') {
            return this.$store.getters.books.filter((book) => {
              return this.checkBookAvailable(book) && (book.title.toUpperCase().includes(this.name.toUpperCase()) || book.author.name.toUpperCase().includes(this.name.toUpperCase()) || book.author.surname.toUpperCase().includes(this.name
                .toUpperCase()));
            });
          } else if (this.name === '' && !this.selectedCategory) {
            return this.$store.getters.books.filter((book) => {
              return this.checkBookAvailable(book);
            });
          }
        } else if (this.isSwitched) {
          if (this.selectedCategory && this.name !== '') {
            return this.$store.getters.books.filter((book) => {
              return this.checkBookAvailable(book) && (book.category.name === this.selectedCategory && (book.title.toUpperCase().includes(this.name.toUpperCase()) || book.author.name.toUpperCase().includes(this.name.toUpperCase()) || book.author.surname.toUpperCase().includes(this.name
                .toUpperCase())));
            });
          } else if (this.selectedCategory) {
            return this.$store.getters.books.filter((book) => {
              return book.category.name === this.selectedCategory;
            });
          } else if (this.name !== '') {
            return this.$store.getters.books.filter((book) => {
              return (book.title.toUpperCase().includes(this.name.toUpperCase()) || book.author.name.toUpperCase().includes(this.name.toUpperCase()) || book.author.surname.toUpperCase().includes(this.name
                .toUpperCase()));
            });
          }
        }
        return this.$store.getters.books;
      }
    }
  };
</script>

<style scoped>
  .book-search {
    display: flex;
    flex-flow: row wrap;
    justify-content: space-between;
  }

  .book-search-inputs {
    display: flex;
    justify-content: space-between;
  }

  .book-search-title {
    margin: 3px;
  }

  .book-search-select {
    margin: 4px;
  }

  .book-search-switch {
    margin: 8px;
  }
</style>
