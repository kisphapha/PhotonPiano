<template>
    <div class="mt-4 ml-4 mr-4">
        <div class="text-4xl font-bold">Entrance Test Arrangement</div>
        <div class="mt-4 flex gap-8">
            <button v-if="this.selectedYear == new Date().getFullYear()" @click="handleAnnounceTimeAll(true)"
                class="p-2 bg-blue-800 hover:bg-blue-400 rounded-lg text-white font-bold flex gap-4">
                <span class="p-1">Annouce Time All</span>
                <span class="material-icons text-2xl">
                    notifications_none
                </span>
            </button>
            <button v-if="this.selectedYear == new Date().getFullYear()" @click="handleAnnounceScoreAll(true)"
                class="p-2 border bg-cyan-400 hover:bg-cyan-200 rounded-lg text-white font-bold flex gap-4">
                <span class="p-1">Annouce Score All</span>
                <span class="material-icons text-2xl">
                    notifications
                </span>
            </button>
            <button v-if="this.selectedYear == new Date().getFullYear()"
                class="p-2 bg-blue-400 hover:bg-blue-200 rounded-lg text-white font-bold"
                @click="toggleAutoArrangePopup">
                Auto-Arrange
            </button>
        </div>
        <div class="mt-8">
            <select v-model="selectedYear" class="text-xl font-normal rouded-lg border px-8 py-2"
                @change="handleSelectedYearChange">
                <option v-for="year in years" :key="year" :value="year">{{ year }}</option>
            </select>
        </div>

        <div class="mt-4">
            <button v-if="this.selectedYear == new Date().getFullYear()"
                class=" p-4 border border-dotted w-1/2 flex gap-8" @click="toggleAddPopup">
                <div class="material-icons text-5xl text-green-400">
                    add_circle
                </div>
                <div class="mt-auto mb-auto text-2xl">
                    New Entrance Test Slot
                </div>
            </button>
            <div v-for="slot in slots" :key="slot.id" class="flex gap-6 mt-4 ">
                <div class="p-4 w-1/2 bg-slate-700 text-white rounded-xl">
                    <div class="mt-auto mb-auto text-2xl font-bold">
                        Entrance Test {{ slot.id }}
                    </div>
                    <div class="flex place-content-between">
                        <div class="mt-auto mb-auto text-lg">
                            Location : {{ slot.location.name }}
                        </div>
                        <div class="mt-auto mb-auto text-lg">
                            Time : {{ this.shifts[slot.shift - 1] }}
                        </div>
                        <div class="mt-auto mb-auto text-lg">
                            Day : {{ slot.date }}
                        </div>
                    </div>
                </div>
                <div class="mt-auto mb-auto flex gap-4" v-if="this.selectedYear == new Date().getFullYear()">
                    <div class="flex flex-col justify-center">
                        <button @click="openEditPopup(slot.id)">
                            <span class="material-icons">
                                edit
                            </span>
                        </button>
                        <div class="text-sm italic">(Edit)</div>
                    </div>
                    <div class="flex flex-col justify-center">
                        <button @click="handleDelete(true, slot.id)">
                            <span class="material-icons">
                                delete
                            </span>
                        </button>
                        <div class="text-sm italic">(Delete)</div>
                    </div>
                    <div v-if="!slot.isAnnoucedTime" class="flex flex-col justify-center">
                        <button @click="handleAnnounceTime(true, slot.id)">
                            <span class="material-icons">
                                notifications_none
                            </span>
                        </button>
                        <div class="text-sm italic">(Announce Time)</div>
                    </div>
                    <div v-if="!slot.isAnnoucedScore" class="flex flex-col justify-center">
                        <button @click="handleAnnounceScore(true, slot.id)">
                            <span class="material-icons">
                                notifications
                            </span>
                        </button>
                        <div class="text-sm italic">(Announce Score)</div>
                    </div>

                </div>
            </div>

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
                    <EntranceTestSlotForm title="Edit entrance test slot" :slotId="this.selectedSlotId" />
                </div>
            </div>
        </div>
        <div v-if="isOpenAutoArrangePopup" class="popup-overlay">
            <div class="flex justify-center items-center w-2/3 ">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAutoArrangePopup">X</button>
                    <EntranceTestAutoArrangeForm />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import EntranceTestSlotForm from '../../components/Staff/EntranceTestSlotForm.vue'
import EntranceTestAutoArrangeForm from '../../components/Staff/EntranceTestAutoArrangeForm.vue'

export default {
    name: "EntranceTestArrangePage",
    inject: ["eventBus"],
    components: { EntranceTestSlotForm, EntranceTestAutoArrangeForm },
    data() {
        return {
            slots: [
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
            editDto: {
                locationId: 0,
                shiftId: 0,
                instructorId: 0,
                selectedStudentIds: []
            },
            isOpenAddPopup: false,
            isOpenEditPopup: false,
            isOpenAutoArrangePopup: false,
            selectedYear: new Date().getFullYear(),
            selectedSlotId: 0,
            user: null
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
        toggleAutoArrangePopup() {
            this.isOpenAutoArrangePopup = !this.isOpenAutoArrangePopup
        },
        openEditPopup(id) {
            this.selectedSlotId = id
            this.isOpenEditPopup = true
        },
        closeEditPopup() {
            this.isOpenEditPopup = false
        },
        async refresh() {
            const userPromise = new Promise((resolve) => {
                this.eventBus.emit("get-staff-user", resolve);
            });
            const user = await userPromise;
            this.user = user

            await this.fetchData()
        },
        async fetchData() {
            const slots = await axios.get(import.meta.env.VITE_API_URL + `/api/EntranceTest/slots?year=${this.selectedYear}`)

            if (slots.data) {
                this.slots = slots.data.sort(this.compareSlot)
            }
        },

        async handleSelectedYearChange() {
            await this.fetchData()
        },

        async handleDelete(confirmation, id) {
            if (confirmation) {
                this.selectedSlotId = id;
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "delete-entrance-test-slot-entrance-test-arrange-page"
                })
            } else {
                try {
                    await axios.delete(import.meta.env.VITE_API_URL + `/api/EntranceTest/${this.selectedSlotId}`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Deleted Successfully",
                        type: "Success"
                    })
                    await this.fetchData()
                } catch (e) {
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response.data.ErrorMessage,
                        type: "Error"
                    })
                }
            }
        },
        async handleAnnounceTime(confirmation, id) {
            if (confirmation) {
                this.selectedSlotId = id;
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "announce-slot-time-entrance-test-arrange-page"
                })
            } else {
                try {
                    await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/${this.selectedSlotId}/announce`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Annouced Successfully",
                        type: "Success"
                    })
                    await this.fetchData()
                } catch (e) {
                    console.log(e)
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response?.data?.ErrorMessage ?? "Something went wrong",
                        type: "Error"
                    })
                }
            }
        },
        async handleAnnounceScore(confirmation, id) {
            if (confirmation) {
                this.selectedSlotId = id;
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "announce-slot-score-entrance-test-arrange-page"
                })
            } else {
                try {
                    await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/${this.selectedSlotId}/announce-score`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Annouced Successfully",
                        type: "Success"
                    })
                    await this.fetchData()
                } catch (e) {
                    console.log(e)
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response?.data?.ErrorMessage ?? "Something went wrong",
                        type: "Error"
                    })
                }
            }
        },
        async handleAnnounceTimeAll(confirmation) {
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "announce-slot-time-all-entrance-test-arrange-page"
                })
            } else {
                try {
                    await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/announce-time-all`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Annouced Successfully",
                        type: "Success"
                    })
                    await this.fetchData()
                } catch (e) {
                    console.log(e)
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response?.data?.ErrorMessage ?? "Something went wrong",
                        type: "Error"
                    })
                }
            }
        },
        async handleAnnounceScoreAll(confirmation) {
            if (confirmation) {
                this.eventBus.emit("open-confirmation-popup", {
                    message: "Are you sure about this?",
                    callback: "announce-slot-score-all-entrance-test-arrange-page"
                })
            } else {
                try {
                    await axios.patch(import.meta.env.VITE_API_URL + `/api/EntranceTest/announce-score-all`)
                    this.eventBus.emit("open-result-dialog", {
                        message: "Annouced Successfully",
                        type: "Success"
                    })
                    await this.fetchData()
                } catch (e) {
                    console.log(e)
                    this.eventBus.emit("open-result-dialog", {
                        message: e.response?.data?.ErrorMessage ?? "Something went wrong",
                        type: "Error"
                    })
                }
            }
        },
        compareSlot(a, b) {
            if (new Date(a.date) < new Date(b.date)) {
                return -1;
            } else if (new Date(a.date) > new Date(b.date)) {
                return 1;
            }
            if (a.shift < b.shift) {
                return -1;
            } else if (a.shift > b.shift) {
                return 1;
            }
            return 0;
        }
    },
    mounted() {
        this.getYears()
        this.eventBus.on("toggle-add-entrance-test-slot-popup", () => {
            this.toggleAddPopup()
        })
        this.eventBus.on("toggle-auto-arrange-entrance-test-slot-popup", () => {
            this.toggleAutoArrangePopup()
        })
        this.eventBus.on("close-edit-entrance-test-slot-popup", () => {
            this.closeEditPopup()
        })
        this.eventBus.on("delete-entrance-test-slot-entrance-test-arrange-page", () => {
            this.handleDelete(false, 0)
        })
        this.eventBus.on("announce-slot-time-entrance-test-arrange-page", () => {
            this.handleAnnounceTime(false, 0)
        })
        this.eventBus.on("announce-slot-score-entrance-test-arrange-page", () => {
            this.handleAnnounceScore(false, 0)
        })
        this.eventBus.on("announce-slot-time-all-entrance-test-arrange-page", () => {
            this.handleAnnounceTimeAll(false)
        })
        this.eventBus.on("announce-slot-score-all-entrance-test-arrange-page", () => {
            this.handleAnnounceScoreAll(false)
        })
        this.eventBus.on("refresh-entrance-test-arrange-page", () => {
            this.refresh()
        })
        this.refresh()
    }
}

</script>

<style></style>