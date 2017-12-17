import Vue from 'vue';
import Router from 'vue-router';
import store from '@/store';
import Login from '@/components/pages/Login';
import Register from '@/components/pages/Register';
import Welcome from '@/components/pages/Welcome';
import Home from '@/components/pages/Home';
import Profile from '@/components/pages/Profile';
import Users from '@/components/pages/Users';

Vue.use(Router);

const router = new Router({
  linkActiveClass: 'is-active',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {auth: true}
    },
    {
      path: '/profile',
      name: 'profile',
      component: Profile,
      meta: {auth: true},
    },
    {
      path: '/welcome',
      name: 'welcome',
      component: Welcome,
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/users',
      name: 'users',
      component: Users,
      meta: { auth: true, requiresAdministrator: true },
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    }
  ]
});
router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.auth)) {
    if (!store.getters.isLogged) {
      next({
        path: '/welcome',
      });
    } else {
      if (to.fullPath === '/users') {
        if (store.getters.admin) {
          next();
        } else {
          next({
            path: '/',
          })
        }
      }
      next();
    }
  } else {
    next();
  }
});

export default router;
