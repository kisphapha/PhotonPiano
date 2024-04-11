import {createRouter , createWebHistory} from 'vue-router';
import HomePage from '../pages/HomePage.vue';
import AboutPage from '../pages/AboutPage.vue';
import StudentInfoPage from '../pages/StudentInfoPage.vue';
import ClassPage from '../pages/ClassPage.vue';
import AdminDashboard from '../pages/AdminDashboard.vue';

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
    path: '/student',
    name: 'StudentProfile',
    component: StudentInfoPage,
    props: true
  },
  {
    path: '/class',
    name: 'Class',
    component: ClassPage,
    props: true
  },
  {
    path: '/staff/',
    name: 'Dashboard',
    component: AdminDashboard,
    props: true
  }
];

const router = createRouter({
    history : createWebHistory("/"),
    routes
})

export default router