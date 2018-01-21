import axios from 'axios';
import _ from 'lodash';
import config from '../../config';

import {
  RENTED_BOOKS,
  ALL_BOOKS
} from '../mutation-types';

const state = {
  books: [],
  allRentedBooks: []
};

const getters = {
  allRentedBooks: state => state.allRentedBooks,
  books: state => state.books
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
  },
  fetchBooks (store) {
    return new Promise((resolve, reject) => {
      axios.get(config.API.BOOK, {
        headers: {
          'Authorization': 'Bearer ' + store.getters.token
        }
      }).then((response) => {
        var tmpBooks = {};

        _.forEach(response.data, (book) => {
          if (tmpBooks[book.isbn]) {
            let oldValue = tmpBooks[book.isbn].copies;

            let b = {
              title: book.title,
              author: book.author,
              isbn: book.isbn,
              id: book.id,
              coverURL: book.coverURL,
              resaizedCoverURL: book.resizedCoverURL,
              category: book.category,
              copies: [oldValue].concat([book])
            };

            tmpBooks[book.isbn] = b;
          } else {
            tmpBooks[book.isbn] = {
              title: book.title,
              author: book.author,
              isbn: book.isbn,
              id: book.id,
              coverURL: book.coverURL,
              category: book.category,
              resaizedCoverURL: book.resizedCoverURL,
              copies: [book]
            };
          }
        });
        var bb = [];
        _.forEach(tmpBooks, (book) => {
          bb.push(book);
        });
        store.commit(ALL_BOOKS, { books: bb });
      });
    });
  }
};

const mutations = {
  [RENTED_BOOKS] (store, { books }) {
    state.allRentedBooks = books;
  },
  [ALL_BOOKS] (store, { books }) {
    state.books = books;
  }
};

export default {
  actions,
  state,
  getters,
  mutations
};
