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
  import axios from 'axios';

  const UPLOAD_URL = 'http://localhost:4567/save_image';
  const BOOK_UPDATE_URL = 'http://localhost:5000/api/book/';

  export default {
    data () {
      return {
        files: []
      };
    },
    props: ['id', 'book'],
    methods: {
      upload: function () {
        axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';

        let formData = new FormData();
        formData.append('file', this.files[0]);

        axios.post(UPLOAD_URL, formData)
          .then((response) => {
            axios.post(`${BOOK_UPDATE_URL}cover`, {
              id: this.book.id,
              CoverURL: response.data.object_url,
              ResizedCoverURL: response.data.cover_url
            }, {
              headers: {
                'Authorization': 'Bearer ' + this.$store.getters.token
              }
            }).then((response) => {
              console.log(response);
            }).catch((error) => {
              console.log(error);
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
        width: auto;
    }
</style>
