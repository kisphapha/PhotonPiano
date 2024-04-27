<template>
    <div class="mt-4 ml-4 mr-4">
        <div class="text-4xl font-bold">Scheduling Entire Center</div>
        <div class="px-8 flex gap-4 mt-4">
            <button @click="toggleOpenAutoSchedulePopup"
                class="p-2 bg-blue-400 rounded-lg text-white font-bold shadow-md hover:bg-blue-200">
                Auto Schedule All Classes
            </button>
            <button @click="null" class="p-2 bg-red-400 rounded-lg text-white font-bold shadow-md hover:bg-red-200">
                Clear All Not Yet Lessons
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
                                <button :class='getLessonDetail(shift, day, "css")'>
                                    <span class="text-xl font-bold text-green-600">
                                        {{ getLessonDetail(shift, day, "number") > 0 ? getLessonDetail(shift, day,
                "number") + " lessons" : "" }}
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
                        @click="toggleOpenAutoSchedulePopup">X</button>
                    <AutoScheduleAllForm :markedDayOffs="this.markedDayOffs" :close="toggleOpenAutoSchedulePopup"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import AutoScheduleAllForm from '../../components/Staff/AutoScheduleAllForm.vue';

export default {
    name: "SchedulePage",
    inject: ["eventBus"],
    components : {AutoScheduleAllForm},
    data() {
        return {
            years: [

            ],
            weeksInYear: [
                {
                    start: new Date(),
                    end: new Date()
                }
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
            daysInWeek: [
                { dayInWeek: "Sunday", specificDay: "07/01" },
                { dayInWeek: "Monday", specificDay: "01/01" },
                { dayInWeek: "Tuesday", specificDay: "02/01" },
                { dayInWeek: "Wednesday", specificDay: "03/01" },
                { dayInWeek: "Thusday", specificDay: "04/01" },
                { dayInWeek: "Friday", specificDay: "05/01" },
                { dayInWeek: "Saturday", specificDay: "06/01" },
            ],
            selectedWeek: null,
            selectedYear: new Date().getFullYear(),
            lessons: [
                {
                    id: 1,
                    shift: 1,
                    date: "2024-01-07",
                    class: {
                        id: 1,
                        name: "BLUE_69_2024"
                    },
                },
                {
                    id: 2,
                    shift: 2,
                    date: "2024-01-08",
                    class: {
                        id: 1,
                        name: "BLUE_69_2024"
                    },
                },
                {
                    id: 3,
                    shift: 3,
                    date: "2024-01-09",
                    class: {
                        id: 1,
                        name: "BLUE_69_2024"
                    },
                },
                {
                    id: 4,
                    shift: 3,
                    date: "2024-01-09",
                    class: {
                        id: 1,
                        name: "PINK_96_2024"
                    },
                }
            ],
            user: null,
            isMarking: false,
            markedDayOffs: [],
            isOpenAutoSchedulePopup : false,
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
            //await this.fetchLessons(inputDate.toDateString(), endDate.toDateString())
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
            const userPromise = new Promise((resolve) => {
                this.eventBus.emit("get-staff-user", resolve);
            });
            const user = await userPromise;
            this.user = user

            // const response = await axios.get(import.meta.env.VITE_API_URL + '/api/Classes/' + user.students[0].currentClassId)

            // this.class = response.data
            this.weeksInYear = this.getWeeksOfYear(new Date().getFullYear())
            this.years = this.getYears()
            this.selectedWeek = this.getFirstDayOfWeek()
            await this.handleSelectedWeekChange()
        },
        async fetchLessons(startDate, endDate) {
            let query = "";
            let queryCount = 0;
            if (startDate) {
                queryCount++;
                query += "StartDate=" + startDate
            }
            if (endDate) {
                query += (queryCount == 0 ? "" : "&") + "EndDate=" + endDate
            }
            const response = await axios.get(import.meta.env.VITE_API_URL + "/api/StudentClasses/" + this.user.students[0].id + "/get-lessons/" + this.user.students[0].currentClassId + "?" + query)
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
            const lessons = this.lessons.filter(l => l.shift == this.shifts.indexOf(shift) + 1 && (new Date(l.date).toDateString() == actualDate.toDateString()))
            switch (information) {
                case "number":
                    return lessons.length
                case "css":
                    if (!lessons[0]) {
                        if (this.markedDayOffs.find(d => d == date.specificDay)) {
                            return css + "bg-orange-200 hover:bg-orange-50"
                        }
                        return css + ((new Date().toDateString() == actualDate.toDateString()) ? "bg-slate-300 hover:bg-gray-50" : "bg-gray-200 hover:bg-gray-50")
                    }
                    return css + "bg-green-200 hover:bg-green-50"

                default:
                    return ""
            }
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
        toggleOpenAutoSchedulePopup(){
            this.isOpenAutoSchedulePopup = !this.isOpenAutoSchedulePopup
        }
    },
    mounted() {
        this.refresh();

    }
}

</script>

<style></style>