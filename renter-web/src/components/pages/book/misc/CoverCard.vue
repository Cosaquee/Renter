<template>
  <div class="card">
    <div @click="showDetails(collection.id)" class="card-content">
      <img :src="collection.resizedCoverURL" :alt="collection.title">
      <h4 class="card-title">{{ collection.title }}</h4>
      <p>{{ collection.author.name }} {{ collection.author.surname }}</p>
    </div>
  </div>
</template>

<script>
export default {
  props: ['collection'],
  name: 'card',
  methods: {
    showDetails (id) {
      this.$router.push({ path: '/book/' + id });
    }
  }
};
</script>

<style scoped>
</style>
