<template>
  <section>
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <at-input class="book-search-title" v-model="name" @input="searchUser" placeholder="Search for user"></at-input>

    <b-table
      :data="searchedBooks"
      :striped="true"
    >
    <template slot-scope="props">

      <b-table-column label="User">
        {{ props.row.user.name }} {{ props.row.user.surname }}
      </b-table-column>

      <b-table-column label="From">
          {{ new Date(props.row.from).toLocaleDateString("pl-PL") }}
      </b-table-column>

      <b-table-column label="To">
          {{ new Date(props.row.to).toLocaleDateString("pl-PL") }}
      </b-table-column>

    </template>
    </b-table>
  </section>
</template>

<script>
export default {
  data () {
    return {
      loading: false,
      books: [],
      name: ''
    };
  },
  props: ['isbn'],
  created () {
    this.loading = true;
    this.$store.dispatch('getBookRentHistory', {
      isbn: this.$route.params.isbn
    }).then((response) => {
      console.log(response);
      this.loading = false;
      this.books = response;
    });
  },
  computed: {
    searchUser () {
      this.name = this.name;
    },
    searchedBooks () {
      if (this.name !== '') {
        return this.books.filter((book) => {
          return (book.user.name.toUpperCase().includes(this.name.toUpperCase()) || book.user.surname.toUpperCase().includes(this.name.toUpperCase()));
        });
      } else {
        return this.books;
      }
    }
  }
};
</script>

<style lang="css">
</style>
