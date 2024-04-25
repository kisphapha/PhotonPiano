<template>
    <div class="overflow-y-auto max-h-screen px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold">Auto-Arrange Entrance Test Slots</div>
        <div class="text-xl font-bold">Year {{ new Date().getFullYear() }}</div>
        <div class="flex gap-2 mt-4">
            <div class="p-2 w-48">Number of Students :</div>
            <input class="border p-1 rounded-md w-64" type="number" v-model="numberOfStudents"
                min="0" :max="registrationLeft">
        </div>
        <div class="flex place-content-between mt-2">
            <div class="flex">
                <div class="p-2">From :</div>
                <input class="border p-1 rounded-md" type="date" v-model="startDate" @change="calculate">
            </div>
            <div class="flex">
                <div class="p-2">To :</div>
                <input class="border p-1 rounded-md" type="date" v-model="endDate" @change="calculate">
            </div>
        </div>
        <div class="mt-4 flex place-content-between gap-4">
            <div>
                <div class="text-xl font-bold">Locations</div>
                <div class="overflow-y-auto h-48 overflow-x-hidden">
                    <div v-for="location in locations" :key="location.id" class="flex gap-4 ">
                        <div class="w-24">{{ location.name }} ({{ location.capacity }})</div>
                        <input type="checkbox" :checked="isLocationSelected(location.id)"
                            @change="toggleLocationSelection(location.id)" />
                    </div>
                </div>

            </div>
            <div>
                <div class="text-xl font-bold">Shifts</div>
                <div class="overflow-y-auto h-48">
                    <div v-for="shift in shifts" :key="shift.id" class="flex gap-4">
                        <div class="w-24">{{ shift.detail }}</div>
                        <input type="checkbox" :checked="isShiftSelected(shift.id)"
                            @change="toggleShiftSelection(shift.id)" />
                    </div>
                </div>
            </div>
            <div>
                <div class="text-xl font-bold">Instructors</div>
                <div class="overflow-y-auto h-48">
                    <div v-for="instructor in instructors" :key="instructor.id" class="flex gap-4">
                        <div class="w-24">{{ instructor.user.name }}</div>
                        <input type="checkbox" :checked="isInstructorSelected(instructor.id)"
                            @change="toggleInstructorSelection(instructor.id)" />
                    </div>
                </div>
            </div>
        </div>
        <div class="mt-4 italic flex gap-2">
            <div class="p-1">Student registrations left : </div>
            <div class="text-2xl font-bold">{{ registrationLeft }}</div>
        </div>
        <div :class='"italic flex gap-2 " + (numberOfStudents > maxStudents ? "text-red-500" : "")'>
            <div class="p-1">Maximum of students supported : </div>
            <div class="text-2xl font-bold">{{ maxStudents }}</div>
        </div>
        <div class="flex gap-4 justify-center">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold" @click="handleAutoArrange(true)">Apply</button>
            <button class="p-2 text-red-400 underline font-bold" @click="toggleAutoArrangeSlotForm">Cancel</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
//import { RouterLink } from 'vue-router';

export default {
    name: "EntranceTestSlotForm",
    inject: ['eventBus'],
    data() {
        return {
            locations: [
            ],
            instructors: [
            ],
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
            shiftsSelected: [],
            locationsSelected: [],
            instructorsSelected: [],
            startDate: this.toSqlDateString(new Date()),
            endDate: this.toSqlDateString(new Date()),
            registrationLeft: 500,
            numberOfStudents: 1000,
            maxStudents: 0
        }
    },
    methods: {
        isShiftSelected(shiftId) {
            return this.shiftsSelected.includes(shiftId);
        },
        toggleShiftSelection(shiftId) {
            if (this.isShiftSelected(shiftId)) {
                const index = this.shiftsSelected.indexOf(shiftId);
                this.shiftsSelected.splice(index, 1);
            } else {
                this.shiftsSelected.push(shiftId);
            }
            this.calculate()
        },
        isInstructorSelected(instructorId) {
            return this.instructorsSelected.includes(instructorId);
        },
        toggleInstructorSelection(instructorId) {
            if (this.isInstructorSelected(instructorId)) {
                const index = this.instructorsSelected.indexOf(instructorId);
                this.instructorsSelected.splice(index, 1);
            } else {
                this.instructorsSelected.push(instructorId);
            }
            this.calculate()
        },
        isLocationSelected(locationId) {
            return this.locationsSelected.includes(locationId);
        },
        toggleLocationSelection(locationId) {
            if (this.isLocationSelected(locationId)) {
                const index = this.locationsSelected.indexOf(locationId);
                this.locationsSelected.splice(index, 1);
            } else {
                this.locationsSelected.push(locationId);
            }
            this.calculate()
        },
        toggleAutoArrangeSlotForm() {
            this.eventBus.emit("toggle-auto-arrange-entrance-test-slot-popup")
        },
        calculate() {
            const from = new Date(this.startDate)
            const to = new Date(this.endDate)
            if (from > to) {
                this.maxStudents = 0
            } else {
                const differenceInMs = Math.abs(from - to);
                const differenceInDays = Math.ceil(differenceInMs / (1000 * 60 * 60 * 24)) + 1;
                let totalStudentADay = 0
                for (var shift in this.shiftsSelected) {
                    let locationCount = 0;
                    for (var location of this.locationsSelected) {
                        if (locationCount < this.instructorsSelected.length) {
                            const detail = this.locations.find(l => l.id == location)
                            if (detail) {
                                totalStudentADay += detail.capacity
                                locationCount++
                            }
                        }

                    }
                }
                this.maxStudents = totalStudentADay * differenceInDays
            }

        },
        async fetchStudents() {
            const students = await axios.get(import.meta.env.VITE_API_URL + `/api/Student?Status=Accepted&pageSize=1&pageNumber=1`)

            if (students.data) {
                this.registrationLeft = students.data.totalRecords
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
            await this.fetchStudents()
            await this.fetchLocations()
            await this.fetchInstructor()

            this.numberOfStudents = this.registrationLeft
        },
        async handleAutoArrange(confirmation) {
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "This would make a huge impact. Are you sure about this?",
                    callback: "auto-arrange-slot-entrance-test-arrange-page"
                })
            } else {
                if (this.numberOfStudents > this.maxStudents) {
                    this.eventBus.emit("open-result-dialog", {
                        message: "Nope",
                        type: "Error"
                    })
                } else {
                    this.eventBus.emit("open-loading-popup",{
                        message : "Please wait..."
                    })
                    try {
                        await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/auto-arranging`,{
                            allowedShifts : this.shiftsSelected,
                            allowedLocationIds : this.locationsSelected,
                            allowedInstructorIds : this.instructorsSelected,
                            from : this.startDate,
                            to : this.endDate,
                            numberOfStudents : this.numberOfStudents
                        })
                        this.eventBus.emit("open-result-dialog", {
                            message: "Arranged Successfully",
                            type: "Success"
                        })
                        this.eventBus.emit("refresh-entrance-test-arrange-page")
                        this.toggleAutoArrangeSlotForm()
                    } catch (e) {
                        console.log(e)
                        this.eventBus.emit("open-result-dialog", {
                            message: e.response?.data?.ErrorMessage ?? "Something went wrong",
                            type: "Error"
                        })
                    }
                    this.eventBus.emit("close-loading-popup")
                }

            }
        },

    },
    mounted() {
        this.refresh()

        this.eventBus.on("auto-arrange-slot-entrance-test-arrange-page",() => {
            this.handleAutoArrange(false)
        })
    }
}
</script>