<template>
  <div>
    <h2>List of Documents</h2>
    
      <div v-for="document in documents" :key="document.id">
        <div class="document">       
          <p><b>Type: </b>{{ document.type }}</p>
          <p><b>Date: </b>{{ document.date }}</p>
          <p><b>Name: </b>{{ document.firstName }} {{ document.lastName }}</p>
          <p><b>City: </b>{{ document.city }}</p> 
          <td><button @click="viewDetails(document)">View Details</button></td>
          

        </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      documents: [] // Tablica do przechowywania dokumentów
    };
  },
  mounted() {
    this.fetchDocuments(); // Wywołanie metody do pobrania dokumentów po zamontowaniu komponentu
  },
  methods: {
    async fetchDocuments() {
      try {
        const response = await axios.get('https://localhost:7107/Document/documents'); 
        this.documents = response.data; // Zdefiniowanie pobranych dokumentów w danych komponentu
      } catch (error) {
        console.error('Error fetching documents:', error);
      }
    },
    viewDetails(document) {
      this.$router.push({ name: 'DocumentDetails', params: { id: document.id } });
  }
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
</style>
