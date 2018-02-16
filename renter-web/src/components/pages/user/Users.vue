<template>
  <section>
         <!-- <b-table
             :data="computedUsers"
             :paginated="isPaginated"
             :per-page="perPage"
             :pagination-simple="isPaginationSimple"
             :default-sort-direction="defaultSortDirection"
             default-sort="username">

             <template slot-scope="props">
                 <b-table-column field="username" label="Username" sortable>
                     {{ props.row.name }}
                 </b-table-column>

                 <b-table-column field="email" label="Email" sortable>
                     {{ props.row.email }}
                 </b-table-column>

                 <b-table-column field="Role" label="Role" sortable>
                     {{ props.row.role }}
                 </b-table-column>

                 <b-table-column field="books" label="Books" sortable>
                     {{ props.row.books }}
                 </b-table-column>

                 <b-table-column field="movies" label="Movies" sortable>
                     {{ props.row.movies }}
                 </b-table-column>
             </template>
         </b-table> -->
           <el-table
            :data="computedUsers"
            style="width: 100%"
            empty-text="No users">
              <el-table-column label="Name">
                <template slot-scope="scope">{{ scope.row.name }}</template>
              </el-table-column>
              <el-table-column label="Surname">
              <template slot-scope="scope">{{ scope.row.surname}}</template>
              </el-table-column>
              <el-table-column label="Role">
              <template slot-scope="scope">{{ scope.row.role.name }}</template>
              </el-table-column>
          </el-table>
     </section>
</template>

<script>
  export default {
    data () {
      return {
      };
    },
    created () {
      this.$store.dispatch('getUsers');
    },
    computed: {
      computedUsers () {
        return this.$store.getters.users;
      }
    }
  };
</script>
