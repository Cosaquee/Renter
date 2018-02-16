<template>
  <section class="movie-details">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
      <section class="menu">
        <div v-if="admin || employee">
          <at-button class="menu-button" type="info" size="large" icon="icon-edit" hollow @click="prompt">Edit</at-button>
          <at-button class="menu-button" type="primary" size="large" icon="icon-file-plus" hollow @click="isComponentModalActive = true">Add cover</at-button>
          <at-button class="menu-button" type="error" size="large" icon="icon-trash-2" hollow @click="deleteMovie">Delete movie</at-button>
          <at-button class="menu-button" type="default" size="large" icon="icon-info" hollow @click="showMovieHistory">Show history</at-button>
        </div>
      </section>

      <b-modal :active.sync="isComponentModalActive" has-modal-card>
        <MovieCoverModal v-bind="{ movie: this.movie }"></MovieCoverModal>
      </b-modal>

      <div class="columns asset-info">
        <div class="column">
          <img :src="movie.coverURL" alt="Movie cover">
        </div>

        <div class="column">
          <div class="movie-title">
            {{ movie.title }}
          </div>

          <!-- <div class="movie-stars">
            Rate: <b>{{ movieRent }}</b>
            Your rate:
            <at-select @on-change="confirm" class="rate" style="width: 10px;" v-model="userRate" placeholder="Your score...">
              <at-option v-for="i in range(1,6)" :key="i" :value="i">{{ i }}</at-option>
            </at-select>
          </div> -->

          <div class="movie-director">
            Directed by <b>{{ movie.director.name }} {{ movie.director.surname }}</b>
          </div>

          <div class="movie-duration">
              {{ movie.category.name }} {{ movie.duration }}
          </div>
          <div class="movie-description">
            {{ movie.description }}
          </div>

            <div v-if="!this.rented" class="movie-rent">
              <at-select class="" style="width: 150px;" v-model="quality" placeholder="Select quality">
                <at-option v-for="quality in qa" :key="quality.id" :value="quality.name">{{ quality.name }}</at-option>
              </at-select>
              <at-button class="menu-button" type="success" hollow @click="rentMovie">Rent</at-button>
            </div>
          </div>
      </div>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';
  import _ from 'lodash';

  import MovieCoverModal from './modals/MovieCoverModal';
  var randomUrl = require('random-uri');

  export default {
    data () {
      return {
        movie: {},
        loading: true,
        isComponentModalActive: false,
        quality: '',
        qa: [
          { name: 'SD', id: 1 },
          { name: 'HDReady', id: 2 },
          { name: 'FullHD', id: 3 },
          { name: '4K', id: 4 }
        ],
        rented: false
      };
    },
    components: {
      MovieCoverModal
    },
    methods: {
      range (x, y) {
        return _.range(x, y);
      },
      confirm (rate) {
        this.$dialog.confirm({
          message: 'Are you sure you want to rate this book?',
          onConfirm: () => {
            this.$toast.open('Book rated');
            this.$store.dispatch('rateBook', { isbn: this.book.isbn, rate: rate });
            this.readonly = true;
          }
        });
      },
      modalAlert () {
        this.$dialog.alert({
          message: 'Select movie quality before renting!',
          onConfirm: () => {
          }
        });
      },
      success () {
        let link = randomUrl();
        this.$dialog.alert({
          message: `Your movie link <a>${link}</a>`,
          onConfirm: () => {
          }
        });
      },
      deleteMovie () {
        this.loading = true;
        this.$store.dispatch('deleteMovie', { movieID: this.movie.id })
          .then(() => {
            this.loading = false;
            this.$router.push({ path: '/movie' });
          }).catch(() => {
            this.error = true;
            this.openError();
          });
      },
      prompt () {

      },
      openError () {
        this.$alert('Something went wrong, please try again.', 'Error', {
          confirmButtonText: 'OK',
          callback: action => {
            this.loading = false;
          }
        });
      },
      showMovieHistory () {
        this.$router.push({ path: `/movie/history/${this.movie.id}` });
      },
      rentMovie () {
        this.loading = true;
        if (!this.quality) {
          this.modalAlert();
          this.loading = false;
        } else {
          const userID = this.$store.getters.user.id;
          const movieID = this.movie.id;
          const quality = this.quality;
          this.$store.dispatch('rentMovie', {
            userID: userID,
            movieID: movieID,
            quality: quality
          }).then(() => {
            this.loading = false;
            this.success();
          }).catch((error) => {
            console.log(error);
            this.loading = false;
            this.modalError();
          });
        }
      }
    },
    created: function () {
      this.loading = true;
      this.$store.dispatch('fetchMovie', {
        movieID: this.$route.params.id
      }).then((response) => {
        this.$store.dispatch('checkIfRented', {
          movieID: this.$route.params.id,
          userID: this.$store.getters.user.id
        }).then((response) => {
          this.rented = response;
        });
        this.loading = false;
        this.movie = response.data;
      });
    },
    computed: mapGetters(['admin', 'employee']),
  };
</script>

<style scoped>
  .movie-title {
    font-size: 4em;
  }

  .movie-director {
    padding: 1em;
  }

  .movie-description {
    font-size: 1.5em;
    padding: 1em;
  }

  .movie-rent {
    text-align: center;
  }

  img {
    width: 70%;
  }

  .rate {
    margin: 3px;
  }
</style>
