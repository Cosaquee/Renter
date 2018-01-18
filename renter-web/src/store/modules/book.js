import axios from 'axios';
import config from '../../config';

import {
  RENTED_BOOKS
} from '../mutation-types';

const state = {
  allRentedBooks: []
};

const getters = {
  allRentedBooks: state => state.allRentedBooks
};

const actions = {
  deleteBook (store, { bookID }) {
    return new Promise((resolve, reject) => {
      axios.delete(config.API.BOOK + `/${bookID}`, {
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
  rentBook (store, { userID, bookID, duration }) {
    return new Promise((resolve, reject) => {
      axios.post(config.API.BOOK + `/rent`, {
        bookID: bookID,
        userID: userID,
        RentDuration: duration
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
  getAllRentedBooks (store) {
    return new Promise((resolve, reject) => {
      axios.get(config.API.BOOK + '/rent', {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(RENTED_BOOKS, { books: response.data });
      });
    });
  }
};

const mutations = {
  [RENTED_BOOKS] (store, { books }) {
    state.allRentedBooks = books;
  }
};

export default {
  actions,
  state,
  getters,
  mutations
};
