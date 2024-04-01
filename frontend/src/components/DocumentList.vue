<template>
  <div>
    <h2>Lista dokumentów</h2>

    <div v-if="documents.length > 0">
      <div class="sorting-buttons">
        <button type="button" class="btn btn-secondary" :class="{ 'active': sortBy === 'default' }" @click="sortByDefault">Sortuj po ID</button>
        <button type="button" class="btn btn-secondary" :class="{ 'active': sortBy === 'date' && sortDirection === 'asc' }" @click="sortByDate('asc')">Od najstarszych</button>
        <button type="button" class="btn btn-secondary" :class="{ 'active': sortBy === 'date' && sortDirection === 'desc' }" @click="sortByDate('desc')">Od najnowszych</button>
      </div>

      <div v-for="(document, index) in paginatedDocuments" :key="index">
        <div class="document">       
          <p><b>Typ: </b>{{ document.type }}</p>
          <p><b>Data: </b>{{ formatDate(document.date) }}</p>
          <p><b>Imię i nazwisko: </b>{{ document.firstName }} {{ document.lastName }}</p>
          <p><b>Miasto: </b>{{ document.city }}</p> 
          <div class="buttonInside"> 
            <MDBBtn color="warning" @click="viewDocumentDetails(document.id)">Szczegóły</MDBBtn>
          </div>
        </div>
      </div>

      <div class="pagination">
        <MDBBtn color="info" @click="prevPage" :disabled="currentPage === 1">Poprzednia strona</MDBBtn>
        <span>Strona {{ currentPage }} z {{ totalPages }}</span>
        <MDBBtn color="info" @click="nextPage" :disabled="currentPage === totalPages">Następna strona</MDBBtn>
      </div>

      <div class="documents-per-page">
        <label for="perPage">Liczba dokumentów na stronę: </label>
        <select v-model="perPage" @change="updatePagination">
          <option v-for="option in perPageOptions" :key="option" :value="option">{{ option }}</option>
        </select>
      </div>
    </div>

    <div v-else>
      <p style="margin-top:50px;">Brak dostępnych dokumentów do wyświetlenia. Zaimportuj dane.</p>
    </div>
  </div>
</template>


<script>
import axios from 'axios';
import { MDBBtn } from 'mdb-vue-ui-kit';
import { mapState } from 'vuex';
import { toast } from 'vue3-toastify';

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
        return this.documents.slice(); 
      }
    },
    totalPages() {
      return Math.ceil(this.documents.length / this.perPage);
    },
    paginatedDocuments() {
      const startIndex = (this.currentPage - 1) * this.perPage;
      const endIndex = startIndex + this.perPage;
      return this.sortedDocuments.slice(startIndex, endIndex);
    },
  },

  data() {
    return {
      sortBy: 'default', 
      sortDirection: 'asc', 
      currentPage: 1,
      perPage: 3, 
      perPageOptions: [3, 5, 10, 20, 50, 100], 
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
        const response = await axios.get(`https://localhost:7107/Document/documents/${id}/items`);
        this.$router.push({ name: 'DocumentDetails', params: { id: id, details: response.data } });
      } catch (error) {
        toast.error('Szczegóły dokumentów nie zostały jeszcze zaimportowane.');
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
    },

    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
        sessionStorage.setItem('currentPage', this.currentPage);
      }
    },

    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
        sessionStorage.setItem('currentPage', this.currentPage);
      }
    },
    
    updatePagination() {
      this.currentPage = 1;
      sessionStorage.setItem('currentPage', this.currentPage);
      sessionStorage.setItem('perPage', this.perPage);
    },
  },
  mounted() {
    const savedPage = sessionStorage.getItem('currentPage');
    const savedPerPage = sessionStorage.getItem('perPage');
    if (savedPage) {
      this.currentPage = parseInt(savedPage, 10);
    }
    if (savedPerPage) {
      this.perPage = parseInt(savedPerPage, 10);
    }
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

.pagination {
  margin-top: 20px;
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.documents-per-page {
  margin-top: 10px;
  margin-bottom: 15px;
}
</style>
