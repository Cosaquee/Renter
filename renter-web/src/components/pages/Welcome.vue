<template>
  <div>
    <div class="bg">
    <!-- <div id="demo">
      <p class="intro">RENTER</p>
      <p class="Two">First books and movies online library in your home.</p>
      <div class="col-lg-12 text-center">
        <button class="intro" v-on:click="goToRegisterPage()">
          <i class="fa fa-user-plus" aria-hidden="true"></i> Register
        </button>
        <button class="intro" v-on:click="goToLoginPage()">
          <i class="fa fa-user" aria-hidden="true"></i> Login
        </button>
      </div>
      <br>
      <p class="intro text-center">Most popular</p>
      <div class="col-lg-12 text-center">
        <nav class="level">
          <div class="level-item has-text-centered">
            <div>
              <p class="heading">Sopel</p>
              <p class="title"><img alt="Smiley face" style="float:right;width:170px;height:250px;"
                                    src="https://www.granice.pl/sys6/pliki/okladka_k/486bf2ca37e46567d9dc41c5830c8431.jpeg"></p>
            </div>
          </div>
          <div class="level-item has-text-centered">
            <div>
              <p class="heading">Lodowa Cytadela</p>
              <p class="title"><img alt="Smiley face" style="float:right;width:170px;height:250px;"
                                    src="http://ecsmedia.pl/c/lodowa-cytadela-b-iext43636295.jpg"></p>
            </div>
          </div>
          <div class="level-item has-text-centered">
            <div>
              <p class="heading">Wonder Women</p>
              <p class="title"><img alt="Smiley face" style="float:right;width:170px;height:250px;"
                                    src="https://i.ytimg.com/vi/95a2aqEAbo8/movieposter.jpg"></p>
            </div>
          </div>
          <div class="level-item has-text-centered">
            <div>
              <p class="heading">Star Wars: Last Jedi</p>
              <p class="title"><img alt="Smiley face" style="float:right;width:170px;height:250px;"
                                    src="https://i.redd.it/5rrni3c4hlcz.jpg"></p>
            </div>
          </div>
        </nav>
      </div>
    </div> -->
    <section class="header">
      <img src="../../../static/logo.png" alt="">
      <div class="description">
        <div class="description-desc">
          Renter - Best library system that you can find.
        </div>
      </div>
    </section>
    <section class="content">
    </section>
    <br>
  </div>
    <footer class="footer is-primary">
      <div class="container">
        <div class="columns">
          <div class="column has-text-centered">
            <div class="students">
              ©2018 PJWSTK
              <img alt="Smiley face" style="width:20px;height:20px;"
                   src=" https://www.cybsecurity.org/wp-content/uploads/2013/02/LOGO-PJWSTK.png">
              Kozakowski, Jarząbek, Wiszniewski, Przybyszewski.
            </div>
          </div>
          <div class="column has-text-centered">
            <img alt="Smiley face" style="width:80px;height:80px;"
                                  src="https://cdn.shopify.com/s/files/1/0824/8827/files/logo_205x@2x.png?v=1517040472">

          </div>
          <div class="column has-text-right">
            <div class="social">
              <a href="https://facebook.com/" class="link facebook" target="_parent"><span class="fa fa-facebook-square"></span></a>
              <a href="https://twitter.com/" class="link twitter" target="_parent"><span class="fa fa-twitter"></span></a>
              <a href="https://plus.google.com/" class="link google-plus" target="_parent"><span class="fa fa-google-plus-square"></span></a>
            </div>
          </div>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import axios from 'axios';
export default {
  data: function () {
    return {
      el: '#demo',
      show: false
    };
  },
  methods: {
    goToLoginPage () {
      this.$router.push({ path: '/login/' });
    },
    goToRegisterPage () {
      this.$router.push({ path: '/register/' });
    }
  },
  computed: {
    books () {
      axios.get('http://localhost:5000/api/book')
      .then((response) => {
        return response.data.length;
      });
    }
  }
};
</script>
<style scoped>

  .header {
    display: flex;
    justify-content: space-around;
  }

  .description {
    font-size: 4em;
  }

  .description-desc {
    margin-top: 10%;
  }
  students{
    top: 50%;
  }
  footer{
    background: white;
  }
  p.intro {
    text-align: center;
    font-size: 50px;
    font-smoothing: antialiased;
    overflow: hidden;
    color: #7957d5;
    margin: 1px;
  }
  p.Two {
    text-align: center;
    font-smoothing: antialiased;
    overflow: hidden;
    color: #7957d5;
    margin: 1px;
    font-size: 18px;
    font-family: Georgia, serif;
  }
  button.intro {
    color: white;
    font-size: 15px;
    font-weight: 600;
    background-color: #7957d5;
    margin: auto;
    width: 100px;
    height: 50px;
    border: #3B3561;
  }

  button.intro:hover {
    background-color: #56419b;
  }

  button.intro:active {
    background-color: #56419b;
  }

  .text-center {
    text-align: center;
  }

  body {
    background: white  no-repeat fixed center;
    background-size: cover;
    overflow: hidden;
  }

  .social {
    width: 100%;
    text-align: center;

  }

  .social .link {
    display: inline-block;
    vertical-align: middle;
    position: relative;
    width: 80px;
    height: 80px;
    border-radius: 50%;
    border: 2px dashed white;
    background-clip: content-box;
    padding: 10px;
    transition: .5s;
    color: #D7D0BE;
    margin-left: 15px;
    margin-right: 15px;
    font-size: 20px;
  }

  .social .link span {
    display: block;
    position: absolute;
    text-align: center;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  .social .link:hover {
    padding: 20px;
    color: white;
    margin-left: -5px;
    transform: translateX(10px) rotate(360deg);
  }

  .social .link.google-plus {
    background-color: tomato;
    color: white;
  }

  .social .link.twitter {
    background-color: #00ACEE;
    color: white;
  }

  .social .link.facebook {
    background-color: #3B5998;
    color: white;
  }
</style>
