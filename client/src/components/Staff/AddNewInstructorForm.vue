<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold">Add New Instructor</div>
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
                <div class="p-2 w-32">Level</div>
                <select class="p-2 rounded-lg border w-full" v-model="this.level">
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>
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
    name: "AddNewInstructorForm",
    props: ['close'],
    inject: ['eventBus'],
    data() {
        return {
            name : "",
            level : 1,
            email : "",
            phone : "",
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