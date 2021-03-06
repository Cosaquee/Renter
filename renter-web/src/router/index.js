import Vue from 'vue';
import Router from 'vue-router';

import store from '@/store';

import Login from '@/components/pages/Login';
import Register from '@/components/pages/Register';
import Welcome from '@/components/pages/Welcome';
import Home from '@/components/pages/Home';
import Profile from '@/components/pages/user/Profile';
import Users from '@/components/pages/user/Users';

import AuthorForm from '@/components/pages/author/AuthorForm';
import Author from '@/components/pages/author/Author';
import AuthorDetails from '@/components/pages/author/AuthorDetails';

import BookDetails from '@/components/pages/book/BookDetails';
import BookForm from '@/components/pages/book/BookForm';
import Book from '@/components/pages/book/Book';
import BookOrders from '@/components/pages/book/Orders';
import BookHistory from '@/components/pages/book/BookRentHistory';

import Category from '@/components/pages/category/Category';
import Movie from '@/components/pages/movie/Movie';
import MovieDetails from '@/components/pages/movie/MovieDetails';
import MovieHistory from '@/components/pages/movie/MovieRentHistory';
import MovieForm from '@/components/pages/movie/MovieForm';

import Director from '@/components/pages/director/Director';
import DirectorDetails from '@/components/pages/director/DirectorDetails';

Vue.use(Router);

const router = new Router({
  linkActiveClass: 'is-active',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: { auth: true }
    },
    {
      path: '/profile',
      name: 'profile',
      component: Profile,
      meta: { auth: true },
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
      meta: { auth: true, adminOnly: true },
    },
    {
      path: '/movie',
      name: 'Movie',
      component: Movie,
      meta: { auth: true },
    },
    {
      path: '/movie/create',
      name: 'MovieForm',
      component: MovieForm,
      meta: { auth: true },
    },
    {
      path: '/movie/history/:id',
      name: 'MovieHistory',
      component: MovieHistory,
      meta: { auth: true, employeeOnly: true },
    },
    {
      path: '/movie/:id',
      name: 'MovieID',
      component: MovieDetails,
      meta: { auth: true },
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/author/create',
      name: 'AuthorForm',
      component: AuthorForm,
      meta: { auth: true, employeeOnly: true },
    },
    {
      path: '/author/',
      name: 'Author',
      component: Author,
      meta: { auth: true },
    },
    {
      path: '/author/:id',
      component: AuthorDetails,
      meta: { auth: true },
    },
    {
      path: '/book/',
      name: 'Book',
      component: Book,
      meta: { auth: true }
    },
    {
      path: '/book/create',
      component: BookForm,
      name: 'BookForm',
      meta: { auth: true, employeeOnly: true }
    },
    {
      path: '/book/orders',
      component: BookOrders,
      meta: { auth: true, employeeOnly: true }
    },
    {
      path: '/book/history/:isbn',
      component: BookHistory,
      meta: { auth: true, employeeOnly: true }
    },
    {
      path: '/book/:id',
      component: BookDetails,
      meta: { auth: true }
    },
    {
      path: '/category',
      component: Category,
      meta: { auth: true }
    },
    {
      path: '/director',
      component: Director,
      meta: { auth: true }
    },
    {
      path: '/director/:id',
      component: DirectorDetails,
      meta: { auth: true }
    },
  ]
});

router.beforeEach((to, from, next) => {
  if (to.meta.auth) {
    if (!store.getters.isLogged) {
      next({
        path: '/welcome',
      });
    } else {
      if (store.getters.admin) {
        next();
      } else if (to.meta.employeeOnly) {
        if (store.getters.employee) {
          next();
        } else {
          next({
            path: '/'
          });
        }
      } else if (Object.getOwnPropertyNames(to.meta).length === 1) {
        next();
      }
    }
  } else {
    next();
  }
});

export default router;
