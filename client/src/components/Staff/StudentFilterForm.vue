<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Students Filter</div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">ID</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.id" type="number">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Name</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.name" type="text">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Email</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.email" type="text">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Phone</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.phone" type="text">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Class Name</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.className" type="text">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Join From</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.joinFrom" type="date">
        </div>
        <div class="mt-2 flex gap-2">
            <div class="p-2 w-48">Join To</div>
            <input class="p-2 rounded-lg border" v-model="this.filterDto.joinTo" type="date">
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
            <div class="p-2 w-48">Status</div>
            <select class="p-2 rounded-lg border" v-model="this.filterDto.status">
                <option value="0">All</option>
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
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold" @click="action(this.filterDto)">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
        </div>
        
    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name: "StudentFilterForm",
    inject : ['eventBus'],
    props : ['id','name','level','joinFrom','joinTo','email','phone','className','status','close','action'],
    data(){
        return {
            years: [

            ],
            filterDto : {
                id : null,
                name : null,
                level : 0,
                joinFrom : null,
                joinTo : null,
                email : "",
                phone : "",
                className : "",
                status : "all"
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
            this.filterDto.joinFrom = this.joinFrom
            this.filterDto.joinTo = this.joinTo
            this.filterDto.email = this.email
            this.filterDto.phone = this.phone
            this.filterDto.className = this.className
            this.filterDto.status = this.status
        },
        
    }
}
</script>
<style></style>