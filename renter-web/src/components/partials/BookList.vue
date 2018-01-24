<template>
  <section>
    <h3 class="title">Our latest books</h3>

    <div class="latest-books container">
      <img @click="change(book.id)" class="book" v-for="book in latestBooks" :src="book.resizedCoverURL"></img>
      <div v-for="book in latestBooks" class="book-stars">
        <Rate :length="5"></Rate>
      </div>
    </div>
  </section>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
  data () {
    return {

    };
  },
  methods: {
    change (id) {
      this.$router.push({ path: '/book/' + id });
    }
  },
  created () {
    this.$store.dispatch('getLatestBooks');
  },
  components: {
  },
  computed: mapGetters(['latestBooks'])
};
</script>

<style scoped>
  .latest-books {
    background: hsla(0, 0%, 92%, 0.5);
  }

 .book {
   padding: 1em;
   background: rgba(#f5f5f5, 0.33);
   cursor: pointer;
   margin-bottom: -1em;
   transition: all 100ms ease-in-out;
   display: inline-block;
   text-align: center;
   max-width: 250px;
   min-width: 250px;
 }

 .book-stars {
   display: inline-block;
   text-align: center;
   max-width: 250px;
   min-width: 250px;
 }

 .book:hover {
   transform: translateY(-0.5em);
   background: #EBEBEB;
 }

 .title {
   text-align: center;
   font-size: 3em;
 }
</style>
