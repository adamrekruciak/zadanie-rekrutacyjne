<template>
  <div>
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
      documents: [] 
    };
  },
  methods: {
    toggleView() {
      this.isListView = !this.isListView;
    },
    async importDocuments() {
      try {
        const response = await axios.post('https://localhost:7107/Document/import');
        console.log(response.data);
        this.$store.dispatch('fetchDocuments');
      } catch (error) {
        console.error('Error importing documents:', error);
      }
    },
    async importDocumentItems() {
      try {
        const response = await axios.post('https://localhost:7107/Document/import-items');
        console.log(response.data); 
        this.$store.dispatch('fetchDocuments');
      } catch (error) {
        console.error('Error importing document items:', error);
      }
    },
    async deleteDocuments() {
    try {
      const response = await axios.delete('https://localhost:7107/Document/delete-all-documents');
      console.log(response.data); 
      this.$store.dispatch('fetchDocuments'); // Pobranie dokumentów ponownie
    } catch (error) {
      console.error('Error deleting documents:', error);
    }
  },
  async deleteDocumentItems() {
  try {
    const response = await axios.delete('https://localhost:7107/Document/delete-all-document-items');
    console.log(response.data); 
    this.$store.dispatch('fetchDocuments'); // Pobranie dokumentów ponownie
  } catch (error) {
    console.error('Error deleting document items:', error);
  }
},
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  max-width: 1800px;
  margin: 0 auto;
  color: #2c3e50;
  margin-top: 20px;
  
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
  float: left; /* Przesuwa przyciski do lewej strony */
}
</style>
