<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
      <h2 class="text-2xl font-semibold text-gray-800 mb-6">Sign in PhotonPiano</h2>
      <div>
        <div class="mb-4">
          <label for="emailOrPhone" class="block text-gray-700 text-sm font-bold mb-2">Email or Phone Number</label>
          <input id="emailOrPhone" v-model="emailOrPhone" type="text" class="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500" />
        </div>
        <div class="mb-4">
          <label for="txtpassword" class="block text-gray-700 text-sm font-bold mb-2">Password</label>
          <input  id="txtpassword" v-model="password" type="password" class="w-full px-4 py-2 border rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500" />
        </div>
        <div class="flex items-center justify-center">
          <button @click="login" class="bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-4 rounded focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50">Login</button>
          <!-- <a href="#" class="text-blue-500 hover:text-blue-700 text-sm">Already have an account? Log in</a> -->
        </div>
      </div>
  </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name : "LoginForm",
    inject : ["eventBus"],
    data() {
      return {
        emailOrPhone: '',
        password: '',
      };
    },
    methods : {
      login(){
        this.eventBus.emit("open-login-popup");
        this.eventBus.emit("login", {
          emailOrPhone : this.emailOrPhone,
          password : this.password
        })
      }
    }
}
</script>
<style>

</style>