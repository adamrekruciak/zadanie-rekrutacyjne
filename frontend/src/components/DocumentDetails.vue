<template>
  <div class="document-details">
    <h2 class="title">Szczegóły dokumentów</h2>
    <div v-if="document" class="document-info">
      <p>
        <span><b>Typ: </b>{{ document.type }}</span>
        <span><b>Data: </b>{{ document.date }}</span>
        <span><b>Imię i nazwisko: </b>{{ document.firstName }} {{ document.lastName }}</span>
        <span><b>Miasto: </b>{{ document.city }}</span>
      </p>
      <h3>Przedmioty</h3>
      <ul>
        <li v-for="item in documentItems" :key="item.id">
          <span><b>{{ item.product }}</b></span> 
          <p> Ilość: {{ item.quantity }}, Cena: {{ formatPrice(item.price) }}</p> 
          </li>
      </ul>
    </div>
    <div v-else class="loading">
      <p>Loading...</p>
    </div>
    <div class="buttons-container">
    <router-link :to="'/document/' + (parseInt($route.params.id) - 1)" v-if="document">
      <MDBBtn color="info" class="inner-button">Poprzedni dokument</MDBBtn>
    </router-link> 
    <router-link to="/">
      <MDBBtn color="danger" class="inner-button">Powrót do listy dokumentów</MDBBtn>
    </router-link>
    <router-link :to="'/document/' + (parseInt($route.params.id) + 1)" v-if="document">
      <MDBBtn color="info" class="inner-button">Następny dokument</MDBBtn>
    </router-link>
    </div>
  </div>
  
</template>

<script>
import axios from 'axios';
import { MDBBtn } from 'mdb-vue-ui-kit';

export default {
  components: {
    MDBBtn
  },
  data() {
    return {
      document: null,
      documentItems: [],
    };
  },
  methods: {
    async fetchDocumentDetails(id) {
      try {
        const response = await axios.get(`https://localhost:7107/Document/documents/${id}`);
        this.document = response.data;

        const itemsResponse = await axios.get(`https://localhost:7107/Document/documents/${id}/items`);
        this.documentItems = itemsResponse.data;
      } catch (error) {
        console.error('Error fetching document details:', error);
      }
    },
    formatPrice(price) {
      // Przeliczenie ceny z groszy na złotówki
      return (price / 100).toFixed(2);
    }
  },
  watch: {
    '$route.params.id': function(newId) {
      this.fetchDocumentDetails(newId);
    }
  },
  mounted() {
    const id = this.$route.params.id;
    this.fetchDocumentDetails(id);
  },
};
</script>

<style>
html{
  background: #f2f2f2
}
.document-details {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.title {
  margin-bottom: 20px; /* Dodanie marginesu na dole tytułu */
}

.document-info {
  max-width: 800px;
  padding: 20px;
  border: 1px solid #ddd;
  background-color: #f9f9f9;
}

.document-info p {
  margin-bottom: 15px;
}

.document-info p span {
  margin-right: 20px;
}

.document-info ul {
  list-style: none;
  padding: 0;
}

.document-info ul li {
  margin-bottom: 5px;
}

.loading {
  text-align: center;
  font-style: italic;
}

.buttons-container {
  display: flex;
  justify-content: space-between;
  width: 40%;
  margin-top: 20px;
}
.inner-button {
  flex: 1; 
  height: 60px;
}
</style>
