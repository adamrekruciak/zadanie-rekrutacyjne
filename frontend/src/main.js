import { createApp } from 'vue';
import App from './App.vue';
import router from './router'; // Twój Vue Router do routingu
import store from './store'; // Twój Vuex store do zarządzania stanem

// Importowanie MDB Vue UI Kit do stylizacji
import 'mdb-vue-ui-kit/css/mdb.min.css';

// Import Vue3Toastify do powiadomień
import Vue3Toastify from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

const app = createApp(App);

// Użyj swojego routera i magazynu
app.use(router);
app.use(store);

// Użyj Vue3Toastify z opcjonalną globalną konfiguracją
app.use(Vue3Toastify, {
  autoClose: 5000, // Przykładowa globalna konfiguracja: Automatyczne zamykanie powiadomień po 5000ms
});

// Inicjalizacja stanu store z localStorage
store.dispatch('initializeStore').then(() => {
  // Montuj aplikację Vue po zakończeniu inicjalizacji stanu
  app.mount('#app');
});
