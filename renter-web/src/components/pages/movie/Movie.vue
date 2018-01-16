<template>
  <section>
    <table-component
      :data="computedAuthors"
      sort-by="title"
      @rowClick="handleClick"
      sort-order="asc">

      <table-column show="title" label="Title"></table-column>
      <table-column show="category.name" label="Category"></table-column>
      <table-column show="description" label="Description"></table-column>
        <table-column label="Director">
          <template slot-scope="row">
            {{ row.director.name }} {{ row.director.surname }}
          </template>
        </table-column>
      <table-column show="duration" label="Duration"></table-column>
    </table-component>
  </section>
</template>

<script>
  export default {
    data () {
      return {
        movies: [],
      };
    },
    created: function () {
      this.$store.dispatch('getMovies');
    },
    methods: {
      handleClick (item) {
        this.$router.push({ path: '/movie/' + item.data.id });
      },
    },
    computed: {
      computedAuthors () {
        return this.$store.getters.movies;
      }
    }
  };
</script>
