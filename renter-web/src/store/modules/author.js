import axios from 'axios';
import config from '../../config';

import {
  AUTHOR_LIST
} from '../mutation-types';

const state = {
  authors: []
};

const getters = {
  authors: state => state.authors
};

const actions = {
  getAuthors (store) {
    return new Promise((resolve, reject) => {
      axios.get(config.API.AUTHORS, {
        headers: {
          'Authorization': 'Bearer ' + store.getters.token
        }
      }).then((response) => {
        store.commit(AUTHOR_LIST, { authors: response.data });
      });
    });
  }
};

const mutations = {
  [AUTHOR_LIST] (store, { authors }) {
    store.authors = authors;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
