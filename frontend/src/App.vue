<template>  

  <router-view/>
</template>

<style>
#app {
  font-family: Roboto, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  max-width: 900px;
  margin: 0 auto;
}

body {
  background: #f2f2f2;
}
nav {
  padding: 30px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
}

nav a.router-link-exact-active {
  color: #42b983;
}
.buttons {
  margin-bottom: 20px;
  text-align: right;
}
</style>
