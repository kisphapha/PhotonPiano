<template>
  <Header :user="user"/>
  <RouterView style="min-height: 35vh;"/>
  <Footer />

  <div v-if="isOpenLoginPopup" class="popup-overlay">
      <div class="sticky top-1/4 flex justify-center">
        <div class="relative">
          <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full" @click="toggleLoginPopup">X</button>
          <LoginForm />
        </div>    
      </div>
  </div>
</template>

<script>
import Header from "./components/Header.vue"
import Footer from "./components/Footer.vue";
import LoginForm from './components/LoginForm.vue';

export default {
    name : "App",
    inject: ["eventBus"],
    components : {Header, Footer, LoginForm},
    data() {
      return {
        user : null,
        isOpenLoginPopup: false
      };
    },
    mounted(){
      this.eventBus.on("open-login-popup", () => {
        this.toggleLoginPopup()
      });
      this.eventBus.on("login", () => {
        this.login()
      })
    },    
    methods:{
      toggleLoginPopup() {
        this.isOpenLoginPopup = !this.isOpenLoginPopup;
      },
      login(){
        this.user = {
          id : 1,
          name : "kisphophu",
          profilePicture : "https://st2.depositphotos.com/2550635/7888/i/450/depositphotos_78887438-stock-photo-colorful-daybreak-in-a-beautiful.jpg"
        }
      }
    }
}
</script>
<style>
.popup-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 500;
  }
</style>