import { createApp } from 'vue'
import './index.css'
import App from './App.vue'
import router from './router'
import mitt from 'mitt'


const app = createApp(App);

const eventBus = mitt();

app.use(router);
app.provide('eventBus', eventBus);
app.mount('#app');