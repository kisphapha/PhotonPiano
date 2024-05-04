<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Instructors Filter</div>
        <div class="mt-2 overflow-y-auto h-96">
            <div class=" flex gap-2">
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
                <div class="p-2 w-48">Classes Teaching From</div>
                <input class="p-2 rounded-lg border" v-model="this.filterDto.teachFrom" type="number">
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-48">Classes Teaching To</div>
                <input class="p-2 rounded-lg border" v-model="this.filterDto.teachTo" type="number">
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
                <div class="p-2 w-48">Contribute From</div>
                <input class="p-2 rounded-lg border" v-model="this.filterDto.contributeFrom" type="number">
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2 w-48">Contribute To</div>
                <input class="p-2 rounded-lg border" v-model="this.filterDto.contributeTo" type="number">
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
                    <option value="all">All</option>
                    <option value="Active">Active</option>
                    <option value="Inactive">Inactive</option>
                    <option value="Fired">Fired</option>
                </select>
            </div>
        </div>
        <div class="flex gap-4 justify-center mt-4">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="action(this.filterDto)">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
            <button class="p-2 text-green-400 underline font-bold" @click="resetDefault">Reset</button>
        </div>

    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name: "InstructorFilterForm",
    inject: ['eventBus'],
    props: ['id', 'name', 'level', 'joinFrom', 'joinTo', 'email', 'phone', 'teachFrom', 'teachTo', 'contributeFrom', 'contributeTo', 'status', 'close', 'action'],
    data() {
        return {
            years: [

            ],
            filterDto: {
                id: null,
                name: null,
                level: 0,
                joinFrom: null,
                joinTo: null,
                teachFrom: null,
                teachTo: null,
                contributeFrom: null,
                contributeTo: null,
                email: "",
                phone: "",
                status: "all"
            },

        }
    },
    mounted() {
        this.getYears()
        this.presetFilter()

    },
    methods: {
        getYears() {
            let year = new Date().getFullYear()
            this.years.push(year)
            for (var i = 0; i < 3; i++) {
                year -= 1
                this.years.push(year)
            }
        },
        handleFilter() {
            //this.eventBus.emit("handle-filter-schedule-classes-page",this.filterDto)
        },
        presetFilter() {
            this.filterDto.id = this.id
            this.filterDto.name = this.name
            this.filterDto.level = this.level
            this.filterDto.joinFrom = this.joinFrom
            this.filterDto.joinTo = this.joinTo
            this.filterDto.contributeFrom = this.contributeFrom
            this.filterDto.contributeTo = this.contributeTo
            this.filterDto.teachFrom = this.teachFrom
            this.filterDto.teachTo = this.teachTo
            this.filterDto.email = this.email
            this.filterDto.phone = this.phone
            this.filterDto.status = this.status
        },
        resetDefault() {
            this.filterDto.id = null
            this.filterDto.name = null
            this.filterDto.level = 0
            this.filterDto.joinFrom = null
            this.filterDto.joinTo = null
            this.filterDto.contributeFrom = null
            this.filterDto.contributeTo = null
            this.filterDto.teachFrom = null
            this.filterDto.teachTo = null
            this.filterDto.email = null
            this.filterDto.phone = null
            this.filterDto.status = "all"
        }

    }
}
</script>
<style></style>