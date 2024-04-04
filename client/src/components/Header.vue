<template>
    <div id="banner" class="bg-black text-white font-bold">
        <!-- <div v-if="user">
            <span>{{ user.name }}</span>
            <img :src="user.profilePicture" alt="Profile Picture" />
        </div> -->
        <div class="flex justify-end ml-4 mr-4 text-xl">
            <RouterLink class="p-2 ml-6" to="/">Home</RouterLink>
            <RouterLink class="p-2 ml-6" to="/about">About</RouterLink>
            <RouterLink v-if="this.user" class="p-2 ml-6" to="/forum">Forum</RouterLink>
            <button v-if='this.user_detail && this.user_detail.status == "Unregistered"' class="p-2 ml-6    text-blue-400" @click="triggerOpenPopup">Enroll</button>
            <button  v-if="!this.user" class="p-2 ml-6 text-cyan-400" @click="triggerOpenPopup">Login</button>
            <RouterLink v-if="this.user" class="p-2 ml-6" :to="`/student`">
                <div class="flex gap-2">
                    {{this.user.name}}
                    <img :src="user.picture" class="w-8 h-8 rounded-full" alt="Profile Picture"/>
                </div>
            </RouterLink>
        </div>
        <div class="text-5xl pl-8 pt-28 w-full italic">PHOTON PIANO</div>
        <div class="shadow text-2xl pl-8 pb-32 font-bold">Kim Cương's Piano Master Class</div>
    </div>
</template>

<script>
import axios from 'axios';
import { RouterLink } from 'vue-router';

export default {
    name : "Header",
    data () {
        return {
            user : null,
            user_detail : null
        }
    },
    inject : ["eventBus"],
    methods : {
        triggerOpenPopup() {
            this.eventBus.emit("open-login-popup")
        },
        async fetchStudentDetail(id) {
            const response = await axios.get(import.meta.env.VITE_API_URL + '/api/Student/' + id)
            if (response.data) {
                return response.data             //console.log(this.user_detail)
            }  
            return null;        
        },
        async refresh(){
            const userPromise = new Promise((resolve) => {
                this.eventBus.emit("get-user", resolve);

            });
            const user = await userPromise;
            this.user = user;

            if (user) {
                this.user_detail = await this.fetchStudentDetail(user.students[0].id);
            }
        }
    },
    mounted() {
        this.eventBus.on("update-header", async () => {
            this.refresh()
        })
    },
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