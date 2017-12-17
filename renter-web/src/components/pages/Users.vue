<template>
  <section>
         <b-table
             :data="users"
             :paginated="isPaginated"
             :per-page="perPage"
             :pagination-simple="isPaginationSimple"
             :default-sort-direction="defaultSortDirection"
             default-sort="userName">

             <template slot-scope="props">
                 <b-table-column field="id" label="ID" width="40" sortable numeric>
                     {{ props.row.id }}
                 </b-table-column>

                 <b-table-column field="username" label="Username" sortable>
                     {{ props.row.userName }}
                 </b-table-column>

                 <b-table-column field="email" label="Email" sortable>
                     {{ props.row.email }}
                 </b-table-column>

                 <b-table-column field="Role" label="Role" sortable>
                     {{ props.row.role.name }}
                 </b-table-column>

                 <b-table-column field="Books" label="Role" sortable>
                     {{ props.row.rentBooks }}
                 </b-table-column>

                 <b-table-column field="Role" label="Role" sortable>
                     {{ props.row.rentMovies }}
                 </b-table-column>
             </template>
         </b-table>
     </section>
</template>

<script>
  export default {
    data () {
      return {
        users: this.$store.state.user.users,
        isPaginated: true,
        isPaginationSimple: false,
        defaultSortDirection: 'asc',
        perPage: 5
      };
    },
    created: function () {
      this.$store.dispatch('getUsers');
    }
  };
</script>
