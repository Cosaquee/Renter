<template>
  <section>
      <b-field grouped position="is-centered">
        <b-input v-model="category" horizontal label="Dodaj kategorię" placeholder="Dodaj kategorię"></b-input>
        <p class="control">
          <button @click="addCategory(category)"  class="button is-primary">Add</button>
        </p>
      </b-field>
    <table-component
      :data="computedCategories"
      sort-by="id"
      sort-order="desc">
      <table-column show="name" label="kategoria"></table-column>
      <table-column label="Usuń">
        <template slot-scope="row">
          <button @click="removeCategory(row.id)" class="button is-danger">Usuń</button>
        </template>
      </table-column>
    </table-component>
  </section>
</template>

<script>
  export default {
    data () {
      console.log(this.$store.state);
      return {
        // categories: this.$store.state.categories.categories
        category: '',
        categories: [{name: 'Horror', id: 1}, {name: 'Rodzinne', id: 2}]
      };
    },
    methods: {
      addCategory (categoryAdd) {
        this.categories.push({name: categoryAdd, id: this.categories.length + 1});
        // Tu requesty na widoku dodajemy żeby nie robic przeladowania
      },
      removeCategory (id) {
        this.categories.splice(this.categories.map(function (e) { return e.id; }).indexOf(id), 1);
        // Tu requesty na widoku wywalamy pod spodem idzie zapytanie do bazy dopiero
      },
    },
    computed: {
      computedCategories: function () {
        var categories = [];
        for (let i = 0; i < this.categories.length; i++) {
          categories.push(this.categories[i]);
        }
        return categories;
      },
    }
  };
</script>
