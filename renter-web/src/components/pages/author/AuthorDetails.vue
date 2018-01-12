<template>
  <section class="author-details">
    <b-tabs position="is-centered" expanded >
      <b-tab-item label="Info" >
        <div class="columns">
            <div class="column">
              <div class="author-profile">
                <img src="https://tinyfac.es/data/avatars/E0B4CAB3-F491-4322-BEF2-208B46748D4A-1000w.jpeg" alt="">
              </div>
            </div>
            <div class="column hcenter">
              <div class="author-info">
                  <div class="author-info-name">
                    <h1>{{ author.name }} {{ author.surname}}</h1>
                  </div>

                  <div class="author-books">
                    {{ author.description }}
                  </div>
              </div>
            </div>
        </div>
      </b-tab-item>

      <b-tab-item label="Books">
        <table-component
             :data="books"
             sort-by="name"
             @rowClick="handleClick"
             sort-order="asc"
        >
             <table-column :hidden='hidden' show="id" label="ID"></table-column>
             <table-column show="title" label="Title"></table-column>
             <table-column label="Copies">
               <template slot-scope="row">
                 {{ row.copies.length }}
               </template>
             </table-column>
             <table-column label="Available">
               <template slot-scope="row">
                 {{ row.rented }}
               </template>
             </table-column>
         </table-component>
      </b-tab-item>
    </b-tabs>
  </section>
</template>

<script>
  import axios from 'axios';
  import _ from 'lodash';

  export default {
    data () {
      return {
        author: {},
        books: [],
        hidden: true
      };
    },
    created: function () {
      axios.get('http://localhost:5000/api/author/' + this.$route.params.id, {
        headers: {
          'Authorization': 'Bearer ' + this.$store.getters.token
        }
      }).then((response) => {
        this.author = response.data;

        var books = {};

        _.forEach(response.data.books, (book) => {
          if (books[book.isbn]) {
            let oB = books[book.isbn].copies;
            let b = {
              title: book.title,
              id: book.id,
              copies: [oB].concat([book])
            };
            books[book.isbn] = b;
          } else {
            let b = {
              title: book.title,
              copies: [book],
              id: book.id
            };
            books[book.isbn] = b;
          };
        });

        var bb = [];

        _.forEach(books, (book) => {
          _.forEach(book.copies, (bookCopy) => {
            if (bookCopy.rented) {
              book.rented = book.copies.length - 1;
            } else {
              book.rented = book.copies.length;
            }
          });
          bb.push(book);
        });

        this.books = bb;
      });
    },
    methods: {
      handleClick (item) {
        this.$router.push({ path: '/book/' + item.data.id });
      }
    }
  };
</script>
