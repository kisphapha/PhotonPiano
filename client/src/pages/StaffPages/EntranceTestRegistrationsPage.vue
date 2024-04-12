<template>
    <div class="mt-4 ml-4 mr-4">
        <div class="text-4xl font-bold">Entrance Test Registrations</div>
        <div class="mt-4 flex place-content-between italic">
            <div>Total registrations left : <span class="ml-4 font-bold">{{ totalRegistrations - thisYearRegistration }}</span></div>
            <div>This year accepted : <span class="ml-4 font-bold">{{ thisYearRegistration }}</span></div>
            <div>Target : <span class="ml-4 font-bold">{{ centerMaxValue }}</span></div>
        </div>
        <div v-if="thisYearRegistration > centerMaxValue" class="bg-red-200 p-6 mt-2 rounded-lg">
            <div class="font-bold mr-2">WARNING : </div>
            <div>Number of students accepted has suppassed the center's target.
                Make sure this is intentional and consider to stop accepting more!</div>
        </div>
        <div class="flex gap-2 place-content-between mt-6">
            <button @click="toggleAutomaticPopup" class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                Automatic Accepting
            </button>
            <div class="flex gap-2">
                <input class="border p-1 rounded-md w-64" type="text" v-model="keyword_name"
                    placeholder="Search by name" @change="handleKeywordChange">
                <button class="p-1 bg-slate-100 rounded-lg">
                    <span class="material-icons p-1">
                        search
                    </span>
                </button>
            </div>

        </div>
        <table id="staff-table" class="mt-2 w-full">
            <colgroup>
                <col style="width: 5%;" />
                <col style="width: 15%;" />
                <col style="width: 15%;" />
                <col style="width: 15%;" />
                <col style="width: 20%;" />
                <col style="width: 20%;" />
                <col style="width: 10%;" />
            </colgroup>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>Short Desc</th>
                    <th>Registrations Time</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="registration in this.registrations" :key="registration.id">
                    <td>{{ registration.id }}</td>
                    <td>{{ registration.name }}</td>
                    <td>{{ registration.email }}</td>
                    <td>{{ registration.phone }}</td>
                    <td>{{ registration.shortDesc }}</td>
                    <td>{{ registration.registrationTime }}</td>
                    <td>
                        <div class="flex gap-2">
                            <span class="material-icons text-lime-500 text-3xl">
                                check_circle
                            </span>
                            <span class="material-icons text-red-500 text-3xl">
                                cancel
                            </span>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="flex gap-4 justify-center mt-4">
            <button @click="movePage(false)">
                <span class="material-icons p-1">arrow_back_ios</span>
            </button>
            <div class="flex gap-2 ">
                <input class="border p-1 rounded-md w-16" type="number" v-model="currentPage" min="1"
                    @change="handlePageChange">
                <div class="p-1"> / {{ this.totalPage }}</div>
            </div>
            <button @click="movePage(true)">
                <span class="material-icons p-1">arrow_forward_ios</span>
            </button>
        </div>
        <div v-if="isOpenAutomaticPopup" class="popup-overlay">
            <div class="overflow-y-auto popup-content sticky top-1/4 flex justify-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAutomaticPopup">X</button>
                    <AutoAcceptPopup :totalRegistrations="totalRegistrations" :centerMaxValue="centerMaxValue" :thisYearRegistration="thisYearRegistration"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import AutoAcceptPopup from '../../components/Staff/AutoAcceptPopup.vue'

export default {
    name: "EntranceTestRegistrationsPage",
    inject: ["eventBus"],
    components : {AutoAcceptPopup},
    data() {
        return {
            registrations: [
                {
                    id: 1,
                    name: "Hung dep trai",
                    email: "hung@abc.com",
                    phone: "0998765413",
                    shortDesc: "I'm very good at piano",
                    registrationTime: '2024-01-01 12:00:00'
                }, {
                    id: 2,
                    name: "Hung dep trai",
                    email: "hung@abc.com",
                    phone: "0998765413",
                    shortDesc: "Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam Spam ",
                    registrationTime: '2024-01-01 12:00:00'
                },
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            keyword_name: "",
            isOpenAutomaticPopup : false,
            totalRegistrations : 2000,
            thisYearRegistration : 1150,
            centerMaxValue : 1000,
        }
    },
    methods: {
        handlePageChange() {

        },
        movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                this.handlePageChange()
            }
        },
        handleKeywordChange() {

        },
        toggleAutomaticPopup(){
            this.isOpenAutomaticPopup = !this.isOpenAutomaticPopup
        }
    },
    mounted(){
        this.eventBus.on("toggle-auto-accept-popup-registration-page",() => {
            this.toggleAutomaticPopup()
        })
    }
}

</script>

<style>
.popup-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    z-index: 2000;
}
</style>