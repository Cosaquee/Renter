<template>
  <div class="register-form">
    <at-card class="register-card" style="width: 300px;">
      <h1 slot="title">Add new author</h1>
      <div>
        <at-input size="large" v-model="name" placeholder="Name"></at-input>
        <at-input size="large" v-model="surname" placeholder="Surname"></at-input>
        <at-input size="large" v-model="description" placeholder="Description"></at-input>
        <at-button @click="addAuthor" class="register-button" size="large" type="primary" hollow>Add</at-button>
      </div>
    </at-card>
  </div>
</template>

<script>
import axios from 'axios';
export default {
  data () {
    return {
      name: '',
      surname: '',
      error: '',
      description: '',
      pending: false
    };
  },
  methods: {
    addAuthor (event) {
      event.preventDefault();
      this.pending = true;
      axios.post('http://localhost:5000/api/author', {
        'surname': this.surname,
        'name': this.name,
        'description': this.description
      }, {
        headers: {
          'Authorization': 'Bearer ' + this.$store.getters.token
        }
      }).then((response) => {
        this.pending = false;
        this.$router.push({ path: '/author/' + response.data.id });
      });
    }
  }
};
</script>

<style>
  .register-card {
    background-color: rgb(234, 235, 237);
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
  }

  .at-input {
    padding: 3px;
  }

  h1 {
    font-size: 2.5em;
  }

</style>
