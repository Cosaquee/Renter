<template>
  <section>
    <div class="movie-menu">
     <div class="movie-menu-left">
        <at-input class="movie-menu-left-input" v-model="name" @input="searchMovie" placeholder="Search by director or title"></at-input>

        <at-select class="movie-menu-left-select" style="width: 150px;" size="large" v-model="selectedCategory" placeholder="Select category">
          <at-option v-for="category in categories" :key="category.name" :value="category.name">{{ category.name }}</at-option>
        </at-select>

        <at-select class="movie-menu-left-select" style="width: 150px;" v-model="selectedYear" placeholder="Select year">
          <at-option v-for="i in range(1950, 2019)" :key="i" :value="i">{{ i }}</at-option>
        </at-select>

        <at-button size="small" class="clearButton" @click="clearFilter" icon="icon-x"></at-button>
      </div>

      <at-button @click="addMovie" type="info" hollow>Add</at-button>
    </div>
    <b-tabs position="is-centered" expanded>
      <b-tab-item label="Covers" class="covers">
        <div class="wrapper">
          <div class="cards">
            <card v-for="collection in selectMovies" :key="collection.resaizedCoverURL" :collection="collection"></card>
          </div>
        </div>
      </b-tab-item>
      <b-tab-item label="List">
        <b-table
          :data="selectMovies"
          @click="handleClick"
          :striped="true"
        >
          <template slot-scope="props">
            <b-table-column  class="movie-title" label="Title">
              {{ props.row.title }}
            </b-table-column>
            <b-table-column  class="movie-date" label="Release date">
              {{ new Date(props.row.releaseDate).getFullYear() }}
            </b-table-column>

            <b-table-column label="Category">
              {{ props.row.category.name }}
            </b-table-column>
            <b-table-column label="Description">
              {{ props.row.description }}
            </b-table-column>
            <b-table-column label="Director">
              {{ props.row.director.name }} {{ props.row.director.surname }}
            </b-table-column>
            <b-table-column label="Duration">
              {{ props.row.duration }}
            </b-table-column>
          </template>
        </b-table>
      </b-tab-item>
    </b-tabs>
  </section>
</template>

<script>
  import Card from './CoverCard';
  import _ from 'lodash';
  export default {
    data () {
      return {
        movies: [],
        name: '',
        selectedCategory: '',
        selectedYear: ''
      };
    },
    components: {
      Card
    },
    created () {
      this.$store.dispatch('getMovies');
      this.$store.dispatch('getCategories');
    },
    methods: {
      range (x, y) {
        return _.range(x, y);
      },
      handleClick (item) {
        this.$router.push({ path: '/movie/' + item.id });
      },
      searchMovie () {
        this.name = this.name;
      },
      selectMovie () {
        this.name = this.name;
        this.selectedCategory = this.selectedCategory;
      },
      addMovie () {
        this.$router.push({
          path: '/movie/create'
        });
      },
      clearFilter () {
        this.selectedCategory = '';
        this.name = '';
        this.selectedYear = '';
      },
    },
    computed: {
      selectMovies () {
        if (this.selectedCategory && this.name !== '' && this.selectedYear !== '') {
          console.log('Category, name and year');
          return this.$store.getters.movies.filter((movie) => {
            return new Date(movie.releaseDate).getFullYear() === this.selectedYear && movie.category.name === this.selectedCategory && (movie.title.toUpperCase().includes(this.name.toUpperCase()) || movie.director.name.toUpperCase().includes(this.name.toUpperCase()) || movie.director.surname.toUpperCase().includes(this.name
              .toUpperCase()));
          });
        } else if (this.selectedCategory && this.name !== '') {
          console.log('Category and name');
          return this.$store.getters.movies.filter((movie) => {
            return movie.category.name === this.selectedCategory && (movie.title.toUpperCase().includes(this.name.toUpperCase()) || movie.director.name.toUpperCase().includes(this.name.toUpperCase()) || movie.director.surname.toUpperCase().includes(this.name
              .toUpperCase()));
          });
        } else if (this.selectedCategory && this.selectedYear !== '') {
          return this.$store.getters.movies.filter((movie) => {
            return new Date(movie.releaseDate).getFullYear() === this.selectedYear && movie.category.name === this.selectedCategory;
          });
        } else if (this.selectedYear !== '' && this.name !== '') {
          return this.$store.getters.movies.filter((movie) => {
            return new Date(movie.releaseDate).getFullYear() === this.selectedYear && (movie.title.toUpperCase().includes(this.name.toUpperCase()) || movie.director.name.toUpperCase().includes(this.name.toUpperCase()) || movie.director.surname.toUpperCase().includes(this.name
              .toUpperCase()));
          });
        } else if (this.selectedYear !== '') {
          return this.$store.getters.movies.filter((movie) => {
            return new Date(movie.releaseDate).getFullYear() === this.selectedYear;
          });
        } else if (this.selectedCategory) {
          return this.$store.getters.movies.filter((movie) => {
            return movie.category.name === this.selectedCategory;
          });
        } else if (this.name !== '') {
          return this.$store.getters.movies.filter((movie) => {
            return movie.title.toUpperCase().includes(this.name.toUpperCase()) || movie.director.name.toUpperCase().includes(this.name.toUpperCase()) || movie.director.surname.toUpperCase().includes(this.name
              .toUpperCase());
          });
        }
        return this.$store.getters.movies;
      },
      user () {
        return this.$store.getters.user;
      },
      admin () {
        return this.$store.getters.admin;
      },
      employee () {
        return this.$store.getters.employee;
      },
      categories () {
        return this.$store.getters.categories;
      },
    }
  };
</script>

<style scoped>
  .category-select {
    padding: 1em;
    display: inline-block;
  }
  .search {
    margin-right: 3px;
  }

  .book-add {
    text-align: center;
  }

  .search-box {
    display: flex;
    justify-content: space-between;
  }

  .select-book {
    padding: 1em;
    display: flex;
    justify-content: space-start;
  }

  .select-book-select {
    margin-right: 3px;
  }

  .movie-menu {
    display: flex;
    justify-content: space-between;
  }

  .movie-menu-left {
    display: flex;
    justify-content: space-around;
  }

  .movie-menu-left-input {
    margin: 3px;
  }

  .movie-menu-left-select {
    margin: 4px;
  }

  .movie-title {
    font-weight: bold;
  }
</style>
