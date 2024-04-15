<template>
    <div id="hor_nav" class="flex justify-end text-xl bg-black text-white sticky top-0 z-[1000]">
        <RouterLink class="p-2 ml-6" to="/">Home</RouterLink>
        <RouterLink v-if='this.student_status == "InClass"' class="p-2 ml-6" to="/class">Class</RouterLink>
        <RouterLink class="p-2 ml-6" to="/about">About</RouterLink>
        <RouterLink v-if="this.user" class="p-2 ml-6" to="/forum">Forum</RouterLink>
        <button v-if='this.student_status == "Unregistered"' class="p-2 ml-6    text-blue-400"
            @click="changeEnrollingStatusToEnroll">Enroll</button>
        <button v-if="!this.user" class="p-2 ml-6 text-cyan-400" @click="triggerOpenPopup">Login</button>
        <RouterLink v-if="this.user" class="p-2 ml-6" :to="`/student`">
            <div class="flex gap-2">
                {{ this.user.name }}
                <img :src="user.picture" class="w-8 h-8 rounded-full" alt="Profile Picture" />
            </div>
        </RouterLink>
        <button v-if='this.user' class="p-2 ml-6" @click="logout">Logout</button>
    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name: "HorNavbar",
    data() {
        return {
            user: null,
            student_status: ""
        }
    },
    inject: ["eventBus"],
    methods: {
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
        async refresh() {
            const userPromise = new Promise((resolve) => {
                this.eventBus.emit("get-user", resolve);

            });
            const user = await userPromise;
            this.user = user;
            this.student_status = this.user?.students[0]?.status ?? ""
        },
        logout() {
            this.eventBus.emit("logout")
        },
        changeEnrollingStatusToEnroll() {
            this.eventBus.emit("update-home-page-enrolling-status", "Enrolling")
        }
    },
    mounted() {
        this.eventBus.on("update-header", async () => {
            this.refresh()
        })
        if (localStorage.token) {
            this.refresh()
        }
    },
}
</script>
<style>
#hor_nav .router-link-active {
    border-radius: 10px;
    background-color: white;
    color: black;
}
</style>