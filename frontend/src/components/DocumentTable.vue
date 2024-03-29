<template>
    <div>
      <h2>List of Documents</h2>
      
      <table>
        <thead>
          <tr>
            <th>Type</th>
            <th>Date</th>
            <th>Name</th>
            <th>City</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="document in documents" :key="document.id">
            <td>{{ document.type }}</td>
            <td>{{ document.date }}</td>
            <td>{{ document.firstName }} {{ document.lastName }}</td>
            <td>{{ document.city }}</td>
          </tr>
        </tbody>
      </table>
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
      }
    }
  };
  </script>
  
  <style>
  table {
    width: 100%;
    border-collapse: collapse;
    background: white;
  }
  
  table th, table td {
    border: 1px solid #ddd;
    padding: 8px;
  }
  
  table th {
    background-color: #f23c97;
    text-align: left;
    font-size: 18px;
  }
  </style>