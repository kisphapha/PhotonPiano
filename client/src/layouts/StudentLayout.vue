<template>
    <HorNavbar />
    <Header />
    <RouterView style="min-height: 35vh;" />
    <Footer />
</template>


<script>
import Footer from '../components/Common/Footer.vue'
import Header from '../components/Common/Header.vue'
import HorNavbar from '../components/Common/HorNavbar.vue'

export default {
    name : "StudentLayout",
    components: { HorNavbar, Header, Footer },
}
</script>

<style></style>