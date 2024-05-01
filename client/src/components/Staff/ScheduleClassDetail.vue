<template>
    <div v-if="this.class">
        <div class="p-8">
            <button class="font-bold text-xl text-blue-500 flex" @click="handleGoBack">
                <span class="material-icons">arrow_back_ios</span> Back
            </button>
            <div class="text-4xl flex">
                CLASS NAME :
                <div class="font-bold flex ml-4">{{ this.class.name }}
                    <img :src="this.colors[this.class.level - 1]" class="w-16 ml-4">
                </div>
            </div>
            <div class="mt-4">
                <div class="text-2xl">
                    LEVEL : <span class="font-bold">{{ this.class.level }}</span> {{ this.class_level[this.class.level -
        1] }}
                </div>
                <div class="text-2xl">
                    INSTRUCTOR : <span class="font-bold">{{ this.class.instructor.user.name }}</span>
                </div>
            </div>
            <div class="italic text-lg">
                {{ "(" + this.class.startDate + " - " + this.class.endDate + ")" }}
            </div>
        </div>
        <div class="px-8 text-lg">
            Lessons Finished : 
            <span class="font-bold">{{ this.countFinsihedLessons() }} / {{ this.class.lessons.length }}</span>
        </div>
        <div class="px-8 text-lg text-red-500" v-if=" this.targetLessons - this.class.lessons.length > 0">
            This class is not meeting requirement about lessons number. Please add more
            <span class="font-bold">{{ this.targetLessons - this.class.lessons.length }} </span>
            lessons to meet center requirement.
        </div>
        <div class="px-8 flex gap-4 mt-4">
            <button @click="openAutoSchedulePopup"
                class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                Auto Schedule This Class
            </button>
            <button @click="handleClear(true)" class="p-2 bg-red-400 rounded-lg text-white font-bold shadow-md hover:bg-red-200">
                Clear All Lessons
            </button>
            <button @click="toggleIsMarking"
                class="p-2 bg-orange-400 rounded-lg text-white font-bold shadow-md hover:bg-orange-200">
                {{ isMarking ? "Done Marking" : "Mark Day-offs" }}
            </button>
            <div v-if="markedDayOffs.length > 0" class="p-2 italic text-orange-400">
                (Marked {{ markedDayOffs.length }} day-offs)
                <button @click="clearAllMarking" class="font-bold underline">Clear all</button>
            </div>
        </div>

        <div class="p-8">
            <div class="flex gap-16">
                <div class="text-2xl">Timetable:</div>
                <div class="font-bold flex gap-8">
                    <button @click="moveWeek(false)"
                        class="bg-gray-300 rounded-xl px-2 hover:bg-slate-100  text-5xl ">◄</button>
                    <select v-model="selectedWeek" @change="handleSelectedWeekChange"
                        class="text-xl font-normal  rouded-lg rouded-lg border">
                        <option v-for="week in weeksInYear" :key="week.start" :value="week.start">
                            {{ week.start.toLocaleDateString() + " - " + week.end.toLocaleDateString() }}
                        </option>
                    </select>
                    <button @click="moveWeek(true)"
                        class="bg-gray-300 rounded-xl px-2 hover:bg-slate-100  text-5xl ">►</button>
                </div>
                <select v-model="selectedYear" @change="handleSelectedYearChange"
                    class="text-xl font-normal rouded-lg border px-4">
                    <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
                </select>
            </div>
            <div>
                <table class="w-full border-collapse">
                    <thead>
                        <tr>
                            <th class="py-2"></th>
                            <th v-for="day in daysInWeek" :key="day.dayInWeek" class="py-2">
                                {{ day.dayInWeek }}<br>{{ day.specificDay }}
                                <div v-if="isMarking">
                                    <input type="checkbox" :checked="isDateMarked(day.specificDay)"
                                        @change="toggleDateMarking(day.specificDay)" />
                                </div>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="shift in shifts" :key="shift" class="py-2">
                            <td class="py-2">Shift {{ shifts.indexOf(shift) + 1 }}<br>({{ shift }})</td>
                            <td v-for="day in daysInWeek" :key="day" class="py-2 ">
                                <button v-if='this.getLessonDetail(shift, day, "isOcupied") == "false"'
                                    :class='getLessonDetail(shift, day, "css")'
                                    @click="toggleAddLessonPopup(shift, day.specificDay)" @dragover.prevent
                                    @drop="onDrop(shift, day)">
                                    <span class="material-icons text-3xl">
                                        add_circle
                                    </span>
                                </button>
                                <button v-else :class='getLessonDetail(shift, day, "css")'
                                    @click='toggleEditLessonPopup(shift, day)' @dragstart='dragSetup(shift, day)'
                                    draggable="true" @dragover.prevent @drop="onDrop(shift, day)">
                                    <span>
                                        <div class="font-bold">{{ getLessonDetail(shift, day, "location") }}</div>
                                        <div class="text-md italic">{{ getLessonDetail(shift, day, "examType") }}</div>
                                    </span>
                                </button>
                            </td>
                        </tr>

                    </tbody>
                </table>
            </div>
        </div>
        <div v-if="isOpenAutoSchedulePopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center overflow-x-auto">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="closeAutoSchedulePopup">X</button>
                    <AutoScheduleAClassForm :classId="this.classId" :markedDayOffs="this.markedDayOffs"
                        :close="closeAutoSchedulePopup" />
                </div>
            </div>
        </div>
        <div v-if="isOpenAddLessonPopup" class="popup-overlay">
            <div class="flex justify-center items-center w-2/3">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAddLessonPopup">X</button>
                    <LessonForm title="Add new lesson" :close="this.toggleAddLessonPopup" :classId="this.classId"
                        :shift="this.selectedShift" :date="this.selectedDate" />
                </div>
            </div>
        </div>
        <div v-if="isOpenEditLessonPopup" class="popup-overlay">
            <div class="flex justify-center items-center w-2/3">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleEditLessonPopup">X</button>
                    <LessonForm title="Edit lesson" :lessonId="this.selectedLessonId"
                        :close="this.toggleEditLessonPopup" :classId="this.classId" :shift="this.selectedShift"
                        :date="this.selectedDate" />
                </div>
            </div>
        </div>
    </div>

</template>
<script>

import axios from 'axios';
import AutoScheduleAClassForm from './AutoScheduleAClassForm.vue';
import LessonForm from './LessonForm.vue';

export default {
    name: "ScheduleClassDetail",
    inject: ["eventBus"],
    props: ['classId', 'user'],
    components: { AutoScheduleAClassForm, LessonForm },
    data() {
        return {
            colors: [
                "/src/assets/diamond_blue.png",
                "/src/assets/diamond_pink.png",
                "/src/assets/diamond_red.png",
                "/src/assets/diamond_black.png",
                "/src/assets/diamond_white.png"
            ],
            daysInWeek: [
                { dayInWeek: "Sunday", specificDay: "07/01" },
                { dayInWeek: "Monday", specificDay: "01/01" },
                { dayInWeek: "Tuesday", specificDay: "02/01" },
                { dayInWeek: "Wednesday", specificDay: "03/01" },
                { dayInWeek: "Thusday", specificDay: "04/01" },
                { dayInWeek: "Friday", specificDay: "05/01" },
                { dayInWeek: "Saturday", specificDay: "06/01" },
            ],
            weeksInYear: [
                {
                    start: new Date(),
                    end: new Date()
                }
            ],
            years: [

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
            class_level: [
                "(Beginner)",
                "(Novice)",
                "(Intermediate)",
                "(Advanced)",
                "(Virtuoso)",
            ],
            selectedWeek: null,
            selectedYear: new Date().getFullYear(),
            lessons: [],
            class: {
                name: "BLUE_69_2024",
                level: 5,
                startDate: "2024-01-01",
                endDate: "2024-12-31",
                instructor: {
                    user: {
                        name: "Diamondzz"
                    }
                },
                lessons : []
            },
            isOpenAutoSchedulePopup: false,
            isOpenDayOffPopup: false,
            isOpenAddLessonPopup: false,
            isOpenEditLessonPopup: false,
            isMarking: false,
            markedDayOffs: [],
            selectedLessonId: 0,
            selectedDate: null,
            selectedShift: 1,
            targetLessons : 90
        }
    },
    methods: {
        async handleSelectedWeekChange() {
            this.specDays = [];
            for (let i = 0; i < 7; i++) {
                const nextDay = new Date(this.selectedWeek);
                nextDay.setDate(this.selectedWeek.getDate() + i);
                this.daysInWeek[i].specificDay = nextDay.toLocaleDateString()
            }
            let endDate = new Date(this.selectedWeek)
            endDate.setDate(this.selectedWeek.getDate() + 7)
            await this.fetchLessons(this.selectedWeek, endDate)
        },
        async handleSelectedYearChange() {
            this.weeksInYear = this.getWeeksOfYear(this.selectedYear)
            this.selectedWeek = this.weeksInYear[0].start
            await this.handleSelectedWeekChange()
        },
        moveWeek(forward) {
            const currentIndex = this.weeksInYear.findIndex((week) => {
                return this.compareDate(week.start, this.selectedWeek) === 0;
            });
            if (forward && currentIndex < this.weeksInYear.length - 1) {
                this.selectedWeek = this.weeksInYear[currentIndex + 1].start
                this.handleSelectedWeekChange()
            } else if (!forward && currentIndex > 0) {
                this.selectedWeek = this.weeksInYear[currentIndex - 1].start
                this.handleSelectedWeekChange()
            }
        },
        async refresh() {
            const response = await axios.get(import.meta.env.VITE_API_URL + '/api/Classes/' + this.classId)

            this.class = response.data
            this.weeksInYear = this.getWeeksOfYear(new Date().getFullYear())
            this.years = this.getYears()
            this.selectedWeek = this.getFirstDayOfWeek()
            await this.handleSelectedWeekChange()
        },
        async fetchLessons(startDate, endDate) {
            let query = "";
            if (startDate) {
                query += "&StartDate=" + this.toSqlDateString(startDate)
            }
            if (endDate) {
                query += "&EndDate=" + this.toSqlDateString(endDate)
            }
            const response = await axios.get(import.meta.env.VITE_API_URL + "/api/Lesson?ClassId=" + this.classId + query)
            if (response.data) {
                this.lessons = response.data
            }

        },
        getLessonDetail(shift, date, information) {
            const css = "h-16 flex flex-col items-center justify-center rounded-xl min-w-32 "
            const inputDateParts = date.specificDay.split('/');
            const day = parseInt(inputDateParts[0], 10);
            const month = parseInt(inputDateParts[1], 10) - 1; // Months are zero-based in JavaScript
            const year = parseInt(inputDateParts[2], 10);
            const actualDate = new Date(year, month, day)
            const lesson = this.lessons.find(l => l.shift == this.shifts.indexOf(shift) + 1 && (new Date(l.date).toDateString() == actualDate.toDateString()))
            switch (information) {
                case "id":
                    if (!lesson) {
                        return 0
                    }
                    return lesson.id
                case "location":
                    if (!lesson) {
                        return ""
                    }
                    return lesson.location.name
                case "examType":
                    if (!lesson) {
                        return ""
                    }
                    return lesson.examType
                case "css":
                    if (!lesson) {
                        if (this.markedDayOffs.find(d => d == date.specificDay)) {
                            return css + "bg-orange-200 hover:bg-orange-50"
                        }
                        return css + ((new Date().toDateString() == actualDate.toDateString()) ? "bg-slate-300 hover:bg-gray-50" : "bg-gray-200 hover:bg-gray-50")
                    }
                    return css + (lesson.examType ? "bg-yellow-200 hover:bg-yellow-50" : "bg-green-200 hover:bg-green-50")
                case "isOcupied":
                    if (!lesson) {
                        return "false"
                    }
                    return "true"
                default:
                    return ""
            }
        },
        handleGoBack() {
            this.eventBus.emit("set-selected-class-id-schedule-classes-page", 0)
        },
        openAutoSchedulePopup() {
            this.isOpenAutoSchedulePopup = true
        },
        closeAutoSchedulePopup() {
            this.isOpenAutoSchedulePopup = false
        },
        toggleIsMarking() {
            this.isMarking = !this.isMarking
        },
        isDateMarked(date) {
            return this.markedDayOffs.includes(date);
        },
        toggleDateMarking(date) {
            if (this.isDateMarked(date)) {
                const index = this.markedDayOffs.indexOf(date);
                this.markedDayOffs.splice(index, 1);
            } else {
                this.markedDayOffs.push(date);
            }
        },
        clearAllMarking() {
            this.markedDayOffs = []
        },
        toggleAddLessonPopup(shift, date) {
            if (this.selectedYear != new Date().getFullYear()) {
                this.eventBus.emit("open-result-dialog", {
                    message: "You can't update or add here!",
                    type: "Other"
                })
            } else {
                this.selectedDate = date
                this.selectedShift = shift
                console.log(shift)
                this.isOpenAddLessonPopup = !this.isOpenAddLessonPopup
            }

        },
        toggleEditLessonPopup(shift, date) {
            if (this.selectedYear != new Date().getFullYear()) {
                this.eventBus.emit("open-result-dialog", {
                    message: "You can't update or add here!",
                    type: "Other"
                })
            } else {
                if (shift && date) {
                    this.selectedLessonId = this.getLessonDetail(shift, date, "id")
                    this.selectedDate = date.specificDay
                    this.selectedShift = shift
                }
                this.isOpenEditLessonPopup = !this.isOpenEditLessonPopup
            }
        },
        async onDrop(shift, day) {
            //alert("Moving " + this.selectedLessonId + " to " + shift + " " + day.specificDay)
            const request = {
                id: this.selectedLessonId,
                shift: this.shifts.indexOf(shift) + 1,
                date: this.slashDateFormatToSqlDateString(day.specificDay),
            }
            try {
                await axios.patch(import.meta.env.VITE_API_URL + `/api/Lesson`, request)

                this.eventBus.emit("open-result-dialog", {
                    message: "Updated Successfully",
                    type: "Success"
                })
                await this.handleSelectedWeekChange()
            } catch (e) {
                console.log(e)
                this.eventBus.emit("open-result-dialog", {
                    message: e.response?.data?.ErrorMessage ?? "Somemthing went wrong",
                    type: "Error"
                })
            }
        },
        dragSetup(shift, date) {
            this.selectedLessonId = this.getLessonDetail(shift, date, "id")
        },
        async handleClear(confirmation){
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "This action will delete all not started lessons (not taken attendence) of this class. Willing to continue?",
                    method : this.handleClear,
                    params : false
                })
            } else {
                try {
                    this.eventBus.emit("open-loading-popup",{
                        message : "Deleteing... Please don't quit!"
                    })
                    await axios.delete(import.meta.env.VITE_API_URL + `/api/Lesson/${this.classId}/clear`)
                    this.eventBus.emit("close-loading-popup")
                    this.eventBus.emit("open-result-dialog", {
                        message: "Deleted Successfully",
                        type: "Success"
                    })
                    
                    this.eventBus.emit("refresh-lesson-schedule-class-detail")
                    this.close()
                } catch (e) {
                    this.eventBus.emit("close-loading-popup")
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response.data.ErrorMessage,
                        type: "Error"
                    })
                }
            }
        },
        countFinsihedLessons(){
            console.log(this.class)
            return this.class?.lessons.filter(l => l.isLocked).length ?? 0
        }

    },
    mounted() {
        this.refresh();

        this.eventBus.on("refresh-lesson-schedule-class-detail", async () => {
            await this.handleSelectedWeekChange()
        })
    }
}
</script>
<style></style>