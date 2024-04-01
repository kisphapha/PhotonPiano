<template>
  <div>

    <div class="w-full flex justify-center items-center flex-col">
      <div class="text-4xl  m-8 font-bold">WELCOME TO THE PARADISE OF PIANO ART</div>
    </div>
    <PianoKeyboard />
  <div class="text-2xl m-8 font-bold text-center drop-shadow-sm	">JOIN US! BECOME A PART OF PHOTONPIANO</div>
  <div class="text-xl m-4 text-center drop-shadow-sm italic">MAKE YOURSELF BECOME A GRANDMASTER OF PIANO</div>
  <div class="text-xl m-4 text-center drop-shadow-sm italic">AND HAVE A CHANCE TO INTERACT WITH THE DIAMOND GOD!</div>
  <div class="flex flex-col justify-center items-center">
    <button @click="triggerOpenPopup" class="m-4 p-8 rounded-lg border-2 border-solid border-lime-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
      Register Now
    </button>
    <button @click="triggerOpenPopup" class="m-4 p-8 rounded-lg border-2 border-solid border-blue-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
      Keep Track of Your Studying
    </button>
    <RouterLink to="/about" class="m-4 p-8 rounded-lg border-2 border-solid border-gray-300 w-1/2 text-center text-xl shadow-md font-bold hover:bg-slate-100">
      About Us
    </RouterLink>
  </div>
F</div>
    
</template>

<script>
import PianoKeyboard from '../components/PianoKeyboard.vue';

export default {
    name : "HomePage",
    inject : ["eventBus"],
    components : {PianoKeyboard},
    methods : {
        triggerOpenPopup() {
            this.eventBus.emit("open-login-popup")
        }
    }
}
</script>

<style>

</style>