<template>
  <div class="card">
  <div @click="showDetails(collection.id)" class="card-content">
    <img :src="collection.resaizedCoverURL" :alt="collection.title">
    <h4 class="title">{{ collection.title }}</h4>
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
.card {
   background: rgba(#f5f5f5, 0.47);
   padding: 10px;
   margin: 0 0 1em;
   cursor: pointer;
   transition: all 100ms ease-in-out;
   display: inline-block;
   text-align: center;
   max-width: 250px;
   min-width: 250px;
 }
 .card:hover {
   transform: translateY(-0.5em);
   background: #EBEBEB;
 }
 img {
   display: block;
   margin: auto;
 }

 .title {
   font-size: 1.5em;
   margin-bottom: 0;
 }
</style>
