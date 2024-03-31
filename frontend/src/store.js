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
      localStorage.setItem('isDocumentsImported', documents.length > 0 ? 'true' : 'false');
    },
    setDocumentItems(state, documentItems) {
      state.documentItems = documentItems;
      state.isDocumentItemsImported = documentItems.length > 0;
      localStorage.setItem('isDocumentItemsImported', documentItems.length > 0 ? 'true' : 'false');
    },
    setIsDocumentItemsImported(state, value) {
      state.isDocumentItemsImported = value;
      localStorage.setItem('isDocumentItemsImported', value ? 'true' : 'false');
    },
    resetImportFlags(state) {
      state.isDocumentsImported = false;
      state.isDocumentItemsImported = false;
      localStorage.setItem('isDocumentsImported', 'false');
      localStorage.setItem('isDocumentItemsImported', 'false');
    }
  },
  actions: {
    async fetchDocuments({ commit }) {
      try {
        const response = await axios.get('https://localhost:7107/Document/documents'); 
        commit('setDocuments', response.data);
        localStorage.setItem('isDocumentsImported', response.data.length > 0 ? 'true' : 'false');
      } catch (error) {
        console.error('Error fetching documents:', error);
      }
    },
    async fetchDocumentItems({ commit }) {
      try {
        const response = await axios.get('https://localhost:7107/Document/document-items'); 
        commit('setDocumentItems', response.data);
        // Ustaw flagę isDocumentItemsImported na true w localStorage, jeśli dostaniemy jakiekolwiek szczegóły dokumentów
        localStorage.setItem('isDocumentItemsImported', response.data.length > 0 ? 'true' : 'false');
      } catch (error) {
        console.error('Error fetching document items:', error);
      }
    },
    initializeStore({ commit }) {
      const isDocumentsImported = localStorage.getItem('isDocumentsImported') === 'true';
      const isDocumentItemsImported = localStorage.getItem('isDocumentItemsImported') === 'true';
      commit('setIsDocumentItemsImported', isDocumentItemsImported);
      commit('setDocuments', isDocumentsImported ? [] : []); // Możesz dostosować tę logikę w zależności od potrzeb
    }
  },
});
