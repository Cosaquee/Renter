import axios from 'axios';
import config from '../../config';

import {
  MOVIE_LIST,
  LATEST_MOVIES,
  RENTED_MOVIES_HISTORY,
  RENTED_MOVIES
} from '../mutation-types';

const state = {
  movies: [],
  rentedMovies: [],
  rentedMoviesHistory: [],
  latestMovies: []
};

const getters = {
  movies: state => state.movies,
  latestMovies: state => state.latestMovies,
  rentedMovies: state => state.rentedMovies,
  rentedMoviesHistory: state => state.rentedMoviesHistory
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
  },
  getLatestMovies (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.MOVIE}/latest`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(LATEST_MOVIES, { movies: response.data });
      });
    });
  },
  fetchRentedMovies (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.MOVIE}/rent/current/${store.getters.user.id}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(RENTED_MOVIES, { movies: response.data });
      });
    });
  },
  fetchRentedMoviesHistory (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.MOVIE}/history/user/${store.getters.user.id}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(RENTED_MOVIES_HISTORY, { movies: response.data });
      });
    });
  },
  fetchMovieHistory (store, { movieID }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.MOVIE}/history/movie/${movieID}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        resolve(response.data);
      });
    });
  },
  rentMovie (store, { userID, movieID, quality }) {
    return new Promise((resolve, reject) => {
      axios.post(`${config.API.MOVIE}/rent/${movieID}/${userID}/${quality}`, {

      }, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then(() => {
        resolve();
      }).catch(() => {
        reject();
      });
    });
  },
  checkIfRented (store, { movieID, userID }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.MOVIE}/rented/${movieID}/${userID}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        resolve(response.data);
      });
    });
  },
  fetchAllRents (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.MOVIE}/rent`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(RENTED_MOVIES, { movies: response.data });
      });
    });
  },
  addMovie (store, { title, directorID, description, categoryID, duration }) {
    return new Promise((resolve, reject) => {
      axios.post(config.API.MOVIE, {
        title: title,
        duration: duration,
        directorId: directorID,
        description: description,
        categoryId: categoryID
      }, {
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
  deleteMovie (store, { movieID }) {
    return new Promise((resolve, reject) => {
      axios.delete(config.API.MOVIE + `/${movieID}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then(() => {
        resolve();
      }).catch(() => {
        reject();
      });
    });
  },
};

const mutations = {
  [MOVIE_LIST] (store, { movies }) {
    store.movies = movies;
  },
  [LATEST_MOVIES] (store, { movies }) {
    store.latestMovies = movies;
  },
  [RENTED_MOVIES] (store, { movies }) {
    store.rentedMovies = movies;
  },
  [RENTED_MOVIES_HISTORY] (store, { movies }) {
    store.rentedMoviesHistory = movies;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
