<template>
  <section>
    <at-menu v-if="isLogged" mode="horizontal" class="nav-bar">
      <div class="navigation">
         <at-menu-item :to="{ path: '/' }" name="home"><i class="icon icon-home"></i>Home</at-menu-item>
         <at-menu-item :to="{ path: '/book' }" name="books"><i class="icon icon-book"></i>Books</at-menu-item>
         <at-menu-item :to="{ path: '/movie' }" name="movie"><i class="icon icon-film"></i>Movies</at-menu-item>
         <at-submenu>
           <template slot="title"><i class="icon icon-users"></i>People</template>
            <at-menu-item :to="{ path: '/author' }" name="author">Authors</at-menu-item>
            <at-menu-item :to="{ path: '/director' }" name="director">Directors</at-menu-item>
         </at-submenu>
      </div>

      <div class="administration">
        <at-submenu v-if="">
          <template slot="title"><i class="icon icon-settings"></i>{{ user.name }} {{ user.surname }}</template>
          <at-menu-item :to="{ path: '/profile' }" name="profile"><i class="icon icon-user"></i>Profile</at-menu-item>
          <at-menu-item v-if="admin" :to="{ path: '/users' }" name="Users"><i class="icon icon-users"></i>Users</at-menu-item>
          <at-menu-item v-if="admin || employee" :to="{ path: '/book/orders' }" name="Orders"><i class="icon icon-shopping-cart"></i>Orders</at-menu-item>
          <at-menu-item name="Logout"><i @click="logout" class="icon icon-log-out"></i>Logout</at-menu-item>
        </at-submenu>
      </div>
    </at-menu>

    <at-menu v-if="!isLogged" mode="horizontal" class="nav-bar">
      <div class="navigation">
         <at-menu-item :to="{ path: '/' }" name="home"><i class="icon icon-home"></i>Home</at-menu-item>
      </div>

      <div class="administration">
         <at-menu-item :to="{ path: '/login' }" name="login">Login</at-menu-item>
         <at-menu-item :to="{ path: '/register' }" name="register">Register</at-menu-item>
      </div>
    </at-menu>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    computed: mapGetters(['isLogged', 'user', 'admin', 'employee']),
    methods: {
      logout () {
        this.$store.dispatch('logout');
        this.$router.push({name: 'welcome'});
      },
      back () {
        this.$router.go(-1);
      },
    }
  };
</script>

<style>
  .nav-bar {
    margin-bottom: 1em;
    display: flex;
    justify-content: space-between;
  }
</style>
