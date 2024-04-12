<template>
  <div>
    <Authourization>
      <div v-if='role == "Student"'>
        <HorNavbar />
        <Header />
        <RouterView style="min-height: 35vh;" />
        <Footer />
      </div>
      <div v-else class="h-screen">
        <HorNavbar class="min-h-[7.5vh]"/>
        <div class="flex">
          <VerNavBar class="min-h-[92.5vh]"/>
          <div class="w-full overflow-y-auto h-[92.5vh]">
            <RouterView style="min-height: 35vh;" />
          </div>
        </div>
      </div>
    </Authourization>
  </div>

</template>

<script>
import Authourization from "./components/Common/Authourization.vue";
import HorNavbar from "./components/Common/HorNavbar.vue";
import Header from "./components/Common/Header.vue"
import Footer from "./components/Common/Footer.vue";
import VerNavBar from "./components/Staff/VerNavBar.vue";

export default {
  name: "App",
  components: { Header, Footer, Authourization, HorNavbar, VerNavBar },
  data() {
    return {
      role: "Staff"
    }
  }
}
</script>
<style>
::-webkit-scrollbar {
  width: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1; 
}
 
/* Handle */
::-webkit-scrollbar-thumb {
  background: #888; 
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555; 
}

#staff-table tr td,
#staff-table th {
  padding: 0.5rem 2rem 0.5rem 2rem;
  text-align: center
}
#staff-table thead tr {
  background-color: #161618;
  color : white;
  font-weight: bold;
}
#staff-table tr {
  background-color: #eeeeee;
}
#staff-table tbody tr:nth-child(even) {
  background-color: #e0e0e0;
}
#staff-table thead tr th:first-child{
  border-radius: 3rem 0 0 0;
}
#staff-table thead tr th:last-child{
  border-radius: 0 3rem 0 0;
}
#staff-table tbody tr:last-child td:first-child {
  border-radius: 0 0 0 3rem;
}
#staff-table tbody tr:last-child td:last-child {
  border-radius: 0 0 3rem 0;
}
</style>