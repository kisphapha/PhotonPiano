<template>
    <div id="banner" class="bg-black text-white font-bold">
        <!-- <div v-if="user">
            <span>{{ user.name }}</span>
            <img :src="user.profilePicture" alt="Profile Picture" />
        </div> -->      
        <div class="text-5xl pl-8 pt-20 w-full italic">PHOTON PIANO</div>
        <div class="shadow text-2xl pl-8 pb-24 font-bold">Kim Cương's Piano Master Class</div>
    </div>
</template>

<script>

export default {
    name : "Header",
    
}
</script>
<style>
#banner{
    background-image: url("../../assets/background.png");
    background-position: center;
    background-repeat: no-repeat;
}

</style>