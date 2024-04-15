<template>
    <HorNavbar />
    <Header />
        No layout for instructors yet!
    <Footer />
</template>

<script>
import Footer from '../components/Common/Footer.vue'
import Header from '../components/Common/Header.vue'
import HorNavbar from '../components/Common/HorNavbar.vue'


export default {
    name : "InstructorLayout",
    components: { HorNavbar, Header, Footer },
}
</script>

<style></style>