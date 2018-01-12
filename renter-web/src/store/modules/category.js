import axios from 'axios';
import config from '../../config';

import {
  CATEGORY_LIST
} from '../mutation-types';

const state = {
  categories: []
};

const getters = {
  categories: state => state.categories
};

const actions = {
  getCategories (store) {
    axios.get(config.API.CATEGORIES, {
      headers: {
        'Authorization': 'Bearer ' + store.getters.token
      }
    }).then((response) => {
      store.commit('CATEGORY_LIST', { categories: response.data });
    });
  }
};

const mutations = {
  [CATEGORY_LIST] (store, { categories }) {
    state.categories = categories;
  }
};

export default {
  state,
  getters,
  mutations,
  actions
};
