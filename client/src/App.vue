<template>
  <div>
    <Authourization>
      <HorNavbar />
      <Header />
      <RouterView style="min-height: 35vh;" />
      <Footer />
    </Authourization>
  </div>

</template>

<script>
import Authourization from "./components/Authourization.vue";
import HorNavbar from "./components/HorNavbar.vue";
import Header from "./components/Header.vue"
import Footer from "./components/Footer.vue";

export default {
  name: "App",
  components: { Header, Footer ,Authourization,HorNavbar},
}
</script>
<style>
</style>