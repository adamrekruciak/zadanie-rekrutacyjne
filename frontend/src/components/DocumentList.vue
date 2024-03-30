<template>
  <div>
    <h2>Lista dokumentów</h2>
    <div v-for="document in documents" :key="document.id">
      <div class="document">       
        <p><b>Typ: </b>{{ document.type }}</p>
        <p><b>Data: </b>{{ document.date }}</p>
        <p><b>Imię i nazwisko: </b>{{ document.firstName }} {{ document.lastName }}</p>
        <p><b>Miasto: </b>{{ document.city }}</p> 
        <div class="buttonInside"> 
          <MDBBtn color="warning"  @click="viewDocumentDetails(document.id)">Szczegóły</MDBBtn>
        </div>
       
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import { MDBBtn } from 'mdb-vue-ui-kit';
import { mapState } from 'vuex';

export default {
  components: {
    MDBBtn
  },

  computed: mapState(['documents']),

  methods: {
    async fetchDocuments() {
      try {
      if (this.documents && this.documents.length > 0) {
        alert('Dane są już zaimportowane');
        return;
      }
        const response = await axios.get('https://localhost:7107/Document/documents'); 
        this.documents = response.data;
      } catch (error) {
        console.error('Error fetching documents:', error);
      }
    },
    async viewDocumentDetails(id) {
      try {
        // Pobierz szczegóły dokumentu z odpowiedzi serwera
        const response = await axios.get(`https://localhost:7107/Document/documents/${id}/items`);
        // Przekieruj do komponentu DocumentDetails i przekaż szczegóły dokumentu
        this.$router.push({ name: 'DocumentDetails', params: { id: id, details: response.data } });
      } catch (error) {
        console.error('Error fetching document items:', error);
      }
    }
  },
  mounted() {
    this.$store.dispatch('fetchDocuments'); 
  },
};
</script>


<style>
.document{
    margin: 20px auto;
    background: white;
    padding: 10px 20px;
    border-radius: 4px;
    box-shadow: 1px 2px 3px rgba(0,0,0,0.05);
    border-left: 4px solid #e90074;
}
.buttonInside{
  text-align: right;
}
</style>
