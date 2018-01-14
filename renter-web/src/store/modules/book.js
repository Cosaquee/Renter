import axios from 'axios';
import config from '../../config';

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
  }
};

export default {
  actions
};
