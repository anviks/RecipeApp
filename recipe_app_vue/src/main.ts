import './assets/main.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';
import 'bootstrap';

import { createApp } from 'vue';
import { createPinia } from 'pinia';

import App from './App.vue';
import router from './router';
import servicesPlugin from '@/plugins/servicesPlugin';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(servicesPlugin);

app.mount('#app');
