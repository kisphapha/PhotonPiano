<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold">Add New Staff</div>
        <div class="mt-4 overflow-y-auto max-h-96">
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Name</div>
                <input type="text" class="p-2 rounded-lg border w-full" v-model="this.name" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Email</div>
                <input type="text" class="p-2 rounded-lg border w-full" v-model="this.email" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Phone</div>
                <input type="text" class="p-2 rounded-lg border w-full" v-model="this.phone" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Gender</div>
                <select class="p-2 rounded-lg border w-full" v-model="this.gender">
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                    <option value="Other">Other</option>
                </select>
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Address</div>
                <input type="text" class="p-2 rounded-lg border w-full" v-model="this.address" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">DoB</div>
                <input type="date" class="p-2 rounded-lg border w-full" v-model="this.doB" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Bank Account</div>
                <input type="text" class="p-2 rounded-lg border w-full" v-model="this.bankAccount" />
            </div>
        </div>
        <div class="mt-4 flex justify-center gap-8">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="close">Create</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: "AddNewStaffForm",
    props: ['close'],
    inject: ['eventBus'],
    data() {
        return {
            name : "",
            gender : "Male",
            email : "",
            phone : "",
            doB : null,
            address : "",
            bankAccount : "",
        }
    },
    methods: {

    },
    mounted() {
        //this.fetchLessons()
    }
}
</script>

<style></style>