<template>
    <section class="login">
      <div class="login-form">
        <img src="../../../static/logo.png" alt="">
        <p class="error">{{ error }}</p>
          <form>
            <at-input size="large" v-model="username" :status="emailStatus" placeholder="Email" icon="mail"></at-input>
            <at-input size="large" v-model="password" :status="passwordStatus" type="password" placeholder="Password"></at-input>
            <at-button @click="makeLogin" size="large" type="primary" hollow>Login</at-button>
          </form>
      </div>
    </section>
  </div>

</template>

<script>
  import Card from '../layout/Card.vue';

  export default {
    data: function () {
      return {
        username: '',
        password: '',
        error: '',
        pending: false,
      };
    },
    methods: {
      makeLogin (e) {
        e.preventDefault();
        this.pending = true;
        if (this.username === '' || this.password === '') {
          this.error = 'Email or password cannot be empty.';
        }
        if (this.error === '') {
          this.$store.dispatch('login', this)
            .then(() => {
              this.pending = false;
              this.$router.push({name: 'home'});
            })
            .catch((error) => {
              this.pending = false;
              if (error.message === 'Request failed with status code 404') {
                this.error = 'Bad email or password combination.';
              } else {
                this.error = 'Something went really wrong. We have dispatched our special forces to fix everything. Please try again!';
              }
            });
        }
      },
    },
    computed: {
      emailStatus () {
        let pattern = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
        if (this.username !== '') {
          if (this.username.match(pattern)) {
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
      }
    },
    components: {
      'card': Card,
    },
  };
</script>

<style scoped>
  .login  {
    text-align: center;
  }

  .login-form {
    width: 20%;
    margin: 0 auto;
  }

  .at-input {
    padding: 3px;
  }

  .error {
    color: red;
  }
</style>
