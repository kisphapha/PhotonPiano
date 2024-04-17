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
                    <button @click=""
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
            <table id="staff-table" class="mt-2 w-full">
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
                        <td>{{ class_.period }}</td>
                        <td :class='class_.lessons == 0 ? "text-red-500 italic" : ""'>
                            {{ class_.lessons == 0 ? "(Not Schedule Yet)" : class_.lessons }}
                        </td>
                        <td>
                            <div class="flex gap-2 justify-center">
                                <button v-if="class_.isNotified" class="material-icons text-3xl ">
                                    notifications
                                </button>
                                <button v-else class="material-icons text-3xl ">
                                    notifications_none
                                </button>
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
            <ScheduleClassDetail :classId="selectedClassId" />
        </div>
    </div>
</template>

<script>
import ScheduleClassFilterForm from '../../components/Staff/ScheduleClassFilterForm.vue'
import ScheduleClassDetail from '../../components/Staff/ScheduleClassDetail.vue'

export default {
    name: "ScheduleClassPage",
    components: { ScheduleClassFilterForm, ScheduleClassDetail },
    inject: ["eventBus"],
    data() {
        return {
            classes: [
                {
                    id: 1,
                    name: "BLUE_1_2024",
                    level: 1,
                    period: 2024,
                    lessons: 0,
                    isNotified: false,
                }, {
                    id: 2,
                    name: "BLUE_2_2024",
                    level: 1,
                    period: 2024,
                    lessons: 30,
                    isNotified: true,
                },
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
        toggleFilterPopup() {
            this.isOpenFilterPopup = !this.isOpenFilterPopup
        },

        handleFilter(filterDto) {
            //filter here
            this.filterDto.id = filterDto.id
            this.filterDto.name = filterDto.name
            this.filterDto.level = filterDto.level
            this.filterDto.period = filterDto.period
            this.filterDto.isScheduled = filterDto.isScheduled
            this.filterDto.isAnnounced = filterDto.isAnnounced
            this.toggleFilterPopup()
        },
        setSelectedClassId(id){
            this.selectedClassId = id
        },
        
    },
    mounted() {
        this.eventBus.on("handle-filter-schedule-classes-page", (filterDto) => {
            this.handleFilter(filterDto)
        })
        this.eventBus.on("set-selected-class-id-schedule-classes-page", (id) => {
            this.setSelectedClassId(id)
        })
        this.eventBus.on("toggle-filter-schedule-class-popup-schedule-classes-page", () => {
            this.toggleFilterPopup()
        })
    }
}

</script>

<style></style>