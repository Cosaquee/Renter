<template>
  <section>
    <div class="search-menu">
      <div>
        <at-input class="search-menu-left-input" v-model="name" @input="searchAuthor" placeholder="Search for author"></at-input>
      </div>
      <div>
        <at-button @click="addAuthor" type="info" hollow>Add</at-button>
      </div>
    </div>
    <div>
      <b-table
           :data="searchBox"
           @click="handleClick"
           :striped="true"
      >
        <template slot-scope="props">
          <b-table-column label="Name">
            {{ props.row.name }}
          </b-table-column>

          <b-table-column label="Surname">
            {{ props.row.surname }}
          </b-table-column>

          <b-table-column label="Books">
            {{ props.row.books.length }}
          </b-table-column>
         </template>
       </b-table>
    </div>
 </section>
</template>

<script>
import _ from 'lodash';
export default {
  data () {
    return {
      name: ''
    };
  },
  created: function () {
    this.$store.dispatch('getAuthors');
  },
  methods: {
    handleClick (item) {
      this.$router.push({ path: '/author/' + item.id });
    },
    addAuthor (event) {
      this.$router.push({ name: 'AuthorForm' });
    },
    searchAuthor () {
      this.name = this.name;
    }
  },
  computed: {
    admin: function () {
      return this.$store.getters.admin;
    },
    employee: function () {
      return this.$store.getters.employee;
    },
    computedAuthors: function () {
      var authors = [];

      _.forEach(this.$store.state.author.authors, (author) => {
        let a = {
          name: author.name,
          surname: author.surname,
          books: author.books.length,
          id: author.id
        };

        authors.push(a);
      });

      return authors;
    },
    searchBox () {
      if (this.name !== '') {
        return this.$store.getters.authors.filter((author) => {
          return author.name.toUpperCase().includes(this.name.toUpperCase()) || author.surname.toUpperCase().includes(this.name.toUpperCase());
        });
      }
      return this.$store.getters.authors;
    }
  }
};
</script>

<style scoped>
  .search-menu {
    display: flex;
    justify-content: space-between;
  }
</style>
