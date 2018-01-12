<template>
  <div class="columns">
    <div class="column is-4 is-offset-4">
      <card title="Login" icon="sign-in">
        <form @submit="addAuthor">
          <b-field label="Name">
            <b-input
              type="text"
              name="sandel"
              required
              v-model="name"
              placeholder='Name'
            ></b-input>
          </b-field>

          <b-field label="Surname">
            <b-input
              type="text"
              v-model="surname"
              class="is-half  "
              required
              placeholder='Surname'
            ></b-input>
          </b-field>

          <b-field label="Description">
            <b-input
              type="textarea"
              v-model="description"
              class="is-half  "
              required
              placeholder='Description'
            ></b-input>
          </b-field>
          <div class="has-text-danger has-text-centered">{{error}}</div>
          <button type="submit" :class="['button', 'is-primary', 'is-fullwidth', {'is-loading': pending}]">Add</button>
        </form>
      </card>
    </div>
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
