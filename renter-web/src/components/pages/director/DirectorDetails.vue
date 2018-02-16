<template>
  <section class="author-details">
    <b-tabs position="is-centered" expanded>
      <b-tab-item label="Info" >
        <div class="columns">
            <div class="column">
              <div class="author-profile">
                <img src="https://tinyfac.es/data/avatars/E0B4CAB3-F491-4322-BEF2-208B46748D4A-1000w.jpeg" alt="">
              </div>
            </div>
            <div class="column">
              <div class="asset-info">
                  <div class="name">
                    <h1>{{ author.name }} {{ author.surname}}</h1>
                  </div>

                  <div>
                    <p class="description">{{ author.description }}</p>
                  </div>
              </div>
            </div>
        </div>
      </b-tab-item>

      <b-tab-item label="Movies">
        <b-table
          :data="books"
          @click="handleClick"
          :striped="true"
        >
          <template slot-scope="props">
              <b-table-column label="Title">
                {{ props.row.title }}
              </b-table-column>
         </template>
        </b-table>
      </b-tab-item>
    </b-tabs>
  </section>

</template>

<script>
export default {
  data () {
    return {
      director: {}
    };
  },
  created () {
    this.$store.dispatch('fetchDirectorMovies', { directorID: this.$route.params.id }).then((response) => this.director);
  },
  methods: {
    handleClick (item) {
      this.$router.push({ path: '/movie/' + item.id });
    }
  }
};
</script>

<style scoped>
</style>
