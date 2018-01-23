<template>
    <form enctype="multipart/form-data">
        <div class="modal-card">
            <header class="modal-card-head">
                <p class="modal-card-title">Upload cover photo</p>
            </header>
            <section class="modal-card-body">

              <b-field>
                <b-upload v-if="files.length === 0" v-model="files"  drag-drop>
                  <section class="section">
                    <div class="content has-text-centered">
                      <p>
                        <b-icon
                          icon="upload"
                          size="is-large">
                        </b-icon>
                      </p>
                      <p>Drop your files here or click to upload</p>
                    </div>
                </section>
                </b-upload>
                <div class="filename" v-if="files && files.length">
                      <p>{{ files[0].name }}</p>
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
    props: ['id', 'movie'],
    methods: {
      upload () {
        let formData = new FormData();
        formData.append('file', this.files[0]);
        this.$store.dispatch('uploadMovieCover', {
          formData: formData
        }).then((response) => {
          this.$store.dispatch('uploadCover', {
            movieID: this.movie.id,
            coverURL: response.data.object_url
          });
        });
      }
    }
  };
</script>

<style scoped>
  .upload {
    text-align: center;
  }

  .modal-card-head {
    text-align: center;
  }

  .modal-card {
      margin: 0 auto;
      width: auto ;
  }

  .button {
    width: 12em;
  }
  .icon {
    padding-right: 1.5em;
  }
  .field {
    text-align: center;
  }
</style>
