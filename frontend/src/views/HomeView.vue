<template>
  <div class="project">
    <div class="buttons">
      <div class="action-buttons">
        <MDBBtn color="info" @click="toggleView">Zmień widok</MDBBtn>
      </div>

      <div class="import-buttons">
        <MDBBtn color="success" @click="importDocuments">Importuj dokumenty</MDBBtn>
        <MDBBtn color="success" @click="importDocumentItems">Importuj szczegóły dokumentów</MDBBtn>
        <vue-notification></vue-notification>
      </div>

      <div class="delete-buttons">
        <MDBBtn color="danger" @click="deleteDocuments">Usuń dokumenty</MDBBtn>
        <MDBBtn color="danger" @click="deleteDocumentItems">Usuń szczegóły dokumentów</MDBBtn>
      </div>
    </div>
    
    <div v-if="isListView" class="documents">
      <DocumentList />
    </div>
    
    <div v-else>
      <DocumentTable />
    </div>
  </div>
</template>

<script>
import DocumentList from '../components/DocumentList.vue';
import DocumentTable from '../components/DocumentTable.vue';
import { MDBBtn } from 'mdb-vue-ui-kit';
import axios from 'axios';
import { toast } from 'vue3-toastify';

export default {
  name: 'App',
  components: {
    DocumentList,
    DocumentTable, 
    MDBBtn
  },
  data() {
    return {
      isListView: true,
      documents: [],
      isImportingDocumentItems: false,
      isImportingDocuments: false, 
    };
  },
  methods: {
    toggleView() {
      this.isListView = !this.isListView;
    },

    async importDocuments() {
    if (this.isImportingDocuments) {
      return; 
    }
    this.isImportingDocuments = true;

    try {
      if (this.$store.getters.isDocumentsImported) {
        toast.error('Dokumenty są już zaimportowane!');
        this.isImportingDocuments = false; 
        return;
      }
      const response = await axios.post('https://localhost:7107/Document/import');
      console.log(response.data);
      this.$store.dispatch('fetchDocuments');
      toast.success('Dokumenty zostały załadowane!'); 
    } catch (error) {
      console.error('Error importing documents:', error);
    } finally {
      this.isImportingDocuments = false; 
    }
    },
    
    async importDocumentItems() {
    if (this.isImportingDocumentItems) {
      return; 
    }
    this.isImportingDocumentItems = true; 

    try {
      const existingDocumentItems = await axios.get('https://localhost:7107/Document/document-items');
      if (existingDocumentItems.data.length > 0) {
        toast.error('Szczegóły zostały już dodane!');
        return;
      }
      
      await axios.post('https://localhost:7107/Document/import-items');
      this.$store.dispatch('fetchDocuments');
      this.$store.commit('setIsDocumentItemsImported', true);
      toast.success('Szczegóły zostały załadowane!');
    } catch (error) {
      console.error('Error importing document items:', error);
    } finally {
      this.isImportingDocumentItems = false;
    }
  },


    async deleteDocuments() {
      try {
        const response = await axios.delete('https://localhost:7107/Document/delete-all-documents');
        console.log(response.data);
        this.$store.dispatch('fetchDocuments');
        this.$store.commit('resetImportFlags'); 
        localStorage.setItem('isDocumentsImported', 'false'); 
        toast.success('Dokumenty zostały usunięte!');
      } catch (error) {
        console.error('Error deleting documents:', error);
      }
    },
    
    async deleteDocumentItems() {
      try {
        const response = await axios.delete('https://localhost:7107/Document/delete-all-document-items');
        console.log(response.data);
        this.$store.dispatch('fetchDocuments'); 
        this.$store.commit('resetImportFlags'); 
        localStorage.setItem('isDocumentItemsImported', 'false'); 
        toast.success('Szczegóły dokumentów zostały usunięte!');
      } catch (error) {
        console.error('Error deleting document items:', error);
      }
    },
  }
};
</script>

<style>
.project{
  max-width: 1600px;
  margin: 0 auto;
  margin-top: 10px;
}

button {
  margin: 5px;
  width: 200px; 
  height: 50px; 
} 

.documents{
  max-width:1200px;
  margin: 0 auto;
}

.import-buttons,
.delete-buttons, 
.action-buttons {
  margin-bottom: 8px; 
}

.action-buttons {
  float: left;
}
</style>
