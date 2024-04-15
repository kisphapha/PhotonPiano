<template>
     <HorNavbar class="min-h-[7.5vh]" />
        <div class="flex">
          <VerNavBar class="min-h-[92.5vh]" />
          <div class="w-full overflow-y-auto h-[92.5vh]">
            <RouterView style="min-height: 35vh;" />
          </div>
        </div>
</template>

<script>
import HorNavbar from '../components/Common/HorNavbar.vue'
import VerNavBar from '../components/Staff/VerNavBar.vue'

export default {
    name : "StaffLayout",
    components: { HorNavbar, VerNavBar },
}
</script>

<style></style>