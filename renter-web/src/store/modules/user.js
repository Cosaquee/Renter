import axios from 'axios';
import config from '../../config';
import {
  USER_LOGOUT,
  USER_AUTH_SUCCESS,
  USER_LIST
} from '../mutation-types';

const state = {
  token: sessionStorage.getItem('token') || '',
  user: JSON.parse(sessionStorage.getItem('user')) || {},
};

const getters = {
  isLogged: state => !!state.token,
  user: state => state.user,
  refresh_token: state => state.refresh_token,
  admin: store => store.user.roleId === 4,
  users: store => store.users || [],
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
        console.log(response);
        store.commit(USER_LIST, { users: response.data })
      })
    })
  },
  login (store, { username, password }) {
    return new Promise((resolve, reject) => {
      axios.post(config.API.LOGIN, {
        'username': username,
        'password': password,
      }).then((response) => {
        if (response.status === 200) {
          const admin = (response.data.user.role.name === 'Administrator');
          store.commit(USER_AUTH_SUCCESS,
            { user: response.data.user, token: response.data.accessToken, refresh_token: response.data.refreshToken, admin: admin });
          resolve();
        } else {
          reject();
        }
      }).catch(() => {
        reject('Error sending request to server!');
      });
    });
  },
  registerUser (store, { username, password, cpassword, email }) {
    return new Promise(function (resolve, reject) {
      if (password !== cpassword) reject(new Error('Passwords do not match'));
      axios.post(config.API.REGISTER, {
        'username': username,
        'password': password,
        'confirmPassword': cpassword,
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
  }
};

const mutations = {
  [USER_AUTH_SUCCESS] (store, { user, token, refreshToken, admin }) {
    store.user = user;
    store.error = '';
    store.token = token;
    store.refresh_token = refreshToken;
    store.admin = admin;
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
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
