<template>
  <div class="card">
    <div @click="showDetails(collection.id)" class="card-content">
      <img :src="collection.thumbnail" :alt="collection.title">
      <p class="card-author">{{ collection.director.name }} {{ collection.director.surname }}</p>
      <h4 class="card-title">{{ collection.title }}</h4>

    </div>
  </div>
</template>

<script>
export default {
  props: ['collection'],
  name: 'card',
  methods: {
    showDetails (id) {
      this.$router.push({ path: '/movie/' + id });
    }
  }
};
</script>

<style scoped>

.card-content {
  /* height: 300px; */
}

.card-title {
  /* font-size: 2vh; */
}

.card-author {
  font-size: 1em;
}

@media only screen and (min-width: 500px) {
  .card-title {
    font-size: 2vh;
  }
}

@media only screen and (min-width: 700px) {
  .card-title {
    font-size: 1em;;
  }

  .card {
    height: 300px;
  }
}

@media only screen and (min-width: 900px) {
  .card-title {
    font-size: 1vh;
  }
}

@media only screen and (min-width: 1100px) {
  .card-title {
    font-size: 2vh;
  }
}

@media only screen and (min-width: 1500px) {
  .card-title {
    font-size: 1.5vh;
  }
}

</style>
