<template>
  <section>
    <div class="search-menu">
      <at-input class="search-menu-left-input" v-model="name" @input="searchDirector" placeholder="Search for author"></at-input>
      <at-button @click="addAuthor" type="info" hollow>Add</at-button>
    </div>
    <div class="authors">
      <el-table
       :data="directors"
       style="width: 100%"
       @row-click="showDirector"
       empty-text="No directors">
         <el-table-column
           label="Name"
         >
           <template slot-scope="scope">{{ scope.row.name }}</template>
         </el-table-column>
         <el-table-column
           label="Name"
         >
           <template slot-scope="scope">{{ scope.row.surname }}</template>
         </el-table-column>

         <el-table-column
           label="Name"
         >
           <template slot-scope="scope">{{ scope.row.movies.length }}</template>
         </el-table-column>
     </el-table>
    </div>
  </section>
</template>

<script>
export default {
  data () {
    return {
      name: ''
    };
  },
  created () {
    this.$store.dispatch('fetchAllDirectors');
  },
  computed: {
    admin: function () {
      return this.$store.getters.admin;
    },
    employee: function () {
      return this.$store.getters.employee;
    },
    directors () {
      return this.$store.getters.directors;
    }
  },
  methods: {
    showDirector (row, event, column) {
      console.log(row);
      this.$router.push({ path: '/director/' + row.id });
    },
    addDirector () {
      console.log();
    },
  }
};
</script>

<style scoped>
  .search-menu {
    display: flex;
    justify-content: space-between;
  }
</style>
