<template>
  <div>
    <h2>Lista dokumentów</h2>
    <div class="sorting-buttons">
      <button type="button" class="btn btn-secondary" :class="{ 'active': sortBy === 'default' }" @click="sortByDefault">Sortuj po ID</button>
      <button type="button" class="btn btn-secondary" :class="{ 'active': sortBy === 'date' && sortDirection === 'asc' }" @click="sortByDate('asc')">Od najstarszych</button>
      <button type="button" class="btn btn-secondary" :class="{ 'active': sortBy === 'date' && sortDirection === 'desc' }" @click="sortByDate('desc')">Od najnowszych</button>
    </div>


    <div v-for="document in sortedDocuments" :key="document.id">
      <div class="document">       
        <p><b>Typ: </b>{{ document.type }}</p>
        <p><b>Data: </b>{{ formatDate(document.date) }}</p>
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

  computed: {
    ...mapState(['documents']),
    sortedDocuments() {
      if (this.sortBy === 'date') {
        if (this.sortDirection === 'asc') {
          return this.documents.slice().sort((a, b) => new Date(a.date) - new Date(b.date));
        } else {
          return this.documents.slice().sort((a, b) => new Date(b.date) - new Date(a.date));
        }
      } else {
        return this.documents.slice(); // Return default order by ID
      }
    }
  },

  data() {
    return {
      sortBy: 'default', // Default sort order
      sortDirection: 'asc' // Default sort direction
    };
  },

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
    },

    formatDate(date) {
      return new Date(date).toISOString().split('T')[0];
    },

    sortByDate(direction) {
      this.sortBy = 'date';
      this.sortDirection = direction;
    },

    sortByDefault() {
      this.sortBy = 'default';
    }
  },

  mounted() {
    this.$store.dispatch('fetchDocuments'); 
  },
};
</script>

<style>
.document {
  margin: 20px auto;
  background: white;
  padding: 10px 20px;
  border-radius: 4px;
  box-shadow: 1px 2px 3px rgba(0,0,0,0.05);
  border-left: 4px solid #e90074;
}

.buttonInside {
  text-align: right;
}

.sorting-buttons {
  margin-bottom: 10px;
  display: flex;
  justify-content: right;
}
.active {
  color: white; 
}
</style>
