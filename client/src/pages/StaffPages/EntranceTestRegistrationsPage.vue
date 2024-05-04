<template>
    <div class="mt-4 ml-4 mr-4">
        <div class="text-4xl font-bold">Entrance Test Registrations</div>
        <div class="mt-4 flex place-content-between italic">
            <div>Total registrations left : <span class="ml-4 font-bold">{{ actualTotalRegistrationsLeft
                    }}</span></div>
            <div>This year accepted : <span class="ml-4 font-bold">{{ thisYearRegistration }}</span></div>
            <div>Target : <span class="ml-4 font-bold">{{ centerMaxValue }}</span></div>
        </div>
        <div v-if="thisYearRegistration > centerMaxValue" class="bg-red-200 p-6 mt-2 rounded-lg">
            <div class="font-bold mr-2">WARNING : </div>
            <div>Number of students accepted has suppassed the center's target.
                Make sure this is intentional and consider to stop accepting more!</div>
        </div>
        <div class="flex gap-2 place-content-between mt-6">
            <button @click="toggleAutomaticPopup"
                class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                Automatic Accepting
            </button>
            <div class="flex gap-2">
                <input class="border p-1 rounded-md w-64" type="text" v-model="keyword_name"
                    placeholder="Search by name">
                <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
                    <span class="material-icons p-1">
                        search
                    </span>
                </button>
            </div>

        </div>
        <table id="staff-table" class="mt-2 w-full" v-if="this.registrations.length > 0">
            <thead>
                <tr>
                    <th>
                        <div class="w-4">ID</div>
                    </th>
                    <th>
                        <div class="w-8">Name</div>
                    </th>
                    <th>
                        <div class="w-32">Email</div>
                    </th>
                    <th>
                        <div class="w-16">Phone</div>
                    </th>
                    <th>
                        <div class="w-64">Short Desc</div>
                    </th>
                    <th>Registrations Time</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="registration in this.registrations" :key="registration.id">
                    <td>{{ registration.id }}</td>
                    <td>
                        <div class="w-8">{{ registration.user.name }}</div>
                    </td>
                    <td>
                        <div class="break-words max-w-32">{{ registration.user.email }}</div>
                    </td>
                    <td>
                        <div class="w-16">{{ registration.user.phone }}</div>
                    </td>
                    <td>
                        <div class="w-64 overflow-y-auto break-words max-h-48">{{ registration.shortDesc }}</div>
                    </td>
                    <td>{{ registration.registrationDate.substring(0, 10) + " " +
                registration.registrationDate.substring(11, 19) }}</td>
                    <td>
                        <div class="flex gap-2">
                            <button class="material-icons text-lime-500 text-3xl" @click="handleAccept(registration.id)">
                                check_circle
                            </button>
                            <button class="material-icons text-red-500 text-3xl" @click="handleReject(registration.id)">
                                cancel
                            </button>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="flex gap-4 justify-center mt-4" v-if="this.registrations.length > 0">
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
        <div class="italic p-2 text-center" v-if="this.registrations.length == 0">
            There is no data
        </div>
        <div v-if="isOpenAutomaticPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAutomaticPopup">X</button>
                    <AutoAcceptPopup :totalRegistrations="totalRegistrationsLeft + thisYearRegistration"
                        :centerMaxValue="centerMaxValue" :thisYearRegistration="thisYearRegistration"
                        :close="toggleAutomaticPopup" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import AutoAcceptPopup from '../../components/Staff/EntranceTests/AutoAcceptPopup.vue'
import axios from 'axios'

export default {
    name: "EntranceTestRegistrationsPage",
    inject: ["eventBus"],
    props: ['getStaffUser'],
    components: { AutoAcceptPopup },
    data() {
        return {
            registrations: [],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            keyword_name: "",
            isOpenAutomaticPopup: false,
            totalRegistrationsLeft: 850,
            actualTotalRegistrationsLeft : 850,
            thisYearRegistration: 1150,
            centerMaxValue: 1000,
            user: null,
        }
    },
    methods: {
        async handlePageChange() {
            if (this.currentPage > this.totalPage){
                this.currentPage = this.totalPage
            }
            if (this.currentPage < 1){
                this.currentPage = 1
            }
            await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        async movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                await this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                await this.handlePageChange()
            }
        },
        async handleSearch(){
            await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        toggleAutomaticPopup() {
            this.isOpenAutomaticPopup = !this.isOpenAutomaticPopup
        },
        async refresh() {
            const userPromise = new Promise((resolve) => {
                this.eventBus.emit("get-staff-user", resolve);
            });
            const user = await userPromise;
            this.user = user
            await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        async fetchRegistration(pageNumber, pageSize, name) {
            const response = await axios.get(import.meta.env.VITE_API_URL + `/api/Student/?status=PendingRegistration&pageSize=${pageSize}&pageNumber=${pageNumber}&name=${name}`)

            if (response.data) {
                this.registrations = response.data.results
                this.registrations.sort((a,b) => a.registrationDate - b.registrationDate)
                this.totalPage = response.data.totalPages
                this.totalRegistrationsLeft = response.data.totalRecords
            }

            const getActualTotal = await axios.get(import.meta.env.VITE_API_URL + `/api/Student/?status=PendingRegistration&pageSize=1&pageNumber=1`)

            if (getActualTotal.data){
                this.actualTotalRegistrationsLeft = getActualTotal.data.totalRecords
            }

            const totalAcceptedRegistrations = await axios.get(import.meta.env.VITE_API_URL + `/api/EntranceTest/${new Date().getFullYear()}/year`)

            if (totalAcceptedRegistrations.data) {
                this.thisYearRegistration = totalAcceptedRegistrations.data.length
            }
        },
        async handleAccept(id){
            //change status
            try {
                await axios.patch(import.meta.env.VITE_API_URL + `/api/Student/${id}/change-status?status=Accepted`)
                //
                await axios.post(import.meta.env.VITE_API_URL + `/api/EntranceTest`,{
                    studentId : id
                })
                this.eventBus.emit("open-result-dialog",{
                    message : "Success!!",
                    type : "Success"
                })
                await this.refresh()
            }
            catch (e) {
                this.eventBus.emit("open-result-dialog",{
                    message : e.response.data.ErrorMessage,
                    type : "Error"
                })
            }
        },
        async handleReject(id){
            //change status
            try {
                await axios.patch(import.meta.env.VITE_API_URL + `/api/Student/${id}/change-status?status=Unregistered`)        
                this.eventBus.emit("open-result-dialog",{
                    message : "Rejected this user!!",
                    type : "Success"
                })
                this.refresh()
            }
            catch (e) {
                this.eventBus.emit("open-result-dialog",{
                    message : e.response.data.ErrorMessage,
                    type : "Error"
                })
            }
        }
    },
    mounted() {
        this.eventBus.on("refresh-registration-page", async () => {
            await this.refresh()
        })
        this.refresh()
    }
}

</script>

<style></style>