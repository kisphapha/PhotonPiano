<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">Lesson : {{ this.date }}, {{ this.shifts[this.shift - 1] }}</div>
        <div class="mt-4 overflow-y-auto max-h-96">
            <table id="staff-table" class="sticky top-0 z-10">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Class</th>
                        <th>Location</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="lesson in this.lessons" :key="lesson.id">
                        <td>{{ lesson.id }}</td>
                        <td>{{ lesson.class.name }}</td>
                        <td>{{ lesson.location.name }}</td>
                        <td>{{ lesson.isLocked ? "Finished" : "Not Yet" }}</td>
                        <td v-if="!lesson.isLocked">
                            <button @click="handleDelete({ confirmation: true, id: lesson.id })">
                                <span class="material-icons">
                                    delete
                                </span>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="mt-4 flex justify-center">
            <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="close">Ok</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    name: "LessonsDetailForm",
    inject : ['eventBus'],
    props: ['shift', 'date', 'close'],
    data() {
        return {
            lessons: [

            ],
            shifts: [
                "7:00 - 8:30",
                "8:45 - 10:15",
                "10:30 - 12:00",
                "12:30 - 14:00",
                "14:15 - 15:45",
                "16:00 - 17:30",
                "18:00 - 19:30",
                "19:45 - 21:15",
            ],
        }
    },
    methods: {
        async fetchLessons() {
            const response = await axios.get(import.meta.env.VITE_API_URL + `/api/Lesson?Shift=${this.shift}&StartDate=${this.date}&EndDate=${this.date}`)
            if (response.data) {
                this.lessons = response.data
            }
            if (this.lessons.length == 0){
                this.close()
            }
        },
        async handleDelete(request) {
            if (request.confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    method: this.handleDelete,
                    params: {
                        confirmation: false,
                        id: request.id
                    }
                })
            } else {
                try {
                    await axios.delete(import.meta.env.VITE_API_URL + `/api/Lesson/${request.id}`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Deleted Successfully",
                        type: "Success"
                    })
                    await this.fetchLessons()
                    this.eventBus.emit("refresh-lessons-schedule-page")
                    
                } catch (e) {
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response.data.ErrorMessage,
                        type: "Error"
                    })
                }
            }
        },
    },
    mounted(){
        this.fetchLessons()
    }
}
</script>

<style></style>