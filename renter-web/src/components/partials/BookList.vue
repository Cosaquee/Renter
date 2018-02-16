<template>
  <section>
    <h3 class="title">Our latest books</h3>
    <div class="content">
      <book-card v-for="book in latestBooks" :key="book.id" :book="book"></book-card>
    </div>
    <h3 class="title">Our latest movies</h3>
    <div class="content">
      <movie-card v-for="movie in latestMovies" :key="movie.id" :movie="movie"></movie-card>
    </div>
  </section>
</template>

<script>
import { mapGetters } from 'vuex';
import BookCard from './BookCard';
import MovieCard from './MovieCard';
export default {
  data () {
    return {

    };
  },
  methods: {
    change (id) {
      this.$router.push({ path: '/book/' + id });
    }
  },
  created () {
    this.$store.dispatch('getLatestBooks');
    this.$store.dispatch('getLatestMovies');
  },
  components: {
    BookCard,
    MovieCard
  },
  computed: mapGetters(['latestBooks', 'latestMovies'])
};
</script>

<style scoped>
  .latest-books {
    background: hsla(0, 0%, 92%, 0.5);
  }

  .content {
    display: flex;
    justify-content: center;
    flex-flow: row wrap;
  }

  .book {
    margin: 10px;
  }

  .book:hover {
    transform: translateY(-0.5em);
    background: #EBEBEB;
  }

 .title {
   text-align: center;
   font-size: 3em;
 }
</style>
