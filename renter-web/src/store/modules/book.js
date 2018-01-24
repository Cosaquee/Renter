import axios from 'axios';
import _ from 'lodash';
import config from '../../config';

import {
  RENTED_BOOKS,
  ALL_BOOKS,
  LATEST_BOOKS
} from '../mutation-types';

const state = {
  books: [],
  allRentedBooks: [],
  latestBooks: []
};

const getters = {
  allRentedBooks: state => state.allRentedBooks,
  books: state => state.books,
  latestBooks: state => state.latestBooks
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
  },
  fetchBookDetails (store, { bookID }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/${bookID}`, {
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
  getAvailableByISBN (store, { isbn }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/GetAvaiableByIsbn/${isbn}`, {
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
  addBook (store, { title, isbn, authorID, description, categoryID }) {
    return new Promise((resolve, reject) => {
      axios.post(config.API.BOOK, {
        title: title,
        isbn: isbn,
        authorId: authorID,
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
  confirmRent (store, { bookID }) {
    return new Promise((resolve, reject) => {
      axios.post(`${config.API.BOOK}/confirm/${bookID}`, {}, {
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
  confirmReturn (store, { bookID }) {
    return new Promise((resolve, reject) => {
      axios.post(`${config.API.BOOK}/confirm-return/${bookID}`, {}, {
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
  updateCover (store, { id, coverURL, resizedCoverURL }) {
    return new Promise((resolve, reject) => {
      axios.post(`${config.API.BOOK}/cover`, {
        id: id,
        CoverURL: coverURL,
        ResizedCoverURL: resizedCoverURL
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
  getLatestBooks (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/latest`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        console.log(response);
        store.commit(LATEST_BOOKS, { books: response.data });
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
  },
  [LATEST_BOOKS] (store, { books }) {
    state.latestBooks = books;
  }
};

export default {
  actions,
  state,
  getters,
  mutations
};
