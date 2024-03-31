<template>
    <div>
      <h2>Lista dokumentów (widok w formie tabelarycznej)</h2>
      
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
          <tr v-for="document in documents" :key="document.id">
            <td>{{ document.id }}</td>
            <td>{{ document.type }}</td>
            <td>{{ formatDate(document.date) }}</td>
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
      },
      formatDate(date) {
      return new Date(date).toISOString().split('T')[0];
    },
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