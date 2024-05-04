<template>
    <div class="p-8">
        <div class="mt-4 " v-if="selectedClassId == 0">
            <div class="text-4xl font-bold">Class Management</div>
            <div class="flex place-content-between mt-4">
                <div class="flex gap-4">
                    <button @click="toggleAddNewClassPopup"
                        class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                        Add new class
                    </button>
                    <button @click="toggleAutoPopup"
                        class="p-2 bg-green-400 rounded-lg text-white font-bold shadow-md hover:bg-green-200">
                        Auto Placement
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
            <div class="mt-2">
                <table id="staff-table" class="w-full">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Level</th>
                            <th>Instructor</th>
                            <th>Students</th>
                            <th>Duration</th>
                            <th>Status</th>
                        </tr>

                    </thead>
                    <tbody>
                        <tr v-for="class_ in classes" :key="class_.id">
                            <td>{{ class_.id }}</td>
                            <td><button @click="setSelectedClassId(class_.id)" class="text-blue-400 font-bold underline">{{ class_.name }}</button></td>
                            <td>{{ class_.level }}</td>
                            <td>{{ class_.instructor?.user.name ?? "" }}</td>
                            <td>{{ class_.totalStudents }}</td>
                            <td>{{ class_.startDate }}<br>{{ class_.endDate }}</td>
                            <td>
                                <div
                                    :class='"rounded-xl px-2 py-1 font-bold text-white" + (class_.status == "Active" ? " bg-green-400" : " bg-red-400")'>
                                    {{ class_.status }}
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="flex gap-4 justify-center mt-4" v-if="this.classes.length > 0">
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
                <div class="italic p-2 text-center" v-if="this.classes.length == 0">
                    There is no data
                </div>
            </div>
        </div>
        <div v-else>
            <AdminClassDetail :classId="selectedClassId" :back="setSelectedClassId"/>
        </div>
        <div v-if="isOpenAddPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAddNewClassPopup">X</button>
                    <AddNewClassForm :close="toggleAddNewClassPopup" />
                </div>
            </div>
        </div>
        <div v-if="isOpenAutoPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAutoPopup">X</button>
                    <AutoPlacementClassForm :close="toggleAutoPopup" />
                </div>
            </div>
        </div>
        <div v-if="isOpenFilterPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleFilterPopup">X</button>
                    <ClassFilterForm :close="toggleFilterPopup" :id="filterDto.id" :name="filterDto.name"
                        :level="filterDto.level" :startDate="filterDto.startDate" :endDate="filterDto.endDate"
                        :studentsFrom="filterDto.studentsFrom" :studentsTo="filterDto.studentsTo"
                        :isAnnounced="filterDto.isAnnounced" :action="handleFilter" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import AddNewClassForm from '../../components/Staff/Classes/AddNewClassForm.vue'
import AutoPlacementClassForm from '../../components/Staff/Classes/AutoPlacementClassForm.vue'
import ClassFilterForm from '../../components/Staff/Classes/ClassFilterForm.vue'
import AdminClassDetail from '../../components/Staff/Classes/AdminClassDetail.vue'

export default {
    name: "AdminClassPage",
    components: { AddNewClassForm, AutoPlacementClassForm, ClassFilterForm, AdminClassDetail },
    inject: ["eventBus"],
    data() {
        return {
            classes: [
                {
                    id: 1,
                    name: "BLUE_69_2024",
                    level: 5,
                    instructor: {
                        user: {
                            name: "Diamond"
                        }
                    },
                    totalStudents: 3,
                    startDate: "2024-01-01",
                    endDate: "2024-12-31",
                    status: "Active"
                }
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            isOpenAddPopup: false,
            isOpenAutoPopup: false,
            isOpenFilterPopup: false,
            filterDto: {
                id: null,
                name: null,
                level: 0,
                startDate: null,
                endDate: null,
                studentsFrom: null,
                studentsTo: null,
                isAnnounced: "all"
            },
            selectedClassId : 0,
        }
    },
    methods: {
        async movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                await this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                await this.handlePageChange()
            }
        },
        async handlePageChange() {
            if (this.currentPage > this.totalPage) {
                this.currentPage = this.totalPage
            }
            if (this.currentPage < 1) {
                this.currentPage = 1
            }
            //await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        toggleAddNewClassPopup() {
            this.isOpenAddPopup = !this.isOpenAddPopup
        },
        toggleAutoPopup() {
            this.isOpenAutoPopup = !this.isOpenAutoPopup
        },
        toggleFilterPopup() {
            this.isOpenFilterPopup = !this.isOpenFilterPopup
        },
        async handleFilter(filterDto) {
            //filter here
            this.filterDto.id = filterDto.id
            this.filterDto.name = filterDto.name
            this.filterDto.level = filterDto.level
            this.filterDto.startDate = filterDto.startDate
            this.filterDto.endDate = filterDto.endDate
            this.filterDto.studentsFrom = filterDto.studentsFrom
            this.filterDto.studentsTo = filterDto.studentsTo
            this.filterDto.isAnnounced = filterDto.isAnnounced
            //await this.fetchClasses(this.pageSize, this.currentPage,this.filterDto)  
            this.toggleFilterPopup()
        },
        setSelectedClassId(id){
            this.selectedClassId = id
        }
    }
}

</script>

<style></style>