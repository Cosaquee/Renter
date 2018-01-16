import axios from 'axios';
import config from '../../config';

import {
  MOVIE_LIST
} from '../mutation-types';

const state = {
  movies: []
};

const getters = {
  movies: state => state.movies
};

const actions = {
  getMovies (store) {
    return new Promise((resolve, reject) => {
      axios.get(config.API.MOVIE, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(MOVIE_LIST, { movies: response.data });
      });
    });
  },
};

const mutations = {
  [MOVIE_LIST] (store, { movies }) {
    store.movies = movies;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
