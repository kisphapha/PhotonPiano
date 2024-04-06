<template>
    <div>
        <div class="p-12 bg-gray-100">
            <div class="text-4xl flex">
                CLASS NAME :
                <div class="font-bold flex ml-4">BLACK_69_2024
                    <img src="../assets/diamond_black.png" class="w-16 ml-4">
                </div>
            </div>
            <div class="mt-4">
                <div class="text-2xl">
                    LEVEL : <span class="font-bold">4</span> (Advanced)
                </div>
                <div class="text-2xl">
                    INSTRUCTOR : <span class="font-bold">Baby Shark</span>
                </div>
            </div>
            <div class="italic text-lg">
                (15/01/2024 - 01/12/2024)
            </div>
        </div>
        <div>
            <div class="p-12 text-xl">
                <div class="text-2xl">This month has : </div>
                <div class="p-12 flex gap-12">
                    <div
                        class="pl-12 pr-12 pt-6 pb-6 w-1/4 rounded-xl border border-orange-400 text-center text-orange-400 font-bold hover:bg-orange-200">
                        0 Assessments
                    </div>
                    <div
                        class="pl-12 pr-12 pt-6 pb-6 w-1/4 rounded-xl border border-blue-400 text-center text-blue-400 font-bold  hover:bg-blue-200">
                        12 Lessons
                    </div>
                    <div
                        class="pl-12 pr-12 pt-6 pb-6 w-1/4 rounded-xl border border-red-400 text-center text-red-400 font-bold  hover:bg-red-200">
                        0 Tuition debts
                    </div>
                </div>
            </div>
            <hr>
            <div class="p-12">
                <div class="flex gap-16">
                    <div class="text-2xl">Timetable:</div>
                    <div class="font-bold flex gap-8">
                        <button @click="moveWeek(false)" class="bg-gray-300 rounded-xl px-2 hover:bg-slate-100  text-5xl ">◄</button>
                        <select v-model="selectedWeek" @change="handleSelectedWeekChange"
                            class="text-xl font-normal  rouded-lg rouded-lg border">
                            <option v-for="week in weeksInYear" :key="week.start" :value="week.start">
                                {{ week.start + " - " + week.end }}
                            </option>
                        </select>
                        <button @click="moveWeek(true)" class="bg-gray-300 rounded-xl px-2 hover:bg-slate-100  text-5xl ">►</button>
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
                                    <div
                                        class="h-16 flex items-center justify-center bg-gray-200 rounded-xl min-w-32 hover:bg-gray-50">
                                        {{ Math.random() < 0.5 ? "Mozart 1" : "" }} </div>
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
//import { RouterLink } from 'vue-router';

export default {
    name: "ClassDetailOfStudent",
    data() {
        return {
            colors: [
                "diamond_blue",
                "diamond_pink",
                "diamond_red",
                "diamond_black",
                "diamond_white"
            ],
            daysInWeek: [
                { dayInWeek: "Monday", specificDay: "01/01" },
                { dayInWeek: "Tuesday", specificDay: "02/01" },
                { dayInWeek: "Wednesday", specificDay: "03/01" },
                { dayInWeek: "Thusday", specificDay: "04/01" },
                { dayInWeek: "Friday", specificDay: "05/01" },
                { dayInWeek: "Saturday", specificDay: "06/01" },
                { dayInWeek: "Sunday", specificDay: "07/01" }
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
            selectedWeek: '',
            selectedYear: new Date().getFullYear()
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
        handleSelectedWeekChange() {
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
        },
        handleSelectedYearChange() {
            this.weeksInYear = this.getWeeksOfYear(this.selectedYear)
            this.selectedWeek = this.weeksInYear[0].start
            this.handleSelectedWeekChange()
        }, 
        getFirstDayOfWeek() {
            const today = new Date();
            const dayOfWeek = today.getDay();
            const firstDay = new Date(today);
            firstDay.setDate(today.getDate() - dayOfWeek);
            return firstDay.toLocaleDateString();
        },
        moveWeek(forward){
            const currentIndex = this.weeksInYear.findIndex((week) => week.start === this.selectedWeek);
            if (forward && currentIndex < this.weeksInYear.length - 1){
                this.selectedWeek = this.weeksInYear[currentIndex + 1].start
                this.handleSelectedWeekChange()
            } else if (!forward && currentIndex > 0){
                this.selectedWeek = this.weeksInYear[currentIndex - 1].start
                this.handleSelectedWeekChange()
            }
        }
    },
    mounted() {
        this.weeksInYear = this.getWeeksOfYear(2024)
        this.getYears()
        this.selectedWeek = this.getFirstDayOfWeek()
        this.handleSelectedWeekChange()
    }
}
</script>
<style></style>