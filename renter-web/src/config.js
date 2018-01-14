const root = 'http://localhost:5000/api/';
export default {
  API: {
    LOGIN: root + 'user/authorize',
    REGISTER: root + 'user',
    AUTHORS: root + 'author',
    CATEGORIES: root + 'category',
    BOOK: root + 'book'
  }
};
