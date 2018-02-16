// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import App from './App';
import router from './router';
import { sync } from 'vuex-router-sync';
import store from './store';
import Buefy from 'buefy';
import './main.scss';

import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import locale from 'element-ui/lib/locale/lang/en'

import AtComponents from 'at-ui';
import 'at-ui-style';

import Rate from 'vue-rate';
import { TableComponent, TableColumn } from 'vue-table-component';

Vue.use(ElementUI, { locale });
Vue.component('table-component', TableComponent);
Vue.component('table-column', TableColumn);
Vue.component('Rate', Rate);
Vue.use(AtComponents);

Vue.use(Buefy, {
  defaultIconPack: 'fa'
});

Vue.config.productionTip = false;

sync(store, router);

/* eslint-disable no-new */
window.a = new Vue({
  el: '#app',
  store,
  router,
  template: '<App/>',
  components: { App }
});
// unsync();
