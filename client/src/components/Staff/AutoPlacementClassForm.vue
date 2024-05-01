<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold">Auto Placement Wizard</div>
        <div class="text-xl mb-6">Period year {{ new Date().getFullYear() }}</div>
        <div class="mt-4 flex gap-8">
            <div class="p-2">
                <div>Number of unclassed students : {{this.students.length}}</div>
                <ul class="list-disc ml-4">
                    <li class="text-blue-400">Level 1 : {{this.students.filter(s => s.level == 1).length}}</li>
                    <li class="text-pink-400">Level 2 : {{this.students.filter(s => s.level == 2).length}}</li>
                    <li class="text-red-400">Level 3 : {{this.students.filter(s => s.level == 3).length}}</li>
                    <li class="text-black">Level 4 : {{this.students.filter(s => s.level == 4).length}}</li>
                    <li class="text-gray-400">Level 5 : {{this.students.filter(s => s.level == 5).length}}</li>
                </ul>
            </div>
            <div class="p-2">
                <div class="font-bold flex gap-4">
                    Auto Create New Class
                    <input type="checkbox" v-model="isGenerateClass">
                </div>
                <div class="mt-2 flex gap-2" v-if="isGenerateClass">
                    <div class="p-2 w-32">Class Size</div>
                    <input type="number" class="p-2 rounded-lg border w-full" v-model="this.classSize" />
                </div>
                
                <div class="mt-2 flex gap-2" v-if="isGenerateClass">
                    <div class="p-2 w-32">Start Date</div>
                    <input type="date" class="p-2 rounded-lg border w-full" v-model="this.startDate" />
                </div>
            </div>
        </div>
        <div class="mt-4 flex justify-center gap-8">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="close">Begin</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: "AutoPlacementClassForm",
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
            maxClassSize: 25,
            classSize : 20,
            startDate : null,
            isGenerateClass : false,
            students : [
                {
                    id : 1,
                    level : 1,
                },
                {
                    id : 1,
                    level : 2,
                },
                {
                    id : 1,
                    level : 2,
                },
                {
                    id : 1,
                    level : 3,
                },
                {
                    id : 1,
                    level : 1,
                }
            ]
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