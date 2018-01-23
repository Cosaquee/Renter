<template>
  <section class="movie-details">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
      <section class="movie-menu">
        <div v-if="admin || employee" class="menu">
          <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">Add cover</button>
          <button class="button is-outlined is-medium is-info" @click="prompt">Edit</button>
          <a class="button is-danger is-outlined is-medium" @click="deleteMovie">Delete</a>
        </div>
      </section>

      <b-modal :active.sync="isComponentModalActive" has-modal-card>
        <MovieCoverModal v-bind="{ movie: this.movie }"></MovieCoverModal>
      </b-modal>

      <div class="columns movie-info">
        <div class="column">
          <img :src="movie.coverURL" alt="Movie cover">
        </div>

        <div class="column">
          <div class="movie-title">
            {{ movie.title }}
          </div>

          <div class="movie-stars">
            <Rate :length="5"></Rate>
          </div>

          <div class="movie-director">
            Directed by <b>{{ movie.director.name }} {{ movie.director.surname }}</b>
          </div>

          <div class="movie-duration">
              {{ movie.category.name }} {{ movie.duration }}
          </div>
          <div class="movie-description">
            {{ movie.description }}
          </div>

            <div class="movie-rent">
              <a  class="button is-success is-outlined is-large rent-button">Rent</a>
            </div>
          </div>
      </div>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';
  import MovieCoverModal from './modals/MovieCoverModal';

  export default {
    data () {
      return {
        movie: {},
        loading: true,
        isComponentModalActive: false
      };
    },
    components: {
      MovieCoverModal
    },
    methods: {
      deleteMovie () {

      },
      prompt () {

      },
    },
    created: function () {
      this.loading = true;
      this.$store.dispatch('fetchMovie', {
        movieID: this.$route.params.id
      }).then((response) => {
        console.log(response.data);
        this.loading = false;
        this.movie = response.data;
      });
    },
    computed: mapGetters(['admin', 'employee']),
  };
</script>

<style scoped>
  .movie-info {
    padding: 2em;
  }
  .movie-title {
    text-align: center;
    font-size: 4em;
  }

  .movie-stars {
    text-align: center;
  }

  .movie-director {
    text-align: center;
    padding: 1em;
  }

  .movie-duration {
    text-align: center;
  }

  .movie-description {
    text-align: center;
    font-size: 1.5em;
    padding: 1em;
  }

  .movie-rent {
    text-align: center;
  }
</style>
