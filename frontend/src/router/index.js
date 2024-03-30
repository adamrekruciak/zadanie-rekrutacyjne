import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import DocumentDetails from '../components/DocumentDetails.vue' // Import komponentu DocumentDetails

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/document/:id', // Ścieżka z parametrem :id
    name: 'DocumentDetails', // Nazwa trasy
    component: DocumentDetails, // Komponent DocumentDetails
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
