<template>
    <form enctype="multipart/form-data">
        <div class="modal-card">
            <header class="modal-card-head">
                <p class="modal-card-title">Upload cover photo</p>
            </header>
            <section class="modal-card-body">

              <b-field>
                <b-upload v-model="files">
                  <a class="button is-primary">
                      <b-icon icon="upload"></b-icon>
                      <span>Click to add cover</span>
                  </a>
                </b-upload>
                <div v-if="files && files.length">
                  <span class="file-name">
                      {{ files[0].name }}
                  </span>
                </div>
              </b-field>

            </section>
            <footer class="modal-card-foot">
                <button class="button" type="button" @click="$parent.close()">Close</button>
                <button type="button" class="button" @click="upload">Upload</button>
            </footer>
        </div>
    </form>
</template>

<script>
  export default {
    data () {
      return {
        files: []
      };
    },
    props: ['id', 'book'],
    methods: {
      upload () {
        let formData = new FormData();
        formData.append('file', this.files[0]);

        this.$store.dispatch('uploadBookCover', {
          formData: formData
        }).then((response) => {
          this.$store.dispatch('updateCover', {
            id: this.book.id,
            coverURL: response.data.object_url,
            resizedCoverURL: response.data.cover_url
          });
        }).catch((error) => {
          console.log(error);
        });
      }
    }
  };
</script>

<style scoped>
    .modal-card {
        width: 100%;
    }
</style>
