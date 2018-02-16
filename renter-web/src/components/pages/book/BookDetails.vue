<template>
  <section class="book-details">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <section class="menu">
      <div v-if="admin || employee">
        <at-button class="menu-button" type="info" size="large" icon="icon-edit" hollow @click="prompt">Edit</at-button>
        <at-button class="menu-button" type="primary" size="large" icon="icon-file-plus" hollow @click="isComponentModalActive = true">Add cover</at-button>
        <at-button class="menu-button" type="error" size="large" icon="icon-trash-2" hollow @click="deleteBook">Delete book</at-button>
        <at-button class="menu-button" type="default" size="large" icon="icon-info" hollow @click="showBookHistory">Show history</at-button>
      </div>
    </section>

    <b-modal :active.sync="isComponentModalActive" has-modal-card>
      <BookCoverModal v-bind="{ book: this.book }"></BookCoverModal>
    </b-modal>

    <div class="row at-row no-gutter flex-center asset-info">
      <div class="col-sd-12 col-md-12">
          <img :src="book.coverURL" alt="Book Cover">
      </div>

      <div class="col-md-12">
        <div class="book">
          <div class="book-title">
            {{ book.title }}
          </div>

          <div class="book-author">
            {{ book.author.name }} {{ book.author.surname }}
          </div>

          <div class="book-stars">
            Rate: <b>{{ bookRate }}</b>
            Your rate:
            <at-select @on-change="confirm" class="rate" style="width: 10px;" v-model="userRate" placeholder="Your score...">
              <at-option v-for="i in range(1,6)" :key="i" :value="i">{{ i }}</at-option>
            </at-select>
          </div>

          <div class="book-description">
            {{ book.description }}
          </div>

          <div v-if="this.authorBooks.length != 0" class="book-rent">
            <h1>Select return date</h1>
            <el-date-picker
              v-model="date"
              type="date"
              placeholder="Pick a return day">
            </el-date-picker>
            <div>
              <at-button class="rent-button" type="success" size="large" hollow @click="rentBook">Rent</at-button>
            </div>
          </div>
          <at-alert class="alert" v-else message="We are sorry but currently there is no book available to rent. Please check later" type="error"></at-alert>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
  import BookCoverModal from './misc/BookCoverModal';
  import _ from 'lodash';

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
        error: true,
        readonly: false,
        rate: 0,
        userRate: 0
      };
    },
    components: {
      BookCoverModal
    },
    computed: {
      admin () {
        return this.$store.getters.admin;
      },
      employee () {
        return this.$store.getters.employee;
      },
      bookRate () {
        console.log(this.$store.getters.rate);
        return this.$store.getters.rate;
      },
    },
    methods: {
      range (x, y) {
        return _.range(x, y);
      },
      open () {
        this.$alert('Please select correct date', 'Error', {
          confirmButtonText: 'OK',
          callback: action => {
            this.loading = false;
          }
        });
      },
      showBookHistory () {
        this.$router.push({ path: `/book/history/${this.book.isbn}` });
      },
      success () {
        this.$toast.open({
          message: `Have fun reading ${this.book.title}`,
          type: 'is-success'
        });
      },
      confirm (rate) {
        this.$dialog.confirm({
          message: 'Are you sure you want to rate this book?',
          onConfirm: () => {
            this.$toast.open('Book rated');
            this.$store.dispatch('rateBook', { isbn: this.book.isbn, rate: rate });
            this.readonly = true;
          }
        });
      },
      prompt () {
      },
      deleteBook () {
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
        if (diff <= 0) {
          this.open();
        } else {
          this.$store.dispatch('rentBook', {
            userID: userID,
            bookID: bookID,
            duration: diff,
            isbn: this.book.isbn
          }).then(() => {
            this.loading = false;
            this.success();
            this.$router.push({ path: '/profile' });
          }).catch((err) => {
            this.error = err;
            this.loading = false;
          });
        }
      }
    },
    created () {
      this.loading = true;
      this.$store.dispatch('fetchBookDetails', {
        bookID: this.$route.params.id
      }).then((response) => {
        this.book = response.data;
        this.rate = response.data.rate;

        this.$store.dispatch('getAvailableByISBN', {
          isbn: response.data.isbn
        }).then((response) => {
          this.authorBooks = response.data;
          this.loading = false;

          this.$store.dispatch('fetchBookRate', {
            isbn: this.book.isbn
          });
        });
      });
    },
    checkRate () {
      return this.book.rate;
    }
  };
</script>

<style scoped>
  img {
    max-width: 600px;
  }

  @media only screen and (min-width: 500px) {
    img {
      max-width: 200px;
    }
  }

  @media only screen and (min-width: 700px) {
    img {
      max-width: 500px;
    }
  }

  @media only screen and (min-width: 900px) {
    img {
      max-width: 500px;
    }
  }

  @media only screen and (min-width: 1100px) {
    img {
      max-width: 600px;
    }
  }

  .book-title {
    font-size: 4em;
    margin-bottom: -0.3em;
  }

  .book-author {
    font-size: 2em;
    color: gray;
  }

  .book-description {
    font-size: 1.2em;
    word-wrap: break-word;
    width: 60%;
    margin: 0 auto;
  }

  .book-rent {
    padding-top: 2em;
  }

  .alert {
    text-align: center;
    max-width: 200px;
    margin-left: 40%;
    margin-top: 10px;
  }

  .rent-datepicker {
    padding-bottom: 1em;
    width: 20%;
    margin: auto;
  }

  .rate {
    margin: 3px;
  }

  .rent-button {
    width: 24%;
    margin: 3px;
  }
</style>
