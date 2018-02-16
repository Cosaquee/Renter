import axios from 'axios';
import _ from 'lodash';
import config from '../../config';

import {
  RENTED_BOOKS,
  ALL_BOOKS,
  LATEST_BOOKS,
  USER_RENT_HISTORY,
  BOOK_RATE
} from '../mutation-types';

const state = {
  books: [],
  allRentedBooks: [],
  latestBooks: [],
  rate: 0
};

const getters = {
  allRentedBooks: state => state.allRentedBooks,
  books: state => state.books,
  latestBooks: state => state.latestBooks,
  rate: state => state.rate
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
  rentBook (store, { userID, bookID, duration, isbn }) {
    return new Promise((resolve, reject) => {
      axios.post(config.API.BOOK + `/rent`, {
        bookID: bookID,
        userID: userID,
        RentDuration: duration,
        ISBN: isbn
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
            console.log(book.state);
            let b = {
              title: book.title,
              author: book.author,
              isbn: book.isbn,
              id: book.id,
              coverURL: book.coverURL,
              resizedCoverURL: book.resizedCoverURL,
              category: book.category,
              rate: book.bookRatings,
              copies: [oldValue].concat([book])
            };

            tmpBooks[book.isbn] = b;
          } else {
            console.log(book.state);
            tmpBooks[book.isbn] = {
              title: book.title,
              author: book.author,
              isbn: book.isbn,
              id: book.id,
              coverURL: book.coverURL,
              category: book.category,
              rate: book.bookRatings,
              resizedCoverURL: book.resizedCoverURL,
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
        store.commit(LATEST_BOOKS, { books: response.data });
      });
    });
  },
  rateBook (store, { isbn, rate }) {
    return new Promise((resolve, reject) => {
      axios.post(`${config.API.BOOK}/rate`, {
        ISBN: isbn,
        UserID: store.getters.user.id,
        Rate: rate
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
  fetchBooksWithRatings (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/stars`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        var allBooks = store.getters.books;
        var books = [];
        allBooks.map((book) => {
          var ratings = [];
          response.data.map((bookRating) => {
            if (book.isbn === bookRating.isbn) {
              ratings.push(bookRating.rate);
            }
          });
          let rate = ratings.reduce((a, b) => a + b, 0);
          let meanRate = rate / ratings.length;
          let bok = {
            title: book.title,
            author: book.author,
            rate: meanRate,
            isbn: book.isbn,
            id: book.id,
            coverURL: book.coverURL,
            copies: book.copies,
            category: book.category,
            resizedCoverURL: book.resaizedCoverURL
          };
          books.push(bok);
        });
        store.commit('ALL_BOOKS', { books: books });
      });
    });
  },
  getBookRentHistory (store, { isbn }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/history/${isbn}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        resolve(response.data);
      }).catch((error) => {
        reject(error);
      });
    });
  },
  fetchBookRate (store, { isbn }) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/rate/${isbn}`, {
        headers: {
          'Authorization': `Bearer ${store.getters.token}`
        }
      }).then((response) => {
        store.commit(BOOK_RATE, { rate: response.data });
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
  [USER_RENT_HISTORY] (store, { books }) {
    state.rentHistory = books;
  },
  [LATEST_BOOKS] (store, { books }) {
    state.latestBooks = books;
  },
  [BOOK_RATE] (store, { rate }) {
    state.rate = rate;
  }
};

export default {
  actions,
  state,
  getters,
  mutations
};
