<template>
  <div class="latest-books">
    <BookList></BookList>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex';
  import BookList from '../partials/BookList';
  export default {
    components: {
      BookList
    },
    computed: mapGetters(['user']),
  };
</script>

<style media="screen">
</style>
