import { createRouter, createWebHashHistory } from 'vue-router';

const router = createRouter({
    history: createWebHashHistory(),
    routes: [
      {
        path: '/list',
        name: 'Grid',
        component: () => import('./views/GridView.vue'),
        meta: {
          browserTabTitle: 'Grid'
        },
      },
  
      {
        path: '/',
        name: 'Home',
        component: () => import('./views/AddWork.vue'),
        meta: {
          browserTabTitle: 'Home'
        },
      },

      {
        path: '/edit/:id',
        name: 'Edit',
        component: () => import('./views/EditWork.vue'),
        meta: {
          browserTabTitle: 'Edit'
        },
        props: true
      },
      {
        path: '/clients',
        name: 'ClientManager',
        component: () => import('./views/ClientManager.vue'),
      }
    ]
});

export default router;
          
        