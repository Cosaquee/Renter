import Vue from 'vue';
import Vuex from 'vuex';

import user from './modules/user';
import author from './modules/author';
import category from './modules/category';
import book from './modules/book';
import movie from './modules/movie';

Vue.use(Vuex);

export default new Vuex.Store({
  strict: true,
  modules: {
    user,
    author,
    category,
    book,
    movie
  },
});
