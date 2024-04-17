<template>
    <div id="ver_nav" class="flex min-w-max">
        <div class="bg-slate-100 flex flex-col overflow-y-auto h-[92.5vh]">
            <RouterLink class="px-4 py-3 flex" to="/manage">
                <span class="material-icons">edit</span>
                <span class="ml-3" v-if="!isCollapsed">Dashboard</span>
            </RouterLink>
            <button :class="this.buttonEntranceCss" @click="expandEntrance(false)">
                <span class="material-icons">key</span>
                <span class="ml-3" v-if="!isCollapsed">Entrance Test</span>
            </button>
            <div v-if='isExpandEntrance && !isCollapsed' id="ver-child-nav" class="flex flex-col bg-slate-200">
                <RouterLink  class="px-4 py-3 flex" to="/manage/entrance-test/registration">
                    <span class="ml-3" v-if="!isCollapsed">Registration</span>
                </RouterLink>
                <RouterLink class="px-4 py-3 flex" to="/manage/entrance-test/arrange">
                    <span class="ml-3" v-if="!isCollapsed">Arranging</span>
                </RouterLink>
            </div>
            <button :class="this.buttonScheduleCss" @click="expandSchedule(false)">
                <span class="material-icons">calendar_month</span>
                <span class="ml-3" v-if="!isCollapsed">Schedule</span>
            </button>
            <div v-if='isExpandSchedule && !isCollapsed' id="ver-child-nav" class="flex flex-col bg-slate-200">
                <RouterLink  class="px-4 py-3 flex" to="/manage/schedule/all">
                    <span class="ml-3" v-if="!isCollapsed">Aggregate</span>
                </RouterLink>
                <RouterLink class="px-4 py-3 flex" to="/manage/schedule/classes">
                    <span class="ml-3" v-if="!isCollapsed">Classes</span>
                </RouterLink>
            </div>
            <RouterLink class="px-4 py-3 flex" to="/manage/class">
                <span class="material-icons">school</span>
                <span class="ml-3" v-if="!isCollapsed">Classes</span>
            </RouterLink>
            <button :class="this.buttonStudentCss" @click="expandStudent(false)">
                <span class="material-icons">person</span>
                <span class="ml-3" v-if="!isCollapsed">Students</span>
            </button>
            <div v-if='isExpandStudent && !isCollapsed' id="ver-child-nav" class="flex flex-col bg-slate-200">
                <RouterLink  class="px-4 py-3 flex" to="/manage/student/list">
                    <span class="ml-3" v-if="!isCollapsed">List</span>
                </RouterLink>
                <RouterLink class="px-4 py-3 flex" to="/manage/student/re-class">
                    <span class="ml-3" v-if="!isCollapsed">Class changing</span>
                </RouterLink>
            </div>
            <RouterLink class="px-4 py-3 flex" to="/manage/instructors">
                <span class="material-icons">badge</span>
                <span class="ml-3" v-if="!isCollapsed">Instructors</span>
            </RouterLink>
            <RouterLink class="px-4 py-3 flex" to="/manage/staff">
                <span class="material-icons">manage_accounts</span>
                <span class="ml-3" v-if="!isCollapsed">Staffs</span>
            </RouterLink>
            <RouterLink class="px-4 py-3 flex" to="/manage/settings">
                <span class="material-icons">settings</span>
                <span class="ml-3" v-if="!isCollapsed">Settings</span>
            </RouterLink>
            <button class="px-4 py-3 flex">
                <span class="material-icons">logout</span>
                <span class="ml-3" v-if="!isCollapsed">Logout</span>
            </button>

        </div>
        <div class="bg-transparent">
            <button class="p-3 h-[10vh] bg-slate-50 rounded-r-xl material-icons" @click="toggleIsCollapse">
                {{ isCollapsed ? "arrow_forward_ios" : "arrow_back_ios" }}
            </button>
        </div>
    </div>
</template>

<script>
import { RouterLink } from 'vue-router';

//import { RouterLink } from 'vue-router';

export default {
    name: "HorNavbar",
    inject: ["eventBus"],
    data() {
        return {
            isCollapsed: false,
            buttonEntranceCss: "px-4 py-3 flex",
            buttonStudentCss: "px-4 py-3 flex",
            buttonScheduleCss: "px-4 py-3 flex",
            isExpandEntrance: false,
            isExpandStudent: false,
            isExpandSchedule: false,
        }
    },
    methods: {
        toggleIsCollapse() {
            this.isCollapsed = !this.isCollapsed
        },
        expandEntrance(reverse) {
            if (!reverse){
                this.isExpandEntrance = true
                this.buttonEntranceCss += " router-link-active"
                if (!window.location.pathname.startsWith("/manage/entrance-test"))
                    this.$router.push("/manage/entrance-test/registration")
            } else {
                this.isExpandEntrance = false
                this.buttonEntranceCss = "px-4 py-3 flex"
            }           
        },
        expandStudent(reverse) {
            if (!reverse){
                this.isExpandStudent = true
                this.buttonStudentCss += " router-link-active"
                if (!window.location.pathname.startsWith("/manage/student"))
                    this.$router.push("/manage/student/list")
            } else {
                this.isExpandStudent = false
                this.buttonStudentCss = "px-4 py-3 flex"
            }    
        },
        expandSchedule(reverse) {
            if (!reverse){
                this.isExpandSchedule = true
                this.buttonScheduleCss += " router-link-active"
                if (!window.location.pathname.startsWith("/manage/schedule"))
                    this.$router.push("/manage/schedule/all")
            } else {
                this.isExpandSchedule = false
                this.buttonScheduleCss = "px-4 py-3 flex"
            }    
        },
        checkPath(){
            if (window.location.pathname.startsWith("/manage/entrance-test")){
                this.expandEntrance(false)
            }
            if (window.location.pathname.startsWith("/manage/student")){
                this.expandStudent(false)
            }
            if (window.location.pathname.startsWith("/manage/schedule")){
                this.expandSchedule(false)
            }
        }
    },
    mounted() {
        this.$router.afterEach((to, from) => {
            if (!to.path.startsWith("/manage/entrance-test")){
                this.expandEntrance(true)
            }
            if (!to.path.startsWith("/manage/student")){
                this.expandStudent(true)
            }
            if (!to.path.startsWith("/manage/schedule")){
                this.expandSchedule(true)
            }
        });
        this.checkPath()
    }
}
</script>
<style>
#ver_nav .router-link-active {
    background-color: rgb(18, 16, 31);
    color: white;
    font-weight: bold;
}

#ver-child-nav .router-link-active {
    background-color: rgb(87, 74, 160);
    color: white;
    font-weight: bold;
}
</style>