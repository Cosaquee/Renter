<template>
  <div class="book">
    <img @click="change(book.id)" :src="book.resizedCoverURL"></img>
    <p class="title">{{ book.title }}</p>
    <p class="info">{{ book.author.name }} {{ book.author.surname }}</p>
  </div>
</template>

<script>
export default {
  props: ['book'],
  methods: {
    change (id) {
      this.$router.push({ path: '/book/' + id });
    }
  }
};
</script>

<style scoped>
  .book {
    background: rgba(#f5f5f5, 0.47);
    padding: 10px;
    margin: 1em 0 1em;
    cursor: pointer;
    transition: all 100ms ease-in-out;
    display: inline-block;
    text-align: center;
    width: 250px;
  }

  .book:hover {
    transform: translateY(-0.5em);
    background: #EBEBEB;
  }

  .title {
    font-size: 1.5em;
  }

  .info {
    margin-top: -20px;
  }
</style>
