<template>
  <div class="movie">
    <img @click="change(movie.id)" :src="movie.thumbnail"></img>
    <p class="title">{{ movie.title }}</p>
    <p class="info">{{ movie.director.name }} {{ movie.director.surname }}</p>
  </div>
</template>

<script>
export default {
  props: ['movie'],
  methods: {
    change (id) {
      this.$router.push({ path: '/movie/' + id });
    }
  }
};
</script>

<style scoped>
  .movie {
    background: rgba(#f5f5f5, 0.47);
    padding: 10px;
    margin: 1em 0 1em;
    cursor: pointer;
    transition: all 100ms ease-in-out;
    display: inline-block;
    text-align: center;
    width: 250px;
  }

  .movie:hover {
    transform: translateY(-0.5em);
    background: #EBEBEB;
  }

  .title {
    font-size: 1.5em;
    margin-top: 5px;
    margin-bottom: 0px;
  }

  .info {
    margin-top: -20px;
  }
</style>
