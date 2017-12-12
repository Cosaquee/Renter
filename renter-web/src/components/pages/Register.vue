<template>
  <div class="columns">
    <div class="column is-4 is-offset-4">
      <card title="Register" icon="user-plus">
        <form @submit="makeRegister">
          <b-field label="Username">
            <b-input
              type="text"
              v-model="username"
              maxlength="30"
              required
              :has-counter="true"
            ></b-input>
          </b-field>

          <b-field label="Email">
            <b-input
              type="email"
              icon="envelope"
              required
              v-model="email"
            ></b-input>
          </b-field>

          <div class="columns">
            <div class="column">
              <b-field label="Password">
                <b-input
                  type="password"
                  v-model="password"
                  class="is-half"
                  icon="key"
                  minlength="6"
                  required
                  maxlength="20"
                  :has-counter="false"
                  password-reveal
                ></b-input>
              </b-field>
            </div>
            <div class="column">
              <b-field label="Confirm Password">
                <b-input
                  type="password"
                  v-model="cpassword"
                  icon="key"
                  minlength="6"
                  required
                  maxlength="20"
                  :has-counter="false"
                  password-reveal
                ></b-input>
              </b-field>
            </div>
          </div>
          <div class="has-text-danger has-text-centered">{{error}}</div>
          <button type="submit" :class="['button', 'is-primary', 'is-fullwidth', {'is-loading': pending}]">Register</button>
        </form>
      </card>
    </div>
  </div>

</template>

<script>
  import Card from '../layout/Card.vue';

  export default {
    data: function () {
      return {
        username: '',
        email: '',
        password: '',
        cpassword: '',
        pending: false,
        error: '',
      };
    },
    methods: {
      makeRegister (e) {
        e.preventDefault();
        console.log(this.password);
        console.log(this.cpassword);
        this.pending = true;
        this.$store.dispatch('registerUser', this)
          .then(() => {
            console.log('Then register');
            this.pending = false;
            this.$router.push({name: 'login'});
          })
          .catch(err => {
            this.error = err;
            this.pending = false;
          });
      },
    },
    components: {
      'card': Card,
    },
  };
</script>
