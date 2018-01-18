<template>
  <section class="user-panel">
    <b-loading :active.sync="loading" :canCancel="false"></b-loading>
    <section class="user-menu">
      <div v-if="admin" class="menu">
        <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">Add avatar</button>
        <button class="button is-outlined is-medium is-info" @click="prompt">Edit</button>
        <a class="button is-danger is-outlined is-medium" @click="deleteUser">Delete</a>
      </div>

      <div v-if="normal_user" class="menu">
        <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">Add avatar</button>
      </div>
      <b-modal :active.sync="isComponentModalActive" has-modal-card>
        <AvatarModal v-bind="{ user: this.user }"></AvatarModal>
      </b-modal>

    </section>

    <div class="columns">
      <div class="column">
        <div class="user-avatar">
          <img :src="user.provileAvatar" alt="User avatar">
        </div>
        <div class="user-info">
          <p>{{ user.name }} {{ user.surname }}</p>
          <p>{{ user.email }}</p>
          <p>Books read: {{ booksRead.length }}</p>
          <p>Movies watched: {{ moviesRented.length }}</p>
        </div>
      </div>

      <div class="column is-two-thirds">
        <div class="user-rented-books">

          <table-component
            :data="rentedBooks"
            @rowClick="handleClick"

            sort-order="desc">

            <table-column show="book.title" label="Title"></table-column>
            <table-column label="Rent date">
              <template slot-scope="row">
                {{ new Date(row.from).toLocaleDateString("pl-PL") }}
              </template>
            </table-column>
            <table-column label="Return date">
              <template slot-scope="row">
                {{ new Date(row.to).toLocaleDateString("pl-PL") }}
              </template>
            </table-column>
            <table-column label="received?">
              <template slot-scope="row">
                {{ row.received ? 'Owned' : 'Ready to pickup' }}
              </template>
            </table-column>
          </table-component>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
  import { mapGetters } from 'vuex';
  import AvatarModal from './modals/AvatarModal';

  export default {
    data () {
      return {
        loading: false,
        isComponentModalActive: false,
        booksRead: [],
        moviesRented: []
      };
    },
    components: {
      AvatarModal
    },
    created () {
      this.$store.dispatch('getRentedBooks');
    },
    methods: {
      prompt () {

      },
      deleteUser () {

      },
      handleClick () {

      },
    },
    computed: mapGetters(['user', 'admin', 'normal_user', 'rentedBooks']),
  };
</script>

<style scoped>
  .user-avatar {
    width: 80%;
    margin: 0 auto;
  }

  .columns {
    padding: 2em;
  }

  .user-info {
    text-align: left;
    padding-left: 10%;
    font-size: 1.5em;
    padding-top: 2em;
  }
</style>
