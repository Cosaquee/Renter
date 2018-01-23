import axios from 'axios';
import config from '../../config';

const actions = {
  uploadMovieCover (store, { formData }) {
    return new Promise((resolve, reject) => {
      axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';
      axios.post(`${config.SERVICES.COVER.MOVIE}`, formData)
        .then((response) => {
          resolve(response);
        }).catch((error) => {
          reject(error);
        });
    });
  },
  uploadBookCover (store, { formData }) {
    axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';
    return new Promise((resolve, reject) => {
      axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';
      axios.post(`${config.SERVICES.COVER.BOOK}`, formData)
        .then((response) => {
          resolve(response);
        }).catch((error) => {
          reject(error);
        });
    });
  }
};

export default {
  actions
};
