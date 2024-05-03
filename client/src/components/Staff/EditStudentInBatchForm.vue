<template>
   <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Edit Status of Students In Batch</div>
        <div class="italic mt-2">
            Selected <span class="font-bold">{{ this.selectedStudentIds.length }} students</span>
        </div>
        <div class="mt-4 text-lg flex gap-4">
            <span class="p-2">STATUS :</span>
            <select class="p-2 rounded-lg border" v-model="this.selectedStatus">
                <option value="InClass">In Class</option>
                <option value="Unregistered">Unregistered</option>
                <option value="PendingRegistration">Pending Registration</option>
                <option value="Accepted">Accepted</option>
                <option value="EntranceTesting">Entrance Testing</option>
                <option value="WaitingForClassPlacement">Waiting Class</option>
                <option value="Dropped">Dropped</option>
            </select>
        </div>
        <div class="flex gap-4 justify-center mt-4">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold" @click="null">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
   </div>
</template>

<script>
export default {
    name : "EditStudentInBatchForm",
    props : ['close','selectedStudentIds'],
    data() {
        return {
            selectedStatus : "InClass"
        }
    }
}
</script>

<style>

</style>