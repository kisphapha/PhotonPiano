<template>
    <div class="p-8" v-if="selectedInstructorId == 0">
        <div class="text-4xl font-bold">Instructors </div>
        <div class="flex gap-2 mt-4 place-content-between">

            <button @click="toggleAddPopup"
                class="flex gap-2 py-2 px-6 bg-green-400 rounded-lg text-white font-bold shadow-md hover:bg-green-200">
                <span>Add New Instructor</span>
            </button>
            <button @click="toggleFilterPopup"
                class="flex gap-2 py-2 px-6 bg-slate-900 rounded-lg text-white font-bold shadow-md hover:bg-slate-500">
                <span>Filter</span>
                <span class="material-icons">
                    filter_list
                </span>
            </button>
        </div>
        <div class="mt-2">
            <table id="staff-table" class="w-full">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Level</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                        <th>Classes Teaching</th>
                        <th>Contribute Score</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="instructor in instructors" :key="instructor.id">
                        <td>{{ instructor.id }}</td>
                        <td>
                            <button class="w-32 break-words underline text-blue-400 font-bold" v-if="editingId != instructor.id" @click="setSelectedInstructorId(instructor.id)">
                                {{ instructor.user.name }}
                            </button>
                            <div v-else>
                                <input class="w-32 p-2 rounded-lg border" v-model="editDto.name">
                            </div>
                        </td>
                        <td>
                            <div v-if="editingId != instructor.id">
                                {{ instructor.level }}
                            </div>
                            <div v-else>
                                <select class="p-2 rounded-lg border w-12" v-model="editDto.level">
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </div>
                        </td>
                        <td>
                            <div class="w-32 break-words" v-if="editingId != instructor.id">
                                {{ instructor.user.email }}
                            </div>
                            <div v-else>
                                <input class="w-32 p-2 rounded-lg border" v-model="editDto.email">
                            </div>
                        </td>
                        <td>
                            <div v-if="editingId != instructor.id">
                                {{ instructor.user.phone }}
                            </div>
                            <div v-else>
                                <input class="w-32 p-2 rounded-lg border" v-model="editDto.phone">
                            </div>
                        </td>
                        <td>{{ instructor.classesTeaching }}</td>
                        <td>
                            <div v-if="editingId != instructor.id">
                                {{ instructor.contributeScore }}
                            </div>
                            <div v-else>
                                <input type="number" class="w-24 p-2 rounded-lg border" v-model="editDto.contributeScore">
                            </div>
                        </td>
                        <td>
                            <div v-if="editingId != instructor.id" :class="getStatusStyle(instructor.status)">
                                {{ instructor.status }}
                            </div>
                            <div v-else>
                                <select class="p-2 rounded-lg border" v-model="editDto.status">
                                    <option value="Active">Active</option>
                                    <option value="Inactive">Inactive</option>
                                    <option value="Fired">Fired</option>
                                </select>
                            </div>
                        </td>
                        <td>
                            <div class="flex gap-2" v-if="editingId == instructor.id">
                                <button class="material-icons text-lime-500 text-3xl" @click="handleEdit()">
                                    check_circle
                                </button>
                                <button class="material-icons text-red-500 text-3xl" @click="setEditingId(0)">
                                    cancel
                                </button>
                            </div>
                            <div v-else>
                                <button class="material-icons text-3xl" @click="setEditingId(instructor.id)">
                                    edit
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="flex gap-4 justify-center mt-4" v-if="this.instructors.length > 0">
                <button @click="movePage(false)">
                    <span class="material-icons p-1">arrow_back_ios</span>
                </button>
                <div class="flex gap-2 ">
                    <input class="border p-1 rounded-md w-16" type="number" v-model="currentPage" min="1"
                        @change="handlePageChange">
                    <div class="p-1"> / {{ this.totalPage }}</div>
                </div>
                <button @click="movePage(true)">
                    <span class="material-icons p-1">arrow_forward_ios</span>
                </button>
            </div>
        </div>
        <div v-if="isOpenFilterPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleFilterPopup">X</button>
                    <InstructorFilterForm :close="toggleFilterPopup" :id="filterDto.id" :name="filterDto.name"
                        :level="filterDto.level" :joinFrom="filterDto.joinFrom" :joinTo="filterDto.joinTo"
                        :email="filterDto.email" :phone="filterDto.phone" :teachFrom="filterDto.teachFrom"
                        :teachTo="filterDto.teachTo" :contributeFrom="filterDto.contributeFrom"
                        :contributeTo="filterDto.contributeTo" :status="filterDto.status" :action="handleFilter" />
                </div>
            </div>
        </div>
        <div v-if="isOpenAddPopup" class="popup-overlay">
            <div class="overflow-y-auto flex justify-center items-center">
                <div class="relative">
                    <button class="absolute right-0 mt-2 mr-2 w-8 h-8 bg-red-400 text-white rounded-full"
                        @click="toggleAddPopup">X</button>
                    <AddNewInstructorForm :close="toggleAddPopup" />
                </div>
            </div>
        </div>
    </div>
    <div v-else>
        <button class="font-bold text-xl text-blue-500 flex p-8" @click="setSelectedInstructorId(0)">
            <span class="material-icons">arrow_back_ios</span> Back
        </button>
        <InstructorProfile :instructorId="selectedInstructorId" />
    </div>
</template>

<script>
import InstructorFilterForm from '../../components/Staff/Instructors/InstructorFilterForm.vue'
import AddNewInstructorForm from '../../components/Staff/Instructors/AddNewInstructorForm.vue'
import InstructorProfile from '../../components/Instructor/InstructorProfile.vue'

export default {
    name: "AdminInstructorPage",
    components: { InstructorFilterForm, AddNewInstructorForm, InstructorProfile },
    inject: ["eventBus"],
    data() {
        return {
            instructors: [
                {
                    id: 1,
                    user: {
                        name: "Diamond",
                        phone: "0987654321",
                        email: "abcabcabcabcabcabcabcabcabcabcabcabcabcabcabc@gmail.com"
                    },
                    joinDate: "2003-01-01",
                    level: 5,
                    classesTeaching: 2,
                    contributeScore: 99999,
                    status: "Active"
                },
                {
                    id: 2,
                    user: {
                        name: "White Wine",
                        phone: "0987654321",
                        email: "abc@gmail.com"
                    },
                    joinDate: "2003-01-01",
                    level: 3,
                    classesTeaching: 1,
                    contributeScore: 696,
                    status: "Inactive"
                }
            ],
            totalPage: 100,
            pageSize: 10,
            currentPage: 1,
            isOpenFilterPopup: false,
            isOpenAddPopup: false,
            filterDto: {
                id: null,
                name: null,
                level: 0,
                joinFrom: null,
                joinTo: null,
                teachFrom: null,
                teachTo: null,
                contributeFrom: null,
                contributeTo: null,
                email: "",
                phone: "",
                status: "all"
            },
            editingId: 0,
            editDto: {
                name: "",
                email: "",
                phone: "",
                status: "",
                level: 0,
                contributeScore: 0
            },
            selectedInstructorId : 0
        }
    },
    methods: {
        async handlePageChange() {
            if (this.currentPage > this.totalPage) {
                this.currentPage = this.totalPage
            }
            if (this.currentPage < 1) {
                this.currentPage = 1
            }
            //await this.fetchRegistration(this.currentPage, this.pageSize, this.keyword_name)
        },
        async movePage(forward) {
            if (forward && this.currentPage < this.totalPage) {
                this.currentPage++
                await this.handlePageChange()
            } else if (!forward && this.currentPage > 1) {
                this.currentPage--
                await this.handlePageChange()
            }
        },
        toggleFilterPopup() {
            this.isOpenFilterPopup = !this.isOpenFilterPopup
        },
        handleFilter(filterDto) {
            this.filterDto.id = filterDto.id
            this.filterDto.name = filterDto.name
            this.filterDto.level = filterDto.level
            this.filterDto.joinFrom = filterDto.joinFrom
            this.filterDto.joinTo = filterDto.joinTo
            this.filterDto.contributeFrom = filterDto.contributeFrom
            this.filterDto.contributeTo = filterDto.contributeTo
            this.filterDto.teachFrom = filterDto.teachFrom
            this.filterDto.teachTo = filterDto.teachTo
            this.filterDto.email = filterDto.email
            this.filterDto.phone = filterDto.phone
            this.filterDto.status = filterDto.status
            this.toggleFilterPopup()
        },
        toggleAddPopup() {
            this.isOpenAddPopup = !this.isOpenAddPopup
        },
        setEditingId(id) {
            this.editingId = id
            const instructor = this.instructors.find(i => i.id == id)
            if (instructor) {
                this.editDto.name = instructor.user.name
                this.editDto.email = instructor.user.email
                this.editDto.phone = instructor.user.phone
                this.editDto.status = instructor.status
                this.editDto.contributeScore = instructor.contributeScore
                this.editDto.level = instructor.level
            }
        },
        setSelectedInstructorId(id){
            this.selectedInstructorId = id
        },
        getStatusStyle(status) {
            let css = "p-2 rounded-lg text-white font-bold "
            switch (status) {
                case "Active":
                    return css + "bg-green-500";
                case "Inactive":
                    return css + "bg-gray-500";
                case "Fired":
                    return css + "bg-red-500";
            }
            return css;
        },
    }
}

</script>

<style></style>