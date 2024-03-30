import { createStore } from 'vuex';
import axios from 'axios';

export default createStore({
  state: {
    documents: [],
    documentItems: [],
    isDocumentsImported: false,
    isDocumentItemsImported: false,
  },
  getters: {
    isDocumentsImported(state) {
      return state.isDocumentsImported;
    },
    isDocumentItemsImported(state) {
      return state.isDocumentItemsImported;
    },
  },
  mutations: {
    setDocuments(state, documents) {
      state.documents = documents;
      state.isDocumentsImported = documents.length > 0;
    },
    setDocumentItems(state, documentItems) {
      state.documentItems = documentItems;
      state.isDocumentItemsImported = documentItems.length > 0;
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