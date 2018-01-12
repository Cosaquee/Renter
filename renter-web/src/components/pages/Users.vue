<template>
  <section>
         <b-table
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
         </b-table>
     </section>
</template>

<script>
  import _ from 'lodash';

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
    },
    computed: {
      computedUsers: function () {
        let roles = {
          1: 'Administrator',
          2: 'Employee',
          3: 'User'
        };

        var users = [];

        _.forEach(this.$store.state.user.users, (user) => {
          var movies = null;
          var books = null;

          if (user.rentMovies === null) {
            movies = [];
          }

          if (user.rentBooks === null) {
            books = [];
          }

          let us = {
            email: user.email,
            role: roles[user.roleId],
            name: user.userName,
            movies: movies.length,
            books: books.length
          };

          users.push(us);
        });

        return users;
      }
    }
  };
</script>
