import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; // Your Vue Router for routing
import store from './store'; // Your Vuex store for state management

// Importing MDB Vue UI Kit for styling
import 'mdb-vue-ui-kit/css/mdb.min.css';

// Import Vue3Toastify for notifications
import Vue3Toastify from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const app = createApp(App);

// Use your router and store
app.use(router);
app.use(store);

// Use Vue3Toastify with optional global configuration
app.use(Vue3Toastify, {
  autoClose: 5000, // Example global configuration: Auto-close notifications after 5000ms
});

// Mount your Vue app
app.mount('#app');
