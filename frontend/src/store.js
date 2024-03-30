import { createStore } from 'vuex';
import axios from 'axios';

export default createStore({
  state: {
    documents: [],
    isDataImported: false,
  },
  mutations: {
    setDocuments(state, documents) {
      state.documents = documents;
    },
    setDataImported(state, value) {
      state.isDataImported = value;
    },
  },
  actions: {
    async fetchDocuments({ commit }) {
      try {
        const response = await axios.get('https://localhost:7107/Document/documents'); 
        commit('setDocuments', response.data);
      } catch (error) {
        console.error('Error fetching documents:', error);
      }
    },
  },
});