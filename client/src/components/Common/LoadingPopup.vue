<template>
  <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">

    <h2 class="text-2xl font-semibold text-gray-800 mb-6 text-center">{{ this.message }}</h2>
    <div class="mr-auto ml-auto loader"></div>

    
  </div>

</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
  name: "LoadingPopup",
  inject: ["eventBus"],
  props: ['message'],
}
</script>
<style>
.loader {
  border: 16px solid #f3f3f3; /* Light grey */
  border-top: 16px solid #3498db; /* Blue */
  border-radius: 50%;
  width: 120px;
  height: 120px;
  animation: spin 2s linear infinite;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>