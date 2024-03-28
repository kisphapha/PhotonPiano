<template>
  <Header />
  <RouterView/>
</template>

<script>
import Header from "./components/Header.vue"
import Home from "./pages/Home.vue"
export default {
    name : "App",
    components : {Header, Home}
}
</script>