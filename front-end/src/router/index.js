import Vue from 'vue'
import VueRouter from 'vue-router'
// import HomeView from '../views/HomeView.vue'
import StudentsView from '../views/StudentsView.vue';
import CreateStudentsView from '../views/CreateStudentsView.vue';

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'StudentsView',
    component: StudentsView
  },
  {
    path: '/create',
    name: 'CreateStudentsView',
    component: CreateStudentsView
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
