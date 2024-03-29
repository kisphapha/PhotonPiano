<template>
  <div class="w-full flex justify-center items-center flex-col">
    <div class="text-4xl  m-8 font-bold">WELCOME TO THE PARADISE OF PIANO ART</div>
  </div>
  <PianoKeyboard />
  <div>
    Register Now
  </div>
  <div>
    Keep track of your studying
  </div>
  <div>
    About us
  </div>
</template>

<script>
import PianoKeyboard from '../components/PianoKeyboard.vue';

export default {
    name : "Home",
    components : {PianoKeyboard}
}
</script>

<style>

</style>