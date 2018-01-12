<template>
  <section>
    <div class="columns .is-centered">
      <div class="column">
        <div v-if="admin || employee" class="book-add">
          <a @click="addBook" class="button is-primary is-outlined">New Book</a>
        </div>

        <table-component
             :data="cleanBooks"
             @rowClick="handleClick"
             sort-order="asc"
        >
             <table-column :hidden="hidden" show="id" label="ID"></table-column>
             <table-column show="title" label="Title"></table-column>

             <table-column label="Author">
               <template slot-scope="row">
                 {{ row.author.name }} {{ row.author.surname }}
               </template>
             </table-column>
         </table-component>
      </div>
    </div>
  </section>
</template>

<script>
  import axios from 'axios';
  import _ from 'lodash';
  export default {
    data () {
      return {
        books: [],
        hidden: true
      };
    },
    created: function () {
      axios.get('http://localhost:5000/api/book', {
        headers: {
          'Authorization': 'Bearer ' + this.$store.getters.token
        }
      }).then((response) => {
        this.books = response.data;
      });
    },
    methods: {
      handleClick (item) {
        this.$router.push({ path: '/book/' + item.data.id });
      },
      addBook (e) {
        this.$router.push({ path: '/book/create' });
      }
    },
    computed: {
      admin: function () {
        return this.$store.getters.admin;
      },
      employee: function () {
        return this.$store.getters.employee;
      },
      cleanBooks: function () {
        var books = {};
        _.forEach(this.books, (book) => {
          if (books[book.isbn]) {
            let oldValue = books[book.isbn].copies;
            let b = {
              title: book.title,
              author: book.author,
              isbn: books.isbn,
              id: book.id,
              copies: [oldValue].concat([book])
            };

            books[book.isbn] = b;
          } else {
            books[book.isbn] = {
              title: book.title,
              author: book.author,
              isbn: book.isbn,
              id: book.id,
              copies: [book]
            };
          }
        });

        var bb = [];
        _.forEach(books, (book) => {
          bb.push(book);
        });
        return bb;
      }
    }
  };
</script>
