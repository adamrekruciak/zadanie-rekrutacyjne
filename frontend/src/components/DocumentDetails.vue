<template>
    <div>
      <h2>Document Details</h2>
      <div>
        <p>Type: {{ document.type }}</p>
        <p>Date: {{ document.date }}</p>
        <p>Name: {{ document.firstName }} {{ document.lastName }}</p>
        <p>City: {{ document.city }}</p>
      </div>
      <h3>Items</h3>
      <table>
        <thead>
          <tr>
            <th>Product</th>
            <th>Quantity</th>
            <th>Price</th>
            <th>Tax Rate</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in document.items" :key="item.id">
            <td>{{ item.product }}</td>
            <td>{{ item.quantity }}</td>
            <td>{{ item.price }}</td>
            <td>{{ item.taxRate }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>
  
  <script>
  //import axios from 'axios';
  
  export default {
    data() {
      return {
        document: null
      };
    },
    mounted() {
      this.fetchDocumentDetails();
    },
    methods: {
      async fetchDocumentDetails() {
        try {
          // You need to fetch the document details from the backend
          // based on the selected document id
          // Example: const response = await axios.get(`https://localhost:7107/Document/details/${document.id}`);
          // Set the fetched document data to 'document' property
        } catch (error) {
          console.error('Error fetching document details:', error);
        }
      }
    }
  };
  </script>
  
  <style>
  /* Add your CSS styling for the document details here */
  </style>
  