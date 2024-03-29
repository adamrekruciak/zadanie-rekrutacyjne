<template>
  <div>
    <div class="buttons">
      <MDBBtn color="info" @click="toggleView">Zmień widok</MDBBtn>
    </div>
    
    <div v-if="isListView">
      <DocumentList />
    </div>
    
    <div v-else>
      <DocumentTable />
    </div>
  </div>
</template>

<script>
import DocumentList from '../components/DocumentList.vue';
import DocumentTable from '../components/DocumentTable.vue';
import { MDBBtn } from 'mdb-vue-ui-kit';

export default {
  name: 'App',
  components: {
    DocumentList,
    DocumentTable, 
    MDBBtn
  },
  data() {
    return {
      isListView: true
    };
  },
  methods: {
    toggleView() {
      this.isListView = !this.isListView;
    }
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  max-width: 1200px;
  margin: 0 auto;
  color: #2c3e50;
  margin-top: 60px;
}

button {
  margin: 5px;
}
</style>
