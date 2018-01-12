<template>
  <section>
    <div class="categories">
      <div v-if="admin || employee" class="author-add">
        <a @click="addCategory" class="button is-primary is-outlined">New category</a>
      </div>

      <table-component
           :data="categories"
           sort-by="name"
           @rowClick="handleClick"
           sort-order="asc"
      >
           <table-column :hidden="hidden" show="id" label="ID"></table-column>
           <table-column show="name" label="Name"></table-column>
       </table-component>

    </div>
  </section>
</template>
<script>
  export default {
    data () {
      return {
        hidden: true
      };
    },
    computed: {
      admin () {
        return this.$store.getters.admin;
      },
      employee () {
        return this.$store.getters.employee;
      },
      categories  () {
        return this.$store.getters.categories;
      }
    },
    methods: {
      addCategory () {
        // TODO: https://github.com/Cosaquee/Renter/issues/23
      },
      handleClick () {
        // TODO: https://github.com/Cosaquee/Renter/issues/21
      }
    },
    created () {
      this.$store.dispatch('getCategories');
    }
  };
</script>
