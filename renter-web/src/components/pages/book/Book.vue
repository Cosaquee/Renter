<template>
  <section>
    <b-tabs position="is-centered" expanded >
      <b-tab-item label="List" >
        <div class="columns .is-centered">
          <div class="column">
            <div v-if="admin || employee" class="book-add">
              <a @click="addBook" class="button is-primary is-outlined">New Book</a>
            </div>

            <table-component
                 :data="books"
                 @rowClick="handleClick"
                 sort-order="asc"
            >
                 <table-column :hidden="hidden" show="id" label="ID"></table-column>
                 <table-column show="title" label="Title"></table-column>

                 <table-column label="Author">
                   <template slot-scope="row">
                     {{ row.author.name }} {{ row.author.surname }}
                   </template>
                 </table-column>
                 <table-column label="Available?">
                   <template slot-scope="row">
                     <b-tag :type="checkAvailable(row) ? 'is-success' : 'is-danger'" rounded>{{ checkAvailable(row) ? 'Available' : 'Not available' }}</b-tag>
                   </template>
                 </table-column>
             </table-component>
          </div>
        </div>
      </b-tab-item>
      <b-tab-item label="Covers" class="covers">
        <div class="">
          <b-select class="category-select" @input="selectBook" v-model="selectedCategory" placeholder="Choose category">
              <option
                  v-for="category in categories"
                  :value="category.name"
                  :key="category.id">
                  {{ category.name }}
              </option>
          </b-select>
            <a @click="clearFilter" class="button clear is-small is-primary is-outlined">Clear</a>
        </div>

        <div class="wrapper">
          <div class="cards">
            <card v-for="collection in selectBooks" :key="collection.resaizedCoverURL" :collection="collection"></card>
          </div>
        </div>
      </b-tab-item>
    </b-tabs>
  </section>
</template>

<script>
  import Card from './misc/CoverCard';
  export default {
    data () {
      return {
        hidden: true,
        selectedCategory: ''
      };
    },
    components: { Card },
    created: function () {
      this.$store.dispatch('fetchBooks');
      this.$store.dispatch('getCategories');
    },
    methods: {
      handleClick (item) {
        this.$router.push({ path: '/book/' + item.data.id });
      },
      addBook (e) {
        this.$router.push({ path: '/book/create' });
      },
      selectBook () {
        this.selectedCategory = this.selectedCategory;
      },
      checkAvailable (row) {
        return row.copies.some((book) => {
          return !book.rented === true;
        });
      },
      clearFilter () {
        this.selectedCategory = '';
      }
    },
    computed: {
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
      books () {
        return this.$store.getters.books;
      },
      selectBooks () {
        if (this.selectedCategory) {
          return this.$store.getters.books.filter((book) => {
            return book.category.name === this.selectedCategory;
          });
        }
        return this.$store.getters.books;
      }
    }
  };
</script>

<style scoped>
  .category-select {
    padding: 1em;
    display: inline-block;
  }

  .clear {
    margin-top: 1.7em;
  }

  .covers {
    overflow: scroll;
  }
  .cards {
    column-count: 1;
    column-gap: 1em;
  }

  @media only screen and (min-width: 500px) {
  .cards {
    column-count: 2;
  }
}

@media only screen and (min-width: 700px) {
  .cards {
    column-count: 3;
  }
}

@media only screen and (min-width: 900px) {
  .cards {
    column-count: 4;
  }
}

@media only screen and (min-width: 1100px) {
  .cards {
    column-count: 5;
  }
}
</style>
