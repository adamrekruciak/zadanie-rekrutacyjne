import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; 
import store from './store'; 
import 'mdb-vue-ui-kit/css/mdb.min.css';
import Vue3Toastify from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const app = createApp(App);
app.use(router);
app.use(store);
app.use(Vue3Toastify, {
  autoClose: 4000,
});

store.dispatch('initializeStore').then(() => {
  app.mount('#app');
});
