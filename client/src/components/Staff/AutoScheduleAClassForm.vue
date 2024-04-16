<template>
    <div class="w-full px-6 py-8 bg-white rounded-lg shadow-md">
        <div class="text-2xl mb-8 flex gap-4">
            <span>Auto Schedule For Class :</span>
            <span class="font-bold">{{ this.class.name }}</span>
        </div>
        <div class="mt-2 h-96 px-2 overflow-y-auto">
            <div class="flex gap-2">
                <div class="p-2">Lessons Each Week</div>
                <input class="p-2 rounded-lg border" type="number" min="0" v-model="lessonEachWeek"  @change="calcLessons">
            </div>
            <div class="mt-2 flex gap-2">
                <div class="p-2">Starting From</div>
                <select class="p-2 rounded-lg border" type="date" v-model="startDate" @change="calcLessons">
                    <option v-for="week in this.weeksInYear" :value="week.start">{{ week.start }}</option>
                </select>
            </div>
            <div class="flex place-content-between mt-2 ">
                <div class="flex gap-2">
                    <div class="p-2">Total Weeks</div>
                    <input class="p-2 rounded-lg border" type="number" min="1" max="52" v-model="totalWeeks"
                        @change="calcLessons">
                </div>
                <div class="italic p-2">
                    (Total {{totalLessons}} lessons)
                </div>
            </div>

            <div class="mt-4 flex place-content-between gap-4 ">
                <div>
                    <div class="text-xl font-bold">Locations</div>
                    <div class="overflow-y-auto h-32">
                        <div v-for="location in locations" :key="location.id" class="flex gap-8 ">
                            <div class="w-24">{{ location.name }}</div>
                            <input type="checkbox" :checked="isLocationSelected(location.id)"
                                @change="toggleLocationSelection(location.id)" />
                        </div>
                    </div>

                </div>
                <div>
                    <div class="text-xl font-bold">Shifts</div>
                    <div class="overflow-y-auto h-32">
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
                <input type="checkbox" v-model="optionWeekTimeConsistent"></input><span class="ml-2">Shift consistence
                    between weeks</span><br>
                <input type="checkbox" v-model="optionWeekLocationConsistent"></input><span class="ml-2">Location
                    consistence between weeks</span><br>
                <input type="checkbox" v-model="optionSameLocationAWeek"></input><span class="ml-2">Same location in a
                    week</span>
            </div>
            <div class="mt-2 p-2">
                <div class=" text-xl font-bold">Day-offs</div>
                <div class="italic">
                    (Total 30 day-offs)
                </div>
                <button class="px-4 py-2 bg-lime-500 rounded-lg hover:bg-lime-300 flex gap-2">
                    <span class="text-white font-bold">Select Day-offs </span>
                    <span class="material-icons">settings</span>
                </button>
            </div>
            <div class="mt-2 flex gap-4 justify-center">
                <button class="bg-blue-400 hover:bg-blue-200 p-2 rounded-lg text-white font-bold">Apply</button>
                <button class="p-2 text-red-400 underline font-bold" @click="handleCancel">Cancel</button>
            </div>
        </div>



    </div>
</template>

<script>
//import { RouterLink } from 'vue-router';

export default {
    name: "AutoScheduleAClassForm",
    inject: ['eventBus'],
    props: [],
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
            locations: [
                {
                    id: 1,
                    name: "Mozart",
                    capacity: 30
                },
                {
                    id: 2,
                    name: "Beethoven",
                    capacity: 25
                },
                {
                    id: 3,
                    name: "Lizst",
                    capacity: 20
                },
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
            weeksInYear: [
                {
                    start: '2024-01-01',
                    end: '2024-31-12'
                }
            ],
            optionWeekTimeConsistent: false,
            optionWeekLocationConsistent: false,
            optionSameLocationAWeek: false,
            totalLessons : 0

        }
    },
    mounted() {
        this.weeksInYear = this.getWeeksOfYear(new Date().getFullYear())
    },
    methods: {
        toggleAutoAcceptPopupEntranceTestRegistrationPage() {
            this.eventBus.emit("toggle-auto-accept-popup-registration-page")
        },
        handleCancel() {
            this.eventBus.emit("toggle-auto-schedule-class-popup-schedule-class-age")
        },
        toSqlDateString(date) {
            const year = date.getFullYear(); // Get the year (4 digits)
            const month = String(date.getMonth() + 1).padStart(2, '0'); // Get the month (0-11) and pad with leading zero if needed
            const day = String(date.getDate()).padStart(2, '0'); // Get the day of the month and pad with leading zero if needed
            return `${year}-${month}-${day}`; // Concatenate the year, month, and day with hyphens
        },
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
        getWeeksOfYear(year) {
            const weeks = new Array;

            const startDate = new Date(year, 0, 1);
            const firstSunday = startDate.getDate() + (7 - startDate.getDay());
            startDate.setDate(firstSunday);

            while (startDate.getFullYear() === year) {
                const endDate = new Date(startDate);
                endDate.setDate(startDate.getDate() + 6);

                weeks.push({
                    start: this.toSqlDateString(startDate),
                    end: this.toSqlDateString(endDate)
                });

                startDate.setDate(startDate.getDate() + 7);
            }

            return weeks;
        },
        calcLessons() {
            this.totalLessons = this.lessonEachWeek * this.totalWeeks;
        }
    }
}
</script>
<style></style>