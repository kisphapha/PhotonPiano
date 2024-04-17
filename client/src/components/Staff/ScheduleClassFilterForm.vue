<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Class Schedule Filter</div>
        <div class="mt-2 flex gap-2">
            <div class="p-2">ID</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.id" type="number">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2">Name</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.name" type="text">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2">Level</div>
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
            <div class="p-2">Period</div>
            <select v-model="this.filterDto.period" class="rouded-lg border p-2">
                <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
            </select>
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2">Is Scheduled</div>
            <select class="p-2 rounded-lg border" v-model="this.filterDto.isScheduled">
                <option value="all">All</option>
                <option value="true">True</option>
                <option value="false">False</option>
            </select>
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2">Is Announced</div>
            <select class="p-2 rounded-lg border" v-model="this.filterDto.isAnnounced">
                <option value="all">All</option>
                <option value="true">True</option>
                <option value="false">False</option>
            </select>
        </div>
        
        <div class="flex gap-4 justify-center mt-4">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold" @click="handleFilter">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="toggleFilterScheduleClassPopupSchedulePage">Cancel</button>
        </div>
        
    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name: "ScheduleClassFilterForm",
    inject : ['eventBus'],
    props : ['period','id','name','level','isScheduled','isAnnounced'],
    data(){
        return {
            years: [

            ],
            filterDto : {
                id : null,
                name : null,
                period : new Date().getFullYear(),
                level : 0,
                isScheduled : "all",
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
        toggleFilterScheduleClassPopupSchedulePage(){
            this.eventBus.emit("toggle-filter-schedule-class-popup-schedule-classes-page")
        },
        handleFilter(){
            this.eventBus.emit("handle-filter-schedule-classes-page",this.filterDto)
        },
        presetFilter(){
            this.filterDto.id = this.id,
            this.filterDto.name = this.name,
            this.filterDto.level = this.level,
            this.filterDto.period = this.period,
            this.filterDto.isAnnounced = this.isAnnounced,
            this.filterDto.isScheduled = this.isScheduled
        },
        
    }
}
</script>
<style></style>