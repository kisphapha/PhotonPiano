<template>
    <div id="banner" class="bg-black text-white font-bold">
        <div class="flex justify-end ml-4 mr-4 text-xl">
            <RouterLink class="p-2 ml-6" to="/">Home</RouterLink>
            <RouterLink class="p-2 ml-6" to="/about">About</RouterLink>
        </div>
        <div class="shadow text-4xl pl-8 pt-28 pb-32 w-full italic">Kim Cương's Piano Master Class</div>
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