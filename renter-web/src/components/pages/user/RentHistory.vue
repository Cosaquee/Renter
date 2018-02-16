<template>
  <div class="rent-history">
    <table-component
      :data="books"
      @rowClick="rentDetails"
      sort-order="desc">
      <table-column show="book.title" label="Title"></table-column>
      <table-column label="Rent date">
        <template slot-scope="row">
          {{ new Date(row.from).toLocaleDateString("pl-PL") }}
        </template>
      </table-column>
      <table-column label="Return date">
        <template slot-scope="row">
          {{ new Date(row.to).toLocaleDateString("pl-PL") }}
        </template>
      </table-column>
      <table-column label="received?">
        <template slot-scope="row">
          {{ row.received ? 'Owned' : 'Ready to pickup' }}
        </template>
      </table-column>
    </table-component>
  </div>
</template>

<script>
export default {
  props: [ 'books' ],
  name: 'rent-history',
  methods: {
    rentDetails () {

    }
  }
};
</script>

<style scoped>
</style>
