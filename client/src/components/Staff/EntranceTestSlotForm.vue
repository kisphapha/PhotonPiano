<template>
    <div class="overflow-y-auto max-h-screen px-6 py-8 bg-white rounded-lg shadow-md overflow-x-auto w-[75vw]">
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
                            <option v-for="instructor in instructors" :key="instructor.id" :value="instructor.id">{{
            instructor.name }}
                            </option>
                        </select>
                    </div>

                </div>

            </div>
            <div class="flex place-content-between mt-2 ">
                <div class="flex">
                    <div class="p-2 w-48">Students to be added :</div>
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
                <div class="flex justify-end">
                    <div class="flex gap-4">
                        <button @click="handleClear"
                            class="p-2 bg-red-400 font-bold text-white rounded-lg hover:bg-red-200">
                            Clear Selection
                        </button>
                        <button @click="handleAutoFill"
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
                        <td>{{ student.shortDesc }}</td>
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
        <div class="flex place-content-between"
            v-if="selectedLocation && selectedInstructorId != 0 && selectedShiftId != 0 && selectedDate">
            <div
                :class='this.studentSelected.length > this.selectedLocation.capacity ? "font-bold italic text-red-400" : "font-bold italic "'>
                <span>Selected</span>
                <span class="ml-4 text-2xl">{{ this.studentSelected.length }}</span>
                <span>/ {{ this.selectedLocation.capacity }} students</span>
            </div>
            <div class="flex gap-4 mt-4" v-if='!checkEditTitle()'>
                <button class="bg-blue-400 hover:bg-blue-200 px-6 py-2 rounded-lg text-white font-bold">OK</button>
                <button class="p-2 text-red-400 underline font-bold"
                    @click="toggleAddSlotFormEntranceTestArrangePage">Cancel</button>
            </div>
            <div class="flex gap-4 mt-4" v-else>
                <button class="bg-blue-400 hover:bg-blue-200 px-6 py-2 rounded-lg text-white font-bold">OK</button>
                <button class="p-2 text-red-400 underline font-bold"
                    @click="closeEditSlotFormEntranceTestArrangePage">Cancel</button>
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
    props: ['title', 'locationId', 'instructorId', 'shiftId', 'selectedStudentIds', 'examDate'],
    data() {
        return {
            locations: [],
            instructors: [
                {
                    id: 1,
                    name: "Diamond",
                },
                {
                    id: 2,
                    name: "WhiteWine",
                },
            ],
            students: [],
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
            totalStudents: 0
        }
    },
    methods: {
        async handlePageChange() {
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
        getFromProps() {
            this.selectedLocationId = this.locationId
            this.selectedInstructorId = this.instructorId,
                this.selectedShiftId = this.shiftId,
                this.selectedDate = this.examDate
            for (var studentId of this.selectedStudentIds) {
                this.studentSelected.push(studentId)
            }
            console.log(this.selectedStudentIds)
            console.log(this.studentSelected)
            this.handleLocationChange()

        },
        checkEditTitle() {
            return (this.title == "Edit entrance test slot")
        },
        async fetchStudents() {
            const students = await axios.get(import.meta.env.VITE_API_URL + `/api/Student?Status=Accepted&pageNumber=${this.currentPage}&pageSize=${this.pageSize}&name=${this.keyword_name}`)

            if (students.data) {
                this.students = students.data.results
                this.totalPage = students.data.totalPages
                this.totalStudents = students.data.totalRecords
            }
        },
        async fetchLocations() {
            const locations = await axios.get(import.meta.env.VITE_API_URL + `/api/Location`)

            if (locations.data) {
                this.locations = locations.data
            }
        },
        async refresh() {

            await this.fetchStudents()
            await this.fetchLocations()
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

        }
    },
    mounted() {
        if (this.checkEditTitle()) {
            this.getFromProps()
        }
        this.refresh()
    }
}
</script>