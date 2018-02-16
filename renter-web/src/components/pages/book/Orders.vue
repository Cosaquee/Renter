<template>
  <section>
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <section class="orders-menu">
      <section>
        <at-button class="menu-button" type="error" size="large" icon="icon-x-circle" hollow @click="checkedRows = []">Clear section</at-button>
        <at-button class="menu-button" type="info" size="large" icon="icon-check" hollow @click="rentBooks">Confirm rent</at-button>
        <at-button class="menu-button" type="primary" size="large" icon="icon-user-check" hollow @click="returnBooks">Confirm return</at-button>
      </section>

      <section class="search-box">
        <at-button size="small" class="clearButton" @click="clearFilter" icon="icon-x"></at-button>
        <at-input class="search-box-input" @input="searchUser" v-model="user" placeholder="Search for title or user"></at-input>
        <at-select class="search-box-select" style="width: 150px;" size="large" @input="selectState" v-model="state" placeholder="Select state">
          <at-option v-for="category in states" :value="category.name">{{ category.name }}</at-option>
        </at-select>
      </section>
    </section>

    <at-tabs>
      <at-tab-pane label="Books" name="books" icon="icon-book">
        <el-table
         ref="multipleTable"
         :data="formattedRentedBooks"
         style="width: 100%"
         @selection-change="handleSelectionChange"
         :row-class-name="rowClass"
         empty-text="No books">
           <el-table-column
             type="selection"
             width="55">
           </el-table-column>
           <el-table-column
             label="Title"
           >
             <template slot-scope="scope">{{ scope.row.book.title }}</template>
           </el-table-column>
           <el-table-column
             label="User"
           >
           <template slot-scope="scope">{{ scope.row.user.name }} {{ scope.row.user.surname }}</template>
           </el-table-column>
           <el-table-column
             label="From"
             show-overflow-tooltip>
            <template slot-scope="scope">{{ new Date(scope.row.from).toLocaleDateString("pl-PL") }}</template>
           </el-table-column>
           <el-table-column
             label="To"
             show-overflow-tooltip>
            <template slot-scope="scope">{{ new Date(scope.row.to).toLocaleDateString("pl-PL") }}</template>
           </el-table-column>
           <el-table-column
             label="State"
             show-overflow-tooltip>
            <template slot-scope="scope">{{ scope.row.state ? 'Owned' : 'Ready to pickup' }}</template>
           </el-table-column>
       </el-table>
      </at-tab-pane>
      <at-tab-pane label="Movies" name="movies" icon="icon-film">
        <el-table
         :data="formattedRentedMovies"
         style="width: 100%"
         :row-class-name="rowClass"
         empty-text="No movies">
           <el-table-column
             label="Title"
           >
             <template slot-scope="scope">{{ scope.row.movie.title }}</template>
           </el-table-column>
           <el-table-column
             label="User"
           >
           <template slot-scope="scope">{{ scope.row.user.name }} {{ scope.row.user.surname }}</template>
           </el-table-column>
           <el-table-column
             label="From"
             show-overflow-tooltip>
            <template slot-scope="scope">{{ new Date(scope.row.from).toLocaleDateString("pl-PL") }}</template>
           </el-table-column>
           <el-table-column
             label="To"
             show-overflow-tooltip>
            <template slot-scope="scope">{{ new Date(scope.row.to).toLocaleDateString("pl-PL") }}</template>
           </el-table-column>
           <el-table-column
             label="Quality"
             show-overflow-tooltip>
            <template slot-scope="scope">{{ quality(scope.row) }}</template>
           </el-table-column>
       </el-table>
      </at-tab-pane>
    </at-tabs>
  </section>
</template>

<script>
import Moment from 'moment';
export default {
  data () {
    return {
      hidden: true,
      checkedRows: [],
      loading: false,
      state: '',
      user: '',
      multipleSelection: []
    };
  },
  created () {
    this.$store.dispatch('getAllRentedBooks');
    this.$store.dispatch('fetchAllRents');
  },
  methods: {
    handleSelectionChange (val) {
      this.multipleSelection = val;
    },
    handleClick () {
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
    rentBooks () {
      console.log(this.multipleSelection);
      this.loading = true;
      this.multipleSelection.map((row) => {
        this.$store.dispatch('confirmRent', {
          bookID: row.bookId
        }).then(() => {
          this.$store.dispatch('getAllRentedBooks');
          this.loading = false;
        });
      });
    },
    returnBooks () {
      this.loading = true;
      this.multipleSelection.map((row) => {
        this.$store.dispatch('confirmReturn', {
          bookID: row.bookId
        }).then(() => {
          this.loading = false;
          this.$store.dispatch('getAllRentedBooks');
        });
      });
    },
    selectState () {
      this.state = this.state;
    },
    searchUser () {
      this.user = this.user;
    },
    clearFilter () {
      this.state = '';
      this.user = '';
    }
  },
  computed: {
    formattedRentedBooks () {
      let states = {
        'ready': 0,
        'rented': 1
      };

      if (this.user !== '' && this.state !== '') {
        return this.$store.getters.allRentedBooks.filter((book) => {
          return book.state === states[this.state.toLowerCase()] && (book.user.name.toUpperCase().includes(this.user.toUpperCase()) || book.user.name.surname.toUpperCase().includes(this.user.toUpperCase()));
        });
      } else if (this.state !== '') {
        return this.$store.getters.allRentedBooks.filter((book) => {
          return book.state === states[this.state.toLowerCase()];
        });
      } else if (this.user !== '') {
        return this.$store.getters.allRentedBooks.filter((book) => {
          return (book.user.name.toUpperCase().includes(this.user.toUpperCase()) || book.user.surname.toUpperCase().includes(this.user.toUpperCase()));
        });
      }
      return this.$store.getters.allRentedBooks;
    },
    formattedRentedMovies () {
      if (this.user !== '') {
        return this.$store.getters.rentedMovies.filter((movie) => {
          return (movie.user.name.toUpperCase().includes(this.user.toUpperCase()) || movie.user.surname.toUpperCase().includes(this.user.toUpperCase()));
        });
      }
      return this.$store.getters.rentedMovies;
    },
    states () {
      return [{ name: 'ready', id: 0 }, { name: 'rented', id: 1 }];
    }
  }
};
</script>
<style>

  .orders-menu {
    display: flex;
    justify-content: space-between;
  }

  .select-box {
    margin: 5px;
  }

  .search-box {
    display: flex;
  }

  .search {
    margin: 5px 3px auto;
  }

  .clearButton {
    margin: 3px;
  }

  .search-box-input {
    margin: 4px;
  }

  .search-box-select {
    margin: 4px;
  }

  tr.is-warning {
    background: hsla(48, 100%, 67%, .5) !important;
  }

  tr.is-error {
    background: hsla(5, 99%, 50%, 0.5) !important;
  }
</style>
