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
    <div class="navbar-start">
       <b-dropdown position="is-bottom-left">
         <a class="navbar-item" slot="trigger">
           <span>Wypożyczalnia</span>
           <b-icon icon="angle-down"></b-icon>
         </a>
         <b-dropdown-item has-link>
           <router-link to="/book">
             <b-icon icon="book"></b-icon>
            Książki
           </router-link>
         </b-dropdown-item>
         <b-dropdown-item has-link>
           <router-link to="/movie">
             <b-icon icon="film"></b-icon>
             Filmy
          </router-link>
        </b-dropdown-item>
      </b-dropdown>
   </div>

   <div class="navbar-end">
     <b-dropdown position="is-bottom-left">
       <a class="navbar-item" slot="trigger">
         <span>Welcome, <b>{{ user.userName }}</b></span>
         <b-icon icon="angle-down"></b-icon>
       </a>
       <b-dropdown-item custom >
         Zalogowany jako <b>{{this.returnNameRole()}}</b>
       </b-dropdown-item>
       <b-dropdown-item has-link>
         <router-link to="/profile">
           <b-icon icon="user"></b-icon>
           Profile
         </router-link>
       </b-dropdown-item>
       <div v-if="admin || employee" >
         <b-dropdown-item @click="showAdminActions">
          <b> Narzędzia Administracyjne</b>
         </b-dropdown-item>
         <div v-if="isAdminOpen">
         <hr class="dropdown-divider">
           <b-dropdown-item has-link>
             <router-link to="/users">
               <b-icon icon="user"></b-icon>
               Users
             </router-link>
           </b-dropdown-item>
           <b-dropdown-item has-link>
             <router-link to="/author">
               <b-icon icon="address-book"></b-icon>
               Authors
             </router-link>
           </b-dropdown-item>
           <b-dropdown-item has-link>
             <router-link to="/category">
               <b-icon icon="camera-retro"></b-icon>
               Kategorie
             </router-link>
           </b-dropdown-item>
       </div>
       </div>
       <hr class="dropdown-divider">
       <b-dropdown-item value="logout" @click="logout">
           <b-icon icon="sign-out"></b-icon>
           Logout
       </b-dropdown-item>
     </b-dropdown>
   </div>
    <div v-if="!isLogged" class="navbar-menu">
      <div class="navbar-end">
        <router-link to="login" class="is-tab navbar-item">Login</router-link>
        <router-link to="register" class="is-tab navbar-item">Register</router-link>
      </div>
    </div>
  </div>
  </nav>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    data () {
      return {
        isAdminOpen: false
      }
    },
    computed: mapGetters(['isLogged', 'user', 'admin', 'employee']),
    methods: {
      logout () {
        this.$store.dispatch('logout');
        this.$router.push({name: 'welcome'});
      },
      back () {
        this.$router.go(-1);
      },
      returnNameRole () {
        if (this.isLogged) {
          return this.$store.state.user.user.role.name.toString();
        } else {
          return '';
        }
      },
      showAdminActions () {
        this.isAdminOpen = !this.isAdminOpen;
        return this.isAdminOpen;
      },
    },
  };
</script>
