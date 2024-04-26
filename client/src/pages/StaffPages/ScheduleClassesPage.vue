<template>
    <div class="mt-4 ml-4 mr-4">
        <div v-if="selectedClassId == 0">
            <div class="text-4xl font-bold">Scheduling Each Class</div>
            <div class="flex gap-2 place-content-between mt-6">
                <div class="flex gap-4">
                    <button @click='this.$router.push("/manage/schedule/all")'
                        class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                        Schedule All
                    </button>
                    <button @click="handleAnnounceAll(true)"
                        class="p-2 bg-green-400 rounded-lg text-white font-bold shadow-md hover:bg-green-200">
                        Announce All
                    </button>
                </div>

                <div class="flex gap-2">
                    <button @click="toggleFilterPopup"
                        class="flex gap-2 py-2 px-6 bg-slate-900 rounded-lg text-white font-bold shadow-md hover:bg-slate-500">
                        <span>Filter</span>
                        <span class="material-icons">
                            filter_list
                        </span>
                    </button>
                </div>

            </div>
            <table id="staff-table" class="mt-2 w-full" v-if="classes.length > 0">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Level</th>
                        <th>Period</th>
                        <th>Lessons</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="class_ in this.classes" :key="class_.id">
                        <td>{{ class_.id }}</td>
                        <td>
                            <button class="text-blue-400 underline font-bold" @click="this.setSelectedClassId(class_.id)">
                                {{ class_.name }}
                            </button>
                        </td>
                        <td>{{ class_.level }}</td>
                        <td>{{ class_.startDate.substring(0,4) }}</td>
                        <td :class='class_.totalLessons == 0 ? "text-red-500 italic" : ""'>
                            {{ class_.totalLessons == 0 ? "(Not Schedule Yet)" : class_.totalLessons }}
                        </td>
                        <td>
                            <div class="flex gap-2 justify-center">
                                <button @click="handleAnnounce(true,class_.id)" v-if="!class_.isAnnouced" class="material-icons text-3xl ">
                                    notifications
                                </button>
                                <span v-else class="material-icons text-3xl ">
                                    notifications_none
                                </span>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="flex gap-4 justify-center mt-4" v-if="classes.length > 0">
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
            <div v-else class="italic text-center">
                There is no data
            </div>
            <div v-if="isOpenFilterPopup" class="popup-overlay">
                <div class="overflow-y-auto flex justify-center items-center">
                    <div class="relative">
                        <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                            @click="toggleFilterPopup">X</button>
                        <ScheduleClassFilterForm :id="filterDto.id" :name="filterDto.name" :level="filterDto.level"
                            :period="filterDto.period" :isScheduled="filterDto.isScheduled"
                            :isAnnounced="filterDto.isAnnounced" />
                    </div>
                </div>
            </div>
        </div>
        <div v-else >
            <ScheduleClassDetail :classId="selectedClassId" :user="user"/>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import ScheduleClassFilterForm from '../../components/Staff/ScheduleClassFilterForm.vue'
import ScheduleClassDetail from '../../components/Staff/ScheduleClassDetail.vue'

export default {
    name: "ScheduleClassPage",
    components: { ScheduleClassFilterForm, ScheduleClassDetail },
    inject: ["eventBus"],
    data() {
        return {
            classes: [
                
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            isOpenFilterPopup: false,
            filterDto: {
                id: null,
                name: null,
                period: new Date().getFullYear(),
                level: 0,
                isScheduled: "all",
                isAnnounced: "all"
            },
            selectedClassId : 0,
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
            await this.fetchClasses(this.pageSize, this.currentPage,this.filterDto)  
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
        toggleFilterPopup() {
            this.isOpenFilterPopup = !this.isOpenFilterPopup
        },

        async handleFilter(filterDto) {
            //filter here
            this.filterDto.id = filterDto.id
            this.filterDto.name = filterDto.name
            this.filterDto.level = filterDto.level
            this.filterDto.period = filterDto.period
            this.filterDto.isScheduled = filterDto.isScheduled
            this.filterDto.isAnnounced = filterDto.isAnnounced
            await this.fetchClasses(this.pageSize, this.currentPage,this.filterDto)  
            this.toggleFilterPopup()
        },
        setSelectedClassId(id){
            this.selectedClassId = id
        },
        async refresh(){
            const userPromise = new Promise((resolve) => {
                this.eventBus.emit("get-staff-user", resolve);
            });
            const user = await userPromise;
            this.user = user

            await this.fetchClasses(this.pageSize, this.currentPage,this.filterDto)  
        },
        async fetchClasses(pageSize,pageNumber,filterDto){
            let query = ""
            if (filterDto?.id){
                query += "&id=" + filterDto.id
            }
            if (filterDto?.name){
                query += "&name=" + filterDto.name
            }
            if (filterDto?.period){
                query += "&period=" + filterDto.period
            }
            if (filterDto?.level && filterDto?.level != 0){
                query += "&level=" + filterDto.level
            }
            if (filterDto?.isScheduled && filterDto?.isScheduled != "all"){
                query += "&isSchedule=" + filterDto.isScheduled
            }
            if (filterDto?.isAnnounced && filterDto?.isAnnounced != "all"){
                query += "&IsAnnouced=" + filterDto.isAnnounced
            }
            const response = await axios.get(import.meta.env.VITE_API_URL + `/api/Classes?pageSize=${pageSize}&pageNumber=${pageNumber}${query}`)

            if (response.data) {
                this.classes = response.data.results
                this.classes.sort((a,b) => a.startDate - b.startDate)
                this.totalPage = response.data.totalPages
                this.totalRegistrationsLeft = response.data.totalRecords
            }
        },
        async handleAnnounce(confirmation,id){
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "announce-class-schedule-classes-page",
                    params : id
                })
            } else {
                try {
                    await axios.patch(import.meta.env.VITE_API_URL + `/api/Classes/${id}/announce`)

                    this.eventBus.emit("open-result-dialog", {
                        message: "Announced Successfully",
                        type: "Success"
                    })
                    await this.fetchClasses(this.pageSize, this.currentPage,this.filterDto) 
                } catch (e) {
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response.data.ErrorMessage,
                        type: "Error"
                    })
                }
            }
        },
        async handleAnnounceAll(confirmation){
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "announce-class-all-schedule-classes-page",
                })
            } else {
                try {
                    await axios.patch(import.meta.env.VITE_API_URL + `/api/Classes/announce-all`)

                    this.eventBus.emit("open-result-dialog", {
                        message: "Announced Successfully",
                        type: "Success"
                    })
                    await this.fetchClasses(this.pageSize, this.currentPage,this.filterDto) 
                } catch (e) {
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response.data.ErrorMessage,
                        type: "Error"
                    })
                }
            }
        },
    },
    mounted() {
        this.eventBus.on("handle-filter-schedule-classes-page", async (filterDto) => {
            await this.handleFilter(filterDto)
        })
        this.eventBus.on("set-selected-class-id-schedule-classes-page", (id) => {
            this.setSelectedClassId(id)
        })
        this.eventBus.on("toggle-filter-schedule-class-popup-schedule-classes-page", () => {
            this.toggleFilterPopup()
        })
        this.eventBus.on("announce-class-schedule-classes-page", (id) => {
            this.handleAnnounce(false,id)
        })
        this.eventBus.on("announce-class-all-schedule-classes-page", () => {
            this.handleAnnounceAll(false)
        })
        this.refresh()
    }
}

</script>

<style></style>