<template>
    <div id="ver_nav" class="bg-slate-200 inline-flex flex-col sticky top-0 z-[600] min-w-max">
        <RouterLink class="px-4 py-2" to="/">
            <span class="material-icons mr-4">edit</span>Dashboard
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">key</span>Entrance Test
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">calendar_month</span>Schedule
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">school</span>Classes
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">person</span>Students
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">badge</span>Instructors
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">manage_accounts</span>Staffs
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">settings</span>Settings
        </RouterLink>
        <RouterLink class="px-4 py-2" to="/student">
            <span class="material-icons mr-4">logout</span>Logout
        </RouterLink>
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
            isCollapsed: true
        }
    }
}
</script>
<style>
#ver_nav .router-link-active {
    background-color: rgb(18, 16, 31);
    color: white;
    font-weight: bold;
}
</style>