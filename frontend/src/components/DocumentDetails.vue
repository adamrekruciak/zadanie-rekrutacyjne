<template>
  <div>
    <h2>Document Details</h2>
    <div v-if="document">
      <p>
        <b>Type: </b>{{ document.type }}
        <b>Date: </b>{{ document.date }}
        <b>Name: </b>{{ document.firstName }} {{ document.lastName }}
        <b>City: </b>{{ document.city }}
      </p>
      <h3>Items</h3>
      <ul>
        <li v-for="item in documentItems" :key="item.id">
          {{ item.product }} - Quantity: {{ item.quantity }}, Price: {{ item.price }}
        </li>
      </ul>
    </div>
    <div v-else>
      <p>Loading...</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
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
  },
  mounted() {
    const id = this.$route.params.id;
    this.fetchDocumentDetails(id);
  },
};
</script>
