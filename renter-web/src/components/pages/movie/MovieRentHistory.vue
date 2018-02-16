<template>
  <section>
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <el-table
     :data="history"
     style="width: 100%"
     empty-text="No movies">
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
     </el-table>
  </section>
</template>

<script>
export default {
  data () {
    return {
      loading: false,
      history: []
    };
  },
  created () {
    this.loading = true;
    this.$store.dispatch('fetchMovieHistory', {
      movieID: this.$route.params.id
    }).then((response) => {
      this.loading = false;
      this.hostory = response;
    });
  }
};
</script>

<style lang="css">
</style>
