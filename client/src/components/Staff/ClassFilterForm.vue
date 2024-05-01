<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Classes Filter</div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">ID</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.id" type="number">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Name</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.name" type="text">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Level</div>
            <select class="p-2 rounded-lg border" v-model="this.filterDto.level">
                <option value="0">All</option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
            </select>
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Start Date</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.startDate" type="date">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">End Date</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.endDate" type="date">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Students number from</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.studentsFrom" type="number">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Students number to</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.studentsTo" type="number">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Is Announced</div>
            <select class="p-2 rounded-lg border" v-model="this.filterDto.isAnnounced">
                <option value="all">All</option>
                <option value="true">True</option>
                <option value="false">False</option>
            </select>
        </div>
        
        <div class="flex gap-4 justify-center mt-4">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold" @click="action(this.filterDto)">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
        
    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name: "ClassFilterForm",
    inject : ['eventBus'],
    props : ['id','name','level','startDate','endDate','studentsFrom','studentsTo','isAnnounced','close','action'],
    data(){
        return {
            years: [

            ],
            filterDto : {
                id : null,
                name : null,
                level : 0,
                startDate : null,
                endDate : null,
                studentsFrom : null,
                studentsTo : null,
                isAnnounced : "all"
            },
            
        }
    },
    mounted(){
        this.getYears()
        this.presetFilter()
        
    },
    methods : {
        getYears() {
            let year = new Date().getFullYear()
            this.years.push(year)
            for (var i = 0; i < 3; i++) {
                year -= 1
                this.years.push(year)
            }
        },
        handleFilter(){
            //this.eventBus.emit("handle-filter-schedule-classes-page",this.filterDto)
        },
        presetFilter(){
            this.filterDto.id = this.id
            this.filterDto.name = this.name
            this.filterDto.level = this.level
            this.filterDto.startDate = this.startDate
            this.filterDto.endDate = this.endDate
            this.filterDto.studentsFrom = this.studentsFrom
            this.filterDto.studentsTo = this.studentsTo
            this.filterDto.isAnnounced = this.isAnnounced
        },
        
    }
}
</script>
<style></style>