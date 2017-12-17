<template>
  <section>
         <b-field grouped group-multiline>
             <b-select v-model="defaultSortDirection">
                 <option value="asc">Default sort direction: ASC</option>
                 <option value="desc">Default sort direction: DESC</option>
             </b-select>
             <b-select v-model="perPage" :disabled="!isPaginated">
                 <option value="5">5 per page</option>
                 <option value="10">10 per page</option>
                 <option value="15">15 per page</option>
                 <option value="20">20 per page</option>
             </b-select>
             <div class="control is-flex">
                 <b-switch v-model="isPaginated">Paginated</b-switch>
             </div>
             <div class="control is-flex">
                 <b-switch v-model="isPaginationSimple" :disabled="!isPaginated">Simple pagination</b-switch>
             </div>
         </b-field>

         <b-table
             :data="users"
             :paginated="isPaginated"
             :per-page="perPage"
             :pagination-simple="isPaginationSimple"
             :default-sort-direction="defaultSortDirection"
             default-sort="user.first_name">

             <template slot-scope="props">

                 <b-table-column field="id" label="ID" width="40" sortable numeric>
                     {{ props.row.id }}
                 </b-table-column>
<!-- <
                 <b-table-column field="user.first_name" label="First Name" sortable>
                     {{ props.row.user.first_name }}
                 </b-table-column>

                 <b-table-column field="user.last_name" label="Last Name" sortable>
                     {{ props.row.user.last_name }}
                 </b-table-column>

                 <b-table-column field="date" label="Date" sortable centered>
                     <span class="tag is-success">
                         {{ new Date(props.row.date).toLocaleDateString() }}
                     </span>
                 </b-table-column>

                 <b-table-column label="Gender">
                     <b-icon pack="fa"
                         :icon="props.row.gender === 'Male' ? 'mars' : 'venus'">
                     </b-icon>
                     {{ props.row.gender }}
                 </b-table-column> -->
             </template>
         </b-table>
     </section>
</template>

<script>
  export default {
    data () {
      console.log(this.$store);

      return {
        users: this.$store.state.users,
        isPaginated: true,
        isPaginationSimple: false,
        defaultSortDirection: 'asc',
        perPage: 5
      };
    },
    computed: {
      users () {
        return this.$store.state.users;
      }
    },
    methods: {
    },
    created: function () {
      this.$store.dispatch('getUsers');
    }
  };
</script>
