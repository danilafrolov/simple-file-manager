<template>
  <div id="app">
    <header></header>
    <main>
      <FilesList/>
    </main>
  </div>
</template>

<script>
import FilesList from "./components/FilesList.vue";

export default {
  name: "app",
  components: {
    FilesList
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  align-content: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
