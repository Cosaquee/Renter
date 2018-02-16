import axios from 'axios';
import config from '../../config';

import {
  ALL_DIRECTORS
} from '../mutation-types';

const state = {
  directors: []
};

const getters = {
  directors: state => state.directors
};

const actions = {
  fetchAllDirectors (store) {
    axios.get(`${config.API.DIRECTOR}`, {
      headers: {
        'Authorization': `Bearer ${store.getters.token}`
      }
    }).then((response) => {
      store.commit(ALL_DIRECTORS, { directors: response.data });
    });
  },
  fetchDirectorMovies (store, { directorID }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.DIRECTOR}/movies/${directorID}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        resolve(response.data);
      });
    });
  }
};

const mutations = {
  [ALL_DIRECTORS] (store, { directors }) {
    store.directors = directors;
  }
};

export default {
  actions,
  state,
  getters,
  mutations
};
