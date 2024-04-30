<template>
    <div class="max-w-md w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl font-bold mb-8">{{ this.title }}</div>
        <div class="mb-2 text-xl font-bold">{{ this.shift + " " + this.date}}</div>
        <div class="mb-2 flex gap-2">
            <div class="w-32">Location</div>
            <select class="p-2 rounded-lg border w-full" v-model="selectedLocation">
                <option value="0">Select a location</option>
                <option v-for="location in locations" :key="location.id" :value="location.id">{{location.name}}</option>
            </select>
        </div>
        <div class="mb-2 flex gap-2">
            <div class="w-32">Exam type</div>
            <input class="p-2 rounded-lg border w-full" type="text" v-model="examType" placeholder="Small test 1" maxlength="20">
        </div>
        <div class="flex gap-4 justify-center mt-4">
            <button v-if="!lessonId" class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="handleAdd">Add</button>
            <button v-else class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold"
                @click="handleEdit">Update</button>
            <button v-if="lessonId" class="bg-red-400 hover:bg-red-200 p-2 rounded-lg text-white font-bold"
                @click="handleDelete(true)">Delete</button>
            <button class="p-2 text-red-400 underline font-bold"
                @click="close">Cancel</button>
        </div>
    </div>
</template>

<script>
import axios from 'axios'

export default {
    name: "LessonForm",
    inject: ['eventBus'],
    props: ['close', 'lessonId','title','shift','date','classId'],
    data() {
        return {
            locations : [],
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
            selectedLocation : 0,
            examType : ""
        }
    },
    methods : {
        async fetchLocation(){
            const response = await axios.get(import.meta.env.VITE_API_URL + "/api/Location")
            if (response.data) {
                this.locations = response.data
            }
        },
        async handleAdd(){
            const request = {
                locationId: this.selectedLocation,
                shift : this.shifts.indexOf(this.shift) + 1,
                classId : this.classId,
                date : this.slashDateFormatToSqlDateString(this.date),
                examType : this.examType ?? null
            }
            try {
                await axios.post(import.meta.env.VITE_API_URL + `/api/Lesson`, request)

                this.eventBus.emit("open-result-dialog", {
                    message: "Created Successfully",
                    type: "Success"
                })
                this.eventBus.emit("refresh-lesson-schedule-class-detail")
                this.close()
            } catch (e) {
                console.log(e)
                this.eventBus.emit("open-result-dialog", {
                    message: e.response?.data?.ErrorMessage ?? "Somemthing went wrong",
                    type: "Error"
                })
            }
        },
        async handleEdit(){
            const request = {
                id : this.lessonId,
                locationId: this.selectedLocation,
                examType : this.examType ?? null
            }
            try {
                await axios.patch(import.meta.env.VITE_API_URL + `/api/Lesson`, request)

                this.eventBus.emit("open-result-dialog", {
                    message: "Updated Successfully",
                    type: "Success"
                })
                this.eventBus.emit("refresh-lesson-schedule-class-detail")
                this.close()
            } catch (e) {
                console.log(e)
                this.eventBus.emit("open-result-dialog", {
                    message: e.response?.data?.ErrorMessage ?? "Somemthing went wrong",
                    type: "Error"
                })
            }
        },
        async handleDelete(confirmation) {
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    method : this.handleDelete,
                    params : false
                })
            } else {
                try {
                    await axios.delete(import.meta.env.VITE_API_URL + `/api/Lesson/${this.lessonId}`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Deleted Successfully",
                        type: "Success"
                    })
                    this.eventBus.emit("refresh-lesson-schedule-class-detail")
                    this.close()
                } catch (e) {
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response.data.ErrorMessage,
                        type: "Error"
                    })
                }
            }
        },
        async refresh(){
            await this.fetchLocation()
            if (this.lessonId){
                await this.getFromProps()
            }
        },
        async getFromProps(){
            const response = await axios.get(import.meta.env.VITE_API_URL + "/api/Lesson?Id=" + this.lessonId)
            if (response.data) {
                this.selectedLocation = response.data[0].locationId
                this.examType = response.data[0].examType
            }
        }
    },
    mounted(){
        this.refresh()

    }
}
</script>

<style></style>