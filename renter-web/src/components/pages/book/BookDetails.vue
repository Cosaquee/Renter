<template>
  <section class="book-details">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <section class="book-menu">

        <div class="columns .is-centered">
          <div class="column">
            <div v-if="admin || employee" class="book-menu">
              <button class="button is-outlined is-medium is-info" @click="prompt">Edit</button>
              <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">Add cover</button>
              <a class="button is-danger is-outlined is-medium" @click="deleteBook">Delete</a>
            </div>
          </div>
        </div>

        <b-modal :active.sync="isComponentModalActive" has-modal-card>
          <BookCoverModal v-bind="{ book: this.book }"></BookCoverModal>
        </b-modal>

    </section>

    <div class="columns">

      <div class="column">
        <div>
          <img class="book-image" :src="book.coverURL" alt="Book Cover">
        </div>
      </div>

      <div class="column">
        <div class="">
          <div>
            <h2 class="book-title">{{ book.title }}</h2>
            <p class="book-description">{{ book.description }}</p>
            <br>
          </div>

          <div style="text-align: center" v-if="this.authorBooks.length != 0">

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
    </div>
  </section>
</template>

<script>
  import axios from 'axios';
  import BookCoverModal from './modals/BookCoverModal';
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
      axios.get('http://localhost:5000/api/book/' + this.$route.params.id, {
        headers: {
          'Authorization': 'Bearer ' + this.$store.getters.token
        }
      }).then((response) => {
        this.book = response.data;
        axios.get('http://localhost:5000/api/book/GetAvaiableByIsbn/' + response.data.isbn, {
          headers: {
            'Authorization': 'Bearer ' + this.$store.getters.token
          }
        }).then((response) => {
          this.authorBooks = response.data;
        });
      });
    }
  };
</script>
