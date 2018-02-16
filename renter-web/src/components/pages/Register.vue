<template>
    <div class="register-form">
      <at-card class="register-card" style="width: 300px;">
        <h2 slot="title">Register</h2>
        <div>
          <at-input size="large" v-model="email" :status="emailStatus" placeholder="Email" icon="mail"></at-input>
          <at-input size="large" v-model="name" placeholder="Name"></at-input>
          <at-input size="large" v-model="surname" placeholder="Surname"></at-input>
          <at-input size="large" v-model="password" :status="passwordStatus"  type="password" icon="unlock" placeholder="Password"></at-input>
          <at-input size="large" v-model="confirmPassword" :status="passwordConfirmStatus" type="password" icon="unlock" placeholder="Confirm password"></at-input>
          <at-button @click="register" class="register-button" size="large" type="primary" hollow>Register</at-button>
        </div>
      </at-card>
    </div>

  </div>

</template>

<script>
  import Card from '../layout/Card.vue';

  export default {
    data: function () {
      return {
        email: '',
        name: '',
        surname: '',
        password: '',
        confirmPassword: ''
      };
    },
    computed: {
      emailStatus () {
        let pattern = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
        if (this.email !== '') {
          if (this.email.match(pattern)) {
            return 'success';
          } else {
            return 'error';
          }
        }
      },
      passwordStatus () {
        if (this.password !== '') {
          if (this.password.length < 6) {
            return 'error';
          } else {
            return 'success';
          }
        }
      },
      passwordConfirmStatus () {
        if (this.confirmPassword !== '') {
          if (this.confirmPassword.length < 6 || this.confirmPassword !== this.password) {
            return 'error';
          } else {
            return 'success';
          }
        }
      }
    },
    methods: {
      register (e) {
        e.preventDefault();
        this.pending = true;
        this.$store.dispatch('registerUser', this)
          .then(() => {
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

<style scoped>
  .error {
    color: red;
  }
  .at-input {
    padding: 3px;
  }

  .password-section {
    display: flex;
    justify-content: space-between;
  }

  .password-section-button {
    margin-top: 2%;
    margin-left: 5px;
  }

  .at-card__body {
    padding-top: 1px;
  }

  .register-card {
    text-align: center;
  }

  .register-form {
    display: flex;
    justify-content: center;
    padding-top: 150px;
  }

  .register-button {
    margin-top: 3px;
    margin-bottom: -11px;
    width: 100%;
  }
</style>
