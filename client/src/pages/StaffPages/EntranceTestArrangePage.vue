<template>
    <div class="mt-4 ml-4 mr-4">
        <div class="text-4xl font-bold">Entrance Test Arrangement</div>
        <div class="flex gap-4 mt-4">
            <button class=" p-4 border border-dotted w-1/2 flex gap-8" @click="toggleAddPopup">
                <div class="material-icons text-5xl text-green-400">
                    add_circle
                </div>
                <div class="mt-auto mb-auto text-2xl">
                    New Entrance Test Slot
                </div>
            </button>
            <button class="p-4 bg-blue-400 hover:bg-blue-200 rounded-lg text-white font-bold text-2xl">
                Auto-Arrange
            </button>
        </div>

        <div class="mt-8">
            <select v-model="selectedYear" class="text-xl font-normal rouded-lg border px-8 py-2">
                <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
            </select>
        </div>

        <div class="mt-4">
            <div v-for="slot in slots" :key="slot.id" class="flex gap-6 mt-4 ">
                <div class="p-4 w-1/2 bg-slate-700 text-white rounded-xl">
                    <div class="mt-auto mb-auto text-2xl font-bold">
                        Entrance Test {{ slot.id }}
                    </div>
                    <div class="flex place-content-between">
                        <div class="mt-auto mb-auto text-lg">
                            Location : {{ slot.location }}
                        </div>
                        <div class="mt-auto mb-auto text-lg">
                            Time : {{ this.shifts[slot.shift - 1] }}
                        </div>
                        <div class="mt-auto mb-auto text-lg">
                            Day : {{ slot.day }}
                        </div>
                    </div>
                </div>
                <div class="mt-auto mb-auto flex gap-4">
                    <div class="flex flex-col justify-center">
                        <button @click="openEditPopup(1,2,4,[1,3])">
                            <span class="material-icons">
                                edit
                            </span>
                        </button>
                        <div class="text-sm italic">(Edit)</div>
                    </div>
                    <div class="flex flex-col justify-center">
                        <button>
                            <span class="material-icons">
                                delete
                            </span>
                        </button>
                        <div class="text-sm italic">(Delete)</div>
                    </div>
                    <div v-if="!slot.isAnnouncedTime" class="flex flex-col justify-center">
                        <button>
                            <span class="material-icons">
                                notifications_none
                            </span>
                        </button>
                        <div class="text-sm italic">(Announce Time)</div>
                    </div>
                    <div v-if="!slot.isAnnouncedScore" class="flex flex-col justify-center">
                        <button>
                            <span class="material-icons">
                                notifications
                            </span>
                        </button>
                        <div class="text-sm italic">(Announce Score)</div>
                    </div>

                </div>
            </div>

        </div>
        <div class="mt-12 flex gap-8">
            <button class="p-4 bg-blue-800 hover:bg-blue-400 rounded-lg text-white text-2xl flex gap-4">
                Annouce Time All
                <span class="material-icons text-2xl">
                    notifications_none
                </span>
            </button>
            <button class="p-4 border border-cyan-400 hover:bg-cyan-200 rounded-lg text-blue-800 text-2xl flex gap-4">
                Annouce Score All
                <span class="material-icons text-2xl">
                    notifications
                </span>
            </button>
        </div>

        <div v-if="isOpenAddPopup" class="popup-overlay">
            <div class="flex justify-center items-center w-2/3">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAddPopup">X</button>
                    <EntranceTestSlotForm title="Add new entrance test slot" />
                </div>
            </div>
        </div>
        <div v-if="isOpenEditPopup" class="popup-overlay">
            <div class="flex justify-center items-center w-2/3">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="closeEditPopup">X</button>
                    <EntranceTestSlotForm title="Edit entrance test slot" :locationId="this.editDto.locationId"
                    :instructorId="this.editDto.instructorId" :shiftId="this.editDto.shiftId" 
                    :selectedStudentIds="this.editDto.selectedStudentIds"/>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import EntranceTestSlotForm from '../../components/Staff/EntranceTestSlotForm.vue'

export default {
    name: "EntranceTestArrangePage",
    inject: ["eventBus"],
    components: { EntranceTestSlotForm },
    data() {
        return {
            slots: [
                {
                    id: 1,
                    location: "Mozart_1",
                    shift: 1,
                    day: "2024-02-10",
                    isAnnouncedTime: false,
                    isAnnouncedScore: false,
                },
                {
                    id: 2,
                    location: "Mozart_2",
                    shift: 2,
                    day: "2024-03-10",
                    isAnnouncedTime: true,
                    isAnnouncedScore: false,
                },
                
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
            years: [

            ],
            editDto : {
                locationId : 0,
                shiftId : 0,
                instructorId : 0,
                selectedStudentIds : []
            },
            isOpenAddPopup: false,
            isOpenEditPopup: false,
            selectedYear: new Date().getFullYear()
        }
    },
    methods: {
        getYears() {
            let year = new Date().getFullYear()
            this.years.push(year)
            for (var i = 0; i < 3; i++) {
                year -= 1
                this.years.push(year)
            }
        },
        toggleAddPopup() {
            this.isOpenAddPopup = !this.isOpenAddPopup
        },
        openEditPopup(locationId, instructorId, shiftId, selectedStudentIds) {
            this.isOpenEditPopup = true
            this.editDto.locationId = locationId
            this.editDto.instructorId = instructorId
            this.editDto.shiftId = shiftId
            this.editDto.selectedStudentIds = selectedStudentIds
        },
        closeEditPopup() {
            this.isOpenEditPopup = false
        }
    },
    mounted() {
        this.getYears()
        this.eventBus.on("toggle-add-entrance-test-slot-popup", () => {
            this.toggleAddPopup()
        })
        this.eventBus.on("close-edit-entrance-test-slot-popup", () => {
            this.closeEditPopup()
        })
    }
}

</script>

<style></style>