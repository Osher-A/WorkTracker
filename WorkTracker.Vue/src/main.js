import './assets/main.css'
import '@fortawesome/fontawesome-free/css/all.css';
import '@fortawesome/fontawesome-free/js/all.js';
import axiosPlugin from './plugins/axios'; // Import the axios plugin
import MainLayout from './layouts/MainLayout.vue';
import TitleHeader from './components/TitleHeader.vue';

import { createApp } from 'vue'
import App from './App.vue'

const app = createApp(App);

import router from './router';
app.use(router);
app.use(axiosPlugin); // Use the axios plugin

app.component('MainLayout', MainLayout);
app.component('TitleHeader', TitleHeader);

app.mount('#app')
