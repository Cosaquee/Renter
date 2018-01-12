<template>
  <nav class="navbar is-light" style="margin-bottom: 10px;">
    <div class="navbar-brand">
      <router-link class="navbar-item" to="/">
        <img src="https://image.flaticon.com/icons/png/512/25/25694.png" alt="Logo" >

      </router-link>
      <a class="navbar-item" slot="trigger">
        <span @click="back"><b>Back</b></span>
      </a>
      <button class="button navbar-burger">
        <span></span>
        <span></span>
        <span></span>
      </button>
    </div>
    <!-- TODO: Move menus closer together -->
    <div v-if="isLogged" class="navbar-menu">
      <div class="navbar-end">
        <b-dropdown position="is-bottom-left">
          <a class="navbar-item" slot="trigger">
            <span>Welcome, <b>{{ user.userName }}</b></span>
            <b-icon icon="angle-down"></b-icon>
          </a>
          <b-dropdown-item has-link>
            <router-link to="/profile">
              <b-icon icon="user"></b-icon>
              Profile
            </router-link>
          </b-dropdown-item>
          <b-dropdown-item has-link>
            <router-link to="/author">
              <b-icon icon=""></b-icon>
              Authors
            </router-link>
          </b-dropdown-item>

          <b-dropdown-item has-link>
            <router-link to="/book/">
              <b-icon icon=""></b-icon>
              Books
            </router-link>
          </b-dropdown-item>


          <hr class="dropdown-divider">
          <b-dropdown-item value="logout" @click="logout">
              <b-icon icon="sign-out"></b-icon>
              Logout
          </b-dropdown-item>
        </b-dropdown>
      </div>
      <!-- Admin -->
      <div v-if="admin || employee" class="navbar-end">
        <b-dropdown v-if="admin" position="is-bottom-left">
          <a class="navbar-item" slot="trigger">
            <span>Administration</b></span>
            <b-icon icon="angle-down"></b-icon>
          </a>
          <b-dropdown-item has-link>
            <router-link to="/users">
              <b-icon icon="user"></b-icon>
                Users
            </router-link>
          </b-dropdown-item>
          <b-dropdown-item has-link>
            <router-link to="/author">
              <b-icon icon="Author"></b-icon>
                Authors
            </router-link>
          </b-dropdown-item>
          <b-dropdown-item has-link>
            <router-link to="/category">
              <b-icon icon="TODO"></b-icon>
                Categories
            </router-link>
          </b-dropdown-item>

        </b-dropdown>
      </div>
    </div>

    <div v-else class="navbar-menu">
      <div class="navbar-end">
        <router-link to="login" class="is-tab navbar-item">Login</router-link>
        <router-link to="register" class="is-tab navbar-item">Register</router-link>
      </div>
    </div>
  </nav>
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
      }
    },
  };
</script>
