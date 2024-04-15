<template>
    <div>
      <slot></slot>
      <div v-if="isOpenLoginPopup" class="popup-overlay">
        <div class="sticky top-1/4 flex justify-center">
          <div class="relative">
            <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
              @click="toggleLoginPopup">X</button>
            <LoginForm />
          </div>
        </div>
      </div>
      <div v-if="isOpenRegisterPopup" class="popup-overlay">
        <div class="sticky top-1/4 flex justify-center">
          <div class="relative">
            <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
              @click="toggleRegsiterPopup">X</button>
            <RegisterForm />
          </div>
        </div>
      </div>
      <div v-if="isOpenConfirmPopup" class="popup-overlay">
        <div class="sticky top-1/4 flex justify-center">
          <div class="relative">
            <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
              @click="toggleOpenOnfirmPopup">X</button>
            <ConfirmationForm :message="this.cfmMessage" :callback="this.cfmCallBack"/>
          </div>
        </div>
      </div>
    </div>
  
  </template>
  
  <script>
  import axios from "axios";
  import LoginForm from './LoginForm.vue';
  import RegisterForm from "./RegisterForm.vue";
  import ConfirmationForm from "./ConfirmationForm.vue";
  
  export default {
    name: "Authourization",
    inject: ["eventBus"],
    components: { LoginForm, RegisterForm, ConfirmationForm },
    data() {
      return {
        cfmMessage : '',
        cfmCallBack : '',
        isOpenLoginPopup: false,
        isOpenRegisterPopup: false,
        isOpenConfirmPopup: false,
      };
    },
    mounted() {
      this.eventBus.on("open-login-popup", () => {
        this.toggleLoginPopup()
      });
      this.eventBus.on("open-register-popup", () => {
        this.toggleRegsiterPopup()
      });
      this.eventBus.on("open-confirmation-popup", (request) => {
        this.toggleOpenOnfirmPopup(request.message, request.callback)
      });
      this.eventBus.on("get-user", async (resolve) => {
        let user = null;
        if (localStorage.token){
          user = await this.getUser(localStorage.token)
        } 
        resolve(user)
      });
      this.eventBus.on("login", (loginDto) => {
        this.login(loginDto)
      });
      this.eventBus.on("logout", () => {
        this.logout()
      });
      this.eventBus.on("register", (registerDto) => {
        this.register(registerDto)
      })
      this.updateApp()
    },
    methods: {
      updateApp(){
        this.eventBus.emit("update-app-user")
        this.eventBus.emit("update-header")
        this.eventBus.emit("update-profile")
        this.eventBus.emit("update-home-page")
        this.eventBus.emit("update-class-page")
      },
      toggleLoginPopup() {
        this.isOpenLoginPopup = !this.isOpenLoginPopup;
      },
      toggleRegsiterPopup() {
        this.isOpenRegisterPopup = !this.isOpenRegisterPopup;
      },
      toggleOpenOnfirmPopup(message, callback) {
        if (this.isOpenConfirmPopup){
          this.isOpenConfirmPopup = false;
          this.cfmMessage = ""
        } else {
          this.isOpenConfirmPopup = true;
          this.cfmMessage = message
          this.cfmCallBack = callback
        }
      },
      async login(loginDto) {
        try {
          const response = await axios.post(import.meta.env.VITE_API_URL + '/api/auth/login', {
            emailOrPhone: loginDto.emailOrPhone,
            password: loginDto.password
          })
          if (response.data) {
            localStorage.setItem("token", response.data.token)
            this.toggleLoginPopup()
            this.updateApp();
          }
        } catch (e) {
          if (e.response.data){
            this.eventBus.emit("login-set-error", e.response.data.ErrorMessage)
          } else {
            this.eventBus.emit("login-set-error", "Something went wrong. Please try again later!")
          }
        }
      },
      async register(registerDto) {
        try {
          const response = await axios.post(import.meta.env.VITE_API_URL + '/api/auth/register', {
            name: registerDto.name,
            password: registerDto.password,
            phone : registerDto.phone,
            email : registerDto.email,
            dob : registerDto.dob,
            address : registerDto.address,
            gender : registerDto.gender,
            bankAccount : registerDto.bankAccount
          })
          if (response.data) {
            await this.login({
              emailOrPhone : registerDto.phone,
              password :registerDto.password
            })
            this.toggleRegsiterPopup()
            this.toggleLoginPopup()
          }
        } catch (e) {
          if (e.response.data){
            this.eventBus.emit("register-set-error", e.response.data.ErrorMessage)
          } else {
            this.eventBus.emit("register-set-error", "Something went wrong. Please try again later!")
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
          return response.data
        }
        return null;
        
      },
      logout(){
        localStorage.removeItem("token")
        this.updateApp()
      },
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
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgba(0, 0, 0, 0.5);
  z-index: 2000;
}
  </style>