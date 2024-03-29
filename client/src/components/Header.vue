<template>
    <div id="banner" class="bg-black text-white font-bold">
        <div class="flex justify-end ml-4 mr-4 text-xl">
            <RouterLink class="p-2 ml-6" to="/">Home</RouterLink>
            <RouterLink class="p-2 ml-6" to="/about">About</RouterLink>
            <!-- <RouterLink class="p-2 ml-6" to="/forum">Forum</RouterLink> -->
            <RouterLink class="p-2 ml-6 text-blue-400" to="/login">Register</RouterLink>
            <RouterLink class="p-2 ml-6 text-cyan-400" to="/login">Login</RouterLink>
        </div>
        <div class="text-5xl pl-8 pt-28 w-full italic">PHOTON PIANO</div>
        <div class="shadow text-2xl pl-8 pb-32 font-bold">Kim Cương's Piano Master Class</div>
    </div>
</template>

<script>
import { RouterLink } from 'vue-router';

export default {
    name : "Header",
}
</script>
<style>
#banner{
    background-image: url("../assets/background.png");
    background-position: center;
    background-repeat: no-repeat;
}

.router-link-active {
    border-radius: 10px;
    background-color: white;
    color : black;
}
</style>