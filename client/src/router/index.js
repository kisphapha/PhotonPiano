import {createRouter , createWebHistory} from 'vue-router';
import HomePage from '../pages/HomePage.vue';
import AboutPage from '../pages/AboutPage.vue';
import StudentInfoPage from '../pages/StudentInfoPage.vue';
import ClassPage from '../pages/ClassPage.vue';
import AdminDashboard from '../pages/StaffPages/AdminDashboard.vue';
import AdminClassPage from '../pages/StaffPages/AdminClassPage.vue';
import AdminInstructorsPage from '../pages/StaffPages/AdminInstructorsPage.vue';
import AdminStaffPage from '../pages/StaffPages/AdminStaffPage.vue';
import AdminStudentListPage from '../pages/StaffPages/AdminStudentListPage.vue';
import AdminStudentReClassPage from '../pages/StaffPages/AdminStudentReClassPage.vue';
import EntranceTestArrangePage from '../pages/StaffPages/EntranceTestArrangePage.vue';
import EntranceTestRegistrationsPage from '../pages/StaffPages/EntranceTestRegistrationsPage.vue';
import SchedulePage from '../pages/StaffPages/SchedulePage.vue';
import SettingsPage from '../pages/StaffPages/SettingsPage.vue';
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
    path: '/manage/',
    name: 'Dashboard',
    component: AdminDashboard,
    props: true
  },
  {
    path: '/manage/class',
    name: 'AdminClass',
    component: AdminClassPage,
    props: true
  },
  {
    path: '/manage/instructors',
    name: 'AdminInstructors',
    component: AdminInstructorsPage,
    props: true
  },
  {
    path: '/manage/staff',
    name: 'AdminStaff',
    component: AdminStaffPage,
    props: true
  },
  {
    path: '/manage/student/list',
    name: 'AdminStudentList',
    component: AdminStudentListPage,
    props: true
  },
  {
    path: '/manage/student/re-class',
    name: 'AdminStudentReClass',
    component: AdminStudentReClassPage,
    props: true
  },
  {
    path: '/manage/entrance-test/arrange',
    name: 'EntranceTestArrange',
    component: EntranceTestArrangePage,
    props: true
  },
  {
    path: '/manage/entrance-test/registration',
    name: 'EntranceTestRegistrations',
    component: EntranceTestRegistrationsPage,
    props: true
  },
  {
    path: '/manage/schedule',
    name: 'Schedule',
    component: SchedulePage,
    props: true
  },
  {
    path: '/manage/settings',
    name: 'Settings',
    component: SettingsPage,
    props: true
  }
];

const router = createRouter({
    history : createWebHistory("/"),
    routes
})

export default router