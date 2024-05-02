<template>
    <div class="overflow-y-auto max-h-screen px-6 py-8 bg-white rounded-lg shadow-md overflow-x-auto">
        <div class="text-2xl font-bold">{{ this.title }}</div>
        <div class="mt-4">
            <div class="flex place-content-between">
                <div class="w-full">
                    <div class="flex gap-2 ">
                        <div class="p-2 w-32">Location</div>
                        <select class="p-2 rounded-lg border w-full" v-model="selectedLocationId"
                            @change="this.handleLocationChange">
                            <option value="0">Select a location</option>
                            <option v-for="location in locations" :key="location.id" :value="location.id">{{
            location.name }}</option>
                        </select>
                    </div>
                    <div class="mt-2 flex gap-2">
                        <div class="p-2 w-32">Shift</div>
                        <select class="p-2 rounded-lg border w-full" v-model="selectedShiftId">
                            <option value="0">Select a shift</option>
                            <option v-for="shift in shifts" :key="shift.id" :value="shift.id">{{ shift.detail }}
                            </option>
                        </select>
                    </div>
                    <div class="mt-2 flex gap-2">
                        <div class="p-2 w-32">Date</div>
                        <input type="date" class="p-2 rounded-lg border w-full" v-model="this.selectedDate" />
                    </div>

                </div>
                <div class="w-full">
                    <div class="flex">
                        <div class="p-2 ">Capacity </div>
                        <div class="font-bold text-red-400 text-xl p-2 ">
                            {{ this.selectedLocation ? this.selectedLocation.capacity : "N/A" }}
                        </div>
                    </div>
                    <div class="mt-2 flex gap-2">
                        <div class="p-2">Instructor</div>
                        <select class="p-2 rounded-lg border w-full" v-model="selectedInstructorId">
                            <option value="0">Select an instructor</option>
                            <option v-for="instructor in instructors" :key="instructor.id" :value="instructor.id">
                                {{ instructor.user.name }}
                            </option>
                        </select>
                    </div>

                </div>

            </div>
            <div class="flex place-content-between mt-2 ">
                <div class="flex">
                    <div class="p-2 w-48">Students to be added :</div>
                    <div class="flex gap-2">
                        <input class="border p-1 rounded-md max-w-64" type="text" v-model="keyword_name"
                            placeholder="Search by name">
                        <button class="p-1 bg-slate-100 rounded-lg" @click="handleSearch">
                            <span class="material-icons p-1">
                                search
                            </span>
                        </button>
                    </div>

                </div>
                <div class="flex justify-end">
                    <div class="flex gap-4">
                        <button @click="toggleViewMode" v-if="this.slotId"
                            class="p-2 bg-green-700 font-bold text-white rounded-lg hover:bg-green-500">
                            {{ viewMode == 0 ? "View selected" : "View unselected" }}
                        </button>
                        <button @click="handleClear"
                            class="p-2 bg-red-400 font-bold text-white rounded-lg hover:bg-red-200">
                            Clear Selection
                        </button>
                        <button @click="handleAutoFill" v-if="viewMode == 0"
                            class="p-2 bg-blue-400 font-bold text-white rounded-lg hover:bg-blue-200">
                            Auto Fill
                        </button>
                    </div>

                </div>
            </div>
        </div>
        <div class="overflow-y-auto h-64 mt-2">
            <table id="staff-table" v-if="this.students.length > 0">
                <thead class="sticky top-0 z-10">
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Phone</th>
                        <th>
                            <div class="w-80">Short Desc.</div>
                        </th>
                        <th>Select</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="student in students" :key="student.id">
                        <td>{{ student.id }}</td>
                        <td>{{ student.user.name }}</td>
                        <td>{{ student.user.email }}</td>
                        <td>{{ student.user.phone }}</td>
                        <td class="w-80 break-words">{{ student.shortDesc }}</td>
                        <td>
                            <input type="checkbox" :checked="isSelected(student.id)"
                                @change="toggleSelection(student.id)" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="italic text-center" v-else>
                There is no data
            </div>
        </div>
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
        <div class="flex place-content-between" v-if="selectedLocation && selectedShiftId != 0 && selectedDate">
            <div
                :class='this.studentSelected.length > this.selectedLocation.capacity ? "font-bold italic text-red-400" : "font-bold italic "'>
                <span>Selected</span>
                <span class="ml-4 text-2xl">{{ this.studentSelected.length }}</span>
                <span>/ {{ this.selectedLocation.capacity }} students</span>
            </div>
            <div class="flex gap-4 mt-4" v-if='!checkEditTitle()'>
                <button @click="handleAdd"
                    class="bg-blue-400 hover:bg-blue-200 px-6 py-2 rounded-lg text-white font-bold">OK</button>
                <button class="p-2 text-red-400 underline font-bold"
                    @click="toggleAddSlotFormEntranceTestArrangePage">Cancel</button>
            </div>
            <div class="flex gap-4 mt-4" v-else>
                <button @click="handleUpdate" class="bg-blue-400 hover:bg-blue-200 px-6 py-2 rounded-lg text-white font-bold">OK</button>
                <button class="p-2 text-red-400 underline font-bold"
                    @click="close">Cancel</button>
            </div>
        </div>

    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';
import axios from 'axios'

export default {
    name: "EntranceTestSlotForm",
    inject: ['eventBus'],
    props: ['title', 'slotId','close'],
    data() {
        return {
            locations: [],
            instructors: [],
            students: [],
            studentsAlreadySelected: [],
            shifts: [
                { id: 1, detail: "7:00 - 8:30" },
                { id: 2, detail: "8:45 - 10:15" },
                { id: 3, detail: "10:30 - 12:00" },
                { id: 4, detail: "12:30 - 14:00" },
                { id: 5, detail: "14:15 - 15:45" },
                { id: 6, detail: "16:00 - 17:30" },
                { id: 7, detail: "18:00 - 19:30" },
                { id: 8, detail: "19:45 - 21:15" },
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            keyword_name: "",
            selectedLocationId: 0,
            selectedInstructorId: 0,
            selectedShiftId: 0,
            selectedLocation: null,
            selectedDate: null,
            studentSelected: [],
            totalStudents: 0,
            viewMode: 0
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
            await this.fetchStudents()
        },
        async movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
            }
            await this.handlePageChange()
        },
        async handleSearch() {
            await this.fetchStudents()
        },
        handleLocationChange() {
            this.selectedLocation = this.locations.find(l => l.id == this.selectedLocationId)
        },
        isSelected(studentId) {
            return this.studentSelected.includes(studentId);
        },
        toggleSelection(studentId) {
            if (this.isSelected(studentId)) {
                const index = this.studentSelected.indexOf(studentId);
                this.studentSelected.splice(index, 1);
            } else {
                this.studentSelected.push(studentId);
            }
        },
        toggleAddSlotFormEntranceTestArrangePage() {
            this.eventBus.emit("toggle-add-entrance-test-slot-popup")
        },
        closeEditSlotFormEntranceTestArrangePage() {
            this.eventBus.emit("close-edit-entrance-test-slot-popup")
        },
        async getFromProps() {
            if (this.slotId) {
                const response = await axios.get(import.meta.env.VITE_API_URL + `/api/EntranceTest/${this.slotId}/slot-detail`)
                if (response.data) {
                    this.selectedLocationId = response.data.location.id
                    this.selectedLocation = response.data.location
                    this.selectedInstructorId = response.data.instructor?.id ?? 0
                    this.selectedShiftId = response.data.shift
                    this.selectedDate = response.data.date
                    this.studentsAlreadySelected = []
                    for (var entranceTest of response.data.entranceTests) {
                        this.studentSelected.push(entranceTest.studentId)
                        this.studentsAlreadySelected.push(entranceTest.student)
                    }
                }
            }
        },
        checkEditTitle() {
            return (this.title == "Edit entrance test slot")
        },
        async fetchStudents() {
            if (this.viewMode == 0) {
                const studentsResponse = await axios.get(import.meta.env.VITE_API_URL + `/api/Student?Status=Accepted&pageNumber=${this.currentPage}&pageSize=${this.pageSize}&name=${this.keyword_name}`);
                if (studentsResponse.data) {
                    this.students = studentsResponse.data.results;
                    this.totalPage = studentsResponse.data.totalPages;
                    this.totalStudents = studentsResponse.data.totalRecords;
                }
            } else {
                this.totalPage = Math.ceil(this.studentsAlreadySelected.length/ this.pageSize)
                const start = (this.currentPage - 1) * this.pageSize
                const end = (this.currentPage - 1) * this.pageSize + this.pageSize
                this.students = this.studentsAlreadySelected.slice(start,end).filter(s => s.user.name.includes(this.keyword_name))
            }

        },
        async fetchLocations() {
            const locations = await axios.get(import.meta.env.VITE_API_URL + `/api/Location`)

            if (locations.data) {
                this.locations = locations.data
            }
        },
        async fetchInstructor() {
            const bait = await axios.get(import.meta.env.VITE_API_URL + `/api/Instructor?pageNumber=1&pageSize=1`)
            if (bait.data) {
                const instructors = await axios.get(import.meta.env.VITE_API_URL + `/api/Instructor?pageNumber=1&pageSize=${bait.data.totalRecords}`)

                if (instructors.data) {
                    this.instructors = instructors.data.results
                }
            }

        },
        async refresh() {
            if (this.checkEditTitle()) {
                await this.getFromProps()
            }
            await this.fetchStudents()
            await this.fetchLocations()
            await this.fetchInstructor()
        },
        handleClear() {
            this.studentSelected = []
        },
        async handleAutoFill() {
            if (this.selectedLocation) {
                let numberToFill = Math.max(Math.min(this.totalStudents, this.selectedLocation.capacity - this.studentSelected.length), 0)
                const response = await axios.get(import.meta.env.VITE_API_URL + `/api/Student?Status=Accepted&pageNumber=1&pageSize=${numberToFill}&name=${this.keyword_name}`)
                if (response.data) {
                    while (numberToFill > 0) {
                        numberToFill -= 1;
                        for (var student of response.data.results) {
                            if (!this.studentSelected.find(sid => sid == student.id)) {
                                this.studentSelected.push(student.id)
                            }
                        }
                    }
                }

            } else {
                this.eventBus.emit("open-result-dialog", {
                    message: "Please select a location first",
                    type: "Error"
                })
            }

        },
        async handleAdd() {
            const request = {
                locationId: this.selectedLocationId,
                shift: this.selectedShiftId,
                date: this.selectedDate,
                instructorId : this.selectedInstructorId != 0 ? this.selectedInstructorId : null
            }
            try {
                const createdSlot = await axios.post(import.meta.env.VITE_API_URL + `/api/EntranceTest/slot`, request)
                await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/upsert-entrance-tests`, {
                    slotId: createdSlot.data.id,
                    studentIds: this.studentSelected
                })

                this.eventBus.emit("open-result-dialog", {
                    message: "Created and Added Students Successfully",
                    type: "Success"
                })
                this.eventBus.emit("refresh-entrance-test-arrange-page")
                this.toggleAddSlotFormEntranceTestArrangePage()
            } catch (e) {
                console.log(e)
                this.eventBus.emit("open-result-dialog", {
                    message: e.response?.data?.ErrorMessage ?? "Somemthing went wrong",
                    type: "Error"
                })
            }
        },
        async handleUpdate() {
            const request = {
                id : this.slotId,
                locationId: this.selectedLocationId,
                shift: this.selectedShiftId,
                date: this.selectedDate,
                instructorId : this.selectedInstructorId != 0 ? this.selectedInstructorId : null
            }
            try {
                await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/slot`, request)
                await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/upsert-entrance-tests`, {
                    slotId: this.slotId,
                    studentIds: this.studentSelected
                })

                this.eventBus.emit("open-result-dialog", {
                    message: "Updated Successfully",
                    type: "Success"
                })
                this.eventBus.emit("refresh-entrance-test-arrange-page")
                this.closeEditSlotFormEntranceTestArrangePage()
            } catch (e) {
                console.log(e)
                this.eventBus.emit("open-result-dialog", {
                    message: e.response?.data?.ErrorMessage ?? "Somemthing went wrong",
                    type: "Error"
                })
            }
        },
        toggleViewMode(){
            this.viewMode = this.viewMode == 0 ? 1 : 0
            this.fetchStudents()
        }
    },
    mounted() {

        this.refresh()
    }
}
</script>