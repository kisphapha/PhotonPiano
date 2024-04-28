<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl mb-8 flex gap-4">
            <span>Auto Schedule For Class :</span>
            <span class="font-bold">{{ this.class.name }}</span>
        </div>
        <div class="mt-2 h-96 px-2 overflow-y-auto">
            <div class="flex gap-2">
                <div class="p-2">Lessons Each Week</div>
                <input class="p-2 rounded-lg border" type="number" min="0" v-model="lessonEachWeek"
                    @change="calcLessons">
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2">Starting From</div>
                <select class="p-2 rounded-lg border" v-model="startDate" @change="calcLessons">
                    <option v-for="week in this.weeksInYear" :key="week.start" :value="week.start">{{
                week.start.toLocaleDateString() }}</option>
                </select>
            </div>
            <div class="flex place-content-between mt-2 ">
                <div class="flex gap-2">
                    <div class="p-2">Total Weeks</div>
                    <input class="p-2 rounded-lg border" type="number" min="1" max="52" v-model="totalWeeks"
                        @change="calcLessons">
                </div>
                <div class="italic p-2">
                    (Total {{ totalLessons }} lessons)
                </div>
            </div>

            <div class="mt-4 flex place-content-between gap-4 ">
                <div>
                    <div class="text-xl font-bold">Locations</div>
                    <div class="overflow-y-auto h-32 overflow-x-hidden">
                        <div v-for="location in locations" :key="location.id" class="flex gap-8 ">
                            <div class="w-32">{{ location.name }} ({{ location.capacity }})</div>
                            <input type="checkbox" :checked="isLocationSelected(location.id)"
                                @change="toggleLocationSelection(location.id)" />
                        </div>
                    </div>

                </div>
                <div>
                    <div class="text-xl font-bold">Shifts</div>
                    <div class="overflow-y-auto h-32 overflow-x-hidden">
                        <div v-for="shift in shifts" :key="shift.id" class="flex gap-8">
                            <div class="w-24">{{ shift.detail }}</div>
                            <input type="checkbox" :checked="isShiftSelected(shift.id)"
                                @change="toggleShiftSelection(shift.id)" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="mt-2">
                <div class="p-2 text-xl font-bold">Options</div>
                <input type="checkbox" v-model="optionWeekTimeConsistent" /><span class="ml-2">Shift consistence
                    between weeks</span><br>
                <input type="checkbox" v-model="optionWeekLocationConsistent" /><span class="ml-2">Location
                    consistence between weeks</span><br>
                <input type="checkbox" v-model="optionSameLocationAWeek" /><span class="ml-2">Same location in a
                    week</span><br>
                <input type="checkbox" v-model="optionSameLocationAWeek" /><span class="ml-2">Include
                    Saturday</span><br>
                <input type="checkbox" v-model="optionSameLocationAWeek" /><span class="ml-2">Include
                    Sunday</span><br>
                <input type="checkbox" v-model="optionIgnoreDayOff" /><span class="ml-2">Ignore Marked
                    Day-offs <span class="text-orange-400">(Marked {{ this.markedDayOffs.length}} days)</span></span>
            </div>
            <div class="mt-2 flex gap-4 justify-center">
                <button @click="handleApply" class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold">Apply</button>
                <button class="p-2 text-red-400 underline font-bold" @click="close">Cancel</button>
            </div>
        </div>



    </div>
</template>

<script>
import axios from 'axios';
//import { RouterLink } from 'vue-router';

export default {
    name: "AutoScheduleAClassForm",
    inject: ['eventBus'],
    props: ['classId', 'markedDayOffs', 'close'],
    data() {
        return {
            class: {
                name: "BLUE_69_2024",
                level: 5,
                startDate: "2024-01-01",
                endDate: "2024-12-31",
                instructor: {
                    user: {
                        name: "Diamondzz"
                    }
                }
            },
            lessonEachWeek: 3,
            totalWeeks: 35,
            startDate: this.toSqlDateString(new Date()),
            locations: [],
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
            weeksInYear: [
                {
                    start: new Date(),
                    end: new Date()
                }
            ],
            optionWeekTimeConsistent: false,
            optionWeekLocationConsistent: false,
            optionSameLocationAWeek: false,
            optionSaturday: false,
            optionSunday: false,
            optionIgnoreDayOff: false,
            totalLessons: 0,


        }
    },
    mounted() {
        this.weeksInYear = this.getWeeksOfYear(new Date().getFullYear())
        this.refresh()
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
        calcLessons() {
            this.totalLessons = this.lessonEachWeek * this.totalWeeks;
        },
        handleClickDayOff() {
            this.eventBus.emit("toggle-day-off-popup-schedule-class-age")
        },
        async fetchLocations() {
            const locations = await axios.get(import.meta.env.VITE_API_URL + `/api/Location?Status=Available`)

            if (locations.data) {
                this.locations = locations.data
            }
        },
        async refresh() {
            await this.fetchLocations()
        },
        async handleApply() {
            const dateIndex = this.weeksInYear.findIndex(w => w.start == this.startDate)
            console.log(this.weeksInYear.length,dateIndex,this.totalWeeks)
            if (dateIndex == -1 || this.weeksInYear.length - (dateIndex) < this.totalWeeks) {
                this.eventBus.emit("open-result-dialog", {
                    message: "Can't proceed because there are not enough weeks to meet the constrain",
                    type: "Error"
                })
            } else {

            }
        }
    }
}
</script>
<style></style>