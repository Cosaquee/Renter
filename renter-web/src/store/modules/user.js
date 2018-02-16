import axios from 'axios';
import config from '../../config';
import {
  USER_LOGOUT,
  USER_AUTH_SUCCESS,
  USER_LIST,
  USER_RENTED_BOOKS,
  USER_RENT_HISTORY
} from '../mutation-types';

const state = {
  token: sessionStorage.getItem('token') || '',
  user: JSON.parse(sessionStorage.getItem('user')) || {},
  users: [],
  rentedBooks: [],
  history: []
};

const getters = {
  isLogged: state => !!state.token,
  user: state => state.user,
  refresh_token: state => state.refresh_token,
  admin: store => state.user.roleId === 3,
  employee: store => state.user.roleId === 2,
  normal_user: store => state.user.roleId === 1,
  users: store => store.users,
  token: store => state.token,
  rentedBooks: state => state.rentedBooks,
  history: state => state.history
};

const actions = {
  getUsers (store) {
    return new Promise((resolve, reject) => {
      let token = state.token;
      axios.get(config.API.REGISTER, {
        headers: {
          'Authorization': 'Bearer ' + token
        }
      }).then((response) => {
        store.commit(USER_LIST, { users: response.data });
      });
    });
  },
  login (store, { username, password }) {
    return new Promise((resolve, reject) => {
      axios.post(config.API.LOGIN, {
        'username': username,
        'password': password,
      }).then((response) => {
        if (response.status === 200) {
          store.commit(USER_AUTH_SUCCESS,
            { user: response.data.user, token: response.data.accessToken, refresh_token: response.data.refreshToken });
          resolve();
        } else if (response.status === 404) {
          reject('Email or password incorrect.');
        }
      }).catch((error) => {
        reject(error);
      });
    });
  },
  registerUser (store, { name, surname, email, password, confirmPassword }) {
    return new Promise(function (resolve, reject) {
      if (password !== confirmPassword) reject(new Error('Passwords do not match'));
      axios.post(config.API.REGISTER, {
        'name': name,
        'surname': surname,
        'password': password,
        'confirmPassword': confirmPassword,
        'email': email
      }).then((response) => {
        if (response.status === 201) {
          resolve('User created');
        } else {
          reject('Cannot create user, please try again');
        }
      });
    });
  },
  logout (store) {
    store.commit(USER_LOGOUT);
  },
  getRentHistory (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/rent/history/${store.getters.user.id}`, {
        headers: {
          'Authorization': 'Bearer ' + state.token
        }}).then((response) => {
          store.commit('USER_RENT_HISTORY', { books: response.data.reverse() });
        });
    });
  },
  getRentedBooks (store) {
    return new Promise((resolve, reject) => {
      axios.get(`${config.API.BOOK}/rent/current/${store.getters.user.id}`, {
        headers: {
          'Authorization': 'Bearer ' + state.token
        }}).then((response) => {
          store.commit('USER_RENTED_BOOKS', { books: response.data.reverse() });
        });
    });
  }
};

const mutations = {
  [USER_AUTH_SUCCESS] (store, { user, token, refreshToken }) {
    store.user = user;
    store.error = '';
    store.token = token;
    store.refresh_token = refreshToken;
    sessionStorage.setItem('token', token);
    sessionStorage.setItem('user', JSON.stringify(user));
  },
  [USER_LOGOUT] (store) {
    store.user = {};
    store.token = '';
    sessionStorage.clear();
  },
  [USER_LIST] (store, { users }) {
    store.users = users;
  },
  [USER_RENTED_BOOKS] (store, { books }) {
    store.rentedBooks = books;
  },
  [USER_RENT_HISTORY] (store, { books }) {
    store.history = books;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
