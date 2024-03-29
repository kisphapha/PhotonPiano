<template>
  <Header />
  <RouterView style="min-height: 35vh;"/>
  <Footer />
</template>

<script>
import Header from "./components/Header.vue"
import Home from "./pages/Home.vue"
import Footer from "./components/Footer.vue";
export default {
    name : "App",
    components : {Header, Home, Footer}
}
</script>
<style>

</style>