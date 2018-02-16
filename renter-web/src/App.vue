<template>
  <div id="app">
    <navbar/>
    <div class="container">
      <router-view/>
    </div>
  </div>
</template>

<script>
  import Navbar from './components/partials/Navbar.vue';

  export default {
    name: 'app',
    components: {
      'navbar': Navbar,
    },
  };
</script>


<style scoped>
  .container {
    width: 100%;
    max-width: 800px;
  }

  @media only screen and (min-width: 500px) {
    .container {
      width: 100%;
      max-width: 500;
    }
  }

  @media only screen and (min-width: 700px) {
    .container {
      width: 100%;
      max-width: 700px;
    }
  }

  @media only screen and (min-width: 900px) {
    .container {
      width: 100%;
      max-width: 900px;
    }
  }

  @media only screen and (min-width: 1100px) {
    .container {
      width: 100%;
      max-width: 1800px;
    }
  }

</style>
