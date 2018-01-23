const root = 'http://localhost:5000/api/';
const coverAPI = 'http://localhost:4567';

export default {
  API: {
    LOGIN: root + 'user/authorize',
    REGISTER: root + 'user',
    AUTHORS: root + 'author',
    CATEGORIES: root + 'category',
    BOOK: root + 'book',
    MOVIE: root + 'movie',
    USER: root + 'user/'
  },
  SERVICES: {
    COVER: {
      MOVIE: coverAPI + '/cover/movie',
      BOOK: coverAPI + '/cover/book'
    },
    AVATAR: coverAPI + '/user/avatar'
  }
};
