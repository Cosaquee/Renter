import Vue from 'vue';
import Vuex from 'vuex';

import user from './modules/user';
import author from './modules/author';
import category from './modules/category';

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    user,
    author,
    category
  },
});
