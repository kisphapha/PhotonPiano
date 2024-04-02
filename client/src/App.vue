<template>
  <div>
    <Header :user="user" />
    <RouterView style="min-height: 35vh;" />
    <Footer />

    <div v-if="isOpenLoginPopup" class="popup-overlay">
      <div class="sticky top-1/4 flex justify-center">
        <div class="relative">
          <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
            @click="toggleLoginPopup">X</button>
          <LoginForm />
        </div>
      </div>
    </div>
  </div>

</template>

<script>
import axios from "axios";
import Header from "./components/Header.vue"
import Footer from "./components/Footer.vue";
import LoginForm from './components/LoginForm.vue';

export default {
  name: "App",
  inject: ["eventBus"],
  components: { Header, Footer, LoginForm },
  data() {
    return {
      user: null,
      isOpenLoginPopup: false,
    };
  },
  mounted() {
    this.eventBus.on("open-login-popup", () => {
      this.toggleLoginPopup()
    });
    this.eventBus.on("login", (loginDto) => {
      this.login(loginDto)
    })
    if (localStorage.token) {
      this.getUser(localStorage.token)
    }
  },
  methods: {
    toggleLoginPopup() {
      this.isOpenLoginPopup = !this.isOpenLoginPopup;
    },
    async login(loginDto) {
      try {
        const response = await axios.post(import.meta.env.VITE_API_URL + '/api/auth/login', {
          emailOrPhone: loginDto.emailOrPhone,
          password: loginDto.password
        })
        if (response.data) {
          localStorage.setItem("token", response.data.token)
          this.getUser(response.data.token)
          this.toggleLoginPopup()
        }
      } catch (e) {
        if (e.response.data){
          this.eventBus.emit("login-set-error", e.response.data.ErrorMessage)
        } else {
          this.eventBus.emit("login-set-error", "Something went wrong. Please try again later!")
        }
      }
    },
    async getUser(token) {
      const response = await axios.get(import.meta.env.VITE_API_URL + '/api/auth/who-am-i', {
        headers: {
          'Authorization': 'Bearer ' + token,
        }
      })
      if (response.data) {
        this.user = response.data
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