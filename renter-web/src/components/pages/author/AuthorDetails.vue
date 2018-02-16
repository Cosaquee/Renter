<template>
  <section class="author-details">
    <b-tabs position="is-centered" expanded>
      <b-tab-item label="Info" >
        <div class="columns">
            <div class="column">
              <div class="author-profile">
                <img src="https://tinyfac.es/data/avatars/E0B4CAB3-F491-4322-BEF2-208B46748D4A-1000w.jpeg" alt="">
              </div>
            </div>
            <div class="column">
              <div class="asset-info">
                  <div class="name">
                    <h1>{{ author.name }} {{ author.surname}}</h1>
                  </div>

                  <div>
                    <p class="description">{{ author.description }}</p>
                  </div>
              </div>
            </div>
        </div>
      </b-tab-item>

      <b-tab-item label="Books">
        <b-table
          :data="books"
          @click="handleClick"
          :striped="true"
        >
          <template slot-scope="props">
              <b-table-column label="Title">
                {{ props.row.title }}
              </b-table-column>

              <b-table-column label="Copies">
                {{ props.row.copies.length }}
              </b-table-column>

              <b-table-column label="Available">
                {{ props.row.rented }}
              </b-table-column>
             </template>
        </b-table>
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
        this.$router.push({ path: '/book/' + item.id });
      }
    }
  };
</script>
<style scoped>
  h1 {
    font-size: 3em;
  }

  .description {
    font-size: 1.3em;
    text-align: center;
    padding: 1em;
  }

  img {
    width: 70%;
  }
</style>
