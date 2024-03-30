import {createRouter , createWebHistory} from 'vue-router';
import HomePage from '../pages/HomePage.vue';
import AboutPage from '../pages/AboutPage.vue';
import StudentInfoPage from '../pages/StudentInfoPage.vue'

// Define your routes
const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomePage
  },
  {
    path: '/about',
    name : 'About',
    component: AboutPage
  },
  {
    path: '/user/:userId',
    name: 'UserProfile',
    component: StudentInfoPage,
    props: true
  }
];

const router = createRouter({
    history : createWebHistory("/"),
    routes
})

export default router