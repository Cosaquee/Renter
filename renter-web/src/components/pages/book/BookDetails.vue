<template>
  <section class="book-details">
    <section class="book-menu">
      <div class="container">
        <button class="button is-outlined is-medium is-info" @click="prompt">
          Edit
        </button>
        <button class="button is-outlined is-primary is-medium" @click="isComponentModalActive = true">
          Add cover
        </button>

        <b-modal :active.sync="isComponentModalActive" has-modal-card>
            <BookCoverModal v-bind="{ book: this.book }"></BookCoverModal>
        </b-modal>
      </div>
    </section>
    <div class="columns">
      <div class="column">
        <div class="book-cover">
          <img :src="book.coverURL" alt="Book Cover">
        </div>
      </div>
      <div class="column">
        <div class="book-info">
          <h2 class="book-title">{{ book.title }}</h2>
          <p class="book-description">{{ book.description }}</p>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
  import axios from 'axios';
  import BookCoverModal from './modals/BookCoverModal';

  export default {
    data () {
      return {
        book: {},
        isComponentModalActive: false
      };
    },
    components: {
      BookCoverModal
    },
    computed: {
      rate: function () {
        return this.book.bookRatings[0].rate / 2;
      },
      admin: function () {
        return this.$store.getters.admin;
      }
    },
    methods: {
      prompt: function () {
        this.$dialog.prompt({
          message: `What's your name?`,
          inputAttrs: {
            placeholder: 'e.g. Walter',
            maxlength: 10
          },
          onConfirm: (value) => this.$toast.open(`Your name is: ${value}`)
        });
      },
      addCover: function () {

      }
    },
    created: function () {
      axios.get('http://localhost:5000/api/book/' + this.$route.params.id, {
        headers: {
          'Authorization': 'Bearer ' + this.$store.getters.token
        }
      }).then((response) => {
        this.book = response.data;
      });
    }
  };
</script>
