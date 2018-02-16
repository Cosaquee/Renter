<template>
  <section class="user-panel">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <section>
      <div v-if="admin" class="menu">
        <at-button class="menu-button" type="primary" size="large" icon="icon-file-plus" hollow @click="isComponentModalActive = true">Add avatar</at-button>
        <at-button class="menu-button" type="info" size="large" icon="icon-edit" hollow @click="prompt">Edit</at-button>
        <at-button class="menu-button" type="error" size="large" icon="icon-trash-2" hollow @click="deleteUser">Delete user</at-button>
      </div>

      <div v-if="normal_user" class="menu">
        <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">Add avatar</button>
      </div>
      <b-modal :active.sync="isComponentModalActive" has-modal-card>
        <AvatarModal v-bind="{ user: this.user }"></AvatarModal>
      </b-modal>

    </section>

    <div class="columns">
      <div class="column">
        <p class="user-name">{{ user.name }} {{ user.surname }}</p>
        <div class="user-avatar">
          <img :src="user.provileAvatar" alt="User avatar">
        </div>
        <div class="user-info">
          <p class="info">Email: {{ user.email }}</p>
          <p class="info">Books read: {{ history.length }}</p>
          <p class="info">Movies watched: {{ moviesRented.length }}</p>
        </div>
      </div>

      <div class="column is-two-thirds">
        <at-tabs>
          <at-tab-pane label="Current" name="current">
            <el-table
             :data="current"
             style="width: 100%"
             :row-class-name="rowClass"
             @row-click="showLink"
             empty-text="No rent">
               <el-table-column
                 label="Type"
               >
                 <template slot-scope="scope"><i :class="decideType(scope.row.type)"></i></template>
               </el-table-column>
               <el-table-column
                 label="Title"
               >
               <template slot-scope="scope">{{ scope.row.title }}</template>
               </el-table-column>
               <el-table-column
                 label="From"
                 show-overflow-tooltip>
                <template slot-scope="scope">{{ scope.row.from }}</template>
               </el-table-column>
               <el-table-column
                 label="To"
                 show-overflow-tooltip>
                <template slot-scope="scope">{{ scope.row.to }}</template>
               </el-table-column>
               <el-table-column
                 label="Extra"
                 show-overflow-tooltip>
                <template slot-scope="scope">{{ scope.row.status }}</template>
               </el-table-column>
           </el-table>
          </at-tab-pane>
          <at-tab-pane label="History" name="history">
            <at-select clearable class="movie-menu-left-select" style="width: 150px;" v-model="selectedType" placeholder="Select type">
              <at-option key="book" value="book">Books</at-option>
              <at-option key="movie" value="movie">Movies</at-option>
            </at-select>

            <el-table
             :data="globalHistory"
             style="width: 100%"
             :row-class-name="rowClass"
             empty-text="No rent">
               <el-table-column
                 label="Type"
               >
                 <template slot-scope="scope"><i :class="decideType(scope.row.type)"></i></template>
               </el-table-column>
               <el-table-column
                 label="Title"
               >
               <template slot-scope="scope">{{ scope.row.title }}</template>
               </el-table-column>
               <el-table-column
                 label="From"
                 show-overflow-tooltip>
                <template slot-scope="scope">{{ scope.row.from }}</template>
               </el-table-column>
               <el-table-column
                 label="To"
                 show-overflow-tooltip>
                <template slot-scope="scope">{{ scope.row.to }}</template>
               </el-table-column>
               <el-table-column
                 label="Extra"
                 show-overflow-tooltip>
                <template slot-scope="scope">{{ scope.row.status }}</template>
               </el-table-column>
           </el-table>
          </at-tab-pane>
        </at-tabs>
      </div>
    </div>
  </section>
</template>

<script>
  import AvatarModal from './modals/AvatarModal';
  import Moment from 'moment';

  export default {
    data () {
      return {
        loading: false,
        isComponentModalActive: false,
        booksRead: [],
        moviesRented: [],
        selectedType: ''
      };
    },
    components: {
      AvatarModal
    },
    created () {
      this.$store.dispatch('getRentHistory');
      this.$store.dispatch('getRentedBooks');
      this.$store.dispatch('fetchRentedMovies');
      this.$store.dispatch('fetchRentedMoviesHistory');
    },
    methods: {
      showLink (row, event, column) {
        if (row.type === 'book') {
          return;
        }
        this.$dialog.alert({
          message: `Your movie link <a>https://asdasdasd.io/movies/${row.title}/${this.$store.getters.user.id}</a>`,
          onConfirm: () => {
          }
        });
      },
      quality (row) {
        let quality;
        switch (row.movieQuality) {
          case 0:
            quality = 'SD';
            break;
          case 1:
            quality = 'HDReady';
            break;
          case 2:
            quality = 'FullHD';
            break;
          case 3:
            quality = '4K';
            break;
        }

        return quality;
      },
      rowClass ({ row, index }) {
        let now = Moment();
        let to = Moment(new Date(row.to));
        let diff = to.diff(now, 'days');
        if (diff <= 4 && diff > 0) {
          return 'is-warning';
        }
        if (diff <= 0) {
          return 'is-error';
        }
      },
      prompt () {

      },
      deleteUser () {

      },
      handleClick () {

      },
      decideType (type) {
        if (type === 'movie') {
          return 'icon icon-film';
        } else {
          return 'icon icon-book';
        }
      },
      handleType (row) {
        return row.bookId ? 'book' : 'film';
      },
      computedBooks () {
        var books = [];
        this.$store.getters.rentedBooks.map((book) => {
          books.push({
            type: 'book',
            title: book.book.title,
            from: new Date(book.from).toLocaleDateString('pl-PL'),
            to: new Date(book.to).toLocaleDateString('pl-PL'),
            status: book.received ? 'Owned' : 'Ready to pickup'
          });
        });
        return books.sort((a, b) => { return a.from - b.from; });
      },
      computedMovies () {
        let movies = [];
        this.$store.getters.rentedMovies.map((movie) => {
          movies.push({
            type: 'movie',
            title: movie.movie.title,
            from: new Date(movie.from).toLocaleDateString('pl-PL'),
            to: new Date(movie.to).toLocaleDateString('pl-PL'),
            status: this.quality(movie)
          });
        });
        return movies.sort((a, b) => { return a.to - b.to; });
      },
      historyBooks () {
        var books = [];
        this.$store.getters.history.map((book) => {
          books.push({
            title: book.book.title,
            from: new Date(book.from).toLocaleDateString('pl-PL'),
            to: new Date(book.to).toLocaleDateString('pl-PL'),
            until: new Date(book.to).toLocaleDateString('pl-PL')
          });
        });
        return books;
      },
      historyMovies () {
        let movies = [];
        this.$store.getters.rentedMoviesHistory.map((movie) => {
          movies.push({
            type: 'movie',
            title: movie.movie.title,
            from: new Date(movie.from).toLocaleDateString('pl-PL'),
            to: new Date(movie.to).toLocaleDateString('pl-PL'),
            status: this.quality(movie)
          });
        });
        return movies.sort((a, b) => { return a.to - b.to; });
      }
    },
    computed: {
      user () {
        return this.$store.getters.user;
      },
      admin () {
        return this.$store.getters.admin;
      },
      normal_user () {
        return this.$store.getters.normal_user;
      },
      history () {
        return this.$store.getters.history;
      },
      current () {
        let books = this.computedBooks();
        let movies = this.computedMovies();
        let currentRent = books.concat(movies);

        return currentRent.sort((a, b) => { return a.to - b.to; });
      },
      globalHistory () {
        let movies = this.historyMovies();
        let books = this.historyBooks();

        if (this.selectedType === 'book') {
          return books.sort((a, b) => { return a.to - b.to; });
        } else if (this.selectedType === 'movie') {
          return movies.sort((a, b) => { return a.to - b.to; });
        } else if (this.selectedType === '') {
          let allHistory = books.concat(movies);
          return allHistory.sort((a, b) => { return a.to - b.to; });
        }
      }
    },
  };
</script>

<style scoped>
  .user-avatar {
    width: 90%;
    margin: 0 0 0 10%;
  }

  .user-name {
    text-align: center;
    font-size: 3em;
  }

  .info {
    font-size: 2em;
  }

  .menu-button {
    min-width: 30%;
  }

  .columns {
    padding: 2em;
  }

  .user-info {
    text-align: left;
    margin: 0 0 0 10%;
  }
</style>
