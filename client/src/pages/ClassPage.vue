<template>
    <div v-if="this.class">
        <div class="p-12 bg-gray-100">
            <div class="text-4xl flex">
                CLASS NAME :
                <div class="font-bold flex ml-4">{{ this.class.name }}
                    <img :src="this.colors[this.class.level - 1]" class="w-16 ml-4">
                </div>
            </div>
            <div class="mt-4">
                <div class="text-2xl">
                    LEVEL : <span class="font-bold">{{ this.class.level }}</span> {{ this.class_level[this.class.level - 1] }}
                </div>
                <div class="text-2xl">
                    INSTRUCTOR : <span class="font-bold">{{ this.class.instructor.user.name }}</span>
                </div>
            </div>
            <div class="italic text-lg">
                {{ "(" + this.class.startDate + " - " + this.class.endDate + ")" }}
            </div>
        </div>
        <div>
            <div class="p-12 text-xl">
                <div class="text-2xl">This month has : </div>
                <div class="p-12 flex gap-12">
                    <div
                        class="pl-12 pr-12 pt-6 pb-6 w-1/4 rounded-xl border border-orange-400 text-center text-orange-400 font-bold hover:bg-orange-200">
                        {{ this.thisMonthAssesments }} Assessments
                    </div>
                    <div
                        class="pl-12 pr-12 pt-6 pb-6 w-1/4 rounded-xl border border-blue-400 text-center text-blue-400 font-bold  hover:bg-blue-200">
                        {{ this.thisMonthLessons }} Lessons
                    </div>
                    <div
                        class="pl-12 pr-12 pt-6 pb-6 w-1/4 rounded-xl border border-red-400 text-center text-red-400 font-bold  hover:bg-red-200">
                        {{ this.thisMonthDebts }} Tuition debts
                    </div>
                </div>
            </div>
            <hr>
            <div class="p-12">
                <div class="flex gap-16">
                    <div class="text-2xl">Timetable:</div>
                    <div class="font-bold flex gap-8">
                        <button @click="moveWeek(false)"
                            class="bg-gray-300 rounded-xl px-2 hover:bg-slate-100  text-5xl ">◄</button>
                        <select v-model="selectedWeek" @change="handleSelectedWeekChange"
                            class="text-xl font-normal  rouded-lg rouded-lg border">
                            <option v-for="week in weeksInYear" :key="week.start" :value="week.start">
                                {{ week.start + " - " + week.end }}
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
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="shift in shifts" :key="shift" class="py-2">
                                <td class="py-2">Shift {{ shifts.indexOf(shift) + 1 }}<br>({{ shift }})</td>
                                <td v-for="day in daysInWeek" :key="day" class="py-2 ">
                                    <div :class='this.getLessonDetail(shift, day, "css")'>
                                        <div>
                                            {{ this.getLessonDetail(shift, day, "location") }}
                                        </div>
                                        <div :class='this.getLessonDetail(shift, day, "attendingStyle")'>
                                            {{ this.getLessonDetail(shift, day, "attending") }}
                                        </div>
                                    </div>
                                </td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

</template>

<script>
import axios from 'axios';

export default {
    name: "ClassPage",
    inject: ["eventBus"],
    data() {
        return {
            colors: [
                "src/assets/diamond_blue.png",
                "src/assets/diamond_pink.png",
                "src/assets/diamond_red.png",
                "src/assets/diamond_black.png",
                "src/assets/diamond_white.png"
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
                    start: '2024-01-01',
                    end: '2024-31-12'
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
            selectedWeek: '',
            thisMonthAssesments: 0,
            thisMonthLessons: 0,
            thisMonthDebts: 0,
            selectedYear: new Date().getFullYear(),
            lessons: [],
            class: null,
            user: null,
        }
    },
    methods: {
        getWeeksOfYear(year) {
            const weeks = [];

            const startDate = new Date(year, 0, 1);
            const firstSunday = startDate.getDate() + (7 - startDate.getDay());
            startDate.setDate(firstSunday);

            while (startDate.getFullYear() === year) {
                const endDate = new Date(startDate);
                endDate.setDate(startDate.getDate() + 6);

                weeks.push({
                    start: startDate.toLocaleDateString(),
                    end: endDate.toLocaleDateString()
                });

                startDate.setDate(startDate.getDate() + 7);
            }

            return weeks;
        },
        getYears() {
            let year = new Date().getFullYear()
            this.years.push(year)
            for (var i = 0; i < 3; i++) {
                year -= 1
                this.years.push(year)
            }
        },
        async handleSelectedWeekChange() {
            const inputDateParts = this.selectedWeek.substring(0, 10).split('/');
            const day = parseInt(inputDateParts[0], 10);
            const month = parseInt(inputDateParts[1], 10) - 1; // Months are zero-based in JavaScript
            const year = parseInt(inputDateParts[2], 10);

            const inputDate = new Date(year, month, day);
            this.specDays = [];
            for (let i = 0; i < 7; i++) {
                const nextDay = new Date(inputDate);
                nextDay.setDate(inputDate.getDate() + i);
                this.daysInWeek[i].specificDay = nextDay.toLocaleDateString()
            }
            let endDate = new Date(inputDate)
            endDate.setDate(inputDate.getDate() + 7)
            await this.fetchLessons(inputDate.toDateString(), endDate.toDateString())
        },
        async handleSelectedYearChange() {
            this.weeksInYear = this.getWeeksOfYear(this.selectedYear)
            this.selectedWeek = this.weeksInYear[0].start
            await this.handleSelectedWeekChange()
        },
        getFirstDayOfWeek() {
            const today = new Date();
            const dayOfWeek = today.getDay();
            const firstDay = new Date(today);
            firstDay.setDate(today.getDate() - dayOfWeek);
            console.log(firstDay.toLocaleDateString())
            return firstDay.toLocaleDateString();
        },
        moveWeek(forward) {
            const currentIndex = this.weeksInYear.findIndex((week) => week.start === this.selectedWeek);
            if (forward && currentIndex < this.weeksInYear.length - 1) {
                this.selectedWeek = this.weeksInYear[currentIndex + 1].start
                this.handleSelectedWeekChange()
            } else if (!forward && currentIndex > 0) {
                this.selectedWeek = this.weeksInYear[currentIndex - 1].start
                this.handleSelectedWeekChange()
            }
        },
        monthlyAnalzying() {
            this.thisMonthAssesments = this.class.lessons.filter(l => l.examType && parseInt(l.date.substring(5, 7)) == new Date().getMonth() + 1).length
            this.thisMonthLessons = this.class.lessons.filter(l => !l.examType && parseInt(l.date.substring(5, 7)) == new Date().getMonth() + 1).length
        },
        async refresh() {
            if (!localStorage.token) {
                this.$router.push("/")
            } else {
                const userPromise = new Promise((resolve) => {
                    this.eventBus.emit("get-user", resolve);
                });
                const user = await userPromise;
                this.user = user;

                if (this.user && this.user.role == "Student" && this.user.students[0].status == "InClass") {
                    const response = await axios.get(import.meta.env.VITE_API_URL + '/api/Classes/' + user.students[0].currentClassId)

                    this.class = response.data
                    this.monthlyAnalzying()
                    this.selectedWeek = this.getFirstDayOfWeek()
                    await this.handleSelectedWeekChange()
                    await this.fetchTuitions()
                } else {
                    this.$router.push("/")
                }
            }
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
        async fetchTuitions() {

            const response = await axios.get(import.meta.env.VITE_API_URL + "/api/StudentClasses/" + this.user.students[0].id + "/get-tuitions/" + this.user.students[0].currentClassId)
            if (response.data) {
                //console.log(response.data)
                this.thisMonthDebts = response.data.filter(t => !t.status).length
            }

        },
        getLessonDetail(shift, date, information) {
            let css = "h-16 flex flex-col items-center justify-center rounded-xl min-w-32 "
            const inputDateParts = date.specificDay.split('/');
            const day = parseInt(inputDateParts[0], 10);
            const month = parseInt(inputDateParts[1], 10) - 1; // Months are zero-based in JavaScript
            const year = parseInt(inputDateParts[2], 10);
            const actualDate = new Date(year, month, day)
            const lesson = this.lessons.find(l => l.lesson.shift == this.shifts.indexOf(shift) + 1 && (new Date(l.lesson.date).toDateString() == actualDate.toDateString()))

            switch (information) {
                case "location":
                    if (!lesson) {
                        return ""
                    }
                    return lesson.lesson.location.name
                case "css":
                    if (!lesson) {
                        return (new Date().toDateString() == actualDate.toDateString()) ? css + "bg-slate-300 hover:bg-gray-50" : css + "bg-gray-200 hover:bg-gray-50"
                    }
                    return lesson.lesson.examType ? css + "bg-yellow-200 hover:bg-yellow-50" : css + "bg-green-200 hover:bg-green-50"
                case "attending":
                    if (!lesson) {
                        return ""
                    }
                    return lesson.attendence
                case "attendingStyle":
                    if (!lesson) {
                        return ""
                    }
                    if (lesson.attendence == "Attended")
                        return "font-bold text-green-400"
                    if (lesson.attendence == "Absent")
                        return "font-bold text-red-400"
                    if (lesson.attendence == "NotYet")
                        return "font-bold text-gray-400"
                default:
                    return ""
            }
        },
    },
    mounted() {
        this.eventBus.on("update-class-page", () => {
            this.refresh();
        })
        this.refresh();

        this.weeksInYear = this.getWeeksOfYear(new Date().getFullYear())
        this.getYears()

    }
}
</script>
<style>
</style>