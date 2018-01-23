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
  fetchMovie (store, { movieID }) {
    return new Promise((resolve, reject) => {
      console.log(`${config.API.MOVIE}/${movieID}`);
      axios.get(`${config.API.MOVIE}/${movieID}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        resolve(response);
      }).catch((error) => {
        reject(error);
      });
    });
  },
  uploadCover (store, { movieID, coverURL }) {
    return new Promise((resolve, reject) => {
      axios.post(`${config.API.MOVIE}/cover`, {
        movieID: movieID,
        coverURL: coverURL
      }, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then(() => {
        resolve();
      });
    });
  }
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
