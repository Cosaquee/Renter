<template>
  <section>
    <div class="new-movie">
      <at-card class="register-card" style="width: 300px;">
        <h1 slot="title">Add new movie</h1>
        <div>
          <at-alert v-if="error" message="Somenthing went wrong, please check data you provided" type="error"></at-alert>
          <at-input size="large" v-model="title" placeholder="Title"></at-input>
          <at-input size="large" v-model="description" type="textarea" placeholder="Description"></at-input>
          <el-input-number class="duration" v-model="duration" @change="handleChange" :min="1" :max="300"></el-input-number>

          <at-select class="select" filterable size="large" v-model="selectedCategory" placeholder="Select category">
            <at-option v-for="category in categories" :value="category.name">{{ category.name }}</at-option>
          </at-select>

          <at-select class="select" filterable size="large" v-model="selectedDirector" placeholder="Select director">
            <at-option v-for="category in directors" :value="category.name">{{ category.name }}</at-option>
          </at-select>

          <at-button @click="addMovie" class="register-button" size="large" type="primary" hollow>Add</at-button>
        </div>
      </at-card>
    </div>
  </section>
</template>

<script>
export default {
  data () {
    return {
      title: '',
      description: '',
      selectedCategory: '',
      selectedDirector: '',
      duration: '',
      error: false
    };
  },
  created () {
    this.$store.dispatch('fetchAllDirectors');
    this.$store.dispatch('getCategories');
  },
  computed: {
    directors () {
      return this.$store.getters.directors.map((director) => {
        return { name: director.name + ' ' + director.surname };
      });
    },
    categories () {
      return this.$store.getters.categories.map((category) => {
        return { name: category.name };
      });
    },
  },
  methods: {
    handleChange (value) {
      this.duration = value;
    },
    addMovie () {
      this.loading = true;
      if (this.selectedDirector === '' || !this.selectedCategory || this.title === '' || this.description === '') {
        this.error = true;
      };
      let directorID;

      let name = this.selectedDirector.split(' ')[0];
      let surname = this.selectedDirector.split(' ')[1];

      this.$store.getters.directors.map((director) => {
        if (director.name === name && director.surname === surname) {
          directorID = director.id;
        };
      });

      let selectedCategory = this.selectedCategory;

      // TODO: reformat to anonymous function
      const cat = this.$store.getters.categories.find(function (category) { return category.name === selectedCategory; });
      console.log(this.duration);

      this.$store.dispatch('addMovie', {
        title: this.title,
        directorID: directorID,
        description: this.description,
        categoryID: cat.id,
        duration: this.duration
      }).then((response) => {
        this.loading = false;
        this.$router.push({ path: '/movie' });
      }).catch(() => {
        this.loading = false;
        this.error = true;
      });
    }
  }
};
</script>

<style scoped>
  .error {
    color: red;
  }
  .register-card {
    text-align: center;
  }

  .new-movie {
    display: flex;
    justify-content: center;
    padding-top: 150px;
  }

  .register-button {
    margin-top: 3px;
    margin-bottom: -11px;
    width: 100%;
  }

  .at-input {
    padding: 3px;
  }

  .select {
    padding: 2px;
  }

  .duration {
    width: 100%;
  }
</style>
