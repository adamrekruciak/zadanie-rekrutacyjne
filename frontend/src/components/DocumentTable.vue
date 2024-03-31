<template>
  <div>
    <h2>Lista dokumentów (widok w formie tabelarycznej)</h2>
    <input v-model="searchQuery" type="text" placeholder="Wyszukaj w dokumentach" class="search-input"/>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Typ</th>
          <th>Data</th>
          <th>Imię i nazwisko</th>
          <th>Miasto</th>
        </tr>
      </thead>
      <tbody>
        <template v-for="document in paginatedDocuments" :key="`document-${document.id}`">
          <tr @click="toggleDetails(document.id)">
            <td>{{ document.id }}</td>
            <td>{{ document.type }}</td>
            <td>{{ formatDate(document.date) }}</td>
            <td>{{ document.firstName }} {{ document.lastName }}</td>
            <td>{{ document.city }}</td>
          </tr>
          <tr v-if="showDetails[document.id]" :key="`details-${document.id}`">
            <td colspan="5">
              <div v-if="documentItems[document.id] && documentItems[document.id].length > 0">
                <div class="documentDetails" v-for="item in documentItems[document.id]" :key="item.id">
                  <p><b>Produkt:</b> {{ item.product }}
                    <b>Ilość:</b> {{ item.quantity }}
                    <b> Cena:</b> {{ formatPrice(item.price) }}</p>
                </div>
              </div>
              <div v-else>
                <p>Ładowanie szczegółów...</p>
              </div>
            </td>
          </tr>
        </template>
      </tbody>
    </table>

    <div class="pagination">
      <MDBBtn color="info" @click="prevPage" :disabled="currentPage === 1">Poprzednia strona</MDBBtn>
      <span>Strona {{ currentPage }} z {{ totalPages }}</span>
      <MDBBtn color="info" @click="nextPage" :disabled="currentPage === totalPages">Następna strona</MDBBtn>
    </div>
  </div>
  <div class="documents-per-page">
      <label for="documentsPerPage">Liczba dokumentów na stronę: </label>
      <select v-model="documentsPerPage" @change="updatePagination">
        <option v-for="option in perPageOptions" :key="option" :value="option">{{ option }}</option>
      </select>
    </div>
</template>
<script>
import axios from 'axios';
import { MDBBtn } from 'mdb-vue-ui-kit';

export default {
  data() {
    return {
      documents: [],
      showDetails: {},
      documentItems: {},
      searchQuery: '', 
      currentPage: 1,
      documentsPerPage: 11,
      perPageOptions: [10, 11, 20, 50, 100] // Dostępne opcje na stronę
    };
  },
  computed: {
    filteredDocuments() {
      if (this.searchQuery) {
        return this.documents.filter(document => {
          return Object.values(document).some(value =>
            String(value).toLowerCase().includes(this.searchQuery.toLowerCase())
          );
        });
      }
      return this.documents;
    },
    totalPages() {
      return Math.ceil(this.filteredDocuments.length / this.documentsPerPage);
    },
    paginatedDocuments() {
      const startIndex = (this.currentPage - 1) * this.documentsPerPage;
      const endIndex = startIndex + this.documentsPerPage;
      return this.filteredDocuments.slice(startIndex, endIndex);
    }
  },
  methods: {
    async fetchDocuments() {
      try {
        const response = await axios.get('https://localhost:7107/Document/documents');
        this.documents = response.data;
      } catch (error) {
        console.error('Error fetching documents:', error);
      }
    },
    toggleDetails(id) {
      this.showDetails[id] = !this.showDetails[id];
      if (this.showDetails[id] && !this.documentItems[id]) {
        this.fetchDocumentItems(id);
      }
    },
    async fetchDocumentItems(documentId) {
      try {
        const response = await axios.get(`https://localhost:7107/Document/documents/${documentId}/items`);
        this.documentItems[documentId] = response.data;
      } catch (error) {
        console.error(`Error fetching items for document ${documentId}:`, error);
        this.documentItems[documentId] = [];
      }
    },
    formatDate(date) {
      return new Date(date).toISOString().split('T')[0];
    },
    formatPrice(price) {
      return (price / 100).toFixed(2);
    },
    nextPage() {
      if (this.currentPage < this.totalPages) {
        this.currentPage++;
      }
    },
    prevPage() {
      if (this.currentPage > 1) {
        this.currentPage--;
      }
    },
    updatePagination() {
      this.currentPage = 1;
    },
  },
  mounted() {
    this.fetchDocuments();
  },
  components: {
    MDBBtn
  }
};
</script>

<style>
table {
  width: 100%;
  border-collapse: collapse;
}

table th, table td {
  border: 1px solid #ddd;
  padding: 8px;
}

table th {
  background-color: #f23c97;
  text-align: left;
}

table td {
  cursor: pointer;  
}

.search-input {
  margin-bottom: 20px;
  padding: 10px;
  width: 30%;
  float: left;
  box-sizing: border-box;
}

.documentDetails {
  cursor: default;
}

.documents-per-page {
  margin-top: 10px;
  margin-bottom: 15px;
}
</style>
