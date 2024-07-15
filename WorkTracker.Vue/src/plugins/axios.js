// src/plugins/axios.js
import axios from 'axios';

// You can configure your axios instance here
const axiosInstance = axios.create({
  baseURL: 'https://localhost:7244/api',
  // other configuration
});

export default {
  install: (app) => {
    app.config.globalProperties.$axios = axiosInstance;
    // Alternatively, for Vue 3 Composition API
    app.provide('axios', axiosInstance);
  },
};