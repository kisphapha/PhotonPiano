<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold">Add New Class</div>
        <div class="text-xl mb-6">Period year {{ new Date().getFullYear() }}</div>
        <div class="mt-4 overflow-y-auto max-h-96">
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Name</div>
                <input type="text" class="p-2 rounded-lg border w-full" v-model="this.className" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Start Date</div>
                <input type="date" class="p-2 rounded-lg border w-full" v-model="this.startDate" />
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Instructor</div>
                <select class="p-2 rounded-lg border w-full" v-model="this.selectedInstructor">
                    <option value="0">None</option>
                    <option v-for="instructor in instructors" :key="instructor.id" :value="instructor.id">
                        {{ instructor.user.name }}
                    </option>
                </select>
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Level</div>
                <select class="p-2 rounded-lg border w-full" v-model="this.selectedLevel">
                    <option value="1">1</option>
                    <option value="2">2</option>
                    <option value="3">3</option>
                    <option value="4">4</option>
                    <option value="5">5</option>
                </select>
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-32">Class Size</div>
                <input type="number" class="p-2 rounded-lg border w-full" v-model="this.classSize" />
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
    name: "AddNewClassForm",
    props: ['close'],
    inject: ['eventBus'],
    data() {
        return {
            lessons: [

            ],
            shifts: [
                "7:00 - 8:30",
                "8:45 - 10:15",
                "10:30 - 12:00",
                "12:30 - 14:00",
                "14:15 - 15:45",
                "16:00 - 17:30",
                "18:00 - 19:30",
                "19:45 - 21:15",
            ],
            instructors: [
                {
                    id: 1,
                    user: {
                        name: "Hello world"
                    }
                }
            ],
            maxClassSize: 25,
            className: '',
            startDate: '',
            selectedLevel: 1,
            selectedInstructor: 0,
            classSize : 25
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