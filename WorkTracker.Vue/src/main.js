import './assets/main.css'
import '@fortawesome/fontawesome-free/css/all.css';
import '@fortawesome/fontawesome-free/js/all.js';
import axiosPlugin from './plugins/axios'; // Import the axios plugin
import MainLayout from './layouts/MainLayout.vue';
import TitleHeader from './components/TitleHeader.vue';
import { createPinia } from 'pinia';

import { createApp } from 'vue'
import App from './App.vue'

// import Bootstrap and BootstrapVue CSS files
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css'

// Import individual components and plugins from BootstrapVueNext
import { BOverlay, BSpinner } from 'bootstrap-vue-next'
const app = createApp(App);

import router from './router';
app.use(router);
app.use(axiosPlugin); // Use the axios plugin
app.use(createPinia());

// Register individual components globally
app.component('BOverlay', BOverlay);
app.component('BSpinner', BSpinner);

app.component('MainLayout', MainLayout);
app.component('TitleHeader', TitleHeader);

app.mount('#app')
