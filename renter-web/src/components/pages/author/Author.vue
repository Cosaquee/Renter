<template>
  <section>
    <div class="columns .is-centered">
        <div class="column">

          <div v-if="admin || employee" class="author-add">
            <a @click="addAuthor" class="button is-primary is-outlined">New Author</a>
          </div>
          <table-component
               :data="computedAuthors"
               sort-by="name"
               @rowClick="handleClick"
               sort-order="asc"
          >
               <table-column :hidden="hidden" show="id" label="ID"></table-column>
               <table-column show="name" label="First name"></table-column>
               <table-column show="surname" label="Surname"></table-column>
               <table-column show="books" label="Books"></table-column>
           </table-component>
         </div>
    </div>
   </section>
</template>

<script>
import _ from 'lodash';
export default {
  data () {
    return {
      hidden: true
    };
  },
  created: function () {
    this.$store.dispatch('getAuthors');
  },
  methods: {
    handleClick (item) {
      this.$router.push({ path: '/author/' + item.data.id });
    },
    addAuthor (event) {
      this.$router.push({ name: 'AuthorForm' });
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
  },

};
</script>
