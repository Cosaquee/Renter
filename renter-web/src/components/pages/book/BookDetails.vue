<template>
  <section class="book-details">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <section class="book-menu">
      <div v-if="admin || employee" class="menu">
        <button class="button is-outlined is-medium is-info" @click="prompt">Edit</button>
        <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">Add cover</button>
        <a class="button is-danger is-outlined is-medium" @click="deleteBook">Delete</a>
      </div>
    </section>

    <b-modal :active.sync="isComponentModalActive" has-modal-card>
      <BookCoverModal v-bind="{ book: this.book }"></BookCoverModal>
    </b-modal>

    <div class="columns book-info">
      <div class="column">
          <img :src="book.coverURL" alt="Book Cover">
      </div>

      <div class="column">
          <div class="book-title">
            {{ book.title }}
          </div>

          <div class="book-author">
            {{ book.author.name }} {{ book.author.surname }}
          </div>

          <div class="book-stars">
            <Rate :length="5"></Rate>
          </div>

          <div class="book-description">
            {{ book.description }}
          </div>

          <div v-if="this.authorBooks.length != 0" class="book-rent">
            <div class="rent-datepicker">
                <h1 class="rent-title">Select return date</h1>
                <b-datepicker v-model="date"></b-datepicker>
            </div>
            <div>
              <a  class="button is-success is-outlined is-large rent-button" @click="rentBook">Rent</a>
            </div>
          </div>
      </div>
    </div>
  </section>
</template>

<script>
  import BookCoverModal from './misc/BookCoverModal';
  var moment = require('moment');

  export default {
    data () {
      return {
        hidden: true,
        book: {},
        authorBooks: [],
        isComponentModalActive: false,
        date: new Date(),
        loading: false,
        error: true
      };
    },
    components: {
      BookCoverModal
    },
    computed: {
      rate: function () {
        return this.book.bookRatings[0].rate / 2;
      },
      admin: function () {
        return this.$store.getters.admin;
      },
      employee: function () {
        return this.$store.getters.employee;
      }
    },
    methods: {
      success () {
        this.$toast.open({
          message: `Have fun reading ${this.book.title}`,
          type: 'is-success'
        });
      },
      prompt: function () {
      },
      deleteBook: function () {
        this.deleteing = true;
        this.$store.dispatch('deleteBook', { bookID: this.$route.params.id })
          .then(() => {
            this.loading = false;
            this.$router.push({ path: '/book' });
          }).catch(err => {
            this.loading = false;
            this.error = err;
          });
      },
      rentBook () {
        this.loading = true;
        let userID = this.$store.getters.user.id;
        let bookID = this.authorBooks[0].id;
        let momentDate = moment.utc(this.date);
        let now = moment.utc();
        let diff = momentDate.diff(now, 'days');

        this.$store.dispatch('rentBook', {
          userID: userID,
          bookID: bookID,
          duration: diff
        }).then(() => {
          this.loading = false;
          this.success();
          this.$router.push({ path: '/profile' });
        }).catch((err) => {
          this.error = err;
          this.loading = false;
        });
      }
    },
    created: function () {
      this.loading = true;
      this.$store.dispatch('fetchBookDetails', {
        bookID: this.$route.params.id
      }).then((response) => {
        this.book = response.data;
        this.$store.dispatch('getAvailableByISBN', {
          isbn: response.data.isbn
        }).then((response) => {
          this.loading = false;
          this.authorBooks = response.data;
        });
      });
    }
  };
</script>

<style scoped>
  .book-info {
    padding: 2em;
  }

  .book-title {
    text-align: center;
    font-size: 4em;
    margin-bottom: -0.3em;
  }

  .book-author {
    font-size: 2em;
    color: gray;
  }

  .book-stars {
    padding: 1em;
  }

  .book-description {
    text-align: center;
    font-size: 1.2em;
  }

  .book-rent {
    text-align: center;
    padding-top: 2em;
  }
</style>
